using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Notice
{
	/// <summary>
	/// frmNotice 的摘要说明。
	/// </summary>
	public class frmNoticeManager : FS.FrameWork.WinForms.Forms.BaseStatusBar
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ToolBarButton tbQuery;
		private System.Windows.Forms.ToolBarButton tbDel;
		private System.Windows.Forms.ToolBarButton tbSave;
		private System.Windows.Forms.ToolBarButton tbExit;
		private System.Windows.Forms.ToolBarButton tbPrint;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.Splitter splitter1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBarButton tbAdd;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ToolBarButton tbbDate;
		private Manager.Notice.ucNotice ucNotice1;
        private FS.HISFC.Models.Base.Employee var;

		public frmNoticeManager()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			this.Load += new EventHandler(frmNoticeManager_Load);
			this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
			this.toolBar1.ButtonClick += new ToolBarButtonClickEventHandler(toolBar1_ButtonClick);
			this.treeView1.AfterLabelEdit += new NodeLabelEditEventHandler(treeView1_AfterLabelEdit);

			this.Init();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNoticeManager));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbbDate = new System.Windows.Forms.ToolBarButton();
            this.tbQuery = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbAdd = new System.Windows.Forms.ToolBarButton();
            this.tbDel = new System.Windows.Forms.ToolBarButton();
            this.tbSave = new System.Windows.Forms.ToolBarButton();
            this.tbPrint = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.tbExit = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ucNotice1 = new FS.HISFC.Components.Manager.Notice.ucNotice();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 736);
            this.statusBar1.Size = new System.Drawing.Size(1138, 24);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbbDate,
            this.tbQuery,
            this.toolBarButton1,
            this.tbAdd,
            this.tbDel,
            this.tbSave,
            this.tbPrint,
            this.toolBarButton2,
            this.tbExit});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(1138, 58);
            this.toolBar1.TabIndex = 1;
            // 
            // tbbDate
            // 
            this.tbbDate.ImageIndex = 14;
            this.tbbDate.Name = "tbbDate";
            this.tbbDate.Text = "时间";
            // 
            // tbQuery
            // 
            this.tbQuery.ImageIndex = 9;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Text = "查询";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbAdd
            // 
            this.tbAdd.ImageIndex = 8;
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Text = "增加";
            // 
            // tbDel
            // 
            this.tbDel.ImageIndex = 11;
            this.tbDel.Name = "tbDel";
            this.tbDel.Text = "删除";
            // 
            // tbSave
            // 
            this.tbSave.ImageIndex = 13;
            this.tbSave.Name = "tbSave";
            this.tbSave.Text = "保存";
            // 
            // tbPrint
            // 
            this.tbPrint.ImageIndex = 10;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Text = "打印";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbExit
            // 
            this.tbExit.ImageIndex = 12;
            this.tbExit.Name = "tbExit";
            this.tbExit.Text = "退   出";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "增加.GIF");
            this.imageList1.Images.SetKeyName(9, "查询.GIF");
            this.imageList1.Images.SetKeyName(10, "打印.GIF");
            this.imageList1.Images.SetKeyName(11, "删除.GIF");
            this.imageList1.Images.SetKeyName(12, "退出.GIF");
            this.imageList1.Images.SetKeyName(13, "保存.GIF");
            this.imageList1.Images.SetKeyName(14, "安排.GIF");
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList2;
            this.treeView1.Location = new System.Drawing.Point(0, 58);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(356, 678);
            this.treeView1.TabIndex = 2;
            this.treeView1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(156, 58);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 678);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            
            // 
            // ucNotice1
            // 
            this.ucNotice1.BackColor = System.Drawing.SystemColors.Control;
            this.ucNotice1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNotice1.Location = new System.Drawing.Point(159, 58);
            this.ucNotice1.Name = "ucNotice1";
            this.ucNotice1.Notice = ((FS.HISFC.Models.Base.Notice)(resources.GetObject("ucNotice1.Notice")));
            this.ucNotice1.Size = new System.Drawing.Size(979, 678);
            this.ucNotice1.TabIndex = 4;
            // 
            // frmNoticeManager
            // 
            this.ClientSize = new System.Drawing.Size(1138, 760);
            this.Controls.Add(this.ucNotice1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolBar1);
            this.Name = "frmNoticeManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "信息发布";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.toolBar1, 0);
            this.Controls.SetChildIndex(this.statusBar1, 0);
            this.Controls.SetChildIndex(this.treeView1, 0);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.Controls.SetChildIndex(this.ucNotice1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// 发布信息管理类
		/// </summary>
		FS.HISFC.BizLogic.Manager.Notice noticeManager = new FS.HISFC.BizLogic.Manager.Notice();
		/// <summary>
		/// 查询起始时间
		/// </summary>
		DateTime myBeginTime;
		/// <summary>
		/// 查询终止时间
		/// </summary>
		DateTime myEndTime;
		/// <summary>
		/// 最后一次已读信息的发布时间
		/// </summary>
		DateTime readDate;

		private bool onlyShow = false;
		/// <summary>
		/// 发布信息显示时使用
		/// </summary>
		public bool OnlyShow
		{
			set
			{
				if (value)
				{			
					this.WindowState = System.Windows.Forms.FormWindowState.Normal;
				}
				else
				{
					this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
				}
				this.onlyShow = value;
				this.ucNotice1.OnlyShow = value;
				this.SetTooBar(value);
			}
		}

		/// <summary>
		/// 信息显示模式 0 只显示新信息 1 显示时间段内全部信息
		/// </summary>
		private string noticeMode = "0";
		/// <summary>
		/// 信息显示模式 0 只显示新信息 1 显示时间段内全部信息
		/// </summary>
		public string NoticeMode
		{
			set
			{
				this.noticeMode = value;
			}
		}

		/// <summary>
		/// 需显示的发布信息
		/// </summary>
		private ArrayList noticeInfo;
		/// <summary>
		/// 需显示的发布信息
		/// </summary>
		public ArrayList NoticeInfo
		{
			get
			{
				if (this.noticeInfo == null)
					this.noticeInfo = new ArrayList();
				return this.noticeInfo;
			}
			set
			{
				this.noticeInfo = value;
			}
		}


		protected void Init()
		{
            var = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
			if (this.ucNotice1 != null)
				this.ucNotice1.Oper = var;

			//定义时间段默认显示区间,取系统日期
			FS.FrameWork.Management.DataBaseManger dataBase = new FS.FrameWork.Management.DataBaseManger();
			this.myEndTime = this.noticeManager.GetDateTimeFromSysDateTime();
			this.myBeginTime = this.myEndTime.AddDays(-30);

		
		}
		/// <summary>
		/// 加载数据显示
		/// </summary>
		/// <param name="alAddInfo">附加显示信息</param>
		public void ShowData(ArrayList alAddInfo)
		{
			this.GetSetting();
			this.ShowList(alAddInfo);
		}
		/// <summary>
		/// 加载列表显示
		/// </summary>
		/// <param name="alAddInfo">附加显示信息</param>
		protected void ShowList(ArrayList alAddInfo)
		{
			ArrayList al = new ArrayList();
			if (this.onlyShow)
			{
				al = noticeManager.GetNotice(var.Dept.ID,var.CurrentGroup.ID,this.myBeginTime,this.myEndTime);
			}
			else
			{
				al = noticeManager.GetNotice("AAAA",this.myBeginTime,this.myEndTime);
			}
			if (al == null)
			{
				MessageBox.Show("获取本月内已发布信息列表出错" + noticeManager.Err);
				return;
			}
			this.treeView1.Nodes.Clear();

			TreeNode root = new TreeNode("发布信息列表");
			root.SelectedImageIndex = 0;
			root.ImageIndex = 0;
			root.Tag = null;

			ArrayList alTemp = new ArrayList();
//			this.treeView1.Nodes.Add(root);
			TreeNode node;
			DateTime tempReadDate = DateTime.MinValue;
			FS.HISFC.Models.Base.Notice info = new FS.HISFC.Models.Base.Notice();
			for(int i = 0;i <al.Count;i++)
			{
				info = al[i] as FS.HISFC.Models.Base.Notice;
				node = new TreeNode();
				node.Text = "("+info.NoticeDate.ToShortDateString()+")"+info.NoticeTitle;
				node.ImageIndex = 1;
				node.SelectedImageIndex = 2;
				node.Tag = info;
				if (this.onlyShow && this.noticeMode == "0")		//显示模式为只显示新信息
				{
					if (info.NoticeDate <= this.readDate)
						continue;
					tempReadDate = info.NoticeDate;
				}
//				root.Nodes.Add(node);
				this.treeView1.Nodes.Add(node);
				alTemp.Add(info);
			}
			
			if (this.onlyShow && this.noticeMode == "0")		//显示模式为只显示新信息
			{
				this.readDate = tempReadDate;
			}
			if (alAddInfo != null && alAddInfo.Count > 0)
			{				
				FS.HISFC.Models.Base.Notice tempInfo;
				for(int j = 0;j < alAddInfo.Count;j++)
				{
					tempInfo = alAddInfo[j] as FS.HISFC.Models.Base.Notice;
					if (tempInfo != null)
					{
						alTemp.Add(tempInfo);
						node = new TreeNode();
						node.Text = tempInfo.NoticeTitle;
						node.ImageIndex = 1;
						node.SelectedImageIndex = 2;
						node.Tag = tempInfo;
						//						root.Nodes.Add(node);
						this.treeView1.Nodes.Add(node);
					}
				}
			}
			this.noticeInfo = alTemp;

			this.treeView1.ExpandAll();
			if (this.treeView1.Nodes.Count > 0)
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
			
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void New() 
		{
			TreeNode node = new TreeNode();

			node.Text = "新信息";
			node.Tag = "";
			node.ImageIndex = 1;
			node.SelectedImageIndex = 1;

			this.treeView1.Nodes.Add(node);
			this.treeView1.SelectedNode = node;

            this.ucNotice1.dtNoticeDate.Value = this.noticeManager.GetDateTimeFromSysDateTime();            
			node.BeginEdit();

		}

		/// <summary>
		/// 获取本地配置文件设置信息
		/// </summary>
		protected void GetSetting()
		{
			string strErr = "";
			ArrayList alValues = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("NoticeSetting",out strErr);
			if (alValues == null)
			{
				MessageBox.Show(strErr);
			}
			bool isCheck = false;
			if (alValues.Count > 0)
			{
				isCheck = FS.FrameWork.Function.NConvert.ToBoolean(alValues[0]);
				this.readDate = FS.FrameWork.Function.NConvert.ToDateTime(alValues[1]);
			}
		}

		/// <summary>
		/// 设置工具栏显示
		/// </summary>
		private void SetTooBar(bool onlyShow)
		{
			this.tbAdd.Visible = !onlyShow;
			this.tbDel.Visible = !onlyShow;
			this.tbSave.Visible = !onlyShow;
		}


		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag != null)
				this.ucNotice1.Notice = e.Node.Tag as FS.HISFC.Models.Base.Notice;
			else
				this.ucNotice1.Clear();
		}

		private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == this.tbExit)
			{
				if (this.onlyShow)
					FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("NoticeSetting","True",this.readDate.ToString());
				this.Close();
				return;
			}
			if (e.Button == this.tbQuery)
			{
				this.ShowList(null);
				return;
			}
			if (e.Button == this.tbSave)
			{
				if (this.ucNotice1.SaveNotice(this.treeView1.SelectedNode.Text) == 1)
				{				
					this.treeView1.SelectedNode.Tag = this.ucNotice1.Notice;
					this.treeView1.SelectedNode = this.treeView1.Nodes[0];
				}
				return;
			}
			if (e.Button == this.tbDel)
			{
                int returnValue = this.ucNotice1.DelNotice();

                //if (this.ucNotice1.DelNotice() == 1)
                //{F12D903F-03C1-4fa1-BB78-72DE12FF062D}
                if (returnValue == 0 || returnValue== 1)
				{
					this.treeView1.SelectedNode.Remove();
                    //{F12D903F-03C1-4fa1-BB78-72DE12FF062D}
                    if (this.treeView1.Nodes.Count > 0)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                    }
				}
				return;
			}
			if (e.Button == this.tbPrint)
			{
				this.ucNotice1.Print();
				return;
			}
			if (e.Button == this.tbAdd)
			{
				this.New();
				return;
			}
			if (e.Button == this.tbbDate)
			{
				//选择时间段，如果没有选择就返回
				if(FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref this.myBeginTime, ref this.myEndTime)==0) 
					return;
				this.ShowList(null);
			}
		}

		private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Label != null) 
			{
				if (e.Label.Length > 0) 
				{
					if (e.Label.IndexOfAny(new char[]{'@', '.', ',', '!'}) == -1) 
					{
						e.Node.EndEdit(false);
					}
					else 
					{
						e.CancelEdit = true;
						MessageBox.Show("存在无效字符!请重新命名");
						e.Node.BeginEdit();
					}
				}
				else 
				{
					e.CancelEdit = true;
					MessageBox.Show("模板名称不能为空");
					e.Node.BeginEdit();
				}
			}
		}

		private void frmNoticeManager_Load(object sender, EventArgs e)
		{
			if (this.Tag != null && this.Tag.ToString() == "1")
				this.OnlyShow = true;
			if (!this.onlyShow)
			{
				this.ShowData(null);
			}

            this.WindowState = FormWindowState.Minimized;

            this.WindowState = FormWindowState.Maximized;

			this.treeView1.LabelEdit = true;

            
		}
	}
}
