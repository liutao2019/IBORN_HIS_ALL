namespace FS.HISFC.Components.MTOrder
{
    partial class ucMTArrange
    {
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。

        /// </summary>
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
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucSchema1 = new FS.HISFC.Components.MTOrder.ucSchema();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucSchema2 = new FS.HISFC.Components.MTOrder.ucSchema();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucSchema3 = new FS.HISFC.Components.MTOrder.ucSchema();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ucSchema4 = new FS.HISFC.Components.MTOrder.ucSchema();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.ucSchema5 = new FS.HISFC.Components.MTOrder.ucSchema();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ucSchema6 = new FS.HISFC.Components.MTOrder.ucSchema();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.ucSchema7 = new FS.HISFC.Components.MTOrder.ucSchema();
            this.splitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbMedTech = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.treeView1 = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.ReflashTimer = new System.Windows.Forms.Timer(this.components);
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
            this.tabControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(497, 486);
            this.tabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucSchema1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(489, 460);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = " 星期一 ";
            // 
            // ucSchema1
            // 
            this.ucSchema1.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchema1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchema1.ExpiredColor = System.Drawing.Color.Empty;
            this.ucSchema1.Font = new System.Drawing.Font("宋体", 9F);
            this.ucSchema1.IsCheckChangceAndSave = false;
            this.ucSchema1.IsLockExpired = false;
            this.ucSchema1.Location = new System.Drawing.Point(0, 0);
            this.ucSchema1.MedTechType = null;
            this.ucSchema1.Name = "ucSchema1";
            this.ucSchema1.SeeDate = new System.DateTime(((long)(0)));
            this.ucSchema1.Size = new System.Drawing.Size(489, 460);
            this.ucSchema1.StopColor = System.Drawing.Color.Empty;
            this.ucSchema1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucSchema2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(489, 460);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = " 星期二 ";
            // 
            // ucSchema2
            // 
            this.ucSchema2.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchema2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchema2.ExpiredColor = System.Drawing.Color.Empty;
            this.ucSchema2.IsCheckChangceAndSave = false;
            this.ucSchema2.IsLockExpired = false;
            this.ucSchema2.Location = new System.Drawing.Point(0, 0);
            this.ucSchema2.MedTechType = null;
            this.ucSchema2.Name = "ucSchema2";
            this.ucSchema2.SeeDate = new System.DateTime(((long)(0)));
            this.ucSchema2.Size = new System.Drawing.Size(489, 460);
            this.ucSchema2.StopColor = System.Drawing.Color.Empty;
            this.ucSchema2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucSchema3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(489, 460);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = " 星期三 ";
            // 
            // ucSchema3
            // 
            this.ucSchema3.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchema3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchema3.ExpiredColor = System.Drawing.Color.Empty;
            this.ucSchema3.IsCheckChangceAndSave = false;
            this.ucSchema3.IsLockExpired = false;
            this.ucSchema3.Location = new System.Drawing.Point(0, 0);
            this.ucSchema3.MedTechType = null;
            this.ucSchema3.Name = "ucSchema3";
            this.ucSchema3.SeeDate = new System.DateTime(((long)(0)));
            this.ucSchema3.Size = new System.Drawing.Size(489, 460);
            this.ucSchema3.StopColor = System.Drawing.Color.Empty;
            this.ucSchema3.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ucSchema4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(489, 460);
            this.tabPage4.TabIndex = 6;
            this.tabPage4.Text = " 星期四 ";
            // 
            // ucSchema4
            // 
            this.ucSchema4.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchema4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchema4.ExpiredColor = System.Drawing.Color.Empty;
            this.ucSchema4.IsCheckChangceAndSave = false;
            this.ucSchema4.IsLockExpired = false;
            this.ucSchema4.Location = new System.Drawing.Point(0, 0);
            this.ucSchema4.MedTechType = null;
            this.ucSchema4.Name = "ucSchema4";
            this.ucSchema4.SeeDate = new System.DateTime(((long)(0)));
            this.ucSchema4.Size = new System.Drawing.Size(489, 460);
            this.ucSchema4.StopColor = System.Drawing.Color.Empty;
            this.ucSchema4.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.ucSchema5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(489, 460);
            this.tabPage5.TabIndex = 7;
            this.tabPage5.Text = " 星期五 ";
            // 
            // ucSchema5
            // 
            this.ucSchema5.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchema5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchema5.ExpiredColor = System.Drawing.Color.Empty;
            this.ucSchema5.IsCheckChangceAndSave = false;
            this.ucSchema5.IsLockExpired = false;
            this.ucSchema5.Location = new System.Drawing.Point(0, 0);
            this.ucSchema5.MedTechType = null;
            this.ucSchema5.Name = "ucSchema5";
            this.ucSchema5.SeeDate = new System.DateTime(((long)(0)));
            this.ucSchema5.Size = new System.Drawing.Size(489, 460);
            this.ucSchema5.StopColor = System.Drawing.Color.Empty;
            this.ucSchema5.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.ucSchema6);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(489, 460);
            this.tabPage6.TabIndex = 8;
            this.tabPage6.Text = " 星期六 ";
            // 
            // ucSchema6
            // 
            this.ucSchema6.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchema6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchema6.ExpiredColor = System.Drawing.Color.Empty;
            this.ucSchema6.IsCheckChangceAndSave = false;
            this.ucSchema6.IsLockExpired = false;
            this.ucSchema6.Location = new System.Drawing.Point(0, 0);
            this.ucSchema6.MedTechType = null;
            this.ucSchema6.Name = "ucSchema6";
            this.ucSchema6.SeeDate = new System.DateTime(((long)(0)));
            this.ucSchema6.Size = new System.Drawing.Size(489, 460);
            this.ucSchema6.StopColor = System.Drawing.Color.Empty;
            this.ucSchema6.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.ucSchema7);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(489, 460);
            this.tabPage7.TabIndex = 9;
            this.tabPage7.Text = " 星期日 ";
            // 
            // ucSchema7
            // 
            this.ucSchema7.BackColor = System.Drawing.SystemColors.Window;
            this.ucSchema7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSchema7.ExpiredColor = System.Drawing.Color.Empty;
            this.ucSchema7.IsCheckChangceAndSave = false;
            this.ucSchema7.IsLockExpired = false;
            this.ucSchema7.Location = new System.Drawing.Point(0, 0);
            this.ucSchema7.MedTechType = null;
            this.ucSchema7.Name = "ucSchema7";
            this.ucSchema7.SeeDate = new System.DateTime(((long)(0)));
            this.ucSchema7.Size = new System.Drawing.Size(489, 460);
            this.ucSchema7.StopColor = System.Drawing.Color.Empty;
            this.ucSchema7.TabIndex = 0;
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
            this.label1.Size = new System.Drawing.Size(62, 16);
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
            this.cmbMedTech.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMedTech.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbMedTech.IsEnter2Tab = false;
            this.cmbMedTech.IsFlat = false;
            this.cmbMedTech.IsLike = true;
            this.cmbMedTech.IsListOnly = false;
            this.cmbMedTech.IsPopForm = true;
            this.cmbMedTech.IsShowCustomerList = false;
            this.cmbMedTech.IsShowID = false;
            this.cmbMedTech.IsShowIDAndName = false;
            this.cmbMedTech.Location = new System.Drawing.Point(80, 8);
            this.cmbMedTech.Name = "cmbMedTech";
            this.cmbMedTech.ShowCustomerList = false;
            this.cmbMedTech.ShowID = false;
            this.cmbMedTech.Size = new System.Drawing.Size(96, 20);
            this.cmbMedTech.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
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
            // ReflashTimer
            // 
            this.ReflashTimer.Interval = 100000;
            // 
            // ucMTArrange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Name = "ucMTArrange";
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
        private MTOrder.ucSchema ucSchema1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        //private FS.FrameWork.WinForms.Controls.NeuTreeView treeView1;
        private FS.HISFC.Components.Common.Controls.baseTreeView treeView1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label1;
        private MTOrder.ucSchema ucSchema2;
        private MTOrder.ucSchema ucSchema3;
        private MTOrder.ucSchema ucSchema4;
        private MTOrder.ucSchema ucSchema5;
        private MTOrder.ucSchema ucSchema6;
        private MTOrder.ucSchema ucSchema7;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMedTech;
        private System.Windows.Forms.Timer ReflashTimer;        
    }
}
