/*
Copyright 2016, Abhinav Bhandari (AKA: PixelZerg) -- Offered under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE VERSION 3
THE PROGRAM IS DISTRIBUTED IN THE HOPE THAT IT WILL BE USEFUL, BUT WITHOUT ANY WARRANTY.
IT IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.
IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW THE AUTHOR WILL BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF THE AUTHOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PZTradeBot
{
    public partial class LoadDialog : Form
    {
        private static Regex rgxsect = new Regex("\\w+{([^{}]|)+}");
        private static Regex rgxitem = new Regex("(\u00E4I([^\u00AF])+?\u00E9I)");
        private static Regex rgxtrade = new Regex("\u00E4T([^T]*)\u00E9T");
        private static Regex rgxfname = new Regex("(\u00E4N(.|\\s|)+?\u00E9N)");
        private static Regex rgxfsell = new Regex("(\u00E4T([^\u00AF])+?\u00E4I)");
        private static Regex rgxfbuy = new Regex("\u00AF.+\u00AF");
        private static Regex rgxincnm = new Regex("(t|f)(\\s|)\u00E9T");

        private static Regex rgxafter = new Regex("(\u00E4I([^\u00AF])+\u00E4N)");
        public static Regex rgxbefore = new Regex("(\u00E4I(.|\\s)+?\u00AF)");

        public string rawdata = string.Empty;

        private string clientname = "";

        private MatchCollection sects = null;
        private List<PZTrade> ltrades = new List<PZTrade>();

        private bool formclosing = false;
        public LoadDialog(string raw, string cname)
        {
            clientname = cname;
            InitializeComponent();
            rawdata = raw;
            sects = rgxsect.Matches(raw);
            foreach (Match item in sects)
            {
                listBox1.Items.Add(item.Value.Substring(0, item.Value.IndexOf('{')));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex == -1) return;
            string txt = sects[listBox1.SelectedIndex].Value;
            MatchCollection trades = rgxtrade.Matches(txt);
            foreach (Match tr in trades)
            {
                PZTrade trade = new PZTrade();

                string fsellr = rgxfsell.Match(tr.Value).Value;
                string fsell = fsellr.Substring(2, fsellr.Length - 4);
                trade.fselling = fsell;

                string before = rgxbefore.Match(tr.Value).Value;
                MatchCollection items = rgxitem.Matches(before);
                foreach (Match itemfdgsasdasdash in items)
                {
                    string item = itemfdgsasdasdash.Value.Substring(2, itemfdgsasdasdash.Value.Length - 4);
                    string[] s = item.Split('º');
                    PZItem pzitem = new PZItem(Int32.Parse(s[2]), Int32.Parse(s[0]), s[1]);
                    trade.selling.Add(pzitem);
                }

                string fbuyr = rgxfbuy.Match(tr.Value).Value;
                string fbuy = fbuyr.Substring(1, fbuyr.Length - 2);
                trade.fbuying = fbuy;

                 string after = rgxafter.Match(tr.Value).Value;
                 items = rgxitem.Matches(after);
                foreach (Match itemfdgsasdasdash in items)
                {
                    string item = itemfdgsasdasdash.Value.Substring(2, itemfdgsasdasdash.Value.Length - 4);
                    string[] s = item.Split('º');
                    PZItem pzitem = new PZItem(Int32.Parse(s[2]), Int32.Parse(s[0]), s[1]);
                    trade.buying.Add(pzitem);
                }

                string inc = rgxincnm.Match(tr.Value).Value;
                if (inc.StartsWith("f"))
                {
                    trade.includename = false;
                }
                else
                {
                    trade.includename = true;
                }

                string fnamer = rgxfname.Match(tr.Value).Value;
                string fname = fnamer.Substring(2, fnamer.Length - 4);
                trade.fname = fname;

                trade.name = clientname;

                Console.WriteLine(trade.ToString());
                ltrades.Add(trade);
            }
            this.Close();
        }

        private void LoadDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            formclosing = true;
        }
        public List<PZTrade> Get()
        {
            while (!formclosing)
            {
            }
            return ltrades;
        }
    }
}
