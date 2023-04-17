namespace FS.SOC.Local.Pharmacy.Print.ZDLY
{
    partial class ucPhaBackInBill
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
            this.lblInputTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotDiff = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotRetail = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTotPurCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbCustody = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAccount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDirector = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbApproval = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPurhance = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblRecord = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPrintDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPage = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblStorageDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblBillID = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCompany = new FS.FrameWork.WinForms.Controls.NeuLabel();
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
            this.neuPanel1.Size = new System.Drawing.Size(806, 210);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.lblInputTime);
            this.neuPanel2.Controls.Add(this.lblTotDiff);
            this.neuPanel2.Controls.Add(this.lblTotRetail);
            this.neuPanel2.Controls.Add(this.lblTotPurCost);
            this.neuPanel2.Controls.Add(this.lbCustody);
            this.neuPanel2.Controls.Add(this.lbAccount);
            this.neuPanel2.Controls.Add(this.lbDirector);
            this.neuPanel2.Controls.Add(this.lbApproval);
            this.neuPanel2.Controls.Add(this.lblPurhance);
            this.neuPanel2.Controls.Add(this.lblRecord);
            this.neuPanel2.Controls.Add(this.neuLabel6);
            this.neuPanel2.Controls.Add(this.lblOper);
            this.neuPanel2.Controls.Add(this.lblPrintDate);
            this.neuPanel2.Controls.Add(this.neuLabel10);
            this.neuPanel2.Controls.Add(this.lblPage);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 107);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(806, 100);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 18;
            // 
            // lblInputTime
            // 
            this.lblInputTime.Font = new System.Drawing.Font("宋体", 10F);
            this.lblInputTime.Location = new System.Drawing.Point(69, 62);
            this.lblInputTime.Name = "lblInputTime";
            this.lblInputTime.Size = new System.Drawing.Size(197, 23);
            this.lblInputTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInputTime.TabIndex = 34;
            this.lblInputTime.Text = "入库日期：";
            this.lblInputTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotDiff
            // 
            this.lblTotDiff.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotDiff.Location = new System.Drawing.Point(494, 1);
            this.lblTotDiff.Name = "lblTotDiff";
            this.lblTotDiff.Size = new System.Drawing.Size(138, 23);
            this.lblTotDiff.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotDiff.TabIndex = 33;
            this.lblTotDiff.Text = "进销差额：";
            this.lblTotDiff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotRetail
            // 
            this.lblTotRetail.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotRetail.Location = new System.Drawing.Point(318, 1);
            this.lblTotRetail.Name = "lblTotRetail";
            this.lblTotRetail.Size = new System.Drawing.Size(152, 23);
            this.lblTotRetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotRetail.TabIndex = 32;
            this.lblTotRetail.Text = "零售金额：";
            this.lblTotRetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotPurCost
            // 
            this.lblTotPurCost.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTotPurCost.Location = new System.Drawing.Point(98, 1);
            this.lblTotPurCost.Name = "lblTotPurCost";
            this.lblTotPurCost.Size = new System.Drawing.Size(168, 23);
            this.lblTotPurCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotPurCost.TabIndex = 31;
            this.lblTotPurCost.Text = "购进金额：";
            this.lblTotPurCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCustody
            // 
            this.lbCustody.Font = new System.Drawing.Font("宋体", 10F);
            this.lbCustody.Location = new System.Drawing.Point(508, 37);
            this.lbCustody.Name = "lbCustody";
            this.lbCustody.Size = new System.Drawing.Size(124, 23);
            this.lbCustody.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCustody.TabIndex = 30;
            this.lbCustody.Text = "保管：";
            this.lbCustody.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAccount
            // 
            this.lbAccount.Font = new System.Drawing.Font("宋体", 10F);
            this.lbAccount.Location = new System.Drawing.Point(380, 39);
            this.lbAccount.Name = "lbAccount";
            this.lbAccount.Size = new System.Drawing.Size(124, 23);
            this.lbAccount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAccount.TabIndex = 29;
            this.lbAccount.Text = "会计：";
            this.lbAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDirector
            // 
            this.lbDirector.Font = new System.Drawing.Font("宋体", 10F);
            this.lbDirector.Location = new System.Drawing.Point(252, 39);
            this.lbDirector.Name = "lbDirector";
            this.lbDirector.Size = new System.Drawing.Size(124, 23);
            this.lbDirector.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDirector.TabIndex = 28;
            this.lbDirector.Text = "药剂科主任：";
            this.lbDirector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbApproval
            // 
            this.lbApproval.Font = new System.Drawing.Font("宋体", 10F);
            this.lbApproval.Location = new System.Drawing.Point(124, 39);
            this.lbApproval.Name = "lbApproval";
            this.lbApproval.Size = new System.Drawing.Size(124, 23);
            this.lbApproval.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbApproval.TabIndex = 27;
            this.lbApproval.Text = "审批：";
            this.lbApproval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPurhance
            // 
            this.lblPurhance.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPurhance.Location = new System.Drawing.Point(185, 20);
            this.lblPurhance.Name = "lblPurhance";
            this.lblPurhance.Size = new System.Drawing.Size(124, 23);
            this.lblPurhance.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPurhance.TabIndex = 26;
            this.lblPurhance.Text = "采购：";
            this.lblPurhance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecord
            // 
            this.lblRecord.Font = new System.Drawing.Font("宋体", 10F);
            this.lblRecord.Location = new System.Drawing.Point(476, 20);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(124, 23);
            this.lblRecord.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblRecord.TabIndex = 15;
            this.lblRecord.Text = "录单：";
            this.lblRecord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel6
            // 
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel6.Location = new System.Drawing.Point(333, 20);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(119, 23);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 14;
            this.neuLabel6.Text = "审核：";
            this.neuLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOper
            // 
            this.lblOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblOper.Location = new System.Drawing.Point(624, 20);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(122, 23);
            this.lblOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOper.TabIndex = 8;
            this.lblOper.Text = "制表：";
            this.lblOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrintDate
            // 
            this.lblPrintDate.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPrintDate.Location = new System.Drawing.Point(337, 62);
            this.lblPrintDate.Name = "lblPrintDate";
            this.lblPrintDate.Size = new System.Drawing.Size(196, 23);
            this.lblPrintDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPrintDate.TabIndex = 9;
            this.lblPrintDate.Text = "打印日期：";
            this.lblPrintDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel10
            // 
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel10.Location = new System.Drawing.Point(39, 20);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(124, 23);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 10;
            this.neuLabel10.Text = "验收：";
            this.neuLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPage.Location = new System.Drawing.Point(604, 62);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(84, 23);
            this.lblPage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPage.TabIndex = 25;
            this.lblPage.Text = "/";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.neuFpEnter1);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 63);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(806, 44);
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
            this.neuFpEnter1.Size = new System.Drawing.Size(806, 44);
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
            this.neuFpEnter1_Sheet1.ColumnCount = 11;
            this.neuFpEnter1_Sheet1.RowCount = 1;
            this.neuFpEnter1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuFpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "生产厂家";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "批号";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "数量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "买入单价";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "买入金额";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "零售单价";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "退货金额";
            this.neuFpEnter1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.neuFpEnter1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.neuFpEnter1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Label = "编码";
            this.neuFpEnter1_Sheet1.Columns.Get(0).Width = 49F;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Label = "名称";
            this.neuFpEnter1_Sheet1.Columns.Get(1).Width = 125F;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Label = "规格";
            this.neuFpEnter1_Sheet1.Columns.Get(2).Width = 73F;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Label = "生产厂家";
            this.neuFpEnter1_Sheet1.Columns.Get(3).Width = 79F;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Label = "批号";
            this.neuFpEnter1_Sheet1.Columns.Get(4).Width = 70F;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Label = "单位";
            this.neuFpEnter1_Sheet1.Columns.Get(5).Width = 43F;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Label = "数量";
            this.neuFpEnter1_Sheet1.Columns.Get(6).Width = 64F;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Label = "买入单价";
            this.neuFpEnter1_Sheet1.Columns.Get(7).Width = 65F;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Label = "买入金额";
            this.neuFpEnter1_Sheet1.Columns.Get(8).Width = 68F;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Label = "零售单价";
            this.neuFpEnter1_Sheet1.Columns.Get(9).Width = 71F;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Label = "退货金额";
            this.neuFpEnter1_Sheet1.Columns.Get(10).Width = 71F;
            this.neuFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuFpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuFpEnter1_Sheet1.Rows.Default.Height = 32F;
            this.neuFpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.lblStorageDept);
            this.neuPanel3.Controls.Add(this.lblBillID);
            this.neuPanel3.Controls.Add(this.lblTitle);
            this.neuPanel3.Controls.Add(this.lblCompany);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(806, 63);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 15;
            // 
            // lblStorageDept
            // 
            this.lblStorageDept.Font = new System.Drawing.Font("宋体", 10F);
            this.lblStorageDept.Location = new System.Drawing.Point(208, 33);
            this.lblStorageDept.Name = "lblStorageDept";
            this.lblStorageDept.Size = new System.Drawing.Size(186, 19);
            this.lblStorageDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblStorageDept.TabIndex = 44;
            this.lblStorageDept.Text = "接收部门：";
            this.lblStorageDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBillID
            // 
            this.lblBillID.Font = new System.Drawing.Font("宋体", 10F);
            this.lblBillID.Location = new System.Drawing.Point(12, 33);
            this.lblBillID.Name = "lblBillID";
            this.lblBillID.Size = new System.Drawing.Size(178, 19);
            this.lblBillID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBillID.TabIndex = 24;
            this.lblBillID.Text = "编号：";
            this.lblBillID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(286, 7);
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
            this.lblCompany.Location = new System.Drawing.Point(400, 33);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(372, 19);
            this.lblCompany.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCompany.TabIndex = 20;
            this.lblCompany.Text = "发出部门：";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPhaBackInBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPhaBackInBill";
            this.Size = new System.Drawing.Size(806, 210);
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
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPrintDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuFpEnter1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblRecord;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblStorageDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPurhance;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbAccount;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDirector;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbApproval;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCustody;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotDiff;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotRetail;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotPurCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInputTime;
    }
}
