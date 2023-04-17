namespace FS.SOC.Local.Assign.ZhuHai.ZDWY.IAssignDisplay
{
    partial class frmDisplay1
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
            this.components = new System.ComponentModel.Container();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timerCall = new System.Windows.Forms.Timer(this.components);
            this.pnlFlash = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblWait = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDelay = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnltop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlTRight = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlctrl5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlTMiddle3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlctrl4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlTMiddle2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlctrl3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlTMiddle = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlctrl2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlTleft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlctrl1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlFlash.SuspendLayout();
            this.pnltop.SuspendLayout();
            this.pnlTRight.SuspendLayout();
            this.pnlTMiddle3.SuspendLayout();
            this.pnlTMiddle2.SuspendLayout();
            this.pnlTMiddle.SuspendLayout();
            this.pnlTleft.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 10000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timerCall
            // 
            this.timerCall.Tick += new System.EventHandler(this.timerCall_Tick);
            // 
            // pnlFlash
            // 
            this.pnlFlash.BackColor = System.Drawing.Color.Black;
            this.pnlFlash.Controls.Add(this.lblWait);
            this.pnlFlash.Controls.Add(this.lblDelay);
            this.pnlFlash.Controls.Add(this.lblTitle);
            this.pnlFlash.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFlash.Location = new System.Drawing.Point(0, 0);
            this.pnlFlash.Name = "pnlFlash";
            this.pnlFlash.Size = new System.Drawing.Size(759, 73);
            this.pnlFlash.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlFlash.TabIndex = 2;
            // 
            // lblWait
            // 
            this.lblWait.AutoSize = true;
            this.lblWait.BackColor = System.Drawing.Color.Black;
            this.lblWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWait.Font = new System.Drawing.Font("宋体", 10F);
            this.lblWait.ForeColor = System.Drawing.Color.Lime;
            this.lblWait.Location = new System.Drawing.Point(0, 0);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(77, 14);
            this.lblWait.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblWait.TabIndex = 2;
            this.lblWait.Text = "进诊患者：";
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.BackColor = System.Drawing.Color.Black;
            this.lblDelay.Font = new System.Drawing.Font("宋体", 10F);
            this.lblDelay.ForeColor = System.Drawing.Color.Lime;
            this.lblDelay.Location = new System.Drawing.Point(86, 53);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(77, 14);
            this.lblDelay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDelay.TabIndex = 1;
            this.lblDelay.Text = "延迟患者：";
            this.lblDelay.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Black;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTitle.ForeColor = System.Drawing.Color.Lime;
            this.lblTitle.Location = new System.Drawing.Point(3, 53);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(77, 14);
            this.lblTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "祝您健康！";
            this.lblTitle.Visible = false;
            // 
            // pnltop
            // 
            this.pnltop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.pnltop.Controls.Add(this.pnlTRight);
            this.pnltop.Controls.Add(this.pnlTMiddle3);
            this.pnltop.Controls.Add(this.pnlTMiddle2);
            this.pnltop.Controls.Add(this.pnlTMiddle);
            this.pnltop.Controls.Add(this.pnlTleft);
            this.pnltop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltop.Location = new System.Drawing.Point(0, 73);
            this.pnltop.Name = "pnltop";
            this.pnltop.Size = new System.Drawing.Size(759, 223);
            this.pnltop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnltop.TabIndex = 3;
            // 
            // pnlTRight
            // 
            this.pnlTRight.BackColor = System.Drawing.Color.White;
            this.pnlTRight.Controls.Add(this.pnlctrl5);
            this.pnlTRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTRight.Location = new System.Drawing.Point(635, 0);
            this.pnlTRight.Name = "pnlTRight";
            this.pnlTRight.Size = new System.Drawing.Size(124, 223);
            this.pnlTRight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTRight.TabIndex = 2;
            // 
            // pnlctrl5
            // 
            this.pnlctrl5.BackColor = System.Drawing.Color.Black;
            this.pnlctrl5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlctrl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlctrl5.Location = new System.Drawing.Point(0, 0);
            this.pnlctrl5.Name = "pnlctrl5";
            this.pnlctrl5.Size = new System.Drawing.Size(124, 223);
            this.pnlctrl5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlctrl5.TabIndex = 5;
            // 
            // pnlTMiddle3
            // 
            this.pnlTMiddle3.Controls.Add(this.pnlctrl4);
            this.pnlTMiddle3.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTMiddle3.Location = new System.Drawing.Point(478, 0);
            this.pnlTMiddle3.Name = "pnlTMiddle3";
            this.pnlTMiddle3.Size = new System.Drawing.Size(157, 223);
            this.pnlTMiddle3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTMiddle3.TabIndex = 0;
            // 
            // pnlctrl4
            // 
            this.pnlctrl4.BackColor = System.Drawing.Color.Black;
            this.pnlctrl4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlctrl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlctrl4.Location = new System.Drawing.Point(0, 0);
            this.pnlctrl4.Name = "pnlctrl4";
            this.pnlctrl4.Size = new System.Drawing.Size(157, 223);
            this.pnlctrl4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlctrl4.TabIndex = 6;
            // 
            // pnlTMiddle2
            // 
            this.pnlTMiddle2.BackColor = System.Drawing.Color.White;
            this.pnlTMiddle2.Controls.Add(this.pnlctrl3);
            this.pnlTMiddle2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTMiddle2.Location = new System.Drawing.Point(341, 0);
            this.pnlTMiddle2.Name = "pnlTMiddle2";
            this.pnlTMiddle2.Size = new System.Drawing.Size(137, 223);
            this.pnlTMiddle2.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.pnlTMiddle2.TabIndex = 1;
            // 
            // pnlctrl3
            // 
            this.pnlctrl3.BackColor = System.Drawing.Color.Black;
            this.pnlctrl3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlctrl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlctrl3.Location = new System.Drawing.Point(0, 0);
            this.pnlctrl3.Name = "pnlctrl3";
            this.pnlctrl3.Size = new System.Drawing.Size(137, 223);
            this.pnlctrl3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlctrl3.TabIndex = 7;
            // 
            // pnlTMiddle
            // 
            this.pnlTMiddle.BackColor = System.Drawing.Color.White;
            this.pnlTMiddle.Controls.Add(this.pnlctrl2);
            this.pnlTMiddle.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTMiddle.Location = new System.Drawing.Point(183, 0);
            this.pnlTMiddle.Name = "pnlTMiddle";
            this.pnlTMiddle.Size = new System.Drawing.Size(158, 223);
            this.pnlTMiddle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTMiddle.TabIndex = 1;
            // 
            // pnlctrl2
            // 
            this.pnlctrl2.BackColor = System.Drawing.Color.Black;
            this.pnlctrl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlctrl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlctrl2.Location = new System.Drawing.Point(0, 0);
            this.pnlctrl2.Name = "pnlctrl2";
            this.pnlctrl2.Size = new System.Drawing.Size(158, 223);
            this.pnlctrl2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlctrl2.TabIndex = 5;
            // 
            // pnlTleft
            // 
            this.pnlTleft.BackColor = System.Drawing.Color.White;
            this.pnlTleft.Controls.Add(this.pnlctrl1);
            this.pnlTleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTleft.Location = new System.Drawing.Point(0, 0);
            this.pnlTleft.Name = "pnlTleft";
            this.pnlTleft.Size = new System.Drawing.Size(183, 223);
            this.pnlTleft.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.pnlTleft.TabIndex = 0;
            // 
            // pnlctrl1
            // 
            this.pnlctrl1.BackColor = System.Drawing.Color.Black;
            this.pnlctrl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlctrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlctrl1.Location = new System.Drawing.Point(0, 0);
            this.pnlctrl1.Name = "pnlctrl1";
            this.pnlctrl1.Size = new System.Drawing.Size(183, 223);
            this.pnlctrl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlctrl1.TabIndex = 4;
            // 
            // frmDisplay1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(759, 447);
            this.Controls.Add(this.pnltop);
            this.Controls.Add(this.pnlFlash);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDisplay1";
            this.Text = "frmDisplay";
            this.Load += new System.EventHandler(this.frmDisplay_Load);
            this.DoubleClick += new System.EventHandler(this.frmDisplay_DoubleClick);
            this.pnlFlash.ResumeLayout(false);
            this.pnlFlash.PerformLayout();
            this.pnltop.ResumeLayout(false);
            this.pnlTRight.ResumeLayout(false);
            this.pnlTMiddle3.ResumeLayout(false);
            this.pnlTMiddle2.ResumeLayout(false);
            this.pnlTMiddle.ResumeLayout(false);
            this.pnlTleft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Timer timerCall;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlFlash;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDelay;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblWait;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnltop;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTRight;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlctrl5;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTMiddle3;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlctrl4;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTMiddle2;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlctrl3;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTMiddle;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlctrl2;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTleft;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlctrl1;
    }
}