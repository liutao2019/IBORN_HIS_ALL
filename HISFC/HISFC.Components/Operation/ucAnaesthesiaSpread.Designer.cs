namespace FS.HISFC.Components.Operation
{
    partial class ucAnaesthesiaSpread
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
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(791, 629);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.fpSpread1_EnterCell);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 23;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "打印";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "手术间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "台序";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "病区";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "床号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "性别";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "年龄";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "术前诊断";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "手术名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "麻醉类型";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "是否特殊手术";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "主刀医生";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "一助手";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "麻醉方式";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "麻醉方式2";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "主麻";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "麻醉助手";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "麻醉助手1";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "总巡";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "接班人员";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "特殊说明";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "打印";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 30F;
            this.fpSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "手术间";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 64F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "台序";
            this.fpSpread1_Sheet1.Columns.Get(2).Locked = false;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 59F;
            this.fpSpread1_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "病区";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 97F;
            this.fpSpread1_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "科室";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 105F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "床号";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 46F;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "姓名";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 68F;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "性别";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 32F;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "年龄";
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 36F;
            this.fpSpread1_Sheet1.Columns.Get(9).Label = "术前诊断";
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 151F;
            this.fpSpread1_Sheet1.Columns.Get(10).Label = "手术名称";
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 129F;
            this.fpSpread1_Sheet1.Columns.Get(11).ForeColor = System.Drawing.Color.Blue;
            this.fpSpread1_Sheet1.Columns.Get(11).Label = "麻醉类型";
            this.fpSpread1_Sheet1.Columns.Get(11).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(12).Label = "是否特殊手术";
            this.fpSpread1_Sheet1.Columns.Get(12).Width = 52F;
            this.fpSpread1_Sheet1.Columns.Get(15).Label = "麻醉方式";
            this.fpSpread1_Sheet1.Columns.Get(15).Width = 73F;
            this.fpSpread1_Sheet1.Columns.Get(16).Label = "麻醉方式2";
            this.fpSpread1_Sheet1.Columns.Get(16).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(17).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(17).Label = "主麻";
            this.fpSpread1_Sheet1.Columns.Get(17).Width = 67F;
            this.fpSpread1_Sheet1.Columns.Get(18).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(18).Label = "麻醉助手";
            this.fpSpread1_Sheet1.Columns.Get(18).Width = 73F;
            this.fpSpread1_Sheet1.Columns.Get(19).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(19).Label = "麻醉助手1";
            this.fpSpread1_Sheet1.Columns.Get(19).Width = 76F;
            this.fpSpread1_Sheet1.Columns.Get(20).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(20).Label = "总巡";
            this.fpSpread1_Sheet1.Columns.Get(21).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(21).Label = "接班人员";
            this.fpSpread1_Sheet1.Columns.Get(21).Width = 70F;
            this.fpSpread1_Sheet1.Columns.Get(22).Label = "特殊说明";
            this.fpSpread1_Sheet1.Columns.Get(22).Width = 102F;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucAnaesthesiaSpread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpSpread1);
            this.Name = "ucAnaesthesiaSpread";
            this.Size = new System.Drawing.Size(791, 629);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;

    }
}
