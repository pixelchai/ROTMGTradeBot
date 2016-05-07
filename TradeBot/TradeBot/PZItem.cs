/*
Copyright 2016, Abhinav Bhandari (AKA: PixelZerg) -- Offered under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE VERSION 3
THE PROGRAM IS DISTRIBUTED IN THE HOPE THAT IT WILL BE USEFUL, BUT WITHOUT ANY WARRANTY.
IT IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.
IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW THE AUTHOR WILL BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF THE AUTHOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
*/
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PZTradeBot
{
    public class PZItem
    {
        public int ID = -1;
        public int amount = 0;
        public string nick = string.Empty;
        public override string ToString()
        {
            if (nick == string.Empty)
            {
                return ActualName();
            }
            else
            {
                return nick;
            }
        }
        public static string GetNameFromID(int id)
        {
            try
            {
                return Serializer.Items.Keys.ToList()[Serializer.Items.Values.ToList().IndexOf((ushort)id)];
            }
            catch
            {
                return "nothing";
            }
        }
        public string ActualName()
        {
            return GetNameFromID(ID);
        }
        public bool selected = false;
        public PZItem() { }
        public PZItem(int _id, int _amount)
        {
            amount = _amount;
            ID = _id;
        }
        public PZItem(int _id, int _amount, string _nick)
        {
            amount = _amount;
            ID = _id;
            nick = _nick;
        }

        public bool Check(Item[] items)
        {
            int no = 0;
            foreach (var item in items)
            {
                if (item.ItemItem == ID) no++;
            }
            return no >= amount;
        }

        public string FullName()
        {
            return "{\"" + this.nick + "\"" + " (ID = " + ID + ") -- "+ActualName()+" } x" + amount;
        }
    }
}
