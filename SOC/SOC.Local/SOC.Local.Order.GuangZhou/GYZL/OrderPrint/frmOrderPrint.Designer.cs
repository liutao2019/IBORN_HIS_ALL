namespace FS.SOC.Local.Order.GuangZhou.GYZL.OrderPrint
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderPrint));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbQuery = new System.Windows.Forms.ToolStripButton();
            this.tbPrint = new System.Windows.Forms.ToolStripButton();
            this.tbRePrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbReset = new System.Windows.Forms.ToolStripDropDownButton();
            this.ResetLong = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetShort = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetCurrentLong = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetCurrentShort = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshLong = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshShort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSetting = new System.Windows.Forms.ToolStripButton();
            this.tbExit = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLong = new System.Windows.Forms.TabPage();
            this.pnLongOrder = new System.Windows.Forms.Panel();
            this.fpLongOrder = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpLongOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnLongHeader = new System.Windows.Forms.Panel();
            this.ucLongOrderBillHeader = new FS.SOC.Local.Order.GuangZhou.GYZL.OrderPrint.ucOrderBillHeader();
            this.pnLongPag = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblLongPage = new System.Windows.Forms.Label();
            this.lblPageLong = new System.Windows.Forms.Label();
            this.tpShort = new System.Windows.Forms.TabPage();
            this.pnShortOrder = new System.Windows.Forms.Panel();
            this.fpShortOrder = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpShortOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnShortHeader = new System.Windows.Forms.Panel();
            this.ucShortOrderBillHeader = new FS.SOC.Local.Order.GuangZhou.GYZL.OrderPrint.ucOrderBillHeader();
            this.pnShortPag = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblShortPageFoot = new System.Windows.Forms.Label();
            this.lblPageShort = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxPreview = new System.Windows.Forms.CheckBox();
            this.cmbPrintType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
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
            this.statusBar1.Location = new System.Drawing.Point(0, 672);
            this.statusBar1.Size = new System.Drawing.Size(1020, 26);
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
            this.toolStrip1.Size = new System.Drawing.Size(1020, 61);
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
            this.ResetAll,
            this.ResetCurrentLong,
            this.ResetCurrentShort,
            this.RefreshLong,
            this.RefreshShort});
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
            this.ResetLong.Size = new System.Drawing.Size(246, 22);
            this.ResetLong.Text = "重置长期医嘱单";
            // 
            // ResetShort
            // 
            this.ResetShort.Name = "ResetShort";
            this.ResetShort.Size = new System.Drawing.Size(246, 22);
            this.ResetShort.Text = "重置临时医嘱单";
            // 
            // ResetAll
            // 
            this.ResetAll.Name = "ResetAll";
            this.ResetAll.Size = new System.Drawing.Size(246, 22);
            this.ResetAll.Text = "全部重置";
            // 
            // ResetCurrentLong
            // 
            this.ResetCurrentLong.Name = "ResetCurrentLong";
            this.ResetCurrentLong.Size = new System.Drawing.Size(246, 22);
            this.ResetCurrentLong.Text = "重置长嘱当前页";
            // 
            // ResetCurrentShort
            // 
            this.ResetCurrentShort.Name = "ResetCurrentShort";
            this.ResetCurrentShort.Size = new System.Drawing.Size(246, 22);
            this.ResetCurrentShort.Text = "重置临嘱当前页";
            // 
            // RefreshLong
            // 
            this.RefreshLong.Name = "RefreshLong";
            this.RefreshLong.Size = new System.Drawing.Size(246, 22);
            this.RefreshLong.Text = "刷新所有长嘱打印状态";
            // 
            // RefreshShort
            // 
            this.RefreshShort.Name = "RefreshShort";
            this.RefreshShort.Size = new System.Drawing.Size(246, 22);
            this.RefreshShort.Text = "刷新所有临嘱打印状态";
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
            this.panel1.Size = new System.Drawing.Size(1020, 611);
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
            this.panel3.Size = new System.Drawing.Size(805, 611);
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
            this.tabControl1.Size = new System.Drawing.Size(805, 611);
            this.tabControl1.TabIndex = 0;
            // 
            // tpLong
            // 
            this.tpLong.BackColor = System.Drawing.Color.White;
            this.tpLong.Controls.Add(this.pnLongOrder);
            this.tpLong.ImageIndex = 0;
            this.tpLong.Location = new System.Drawing.Point(4, 25);
            this.tpLong.Name = "tpLong";
            this.tpLong.Size = new System.Drawing.Size(797, 582);
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
            this.pnLongOrder.Size = new System.Drawing.Size(797, 582);
            this.pnLongOrder.TabIndex = 2;
            // 
            // fpLongOrder
            // 
            this.fpLongOrder.About = "3.0.2004.2005";
            this.fpLongOrder.AccessibleDescription = "fpLongOrder, Sheet1, Row 0, Column 0, ";
            this.fpLongOrder.AllowColumnMove = true;
            this.fpLongOrder.BackColor = System.Drawing.Color.White;
            this.fpLongOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpLongOrder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpLongOrder.Location = new System.Drawing.Point(0, 132);
            this.fpLongOrder.Name = "fpLongOrder";
            this.fpLongOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpLongOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpLongOrder_Sheet1});
            this.fpLongOrder.Size = new System.Drawing.Size(797, 411);
            this.fpLongOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpLongOrder.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpLongOrder.TextTipAppearance = tipAppearance1;
            // 
            // fpLongOrder_Sheet1
            // 
            this.fpLongOrder_Sheet1.Reset();
            this.fpLongOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpLongOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpLongOrder_Sheet1.Columns.Get(3).Width = 92F;
            this.fpLongOrder_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpLongOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnLongHeader
            // 
            this.pnLongHeader.Controls.Add(this.ucLongOrderBillHeader);
            this.pnLongHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnLongHeader.Location = new System.Drawing.Point(0, 0);
            this.pnLongHeader.Name = "pnLongHeader";
            this.pnLongHeader.Size = new System.Drawing.Size(797, 132);
            this.pnLongHeader.TabIndex = 0;
            // 
            // ucLongOrderBillHeader
            // 
            this.ucLongOrderBillHeader.BackColor = System.Drawing.Color.White;
            this.ucLongOrderBillHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLongOrderBillHeader.Header = null;
            this.ucLongOrderBillHeader.Location = new System.Drawing.Point(0, 0);
            this.ucLongOrderBillHeader.Name = "ucLongOrderBillHeader";
            this.ucLongOrderBillHeader.Size = new System.Drawing.Size(797, 132);
            this.ucLongOrderBillHeader.TabIndex = 0;
            // 
            // pnLongPag
            // 
            this.pnLongPag.BackColor = System.Drawing.Color.White;
            this.pnLongPag.Controls.Add(this.lblLongPage);
            this.pnLongPag.Controls.Add(this.lblPageLong);
            this.pnLongPag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnLongPag.Location = new System.Drawing.Point(0, 543);
            this.pnLongPag.Name = "pnLongPag";
            this.pnLongPag.Size = new System.Drawing.Size(797, 39);
            this.pnLongPag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnLongPag.TabIndex = 2;
            // 
            // lblLongPage
            // 
            this.lblLongPage.AutoSize = true;
            this.lblLongPage.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLongPage.ForeColor = System.Drawing.Color.Red;
            this.lblLongPage.Location = new System.Drawing.Point(392, 3);
            this.lblLongPage.Name = "lblLongPage";
            this.lblLongPage.Size = new System.Drawing.Size(109, 19);
            this.lblLongPage.TabIndex = 1;
            this.lblLongPage.Text = "长期医嘱单";
            this.lblLongPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageLong
            // 
            this.lblPageLong.AutoSize = true;
            this.lblPageLong.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageLong.ForeColor = System.Drawing.Color.Red;
            this.lblPageLong.Location = new System.Drawing.Point(325, 3);
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
            this.tpShort.Size = new System.Drawing.Size(797, 582);
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
            this.pnShortOrder.Size = new System.Drawing.Size(797, 582);
            this.pnShortOrder.TabIndex = 1;
            // 
            // fpShortOrder
            // 
            this.fpShortOrder.About = "3.0.2004.2005";
            this.fpShortOrder.AccessibleDescription = "fpShortOrder, Sheet1, Row 0, Column 0, ";
            this.fpShortOrder.AllowColumnMove = true;
            this.fpShortOrder.BackColor = System.Drawing.Color.White;
            this.fpShortOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpShortOrder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpShortOrder.Location = new System.Drawing.Point(0, 132);
            this.fpShortOrder.Name = "fpShortOrder";
            this.fpShortOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpShortOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpShortOrder_Sheet1});
            this.fpShortOrder.Size = new System.Drawing.Size(797, 411);
            this.fpShortOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpShortOrder.TabIndex = 4;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpShortOrder.TextTipAppearance = tipAppearance2;
            // 
            // fpShortOrder_Sheet1
            // 
            this.fpShortOrder_Sheet1.Reset();
            this.fpShortOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpShortOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpShortOrder_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpShortOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnShortHeader
            // 
            this.pnShortHeader.Controls.Add(this.ucShortOrderBillHeader);
            this.pnShortHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnShortHeader.Location = new System.Drawing.Point(0, 0);
            this.pnShortHeader.Name = "pnShortHeader";
            this.pnShortHeader.Size = new System.Drawing.Size(797, 132);
            this.pnShortHeader.TabIndex = 0;
            // 
            // ucShortOrderBillHeader
            // 
            this.ucShortOrderBillHeader.BackColor = System.Drawing.Color.White;
            this.ucShortOrderBillHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucShortOrderBillHeader.Header = null;
            this.ucShortOrderBillHeader.Location = new System.Drawing.Point(0, 0);
            this.ucShortOrderBillHeader.Name = "ucShortOrderBillHeader";
            this.ucShortOrderBillHeader.Size = new System.Drawing.Size(797, 132);
            this.ucShortOrderBillHeader.TabIndex = 1;
            // 
            // pnShortPag
            // 
            this.pnShortPag.BackColor = System.Drawing.Color.White;
            this.pnShortPag.Controls.Add(this.lblShortPageFoot);
            this.pnShortPag.Controls.Add(this.lblPageShort);
            this.pnShortPag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnShortPag.Location = new System.Drawing.Point(0, 543);
            this.pnShortPag.Name = "pnShortPag";
            this.pnShortPag.Size = new System.Drawing.Size(797, 39);
            this.pnShortPag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnShortPag.TabIndex = 2;
            // 
            // lblShortPageFoot
            // 
            this.lblShortPageFoot.AutoSize = true;
            this.lblShortPageFoot.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShortPageFoot.ForeColor = System.Drawing.Color.Red;
            this.lblShortPageFoot.Location = new System.Drawing.Point(387, 3);
            this.lblShortPageFoot.Name = "lblShortPageFoot";
            this.lblShortPageFoot.Size = new System.Drawing.Size(109, 19);
            this.lblShortPageFoot.TabIndex = 4;
            this.lblShortPageFoot.Text = "临时医嘱单";
            this.lblShortPageFoot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageShort
            // 
            this.lblPageShort.AutoSize = true;
            this.lblPageShort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblPageShort.ForeColor = System.Drawing.Color.Red;
            this.lblPageShort.Location = new System.Drawing.Point(321, 3);
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
            this.splitter1.Size = new System.Drawing.Size(3, 611);
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
            this.panel2.Size = new System.Drawing.Size(212, 611);
            this.panel2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("宋体", 11F);
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 41);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(212, 570);
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
            //this.ucQueryInpatientNo1.IsDeptOnly = true;
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
            this.groupBox1.Controls.Add(this.cbxPreview);
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
            this.groupBox1.Location = new System.Drawing.Point(326, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(682, 40);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印设置";
            // 
            // cbxPreview
            // 
            this.cbxPreview.AutoSize = true;
            this.cbxPreview.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxPreview.Location = new System.Drawing.Point(611, 18);
            this.cbxPreview.Name = "cbxPreview";
            this.cbxPreview.Size = new System.Drawing.Size(50, 16);
            this.cbxPreview.TabIndex = 10;
            this.cbxPreview.Text = "预览";
            this.cbxPreview.UseVisualStyleBackColor = true;
            this.cbxPreview.Visible = false;
            // 
            // cmbPrintType
            // 
            this.cmbPrintType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPrintType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrintType.IsEnter2Tab = false;
            this.cmbPrintType.IsFlat = false;
            this.cmbPrintType.IsLike = true;
            this.cmbPrintType.IsListOnly = false;
            this.cmbPrintType.IsPopForm = true;
            this.cmbPrintType.IsShowCustomerList = false;
            this.cmbPrintType.IsShowID = false;
            this.cmbPrintType.Location = new System.Drawing.Point(315, 16);
            this.cmbPrintType.Name = "cmbPrintType";
            this.cmbPrintType.PopForm = null;
            this.cmbPrintType.ShowCustomerList = false;
            this.cmbPrintType.ShowID = false;
            this.cmbPrintType.Size = new System.Drawing.Size(92, 20);
            this.cmbPrintType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPrintType.TabIndex = 9;
            this.cmbPrintType.Tag = "";
            this.cmbPrintType.ToolBarUse = false;
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
            this.label2.Location = new System.Drawing.Point(326, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(606, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "红色代表已经打印，如果医嘱已经打印，将只可以作废，不可以删除!";
            // 
            // frmOrderPrint
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(1020, 698);
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
        private ucOrderBillHeader ucShortOrderBillHeader;
        //private FS.FrameWork.WinForms.Controls.NeuSpread fpLongOrder;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.SOC.Windows.Forms.FpSpread fpShortOrder;
        private FarPoint.Win.Spread.SheetView fpShortOrder_Sheet1;
        private FS.SOC.Windows.Forms.FpSpread fpLongOrder;
        private FarPoint.Win.Spread.SheetView fpLongOrder_Sheet1;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbPrintType;
        private ucOrderBillHeader ucLongOrderBillHeader;
        private System.Windows.Forms.CheckBox cbxPreview;
        private System.Windows.Forms.ToolStripMenuItem ResetCurrentLong;
        private System.Windows.Forms.ToolStripMenuItem ResetCurrentShort;
        private System.Windows.Forms.ToolStripMenuItem RefreshLong;
        private System.Windows.Forms.ToolStripMenuItem RefreshShort;
        private System.Windows.Forms.Label lblLongPage;
        private System.Windows.Forms.Label lblShortPageFoot;

    }
}