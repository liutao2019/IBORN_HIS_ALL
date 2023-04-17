namespace FS.SOC.Local.Order.OrderPrint
{
    /// <summary>
    /// frmOrderPrint 的摘要说明。
    /// </summary>
    public partial class frmOrderPrint
    {
        private System.ComponentModel.IContainer components;
        private int StartSplitPage;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderPrint));
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbQuery = new System.Windows.Forms.ToolStripButton();
            this.tbPrint = new System.Windows.Forms.ToolStripButton();
            this.tbRePrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbReset = new System.Windows.Forms.ToolStripDropDownButton();
            this.ResetLong = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetShort = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSetting = new System.Windows.Forms.ToolStripButton();
            this.tbExit = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLong = new System.Windows.Forms.TabPage();
            this.pnLongOrder = new System.Windows.Forms.Panel();
            this.fpLongOrder = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpLongOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnLongHeader = new System.Windows.Forms.Panel();
            this.ucOrderBillHeader2 = new FS.SOC.Local.Order.OrderPrint.ucOrderBillHeader();
            this.pnLongPag = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPageLong = new System.Windows.Forms.Label();
            this.tpShort = new System.Windows.Forms.TabPage();
            this.pnShortOrder = new System.Windows.Forms.Panel();
            this.fpShortOrder = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpShortOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnShortHeader = new System.Windows.Forms.Panel();
            this.ucOrderBillHeader1 = new FS.SOC.Local.Order.OrderPrint.ucOrderBillHeader();
            this.pnShortPag = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPageShort = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbPrintType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPrinter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nuRowNum = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nuTop = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nuLeft = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpLong.SuspendLayout();
            this.pnLongOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpLongOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpLongOrder_Sheet1)).BeginInit();
            this.pnLongHeader.SuspendLayout();
            this.pnLongPag.SuspendLayout();
            this.tpShort.SuspendLayout();
            this.pnShortOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpShortOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpShortOrder_Sheet1)).BeginInit();
            this.pnShortHeader.SuspendLayout();
            this.pnShortPag.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuRowNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 720);
            this.statusBar1.Size = new System.Drawing.Size(1004, 26);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 12.5F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbQuery,
            this.tbPrint,
            this.tbRePrint,
            this.toolStripSeparator1,
            this.tbReset,
            this.toolStripSeparator2,
            this.tbSetting,
            this.tbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1004, 61);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tbQuery
            // 
            this.tbQuery.Image = ((System.Drawing.Image)(resources.GetObject("tbQuery.Image")));
            this.tbQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(46, 58);
            this.tbQuery.Text = "查询";
            this.tbQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbPrint
            // 
            this.tbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tbPrint.Image")));
            this.tbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(46, 58);
            this.tbPrint.Text = "打印";
            this.tbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbRePrint
            // 
            this.tbRePrint.Image = ((System.Drawing.Image)(resources.GetObject("tbRePrint.Image")));
            this.tbRePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRePrint.Name = "tbRePrint";
            this.tbRePrint.Size = new System.Drawing.Size(46, 58);
            this.tbRePrint.Text = "重打";
            this.tbRePrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 61);
            // 
            // tbReset
            // 
            this.tbReset.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResetLong,
            this.ResetShort,
            this.ResetAll});
            this.tbReset.Image = ((System.Drawing.Image)(resources.GetObject("tbReset.Image")));
            this.tbReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbReset.Name = "tbReset";
            this.tbReset.Size = new System.Drawing.Size(55, 58);
            this.tbReset.Text = "重置";
            this.tbReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ResetLong
            // 
            this.ResetLong.Name = "ResetLong";
            this.ResetLong.Size = new System.Drawing.Size(195, 22);
            this.ResetLong.Text = "重置长期医嘱单";
            // 
            // ResetShort
            // 
            this.ResetShort.Name = "ResetShort";
            this.ResetShort.Size = new System.Drawing.Size(195, 22);
            this.ResetShort.Text = "重置临时医嘱单";
            // 
            // ResetAll
            // 
            this.ResetAll.Name = "ResetAll";
            this.ResetAll.Size = new System.Drawing.Size(195, 22);
            this.ResetAll.Text = "全部重置";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 61);
            // 
            // tbSetting
            // 
            this.tbSetting.Enabled = false;
            this.tbSetting.Image = ((System.Drawing.Image)(resources.GetObject("tbSetting.Image")));
            this.tbSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSetting.Name = "tbSetting";
            this.tbSetting.Size = new System.Drawing.Size(46, 58);
            this.tbSetting.Text = "设置";
            this.tbSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbExit
            // 
            this.tbExit.Image = ((System.Drawing.Image)(resources.GetObject("tbExit.Image")));
            this.tbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExit.Name = "tbExit";
            this.tbExit.Size = new System.Drawing.Size(46, 58);
            this.tbExit.Text = "退出";
            this.tbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 659);
            this.panel1.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(215, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(789, 659);
            this.panel3.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpLong);
            this.tabControl1.Controls.Add(this.tpShort);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 11F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(789, 659);
            this.tabControl1.TabIndex = 0;
            // 
            // tpLong
            // 
            this.tpLong.BackColor = System.Drawing.Color.White;
            this.tpLong.Controls.Add(this.pnLongOrder);
            this.tpLong.ImageIndex = 0;
            this.tpLong.Location = new System.Drawing.Point(4, 25);
            this.tpLong.Name = "tpLong";
            this.tpLong.Size = new System.Drawing.Size(781, 630);
            this.tpLong.TabIndex = 0;
            this.tpLong.Text = "长期医嘱单";
            // 
            // pnLongOrder
            // 
            this.pnLongOrder.Controls.Add(this.fpLongOrder);
            this.pnLongOrder.Controls.Add(this.pnLongHeader);
            this.pnLongOrder.Controls.Add(this.pnLongPag);
            this.pnLongOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLongOrder.Location = new System.Drawing.Point(0, 0);
            this.pnLongOrder.Name = "pnLongOrder";
            this.pnLongOrder.Size = new System.Drawing.Size(781, 630);
            this.pnLongOrder.TabIndex = 2;
            // 
            // fpLongOrder
            // 
            this.fpLongOrder.About = "3.0.2004.2005";
            this.fpLongOrder.AccessibleDescription = "fpLongOrder, Sheet1, Row 0, Column 0, ";
            this.fpLongOrder.BackColor = System.Drawing.Color.White;
            this.fpLongOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpLongOrder.FileName = "";
            this.fpLongOrder.Font = new System.Drawing.Font("宋体", 10F);
            this.fpLongOrder.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpLongOrder.IsAutoSaveGridStatus = false;
            this.fpLongOrder.IsCanCustomConfigColumn = false;
            this.fpLongOrder.Location = new System.Drawing.Point(0, 155);
            this.fpLongOrder.Name = "fpLongOrder";
            this.fpLongOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpLongOrder_Sheet1});
            this.fpLongOrder.Size = new System.Drawing.Size(781, 436);
            this.fpLongOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpLongOrder.TabIndex = 1;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpLongOrder.TextTipAppearance = tipAppearance3;
            this.fpLongOrder.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpLongOrder.ActiveSheetChanged += new System.EventHandler(this.fpShortOrder_ActiveSheetChanged);
            this.fpLongOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpLongOrder_MouseUp);
            // 
            // fpLongOrder_Sheet1
            // 
            this.fpLongOrder_Sheet1.Reset();
            this.fpLongOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpLongOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpLongOrder_Sheet1.ColumnCount = 11;
            this.fpLongOrder_Sheet1.ColumnHeader.RowCount = 2;
            this.fpLongOrder_Sheet1.RowCount = 21;
            this.fpLongOrder_Sheet1.RowHeader.ColumnCount = 0;
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "起始";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 2;
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "医嘱";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "医嘱";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医师签名";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "执行护士签名";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 4;
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "停止";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 0).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 0).Value = "日期";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 1).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 1).Value = "时间";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 6).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "日期";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 7).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "时间";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 8).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "医师签名";
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 9).Font = new System.Drawing.Font("宋体", 11F);
            this.fpLongOrder_Sheet1.ColumnHeader.Cells.Get(1, 9).Value = "执行护士签名";
            this.fpLongOrder_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpLongOrder_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpLongOrder_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpLongOrder_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.fpLongOrder_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpLongOrder_Sheet1.ColumnHeader.Rows.Get(1).Height = 57F;
            this.fpLongOrder_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.fpLongOrder_Sheet1.Columns.Get(0).Label = "日期";
            this.fpLongOrder_Sheet1.Columns.Get(0).Width = 56F;
            this.fpLongOrder_Sheet1.Columns.Get(1).Label = "时间";
            this.fpLongOrder_Sheet1.Columns.Get(1).Width = 54F;
            this.fpLongOrder_Sheet1.Columns.Get(2).Width = 18F;
            textCellType3.WordWrap = true;
            this.fpLongOrder_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.fpLongOrder_Sheet1.Columns.Get(3).Width = 240F;
            this.fpLongOrder_Sheet1.Columns.Get(4).Width = 66F;
            this.fpLongOrder_Sheet1.Columns.Get(5).Width = 66F;
            this.fpLongOrder_Sheet1.Columns.Get(6).Label = "日期";
            this.fpLongOrder_Sheet1.Columns.Get(6).Width = 56F;
            this.fpLongOrder_Sheet1.Columns.Get(7).Label = "时间";
            this.fpLongOrder_Sheet1.Columns.Get(7).Width = 56F;
            this.fpLongOrder_Sheet1.Columns.Get(8).Label = "医师签名";
            this.fpLongOrder_Sheet1.Columns.Get(8).Width = 64F;
            this.fpLongOrder_Sheet1.Columns.Get(9).Label = "执行护士签名";
            this.fpLongOrder_Sheet1.Columns.Get(9).Width = 64F;
            this.fpLongOrder_Sheet1.Columns.Get(10).Visible = false;
            this.fpLongOrder_Sheet1.DataAutoHeadings = false;
            this.fpLongOrder_Sheet1.DataAutoSizeColumns = false;
            this.fpLongOrder_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpLongOrder_Sheet1.DefaultStyle.Locked = false;
            this.fpLongOrder_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpLongOrder_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpLongOrder_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpLongOrder_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            this.fpLongOrder_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
            this.fpLongOrder_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpLongOrder_Sheet1.Rows.Default.Height = 39F;
            this.fpLongOrder_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpLongOrder_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.fpLongOrder_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpLongOrder_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            this.fpLongOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnLongHeader
            // 
            this.pnLongHeader.Controls.Add(this.ucOrderBillHeader2);
            this.pnLongHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnLongHeader.Location = new System.Drawing.Point(0, 0);
            this.pnLongHeader.Name = "pnLongHeader";
            this.pnLongHeader.Size = new System.Drawing.Size(781, 155);
            this.pnLongHeader.TabIndex = 0;
            // 
            // ucOrderBillHeader2
            // 
            this.ucOrderBillHeader2.BackColor = System.Drawing.Color.White;
            this.ucOrderBillHeader2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrderBillHeader2.Header = null;
            this.ucOrderBillHeader2.Location = new System.Drawing.Point(0, 0);
            this.ucOrderBillHeader2.Name = "ucOrderBillHeader2";
            this.ucOrderBillHeader2.Size = new System.Drawing.Size(781, 155);
            this.ucOrderBillHeader2.TabIndex = 4;
            // 
            // pnLongPag
            // 
            this.pnLongPag.BackColor = System.Drawing.Color.White;
            this.pnLongPag.Controls.Add(this.label1);
            this.pnLongPag.Controls.Add(this.lblPageLong);
            this.pnLongPag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnLongPag.Location = new System.Drawing.Point(0, 591);
            this.pnLongPag.Name = "pnLongPag";
            this.pnLongPag.Size = new System.Drawing.Size(781, 39);
            this.pnLongPag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnLongPag.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(23, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(600, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "住院医生：      时间：    年  月  日  责任护士：      时间：    年  月  日";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageLong
            // 
            this.lblPageLong.AutoSize = true;
            this.lblPageLong.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageLong.ForeColor = System.Drawing.Color.Red;
            this.lblPageLong.Location = new System.Drawing.Point(677, 3);
            this.lblPageLong.Name = "lblPageLong";
            this.lblPageLong.Size = new System.Drawing.Size(60, 19);
            this.lblPageLong.TabIndex = 0;
            this.lblPageLong.Text = "第1页";
            this.lblPageLong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpShort
            // 
            this.tpShort.BackColor = System.Drawing.Color.White;
            this.tpShort.Controls.Add(this.pnShortOrder);
            this.tpShort.ImageIndex = 1;
            this.tpShort.Location = new System.Drawing.Point(4, 25);
            this.tpShort.Name = "tpShort";
            this.tpShort.Size = new System.Drawing.Size(781, 630);
            this.tpShort.TabIndex = 1;
            this.tpShort.Text = "临时医嘱单";
            // 
            // pnShortOrder
            // 
            this.pnShortOrder.AutoScroll = true;
            this.pnShortOrder.Controls.Add(this.fpShortOrder);
            this.pnShortOrder.Controls.Add(this.pnShortHeader);
            this.pnShortOrder.Controls.Add(this.pnShortPag);
            this.pnShortOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnShortOrder.Location = new System.Drawing.Point(0, 0);
            this.pnShortOrder.Name = "pnShortOrder";
            this.pnShortOrder.Size = new System.Drawing.Size(781, 630);
            this.pnShortOrder.TabIndex = 1;
            // 
            // fpShortOrder
            // 
            this.fpShortOrder.About = "3.0.2004.2005";
            this.fpShortOrder.AccessibleDescription = "fpShortOrder, Sheet1, Row 0, Column 0, ";
            this.fpShortOrder.BackColor = System.Drawing.Color.White;
            this.fpShortOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpShortOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpShortOrder.FileName = "";
            this.fpShortOrder.Font = new System.Drawing.Font("宋体", 10F);
            this.fpShortOrder.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpShortOrder.IsAutoSaveGridStatus = false;
            this.fpShortOrder.IsCanCustomConfigColumn = false;
            this.fpShortOrder.Location = new System.Drawing.Point(0, 157);
            this.fpShortOrder.Name = "fpShortOrder";
            this.fpShortOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpShortOrder_Sheet1});
            this.fpShortOrder.Size = new System.Drawing.Size(781, 434);
            this.fpShortOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpShortOrder.TabIndex = 1;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpShortOrder.TextTipAppearance = tipAppearance4;
            this.fpShortOrder.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpShortOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpShortOrder_MouseUp);
            // 
            // fpShortOrder_Sheet1
            // 
            this.fpShortOrder_Sheet1.Reset();
            this.fpShortOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpShortOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpShortOrder_Sheet1.ColumnCount = 9;
            this.fpShortOrder_Sheet1.RowCount = 22;
            this.fpShortOrder_Sheet1.RowHeader.ColumnCount = 0;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 11F);
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "日 期";
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 11F);
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "时 间";
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 2;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 11F);
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "临  时  医  嘱";
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 11F);
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 11F);
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医 生 签 名";
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 11F);
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "执 行 时 间";
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 11F);
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "核   对  者 签 名";
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "执 行 护 士 签 名";
            this.fpShortOrder_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "组合号";
            this.fpShortOrder_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpShortOrder_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpShortOrder_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.fpShortOrder_Sheet1.ColumnHeader.Rows.Get(0).Height = 57F;
            this.fpShortOrder_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.fpShortOrder_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F);
            this.fpShortOrder_Sheet1.Columns.Get(0).Label = "日 期";
            this.fpShortOrder_Sheet1.Columns.Get(0).Width = 45F;
            this.fpShortOrder_Sheet1.Columns.Get(1).Label = "时 间";
            this.fpShortOrder_Sheet1.Columns.Get(1).Width = 54F;
            this.fpShortOrder_Sheet1.Columns.Get(2).Label = "临  时  医  嘱";
            this.fpShortOrder_Sheet1.Columns.Get(2).Width = 20F;
            textCellType4.WordWrap = true;
            this.fpShortOrder_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.fpShortOrder_Sheet1.Columns.Get(3).Width = 330F;
            this.fpShortOrder_Sheet1.Columns.Get(4).Label = "医 生 签 名";
            this.fpShortOrder_Sheet1.Columns.Get(4).Width = 62F;
            this.fpShortOrder_Sheet1.Columns.Get(6).Label = "核   对  者 签 名";
            this.fpShortOrder_Sheet1.Columns.Get(6).Width = 106F;
            this.fpShortOrder_Sheet1.Columns.Get(7).Label = "执 行 护 士 签 名";
            this.fpShortOrder_Sheet1.Columns.Get(7).Width = 88F;
            this.fpShortOrder_Sheet1.Columns.Get(8).Label = "组合号";
            this.fpShortOrder_Sheet1.Columns.Get(8).Visible = false;
            this.fpShortOrder_Sheet1.Columns.Get(8).Width = 61F;
            this.fpShortOrder_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.DefaultStyle.Locked = false;
            this.fpShortOrder_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpShortOrder_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpShortOrder_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpShortOrder_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            this.fpShortOrder_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpShortOrder_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpShortOrder_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ActiveCaptionText);
            this.fpShortOrder_Sheet1.Rows.Default.Height = 39F;
            this.fpShortOrder_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpShortOrder_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpShortOrder_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            this.fpShortOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnShortHeader
            // 
            this.pnShortHeader.Controls.Add(this.ucOrderBillHeader1);
            this.pnShortHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnShortHeader.Location = new System.Drawing.Point(0, 0);
            this.pnShortHeader.Name = "pnShortHeader";
            this.pnShortHeader.Size = new System.Drawing.Size(781, 157);
            this.pnShortHeader.TabIndex = 0;
            // 
            // ucOrderBillHeader1
            // 
            this.ucOrderBillHeader1.BackColor = System.Drawing.Color.White;
            this.ucOrderBillHeader1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrderBillHeader1.Header = null;
            this.ucOrderBillHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucOrderBillHeader1.Name = "ucOrderBillHeader1";
            this.ucOrderBillHeader1.Size = new System.Drawing.Size(781, 157);
            this.ucOrderBillHeader1.TabIndex = 1;
            // 
            // pnShortPag
            // 
            this.pnShortPag.BackColor = System.Drawing.Color.White;
            this.pnShortPag.Controls.Add(this.label8);
            this.pnShortPag.Controls.Add(this.lblPageShort);
            this.pnShortPag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnShortPag.Location = new System.Drawing.Point(0, 591);
            this.pnShortPag.Name = "pnShortPag";
            this.pnShortPag.Size = new System.Drawing.Size(781, 39);
            this.pnShortPag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnShortPag.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(23, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(600, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "住院医生：      时间：    年  月  日  责任护士：      时间：    年  月  日";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageShort
            // 
            this.lblPageShort.AutoSize = true;
            this.lblPageShort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblPageShort.ForeColor = System.Drawing.Color.Red;
            this.lblPageShort.Location = new System.Drawing.Point(677, 3);
            this.lblPageShort.Name = "lblPageShort";
            this.lblPageShort.Size = new System.Drawing.Size(60, 19);
            this.lblPageShort.TabIndex = 3;
            this.lblPageShort.Text = "第1页";
            this.lblPageShort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(212, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 659);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(212, 659);
            this.panel2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("宋体", 11F);
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 41);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(212, 618);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucQueryInpatientNo1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Font = new System.Drawing.Font("宋体", 11F);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(212, 41);
            this.panel4.TabIndex = 2;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.ForeColor = System.Drawing.Color.Black;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(5, 8);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInpatientNo1.TabIndex = 9;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cmbPrintType);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbPrinter);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.nuRowNum);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nuTop);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nuLeft);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(356, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 40);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印设置";
            // 
            // cmbPrintType
            // 
            this.cmbPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrintType.Enabled = false;
            this.cmbPrintType.FormattingEnabled = true;
            this.cmbPrintType.Items.AddRange(new object[] {
            "印刷套打",
            "白纸套打",
            "出院打印"});
            this.cmbPrintType.Location = new System.Drawing.Point(315, 16);
            this.cmbPrintType.Name = "cmbPrintType";
            this.cmbPrintType.Size = new System.Drawing.Size(92, 20);
            this.cmbPrintType.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(262, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "打印方式";
            // 
            // tbPrinter
            // 
            this.tbPrinter.FormattingEnabled = true;
            this.tbPrinter.Location = new System.Drawing.Point(452, 17);
            this.tbPrinter.Name = "tbPrinter";
            this.tbPrinter.Size = new System.Drawing.Size(148, 20);
            this.tbPrinter.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(412, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "打印机";
            // 
            // nuRowNum
            // 
            this.nuRowNum.Enabled = false;
            this.nuRowNum.Location = new System.Drawing.Point(217, 16);
            this.nuRowNum.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nuRowNum.Name = "nuRowNum";
            this.nuRowNum.ReadOnly = true;
            this.nuRowNum.Size = new System.Drawing.Size(39, 21);
            this.nuRowNum.TabIndex = 5;
            this.nuRowNum.Value = new decimal(new int[] {
            27,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(186, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "行数";
            // 
            // nuTop
            // 
            this.nuTop.Location = new System.Drawing.Point(140, 16);
            this.nuTop.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nuTop.Name = "nuTop";
            this.nuTop.Size = new System.Drawing.Size(39, 21);
            this.nuTop.TabIndex = 3;
            this.nuTop.ValueChanged += new System.EventHandler(this.nuTop_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "上边距";
            // 
            // nuLeft
            // 
            this.nuLeft.Location = new System.Drawing.Point(51, 16);
            this.nuLeft.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nuLeft.Name = "nuLeft";
            this.nuLeft.Size = new System.Drawing.Size(39, 21);
            this.nuLeft.TabIndex = 1;
            this.nuLeft.ValueChanged += new System.EventHandler(this.nuLeft_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "左边距";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(356, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(606, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "红色代表已经打印，如果医嘱已经打印，将只可以作废，不可以删除!";
            // 
            // frmOrderPrint
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(1004, 746);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 12.5F);
            this.Name = "frmOrderPrint";
            this.Text = "医嘱单打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOrderPrint_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOrderPrint_FormClosed);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.statusBar1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpLong.ResumeLayout(false);
            this.pnLongOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpLongOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpLongOrder_Sheet1)).EndInit();
            this.pnLongHeader.ResumeLayout(false);
            this.pnLongPag.ResumeLayout(false);
            this.pnLongPag.PerformLayout();
            this.tpShort.ResumeLayout(false);
            this.pnShortOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpShortOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpShortOrder_Sheet1)).EndInit();
            this.pnShortHeader.ResumeLayout(false);
            this.pnShortPag.ResumeLayout(false);
            this.pnShortPag.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuRowNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuLeft)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbQuery;
        private System.Windows.Forms.ToolStripButton tbPrint;
        private System.Windows.Forms.ToolStripButton tbRePrint;
        private System.Windows.Forms.ToolStripButton tbExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLong;
        private System.Windows.Forms.Panel pnLongOrder;
        private System.Windows.Forms.TabPage tpShort;
        private System.Windows.Forms.Panel pnShortOrder;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpShortOrder;
        private FarPoint.Win.Spread.SheetView fpShortOrder_Sheet1;
        private System.Windows.Forms.Panel pnShortHeader;
        private System.Windows.Forms.Label lblPageShort;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnLongHeader;
        private System.Windows.Forms.Label lblPageLong;
        private System.Windows.Forms.ToolStripButton tbSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnShortPag;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnLongPag;
        private System.Windows.Forms.ToolStripDropDownButton tbReset;
        private System.Windows.Forms.ToolStripMenuItem ResetLong;
        private System.Windows.Forms.ToolStripMenuItem ResetShort;
        private System.Windows.Forms.ToolStripMenuItem ResetAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbPrintType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox tbPrinter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nuRowNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nuTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nuLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ucOrderBillHeader ucOrderBillHeader2;
        private ucOrderBillHeader ucOrderBillHeader1;
        //private FS.FrameWork.WinForms.Controls.NeuSpread fpLongOrder;
        private FarPoint.Win.Spread.SheetView fpLongOrder_Sheet1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpLongOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;

    }
}