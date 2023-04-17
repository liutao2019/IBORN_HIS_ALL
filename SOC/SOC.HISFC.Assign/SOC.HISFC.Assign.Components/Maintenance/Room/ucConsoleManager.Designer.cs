namespace FS.SOC.HISFC.Assign.Components.Maintenance.Room
{
    partial class ucConsoleManager
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
            this.gbAssignConsole = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lvAssignConsole = new FS.FrameWork.WinForms.Controls.NeuListView();
            this.gbAssignConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAssignConsole
            // 
            this.gbAssignConsole.Controls.Add(this.lvAssignConsole);
            this.gbAssignConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAssignConsole.ForeColor = System.Drawing.Color.Blue;
            this.gbAssignConsole.Location = new System.Drawing.Point(0, 0);
            this.gbAssignConsole.Name = "gbAssignConsole";
            this.gbAssignConsole.Size = new System.Drawing.Size(150, 150);
            this.gbAssignConsole.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbAssignConsole.TabIndex = 2;
            this.gbAssignConsole.TabStop = false;
            this.gbAssignConsole.Text = "分诊诊台";
            // 
            // lvAssignConsole
            // 
            this.lvAssignConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAssignConsole.HideSelection = false;
            this.lvAssignConsole.Location = new System.Drawing.Point(3, 17);
            this.lvAssignConsole.MultiSelect = false;
            this.lvAssignConsole.Name = "lvAssignConsole";
            this.lvAssignConsole.Size = new System.Drawing.Size(144, 130);
            this.lvAssignConsole.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lvAssignConsole.TabIndex = 0;
            this.lvAssignConsole.UseCompatibleStateImageBehavior = false;
            // 
            // ucConsoleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbAssignConsole);
            this.Name = "ucConsoleManager";
            this.gbAssignConsole.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbAssignConsole;
        private FS.FrameWork.WinForms.Controls.NeuListView lvAssignConsole;
    }
}
