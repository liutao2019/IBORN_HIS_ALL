namespace FS.HISFC.Components.Operation.Report
{
    partial class ucAnaArrangementPrint
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(825, 384);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.fpSpread1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(825, 310);
            this.panel3.TabIndex = 1;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(825, 310);
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 24;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "手术间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "手术时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "台序";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "病区";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "性别";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "年龄";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "床号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "住院号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "术前诊断";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "手术名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "是否特殊手术";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "特殊说明";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "主刀医生";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "一助手";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "麻醉方式1";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "麻醉方式2";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "主麻";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "麻醉助手";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "麻醉助手1";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "总巡";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "助手3";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 23).Value = "接班人员";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "手术间";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "手术时间";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 39F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "台序";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 29F;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "病区";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 93F;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "科室";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 79F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "姓名";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 47F;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "性别";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 27F;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "年龄";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 36F;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "床号";
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 28F;
            this.fpSpread1_Sheet1.Columns.Get(9).CellType = textCellType1;
            this.fpSpread1_Sheet1.Columns.Get(9).Label = "住院号";
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 68F;
            this.fpSpread1_Sheet1.Columns.Get(10).Label = "术前诊断";
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 111F;
            this.fpSpread1_Sheet1.Columns.Get(11).Label = "手术名称";
            this.fpSpread1_Sheet1.Columns.Get(11).Width = 197F;
            this.fpSpread1_Sheet1.Columns.Get(12).Label = "是否特殊手术";
            this.fpSpread1_Sheet1.Columns.Get(12).Width = 50F;
            this.fpSpread1_Sheet1.Columns.Get(13).Label = "特殊说明";
            this.fpSpread1_Sheet1.Columns.Get(13).Width = 77F;
            this.fpSpread1_Sheet1.Columns.Get(17).Label = "麻醉方式2";
            this.fpSpread1_Sheet1.Columns.Get(17).Width = 59F;
            this.fpSpread1_Sheet1.Columns.Get(18).Label = "主麻";
            this.fpSpread1_Sheet1.Columns.Get(18).Width = 48F;
            this.fpSpread1_Sheet1.Columns.Get(19).Label = "麻醉助手";
            this.fpSpread1_Sheet1.Columns.Get(19).Width = 38F;
            this.fpSpread1_Sheet1.Columns.Get(20).Label = "麻醉助手1";
            this.fpSpread1_Sheet1.Columns.Get(20).Width = 39F;
            this.fpSpread1_Sheet1.Columns.Get(21).Label = "总巡";
            this.fpSpread1_Sheet1.Columns.Get(21).Width = 42F;
            this.fpSpread1_Sheet1.Columns.Get(22).Label = "助手3";
            this.fpSpread1_Sheet1.Columns.Get(22).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(23).Label = "接班人员";
            this.fpSpread1_Sheet1.Columns.Get(23).Width = 37F;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblDate);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(825, 74);
            this.panel2.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDate.Location = new System.Drawing.Point(12, 49);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(47, 19);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "截止";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(311, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(216, 29);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "手术安排一览表";
            // 
            // ucAnaArrangementPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "ucAnaArrangementPrint";
            this.Size = new System.Drawing.Size(825, 384);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDate;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
    }
}
