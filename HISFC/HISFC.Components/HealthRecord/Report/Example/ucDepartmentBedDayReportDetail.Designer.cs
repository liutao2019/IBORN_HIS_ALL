namespace FS.WinForms.Report.BedDayReport
{
    partial class ucDepartmentBedDayReportDetail
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblNurseName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plResult = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plDetail = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plSheet = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.plTitle = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plTimePick = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox1.SuspendLayout();
            this.plResult.SuspendLayout();
            this.plDetail.SuspendLayout();
            this.plSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.plTitle.SuspendLayout();
            this.plTimePick.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuDateTimePicker1);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Location = new System.Drawing.Point(6, 3);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(822, 42);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(299, 15);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(90, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(240, 19);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "统计区间";
            // 
            // lblNurseName
            // 
            this.lblNurseName.AutoSize = true;
            this.lblNurseName.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblNurseName.Location = new System.Drawing.Point(108, 46);
            this.lblNurseName.Name = "lblNurseName";
            this.lblNurseName.Size = new System.Drawing.Size(63, 14);
            this.lblNurseName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblNurseName.TabIndex = 3;
            this.lblNurseName.Text = "护理单元";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lbDate.Location = new System.Drawing.Point(511, 46);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(35, 14);
            this.lbDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDate.TabIndex = 3;
            this.lbDate.Text = "日期";
            // 
            // plResult
            // 
            this.plResult.Controls.Add(this.plDetail);
            this.plResult.Controls.Add(this.plTitle);
            this.plResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plResult.Location = new System.Drawing.Point(0, 50);
            this.plResult.Name = "plResult";
            this.plResult.Size = new System.Drawing.Size(733, 469);
            this.plResult.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plResult.TabIndex = 4;
            // 
            // plDetail
            // 
            this.plDetail.Controls.Add(this.plSheet);
            this.plDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plDetail.Location = new System.Drawing.Point(0, 66);
            this.plDetail.Name = "plDetail";
            this.plDetail.Size = new System.Drawing.Size(733, 403);
            this.plDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plDetail.TabIndex = 8;
            // 
            // plSheet
            // 
            this.plSheet.Controls.Add(this.neuSpread1);
            this.plSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plSheet.Location = new System.Drawing.Point(0, 0);
            this.plSheet.Name = "plSheet";
            this.plSheet.Size = new System.Drawing.Size(733, 403);
            this.plSheet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plSheet.TabIndex = 0;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(733, 403);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 6;
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
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 3;
            this.neuSpread1_Sheet1.RowCount = 30;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, false, true, false);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "入院情况";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 12;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "出院情况";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 0).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 0).Value = "病人姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 1).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 1).Value = "床号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 2).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 2).Value = "新入院";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 3).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 3).Value = "何区转入";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 4).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "何科转入";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 5).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "入院科室";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "住院号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "病人姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "临床最后主要诊断";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).ColumnSpan = 5;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).Value = "出院动态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 14).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 14).Value = "转往何区";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 15).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 15).Value = "转往何科";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 16).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 16).Value = "出院科室";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 17).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 17).Value = "住院天数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(2, 9).Value = "治愈";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(2, 10).Value = "好转";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(2, 11).Value = "未愈";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(2, 12).Value = "死亡";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(2, 13).Value = "其他";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 28F;
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(1).Height = 28F;
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(2).Height = 33F;
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 47F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 67F;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 67F;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 84F;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 57F;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "治愈";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 22F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "好转";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 19F;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "未愈";
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 22F;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "死亡";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 16F;
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "其他";
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 20F;
            this.neuSpread1_Sheet1.Columns.Get(14).Width = 68F;
            this.neuSpread1_Sheet1.Columns.Get(15).Width = 47F;
            this.neuSpread1_Sheet1.Columns.Get(16).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(17).Width = 34F;
            this.neuSpread1_Sheet1.PrintInfo.Footer = "";
            this.neuSpread1_Sheet1.PrintInfo.Header = "";
            this.neuSpread1_Sheet1.PrintInfo.JobName = "";
            this.neuSpread1_Sheet1.PrintInfo.Printer = "";
            this.neuSpread1_Sheet1.PrintInfo.ShowRowHeaders = false;
            this.neuSpread1_Sheet1.PrintInfo.ShowShadows = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // plTitle
            // 
            this.plTitle.Controls.Add(this.neuLabel5);
            this.plTitle.Controls.Add(this.lbDate);
            this.plTitle.Controls.Add(this.lblNurseName);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle.Location = new System.Drawing.Point(0, 0);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(733, 66);
            this.plTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTitle.TabIndex = 7;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 15F);
            this.neuLabel5.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel5.Location = new System.Drawing.Point(251, 13);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(129, 20);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 4;
            this.neuLabel5.Text = "病室工作日志";
            // 
            // plTimePick
            // 
            this.plTimePick.Controls.Add(this.neuGroupBox1);
            this.plTimePick.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTimePick.Location = new System.Drawing.Point(0, 0);
            this.plTimePick.Name = "plTimePick";
            this.plTimePick.Size = new System.Drawing.Size(733, 50);
            this.plTimePick.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTimePick.TabIndex = 6;
            // 
            // ucDepartmentBedDayReportDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.plResult);
            this.Controls.Add(this.plTimePick);
            this.Name = "ucDepartmentBedDayReportDetail";
            this.Size = new System.Drawing.Size(733, 519);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.plResult.ResumeLayout(false);
            this.plDetail.ResumeLayout(false);
            this.plSheet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.plTitle.ResumeLayout(false);
            this.plTitle.PerformLayout();
            this.plTimePick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblNurseName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDate;
        private FS.FrameWork.WinForms.Controls.NeuPanel plResult;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTimePick;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plDetail;
        private FS.FrameWork.WinForms.Controls.NeuPanel plSheet;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTitle;
    }
}
