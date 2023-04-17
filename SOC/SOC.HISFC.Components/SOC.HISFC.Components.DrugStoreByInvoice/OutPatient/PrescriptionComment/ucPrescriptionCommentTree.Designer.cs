namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.PrescriptionComment
{
    partial class ucPrescriptionCommentTree
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
            this.tvBaseTree1 = new SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree(this.components);
            this.nlbBeginTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nblEndTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuDateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.nlbDoct = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nDoct = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuPanel1.SuspendLayout();
            this.ngbQueryRecipe.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ngbQueryRecipe);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(231, 147);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ngbQueryRecipe
            // 
            this.ngbQueryRecipe.Controls.Add(this.nDoct);
            this.ngbQueryRecipe.Controls.Add(this.neuLabel2);
            this.ngbQueryRecipe.Controls.Add(this.neuLabel1);
            this.ngbQueryRecipe.Controls.Add(this.ntxtBillNO);
            this.ngbQueryRecipe.Controls.Add(this.nlbQueryType);
            this.ngbQueryRecipe.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbQueryRecipe.Location = new System.Drawing.Point(0, 0);
            this.ngbQueryRecipe.Name = "ngbQueryRecipe";
            this.ngbQueryRecipe.Size = new System.Drawing.Size(231, 147);
            this.ngbQueryRecipe.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbQueryRecipe.TabIndex = 0;
            this.ngbQueryRecipe.TabStop = false;
            this.ngbQueryRecipe.Text = "处方查找";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(202, 47);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(23, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 8;
            this.neuLabel2.Text = "F10";
            // 
            // neuLabel1
            // 
            this.neuLabel1.ForeColor = System.Drawing.Color.Black;
            this.neuLabel1.Location = new System.Drawing.Point(14, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(209, 24);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "单击发票号下面蓝色字或按F9可更改查询依据";
            // 
            // ntxtBillNO
            // 
            this.ntxtBillNO.IsEnter2Tab = false;
            this.ntxtBillNO.Location = new System.Drawing.Point(68, 44);
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
            this.neuPanel2.Location = new System.Drawing.Point(0, 149);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(231, 479);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Location = new System.Drawing.Point(0, 4);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(231, 460);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvBaseTree1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(223, 435);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "处方列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvBaseTree1
            // 
            this.tvBaseTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvBaseTree1.HideSelection = false;
            this.tvBaseTree1.ImageIndex = 0;
            this.tvBaseTree1.Location = new System.Drawing.Point(3, 3);
            this.tvBaseTree1.Name = "tvBaseTree1";
            this.tvBaseTree1.SelectedImageIndex = 0;
            this.tvBaseTree1.Size = new System.Drawing.Size(217, 429);
            this.tvBaseTree1.State = "0";
            this.tvBaseTree1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvBaseTree1.TabIndex = 0;
            // 
            // nlbBeginTime
            // 
            this.nlbBeginTime.AutoSize = true;
            this.nlbBeginTime.ForeColor = System.Drawing.Color.Blue;
            this.nlbBeginTime.Location = new System.Drawing.Point(2, 72);
            this.nlbBeginTime.Name = "nlbBeginTime";
            this.nlbBeginTime.Size = new System.Drawing.Size(65, 12);
            this.nlbBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbBeginTime.TabIndex = 1;
            this.nlbBeginTime.Text = "开始时间：";
            // 
            // nblEndTime
            // 
            this.nblEndTime.AutoSize = true;
            this.nblEndTime.ForeColor = System.Drawing.Color.Blue;
            this.nblEndTime.Location = new System.Drawing.Point(2, 94);
            this.nblEndTime.Name = "nblEndTime";
            this.nblEndTime.Size = new System.Drawing.Size(65, 12);
            this.nblEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nblEndTime.TabIndex = 5;
            this.nblEndTime.Text = "结束时间：";
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(68, 68);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(159, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 6;
            // 
            // neuDateTimePicker2
            // 
            this.neuDateTimePicker2.IsEnter2Tab = false;
            this.neuDateTimePicker2.Location = new System.Drawing.Point(68, 90);
            this.neuDateTimePicker2.Name = "neuDateTimePicker2";
            this.neuDateTimePicker2.Size = new System.Drawing.Size(160, 21);
            this.neuDateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker2.TabIndex = 7;
            // 
            // nlbDoct
            // 
            this.nlbDoct.AutoSize = true;
            this.nlbDoct.ForeColor = System.Drawing.Color.Blue;
            this.nlbDoct.Location = new System.Drawing.Point(26, 114);
            this.nlbDoct.Name = "nlbDoct";
            this.nlbDoct.Size = new System.Drawing.Size(41, 12);
            this.nlbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDoct.TabIndex = 8;
            this.nlbDoct.Text = "医生：";
            // 
            // nDoct
            // 
            this.nDoct.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.nDoct.FormattingEnabled = true;
            this.nDoct.IsEnter2Tab = false;
            this.nDoct.IsFlat = false;
            this.nDoct.IsLike = true;
            this.nDoct.IsListOnly = false;
            this.nDoct.IsPopForm = true;
            this.nDoct.IsShowCustomerList = false;
            this.nDoct.IsShowID = false;
            this.nDoct.Location = new System.Drawing.Point(68, 112);
            this.nDoct.Name = "nDoct";
            this.nDoct.PopForm = null;
            this.nDoct.ShowCustomerList = false;
            this.nDoct.ShowID = false;
            this.nDoct.Size = new System.Drawing.Size(122, 20);
            this.nDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.nDoct.TabIndex = 9;
            this.nDoct.Tag = "";
            this.nDoct.ToolBarUse = false;
            // 
            // ucPrescriptionCommentTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nlbDoct);
            this.Controls.Add(this.neuDateTimePicker2);
            this.Controls.Add(this.neuDateTimePicker1);
            this.Controls.Add(this.nblEndTime);
            this.Controls.Add(this.nlbBeginTime);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPrescriptionCommentTree";
            this.Size = new System.Drawing.Size(231, 632);
            this.neuPanel1.ResumeLayout(false);
            this.ngbQueryRecipe.ResumeLayout(false);
            this.ngbQueryRecipe.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree tvBaseTree1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox ngbQueryRecipe;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtBillNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbQueryType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel nblEndTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbDoct;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox nDoct;
    }
}