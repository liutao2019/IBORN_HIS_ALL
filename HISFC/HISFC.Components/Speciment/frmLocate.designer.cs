namespace FS.HISFC.Components.Speciment
{
    partial class frmLocate
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
            this.tvLocate = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvLocate
            // 
            this.tvLocate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLocate.Location = new System.Drawing.Point(0, 0);
            this.tvLocate.Name = "tvLocate";
            this.tvLocate.Size = new System.Drawing.Size(256, 489);
            this.tvLocate.TabIndex = 0;
            this.tvLocate.DoubleClick += new System.EventHandler(this.tvLocate_DoubleClick);
            // 
            // frmLocate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(256, 489);
            this.Controls.Add(this.tvLocate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLocate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "冰箱浏览";
            this.Load += new System.EventHandler(this.frmLocate_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvLocate;
    }
}