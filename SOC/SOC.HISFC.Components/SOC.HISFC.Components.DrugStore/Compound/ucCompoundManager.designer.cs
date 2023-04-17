namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    partial class ucCompoundManager
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCompoundManager));
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.rbCancel = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbAll = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbPrinted = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbPrinting = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.dtMinDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkPhamrcy = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbParmacy = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtMaxDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrderGroup = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpListStyle = new System.Windows.Forms.TabPage();
            this.fpApply = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpApply_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpCardStyle = new System.Windows.Forms.TabPage();
            this.pnlCardCollections = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tpListStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply_Sheet1)).BeginInit();
            this.tpCardStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(947, 380);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.neuGroupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.neuTabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(947, 380);
            this.splitContainer2.SplitterDistance = 49;
            this.splitContainer2.TabIndex = 0;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.rbCancel);
            this.neuGroupBox1.Controls.Add(this.rbAll);
            this.neuGroupBox1.Controls.Add(this.rbPrinted);
            this.neuGroupBox1.Controls.Add(this.rbPrinting);
            this.neuGroupBox1.Controls.Add(this.dtMinDate);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.chkPhamrcy);
            this.neuGroupBox1.Controls.Add(this.cmbParmacy);
            this.neuGroupBox1.Controls.Add(this.dtMaxDate);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.cmbOrderGroup);
            this.neuGroupBox1.Controls.Add(this.lbInfo);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(947, 49);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // rbCancel
            // 
            this.rbCancel.AutoSize = true;
            this.rbCancel.Location = new System.Drawing.Point(862, 17);
            this.rbCancel.Name = "rbCancel";
            this.rbCancel.Size = new System.Drawing.Size(83, 16);
            this.rbCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbCancel.TabIndex = 10;
            this.rbCancel.TabStop = true;
            this.rbCancel.Text = "已作废打印";
            this.rbCancel.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(879, 18);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(47, 16);
            this.rbAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbAll.TabIndex = 9;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "全部";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbPrinted
            // 
            this.rbPrinted.AutoSize = true;
            this.rbPrinted.Checked = true;
            this.rbPrinted.Location = new System.Drawing.Point(794, 17);
            this.rbPrinted.Name = "rbPrinted";
            this.rbPrinted.Size = new System.Drawing.Size(59, 16);
            this.rbPrinted.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbPrinted.TabIndex = 9;
            this.rbPrinted.TabStop = true;
            this.rbPrinted.Text = "已打印";
            this.rbPrinted.UseVisualStyleBackColor = true;
            // 
            // rbPrinting
            // 
            this.rbPrinting.AutoSize = true;
            this.rbPrinting.Location = new System.Drawing.Point(727, 17);
            this.rbPrinting.Name = "rbPrinting";
            this.rbPrinting.Size = new System.Drawing.Size(59, 16);
            this.rbPrinting.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbPrinting.TabIndex = 9;
            this.rbPrinting.TabStop = true;
            this.rbPrinting.Text = "未打印";
            this.rbPrinting.UseVisualStyleBackColor = true;
            // 
            // dtMinDate
            // 
            this.dtMinDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtMinDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtMinDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMinDate.IsEnter2Tab = false;
            this.dtMinDate.Location = new System.Drawing.Point(186, 16);
            this.dtMinDate.Name = "dtMinDate";
            this.dtMinDate.Size = new System.Drawing.Size(141, 21);
            this.dtMinDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtMinDate.TabIndex = 8;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel3.Location = new System.Drawing.Point(129, 21);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 7;
            this.neuLabel3.Text = "开始时间";
            // 
            // chkPhamrcy
            // 
            this.chkPhamrcy.AutoSize = true;
            this.chkPhamrcy.Location = new System.Drawing.Point(543, 17);
            this.chkPhamrcy.Name = "chkPhamrcy";
            this.chkPhamrcy.Size = new System.Drawing.Size(72, 16);
            this.chkPhamrcy.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkPhamrcy.TabIndex = 6;
            this.chkPhamrcy.Text = "药品检索";
            this.chkPhamrcy.UseVisualStyleBackColor = true;
            this.chkPhamrcy.CheckedChanged += new System.EventHandler(this.chkPhamrcy_CheckedChanged);
            // 
            // cmbParmacy
            // 
            this.cmbParmacy.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbParmacy.FormattingEnabled = true;
            this.cmbParmacy.IsEnter2Tab = false;
            this.cmbParmacy.IsFlat = false;
            this.cmbParmacy.IsLike = true;
            this.cmbParmacy.IsListOnly = false;
            this.cmbParmacy.IsPopForm = true;
            this.cmbParmacy.IsShowCustomerList = false;
            this.cmbParmacy.IsShowID = false;
            this.cmbParmacy.Location = new System.Drawing.Point(621, 15);
            this.cmbParmacy.Name = "cmbParmacy";
            this.cmbParmacy.PopForm = null;
            this.cmbParmacy.ShowCustomerList = false;
            this.cmbParmacy.ShowID = false;
            this.cmbParmacy.Size = new System.Drawing.Size(101, 20);
            this.cmbParmacy.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbParmacy.TabIndex = 5;
            this.cmbParmacy.Tag = "";
            this.cmbParmacy.ToolBarUse = false;
            this.cmbParmacy.Visible = false;
            this.cmbParmacy.SelectedIndexChanged += new System.EventHandler(this.cmbParmacy_SelectedIndexChanged);
            // 
            // dtMaxDate
            // 
            this.dtMaxDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtMaxDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMaxDate.IsEnter2Tab = false;
            this.dtMaxDate.Location = new System.Drawing.Point(397, 16);
            this.dtMaxDate.Name = "dtMaxDate";
            this.dtMaxDate.Size = new System.Drawing.Size(140, 21);
            this.dtMaxDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtMaxDate.TabIndex = 4;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(337, 20);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "截至时间";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(5, 21);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "批  次：";
            // 
            // cmbOrderGroup
            // 
            this.cmbOrderGroup.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOrderGroup.FormattingEnabled = true;
            this.cmbOrderGroup.IsEnter2Tab = false;
            this.cmbOrderGroup.IsFlat = false;
            this.cmbOrderGroup.IsLike = true;
            this.cmbOrderGroup.IsListOnly = false;
            this.cmbOrderGroup.IsPopForm = true;
            this.cmbOrderGroup.IsShowCustomerList = false;
            this.cmbOrderGroup.IsShowID = false;
            //this.cmbOrderGroup.Items.AddRange(new object[] {
            //"全部",
            //"1",
            //"2",
            //"3",
            //"4",
            //"5",
            //"A",
            //"B",
            //"C",
            //"D",
            //"E"});
            this.cmbOrderGroup.Location = new System.Drawing.Point(59, 17);
            this.cmbOrderGroup.Name = "cmbOrderGroup";
            this.cmbOrderGroup.PopForm = null;
            this.cmbOrderGroup.ShowCustomerList = false;
            this.cmbOrderGroup.ShowID = false;
            this.cmbOrderGroup.Size = new System.Drawing.Size(60, 20);
            this.cmbOrderGroup.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbOrderGroup.TabIndex = 1;
            this.cmbOrderGroup.Tag = "";
            this.cmbOrderGroup.ToolBarUse = false;
            this.cmbOrderGroup.SelectedIndexChanged += new System.EventHandler(this.cmbOrderGroup_SelectedIndexChanged);
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.ForeColor = System.Drawing.Color.Blue;
            this.lbInfo.Location = new System.Drawing.Point(342, 21);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(0, 12);
            this.lbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Visible = false;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpListStyle);
            this.neuTabControl1.Controls.Add(this.tpCardStyle);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(947, 327);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 1;
            this.neuTabControl1.SelectedIndexChanged += new System.EventHandler(this.neuTabControl1_SelectedIndexChanged);
            // 
            // tpListStyle
            // 
            this.tpListStyle.Controls.Add(this.fpApply);
            this.tpListStyle.Location = new System.Drawing.Point(4, 21);
            this.tpListStyle.Name = "tpListStyle";
            this.tpListStyle.Padding = new System.Windows.Forms.Padding(3);
            this.tpListStyle.Size = new System.Drawing.Size(939, 302);
            this.tpListStyle.TabIndex = 0;
            this.tpListStyle.Text = "列表样式";
            this.tpListStyle.UseVisualStyleBackColor = true;
            // 
            // fpApply
            // 
            this.fpApply.About = "3.0.2004.2005";
            this.fpApply.AccessibleDescription = "fpApply, Sheet1";
            this.fpApply.BackColor = System.Drawing.Color.White;
            this.fpApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpApply.FileName = "";
            this.fpApply.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpApply.IsAutoSaveGridStatus = false;
            this.fpApply.IsCanCustomConfigColumn = false;
            this.fpApply.Location = new System.Drawing.Point(3, 3);
            this.fpApply.Name = "fpApply";
            this.fpApply.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpApply.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpApply_Sheet1});
            this.fpApply.Size = new System.Drawing.Size(933, 296);
            this.fpApply.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpApply.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpApply.TextTipAppearance = tipAppearance1;
            this.fpApply.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpApply.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpApply_ButtonClicked);
            this.fpApply.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpApply_EditChange);
            // 
            // fpApply_Sheet1
            // 
            this.fpApply_Sheet1.Reset();
            this.fpApply_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpApply_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpApply_Sheet1.ColumnCount = 17;
            this.fpApply_Sheet1.RowCount = 0;
            this.fpApply_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "[床号]姓名";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "选中";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "批次号";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "组";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "药品[规格]";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "零售价";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "用量";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "单位";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "总量";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "单位";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "频次";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "用法";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "用药时间";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "医嘱类型";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "开方医生";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "申请时间";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "组号";
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.Columns.Get(0).Label = "[床号]姓名";
            this.fpApply_Sheet1.Columns.Get(0).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.fpApply_Sheet1.Columns.Get(0).Width = 78F;
            this.fpApply_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.SeaShell;
            this.fpApply_Sheet1.Columns.Get(1).CellType = checkBoxCellType1;
            this.fpApply_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpApply_Sheet1.Columns.Get(1).Label = "选中";
            this.fpApply_Sheet1.Columns.Get(1).Locked = false;
            this.fpApply_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.fpApply_Sheet1.Columns.Get(1).Width = 38F;
            this.fpApply_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.fpApply_Sheet1.Columns.Get(3).Label = "组";
            this.fpApply_Sheet1.Columns.Get(3).Width = 29F;
            this.fpApply_Sheet1.Columns.Get(4).Label = "药品[规格]";
            this.fpApply_Sheet1.Columns.Get(4).Width = 140F;
            this.fpApply_Sheet1.Columns.Get(5).CellType = numberCellType1;
            this.fpApply_Sheet1.Columns.Get(5).Label = "零售价";
            this.fpApply_Sheet1.Columns.Get(6).CellType = numberCellType2;
            this.fpApply_Sheet1.Columns.Get(6).Label = "用量";
            this.fpApply_Sheet1.Columns.Get(7).Label = "单位";
            this.fpApply_Sheet1.Columns.Get(7).Width = 40F;
            this.fpApply_Sheet1.Columns.Get(8).CellType = numberCellType3;
            this.fpApply_Sheet1.Columns.Get(8).Label = "总量";
            this.fpApply_Sheet1.Columns.Get(9).Label = "单位";
            this.fpApply_Sheet1.Columns.Get(9).Width = 40F;
            this.fpApply_Sheet1.Columns.Get(10).Label = "频次";
            this.fpApply_Sheet1.Columns.Get(10).Width = 46F;
            this.fpApply_Sheet1.Columns.Get(11).Label = "用法";
            this.fpApply_Sheet1.Columns.Get(11).Width = 50F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2007, 8, 21, 10, 9, 47, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;
            dateTimeCellType1.TimeDefault = new System.DateTime(2007, 8, 21, 10, 9, 47, 0);
            this.fpApply_Sheet1.Columns.Get(12).CellType = dateTimeCellType1;
            this.fpApply_Sheet1.Columns.Get(12).Label = "用药时间";
            this.fpApply_Sheet1.Columns.Get(12).Width = 113F;
            this.fpApply_Sheet1.Columns.Get(14).CellType = textCellType2;
            this.fpApply_Sheet1.Columns.Get(14).Label = "开方医生";
            this.fpApply_Sheet1.Columns.Get(14).Width = 70F;
            this.fpApply_Sheet1.Columns.Get(15).Label = "申请时间";
            this.fpApply_Sheet1.Columns.Get(15).Width = 106F;
            this.fpApply_Sheet1.Columns.Get(16).Label = "组号";
            this.fpApply_Sheet1.Columns.Get(16).Visible = false;
            this.fpApply_Sheet1.Columns.Get(16).Width = 27F;
            this.fpApply_Sheet1.DefaultStyle.Locked = true;
            this.fpApply_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpApply_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpApply_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpApply_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.SheetCornerStyle.Locked = false;
            this.fpApply_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpApply_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpApply.SetActiveViewport(0, 1, 0);
            // 
            // tpCardStyle
            // 
            this.tpCardStyle.Controls.Add(this.pnlCardCollections);
            this.tpCardStyle.Location = new System.Drawing.Point(4, 21);
            this.tpCardStyle.Name = "tpCardStyle";
            this.tpCardStyle.Padding = new System.Windows.Forms.Padding(3);
            this.tpCardStyle.Size = new System.Drawing.Size(939, 302);
            this.tpCardStyle.TabIndex = 1;
            this.tpCardStyle.Text = "卡片样式";
            this.tpCardStyle.UseVisualStyleBackColor = true;
            // 
            // pnlCardCollections
            // 
            this.pnlCardCollections.AutoScroll = true;
            this.pnlCardCollections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardCollections.Location = new System.Drawing.Point(3, 3);
            this.pnlCardCollections.Name = "pnlCardCollections";
            this.pnlCardCollections.Size = new System.Drawing.Size(933, 296);
            this.pnlCardCollections.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlCardCollections.TabIndex = 0;
            // 
            // ucCompoundManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucCompoundManager";
            this.Size = new System.Drawing.Size(947, 380);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuTabControl1.ResumeLayout(false);
            this.tpListStyle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply_Sheet1)).EndInit();
            this.tpCardStyle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbInfo;
        private FarPoint.Win.Spread.SheetView fpApply_Sheet1;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpApply;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOrderGroup;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtMaxDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkPhamrcy;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbParmacy;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tpListStyle;
        private System.Windows.Forms.TabPage tpCardStyle;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlCardCollections;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtMinDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbPrinting;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbPrinted;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbAll;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbCancel;
    }
}
