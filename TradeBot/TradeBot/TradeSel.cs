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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PZTradeBot
{
    public partial class TradeSel : UserControl
    {
        public bool sitemmed = false;
        public bool bitemmed = false;
        public List<PZItem> selling = new List<PZItem>();
        public List<PZItem> buying = new List<PZItem>();

        public PZItem cursitem = new PZItem();
        public PZItem curbitem = new PZItem();

        public PZTrade trade = new PZTrade();
        public int sid = -1;
        public int bid = -1;
        public TradeSel()
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.DefSfstr;
            textBox2.Text = Settings.Default.DefBfstr;
            textBox3.Text = Settings.Default.DefNamefstr;
            Upd();
        }
        public TradeSel(PZTrade tradee,string name)
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.DefSfstr;
            textBox2.Text = Settings.Default.DefBfstr;
            textBox3.Text = Settings.Default.DefNamefstr;
            textBox4.Text = name;
            LoadTrade(tradee);
            Upd();
        }
        public TradeSel(string name)
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.DefSfstr;
            textBox2.Text = Settings.Default.DefBfstr;
            textBox3.Text = Settings.Default.DefNamefstr;
            textBox4.Text = name;
            Upd();

        }
        public void LoadTrade(PZTrade t)
        {
            selling = t.selling;
            buying = t.buying;
            textBox2.Text = t.fbuying;
            textBox1.Text = t.fselling;
            textBox3.Text = t.fname;
            textBox4.Text = t.name;
            checkBox1.Checked = t.includename;
            textBox5.Text = t.GetMessage();
        }
        public void Upd()
        {
            UpdtListboxes();
            trade = new PZTrade();
            trade.selling = selling;
            trade.buying = buying;
            trade.fbuying = textBox2.Text;
            trade.fselling = textBox1.Text;
            trade.fname = textBox3.Text;
            trade.includename = checkBox1.Checked;
            trade.name = textBox4.Text;
            textBox5.Text = trade.GetMessage();
        }
        public void UpdtListboxes()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var item in selling)
            {
                listBox2.Items.Add(item.FullName());
            }

            foreach (var item in buying)
            {
                listBox1.Items.Add(item.FullName());
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Upd();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Upd();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = (int)Math.Round(numericUpDown1.Value, 0, MidpointRounding.AwayFromZero);
            cursitem.amount = (int)numericUpDown1.Value;
            Upd();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            cursitem.nick = textBox6.Text;
            Upd();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = (int)Math.Round(numericUpDown2.Value, 0, MidpointRounding.AwayFromZero);
            curbitem.amount = (int)numericUpDown2.Value;
            Upd();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            curbitem.nick = textBox7.Text;
            Upd();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Upd();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sitemmed)
            {
                var temp = cursitem;
                selling.Add(cursitem);
                cursitem = new PZItem();
                cursitem.ID = temp.ID;
                cursitem.nick = temp.nick;
                cursitem.amount = temp.amount;
                Upd();
            }
            else
            {
                MessageBox.Show("Please select an item to sell using the \"...\" button");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bitemmed)
            {
                var temp = curbitem;
                buying.Add(curbitem);
                curbitem = new PZItem();
                curbitem.ID = temp.ID;
                curbitem.nick = temp.nick;
                curbitem.amount = temp.amount;
                Upd();
            }
            else
            {
                MessageBox.Show("Please select an item to buy using the \"...\" button");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Upd();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemSelDialog sd = new ItemSelDialog();
            sd.ShowDialog();
            cursitem.ID = sd.GetID();
            textBox6.Text = cursitem.ActualName();
            sitemmed = true;
            Upd();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ItemSelDialog sd = new ItemSelDialog();
            sd.ShowDialog();
            curbitem.ID = sd.GetID();
            textBox7.Text = curbitem.ActualName();
            bitemmed = true;
            Upd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                selling.RemoveAt(listBox2.SelectedIndex);
            }
            catch
            {
            }
            Upd();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                buying.RemoveAt(listBox1.SelectedIndex);
            }
            catch { }
            Upd();
        }
    }
}
