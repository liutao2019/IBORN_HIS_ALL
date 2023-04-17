﻿namespace FS.HISFC.Components.Common.Controls
{
    partial class ucInpatientCharge
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucInpatientCharge));
            this.fpDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpDetail_Sheet = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDetail_Sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // fpDetail
            // 
            this.fpDetail.About = "3.0.2004.2005";
            this.fpDetail.AccessibleDescription = "fpDetail, Sheet, Row 0, Column 0, ";
            this.fpDetail.BackColor = System.Drawing.Color.White;
            this.fpDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpDetail.EditModePermanent = true;
            this.fpDetail.EditModeReplace = true;
            this.fpDetail.FileName = "";
            this.fpDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpDetail.IsAutoSaveGridStatus = false;
            this.fpDetail.IsCanCustomConfigColumn = false;
            this.fpDetail.Location = new System.Drawing.Point(0, 0);
            this.fpDetail.Name = "fpDetail";
            this.fpDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpDetail_Sheet});
            this.fpDetail.Size = new System.Drawing.Size(886, 595);
            this.fpDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpDetail.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpDetail.TextTipAppearance = tipAppearance1;
            this.fpDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpDetail.EditModeOn += new System.EventHandler(this.fpDetail_EditModeOn);
            this.fpDetail.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDetail_EditChange);
            this.fpDetail.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpDetail_CellDoubleClick);
            this.fpDetail.DragDrop += new System.Windows.Forms.DragEventHandler(this.fpDetail_DragDrop);
            this.fpDetail.ComboSelChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDetail_ComboSelChange);
            this.fpDetail.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);
            // 
            // fpDetail_Sheet
            // 
            this.fpDetail_Sheet.Reset();
            this.fpDetail_Sheet.SheetName = "Sheet";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpDetail_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpDetail_Sheet.ColumnCount = 15;
            this.fpDetail_Sheet.RowCount = 2;
            this.fpDetail_Sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Transparent, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpDetail_Sheet.Cells.Get(1, 5).Value = "合计:";
            this.fpDetail_Sheet.Cells.Get(1, 6).Value = 0;
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 0).Value = "国家编码";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 1).Value = "项目名称";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 2).Value = "价格";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 3).Value = "数量";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 4).Value = "付数";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 6).Value = "合计金额";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 7).Value = "执行科室";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 8).Value = "ItemObject";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 9).Value = "IsNew";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 10).Value = "IsDeptChange";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 11).Value = "IsDrug";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 12).Value = "收费比例";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 13).Value = "批号";
            this.fpDetail_Sheet.ColumnHeader.Cells.Get(0, 14).Value = "计费日期";
            this.fpDetail_Sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Transparent;
            this.fpDetail_Sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDetail_Sheet.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpDetail_Sheet.Columns.Get(0).Label = "国家编码";
            this.fpDetail_Sheet.Columns.Get(0).Width = 78F;
            this.fpDetail_Sheet.Columns.Get(1).CellType = textCellType1;
            this.fpDetail_Sheet.Columns.Get(1).Font = new System.Drawing.Font("宋体", 12F);
            this.fpDetail_Sheet.Columns.Get(1).Label = "项目名称";
            this.fpDetail_Sheet.Columns.Get(1).Width = 230F;
            numberCellType1.DecimalPlaces = 4;
            this.fpDetail_Sheet.Columns.Get(2).CellType = numberCellType1;
            this.fpDetail_Sheet.Columns.Get(2).Font = new System.Drawing.Font("宋体", 12F);
            this.fpDetail_Sheet.Columns.Get(2).Label = "价格";
            this.fpDetail_Sheet.Columns.Get(2).Width = 62F;
            numberCellType2.MaximumValue = 99999.99;
            numberCellType2.MinimumValue = -9999.99;
            this.fpDetail_Sheet.Columns.Get(3).CellType = numberCellType2;
            this.fpDetail_Sheet.Columns.Get(3).Font = new System.Drawing.Font("宋体", 12F);
            this.fpDetail_Sheet.Columns.Get(3).Label = "数量";
            this.fpDetail_Sheet.Columns.Get(3).Width = 50F;
            this.fpDetail_Sheet.Columns.Get(4).CellType = numberCellType3;
            this.fpDetail_Sheet.Columns.Get(4).Label = "付数";
            this.fpDetail_Sheet.Columns.Get(4).Width = 33F;
            this.fpDetail_Sheet.Columns.Get(5).CellType = textCellType2;
            this.fpDetail_Sheet.Columns.Get(5).Font = new System.Drawing.Font("宋体", 12F);
            this.fpDetail_Sheet.Columns.Get(5).Label = "单位";
            this.fpDetail_Sheet.Columns.Get(5).Width = 50F;
            numberCellType4.ReadOnly = true;
            this.fpDetail_Sheet.Columns.Get(6).CellType = numberCellType4;
            this.fpDetail_Sheet.Columns.Get(6).Font = new System.Drawing.Font("宋体", 12F);
            this.fpDetail_Sheet.Columns.Get(6).Label = "合计金额";
            this.fpDetail_Sheet.Columns.Get(6).Locked = true;
            this.fpDetail_Sheet.Columns.Get(6).Width = 83F;
            this.fpDetail_Sheet.Columns.Get(7).CellType = textCellType3;
            this.fpDetail_Sheet.Columns.Get(7).Label = "执行科室";
            this.fpDetail_Sheet.Columns.Get(7).Width = 102F;
            this.fpDetail_Sheet.Columns.Get(8).Label = "ItemObject";
            this.fpDetail_Sheet.Columns.Get(8).Width = 0F;
            this.fpDetail_Sheet.Columns.Get(9).Label = "IsNew";
            this.fpDetail_Sheet.Columns.Get(9).Width = 0F;
            this.fpDetail_Sheet.Columns.Get(10).Label = "IsDeptChange";
            this.fpDetail_Sheet.Columns.Get(10).Width = 0F;
            this.fpDetail_Sheet.Columns.Get(11).Label = "IsDrug";
            this.fpDetail_Sheet.Columns.Get(11).Width = 0F;
            numberCellType5.MaximumValue = 1;
            numberCellType5.MinimumValue = 0;
            this.fpDetail_Sheet.Columns.Get(12).CellType = numberCellType5;
            this.fpDetail_Sheet.Columns.Get(12).Label = "收费比例";
            this.fpDetail_Sheet.Columns.Get(12).Width = 0F;
            this.fpDetail_Sheet.Columns.Get(13).Label = "批号";
            this.fpDetail_Sheet.Columns.Get(13).Width = 72F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2022, 5, 26, 14, 17, 24, 764);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;
            dateTimeCellType1.TimeDefault = new System.DateTime(2022, 5, 26, 14, 17, 24, 764);
            this.fpDetail_Sheet.Columns.Get(14).CellType = dateTimeCellType1;
            this.fpDetail_Sheet.Columns.Get(14).Label = "计费日期";
            this.fpDetail_Sheet.Columns.Get(14).Width = 130F;
            this.fpDetail_Sheet.GroupBarBackColor = System.Drawing.Color.White;
            this.fpDetail_Sheet.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpDetail_Sheet.RowHeader.Columns.Default.Resizable = true;
            this.fpDetail_Sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Transparent;
            this.fpDetail_Sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDetail_Sheet.Rows.Get(0).Height = 23F;
            this.fpDetail_Sheet.Rows.Get(1).Height = 23F;
            this.fpDetail_Sheet.Rows.Get(1).Locked = true;
            this.fpDetail_Sheet.SheetCornerStyle.BackColor = System.Drawing.Color.Transparent;
            this.fpDetail_Sheet.SheetCornerStyle.Parent = "CornerDefault";
            this.fpDetail_Sheet.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpDetail_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucInpatientCharge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.fpDetail);
            this.Name = "ucInpatientCharge";
            this.Size = new System.Drawing.Size(886, 595);
            this.Load += new System.EventHandler(this.ucInpatientCharge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDetail_Sheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread fpDetail;
        private FarPoint.Win.Spread.SheetView fpDetail_Sheet;
    }
}
