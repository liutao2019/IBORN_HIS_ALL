using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace FS.HISFC.Components.Material.Base
{
    /// <summary>
    /// [��������: ���������]
    /// [�� �� ��: ��ά]
    /// [����ʱ��: 2008-03-16]
    /// </summary>
    public partial class tvKindTree1 : Common.Controls.baseTreeView
    {
        public tvKindTree1()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.Material.Baseset matBase = new FS.HISFC.BizLogic.Material.Baseset();

        public tvKindTree1(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            try
            {
                this.InitTreeView();
            }
            catch
            { }
        }

        /// <summary>
        /// ��ʼ��TreeView
        /// </summary>
        public void InitTreeView()
        {
            if (this.DesignMode)
            {
                return;
            }

            this.ImageList = this.groupImageList;
            this.SuspendLayout();
            this.Nodes.Clear();

            TreeNode title = new TreeNode("ȫ����Ŀ��Ϣ", 1, 2);

            this.ImageIndex = 0;
            this.SelectedImageIndex = 0;

            title.Tag = "0";
            this.Nodes.Add(title);

            ArrayList al = new ArrayList();

            try
            {
                //ȡĬ��һ����Ŀ
                al = this.matBase.QueryKindAllByPreID("0");
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
            catch { }
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
                al = this.matBase.QueryKindAllByPreID(preID);
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

                    if (materialKind.EndGrade)
                    {
                        this.InsertNode(kindTree, materialKind.ID);
                    }

                }
            }
            catch { }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public virtual int Reset()
        {
            this.InitTreeView();

            return 1;
        }

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

    }
}
