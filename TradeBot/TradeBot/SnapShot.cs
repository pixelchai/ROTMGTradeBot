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

namespace PZTradeBot
{
    public class SnapShot
    {
        public List<SnapshotClient> clientlist = new List<SnapshotClient>();
        public List<System.Text.RegularExpressions.Regex> blacklist =new List<System.Text.RegularExpressions.Regex>();
        public List<SnapshotStorage> slist = new List<SnapshotStorage>();
        public  List<SnapshotOutput> olist=new List<SnapshotOutput>();
        public List<SnapshotConverter> clist=new List<SnapshotConverter>();

        public bool helpersenabled = false;

        public SnapShot()
        {
            blacklist = TradeBotCore.blacklist;

            foreach (var item in HelperHandler.slist)
            {
                slist.Add(new SnapshotStorage(item));
            }
            foreach (var item in HelperHandler.clist)
            {
                clist.Add(new SnapshotConverter(item));
            }
            foreach (var item in HelperHandler.olist)
            {
                olist.Add(new SnapshotOutput(item));
            }

            helpersenabled = HelperHandler.enabled;

            clientlist = SnapshotClient.ConvertList(TradeBotCore.clientlist);//must be last one to be converted!!
        }
    }
}
