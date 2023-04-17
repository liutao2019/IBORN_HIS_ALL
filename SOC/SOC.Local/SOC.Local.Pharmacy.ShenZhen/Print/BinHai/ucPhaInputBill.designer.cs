namespace FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCurPagePurchaseCost = new System.Windows.Forms.Label();
            this.lbCurPageSubCost = new System.Windows.Forms.Label();
            this.lbCurPageRetailCost = new System.Windows.Forms.Label();
            this.lbMadeBillOper = new System.Windows.Forms.Label();
            this.lbPlanOper = new System.Windows.Forms.Label();
            this.lbStockOper = new System.Windows.Forms.Label();
            this.neuPanel5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.socFpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.socFpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbTotSubCost = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblInputDate = new System.Windows.Forms.Label();
            this.lbTotRetailCost = new System.Windows.Forms.Label();
            this.lbTotPurchaseCost = new System.Windows.Forms.Label();
            this.lbBCost = new System.Windows.Forms.Label();
            this.lbMCost = new System.Windows.Forms.Label();
            this.lbDrugSubCost = new System.Windows.Forms.Label();
            this.lbBillNO = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbDrugPurchaseCost = new System.Windows.Forms.Label();
            this.lbDrugRetailCost = new System.Windows.Forms.Label();
            this.lbCompany = new System.Windows.Forms.Label();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.socFpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.socFpSpread1_Sheet1)).BeginInit();
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
            this.neuPanel1.Size = new System.Drawing.Size(804, 191);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.label3);
            this.neuPanel2.Controls.Add(this.label2);
            this.neuPanel2.Controls.Add(this.label1);
            this.neuPanel2.Controls.Add(this.lbCurPagePurchaseCost);
            this.neuPanel2.Controls.Add(this.lbCurPageSubCost);
            this.neuPanel2.Controls.Add(this.lbCurPageRetailCost);
            this.neuPanel2.Controls.Add(this.lbMadeBillOper);
            this.neuPanel2.Controls.Add(this.lbPlanOper);
            this.neuPanel2.Controls.Add(this.lbStockOper);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 112);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(804, 59);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(597, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "制表日期：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(139, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 23);
            this.label2.TabIndex = 15;
            this.label2.Text = "核对：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 23);
            this.label1.TabIndex = 14;
            this.label1.Text = "主管：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCurPagePurchaseCost
            // 
            this.lbCurPagePurchaseCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lbCurPagePurchaseCost.Location = new System.Drawing.Point(12, 3);
            this.lbCurPagePurchaseCost.Name = "lbCurPagePurchaseCost";
            this.lbCurPagePurchaseCost.Size = new System.Drawing.Size(179, 23);
            this.lbCurPagePurchaseCost.TabIndex = 11;
            this.lbCurPagePurchaseCost.Text = "本页买入金额：";
            this.lbCurPagePurchaseCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCurPageSubCost
            // 
            this.lbCurPageSubCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lbCurPageSubCost.Location = new System.Drawing.Point(585, 3);
            this.lbCurPageSubCost.Name = "lbCurPageSubCost";
            this.lbCurPageSubCost.Size = new System.Drawing.Size(167, 23);
            this.lbCurPageSubCost.TabIndex = 12;
            this.lbCurPageSubCost.Text = "本页购零差：";
            this.lbCurPageSubCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCurPageRetailCost
            // 
            this.lbCurPageRetailCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lbCurPageRetailCost.Location = new System.Drawing.Point(292, 3);
            this.lbCurPageRetailCost.Name = "lbCurPageRetailCost";
            this.lbCurPageRetailCost.Size = new System.Drawing.Size(178, 23);
            this.lbCurPageRetailCost.TabIndex = 13;
            this.lbCurPageRetailCost.Text = "本页零售金额：";
            this.lbCurPageRetailCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMadeBillOper
            // 
            this.lbMadeBillOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lbMadeBillOper.Location = new System.Drawing.Point(474, 26);
            this.lbMadeBillOper.Name = "lbMadeBillOper";
            this.lbMadeBillOper.Size = new System.Drawing.Size(112, 23);
            this.lbMadeBillOper.TabIndex = 8;
            this.lbMadeBillOper.Text = "制表人：";
            this.lbMadeBillOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbPlanOper
            // 
            this.lbPlanOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lbPlanOper.Location = new System.Drawing.Point(364, 26);
            this.lbPlanOper.Name = "lbPlanOper";
            this.lbPlanOper.Size = new System.Drawing.Size(113, 23);
            this.lbPlanOper.TabIndex = 9;
            this.lbPlanOper.Text = "采购：";
            this.lbPlanOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbStockOper
            // 
            this.lbStockOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lbStockOper.Location = new System.Drawing.Point(248, 26);
            this.lbStockOper.Name = "lbStockOper";
            this.lbStockOper.Size = new System.Drawing.Size(107, 23);
            this.lbStockOper.TabIndex = 10;
            this.lbStockOper.Text = "验收：";
            this.lbStockOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel5
            // 
            this.neuPanel5.Controls.Add(this.socFpSpread1);
            this.neuPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel5.Location = new System.Drawing.Point(0, 88);
            this.neuPanel5.Name = "neuPanel5";
            this.neuPanel5.Size = new System.Drawing.Size(804, 24);
            this.neuPanel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel5.TabIndex = 16;
            // 
            // socFpSpread1
            // 
            this.socFpSpread1.About = "3.0.2004.2005";
            this.socFpSpread1.AccessibleDescription = "socFpSpread1, Sheet1, Row 0, Column 0, ";
            this.socFpSpread1.BackColor = System.Drawing.Color.White;
            this.socFpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.socFpSpread1.EditModePermanent = true;
            this.socFpSpread1.EditModeReplace = true;
            this.socFpSpread1.Font = new System.Drawing.Font("宋体", 10F);
            this.socFpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.socFpSpread1.Location = new System.Drawing.Point(0, 0);
            this.socFpSpread1.Name = "socFpSpread1";
            this.socFpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.socFpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.socFpSpread1_Sheet1});
            this.socFpSpread1.Size = new System.Drawing.Size(804, 24);
            this.socFpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.socFpSpread1.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.socFpSpread1.TextTipAppearance = tipAppearance1;
            this.socFpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // socFpSpread1_Sheet1
            // 
            this.socFpSpread1_Sheet1.Reset();
            this.socFpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.socFpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.socFpSpread1_Sheet1.ColumnCount = 11;
            this.socFpSpread1_Sheet1.RowCount = 6;
            this.socFpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "产品ID";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单位";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "厂家";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "批号";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "有效期";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "购入单价";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "购入金额";
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "发票号";
            this.socFpSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.socFpSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.socFpSpread1_Sheet1.Columns.Get(0).Label = "产品ID";
            this.socFpSpread1_Sheet1.Columns.Get(0).Width = 52F;
            this.socFpSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.socFpSpread1_Sheet1.Columns.Get(1).Label = "名称";
            this.socFpSpread1_Sheet1.Columns.Get(1).Width = 131F;
            this.socFpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.socFpSpread1_Sheet1.Columns.Get(2).Label = "规格";
            this.socFpSpread1_Sheet1.Columns.Get(2).Width = 84F;
            this.socFpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.socFpSpread1_Sheet1.Columns.Get(3).Label = "单位";
            this.socFpSpread1_Sheet1.Columns.Get(3).Width = 37F;
            this.socFpSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.socFpSpread1_Sheet1.Columns.Get(4).Label = "数量";
            this.socFpSpread1_Sheet1.Columns.Get(4).Width = 42F;
            this.socFpSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.socFpSpread1_Sheet1.Columns.Get(5).Label = "厂家";
            this.socFpSpread1_Sheet1.Columns.Get(5).Width = 71F;
            this.socFpSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.socFpSpread1_Sheet1.Columns.Get(6).Label = "批号";
            this.socFpSpread1_Sheet1.Columns.Get(6).Width = 49F;
            this.socFpSpread1_Sheet1.Columns.Get(7).Label = "有效期";
            this.socFpSpread1_Sheet1.Columns.Get(7).Width = 83F;
            this.socFpSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.socFpSpread1_Sheet1.Columns.Get(8).Label = "购入单价";
            this.socFpSpread1_Sheet1.Columns.Get(8).Width = 66F;
            this.socFpSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.socFpSpread1_Sheet1.Columns.Get(9).Label = "购入金额";
            this.socFpSpread1_Sheet1.Columns.Get(9).Width = 100F;
            this.socFpSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.socFpSpread1_Sheet1.Columns.Get(10).Label = "发票号";
            this.socFpSpread1_Sheet1.Columns.Get(10).Width = 71F;
            this.socFpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.socFpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.socFpSpread1_Sheet1.RowHeader.Visible = false;
            this.socFpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.socFpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuPanel4);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(804, 88);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 15;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.lbTotSubCost);
            this.neuPanel4.Controls.Add(this.lblPage);
            this.neuPanel4.Controls.Add(this.lblInputDate);
            this.neuPanel4.Controls.Add(this.lbTotRetailCost);
            this.neuPanel4.Controls.Add(this.lbTotPurchaseCost);
            this.neuPanel4.Controls.Add(this.lbBCost);
            this.neuPanel4.Controls.Add(this.lbMCost);
            this.neuPanel4.Controls.Add(this.lbDrugSubCost);
            this.neuPanel4.Controls.Add(this.lbBillNO);
            this.neuPanel4.Controls.Add(this.lbTitle);
            this.neuPanel4.Controls.Add(this.lbDrugPurchaseCost);
            this.neuPanel4.Controls.Add(this.lbDrugRetailCost);
            this.neuPanel4.Controls.Add(this.lbCompany);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(804, 88);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 0;
            // 
            // lbTotSubCost
            // 
            this.lbTotSubCost.AutoSize = true;
            this.lbTotSubCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTotSubCost.Location = new System.Drawing.Point(596, 47);
            this.lbTotSubCost.Name = "lbTotSubCost";
            this.lbTotSubCost.Size = new System.Drawing.Size(91, 14);
            this.lbTotSubCost.TabIndex = 51;
            this.lbTotSubCost.Text = "总零购差额：";
            this.lbTotSubCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPage.Location = new System.Drawing.Point(698, 68);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(69, 17);
            this.lblPage.TabIndex = 42;
            this.lblPage.Text = "/";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInputDate
            // 
            this.lblInputDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInputDate.Location = new System.Drawing.Point(481, 64);
            this.lblInputDate.Name = "lblInputDate";
            this.lblInputDate.Size = new System.Drawing.Size(169, 19);
            this.lblInputDate.TabIndex = 38;
            this.lblInputDate.Text = "日期:2010-05-01";
            this.lblInputDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTotRetailCost
            // 
            this.lbTotRetailCost.AutoSize = true;
            this.lbTotRetailCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTotRetailCost.Location = new System.Drawing.Point(596, 28);
            this.lbTotRetailCost.Name = "lbTotRetailCost";
            this.lbTotRetailCost.Size = new System.Drawing.Size(91, 14);
            this.lbTotRetailCost.TabIndex = 50;
            this.lbTotRetailCost.Text = "总零售金额：";
            this.lbTotRetailCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTotPurchaseCost
            // 
            this.lbTotPurchaseCost.AutoSize = true;
            this.lbTotPurchaseCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTotPurchaseCost.Location = new System.Drawing.Point(596, 9);
            this.lbTotPurchaseCost.Name = "lbTotPurchaseCost";
            this.lbTotPurchaseCost.Size = new System.Drawing.Size(91, 14);
            this.lbTotPurchaseCost.TabIndex = 49;
            this.lbTotPurchaseCost.Text = "总购入金额：";
            this.lbTotPurchaseCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbBCost
            // 
            this.lbBCost.BackColor = System.Drawing.Color.Orange;
            this.lbBCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbBCost.Location = new System.Drawing.Point(439, 34);
            this.lbBCost.Name = "lbBCost";
            this.lbBCost.Size = new System.Drawing.Size(147, 23);
            this.lbBCost.TabIndex = 45;
            this.lbBCost.Text = "卫生材料：";
            this.lbBCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbBCost.Visible = false;
            // 
            // lbMCost
            // 
            this.lbMCost.BackColor = System.Drawing.Color.Orange;
            this.lbMCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMCost.Location = new System.Drawing.Point(332, 30);
            this.lbMCost.Name = "lbMCost";
            this.lbMCost.Size = new System.Drawing.Size(159, 23);
            this.lbMCost.TabIndex = 44;
            this.lbMCost.Text = "原材料：";
            this.lbMCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbMCost.Visible = false;
            // 
            // lbDrugSubCost
            // 
            this.lbDrugSubCost.BackColor = System.Drawing.Color.Gold;
            this.lbDrugSubCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDrugSubCost.Location = new System.Drawing.Point(277, 36);
            this.lbDrugSubCost.Name = "lbDrugSubCost";
            this.lbDrugSubCost.Size = new System.Drawing.Size(151, 23);
            this.lbDrugSubCost.TabIndex = 39;
            this.lbDrugSubCost.Text = "药品差：";
            this.lbDrugSubCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbDrugSubCost.Visible = false;
            // 
            // lbBillNO
            // 
            this.lbBillNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbBillNO.Location = new System.Drawing.Point(301, 64);
            this.lbBillNO.Name = "lbBillNO";
            this.lbBillNO.Size = new System.Drawing.Size(208, 19);
            this.lbBillNO.TabIndex = 41;
            this.lbBillNO.Text = "单号：20100402001(4191)";
            this.lbBillNO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(229, 11);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(147, 20);
            this.lbTitle.TabIndex = 35;
            this.lbTitle.Text = "{0}药品入库单";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDrugPurchaseCost
            // 
            this.lbDrugPurchaseCost.BackColor = System.Drawing.Color.Gold;
            this.lbDrugPurchaseCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDrugPurchaseCost.Location = new System.Drawing.Point(141, 36);
            this.lbDrugPurchaseCost.Name = "lbDrugPurchaseCost";
            this.lbDrugPurchaseCost.Size = new System.Drawing.Size(131, 23);
            this.lbDrugPurchaseCost.TabIndex = 40;
            this.lbDrugPurchaseCost.Text = "药品购额：";
            this.lbDrugPurchaseCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbDrugPurchaseCost.Visible = false;
            // 
            // lbDrugRetailCost
            // 
            this.lbDrugRetailCost.BackColor = System.Drawing.Color.Gold;
            this.lbDrugRetailCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDrugRetailCost.Location = new System.Drawing.Point(7, 36);
            this.lbDrugRetailCost.Name = "lbDrugRetailCost";
            this.lbDrugRetailCost.Size = new System.Drawing.Size(128, 23);
            this.lbDrugRetailCost.TabIndex = 36;
            this.lbDrugRetailCost.Text = "药品零额：";
            this.lbDrugRetailCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbDrugRetailCost.Visible = false;
            // 
            // lbCompany
            // 
            this.lbCompany.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCompany.Location = new System.Drawing.Point(7, 64);
            this.lbCompany.Name = "lbCompany";
            this.lbCompany.Size = new System.Drawing.Size(279, 19);
            this.lbCompany.TabIndex = 37;
            this.lbCompany.Text = "送货单位：";
            this.lbCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPhaInputBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPhaInputBill";
            this.Size = new System.Drawing.Size(804, 191);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.socFpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.socFpSpread1_Sheet1)).EndInit();
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private System.Windows.Forms.Label lbTotSubCost;
        private System.Windows.Forms.Label lbTotRetailCost;
        private System.Windows.Forms.Label lbTotPurchaseCost;
        private System.Windows.Forms.Label lbBCost;
        private System.Windows.Forms.Label lbMCost;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lbDrugSubCost;
        private System.Windows.Forms.Label lbBillNO;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbDrugPurchaseCost;
        private System.Windows.Forms.Label lbDrugRetailCost;
        private System.Windows.Forms.Label lbCompany;
        private System.Windows.Forms.Label lblInputDate;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.Label lbCurPagePurchaseCost;
        private System.Windows.Forms.Label lbCurPageSubCost;
        private System.Windows.Forms.Label lbCurPageRetailCost;
        private System.Windows.Forms.Label lbMadeBillOper;
        private System.Windows.Forms.Label lbPlanOper;
        private System.Windows.Forms.Label lbStockOper;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel5;
        private FS.SOC.Windows.Forms.FpSpread socFpSpread1;
        private FarPoint.Win.Spread.SheetView socFpSpread1_Sheet1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
