namespace FS.HISFC.Components.Order.Controls
{
    partial class ucOrderExecMultiInput
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
            this.btQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuRadioButton2 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rdoIsExamed = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.btSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.rdoIsAll = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtItemFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuRadioButton1 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btQuery
            // 
            this.btQuery.Location = new System.Drawing.Point(133, 38);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(79, 23);
            this.btQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btQuery.TabIndex = 2;
            this.btQuery.Text = "查询";
            this.btQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuRadioButton2);
            this.neuGroupBox1.Controls.Add(this.rdoIsExamed);
            this.neuGroupBox1.Controls.Add(this.btSave);
            this.neuGroupBox1.Controls.Add(this.btQuery);
            this.neuGroupBox1.Location = new System.Drawing.Point(533, 3);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(354, 73);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 3;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "操作";
            // 
            // neuRadioButton2
            // 
            this.neuRadioButton2.AutoSize = true;
            this.neuRadioButton2.Checked = true;
            this.neuRadioButton2.Location = new System.Drawing.Point(27, 46);
            this.neuRadioButton2.Name = "neuRadioButton2";
            this.neuRadioButton2.Size = new System.Drawing.Size(59, 16);
            this.neuRadioButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuRadioButton2.TabIndex = 6;
            this.neuRadioButton2.TabStop = true;
            this.neuRadioButton2.Text = "未录入";
            this.neuRadioButton2.UseVisualStyleBackColor = true;
            // 
            // rdoIsExamed
            // 
            this.rdoIsExamed.AutoSize = true;
            this.rdoIsExamed.Location = new System.Drawing.Point(27, 16);
            this.rdoIsExamed.Name = "rdoIsExamed";
            this.rdoIsExamed.Size = new System.Drawing.Size(59, 16);
            this.rdoIsExamed.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdoIsExamed.TabIndex = 5;
            this.rdoIsExamed.Text = "已录入";
            this.rdoIsExamed.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(258, 38);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(79, 23);
            this.btSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btSave.TabIndex = 3;
            this.btSave.Text = "保存";
            this.btSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // rdoIsAll
            // 
            this.rdoIsAll.AutoSize = true;
            this.rdoIsAll.Checked = true;
            this.rdoIsAll.Location = new System.Drawing.Point(269, 20);
            this.rdoIsAll.Name = "rdoIsAll";
            this.rdoIsAll.Size = new System.Drawing.Size(71, 16);
            this.rdoIsAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdoIsAll.TabIndex = 4;
            this.rdoIsAll.TabStop = true;
            this.rdoIsAll.Text = "全部查询";
            this.rdoIsAll.UseVisualStyleBackColor = true;
            this.rdoIsAll.CheckedChanged += new System.EventHandler(this.rdoIsAll_CheckedChanged);
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.Enabled = false;
            this.ucQueryInpatientNo1.InputType = 0;
            //this.ucQueryInpatientNo1.IsDeptOnly = false;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(30, 13);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInpatientNo1.TabIndex = 6;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.txtItemFilter);
            this.neuPanel1.Controls.Add(this.neuGroupBox3);
            this.neuPanel1.Controls.Add(this.neuGroupBox2);
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(899, 122);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 4;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(540, 94);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 7;
            this.neuLabel3.Text = "检索：";
            // 
            // txtItemFilter
            // 
            this.txtItemFilter.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtItemFilter.IsEnter2Tab = false;
            this.txtItemFilter.Location = new System.Drawing.Point(602, 91);
            this.txtItemFilter.Name = "txtItemFilter";
            this.txtItemFilter.Size = new System.Drawing.Size(208, 21);
            this.txtItemFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtItemFilter.TabIndex = 6;
            this.txtItemFilter.TextChanged += new System.EventHandler(this.txtItemFilter_TextChanged);
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.neuLabel2);
            this.neuGroupBox3.Controls.Add(this.neuLabel1);
            this.neuGroupBox3.Controls.Add(this.dtEnd);
            this.neuGroupBox3.Controls.Add(this.dtBegin);
            this.neuGroupBox3.Location = new System.Drawing.Point(16, 3);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(491, 52);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 5;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "时间";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(267, 22);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 10;
            this.neuLabel2.Text = "结束：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(33, 21);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 9;
            this.neuLabel1.Text = "开始：";
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(328, 18);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(143, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 8;
            this.dtEnd.Tag = "结束时间";
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(94, 18);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(144, 21);
            this.dtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 7;
            this.dtBegin.Tag = "开始时间";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuRadioButton1);
            this.neuGroupBox2.Controls.Add(this.rdoIsAll);
            this.neuGroupBox2.Controls.Add(this.ucQueryInpatientNo1);
            this.neuGroupBox2.Location = new System.Drawing.Point(16, 61);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(491, 51);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 4;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "患者范围";
            // 
            // neuRadioButton1
            // 
            this.neuRadioButton1.AutoSize = true;
            this.neuRadioButton1.Location = new System.Drawing.Point(358, 20);
            this.neuRadioButton1.Name = "neuRadioButton1";
            this.neuRadioButton1.Size = new System.Drawing.Size(71, 16);
            this.neuRadioButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuRadioButton1.TabIndex = 7;
            this.neuRadioButton1.TabStop = true;
            this.neuRadioButton1.Text = "单人查询";
            this.neuRadioButton1.UseVisualStyleBackColor = true;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuFpEnter1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 122);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(899, 460);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 5;
            // 
            // neuFpEnter1
            // 
            this.neuFpEnter1.About = "3.0.2004.2005";
            this.neuFpEnter1.AccessibleDescription = "neuFpEnter1, Sheet1, Row 0, Column 0, ";
            this.neuFpEnter1.BackColor = System.Drawing.SystemColors.Control;
            this.neuFpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuFpEnter1.EditModePermanent = true;
            this.neuFpEnter1.EditModeReplace = true;
            this.neuFpEnter1.Location = new System.Drawing.Point(0, 0);
            this.neuFpEnter1.Name = "neuFpEnter1";
            this.neuFpEnter1.SelectNone = false;
            this.neuFpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuFpEnter1.ShowListWhenOfFocus = false;
            this.neuFpEnter1.Size = new System.Drawing.Size(899, 460);
            this.neuFpEnter1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuFpEnter1.TextTipAppearance = tipAppearance1;
            this.neuFpEnter1.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(this.fpSpread3_SetItem);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuFpEnter1.SetActiveViewport(0, 1, 0);
            // 
            // ucOrderExecMultiInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucOrderExecMultiInput";
            this.Size = new System.Drawing.Size(899, 582);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton btQuery;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuButton btSave;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rdoIsExamed;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rdoIsAll;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton neuRadioButton2;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton neuRadioButton1;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtItemFilter;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;

    }
}
