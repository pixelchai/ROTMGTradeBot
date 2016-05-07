/*
Copyright 2016, Abhinav Bhandari (AKA: PixelZerg) -- Offered under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE VERSION 3
THE PROGRAM IS DISTRIBUTED IN THE HOPE THAT IT WILL BE USEFUL, BUT WITHOUT ANY WARRANTY.
IT IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.
IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW THE AUTHOR WILL BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF THE AUTHOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
*/
using System;
using System.IO;

namespace PZTradeBot
{
    public class SnapShotHandler
    {
        private static string _path = "";
        public static string DataPath
        {
            get
            {
                DirectoryInfo d = new DirectoryInfo("TradeBotData");
                if (!d.Exists)
                {
                    if (d.Parent.Name.ToString() == "Plugins")
                    {
                        d.Create();
                        return d.FullName;
                    }
                }
                else
                {
                    if (d.Parent.Name.ToString() == "Plugins")
                    {
                        return d.FullName;
                    }
                    else
                    {
                        Console.WriteLine("Data path Fallback!!");
                        return @"C:\Users\PixelZerg\Documents\ROTMG\K-Relay\Plugins\TradeBotData";
                    }
                }

                Console.WriteLine("Data path Fallback!!");
                return @"C:\Users\PixelZerg\Documents\ROTMG\K-Relay\Plugins\TradeBotData";
            }
        }
        public static void Save(string name = "Snapshot-")
        {
           // SaveDialog.CrFolder("TradeBotData");
            try
            {
                SnapShot s = new SnapShot();
                //name = "Snapshot-";

               // name += s.GetHashCode()+"-";
                name += DateTime.Today.Year+"-";
                name += DateTime.Today.Month + "-";
                name += DateTime.Today.Day + "-";
                name+=System.Environment.TickCount+"-"+s.GetHashCode();
                using (StreamWriter sw = new StreamWriter(DataPath+"\\"+name+".pztbs"))
                {
                    //sw.WriteLine("moo");
                    sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(s, Newtonsoft.Json.Formatting.Indented));
                }
                //File.WriteAllText("TradeBotData/" + name + ".pztbs", Newtonsoft.Json.JsonConvert.SerializeObject(new SnapShot()));
            }
            catch { }
        }

        public static SnapShot Load()
        {
            SnapshotLoadDialog d = new SnapshotLoadDialog();
            d.ShowDialog();
            return d.Get();
        }

        public static void LoadApply()
        {
            SnapshotLoadDialog d = new SnapshotLoadDialog();
            d.ShowDialog();
            LoadApply(d.Get());
        }
        public static void LoadApply(SnapShot s)
        {
            Console.WriteLine("Loading Snapshot...");
            Console.WriteLine("\t Load blacklist...");
            {
                TradeBotCore.blacklist = s.blacklist;
            }
            foreach (var item in TradeBotCore.blacklist)
            {
                Console.WriteLine("\t\t" + item);
            }
            // catch { TradeBotCore.blacklist.Clear(); }

            Console.WriteLine("\t Load clist...");
            HelperHandler.clist.Clear();
            foreach (var item in s.clist)
            {
                HelperHandler.clist.Add(item.ToConverter());
                Console.WriteLine("\t\t" + item.client.clientname);
            }

            Console.WriteLine("\t Load olist...");
            HelperHandler.olist.Clear();
            foreach (var item in s.olist)
            {
                HelperHandler.olist.Add(item.ToOutput());
                Console.WriteLine("\t\t" + item.client.clientname);

            }

            Console.WriteLine("\t Loaded slist...");
            HelperHandler.slist.Clear();
            foreach (var item in s.slist)
            {
                HelperHandler.slist.Add(item.ToStorage());
                Console.WriteLine("\t\t" + item.client.clientname);

            }

            Console.WriteLine("\t Load Helper Enabled state...");
            HelperHandler.enabled = s.helpersenabled;
            Console.WriteLine("\t\t" + HelperHandler.enabled);

            Console.WriteLine("\t Load clientlist...");
            TradeBotCore.clientlist = SnapshotClient.ConvertList(s.clientlist);
            foreach (var item in TradeBotCore.clientlist)
            {
                Console.WriteLine("\t\t" + item);
            }
            Console.WriteLine("Finished Loading!!");
            //new System.Threading.Thread(() => {
            //foreach (var client in s.clientlist)
            //{
            //    KRelayMove.Core.clientlist[KRelayMove.Core.IndexOfClient(client.ToClient().client)].StartMove(client.clientloc);
            //}
            //}).Start();
            MoveClients(s);
        }
        private static void MoveClients(SnapShot s)
        {
            foreach (var pzclient in s.clientlist)
            {
                KRelayMove.PZClient mclient = null;
                foreach (var client in KRelayMove.Core.clientlist)
                {
                    if (client.client.PlayerData.Name == pzclient.clientname)
                    {
                        mclient = client;
                        continue;
                    }
                }

                try
                {
                    mclient.care = 1;
                    mclient.StartMove(pzclient.clientloc);
                }
                catch { }
            }
        }
    }
}
