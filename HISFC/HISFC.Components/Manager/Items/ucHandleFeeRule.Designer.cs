namespace FS.HISFC.Components.Manager.Items
{
    partial class ucHandleFeeRule
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
            this.lbDescription = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRemarks = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbItem = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.chkedList = new FS.FrameWork.WinForms.Controls.NeuCheckedListBox();
            this.txtQuality = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.rdoQuality = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rdoTime = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.cmbRegular = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel61 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel60 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel59 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel58 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel57 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnDelete = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuTextBox2 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbItemCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ruleCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ckOutFee = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDescription.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbDescription.Location = new System.Drawing.Point(504, 139);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(29, 12);
            this.lbDescription.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDescription.TabIndex = 29;
            this.lbDescription.Text = "说明";
            this.lbDescription.Visible = false;
            // 
            // lbRemarks
            // 
            this.lbRemarks.AutoSize = true;
            this.lbRemarks.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRemarks.ForeColor = System.Drawing.Color.Red;
            this.lbRemarks.Location = new System.Drawing.Point(468, 140);
            this.lbRemarks.Name = "lbRemarks";
            this.lbRemarks.Size = new System.Drawing.Size(30, 14);
            this.lbRemarks.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRemarks.TabIndex = 28;
            this.lbRemarks.Text = "注:";
            this.lbRemarks.Visible = false;
            // 
            // cmbItem
            // 
            this.cmbItem.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.IsEnter2Tab = false;
            this.cmbItem.IsFlat = false;
            this.cmbItem.IsLike = true;
            this.cmbItem.IsListOnly = false;
            this.cmbItem.IsPopForm = true;
            this.cmbItem.IsShowCustomerList = false;
            this.cmbItem.IsShowID = false;
            this.cmbItem.Location = new System.Drawing.Point(93, 245);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.PopForm = null;
            this.cmbItem.ShowCustomerList = false;
            this.cmbItem.ShowID = false;
            this.cmbItem.Size = new System.Drawing.Size(121, 20);
            this.cmbItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbItem.TabIndex = 27;
            this.cmbItem.Tag = "";
            this.cmbItem.ToolBarUse = false;
            this.cmbItem.SelectedIndexChanged += new System.EventHandler(this.cmbItem_SelectedIndexChanged);
            // 
            // chkedList
            // 
            this.chkedList.FormattingEnabled = true;
            this.chkedList.Location = new System.Drawing.Point(93, 291);
            this.chkedList.Name = "chkedList";
            this.chkedList.Size = new System.Drawing.Size(420, 148);
            this.chkedList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkedList.TabIndex = 26;
            // 
            // txtQuality
            // 
            this.txtQuality.IsEnter2Tab = false;
            this.txtQuality.Location = new System.Drawing.Point(325, 194);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(80, 21);
            this.txtQuality.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtQuality.TabIndex = 25;
            // 
            // rdoQuality
            // 
            this.rdoQuality.AutoSize = true;
            this.rdoQuality.Location = new System.Drawing.Point(186, 195);
            this.rdoQuality.Name = "rdoQuality";
            this.rdoQuality.Size = new System.Drawing.Size(59, 16);
            this.rdoQuality.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdoQuality.TabIndex = 24;
            this.rdoQuality.TabStop = true;
            this.rdoQuality.Text = "按数量";
            this.rdoQuality.UseVisualStyleBackColor = true;
            // 
            // rdoTime
            // 
            this.rdoTime.AutoSize = true;
            this.rdoTime.Location = new System.Drawing.Point(93, 195);
            this.rdoTime.Name = "rdoTime";
            this.rdoTime.Size = new System.Drawing.Size(59, 16);
            this.rdoTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdoTime.TabIndex = 23;
            this.rdoTime.TabStop = true;
            this.rdoTime.Text = "按时间";
            this.rdoTime.UseVisualStyleBackColor = true;
            // 
            // cmbRegular
            // 
            this.cmbRegular.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbRegular.FormattingEnabled = true;
            this.cmbRegular.IsEnter2Tab = false;
            this.cmbRegular.IsFlat = false;
            this.cmbRegular.IsLike = true;
            this.cmbRegular.IsListOnly = false;
            this.cmbRegular.IsPopForm = true;
            this.cmbRegular.IsShowCustomerList = false;
            this.cmbRegular.IsShowID = false;
            this.cmbRegular.Location = new System.Drawing.Point(93, 136);
            this.cmbRegular.Name = "cmbRegular";
            this.cmbRegular.PopForm = null;
            this.cmbRegular.ShowCustomerList = false;
            this.cmbRegular.ShowID = false;
            this.cmbRegular.Size = new System.Drawing.Size(325, 20);
            this.cmbRegular.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRegular.TabIndex = 22;
            this.cmbRegular.Tag = "";
            this.cmbRegular.ToolBarUse = false;
            // 
            // neuLabel61
            // 
            this.neuLabel61.AutoSize = true;
            this.neuLabel61.Location = new System.Drawing.Point(23, 142);
            this.neuLabel61.Name = "neuLabel61";
            this.neuLabel61.Size = new System.Drawing.Size(65, 12);
            this.neuLabel61.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel61.TabIndex = 21;
            this.neuLabel61.Text = "收费规则：";
            // 
            // neuLabel60
            // 
            this.neuLabel60.AutoSize = true;
            this.neuLabel60.Location = new System.Drawing.Point(263, 199);
            this.neuLabel60.Name = "neuLabel60";
            this.neuLabel60.Size = new System.Drawing.Size(59, 12);
            this.neuLabel60.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel60.TabIndex = 20;
            this.neuLabel60.Text = "数量限额:";
            // 
            // neuLabel59
            // 
            this.neuLabel59.AutoSize = true;
            this.neuLabel59.Location = new System.Drawing.Point(23, 291);
            this.neuLabel59.Name = "neuLabel59";
            this.neuLabel59.Size = new System.Drawing.Size(65, 12);
            this.neuLabel59.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel59.TabIndex = 19;
            this.neuLabel59.Text = "项目列表：";
            // 
            // neuLabel58
            // 
            this.neuLabel58.AutoSize = true;
            this.neuLabel58.Location = new System.Drawing.Point(23, 248);
            this.neuLabel58.Name = "neuLabel58";
            this.neuLabel58.Size = new System.Drawing.Size(65, 12);
            this.neuLabel58.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel58.TabIndex = 18;
            this.neuLabel58.Text = "互斥项目：";
            // 
            // neuLabel57
            // 
            this.neuLabel57.AutoSize = true;
            this.neuLabel57.Location = new System.Drawing.Point(23, 197);
            this.neuLabel57.Name = "neuLabel57";
            this.neuLabel57.Size = new System.Drawing.Size(65, 12);
            this.neuLabel57.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel57.TabIndex = 17;
            this.neuLabel57.Text = "限制条件：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(283, 469);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 33;
            this.btnSave.Text = "保 存";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ckOutFee);
            this.neuGroupBox1.Controls.Add(this.btnDelete);
            this.neuGroupBox1.Controls.Add(this.neuTextBox2);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.tbItemCode);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.ruleCode);
            this.neuGroupBox1.Controls.Add(this.lbDescription);
            this.neuGroupBox1.Controls.Add(this.btnSave);
            this.neuGroupBox1.Controls.Add(this.lbRemarks);
            this.neuGroupBox1.Controls.Add(this.chkedList);
            this.neuGroupBox1.Controls.Add(this.cmbRegular);
            this.neuGroupBox1.Controls.Add(this.neuLabel61);
            this.neuGroupBox1.Controls.Add(this.rdoQuality);
            this.neuGroupBox1.Controls.Add(this.rdoTime);
            this.neuGroupBox1.Controls.Add(this.txtQuality);
            this.neuGroupBox1.Controls.Add(this.cmbItem);
            this.neuGroupBox1.Controls.Add(this.neuLabel59);
            this.neuGroupBox1.Controls.Add(this.neuLabel57);
            this.neuGroupBox1.Controls.Add(this.neuLabel58);
            this.neuGroupBox1.Controls.Add(this.neuLabel60);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(700, 507);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 34;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "项目收费规则维护";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(249, 243);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(55, 23);
            this.btnDelete.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnDelete.TabIndex = 40;
            this.btnDelete.Text = "移除(&D)";
            this.btnDelete.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.Remove;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // neuTextBox2
            // 
            this.neuTextBox2.IsEnter2Tab = false;
            this.neuTextBox2.Location = new System.Drawing.Point(93, 82);
            this.neuTextBox2.Name = "neuTextBox2";
            this.neuTextBox2.ReadOnly = true;
            this.neuTextBox2.Size = new System.Drawing.Size(405, 21);
            this.neuTextBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTextBox2.TabIndex = 39;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(23, 85);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 38;
            this.neuLabel3.Text = "项目名称：";
            // 
            // tbItemCode
            // 
            this.tbItemCode.IsEnter2Tab = false;
            this.tbItemCode.Location = new System.Drawing.Point(352, 35);
            this.tbItemCode.Name = "tbItemCode";
            this.tbItemCode.ReadOnly = true;
            this.tbItemCode.Size = new System.Drawing.Size(146, 21);
            this.tbItemCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbItemCode.TabIndex = 37;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(281, 38);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 36;
            this.neuLabel2.Text = "项目编码：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(23, 39);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 35;
            this.neuLabel1.Text = "规则编码：";
            // 
            // ruleCode
            // 
            this.ruleCode.IsEnter2Tab = false;
            this.ruleCode.Location = new System.Drawing.Point(93, 35);
            this.ruleCode.Name = "ruleCode";
            this.ruleCode.ReadOnly = true;
            this.ruleCode.Size = new System.Drawing.Size(114, 21);
            this.ruleCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ruleCode.TabIndex = 34;
            // 
            // ckOutFee
            // 
            this.ckOutFee.AutoSize = true;
            this.ckOutFee.Checked = true;
            this.ckOutFee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckOutFee.Location = new System.Drawing.Point(426, 196);
            this.ckOutFee.Name = "ckOutFee";
            this.ckOutFee.Size = new System.Drawing.Size(72, 16);
            this.ckOutFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckOutFee.TabIndex = 41;
            this.ckOutFee.Text = "出院计费";
            this.ckOutFee.UseVisualStyleBackColor = true;
            // 
            // ucHandleFeeRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucHandleFeeRule";
            this.Size = new System.Drawing.Size(700, 507);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel lbDescription;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRemarks;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbItem;
        private FS.FrameWork.WinForms.Controls.NeuCheckedListBox chkedList;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtQuality;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rdoQuality;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rdoTime;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbRegular;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel61;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel60;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel59;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel58;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel57;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ruleCode;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuTextBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbItemCode;
        private FS.FrameWork.WinForms.Controls.NeuButton btnDelete;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckOutFee;
    }
}
