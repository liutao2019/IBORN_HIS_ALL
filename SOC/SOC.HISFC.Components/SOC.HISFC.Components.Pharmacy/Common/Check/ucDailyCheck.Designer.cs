namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    partial class ucDailyCheck
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("表单列表");
            this.neuTreeView1 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
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
            this.panelCustomType.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAll
            // 
            this.panelAll.Controls.Add(this.neuGroupBox1);
            this.panelAll.Size = new System.Drawing.Size(802, 553);
            this.panelAll.Controls.SetChildIndex(this.neuGroupBox1, 0);
            this.panelAll.Controls.SetChildIndex(this.nGroupBoxQueryCondition, 0);
            this.panelAll.Controls.SetChildIndex(this.neuGroupBox2, 0);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Location = new System.Drawing.Point(269, 0);
            this.neuGroupBox2.Size = new System.Drawing.Size(533, 499);
            // 
            // panelPrint
            // 
            this.panelPrint.Size = new System.Drawing.Size(527, 479);
            // 
            // panelTitle
            // 
            this.panelTitle.Size = new System.Drawing.Size(527, 47);
            // 
            // lbMainTitle
            // 
            this.lbMainTitle.Location = new System.Drawing.Point(362, 12);
            // 
            // panelAdditionTitle
            // 
            this.panelAdditionTitle.Size = new System.Drawing.Size(527, 15);
            // 
            // lbAdditionTitleMid
            // 
            this.lbAdditionTitleMid.Location = new System.Drawing.Point(-172, 0);
            // 
            // panelDataView
            // 
            this.panelDataView.Size = new System.Drawing.Size(527, 417);
            // 
            // fpSpread1
            // 
            this.fpSpread1.Size = new System.Drawing.Size(527, 417);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
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
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
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
            this.fpSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // nGroupBoxQueryCondition
            // 
            this.nGroupBoxQueryCondition.Dock = System.Windows.Forms.DockStyle.Left;
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(269, 499);
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Controls.Add(this.neuTreeView1);
            this.panelQueryConditions.Size = new System.Drawing.Size(263, 479);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelFilter, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelDept, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelTime, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelCustomType, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.neuTreeView1, 0);
            // 
            // panelTime
            // 
            this.panelTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTime.Location = new System.Drawing.Point(0, 44);
            this.panelTime.Size = new System.Drawing.Size(263, 65);
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(60, 6);
            this.dtStart.Size = new System.Drawing.Size(184, 21);
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(60, 33);
            this.dtEnd.Size = new System.Drawing.Size(184, 21);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(220, 6);
            // 
            // lbTime
            // 
            this.lbTime.Location = new System.Drawing.Point(6, 3);
            // 
            // panelDept
            // 
            this.panelDept.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDept.Size = new System.Drawing.Size(263, 44);
            // 
            // cmbDept
            // 
            this.cmbDept.Size = new System.Drawing.Size(183, 20);
            // 
            // lbDept
            // 
            this.lbDept.Location = new System.Drawing.Point(6, 12);
            // 
            // ncmbTime
            // 
            this.ncmbTime.Location = new System.Drawing.Point(10, 72);
            this.ncmbTime.Size = new System.Drawing.Size(381, 21);
            // 
            // panelFilter
            // 
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFilter.Location = new System.Drawing.Point(0, 435);
            this.panelFilter.Size = new System.Drawing.Size(263, 44);
            // 
            // txtFilter
            // 
            this.txtFilter.Size = new System.Drawing.Size(184, 21);
            // 
            // panelCustomType
            // 
            this.panelCustomType.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCustomType.Location = new System.Drawing.Point(0, 109);
            this.panelCustomType.Size = new System.Drawing.Size(263, 44);
            // 
            // cmbCustomType
            // 
            this.cmbCustomType.Location = new System.Drawing.Point(61, 7);
            this.cmbCustomType.Size = new System.Drawing.Size(183, 20);
            // 
            // neuLabel4
            // 
            this.neuLabel4.Location = new System.Drawing.Point(6, 6);
            // 
            // neuTreeView1
            // 
            this.neuTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTreeView1.HideSelection = false;
            this.neuTreeView1.Location = new System.Drawing.Point(0, 153);
            this.neuTreeView1.Name = "neuTreeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "表单列表";
            this.neuTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.neuTreeView1.Size = new System.Drawing.Size(263, 282);
            this.neuTreeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView1.TabIndex = 23;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 499);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(802, 54);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 6;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "附加信息";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(540, 26);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(185, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "没有可选择的科室说明您没有权限";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(23, 26);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(425, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "根据科室、时间设置查询表单列表，点击列表中的表单会根据分类显示详细数据";
            // 
            // ucDailyCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDailyCheck";
            this.Size = new System.Drawing.Size(802, 553);
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
            this.nGroupBoxQueryCondition.PerformLayout();
            this.panelQueryConditions.ResumeLayout(false);
            this.panelTime.ResumeLayout(false);
            this.panelDept.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelCustomType.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
    }
}
