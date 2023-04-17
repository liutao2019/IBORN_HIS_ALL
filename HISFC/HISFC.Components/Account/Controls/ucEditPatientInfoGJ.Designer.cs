namespace FS.HISFC.Components.Account.Controls
{
    partial class ucEditPatientInfoGJ
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.txtMarkNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.spInfo = new FarPoint.Win.Spread.SheetView();
            this.spPatient = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ucRegPatientInfo1 = new FS.HISFC.Components.Account.Controls.ucRegPatientInfoGJ();
            this.menu = new FS.FrameWork.WinForms.Controls.NeuContexMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPatient)).BeginInit();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMarkNO
            // 
            this.txtMarkNO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMarkNO.IsEnter2Tab = false;
            this.txtMarkNO.Location = new System.Drawing.Point(90, 15);
            this.txtMarkNO.Name = "txtMarkNO";
            this.txtMarkNO.Size = new System.Drawing.Size(120, 21);
            this.txtMarkNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMarkNO.TabIndex = 1;
            this.txtMarkNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMarkNO_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(17, 19);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "就诊卡号";
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuSpread1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 439);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(930, 74);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spInfo,
            this.spPatient});
            this.neuSpread1.Size = new System.Drawing.Size(930, 74);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellClick);
            this.neuSpread1.ActiveSheetIndex = 1;
            // 
            // spInfo
            // 
            this.spInfo.Reset();
            this.spInfo.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spInfo.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spInfo.ColumnCount = 6;
            this.spInfo.RowCount = 1;
            this.spInfo.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.spInfo.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.spInfo.ColumnHeader.Cells.Get(0, 2).Value = "编码";
            this.spInfo.ColumnHeader.Cells.Get(0, 3).Value = "名称";
            this.spInfo.ColumnHeader.Cells.Get(0, 4).Value = "编码";
            this.spInfo.ColumnHeader.Cells.Get(0, 5).Value = "名称";
            this.spInfo.Columns.Get(0).CellType = textCellType1;
            this.spInfo.Columns.Get(0).Label = "编码";
            this.spInfo.Columns.Get(0).Locked = true;
            this.spInfo.Columns.Get(0).Width = 100F;
            this.spInfo.Columns.Get(1).CellType = textCellType2;
            this.spInfo.Columns.Get(1).Label = "名称";
            this.spInfo.Columns.Get(1).Locked = true;
            this.spInfo.Columns.Get(1).Width = 170F;
            this.spInfo.Columns.Get(2).CellType = textCellType3;
            this.spInfo.Columns.Get(2).Label = "编码";
            this.spInfo.Columns.Get(2).Locked = true;
            this.spInfo.Columns.Get(2).Width = 100F;
            this.spInfo.Columns.Get(3).CellType = textCellType4;
            this.spInfo.Columns.Get(3).Label = "名称";
            this.spInfo.Columns.Get(3).Locked = true;
            this.spInfo.Columns.Get(3).Width = 170F;
            this.spInfo.Columns.Get(4).CellType = textCellType5;
            this.spInfo.Columns.Get(4).Label = "编码";
            this.spInfo.Columns.Get(4).Locked = true;
            this.spInfo.Columns.Get(4).Width = 100F;
            this.spInfo.Columns.Get(5).CellType = textCellType6;
            this.spInfo.Columns.Get(5).Label = "名称";
            this.spInfo.Columns.Get(5).Locked = true;
            this.spInfo.Columns.Get(5).Width = 170F;
            this.spInfo.RowHeader.Columns.Default.Resizable = false;
            this.spInfo.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // spPatient
            // 
            this.spPatient.Reset();
            this.spPatient.SheetName = "患者信息";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spPatient.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spPatient.ColumnCount = 12;
            this.spPatient.RowCount = 1;
            this.spPatient.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.spPatient.ColumnHeader.Cells.Get(0, 1).Value = "性别";
            this.spPatient.ColumnHeader.Cells.Get(0, 2).Value = "年龄";
            this.spPatient.ColumnHeader.Cells.Get(0, 3).Value = "民族";
            this.spPatient.ColumnHeader.Cells.Get(0, 4).Value = "费用来源";
            this.spPatient.ColumnHeader.Cells.Get(0, 5).Value = "证件类型";
            this.spPatient.ColumnHeader.Cells.Get(0, 6).Value = "证件号";
            this.spPatient.ColumnHeader.Cells.Get(0, 7).Value = "电话号码";
            this.spPatient.ColumnHeader.Cells.Get(0, 8).Value = "家庭住址";
            this.spPatient.ColumnHeader.Cells.Get(0, 9).Value = "病历号";
            this.spPatient.ColumnHeader.Cells.Get(0, 10).Value = "卡类型";
            this.spPatient.ColumnHeader.Cells.Get(0, 11).Value = "卡号";
            this.spPatient.Columns.Get(0).CellType = textCellType7;
            this.spPatient.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(0).Label = "姓名";
            this.spPatient.Columns.Get(0).Locked = true;
            this.spPatient.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(0).Width = 83F;
            this.spPatient.Columns.Get(1).CellType = textCellType8;
            this.spPatient.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(1).Label = "性别";
            this.spPatient.Columns.Get(1).Locked = true;
            this.spPatient.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(1).Width = 40F;
            this.spPatient.Columns.Get(2).CellType = textCellType9;
            this.spPatient.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(2).Label = "年龄";
            this.spPatient.Columns.Get(2).Locked = true;
            this.spPatient.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(2).Width = 44F;
            this.spPatient.Columns.Get(3).CellType = textCellType10;
            this.spPatient.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(3).Label = "民族";
            this.spPatient.Columns.Get(3).Locked = true;
            this.spPatient.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(3).Width = 66F;
            this.spPatient.Columns.Get(4).CellType = textCellType11;
            this.spPatient.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(4).Label = "费用来源";
            this.spPatient.Columns.Get(4).Locked = true;
            this.spPatient.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(4).Width = 81F;
            this.spPatient.Columns.Get(5).CellType = textCellType12;
            this.spPatient.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(5).Label = "证件类型";
            this.spPatient.Columns.Get(5).Locked = true;
            this.spPatient.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(5).Width = 65F;
            this.spPatient.Columns.Get(6).CellType = textCellType13;
            this.spPatient.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(6).Label = "证件号";
            this.spPatient.Columns.Get(6).Locked = true;
            this.spPatient.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(6).Width = 116F;
            this.spPatient.Columns.Get(7).CellType = textCellType14;
            this.spPatient.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(7).Label = "电话号码";
            this.spPatient.Columns.Get(7).Locked = true;
            this.spPatient.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(7).Width = 150F;
            this.spPatient.Columns.Get(8).CellType = textCellType15;
            this.spPatient.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(8).Label = "家庭住址";
            this.spPatient.Columns.Get(8).Locked = true;
            this.spPatient.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(8).Width = 160F;
            this.spPatient.Columns.Get(9).CellType = textCellType15;
            this.spPatient.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spPatient.Columns.Get(9).Label = "病历号";
            this.spPatient.Columns.Get(9).Locked = true;
            this.spPatient.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spPatient.Columns.Get(9).Width = 150F;
            this.spPatient.Columns.Get(10).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.spPatient.Columns.Get(10).CellType = textCellType16;
            this.spPatient.Columns.Get(10).Label = "卡类型";
            this.spPatient.Columns.Get(10).Locked = true;
            this.spPatient.Columns.Get(10).Width = 77F;
            this.spPatient.Columns.Get(11).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.spPatient.Columns.Get(11).CellType = textCellType17;
            this.spPatient.Columns.Get(11).Label = "卡号";
            this.spPatient.Columns.Get(11).Locked = true;
            this.spPatient.Columns.Get(11).Visible = false;
            this.spPatient.Columns.Get(11).Width = 86F;
            this.spPatient.GrayAreaBackColor = System.Drawing.Color.White;
            this.spPatient.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.spPatient.RowHeader.Columns.Default.Resizable = true;
            this.spPatient.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.spPatient.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel4);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.txtName);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.txtMarkNO);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(930, 48);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 4;
            this.neuGroupBox1.TabStop = false;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Red;
            this.neuLabel4.Location = new System.Drawing.Point(67, 19);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(17, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 0;
            this.neuLabel4.Text = "F2";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Red;
            this.neuLabel3.Location = new System.Drawing.Point(447, 19);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(17, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "F1";
            this.neuLabel3.Visible = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(470, 18);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(29, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "姓名";
            this.neuLabel2.Visible = false;
            // 
            // txtName
            // 
            this.txtName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(520, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(120, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            this.txtName.Visible = false;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.ucRegPatientInfo1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 48);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(930, 391);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 5;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "患者信息";
            // 
            // ucRegPatientInfo1
            // 
            this.ucRegPatientInfo1.AutoCardNo = "";
            this.ucRegPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucRegPatientInfo1.CardNO = "";
            this.ucRegPatientInfo1.CardWay = false;
            this.ucRegPatientInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRegPatientInfo1.IMustInpubByOne = 0;
            this.ucRegPatientInfo1.IsAutoBuildCardNo = false;
            this.ucRegPatientInfo1.IsEditMode = false;
            this.ucRegPatientInfo1.IsEnableEntry = true;
            this.ucRegPatientInfo1.IsEnableIDENO = true;
            this.ucRegPatientInfo1.IsEnableIDEType = true;
            this.ucRegPatientInfo1.IsEnablePact = true;
            this.ucRegPatientInfo1.IsEnableSiNO = true;
            this.ucRegPatientInfo1.IsEnableVip = true;
            this.ucRegPatientInfo1.IsFullConvertToHalf = true;
            this.ucRegPatientInfo1.IsInputBirthDay = false;
            this.ucRegPatientInfo1.IsInputIDENO = false;
            this.ucRegPatientInfo1.IsInputIDEType = false;
            this.ucRegPatientInfo1.IsInputName = false;
            this.ucRegPatientInfo1.IsInputPact = false;
            this.ucRegPatientInfo1.IsInputPHONE = true;
            this.ucRegPatientInfo1.IsInputSex = false;
            this.ucRegPatientInfo1.IsInputSiNo = false;
            this.ucRegPatientInfo1.IsInSequentialOrder = false;
            this.ucRegPatientInfo1.IsJudgePact = false;
            this.ucRegPatientInfo1.IsJudgePactByIdno = false;
            this.ucRegPatientInfo1.IsJumpHomePhone = false;
            this.ucRegPatientInfo1.IsLocalOperation = true;
            this.ucRegPatientInfo1.IsMustInputTabIndex = false;
            this.ucRegPatientInfo1.IsMutilPactInfo = false;
            this.ucRegPatientInfo1.IsPrint = false;
            this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter = false;
            this.ucRegPatientInfo1.IsShowTitle = false;
            this.ucRegPatientInfo1.IsTreatment = false;
            this.ucRegPatientInfo1.IsValidHospitalStaff = "";
            this.ucRegPatientInfo1.Location = new System.Drawing.Point(3, 17);
            this.ucRegPatientInfo1.McardNO = "";
            this.ucRegPatientInfo1.Name = "ucRegPatientInfo1";
            this.ucRegPatientInfo1.ParentFormToolBar = null;
            this.ucRegPatientInfo1.Size = new System.Drawing.Size(924, 371);
            this.ucRegPatientInfo1.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            this.menu.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "显示患者信息";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // ucEditPatientInfoGJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucEditPatientInfoGJ";
            this.Size = new System.Drawing.Size(930, 513);
            this.Load += new System.EventHandler(this.ucEditPatientInfo_Load);
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPatient)).EndInit();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMarkNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView spInfo;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private ucRegPatientInfoGJ ucRegPatientInfo1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FarPoint.Win.Spread.SheetView spPatient;
        private FS.FrameWork.WinForms.Controls.NeuContexMenu menu;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}
