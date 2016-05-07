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

namespace PZTradeBot
{
    public partial class GiveDialog : Form
    {
        private PZClient client = null;
        public GiveDialog(PZClient c)
        {
            client = c;
            InitializeComponent();
        }
        public List<int> ids = new List<int>();

        private void GiveDialog_Load(object sender, EventArgs e)
        {
            foreach (var item in client.client.PlayerData.Slot)
            {
                if (!ids.Contains(item))
                {
                    ids.Add(item);
                    listBox1.Items.Add(PZItem.GetNameFromID(item));
                }
            }

            foreach (var client in TradeBotCore.clientlist)
            {
                comboBox1.Items.Add(client);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HelperHandler.GiveItem(client, (PZClient)comboBox1.SelectedItem, new PZItem(ids[listBox1.SelectedIndex], (int)numericUpDown1.Value));
            this.Close();
        }
    }
}
