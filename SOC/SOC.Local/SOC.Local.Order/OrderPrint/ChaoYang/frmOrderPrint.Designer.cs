namespace FS.SOC.Local.Order.ChaoYang.OrderPrint
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ucOrderBillHeader2 = new FS.SOC.Local.Order.ChaoYang.OrderPrint.ucOrderBillHeader();
            this.panel10 = new System.Windows.Forms.Panel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblPageLong = new System.Windows.Forms.Label();
            this.tpShort = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.neuSpread2 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel8 = new System.Windows.Forms.Panel();
            this.ucOrderBillHeader1 = new FS.SOC.Local.Order.ChaoYang.OrderPrint.ucOrderBillHeader();
            this.panel9 = new System.Windows.Forms.Panel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpLong.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.panel5.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.tpShort.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).BeginInit();
            this.panel8.SuspendLayout();
            this.neuPanel1.SuspendLayout();
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
            this.tbReset.Enabled = false;
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
            this.tpLong.Controls.Add(this.panel6);
            this.tpLong.ImageIndex = 0;
            this.tpLong.Location = new System.Drawing.Point(4, 25);
            this.tpLong.Name = "tpLong";
            this.tpLong.Size = new System.Drawing.Size(781, 630);
            this.tpLong.TabIndex = 0;
            this.tpLong.Text = "长期医嘱单";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.neuSpread1);
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Controls.Add(this.neuPanel2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(781, 630);
            this.panel6.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 10F);
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 155);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(781, 452);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.ActiveSheetChanged += new System.EventHandler(this.fpSpread2_ActiveSheetChanged);
            this.neuSpread1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.neuSpread1_MouseUp);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 11;
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 2;
            this.neuSpread1_Sheet1.RowCount = 21;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "起始";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "医嘱";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "医嘱";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医师签名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "执行护士签名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "停止";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 0).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 0).Value = "日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 1).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 1).Value = "时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "医师签名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).Value = "执行护士签名";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(1).Height = 57F;
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "日期";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 56F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "时间";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 54F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 18F;
            textCellType1.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 240F;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "日期";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 56F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "时间";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 56F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "医师签名";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 64F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "执行护士签名";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 64F;
            this.neuSpread1_Sheet1.Columns.Get(10).Visible = false;
            this.neuSpread1_Sheet1.DataAutoHeadings = false;
            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 39F;
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.neuSpread1_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ucOrderBillHeader2);
            this.panel5.Controls.Add(this.panel10);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(781, 155);
            this.panel5.TabIndex = 0;
            // 
            // ucOrderBillHeader2
            // 
            this.ucOrderBillHeader2.BackColor = System.Drawing.Color.White;
            this.ucOrderBillHeader2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrderBillHeader2.Header = null;
            this.ucOrderBillHeader2.Location = new System.Drawing.Point(0, 13);
            this.ucOrderBillHeader2.Name = "ucOrderBillHeader2";
            this.ucOrderBillHeader2.PInfo = ((FS.HISFC.Models.RADT.PatientInfo)(resources.GetObject("ucOrderBillHeader2.PInfo")));
            this.ucOrderBillHeader2.Size = new System.Drawing.Size(781, 142);
            this.ucOrderBillHeader2.TabIndex = 4;
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(781, 13);
            this.panel10.TabIndex = 3;
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.White;
            this.neuPanel2.Controls.Add(this.label1);
            this.neuPanel2.Controls.Add(this.lblPageLong);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 607);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(781, 23);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // lblPageLong
            // 
            this.lblPageLong.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageLong.ForeColor = System.Drawing.Color.Red;
            this.lblPageLong.Location = new System.Drawing.Point(358, 0);
            this.lblPageLong.Name = "lblPageLong";
            this.lblPageLong.Size = new System.Drawing.Size(28, 23);
            this.lblPageLong.TabIndex = 0;
            this.lblPageLong.Text = "1";
            this.lblPageLong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpShort
            // 
            this.tpShort.BackColor = System.Drawing.Color.White;
            this.tpShort.Controls.Add(this.panel7);
            this.tpShort.ImageIndex = 1;
            this.tpShort.Location = new System.Drawing.Point(4, 25);
            this.tpShort.Name = "tpShort";
            this.tpShort.Size = new System.Drawing.Size(781, 630);
            this.tpShort.TabIndex = 1;
            this.tpShort.Text = "临时医嘱单";
            // 
            // panel7
            // 
            this.panel7.AutoScroll = true;
            this.panel7.Controls.Add(this.neuSpread2);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.neuPanel1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(781, 630);
            this.panel7.TabIndex = 1;
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "3.0.2004.2005";
            this.neuSpread2.AccessibleDescription = "neuSpread2, Sheet1, Row 0, Column 0, ";
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread2.FileName = "";
            this.neuSpread2.Font = new System.Drawing.Font("宋体", 10F);
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(0, 157);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread2_Sheet1});
            this.neuSpread2.Size = new System.Drawing.Size(781, 450);
            this.neuSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance2;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.neuSpread2_MouseUp);
            // 
            // neuSpread2_Sheet1
            // 
            this.neuSpread2_Sheet1.Reset();
            this.neuSpread2_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread2_Sheet1.ColumnCount = 9;
            this.neuSpread2_Sheet1.RowCount = 22;
            this.neuSpread2_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "日 期";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "时 间";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 2;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "临  时  医  嘱";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医 生 签 名";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "执 行 时 间";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 11F);
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "核   对  者 签 名";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "执 行 护 士 签 名";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "组合号";
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread2_Sheet1.ColumnHeader.Rows.Get(0).Height = 57F;
            this.neuSpread2_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread2_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread2_Sheet1.Columns.Get(0).Label = "日 期";
            this.neuSpread2_Sheet1.Columns.Get(0).Width = 45F;
            this.neuSpread2_Sheet1.Columns.Get(1).Label = "时 间";
            this.neuSpread2_Sheet1.Columns.Get(1).Width = 54F;
            this.neuSpread2_Sheet1.Columns.Get(2).Label = "临  时  医  嘱";
            this.neuSpread2_Sheet1.Columns.Get(2).Width = 20F;
            textCellType2.WordWrap = true;
            this.neuSpread2_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.neuSpread2_Sheet1.Columns.Get(3).Width = 330F;
            this.neuSpread2_Sheet1.Columns.Get(4).Label = "医 生 签 名";
            this.neuSpread2_Sheet1.Columns.Get(4).Width = 62F;
            this.neuSpread2_Sheet1.Columns.Get(6).Label = "核   对  者 签 名";
            this.neuSpread2_Sheet1.Columns.Get(6).Width = 106F;
            this.neuSpread2_Sheet1.Columns.Get(7).Label = "执 行 护 士 签 名";
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 88F;
            this.neuSpread2_Sheet1.Columns.Get(8).Label = "组合号";
            this.neuSpread2_Sheet1.Columns.Get(8).Visible = false;
            this.neuSpread2_Sheet1.Columns.Get(8).Width = 61F;
            this.neuSpread2_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread2_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread2_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            this.neuSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.neuSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread2_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ActiveCaptionText);
            this.neuSpread2_Sheet1.Rows.Default.Height = 39F;
            this.neuSpread2_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread2_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread2_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.ucOrderBillHeader1);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(781, 157);
            this.panel8.TabIndex = 0;
            // 
            // ucOrderBillHeader1
            // 
            this.ucOrderBillHeader1.BackColor = System.Drawing.Color.White;
            this.ucOrderBillHeader1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrderBillHeader1.Header = null;
            this.ucOrderBillHeader1.Location = new System.Drawing.Point(0, 13);
            this.ucOrderBillHeader1.Name = "ucOrderBillHeader1";
            this.ucOrderBillHeader1.PInfo = ((FS.HISFC.Models.RADT.PatientInfo)(resources.GetObject("ucOrderBillHeader1.PInfo")));
            this.ucOrderBillHeader1.Size = new System.Drawing.Size(781, 144);
            this.ucOrderBillHeader1.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(781, 13);
            this.panel9.TabIndex = 0;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.label8);
            this.neuPanel1.Controls.Add(this.lblPageShort);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(0, 607);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(781, 23);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // lblPageShort
            // 
            this.lblPageShort.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageShort.ForeColor = System.Drawing.Color.Red;
            this.lblPageShort.Location = new System.Drawing.Point(365, 0);
            this.lblPageShort.Name = "lblPageShort";
            this.lblPageShort.Size = new System.Drawing.Size(32, 23);
            this.lblPageShort.TabIndex = 3;
            this.lblPageShort.Text = "1";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(466, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "确认无误后，请签名： ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(460, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(176, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "确认无误后，请签名： ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.tpShort.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).EndInit();
            this.panel8.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TabPage tpShort;
        private System.Windows.Forms.Panel panel7;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FarPoint.Win.Spread.SheetView neuSpread2_Sheet1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblPageShort;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblPageLong;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ToolStripButton tbSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
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
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;

    }
}