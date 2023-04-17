using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace FS.HISFC.Components.EPR.Query
{

	/// <summary>
	/// ucConditions ��ժҪ˵����
	/// </summary>
	public class ucConditions : System.Windows.Forms.UserControl
	{
		[Category("Action"),Description("������ѯ��ťʱ����")]
		public event QueryHandler Query;
		[Category("Action"),Description("����ȡ����ťʱ����")]
		public event EventHandler Cancel;
		[Category("Action"),Description("�ڲ�ѯ�������������ı�ʱ����")]
		public event EventHandler ConditionCountChanged;

//		private string _QueryConditionFileName = "";
		private int _MaxConditionCount = 4;
		private int _initHeight = 40;
		private int _top = 8;
		private int _left = 8;
		private int _height = 50;
		public int ControlHeight = 50;

		protected ArrayList alOperations;
		protected ArrayList alRelations;

//		private FS.HISFC.Management.Manager.QueryCondition managerCondition;
		public System.Windows.Forms.Button btnOK;
		public System.Windows.Forms.Button btnCanel;
		public System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Panel pnlConditionGroup;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// ���캯��
		/// </summary>
		public ucConditions()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();
//			this.managerCondition = new FS.HISFC.Management.Manager.QueryCondition();
			this.Load+=new EventHandler(ucConditions_Load);

//			this._MaxConditionCount = MaxConditionCount;
//			_QueryConditionFileName = Application.StartupPath + "\\QueryCondition.xml";
//
//			//������ڲ�ѯ�����ļ�����Ӳ�ѯ�����ļ�ȡ�ڵ�ֵ��������ѯ���������򣬴���һ���յĽڵ��ѯ����
//			if(System.IO.File.Exists(_QueryConditionFileName))
//			{
//				this.AddCondition(_QueryConditionFileName);
//			}
//			else
//			{
//				AddCondition();
//			}
//			this.InitRelationOperatorHashtable();
//			this.InitCompareOperatorHashtable();
		}

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

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCanel = new System.Windows.Forms.Button();
			this.btnDel = new System.Windows.Forms.Button();
			this.pnlConditionGroup = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnOK.Location = new System.Drawing.Point(392, 12);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(64, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "ȷ��";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCanel
			// 
			this.btnCanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCanel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnCanel.Location = new System.Drawing.Point(86, 13);
			this.btnCanel.Name = "btnCanel";
			this.btnCanel.Size = new System.Drawing.Size(64, 24);
			this.btnCanel.TabIndex = 0;
			this.btnCanel.Text = "ȡ��";
			this.btnCanel.Visible = false;
			this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
			// 
			// btnDel
			// 
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDel.Location = new System.Drawing.Point(464, 12);
			this.btnDel.Name = "btnDel";
			this.btnDel.TabIndex = 2;
			this.btnDel.Text = "ɾ��...";
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// pnlConditionGroup
			// 
			this.pnlConditionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlConditionGroup.Location = new System.Drawing.Point(0, 0);
			this.pnlConditionGroup.Name = "pnlConditionGroup";
			this.pnlConditionGroup.Size = new System.Drawing.Size(552, 8);
			this.pnlConditionGroup.TabIndex = 1;
			this.pnlConditionGroup.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.pnlConditionGroup_ControlChanged);
			this.pnlConditionGroup.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.pnlConditionGroup_ControlChanged);
			// 
			// ucConditions
			// 
			this.Controls.Add(this.pnlConditionGroup);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCanel);
			this.Controls.Add(this.btnDel);
			this.Name = "ucConditions";
			this.Size = new System.Drawing.Size(552, 40);
			this.ResumeLayout(false);

		}
		#endregion
		#region ����
		/// <summary>
		/// ���в�ѯ��������
		/// </summary>
		private int _CurConditionCount
		{
			get
			{
				return this.pnlConditionGroup.Controls.Count;
			}
		}


		/// <summary>
		/// ��������ѯ����������
		/// </summary>
		[Category("Design"),Description("��������ѯ����������")]
		public int MaxConditionCount
		{
			get
			{
				return _MaxConditionCount;
			}
			set
			{
				_MaxConditionCount = value;
				AddCondition();
//				InitCondition();
			}
		}

