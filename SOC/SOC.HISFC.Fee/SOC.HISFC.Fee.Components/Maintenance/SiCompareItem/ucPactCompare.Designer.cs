namespace FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem
{
    partial class ucPactCompare
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
            this.pnlComparedItem = new System.Windows.Forms.Panel();
            this.pnlCompareForP = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlComparedItem
            // 
            this.pnlComparedItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlComparedItem.Location = new System.Drawing.Point(0, 0);
            this.pnlComparedItem.Name = "pnlComparedItem";
            this.pnlComparedItem.Size = new System.Drawing.Size(747, 319);
            this.pnlComparedItem.TabIndex = 0;
            // 
            // pnlCompareForP
            // 
            this.pnlCompareForP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCompareForP.Location = new System.Drawing.Point(0, 171);
            this.pnlCompareForP.Name = "pnlCompareForP";
            this.pnlCompareForP.Size = new System.Drawing.Size(747, 148);
            this.pnlCompareForP.TabIndex = 1;
            // 
            // ucPactCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCompareForP);
            this.Controls.Add(this.pnlComparedItem);
            this.Name = "ucPactCompare";
            this.Size = new System.Drawing.Size(747, 319);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlComparedItem;
        private System.Windows.Forms.Panel pnlCompareForP;
    }
}
