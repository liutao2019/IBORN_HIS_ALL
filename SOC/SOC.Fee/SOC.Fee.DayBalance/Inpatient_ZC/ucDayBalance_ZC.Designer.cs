namespace SOC.Fee.DayBalance.Inpatient_ZC
{
    partial class ucDayBalance_ZC
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 3, false, false, false, true);
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, true, false, false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.EmptyBorder emptyBorder1 = new FarPoint.Win.EmptyBorder();
            FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);
            FarPoint.Win.LineBorder lineBorder4 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame);
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder5 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 3, false, false, false, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder6 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.ctlBalanceDate = new SOC.Fee.DayBalance.Inpatient.ucInpatientDayBalanceDateControl();
            this.gbxTop = new System.Windows.Forms.GroupBox();
            this.pnlReport = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.gboHistory = new System.Windows.Forms.GroupBox();
            this.neuSpread2 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.lblHistory = new System.Windows.Forms.Label();
            this.dtpHistory = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbxTop.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.gboHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctlBalanceDate
            // 
            this.ctlBalanceDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlBalanceDate.DtLastBalance = new System.DateTime(((long)(0)));
            this.ctlBalanceDate.Location = new System.Drawing.Point(3, 17);
            this.ctlBalanceDate.Name = "ctlBalanceDate";
            this.ctlBalanceDate.Size = new System.Drawing.Size(745, 36);
            this.ctlBalanceDate.TabIndex = 0;
            this.ctlBalanceDate.Load += new System.EventHandler(this.ctlBalanceDate_Load);
            // 
            // gbxTop
            // 
            this.gbxTop.Controls.Add(this.ctlBalanceDate);
            this.gbxTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxTop.Location = new System.Drawing.Point(0, 0);
            this.gbxTop.Name = "gbxTop";
            this.gbxTop.Size = new System.Drawing.Size(751, 56);
            this.gbxTop.TabIndex = 1;
            this.gbxTop.TabStop = false;
            this.gbxTop.Text = "日结查询条件";
            // 
            // pnlReport
            // 
            this.pnlReport.BackColor = System.Drawing.Color.White;
            this.pnlReport.Controls.Add(this.neuSpread1);
            this.pnlReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReport.Location = new System.Drawing.Point(0, 56);
            this.pnlReport.Name = "pnlReport";
            this.pnlReport.Size = new System.Drawing.Size(751, 514);
            this.pnlReport.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, 收款员缴款报表";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(751, 514);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
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
            this.neuSpread1_Sheet1.ColumnCount = 12;
            this.neuSpread1_Sheet1.RowCount = 20;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).CellType = textCellType1;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ColumnSpan = 12;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "收款员缴款报表";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Value = "收款员：";
            this.neuSpread1_Sheet1.Cells.Get(1, 2).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(1, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(1, 2).Tag = "OPER_CODE";
            this.neuSpread1_Sheet1.Cells.Get(1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 6).Value = "日期：";
            this.neuSpread1_Sheet1.Cells.Get(1, 7).ColumnSpan = 5;
            this.neuSpread1_Sheet1.Cells.Get(1, 7).Tag = "BEGIN_DATE";
            this.neuSpread1_Sheet1.Cells.Get(2, 0).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.Cells.Get(2, 0).Border = lineBorder1;
            this.neuSpread1_Sheet1.Cells.Get(2, 0).ColumnSpan = 12;
            this.neuSpread1_Sheet1.Cells.Get(3, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(3, 0).Value = "发票合计";
            this.neuSpread1_Sheet1.Cells.Get(3, 1).Tag = "INVICE_TCOST";
            this.neuSpread1_Sheet1.Cells.Get(3, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).Value = "退费原单";
            this.neuSpread1_Sheet1.Cells.Get(3, 3).Tag = "QTOLD_TCOST";
            this.neuSpread1_Sheet1.Cells.Get(3, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(3, 4).Value = "退费新单";
            this.neuSpread1_Sheet1.Cells.Get(3, 5).Tag = "QTNEW_TCOST";
            this.neuSpread1_Sheet1.Cells.Get(3, 6).Value = "退费金额";
            this.neuSpread1_Sheet1.Cells.Get(3, 7).Tag = "QT_TCOST";
            this.neuSpread1_Sheet1.Cells.Get(3, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(3, 10).Value = "收入合计";
            this.neuSpread1_Sheet1.Cells.Get(3, 11).Tag = "INVICE_TCOST + PREPAY_TCOST";
            this.neuSpread1_Sheet1.Cells.Get(4, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(4, 0).Value = "冲销预付款";
            this.neuSpread1_Sheet1.Cells.Get(4, 1).Tag = "PAYPREPAY_TCOST";
            this.neuSpread1_Sheet1.Cells.Get(4, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(4, 2).Value = "收预收款";
            this.neuSpread1_Sheet1.Cells.Get(4, 3).Tag = "PREPAY_TCOST";
            this.neuSpread1_Sheet1.Cells.Get(4, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(4, 4).Value = "现金";
            this.neuSpread1_Sheet1.Cells.Get(4, 5).Tag = "PREPAYCA_COST";
            this.neuSpread1_Sheet1.Cells.Get(4, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(4, 6).Value = "信用卡";
            this.neuSpread1_Sheet1.Cells.Get(4, 7).Tag = "PREPAYCD_COST";
            this.neuSpread1_Sheet1.Cells.Get(4, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(4, 8).Value = "支票";
            this.neuSpread1_Sheet1.Cells.Get(4, 9).Tag = "PREPAYOTHER_COST";
            this.neuSpread1_Sheet1.Cells.Get(4, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(4, 10).Value = "支出合计";
            this.neuSpread1_Sheet1.Cells.Get(4, 11).Tag = "PAYPREPAY_TCOST";
            this.neuSpread1_Sheet1.Cells.Get(5, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(5, 0).Value = "现金";
            this.neuSpread1_Sheet1.Cells.Get(5, 1).Tag = "CHARGEPAYCA_COST";
            this.neuSpread1_Sheet1.Cells.Get(5, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(5, 2).Value = "支票";
            this.neuSpread1_Sheet1.Cells.Get(5, 3).Tag = "CHARGEPAYOTHER_COST";
            this.neuSpread1_Sheet1.Cells.Get(5, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(5, 4).Value = "信用卡";
            this.neuSpread1_Sheet1.Cells.Get(5, 5).Tag = "CHARGEPAYCD_COST";
            this.neuSpread1_Sheet1.Cells.Get(5, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(5, 6).Value = "退支票";
            this.neuSpread1_Sheet1.Cells.Get(5, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(5, 10).Value = "应交合计";
            this.neuSpread1_Sheet1.Cells.Get(5, 11).Tag = "CHARGEPAYCA_COST + CHARGEPAYOTHER_COST + CHARGEPAYCD_COST";
            this.neuSpread1_Sheet1.Cells.Get(6, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(6, 0).Value = "优惠";
            this.neuSpread1_Sheet1.Cells.Get(6, 1).Tag = "CHARGEJZ_COST";
            this.neuSpread1_Sheet1.Cells.Get(6, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(6, 2).Value = "记帐";
            this.neuSpread1_Sheet1.Cells.Get(6, 3).Tag = "CHARGEJZ1_COST";
            this.neuSpread1_Sheet1.Cells.Get(6, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(6, 5).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(6, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(6, 5).Value = "公费记帐金额";
            this.neuSpread1_Sheet1.Cells.Get(6, 7).Tag = "CHARGEJZGF_COST";
            this.neuSpread1_Sheet1.Cells.Get(6, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(6, 10).Value = "医保金额";
            this.neuSpread1_Sheet1.Cells.Get(6, 11).Tag = "PUB_COST";
            this.neuSpread1_Sheet1.Cells.Get(7, 0).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(7, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(7, 0).Value = "扣除医保金额实交";
            this.neuSpread1_Sheet1.Cells.Get(7, 2).Tag = "INVICE_TCOST - PUB_COST";
            this.neuSpread1_Sheet1.Cells.Get(7, 3).ColumnSpan = 9;
            this.neuSpread1_Sheet1.Cells.Get(7, 3).Tag = "(INVICE_TCOST - PUB_COST) tostring";
            this.neuSpread1_Sheet1.Cells.Get(8, 0).Border = lineBorder2;
            this.neuSpread1_Sheet1.Cells.Get(8, 0).ColumnSpan = 12;
            this.neuSpread1_Sheet1.Cells.Get(9, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(9, 0).Value = "发票号从：";
            this.neuSpread1_Sheet1.Cells.Get(9, 1).ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells.Get(9, 1).Tag = "INVOICENO";
            this.neuSpread1_Sheet1.Cells.Get(9, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(9, 5).Tag = "INVOICECOUNT";
            this.neuSpread1_Sheet1.Cells.Get(9, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(9, 6).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(9, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(9, 6).Value = "张      印刷号：";
            this.neuSpread1_Sheet1.Cells.Get(9, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(9, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(9, 8).ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells.Get(9, 8).Tag = "PRINTINVOICENO";
            this.neuSpread1_Sheet1.Cells.Get(10, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(10, 0).Value = "票据号备注：";
            this.neuSpread1_Sheet1.Cells.Get(10, 1).ColumnSpan = 11;
            this.neuSpread1_Sheet1.Cells.Get(11, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(11, 0).Value = "作废：";
            this.neuSpread1_Sheet1.Cells.Get(11, 1).Tag = "CANCELINVOICECOUNT";
            this.neuSpread1_Sheet1.Cells.Get(11, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(11, 2).Value = "作废票号：";
            this.neuSpread1_Sheet1.Cells.Get(11, 3).ColumnSpan = 9;
            this.neuSpread1_Sheet1.Cells.Get(11, 3).Tag = "CANCELINVOICENO";
            this.neuSpread1_Sheet1.Cells.Get(12, 0).Value = "作废票备注：";
            this.neuSpread1_Sheet1.Cells.Get(12, 1).ColumnSpan = 11;
            this.neuSpread1_Sheet1.Cells.Get(13, 0).Value = "退费：";
            this.neuSpread1_Sheet1.Cells.Get(13, 1).Tag = "QUITINVOICECOUNT";
            this.neuSpread1_Sheet1.Cells.Get(13, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(13, 2).Value = "退费票号：";
            textCellType2.WordWrap = true;
            this.neuSpread1_Sheet1.Cells.Get(13, 3).CellType = textCellType2;
            this.neuSpread1_Sheet1.Cells.Get(13, 3).ColumnSpan = 9;
            this.neuSpread1_Sheet1.Cells.Get(13, 3).Tag = "QUITINVOICENO";
            this.neuSpread1_Sheet1.Cells.Get(14, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(14, 0).Value = "退费票备注：";
            this.neuSpread1_Sheet1.Cells.Get(14, 1).ColumnSpan = 11;
            this.neuSpread1_Sheet1.Cells.Get(15, 0).ColumnSpan = 12;
            this.neuSpread1_Sheet1.Cells.Get(16, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(16, 0).Value = "处币明细";
            this.neuSpread1_Sheet1.Cells.Get(16, 1).ColumnSpan = 11;
            this.neuSpread1_Sheet1.Cells.Get(17, 0).ColumnSpan = 12;
            this.neuSpread1_Sheet1.Cells.Get(18, 5).Value = "复核：";
            this.neuSpread1_Sheet1.Cells.Get(18, 8).Value = "出纳：";
            this.neuSpread1_Sheet1.Cells.Get(19, 0).Border = emptyBorder1;
            this.neuSpread1_Sheet1.Cells.Get(19, 0).ColumnSpan = 12;
            this.neuSpread1_Sheet1.ColumnHeader.Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 78F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 68F;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 53F;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 57F;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 54F;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 55F;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 44F;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 56F;
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 58F;
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 66F;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.SystemColors.HighlightText;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Get(0).Height = 40F;
            this.neuSpread1_Sheet1.Rows.Get(1).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(2).BackColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.Rows.Get(2).Border = lineBorder3;
            this.neuSpread1_Sheet1.Rows.Get(2).Height = 2F;
            this.neuSpread1_Sheet1.Rows.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Rows.Get(3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Rows.Get(3).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Rows.Get(4).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Rows.Get(5).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Rows.Get(6).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(7).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Rows.Get(7).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(8).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.Rows.Get(8).Border = lineBorder4;
            this.neuSpread1_Sheet1.Rows.Get(8).Height = 4F;
            this.neuSpread1_Sheet1.Rows.Get(9).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(10).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType3.WordWrap = true;
            this.neuSpread1_Sheet1.Rows.Get(11).CellType = textCellType3;
            this.neuSpread1_Sheet1.Rows.Get(11).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(12).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Rows.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(13).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Rows.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(14).Height = 25F;
            this.neuSpread1_Sheet1.Rows.Get(14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(15).Border = lineBorder5;
            this.neuSpread1_Sheet1.Rows.Get(15).Height = 2F;
            textCellType4.WordWrap = true;
            this.neuSpread1_Sheet1.Rows.Get(16).CellType = textCellType4;
            this.neuSpread1_Sheet1.Rows.Get(16).Height = 40F;
            this.neuSpread1_Sheet1.Rows.Get(16).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(17).Border = lineBorder6;
            this.neuSpread1_Sheet1.Rows.Get(17).Height = 2F;
            this.neuSpread1_Sheet1.Rows.Get(18).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Rows.Get(18).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(18).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // gboHistory
            // 
            this.gboHistory.Controls.Add(this.neuSpread2);
            this.gboHistory.Controls.Add(this.lblHistory);
            this.gboHistory.Controls.Add(this.dtpHistory);
            this.gboHistory.Dock = System.Windows.Forms.DockStyle.Left;
            this.gboHistory.Location = new System.Drawing.Point(0, 0);
            this.gboHistory.Name = "gboHistory";
            this.gboHistory.Size = new System.Drawing.Size(294, 570);
            this.gboHistory.TabIndex = 3;
            this.gboHistory.TabStop = false;
            this.gboHistory.Text = "历史日结记录";
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "3.0.2004.2005";
            this.neuSpread2.AccessibleDescription = "neuSpread2, Sheet1, Row 0, Column 0, ";
            this.neuSpread2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.FileName = "";
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(6, 61);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.neuSpread2.Size = new System.Drawing.Size(282, 495);
            this.neuSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 2;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance2;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread2_CellDoubleClick);
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 1;
            this.sheetView1.RowCount = 5;
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "日结记录";
            this.sheetView1.Columns.Get(0).Label = "日结记录";
            this.sheetView1.Columns.Get(0).Width = 249F;
            this.sheetView1.DefaultStyle.Locked = true;
            this.sheetView1.DefaultStyle.Parent = "DataAreaDefault";
            this.sheetView1.GrayAreaBackColor = System.Drawing.Color.White;
            this.sheetView1.RowHeader.Columns.Default.Resizable = false;
            this.sheetView1.RowHeader.Columns.Get(0).Width = 26F;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Location = new System.Drawing.Point(27, 28);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(89, 12);
            this.lblHistory.TabIndex = 1;
            this.lblHistory.Text = "历史日结记录：";
            // 
            // dtpHistory
            // 
            this.dtpHistory.CustomFormat = "yyyy年MM月";
            this.dtpHistory.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHistory.Location = new System.Drawing.Point(116, 23);
            this.dtpHistory.Name = "dtpHistory";
            this.dtpHistory.Size = new System.Drawing.Size(97, 21);
            this.dtpHistory.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlReport);
            this.panel1.Controls.Add(this.gbxTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(294, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 570);
            this.panel1.TabIndex = 4;
            // 
            // ucDayBalance_ZC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gboHistory);
            this.Name = "ucDayBalance_ZC";
            this.Size = new System.Drawing.Size(1045, 570);
            this.gbxTop.ResumeLayout(false);
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.gboHistory.ResumeLayout(false);
            this.gboHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SOC.Fee.DayBalance.Inpatient.ucInpatientDayBalanceDateControl ctlBalanceDate;
        private System.Windows.Forms.GroupBox gbxTop;
        private System.Windows.Forms.Panel pnlReport;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.GroupBox gboHistory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.DateTimePicker dtpHistory;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FarPoint.Win.Spread.SheetView sheetView1;
    }
}
