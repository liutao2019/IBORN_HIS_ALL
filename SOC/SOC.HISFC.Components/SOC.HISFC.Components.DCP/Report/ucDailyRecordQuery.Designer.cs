namespace FS.SOC.HISFC.Components.DCP.Report
{
    partial class ucDailyRecordQuery
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbDoctor = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.nlbResetFarpoint = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.ntxtPatient = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbDiagnose = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.ndtpBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpOutpatient = new System.Windows.Forms.TabPage();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpInpatient = new System.Windows.Forms.TabPage();
            this.fpSpread2 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpOutpatient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.tpInpatient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuPanel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(987, 100);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 2;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询设置";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel6);
            this.neuPanel1.Controls.Add(this.ncmbDoctor);
            this.neuPanel1.Controls.Add(this.nlbResetFarpoint);
            this.neuPanel1.Controls.Add(this.ntxtPatient);
            this.neuPanel1.Controls.Add(this.neuLabel5);
            this.neuPanel1.Controls.Add(this.neuLabel4);
            this.neuPanel1.Controls.Add(this.ncmbDiagnose);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.ndtpEndTime);
            this.neuPanel1.Controls.Add(this.ndtpBeginTime);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.ncmbDept);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 17);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(981, 71);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(22, 45);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(41, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 12;
            this.neuLabel6.Text = "医生：";
            // 
            // ncmbDoctor
            // 
            this.ncmbDoctor.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbDoctor.FormattingEnabled = true;
            this.ncmbDoctor.IsEnter2Tab = false;
            this.ncmbDoctor.IsFlat = false;
            this.ncmbDoctor.IsLike = true;
            this.ncmbDoctor.IsListOnly = false;
            this.ncmbDoctor.IsPopForm = true;
            this.ncmbDoctor.IsShowCustomerList = false;
            this.ncmbDoctor.IsShowID = false;
            this.ncmbDoctor.Location = new System.Drawing.Point(69, 42);
            this.ncmbDoctor.Name = "ncmbDoctor";
            this.ncmbDoctor.ShowCustomerList = false;
            this.ncmbDoctor.ShowID = false;
            this.ncmbDoctor.Size = new System.Drawing.Size(121, 20);
            this.ncmbDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDoctor.TabIndex = 11;
            this.ncmbDoctor.Tag = "";
            this.ncmbDoctor.ToolBarUse = false;
            // 
            // nlbResetFarpoint
            // 
            this.nlbResetFarpoint.AutoSize = true;
            this.nlbResetFarpoint.Location = new System.Drawing.Point(861, 45);
            this.nlbResetFarpoint.Name = "nlbResetFarpoint";
            this.nlbResetFarpoint.Size = new System.Drawing.Size(53, 12);
            this.nlbResetFarpoint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbResetFarpoint.TabIndex = 10;
            this.nlbResetFarpoint.TabStop = true;
            this.nlbResetFarpoint.Text = "重置格式";
            this.nlbResetFarpoint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.nlbResetFarpoint_LinkClicked);
            // 
            // ntxtPatient
            // 
            this.ntxtPatient.IsEnter2Tab = false;
            this.ntxtPatient.Location = new System.Drawing.Point(711, 41);
            this.ntxtPatient.Name = "ntxtPatient";
            this.ntxtPatient.Size = new System.Drawing.Size(121, 21);
            this.ntxtPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtPatient.TabIndex = 9;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(664, 45);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(41, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 8;
            this.neuLabel5.Text = "患者：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(219, 46);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(41, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "诊断：";
            // 
            // ncmbDiagnose
            // 
            this.ncmbDiagnose.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDiagnose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbDiagnose.FormattingEnabled = true;
            this.ncmbDiagnose.IsEnter2Tab = false;
            this.ncmbDiagnose.IsFlat = false;
            this.ncmbDiagnose.IsLike = true;
            this.ncmbDiagnose.IsListOnly = false;
            this.ncmbDiagnose.IsPopForm = true;
            this.ncmbDiagnose.IsShowCustomerList = false;
            this.ncmbDiagnose.IsShowID = false;
            this.ncmbDiagnose.Location = new System.Drawing.Point(290, 42);
            this.ncmbDiagnose.Name = "ncmbDiagnose";
            this.ncmbDiagnose.ShowCustomerList = false;
            this.ncmbDiagnose.ShowID = false;
            this.ncmbDiagnose.Size = new System.Drawing.Size(345, 20);
            this.ncmbDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDiagnose.TabIndex = 6;
            this.ncmbDiagnose.Tag = "";
            this.ncmbDiagnose.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(457, 19);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(11, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "-";
            // 
            // ndtpEndTime
            // 
            this.ndtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpEndTime.IsEnter2Tab = false;
            this.ndtpEndTime.Location = new System.Drawing.Point(476, 15);
            this.ndtpEndTime.Name = "ndtpEndTime";
            this.ndtpEndTime.Size = new System.Drawing.Size(159, 21);
            this.ndtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpEndTime.TabIndex = 4;
            // 
            // ndtpBeginTime
            // 
            this.ndtpBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpBeginTime.IsEnter2Tab = false;
            this.ndtpBeginTime.Location = new System.Drawing.Point(290, 15);
            this.ndtpBeginTime.Name = "ndtpBeginTime";
            this.ndtpBeginTime.Size = new System.Drawing.Size(159, 21);
            this.ndtpBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpBeginTime.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(219, 18);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "诊断时间：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(22, 18);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "科室：";
            // 
            // ncmbDept
            // 
            this.ncmbDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbDept.FormattingEnabled = true;
            this.ncmbDept.IsEnter2Tab = false;
            this.ncmbDept.IsFlat = false;
            this.ncmbDept.IsLike = true;
            this.ncmbDept.IsListOnly = false;
            this.ncmbDept.IsPopForm = true;
            this.ncmbDept.IsShowCustomerList = false;
            this.ncmbDept.IsShowID = false;
            this.ncmbDept.Location = new System.Drawing.Point(69, 15);
            this.ncmbDept.Name = "ncmbDept";
            this.ncmbDept.ShowCustomerList = false;
            this.ncmbDept.ShowID = false;
            this.ncmbDept.Size = new System.Drawing.Size(121, 20);
            this.ncmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDept.TabIndex = 0;
            this.ncmbDept.Tag = "";
            this.ncmbDept.ToolBarUse = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpOutpatient);
            this.tabControl1.Controls.Add(this.tpInpatient);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 100);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(987, 433);
            this.tabControl1.TabIndex = 3;
            // 
            // tpOutpatient
            // 
            this.tpOutpatient.Controls.Add(this.fpSpread1);
            this.tpOutpatient.Location = new System.Drawing.Point(4, 22);
            this.tpOutpatient.Name = "tpOutpatient";
            this.tpOutpatient.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutpatient.Size = new System.Drawing.Size(979, 407);
            this.tpOutpatient.TabIndex = 0;
            this.tpOutpatient.Text = "门诊日志";
            this.tpOutpatient.UseVisualStyleBackColor = true;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(3, 3);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(973, 401);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // 
            // tpInpatient
            // 
            this.tpInpatient.Controls.Add(this.fpSpread2);
            this.tpInpatient.Location = new System.Drawing.Point(4, 22);
            this.tpInpatient.Name = "tpInpatient";
            this.tpInpatient.Padding = new System.Windows.Forms.Padding(3);
            this.tpInpatient.Size = new System.Drawing.Size(979, 407);
            this.tpInpatient.TabIndex = 1;
            this.tpInpatient.Text = "住院日志";
            this.tpInpatient.UseVisualStyleBackColor = true;
            // 
            // fpSpread2
            // 
            this.fpSpread2.About = "3.0.2004.2005";
            this.fpSpread2.AccessibleDescription = "";
            this.fpSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread2.Location = new System.Drawing.Point(3, 3);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSpread2.Size = new System.Drawing.Size(973, 401);
            this.fpSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread2.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread2.TextTipAppearance = tipAppearance2;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // 
            // ucDailyRecordQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucDailyRecordQuery";
            this.Size = new System.Drawing.Size(987, 533);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpOutpatient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.tpInpatient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel nlbResetFarpoint;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtPatient;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDiagnose;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpEndTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDept;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpOutpatient;
        private System.Windows.Forms.TabPage tpInpatient;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDoctor;
        private FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.SOC.Windows.Forms.FpSpread fpSpread2;
        private FarPoint.Win.Spread.SheetView sheetView1;

    }
}
