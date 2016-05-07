/*
Copyright 2016, Abhinav Bhandari (AKA: PixelZerg) -- Offered under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE VERSION 3
THE PROGRAM IS DISTRIBUTED IN THE HOPE THAT IT WILL BE USEFUL, BUT WITHOUT ANY WARRANTY.
IT IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.
IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW THE AUTHOR WILL BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF THE AUTHOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
*/
using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib_K_Relay;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using Lib_K_Relay.Networking.Packets;

namespace PZTradeBot
{
    public class PZTrade : PZAction
    {
        #region PZAction stuff
        public string GetActionText()
        {
            return GetMessage();
        }
        #endregion

        public List<PZItem> buying = new List<PZItem>();
        public List<PZItem> selling = new List<PZItem>();

        public bool includename = true;
        public string name = "";

        public bool CheckBuying(PZClient cl, Item[] yitems)
        {
            int no = 0;
            foreach (PZItem item in cl.curTrade.buying)
            {
                if (item.Check(yitems)) no++;
            }

            #region alts (depr)
            /*
                        if (no < cl.curTrade.buying.Count)
                        {
                            int altindex = 0;
                            foreach (var alt in cl.curTrade.alts)
                            {
                                int na = 0;
                                foreach (PZItem item in alt.buying)
                                {
                                    if (item.Check(yitems)) na++;
                                }

                                if (na >= alt.buying.Count)
                                {
                                    Console.WriteLine("Changing to alternative!");
                                    continue;
                                }
                                else if (altindex == cl.curTrade.alts.Count - 1)
                                {
                                    altindex = -1;
                                    continue;
                                }

                                altindex++;
                            }


                            if (altindex != -1)
                            {
                                PZTrade temp = cl.curTrade;
                                List<PZTrade> olalts = cl.curTrade.alts;
                                cl.curTrade = cl.curTrade.alts[altindex];
                                olalts.RemoveAt(altindex);
                                olalts.Add(temp);
                                cl.curTrade.alts = olalts;


                                Console.WriteLine("Switched from: " + temp.ToString());
                                Console.WriteLine("To alternative: " + cl.curTrade.ToString());
                                Console.WriteLine("------[Extra Details]----");
                                foreach (var item in cl.curTrade.alts)
                                {
                                    Console.WriteLine("Alt: " + item.ToString());
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                        //*/
            #endregion
            return (no >= cl.curTrade.buying.Count);
        }

        public string fbuying = Settings.Default.DefBfstr;
        public string fselling = Settings.Default.DefSfstr;
        public string fname = Settings.Default.DefNamefstr;

        public string GetMessage()
        {
            string ret = "";
            ret += fselling;
            ret += SellingToString() + " ";
            ret += fbuying;
            ret += BuyingToString();
            if (includename)
                ret += " " + fname + name;
            return ret;
        }

        public string BuyingToString()
        {
            string bstr = "";
            for (int i = 0; i < buying.Count(); i++)
            {
                bstr += buying[i].amount + " ";
                bstr += buying[i].ToString();
                if (i < buying.Count() - 1)
                {
                    bstr += "; ";
                }
            }
            return bstr;
        }
        public string SellingToString()
        {
            string sstr = "";
            for (int i = 0; i < selling.Count(); i++)
            {
                sstr += selling[i].amount + " ";
                sstr += selling[i].ToString();
                if (i < selling.Count() - 1)
                {
                    sstr += "; ";
                }
            }
            return sstr;
        }
        public override string ToString()
        {
            return GetMessage();
        }
        public string ToF()
        {
            char st = (char)228;
            char en = (char)233;
            char sep = (char)186;
            char p = (char)175;
            string ret = st+"T" + fselling;
            foreach (var item in selling)
            {
                ret += st+"I"+
                    item.amount +sep + item.ToString() + sep + item.ID+
                    en+"I";
            }
            ret += p+fbuying+p;
            foreach (var item in buying)
            {
                ret += st+"I"
                    + item.amount + sep + item.ToString() + sep + item.ID + 
                    en+"I";
            }
            ret += st+"N" + this.fname + en+"N";
            if (this.includename)
            {
                ret += "t";
            }
            else
            {
                ret += "f";
            }
            ret += en + "T";
            return ret;
        }

    }
}