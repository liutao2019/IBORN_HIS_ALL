namespace FS.HISFC.Components.Speciment.DataOper
{
    partial class frmInTime
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
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.rbtBld = new System.Windows.Forms.RadioButton();
            this.rbtOrg = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpOperTime = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // dtpTime
            // 
            this.dtpTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Location = new System.Drawing.Point(153, 124);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(179, 26);
            this.dtpTime.TabIndex = 0;
            // 
            // rbtBld
            // 
            this.rbtBld.AutoSize = true;
            this.rbtBld.Checked = true;
            this.rbtBld.Location = new System.Drawing.Point(92, 33);
            this.rbtBld.Name = "rbtBld";
            this.rbtBld.Size = new System.Drawing.Size(42, 20);
            this.rbtBld.TabIndex = 1;
            this.rbtBld.TabStop = true;
            this.rbtBld.Text = "血";
            this.rbtBld.UseVisualStyleBackColor = true;
            // 
            // rbtOrg
            // 
            this.rbtOrg.AutoSize = true;
            this.rbtOrg.Location = new System.Drawing.Point(223, 33);
            this.rbtOrg.Name = "rbtOrg";
            this.rbtOrg.Size = new System.Drawing.Size(58, 20);
            this.rbtOrg.TabIndex = 2;
            this.rbtOrg.Text = "组织";
            this.rbtOrg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "入库时间:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(92, 178);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 34);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(223, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "分装时间:";
            // 
            // dtpOperTime
            // 
            this.dtpOperTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpOperTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOperTime.Location = new System.Drawing.Point(153, 70);
            this.dtpOperTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpOperTime.Name = "dtpOperTime";
            this.dtpOperTime.Size = new System.Drawing.Size(179, 26);
            this.dtpOperTime.TabIndex = 7;
            // 
            // frmInTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 249);
            this.Controls.Add(this.dtpOperTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtOrg);
            this.Controls.Add(this.rbtBld);
            this.Controls.Add(this.dtpTime);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmInTime";
            this.Text = "更新入库时间";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.RadioButton rbtBld;
        private System.Windows.Forms.RadioButton rbtOrg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpOperTime;
    }
}