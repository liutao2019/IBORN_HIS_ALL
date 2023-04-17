namespace Neusoft.SOC.Local.Order.ZhuHai.ZDWY.InPatient.ExecBill
{
    partial class ucPrint
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
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder4 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder5 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder6 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder7 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder8 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder9 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder10 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.neuSpread1 = new Neusoft.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPageNO = new System.Windows.Forms.Label();
            this.lblPrintTime = new System.Windows.Forms.Label();
            this.lblDeptName = new System.Windows.Forms.Label();
            this.PanelMain.SuspendLayout();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.BackColor = System.Drawing.Color.White;
            this.PanelMain.Controls.Add(this.panelDetail);
            this.PanelMain.Controls.Add(this.panelTitle);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(576, 550);
            this.PanelMain.TabIndex = 1;
            // 
            // panelDetail
            // 
            this.panelDetail.Controls.Add(this.neuSpread1);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(0, 83);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(576, 467);
            this.panelDetail.TabIndex = 1;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(576, 467);
            this.neuSpread1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 10;
            this.neuSpread1_Sheet1.RowCount = 15;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, System.Drawing.Color.White, false, false, false, true, false);
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "1";
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Value = "2";
            this.neuSpread1_Sheet1.Cells.Get(2, 1).Value = "3";
            this.neuSpread1_Sheet1.Cells.Get(3, 1).Value = "4";
            this.neuSpread1_Sheet1.Cells.Get(4, 1).Value = "5";
            this.neuSpread1_Sheet1.Cells.Get(5, 1).Value = "6";
            this.neuSpread1_Sheet1.Cells.Get(6, 1).Value = "7";
            this.neuSpread1_Sheet1.Cells.Get(7, 1).Value = "8";
            this.neuSpread1_Sheet1.Cells.Get(8, 1).Value = "9";
            this.neuSpread1_Sheet1.Cells.Get(9, 1).Value = "10";
            this.neuSpread1_Sheet1.Cells.Get(10, 1).Value = "11";
            this.neuSpread1_Sheet1.Cells.Get(11, 1).Value = "12";
            this.neuSpread1_Sheet1.Cells.Get(12, 1).Value = "13";
            this.neuSpread1_Sheet1.Cells.Get(13, 1).Value = "14";
            this.neuSpread1_Sheet1.Cells.Get(14, 1).Value = "15";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Border = bevelBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "组";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Border = bevelBorder2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目名称（规格）";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Border = bevelBorder3;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "用量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Border = bevelBorder4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Border = bevelBorder5;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "频次";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Border = bevelBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "执行时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Border = bevelBorder7;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Border = bevelBorder8;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Border = bevelBorder9;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Border = bevelBorder10;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "签名";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "组";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 0F;
            textCellType1.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "项目名称（规格）";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 250F;
            textCellType2.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "用量";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "频次";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 47F;
            textCellType3.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "执行时间";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 100F;
            textCellType4.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 0F;
            textCellType5.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType5;
            this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 0F;
            textCellType6.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType6;
            this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 0F;
            textCellType7.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(9).CellType = textCellType7;
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "签名";
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 56F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.PrintInfo.ShowBorder = false;
            this.neuSpread1_Sheet1.PrintInfo.UseSmartPrint = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Get(0).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(1).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(2).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(3).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(4).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(5).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(6).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(7).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(8).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(9).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(10).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(11).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(12).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(13).Height = 28F;
            this.neuSpread1_Sheet1.Rows.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(14).Height = 28F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.lblTitle);
            this.panelTitle.Controls.Add(this.lblPageNO);
            this.panelTitle.Controls.Add(this.lblPrintTime);
            this.panelTitle.Controls.Add(this.lblDeptName);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(576, 83);
            this.panelTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(0, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(576, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = " 执行单 ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPageNO
            // 
            this.lblPageNO.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPageNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageNO.Location = new System.Drawing.Point(0, 0);
            this.lblPageNO.Name = "lblPageNO";
            this.lblPageNO.Size = new System.Drawing.Size(576, 24);
            this.lblPageNO.TabIndex = 3;
            this.lblPageNO.Text = "第X页/共X页";
            // 
            // lblPrintTime
            // 
            this.lblPrintTime.AutoSize = true;
            this.lblPrintTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrintTime.Location = new System.Drawing.Point(418, 57);
            this.lblPrintTime.Name = "lblPrintTime";
            this.lblPrintTime.Size = new System.Drawing.Size(49, 14);
            this.lblPrintTime.TabIndex = 2;
            this.lblPrintTime.Text = "日期：";
            // 
            // lblDeptName
            // 
            this.lblDeptName.AutoSize = true;
            this.lblDeptName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDeptName.Location = new System.Drawing.Point(-3, 57);
            this.lblDeptName.Name = "lblDeptName";
            this.lblDeptName.Size = new System.Drawing.Size(49, 14);
            this.lblDeptName.TabIndex = 1;
            this.lblDeptName.Text = "病区：";
            // 
            // ucPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PanelMain);
            this.Name = "ucPrint";
            this.Size = new System.Drawing.Size(576, 550);
            this.PanelMain.ResumeLayout(false);
            this.panelDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Panel panelDetail;
        private Neusoft.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPageNO;
        private System.Windows.Forms.Label lblPrintTime;
        private System.Windows.Forms.Label lblDeptName;
    }
}
