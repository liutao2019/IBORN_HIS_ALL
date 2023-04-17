namespace FS.HISFC.Components.Order.Controls
{
    partial class ucDrugCardControl
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtShort = new System.Windows.Forms.RadioButton();
            this.rbtAll = new System.Windows.Forms.RadioButton();
            this.rbtLong = new System.Windows.Forms.RadioButton();
            this.chkFirst = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLinkLabel1 = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.btnQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.chkRePrint = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.panelMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.chkFirst);
            this.panelMain.Controls.Add(this.neuLinkLabel1);
            this.panelMain.Controls.Add(this.btnQuery);
            this.panelMain.Controls.Add(this.chkRePrint);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.dateTimePicker2);
            this.panelMain.Controls.Add(this.dateTimePicker1);
            this.panelMain.Controls.Add(this.neuLabel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(772, 47);
            this.panelMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtShort);
            this.groupBox1.Controls.Add(this.rbtAll);
            this.groupBox1.Controls.Add(this.rbtLong);
            this.groupBox1.Location = new System.Drawing.Point(412, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 33);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // rbtShort
            // 
            this.rbtShort.AutoSize = true;
            this.rbtShort.Location = new System.Drawing.Point(110, 11);
            this.rbtShort.Name = "rbtShort";
            this.rbtShort.Size = new System.Drawing.Size(47, 16);
            this.rbtShort.TabIndex = 10;
            this.rbtShort.Text = "临嘱";
            this.rbtShort.UseVisualStyleBackColor = true;
            // 
            // rbtAll
            // 
            this.rbtAll.AutoSize = true;
            this.rbtAll.Checked = true;
            this.rbtAll.Location = new System.Drawing.Point(6, 11);
            this.rbtAll.Name = "rbtAll";
            this.rbtAll.Size = new System.Drawing.Size(47, 16);
            this.rbtAll.TabIndex = 8;
            this.rbtAll.TabStop = true;
            this.rbtAll.Text = "全部";
            this.rbtAll.UseVisualStyleBackColor = true;
            // 
            // rbtLong
            // 
            this.rbtLong.AutoSize = true;
            this.rbtLong.Location = new System.Drawing.Point(58, 11);
            this.rbtLong.Name = "rbtLong";
            this.rbtLong.Size = new System.Drawing.Size(47, 16);
            this.rbtLong.TabIndex = 9;
            this.rbtLong.Text = "长嘱";
            this.rbtLong.UseVisualStyleBackColor = true;
            // 
            // chkFirst
            // 
            this.chkFirst.AutoSize = true;
            this.chkFirst.Location = new System.Drawing.Point(643, 15);
            this.chkFirst.Name = "chkFirst";
            this.chkFirst.Size = new System.Drawing.Size(60, 16);
            this.chkFirst.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkFirst.TabIndex = 8;
            this.chkFirst.Text = "首日量";
            this.chkFirst.UseVisualStyleBackColor = true;
            // 
            // neuLinkLabel1
            // 
            this.neuLinkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.neuLinkLabel1.AutoSize = true;
            this.neuLinkLabel1.Location = new System.Drawing.Point(731, 20);
            this.neuLinkLabel1.Name = "neuLinkLabel1";
            this.neuLinkLabel1.Size = new System.Drawing.Size(29, 12);
            this.neuLinkLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLinkLabel1.TabIndex = 6;
            this.neuLinkLabel1.TabStop = true;
            this.neuLinkLabel1.Text = "设置";
            this.neuLinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.neuLinkLabel1_LinkClicked);
            // 
            // btnQuery
            // 
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(466, 24);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "刷新(&R)";
            this.btnQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Visible = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // chkRePrint
            // 
            this.chkRePrint.AutoSize = true;
            this.chkRePrint.Location = new System.Drawing.Point(584, 15);
            this.chkRePrint.Name = "chkRePrint";
            this.chkRePrint.Size = new System.Drawing.Size(48, 16);
            this.chkRePrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkRePrint.TabIndex = 4;
            this.chkRePrint.Text = "补打";
            this.chkRePrint.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "到";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.IsEnter2Tab = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(261, 11);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(145, 21);
            this.dateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dateTimePicker2.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.IsEnter2Tab = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(97, 11);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(137, 21);
            this.dateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dateTimePicker1.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(15, 15);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "输液卡时间：";
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 47);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(772, 465);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 1;
            this.neuTabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectionChanged);
            // 
            // ucDrugCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuTabControl1);
            this.Controls.Add(this.panelMain);
            this.Name = "ucDrugCardControl";
            this.Size = new System.Drawing.Size(772, 512);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        protected System.Windows.Forms.Panel panelMain;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkRePrint;
        private FS.FrameWork.WinForms.Controls.NeuButton btnQuery;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel neuLinkLabel1;
        protected FS.FrameWork.WinForms.Controls.NeuCheckBox chkFirst;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtShort;
        private System.Windows.Forms.RadioButton rbtAll;
        private System.Windows.Forms.RadioButton rbtLong;
    }
}
