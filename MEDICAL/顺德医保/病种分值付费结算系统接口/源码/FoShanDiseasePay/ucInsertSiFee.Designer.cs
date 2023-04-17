namespace FoShanDiseasePay
{
    partial class ucInsertSiFee
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
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblQty = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDJH = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTop
            // 
            this.gbTop.Controls.Add(this.lblPatientInfo);
            this.gbTop.Controls.Add(this.neuLabel2);
            this.gbTop.Controls.Add(this.txtDJH);
            this.gbTop.Controls.Add(this.neuLabel7);
            this.gbTop.Controls.Add(this.lblQty);
            this.gbTop.Controls.Add(this.txtNo);
            this.gbTop.Controls.Add(this.neuLabel9);
            this.gbTop.Controls.Add(this.neuLabel1);
            this.gbTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTop.Location = new System.Drawing.Point(0, 0);
            this.gbTop.Name = "gbTop";
            this.gbTop.Size = new System.Drawing.Size(1152, 154);
            this.gbTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTop.TabIndex = 0;
            this.gbTop.TabStop = false;
            this.gbTop.Text = "操作信息";
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
            // txtDJH
            // 
            this.txtDJH.IsEnter2Tab = false;
            this.txtDJH.Location = new System.Drawing.Point(624, 23);
            this.txtDJH.Name = "txtDJH";
            this.txtDJH.Size = new System.Drawing.Size(164, 21);
            this.txtDJH.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDJH.TabIndex = 27;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(553, 28);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 28;
            this.neuLabel1.Text = "收费单据号：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(87, 72);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(89, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 29;
            this.neuLabel2.Text = "患者基本信息：";
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Location = new System.Drawing.Point(108, 93);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(11, 12);
            this.lblPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientInfo.TabIndex = 30;
            this.lblPatientInfo.Text = " ";
            // 
            // ucInsertSiFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTop);
            this.Name = "ucInsertSiFee";
            this.Size = new System.Drawing.Size(1152, 648);
            this.gbTop.ResumeLayout(false);
            this.gbTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTop;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpNeedUpload;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpHaveUploaded;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPatientType;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuTextBox lblQty;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtDJH;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientInfo;
    }
}
