namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{
    partial class ucTriaggingPatient
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
            this.components = new System.ComponentModel.Container();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tvTriaggingPatient = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.tvTriaggingPatient);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.ForeColor = System.Drawing.Color.Blue;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(230, 461);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 1;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "待分诊患者";
            // 
            // tvTriaggingPatient
            // 
            this.tvTriaggingPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTriaggingPatient.HideSelection = false;
            this.tvTriaggingPatient.Location = new System.Drawing.Point(3, 17);
            this.tvTriaggingPatient.Name = "tvTriaggingPatient";
            this.tvTriaggingPatient.Size = new System.Drawing.Size(224, 441);
            this.tvTriaggingPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvTriaggingPatient.TabIndex = 0;
            // 
            // ucTriaggingPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucTriaggingPatient";
            this.Size = new System.Drawing.Size(230, 461);
            this.neuGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.HISFC.Components.Common.Controls.baseTreeView tvTriaggingPatient;

    }
}
