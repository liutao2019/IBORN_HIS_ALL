namespace FS.HISFC.Components.HealthRecord.Case
{
    partial class ucBarCode
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
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmbOper = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lalbel1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(258, 15);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 8;
            this.neuLabel2.Text = "结束时间";
            // 
            // panMain
            // 
            this.panMain.AutoScroll = true;
            this.panMain.BackColor = System.Drawing.Color.White;
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 40);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(793, 458);
            this.panMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panMain.TabIndex = 2;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.Azure;
            this.neuPanel1.Controls.Add(this.checkBox1);
            this.neuPanel1.Controls.Add(this.lalbel1);
            this.neuPanel1.Controls.Add(this.textBox1);
            this.neuPanel1.Controls.Add(this.cmbOper);
            this.neuPanel1.Controls.Add(this.label2);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.dtpBeginTime);
            this.neuPanel1.Controls.Add(this.dtpEndTime);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(793, 40);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 3;
            // 
            // cmbOper
            // 
            this.cmbOper.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOper.FormattingEnabled = true;
            this.cmbOper.IsEnter2Tab = false;
            this.cmbOper.IsFlat = true;
            this.cmbOper.IsLike = true;
            this.cmbOper.Location = new System.Drawing.Point(544, 12);
            this.cmbOper.Name = "cmbOper";
            this.cmbOper.PopForm = null;
            this.cmbOper.ShowCustomerList = false;
            this.cmbOper.ShowID = false;
            this.cmbOper.Size = new System.Drawing.Size(90, 20);
            this.cmbOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbOper.TabIndex = 37;
            this.cmbOper.Tag = "";
            this.cmbOper.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(484, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "录入员";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(31, 15);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "开始时间";
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBeginTime.IsEnter2Tab = false;
            this.dtpBeginTime.Location = new System.Drawing.Point(90, 11);
            this.dtpBeginTime.Name = "dtpBeginTime";
            this.dtpBeginTime.Size = new System.Drawing.Size(143, 21);
            this.dtpBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBeginTime.TabIndex = 2;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndTime.IsEnter2Tab = false;
            this.dtpEndTime.Location = new System.Drawing.Point(317, 11);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(143, 21);
            this.dtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndTime.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(765, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(26, 21);
            this.textBox1.TabIndex = 38;
            // 
            // lalbel1
            // 
            this.lalbel1.AutoSize = true;
            this.lalbel1.Location = new System.Drawing.Point(647, 18);
            this.lalbel1.Name = "lalbel1";
            this.lalbel1.Size = new System.Drawing.Size(0, 12);
            this.lalbel1.TabIndex = 39;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(689, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 40;
            this.checkBox1.Text = "选择打印";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ucBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucBarCode";
            this.Size = new System.Drawing.Size(793, 498);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel panMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndTime;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOper;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lalbel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
