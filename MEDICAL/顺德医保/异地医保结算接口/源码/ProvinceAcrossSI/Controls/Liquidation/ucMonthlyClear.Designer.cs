namespace ProvinceAcrossSI.Controls.Liquidation
{
    partial class ucMonthlyClear
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.panelAll = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panelPrint = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpBalance = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpResult = new FarPoint.Win.Spread.SheetView();
            this.nGroupBoxQueryCondition = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panelQueryConditions = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panelTime = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dtMonth = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.lbTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panelAll.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.panelPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpResult)).BeginInit();
            this.nGroupBoxQueryCondition.SuspendLayout();
            this.panelQueryConditions.SuspendLayout();
            this.panelTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAll
            // 
            this.panelAll.Controls.Add(this.neuGroupBox2);
            this.panelAll.Controls.Add(this.nGroupBoxQueryCondition);
            this.panelAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAll.Location = new System.Drawing.Point(0, 0);
            this.panelAll.Name = "panelAll";
            this.panelAll.Size = new System.Drawing.Size(744, 572);
            this.panelAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelAll.TabIndex = 12;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.panelPrint);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 64);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(744, 508);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 5;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "查询结果";
            // 
            // panelPrint
            // 
            this.panelPrint.Controls.Add(this.fpBalance);
            this.panelPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrint.Location = new System.Drawing.Point(3, 17);
            this.panelPrint.Name = "panelPrint";
            this.panelPrint.Size = new System.Drawing.Size(738, 488);
            this.panelPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelPrint.TabIndex = 6;
            // 
            // fpBalance
            // 
            this.fpBalance.About = "3.0.2004.2005";
            this.fpBalance.AccessibleDescription = "fpBalance, Sheet1, Row 0, Column 0, ";
            this.fpBalance.BackColor = System.Drawing.Color.White;
            this.fpBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpBalance.FileName = "";
            this.fpBalance.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpBalance.IsAutoSaveGridStatus = false;
            this.fpBalance.IsCanCustomConfigColumn = false;
            this.fpBalance.Location = new System.Drawing.Point(0, 0);
            this.fpBalance.Name = "fpBalance";
            this.fpBalance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpBalance.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpResult});
            this.fpBalance.Size = new System.Drawing.Size(738, 488);
            this.fpBalance.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpBalance.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpBalance.TextTipAppearance = tipAppearance1;
            this.fpBalance.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpResult
            // 
            this.fpResult.Reset();
            this.fpResult.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpResult.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpResult.ColumnCount = 11;
            this.fpResult.RowCount = 2;
            this.fpResult.ColumnHeader.Cells.Get(0, 0).Value = "选中";
            this.fpResult.ColumnHeader.Cells.Get(0, 1).Value = "顺序号";
            this.fpResult.ColumnHeader.Cells.Get(0, 2).Value = "社会保障号码";
            this.fpResult.ColumnHeader.Cells.Get(0, 3).Value = "就诊登记号";
            this.fpResult.ColumnHeader.Cells.Get(0, 4).Value = "就诊结算时间";
            this.fpResult.ColumnHeader.Cells.Get(0, 5).Value = "结算流水号";
            this.fpResult.ColumnHeader.Cells.Get(0, 6).Value = "全额垫付标志";
            this.fpResult.ColumnHeader.Cells.Get(0, 7).Value = "总费用";
            this.fpResult.ColumnHeader.Cells.Get(0, 8).Value = "经办机构支付总额";
            this.fpResult.ColumnHeader.Cells.Get(0, 9).Value = "患者姓名";
            this.fpResult.ColumnHeader.Cells.Get(0, 10).Value = "合同单位";
            this.fpResult.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpResult.Columns.Get(0).Label = "选中";
            this.fpResult.Columns.Get(0).Width = 0F;
            this.fpResult.Columns.Get(1).CellType = textCellType1;
            this.fpResult.Columns.Get(1).Label = "顺序号";
            this.fpResult.Columns.Get(1).Locked = false;
            this.fpResult.Columns.Get(2).CellType = textCellType1;
            this.fpResult.Columns.Get(2).Label = "社会保障号码";
            this.fpResult.Columns.Get(2).Locked = false;
            this.fpResult.Columns.Get(2).Width = 140F;
            this.fpResult.Columns.Get(3).CellType = textCellType1;
            this.fpResult.Columns.Get(3).Label = "就诊登记号";
            this.fpResult.Columns.Get(3).Locked = false;
            this.fpResult.Columns.Get(3).Width = 160F;
            this.fpResult.Columns.Get(4).CellType = textCellType1;
            this.fpResult.Columns.Get(4).Label = "就诊结算时间";
            this.fpResult.Columns.Get(4).Locked = false;
            this.fpResult.Columns.Get(4).Width = 130F;
            this.fpResult.Columns.Get(5).CellType = textCellType1;
            this.fpResult.Columns.Get(5).Label = "结算流水号";
            this.fpResult.Columns.Get(5).Locked = false;
            this.fpResult.Columns.Get(5).Width = 100F;
            this.fpResult.Columns.Get(6).CellType = checkBoxCellType2;
            this.fpResult.Columns.Get(6).Label = "全额垫付标志";
            this.fpResult.Columns.Get(6).Locked = true;
            this.fpResult.Columns.Get(7).CellType = textCellType2;
            this.fpResult.Columns.Get(7).Label = "总费用";
            this.fpResult.Columns.Get(7).Locked = false;
            this.fpResult.Columns.Get(8).CellType = textCellType2;
            this.fpResult.Columns.Get(8).Label = "经办机构支付总额";
            this.fpResult.Columns.Get(8).Locked = false;
            this.fpResult.Columns.Get(9).CellType = textCellType2;
            this.fpResult.Columns.Get(9).Label = "患者姓名";
            this.fpResult.Columns.Get(9).Width = 120F;
            this.fpResult.Columns.Get(10).CellType = textCellType2;
            this.fpResult.Columns.Get(10).Label = "合同单位";
            this.fpResult.Columns.Get(10).Width = 100F;
            this.fpResult.RowHeader.Columns.Default.Resizable = false;
            this.fpResult.RowHeader.Columns.Get(0).Width = 37F;
            this.fpResult.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // nGroupBoxQueryCondition
            // 
            this.nGroupBoxQueryCondition.Controls.Add(this.panelQueryConditions);
            this.nGroupBoxQueryCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.nGroupBoxQueryCondition.Location = new System.Drawing.Point(0, 0);
            this.nGroupBoxQueryCondition.Name = "nGroupBoxQueryCondition";
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(744, 64);
            this.nGroupBoxQueryCondition.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nGroupBoxQueryCondition.TabIndex = 4;
            this.nGroupBoxQueryCondition.TabStop = false;
            this.nGroupBoxQueryCondition.Text = "查询条件";
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Controls.Add(this.panelTime);
            this.panelQueryConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQueryConditions.Location = new System.Drawing.Point(3, 17);
            this.panelQueryConditions.Name = "panelQueryConditions";
            this.panelQueryConditions.Size = new System.Drawing.Size(738, 44);
            this.panelQueryConditions.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelQueryConditions.TabIndex = 3;
            // 
            // panelTime
            // 
            this.panelTime.Controls.Add(this.dtMonth);
            this.panelTime.Controls.Add(this.lbTime);
            this.panelTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTime.Location = new System.Drawing.Point(0, 0);
            this.panelTime.Name = "panelTime";
            this.panelTime.Size = new System.Drawing.Size(244, 44);
            this.panelTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelTime.TabIndex = 11;
            // 
            // dtMonth
            // 
            this.dtMonth.CustomFormat = "yyyy年MM月";
            this.dtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMonth.IsEnter2Tab = false;
            this.dtMonth.Location = new System.Drawing.Point(112, 12);
            this.dtMonth.Name = "dtMonth";
            this.dtMonth.Size = new System.Drawing.Size(92, 21);
            this.dtMonth.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtMonth.TabIndex = 14;
            // 
            // lbTime
            // 
            this.lbTime.ForeColor = System.Drawing.Color.Blue;
            this.lbTime.Location = new System.Drawing.Point(42, 12);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(66, 21);
            this.lbTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTime.TabIndex = 15;
            this.lbTime.Text = "清分年月：";
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucMonthlyClear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAll);
            this.Name = "ucMonthlyClear";
            this.Size = new System.Drawing.Size(744, 572);
            this.panelAll.ResumeLayout(false);
            this.neuGroupBox2.ResumeLayout(false);
            this.panelPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpResult)).EndInit();
            this.nGroupBoxQueryCondition.ResumeLayout(false);
            this.panelQueryConditions.ResumeLayout(false);
            this.panelTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public FS.FrameWork.WinForms.Controls.NeuPanel panelAll;
        public FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        public FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        public FS.FrameWork.WinForms.Controls.NeuGroupBox nGroupBoxQueryCondition;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelQueryConditions;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelTime;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtMonth;
        public FS.FrameWork.WinForms.Controls.NeuLabel lbTime;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelPrint;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpBalance;
        private FarPoint.Win.Spread.SheetView fpResult;

    }
}
