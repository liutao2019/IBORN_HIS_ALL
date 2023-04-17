namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    partial class ucPhaApplyInputBill
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOper = new System.Windows.Forms.Label();
            this.neuLabel9 = new System.Windows.Forms.Label();
            this.neuLabel10 = new System.Windows.Forms.Label();
            this.neuPanel5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblApplyDept = new System.Windows.Forms.Label();
            this.neuLabel2 = new System.Windows.Forms.Label();
            this.lblTotDif = new System.Windows.Forms.Label();
            this.lblInputDate = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblTotRetailCost = new System.Windows.Forms.Label();
            this.lblTotPurCost = new System.Windows.Forms.Label();
            this.neuLabel1 = new System.Windows.Forms.Label();
            this.neuLabel99 = new System.Windows.Forms.Label();
            this.lblBillID = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTargetDept = new System.Windows.Forms.Label();
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
            this.neuPanel1.Size = new System.Drawing.Size(854, 122);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.label1);
            this.neuPanel2.Controls.Add(this.lblOper);
            this.neuPanel2.Controls.Add(this.neuLabel9);
            this.neuPanel2.Controls.Add(this.neuLabel10);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 92);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(854, 28);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(184, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "会计:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOper
            // 
            this.lblOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblOper.Location = new System.Drawing.Point(369, 3);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(186, 23);
            this.lblOper.TabIndex = 8;
            this.lblOper.Text = "制单人:";
            this.lblOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOper.Click += new System.EventHandler(this.lblOper_Click);
            // 
            // neuLabel9
            // 
            this.neuLabel9.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel9.Location = new System.Drawing.Point(561, 3);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(115, 23);
            this.neuLabel9.TabIndex = 9;
            this.neuLabel9.Text = "申请人：";
            this.neuLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel10
            // 
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel10.Location = new System.Drawing.Point(7, 3);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(81, 23);
            this.neuLabel10.TabIndex = 10;
            this.neuLabel10.Text = "仓管：";
            this.neuLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel5
            // 
            this.neuPanel5.Controls.Add(this.neuFpEnter1);
            this.neuPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel5.Location = new System.Drawing.Point(0, 68);
            this.neuPanel5.Name = "neuPanel5";
            this.neuPanel5.Size = new System.Drawing.Size(854, 24);
            this.neuPanel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel5.TabIndex = 16;
            // 
            // neuFpEnter1
            // 
            this.neuFpEnter1.About = "3.0.2004.2005";
            this.neuFpEnter1.AccessibleDescription = "neuFpEnter1, Sheet1, Row 0, Column 0, 3214101";
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
            this.neuFpEnter1.Size = new System.Drawing.Size(854, 24);
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
            this.neuFpEnter1_Sheet1.ColumnCount = 9;
            this.neuFpEnter1_Sheet1.RowCount = 6;
            this.neuFpEnter1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuFpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).ParseFormatString = "n";
            this.neuFpEnter1_Sheet1.Cells.Get(0, 0).Value = 3214101;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "药品代码";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "药品名称";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "药房申请数量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "产地";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "药库数量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "药品单位";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.Transparent;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "药库确认";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "序号";
            this.neuFpEnter1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Label = "药品代码";
            this.neuFpEnter1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Width = 84F;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Label = "药品名称";
            this.neuFpEnter1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Width = 168F;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Label = "规格";
            this.neuFpEnter1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Width = 110F;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Label = "药房申请数量";
            this.neuFpEnter1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Width = 96F;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Label = "产地";
            this.neuFpEnter1_Sheet1.Columns.Get(4).Visible = false;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Width = 71F;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Label = "药库数量";
            this.neuFpEnter1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Width = 68F;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Label = "药品单位";
            this.neuFpEnter1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Width = 66F;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Label = "药库确认";
            this.neuFpEnter1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Width = 71F;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Label = "序号";
            this.neuFpEnter1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Width = 76F;
            this.neuFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuFpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuFpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuPanel4);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(854, 68);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 15;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.lblApplyDept);
            this.neuPanel4.Controls.Add(this.neuLabel2);
            this.neuPanel4.Controls.Add(this.lblTotDif);
            this.neuPanel4.Controls.Add(this.lblInputDate);
            this.neuPanel4.Controls.Add(this.lblPage);
            this.neuPanel4.Controls.Add(this.lblTotRetailCost);
            this.neuPanel4.Controls.Add(this.lblTotPurCost);
            this.neuPanel4.Controls.Add(this.neuLabel1);
            this.neuPanel4.Controls.Add(this.neuLabel99);
            this.neuPanel4.Controls.Add(this.lblBillID);
            this.neuPanel4.Controls.Add(this.lblTitle);
            this.neuPanel4.Controls.Add(this.lblTargetDept);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(854, 67);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 0;
            // 
            // lblApplyDept
            // 
            this.lblApplyDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblApplyDept.Location = new System.Drawing.Point(7, 46);
            this.lblApplyDept.Name = "lblApplyDept";
            this.lblApplyDept.Size = new System.Drawing.Size(104, 19);
            this.lblApplyDept.TabIndex = 52;
            this.lblApplyDept.Text = "申请药房 4030";
            this.lblApplyDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(523, 8);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(63, 14);
            this.neuLabel2.TabIndex = 48;
            this.neuLabel2.Text = "总差额：";
            this.neuLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel2.Visible = false;
            // 
            // lblTotDif
            // 
            this.lblTotDif.AutoSize = true;
            this.lblTotDif.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotDif.Location = new System.Drawing.Point(582, 8);
            this.lblTotDif.Name = "lblTotDif";
            this.lblTotDif.Size = new System.Drawing.Size(42, 14);
            this.lblTotDif.TabIndex = 51;
            this.lblTotDif.Text = "30.00";
            this.lblTotDif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotDif.Visible = false;
            // 
            // lblInputDate
            // 
            this.lblInputDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInputDate.Location = new System.Drawing.Point(473, 46);
            this.lblInputDate.Name = "lblInputDate";
            this.lblInputDate.Size = new System.Drawing.Size(216, 19);
            this.lblInputDate.TabIndex = 38;
            this.lblInputDate.Text = "申领日期 2014-05-19 09:00";
            this.lblInputDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPage.Location = new System.Drawing.Point(695, 47);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(69, 17);
            this.lblPage.TabIndex = 42;
            this.lblPage.Text = "/";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotRetailCost
            // 
            this.lblTotRetailCost.AutoSize = true;
            this.lblTotRetailCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotRetailCost.Location = new System.Drawing.Point(707, 28);
            this.lblTotRetailCost.Name = "lblTotRetailCost";
            this.lblTotRetailCost.Size = new System.Drawing.Size(42, 14);
            this.lblTotRetailCost.TabIndex = 50;
            this.lblTotRetailCost.Text = "30.00";
            this.lblTotRetailCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotRetailCost.Visible = false;
            // 
            // lblTotPurCost
            // 
            this.lblTotPurCost.AutoSize = true;
            this.lblTotPurCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotPurCost.Location = new System.Drawing.Point(707, 8);
            this.lblTotPurCost.Name = "lblTotPurCost";
            this.lblTotPurCost.Size = new System.Drawing.Size(42, 14);
            this.lblTotPurCost.TabIndex = 49;
            this.lblTotPurCost.Text = "30.00";
            this.lblTotPurCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotPurCost.Visible = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(647, 28);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(63, 14);
            this.neuLabel1.TabIndex = 47;
            this.neuLabel1.Text = "总零价：";
            this.neuLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel1.Visible = false;
            // 
            // neuLabel99
            // 
            this.neuLabel99.AutoSize = true;
            this.neuLabel99.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel99.Location = new System.Drawing.Point(647, 8);
            this.neuLabel99.Name = "neuLabel99";
            this.neuLabel99.Size = new System.Drawing.Size(63, 14);
            this.neuLabel99.TabIndex = 46;
            this.neuLabel99.Text = "总购价：";
            this.neuLabel99.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel99.Visible = false;
            // 
            // lblBillID
            // 
            this.lblBillID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBillID.Location = new System.Drawing.Point(125, 46);
            this.lblBillID.Name = "lblBillID";
            this.lblBillID.Size = new System.Drawing.Size(193, 19);
            this.lblBillID.TabIndex = 41;
            this.lblBillID.Text = "申请单号 1104020014191";
            this.lblBillID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(229, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(182, 19);
            this.lblTitle.TabIndex = 35;
            this.lblTitle.Text = "{0}门诊药房申请单";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTargetDept
            // 
            this.lblTargetDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTargetDept.Location = new System.Drawing.Point(319, 46);
            this.lblTargetDept.Name = "lblTargetDept";
            this.lblTargetDept.Size = new System.Drawing.Size(150, 19);
            this.lblTargetDept.TabIndex = 37;
            this.lblTargetDept.Text = "申领药库 药库";
            this.lblTargetDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPhaApplyInputBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPhaApplyInputBill";
            this.Size = new System.Drawing.Size(854, 122);
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
        private System.Windows.Forms.Label lblTotDif;
        private System.Windows.Forms.Label lblTotRetailCost;
        private System.Windows.Forms.Label lblTotPurCost;
        private System.Windows.Forms.Label neuLabel2;
        private System.Windows.Forms.Label neuLabel1;
        private System.Windows.Forms.Label neuLabel99;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lblBillID;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTargetDept;
        private System.Windows.Forms.Label lblInputDate;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.Label lblOper;
        private System.Windows.Forms.Label neuLabel9;
        private System.Windows.Forms.Label neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel5;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuFpEnter1_Sheet1;
        private System.Windows.Forms.Label lblApplyDept;
        private System.Windows.Forms.Label label1;
    }
}
