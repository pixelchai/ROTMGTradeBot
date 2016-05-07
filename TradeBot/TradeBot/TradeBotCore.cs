/*
Copyright 2016, Abhinav Bhandari (AKA: PixelZerg) -- Offered under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE VERSION 3
THE PROGRAM IS DISTRIBUTED IN THE HOPE THAT IT WILL BE USEFUL, BUT WITHOUT ANY WARRANTY.
IT IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.
IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW THE AUTHOR WILL BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF THE AUTHOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
*/
using Lib_K_Relay;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using Lib_K_Relay.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace PZTradeBot
{
    public class TradeBotCore : Lib_K_Relay.Interface.IPlugin
    {
        #region iplugin
        public string GetAuthor()
        {
            return "PixelZerg";
        }
        public string GetName()
        {
            return "TradeBot";
        }
        public string GetDescription()
        {
            return
@"┏━━━┳━━━━┳━━━━┓╋╋╋╋╋╋┏┓╋╋┏━━┓╋╋╋╋┏┓
┃┏━┓┣━━┓━┃┏┓┏┓┃╋╋╋╋╋╋┃┃╋╋┃┏┓┃╋╋╋┏┛┗┓
┃┗━┛┃╋┏┛┏┻┛┃┃┗╋━┳━━┳━┛┣━━┫┗┛┗┳━━╋┓┏┛
┃┏━━┛┏┛┏┛╋╋┃┃╋┃┏┫┏┓┃┏┓┃┃━┫┏━┓┃┏┓┃┃┃
┃┃╋╋┏┛━┗━┓╋┃┃╋┃┃┃┏┓┃┗┛┃┃━┫┗━┛┃┗┛┃┃┗┓
┗┛╋╋┗━━━━┛╋┗┛╋┗┛┗┛┗┻━━┻━━┻━━━┻━━┛┗━┛
";
        }
        public string[] GetCommands()
        {
            return new string[0];
        }
        #endregion
        public static List<Regex> blacklist = new List<Regex>();
        public static List<PZClient> clientlist = new List<PZClient>();
        public int tickno = 0;

        public void Initialize(Proxy p)
        {
            try
            {
                Console.WriteLine("Loading PZTradeBot build " + File.ReadAllText("buildver.txt"));
            }
            catch { }
            CompileRegex();
            p.HookPacket<TradeRequestedPacket>(TR);
            p.HookPacket<TradeChangedPacket>(TC);
            p.HookPacket<TradeAcceptedPacket>(TA);
            p.HookPacket<TradeStartPacket>(TS);
            p.HookPacket<TradeDonePacket>(TD);
            p.HookPacket < CancelTradePacket > (CT);
            p.HookPacket<NewTickPacket>(NT);
            p.HookCommand("scamchk", CHKCOM);
            p.HookCommand("tb", Com);
            p.HookCommand("give", givecom);
            p.ClientConnected += ClientConnected;
            p.ClientDisconnected += ClientDisconnected;
        }

        private void ClientDisconnected(Client client)
        {
            clientlist.RemoveAt(IndexOfClient(client));
        }
        private void ClientConnected(Client client)
        {
            clientlist.Add(new PZClient(client));
        }
        public static int IndexOfClient(Client c)
        {
            for (int i = 0; i < clientlist.Count; i++)
                if (clientlist[i].client.PlayerData.Name == c.PlayerData.Name)
                    return i;
            return -1;
        }
        public static PZClient GetClientByName(string name)
        {
            foreach (var client in clientlist)
            {
                if (client.client.PlayerData.Name == name)
                {
                    return client;
                }
            }
            return null;
        }
        public void NT(Client client, Packet packet)
        {
            try
            {
                clientlist[IndexOfClient(client)].Msg();
            }
            catch
            {
                Console.WriteLine("An error happened so offing spam");
                try
                {
                    clientlist[IndexOfClient(client)].spamenabled = false;
                }
                catch { }
            }

            if (HelperHandler.enabled)
            {
                if (client.PlayerData.Name == clientlist[0].client.PlayerData.Name)
                {
                    //cancel out duplicates
                    if (tickno % 6 == 0)
                    {
                        try
                        {
                            foreach (var o in HelperHandler.olist) o.Do();
                            foreach (var c in HelperHandler.clist) c.Do();
                            //foreach (var s in HelperHandler.slist) s.Do();
                        }
                        catch { Console.WriteLine("Doing error!"); }
                    }
                }
            }

            if (client.PlayerData.Name == clientlist[0].client.PlayerData.Name)
            {
                if (tickno % 30 == 0)
                {
                    try
                    {
                        foreach (var item in new DirectoryInfo(SnapShotHandler.DataPath).GetFiles())
                        {
                            if (item.Name.StartsWith("AutosaveSnapshot-" + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-"))
                            {
                                item.Delete();
                            }
                        }
                    }
                    catch { }
                    SnapShotHandler.Save("AutosaveSnapshot-");
                }

                tickno++;
            }
        }
        public void givecom(Client client, string command, string[] args)
        {
            new GiveDialog(clientlist[IndexOfClient(client)]).ShowDialog();
        }
        public void Com(Client client, string command, string[] args)
        {
            PZClient cl = clientlist[IndexOfClient(client)];
            if (args.Length == 0)
            {
                PluginUtils.ShowGUI(new TradeBotSettings(clientlist[IndexOfClient(client)]));
            }
            try
            {
                if (args[0] == "stopspam"|| args[0]=="stop")
                {
                    cl.spamenabled = false;
                    if (args[0] == "stop")
                    {
                        cl.enabled = false;
                    }
                }
                if (args[0] == "startspam" || args[0] == "start")
                {
                    cl.spamenabled = !false;
                    if (args[0] == "start")
                    {
                        cl.enabled = !false;
                    }
                }
            }
            catch { }
        }
        public void CHKCOM(Client client, string command, string[] args)
        {
            clientlist[IndexOfClient(client)].CheckNext();
        }

        public void TD(Client client, Packet packet)
        {
            TradeDonePacket p = (TradeDonePacket)packet;
            PZClient cl = clientlist[IndexOfClient(client)];
            cl.InTrade = false;
            if (cl.enabled)
            {
                if (p.Message.ToLower().Contains("successful"))
                {
                    cl.index++;
                }
            }
        }
        public void CT(Client client, Packet packet)
        {
            PZClient cl = clientlist[IndexOfClient(client)];
            cl.InTrade = false;
        }
        public void TR(Client client, Packet packet)
        {
            TradeRequestedPacket p = (TradeRequestedPacket)packet;
            if (clientlist[IndexOfClient(client)].enabled)
            {
                Console.Write("Trade Req from \"" + p.Name + "\"");
                if (Settings.Default.UseRegex)
                {
                    foreach (Regex rgx in blacklist)
                    {
                        if (rgx.IsMatch(p.Name)) { Console.WriteLine(" ignored!"); return; }
                    }
                }
                else
                {
                    if (Settings.Default.Blacklist.Contains(p.Name))
                    {
                        Console.WriteLine(" ignored!");
                        return;
                    }
                }
                RequestTradePacket rt = (RequestTradePacket)Packet.Create(PacketType.REQUESTTRADE);
                rt.Name = p.Name;
                client.SendToServer(rt);
                Console.WriteLine("accepted!");
            }
        }
        public void TS(Client client, Packet packet)
        {
            TradeStartPacket ts = (TradeStartPacket)packet;

            PZClient cl = clientlist[IndexOfClient(client)];
            cl.InTrade = true;
            if (cl.enabled)
            {
                cl.customer = new PZCustomer(ts);
                cl.last = client.PlayerData.Slot;
                cl.CancelTimerStart();
                if (!cl.curTrade.CheckBuying(cl, ts.YourItems))
                {
                    PlayerTextPacket pt = (PlayerTextPacket)Packet.Create(PacketType.PLAYERTEXT);
                    try
                    {
                        pt.Text = @"/tell " + ts.YourName + " You don't have " + cl.curTrade.BuyingToString();
                        client.SendToServer(pt);
                    }
                    catch
                    {
                        Console.WriteLine("Please define trades for \"" + client.PlayerData.Name + "\" to do");
                    }

                    CancelTradePacket ct = (CancelTradePacket)Packet.Create(PacketType.CANCELTRADE);
                    client.SendToServer(ct);
                    cl.InTrade = false;
                }
                else
                {
                    Console.WriteLine("Found correct stuff!");
                    cl.InTrade = true;
                    cl.SelectSelling();
                }
            }
        }
        public void TA(Client client, Packet packet)
        {
            TradeAcceptedPacket ta = (TradeAcceptedPacket)packet;
        }
        public void TC(Client client, Packet packet)
        {
            TradeChangedPacket p = (TradeChangedPacket)packet;
            PZClient cl = clientlist[IndexOfClient(client)];
            cl.customer.UpdateItems(p.Offers);
            if (cl.enabled)
            {
                Console.WriteLine("--------------------------");
                int totlefts = 0;
                string msg = "";
                foreach (var item in cl.CheckCustomer(true))
                {
                    Console.WriteLine(item.ToString() + " - " + item.amount);
                    totlefts += item.amount;
                    if (item.amount > 0)
                    {
                        msg += item.amount + " " + item.ToString() + ";";
                    }
                }
                Console.WriteLine("Verdict: " + (totlefts <= 0));
                if (totlefts <= 0)
                {
                    cl.SelectSelling();
                    AcceptTradePacket ac = (AcceptTradePacket)Packet.Create(PacketType.ACCEPTTRADE);
                    ac.YourOffers = p.Offers;
                    ac.MyOffers = cl.GetToSel();
                    cl.client.SendToServer(ac);
                }
                else
                {
                    PlayerTextPacket ptpkt = (PlayerTextPacket)Packet.Create(PacketType.PLAYERTEXT);
                    ptpkt.Text = "/t " + cl.customer.name + " An extra: " + msg + " plz!";
                    cl.client.SendToServer(ptpkt);
                }
            }
        }
        public static void CompileRegex()
        {
            if (Settings.Default.UseRegex)
            {
                blacklist.Clear();
                foreach (var rgx in Settings.Default.Blacklist)
                {
                    blacklist.Add(new Regex(rgx));
                }
            }
        }
    }
}
