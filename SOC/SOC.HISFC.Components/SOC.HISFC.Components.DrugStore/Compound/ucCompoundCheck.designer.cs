namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    partial class ucCompoundCheck
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCompoundCheck));
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpApply = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpApply_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpApply
            // 
            this.fpApply.About = "3.0.2004.2005";
            this.fpApply.AccessibleDescription = "fpApply, Sheet1";
            this.fpApply.BackColor = System.Drawing.Color.White;
            this.fpApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpApply.FileName = "";
            this.fpApply.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpApply.IsAutoSaveGridStatus = false;
            this.fpApply.IsCanCustomConfigColumn = false;
            this.fpApply.Location = new System.Drawing.Point(0, 0);
            this.fpApply.Name = "fpApply";
            this.fpApply.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpApply.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpApply_Sheet1});
            this.fpApply.Size = new System.Drawing.Size(800, 600);
            this.fpApply.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpApply.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpApply.TextTipAppearance = tipAppearance1;
            this.fpApply.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpApply.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpApply_ButtonClicked);
            this.fpApply.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpApply_CellClick);
            // 
            // fpApply_Sheet1
            // 
            this.fpApply_Sheet1.Reset();
            this.fpApply_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpApply_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpApply_Sheet1.ColumnCount = 17;
            this.fpApply_Sheet1.RowCount = 0;
            this.fpApply_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "[床号]姓名";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "选中";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "批次号";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "组";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "药品[规格]";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "零售价";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "用量";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "单位";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "总量";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "单位";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "频次";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "用法";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "用药时间";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "医嘱类型";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "开方医生";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "申请时间";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "组号";
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.Columns.Get(0).Label = "[床号]姓名";
            this.fpApply_Sheet1.Columns.Get(0).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.fpApply_Sheet1.Columns.Get(0).Width = 78F;
            this.fpApply_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.SeaShell;
            this.fpApply_Sheet1.Columns.Get(1).CellType = checkBoxCellType1;
            this.fpApply_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpApply_Sheet1.Columns.Get(1).Label = "选中";
            this.fpApply_Sheet1.Columns.Get(1).Locked = false;
            this.fpApply_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.fpApply_Sheet1.Columns.Get(1).Width = 38F;
            this.fpApply_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.fpApply_Sheet1.Columns.Get(3).Label = "组";
            this.fpApply_Sheet1.Columns.Get(3).Width = 29F;
            this.fpApply_Sheet1.Columns.Get(4).Label = "药品[规格]";
            this.fpApply_Sheet1.Columns.Get(4).Width = 140F;
            this.fpApply_Sheet1.Columns.Get(5).CellType = numberCellType1;
            this.fpApply_Sheet1.Columns.Get(5).Label = "零售价";
            this.fpApply_Sheet1.Columns.Get(6).CellType = numberCellType2;
            this.fpApply_Sheet1.Columns.Get(6).Label = "用量";
            this.fpApply_Sheet1.Columns.Get(7).Label = "单位";
            this.fpApply_Sheet1.Columns.Get(7).Width = 40F;
            this.fpApply_Sheet1.Columns.Get(8).CellType = numberCellType3;
            this.fpApply_Sheet1.Columns.Get(8).Label = "总量";
            this.fpApply_Sheet1.Columns.Get(9).Label = "单位";
            this.fpApply_Sheet1.Columns.Get(9).Width = 40F;
            this.fpApply_Sheet1.Columns.Get(10).Label = "频次";
            this.fpApply_Sheet1.Columns.Get(10).Width = 46F;
            this.fpApply_Sheet1.Columns.Get(11).Label = "用法";
            this.fpApply_Sheet1.Columns.Get(11).Width = 50F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2007, 8, 21, 10, 9, 47, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;
            dateTimeCellType1.TimeDefault = new System.DateTime(2007, 8, 21, 10, 9, 47, 0);
            this.fpApply_Sheet1.Columns.Get(12).CellType = dateTimeCellType1;
            this.fpApply_Sheet1.Columns.Get(12).Label = "用药时间";
            this.fpApply_Sheet1.Columns.Get(12).Width = 113F;
            this.fpApply_Sheet1.Columns.Get(14).CellType = textCellType2;
            this.fpApply_Sheet1.Columns.Get(14).Label = "开方医生";
            this.fpApply_Sheet1.Columns.Get(14).Width = 70F;
            this.fpApply_Sheet1.Columns.Get(15).Label = "申请时间";
            this.fpApply_Sheet1.Columns.Get(15).Width = 106F;
            this.fpApply_Sheet1.Columns.Get(16).Label = "组号";
            this.fpApply_Sheet1.Columns.Get(16).Visible = false;
            this.fpApply_Sheet1.Columns.Get(16).Width = 27F;
            this.fpApply_Sheet1.DefaultStyle.Locked = true;
            this.fpApply_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpApply_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpApply_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpApply_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.SheetCornerStyle.Locked = false;
            this.fpApply_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpApply_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpApply.SetActiveViewport(0, 1, 0);
            // 
            // ucCompoundCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpApply);
            this.Name = "ucCompoundCheck";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.fpApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuSpread fpApply;
        private FarPoint.Win.Spread.SheetView fpApply_Sheet1;
    }
}
