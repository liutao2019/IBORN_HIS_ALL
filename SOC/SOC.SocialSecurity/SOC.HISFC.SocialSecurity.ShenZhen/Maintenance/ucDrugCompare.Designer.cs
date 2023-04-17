namespace FS.SOC.HISFC.SocialSecurity.ShenZhen.Maintenance
{
    partial class ucDrugCompare
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ntxtDrugInfo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.nlbSocialSecrityInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbFilter = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtSocialSecrityInfo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ncbRefreshAfterSave = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Size = new System.Drawing.Size(714, 392);
            this.splitContainer1.SplitterDistance = 44;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.neuGroupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(725, 492);
            this.splitContainer2.SplitterDistance = 44;
            this.splitContainer2.TabIndex = 1;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.ntxtDrugInfo);
            this.neuGroupBox2.Controls.Add(this.nlbSocialSecrityInfo);
            this.neuGroupBox2.Controls.Add(this.nlbFilter);
            this.neuGroupBox2.Controls.Add(this.ntxtSocialSecrityInfo);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(725, 44);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 9;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "查找过滤";
            // 
            // ntxtDrugInfo
            // 
            this.ntxtDrugInfo.IsEnter2Tab = false;
            this.ntxtDrugInfo.Location = new System.Drawing.Point(68, 16);
            this.ntxtDrugInfo.Name = "ntxtDrugInfo";
            this.ntxtDrugInfo.Size = new System.Drawing.Size(200, 21);
            this.ntxtDrugInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtDrugInfo.TabIndex = 13;
            // 
            // nlbSocialSecrityInfo
            // 
            this.nlbSocialSecrityInfo.AutoSize = true;
            this.nlbSocialSecrityInfo.Location = new System.Drawing.Point(303, 19);
            this.nlbSocialSecrityInfo.Name = "nlbSocialSecrityInfo";
            this.nlbSocialSecrityInfo.Size = new System.Drawing.Size(65, 12);
            this.nlbSocialSecrityInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbSocialSecrityInfo.TabIndex = 12;
            this.nlbSocialSecrityInfo.Text = "医保编码：";
            // 
            // nlbFilter
            // 
            this.nlbFilter.AutoSize = true;
            this.nlbFilter.Location = new System.Drawing.Point(21, 19);
            this.nlbFilter.Name = "nlbFilter";
            this.nlbFilter.Size = new System.Drawing.Size(41, 12);
            this.nlbFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbFilter.TabIndex = 10;
            this.nlbFilter.Text = "药品：";
            // 
            // ntxtSocialSecrityInfo
            // 
            this.ntxtSocialSecrityInfo.BackColor = System.Drawing.Color.White;
            this.ntxtSocialSecrityInfo.IsEnter2Tab = false;
            this.ntxtSocialSecrityInfo.Location = new System.Drawing.Point(374, 16);
            this.ntxtSocialSecrityInfo.Name = "ntxtSocialSecrityInfo";
            this.ntxtSocialSecrityInfo.Size = new System.Drawing.Size(125, 21);
            this.ntxtSocialSecrityInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtSocialSecrityInfo.TabIndex = 9;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.neuGroupBox3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.neuGroupBox1);
            this.splitContainer3.Size = new System.Drawing.Size(725, 444);
            this.splitContainer3.SplitterDistance = 385;
            this.splitContainer3.TabIndex = 0;
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.fpSpread1);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(725, 385);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 0;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "对照信息";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.EditModeReplace = true;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 17);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(719, 365);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 3;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 29F;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.ncbRefreshAfterSave);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(725, 55);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "其它信息";
            // 
            // ncbRefreshAfterSave
            // 
            this.ncbRefreshAfterSave.AutoSize = true;
            this.ncbRefreshAfterSave.Location = new System.Drawing.Point(23, 23);
            this.ncbRefreshAfterSave.Name = "ncbRefreshAfterSave";
            this.ncbRefreshAfterSave.Size = new System.Drawing.Size(132, 16);
            this.ncbRefreshAfterSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbRefreshAfterSave.TabIndex = 0;
            this.ncbRefreshAfterSave.Text = "保存后自动刷新数据";
            this.ncbRefreshAfterSave.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(224, 24);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(401, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 13;
            this.neuLabel1.Text = "保存时药品编码和对照编码必须同时维护好，系统不保存不完整的对照信息";
            // 
            // ucDrugCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "ucDrugCompare";
            this.Size = new System.Drawing.Size(725, 492);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.neuGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbSocialSecrityInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbFilter;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtSocialSecrityInfo;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtDrugInfo;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbRefreshAfterSave;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}
