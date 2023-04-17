namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    partial class frmPrintMetCasBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintMetCasBase));
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnPreviewFirst = new System.Windows.Forms.Button();
            this.btnPreviewSecond = new System.Windows.Forms.Button();
            this.btnPrintFirst = new System.Windows.Forms.Button();
            this.btnPrintSecond = new System.Windows.Forms.Button();
            this.btPrintThird = new System.Windows.Forms.Button();
            this.btnPreviewThird = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btnPreviewFirst
            // 
            this.btnPreviewFirst.Location = new System.Drawing.Point(71, 19);
            this.btnPreviewFirst.Name = "btnPreviewFirst";
            this.btnPreviewFirst.Size = new System.Drawing.Size(100, 30);
            this.btnPreviewFirst.TabIndex = 0;
            this.btnPreviewFirst.Text = "预览第一页";
            this.btnPreviewFirst.UseVisualStyleBackColor = true;
            this.btnPreviewFirst.Click += new System.EventHandler(this.btnPreviewFirst_Click);
            // 
            // btnPreviewSecond
            // 
            this.btnPreviewSecond.Location = new System.Drawing.Point(71, 53);
            this.btnPreviewSecond.Name = "btnPreviewSecond";
            this.btnPreviewSecond.Size = new System.Drawing.Size(100, 30);
            this.btnPreviewSecond.TabIndex = 1;
            this.btnPreviewSecond.Text = "预览第二页";
            this.btnPreviewSecond.UseVisualStyleBackColor = true;
            this.btnPreviewSecond.Click += new System.EventHandler(this.btnPreviewSecond_Click);
            // 
            // btnPrintFirst
            // 
            this.btnPrintFirst.Location = new System.Drawing.Point(71, 144);
            this.btnPrintFirst.Name = "btnPrintFirst";
            this.btnPrintFirst.Size = new System.Drawing.Size(100, 30);
            this.btnPrintFirst.TabIndex = 2;
            this.btnPrintFirst.Text = "打印第一页";
            this.btnPrintFirst.UseVisualStyleBackColor = true;
            this.btnPrintFirst.Click += new System.EventHandler(this.btnPrintFirst_Click);
            // 
            // btnPrintSecond
            // 
            this.btnPrintSecond.Location = new System.Drawing.Point(71, 178);
            this.btnPrintSecond.Name = "btnPrintSecond";
            this.btnPrintSecond.Size = new System.Drawing.Size(100, 30);
            this.btnPrintSecond.TabIndex = 3;
            this.btnPrintSecond.Text = "打印第二页";
            this.btnPrintSecond.UseVisualStyleBackColor = true;
            this.btnPrintSecond.Click += new System.EventHandler(this.btnPrintSecond_Click);
            // 
            // btPrintThird
            // 
            this.btPrintThird.Location = new System.Drawing.Point(71, 212);
            this.btPrintThird.Name = "btPrintThird";
            this.btPrintThird.Size = new System.Drawing.Size(100, 30);
            this.btPrintThird.TabIndex = 5;
            this.btPrintThird.Text = "打印第三页";
            this.btPrintThird.UseVisualStyleBackColor = true;
            this.btPrintThird.Click += new System.EventHandler(this.btPrintThird_Click);
            // 
            // btnPreviewThird
            // 
            this.btnPreviewThird.Location = new System.Drawing.Point(71, 87);
            this.btnPreviewThird.Name = "btnPreviewThird";
            this.btnPreviewThird.Size = new System.Drawing.Size(100, 30);
            this.btnPreviewThird.TabIndex = 4;
            this.btnPreviewThird.Text = "预览第三页";
            this.btnPreviewThird.UseVisualStyleBackColor = true;
            this.btnPreviewThird.Click += new System.EventHandler(this.btnPreviewThird_Click);
            // 
            // frmPrintMetCasBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 266);
            this.Controls.Add(this.btPrintThird);
            this.Controls.Add(this.btnPreviewThird);
            this.Controls.Add(this.btnPrintSecond);
            this.Controls.Add(this.btnPrintFirst);
            this.Controls.Add(this.btnPreviewSecond);
            this.Controls.Add(this.btnPreviewFirst);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(250, 300);
            this.Name = "frmPrintMetCasBase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印选项";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button btnPreviewFirst;
        private System.Windows.Forms.Button btnPreviewSecond;
        private System.Windows.Forms.Button btnPrintFirst;
        private System.Windows.Forms.Button btnPrintSecond;
        private System.Windows.Forms.Button btPrintThird;
        private System.Windows.Forms.Button btnPreviewThird;

    }
}