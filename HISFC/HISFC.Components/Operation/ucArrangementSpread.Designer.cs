namespace FS.HISFC.Components.Operation
{
    partial class ucArrangementSpread
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
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucArrangementSpread));
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
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
            this.fpSpread1.Size = new System.Drawing.Size(1132, 611);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.EditModeOn += new System.EventHandler(this.fpSpread1_EditModeOn);
            this.fpSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_EditChange);
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellClick);
            this.fpSpread1.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.fpSpread1_LeaveCell);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 36;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "打印 ";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "病区";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "手术间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "手术台序";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "台序";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "正台";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "床号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "住院号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "性别";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "年龄";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "手术时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "手术名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "术前诊断";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "感染类型";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "主刀医生";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "一助";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "二助手";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "麻醉医生";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "麻醉一助";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "麻醉类别";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "麻醉方式";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 23).Value = "麻醉方式2";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 24).Value = "洗手护士1";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 25).Value = "洗手护士2";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 26).Value = "允许医生查看";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 27).Value = "巡回护士1";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 28).Value = "巡回护士2";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 29).Value = "总巡";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 30).Value = "手术不安排原因";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 31).Value = "接班人员";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 32).Value = "特殊说明";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 33).Value = "手术申请时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 34).Value = "允许医生查看";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 35).Value = "总巡";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 48F;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "打印 ";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 30F;
            this.fpSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "病区";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "手术间";
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "台序";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 51F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "正台";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 42F;
            this.fpSpread1_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "科室";
            this.fpSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 107F;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "床号";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 29F;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "住院号";
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 93F;
            this.fpSpread1_Sheet1.Columns.Get(9).Label = "姓名";
            this.fpSpread1_Sheet1.Columns.Get(9).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 58F;
            this.fpSpread1_Sheet1.Columns.Get(10).Label = "性别";
            this.fpSpread1_Sheet1.Columns.Get(10).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 23F;
            this.fpSpread1_Sheet1.Columns.Get(11).Label = "年龄";
            this.fpSpread1_Sheet1.Columns.Get(11).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(11).Width = 20F;
            this.fpSpread1_Sheet1.Columns.Get(12).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(12).Label = "手术时间";
            this.fpSpread1_Sheet1.Columns.Get(12).Width = 121F;
            this.fpSpread1_Sheet1.Columns.Get(13).Label = "手术名称";
            this.fpSpread1_Sheet1.Columns.Get(13).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(13).Width = 185F;
            this.fpSpread1_Sheet1.Columns.Get(14).Label = "术前诊断";
            this.fpSpread1_Sheet1.Columns.Get(14).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(15).Label = "感染类型";
            this.fpSpread1_Sheet1.Columns.Get(15).Width = 50F;
            this.fpSpread1_Sheet1.Columns.Get(18).Label = "二助手";
            this.fpSpread1_Sheet1.Columns.Get(18).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(19).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpSpread1_Sheet1.Columns.Get(19).Label = "麻醉医生";
            this.fpSpread1_Sheet1.Columns.Get(19).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(20).Label = "麻醉一助";
            this.fpSpread1_Sheet1.Columns.Get(20).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(21).Label = "麻醉类别";
            this.fpSpread1_Sheet1.Columns.Get(21).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(22).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(22).Label = "麻醉方式";
            this.fpSpread1_Sheet1.Columns.Get(22).Width = 79F;
            this.fpSpread1_Sheet1.Columns.Get(23).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(23).Label = "麻醉方式2";
            this.fpSpread1_Sheet1.Columns.Get(23).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(24).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(24).Label = "洗手护士1";
            this.fpSpread1_Sheet1.Columns.Get(25).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(25).Label = "洗手护士2";
            this.fpSpread1_Sheet1.Columns.Get(26).Label = "允许医生查看";
            this.fpSpread1_Sheet1.Columns.Get(26).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(27).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(27).Label = "巡回护士1";
            this.fpSpread1_Sheet1.Columns.Get(28).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(28).Label = "巡回护士2";
            this.fpSpread1_Sheet1.Columns.Get(29).Label = "总巡";
            this.fpSpread1_Sheet1.Columns.Get(29).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(30).Label = "手术不安排原因";
            this.fpSpread1_Sheet1.Columns.Get(30).Width = 90F;
            this.fpSpread1_Sheet1.Columns.Get(31).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(31).Label = "接班人员";
            this.fpSpread1_Sheet1.Columns.Get(31).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(32).Label = "特殊说明";
            this.fpSpread1_Sheet1.Columns.Get(32).Width = 137F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2006, 12, 11, 11, 18, 11, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType1.TimeDefault = new System.DateTime(2006, 12, 11, 11, 18, 11, 0);
            dateTimeCellType1.UserDefinedFormat = "yyyy-MM-dd HH:mm:ss";
            this.fpSpread1_Sheet1.Columns.Get(33).CellType = dateTimeCellType1;
            this.fpSpread1_Sheet1.Columns.Get(33).Label = "手术申请时间";
            this.fpSpread1_Sheet1.Columns.Get(33).Width = 83F;
            this.fpSpread1_Sheet1.Columns.Get(34).CellType = checkBoxCellType2;
            this.fpSpread1_Sheet1.Columns.Get(34).Label = "允许医生查看";
            this.fpSpread1_Sheet1.Columns.Get(34).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(35).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(35).Label = "总巡";
            this.fpSpread1_Sheet1.Columns.Get(35).Width = 0F;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucArrangementSpread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpSpread1);
            this.Name = "ucArrangementSpread";
            this.Size = new System.Drawing.Size(1132, 611);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
    }
}