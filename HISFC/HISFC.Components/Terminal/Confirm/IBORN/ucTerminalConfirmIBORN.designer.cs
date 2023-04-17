using FS.HISFC.Components.Terminal.Confirm;
namespace FS.HISFC.Components.Terminal.Confirm.IBORN
{
	partial class ucTerminalConfirmIBORN
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
            this.neuPanelPatientTree = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucPatientTree = new FS.HISFC.Components.Terminal.Confirm.ucPatientTree();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanelPatientInformation = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucPatientInformation = new FS.HISFC.Components.Terminal.Confirm.IBORN.ucPatientInformationIBORN();
            this.neuPanelTerminalConfirm = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucItemApplyIBORN = new FS.HISFC.Components.Terminal.Confirm.IBORN.ucItemApplyIBORN();
            this.neuPanelKey = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabelKey1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanelPatientTree.SuspendLayout();
            this.neuPanelPatientInformation.SuspendLayout();
            this.neuPanelTerminalConfirm.SuspendLayout();
            this.neuPanelKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelPatientTree
            // 
            this.neuPanelPatientTree.Controls.Add(this.ucPatientTree);
            this.neuPanelPatientTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanelPatientTree.Location = new System.Drawing.Point(0, 0);
            this.neuPanelPatientTree.Name = "neuPanelPatientTree";
            this.neuPanelPatientTree.Size = new System.Drawing.Size(183, 533);
            this.neuPanelPatientTree.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelPatientTree.TabIndex = 0;
            this.neuPanelPatientTree.Visible = false;
            // 
            // ucPatientTree
            // 
            this.ucPatientTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPatientTree.CurrentDepartment = "";
            this.ucPatientTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPatientTree.IsFullConvertToHalf = true;
            this.ucPatientTree.IsPrint = false;
            this.ucPatientTree.Location = new System.Drawing.Point(0, 0);
            this.ucPatientTree.Name = "ucPatientTree";
            this.ucPatientTree.ParentFormToolBar = null;
            this.ucPatientTree.Size = new System.Drawing.Size(183, 533);
            this.ucPatientTree.TabIndex = 0;
            this.ucPatientTree.WindowType = "0";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(183, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(5, 533);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanelPatientInformation
            // 
            this.neuPanelPatientInformation.Controls.Add(this.ucPatientInformation);
            this.neuPanelPatientInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanelPatientInformation.Location = new System.Drawing.Point(188, 0);
            this.neuPanelPatientInformation.Name = "neuPanelPatientInformation";
            this.neuPanelPatientInformation.Size = new System.Drawing.Size(1090, 59);
            this.neuPanelPatientInformation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelPatientInformation.TabIndex = 2;
            // 
            // ucPatientInformation
            // 
            this.ucPatientInformation.BackColor = System.Drawing.SystemColors.Control;
            this.ucPatientInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPatientInformation.IsFullConvertToHalf = true;
            this.ucPatientInformation.IsPrint = false;
            this.ucPatientInformation.Location = new System.Drawing.Point(0, 0);
            this.ucPatientInformation.Name = "ucPatientInformation";
            this.ucPatientInformation.ParentFormToolBar = null;
            this.ucPatientInformation.Size = new System.Drawing.Size(1090, 59);
            this.ucPatientInformation.TabIndex = 0;
            this.ucPatientInformation.WindowType = "0";
            // 
            // neuPanelTerminalConfirm
            // 
            this.neuPanelTerminalConfirm.Controls.Add(this.ucItemApplyIBORN);
            this.neuPanelTerminalConfirm.Controls.Add(this.neuPanelKey);
            this.neuPanelTerminalConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelTerminalConfirm.Location = new System.Drawing.Point(188, 59);
            this.neuPanelTerminalConfirm.Name = "neuPanelTerminalConfirm";
            this.neuPanelTerminalConfirm.Size = new System.Drawing.Size(1090, 474);
            this.neuPanelTerminalConfirm.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelTerminalConfirm.TabIndex = 3;
            // 
            // ucItemApplyIBORN
            // 
            this.ucItemApplyIBORN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucItemApplyIBORN.ByText = "";
            this.ucItemApplyIBORN.CurrentColumn = 0;
            this.ucItemApplyIBORN.CurrentRow = -1;
            this.ucItemApplyIBORN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucItemApplyIBORN.EnumItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.All;
            this.ucItemApplyIBORN.IsConfirmItem = false;
            this.ucItemApplyIBORN.IsFullConvertToHalf = true;
            this.ucItemApplyIBORN.IsNeedOper = false;
            this.ucItemApplyIBORN.IsPrint = false;
            this.ucItemApplyIBORN.Location = new System.Drawing.Point(0, 0);
            this.ucItemApplyIBORN.Name = "ucItemApplyIBORN";
            this.ucItemApplyIBORN.ParentFormToolBar = null;
            this.ucItemApplyIBORN.RowCount = 0;
            this.ucItemApplyIBORN.Size = new System.Drawing.Size(1090, 437);
            this.ucItemApplyIBORN.TabIndex = 0;
            this.ucItemApplyIBORN.WindowType = "0";
            this.ucItemApplyIBORN.Load += new System.EventHandler(this.ucItemApplyIBORN_Load);
            // 
            // neuPanelKey
            // 
            this.neuPanelKey.Controls.Add(this.neuLabelKey1);
            this.neuPanelKey.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanelKey.Location = new System.Drawing.Point(0, 437);
            this.neuPanelKey.Name = "neuPanelKey";
            this.neuPanelKey.Size = new System.Drawing.Size(1090, 37);
            this.neuPanelKey.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelKey.TabIndex = 1;
            // 
            // neuLabelKey1
            // 
            this.neuLabelKey1.AutoSize = true;
            this.neuLabelKey1.Location = new System.Drawing.Point(3, 12);
            this.neuLabelKey1.Name = "neuLabelKey1";
            this.neuLabelKey1.Size = new System.Drawing.Size(743, 12);
            this.neuLabelKey1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelKey1.TabIndex = 0;
            this.neuLabelKey1.Text = "" ;
            // 
            // ucTerminalConfirmIBORN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanelTerminalConfirm);
            this.Controls.Add(this.neuPanelPatientInformation);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanelPatientTree);
            this.Name = "ucTerminalConfirmIBORN";
            this.Size = new System.Drawing.Size(1278, 533);
            this.neuPanelPatientTree.ResumeLayout(false);
            this.neuPanelPatientInformation.ResumeLayout(false);
            this.neuPanelTerminalConfirm.ResumeLayout(false);
            this.neuPanelKey.ResumeLayout(false);
            this.neuPanelKey.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// 患者树的Panel
		/// </summary>
		private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelPatientTree;
		/// <summary>
		/// 患者树
		/// </summary>
		private ucPatientTree ucPatientTree;
		/// <summary>
		/// 分割条
		/// </summary>
		private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
		/// <summary>
		/// 患者基本信息Panel
		/// </summary>
		private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelPatientInformation;
		/// <summary>
		/// 终端确认Panel
		/// </summary>
		private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelTerminalConfirm;
		/// <summary>
		/// 终端确认项目UC
		/// </summary>
		private ucItemApplyIBORN ucItemApplyIBORN;
		/// <summary>
		/// 患者基本信息UC
		/// </summary>
		private ucPatientInformationIBORN ucPatientInformation;
		private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelKey;
		private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelKey1;

	}
}
