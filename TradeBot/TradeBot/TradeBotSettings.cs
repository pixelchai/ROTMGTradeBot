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
    public partial class TradeBotSettings : Form
    {
        
        private TradeSel tradeSel;
        private PZClient client;
        public TradeBotSettings(PZClient cl)
        {
            client = cl;
            InitializeComponent();
            UpdtListBox();
            //tradeSel = new TradeSel(cl.client.PlayerData.Name);
            //tradeSel.Dock = DockStyle.Fill;
            //this.panel1.Controls.Add(tradeSel);
            
            this.Text += " - " + cl.client.PlayerData.Name;
            numericUpDown1.Value = client.index;
        }
        public PZItem CloneItem(PZItem input)
        {
            PZItem ret = new PZItem();
            ret.amount = input.amount;
            ret.ID = input.ID;
            ret.nick = input.nick;
            ret.selected = input.selected;
            return ret;
        }
        public PZTrade CloneTrade(PZTrade input)
        {
            PZTrade ret = new PZTrade();
            foreach(var item in input.buying)
            {
                ret.buying.Add(CloneItem(item));
            }
            foreach (var item in input.selling)
            {
                ret.selling.Add(CloneItem(item));
            }
            ret.fbuying = input.fbuying;
            ret.fname = input.fname;
            ret.fselling = input.fselling;
            ret.includename = input.includename;
            ret.name = input.name;
            Console.WriteLine(ret);
            return ret;
        }
        public void UpdateDisp()
        {
            panel1.Controls.Clear();
            tradeSel = new TradeSel(client.tradequeue[listBox1.SelectedIndex], client.client.PlayerData.Name);
            tradeSel.Dock = DockStyle.Fill;
            panel1.Controls.Add(tradeSel);
        }
        public void Apply()
        {
            client.tradequeue[listBox1.SelectedIndex] = tradeSel.trade;
            UpdtListBox();
        }
        public void button3_Click(object sender, EventArgs e)
        {
            Apply();
        }
        public void UpdtListBox()
        {
            listBox1.Items.Clear();
            foreach (var item in client.tradequeue)
            {
                listBox1.Items.Add(item.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisp();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                client.tradequeue.RemoveAt(listBox1.SelectedIndex);
           
           
            UpdateDisp();
            UpdtListBox();
                listBox1.SelectedIndex--;
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                client.tradequeue.Add(CloneTrade(client.tradequeue[listBox1.SelectedIndex]));
                UpdtListBox();
                listBox1.SelectedIndex++;
                listBox1_SelectedIndexChanged(null, EventArgs.Empty);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PZTrade temp = client.tradequeue[listBox1.SelectedIndex + 1];
                client.tradequeue[listBox1.SelectedIndex + 1] = client.tradequeue[listBox1.SelectedIndex];
                client.tradequeue[listBox1.SelectedIndex] = temp;
                UpdtListBox();
                listBox1.SelectedIndex--;
                listBox1_SelectedIndexChanged(null, EventArgs.Empty);
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PZTrade temp = client.tradequeue[listBox1.SelectedIndex + 0];
                client.tradequeue[listBox1.SelectedIndex + 0] = client.tradequeue[listBox1.SelectedIndex - 1];
                client.tradequeue[listBox1.SelectedIndex - 1] = temp;
                UpdtListBox();
                listBox1.SelectedIndex++;
                listBox1_SelectedIndexChanged(null, EventArgs.Empty);
            }
            catch
            {
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            client.tradequeue.Add(new PZTrade() { name = client.client.PlayerData.Name } );
            UpdtListBox();
        }

        private void TradeBotSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.enabled = true;
            client.spamenabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new SaveDialog(client.tradequeue).ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDialog d = new LoadDialog(System.IO.File.ReadAllText(SnapShotHandler.DataPath+"\\data.pztb"), client.client.PlayerData.Name);
                d.ShowDialog();
                client.tradequeue = d.Get();
            }
            catch (Exception ex){ MessageBox.Show("You have no saved files: " + ex); }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new HelperSettings().ShowDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            client.index = (int)numericUpDown1.Value;
            numericUpDown1.Value = client.index;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SnapShotHandler.Save();
            MessageBox.Show("Saved!");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SnapShotHandler.LoadApply();
            MessageBox.Show("Loaded!");
        }
    }
}
