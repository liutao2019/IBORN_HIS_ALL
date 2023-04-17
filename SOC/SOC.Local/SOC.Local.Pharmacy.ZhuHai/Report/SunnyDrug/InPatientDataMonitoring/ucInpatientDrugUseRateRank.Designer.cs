namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.InPatientDataMonitoring
{
    partial class ucInpatientDrugUseRateRank
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
            this.cmbDrugType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbDoct = new FS.FrameWork.WinForms.Controls.NeuLabel();
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
            this.SuspendLayout();
            // 
            // panelAll
            // 
            this.panelAll.Size = new System.Drawing.Size(1225, 348);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Size = new System.Drawing.Size(1225, 284);
            // 
            // panelPrint
            // 
            this.panelPrint.Size = new System.Drawing.Size(1219, 264);
            // 
            // panelTitle
            // 
            this.panelTitle.Size = new System.Drawing.Size(1219, 47);
            // 
            // panelAdditionTitle
            // 
            this.panelAdditionTitle.Size = new System.Drawing.Size(1219, 15);
            // 
            // lbAdditionTitleMid
            // 
            this.lbAdditionTitleMid.Location = new System.Drawing.Point(834, 0);
            // 
            // panelDataView
            // 
            this.panelDataView.Size = new System.Drawing.Size(1219, 202);
            // 
            // fpSpread1
            // 
            this.fpSpread1.Size = new System.Drawing.Size(1219, 202);
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
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(1225, 64);
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Size = new System.Drawing.Size(1219, 44);
            // 
            // panelTime
            // 
            this.panelTime.Location = new System.Drawing.Point(221, 0);
            // 
            // panelDept
            // 
            this.panelDept.Controls.Add(this.cmbDrugType);
            this.panelDept.Controls.Add(this.lbDoct);
            this.panelDept.Size = new System.Drawing.Size(221, 44);
            this.panelDept.Controls.SetChildIndex(this.lbDoct, 0);
            this.panelDept.Controls.SetChildIndex(this.cmbDrugType, 0);
            this.panelDept.Controls.SetChildIndex(this.lbDept, 0);
            this.panelDept.Controls.SetChildIndex(this.cmbDept, 0);
            // 
            // cmbDept
            // 
            this.cmbDept.Visible = false;
            // 
            // lbDept
            // 
            this.lbDept.Visible = false;
            // 
            // ncmbTime
            // 
            this.ncmbTime.Size = new System.Drawing.Size(374, 21);
            // 
            // panelFilter
            // 
            this.panelFilter.Location = new System.Drawing.Point(849, 0);
            // 
            // cmbDrugType
            // 
            this.cmbDrugType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDrugType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDrugType.FormattingEnabled = true;
            this.cmbDrugType.IsEnter2Tab = false;
            this.cmbDrugType.IsFlat = false;
            this.cmbDrugType.IsLike = true;
            this.cmbDrugType.IsListOnly = false;
            this.cmbDrugType.IsPopForm = true;
            this.cmbDrugType.IsShowCustomerList = false;
            this.cmbDrugType.IsShowID = false;
            this.cmbDrugType.IsShowIDAndName = false;
            this.cmbDrugType.Location = new System.Drawing.Point(76, 11);
            this.cmbDrugType.Name = "cmbDrugType";
            this.cmbDrugType.ShowCustomerList = false;
            this.cmbDrugType.ShowID = false;
            this.cmbDrugType.Size = new System.Drawing.Size(121, 20);
            this.cmbDrugType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDrugType.TabIndex = 6;
            this.cmbDrugType.Tag = "";
            this.cmbDrugType.ToolBarUse = false;
            // 
            // lbDoct
            // 
            this.lbDoct.AutoSize = true;
            this.lbDoct.Location = new System.Drawing.Point(13, 15);
            this.lbDoct.Name = "lbDoct";
            this.lbDoct.Size = new System.Drawing.Size(65, 12);
            this.lbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDoct.TabIndex = 7;
            this.lbDoct.Text = "药品分类：";
            // 
            // ucInpatientDrugUseRateRank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucInpatientDrugUseRateRank";
            this.Size = new System.Drawing.Size(1225, 348);
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
            this.panelDept.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDrugType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDoct;

    }
}
