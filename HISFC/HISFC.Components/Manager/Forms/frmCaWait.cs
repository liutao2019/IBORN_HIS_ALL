using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Forms
{
	/// <summary>
	/// frmWait 的摘要说明。
	/// </summary>
	public class frmCaWait : FS.FrameWork.WinForms.Forms.BaseForm
	{
		private FS.FrameWork.WinForms.Controls.NeuLabel label1;
		public FS.FrameWork.WinForms.Controls.NeuProgressBar progressBar1;
		private FS.FrameWork.WinForms.Controls.NeuPictureBox pictureBox1;
		private FS.FrameWork.WinForms.Controls.NeuButton button1;
		private FS.FrameWork.WinForms.Controls.NeuLinkLabel linkLabel1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

        public frmCaWait()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaWait));
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.progressBar1 = new FS.FrameWork.WinForms.Controls.NeuProgressBar();
            this.pictureBox1 = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.button1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.linkLabel1 = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 25);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label1.TabIndex = 0;
            this.label1.Text = "正在处理，请稍候...";
            // 
            // progressBar1
            // 
            this.progressBar1.BackgroundBitmap = null;
            this.progressBar1.BackgroundColor = System.Drawing.Color.White;
            this.progressBar1.Border3D = System.Windows.Forms.Border3DStyle.Flat;
            this.progressBar1.BorderColor = System.Drawing.SystemColors.Highlight;
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.EnableBorder3D = false;
            this.progressBar1.ForegroundBitmap = null;
            this.progressBar1.ForegroundColor = System.Drawing.SystemColors.Highlight;
            this.progressBar1.GradientEndColor = System.Drawing.Color.Empty;
            this.progressBar1.GradientMiddleColor = System.Drawing.Color.Empty;
            this.progressBar1.GradientStartColor = System.Drawing.Color.Empty;
            this.progressBar1.Location = new System.Drawing.Point(0, 54);
            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.ProgressTextColor = System.Drawing.Color.Empty;
            this.progressBar1.ProgressTextHiglightColor = System.Drawing.Color.Empty;
            this.progressBar1.ShowProgressText = false;
            this.progressBar1.Size = new System.Drawing.Size(298, 20);
            this.progressBar1.Smooth = false;
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(278, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(16, 16);
            this.button1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.button1.TabIndex = 3;
            this.button1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.Location = new System.Drawing.Point(239, 2);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(33, 16);
            this.linkLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "取消";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // frmCaWait
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(298, 74);
            this.ControlBox = false;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmCaWait";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmWait_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmWait_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		public Timer t =new Timer();
		private void frmWait_Load(object sender, System.EventArgs e)
		{
			t.Interval =1000;
			t.Tick+=new EventHandler(t_Tick);
			this.ShowInTaskbar = false;
			this.TopMost = true;
			this.progressBar1.Maximum =100;
		}
		public new void Hide()
		{
			t.Enabled = false;
			base.Hide();
		}
		public new void Show()
		{
			dt = DateTime.Now;
			t.Enabled = true;
			this.button1.Visible = false;
			base.Show();
		}
		public string Tip
		{
			set
			{
				this.label1.Text = value;
				if(value.IndexOf("\n")>0)
				{
					this.label1.Location= new Point(60, 6);
				}
				else
				{
					this.label1.Location= new Point(60, 16);
				}
				this.label1.Refresh();
				Application.DoEvents();
				this.label1.Refresh();
			}
		}
		protected DateTime dt;
		public int Progress
		{
			set
			{
				if(value <=0 || value >=this.progressBar1.Maximum) 
				{
					if(this.progressBar1.Visible)
					{
						this.progressBar1.Visible = false;
						Application.DoEvents();
						this.progressBar1.Refresh();
					}
					
				}
				else
				{
					if(this.progressBar1.Visible==false)
					{
						this.progressBar1.Visible = true;
					}
					this.progressBar1.Value = value;
					Application.DoEvents();
					this.progressBar1.Refresh();
				}
				
			}
		}

		private void t_Tick(object sender, EventArgs e)
		{
			TimeSpan p= new TimeSpan( DateTime.Now.Ticks - dt.Ticks);
			if(  p.TotalMinutes > 3)
			{
				if(this.button1.Visible==false) this.button1.Visible = true;
			}
			else
			{
				if(this.button1.Visible) this.button1.Visible =false;
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}
		public bool Cancel = false;

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.linkLabel1.Visible = false;
			this.Cancel = true;
			this.Hide();
			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}
	
		public bool IsShowCancelButton
		{
			set
			{
				this.linkLabel1.Visible = value;
			}
		}

        /// <summary>
        /// 加入此段代码屏蔽ALT+F4关闭窗口
        /// 此窗口关闭后，再次调用ShowWaitForm会报错
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWait_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
            {
                this.Hide();
                Cursor.Current = System.Windows.Forms.Cursors.Default;
                e.Handled = true;
            }
        }
	}
}
