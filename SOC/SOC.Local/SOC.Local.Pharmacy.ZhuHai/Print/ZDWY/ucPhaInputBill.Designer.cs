namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    partial class ucPhaInputBill
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
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblTotDiff = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotRetail = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotPurCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbDrugFinOper = new System.Windows.Forms.Label();
            this.lblCurPur = new System.Windows.Forms.Label();
            this.lblCurDif = new System.Windows.Forms.Label();
            this.lblCurRet = new System.Windows.Forms.Label();
            this.lblOper = new System.Windows.Forms.Label();
            this.neuLabel9 = new System.Windows.Forms.Label();
            this.neuLabel10 = new System.Windows.Forms.Label();
            this.neuPanel5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbMemo = new System.Windows.Forms.Label();
            this.lblInputDate = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblBillID = new System.Windows.Forms.Label();
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
            this.neuPanel1.Size = new System.Drawing.Size(854, 142);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.lblTotDiff);
            this.neuPanel2.Controls.Add(this.lblTotRetail);
            this.neuPanel2.Controls.Add(this.lblTotPurCost);
            this.neuPanel2.Controls.Add(this.nlbDrugFinOper);
            this.neuPanel2.Controls.Add(this.lblCurPur);
            this.neuPanel2.Controls.Add(this.lblCurDif);
            this.neuPanel2.Controls.Add(this.lblCurRet);
            this.neuPanel2.Controls.Add(this.lblOper);
            this.neuPanel2.Controls.Add(this.neuLabel9);
            this.neuPanel2.Controls.Add(this.neuLabel10);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 78);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(854, 64);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 17;
            // 
            // lblTotDiff
            // 
            this.lblTotDiff.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotDiff.Location = new System.Drawing.Point(567, 19);
            this.lblTotDiff.Name = "lblTotDiff";
            this.lblTotDiff.Size = new System.Drawing.Size(177, 23);
            this.lblTotDiff.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotDiff.TabIndex = 36;
            this.lblTotDiff.Text = "进销差：";
            this.lblTotDiff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotRetail
            // 
            this.lblTotRetail.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotRetail.Location = new System.Drawing.Point(320, 21);
            this.lblTotRetail.Name = "lblTotRetail";
            this.lblTotRetail.Size = new System.Drawing.Size(216, 23);
            this.lblTotRetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotRetail.TabIndex = 35;
            this.lblTotRetail.Text = "零售金额：";
            this.lblTotRetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotPurCost
            // 
            this.lblTotPurCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotPurCost.Location = new System.Drawing.Point(26, 21);
            this.lblTotPurCost.Name = "lblTotPurCost";
            this.lblTotPurCost.Size = new System.Drawing.Size(226, 23);
            this.lblTotPurCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotPurCost.TabIndex = 34;
            this.lblTotPurCost.Text = "总合计：买入金额";
            this.lblTotPurCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nlbDrugFinOper
            // 
            this.nlbDrugFinOper.Font = new System.Drawing.Font("宋体", 10F);
            this.nlbDrugFinOper.Location = new System.Drawing.Point(578, 40);
            this.nlbDrugFinOper.Name = "nlbDrugFinOper";
            this.nlbDrugFinOper.Size = new System.Drawing.Size(115, 23);
            this.nlbDrugFinOper.TabIndex = 14;
            this.nlbDrugFinOper.Text = "财务会计：";
            this.nlbDrugFinOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurPur
            // 
            this.lblCurPur.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurPur.Location = new System.Drawing.Point(12, 1);
            this.lblCurPur.Name = "lblCurPur";
            this.lblCurPur.Size = new System.Drawing.Size(271, 23);
            this.lblCurPur.TabIndex = 11;
            this.lblCurPur.Text = "本页合计：买入金额：";
            this.lblCurPur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurDif
            // 
            this.lblCurDif.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurDif.Location = new System.Drawing.Point(567, 1);
            this.lblCurDif.Name = "lblCurDif";
            this.lblCurDif.Size = new System.Drawing.Size(218, 23);
            this.lblCurDif.TabIndex = 12;
            this.lblCurDif.Text = "进销差：";
            this.lblCurDif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurRet
            // 
            this.lblCurRet.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurRet.Location = new System.Drawing.Point(320, 1);
            this.lblCurRet.Name = "lblCurRet";
            this.lblCurRet.Size = new System.Drawing.Size(178, 23);
            this.lblCurRet.TabIndex = 13;
            this.lblCurRet.Text = "零售金额：";
            this.lblCurRet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOper
            // 
            this.lblOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblOper.Location = new System.Drawing.Point(12, 39);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(245, 23);
            this.lblOper.TabIndex = 8;
            this.lblOper.Text = "制单人：刘德华 2012-01-01 12:00:00";
            this.lblOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOper.Click += new System.EventHandler(this.lblOper_Click);
            // 
            // neuLabel9
            // 
            this.neuLabel9.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel9.Location = new System.Drawing.Point(421, 40);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(115, 23);
            this.neuLabel9.TabIndex = 9;
            this.neuLabel9.Text = "采购员签名：";
            this.neuLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel10
            // 
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel10.Location = new System.Drawing.Point(263, 40);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(178, 23);
            this.neuLabel10.TabIndex = 10;
            this.neuLabel10.Text = "仓管员签名：";
            this.neuLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel5
            // 
            this.neuPanel5.Controls.Add(this.neuFpEnter1);
            this.neuPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel5.Location = new System.Drawing.Point(0, 56);
            this.neuPanel5.Name = "neuPanel5";
            this.neuPanel5.Size = new System.Drawing.Size(854, 22);
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
            this.neuFpEnter1.Size = new System.Drawing.Size(854, 22);
            this.neuFpEnter1.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuFpEnter1.TextTipAppearance = tipAppearance1;
            this.neuFpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuFpEnter1_Sheet1
            // 
            this.neuFpEnter1_Sheet1.Reset();
            this.neuFpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuFpEnter1_Sheet1.ColumnCount = 12;
            this.neuFpEnter1_Sheet1.RowCount = 5;
            this.neuFpEnter1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuFpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatString = "n";
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).Value = 1234667890;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单位";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "产地";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "买入单价";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "买入金额";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.Transparent;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "零售单价";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "零售金额";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.Color.Transparent;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "进销差";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 11).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "批号";
            this.neuFpEnter1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Label = "编码";
            this.neuFpEnter1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Width = 65F;
            textCellType1.WordWrap = true;
            this.neuFpEnter1_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Label = "名称";
            this.neuFpEnter1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Width = 163F;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Label = "规格";
            this.neuFpEnter1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Width = 88F;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Label = "单位";
            this.neuFpEnter1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Width = 37F;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Label = "数量";
            this.neuFpEnter1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Width = 48F;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Label = "产地";
            this.neuFpEnter1_Sheet1.Columns.Get(5).Visible = false;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Width = 71F;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Label = "买入单价";
            this.neuFpEnter1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Width = 66F;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Label = "买入金额";
            this.neuFpEnter1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Width = 66F;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Label = "零售单价";
            this.neuFpEnter1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Width = 69F;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Label = "零售金额";
            this.neuFpEnter1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Width = 71F;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Label = "进销差";
            this.neuFpEnter1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Width = 57F;
            this.neuFpEnter1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(11).Label = "批号";
            this.neuFpEnter1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(11).Width = 73F;
            this.neuFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuFpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuFpEnter1_Sheet1.Rows.Default.Height = 26F;
            this.neuFpEnter1_Sheet1.Rows.Get(4).Height = 25F;
            this.neuFpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuPanel4);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(854, 56);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 15;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.nlbMemo);
            this.neuPanel4.Controls.Add(this.lblInputDate);
            this.neuPanel4.Controls.Add(this.lblPage);
            this.neuPanel4.Controls.Add(this.lblBillID);
            this.neuPanel4.Controls.Add(this.lblTitle);
            this.neuPanel4.Controls.Add(this.lblCompany);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(854, 56);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 0;
            // 
            // nlbMemo
            // 
            this.nlbMemo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbMemo.Location = new System.Drawing.Point(709, 9);
            this.nlbMemo.Name = "nlbMemo";
            this.nlbMemo.Size = new System.Drawing.Size(113, 42);
            this.nlbMemo.TabIndex = 43;
            this.nlbMemo.Text = "药品退货药品退货药品退货";
            this.nlbMemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInputDate
            // 
            this.lblInputDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInputDate.Location = new System.Drawing.Point(558, 36);
            this.lblInputDate.Name = "lblInputDate";
            this.lblInputDate.Size = new System.Drawing.Size(145, 19);
            this.lblInputDate.TabIndex = 38;
            this.lblInputDate.Text = "入库日期:2010-05-01";
            this.lblInputDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPage.Location = new System.Drawing.Point(628, 14);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(81, 17);
            this.lblPage.TabIndex = 42;
            this.lblPage.Text = "页码：1/1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBillID
            // 
            this.lblBillID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBillID.Location = new System.Drawing.Point(332, 36);
            this.lblBillID.Name = "lblBillID";
            this.lblBillID.Size = new System.Drawing.Size(204, 19);
            this.lblBillID.TabIndex = 41;
            this.lblBillID.Text = "入库单:1104020014191";
            this.lblBillID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(229, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(142, 19);
            this.lblTitle.TabIndex = 35;
            this.lblTitle.Text = "{0}药品入库单";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCompany.Location = new System.Drawing.Point(4, 36);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(333, 19);
            this.lblCompany.TabIndex = 37;
            this.lblCompany.Text = "送货单位：国药控股广州有限公司";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPhaInputBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPhaInputBill";
            this.Size = new System.Drawing.Size(854, 142);
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
        private System.Windows.Forms.Label neuLabel9;
        private System.Windows.Forms.Label neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel5;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuFpEnter1_Sheet1;
        private System.Windows.Forms.Label nlbDrugFinOper;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotDiff;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotRetail;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotPurCost;
        private System.Windows.Forms.Label nlbMemo;
    }
}
