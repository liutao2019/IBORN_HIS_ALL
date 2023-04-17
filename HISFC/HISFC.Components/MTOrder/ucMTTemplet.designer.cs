namespace FS.HISFC.Components.MTOrder
{
    partial class ucMTTemplet
    {
        /// <summary> 
        /// 必需的设计器变量。        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucSchemaMon = new FS.HISFC.Components.MTOrder.ucSchemaTemplet();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucSchemaTue = new FS.HISFC.Components.MTOrder.ucSchemaTemplet();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucSchemaWed = new FS.HISFC.Components.MTOrder.ucSchemaTemplet();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ucSchemaThu = new FS.HISFC.Components.MTOrder.ucSchemaTemplet();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.ucSchemaFri = new FS.HISFC.Components.MTOrder.ucSchemaTemplet();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ucSchemaSat = new FS.HISFC.Components.MTOrder.ucSchemaTemplet();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.ucSchemaSun = new FS.HISFC.Components.MTOrder.ucSchemaTemplet();
            this.splitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbMedTech = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.treeView1 = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 486);
            this.panel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(199, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(497, 486);
            this.panel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel3.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(497, 486);
            this.tabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucSchemaMon);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(489, 460);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = " 星期一 ";
            // 
            // ucSchemaMon
            // 
            this.ucSchemaMon.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchemaMon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchemaMon.Location = new System.Drawing.Point(0, 0);
            this.ucSchemaMon.MedTechType = null;
            this.ucSchemaMon.Name = "ucSchemaMon";
            this.ucSchemaMon.Size = new System.Drawing.Size(489, 460);
            this.ucSchemaMon.TabIndex = 0;
            this.ucSchemaMon.Week = System.DayOfWeek.Monday;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucSchemaTue);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(489, 460);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = " 星期二 ";
            // 
            // ucSchemaTue
            // 
            this.ucSchemaTue.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchemaTue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchemaTue.Location = new System.Drawing.Point(0, 0);
            this.ucSchemaTue.MedTechType = null;
            this.ucSchemaTue.Name = "ucSchemaTue";
            this.ucSchemaTue.Size = new System.Drawing.Size(489, 460);
            this.ucSchemaTue.TabIndex = 0;
            this.ucSchemaTue.Week = System.DayOfWeek.Monday;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucSchemaWed);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(489, 460);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = " 星期三 ";
            // 
            // ucSchemaWed
            // 
            this.ucSchemaWed.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchemaWed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchemaWed.Location = new System.Drawing.Point(0, 0);
            this.ucSchemaWed.MedTechType = null;
            this.ucSchemaWed.Name = "ucSchemaWed";
            this.ucSchemaWed.Size = new System.Drawing.Size(489, 460);
            this.ucSchemaWed.TabIndex = 0;
            this.ucSchemaWed.Week = System.DayOfWeek.Monday;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ucSchemaThu);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(489, 460);
            this.tabPage4.TabIndex = 6;
            this.tabPage4.Text = " 星期四 ";
            // 
            // ucSchemaThu
            // 
            this.ucSchemaThu.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchemaThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchemaThu.Location = new System.Drawing.Point(0, 0);
            this.ucSchemaThu.MedTechType = null;
            this.ucSchemaThu.Name = "ucSchemaThu";
            this.ucSchemaThu.Size = new System.Drawing.Size(489, 460);
            this.ucSchemaThu.TabIndex = 0;
            this.ucSchemaThu.Week = System.DayOfWeek.Monday;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.ucSchemaFri);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(489, 460);
            this.tabPage5.TabIndex = 7;
            this.tabPage5.Text = " 星期五 ";
            // 
            // ucSchemaFri
            // 
            this.ucSchemaFri.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchemaFri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchemaFri.Location = new System.Drawing.Point(0, 0);
            this.ucSchemaFri.MedTechType = null;
            this.ucSchemaFri.Name = "ucSchemaFri";
            this.ucSchemaFri.Size = new System.Drawing.Size(489, 460);
            this.ucSchemaFri.TabIndex = 0;
            this.ucSchemaFri.Week = System.DayOfWeek.Monday;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.ucSchemaSat);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(489, 460);
            this.tabPage6.TabIndex = 8;
            this.tabPage6.Text = " 星期六 ";
            // 
            // ucSchemaSat
            // 
            this.ucSchemaSat.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchemaSat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchemaSat.Location = new System.Drawing.Point(0, 0);
            this.ucSchemaSat.MedTechType = null;
            this.ucSchemaSat.Name = "ucSchemaSat";
            this.ucSchemaSat.Size = new System.Drawing.Size(489, 460);
            this.ucSchemaSat.TabIndex = 0;
            this.ucSchemaSat.Week = System.DayOfWeek.Monday;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.ucSchemaSun);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(489, 460);
            this.tabPage7.TabIndex = 9;
            this.tabPage7.Text = " 星期日 ";
            // 
            // ucSchemaSun
            // 
            this.ucSchemaSun.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchemaSun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchemaSun.Location = new System.Drawing.Point(0, 0);
            this.ucSchemaSun.MedTechType = null;
            this.ucSchemaSun.Name = "ucSchemaSun";
            this.ucSchemaSun.Size = new System.Drawing.Size(489, 460);
            this.ucSchemaSun.TabIndex = 0;
            this.ucSchemaSun.Week = System.DayOfWeek.Monday;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Control;
            this.splitter1.Location = new System.Drawing.Point(197, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 486);
            this.splitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FloralWhite;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(197, 486);
            this.panel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label1.TabIndex = 1;
            this.label1.Text = "查找(F1)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbMedTech);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 1F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 486);
            this.groupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbMedTech
            // 
            this.cmbMedTech.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbMedTech.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbMedTech.IsFlat = false;
            this.cmbMedTech.IsLike = true;
            this.cmbMedTech.Location = new System.Drawing.Point(80, 8);
            this.cmbMedTech.Name = "cmbMedTech";
            this.cmbMedTech.PopForm = null;
            this.cmbMedTech.ShowCustomerList = false;
            this.cmbMedTech.IsShowID = false;
            this.cmbMedTech.Size = new System.Drawing.Size(96, 20);
            this.cmbMedTech.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbMedTech.TabIndex = 1;
            this.cmbMedTech.Tag = "";
            this.cmbMedTech.ToolBarUse = false;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Font = new System.Drawing.Font("宋体", 9F);
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(1, 35);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(193, 449);
            this.treeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.treeView1.TabIndex = 0;
            // 
            // ucMTTemplet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Name = "ucMTTemplet";
            this.Size = new System.Drawing.Size(696, 486);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel2;
        private FS.FrameWork.WinForms.Controls.NeuSplitter splitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel3;
        private FS.FrameWork.WinForms.Controls.NeuTabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        private FS.HISFC.Components.Common.Controls.baseTreeView treeView1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMedTech;
        private FS.HISFC.Components.MTOrder.ucSchemaTemplet ucSchemaMon;
        private FS.HISFC.Components.MTOrder.ucSchemaTemplet ucSchemaTue;
        private FS.HISFC.Components.MTOrder.ucSchemaTemplet ucSchemaWed;
        private FS.HISFC.Components.MTOrder.ucSchemaTemplet ucSchemaThu;
        private FS.HISFC.Components.MTOrder.ucSchemaTemplet ucSchemaFri;
        private FS.HISFC.Components.MTOrder.ucSchemaTemplet ucSchemaSat;
        private FS.HISFC.Components.MTOrder.ucSchemaTemplet ucSchemaSun;        
    }
}
