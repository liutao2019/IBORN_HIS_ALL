namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    partial class ucCallBackNum
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
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuCmbType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuDtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuComDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuBtnExit = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuBtnPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuBtnQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(645, 279);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuCmbType);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.neuDtEnd);
            this.neuPanel1.Controls.Add(this.neuDtBegin);
            this.neuPanel1.Controls.Add(this.neuComDept);
            this.neuPanel1.Controls.Add(this.neuBtnExit);
            this.neuPanel1.Controls.Add(this.neuBtnPrint);
            this.neuPanel1.Controls.Add(this.neuBtnQuery);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(645, 62);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // neuCmbType
            // 
            this.neuCmbType.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuCmbType.FormattingEnabled = true;
            this.neuCmbType.IsEnter2Tab = false;
            this.neuCmbType.IsFlat = true;
            this.neuCmbType.IsLike = true;
            this.neuCmbType.Location = new System.Drawing.Point(80, 3);
            this.neuCmbType.Name = "neuCmbType";
            this.neuCmbType.PopForm = null;
            this.neuCmbType.ShowCustomerList = false;
            this.neuCmbType.ShowID = false;
            this.neuCmbType.Size = new System.Drawing.Size(189, 20);
            this.neuCmbType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCmbType.TabIndex = 9;
            this.neuCmbType.Tag = "";
            this.neuCmbType.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(15, 6);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 8;
            this.neuLabel3.Text = "查询类型：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(9, 34);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 7;
            this.neuLabel2.Text = "科室名称：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(316, 6);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "时间：";
            // 
            // neuDtEnd
            // 
            this.neuDtEnd.CustomFormat = "yyyy-MM-dd 23:59:59";
            this.neuDtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDtEnd.IsEnter2Tab = false;
            this.neuDtEnd.Location = new System.Drawing.Point(507, 2);
            this.neuDtEnd.Name = "neuDtEnd";
            this.neuDtEnd.Size = new System.Drawing.Size(137, 21);
            this.neuDtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDtEnd.TabIndex = 5;
            // 
            // neuDtBegin
            // 
            this.neuDtBegin.CustomFormat = "yyyy-MM-dd 00:00:00";
            this.neuDtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDtBegin.IsEnter2Tab = false;
            this.neuDtBegin.Location = new System.Drawing.Point(363, 2);
            this.neuDtBegin.Name = "neuDtBegin";
            this.neuDtBegin.Size = new System.Drawing.Size(138, 21);
            this.neuDtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDtBegin.TabIndex = 4;
            // 
            // neuComDept
            // 
            this.neuComDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuComDept.FormattingEnabled = true;
            this.neuComDept.IsEnter2Tab = false;
            this.neuComDept.IsFlat = true;
            this.neuComDept.IsLike = true;
            this.neuComDept.Location = new System.Drawing.Point(80, 31);
            this.neuComDept.Name = "neuComDept";
            this.neuComDept.PopForm = null;
            this.neuComDept.ShowCustomerList = false;
            this.neuComDept.ShowID = false;
            this.neuComDept.Size = new System.Drawing.Size(189, 20);
            this.neuComDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuComDept.TabIndex = 3;
            this.neuComDept.Tag = "";
            this.neuComDept.ToolBarUse = false;
            // 
            // neuBtnExit
            // 
            this.neuBtnExit.Enabled = false;
            this.neuBtnExit.Location = new System.Drawing.Point(559, 31);
            this.neuBtnExit.Name = "neuBtnExit";
            this.neuBtnExit.Size = new System.Drawing.Size(75, 23);
            this.neuBtnExit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnExit.TabIndex = 2;
            this.neuBtnExit.Text = "退出";
            this.neuBtnExit.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnExit.UseVisualStyleBackColor = true;
            this.neuBtnExit.Visible = false;
            this.neuBtnExit.Click += new System.EventHandler(this.neuBtnExit_Click);
            // 
            // neuBtnPrint
            // 
            this.neuBtnPrint.Location = new System.Drawing.Point(478, 31);
            this.neuBtnPrint.Name = "neuBtnPrint";
            this.neuBtnPrint.Size = new System.Drawing.Size(75, 23);
            this.neuBtnPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnPrint.TabIndex = 1;
            this.neuBtnPrint.Text = "打印";
            this.neuBtnPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnPrint.UseVisualStyleBackColor = true;
            this.neuBtnPrint.Click += new System.EventHandler(this.neuBtnPrint_Click);
            // 
            // neuBtnQuery
            // 
            this.neuBtnQuery.Location = new System.Drawing.Point(397, 31);
            this.neuBtnQuery.Name = "neuBtnQuery";
            this.neuBtnQuery.Size = new System.Drawing.Size(75, 23);
            this.neuBtnQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnQuery.TabIndex = 0;
            this.neuBtnQuery.Text = "检索病历";
            this.neuBtnQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnQuery.UseVisualStyleBackColor = true;
            this.neuBtnQuery.Click += new System.EventHandler(this.neuBtnQuery_Click);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 62);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(645, 279);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuPanel2);
            this.neuPanel3.Controls.Add(this.neuPanel1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(645, 341);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 3;
            // 
            // ucCallBackNum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.neuPanel3);
            this.Name = "ucCallBackNum";
            this.Size = new System.Drawing.Size(645, 341);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDtEnd;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDtBegin;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuComDept;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnExit;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnPrint;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnQuery;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuCmbType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
    }
}
