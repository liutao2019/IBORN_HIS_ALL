﻿namespace FS.HISFC.Components.Operation.Report
{
    partial class ucReportPatient
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
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1";
            this.fpSpread1.Size = new System.Drawing.Size(691, 314);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 12;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "床号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "住院号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "性别";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "年龄";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "诊断";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "手术名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "手术开始时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "手术结束时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "术者";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "麻醉方式";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "分类";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(1, 0);
            // 
            // ucReportPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucReportPatient";
            this.Size = new System.Drawing.Size(691, 455);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
