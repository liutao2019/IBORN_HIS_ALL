namespace FS.HISFC.Components.Account.Controls
{
    partial class ucCardLoss
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.txtIDNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblIDNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbCardType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblCardType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblCardNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPhone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblshow = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread2 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread2_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet2)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIDNO
            // 
            this.txtIDNO.IsEnter2Tab = false;
            this.txtIDNO.Location = new System.Drawing.Point(63, 30);
            this.txtIDNO.Name = "txtIDNO";
            this.txtIDNO.Size = new System.Drawing.Size(191, 21);
            this.txtIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDNO.TabIndex = 20;
            // 
            // lblIDNO
            // 
            this.lblIDNO.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIDNO.Location = new System.Drawing.Point(3, 38);
            this.lblIDNO.Name = "lblIDNO";
            this.lblIDNO.Size = new System.Drawing.Size(73, 13);
            this.lblIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblIDNO.TabIndex = 18;
            this.lblIDNO.Text = "证 件 号:";
            // 
            // cmbCardType
            // 
            this.cmbCardType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.IsEnter2Tab = false;
            this.cmbCardType.IsFlat = false;
            this.cmbCardType.IsLike = true;
            this.cmbCardType.IsListOnly = false;
            this.cmbCardType.IsPopForm = true;
            this.cmbCardType.IsShowCustomerList = false;
            this.cmbCardType.IsShowID = false;
            this.cmbCardType.Location = new System.Drawing.Point(335, 30);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.PopForm = null;
            this.cmbCardType.ShowCustomerList = false;
            this.cmbCardType.ShowID = false;
            this.cmbCardType.Size = new System.Drawing.Size(191, 20);
            this.cmbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCardType.TabIndex = 19;
            this.cmbCardType.Tag = "";
            this.cmbCardType.ToolBarUse = false;
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardType.Location = new System.Drawing.Point(273, 35);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(66, 13);
            this.lblCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCardType.TabIndex = 17;
            this.lblCardType.Text = "证件类型:";
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.Location = new System.Drawing.Point(237, 61);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.PopForm = null;
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(61, 20);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 22;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.ForeColor = System.Drawing.Color.Black;
            this.lblSex.Location = new System.Drawing.Point(170, 63);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(61, 13);
            this.lblSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSex.TabIndex = 21;
            this.lblSex.Text = "性   别:";
            // 
            // txtName
            // 
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(65, 60);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(97, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 25;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(3, 66);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(73, 13);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 23;
            this.lblName.Text = "姓   名:";
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.Location = new System.Drawing.Point(335, 90);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.PopForm = null;
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(191, 20);
            this.cmbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 27;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPact.ForeColor = System.Drawing.Color.Black;
            this.lblPact.Location = new System.Drawing.Point(273, 94);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(66, 13);
            this.lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPact.TabIndex = 26;
            this.lblPact.Text = "结算类别:";
            // 
            // txtCardNo
            // 
            this.txtCardNo.IsEnter2Tab = false;
            this.txtCardNo.Location = new System.Drawing.Point(65, 89);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(189, 21);
            this.txtCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNo.TabIndex = 31;
            // 
            // lblCardNo
            // 
            this.lblCardNo.Font = new System.Drawing.Font("宋体", 9.75F);
            this.lblCardNo.Location = new System.Drawing.Point(3, 93);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(72, 17);
            this.lblCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCardNo.TabIndex = 30;
            this.lblCardNo.Text = "病 历 号:";
            // 
            // txtPhone
            // 
            this.txtPhone.IsEnter2Tab = false;
            this.txtPhone.Location = new System.Drawing.Point(389, 58);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(137, 21);
            this.txtPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPhone.TabIndex = 33;
            // 
            // lblPhone
            // 
            this.lblPhone.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.Location = new System.Drawing.Point(323, 63);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(73, 13);
            this.lblPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPhone.TabIndex = 32;
            this.lblPhone.Text = "电    话:";
            // 
            // lblshow
            // 
            this.lblshow.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblshow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblshow.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblshow.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblshow.ForeColor = System.Drawing.Color.Red;
            this.lblshow.Location = new System.Drawing.Point(0, 0);
            this.lblshow.Name = "lblshow";
            this.lblshow.Size = new System.Drawing.Size(1053, 18);
            this.lblshow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblshow.TabIndex = 34;
            this.lblshow.Text = "姓名、性别、结算类别、证件类型、证件号、病历号、电话为查询用户的组合条件";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, 主显示, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 17);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1047, 179);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 35;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "主显示";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).CellType = textCellType1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "病历号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "就诊卡号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "卡类别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "卡状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "补发标记";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "办卡人";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "办卡时间";
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "病历号";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 159F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "就诊卡号";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 71F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "卡类别";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 69F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "办卡时间";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 91F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "3.0.2004.2005";
            this.neuSpread2.AccessibleDescription = "neuSpread2, 提示, Row 0, Column 0, ";
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread2.FileName = "";
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(3, 17);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread2_Sheet1,
            this.neuSpread2_Sheet2});
            this.neuSpread2.Size = new System.Drawing.Size(1047, 161);
            this.neuSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 36;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance2;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread2_CellClick);
            // 
            // neuSpread2_Sheet1
            // 
            this.neuSpread2_Sheet1.Reset();
            this.neuSpread2_Sheet1.SheetName = "患者信息";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread2_Sheet1.ColumnCount = 9;
            this.neuSpread2_Sheet1.RowCount = 1;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "性别";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "年龄";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "证件类型";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "证件号";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "电话";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "工作单位";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "家庭地址";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "病历号";
            this.neuSpread2_Sheet1.Columns.Get(4).Label = "证件号";
            this.neuSpread2_Sheet1.Columns.Get(4).Width = 131F;
            this.neuSpread2_Sheet1.Columns.Get(6).Label = "工作单位";
            this.neuSpread2_Sheet1.Columns.Get(6).Width = 193F;
            this.neuSpread2_Sheet1.Columns.Get(7).Label = "家庭地址";
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 152F;
            this.neuSpread2_Sheet1.Columns.Get(8).CellType = textCellType3;
            this.neuSpread2_Sheet1.Columns.Get(8).Label = "病历号";
            this.neuSpread2_Sheet1.Columns.Get(8).Width = 131F;
            this.neuSpread2_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSpread2_Sheet2
            // 
            this.neuSpread2_Sheet2.Reset();
            this.neuSpread2_Sheet2.SheetName = "提示";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread2_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread2_Sheet2.ColumnCount = 6;
            this.neuSpread2_Sheet2.RowCount = 1;
            this.neuSpread2_Sheet2.Cells.Get(0, 0).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.neuSpread2_Sheet2.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.neuSpread2_Sheet2.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.neuSpread2_Sheet2.ColumnHeader.Cells.Get(0, 2).Value = "编码";
            this.neuSpread2_Sheet2.ColumnHeader.Cells.Get(0, 3).Value = "名称";
            this.neuSpread2_Sheet2.ColumnHeader.Cells.Get(0, 4).Value = "编码";
            this.neuSpread2_Sheet2.ColumnHeader.Cells.Get(0, 5).Value = "名称";
            this.neuSpread2_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread2_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.txtPhone);
            this.neuPanel1.Controls.Add(this.cmbCardType);
            this.neuPanel1.Controls.Add(this.txtIDNO);
            this.neuPanel1.Controls.Add(this.txtCardNo);
            this.neuPanel1.Controls.Add(this.cmbSex);
            this.neuPanel1.Controls.Add(this.cmbPact);
            this.neuPanel1.Controls.Add(this.txtName);
            this.neuPanel1.Controls.Add(this.lblshow);
            this.neuPanel1.Controls.Add(this.lblIDNO);
            this.neuPanel1.Controls.Add(this.lblCardType);
            this.neuPanel1.Controls.Add(this.lblPhone);
            this.neuPanel1.Controls.Add(this.lblSex);
            this.neuPanel1.Controls.Add(this.lblCardNo);
            this.neuPanel1.Controls.Add(this.lblName);
            this.neuPanel1.Controls.Add(this.lblPact);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1053, 125);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 37;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuSpread2);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 125);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(1053, 181);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 40;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "患者信息";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuSpread1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 306);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(1053, 199);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 41;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "患者卡信息";
            // 
            // ucCardLoss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucCardLoss";
            this.Size = new System.Drawing.Size(1053, 505);
            this.Load += new System.EventHandler(this.ucCardLoss_Load);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet2)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIDNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblIDNO;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbCardType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCardType;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSex;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPact;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCardNo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPhone;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblshow;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FarPoint.Win.Spread.SheetView neuSpread2_Sheet1;
        private FarPoint.Win.Spread.SheetView neuSpread2_Sheet2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;

    }
}

