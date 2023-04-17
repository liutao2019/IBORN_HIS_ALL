namespace FS.HISFC.Components.Pharmacy.Report
{
    partial class ucCheckDetailPrint
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpCheckDetail = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbCurrTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tvCheckList = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuPanel5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.rbtCancel = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtCommit = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtNoneCommit = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCheckDetail)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.neuPanel5.SuspendLayout();
            this.SuspendLayout();
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
            this.neuSpread1.Location = new System.Drawing.Point( 0, 0 );
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange( new FarPoint.Win.Spread.SheetView[] {
            this.fpCheckDetail} );
            this.neuSpread1.Size = new System.Drawing.Size( 797, 944 );
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font( "宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)) );
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.AutoSortingColumn += new FarPoint.Win.Spread.AutoSortingColumnEventHandler( this.neuSpread1_AutoSortingColumn );
            this.neuSpread1.AutoSortedColumn += new FarPoint.Win.Spread.AutoSortedColumnEventHandler( this.neuSpread1_AutoSortedColumn );
            // 
            // fpCheckDetail
            // 
            this.fpCheckDetail.Reset();
            this.fpCheckDetail.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCheckDetail.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCheckDetail.ColumnCount = 10;
            this.fpCheckDetail.ColumnHeader.RowCount = 2;
            this.fpCheckDetail.RowCount = 1;
            this.fpCheckDetail.ActiveSkin = new FarPoint.Win.Spread.SheetSkin( "CustomSkin2", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true );
            numberCellType1.SpinDecimalIncrement = 0.01F;
            this.fpCheckDetail.Cells.Get( 0, 9 ).CellType = numberCellType1;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 0 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 0 ).RowSpan = 2;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 0 ).Value = "药品名称";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 0 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 1 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 1 ).RowSpan = 2;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 1 ).Value = "规格";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 1 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 2 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 2 ).RowSpan = 2;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 2 ).Value = "单位";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 2 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 3 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 3 ).RowSpan = 2;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 3 ).Value = "单价";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 3 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 4 ).ColumnSpan = 2;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 4 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 4 ).Value = "账面数";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 4 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 5 ).Value = "账面数";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 6 ).ColumnSpan = 2;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 6 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 6 ).Value = "实盘数";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 6 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 8 ).ColumnSpan = 2;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 8 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 8 ).Value = "盈亏数";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 0, 8 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 0 ).Value = "药品名称";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 1 ).Value = "规格";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 2 ).Value = "单位";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 3 ).Value = "单价";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 4 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 4 ).Value = "数量";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 4 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 5 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 5 ).Value = "金额";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 5 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 6 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 6 ).Value = "数量";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 6 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 7 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 7 ).Value = "金额";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 7 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 8 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 8 ).Value = "数量";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 8 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 9 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 9 ).Value = "金额";
            this.fpCheckDetail.ColumnHeader.Cells.Get( 1, 9 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCheckDetail.ColumnHeader.DefaultStyle.Locked = false;
            this.fpCheckDetail.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCheckDetail.Columns.Get( 0 ).CellType = textCellType1;
            this.fpCheckDetail.Columns.Get( 0 ).Label = "药品名称";
            this.fpCheckDetail.Columns.Get( 0 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 0 ).Width = 160F;
            this.fpCheckDetail.Columns.Get( 1 ).CellType = textCellType2;
            this.fpCheckDetail.Columns.Get( 1 ).Label = "规格";
            this.fpCheckDetail.Columns.Get( 1 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 1 ).Width = 80F;
            this.fpCheckDetail.Columns.Get( 2 ).CellType = textCellType3;
            this.fpCheckDetail.Columns.Get( 2 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 2 ).Label = "单位";
            this.fpCheckDetail.Columns.Get( 2 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 2 ).Width = 35F;
            this.fpCheckDetail.Columns.Get( 3 ).CellType = textCellType4;
            this.fpCheckDetail.Columns.Get( 3 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpCheckDetail.Columns.Get( 3 ).Label = "单价";
            this.fpCheckDetail.Columns.Get( 3 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 4 ).CellType = textCellType5;
            this.fpCheckDetail.Columns.Get( 4 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpCheckDetail.Columns.Get( 4 ).Label = "数量";
            this.fpCheckDetail.Columns.Get( 4 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 5 ).CellType = textCellType6;
            this.fpCheckDetail.Columns.Get( 5 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpCheckDetail.Columns.Get( 5 ).Label = "金额";
            this.fpCheckDetail.Columns.Get( 5 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 5 ).Width = 70F;
            this.fpCheckDetail.Columns.Get( 6 ).CellType = textCellType7;
            this.fpCheckDetail.Columns.Get( 6 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpCheckDetail.Columns.Get( 6 ).Label = "数量";
            this.fpCheckDetail.Columns.Get( 6 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 7 ).CellType = textCellType8;
            this.fpCheckDetail.Columns.Get( 7 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpCheckDetail.Columns.Get( 7 ).Label = "金额";
            this.fpCheckDetail.Columns.Get( 7 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 7 ).Width = 70F;
            this.fpCheckDetail.Columns.Get( 8 ).CellType = textCellType9;
            this.fpCheckDetail.Columns.Get( 8 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpCheckDetail.Columns.Get( 8 ).Label = "数量";
            this.fpCheckDetail.Columns.Get( 8 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 9 ).AllowAutoSort = true;
            this.fpCheckDetail.Columns.Get( 9 ).CellType = numberCellType1;
            this.fpCheckDetail.Columns.Get( 9 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpCheckDetail.Columns.Get( 9 ).Label = "金额";
            this.fpCheckDetail.Columns.Get( 9 ).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpCheckDetail.Columns.Get( 9 ).Width = 70F;
            this.fpCheckDetail.DefaultStyle.Locked = true;
            this.fpCheckDetail.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpCheckDetail.RowHeader.Columns.Default.Resizable = false;
            this.fpCheckDetail.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCheckDetail.RowHeader.DefaultStyle.Locked = false;
            this.fpCheckDetail.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCheckDetail.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpCheckDetail.SheetCornerStyle.Locked = false;
            this.fpCheckDetail.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpCheckDetail.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpCheckDetail.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add( this.neuPanel3 );
            this.neuPanel1.Controls.Add( this.neuPanel2 );
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point( 177, 0 );
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size( 797, 1024 );
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add( this.neuSpread1 );
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point( 0, 80 );
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size( 797, 944 );
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 3;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add( this.lbCurrTime );
            this.neuPanel2.Controls.Add( this.lbCount );
            this.neuPanel2.Controls.Add( this.lbOper );
            this.neuPanel2.Controls.Add( this.lbDate );
            this.neuPanel2.Controls.Add( this.lbDept );
            this.neuPanel2.Controls.Add( this.lblTitle );
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point( 0, 0 );
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size( 797, 80 );
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // lbCurrTime
            // 
            this.lbCurrTime.AutoSize = true;
            this.lbCurrTime.Location = new System.Drawing.Point( 636, 50 );
            this.lbCurrTime.Name = "lbCurrTime";
            this.lbCurrTime.Size = new System.Drawing.Size( 65, 12 );
            this.lbCurrTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCurrTime.TabIndex = 4;
            this.lbCurrTime.Text = "制单时间：";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point( 390, 50 );
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size( 65, 12 );
            this.lbCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCount.TabIndex = 3;
            this.lbCount.Text = "品种总数：";
            // 
            // lbOper
            // 
            this.lbOper.AutoSize = true;
            this.lbOper.Location = new System.Drawing.Point( 499, 50 );
            this.lbOper.Name = "lbOper";
            this.lbOper.Size = new System.Drawing.Size( 53, 12 );
            this.lbOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbOper.TabIndex = 3;
            this.lbOper.Text = "制表人：";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Location = new System.Drawing.Point( 184, 50 );
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size( 65, 12 );
            this.lbDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDate.TabIndex = 2;
            this.lbDate.Text = "执行日期：";
            // 
            // lbDept
            // 
            this.lbDept.AutoSize = true;
            this.lbDept.Location = new System.Drawing.Point( 17, 50 );
            this.lbDept.Name = "lbDept";
            this.lbDept.Size = new System.Drawing.Size( 65, 12 );
            this.lbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDept.TabIndex = 1;
            this.lbDept.Text = "药房名称：";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font( "宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)) );
            this.lblTitle.Location = new System.Drawing.Point( 376, 12 );
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size( 69, 19 );
            this.lblTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "盘点单";
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add( this.tvCheckList );
            this.neuPanel4.Controls.Add( this.neuPanel5 );
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel4.Location = new System.Drawing.Point( 0, 0 );
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size( 177, 1024 );
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 2;
            // 
            // tvCheckList
            // 
            this.tvCheckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCheckList.HideSelection = false;
            this.tvCheckList.Location = new System.Drawing.Point( 0, 99 );
            this.tvCheckList.Name = "tvCheckList";
            this.tvCheckList.Size = new System.Drawing.Size( 177, 925 );
            this.tvCheckList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvCheckList.TabIndex = 3;
            this.tvCheckList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tvCheckList_AfterSelect );
            // 
            // neuPanel5
            // 
            this.neuPanel5.Controls.Add( this.rbtCancel );
            this.neuPanel5.Controls.Add( this.rbtCommit );
            this.neuPanel5.Controls.Add( this.rbtNoneCommit );
            this.neuPanel5.Controls.Add( this.neuLabel4 );
            this.neuPanel5.Controls.Add( this.neuLabel3 );
            this.neuPanel5.Controls.Add( this.dtpEnd );
            this.neuPanel5.Controls.Add( this.dtpStart );
            this.neuPanel5.Controls.Add( this.neuLabel2 );
            this.neuPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel5.Location = new System.Drawing.Point( 0, 0 );
            this.neuPanel5.Name = "neuPanel5";
            this.neuPanel5.Size = new System.Drawing.Size( 177, 99 );
            this.neuPanel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel5.TabIndex = 3;
            // 
            // rbtCancel
            // 
            this.rbtCancel.AutoSize = true;
            this.rbtCancel.Location = new System.Drawing.Point( 116, 76 );
            this.rbtCancel.Name = "rbtCancel";
            this.rbtCancel.Size = new System.Drawing.Size( 47, 16 );
            this.rbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtCancel.TabIndex = 9;
            this.rbtCancel.Tag = "2";
            this.rbtCancel.Text = "解封";
            this.rbtCancel.UseVisualStyleBackColor = true;
            this.rbtCancel.CheckedChanged += new System.EventHandler( this.rbtNoneCommit_CheckedChanged );
            // 
            // rbtCommit
            // 
            this.rbtCommit.AutoSize = true;
            this.rbtCommit.Checked = true;
            this.rbtCommit.Location = new System.Drawing.Point( 63, 76 );
            this.rbtCommit.Name = "rbtCommit";
            this.rbtCommit.Size = new System.Drawing.Size( 47, 16 );
            this.rbtCommit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtCommit.TabIndex = 8;
            this.rbtCommit.TabStop = true;
            this.rbtCommit.Tag = "1";
            this.rbtCommit.Text = "结存";
            this.rbtCommit.UseVisualStyleBackColor = true;
            this.rbtCommit.CheckedChanged += new System.EventHandler( this.rbtNoneCommit_CheckedChanged );
            // 
            // rbtNoneCommit
            // 
            this.rbtNoneCommit.AutoSize = true;
            this.rbtNoneCommit.Location = new System.Drawing.Point( 10, 76 );
            this.rbtNoneCommit.Name = "rbtNoneCommit";
            this.rbtNoneCommit.Size = new System.Drawing.Size( 47, 16 );
            this.rbtNoneCommit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtNoneCommit.TabIndex = 6;
            this.rbtNoneCommit.Tag = "0";
            this.rbtNoneCommit.Text = "封帐";
            this.rbtNoneCommit.UseVisualStyleBackColor = true;
            this.rbtNoneCommit.CheckedChanged += new System.EventHandler( this.rbtNoneCommit_CheckedChanged );
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point( 8, 53 );
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size( 17, 12 );
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "至";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point( 8, 30 );
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size( 17, 12 );
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 6;
            this.neuLabel3.Text = "从";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point( 31, 50 );
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size( 121, 21 );
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 5;
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.IsEnter2Tab = false;
            this.dtpStart.Location = new System.Drawing.Point( 31, 26 );
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size( 121, 21 );
            this.dtpStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpStart.TabIndex = 4;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.neuLabel2.Location = new System.Drawing.Point( 8, 8 );
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size( 113, 12 );
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "盘点单检索时间范围";
            // 
            // ucCheckDetailPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add( this.neuPanel1 );
            this.Controls.Add( this.neuPanel4 );
            this.Name = "ucCheckDetailPrint";
            this.Size = new System.Drawing.Size( 974, 1024 );
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCheckDetail)).EndInit();
            this.neuPanel1.ResumeLayout( false );
            this.neuPanel3.ResumeLayout( false );
            this.neuPanel2.ResumeLayout( false );
            this.neuPanel2.PerformLayout();
            this.neuPanel4.ResumeLayout( false );
            this.neuPanel5.ResumeLayout( false );
            this.neuPanel5.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView fpCheckDetail;        
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCount;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpStart;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvCheckList;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCurrTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbOper;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtCommit;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtNoneCommit;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtCancel;
    }
}
