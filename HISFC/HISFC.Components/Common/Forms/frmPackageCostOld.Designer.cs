namespace FS.HISFC.Components.Common.Forms
{
    partial class frmPackageCostOld
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FS.FrameWork.WinForms.Controls.NeuLabel lbCardType;
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.plTop = new System.Windows.Forms.Panel();
            this.cmbLevel = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.tbMedicalNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblMedicalNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblLevel = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPhone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbIDNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbCardType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.pnBottom2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.plBottom1 = new System.Windows.Forms.Panel();
            this.FpCost = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.FpCost_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FpPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.FpPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FpDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.FpDetail_Sheet1 = new FarPoint.Win.Spread.SheetView();
            lbCardType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plTop.SuspendLayout();
            this.pnBottom2.SuspendLayout();
            this.plBottom1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpCost_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpPackage_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpDetail_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCardType
            // 
            lbCardType.AutoSize = true;
            lbCardType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbCardType.ForeColor = System.Drawing.Color.Blue;
            lbCardType.Location = new System.Drawing.Point(549, 15);
            lbCardType.Name = "lbCardType";
            lbCardType.Size = new System.Drawing.Size(75, 14);
            lbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbCardType.TabIndex = 47;
            lbCardType.Text = "证件类型:";
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.cmbLevel);
            this.plTop.Controls.Add(this.tbMedicalNO);
            this.plTop.Controls.Add(this.lblMedicalNO);
            this.plTop.Controls.Add(this.cmbPact);
            this.plTop.Controls.Add(this.lblPact);
            this.plTop.Controls.Add(this.lblLevel);
            this.plTop.Controls.Add(this.tbPhone);
            this.plTop.Controls.Add(this.lblPhone);
            this.plTop.Controls.Add(this.tbIDNO);
            this.plTop.Controls.Add(this.tbAge);
            this.plTop.Controls.Add(this.tbName);
            this.plTop.Controls.Add(this.lbName);
            this.plTop.Controls.Add(this.lbSex);
            this.plTop.Controls.Add(this.cmbSex);
            this.plTop.Controls.Add(this.lbAge);
            this.plTop.Controls.Add(this.lbRegDept);
            this.plTop.Controls.Add(this.cmbCardType);
            this.plTop.Controls.Add(lbCardType);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(1086, 82);
            this.plTop.TabIndex = 0;
            // 
            // cmbLevel
            // 
            this.cmbLevel.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.Enabled = false;
            this.cmbLevel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.IsEnter2Tab = false;
            this.cmbLevel.IsFlat = false;
            this.cmbLevel.IsLike = true;
            this.cmbLevel.IsListOnly = false;
            this.cmbLevel.IsPopForm = true;
            this.cmbLevel.IsShowCustomerList = false;
            this.cmbLevel.IsShowID = false;
            this.cmbLevel.IsShowIDAndName = false;
            this.cmbLevel.Location = new System.Drawing.Point(406, 43);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.ShowCustomerList = false;
            this.cmbLevel.ShowID = false;
            this.cmbLevel.Size = new System.Drawing.Size(128, 22);
            this.cmbLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbLevel.TabIndex = 60;
            this.cmbLevel.Tag = "";
            this.cmbLevel.ToolBarUse = false;
            // 
            // tbMedicalNO
            // 
            this.tbMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMedicalNO.IsEnter2Tab = false;
            this.tbMedicalNO.Location = new System.Drawing.Point(111, 12);
            this.tbMedicalNO.Name = "tbMedicalNO";
            this.tbMedicalNO.Size = new System.Drawing.Size(128, 23);
            this.tbMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbMedicalNO.TabIndex = 44;
            this.tbMedicalNO.Tag = "MEDNO";
            // 
            // lblMedicalNO
            // 
            this.lblMedicalNO.AutoSize = true;
            this.lblMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMedicalNO.ForeColor = System.Drawing.Color.Red;
            this.lblMedicalNO.Location = new System.Drawing.Point(17, 15);
            this.lblMedicalNO.Name = "lblMedicalNO";
            this.lblMedicalNO.Size = new System.Drawing.Size(76, 14);
            this.lblMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMedicalNO.TabIndex = 43;
            this.lblMedicalNO.Text = "病 历 号:";
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPact.Enabled = false;
            this.cmbPact.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.IsShowIDAndName = false;
            this.cmbPact.Location = new System.Drawing.Point(876, 43);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(189, 22);
            this.cmbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 59;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPact.ForeColor = System.Drawing.Color.Blue;
            this.lblPact.Location = new System.Drawing.Point(795, 46);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(75, 14);
            this.lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPact.TabIndex = 58;
            this.lblPact.Text = "合同单位:";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLevel.ForeColor = System.Drawing.Color.Blue;
            this.lblLevel.Location = new System.Drawing.Point(313, 46);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(75, 14);
            this.lblLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLevel.TabIndex = 55;
            this.lblLevel.Text = "会员类型:";
            // 
            // tbPhone
            // 
            this.tbPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhone.IsEnter2Tab = false;
            this.tbPhone.Location = new System.Drawing.Point(643, 43);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.ReadOnly = true;
            this.tbPhone.Size = new System.Drawing.Size(127, 23);
            this.tbPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPhone.TabIndex = 57;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.ForeColor = System.Drawing.Color.Blue;
            this.lblPhone.Location = new System.Drawing.Point(551, 46);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(75, 14);
            this.lblPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPhone.TabIndex = 56;
            this.lblPhone.Text = "联系电话:";
            // 
            // tbIDNO
            // 
            this.tbIDNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbIDNO.IsEnter2Tab = false;
            this.tbIDNO.Location = new System.Drawing.Point(876, 12);
            this.tbIDNO.Name = "tbIDNO";
            this.tbIDNO.ReadOnly = true;
            this.tbIDNO.Size = new System.Drawing.Size(189, 23);
            this.tbIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbIDNO.TabIndex = 50;
            // 
            // tbAge
            // 
            this.tbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.IsEnter2Tab = false;
            this.tbAge.Location = new System.Drawing.Point(235, 43);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(66, 23);
            this.tbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbAge.TabIndex = 54;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(345, 12);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(189, 23);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 46;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.Blue;
            this.lbName.Location = new System.Drawing.Point(258, 15);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(77, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 45;
            this.lbName.Text = "姓    名:";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.ForeColor = System.Drawing.Color.Blue;
            this.lbSex.Location = new System.Drawing.Point(16, 46);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(77, 14);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 51;
            this.lbSex.Text = "性    别:";
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSex.Enabled = false;
            this.cmbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.IsShowIDAndName = false;
            this.cmbSex.Location = new System.Drawing.Point(111, 43);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(62, 22);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 52;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.ForeColor = System.Drawing.Color.Blue;
            this.lbAge.Location = new System.Drawing.Point(180, 46);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(45, 14);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 53;
            this.lbAge.Text = "年龄:";
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.Color.Blue;
            this.lbRegDept.Location = new System.Drawing.Point(795, 15);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(75, 14);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 49;
            this.lbRegDept.Text = "证件号码:";
            // 
            // cmbCardType
            // 
            this.cmbCardType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.Enabled = false;
            this.cmbCardType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.IsEnter2Tab = false;
            this.cmbCardType.IsFlat = false;
            this.cmbCardType.IsLike = true;
            this.cmbCardType.IsListOnly = false;
            this.cmbCardType.IsPopForm = true;
            this.cmbCardType.IsShowCustomerList = false;
            this.cmbCardType.IsShowID = false;
            this.cmbCardType.IsShowIDAndName = false;
            this.cmbCardType.Location = new System.Drawing.Point(643, 12);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.ShowCustomerList = false;
            this.cmbCardType.ShowID = false;
            this.cmbCardType.Size = new System.Drawing.Size(127, 22);
            this.cmbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCardType.TabIndex = 48;
            this.cmbCardType.Tag = "";
            this.cmbCardType.ToolBarUse = false;
            // 
            // pnBottom2
            // 
            this.pnBottom2.Controls.Add(this.button1);
            this.pnBottom2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom2.Location = new System.Drawing.Point(0, 483);
            this.pnBottom2.Name = "pnBottom2";
            this.pnBottom2.Size = new System.Drawing.Size(1086, 46);
            this.pnBottom2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(966, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // plBottom1
            // 
            this.plBottom1.Controls.Add(this.FpCost);
            this.plBottom1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom1.Location = new System.Drawing.Point(0, 373);
            this.plBottom1.Name = "plBottom1";
            this.plBottom1.Size = new System.Drawing.Size(1086, 110);
            this.plBottom1.TabIndex = 2;
            // 
            // FpCost
            // 
            this.FpCost.About = "3.0.2004.2005";
            this.FpCost.AccessibleDescription = "FpCost, Sheet1, Row 0, Column 0, ";
            this.FpCost.BackColor = System.Drawing.Color.White;
            this.FpCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpCost.FileName = "";
            this.FpCost.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpCost.IsAutoSaveGridStatus = false;
            this.FpCost.IsCanCustomConfigColumn = false;
            this.FpCost.Location = new System.Drawing.Point(0, 0);
            this.FpCost.Name = "FpCost";
            this.FpCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FpCost.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpCost_Sheet1});
            this.FpCost.Size = new System.Drawing.Size(1086, 110);
            this.FpCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.FpCost.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FpCost.TextTipAppearance = tipAppearance1;
            this.FpCost.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // FpCost_Sheet1
            // 
            this.FpCost_Sheet1.Reset();
            this.FpCost_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpCost_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpCost_Sheet1.ColumnCount = 8;
            this.FpCost_Sheet1.RowCount = 2;
            this.FpCost_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目名称";
            this.FpCost_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "规格";
            this.FpCost_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "开立数量";
            this.FpCost_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "开立单位";
            this.FpCost_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "总金额";
            this.FpCost_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "实收金额";
            this.FpCost_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "赠送金额";
            this.FpCost_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "优惠金额";
            this.FpCost_Sheet1.Columns.Get(0).Label = "项目名称";
            this.FpCost_Sheet1.Columns.Get(0).Width = 200F;
            this.FpCost_Sheet1.Columns.Get(1).Label = "规格";
            this.FpCost_Sheet1.Columns.Get(1).Width = 100F;
            this.FpCost_Sheet1.Columns.Get(4).Label = "总金额";
            this.FpCost_Sheet1.Columns.Get(4).Width = 70F;
            this.FpCost_Sheet1.Columns.Get(5).Label = "实收金额";
            this.FpCost_Sheet1.Columns.Get(5).Width = 80F;
            this.FpCost_Sheet1.Columns.Get(6).Label = "赠送金额";
            this.FpCost_Sheet1.Columns.Get(6).Width = 80F;
            this.FpCost_Sheet1.Columns.Get(7).Label = "优惠金额";
            this.FpCost_Sheet1.Columns.Get(7).Width = 80F;
            this.FpCost_Sheet1.DefaultStyle.Locked = true;
            this.FpCost_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.FpCost_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.FpCost_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.FpCost_Sheet1.RowHeader.Columns.Get(0).Width = 28F;
            this.FpCost_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.FpCost_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.FpCost_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.FpCost_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.FpCost_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FpPackage
            // 
            this.FpPackage.About = "3.0.2004.2005";
            this.FpPackage.AccessibleDescription = "FpPackage, Sheet1, Row 0, Column 0, ";
            this.FpPackage.BackColor = System.Drawing.Color.White;
            this.FpPackage.Dock = System.Windows.Forms.DockStyle.Left;
            this.FpPackage.FileName = "";
            this.FpPackage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpPackage.IsAutoSaveGridStatus = false;
            this.FpPackage.IsCanCustomConfigColumn = false;
            this.FpPackage.Location = new System.Drawing.Point(0, 82);
            this.FpPackage.Name = "FpPackage";
            this.FpPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FpPackage.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.FpPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpPackage_Sheet1});
            this.FpPackage.Size = new System.Drawing.Size(427, 291);
            this.FpPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.FpPackage.TabIndex = 3;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FpPackage.TextTipAppearance = tipAppearance2;
            this.FpPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // FpPackage_Sheet1
            // 
            this.FpPackage_Sheet1.Reset();
            this.FpPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpPackage_Sheet1.ColumnCount = 2;
            this.FpPackage_Sheet1.RowCount = 2;
            this.FpPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "套餐名称";
            this.FpPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "购买时间";
            this.FpPackage_Sheet1.Columns.Get(0).Label = "套餐名称";
            this.FpPackage_Sheet1.Columns.Get(0).Width = 219F;
            this.FpPackage_Sheet1.Columns.Get(1).Label = "购买时间";
            this.FpPackage_Sheet1.Columns.Get(1).Width = 160F;
            this.FpPackage_Sheet1.DefaultStyle.Locked = true;
            this.FpPackage_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.FpPackage_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.FpPackage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpPackage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.FpPackage_Sheet1.RowHeader.Columns.Get(0).Width = 27F;
            this.FpPackage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.FpPackage_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.FpPackage_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.FpPackage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.FpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FpDetail
            // 
            this.FpDetail.About = "3.0.2004.2005";
            this.FpDetail.AccessibleDescription = "FpDetail, Sheet1, Row 0, Column 0, ";
            this.FpDetail.BackColor = System.Drawing.Color.White;
            this.FpDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpDetail.EditModePermanent = true;
            this.FpDetail.EditModeReplace = true;
            this.FpDetail.FileName = "";
            this.FpDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpDetail.IsAutoSaveGridStatus = false;
            this.FpDetail.IsCanCustomConfigColumn = false;
            this.FpDetail.Location = new System.Drawing.Point(427, 82);
            this.FpDetail.Name = "FpDetail";
            this.FpDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FpDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpDetail_Sheet1});
            this.FpDetail.Size = new System.Drawing.Size(659, 291);
            this.FpDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.FpDetail.TabIndex = 4;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FpDetail.TextTipAppearance = tipAppearance3;
            this.FpDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // FpDetail_Sheet1
            // 
            this.FpDetail_Sheet1.Reset();
            this.FpDetail_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpDetail_Sheet1.ColumnCount = 11;
            this.FpDetail_Sheet1.RowCount = 2;
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目名称";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目规格";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "开立数量";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单位";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "总数量";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "可用数量";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "已开数量";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "总金额";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "实收金额";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "赠送金额";
            this.FpDetail_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "优惠金额";
            this.FpDetail_Sheet1.Columns.Get(0).Label = "项目名称";
            this.FpDetail_Sheet1.Columns.Get(0).Width = 200F;
            this.FpDetail_Sheet1.Columns.Get(1).Label = "项目规格";
            this.FpDetail_Sheet1.Columns.Get(1).Width = 100F;
            numberCellType1.DecimalPlaces = 0;
            this.FpDetail_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.FpDetail_Sheet1.Columns.Get(2).Label = "开立数量";
            this.FpDetail_Sheet1.Columns.Get(6).Label = "已开数量";
            this.FpDetail_Sheet1.Columns.Get(6).Width = 68F;
            this.FpDetail_Sheet1.Columns.Get(7).Label = "总金额";
            this.FpDetail_Sheet1.Columns.Get(7).Width = 70F;
            this.FpDetail_Sheet1.Columns.Get(8).Label = "实收金额";
            this.FpDetail_Sheet1.Columns.Get(8).Width = 70F;
            this.FpDetail_Sheet1.Columns.Get(9).Label = "赠送金额";
            this.FpDetail_Sheet1.Columns.Get(9).Width = 70F;
            this.FpDetail_Sheet1.Columns.Get(10).Label = "优惠金额";
            this.FpDetail_Sheet1.Columns.Get(10).Width = 70F;
            this.FpDetail_Sheet1.DefaultStyle.Locked = true;
            this.FpDetail_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.FpDetail_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.FpDetail_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpDetail_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.FpDetail_Sheet1.RowHeader.Columns.Get(0).Width = 30F;
            this.FpDetail_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.FpDetail_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.FpDetail_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.FpDetail_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.FpDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // frmPackageCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 529);
            this.Controls.Add(this.FpDetail);
            this.Controls.Add(this.FpPackage);
            this.Controls.Add(this.plBottom1);
            this.Controls.Add(this.pnBottom2);
            this.Controls.Add(this.plTop);
            this.Name = "frmPackageCost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "套餐查询与消费";
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.pnBottom2.ResumeLayout(false);
            this.plBottom1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FpCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpCost_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpPackage_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpDetail_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plTop;
        private System.Windows.Forms.Panel pnBottom2;
        private System.Windows.Forms.Panel plBottom1;
        private FS.FrameWork.WinForms.Controls.NeuSpread FpCost;
        private FarPoint.Win.Spread.SheetView FpCost_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread FpPackage;
        private FarPoint.Win.Spread.SheetView FpPackage_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread FpDetail;
        private FarPoint.Win.Spread.SheetView FpDetail_Sheet1;
        private System.Windows.Forms.Button button1;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbLevel;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblLevel;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbIDNO;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbAge;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbCardType;


    }
}