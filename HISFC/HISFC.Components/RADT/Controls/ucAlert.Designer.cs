namespace FS.HISFC.Components.RADT.Controls
{
    partial class ucAlert
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntbEnughCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ntxtPatientNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntbStartCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntbDept = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.chkSelectAll = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btnPrintGrid = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.chkOwnFeePatient = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btnPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCompute = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.chkAll = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.neuSpread1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 559);
            this.panel1.TabIndex = 29;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 78);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(953, 481);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 2;
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
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 13;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "床号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "病人号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "类别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "合同单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "预交金";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "总金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "自费金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "余额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "警戒线";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "入院日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "补缴金额";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 31F;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "床号";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "病人号";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "类别";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType5;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "合同单位";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType6;
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "预交金";
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType7;
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "总金额";
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 93F;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType8;
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "自费金额";
            this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 86F;
            this.neuSpread1_Sheet1.Columns.Get(9).CellType = textCellType9;
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "余额";
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(10).CellType = textCellType10;
            this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "警戒线";
            this.neuSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(11).CellType = textCellType11;
            this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "入院日期";
            this.neuSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(12).CellType = textCellType12;
            this.neuSpread1_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "补缴金额";
            this.neuSpread1_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 74F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(953, 481);
            this.panel3.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.neuLabel5);
            this.panel2.Controls.Add(this.ntbEnughCost);
            this.panel2.Controls.Add(this.ntxtPatientNo);
            this.panel2.Controls.Add(this.neuLabel4);
            this.panel2.Controls.Add(this.neuLabel3);
            this.panel2.Controls.Add(this.ntbStartCost);
            this.panel2.Controls.Add(this.neuLabel6);
            this.panel2.Controls.Add(this.neuLabel2);
            this.panel2.Controls.Add(this.neuLabel1);
            this.panel2.Controls.Add(this.ntbDept);
            this.panel2.Controls.Add(this.chkSelectAll);
            this.panel2.Controls.Add(this.btnPrintGrid);
            this.panel2.Controls.Add(this.chkOwnFeePatient);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnCompute);
            this.panel2.Controls.Add(this.chkAll);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(953, 78);
            this.panel2.TabIndex = 29;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel5.ForeColor = System.Drawing.Color.SteelBlue;
            this.neuLabel5.Location = new System.Drawing.Point(646, 38);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(161, 14);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 37;
            this.neuLabel5.Text = "起催金额：起始催款金额";
            // 
            // ntbEnughCost
            // 
            this.ntbEnughCost.ForeColor = System.Drawing.Color.Red;
            this.ntbEnughCost.IsEnter2Tab = false;
            this.ntbEnughCost.Location = new System.Drawing.Point(519, 12);
            this.ntbEnughCost.Name = "ntbEnughCost";
            this.ntbEnughCost.Size = new System.Drawing.Size(100, 21);
            this.ntbEnughCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbEnughCost.TabIndex = 31;
            this.ntbEnughCost.Text = "500.00";
            // 
            // ntxtPatientNo
            // 
            this.ntxtPatientNo.IsEnter2Tab = false;
            this.ntxtPatientNo.Location = new System.Drawing.Point(519, 45);
            this.ntxtPatientNo.Name = "ntxtPatientNo";
            this.ntxtPatientNo.Size = new System.Drawing.Size(100, 21);
            this.ntxtPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtPatientNo.TabIndex = 36;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel4.Location = new System.Drawing.Point(310, 48);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(168, 14);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 35;
            this.neuLabel4.Text = "选择病人催款（病人号）:";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.ForeColor = System.Drawing.Color.Red;
            this.neuLabel3.Location = new System.Drawing.Point(421, 19);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(77, 14);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 34;
            this.neuLabel3.Text = "催足金额：";
            // 
            // ntbStartCost
            // 
            this.ntbStartCost.ForeColor = System.Drawing.Color.Red;
            this.ntbStartCost.IsEnter2Tab = false;
            this.ntbStartCost.Location = new System.Drawing.Point(294, 12);
            this.ntbStartCost.Name = "ntbStartCost";
            this.ntbStartCost.Size = new System.Drawing.Size(100, 21);
            this.ntbStartCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbStartCost.TabIndex = 30;
            this.ntbStartCost.Text = "500.00";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel6.ForeColor = System.Drawing.Color.SteelBlue;
            this.neuLabel6.Location = new System.Drawing.Point(646, 59);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(203, 14);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 38;
            this.neuLabel6.Text = "催足金额：患者交款后剩余金额";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.ForeColor = System.Drawing.Color.Red;
            this.neuLabel2.Location = new System.Drawing.Point(211, 19);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(77, 14);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 33;
            this.neuLabel2.Text = "起催金额：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(25, 19);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(49, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 32;
            this.neuLabel1.Text = "科室：";
            // 
            // ntbDept
            // 
            this.ntbDept.IsEnter2Tab = false;
            this.ntbDept.Location = new System.Drawing.Point(82, 12);
            this.ntbDept.Name = "ntbDept";
            this.ntbDept.Size = new System.Drawing.Size(100, 21);
            this.ntbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbDept.TabIndex = 29;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkSelectAll.Location = new System.Drawing.Point(200, 48);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(54, 18);
            this.chkSelectAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkSelectAll.TabIndex = 17;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrintGrid.Location = new System.Drawing.Point(839, 9);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(74, 24);
            this.btnPrintGrid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnPrintGrid.TabIndex = 15;
            this.btnPrintGrid.Text = "打印表格";
            this.btnPrintGrid.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnPrintGrid.UseVisualStyleBackColor = false;
            this.btnPrintGrid.Visible = false;
            // 
            // chkOwnFeePatient
            // 
            this.chkOwnFeePatient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkOwnFeePatient.AutoSize = true;
            this.chkOwnFeePatient.Checked = true;
            this.chkOwnFeePatient.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOwnFeePatient.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOwnFeePatient.Location = new System.Drawing.Point(24, 48);
            this.chkOwnFeePatient.Name = "chkOwnFeePatient";
            this.chkOwnFeePatient.Size = new System.Drawing.Size(82, 18);
            this.chkOwnFeePatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkOwnFeePatient.TabIndex = 28;
            this.chkOwnFeePatient.Text = "只催自费";
            this.chkOwnFeePatient.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.Location = new System.Drawing.Point(717, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(74, 24);
            this.btnPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnPrint.TabIndex = 14;
            this.btnPrint.Text = "打印催款";
            this.btnPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnCompute
            // 
            this.btnCompute.BackColor = System.Drawing.SystemColors.Control;
            this.btnCompute.Location = new System.Drawing.Point(637, 9);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(74, 24);
            this.btnCompute.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCompute.TabIndex = 13;
            this.btnCompute.Text = "查询";
            this.btnCompute.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCompute.UseVisualStyleBackColor = false;
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAll.Location = new System.Drawing.Point(112, 48);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(82, 18);
            this.chkAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkAll.TabIndex = 16;
            this.chkAll.Text = "全部病人";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.Visible = false;
            // 
            // ucAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.Controls.Add(this.panel1);
            this.Name = "ucAlert";
            this.Size = new System.Drawing.Size(953, 559);
            this.Load += new System.EventHandler(this.ucAlert_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Panel panel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntbEnughCost;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtPatientNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntbStartCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntbDept;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkSelectAll;
        private FS.FrameWork.WinForms.Controls.NeuButton btnPrintGrid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkOwnFeePatient;
        private FS.FrameWork.WinForms.Controls.NeuButton btnPrint;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCompute;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkAll;
    }
}
