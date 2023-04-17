namespace Neusoft.HISFC.Components.Operation
{
    partial class ucArrangement
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
            this.neuPanel2 = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel1 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePicker1 = new Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuSplitter1 = new Neusoft.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel1 = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.ucArrangementSpread1 = new Neusoft.HISFC.Components.Operation.ucArrangementSpread();
            this.ucArrangementInfo1 = new Neusoft.HISFC.Components.Operation.ucArrangementInfo();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.ucArrangementInfo1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(369, 3);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(833, 174);
            this.neuPanel2.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuDateTimePicker1);
            this.neuPanel3.Controls.Add(this.neuLabel1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel3.Location = new System.Drawing.Point(0, 3);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(369, 174);
            this.neuPanel3.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 4;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(3, 6);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "设定时间";
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(5, 21);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(118, 21);
            this.neuDateTimePicker1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 1;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(1202, 3);
            this.neuSplitter1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 5;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuSplitter1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1202, 177);
            this.neuPanel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ucArrangementSpread1
            // 
            this.ucArrangementSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucArrangementSpread1.Filter = Neusoft.HISFC.Components.Operation.ucArrangementSpread.EnumFilter.All;
            this.ucArrangementSpread1.Location = new System.Drawing.Point(0, 177);
            this.ucArrangementSpread1.Name = "ucArrangementSpread1";
            this.ucArrangementSpread1.Size = new System.Drawing.Size(1202, 426);
            this.ucArrangementSpread1.TabIndex = 3;
            this.ucArrangementSpread1.applictionSelected += new Neusoft.HISFC.Components.Operation.ApplicationSelectedEventHandler(this.ucArrangementSpread1_applictionSelected);
            // 
            // ucArrangementInfo1
            // 
            this.ucArrangementInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucArrangementInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucArrangementInfo1.Name = "ucArrangementInfo1";
            this.ucArrangementInfo1.Size = new System.Drawing.Size(833, 174);
            this.ucArrangementInfo1.TabIndex = 2;
            // 
            // ucArrangement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucArrangementSpread1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucArrangement";
            this.Size = new System.Drawing.Size(1202, 603);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucArrangementInfo ucArrangementInfo1;
        private Neusoft.HISFC.Components.Operation.ucArrangementSpread ucArrangementSpread1;
        private Neusoft.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private Neusoft.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private Neusoft.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
    }
}
