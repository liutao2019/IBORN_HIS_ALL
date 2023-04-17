namespace FS.HISFC.Components.Speciment
{
    partial class ucOutSpecList
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.btnOut = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.nspSpecList = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.nspSpecList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.chkBack = new System.Windows.Forms.CheckBox();
            this.nudBorDay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtApplyNum = new System.Windows.Forms.TextBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.nspSpecList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nspSpecList_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOut
            // 
            this.btnOut.Enabled = false;
            this.btnOut.Location = new System.Drawing.Point(664, 1);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(87, 25);
            this.btnOut.TabIndex = 1;
            this.btnOut.Text = "出库";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(5, 3);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(54, 18);
            this.chkSelectAll.TabIndex = 0;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // nspSpecList
            // 
            this.nspSpecList.About = "2.5.2007.2005";
            this.nspSpecList.AccessibleDescription = "nspSpecList, Sheet1";
            this.nspSpecList.BackColor = System.Drawing.Color.White;
            this.nspSpecList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nspSpecList.FileName = "";
            this.nspSpecList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.nspSpecList.IsAutoSaveGridStatus = false;
            this.nspSpecList.IsCanCustomConfigColumn = false;
            this.nspSpecList.Location = new System.Drawing.Point(0, 0);
            this.nspSpecList.Name = "nspSpecList";
            this.nspSpecList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.nspSpecList_Sheet1});
            this.nspSpecList.Size = new System.Drawing.Size(1310, 569);
            this.nspSpecList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nspSpecList.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.nspSpecList.TextTipAppearance = tipAppearance1;
            this.nspSpecList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.nspSpecList.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.nspSpecList_CellClick);
            this.nspSpecList.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.nspSpecList_SelectionChanged);
            this.nspSpecList.AutoFilteredColumn += new FarPoint.Win.Spread.AutoFilteredColumnEventHandler(this.nspSpecList_AutoFilteredColumn);
            // 
            // nspSpecList_Sheet1
            // 
            this.nspSpecList_Sheet1.Reset();
            this.nspSpecList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.nspSpecList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.nspSpecList_Sheet1.ColumnCount = 29;
            this.nspSpecList_Sheet1.RowCount = 0;
            this.nspSpecList_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.nspSpecList_Sheet1.AutoGenerateColumns = false;
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "归还";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "借用期限";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "份量";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "条码";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "病种";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "标本号";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "标本类型";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "肿物部位";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "癌种类";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "姓名";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "病历号";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "性别";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "年龄";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "送存科室";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "送存医生";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "送存日期";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "主诊断";

            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "癌性质";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "出库次数";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "盒条码";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "行";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "列";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 23).Value = "放疗方案";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 24).Value = "化疗方案";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 25).Value = "入院诊断";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 26).Value = "门诊诊断";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 27).Value = "出院诊断";
            this.nspSpecList_Sheet1.ColumnHeader.Cells.Get(0, 28).Value = "在库状态";
            this.nspSpecList_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.nspSpecList_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.nspSpecList_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.nspSpecList_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.nspSpecList_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.nspSpecList_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.nspSpecList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(0).Label = "选择";
            this.nspSpecList_Sheet1.Columns.Get(0).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(0).Width = 34F;
            checkBoxCellType2.TextFalse = "不归还";
            checkBoxCellType2.TextIndeterminate = "多次出库";
            checkBoxCellType2.TextTrue = "归还";
            checkBoxCellType2.ThreeState = true;
            this.nspSpecList_Sheet1.Columns.Get(1).CellType = checkBoxCellType2;
            this.nspSpecList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(1).Label = "归还";
            this.nspSpecList_Sheet1.Columns.Get(1).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(1).Width = 70F;
            this.nspSpecList_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.nspSpecList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(2).Label = "借用期限";
            this.nspSpecList_Sheet1.Columns.Get(2).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.nspSpecList_Sheet1.Columns.Get(3).Label = "份量";
            this.nspSpecList_Sheet1.Columns.Get(3).Width = 44F;
            this.nspSpecList_Sheet1.Columns.Get(4).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(4).Label = "条码";
            this.nspSpecList_Sheet1.Columns.Get(4).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(4).Width = 100F;
            this.nspSpecList_Sheet1.Columns.Get(5).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(5).Label = "病种";
            this.nspSpecList_Sheet1.Columns.Get(5).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(5).Width = 75F;
            this.nspSpecList_Sheet1.Columns.Get(6).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(6).Label = "标本号";
            this.nspSpecList_Sheet1.Columns.Get(6).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(6).Width = 70F;
            this.nspSpecList_Sheet1.Columns.Get(7).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(7).Label = "标本类型";
            this.nspSpecList_Sheet1.Columns.Get(7).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(7).Width = 70F;
            this.nspSpecList_Sheet1.Columns.Get(8).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(8).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(8).CellType = textCellType2;
            this.nspSpecList_Sheet1.Columns.Get(8).Label = "肿物部位";
            this.nspSpecList_Sheet1.Columns.Get(8).Width = 70F;
            this.nspSpecList_Sheet1.Columns.Get(9).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(9).Label = "癌种类";
            this.nspSpecList_Sheet1.Columns.Get(9).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(9).Width = 70F;
            this.nspSpecList_Sheet1.Columns.Get(10).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(10).AllowAutoSort = true;
            //this.nspSpecList_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(10).Label = "姓名";
            this.nspSpecList_Sheet1.Columns.Get(10).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(10).Width = 96F;
            this.nspSpecList_Sheet1.Columns.Get(11).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(11).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(11).Label = "病历号";
            this.nspSpecList_Sheet1.Columns.Get(11).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(11).Width = 98F;
            this.nspSpecList_Sheet1.Columns.Get(12).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(12).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(12).Label = "性别";
            this.nspSpecList_Sheet1.Columns.Get(12).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(12).Width = 45F;
            this.nspSpecList_Sheet1.Columns.Get(13).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(13).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(13).Label = "年龄";
            this.nspSpecList_Sheet1.Columns.Get(13).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(13).Width = 98F;
            this.nspSpecList_Sheet1.Columns.Get(14).AllowAutoFilter = true;
            this.nspSpecList_Sheet1.Columns.Get(14).AllowAutoSort = true;
            this.nspSpecList_Sheet1.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(14).Label = "送存科室";
            this.nspSpecList_Sheet1.Columns.Get(14).Resizable = false;
            this.nspSpecList_Sheet1.Columns.Get(14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(14).Width = 90F;
            this.nspSpecList_Sheet1.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(15).Label = "送存医生";
            this.nspSpecList_Sheet1.Columns.Get(15).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(16).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(16).Label = "送存日期";
            this.nspSpecList_Sheet1.Columns.Get(16).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(16).Width = 98F;
            this.nspSpecList_Sheet1.Columns.Get(17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(17).Label = "主诊断";
            this.nspSpecList_Sheet1.Columns.Get(17).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(18).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(18).Label = "癌性质";
            this.nspSpecList_Sheet1.Columns.Get(18).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(19).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(19).Label = "出库次数";
            this.nspSpecList_Sheet1.Columns.Get(19).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(20).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(20).Label = "盒条码";
            this.nspSpecList_Sheet1.Columns.Get(20).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(21).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(21).Label = "行";
            this.nspSpecList_Sheet1.Columns.Get(21).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(22).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(22).Label = "列";
            this.nspSpecList_Sheet1.Columns.Get(22).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(23).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(23).Label = "放疗方案";
            this.nspSpecList_Sheet1.Columns.Get(23).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(24).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(24).Label = "化疗方案";
            this.nspSpecList_Sheet1.Columns.Get(24).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(25).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(25).Label = "入院诊断";
            this.nspSpecList_Sheet1.Columns.Get(25).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(26).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(26).Label = "门诊诊断";
            this.nspSpecList_Sheet1.Columns.Get(26).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(27).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(27).Label = "出院诊断";
            this.nspSpecList_Sheet1.Columns.Get(27).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(28).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.nspSpecList_Sheet1.Columns.Get(28).Label = "在库状态";
            this.nspSpecList_Sheet1.Columns.Get(28).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.nspSpecList_Sheet1.DataAutoSizeColumns = false;
            this.nspSpecList_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.nspSpecList_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.nspSpecList_Sheet1.DefaultStyle.Locked = false;
            this.nspSpecList_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.nspSpecList_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.nspSpecList_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.nspSpecList_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.nspSpecList_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.nspSpecList_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.nspSpecList_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.nspSpecList_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.nspSpecList_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.nspSpecList_Sheet1.SheetCornerStyle.Locked = false;
            this.nspSpecList_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.nspSpecList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.nspSpecList.SetActiveViewport(1, 0);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(756, 1);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(87, 25);
            this.btnExport.TabIndex = 74;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(862, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 75;
            this.label1.Text = "共找到记录数：";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(970, 8);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 14);
            this.lblCount.TabIndex = 76;
            // 
            // chkBack
            // 
            this.chkBack.AutoSize = true;
            this.chkBack.Location = new System.Drawing.Point(61, 3);
            this.chkBack.Name = "chkBack";
            this.chkBack.Size = new System.Drawing.Size(68, 18);
            this.chkBack.TabIndex = 77;
            this.chkBack.Text = "不归还";
            this.chkBack.ThreeState = true;
            this.chkBack.UseVisualStyleBackColor = true;
            this.chkBack.CheckStateChanged += new System.EventHandler(this.chkBack_CheckedChanged);
            // 
            // nudBorDay
            // 
            this.nudBorDay.Location = new System.Drawing.Point(207, 2);
            this.nudBorDay.Name = "nudBorDay";
            this.nudBorDay.Size = new System.Drawing.Size(62, 23);
            this.nudBorDay.TabIndex = 78;
            this.nudBorDay.ValueChanged += new System.EventHandler(this.nudBorDay_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 79;
            this.label2.Text = "借用期限：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 14);
            this.label3.TabIndex = 80;
            this.label3.Text = "天";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 79;
            this.label4.Text = "份";
            // 
            // nudCount
            // 
            this.nudCount.DecimalPlaces = 2;
            this.nudCount.Location = new System.Drawing.Point(309, 2);
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(62, 23);
            this.nudCount.TabIndex = 78;
            this.nudCount.ValueChanged += new System.EventHandler(this.nudCount_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(498, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 81;
            this.label5.Text = "申请编号：";
            // 
            // txtApplyNum
            // 
            this.txtApplyNum.Location = new System.Drawing.Point(577, 2);
            this.txtApplyNum.Name = "txtApplyNum";
            this.txtApplyNum.Size = new System.Drawing.Size(81, 23);
            this.txtApplyNum.TabIndex = 82;
            this.txtApplyNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtApplyNum_KeyDown);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(402, 1);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(87, 25);
            this.btnModify.TabIndex = 83;
            this.btnModify.Text = "标本详情";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.chkSelectAll);
            this.splitContainer1.Panel1.Controls.Add(this.btnModify);
            this.splitContainer1.Panel1.Controls.Add(this.btnOut);
            this.splitContainer1.Panel1.Controls.Add(this.txtApplyNum);
            this.splitContainer1.Panel1.Controls.Add(this.btnExport);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.lblCount);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.chkBack);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.nudBorDay);
            this.splitContainer1.Panel1.Controls.Add(this.nudCount);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.nspSpecList);
            this.splitContainer1.Size = new System.Drawing.Size(1310, 600);
            this.splitContainer1.SplitterDistance = 27;
            this.splitContainer1.TabIndex = 84;
            // 
            // ucOutSpecList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucOutSpecList";
            this.Size = new System.Drawing.Size(1310, 600);
            ((System.ComponentModel.ISupportInitialize)(this.nspSpecList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nspSpecList_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private FS.FrameWork.WinForms.Controls.NeuSpread nspSpecList;
        private FarPoint.Win.Spread.SheetView nspSpecList_Sheet1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.CheckBox chkBack;
        private System.Windows.Forms.NumericUpDown nudBorDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtApplyNum;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
