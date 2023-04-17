namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint
{
    partial class ucRecipeBase
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
            this.pnTitle = new System.Windows.Forms.Panel();
            this.pnPatientInfo = new System.Windows.Forms.Panel();
            this.pnOrderInfo = new System.Windows.Forms.Panel();
            this.pnOperInfo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnTitle
            // 
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(583, 100);
            this.pnTitle.TabIndex = 0;
            // 
            // pnPatientInfo
            // 
            this.pnPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnPatientInfo.Location = new System.Drawing.Point(0, 100);
            this.pnPatientInfo.Name = "pnPatientInfo";
            this.pnPatientInfo.Size = new System.Drawing.Size(583, 100);
            this.pnPatientInfo.TabIndex = 1;
            // 
            // pnOrderInfo
            // 
            this.pnOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnOrderInfo.Location = new System.Drawing.Point(0, 200);
            this.pnOrderInfo.Name = "pnOrderInfo";
            this.pnOrderInfo.Size = new System.Drawing.Size(583, 527);
            this.pnOrderInfo.TabIndex = 2;
            // 
            // pnOperInfo
            // 
            this.pnOperInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnOperInfo.Location = new System.Drawing.Point(0, 727);
            this.pnOperInfo.Name = "pnOperInfo";
            this.pnOperInfo.Size = new System.Drawing.Size(583, 100);
            this.pnOperInfo.TabIndex = 3;
            // 
            // ucRecipeBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnOrderInfo);
            this.Controls.Add(this.pnPatientInfo);
            this.Controls.Add(this.pnOperInfo);
            this.Controls.Add(this.pnTitle);
            this.Name = "ucRecipeBase";
            this.Size = new System.Drawing.Size(583, 827);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Panel pnPatientInfo;
        private System.Windows.Forms.Panel pnOrderInfo;
        private System.Windows.Forms.Panel pnOperInfo;
    }
}
