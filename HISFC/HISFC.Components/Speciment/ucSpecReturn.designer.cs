namespace FS.HISFC.Components.Speciment
{
    partial class ucSpecReturn
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType21 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.grpSpecInfo = new System.Windows.Forms.GroupBox();
            this.nudCapacity = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.chkChangeType = new System.Windows.Forms.CheckBox();
            this.chkGenBarCode = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.btnIn = new System.Windows.Forms.Button();
            this.chkKeepOld = new System.Windows.Forms.CheckBox();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.cmbOrgType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpSpecInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSpecInfo
            // 
            this.grpSpecInfo.Controls.Add(this.splitContainer1);
            this.grpSpecInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSpecInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpSpecInfo.Location = new System.Drawing.Point(0, 0);
            this.grpSpecInfo.Name = "grpSpecInfo";
            this.grpSpecInfo.Size = new System.Drawing.Size(1200, 730);
            this.grpSpecInfo.TabIndex = 4;
            this.grpSpecInfo.TabStop = false;
            this.grpSpecInfo.Text = "标本信息";
            // 
            // nudCapacity
            // 
            this.nudCapacity.DecimalPlaces = 2;
            this.nudCapacity.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudCapacity.Location = new System.Drawing.Point(627, 8);
            this.nudCapacity.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCapacity.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudCapacity.Name = "nudCapacity";
            this.nudCapacity.Size = new System.Drawing.Size(60, 23);
            this.nudCapacity.TabIndex = 17;
            this.nudCapacity.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nudCapacity.ValueChanged += new System.EventHandler(this.nudCapacity_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(581, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 16;
            this.label3.Text = "容量:";
            // 
            // nudCount
            // 
            this.nudCount.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudCount.Location = new System.Drawing.Point(536, 8);
            this.nudCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(36, 23);
            this.nudCount.TabIndex = 15;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCount.ValueChanged += new System.EventHandler(this.nudCount_ValueChanged);
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1194, 663);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 14;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.neuSpread1_EditChange);
            this.neuSpread1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.neuSpread1_KeyUp);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 17;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.AutoGenerateColumns = false;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "条形码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "标本号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "标本组织类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "标本类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "病种类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "位置";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "变更类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "变更条码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "变更位置";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "借出次数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "上次返回时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "存放时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "N13";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "O14";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "变更数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "变更容量";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType2;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 36F;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType12;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "条形码";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 95F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType13;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "标本号";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 82F;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType14;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "标本组织类型";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 95F;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType15;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "标本类型";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 76F;
            this.neuSpread1_Sheet1.Columns.Get(5).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType16;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "病种类型";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 93F;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType17;
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "位置";
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 95F;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType18;
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "变更类型";
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "变更条码";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 95F;
            this.neuSpread1_Sheet1.Columns.Get(9).CellType = textCellType19;
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "变更位置";
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(10).CellType = textCellType20;
            this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "借出次数";
            this.neuSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 64F;
            this.neuSpread1_Sheet1.Columns.Get(11).CellType = textCellType21;
            this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "上次返回时间";
            this.neuSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 89F;
            this.neuSpread1_Sheet1.Columns.Get(12).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(12).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(12).CellType = textCellType22;
            this.neuSpread1_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "存放时间";
            this.neuSpread1_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 90F;
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "N13";
            this.neuSpread1_Sheet1.Columns.Get(13).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(14).Label = "O14";
            this.neuSpread1_Sheet1.Columns.Get(14).Visible = false;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MaximumValue = 10000000;
            numberCellType3.MinimumValue = 1;
            this.neuSpread1_Sheet1.Columns.Get(15).CellType = numberCellType3;
            this.neuSpread1_Sheet1.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(15).Label = "变更数量";
            this.neuSpread1_Sheet1.Columns.Get(15).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(15).Width = 62F;
            numberCellType4.DecimalPlaces = 2;
            numberCellType4.MaximumValue = 10;
            numberCellType4.MinimumValue = 0.25;
            this.neuSpread1_Sheet1.Columns.Get(16).CellType = numberCellType4;
            this.neuSpread1_Sheet1.Columns.Get(16).Label = "变更容量";
            this.neuSpread1_Sheet1.Columns.Get(16).Width = 64F;
            this.neuSpread1_Sheet1.DataAutoCellTypes = false;
            this.neuSpread1_Sheet1.DataAutoHeadings = false;
            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Location = new System.Drawing.Point(997, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(76, 27);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "打印条码";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // chkChangeType
            // 
            this.chkChangeType.AutoSize = true;
            this.chkChangeType.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkChangeType.Location = new System.Drawing.Point(80, 11);
            this.chkChangeType.Name = "chkChangeType";
            this.chkChangeType.Size = new System.Drawing.Size(82, 18);
            this.chkChangeType.TabIndex = 12;
            this.chkChangeType.Text = "变更类型";
            this.chkChangeType.UseVisualStyleBackColor = true;
            this.chkChangeType.CheckedChanged += new System.EventHandler(this.chkChangeType_CheckedChanged);
            // 
            // chkGenBarCode
            // 
            this.chkGenBarCode.AutoSize = true;
            this.chkGenBarCode.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkGenBarCode.Location = new System.Drawing.Point(822, 11);
            this.chkGenBarCode.Name = "chkGenBarCode";
            this.chkGenBarCode.Size = new System.Drawing.Size(110, 18);
            this.chkGenBarCode.TabIndex = 11;
            this.chkGenBarCode.Text = "自动生成条码";
            this.chkGenBarCode.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAll.Location = new System.Drawing.Point(12, 10);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(54, 18);
            this.chkAll.TabIndex = 10;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // btnIn
            // 
            this.btnIn.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnIn.Location = new System.Drawing.Point(938, 5);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(54, 25);
            this.btnIn.TabIndex = 9;
            this.btnIn.Text = "入库";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // chkKeepOld
            // 
            this.chkKeepOld.AutoSize = true;
            this.chkKeepOld.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkKeepOld.Location = new System.Drawing.Point(691, 11);
            this.chkKeepOld.Name = "chkKeepOld";
            this.chkKeepOld.Size = new System.Drawing.Size(124, 18);
            this.chkKeepOld.TabIndex = 6;
            this.chkKeepOld.Text = "保留变更前标本";
            this.chkKeepOld.UseVisualStyleBackColor = true;
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.Enabled = false;
            this.cmbSpecType.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(355, 9);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(121, 21);
            this.cmbSpecType.TabIndex = 5;
            this.cmbSpecType.SelectedIndexChanged += new System.EventHandler(this.cmbSpecType_TextChanged);
            // 
            // cmbOrgType
            // 
            this.cmbOrgType.Enabled = false;
            this.cmbOrgType.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbOrgType.FormattingEnabled = true;
            this.cmbOrgType.Location = new System.Drawing.Point(266, 9);
            this.cmbOrgType.Name = "cmbOrgType";
            this.cmbOrgType.Size = new System.Drawing.Size(83, 21);
            this.cmbOrgType.TabIndex = 4;
            this.cmbOrgType.SelectedIndexChanged += new System.EventHandler(this.orgTypeSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(482, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "数量:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(177, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "更改类型:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 22);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkAll);
            this.splitContainer1.Panel1.Controls.Add(this.nudCapacity);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.nudCount);
            this.splitContainer1.Panel1.Controls.Add(this.cmbOrgType);
            this.splitContainer1.Panel1.Controls.Add(this.cmbSpecType);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel1.Controls.Add(this.chkKeepOld);
            this.splitContainer1.Panel1.Controls.Add(this.chkChangeType);
            this.splitContainer1.Panel1.Controls.Add(this.btnIn);
            this.splitContainer1.Panel1.Controls.Add(this.chkGenBarCode);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.neuSpread1);
            this.splitContainer1.Size = new System.Drawing.Size(1194, 705);
            this.splitContainer1.SplitterDistance = 38;
            this.splitContainer1.TabIndex = 18;
            // 
            // ucSpecReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSpecInfo);
            this.Name = "ucSpecReturn";
            this.Size = new System.Drawing.Size(1200, 730);
            this.Load += new System.EventHandler(this.ucSpecReturn_Load);
            this.grpSpecInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSpecInfo;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.CheckBox chkChangeType;
        private System.Windows.Forms.CheckBox chkGenBarCode;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.CheckBox chkKeepOld;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private System.Windows.Forms.ComboBox cmbOrgType;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudCapacity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
