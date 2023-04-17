namespace FS.HISFC.Components.Manager.Controls
{
    partial class ucEmployeeBarcode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.npbEmpNO = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.nlbEmpName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.npbEmpNO)).BeginInit();
            this.SuspendLayout();
            // 
            // npbEmpNO
            // 
            this.npbEmpNO.Location = new System.Drawing.Point(25, 46);
            this.npbEmpNO.Name = "npbEmpNO";
            this.npbEmpNO.Size = new System.Drawing.Size(151, 64);
            this.npbEmpNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npbEmpNO.TabIndex = 30;
            this.npbEmpNO.TabStop = false;
            // 
            // nlbEmpName
            // 
            this.nlbEmpName.AutoSize = true;
            this.nlbEmpName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbEmpName.Location = new System.Drawing.Point(77, 13);
            this.nlbEmpName.Name = "nlbEmpName";
            this.nlbEmpName.Size = new System.Drawing.Size(37, 14);
            this.nlbEmpName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbEmpName.TabIndex = 31;
            this.nlbEmpName.Text = "张三";
            // 
            // ucEmployeeBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.nlbEmpName);
            this.Controls.Add(this.npbEmpNO);
            this.Name = "ucEmployeeBarcode";
            this.Size = new System.Drawing.Size(200, 120);
            ((System.ComponentModel.ISupportInitialize)(this.npbEmpNO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPictureBox npbEmpNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbEmpName;
    }
}