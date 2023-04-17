using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FS.SOC.HISFC.Components.DCP.CancerReport
{
	/// <summary>
	/// frmReportRegistor ��ժҪ˵����
	/// </summary>
	public class frmReportCancerRegister : FS.FrameWork.WinForms.Forms.BaseStatusBar
	{  
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ToolBarButton tbbSearch;
		private System.Windows.Forms.ToolBarButton tbbSave;
		private System.Windows.Forms.ToolBarButton tbbModify;
		private System.Windows.Forms.ToolBarButton tbbClear;
		private System.Windows.Forms.ToolBarButton tbbColor;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton tbbExit;
		private System.Windows.Forms.ToolBarButton tbbPrint;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lbPatientNO;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtInPatienNo;
		private System.Windows.Forms.TreeView tvPatientInfo;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ToolBarButton tbbNew;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton tbbDel;
		private System.Windows.Forms.ToolBarButton tbbEligible;
		private System.Windows.Forms.ToolBarButton tbbUneligible;
		private System.Windows.Forms.ToolBarButton tbbCancel;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolBarButton tbbTime;
		private System.Windows.Forms.ToolBarButton tbbRenew;
		private System.Windows.Forms.TextBox txtReportNo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDoctor;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolBarButton tbbRecover;
		private System.Windows.Forms.ContextMenu MenuForSearch;
		private System.Windows.Forms.MenuItem menuPatientBaseInfo;
		private System.Windows.Forms.MenuItem menuPatientReportInfo;
		private System.Windows.Forms.MenuItem menuDeptReportInfo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbQueryContent;
		private System.Windows.Forms.DateTimePicker dtBegin;
		private System.Windows.Forms.DateTimePicker dtEnd;
		private System.Windows.Forms.LinkLabel llbPatienNO;
		private System.Windows.Forms.ToolBarButton tbbHelp;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton tbbDesigner;
		private System.Windows.Forms.ContextMenu contextMenuNew;
		private System.Windows.Forms.MenuItem menuItemNew;
		private System.Windows.Forms.TextBox txtAddress;
        private FS.SOC.HISFC.Components.DCP.CancerReport.ucReportCancerRegister ucReportCancerRegister1;
		private System.ComponentModel.IContainer components;

		#region ���캯��
		public frmReportCancerRegister()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
			this.ProgressRun(true);
			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		public frmReportCancerRegister(FS.HISFC.Models.RADT.PatientInfo patientInfo)
		{
			InitializeComponent();
			this.ProgressRun(true);
			this.State = "A";
			this.patientInfo = patientInfo;
			
		}

		public frmReportCancerRegister( FS.HISFC.Models.Registration.Register r, string infectCode)
		{
			InitializeComponent();
			this.ProgressRun(true);

			this.carNO = r.PID.CardNO;
			this.clinicNo = r.ID;
			//this.infectCode = infectCode;
			try
			{
				this.State = "A";
				this.patient = (FS.HISFC.Models.RADT.Patient)r;
				this.patient.User03 = infectCode;
			}
			catch
			{}
		}

		#endregion

		/// <summary>
		/// ������������ʹ�õ���Դ��
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


		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmReportCancerRegister));
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.tbbDesigner = new System.Windows.Forms.ToolBarButton();
			this.contextMenuNew = new System.Windows.Forms.ContextMenu();
			this.menuItemNew = new System.Windows.Forms.MenuItem();
			this.tbbSave = new System.Windows.Forms.ToolBarButton();
			this.tbbDel = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.tbbNew = new System.Windows.Forms.ToolBarButton();
			this.tbbModify = new System.Windows.Forms.ToolBarButton();
			this.tbbRenew = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.tbbSearch = new System.Windows.Forms.ToolBarButton();
			this.MenuForSearch = new System.Windows.Forms.ContextMenu();
			this.menuPatientBaseInfo = new System.Windows.Forms.MenuItem();
			this.menuPatientReportInfo = new System.Windows.Forms.MenuItem();
			this.menuDeptReportInfo = new System.Windows.Forms.MenuItem();
			this.tbbTime = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.tbbEligible = new System.Windows.Forms.ToolBarButton();
			this.tbbUneligible = new System.Windows.Forms.ToolBarButton();
			this.tbbRecover = new System.Windows.Forms.ToolBarButton();
			this.tbbCancel = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.tbbClear = new System.Windows.Forms.ToolBarButton();
			this.tbbColor = new System.Windows.Forms.ToolBarButton();
			this.tbbPrint = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.tbbHelp = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.tbbExit = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
            this.ucReportCancerRegister1 = new FS.SOC.HISFC.Components.DCP.CancerReport.ucReportCancerRegister();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tvPatientInfo = new System.Windows.Forms.TreeView();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtInPatienNo = new System.Windows.Forms.TextBox();
			this.llbPatienNO = new System.Windows.Forms.LinkLabel();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.dtEnd = new System.Windows.Forms.DateTimePicker();
			this.dtBegin = new System.Windows.Forms.DateTimePicker();
			this.cmbQueryContent = new System.Windows.Forms.ComboBox();
			this.txtDoctor = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtReportNo = new System.Windows.Forms.TextBox();
			this.lbPatientNO = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 722);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(1028, 24);
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.tbbDesigner,
																						this.tbbSave,
																						this.tbbDel,
																						this.toolBarButton3,
																						this.tbbNew,
																						this.tbbModify,
																						this.tbbRenew,
																						this.toolBarButton2,
																						this.tbbSearch,
																						this.tbbTime,
																						this.toolBarButton4,
																						this.tbbEligible,
																						this.tbbUneligible,
																						this.tbbRecover,
																						this.tbbCancel,
																						this.toolBarButton1,
																						this.tbbClear,
																						this.tbbColor,
																						this.tbbPrint,
																						this.toolBarButton6,
																						this.tbbHelp,
																						this.toolBarButton5,
																						this.tbbExit});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(1028, 41);
			this.toolBar1.TabIndex = 1;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// tbbDesigner
			// 
			this.tbbDesigner.DropDownMenu = this.contextMenuNew;
			this.tbbDesigner.Enabled = false;
			this.tbbDesigner.Text = "���";
			// 
			// contextMenuNew
			// 
			this.contextMenuNew.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.menuItemNew});
			// 
			// menuItemNew
			// 
			this.menuItemNew.Index = 0;
			this.menuItemNew.Text = "��������";
			this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
			// 
			// tbbSave
			// 
			this.tbbSave.ImageIndex = 0;
			this.tbbSave.Text = "���沢�ϱ�";
			this.tbbSave.ToolTipText = "���汨�����ϴ�������Ԥ���ơ������޸ĺ�Ĳ��ϸ񱨿��ָ�������״̬���ϴ�";
			// 
			// tbbDel
			// 
			this.tbbDel.ImageIndex = 2;
			this.tbbDel.Text = "ɾ������";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbNew
			// 
			this.tbbNew.DropDownMenu = this.contextMenuNew;
			this.tbbNew.ImageIndex = 4;
			this.tbbNew.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.tbbNew.Text = "�½�����";
			this.tbbNew.ToolTipText = "�½���Ⱦ�����濨";
			// 
			// tbbModify
			// 
			this.tbbModify.ImageIndex = 3;
			this.tbbModify.Text = "�޸ı���";
			this.tbbModify.ToolTipText = "���ϸ񱨿��޸�\\������Ϣ�޸�";
			// 
			// tbbRenew
			// 
			this.tbbRenew.ImageIndex = 12;
			this.tbbRenew.Text = "��������";
			this.tbbRenew.ToolTipText = "���Ʋ�����ȷ��\\��Ⱦ����ϱ��";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbSearch
			// 
			this.tbbSearch.DropDownMenu = this.MenuForSearch;
			this.tbbSearch.ImageIndex = 1;
			this.tbbSearch.Text = "��ѯ";
			this.tbbSearch.ToolTipText = "�ڱ��濨��ѯʱ���ڲ�ѯ�������б��濨";
			// 
			// MenuForSearch
			// 
			this.MenuForSearch.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.menuPatientBaseInfo,
																						  this.menuPatientReportInfo,
																						  this.menuDeptReportInfo});
			// 
			// menuPatientBaseInfo
			// 
			this.menuPatientBaseInfo.Index = 0;
			this.menuPatientBaseInfo.Text = "���߻�����Ϣ��ѯ";
			this.menuPatientBaseInfo.Click += new System.EventHandler(this.menuPatientBaseInfo_Click);
			// 
			// menuPatientReportInfo
			// 
			this.menuPatientReportInfo.Index = 1;
			this.menuPatientReportInfo.Text = "���߱�����Ϣ��ѯ";
			this.menuPatientReportInfo.Click += new System.EventHandler(this.menuPatientReportInfo_Click);
			// 
			// menuDeptReportInfo
			// 
			this.menuDeptReportInfo.Index = 2;
			this.menuDeptReportInfo.Text = "���ұ�����Ϣ��ѯ";
			this.menuDeptReportInfo.Click += new System.EventHandler(this.menuDeptReportInfo_Click);
			// 
			// tbbTime
			// 
			this.tbbTime.ImageIndex = 12;
			this.tbbTime.Text = "��ѯʱ������";
			this.tbbTime.Visible = false;
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbEligible
			// 
			this.tbbEligible.ImageIndex = 5;
			this.tbbEligible.Text = "�ϸ�";
			// 
			// tbbUneligible
			// 
			this.tbbUneligible.ImageIndex = 6;
			this.tbbUneligible.Text = "���ϸ�";
			// 
			// tbbRecover
			// 
			this.tbbRecover.ImageIndex = 11;
			this.tbbRecover.Text = "�ָ�";
			// 
			// tbbCancel
			// 
			this.tbbCancel.ImageIndex = 9;
			this.tbbCancel.Text = "���ϱ���";
			this.tbbCancel.ToolTipText = "�󱨿���ɾ��\\�ų���Ⱦ����ɾ��";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbClear
			// 
			this.tbbClear.ImageIndex = 10;
			this.tbbClear.Text = "���";
			this.tbbClear.ToolTipText = "��ձ��濨";
			// 
			// tbbColor
			// 
			this.tbbColor.ImageIndex = 11;
			this.tbbColor.Text = "��ɫ";
			this.tbbColor.Visible = false;
			// 
			// tbbPrint
			// 
			this.tbbPrint.ImageIndex = 14;
			this.tbbPrint.Text = "��ӡ";
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbHelp
			// 
			this.tbbHelp.ImageIndex = 15;
			this.tbbHelp.Text = "����";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbExit
			// 
			this.tbbExit.ImageIndex = 8;
			this.tbbExit.Text = "�˳�";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ucReportCancerRegister1);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.txtAddress);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 41);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1028, 705);
			this.panel1.TabIndex = 2;
			// 
			// ucReportCancerRegister1
			// 
			this.ucReportCancerRegister1.AutoScroll = true;
			this.ucReportCancerRegister1.BackColor = System.Drawing.Color.LightBlue;
			this.ucReportCancerRegister1.DiagNose = "";
			this.ucReportCancerRegister1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucReportCancerRegister1.InfectCode = null;
			this.ucReportCancerRegister1.IsAdvance = false;
			this.ucReportCancerRegister1.IsLisResult = false;
			this.ucReportCancerRegister1.IsRenew = false;
			this.ucReportCancerRegister1.Location = new System.Drawing.Point(216, 0);
			this.ucReportCancerRegister1.Name = "ucReportCancerRegister1";
			this.ucReportCancerRegister1.Size = new System.Drawing.Size(804, 681);
			this.ucReportCancerRegister1.State = null;
			this.ucReportCancerRegister1.TabIndex = 0;
			this.ucReportCancerRegister1.Type = null;
			// 
			// panel3
			// 
			this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel3.Location = new System.Drawing.Point(1020, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(8, 681);
			this.panel3.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.tvPatientInfo);
			this.panel2.Controls.Add(this.groupBox3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(216, 681);
			this.panel2.TabIndex = 1;
			// 
			// tvPatientInfo
			// 
			this.tvPatientInfo.BackColor = System.Drawing.Color.LightBlue;
			this.tvPatientInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvPatientInfo.ImageList = this.imageList2;
			this.tvPatientInfo.Location = new System.Drawing.Point(0, 240);
			this.tvPatientInfo.Name = "tvPatientInfo";
			this.tvPatientInfo.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																					  new System.Windows.Forms.TreeNode("�ѱ���", new System.Windows.Forms.TreeNode[] {
																																									   new System.Windows.Forms.TreeNode("����")})});
			this.tvPatientInfo.Size = new System.Drawing.Size(216, 441);
			this.tvPatientInfo.TabIndex = 7;
			// 
			// imageList2
			// 
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtInPatienNo);
			this.groupBox3.Controls.Add(this.llbPatienNO);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.dtEnd);
			this.groupBox3.Controls.Add(this.dtBegin);
			this.groupBox3.Controls.Add(this.cmbQueryContent);
			this.groupBox3.Controls.Add(this.txtDoctor);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.txtReportNo);
			this.groupBox3.Controls.Add(this.lbPatientNO);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.txtName);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(216, 240);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			// 
			// txtInPatienNo
			// 
			this.txtInPatienNo.Location = new System.Drawing.Point(64, 144);
			this.txtInPatienNo.Name = "txtInPatienNo";
			this.txtInPatienNo.Size = new System.Drawing.Size(144, 21);
			this.txtInPatienNo.TabIndex = 3;
			this.txtInPatienNo.Text = "";
			this.toolTip1.SetToolTip(this.txtInPatienNo, "ģ����ѯ���س�ȷ������");
			this.txtInPatienNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInPatienNo_KeyPress);
			// 
			// llbPatienNO
			// 
			this.llbPatienNO.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.llbPatienNO.Location = new System.Drawing.Point(10, 144);
			this.llbPatienNO.Name = "llbPatienNO";
			this.llbPatienNO.Size = new System.Drawing.Size(56, 23);
			this.llbPatienNO.TabIndex = 16;
			this.llbPatienNO.TabStop = true;
			this.llbPatienNO.Text = "ס Ժ ��";
			this.llbPatienNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.llbPatienNO.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbPatienNO_LinkClicked);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 21);
			this.label6.TabIndex = 15;
			this.label6.Text = "��ֹʱ��";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 21);
			this.label5.TabIndex = 14;
			this.label5.Text = "��ʼʱ��";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 20);
			this.label4.TabIndex = 13;
			this.label4.Text = "��ѯ����";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtEnd
			// 
			this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
			this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtEnd.Location = new System.Drawing.Point(64, 80);
			this.dtEnd.Name = "dtEnd";
			this.dtEnd.Size = new System.Drawing.Size(144, 21);
			this.dtEnd.TabIndex = 12;
			// 
			// dtBegin
			// 
			this.dtBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
			this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtBegin.Location = new System.Drawing.Point(64, 48);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(144, 21);
			this.dtBegin.TabIndex = 11;
			// 
			// cmbQueryContent
			// 
			this.cmbQueryContent.Location = new System.Drawing.Point(64, 16);
			this.cmbQueryContent.Name = "cmbQueryContent";
			this.cmbQueryContent.Size = new System.Drawing.Size(144, 20);
			this.cmbQueryContent.TabIndex = 10;
			// 
			// txtDoctor
			// 
			this.txtDoctor.Location = new System.Drawing.Point(64, 208);
			this.txtDoctor.Name = "txtDoctor";
			this.txtDoctor.Size = new System.Drawing.Size(144, 21);
			this.txtDoctor.TabIndex = 8;
			this.txtDoctor.Text = "";
			this.toolTip1.SetToolTip(this.txtDoctor, "����ҽ���Ĺ��ţ�ע���Сд���س�ȷ������");
			this.txtDoctor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDoctor_KeyPress);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 208);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "ҽ������";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtReportNo
			// 
			this.txtReportNo.Location = new System.Drawing.Point(64, 112);
			this.txtReportNo.Name = "txtReportNo";
			this.txtReportNo.Size = new System.Drawing.Size(144, 21);
			this.txtReportNo.TabIndex = 1;
			this.txtReportNo.Text = "";
			this.toolTip1.SetToolTip(this.txtReportNo, "���뱨�濨������ź�س�");
			this.txtReportNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReportNo_KeyPress);
			// 
			// lbPatientNO
			// 
			this.lbPatientNO.Location = new System.Drawing.Point(8, 112);
			this.lbPatientNO.Name = "lbPatientNO";
			this.lbPatientNO.Size = new System.Drawing.Size(56, 23);
			this.lbPatientNO.TabIndex = 0;
			this.lbPatientNO.Text = "���濨��";
			this.lbPatientNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 176);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "��������";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(64, 176);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(144, 21);
			this.txtName.TabIndex = 5;
			this.txtName.Text = "";
			this.toolTip1.SetToolTip(this.txtName, "ģ����ѯ���س�ȷ������");
			this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
			// 
			// groupBox1
			// 
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 681);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1028, 24);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// txtAddress
			// 
			this.txtAddress.ForeColor = System.Drawing.Color.Blue;
			this.txtAddress.Location = new System.Drawing.Point(240, 456);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(304, 21);
			this.txtAddress.TabIndex = 6;
			this.txtAddress.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.ForeColor = System.Drawing.Color.Black;
			this.groupBox2.Location = new System.Drawing.Point(0, 41);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(1028, 8);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			// 
			// frmReportCancerRegister
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1028, 746);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolBar1);
			this.KeyPreview = true;
			this.Name = "frmReportCancerRegister";
			this.Text = "��������";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.frmReportRegister_Resize);
			this.Load += new System.EventHandler(this.frmReportRegister_Load);
			this.Controls.SetChildIndex(this.toolBar1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.statusBar1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region ��������
		private FS.HISFC.Models.RADT.PatientInfo patientInfo;
        private FS.HISFC.Models.RADT.Patient patient = new  FS.HISFC.Models.RADT.Patient();
		private FS.SOC.HISFC.BizLogic.DCP.DiseaseReport myReport = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();
		private string BeginTime = "";
		private string EndTime = "";

        private FS.HISFC.Models.Base.Employee emplInfo = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.HISFC.Models.RADT.Patient Patient
		{
			get
			{
				return patient;
			}
			set
			{
				patient = value;
				if(State == "A")
				{
					this.ucReportCancerRegister1.InfectCode = value.User03;
    				this.ucReportCancerRegister1.ShowPatienInfo(value);
    				this.ucReportCancerRegister1.SetEnable(true);
				}
			}
		}
		//private string infectCode = "";

		private string clinicNo = "";
		/// <summary>
		/// ����Patient.ID��
		/// </summary>
		public string ClinicNo
		{
			get
			{
				return clinicNo;
			}
			set
			{
				if(value != null && value != "")
				{
					clinicNo = value;
				}
			}
		}
		/// <summary>
		/// ���ߵĿ���
		/// </summary>
		private string carNO = "";
		/// <summary>
		/// ���ߵ�סԺ��
		/// </summary>
		private string InpatientNO = "";
		/// <summary>
		/// ��������
		/// </summary>
		private string type;
		/// <summary>
		/// ����[IסԺ C���� O����]
		/// </summary>
		public string Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				this.lbPatientNO.Tag = type;
				this.ucReportCancerRegister1.Type = value;
			}
		}
		
		/// <summary>
		/// ����״̬
		/// </summary>
		private string state;
		/// <summary>
		/// ״̬ �����б�ΪA �����б�ΪB ѡ���˱�������Ϊ����״̬
		/// </summary>
		public string State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;			
				this.ucReportCancerRegister1.State = value;
			}
		}
	
		/// <summary>
		/// ���濨����[�����������,���]
		/// </summary>
		private ArrayList alReport = new ArrayList();
		/// <summary>
		/// ���濨[�ڱ��������ʱ����show֮ǰ��ֵ]
		/// </summary>
		public ArrayList AlReport
		{
			get
			{
				return alReport;
			}
			set
			{
				alReport = value;
			}
		}
	

		/// <summary>
		/// �Ƿ��������
		/// </summary>
		private bool isAllowApprove = false;

		/// <summary>
		/// �Ƿ�����ָ�
		/// </summary>
		private bool isAllowRecover = false;

		#endregion
		
		#region ����	
		/// <summary>
		/// ������ʷ����
		/// </summary>
		private void mySearchOldReport()
		{
			//��ȡ���ҵ���ʷ����
			this.query(enumQueryType.�����ұ�����ѯ.ToString());			
		}

		/// <summary>
		/// ��ʾ����״̬�ı��濨
		/// </summary>
		/// <param name="al"></param>
		private void showAllReport(ArrayList al)
		{
			this.tvPatientInfo.Nodes.Clear();
			if(al == null ||al.Count < 1)
			{
				return;
			}
			for(int state = 0; state <= 5; state++)
			{
				ArrayList altemp = new ArrayList();
				foreach(FS.HISFC.DCP.Object.CommonReport report in al)
				{
					if(report.State == state.ToString() )
					{
						altemp.Add(report);
					}
				}
				this.ShowReport(altemp);
			}
		}
		/// <summary>
		/// ��ʾ�����б�[��״̬���б������ӷּ��ڵ�]
		/// </summary>
		private void addAllOldReportInfo(ArrayList al, string state)
		{
			//����״̬������ʾ
			try
			{
				this.state = "B";
				System.Windows.Forms.TreeNode node = new TreeNode();
				int imagindex = 0;

				//���ڵ����� ��ʾ����״̬
				switch(state)
				{
					case "0":
						node.Text = "����";	
						break;
					case "1":
						node.Text = "�ϸ�";	
						imagindex = 1;
						break;
					case "2"://���
						node.Text = "���ϸ����޸ı�����";	
						node.ForeColor = System.Drawing.Color.Red;
						imagindex = 2;
						break;
					case "3":
						//						node.Text = "��������";
						//						imagindex = 5;
						//						break;
					case "4"://����
						node.Text = "����";//����������
						imagindex = 3;
						break;
					case "5"://���ϸ������޸��ϱ�
						node.Text = "���ϸ��޸ı���";//����������
						imagindex = 4;
						break;
					default:
						break;
				}

				node.Tag = "root";
				node.SelectedImageIndex = imagindex;
				node.ImageIndex = imagindex;
				this.tvPatientInfo.Nodes.Add(node);
				
				//�ӽڵ���� ��ʾ�������� ������
				foreach(FS.HISFC.DCP.Object.CommonReport report in al)
				{
					System.Windows.Forms.TreeNode reportnode = new TreeNode();
					reportnode.Tag = report;
					if(report.Patient.Name == null || report.Patient.Name == "")
					{
						reportnode.Text = report.PatientParents + "["+report.ReportNO+"]"+report.ExtendInfo3;
					}
					else
					{
						reportnode.Text = report.Patient.Name + "["+report.ReportNO+"]"+report.ExtendInfo3;;
					}
					reportnode.ImageIndex = 5;
					reportnode.SelectedImageIndex = 5;
					this.tvPatientInfo.Nodes[this.tvPatientInfo.Nodes.Count-1].Nodes.Add(reportnode);
					if(state == "2" && report.ReportDoctor.ID == FS.FrameWork.Management.Connection.Operator.ID)
					{
						MessageBox.Show(this,"����д��"+reportnode.Text+"���濨���ϸ���鿴[�˿�ԭ��]��������Ӧ�޸�","�˿�ԭ��"+report.OperCase);
					}
				}
				this.tvPatientInfo.ExpandAll();
			}
			catch(Exception ex)
			{
				MessageBox.Show(this,"������ʷ������Ϣʧ��"+ex.Message,"����>>");
			}
		}

		/// <summary>
		///  ��ʾ���濨[ͬ״̬�ı���]
		/// </summary>
		/// <param name="al">ͬ״̬�ı���</param>
		private void ShowReport(ArrayList al)
		{
			if(al.Count < 1 || al == null)
				return;
			this.addAllOldReportInfo(al,((FS.HISFC.DCP.Object.CommonReport)al[0]).State);
		}
		/// <summary>
		/// ��ȡ���һ���
		/// </summary>
		private void addPatientInfo()
		{
			//������ӱ��濨ʱ�������ﻼ�߻��߲�������
			try
			{
				if(this.type == "C")//����
				{
					if(this.ClinicNo != "" && this.patient != null)
					{
						//������ҽ��վ�����ʱ�ᴫ�뿴��ţ���ʱֻ��ʾһ������
						ArrayList al = new ArrayList();
						al.Add(this.patient);
						this.addAllClincPatientInfo(al);
					}
				
					else
					{
						//��ʾ��Dr���в��ϸ�ı���
						this.tvPatientInfo.Nodes.Clear();
						ArrayList al = new ArrayList();
						al = this.myReport.GetReportListByStateAndDoctor("2", FS.FrameWork.Management.Connection.Operator.ID);
						if(al != null && al.Count > 0)

						{   //���˵���Ⱦ������ ֻ��ʾ��������
							ArrayList alFilter = new ArrayList();
                            foreach (FS.HISFC.DCP.Object.CommonReport cm in al)
                            {
                                if (cm.Cancer_Flag == "1") alFilter.Add(cm);
                            }
                            //this.ShowReport(al);
							if (alFilter.Count>0)
							{
								this.ShowReport(alFilter);
							}
							else
							{
                             return ;
							}
						}
						return;
					}
					
				}
				else if(this.type == "I")//סԺ
				{
					if(this.patientInfo != null)
					{
						System.Collections.ArrayList al = new ArrayList();
						al.Add(this.patientInfo);
						this.addAllInpatientInfo(al);
					}
				}
				try
				{
					if(this.patient != null)
					{
						this.tvPatientInfo.SelectedNode = this.tvPatientInfo.Nodes[0].FirstNode;
					}
				}
				catch
				{
					//
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("��ȡ������Ϣʧ��>>"+ex.Message);
			}


		}

		/// <summary>
		/// ��ʾ���ﻼ���б�
		/// </summary>
		/// <param name="al"></param>
		private void addAllClincPatientInfo(ArrayList al)
		{
			this.addAllClincPatientInfo(al,false,false);
		}
		/// <summary>
		/// ��ʾ���ﻼ���б�
		/// </summary>
		/// <param name="al"></param>
		/// <param name="isDeptLimited">�Ƿ����������</param>
		/// <param name="isTimeLimited">�Ƿ���ʱ��������</param>
		private void addAllClincPatientInfo(ArrayList al, bool isDeptLimited,bool isTimeLimited)
		{
			this.tvPatientInfo.Nodes.Clear();
			//��Ӹ�
			System.Windows.Forms.TreeNode node = new TreeNode();
			node.Text = "�����б�--����[����ʱ��]�Һſ���";			
			node.Tag = "root";
			node.ImageIndex = 0;
			this.tvPatientInfo.Nodes.Add(node);

			//״̬
			this.state = "A";

			//��ӻ����ӽ��
			foreach(FS.HISFC.Models.Registration.Register reg in al)
			{
				if(isDeptLimited && reg.DoctorInfo.Templet.Dept.ID != this.emplInfo.Dept.ID)
				{
					continue;
				}
				if(isTimeLimited && (reg.DoctorInfo.SeeDate.CompareTo(this.dtBegin.Value) < 0 || reg.DoctorInfo.SeeDate.CompareTo(this.dtEnd.Value) > 0))
				{
					continue;
				}
				System.Windows.Forms.TreeNode patientnode = new TreeNode();
				FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
				patient = reg as FS.HISFC.Models.RADT.Patient;
				patient.User01 = reg.DoctorInfo.Templet.Dept.ID;
				
				patientnode.Tag = patient;
				patientnode.Text = ((FS.HISFC.Models.RADT.Patient)reg).Name+"["+reg.DoctorInfo.SeeDate.ToShortDateString()+"]"+reg.DoctorInfo.Templet.Dept.Name;
				patientnode.ImageIndex = 4;
				node.SelectedImageIndex = 4;
				this.tvPatientInfo.Nodes[0].Nodes.Add(patientnode);
			}
			this.tvPatientInfo.ExpandAll();
		}

		/// <summary>
		/// ��ʾסԺ�����б�
		/// </summary>
		/// <param name="al"></param>
		private void addAllInpatientInfo(ArrayList al)
		{
			this.addAllInpatientInfo(al,false,false);
		}

		/// <summary>
		/// ��ʾסԺ�����б�
		/// </summary>
		/// <param name="al"></param>
		/// <param name="isDeptLimited">�Ƿ����������</param>
		/// <param name="isTimeLimited">�Ƿ���ʱ��������</param>
		private void addAllInpatientInfo(ArrayList al, bool isDeptLimited, bool isTimeLimited)
		{
			this.tvPatientInfo.Nodes.Clear();
			//��Ӹ�
			System.Windows.Forms.TreeNode node = new TreeNode();
			node.Text = "�����б�--����[��Ժ����]��Ժ����";			
			node.Tag = "root";
			node.ImageIndex = 0;
			this.tvPatientInfo.Nodes.Add(node);

			//״̬
			this.state = "A";
			foreach(FS.HISFC.Models.RADT.PatientInfo info in al)
			{
				if(isDeptLimited && info.PVisit.PatientLocation.Dept.ID != this.emplInfo.Dept.ID)
				{
					continue;
				}
				if(isTimeLimited && (info.PVisit.InTime.CompareTo(this.dtBegin.Value) < 0 || info.PVisit.InTime.CompareTo(this.dtEnd.Value) > 0))
				{
					continue;
				}
				System.Windows.Forms.TreeNode patientnode = new TreeNode();
				
				info.User01 = info.PVisit.PatientLocation.Dept.ID;
				if(info.User01 == null|| info.User01 == "")
				{
					info.User01 = this.emplInfo.Dept.ID;
				}
				patientnode.Tag = info;
				string id = "";
				if(info.ID != null && info.ID.Length > 8)
				{
					id = info.ID.Remove(0,4);
				}
				patientnode.Text = info.Patient.Name+"["+info.PVisit.InTime.ToShortDateString()+"]"+info.PVisit.PatientLocation.Dept.Name;
				patientnode.ImageIndex = 4;
				node.SelectedImageIndex = 4;
				this.tvPatientInfo.Nodes[0].Nodes.Add(patientnode);
			}
			this.tvPatientInfo.ExpandAll();
		}
		/// <summary>
		/// ��� ����
		/// </summary>
		/// <param name="state">״̬</param>
		private void approveReport(string state)
		{
			string tempstate = "";
			if(this.tvPatientInfo.SelectedNode == null)
			{
				return;
			}
			try
			{
				if(this.tvPatientInfo.SelectedNode.Tag.ToString() == "root") return;
				FS.HISFC.DCP.Object.CommonReport report = this.tvPatientInfo.SelectedNode.Tag as FS.HISFC.DCP.Object.CommonReport;
				//������״̬���д���
				if(state == "34")
				{
					if(FS.FrameWork.Management.Connection.Operator.ID == report.ReportDoctor.ID)
					{
						//����������
						tempstate = "3";
					}
					else
					{
						//����������
						tempstate = "4";
					}
					if(!this.isAllowOper(report,"Cancel"))
					{
						return;
					}
				}
				else if(state == "1")
				{
					if(!this.isAllowOper(report,"ApproveOne"))
					{
						return;
					}
					tempstate = state;
				}
				else if(state == "2")
				{
					if(!this.isAllowOper(report,"ApproveTwo"))
					{
						return;
					}
					tempstate = state;
				}
				else if(state == "0")
				{
					if(!this.isAllowOper(report,"Recover"))
					{
						return;
					}
					tempstate = state;
				}

				this.ucReportCancerRegister1.ApproveReport(report,tempstate);

				if(this.alReport.Count > 0)
				{
					//���ʱ��ˢ��
					this.myGetReportByReport(this.AlReport);
				}
				else if(this.Type != "O")
				{
					//�����ʱ��ˢ��
					this.mySearchOldReport();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(this,"���ܽ����ת���ɱ��濨"+ex.Message,"����>>");
			}
		}
		
		/// <summary>
		/// ���б�ˢ��
		/// </summary>
		/// <param name="alAllState">�б��е����б���</param>
		private void myGetReportByReport(ArrayList alAllState)
		{
			//���������ʱˢ�±����б� ���Ƶ��㷨
			//���ڳ�ʼ����ǰ���濨�����Ѿ���ֵ������������״̬�ı䡣���԰�״̬���·�����ʾ
			try
			{
				ArrayList alone = new ArrayList();//�¼�
				ArrayList altwo = new ArrayList();//�ϸ�
				ArrayList althree = new ArrayList();//���ϸ�
				ArrayList alfour = new ArrayList();//����������
				ArrayList alfive = new ArrayList();//����������

				foreach(ArrayList alonestate in alAllState)
				{
					foreach(FS.HISFC.DCP.Object.CommonReport report in alonestate)
					{
						FS.HISFC.DCP.Object.CommonReport tempreport = new FS.HISFC.DCP.Object.CommonReport();
						tempreport = this.myReport.GetCommonReportByID(report.ID);
						switch(tempreport.State)
						{
							case "0":
								alone.Add(tempreport);
								break;
							case "1":
								altwo.Add(tempreport);
								break;
							case "2":
								althree.Add(tempreport);
								break;
							case "3":
								alfour.Add(tempreport);
								break;
							case "4":
								alfive.Add(tempreport);
								break;
						}
					}
				}
				this.tvPatientInfo.Nodes.Clear();
				this.ShowReport(alone);
				this.ShowReport(altwo);
				this.ShowReport(althree);
				this.ShowReport(alfour);
				this.ShowReport(alfive);
			}
			catch(Exception ex)
			{
				MessageBox.Show("ˢ���б����>>" + ex.Message);
			}
		}

		/// <summary>
		/// ����Ȩ�޵��ж�
		/// </summary>
		/// <param name="report">����ʵ��</param>
		/// <param name="operType">������ʽ</param>
		/// <returns>true�в���Ȩ�� false�޲���Ȩ��</returns>
		private bool isAllowOper(FS.HISFC.DCP.Object.CommonReport report, string operType)
		{
			bool isallow = false;			
			switch(operType)
			{
				case "Modify":
					#region Modify
				switch(report.State)
				{
					case "0":
					case "5": //���ϸ������޸��ϱ��ı���
					case "2":
						if(FS.FrameWork.Management.Connection.Operator.ID == report.ReportDoctor.ID)
						{
							isallow = true;
						}
						else
						{
							if(this.isAllowApprove)
							{
								isallow = true;
							}
						}
						if(!isallow)
						{
							MessageBox.Show(this,"��ʾ�������޸�������д�ı���","��ʾ>>");
						}
						break;
					case "1":
						isallow = this.tbbRenew.Pushed;
						if(!isallow)MessageBox.Show(this,"��ʾ�������Ѿ��ϸ�","��ʾ>>");
						break;
					case "3":
						MessageBox.Show(this,"��ʾ�������Ѿ�������������","��ʾ>>");
						break;
					case "4":
						MessageBox.Show(this,"��ʾ���������ʱ�Ѿ�����","��ʾ>>");
						break;
				}
					#endregion
					break;
				case "Cancel":
					#region Cancel
				switch(report.State)
				{
					case "0":
					case "2":
						if(FS.FrameWork.Management.Connection.Operator.ID == report.ReportDoctor.ID)
						{
								
							isallow = true;
						}
						else
						{
							if(this.isAllowApprove)
							{
								isallow = true;
							}
						}
						if(!isallow)
						{
							MessageBox.Show(this,"��ʾ�������޸�������д�ı���","��ʾ>>");
						}
						else
						{
							if(MessageBox.Show(this,"ȷʵҪ���ϱ�����","��ʾ>>",System.Windows.Forms.MessageBoxButtons.YesNo,
								System.Windows.Forms.MessageBoxIcon.Information,System.Windows.Forms.MessageBoxDefaultButton.Button2) 
								== System.Windows.Forms.DialogResult.No) 
							{
								isallow = false;
							}
						}
						break;
					case "1":
						MessageBox.Show(this,"��ʾ�������Ѿ��ϸ�","��ʾ>>");
						break;
					case "3":
						MessageBox.Show(this,"��ʾ�������Ѿ�������������","��ʾ>>");
						break;
					case "4":
						MessageBox.Show(this,"��ʾ���������ʱ�Ѿ�����","��ʾ>>");
						break;
				}
					#endregion
					break;
				case "Delete":
					#region ɾ��
					if(FS.FrameWork.Management.Connection.Operator.ID == report.ReportDoctor.ID)
					{
						isallow = true;
					}
					else
					{
						MessageBox.Show(this,"��ʾ������ɾ��������д�ı���","��ʾ>>");
					}
					#endregion
					break;
				case "ApproveOne":
					#region ���
					if(report.State == "0" ||report.State == "5" ) 
					{
						isallow = true;
					}
					else if(report.State == "1")
					{
						MessageBox.Show(this,"��ʾ����������˺ϸ�","��ʾ>>");						
					}
					else if(report.State == "2")
					{
						if(MessageBox.Show(this,"���濨����ˣ��Ƿ�����","��ʾ>>",System.Windows.Forms.MessageBoxButtons.YesNo,
							System.Windows.Forms.MessageBoxIcon.Information,System.Windows.Forms.MessageBoxDefaultButton.Button2) 
							== System.Windows.Forms.DialogResult.Yes)
							isallow = true;
					}
					else if(report.State == "3")
					{
						MessageBox.Show(this,"��ʾ�������Ѿ�����������","��ʾ>>");								
					}
					else if(report.State == "4")
					{
						MessageBox.Show(this,"��ʾ���������ʱ�Ѿ�����","��ʾ>>");								
					}
					break;
				case "ApproveTwo":
//					if(report.State == "0" )
					if(report.State == "0"||report.State == "5" )
					{
						isallow = true;
					}
					else if(report.State == "2")
					{
						MessageBox.Show(this,"��ʾ����������˲��ϸ�","��ʾ>>");						
					}
					else if(report.State == "1")
					{
						if(MessageBox.Show(this,"���濨����ˣ��Ƿ�����","��ʾ>>",System.Windows.Forms.MessageBoxButtons.YesNo,
							System.Windows.Forms.MessageBoxIcon.Information,System.Windows.Forms.MessageBoxDefaultButton.Button2) 
							== System.Windows.Forms.DialogResult.Yes)
							isallow = true;
					}
					else if(report.State == "3")
					{
						MessageBox.Show(this,"��ʾ�������Ѿ�����������","��ʾ>>");								
					}
					else if(report.State == "4")
					{
						MessageBox.Show(this,"��ʾ���������ʱ�Ѿ�����","��ʾ>>");								
					}				
					break;
					#endregion
				case "Recover":
					#region �ָ�
					if(!this.isAllowRecover)
					{
						MessageBox.Show(this,"��ʾ���ָ����濨���ڼ���Ԥ���������Ա��ϵ","��ʾ>>");								
					}
					else
					{
						if(report.State != "3" && report.State != "4")
						{
							MessageBox.Show(this,"��ʾ��û�����ϵı��治����ָ�","��ʾ>>");								
						}
						isallow = true;
						if(MessageBox.Show(this,"ȷʵҪ�ָ���","��ʾ>>",System.Windows.Forms.MessageBoxButtons.YesNo,
							System.Windows.Forms.MessageBoxIcon.Information,System.Windows.Forms.MessageBoxDefaultButton.Button2) 
							== System.Windows.Forms.DialogResult.No) 
						{
							isallow = false;
						}
					}
					break;
					#endregion
			}
			if(isallow && this.tbbRenew.Pushed && (report.ExtendInfo3.IndexOf("�Ѷ���") != -1 || report.ExtendInfo2 != ""))
			{
				string state = "";
				//state = this.myReport.GetRenewReportByID(report.ExtendInfo2).State; 
				if(state != "" && state != "3" && state == "4")
				{
					MessageBox.Show(this,"���濨�Ѿ�����","����>>");					
					isallow = false;	
				}
			}
			return isallow;
		}

		/// <summary>
		/// ��ѯ���ݼ���ѯʱ��ĳ�ʼ��
		/// </summary>
		private void initQueryContent()
		{
			//��ʼ����ѯʱ��
			System.DateTime dt = this.myReport.GetDateTimeFromSysDateTime();
			dt = dt.AddDays(1);
			this.dtEnd.Value = new DateTime(dt.Year,dt.Month,dt.Day,0,0,0);
			this.dtBegin.Value = this.dtEnd.Value.AddDays(-3);
			//��ʼ����ѯ����
			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "choose";
			obj.Name = "��ѡ��";
			al.Add(obj);

			if(this.isAllowApprove)
			{
				obj = new FS.FrameWork.Models.NeuObject();
				obj.ID = enumQueryType.ȫԺ������ѯ.ToString();
				obj.Name = enumQueryType.ȫԺ������ѯ.ToString();
				al.Add(obj);
			}

			#region ��ʿ
			FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
			FS.HISFC.Models.Base.Employee person = personMgr.GetPersonByID(personMgr.Operator.ID);
			
			if(person.EmployeeType.ID.ToString() == "N")
			{
				if(al.Count == 1)
				{
					al.Clear();
				}
				this.tbbSave.Visible = false;
				this.tbbNew.Visible = false;
				this.tbbModify.Visible = false;
				this.tbbCancel.Visible = false;
				this.tbbRenew.Visible = false;

				obj = new FS.FrameWork.Models.NeuObject();
				obj.ID = enumQueryType.������������ѯ.ToString();
				obj.Name = enumQueryType.������������ѯ.ToString();
				al.Add(obj);
			}
			#endregion

		    #region �ǻ�ʿ
			else
			{
				for(int i = 0; i < (int)enumQueryType.End; i ++)
				{
					if((int)enumQueryType.ȫԺ������ѯ != i)
					{
						obj = new FS.FrameWork.Models.NeuObject();
						obj.ID = ((enumQueryType)i).ToString();
						obj.Name = ((enumQueryType)i).ToString();
						al.Add(obj);
					}
				}
			}
			#endregion

			this.cmbQueryContent.DataSource = al;
			this.cmbQueryContent.DisplayMember = "Name";
			this.cmbQueryContent.ValueMember = "ID";
			this.cmbQueryContent.SelectedIndex = 0;
		}

		/// <summary>
		/// ѡ����ʾ��������ڷ�Χ
		/// </summary>
		private void ChanageTime() 
		{
			//ѡ��ʱ��Σ����û��ѡ��ͷ���
			
			DateTime dtbegin = this.myReport.GetDateTimeFromSysDateTime().Date;
			DateTime dtend = dtbegin;
			if(FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref dtbegin, ref dtend)==0) return;
			this.BeginTime = dtbegin.ToString();
			this.EndTime = dtend.ToString();
		}
		
		/// <summary>
		/// ����lis���
		/// </summary>
		private void queryLisResults()
		{
//			//this.tvPatientInfo.Nodes.Clear();
//			ArrayList al = new ArrayList();
//			al = this.myReport.GetNeedReportLisResultList(FS.FrameWork.Management.Connection.Operator.ID, this.emplInfo.Dept.ID);
//			if(al == null)
//			{
//				al = new ArrayList();
//			}
//			if(al.Count == 0)
//			{
//				MessageBox.Show(this, "û�п���ʵ���Ҽ�����Ա���","��ʾ>>");
//				return;
//			}
//			this.showLisResults(al);
		}

		/// <summary>
		/// ��ʾlis���
		/// </summary>
		/// <param name="al"></param>
		private void showLisResults(ArrayList al)
		{
//			System.Windows.Forms.TreeNode node = new TreeNode();
//			this.State = "A";
//			int imagindex = 1;
//
//			node.Text = "ʵ���Ҽ�����Ա���";
//
//			node.Tag = "root";
//			node.ImageIndex = 0;
//			this.tvPatientInfo.Nodes.Add(node);
//				
//			foreach(FS.HISFC.Models.HealthCare.LisResult result in al)
//			{
//				System.Windows.Forms.TreeNode reportnode = new TreeNode();
//
//
//				if(result.Type == "C")
//				{
//					result.Patient.PID.CardNo = result.Patient.ID;
//					if(result.Patient.ID != "" && result.Patient.ID != null)
//					{
//						FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
//						al = regMgr.Query(result.Patient.ID,result.CheckDate);
//						if(al == null || al.Count == 0)
//						{
//							//
//						}
//						else
//						{
//							result.Patient = al[0] as FS.HISFC.Models.RADT.Patient;
//						}
//					}
//				}
//				if(result.Type == "I")
//				{
//					if(result.Patient.ID != "" && result.Patient.ID != null)
//					{
//						FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ...");
//						Application.DoEvents();
//						string sql = @" where d.patient_no like '%{0}'";
//						
//						FS.HISFC.BizLogic.RADT.InPatient inpMgr = new FS.HISFC.BizLogic.RADT.InPatient();
//						al = inpMgr.PatientInfoGet(string.Format(sql,result.Patient.ID));
//						FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
//						if(al == null || al.Count == 0)
//						{
//							//
//						}
//						else
//						{
//							result.Patient = al[0] as FS.HISFC.Models.RADT.Patient;
//						}
//					}
//			
//				}
//				result.Patient.Memo = "lisResult";
//				result.Patient.Weight = result.ID;
//				//result.Patient.User01 = result.ID;
//				result.Patient.User02 = result.SampleID;
//				result.Patient.User03 = result.Disease.ID;
//
//				reportnode.Tag = result.Patient;
//			
//				reportnode.ImageIndex = imagindex;
//				reportnode.SelectedImageIndex = 2;
//				reportnode.Text = result.Patient.Name + "[" + result.DiagRecord + "]";
//
//				this.tvPatientInfo.Nodes[this.tvPatientInfo.Nodes.Count-1].Nodes.Add(reportnode);
//			}
//			this.tvPatientInfo.ExpandAll();
//	
		}
		/// <summary>
		/// ��ʾ���ȡ�û����������Ƿ������һ������
		/// </summary>
		/// <param name="information">��ʾ��Ϣ</param>
		/// <returns></returns>
		private bool isContinue(string information)
		{
			if(MessageBox.Show(this,information,"��ܰ��ʾ>>",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Information,
				System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// ��ʼ��
		/// </summary>
		private void init()
		{
			#region �¼�����
			//getDeptAllPatientInfo�����д������¼��Ĵ���
			this.tvPatientInfo.AfterSelect += new TreeViewEventHandler(tvPatientInfo_AfterSelect);
			//this.tvPatientInfo.DoubleClick += new EventHandler(tvPatientInfo_DoubleClick);
			this.cmbQueryContent.SelectedValueChanged +=new EventHandler(cmbQueryContent_SelectedValueChanged);
			#endregion
			
			#region uc�ĳ�ʼ��

			this.tvPatientInfo.Nodes.Clear();
			this.ucReportCancerRegister1.init();
			this.ucReportCancerRegister1.PriOper = this.emplInfo as FS.FrameWork.Models.NeuObject;
			this.ucReportCancerRegister1.MyPriDept = this.emplInfo.Dept;
            this.ucReportCancerRegister1.SetEnable(false);
			this.tbbSave.Enabled = false;
			#endregion

			#region Ȩ�޴���
			FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new  FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
			List<FS.FrameWork.Models.NeuObject> alPriv = privManager.QueryUserPriv(privManager.Operator.ID, "0901");
			
			if(alPriv == null || alPriv.Count == 0)
			{
				//�Ǳ����Ʋ���ɾ�������
				this.tbbEligible.Visible = false;
				this.tbbUneligible.Visible = false;
				this.ucReportCancerRegister1.IsAdvance = false;
				this.isAllowApprove = false;
			}
			else
			{
				this.ucReportCancerRegister1.IsAdvance = true;
				this.isAllowApprove = true;
			}
			alPriv = privManager.QueryUserPriv(privManager.Operator.ID, "0902");
			if(alPriv == null || alPriv.Count == 0)
			{
				this.tbbRecover.Visible = false;
				this.isAllowRecover = false;
			}
			else
			{
				this.isAllowRecover = true;
				this.tbbRecover.Visible = true;
			}
			this.tbbRecover.Visible = this.isAllowRecover;

			alPriv = privManager.QueryUserPriv(privManager.Operator.ID, "0903");
			if(alPriv == null || alPriv.Count == 0 || privManager.Operator.ID != "001406")
			{
				this.tbbDesigner.Visible = false;
			}
			else
			{
				this.tbbDesigner.Visible = true;
			}

			//ɾ�������ã�ֻ��������
			this.tbbDel.Visible = false;
			
			#endregion

			#region ��������
			FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
			if(deptMgr.GetDeptmentById(this.emplInfo.Dept.ID).DeptType.ID.ToString() == "C")
			{
				this.Type = "C";
				this.llbPatienNO.Tag = "C";
				this.llbPatienNO.Text = "���￨��";
			}
			else if(deptMgr.GetDeptmentById(this.emplInfo.Dept.ID).DeptType.ID.ToString() == "I")
			{
				this.Type = "I";
				this.llbPatienNO.Tag = "I";
				this.llbPatienNO.Text = "ס Ժ ��";
			}
			else
			{
				this.Type = "O";
				this.llbPatienNO.Tag = "O";
				this.llbPatienNO.Text = "�� �� ��";
			}
			#endregion

			//������Ȩ�޴�������
			this.initQueryContent();          

			#region ���ݴ��ڲ�����ʼ����ť���б���Ϣ��ʾ

			//��ѯ����δѡ��ʱ��ѯ����������
			this.tbbTime.Visible = false;
            
			if(this.type != "O" && this.alReport.Count == 0)
			{
				this.addPatientInfo();
			}
			else
			{
				foreach(ArrayList altemp in this.AlReport)
				{
					this.ShowReport(altemp);
				}
				if(this.AlReport.Count <= 0 && this.InpatientNO != "")
				{
					//����סԺ������Ϣ
					this.Type = "I";
					this.addPatientInfo();
				}
			}
			#endregion					
		}

		/// <summary>
		/// ��ѯ
		/// </summary>
		private void query()
		{
			string queryType = this.cmbQueryContent.SelectedValue.ToString();
			this.query(queryType);
		}
		private void query(string queryType)
		{
			this.tvPatientInfo.Nodes.Clear();

			bool isReport = false;
			bool isDept = false;
			if(queryType == "choose")
			{
				MessageBox.Show("��ѡ���ѯ����");
				return;
			}
			if(queryType == enumQueryType.ȫԺ������ѯ.ToString())
			{
				isReport = true;
			}
			else if(queryType == enumQueryType.�����Ҳ��ϸ񱨿���ѯ.ToString())
			{
				isReport = true;
				isDept = true;
			}	
			else if(queryType == enumQueryType.�����ұ�����ѯ.ToString())
			{
				isReport = true;
				isDept = true;
			}
			else if(queryType == enumQueryType.������������ѯ.ToString())
			{
				isReport = true;
			}
//			else if(queryType == enumQueryType.�����Ҳ�����Ϣ��ѯ.ToString())
//			{
//				isDept = true;
//			}
			ArrayList al = new ArrayList();

			string SQL = "\nwhere report_date > to_date('{0}','yyyy-mm-dd hh24-mi-ss')\nand   report_date < to_date('{1}','yyyy-mm-dd hh24-mi-ss') \n and cancer_flag='1'";
			SQL = string.Format(SQL, this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString());
			
			
			if(this.txtReportNo.Text.Trim() != "")
			{
				SQL += "\nand   report_no = '" + this.txtReportNo.Text.Trim() + "'";
			}
			else if(this.txtInPatienNo.Text.Trim() != "")
			{
				SQL += "\nand   patient_id like '%" + this.txtInPatienNo.Text.Trim() + "'";
			}
			else if(this.txtName.Text != "")
			{
				SQL += "\nand   patient_name like '%" + this.txtName.Text.Trim() + "%'";
					
			}
			else if(this.txtDoctor.Text.Trim() != "")
			{
				SQL += "\nand   report_doctor = '" + this.txtDoctor.Text.Trim() + "'";
					
			}
			else if(queryType == enumQueryType.�����Ҳ��ϸ񱨿���ѯ.ToString())
			{
				SQL += "\nand   state = '2'";
			}
			else
			{
				if(!isReport)
				{
					return;
				}
			}
			if(isDept)
			{
				SQL += "\nand   doctor_dept = '" + this.emplInfo.Dept.ID + "'";
			}

			if(queryType == enumQueryType.������������ѯ.ToString())
			{
				try
				{
					FS.HISFC.BizLogic.Manager.DepartmentStatManager dsm = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
					System.Collections.ArrayList alDept = new ArrayList();// = dsm.LoadChildren("01",((FS.HISFC.Models.Base.Employee)dsm.Operator).Dept.ID, 2);
					System.Collections.ArrayList alParent = dsm.LoadByChildren("01",((FS.HISFC.Models.Base.Employee)dsm.Operator).Dept.ID);
					
					foreach(FS.HISFC.Models.Base.DepartmentStat dept in alParent)
					{
						alDept.AddRange(dsm.LoadChildren("01", dept.PardepCode, 2));
						alDept.AddRange(dsm.LoadChildren("01", dept.DeptCode, 2));
					}

					string deptCode = "'" + ((FS.HISFC.Models.Base.Employee)dsm.Operator).Dept.ID + "',";
					foreach(FS.HISFC.Models.Base.DepartmentStat dept in alDept)
					{
						deptCode += "'"+dept.DeptCode +"'," + "'"+dept.PardepCode +"',";
					}
					deptCode = deptCode.TrimEnd(',');
					SQL += "\nand   doctor_dept in (" +deptCode + ")";
				}
				catch
				{}
			}

			al = this.myReport.GetCommonReportListByWhere(SQL+" and  cancer_flag<>'0'"+ "order by report_no");

			if(isReport)
			{
				this.showAllReport(al);
				return;
			}
			if(al != null && al.Count > 0)
			{
				if(this.isContinue("�������Ĳ�ѯ����,ϵͳ��⵽�����Ѿ��������Ƿ������"))
				{
					ArrayList a = new ArrayList();
					this.showAllReport(al);
					return;
				}
			}
            //��ʼ��ʱ����ȡ�ĵ�½������� IסԺ C���� ���ٴ����Ҳ��ܱ���  chengym
			if((this.llbPatienNO.Tag != null && this.llbPatienNO.Tag.ToString() == "I"))
			{
				FS.SOC.HISFC.BizProcess.DCP.Patient patientBizPro = new  FS.SOC.HISFC.BizProcess.DCP.Patient();
				SQL = "\nwhere d.in_date > to_date('{0}','yyyy-mm-dd hh24-mi-ss')\nand   d.in_date < to_date('{1}','yyyy-mm-dd hh24-mi-ss')";
				SQL = string.Format(SQL, this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString());
				if(this.txtInPatienNo.Text.Trim() != "")
				{
					SQL += "\nand   d.patient_no like '%" + this.txtInPatienNo.Text.Trim() + "'";
				}
				else if(this.txtName.Text != "")
				{
					SQL += "\nand   d.name like '" + this.txtName.Text.Trim() + "%'";
					
				}
				else
				{
					return;
				}
				FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ...");
				Application.DoEvents();
                //totototo
                al = patientBizPro.QueryPatientInfoBySqlWhere(SQL + "order by report_no");
				FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

				if(al != null && al.Count > 0)
				{
					this.addAllInpatientInfo(al);
				}
				else
				{
					MessageBox.Show("û�в�ѯ���κ���Ϣ��");
				}
			}
			else if((this.llbPatienNO.Tag != null && this.llbPatienNO.Tag.ToString() == "C"))
			{
                FS.SOC.HISFC.BizProcess.DCP.Patient patientBizPro = new FS.SOC.HISFC.BizProcess.DCP.Patient();
				if(this.myReport.Sql.GetSql("Registration.Register.Query.1",ref SQL) == -1)
				{
					return;
				}
				SQL += "\nwhere reg_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')\n and reg_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')";
				SQL = string.Format(SQL, this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString());

				if(this.txtInPatienNo.Text.Trim() != "")
				{
					SQL += "\nand card_no like '%" + this.txtInPatienNo.Text.Trim() + "'";
				}
				else if(this.txtName.Text != "")
				{
					SQL += "\nand name like '" + this.txtName.Text.Trim() + "%'";
				}
				else
				{
					return;
				}

				FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ...");
				Application.DoEvents();
                //totototo
                al = patientBizPro.QueryRegister(SQL + "order by report_no");
				FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

				if(al != null && al.Count > 0)
				{
					this.addAllClincPatientInfo(al);
				}
				else
				{
					MessageBox.Show("û�в�ѯ���κ���Ϣ��");
				}
			}
		}

		public void ClearAll()
		{
			try
			{
				//this.txtPatientName.Clear();
//				this.Printpanel.Tag = null;
//				this.lbPatientDept.Tag = "";
//				this.txtSpecialAddress.Clear();
//				this.lbID.Text = "";
//				this.txtPatientName.Text = "";
//				this.txtPatientParents.Clear();
//				this.txtPatientID.Clear();
//
//				//�Ա�
//				this.ucState = 0;
//				this.cbxMan.Checked = false;
//				this.cbxWomen.Checked = false;
//				this.ucState = 1;
//
//				this.txtAge.Clear();
//				this.rdbYear.Checked = true;
//				this.txtWorkPlace.Clear();
//				this.txtTelephone.Clear();
//				this.mySetHomeArea(8);
//				//				this.cmbprovince.SelectedIndex = 0;
//				//				this.cmbCity.SelectedIndex = 0;
//				//				this.cmbCouty.SelectedIndex = 0;
//				//				this.cmbTown.SelectedIndex = 0;
//				//				this.txtHomeAddress.Clear();
//				this.txtProvince.Clear();
//				this.txtCity.Clear();
//				this.txtCounty.Clear();
//				this.txtTown.Clear();
//				this.txtVillage.Clear();
//				this.cmbProfession.SelectedIndex = 0;
//				this.rdbInfectionClass.Checked = true;
//				this.cmbInfectionClass.SelectedIndex = 0;
//				this.cmbInfectionClass.Enabled = true;
//
//				this.dtBirthDay.Value = this.myReport.GetDateTimeFromSysDateTime();
//				this.txtAge.Text = "";
//				this.rdbYear.Checked = true;
//
//				this.dtInfectionDate.Value = this.dtBirthDay.Value;
//				this.dtDiaDate.Value = this.dtBirthDay.Value;
//				this.dtDeadDate.Value = this.dtBirthDay.Value;
//
//				this.txtInfectDate.Text = "";
//				this.txtInfectDate.Visible = true;
//
//				this.cbxDeadDate.Checked = true;
//
//				//����
//				if(this.panelAddtion.Visible)
//				{
//					this.panelAdditionMain.Controls.Clear();				
//				}
//				this.panelAddtion.Visible = false;
//
//				this.cmbCaseClassOne.SelectedIndex = 0;
//				this.rdbInfectionOtherNo.Checked = true;
//				this.rtxtMemo.Clear();
//				this.txtCase.Clear();
				//������ �������
				//this.lbReportDoctor.Text = this.employHelper.GetName(this.PriOper.ID);
				//this.lbDoctorDept.Text = this.deptHelper.GetName(this.MyPriDept.ID);
				//����ʱ��
				//this.lbReportTime.Text = this.myReport.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:MM:ss");
			}
			catch
			{
				//
			}
		}

		#endregion
		#region ������
		private void tvPatientInfo_AfterSelect(object sender, TreeViewEventArgs e)
		{
			

			//���ڵ�tagΪroot
			this.statusBar1.Panels[1].Text = "";
			this.ucReportCancerRegister1.ClearAll(true);
			
			if(this.tvPatientInfo.SelectedNode.Tag.ToString() == "root") 
			{
				this.tbbSave.Enabled = true;
				this.ucReportCancerRegister1.SetEnable(true);
				return;
			}
			//״̬ΪAʱ���ص��ǻ����б� Bʱ���ص��Ǳ����б�
			if(this.state == "A")
			{
				this.Patient = this.tvPatientInfo.SelectedNode.Tag as FS.HISFC.Models.RADT.Patient;
				this.tbbModify.Enabled = false;
				this.tbbRenew.Enabled = false;
				this.tbbSave.Enabled = true;
				//this.txtAddress.Text = "��ַ��" + this.patient.AddressHome;
				if(this.patient.Memo == "lisResult")
				{
					//					this.ucReportRegister1.IsLisResult = true;
					//					FS.HISFC.Models.HealthCare.LisResult result = new FS.HISFC.Models.HealthCare.LisResult();
					//					result.ID = this.patient.Weight;
					//					result.SampleID = this.patient.User02;
					//					this.ucReportRegister1.LisResultObject = result;
				}
				else 
				{
					this.ucReportCancerRegister1.IsLisResult = false;
				}
			}
			else
			{
				FS.HISFC.DCP.Object.CommonReport report = this.tvPatientInfo.SelectedNode.Tag as FS.HISFC.DCP.Object.CommonReport;
				//��״̬����Ϊѡ�еı����״̬
				this.State = report.State;
				this.tbbModify.Enabled = true;
				this.tbbRenew.Enabled = true;
				this.tbbSave.Enabled = false;
				//��ʾ����
				this.ucReportCancerRegister1.ShowReportData(report);
							
			}
			//this.ucReportCancerRegister1.cmbNation.SelectedValue = 1;
		}

		#endregion
		#region �¼�

		#region ������
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			this.ucReportCancerRegister1.InfectCode = "";
			if(e.Button != this.tbbRenew)
			{
				//������Ƕ���
				this.tbbRenew.Pushed = false;
				if(e.Button != this.tbbSave)
				{
					this.ucReportCancerRegister1.IsRenew = false;
				}
			}
			if(e.Button == this.tbbPrint)
			{
				//��ӡ

					this.ucReportCancerRegister1.print();
				
			}
			else if(e.Button == this.tbbExit)
			{
				//�˳�
				if(this.isContinue("ȷ���˳���"))
				{
					this.Close();
				}
			}
			else if(e.Button == this.tbbModify)
			{
				//�޸�
				if(this.state == "A" || this.tvPatientInfo.Nodes.Count < 1 || this.tvPatientInfo.SelectedNode == null)
				{
					return;
				}
				if(this.tvPatientInfo.SelectedNode.Tag.ToString() == "root") return;
				this.tbbSave.Enabled = this.isAllowOper(
					(FS.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag,"Modify");			
				this.ucReportCancerRegister1.SetEnable(this.tbbSave.Enabled);				
				this.ucReportCancerRegister1.IsRenew = false;		
				this.tbbRenew.Pushed = false;
			}
			else if(e.Button == this.tbbRenew)
			{
				//����
				if(this.state == "A" || this.tvPatientInfo.Nodes.Count < 1 || this.tvPatientInfo.SelectedNode == null)
				{
					return;
				}
				this.tbbRenew.Pushed = !this.tbbRenew.Pushed;
				if(this.state == "A" || !this.tbbRenew.Pushed)
				{
					this.ucReportCancerRegister1.IsRenew = false;
					this.ucReportCancerRegister1.SetEnable(false);
					this.tbbSave.Enabled = false;
					return;
				}
				if(this.tvPatientInfo.SelectedNode.Tag.ToString() == "root")
				{
					return;
				}
				FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
				report = (FS.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag;
				string tmpMessage = "�����������ʺ��ںϸ񱨿��ġ����Ʋ�����ȷ����ߡ���Ⱦ����ϱ����";
				if(report.State == "0"||report.State == "5")
				{
					MessageBox.Show(tmpMessage + "\n�±�������ֱ�ӡ��޸ġ���");
					return;
				}
				if(report.State == "2")
				{
					MessageBox.Show(tmpMessage + "\n���ϸ񿨣���ֱ�ӡ��޸ġ���");
					return;
				}
				this.tbbSave.Enabled = this.isAllowOper(report,"Modify");			
				this.ucReportCancerRegister1.SetEnable(this.tbbSave.Enabled);
				this.ucReportCancerRegister1.IsRenew = this.tbbSave.Enabled;
				this.tbbRenew.Pushed = this.tbbSave.Enabled;
			}
			else if(e.Button == this.tbbClear)
			{
				//���
				if(this.isContinue("�Ƿ�Ҫ�����ǰ������Ϣ��"))
				{
					this.ucReportCancerRegister1.ClearAll();
				}
			}
			else if(e.Button == this.tbbSearch)
			{
				//��ѯ
				this.query();

			}
			else if(e.Button == this.tbbTime)
			{
				//ʱ��ѡ��
				this.ChanageTime();
			}
			else if(e.Button == this.tbbNew)
			{
				//�¼�
				if(this.isContinue("ȷ���½�һ�ű��濨��"))
				{
					this.ClinicNo = "";
					this.Type = "C";
					this.addPatientInfo();
					this.tbbSave.Enabled = true;
					this.ucReportCancerRegister1.ClearAll(true);
					this.ucReportCancerRegister1.SetEnable(true);

				}
			}
			else if(e.Button == this.tbbSave)  //����
			{


				if(this.ucReportCancerRegister1.SaveReport() == 0)
				{
					this.ucReportCancerRegister1.ClearAll();
					if(this.tbbUneligible.Visible && this.AlReport != null && this.AlReport.Count > 0)
					{
						this.myGetReportByReport(this.AlReport);
					}
					else
					{
						this.mySearchOldReport();
					}
					this.tbbSave.Enabled = false;
					this.ucReportCancerRegister1.SetEnable(false);
					//����Ϻ��Ƿ���д�˱��濨��������ɹ���������
					this.Text += "--����ɹ�";
				}
			}
			else if(e.Button == this.tbbEligible)
			{
				//���Ϊ�ϸ�
				if(this.state == "A")
				{
					return;
				}
				this.approveReport("1");
			}
			else if(e.Button == this.tbbUneligible)
			{
				//���ϸ�
				if(this.state == "A")
				{
					return;
				}
				this.approveReport("2");
			}	
			else if(e.Button == this.tbbRecover)
			{
				//�ָ�
				if(this.state == "A")
				{
					return;
				}
				this.approveReport("0");
			}
			else if(e.Button == this.tbbHelp)
			{
				//����
				//this.ucReportCancerRegister1.Help();
			}
			else if(e.Button == this.tbbCancel)
			{
				//����
				if(this.state == "A" || this.tvPatientInfo.Nodes.Count < 1)
				{
					return;
				}
				this.approveReport("34");
			}
			else if(e.Button == this.tbbDel)
			{
				//ɾ��
				if(this.state == "A" || this.tvPatientInfo.SelectedNode == null)
				{
					return;
				}
				if(this.tvPatientInfo.SelectedNode.Tag.ToString() == "root") return;
				FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
				report = (FS.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag;
				if(report != null && report.ID != "")
				{
					if(this.ucReportCancerRegister1.DeleteReport(report.ID) == 0)
					{
						this.tvPatientInfo.Nodes.Remove(this.tvPatientInfo.SelectedNode);
						ArrayList alTempReport = new ArrayList();
						foreach(ArrayList al in this.AlReport)
						{
							ArrayList altemp = new ArrayList();
							foreach(FS.HISFC.DCP.Object.CommonReport rpt in al)
							{
								if(rpt.ID != report.ID)
								{
									altemp.Add(rpt);
								}
							}
							if(altemp != null && altemp.Count > 0)
							{
								alTempReport.Add(altemp);
							}
						}
						this.AlReport = alTempReport;
						this.myGetReportByReport(this.AlReport);
					}
				}
			}
			else if(e.Button == this.tbbDesigner)
			{
				this.tbbDesigner.Pushed = !this.tbbDesigner.Pushed;
				//this.ucReportCancerRegister1.IsDesigner = this.tbbDesigner.Pushed;

				if(this.tbbDesigner.Pushed)
				{
				    this.showCanUsedControlList();
				}
			}
		}
		#endregion



		#region ����
		private void frmReportRegister_Load(object sender, System.EventArgs e)
		{		
		   this.init();
			if(this.patientInfo!= null)
			{
				this.ucReportCancerRegister1.ShowInfo(this.patientInfo);
			}
		}
		#endregion

		#region ��ʾ�����ڱ༭ģ��Ŀؼ��б�
		private void showCanUsedControlList()
		{
			this.tvPatientInfo.Nodes.Clear();
            for (int i = 0; i < (int)FS.SOC.HISFC.Components.DCP.Classes.EnumControl.End; i++)
			{
				System.Windows.Forms.TreeNode node = new TreeNode();
                node.Text = ((FS.SOC.HISFC.Components.DCP.Classes.EnumControl)i).ToString();
				this.tvPatientInfo.Nodes.Add(node);
			}

		}
		#endregion
		
		#region ��ѯ

		private void cmbQueryContent_SelectedValueChanged(object sender, EventArgs e)
		{
			this.ucReportCancerRegister1.InfectCode = "";
			
//			if(this.cmbQueryContent.SelectedValue.ToString() == "patientInfo")
//			{
//				//���߻�����Ϣ��ѯ
//				this.dtBegin.Enabled = true;
//				this.dtEnd.Enabled = true;
//				this.groupBox3.Enabled = true;
//				this.tvPatientInfo.Nodes.Clear();
//				this.txtReportNo.Enabled = false;
//				this.txtDoctor.Enabled = false;
//				this.txtName.Enabled = true;
//				this.txtInPatienNo.Enabled = true;
//			}
//			else if(this.cmbQueryContent.SelectedValue.ToString() == "reportInfo")
//			{
//				//���߱�����Ϣ��ѯ
//				this.dtBegin.Enabled = true;
//				this.dtEnd.Enabled = true;
//				this.groupBox3.Enabled = true;			
//				this.tvPatientInfo.Nodes.Clear();
//				this.txtReportNo.Enabled = true;
//				this.txtDoctor.Enabled = true;
//				this.txtInPatienNo.Enabled = true;
//				this.txtName.Enabled = true;
//			}
//			else if(this.cmbQueryContent.SelectedValue.ToString() == "DeptReport" 
//				|| this.cmbQueryContent.SelectedValue.ToString() == "DeptUnReport")
//			{
//				//���ұ�����Ϣ��ѯ
//				this.dtBegin.Enabled = true;
//				this.dtEnd.Enabled = true;
//				this.groupBox3.Enabled = true;
//				this.txtDoctor.Enabled = false;
//				this.txtInPatienNo.Enabled = false;
//				this.txtName.Enabled = false;
//				this.txtReportNo.Enabled = false;
//				this.tvPatientInfo.Nodes.Clear();				
//				//this.mySearchOldReport();
//			}
//			else if(this.cmbQueryContent.SelectedValue.ToString() == "choose")
//			{
//				this.groupBox3.Enabled = true;
//				this.txtDoctor.Enabled = false;
//				this.txtInPatienNo.Enabled = false;
//				this.txtName.Enabled = false;
//				this.txtReportNo.Enabled = false;
//				this.dtBegin.Enabled = false;
//				this.dtEnd.Enabled = false;
//				this.tvPatientInfo.Nodes.Clear();				
//				//this.mySearchOldReport();
//			}
//			else if(this.cmbQueryContent.SelectedValue.ToString() == "deptLisResult")
//			{
//				this.groupBox3.Enabled = true;
//				this.txtDoctor.Enabled = false;
//				this.txtInPatienNo.Enabled = false;
//				this.txtName.Enabled = false;
//				this.txtReportNo.Enabled = false;
//				this.dtBegin.Enabled = false;
//				this.dtEnd.Enabled = false;
//				this.tvPatientInfo.Nodes.Clear();	
//			}
		}

		private void txtReportNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				//���濨�����Ų�ѯ
				this.ucReportCancerRegister1.InfectCode = "";
				this.query();
			}
		}

		private void txtInPatienNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				//���ߺŲ�ѯ
				this.ucReportCancerRegister1.InfectCode = "";
				this.query();
			}
		}

		private void txtName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				//���ݻ�������ģ����ѯ
				this.ucReportCancerRegister1.InfectCode = "";
				this.query();
			}
		}
		
		private void txtDoctor_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				//����ҽ�����Ų�ѯ��ҽ������д����
				this.ucReportCancerRegister1.InfectCode = "";
				this.query();
			}
		}

		private void menuPatientBaseInfo_Click(object sender, System.EventArgs e)
		{
			//���߻�����Ϣ��ѯ
			this.groupBox3.Enabled = true;
			this.tvPatientInfo.Nodes.Clear();
			this.txtReportNo.Enabled = false;
			this.txtDoctor.Enabled = false;
		}

		private void menuPatientReportInfo_Click(object sender, System.EventArgs e)
		{
			//���߱�����Ϣ��ѯ
			this.groupBox3.Enabled = true;			
			this.tvPatientInfo.Nodes.Clear();
			this.txtReportNo.Enabled = true;
			this.txtDoctor.Enabled = true;
		} 

		private void menuDeptReportInfo_Click(object sender, System.EventArgs e)
		{
			//���ұ�����Ϣ��ѯ
			this.groupBox3.Enabled = false;
			this.tvPatientInfo.Nodes.Clear();
			this.mySearchOldReport();
		}

		
		#endregion

		private void menuItemNew_Click(object sender, System.EventArgs e)
		{
			if(this.isContinue("ȷ���½�һ�ű��濨��"))
			{
				//this.ucReportCancerRegister1.ClearForAdd();
				this.ucReportCancerRegister1.SetEnable(true);
				this.tbbSave.Enabled = true;
			}
		}

		private void llbPatienNO_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(!this.ucReportCancerRegister1.IsAdvance)
			{
				return;
			}
			if(this.llbPatienNO.Tag.ToString() == "C")
			{
				this.llbPatienNO.Text = "ס Ժ ��";
				this.llbPatienNO.Tag = "I";
			}
			else
			{
				this.llbPatienNO.Text = "���￨��";
				this.llbPatienNO.Tag = "C";
			}
		}

		
		#endregion

		private void frmReportRegister_Resize(object sender, System.EventArgs e)
		{
			this.txtAddress.Width = this.statusBar1.Panels[1].Width+2;
			this.txtAddress.Location = new Point(192,this.statusBar1.Location.Y+2);
		}
		
		#region ��ѯ����ö��
		enum enumQueryType
		{
			ȫԺ������ѯ,
			�����ұ�����ѯ,
			�����Ҳ��ϸ񱨿���ѯ,
			ȫԺ������Ϣ��ѯ,
			//�����Ҳ�����Ϣ��ѯ,
			������������ѯ,
			End
		}
		#endregion
	}
}