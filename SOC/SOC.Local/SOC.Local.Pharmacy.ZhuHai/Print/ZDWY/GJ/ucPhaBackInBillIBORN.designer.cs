namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.GJ
{
    partial class ucPhaBackInBillIBORN
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
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblCurDif = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCurRetailCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCurPurCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotDiff = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotRetail = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotPurCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblBuyer = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblRecord = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblStoreOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblInputTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblBillID = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCompany = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPage = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPrintDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1_Sheet1)).BeginInit();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Controls.Add(this.neuPanel4);
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(860, 176);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.lblCurPurCost);
            this.neuPanel2.Controls.Add(this.lblTotPurCost);
            this.neuPanel2.Controls.Add(this.lblBuyer);
            this.neuPanel2.Controls.Add(this.lblRecord);
            this.neuPanel2.Controls.Add(this.lblOper);
            this.neuPanel2.Controls.Add(this.lblStoreOper);
            this.neuPanel2.Controls.Add(this.lblCurDif);
            this.neuPanel2.Controls.Add(this.lblCurRetailCost);
            this.neuPanel2.Controls.Add(this.lblTotDiff);
            this.neuPanel2.Controls.Add(this.lblTotRetail);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 114);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(860, 67);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 18;
            // 
            // lblCurDif
            // 
            this.lblCurDif.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurDif.Location = new System.Drawing.Point(309, 3);
            this.lblCurDif.Name = "lblCurDif";
            this.lblCurDif.Size = new System.Drawing.Size(177, 23);
            this.lblCurDif.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCurDif.TabIndex = 36;
            this.lblCurDif.Text = "本页进销差：";
            this.lblCurDif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurRetailCost
            // 
            this.lblCurRetailCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurRetailCost.Location = new System.Drawing.Point(3, 3);
            this.lblCurRetailCost.Name = "lblCurRetailCost";
            this.lblCurRetailCost.Size = new System.Drawing.Size(216, 23);
            this.lblCurRetailCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCurRetailCost.TabIndex = 35;
            this.lblCurRetailCost.Text = "本页零售金额：";
            this.lblCurRetailCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurPurCost
            // 
            this.lblCurPurCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurPurCost.Location = new System.Drawing.Point(606, 3);
            this.lblCurPurCost.Name = "lblCurPurCost";
            this.lblCurPurCost.Size = new System.Drawing.Size(226, 23);
            this.lblCurPurCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCurPurCost.TabIndex = 34;
            this.lblCurPurCost.Text = "本页买入金额：";
            this.lblCurPurCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotDiff
            // 
            this.lblTotDiff.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotDiff.Location = new System.Drawing.Point(309, 21);
            this.lblTotDiff.Name = "lblTotDiff";
            this.lblTotDiff.Size = new System.Drawing.Size(177, 23);
            this.lblTotDiff.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotDiff.TabIndex = 33;
            this.lblTotDiff.Text = "进销差总计：";
            this.lblTotDiff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotRetail
            // 
            this.lblTotRetail.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotRetail.Location = new System.Drawing.Point(3, 21);
            this.lblTotRetail.Name = "lblTotRetail";
            this.lblTotRetail.Size = new System.Drawing.Size(216, 23);
            this.lblTotRetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotRetail.TabIndex = 32;
            this.lblTotRetail.Text = "零售金额总计：";
            this.lblTotRetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotPurCost
            // 
            this.lblTotPurCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotPurCost.Location = new System.Drawing.Point(606, 21);
            this.lblTotPurCost.Name = "lblTotPurCost";
            this.lblTotPurCost.Size = new System.Drawing.Size(226, 23);
            this.lblTotPurCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotPurCost.TabIndex = 31;
            this.lblTotPurCost.Text = "买入金额总计：";
            this.lblTotPurCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBuyer
            // 
            this.lblBuyer.Font = new System.Drawing.Font("宋体", 10F);
            this.lblBuyer.Location = new System.Drawing.Point(425, 41);
            this.lblBuyer.Name = "lblBuyer";
            this.lblBuyer.Size = new System.Drawing.Size(124, 23);
            this.lblBuyer.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBuyer.TabIndex = 26;
            this.lblBuyer.Text = "采购员：";
            this.lblBuyer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecord
            // 
            this.lblRecord.Font = new System.Drawing.Font("宋体", 10F);
            this.lblRecord.Location = new System.Drawing.Point(653, 41);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(124, 23);
            this.lblRecord.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblRecord.TabIndex = 15;
            this.lblRecord.Text = "批准人：";
            this.lblRecord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOper
            // 
            this.lblOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblOper.Location = new System.Drawing.Point(54, 41);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(122, 23);
            this.lblOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOper.TabIndex = 8;
            this.lblOper.Text = "制单人：";
            this.lblOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStoreOper
            // 
            this.lblStoreOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblStoreOper.Location = new System.Drawing.Point(221, 41);
            this.lblStoreOper.Name = "lblStoreOper";
            this.lblStoreOper.Size = new System.Drawing.Size(124, 23);
            this.lblStoreOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblStoreOper.TabIndex = 10;
            this.lblStoreOper.Text = "仓管员：";
            this.lblStoreOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.neuFpEnter1);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 70);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(860, 44);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 17;
            // 
            // neuFpEnter1
            // 
            this.neuFpEnter1.About = "3.0.2004.2005";
            this.neuFpEnter1.AccessibleDescription = "neuFpEnter1, Sheet1, Row 0, Column 0, ";
            this.neuFpEnter1.BackColor = System.Drawing.Color.White;
            this.neuFpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuFpEnter1.EditModePermanent = true;
            this.neuFpEnter1.EditModeReplace = true;
            this.neuFpEnter1.Font = new System.Drawing.Font("宋体", 10F);
            this.neuFpEnter1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuFpEnter1.Location = new System.Drawing.Point(0, 0);
            this.neuFpEnter1.Name = "neuFpEnter1";
            this.neuFpEnter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuFpEnter1.SelectNone = false;
            this.neuFpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuFpEnter1_Sheet1});
            this.neuFpEnter1.ShowListWhenOfFocus = false;
            this.neuFpEnter1.Size = new System.Drawing.Size(860, 44);
            this.neuFpEnter1.TabIndex = 8;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuFpEnter1.TextTipAppearance = tipAppearance4;
            this.neuFpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuFpEnter1_Sheet1
            // 
            this.neuFpEnter1_Sheet1.Reset();
            this.neuFpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuFpEnter1_Sheet1.ColumnCount = 9;
            this.neuFpEnter1_Sheet1.RowCount = 1;
            this.neuFpEnter1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuFpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "序号";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "批号";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "药库单位";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "购入单价(元)";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "购入金额(元)";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "发票号";
            this.neuFpEnter1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.neuFpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuFpEnter1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.neuFpEnter1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            textCellType4.Multiline = true;
            textCellType4.WordWrap = true;
            this.neuFpEnter1_Sheet1.Columns.Get(0).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Label = "序号";
            this.neuFpEnter1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Width = 31F;
            this.neuFpEnter1_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Label = "名称";
            this.neuFpEnter1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Width = 227F;
            this.neuFpEnter1_Sheet1.Columns.Get(2).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Label = "规格";
            this.neuFpEnter1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Width = 108F;
            this.neuFpEnter1_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Label = "批号";
            this.neuFpEnter1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Width = 74F;
            this.neuFpEnter1_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Label = "数量";
            this.neuFpEnter1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Width = 57F;
            this.neuFpEnter1_Sheet1.Columns.Get(5).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Label = "药库单位";
            this.neuFpEnter1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Width = 40F;
            this.neuFpEnter1_Sheet1.Columns.Get(6).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Label = "购入单价(元)";
            this.neuFpEnter1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Width = 77F;
            this.neuFpEnter1_Sheet1.Columns.Get(7).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Label = "购入金额(元)";
            this.neuFpEnter1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Width = 88F;
            this.neuFpEnter1_Sheet1.Columns.Get(8).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Label = "发票号";
            this.neuFpEnter1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Width = 111F;
            this.neuFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuFpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuFpEnter1_Sheet1.Rows.Default.Height = 30F;
            this.neuFpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.lblPrintDate);
            this.neuPanel3.Controls.Add(this.lblInputTime);
            this.neuPanel3.Controls.Add(this.lblBillID);
            this.neuPanel3.Controls.Add(this.lblTitle);
            this.neuPanel3.Controls.Add(this.lblCompany);
            this.neuPanel3.Controls.Add(this.lblPage);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(860, 70);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 15;
            // 
            // lblInputTime
            // 
            this.lblInputTime.Font = new System.Drawing.Font("宋体", 10F);
            this.lblInputTime.Location = new System.Drawing.Point(402, 27);
            this.lblInputTime.Name = "lblInputTime";
            this.lblInputTime.Size = new System.Drawing.Size(265, 23);
            this.lblInputTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInputTime.TabIndex = 34;
            this.lblInputTime.Text = "退货日期：2014.01.02";
            this.lblInputTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBillID
            // 
            this.lblBillID.Font = new System.Drawing.Font("宋体", 10F);
            this.lblBillID.Location = new System.Drawing.Point(402, 49);
            this.lblBillID.Name = "lblBillID";
            this.lblBillID.Size = new System.Drawing.Size(241, 19);
            this.lblBillID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBillID.TabIndex = 24;
            this.lblBillID.Text = "外退单号：";
            this.lblBillID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(286, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(142, 19);
            this.lblTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTitle.TabIndex = 18;
            this.lblTitle.Text = "{0}药品入库单";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCompany.Location = new System.Drawing.Point(12, 49);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(369, 19);
            this.lblCompany.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCompany.TabIndex = 20;
            this.lblCompany.Text = "供货单位：";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPage.Location = new System.Drawing.Point(719, 5);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(84, 23);
            this.lblPage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPage.TabIndex = 25;
            this.lblPage.Text = "/";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrintDate
            // 
            this.lblPrintDate.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPrintDate.Location = new System.Drawing.Point(12, 26);
            this.lblPrintDate.Name = "lblPrintDate";
            this.lblPrintDate.Size = new System.Drawing.Size(286, 23);
            this.lblPrintDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPrintDate.TabIndex = 35;
            this.lblPrintDate.Text = "制单日期：2014.01.02";
            this.lblPrintDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPhaBackInBillIBORN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPhaBackInBillIBORN";
            this.Size = new System.Drawing.Size(860, 176);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1_Sheet1)).EndInit();
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPage;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBillID;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCompany;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblOper;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblStoreOper;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuFpEnter1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblRecord;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBuyer;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotDiff;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotRetail;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotPurCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInputTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCurDif;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCurRetailCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCurPurCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPrintDate;
    }
}
