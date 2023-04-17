namespace SOC.Local.Assign.ShenZhen.BinHai.ITriaggedPatient
{
    partial class ucTriaggedPatient
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
            this.gbTriageWaiting = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tvTriageWaiting = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.gbTriageWaiting.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTriageWaiting
            // 
            this.gbTriageWaiting.Controls.Add(this.tvTriageWaiting);
            this.gbTriageWaiting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTriageWaiting.ForeColor = System.Drawing.Color.Blue;
            this.gbTriageWaiting.Location = new System.Drawing.Point(0, 0);
            this.gbTriageWaiting.Name = "gbTriageWaiting";
            this.gbTriageWaiting.Size = new System.Drawing.Size(209, 236);
            this.gbTriageWaiting.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTriageWaiting.TabIndex = 1;
            this.gbTriageWaiting.TabStop = false;
            this.gbTriageWaiting.Text = "刘德华-内科门诊-候诊：10";
            // 
            // tvTriageWaiting
            // 
            this.tvTriageWaiting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTriageWaiting.HideSelection = false;
            this.tvTriageWaiting.Location = new System.Drawing.Point(3, 17);
            this.tvTriageWaiting.Name = "tvTriageWaiting";
            this.tvTriageWaiting.Size = new System.Drawing.Size(203, 216);
            this.tvTriageWaiting.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvTriageWaiting.TabIndex = 1;
            // 
            // ucTriageWaitting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTriageWaiting);
            this.Name = "ucTriageWaitting";
            this.Size = new System.Drawing.Size(209, 236);
            this.gbTriageWaiting.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTriageWaiting;
        private FS.HISFC.Components.Common.Controls.baseTreeView tvTriageWaiting;
    }
}
