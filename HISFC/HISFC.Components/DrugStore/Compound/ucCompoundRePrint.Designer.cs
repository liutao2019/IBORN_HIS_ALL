﻿namespace FS.HISFC.Components.DrugStore.Compound
{
    partial class ucCompoundRePrint
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType3 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCompoundRePrint));
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrderGroup = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCompoundGroup = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpApply = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpApply_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.neuGroupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fpApply);
            this.splitContainer1.Size = new System.Drawing.Size(714, 425);
            this.splitContainer1.SplitterDistance = 66;
            this.splitContainer1.TabIndex = 0;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.cmbOrderGroup);
            this.neuGroupBox1.Controls.Add(this.lbInfo);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.txtCompoundGroup);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(714, 66);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel3.Location = new System.Drawing.Point(6, 17);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "批  次：";
            // 
            // cmbOrderGroup
            // 
            this.cmbOrderGroup.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOrderGroup.FormattingEnabled = true;
            this.cmbOrderGroup.IsFlat = true;
            this.cmbOrderGroup.IsLike = true;
            this.cmbOrderGroup.Location = new System.Drawing.Point(60, 13);
            this.cmbOrderGroup.Name = "cmbOrderGroup";
            this.cmbOrderGroup.PopForm = null;
            this.cmbOrderGroup.ShowCustomerList = false;
            this.cmbOrderGroup.ShowID = false;
            this.cmbOrderGroup.Size = new System.Drawing.Size(82, 20);
            this.cmbOrderGroup.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbOrderGroup.TabIndex = 4;
            this.cmbOrderGroup.Tag = "";
            this.cmbOrderGroup.ToolBarUse = false;
            this.cmbOrderGroup.SelectedIndexChanged += new System.EventHandler(this.cmbOrderGroup_SelectedIndexChanged);
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.ForeColor = System.Drawing.Color.Blue;
            this.lbInfo.Location = new System.Drawing.Point(6, 45);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(53, 12);
            this.lbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInfo.TabIndex = 3;
            this.lbInfo.Text = "患者信息";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(424, 17);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(245, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "根据批次流水号检索数据，进行配置标签补打";
            // 
            // txtCompoundGroup
            // 
            this.txtCompoundGroup.Location = new System.Drawing.Point(237, 13);
            this.txtCompoundGroup.Name = "txtCompoundGroup";
            this.txtCompoundGroup.Size = new System.Drawing.Size(180, 21);
            this.txtCompoundGroup.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCompoundGroup.TabIndex = 1;
            this.txtCompoundGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompoundGroup_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(157, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "批次流水号：";
            // 
            // fpApply
            // 
            this.fpApply.About = "2.5.2007.2005";
            this.fpApply.AccessibleDescription = "fpApply, Sheet1";
            this.fpApply.BackColor = System.Drawing.Color.White;
            this.fpApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpApply.FileName = "";
            this.fpApply.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpApply.IsAutoSaveGridStatus = false;
            this.fpApply.IsCanCustomConfigColumn = false;
            this.fpApply.Location = new System.Drawing.Point(0, 0);
            this.fpApply.Name = "fpApply";
            this.fpApply.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpApply_Sheet1});
            this.fpApply.Size = new System.Drawing.Size(714, 355);
            this.fpApply.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpApply.TabIndex = 1;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpApply.TextTipAppearance = tipAppearance3;
            this.fpApply.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpApply_Sheet1
            // 
            this.fpApply_Sheet1.Reset();
            this.fpApply_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpApply_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpApply_Sheet1.ColumnCount = 14;
            this.fpApply_Sheet1.RowCount = 0;
            this.fpApply_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "[床号]姓名";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "选中";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "药品[规格]";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "零售价";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "用量";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "总量";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "单位";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "频次";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "用法";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "用药时间";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "开方医生";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "申请时间";
            this.fpApply_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "批次号";
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpApply_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.Columns.Get(0).Label = "[床号]姓名";
            this.fpApply_Sheet1.Columns.Get(0).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.fpApply_Sheet1.Columns.Get(0).Width = 78F;
            this.fpApply_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.SeaShell;
            this.fpApply_Sheet1.Columns.Get(1).CellType = checkBoxCellType3;
            this.fpApply_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpApply_Sheet1.Columns.Get(1).Label = "选中";
            this.fpApply_Sheet1.Columns.Get(1).Locked = false;
            this.fpApply_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.fpApply_Sheet1.Columns.Get(1).Width = 38F;
            this.fpApply_Sheet1.Columns.Get(2).Label = "药品[规格]";
            this.fpApply_Sheet1.Columns.Get(2).Width = 140F;
            this.fpApply_Sheet1.Columns.Get(3).CellType = numberCellType7;
            this.fpApply_Sheet1.Columns.Get(3).Label = "零售价";
            this.fpApply_Sheet1.Columns.Get(4).CellType = numberCellType8;
            this.fpApply_Sheet1.Columns.Get(4).Label = "用量";
            this.fpApply_Sheet1.Columns.Get(5).Label = "单位";
            this.fpApply_Sheet1.Columns.Get(5).Width = 40F;
            this.fpApply_Sheet1.Columns.Get(6).CellType = numberCellType9;
            this.fpApply_Sheet1.Columns.Get(6).Label = "总量";
            this.fpApply_Sheet1.Columns.Get(7).Label = "单位";
            this.fpApply_Sheet1.Columns.Get(7).Width = 40F;
            this.fpApply_Sheet1.Columns.Get(8).Label = "频次";
            this.fpApply_Sheet1.Columns.Get(8).Width = 46F;
            this.fpApply_Sheet1.Columns.Get(9).Label = "用法";
            this.fpApply_Sheet1.Columns.Get(9).Width = 50F;
            dateTimeCellType3.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType3.Calendar")));
            dateTimeCellType3.DateDefault = new System.DateTime(2007, 8, 21, 10, 9, 47, 0);
            dateTimeCellType3.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;
            dateTimeCellType3.TimeDefault = new System.DateTime(2007, 8, 21, 10, 9, 47, 0);
            this.fpApply_Sheet1.Columns.Get(10).CellType = dateTimeCellType3;
            this.fpApply_Sheet1.Columns.Get(10).Label = "用药时间";
            this.fpApply_Sheet1.Columns.Get(10).Width = 113F;
            this.fpApply_Sheet1.Columns.Get(11).CellType = textCellType5;
            this.fpApply_Sheet1.Columns.Get(11).Label = "开方医生";
            this.fpApply_Sheet1.Columns.Get(11).Width = 70F;
            this.fpApply_Sheet1.Columns.Get(12).Label = "申请时间";
            this.fpApply_Sheet1.Columns.Get(12).Width = 104F;
            this.fpApply_Sheet1.Columns.Get(13).CellType = textCellType6;
            this.fpApply_Sheet1.Columns.Get(13).Label = "批次号";
            this.fpApply_Sheet1.Columns.Get(13).Width = 103F;
            this.fpApply_Sheet1.DefaultStyle.Locked = true;
            this.fpApply_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpApply_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpApply_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpApply_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpApply_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpApply_Sheet1.SheetCornerStyle.Locked = false;
            this.fpApply_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpApply_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpApply.SetActiveViewport(1, 0);
            // 
            // ucCompoundRePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucCompoundRePrint";
            this.Size = new System.Drawing.Size(714, 425);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpApply_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCompoundGroup;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbInfo;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpApply;
        private FarPoint.Win.Spread.SheetView fpApply_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOrderGroup;
    }
}
