namespace API.GZSI.UI
{
    partial class ucUploadNucleicBanlance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucUploadNucleicBanlance));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btDelete = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btOutPutExcel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cbCheckAll = new System.Windows.Forms.CheckBox();
            this.btCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btInputLocal = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btUploadBalance = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnInputExcel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.dtEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.neuSpread2 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btDelete);
            this.panel1.Controls.Add(this.btOutPutExcel);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.cbCheckAll);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btInputLocal);
            this.panel1.Controls.Add(this.btUploadBalance);
            this.panel1.Controls.Add(this.btQuery);
            this.panel1.Controls.Add(this.btnInputExcel);
            this.panel1.Controls.Add(this.dtEndTime);
            this.panel1.Controls.Add(this.neuLabel4);
            this.panel1.Controls.Add(this.neuLabel3);
            this.panel1.Controls.Add(this.dtBeginTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1341, 132);
            this.panel1.TabIndex = 0;
            // 
            // btDelete
            // 
            this.btDelete.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDelete.Location = new System.Drawing.Point(16, 50);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(106, 31);
            this.btDelete.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btDelete.TabIndex = 22;
            this.btDelete.Text = "删除";
            this.btDelete.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btOutPutExcel
            // 
            this.btOutPutExcel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOutPutExcel.Location = new System.Drawing.Point(760, 90);
            this.btOutPutExcel.Name = "btOutPutExcel";
            this.btOutPutExcel.Size = new System.Drawing.Size(87, 31);
            this.btOutPutExcel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btOutPutExcel.TabIndex = 21;
            this.btOutPutExcel.Text = "导出";
            this.btOutPutExcel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btOutPutExcel.UseVisualStyleBackColor = true;
            this.btOutPutExcel.Click += new System.EventHandler(this.btOutPutExcel_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.ForeColor = System.Drawing.Color.Red;
            this.richTextBox1.Location = new System.Drawing.Point(326, 7);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(896, 83);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // cbCheckAll
            // 
            this.cbCheckAll.AutoSize = true;
            this.cbCheckAll.Location = new System.Drawing.Point(272, 23);
            this.cbCheckAll.Name = "cbCheckAll";
            this.cbCheckAll.Size = new System.Drawing.Size(48, 16);
            this.cbCheckAll.TabIndex = 19;
            this.cbCheckAll.Text = "全选";
            this.cbCheckAll.UseVisualStyleBackColor = true;
            this.cbCheckAll.CheckedChanged += new System.EventHandler(this.cbCheckAll_CheckedChanged);
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.Location = new System.Drawing.Point(652, 91);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(87, 31);
            this.btCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancel.TabIndex = 18;
            this.btCancel.Text = "撤销";
            this.btCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btInputLocal
            // 
            this.btInputLocal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btInputLocal.Location = new System.Drawing.Point(140, 13);
            this.btInputLocal.Name = "btInputLocal";
            this.btInputLocal.Size = new System.Drawing.Size(106, 31);
            this.btInputLocal.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btInputLocal.TabIndex = 17;
            this.btInputLocal.Text = "本地导入";
            this.btInputLocal.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btInputLocal.UseVisualStyleBackColor = true;
            this.btInputLocal.Click += new System.EventHandler(this.btInputLocal_Click);
            // 
            // btUploadBalance
            // 
            this.btUploadBalance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btUploadBalance.Location = new System.Drawing.Point(546, 91);
            this.btUploadBalance.Name = "btUploadBalance";
            this.btUploadBalance.Size = new System.Drawing.Size(87, 31);
            this.btUploadBalance.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btUploadBalance.TabIndex = 16;
            this.btUploadBalance.Text = "上传";
            this.btUploadBalance.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btUploadBalance.UseVisualStyleBackColor = true;
            this.btUploadBalance.Click += new System.EventHandler(this.btUploadBalance_Click);
            // 
            // btQuery
            // 
            this.btQuery.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btQuery.Location = new System.Drawing.Point(441, 91);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(87, 31);
            this.btQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btQuery.TabIndex = 15;
            this.btQuery.Text = "查询";
            this.btQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // btnInputExcel
            // 
            this.btnInputExcel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInputExcel.Location = new System.Drawing.Point(16, 13);
            this.btnInputExcel.Name = "btnInputExcel";
            this.btnInputExcel.Size = new System.Drawing.Size(106, 31);
            this.btnInputExcel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnInputExcel.TabIndex = 14;
            this.btnInputExcel.Text = "Excel导入";
            this.btnInputExcel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnInputExcel.UseVisualStyleBackColor = true;
            this.btnInputExcel.Click += new System.EventHandler(this.btnInputExcel_Click);
            // 
            // dtEndTime
            // 
            this.dtEndTime.CustomFormat = "yyyy-MM-dd";
            this.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndTime.IsEnter2Tab = false;
            this.dtEndTime.Location = new System.Drawing.Point(316, 96);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(101, 21);
            this.dtEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEndTime.TabIndex = 13;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel4.Location = new System.Drawing.Point(225, 97);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(79, 19);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 12;
            this.neuLabel4.Text = "结束时间：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.Location = new System.Drawing.Point(12, 96);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(79, 19);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 11;
            this.neuLabel3.Text = "开始时间：";
            // 
            // dtBeginTime
            // 
            this.dtBeginTime.CustomFormat = "yyyy-MM-dd";
            this.dtBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBeginTime.IsEnter2Tab = false;
            this.dtBeginTime.Location = new System.Drawing.Point(97, 96);
            this.dtBeginTime.Name = "dtBeginTime";
            this.dtBeginTime.Size = new System.Drawing.Size(106, 21);
            this.dtBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBeginTime.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.tabMain);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 132);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1341, 400);
            this.panel2.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1341, 400);
            this.tabMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.neuSpread1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(1333, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "待结算";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 3);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1327, 368);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 14;
            this.neuSpread1_Sheet1.RowCount = 5;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "编号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "证件类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "证件号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "就诊日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "检测次数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "单价";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "医疗费用总额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "核酸检测患者类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "项目信息";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "患者类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "医师工号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "错误信息";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 33F;
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 41F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "编号";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 73F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "证件类型";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "证件号";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 134F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 72F;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "就诊日期";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 121F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "检测次数";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 83F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "单价";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 86F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "医疗费用总额";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 105F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "核酸检测患者类型";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 105F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "项目信息";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 211F;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "医师工号";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "错误信息";
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 267F;
            textCellType3.WordWrap = true;
            this.neuSpread1_Sheet1.DefaultStyle.CellType = textCellType3;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.neuSpread2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(1350, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "已结算";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "3.0.2004.2005";
            this.neuSpread2.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread2.FileName = "";
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(3, 3);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet2});
            this.neuSpread2.Size = new System.Drawing.Size(1344, 368);
            this.neuSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance2;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet2
            // 
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet2.ColumnCount = 14;
            this.neuSpread1_Sheet2.RowCount = 5;
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 1).Value = "编号";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 2).Value = "证件类型";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 3).Value = "证件号";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 4).Value = "姓名";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 5).Value = "就诊日期";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 6).Value = "检测次数";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 7).Value = "核酸检测患者类型";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 8).Value = "总金额";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 9).Value = "基金支付";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 10).Value = "个人支付";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 11).Value = "就诊ID";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 12).Value = "结算ID";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 13).Value = "错误信息";
            this.neuSpread1_Sheet2.ColumnHeader.Rows.Get(0).Height = 33F;
            this.neuSpread1_Sheet2.Columns.Get(0).CellType = checkBoxCellType2;
            this.neuSpread1_Sheet2.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet2.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet2.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet2.Columns.Get(0).Width = 62F;
            this.neuSpread1_Sheet2.Columns.Get(1).Label = "编号";
            this.neuSpread1_Sheet2.Columns.Get(1).Width = 73F;
            this.neuSpread1_Sheet2.Columns.Get(2).Label = "证件类型";
            this.neuSpread1_Sheet2.Columns.Get(2).Width = 82F;
            this.neuSpread1_Sheet2.Columns.Get(3).Label = "证件号";
            this.neuSpread1_Sheet2.Columns.Get(3).Width = 177F;
            this.neuSpread1_Sheet2.Columns.Get(4).Label = "姓名";
            this.neuSpread1_Sheet2.Columns.Get(4).Width = 158F;
            this.neuSpread1_Sheet2.Columns.Get(5).Label = "就诊日期";
            this.neuSpread1_Sheet2.Columns.Get(5).Width = 163F;
            this.neuSpread1_Sheet2.Columns.Get(6).Label = "检测次数";
            this.neuSpread1_Sheet2.Columns.Get(6).Width = 65F;
            this.neuSpread1_Sheet2.Columns.Get(7).Label = "核酸检测患者类型";
            this.neuSpread1_Sheet2.Columns.Get(7).Width = 132F;
            this.neuSpread1_Sheet2.Columns.Get(8).Label = "总金额";
            this.neuSpread1_Sheet2.Columns.Get(8).Width = 91F;
            this.neuSpread1_Sheet2.Columns.Get(9).Label = "基金支付";
            this.neuSpread1_Sheet2.Columns.Get(9).Width = 91F;
            this.neuSpread1_Sheet2.Columns.Get(10).Label = "个人支付";
            this.neuSpread1_Sheet2.Columns.Get(10).Width = 91F;
            this.neuSpread1_Sheet2.Columns.Get(11).Label = "就诊ID";
            this.neuSpread1_Sheet2.Columns.Get(11).Width = 91F;
            this.neuSpread1_Sheet2.Columns.Get(12).Label = "结算ID";
            this.neuSpread1_Sheet2.Columns.Get(12).Width = 91F;
            this.neuSpread1_Sheet2.Columns.Get(13).Label = "错误信息";
            this.neuSpread1_Sheet2.Columns.Get(13).Width = 154F;
            textCellType4.WordWrap = true;
            this.neuSpread1_Sheet2.DefaultStyle.CellType = textCellType4;
            this.neuSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 532);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1341, 0);
            this.panel3.TabIndex = 2;
            // 
            // ucUploadNucleicBanlance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucUploadNucleicBanlance";
            this.Size = new System.Drawing.Size(1341, 500);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEndTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuButton btnInputExcel;
        private FS.FrameWork.WinForms.Controls.NeuButton btQuery;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet2;
        private FS.FrameWork.WinForms.Controls.NeuButton btUploadBalance;
        private FS.FrameWork.WinForms.Controls.NeuButton btInputLocal;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancel;
        private System.Windows.Forms.CheckBox cbCheckAll;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private FS.FrameWork.WinForms.Controls.NeuButton btOutPutExcel;
        private FS.FrameWork.WinForms.Controls.NeuButton btDelete;
    }
}
