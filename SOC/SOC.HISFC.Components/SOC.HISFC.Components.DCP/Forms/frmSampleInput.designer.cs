namespace FS.SOC.HISFC.Components.DCP
{
    partial class frmSampleInput
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox rtInput;
        private System.Windows.Forms.Button bttOk;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button bttCancel; 

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
            this.rtInput = new System.Windows.Forms.RichTextBox();
            this.bttOk = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.bttCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtInput
            // 
            this.rtInput.Location = new System.Drawing.Point(0, 24);
            this.rtInput.Name = "rtInput";
            this.rtInput.Size = new System.Drawing.Size(368, 128);
            this.rtInput.TabIndex = 0;
            this.rtInput.Text = "";
            // 
            // bttOk
            // 
            this.bttOk.Location = new System.Drawing.Point(147, 160);
            this.bttOk.Name = "bttOk";
            this.bttOk.Size = new System.Drawing.Size(75, 23);
            this.bttOk.TabIndex = 1;
            this.bttOk.Text = "确定";
            this.bttOk.Click += new System.EventHandler(this.bttOk_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.ForeColor = System.Drawing.Color.Blue;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(368, 23);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // bttCancel
            // 
            this.bttCancel.Location = new System.Drawing.Point(240, 160);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(75, 23);
            this.bttCancel.TabIndex = 3;
            this.bttCancel.Text = "返回";
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            // 
            // frmSampleInput
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(368, 197);
            this.ControlBox = false;
            this.Controls.Add(this.bttCancel);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.bttOk);
            this.Controls.Add(this.rtInput);
            this.Name = "frmSampleInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<<录入>>";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmCaseInput_Closing);
            this.ResumeLayout(false);

        }

        #endregion
    }
}