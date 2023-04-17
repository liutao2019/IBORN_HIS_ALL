namespace FS.HISFC.Components.Order.Controls
{
    partial class ucExecBill
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkFirst = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkRePrint = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.dateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtShort = new System.Windows.Forms.RadioButton();
            this.rbtAll = new System.Windows.Forms.RadioButton();
            this.rbtLong = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.chkFirst);
            this.panel2.Controls.Add(this.chkRePrint);
            this.panel2.Controls.Add(this.dateTimePicker2);
            this.panel2.Controls.Add(this.neuLabel2);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.neuLabel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(741, 53);
            this.panel2.TabIndex = 0;
            // 
            // chkFirst
            // 
            this.chkFirst.AutoSize = true;
            this.chkFirst.Location = new System.Drawing.Point(652, 20);
            this.chkFirst.Name = "chkFirst";
            this.chkFirst.Size = new System.Drawing.Size(60, 16);
            this.chkFirst.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkFirst.TabIndex = 5;
            this.chkFirst.Text = "首日量";
            this.chkFirst.UseVisualStyleBackColor = true;
            // 
            // chkRePrint
            // 
            this.chkRePrint.AutoSize = true;
            this.chkRePrint.Location = new System.Drawing.Point(574, 20);
            this.chkRePrint.Name = "chkRePrint";
            this.chkRePrint.Size = new System.Drawing.Size(48, 16);
            this.chkRePrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkRePrint.TabIndex = 4;
            this.chkRePrint.Text = "补打";
            this.chkRePrint.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.IsEnter2Tab = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(244, 19);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(135, 21);
            this.dateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dateTimePicker2.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(227, 23);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(11, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "-";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.IsEnter2Tab = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(84, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(135, 21);
            this.dateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dateTimePicker1.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(13, 23);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "执行时间：";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(741, 471);
            this.panel3.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(741, 471);
            this.tabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectionChanged);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 53);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(741, 3);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtShort);
            this.groupBox1.Controls.Add(this.rbtAll);
            this.groupBox1.Controls.Add(this.rbtLong);
            this.groupBox1.Location = new System.Drawing.Point(387, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 33);
            this.groupBox1.TabIndex = 12;
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
            // ucExecBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "ucExecBill";
            this.Size = new System.Drawing.Size(741, 524);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        protected FS.FrameWork.WinForms.Controls.NeuDateTimePicker dateTimePicker2;
        protected FS.FrameWork.WinForms.Controls.NeuCheckBox chkRePrint;
        protected System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.Panel panel3;
        protected FS.FrameWork.WinForms.Controls.NeuDateTimePicker dateTimePicker1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl tabControl1;
        protected FS.FrameWork.WinForms.Controls.NeuCheckBox chkFirst;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtShort;
        private System.Windows.Forms.RadioButton rbtAll;
        private System.Windows.Forms.RadioButton rbtLong;
    }
}
