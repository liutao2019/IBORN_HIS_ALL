using FS.HISFC.Components.Terminal.Confirm;
namespace FS.SOC.Local.OutpatientFee.FoSi
{
    partial class ucQueryOutPatientFee
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
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpFeed = new System.Windows.Forms.TabPage();
            this.tpUnFee = new System.Windows.Forms.TabPage();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.txtCardNO = new System.Windows.Forms.TextBox();
            this.pnlTime = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtBeginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanelPatientInformation = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanelTerminalConfirm = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucChargeItem = new FS.HISFC.FSLocal.OutPatientFee.From.ucShowItem();
            this.neuPanelKey = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabelKey1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucPatientInformation1 = new FS.SOC.Local.OutpatientFee.FoSi.ucPatientInformation();
            this.ucPatientTree = new FS.SOC.Local.OutpatientFee.FoSi.ucPatientTree();
            this.ucPatientTree1 = new FS.SOC.Local.OutpatientFee.FoSi.ucPatientTree();
            this.neuPanelPatientTree.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tpFeed.SuspendLayout();
            this.tpUnFee.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlTime.SuspendLayout();
            this.neuPanelPatientInformation.SuspendLayout();
            this.neuPanelTerminalConfirm.SuspendLayout();
            this.neuPanelKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelPatientTree
            // 
            this.neuPanelPatientTree.Controls.Add(this.neuTabControl1);
            this.neuPanelPatientTree.Controls.Add(this.pnlCard);
            this.neuPanelPatientTree.Controls.Add(this.pnlTime);
            this.neuPanelPatientTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanelPatientTree.Location = new System.Drawing.Point(0, 0);
            this.neuPanelPatientTree.Name = "neuPanelPatientTree";
            this.neuPanelPatientTree.Size = new System.Drawing.Size(251, 533);
            this.neuPanelPatientTree.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelPatientTree.TabIndex = 0;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpFeed);
            this.neuTabControl1.Controls.Add(this.tpUnFee);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 104);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(251, 429);
            this.neuTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 3;
            this.neuTabControl1.SelectedIndexChanged += new System.EventHandler(this.neuTabControl1_SelectedIndexChanged);
            // 
            // tpFeed
            // 
            this.tpFeed.Controls.Add(this.ucPatientTree);
            this.tpFeed.Location = new System.Drawing.Point(4, 21);
            this.tpFeed.Name = "tpFeed";
            this.tpFeed.Padding = new System.Windows.Forms.Padding(3);
            this.tpFeed.Size = new System.Drawing.Size(243, 404);
            this.tpFeed.TabIndex = 0;
            this.tpFeed.Text = "已扣费患者";
            this.tpFeed.UseVisualStyleBackColor = true;
            // 
            // tpUnFee
            // 
            this.tpUnFee.Controls.Add(this.ucPatientTree1);
            this.tpUnFee.Location = new System.Drawing.Point(4, 21);
            this.tpUnFee.Name = "tpUnFee";
            this.tpUnFee.Padding = new System.Windows.Forms.Padding(3);
            this.tpUnFee.Size = new System.Drawing.Size(231, 404);
            this.tpUnFee.TabIndex = 1;
            this.tpUnFee.Text = "当前患者有效信息";
            this.tpUnFee.UseVisualStyleBackColor = true;
            // 
            // pnlCard
            // 
            this.pnlCard.Controls.Add(this.txtCardNO);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCard.Location = new System.Drawing.Point(0, 74);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(251, 30);
            this.pnlCard.TabIndex = 0;
            // 
            // txtCardNO
            // 
            this.txtCardNO.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.Location = new System.Drawing.Point(0, 0);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(251, 29);
            this.txtCardNO.TabIndex = 0;
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // pnlTime
            // 
            this.pnlTime.Controls.Add(this.neuLabel2);
            this.pnlTime.Controls.Add(this.neuLabel1);
            this.pnlTime.Controls.Add(this.dtEndTime);
            this.pnlTime.Controls.Add(this.dtBeginDate);
            this.pnlTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTime.Location = new System.Drawing.Point(0, 0);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(251, 74);
            this.pnlTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTime.TabIndex = 2;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(20, 47);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(59, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "结束时间:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(20, 14);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "开始时间:";
            // 
            // dtEndTime
            // 
            this.dtEndTime.CustomFormat = "yyyy-MM-dd";
            this.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndTime.IsEnter2Tab = false;
            this.dtEndTime.Location = new System.Drawing.Point(85, 43);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(99, 21);
            this.dtEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEndTime.TabIndex = 0;
            // 
            // dtBeginDate
            // 
            this.dtBeginDate.CustomFormat = "yyyy-MM-dd";
            this.dtBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBeginDate.IsEnter2Tab = false;
            this.dtBeginDate.Location = new System.Drawing.Point(85, 10);
            this.dtBeginDate.Name = "dtBeginDate";
            this.dtBeginDate.Size = new System.Drawing.Size(99, 21);
            this.dtBeginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBeginDate.TabIndex = 0;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(251, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(5, 533);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanelPatientInformation
            // 
            this.neuPanelPatientInformation.BackColor = System.Drawing.SystemColors.Control;
            this.neuPanelPatientInformation.Controls.Add(this.ucPatientInformation1);
            this.neuPanelPatientInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanelPatientInformation.ForeColor = System.Drawing.Color.Red;
            this.neuPanelPatientInformation.Location = new System.Drawing.Point(256, 0);
            this.neuPanelPatientInformation.Name = "neuPanelPatientInformation";
            this.neuPanelPatientInformation.Size = new System.Drawing.Size(1022, 59);
            this.neuPanelPatientInformation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelPatientInformation.TabIndex = 2;
            // 
            // neuPanelTerminalConfirm
            // 
            this.neuPanelTerminalConfirm.Controls.Add(this.ucChargeItem);
            this.neuPanelTerminalConfirm.Controls.Add(this.neuPanelKey);
            this.neuPanelTerminalConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelTerminalConfirm.Location = new System.Drawing.Point(256, 59);
            this.neuPanelTerminalConfirm.Name = "neuPanelTerminalConfirm";
            this.neuPanelTerminalConfirm.Size = new System.Drawing.Size(1022, 474);
            this.neuPanelTerminalConfirm.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelTerminalConfirm.TabIndex = 3;
            // 
            // ucChargeItem
            // 
            this.ucChargeItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucChargeItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChargeItem.IsFullConvertToHalf = true;
            this.ucChargeItem.IsPrint = false;
            this.ucChargeItem.IsShowDoseOnce = true;
            this.ucChargeItem.IsShowSpecs = true;
            this.ucChargeItem.IsShowUsage = true;
            this.ucChargeItem.Location = new System.Drawing.Point(0, 0);
            this.ucChargeItem.Name = "ucChargeItem";
            this.ucChargeItem.Size = new System.Drawing.Size(1022, 437);
            this.ucChargeItem.TabIndex = 0;
            // 
            // neuPanelKey
            // 
            this.neuPanelKey.Controls.Add(this.neuLabelKey1);
            this.neuPanelKey.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanelKey.Location = new System.Drawing.Point(0, 437);
            this.neuPanelKey.Name = "neuPanelKey";
            this.neuPanelKey.Size = new System.Drawing.Size(1022, 37);
            this.neuPanelKey.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelKey.TabIndex = 1;
            // 
            // neuLabelKey1
            // 
            this.neuLabelKey1.AutoSize = true;
            this.neuLabelKey1.Location = new System.Drawing.Point(3, 12);
            this.neuLabelKey1.Name = "neuLabelKey1";
            this.neuLabelKey1.Size = new System.Drawing.Size(221, 12);
            this.neuLabelKey1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelKey1.TabIndex = 0;
            this.neuLabelKey1.Text = "F5——显示全部    F7——确认扣费    ";
            // 
            // ucPatientInformation1
            // 
            this.ucPatientInformation1.BackColor = System.Drawing.SystemColors.Control;
            this.ucPatientInformation1.IsFullConvertToHalf = true;
            this.ucPatientInformation1.IsPrint = false;
            this.ucPatientInformation1.Location = new System.Drawing.Point(1, 0);
            this.ucPatientInformation1.Name = "ucPatientInformation1";
            this.ucPatientInformation1.Size = new System.Drawing.Size(726, 61);
            this.ucPatientInformation1.TabIndex = 0;
            this.ucPatientInformation1.WindowType = "1";
            // 
            // ucPatientTree
            // 
            this.ucPatientTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPatientTree.CurrentDepartment = "";
            this.ucPatientTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPatientTree.IsFullConvertToHalf = true;
            this.ucPatientTree.IsPrint = false;
            this.ucPatientTree.Location = new System.Drawing.Point(3, 3);
            this.ucPatientTree.Name = "ucPatientTree";
            this.ucPatientTree.Size = new System.Drawing.Size(237, 398);
            this.ucPatientTree.TabIndex = 1;
            this.ucPatientTree.WindowType = "3";
            // 
            // ucPatientTree1
            // 
            this.ucPatientTree1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPatientTree1.CurrentDepartment = "";
            this.ucPatientTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPatientTree1.IsFullConvertToHalf = true;
            this.ucPatientTree1.IsPrint = false;
            this.ucPatientTree1.Location = new System.Drawing.Point(3, 3);
            this.ucPatientTree1.Name = "ucPatientTree1";
            this.ucPatientTree1.Size = new System.Drawing.Size(225, 398);
            this.ucPatientTree1.TabIndex = 1;
            this.ucPatientTree1.WindowType = "3";
            // 
            // ucQueryOutPatientFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanelTerminalConfirm);
            this.Controls.Add(this.neuPanelPatientInformation);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanelPatientTree);
            this.Name = "ucQueryOutPatientFee";
            this.Size = new System.Drawing.Size(1278, 533);
            this.Load += new System.EventHandler(this.ucAccountTerminalForDoc_Load);
            this.neuPanelPatientTree.ResumeLayout(false);
            this.neuTabControl1.ResumeLayout(false);
            this.tpFeed.ResumeLayout(false);
            this.tpUnFee.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
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
		private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelKey;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelKey1;
        private FS.HISFC.FSLocal.OutPatientFee.From.ucShowItem ucChargeItem;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTime;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TextBox txtCardNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBeginDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEndTime;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tpFeed;
        private System.Windows.Forms.TabPage tpUnFee;
        private ucPatientTree ucPatientTree;
        private ucPatientTree ucPatientTree1;
        private ucPatientInformation ucPatientInformation1;

	}
}
