namespace Neusoft.SOC.Local.PubReport.Components
{
    partial class ucPubReport
    {
       private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tvPactList;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvPactList = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtStaticMonth = new SOC.Local.PubReport.Components.DateTimeTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList16
            // 
            this.imageList16.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList16.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(736, 2);
            this.label1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tvPactList);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 428);
            this.panel1.TabIndex = 2;
            // 
            // tvPactList
            // 
            this.tvPactList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPactList.ImageIndex = 0;
            this.tvPactList.ImageList = this.imageList1;
            this.tvPactList.Location = new System.Drawing.Point(0, 40);
            this.tvPactList.Name = "tvPactList";
            this.tvPactList.SelectedImageIndex = 0;
            this.tvPactList.Size = new System.Drawing.Size(208, 388);
            this.tvPactList.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 2);
            this.label2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtStaticMonth);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(208, 38);
            this.panel3.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(208, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 428);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightCyan;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(211, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(525, 428);
            this.panel2.TabIndex = 4;
            // 
            // txtStaticMonth
            // 
            this.txtStaticMonth.Err = "";
            this.txtStaticMonth.Location = new System.Drawing.Point(76, 7);
            this.txtStaticMonth.Name = "txtStaticMonth";
            this.txtStaticMonth.Size = new System.Drawing.Size(113, 21);
            this.txtStaticMonth.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtStaticMonth.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "统计月份";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucPubReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "ucPubReport";
            this.Size = new System.Drawing.Size(736, 430);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private DateTimeTextBox txtStaticMonth;
        private System.Windows.Forms.Label label5;
    }
}
