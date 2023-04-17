using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.QC
{
	/// <summary>
	/// ucQCRuleInput ��ժҪ˵����
	/// </summary>
	public class ucQCRuleInput : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnModify;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.ListBox lst;
		private System.Windows.Forms.RichTextBox rtbMemo;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucQCRuleInput()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

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
			this.lst = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.rtbMemo = new System.Windows.Forms.RichTextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnModify = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lst
			// 
			this.lst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lst.ItemHeight = 12;
			this.lst.Location = new System.Drawing.Point(16, 16);
			this.lst.Name = "lst";
			this.lst.Size = new System.Drawing.Size(240, 208);
			this.lst.TabIndex = 0;
			this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 232);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(296, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "����������";
			// 
			// rtbMemo
			// 
			this.rtbMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbMemo.Location = new System.Drawing.Point(16, 256);
			this.rtbMemo.Name = "rtbMemo";
			this.rtbMemo.Size = new System.Drawing.Size(352, 176);
			this.rtbMemo.TabIndex = 2;
			this.rtbMemo.Text = "";
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAdd.Location = new System.Drawing.Point(264, 16);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(96, 23);
			this.btnAdd.TabIndex = 3;
			this.btnAdd.Text = "���(&A)";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnModify
			// 
			this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnModify.Location = new System.Drawing.Point(264, 56);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(96, 23);
			this.btnModify.TabIndex = 4;
			this.btnModify.Text = "�޸�(&M)";
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDelete.Location = new System.Drawing.Point(264, 96);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(96, 23);
			this.btnDelete.TabIndex = 5;
			this.btnDelete.Text = "ɾ��(&D)";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnExit.Location = new System.Drawing.Point(264, 192);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(96, 23);
			this.btnExit.TabIndex = 6;
			this.btnExit.Text = "�˳�(&X)";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// ucQCRuleInput
			// 
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnModify);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.rtbMemo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lst);
			this.Name = "ucQCRuleInput";
			this.Size = new System.Drawing.Size(376, 440);
			this.Load += new System.EventHandler(this.ucQCRuleInput_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Exit();		
		}
		
		/// <summary>
		/// load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ucQCRuleInput_Load(object sender, System.EventArgs e)
		{
			this.Retrieve();
		}

		#region IToolBar ��Ա

		public ToolBarButton PreButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.PreButton getter ʵ��
				return null;
			}
		}

		public int Search()
		{
			// TODO:  ��� ucQCRuleInput.Search ʵ��
			return 0;
		}

		public ToolBarButton SaveButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.SaveButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton SearchButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.SearchButton getter ʵ��
				return null;
			}
		}

		public int Auditing()
		{
			// TODO:  ��� ucQCRuleInput.Auditing ʵ��
			return 0;
		}

		public int Del()
		{
			// TODO:  ��� ucQCRuleInput.Del ʵ��
			if(this.lst.SelectedIndex<0) return 0;
			if(MessageBox.Show("ȷ��ɾ��"+this.lst.SelectedItem.ToString()+"��?","��ʾ",MessageBoxButtons.OKCancel)== DialogResult.Cancel)
			{
				return 0;
			}
			FS.HISFC.BizProcess.Factory.Function.BeginTransaction();
            
			if(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.DeleteQCCondition(((FS.HISFC.Models.EPR.QCConditions)this.lst.SelectedItem).ID)==-1)
			{
                FS.HISFC.BizProcess.Factory.Function.RollBack();
				MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
				return -1;
			}
            FS.HISFC.BizProcess.Factory.Function.Commit();
			this.Retrieve();
			return 0;
		}

		public ToolBarButton AddButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.AddButton getter ʵ��
				return null;
			}
		}

		public int Print()
		{
			// TODO:  ��� ucQCRuleInput.Print ʵ��
			return 0;
		}

		public int Pre()
		{
			// TODO:  ��� ucQCRuleInput.Pre ʵ��
			return 0;
		}

		public ToolBarButton NextButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.NextButton getter ʵ��
				return null;
			}
		}

		public int Help()
		{
			// TODO:  ��� ucQCRuleInput.Help ʵ��
			return 0;
		}

		public int Next()
		{
			// TODO:  ��� ucQCRuleInput.Next ʵ��
			return 0;
		}

		public int Retrieve()
		{
			// TODO:  ��� ucQCRuleInput.Retrieve ʵ��
			ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCConditionList();
			if(al == null)
			{
				MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
				return -1;
			}
			
			this.lst.Items.Clear();
			for(int i=0;i<al.Count;i++)
			{
				lst.Items.Add(al[i]);
			}
			return 0;
		}

		public int Add()
		{
			// TODO:  ��� ucQCRuleInput.Add ʵ��
			ucQCRuleSet  uc = new ucQCRuleSet(new FS.HISFC.Models.EPR.QCConditions());
			uc.SelectedEvent+=new FS.FrameWork.WinForms.Forms.SelectedItemHandler(uc_SelectedEvent);
			FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
			return 0;
		}

		public ToolBarButton RetrieveButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.RetrieveButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton DelButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.DelButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton PrintButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.PrintButton getter ʵ��
				return null;
			}
		}

		public int Exit()
		{
			// TODO:  ��� ucQCRuleInput.Exit ʵ��
			this.FindForm().Close();
			return 0;
		}

		public int Save()
		{
			// TODO:  ��� ucQCRuleInput.Save ʵ��
			return 0;
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.Add();
		}
		
	

		public ToolBarButton AuditingButton
		{
			get
			{
				// TODO:  ��� ucQCRuleInput.AuditingButton getter ʵ��
				return null;
			}
		}

		#endregion

		private void uc_SelectedEvent(FS.FrameWork.Models.NeuObject sender)
		{
            FS.HISFC.BizProcess.Factory.Function.BeginTransaction();
			
			if(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.InsertQCCondition((FS.HISFC.Models.EPR.QCConditions)sender)==-1)
			{
                FS.HISFC.BizProcess.Factory.Function.RollBack();
				MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
				return;
			}
            FS.HISFC.BizProcess.Factory.Function.Commit();
			this.Retrieve();
		}
		private void uc_SelectedEvent1(FS.FrameWork.Models.NeuObject sender)
		{
            FS.HISFC.BizProcess.Factory.Function.BeginTransaction();
			
			if(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.UpdateQCCondition((FS.HISFC.Models.EPR.QCConditions)sender)==-1)
			{
				FS.HISFC.BizProcess.Factory.Function.RollBack();
				MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
				return;
			}
            FS.HISFC.BizProcess.Factory.Function.Commit();
			this.Retrieve();
		}

		private void btnModify_Click(object sender, System.EventArgs e)
		{
			if(this.lst.SelectedIndex<0) return;
			ucQCRuleSet  uc = new ucQCRuleSet((FS.HISFC.Models.EPR.QCConditions)this.lst.SelectedItem);
			uc.SelectedEvent+=new FS.FrameWork.WinForms.Forms.SelectedItemHandler(uc_SelectedEvent1);
			FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			this.Del();
		}

		private void lst_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.rtbMemo.Text = ((FS.HISFC.Models.EPR.QCConditions)this.lst.SelectedItem).Memo;
		}
	}
}
