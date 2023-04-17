namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IOutpatientGuide
{
    partial class ucFeeDetailPricedList
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
            this.lblDateInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLblHeader = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPayKind = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblSum = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.neuSp_FeeDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSp_FeeDetailList_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(569, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "---------------------------------------------------------------------------------" +
                "-------------";
            // 
            // neuSp_FeeDetailList
            // 
            this.neuSp_FeeDetailList.About = "3.0.2004.2005";
            this.neuSp_FeeDetailList.AccessibleDescription = "neuSp_FeeDetailList, Sheet1, Row 0, Column 0, 1";
            this.neuSp_FeeDetailList.BackColor = System.Drawing.Color.White;
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
            this.neuSp_FeeDetailList.Size = new System.Drawing.Size(709, 319);
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
            this.neuSp_FeeDetailList_Sheet1.ColumnCount = 8;
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
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "统一编码";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "收费项目规格";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单位";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单价";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "总金额(元)";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "费用科室";
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSp_FeeDetailList_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSp_FeeDetailList_Sheet1.Columns.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).Label = "序号";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(0).NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            textCellType1.WordWrap = true;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).Label = "统一编码";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(1).Width = 97F;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).Label = "收费项目规格";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(2).Width = 150F;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).Label = "单位";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(3).Width = 50F;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).Label = "数量";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(5).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(5).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(5).Label = "单价";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(6).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(6).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(6).Label = "总金额(元)";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(6).Width = 90F;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(7).BackColor = System.Drawing.Color.White;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(7).ForeColor = System.Drawing.Color.Black;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(7).Label = "费用科室";
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSp_FeeDetailList_Sheet1.Columns.Get(7).Width = 90F;
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
            this.neuLblPatientName.Location = new System.Drawing.Point(48, 35);
            this.neuLblPatientName.Name = "neuLblPatientName";
            this.neuLblPatientName.Size = new System.Drawing.Size(53, 12);
            this.neuLblPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLblPatientName.TabIndex = 7;
            this.neuLblPatientName.Text = "费用清单";
            // 
            // lblDateInfo
            // 
            this.lblDateInfo.AutoSize = true;
            this.lblDateInfo.Location = new System.Drawing.Point(535, 35);
            this.lblDateInfo.Name = "lblDateInfo";
            this.lblDateInfo.Size = new System.Drawing.Size(59, 12);
            this.lblDateInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDateInfo.TabIndex = 5;
            this.lblDateInfo.Text = "记账日期:";
            // 
            // neuLblHeader
            // 
            this.neuLblHeader.AutoSize = true;
            this.neuLblHeader.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.neuLblHeader.Location = new System.Drawing.Point(173, 10);
            this.neuLblHeader.Name = "neuLblHeader";
            this.neuLblHeader.Size = new System.Drawing.Size(114, 20);
            this.neuLblHeader.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLblHeader.TabIndex = 6;
            this.neuLblHeader.Text = "门诊记账单";
            // 
            // lblPayKind
            // 
            this.lblPayKind.AutoSize = true;
            this.lblPayKind.Location = new System.Drawing.Point(210, 35);
            this.lblPayKind.Name = "lblPayKind";
            this.lblPayKind.Size = new System.Drawing.Size(59, 12);
            this.lblPayKind.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPayKind.TabIndex = 11;
            this.lblPayKind.Text = "病人性质:";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(328, 35);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(35, 12);
            this.lblSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSex.TabIndex = 12;
            this.lblSex.Text = "性别:";
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(425, 35);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(59, 12);
            this.lblSum.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSum.TabIndex = 13;
            this.lblSum.Text = "费用总额:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(153, 35);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 14;
            this.neuLabel1.Text = "病人性质:";
            // 
            // ucFeeDetailPricedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.lblSum);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblPayKind);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.neuSp_FeeDetailList);
            this.Controls.Add(this.neuLblPatientName);
            this.Controls.Add(this.lblDateInfo);
            this.Controls.Add(this.neuLblHeader);
            this.Name = "ucFeeDetailPricedList";
            this.Size = new System.Drawing.Size(726, 394);
            ((System.ComponentModel.ISupportInitialize)(this.neuSp_FeeDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSp_FeeDetailList_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSp_FeeDetailList;
        private FarPoint.Win.Spread.SheetView neuSp_FeeDetailList_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLblPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDateInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLblHeader;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPayKind;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSum;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;



    }
}
