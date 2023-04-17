namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    partial class ucDrugBill
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ndtpDrugedDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.ntxtDrugedBillNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.nbtBillNOOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtDateOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucDrugDetail1 = new FS.SOC.HISFC.Components.DrugStore.Inpatient.Common.ucDrugDetail();
            this.tvMessageBaseTree1 = new FS.SOC.HISFC.Components.DrugStore.Inpatient.Base.tvMessageBaseTree(this.components);
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ngbQuerySet.SuspendLayout();
            this.npanelDrugMessage.SuspendLayout();
            this.ngbDrugDetail.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Controls.Add(this.nlbInfo);
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 653);
            this.ngbQuerySet.Size = new System.Drawing.Size(1171, 62);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Size = new System.Drawing.Size(1171, 3);
            // 
            // npanelDrugMessage
            // 
            this.npanelDrugMessage.Controls.Add(this.tvMessageBaseTree1);
            this.npanelDrugMessage.Controls.Add(this.neuGroupBox1);
            this.npanelDrugMessage.Size = new System.Drawing.Size(245, 650);
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Location = new System.Drawing.Point(245, 3);
            this.neuSplitter2.Size = new System.Drawing.Size(3, 650);
            // 
            // ngbDrugDetail
            // 
            this.ngbDrugDetail.Controls.Add(this.ucDrugDetail1);
            this.ngbDrugDetail.Location = new System.Drawing.Point(248, 3);
            this.ngbDrugDetail.Size = new System.Drawing.Size(923, 650);
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ndtpDrugedDate);
            this.neuGroupBox1.Controls.Add(this.ntxtDrugedBillNO);
            this.neuGroupBox1.Controls.Add(this.nbtBillNOOK);
            this.neuGroupBox1.Controls.Add(this.nbtDateOK);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(245, 59);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "摆药单";
            // 
            // ndtpDrugedDate
            // 
            this.ndtpDrugedDate.IsEnter2Tab = false;
            this.ndtpDrugedDate.Location = new System.Drawing.Point(84, 12);
            this.ndtpDrugedDate.Name = "ndtpDrugedDate";
            this.ndtpDrugedDate.Size = new System.Drawing.Size(109, 21);
            this.ndtpDrugedDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpDrugedDate.TabIndex = 1;
            // 
            // ntxtDrugedBillNO
            // 
            this.ntxtDrugedBillNO.IsEnter2Tab = false;
            this.ntxtDrugedBillNO.Location = new System.Drawing.Point(84, 35);
            this.ntxtDrugedBillNO.Name = "ntxtDrugedBillNO";
            this.ntxtDrugedBillNO.Size = new System.Drawing.Size(109, 21);
            this.ntxtDrugedBillNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtDrugedBillNO.TabIndex = 2;
            // 
            // nbtBillNOOK
            // 
            this.nbtBillNOOK.Location = new System.Drawing.Point(194, 34);
            this.nbtBillNOOK.Name = "nbtBillNOOK";
            this.nbtBillNOOK.Size = new System.Drawing.Size(38, 23);
            this.nbtBillNOOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtBillNOOK.TabIndex = 5;
            this.nbtBillNOOK.Text = "OK";
            this.nbtBillNOOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtBillNOOK.UseVisualStyleBackColor = true;
            // 
            // nbtDateOK
            // 
            this.nbtDateOK.Location = new System.Drawing.Point(194, 11);
            this.nbtDateOK.Name = "nbtDateOK";
            this.nbtDateOK.Size = new System.Drawing.Size(38, 23);
            this.nbtDateOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtDateOK.TabIndex = 4;
            this.nbtDateOK.Text = "OK";
            this.nbtDateOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtDateOK.UseVisualStyleBackColor = true;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(19, 40);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "摆药单号：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(19, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "摆药日期：";
            // 
            // ucDrugDetail1
            // 
            this.ucDrugDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDrugDetail1.Location = new System.Drawing.Point(3, 17);
            this.ucDrugDetail1.Name = "ucDrugDetail1";
            this.ucDrugDetail1.Size = new System.Drawing.Size(917, 630);
            this.ucDrugDetail1.TabIndex = 0;
            // 
            // tvMessageBaseTree1
            // 
            this.tvMessageBaseTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMessageBaseTree1.HideSelection = false;
            this.tvMessageBaseTree1.ImageIndex = 0;
            this.tvMessageBaseTree1.Location = new System.Drawing.Point(0, 59);
            this.tvMessageBaseTree1.Name = "tvMessageBaseTree1";
            this.tvMessageBaseTree1.SelectedImageIndex = 0;
            this.tvMessageBaseTree1.Size = new System.Drawing.Size(245, 591);
            this.tvMessageBaseTree1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvMessageBaseTree1.TabIndex = 36;
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo.Location = new System.Drawing.Point(31, 29);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(101, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 1;
            this.nlbInfo.Text = "你选择的科室是：";
            // 
            // ucDrugBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDrugBill";
            this.Size = new System.Drawing.Size(1171, 715);
            this.ngbQuerySet.ResumeLayout(false);
            this.ngbQuerySet.PerformLayout();
            this.npanelDrugMessage.ResumeLayout(false);
            this.ngbDrugDetail.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private ucDrugDetail ucDrugDetail1;
        private FS.SOC.HISFC.Components.DrugStore.Inpatient.Base.tvMessageBaseTree tvMessageBaseTree1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtDrugedBillNO;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpDrugedDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtBillNOOK;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtDateOK;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;


    }
}
