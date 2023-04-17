using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
namespace FS.HISFC.Components.Common.Controls
{
    public delegate void SelectOrderHandler(ArrayList alOrders);

    /// <summary>
    /// ҽ��վ�����б�
    /// </summary>
    public partial class tvDoctorGroup : FS.FrameWork.WinForms.Controls.NeuTreeView
    {
        public tvDoctorGroup()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.ImageList = imageList1;
            }
        }

        public tvDoctorGroup(IContainer container)
        {
            if (!DesignMode)
            {
                container.Add(this);
            }

            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����ѡ���¼�
        /// </summary>
        public event SelectOrderHandler SelectOrder;

        /// <summary>
        /// ��������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper deptHelper = null;

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper personHelper = null;

        /// <summary>
        /// �Ƿ��п��������޸�Ȩ��
        /// </summary>
        private bool isHaveDeptEditPower = false;

        /// <summary>
        /// �Ƿ���ȫԺ�����޸�Ȩ��
        /// </summary>
        private bool isHaveAllEditPower = false;

        /// <summary>
        /// �Ƿ�˫����ʾ������ϸ��0 Ϊ������ʾ
        /// </summary>
        private int isDoubleClickShowSelect = -1;

        /// <summary>
        /// �Ƿ�ģʽ�Ի�����ʾ������ϸ 0����ģʽ�Ի���
        /// </summary>
        private int isShowDetailDialog = -1;

        /// <summary>
        /// �Ƿ���������޸��������
        /// </summary>
        private int isLeftMouseEditGroupName = -1;
        
        /// <summary>
        /// �Ƿ������޸���������
        /// </summary>
        private bool isAllowEditGroupName = false;

        /// <summary>
        /// �޸�ǰ��������
        /// </summary>
        string groupOldName = "";

        /// <summary>
        /// �Ƿ�༭����ģʽ
        /// </summary>
        private bool isEditGroup = false;

        /// <summary>
        /// �Ƿ�༭����ģʽ
        /// </summary>
        public bool IsEditGroup
        {
            get
            {
                return isEditGroup;
            }
            set
            {
                isEditGroup = value;
            }
        }

        #region ����

        /// <summary>
        /// ��ǰ�������ͣ�ҽ��
        /// </summary>
        protected enuType myType = enuType.Order;
        
        /// <summary>
        /// ��ǰ�������ͣ�ҽ��
        /// </summary>
        [DefaultValue(enuType.Order)]
        public enuType Type
        {
            get
            {
                return this.myType;
            }
            set
            {
                this.myType = value;
            }
        }

        /// <summary>
        /// ��ʾ��������
        /// </summary>
        protected enuGroupShowType myShowType = enuGroupShowType.Dept;
        
        /// <summary>
        /// ��ʾ��������
        /// </summary>
        [DefaultValue(enuGroupShowType.Dept)]
        public enuGroupShowType ShowType
        {
            get
            {
                return this.myShowType;
            }
            set
            {
                this.myShowType = value;
            }
        }
        
        /// <summary>
        /// ����������סԺ
        /// </summary>
        protected FS.HISFC.Models.Base.ServiceTypes inpatientType = FS.HISFC.Models.Base.ServiceTypes.I;

        FS.HISFC.BizLogic.Manager.Group groupManager = new FS.HISFC.BizLogic.Manager.Group();
        
        /// <summary>
        /// ����������סԺ
        /// </summary>
        [DefaultValue( FS.HISFC.Models.Base.ServiceTypes.I)]
        public FS.HISFC.Models.Base.ServiceTypes InpatientType
        {
            get
            {
                return inpatientType;
            }
            set
            {
                inpatientType = value;
            }
        }

        /// <summary>
        /// ��ͬ��λ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.PactInfo pact;

        /// <summary>
        /// ��ͬ��λ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.PactInfo Pact
        {
            set
            {
                pact = value;
            }
        }
        
        #endregion

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Init()
        {
            try
            {
                this.personHelper = new FS.FrameWork.Public.ObjectHelper(manager.QueryEmployeeAll());
                this.deptHelper = new FS.FrameWork.Public.ObjectHelper(manager.QueryDeptmentsInHos(false));

                this.RefrshGroup();

                #region �����޸�Ȩ���ж�

                FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();

                string errInfo = "";
                int ret = docAbility.CheckPopedom(docAbility.Operator.ID, "ALL", "groupManager", false, ref errInfo);

                if (ret > 0)
                {
                    this.isHaveAllEditPower = true;
                }
                else if (ret < 0)
                {
                    this.isHaveAllEditPower = false;
                    ShowErr(errInfo);
                    return;
                }
                else
                {
                    this.isHaveAllEditPower = false;
                }

                ret = docAbility.CheckPopedom(docAbility.Operator.ID, "DEPT", "groupManager", false, ref errInfo);

                if (ret > 0)
                {
                    this.isHaveDeptEditPower = true;
                }
                else if (ret < 0)
                {
                    this.isHaveDeptEditPower = false;
                    ShowErr(errInfo);
                    return;
                }
                else
                {
                    this.isHaveDeptEditPower = false;
                }
                #endregion

                if (this.isLeftMouseEditGroupName == -1)
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam contrIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    this.isLeftMouseEditGroupName = contrIntegrate.GetControlParam<int>("HNMZ38", true, 1);
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        #region ��ʼ���б�

        /// <summary>
        /// ˢ������
        /// </summary>
        public void RefrshGroup()
        {
            if (this.myShowType == enuGroupShowType.Dept)
            {
                this.RefreshGroupMe();
            }
            else
            {
                this.RefreshGroupAll();
            }
        }
      
        /// <summary>
        /// ��ӵ����������ף�ȫԺ�����ˡ����ң�
        /// </summary>
        protected  void RefreshGroupMe()
        {
            this.Nodes.Clear();
            TreeNode rootNode = new TreeNode(FS.FrameWork.Management.Language.Msg("����"));
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 1;
            rootNode.Tag = null;
            this.Nodes.Add(rootNode);

            #region ��ӿ��ҽڵ㡢���ˡ�ȫԺ�ڵ�

            FS.HISFC.Models.Base.Employee person = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

            //��ӿ������׽ڵ�
            TreeNode deptNode = new TreeNode( FS.FrameWork.Management.Language.Msg( "��������" ) );
            deptNode.ImageIndex = 4;
            deptNode.SelectedImageIndex = 5;
            deptNode.Tag = null;
            rootNode.Nodes.Add(deptNode);

            //��Ӹ������׽ڵ�
            TreeNode perNode = new TreeNode( FS.FrameWork.Management.Language.Msg( "��������" ) );
            perNode.ImageIndex = 4;
            perNode.SelectedImageIndex = 5;
            perNode.Tag = null;
            rootNode.Nodes.Add(perNode);

            //���ȫԺ���׽ڵ�
            TreeNode allNode = new TreeNode( FS.FrameWork.Management.Language.Msg( "ȫԺ����" ) );
            allNode.ImageIndex = 4;
            allNode.SelectedImageIndex = 5;
            allNode.Tag = null;
            rootNode.Nodes.Add(allNode);

          
            #endregion

            #region "������� ��ȡ��ǰ���ҵĿ������� ��ǰ����Ա�ĸ�������  ȫԺ����"

            //��ѯ���������ļ���
            ArrayList alFolder = this.groupManager.GetAllFirstLVFolder(InpatientType, person.Dept.ID, person.ID);

            if (alFolder == null)
            {
                this.ShowErr(groupManager.Err);
                return;
            }

            //��ѯ�����ļ����ڵ�����
            ArrayList al = this.groupManager.GetDeptOrderGroup(InpatientType, person.Dept.ID, person.ID);
            if (al == null)
            {
                this.ShowErr(groupManager.Err);
                return;
            }

            #endregion

            TreeNode node;
            FS.HISFC.Models.Base.Group info;

            #region ��������ļ��м��ļ����µ�����

            //���������ļ���  ��ʼ��ʱֻ����2��Ŀ¼�����������ļ���ʱ���ټ��ض༶Ŀ¼

            try
            {
                for (int i = 0; i < alFolder.Count; i++)
                {
                    info = alFolder[i] as FS.HISFC.Models.Base.Group;
                    if (info == null)
                    {
                        continue;
                    }

                    node = new TreeNode(info.Name);
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 3;
                    node.Tag = info;

                    switch (info.Kind)
                    {
                        case FS.HISFC.Models.Base.GroupKinds.Dept:					//��������						
                            deptNode.Nodes.Add(node);
                            break;
                        case FS.HISFC.Models.Base.GroupKinds.Doctor:					//��������
                            perNode.Nodes.Add(node);
                            break;
                        case FS.HISFC.Models.Base.GroupKinds.All:					//ȫԺ����
                            allNode.Nodes.Add(node);
                            break;
                    }

                    //��ѯ�����ļ����µ�����
                    ArrayList alGroup = this.groupManager.GetGroupByFolderID(info.ID);
                    if (alGroup == null )
                    {
                        this.ShowErr(groupManager.Err);
                        continue;
                    }
                    else if (alGroup.Count == 0)
                    {
                        continue;
                    }

                    for (int j = 0; j < alGroup.Count; j++)
                    {
                        FS.HISFC.Models.Base.Group group = alGroup[j] as FS.HISFC.Models.Base.Group;
                        if (group == null)
                        {
                            continue;
                        }
                        TreeNode temNode = new TreeNode(group.Name);
                        temNode.ImageIndex = 10;
                        temNode.SelectedImageIndex = 11;
                        temNode.Tag = group;
                        node.Nodes.Add(temNode);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
                return;
            }
            #endregion

            #region ����ļ�����ĵ�������
            try
            {
                for (int i = 0; i < al.Count; i++)
                {
                    info = al[i] as FS.HISFC.Models.Base.Group;
                    if (info == null)
                    {
                        continue;
                    }

                    node = new TreeNode(info.Name);
                    node.ImageIndex = 10;
                    node.SelectedImageIndex = 11;
                    node.Tag = info;

                    switch (info.Kind)
                    {
                        case FS.HISFC.Models.Base.GroupKinds.Dept:					//��������						
                            deptNode.Nodes.Add(node);
                            break;
                        case FS.HISFC.Models.Base.GroupKinds.Doctor:					//��������
                            perNode.Nodes.Add(node);
                            break;
                        case FS.HISFC.Models.Base.GroupKinds.All:					//ȫԺ����
                            allNode.Nodes.Add(node);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
                return;
            }
            #endregion

            this.Nodes[0].Expand();

            //��������
            this.Nodes[0].Nodes[0].Expand();
            //��������
            this.Nodes[0].Nodes[1].Expand();
            //ȫԺ����
            this.Nodes[0].Nodes[2].Expand();
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public ArrayList AllGroup = null;
        
        /// <summary>
        /// ��ʾȫԺ���п��������б�  �������û�У��Ͳ�������
        /// </summary>
        protected void RefreshGroupAll()
        {
            this.Nodes.Clear();
            TreeNode rootNode = new TreeNode("ȫԺ����");
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 1;
            this.Nodes.Add(rootNode);

            ArrayList al = groupManager.GetAllOrderGroup(inpatientType);
            AllGroup = al;
            if (al == null) 
                return;

            ArrayList alDepts = new ArrayList();

            ArrayList alDept = manager.GetDepartment();

            FS.HISFC.Models.Base.Department obj = null;
            for (int i = 0; i < alDept.Count; i++)
            {
                obj = alDept[i] as FS.HISFC.Models.Base.Department;

                if (this.myType == enuType.Order)
                {
                    if (obj.DeptType.ID.ToString() == "I")
                    {
                        alDepts.Add(obj);
                    }
                }
                else if (this.myType == enuType.Fee)
                {
                    if (obj.DeptType.ID.ToString() == "N" || obj.DeptType.ToString() == "F")
                    {
                        alDepts.Add(obj);
                    }
                }
                else if (this.myType == enuType.Terminal)
                {
                    if (obj.DeptType.ID.ToString() == "T")
                    {
                        alDepts.Add(obj);
                    }
                }
            }
         
            AddDeptGroup(rootNode, alDepts);
            addPerson();
            addNodes(al);
            this.Nodes[0].Expand();
        }
      
        /// <summary>
        /// ��ӿ�������
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="alDepts"></param>
        private static void AddDeptGroup(TreeNode rootNode, ArrayList alDepts)
        {
            for (int i = 0; i < alDepts.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = alDepts[i] as FS.FrameWork.Models.NeuObject;
                TreeNode node = new TreeNode(obj.Name);
                node.ImageIndex = 2;
                node.SelectedImageIndex = 3;
                node.Tag = obj.ID;

                TreeNode cnode = new TreeNode("��������");
                cnode.ImageIndex = 4;
                cnode.SelectedImageIndex = 5;
                node.Nodes.Add(cnode);

                TreeNode cnode1 = new TreeNode("ҽ������");
                cnode1.ImageIndex = 4;
                cnode1.SelectedImageIndex = 5;
                node.Nodes.Add(cnode1);

                rootNode.Nodes.Add(node);
            }
        }

        /// <summary>
        /// ���������Ա
        /// </summary>
        private void addPerson()
        {
            for (int i = 0; i < personHelper.ArrayObject.Count; i++)
            {
                FS.HISFC.Models.Base.Employee obj = personHelper.ArrayObject[i] as FS.HISFC.Models.Base.Employee;
                TreeNode deptNode = this.GetDeptNodeByTag(obj.Dept.ID);
                if (deptNode == null)
                {

                }
                else
                {
                    TreeNode node = new TreeNode(obj.Name);
                    node.ImageIndex = 6;
                    node.SelectedImageIndex = 7;
                    node.Tag = obj.ID;
                    deptNode.Nodes[1].Nodes.Add(node);
                }
            }
        }

        private void addNodes(ArrayList al)
        {
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Base.Group info = al[i] as FS.HISFC.Models.Base.Group;
                TreeNode myRoot = null;
                myRoot = this.GetDeptNodeByTag(info.Dept.ID);
                if (info.Kind == FS.HISFC.Models.Base.GroupKinds.Dept)//��������
                {
                    if (myRoot != null)
                    {
                        TreeNode node = new TreeNode(info.Name);
                        node.ImageIndex = 10;
                        node.SelectedImageIndex = 11;
                        node.Tag = info;
                        myRoot.Nodes[0].Nodes.Add(node);
                    }
                }
                else
                {
                    if (myRoot != null)
                    {
                        myRoot = this.GetDocNodeByDeptNode(myRoot, info.Doctor.ID);
                        if (myRoot != null)
                        {
                            TreeNode node = new TreeNode(info.Name);
                            node.Tag = info;
                            node.ImageIndex = 10;
                            node.SelectedImageIndex = 11;
                            myRoot.Nodes.Add(node);
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// ��ÿ��ҽ��
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private TreeNode GetDeptNodeByTag(string tag)
        {
            foreach (TreeNode node in this.Nodes[0].Nodes)
            {
                if (node.Tag != null && node.Tag.ToString() == tag)
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// �����Ա���
        /// </summary>
        /// <param name="deptNode"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private TreeNode GetDocNodeByDeptNode(TreeNode deptNode, string tag)
        {
            foreach (TreeNode node in deptNode.Nodes[1].Nodes)
            {
                if (node.Tag != null && node.Tag.ToString() == tag)
                {
                    return node;
                }
            }
            return null;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �Ƿ�������ʾ������ϸ
        /// </summary>
        bool isShowGroupDetailing = false;

        /// <summary>
        /// �Ƿ�������ʾ������ϸ
        /// </summary>
        public bool IsShowGroupDetailing
        {
            get
            {
                return isShowGroupDetailing;
            }
            set
            {
                isShowGroupDetailing = value;

                if (isShowGroupDetailing)
                {
                    this.Enabled = false;
                }
                else
                {
                    this.Enabled = true;
                }
            }
        }

        /// <summary>
        /// ����Ҽ��¼�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.SelectedNode = this.GetNodeAt(e.X, e.Y);

            if (this.isLeftMouseEditGroupName == 1)
            {
                if (this.SelectedNode == null || this.SelectedNode.Tag == null)
                    this.LabelEdit = false;
                else
                    this.LabelEdit = true;
            }

            if (this.SelectedNode == this.Nodes[0])
            {
                return;
            }

            try
            {
                ContextMenu m = null;

                #region ����¼�

                if (e.Button == MouseButtons.Left)
                {
                    if (isDragDropEvent)
                    {
                        return;
                    }
                    if (this.isDoubleClickShowSelect == -1)
                    {
                        FS.HISFC.BizProcess.Integrate.Common.ControlParam contrIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                        this.isDoubleClickShowSelect = contrIntegrate.GetControlParam<int>("HNMZ36", true, 1);
                    }

                    if (isDoubleClickShowSelect == 1)
                    {
                        return;
                    }

                    if (this.SelectedNode.Tag != null 
                        && this.SelectedNode.Tag.GetType() == typeof(FS.HISFC.Models.Base.Group))
                    {
                        FS.HISFC.Models.Base.Group groupSelected = this.SelectedNode.Tag as FS.HISFC.Models.Base.Group;
                        //�ļ���
                        if (groupSelected.UserCode != "F")
                        {
                            this.ShowGroupDetail();
                        }
                    }
                }
                #endregion

                #region �Ҽ��¼�

                if (e.Button == MouseButtons.Right)
                {
                    if (this.fSelect != null && fSelect.Visible)
                    {
                        this.fSelect.Close();
                    }

                    MenuItem AddItem = new MenuItem("�����ļ���");
                    AddItem.Click += new EventHandler(AddItem_Click);

                    MenuItem deleteItem = new MenuItem("ɾ��");
                    deleteItem.Click += new EventHandler(deleteItem_Click);

                    MenuItem ChangeGroupName = new MenuItem("�޸���������");
                    ChangeGroupName.Click += new EventHandler(ChangeGroupName_Click);

                    //���ϼ��ڵ�
                    if (this.SelectedNode.Tag == null)
                    {
                        m = new ContextMenu();
                        m.MenuItems.Add(AddItem);
                        m.MenuItems.Add(deleteItem);

                        this.ContextMenu = m;
                        this.ContextMenu.Show(this, new Point(e.X, e.Y));
                    }
                    else
                    {
                        if (this.SelectedNode.Tag.GetType() == typeof(FS.HISFC.Models.Base.Group))
                        {
                            FS.HISFC.Models.Base.Group groupSelected = this.SelectedNode.Tag as FS.HISFC.Models.Base.Group;
                            //�ļ���
                            if (groupSelected.UserCode == "F")
                            {
                                if (this.SelectedNode.Nodes.Count > 0)
                                {
                                    m = new ContextMenu();
                                    m.MenuItems.Add(AddItem);
                                    m.MenuItems.Add(ChangeGroupName);

                                    this.ContextMenu = m;
                                    this.ContextMenu.Show(this, new Point(e.X, e.Y));
                                }
                                else
                                {
                                    m = new ContextMenu();
                                    m.MenuItems.Add(AddItem);
                                    m.MenuItems.Add(deleteItem);
                                    m.MenuItems.Add(ChangeGroupName);

                                    this.ContextMenu = m;
                                    this.ContextMenu.Show(this, new Point(e.X, e.Y));
                                }
                            }
                            else
                            {
                                m = new ContextMenu();
                                m.MenuItems.Add(deleteItem);

                                if (groupSelected.Kind == FS.HISFC.Models.Base.GroupKinds.All)
                                {
                                    if (this.isHaveDeptEditPower)
                                    {
                                        MenuItem copyToDeptGroup = new MenuItem("����Ϊ��������");
                                        copyToDeptGroup.Click += new EventHandler(copyAllToDeptGroup_Click);
                                        m.MenuItems.Add(copyToDeptGroup);
                                    }

                                    MenuItem copyAllToPersonGroup = new MenuItem("����Ϊ��������");
                                    copyAllToPersonGroup.Click += new EventHandler(copyAllToPersonGroup_Click);
                                    m.MenuItems.Add(copyAllToPersonGroup);

                                    m.MenuItems.Add(ChangeGroupName);
                                }
                                else if (groupSelected.Kind == FS.HISFC.Models.Base.GroupKinds.Dept)
                                {
                                    if (this.isHaveAllEditPower)
                                    {
                                        MenuItem copyDeptToAllGroup = new MenuItem("����ΪȫԺ����");
                                        copyDeptToAllGroup.Click += new EventHandler(copyDeptToAllGroup_Click);
                                        m.MenuItems.Add(copyDeptToAllGroup);
                                    }
                                    MenuItem copyDeptToPersonGroup = new MenuItem("����Ϊ��������");
                                    copyDeptToPersonGroup.Click += new EventHandler(copyDeptToPersonGroup_Click);
                                    m.MenuItems.Add(copyDeptToPersonGroup);

                                    m.MenuItems.Add(ChangeGroupName);
                                }
                                else
                                {
                                    if (this.isHaveAllEditPower)
                                    {
                                        MenuItem copyPersonToAllGroup = new MenuItem("����ΪȫԺ����");
                                        copyPersonToAllGroup.Click += new EventHandler(copyPersonToAllGroup_Click);
                                        m.MenuItems.Add(copyPersonToAllGroup);
                                    }

                                    if (this.isHaveDeptEditPower)
                                    {
                                        MenuItem copyPersonToDeptGroup = new MenuItem("����Ϊ��������");
                                        copyPersonToDeptGroup.Click += new EventHandler(copyPersonToDeptGroup_Click);
                                        m.MenuItems.Add(copyPersonToDeptGroup);
                                    }

                                    m.MenuItems.Add(ChangeGroupName);
                                }

                                this.ContextMenu = m;
                                this.ContextMenu.Show(this, new Point(e.X, e.Y));
                            }
                        }
                        else
                        {
                            this.ContextMenu = null;
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.ShowErr(ex.Message);
            }
        }

        #region ��������

        /// <summary>
        /// �������׸���ΪȫԺ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyDeptToAllGroup_Click(object sender, EventArgs e)
        {
            this.SaveGroup(FS.HISFC.Models.Base.GroupKinds.All);
        }

        /// <summary>
        /// �������׸���Ϊ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyDeptToPersonGroup_Click(object sender, EventArgs e)
        {
            this.SaveGroup(FS.HISFC.Models.Base.GroupKinds.Doctor);
        }

        /// <summary>
        /// ȫԺ���׸���Ϊ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyAllToPersonGroup_Click(object sender, EventArgs e)
        {
            this.SaveGroup(FS.HISFC.Models.Base.GroupKinds.Doctor);
        }

        /// <summary>
        /// ȫԺ���׸���Ϊ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyAllToDeptGroup_Click(object sender, EventArgs e)
        {
            this.SaveGroup(FS.HISFC.Models.Base.GroupKinds.Dept);
        }

        /// <summary>
        /// �������׸���ΪȫԺ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyPersonToAllGroup_Click(object sender, EventArgs e)
        {
            this.SaveGroup(FS.HISFC.Models.Base.GroupKinds.All);
        }

        /// <summary>
        /// �������׸���Ϊ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyPersonToDeptGroup_Click(object sender, EventArgs e)
        {
            this.SaveGroup(FS.HISFC.Models.Base.GroupKinds.Dept);
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="groupKind"></param>
        private void SaveGroup(FS.HISFC.Models.Base.GroupKinds groupKind)
        {
            //������
            FS.HISFC.Models.Base.Group newGroup = (this.SelectedNode.Tag as FS.HISFC.Models.Base.Group).Clone();
            ArrayList alDetail = groupManager.GetAllItem(newGroup);
            if (alDetail == null)
            {
                this.ShowErr("����������ϸʧ��:" + groupManager.Err);
                return;
            }

            //1 ���� 2 סԺ
            string outOrIn = newGroup.UserType == FS.HISFC.Models.Base.ServiceTypes.C ? "1" : "2";

            newGroup.Kind = groupKind;
            newGroup.ID = groupManager.GetNewGroupID();
            newGroup.Dept = ((FS.HISFC.Models.Base.Employee)groupManager.Operator).Dept;
            newGroup.Doctor = groupManager.Operator;
            newGroup.ParentID = null;

            //���ױ�ע���������� {2E2FE2A6-3C9C-431e-908F-77B5B941E5F9} houwb
            if (string.IsNullOrEmpty(newGroup.Memo))
            {
                newGroup.Memo = this.groupManager.GetMaxGroupSortID();
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (groupManager.UpdateGroup(newGroup) == -1)
            {
                FrameWork.Management.PublicTrans.RollBack();
                this.ShowErr("��������ʧ��:" + groupManager.Err);
                return;
            }

            //����
            if (outOrIn == "1")
            {
                foreach (FS.HISFC.Models.Order.OutPatient.Order order in alDetail)
                {
                    order.Item.ID = order.ID;
                    if (groupManager.UpdateGroupItem(newGroup, order) == -1)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        this.ShowErr("����������ϸʧ��:" + groupManager.Err);
                        return;
                    }
                }
            }
            //סԺ
            else
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alDetail)
                {
                    order.Item.ID = order.ID;

                    if (groupManager.UpdateGroupItem(newGroup, order) == -1)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        this.ShowErr("����������ϸʧ��:" + groupManager.Err);
                        return;
                    }
                }
            }

            FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�");
            this.RefrshGroup();
        }

        #endregion

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.ContextMenu != null)
            {
                if (this.ContextMenu.MenuItems.Count > 0)
                {
                    this.ContextMenu.MenuItems.Clear();
                }
            }

            //this.SelectedNode = this.GetNodeAt(e.X, e.Y);

            //if (this.isLeftMouseEditGroupName == 1)
            //{
            //    if (this.SelectedNode == null || this.SelectedNode.Tag == null)
            //        this.LabelEdit = false;
            //    else
            //        this.LabelEdit = true;
            //}

            //if (this.SelectedNode == this.Nodes[0])
            //{
            //    return;
            //}

            //try
            //{
            //    ContextMenu m = null;

            //    #region ����¼�

            //    if (e.Button == MouseButtons.Left)
            //    {
            //        if (this.isDoubleClickShowSelect == -1)
            //        {
            //            FS.HISFC.BizProcess.Integrate.Common.ControlParam contrIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //            this.isDoubleClickShowSelect = contrIntegrate.GetControlParam<int>("HNMZ36", true, 1);
            //        }

            //        if (isDoubleClickShowSelect == 1)
            //        {
            //            return;
            //        }

            //        if (this.SelectedNode.Tag.GetType() == typeof(FS.HISFC.Models.Base.Group))
            //        {
            //            FS.HISFC.Models.Base.Group groupSelected = this.SelectedNode.Tag as FS.HISFC.Models.Base.Group;
            //            //�ļ���
            //            if (groupSelected.UserCode != "F")
            //            {
            //                this.ShowGroupDetail();
            //            }
            //        }
            //    }
            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    this.ShowErr(ex.Message);
            //}
        }

        /// <summary>
        /// �޸���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChangeGroupName_Click(object sender, EventArgs e)
        {
            isAllowEditGroupName = true;

            if (this.SelectedNode.Tag != null)
            {
                this.LabelEdit = true;
                groupOldName = this.SelectedNode.Text;

                this.SelectedNode.BeginEdit();
            }
        }

        /// <summary>
        /// �����ļ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddItem_Click(object sender, EventArgs e)
        {
            #region �����޸��ж�

            FS.HISFC.Models.Base.Group info = this.SelectedNode.Tag as FS.HISFC.Models.Base.Group;

            if (info != null && info.Kind == FS.HISFC.Models.Base.GroupKinds.Dept&&!this.isHaveDeptEditPower)
            {
                this.ShowErr("��û���޸Ŀ������׵�Ȩ��!");
                return;
            }

            if (info != null && info.Kind == FS.HISFC.Models.Base.GroupKinds.All&&!this.isHaveAllEditPower)
            {
                this.ShowErr("��û���޸�ȫԺ���׵�Ȩ��!");
                return;
            }

            #endregion

            TreeNode node = new TreeNode();
            node.ImageIndex = 2;
            node.SelectedImageIndex = 3;
            FS.HISFC.Models.Base.Group group = new FS.HISFC.Models.Base.Group();
            group.ID = this.groupManager.GetNewFolderID();
            group.Name = "�½��ļ���";
            group.UserType = this.inpatientType;

            if (this.SelectedNode == this.Nodes[0].Nodes[0])
            {
                group.Kind = FS.HISFC.Models.Base.GroupKinds.Dept;
                group.Dept.ID = (this.groupManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                group.Doctor = this.groupManager.Operator;
                group.ParentID = "ROOT";
            }
            else if (this.SelectedNode == this.Nodes[0].Nodes[1])
            {
                group.Kind = FS.HISFC.Models.Base.GroupKinds.Doctor;
                group.Dept.ID = (this.groupManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                group.Doctor = this.groupManager.Operator;
                group.ParentID = "ROOT";
            }
            else if (this.SelectedNode == this.Nodes[0].Nodes[2])
            {
                group.Kind = FS.HISFC.Models.Base.GroupKinds.All;
                group.Dept.ID = "ALL";
                group.Doctor = this.groupManager.Operator;
                group.ParentID = "ROOT";
            }
            else
            {
                FS.HISFC.Models.Base.Group groupSelected = this.SelectedNode.Tag as FS.HISFC.Models.Base.Group;

                group.Kind = groupSelected.Kind;
                group.Dept = groupSelected.Dept;

                group.Doctor = this.groupManager.Operator;
                group.ParentID = groupSelected.ID;
            }

            group.UserCode = "F";

            if (this.groupManager.SetNewFolder(group) < 0)
            {
                ShowErr("�����ļ���ʧ�ܣ�");
                return;
            }
            node.Text = group.Name;
            node.Tag = group;
            this.SelectedNode.Nodes.Add(node);
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="errInfo"></param>
        private void ShowErr(string errInfo)
        {
            MessageBox.Show(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                #region �����޸��ж�

                FS.HISFC.Models.Base.Group info = this.SelectedNode.Tag as FS.HISFC.Models.Base.Group;

                if (info != null && info.Kind == FS.HISFC.Models.Base.GroupKinds.All && !this.isHaveAllEditPower)
                {
                    this.ShowErr("��û���޸�ȫԺ���׵�Ȩ��!");
                    return;
                }
                if (info != null && info.Kind == FS.HISFC.Models.Base.GroupKinds.Dept && !this.isHaveDeptEditPower)
                {
                    this.ShowErr("��û���޸Ŀ������׵�Ȩ��!");
                    return;
                }

                #endregion

                if (info.UserCode == "F")//�ļ���
                {
                    if (MessageBox.Show("�Ƿ�ɾ���ļ���" + info.Name, "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        if (this.groupManager.deleteFolder(info) < 0)
                        {
                            ShowErr(this.groupManager.Err);
                        }
                        this.RefrshGroup();
                    }
                }
                else
                {
                    if (MessageBox.Show("�Ƿ�ɾ������" + info.Name, "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        /*
                         * �Ե�����Ȩ����ά��
                         * 
                        if (!(this.groupManager.Operator as FS.HISFC.Models.Base.Employee).IsManager &&
                            info.Kind == FS.HISFC.Models.Base.GroupKinds.All)
                        {
                            MessageBox.Show("�����ǹ���Ա,û��Ȩ��ɾ������", "��ʾ");
                            return;
                        }
                        */
                        if (this.groupManager.DeleteGroup(info) == -1)
                        {
                            ShowErr(this.groupManager.Err);
                        }
                        this.RefrshGroup();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// ѡ�к�ˢ�������ӽڵ�����
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            if (isDragDropEvent)
            {
                return;
            }
            object o = this.SelectedNode.Tag;
            if (o != null)
            {
                if (o.GetType() == typeof(FS.HISFC.Models.Base.Group))
                {
                    //if (this.SelectedNode.Nodes.Count > 0)
                    //{
                    //    return;
                    //}
                    this.SelectedNode.Nodes.Clear();
                    FS.HISFC.Models.Base.Group info = o as FS.HISFC.Models.Base.Group;
                    if (info.UserCode == "F")
                    {
                        ArrayList alFolder = this.groupManager.GetAllFolderByFolderID(info.ID);

                        if (alFolder == null)
                        {
                            return;
                        }

                        try
                        {
                            TreeNode node;

                            FS.HISFC.Models.Base.Group myGroup;
                            for (int i = 0; i < alFolder.Count; i++)
                            {
                                myGroup = alFolder[i] as FS.HISFC.Models.Base.Group;
                                if (info == null)
                                {
                                    continue;
                                }
                                node = new TreeNode(myGroup.Name);
                                node.ImageIndex = 2;
                                node.SelectedImageIndex = 3;
                                node.Tag = myGroup;


                                switch (myGroup.Kind)
                                {
                                    case FS.HISFC.Models.Base.GroupKinds.Dept:					//��������						
                                        this.SelectedNode.Nodes.Add(node);
                                        break;
                                    case FS.HISFC.Models.Base.GroupKinds.Doctor:					//��������
                                        this.SelectedNode.Nodes.Add(node);
                                        break;
                                    case FS.HISFC.Models.Base.GroupKinds.All:					//ȫԺ����
                                        this.SelectedNode.Nodes.Add(node);
                                        break;
                                }
                            }
                            ArrayList alGroup = this.groupManager.GetGroupByFolderID(info.ID);
                            if (alGroup != null && alGroup.Count > 0)
                            {
                                for (int j = 0; j < alGroup.Count; j++)
                                {
                                    FS.HISFC.Models.Base.Group group = alGroup[j] as FS.HISFC.Models.Base.Group;
                                    if (group == null)
                                    {
                                        continue;
                                    }
                                    TreeNode temNode = new TreeNode(group.Name);
                                    temNode.ImageIndex = 10;
                                    temNode.SelectedImageIndex = 11;
                                    temNode.Tag = group;
                                    this.SelectedNode.Nodes.Add(temNode);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            ShowErr(ex.Message);
                            return;
                        }
                    }
                }
            }
            base.OnAfterSelect(e);
        }

        /// <summary>
        /// ���ֱ��༭֮ǰ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            if (this.isLeftMouseEditGroupName != 1 && !isAllowEditGroupName)
            {
                return;
            }

            if (this.SelectedNode == null)
            {
                this.groupOldName = "";
            }

            this.groupOldName = this.SelectedNode.Text;

            base.OnBeforeLabelEdit(e);
        }

        /// <summary>
        /// ���Ʊ��༭
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            if (this.isLeftMouseEditGroupName != 1 && !isAllowEditGroupName)
            {
                return;
            }

            if (e.Label == null)
            {
                this.LabelEdit = false;
                return;
            }

            FS.HISFC.Models.Base.Group group = this.SelectedNode.Tag as FS.HISFC.Models.Base.Group;

            if (group.Kind == FS.HISFC.Models.Base.GroupKinds.Dept && !this.isHaveDeptEditPower)
            {
                this.ShowErr("��û���޸Ŀ������׵�Ȩ��!");
                this.SelectedNode.Text = this.groupOldName;
                return;
            }
            if (group.Kind == FS.HISFC.Models.Base.GroupKinds.All && !this.isHaveAllEditPower)
            {
                this.ShowErr("��û���޸�ȫԺ���׵�Ȩ��!");
                this.SelectedNode.Text = this.groupOldName;
                return;
            }

            DialogResult r = MessageBox.Show("�ڵ������Ѿ��޸ģ��Ƿ񱣴棿", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (r == DialogResult.Cancel)
            {
                this.LabelEdit = false;
                this.SelectedNode.Text = this.groupOldName;
                this.RefrshGroup();
                return;
            }

            if ((this.SelectedNode.Tag as FS.HISFC.Models.Base.Group).UserCode == "F")
            {
                FS.HISFC.Models.Base.Group tem = this.SelectedNode.Tag as FS.HISFC.Models.Base.Group;
                tem.Name = e.Label;
                if (this.groupManager.updateFolder(tem) <= 0)
                {
                    MessageBox.Show("�ļ������Ƹ���ʧ�ܡ�", "��ʾ");
                }
                else
                {
                    MessageBox.Show("�ļ������Ƹ��³ɹ���", "��ʾ");
                }
            }
            else
            {
                string GroupId = (this.SelectedNode.Tag as FS.HISFC.Models.Base.Group).ID;
                if (groupManager.UpdateGroupName(GroupId, e.Label) > 0)
                    MessageBox.Show("�������Ƹ��³ɹ�", "��ʾ");
                else
                {
                    MessageBox.Show("����ʧ��", "��ʾ");
                }
            }

            this.LabelEdit = false;
        }

        /// <summary>
        /// ѡ�нڵ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDoctorGroup_AfterSelect(object sender, TreeViewEventArgs e)
        {
            return;

            if (this.isDoubleClickShowSelect == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam contrIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                this.isDoubleClickShowSelect = contrIntegrate.GetControlParam<int>("HNMZ36", true, 1);
            }

            if (isDoubleClickShowSelect == 1)
            {
                return;
            }

            this.ShowGroupDetail();
        }

        /// <summary>
        /// ˫��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDoubleClick(EventArgs e)
        {
            if (this.isDoubleClickShowSelect == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam contrIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                this.isDoubleClickShowSelect = contrIntegrate.GetControlParam<int>("HNMZ36", true, 0);
            }

            if (isDoubleClickShowSelect != 1)
            {
                return;
            }

            this.ShowGroupDetail();
        }

        Forms.frmSelectGroup fSelect = null;

        /// <summary>
        /// ��ʾ������ϸ
        /// </summary>
        private void ShowGroupDetail()
        {
            object o = this.SelectedNode.Tag;
            if (o != null)
            {
                if (o.GetType() == typeof(FS.HISFC.Models.Base.Group))
                {
                    FS.HISFC.Models.Base.Group info = o as FS.HISFC.Models.Base.Group;
                    //�ļ���
                    if (info.UserCode == "F")
                    {
                        this.SelectedNode.Expand();
                        return;
                    }

                    if (this.isShowDetailDialog == -1)
                    {
                        FS.HISFC.BizProcess.Integrate.Common.ControlParam contrIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                        this.isShowDetailDialog = contrIntegrate.GetControlParam<int>("HNMZ37", true, 1);
                    }

                    if (fSelect == null||fSelect.IsDisposed)
                    {
                        fSelect = new FS.HISFC.Components.Common.Forms.frmSelectGroup();
                        fSelect.IsShowDetailDialog = isShowDetailDialog;

                        fSelect.IsEditGroup = this.isEditGroup;
                        fSelect.Pact = this.pact;

                        fSelect.Init(info);

                        if (this.isShowDetailDialog == 0)
                        {
                            fSelect.GroupConfirm -= this.AfterSelectOrder;
                            fSelect.GroupConfirm += this.AfterSelectOrder;
                        }
                    }
                    else
                    {

                        fSelect.IsEditGroup = this.isEditGroup;
                        fSelect.Pact = this.pact;
                        fSelect.Init(info);
                    }

                    fSelect.InpatientType = this.inpatientType;
                    fSelect.IsHaveAllEditPower = this.isHaveAllEditPower;
                    fSelect.IsHaveDeptEditPower = this.isHaveDeptEditPower;

                    fSelect.Text = "����ѡ��  ��" + info.Name + "��";

                    if (this.isShowDetailDialog == 1)
                    {
                        fSelect.ShowDialog();

                        if (fSelect.DialogResult == DialogResult.OK)
                        {
                            try
                            {
                                if (SelectOrder != null)
                                {
                                    SelectOrder(fSelect.Orders);
                                }
                            }
                            catch (Exception ex)
                            {
                                this.ShowErr(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        fSelect.Visible = false;
                        fSelect.Show();
                        fSelect.TopMost = true;
                    }
                }
            }
        }

        private void AfterSelectOrder()
        {
            try
            {
                if (SelectOrder != null)
                {
                    SelectOrder(fSelect.Orders);
                }
            }
            catch (Exception ex)
            {
                this.ShowErr(ex.Message);
            }
        }

        private void tvDoctorGroup_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = System.Windows.Forms.DragDropEffects.Move;
        }

        private void tvDoctorGroup_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            System.Drawing.Point position = new Point(0, 0);
            position.X = e.X;
            position.Y = e.Y;
            position = this.PointToClient(position);
            TreeNode dropNode = this.GetNodeAt(position);
            this.isDragDropEvent = true;
            this.SelectedNode = dropNode;
            isDragDropEvent = false;
            this.Focus();
        }

        private void tvDoctorGroup_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //��ʼ����"Drag"����
                DoDragDrop((TreeNode)e.Item, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// �Ƿ����϶��¼����϶�ʱ�����¼����ļ����µĽڵ㣬������ļ������϶��������ڵ�������
        /// </summary>
        bool isDragDropEvent = false;

        /// <summary>
        /// �ƶ��ڵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDoctorGroup_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            TreeNode temp = new TreeNode();

            //�õ�Ҫ�ƶ��Ľڵ�
            TreeNode moveNode = (TreeNode)e.Data.GetData(temp.GetType());
            //ת������Ϊ�ؼ�treeview������
            Point position = new Point(0, 0);
            position.X = e.X;
            position.Y = e.Y;
            position = this.PointToClient(position);

            //�õ��ƶ���Ŀ�ĵصĽڵ�
            TreeNode aimNode = this.GetNodeAt(position);
            if (aimNode == null)//�������� ����
            {
                return;
            }

            FS.HISFC.Models.Base.Group fromGroup = moveNode.Tag as FS.HISFC.Models.Base.Group;
            FS.HISFC.Models.Base.Group toGroup = aimNode.Tag as FS.HISFC.Models.Base.Group;

            if (fromGroup == null) //�����׸��ڵ� ����
            {
                return;
            }
            if (fromGroup.UserCode == "F")//���ļ��нڵ� ����
            {
                MessageBox.Show("�ļ��в������ƶ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }
            if (toGroup != null && fromGroup.ParentID == toGroup.ID)
            {
                MessageBox.Show("Ŀ��ڵ��Ѿ��Ǹ����ڵ㣬����Ҫ�ƶ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (toGroup == null)//Ŀ��ڵ� �Ǹ��ڵ�
            {
                if (aimNode.Text == "��������")
                {
                    toGroup = new FS.HISFC.Models.Base.Group();
                    toGroup.ID = "";
                    toGroup.Name = "��������";
                    toGroup.UserCode = "F";
                    toGroup.Kind = FS.HISFC.Models.Base.GroupKinds.Dept;
                }
                else if (aimNode.Text == "��������")
                {
                    toGroup = new FS.HISFC.Models.Base.Group();
                    toGroup.ID = "";
                    toGroup.Name = "��������";
                    toGroup.UserCode = "F";
                    toGroup.Kind = FS.HISFC.Models.Base.GroupKinds.Doctor;
                }
                else if (aimNode.Text == "ȫԺ����")
                {
                    toGroup = new FS.HISFC.Models.Base.Group();
                    toGroup.ID = "";
                    toGroup.Name = "ȫԺ����";
                    toGroup.UserCode = "F";
                    toGroup.Kind = FS.HISFC.Models.Base.GroupKinds.All;
                }
                //return;
            }

            #region �����޸��ж�

            if (fromGroup.Kind == FS.HISFC.Models.Base.GroupKinds.Dept && !this.isHaveDeptEditPower)
            {
                this.ShowErr("��û���޸Ŀ������׵�Ȩ��!");
                return;
            }
            if (fromGroup.Kind == FS.HISFC.Models.Base.GroupKinds.All && !this.isHaveAllEditPower)
            {
                this.ShowErr("��û���޸�ȫԺ���׵�Ȩ��!");
                return;
            }

            if (toGroup != null && 
                toGroup.Kind == FS.HISFC.Models.Base.GroupKinds.Dept && !this.isHaveDeptEditPower)
            {
                this.ShowErr("��û���޸Ŀ������׵�Ȩ��!");
                return;
            }
            if (toGroup != null
                && toGroup.Kind == FS.HISFC.Models.Base.GroupKinds.All && !this.isHaveAllEditPower)
            {
                this.ShowErr("��û���޸�ȫԺ���׵�Ȩ��!");
                return;
            }

            #endregion

            if (toGroup != null && fromGroup.Kind != toGroup.Kind)//���Һ͸���֮�䲻������
            {
                this.ShowErr("��ͬ���䲻�����϶���");
                return;
            }

            //Ŀ��ڵ�Ҳ�����ף��û�λ��
            if (toGroup.UserCode != "F")//Ŀ��ڵ㲻���ļ���
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //���������� {2E2FE2A6-3C9C-431e-908F-77B5B941E5F9} houwb
                if (this.groupManager.UpdateGroupSortID(fromGroup.ID, toGroup.Memo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����������ʧ�ܣ�" + groupManager.Err);
                    return;
                }

                //���������� {2E2FE2A6-3C9C-431e-908F-77B5B941E5F9} houwb
                if(this.groupManager.UpdateGroupSortID(toGroup.ID, fromGroup.Memo)==-1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����������ʧ�ܣ�" + groupManager.Err);
                    return;
                }
                string memoTemp = toGroup.Memo;
                //�û���ע
                toGroup.Memo = fromGroup.Memo;
                fromGroup.Memo = memoTemp;
                //�û����ڵ�
                memoTemp = toGroup.ParentID;
                toGroup.ParentID = fromGroup.ParentID;
                fromGroup.ParentID = memoTemp;

                FS.FrameWork.Models.NeuObject groupObjTemp = new FS.FrameWork.Models.NeuObject();
                groupObjTemp = fromGroup.Clone();
                moveNode.Tag = toGroup;
                moveNode.Text = toGroup.Name;
                aimNode.Tag = groupObjTemp;
                aimNode.Text = groupObjTemp.Name;

                FS.FrameWork.Management.PublicTrans.Commit();
                return;
            }

            if (IsDragEnable(aimNode, moveNode) == true)
            {
                if (aimNode != moveNode)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.HISFC.Models.Base.Group temGroup = aimNode.Tag as FS.HISFC.Models.Base.Group;
                    //FS.HISFC.Models.Base.Group tempGroup = moveNode.Tag as FS.HISFC.Models.Base.Group;

                    if (fromGroup == null || toGroup == null)
                    {
                        return;
                    }
                    try
                    {
                        if (this.groupManager.UpdateGroupFolderID(fromGroup.ID, toGroup.ID) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.ShowErr("�϶����׵��ļ���ʧ��:" + groupManager.Err);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.ShowErr("�϶����׵��ļ���ʧ��:" + groupManager.Err);
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();

                    this.Nodes.Remove(moveNode);
                    aimNode.Nodes.Add(moveNode);
                    fromGroup.ParentID = toGroup.ID;
                    moveNode.Tag = fromGroup;

                    //this.RefrshGroup();
                }
            }
        }

        /// <summary>
        /// �ж��Ƿ�����϶���Ŀ��ڵ㣬��������򷵻�true������Ϊfalse;
        /// �жϸ����ǣ�Ŀ��ڵ㲻���Ǳ��϶��Ľڵ�ĸ��׽ڵ㣡
        /// </summary>
        private bool IsDragEnable(TreeNode aimNode, TreeNode oriNode)
        {
            while (aimNode != null)
            {
                if (aimNode.Parent != oriNode)
                {
                    aimNode = aimNode.Parent;
                    IsDragEnable(aimNode, oriNode);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

    }

    /// <summary>
    /// ��ʾ����
    /// </summary>
    public enum enuGroupShowType
    {
        /// <summary>
        /// ȫԺ����
        /// </summary>
        All = 0,

        /// <summary>
        /// ��������
        /// </summary>
        Dept = 1,

        /// <summary>
        /// ��������
        /// </summary>
        Person = 1
    }

    /// <summary>
    /// ����orסԺ
    /// </summary>
    public enum enuInpatientType
    {
        /// <summary>
        /// ����
        /// </summary>
        C = 0,

        /// <summary>
        /// סԺ
        /// </summary>
        I = 1 
    }

    /// <summary>
    /// ����
    /// </summary>
    public enum enuType
    {
        /// <summary>
        /// ҽ��
        /// </summary>
        Order = 0,

        /// <summary>
        /// ����
        /// </summary>
        Fee = 1,

        /// <summary>
        /// �նˣ���
        /// </summary>
        Terminal = 2
    }
}
