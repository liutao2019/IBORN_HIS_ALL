namespace FS.SOC.HISFC.Components.DrugStore.Outpatient.Common
{
    partial class ucRecipeQuery
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
            this.ngbQueryRecipe = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.ndtpBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtPatientName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtInvoiceNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtRecipeNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ntvRecipeTree = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.nlbDeptInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTermialInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.ngbAdd.SuspendLayout();
            this.ngbQueryRecipe.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ntvRecipeTree);
            this.neuPanel1.Controls.Add(this.ngbQueryRecipe);
            this.neuPanel1.Size = new System.Drawing.Size(279, 546);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(279, 0);
            this.neuSplitter1.Size = new System.Drawing.Size(3, 546);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Location = new System.Drawing.Point(282, 0);
            this.neuPanel2.Size = new System.Drawing.Size(448, 546);
            // 
            // ngbAdd
            // 
            this.ngbAdd.Controls.Add(this.nlbTermialInfo);
            this.ngbAdd.Controls.Add(this.nlbDeptInfo);
            this.ngbAdd.Location = new System.Drawing.Point(0, 486);
            this.ngbAdd.Size = new System.Drawing.Size(448, 60);
            // 
            // ngbPatientInfo
            // 
            this.ngbPatientInfo.Size = new System.Drawing.Size(448, 90);
            // 
            // ngbRecipeDetail
            // 
            this.ngbRecipeDetail.Location = new System.Drawing.Point(0, 90);
            this.ngbRecipeDetail.Size = new System.Drawing.Size(448, 396);
            // 
            // ngbQueryRecipe
            // 
            this.ngbQueryRecipe.Controls.Add(this.neuLabel6);
            this.ngbQueryRecipe.Controls.Add(this.ndtpEndTime);
            this.ngbQueryRecipe.Controls.Add(this.ndtpBeginTime);
            this.ngbQueryRecipe.Controls.Add(this.neuLabel4);
            this.ngbQueryRecipe.Controls.Add(this.ntxtPatientName);
            this.ngbQueryRecipe.Controls.Add(this.neuLabel3);
            this.ngbQueryRecipe.Controls.Add(this.ntxtCardNO);
            this.ngbQueryRecipe.Controls.Add(this.neuLabel2);
            this.ngbQueryRecipe.Controls.Add(this.ntxtInvoiceNO);
            this.ngbQueryRecipe.Controls.Add(this.neuLabel1);
            this.ngbQueryRecipe.Controls.Add(this.ntxtRecipeNO);
            this.ngbQueryRecipe.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbQueryRecipe.Location = new System.Drawing.Point(0, 0);
            this.ngbQueryRecipe.Name = "ngbQueryRecipe";
            this.ngbQueryRecipe.Size = new System.Drawing.Size(279, 224);
            this.ngbQueryRecipe.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbQueryRecipe.TabIndex = 1;
            this.ngbQueryRecipe.TabStop = false;
            this.ngbQueryRecipe.Text = "处方查找";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(19, 164);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(53, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 15;
            this.neuLabel6.Text = "时  间：";
            // 
            // ndtpEndTime
            // 
            this.ndtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpEndTime.IsEnter2Tab = false;
            this.ndtpEndTime.Location = new System.Drawing.Point(78, 191);
            this.ndtpEndTime.Name = "ndtpEndTime";
            this.ndtpEndTime.Size = new System.Drawing.Size(185, 21);
            this.ndtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpEndTime.TabIndex = 14;
            // 
            // ndtpBeginTime
            // 
            this.ndtpBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpBeginTime.IsEnter2Tab = false;
            this.ndtpBeginTime.Location = new System.Drawing.Point(78, 158);
            this.ndtpBeginTime.Name = "ndtpBeginTime";
            this.ndtpBeginTime.Size = new System.Drawing.Size(185, 21);
            this.ndtpBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpBeginTime.TabIndex = 13;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(19, 128);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(53, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 12;
            this.neuLabel4.Text = "姓  名：";
            // 
            // ntxtPatientName
            // 
            this.ntxtPatientName.IsEnter2Tab = true;
            this.ntxtPatientName.Location = new System.Drawing.Point(78, 125);
            this.ntxtPatientName.Name = "ntxtPatientName";
            this.ntxtPatientName.Size = new System.Drawing.Size(185, 21);
            this.ntxtPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtPatientName.TabIndex = 11;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(19, 95);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 10;
            this.neuLabel3.Text = "病历号：";
            // 
            // ntxtCardNO
            // 
            this.ntxtCardNO.IsEnter2Tab = true;
            this.ntxtCardNO.Location = new System.Drawing.Point(78, 92);
            this.ntxtCardNO.Name = "ntxtCardNO";
            this.ntxtCardNO.Size = new System.Drawing.Size(185, 21);
            this.ntxtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtCardNO.TabIndex = 9;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(19, 61);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 8;
            this.neuLabel2.Text = "发票号：";
            // 
            // ntxtInvoiceNO
            // 
            this.ntxtInvoiceNO.IsEnter2Tab = true;
            this.ntxtInvoiceNO.Location = new System.Drawing.Point(78, 58);
            this.ntxtInvoiceNO.Name = "ntxtInvoiceNO";
            this.ntxtInvoiceNO.Size = new System.Drawing.Size(185, 21);
            this.ntxtInvoiceNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtInvoiceNO.TabIndex = 7;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(19, 28);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "处方号：";
            // 
            // ntxtRecipeNO
            // 
            this.ntxtRecipeNO.IsEnter2Tab = true;
            this.ntxtRecipeNO.Location = new System.Drawing.Point(78, 25);
            this.ntxtRecipeNO.Name = "ntxtRecipeNO";
            this.ntxtRecipeNO.Size = new System.Drawing.Size(185, 21);
            this.ntxtRecipeNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtRecipeNO.TabIndex = 5;
            // 
            // ntvRecipeTree
            // 
            this.ntvRecipeTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntvRecipeTree.HideSelection = false;
            this.ntvRecipeTree.Location = new System.Drawing.Point(0, 224);
            this.ntvRecipeTree.Name = "ntvRecipeTree";
            this.ntvRecipeTree.Size = new System.Drawing.Size(279, 322);
            this.ntvRecipeTree.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntvRecipeTree.TabIndex = 2;
            // 
            // nlbDeptInfo
            // 
            this.nlbDeptInfo.AutoSize = true;
            this.nlbDeptInfo.Location = new System.Drawing.Point(281, 28);
            this.nlbDeptInfo.Name = "nlbDeptInfo";
            this.nlbDeptInfo.Size = new System.Drawing.Size(161, 12);
            this.nlbDeptInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDeptInfo.TabIndex = 16;
            this.nlbDeptInfo.Text = "您可以查询登录科室内的处方";
            // 
            // nlbTermialInfo
            // 
            this.nlbTermialInfo.AutoSize = true;
            this.nlbTermialInfo.Location = new System.Drawing.Point(23, 28);
            this.nlbTermialInfo.Name = "nlbTermialInfo";
            this.nlbTermialInfo.Size = new System.Drawing.Size(11, 12);
            this.nlbTermialInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTermialInfo.TabIndex = 17;
            this.nlbTermialInfo.Text = "*";
            // 
            // ucRecipeQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucRecipeQuery";
            this.Size = new System.Drawing.Size(730, 546);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.ngbAdd.ResumeLayout(false);
            this.ngbAdd.PerformLayout();
            this.ngbQueryRecipe.ResumeLayout(false);
            this.ngbQueryRecipe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox ngbQueryRecipe;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtCardNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtInvoiceNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtRecipeNO;
        private FS.FrameWork.WinForms.Controls.NeuTreeView ntvRecipeTree;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpEndTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbDeptInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTermialInfo;
    }
}
