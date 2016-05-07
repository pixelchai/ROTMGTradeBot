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
    public class PZOutput
    {

        public PZOutput(PZClient cl, int outitem)
        {
            client = cl;
            outputitem = outitem;
        }

        public PZClient client = null;
        public int outputitem = 0;

        public override string ToString()
        {
            return client.ToString() + " -- outputing " + PZItem.GetNameFromID(outputitem);
        }

        public void Do()
        {
            //Console.WriteLine("Output: \"" + client.ToString() + "\" doing...");
            bool found = false;
            foreach (var converter in HelperHandler.clist)
            {
                if (converter.input.ID == outputitem)
                {
                    found = true;
                    int give = (CalcGive(converter, client.CountItem(converter.input.ID)))-1;
                    if (give > 0)
                    {
                        //Console.WriteLine("\t Converter: \"" + converter.client.ToString() + "\" is succeptible!");
                        //give the maximum amount I can give:
                        PZItem giveitem = new PZItem(converter.input.ID, give);
                        Console.WriteLine("(output)" + client + " give " + giveitem + " to (converter)" + converter.client);
                        HelperHandler.GiveItem(client, converter.client,giveitem);
                    }

                    continue; //since I've given my max
                }
            }

            if (!found)
            {
               // Console.WriteLine("Did not find any succeptible converters, jumping to storage");
                //straight to store
                foreach (var storage in HelperHandler.slist)
                {
                    if (storage.inputitem == outputitem)
                    {
                        int give = (SCalcGive(storage, client.CountItem(storage.inputitem)))-1;
                        if (give > 0)
                        {
                            PZItem giveitem = new PZItem(storage.inputitem, give);
                            Console.WriteLine("(output)" + client + " give " + giveitem + " to (storage)" + storage.client);
                            //Console.WriteLine("\t Storage: \"" + storage.client.ToString() + "\" is succeptible!");
                            HelperHandler.GiveItem(client, storage.client, giveitem);
                        }
                        continue;
                    }
                }
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

        public int SCalcMaxGive(PZStorage s)
        {
            int no = s.client.CountItem(-1); //free spaces
            if (no < 1)
            {
                return 1;
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
    }
}
