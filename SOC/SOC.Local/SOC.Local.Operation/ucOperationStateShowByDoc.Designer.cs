namespace SOC.Local.Operation
{
    partial class ucOperationStateShowByDoc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
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
            this.neuPanelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanelBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuOperationSpread = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuOperationSpread_汇总 = new FarPoint.Win.Spread.SheetView();
            this.neuPanelTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuOperationDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.neuPanelMain.SuspendLayout();
            this.neuPanelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread_汇总)).BeginInit();
            this.neuPanelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.BackColor = System.Drawing.Color.Transparent;
            this.neuPanelMain.Controls.Add(this.neuPanelBottom);
            this.neuPanelMain.Controls.Add(this.neuPanelTop);
            this.neuPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelMain.Location = new System.Drawing.Point(0, 0);
            this.neuPanelMain.Name = "neuPanelMain";
            this.neuPanelMain.Size = new System.Drawing.Size(1356, 495);
            this.neuPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelMain.TabIndex = 0;
            // 
            // neuPanelBottom
            // 
            this.neuPanelBottom.Controls.Add(this.neuOperationSpread);
            this.neuPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelBottom.Location = new System.Drawing.Point(0, 52);
            this.neuPanelBottom.Name = "neuPanelBottom";
            this.neuPanelBottom.Size = new System.Drawing.Size(1356, 443);
            this.neuPanelBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelBottom.TabIndex = 1;
            // 
            // neuOperationSpread
            // 
            this.neuOperationSpread.About = "3.0.2004.2005";
            this.neuOperationSpread.AccessibleDescription = "neuOperationSpread, 汇总, Row 0, Column 0, ";
            this.neuOperationSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuOperationSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuOperationSpread.FileName = "";
            this.neuOperationSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuOperationSpread.IsAutoSaveGridStatus = false;
            this.neuOperationSpread.IsCanCustomConfigColumn = false;
            this.neuOperationSpread.Location = new System.Drawing.Point(0, 0);
            this.neuOperationSpread.Name = "neuOperationSpread";
            this.neuOperationSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuOperationSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuOperationSpread_汇总});
            this.neuOperationSpread.Size = new System.Drawing.Size(1356, 443);
            this.neuOperationSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuOperationSpread.TabIndex = 5;
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
            this.neuOperationSpread_汇总.Cells.Get(0, 0).Value = "1间";
            this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuOperationSpread_汇总.Cells.Get(0, 1).ParseFormatString = "n";
            this.neuOperationSpread_汇总.Cells.Get(0, 1).Value = 1;
            this.neuOperationSpread_汇总.Cells.Get(0, 2).Value = "耳鼻咽喉科住院";
            this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuOperationSpread_汇总.Cells.Get(0, 3).ParseFormatString = "n";
            this.neuOperationSpread_汇总.Cells.Get(0, 3).Value = 46;
            this.neuOperationSpread_汇总.Cells.Get(0, 4).Value = "苏丽康";
            this.neuOperationSpread_汇总.Cells.Get(0, 5).Value = "男";
            this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuOperationSpread_汇总.Cells.Get(0, 6).ParseFormatString = "n";
            this.neuOperationSpread_汇总.Cells.Get(0, 6).Value = 201926;
            this.neuOperationSpread_汇总.Cells.Get(0, 7).Value = "28岁";
            this.neuOperationSpread_汇总.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Cells.Get(0, 8).Value = "取除骨折内固定装置(Z47.001)";
            this.neuOperationSpread_汇总.Cells.Get(0, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Cells.Get(0, 9).Value = "腹腔镜下全子宫切除术(TLH)";
            this.neuOperationSpread_汇总.Cells.Get(0, 10).Value = "HIV";
            this.neuOperationSpread_汇总.Cells.Get(0, 11).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 12).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 13).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 14).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 15).Value = "局备全麻";
            this.neuOperationSpread_汇总.Cells.Get(0, 16).Value = "樊韵平";
            this.neuOperationSpread_汇总.Cells.Get(0, 17).Value = "樊韵平";
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
            this.neuOperationSpread_汇总.Columns.Get(0).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(0).Label = "手术间";
            this.neuOperationSpread_汇总.Columns.Get(0).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(0).Width = 42F;
            this.neuOperationSpread_汇总.Columns.Get(1).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(1).Label = "手术台";
            this.neuOperationSpread_汇总.Columns.Get(1).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(1).Width = 34F;
            textCellType1.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(2).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(2).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Columns.Get(2).Label = "科室";
            this.neuOperationSpread_汇总.Columns.Get(2).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(2).Width = 142F;
            this.neuOperationSpread_汇总.Columns.Get(3).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(3).Label = "床号";
            this.neuOperationSpread_汇总.Columns.Get(3).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(3).Width = 35F;
            this.neuOperationSpread_汇总.Columns.Get(4).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(4).Label = "姓名";
            this.neuOperationSpread_汇总.Columns.Get(4).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(4).Width = 67F;
            this.neuOperationSpread_汇总.Columns.Get(5).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(5).Label = "性别";
            this.neuOperationSpread_汇总.Columns.Get(5).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(5).Width = 34F;
            this.neuOperationSpread_汇总.Columns.Get(6).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(6).Label = "住院号";
            this.neuOperationSpread_汇总.Columns.Get(6).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(7).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(7).Label = "年龄";
            this.neuOperationSpread_汇总.Columns.Get(7).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(7).Width = 53F;
            this.neuOperationSpread_汇总.Columns.Get(8).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(8).Label = "术前诊断";
            this.neuOperationSpread_汇总.Columns.Get(8).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(8).Width = 157F;
            textCellType2.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(9).CellType = textCellType2;
            this.neuOperationSpread_汇总.Columns.Get(9).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Columns.Get(9).Label = "手术名称";
            this.neuOperationSpread_汇总.Columns.Get(9).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(9).Width = 183F;
            this.neuOperationSpread_汇总.Columns.Get(10).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(10).Label = "感染类型";
            this.neuOperationSpread_汇总.Columns.Get(10).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(10).Width = 39F;
            this.neuOperationSpread_汇总.Columns.Get(11).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(11).Label = "主刀医生";
            this.neuOperationSpread_汇总.Columns.Get(11).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(11).Width = 55F;
            this.neuOperationSpread_汇总.Columns.Get(12).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(12).Label = "一助";
            this.neuOperationSpread_汇总.Columns.Get(12).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(12).Width = 0F;
            this.neuOperationSpread_汇总.Columns.Get(13).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(13).Label = "麻醉医生";
            this.neuOperationSpread_汇总.Columns.Get(13).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(14).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(14).Label = "麻醉一助";
            this.neuOperationSpread_汇总.Columns.Get(14).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(14).Width = 0F;
            textCellType3.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(15).CellType = textCellType3;
            this.neuOperationSpread_汇总.Columns.Get(15).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(15).Label = "麻醉方式";
            this.neuOperationSpread_汇总.Columns.Get(15).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(16).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(16).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(16).Label = "洗手护士";
            this.neuOperationSpread_汇总.Columns.Get(16).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(16).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(16).Width = 57F;
            this.neuOperationSpread_汇总.Columns.Get(17).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(17).Label = "巡回护士";
            this.neuOperationSpread_汇总.Columns.Get(17).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(17).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(17).Width = 58F;
            textCellType4.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(18).CellType = textCellType4;
            this.neuOperationSpread_汇总.Columns.Get(18).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.Columns.Get(18).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Columns.Get(18).Label = "特殊说明";
            this.neuOperationSpread_汇总.Columns.Get(18).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(18).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(18).Width = 162F;
            this.neuOperationSpread_汇总.DefaultStyle.Locked = false;
            this.neuOperationSpread_汇总.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuOperationSpread_汇总.RowHeader.Columns.Default.Resizable = true;
            this.neuOperationSpread_汇总.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.Rows.Get(0).Height = 76F;
            this.neuOperationSpread_汇总.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.SheetCornerStyle.Locked = false;
            this.neuOperationSpread_汇总.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuOperationSpread_汇总.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanelTop
            // 
            this.neuPanelTop.Controls.Add(this.neuOperationDate);
            this.neuPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanelTop.Location = new System.Drawing.Point(0, 0);
            this.neuPanelTop.Name = "neuPanelTop";
            this.neuPanelTop.Size = new System.Drawing.Size(1356, 52);
            this.neuPanelTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelTop.TabIndex = 0;
            // 
            // neuOperationDate
            // 
            this.neuOperationDate.CustomFormat = "yyyy-MM-dd";
            this.neuOperationDate.Font = new System.Drawing.Font("宋体", 11F);
            this.neuOperationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuOperationDate.IsEnter2Tab = false;
            this.neuOperationDate.Location = new System.Drawing.Point(20, 15);
            this.neuOperationDate.Name = "neuOperationDate";
            this.neuOperationDate.Size = new System.Drawing.Size(122, 24);
            this.neuOperationDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuOperationDate.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 50000;
            // 
            // ucOperationStateShowByDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.neuPanelMain);
            this.Name = "ucOperationStateShowByDoc";
            this.Size = new System.Drawing.Size(1356, 495);
            this.neuPanelMain.ResumeLayout(false);
            this.neuPanelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread_汇总)).EndInit();
            this.neuPanelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelBottom;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelTop;
        protected FS.FrameWork.WinForms.Controls.NeuSpread neuOperationSpread;
        private FarPoint.Win.Spread.SheetView neuOperationSpread_汇总;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuOperationDate;
        private System.Windows.Forms.Timer timer1;

    }
}