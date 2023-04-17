namespace IBorn.SI.MedicalInsurance.BaseControls
{
    partial class ucInPatientFeeDetailBabyBill
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
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder lineBorder4 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder lineBorder5 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder lineBorder6 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbprintdate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbhospital = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblFeeDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPatientNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPageNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRowCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.lbprintdate);
            this.neuPanel1.Controls.Add(this.lbhospital);
            this.neuPanel1.Controls.Add(this.lblSex);
            this.neuPanel1.Controls.Add(this.lblName);
            this.neuPanel1.Controls.Add(this.lblFeeDate);
            this.neuPanel1.Controls.Add(this.nlbTitle);
            this.neuPanel1.Controls.Add(this.lblPatientNo);
            this.neuPanel1.Controls.Add(this.nlbPageNo);
            this.neuPanel1.Controls.Add(this.lblDept);
            this.neuPanel1.Controls.Add(this.nlbRowCount);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(770, 131);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // lbprintdate
            // 
            this.lbprintdate.AutoSize = true;
            this.lbprintdate.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbprintdate.Location = new System.Drawing.Point(551, 40);
            this.lbprintdate.Name = "lbprintdate";
            this.lbprintdate.Size = new System.Drawing.Size(191, 15);
            this.lbprintdate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbprintdate.TabIndex = 250;
            this.lbprintdate.Text = "打印日期：2020年06月23号";
            // 
            // lbhospital
            // 
            this.lbhospital.AutoSize = true;
            this.lbhospital.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbhospital.Location = new System.Drawing.Point(22, 68);
            this.lbhospital.Name = "lbhospital";
            this.lbhospital.Size = new System.Drawing.Size(247, 15);
            this.lbhospital.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbhospital.TabIndex = 249;
            this.lbhospital.Text = "医疗机构名称：广州爱博恩妇产医院";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(167, 100);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(67, 15);
            this.lblSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSex.TabIndex = 248;
            this.lblSex.Text = "性别：女";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(22, 100);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(112, 15);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 246;
            this.lblName.Text = "姓名：周三角阀";
            // 
            // lblFeeDate
            // 
            this.lblFeeDate.AutoSize = true;
            this.lblFeeDate.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFeeDate.Location = new System.Drawing.Point(478, 68);
            this.lblFeeDate.Name = "lblFeeDate";
            this.lblFeeDate.Size = new System.Drawing.Size(273, 15);
            this.lblFeeDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblFeeDate.TabIndex = 242;
            this.lblFeeDate.Text = "统计日期：2017-10-20 至 2017-10-22";
            // 
            // nlbTitle
            // 
            this.nlbTitle.AutoSize = true;
            this.nlbTitle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTitle.Location = new System.Drawing.Point(173, 7);
            this.nlbTitle.Name = "nlbTitle";
            this.nlbTitle.Size = new System.Drawing.Size(364, 21);
            this.nlbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTitle.TabIndex = 238;
            this.nlbTitle.Text = "广州爱博恩妇产医院(住院)明细清单";
            // 
            // lblPatientNo
            // 
            this.lblPatientNo.AutoSize = true;
            this.lblPatientNo.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientNo.Location = new System.Drawing.Point(302, 100);
            this.lblPatientNo.Name = "lblPatientNo";
            this.lblPatientNo.Size = new System.Drawing.Size(147, 15);
            this.lblPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientNo.TabIndex = 229;
            this.lblPatientNo.Text = "住院号：1234567890";
            // 
            // nlbPageNo
            // 
            this.nlbPageNo.AutoSize = true;
            this.nlbPageNo.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbPageNo.Location = new System.Drawing.Point(562, 4);
            this.nlbPageNo.Name = "nlbPageNo";
            this.nlbPageNo.Size = new System.Drawing.Size(76, 15);
            this.nlbPageNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPageNo.TabIndex = 47;
            this.nlbPageNo.Text = "页码：1/1";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(302, 68);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(97, 15);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 16;
            this.lblDept.Text = "科室：妇产科";
            // 
            // nlbRowCount
            // 
            this.nlbRowCount.AutoSize = true;
            this.nlbRowCount.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbRowCount.Location = new System.Drawing.Point(648, 4);
            this.nlbRowCount.Name = "nlbRowCount";
            this.nlbRowCount.Size = new System.Drawing.Size(91, 15);
            this.nlbRowCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRowCount.TabIndex = 15;
            this.nlbRowCount.Text = "记录数：120";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.Location = new System.Drawing.Point(0, 131);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(770, 969);
            this.neuSpread1.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.Color.Empty;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.Color.Empty;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 9;
            this.neuSpread1_Sheet1.RowCount = 10;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, false);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "序号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "费用类别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Border = lineBorder2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "可报销基本药物";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "剂型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Border = lineBorder3;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Border = lineBorder4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "单价";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Border = lineBorder5;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            textCellType1.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 10F);
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "序号";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 40F;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 10.75F);
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "费用类别";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 64F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 10.75F);
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "名称";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 247F;
            textCellType2.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 10.75F);
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "可报销基本药物";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(3).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 78F;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 10.75F);
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "剂型";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(4).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 69F;
            textCellType3.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 10.75F);
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 148F;
            textCellType4.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 10.75F);
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "单价";
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 106F;
            textCellType5.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType5;
            this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 10.75F);
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 53F;
            textCellType6.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType6;
            this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 10.75F);
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 92F;
            this.neuSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 10F);
            this.neuSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(0, 131);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1,
            this.fpSpread1_Sheet2});
            this.fpSpread1.Size = new System.Drawing.Size(770, 969);
            this.fpSpread1.TabIndex = 3;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Visible = false;
            this.fpSpread1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellClick_1);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread1_Sheet2
            // 
            this.fpSpread1_Sheet2.Reset();
            this.fpSpread1_Sheet2.SheetName = "明细";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet2.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucInPatientFeeDetailBabyBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucInPatientFeeDetailBabyBill";
            this.Size = new System.Drawing.Size(770, 1100);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FarPoint.Win.Spread.FpSpread neuSpread1;
        protected FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRowCount;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbPageNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientNo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbTitle;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblFeeDate;
        public FarPoint.Win.Spread.FpSpread fpSpread1;
        public FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        public FarPoint.Win.Spread.SheetView fpSpread1_Sheet2;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbhospital;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbprintdate;

    }
}
