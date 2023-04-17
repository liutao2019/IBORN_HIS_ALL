using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.DrugStore.InBase
{
    /// <summary>
    /// [�ؼ�����:ucDrugBill]<br></br>
    /// [��������: ҩƷ�������ؼ�������ҩƷ������ʡ�ҩ�����á��÷������͡��Լ�ҽ�����͵ȣ�ͨ����������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-7]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='������' 
    ///		�޸�ʱ��='2007-04' 
    ///		�޸�Ŀ��=' ������ʾ�÷�����͵Ĵ��� �� ������������'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class tvDrugType : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvDrugType()
        {
            InitializeComponent();
            //��ʼ��
            try
            {
                if (!this.DesignMode)
                {
                    this.InitTreeView();
                }
            }
            catch
            {
            }
        }

        public tvDrugType(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            //��ʼ��
            try
            {
                if (!this.DesignMode)
                {
                    this.InitTreeView();
                }
            }
            catch
            {
            }
        }

        #region ����

        /// <summary>
        /// �Ƿ���ʾҩƷ���ʡ�ҩƷ���͡�ҩ�����á��÷�
        /// </summary>
        private ShowDrugKind showDrugKind = new ShowDrugKind();

        /// <summary>
        /// �Ƿ�Ƕ����ʾ
        /// </summary>
        private bool isShowNested = true;

        /// <summary>
        /// �Ƿ��Զ�չ���ڵ�
        /// </summary>
        private bool isExpandAll = false;

        /// <summary>
        /// �Ƿ���ʾ�������÷������������ʾ��ϸ
        /// </summary>
        private bool isShowUsageDosageClass = false;

        #endregion

        #region  ����

        /// <summary>
        /// �Ƿ���ʾҩƷ���ʡ�ҩƷ���͡�ҩ�����á��÷�
        /// </summary>
        [Description("�Ƿ���ʾҩƷ���ʡ�ҩƷ���͡�ҩ�����á��÷�"), Category("����"), DefaultValue(0)]
        public ShowDrugKind ShowKind
        {
            get
            {
                return this.showDrugKind;
            }
            set
            {
                if (value != this.showDrugKind)
                {
                    if (value == ShowDrugKind.ShowAll)
                    {
                        this.isShowNested = false;
                    }

                    this.showDrugKind = value;

                    this.InitTreeView();
                }
            }
        }

        /// <summary>
        /// �Ƿ�Ƕ����ʾ
        /// </summary>
        [Description("�Ƿ�Ƕ����ʾ"), Category("����"), DefaultValue(true)]
        public bool ShowNested
        {
            get
            {
                return this.isShowNested;
            }
            set
            {
                if (value != this.isShowNested)
                {
                    if (this.showDrugKind == ShowDrugKind.ShowAll)
                    {
                        this.isShowNested = false;
                    }
                    else
                    {
                        this.isShowNested = value;
                    }

                    this.InitTreeView();
                }
            }
        }

        /// <summary>
        /// �Ƿ��Զ�չ���ڵ�
        /// </summary>
        [Description("�Ƿ��Զ�չ���ڵ�"), Category("����"), DefaultValue(false)]
        public bool ExpandAllNodes
        {
            get
            {
                return this.isExpandAll;
            }
            set
            {
                this.isExpandAll = value;

                this.ExpandAll();
            }
        }

        /// <summary>
        /// ��ȡѡ�еĽڵ��б�
        /// </summary>
        [Description("ѡ�еĽڵ���Ϣ�б�(����ҩƷ���Ե�ѡʱ����)"), Category("����")]
        public List<FS.HISFC.Models.Base.Const> SelectedNodes
        {
            get
            {
                List<FS.HISFC.Models.Base.Const> selectednodes = new List<FS.HISFC.Models.Base.Const>();

                foreach (TreeNode node in this.Nodes)
                {
                    if (this.showDrugKind == ShowDrugKind.ShowModeOnly || this.showDrugKind == ShowDrugKind.ShowQualityOnly || this.showDrugKind == ShowDrugKind.ShowTypeOnly || this.showDrugKind == ShowDrugKind.ShowUsageOnly)
                    {
                        foreach (TreeNode childNode in node.Nodes)
                        {
                            if (childNode.Checked)
                            {
                                selectednodes.Add(childNode.Tag as FS.HISFC.Models.Base.Const);
                            }
                        }
                    }
                    else
                    {
                        selectednodes = null;
                    }
                }

                return selectednodes;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ�������б�,����ǰ�����úÿؼ����������
        /// </summary>
        public void InitTreeView()
        {
            if (this.DesignMode)
            {
                return;
            }
            try
            {
                this.ImageList = this.groupImageList;

                FS.HISFC.BizProcess.Integrate.Manager drugconstant = new FS.HISFC.BizProcess.Integrate.Manager();
                this.SuspendLayout();
                this.Nodes.Clear();

                switch (showDrugKind)
                {
                    case ShowDrugKind.ShowTypeOnly:
                        this.AddFirstNode("ҩƷ���");
                        this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), 0);
                        break;
                    case ShowDrugKind.ShowFunctionOnly:
                        //��ʾ����ҩ������
                        this.InitPharmacyFunction();
                        break;
                    case ShowDrugKind.ShowQualityOnly:
                        this.AddFirstNode("ҩƷ����");
                        this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY), 0);
                        break;
                    case ShowDrugKind.ShowUsageOnly:
                        this.AddFirstNode("ҩƷ�÷�");
                        this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE), 0);
                        break;
                    case ShowDrugKind.ShowModeOnly:
                        this.AddFirstNode("ҩƷ����");
                        this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM), 0);
                        break;
                    case ShowDrugKind.ShowAdviceKindOnly:
                        this.AddFirstNode("ҽ�����");
                        this.AddSecondNode(drugconstant.QueryOrderTypeList(), 0);
                        break;
                    case ShowDrugKind.ShowTypeAndQuality:
                        if (this.isShowNested)
                        {
                            this.AddFirstNode("ҩƷ��Ϣ");
                            this.AddThirdNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY));
                        }
                        else
                        {
                            this.AddFirstNode("ҩƷ���");
                            this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), 0);
                            this.AddFirstNode("ҩƷ����");
                            this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY), 1);
                        }
                        break;
                    case ShowDrugKind.ShowTypeAndUsage:
                        if (this.isShowNested)
                        {
                            this.AddFirstNode("ҩƷ��Ϣ");
                            this.AddThirdNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE));
                        }
                        else
                        {
                            this.AddFirstNode("ҩƷ���");
                            this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), 0);
                            this.AddFirstNode("ҩƷ�÷�");
                            this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE), 1);
                        }
                        break;
                    case ShowDrugKind.ShowTypeAndMode:
                        if (this.isShowNested)
                        {
                            this.AddFirstNode("ҩƷ��Ϣ");
                            this.AddThirdNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM));
                        }
                        else
                        {
                            this.AddFirstNode("ҩƷ���");
                            this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), 0);
                            this.AddFirstNode("ҩƷ����");
                            this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM), 1);
                        }
                        break;
                    case ShowDrugKind.ShowTypeAndFunction:
                        if (this.isShowNested)
                        {
                            this.AddFirstNode("ҩƷ��Ϣ");
                            this.AddThirdNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PHYFUNCTION));
                        }
                        else
                        {
                            this.AddFirstNode("ҩƷ���");
                            this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), 0);
                            this.AddFirstNode("ҩ������");
                            this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PHYFUNCTION), 1);
                        }
                        break;
                    case ShowDrugKind.ShowAll:
                        this.AddFirstNode("ҩƷ���");
                        this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE), 0);
                        this.AddFirstNode("ҩƷ����");
                        this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY), 1);
                        this.AddFirstNode("ҩƷ����");
                        this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM), 2);
                        this.AddFirstNode("ҩƷ�÷�");
                        this.AddSecondNode(drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE), 3);
                        //��ʾ����ҩ������
                        this.InitPharmacyFunction();
                        break;
                    case ShowDrugKind.ShowDosageClass:          //��ʾ���ʹ���
                        this.isShowUsageDosageClass = true;
                        this.AddDosage();
                        break;
                    case ShowDrugKind.ShowUsageClass:           //��ʾ�÷�����
                        this.isShowUsageDosageClass = true;
                        this.AddUsage();
                        break;
                }

            }
            catch
            {

            }
            finally
            {
                if (this.isExpandAll)
                {
                    this.ExpandAll();
                }
                this.ResumeLayout();
            }
            return;
        }

        /// <summary>
        /// ����ҩƷ�÷�
        /// </summary>
        protected void AddUsage()
        {
            this.AddFirstNode("ҩƷ�÷�");

            FS.HISFC.BizProcess.Integrate.Manager drugconstant = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList alUsage = new ArrayList();

            if (this.isShowUsageDosageClass)        //��ʾ�÷�/���ʹ���
            {
                alUsage = drugconstant.GetConstantList("USAGECLASS");
            }
            else
            {
                alUsage = drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            }

            this.AddSecondNode(alUsage, this.Nodes.Count - 1);
        }

        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        protected void AddDosage()
        {
            this.AddFirstNode("ҩƷ����");

            FS.HISFC.BizProcess.Integrate.Manager drugconstant = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList alDosage = new ArrayList();

            if (this.isShowUsageDosageClass)        //��ʾ�÷�/���ʹ���
            {
                alDosage = drugconstant.GetConstantList("DOSAGECLASS");
            }
            else
            {
                alDosage = drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);
            }

            this.AddSecondNode(alDosage, this.Nodes.Count - 1);
        }

        #region ���ڵ����

        /// <summary>
        /// ��Ӹ��ڵ�
        /// </summary>
        private void AddFirstNode(string text)
        {
            TreeNode currentnode = new TreeNode();
            currentnode.Text = text;
            currentnode.Tag = "ALL";
            currentnode.ImageIndex = 0;
            currentnode.SelectedImageIndex = 0;
            this.Nodes.Add(currentnode);
        }
        /// <summary>
        /// ��Ӷ����ڵ�
        /// </summary>
        /// <param name="al"></param>
        private void AddSecondNode(ArrayList al, int index)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                TreeNode currentnode = new TreeNode();
                currentnode.Text = obj.Name;
                currentnode.Tag = obj;
                currentnode.ImageIndex = 2;
                currentnode.SelectedImageIndex = 4;
                this.Nodes[index].Nodes.Add(currentnode);
            }
        }
        /// <summary>
        /// Ƕ����Ӷ������ڵ�
        /// </summary>
        /// <param name="parent">ҩƷ���</param>
        /// <param name="child">�������</param>
        private void AddThirdNode(ArrayList parent, ArrayList child)
        {
            int i = 0;
            foreach (FS.HISFC.Models.Base.Const drugtype in parent)
            {

                TreeNode parentnode = new TreeNode();
                parentnode.Text = drugtype.Name;
                parentnode.Tag = drugtype;
                parentnode.ImageIndex = 3;
                parentnode.SelectedImageIndex = 3;
                this.Nodes[0].Nodes.Add(parentnode);

                foreach (FS.HISFC.Models.Base.Const other in child)
                {
                    TreeNode childnode = new TreeNode();
                    childnode.Text = other.Name;
                    childnode.Tag = other;
                    childnode.ImageIndex = 3;
                    childnode.SelectedImageIndex = 3;
                    this.Nodes[0].Nodes[i].Nodes.Add(childnode);
                }

                i++;
            }

        }
        #endregion

        #region ����ҩ������
        /// <summary>
        /// ����ҩ������DataView�����ڸ��ݸ��ڵ�����ӽڵ�
        /// </summary>
        /// <returns></returns>
        private DataView CreateDataView()
        {
            FS.HISFC.BizLogic.Pharmacy.Constant pharmacyConstant = new FS.HISFC.BizLogic.Pharmacy.Constant();
            DataTable myDataTable = new DataTable("tbFunction");
            DataRow myDataRow;
            myDataTable.Columns.Add("NODE_CODE", typeof(String));
            myDataTable.Columns.Add("PARENT_NODE", typeof(String));
            myDataTable.Columns.Add("NODE_NAME", typeof(String));
            myDataTable.Columns.Add("NODE_KIND", typeof(int));
            foreach (FS.HISFC.Models.Pharmacy.PhaFunction phafun in pharmacyConstant.QueryPhaFunction())
            {
                myDataRow = myDataTable.NewRow();
                myDataRow["PARENT_NODE"] = phafun.ParentNode;
                myDataRow["NODE_CODE"] = phafun.ID;
                myDataRow["NODE_NAME"] = phafun.Name;
                myDataRow["NODE_KIND"] = phafun.NodeKind;
                myDataTable.Rows.Add(myDataRow);
            }
            DataView custDV = new DataView(myDataTable);
            return custDV;

        }
        /// <summary>
        /// �������ҩ������
        /// </summary>
        /// <param name="arDataView"></param>
        /// <param name="currNodes"></param>
        /// <param name="ParentNodeID">�Ƿ���ʾ���ڵ㣬�����ʾ����0������R</param>
        private void InitTree(DataView arDataView, TreeNodeCollection currNodes, string ParentNodeID)
        {
            string FilterString, NodeName, NodeCode;
            FilterString = "PARENT_NODE = " + "'" + ParentNodeID + "'";
            arDataView.RowFilter = FilterString;
            arDataView.Sort = "NODE_KIND DESC";
            foreach (DataRowView drvRow in arDataView)
            {
                NodeCode = drvRow["NODE_CODE"].ToString();
                NodeName = drvRow["NODE_NAME"].ToString().Trim();
                TreeNode NewNode = new TreeNode();
                NewNode.Tag = NodeCode;
                NewNode.Text = NodeName;
                //����Ƿ�������
                arDataView.RowFilter = "PARENT_NODE = " + "'" + NodeCode + "'";
                currNodes.Add(NewNode);
                //�����������ݹ���ã���������
                if (arDataView.Count > 0)
                {
                    NewNode.ImageIndex = 2;
                    NewNode.SelectedImageIndex = 4;
                    InitTree(arDataView, NewNode.Nodes, NodeCode);
                }
                else
                {
                    NewNode.ImageIndex = 0;
                    NewNode.SelectedImageIndex = 0;
                }

                //��ǰ�ڵ㲻�����ӽڵ���ԭΪ�����Ĺ�������
                arDataView.RowFilter = FilterString;
            }
        }

        /// <summary>
        /// ��ʾ����ҩ������
        /// </summary>
        private void InitPharmacyFunction()
        {
            DataView dv = this.CreateDataView();
            this.InitTree(dv, this.Nodes, "0");
        }
        #endregion

        #endregion

        #region �¼�

        /// <summary>
        /// ������ǰ�ڵ�������ӽڵ�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (this.CheckBoxes)
                {
                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        if (node.Checked != e.Node.Checked)
                        {
                            node.Checked = e.Node.Checked;
                        }
                    }
                }
            }
            base.OnAfterCheck(e);
        }

        #endregion

        /// <summary>
        /// �Ƿ���ʾҩƷ���ʺ�ҩ������
        /// </summary>
        public enum ShowDrugKind
        {
            /// <summary>
            /// ֻ��ʾҩƷ���
            /// </summary>
            ShowTypeOnly = 0,
            /// <summary>
            /// ֻ��ʾҩƷ����
            /// </summary>
            ShowQualityOnly = 1,
            /// <summary>
            /// ֻ��ʾҩƷ����
            /// </summary>
            ShowModeOnly = 2,
            /// <summary>
            /// ֻ��ʾҩ�����ã�������ʾ��
            /// </summary>
            ShowFunctionOnly = 3,
            /// <summary>
            /// ֻ��ʾ�÷�
            /// </summary>
            ShowUsageOnly = 4,
            /// <summary>
            /// ��ʾҩƷ��������
            /// </summary>
            ShowTypeAndQuality = 5,
            /// <summary>
            /// ��ʾҩƷ����ҩ������
            /// </summary>
            ShowTypeAndFunction = 6,
            /// <summary>
            /// ��ʾҩƷ�����÷�
            /// </summary>
            ShowTypeAndUsage = 7,
            /// <summary>
            /// ��ʾҩƷ���ͼ���
            /// </summary>
            ShowTypeAndMode = 8,
            /// <summary>
            /// ȫ����ʾҩƷ������Ϣ
            /// </summary>
            ShowAll = 9,
            /// <summary>
            /// ��ʾҽ�����
            /// </summary>
            ShowAdviceKindOnly,
            /// <summary>
            /// ��ʾ�÷�����
            /// </summary>
            ShowUsageClass,
            /// <summary>
            /// ��ʾ���ʹ���
            /// </summary>
            ShowDosageClass

        }


    }
}
