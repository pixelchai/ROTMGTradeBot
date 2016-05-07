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
    public class PZClient
    {
        public bool spamenabled = false;
        public bool enabled = false;
        private bool intrade;
        public bool InTrade
        {
            get { return intrade; }
            set
            {
                intrade = value;
                Console.Write(this.client.PlayerData.Name + " ");
                if (intrade)
                {
                    Console.WriteLine("is now in a trade");
                }
                else
                {
                    Console.WriteLine("has now exited the trade");
                }
            }
        }
        public PZCustomer customer = null;
        public Lib_K_Relay.Networking.Client client = null;
        public List<PZTrade> tradequeue = new List<PZTrade>();
        public int[] last = null;
        private int _index = 0;
        public int index
        {
            get { return _index; }
            set
            {
                if (enabled)
                {
                    if (value >= tradequeue.Count)
                    {
                        HelperHandler.FinCycle(this);
                        _index = 0;
                        Console.WriteLine(client.PlayerData.Name + " reset cycle to start");
                    }
                    else
                    {
                        _index = value;
                    }
                    Console.WriteLine(client.PlayerData.Name + " moved to action " + index);
                }
            }
        }
        public PZTrade curTrade
        { get { return tradequeue[index]; } set { tradequeue[index] = value; } }

        private int msgindex = 0;

        public PZClient(Lib_K_Relay.Networking.Client c)
        {
            client = c;
            try
            {
                last = c.PlayerData.Slot;
            }
            catch { }
        }
        public PZClient(Lib_K_Relay.Networking.Client c, List<PZTrade> queue)
        {
            tradequeue = queue;
            client = c;
            try
            {
                last = c.PlayerData.Slot;
            }
            catch { }
        }

        public int CountItem(int itemid)
        {
            int ret = 0;
            for (int no = 4;no<client.PlayerData.Slot.Length;no++)
            {
                if (client.PlayerData.Slot[no] == itemid)
                {
                    ret++;
                }
            }
            return ret;
        }

        public void SelectSellingSlow()
        {
            //bool[] selling = GetToSel();

            //for(int i = 0; i < selling.Length; i++)
            //{
            //    if (selling[i] == true)
            //    {
            //        //gen bool array as it is
            //        bool[] blarray = new bool[selling.Length];
            //        for (int ii = 0; ii !=i; ii++)
            //        {
            //            blarray[ii] = selling[ii];
            //        }
            //        //send that w/delay
            //        PluginUtils.Delay(1000, new Action(()=>SendSel(blarray)));
            //    }
            //}
        }
        public void SelectSelling()
        {
            SendSel(GetToSel());
        }
        public static bool[] GetToSel(Client client, PZItem item)
        {
            List<int> indexestosel = new List<int>();

            {
                //search for that item
                int lefttofind = item.amount;
                for (int i = 4; i < client.PlayerData.Slot.Length; i++)
                {
                    if (client.PlayerData.Slot[i] == item.ID && lefttofind > 0)
                    {
                        lefttofind--;
                        indexestosel.Add(i);
                    }
                }
                if (lefttofind > 0)
                {
                    Console.WriteLine("You are missing " + lefttofind + " " + item.ToString());
                }
            }

            //conv to bool format
            bool[] ret = new bool[12];
            for (int i = 4; i != 12; i++)
            {
                if (indexestosel.Contains(i))
                {
                    ret[i] = true;
                }
                else
                {
                    ret[i] = false;
                }
            }
            return ret;
        }
        public bool[] GetToSel()
        {
            List<int> indexestosel = new List<int>();

            foreach (var item in curTrade.selling)
            {
                //search for that item
                int lefttofind = item.amount;
                for (int i = 4; i < client.PlayerData.Slot.Length; i++)
                {
                    if (client.PlayerData.Slot[i] == item.ID && lefttofind > 0)
                    {
                        lefttofind--;
                        indexestosel.Add(i);
                    }
                }
                if (lefttofind > 0)
                {
                    Console.WriteLine("You are missing " + lefttofind + " " + item.ToString());
                    spamenabled = false;
                }
            }

            //conv to bool format
            bool[] ret = new bool[12];
            for (int i = 4; i != 12; i++)
            {
                if (indexestosel.Contains(i))
                {
                    ret[i] = true;
                }
                else
                {
                    ret[i] = false;
                }
            }
            return ret;
        }
        public void SendSel(bool[] sels)
        {
            ChangeTradePacket ch = (ChangeTradePacket)Packet.Create(PacketType.CHANGETRADE);
            ch.Offers = sels;
            client.SendToServer(ch);
        }

        public void CancelTimerStart()
        {
            PluginUtils.Delay(30000, new Action(() => { if (this.InTrade) { client.SendToServer(Packet.Create(PacketType.CANCELTRADE)); InTrade = false; } }));
        }
        //public List<PZItem> CheckCustomer(bool seltoo = true)
        //{
        //    List<PZItem> lefts = new List<PZItem>();
        //    foreach (var item in this.curTrade.buying)
        //    {
        //        int left = item.amount;
        //        foreach (var itemm in this.customer.items)
        //        {
        //            if (itemm.ID == item.ID)
        //            {
        //                if ((seltoo) ? item.selected : true)
        //                {
        //                    left -= itemm.amount;
        //                }
        //            }
        //        }
        //        lefts.Add(new PZItem(item.ID, left));
        //    }
        //    return lefts;
        //}
        public List<PZItem> CheckCustomer(bool seltoo = true)
        {
            int left = 0;
            List<PZItem> lefts = new List<PZItem>();
            foreach (var item in this.curTrade.buying)
            {
                int l = customer.Check(item);
                left += l;
                lefts.Add(new PZItem(item.ID, l));
            }
            //Console.WriteLine("Overally, we have " + left + " no of items left");
            return lefts;
        }
        public void ChGoNext()
        {

        }
        public static int CountItem(int itemid, int[] items)
        {
            int ret = 0;
            foreach (var item in items)
            {
                if (item == itemid)
                {
                    ret++;
                }
            }
            return ret;
        }
        public static int CountItem(int itemid, List<PZItem> items)
        {
            int ret = 0;
            foreach (var item in items)
            {
                if (item.ID == itemid)
                {
                    ret++;
                }
            }
            return ret;
        }
        public void CheckNext()
        {//TODO
            int totscam = 0;
            foreach (var item in curTrade.buying)
            {
                int prev = CountItem(item.ID, last);
                int cur = CountItem(item.ID, client.PlayerData.Slot);
                int incr = cur - prev;
                Console.WriteLine(incr + " increase of " + item + " -- " + prev + " --> " + cur);
                int lost = item.amount - incr;
                Console.WriteLine("lost = " + lost);
            }
        }
        public void Msg()
        {
            if (enabled && spamenabled)
            {
                if (msgindex % 3 == 0)
                {
                    PlayerTextPacket pt = (PlayerTextPacket)Packet.Create(PacketType.PLAYERTEXT);
                    pt.Text = this.curTrade.GetMessage();
                    if (msgindex % 6 == 0)
                    {
                        pt.Text += " ";
                    }
                    client.SendToServer(pt);

                }
            }
            msgindex++;
        }
        public void UpdtItems(bool[] l)
        {
        }
        public override string ToString()
        {
            return client.PlayerData.Name;
        }
    }
}
