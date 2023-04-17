namespace FS.HISFC.Components.Registration.NewRegister
{
    partial class UCCustomerTag
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCustomerTag));
            this.plTB = new System.Windows.Forms.Panel();
            this.imgPackage = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.imgeallery = new System.Windows.Forms.PictureBox();
            this.imgVip = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.plTB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgeallery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVip)).BeginInit();
            this.SuspendLayout();
            // 
            // plTB
            // 
            this.plTB.Controls.Add(this.imgPackage);
            this.plTB.Controls.Add(this.pictureBox4);
            this.plTB.Controls.Add(this.imgeallery);
            this.plTB.Controls.Add(this.imgVip);
            this.plTB.Location = new System.Drawing.Point(4, 3);
            this.plTB.Name = "plTB";
            this.plTB.Size = new System.Drawing.Size(115, 30);
            this.plTB.TabIndex = 0;
            // 
            // imgPackage
            // 
            this.imgPackage.Image = global::FS.HISFC.Components.Registration.Properties.Resources.套餐2;
            this.imgPackage.Location = new System.Drawing.Point(4, 3);
            this.imgPackage.Name = "imgPackage";
            this.imgPackage.Size = new System.Drawing.Size(25, 25);
            this.imgPackage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgPackage.TabIndex = 106;
            this.imgPackage.TabStop = false;
            this.imgPackage.Click += new System.EventHandler(this.imgPackage_Click);
            this.imgPackage.MouseHover += new System.EventHandler(this.imgPackage_MouseHover);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::FS.HISFC.Components.Registration.Properties.Resources.星标2;
            this.pictureBox4.Location = new System.Drawing.Point(87, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(25, 25);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 107;
            this.pictureBox4.TabStop = false;
            // 
            // imgeallery
            // 
            this.imgeallery.Image = global::FS.HISFC.Components.Registration.Properties.Resources.过敏2;
            this.imgeallery.Location = new System.Drawing.Point(59, 3);
            this.imgeallery.Name = "imgeallery";
            this.imgeallery.Size = new System.Drawing.Size(25, 25);
            this.imgeallery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgeallery.TabIndex = 104;
            this.imgeallery.TabStop = false;
            // 
            // imgVip
            // 
            this.imgVip.Image = global::FS.HISFC.Components.Registration.Properties.Resources.VIP2;
            this.imgVip.Location = new System.Drawing.Point(31, 3);
            this.imgVip.Name = "imgVip";
            this.imgVip.Size = new System.Drawing.Size(25, 25);
            this.imgVip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgVip.TabIndex = 105;
            this.imgVip.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "VIP1.jpg");
            this.imageList1.Images.SetKeyName(1, "VIP2.jpg");
            this.imageList1.Images.SetKeyName(2, "套餐1.jpg");
            this.imageList1.Images.SetKeyName(3, "套餐2.jpg");
            this.imageList1.Images.SetKeyName(4, "过敏1.jpg");
            this.imageList1.Images.SetKeyName(5, "过敏2.jpg");
            this.imageList1.Images.SetKeyName(6, "星标1.jpg");
            this.imageList1.Images.SetKeyName(7, "星标2.jpg");
            // 
            // UCCustomerTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.plTB);
            this.Name = "UCCustomerTag";
            this.Size = new System.Drawing.Size(121, 35);
            this.Load += new System.EventHandler(this.UCCustomerTag_Load);
            this.plTB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgeallery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plTB;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox imgPackage;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox imgeallery;
        private System.Windows.Forms.PictureBox imgVip;
    }
}
