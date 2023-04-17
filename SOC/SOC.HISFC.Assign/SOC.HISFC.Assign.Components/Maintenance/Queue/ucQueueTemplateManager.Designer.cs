namespace FS.SOC.HISFC.Assign.Components.Maintenance.Queue
{
    partial class ucQueueTemplateManager
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.plNurseStation = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbConsole = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRoom = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbReglevel = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSeeDoctor = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbQueueType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.lbConsole);
            this.neuGroupBox1.Controls.Add(this.lbRoom);
            this.neuGroupBox1.Controls.Add(this.lbReglevel);
            this.neuGroupBox1.Controls.Add(this.lbSeeDoctor);
            this.neuGroupBox1.Controls.Add(this.lbQueueType);
            this.neuGroupBox1.Controls.Add(this.cmbDept);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(970, 56);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "信息显示";
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.Location = new System.Drawing.Point(58, 20);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(121, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 3;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(11, 23);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "科室：";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuLabel1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 346);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(970, 68);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 1;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "附加信息";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(43, 32);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(257, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "可选择多个科室进行显示，红色字体代表已停用";
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.neuTabControl1);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox3.ForeColor = System.Drawing.Color.Blue;
            this.neuGroupBox3.Location = new System.Drawing.Point(328, 56);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(642, 290);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 11;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "模板";
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(3, 17);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(636, 270);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 0;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(325, 56);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 290);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 10;
            this.neuSplitter1.TabStop = false;
            // 
            // plNurseStation
            // 
            this.plNurseStation.Dock = System.Windows.Forms.DockStyle.Left;
            this.plNurseStation.Location = new System.Drawing.Point(0, 56);
            this.plNurseStation.Name = "plNurseStation";
            this.plNurseStation.Size = new System.Drawing.Size(325, 290);
            this.plNurseStation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plNurseStation.TabIndex = 9;
            // 
            // lbConsole
            // 
            this.lbConsole.AutoSize = true;
            this.lbConsole.ForeColor = System.Drawing.Color.Black;
            this.lbConsole.Location = new System.Drawing.Point(873, 23);
            this.lbConsole.Name = "lbConsole";
            this.lbConsole.Size = new System.Drawing.Size(65, 12);
            this.lbConsole.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbConsole.TabIndex = 43;
            this.lbConsole.Text = "诊    台：";
            // 
            // lbRoom
            // 
            this.lbRoom.AutoSize = true;
            this.lbRoom.ForeColor = System.Drawing.Color.Black;
            this.lbRoom.Location = new System.Drawing.Point(739, 23);
            this.lbRoom.Name = "lbRoom";
            this.lbRoom.Size = new System.Drawing.Size(65, 12);
            this.lbRoom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRoom.TabIndex = 42;
            this.lbRoom.Text = "诊    室：";
            // 
            // lbReglevel
            // 
            this.lbReglevel.AutoSize = true;
            this.lbReglevel.Location = new System.Drawing.Point(607, 23);
            this.lbReglevel.Name = "lbReglevel";
            this.lbReglevel.Size = new System.Drawing.Size(65, 12);
            this.lbReglevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbReglevel.TabIndex = 41;
            this.lbReglevel.Text = "挂号级别：";
            // 
            // lbSeeDoctor
            // 
            this.lbSeeDoctor.AutoSize = true;
            this.lbSeeDoctor.Location = new System.Drawing.Point(479, 23);
            this.lbSeeDoctor.Name = "lbSeeDoctor";
            this.lbSeeDoctor.Size = new System.Drawing.Size(65, 12);
            this.lbSeeDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSeeDoctor.TabIndex = 40;
            this.lbSeeDoctor.Text = "看诊医生：";
            // 
            // lbQueueType
            // 
            this.lbQueueType.AutoSize = true;
            this.lbQueueType.ForeColor = System.Drawing.Color.Blue;
            this.lbQueueType.Location = new System.Drawing.Point(333, 22);
            this.lbQueueType.Name = "lbQueueType";
            this.lbQueueType.Size = new System.Drawing.Size(65, 12);
            this.lbQueueType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbQueueType.TabIndex = 39;
            this.lbQueueType.Text = "队列类型：";
            // 
            // ucQueueTemplateManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox3);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.plNurseStation);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucQueueTemplateManager";
            this.Size = new System.Drawing.Size(970, 414);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.neuGroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plNurseStation;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbConsole;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRoom;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbReglevel;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbSeeDoctor;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbQueueType;


    }
}
