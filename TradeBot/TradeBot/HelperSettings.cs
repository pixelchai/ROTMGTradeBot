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
    public partial class HelperSettings : Form
    {
        List<PZClient> clients = new List<PZClient>();
        private int oid = 0;
        private int cinid = 0;
        private int coutid = 0;
        private int sid = 0;
         
        public HelperSettings()
        {
            InitializeComponent();
        }
        public void UpdtListBoxes()
        {
            olist.Items.Clear();
            clist.Items.Clear();
            slist.Items.Clear();

            foreach (var item in HelperHandler.olist)
            {
                olist.Items.Add(item);
            }
            foreach (var item in HelperHandler.clist)
            {
                clist.Items.Add(item);
            }
            foreach (var item in HelperHandler.slist)
            {
                slist.Items.Add(item);
            }
        }

        private void HelperSettings_Load(object sender, EventArgs e)
        {
            UpdtListBoxes();
            UpdtCombos();
        }
        public void UpdtCombos()
        {
            UpdtCombo(ocombo);
            UpdtCombo(ccombo);
            UpdtCombo(scombo);
        }
        public void UpdtCombo(ComboBox c)
        {
            c.Items.Clear();
            foreach (var item in TradeBotCore.clientlist)
            {
                if (!clients.Contains(item))
                {
                    c.Items.Add(item);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HelperHandler.olist.Add(new PZOutput((PZClient)ocombo.SelectedItem, oid));
            UpdtListBoxes();
            UpdtCombos();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cadd_Click(object sender, EventArgs e)
        {
            HelperHandler.clist.Add(new PZConverter((PZClient)ccombo.SelectedItem, new PZItem(cinid, (int)cinnum.Value), new PZItem(coutid, (int)coutnum.Value)));
            UpdtListBoxes();
            UpdtCombos();
        }

        private void osel_Click(object sender, EventArgs e)
        {
            ItemSelDialog s = new ItemSelDialog();
            s.ShowDialog();
            oid =  s.GetID();
            odisp.Text = PZItem.GetNameFromID(oid);
        }

        private void orem_Click(object sender, EventArgs e)
        {
            HelperHandler.olist.RemoveAt(olist.SelectedIndex);
        }

        private void crem_Click(object sender, EventArgs e)
        {
            HelperHandler.clist.RemoveAt(clist.SelectedIndex);
        }

        private void srem_Click(object sender, EventArgs e)
        {
            HelperHandler.slist.RemoveAt(slist.SelectedIndex);
        }

        private void cinsel_Click(object sender, EventArgs e)
        {
            ItemSelDialog s = new ItemSelDialog();
            s.ShowDialog();
            cinid =  s.GetID();
            cindisp.Text = PZItem.GetNameFromID(cinid);
        }

        private void coutsel_Click(object sender, EventArgs e)
        {
            ItemSelDialog s = new ItemSelDialog();
            s.ShowDialog();
            coutid =  s.GetID();
            coutdisp.Text = PZItem.GetNameFromID(coutid);
        }

        private void ssel_Click(object sender, EventArgs e)
        {
            ItemSelDialog s = new ItemSelDialog();
            s.ShowDialog();
            sid = s.GetID();
            sdisp.Text = PZItem.GetNameFromID(sid);
        }

        private void sadd_Click(object sender, EventArgs e)
        {
            HelperHandler.slist.Add(new PZStorage((PZClient)scombo.SelectedItem, sid));
            UpdtCombos();
            UpdtListBoxes();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            HelperHandler.enabled = checkBox1.Checked;
        }
    }
}
