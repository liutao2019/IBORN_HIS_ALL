namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{
    partial class ucTriageQueue
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
            this.gbTriageQueue = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lvTriageQueue = new FS.FrameWork.WinForms.Controls.NeuListView();
            this.gbTriageQueue.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTriageQueue
            // 
            this.gbTriageQueue.Controls.Add(this.lvTriageQueue);
            this.gbTriageQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTriageQueue.ForeColor = System.Drawing.Color.Blue;
            this.gbTriageQueue.Location = new System.Drawing.Point(0, 0);
            this.gbTriageQueue.Name = "gbTriageQueue";
            this.gbTriageQueue.Size = new System.Drawing.Size(641, 230);
            this.gbTriageQueue.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTriageQueue.TabIndex = 0;
            this.gbTriageQueue.TabStop = false;
            this.gbTriageQueue.Text = "队列信息";
            // 
            // lvTriageQueue
            // 
            this.lvTriageQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTriageQueue.FullRowSelect = true;
            this.lvTriageQueue.GridLines = true;
            this.lvTriageQueue.HideSelection = false;
            this.lvTriageQueue.Location = new System.Drawing.Point(3, 17);
            this.lvTriageQueue.MultiSelect = false;
            this.lvTriageQueue.Name = "lvTriageQueue";
            this.lvTriageQueue.ShowItemToolTips = true;
            this.lvTriageQueue.Size = new System.Drawing.Size(635, 210);
            this.lvTriageQueue.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lvTriageQueue.TabIndex = 1;
            this.lvTriageQueue.UseCompatibleStateImageBehavior = false;
            // 
            // ucTriageQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTriageQueue);
            this.Name = "ucTriageQueue";
            this.Size = new System.Drawing.Size(641, 230);
            this.gbTriageQueue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTriageQueue;
        private FS.FrameWork.WinForms.Controls.NeuListView lvTriageQueue;

    }
}
