using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Material.Check
{
    ///{83246F26-E161-4f5b-BC3A-ECBED5B1B3A8}
    /// <summary>
    /// [���������������̵�����]
    /// [�� �� �ߣ�Ѧ�Ľ�]
    /// [����ʱ�䣺2008-08-21]
    /// </summary>
    public partial class ucTypeOrQualityChoose : UserControl
    {
        public ucTypeOrQualityChoose()
        {
            InitializeComponent();
        }

        public ucTypeOrQualityChoose(bool isKindType): this()
        {
            this.IsKindType = isKindType;

            this.InitTreeView();
        }

        #region �����

        /// <summary>
        /// ѡ���ҩƷ���/ҩƷ����
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> kindList = new List<FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// �Ƿ���ʾ���ʿ�Ŀ���
        /// </summary>
        private bool IsKindType = true;

        /// <summary>
        /// ������� 0 ȡ�� 1 ȷ�� 2 ȫ����Ŀ
        /// </summary>
        private string resultFlag = "1";

        /// <summary>
        /// ѡ������ʿ�Ŀ���
        /// </summary>
        private string kindType = "";

        /// <summary>
        /// ���ʻ���������
        /// </summary>
        FS.HISFC.BizLogic.Material.Baseset matBase = new FS.HISFC.BizLogic.Material.Baseset();

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        public string ResultFlag
        {
            get
            {
                return this.resultFlag;
            }
            set
            {
                this.resultFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ�Կ��Ϊ������ʽ��з��ʴ���
        /// </summary>
        public bool IsCheckZeroStock
        {
            get
            {
                return this.ckZeroState.Checked;
            }
        }

        /// <summary>
        /// �Ƿ��ͣ������(���ⷿͣ��)���з��ʴ���
        /// </summary>
        public bool IsCheckStopMaterial
        {
            get
            {
                return this.ckValidDrug.Checked;
            }
        }

        /// <summary>
        /// ѡ������ʿ�Ŀ���
        /// </summary>
        public string KindType
        {
            get
            {
                return this.kindType;
            }
            set
            {
                this.kindType = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��TreeView
        /// </summary>
        public void InitTreeView()
        {
            this.tvObject.Nodes.Clear();

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
                    foreach (FS.HISFC.Models.Material.MaterialKind title in al)
                    {
                        TreeNode kindTree = new TreeNode(title.Name, 0, 0);
                        kindTree.Tag = "";
                        this.tvObject.Nodes.Add(kindTree);
                        //����ӽڵ�
                        this.InsertNode(kindTree, title.ID);
                    }
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ʼ����Ŀ��ʧ�ܣ�" + e.Message);
                return;
            }
            this.tvObject.ExpandAll();
        }

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

                    TreeNode kindTree = new TreeNode(materialKind.Name,0,0);
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
        /// ��treeview��ѡ�е����ݱ��浽������
        /// </summary>
        public void Save()
        {
            //��������е����ݡ�
            this.kindList.Clear();

            if (this.tvObject.Nodes.Count == 0)
                return;

            foreach (TreeNode node in this.tvObject.Nodes)
            {
                foreach (TreeNode tn in node.Nodes)
                {
                    //��ѡ�е���浽������
                    if (tn.Checked) this.kindList.Add(tn.Tag as FS.FrameWork.Models.NeuObject);
                }
            }
        }

        /// <summary>
        /// ��ҩƷ������ҩƷ����ѡ�񷵻��ַ���
        /// </summary>
        public void SaveForKind()
        {
            //�������
            this.kindType = "AAAA";
            if (this.tvObject.Nodes.Count == 0)
            {
                return;
            }

            this.TraversTreeView(this.tvObject);
            //foreach (TreeNode node in this.tvObject.Nodes)
            //{
            //    if (node.Checked)
            //    {
            //        if (this.kindType == "AAAA")
            //            this.kindType = "";
            //        this.kindType += node.Tag.ToString() + "','";
            //    }
            //}

        }

        /// <summary>
        /// �������ڵ�
        /// </summary>
        /// <param name="treeView"></param>
        public void TraversTreeView(FS.FrameWork.WinForms.Controls.NeuTreeView treeView)
        {
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                this.TraversTreeNode(n);
            }
        }

        /// <summary>
        /// �����ӽ��
        /// </summary>
        /// <param name="treeNode"></param>
        public void TraversTreeNode(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Checked)
                {
                    if (this.kindType == "AAAA")
                        this.kindType = "";
                    this.kindType += tn.Tag.ToString() + "','";
                }
                this.TraversTreeNode(tn);
            }
        }

        /// <summary>
        /// �ر�
        /// </summary>
        public void Close()
        {
            if (this.ParentForm != null)
                this.ParentForm.Close();
        }

        #endregion

        #region �¼�

        private void tvObject_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //���ѡ�е��Ǹ��ڵ㣬��ѡ���������ӽڵ�
            if (e.Node.Nodes!= null)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    if (node.Checked != e.Node.Checked) node.Checked = e.Node.Checked;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.IsKindType)
            {
                this.SaveForKind();
                this.resultFlag = "1";
            }
            else
            {
                this.Save();
                this.resultFlag = "1";
            }

            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.resultFlag = "0";

            this.Close();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.resultFlag = "2";

            this.Close();
        }

        #endregion
        
    }
}