//		/// <summary>
//		/// ��ѯ���������ļ�����
//		/// </summary>
//		[Category("Design"),Description("��ѯ���������ļ�����")]
//		public string SettingFileName
//		{
//			get
//			{
//				return this._QueryConditionFileName;
//			}
//			set
//			{
//				this._QueryConditionFileName = value;
//				InitCondition();
//			}
//		}

		#endregion ����

		#region ����

		private void InitCondition()
		{
			this.pnlConditionGroup.Controls.Clear();
			//������ڲ�ѯ�����ļ�����Ӳ�ѯ�����ļ�ȡ�ڵ�ֵ��������ѯ���������򣬴���һ���յĽڵ��ѯ����
			if(this._MaxConditionCount == 0) return;
//			if(this._QueryConditionFileName == null || this._QueryConditionFileName == "") return;

			string s = "";
			if(this.FindForm() ==null) return;
			try
			{
                s = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetQueryCondtion(this.FindForm().Name);
			}
			catch{return ;}
			if(s =="-1")
			{
				AddCondition();
				return;
			}
			if(s =="")
                s = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetQueryCondtion(this.FindForm().Name, false);

			if(s =="")
			{
				AddCondition();
				return;
			}

			XmlDocument doc = new XmlDocument();
			try
			{
				doc.LoadXml(s);
			}
			catch//(Exception ex)
			{
				AddCondition();
				return ;
			}

			XmlNodeList nodes = doc.SelectNodes("/SETTING/Condition");
			if(nodes.Count == 0)
			{
				AddCondition();
				return ;
			}

			foreach(XmlNode node in nodes)
			{
				AddCondition(node);

				if(this.pnlConditionGroup.Controls.Count == this._MaxConditionCount)
				{
					break;
				}
			}

//			if(System.IO.File.Exists(Application.StartupPath + "\\" + _QueryConditionFileName))
//			{
//				this.AddCondition(Application.StartupPath + "\\" + _QueryConditionFileName);
//			}
//			else
//			{
//				AddCondition();
//			}
//			this.RelocateConditionGroup();
		}

		/// <summary>
		/// ������ɾ��ָ��������
		/// </summary>
		/// <param name="index"></param>
		private void DeleteCondition(int index)
		{
			this.pnlConditionGroup.Controls.RemoveAt(index);
		}

		/// <summary>
		/// ���������������ַ���ɾ������
		/// </summary>
		/// <param name="index"></param>
		private void DeleteCondition(string strDelCondition)
		{
			if(strDelCondition == null || strDelCondition.Length ==0) return;
			string[] arrDelCondition = strDelCondition.Split(';');
			for(int i = arrDelCondition.Length - 1; i >= 0; i--)
			{
				if(bool.Parse(arrDelCondition[i]))
				{
					DeleteCondition(i);
				}
			}

			if(this._CurConditionCount == 0)
			{
				AddCondition();
			}
		}

		/// <summary>
		/// �ܱ����ķ���������Query�¼�
		/// </summary>
		/// <param name="e"></param>
		protected void OnQuery(QueryEventArgs e)
		{
			if(this.Query != null)
			{
				this.Query(this, e);
			}
		}

		/// <summary>
		/// �ܱ����ķ���������Cancel�¼�
		/// </summary>
		/// <param name="e"></param>
		protected void OnCancel(EventArgs e)
		{
			if(this.Cancel != null)
			{
				this.Cancel(this, e);
			}
		}
		/// <summary>
		/// ����������������
		/// </summary>
		private ucCondition AddCondition()
		{
			bool AllowNewCondition = _CurConditionCount == _MaxConditionCount - 1;
			ucCondition condition = new ucCondition();
			condition.Index = _CurConditionCount;
			condition.AllowNewCondition = AllowNewCondition;
//			condition.SelectTree += new EventHandler(group_SelectTree);
			condition.RelationOperatorSelectedIndexChanged += new EventHandler(group_RelationOperatorSelectedIndexChanged);

			condition.Location = new Point(_left, _top + _height * _CurConditionCount);
			condition.Width = this.Width;
			condition.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top; 
			this.pnlConditionGroup.Controls.Add(condition);
			return condition;
		}

		/// <summary>
		/// ���������������������Ҹ�ֵ
		/// </summary>
		/// <param name="node">���ڵ�ֵ</param>
		/// <returns></returns>
		private ucCondition AddCondition(System.Xml.XmlNode node)
		{
//			bool AllowNewCondition = _CurConditionCount == _MaxConditionCount - 1;
//			ucCondition condition = new ucCondition();
//			
//			condition.Index = _CurConditionCount;
//			condition.AllowNewCondition = AllowNewCondition;
////			condition.SelectTree += new EventHandler(group_SelectTree);
//			condition.RelationOperatorSelectedIndexChanged += new EventHandler(group_RelationOperatorSelectedIndexChanged);
//
//			condition.Location = new Point(_left, _top + _height * _CurConditionCount);
//			this.pnlConditionGroup.Controls.Add(condition);
			ucCondition condition = this.AddCondition();
			condition.Node = node;
			return condition;
		}

		/// <summary>
		/// ��������ָ�����ļ���ȡ��ʼ����
		/// </summary>
		/// <param name="FileName"></param>
