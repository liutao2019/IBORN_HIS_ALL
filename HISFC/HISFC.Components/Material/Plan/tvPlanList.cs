using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.Material.Plan
{
    public partial class tvPlanList : FS.FrameWork.WinForms.Controls.NeuTreeView
    {
        public tvPlanList()
        {
            InitializeComponent();
        }

        public tvPlanList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// ���ƻ������б�
        /// </summary>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <param name="planState">�ƻ�״̬</param>
        public void ShowInPlanList(FS.FrameWork.Models.NeuObject privDept, string planState)
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

            ArrayList alList = planManager.QueryInPLanList(privDept.ID, planState);
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ���ƻ����б�������" + planManager.Err);
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

                    if (planState == "0" || planState == "1" || planState == "2")
                        node.Text = info.ID + "  ���ƻ�����";
                    else
                        node.Text = info.ID + "  ���ɹ�����";

                    info.Memo = planState;

                    node.ImageIndex = 0;
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
        /// <param name="planState">�ƻ�״̬</param>
        public void ShowBuyPlanList(FS.FrameWork.Models.NeuObject privDept, string planState)
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

            ArrayList alList = planManager.QueryInPLanList(privDept.ID, planState);
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ�ɹ��ƻ����б�������" + planManager.Err);
                return;
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("û�вɹ��ƻ���", 0, 0));
            }
            else
            {
                System.Windows.Forms.TreeNode parentNode = new System.Windows.Forms.TreeNode("�ɹ��ƻ����б�", 0, 0);
                this.Nodes.Add(parentNode);

                System.Windows.Forms.TreeNode node;
                string temp = "";
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    node = new System.Windows.Forms.TreeNode();

                    if (planState == "3" || planState == "4" || planState == "5")
                        node.Text = info.ID + "  ���ɹ�����";
                    else
                        node.Text = info.ID + "  ���ɹ�����";

                    info.Memo = planState;

                    node.ImageIndex = 0;
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

            FS.HISFC.BizLogic.Material.Plan planManger = new FS.HISFC.BizLogic.Material.Plan();

            //��ʾ�ɹ����б�  0 ״̬Ϊ���ƻ���
            ArrayList alList = planManger.QueryStockPLanCompanayList(privDept.ID, stockState);
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ�ɹ��ƻ����б�������" + planManger.Err);
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

                    node.Text = "��" + info.ID + "��" + info.User01;//�����ƻ����š�+������˾����
                    node.SelectedImageIndex = node.ImageIndex;

                    //��ʼ���ƻ��ƶ�ʱָ��������˾����Ϊ'0000000000'
                    if (info.Name == null || info.Name.Trim() == "")
                    {
                        info.Name = "0000000000";
                    }

                    node.Tag = info;   //���ƻ�����+������˾ID

                    this.Nodes[0].Nodes.Add(node);
                }
                this.Nodes[0].ExpandAll();
                this.SelectedNode = this.Nodes[0];
            }
        }

        /// <summary>
        /// ������뵥���б�
        /// </summary>
        /// <param name="privDept">Ȩ�޿���</param>
        public void ShowApplyList(FS.FrameWork.Models.NeuObject privDept, DateTime dateBegin, DateTime dateEnd)
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

            ArrayList alList = storeManager.QueryApplySimpleForPlan(privDept.ID, dateBegin, dateEnd);

            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ���뵥�б�������" + storeManager.Err);
                return;
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("û��������뵥", 0, 0));
            }
            else
            {
                System.Windows.Forms.TreeNode parentNode = new System.Windows.Forms.TreeNode("���뵥�б�", 0, 0);
                this.Nodes.Add(parentNode);

                System.Windows.Forms.TreeNode node;
                string temp = "";
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    node = new System.Windows.Forms.TreeNode();

                    node.Text = info.ID + "  ��" + info.Name + " ���뵥��";

                    node.ImageIndex = 0;
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

    }
}
