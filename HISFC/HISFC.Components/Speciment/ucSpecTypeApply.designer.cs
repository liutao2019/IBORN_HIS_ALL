namespace FS.HISFC.Components.Speciment
{
    partial class ucSpecTypeApply
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDisType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.flpTumorType = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbOrgType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "病种：";
            // 
            // cmbDisType
            // 
            this.cmbDisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisType.FormattingEnabled = true;
            this.cmbDisType.Location = new System.Drawing.Point(50, 37);
            this.cmbDisType.Name = "cmbDisType";
            this.cmbDisType.Size = new System.Drawing.Size(100, 20);
            this.cmbDisType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "标本类型：";
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(297, 9);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(99, 20);
            this.cmbSpecType.TabIndex = 3;
            // 
            // flpTumorType
            // 
            this.flpTumorType.AutoScroll = true;
            this.flpTumorType.Location = new System.Drawing.Point(156, 32);
            this.flpTumorType.Name = "flpTumorType";
            this.flpTumorType.Size = new System.Drawing.Size(456, 29);
            this.flpTumorType.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(404, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "数量：";
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(450, 8);
            this.nudCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(73, 21);
            this.nudCount.TabIndex = 6;
            this.nudCount.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(618, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // cmbOrgType
            // 
            this.cmbOrgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgType.FormattingEnabled = true;
            this.cmbOrgType.Location = new System.Drawing.Point(98, 7);
            this.cmbOrgType.Name = "cmbOrgType";
            this.cmbOrgType.Size = new System.Drawing.Size(121, 20);
            this.cmbOrgType.TabIndex = 10;
            this.cmbOrgType.SelectedIndexChanged += new System.EventHandler(this.cmbOrgType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "标本组织类型：";
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(618, 32);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 11;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // ucSpecTypeApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.cmbOrgType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.nudCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flpTumorType);
            this.Controls.Add(this.cmbSpecType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDisType);
            this.Controls.Add(this.label1);
            this.Name = "ucSpecTypeApply";
            this.Size = new System.Drawing.Size(720, 63);
            this.Load += new System.EventHandler(this.ucSpecTypeApply_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDisType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private System.Windows.Forms.FlowLayoutPanel flpTumorType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbOrgType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDel;
    }
}
