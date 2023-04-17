using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������:  ҩƷ�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <˵��
    ///		
    ///  />
    /// </summary>
    public partial class tvDrugCheckTree : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvDrugCheckTree()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.InitTree();
            }
        }

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        /// <summary>
        /// ����������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ������
        /// </summary>
        protected virtual void InitTree()
        {
            this.ImageList = this.groupImageList;

            this.Nodes.Clear();

            ArrayList alType = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);

            TreeNode root = new TreeNode("ȫ��ҩƷ��Ϣ", 0, 0);
            root.Tag = "1=1";

            this.Nodes.Add(root);

            foreach (FS.FrameWork.Models.NeuObject objType in alType) //ҩƷ����
            {
                TreeNode typeNode = new TreeNode(objType.Name, 1, 1);
                typeNode.Tag = "ҩƷ���� = '" + objType.Name.ToString() + "'";
                root.Nodes.Add(typeNode);

                AddNode(objType, typeNode);
            }
            //չ�����ڵ�
            root.Expand();
        }

        /// <summary>
        /// ���ݲ�ͬҩƷ�����������ҩƷ��
        /// </summary>
        /// <param name="drugType">ҩƷ���</param>
        /// <param name="rootNode">���ڵ�</param>
        private void AddNode(FS.FrameWork.Models.NeuObject drugType, TreeNode rootNode)
        {
            //��ȡҩƷ���Ͷ�Ӧ��ҩƷ���б�����ʾ�����������ҩƷ
            List<FS.HISFC.Models.Pharmacy.Item> checkItemList = this.itemManager.QueryItemListForCheck(drugType.ID.ToString());
            if (checkItemList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.Item item in checkItemList)
                {
                    TreeNode tn = new TreeNode();
                    if (this.itemManager.QueryValidDrugByCustomCode(item.NameCollection.UserCode).Count > 0)
                    {
                        tn.Text = "@" + item.Name;//�Ѿ�������Ч��¼����ͨ����ˣ���������
                    }
                    else
                    {
                        tn.Text = item.Name;
                    }

                    tn.ImageIndex = 2;
                    tn.SelectedImageIndex = 4;

                    tn.Tag = item;
                    rootNode.Nodes.Add(tn);
                }
                rootNode.Expand();
            }
            else
            {
                MessageBox.Show("����ҩƷ�����ش����ҩƷ��������" + this.itemManager.Err);
            }
        }

    }
}
