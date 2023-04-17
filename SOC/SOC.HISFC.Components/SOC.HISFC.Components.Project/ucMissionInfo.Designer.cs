namespace FS.SOC.HISFC.Components.Project
{
    partial class ucMissionInfo
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
            this.ncmProjectStage = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtProjectName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtProjectID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ndtpMadeTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbMadeOper = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbPriorityLevel = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbFunctionType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbModelName = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbState = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nrtbMemo = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nrtbMission = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ncmProjectStage);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.ntxtProjectName);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.ntxtProjectID);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(519, 94);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 1;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "项目信息";
            // 
            // ncmProjectStage
            // 
            this.ncmProjectStage.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmProjectStage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmProjectStage.FormattingEnabled = true;
            this.ncmProjectStage.IsEnter2Tab = false;
            this.ncmProjectStage.IsFlat = false;
            this.ncmProjectStage.IsLike = true;
            this.ncmProjectStage.IsListOnly = false;
            this.ncmProjectStage.IsPopForm = true;
            this.ncmProjectStage.IsShowCustomerList = false;
            this.ncmProjectStage.IsShowID = false;
            this.ncmProjectStage.Items.AddRange(new object[] {
            "需求调研",
            "离岸开发",
            "现场开发",
            "培训测试",
            "交付"});
            this.ncmProjectStage.Location = new System.Drawing.Point(106, 59);
            this.ncmProjectStage.Name = "ncmProjectStage";
            this.ncmProjectStage.ShowCustomerList = false;
            this.ncmProjectStage.ShowID = false;
            this.ncmProjectStage.Size = new System.Drawing.Size(121, 20);
            this.ncmProjectStage.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmProjectStage.TabIndex = 6;
            this.ncmProjectStage.Tag = "";
            this.ncmProjectStage.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(29, 63);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "项目阶段";
            // 
            // ntxtProjectName
            // 
            this.ntxtProjectName.IsEnter2Tab = true;
            this.ntxtProjectName.Location = new System.Drawing.Point(334, 23);
            this.ntxtProjectName.Name = "ntxtProjectName";
            this.ntxtProjectName.Size = new System.Drawing.Size(153, 21);
            this.ntxtProjectName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtProjectName.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(257, 27);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "项目名称";
            // 
            // ntxtProjectID
            // 
            this.ntxtProjectID.IsEnter2Tab = true;
            this.ntxtProjectID.Location = new System.Drawing.Point(106, 23);
            this.ntxtProjectID.Name = "ntxtProjectID";
            this.ntxtProjectID.Size = new System.Drawing.Size(121, 21);
            this.ntxtProjectID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtProjectID.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(29, 27);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "项目编号";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuPanel3);
            this.neuGroupBox2.Controls.Add(this.neuPanel1);
            this.neuGroupBox2.Controls.Add(this.neuPanel2);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 94);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(519, 399);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 4;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "问题信息";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.ncmbState);
            this.neuPanel2.Controls.Add(this.neuLabel10);
            this.neuPanel2.Controls.Add(this.ndtpMadeTime);
            this.neuPanel2.Controls.Add(this.neuLabel9);
            this.neuPanel2.Controls.Add(this.ncmbMadeOper);
            this.neuPanel2.Controls.Add(this.neuLabel7);
            this.neuPanel2.Controls.Add(this.ncmbPriorityLevel);
            this.neuPanel2.Controls.Add(this.neuLabel8);
            this.neuPanel2.Controls.Add(this.neuLabel6);
            this.neuPanel2.Controls.Add(this.ncmbFunctionType);
            this.neuPanel2.Controls.Add(this.neuLabel5);
            this.neuPanel2.Controls.Add(this.ncmbModelName);
            this.neuPanel2.Controls.Add(this.neuLabel4);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(3, 17);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(513, 141);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // ndtpMadeTime
            // 
            this.ndtpMadeTime.IsEnter2Tab = false;
            this.ndtpMadeTime.Location = new System.Drawing.Point(103, 51);
            this.ndtpMadeTime.Name = "ndtpMadeTime";
            this.ndtpMadeTime.Size = new System.Drawing.Size(121, 21);
            this.ndtpMadeTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpMadeTime.TabIndex = 12;
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(26, 55);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(53, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 16;
            this.neuLabel9.Text = "提出时间";
            // 
            // ncmbMadeOper
            // 
            this.ncmbMadeOper.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbMadeOper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbMadeOper.FormattingEnabled = true;
            this.ncmbMadeOper.IsEnter2Tab = false;
            this.ncmbMadeOper.IsFlat = false;
            this.ncmbMadeOper.IsLike = true;
            this.ncmbMadeOper.IsListOnly = false;
            this.ncmbMadeOper.IsPopForm = true;
            this.ncmbMadeOper.IsShowCustomerList = false;
            this.ncmbMadeOper.IsShowID = false;
            this.ncmbMadeOper.Location = new System.Drawing.Point(331, 51);
            this.ncmbMadeOper.Name = "ncmbMadeOper";
            this.ncmbMadeOper.ShowCustomerList = false;
            this.ncmbMadeOper.ShowID = false;
            this.ncmbMadeOper.Size = new System.Drawing.Size(156, 20);
            this.ncmbMadeOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbMadeOper.TabIndex = 14;
            this.ncmbMadeOper.Tag = "";
            this.ncmbMadeOper.ToolBarUse = false;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(254, 54);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(53, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 14;
            this.neuLabel7.Text = "提 出 人";
            // 
            // ncmbPriorityLevel
            // 
            this.ncmbPriorityLevel.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbPriorityLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbPriorityLevel.FormattingEnabled = true;
            this.ncmbPriorityLevel.IsEnter2Tab = false;
            this.ncmbPriorityLevel.IsFlat = false;
            this.ncmbPriorityLevel.IsLike = true;
            this.ncmbPriorityLevel.IsListOnly = false;
            this.ncmbPriorityLevel.IsPopForm = true;
            this.ncmbPriorityLevel.IsShowCustomerList = false;
            this.ncmbPriorityLevel.IsShowID = false;
            this.ncmbPriorityLevel.Location = new System.Drawing.Point(103, 85);
            this.ncmbPriorityLevel.Name = "ncmbPriorityLevel";
            this.ncmbPriorityLevel.ShowCustomerList = false;
            this.ncmbPriorityLevel.ShowID = false;
            this.ncmbPriorityLevel.Size = new System.Drawing.Size(121, 20);
            this.ncmbPriorityLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbPriorityLevel.TabIndex = 16;
            this.ncmbPriorityLevel.Tag = "";
            this.ncmbPriorityLevel.ToolBarUse = false;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(26, 89);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(53, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 12;
            this.neuLabel8.Text = "优 先 级";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(26, 121);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(101, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 11;
            this.neuLabel6.Text = "描述（需求说明）";
            // 
            // ncmbFunctionType
            // 
            this.ncmbFunctionType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbFunctionType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbFunctionType.FormattingEnabled = true;
            this.ncmbFunctionType.IsEnter2Tab = false;
            this.ncmbFunctionType.IsFlat = false;
            this.ncmbFunctionType.IsLike = true;
            this.ncmbFunctionType.IsListOnly = false;
            this.ncmbFunctionType.IsPopForm = true;
            this.ncmbFunctionType.IsShowCustomerList = false;
            this.ncmbFunctionType.IsShowID = false;
            this.ncmbFunctionType.Location = new System.Drawing.Point(331, 18);
            this.ncmbFunctionType.Name = "ncmbFunctionType";
            this.ncmbFunctionType.ShowCustomerList = false;
            this.ncmbFunctionType.ShowID = false;
            this.ncmbFunctionType.Size = new System.Drawing.Size(156, 20);
            this.ncmbFunctionType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbFunctionType.TabIndex = 10;
            this.ncmbFunctionType.Tag = "";
            this.ncmbFunctionType.ToolBarUse = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(254, 21);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(53, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 9;
            this.neuLabel5.Text = "功能分类";
            // 
            // ncmbModelName
            // 
            this.ncmbModelName.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbModelName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbModelName.FormattingEnabled = true;
            this.ncmbModelName.IsEnter2Tab = false;
            this.ncmbModelName.IsFlat = false;
            this.ncmbModelName.IsLike = true;
            this.ncmbModelName.IsListOnly = false;
            this.ncmbModelName.IsPopForm = true;
            this.ncmbModelName.IsShowCustomerList = false;
            this.ncmbModelName.IsShowID = false;
            this.ncmbModelName.Location = new System.Drawing.Point(103, 18);
            this.ncmbModelName.Name = "ncmbModelName";
            this.ncmbModelName.ShowCustomerList = false;
            this.ncmbModelName.ShowID = false;
            this.ncmbModelName.Size = new System.Drawing.Size(121, 20);
            this.ncmbModelName.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbModelName.TabIndex = 8;
            this.ncmbModelName.Tag = "";
            this.ncmbModelName.ToolBarUse = false;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(26, 21);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(53, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "模块名称";
            // 
            // ncmbState
            // 
            this.ncmbState.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbState.FormattingEnabled = true;
            this.ncmbState.IsEnter2Tab = false;
            this.ncmbState.IsFlat = false;
            this.ncmbState.IsLike = true;
            this.ncmbState.IsListOnly = false;
            this.ncmbState.IsPopForm = true;
            this.ncmbState.IsShowCustomerList = false;
            this.ncmbState.IsShowID = false;
            this.ncmbState.Location = new System.Drawing.Point(331, 85);
            this.ncmbState.Name = "ncmbState";
            this.ncmbState.ShowCustomerList = false;
            this.ncmbState.ShowID = false;
            this.ncmbState.Size = new System.Drawing.Size(156, 20);
            this.ncmbState.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbState.TabIndex = 18;
            this.ncmbState.Tag = "";
            this.ncmbState.ToolBarUse = false;
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(254, 89);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(53, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 17;
            this.neuLabel10.Text = "状    态";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel11);
            this.neuPanel1.Controls.Add(this.nrtbMemo);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(3, 324);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(513, 72);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Location = new System.Drawing.Point(27, 8);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(101, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 22;
            this.neuLabel11.Text = "备注（补充说明）";
            // 
            // nrtbMemo
            // 
            this.nrtbMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nrtbMemo.Location = new System.Drawing.Point(29, 30);
            this.nrtbMemo.Name = "nrtbMemo";
            this.nrtbMemo.Size = new System.Drawing.Size(456, 39);
            this.nrtbMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nrtbMemo.TabIndex = 21;
            this.nrtbMemo.Text = "";
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.nrtbMission);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(3, 158);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(513, 166);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 3;
            // 
            // nrtbMission
            // 
            this.nrtbMission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nrtbMission.Location = new System.Drawing.Point(29, 0);
            this.nrtbMission.Name = "nrtbMission";
            this.nrtbMission.Size = new System.Drawing.Size(456, 160);
            this.nrtbMission.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nrtbMission.TabIndex = 18;
            this.nrtbMission.Text = "";
            // 
            // ucMissionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucMissionInfo";
            this.Size = new System.Drawing.Size(519, 493);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmProjectStage;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtProjectName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtProjectID;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpMadeTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbMadeOper;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbPriorityLevel;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbFunctionType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbModelName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbState;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox nrtbMemo;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox nrtbMission;
    }
}
