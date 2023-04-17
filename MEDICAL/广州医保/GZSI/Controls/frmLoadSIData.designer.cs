namespace GZSI.Controls
{
    partial class frmLoadSIData
    {
        /// <summary>
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbTimer;
        private System.Windows.Forms.ProgressBar progressBar1;
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
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbTimer = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnAddCompare = new System.Windows.Forms.Button();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnLoadYD = new System.Windows.Forms.Button();
            this.btnAddCompareYD = new System.Windows.Forms.Button();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // lbInfo
            // 
            this.lbInfo.Location = new System.Drawing.Point(24, 8);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(496, 24);
            this.lbInfo.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(521, 97);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "项目自动对照(&S)";
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(427, 97);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbTimer
            // 
            this.lbTimer.Location = new System.Drawing.Point(16, 72);
            this.lbTimer.Name = "lbTimer";
            this.lbTimer.Size = new System.Drawing.Size(100, 23);
            this.lbTimer.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(674, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(239, 97);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(88, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "医保项目下载(&N)";
            this.btnLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAddCompare
            // 
            this.btnAddCompare.Location = new System.Drawing.Point(333, 97);
            this.btnAddCompare.Name = "btnAddCompare";
            this.btnAddCompare.Size = new System.Drawing.Size(88, 23);
            this.btnAddCompare.TabIndex = 6;
            this.btnAddCompare.Text = "项目增量对照(&S)";
            this.btnAddCompare.Click += new System.EventHandler(this.btnAddCompare_Click);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(146, 100);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(72, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "广州医保";
            // 
            // btnLoadYD
            // 
            this.btnLoadYD.Enabled = false;
            this.btnLoadYD.Location = new System.Drawing.Point(239, 126);
            this.btnLoadYD.Name = "btnLoadYD";
            this.btnLoadYD.Size = new System.Drawing.Size(88, 23);
            this.btnLoadYD.TabIndex = 8;
            this.btnLoadYD.Text = "医保项目下载(&N)";
            this.btnLoadYD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadYD.Visible = false;
            // 
            // btnAddCompareYD
            // 
            this.btnAddCompareYD.Enabled = false;
            this.btnAddCompareYD.Location = new System.Drawing.Point(333, 126);
            this.btnAddCompareYD.Name = "btnAddCompareYD";
            this.btnAddCompareYD.Size = new System.Drawing.Size(88, 23);
            this.btnAddCompareYD.TabIndex = 9;
            this.btnAddCompareYD.Text = "项目增量对照(&S)";
            this.btnAddCompareYD.Visible = false;
            this.btnAddCompareYD.Click += new System.EventHandler(this.btnAddCompareYD_Click);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(146, 129);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(72, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 10;
            this.neuLabel2.Text = "异地医保";
            this.neuLabel2.Visible = false;
            // 
            // frmLoadSIData
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(724, 150);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.btnAddCompareYD);
            this.Controls.Add(this.btnLoadYD);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.btnAddCompare);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lbTimer);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.progressBar1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoadSIData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "同步数据";
            this.Load += new System.EventHandler(this.frmSameData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAddCompare;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.Button btnLoadYD;
        private System.Windows.Forms.Button btnAddCompareYD;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
    }
}