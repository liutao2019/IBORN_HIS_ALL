namespace FS.HISFC.Components.MTOrder.Forms
{
    partial class frmSelectPatient
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cbType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtClinicNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.groupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.button2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.button1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbType);
            this.panel1.Controls.Add(this.neuLabel1);
            this.panel1.Controls.Add(this.txtClinicNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 178);
            this.panel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel1.TabIndex = 1;
            // 
            // cbType
            // 
            this.cbType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.IsEnter2Tab = false;
            this.cbType.IsFlat = false;
            this.cbType.IsLike = true;
            this.cbType.IsListOnly = false;
            this.cbType.IsPopForm = true;
            this.cbType.IsShowCustomerList = false;
            this.cbType.IsShowID = false;
            this.cbType.IsShowIDAndName = false;
            this.cbType.Items.AddRange(new object[] {
            "门诊",
            "住院"});
            this.cbType.Location = new System.Drawing.Point(132, 57);
            this.cbType.Name = "cbType";
            this.cbType.ShowCustomerList = false;
            this.cbType.ShowID = false;
            this.cbType.Size = new System.Drawing.Size(121, 20);
            this.cbType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbType.TabIndex = 6;
            this.cbType.Tag = "";
            this.cbType.ToolBarUse = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(61, 60);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 5;
            this.neuLabel1.Text = "患者类型：";
            // 
            // txtClinicNo
            // 
            this.txtClinicNo.IsEnter2Tab = false;
            this.txtClinicNo.Location = new System.Drawing.Point(133, 94);
            this.txtClinicNo.Name = "txtClinicNo";
            this.txtClinicNo.Size = new System.Drawing.Size(120, 21);
            this.txtClinicNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtClinicNo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(43, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 23);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label1.TabIndex = 3;
            this.label1.Text = "病历/住院号：";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 128);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(283, 50);
            this.panel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel3.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 3);
            this.groupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button2.Location = new System.Drawing.Point(187, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button2.TabIndex = 1;
            this.button2.Text = "取消(&X)";
            this.button2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button1.Location = new System.Drawing.Point(93, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button1.TabIndex = 0;
            this.button1.Text = "确定(&O)";
            this.button1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(283, 53);
            this.panel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 32);
            this.label2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label2.TabIndex = 1;
            this.label2.Text = "请输入病人的病历/住院号";
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 2);
            this.groupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // frmSelectPatient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(283, 178);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择病人信息";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel3;
        private FS.FrameWork.WinForms.Controls.NeuButton button1;
        private FS.FrameWork.WinForms.Controls.NeuButton button2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtClinicNo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}