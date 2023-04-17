namespace FS.SOC.Local.InpatientFee.ZhangCha
{
    partial class ucQueryInPatientInfo
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
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbInState = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
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
            this.SuspendLayout();
            // 
            // panelAll
            // 
            this.panelAll.Size = new System.Drawing.Size(885, 348);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Size = new System.Drawing.Size(885, 284);
            // 
            // panelPrint
            // 
            this.panelPrint.Size = new System.Drawing.Size(879, 264);
            // 
            // panelTitle
            // 
            this.panelTitle.Size = new System.Drawing.Size(879, 47);
            // 
            // panelAdditionTitle
            // 
            this.panelAdditionTitle.Size = new System.Drawing.Size(879, 15);
            // 
            // panelDataView
            // 
            this.panelDataView.Size = new System.Drawing.Size(879, 202);
            // 
            // fpSpread1
            // 
            this.fpSpread1.Size = new System.Drawing.Size(879, 202);
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
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(885, 64);
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Controls.Add(this.neuLabel2);
            this.panelQueryConditions.Controls.Add(this.cmbInState);
            this.panelQueryConditions.Size = new System.Drawing.Size(879, 44);
            this.panelQueryConditions.Controls.SetChildIndex(this.cmbInState, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.neuLabel2, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelDept, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelTime, 0);
            this.panelQueryConditions.Controls.SetChildIndex(this.panelFilter, 0);
            // 
            // panelFilter
            // 
            this.panelFilter.Controls.Add(this.neuLabel1);
            this.panelFilter.Controls.Add(this.cmbPact);
            this.panelFilter.Location = new System.Drawing.Point(561, 0);
            this.panelFilter.Size = new System.Drawing.Size(178, 44);
            this.panelFilter.Controls.SetChildIndex(this.txtFilter, 0);
            this.panelFilter.Controls.SetChildIndex(this.cmbPact, 0);
            this.panelFilter.Controls.SetChildIndex(this.neuLabel1, 0);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(15, 49);
            this.txtFilter.Visible = false;
            // 
            // panelTime
            // 
            this.panelTime.Location = new System.Drawing.Point(156, 0);
            this.panelTime.Size = new System.Drawing.Size(405, 44);
            // 
            // panelDept
            // 
            this.panelDept.Size = new System.Drawing.Size(156, 44);
            // 
            // cmbDept
            // 
            this.cmbDept.Location = new System.Drawing.Point(51, 12);
            this.cmbDept.Size = new System.Drawing.Size(98, 20);
            // 
            // lbDept
            // 
            this.lbDept.Location = new System.Drawing.Point(5, 12);
            // 
            // ncmbTime
            // 
            this.ncmbTime.Location = new System.Drawing.Point(50, 52);
            this.ncmbTime.Size = new System.Drawing.Size(349, 21);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(745, 18);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(35, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 13;
            this.neuLabel2.Text = "状态:";
            // 
            // cmbInState
            // 
            this.cmbInState.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbInState.FormattingEnabled = true;
            this.cmbInState.IsEnter2Tab = false;
            this.cmbInState.IsFlat = false;
            this.cmbInState.IsLike = true;
            this.cmbInState.IsListOnly = false;
            this.cmbInState.IsPopForm = true;
            this.cmbInState.IsShowCustomerList = false;
            this.cmbInState.IsShowID = false;
            this.cmbInState.Location = new System.Drawing.Point(781, 14);
            this.cmbInState.Name = "cmbInState";
            this.cmbInState.PopForm = null;
            this.cmbInState.ShowCustomerList = false;
            this.cmbInState.ShowID = false;
            this.cmbInState.Size = new System.Drawing.Size(91, 20);
            this.cmbInState.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbInState.TabIndex = 14;
            this.cmbInState.Tag = "";
            this.cmbInState.ToolBarUse = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(8, 18);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 17;
            this.neuLabel1.Text = "合同单位:";
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.Location = new System.Drawing.Point(69, 14);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.PopForm = null;
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(102, 20);
            this.cmbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 18;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            // 
            // ucQueryInPatientInfo
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
            this.Name = "ucQueryInPatientInfo";
            this.RHHGridLineColor = System.Drawing.Color.LightGray;
            this.RHVGridLineColor = System.Drawing.Color.LightGray;
            this.Size = new System.Drawing.Size(885, 348);
            this.VerticalGridLineColor = System.Drawing.Color.LightGray;
            this.Load += new System.EventHandler(this.ucQueryInPatientInfo_Load);
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
            this.panelQueryConditions.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelTime.ResumeLayout(false);
            this.panelDept.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbInState;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPact;
    }
}
