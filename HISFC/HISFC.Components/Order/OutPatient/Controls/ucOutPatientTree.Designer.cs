namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucOutPatientTree
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
            //新分诊流程 且 开启自动更新队列才调用，同时排除管理员
            //与frmSelectRoom.CheckUpdateQueue()判断条件保持一致
            if (this.isNewAssign && this.isAutoDoctQueue && !this.isManager)
            {
                this.RemoveDoctQueue();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOutPatientTree));
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.linkLabel1 = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.ucQuerySeeNoByCardNo1 = new FS.HISFC.Components.Common.Controls.ucQuerySeeNoByCardNo();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuTreeView1 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.neuTreeView2 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.linkLabel1);
            this.neuPanel1.Controls.Add(this.ucQuerySeeNoByCardNo1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(219, 35);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.Location = new System.Drawing.Point(155, 10);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 14);
            this.linkLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "已诊";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ucQuerySeeNoByCardNo1
            // 
            this.ucQuerySeeNoByCardNo1.BackColor = System.Drawing.Color.Transparent;
            this.ucQuerySeeNoByCardNo1.IsAllowNoRegSee = false;
            this.ucQuerySeeNoByCardNo1.IsUserOnePersonMorePact = false;
            this.ucQuerySeeNoByCardNo1.Location = new System.Drawing.Point(0, 4);
            this.ucQuerySeeNoByCardNo1.Name = "ucQuerySeeNoByCardNo1";
            this.ucQuerySeeNoByCardNo1.Size = new System.Drawing.Size(144, 31);
            this.ucQuerySeeNoByCardNo1.TabIndex = 1;
            this.ucQuerySeeNoByCardNo1.UseType = FS.HISFC.Components.Common.Controls.enumUseType.Other;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuTreeView1);
            this.neuPanel2.Controls.Add(this.neuTreeView2);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 35);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(219, 425);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // neuTreeView1
            // 
            this.neuTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTreeView1.HideSelection = false;
            this.neuTreeView1.ImageIndex = 0;
            this.neuTreeView1.ImageList = this.imageList1;
            this.neuTreeView1.Location = new System.Drawing.Point(0, 0);
            this.neuTreeView1.Name = "neuTreeView1";
            this.neuTreeView1.SelectedImageIndex = 0;
            this.neuTreeView1.Size = new System.Drawing.Size(219, 425);
            this.neuTreeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "人物_成人_男.png");
            this.imageList1.Images.SetKeyName(5, "人物_成人_女.png");
            this.imageList1.Images.SetKeyName(6, "人物_儿童_男.png");
            this.imageList1.Images.SetKeyName(7, "人物_儿童_女.png");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            // 
            // neuTreeView2
            // 
            this.neuTreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTreeView2.HideSelection = false;
            this.neuTreeView2.ImageIndex = 0;
            this.neuTreeView2.ImageList = this.imageList1;
            this.neuTreeView2.Location = new System.Drawing.Point(0, 0);
            this.neuTreeView2.Name = "neuTreeView2";
            this.neuTreeView2.SelectedImageIndex = 0;
            this.neuTreeView2.Size = new System.Drawing.Size(219, 425);
            this.neuTreeView2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView2.TabIndex = 1;
            // 
            // ucOutPatientTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucOutPatientTree";
            this.Size = new System.Drawing.Size(219, 460);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        //{22571B58-A56B-4dc3-A32C-EC17D74423A2}
        public FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView1;
        private System.Windows.Forms.ImageList imageList1;
        //{22571B58-A56B-4dc3-A32C-EC17D74423A2}
        public FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView2;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel linkLabel1;
        private FS.HISFC.Components.Common.Controls.ucQuerySeeNoByCardNo ucQuerySeeNoByCardNo1;
        //= new FS.HISFC.Components.Common.Controls.ucQuerySeeNoByCardNo();
    }
}
