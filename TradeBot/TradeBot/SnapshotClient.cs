/*
Copyright 2016, Abhinav Bhandari (AKA: PixelZerg) -- Offered under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE VERSION 3
THE PROGRAM IS DISTRIBUTED IN THE HOPE THAT IT WILL BE USEFUL, BUT WITHOUT ANY WARRANTY.
IT IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.
IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW THE AUTHOR WILL BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF THE AUTHOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
*/
using Lib_K_Relay.Networking.Packets.DataObjects;
using System.Collections.Generic;

namespace PZTradeBot
{
    public class SnapshotClient
    {
        public bool spamenabled = false;
        public bool enabled = false;
        public List<PZTrade> tradequeue = new List<PZTrade>();
        public int[] last = null;
        public int index = 0;

        public string clientname = "";
        public Location clientloc = null;

        public SnapshotClient() { }
        public SnapshotClient(PZClient cl)
        { 
            this.spamenabled = cl.spamenabled;
            this.enabled = cl.enabled;
            this.tradequeue = cl.tradequeue;
            this.last = cl.last;
            this.index = cl.index;

            this.clientname = cl.client.PlayerData.Name;
            this.clientloc = cl.client.PlayerData.Pos;
        }
        public static List<SnapshotClient> ConvertList(List<PZClient> l)
        {
            List<SnapshotClient> ret = new List<SnapshotClient>();
            foreach (var client in l)
            {
                ret.Add(new SnapshotClient(client));
            }
            return ret;
        }
        public static List<PZClient> ConvertList(List<SnapshotClient> l)
        {
            List<PZClient> ret = new List<PZClient>();
            foreach (var client in l)
            {
                ret.Add(client.ToClient());
            }
            return ret;
        }
        public PZClient ToClient()
        {
            PZClient cl = null;
            for (int i = TradeBotCore.clientlist.Count - 1; i >= 0; i--)
            {
                if (TradeBotCore.clientlist[i].client.PlayerData.Name == this.clientname)
                {
                    cl = TradeBotCore.clientlist[i];
                   // TradeBotCore.clientlist.RemoveAt(i);
                }
            }
            if (cl == null) return null;

            cl.enabled = this.enabled;
            cl.tradequeue = this.tradequeue;
            cl.last = this.last;
            cl.index = this.index;
            cl.spamenabled = this.spamenabled;

            //KRelayMove.Core.clientlist[KRelayMove.Core.IndexOfClient(cl.client)].StartMove(this.clientloc);

            return cl;
        }
    }
}
