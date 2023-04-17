namespace FS.HISFC.Components.Operation.ElectronicDisplay
{
    partial class frmArrangeDisplayInWeb
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
            this.webArrangeBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webArrangeBrowser
            // 
            this.webArrangeBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webArrangeBrowser.Location = new System.Drawing.Point(0, 0);
            this.webArrangeBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webArrangeBrowser.Name = "webArrangeBrowser";
            this.webArrangeBrowser.Size = new System.Drawing.Size(1272, 734);
            this.webArrangeBrowser.TabIndex = 0;
            this.webArrangeBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webArrangeBrowser_DocumentCompleted);
            // 
            // frmArrangeDisplayInWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 734);
            this.Controls.Add(this.webArrangeBrowser);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArrangeDisplayInWeb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电子屏幕显示";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webArrangeBrowser;

    }
}