namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.OrderPrint
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
            FarPoint.Win.Spread.TipAppearance tipAppearance22 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance23 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance24 = new FarPoint.Win.Spread.TipAppearance();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbQuery = new System.Windows.Forms.ToolStripButton();
            this.tbAllPrint = new System.Windows.Forms.ToolStripButton();
            this.tbPrint = new System.Windows.Forms.ToolStripButton();
            this.tbRePrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbReset = new System.Windows.Forms.ToolStripDropDownButton();
            this.ResetLong = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetShort = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetUCUL = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetCurrentLong = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetCurrentShort = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetCurrentUCUL = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshLong = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshShort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSetting = new System.Windows.Forms.ToolStripButton();
            this.tbDeleteLongProfile = new System.Windows.Forms.ToolStripButton();
            this.tbDeleteShortProfile = new System.Windows.Forms.ToolStripButton();
            this.tbExit = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tcControl = new System.Windows.Forms.TabControl();
            this.tpLong = new System.Windows.Forms.TabPage();
            this.pnLongOrder = new System.Windows.Forms.Panel();
            this.fpLongOrder = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpLongOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnLongHeader = new System.Windows.Forms.Panel();
            this.ucLongOrderBillHeader = new FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.OrderPrint.ucOrderBillHeader();
            this.pnLongPag = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPageLong = new System.Windows.Forms.Label();
            this.tpShort = new System.Windows.Forms.TabPage();
            this.pnShortOrder = new System.Windows.Forms.Panel();
            this.fpShortOrder = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpShortOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnShortHeader = new System.Windows.Forms.Panel();
            this.ucShortOrderBillHeader = new FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.OrderPrint.ucOrderBillHeader();
            this.pnShortPag = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPageShort = new System.Windows.Forms.Label();
            this.tpUCUL = new System.Windows.Forms.TabPage();
            this.tpOperate = new System.Windows.Forms.TabPage();
            this.pnOperates = new System.Windows.Forms.Panel();
            this.fpOperateOrder = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpOperateOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ucOperatePanel = new System.Windows.Forms.Panel();
            this.ucOperateBillHeader = new FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.OrderPrint.ucOrderBillHeader();
            this.pnOperateOrder = new System.Windows.Forms.Panel();
            this.pnOperatePag = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblPageOperatets = new System.Windows.Forms.Label();
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
            this.label13 = new System.Windows.Forms.Label();
            this.lblPageOperate = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tcControl.SuspendLayout();
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
            this.tpOperate.SuspendLayout();
            this.pnOperates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpOperateOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOperateOrder_Sheet1)).BeginInit();
            this.ucOperatePanel.SuspendLayout();
            this.pnOperatePag.SuspendLayout();
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
            this.statusBar1.Size = new System.Drawing.Size(1062, 26);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 12.5F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbQuery,
            this.tbAllPrint,
            this.tbPrint,
            this.tbRePrint,
            this.toolStripSeparator1,
            this.tbReset,
            this.toolStripSeparator2,
            this.tbSetting,
            this.tbDeleteLongProfile,
            this.tbDeleteShortProfile,
            this.tbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1062, 61);
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
            // tbAllPrint
            // 
            this.tbAllPrint.Image = ((System.Drawing.Image)(resources.GetObject("tbAllPrint.Image")));
            this.tbAllPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAllPrint.Name = "tbAllPrint";
            this.tbAllPrint.Size = new System.Drawing.Size(80, 58);
            this.tbAllPrint.Text = "打印全部";
            this.tbAllPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbAllPrint.ToolTipText = "在原有未打印完毕的医嘱单上，接着打印！";
            // 
            // tbPrint
            // 
            this.tbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tbPrint.Image")));
            this.tbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(46, 58);
            this.tbPrint.Text = "续打";
            this.tbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbPrint.ToolTipText = "在原有未打印完毕的医嘱单上，接着打印！";
            // 
            // tbRePrint
            // 
            this.tbRePrint.Image = ((System.Drawing.Image)(resources.GetObject("tbRePrint.Image")));
            this.tbRePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRePrint.Name = "tbRePrint";
            this.tbRePrint.Size = new System.Drawing.Size(46, 58);
            this.tbRePrint.Text = "重打";
            this.tbRePrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbRePrint.ToolTipText = "已经打印过的医嘱单，重复打印！";
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
            this.ResetUCUL,
            this.ResetAll,
            this.ResetCurrentLong,
            this.ResetCurrentShort,
            this.ResetCurrentUCUL,
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
            this.ResetLong.Size = new System.Drawing.Size(263, 22);
            this.ResetLong.Text = "重置长期医嘱单";
            // 
            // ResetShort
            // 
            this.ResetShort.Name = "ResetShort";
            this.ResetShort.Size = new System.Drawing.Size(263, 22);
            this.ResetShort.Text = "重置临时医嘱单";
            // 
            // ResetUCUL
            // 
            this.ResetUCUL.Name = "ResetUCUL";
            this.ResetUCUL.Size = new System.Drawing.Size(263, 22);
            this.ResetUCUL.Text = "重置检查检验医嘱单";
            // 
            // ResetAll
            // 
            this.ResetAll.Name = "ResetAll";
            this.ResetAll.Size = new System.Drawing.Size(263, 22);
            this.ResetAll.Text = "全部重置";
            // 
            // ResetCurrentLong
            // 
            this.ResetCurrentLong.Name = "ResetCurrentLong";
            this.ResetCurrentLong.Size = new System.Drawing.Size(263, 22);
            this.ResetCurrentLong.Text = "重置长嘱当前页";
            // 
            // ResetCurrentShort
            // 
            this.ResetCurrentShort.Name = "ResetCurrentShort";
            this.ResetCurrentShort.Size = new System.Drawing.Size(263, 22);
            this.ResetCurrentShort.Text = "重置临嘱当前页";
            // 
            // ResetCurrentUCUL
            // 
            this.ResetCurrentUCUL.Name = "ResetCurrentUCUL";
            this.ResetCurrentUCUL.Size = new System.Drawing.Size(263, 22);
            this.ResetCurrentUCUL.Text = "重置检查检验医嘱当前页";
            // 
            // RefreshLong
            // 
            this.RefreshLong.Name = "RefreshLong";
            this.RefreshLong.Size = new System.Drawing.Size(263, 22);
            this.RefreshLong.Text = "刷新所有长嘱打印状态";
            // 
            // RefreshShort
            // 
            this.RefreshShort.Name = "RefreshShort";
            this.RefreshShort.Size = new System.Drawing.Size(263, 22);
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
            // tbDeleteLongProfile
            // 
            this.tbDeleteLongProfile.Image = ((System.Drawing.Image)(resources.GetObject("tbDeleteLongProfile.Image")));
            this.tbDeleteLongProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDeleteLongProfile.Name = "tbDeleteLongProfile";
            this.tbDeleteLongProfile.Size = new System.Drawing.Size(114, 58);
            this.tbDeleteLongProfile.Text = "删除长嘱配置";
            this.tbDeleteLongProfile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbDeleteLongProfile.ToolTipText = "删除长期医嘱的界面显示配置文件！";
            // 
            // tbDeleteShortProfile
            // 
            this.tbDeleteShortProfile.Image = ((System.Drawing.Image)(resources.GetObject("tbDeleteShortProfile.Image")));
            this.tbDeleteShortProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDeleteShortProfile.Name = "tbDeleteShortProfile";
            this.tbDeleteShortProfile.Size = new System.Drawing.Size(114, 58);
            this.tbDeleteShortProfile.Text = "删除临嘱配置";
            this.tbDeleteShortProfile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbDeleteShortProfile.ToolTipText = "删除临时医嘱的界面显示配置文件！";
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
            this.panel1.Size = new System.Drawing.Size(1062, 611);
            this.panel1.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.tcControl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(215, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(847, 611);
            this.panel3.TabIndex = 2;
            // 
            // tcControl
            // 
            this.tcControl.Controls.Add(this.tpLong);
            this.tcControl.Controls.Add(this.tpShort);
            this.tcControl.Controls.Add(this.tpUCUL);
            this.tcControl.Controls.Add(this.tpOperate);
            this.tcControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcControl.Font = new System.Drawing.Font("宋体", 11F);
            this.tcControl.Location = new System.Drawing.Point(0, 0);
            this.tcControl.Name = "tcControl";
            this.tcControl.SelectedIndex = 0;
            this.tcControl.Size = new System.Drawing.Size(847, 611);
            this.tcControl.TabIndex = 0;
            // 
            // tpLong
            // 
            this.tpLong.BackColor = System.Drawing.Color.White;
            this.tpLong.Controls.Add(this.pnLongOrder);
            this.tpLong.ImageIndex = 0;
            this.tpLong.Location = new System.Drawing.Point(4, 25);
            this.tpLong.Name = "tpLong";
            this.tpLong.Size = new System.Drawing.Size(839, 582);
            this.tpLong.TabIndex = 0;
            this.tpLong.Text = "长期医嘱单";
            this.tpLong.UseVisualStyleBackColor = true;
            // 
            // pnLongOrder
            // 
            this.pnLongOrder.Controls.Add(this.fpLongOrder);
            this.pnLongOrder.Controls.Add(this.pnLongHeader);
            this.pnLongOrder.Controls.Add(this.pnLongPag);
            this.pnLongOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLongOrder.Location = new System.Drawing.Point(0, 0);
            this.pnLongOrder.Name = "pnLongOrder";
            this.pnLongOrder.Size = new System.Drawing.Size(839, 582);
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
            this.fpLongOrder.Size = new System.Drawing.Size(839, 411);
            this.fpLongOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpLongOrder.TabIndex = 4;
            tipAppearance22.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance22.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance22.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpLongOrder.TextTipAppearance = tipAppearance22;
            // 
            // fpLongOrder_Sheet1
            // 
            this.fpLongOrder_Sheet1.Reset();
            this.fpLongOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpLongOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpLongOrder_Sheet1.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpLongOrder_Sheet1.Cells.Get(0, 3).Value = "哈哈哈哈";
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
            this.pnLongHeader.Size = new System.Drawing.Size(839, 132);
            this.pnLongHeader.TabIndex = 0;
            // 
            // ucLongOrderBillHeader
            // 
            this.ucLongOrderBillHeader.BackColor = System.Drawing.Color.White;
            this.ucLongOrderBillHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLongOrderBillHeader.Header = null;
            this.ucLongOrderBillHeader.Location = new System.Drawing.Point(0, 0);
            this.ucLongOrderBillHeader.Name = "ucLongOrderBillHeader";
            this.ucLongOrderBillHeader.Size = new System.Drawing.Size(839, 132);
            this.ucLongOrderBillHeader.TabIndex = 0;
            // 
            // pnLongPag
            // 
            this.pnLongPag.BackColor = System.Drawing.Color.White;
            this.pnLongPag.Controls.Add(this.label8);
            this.pnLongPag.Controls.Add(this.label1);
            this.pnLongPag.Controls.Add(this.lblPageLong);
            this.pnLongPag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnLongPag.Location = new System.Drawing.Point(0, 543);
            this.pnLongPag.Name = "pnLongPag";
            this.pnLongPag.Size = new System.Drawing.Size(839, 39);
            this.pnLongPag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnLongPag.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(496, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "责任护士签名：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(172, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "住院医生签名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageLong
            // 
            this.lblPageLong.AutoSize = true;
            this.lblPageLong.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageLong.ForeColor = System.Drawing.Color.Red;
            this.lblPageLong.Location = new System.Drawing.Point(21, 10);
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
            this.tpShort.Size = new System.Drawing.Size(839, 582);
            this.tpShort.TabIndex = 1;
            this.tpShort.Text = "临时医嘱单";
            this.tpShort.UseVisualStyleBackColor = true;
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
            this.pnShortOrder.Size = new System.Drawing.Size(839, 582);
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
            this.fpShortOrder.Size = new System.Drawing.Size(839, 411);
            this.fpShortOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpShortOrder.TabIndex = 4;
            tipAppearance23.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance23.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance23.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpShortOrder.TextTipAppearance = tipAppearance23;
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
            this.pnShortHeader.Size = new System.Drawing.Size(839, 132);
            this.pnShortHeader.TabIndex = 0;
            // 
            // ucShortOrderBillHeader
            // 
            this.ucShortOrderBillHeader.BackColor = System.Drawing.Color.White;
            this.ucShortOrderBillHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucShortOrderBillHeader.Header = null;
            this.ucShortOrderBillHeader.Location = new System.Drawing.Point(0, 0);
            this.ucShortOrderBillHeader.Name = "ucShortOrderBillHeader";
            this.ucShortOrderBillHeader.Size = new System.Drawing.Size(839, 132);
            this.ucShortOrderBillHeader.TabIndex = 0;
            // 
            // pnShortPag
            // 
            this.pnShortPag.BackColor = System.Drawing.Color.White;
            this.pnShortPag.Controls.Add(this.label9);
            this.pnShortPag.Controls.Add(this.label10);
            this.pnShortPag.Controls.Add(this.lblPageShort);
            this.pnShortPag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnShortPag.Location = new System.Drawing.Point(0, 543);
            this.pnShortPag.Name = "pnShortPag";
            this.pnShortPag.Size = new System.Drawing.Size(839, 39);
            this.pnShortPag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnShortPag.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(496, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "责任护士签名：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(172, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "住院医生签名：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageShort
            // 
            this.lblPageShort.AutoSize = true;
            this.lblPageShort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblPageShort.ForeColor = System.Drawing.Color.Red;
            this.lblPageShort.Location = new System.Drawing.Point(21, 10);
            this.lblPageShort.Name = "lblPageShort";
            this.lblPageShort.Size = new System.Drawing.Size(60, 19);
            this.lblPageShort.TabIndex = 3;
            this.lblPageShort.Text = "第1页";
            this.lblPageShort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpUCUL
            // 
            this.tpUCUL.BackColor = System.Drawing.Color.White;
            this.tpUCUL.Location = new System.Drawing.Point(4, 25);
            this.tpUCUL.Name = "tpUCUL";
            this.tpUCUL.Padding = new System.Windows.Forms.Padding(3);
            this.tpUCUL.Size = new System.Drawing.Size(839, 582);
            this.tpUCUL.TabIndex = 2;
            this.tpUCUL.Text = "检查检验医嘱单";
            this.tpUCUL.UseVisualStyleBackColor = true;
            // 
            // tpOperate
            // 
            this.tpOperate.Controls.Add(this.pnOperates);
            this.tpOperate.Location = new System.Drawing.Point(4, 25);
            this.tpOperate.Name = "tpOperate";
            this.tpOperate.Size = new System.Drawing.Size(839, 582);
            this.tpOperate.TabIndex = 3;
            this.tpOperate.Text = "手术单";
            this.tpOperate.UseVisualStyleBackColor = true;
            // 
            // pnOperates
            // 
            this.pnOperates.Controls.Add(this.fpOperateOrder);
            this.pnOperates.Controls.Add(this.ucOperatePanel);
            this.pnOperates.Controls.Add(this.pnOperatePag);
            this.pnOperates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnOperates.Location = new System.Drawing.Point(0, 0);
            this.pnOperates.Name = "pnOperates";
            this.pnOperates.Size = new System.Drawing.Size(839, 582);
            this.pnOperates.TabIndex = 1;
            // 
            // fpOperateOrder
            // 
            this.fpOperateOrder.About = "3.0.2004.2005";
            this.fpOperateOrder.AccessibleDescription = "fpShortOrder, Sheet1, Row 0, Column 0, ";
            this.fpOperateOrder.AllowColumnMove = true;
            this.fpOperateOrder.BackColor = System.Drawing.Color.White;
            this.fpOperateOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpOperateOrder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpOperateOrder.Location = new System.Drawing.Point(0, 132);
            this.fpOperateOrder.Name = "fpOperateOrder";
            this.fpOperateOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpOperateOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpOperateOrder_Sheet1});
            this.fpOperateOrder.Size = new System.Drawing.Size(839, 411);
            this.fpOperateOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpOperateOrder.TabIndex = 5;
            tipAppearance24.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance24.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance24.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpOperateOrder.TextTipAppearance = tipAppearance24;
            // 
            // fpOperateOrder_Sheet1
            // 
            this.fpOperateOrder_Sheet1.Reset();
            this.fpOperateOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpOperateOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpOperateOrder_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpOperateOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucOperatePanel
            // 
            this.ucOperatePanel.Controls.Add(this.ucOperateBillHeader);
            this.ucOperatePanel.Controls.Add(this.pnOperateOrder);
            this.ucOperatePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucOperatePanel.Location = new System.Drawing.Point(0, 0);
            this.ucOperatePanel.Name = "ucOperatePanel";
            this.ucOperatePanel.Size = new System.Drawing.Size(839, 132);
            this.ucOperatePanel.TabIndex = 0;
            // 
            // ucOperateBillHeader
            // 
            this.ucOperateBillHeader.BackColor = System.Drawing.Color.White;
            this.ucOperateBillHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOperateBillHeader.Header = null;
            this.ucOperateBillHeader.Location = new System.Drawing.Point(0, 0);
            this.ucOperateBillHeader.Name = "ucOperateBillHeader";
            this.ucOperateBillHeader.Size = new System.Drawing.Size(839, 132);
            this.ucOperateBillHeader.TabIndex = 1;
            // 
            // pnOperateOrder
            // 
            this.pnOperateOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnOperateOrder.Location = new System.Drawing.Point(0, 0);
            this.pnOperateOrder.Name = "pnOperateOrder";
            this.pnOperateOrder.Size = new System.Drawing.Size(839, 132);
            this.pnOperateOrder.TabIndex = 2;
            // 
            // pnOperatePag
            // 
            this.pnOperatePag.Controls.Add(this.label11);
            this.pnOperatePag.Controls.Add(this.label12);
            this.pnOperatePag.Controls.Add(this.lblPageOperatets);
            this.pnOperatePag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnOperatePag.Location = new System.Drawing.Point(0, 543);
            this.pnOperatePag.Name = "pnOperatePag";
            this.pnOperatePag.Size = new System.Drawing.Size(839, 39);
            this.pnOperatePag.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(494, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "责任护士签名：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(170, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "住院医生签名：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageOperatets
            // 
            this.lblPageOperatets.AutoSize = true;
            this.lblPageOperatets.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageOperatets.ForeColor = System.Drawing.Color.Red;
            this.lblPageOperatets.Location = new System.Drawing.Point(19, 10);
            this.lblPageOperatets.Name = "lblPageOperatets";
            this.lblPageOperatets.Size = new System.Drawing.Size(60, 19);
            this.lblPageOperatets.TabIndex = 3;
            this.lblPageOperatets.Text = "第1页";
            this.lblPageOperatets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ucQueryInpatientNo1.IsDeptOnly = true;
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
            this.groupBox1.Location = new System.Drawing.Point(351, 18);
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
            this.cmbPrintType.IsShowIDAndName = false;
            this.cmbPrintType.Location = new System.Drawing.Point(332, 16);
            this.cmbPrintType.Name = "cmbPrintType";
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
            this.label7.Location = new System.Drawing.Point(279, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "打印方式";
            // 
            // tbPrinter
            // 
            this.tbPrinter.FormattingEnabled = true;
            this.tbPrinter.Location = new System.Drawing.Point(469, 17);
            this.tbPrinter.Name = "tbPrinter";
            this.tbPrinter.Size = new System.Drawing.Size(148, 20);
            this.tbPrinter.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(429, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "打印机";
            // 
            // nuRowNum
            // 
            this.nuRowNum.Enabled = false;
            this.nuRowNum.Location = new System.Drawing.Point(234, 16);
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
            this.label5.Location = new System.Drawing.Point(203, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "行数";
            // 
            // nuTop
            // 
            this.nuTop.Location = new System.Drawing.Point(149, 16);
            this.nuTop.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nuTop.Name = "nuTop";
            this.nuTop.Size = new System.Drawing.Size(50, 21);
            this.nuTop.TabIndex = 3;
            this.nuTop.ValueChanged += new System.EventHandler(this.nuTop_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "上边距";
            // 
            // nuLeft
            // 
            this.nuLeft.Location = new System.Drawing.Point(51, 16);
            this.nuLeft.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nuLeft.Name = "nuLeft";
            this.nuLeft.Size = new System.Drawing.Size(50, 21);
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
            this.label2.Location = new System.Drawing.Point(351, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(606, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "红色代表已经打印，如果医嘱已经打印，将只可以作废，不可以删除!";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(19, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 19);
            this.label13.TabIndex = 3;
            this.label13.Text = "第1页";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageOperate
            // 
            this.lblPageOperate.AutoSize = true;
            this.lblPageOperate.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageOperate.ForeColor = System.Drawing.Color.Red;
            this.lblPageOperate.Location = new System.Drawing.Point(19, 10);
            this.lblPageOperate.Name = "lblPageOperate";
            this.lblPageOperate.Size = new System.Drawing.Size(60, 19);
            this.lblPageOperate.TabIndex = 3;
            this.lblPageOperate.Text = "第1页";
            this.lblPageOperate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmOrderPrint
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(1062, 698);
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
            this.tcControl.ResumeLayout(false);
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
            this.tpOperate.ResumeLayout(false);
            this.pnOperates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpOperateOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOperateOrder_Sheet1)).EndInit();
            this.ucOperatePanel.ResumeLayout(false);
            this.pnOperatePag.ResumeLayout(false);
            this.pnOperatePag.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton tbDeleteLongProfile;
        private System.Windows.Forms.ToolStripButton tbDeleteShortProfile;
        private System.Windows.Forms.ToolStripButton tbRePrint;
        private System.Windows.Forms.ToolStripButton tbExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tcControl;
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
        private System.Windows.Forms.ToolStripMenuItem ResetUCUL;
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
        //private FS.FrameWork.WinForms.Controls.NeuSpread fpLongOrder;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.SOC.Windows.Forms.FpSpread fpShortOrder;
        private FarPoint.Win.Spread.SheetView fpShortOrder_Sheet1;
        private FS.SOC.Windows.Forms.FpSpread fpLongOrder;
        private FarPoint.Win.Spread.SheetView fpLongOrder_Sheet1;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbPrintType;
        private System.Windows.Forms.CheckBox cbxPreview;
        private System.Windows.Forms.ToolStripMenuItem ResetCurrentLong;
        private System.Windows.Forms.ToolStripMenuItem ResetCurrentShort;
        private System.Windows.Forms.ToolStripMenuItem ResetCurrentUCUL;
        private System.Windows.Forms.ToolStripMenuItem RefreshLong;
        private System.Windows.Forms.ToolStripMenuItem RefreshShort;
        private System.Windows.Forms.TabPage tpUCUL;
        private ucOrderBillHeader ucLongOrderBillHeader;
        private ucOrderBillHeader ucShortOrderBillHeader;
        private System.Windows.Forms.ToolStripButton tbAllPrint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tpOperate;
        private System.Windows.Forms.Panel ucOperatePanel;
        private System.Windows.Forms.Panel pnOperatePag;
        private ucOrderBillHeader ucOperateBillHeader;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblPageOperatets;
        private System.Windows.Forms.Panel pnOperateOrder;
        private System.Windows.Forms.Panel pnOperates;
        private FS.SOC.Windows.Forms.FpSpread fpOperateOrder;
        private FarPoint.Win.Spread.SheetView fpOperateOrder_Sheet1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblPageOperate;

    }
}