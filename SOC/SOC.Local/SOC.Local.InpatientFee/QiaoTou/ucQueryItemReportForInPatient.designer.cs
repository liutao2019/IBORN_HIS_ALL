namespace FS.SOC.Local.InpatientFee.QiaoTou
{
    partial class ucQueryItemReportForInPatient
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbQueryItem = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.endDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.beginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuPrint = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuMemo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1.SuspendLayout();
            this.neuPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.cmbQueryItem);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.endDate);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.beginDate);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(855, 64);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询条件";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(452, 30);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(29, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "项目";
            // 
            // cmbQueryItem
            // 
            this.cmbQueryItem.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbQueryItem.FormattingEnabled = true;
            this.cmbQueryItem.IsEnter2Tab = false;
            this.cmbQueryItem.IsFlat = false;
            this.cmbQueryItem.IsLike = true;
            this.cmbQueryItem.IsListOnly = false;
            this.cmbQueryItem.IsPopForm = true;
            this.cmbQueryItem.IsShowCustomerList = false;
            this.cmbQueryItem.IsShowID = false;
            this.cmbQueryItem.Location = new System.Drawing.Point(491, 26);
            this.cmbQueryItem.Name = "cmbQueryItem";
            this.cmbQueryItem.PopForm = null;
            this.cmbQueryItem.ShowCustomerList = false;
            this.cmbQueryItem.ShowID = false;
            this.cmbQueryItem.Size = new System.Drawing.Size(197, 20);
            this.cmbQueryItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbQueryItem.TabIndex = 4;
            this.cmbQueryItem.Tag = "";
            this.cmbQueryItem.ToolBarUse = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(212, 30);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(11, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "-";
            // 
            // endDate
            // 
            this.endDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.IsEnter2Tab = false;
            this.endDate.Location = new System.Drawing.Point(231, 25);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(143, 21);
            this.endDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.endDate.TabIndex = 2;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(24, 30);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(29, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "日期";
            // 
            // beginDate
            // 
            this.beginDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.beginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.beginDate.IsEnter2Tab = false;
            this.beginDate.Location = new System.Drawing.Point(60, 25);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(143, 21);
            this.beginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.beginDate.TabIndex = 0;
            // 
            // neuPrint
            // 
            this.neuPrint.Controls.Add(this.neuSpread1);
            this.neuPrint.Controls.Add(this.neuPanel1);
            this.neuPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPrint.Location = new System.Drawing.Point(0, 64);
            this.neuPrint.Name = "neuPrint";
            this.neuPrint.Size = new System.Drawing.Size(855, 477);
            this.neuPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPrint.TabIndex = 1;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 68);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(855, 409);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 6;
            this.neuSpread1_Sheet1.RowCount = 20;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "科室";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "工号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "医生姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单价";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "总金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuSpread1_Sheet1.GroupBarBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuMemo);
            this.neuPanel1.Controls.Add(this.neuDate);
            this.neuPanel1.Controls.Add(this.neuLblTitle);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(855, 68);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuMemo
            // 
            this.neuMemo.AutoSize = true;
            this.neuMemo.Location = new System.Drawing.Point(17, 47);
            this.neuMemo.Name = "neuMemo";
            this.neuMemo.Size = new System.Drawing.Size(53, 12);
            this.neuMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuMemo.TabIndex = 2;
            this.neuMemo.Text = "查询项目";
            // 
            // neuDate
            // 
            this.neuDate.AutoSize = true;
            this.neuDate.Location = new System.Drawing.Point(17, 28);
            this.neuDate.Name = "neuDate";
            this.neuDate.Size = new System.Drawing.Size(53, 12);
            this.neuDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDate.TabIndex = 1;
            this.neuDate.Text = "统计信息";
            // 
            // neuLblTitle
            // 
            this.neuLblTitle.AutoSize = true;
            this.neuLblTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLblTitle.Location = new System.Drawing.Point(354, 9);
            this.neuLblTitle.Name = "neuLblTitle";
            this.neuLblTitle.Size = new System.Drawing.Size(37, 14);
            this.neuLblTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLblTitle.TabIndex = 0;
            this.neuLblTitle.Text = "标题";
            // 
            // ucQueryItemReportForOutPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.neuPrint);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucQueryItemReportForOutPatient";
            this.Size = new System.Drawing.Size(855, 541);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker beginDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker endDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbQueryItem;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPrint;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLblTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuMemo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuDate;
    }
}
