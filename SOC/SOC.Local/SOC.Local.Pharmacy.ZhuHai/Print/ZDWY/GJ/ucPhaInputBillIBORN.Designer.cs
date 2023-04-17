namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.GJ
{
    partial class ucPhaInputBillIBORN
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
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotRetail = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbDrugFinOper = new System.Windows.Forms.Label();
            this.lblCurDif = new System.Windows.Forms.Label();
            this.lblCurRet = new System.Windows.Forms.Label();
            this.lblOper = new System.Windows.Forms.Label();
            this.lblBuyer = new System.Windows.Forms.Label();
            this.lblStoreOper = new System.Windows.Forms.Label();
            this.lblTotDiff = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCurPur = new System.Windows.Forms.Label();
            this.lblTotPurCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblBillID = new System.Windows.Forms.Label();
            this.lblPrintDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbMemo = new System.Windows.Forms.Label();
            this.lblInputDate = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1_Sheet1)).BeginInit();
            this.neuPanel3.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Controls.Add(this.neuPanel5);
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(800, 168);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.label1);
            this.neuPanel2.Controls.Add(this.lblTotRetail);
            this.neuPanel2.Controls.Add(this.nlbDrugFinOper);
            this.neuPanel2.Controls.Add(this.lblCurDif);
            this.neuPanel2.Controls.Add(this.lblCurRet);
            this.neuPanel2.Controls.Add(this.lblOper);
            this.neuPanel2.Controls.Add(this.lblBuyer);
            this.neuPanel2.Controls.Add(this.lblStoreOper);
            this.neuPanel2.Controls.Add(this.lblTotDiff);
            this.neuPanel2.Controls.Add(this.lblCurPur);
            this.neuPanel2.Controls.Add(this.lblTotPurCost);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 101);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(800, 64);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(494, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 23);
            this.label1.TabIndex = 37;
            this.label1.Text = "入库人：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotRetail
            // 
            this.lblTotRetail.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotRetail.Location = new System.Drawing.Point(533, 21);
            this.lblTotRetail.Name = "lblTotRetail";
            this.lblTotRetail.Size = new System.Drawing.Size(250, 23);
            this.lblTotRetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotRetail.TabIndex = 35;
            this.lblTotRetail.Text = "总合计：售价总金额：1234567890.11";
            this.lblTotRetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nlbDrugFinOper
            // 
            this.nlbDrugFinOper.Font = new System.Drawing.Font("宋体", 10F);
            this.nlbDrugFinOper.Location = new System.Drawing.Point(633, 40);
            this.nlbDrugFinOper.Name = "nlbDrugFinOper";
            this.nlbDrugFinOper.Size = new System.Drawing.Size(115, 23);
            this.nlbDrugFinOper.TabIndex = 14;
            this.nlbDrugFinOper.Text = "批准人：";
            this.nlbDrugFinOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurDif
            // 
            this.lblCurDif.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurDif.Location = new System.Drawing.Point(259, 1);
            this.lblCurDif.Name = "lblCurDif";
            this.lblCurDif.Size = new System.Drawing.Size(218, 23);
            this.lblCurDif.TabIndex = 12;
            this.lblCurDif.Text = "进销差：";
            this.lblCurDif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurRet
            // 
            this.lblCurRet.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurRet.Location = new System.Drawing.Point(533, 1);
            this.lblCurRet.Name = "lblCurRet";
            this.lblCurRet.Size = new System.Drawing.Size(250, 23);
            this.lblCurRet.TabIndex = 13;
            this.lblCurRet.Text = "本页合计：售价金额：1234567890.11";
            this.lblCurRet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOper
            // 
            this.lblOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblOper.Location = new System.Drawing.Point(10, 39);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(166, 23);
            this.lblOper.TabIndex = 8;
            this.lblOper.Text = "制单人：刘德华";
            this.lblOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOper.Click += new System.EventHandler(this.lblOper_Click);
            // 
            // lblBuyer
            // 
            this.lblBuyer.Font = new System.Drawing.Font("宋体", 10F);
            this.lblBuyer.Location = new System.Drawing.Point(360, 40);
            this.lblBuyer.Name = "lblBuyer";
            this.lblBuyer.Size = new System.Drawing.Size(115, 23);
            this.lblBuyer.TabIndex = 9;
            this.lblBuyer.Text = "采购员：刘德华";
            this.lblBuyer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStoreOper
            // 
            this.lblStoreOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblStoreOper.Location = new System.Drawing.Point(173, 40);
            this.lblStoreOper.Name = "lblStoreOper";
            this.lblStoreOper.Size = new System.Drawing.Size(178, 23);
            this.lblStoreOper.TabIndex = 10;
            this.lblStoreOper.Text = "仓管员：刘德华";
            this.lblStoreOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotDiff
            // 
            this.lblTotDiff.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotDiff.Location = new System.Drawing.Point(259, 17);
            this.lblTotDiff.Name = "lblTotDiff";
            this.lblTotDiff.Size = new System.Drawing.Size(177, 23);
            this.lblTotDiff.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotDiff.TabIndex = 36;
            this.lblTotDiff.Text = "进销差：";
            this.lblTotDiff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurPur
            // 
            this.lblCurPur.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurPur.Location = new System.Drawing.Point(533, 1);
            this.lblCurPur.Name = "lblCurPur";
            this.lblCurPur.Size = new System.Drawing.Size(250, 23);
            this.lblCurPur.TabIndex = 11;
            this.lblCurPur.Text = "本页合计：购入金额：1234567890.11";
            this.lblCurPur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotPurCost
            // 
            this.lblTotPurCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotPurCost.Location = new System.Drawing.Point(533, 21);
            this.lblTotPurCost.Name = "lblTotPurCost";
            this.lblTotPurCost.Size = new System.Drawing.Size(250, 23);
            this.lblTotPurCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotPurCost.TabIndex = 34;
            this.lblTotPurCost.Text = "总合计：购入总金额：1234567890";
            this.lblTotPurCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel5
            // 
            this.neuPanel5.Controls.Add(this.neuFpEnter1);
            this.neuPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel5.Location = new System.Drawing.Point(0, 66);
            this.neuPanel5.Name = "neuPanel5";
            this.neuPanel5.Size = new System.Drawing.Size(800, 35);
            this.neuPanel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel5.TabIndex = 16;
            // 
            // neuFpEnter1
            // 
            this.neuFpEnter1.About = "3.0.2004.2005";
            this.neuFpEnter1.AccessibleDescription = "neuFpEnter1, Sheet1, Row 0, Column 0, 1234667890";
            this.neuFpEnter1.BackColor = System.Drawing.Color.White;
            this.neuFpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuFpEnter1.EditModePermanent = true;
            this.neuFpEnter1.EditModeReplace = true;
            this.neuFpEnter1.Font = new System.Drawing.Font("宋体", 10F);
            this.neuFpEnter1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.neuFpEnter1.Location = new System.Drawing.Point(0, 0);
            this.neuFpEnter1.Name = "neuFpEnter1";
            this.neuFpEnter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuFpEnter1.SelectNone = false;
            this.neuFpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuFpEnter1_Sheet1});
            this.neuFpEnter1.ShowListWhenOfFocus = false;
            this.neuFpEnter1.Size = new System.Drawing.Size(800, 35);
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
            this.neuFpEnter1_Sheet1.ColumnCount = 11;
            this.neuFpEnter1_Sheet1.RowCount = 5;
            this.neuFpEnter1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuFpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatString = "n";
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).Value = "1";
            this.neuFpEnter1_Sheet1.Cells.Get(0, 2).Value = "哈哈哈哈哈哈哈哈哈哈哈";
            this.neuFpEnter1_Sheet1.Cells.Get(0, 3).Value = "哈哈哈哈规格";
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
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "产地";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "药库单位";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "购入单价(元)";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.Transparent;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "购入金额(元)";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "发票号";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "备注";
            this.neuFpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuFpEnter1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            textCellType7.Multiline = true;
            textCellType7.WordWrap = true;
            this.neuFpEnter1_Sheet1.Columns.Get(0).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Label = "序号";
            this.neuFpEnter1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Width = 29F;
            this.neuFpEnter1_Sheet1.Columns.Get(1).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Label = "名称";
            this.neuFpEnter1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Width = 210F;
            this.neuFpEnter1_Sheet1.Columns.Get(2).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Label = "规格";
            this.neuFpEnter1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Width = 110F;
            this.neuFpEnter1_Sheet1.Columns.Get(3).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Label = "批号";
            this.neuFpEnter1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Width = 68F;
            this.neuFpEnter1_Sheet1.Columns.Get(4).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Label = "数量";
            this.neuFpEnter1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Width = 48F;
            this.neuFpEnter1_Sheet1.Columns.Get(5).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Label = "产地";
            this.neuFpEnter1_Sheet1.Columns.Get(5).Visible = false;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Width = 71F;
            this.neuFpEnter1_Sheet1.Columns.Get(6).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Label = "药库单位";
            this.neuFpEnter1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Width = 43F;
            this.neuFpEnter1_Sheet1.Columns.Get(7).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Label = "购入单价(元)";
            this.neuFpEnter1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Width = 65F;
            this.neuFpEnter1_Sheet1.Columns.Get(8).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Label = "购入金额(元)";
            this.neuFpEnter1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Width = 70F;
            this.neuFpEnter1_Sheet1.Columns.Get(9).CellType = textCellType7;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Label = "发票号";
            this.neuFpEnter1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Width = 70F;
            textCellType8.Multiline = true;
            textCellType8.WordWrap = true;
            this.neuFpEnter1_Sheet1.Columns.Get(10).CellType = textCellType8;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 9F);
            this.neuFpEnter1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Label = "备注";
            this.neuFpEnter1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Width = 68F;
            this.neuFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuFpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuFpEnter1_Sheet1.Rows.Default.Height = 35F;
            this.neuFpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuPanel4);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(800, 66);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 15;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.lblBillID);
            this.neuPanel4.Controls.Add(this.lblPrintDate);
            this.neuPanel4.Controls.Add(this.nlbMemo);
            this.neuPanel4.Controls.Add(this.lblInputDate);
            this.neuPanel4.Controls.Add(this.lblPage);
            this.neuPanel4.Controls.Add(this.lblTitle);
            this.neuPanel4.Controls.Add(this.lblCompany);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(800, 64);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 0;
            // 
            // lblBillID
            // 
            this.lblBillID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBillID.Location = new System.Drawing.Point(368, 44);
            this.lblBillID.Name = "lblBillID";
            this.lblBillID.Size = new System.Drawing.Size(204, 19);
            this.lblBillID.TabIndex = 41;
            this.lblBillID.Text = "入库单号:1104020014191";
            this.lblBillID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrintDate
            // 
            this.lblPrintDate.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblPrintDate.Location = new System.Drawing.Point(4, 21);
            this.lblPrintDate.Name = "lblPrintDate";
            this.lblPrintDate.Size = new System.Drawing.Size(226, 23);
            this.lblPrintDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPrintDate.TabIndex = 44;
            this.lblPrintDate.Text = "制单日期：2017-10-30 09:29:56";
            this.lblPrintDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nlbMemo
            // 
            this.nlbMemo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbMemo.Location = new System.Drawing.Point(684, 25);
            this.nlbMemo.Name = "nlbMemo";
            this.nlbMemo.Size = new System.Drawing.Size(113, 42);
            this.nlbMemo.TabIndex = 43;
            this.nlbMemo.Text = "药品退货药品退货药品退货";
            this.nlbMemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInputDate
            // 
            this.lblInputDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInputDate.Location = new System.Drawing.Point(368, 25);
            this.lblInputDate.Name = "lblInputDate";
            this.lblInputDate.Size = new System.Drawing.Size(145, 19);
            this.lblInputDate.TabIndex = 38;
            this.lblInputDate.Text = "入库日期:2010-05-01 10:30:24";
            this.lblInputDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPage.Location = new System.Drawing.Point(694, 4);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(81, 17);
            this.lblPage.TabIndex = 42;
            this.lblPage.Text = "页码：1/1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(229, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(142, 19);
            this.lblTitle.TabIndex = 35;
            this.lblTitle.Text = "{0}药品入库单";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCompany.Location = new System.Drawing.Point(4, 44);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(333, 19);
            this.lblCompany.TabIndex = 37;
            this.lblCompany.Text = "送货单位：国药控股广州有限公司";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPhaInputBillIBORN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPhaInputBillIBORN";
            this.Size = new System.Drawing.Size(800, 168);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1_Sheet1)).EndInit();
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lblBillID;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblInputDate;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.Label lblCurPur;
        private System.Windows.Forms.Label lblCurDif;
        private System.Windows.Forms.Label lblCurRet;
        private System.Windows.Forms.Label lblOper;
        private System.Windows.Forms.Label lblBuyer;
        private System.Windows.Forms.Label lblStoreOper;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel5;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuFpEnter1_Sheet1;
        private System.Windows.Forms.Label nlbDrugFinOper;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotDiff;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotRetail;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotPurCost;
        private System.Windows.Forms.Label nlbMemo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPrintDate;
        private System.Windows.Forms.Label label1;
    }
}
