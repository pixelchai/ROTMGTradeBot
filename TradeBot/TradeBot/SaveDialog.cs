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
using System.IO;
namespace PZTradeBot
{
    public partial class SaveDialog : Form
    {
        List<PZTrade> list = new List<PZTrade>();
        public SaveDialog(List<PZTrade> l)
        {
            InitializeComponent();
            list = l;
        }
        public static void CrFolder(string path)
        {
            try
            {
                System.IO.DirectoryInfo d = new DirectoryInfo(path);
                if (!d.Exists)
                {
                    d.Create();
                }
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //CrFolder("TradeBotData");
            try
            {
                using (StreamWriter sw = new StreamWriter(SnapShotHandler.DataPath+"\\data.pztb"))
                {
                    sw.WriteLine(textBox1.Text + "{");
                    foreach (var item in list)
                    {
                        sw.WriteLine(item.ToF());
                    }
                    sw.WriteLine("}");
                }
            }
            catch (Exception ex){ MessageBox.Show(ex.ToString()); }
            this.Close();
        }
    }
}
