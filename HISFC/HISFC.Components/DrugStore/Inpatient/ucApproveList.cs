using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    public partial class ucApproveList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucApproveList()
        {
            InitializeComponent();
        }

        public delegate void SelectBillHandler(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass);

        public event SelectBillHandler SelectBillEvent;

        public event System.EventHandler RootNodeCheckedEvent;

        #region �����

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = null;

        /// <summary>
        /// �Ƿ���ʱ����ҩ�� {B58FA980-2D4F-4d43-BEC2-DC693203F39F} �����ݺŲ��ҩ��
        /// </summary>
        private bool isFindByOperTime = false;

        #endregion

        #region ����

        /// <summary>
        /// ��ʱ����ҩ�� true,�õ��Ų� false  {B58FA980-2D4F-4d43-BEC2-DC693203F39F} �����ݺŲ��ҩ��
        /// </summary>
        public bool IsFindByOperTime
        {
            get
            {
                return this.isFindByOperTime;
            }
            set
            {
                this.isFindByOperTime = value;
            }
        }
        /// <summary>
        /// �Ƿ���ʾ���ҿ�
        /// </summary>
        public bool ShowOperTimeFilter
        {
            get
            {
                return this.panelFind.Visible;
            }
            set
            {
                this.panelFind.Visible = value;
            }
        }

        /// <summary>
        /// ��ҩʱ��
        /// </summary>
        protected DateTime DrugedDate
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtpDrugedDate.Text).Date;
            }
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperDept
        {
            get
            {
                return this.operDept;
            }
            set
            {
                this.operDept = value;
            }
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ˢ�µ�ǰ��ҩ���б�
        /// </summary>
        public void RefreshBill()
        {
            if (this.operDept == null)
            {
                this.ShowList(((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID, this.DrugedDate);
            }
            else
            {
                this.ShowList(this.operDept.ID, this.DrugedDate);
            }
        }

        /// <summary>
        /// �ڵ�չ��
        /// </summary>
        /// <param name="isDrugTree">�Ƿ���δ��׼��</param>
        /// <param name="isExpandAll">�Ƿ�ȫ��չ��</param>
        public virtual void ExpandNode(bool isDrugTree, bool isExpandAll)
        {
            TreeView tempTree;
            if (isDrugTree)
                tempTree = this.tvDrugBill;
            else
                tempTree = this.tvApproveBill;

            if (isExpandAll)
            {
                tempTree.ExpandAll();
            }
            else
            {
                if (tempTree.Nodes.Count > 0)
                    tempTree.Nodes[0].Expand();
            }
        }

        /// <summary>
        /// ȡ��ҩ���б��У���ѡ�еİ�ҩ����ϡ��������Ƿ���Զ԰�ҩ�����к�׼
        /// </summary>
        /// <param name="billCodes">ѡ�еİ�ҩ��������� �ɶ��ż��</param>
        /// <returns>�����Ƿ�������к�׼���� True ���� Flase ������</returns>
        public virtual bool GetCheckBill(ref string billCodes)
        {
            if( this.neuTabControl1.SelectedTab == this.tpDrugBill )
            {
                return this.GetCheckBill( this.tvDrugBill , ref billCodes );
            }
            else
            {
                return this.GetCheckBill( this.tvApproveBill , ref billCodes );
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public new void Focus()
        {
            this.neuTabControl1.SelectedTab = this.tpDrugBill;
            if (this.tvDrugBill.Nodes.Count > 0)
                this.tvDrugBill.SelectedNode = this.tvDrugBill.Nodes[0];
            this.tvDrugBill.Select();
            this.tvDrugBill.Focus();
        }

        /// <summary>
        /// ���ý��㵽DateTimePicker
        /// </summary>
        public void DateFocus()
        {
            this.dtpDrugedDate.Select();
            this.dtpDrugedDate.Focus();
        }

        /// <summary>
        /// ���ý��㵽��ҩ���ڲ��ҿ�
        /// </summary>
        public void FindTextFocus()
        {
            this.txtDrugBillCode.SelectAll();
            this.txtDrugBillCode.Focus();
        }

        #endregion

        /// <summary>
        /// ��ʾ�����ң�ĳһ��İ�ҩ���б�
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="dateTime">��ҩʱ��</param>
        protected virtual void ShowList(string deptCode, DateTime dateTime)
        {
            //ȡ��ҩ����ϢDrugBillClass
            ArrayList al = this.itemManager.QueryDrugBillByDay(deptCode, dateTime);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ" + dateTime.ToString() + "�İ�ҩ���б���Ϣ��������!") + this.itemManager.Err);
                return;
            }

            //��ʾ�Ѻ�׼�İ�ҩ��
            this.AddListToTree(this.tvApproveBill, al, "2");

            //��ʾδ��׼�İ�ҩ��
            this.AddListToTree(this.tvDrugBill, al, "1");

            if (this.neuTabControl1.SelectedTab == this.tpDrugBill)
            {
                if (this.tvDrugBill.Nodes.Count > 0)
                    this.tvDrugBill.SelectedNode = this.tvDrugBill.Nodes[0];
            }
            if (this.neuTabControl1.SelectedTab == this.tpApproveBill)
            {
                if (this.tvApproveBill.Nodes.Count > 0)
                    this.tvApproveBill.SelectedNode = this.tvApproveBill.Nodes[0];
            }
        }

        /// <summary>
        /// ��ʾ��ҩ���б�
        /// </summary>
        /// <param name="tvBill">�����б�</param>
        /// <param name="alBill">��ҩ������</param>
        /// <param name="applyState">��ҩ����״̬</param>
        protected virtual void AddListToTree(FS.FrameWork.WinForms.Controls.NeuTreeView tvBill, ArrayList alBill, string applyState)
        {
            //��������б��еĽڵ�
            tvBill.Nodes.Clear();

            //������������ǰ��հ�ҩ�����͡����ҡ�����ʱ�䣨���������
            string privBillClassCode = "";
            TreeNode nodeBillClass = new TreeNode();
            foreach (DrugBillClass info in alBill)
            {
                if( info.ApplyState == null )
                {
                    info.ApplyState = "2";
                }
                if( info.ApplyState != applyState )
                    continue;

                if (info.ID != privBillClassCode)
                {
                    nodeBillClass = this.GetBillClassNode(info, tvBill.Nodes);

                    privBillClassCode = info.ID; //��������һ����ӵİ�ҩ���ڵ�
                }

                this.GetDeptBillNode(info, nodeBillClass.Nodes);
            }

            this.ExpandNode(true, false);
        }

        /// <summary>
        /// ����ҩ����Ϣ�����б� �����γɵİ�ҩ���ڵ�
        /// </summary>
        /// <param name="drugBillClass">��ҩ����Ϣ</param>
        /// <param name="nodeCollection">���ڵ㼯��</param>
        /// <returns>���ذ�ҩ���ڵ�</returns>
        private TreeNode GetBillClassNode(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass,TreeNodeCollection nodeCollection)
        {
            TreeNode nodeBillClass = new TreeNode(drugBillClass.Name);
            nodeBillClass.Tag = drugBillClass;

            nodeCollection.Add(nodeBillClass);

            return nodeBillClass;
        }

        /// <summary>
        /// ����ҩ����Ϣ�����б� �����γɿ��ҵİ�ҩ���ڵ�
        /// </summary>
        /// <param name="drugBillClass">��ҩ����Ϣ</param>
        /// <param name="nodeCollection">���ڵ㼯��(��ҩ���ڵ�)</param>
        /// <returns>���ؿ��Ұ�ҩ���ڵ�</returns>
        private TreeNode GetDeptBillNode(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, TreeNodeCollection nodeCollection)
        {
            //��Ӱ�ҩ���б�
            TreeNode nodeDeptBill = new TreeNode();
            nodeDeptBill.Text = drugBillClass.ApplyDept.Name + "[" + drugBillClass.Oper.OperTime.ToString("Hmmss") + "]";
            if (drugBillClass.ApplyState == "1")
            {
                nodeDeptBill.ImageIndex = 2;
                nodeDeptBill.SelectedImageIndex = 2;
            }
            else
            {
                nodeDeptBill.ImageIndex = 3;
                nodeDeptBill.SelectedImageIndex = 3;
            }
            nodeDeptBill.Tag = drugBillClass;
            nodeCollection.Add(nodeDeptBill);

            return nodeDeptBill;
        }

        /// <summary>
        /// ȡ��ҩ���б��У���ѡ�еİ�ҩ����ϡ��������Ƿ���Զ԰�ҩ�����к�׼
        /// </summary>
        /// <param name="tvBill">���ж��Ƿ�ѡ�еĽڵ���</param>
        /// <param name="billCodes">ѡ�еİ�ҩ��������� �ɶ��ż��</param>
        /// <returns>�����Ƿ�������к�׼���� True ���� Flase ������</returns>
        protected bool GetCheckBill(FS.FrameWork.WinForms.Controls.NeuTreeView tvBill, ref string billCodes)
        {
            billCodes = "''";
            bool enabledApprove = false;        //�Ƿ�����Ա����ҩ�����к�׼����

            if (tvBill.Nodes.Count <= 0)
                return false;

            foreach (TreeNode billClassNode in tvBill.Nodes)
            {
                foreach (TreeNode deptBillNode in billClassNode.Nodes)
                {
                    if (!deptBillNode.Checked)
                        continue;

                    DrugBillClass drugBillClass = deptBillNode.Tag as DrugBillClass;

                    billCodes = billCodes + string.Format(",'{0}'",drugBillClass.DrugBillNO);

                    if (drugBillClass.ApplyState == "1")
                        enabledApprove = true;
                }
            }

            if (billCodes == "" || billCodes == "''")
            {
                MessageBox.Show(Language.Msg("��ѡ�����׼��ҩ��"));
                billCodes = null;
            }
            //Ϊ�����Ӵ˴����룿
            if (billCodes == "''" && tvBill.SelectedNode.Level == 1)
            {
                FS.HISFC.Models.Pharmacy.DrugBillClass selectBillClass = tvBill.SelectedNode.Tag as FS.HISFC.Models.Pharmacy.DrugBillClass;

                if (selectBillClass.DrugBillNO.IndexOf("'") == -1)
                    billCodes = string.Format("'{0}'", selectBillClass.DrugBillNO);
                else
                    billCodes = selectBillClass.DrugBillNO;

                if (selectBillClass.ApplyState == "1")
                    enabledApprove = true;
            }
           
            return enabledApprove;
        }

        /// <summary>
        /// ����ָ��ʱ����ӡ�İ�ҩ���Ľڵ�
        /// </summary>
        /// <param name="strOperTime">ָ���İ�ҩʱ���</param>
        private void FindNode(string strOperTime)
        {
            TreeView tvBill;
            if (this.neuTabControl1.SelectedTab == this.tpDrugBill)
                tvBill = this.tvDrugBill;
            else
                tvBill = this.tvApproveBill;

            foreach (TreeNode billClassNode in tvBill.Nodes)
            {
                foreach (TreeNode deptBillNode in billClassNode.Nodes)
                {
                    DrugBillClass drugBillClass = deptBillNode.Tag as DrugBillClass;

                    if (drugBillClass.Oper.OperTime.ToString("Hmmss") == strOperTime)
                    {
                        tvBill.SelectedNode = deptBillNode;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// ����ָ����ҩ���ŵĽڵ� {B58FA980-2D4F-4d43-BEC2-DC693203F39F} �����ݺŲ��ҩ��
        /// </summary>
        /// <param name="strOperTime">ָ���İ�ҩ����</param>
        private void FindNodeByBillNO(string strBillNO)
        {
            TreeView tvBill;
            if (this.neuTabControl1.SelectedTab == this.tpDrugBill)
                tvBill = this.tvDrugBill;
            else
                tvBill = this.tvApproveBill;

            foreach (TreeNode billClassNode in tvBill.Nodes)
            {
                foreach (TreeNode deptBillNode in billClassNode.Nodes)
                {
                    DrugBillClass drugBillClass = deptBillNode.Tag as DrugBillClass;

                    if (drugBillClass.DrugBillNO== strBillNO)
                    {
                        tvBill.SelectedNode = deptBillNode;
                        return;
                    }
                }
            }
        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                DateTime dtpDate = this.itemManager.GetDateTimeFromSysDateTime().Date;

                this.dtpDrugedDate.Value = dtpDate;
            }
            catch
            { }
        }

        private void tvBill_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)          //��������Ϊ��ҩ������ڵ� �� �ڵ�δѡ�� ��������ӽڵ�ȡ��ѡ��
            {
                if (!e.Node.Checked)
                {
                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        if (node.Checked)
                            node.Checked = false;
                    }
                }
            }

            if (this.SelectBillEvent != null)
                this.SelectBillEvent(e.Node.Tag as DrugBillClass);
        }

        private void tvBill_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)                      //ѡ�е�Ϊ���ڵ� �����ӽڵ�����ڵ�ѡ��״̬��ͬ
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
                if (this.RootNodeCheckedEvent != null)
                {
                    this.RootNodeCheckedEvent(e.Node.Checked, e);
                }
            }
            else                                          //��Ա������нڵ�ȡ��ѡ�� ��Ը��ڵ�ȡ��ѡ��
            {
                bool hasCheck = false;
                foreach (TreeNode tempNode in e.Node.Parent.Nodes)
                {
                    if (tempNode.Checked)
                    {
                        hasCheck = true;
                        break;
                    }
                }

                if (!hasCheck && e.Node.Parent.Checked)
                {
                    e.Node.Parent.Checked = false;
                }
                //if (e.Node.Checked)
                //{
                //    (sender as FS.FrameWork.WinForms.Controls.NeuTreeView).SelectedNode = e.Node;                    
                //}
            }
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpDrugBill)
            {
                if (this.tvDrugBill.Nodes.Count > 0)
                    this.tvDrugBill.SelectedNode = this.tvDrugBill.Nodes[0];
            }
            else
            {
                if (this.tvApproveBill.Nodes.Count > 0)
                    this.tvApproveBill.SelectedNode = this.tvApproveBill.Nodes[0];
            }
        }

        private void dtpDrugedDate_ValueChanged(object sender, EventArgs e)
        {
            this.RefreshBill();
        }

        private void txtDrugBillCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (isFindByOperTime)
                {
                    this.FindNode(this.txtDrugBillCode.Text);
                }
                //{B58FA980-2D4F-4d43-BEC2-DC693203F39F} �����ݺŲ��ҩ��
                else
                {
                    this.FindNodeByBillNO(this.txtDrugBillCode.Text);
                }
            }
            //{B58FA980-2D4F-4d43-BEC2-DC693203F39F} �����ݺŲ��ҩ��
            else if(e.KeyCode == Keys.F2)
            {
                if (!isFindByOperTime)
                {
                    this.neuLabel2.Text = "ʱ��";
                    isFindByOperTime = true;
                }
                else
                {
                    this.neuLabel2.Text = "���ݺ�";
                    this.isFindByOperTime = false;
                }
            }
        }
    }
}
