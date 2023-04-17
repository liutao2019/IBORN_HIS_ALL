namespace API.GZSI.UI
{
    partial class frmComPareUpload
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
            this.neuButton3 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton4 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // neuButton3
            // 
            this.neuButton3.Location = new System.Drawing.Point(486, 26);
            this.neuButton3.Name = "neuButton3";
            this.neuButton3.Size = new System.Drawing.Size(186, 36);
            this.neuButton3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton3.TabIndex = 11;
            this.neuButton3.Tag = "3360";
            this.neuButton3.Text = "[3360]目录对照作废";
            this.neuButton3.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton3.UseVisualStyleBackColor = true;
            this.neuButton3.Click += new System.EventHandler(this.neuButton3_Click);
            // 
            // neuButton2
            // 
            this.neuButton2.Location = new System.Drawing.Point(231, 26);
            this.neuButton2.Name = "neuButton2";
            this.neuButton2.Size = new System.Drawing.Size(186, 36);
            this.neuButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton2.TabIndex = 10;
            this.neuButton2.Tag = "3302";
            this.neuButton2.Text = "[3302]目录对照撤销";
            this.neuButton2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton2.UseVisualStyleBackColor = true;
            this.neuButton2.Click += new System.EventHandler(this.neuButton2_Click);
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(12, 26);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(186, 36);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 9;
            this.neuButton1.Tag = "3301";
            this.neuButton1.Text = "[3301]目录对照上传";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // neuButton4
            // 
            this.neuButton4.Location = new System.Drawing.Point(12, 102);
            this.neuButton4.Name = "neuButton4";
            this.neuButton4.Size = new System.Drawing.Size(186, 36);
            this.neuButton4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton4.TabIndex = 12;
            this.neuButton4.Tag = "4101";
            this.neuButton4.Text = "[4101]";
            this.neuButton4.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton4.UseVisualStyleBackColor = true;
            this.neuButton4.Click += new System.EventHandler(this.neuButton4_Click);
            // 
            // frmComPareUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 161);
            this.Controls.Add(this.neuButton4);
            this.Controls.Add(this.neuButton3);
            this.Controls.Add(this.neuButton2);
            this.Controls.Add(this.neuButton1);
            this.Name = "frmComPareUpload";
            this.Text = "对照信息上传";
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton neuButton3;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton2;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton4;

    }
}