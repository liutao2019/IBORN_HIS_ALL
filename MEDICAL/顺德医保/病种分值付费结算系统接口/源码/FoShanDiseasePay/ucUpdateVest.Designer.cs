namespace FoShanDiseasePay
{
    partial class ucUpdateVest
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
            this.gbTop = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblQty = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblLoginInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtRYQK = new System.Windows.Forms.TextBox();
            this.txtZLGC = new System.Windows.Forms.TextBox();
            this.txtCYQK = new System.Windows.Forms.TextBox();
            this.txtCYYZ = new System.Windows.Forms.TextBox();
            this.txtSWJLRYQK = new System.Windows.Forms.TextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSWJLZLGC = new System.Windows.Forms.TextBox();
            this.lblPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTop
            // 
            this.gbTop.Controls.Add(this.lblPatientInfo);
            this.gbTop.Controls.Add(this.button1);
            this.gbTop.Controls.Add(this.neuLabel7);
            this.gbTop.Controls.Add(this.lblQty);
            this.gbTop.Controls.Add(this.txtNo);
            this.gbTop.Controls.Add(this.lblLoginInfo);
            this.gbTop.Controls.Add(this.neuLabel9);
            this.gbTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTop.Location = new System.Drawing.Point(0, 0);
            this.gbTop.Name = "gbTop";
            this.gbTop.Size = new System.Drawing.Size(1152, 79);
            this.gbTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTop.TabIndex = 0;
            this.gbTop.TabStop = false;
            this.gbTop.Text = "操作信息";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(598, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "保   存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(321, 30);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(65, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 26;
            this.neuLabel7.Text = "住院次数：";
            // 
            // lblQty
            // 
            this.lblQty.IsEnter2Tab = false;
            this.lblQty.Location = new System.Drawing.Point(387, 25);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(104, 21);
            this.lblQty.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblQty.TabIndex = 25;
            // 
            // txtNo
            // 
            this.txtNo.IsEnter2Tab = false;
            this.txtNo.Location = new System.Drawing.Point(141, 26);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(104, 21);
            this.txtNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNo.TabIndex = 23;
            this.txtNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNo_KeyDown);
            // 
            // lblLoginInfo
            // 
            this.lblLoginInfo.AutoSize = true;
            this.lblLoginInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoginInfo.ForeColor = System.Drawing.Color.Red;
            this.lblLoginInfo.Location = new System.Drawing.Point(6, 109);
            this.lblLoginInfo.Name = "lblLoginInfo";
            this.lblLoginInfo.Size = new System.Drawing.Size(59, 12);
            this.lblLoginInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLoginInfo.TabIndex = 8;
            this.lblLoginInfo.Text = "请先登录!";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(87, 32);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(53, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 24;
            this.neuLabel9.Text = "住院号：";
            // 
            // txtRYQK
            // 
            this.txtRYQK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRYQK.Location = new System.Drawing.Point(200, 106);
            this.txtRYQK.Multiline = true;
            this.txtRYQK.Name = "txtRYQK";
            this.txtRYQK.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRYQK.Size = new System.Drawing.Size(503, 72);
            this.txtRYQK.TabIndex = 42;
            // 
            // txtZLGC
            // 
            this.txtZLGC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZLGC.Location = new System.Drawing.Point(200, 197);
            this.txtZLGC.Multiline = true;
            this.txtZLGC.Name = "txtZLGC";
            this.txtZLGC.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtZLGC.Size = new System.Drawing.Size(503, 72);
            this.txtZLGC.TabIndex = 43;
            // 
            // txtCYQK
            // 
            this.txtCYQK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCYQK.Location = new System.Drawing.Point(200, 286);
            this.txtCYQK.Multiline = true;
            this.txtCYQK.Name = "txtCYQK";
            this.txtCYQK.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCYQK.Size = new System.Drawing.Size(503, 72);
            this.txtCYQK.TabIndex = 44;
            // 
            // txtCYYZ
            // 
            this.txtCYYZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCYYZ.Location = new System.Drawing.Point(200, 376);
            this.txtCYYZ.Multiline = true;
            this.txtCYYZ.Name = "txtCYYZ";
            this.txtCYYZ.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCYYZ.Size = new System.Drawing.Size(503, 72);
            this.txtCYYZ.TabIndex = 45;
            // 
            // txtSWJLRYQK
            // 
            this.txtSWJLRYQK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSWJLRYQK.Location = new System.Drawing.Point(200, 463);
            this.txtSWJLRYQK.Multiline = true;
            this.txtSWJLRYQK.Name = "txtSWJLRYQK";
            this.txtSWJLRYQK.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSWJLRYQK.Size = new System.Drawing.Size(503, 72);
            this.txtSWJLRYQK.TabIndex = 46;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(129, 109);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 25;
            this.neuLabel1.Text = "入院情况：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(57, 200);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(137, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 47;
            this.neuLabel2.Text = "住院情况（诊疗过程）：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(-1, 557);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(197, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 48;
            this.neuLabel3.Text = "死亡记录的住院情况（诊疗过程）：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(129, 380);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 49;
            this.neuLabel4.Text = "出院医嘱：";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(129, 289);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 50;
            this.neuLabel5.Text = "出院情况：";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(69, 468);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(125, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 51;
            this.neuLabel6.Text = "死亡记录的入院情况：";
            // 
            // txtSWJLZLGC
            // 
            this.txtSWJLZLGC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSWJLZLGC.Location = new System.Drawing.Point(200, 554);
            this.txtSWJLZLGC.Multiline = true;
            this.txtSWJLZLGC.Name = "txtSWJLZLGC";
            this.txtSWJLZLGC.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSWJLZLGC.Size = new System.Drawing.Size(503, 72);
            this.txtSWJLZLGC.TabIndex = 52;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Location = new System.Drawing.Point(87, 55);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(539, 12);
            this.lblPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientInfo.TabIndex = 28;
            this.lblPatientInfo.Text = "患者信息：刘德华  男 49岁    入院时间：2018-09-10 00:00:00  出院时间：2018-10-16 23:59:59";
            // 
            // ucUpdateVest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSWJLZLGC);
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.txtSWJLRYQK);
            this.Controls.Add(this.txtCYYZ);
            this.Controls.Add(this.txtCYQK);
            this.Controls.Add(this.txtZLGC);
            this.Controls.Add(this.txtRYQK);
            this.Controls.Add(this.gbTop);
            this.Name = "ucUpdateVest";
            this.Size = new System.Drawing.Size(1152, 648);
            this.gbTop.ResumeLayout(false);
            this.gbTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTop;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblLoginInfo;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpNeedUpload;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpHaveUploaded;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPatientType;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private System.Windows.Forms.TextBox txtRYQK;
        private System.Windows.Forms.TextBox txtZLGC;
        private System.Windows.Forms.TextBox txtCYQK;
        private System.Windows.Forms.TextBox txtCYYZ;
        private System.Windows.Forms.TextBox txtSWJLRYQK;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private System.Windows.Forms.TextBox txtSWJLZLGC;
        private FS.FrameWork.WinForms.Controls.NeuTextBox lblQty;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private System.Windows.Forms.Button button1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientInfo;
    }
}
