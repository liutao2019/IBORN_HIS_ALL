﻿namespace FS.SOC.Local.Pharmacy.Report.NanZhuang
{
    partial class ucRecipeAppraise
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
            this.ncbDiagnose = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbDoctor = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nPatientInfo = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
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
            // neuGroupBox2
            // 
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 95);
            this.neuGroupBox2.Size = new System.Drawing.Size(781, 253);
            // 
            // panelPrint
            // 
            this.panelPrint.Size = new System.Drawing.Size(775, 233);
            // 
            // panelDataView
            // 
            this.panelDataView.Size = new System.Drawing.Size(775, 171);
            // 
            // fpSpread1
            // 
            this.fpSpread1.Size = new System.Drawing.Size(775, 171);
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
            this.nGroupBoxQueryCondition.Controls.Add(this.nPatientInfo);
            this.nGroupBoxQueryCondition.Controls.Add(this.ncbDiagnose);
            this.nGroupBoxQueryCondition.Controls.Add(this.neuLabel2);
            this.nGroupBoxQueryCondition.Controls.Add(this.neuLabel3);
            this.nGroupBoxQueryCondition.Controls.Add(this.ncbDoctor);
            this.nGroupBoxQueryCondition.Controls.Add(this.neuLabel1);
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(781, 95);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.panelQueryConditions, 0);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.neuLabel1, 0);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.ncbDoctor, 0);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.neuLabel3, 0);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.neuLabel2, 0);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.ncbDiagnose, 0);
            this.nGroupBoxQueryCondition.Controls.SetChildIndex(this.nPatientInfo, 0);
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Dock = System.Windows.Forms.DockStyle.Top;
            // 
            // panelFilter
            // 
            this.panelFilter.Size = new System.Drawing.Size(160, 44);
            // 
            // ncbDiagnose
            // 
            this.ncbDiagnose.ArrowBackColor = System.Drawing.Color.Silver;
            this.ncbDiagnose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.ncbDiagnose.FormattingEnabled = true;
            this.ncbDiagnose.IsEnter2Tab = false;
            this.ncbDiagnose.IsFlat = false;
            this.ncbDiagnose.IsLike = true;
            this.ncbDiagnose.IsListOnly = false;
            this.ncbDiagnose.IsPopForm = true;
            this.ncbDiagnose.IsShowCustomerList = false;
            this.ncbDiagnose.IsShowID = false;
            this.ncbDiagnose.Location = new System.Drawing.Point(409, 69);
            this.ncbDiagnose.Name = "ncbDiagnose";
            this.ncbDiagnose.PopForm = null;
            this.ncbDiagnose.ShowCustomerList = false;
            this.ncbDiagnose.ShowID = false;
            this.ncbDiagnose.Size = new System.Drawing.Size(350, 20);
            this.ncbDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncbDiagnose.TabIndex = 12;
            this.ncbDiagnose.Tag = "";
            this.ncbDiagnose.ToolBarUse = false;
            this.ncbDiagnose.Visible = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(369, 73);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 11;
            this.neuLabel2.Text = "诊断：";
            this.neuLabel2.Visible = false;
            // 
            // ncbDoctor
            // 
            this.ncbDoctor.ArrowBackColor = System.Drawing.Color.Silver;
            this.ncbDoctor.FormattingEnabled = true;
            this.ncbDoctor.IsEnter2Tab = false;
            this.ncbDoctor.IsFlat = false;
            this.ncbDoctor.IsLike = true;
            this.ncbDoctor.IsListOnly = false;
            this.ncbDoctor.IsPopForm = true;
            this.ncbDoctor.IsShowCustomerList = false;
            this.ncbDoctor.IsShowID = false;
            this.ncbDoctor.Location = new System.Drawing.Point(63, 69);
            this.ncbDoctor.Name = "ncbDoctor";
            this.ncbDoctor.PopForm = null;
            this.ncbDoctor.ShowCustomerList = false;
            this.ncbDoctor.ShowID = false;
            this.ncbDoctor.Size = new System.Drawing.Size(136, 20);
            this.ncbDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncbDoctor.TabIndex = 10;
            this.ncbDoctor.Tag = "";
            this.ncbDoctor.ToolBarUse = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(18, 73);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 9;
            this.neuLabel1.Text = "医生：";
            // 
            // nPatientInfo
            // 
            this.nPatientInfo.ArrowBackColor = System.Drawing.Color.Silver;
            this.nPatientInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.nPatientInfo.FormattingEnabled = true;
            this.nPatientInfo.IsEnter2Tab = false;
            this.nPatientInfo.IsFlat = false;
            this.nPatientInfo.IsLike = true;
            this.nPatientInfo.IsListOnly = false;
            this.nPatientInfo.IsPopForm = true;
            this.nPatientInfo.IsShowCustomerList = false;
            this.nPatientInfo.IsShowID = false;
            this.nPatientInfo.Location = new System.Drawing.Point(259, 69);
            this.nPatientInfo.Name = "nPatientInfo";
            this.nPatientInfo.PopForm = null;
            this.nPatientInfo.ShowCustomerList = false;
            this.nPatientInfo.ShowID = false;
            this.nPatientInfo.Size = new System.Drawing.Size(93, 20);
            this.nPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.nPatientInfo.TabIndex = 14;
            this.nPatientInfo.Tag = "";
            this.nPatientInfo.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(216, 73);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 13;
            this.neuLabel3.Text = "患者：";
            // 
            // ucRecipeAppraise
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
            this.Name = "ucRecipeAppraise";
            this.RHHGridLineColor = System.Drawing.Color.LightGray;
            this.RHVGridLineColor = System.Drawing.Color.LightGray;
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
            this.nGroupBoxQueryCondition.PerformLayout();
            this.panelQueryConditions.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelTime.ResumeLayout(false);
            this.panelDept.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuComboBox ncbDiagnose;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncbDoctor;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox nPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
    }
}
