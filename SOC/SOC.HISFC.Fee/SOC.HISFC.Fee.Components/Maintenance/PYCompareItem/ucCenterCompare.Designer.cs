namespace FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem
{
    partial class ucCenterCompare
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
            this.pnlCenterPact = new System.Windows.Forms.Panel();
            this.pnlCompareForCT = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlCenterPact
            // 
            this.pnlCenterPact.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCenterPact.Location = new System.Drawing.Point(0, 0);
            this.pnlCenterPact.Name = "pnlCenterPact";
            this.pnlCenterPact.Size = new System.Drawing.Size(627, 235);
            this.pnlCenterPact.TabIndex = 0;
            this.pnlCenterPact.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCenterPact_Paint);
            // 
            // pnlCompareForCT
            // 
            this.pnlCompareForCT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCompareForCT.Location = new System.Drawing.Point(0, 235);
            this.pnlCompareForCT.Name = "pnlCompareForCT";
            this.pnlCompareForCT.Size = new System.Drawing.Size(627, 78);
            this.pnlCompareForCT.TabIndex = 1;
            // 
            // ucCenterCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCompareForCT);
            this.Controls.Add(this.pnlCenterPact);
            this.Name = "ucCenterCompare";
            this.Size = new System.Drawing.Size(627, 313);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCenterPact;
        private System.Windows.Forms.Panel pnlCompareForCT;
    }
}
