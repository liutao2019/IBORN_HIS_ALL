namespace FS.HISFC.Components.Operation
{
    partial class ucArrangement
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
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucArrangementInfo1 = new FS.HISFC.Components.Operation.ucArrangementInfo();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbRoom = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.rb2 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rb1 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rb4 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rb3 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.labApplyTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tabInformation = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabOpertion = new System.Windows.Forms.TabPage();
            this.ucArrangementSpread1 = new FS.HISFC.Components.Operation.ucArrangementSpread();
            this.tabAnaesthesia = new System.Windows.Forms.TabPage();
            this.ucAnaesthesiaSpread1 = new FS.HISFC.Components.Operation.ucAnaesthesiaSpread();
            this.tabAlreadyOperation = new System.Windows.Forms.TabPage();
            //this.ucAlreadyOperationSpread1 = new FS.HISFC.Components.Operation.ucArrangementSpread();
            this.ucAlreadyOperationSpread1 = new FS.HISFC.Components.Operation.ucAlreadyOperationSpread();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.tabInformation.SuspendLayout();
            this.tabOpertion.SuspendLayout();
            this.tabAnaesthesia.SuspendLayout();
            this.tabAlreadyOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.ucArrangementInfo1);
            this.neuPanel2.Location = new System.Drawing.Point(369, 3);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(830, 215);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // ucArrangementInfo1
            // 
            this.ucArrangementInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucArrangementInfo1.Name = "ucArrangementInfo1";
            this.ucArrangementInfo1.Size = new System.Drawing.Size(672, 215);
            this.ucArrangementInfo1.TabIndex = 2;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuGroupBox1);
            this.neuPanel3.Controls.Add(this.labApplyTime);
            this.neuPanel3.Controls.Add(this.neuDateTimePicker1);
            this.neuPanel3.Controls.Add(this.neuLabel1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel3.Location = new System.Drawing.Point(0, 3);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(363, 218);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 4;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuPanel4);
            this.neuGroupBox1.Location = new System.Drawing.Point(5, 56);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(285, 112);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 3;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "打印模式选择";
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.cmbDept);
            this.neuPanel4.Controls.Add(this.cmbRoom);
            this.neuPanel4.Controls.Add(this.rb2);
            this.neuPanel4.Controls.Add(this.rb1);
            this.neuPanel4.Controls.Add(this.rb4);
            this.neuPanel4.Controls.Add(this.rb3);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel4.Location = new System.Drawing.Point(3, 17);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(279, 92);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 6;
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.Enabled = false;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.IsShowIDAndName = false;
            this.cmbDept.Location = new System.Drawing.Point(145, 61);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(121, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 5;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            this.cmbDept.SelectedIndexChanged += new System.EventHandler(this.cmbDept_SelectedIndexChanged);
            // 
            // cmbRoom
            // 
            this.cmbRoom.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoom.Enabled = false;
            this.cmbRoom.FormattingEnabled = true;
            this.cmbRoom.IsEnter2Tab = false;
            this.cmbRoom.IsFlat = false;
            this.cmbRoom.IsLike = true;
            this.cmbRoom.IsListOnly = false;
            this.cmbRoom.IsPopForm = true;
            this.cmbRoom.IsShowCustomerList = false;
            this.cmbRoom.IsShowID = false;
            this.cmbRoom.IsShowIDAndName = false;
            this.cmbRoom.Location = new System.Drawing.Point(145, 34);
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.ShowCustomerList = false;
            this.cmbRoom.ShowID = false;
            this.cmbRoom.Size = new System.Drawing.Size(121, 20);
            this.cmbRoom.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRoom.TabIndex = 4;
            this.cmbRoom.Tag = "";
            this.cmbRoom.ToolBarUse = false;
            this.cmbRoom.SelectedIndexChanged += new System.EventHandler(this.cmbRoom_SelectedIndexChanged);
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(145, 12);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(107, 16);
            this.rb2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rb2.TabIndex = 1;
            this.rb2.Text = "手术间顺序打印";
            this.rb2.UseVisualStyleBackColor = true;
            this.rb2.CheckedChanged += new System.EventHandler(this.rb2_CheckedChanged);
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(15, 12);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(71, 16);
            this.rb1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rb1.TabIndex = 0;
            this.rb1.TabStop = true;
            this.rb1.Text = "默认打印";
            this.rb1.UseVisualStyleBackColor = true;
            this.rb1.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // rb4
            // 
            this.rb4.AutoSize = true;
            this.rb4.Location = new System.Drawing.Point(15, 64);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(95, 16);
            this.rb4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rb4.TabIndex = 4;
            this.rb4.TabStop = true;
            this.rb4.Text = "单独病区打印";
            this.rb4.UseVisualStyleBackColor = true;
            this.rb4.CheckedChanged += new System.EventHandler(this.rb4_CheckedChanged);
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(15, 37);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(107, 16);
            this.rb3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rb3.TabIndex = 2;
            this.rb3.Text = "单独手术间打印";
            this.rb3.UseVisualStyleBackColor = true;
            this.rb3.CheckedChanged += new System.EventHandler(this.rb3_CheckedChanged);
            // 
            // labApplyTime
            // 
            this.labApplyTime.AutoSize = true;
            this.labApplyTime.Location = new System.Drawing.Point(6, 183);
            this.labApplyTime.Name = "labApplyTime";
            this.labApplyTime.Size = new System.Drawing.Size(59, 12);
            this.labApplyTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labApplyTime.TabIndex = 2;
            this.labApplyTime.Text = "neuLabel2";
            this.labApplyTime.Visible = false;
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(5, 21);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(118, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(3, 6);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "设定时间";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(1202, 3);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 5;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuSplitter1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1202, 221);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // tabInformation
            // 
            this.tabInformation.Controls.Add(this.tabOpertion);
            this.tabInformation.Controls.Add(this.tabAnaesthesia);
            this.tabInformation.Controls.Add(this.tabAlreadyOperation);
            this.tabInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInformation.Location = new System.Drawing.Point(0, 221);
            this.tabInformation.Name = "tabInformation";
            this.tabInformation.SelectedIndex = 0;
            this.tabInformation.Size = new System.Drawing.Size(1202, 382);
            this.tabInformation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabInformation.TabIndex = 1;
            this.tabInformation.SelectedIndexChanged += new System.EventHandler(this.tabInformation_SelectedIndexChanged);
            // 
            // tabOpertion
            // 
            this.tabOpertion.Controls.Add(this.ucArrangementSpread1);
            this.tabOpertion.Location = new System.Drawing.Point(4, 21);
            this.tabOpertion.Name = "tabOpertion";
            this.tabOpertion.Padding = new System.Windows.Forms.Padding(3);
            this.tabOpertion.Size = new System.Drawing.Size(1194, 357);
            this.tabOpertion.TabIndex = 0;
            this.tabOpertion.Text = "手术安排";
            this.tabOpertion.UseVisualStyleBackColor = true;
            // 
            // ucArrangementSpread1
            // 
            this.ucArrangementSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucArrangementSpread1.Filter = FS.HISFC.Components.Operation.ucArrangementSpread.EnumFilter.All;
            this.ucArrangementSpread1.Location = new System.Drawing.Point(3, 3);
            this.ucArrangementSpread1.Name = "ucArrangementSpread1";
            this.ucArrangementSpread1.Size = new System.Drawing.Size(1188, 351);
            this.ucArrangementSpread1.TabIndex = 3;
            this.ucArrangementSpread1.applictionSelected += new FS.HISFC.Components.Operation.ApplicationSelectedEventHandler(this.ucArrangementSpread1_applictionSelected);
            // 
            // tabAnaesthesia
            // 
            this.tabAnaesthesia.Controls.Add(this.ucAnaesthesiaSpread1);
            this.tabAnaesthesia.Location = new System.Drawing.Point(4, 21);
            this.tabAnaesthesia.Name = "tabAnaesthesia";
            this.tabAnaesthesia.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnaesthesia.Size = new System.Drawing.Size(1194, 357);
            this.tabAnaesthesia.TabIndex = 1;
            this.tabAnaesthesia.Text = "麻醉安排";
            this.tabAnaesthesia.UseVisualStyleBackColor = true;
            // 
            // ucAnaesthesiaSpread1
            // 
            this.ucAnaesthesiaSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAnaesthesiaSpread1.Location = new System.Drawing.Point(3, 3);
            this.ucAnaesthesiaSpread1.Name = "ucAnaesthesiaSpread1";
            this.ucAnaesthesiaSpread1.Size = new System.Drawing.Size(1188, 351);
            this.ucAnaesthesiaSpread1.TabIndex = 0;
            this.ucAnaesthesiaSpread1.applictionSelected += new FS.HISFC.Components.Operation.ApplicationSelectedEventHandler(this.ucAnaesthesiaSpread1_applictionSelected);
            // 
            // tabAlreadyOperation
            // 
            this.tabAlreadyOperation.Controls.Add(this.ucAlreadyOperationSpread1);
            this.tabAlreadyOperation.Location = new System.Drawing.Point(4, 21);
            this.tabAlreadyOperation.Name = "tabAlreadyOperation";
            this.tabAlreadyOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlreadyOperation.Size = new System.Drawing.Size(1194, 357);
            this.tabAlreadyOperation.TabIndex = 2;
            this.tabAlreadyOperation.Text = "已完成未收费手术";
            this.tabAlreadyOperation.UseVisualStyleBackColor = true;
            // 
            // ucAlreadyOperationSpread1
            // 
            this.ucAlreadyOperationSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAlreadyOperationSpread1.Filter = FS.HISFC.Components.Operation.ucAlreadyOperationSpread.EnumFilter.All;
            this.ucAlreadyOperationSpread1.Location = new System.Drawing.Point(3, 3);
            this.ucAlreadyOperationSpread1.Name = "ucAlreadyOperationSpread1";
            this.ucAlreadyOperationSpread1.Size = new System.Drawing.Size(1188, 351);
            this.ucAlreadyOperationSpread1.TabIndex = 4;
            this.ucAlreadyOperationSpread1.applictionSelected += new FS.HISFC.Components.Operation.ApplicationSelectedEventHandler(this.ucAlreadyOperationSpread1_applictionSelected);
            // 
            // ucArrangement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabInformation);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucArrangement";
            this.Size = new System.Drawing.Size(1202, 603);
            this.Load += new System.EventHandler(this.ucArrangement_Load);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.tabInformation.ResumeLayout(false);
            this.tabOpertion.ResumeLayout(false);
            this.tabAnaesthesia.ResumeLayout(false);
            this.tabAlreadyOperation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucArrangementInfo ucArrangementInfo1;
        private FS.HISFC.Components.Operation.ucArrangementSpread ucArrangementSpread1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl tabInformation;
        private System.Windows.Forms.TabPage tabOpertion;
        private System.Windows.Forms.TabPage tabAnaesthesia;
        private ucAnaesthesiaSpread ucAnaesthesiaSpread1;
        private FS.FrameWork.WinForms.Controls.NeuLabel labApplyTime;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rb3;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rb2;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rb1;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rb4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbRoom;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private System.Windows.Forms.TabPage tabAlreadyOperation;
       // private ucArrangementSpread ucAlreadyOperationSpread1;
        private ucAlreadyOperationSpread ucAlreadyOperationSpread1;
    }
}
