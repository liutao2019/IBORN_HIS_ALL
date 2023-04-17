namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common
{
    partial class ucRecipeTree
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ngbQueryRecipe = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtBillNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.nlbQueryType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tvBaseTree1 = new SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree(this.components);
            this.tvBaseTree2 = new SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree(this.components);
            this.neuPanel1.SuspendLayout();
            this.ngbQueryRecipe.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ngbQueryRecipe);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(231, 73);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ngbQueryRecipe
            // 
            this.ngbQueryRecipe.Controls.Add(this.neuLabel2);
            this.ngbQueryRecipe.Controls.Add(this.neuLabel1);
            this.ngbQueryRecipe.Controls.Add(this.ntxtBillNO);
            this.ngbQueryRecipe.Controls.Add(this.nlbQueryType);
            this.ngbQueryRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ngbQueryRecipe.Location = new System.Drawing.Point(0, 0);
            this.ngbQueryRecipe.Name = "ngbQueryRecipe";
            this.ngbQueryRecipe.Size = new System.Drawing.Size(231, 73);
            this.ngbQueryRecipe.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbQueryRecipe.TabIndex = 0;
            this.ngbQueryRecipe.TabStop = false;
            this.ngbQueryRecipe.Text = "处方查找";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(197, 47);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(23, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 8;
            this.neuLabel2.Text = "F10";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Black;
            this.neuLabel1.Location = new System.Drawing.Point(12, 23);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(209, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "单击下面蓝色字或按F9可更改查询依据";
            // 
            // ntxtBillNO
            // 
            this.ntxtBillNO.IsEnter2Tab = false;
            this.ntxtBillNO.Location = new System.Drawing.Point(71, 43);
            this.ntxtBillNO.Name = "ntxtBillNO";
            this.ntxtBillNO.Size = new System.Drawing.Size(120, 21);
            this.ntxtBillNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtBillNO.TabIndex = 5;
            // 
            // nlbQueryType
            // 
            this.nlbQueryType.AutoSize = true;
            this.nlbQueryType.ForeColor = System.Drawing.Color.Blue;
            this.nlbQueryType.Location = new System.Drawing.Point(14, 47);
            this.nlbQueryType.Name = "nlbQueryType";
            this.nlbQueryType.Size = new System.Drawing.Size(53, 12);
            this.nlbQueryType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbQueryType.TabIndex = 6;
            this.nlbQueryType.Text = "发票号：";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuTabControl1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 73);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(231, 559);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Controls.Add(this.tabPage2);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(231, 559);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvBaseTree1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(223, 534);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "未打印/未配药";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tvBaseTree2);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(223, 534);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "已配药/已发药";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tvBaseTree1
            // 
            this.tvBaseTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvBaseTree1.HideSelection = false;
            this.tvBaseTree1.ImageIndex = 0;
            this.tvBaseTree1.Location = new System.Drawing.Point(3, 3);
            this.tvBaseTree1.Name = "tvBaseTree1";
            this.tvBaseTree1.SelectedImageIndex = 0;
            this.tvBaseTree1.Size = new System.Drawing.Size(217, 528);
            this.tvBaseTree1.State = "0";
            this.tvBaseTree1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvBaseTree1.TabIndex = 0;
            // 
            // tvBaseTree2
            // 
            this.tvBaseTree2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvBaseTree2.HideSelection = false;
            this.tvBaseTree2.ImageIndex = 0;
            this.tvBaseTree2.Location = new System.Drawing.Point(3, 3);
            this.tvBaseTree2.Name = "tvBaseTree2";
            this.tvBaseTree2.SelectedImageIndex = 0;
            this.tvBaseTree2.Size = new System.Drawing.Size(217, 528);
            this.tvBaseTree2.State = "0";
            this.tvBaseTree2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvBaseTree2.TabIndex = 1;
            // 
            // ucRecipeTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucRecipeTree";
            this.Size = new System.Drawing.Size(231, 632);
            this.neuPanel1.ResumeLayout(false);
            this.ngbQueryRecipe.ResumeLayout(false);
            this.ngbQueryRecipe.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree tvBaseTree1;
        private System.Windows.Forms.TabPage tabPage2;
        private SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree tvBaseTree2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox ngbQueryRecipe;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtBillNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbQueryType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
    }
}