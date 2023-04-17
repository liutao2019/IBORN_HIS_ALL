namespace FS.SOC.HISFC.Components.DrugStore.Outpatient.Common
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
            this.nlbPatientName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbSexAndAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbDiagnose = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInvoiceNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRecipeNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRecipeDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRegDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbFeeDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRecipDoctor = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbDrugOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // nlbPatientName
            // 
            this.nlbPatientName.AutoSize = true;
            this.nlbPatientName.Location = new System.Drawing.Point(153, 32);
            this.nlbPatientName.Name = "nlbPatientName";
            this.nlbPatientName.Size = new System.Drawing.Size(125, 12);
            this.nlbPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPatientName.TabIndex = 0;
            this.nlbPatientName.Text = "患者：刘德华 男 20岁";
            // 
            // nlbSexAndAge
            // 
            this.nlbSexAndAge.AutoSize = true;
            this.nlbSexAndAge.Location = new System.Drawing.Point(255, 32);
            this.nlbSexAndAge.Name = "nlbSexAndAge";
            this.nlbSexAndAge.Size = new System.Drawing.Size(0, 12);
            this.nlbSexAndAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbSexAndAge.TabIndex = 1;
            // 
            // nlbDiagnose
            // 
            this.nlbDiagnose.AutoSize = true;
            this.nlbDiagnose.ForeColor = System.Drawing.Color.Blue;
            this.nlbDiagnose.Location = new System.Drawing.Point(607, 7);
            this.nlbDiagnose.Name = "nlbDiagnose";
            this.nlbDiagnose.Size = new System.Drawing.Size(41, 12);
            this.nlbDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDiagnose.TabIndex = 2;
            this.nlbDiagnose.Text = "诊断：";
            // 
            // nlbInvoiceNO
            // 
            this.nlbInvoiceNO.AutoSize = true;
            this.nlbInvoiceNO.Location = new System.Drawing.Point(306, 32);
            this.nlbInvoiceNO.Name = "nlbInvoiceNO";
            this.nlbInvoiceNO.Size = new System.Drawing.Size(113, 12);
            this.nlbInvoiceNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInvoiceNO.TabIndex = 3;
            this.nlbInvoiceNO.Text = "发票：012345678912";
            // 
            // nlbRecipeNO
            // 
            this.nlbRecipeNO.AutoSize = true;
            this.nlbRecipeNO.Location = new System.Drawing.Point(11, 7);
            this.nlbRecipeNO.Name = "nlbRecipeNO";
            this.nlbRecipeNO.Size = new System.Drawing.Size(137, 12);
            this.nlbRecipeNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRecipeNO.TabIndex = 4;
            this.nlbRecipeNO.Text = "处方号：12345678901234";
            // 
            // nlbCardNO
            // 
            this.nlbCardNO.AutoSize = true;
            this.nlbCardNO.Location = new System.Drawing.Point(11, 32);
            this.nlbCardNO.Name = "nlbCardNO";
            this.nlbCardNO.Size = new System.Drawing.Size(113, 12);
            this.nlbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbCardNO.TabIndex = 5;
            this.nlbCardNO.Text = "病例号：0123456789";
            // 
            // nlbRecipeDept
            // 
            this.nlbRecipeDept.AutoSize = true;
            this.nlbRecipeDept.Location = new System.Drawing.Point(153, 7);
            this.nlbRecipeDept.Name = "nlbRecipeDept";
            this.nlbRecipeDept.Size = new System.Drawing.Size(89, 12);
            this.nlbRecipeDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRecipeDept.TabIndex = 6;
            this.nlbRecipeDept.Text = "开方：儿科门诊";
            // 
            // nlbRegDate
            // 
            this.nlbRegDate.AutoSize = true;
            this.nlbRegDate.Location = new System.Drawing.Point(437, 32);
            this.nlbRegDate.Name = "nlbRegDate";
            this.nlbRegDate.Size = new System.Drawing.Size(155, 12);
            this.nlbRegDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRegDate.TabIndex = 7;
            this.nlbRegDate.Text = "挂号：2010-12-30 00:00:00";
            // 
            // nlbFeeDate
            // 
            this.nlbFeeDate.AutoSize = true;
            this.nlbFeeDate.Location = new System.Drawing.Point(607, 32);
            this.nlbFeeDate.Name = "nlbFeeDate";
            this.nlbFeeDate.Size = new System.Drawing.Size(155, 12);
            this.nlbFeeDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbFeeDate.TabIndex = 8;
            this.nlbFeeDate.Text = "收费：2010-12-30 00:00:00";
            // 
            // nlbRecipDoctor
            // 
            this.nlbRecipDoctor.AutoSize = true;
            this.nlbRecipDoctor.Location = new System.Drawing.Point(306, 7);
            this.nlbRecipDoctor.Name = "nlbRecipDoctor";
            this.nlbRecipDoctor.Size = new System.Drawing.Size(77, 12);
            this.nlbRecipDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRecipDoctor.TabIndex = 9;
            this.nlbRecipDoctor.Text = "医生：张学友";
            // 
            // nlbDrugOper
            // 
            this.nlbDrugOper.AutoSize = true;
            this.nlbDrugOper.Location = new System.Drawing.Point(425, 7);
            this.nlbDrugOper.Name = "nlbDrugOper";
            this.nlbDrugOper.Size = new System.Drawing.Size(89, 12);
            this.nlbDrugOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDrugOper.TabIndex = 10;
            this.nlbDrugOper.Text = "配药员：刘德华";
            // 
            // ucPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nlbDrugOper);
            this.Controls.Add(this.nlbRecipDoctor);
            this.Controls.Add(this.nlbFeeDate);
            this.Controls.Add(this.nlbRegDate);
            this.Controls.Add(this.nlbRecipeDept);
            this.Controls.Add(this.nlbCardNO);
            this.Controls.Add(this.nlbRecipeNO);
            this.Controls.Add(this.nlbInvoiceNO);
            this.Controls.Add(this.nlbDiagnose);
            this.Controls.Add(this.nlbSexAndAge);
            this.Controls.Add(this.nlbPatientName);
            this.Name = "ucPatientInfo";
            this.Size = new System.Drawing.Size(964, 56);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbPatientName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbSexAndAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbDiagnose;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInvoiceNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRecipeNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRecipeDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRegDate;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbFeeDate;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRecipDoctor;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbDrugOper;
    }
}