//		private void AddCondition(bool AddBla)
//		{
//			//����ļ������ڻ����ļ����ɶ������ļ��ڵ���Ϊ0������һ���յ�����
//			//����
//			//ѭ��
//			//�������ֵ�ڵ�
//			string s = "";
//			if(this.FindForm() ==null) return;
//			try
//			{
//				s = this.managerCondition.GetQueryCondtion(this.FindForm().Name);
//			}
//			catch{return ;}
//			if(s =="-1")
//			{
//				MessageBox.Show(this.managerCondition.Err);
//				return;
//			}
//			if(s =="")
//				s = this.managerCondition.GetQueryCondtion(this.FindForm().Name,true);
//
//			if(s =="") return;
//			
//			XmlDocument doc = new XmlDocument();
//			try
//			{
//				doc.LoadXml(s);
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show(ex.Message);
//				return ;
//			}
//
//			XmlNodeList nodes = doc.SelectNodes("/SETTING/Condition");
//			foreach(XmlNode node in nodes)
//			{
//				AddCondition(node);
//
//				if(this.pnlConditionGroup.Controls.Count == this._MaxConditionCount)
//				{
//					break;
//				}
//			}
//		}
//
		/// <summary>
		/// ��ѯ������λ
		/// </summary>
		private void RelocateConditionGroup()
		{
			for(int i = 0; i < _CurConditionCount; i++)
			{
				ucCondition condition = (ucCondition)this.pnlConditionGroup.Controls[i];
				condition.Index = i;
//				condition.Text = "��ѯ����" + (i+1).ToString() + "(&" + (i+1).ToString() + ")";
				condition.Location = new Point(_left, _top + (i) * _height);
				if(i < _MaxConditionCount - 1)
				{
					condition.AllowNewCondition = true;
				}
				else
				{
					condition.AllowNewCondition = false;
				}
			}

//			if(_CurConditionCount > 0)
//			{
//				((ucCondition)this.pnlConditionGroup.Controls[_CurConditionCount - 1]).IsLastCondition = true;
//			}
			this.Height = _initHeight + _height * _CurConditionCount;
			this.ControlHeight = _initHeight + _height * _CurConditionCount;
		}

		/// <summary>
		/// �����ѯ����
		/// </summary>
		private void SaveQueryCondition()
		{
            FS.FrameWork.Xml.XML xml = new FS.FrameWork.Xml.XML();
			XmlDocument doc = new XmlDocument();
			XmlElement root = xml.CreateRootElement(doc,"SETTING","1.0");

			for(int i = 0; i < this._CurConditionCount; i++)
			{
				//��Ӳ�ѯ�����ڵ�
				ucCondition condition = (ucCondition)this.pnlConditionGroup.Controls[i];
				XmlElement xmlCondition = xml.AddXmlNode(doc,root,"Condition","");
				xml.AddNodeAttibute(xmlCondition, "Tree", condition.Field);
				xml.AddNodeAttibute(xmlCondition, "CompareOperator", condition.CompareOperator);
				xml.AddNodeAttibute(xmlCondition, "CompareContext1", condition.CompareContext1);
				xml.AddNodeAttibute(xmlCondition, "RelationOperator", condition.RelationOperator);
				xml.AddNodeAttibute(xmlCondition, "CompareContext2", condition.CompareContext2);
			}
            if (FS.HISFC.BizProcess.Factory.Function.IntegrateManager.SetQueryCondition(this.FindForm().Name, doc.OuterXml, false) == -1)
			{
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err);
			}
