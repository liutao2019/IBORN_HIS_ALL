using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Plan
{
    /// <summary>
    /// [��������: ���/�ɹ��ƻ����б�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class tvPlanList : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvPlanList()
        {
            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        public tvPlanList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        /// <summary>
        /// ���ƻ������б�
        /// </summary>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <param name="planState">�ƻ�״̬</param>
        public void ShowInPlanList(FS.FrameWork.Models.NeuObject privDept, string planState)
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alList = itemManager.QueryInPLanList(privDept.ID, "0");
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ���ƻ����б�������" + itemManager.Err));
                return;
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("û�����ƻ���", 0, 0));
            }
            else
            {
                System.Windows.Forms.TreeNode parentNode = new System.Windows.Forms.TreeNode("���ƻ����б�", 0, 0);
                this.Nodes.Add(parentNode);

                System.Windows.Forms.TreeNode node;
                string temp = "";
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    node = new System.Windows.Forms.TreeNode();

                    if (planState == "0")
                        node.Text = info.ID + "  ���ƻ�����";
                    else
                        node.Text = info.ID + "  ���ɹ�����";

                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 4;

                    info.Memo = planState;

                    node.Tag = info;

                    if (temp != info.ID)
                    {
                        parentNode.Nodes.Add(node);
                    }

                    temp = info.ID;
                }
                this.Nodes[0].ExpandAll();
                this.SelectedNode = this.Nodes[0];
            }
        }

        /// <summary>
        /// �ɹ��ƻ������б�
        /// </summary>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <param name="stockState">�ɹ�״̬</param>
        public void ShowStockPlanList(FS.FrameWork.Models.NeuObject privDept, string stockState)
        {
            //����б�
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            //��ʾ�ɹ����б�  0 ״̬Ϊ���ƻ���
            ArrayList alList = itemManager.QueryStockPLanCompanayList(privDept.ID, stockState);
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ�ɹ��ƻ����б�������" + itemManager.Err));
                return;
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("û�вɹ��ƻ���", 0, 0));
            }
            else
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("�ɹ��ƻ����б�", 0, 0));
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();

                    node.Text = "��" + info.ID + "��" + info.Name;//�����ƻ����š�+������˾����
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 4;

                    //��ʼ���ƻ��ƶ�ʱָ��������˾����Ϊ'0000000000'
                    if (info.User01 == null || info.User01.Trim() == "")
                    {
                        info.User01 = "0000000000";
                    }

                    node.Tag = info;   //���ƻ�����+������˾ID

                    this.Nodes[0].Nodes.Add(node);
                }
                this.Nodes[0].ExpandAll();
                this.SelectedNode = this.Nodes[0];
            }
        }
    }
}
