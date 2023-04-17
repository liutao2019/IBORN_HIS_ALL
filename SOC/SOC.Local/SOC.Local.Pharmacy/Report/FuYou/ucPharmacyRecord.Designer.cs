namespace Neusoft.SOC.Local.Pharmacy.Report.FuYou
{
    partial class ucPharmacyRecord
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
            this.components = new System.ComponentModel.Container();
            this.neuPanel1 = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel5 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbDrug = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.panelAll.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.panelPrint.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.panelAdditionTitle.SuspendLayout();
            this.panelDataView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).BeginInit();
            this.nGroupBoxQueryCondition.SuspendLayout();
            this.panelQueryConditions.SuspendLayout();
            this.panelTime.SuspendLayout();
            this.panelDept.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAll
            // 
            this.panelAll.Size = new System.Drawing.Size(921, 516);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 97);
            this.neuGroupBox2.Size = new System.Drawing.Size(921, 419);
            // 
            // panelPrint
            // 
            this.panelPrint.Size = new System.Drawing.Size(915, 399);
            // 
            // panelTitle
            // 
            this.panelTitle.Size = new System.Drawing.Size(915, 47);
            // 
            // panelAdditionTitle
            // 
            this.panelAdditionTitle.Size = new System.Drawing.Size(915, 15);
            // 
            // lbAdditionTitleMid
            // 
            this.lbAdditionTitleMid.Location = new System.Drawing.Point(478, 0);
            // 
            // panelDataView
            // 
            this.panelDataView.Size = new System.Drawing.Size(915, 337);
            // 
            // fpSpread1
            // 
            this.fpSpread1.Size = new System.Drawing.Size(915, 337);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 12;
            this.fpSpread1_Sheet1.ColumnHeader.RowCount = 2;
            this.fpSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).RowSpan = 2;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "凭证号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "摘要";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "借方";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "贷方";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "结存";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 3).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 10).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 11).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread1_Sheet2
            // 
            this.fpSpread1_Sheet2.Reset();
            this.fpSpread1_Sheet2.SheetName = "明细";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet2.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet2.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet2.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet2.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // nGroupBoxQueryCondition
            // 
            this.nGroupBoxQueryCondition.Controls.Add(this.neuPanel1);
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(921, 97);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.panelQueryConditions, 0);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.neuPanel1, 0);
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQueryConditions.Size = new System.Drawing.Size(915, 44);
            // 
            // panelTime
            // 
            this.panelTime.Location = new System.Drawing.Point(277, 0);
            // 
            // panelDept
            // 
            this.panelDept.Size = new System.Drawing.Size(277, 44);
            // 
            // cmbDept
            // 
            this.cmbDept.Size = new System.Drawing.Size(197, 20);
            // 
            // ncmbTime
            // 
            this.ncmbTime.Size = new System.Drawing.Size(381, 21);
            // 
            // panelFilter
            // 
            this.panelFilter.Location = new System.Drawing.Point(905, 0);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel5);
            this.neuPanel1.Controls.Add(this.ncmbDrug);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 61);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(915, 36);
            this.neuPanel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 17;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(14, 14);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(41, 12);
            this.neuLabel5.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 11;
            this.neuLabel5.Text = "药品：";
            // 
            // ncmbDrug
            // 
            this.ncmbDrug.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDrug.FormattingEnabled = true;
            this.ncmbDrug.IsEnter2Tab = false;
            this.ncmbDrug.IsFlat = false;
            this.ncmbDrug.IsLike = true;
            this.ncmbDrug.IsListOnly = false;
            this.ncmbDrug.IsPopForm = true;
            this.ncmbDrug.IsShowCustomerList = false;
            this.ncmbDrug.IsShowID = false;
            this.ncmbDrug.Location = new System.Drawing.Point(61, 10);
            this.ncmbDrug.Name = "ncmbDrug";
            this.ncmbDrug.PopForm = null;
            this.ncmbDrug.ShowCustomerList = false;
            this.ncmbDrug.ShowID = false;
            this.ncmbDrug.Size = new System.Drawing.Size(197, 20);
            this.ncmbDrug.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDrug.TabIndex = 10;
            this.ncmbDrug.Tag = "";
            this.ncmbDrug.ToolBarUse = false;
            // 
            // ucPharmacyRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CHHGridLineColor = System.Drawing.Color.LightGray;
            this.CHVGridLineColor = System.Drawing.Color.LightGray;
            this.DetailCHHGridLineColor = System.Drawing.Color.LightGray;
            this.DetailCHVGridLineColor = System.Drawing.Color.LightGray;
            this.DetailHorizontalGridLineColor = System.Drawing.Color.LightGray;
            this.DetailRHHGridLineColor = System.Drawing.Color.LightGray;
            this.DetailRHVGridLineColor = System.Drawing.Color.LightGray;
            this.DetailVerticalGridLineColor = System.Drawing.Color.LightGray;
            this.HorizontalGridLineColor = System.Drawing.Color.LightGray;
            this.Name = "ucPharmacyRecord";
            this.RHHGridLineColor = System.Drawing.Color.LightGray;
            this.RHVGridLineColor = System.Drawing.Color.LightGray;
            this.Size = new System.Drawing.Size(921, 516);
            this.VerticalGridLineColor = System.Drawing.Color.LightGray;
            this.panelAll.ResumeLayout(false);
            this.neuGroupBox2.ResumeLayout(false);
            this.panelPrint.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.panelAdditionTitle.ResumeLayout(false);
            this.panelAdditionTitle.PerformLayout();
            this.panelDataView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).EndInit();
            this.nGroupBoxQueryCondition.ResumeLayout(false);
            this.panelQueryConditions.ResumeLayout(false);
            this.panelTime.ResumeLayout(false);
            this.panelDept.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Neusoft.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox ncmbDrug;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
    }
}
