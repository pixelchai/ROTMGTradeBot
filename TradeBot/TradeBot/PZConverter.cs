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
   public class PZConverter
    {
        public bool doing = false;
        public PZClient client = null;
        public PZItem input = new PZItem();
        public PZItem output = new PZItem();
        private PZTrade t = new PZTrade();
        public PZConverter(PZClient c, PZItem i, PZItem o)
        {
            client = c;
            input = i;
            output = o;
        }
        public void Input(PZItem input)
        {
            t = new PZTrade();
            t.selling.Add(input);
            t.buying.Add(output);
            t.name = client.ToString();

            client.tradequeue = new PZTrade[]{t}.ToList();
            client.index = 0;
            client.enabled = true;
            client.spamenabled = true;

            doing = true;
        }
        public override string ToString()
        {
            return client.ToString() + " -- input " + input.FullName()+" --> "+output.FullName();
        }

        public void Do()
        {
           // Console.WriteLine("Converter: \"" + client.ToString() + "\" doing...");
            int myinno = client.CountItem(input.ID);
            if (myinno >= input.amount)
            {
                Console.WriteLine("(converter)" + client + " conversion process started");
                //we've got enough input!
                #region SetTrade
                client.tradequeue.Clear();
                client.tradequeue.Add(new PZTrade()
                {
                    selling = new PZItem[] { input }.ToList(),
                    buying = new PZItem[] { output }.ToList(),
                    name = client.ToString()
                });
                client.enabled = true;
                client.spamenabled = true;
                client.index = 0; 
                #endregion
            }

            bool found = false;
            foreach (var converter in HelperHandler.clist)
            {
                if (converter.input.ID == this.output.ID)
                {
                    found = true;
                    int give = (CalcGive(converter, client.CountItem(converter.input.ID)));
                    if (give > 0)
                    {
                        PZItem giveitem = new PZItem(converter.input.ID, give);
                        Console.WriteLine("(converter)" + client.ToString() + " give " + giveitem + " to (converter)" + converter.client);
                        HelperHandler.GiveItem(client, converter.client, giveitem);
                    }

                    continue; //since I've given my max
                }
            }

            if (!found)
            {
                #region Store
                int myoutno = client.CountItem(output.ID);
                if (myoutno >= output.amount)
                {
                    foreach (var storage in HelperHandler.slist)
                    {
                        if (storage.inputitem == output.ID)
                        {
                            int give = SCalcGive(storage, client.CountItem(storage.inputitem));
                            if (give > 0)
                            {
                                PZItem giveitem = new PZItem(storage.inputitem, give);
                                Console.WriteLine("(converter)" + client + " give " + giveitem + " to (storage)" + storage.client);
                                //give max amount I can give
                                HelperHandler.GiveItem(client, storage.client, giveitem);
                            }
                            continue;
                        }
                    }
                }
                #endregion
            }
        }

        public int SCalcMaxGive(PZStorage s)
        {
            int no = s.client.CountItem(-1); //free spaces
            if (no < output.amount)
            {
                return output.amount;
            }
            else
            {
                return no; //do not have enough space so just fill up their inv
            }
        }
        public int SCalcGive(PZStorage s, int myamount)
        {
            int max = SCalcMaxGive(s);
            if (myamount > max)
            {
                return max;
            }
            else
            {
                return myamount;
            }
        }

        public int CalcMaxGive(PZConverter c)
        {
            int no = c.client.CountItem(-1); //free spaces
            if (no < c.input.amount)
            {
                return c.input.amount;
            }
            else
            {
                return no;
            }
        }
        public int CalcGive(PZConverter c, int myamount)
        {
            int max = CalcMaxGive(c);
            if (myamount > max)
            {
                return max;
            }
            else
            {
                return myamount;
            }
        }

    }
}
