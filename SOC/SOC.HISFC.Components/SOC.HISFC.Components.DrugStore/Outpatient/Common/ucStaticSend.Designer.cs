namespace FS.SOC.HISFC.Components.DrugStore.Outpatient.Common
{
    partial class ucStaticSend
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
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nlbTermialInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbWorkLoadInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.ngbRecipeDetail.SuspendLayout();
            this.ngbAdd.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucRecipeDetail1
            // 
            this.ucRecipeDetail1.Size = new System.Drawing.Size(769, 455);
            // 
            // ucDrugTree1
            // 
            this.ucDrugTree1.Size = new System.Drawing.Size(233, 548);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Controls.SetChildIndex(this.neuGroupBox1, 0);
            this.neuPanel1.Controls.SetChildIndex(this.ucDrugTree1, 0);
            // 
            // ngbRecipeDetail
            // 
            this.ngbRecipeDetail.Location = new System.Drawing.Point(0, 73);
            this.ngbRecipeDetail.Size = new System.Drawing.Size(775, 475);
            // 
            // ngbAdd
            // 
            this.ngbAdd.Controls.Add(this.nlbWorkLoadInfo);
            this.ngbAdd.Controls.Add(this.nlbTermialInfo);
            this.ngbAdd.Location = new System.Drawing.Point(0, 548);
            this.ngbAdd.Size = new System.Drawing.Size(775, 94);
            // 
            // ngbPatientInfo
            // 
            this.ngbPatientInfo.Size = new System.Drawing.Size(775, 73);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(17, 23);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "开始时间：";
            // 
            // ndtpBegin
            // 
            this.ndtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpBegin.IsEnter2Tab = false;
            this.ndtpBegin.Location = new System.Drawing.Point(88, 19);
            this.ndtpBegin.Name = "ndtpBegin";
            this.ndtpBegin.Size = new System.Drawing.Size(138, 21);
            this.ndtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpBegin.TabIndex = 1;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(17, 50);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "结束时间：";
            // 
            // ndtpEnd
            // 
            this.ndtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpEnd.IsEnter2Tab = false;
            this.ndtpEnd.Location = new System.Drawing.Point(88, 46);
            this.ndtpEnd.Name = "ndtpEnd";
            this.ndtpEnd.Size = new System.Drawing.Size(138, 21);
            this.ndtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpEnd.TabIndex = 3;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ndtpEnd);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.ndtpBegin);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 548);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(233, 94);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 2;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "时间设置";
            // 
            // nlbTermialInfo
            // 
            this.nlbTermialInfo.AutoSize = true;
            this.nlbTermialInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbTermialInfo.Location = new System.Drawing.Point(16, 23);
            this.nlbTermialInfo.Name = "nlbTermialInfo";
            this.nlbTermialInfo.Size = new System.Drawing.Size(53, 12);
            this.nlbTermialInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTermialInfo.TabIndex = 1;
            this.nlbTermialInfo.Text = "终端信息";
            // 
            // nlbWorkLoadInfo
            // 
            this.nlbWorkLoadInfo.AutoSize = true;
            this.nlbWorkLoadInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbWorkLoadInfo.Location = new System.Drawing.Point(16, 50);
            this.nlbWorkLoadInfo.Name = "nlbWorkLoadInfo";
            this.nlbWorkLoadInfo.Size = new System.Drawing.Size(65, 12);
            this.nlbWorkLoadInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbWorkLoadInfo.TabIndex = 2;
            this.nlbWorkLoadInfo.Text = "工作量情况";
            // 
            // ucStaticSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucStaticSend";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.ngbRecipeDetail.ResumeLayout(false);
            this.ngbAdd.ResumeLayout(false);
            this.ngbAdd.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTermialInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbWorkLoadInfo;




    }
}
