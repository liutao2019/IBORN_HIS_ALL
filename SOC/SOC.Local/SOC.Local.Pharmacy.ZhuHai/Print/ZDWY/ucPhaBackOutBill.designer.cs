namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    partial class ucPhaBackOutBill
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType10 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblCurPur = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCurDif = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCurRet = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblInputDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotRetailCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotPurCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel99 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPage = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblBillID = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCompany = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotDif = new FS.FrameWork.WinForms.Controls.NeuLabel();
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
            this.neuPanel1.Size = new System.Drawing.Size(860, 149);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.lblTotDif);
            this.neuPanel2.Controls.Add(this.neuLabel4);
            this.neuPanel2.Controls.Add(this.lblTotRetailCost);
            this.neuPanel2.Controls.Add(this.neuLabel3);
            this.neuPanel2.Controls.Add(this.lblTotPurCost);
            this.neuPanel2.Controls.Add(this.neuLabel2);
            this.neuPanel2.Controls.Add(this.lblCurPur);
            this.neuPanel2.Controls.Add(this.lblCurDif);
            this.neuPanel2.Controls.Add(this.lblCurRet);
            this.neuPanel2.Controls.Add(this.lblOper);
            this.neuPanel2.Controls.Add(this.neuLabel10);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Font = new System.Drawing.Font("宋体", 10F);
            this.neuPanel2.Location = new System.Drawing.Point(0, 99);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(860, 47);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 17;
            // 
            // lblCurPur
            // 
            this.lblCurPur.AutoSize = true;
            this.lblCurPur.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurPur.Location = new System.Drawing.Point(585, 30);
            this.lblCurPur.Name = "lblCurPur";
            this.lblCurPur.Size = new System.Drawing.Size(42, 14);
            this.lblCurPur.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCurPur.TabIndex = 14;
            this.lblCurPur.Text = "36.22";
            this.lblCurPur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurDif
            // 
            this.lblCurDif.AutoSize = true;
            this.lblCurDif.BackColor = System.Drawing.Color.White;
            this.lblCurDif.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurDif.Location = new System.Drawing.Point(723, 30);
            this.lblCurDif.Name = "lblCurDif";
            this.lblCurDif.Size = new System.Drawing.Size(42, 14);
            this.lblCurDif.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCurDif.TabIndex = 15;
            this.lblCurDif.Text = "36.22";
            this.lblCurDif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurRet
            // 
            this.lblCurRet.AutoSize = true;
            this.lblCurRet.BackColor = System.Drawing.Color.White;
            this.lblCurRet.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurRet.Location = new System.Drawing.Point(656, 30);
            this.lblCurRet.Name = "lblCurRet";
            this.lblCurRet.Size = new System.Drawing.Size(42, 14);
            this.lblCurRet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCurRet.TabIndex = 16;
            this.lblCurRet.Text = "36.22";
            this.lblCurRet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOper
            // 
            this.lblOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblOper.Location = new System.Drawing.Point(331, 3);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(150, 23);
            this.lblOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOper.TabIndex = 8;
            this.lblOper.Text = "制单人：";
            this.lblOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel10
            // 
            this.neuLabel10.BackColor = System.Drawing.Color.White;
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel10.Location = new System.Drawing.Point(14, 3);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(131, 23);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 10;
            this.neuLabel10.Text = "仓管：";
            this.neuLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.neuFpEnter1);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 73);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(860, 26);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 16;
            // 
            // neuFpEnter1
            // 
            this.neuFpEnter1.About = "3.0.2004.2005";
            this.neuFpEnter1.AccessibleDescription = "neuFpEnter1, Sheet1, Row 0, Column 0, 123";
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
            this.neuFpEnter1.Size = new System.Drawing.Size(860, 26);
            this.neuFpEnter1.TabIndex = 8;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuFpEnter1.TextTipAppearance = tipAppearance2;
            this.neuFpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuFpEnter1_Sheet1
            // 
            this.neuFpEnter1_Sheet1.Reset();
            this.neuFpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuFpEnter1_Sheet1.ColumnCount = 11;
            this.neuFpEnter1_Sheet1.RowCount = 6;
            this.neuFpEnter1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuFpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).Value = "123";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "药品代码";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "药品名称";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "药品规格";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "药房数量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "药品单位";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "药库数量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "药库进价";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "药库零价";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "进价金额";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "零价金额";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "进零差";
            this.neuFpEnter1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.neuFpEnter1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.neuFpEnter1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.Columns.Get(0).CellType = textCellType3;
            this.neuFpEnter1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Label = "药品代码";
            this.neuFpEnter1_Sheet1.Columns.Get(0).Width = 66F;
            this.neuFpEnter1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Label = "药品名称";
            this.neuFpEnter1_Sheet1.Columns.Get(1).Width = 131F;
            this.neuFpEnter1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Label = "药品规格";
            this.neuFpEnter1_Sheet1.Columns.Get(2).Width = 80F;
            this.neuFpEnter1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Label = "药房数量";
            this.neuFpEnter1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Width = 67F;
            this.neuFpEnter1_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.neuFpEnter1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Label = "药品单位";
            this.neuFpEnter1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Width = 66F;
            this.neuFpEnter1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Label = "药库数量";
            this.neuFpEnter1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Width = 66F;
            numberCellType6.DecimalPlaces = 4;
            this.neuFpEnter1_Sheet1.Columns.Get(6).CellType = numberCellType6;
            this.neuFpEnter1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Label = "药库进价";
            this.neuFpEnter1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Width = 79F;
            numberCellType7.DecimalPlaces = 4;
            this.neuFpEnter1_Sheet1.Columns.Get(7).CellType = numberCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Label = "药库零价";
            this.neuFpEnter1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Width = 84F;
            numberCellType8.DecimalPlaces = 2;
            this.neuFpEnter1_Sheet1.Columns.Get(8).CellType = numberCellType8;
            this.neuFpEnter1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Label = "进价金额";
            this.neuFpEnter1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Width = 69F;
            numberCellType9.DecimalPlaces = 2;
            this.neuFpEnter1_Sheet1.Columns.Get(9).CellType = numberCellType9;
            this.neuFpEnter1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Label = "零价金额";
            this.neuFpEnter1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Width = 79F;
            this.neuFpEnter1_Sheet1.Columns.Get(10).CellType = numberCellType10;
            this.neuFpEnter1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Label = "进零差";
            this.neuFpEnter1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Width = 64F;
            this.neuFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuFpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuFpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.lblInputDate);
            this.neuPanel3.Controls.Add(this.neuLabel1);
            this.neuPanel3.Controls.Add(this.neuLabel99);
            this.neuPanel3.Controls.Add(this.lblPage);
            this.neuPanel3.Controls.Add(this.lblBillID);
            this.neuPanel3.Controls.Add(this.lblTitle);
            this.neuPanel3.Controls.Add(this.lblCompany);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(860, 73);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 15;
            // 
            // lblInputDate
            // 
            this.lblInputDate.Font = new System.Drawing.Font("宋体", 10F);
            this.lblInputDate.Location = new System.Drawing.Point(417, 51);
            this.lblInputDate.Name = "lblInputDate";
            this.lblInputDate.Size = new System.Drawing.Size(209, 19);
            this.lblInputDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInputDate.TabIndex = 21;
            this.lblInputDate.Text = "内退日期：2014-05-19 09：00";
            this.lblInputDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotRetailCost
            // 
            this.lblTotRetailCost.AutoSize = true;
            this.lblTotRetailCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotRetailCost.Location = new System.Drawing.Point(657, 7);
            this.lblTotRetailCost.Name = "lblTotRetailCost";
            this.lblTotRetailCost.Size = new System.Drawing.Size(42, 14);
            this.lblTotRetailCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotRetailCost.TabIndex = 33;
            this.lblTotRetailCost.Text = "32.22";
            this.lblTotRetailCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotPurCost
            // 
            this.lblTotPurCost.AutoSize = true;
            this.lblTotPurCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotPurCost.Location = new System.Drawing.Point(585, 7);
            this.lblTotPurCost.Name = "lblTotPurCost";
            this.lblTotPurCost.Size = new System.Drawing.Size(42, 14);
            this.lblTotPurCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotPurCost.TabIndex = 32;
            this.lblTotPurCost.Text = "32.22";
            this.lblTotPurCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel1.Location = new System.Drawing.Point(622, 31);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 30;
            this.neuLabel1.Text = "零售金额：";
            this.neuLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel1.Visible = false;
            // 
            // neuLabel99
            // 
            this.neuLabel99.AutoSize = true;
            this.neuLabel99.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel99.Location = new System.Drawing.Point(620, 5);
            this.neuLabel99.Name = "neuLabel99";
            this.neuLabel99.Size = new System.Drawing.Size(77, 14);
            this.neuLabel99.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel99.TabIndex = 29;
            this.neuLabel99.Text = "买入金额：";
            this.neuLabel99.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel99.Visible = false;
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPage.Location = new System.Drawing.Point(692, 53);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(14, 14);
            this.lblPage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPage.TabIndex = 25;
            this.lblPage.Text = "/";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBillID
            // 
            this.lblBillID.Font = new System.Drawing.Font("宋体", 10F);
            this.lblBillID.Location = new System.Drawing.Point(193, 51);
            this.lblBillID.Name = "lblBillID";
            this.lblBillID.Size = new System.Drawing.Size(165, 19);
            this.lblBillID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBillID.TabIndex = 24;
            this.lblBillID.Text = "内退单号：201403170001";
            this.lblBillID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(271, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 16);
            this.lblTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTitle.TabIndex = 18;
            this.lblTitle.Text = "{0}药品内退单";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCompany.Location = new System.Drawing.Point(14, 51);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(209, 19);
            this.lblCompany.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCompany.TabIndex = 20;
            this.lblCompany.Text = "药品去向：急诊药房";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel2
            // 
            this.neuLabel2.BackColor = System.Drawing.Color.White;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel2.Location = new System.Drawing.Point(163, 3);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(131, 23);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 17;
            this.neuLabel2.Text = "会计：";
            this.neuLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel3.Location = new System.Drawing.Point(502, 30);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(77, 14);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 18;
            this.neuLabel3.Text = "本页合计：";
            this.neuLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel4.Location = new System.Drawing.Point(502, 7);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(77, 14);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 19;
            this.neuLabel4.Text = "单据累计：";
            this.neuLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotDif
            // 
            this.lblTotDif.AutoSize = true;
            this.lblTotDif.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotDif.Location = new System.Drawing.Point(723, 7);
            this.lblTotDif.Name = "lblTotDif";
            this.lblTotDif.Size = new System.Drawing.Size(42, 14);
            this.lblTotDif.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotDif.TabIndex = 34;
            this.lblTotDif.Text = "32.22";
            this.lblTotDif.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucPhaBackOutBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPhaBackOutBill";
            this.Size = new System.Drawing.Size(860, 149);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
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
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInputDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotRetailCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotPurCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel99;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCurPur;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCurDif;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCurRet;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblOper;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuFpEnter1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotDif;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
    }
}
