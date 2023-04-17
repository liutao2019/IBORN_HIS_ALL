using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Notice
{
	/// <summary>
	/// frmNotice 的摘要说明。
	/// </summary>
	public class frmNotice : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox rtbNotice;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmNotice()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		public frmNotice(bool isRun)
		{
			InitializeComponent();
			this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width,System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height);
			timer.Enabled = isRun;
			timer.Interval = 1;
			this.Load += new EventHandler(frmNotice_Load);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmNotice));
			this.rtbNotice = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// rtbNotice
			// 
			this.rtbNotice.BackColor = System.Drawing.Color.Honeydew;
			this.rtbNotice.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbNotice.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbNotice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rtbNotice.Location = new System.Drawing.Point(0, 0);
			this.rtbNotice.Name = "rtbNotice";
			this.rtbNotice.ReadOnly = true;
			this.rtbNotice.Size = new System.Drawing.Size(234, 144);
			this.rtbNotice.TabIndex = 0;
			this.rtbNotice.Text = "";
			// 
			// frmNotice
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(234, 144);
			this.Controls.Add(this.rtbNotice);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmNotice";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "温馨提示>>";
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Timer timer = new Timer();
		private int curHeight = 0;

		/// <summary>
		/// 提示信息
		/// </summary>
		public string Notice
		{
			set
			{
				this.rtbNotice.Text = value;
			}
		}

		/// <summary>
		/// 运行
		/// </summary>
		public void Run()
		{
			if(!this.timer.Enabled)
			{
				this.timer.Enabled = true;
			}
		}

		/// <summary>
		/// 隐藏
		/// </summary>
		public new void Hide()
		{
			if(this.timer.Enabled)
			{
				this.timer.Enabled = false;
				curHeight = 0;
				this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width-this.Width,System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height-(curHeight++));
				this.Hide();
			}
		}

		/// <summary>
		/// 显示消息
		/// </summary>
		/// <param name="notice"></param>
		public void Show(string notice)
		{
			this.Show(notice,true);
		}

		/// <summary>
		/// 显示消息
		/// </summary>
		/// <param name="notice"></param>
		public void Show(string notice, bool isNeedSound)
		{
			this.timer.Enabled = true;
			this.Notice = notice;
			this.Show();
			if(isNeedSound)
			{
                string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\DCP\\msg.wav";
				if(System.IO.File.Exists(fileName))
				{
					Sound.Play(fileName);
				}
			}
		}

		private void frmNotice_Load(object sender, EventArgs e)
		{
			timer.Tick += new EventHandler(timer_Tick);
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			curHeight++;
			if(curHeight > this.Height)
			{
				this.TopMost = true;
				timer.Enabled = false;
			}
			this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width-this.Width,System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height-(curHeight++));
		}
	}
}
