namespace FS.HISFC.Components.MTOrder
{
    partial class ucMTList
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMTList));
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dtpSearchDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.btnNext = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbMTType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btnPrevious = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbOrder = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pbZero = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.pbStop = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.pbOut = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel3.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbZero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOut)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.dtpSearchDate);
            this.neuPanel3.Controls.Add(this.btnNext);
            this.neuPanel3.Controls.Add(this.neuLabel1);
            this.neuPanel3.Controls.Add(this.cmbMTType);
            this.neuPanel3.Controls.Add(this.btnPrevious);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 35);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(689, 22);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 5;
            // 
            // dtpSearchDate
            // 
            this.dtpSearchDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpSearchDate.IsEnter2Tab = false;
            this.dtpSearchDate.Location = new System.Drawing.Point(75, 0);
            this.dtpSearchDate.Name = "dtpSearchDate";
            this.dtpSearchDate.Size = new System.Drawing.Size(329, 21);
            this.dtpSearchDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpSearchDate.TabIndex = 7;
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.Location = new System.Drawing.Point(404, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 22);
            this.btnNext.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "下一天";
            this.btnNext.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.neuLabel1.Location = new System.Drawing.Point(479, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.neuLabel1.Size = new System.Drawing.Size(89, 18);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "医技类型筛选：";
            // 
            // cmbMTType
            // 
            this.cmbMTType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbMTType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMTType.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbMTType.FormattingEnabled = true;
            this.cmbMTType.IsEnter2Tab = false;
            this.cmbMTType.IsFlat = false;
            this.cmbMTType.IsLike = true;
            this.cmbMTType.IsListOnly = false;
            this.cmbMTType.IsPopForm = true;
            this.cmbMTType.IsShowCustomerList = false;
            this.cmbMTType.IsShowID = false;
            this.cmbMTType.IsShowIDAndName = false;
            this.cmbMTType.Location = new System.Drawing.Point(568, 0);
            this.cmbMTType.Name = "cmbMTType";
            this.cmbMTType.ShowCustomerList = false;
            this.cmbMTType.ShowID = false;
            this.cmbMTType.Size = new System.Drawing.Size(121, 20);
            this.cmbMTType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbMTType.TabIndex = 3;
            this.cmbMTType.Tag = "";
            this.cmbMTType.ToolBarUse = false;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevious.Location = new System.Drawing.Point(0, 0);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 22);
            this.btnPrevious.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.Text = "上一天";
            this.btnPrevious.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.lbOrder);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(689, 35);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // lbOrder
            // 
            this.lbOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbOrder.AutoSize = true;
            this.lbOrder.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOrder.Location = new System.Drawing.Point(279, 7);
            this.lbOrder.Name = "lbOrder";
            this.lbOrder.Size = new System.Drawing.Size(142, 21);
            this.lbOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbOrder.TabIndex = 0;
            this.lbOrder.Text = "当前申请项目";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 57);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(689, 418);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 8;
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
            this.neuSpread1_Sheet1.ColumnCount = 13;
            this.neuSpread1_Sheet1.RowCount = 10;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ID";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ItemCode";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "TypeCode";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医技类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "DoctCode";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "医生姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "可用限额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "星期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "开始时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "结束时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "停诊原因";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "ID";
            this.neuSpread1_Sheet1.Columns.Get(0).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "ItemCode";
            this.neuSpread1_Sheet1.Columns.Get(1).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(2).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "TypeCode";
            this.neuSpread1_Sheet1.Columns.Get(3).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "医技类型";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 57F;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "DoctCode";
            this.neuSpread1_Sheet1.Columns.Get(5).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 49F;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "医生姓名";
            this.neuSpread1_Sheet1.Columns.Get(6).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 73F;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = numberCellType1;
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "可用限额";
            this.neuSpread1_Sheet1.Columns.Get(7).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 35F;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "日期";
            this.neuSpread1_Sheet1.Columns.Get(8).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(9).CellType = textCellType5;
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "星期";
            this.neuSpread1_Sheet1.Columns.Get(9).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 70F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2013, 10, 30, 23, 40, 42, 0);
            dateTimeCellType1.TimeDefault = new System.DateTime(2013, 10, 30, 23, 40, 42, 0);
            this.neuSpread1_Sheet1.Columns.Get(10).CellType = dateTimeCellType1;
            this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "开始时间";
            this.neuSpread1_Sheet1.Columns.Get(10).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 70F;
            dateTimeCellType2.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType2.Calendar")));
            dateTimeCellType2.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType2.DateDefault = new System.DateTime(2013, 10, 30, 23, 40, 25, 0);
            dateTimeCellType2.TimeDefault = new System.DateTime(2013, 10, 30, 23, 40, 25, 0);
            this.neuSpread1_Sheet1.Columns.Get(11).CellType = dateTimeCellType2;
            this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "结束时间";
            this.neuSpread1_Sheet1.Columns.Get(11).Resizable = false;
            this.neuSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "停诊原因";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 125F;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.pbZero);
            this.neuPanel2.Controls.Add(this.pbStop);
            this.neuPanel2.Controls.Add(this.pbOut);
            this.neuPanel2.Controls.Add(this.neuLabel5);
            this.neuPanel2.Controls.Add(this.neuLabel4);
            this.neuPanel2.Controls.Add(this.neuLabel3);
            this.neuPanel2.Controls.Add(this.neuLabel2);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 475);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(689, 25);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 9;
            // 
            // pbZero
            // 
            this.pbZero.Location = new System.Drawing.Point(241, 2);
            this.pbZero.Name = "pbZero";
            this.pbZero.Size = new System.Drawing.Size(35, 20);
            this.pbZero.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pbZero.TabIndex = 1;
            this.pbZero.TabStop = false;
            // 
            // pbStop
            // 
            this.pbStop.Location = new System.Drawing.Point(147, 2);
            this.pbStop.Name = "pbStop";
            this.pbStop.Size = new System.Drawing.Size(35, 20);
            this.pbStop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pbStop.TabIndex = 1;
            this.pbStop.TabStop = false;
            // 
            // pbOut
            // 
            this.pbOut.Location = new System.Drawing.Point(41, 2);
            this.pbOut.Name = "pbOut";
            this.pbOut.Size = new System.Drawing.Size(35, 20);
            this.pbOut.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pbOut.TabIndex = 1;
            this.pbOut.TabStop = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(285, 6);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(59, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 0;
            this.neuLabel5.Text = "为限额为0";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(191, 6);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(41, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 0;
            this.neuLabel4.Text = "为停班";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(85, 6);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "为已过期";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(3, 6);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(29, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "注：";
            // 
            // ucMTList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucMTList";
            this.Size = new System.Drawing.Size(689, 500);
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbZero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuButton btnPrevious;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMTType;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpSearchDate;
        private FS.FrameWork.WinForms.Controls.NeuButton btnNext;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbOrder;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPictureBox pbZero;
        private FS.FrameWork.WinForms.Controls.NeuPictureBox pbStop;
        private FS.FrameWork.WinForms.Controls.NeuPictureBox pbOut;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;

    }
}
