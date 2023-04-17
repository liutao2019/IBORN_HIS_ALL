namespace API.GZSI.UI
{
    partial class ucBalanceQuery
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucQueryPatientInfo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.lblCardType = new System.Windows.Forms.Label();
            this.rbOut = new System.Windows.Forms.RadioButton();
            this.rbIn = new System.Windows.Forms.RadioButton();
            this.txtCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ucQueryPatientInfo);
            this.neuPanel1.Controls.Add(this.lblCardType);
            this.neuPanel1.Controls.Add(this.rbOut);
            this.neuPanel1.Controls.Add(this.rbIn);
            this.neuPanel1.Controls.Add(this.txtCardNo);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(813, 53);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ucQueryPatientInfo
            // 
            this.ucQueryPatientInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(241)))));
            this.ucQueryPatientInfo.DefaultInputType = 0;
            this.ucQueryPatientInfo.InputType = 0;
            this.ucQueryPatientInfo.IsDeptOnly = true;
            this.ucQueryPatientInfo.Location = new System.Drawing.Point(209, 13);
            this.ucQueryPatientInfo.Name = "ucQueryPatientInfo";
            this.ucQueryPatientInfo.PatientInState = "ALL";
            this.ucQueryPatientInfo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.AfterArrived;
            this.ucQueryPatientInfo.Size = new System.Drawing.Size(237, 27);
            this.ucQueryPatientInfo.TabIndex = 103;
            this.ucQueryPatientInfo.Visible = false;
            this.ucQueryPatientInfo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo_myEvent);
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Location = new System.Drawing.Point(207, 23);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(53, 12);
            this.lblCardType.TabIndex = 3;
            this.lblCardType.Text = "门诊号：";
            // 
            // rbOut
            // 
            this.rbOut.AutoSize = true;
            this.rbOut.Checked = true;
            this.rbOut.Location = new System.Drawing.Point(28, 20);
            this.rbOut.Name = "rbOut";
            this.rbOut.Size = new System.Drawing.Size(47, 16);
            this.rbOut.TabIndex = 2;
            this.rbOut.TabStop = true;
            this.rbOut.Text = "门诊";
            this.rbOut.UseVisualStyleBackColor = true;
            this.rbOut.CheckedChanged += new System.EventHandler(this.rbOut_CheckedChanged);
            // 
            // rbIn
            // 
            this.rbIn.AutoSize = true;
            this.rbIn.Location = new System.Drawing.Point(104, 20);
            this.rbIn.Name = "rbIn";
            this.rbIn.Size = new System.Drawing.Size(47, 16);
            this.rbIn.TabIndex = 1;
            this.rbIn.Text = "住院";
            this.rbIn.UseVisualStyleBackColor = true;
            this.rbIn.CheckedChanged += new System.EventHandler(this.rbIn_CheckedChanged);
            // 
            // txtCardNo
            // 
            this.txtCardNo.IsEnter2Tab = false;
            this.txtCardNo.Location = new System.Drawing.Point(266, 19);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(180, 21);
            this.txtCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNo.TabIndex = 0;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuButton1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 970);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(813, 48);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(540, 15);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(75, 23);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 0;
            this.neuButton1.Text = "打印";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // neuPanel3
            // 
            this.neuPanel3.AutoScroll = true;
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 53);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(813, 917);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // ucInBalanceInfoPrintForClinic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucInBalanceInfoPrintForClinic";
            this.Size = new System.Drawing.Size(813, 1018);
            this.Load += new System.EventHandler(this.ucInBalanceInfoPrintForClinic_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNo;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private System.Windows.Forms.RadioButton rbOut;
        private System.Windows.Forms.RadioButton rbIn;
        private System.Windows.Forms.Label lblCardType;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryPatientInfo;
    }
}
