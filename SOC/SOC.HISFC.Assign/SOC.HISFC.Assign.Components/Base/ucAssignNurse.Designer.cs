namespace FS.SOC.HISFC.Assign.Components.Base
{
    partial class ucAssignNurse
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
            this.gbNurseStation = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lvNurseStation = new FS.FrameWork.WinForms.Controls.NeuListView();
            this.gbNurseStation.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbNurseStation
            // 
            this.gbNurseStation.Controls.Add(this.lvNurseStation);
            this.gbNurseStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbNurseStation.ForeColor = System.Drawing.Color.Blue;
            this.gbNurseStation.Location = new System.Drawing.Point(0, 0);
            this.gbNurseStation.Name = "gbNurseStation";
            this.gbNurseStation.Size = new System.Drawing.Size(258, 417);
            this.gbNurseStation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbNurseStation.TabIndex = 0;
            this.gbNurseStation.TabStop = false;
            this.gbNurseStation.Text = "分诊护士站及科室";
            // 
            // lvNurseStation
            // 
            this.lvNurseStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNurseStation.HideSelection = false;
            this.lvNurseStation.Location = new System.Drawing.Point(3, 17);
            this.lvNurseStation.Name = "lvNurseStation";
            this.lvNurseStation.Size = new System.Drawing.Size(252, 397);
            this.lvNurseStation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lvNurseStation.TabIndex = 0;
            this.lvNurseStation.UseCompatibleStateImageBehavior = false;
            // 
            // ucAssignNurse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbNurseStation);
            this.Name = "ucAssignNurse";
            this.Size = new System.Drawing.Size(258, 417);
            this.gbNurseStation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbNurseStation;
        private FS.FrameWork.WinForms.Controls.NeuListView lvNurseStation;
    }
}
