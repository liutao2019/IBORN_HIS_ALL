namespace UFC.Speciment
{
    partial class ucCondition
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
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.cmbOperator = new System.Windows.Forms.ComboBox();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.rbtAnd = new System.Windows.Forms.RadioButton();
            this.rbtOr = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cmbField
            // 
            this.cmbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(3, 3);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(149, 20);
            this.cmbField.TabIndex = 23;
            this.cmbField.SelectedValueChanged += new System.EventHandler(this.cmbField_SelectedValueChanged);
            // 
            // cmbOperator
            // 
            this.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Location = new System.Drawing.Point(158, 3);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(67, 20);
            this.cmbOperator.TabIndex = 25;
            // 
            // txtCondition
            // 
            this.txtCondition.Location = new System.Drawing.Point(231, 3);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Size = new System.Drawing.Size(138, 21);
            this.txtCondition.TabIndex = 26;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(375, 3);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(138, 21);
            this.dtpDate.TabIndex = 24;
            // 
            // rbtAnd
            // 
            this.rbtAnd.AutoSize = true;
            this.rbtAnd.Location = new System.Drawing.Point(519, 6);
            this.rbtAnd.Name = "rbtAnd";
            this.rbtAnd.Size = new System.Drawing.Size(41, 16);
            this.rbtAnd.TabIndex = 28;
            this.rbtAnd.TabStop = true;
            this.rbtAnd.Text = "AND";
            this.rbtAnd.UseVisualStyleBackColor = true;
            // 
            // rbtOr
            // 
            this.rbtOr.AutoSize = true;
            this.rbtOr.Location = new System.Drawing.Point(564, 6);
            this.rbtOr.Name = "rbtOr";
            this.rbtOr.Size = new System.Drawing.Size(35, 16);
            this.rbtOr.TabIndex = 29;
            this.rbtOr.TabStop = true;
            this.rbtOr.Text = "OR";
            this.rbtOr.UseVisualStyleBackColor = true;
            // 
            // ucCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbField);
            this.Controls.Add(this.cmbOperator);
            this.Controls.Add(this.txtCondition);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.rbtAnd);
            this.Controls.Add(this.rbtOr);
            this.Name = "ucCondition";
            this.Size = new System.Drawing.Size(602, 28);
            this.Load += new System.EventHandler(this.ucCondition_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.RadioButton rbtAnd;
        private System.Windows.Forms.RadioButton rbtOr;
    }
}
