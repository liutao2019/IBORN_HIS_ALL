namespace FS.HISFC.Components.Speciment
{
    partial class ucImplent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpCondition = new System.Windows.Forms.GroupBox();
            this.cmbEmployee = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.chkImped = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbApplyDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtApplyNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flpApplyDemand = new System.Windows.Forms.FlowLayoutPanel();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.tpApply = new System.Windows.Forms.TabPage();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.dclDetail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dclViewSpecList = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dclReturnSpec = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dclSpecSchedule = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dclSpecAppConfirm = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpSpec = new System.Windows.Forms.TabPage();
            this.tpReturn = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chxUnImped = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxReject = new System.Windows.Forms.CheckBox();
            this.grpCondition.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tpApply.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCondition
            // 
            this.grpCondition.Controls.Add(this.cbxReject);
            this.grpCondition.Controls.Add(this.label7);
            this.grpCondition.Controls.Add(this.label6);
            this.grpCondition.Controls.Add(this.label5);
            this.grpCondition.Controls.Add(this.label4);
            this.grpCondition.Controls.Add(this.chxUnImped);
            this.grpCondition.Controls.Add(this.cmbEmployee);
            this.grpCondition.Controls.Add(this.chkImped);
            this.grpCondition.Controls.Add(this.label3);
            this.grpCondition.Controls.Add(this.label1);
            this.grpCondition.Controls.Add(this.cmbApplyDept);
            this.grpCondition.Controls.Add(this.txtApplyNum);
            this.grpCondition.Controls.Add(this.label2);
            this.grpCondition.Location = new System.Drawing.Point(3, 3);
            this.grpCondition.Name = "grpCondition";
            this.grpCondition.Size = new System.Drawing.Size(795, 61);
            this.grpCondition.TabIndex = 69;
            this.grpCondition.TabStop = false;
            this.grpCondition.Text = "审批申请单查找";
            // 
            // cmbEmployee
            // 
            //this.cmbEmployee.A = false;
            this.cmbEmployee.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.IsFlat = true;
            this.cmbEmployee.IsLike = true;
            this.cmbEmployee.Location = new System.Drawing.Point(262, 22);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.PopForm = null;
            this.cmbEmployee.ShowCustomerList = false;
            this.cmbEmployee.ShowID = false;
            this.cmbEmployee.Size = new System.Drawing.Size(124, 21);
            this.cmbEmployee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbEmployee.TabIndex = 70;
            this.cmbEmployee.Tag = "";
            this.cmbEmployee.ToolBarUse = false;
            // 
            // chkImped
            // 
            this.chkImped.AutoSize = true;
            this.chkImped.Checked = true;
            this.chkImped.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkImped.Location = new System.Drawing.Point(573, 15);
            this.chkImped.Name = "chkImped";
            this.chkImped.Size = new System.Drawing.Size(68, 18);
            this.chkImped.TabIndex = 69;
            this.chkImped.Text = "已审批";
            this.chkImped.UseVisualStyleBackColor = true;
            this.chkImped.CheckedChanged += new System.EventHandler(this.chkImped_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 67;
            this.label3.Text = "申请人姓名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "申请编号：";
            // 
            // cmbApplyDept
            // 
            //this.cmbApplyDept.A = false;
            this.cmbApplyDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbApplyDept.FormattingEnabled = true;
            this.cmbApplyDept.IsFlat = true;
            this.cmbApplyDept.IsLike = true;
            this.cmbApplyDept.Location = new System.Drawing.Point(436, 22);
            this.cmbApplyDept.Name = "cmbApplyDept";
            this.cmbApplyDept.PopForm = null;
            this.cmbApplyDept.ShowCustomerList = false;
            this.cmbApplyDept.ShowID = false;
            this.cmbApplyDept.Size = new System.Drawing.Size(124, 21);
            this.cmbApplyDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbApplyDept.TabIndex = 66;
            this.cmbApplyDept.Tag = "";
            this.cmbApplyDept.ToolBarUse = false;
            // 
            // txtApplyNum
            // 
            this.txtApplyNum.Location = new System.Drawing.Point(86, 23);
            this.txtApplyNum.Name = "txtApplyNum";
            this.txtApplyNum.Size = new System.Drawing.Size(91, 23);
            this.txtApplyNum.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "科室：";
            // 
            // flpApplyDemand
            // 
            this.flpApplyDemand.AutoScroll = true;
            this.flpApplyDemand.Location = new System.Drawing.Point(804, 3);
            this.flpApplyDemand.Name = "flpApplyDemand";
            this.flpApplyDemand.Size = new System.Drawing.Size(426, 61);
            this.flpApplyDemand.TabIndex = 73;
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.tabResult);
            this.grpResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResult.Location = new System.Drawing.Point(0, 0);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(1281, 783);
            this.grpResult.TabIndex = 74;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "查询结果";
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.tpApply);
            this.tabResult.Controls.Add(this.tpSpec);
            this.tabResult.Controls.Add(this.tpReturn);
            this.tabResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResult.Location = new System.Drawing.Point(3, 19);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.Size = new System.Drawing.Size(1275, 761);
            this.tabResult.TabIndex = 0;
            // 
            // tpApply
            // 
            this.tpApply.AutoScroll = true;
            this.tpApply.Controls.Add(this.dgvResult);
            this.tpApply.Location = new System.Drawing.Point(4, 22);
            this.tpApply.Name = "tpApply";
            this.tpApply.Padding = new System.Windows.Forms.Padding(3);
            this.tpApply.Size = new System.Drawing.Size(1267, 735);
            this.tpApply.TabIndex = 0;
            this.tpApply.Text = "申请单";
            this.tpApply.UseVisualStyleBackColor = true;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dclDetail,
            this.dclViewSpecList,
            this.dclReturnSpec,
            this.dclSpecSchedule,
            this.dclSpecAppConfirm});
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(3, 3);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.Size = new System.Drawing.Size(1261, 729);
            this.dgvResult.TabIndex = 0;
            this.dgvResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellClick);
            // 
            // dclDetail
            // 
            this.dclDetail.HeaderText = "详情";
            this.dclDetail.Name = "dclDetail";
            this.dclDetail.Text = "详情";
            this.dclDetail.UseColumnTextForButtonValue = true;
            this.dclDetail.Width = 50;
            // 
            // dclViewSpecList
            // 
            this.dclViewSpecList.HeaderText = "查看标本列表";
            this.dclViewSpecList.Name = "dclViewSpecList";
            this.dclViewSpecList.Text = "查看标本列表";
            this.dclViewSpecList.UseColumnTextForButtonValue = true;
            // 
            // dclReturnSpec
            // 
            this.dclReturnSpec.HeaderText = "归还标本";
            this.dclReturnSpec.Name = "dclReturnSpec";
            this.dclReturnSpec.Text = "归还标本";
            this.dclReturnSpec.UseColumnTextForButtonValue = true;
            this.dclReturnSpec.Width = 80;
            // 
            // dclSpecSchedule
            // 
            this.dclSpecSchedule.HeaderText = "进度情况";
            this.dclSpecSchedule.Name = "dclSpecSchedule";
            this.dclSpecSchedule.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dclSpecSchedule.Text = "进度情况";
            this.dclSpecSchedule.UseColumnTextForButtonValue = true;
            // 
            // dclSpecAppConfirm
            // 
            this.dclSpecAppConfirm.HeaderText = "审批";
            this.dclSpecAppConfirm.Name = "dclSpecAppConfirm";
            this.dclSpecAppConfirm.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dclSpecAppConfirm.Text = "审批";
            this.dclSpecAppConfirm.UseColumnTextForButtonValue = true;
            // 
            // tpSpec
            // 
            this.tpSpec.AutoScroll = true;
            this.tpSpec.Location = new System.Drawing.Point(4, 22);
            this.tpSpec.Name = "tpSpec";
            this.tpSpec.Padding = new System.Windows.Forms.Padding(3);
            this.tpSpec.Size = new System.Drawing.Size(1267, 737);
            this.tpSpec.TabIndex = 1;
            this.tpSpec.Text = "标本列表";
            this.tpSpec.UseVisualStyleBackColor = true;
            // 
            // tpReturn
            // 
            this.tpReturn.Location = new System.Drawing.Point(4, 22);
            this.tpReturn.Name = "tpReturn";
            this.tpReturn.Padding = new System.Windows.Forms.Padding(3);
            this.tpReturn.Size = new System.Drawing.Size(1267, 737);
            this.tpReturn.TabIndex = 2;
            this.tpReturn.Text = "归还标本";
            this.tpReturn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpCondition);
            this.panel1.Controls.Add(this.flpApplyDemand);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1281, 72);
            this.panel1.TabIndex = 75;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1281, 855);
            this.panel2.TabIndex = 76;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grpResult);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1281, 783);
            this.panel3.TabIndex = 76;
            // 
            // chxUnImped
            // 
            this.chxUnImped.AutoSize = true;
            this.chxUnImped.Checked = true;
            this.chxUnImped.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxUnImped.Location = new System.Drawing.Point(644, 15);
            this.chxUnImped.Name = "chxUnImped";
            this.chxUnImped.Size = new System.Drawing.Size(68, 18);
            this.chxUnImped.TabIndex = 71;
            this.chxUnImped.Text = "未审批";
            this.chxUnImped.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightPink;
            this.label4.Location = new System.Drawing.Point(658, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 10);
            this.label4.TabIndex = 72;
            this.label4.Text = " ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(678, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 73;
            this.label5.Text = "拒绝审批";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightGreen;
            this.label6.Location = new System.Drawing.Point(572, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 10);
            this.label6.TabIndex = 74;
            this.label6.Text = " ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(589, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 75;
            this.label7.Text = "未审批";
            // 
            // cbxReject
            // 
            this.cbxReject.AutoSize = true;
            this.cbxReject.Location = new System.Drawing.Point(718, 15);
            this.cbxReject.Name = "cbxReject";
            this.cbxReject.Size = new System.Drawing.Size(54, 18);
            this.cbxReject.TabIndex = 76;
            this.cbxReject.Text = "拒绝";
            this.cbxReject.UseVisualStyleBackColor = true;
            // 
            // ucImplent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucImplent";
            this.Size = new System.Drawing.Size(1281, 855);
            this.Load += new System.EventHandler(this.ucImplent_Load);
            this.grpCondition.ResumeLayout(false);
            this.grpCondition.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            this.tpApply.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCondition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbApplyDept;
        private System.Windows.Forms.TextBox txtApplyNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flpApplyDemand;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tpApply;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.CheckBox chkImped;
        private System.Windows.Forms.TabPage tpReturn;
        private System.Windows.Forms.TabPage tpSpec;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbEmployee;
        private System.Windows.Forms.DataGridViewButtonColumn dclDetail;
        private System.Windows.Forms.DataGridViewButtonColumn dclViewSpecList;
        private System.Windows.Forms.DataGridViewButtonColumn dclReturnSpec;
        private System.Windows.Forms.DataGridViewButtonColumn dclSpecSchedule;
        private System.Windows.Forms.DataGridViewButtonColumn dclSpecAppConfirm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chxUnImped;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbxReject;
        private System.Windows.Forms.Label label7;

    }
}
