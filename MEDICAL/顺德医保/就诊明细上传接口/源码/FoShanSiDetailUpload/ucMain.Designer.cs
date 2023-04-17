namespace FoShanSiDetailUpload
{
    partial class ucMain
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
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.gbTop = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.dtEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblLoginInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSessionID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPassWord = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtUserID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtHosInfo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tabMain = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpNeedUpload = new System.Windows.Forms.TabPage();
            this.fpNeedUpload = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpNeedUpload_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpUploaded = new System.Windows.Forms.TabPage();
            this.gbTop.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tpNeedUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpNeedUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpNeedUpload_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTop
            // 
            this.gbTop.Controls.Add(this.dtEndTime);
            this.gbTop.Controls.Add(this.neuLabel6);
            this.gbTop.Controls.Add(this.dtBeginTime);
            this.gbTop.Controls.Add(this.neuLabel5);
            this.gbTop.Controls.Add(this.lblLoginInfo);
            this.gbTop.Controls.Add(this.txtSessionID);
            this.gbTop.Controls.Add(this.neuLabel4);
            this.gbTop.Controls.Add(this.txtPassWord);
            this.gbTop.Controls.Add(this.neuLabel3);
            this.gbTop.Controls.Add(this.txtUserID);
            this.gbTop.Controls.Add(this.neuLabel2);
            this.gbTop.Controls.Add(this.txtHosInfo);
            this.gbTop.Controls.Add(this.neuLabel1);
            this.gbTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTop.Location = new System.Drawing.Point(0, 0);
            this.gbTop.Name = "gbTop";
            this.gbTop.Size = new System.Drawing.Size(1152, 133);
            this.gbTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTop.TabIndex = 0;
            this.gbTop.TabStop = false;
            this.gbTop.Text = "操作信息";
            // 
            // dtEndTime
            // 
            this.dtEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndTime.IsEnter2Tab = false;
            this.dtEndTime.Location = new System.Drawing.Point(265, 76);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(140, 21);
            this.dtEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEndTime.TabIndex = 12;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(232, 80);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(11, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 11;
            this.neuLabel6.Text = "-";
            // 
            // dtBeginTime
            // 
            this.dtBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBeginTime.IsEnter2Tab = false;
            this.dtBeginTime.Location = new System.Drawing.Point(77, 76);
            this.dtBeginTime.Name = "dtBeginTime";
            this.dtBeginTime.Size = new System.Drawing.Size(140, 21);
            this.dtBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBeginTime.TabIndex = 10;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(6, 78);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.lblLoginInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLoginInfo.TabIndex = 8;
            this.lblLoginInfo.Text = "请先登录!";
            // 
            // txtSessionID
            // 
            this.txtSessionID.IsEnter2Tab = false;
            this.txtSessionID.Location = new System.Drawing.Point(667, 36);
            this.txtSessionID.Name = "txtSessionID";
            this.txtSessionID.Size = new System.Drawing.Size(140, 21);
            this.txtSessionID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSessionID.TabIndex = 7;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(615, 39);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(53, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "会话ID：";
            // 
            // txtPassWord
            // 
            this.txtPassWord.IsEnter2Tab = false;
            this.txtPassWord.Location = new System.Drawing.Point(468, 36);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(140, 21);
            this.txtPassWord.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPassWord.TabIndex = 5;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(425, 39);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "密码：";
            // 
            // txtUserID
            // 
            this.txtUserID.IsEnter2Tab = false;
            this.txtUserID.Location = new System.Drawing.Point(268, 36);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(140, 21);
            this.txtUserID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtUserID.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(225, 39);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "账户：";
            // 
            // txtHosInfo
            // 
            this.txtHosInfo.BackColor = System.Drawing.Color.White;
            this.txtHosInfo.Enabled = false;
            this.txtHosInfo.IsEnter2Tab = false;
            this.txtHosInfo.Location = new System.Drawing.Point(77, 36);
            this.txtHosInfo.Name = "txtHosInfo";
            this.txtHosInfo.Size = new System.Drawing.Size(140, 21);
            this.txtHosInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHosInfo.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(6, 39);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "医院编号：";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpNeedUpload);
            this.tabMain.Controls.Add(this.tpUploaded);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 133);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1152, 367);
            this.tabMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabMain.TabIndex = 1;
            // 
            // tpNeedUpload
            // 
            this.tpNeedUpload.Controls.Add(this.fpNeedUpload);
            this.tpNeedUpload.ForeColor = System.Drawing.Color.Red;
            this.tpNeedUpload.Location = new System.Drawing.Point(4, 22);
            this.tpNeedUpload.Name = "tpNeedUpload";
            this.tpNeedUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpNeedUpload.Size = new System.Drawing.Size(1144, 341);
            this.tpNeedUpload.TabIndex = 0;
            this.tpNeedUpload.Text = "待上传患者";
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
            this.fpNeedUpload.Size = new System.Drawing.Size(1138, 335);
            this.fpNeedUpload.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.fpNeedUpload_Sheet1.ColumnCount = 16;
            this.fpNeedUpload_Sheet1.RowCount = 3;
            this.fpNeedUpload_Sheet1.Cells.Get(0, 1).Value = "门诊";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 2).Value = "0000502956";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 3).Value = "黄肖华";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 4).Value = "440623196304152668";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 5).Value = "男";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 6).Value = "特定门诊";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 7).Value = "160626280016";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 8).Value = "000012169100";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 9).Value = "10000.00";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 10).Value = "10000.00";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 11).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.fpNeedUpload_Sheet1.Cells.Get(0, 11).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.fpNeedUpload_Sheet1.Cells.Get(0, 11).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.fpNeedUpload_Sheet1.Cells.Get(0, 11).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.fpNeedUpload_Sheet1.Cells.Get(0, 11).ParseFormatString = "n";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 11).Value = "10000.00";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 12).Value = "符喜春";
            this.fpNeedUpload_Sheet1.Cells.Get(0, 13).Value = "2016-06-26 08:47:16";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "类别";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "病历号";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "证件号码";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "性别";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "结算类型";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "发票电脑号";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "发票印刷号";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "总金额";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "统筹金额";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "自费金额";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "结算员";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "结算日期";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "CLINIC_CODE";
            this.fpNeedUpload_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "PACT_CODE";
            this.fpNeedUpload_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpNeedUpload_Sheet1.Columns.Get(0).Label = "选择";
            this.fpNeedUpload_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpNeedUpload_Sheet1.Columns.Get(0).Width = 30F;
            this.fpNeedUpload_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpNeedUpload_Sheet1.Columns.Get(1).Label = "类别";
            this.fpNeedUpload_Sheet1.Columns.Get(1).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.fpNeedUpload_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(2).Label = "病历号";
            this.fpNeedUpload_Sheet1.Columns.Get(2).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(2).Width = 70F;
            this.fpNeedUpload_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.fpNeedUpload_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(3).Label = "姓名";
            this.fpNeedUpload_Sheet1.Columns.Get(3).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(3).Width = 70F;
            this.fpNeedUpload_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.fpNeedUpload_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(4).Label = "证件号码";
            this.fpNeedUpload_Sheet1.Columns.Get(4).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(4).Width = 120F;
            this.fpNeedUpload_Sheet1.Columns.Get(5).CellType = textCellType5;
            this.fpNeedUpload_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(5).Label = "性别";
            this.fpNeedUpload_Sheet1.Columns.Get(5).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(5).Width = 30F;
            this.fpNeedUpload_Sheet1.Columns.Get(6).CellType = textCellType6;
            this.fpNeedUpload_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(6).Label = "结算类型";
            this.fpNeedUpload_Sheet1.Columns.Get(6).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(6).Width = 70F;
            this.fpNeedUpload_Sheet1.Columns.Get(7).CellType = textCellType7;
            this.fpNeedUpload_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(7).Label = "发票电脑号";
            this.fpNeedUpload_Sheet1.Columns.Get(7).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(7).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(8).CellType = textCellType8;
            this.fpNeedUpload_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(8).Label = "发票印刷号";
            this.fpNeedUpload_Sheet1.Columns.Get(8).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(8).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(9).CellType = textCellType9;
            this.fpNeedUpload_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(9).Label = "总金额";
            this.fpNeedUpload_Sheet1.Columns.Get(9).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(9).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(10).CellType = textCellType10;
            this.fpNeedUpload_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(10).Label = "统筹金额";
            this.fpNeedUpload_Sheet1.Columns.Get(10).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(10).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(11).CellType = textCellType11;
            this.fpNeedUpload_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.Columns.Get(11).Label = "自费金额";
            this.fpNeedUpload_Sheet1.Columns.Get(11).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(11).Width = 90F;
            this.fpNeedUpload_Sheet1.Columns.Get(12).CellType = textCellType12;
            this.fpNeedUpload_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(12).Label = "结算员";
            this.fpNeedUpload_Sheet1.Columns.Get(12).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(12).Width = 70F;
            this.fpNeedUpload_Sheet1.Columns.Get(13).CellType = textCellType13;
            this.fpNeedUpload_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpNeedUpload_Sheet1.Columns.Get(13).Label = "结算日期";
            this.fpNeedUpload_Sheet1.Columns.Get(13).Locked = true;
            this.fpNeedUpload_Sheet1.Columns.Get(13).Width = 130F;
            this.fpNeedUpload_Sheet1.Columns.Get(14).Label = "CLINIC_CODE";
            this.fpNeedUpload_Sheet1.Columns.Get(14).Visible = false;
            this.fpNeedUpload_Sheet1.Columns.Get(15).Label = "PACT_CODE";
            this.fpNeedUpload_Sheet1.Columns.Get(15).Visible = false;
            this.fpNeedUpload_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpNeedUpload_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpNeedUpload_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpNeedUpload_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpNeedUpload_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tpUploaded
            // 
            this.tpUploaded.Location = new System.Drawing.Point(4, 22);
            this.tpUploaded.Name = "tpUploaded";
            this.tpUploaded.Padding = new System.Windows.Forms.Padding(3);
            this.tpUploaded.Size = new System.Drawing.Size(857, 341);
            this.tpUploaded.TabIndex = 1;
            this.tpUploaded.Text = "已上传患者";
            this.tpUploaded.UseVisualStyleBackColor = true;
            // 
            // ucMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.gbTop);
            this.Name = "ucMain";
            this.Size = new System.Drawing.Size(1152, 500);
            this.gbTop.ResumeLayout(false);
            this.gbTop.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tpNeedUpload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpNeedUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpNeedUpload_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTop;
        private FS.FrameWork.WinForms.Controls.NeuTabControl tabMain;
        private System.Windows.Forms.TabPage tpNeedUpload;
        private System.Windows.Forms.TabPage tpUploaded;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtHosInfo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtUserID;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPassWord;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSessionID;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblLoginInfo;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpNeedUpload;
        private FarPoint.Win.Spread.SheetView fpNeedUpload_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEndTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
    }
}
