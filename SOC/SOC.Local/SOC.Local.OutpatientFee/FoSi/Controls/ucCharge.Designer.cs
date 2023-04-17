namespace FS.SOC.Local.OutpatientFee.FoSi.Controls
{
    partial class ucCharge
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

            if (disposing)
            {
                if (this.fPopWin != null)
                {
                    this.fPopWin.Dispose();
                    this.fPopWin = null;
                }

                if (ucShow != null)
                {
                    ucShow.Dispose();
                    ucShow = null;
                }

                if (comFeeItemLists != null)
                {
                    comFeeItemLists.Clear();
                    comFeeItemLists = null;
                }

                if (hsToolBar != null)
                {
                    hsToolBar.Clear();
                    hsToolBar = null;
                }
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
            this.plTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.plLeft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plTop
            // 
            this.plTop.BackColor = System.Drawing.SystemColors.Control;
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(836, 135);
            this.plTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTop.TabIndex = 0;
            // 
            // plBottom
            // 
            this.plBottom.BackColor = System.Drawing.SystemColors.Control;
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Location = new System.Drawing.Point(0, 583);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(836, 167);
            this.plBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottom.TabIndex = 1;
            // 
            // plMain
            // 
            this.plMain.BackColor = System.Drawing.SystemColors.Control;
            this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMain.Location = new System.Drawing.Point(0, 135);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(836, 448);
            this.plMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plMain.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.plLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.plMain);
            this.splitContainer1.Panel2.Controls.Add(this.plTop);
            this.splitContainer1.Panel2.Controls.Add(this.plBottom);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 750);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 4;
            // 
            // plLeft
            // 
            this.plLeft.BackColor = System.Drawing.SystemColors.Control;
            this.plLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plLeft.Location = new System.Drawing.Point(0, 0);
            this.plLeft.Name = "plLeft";
            this.plLeft.Size = new System.Drawing.Size(160, 750);
            this.plLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plLeft.TabIndex = 0;
            // 
            // ucCharge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucCharge";
            this.Size = new System.Drawing.Size(1000, 750);
            this.Load += new System.EventHandler(this.ucCharge_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel plTop;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottom;
        private FS.FrameWork.WinForms.Controls.NeuPanel plMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plLeft;
    }
}
