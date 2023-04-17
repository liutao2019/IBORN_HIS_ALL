namespace SOC.Fee.DayBalance.InpatientPrepay
{
    partial class ucPrepayIncomeAndQuitList
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucPrepayCompare1 = new SOC.Fee.DayBalance.InpatientPrepay.ucPrepayCompare();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucPrepayHistory1 = new SOC.Fee.DayBalance.InpatientPrepay.ucPrepayHistory();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1070, 456);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.ucPrepayCompare1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(178, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(892, 456);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // ucPrepayCompare1
            // 
            this.ucPrepayCompare1.BackColor = System.Drawing.Color.White;
            this.ucPrepayCompare1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrepayCompare1.IsPrint = false;
            this.ucPrepayCompare1.Location = new System.Drawing.Point(0, 0);
            this.ucPrepayCompare1.Name = "ucPrepayCompare1";
            this.ucPrepayCompare1.Size = new System.Drawing.Size(892, 456);
            this.ucPrepayCompare1.TabIndex = 0;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.ucPrepayHistory1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(178, 456);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 0;
            // 
            // ucPrepayHistory1
            // 
            this.ucPrepayHistory1.BackColor = System.Drawing.Color.White;
            this.ucPrepayHistory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrepayHistory1.IsPrint = false;
            this.ucPrepayHistory1.Location = new System.Drawing.Point(0, 0);
            this.ucPrepayHistory1.Name = "ucPrepayHistory1";
            this.ucPrepayHistory1.Size = new System.Drawing.Size(178, 456);
            this.ucPrepayHistory1.TabIndex = 0;
            // 
            // ucPrepayIncomeAndQuitList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPrepayIncomeAndQuitList";
            this.Size = new System.Drawing.Size(1070, 456);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private ucPrepayHistory ucPrepayHistory1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private ucPrepayCompare ucPrepayCompare1;
    }
}
