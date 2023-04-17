namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    partial class ucStencilManager
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
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPriveDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanelMain.SuspendLayout();
            this.ngbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelLeft
            // 
            this.neuPanelLeft.Size = new System.Drawing.Size(296, 555);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(296, 0);
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Location = new System.Drawing.Point(299, 0);
            this.neuPanelMain.Size = new System.Drawing.Size(647, 555);
            // 
            // ngbInfo
            // 
            this.ngbInfo.Controls.Add(this.nlbPriveDept);
            this.ngbInfo.Controls.Add(this.nlbInfo);
            this.ngbInfo.Size = new System.Drawing.Size(647, 55);
            // 
            // neuPanelData
            // 
            this.neuPanelData.Size = new System.Drawing.Size(647, 500);
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo.Location = new System.Drawing.Point(275, 29);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(0, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 0;
            // 
            // nlbPriveDept
            // 
            this.nlbPriveDept.AutoSize = true;
            this.nlbPriveDept.ForeColor = System.Drawing.Color.Blue;
            this.nlbPriveDept.Location = new System.Drawing.Point(19, 29);
            this.nlbPriveDept.Name = "nlbPriveDept";
            this.nlbPriveDept.Size = new System.Drawing.Size(29, 12);
            this.nlbPriveDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPriveDept.TabIndex = 1;
            this.nlbPriveDept.Text = "提示";
            // 
            // ucStencilManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucStencilManager";
            this.Size = new System.Drawing.Size(946, 555);
            this.neuPanelMain.ResumeLayout(false);
            this.ngbInfo.ResumeLayout(false);
            this.ngbInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPriveDept;
    }
}
