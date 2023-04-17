namespace FS.SOC.Local.InpatientFee.FuYou
{
    partial class ucFinIpbPatientDayFeeDouble
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.pMainPrint = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbMemo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbTotCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPrepayCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pfspread = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPrintDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbStatDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbInDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPactCode = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbBedNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbInDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbInTimes = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPatientNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pMainPrint.SuspendLayout();
            this.pBottom.SuspendLayout();
            this.pfspread.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMainPrint
            // 
            this.pMainPrint.BackColor = System.Drawing.Color.White;
            this.pMainPrint.Controls.Add(this.pBottom);
            this.pMainPrint.Controls.Add(this.pfspread);
            this.pMainPrint.Controls.Add(this.pTop);
            this.pMainPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainPrint.Location = new System.Drawing.Point(0, 0);
            this.pMainPrint.Name = "pMainPrint";
            this.pMainPrint.Size = new System.Drawing.Size(850, 184);
            this.pMainPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pMainPrint.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.White;
            this.pBottom.Controls.Add(this.lbMemo);
            this.pBottom.Controls.Add(this.lbCount);
            this.pBottom.Controls.Add(this.lbTotCost);
            this.pBottom.Controls.Add(this.lbPrepayCost);
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.pBottom.Location = new System.Drawing.Point(0, 131);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(850, 51);
            this.pBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pBottom.TabIndex = 2;
            // 
            // lbMemo
            // 
            this.lbMemo.AutoSize = true;
            this.lbMemo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMemo.Location = new System.Drawing.Point(13, 33);
            this.lbMemo.Name = "lbMemo";
            this.lbMemo.Size = new System.Drawing.Size(371, 14);
            this.lbMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbMemo.TabIndex = 3;
            this.lbMemo.Text = "注：本清单属阶段费用合计，最终具体费用以出院结算为准";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCount.Location = new System.Drawing.Point(565, 8);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(112, 14);
            this.lbCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCount.TabIndex = 2;
            this.lbCount.Text = "本日记账共999条";
            // 
            // lbTotCost
            // 
            this.lbTotCost.AutoSize = true;
            this.lbTotCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTotCost.Location = new System.Drawing.Point(191, 8);
            this.lbTotCost.Name = "lbTotCost";
            this.lbTotCost.Size = new System.Drawing.Size(133, 14);
            this.lbTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTotCost.TabIndex = 1;
            this.lbTotCost.Text = "本张清单费用小计：";
            // 
            // lbPrepayCost
            // 
            this.lbPrepayCost.AutoSize = true;
            this.lbPrepayCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPrepayCost.Location = new System.Drawing.Point(13, 8);
            this.lbPrepayCost.Name = "lbPrepayCost";
            this.lbPrepayCost.Size = new System.Drawing.Size(63, 14);
            this.lbPrepayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPrepayCost.TabIndex = 0;
            this.lbPrepayCost.Text = "预交款：";
            // 
            // pfspread
            // 
            this.pfspread.Controls.Add(this.neuSpread1);
            this.pfspread.Dock = System.Windows.Forms.DockStyle.Top;
            this.pfspread.Location = new System.Drawing.Point(0, 107);
            this.pfspread.Name = "pfspread";
            this.pfspread.Size = new System.Drawing.Size(850, 24);
            this.pfspread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pfspread.TabIndex = 1;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(850, 24);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
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
            this.neuSpread1_Sheet1.ColumnCount = 13;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = " ";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "编码";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 150F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "单位";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 25F;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 25F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 40F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = " ";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 5F;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "编码";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 150F;
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "单位";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 25F;
            this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 25F;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 40F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.SheetCornerHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.Controls.Add(this.lbTitle);
            this.pTop.Controls.Add(this.lbPrintDate);
            this.pTop.Controls.Add(this.lbStatDate);
            this.pTop.Controls.Add(this.lbInDate);
            this.pTop.Controls.Add(this.lbPactCode);
            this.pTop.Controls.Add(this.lbAge);
            this.pTop.Controls.Add(this.lbSex);
            this.pTop.Controls.Add(this.lbName);
            this.pTop.Controls.Add(this.lbBedNo);
            this.pTop.Controls.Add(this.lbInDept);
            this.pTop.Controls.Add(this.lbInTimes);
            this.pTop.Controls.Add(this.lbPatientNo);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(850, 107);
            this.pTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pTop.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(371, 11);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(91, 14);
            this.lbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTitle.TabIndex = 11;
            this.lbTitle.Text = "患者一日清单";
            // 
            // lbPrintDate
            // 
            this.lbPrintDate.AutoSize = true;
            this.lbPrintDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPrintDate.Location = new System.Drawing.Point(565, 89);
            this.lbPrintDate.Name = "lbPrintDate";
            this.lbPrintDate.Size = new System.Drawing.Size(210, 14);
            this.lbPrintDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPrintDate.TabIndex = 10;
            this.lbPrintDate.Text = "打印日期：2011-01-01 00:00:00";
            // 
            // lbStatDate
            // 
            this.lbStatDate.AutoSize = true;
            this.lbStatDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStatDate.Location = new System.Drawing.Point(13, 89);
            this.lbStatDate.Name = "lbStatDate";
            this.lbStatDate.Size = new System.Drawing.Size(399, 14);
            this.lbStatDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbStatDate.TabIndex = 9;
            this.lbStatDate.Text = "统计日期范围：2011-01-01 00:00:00 至 2011-01-01 23:59:59";
            // 
            // lbInDate
            // 
            this.lbInDate.AutoSize = true;
            this.lbInDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInDate.Location = new System.Drawing.Point(565, 37);
            this.lbInDate.Name = "lbInDate";
            this.lbInDate.Size = new System.Drawing.Size(210, 14);
            this.lbInDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInDate.TabIndex = 8;
            this.lbInDate.Text = "入院日期：2011-01-01 00:00:00";
            // 
            // lbPactCode
            // 
            this.lbPactCode.AutoSize = true;
            this.lbPactCode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPactCode.Location = new System.Drawing.Point(565, 63);
            this.lbPactCode.Name = "lbPactCode";
            this.lbPactCode.Size = new System.Drawing.Size(189, 14);
            this.lbPactCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPactCode.TabIndex = 7;
            this.lbPactCode.Text = "结算种类：普通患者（自费）";
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.Location = new System.Drawing.Point(289, 63);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(147, 14);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 6;
            this.lbAge.Text = "年龄：100岁99月100天";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.Location = new System.Drawing.Point(191, 63);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(63, 14);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 5;
            this.lbSex.Text = "性别：男";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(13, 63);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(105, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 4;
            this.lbName.Text = "姓名：张三疯子";
            // 
            // lbBedNo
            // 
            this.lbBedNo.AutoSize = true;
            this.lbBedNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbBedNo.Location = new System.Drawing.Point(451, 37);
            this.lbBedNo.Name = "lbBedNo";
            this.lbBedNo.Size = new System.Drawing.Size(77, 14);
            this.lbBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbBedNo.TabIndex = 3;
            this.lbBedNo.Text = "床号：WU01";
            // 
            // lbInDept
            // 
            this.lbInDept.AutoSize = true;
            this.lbInDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInDept.Location = new System.Drawing.Point(289, 37);
            this.lbInDept.Name = "lbInDept";
            this.lbInDept.Size = new System.Drawing.Size(119, 14);
            this.lbInDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInDept.TabIndex = 2;
            this.lbInDept.Text = "科别：住院小儿科";
            // 
            // lbInTimes
            // 
            this.lbInTimes.AutoSize = true;
            this.lbInTimes.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInTimes.Location = new System.Drawing.Point(191, 37);
            this.lbInTimes.Name = "lbInTimes";
            this.lbInTimes.Size = new System.Drawing.Size(63, 14);
            this.lbInTimes.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInTimes.TabIndex = 1;
            this.lbInTimes.Text = "次数：99";
            // 
            // lbPatientNo
            // 
            this.lbPatientNo.AutoSize = true;
            this.lbPatientNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPatientNo.Location = new System.Drawing.Point(13, 37);
            this.lbPatientNo.Name = "lbPatientNo";
            this.lbPatientNo.Size = new System.Drawing.Size(133, 14);
            this.lbPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPatientNo.TabIndex = 0;
            this.lbPatientNo.Text = "住院号：0000000000";
            // 
            // ucFinIpbPatientDayFeeDouble
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pMainPrint);
            this.Name = "ucFinIpbPatientDayFeeDouble";
            this.Size = new System.Drawing.Size(850, 184);
            this.pMainPrint.ResumeLayout(false);
            this.pBottom.ResumeLayout(false);
            this.pBottom.PerformLayout();
            this.pfspread.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pMainPrint;
        private FS.FrameWork.WinForms.Controls.NeuPanel pBottom;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbMemo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCount;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTotCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPrepayCost;
        private FS.FrameWork.WinForms.Controls.NeuPanel pfspread;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pTop;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPrintDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbStatDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbInDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPactCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbBedNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbInDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbInTimes;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPatientNo;

    }
}
