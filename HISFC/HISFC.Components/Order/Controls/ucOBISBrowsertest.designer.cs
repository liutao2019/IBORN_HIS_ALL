namespace FS.HISFC.Components.Order.Controls
{
    partial class ucOBISBrowsertest
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.labtxt = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(662, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(98, 25);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "跳转";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // labtxt
            // 
            this.labtxt.AutoSize = true;
            this.labtxt.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labtxt.Location = new System.Drawing.Point(30, 56);
            this.labtxt.Name = "labtxt";
            this.labtxt.Size = new System.Drawing.Size(503, 19);
            this.labtxt.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labtxt.TabIndex = 2;
            this.labtxt.Text = "新版孕产无法在系统里打开，请点击跳转按钮切换到浏览器";
            // 
            // ucOBISBrowsertest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labtxt);
            this.Controls.Add(this.btnRefresh);
            this.Name = "ucOBISBrowsertest";
            this.Size = new System.Drawing.Size(773, 375);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private FS.FrameWork.WinForms.Controls.NeuLabel labtxt;


    }
}
