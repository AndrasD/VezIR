namespace Vezir
{
    partial class MainControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.kezeloszerep = new System.Windows.Forms.Label();
            this.szladat = new System.Windows.Forms.DateTimePicker();
            this.kezelonev = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.kilep = new System.Windows.Forms.ToolStripMenuItem();
            this.hianyzok = new System.Windows.Forms.ToolStripMenuItem();
            this.teszt = new System.Windows.Forms.ToolStripMenuItem();
            this.eredmfo = new System.Windows.Forms.ToolStripMenuItem();
            this.eredm = new System.Windows.Forms.ToolStripMenuItem();
            this.cegall = new System.Windows.Forms.ToolStripMenuItem();
            this.frissit = new System.Windows.Forms.ToolStripMenuItem();
            this.help = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aktivcegek = new System.Windows.Forms.ToolStripMenuItem();
            this.lezartcegek = new System.Windows.Forms.ToolStripMenuItem();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.AlapTablaView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EgyszeruTablaView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OsszetettKozepsoTablaView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OsszetettAlsoTablaView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(404, 34);
            this.comboBox1.Size = new System.Drawing.Size(21, 23);
            this.comboBox1.DropDownClosed += new System.EventHandler(this.comboBox2_DropDownClosed);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1041, 78);
            this.panel1.TabIndex = 18;
            this.panel1.Tag = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.kezeloszerep);
            this.panel4.Controls.Add(this.szladat);
            this.panel4.Controls.Add(this.kezelonev);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(430, 21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(607, 52);
            this.panel4.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Adatbeviteli hónap:";
            // 
            // kezeloszerep
            // 
            this.kezeloszerep.AutoSize = true;
            this.kezeloszerep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kezeloszerep.Location = new System.Drawing.Point(361, 29);
            this.kezeloszerep.Name = "kezeloszerep";
            this.kezeloszerep.Size = new System.Drawing.Size(13, 15);
            this.kezeloszerep.TabIndex = 12;
            this.kezeloszerep.Text = "s";
            // 
            // szladat
            // 
            this.szladat.CustomFormat = "yyyy.MMMMMMMMMMMM";
            this.szladat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.szladat.Location = new System.Drawing.Point(6, 26);
            this.szladat.Name = "szladat";
            this.szladat.Size = new System.Drawing.Size(130, 21);
            this.szladat.TabIndex = 13;
            this.szladat.CloseUp += new System.EventHandler(this.dateTimePicker_CloseUp);
            // 
            // kezelonev
            // 
            this.kezelonev.AutoSize = true;
            this.kezelonev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kezelonev.Location = new System.Drawing.Point(361, 8);
            this.kezelonev.Name = "kezelonev";
            this.kezelonev.Size = new System.Drawing.Size(14, 15);
            this.kezelonev.TabIndex = 10;
            this.kezelonev.Text = "n";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(217, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "Kezelöi szerep:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Kezelö:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kilep,
            this.hianyzok,
            this.teszt,
            this.eredmfo,
            this.frissit,
            this.help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(1039, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // kilep
            // 
            this.kilep.Image = global::Vezir.Properties.Resources.Button_Turn_Off_01;
            this.kilep.Name = "kilep";
            this.kilep.Size = new System.Drawing.Size(72, 20);
            this.kilep.Text = "Kilépés";
            this.kilep.Click += new System.EventHandler(this.kilep_Click);
            // 
            // hianyzok
            // 
            this.hianyzok.Image = global::Vezir.Properties.Resources.question;
            this.hianyzok.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.hianyzok.Name = "hianyzok";
            this.hianyzok.Size = new System.Drawing.Size(97, 20);
            this.hianyzok.Text = "Mi hiányzik?";
            this.hianyzok.Click += new System.EventHandler(this.hianyzok_Click);
            // 
            // teszt
            // 
            this.teszt.Image = global::Vezir.Properties.Resources.chart_pie;
            this.teszt.Name = "teszt";
            this.teszt.Size = new System.Drawing.Size(179, 20);
            this.teszt.Text = "Bevétel-/költségstruktúrák";
            this.teszt.Click += new System.EventHandler(this.teszt_Click);
            this.teszt.VisibleChanged += new System.EventHandler(this.teszt_VisibleChanged);
            // 
            // eredmfo
            // 
            this.eredmfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eredm,
            this.cegall});
            this.eredmfo.Image = global::Vezir.Properties.Resources.copy;
            this.eredmfo.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.eredmfo.Name = "eredmfo";
            this.eredmfo.Size = new System.Drawing.Size(117, 20);
            this.eredmfo.Text = "Céginformációk";
            this.eredmfo.VisibleChanged += new System.EventHandler(this.eredmfo_VisibleChanged);
            // 
            // eredm
            // 
            this.eredm.Name = "eredm";
            this.eredm.Size = new System.Drawing.Size(316, 22);
            this.eredm.Text = "Felosztási sémák, Eredmény lehetséges sorai";
            this.eredm.Click += new System.EventHandler(this.eredm_Click);
            // 
            // cegall
            // 
            this.cegall.Name = "cegall";
            this.cegall.Size = new System.Drawing.Size(316, 22);
            this.cegall.Text = "Cégállapotok";
            this.cegall.Click += new System.EventHandler(this.cegall_Click);
            // 
            // frissit
            // 
            this.frissit.Image = global::Vezir.Properties.Resources.Button_Refresh_01;
            this.frissit.ImageTransparentColor = System.Drawing.Color.Black;
            this.frissit.Name = "frissit";
            this.frissit.Size = new System.Drawing.Size(64, 20);
            this.frissit.Text = "Frissit";
            this.frissit.ToolTipText = resources.GetString("frissit.ToolTipText");
            this.frissit.Click += new System.EventHandler(this.frissit_Click);
            // 
            // help
            // 
            this.help.Image = global::Vezir.Properties.Resources.kerdojel_1;
            this.help.ImageTransparentColor = System.Drawing.Color.Black;
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(82, 20);
            this.help.Tag = "A VEZIR rendszer";
            this.help.Text = "Segitség";
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 73);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1039, 3);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cég neve:";
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.SystemColors.Control;
            this.comboBox2.ContextMenuStrip = this.contextMenuStrip1;
            this.comboBox2.Location = new System.Drawing.Point(0, 47);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(337, 23);
            this.comboBox2.TabIndex = 0;
            this.comboBox2.Tag = "";
            this.comboBox2.SelectionChangeCommitted += new System.EventHandler(this.comboBox2_SelectionChangeCommitted);
            this.comboBox2.DropDownClosed += new System.EventHandler(this.comboBox2_DropDownClosed);
            this.comboBox2.Enter += new System.EventHandler(this.comboBox2_Enter);
            this.comboBox2.Leave += new System.EventHandler(this.comboBox2_Leave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.contextMenuStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aktivcegek,
            this.lezartcegek});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 48);
            // 
            // aktivcegek
            // 
            this.aktivcegek.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aktivcegek.Image = global::Vezir.Properties.Resources.TabGroupVertical;
            this.aktivcegek.Name = "aktivcegek";
            this.aktivcegek.Size = new System.Drawing.Size(144, 22);
            this.aktivcegek.Text = "Aktiv cégek";
            this.aktivcegek.Click += new System.EventHandler(this.aktivcegek_Click);
            // 
            // lezartcegek
            // 
            this.lezartcegek.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lezartcegek.Image = global::Vezir.Properties.Resources.RegisterClosed;
            this.lezartcegek.Name = "lezartcegek";
            this.lezartcegek.Size = new System.Drawing.Size(144, 22);
            this.lezartcegek.Text = "Lezárt cégek";
            this.lezartcegek.Click += new System.EventHandler(this.lezartcegek_Click);
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 28);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(229, 609);
            this.panel8.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.treeView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(243, 417);
            this.panel3.TabIndex = 23;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(243, 417);
            this.treeView1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(243, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 417);
            this.panel2.TabIndex = 22;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(243, 78);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 417);
            this.splitter2.TabIndex = 24;
            this.splitter2.TabStop = false;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1041, 495);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.splitter2, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.AlapTablaView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EgyszeruTablaView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OsszetettKozepsoTablaView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OsszetettAlsoTablaView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label kezelonev;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label kezeloszerep;
        private System.Windows.Forms.Label label8;
        //private System.Windows.Forms.Splitter splitter6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aktivcegek;
        private System.Windows.Forms.ToolStripMenuItem lezartcegek;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.ToolStripMenuItem hianyzok;
        public System.Windows.Forms.ToolStripMenuItem teszt;
        public System.Windows.Forms.DateTimePicker szladat;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem frissit;
        private System.Windows.Forms.ToolStripMenuItem kilep;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ToolStripMenuItem help;
        public System.Windows.Forms.ToolStripMenuItem eredmfo;
        public System.Windows.Forms.ToolStripMenuItem eredm;
        public System.Windows.Forms.ToolStripMenuItem cegall;
    }
}
