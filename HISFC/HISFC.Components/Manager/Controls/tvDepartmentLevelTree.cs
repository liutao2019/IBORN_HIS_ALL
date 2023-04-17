using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FS.HISFC.Models.Base;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ������TreeView��]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��11��27]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class tvDepartmentLevelTree :System.Windows.Forms.TreeView
    {
        #region �ֶ�
        private System.Collections.Hashtable hashTable = new System.Collections.Hashtable();

        /// <summary>
        /// �������Ƽ���  {31FD985A-A165-4812-8CBE-CA2E2C7B9A7B}
        /// </summary>
        private Dictionary<string, string> deptNameDictionary = new Dictionary<string, string>();
        #endregion

        /// <summary>
        /// ���ص�ǰ������
        /// </summary>
        public DepartmentStat departmentStat
        {
            get 
            {
                if (this.SelectedNode.Tag.GetType() == typeof(DepartmentStat))
                {
                    return (DepartmentStat)this.SelectedNode.Tag;
                }
                else
                {
                    
                    return null;
                }
            }
        }

        /// <summary>
        /// ȡȫ�������б�
        /// </summary>
        public void BeforeLoad(string statCode)
        {
            try
            {
                FS.HISFC.BizLogic.Manager.DepartmentStatManager statMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                //�������ҷ���ȼ���������һ���ڵ��б�
                ArrayList depts = statMgr.LoadLevelViewDepartemt(statCode);
                foreach (FS.HISFC.Models.Base.DepartmentStat info in depts)
                {
                    hashTable.Add(info.PkID, info);
                }

                //{31FD985A-A165-4812-8CBE-CA2E2C7B9A7B}  ���ؿ��ұ��롢���ƶ����ֵ�
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptAllUserStopDisuse();
                if (alDept != null)
                {
                    this.deptNameDictionary = new Dictionary<string, string>();
                    foreach (FS.HISFC.Models.Base.Department info in alDept)
                    {
                        this.deptNameDictionary.Add(info.ID, info.Name);
                    }
                }

                //��TreeView����ʾ������Ϣ
                AddView(statCode);
            }
            catch { }
        }


        /// <summary>
        /// ���ݲ����������Ϳؼ�����ʾ��ά���Ŀ��ҷ���Ϳ��ҽڵ�
        /// </summary>
        /// <param name="statCode">����</param>
        private void AddView(string statCode)
        {
            //ȡ��ά���Ĵ��࣬��ʾ�����Ϳؼ��ĸ��ڵ㡣
            FS.HISFC.BizLogic.Manager.PowerLevel1Manager class1 = new FS.HISFC.BizLogic.Manager.PowerLevel1Manager();
            ArrayList al = class1.LoadLevel1Available(statCode);
            if (al.Count == 0) return;

            foreach (FS.HISFC.Models.Admin.PowerLevelClass1 info in al)
            {
                TreeNode node = this.Nodes.Add(info.Name);
                node.Text = info.Class1Name;
                node.ToolTipText = info.Class1Name;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                node.Tag = info.Class1Code;
            }

            //�ҵ�һ���ڵ���������࣬����ӵ������µĽڵ���
            foreach (FS.HISFC.Models.Base.DepartmentStat stat in hashTable.Values)
            {

                TreeNode parentnode = SearchParentNode(stat);

                //{31FD985A-A165-4812-8CBE-CA2E2C7B9A7B}  ���¿�������
                if (this.deptNameDictionary.ContainsKey(stat.DeptCode))
                {
                    stat.DeptName = this.deptNameDictionary[stat.DeptCode];
                }

                TreeNode statNode = new TreeNode(stat.DeptName);
                statNode.Text = stat.DeptName;
                if (stat.NodeKind == 0)
                    statNode.ImageIndex = 1;  //����
                else
                    statNode.ImageIndex = 2; //����

                statNode.SelectedImageIndex = statNode.ImageIndex;
                statNode.Tag = stat;
                parentnode.Nodes.Add(statNode);
                //���ݸ����ڵ�͸������ң������Ϳؼ��еݹ���ʾ���ӽڵ�
                AddStatNode(statNode, stat);
            }
        }


        /// <summary>
        /// ���ݸ����ڵ�͸������ң������Ϳؼ��еݹ���ʾ���ӽڵ�
        /// </summary>
        /// <param name="statNode">�����ڵ�λ��</param>
        /// <param name="stat">��������</param>
        private void AddStatNode(TreeNode statNode, FS.HISFC.Models.Base.DepartmentStat stat)
        {
            if (stat.Childs.Count > 0)
            {
                //�����ж��ӽڵ���ʾ�����Ϳؼ���
                foreach (FS.HISFC.Models.Base.DepartmentStat child in stat.Childs)
                {
                    //{31FD985A-A165-4812-8CBE-CA2E2C7B9A7B}  ���¿�������
                    if (this.deptNameDictionary.ContainsKey(child.DeptCode))
                    {
                        child.DeptName = this.deptNameDictionary[child.DeptCode];
                    }

                    TreeNode node = new TreeNode(child.DeptName);
                    node.Text = child.DeptName;
                    node.ToolTipText = child.DeptName;
                    if (child.NodeKind == 0)
                        node.ImageIndex = 1;  //����
                    else
                        node.ImageIndex = 2; //����
                    node.SelectedImageIndex = node.ImageIndex;

                    node.Tag = child;
                    statNode.Nodes.Add(node);
                    //�����걾�ڵ�󣬼��������ӽڵ�
                    AddStatNode(node, child);
                }
            }
        }


        /// <summary>
        /// ���ݴ���Ŀ���ʵ�壬�ҳ�����������
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        private TreeNode SearchParentNode(FS.HISFC.Models.Base.DepartmentStat stat)
        {
            //��һ���ڵ����ҿ��ҵ���������
            foreach (TreeNode node in this.Nodes)
            {
                if (node.Tag.ToString() == stat.StatCode)
                {
                    return node;
                }
            }

            //�����һ���ڵ����Ҳ������ҵ��������࣬������һ��ͳ�Ʒ���
            TreeNode statnode = new TreeNode(stat.StatCode);
            statnode.Tag = stat.StatCode;
            statnode.Text = stat.StatCode;
            statnode.ToolTipText = stat.StatCode;
            this.Nodes.Add(statnode);
            return statnode;
        }


        /// <summary>
        /// ���ݸ����ڵ�͸������ң������Ϳؼ��в���һ���µ��ӽڵ�
        /// </summary>
        /// <param name="node">�����ڵ�</param>
        /// <param name="dept">��������</param>
        public void AddDepartment(TreeNode node, FS.HISFC.Models.Base.DepartmentStat dept)
        {
            //����ӵĽڵ���ʾ��TreeView��
            TreeNode deptNode = new TreeNode(dept.DeptName);
            deptNode.Text = dept.DeptName;
            if (dept.NodeKind == 0)
                deptNode.ImageIndex = 1;  //����
            else
                deptNode.ImageIndex = 2; //����

            deptNode.SelectedImageIndex = deptNode.ImageIndex;
            deptNode.Tag = dept;
            node.Nodes.Add(deptNode);

            //���˿��Ҹ����丸�����ҵ�Childs���ԡ�
            FS.HISFC.Models.Base.DepartmentStat parentDept = node.Tag as FS.HISFC.Models.Base.DepartmentStat;
            if (parentDept != null)
            {
                parentDept.Childs.Add(dept);
            }

        }


        /// <summary>
        /// Ҫ������ӽڵ�����ڸ����ڵ���
        /// </summary>
        /// <param name="parentNode">�����ڵ�</param>
        /// <param name="node">Ҫ����Ľڵ�</param>
        public void AddDepartment(TreeNode parentNode, TreeNode node)
        {
            //�ڸ����ڵ��в����ӽڵ�
            parentNode.Nodes.Add(node);
            //�������ڵ�ת���ɿ��ҡ�
            FS.HISFC.Models.Base.DepartmentStat parentDept = parentNode.Tag as FS.HISFC.Models.Base.DepartmentStat;
            if (parentDept != null)
            {
                FS.HISFC.Models.Base.DepartmentStat dept = node.Tag as FS.HISFC.Models.Base.DepartmentStat;
                parentDept.Childs.Add(dept);
            }
            //ѡ�е�ǰ�ڵ�
            this.SelectedNode = node;
        }


        /// <summary>
        /// ɾ������Ľڵ�
        /// </summary>
        /// <param name="node">��ɾ���Ľڵ�</param>
        public void DelDepartment(TreeNode node)
        {
            try
            {
                //���˿��Ҵ��丸�����ҵ�Childs������ɾ����
                FS.HISFC.Models.Base.DepartmentStat parentDept = node.Parent.Tag as FS.HISFC.Models.Base.DepartmentStat;
                //���ڵ�ɾ��
                node.Remove();
                if (parentDept != null)
                {
                    //ȡ����������Ϣ
                    FS.HISFC.Models.Base.DepartmentStat dept = node.Tag as FS.HISFC.Models.Base.DepartmentStat;
                    parentDept.Childs.Remove(dept);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        public tvDepartmentLevelTree()
        {
            InitializeComponent();
        }

        public tvDepartmentLevelTree(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}
