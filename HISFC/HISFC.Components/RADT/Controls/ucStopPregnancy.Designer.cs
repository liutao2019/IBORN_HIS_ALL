namespace FS.HISFC.Components.RADT.Controls
{
    partial class ucStopPregnancy
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
            this.rBtnOutProvince = new System.Windows.Forms.RadioButton();
            this.rBtnInProvince = new System.Windows.Forms.RadioButton();
            this.rBtnLocal = new System.Windows.Forms.RadioButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtOperatedate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSex = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbRegisterInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtStopPregnancyWeeks = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbPatientNO = new System.Windows.Forms.GroupBox();
            this.ucQueryInpatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.gbRegisterInfo.SuspendLayout();
            this.gbPatientNO.SuspendLayout();
            this.SuspendLayout();
            // 
            // rBtnOutProvince
            // 
            this.rBtnOutProvince.AutoSize = true;
            this.rBtnOutProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.rBtnOutProvince.Location = new System.Drawing.Point(199, 127);
            this.rBtnOutProvince.Name = "rBtnOutProvince";
            this.rBtnOutProvince.Size = new System.Drawing.Size(47, 16);
            this.rBtnOutProvince.TabIndex = 31;
            this.rBtnOutProvince.TabStop = true;
            this.rBtnOutProvince.Text = "省外";
            this.rBtnOutProvince.UseVisualStyleBackColor = true;
            // 
            // rBtnInProvince
            // 
            this.rBtnInProvince.AutoSize = true;
            this.rBtnInProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.rBtnInProvince.Location = new System.Drawing.Point(149, 127);
            this.rBtnInProvince.Name = "rBtnInProvince";
            this.rBtnInProvince.Size = new System.Drawing.Size(47, 16);
            this.rBtnInProvince.TabIndex = 30;
            this.rBtnInProvince.TabStop = true;
            this.rBtnInProvince.Text = "省内";
            this.rBtnInProvince.UseVisualStyleBackColor = true;
            // 
            // rBtnLocal
            // 
            this.rBtnLocal.AutoSize = true;
            this.rBtnLocal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.rBtnLocal.Location = new System.Drawing.Point(99, 127);
            this.rBtnLocal.Name = "rBtnLocal";
            this.rBtnLocal.Size = new System.Drawing.Size(47, 16);
            this.rBtnLocal.TabIndex = 29;
            this.rBtnLocal.TabStop = true;
            this.rBtnLocal.Text = "本地";
            this.rBtnLocal.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(34, 33);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "姓    名：";
            // 
            // dtOperatedate
            // 
            this.dtOperatedate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtOperatedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOperatedate.IsEnter2Tab = false;
            this.dtOperatedate.Location = new System.Drawing.Point(99, 158);
            this.dtOperatedate.Name = "dtOperatedate";
            this.dtOperatedate.Size = new System.Drawing.Size(147, 21);
            this.dtOperatedate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtOperatedate.TabIndex = 28;
            // 
            // txtName
            // 
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(99, 30);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(147, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(34, 65);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "性    别：";
            // 
            // txtSex
            // 
            this.txtSex.IsEnter2Tab = false;
            this.txtSex.Location = new System.Drawing.Point(99, 62);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(147, 21);
            this.txtSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSex.TabIndex = 3;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.neuLabel3.Location = new System.Drawing.Point(10, 97);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(89, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "终止妊娠周数：";
            // 
            // gbRegisterInfo
            // 
            this.gbRegisterInfo.Controls.Add(this.rBtnOutProvince);
            this.gbRegisterInfo.Controls.Add(this.rBtnInProvince);
            this.gbRegisterInfo.Controls.Add(this.rBtnLocal);
            this.gbRegisterInfo.Controls.Add(this.neuLabel1);
            this.gbRegisterInfo.Controls.Add(this.dtOperatedate);
            this.gbRegisterInfo.Controls.Add(this.txtName);
            this.gbRegisterInfo.Controls.Add(this.neuLabel2);
            this.gbRegisterInfo.Controls.Add(this.txtSex);
            this.gbRegisterInfo.Controls.Add(this.neuLabel3);
            this.gbRegisterInfo.Controls.Add(this.txtStopPregnancyWeeks);
            this.gbRegisterInfo.Controls.Add(this.neuLabel4);
            this.gbRegisterInfo.Controls.Add(this.btCancel);
            this.gbRegisterInfo.Controls.Add(this.btSave);
            this.gbRegisterInfo.Controls.Add(this.neuLabel10);
            this.gbRegisterInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRegisterInfo.Location = new System.Drawing.Point(0, 48);
            this.gbRegisterInfo.Name = "gbRegisterInfo";
            this.gbRegisterInfo.Size = new System.Drawing.Size(269, 252);
            this.gbRegisterInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbRegisterInfo.TabIndex = 39;
            this.gbRegisterInfo.TabStop = false;
            this.gbRegisterInfo.Text = "终止妊娠登记信息";
            // 
            // txtStopPregnancyWeeks
            // 
            this.txtStopPregnancyWeeks.IsEnter2Tab = false;
            this.txtStopPregnancyWeeks.Location = new System.Drawing.Point(99, 94);
            this.txtStopPregnancyWeeks.Name = "txtStopPregnancyWeeks";
            this.txtStopPregnancyWeeks.Size = new System.Drawing.Size(147, 21);
            this.txtStopPregnancyWeeks.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtStopPregnancyWeeks.TabIndex = 5;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.neuLabel4.Location = new System.Drawing.Point(34, 129);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "地    区：";
            // 
            // btCancel
            // 
            this.btCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCancel.Location = new System.Drawing.Point(149, 208);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(91, 23);
            this.btCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancel.TabIndex = 22;
            this.btCancel.Text = "取消登记(&C)";
            this.btCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSave.Location = new System.Drawing.Point(56, 208);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(91, 23);
            this.btSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btSave.TabIndex = 21;
            this.btSave.Text = "保存(&S)";
            this.btSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btSave.UseVisualStyleBackColor = true;
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(34, 162);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(65, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 18;
            this.neuLabel10.Text = "登记时间：";
            // 
            // gbPatientNO
            // 
            this.gbPatientNO.Controls.Add(this.ucQueryInpatientNo);
            this.gbPatientNO.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPatientNO.Location = new System.Drawing.Point(0, 0);
            this.gbPatientNO.Name = "gbPatientNO";
            this.gbPatientNO.Size = new System.Drawing.Size(269, 48);
            this.gbPatientNO.TabIndex = 38;
            this.gbPatientNO.TabStop = false;
            this.gbPatientNO.Visible = false;
            // 
            // ucQueryInpatientNo
            // 
            this.ucQueryInpatientNo.DefaultInputType = 0;
            this.ucQueryInpatientNo.InputType = 0;
            this.ucQueryInpatientNo.Location = new System.Drawing.Point(36, 13);
            this.ucQueryInpatientNo.Name = "ucQueryInpatientNo";
            this.ucQueryInpatientNo.PatientInState = "I";
            this.ucQueryInpatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo.Size = new System.Drawing.Size(210, 27);
            this.ucQueryInpatientNo.TabIndex = 34;
            // 
            // ucStopPregnancy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbRegisterInfo);
            this.Controls.Add(this.gbPatientNO);
            this.Name = "ucStopPregnancy";
            this.Size = new System.Drawing.Size(269, 300);
            this.gbRegisterInfo.ResumeLayout(false);
            this.gbRegisterInfo.PerformLayout();
            this.gbPatientNO.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rBtnOutProvince;
        private System.Windows.Forms.RadioButton rBtnInProvince;
        private System.Windows.Forms.RadioButton rBtnLocal;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtOperatedate;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbRegisterInfo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtStopPregnancyWeeks;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btSave;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private System.Windows.Forms.GroupBox gbPatientNO;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo;
    }
}
