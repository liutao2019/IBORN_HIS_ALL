namespace FS.HISFC.Components.Order.Controls
{
    partial class ucOrderShow
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
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOderStatus = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.pnPatient = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbPatient = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.neuLinkLabel1 = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.btnReOrderQueryed = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.chkFee = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtItemInfo = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.fpSpread2 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread2_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btOrderPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnPatient.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(3, 10);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "医嘱状态：";
            // 
            // cmbOderStatus
            // 
            this.cmbOderStatus.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOderStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbOderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOderStatus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbOderStatus.FormattingEnabled = true;
            this.cmbOderStatus.IsEnter2Tab = false;
            this.cmbOderStatus.IsFlat = false;
            this.cmbOderStatus.IsLike = true;
            this.cmbOderStatus.IsListOnly = false;
            this.cmbOderStatus.IsPopForm = true;
            this.cmbOderStatus.IsShowCustomerList = false;
            this.cmbOderStatus.IsShowID = false;
            this.cmbOderStatus.IsShowIDAndName = false;
            this.cmbOderStatus.Items.AddRange(new object[] {
            "全部医嘱",
            "有效医嘱",
            "当天医嘱",
            "待审核医嘱",
            "当天作废医嘱",
            "全部作废医嘱",
            "皮试医嘱"});
            this.cmbOderStatus.Location = new System.Drawing.Point(72, 6);
            this.cmbOderStatus.Name = "cmbOderStatus";
            this.cmbOderStatus.ShowCustomerList = false;
            this.cmbOderStatus.ShowID = false;
            this.cmbOderStatus.Size = new System.Drawing.Size(108, 22);
            this.cmbOderStatus.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbOderStatus.TabIndex = 1;
            this.cmbOderStatus.Tag = "";
            this.cmbOderStatus.ToolBarUse = false;
            this.cmbOderStatus.SelectedIndexChanged += new System.EventHandler(this.cmbOderStatus_SelectedIndexChanged_1);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(432, 10);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(77, 14);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "查找医嘱：";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, 临时医嘱(F12切换), Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.ContextMenuStrip = this.contextMenuStrip1;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1,
            this.fpSpread1_Sheet2});
            this.fpSpread1.Size = new System.Drawing.Size(992, 127);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 4;
            this.fpSpread1.TabStripRatio = 0.814771395076202;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance4;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fpSpread1_MouseDown);
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_Sheet_Clicked);
            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.fpSpread1_ColumnWidthChanged);
            this.fpSpread1.ActiveSheetIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "长期医嘱(F12切换)";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.RowCount = 2;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 53F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 27F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 25F;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread1_Sheet2
            // 
            this.fpSpread1_Sheet2.Reset();
            this.fpSpread1_Sheet2.SheetName = "临时医嘱(F12切换)";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet2.RowCount = 2;
            this.fpSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet2.ColumnHeader.AutoTextIndex = 0;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet2.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet2.RowHeader.AutoTextIndex = 0;
            this.fpSpread1_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet2.RowHeader.Columns.Get(0).Width = 25F;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.Rows.Default.Height = 25F;
            this.fpSpread1_Sheet2.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet2.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet2.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet2.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.pnPatient);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(992, 87);
            this.groupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // pnPatient
            // 
            this.pnPatient.BackColor = System.Drawing.Color.AliceBlue;
            this.pnPatient.Controls.Add(this.lbPatient);
            this.pnPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnPatient.Location = new System.Drawing.Point(3, 49);
            this.pnPatient.Name = "pnPatient";
            this.pnPatient.Size = new System.Drawing.Size(986, 42);
            this.pnPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnPatient.TabIndex = 11;
            // 
            // lbPatient
            // 
            this.lbPatient.BackColor = System.Drawing.Color.AliceBlue;
            this.lbPatient.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPatient.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPatient.Location = new System.Drawing.Point(0, 0);
            this.lbPatient.Multiline = true;
            this.lbPatient.Name = "lbPatient";
            this.lbPatient.ReadOnly = true;
            this.lbPatient.Size = new System.Drawing.Size(986, 42);
            this.lbPatient.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btOrderPrint);
            this.panel4.Controls.Add(this.txtQuery);
            this.panel4.Controls.Add(this.cmbOderStatus);
            this.panel4.Controls.Add(this.neuLabel2);
            this.panel4.Controls.Add(this.neuLinkLabel1);
            this.panel4.Controls.Add(this.btnReOrderQueryed);
            this.panel4.Controls.Add(this.neuLabel1);
            this.panel4.Controls.Add(this.chkFee);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 17);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(986, 32);
            this.panel4.TabIndex = 13;
            // 
            // txtQuery
            // 
            this.txtQuery.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQuery.Location = new System.Drawing.Point(502, 5);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(181, 23);
            this.txtQuery.TabIndex = 10;
            // 
            // neuLinkLabel1
            // 
            this.neuLinkLabel1.AutoSize = true;
            this.neuLinkLabel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.neuLinkLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLinkLabel1.Location = new System.Drawing.Point(685, 8);
            this.neuLinkLabel1.Name = "neuLinkLabel1";
            this.neuLinkLabel1.Size = new System.Drawing.Size(91, 14);
            this.neuLinkLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLinkLabel1.TabIndex = 8;
            this.neuLinkLabel1.TabStop = true;
            this.neuLinkLabel1.Text = "保存显示格式";
            this.neuLinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.neuLinkLabel1_LinkClicked);
            // 
            // btnReOrderQueryed
            // 
            this.btnReOrderQueryed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReOrderQueryed.Location = new System.Drawing.Point(184, 5);
            this.btnReOrderQueryed.Name = "btnReOrderQueryed";
            this.btnReOrderQueryed.Size = new System.Drawing.Size(100, 23);
            this.btnReOrderQueryed.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnReOrderQueryed.TabIndex = 6;
            this.btnReOrderQueryed.Text = "查询已重整医嘱";
            this.btnReOrderQueryed.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnReOrderQueryed.UseVisualStyleBackColor = true;
            this.btnReOrderQueryed.Click += new System.EventHandler(this.btnReOrderQueryed_Click);
            // 
            // chkFee
            // 
            this.chkFee.AutoSize = true;
            this.chkFee.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chkFee.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkFee.ForeColor = System.Drawing.Color.Red;
            this.chkFee.Location = new System.Drawing.Point(786, 6);
            this.chkFee.Name = "chkFee";
            this.chkFee.Size = new System.Drawing.Size(138, 18);
            this.chkFee.TabIndex = 9;
            this.chkFee.Text = "显示医嘱对应费用";
            this.chkFee.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkFee.UseVisualStyleBackColor = false;
            this.chkFee.Visible = false;
            this.chkFee.CheckedChanged += new System.EventHandler(this.chkFee_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 351);
            this.panel1.TabIndex = 7;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 211);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(992, 3);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpSpread1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 127);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 214);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 137);
            this.panel3.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(992, 137);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtItemInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(984, 111);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "项目信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtItemInfo
            // 
            this.txtItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItemInfo.ForeColor = System.Drawing.Color.Blue;
            this.txtItemInfo.Location = new System.Drawing.Point(3, 3);
            this.txtItemInfo.Name = "txtItemInfo";
            this.txtItemInfo.Size = new System.Drawing.Size(978, 105);
            this.txtItemInfo.TabIndex = 2;
            this.txtItemInfo.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.fpSpread2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(891, 111);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "费用信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // fpSpread2
            // 
            this.fpSpread2.About = "3.0.2004.2005";
            this.fpSpread2.AccessibleDescription = "fpSpread2, 汇总, Row 0, Column 0, ";
            this.fpSpread2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fpSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread2.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.Location = new System.Drawing.Point(3, 3);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread2_Sheet2,
            this.fpSpread2_Sheet1});
            this.fpSpread2.Size = new System.Drawing.Size(885, 105);
            this.fpSpread2.TabIndex = 0;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread2.TextTipAppearance = tipAppearance3;
            this.fpSpread2.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread2_CellDoubleClick);
            this.fpSpread2.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpSpread2_SelectionChanged);
            this.fpSpread2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpSpread2_MouseUp);
            // 
            // fpSpread2_Sheet2
            // 
            this.fpSpread2_Sheet2.Reset();
            this.fpSpread2_Sheet2.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread2_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread2_Sheet2.ColumnCount = 4;
            this.fpSpread2_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread2_Sheet2.ColumnHeader.Cells.Get(0, 0).Value = "收费时间";
            this.fpSpread2_Sheet2.ColumnHeader.Cells.Get(0, 1).Value = "项目名称";
            this.fpSpread2_Sheet2.ColumnHeader.Cells.Get(0, 2).Value = "数量";
            this.fpSpread2_Sheet2.ColumnHeader.Cells.Get(0, 3).Value = "金额";
            this.fpSpread2_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread2_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread2_Sheet2.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread2_Sheet2.Columns.Get(0).Label = "收费时间";
            this.fpSpread2_Sheet2.Columns.Get(0).Width = 99F;
            this.fpSpread2_Sheet2.Columns.Get(1).Label = "项目名称";
            this.fpSpread2_Sheet2.Columns.Get(1).Width = 177F;
            this.fpSpread2_Sheet2.Columns.Get(3).Label = "金额";
            this.fpSpread2_Sheet2.Columns.Get(3).Width = 90F;
            this.fpSpread2_Sheet2.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread2_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread2_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread2_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread2_Sheet2.Rows.Default.Height = 25F;
            this.fpSpread2_Sheet2.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread2_Sheet2.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread2_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread2_Sheet2.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread2_Sheet2.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread2_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread2_Sheet1
            // 
            this.fpSpread2_Sheet1.Reset();
            this.fpSpread2_Sheet1.SheetName = "明细";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread2_Sheet1.ColumnCount = 8;
            this.fpSpread2_Sheet1.ActiveColumnIndex = 3;
            this.fpSpread2_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目名称";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "医嘱时间";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "收费时间";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "收费数量";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "收费金额";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "收费人";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "流水号";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "选中";
            this.fpSpread2_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread2_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread2_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread2_Sheet1.Columns.Get(0).Label = "项目名称";
            this.fpSpread2_Sheet1.Columns.Get(0).Locked = true;
            this.fpSpread2_Sheet1.Columns.Get(0).Width = 204F;
            this.fpSpread2_Sheet1.Columns.Get(1).Label = "医嘱时间";
            this.fpSpread2_Sheet1.Columns.Get(1).Locked = true;
            this.fpSpread2_Sheet1.Columns.Get(1).Width = 143F;
            this.fpSpread2_Sheet1.Columns.Get(2).Label = "收费时间";
            this.fpSpread2_Sheet1.Columns.Get(2).Locked = true;
            this.fpSpread2_Sheet1.Columns.Get(2).Width = 131F;
            this.fpSpread2_Sheet1.Columns.Get(3).Label = "收费数量";
            this.fpSpread2_Sheet1.Columns.Get(3).Locked = true;
            this.fpSpread2_Sheet1.Columns.Get(3).Width = 68F;
            this.fpSpread2_Sheet1.Columns.Get(4).Label = "收费金额";
            this.fpSpread2_Sheet1.Columns.Get(4).Locked = true;
            this.fpSpread2_Sheet1.Columns.Get(4).Width = 90F;
            this.fpSpread2_Sheet1.Columns.Get(5).Label = "收费人";
            this.fpSpread2_Sheet1.Columns.Get(5).Locked = true;
            this.fpSpread2_Sheet1.Columns.Get(5).Width = 114F;
            this.fpSpread2_Sheet1.Columns.Get(6).Label = "流水号";
            this.fpSpread2_Sheet1.Columns.Get(6).Locked = true;
            this.fpSpread2_Sheet1.Columns.Get(7).CellType = checkBoxCellType3;
            this.fpSpread2_Sheet1.Columns.Get(7).Label = "选中";
            this.fpSpread2_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread2_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread2_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread2_Sheet1.Rows.Default.Height = 25F;
            this.fpSpread2_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread2_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread2_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btOrderPrint
            // 
            this.btOrderPrint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOrderPrint.Location = new System.Drawing.Point(315, 5);
            this.btOrderPrint.Name = "btOrderPrint";
            this.btOrderPrint.Size = new System.Drawing.Size(100, 23);
            this.btOrderPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btOrderPrint.TabIndex = 11;
            this.btOrderPrint.Text = "打印医嘱单";
            this.btOrderPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btOrderPrint.UseVisualStyleBackColor = true;
            this.btOrderPrint.Click += new System.EventHandler(this.btOrderPrint_Click);
            // 
            // ucOrderShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.panel1);
            this.Name = "ucOrderShow";
            this.Size = new System.Drawing.Size(992, 351);
            this.Load += new System.EventHandler(this.ucOrderShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.pnPatient.ResumeLayout(false);
            this.pnPatient.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOderStatus;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnReOrderQueryed;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private FarPoint.Win.Spread.FpSpread fpSpread2;
        private FarPoint.Win.Spread.SheetView fpSpread2_Sheet1;
        private FarPoint.Win.Spread.SheetView fpSpread2_Sheet2;
        private System.Windows.Forms.CheckBox chkFee;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel neuLinkLabel1;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Panel panel4;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnPatient;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox txtItemInfo;
        private System.Windows.Forms.TextBox lbPatient;
        private FS.FrameWork.WinForms.Controls.NeuButton btOrderPrint;
    }
}
