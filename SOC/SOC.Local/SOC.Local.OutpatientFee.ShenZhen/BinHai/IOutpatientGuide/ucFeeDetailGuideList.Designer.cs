namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IOutpatientGuide
{
    partial class ucFeeDetailGuideList
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
            this.label1 = new System.Windows.Forms.Label();
            this.neuSp_FeeDetailList = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSp_FeeDetailList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuLblPatientName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLblDatetime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLblHeader = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.neuSp_FeeDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSp_FeeDetailList_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "-----------------------------------------------";
            // 
            // neuSp_FeeDetailList
            // 
            this.neuSp_FeeDetailList.About = "3.0.2004.2005";
            this.neuSp_FeeDetailList.AccessibleDescription = "neuSp_FeeDetailList, Sheet1, Row 0, Column 0, 1";
            this.neuSp_FeeDetailList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.neuSp_FeeDetailList.BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSp_FeeDetailList.FileName = "";
            this.neuSp_FeeDetailList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.neuSp_FeeDetailList.IsAutoSaveGridStatus = false;
            this.neuSp_FeeDetailList.IsCanCustomConfigColumn = false;
            this.neuSp_FeeDetailList.Location = new System.Drawing.Point(3, 65);
            this.neuSp_FeeDetailList.MoveActiveOnFocus = false;
            this.neuSp_FeeDetailList.Name = "neuSp_FeeDetailList";
            this.neuSp_FeeDetailList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSp_FeeDetailList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSp_FeeDetailList_Sheet1});
            this.neuSp_FeeDetailList.Size = new System.Drawing.Size(367, 319);
            this.neuSp_FeeDetailList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSp_FeeDetailList.TabIndex = 9;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSp_FeeDetailList.TextTipAppearance = tipAppearance1;
            this.neuSp_FeeDetailList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // neuSp_FeeDetailList_Sheet1
            // 
            this.neuSp_FeeDetailList_Sheet1.Reset();
            this.neuSp_FeeDetailList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSp_FeeDetailList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSp_FeeDetailList_Sheet1.ColumnCount = 5;
            this.neuSp_FeeDetailList_Sheet1.RowCount = 5;
            this.neuSp_FeeDetailList_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 0).ParseFormatString = "n";
            this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 0).Value = 1;
            this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSp_FeeDetailList_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "序号";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "费用名称";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "数量";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单价";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "金额";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSp_FeeDetailList_Sheet1.Columns.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).Label = "序号";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            textCellType1.WordWrap = true;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).Label = "费用名称";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).Width = 137F;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).Label = "数量";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).Label = "单价";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).Label = "金额";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSp_FeeDetailList_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSp_FeeDetailList_Sheet1.LockBackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.LockForeColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSp_FeeDetailList_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSp_FeeDetailList_Sheet1.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White);
            this.neuSp_FeeDetailList_Sheet1.Rows.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.SelectionBackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.SelectionForeColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.None;
            this.neuSp_FeeDetailList_Sheet1.SheetCornerHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSp_FeeDetailList_Sheet1.SheetCornerVerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSp_FeeDetailList_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSp_FeeDetailList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuLblPatientName
            // 
            this.neuLblPatientName.AutoSize = true;
            this.neuLblPatientName.Location = new System.Drawing.Point(48, 33);
            this.neuLblPatientName.Name = "neuLblPatientName";
            this.neuLblPatientName.Size = new System.Drawing.Size(53, 12);
            this.neuLblPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLblPatientName.TabIndex = 7;
            this.neuLblPatientName.Text = "费用清单";
            // 
            // neuLblDatetime
            // 
            this.neuLblDatetime.AutoSize = true;
            this.neuLblDatetime.Location = new System.Drawing.Point(282, 33);
            this.neuLblDatetime.Name = "neuLblDatetime";
            this.neuLblDatetime.Size = new System.Drawing.Size(53, 12);
            this.neuLblDatetime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLblDatetime.TabIndex = 5;
            this.neuLblDatetime.Text = "费用清单";
            // 
            // neuLblHeader
            // 
            this.neuLblHeader.AutoSize = true;
            this.neuLblHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLblHeader.Location = new System.Drawing.Point(96, 10);
            this.neuLblHeader.Name = "neuLblHeader";
            this.neuLblHeader.Size = new System.Drawing.Size(57, 12);
            this.neuLblHeader.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLblHeader.TabIndex = 6;
            this.neuLblHeader.Text = "费用清单";
            // 
            // ucFeeDetailGuideList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.neuSp_FeeDetailList);
            this.Controls.Add(this.neuLblPatientName);
            this.Controls.Add(this.neuLblDatetime);
            this.Controls.Add(this.neuLblHeader);
            this.Name = "ucFeeDetailGuideList";
            this.Size = new System.Drawing.Size(384, 394);
            ((System.ComponentModel.ISupportInitialize)(this.neuSp_FeeDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSp_FeeDetailList_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSp_FeeDetailList;
        private FarPoint.Win.Spread.SheetView neuSp_FeeDetailList_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLblPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLblDatetime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLblHeader;
        private System.Windows.Forms.Label label1;



    }
}
