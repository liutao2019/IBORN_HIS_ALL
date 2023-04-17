using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Material.Base
{
    /// <summary>
    /// [��������: ���ʿ����б���ʾ�ؼ�]
    /// [�� �� ��: ��ά]
    /// [����ʱ��: 2008-03-16]
    /// </summary>
    public partial class tvDeptTree1 : Common.Controls.baseTreeView
    {
        public tvDeptTree1()
        {
            InitializeComponent();
        }

        public tvDeptTree1(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            try
            {
                this.InitTree();
            }
            catch
            { }
        }

        /// <summary>
        /// ��ȡ��������ѡ�еĽڵ�
        /// </summary>
        public List<FS.HISFC.Models.Material.MaterialStorage> SelectNodes
        {
            get
            {
                List<FS.HISFC.Models.Material.MaterialStorage> selectNodes = new List<FS.HISFC.Models.Material.MaterialStorage>();
                foreach (TreeNode node in this.Nodes)
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            selectNodes.Add(childNode.Tag as FS.HISFC.Models.Material.MaterialStorage);
                        }
                    }
                }
                return selectNodes;
            }
        }

        /// <summary>
        /// ���ʲֿ�
        /// </summary>
        private ArrayList deptList;

        /// <summary>
        /// �����б����ݼ���
        /// </summary>
        /// <returns>���ݼ��سɹ�����1 ���ش��ڴ��󷵻�-1</returns>
        protected virtual int InitDept()
        {
            FS.HISFC.BizLogic.Material.Baseset matDeptManagment = new FS.HISFC.BizLogic.Material.Baseset();
            deptList = matDeptManagment.GetStorageInfo("A", "A", "1", "A");
            if (deptList == null)
            {
                MessageBox.Show("��ȡ�������ݷ�������" + matDeptManagment.Err);
                return -1;
            }

            Hashtable hsDeptList = new Hashtable();

            foreach (FS.HISFC.Models.Material.MaterialStorage dept in deptList)
            {
                hsDeptList.Add(dept.ID, dept);
            }

            return 1;
        }

        /// <summary>
        /// ���ݿ������� ���������б�
        /// </summary>
        /// <returns>�ɹ����ط���1 ʧ�ܷ���-1</returns>
        protected virtual int InitTree()
        {
            if (this.DesignMode)
            {
                return 1;
            }
            this.ImageList = this.deptImageList;

            if (this.InitDept() == -1)
            {
                return -1;
            }

            this.SuspendLayout();

            this.Nodes.Clear();

            TreeNode mRootNode = new TreeNode("���ʲֿ�", 0, 0);

            this.Nodes.Add(mRootNode);

            foreach (FS.HISFC.Models.Material.MaterialStorage dept in deptList)
            {
                TreeNode node = new TreeNode(dept.Name);
                node.ImageIndex = 4;
                node.StateImageIndex = 5;

                node.Tag = dept;
                mRootNode.Nodes.Add(node);
            }

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