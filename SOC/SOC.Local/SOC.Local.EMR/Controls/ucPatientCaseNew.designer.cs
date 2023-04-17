namespace FS.SOC.Local.EMR.Contrl
{
    public partial class ucPatientCaseNew
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
            try
            {
                this.tempCaseModule.Dispose();
                this.caseHistory.Dispose();
                System.GC.Collect();
            }
            catch { }
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
            this.pnlPatientCase = new System.Windows.Forms.Panel();
            this.cmbPatientCaseType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlPatientCase
            // 
            this.pnlPatientCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPatientCase.Location = new System.Drawing.Point(3, 41);
            this.pnlPatientCase.Name = "pnlPatientCase";
            this.pnlPatientCase.Size = new System.Drawing.Size(1035, 548);
            this.pnlPatientCase.TabIndex = 1;
            // 
            // cmbPatientCaseType
            // 
            this.cmbPatientCaseType.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPatientCaseType.FormattingEnabled = true;
            this.cmbPatientCaseType.Location = new System.Drawing.Point(286, 1);
            this.cmbPatientCaseType.Name = "cmbPatientCaseType";
            this.cmbPatientCaseType.Size = new System.Drawing.Size(121, 29);
            this.cmbPatientCaseType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文行楷", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "请选择要建立的病历类型：";
            // 
            // ucPatientCaseNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPatientCaseType);
            this.Controls.Add(this.pnlPatientCase);
            this.Name = "ucPatientCaseNew";
            this.Size = new System.Drawing.Size(1041, 592);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPatientCase;
        private System.Windows.Forms.ComboBox cmbPatientCaseType;
        private System.Windows.Forms.Label label1;




    }
}
