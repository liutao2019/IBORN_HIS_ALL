namespace FS.SOC.HISFC.Components.DCP.Report
{
    partial class ucReportQuery
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
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucReportQuery));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbReportInfo = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radCanel = new System.Windows.Forms.RadioButton();
            this.radUneligible = new System.Windows.Forms.RadioButton();
            this.radEligible = new System.Windows.Forms.RadioButton();
            this.radUnapprove = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nlbRefreshUnit = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nTxtRefreshSpan = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ncbAutoQuery = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncmbDisease = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.ncmbReportDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbPatientNO = new System.Windows.Forms.Label();
            this.txtCarNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInPatienNo = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(216, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 656);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.fpSpread1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 48);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(496, 608);
            this.panel4.TabIndex = 1;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpSpread1.ContextMenu = this.contextMenu1;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(496, 608);
            this.fpSpread1.TabIndex = 11;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellClick);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "全选";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "全不选";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 20;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 45F;
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 58F;
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 59F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2012, 11, 1, 16, 32, 25, 214);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.LongDateWithTime;
            dateTimeCellType1.TimeDefault = new System.DateTime(2012, 11, 1, 16, 32, 25, 214);
            this.fpSpread1_Sheet1.Columns.Get(12).CellType = dateTimeCellType1;
            this.fpSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.fpSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(496, 48);
            this.panel3.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbReportInfo);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(496, 48);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            // 
            // lbReportInfo
            // 
            this.lbReportInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReportInfo.ForeColor = System.Drawing.Color.Blue;
            this.lbReportInfo.Location = new System.Drawing.Point(3, 17);
            this.lbReportInfo.Name = "lbReportInfo";
            this.lbReportInfo.Size = new System.Drawing.Size(490, 28);
            this.lbReportInfo.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(216, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 656);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(80, 264);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(120, 21);
            this.dtEnd.TabIndex = 10;
            this.dtEnd.Value = new System.DateTime(2007, 4, 24, 15, 2, 21, 640);
            // 
            // dtBegin
            // 
            this.dtBegin.Location = new System.Drawing.Point(80, 224);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(120, 21);
            this.dtBegin.TabIndex = 9;
            this.dtBegin.Value = new System.DateTime(2007, 4, 24, 15, 2, 21, 671);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radAll);
            this.groupBox1.Controls.Add(this.radCanel);
            this.groupBox1.Controls.Add(this.radUneligible);
            this.groupBox1.Controls.Add(this.radEligible);
            this.groupBox1.Controls.Add(this.radUnapprove);
            this.groupBox1.Location = new System.Drawing.Point(8, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 119);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "报告状态";
            // 
            // radAll
            // 
            this.radAll.Location = new System.Drawing.Point(112, 88);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(64, 24);
            this.radAll.TabIndex = 4;
            this.radAll.Text = "全部";
            // 
            // radCanel
            // 
            this.radCanel.Location = new System.Drawing.Point(11, 88);
            this.radCanel.Name = "radCanel";
            this.radCanel.Size = new System.Drawing.Size(64, 24);
            this.radCanel.TabIndex = 3;
            this.radCanel.Text = "作废";
            // 
            // radUneligible
            // 
            this.radUneligible.Location = new System.Drawing.Point(112, 56);
            this.radUneligible.Name = "radUneligible";
            this.radUneligible.Size = new System.Drawing.Size(64, 24);
            this.radUneligible.TabIndex = 2;
            this.radUneligible.Text = "不合格";
            this.radUneligible.Visible = false;
            // 
            // radEligible
            // 
            this.radEligible.Location = new System.Drawing.Point(11, 56);
            this.radEligible.Name = "radEligible";
            this.radEligible.Size = new System.Drawing.Size(72, 24);
            this.radEligible.TabIndex = 1;
            this.radEligible.Text = "合格";
            // 
            // radUnapprove
            // 
            this.radUnapprove.Checked = true;
            this.radUnapprove.Location = new System.Drawing.Point(11, 24);
            this.radUnapprove.Name = "radUnapprove";
            this.radUnapprove.Size = new System.Drawing.Size(120, 24);
            this.radUnapprove.TabIndex = 0;
            this.radUnapprove.TabStop = true;
            this.radUnapprove.Text = "未审核及不合格";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "结束时间：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtEnd);
            this.panel1.Controls.Add(this.dtBegin);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbDateType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 656);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "开始时间：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDateType
            // 
            this.cmbDateType.Items.AddRange(new object[] {
            "报告日期",
            "发病日期",
            "诊断日期",
            "死亡日期",
            "审核日期"});
            this.cmbDateType.Location = new System.Drawing.Point(80, 184);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Size = new System.Drawing.Size(121, 20);
            this.cmbDateType.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "报告科室：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "时间类型：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nlbRefreshUnit);
            this.groupBox2.Controls.Add(this.nTxtRefreshSpan);
            this.groupBox2.Controls.Add(this.ncbAutoQuery);
            this.groupBox2.Controls.Add(this.ncmbDisease);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ncmbReportDept);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.lbPatientNO);
            this.groupBox2.Controls.Add(this.txtCarNo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtInPatienNo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 656);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // neuLabel1
            // 
            this.nlbRefreshUnit.AutoSize = true;
            this.nlbRefreshUnit.Location = new System.Drawing.Point(189, 609);
            this.nlbRefreshUnit.Name = "neuLabel1";
            this.nlbRefreshUnit.Size = new System.Drawing.Size(17, 12);
            this.nlbRefreshUnit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRefreshUnit.TabIndex = 24;
            this.nlbRefreshUnit.Text = "秒";
            // 
            // nTxtRefreshSpan
            // 
            this.nTxtRefreshSpan.IsEnter2Tab = false;
            this.nTxtRefreshSpan.Location = new System.Drawing.Point(164, 606);
            this.nTxtRefreshSpan.Name = "nTxtRefreshSpan";
            this.nTxtRefreshSpan.ReadOnly = true;
            this.nTxtRefreshSpan.Size = new System.Drawing.Size(20, 21);
            this.nTxtRefreshSpan.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nTxtRefreshSpan.TabIndex = 23;
            this.nTxtRefreshSpan.Text = "10";
            // 
            // ncbAutoQuery
            // 
            this.ncbAutoQuery.AutoSize = true;
            this.ncbAutoQuery.Checked = true;
            this.ncbAutoQuery.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbAutoQuery.Location = new System.Drawing.Point(18, 608);
            this.ncbAutoQuery.Name = "ncbAutoQuery";
            this.ncbAutoQuery.Size = new System.Drawing.Size(144, 16);
            this.ncbAutoQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbAutoQuery.TabIndex = 22;
            this.ncbAutoQuery.Text = "自动查询新上报的卡每";
            this.ncbAutoQuery.UseVisualStyleBackColor = true;
            // 
            // ncmbDisease
            // 
            this.ncmbDisease.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDisease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbDisease.FormattingEnabled = true;
            this.ncmbDisease.IsEnter2Tab = false;
            this.ncmbDisease.IsFlat = false;
            this.ncmbDisease.IsLike = true;
            this.ncmbDisease.IsListOnly = false;
            this.ncmbDisease.IsPopForm = true;
            this.ncmbDisease.IsShowCustomerList = false;
            this.ncmbDisease.IsShowID = false;
            this.ncmbDisease.Location = new System.Drawing.Point(80, 436);
            this.ncmbDisease.Name = "ncmbDisease";
            this.ncmbDisease.ShowCustomerList = false;
            this.ncmbDisease.ShowID = false;
            this.ncmbDisease.Size = new System.Drawing.Size(121, 20);
            this.ncmbDisease.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDisease.TabIndex = 21;
            this.ncmbDisease.Tag = "";
            this.ncmbDisease.ToolBarUse = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 434);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 20;
            this.label5.Text = "疾病名称：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ncmbReportDept
            // 
            this.ncmbReportDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbReportDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbReportDept.FormattingEnabled = true;
            this.ncmbReportDept.IsEnter2Tab = false;
            this.ncmbReportDept.IsFlat = false;
            this.ncmbReportDept.IsLike = true;
            this.ncmbReportDept.IsListOnly = false;
            this.ncmbReportDept.IsPopForm = true;
            this.ncmbReportDept.IsShowCustomerList = false;
            this.ncmbReportDept.IsShowID = false;
            this.ncmbReportDept.Location = new System.Drawing.Point(80, 146);
            this.ncmbReportDept.Name = "ncmbReportDept";
            this.ncmbReportDept.ShowCustomerList = false;
            this.ncmbReportDept.ShowID = false;
            this.ncmbReportDept.Size = new System.Drawing.Size(121, 20);
            this.ncmbReportDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbReportDept.TabIndex = 18;
            this.ncmbReportDept.Tag = "";
            this.ncmbReportDept.ToolBarUse = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(10, 472);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(191, 120);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "说明";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.SkyBlue;
            this.label17.Location = new System.Drawing.Point(79, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(16, 24);
            this.label17.TabIndex = 11;
            this.label17.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Blue;
            this.label16.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label16.Location = new System.Drawing.Point(63, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 24);
            this.label16.TabIndex = 10;
            this.label16.Text = "义";
            this.label16.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Lime;
            this.label15.Location = new System.Drawing.Point(47, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 24);
            this.label15.TabIndex = 9;
            this.label15.Text = "含";
            this.label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(31, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 24);
            this.label14.TabIndex = 8;
            this.label14.Text = "色";
            this.label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(7, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 24);
            this.label12.TabIndex = 7;
            this.label12.Text = "颜";
            this.label12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Blue;
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label13.Location = new System.Drawing.Point(7, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 23);
            this.label13.TabIndex = 6;
            this.label13.Text = "审核作废";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(95, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 23);
            this.label10.TabIndex = 5;
            this.label10.Text = "合格报告";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(95, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 23);
            this.label11.TabIndex = 4;
            this.label11.Text = "未审核报告";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.SkyBlue;
            this.label9.Location = new System.Drawing.Point(95, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 23);
            this.label9.TabIndex = 3;
            this.label9.Text = "报告人作废";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(7, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 23);
            this.label8.TabIndex = 2;
            this.label8.Text = "不合格报告";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(8, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 8);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 23);
            this.label6.TabIndex = 14;
            this.label6.Text = "姓    名：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(80, 88);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(120, 21);
            this.txtName.TabIndex = 15;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // lbPatientNO
            // 
            this.lbPatientNO.Location = new System.Drawing.Point(8, 24);
            this.lbPatientNO.Name = "lbPatientNO";
            this.lbPatientNO.Size = new System.Drawing.Size(72, 23);
            this.lbPatientNO.TabIndex = 10;
            this.lbPatientNO.Text = "报告卡号：";
            this.lbPatientNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCarNo
            // 
            this.txtCarNo.Location = new System.Drawing.Point(80, 24);
            this.txtCarNo.Name = "txtCarNo";
            this.txtCarNo.Size = new System.Drawing.Size(120, 21);
            this.txtCarNo.TabIndex = 11;
            this.txtCarNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarNo_KeyPress);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 32);
            this.label7.TabIndex = 12;
            this.label7.Text = "病 历 号  住 院 号";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInPatienNo
            // 
            this.txtInPatienNo.Location = new System.Drawing.Point(80, 56);
            this.txtInPatienNo.Name = "txtInPatienNo";
            this.txtInPatienNo.Size = new System.Drawing.Size(120, 21);
            this.txtInPatienNo.TabIndex = 13;
            this.txtInPatienNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInPatienNo_KeyPress);
            // 
            // ucReportQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucReportQuery";
            this.Size = new System.Drawing.Size(712, 656);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radCanel;
        private System.Windows.Forms.RadioButton radUneligible;
        private System.Windows.Forms.RadioButton radEligible;
        private System.Windows.Forms.RadioButton radUnapprove;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDateType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.TextBox txtInPatienNo;
        private System.Windows.Forms.TextBox txtCarNo;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbPatientNO;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        public FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private SOC.Windows.Forms.FpSpread fpSpread1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lbReportInfo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbReportDept;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDisease;
        private System.Windows.Forms.Label label5;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbAutoQuery;
        private FS.FrameWork.WinForms.Controls.NeuTextBox nTxtRefreshSpan;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbRefreshUnit;
    }
}