//			else
//			{
//				MessageBox.Show("����ģ�屣��ɹ���");
//			}
//
//			doc.Save(Application.StartupPath + "\\" + this._QueryConditionFileName);
		}

		/// <summary>
		/// ȡ��ѯ����
		/// </summary>
		/// <returns></returns>
		public ArrayList GetQueryCondition()
		{
			ArrayList ar = new ArrayList();

			for(int i = 0; i < this._CurConditionCount; i++)
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
				ucCondition condition = (ucCondition)this.pnlConditionGroup.Controls[i];
				obj.ID = condition.Field;
				obj.Name = condition.CompareOperator;
				obj.Memo = condition.CompareContext1;
				obj.User01 = condition.RelationOperator;
				obj.User02 = condition.CompareContext2;
				ar.Add(obj);
			}
			return ar;
		}

		#endregion ����

		#region �¼�
		/// <summary>
		/// ȷ����ť�����¼�
		/// �������������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			SaveQueryCondition();
			QueryEventArgs arg = new QueryEventArgs();

			arg.QueryCondition = GetQueryCondition();
			OnQuery(arg);
		}

		/// <summary>
		/// ȡ����ť�����¼�
		/// ���ؿ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCanel_Click(object sender, System.EventArgs e)
		{
			System.EventArgs arg = new EventArgs();
			OnCancel(arg);
		}

		/// <summary>
		/// ɾ����ť�����¼�
		/// ɾ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDel_Click(object sender, System.EventArgs e)
		{
			frmDeleteCondition frmDelCondition = new frmDeleteCondition(_CurConditionCount);
			frmDelCondition.ShowDialog();
			if(frmDelCondition.DialogResult == DialogResult.OK)
			{
				string strDelCondition = frmDelCondition.deleteConditions;
				DeleteCondition(strDelCondition);
			}
		}

		/// <summary>
		/// ��ϵ������ı��¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void group_RelationOperatorSelectedIndexChanged(object sender, EventArgs e)
		{
			ucCondition condition = (ucCondition)sender;
			if(condition.RelationOperator == "") return;
			ucCondition lastGroup = (ucCondition)this.pnlConditionGroup.Controls[condition.Parent.Controls.Count - 1];
			if(condition == lastGroup)
			{
				this.AddCondition();
			}
		}

//		/// <summary>
//		/// ѡ������ť�����¼�
//		/// </summary>
//		/// <param name="sender"></param>
//		/// <param name="e"></param>
//		private void group_SelectTree(object sender, EventArgs e)
//		{
//
//		}

		/// <summary>
		/// ��ѯ������Ŀ�ı�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pnlConditionGroup_ControlChanged(object sender, ControlEventArgs e)
		{
			this.RelocateConditionGroup();
			if(this.ConditionCountChanged != null)
			{
				EventArgs arg = new EventArgs();
				this.ConditionCountChanged(this, arg);
			}
		}

		private void ucConditions_Load(object sender, EventArgs e)
		{
			this.InitCondition();
			this.RelocateConditionGroup();
		}
		#endregion �¼�
	}


	/// <summary>
	/// Query �¼�������
	/// </summary>
	public class QueryEventArgs
	{
		/// <summary>
		/// ��ѯ����
		/// </summary>
		public ArrayList QueryCondition;
	}

	/// <summary>
	/// Query�¼�ί��
	/// </summary>
	public delegate void QueryHandler(System.Object sender, QueryEventArgs e);
}
