namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    partial class ucBaseControl
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
            this.neuPanelLeft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanelData = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ngbInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ngbLeftInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanelLeftChoose = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanelLeft.SuspendLayout();
            this.neuPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelLeft
            // 
            this.neuPanelLeft.Controls.Add(this.neuPanelLeftChoose);
            this.neuPanelLeft.Controls.Add(this.ngbLeftInfo);
            this.neuPanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.neuPanelLeft.Name = "neuPanelLeft";
            this.neuPanelLeft.Size = new System.Drawing.Size(267, 555);
            this.neuPanelLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelLeft.TabIndex = 0;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(267, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 555);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Controls.Add(this.neuPanelData);
            this.neuPanelMain.Controls.Add(this.ngbInfo);
            this.neuPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelMain.Location = new System.Drawing.Point(270, 0);
            this.neuPanelMain.Name = "neuPanelMain";
            this.neuPanelMain.Size = new System.Drawing.Size(542, 555);
            this.neuPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelMain.TabIndex = 2;
            // 
            // neuPanelData
            // 
            this.neuPanelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelData.Location = new System.Drawing.Point(0, 0);
            this.neuPanelData.Name = "neuPanelData";
            this.neuPanelData.Size = new System.Drawing.Size(542, 500);
            this.neuPanelData.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelData.TabIndex = 1;
            // 
            // ngbInfo
            // 
            this.ngbInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ngbInfo.Location = new System.Drawing.Point(0, 500);
            this.ngbInfo.Name = "ngbInfo";
            this.ngbInfo.Size = new System.Drawing.Size(542, 55);
            this.ngbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbInfo.TabIndex = 0;
            this.ngbInfo.TabStop = false;
            this.ngbInfo.Text = "附加信息";
            // 
            // ngbLeftInfo
            // 
            this.ngbLeftInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ngbLeftInfo.Location = new System.Drawing.Point(0, 500);
            this.ngbLeftInfo.Name = "ngbLeftInfo";
            this.ngbLeftInfo.Size = new System.Drawing.Size(267, 55);
            this.ngbLeftInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbLeftInfo.TabIndex = 1;
            this.ngbLeftInfo.TabStop = false;
            this.ngbLeftInfo.Text = "附加信息";
            // 
            // neuPanelLeftChoose
            // 
            this.neuPanelLeftChoose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelLeftChoose.Location = new System.Drawing.Point(0, 0);
            this.neuPanelLeftChoose.Name = "neuPanelLeftChoose";
            this.neuPanelLeftChoose.Size = new System.Drawing.Size(267, 500);
            this.neuPanelLeftChoose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelLeftChoose.TabIndex = 2;
            // 
            // ucBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanelMain);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanelLeft);
            this.Name = "ucBaseControl";
            this.Size = new System.Drawing.Size(812, 555);
            this.neuPanelLeft.ResumeLayout(false);
            this.neuPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanelLeft;
        protected FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanelMain;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbInfo;
        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanelData;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbLeftInfo;
        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanelLeftChoose;
    }
}
