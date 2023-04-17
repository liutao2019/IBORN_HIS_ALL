using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ���ҽṹά����]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��11��27]<br></br>
    /// 
    /// ˵������Load�����д����Tag��Ϊ�Ժ�������������
    /// ��ϵͳ��ά�������� ����ģ�鴰����ô��ݵ�Tag
    /// Tag������ �ɸ���com_priv_class1 �µ�Class1_Code��������
    /// �磺 Tag.toString()="@@"��ʱ����ʾȫ�������
    ///      Tag.toString()="03"��ʱ����ʾҩ��Ȩ�޹�����
    ///     ��Tag.toString()=""����ʾ
    /// 
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDepartmentLevel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        
        public ucDepartmentLevel()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// �����ڼӿ���ʱ�жϸøÿ����ڱ��ṹ���Ƿ��Ѿ�����
        /// </summary>
        bool bl = true;
        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
           
            //���ӹ�����
            this.toolBarService.AddToolButton("�ϼ�����", "�ϼ�����",0, true, false, null);
            this.toolBarService.AddToolButton("����", "����", 1, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "ɾ��", 2, true, false, null);
            this.toolBarService.AddToolButton("����", "����", 2, true, false, null);
            return this.toolBarService;
        }
        #endregion


        #region ��д���а�ť�¼�
        public override int Exit(object sender, object neuObject)
        {
            return base.Exit(sender, neuObject);
        }
        #endregion


        #region ���������Ӱ�ť�����¼�

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�ϼ�����":
                    if (this.tvDepartmentLevelTree1.SelectedNode.Parent != null)
                        this.tvDepartmentLevelTree1.SelectedNode = this.tvDepartmentLevelTree1.SelectedNode.Parent;
                    break;
                case "ɾ��":
                    this.DelDepartment();
                    break;
                case "����":
                    this.Property(true);
                    break;
                case "����":
                    SearchTree();
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void SearchTree()
        {
            FS.HISFC.Components.Common.Forms.frmTreeNodeSearch frm = new FS.HISFC.Components.Common.Forms.frmTreeNodeSearch();
            frm.Init(tvDepartmentLevelTree1);
            frm.ShowDialog();
        }
    #endregion

        #region  �����Ϳؼ���ǰ��ѡ�нڵ�Ķ��ӽڵ㣨�û�����ʾ��listView��
        /// <summary>
        /// �����Ϳؼ���ǰ��ѡ�нڵ�Ķ��ӽڵ㣨�û�����ʾ��listView��
        /// </summary>
        private void ShowListUser()
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            FS.HISFC.Models.Base.DepartmentStat dept = new FS.HISFC.Models.Base.DepartmentStat();
            ParentNodeStat(this.tvDepartmentLevelTree1.SelectedNode, ref dept);
            System.Collections.ArrayList al = userManager.LoadUser(dept.StatCode, dept.PardepCode);
            foreach (FS.HISFC.Models.Admin.UserPowerDetail info in al)
            {
                ListViewItem item = this.neuListView1.Items.Add(info.User.Name);
                item.Tag = info;
                if (info.User01 == "F")
                {
                    item.ImageIndex = 3;
                    item.StateImageIndex = 3;
                }
                else
                {
                    item.ImageIndex = 2;
                    item.StateImageIndex = 2;
                }
            }
        }
        #endregion

        #region ���ݴ���Ľڵ㣬��������ڵ�������������
        /// <summary>
        /// ���ݴ���Ľڵ㣬��������ڵ�������������
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private void ParentNodeStat(TreeNode node, ref FS.HISFC.Models.Base.DepartmentStat dept)
        {
            if (dept == null) dept = new FS.HISFC.Models.Base.DepartmentStat();
            if (node.Parent == null)
            {
                //�����ڵ���ͳ�ƴ���ʱ��ֱ��ȡ�����
                dept.StatCode = node.Tag.ToString();  //ȡͳ�ƴ������
                dept.PardepCode = "AAAA";             //ȡ��������
                dept.PardepName = node.Text;          //ȡ��������
                dept.GradeCode = 1;				  //ȡ�ڵ�ȼ�
            }
            else
            {
                //�����ڵ㲻��ͳ�ƴ���ʱ����������ͳ�ƴ������
                TreeNode thisNode = node;
                //�ҵ����ڵ�
                while (thisNode.Parent != null)
                {
                    thisNode = thisNode.Parent;
                }
                if (thisNode != null)
                    dept.StatCode = thisNode.Tag.ToString();
                else
                    dept.StatCode = "0000";

                FS.HISFC.Models.Base.DepartmentStat parentStat = node.Tag as FS.HISFC.Models.Base.DepartmentStat;
                if (parentStat == null)
                {
                    MessageBox.Show("ȡ����" + dept.DeptName + "�ĸ�������ʱ����", "����ʧ��");
                    return;
                }
                dept.PardepCode = parentStat.DeptCode;      //ȡ��������
                dept.PardepName = parentStat.DeptName;      //ȡ�������� 
                dept.GradeCode = parentStat.GradeCode + 1; //ȡ�ڵ�ȼ�
            }
        }
        #endregion

        #region  ��ʾ���һ�����Ա������
        /// <summary>
        /// ��ʾ���һ�����Ա������
        /// </summary>
        /// <param name="ShowProperty">true��ʾ���ԣ�false���˫��ʱ��ʾ���ҵ���һ������</param>
        public void Property(bool ShowProperty)
        {
            if (this.neuListView1.SelectedItems.Count > 0 && this.neuListView1.Focused)
            {
                //���赱ǰ�ڵ�Ϊ������Ϣ
                TreeNode node = this.neuListView1.SelectedItems[0].Tag as TreeNode;
                if (node == null)
                {
                    //��Ա�����޸�
                    this.UserProperty();
                }
                else
                {
                    //ShowProperty   true��ʾ���ԣ�false���˫��ʱ��ʾ���ҵ���һ������
                    if (ShowProperty)
                    {
                        //��ʾ��������
                        this.DeptProperty();
                    }
                    else
                    {
                        //��ʾѡ�п��ҵ���һ�����ݡ�
                        this.tvDepartmentLevelTree1.SelectedNode = node;
                    }
                }
            }
            else
            {
                if (this.tvDepartmentLevelTree1.SelectedNode != null && this.tvDepartmentLevelTree1.SelectedNode.Parent != null)
                {
                    //��ʾ��������
                    this.DeptProperty();
                }
            }
        }
        #endregion

        #region �����Ա
        /// <summary>
        /// �����Ա
        /// </summary>
        public void AddUser()
        {
            //�������ڴ����¼��������������Ա
            if (this.tvDepartmentLevelTree1.SelectedNode.Parent == null) return;

            //ȡ�õ�ǰTreeView�еĿ�����Ϣ
            FS.HISFC.Models.Base.DepartmentStat dept = this.tvDepartmentLevelTree1.SelectedNode.Tag as FS.HISFC.Models.Base.DepartmentStat;

            //ȡ�õ�ǰListView��Ҫ�޸ĵ���Ա����
            //��ԱȨ����ϸʵ����
            FS.HISFC.Models.Admin.UserPowerDetail userPower = new FS.HISFC.Models.Admin.UserPowerDetail();

            userPower.Dept.ID = dept.DeptCode;
            userPower.Dept.Name = dept.DeptName;
            userPower.Class1Code = dept.StatCode;
            userPower.GrantDept = dept.DeptCode;
            if (dept != null)
            {
                Manager.Controls.ucPrivUserManager userManager = new Manager.Controls.ucPrivUserManager(userPower);
                //������ʱ���������޸�����
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��Ա����";
                DialogResult dlg = FS.FrameWork.WinForms.Classes.Function.PopShowControl(userManager);
                //ȡ���ڷ��ز���
                if (dlg == DialogResult.OK)
                    //��ʾ��ǰTreeView��ѡ�нڵ���¼�����
                    this.ShowList();
            }
        }
        #endregion

        #region ��Ա����
        /// <summary>
        /// ��Ա����
        /// </summary>
        public void UserProperty()
        {
            //ȡ�õ�ǰTreeView�еĿ�����Ϣ
            FS.HISFC.Models.Base.DepartmentStat dept = this.tvDepartmentLevelTree1.SelectedNode.Tag as FS.HISFC.Models.Base.DepartmentStat;
            //ȡ�õ�ǰListView��Ҫ�޸ĵ���Ա����
            FS.HISFC.Models.Admin.UserPowerDetail userPower = this.neuListView1.SelectedItems[0].Tag as FS.HISFC.Models.Admin.UserPowerDetail;
            userPower.Dept.ID = dept.DeptCode;//���ұ���
            userPower.Dept.Name = dept.DeptName;//��������
            userPower.GrantDept = dept.DeptCode;
            if (dept != null)
            {

                Manager.Controls.ucPrivUserManager userManager = new Manager.Controls.ucPrivUserManager(userPower);
                //������ʱ���������޸�����
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��Ա����";
                DialogResult dlg = FS.FrameWork.WinForms.Classes.Function.PopShowControl(userManager);
                //ȡ���ڷ��ز���
                if (dlg == DialogResult.OK)
                {
                    //����ListView�п��ҵ�����
                    //this.lvInfo.SelectedItems[0].Text = userPower.User.Name;
                    //��ʾ��ǰTreeView��ѡ�нڵ���¼�����
                    this.ShowList();
                }
            }
        }
        #endregion

        #region ��ӿ���
        /// <summary>
        /// ��ӿ���
        /// </summary>
        public void AddDepartment()
        {
            //������������
            FS.HISFC.Models.Base.DepartmentStat dept = new FS.HISFC.Models.Base.DepartmentStat();

            //ȡ�����ڵ��ͳ�ƴ�����롢�������롢��������		
            ParentNodeStat(this.tvDepartmentLevelTree1.SelectedNode, ref dept);

            //�����ڵ�ΪҶ�ӽڵ㡣
            dept.NodeKind = 1;
                      
            ucDepartmentStat deptLevel = new ucDepartmentStat(dept);

            //�����¼�(�����ж��Ƿ����ӵ��Ǳ�����)·־��,2007-4-11
            ucDepartmentStat.DoCheckNode += new ucDepartmentStat.CheckHander(ucDepartmentStat_DoCheckNode);
            
            //������ʱ���������޸�����
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��������";
            DialogResult dlg = FS.FrameWork.WinForms.Classes.Function.PopShowControl(deptLevel);
            if (dlg == DialogResult.OK)
            {
                this.tvDepartmentLevelTree1.AddDepartment(this.tvDepartmentLevelTree1.SelectedNode, dept);
                //��ʾ��ǰTreeView��ѡ�нڵ���¼�����
                this.ShowList();
            }
            ucDepartmentStat.DoCheckNode -= new ucDepartmentStat.CheckHander(ucDepartmentStat_DoCheckNode);
        }
        #endregion

        #region ɾ����ǰListView��ѡ�еĿ���
        /// <summary>
        /// ɾ����ǰListView��ѡ�еĿ���
        /// </summary>
        public void DelDepartment()
        {
            //ȡ�õ�ǰListView��Ҫ�޸ĵĿ���������Ϣ
            TreeNode node = this.neuListView1.SelectedItems[0].Tag as TreeNode;
            //ֻ��ɾ����������
            if (node == null) return;

            //ȡҪɾ���Ŀ�����Ϣ
            FS.HISFC.Models.Base.DepartmentStat dept = node.Tag as FS.HISFC.Models.Base.DepartmentStat;
            if (dept != null)
            {
                if (dept.Childs.Count > 0)
                {
                    MessageBox.Show("�˿������¼����ң�������ɾ����\n����ɾ���¼����ҡ�", "ɾ���޷�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //ȡ�˿����¼���Ա��Ϣ�����������Ա������ɾ����
               FS.HISFC.BizLogic.Manager.UserPowerDetailManager userMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
                ArrayList al = userMgr.LoadUser(dept.StatCode, dept.DeptCode);
                if (al == null)
                {
                    MessageBox.Show("ȡ��Ա��Ϣʱ����:" + userMgr.Err);
                    return;
                }

                if (al.Count > 0)
                {
                    MessageBox.Show("�˿������¼���Ա��������ɾ����\n����ɾ���¼���Ա��", "ɾ���޷�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("ȷ��Ҫ�ѿ��ҡ�" + dept.DeptName + "��ɾ����", "ȷ�Ͽ���ɾ��", MessageBoxButtons.YesNo) == DialogResult.No) return;

                //��������࣬ɾ������

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(deptStatMgr.Connection);
                //trans.BeginTransaction();
                //deptStatMgr.SetTrans(trans.Trans);
                try
                {

                    //ɾ��һ����������
                    int parm = deptStatMgr.Delete(dept.StatCode, dept.DeptCode, dept.PardepCode);
                    if (parm != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();;
                        MessageBox.Show("���ݱ���ʧ��:" + deptStatMgr.Err);
                        return;
                    }
                }
                catch (Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show("���ݱ���ʧ�ܣ�" + e.Message, "��ʾ");
                    return;
                }

                string errInfo = "";
                ArrayList alInfo = new ArrayList();
                alInfo.Add(dept);
                int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DeptStat, ref errInfo);

                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    Function.ShowMessage("���ҽṹɾ��ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                    return;
                }


                FS.FrameWork.Management.PublicTrans.Commit();
                //MessageBox.Show("���ݱ���ʧ�ܣ�" + e.Message,"��ʾ");
            }

            //��TreeView���Ƴ���ɾ���Ľڵ�
            this.tvDepartmentLevelTree1.DelDepartment(node);

            //��ʾ��ǰTreeView��ѡ�нڵ���¼�����
            this.ShowList();
        }
        #endregion
        
        #region �޸Ŀ�������
        /// <summary>
        /// �޸Ŀ�������
        /// </summary>
        public void DeptProperty()
        {
            //�ж��޸ĵĿ����Ƿ������ϵ�
            //��ListView��ѡ����Ŀʱ���鿴��ListView��ѡ����Ŀ������
            FS.HISFC.Models.Base.DepartmentStat dept = null;
            if (this.neuListView1.SelectedItems.Count > 0 && this.neuListView1.Focused)
            {
                //ȡ�õ�ǰListView��Ҫ�޸ĵĿ���������Ϣ
                dept = ((TreeNode)this.neuListView1.SelectedItems[0].Tag).Tag as FS.HISFC.Models.Base.DepartmentStat;
              
            }
            else
            {
                //��ListView��û��ѡ����Ŀʱ�����TreeView��ѡ������Ŀ���Ҳ���ͳ�ƴ��࣬������鿴TreeView�нڵ������
                if (this.tvDepartmentLevelTree1.SelectedNode != null && this.tvDepartmentLevelTree1.SelectedNode.Parent != null)
                {
                    dept = this.tvDepartmentLevelTree1.SelectedNode.Tag as FS.HISFC.Models.Base.DepartmentStat;
                }
            }
            if (dept != null)
            {
                string s=dept.StatCode.ToString();
                ucDepartmentStat deptLevel = new ucDepartmentStat(dept);
                //������ʱ���������޸�����
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��������";
                DialogResult dialogResult=FS.FrameWork.WinForms.Classes.Function.PopShowControl(deptLevel);
                if (dialogResult == DialogResult.OK)
                {
                    this.ShowList();
                }
            }
            
        }

        #endregion

        #region �¼�����������жϼ���Ľڵ� ·־����2007-4-11
        private bool ucDepartmentStat_DoCheckNode(string DeptCode)
        {
            TreeNode node = this.tvDepartmentLevelTree1.SelectedNode;
           //�õ������ҵĸ��ڵ�
            while (true)
            {
                if (node.Parent == null)
                {
                    break;
                }
                else
                {
                    node = node.Parent;
                }
            }
            bl = true;

            if (node.Text == "���Ҳ�����ϵ")
            {
                return true;
            }
            else
            {
                return CheckNode(node, DeptCode);
            }
        }
        /// <summary>
        /// �ݹ���ұ��ڵ��µ����нڵ��Tag�Ƿ���CheckStr
        /// </summary>
        /// <param name="node">����ĸ��ڵ�</param>
        /// /// <param name="CheckStr">Ҫ�Ƚϵ�ֵ</param>
        /// <returns></returns>
        private bool CheckNode(TreeNode node,string CheckStr)
        {
            
            foreach (TreeNode tempNode in node.Nodes)
            {
                if ((tempNode.Tag as FS.HISFC.Models.Base.DepartmentStat).DeptCode == CheckStr)
                {
                    bl=false;
                }

                if (tempNode.Nodes.Count > 0)
                {
                    if (!bl) return false;
                    CheckNode(tempNode, CheckStr);
                }
            }
            return bl;
        }
        #endregion

        #region UCLoad
        /// <summary>
        /// ucLoad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDepartmentLevel_Load(object sender, EventArgs e)
        {
            //{B8E42CCC-11FF-4422-9A9E-18AB421B1297}
            if (this.Tag == null)
            {
                MessageBox.Show("��Ч�Ĵ��ڲ�����");
                return;
            }
            try
            {
                if (this.Tag.ToString() == "")
                {
                    tvDepartmentLevelTree1.BeforeLoad("@@");
                }
                else
                {
                    tvDepartmentLevelTree1.BeforeLoad(this.Tag.ToString());
                }
                
                
            }
            catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        #endregion

        #region ��ʾ��ǰTreeView��ѡ�нڵ���¼�����
        /// <summary>
        /// ��ʾ��ǰTreeView��ѡ�нڵ���¼�����
        /// </summary>
        private void ShowList()
        {
            ShowListDept();//��ʾ����
            ShowListUser();//��ʾ��Ա

            //���ò˵��͹������е���Ŀ�Ƿ����
           this.SetEnable();
       }
        #endregion

        #region �����Ϳؼ���ǰ��ѡ�нڵ�Ķ��ӽڵ㣨���ң���ʾ��listView��
       /// <summary>
        /// �����Ϳؼ���ǰ��ѡ�нڵ�Ķ��ӽڵ㣨���ң���ʾ��listView��
        /// </summary>
        private void ShowListDept()
        {
            this.neuListView1.Items.Clear();
            if (this.tvDepartmentLevelTree1.SelectedNode.Nodes.Count > 0)
            {
                foreach (TreeNode node in this.tvDepartmentLevelTree1.SelectedNode.Nodes)
                {
                    ListViewItem item = this.neuListView1.Items.Add(node.Text);
                    item.Tag = node;
                    //���ݿ��ҽڵ����ͣ���ʾ��ͬ��ͼƬ
                   FS.HISFC.Models.Base.DepartmentStat dept = node.Tag as FS.HISFC.Models.Base.DepartmentStat;
                    if (dept != null)
                    {
                        item.ImageIndex = dept.NodeKind;
                    }
                }
            }
        }
       #endregion


        #region ���ò˵��͹������е���Ŀ�Ƿ����
        /// <summary>
        /// ���ò˵��͹������е���Ŀ�Ƿ����
        /// </summary>
        private void SetEnable()
        {
            //ȡ��ǰTreeView��ѡ�еĽڵ�
            FS.HISFC.Models.Base.DepartmentStat departmentStat = this.tvDepartmentLevelTree1.SelectedNode.Tag as FS.HISFC.Models.Base.DepartmentStat;

            //�����ǰ��TreeViewѡ�еĽڵ㲻��ͳ�ƴ��ࣨһ���ڵ㣩������������ж��Ƿ���������ˣ���������������Ա
            if (departmentStat != null)
            {
                FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                
                #region ���ݴ���Ȩ���������ж��Ƿ����������Ա��·־����2007-6-15��
                int resultValue = deptStatMgr.DepartMentClassCount(departmentStat.StatCode);
                if (resultValue <= 0)
                {
                    this.menuItemAddUser.Enabled = false;
                }
                else
                {
                    this.menuItemAddUser.Enabled = true;
                }
                #endregion

            }
            else
            {
                this.menuItemAddUser.Enabled = false;
            }

            //�����ǰ��ListView��ѡ������Ŀ�������Բ˵�����ã����򲻿���
            if (this.neuListView1.SelectedItems.Count > 0 && this.neuListView1.Focused)
            {
                //��ListView��ѡ����Ŀʱ������鿴������
                this.menuItemProperty.Enabled = true;
                this.toolBarService.SetToolButtonEnabled("����", true);

                //�����ǰ��ListView��ѡ���˿��ң���ɾ���˵�����ã����򲻿��á���Ա�����ڴ˴�ɾ����
                FS.HISFC.Models.Admin.UserPowerDetail userPower = this.neuListView1.SelectedItems[0].Tag as FS.HISFC.Models.Admin.UserPowerDetail;
                if (userPower == null)
                {
                    this.menuItemDelete.Enabled = true;
                    this.toolBarService.SetToolButtonEnabled("ɾ��", true);
                }
                else
                {
                    this.menuItemDelete.Enabled = false;
                    this.toolBarService.SetToolButtonEnabled("ɾ��", false);
                }
            }
            else
            {
                //��ListView��û��ѡ����Ŀʱ�����TreeView��ѡ������Ŀ������鿴TreeView�нڵ������
                if (this.tvDepartmentLevelTree1.SelectedNode != null && this.tvDepartmentLevelTree1.SelectedNode.Parent != null)
                {
                    this.menuItemProperty.Enabled = true;
                    this.toolBarService.SetToolButtonEnabled("����", true);
                }
                else
                {
                    this.menuItemProperty.Enabled = false;
                    this.toolBarService.SetToolButtonEnabled("����", false);
                }
                this.menuItemDelete.Enabled = false;
                this.toolBarService.SetToolButtonEnabled("ɾ��", false);
            }


        }
        #endregion

        /// <summary>
        /// ѡ�����������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDepartmentLevelTree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //��ʾ��ǰTreeView��ѡ�нڵ���¼�����
            this.ShowList();
        }
        
        /// <summary>
        /// �����Ա�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemAddUser_Click(object sender, EventArgs e)
        {   
            //�����Ա
            AddUser();
        }

        /// <summary>
        ///��ӿ����¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemAddDept_Click(object sender, EventArgs e)
        { 
            //��ӿ���
            AddDepartment();
        }

        /// <summary>
        /// ɾ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            //ɾ������
            this.DelDepartment();
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemProperty_Click(object sender, EventArgs e)
        {
            this.Property(true);
        }

        private void neuListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetEnable();
        }

        private void neuContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.SetEnable();
        }

        private void neuListView1_DoubleClick(object sender, EventArgs e)
        {
            this.Property(false);
        }
        
        private void tvDepartmentLevelTree1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode") || e.Data.GetDataPresent("System.Windows.Forms.ListViewItem"))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tvDepartmentLevelTree1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button != MouseButtons.Left || this.tvDepartmentLevelTree1.SelectedNode == null) return;
            FS.HISFC.Models.Base.DepartmentStat dept = new FS.HISFC.Models.Base.DepartmentStat();
            if (dept == null) return;
            //Control�µĿ�ʼ�ϷŲ���
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void tvDepartmentLevelTree1_DragDrop(object sender, DragEventArgs e)
        {   
            /*�ó���α���ע�ͣ�
             dragNodeΪ���϶����ڵ�
             objectNodeΪ��Ŀ�꡿�ڵ�
             * */
            try
            {                
                //��ý���"Drag"�����С��϶����Ľڵ�
                TreeNode dragNode = null;
                ListViewItem item = null;
                
                //�����ڲ��϶�
                dragNode = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
                
                //�Ǵ�ListView�϶�
                if (dragNode == null)
                {
                    item = e.Data.GetData("System.Windows.Forms.ListViewItem") as ListViewItem;
                    dragNode = (item.Tag) as TreeNode;
                }
                if (dragNode != null)
                { 
                    //���ݴ���λ��ת����TreeViewλ��
                    Point position = this.tvDepartmentLevelTree1.PointToClient(new Point(e.X, e.Y));
                    //����TreeViewλ��ȡ��ǰ��Ŀ�꡿�ڵ�
                    TreeNode objectNode = this.tvDepartmentLevelTree1.GetNodeAt(position);
                    
                    //��Ŀ������м����������Ŀ
                    if (objectNode != null)
                    {
                        //����϶����λ�ø��϶�ǰ��λ��û�䣬����ʾ�����϶�
                        if (dragNode.Parent == objectNode)
                        {
                            MessageBox.Show("�޷��ƶ����ң�Դ������Ŀ�������ͬ��", "��ʾ");
                            return;
                        }

                        //���Ŀ��ڵ����϶��ڵ���ӽڵ㣬�������϶�
                        if (objectNode.FullPath.IndexOf(dragNode.FullPath) > 0)
                        {
                            MessageBox.Show("�޷��ƶ����ң����ܽ������ƶ������¼������У�", "��ʾ");
                            return;
                        }

                        //�����϶����ҵĸ������ұ��������
                        FS.HISFC.BizLogic.Manager.DepartmentStatManager deptMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                        FS.HISFC.Models.Base.DepartmentStat parentDept = objectNode.Tag as FS.HISFC.Models.Base.DepartmentStat;
                        FS.HISFC.Models.Base.DepartmentStat dept = dragNode.Tag as FS.HISFC.Models.Base.DepartmentStat;

                        //Ŀ��ڵ��ͳ�ƴ�����룬���ڱȽ��϶��Ŀ����Ƿ���ͬһ��ͳ�ƴ�����
                        string statCode = "";
                    
                        //���Ŀ��ڵ���ͳ�ƴ��ࣨһ���ڵ㣩���򽫴��ึ���϶����ҵĸ���
                        if (parentDept == null)
                        {
                            //ȡĿ��ڵ�ͳ�ƴ���ı���
                            statCode = objectNode.Tag.ToString();

                            //ֻ����ͬһ�����½����϶�
                            if (statCode != dept.StatCode)
                            {
                                MessageBox.Show("�޷��ƶ����ң����ܽ������ƶ�����һ�����ҷ����С�", "��ʾ");
                                return;
                            }
                            if (MessageBox.Show("ȷ��Ҫ�ѿ���" + dept.DeptName + "ת��" + objectNode.Text + "����?", "����ת����ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                dept.PardepCode = "AAAA";
                                dept.PardepName = objectNode.Text;
                            }
                        }
                        else
                        {
                        	//ȡĿ��ڵ��ͳ�ƴ������
							statCode = parentDept.StatCode;

							//ֻ����ͬһ�����½����϶�
							if (statCode != dept.StatCode) 
							{
								MessageBox.Show("�޷��ƶ����ң����ܽ������ƶ�����һ�����ҷ����С�","��ʾ");
								return;
							}

							if (MessageBox.Show("ȷ��Ҫ�ѿ��ҡ�" + dept.DeptName+ "��ת����"+parentDept.DeptName+"������" ,"����ת����ʾ",MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

							//���Ŀ��ڵ��ǿ��ң��򽫿�����Ϣ�����϶����ҵĸ���
							dept.PardepCode = parentDept.DeptCode;
							dept.PardepName = parentDept.DeptName;

						}

						if (deptMgr.UpdateDepartmentStat(dept) != 1) 
						{
							MessageBox.Show("���¿�����Ϣʱ����:" + deptMgr.Err);
							return;
						}
						//ɾ���϶��Ľڵ���ǰ��λ��
						this.tvDepartmentLevelTree1.DelDepartment(dragNode);
						//���µ�λ�ò����϶��Ľڵ�
						this.tvDepartmentLevelTree1.AddDepartment(objectNode,dragNode);
                    }
                                    
                }

            }
            catch
            { 
            }
        }
        
      

    }
}
