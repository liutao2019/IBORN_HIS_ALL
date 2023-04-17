namespace FS.Report.Finance.FinReg
{
    partial class ucDayReportNew
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
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucRegDayBalanceReport1 = new FS.Report.Finance.FinReg.ucRegDayBalanceReportNew();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nDTPEndDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.nDTPBeginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabelTextBox2 = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.neuLabelTextBox1 = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.splitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.treeView1 = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.neuPanel1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(703, 464);
            this.panel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.ucRegDayBalanceReport1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(187, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(516, 427);
            this.panel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel3.TabIndex = 2;
            // 
            // ucRegDayBalanceReport1
            // 
            this.ucRegDayBalanceReport1.BackColor = System.Drawing.SystemColors.Window;
            this.ucRegDayBalanceReport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRegDayBalanceReport1.Location = new System.Drawing.Point(0, 0);
            this.ucRegDayBalanceReport1.Name = "ucRegDayBalanceReport1";
            this.ucRegDayBalanceReport1.Size = new System.Drawing.Size(516, 427);
            this.ucRegDayBalanceReport1.TabIndex = 1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.neuPanel1.Controls.Add(this.nDTPEndDate);
            this.neuPanel1.Controls.Add(this.nDTPBeginDate);
            this.neuPanel1.Controls.Add(this.neuLabelTextBox2);
            this.neuPanel1.Controls.Add(this.neuLabelTextBox1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(187, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(516, 37);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 3;
            // 
            // nDTPEndDate
            // 
            this.nDTPEndDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.nDTPEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.nDTPEndDate.IsEnter2Tab = false;
            this.nDTPEndDate.Location = new System.Drawing.Point(323, 6);
            this.nDTPEndDate.Name = "nDTPEndDate";
            this.nDTPEndDate.Size = new System.Drawing.Size(158, 21);
            this.nDTPEndDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nDTPEndDate.TabIndex = 1;
            // 
            // nDTPBeginDate
            // 
            this.nDTPBeginDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.nDTPBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.nDTPBeginDate.IsEnter2Tab = false;
            this.nDTPBeginDate.Location = new System.Drawing.Point(81, 6);
            this.nDTPBeginDate.Name = "nDTPBeginDate";
            this.nDTPBeginDate.Size = new System.Drawing.Size(167, 21);
            this.nDTPBeginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nDTPBeginDate.TabIndex = 0;
            // 
            // neuLabelTextBox2
            // 
            this.neuLabelTextBox2.Label = "截至时间";
            this.neuLabelTextBox2.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.neuLabelTextBox2.Location = new System.Drawing.Point(254, 2);
            this.neuLabelTextBox2.MaxLength = 32767;
            this.neuLabelTextBox2.Name = "neuLabelTextBox2";
            this.neuLabelTextBox2.ReadOnly = false;
            this.neuLabelTextBox2.Size = new System.Drawing.Size(90, 29);
            this.neuLabelTextBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelTextBox2.TabIndex = 4;
            this.neuLabelTextBox2.TextBoxLeft = 82;
            // 
            // neuLabelTextBox1
            // 
            this.neuLabelTextBox1.Label = "起始时间";
            this.neuLabelTextBox1.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.neuLabelTextBox1.Location = new System.Drawing.Point(5, 2);
            this.neuLabelTextBox1.MaxLength = 32767;
            this.neuLabelTextBox1.Name = "neuLabelTextBox1";
            this.neuLabelTextBox1.ReadOnly = false;
            this.neuLabelTextBox1.Size = new System.Drawing.Size(92, 29);
            this.neuLabelTextBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelTextBox1.TabIndex = 3;
            this.neuLabelTextBox1.TextBoxLeft = 82;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(184, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 464);
            this.splitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(184, 464);
            this.panel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(184, 464);
            this.treeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.treeView1.TabIndex = 0;
            // 
            // ucDayReportNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.panel1);
            this.Name = "ucDayReportNew";
            this.Size = new System.Drawing.Size(703, 464);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel2;
        private FS.FrameWork.WinForms.Controls.NeuSplitter splitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel3;
        //private FS.FrameWork.WinForms.Controls.NeuTreeView treeView1;
        private FS.HISFC.Components.Common.Controls.baseTreeView treeView1;
        private ucRegDayBalanceReportNew ucRegDayBalanceReport1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker nDTPEndDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker nDTPBeginDate;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox neuLabelTextBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox neuLabelTextBox1;
    }
}
