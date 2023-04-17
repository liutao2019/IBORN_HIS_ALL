namespace FS.SOC.Local.Account.FoSan
{
    partial class ucLabelPrint
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
            this.npbCardNo1 = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.lblInfo1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            ((System.ComponentModel.ISupportInitialize)(this.npbCardNo1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // npbCardNo1
            // 
            this.npbCardNo1.Location = new System.Drawing.Point(13, 27);
            this.npbCardNo1.Name = "npbCardNo1";
            this.npbCardNo1.Size = new System.Drawing.Size(135, 37);
            this.npbCardNo1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npbCardNo1.TabIndex = 0;
            this.npbCardNo1.TabStop = false;
            // 
            // lblInfo1
            // 
            this.lblInfo1.AutoSize = true;
            this.lblInfo1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo1.Location = new System.Drawing.Point(23, 69);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(112, 16);
            this.lblInfo1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInfo1.TabIndex = 1;
            this.lblInfo1.Text = "张三三     男";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "佛山市第三人民医院";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.label1);
            this.neuPanel1.Controls.Add(this.lblInfo1);
            this.neuPanel1.Controls.Add(this.npbCardNo1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(170, 90);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ucLabelPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucLabelPrint";
            this.Size = new System.Drawing.Size(170, 90);
            ((System.ComponentModel.ISupportInitialize)(this.npbCardNo1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPictureBox npbCardNo1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInfo1;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;


    }
}
