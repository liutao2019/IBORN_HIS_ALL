namespace FS.HISFC.Components.HealthRecord.Case
{
    partial class ucPaitentBarCode
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblDeptName = new System.Windows.Forms.Label();
            this.lblpatientNoAndInTimes = new System.Windows.Forms.Label();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 103);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.lblPatientName);
            this.groupBox1.Controls.Add(this.lblDeptName);
            this.groupBox1.Controls.Add(this.lblpatientNoAndInTimes);
            this.groupBox1.Controls.Add(this.lblBarCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(5, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 28);
            this.pictureBox1.TabIndex = 1803;
            this.pictureBox1.TabStop = false;
            // 
            // lblPatientName
            // 
            this.lblPatientName.Location = new System.Drawing.Point(6, 80);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(122, 17);
            this.lblPatientName.TabIndex = 1;
            this.lblPatientName.Text = "患者姓名";
            // 
            // lblDeptName
            // 
            this.lblDeptName.Location = new System.Drawing.Point(6, 61);
            this.lblDeptName.Name = "lblDeptName";
            this.lblDeptName.Size = new System.Drawing.Size(122, 17);
            this.lblDeptName.TabIndex = 1;
            this.lblDeptName.Text = "科室名称";
            // 
            // lblpatientNoAndInTimes
            // 
            this.lblpatientNoAndInTimes.Location = new System.Drawing.Point(6, 40);
            this.lblpatientNoAndInTimes.Name = "lblpatientNoAndInTimes";
            this.lblpatientNoAndInTimes.Size = new System.Drawing.Size(122, 17);
            this.lblpatientNoAndInTimes.TabIndex = 1;
            this.lblpatientNoAndInTimes.Text = "住院号-住次";
            // 
            // lblBarCode
            // 
            this.lblBarCode.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBarCode.Location = new System.Drawing.Point(6, 12);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(121, 23);
            this.lblBarCode.TabIndex = 1;
            this.lblBarCode.Text = "lblBarCode";
            this.lblBarCode.Visible = false;
            // 
            // ucPaitentBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucPaitentBarCode";
            this.Size = new System.Drawing.Size(143, 103);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblDeptName;
        private System.Windows.Forms.Label lblpatientNoAndInTimes;
        private System.Windows.Forms.Label lblBarCode;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
