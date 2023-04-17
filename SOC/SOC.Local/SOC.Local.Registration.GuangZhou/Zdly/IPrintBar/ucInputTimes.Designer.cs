namespace FS.SOC.Local.Registration.GuangZhou.Zdly.IPrintBar
{
    partial class ucInputTimes
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
            this.tbInjec = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbOk = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // tbInjec
            // 
            this.tbInjec.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbInjec.IsEnter2Tab = false;
            this.tbInjec.Location = new System.Drawing.Point(106, 10);
            this.tbInjec.Name = "tbInjec";
            this.tbInjec.Size = new System.Drawing.Size(156, 22);
            this.tbInjec.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInjec.TabIndex = 3;
            this.tbInjec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInjec_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 12F);
            this.neuLabel1.Location = new System.Drawing.Point(8, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(96, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "请输入次数:";
            // 
            // tbOk
            // 
            this.tbOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbOk.Location = new System.Drawing.Point(187, 41);
            this.tbOk.Name = "tbOk";
            this.tbOk.Size = new System.Drawing.Size(75, 23);
            this.tbOk.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOk.TabIndex = 5;
            this.tbOk.Text = "确定(&O)";
            this.tbOk.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.tbOk.UseVisualStyleBackColor = true;
            this.tbOk.Click += new System.EventHandler(this.tbOk_Click);
            // 
            // ucInputTimes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbOk);
            this.Controls.Add(this.tbInjec);
            this.Controls.Add(this.neuLabel1);
            this.Name = "ucInputTimes";
            this.Size = new System.Drawing.Size(275, 75);
            this.Load += new System.EventHandler(this.ucInjec_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTextBox tbInjec;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton tbOk;
    }
}
