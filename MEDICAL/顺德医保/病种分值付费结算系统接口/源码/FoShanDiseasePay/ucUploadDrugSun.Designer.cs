namespace FoShanDiseasePay
{
    partial class ucUploadDrugSun
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.gbTop = new Neusoft.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbUpType = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDrugDept = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblCountry = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.txtQueryCode = new System.Windows.Forms.TextBox();
            this.cbChooseAll = new Neusoft.FrameWork.WinForms.Controls.NeuCheckBox();
            this.dtEndTime = new Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel6 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBeginTime = new Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel5 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.lblLoginInfo = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.tpNeedUpload = new System.Windows.Forms.TabPage();
            this.fpNeedUpload = new Neusoft.FrameWork.WinForms.Controls.NeuSpread();
            this.fpNeedUpload_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabMain = new Neusoft.FrameWork.WinForms.Controls.NeuTabControl();
            this.gbTop.SuspendLayout();
            this.tpNeedUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpNeedUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpNeedUpload_Sheet1)).BeginInit();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTop
            // 
            this.gbTop.Controls.Add(this.cmbUpType);
            this.gbTop.Controls.Add(this.neuLabel3);
            this.gbTop.Controls.Add(this.cmbDrugDept);
            this.gbTop.Controls.Add(this.lblCountry);
            this.gbTop.Controls.Add(this.neuLabel1);
            this.gbTop.Controls.Add(this.txtQueryCode);
            this.gbTop.Controls.Add(this.cbChooseAll);
            this.gbTop.Controls.Add(this.dtEndTime);
            this.gbTop.Controls.Add(this.neuLabel6);
            this.gbTop.Controls.Add(this.dtBeginTime);
            this.gbTop.Controls.Add(this.neuLabel5);
            this.gbTop.Controls.Add(this.lblLoginInfo);
            this.gbTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTop.Location = new System.Drawing.Point(0, 0);
            this.gbTop.Name = "gbTop";
            this.gbTop.Size = new System.Drawing.Size(1230, 79);
            this.gbTop.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTop.TabIndex = 0;
            this.gbTop.TabStop = false;
            this.gbTop.Text = "操作信息";
            // 
            // cmbUpType
            // 
            this.cmbUpType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbUpType.FormattingEnabled = true;
            this.cmbUpType.IsEnter2Tab = false;
            this.cmbUpType.IsFlat = false;
            this.cmbUpType.IsLike = true;
            this.cmbUpType.IsListOnly = false;
            this.cmbUpType.IsPopForm = true;
            this.cmbUpType.IsShowCustomerList = false;
            this.cmbUpType.IsShowID = false;
            this.cmbUpType.IsShowIDAndName = false;
            this.cmbUpType.Location = new System.Drawing.Point(1040, 31);
            this.cmbUpType.Name = "cmbUpType";
            this.cmbUpType.PopForm = null;
            this.cmbUpType.ShowCustomerList = false;
            this.cmbUpType.ShowID = false;
            this.cmbUpType.Size = new System.Drawing.Size(160, 20);
            this.cmbUpType.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbUpType.TabIndex = 20;
            this.cmbUpType.Tag = "";
            this.cmbUpType.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.Location = new System.Drawing.Point(921, 35);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(131, 15);
            this.neuLabel3.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 19;
            this.neuLabel3.Text = "基本信息上传标志：";
            // 
            // cmbDrugDept
            // 
            this.cmbDrugDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDrugDept.FormattingEnabled = true;
            this.cmbDrugDept.IsEnter2Tab = false;
            this.cmbDrugDept.IsFlat = false;
            this.cmbDrugDept.IsLike = true;
            this.cmbDrugDept.IsListOnly = false;
            this.cmbDrugDept.IsPopForm = true;
            this.cmbDrugDept.IsShowCustomerList = false;
            this.cmbDrugDept.IsShowID = false;
            this.cmbDrugDept.IsShowIDAndName = false;
            this.cmbDrugDept.Location = new System.Drawing.Point(83, 31);
            this.cmbDrugDept.Name = "cmbDrugDept";
            this.cmbDrugDept.PopForm = null;
            this.cmbDrugDept.ShowCustomerList = false;
            this.cmbDrugDept.ShowID = false;
            this.cmbDrugDept.Size = new System.Drawing.Size(160, 20);
            this.cmbDrugDept.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDrugDept.TabIndex = 18;
            this.cmbDrugDept.Tag = "";
            this.cmbDrugDept.ToolBarUse = false;
            // 
            // lblCountry
            // 
            this.lblCountry.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCountry.Location = new System.Drawing.Point(19, 36);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(73, 13);
            this.lblCountry.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCountry.TabIndex = 17;
            this.lblCountry.Text = "库  房：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(718, 36);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 16;
            this.neuLabel1.Text = "查询：";
            // 
            // txtQueryCode
            // 
            this.txtQueryCode.Location = new System.Drawing.Point(765, 32);
            this.txtQueryCode.Name = "txtQueryCode";
            this.txtQueryCode.Size = new System.Drawing.Size(137, 21);
            this.txtQueryCode.TabIndex = 15;
            this.txtQueryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQueryCode_KeyDown);
            // 
            // cbChooseAll
            // 
            this.cbChooseAll.AutoSize = true;
            this.cbChooseAll.Checked = true;
            this.cbChooseAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbChooseAll.Location = new System.Drawing.Point(660, 36);
            this.cbChooseAll.Name = "cbChooseAll";
            this.cbChooseAll.Size = new System.Drawing.Size(48, 16);
            this.cbChooseAll.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbChooseAll.TabIndex = 14;
            this.cbChooseAll.Text = "全选";
            this.cbChooseAll.UseVisualStyleBackColor = true;
            this.cbChooseAll.CheckedChanged += new System.EventHandler(this.cbChooseAll_CheckedChanged);
            // 
            // dtEndTime
            // 
            this.dtEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndTime.IsEnter2Tab = false;
            this.dtEndTime.Location = new System.Drawing.Point(498, 32);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(140, 21);
            this.dtEndTime.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEndTime.TabIndex = 12;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(484, 36);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(11, 12);
            this.neuLabel6.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 11;
            this.neuLabel6.Text = "-";
            // 
            // dtBeginTime
            // 
            this.dtBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBeginTime.IsEnter2Tab = false;
            this.dtBeginTime.Location = new System.Drawing.Point(339, 32);
            this.dtBeginTime.Name = "dtBeginTime";
            this.dtBeginTime.Size = new System.Drawing.Size(140, 21);
            this.dtBeginTime.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBeginTime.TabIndex = 10;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(279, 34);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 9;
            this.neuLabel5.Text = "日    期：";
            // 
            // lblLoginInfo
            // 
            this.lblLoginInfo.AutoSize = true;
            this.lblLoginInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoginInfo.ForeColor = System.Drawing.Color.Red;
            this.lblLoginInfo.Location = new System.Drawing.Point(6, 109);
            this.lblLoginInfo.Name = "lblLoginInfo";
            this.lblLoginInfo.Size = new System.Drawing.Size(59, 12);
            this.lblLoginInfo.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLoginInfo.TabIndex = 8;
            this.lblLoginInfo.Text = "请先登录!";
            // 
            // tpNeedUpload
            // 
            this.tpNeedUpload.Controls.Add(this.fpNeedUpload);
            this.tpNeedUpload.ForeColor = System.Drawing.Color.Red;
            this.tpNeedUpload.Location = new System.Drawing.Point(4, 22);
            this.tpNeedUpload.Name = "tpNeedUpload";
            this.tpNeedUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpNeedUpload.Size = new System.Drawing.Size(1222, 395);
            this.tpNeedUpload.TabIndex = 0;
            this.tpNeedUpload.Text = "药品信息";
            this.tpNeedUpload.UseVisualStyleBackColor = true;
            // 
            // fpNeedUpload
            // 
            this.fpNeedUpload.About = "3.0.2004.2005";
            this.fpNeedUpload.AccessibleDescription = "fpNeedUpload, Sheet1, Row 0, Column 0, ";
            this.fpNeedUpload.BackColor = System.Drawing.Color.White;
            this.fpNeedUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpNeedUpload.FileName = "";
            this.fpNeedUpload.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpNeedUpload.IsAutoSaveGridStatus = false;
            this.fpNeedUpload.IsCanCustomConfigColumn = false;
            this.fpNeedUpload.Location = new System.Drawing.Point(3, 3);
            this.fpNeedUpload.Name = "fpNeedUpload";
            this.fpNeedUpload.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpNeedUpload.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpNeedUpload_Sheet1});
            this.fpNeedUpload.Size = new System.Drawing.Size(1216, 389);
            this.fpNeedUpload.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpNeedUpload.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpNeedUpload.TextTipAppearance = tipAppearance1;
            this.fpNeedUpload.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpNeedUpload_Sheet1
            // 
            this.fpNeedUpload_Sheet1.Reset();
            this.fpNeedUpload_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpNeedUpload_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpNeedUpload_Sheet1.ColumnCount = 15;
            this.fpNeedUpload_Sheet1.RowCount = 3;
            this.fpNeedUpload_Sheet1.Cells.Get(0, 1).Value = "Y00000000361";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 2).Value = "盐酸坦索罗辛缓释胶囊";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 3).Value = "0480002";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 4).Value = "0154";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 5).Value = "0.2mg*10粒/盒";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 6).Value = "59.00";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 7).Value = "国药准字H20000681";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 8).Value = "安斯泰来制药(中国)有限公司";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 9).Value = "安斯泰来制药(中国)有限公司";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 10).Value = "99009133";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 11).Value = "1838";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "药品编码";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "药品名称";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "自定义码";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "国标码";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "规格";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "零售价";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "批文号";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "厂家";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "供货商";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "库存";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "发票号";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "基本信息上传标志";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "CLINIC_CODE";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "PACT_CODE";
            this.fpNeedUpload_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpNeedUpload_Sheet1.Columns.Get(0).Label = "选择";
            this.fpNeedUpload_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpNeedUpload_Sheet1.Columns.Get(0).Width = 30F;
            this.fpNeedUpload_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpNeedUpload_Sheet1.Columns.Get(1).Label = "药品编码";
            this.fpNeedUpload_Sheet1.Columns.Get(1).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(1).Width = 80F;
            this.fpNeedUpload_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.fpNeedUpload_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(2).Label = "药品名称";
            this.fpNeedUpload_Sheet1.Columns.Get(2).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(2).Width = 200F;
            this.fpNeedUpload_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.fpNeedUpload_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(3).Label = "自定义码";
            this.fpNeedUpload_Sheet1.Columns.Get(3).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(3).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.fpNeedUpload_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(4).Label = "国标码";
            this.fpNeedUpload_Sheet1.Columns.Get(4).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(4).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(5).CellType = textCellType5;
            this.fpNeedUpload_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(5).Label = "规格";
            this.fpNeedUpload_Sheet1.Columns.Get(5).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(5).Width = 120F;
            this.fpNeedUpload_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(6).CellType = textCellType6;
            this.fpNeedUpload_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(6).Label = "零售价";
            this.fpNeedUpload_Sheet1.Columns.Get(6).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(6).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(7).CellType = textCellType7;
            this.fpNeedUpload_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(7).Label = "批文号";
            this.fpNeedUpload_Sheet1.Columns.Get(7).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(7).Width = 120F;
            this.fpNeedUpload_Sheet1.Columns.Get(8).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(8).CellType = textCellType8;
            this.fpNeedUpload_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(8).Label = "厂家";
            this.fpNeedUpload_Sheet1.Columns.Get(8).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(8).Width = 170F;
            this.fpNeedUpload_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(9).CellType = textCellType9;
            this.fpNeedUpload_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(9).Label = "供货商";
            this.fpNeedUpload_Sheet1.Columns.Get(9).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(9).Width = 170F;
            this.fpNeedUpload_Sheet1.Columns.Get(10).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(10).CellType = textCellType9;
            this.fpNeedUpload_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(10).Label = "库存";
            this.fpNeedUpload_Sheet1.Columns.Get(10).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(10).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(11).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(11).CellType = textCellType9;
            this.fpNeedUpload_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(11).Label = "发票号";
            this.fpNeedUpload_Sheet1.Columns.Get(11).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(11).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(12).AllowAutoSort = true;
            this.fpNeedUpload_Sheet1.Columns.Get(12).Label = "基本信息上传标志";
            this.fpNeedUpload_Sheet1.Columns.Get(12).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(13).Label = "CLINIC_CODE";
            this.fpNeedUpload_Sheet1.Columns.Get(13).Visible = false;
            this.fpNeedUpload_Sheet1.Columns.Get(14).Label = "PACT_CODE";
            this.fpNeedUpload_Sheet1.Columns.Get(14).Visible = false;
            this.fpNeedUpload_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpNeedUpload_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpNeedUpload_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpNeedUpload_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpNeedUpload);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 79);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1230, 421);
            this.tabMain.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabMain.TabIndex = 1;
            // 
            // ucUploadDrugSun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.gbTop);
            this.Name = "ucUploadDrugSun";
            this.Size = new System.Drawing.Size(1230, 500);
            this.gbTop.ResumeLayout(false);
            this.gbTop.PerformLayout();
            this.tpNeedUpload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpNeedUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpNeedUpload_Sheet1)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Neusoft.FrameWork.WinForms.Controls.NeuGroupBox gbTop;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel lblLoginInfo;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker dtBeginTime;
        private Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker dtEndTime;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private Neusoft.FrameWork.WinForms.Controls.NeuCheckBox cbChooseAll;
        private Neusoft.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private System.Windows.Forms.TabPage tpNeedUpload;
        private Neusoft.FrameWork.WinForms.Controls.NeuSpread fpNeedUpload;
        private FarPoint.Win.Spread.SheetView fpNeedUpload_Sheet1;
        private Neusoft.FrameWork.WinForms.Controls.NeuTabControl tabMain;
        private System.Windows.Forms.TextBox txtQueryCode;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox cmbDrugDept;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel lblCountry;
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox cmbUpType;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
    }
}
