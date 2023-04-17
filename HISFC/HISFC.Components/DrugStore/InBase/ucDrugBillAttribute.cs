using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.UFC.DrugStore.Inpatient
{
    public partial class ucDrugBillAttribute : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucDrugBillAttribute( )
        {
            InitializeComponent( );
        }
        private System.Windows.Forms.GroupBox groupBox1;
		private DrugStore.ucTreeViewChecked tvOrderType;
		private DrugStore.ucTreeViewChecked tvUsage;
		private DrugStore.ucTreeViewChecked tvDosageForm;
		private DrugStore.ucTreeViewChecked tvDrugQuality;
		private DrugStore.ucTreeViewChecked tvDrugType;
		private System.ComponentModel.IContainer components;

		//ҩ�������࣭����ҩ���������еķ���
		private neusoft.HISFC.Management.Pharmacy.DrugStore myDrugStore = new neusoft.HISFC.Management.Pharmacy.DrugStore(); 
		//�����࣭ȡ�����б�
		neusoft.HISFC.Management.Manager.Constant constant = new neusoft.HISFC.Management.Manager.Constant();

		/// <summary>
		/// ����ؼ��еİ�ҩ��������ϸ
		/// </summary>
		public int Save(DrugBillClass info, bool IsDelete) {
			//�жϴ�������Ƿ���Ч
			if (info.ID == "") {
				MessageBox.Show("���ڰ�ҩ�������б���ѡ��Ҫά���ļ�¼������");
				return -1;
			}

			//��ҽ����ҩ������ҩ��������������ϸ��Ϣ
			if (info.ID == "R"||info.ID == "P") {
				MessageBox.Show("�����Ұ�ҩ������Ҫ������ϸ��Ϣ��");
				return -1;
			}

			#region ȡ�����б��б�ѡ�е��������Ƿ�©ѡ
			//ҽ������
			ArrayList alOrderType = this.GetSelectedItems(tvOrderType);
			if (alOrderType.Count == 0) {
				MessageBox.Show("��ѡ��ҽ������");
				return -1;
			}
			//ҩƷ�÷�
			ArrayList alUsage = this.GetSelectedItems(tvUsage);
			if (alUsage.Count == 0) {
				MessageBox.Show("��ѡ��ҩƷ�÷�");
				return -1;
			}
			//ҩƷ����
			ArrayList alDosageForm = this.GetSelectedItems(tvDosageForm);
			if (alDosageForm.Count == 0) {
				MessageBox.Show("��ѡ��ҩƷ����");
				return -1;
			}
			//ҩƷ����
			ArrayList alDrugQuality = this.GetSelectedItems(tvDrugQuality);
			if (alDrugQuality.Count == 0) {
				MessageBox.Show("��ѡ��ҩƷ����");
				return -1;
			}
			//ҩƷ����
			ArrayList alDrugType = this.GetSelectedItems(tvDrugType);
			if (alDrugType.Count == 0) {
				MessageBox.Show("��ѡ��ҩƷ����");
				return -1;
			}
			#endregion

			int parm;
			neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(neusoft.neuFC.Management.Connection.Instance);
			t.BeginTransaction();
			myDrugStore.SetTrans(t.Trans);

			//���ݲ����ж��Ƿ���Ҫ��ɾ�������ӡ�
			if (IsDelete) {
				//��ɾ���ɰ�ҩ��������ϸ�е��������ݣ�Ȼ������µ����ݡ�
				parm = myDrugStore.DeleteDrugBillList(info.ID);
				if (parm == -1) {
					t.RollBack();
					MessageBox.Show(this.myDrugStore.Err);
					return -1;
				}
			}

			//���������ݣ���ҽ�����ͣ��÷������ͣ�ҩƷ���ʣ�ҩ�����͵�ȫ���зֱ������ϸ��	
			DrugBillList myList = new DrugBillList();
			myList.DrugBillClass.ID = info.ID;
			int pro	= 0; //����������ʾ������
			int max = alOrderType.Count * alUsage.Count * alDosageForm.Count * alDrugQuality.Count * alDrugType.Count;
			foreach(neuObject OrderType in alOrderType) {
				foreach(neuObject Usage in alUsage) {
					foreach(neuObject DosageForm in alDosageForm) {
						foreach(neuObject DrugQuality in alDrugQuality) {
							foreach(neuObject DrugType in alDrugType) {
								//Ϊ��ҩ����ϸʵ����ֵ
								myList.TypeCode       = OrderType.ID;
								myList.UsageCode      = Usage.ID;
								myList.DosageFormCode = DosageForm.ID;
								myList.DrugQuality    = DrugQuality.ID;
								myList.DrugType       = DrugType.ID;

								//�����ҩ��������ϸ��
								parm = this.myDrugStore.InsertDrugBillList(myList);
								if (parm != 1) {
									t.RollBack();
									if (this.myDrugStore.DBErrCode==1)
										MessageBox.Show("�����Ѿ����ڣ������ظ�ά����\n"+
											" ҽ������;"+OrderType.ID+OrderType.Name+
											" �÷�:"+Usage.ID+Usage.Name+
											" ����:"+DosageForm.ID+DosageForm.Name+
											" ҩƷ����:"+DrugQuality.ID+DrugQuality.Name+
											" ҩƷ����:"+DrugType.ID+DrugType.Name);
									else
										MessageBox.Show(this.myDrugStore.Err);
									return -1;
								}
								neusoft.neuFC.Interface.Classes.Function.ShowWaitForm(pro++,max);
								Application.DoEvents();
							}
						}
					}
				}
			}
			//�ύ���ݿ�
			t.Commit();

			return 1;
		}


		/// <summary>
		/// ȡTreeView�б�ѡ�е���Ŀ����
		/// </summary>
		/// <param name="tv">�����б�ucTreeViewChecked</param>
		/// <returns>����</returns>
		private ArrayList GetSelectedItems(ucTreeViewChecked tv){
			ArrayList al = new ArrayList();
			foreach( TreeNode tn in tv.Nodes[0].Nodes) {
				if (tn.Checked) al.Add((neuObject)tn.Tag);
			}
			return al;
		}


		/// <summary>
		/// ȡ������ϸ�еĸ������ݣ�����ʾ��TreeView��
		/// </summary>
		/// <param name="drugBillClassCode">��ҩ���������</param>
		public void ShowList(string drugBillClassCode){

			ArrayList al;
			//ҽ�����
			this.tvOrderType.Nodes[0].Checked = false;
			al = this.myDrugStore.GetDrugBillList(drugBillClassCode,"TYPE_CODE");
			foreach( DrugBillList info in al) {
				foreach(TreeNode tn in this.tvOrderType.Nodes[0].Nodes) {
					neuObject obj = (neuObject)tn.Tag;
					if (info.ID == obj.ID) tn.Checked = true;
				}
			}
			//ҩƷ�÷�
			this.tvUsage.Nodes[0].Checked = false;
			al = this.myDrugStore.GetDrugBillList(drugBillClassCode,"USAGE_CODE");
			foreach( DrugBillList info in al) {
				foreach(TreeNode tn in this.tvUsage.Nodes[0].Nodes) {
					neuObject obj = (neuObject)tn.Tag;
					if (info.ID == obj.ID) tn.Checked = true;
				}
			}
			//ҩƷ����
			this.tvDosageForm.Nodes[0].Checked = false;
			al = this.myDrugStore.GetDrugBillList(drugBillClassCode,"DOSE_MODEL_CODE");
			foreach( DrugBillList info in al) {
				foreach(TreeNode tn in this.tvDosageForm.Nodes[0].Nodes) {
					neuObject obj = (neuObject)tn.Tag;
					if (info.ID == obj.ID) tn.Checked = true;
				}
			}
			//ҩƷ����
			this.tvDrugQuality.Nodes[0].Checked = false;
			al = this.myDrugStore.GetDrugBillList(drugBillClassCode,"DRUG_QUALITY");
			foreach( DrugBillList info in al) {
				foreach(TreeNode tn in this.tvDrugQuality.Nodes[0].Nodes) {
					neuObject obj = (neuObject)tn.Tag;
					if (info.ID == obj.ID) tn.Checked = true;
				}
			}
			//ҩƷ���
			this.tvDrugType.Nodes[0].Checked = false;
			al = this.myDrugStore.GetDrugBillList(drugBillClassCode,"DRUG_TYPE");
			foreach( DrugBillList info in al) {
				foreach(TreeNode tn in this.tvDrugType.Nodes[0].Nodes) {
					neuObject obj = (neuObject)tn.Tag;
					if (info.ID == obj.ID) tn.Checked = true;
				}
			}
		}


		/// <summary>
		/// ��TreeView�в����½ڵ�
		/// </summary>
		/// <param name="parent">�����ڵ�</param>
		/// <param name="item">����Ľڵ�</param>
		/// <returns></returns>
		private int AddTreeViewItem( TreeNode parent, neuObject item) {
			//���ò���Ľڵ���Ϣ
			TreeNode tn = new TreeNode();
			tn.Text = item.Name;
			tn.ImageIndex = -1;
			tn.SelectedImageIndex = -1;
			tn.Tag = item; //Tag���Ա���item
			//���ز���Ľڵ�
			return parent.Nodes.Add(tn);
		}


		/// <summary>
		/// ���TreeView�����нڵ��checked
		/// </summary>
		public void ClearTreeViewChecked( ) {
			//���ҽ������
			tvOrderType.Nodes[0].Checked = false;
			
			//���ҩƷ�÷�
			tvUsage.Nodes[0].Checked = false;
			
			//���ҩƷ����
			tvDosageForm.Nodes[0].Checked = false;
			
			//���ҩƷ����
			tvDrugQuality.Nodes[0].Checked = false;
			
			//���ҩƷ����
			tvDrugType.Nodes[0].Checked = false;
		}


		/// <summary>
		/// �������е�������ʾ��ָ���ڵ���
		/// </summary>
		/// <param name="tn"></param>
		/// <param name="al"></param>
		private void FillTreeViewList(TreeNode tn, ArrayList al) {
			foreach ( neuObject obj in al)  {
				//�������е�ʵ�����뵽TreeView�б���
				this.AddTreeViewItem(tn, obj);
			}
		}


		/// <summary>
		/// ��ʾ���пյ������б�
		/// </summary>
		public void FillTreeView() {
			TreeNode tn;
			ArrayList al = new ArrayList();

			//���ҽ����������б�
			tn = new TreeNode("ҽ�����");
			//ȡҽ����������
			neusoft.HISFC.Management.Manager.OrderType orderType = new neusoft.HISFC.Management.Manager.OrderType();
		    al = orderType.GetList();
			this.FillTreeViewList(tn, al);
			this.tvOrderType.Nodes.Add(tn);
			this.tvOrderType.ExpandAll(); 

			//���ҩƷ�÷������б�
			tn = new TreeNode("ҩƷ�÷�");
			//ȡҩƷ�÷�����
			al = constant.GetList(neusoft.HISFC.Object.Base.enuConstant.USAGE);
			this.FillTreeViewList(tn, al);
			this.tvUsage.Nodes.Add(tn);
			this.tvUsage.ExpandAll();

			//���ҩƷ���������б�
			tn = new TreeNode("ҩƷ����");
			//ȡҩƷ��������
			al = constant.GetList(neusoft.HISFC.Object.Base.enuConstant.DOSAGEFORM);
			this.FillTreeViewList(tn, al);
			this.tvDosageForm.Nodes.Add(tn);
			this.tvDosageForm.ExpandAll();

			//���ҩƷ���������б�
			tn = new TreeNode("ҩƷ����");
			//ȡҩƷ��������
			al = constant.GetList("DRUGQUALITY");
			this.FillTreeViewList(tn, al);
			this.tvDrugQuality.Nodes.Add(tn);
			this.tvDrugQuality.ExpandAll();

			//���ҩƷ��������б�
			tn = new TreeNode("ҩƷ���");
			//ȡҩƷ�������
			al = constant.GetList(neusoft.HISFC.Object.Base.enuConstant.ITEMTYPE);
			this.FillTreeViewList(tn, al);
			this.tvDrugType.Nodes.Add(tn);
			this.tvDrugType.ExpandAll();
		}
    }
}
