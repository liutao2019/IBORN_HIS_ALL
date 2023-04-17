using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Notice
{
	/// <summary>
	/// ucNotice 的摘要说明。
	/// </summary>
	public class ucNotice : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.RichTextBox rtbNotice;
		private System.Windows.Forms.Panel panelTop;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Label label3;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbNoticeDept;
		private System.Windows.Forms.Label label4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSysGroup;
		private System.Windows.Forms.CheckBox ckDeptAll;
		private System.Windows.Forms.CheckBox ckGroupAll;
		public System.Windows.Forms.DateTimePicker dtNoticeDate;
        private Label lblOper;
        private Label label5;
		private System.ComponentModel.IContainer components;

		public ucNotice()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			
			this.Load += new EventHandler(ucNotice_Load);
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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.rtbNotice = new System.Windows.Forms.RichTextBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.ckDeptAll = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbSysGroup = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.ckGroupAll = new System.Windows.Forms.CheckBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblOper = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtNoticeDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbNoticeDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbNotice
            // 
            this.rtbNotice.AcceptsTab = true;
            this.rtbNotice.BackColor = System.Drawing.Color.White;
            this.rtbNotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbNotice.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbNotice.Location = new System.Drawing.Point(0, 32);
            this.rtbNotice.Name = "rtbNotice";
            this.rtbNotice.Size = new System.Drawing.Size(635, 323);
            this.rtbNotice.TabIndex = 0;
            this.rtbNotice.Text = "";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.ckDeptAll);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.cmbDept);
            this.panelTop.Controls.Add(this.cmbSysGroup);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.ckGroupAll);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(635, 32);
            this.panelTop.TabIndex = 1;
            // 
            // ckDeptAll
            // 
            this.ckDeptAll.Location = new System.Drawing.Point(211, 6);
            this.ckDeptAll.Name = "ckDeptAll";
            this.ckDeptAll.Size = new System.Drawing.Size(74, 22);
            this.ckDeptAll.TabIndex = 2;
            this.ckDeptAll.Text = "全部科室";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "接收科室：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.IsShowIDAndName = false;
            this.cmbDept.Location = new System.Drawing.Point(72, 6);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(126, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 0;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // cmbSysGroup
            // 
            this.cmbSysGroup.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSysGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSysGroup.IsEnter2Tab = false;
            this.cmbSysGroup.IsFlat = false;
            this.cmbSysGroup.IsLike = true;
            this.cmbSysGroup.IsListOnly = false;
            this.cmbSysGroup.IsPopForm = true;
            this.cmbSysGroup.IsShowCustomerList = false;
            this.cmbSysGroup.IsShowID = false;
            this.cmbSysGroup.IsShowIDAndName = false;
            this.cmbSysGroup.Location = new System.Drawing.Point(405, 6);
            this.cmbSysGroup.Name = "cmbSysGroup";
            this.cmbSysGroup.ShowCustomerList = false;
            this.cmbSysGroup.ShowID = false;
            this.cmbSysGroup.Size = new System.Drawing.Size(137, 20);
            this.cmbSysGroup.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSysGroup.TabIndex = 0;
            this.cmbSysGroup.Tag = "";
            this.cmbSysGroup.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(322, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "接收功能组：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckGroupAll
            // 
            this.ckGroupAll.Location = new System.Drawing.Point(549, 5);
            this.ckGroupAll.Name = "ckGroupAll";
            this.ckGroupAll.Size = new System.Drawing.Size(85, 22);
            this.ckGroupAll.TabIndex = 2;
            this.ckGroupAll.Text = "全部功能组";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblOper);
            this.panelBottom.Controls.Add(this.label5);
            this.panelBottom.Controls.Add(this.dtNoticeDate);
            this.panelBottom.Controls.Add(this.label3);
            this.panelBottom.Controls.Add(this.cmbNoticeDept);
            this.panelBottom.Controls.Add(this.label4);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 355);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(635, 33);
            this.panelBottom.TabIndex = 2;
            // 
            // lblOper
            // 
            this.lblOper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOper.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOper.Location = new System.Drawing.Point(478, 5);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(84, 22);
            this.lblOper.TabIndex = 6;
            this.lblOper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(423, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 22);
            this.label5.TabIndex = 5;
            this.label5.Text = "发布人：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtNoticeDate
            // 
            this.dtNoticeDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNoticeDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtNoticeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNoticeDate.Location = new System.Drawing.Point(280, 3);
            this.dtNoticeDate.Name = "dtNoticeDate";
            this.dtNoticeDate.Size = new System.Drawing.Size(137, 21);
            this.dtNoticeDate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(6, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "发布科室：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbNoticeDept
            // 
            this.cmbNoticeDept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbNoticeDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbNoticeDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbNoticeDept.IsEnter2Tab = false;
            this.cmbNoticeDept.IsFlat = false;
            this.cmbNoticeDept.IsLike = true;
            this.cmbNoticeDept.IsListOnly = false;
            this.cmbNoticeDept.IsPopForm = true;
            this.cmbNoticeDept.IsShowCustomerList = false;
            this.cmbNoticeDept.IsShowID = false;
            this.cmbNoticeDept.IsShowIDAndName = false;
            this.cmbNoticeDept.Location = new System.Drawing.Point(72, 4);
            this.cmbNoticeDept.Name = "cmbNoticeDept";
            this.cmbNoticeDept.ShowCustomerList = false;
            this.cmbNoticeDept.ShowID = false;
            this.cmbNoticeDept.Size = new System.Drawing.Size(126, 20);
            this.cmbNoticeDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbNoticeDept.TabIndex = 2;
            this.cmbNoticeDept.Tag = "";
            this.cmbNoticeDept.ToolBarUse = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(209, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "发布日期：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucNotice
            // 
            this.Controls.Add(this.rtbNotice);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "ucNotice";
            this.Size = new System.Drawing.Size(635, 388);
            this.panelTop.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 发布信息管理类
		/// </summary>
        FS.HISFC.BizLogic.Manager.Notice noticeManager = new FS.HISFC.BizLogic.Manager.Notice();

        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

		/// <summary>
		/// 发布信息实体
		/// </summary>
        private FS.HISFC.Models.Base.Notice notice = null;
		/// <summary>
		/// 本次操作的发布信息
		/// </summary>
        public FS.HISFC.Models.Base.Notice Notice
		{
			get
			{
				if (this.notice == null)
					this.notice = new FS.HISFC.Models.Base.Notice();
				return this.notice;
			}
			set
			{
				if (value == null)
					value = new FS.HISFC.Models.Base.Notice();
				this.notice = value;
				this.ShowNotice(this.notice);
			}
		}

		/// <summary>
		/// 操作员信息
		/// </summary>
		private FS.HISFC.Models.Base.Employee oper = null;
		/// <summary>
		/// 操作员信息
		/// </summary>
		public FS.HISFC.Models.Base.Employee Oper
		{
			set
			{
				this.oper = value;
			}
		}

		/// <summary>
		/// 只用于显示
		/// </summary>
		public bool OnlyShow
		{
			set
			{
				this.panelTop.Visible = !value;
				this.panelBottom.Visible = !value;
				this.rtbNotice.ReadOnly = value;
			}
		}


		/// <summary>
		/// 数据初始化
		/// </summary>
		protected void Init()
		{
			#region 加载科室
			FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
			ArrayList alDept = deptManager.GetDeptmentAll();
			if (alDept == null)
			{
				MessageBox.Show("获取科室列表出错" + deptManager.Err);
				return;
			}
			this.cmbDept.AddItems(alDept);
			this.cmbNoticeDept.AddItems(alDept);
			#endregion

			#region 加载功能组
			FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();
            ArrayList alSysGroup = sysGroupManager.GetList();
            if (alSysGroup == null)
            {
                MessageBox.Show("获取功能组列表出错" + sysGroupManager.Err);
                return;
            }
            this.cmbSysGroup.AddItems(alSysGroup);
			#endregion
		}

		/// <summary>
		/// 清空显示
		/// </summary>
		public void Clear()
		{
			this.cmbDept.Tag = null;
			this.cmbDept.Text = "";
			this.ckDeptAll.Checked = false;
			this.cmbSysGroup.Tag = null;
			this.cmbSysGroup.Text = "";
			this.ckGroupAll.Checked = false;
			this.rtbNotice.Text = "";
			if (this.oper != null)
			{
				this.cmbNoticeDept.Tag = this.oper.Dept.ID;
				this.cmbNoticeDept.Text = this.oper.Dept.Name;
			}
		}
		/// <summary>
		/// 加载显示发布信息
		/// </summary>
		/// <param name="notice">发布信息实体</param>
		protected void ShowNotice(FS.HISFC.Models.Base.Notice notice)
		{
			if (notice == null)
				return;
			if (notice.ID == "")
				this.Clear();

			//不使用Rtf属性 保存后的字符串长度过大 
			this.rtbNotice.Text = notice.NoticeInfo;
			if (notice.Dept.ID == "AAAA")						//接收科室
			{
				this.ckDeptAll.Checked = true;
				this.cmbDept.Tag = null;
				this.cmbDept.Text = "";
			}
			else
			{
				if (notice.Dept.ID != "")
					this.cmbDept.Tag = notice.Dept.ID;
			}
			if (notice.Group.ID == "AAAA")						//接收功能组
			{
				this.ckGroupAll.Checked = true;
				this.cmbSysGroup.Tag = null;
				this.cmbSysGroup.Text = "";
			}
			else
			{
				if (notice.Group.ID != "")
					this.cmbSysGroup.Tag = notice.Group.ID;
			}
			this.cmbNoticeDept.Tag = notice.NoticeDept.ID;		//发布科室
			if (notice.NoticeDate != DateTime.MinValue)
                this.dtNoticeDate.Value = notice.NoticeDate;		//发布日期

            FS.HISFC.Models.Base.Employee employee = this.personManager.GetPersonByID(notice.OperEnvironment.ID);

            this.lblOper.Text = employee.Name;
		}

		protected int ValidSave()
		{
			if (!this.ckDeptAll.Checked && this.cmbDept.Text == "")
			{
				MessageBox.Show("请填写接收科室");
				return -1;
			}
			if (!this.ckGroupAll.Checked && this.cmbSysGroup.Text == "")
			{
				MessageBox.Show("请填写接收功能组");
				return -1;
			}
			if (this.rtbNotice.Text == "")
			{
				MessageBox.Show("请填写发布信息");
				return -1;
			}

            if (this.notice.ID.Length > 0 && this.notice.OperEnvironment.ID != this.noticeManager.Operator.ID && this.notice.OperEnvironment.ID.Length > 0)
            {
                MessageBox.Show("该信息不是由您发布，您无权修改！");
                return -1;
            }

			return 1;
		}
		/// <summary>
		/// 保存发布信息
		/// </summary>
		/// <returns>成功返回1 失败返回－1</returns>
		public int SaveNotice(string noticeTitle)
		{
			if (this.ValidSave() == -1)
				return -1;

			if (this.notice == null)
				this.notice = new FS.HISFC.Models.Base.Notice();

			//不使用Rtf属性 保存后的字符串长度过大 
			//如使用Rtf属性 需更改业务层参数赋值方式 不使用string.format 直接使用execnoquery(sql,parms..)
			this.notice.NoticeInfo = this.rtbNotice.Text;
			if (this.ckDeptAll.Checked)
				this.notice.Dept.ID = "AAAA";
			else
				this.notice.Dept.ID = this.cmbDept.Tag.ToString();
			if (this.ckGroupAll.Checked)
				this.notice.Group.ID = "AAAA";
			else
				this.notice.Group.ID = this.cmbSysGroup.Tag.ToString();
			this.notice.NoticeDept.ID = this.cmbNoticeDept.Tag.ToString();
			this.notice.NoticeDept.Name = this.cmbNoticeDept.Text;
			this.notice.NoticeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.dtNoticeDate.Text);
			this.notice.NoticeTitle = noticeTitle;
            this.notice.OperEnvironment.ID = this.noticeManager.Operator.ID;
            this.notice.OperEnvironment.OperTime = DateTime.Now;
            //{F12D903F-03C1-4fa1-BB78-72DE12FF062D}
            if (this.notice.ID == string.Empty)
            {
                this.notice.ID = noticeManager.GetSequence("Manager.Notice.GetIDSeq");

                if (this.notice.ID == null)
               {
                   MessageBox.Show("获取流水号失败");
                   return -1;
               }
            }
			
			if (noticeManager.SetNotice(this.notice) != 1)
			{
				MessageBox.Show("保存发布信息失败" + noticeManager.Err);
				return -1;
			}

			MessageBox.Show("保存成功");
			return 1;
		}

		/// <summary>
		/// 删除发布信息
		/// </summary>
		/// <returns>成功返回1 失败返回-1</returns>
		public int DelNotice()
        {	//{F12D903F-03C1-4fa1-BB78-72DE12FF062D}
			//if (this.notice != null)
            if (this.notice != null && this.notice.ID != string.Empty)
			{
				if (MessageBox.Show("确认进行删除操作吗?","",System.Windows.Forms.MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
                    if (this.notice.OperEnvironment.ID != this.noticeManager.Operator.ID && this.notice.OperEnvironment.ID.Length > 0)
                    {
                        MessageBox.Show("该信息不是由您发布，您无权删除！");
                        return -1;
                    }

					if (this.noticeManager.DeleteNotice(this.notice.ID) != 1)
					{
						MessageBox.Show("删除发布信息失败" + noticeManager.Err);
						return -1;
					}
					MessageBox.Show("删除成功");
					return 1;
				}
				else
				{
					return -1;
				}
			}
			return 0;
		}

		/// <summary>
		/// 打印
		/// </summary>
		public void Print()
		{
			if (this.rtbNotice.Text != "")
			{
				FS.FrameWork.WinForms.Classes.Print p=new FS.FrameWork.WinForms.Classes.Print();
				Control c = this;
				this.OnlyShow = true;
				p.PrintPreview(50,30,c);
				this.OnlyShow = false;
			}
		}

		private void ucNotice_Load(object sender, EventArgs e)
		{
            try
            {
                this.Init();
            }
            catch { }
		}


	}
}
