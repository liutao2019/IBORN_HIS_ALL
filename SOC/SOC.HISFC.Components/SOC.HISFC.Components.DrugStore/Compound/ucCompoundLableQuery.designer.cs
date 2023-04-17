namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    partial class ucCompoundLableQuery
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpCompoundSelect = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpCompoundSelect_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpCompoundDetial = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpCompoundDetial_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLbDrugType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuCmbDrugType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ckChange = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblDrugBill = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundSelect_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundDetial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundDetial_Sheet1)).BeginInit();
            this.neuPanel3.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.fpCompoundSelect);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(325, 800);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // fpCompoundSelect
            // 
            this.fpCompoundSelect.About = "3.0.2004.2005";
            this.fpCompoundSelect.AccessibleDescription = "fpCompoundSelect, Sheet1";
            this.fpCompoundSelect.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpCompoundSelect.FileName = "";
            this.fpCompoundSelect.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCompoundSelect.IsAutoSaveGridStatus = false;
            this.fpCompoundSelect.IsCanCustomConfigColumn = false;
            this.fpCompoundSelect.Location = new System.Drawing.Point(0, 0);
            this.fpCompoundSelect.Name = "fpCompoundSelect";
            this.fpCompoundSelect.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCompoundSelect_Sheet1});
            this.fpCompoundSelect.Size = new System.Drawing.Size(325, 800);
            this.fpCompoundSelect.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpCompoundSelect.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCompoundSelect.TextTipAppearance = tipAppearance1;
            this.fpCompoundSelect.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCompoundSelect.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpCompoundSelect_ButtonClicked);
            // 
            // fpCompoundSelect_Sheet1
            // 
            this.fpCompoundSelect_Sheet1.Reset();
            this.fpCompoundSelect_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCompoundSelect_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCompoundSelect_Sheet1.ColumnCount = 5;
            this.fpCompoundSelect_Sheet1.RowCount = 0;
            this.fpCompoundSelect_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "科室编码";
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "科室名称";
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "选择";
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "批次号";
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "用药时间";
            this.fpCompoundSelect_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpCompoundSelect_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompoundSelect_Sheet1.Columns.Get(1).Label = "科室名称";
            this.fpCompoundSelect_Sheet1.Columns.Get(1).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.fpCompoundSelect_Sheet1.Columns.Get(1).Width = 62F;
            this.fpCompoundSelect_Sheet1.Columns.Get(2).CellType = checkBoxCellType1;
            this.fpCompoundSelect_Sheet1.Columns.Get(2).Label = "选择";
            this.fpCompoundSelect_Sheet1.Columns.Get(2).Width = 34F;
            this.fpCompoundSelect_Sheet1.Columns.Get(4).Label = "用药时间";
            this.fpCompoundSelect_Sheet1.Columns.Get(4).Width = 118F;
            this.fpCompoundSelect_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect_Sheet1.DefaultStyle.Locked = false;
            this.fpCompoundSelect_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpCompoundSelect_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpCompoundSelect_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpCompoundSelect_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompoundSelect_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect_Sheet1.SheetCornerStyle.Locked = false;
            this.fpCompoundSelect_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpCompoundSelect_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpCompoundSelect_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpCompoundSelect.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuPanel4);
            this.neuPanel2.Controls.Add(this.neuPanel3);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(325, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(826, 800);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.fpCompoundDetial);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel4.Location = new System.Drawing.Point(0, 53);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(826, 747);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 1;
            // 
            // fpCompoundDetial
            // 
            this.fpCompoundDetial.About = "3.0.2004.2005";
            this.fpCompoundDetial.AccessibleDescription = "fpCompoundDetial, Sheet1";
            this.fpCompoundDetial.BackColor = System.Drawing.Color.White;
            this.fpCompoundDetial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpCompoundDetial.FileName = "";
            this.fpCompoundDetial.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCompoundDetial.IsAutoSaveGridStatus = false;
            this.fpCompoundDetial.IsCanCustomConfigColumn = false;
            this.fpCompoundDetial.Location = new System.Drawing.Point(0, 0);
            this.fpCompoundDetial.Name = "fpCompoundDetial";
            this.fpCompoundDetial.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCompoundDetial_Sheet1});
            this.fpCompoundDetial.Size = new System.Drawing.Size(826, 747);
            this.fpCompoundDetial.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpCompoundDetial.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCompoundDetial.TextTipAppearance = tipAppearance2;
            this.fpCompoundDetial.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpCompoundDetial_Sheet1
            // 
            this.fpCompoundDetial_Sheet1.Reset();
            this.fpCompoundDetial_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCompoundDetial_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCompoundDetial_Sheet1.ColumnCount = 15;
            this.fpCompoundDetial_Sheet1.RowCount = 0;
            this.fpCompoundDetial_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "批次号";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "摆药单号";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "患者姓名";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "组";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "药品名称";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "零售价";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "用量";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "总量";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "频次";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "用法";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "用药时间";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "开药医生";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "开药时间";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "组合号";
            this.fpCompoundDetial_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "存储条件";
            this.fpCompoundDetial_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundDetial_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpCompoundDetial_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompoundDetial_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpCompoundDetial_Sheet1.Columns.Get(1).Label = "摆药单号";
            this.fpCompoundDetial_Sheet1.Columns.Get(1).Width = 69F;
            this.fpCompoundDetial_Sheet1.Columns.Get(3).Label = "组";
            this.fpCompoundDetial_Sheet1.Columns.Get(3).Width = 31F;
            this.fpCompoundDetial_Sheet1.Columns.Get(4).Label = "药品名称";
            this.fpCompoundDetial_Sheet1.Columns.Get(4).Width = 115F;
            this.fpCompoundDetial_Sheet1.Columns.Get(6).Label = "用量";
            this.fpCompoundDetial_Sheet1.Columns.Get(6).Width = 66F;
            this.fpCompoundDetial_Sheet1.Columns.Get(8).Label = "频次";
            this.fpCompoundDetial_Sheet1.Columns.Get(8).Width = 54F;
            this.fpCompoundDetial_Sheet1.Columns.Get(10).Label = "用药时间";
            this.fpCompoundDetial_Sheet1.Columns.Get(10).Width = 112F;
            this.fpCompoundDetial_Sheet1.Columns.Get(11).Label = "开药医生";
            this.fpCompoundDetial_Sheet1.Columns.Get(11).Width = 77F;
            this.fpCompoundDetial_Sheet1.Columns.Get(12).Label = "开药时间";
            this.fpCompoundDetial_Sheet1.Columns.Get(12).Width = 112F;
            this.fpCompoundDetial_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundDetial_Sheet1.DefaultStyle.Locked = false;
            this.fpCompoundDetial_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpCompoundDetial_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpCompoundDetial_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundDetial_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpCompoundDetial_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompoundDetial_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundDetial_Sheet1.SheetCornerStyle.Locked = false;
            this.fpCompoundDetial_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpCompoundDetial_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpCompoundDetial_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpCompoundDetial.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuGroupBox1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(826, 53);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 0;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.neuLbDrugType);
            this.neuGroupBox1.Controls.Add(this.neuCmbDrugType);
            this.neuGroupBox1.Controls.Add(this.ckChange);
            this.neuGroupBox1.Controls.Add(this.txtCardNo);
            this.neuGroupBox1.Controls.Add(this.cmbType);
            this.neuGroupBox1.Controls.Add(this.lblDrugBill);
            this.neuGroupBox1.Controls.Add(this.dtEnd);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.dtStart);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(826, 53);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询条件";
            // 
            // neuLbDrugType
            // 
            this.neuLbDrugType.AutoSize = true;
            this.neuLbDrugType.ForeColor = System.Drawing.Color.Blue;
            this.neuLbDrugType.Location = new System.Drawing.Point(694, 28);
            this.neuLbDrugType.Name = "neuLbDrugType";
            this.neuLbDrugType.Size = new System.Drawing.Size(65, 12);
            this.neuLbDrugType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLbDrugType.TabIndex = 13;
            this.neuLbDrugType.Text = "药品类别：";
            // 
            // neuCmbDrugType
            // 
            this.neuCmbDrugType.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuCmbDrugType.FormattingEnabled = true;
            this.neuCmbDrugType.IsEnter2Tab = false;
            this.neuCmbDrugType.IsFlat = false;
            //this.neuCmbDrugType.IsLeftPad = false;
            this.neuCmbDrugType.IsLike = true;
            this.neuCmbDrugType.IsListOnly = false;
            this.neuCmbDrugType.IsPopForm = true;
            this.neuCmbDrugType.IsShowCustomerList = false;
            this.neuCmbDrugType.IsShowID = false;
            this.neuCmbDrugType.Location = new System.Drawing.Point(762, 24);
            this.neuCmbDrugType.Name = "neuCmbDrugType";
            this.neuCmbDrugType.PopForm = null;
            this.neuCmbDrugType.ShowCustomerList = false;
            this.neuCmbDrugType.ShowID = false;
            this.neuCmbDrugType.Size = new System.Drawing.Size(96, 20);
            this.neuCmbDrugType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuCmbDrugType.TabIndex = 12;
            this.neuCmbDrugType.Tag = "";
            this.neuCmbDrugType.ToolBarUse = false;
            // 
            // ckChange
            // 
            this.ckChange.AutoSize = true;
            this.ckChange.Location = new System.Drawing.Point(412, 26);
            this.ckChange.Name = "ckChange";
            this.ckChange.Size = new System.Drawing.Size(15, 14);
            this.ckChange.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckChange.TabIndex = 11;
            this.ckChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckChange.UseVisualStyleBackColor = true;
            this.ckChange.CheckedChanged += new System.EventHandler(this.ckChange_CheckedChanged);
            // 
            // txtCardNo
            // 
            this.txtCardNo.Enabled = false;
            //this.txtCardNo.Interval = 1500;
            this.txtCardNo.IsEnter2Tab = false;
            //this.txtCardNo.IsStopPaste = false;
            //this.txtCardNo.IsTimerDel = false;
            //this.txtCardNo.IsUseTimer = false;
            this.txtCardNo.Location = new System.Drawing.Point(564, 23);
            this.txtCardNo.Name = "txtCardNo";
            //this.txtCardNo.SendKey = System.Windows.Forms.Keys.Return;
            this.txtCardNo.Size = new System.Drawing.Size(124, 21);
            this.txtCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNo.TabIndex = 10;
            // 
            // cmbType
            // 
            this.cmbType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbType.Enabled = false;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.IsEnter2Tab = false;
            this.cmbType.IsFlat = false;
            //this.cmbType.IsLeftPad = false;
            this.cmbType.IsLike = true;
            this.cmbType.IsListOnly = false;
            this.cmbType.IsPopForm = true;
            this.cmbType.IsShowCustomerList = false;
            this.cmbType.IsShowID = false;
            this.cmbType.Location = new System.Drawing.Point(480, 22);
            this.cmbType.Name = "cmbType";
            this.cmbType.PopForm = null;
            this.cmbType.ShowCustomerList = false;
            this.cmbType.ShowID = false;
            this.cmbType.Size = new System.Drawing.Size(79, 22);
            this.cmbType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbType.TabIndex = 9;
            this.cmbType.Tag = "";
            this.cmbType.ToolBarUse = false;
            // 
            // lblDrugBill
            // 
            this.lblDrugBill.AutoSize = true;
            this.lblDrugBill.Enabled = false;
            this.lblDrugBill.ForeColor = System.Drawing.Color.Blue;
            this.lblDrugBill.Location = new System.Drawing.Point(429, 27);
            this.lblDrugBill.Name = "lblDrugBill";
            this.lblDrugBill.Size = new System.Drawing.Size(53, 12);
            this.lblDrugBill.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDrugBill.TabIndex = 8;
            this.lblDrugBill.Text = "摆药单：";
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(247, 23);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(153, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 7;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(232, 27);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(11, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 6;
            this.neuLabel2.Text = "-";
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.IsEnter2Tab = false;
            this.dtStart.Location = new System.Drawing.Point(76, 23);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(153, 21);
            this.dtStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtStart.TabIndex = 5;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(9, 27);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "用药时间：";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(325, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 800);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // ucCompoundLableQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucCompoundLableQuery";
            this.Size = new System.Drawing.Size(1151, 800);
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundSelect_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundDetial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundDetial_Sheet1)).EndInit();
            this.neuPanel3.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpCompoundDetial;
        private FarPoint.Win.Spread.SheetView fpCompoundDetial_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtStart;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDrugBill;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpCompoundSelect;
        private FarPoint.Win.Spread.SheetView fpCompoundSelect_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckChange;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLbDrugType;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuCmbDrugType;
    }
}
