namespace FS.HISFC.Components.Speciment.SpecType
{
    partial class frmSpecType
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbOrgType = new System.Windows.Forms.ComboBox();
            this.cmbSpecName = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.diaColor = new System.Windows.Forms.ColorDialog();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.chk = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nupCnt = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudCapacity = new System.Windows.Forms.NumericUpDown();
            this.lblMl = new System.Windows.Forms.Label();
            this.txtAbrre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nupCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "标本种类：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 147);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "名    称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, -5);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "颜    色：";
            this.label3.Visible = false;
            // 
            // cmbOrgType
            // 
            this.cmbOrgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgType.FormattingEnabled = true;
            this.cmbOrgType.Location = new System.Drawing.Point(175, 15);
            this.cmbOrgType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOrgType.Name = "cmbOrgType";
            this.cmbOrgType.Size = new System.Drawing.Size(160, 24);
            this.cmbOrgType.TabIndex = 3;
            this.cmbOrgType.SelectedIndexChanged += new System.EventHandler(this.cmbOrgType_SelectedIndexChanged);
            // 
            // cmbSpecName
            // 
            this.cmbSpecName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecName.FormattingEnabled = true;
            this.cmbSpecName.Location = new System.Drawing.Point(175, 76);
            this.cmbSpecName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecName.Name = "cmbSpecName";
            this.cmbSpecName.Size = new System.Drawing.Size(160, 24);
            this.cmbSpecName.TabIndex = 4;
            this.cmbSpecName.SelectedIndexChanged += new System.EventHandler(this.cmbSpecName_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(33, 362);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 31);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(177, 362);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(103, 31);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(326, 362);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 31);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(175, 142);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 26);
            this.txtName.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "标本种类和分装：";
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(264, -10);
            this.txtColor.Margin = new System.Windows.Forms.Padding(4);
            this.txtColor.Name = "txtColor";
            this.txtColor.ReadOnly = true;
            this.txtColor.Size = new System.Drawing.Size(101, 26);
            this.txtColor.TabIndex = 11;
            this.txtColor.Visible = false;
            this.txtColor.Click += new System.EventHandler(this.txtColor_Click);
            // 
            // chk
            // 
            this.chk.AutoSize = true;
            this.chk.Location = new System.Drawing.Point(48, 267);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(59, 20);
            this.chk.TabIndex = 12;
            this.chk.Text = "分装";
            this.chk.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "默认分装支数:";
            // 
            // nupCnt
            // 
            this.nupCnt.Location = new System.Drawing.Point(259, 266);
            this.nupCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupCnt.Name = "nupCnt";
            this.nupCnt.Size = new System.Drawing.Size(120, 26);
            this.nupCnt.TabIndex = 14;
            this.nupCnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 317);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "分装体积:";
            // 
            // nudCapacity
            // 
            this.nudCapacity.DecimalPlaces = 2;
            this.nudCapacity.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCapacity.Location = new System.Drawing.Point(176, 312);
            this.nudCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudCapacity.Name = "nudCapacity";
            this.nudCapacity.Size = new System.Drawing.Size(120, 26);
            this.nudCapacity.TabIndex = 16;
            this.nudCapacity.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // lblMl
            // 
            this.lblMl.AutoSize = true;
            this.lblMl.Location = new System.Drawing.Point(312, 317);
            this.lblMl.Name = "lblMl";
            this.lblMl.Size = new System.Drawing.Size(24, 16);
            this.lblMl.TabIndex = 17;
            this.lblMl.Text = "ml";
            // 
            // txtAbrre
            // 
            this.txtAbrre.Location = new System.Drawing.Point(175, 208);
            this.txtAbrre.Margin = new System.Windows.Forms.Padding(4);
            this.txtAbrre.Name = "txtAbrre";
            this.txtAbrre.Size = new System.Drawing.Size(160, 26);
            this.txtAbrre.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 213);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = "缩    写：";
            // 
            // frmSpecType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(448, 408);
            this.Controls.Add(this.txtAbrre);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblMl);
            this.Controls.Add(this.nudCapacity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nupCnt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chk);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbSpecName);
            this.Controls.Add(this.cmbOrgType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpecType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标本种类和分装设置";
            this.Load += new System.EventHandler(this.frmSpecType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nupCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbOrgType;
        private System.Windows.Forms.ComboBox cmbSpecName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColorDialog diaColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.CheckBox chk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nupCnt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudCapacity;
        private System.Windows.Forms.Label lblMl;
        private System.Windows.Forms.TextBox txtAbrre;
        private System.Windows.Forms.Label label7;
    }
}