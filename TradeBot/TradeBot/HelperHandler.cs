/*
Copyright 2016, Abhinav Bhandari (AKA: PixelZerg) -- Offered under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE VERSION 3
THE PROGRAM IS DISTRIBUTED IN THE HOPE THAT IT WILL BE USEFUL, BUT WITHOUT ANY WARRANTY.
IT IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.
IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW THE AUTHOR WILL BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF THE AUTHOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib_K_Relay;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using Lib_K_Relay.Networking.Packets;

namespace PZTradeBot
{
    public class HelperHandler
    {
        public static bool enabled = false;
        public enum HelperType{storage,output,converter,neutral}

        public static List<PZStorage> slist = new List<PZStorage>();
        public static List<PZOutput> olist = new List<PZOutput>();
        public static List<PZConverter> clist = new List<PZConverter>();

        public static HelperType HelperTypeof(PZClient client)
        {
            foreach (var item in slist)
            {
                if (item.client.ToString() == client.ToString())
                {
                    return HelperType.storage;
                }
            }

            foreach (var item in clist)
            {
                if (item.client.ToString() == client.ToString())
                {
                    return HelperType.converter;
                }
            }

            foreach (var item in olist)
            {
                if (item.client.ToString() == client.ToString())
                {
                    return HelperType.output;
                }
            }

            return HelperType.neutral;
        }
        public static T GetHelper<T>(PZClient client)
        {
            object ret = null;
            if (typeof(T) == typeof(PZStorage))
            {
                foreach (var item in slist)
                {
                    if (item.client.ToString() == client.ToString())
                    {
                        ret = item;
                    }
                }
            }

            if (typeof(T) == typeof(PZOutput))
            {
                foreach (var item in olist)
                {
                    if (item.client.ToString() == client.ToString())
                    {
                        ret = item;
                    }
                }
            }

            if (typeof(T) == typeof(PZConverter))
            {
                foreach (var item in clist)
                {
                    if (item.client.ToString() == client.ToString())
                    {
                        ret = item;
                    }
                }
            }

            return (T)ret;
        }

        public static void FinCycle(PZClient client)
        {
            if (false)
            {
#pragma warning disable CS0162 // Unreachable code detected
                switch (HelperTypeof(client))
                {
                    case HelperType.neutral:
                        return;

                    case HelperType.output:
                        HandleOutput(GetHelper<PZOutput>(client));
                        break;
                    case HelperType.converter:
                        HandleConverter(GetHelper<PZConverter>(client));
                        break;
                    case HelperType.storage:
                        HandleStorage(GetHelper<PZStorage>(client));
                        break;
                }
#pragma warning restore CS0162 // Unreachable code detected

            }
            else
            {
                return;
            }
        }
        private static void GiveItemRetry(PZClient giver, PZClient reciever, PZItem item, int start, int no)
        {
            int na = no+1;

            bool[] sel = PZClient.GetToSel(giver.client, item);
            RequestTradePacket req = (RequestTradePacket)Packet.Create(PacketType.REQUESTTRADE);
            req.Name = reciever.ToString();
            giver.client.SendToServer(req);

                req.Name = giver.ToString();
                reciever.client.SendToServer(req);

                ChangeTradePacket cht = (ChangeTradePacket)Packet.Create(PacketType.CHANGETRADE);
                cht.Offers = sel;
                giver.client.SendToServer(cht);

                AcceptTradePacket atpkt = (AcceptTradePacket)Packet.Create(PacketType.ACCEPTTRADE);
                atpkt.MyOffers = sel;
                atpkt.YourOffers = new bool[12];
                giver.client.SendToServer(atpkt);

                    atpkt.MyOffers = new bool[12];
                    atpkt.YourOffers = sel;
                    reciever.client.SendToServer(atpkt);
                    Console.WriteLine(giver.ToString() + " gave " + item + " to " + reciever);
                    PluginUtils.Delay(2000, new Action(() =>
                    {
                        int now = reciever.CountItem(item.ID);
                        int addition = now - start;
                        if (addition < item.amount)
                        {
                            Console.WriteLine("[ERROR]" + (item.amount - addition) + " " + item.ActualName() + " failed to be recieved so trying again");
                            if (na <= 7)
                            {
                                PluginUtils.Delay(500, new Action(() => GiveItemRetry(giver, reciever, item,start,na)));
                            }
                            else
                            {
                                Console.WriteLine("Tried over 7 times, give up!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Recieve success!");
                            PluginUtils.Delay(500, new Action(() =>
                            {
                                giver.enabled = true;
                                reciever.enabled = true;
                                reciever.spamenabled = true;
                                giver.spamenabled = true;
                            }));
                        }
                    }));
            na++;
        }
        public static void GiveItem(PZClient giver, PZClient reciever, PZItem item)
        {
            //int tries = 0;
            giver.enabled = false;
            reciever.enabled = false;
            reciever.spamenabled = false;
            giver.spamenabled = false;
            int start = reciever.CountItem(item.ID);

            GiveItemRetry(giver, reciever, item, start, 0);
        }

        public static void HandleOutput(PZOutput c)
        {
            bool found = false;
            foreach (var converter in clist)
            {
                if (converter.input.ID == c.outputitem && !converter.doing && c.client.CountItem(converter.input.ID) >= converter.input.amount)
                {
                    found = true;
                    PZItem item = new PZItem(c.outputitem, converter.input.amount);
                    GiveItem(c.client, converter.client, item);
                    converter.Input(item);
                }
            }

            if (!found)
            {
                foreach (var storage in slist)
                {
                    if (storage.inputitem == c.outputitem)
                    {
                        found = true;
                        PZItem item = new PZItem(c.outputitem, 1);
                        GiveItem(c.client, storage.client, item);
                        //c.doing = false;
                    }
                }
            }
        }
        public static void HandleStorage(PZStorage c)
        {
        }
        public static void HandleConverter(PZConverter c)
        {
            bool found = false;
            foreach (var converter in clist)
            {
                if (converter.input.ID == c.output.ID && !converter.doing && c.client.CountItem(converter.input.ID) >= converter.input.amount)
                {
                    found = true;
                    PZItem item = c.output;
                    GiveItem(c.client, converter.client, item);
                    c.doing = false;
                    converter.Input(item);
                }
            }

            if (!found)
            {
                foreach (var storage in slist)
                {
                    if (storage.inputitem == c.output.ID && c.client.CountItem(storage.inputitem) >= c.output.amount)
                    {
                        found = true;
                        PZItem item = c.output;
                        GiveItem(c.client, storage.client, item);
                        c.doing = false;
                    }
                }
            }

            c.doing = false;
        }
    }
}
