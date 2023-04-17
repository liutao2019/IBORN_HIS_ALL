namespace HISFC.Components.Package.Fee.Controls
{
    partial class ucPackgeOperate
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.gbPatient = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.chkAll = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtIDCardType = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtsiNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCountry = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtSex = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtRace = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtIDCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtMarkNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblCardNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.trvInvoice = new System.Windows.Forms.TreeView();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gbPatient.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.neuPanel5.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPatient
            // 
            this.gbPatient.BackColor = System.Drawing.Color.Transparent;
            this.gbPatient.Controls.Add(this.chkAll);
            this.gbPatient.Controls.Add(this.txtIDCardType);
            this.gbPatient.Controls.Add(this.txtsiNo);
            this.gbPatient.Controls.Add(this.txtCountry);
            this.gbPatient.Controls.Add(this.txtAge);
            this.gbPatient.Controls.Add(this.txtSex);
            this.gbPatient.Controls.Add(this.txtRace);
            this.gbPatient.Controls.Add(this.txtName);
            this.gbPatient.Controls.Add(this.neuLabel12);
            this.gbPatient.Controls.Add(this.txtIDCardNO);
            this.gbPatient.Controls.Add(this.neuLabel11);
            this.gbPatient.Controls.Add(this.neuLabel10);
            this.gbPatient.Controls.Add(this.neuLabel9);
            this.gbPatient.Controls.Add(this.neuLabel5);
            this.gbPatient.Controls.Add(this.neuLabel4);
            this.gbPatient.Controls.Add(this.neuLabel3);
            this.gbPatient.Controls.Add(this.neuLabel2);
            this.gbPatient.Controls.Add(this.txtMarkNo);
            this.gbPatient.Controls.Add(this.lblCardNo);
            this.gbPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPatient.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbPatient.Location = new System.Drawing.Point(0, 0);
            this.gbPatient.Name = "gbPatient";
            this.gbPatient.Size = new System.Drawing.Size(1452, 86);
            this.gbPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatient.TabIndex = 5;
            this.gbPatient.TabStop = false;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(855, 52);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkAll.TabIndex = 53;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // txtIDCardType
            // 
            this.txtIDCardType.Enabled = false;
            this.txtIDCardType.IsEnter2Tab = false;
            this.txtIDCardType.Location = new System.Drawing.Point(77, 50);
            this.txtIDCardType.Name = "txtIDCardType";
            this.txtIDCardType.ReadOnly = true;
            this.txtIDCardType.Size = new System.Drawing.Size(118, 21);
            this.txtIDCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDCardType.TabIndex = 52;
            // 
            // txtsiNo
            // 
            this.txtsiNo.Enabled = false;
            this.txtsiNo.IsEnter2Tab = false;
            this.txtsiNo.Location = new System.Drawing.Point(702, 50);
            this.txtsiNo.Name = "txtsiNo";
            this.txtsiNo.ReadOnly = true;
            this.txtsiNo.Size = new System.Drawing.Size(117, 21);
            this.txtsiNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtsiNo.TabIndex = 46;
            // 
            // txtCountry
            // 
            this.txtCountry.Enabled = false;
            this.txtCountry.IsEnter2Tab = false;
            this.txtCountry.Location = new System.Drawing.Point(487, 50);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.ReadOnly = true;
            this.txtCountry.Size = new System.Drawing.Size(121, 21);
            this.txtCountry.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCountry.TabIndex = 45;
            // 
            // txtAge
            // 
            this.txtAge.Enabled = false;
            this.txtAge.IsEnter2Tab = false;
            this.txtAge.Location = new System.Drawing.Point(568, 13);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(40, 21);
            this.txtAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAge.TabIndex = 44;
            // 
            // txtSex
            // 
            this.txtSex.Enabled = false;
            this.txtSex.IsEnter2Tab = false;
            this.txtSex.Location = new System.Drawing.Point(487, 14);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(37, 21);
            this.txtSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSex.TabIndex = 43;
            // 
            // txtRace
            // 
            this.txtRace.Enabled = false;
            this.txtRace.IsEnter2Tab = false;
            this.txtRace.Location = new System.Drawing.Point(702, 14);
            this.txtRace.Name = "txtRace";
            this.txtRace.ReadOnly = true;
            this.txtRace.Size = new System.Drawing.Size(117, 21);
            this.txtRace.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRace.TabIndex = 42;
            // 
            // txtName
            // 
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(275, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(126, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 41;
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Location = new System.Drawing.Point(528, 18);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(41, 12);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 40;
            this.neuLabel12.Text = "年龄：";
            // 
            // txtIDCardNO
            // 
            this.txtIDCardNO.Enabled = false;
            this.txtIDCardNO.IsEnter2Tab = false;
            this.txtIDCardNO.Location = new System.Drawing.Point(275, 50);
            this.txtIDCardNO.Name = "txtIDCardNO";
            this.txtIDCardNO.ReadOnly = true;
            this.txtIDCardNO.Size = new System.Drawing.Size(126, 21);
            this.txtIDCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDCardNO.TabIndex = 39;
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Location = new System.Drawing.Point(631, 55);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(65, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 38;
            this.neuLabel11.Text = "医疗证号：";
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(440, 55);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(41, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 37;
            this.neuLabel10.Text = "国籍：";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(631, 18);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(65, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 36;
            this.neuLabel9.Text = "患者民族：";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(204, 55);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 35;
            this.neuLabel5.Text = "证 件 号：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(416, 19);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 34;
            this.neuLabel4.Text = "患者性别：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(7, 51);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 33;
            this.neuLabel3.Text = "证件类型：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(228, 19);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 32;
            this.neuLabel2.Text = "姓名：";
            // 
            // txtMarkNo
            // 
            this.txtMarkNo.IsEnter2Tab = false;
            this.txtMarkNo.Location = new System.Drawing.Point(77, 16);
            this.txtMarkNo.Name = "txtMarkNo";
            this.txtMarkNo.Size = new System.Drawing.Size(118, 21);
            this.txtMarkNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMarkNo.TabIndex = 31;
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Location = new System.Drawing.Point(6, 19);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(65, 12);
            this.lblCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCardNo.TabIndex = 30;
            this.lblCardNo.Text = "就诊卡号：";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 86);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1452, 308);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 6;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.fpPackage);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(228, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(1224, 308);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // fpPackage
            // 
            this.fpPackage.About = "3.0.2004.2005";
            this.fpPackage.AccessibleDescription = "fpPackage, Sheet1, Row 0, Column 0, ";
            this.fpPackage.BackColor = System.Drawing.Color.White;
            this.fpPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPackage.FileName = "";
            this.fpPackage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackage.IsAutoSaveGridStatus = false;
            this.fpPackage.IsCanCustomConfigColumn = false;
            this.fpPackage.Location = new System.Drawing.Point(0, 0);
            this.fpPackage.Name = "fpPackage";
            this.fpPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPackage_Sheet1});
            this.fpPackage.Size = new System.Drawing.Size(1224, 308);
            this.fpPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackage.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackage.TextTipAppearance = tipAppearance1;
            this.fpPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPackage_Sheet1
            // 
            this.fpPackage_Sheet1.Reset();
            this.fpPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackage_Sheet1.ColumnCount = 9;
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "特殊折扣";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "发票号";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "套餐名称";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "套餐包名称";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "购买时间";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "总金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "实收金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "赠送金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "优惠金额";
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpPackage_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpPackage_Sheet1.Columns.Get(0).Label = "特殊折扣";
            this.fpPackage_Sheet1.Columns.Get(0).Locked = false;
            this.fpPackage_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpPackage_Sheet1.Columns.Get(1).Label = "发票号";
            this.fpPackage_Sheet1.Columns.Get(1).Width = 120F;
            this.fpPackage_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPackage_Sheet1.Columns.Get(2).Label = "套餐名称";
            this.fpPackage_Sheet1.Columns.Get(2).Width = 197F;
            this.fpPackage_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPackage_Sheet1.Columns.Get(3).Label = "套餐包名称";
            this.fpPackage_Sheet1.Columns.Get(3).Width = 200F;
            this.fpPackage_Sheet1.Columns.Get(4).Label = "购买时间";
            this.fpPackage_Sheet1.Columns.Get(4).Width = 150F;
            this.fpPackage_Sheet1.Columns.Get(5).Label = "总金额";
            this.fpPackage_Sheet1.Columns.Get(5).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(6).Label = "实收金额";
            this.fpPackage_Sheet1.Columns.Get(6).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(7).Label = "赠送金额";
            this.fpPackage_Sheet1.Columns.Get(7).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(8).Label = "优惠金额";
            this.fpPackage_Sheet1.Columns.Get(8).Width = 70F;
            this.fpPackage_Sheet1.DefaultStyle.Locked = true;
            this.fpPackage_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPackage_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPackage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPackage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPackage_Sheet1.RowHeader.Columns.Get(0).Width = 27F;
            this.fpPackage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPackage_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPackage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPackage.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel2
            // 
            this.neuPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.neuPanel2.Controls.Add(this.neuPanel5);
            this.neuPanel2.Controls.Add(this.neuPanel4);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(228, 308);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 0;
            // 
            // neuPanel5
            // 
            this.neuPanel5.Controls.Add(this.trvInvoice);
            this.neuPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel5.Location = new System.Drawing.Point(0, 127);
            this.neuPanel5.Name = "neuPanel5";
            this.neuPanel5.Size = new System.Drawing.Size(224, 177);
            this.neuPanel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel5.TabIndex = 1;
            // 
            // trvInvoice
            // 
            this.trvInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvInvoice.Location = new System.Drawing.Point(0, 0);
            this.trvInvoice.Name = "trvInvoice";
            this.trvInvoice.Size = new System.Drawing.Size(224, 177);
            this.trvInvoice.TabIndex = 1;
            // 
            // neuPanel4
            // 
            this.neuPanel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.neuPanel4.Controls.Add(this.btnSearch);
            this.neuPanel4.Controls.Add(this.dtEnd);
            this.neuPanel4.Controls.Add(this.label2);
            this.neuPanel4.Controls.Add(this.dtBegin);
            this.neuPanel4.Controls.Add(this.label1);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(224, 127);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(131, 88);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 20;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(78, 52);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(128, 21);
            this.dtEnd.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "结束日期：";
            // 
            // dtBegin
            // 
            this.dtBegin.Location = new System.Drawing.Point(78, 14);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(128, 21);
            this.dtBegin.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "开始日期：";
            // 
            // ucPackgeOperate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.gbPatient);
            this.Name = "ucPackgeOperate";
            this.Size = new System.Drawing.Size(1452, 394);
            this.gbPatient.ResumeLayout(false);
            this.gbPatient.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel5.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatient;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtsiNo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCountry;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAge;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSex;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtRace;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIDCardNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMarkNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCardNo;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackage;
        private FarPoint.Win.Spread.SheetView fpPackage_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIDCardType;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel5;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView trvInvoice;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkAll;
    }
}
