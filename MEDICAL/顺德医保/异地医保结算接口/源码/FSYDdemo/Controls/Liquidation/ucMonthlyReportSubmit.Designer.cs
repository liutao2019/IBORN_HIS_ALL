namespace FoShanYDSI.Controls.Liquidation
{
    partial class ucMonthlyReportSubmit
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.panelAll = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panelPrint = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panelDataView = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpTotal = new FarPoint.Win.Spread.SheetView();
            this.panelAdditionTitle = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nGroupBoxQueryCondition = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panelQueryConditions = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panelFilter = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panelCustomType = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmbCustomType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panelTime = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dtMonth = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.lbTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbFilter = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPhone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbTB = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbFZ = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbFH = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.panelAll.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.panelPrint.SuspendLayout();
            this.panelDataView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpTotal)).BeginInit();
            this.panelAdditionTitle.SuspendLayout();
            this.nGroupBoxQueryCondition.SuspendLayout();
            this.panelQueryConditions.SuspendLayout();
            this.panelCustomType.SuspendLayout();
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
            this.panelAll.Size = new System.Drawing.Size(950, 681);
            this.panelAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelAll.TabIndex = 12;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.panelPrint);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 64);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(950, 617);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 5;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "查询结果";
            // 
            // panelPrint
            // 
            this.panelPrint.Controls.Add(this.panelDataView);
            this.panelPrint.Controls.Add(this.panelAdditionTitle);
            this.panelPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrint.Location = new System.Drawing.Point(3, 17);
            this.panelPrint.Name = "panelPrint";
            this.panelPrint.Size = new System.Drawing.Size(944, 597);
            this.panelPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelPrint.TabIndex = 5;
            // 
            // panelDataView
            // 
            this.panelDataView.Controls.Add(this.fpSpread1);
            this.panelDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataView.Location = new System.Drawing.Point(0, 50);
            this.panelDataView.Name = "panelDataView";
            this.panelDataView.Size = new System.Drawing.Size(944, 547);
            this.panelDataView.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelDataView.TabIndex = 7;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpTotal});
            this.fpSpread1.Size = new System.Drawing.Size(944, 547);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpTotal
            // 
            this.fpTotal.Reset();
            this.fpTotal.SheetName = "Sheet1";
            // 
            // panelAdditionTitle
            // 
            this.panelAdditionTitle.BackColor = System.Drawing.Color.White;
            this.panelAdditionTitle.Controls.Add(this.cmbFH);
            this.panelAdditionTitle.Controls.Add(this.cmbFZ);
            this.panelAdditionTitle.Controls.Add(this.cmbTB);
            this.panelAdditionTitle.Controls.Add(this.neuLabel3);
            this.panelAdditionTitle.Controls.Add(this.neuLabel2);
            this.panelAdditionTitle.Controls.Add(this.neuLabel1);
            this.panelAdditionTitle.Controls.Add(this.nlbFilter);
            this.panelAdditionTitle.Controls.Add(this.txtPhone);
            this.panelAdditionTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAdditionTitle.Location = new System.Drawing.Point(0, 0);
            this.panelAdditionTitle.Name = "panelAdditionTitle";
            this.panelAdditionTitle.Size = new System.Drawing.Size(944, 50);
            this.panelAdditionTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelAdditionTitle.TabIndex = 6;
            // 
            // nGroupBoxQueryCondition
            // 
            this.nGroupBoxQueryCondition.Controls.Add(this.panelQueryConditions);
            this.nGroupBoxQueryCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.nGroupBoxQueryCondition.Location = new System.Drawing.Point(0, 0);
            this.nGroupBoxQueryCondition.Name = "nGroupBoxQueryCondition";
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(950, 64);
            this.nGroupBoxQueryCondition.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nGroupBoxQueryCondition.TabIndex = 4;
            this.nGroupBoxQueryCondition.TabStop = false;
            this.nGroupBoxQueryCondition.Text = "查询条件";
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Controls.Add(this.panelFilter);
            this.panelQueryConditions.Controls.Add(this.panelCustomType);
            this.panelQueryConditions.Controls.Add(this.panelTime);
            this.panelQueryConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQueryConditions.Location = new System.Drawing.Point(3, 17);
            this.panelQueryConditions.Name = "panelQueryConditions";
            this.panelQueryConditions.Size = new System.Drawing.Size(944, 44);
            this.panelQueryConditions.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelQueryConditions.TabIndex = 3;
            // 
            // panelFilter
            // 
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFilter.Location = new System.Drawing.Point(431, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(451, 44);
            this.panelFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelFilter.TabIndex = 21;
            // 
            // panelCustomType
            // 
            this.panelCustomType.Controls.Add(this.cmbCustomType);
            this.panelCustomType.Controls.Add(this.neuLabel4);
            this.panelCustomType.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCustomType.Location = new System.Drawing.Point(198, 0);
            this.panelCustomType.Name = "panelCustomType";
            this.panelCustomType.Size = new System.Drawing.Size(233, 44);
            this.panelCustomType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelCustomType.TabIndex = 20;
            // 
            // cmbCustomType
            // 
            this.cmbCustomType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCustomType.IsEnter2Tab = false;
            this.cmbCustomType.IsFlat = false;
            this.cmbCustomType.IsLike = true;
            this.cmbCustomType.IsListOnly = false;
            this.cmbCustomType.IsPopForm = true;
            this.cmbCustomType.IsShowCustomerList = false;
            this.cmbCustomType.IsShowID = false;
            this.cmbCustomType.Location = new System.Drawing.Point(89, 11);
            this.cmbCustomType.Name = "cmbCustomType";
            this.cmbCustomType.PopForm = null;
            this.cmbCustomType.ShowCustomerList = false;
            this.cmbCustomType.ShowID = false;
            this.cmbCustomType.Size = new System.Drawing.Size(118, 20);
            this.cmbCustomType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCustomType.TabIndex = 19;
            this.cmbCustomType.Tag = "";
            this.cmbCustomType.ToolBarUse = false;
            this.cmbCustomType.SelectedIndexChanged += new System.EventHandler(this.cmbCustomType_SelectedIndexChanged);
            // 
            // neuLabel4
            // 
            this.neuLabel4.ForeColor = System.Drawing.Color.Black;
            this.neuLabel4.Location = new System.Drawing.Point(11, 11);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(74, 20);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 18;
            this.neuLabel4.Text = "险种类型：";
            this.neuLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelTime
            // 
            this.panelTime.Controls.Add(this.dtMonth);
            this.panelTime.Controls.Add(this.lbTime);
            this.panelTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTime.Location = new System.Drawing.Point(0, 0);
            this.panelTime.Name = "panelTime";
            this.panelTime.Size = new System.Drawing.Size(198, 44);
            this.panelTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelTime.TabIndex = 11;
            // 
            // dtMonth
            // 
            this.dtMonth.CustomFormat = "yyyy年MM月";
            this.dtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMonth.IsEnter2Tab = false;
            this.dtMonth.Location = new System.Drawing.Point(83, 11);
            this.dtMonth.Name = "dtMonth";
            this.dtMonth.Size = new System.Drawing.Size(92, 21);
            this.dtMonth.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtMonth.TabIndex = 10;
            // 
            // lbTime
            // 
            this.lbTime.ForeColor = System.Drawing.Color.Blue;
            this.lbTime.Location = new System.Drawing.Point(15, 11);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(66, 21);
            this.lbTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTime.TabIndex = 11;
            this.lbTime.Text = "申报年月：";
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Black;
            this.neuLabel3.Location = new System.Drawing.Point(594, 18);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 14;
            this.neuLabel3.Text = "联系电话：";
            this.neuLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Black;
            this.neuLabel2.Location = new System.Drawing.Point(401, 18);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 13;
            this.neuLabel2.Text = "填表人：";
            this.neuLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Black;
            this.neuLabel1.Location = new System.Drawing.Point(208, 18);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 12;
            this.neuLabel1.Text = "复核人：";
            this.neuLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nlbFilter
            // 
            this.nlbFilter.AutoSize = true;
            this.nlbFilter.ForeColor = System.Drawing.Color.Black;
            this.nlbFilter.Location = new System.Drawing.Point(15, 18);
            this.nlbFilter.Name = "nlbFilter";
            this.nlbFilter.Size = new System.Drawing.Size(53, 12);
            this.nlbFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbFilter.TabIndex = 11;
            this.nlbFilter.Text = "负责人：";
            this.nlbFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhone
            // 
            this.txtPhone.IsEnter2Tab = false;
            this.txtPhone.Location = new System.Drawing.Point(670, 14);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(166, 21);
            this.txtPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPhone.TabIndex = 10;
            // 
            // cmbTB
            // 
            this.cmbTB.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbTB.IsEnter2Tab = false;
            this.cmbTB.IsFlat = false;
            this.cmbTB.IsLike = true;
            this.cmbTB.IsListOnly = false;
            this.cmbTB.IsPopForm = true;
            this.cmbTB.IsShowCustomerList = false;
            this.cmbTB.IsShowID = false;
            this.cmbTB.Location = new System.Drawing.Point(454, 14);
            this.cmbTB.Name = "cmbTB";
            this.cmbTB.PopForm = null;
            this.cmbTB.ShowCustomerList = false;
            this.cmbTB.ShowID = false;
            this.cmbTB.Size = new System.Drawing.Size(118, 20);
            this.cmbTB.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbTB.TabIndex = 20;
            this.cmbTB.Tag = "";
            this.cmbTB.ToolBarUse = false;
            // 
            // cmbFZ
            // 
            this.cmbFZ.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbFZ.IsEnter2Tab = false;
            this.cmbFZ.IsFlat = false;
            this.cmbFZ.IsLike = true;
            this.cmbFZ.IsListOnly = false;
            this.cmbFZ.IsPopForm = true;
            this.cmbFZ.IsShowCustomerList = false;
            this.cmbFZ.IsShowID = false;
            this.cmbFZ.Location = new System.Drawing.Point(70, 14);
            this.cmbFZ.Name = "cmbFZ";
            this.cmbFZ.PopForm = null;
            this.cmbFZ.ShowCustomerList = false;
            this.cmbFZ.ShowID = false;
            this.cmbFZ.Size = new System.Drawing.Size(118, 20);
            this.cmbFZ.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbFZ.TabIndex = 21;
            this.cmbFZ.Tag = "";
            this.cmbFZ.ToolBarUse = false;
            // 
            // cmbFH
            // 
            this.cmbFH.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbFH.IsEnter2Tab = false;
            this.cmbFH.IsFlat = false;
            this.cmbFH.IsLike = true;
            this.cmbFH.IsListOnly = false;
            this.cmbFH.IsPopForm = true;
            this.cmbFH.IsShowCustomerList = false;
            this.cmbFH.IsShowID = false;
            this.cmbFH.Location = new System.Drawing.Point(263, 14);
            this.cmbFH.Name = "cmbFH";
            this.cmbFH.PopForm = null;
            this.cmbFH.ShowCustomerList = false;
            this.cmbFH.ShowID = false;
            this.cmbFH.Size = new System.Drawing.Size(118, 20);
            this.cmbFH.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbFH.TabIndex = 22;
            this.cmbFH.Tag = "";
            this.cmbFH.ToolBarUse = false;
            // 
            // ucMonthlyReportSubmit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAll);
            this.Name = "ucMonthlyReportSubmit";
            this.Size = new System.Drawing.Size(950, 681);
            this.panelAll.ResumeLayout(false);
            this.neuGroupBox2.ResumeLayout(false);
            this.panelPrint.ResumeLayout(false);
            this.panelDataView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpTotal)).EndInit();
            this.panelAdditionTitle.ResumeLayout(false);
            this.panelAdditionTitle.PerformLayout();
            this.nGroupBoxQueryCondition.ResumeLayout(false);
            this.panelQueryConditions.ResumeLayout(false);
            this.panelCustomType.ResumeLayout(false);
            this.panelTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public FS.FrameWork.WinForms.Controls.NeuPanel panelAll;
        public FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelPrint;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelDataView;
        public FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpTotal;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelAdditionTitle;
        public FS.FrameWork.WinForms.Controls.NeuGroupBox nGroupBoxQueryCondition;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelQueryConditions;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelFilter;
        private FS.FrameWork.WinForms.Controls.NeuPanel panelCustomType;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbCustomType;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelTime;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtMonth;
        public FS.FrameWork.WinForms.Controls.NeuLabel lbTime;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        public FS.FrameWork.WinForms.Controls.NeuLabel nlbFilter;
        public FS.FrameWork.WinForms.Controls.NeuTextBox txtPhone;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbFH;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbFZ;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbTB;
    }
}
