using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.DrugStore.Base
{
    /// <summary>
    /// <br></br>
    /// [��������: �����б���ʾ�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// </summary>
    public partial class tvDeptTree : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvDeptTree()
        {
            InitializeComponent();

            try
            {
                this.InitTree();
            }
            catch
            { }
        }

        public tvDeptTree(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            try
            {
                this.InitTree( );
            }
            catch
            { }
        }

        #region �����

        /// <summary>
        /// �Ƿ���ʾ���ڵ�
        /// </summary>
        bool isShowRoot = true;

        /// <summary>
        /// �Ƿ���ʾҩ��
        /// </summary>
        bool isShowP = true;

        /// <summary>
        /// �Ƿ���ʾҩ��
        /// </summary>
        bool isShowPI = true;

        /// <summary>
        /// �Ƿ�Խڵ��Զ�չ��
        /// </summary>
        bool autoExpand = true;

        /// <summary>
        /// ҩ���б�
        /// </summary>
        List<FS.HISFC.Models.Base.Department> pList = null;

        /// <summary>
        /// ҩ���б�
        /// </summary>
        List<FS.HISFC.Models.Base.Department> piList = null;

        /// <summary>
        /// ҩ��������
        /// </summary>
        private string piStatCode = "S001";

        /// <summary>
        /// ҩ���������
        /// </summary>
        private string pStatCode = "S002";

        /// <summary>
        /// �Ƿ�ʹ�ÿ��ҽṹ����ʾ
        /// </summary>
        private bool isUseDeptStruct = false;
        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ���ڵ�(ҩ����ҩ����ڵ�)
        /// </summary>
        [Description("�������б�ʱ �Ƿ���ʾҩ����ҩ����ڵ�"),Category("����"),DefaultValue(true)]
        public bool IsShowRoot
        {
            get
            {
                return this.isShowRoot;
            }
            set
            {
                this.isShowRoot = value;

                this.InitTree();
            }
        }

        /// <summary>
        /// �Ƿ���ʾҩ��
        /// </summary>
        [Description("�����б�ʱ �Ƿ����ҩ���б�"),Category("����"),DefaultValue(true)]
        public bool IsShowP
        {
            get
            {
                return this.isShowP;
            }
            set
            {
                this.isShowP = value;

                this.InitTree();
            }
        }

        /// <summary>
        /// �Ƿ���ʾҩ��
        /// </summary>
        [Description("�����б�ʱ �Ƿ����ҩ���б�"),Category("����"),DefaultValue(true)]
        public bool IsShowPI
        {
            get
            {
                return this.isShowPI;
            }
            set
            {
                this.isShowPI = value;

                this.InitTree();
            }
        }

        /// <summary>
        /// �Ƿ�Խڵ��Զ�չ��
        /// </summary>
        [Description("�б���غ� �Ƿ�Ը����ڵ��Զ�չ��"),Category("����"),DefaultValue(true)]
        public bool AutoExpand
        {
            get
            {
                return this.autoExpand;
            }
            set
            {
                this.autoExpand = value;

                this.InitTree();
            }
        }

        /// <summary>
        /// ҩ��������
        /// </summary>
        [Description("���ҽṹ��ά����ҩ�������룬Ĭ��ΪS001"), Category("����"), DefaultValue("S001")]
        public string PIStatCode
        {
            get
            {
                return this.piStatCode;
            }
            set
            {
                this.piStatCode = value;
            }
        }

        /// <summary>
        /// ҩ���������
        /// </summary>
        [Description("���ҽṹ��ά����ҩ��������룬Ĭ��ΪS002"), Category("����"), DefaultValue("S002")]
        public string PStatCode
        {
            get
            {
                return this.pStatCode;
            }
            set
            {
                this.pStatCode = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ�ÿ��ҽṹ����ʾ
        /// </summary>
        [Description("�Ƿ�ʹ�ÿ��ҽṹ����ʾ������ΪTrueʹ�ÿ��ҽṹ��ʾʱ����ע������PIStatCode��PStatCode����ֵ"), Category("����"), DefaultValue(false)]
        public bool IsUseDeptStruct
        {
            get
            {
                return this.isUseDeptStruct;
            }
            set
            {
                this.isUseDeptStruct = value;
            }
        }
        #endregion

        /// <summary>
        /// ��ȡ��������ѡ�еĽڵ�
        /// </summary>
        public List<FS.HISFC.Models.Base.Department> SelectNodes
        {
            get
            {
                List<FS.HISFC.Models.Base.Department> selectNodes = new List<FS.HISFC.Models.Base.Department>();
                foreach (TreeNode node in this.Nodes)
                {
                    if (this.IsShowRoot)
                    {
                        foreach (TreeNode childNode in node.Nodes)
                        {
                            if (childNode.Checked)
                            {
                                selectNodes.Add(childNode.Tag as FS.HISFC.Models.Base.Department);
                            }
                        }
                    }
                    else
                    {
                        if (node.Checked)
                            selectNodes.Add(node.Tag as FS.HISFC.Models.Base.Department);
                    }
                }

                return selectNodes;
            }
        }

        /// <summary>
        /// �����б����ݼ���
        /// </summary>
        /// <returns>���ݼ��سɹ�����1 ���ش��ڴ��󷵻�-1</returns>
        protected virtual int InitDept()
        {
            FS.HISFC.BizLogic.Manager.Department deptManagment = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList deptList = deptManagment.GetDeptmentAll();
            if (deptList == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ�������ݷ�������" + deptManagment.Err);
                return 1;
            }

            System.Collections.Hashtable hsDeptList = new Hashtable();

            pList = new List<FS.HISFC.Models.Base.Department>();
            piList = new List<FS.HISFC.Models.Base.Department>();

            #region ���տ������ͼ���

            foreach (FS.HISFC.Models.Base.Department dept in deptList)
            {
                hsDeptList.Add(dept.ID, dept);

                if (dept.DeptType.ID.ToString() == FS.HISFC.Models.Base.EnumDepartmentType.P.ToString())
                {
                    pList.Add(dept);
                    continue;
                }
                if (dept.DeptType.ID.ToString() == FS.HISFC.Models.Base.EnumDepartmentType.PI.ToString())
                {
                    piList.Add(dept);
                    continue;
                }
            }

            #endregion

            //ʹ�ÿ��ҽṹ����ʾʱ���������ִ�С����򷵻�
            if (!this.isUseDeptStruct)
            {
                return 0;
            }


            #region ���տ��ҽṹ����

            pList.Clear();
            piList.Clear();

            //�������ҷ���ȼ���������һ���ڵ��б�
            FS.HISFC.BizLogic.Manager.DepartmentStatManager statMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();            
            ArrayList deptStruct = statMgr.LoadLevelViewDepartemt("03");
            if (deptStruct == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ���ҽṹ���ݷ�������" + deptManagment.Err);
                return 1;
            }
            foreach (FS.HISFC.Models.Base.DepartmentStat info in deptStruct)
            {
                if (info.DeptCode == this.piStatCode)       //���ش���ҩ��
                {
                    foreach (FS.HISFC.Models.Base.DepartmentStat piInfo in info.Childs)
                    {
                        if (hsDeptList.ContainsKey(piInfo.DeptCode))
                        {
                            piList.Add(hsDeptList[piInfo.ID] as FS.HISFC.Models.Base.Department);
                        }
                    }
                }
                if (info.DeptCode == this.pStatCode)        //���ش���ҩ��
                {
                    foreach (FS.HISFC.Models.Base.DepartmentStat pInfo in info.Childs)
                    {
                        if (hsDeptList.ContainsKey(pInfo.DeptCode))
                        {
                            pList.Add(hsDeptList[pInfo.ID] as FS.HISFC.Models.Base.Department);
                        }
                    }
                }
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// ���ݿ������� ���������б�
        /// </summary>
        /// <returns>�ɹ����ط���1 ʧ�ܷ���-1</returns>
        protected virtual int InitTree()
        {
            if (this.DesignMode)
                return 1;

            this.ImageList = this.deptImageList;

            if (this.pList == null || this.piList == null)
            {
                if (this.InitDept() == -1)
                    return -1;
            }

            this.SuspendLayout();

            this.Nodes.Clear();

            TreeNode pRootNode = new TreeNode("ҩ��", 0, 0);
            TreeNode piRootNode = new TreeNode("ҩ��", 0, 0);

            if (this.isShowRoot)
            {
                if (this.isShowP)
                    this.Nodes.Add(pRootNode);
                if (this.isShowPI)
                    this.Nodes.Add(piRootNode);
            }
            if (this.isShowP)
            {
                foreach (FS.HISFC.Models.Base.Department dept in pList)
                {
                    TreeNode node = new TreeNode(dept.Name);
                    node.ImageIndex = 4;
                    node.SelectedImageIndex = 5;

                    node.Tag = dept;
                    if (this.isShowRoot)
                        pRootNode.Nodes.Add(node);
                    else
                        this.Nodes.Add(node);
                }
            }
            if (this.IsShowPI)
            {
                foreach (FS.HISFC.Models.Base.Department dept in piList)
                {
                    TreeNode node = new TreeNode(dept.Name);
                    node.ImageIndex = 4;
                    node.SelectedImageIndex = 5;

                    node.Tag = dept;
                    if (this.isShowPI)
                        piRootNode.Nodes.Add(node);
                    else
                        piRootNode.Nodes.Add(node);
                }
            }

            if (this.AutoExpand)
                this.ExpandAll();

            this.ResumeLayout();

            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public virtual int Reset()
        {
            this.InitDept();

            this.InitTree();

            return 1;
        }

        /// <summary>
        /// ѡ�и��ڵ�ʱ �������ӽڵ�ѡ�� 
        /// </summary>
        /// <param name="e">Select�¼���Ϣ</param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            if (this.CheckBoxes)
            {
                if (e.Node.Nodes != null && e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        node.Checked = e.Node.Checked;
                    }
                }
            }
            base.OnAfterSelect(e);
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (this.CheckBoxes)
            {
                if (e.Node.Nodes != null && e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        node.Checked = e.Node.Checked;
                    }
                }
            }
            base.OnAfterCheck(e);
        }


    }
}
