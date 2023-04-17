namespace SOC.Local.LisInterface.ZDWY
{
    partial class frmLisResultShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbResultShow = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.cbxIsAll = new System.Windows.Forms.CheckBox();
            this.lblDeptInfo = new System.Windows.Forms.Label();
            this.lblDeptAll = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbResultShow
            // 
            this.wbResultShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbResultShow.Location = new System.Drawing.Point(0, 37);
            this.wbResultShow.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbResultShow.Name = "wbResultShow";
            this.wbResultShow.Size = new System.Drawing.Size(803, 319);
            this.wbResultShow.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDeptAll);
            this.panel1.Controls.Add(this.lblDeptInfo);
            this.panel1.Controls.Add(this.lblPatientInfo);
            this.panel1.Controls.Add(this.cbxIsAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 37);
            this.panel1.TabIndex = 1;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPatientInfo.Location = new System.Drawing.Point(321, 14);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(38, 12);
            this.lblPatientInfo.TabIndex = 1;
            this.lblPatientInfo.Text = "姓名:";
            // 
            // cbxIsAll
            // 
            this.cbxIsAll.AutoSize = true;
            this.cbxIsAll.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxIsAll.Location = new System.Drawing.Point(9, 9);
            this.cbxIsAll.Name = "cbxIsAll";
            this.cbxIsAll.Size = new System.Drawing.Size(163, 20);
            this.cbxIsAll.TabIndex = 0;
            this.cbxIsAll.Text = "查看所有在院记录";
            this.cbxIsAll.UseVisualStyleBackColor = true;
            this.cbxIsAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblDeptInfo
            // 
            this.lblDeptInfo.AutoSize = true;
            this.lblDeptInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDeptInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDeptInfo.Location = new System.Drawing.Point(199, 14);
            this.lblDeptInfo.Name = "lblDeptInfo";
            this.lblDeptInfo.Size = new System.Drawing.Size(77, 12);
            this.lblDeptInfo.TabIndex = 2;
            this.lblDeptInfo.Text = "科室:综合科";
            // 
            // lblDeptAll
            // 
            this.lblDeptAll.AutoSize = true;
            this.lblDeptAll.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDeptAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDeptAll.Location = new System.Drawing.Point(199, 14);
            this.lblDeptAll.Name = "lblDeptAll";
            this.lblDeptAll.Size = new System.Drawing.Size(64, 12);
            this.lblDeptAll.TabIndex = 3;
            this.lblDeptAll.Text = "科室:全院";
            this.lblDeptAll.Visible = false;
            // 
            // frmLisResultShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 356);
            this.Controls.Add(this.wbResultShow);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmLisResultShow";
            this.Text = "LIS结果查看";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbResultShow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbxIsAll;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Label lblDeptInfo;
        private System.Windows.Forms.Label lblDeptAll;

    }
}