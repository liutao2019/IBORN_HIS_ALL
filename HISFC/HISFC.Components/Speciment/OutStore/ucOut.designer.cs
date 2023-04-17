namespace FS.HISFC.Components.Speciment.OutStore
{
    partial class ucOut
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.txtApplyNum = new System.Windows.Forms.TextBox();
            this.rbtEnd = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.rbtFirst = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbOutputOperName = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbOutInfo = new System.Windows.Forms.TextBox();
            this.rbMt = new System.Windows.Forms.RadioButton();
            this.rbURt = new System.Windows.Forms.RadioButton();
            this.rbRt = new System.Windows.Forms.RadioButton();
            this.nudBorDay = new System.Windows.Forms.NumericUpDown();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOtherDemand = new System.Windows.Forms.TextBox();
            this.chkBack = new System.Windows.Forms.CheckBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.txtApplyNum);
            this.panel1.Controls.Add(this.rbtEnd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.rbtFirst);
            this.panel1.Controls.Add(this.txtFileName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1067, 38);
            this.panel1.TabIndex = 0;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.ForeColor = System.Drawing.Color.Red;
            this.lbStatus.Location = new System.Drawing.Point(886, 12);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(72, 16);
            this.lbStatus.TabIndex = 87;
            this.lbStatus.Text = "出库状态";
            // 
            // txtApplyNum
            // 
            this.txtApplyNum.Location = new System.Drawing.Point(800, 6);
            this.txtApplyNum.Name = "txtApplyNum";
            this.txtApplyNum.Size = new System.Drawing.Size(81, 26);
            this.txtApplyNum.TabIndex = 84;
            this.txtApplyNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtApplyNum_KeyDown);
            // 
            // rbtEnd
            // 
            this.rbtEnd.AutoSize = true;
            this.rbtEnd.Location = new System.Drawing.Point(576, 9);
            this.rbtEnd.Name = "rbtEnd";
            this.rbtEnd.Size = new System.Drawing.Size(122, 20);
            this.rbtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtEnd.TabIndex = 86;
            this.rbtEnd.Text = "覆盖原有已选";
            this.rbtEnd.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(714, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 83;
            this.label5.Text = "申请编号:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(385, 5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 29);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...浏览";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // rbtFirst
            // 
            this.rbtFirst.AutoSize = true;
            this.rbtFirst.Checked = true;
            this.rbtFirst.Location = new System.Drawing.Point(480, 9);
            this.rbtFirst.Name = "rbtFirst";
            this.rbtFirst.Size = new System.Drawing.Size(90, 20);
            this.rbtFirst.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtFirst.TabIndex = 85;
            this.rbtFirst.TabStop = true;
            this.rbtFirst.Text = "追加标本";
            this.rbtFirst.UseVisualStyleBackColor = true;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(76, 6);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(303, 26);
            this.txtFileName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbOutputOperName);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbOutInfo);
            this.panel2.Controls.Add(this.rbMt);
            this.panel2.Controls.Add(this.rbURt);
            this.panel2.Controls.Add(this.rbRt);
            this.panel2.Controls.Add(this.nudBorDay);
            this.panel2.Controls.Add(this.nudCount);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtOtherDemand);
            this.panel2.Controls.Add(this.chkBack);
            this.panel2.Controls.Add(this.chkSelectAll);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1067, 75);
            this.panel2.TabIndex = 1;
            // 
            // cmbOutputOperName
            // 
            //this.cmbOutputOperName.A = false;
            this.cmbOutputOperName.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOutputOperName.FormattingEnabled = true;
            this.cmbOutputOperName.IsFlat = true;
            this.cmbOutputOperName.IsLike = true;
            this.cmbOutputOperName.Location = new System.Drawing.Point(675, 1);
            this.cmbOutputOperName.Name = "cmbOutputOperName";
            this.cmbOutputOperName.PopForm = null;
            this.cmbOutputOperName.ShowCustomerList = false;
            this.cmbOutputOperName.ShowID = false;
            this.cmbOutputOperName.Size = new System.Drawing.Size(156, 24);
            this.cmbOutputOperName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbOutputOperName.TabIndex = 99;
            this.cmbOutputOperName.Tag = "";
            this.cmbOutputOperName.ToolBarUse = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(839, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 16);
            this.label9.TabIndex = 98;
            this.label9.Text = "出库输出情况:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(606, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 97;
            this.label8.Text = "执行人:";
            // 
            // tbOutInfo
            // 
            this.tbOutInfo.Location = new System.Drawing.Point(606, 28);
            this.tbOutInfo.Multiline = true;
            this.tbOutInfo.Name = "tbOutInfo";
            this.tbOutInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutInfo.Size = new System.Drawing.Size(444, 43);
            this.tbOutInfo.TabIndex = 96;
            // 
            // rbMt
            // 
            this.rbMt.AutoSize = true;
            this.rbMt.Location = new System.Drawing.Point(96, 49);
            this.rbMt.Name = "rbMt";
            this.rbMt.Size = new System.Drawing.Size(90, 20);
            this.rbMt.TabIndex = 94;
            this.rbMt.Text = "多次出库";
            this.rbMt.UseVisualStyleBackColor = true;
            this.rbMt.CheckedChanged += new System.EventHandler(this.rbMt_CheckedChanged);
            // 
            // rbURt
            // 
            this.rbURt.AutoSize = true;
            this.rbURt.Checked = true;
            this.rbURt.Location = new System.Drawing.Point(96, 1);
            this.rbURt.Name = "rbURt";
            this.rbURt.Size = new System.Drawing.Size(74, 20);
            this.rbURt.TabIndex = 93;
            this.rbURt.TabStop = true;
            this.rbURt.Text = "不归还";
            this.rbURt.UseVisualStyleBackColor = true;
            this.rbURt.CheckedChanged += new System.EventHandler(this.rbURt_CheckedChanged);
            // 
            // rbRt
            // 
            this.rbRt.AutoSize = true;
            this.rbRt.Location = new System.Drawing.Point(96, 25);
            this.rbRt.Name = "rbRt";
            this.rbRt.Size = new System.Drawing.Size(58, 20);
            this.rbRt.TabIndex = 92;
            this.rbRt.Text = "归还";
            this.rbRt.UseVisualStyleBackColor = true;
            this.rbRt.CheckedChanged += new System.EventHandler(this.rbRt_CheckedChanged);
            // 
            // nudBorDay
            // 
            this.nudBorDay.Location = new System.Drawing.Point(249, 2);
            this.nudBorDay.Name = "nudBorDay";
            this.nudBorDay.Size = new System.Drawing.Size(49, 26);
            this.nudBorDay.TabIndex = 83;
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(249, 43);
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(49, 26);
            this.nudCount.TabIndex = 84;
            this.nudCount.ValueChanged += new System.EventHandler(this.nudCount_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 91;
            this.label7.Text = "数量:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(335, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 90;
            this.label6.Text = "出库说明:";
            // 
            // txtOtherDemand
            // 
            this.txtOtherDemand.Location = new System.Drawing.Point(338, 27);
            this.txtOtherDemand.Multiline = true;
            this.txtOtherDemand.Name = "txtOtherDemand";
            this.txtOtherDemand.Size = new System.Drawing.Size(262, 44);
            this.txtOtherDemand.TabIndex = 89;
            // 
            // chkBack
            // 
            this.chkBack.AutoSize = true;
            this.chkBack.Location = new System.Drawing.Point(96, 5);
            this.chkBack.Name = "chkBack";
            this.chkBack.Size = new System.Drawing.Size(91, 20);
            this.chkBack.TabIndex = 88;
            this.chkBack.Text = "是否归还";
            this.chkBack.ThreeState = true;
            this.chkBack.UseVisualStyleBackColor = true;
            this.chkBack.Visible = false;
            this.chkBack.CheckStateChanged += new System.EventHandler(this.chkBack_CheckStateChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(22, 5);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(59, 20);
            this.chkSelectAll.TabIndex = 81;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 87;
            this.label3.Text = "天";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(304, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 85;
            this.label4.Text = "份";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 86;
            this.label2.Text = "期限:";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 113);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1067, 296);
            this.panel3.TabIndex = 0;
            // 
            // ucOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucOut";
            this.Size = new System.Drawing.Size(1067, 409);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudBorDay;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtApplyNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkBack;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtFirst;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtEnd;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.RadioButton rbMt;
        private System.Windows.Forms.RadioButton rbURt;
        private System.Windows.Forms.RadioButton rbRt;
        private System.Windows.Forms.TextBox tbOutInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOtherDemand;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOutputOperName;
    }
}
