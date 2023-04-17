namespace Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Register
{
    partial class ucInpatient
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
            this.gbQuery = new Neusoft.FrameWork.WinForms.Controls.NeuGroupBox();
            this.gbPatientList = new Neusoft.FrameWork.WinForms.Controls.NeuGroupBox();
            this.SuspendLayout();
            // 
            // gbQuery
            // 
            this.gbQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbQuery.Location = new System.Drawing.Point(0, 0);
            this.gbQuery.Name = "gbQuery";
            this.gbQuery.Size = new System.Drawing.Size(596, 58);
            this.gbQuery.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbQuery.TabIndex = 2;
            this.gbQuery.TabStop = false;
            this.gbQuery.Text = "登记信息";
            // 
            // gbPatientList
            // 
            this.gbPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPatientList.Location = new System.Drawing.Point(0, 58);
            this.gbPatientList.Name = "gbPatientList";
            this.gbPatientList.Size = new System.Drawing.Size(596, 398);
            this.gbPatientList.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientList.TabIndex = 3;
            this.gbPatientList.TabStop = false;
            this.gbPatientList.Text = "已入院患者信息";
            // 
            // ucRegisterMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbPatientList);
            this.Controls.Add(this.gbQuery);
            this.Name = "ucRegisterMain";
            this.Size = new System.Drawing.Size(596, 456);
            this.ResumeLayout(false);

        }

        #endregion

        private Neusoft.FrameWork.WinForms.Controls.NeuGroupBox gbQuery;
        private Neusoft.FrameWork.WinForms.Controls.NeuGroupBox gbPatientList;

    }
}
