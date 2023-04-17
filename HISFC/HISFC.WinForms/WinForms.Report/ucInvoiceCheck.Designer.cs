namespace FS.WinForms.Report
{
    partial class ucInvoiceCheck
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuGroupBox4 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.rbCheckTrue = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbCheckAll = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbCheckFalse = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbReceiptType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btnQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.rbSelect2 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.dtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.rbSelect1 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.cmbName = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.rbSelect3 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.tbNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuGroupBox2.SuspendLayout();
            this.neuGroupBox4.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.neuSpread1);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox3.Location = new System.Drawing.Point(283, 0);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(954, 547);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 15;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "发票核销";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 17);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(948, 527);
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
            this.neuSpread1_Sheet1.ColumnCount = 14;
            this.neuSpread1_Sheet1.AllowNoteEdit = false;
            this.neuSpread1_Sheet1.AutoCalculation = false;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "日结报表编号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "收款员编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "收款员姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "日结开始时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "日结结束时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "发票起始号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "发票张数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "总金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "现金金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "交账日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "核销状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "核销人编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "核销人姓名";
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 39F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "日结报表编号";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 87F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "收款员编码";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "收款员姓名";
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "日结开始时间";
            this.neuSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 87F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "日结结束时间";
            this.neuSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 87F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "发票起始号";
            this.neuSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "发票张数";
            this.neuSpread1_Sheet1.Columns.Get(7).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 63F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "总金额";
            this.neuSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 51F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "现金金额";
            this.neuSpread1_Sheet1.Columns.Get(9).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 63F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "交账日期";
            this.neuSpread1_Sheet1.Columns.Get(10).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 63F;
            this.neuSpread1_Sheet1.Columns.Get(11).CellType = checkBoxCellType2;
            this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "核销状态";
            this.neuSpread1_Sheet1.Columns.Get(11).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 63F;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "核销人编码";
            this.neuSpread1_Sheet1.Columns.Get(12).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "核销人姓名";
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 75F;
            this.neuSpread1_Sheet1.DataAutoCellTypes = false;
            this.neuSpread1_Sheet1.DataAutoHeadings = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuGroupBox4);
            this.neuGroupBox2.Controls.Add(this.btnQuery);
            this.neuGroupBox2.Controls.Add(this.neuGroupBox1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(283, 547);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 15;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "日结查询";
            // 
            // neuGroupBox4
            // 
            this.neuGroupBox4.Controls.Add(this.neuPanel1);
            this.neuGroupBox4.Controls.Add(this.neuLabel1);
            this.neuGroupBox4.Controls.Add(this.neuLabel2);
            this.neuGroupBox4.Controls.Add(this.cmbReceiptType);
            this.neuGroupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox4.Location = new System.Drawing.Point(3, 146);
            this.neuGroupBox4.Name = "neuGroupBox4";
            this.neuGroupBox4.Size = new System.Drawing.Size(277, 139);
            this.neuGroupBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox4.TabIndex = 16;
            this.neuGroupBox4.TabStop = false;
            this.neuGroupBox4.Text = "查询条件";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.rbCheckTrue);
            this.neuPanel1.Controls.Add(this.rbCheckAll);
            this.neuPanel1.Controls.Add(this.rbCheckFalse);
            this.neuPanel1.Location = new System.Drawing.Point(115, 45);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(86, 81);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 19;
            // 
            // rbCheckTrue
            // 
            this.rbCheckTrue.AutoSize = true;
            this.rbCheckTrue.Location = new System.Drawing.Point(3, 3);
            this.rbCheckTrue.Name = "rbCheckTrue";
            this.rbCheckTrue.Size = new System.Drawing.Size(59, 16);
            this.rbCheckTrue.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbCheckTrue.TabIndex = 14;
            this.rbCheckTrue.Text = "已核销";
            this.rbCheckTrue.UseVisualStyleBackColor = true;
            // 
            // rbCheckAll
            // 
            this.rbCheckAll.AutoSize = true;
            this.rbCheckAll.Checked = true;
            this.rbCheckAll.Location = new System.Drawing.Point(3, 61);
            this.rbCheckAll.Name = "rbCheckAll";
            this.rbCheckAll.Size = new System.Drawing.Size(47, 16);
            this.rbCheckAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbCheckAll.TabIndex = 18;
            this.rbCheckAll.TabStop = true;
            this.rbCheckAll.Text = "全部";
            this.rbCheckAll.UseVisualStyleBackColor = true;
            // 
            // rbCheckFalse
            // 
            this.rbCheckFalse.AutoSize = true;
            this.rbCheckFalse.Location = new System.Drawing.Point(3, 32);
            this.rbCheckFalse.Name = "rbCheckFalse";
            this.rbCheckFalse.Size = new System.Drawing.Size(59, 16);
            this.rbCheckFalse.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbCheckFalse.TabIndex = 17;
            this.rbCheckFalse.Text = "未核销";
            this.rbCheckFalse.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(6, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 5;
            this.neuLabel1.Text = "票据类型";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(6, 45);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 6;
            this.neuLabel2.Text = "核销状态";
            // 
            // cmbReceiptType
            // 
            this.cmbReceiptType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbReceiptType.FormattingEnabled = true;
            this.cmbReceiptType.IsEnter2Tab = false;
            this.cmbReceiptType.IsFlat = false;
            this.cmbReceiptType.IsLike = true;
            this.cmbReceiptType.IsListOnly = false;
            this.cmbReceiptType.IsPopForm = true;
            this.cmbReceiptType.IsShowCustomerList = false;
            this.cmbReceiptType.IsShowID = false;
            this.cmbReceiptType.Location = new System.Drawing.Point(115, 17);
            this.cmbReceiptType.Name = "cmbReceiptType";
            this.cmbReceiptType.PopForm = null;
            this.cmbReceiptType.ShowCustomerList = false;
            this.cmbReceiptType.ShowID = false;
            this.cmbReceiptType.Size = new System.Drawing.Size(121, 20);
            this.cmbReceiptType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbReceiptType.TabIndex = 7;
            this.cmbReceiptType.Tag = "";
            this.cmbReceiptType.ToolBarUse = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(179, 348);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Text = "查询";
            this.btnQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Visible = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.rbSelect2);
            this.neuGroupBox1.Controls.Add(this.dtpBegin);
            this.neuGroupBox1.Controls.Add(this.dtpEnd);
            this.neuGroupBox1.Controls.Add(this.rbSelect1);
            this.neuGroupBox1.Controls.Add(this.cmbName);
            this.neuGroupBox1.Controls.Add(this.rbSelect3);
            this.neuGroupBox1.Controls.Add(this.tbNo);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 17);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(277, 129);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 14;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询方式";
            // 
            // rbSelect2
            // 
            this.rbSelect2.AutoSize = true;
            this.rbSelect2.Location = new System.Drawing.Point(6, 76);
            this.rbSelect2.Name = "rbSelect2";
            this.rbSelect2.Size = new System.Drawing.Size(95, 16);
            this.rbSelect2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbSelect2.TabIndex = 4;
            this.rbSelect2.Text = "按收款员姓名";
            this.rbSelect2.UseVisualStyleBackColor = true;
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.IsEnter2Tab = false;
            this.dtpBegin.Location = new System.Drawing.Point(115, 18);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(152, 21);
            this.dtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBegin.TabIndex = 10;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point(115, 47);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(152, 21);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 11;
            // 
            // rbSelect1
            // 
            this.rbSelect1.AutoSize = true;
            this.rbSelect1.Checked = true;
            this.rbSelect1.Location = new System.Drawing.Point(6, 20);
            this.rbSelect1.Name = "rbSelect1";
            this.rbSelect1.Size = new System.Drawing.Size(107, 16);
            this.rbSelect1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbSelect1.TabIndex = 2;
            this.rbSelect1.TabStop = true;
            this.rbSelect1.Text = "按开始结束时间";
            this.rbSelect1.UseVisualStyleBackColor = true;
            // 
            // cmbName
            // 
            this.cmbName.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.IsEnter2Tab = false;
            this.cmbName.IsFlat = false;
            this.cmbName.IsLike = true;
            this.cmbName.IsListOnly = false;
            this.cmbName.IsPopForm = true;
            this.cmbName.IsShowCustomerList = false;
            this.cmbName.IsShowID = false;
            this.cmbName.Location = new System.Drawing.Point(115, 76);
            this.cmbName.Name = "cmbName";
            this.cmbName.PopForm = null;
            this.cmbName.ShowCustomerList = false;
            this.cmbName.ShowID = false;
            this.cmbName.Size = new System.Drawing.Size(152, 20);
            this.cmbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbName.TabIndex = 12;
            this.cmbName.Tag = "";
            this.cmbName.ToolBarUse = false;
            // 
            // rbSelect3
            // 
            this.rbSelect3.AutoSize = true;
            this.rbSelect3.Location = new System.Drawing.Point(6, 104);
            this.rbSelect3.Name = "rbSelect3";
            this.rbSelect3.Size = new System.Drawing.Size(107, 16);
            this.rbSelect3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbSelect3.TabIndex = 3;
            this.rbSelect3.TabStop = true;
            this.rbSelect3.Text = "按日结报表编号";
            this.rbSelect3.UseVisualStyleBackColor = true;
            // 
            // tbNo
            // 
            this.tbNo.IsEnter2Tab = false;
            this.tbNo.Location = new System.Drawing.Point(115, 104);
            this.tbNo.Name = "tbNo";
            this.tbNo.Size = new System.Drawing.Size(152, 21);
            this.tbNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbNo.TabIndex = 13;
            // 
            // ucInvoiceCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox3);
            this.Controls.Add(this.neuGroupBox2);
            this.Name = "ucInvoiceCheck";
            this.Size = new System.Drawing.Size(1237, 547);
            this.Load += new System.EventHandler(this.ucInvoiceCheck_Load);
            this.neuGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox4.ResumeLayout(false);
            this.neuGroupBox4.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbSelect1;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbSelect3;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbSelect2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbReceiptType;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBegin;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbNo;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuButton btnQuery;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox4;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbCheckAll;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbCheckFalse;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbCheckTrue;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
    }
}
