namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemCompare
{
    partial class ucExecDeptManager
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtOriginalFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox4 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.sheetView2 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtCompareFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.neuGroupBox1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuGroupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView2)).BeginInit();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuGroupBox3);
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(309, 468);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.neuSpread);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox3.Font = new System.Drawing.Font("宋体", 1F);
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 49);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(309, 419);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 3;
            this.neuGroupBox3.TabStop = false;
            // 
            // neuSpread
            // 
            this.neuSpread.About = "3.0.2004.2005";
            this.neuSpread.AccessibleDescription = "neuSpread, Sheet1, Row 0, Column 0, ";
            this.neuSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread.Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread.Location = new System.Drawing.Point(3, 5);
            this.neuSpread.Name = "neuSpread";
            this.neuSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.neuSpread.Size = new System.Drawing.Size(303, 411);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance1;
            this.neuSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 0;
            this.sheetView1.RowCount = 0;
            this.sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 25F;
            this.sheetView1.DefaultStyle.Locked = false;
            this.sheetView1.DefaultStyle.Parent = "DataAreaDefault";
            this.sheetView1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.sheetView1.RowHeader.Columns.Default.Resizable = true;
            this.sheetView1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.Rows.Default.Height = 22F;
            this.sheetView1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView1.SheetCornerStyle.Locked = false;
            this.sheetView1.SheetCornerStyle.Parent = "HeaderDefault";
            this.sheetView1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread.SetActiveViewport(0, 1, 1);
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.txtOriginalFilter);
            this.neuGroupBox1.Controls.Add(this.neuLabel5);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(309, 49);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 2;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询设置";
            // 
            // txtOriginalFilter
            // 
            this.txtOriginalFilter.ForeColor = System.Drawing.Color.Black;
            this.txtOriginalFilter.IsEnter2Tab = false;
            this.txtOriginalFilter.Location = new System.Drawing.Point(114, 21);
            this.txtOriginalFilter.Name = "txtOriginalFilter";
            this.txtOriginalFilter.Size = new System.Drawing.Size(111, 21);
            this.txtOriginalFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOriginalFilter.TabIndex = 2;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel5.Location = new System.Drawing.Point(17, 25);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(89, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 1;
            this.neuLabel5.Text = "名称编码过滤：";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(309, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(5, 468);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuGroupBox4);
            this.neuPanel2.Controls.Add(this.neuGroupBox2);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(314, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(612, 468);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // neuGroupBox4
            // 
            this.neuGroupBox4.Controls.Add(this.groupBox1);
            this.neuGroupBox4.Controls.Add(this.fpSpread1);
            this.neuGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox4.Font = new System.Drawing.Font("宋体", 1F);
            this.neuGroupBox4.Location = new System.Drawing.Point(0, 49);
            this.neuGroupBox4.Name = "neuGroupBox4";
            this.neuGroupBox4.Size = new System.Drawing.Size(612, 419);
            this.neuGroupBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox4.TabIndex = 4;
            this.neuGroupBox4.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.groupBox1.Location = new System.Drawing.Point(206, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 224);
            this.groupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 197);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label1.TabIndex = 0;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            this.fpSpread1.Font = new System.Drawing.Font("宋体", 9F);
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 5);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView2});
            this.fpSpread1.Size = new System.Drawing.Size(606, 411);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 6;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // sheetView2
            // 
            this.sheetView2.Reset();
            this.sheetView2.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView2.ColumnCount = 6;
            this.sheetView2.RowCount = 1;
            this.sheetView2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView2.ColumnHeader.Rows.Get(0).Height = 25F;
            this.sheetView2.DefaultStyle.Locked = false;
            this.sheetView2.DefaultStyle.Parent = "DataAreaDefault";
            this.sheetView2.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView2.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.sheetView2.RowHeader.Columns.Default.Resizable = true;
            this.sheetView2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView2.Rows.Default.Height = 22F;
            this.sheetView2.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView2.SheetCornerStyle.Locked = false;
            this.sheetView2.SheetCornerStyle.Parent = "HeaderDefault";
            this.sheetView2.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.txtCompareFilter);
            this.neuGroupBox2.Controls.Add(this.neuLabel1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(612, 49);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 2;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "查询设置";
            // 
            // txtCompareFilter
            // 
            this.txtCompareFilter.ForeColor = System.Drawing.Color.Black;
            this.txtCompareFilter.IsEnter2Tab = false;
            this.txtCompareFilter.Location = new System.Drawing.Point(114, 21);
            this.txtCompareFilter.Name = "txtCompareFilter";
            this.txtCompareFilter.Size = new System.Drawing.Size(111, 21);
            this.txtCompareFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCompareFilter.TabIndex = 2;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(17, 25);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(89, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "名称编码过滤：";
            // 
            // ucExecDeptManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucExecDeptManager";
            this.Size = new System.Drawing.Size(926, 468);
            this.neuPanel1.ResumeLayout(false);
            this.neuGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.neuGroupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView2)).EndInit();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtOriginalFilter;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCompareFilter;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox4;
        protected FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView sheetView2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label1;
    }
}
