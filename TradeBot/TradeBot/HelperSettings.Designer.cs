namespace PZTradeBot
{
    partial class HelperSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.osel = new System.Windows.Forms.Button();
            this.odisp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.oadd = new System.Windows.Forms.Button();
            this.ocombo = new System.Windows.Forms.ComboBox();
            this.orem = new System.Windows.Forms.Button();
            this.olist = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ssel = new System.Windows.Forms.Button();
            this.sdisp = new System.Windows.Forms.Label();
            this.sadd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.scombo = new System.Windows.Forms.ComboBox();
            this.srem = new System.Windows.Forms.Button();
            this.slist = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.coutnum = new System.Windows.Forms.NumericUpDown();
            this.coutsel = new System.Windows.Forms.Button();
            this.coutdisp = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cinnum = new System.Windows.Forms.NumericUpDown();
            this.cinsel = new System.Windows.Forms.Button();
            this.cindisp = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cadd = new System.Windows.Forms.Button();
            this.ccombo = new System.Windows.Forms.ComboBox();
            this.crem = new System.Windows.Forms.Button();
            this.clist = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coutnum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cinnum)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.osel);
            this.panel1.Controls.Add(this.odisp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.oadd);
            this.panel1.Controls.Add(this.ocombo);
            this.panel1.Controls.Add(this.orem);
            this.panel1.Controls.Add(this.olist);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 543);
            this.panel1.TabIndex = 0;
            // 
            // osel
            // 
            this.osel.Location = new System.Drawing.Point(269, 487);
            this.osel.Name = "osel";
            this.osel.Size = new System.Drawing.Size(63, 23);
            this.osel.TabIndex = 6;
            this.osel.Text = "...";
            this.osel.UseVisualStyleBackColor = true;
            this.osel.Click += new System.EventHandler(this.osel_Click);
            // 
            // odisp
            // 
            this.odisp.AutoSize = true;
            this.odisp.Location = new System.Drawing.Point(83, 497);
            this.odisp.Name = "odisp";
            this.odisp.Size = new System.Drawing.Size(31, 13);
            this.odisp.TabIndex = 5;
            this.odisp.Text = "none";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 497);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Output Item:";
            // 
            // oadd
            // 
            this.oadd.Location = new System.Drawing.Point(269, 462);
            this.oadd.Name = "oadd";
            this.oadd.Size = new System.Drawing.Size(63, 23);
            this.oadd.TabIndex = 2;
            this.oadd.Text = "Add";
            this.oadd.UseVisualStyleBackColor = true;
            this.oadd.Click += new System.EventHandler(this.button1_Click);
            // 
            // ocombo
            // 
            this.ocombo.FormattingEnabled = true;
            this.ocombo.Location = new System.Drawing.Point(13, 464);
            this.ocombo.Name = "ocombo";
            this.ocombo.Size = new System.Drawing.Size(249, 21);
            this.ocombo.TabIndex = 1;
            this.ocombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // orem
            // 
            this.orem.Dock = System.Windows.Forms.DockStyle.Top;
            this.orem.Location = new System.Drawing.Point(0, 433);
            this.orem.Name = "orem";
            this.orem.Size = new System.Drawing.Size(353, 23);
            this.orem.TabIndex = 3;
            this.orem.Text = "Remove";
            this.orem.UseVisualStyleBackColor = true;
            this.orem.Click += new System.EventHandler(this.orem_Click);
            // 
            // olist
            // 
            this.olist.Dock = System.Windows.Forms.DockStyle.Top;
            this.olist.FormattingEnabled = true;
            this.olist.HorizontalScrollbar = true;
            this.olist.Location = new System.Drawing.Point(0, 0);
            this.olist.Name = "olist";
            this.olist.Size = new System.Drawing.Size(353, 433);
            this.olist.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ssel);
            this.panel2.Controls.Add(this.sdisp);
            this.panel2.Controls.Add(this.sadd);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.scombo);
            this.panel2.Controls.Add(this.srem);
            this.panel2.Controls.Add(this.slist);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(691, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(353, 543);
            this.panel2.TabIndex = 1;
            // 
            // ssel
            // 
            this.ssel.Location = new System.Drawing.Point(278, 487);
            this.ssel.Name = "ssel";
            this.ssel.Size = new System.Drawing.Size(63, 23);
            this.ssel.TabIndex = 9;
            this.ssel.Text = "...";
            this.ssel.UseVisualStyleBackColor = true;
            this.ssel.Click += new System.EventHandler(this.ssel_Click);
            // 
            // sdisp
            // 
            this.sdisp.AutoSize = true;
            this.sdisp.Location = new System.Drawing.Point(91, 497);
            this.sdisp.Name = "sdisp";
            this.sdisp.Size = new System.Drawing.Size(31, 13);
            this.sdisp.TabIndex = 8;
            this.sdisp.Text = "none";
            // 
            // sadd
            // 
            this.sadd.Location = new System.Drawing.Point(278, 466);
            this.sadd.Name = "sadd";
            this.sadd.Size = new System.Drawing.Size(63, 23);
            this.sadd.TabIndex = 6;
            this.sadd.Text = "Add";
            this.sadd.UseVisualStyleBackColor = true;
            this.sadd.Click += new System.EventHandler(this.sadd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 497);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Input Item:";
            // 
            // scombo
            // 
            this.scombo.FormattingEnabled = true;
            this.scombo.Location = new System.Drawing.Point(22, 466);
            this.scombo.Name = "scombo";
            this.scombo.Size = new System.Drawing.Size(249, 21);
            this.scombo.TabIndex = 5;
            // 
            // srem
            // 
            this.srem.Dock = System.Windows.Forms.DockStyle.Top;
            this.srem.Location = new System.Drawing.Point(0, 433);
            this.srem.Name = "srem";
            this.srem.Size = new System.Drawing.Size(353, 23);
            this.srem.TabIndex = 7;
            this.srem.Text = "Remove";
            this.srem.UseVisualStyleBackColor = true;
            this.srem.Click += new System.EventHandler(this.srem_Click);
            // 
            // slist
            // 
            this.slist.Dock = System.Windows.Forms.DockStyle.Top;
            this.slist.FormattingEnabled = true;
            this.slist.HorizontalScrollbar = true;
            this.slist.Location = new System.Drawing.Point(0, 0);
            this.slist.Name = "slist";
            this.slist.Size = new System.Drawing.Size(353, 433);
            this.slist.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.coutnum);
            this.panel3.Controls.Add(this.coutsel);
            this.panel3.Controls.Add(this.coutdisp);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.cinnum);
            this.panel3.Controls.Add(this.cinsel);
            this.panel3.Controls.Add(this.cindisp);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.cadd);
            this.panel3.Controls.Add(this.ccombo);
            this.panel3.Controls.Add(this.crem);
            this.panel3.Controls.Add(this.clist);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(353, 23);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(338, 543);
            this.panel3.TabIndex = 2;
            // 
            // coutnum
            // 
            this.coutnum.Location = new System.Drawing.Point(269, 511);
            this.coutnum.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.coutnum.Name = "coutnum";
            this.coutnum.Size = new System.Drawing.Size(63, 20);
            this.coutnum.TabIndex = 17;
            // 
            // coutsel
            // 
            this.coutsel.Location = new System.Drawing.Point(200, 508);
            this.coutsel.Name = "coutsel";
            this.coutsel.Size = new System.Drawing.Size(63, 23);
            this.coutsel.TabIndex = 16;
            this.coutsel.Text = "...";
            this.coutsel.UseVisualStyleBackColor = true;
            this.coutsel.Click += new System.EventHandler(this.coutsel_Click);
            // 
            // coutdisp
            // 
            this.coutdisp.AutoSize = true;
            this.coutdisp.Location = new System.Drawing.Point(83, 518);
            this.coutdisp.Name = "coutdisp";
            this.coutdisp.Size = new System.Drawing.Size(31, 13);
            this.coutdisp.TabIndex = 15;
            this.coutdisp.Text = "none";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 518);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Output Item:";
            // 
            // cinnum
            // 
            this.cinnum.Location = new System.Drawing.Point(269, 490);
            this.cinnum.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.cinnum.Name = "cinnum";
            this.cinnum.Size = new System.Drawing.Size(63, 20);
            this.cinnum.TabIndex = 13;
            // 
            // cinsel
            // 
            this.cinsel.Location = new System.Drawing.Point(200, 487);
            this.cinsel.Name = "cinsel";
            this.cinsel.Size = new System.Drawing.Size(63, 23);
            this.cinsel.TabIndex = 10;
            this.cinsel.Text = "...";
            this.cinsel.UseVisualStyleBackColor = true;
            this.cinsel.Click += new System.EventHandler(this.cinsel_Click);
            // 
            // cindisp
            // 
            this.cindisp.AutoSize = true;
            this.cindisp.Location = new System.Drawing.Point(83, 497);
            this.cindisp.Name = "cindisp";
            this.cindisp.Size = new System.Drawing.Size(31, 13);
            this.cindisp.TabIndex = 9;
            this.cindisp.Text = "none";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 497);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Input Item:";
            // 
            // cadd
            // 
            this.cadd.Location = new System.Drawing.Point(269, 464);
            this.cadd.Name = "cadd";
            this.cadd.Size = new System.Drawing.Size(63, 23);
            this.cadd.TabIndex = 6;
            this.cadd.Text = "Add";
            this.cadd.UseVisualStyleBackColor = true;
            this.cadd.Click += new System.EventHandler(this.cadd_Click);
            // 
            // ccombo
            // 
            this.ccombo.FormattingEnabled = true;
            this.ccombo.Location = new System.Drawing.Point(10, 466);
            this.ccombo.Name = "ccombo";
            this.ccombo.Size = new System.Drawing.Size(253, 21);
            this.ccombo.TabIndex = 5;
            // 
            // crem
            // 
            this.crem.Dock = System.Windows.Forms.DockStyle.Top;
            this.crem.Location = new System.Drawing.Point(0, 433);
            this.crem.Name = "crem";
            this.crem.Size = new System.Drawing.Size(338, 23);
            this.crem.TabIndex = 7;
            this.crem.Text = "Remove";
            this.crem.UseVisualStyleBackColor = true;
            this.crem.Click += new System.EventHandler(this.crem_Click);
            // 
            // clist
            // 
            this.clist.Dock = System.Windows.Forms.DockStyle.Top;
            this.clist.FormattingEnabled = true;
            this.clist.HorizontalScrollbar = true;
            this.clist.Location = new System.Drawing.Point(0, 0);
            this.clist.Name = "clist";
            this.clist.Size = new System.Drawing.Size(338, 433);
            this.clist.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1044, 23);
            this.panel4.TabIndex = 7;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(979, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Enabled";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(703, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Storage";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(360, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Converters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Outputs:";
            // 
            // HelperSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 566);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Name = "HelperSettings";
            this.Text = "HelperSettings";
            this.Load += new System.EventHandler(this.HelperSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coutnum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cinnum)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button oadd;
        private System.Windows.Forms.ComboBox ocombo;
        private System.Windows.Forms.ListBox olist;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button osel;
        private System.Windows.Forms.Label odisp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button orem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button sadd;
        private System.Windows.Forms.ComboBox scombo;
        private System.Windows.Forms.Button srem;
        private System.Windows.Forms.ListBox slist;
        private System.Windows.Forms.Button cadd;
        private System.Windows.Forms.ComboBox ccombo;
        private System.Windows.Forms.Button crem;
        private System.Windows.Forms.ListBox clist;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cinsel;
        private System.Windows.Forms.Label cindisp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button ssel;
        private System.Windows.Forms.Label sdisp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown coutnum;
        private System.Windows.Forms.Button coutsel;
        private System.Windows.Forms.Label coutdisp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown cinnum;
    }
}