namespace FS.SOC.Local.Material.Extend
{
    partial class ucBillRePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBillRePrint));
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet2)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.dtEnd);
            this.neuGroupBox1.Controls.Add(this.dtBegin);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(915, 50);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "设  定";
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(248, 19);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(159, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 1;
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(65, 19);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(154, 21);
            this.dtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 1;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(226, 23);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(17, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "－";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(6, 23);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "开始时间";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, 详细, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 50);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1,
            this.neuSpread1_Sheet2});
            this.neuSpread1.Size = new System.Drawing.Size(915, 371);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            this.neuSpread1.ActiveSheetIndex = 1;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.DarkGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo = ((System.Globalization.DateTimeFormatInfo)(cultureInfo.DateTimeFormat.Clone()));
            ((System.Globalization.DateTimeFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).DateSeparator = "-";
            ((System.Globalization.DateTimeFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).FullDateTimePattern = "yyyy\'年\'M\'月\'d\'日\' HH:mm:ss";
            ((System.Globalization.DateTimeFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).LongTimePattern = "HH:mm:ss";
            ((System.Globalization.DateTimeFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).ShortDatePattern = "yyyy-MM-dd";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).ParseFormatString = "yyyy-MM-dd H:mm:ss";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = new System.DateTime(2012, 3, 3, 9, 9, 9, 0);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "单号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "操作类别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "目标单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "操作员";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "操作时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "出库时间";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "单号";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 139F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "操作类别";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 83F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "目标单位";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 198F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 95F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "操作员";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "操作时间";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 135F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "出库时间";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 135F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSpread1_Sheet2
            // 
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.SheetName = "详细";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet2.ColumnCount = 14;
            this.neuSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.DarkGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 0).Value = "单号";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 1).Value = "发票号";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 2).Value = "物资名称[规格]";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 3).Value = "数量";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 4).Value = "最小单位";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 5).Value = "包装数量";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 6).Value = "零售单价";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 7).Value = "零售金额";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 8).Value = "购入价";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 9).Value = "购入金额";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 10).Value = "有效期";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 11).Value = "批号";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 12).Value = "生产厂家";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 13).Value = "高值耗材编码";
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.Columns.Get(0).Label = "单号";
            this.neuSpread1_Sheet2.Columns.Get(0).Width = 87F;
            this.neuSpread1_Sheet2.Columns.Get(1).Label = "发票号";
            this.neuSpread1_Sheet2.Columns.Get(1).Width = 85F;
            this.neuSpread1_Sheet2.Columns.Get(2).Label = "物资名称[规格]";
            this.neuSpread1_Sheet2.Columns.Get(2).Width = 235F;
            this.neuSpread1_Sheet2.Columns.Get(3).Label = "数量";
            this.neuSpread1_Sheet2.Columns.Get(3).Width = 46F;
            this.neuSpread1_Sheet2.Columns.Get(4).Label = "最小单位";
            this.neuSpread1_Sheet2.Columns.Get(4).Width = 63F;
            this.neuSpread1_Sheet2.Columns.Get(6).Label = "零售单价";
            this.neuSpread1_Sheet2.Columns.Get(6).Width = 100F;
            this.neuSpread1_Sheet2.Columns.Get(7).Label = "零售金额";
            this.neuSpread1_Sheet2.Columns.Get(7).Width = 100F;
            this.neuSpread1_Sheet2.Columns.Get(8).Label = "购入价";
            this.neuSpread1_Sheet2.Columns.Get(8).Width = 100F;
            this.neuSpread1_Sheet2.Columns.Get(9).Label = "购入金额";
            this.neuSpread1_Sheet2.Columns.Get(9).Width = 100F;
            this.neuSpread1_Sheet2.Columns.Get(10).Label = "有效期";
            this.neuSpread1_Sheet2.Columns.Get(10).Width = 78F;
            this.neuSpread1_Sheet2.Columns.Get(11).Label = "批号";
            this.neuSpread1_Sheet2.Columns.Get(11).Width = 93F;
            this.neuSpread1_Sheet2.Columns.Get(12).Label = "生产厂家";
            this.neuSpread1_Sheet2.Columns.Get(12).Width = 120F;
            this.neuSpread1_Sheet2.Columns.Get(13).Label = "高值耗材编码";
            this.neuSpread1_Sheet2.Columns.Get(13).Width = 100F;
            this.neuSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet2.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet2.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucBillRePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucBillRePrint";
            this.Size = new System.Drawing.Size(915, 421);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet2;
    }
}
