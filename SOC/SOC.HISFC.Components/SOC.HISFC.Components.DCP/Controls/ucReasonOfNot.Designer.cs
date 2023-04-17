namespace FS.SOC.HISFC.Components.DCP.Controls
{
    partial class ucReasonOfNot
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
            this.gbDiseaseInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtOtherName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cl1 = new System.Windows.Forms.Label();
            this.cmbReasons = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label48 = new System.Windows.Forms.Label();
            this.gbDiseaseInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDiseaseInfo
            // 
            this.gbDiseaseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDiseaseInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbDiseaseInfo.Controls.Add(this.txtOtherName);
            this.gbDiseaseInfo.Controls.Add(this.label1);
            this.gbDiseaseInfo.Controls.Add(this.label2);
            this.gbDiseaseInfo.Controls.Add(this.button2);
            this.gbDiseaseInfo.Controls.Add(this.button1);
            this.gbDiseaseInfo.Controls.Add(this.cl1);
            this.gbDiseaseInfo.Controls.Add(this.cmbReasons);
            this.gbDiseaseInfo.Controls.Add(this.label48);
            this.gbDiseaseInfo.Location = new System.Drawing.Point(3, 3);
            this.gbDiseaseInfo.Name = "gbDiseaseInfo";
            this.gbDiseaseInfo.Size = new System.Drawing.Size(522, 161);
            this.gbDiseaseInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbDiseaseInfo.TabIndex = 0;
            this.gbDiseaseInfo.TabStop = false;
            this.gbDiseaseInfo.Text = "请填写不报卡原因";
            // 
            // txtOtherName
            // 
            this.txtOtherName.Enabled = false;
            this.txtOtherName.Location = new System.Drawing.Point(270, 66);
            this.txtOtherName.Name = "txtOtherName";
            this.txtOtherName.Size = new System.Drawing.Size(174, 21);
            this.txtOtherName.TabIndex = 116;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(107, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 115;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(123, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 114;
            this.label2.Text = "请填写已报卡的外院名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(410, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 112;
            this.button2.Text = "填写报卡";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(219, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 111;
            this.button1.Text = "保存不报卡原因";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cl1
            // 
            this.cl1.AutoSize = true;
            this.cl1.ForeColor = System.Drawing.Color.Red;
            this.cl1.Location = new System.Drawing.Point(109, 34);
            this.cl1.Name = "cl1";
            this.cl1.Size = new System.Drawing.Size(11, 12);
            this.cl1.TabIndex = 110;
            this.cl1.Text = "*";
            this.cl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbReasons
            // 
            this.cmbReasons.ArrowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cmbReasons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbReasons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReasons.DropDownWidth = 174;
            this.cmbReasons.Font = new System.Drawing.Font("宋体", 10.5F);
            this.cmbReasons.IsEnter2Tab = false;
            this.cmbReasons.IsFlat = false;
            this.cmbReasons.IsLike = true;
            this.cmbReasons.IsListOnly = false;
            this.cmbReasons.IsPopForm = true;
            this.cmbReasons.IsShowCustomerList = false;
            this.cmbReasons.IsShowID = false;
            this.cmbReasons.IsShowIDAndName = false;
            this.cmbReasons.ItemHeight = 14;
            this.cmbReasons.Items.AddRange(new object[] {
            "ds"});
            this.cmbReasons.Location = new System.Drawing.Point(270, 30);
            this.cmbReasons.MaxDropDownItems = 20;
            this.cmbReasons.Name = "cmbReasons";
            this.cmbReasons.ShowCustomerList = false;
            this.cmbReasons.ShowID = false;
            this.cmbReasons.Size = new System.Drawing.Size(174, 22);
            this.cmbReasons.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbReasons.TabIndex = 0;
            this.cmbReasons.Tag = "";
            this.cmbReasons.ToolBarUse = false;
            this.cmbReasons.SelectedIndexChanged += new System.EventHandler(this.cmbReasons_SelectedIndexChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("宋体", 9F);
            this.label48.Location = new System.Drawing.Point(125, 35);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(101, 12);
            this.label48.TabIndex = 92;
            this.label48.Text = "请填写不报卡原因";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucReasonOfNot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.Controls.Add(this.gbDiseaseInfo);
            this.Name = "ucReasonOfNot";
            this.Size = new System.Drawing.Size(528, 168);
            this.gbDiseaseInfo.ResumeLayout(false);
            this.gbDiseaseInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbDiseaseInfo;
        private System.Windows.Forms.TextBox txtOtherName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label cl1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbReasons;
        private System.Windows.Forms.Label label48;
    }
}
