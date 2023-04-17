namespace SOC.Local.Operation
{
    partial class frmOpsLEDShowByDoc
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

        #region Windows 窗体设计器生成的代码

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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nPanelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuOperationSpread = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuOperationSpread_汇总 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblWindow = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.nPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread_汇总)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            // 
            // nPanelMain
            // 
            this.nPanelMain.BackColor = System.Drawing.Color.Transparent;
            this.nPanelMain.Controls.Add(this.neuOperationSpread);
            this.nPanelMain.Controls.Add(this.neuPanel1);
            this.nPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nPanelMain.ForeColor = System.Drawing.Color.Transparent;
            this.nPanelMain.Location = new System.Drawing.Point(0, 0);
            this.nPanelMain.Name = "nPanelMain";
            this.nPanelMain.Size = new System.Drawing.Size(1292, 809);
            this.nPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nPanelMain.TabIndex = 0;
            // 
            // neuOperationSpread
            // 
            this.neuOperationSpread.About = "3.0.2004.2005";
            this.neuOperationSpread.AccessibleDescription = "neuOperationSpread, 汇总, Row 0, Column 0, 1间";
            this.neuOperationSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuOperationSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuOperationSpread.FileName = "";
            this.neuOperationSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuOperationSpread.IsAutoSaveGridStatus = false;
            this.neuOperationSpread.IsCanCustomConfigColumn = false;
            this.neuOperationSpread.Location = new System.Drawing.Point(0, 87);
            this.neuOperationSpread.Name = "neuOperationSpread";
            this.neuOperationSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuOperationSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuOperationSpread_汇总});
            this.neuOperationSpread.Size = new System.Drawing.Size(1292, 722);
            this.neuOperationSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuOperationSpread.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuOperationSpread.TextTipAppearance = tipAppearance1;
            this.neuOperationSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuOperationSpread_汇总
            // 
            this.neuOperationSpread_汇总.Reset();
            this.neuOperationSpread_汇总.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuOperationSpread_汇总.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuOperationSpread_汇总.ColumnCount = 19;
            this.neuOperationSpread_汇总.RowCount = 1;
            this.neuOperationSpread_汇总.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), false, false, false, true, true);
            this.neuOperationSpread_汇总.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 0).Value = "1间";
            this.neuOperationSpread_汇总.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatString = "n";
            this.neuOperationSpread_汇总.Cells.Get(0, 1).Value = 1;
            this.neuOperationSpread_汇总.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 2).Value = "耳鼻咽喉科住院";
            this.neuOperationSpread_汇总.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatString = "n";
            this.neuOperationSpread_汇总.Cells.Get(0, 3).Value = 46;
            this.neuOperationSpread_汇总.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 4).Value = "苏丽康";
            this.neuOperationSpread_汇总.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 5).Value = "男";
            this.neuOperationSpread_汇总.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatString = "n";
            this.neuOperationSpread_汇总.Cells.Get(0, 6).Value = 201926;
            this.neuOperationSpread_汇总.Cells.Get(0, 7).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 7).Value = "28岁";
            this.neuOperationSpread_汇总.Cells.Get(0, 8).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Cells.Get(0, 8).Value = "取除骨折内固定装置(Z47.001)";
            this.neuOperationSpread_汇总.Cells.Get(0, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Cells.Get(0, 9).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 9).Value = "腹腔镜下全子宫切除术(TLH)";
            this.neuOperationSpread_汇总.Cells.Get(0, 10).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 10).Value = "HIV";
            this.neuOperationSpread_汇总.Cells.Get(0, 11).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 11).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 12).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 12).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 13).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 13).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 14).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 14).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 15).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 15).Value = "局备全麻";
            this.neuOperationSpread_汇总.Cells.Get(0, 16).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 16).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 17).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 17).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 18).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Cells.Get(0, 18).Value = "备隧道针、长导丝（尖端为圆头的，无折弯痕迹），术中心电血压、血氧监护，";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).Value = "手术间";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).Value = "手术台";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).Value = "科室";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).Value = "床号";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).Value = "姓名";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).Value = "性别";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).Value = "住院号";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 7).Value = "年龄";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 8).Value = "术前诊断";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 9).Value = "手术名称";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 10).Value = "感染类型";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 11).Value = "主刀医生";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 12).Value = "一助";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 13).Value = "麻醉医生";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 14).Value = "麻醉一助";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 15).Value = "麻醉方式";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 16).Value = "洗手护士";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 17).Value = "巡回护士";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 18).Value = "特殊说明";
            this.neuOperationSpread_汇总.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.ColumnHeader.Rows.Get(0).Height = 34F;
            textCellType1.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(0).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(0).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(0).Label = "手术间";
            this.neuOperationSpread_汇总.Columns.Get(0).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(0).Width = 62F;
            textCellType2.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(1).CellType = textCellType2;
            this.neuOperationSpread_汇总.Columns.Get(1).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(1).Label = "手术台";
            this.neuOperationSpread_汇总.Columns.Get(1).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(1).Width = 35F;
            textCellType3.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(2).CellType = textCellType3;
            this.neuOperationSpread_汇总.Columns.Get(2).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Columns.Get(2).Label = "科室";
            this.neuOperationSpread_汇总.Columns.Get(2).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(2).Width = 153F;
            textCellType4.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(3).CellType = textCellType4;
            this.neuOperationSpread_汇总.Columns.Get(3).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(3).Label = "床号";
            this.neuOperationSpread_汇总.Columns.Get(3).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(3).Width = 50F;
            textCellType5.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(4).CellType = textCellType5;
            this.neuOperationSpread_汇总.Columns.Get(4).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(4).Label = "姓名";
            this.neuOperationSpread_汇总.Columns.Get(4).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(4).Width = 115F;
            textCellType6.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(5).CellType = textCellType6;
            this.neuOperationSpread_汇总.Columns.Get(5).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(5).Label = "性别";
            this.neuOperationSpread_汇总.Columns.Get(5).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(5).Width = 32F;
            textCellType7.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(6).CellType = textCellType7;
            this.neuOperationSpread_汇总.Columns.Get(6).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(6).Label = "住院号";
            this.neuOperationSpread_汇总.Columns.Get(6).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(6).Width = 116F;
            textCellType8.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(7).CellType = textCellType8;
            this.neuOperationSpread_汇总.Columns.Get(7).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(7).Label = "年龄";
            this.neuOperationSpread_汇总.Columns.Get(7).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(7).Width = 77F;
            textCellType9.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(8).CellType = textCellType9;
            this.neuOperationSpread_汇总.Columns.Get(8).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(8).Label = "术前诊断";
            this.neuOperationSpread_汇总.Columns.Get(8).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(8).Width = 136F;
            textCellType10.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(9).CellType = textCellType10;
            this.neuOperationSpread_汇总.Columns.Get(9).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Columns.Get(9).Label = "手术名称";
            this.neuOperationSpread_汇总.Columns.Get(9).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(9).Width = 136F;
            textCellType11.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(10).CellType = textCellType11;
            this.neuOperationSpread_汇总.Columns.Get(10).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(10).Label = "感染类型";
            this.neuOperationSpread_汇总.Columns.Get(10).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(10).Width = 73F;
            textCellType12.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(11).CellType = textCellType12;
            this.neuOperationSpread_汇总.Columns.Get(11).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(11).Label = "主刀医生";
            this.neuOperationSpread_汇总.Columns.Get(11).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(11).Width = 107F;
            textCellType13.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(12).CellType = textCellType13;
            this.neuOperationSpread_汇总.Columns.Get(12).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(12).Label = "一助";
            this.neuOperationSpread_汇总.Columns.Get(12).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(12).Width = 1F;
            textCellType14.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(13).CellType = textCellType14;
            this.neuOperationSpread_汇总.Columns.Get(13).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(13).Label = "麻醉医生";
            this.neuOperationSpread_汇总.Columns.Get(13).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(13).Width = 118F;
            textCellType15.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(14).CellType = textCellType15;
            this.neuOperationSpread_汇总.Columns.Get(14).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(14).Label = "麻醉一助";
            this.neuOperationSpread_汇总.Columns.Get(14).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(14).Width = 0F;
            textCellType16.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(15).CellType = textCellType16;
            this.neuOperationSpread_汇总.Columns.Get(15).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(15).Label = "麻醉方式";
            this.neuOperationSpread_汇总.Columns.Get(15).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(15).Width = 81F;
            textCellType17.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(16).CellType = textCellType17;
            this.neuOperationSpread_汇总.Columns.Get(16).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(16).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(16).Label = "洗手护士";
            this.neuOperationSpread_汇总.Columns.Get(16).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(16).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(16).Width = 118F;
            textCellType18.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(17).CellType = textCellType18;
            this.neuOperationSpread_汇总.Columns.Get(17).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(17).Label = "巡回护士";
            this.neuOperationSpread_汇总.Columns.Get(17).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(17).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(17).Width = 118F;
            textCellType19.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(18).CellType = textCellType19;
            this.neuOperationSpread_汇总.Columns.Get(18).Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(18).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Columns.Get(18).Label = "特殊说明";
            this.neuOperationSpread_汇总.Columns.Get(18).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(18).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(18).Width = 150F;
            this.neuOperationSpread_汇总.DefaultStyle.Locked = false;
            this.neuOperationSpread_汇总.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuOperationSpread_汇总.RowHeader.Columns.Default.Resizable = true;
            this.neuOperationSpread_汇总.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.Rows.Get(0).Height = 157F;
            this.neuOperationSpread_汇总.SelectionBackColor = System.Drawing.Color.Transparent;
            this.neuOperationSpread_汇总.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.None;
            this.neuOperationSpread_汇总.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.SheetCornerStyle.Locked = false;
            this.neuOperationSpread_汇总.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuOperationSpread_汇总.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.neuPanel1.Controls.Add(this.nlbDate);
            this.neuPanel1.Controls.Add(this.button1);
            this.neuPanel1.Controls.Add(this.lblWindow);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1292, 87);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // nlbDate
            // 
            this.nlbDate.AutoSize = true;
            this.nlbDate.Font = new System.Drawing.Font("华文中宋", 22F, System.Drawing.FontStyle.Bold);
            this.nlbDate.ForeColor = System.Drawing.Color.Navy;
            this.nlbDate.Location = new System.Drawing.Point(12, 47);
            this.nlbDate.Name = "nlbDate";
            this.nlbDate.Size = new System.Drawing.Size(294, 34);
            this.nlbDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDate.TabIndex = 2;
            this.nlbDate.Text = "日期：2014-12-02";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1059, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblWindow
            // 
            this.lblWindow.AutoSize = true;
            this.lblWindow.Font = new System.Drawing.Font("华文中宋", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWindow.ForeColor = System.Drawing.Color.Navy;
            this.lblWindow.Location = new System.Drawing.Point(488, 9);
            this.lblWindow.Name = "lblWindow";
            this.lblWindow.Size = new System.Drawing.Size(427, 64);
            this.lblWindow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblWindow.TabIndex = 0;
            this.lblWindow.Text = "手术安排一览表";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 3000;
            // 
            // frmOpsLEDShowByDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1292, 809);
            this.Controls.Add(this.nPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOpsLEDShowByDoc";
            this.Text = "屏幕显示";
            this.Load += new System.EventHandler(this.frmDisplay_Load);
            this.DoubleClick += new System.EventHandler(this.frmDisplay_DoubleClick);
            this.nPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread_汇总)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private System.Windows.Forms.Timer timer1;
        private FS.FrameWork.WinForms.Controls.NeuPanel nPanelMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer2;
        protected FS.FrameWork.WinForms.Controls.NeuSpread neuOperationSpread;
        private FarPoint.Win.Spread.SheetView neuOperationSpread_汇总;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblWindow;
    }
}