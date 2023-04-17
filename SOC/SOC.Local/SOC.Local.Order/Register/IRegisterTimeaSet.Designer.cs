namespace FS.SOC.Local.Order.Register
{
    partial class IRegisterTimeaSet
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
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IRegisterTimeaSet));
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.fpRegTimeSet = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpRegTimeSet_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpRegTimeSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRegTimeSet_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpRegTimeSet
            // 
            this.fpRegTimeSet.About = "3.0.2004.2005";
            this.fpRegTimeSet.AccessibleDescription = "fpRegTimeSet, Sheet1, Row 0, Column 0, ";
            this.fpRegTimeSet.BackColor = System.Drawing.Color.White;
            this.fpRegTimeSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpRegTimeSet.FileName = "";
            this.fpRegTimeSet.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpRegTimeSet.IsAutoSaveGridStatus = false;
            this.fpRegTimeSet.IsCanCustomConfigColumn = false;
            this.fpRegTimeSet.Location = new System.Drawing.Point(0, 0);
            this.fpRegTimeSet.Name = "fpRegTimeSet";
            this.fpRegTimeSet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpRegTimeSet.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpRegTimeSet_Sheet1});
            this.fpRegTimeSet.Size = new System.Drawing.Size(644, 514);
            this.fpRegTimeSet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpRegTimeSet.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpRegTimeSet.TextTipAppearance = tipAppearance1;
            this.fpRegTimeSet.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpRegTimeSet_Sheet1
            // 
            this.fpRegTimeSet_Sheet1.Reset();
            this.fpRegTimeSet_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpRegTimeSet_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpRegTimeSet_Sheet1.ColumnCount = 10;
            this.fpRegTimeSet_Sheet1.RowCount = 1;
            this.fpRegTimeSet_Sheet1.Cells.Get(0, 2).Value = new System.DateTime(2011, 6, 20, 20, 5, 24, 0);
            this.fpRegTimeSet_Sheet1.Cells.Get(0, 6).Value = 2;
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "星期";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "开始时间";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "截止时间";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "拼音码";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "五笔码";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "顺序号";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "有效";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "操作员";
            this.fpRegTimeSet_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "操作日期";
            comboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType1.Items = new string[] {
        "星期一",
        "星期二",
        "星期三",
        "星期四",
        "星期五",
        "星期六",
        "星期日"};
            this.fpRegTimeSet_Sheet1.Columns.Get(1).CellType = comboBoxCellType1;
            this.fpRegTimeSet_Sheet1.Columns.Get(1).Label = "星期";
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2011, 6, 20, 20, 25, 24, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.TimeOnly;
            dateTimeCellType1.TimeDefault = new System.DateTime(2011, 6, 20, 20, 25, 24, 0);
            this.fpRegTimeSet_Sheet1.Columns.Get(2).CellType = dateTimeCellType1;
            this.fpRegTimeSet_Sheet1.Columns.Get(2).Label = "开始时间";
            dateTimeCellType2.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType2.Calendar")));
            dateTimeCellType2.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType2.DateDefault = new System.DateTime(2011, 6, 20, 20, 26, 0, 0);
            dateTimeCellType2.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.TimeOnly;
            dateTimeCellType2.TimeDefault = new System.DateTime(2011, 6, 20, 20, 26, 0, 0);
            this.fpRegTimeSet_Sheet1.Columns.Get(3).CellType = dateTimeCellType2;
            this.fpRegTimeSet_Sheet1.Columns.Get(3).Label = "截止时间";
            this.fpRegTimeSet_Sheet1.Columns.Get(4).Label = "拼音码";
            this.fpRegTimeSet_Sheet1.Columns.Get(4).Locked = true;
            this.fpRegTimeSet_Sheet1.Columns.Get(4).Visible = false;
            this.fpRegTimeSet_Sheet1.Columns.Get(5).Label = "五笔码";
            this.fpRegTimeSet_Sheet1.Columns.Get(5).Locked = true;
            this.fpRegTimeSet_Sheet1.Columns.Get(5).Visible = false;
            numberCellType1.DecimalPlaces = 0;
            this.fpRegTimeSet_Sheet1.Columns.Get(6).CellType = numberCellType1;
            this.fpRegTimeSet_Sheet1.Columns.Get(6).Label = "顺序号";
            comboBoxCellType2.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType2.Items = new string[] {
        "有效",
        "无效"};
            this.fpRegTimeSet_Sheet1.Columns.Get(7).CellType = comboBoxCellType2;
            this.fpRegTimeSet_Sheet1.Columns.Get(7).Label = "有效";
            this.fpRegTimeSet_Sheet1.Columns.Get(8).Label = "操作员";
            this.fpRegTimeSet_Sheet1.Columns.Get(8).Locked = true;
            this.fpRegTimeSet_Sheet1.Columns.Get(9).Label = "操作日期";
            this.fpRegTimeSet_Sheet1.Columns.Get(9).Locked = true;
            this.fpRegTimeSet_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpRegTimeSet_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // IRegisterTimeaSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.fpRegTimeSet);
            this.Name = "IRegisterTimeaSet";
            this.Size = new System.Drawing.Size(644, 514);
            ((System.ComponentModel.ISupportInitialize)(this.fpRegTimeSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRegTimeSet_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView fpRegTimeSet_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpRegTimeSet;
    }
}
