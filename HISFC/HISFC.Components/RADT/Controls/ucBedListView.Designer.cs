namespace FS.HISFC.Components.RADT.Controls
{
    partial class ucBedListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBedListView));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mnuSet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuI_Large = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuI_Small = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuI_List = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuI_Detail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuI_card = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintCard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBed = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuI_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuI_Wap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_ChangeBed = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuI_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuI_Cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDealBed = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuI_Unwap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReceive = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuI_Arrange = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBedSumInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lsvBedView = new FS.FrameWork.WinForms.Controls.NeuListView();
            this.mmuBedChange = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSet.SuspendLayout();
            this.mnuBed.SuspendLayout();
            this.mnuDealBed.SuspendLayout();
            this.mnuReceive.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bed_e.ico");
            this.imageList1.Images.SetKeyName(1, "bed_m.ico");
            this.imageList1.Images.SetKeyName(2, "bed_f.ico");
            this.imageList1.Images.SetKeyName(3, "bed_l.ico");
            this.imageList1.Images.SetKeyName(4, "bed_z.ico");
            this.imageList1.Images.SetKeyName(5, "bed_bw2.ico");
            this.imageList1.Images.SetKeyName(6, "bed_bw1.ico");
            this.imageList1.Images.SetKeyName(7, "bed_zj.ico");
            this.imageList1.Images.SetKeyName(8, "bed_bw.ico");
            this.imageList1.Images.SetKeyName(9, "bed_b.ico");
            this.imageList1.Images.SetKeyName(10, "bed_out.ico");
            this.imageList1.Images.SetKeyName(11, "bed_yy.ico");
            this.imageList1.Images.SetKeyName(12, "bed_baby.png");
            this.imageList1.Images.SetKeyName(13, "bed_baby1.png");
            // 
            // mnuSet
            // 
            this.mnuSet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuI_Large,
            this.mnuI_Small,
            this.mnuI_List,
            this.mnuI_Detail,
            this.mnuI_card,
            this.mnuPrintCard,
            this.mmuBedChange});
            this.mnuSet.Name = "contextMenuStrip1";
            this.mnuSet.Size = new System.Drawing.Size(153, 180);
            // 
            // mnuI_Large
            // 
            this.mnuI_Large.Name = "mnuI_Large";
            this.mnuI_Large.Size = new System.Drawing.Size(152, 22);
            this.mnuI_Large.Text = "大图标";
            this.mnuI_Large.Click += new System.EventHandler(this.mnuI_Large_Click);
            // 
            // mnuI_Small
            // 
            this.mnuI_Small.Name = "mnuI_Small";
            this.mnuI_Small.Size = new System.Drawing.Size(152, 22);
            this.mnuI_Small.Text = "小图标";
            this.mnuI_Small.Click += new System.EventHandler(this.mnuI_Small_Click);
            // 
            // mnuI_List
            // 
            this.mnuI_List.Name = "mnuI_List";
            this.mnuI_List.Size = new System.Drawing.Size(152, 22);
            this.mnuI_List.Text = "列表";
            this.mnuI_List.Click += new System.EventHandler(this.mnuI_List_Click);
            // 
            // mnuI_Detail
            // 
            this.mnuI_Detail.Name = "mnuI_Detail";
            this.mnuI_Detail.Size = new System.Drawing.Size(152, 22);
            this.mnuI_Detail.Text = "详细资料";
            this.mnuI_Detail.Click += new System.EventHandler(this.mnuI_Detail_Click);
            // 
            // mnuI_card
            // 
            this.mnuI_card.Name = "mnuI_card";
            this.mnuI_card.Size = new System.Drawing.Size(152, 22);
            this.mnuI_card.Text = "卡片格式";
            this.mnuI_card.Click += new System.EventHandler(this.mnuI_card_Click);
            // 
            // mnuPrintCard
            // 
            this.mnuPrintCard.Name = "mnuPrintCard";
            this.mnuPrintCard.Size = new System.Drawing.Size(152, 22);
            this.mnuPrintCard.Text = "打印卡片";
            this.mnuPrintCard.Click += new System.EventHandler(this.mnuPrintCard_Click);
            // 
            // mnuBed
            // 
            this.mnuBed.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuI_Info,
            this.mnuI_Wap,
            this.mnu_ChangeBed});
            this.mnuBed.Name = "mnuBed";
            this.mnuBed.Size = new System.Drawing.Size(125, 70);
            // 
            // mnuI_Info
            // 
            this.mnuI_Info.Name = "mnuI_Info";
            this.mnuI_Info.Size = new System.Drawing.Size(124, 22);
            this.mnuI_Info.Text = "患者信息";
            this.mnuI_Info.Click += new System.EventHandler(this.mnuI_Info_Click);
            // 
            // mnuI_Wap
            // 
            this.mnuI_Wap.Name = "mnuI_Wap";
            this.mnuI_Wap.Size = new System.Drawing.Size(124, 22);
            this.mnuI_Wap.Text = "包床";
            this.mnuI_Wap.Click += new System.EventHandler(this.mnuI_Wap_Click);
            // 
            // mnu_ChangeBed
            // 
            this.mnu_ChangeBed.Name = "mnu_ChangeBed";
            this.mnu_ChangeBed.Size = new System.Drawing.Size(124, 22);
            this.mnu_ChangeBed.Text = "换床";
            this.mnu_ChangeBed.Click += new System.EventHandler(this.mnu_ChangeBed_Click);
            // 
            // mnuI_Add
            // 
            this.mnuI_Add.Name = "mnuI_Add";
            this.mnuI_Add.Size = new System.Drawing.Size(118, 22);
            this.mnuI_Add.Text = "设置为病重";
            this.mnuI_Add.Click += new System.EventHandler(this.mnuI_Add_Click);
            // 
            // mnuI_Cancel
            // 
            this.mnuI_Cancel.Name = "mnuI_Cancel";
            this.mnuI_Cancel.Size = new System.Drawing.Size(118, 22);
            this.mnuI_Cancel.Text = "取消病重标记";
            this.mnuI_Cancel.Click += new System.EventHandler(this.mnuI_Cancel_Click);
            // 
            // mnuDealBed
            // 
            this.mnuDealBed.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuI_Unwap});
            this.mnuDealBed.Name = "mnuDealBed";
            this.mnuDealBed.Size = new System.Drawing.Size(101, 26);
            // 
            // mnuI_Unwap
            // 
            this.mnuI_Unwap.Name = "mnuI_Unwap";
            this.mnuI_Unwap.Size = new System.Drawing.Size(100, 22);
            this.mnuI_Unwap.Text = "解包";
            this.mnuI_Unwap.Click += new System.EventHandler(this.mnuI_Unwap_Click);
            // 
            // mnuReceive
            // 
            this.mnuReceive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuI_Arrange});
            this.mnuReceive.Name = "mnuReceive";
            this.mnuReceive.Size = new System.Drawing.Size(125, 26);
            // 
            // mnuI_Arrange
            // 
            this.mnuI_Arrange.Name = "mnuI_Arrange";
            this.mnuI_Arrange.Size = new System.Drawing.Size(124, 22);
            this.mnuI_Arrange.Text = "分配床位";
            this.mnuI_Arrange.Click += new System.EventHandler(this.mnuI_Arrange_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBedSumInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1100, 56);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblBedSumInfo
            // 
            this.lblBedSumInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedSumInfo.Location = new System.Drawing.Point(6, 8);
            this.lblBedSumInfo.Name = "lblBedSumInfo";
            this.lblBedSumInfo.Size = new System.Drawing.Size(582, 46);
            this.lblBedSumInfo.TabIndex = 0;
            this.lblBedSumInfo.Text = "床位信息、病床总数：111   占用床位数：100   加床数：111   床位占用率：111.11%\r\n床位使用率";
            this.lblBedSumInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lsvBedView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 410);
            this.panel1.TabIndex = 5;
            // 
            // lsvBedView
            // 
            this.lsvBedView.AllowDrop = true;
            this.lsvBedView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvBedView.HideSelection = false;
            this.lsvBedView.LargeImageList = this.imageList1;
            this.lsvBedView.Location = new System.Drawing.Point(0, 0);
            this.lsvBedView.Name = "lsvBedView";
            this.lsvBedView.ShowItemToolTips = true;
            this.lsvBedView.Size = new System.Drawing.Size(1100, 410);
            this.lsvBedView.SmallImageList = this.imageList1;
            this.lsvBedView.StateImageList = this.imageList1;
            this.lsvBedView.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lsvBedView.TabIndex = 1;
            this.lsvBedView.UseCompatibleStateImageBehavior = false;
            this.lsvBedView.ItemActivate += new System.EventHandler(this.lsvBedView_ItemActivate);
            this.lsvBedView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lsvBedView_MouseUp);
            this.lsvBedView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lsvBedView_ItemSelectionChanged);
            this.lsvBedView.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsvBedView_DragEnter);
            this.lsvBedView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lsvBedView_ItemDrag);
            this.lsvBedView.DragOver += new System.Windows.Forms.DragEventHandler(this.lsvBedView_DragOver);
            // 
            // mmuBedChange
            // 
            this.mmuBedChange.Name = "mmuBedChange";
            this.mmuBedChange.Size = new System.Drawing.Size(152, 22);
            this.mmuBedChange.Text = "换床";
            this.mmuBedChange.Click += new System.EventHandler(this.mnu_BedChange_Click);
            // 
            // ucBedListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucBedListView";
            this.Size = new System.Drawing.Size(1100, 466);
            this.mnuSet.ResumeLayout(false);
            this.mnuBed.ResumeLayout(false);
            this.mnuDealBed.ResumeLayout(false);
            this.mnuReceive.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip mnuSet;
        private System.Windows.Forms.ToolStripMenuItem mnuI_Large;
        private System.Windows.Forms.ToolStripMenuItem mnuI_Small;
        private System.Windows.Forms.ToolStripMenuItem mnuI_List;
        private System.Windows.Forms.ToolStripMenuItem mnuI_Detail;
        private System.Windows.Forms.ToolStripMenuItem mnuI_card;
        private System.Windows.Forms.ContextMenuStrip mnuBed;
        private System.Windows.Forms.ContextMenuStrip mnuDealBed;
        private System.Windows.Forms.ToolStripMenuItem mnuI_Unwap;
        private System.Windows.Forms.ToolStripMenuItem mnuI_Info;
        private System.Windows.Forms.ToolStripMenuItem mnuI_Wap;
        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
        private System.Windows.Forms.ToolStripMenuItem mnuI_Add;
        private System.Windows.Forms.ToolStripMenuItem mnuI_Cancel;
        private System.Windows.Forms.ToolStripMenuItem mnu_ChangeBed;
        private System.Windows.Forms.ToolStripMenuItem mnuPrintCard;
        //{C29C2D37-D519-49af-AFEA-4981B1A013AE}
        private System.Windows.Forms.ContextMenuStrip mnuReceive;
        private System.Windows.Forms.ToolStripMenuItem mnuI_Arrange;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBedSumInfo;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuListView lsvBedView;
        private System.Windows.Forms.ToolStripMenuItem mmuBedChange;
    }
}
