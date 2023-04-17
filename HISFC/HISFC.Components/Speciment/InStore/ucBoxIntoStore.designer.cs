namespace FS.HISFC.Components.Speciment.InStore
{
    partial class ucBoxIntoStore
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
            this.gbQuery = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.chkIsOccupy = new System.Windows.Forms.CheckBox();
            this.txtBoxCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblBoxCode = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrgType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblOrgOrBlood = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbBoxSpec = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblBoxSpec = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSpecType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDiseaseType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblDiseaseType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblSpecType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbResult = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.spreadSpecBoxInfo = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.spreadSpecBoxInfo_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.nudCopy = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tSpecId = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.gbQuery.SuspendLayout();
            this.gbResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSpecBoxInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSpecBoxInfo_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopy)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbQuery
            // 
            this.gbQuery.Controls.Add(this.tSpecId);
            this.gbQuery.Controls.Add(this.neuLabel1);
            this.gbQuery.Controls.Add(this.chkIsOccupy);
            this.gbQuery.Controls.Add(this.txtBoxCode);
            this.gbQuery.Controls.Add(this.lblBoxCode);
            this.gbQuery.Controls.Add(this.cmbOrgType);
            this.gbQuery.Controls.Add(this.lblOrgOrBlood);
            this.gbQuery.Controls.Add(this.cmbBoxSpec);
            this.gbQuery.Controls.Add(this.lblBoxSpec);
            this.gbQuery.Controls.Add(this.cmbSpecType);
            this.gbQuery.Controls.Add(this.cmbDiseaseType);
            this.gbQuery.Controls.Add(this.lblDiseaseType);
            this.gbQuery.Controls.Add(this.lblSpecType);
            this.gbQuery.Controls.Add(this.neuLabel2);
            this.gbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbQuery.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbQuery.Location = new System.Drawing.Point(0, 0);
            this.gbQuery.Margin = new System.Windows.Forms.Padding(5);
            this.gbQuery.Name = "gbQuery";
            this.gbQuery.Padding = new System.Windows.Forms.Padding(5);
            this.gbQuery.Size = new System.Drawing.Size(1280, 58);
            this.gbQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbQuery.TabIndex = 6;
            this.gbQuery.TabStop = false;
            this.gbQuery.Text = "标本盒查找";
            // 
            // chkIsOccupy
            // 
            this.chkIsOccupy.AutoSize = true;
            this.chkIsOccupy.Location = new System.Drawing.Point(649, 70);
            this.chkIsOccupy.Margin = new System.Windows.Forms.Padding(5);
            this.chkIsOccupy.Name = "chkIsOccupy";
            this.chkIsOccupy.Size = new System.Drawing.Size(59, 20);
            this.chkIsOccupy.TabIndex = 53;
            this.chkIsOccupy.Text = "满盒";
            this.chkIsOccupy.UseVisualStyleBackColor = true;
            this.chkIsOccupy.Visible = false;
            // 
            // txtBoxCode
            // 
            this.txtBoxCode.Location = new System.Drawing.Point(959, 23);
            this.txtBoxCode.Margin = new System.Windows.Forms.Padding(5);
            this.txtBoxCode.Name = "txtBoxCode";
            this.txtBoxCode.Size = new System.Drawing.Size(133, 26);
            this.txtBoxCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBoxCode.TabIndex = 52;
            // 
            // lblBoxCode
            // 
            this.lblBoxCode.AutoSize = true;
            this.lblBoxCode.Location = new System.Drawing.Point(867, 28);
            this.lblBoxCode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBoxCode.Name = "lblBoxCode";
            this.lblBoxCode.Size = new System.Drawing.Size(72, 16);
            this.lblBoxCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBoxCode.TabIndex = 51;
            this.lblBoxCode.Text = "条形码：";
            // 
            // cmbOrgType
            // 
            //this.cmbOrgType.A = false;
            this.cmbOrgType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOrgType.FormattingEnabled = true;
            this.cmbOrgType.IsFlat = true;
            this.cmbOrgType.IsLike = true;
            this.cmbOrgType.Location = new System.Drawing.Point(493, 24);
            this.cmbOrgType.Margin = new System.Windows.Forms.Padding(5);
            this.cmbOrgType.Name = "cmbOrgType";
            this.cmbOrgType.PopForm = null;
            this.cmbOrgType.ShowCustomerList = false;
            this.cmbOrgType.ShowID = false;
            this.cmbOrgType.Size = new System.Drawing.Size(107, 24);
            this.cmbOrgType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbOrgType.TabIndex = 50;
            this.cmbOrgType.Tag = "";
            this.cmbOrgType.ToolBarUse = false;
            this.cmbOrgType.SelectedIndexChanged += new System.EventHandler(this.cmbSpecType_SelectedIndexChanged);
            // 
            // lblOrgOrBlood
            // 
            this.lblOrgOrBlood.AutoSize = true;
            this.lblOrgOrBlood.Location = new System.Drawing.Point(395, 28);
            this.lblOrgOrBlood.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblOrgOrBlood.Name = "lblOrgOrBlood";
            this.lblOrgOrBlood.Size = new System.Drawing.Size(88, 16);
            this.lblOrgOrBlood.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOrgOrBlood.TabIndex = 49;
            this.lblOrgOrBlood.Text = "标本种类：";
            // 
            // cmbBoxSpec
            // 
            //this.cmbBoxSpec.A = false;
            this.cmbBoxSpec.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbBoxSpec.FormattingEnabled = true;
            this.cmbBoxSpec.IsFlat = true;
            this.cmbBoxSpec.IsLike = true;
            this.cmbBoxSpec.Location = new System.Drawing.Point(797, 68);
            this.cmbBoxSpec.Margin = new System.Windows.Forms.Padding(5);
            this.cmbBoxSpec.Name = "cmbBoxSpec";
            this.cmbBoxSpec.PopForm = null;
            this.cmbBoxSpec.ShowCustomerList = false;
            this.cmbBoxSpec.ShowID = false;
            this.cmbBoxSpec.Size = new System.Drawing.Size(199, 24);
            this.cmbBoxSpec.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbBoxSpec.TabIndex = 48;
            this.cmbBoxSpec.Tag = "";
            this.cmbBoxSpec.ToolBarUse = false;
            this.cmbBoxSpec.Visible = false;
            // 
            // lblBoxSpec
            // 
            this.lblBoxSpec.AutoSize = true;
            this.lblBoxSpec.Location = new System.Drawing.Point(718, 74);
            this.lblBoxSpec.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBoxSpec.Name = "lblBoxSpec";
            this.lblBoxSpec.Size = new System.Drawing.Size(56, 16);
            this.lblBoxSpec.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBoxSpec.TabIndex = 47;
            this.lblBoxSpec.Text = "规格：";
            this.lblBoxSpec.Visible = false;
            // 
            // cmbSpecType
            // 
            //this.cmbSpecType.A = false;
            this.cmbSpecType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.IsFlat = true;
            this.cmbSpecType.IsLike = true;
            this.cmbSpecType.Location = new System.Drawing.Point(735, 24);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(5);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.PopForm = null;
            this.cmbSpecType.ShowCustomerList = false;
            this.cmbSpecType.ShowID = false;
            this.cmbSpecType.Size = new System.Drawing.Size(110, 24);
            this.cmbSpecType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbSpecType.TabIndex = 46;
            this.cmbSpecType.Tag = "";
            this.cmbSpecType.ToolBarUse = false;
            // 
            // cmbDiseaseType
            // 
            //this.cmbDiseaseType.A = false;
            this.cmbDiseaseType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDiseaseType.FormattingEnabled = true;
            this.cmbDiseaseType.IsFlat = true;
            this.cmbDiseaseType.IsLike = true;
            this.cmbDiseaseType.Location = new System.Drawing.Point(63, 24);
            this.cmbDiseaseType.Margin = new System.Windows.Forms.Padding(5);
            this.cmbDiseaseType.Name = "cmbDiseaseType";
            this.cmbDiseaseType.PopForm = null;
            this.cmbDiseaseType.ShowCustomerList = false;
            this.cmbDiseaseType.ShowID = false;
            this.cmbDiseaseType.Size = new System.Drawing.Size(118, 24);
            this.cmbDiseaseType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDiseaseType.TabIndex = 45;
            this.cmbDiseaseType.Tag = "";
            this.cmbDiseaseType.ToolBarUse = false;
            // 
            // lblDiseaseType
            // 
            this.lblDiseaseType.AutoSize = true;
            this.lblDiseaseType.Location = new System.Drawing.Point(10, 28);
            this.lblDiseaseType.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDiseaseType.Name = "lblDiseaseType";
            this.lblDiseaseType.Size = new System.Drawing.Size(56, 16);
            this.lblDiseaseType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDiseaseType.TabIndex = 42;
            this.lblDiseaseType.Text = "病种：";
            // 
            // lblSpecType
            // 
            this.lblSpecType.AutoSize = true;
            this.lblSpecType.Location = new System.Drawing.Point(637, 28);
            this.lblSpecType.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSpecType.Name = "lblSpecType";
            this.lblSpecType.Size = new System.Drawing.Size(88, 16);
            this.lblSpecType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSpecType.TabIndex = 41;
            this.lblSpecType.Text = "标本类型：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(162, 41);
            this.neuLabel2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(0, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 7;
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.spreadSpecBoxInfo);
            this.gbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbResult.Location = new System.Drawing.Point(0, 0);
            this.gbResult.Margin = new System.Windows.Forms.Padding(5);
            this.gbResult.Name = "gbResult";
            this.gbResult.Padding = new System.Windows.Forms.Padding(5);
            this.gbResult.Size = new System.Drawing.Size(1280, 801);
            this.gbResult.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbResult.TabIndex = 8;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "查询结果";
            // 
            // spreadSpecBoxInfo
            // 
            this.spreadSpecBoxInfo.About = "2.5.2007.2005";
            this.spreadSpecBoxInfo.AccessibleDescription = "spreadSpecBoxInfo, Sheet1, Row 0, Column 0, ";
            this.spreadSpecBoxInfo.BackColor = System.Drawing.Color.White;
            this.spreadSpecBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadSpecBoxInfo.FileName = "";
            this.spreadSpecBoxInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.spreadSpecBoxInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spreadSpecBoxInfo.IsAutoSaveGridStatus = false;
            this.spreadSpecBoxInfo.IsCanCustomConfigColumn = false;
            this.spreadSpecBoxInfo.Location = new System.Drawing.Point(5, 24);
            this.spreadSpecBoxInfo.Margin = new System.Windows.Forms.Padding(5);
            this.spreadSpecBoxInfo.Name = "spreadSpecBoxInfo";
            this.spreadSpecBoxInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spreadSpecBoxInfo_Sheet1});
            this.spreadSpecBoxInfo.Size = new System.Drawing.Size(1270, 772);
            this.spreadSpecBoxInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.spreadSpecBoxInfo.TabIndex = 9;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.spreadSpecBoxInfo.TextTipAppearance = tipAppearance1;
            this.spreadSpecBoxInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // spreadSpecBoxInfo_Sheet1
            // 
            this.spreadSpecBoxInfo_Sheet1.Reset();
            this.spreadSpecBoxInfo_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spreadSpecBoxInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spreadSpecBoxInfo_Sheet1.ColumnCount = 12;
            this.spreadSpecBoxInfo_Sheet1.RowCount = 0;
            this.spreadSpecBoxInfo_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "打印数";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "条形码";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "起始标本号";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "所在冰箱";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "规格";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "容量";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "已放标本量";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "占用率（%）";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "标本种类";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "标本类型";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "病种";
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.spreadSpecBoxInfo_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            checkBoxCellType1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            checkBoxCellType1.TextAlign = FarPoint.Win.ButtonTextAlign.TextTopPictBottom;
            checkBoxCellType1.ThreeState = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(0).Label = "选择";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(0).Width = 59F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(2).AllowAutoFilter = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(2).Label = "条形码";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(2).Width = 127F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(3).AllowAutoFilter = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(3).Label = "起始标本号";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(3).Width = 132F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(4).AllowAutoFilter = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(4).CellType = textCellType2;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(4).Label = "所在冰箱";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(4).Visible = false;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(4).Width = 122F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(5).CellType = textCellType3;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(5).Label = "规格";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(5).Visible = false;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(5).Width = 58F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(6).CellType = textCellType4;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(6).Label = "容量";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(6).Visible = false;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(6).Width = 56F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(7).CellType = textCellType5;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(7).Label = "已放标本量";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(7).Width = 101F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(8).AllowAutoFilter = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(8).AllowAutoSort = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(8).CellType = textCellType6;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(8).Label = "占用率（%）";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(8).Width = 137F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(9).AllowAutoFilter = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(9).CellType = textCellType7;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(9).Label = "标本种类";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(9).Width = 117F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(10).AllowAutoFilter = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(10).AllowAutoSort = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(10).CellType = textCellType8;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(10).Label = "标本类型";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(10).Width = 116F;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(11).AllowAutoFilter = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(11).AllowAutoSort = true;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(11).CellType = textCellType9;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(11).Label = "病种";
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spreadSpecBoxInfo_Sheet1.Columns.Get(11).Width = 88F;
            this.spreadSpecBoxInfo_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.spreadSpecBoxInfo_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.spreadSpecBoxInfo_Sheet1.DefaultStyle.Locked = false;
            this.spreadSpecBoxInfo_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.spreadSpecBoxInfo_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.spreadSpecBoxInfo_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.spreadSpecBoxInfo_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.spreadSpecBoxInfo_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.spreadSpecBoxInfo_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.spreadSpecBoxInfo_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.spreadSpecBoxInfo_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.spreadSpecBoxInfo_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.spreadSpecBoxInfo_Sheet1.SheetCornerStyle.Locked = false;
            this.spreadSpecBoxInfo_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.spreadSpecBoxInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spreadSpecBoxInfo.SetActiveViewport(1, 0);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbQuery);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 58);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.nudCopy);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1280, 41);
            this.panel2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "打印份数:";
            // 
            // nudCopy
            // 
            this.nudCopy.Location = new System.Drawing.Point(122, 6);
            this.nudCopy.Name = "nudCopy";
            this.nudCopy.Size = new System.Drawing.Size(62, 29);
            this.nudCopy.TabIndex = 0;
            this.nudCopy.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudCopy.ValueChanged += new System.EventHandler(this.nudCopy_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbResult);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 99);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1280, 801);
            this.panel3.TabIndex = 11;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(203, 28);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(72, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 54;
            this.neuLabel1.Text = "标本号：";
            // 
            // tSpecId
            // 
            this.tSpecId.Location = new System.Drawing.Point(285, 23);
            this.tSpecId.Margin = new System.Windows.Forms.Padding(5);
            this.tSpecId.Name = "tSpecId";
            this.tSpecId.Size = new System.Drawing.Size(99, 26);
            this.tSpecId.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tSpecId.TabIndex = 55;
            // 
            // ucBoxIntoStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ucBoxIntoStore";
            this.Size = new System.Drawing.Size(1280, 900);
            this.Load += new System.EventHandler(this.ucBoxIntoStore_Load);
            this.gbQuery.ResumeLayout(false);
            this.gbQuery.PerformLayout();
            this.gbResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSpecBoxInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSpecBoxInfo_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopy)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbQuery;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDiseaseType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSpecType;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiseaseType;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbBoxSpec;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBoxSpec;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOrgType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblOrgOrBlood;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBoxCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBoxCode;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbResult;
        private System.Windows.Forms.CheckBox chkIsOccupy;
        private FS.FrameWork.WinForms.Controls.NeuSpread spreadSpecBoxInfo;
        private FarPoint.Win.Spread.SheetView spreadSpecBoxInfo_Sheet1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudCopy;
        private System.Windows.Forms.Panel panel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tSpecId;
       // private FS.FrameWork.WinForms.Controls.NeuSpread spreadSpecBoxInfo;

    }
}
