namespace FS.HISFC.Components.Order.Forms
{
    partial class frmAddMsgForm
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
            this.ucMesseageSend1 = new FS.HISFC.Components.Order.Controls.ucMesseageSend();
            this.SuspendLayout();
            // 
            // ucMesseageSend1
            // 
            this.ucMesseageSend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucMesseageSend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMesseageSend1.IsFullConvertToHalf = true;
            this.ucMesseageSend1.IsPrint = false;
            this.ucMesseageSend1.Location = new System.Drawing.Point(0, 0);
            this.ucMesseageSend1.Name = "ucMesseageSend1";
            this.ucMesseageSend1.ParentFormToolBar = null;
            this.ucMesseageSend1.Size = new System.Drawing.Size(394, 261);
            this.ucMesseageSend1.TabIndex = 0;
            // 
            // frmAddMsgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 261);
            this.Controls.Add(this.ucMesseageSend1);
            this.Name = "frmAddMsgForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "消息发送";
            this.ResumeLayout(false);

        }

        #endregion

        private FS.HISFC.Components.Order.Controls.ucMesseageSend ucMesseageSend1;
    }
}