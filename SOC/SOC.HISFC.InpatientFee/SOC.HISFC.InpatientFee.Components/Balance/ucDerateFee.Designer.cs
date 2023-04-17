namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    partial class ucDerateFee
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDerateType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.rbRate = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.ntxRate = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.ntxtFee = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.rbCost = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpDerateInfo = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpDerateInfo_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btCancelAll = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btCancelOne = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btDerateAll = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btDerateOne = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.fpFeeInfo = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpFeeInfo_TotFee = new FarPoint.Win.Spread.SheetView();
            this.fpFeeInfo_FeeCode = new FarPoint.Win.Spread.SheetView();
            this.fpFeeInfo_Items = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cmbMinFee = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpDerateInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDerateInfo_Sheet1)).BeginInit();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeInfo_TotFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeInfo_FeeCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeInfo_Items)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.lblPatientInfo);
            this.neuGroupBox1.Controls.Add(this.ucQueryInpatientNo1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(1089, 40);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientInfo.Location = new System.Drawing.Point(245, 17);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(0, 14);
            this.lblPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientInfo.TabIndex = 3;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.IsDeptOnly = true;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(21, 8);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInpatientNo1.TabIndex = 0;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.txtFilter);
            this.neuGroupBox3.Controls.Add(this.cmbMinFee);
            this.neuGroupBox3.Controls.Add(this.label2);
            this.neuGroupBox3.Controls.Add(this.neuLabel1);
            this.neuGroupBox3.Controls.Add(this.cmbDerateType);
            this.neuGroupBox3.Controls.Add(this.rbRate);
            this.neuGroupBox3.Controls.Add(this.ntxRate);
            this.neuGroupBox3.Controls.Add(this.ntxtFee);
            this.neuGroupBox3.Controls.Add(this.rbCost);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 40);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(1089, 38);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 4;
            this.neuGroupBox3.TabStop = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(939, 16);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 5;
            this.neuLabel1.Text = "减免类型";
            // 
            // cmbDerateType
            // 
            this.cmbDerateType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDerateType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDerateType.FormattingEnabled = true;
            this.cmbDerateType.IsEnter2Tab = false;
            this.cmbDerateType.IsFlat = false;
            this.cmbDerateType.IsLike = true;
            this.cmbDerateType.IsListOnly = false;
            this.cmbDerateType.IsPopForm = true;
            this.cmbDerateType.IsShowCustomerList = false;
            this.cmbDerateType.IsShowID = false;
            this.cmbDerateType.Location = new System.Drawing.Point(1003, 11);
            this.cmbDerateType.Name = "cmbDerateType";
            this.cmbDerateType.ShowCustomerList = false;
            this.cmbDerateType.ShowID = false;
            this.cmbDerateType.Size = new System.Drawing.Size(121, 20);
            this.cmbDerateType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDerateType.TabIndex = 4;
            this.cmbDerateType.Tag = "";
            this.cmbDerateType.ToolBarUse = false;
            // 
            // rbRate
            // 
            this.rbRate.AutoSize = true;
            this.rbRate.Location = new System.Drawing.Point(801, 15);
            this.rbRate.Name = "rbRate";
            this.rbRate.Size = new System.Drawing.Size(59, 16);
            this.rbRate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbRate.TabIndex = 3;
            this.rbRate.TabStop = true;
            this.rbRate.Text = "按比例";
            this.rbRate.UseVisualStyleBackColor = true;
            this.rbRate.CheckedChanged += new System.EventHandler(this.rbRate_CheckedChanged);
            // 
            // ntxRate
            // 
            this.ntxRate.AllowNegative = false;
            this.ntxRate.IsAutoRemoveDecimalZero = false;
            this.ntxRate.IsEnter2Tab = false;
            this.ntxRate.Location = new System.Drawing.Point(866, 11);
            this.ntxRate.Name = "ntxRate";
            this.ntxRate.NumericPrecision = 3;
            this.ntxRate.NumericScaleOnFocus = 2;
            this.ntxRate.NumericScaleOnLostFocus = 2;
            this.ntxRate.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ntxRate.SetRange = new System.Drawing.Size(-1, -1);
            this.ntxRate.Size = new System.Drawing.Size(62, 21);
            this.ntxRate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxRate.TabIndex = 2;
            this.ntxRate.Text = "0.00";
            this.ntxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntxRate.UseGroupSeperator = true;
            this.ntxRate.ZeroIsValid = true;
            this.ntxRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ntxRate_KeyDown);
            // 
            // ntxtFee
            // 
            this.ntxtFee.AllowNegative = false;
            this.ntxtFee.IsAutoRemoveDecimalZero = false;
            this.ntxtFee.IsEnter2Tab = false;
            this.ntxtFee.Location = new System.Drawing.Point(718, 11);
            this.ntxtFee.Name = "ntxtFee";
            this.ntxtFee.NumericPrecision = 10;
            this.ntxtFee.NumericScaleOnFocus = 2;
            this.ntxtFee.NumericScaleOnLostFocus = 2;
            this.ntxtFee.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ntxtFee.SetRange = new System.Drawing.Size(-1, -1);
            this.ntxtFee.Size = new System.Drawing.Size(77, 21);
            this.ntxtFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtFee.TabIndex = 1;
            this.ntxtFee.Text = "0.00";
            this.ntxtFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntxtFee.UseGroupSeperator = true;
            this.ntxtFee.ZeroIsValid = true;
            this.ntxtFee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ntxtFee_KeyDown);
            // 
            // rbCost
            // 
            this.rbCost.AutoSize = true;
            this.rbCost.Location = new System.Drawing.Point(653, 14);
            this.rbCost.Name = "rbCost";
            this.rbCost.Size = new System.Drawing.Size(59, 16);
            this.rbCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbCost.TabIndex = 0;
            this.rbCost.TabStop = true;
            this.rbCost.Text = "按金额";
            this.rbCost.UseVisualStyleBackColor = true;
            this.rbCost.CheckedChanged += new System.EventHandler(this.rbCost_CheckedChanged);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.fpDerateInfo);
            this.neuPanel2.Controls.Add(this.neuPanel3);
            this.neuPanel2.Controls.Add(this.neuSplitter1);
            this.neuPanel2.Controls.Add(this.fpFeeInfo);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 78);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1089, 434);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 5;
            // 
            // fpDerateInfo
            // 
            this.fpDerateInfo.About = "3.0.2004.2005";
            this.fpDerateInfo.AccessibleDescription = "fpDerateInfo, Sheet1";
            this.fpDerateInfo.BackColor = System.Drawing.Color.White;
            this.fpDerateInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpDerateInfo.FileName = "";
            this.fpDerateInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpDerateInfo.IsAutoSaveGridStatus = false;
            this.fpDerateInfo.IsCanCustomConfigColumn = false;
            this.fpDerateInfo.Location = new System.Drawing.Point(652, 0);
            this.fpDerateInfo.Name = "fpDerateInfo";
            this.fpDerateInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpDerateInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpDerateInfo_Sheet1});
            this.fpDerateInfo.Size = new System.Drawing.Size(437, 434);
            this.fpDerateInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpDerateInfo.TabIndex = 3;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpDerateInfo.TextTipAppearance = tipAppearance1;
            this.fpDerateInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpDerateInfo_Sheet1
            // 
            this.fpDerateInfo_Sheet1.Reset();
            this.fpDerateInfo_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpDerateInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpDerateInfo_Sheet1.ColumnCount = 21;
            this.fpDerateInfo_Sheet1.RowCount = 0;
            this.fpDerateInfo_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "减免种类";
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "减免类型";
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "最小费用";
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "减免金额";
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "科室";
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "操作员";
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "操作时间";
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "项目";
            this.fpDerateInfo_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "有效性";
            this.fpDerateInfo_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDerateInfo_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDerateInfo_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpDerateInfo_Sheet1.Columns.Get(0).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(1).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(2).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(3).Label = "减免种类";
            this.fpDerateInfo_Sheet1.Columns.Get(3).Width = 61F;
            this.fpDerateInfo_Sheet1.Columns.Get(4).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(6).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(9).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(10).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(11).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(12).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(14).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(15).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(18).Visible = false;
            this.fpDerateInfo_Sheet1.Columns.Get(18).Width = 126F;
            this.fpDerateInfo_Sheet1.Columns.Get(19).Label = "项目";
            this.fpDerateInfo_Sheet1.Columns.Get(19).Width = 122F;
            this.fpDerateInfo_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpDerateInfo_Sheet1.RowHeader.Columns.Get(0).Width = 26F;
            this.fpDerateInfo_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDerateInfo_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDerateInfo_Sheet1.Rows.Default.Height = 25F;
            this.fpDerateInfo_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpDerateInfo_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpDerateInfo_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDerateInfo_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpDerateInfo_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpDerateInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpDerateInfo.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.btCancelAll);
            this.neuPanel3.Controls.Add(this.btCancelOne);
            this.neuPanel3.Controls.Add(this.btDerateAll);
            this.neuPanel3.Controls.Add(this.btDerateOne);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel3.Location = new System.Drawing.Point(621, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(31, 434);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // btCancelAll
            // 
            this.btCancelAll.Location = new System.Drawing.Point(1, 241);
            this.btCancelAll.Name = "btCancelAll";
            this.btCancelAll.Size = new System.Drawing.Size(28, 51);
            this.btCancelAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancelAll.TabIndex = 3;
            this.btCancelAll.Text = "<<";
            this.btCancelAll.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancelAll.UseVisualStyleBackColor = true;
            this.btCancelAll.Click += new System.EventHandler(this.btCancelAll_Click);
            // 
            // btCancelOne
            // 
            this.btCancelOne.Location = new System.Drawing.Point(1, 184);
            this.btCancelOne.Name = "btCancelOne";
            this.btCancelOne.Size = new System.Drawing.Size(28, 51);
            this.btCancelOne.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancelOne.TabIndex = 2;
            this.btCancelOne.Text = "<";
            this.btCancelOne.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancelOne.UseVisualStyleBackColor = true;
            this.btCancelOne.Click += new System.EventHandler(this.btCancelOne_Click);
            // 
            // btDerateAll
            // 
            this.btDerateAll.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDerateAll.Location = new System.Drawing.Point(1, 79);
            this.btDerateAll.Name = "btDerateAll";
            this.btDerateAll.Size = new System.Drawing.Size(28, 51);
            this.btDerateAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btDerateAll.TabIndex = 1;
            this.btDerateAll.Text = ">>";
            this.btDerateAll.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btDerateAll.UseVisualStyleBackColor = true;
            this.btDerateAll.Click += new System.EventHandler(this.btDerateAll_Click);
            // 
            // btDerateOne
            // 
            this.btDerateOne.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDerateOne.Location = new System.Drawing.Point(1, 22);
            this.btDerateOne.Name = "btDerateOne";
            this.btDerateOne.Size = new System.Drawing.Size(28, 51);
            this.btDerateOne.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btDerateOne.TabIndex = 0;
            this.btDerateOne.Text = ">";
            this.btDerateOne.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btDerateOne.UseVisualStyleBackColor = true;
            this.btDerateOne.Click += new System.EventHandler(this.btDerateOne_Click);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(618, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 434);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // fpFeeInfo
            // 
            this.fpFeeInfo.About = "3.0.2004.2005";
            this.fpFeeInfo.AccessibleDescription = "fpFeeInfo, 明细";
            this.fpFeeInfo.BackColor = System.Drawing.Color.White;
            this.fpFeeInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.fpFeeInfo.FileName = "";
            this.fpFeeInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFeeInfo.IsAutoSaveGridStatus = false;
            this.fpFeeInfo.IsCanCustomConfigColumn = false;
            this.fpFeeInfo.Location = new System.Drawing.Point(0, 0);
            this.fpFeeInfo.Name = "fpFeeInfo";
            this.fpFeeInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpFeeInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpFeeInfo_TotFee,
            this.fpFeeInfo_FeeCode,
            this.fpFeeInfo_Items});
            this.fpFeeInfo.Size = new System.Drawing.Size(618, 434);
            this.fpFeeInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpFeeInfo.TabIndex = 0;
            this.fpFeeInfo.TabStrip.ButtonPolicy = FarPoint.Win.Spread.TabStripButtonPolicy.AsNeeded;
            this.fpFeeInfo.TabStrip.DefaultSheetTab.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpFeeInfo.TabStrip.DefaultSheetTab.Size = -1;
            this.fpFeeInfo.TabStripPlacement = FarPoint.Win.Spread.TabStripPlacement.Top;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFeeInfo.TextTipAppearance = tipAppearance2;
            this.fpFeeInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFeeInfo.ActiveSheetIndex = 1;
            // 
            // fpFeeInfo_TotFee
            // 
            this.fpFeeInfo_TotFee.Reset();
            this.fpFeeInfo_TotFee.SheetName = "总费用";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFeeInfo_TotFee.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpFeeInfo_TotFee.ColumnCount = 6;
            this.fpFeeInfo_TotFee.RowCount = 0;
            this.fpFeeInfo_TotFee.ActiveColumnIndex = 5;
            this.fpFeeInfo_TotFee.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpFeeInfo_TotFee.ColumnHeader.Cells.Get(0, 0).Value = "总金额";
            this.fpFeeInfo_TotFee.ColumnHeader.Cells.Get(0, 1).Value = "自费金额";
            this.fpFeeInfo_TotFee.ColumnHeader.Cells.Get(0, 2).Value = "自付金额";
            this.fpFeeInfo_TotFee.ColumnHeader.Cells.Get(0, 3).Value = "公费金额";
            this.fpFeeInfo_TotFee.ColumnHeader.Cells.Get(0, 4).Value = "已减免金额";
            this.fpFeeInfo_TotFee.ColumnHeader.Cells.Get(0, 5).Value = "剩余可减免金额";
            this.fpFeeInfo_TotFee.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_TotFee.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeInfo_TotFee.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpFeeInfo_TotFee.Columns.Get(0).CellType = numberCellType1;
            this.fpFeeInfo_TotFee.Columns.Get(0).Label = "总金额";
            this.fpFeeInfo_TotFee.Columns.Get(0).Width = 62F;
            this.fpFeeInfo_TotFee.Columns.Get(1).CellType = numberCellType2;
            this.fpFeeInfo_TotFee.Columns.Get(1).Label = "自费金额";
            this.fpFeeInfo_TotFee.Columns.Get(1).Width = 61F;
            this.fpFeeInfo_TotFee.Columns.Get(2).CellType = numberCellType3;
            this.fpFeeInfo_TotFee.Columns.Get(2).Label = "自付金额";
            this.fpFeeInfo_TotFee.Columns.Get(2).Width = 63F;
            this.fpFeeInfo_TotFee.Columns.Get(3).CellType = numberCellType4;
            this.fpFeeInfo_TotFee.Columns.Get(3).Label = "公费金额";
            this.fpFeeInfo_TotFee.Columns.Get(3).Locked = false;
            this.fpFeeInfo_TotFee.Columns.Get(3).Width = 63F;
            this.fpFeeInfo_TotFee.Columns.Get(4).CellType = numberCellType5;
            this.fpFeeInfo_TotFee.Columns.Get(4).Label = "已减免金额";
            this.fpFeeInfo_TotFee.Columns.Get(4).Width = 75F;
            this.fpFeeInfo_TotFee.Columns.Get(5).CellType = numberCellType6;
            this.fpFeeInfo_TotFee.Columns.Get(5).Label = "剩余可减免金额";
            this.fpFeeInfo_TotFee.Columns.Get(5).Width = 99F;
            this.fpFeeInfo_TotFee.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpFeeInfo_TotFee.RowHeader.Columns.Default.Resizable = false;
            this.fpFeeInfo_TotFee.RowHeader.Columns.Get(0).Width = 27F;
            this.fpFeeInfo_TotFee.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_TotFee.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeInfo_TotFee.Rows.Default.Height = 25F;
            this.fpFeeInfo_TotFee.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_TotFee.SheetCornerStyle.Parent = "CornerDefault";
            this.fpFeeInfo_TotFee.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpFeeInfo_TotFee.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFeeInfo.SetActiveViewport(0, 1, 0);
            // 
            // fpFeeInfo_FeeCode
            // 
            this.fpFeeInfo_FeeCode.Reset();
            this.fpFeeInfo_FeeCode.SheetName = "最小费用";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFeeInfo_FeeCode.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpFeeInfo_FeeCode.ColumnCount = 12;
            this.fpFeeInfo_FeeCode.RowCount = 0;
            this.fpFeeInfo_FeeCode.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 2).Value = "费用名称";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 3).Value = "总金额";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 4).Value = "自费金额";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 5).Value = "自付金额";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 6).Value = "公费金额";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 7).Value = "已减免金额";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 8).Value = "剩余可减免金额";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 9).Value = "拼音码";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 10).Value = "五笔码";
            this.fpFeeInfo_FeeCode.ColumnHeader.Cells.Get(0, 11).Value = "自定义码";
            this.fpFeeInfo_FeeCode.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_FeeCode.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeInfo_FeeCode.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpFeeInfo_FeeCode.Columns.Get(0).Visible = false;
            this.fpFeeInfo_FeeCode.Columns.Get(1).Visible = false;
            this.fpFeeInfo_FeeCode.Columns.Get(7).Label = "已减免金额";
            this.fpFeeInfo_FeeCode.Columns.Get(7).Width = 75F;
            this.fpFeeInfo_FeeCode.Columns.Get(8).Label = "剩余可减免金额";
            this.fpFeeInfo_FeeCode.Columns.Get(8).Width = 99F;
            this.fpFeeInfo_FeeCode.Columns.Get(9).Label = "拼音码";
            this.fpFeeInfo_FeeCode.Columns.Get(9).Visible = false;
            this.fpFeeInfo_FeeCode.Columns.Get(10).Label = "五笔码";
            this.fpFeeInfo_FeeCode.Columns.Get(10).Visible = false;
            this.fpFeeInfo_FeeCode.Columns.Get(11).Label = "自定义码";
            this.fpFeeInfo_FeeCode.Columns.Get(11).Visible = false;
            this.fpFeeInfo_FeeCode.RowHeader.Columns.Default.Resizable = false;
            this.fpFeeInfo_FeeCode.RowHeader.Columns.Get(0).Width = 28F;
            this.fpFeeInfo_FeeCode.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_FeeCode.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeInfo_FeeCode.Rows.Default.Height = 25F;
            this.fpFeeInfo_FeeCode.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpFeeInfo_FeeCode.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpFeeInfo_FeeCode.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_FeeCode.SheetCornerStyle.Parent = "CornerDefault";
            this.fpFeeInfo_FeeCode.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpFeeInfo_FeeCode.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFeeInfo.SetActiveViewport(1, 1, 0);
            // 
            // fpFeeInfo_Items
            // 
            this.fpFeeInfo_Items.Reset();
            this.fpFeeInfo_Items.SheetName = "明细";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFeeInfo_Items.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpFeeInfo_Items.ColumnCount = 12;
            this.fpFeeInfo_Items.RowCount = 0;
            this.fpFeeInfo_Items.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 0).Value = "费用编码";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 1).Value = "费用名称";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 2).Value = "项目编码";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 3).Value = "项目名称";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 4).Value = "费用总额";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 5).Value = "自费金额";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 6).Value = "已减免金额";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 7).Value = "剩余可减免金额";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 9).Value = "拼音码";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 10).Value = "五笔码";
            this.fpFeeInfo_Items.ColumnHeader.Cells.Get(0, 11).Value = "自定义码";
            this.fpFeeInfo_Items.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_Items.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeInfo_Items.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpFeeInfo_Items.Columns.Get(0).Label = "费用编码";
            this.fpFeeInfo_Items.Columns.Get(0).Width = 0F;
            this.fpFeeInfo_Items.Columns.Get(2).Label = "项目编码";
            this.fpFeeInfo_Items.Columns.Get(2).Width = 0F;
            this.fpFeeInfo_Items.Columns.Get(3).Label = "项目名称";
            this.fpFeeInfo_Items.Columns.Get(3).Width = 223F;
            this.fpFeeInfo_Items.Columns.Get(7).Label = "剩余可减免金额";
            this.fpFeeInfo_Items.Columns.Get(7).Width = 99F;
            this.fpFeeInfo_Items.Columns.Get(8).Visible = false;
            this.fpFeeInfo_Items.Columns.Get(9).Label = "拼音码";
            this.fpFeeInfo_Items.Columns.Get(9).Visible = false;
            this.fpFeeInfo_Items.Columns.Get(10).Label = "五笔码";
            this.fpFeeInfo_Items.Columns.Get(10).Visible = false;
            this.fpFeeInfo_Items.Columns.Get(11).Label = "自定义码";
            this.fpFeeInfo_Items.Columns.Get(11).Visible = false;
            this.fpFeeInfo_Items.RowHeader.Columns.Default.Resizable = true;
            this.fpFeeInfo_Items.RowHeader.Columns.Get(0).Width = 31F;
            this.fpFeeInfo_Items.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_Items.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeInfo_Items.Rows.Default.Height = 25F;
            this.fpFeeInfo_Items.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpFeeInfo_Items.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpFeeInfo_Items.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeInfo_Items.SheetCornerStyle.Parent = "CornerDefault";
            this.fpFeeInfo_Items.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpFeeInfo_Items.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFeeInfo.SetActiveViewport(2, 1, 0);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(0, 512);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1089, 38);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(30, 13);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(377, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "注意：自费患者可减免全部费用，医保、公费患者只能减免高收费部分";
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFilter.Location = new System.Drawing.Point(185, 11);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(433, 21);
            this.txtFilter.TabIndex = 38;
            // 
            // cmbMinFee
            // 
            this.cmbMinFee.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbMinFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMinFee.FormattingEnabled = true;
            this.cmbMinFee.IsEnter2Tab = false;
            this.cmbMinFee.IsFlat = false;
            this.cmbMinFee.IsLike = true;
            this.cmbMinFee.IsListOnly = false;
            this.cmbMinFee.IsPopForm = true;
            this.cmbMinFee.IsShowCustomerList = false;
            this.cmbMinFee.IsShowID = false;
            this.cmbMinFee.Location = new System.Drawing.Point(65, 11);
            this.cmbMinFee.Name = "cmbMinFee";
            this.cmbMinFee.ShowCustomerList = false;
            this.cmbMinFee.ShowID = false;
            this.cmbMinFee.Size = new System.Drawing.Size(116, 20);
            this.cmbMinFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbMinFee.TabIndex = 37;
            this.cmbMinFee.Tag = "";
            this.cmbMinFee.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "过滤:";
            // 
            // ucDerateFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuGroupBox3);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucDerateFee";
            this.Size = new System.Drawing.Size(1089, 550);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpDerateInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDerateInfo_Sheet1)).EndInit();
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeInfo_TotFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeInfo_FeeCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeInfo_Items)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucInpatientInfo1;
 
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpDerateInfo;
        private FarPoint.Win.Spread.SheetView fpDerateInfo_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpFeeInfo;
        private FarPoint.Win.Spread.SheetView fpFeeInfo_TotFee;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FarPoint.Win.Spread.SheetView fpFeeInfo_FeeCode;
        private FarPoint.Win.Spread.SheetView fpFeeInfo_Items;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbRate;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox ntxRate;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox ntxtFee;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbCost;
        private FS.FrameWork.WinForms.Controls.NeuButton btDerateOne;
        private FS.FrameWork.WinForms.Controls.NeuButton btDerateAll;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDerateType;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancelOne;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancelAll;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientInfo;
        private System.Windows.Forms.TextBox txtFilter;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMinFee;
        private System.Windows.Forms.Label label2;
        //private FS.FrameWork.WinForms.Controls.NeuSpread fpDerateInfo;
        //private FS.FrameWork.WinForms.Controls.NeuSpread fpFeeInfo;
    }
}
