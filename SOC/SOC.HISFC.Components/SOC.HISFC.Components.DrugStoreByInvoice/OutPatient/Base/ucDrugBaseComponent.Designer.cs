namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base
{
    partial class ucDrugBaseComponent
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
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ngbRecipeDetail = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ngbAdd = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ngbPatientInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(233, 642);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 5;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(233, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 642);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 6;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.ngbRecipeDetail);
            this.neuPanel2.Controls.Add(this.ngbAdd);
            this.neuPanel2.Controls.Add(this.ngbPatientInfo);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(236, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(775, 642);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 7;
            // 
            // ngbRecipeDetail
            // 
            this.ngbRecipeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ngbRecipeDetail.Location = new System.Drawing.Point(0, 73);
            this.ngbRecipeDetail.Name = "ngbRecipeDetail";
            this.ngbRecipeDetail.Size = new System.Drawing.Size(775, 469);
            this.ngbRecipeDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbRecipeDetail.TabIndex = 11;
            this.ngbRecipeDetail.TabStop = false;
            this.ngbRecipeDetail.Text = "处方明细";
            // 
            // ngbAdd
            // 
            this.ngbAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ngbAdd.Location = new System.Drawing.Point(0, 542);
            this.ngbAdd.Name = "ngbAdd";
            this.ngbAdd.Size = new System.Drawing.Size(775, 100);
            this.ngbAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbAdd.TabIndex = 6;
            this.ngbAdd.TabStop = false;
            this.ngbAdd.Text = "附加信息";
            // 
            // ngbPatientInfo
            // 
            this.ngbPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbPatientInfo.Location = new System.Drawing.Point(0, 0);
            this.ngbPatientInfo.Name = "ngbPatientInfo";
            this.ngbPatientInfo.Size = new System.Drawing.Size(775, 73);
            this.ngbPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbPatientInfo.TabIndex = 5;
            this.ngbPatientInfo.TabStop = false;
            this.ngbPatientInfo.Text = "基本信息";
            // 
            // ucDrugBaseComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucDrugBaseComponent";
            this.Size = new System.Drawing.Size(1011, 642);
            this.neuPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbAdd;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbPatientInfo;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbRecipeDetail;

    }
}