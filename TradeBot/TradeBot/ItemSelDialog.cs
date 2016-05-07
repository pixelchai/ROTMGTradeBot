/*
Copyright 2016, Abhinav Bhandari (AKA: PixelZerg) -- Offered under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE VERSION 3
THE PROGRAM IS DISTRIBUTED IN THE HOPE THAT IT WILL BE USEFUL, BUT WITHOUT ANY WARRANTY.
IT IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.
IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW THE AUTHOR WILL BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IF THE AUTHOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
*/
using Lib_K_Relay.Utilities;
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
    public partial class ItemSelDialog : Form
    {
        public int ret = -1;
        public bool ov = false;
        public ItemSelDialog()
        {
            InitializeComponent();
            Updt();
        }
        public void Updt()
        {
            listBox1.Items.Clear();
            foreach (string item in Serializer.Items.Keys)
            {
                bool rgxmatch = false;
                try {
                    rgxmatch = new Regex(textBox1.Text, RegexOptions.IgnoreCase).IsMatch(item);
                }
                catch { }
                if (item.ToLower().Contains(textBox1.Text.ToLower()) || rgxmatch)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Updt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ret = Serializer.Items[listBox1.SelectedItem.ToString()];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting..." + ex+Environment.NewLine+Environment.NewLine+"Send this message to PixelZerg (the dev)");
            }
            this.Close();
        }
        public int GetID()
        {
            while (ret == -1 && !ov)
            {
            }
            return ret;
        }

        private void ItemSelDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            ov = true;
        }
    }
}
