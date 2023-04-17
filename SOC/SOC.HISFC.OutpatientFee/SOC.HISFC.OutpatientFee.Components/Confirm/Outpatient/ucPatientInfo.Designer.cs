namespace FS.SOC.HISFC.OutpatientFee.Components.Confirm.Controls
{
    partial class ucPatientInfo
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
            this.lbAccountVacancy = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDiagnose = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbSexAndAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPatientName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // lbAccountVacancy
            // 
            this.lbAccountVacancy.AutoSize = true;
            this.lbAccountVacancy.Location = new System.Drawing.Point(458, 9);
            this.lbAccountVacancy.Name = "lbAccountVacancy";
            this.lbAccountVacancy.Size = new System.Drawing.Size(89, 12);
            this.lbAccountVacancy.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAccountVacancy.TabIndex = 18;
            this.lbAccountVacancy.Text = "账户余额：0.00";
            // 
            // lbRegDate
            // 
            this.lbRegDate.AutoSize = true;
            this.lbRegDate.Location = new System.Drawing.Point(149, 31);
            this.lbRegDate.Name = "lbRegDate";
            this.lbRegDate.Size = new System.Drawing.Size(155, 12);
            this.lbRegDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDate.TabIndex = 17;
            this.lbRegDate.Text = "时间：2010-12-30 00:00:00";
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Location = new System.Drawing.Point(7, 31);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(101, 12);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 16;
            this.lbRegDept.Text = "挂  号：儿科门诊";
            // 
            // lbCardNO
            // 
            this.lbCardNO.AutoSize = true;
            this.lbCardNO.Location = new System.Drawing.Point(7, 9);
            this.lbCardNO.Name = "lbCardNO";
            this.lbCardNO.Size = new System.Drawing.Size(113, 12);
            this.lbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNO.TabIndex = 15;
            this.lbCardNO.Text = "病历号：0123456789";
            // 
            // lbPact
            // 
            this.lbPact.AutoSize = true;
            this.lbPact.Location = new System.Drawing.Point(327, 9);
            this.lbPact.Name = "lbPact";
            this.lbPact.Size = new System.Drawing.Size(89, 12);
            this.lbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPact.TabIndex = 13;
            this.lbPact.Text = "合同单位：自费";
            // 
            // lbDiagnose
            // 
            this.lbDiagnose.AutoSize = true;
            this.lbDiagnose.ForeColor = System.Drawing.Color.Blue;
            this.lbDiagnose.Location = new System.Drawing.Point(327, 31);
            this.lbDiagnose.Name = "lbDiagnose";
            this.lbDiagnose.Size = new System.Drawing.Size(41, 12);
            this.lbDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDiagnose.TabIndex = 12;
            this.lbDiagnose.Text = "诊断：";
            // 
            // nlbSexAndAge
            // 
            this.nlbSexAndAge.AutoSize = true;
            this.nlbSexAndAge.Location = new System.Drawing.Point(251, 9);
            this.nlbSexAndAge.Name = "nlbSexAndAge";
            this.nlbSexAndAge.Size = new System.Drawing.Size(0, 12);
            this.nlbSexAndAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbSexAndAge.TabIndex = 11;
            // 
            // lbPatientName
            // 
            this.lbPatientName.AutoSize = true;
            this.lbPatientName.Location = new System.Drawing.Point(149, 9);
            this.lbPatientName.Name = "lbPatientName";
            this.lbPatientName.Size = new System.Drawing.Size(125, 12);
            this.lbPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPatientName.TabIndex = 10;
            this.lbPatientName.Text = "患者：刘德华 男 20岁";
            // 
            // ucPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbAccountVacancy);
            this.Controls.Add(this.lbRegDate);
            this.Controls.Add(this.lbRegDept);
            this.Controls.Add(this.lbCardNO);
            this.Controls.Add(this.lbPact);
            this.Controls.Add(this.lbDiagnose);
            this.Controls.Add(this.nlbSexAndAge);
            this.Controls.Add(this.lbPatientName);
            this.Name = "ucPatientInfo";
            this.Size = new System.Drawing.Size(877, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAccountVacancy;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDate;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbDiagnose;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbSexAndAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbPatientName;
    }
}
