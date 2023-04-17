using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.Query
{
	/// <summary>
	/// ��ѯ������
	/// ���������ѯ����
	/// </summary>
	[DefaultProperty("index"),DefaultEvent("RelationOperatorSelectedIndexChanged")]
	public class ucCondition : System.Windows.Forms.UserControl
	{
//		public event System.EventHandler SelectTree;
		[Category("Action"),Description("�ڹ�ϵ������ı�ʱ����")]
		public event System.EventHandler RelationOperatorSelectedIndexChanged;

		private ArrayList arrRelationOperator;
		private ArrayList arrCompareOperator;
		private bool IsAddValue = false;
		private System.Windows.Forms.GroupBox grpCondition;
		private System.Windows.Forms.Button btnSearchTree;
		private System.Windows.Forms.TextBox txtTree;
		private System.Windows.Forms.TextBox txtCompareText;
		private System.Windows.Forms.ComboBox cboCompareOperator;
		private System.Windows.Forms.ComboBox cboRelationOperator;

		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucCondition()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();
			initCompareOperatorItems();
			initRelationOperatorItems();
			AddCompareOperatorItems();
			AddRelationOperatorItems();


			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

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
			this.grpCondition = new System.Windows.Forms.GroupBox();
			this.cboRelationOperator = new System.Windows.Forms.ComboBox();
			this.cboCompareOperator = new System.Windows.Forms.ComboBox();
			this.txtCompareText = new System.Windows.Forms.TextBox();
			this.txtTree = new System.Windows.Forms.TextBox();
			this.btnSearchTree = new System.Windows.Forms.Button();
			this.grpCondition.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpCondition
			// 
			this.grpCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpCondition.Controls.Add(this.cboRelationOperator);
			this.grpCondition.Controls.Add(this.cboCompareOperator);
			this.grpCondition.Controls.Add(this.txtCompareText);
			this.grpCondition.Controls.Add(this.txtTree);
			this.grpCondition.Controls.Add(this.btnSearchTree);
			this.grpCondition.Location = new System.Drawing.Point(0, 2);
			this.grpCondition.Name = "grpCondition";
			this.grpCondition.Size = new System.Drawing.Size(552, 44);
			this.grpCondition.TabIndex = 0;
			this.grpCondition.TabStop = false;
			this.grpCondition.Text = "��ѯ����";
			// 
			// cboRelationOperator
			// 
			this.cboRelationOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboRelationOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRelationOperator.Items.AddRange(new object[] {
																	 "����"});
			this.cboRelationOperator.Location = new System.Drawing.Point(464, 16);
			this.cboRelationOperator.Name = "cboRelationOperator";
			this.cboRelationOperator.Size = new System.Drawing.Size(80, 20);
			this.cboRelationOperator.TabIndex = 4;
			this.cboRelationOperator.SelectedIndexChanged += new System.EventHandler(this.cboRelationOperator_SelectedIndexChanged);
			// 
			// cboCompareOperator
			// 
			this.cboCompareOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboCompareOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCompareOperator.Items.AddRange(new object[] {
																	"����",
																	"������",
																	"����",
																	"������",
																	"����",
																	"С��",
																	"���ڻ����",
																	"С�ڻ����"});
			this.cboCompareOperator.Location = new System.Drawing.Point(208, 16);
			this.cboCompareOperator.Name = "cboCompareOperator";
			this.cboCompareOperator.Size = new System.Drawing.Size(88, 20);
			this.cboCompareOperator.TabIndex = 2;
			// 
			// txtCompareText
			// 
			this.txtCompareText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCompareText.Location = new System.Drawing.Point(304, 16);
			this.txtCompareText.Name = "txtCompareText";
			this.txtCompareText.Size = new System.Drawing.Size(152, 21);
			this.txtCompareText.TabIndex = 3;
			this.txtCompareText.Text = "";
			// 
			// txtTree
			// 
			this.txtTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtTree.Location = new System.Drawing.Point(8, 16);
			this.txtTree.Name = "txtTree";
			this.txtTree.Size = new System.Drawing.Size(152, 21);
			this.txtTree.TabIndex = 0;
			this.txtTree.Text = "";
			// 
			// btnSearchTree
			// 
			this.btnSearchTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearchTree.Location = new System.Drawing.Point(168, 16);
			this.btnSearchTree.Name = "btnSearchTree";
			this.btnSearchTree.Size = new System.Drawing.Size(32, 21);
			this.btnSearchTree.TabIndex = 1;
			this.btnSearchTree.Text = "...";
			this.btnSearchTree.Click += new System.EventHandler(this.btnSearchTree_Click);
			// 
			// ucCondition
			// 
			this.Controls.Add(this.grpCondition);
			this.Name = "ucCondition";
			this.Size = new System.Drawing.Size(552, 50);
			this.grpCondition.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region ����

		/// <summary>
		/// ��0��ʼ����������
		/// </summary>
		[Category("Design"),Description("��0��ʼ����������")]
		public int Index
		{
			set
			{
				string str;
				if(value < -1)
				{
					value = -1;
					return;
				}
				str = (value + 1).ToString();

				grpCondition.Text = "��ѯ����" + str + "(&" + str + ")";
			}
			get
			{
				int i = 0;
				int len = this.grpCondition.Text.Length;
				if(len > 7)
				{
					i = int.Parse(this.grpCondition.Text.Substring(4, (int)(len - 7) / 2));
				}

				return --i;
			}
		}

		/// <summary>
		/// �Ƚ��������Ŀ
		/// </summary>
		[Description("�Ƚ��������Ŀ")]
		public ArrayList CompareOperatorItems
		{
			set
			{
				AddCompareOperatorItems(value);
			}
		}

		/// <summary>
		/// ��ϵ�������Ŀ
		/// </summary>
		[Description("��ϵ�������Ŀ")]
		public ArrayList RelationOperatorItems
		{
			set
			{
				AddRelationOperatorItems(value);
			}
		}

		/// <summary>
		/// ������ʼ����ֵ�Ľڵ�
		/// </summary>
		[Description("������ʼ����ֵ�Ľڵ�")]
		public System.Xml.XmlNode Node
		{
			set
			{
				this.IsAddValue = true;
				AddNodeValueToConditionGroup(value);
				this.IsAddValue = false;
			}
		}


		/// <summary>
		/// �Ƿ���������²�ѯ����
		/// </summary>
		[Category("Behavior"),Description("�Ƿ���������²�ѯ����")]
		public bool AllowNewCondition
		{
			get
			{
				return this.cboRelationOperator.Enabled;
			}
			set
			{
				this.cboRelationOperator.Enabled = value;
			}
		}

		/// <summary>
		/// ��ѯ�ֶ�
		/// </summary>
		[Description("��ѯ�ֶ�"),Browsable(false)]
		public string Field
		{
			get 
			{
				return this.txtTree.Text;
			}
		}

		/// <summary>
		/// �Ƚ������
		/// </summary>
		[Description("�Ƚ������"),Browsable(false)]
		public string CompareOperator
		{
			get
			{
				if(this.cboCompareOperator.SelectedIndex == -1)
				{
					return "";
				}
				else
				{
					return this.cboCompareOperator.SelectedValue.ToString();
				}
			}
		}


		/// <summary>
		/// �Ƚ�����1
		/// </summary>
		[Description("�Ƚ�����1"),Browsable(false)]
		public string CompareContext1
		{
			get
			{
				return this.txtCompareText.Text;
			}
		}

		/// <summary>
		/// �Ƚ�����2
		/// </summary>
		[Description("�Ƚ�����2"),Browsable(false)]
		public string CompareContext2
		{
			get
			{
				return "";
			}
		}

		/// <summary>
		/// ��ϵ�����
		/// </summary>
		[Description("��ϵ�����"),Browsable(false)]
		public string RelationOperator
		{
			get
			{
				if(this.cboRelationOperator.SelectedIndex == -1)
				{
					return "";
				}
				else
				{
					return this.cboRelationOperator.SelectedValue.ToString();
				}
			}
		}
		#endregion ����

		#region ����

		/// <summary>
		/// ���������ڵ�ֵ�ŵ������ؼ���
		/// </summary>
		/// <param name="node"></param>
		private void AddNodeValueToConditionGroup(System.Xml.XmlNode node)
		{
			if(node == null) return;
			if(node.Attributes["Tree"] != null)
			{
				this.txtTree.Text = node.Attributes["Tree"].Value;
			}
			if(node.Attributes["CompareOperator"] != null)
			{
				string strOperator = node.Attributes["CompareOperator"].Value;
				if(strOperator != "")
				{
					for(int i = 0; i< this.cboCompareOperator.Items.Count; i++)
					{
						FS.FrameWork.Models.NeuObject obj = (FS.FrameWork.Models.NeuObject)this.arrCompareOperator[i];
						
						if(obj.ID == strOperator)
						{
							this.cboCompareOperator.SelectedIndex = i;
							break;
						}
					}
				}
			}

			if(node.Attributes["CompareContext1"] != null)
			{
				this.txtCompareText.Text = node.Attributes["CompareContext1"].Value;
			}

			if(node.Attributes["RelationOperator"] != null)
			{
				string strOperator = node.Attributes["RelationOperator"].Value;
				if(strOperator != "")
				{
					for(int i = 0; i< this.cboRelationOperator.Items.Count; i++)
					{
						FS.FrameWork.Models.NeuObject obj = (FS.FrameWork.Models.NeuObject)this.arrRelationOperator[i];
						if(obj.ID == strOperator)
						{
//							this.cboCompareOperator.SelectedIndexChanged -= new System.EventHandler(this.cboRelationOperator_SelectedIndexChanged);
							this.cboRelationOperator.SelectedIndex = i;
//							this.cboRelationOperator.SelectedIndexChanged +=new EventHandler(cboRelationOperator_SelectedIndexChanged);
							break;
						}
					}
				}
			}
		}

		
		/// <summary>
		/// ���������ù�ϵ�������ı��¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnRelationOperatorSelectedIndexChanged(object sender, EventArgs e)
		{
			if(this.RelationOperatorSelectedIndexChanged != null)
			{
				RelationOperatorSelectedIndexChanged(sender, e);
			}
		}

		private void AddCompareOperatorItems()
		{
			if(this.arrCompareOperator == null || this.arrCompareOperator.Count == 0) return;
			this.cboCompareOperator.Items.Clear();
			System.Data.DataTable dt = new System.Data.DataTable("Relation");
			dt.Columns.Add("ID");
			dt.Columns.Add("Name");
			foreach(object obj in this.arrCompareOperator)
			{
				FS.FrameWork.Models.NeuObject neuobj = (FS.FrameWork.Models.NeuObject)obj;
				DataRow dr = dt.NewRow();
				dr["ID"]=neuobj.ID;
				dr["Name"]=neuobj.Name;
				dt.Rows.Add(dr);
			}
			this.cboCompareOperator.DataSource = dt;
			this.cboCompareOperator.DisplayMember = "Name";
			this.cboCompareOperator.ValueMember = "ID";
//			foreach(object obj in al)
//			{
//				FS.FrameWork.Models.NeuObject neuobj = (FS.FrameWork.Models.NeuObject)obj;
//				this.cboCompareOperator.Items.Add(neuobj.Name);
//			}
		}

		/// <summary>
		/// ��ʼ���Ƚ������
		/// </summary>
		/// <param name="al">������ѡ��</param>
		private void AddCompareOperatorItems(ArrayList al)
		{
			this.arrCompareOperator = al;
			this.AddCompareOperatorItems();
		}

		/// <summary>
		/// ��ʼ����ϵ�����
		/// </summary>
		/// <param name="al">������ѡ��</param>
		private void AddRelationOperatorItems(ArrayList al)
		{
			this.arrRelationOperator = al;
			this.AddRelationOperatorItems();
		}

		/// <summary>
		/// ��ʼ����ϵ�����
		/// </summary>
		/// <param name="al">������ѡ��</param>
		private void AddRelationOperatorItems()
		{
			if(this.arrRelationOperator == null || this.arrRelationOperator.Count == 0) return;
			this.cboRelationOperator.Items.Clear();

			System.Data.DataTable dt = new System.Data.DataTable("Relation");
			dt.Columns.Add("ID");
			dt.Columns.Add("Name");
			foreach(object obj in this.arrRelationOperator)
			{
				FS.FrameWork.Models.NeuObject neuobj = (FS.FrameWork.Models.NeuObject)obj;
				DataRow dr = dt.NewRow();
				dr["ID"]=neuobj.ID;
				dr["Name"]=neuobj.Name;
				dt.Rows.Add(dr);
			}
			this.cboRelationOperator.DataSource = dt;
			this.cboRelationOperator.DisplayMember = "Name";
			this.cboRelationOperator.ValueMember = "ID";

//			foreach(object obj in al)
//			{
//				FS.FrameWork.Models.NeuObject neuobj = (FS.FrameWork.Models.NeuObject)obj;
//				this.cboRelationOperator.Items.Add(neuobj.Name);
//			}
		}

		/// <summary>
		/// �Ƚ��������Ŀ
		/// </summary>
		/// <returns></returns>
		private void initCompareOperatorItems()
		{
			this.arrCompareOperator = new ArrayList();
			FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "Like";
			obj.Name = "����";
			this.arrCompareOperator.Add(obj);

			obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "not Like";
			obj.Name = "������";
			this.arrCompareOperator.Add(obj);

			obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "=";
			obj.Name = "����";
			this.arrCompareOperator.Add(obj);

			obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "<>";
			obj.Name = "������";
			this.arrCompareOperator.Add(obj);

			obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = ">";
			obj.Name = "����";
			this.arrCompareOperator.Add(obj);

			obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "<";
			obj.Name = "С��";
			this.arrCompareOperator.Add(obj);

			obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = ">=";
			obj.Name = "���ڻ����";
			this.arrCompareOperator.Add(obj);

			obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "<=";
			obj.Name = "С�ڻ����";
			this.arrCompareOperator.Add(obj);
		}

		/// <summary>
		/// ��ϵ�������ϣ��
		/// </summary>
		/// <returns></returns>
		private void initRelationOperatorItems()
		{
			this.arrRelationOperator = new ArrayList();

			FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "or";
			obj.Name = "����";
			this.arrRelationOperator.Add(obj);

			obj = new FS.FrameWork.Models.NeuObject();
			obj.ID = "or";
			obj.Name = "����";
			this.arrRelationOperator.Add(obj);
		}

		#endregion ����

		#region �¼�
		/// <summary>
		/// ѡ������ť�����¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearchTree_Click(object sender, System.EventArgs e)
		{
			
			Function.FormSelectTree.ShowDialog();
			if(Function.FormSelectTree.DialogResult == DialogResult.OK)
			{
				if(Function.FormSelectTree.NodeFullPath != null)
				{
					this.txtTree.Text = Function.FormSelectTree.NodeFullPath;
				}
			}
		}

		/// <summary>
		/// ��ϵ�������ı��¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboRelationOperator_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(!IsAddValue)
			{
				OnRelationOperatorSelectedIndexChanged(this, e);
			}
		}

		#endregion �¼�

	}
}
