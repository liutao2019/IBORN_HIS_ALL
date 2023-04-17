namespace FS.SOC.Local.Registration.GuangZhou.Zdly.IPrintBar
{
    partial class ucBarCode
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
            this.lblname = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblsex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.BackColor = System.Drawing.Color.White;
            this.lblname.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblname.Location = new System.Drawing.Point(24, 74);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(22, 14);
            this.lblname.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblname.TabIndex = 13;
            this.lblname.Tag = "print";
            this.lblname.Text = "印";
            // 
            // lblsex
            // 
            this.lblsex.AutoSize = true;
            this.lblsex.BackColor = System.Drawing.Color.White;
            this.lblsex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblsex.Location = new System.Drawing.Point(122, 74);
            this.lblsex.Name = "lblsex";
            this.lblsex.Size = new System.Drawing.Size(22, 14);
            this.lblsex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblsex.TabIndex = 14;
            this.lblsex.Tag = "print";
            this.lblsex.Text = "印";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 47);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.BackColor = System.Drawing.Color.White;
            this.neuLabel1.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(14, 3);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(157, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 16;
            this.neuLabel1.Tag = "print";
            this.neuLabel1.Text = "中山大学附属第六医院";
            // 
            // ucBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblsex);
            this.Controls.Add(this.lblname);
            this.Name = "ucBarCode";
            this.Size = new System.Drawing.Size(183, 91);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel lblname;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblsex;
        private System.Windows.Forms.PictureBox pictureBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}
