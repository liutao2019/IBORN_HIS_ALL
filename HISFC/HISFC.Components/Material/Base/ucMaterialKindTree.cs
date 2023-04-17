using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Material.Base
{
    /// <summary>		
    /// ucMaterialKindTree��ժҪ˵����<br></br>
    /// [��������: ������Ϣ��ѯ]<br></br>
    /// [�� �� ��: �]<br></br>
    /// [����ʱ��: 2007-03-28<br></br>
    /// </summary>
    public partial class ucMaterialKindTree : UserControl
    {
        public ucMaterialKindTree()
        {
            InitializeComponent();
        }

        private string filter;

        private string nodeName;

        public string storagecode;

        private FS.HISFC.BizLogic.Material.Baseset matBase = new FS.HISFC.BizLogic.Material.Baseset();

        public string NodeTag
        {
            get
            {
                return this.filter;
            }
            set
            {
                this.filter = value;
            }
        }

        public string NodeName
        {
            get
            {
                return this.nodeName;
            }
            set
            {
                this.nodeName = value;
            }
        }

        public delegate void GetLevelAndKind(object sender, System.Windows.Forms.TreeViewEventArgs e);

        public event GetLevelAndKind GetLak;

        #region ��ʼ�����οؼ�

        /// <summary>
        /// ���TreeView�Ľڵ���Ϣ
        /// </summary>
        /// <param name="preID">�ϼ���Ŀ����</param>
        /// <param name="curNode">�ϼ��ڵ�</param>
        public void InsertNode(System.Windows.Forms.TreeNode node, string preID)
        {
            ArrayList al = new ArrayList();

            try
            {
                //ȡ�ӽڵ���Ϣ
                al = this.matBase.GetMetKindByPreID(preID);
                if (al == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ʿ�����Ϣ��������") + this.matBase.Err);
                    return;
                }

                if (al.Count <= 0)
                {
                    return;
                }

                //����ӽڵ���Ϣ
                foreach (FS.HISFC.Models.Material.MaterialKind materialKind in al)
                {

                    TreeNode kindTree = new TreeNode(materialKind.Name, 2, 1);

                    kindTree.ImageIndex = 0;
                    kindTree.SelectedImageIndex = 0;

                    kindTree.Tag = materialKind.ID;

                    node.Nodes.Add(kindTree);

                    if (!materialKind.EndGrade)
                    {
                        this.InsertNode(kindTree, materialKind.ID);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ӽڵ�ʧ�ܣ�" + e.Message);
                return;
            }
        }

        /// <summary>
        /// ��ʼ��TreeView
        /// </summary>
        public void InitTreeView()
        {
            this.neuTreeView1.ImageList = this.neuTreeView1.groupImageList;

            this.neuTreeView1.Nodes.Clear();
            TreeNode title = new TreeNode("ȫ����Ŀ��Ϣ", 1, 2);
            title.ImageIndex = 4;

            title.Tag = "0";

            //��Ӹ��ڵ�
            this.neuTreeView1.Nodes.Add(title);

            ArrayList al = new ArrayList();

            try
            {
                //ȡĬ��һ����Ŀ
                al = this.matBase.GetMetKindByPreID("0");
                if (al == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ʿ�����Ϣ��������") + this.matBase.Err);
                    return;
                }
                if (al.Count > 0)
                {
                    //����ӽڵ�
                    this.InsertNode(title, "0");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ʼ����Ŀ��ʧ�ܣ�" + e.Message);
                return;
            }

            this.neuTreeView1.ExpandAll();
        }

        #endregion

        private void ucMaterialKindTree_Load(object sender, System.EventArgs e)
        {
            this.InitTreeView();
        }

        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            //���ù�������
            this.filter = e.Node.Tag.ToString();
            this.nodeName = e.Node.Text;

            this.GetLak(sender, e);

        }

    }
}
