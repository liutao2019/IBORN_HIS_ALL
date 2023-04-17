namespace FS.HISFC.Components.Registration.NewRegister
{
    partial class FrmTag
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTag));
            this.plTB = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // plTB
            // 
            this.plTB.Location = new System.Drawing.Point(3, 2);
            this.plTB.Name = "plTB";
            this.plTB.Size = new System.Drawing.Size(176, 56);
            this.plTB.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "微信图片_20180705102025.png");
            this.imageList1.Images.SetKeyName(1, "微信图片_20180705102120.png");
            this.imageList1.Images.SetKeyName(2, "微信图片_20180705102317.png");
            this.imageList1.Images.SetKeyName(3, "微信图片_20180705102322.png");
            this.imageList1.Images.SetKeyName(4, "微信图片_20180705102326.png");
            // 
            // FrmTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 61);
            this.Controls.Add(this.plTB);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTag";
            this.Text = "客户标签";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plTB;
        private System.Windows.Forms.ImageList imageList1;
    }
}