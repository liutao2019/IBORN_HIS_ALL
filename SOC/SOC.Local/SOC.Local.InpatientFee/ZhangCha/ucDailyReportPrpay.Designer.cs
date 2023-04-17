namespace FS.SOC.Local.InpatientFee.ZhangCha
{
    partial class ucDailyReportPrpay
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
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
            this.panelFilter.SuspendLayout();
            this.panelTime.SuspendLayout();
            this.panelDept.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAll
            // 
            this.panelAll.Size = new System.Drawing.Size(1047, 348);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Size = new System.Drawing.Size(1047, 284);
            // 
            // panelPrint
            // 
            this.panelPrint.Size = new System.Drawing.Size(1041, 264);
            // 
            // panelTitle
            // 
            this.panelTitle.Size = new System.Drawing.Size(1041, 47);
            // 
            // panelAdditionTitle
            // 
            this.panelAdditionTitle.Size = new System.Drawing.Size(1041, 15);
            // 
            // panelDataView
            // 
            this.panelDataView.Controls.Add(this.neuPanel1);
            this.panelDataView.Size = new System.Drawing.Size(1041, 202);
            this.panelDataView.Controls.SetChildIndex(this.neuPanel1, 0);
            this.panelDataView.Controls.SetChildIndex(this.fpSpread1, 0);
            // 
            // fpSpread1
            // 
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpSpread1.Location = new System.Drawing.Point(0, 10);
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Size = new System.Drawing.Size(1041, 192);
            this.fpSpread1.ActiveSheetIndex = 1;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
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
            this.fpSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(93)))), ((int)(((byte)(90))))), System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet2.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // nGroupBoxQueryCondition
            // 
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(1047, 64);
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Controls.Add(this.panel1);
            this.panelQueryConditions.Size = new System.Drawing.Size(1041, 44);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelDept, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelTime, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelFilter, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panel1, 0);
            // 
            // ncmbTime
            // 
            this.ncmbTime.Size = new System.Drawing.Size(349, 21);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(768, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 44);
            this.panel1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "天内的时间段";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(58, 12);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 21);
            this.numericUpDown1.TabIndex = 20;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "显示";
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1041, 10);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.Black;
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(621, 10);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // ucDailyReportPrpay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CHHGridLineColor = System.Drawing.Color.LightGray;
            this.CHVGridLineColor = System.Drawing.Color.LightGray;
            this.DetailCHHGridLineColor = System.Drawing.Color.White;
            this.DetailCHVGridLineColor = System.Drawing.Color.White;
            this.DetailHorizontalGridLineColor = System.Drawing.Color.White;
            this.DetailRHHGridLineColor = System.Drawing.Color.White;
            this.DetailRHVGridLineColor = System.Drawing.Color.White;
            this.DetailVerticalGridLineColor = System.Drawing.Color.White;
            this.HorizontalGridLineColor = System.Drawing.Color.LightGray;
            this.Name = "ucDailyReportPrpay";
            this.RHHGridLineColor = System.Drawing.Color.LightGray;
            this.RHVGridLineColor = System.Drawing.Color.LightGray;
            this.Size = new System.Drawing.Size(1047, 348);
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
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelTime.ResumeLayout(false);
            this.panelDept.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
    }
}
