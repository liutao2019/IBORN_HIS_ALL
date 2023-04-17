namespace FS.SOC.HISFC.Components.DCP.CancerReport
{
    partial class frmCaseInput
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
            this.bttCancel = new System.Windows.Forms.Button();
            this.bttOk = new System.Windows.Forms.Button();
            this.rtxtCase = new System.Windows.Forms.RichTextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bttCancel
            // 
            this.bttCancel.Location = new System.Drawing.Point(240, 160);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(75, 23);
            this.bttCancel.TabIndex = 6;
            this.bttCancel.Text = "返回";
            // 
            // bttOk
            // 
            this.bttOk.Location = new System.Drawing.Point(147, 160);
            this.bttOk.Name = "bttOk";
            this.bttOk.Size = new System.Drawing.Size(75, 23);
            this.bttOk.TabIndex = 5;
            this.bttOk.Text = "确定";
            // 
            // rtxtCase
            // 
            this.rtxtCase.Location = new System.Drawing.Point(0, 30);
            this.rtxtCase.Name = "rtxtCase";
            this.rtxtCase.Size = new System.Drawing.Size(368, 128);
            this.rtxtCase.TabIndex = 4;
            this.rtxtCase.Text = "";
            // 
            // lbTitle
            // 
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.ForeColor = System.Drawing.Color.Blue;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(368, 23);
            this.lbTitle.TabIndex = 7;
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // frmCaseInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 197);
            this.ControlBox = false;
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.bttCancel);
            this.Controls.Add(this.bttOk);
            this.Controls.Add(this.rtxtCase);
            this.Name = "frmCaseInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<<录入>>";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmCaseInput_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bttCancel;
        private System.Windows.Forms.Button bttOk;
        private System.Windows.Forms.RichTextBox rtxtCase;
        private System.Windows.Forms.Label lbTitle;
    }
}