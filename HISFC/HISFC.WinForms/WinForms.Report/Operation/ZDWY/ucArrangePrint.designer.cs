namespace FS.WinForms.Report.Operation.ZDWY
{
    partial class ucArrangePrint
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
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbPageNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.nlbPageNO);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.nlbTitle);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1645, 85);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // nlbPageNO
            // 
            this.nlbPageNO.AutoSize = true;
            this.nlbPageNO.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbPageNO.Location = new System.Drawing.Point(1425, 61);
            this.nlbPageNO.Name = "nlbPageNO";
            this.nlbPageNO.Size = new System.Drawing.Size(40, 16);
            this.nlbPageNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPageNO.TabIndex = 2;
            this.nlbPageNO.Text = "页码";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(32, 61);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(40, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "截止";
            // 
            // nlbTitle
            // 
            this.nlbTitle.AutoSize = true;
            this.nlbTitle.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTitle.Location = new System.Drawing.Point(752, 18);
            this.nlbTitle.Name = "nlbTitle";
            this.nlbTitle.Size = new System.Drawing.Size(180, 33);
            this.nlbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTitle.TabIndex = 0;
            this.nlbTitle.Text = "安排一览表";
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 85);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1645, 1075);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
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
            this.neuSpread1_Sheet1.ColumnCount = 18;
            this.neuSpread1_Sheet1.RowCount = 33;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin5", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "1号手术间";
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "第一台";
            this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "呼吸内科";
            this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = "刘德华";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = 1234567890;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = "男";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = "18岁";
            textCellType1.WordWrap = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 17).CellType = textCellType1;
            this.neuSpread1_Sheet1.Cells.Get(0, 17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(0, 17).Value = "气钻、显微镜、显微器械、头架、可吸收颅骨固定钉*3、脑膜补片电脑输不进第一台，明日为脑科手术日，此为第一台手术";
            this.neuSpread1_Sheet1.Cells.Get(0, 17).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "手术间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "台次";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "科室";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "床号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "住院号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "性别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "年龄";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "术前诊断";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "手术名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "主刀医生";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "一助";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "二助";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "麻醉方式";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "麻醉医生";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "洗手护士";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "巡回护士";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 45F;
            textCellType2.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "手术间";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 71F;
            textCellType3.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "台次";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 58F;
            textCellType4.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "科室";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 96F;
            textCellType5.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType5;
            this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "床号";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 32F;
            textCellType6.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType6;
            this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 54F;
            textCellType7.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType7;
            this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "住院号";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 83F;
            textCellType8.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType8;
            this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "性别";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 54F;
            textCellType9.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType9;
            this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "年龄";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 66F;
            textCellType10.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType10;
            this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "术前诊断";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 156F;
            textCellType11.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(9).CellType = textCellType11;
            this.neuSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "手术名称";
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 215F;
            textCellType12.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(10).CellType = textCellType12;
            this.neuSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "主刀医生";
            textCellType13.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(11).CellType = textCellType13;
            this.neuSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "一助";
            textCellType14.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(12).CellType = textCellType14;
            this.neuSpread1_Sheet1.Columns.Get(12).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "二助";
            textCellType15.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(13).CellType = textCellType15;
            this.neuSpread1_Sheet1.Columns.Get(13).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "麻醉方式";
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 76F;
            textCellType16.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(14).CellType = textCellType16;
            this.neuSpread1_Sheet1.Columns.Get(14).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(14).Label = "麻醉医生";
            textCellType17.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(15).CellType = textCellType17;
            this.neuSpread1_Sheet1.Columns.Get(15).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(15).Label = "洗手护士";
            textCellType18.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(16).CellType = textCellType18;
            this.neuSpread1_Sheet1.Columns.Get(16).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(16).Label = "巡回护士";
            textCellType19.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(17).CellType = textCellType19;
            this.neuSpread1_Sheet1.Columns.Get(17).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(17).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get(17).Width = 232F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Get(0).Height = 62F;
            this.neuSpread1_Sheet1.Rows.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows.Get(1).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(2).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(3).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(4).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(5).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(6).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(7).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(8).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(9).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(10).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(11).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(12).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(13).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(14).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(15).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(16).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(17).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(18).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(19).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(20).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(21).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(22).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(23).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(24).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(25).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(26).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(27).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(28).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(29).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(30).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(31).Height = 30F;
            this.neuSpread1_Sheet1.Rows.Get(32).Height = 30F;
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucArrangePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucArrangePrint";
            this.Size = new System.Drawing.Size(1645, 1160);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTitle;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPageNO;
    }
}
