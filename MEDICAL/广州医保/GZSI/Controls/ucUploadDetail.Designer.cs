namespace GZSI.Controls
{
    partial class ucUploadDetail
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
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbDrug = new System.Windows.Forms.ProgressBar();
            this.gbDrug = new System.Windows.Forms.GroupBox();
            this.gbUndrug = new System.Windows.Forms.GroupBox();
            this.pbUndrug = new System.Windows.Forms.ProgressBar();
            this.gbDrug.SuspendLayout();
            this.gbUndrug.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbDrug
            // 
            this.pbDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDrug.Location = new System.Drawing.Point(3, 17);
            this.pbDrug.Name = "pbDrug";
            this.pbDrug.Size = new System.Drawing.Size(514, 28);
            this.pbDrug.TabIndex = 0;
            // 
            // gbDrug
            // 
            this.gbDrug.Controls.Add(this.pbDrug);
            this.gbDrug.Location = new System.Drawing.Point(8, 8);
            this.gbDrug.Name = "gbDrug";
            this.gbDrug.Size = new System.Drawing.Size(520, 48);
            this.gbDrug.TabIndex = 2;
            this.gbDrug.TabStop = false;
            // 
            // gbUndrug
            // 
            this.gbUndrug.Controls.Add(this.pbUndrug);
            this.gbUndrug.Location = new System.Drawing.Point(8, 72);
            this.gbUndrug.Name = "gbUndrug";
            this.gbUndrug.Size = new System.Drawing.Size(520, 48);
            this.gbUndrug.TabIndex = 3;
            this.gbUndrug.TabStop = false;
            // 
            // pbUndrug
            // 
            this.pbUndrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbUndrug.Location = new System.Drawing.Point(3, 17);
            this.pbUndrug.Name = "pbUndrug";
            this.pbUndrug.Size = new System.Drawing.Size(514, 28);
            this.pbUndrug.TabIndex = 0;
            // 
            // ucUploadDetail
            // 
            this.Controls.Add(this.gbUndrug);
            this.Controls.Add(this.gbDrug);
            this.Name = "ucUploadDetail";
            this.Size = new System.Drawing.Size(544, 136);
            this.gbDrug.ResumeLayout(false);
            this.gbUndrug.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
       
    }
}
