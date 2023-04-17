using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SOC.Local.PacsInterface.GYSY.InPaitent
{
	/// <summary>
	/// frmPacsApply 的摘要说明。
	/// </summary>
	public class frmPacsApply : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton tbSave;
		private System.Windows.Forms.ToolBarButton tbPrint;
		private System.Windows.Forms.ToolBarButton tbExit;
		private System.Windows.Forms.ImageList imageList32;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
        public  ucPacsApply ucPacsApply1;
		private System.ComponentModel.IContainer components;
		private ArrayList  alItems=null;
		private System.Windows.Forms.ToolBarButton tbPrintView;
		private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="Items"></param>
		/// <param name="patientInfo"></param>
		public frmPacsApply(ArrayList Items,FS.HISFC.Models.RADT.PatientInfo patientInfo)
		{
			this.alItems = Items;
			this.patientInfo = patientInfo;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmPacsApply));
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.tbSave = new System.Windows.Forms.ToolBarButton();
			this.tbPrint = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.tbExit = new System.Windows.Forms.ToolBarButton();
			this.imageList32 = new System.Windows.Forms.ImageList(this.components);
			this.tbPrintView = new System.Windows.Forms.ToolBarButton();
			this.SuspendLayout();
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolBarButton1,
																						this.tbSave,
																						this.tbPrint,
																						this.tbPrintView,
																						this.toolBarButton2,
																						this.tbExit});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList32;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(776, 57);
			this.toolBar1.TabIndex = 0;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex = 11;
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbSave
			// 
			this.tbSave.ImageIndex = 6;
			this.tbSave.Text = "保  存";
			// 
			// tbPrint
			// 
			this.tbPrint.ImageIndex = 12;
			this.tbPrint.Text = "打  印";
			this.tbPrint.Visible =false;
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbExit
			// 
			this.tbExit.ImageIndex = 14;
			this.tbExit.Text = "退  出";
			// 
			// imageList32
			// 
			this.imageList32.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList32.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
			this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbPrintView
			// 
			this.tbPrintView.ImageIndex = 11;
			this.tbPrintView.Text = "打印预览";
			// 
			// frmPacsApply
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(776, 637);
			this.Controls.Add(this.toolBar1);
			this.Name = "frmPacsApply";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "检查申请单";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.frmPacsApply_Load);
			this.ResumeLayout(false);

		}
		#endregion
        /// <summary>
        /// ToolBar1的按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == this.tbPrintView)
			{
				this.ucPacsApply1.PrintPreview();
			}
			if(e.Button == this.tbSave)
			{
				ucPacsApply1.Save();
			}
			else if(e.Button == this.tbPrint)
			{
				this.ucPacsApply1.Print();
			}
			else if(e.Button == this.tbExit)
			{
				this.DialogResult = DialogResult.Cancel;
				this.ucPacsApply1.Exit();
			}
		}
        /// <summary>
        /// 检查申请单Load函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void frmPacsApply_Load(object sender, System.EventArgs e)
		{
			this.tbSave.Visible = true;
			ucPacsApply1 = new ucPacsApply(alItems,this.patientInfo);
			this.Controls.Add(ucPacsApply1);
			ucPacsApply1.Dock = DockStyle.Fill;
			ucPacsApply1.BringToFront();
			ucPacsApply1.Visible = true;
		}
	}
}
