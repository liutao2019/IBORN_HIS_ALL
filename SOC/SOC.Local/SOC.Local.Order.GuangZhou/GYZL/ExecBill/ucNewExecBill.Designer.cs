namespace FS.SOC.Local.Order.GuangZhou.GYZL.ExecBill
{
    partial class ucNewExecBill
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cbbPrint = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnPatient = new System.Windows.Forms.Button();
            this.gbUsage = new System.Windows.Forms.GroupBox();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.cb1 = new System.Windows.Forms.CheckBox();
            this.cb2 = new System.Windows.Forms.CheckBox();
            this.cb3 = new System.Windows.Forms.CheckBox();
            this.cb4 = new System.Windows.Forms.CheckBox();
            this.cb5 = new System.Windows.Forms.CheckBox();
            this.cb6 = new System.Windows.Forms.CheckBox();
            this.cb7 = new System.Windows.Forms.CheckBox();
            this.cb8 = new System.Windows.Forms.CheckBox();
            this.cb9 = new System.Windows.Forms.CheckBox();
            this.cb10 = new System.Windows.Forms.CheckBox();
            this.cb11 = new System.Windows.Forms.CheckBox();
            this.cb12 = new System.Windows.Forms.CheckBox();
            this.cb13 = new System.Windows.Forms.CheckBox();
            this.cb14 = new System.Windows.Forms.CheckBox();
            this.cb15 = new System.Windows.Forms.CheckBox();
            this.cb16 = new System.Windows.Forms.CheckBox();
            this.dtpTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.gbType = new System.Windows.Forms.GroupBox();
            this.rbtShort = new System.Windows.Forms.RadioButton();
            this.rbtAll = new System.Windows.Forms.RadioButton();
            this.rbtLong = new System.Windows.Forms.RadioButton();
            this.lblTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbIsPrinted = new System.Windows.Forms.CheckBox();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.gbUsage.SuspendLayout();
            this.gbType.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.cbbPrint);
            this.neuPanel1.Controls.Add(this.btnClear);
            this.neuPanel1.Controls.Add(this.btnAll);
            this.neuPanel1.Controls.Add(this.btnPatient);
            this.neuPanel1.Controls.Add(this.gbUsage);
            this.neuPanel1.Controls.Add(this.dtpTime);
            this.neuPanel1.Controls.Add(this.gbType);
            this.neuPanel1.Controls.Add(this.lblTime);
            this.neuPanel1.Controls.Add(this.cbIsPrinted);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(850, 135);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // cbbPrint
            // 
            this.cbbPrint.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cbbPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbbPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrint.FormattingEnabled = true;
            this.cbbPrint.IsEnter2Tab = false;
            this.cbbPrint.IsFlat = false;
            this.cbbPrint.IsLike = true;
            this.cbbPrint.IsListOnly = true;
            this.cbbPrint.IsPopForm = false;
            this.cbbPrint.IsShowCustomerList = false;
            this.cbbPrint.IsShowID = false;
            this.cbbPrint.IsShowIDAndName = false;
            this.cbbPrint.Location = new System.Drawing.Point(692, 15);
            this.cbbPrint.Name = "cbbPrint";
            this.cbbPrint.ShowCustomerList = false;
            this.cbbPrint.ShowID = false;
            this.cbbPrint.Size = new System.Drawing.Size(138, 20);
            this.cbbPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbbPrint.TabIndex = 16;
            this.cbbPrint.Tag = "";
            this.cbbPrint.ToolBarUse = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(518, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(433, 13);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 15;
            this.btnAll.Text = "全选";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnPatient
            // 
            this.btnPatient.Location = new System.Drawing.Point(603, 13);
            this.btnPatient.Name = "btnPatient";
            this.btnPatient.Size = new System.Drawing.Size(75, 23);
            this.btnPatient.TabIndex = 15;
            this.btnPatient.Text = "选取同患者";
            this.btnPatient.UseVisualStyleBackColor = true;
            this.btnPatient.Click += new System.EventHandler(this.btnPatient_Click);
            // 
            // gbUsage
            // 
            this.gbUsage.Controls.Add(this.cbAll);
            this.gbUsage.Controls.Add(this.cb1);
            this.gbUsage.Controls.Add(this.cb2);
            this.gbUsage.Controls.Add(this.cb3);
            this.gbUsage.Controls.Add(this.cb4);
            this.gbUsage.Controls.Add(this.cb5);
            this.gbUsage.Controls.Add(this.cb6);
            this.gbUsage.Controls.Add(this.cb7);
            this.gbUsage.Controls.Add(this.cb8);
            this.gbUsage.Controls.Add(this.cb9);
            this.gbUsage.Controls.Add(this.cb10);
            this.gbUsage.Controls.Add(this.cb11);
            this.gbUsage.Controls.Add(this.cb12);
            this.gbUsage.Controls.Add(this.cb13);
            this.gbUsage.Controls.Add(this.cb14);
            this.gbUsage.Controls.Add(this.cb15);
            this.gbUsage.Controls.Add(this.cb16);
            this.gbUsage.Location = new System.Drawing.Point(17, 42);
            this.gbUsage.Name = "gbUsage";
            this.gbUsage.Size = new System.Drawing.Size(816, 86);
            this.gbUsage.TabIndex = 14;
            this.gbUsage.TabStop = false;
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Checked = true;
            this.cbAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAll.Location = new System.Drawing.Point(19, 16);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(48, 16);
            this.cbAll.TabIndex = 0;
            this.cbAll.Tag = "ALL";
            this.cbAll.Text = "全选";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // cb1
            // 
            this.cb1.AutoSize = true;
            this.cb1.Checked = true;
            this.cb1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb1.Location = new System.Drawing.Point(19, 38);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(78, 16);
            this.cb1.TabIndex = 0;
            this.cb1.Text = "checkBox1";
            this.cb1.UseVisualStyleBackColor = true;
            this.cb1.Visible = false;
            // 
            // cb2
            // 
            this.cb2.AutoSize = true;
            this.cb2.Checked = true;
            this.cb2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb2.Location = new System.Drawing.Point(119, 38);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(78, 16);
            this.cb2.TabIndex = 0;
            this.cb2.Text = "checkBox2";
            this.cb2.UseVisualStyleBackColor = true;
            this.cb2.Visible = false;
            // 
            // cb3
            // 
            this.cb3.AutoSize = true;
            this.cb3.Checked = true;
            this.cb3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb3.Location = new System.Drawing.Point(219, 38);
            this.cb3.Name = "cb3";
            this.cb3.Size = new System.Drawing.Size(78, 16);
            this.cb3.TabIndex = 0;
            this.cb3.Text = "checkBox3";
            this.cb3.UseVisualStyleBackColor = true;
            this.cb3.Visible = false;
            // 
            // cb4
            // 
            this.cb4.AutoSize = true;
            this.cb4.Checked = true;
            this.cb4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb4.Location = new System.Drawing.Point(319, 38);
            this.cb4.Name = "cb4";
            this.cb4.Size = new System.Drawing.Size(78, 16);
            this.cb4.TabIndex = 0;
            this.cb4.Text = "checkBox4";
            this.cb4.UseVisualStyleBackColor = true;
            this.cb4.Visible = false;
            // 
            // cb5
            // 
            this.cb5.AutoSize = true;
            this.cb5.Checked = true;
            this.cb5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb5.Location = new System.Drawing.Point(419, 38);
            this.cb5.Name = "cb5";
            this.cb5.Size = new System.Drawing.Size(78, 16);
            this.cb5.TabIndex = 0;
            this.cb5.Text = "checkBox5";
            this.cb5.UseVisualStyleBackColor = true;
            this.cb5.Visible = false;
            // 
            // cb6
            // 
            this.cb6.AutoSize = true;
            this.cb6.Checked = true;
            this.cb6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb6.Location = new System.Drawing.Point(519, 38);
            this.cb6.Name = "cb6";
            this.cb6.Size = new System.Drawing.Size(78, 16);
            this.cb6.TabIndex = 0;
            this.cb6.Text = "checkBox6";
            this.cb6.UseVisualStyleBackColor = true;
            this.cb6.Visible = false;
            // 
            // cb7
            // 
            this.cb7.AutoSize = true;
            this.cb7.Checked = true;
            this.cb7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb7.Location = new System.Drawing.Point(619, 37);
            this.cb7.Name = "cb7";
            this.cb7.Size = new System.Drawing.Size(78, 16);
            this.cb7.TabIndex = 0;
            this.cb7.Text = "checkBox7";
            this.cb7.UseVisualStyleBackColor = true;
            this.cb7.Visible = false;
            // 
            // cb8
            // 
            this.cb8.AutoSize = true;
            this.cb8.Checked = true;
            this.cb8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb8.Location = new System.Drawing.Point(719, 37);
            this.cb8.Name = "cb8";
            this.cb8.Size = new System.Drawing.Size(78, 16);
            this.cb8.TabIndex = 0;
            this.cb8.Text = "checkBox8";
            this.cb8.UseVisualStyleBackColor = true;
            this.cb8.Visible = false;
            // 
            // cb9
            // 
            this.cb9.AutoSize = true;
            this.cb9.Checked = true;
            this.cb9.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb9.Location = new System.Drawing.Point(19, 62);
            this.cb9.Name = "cb9";
            this.cb9.Size = new System.Drawing.Size(78, 16);
            this.cb9.TabIndex = 3;
            this.cb9.Text = "checkBox9";
            this.cb9.UseVisualStyleBackColor = true;
            this.cb9.Visible = false;
            // 
            // cb10
            // 
            this.cb10.AutoSize = true;
            this.cb10.Checked = true;
            this.cb10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb10.Location = new System.Drawing.Point(119, 62);
            this.cb10.Name = "cb10";
            this.cb10.Size = new System.Drawing.Size(84, 16);
            this.cb10.TabIndex = 4;
            this.cb10.Text = "checkBox10";
            this.cb10.UseVisualStyleBackColor = true;
            this.cb10.Visible = false;
            // 
            // cb11
            // 
            this.cb11.AutoSize = true;
            this.cb11.Checked = true;
            this.cb11.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb11.Location = new System.Drawing.Point(219, 62);
            this.cb11.Name = "cb11";
            this.cb11.Size = new System.Drawing.Size(84, 16);
            this.cb11.TabIndex = 1;
            this.cb11.Text = "checkBox11";
            this.cb11.UseVisualStyleBackColor = true;
            this.cb11.Visible = false;
            // 
            // cb12
            // 
            this.cb12.AutoSize = true;
            this.cb12.Checked = true;
            this.cb12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb12.Location = new System.Drawing.Point(319, 62);
            this.cb12.Name = "cb12";
            this.cb12.Size = new System.Drawing.Size(84, 16);
            this.cb12.TabIndex = 2;
            this.cb12.Text = "checkBox12";
            this.cb12.UseVisualStyleBackColor = true;
            this.cb12.Visible = false;
            // 
            // cb13
            // 
            this.cb13.AutoSize = true;
            this.cb13.Checked = true;
            this.cb13.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb13.Location = new System.Drawing.Point(419, 62);
            this.cb13.Name = "cb13";
            this.cb13.Size = new System.Drawing.Size(84, 16);
            this.cb13.TabIndex = 7;
            this.cb13.Text = "checkBox13";
            this.cb13.UseVisualStyleBackColor = true;
            this.cb13.Visible = false;
            // 
            // cb14
            // 
            this.cb14.AutoSize = true;
            this.cb14.Checked = true;
            this.cb14.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb14.Location = new System.Drawing.Point(519, 62);
            this.cb14.Name = "cb14";
            this.cb14.Size = new System.Drawing.Size(84, 16);
            this.cb14.TabIndex = 8;
            this.cb14.Text = "checkBox14";
            this.cb14.UseVisualStyleBackColor = true;
            this.cb14.Visible = false;
            // 
            // cb15
            // 
            this.cb15.AutoSize = true;
            this.cb15.Checked = true;
            this.cb15.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb15.Location = new System.Drawing.Point(619, 61);
            this.cb15.Name = "cb15";
            this.cb15.Size = new System.Drawing.Size(84, 16);
            this.cb15.TabIndex = 5;
            this.cb15.Text = "checkBox15";
            this.cb15.UseVisualStyleBackColor = true;
            this.cb15.Visible = false;
            // 
            // cb16
            // 
            this.cb16.AutoSize = true;
            this.cb16.Checked = true;
            this.cb16.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb16.Location = new System.Drawing.Point(719, 61);
            this.cb16.Name = "cb16";
            this.cb16.Size = new System.Drawing.Size(84, 16);
            this.cb16.TabIndex = 6;
            this.cb16.Text = "checkBox16";
            this.cb16.UseVisualStyleBackColor = true;
            this.cb16.Visible = false;
            // 
            // dtpTime
            // 
            this.dtpTime.CustomFormat = "yyyy-MM-dd";
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.IsEnter2Tab = false;
            this.dtpTime.Location = new System.Drawing.Point(92, 17);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(96, 21);
            this.dtpTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpTime.TabIndex = 12;
            this.dtpTime.ValueChanged += new System.EventHandler(this.dtpTime_ValueChanged);
            // 
            // gbType
            // 
            this.gbType.Controls.Add(this.rbtShort);
            this.gbType.Controls.Add(this.rbtAll);
            this.gbType.Controls.Add(this.rbtLong);
            this.gbType.Location = new System.Drawing.Point(198, 8);
            this.gbType.Name = "gbType";
            this.gbType.Size = new System.Drawing.Size(162, 33);
            this.gbType.TabIndex = 13;
            this.gbType.TabStop = false;
            // 
            // rbtShort
            // 
            this.rbtShort.AutoSize = true;
            this.rbtShort.Location = new System.Drawing.Point(110, 11);
            this.rbtShort.Name = "rbtShort";
            this.rbtShort.Size = new System.Drawing.Size(47, 16);
            this.rbtShort.TabIndex = 10;
            this.rbtShort.Text = "临嘱";
            this.rbtShort.UseVisualStyleBackColor = true;
            // 
            // rbtAll
            // 
            this.rbtAll.AutoSize = true;
            this.rbtAll.Checked = true;
            this.rbtAll.Location = new System.Drawing.Point(6, 11);
            this.rbtAll.Name = "rbtAll";
            this.rbtAll.Size = new System.Drawing.Size(47, 16);
            this.rbtAll.TabIndex = 8;
            this.rbtAll.TabStop = true;
            this.rbtAll.Text = "全部";
            this.rbtAll.UseVisualStyleBackColor = true;
            // 
            // rbtLong
            // 
            this.rbtLong.AutoSize = true;
            this.rbtLong.Location = new System.Drawing.Point(58, 11);
            this.rbtLong.Name = "rbtLong";
            this.rbtLong.Size = new System.Drawing.Size(47, 16);
            this.rbtLong.TabIndex = 9;
            this.rbtLong.Text = "长嘱";
            this.rbtLong.UseVisualStyleBackColor = true;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(21, 21);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(65, 12);
            this.lblTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTime.TabIndex = 11;
            this.lblTime.Text = "查询时间：";
            // 
            // cbIsPrinted
            // 
            this.cbIsPrinted.AutoSize = true;
            this.cbIsPrinted.Location = new System.Drawing.Point(376, 19);
            this.cbIsPrinted.Name = "cbIsPrinted";
            this.cbIsPrinted.Size = new System.Drawing.Size(48, 16);
            this.cbIsPrinted.TabIndex = 0;
            this.cbIsPrinted.Text = "补打";
            this.cbIsPrinted.UseVisualStyleBackColor = true;
            this.cbIsPrinted.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 135);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(850, 3);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 138);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(850, 412);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(850, 412);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance3;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.neuSpread1_ButtonClicked);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 14;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "医嘱流水号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "住院号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "床号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "组";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "打印";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "用量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "频次";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "是否打印";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "医嘱流水号";
            this.neuSpread1_Sheet1.Columns.Get(0).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 87F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "住院号";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "床号";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "类型";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "组";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 40F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "打印";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 200F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "用量";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "单位";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "频次";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "是否打印";
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 65F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucNewExecBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucNewExecBill";
            this.Size = new System.Drawing.Size(850, 550);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.gbUsage.ResumeLayout(false);
            this.gbUsage.PerformLayout();
            this.gbType.ResumeLayout(false);
            this.gbType.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.GroupBox gbType;
        private System.Windows.Forms.RadioButton rbtShort;
        private System.Windows.Forms.RadioButton rbtAll;
        private System.Windows.Forms.RadioButton rbtLong;
        protected FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpTime;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblTime;
        private System.Windows.Forms.GroupBox gbUsage;
        private System.Windows.Forms.CheckBox cb4;
        private System.Windows.Forms.CheckBox cb3;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.CheckBox cb1;
        private System.Windows.Forms.CheckBox cb7;
        private System.Windows.Forms.CheckBox cb6;
        private System.Windows.Forms.CheckBox cb5;
        private System.Windows.Forms.CheckBox cb8;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.CheckBox cbIsPrinted;
        private System.Windows.Forms.Button btnPatient;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.CheckBox cb16;
        private System.Windows.Forms.CheckBox cb15;
        private System.Windows.Forms.CheckBox cb14;
        private System.Windows.Forms.CheckBox cb13;
        private System.Windows.Forms.CheckBox cb12;
        private System.Windows.Forms.CheckBox cb11;
        private System.Windows.Forms.CheckBox cb10;
        private System.Windows.Forms.CheckBox cb9;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbbPrint;



    }
}
