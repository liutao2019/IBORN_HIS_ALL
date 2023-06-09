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
        /// 入库计划单据列表
        /// </summary>
        /// <param name="privDept">权限科室</param>
        /// <param name="planState">计划状态</param>
        public void ShowInPlanList(FS.FrameWork.Models.NeuObject privDept, string planState)
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

            ArrayList alList = planManager.QueryInPLanList(privDept.ID, planState);
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show("获取入库计划单列表发生错误" + planManager.Err);
                return;
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("没有入库计划单", 0, 0));
            }
            else
            {
                System.Windows.Forms.TreeNode parentNode = new System.Windows.Forms.TreeNode("入库计划单列表", 0, 0);
                this.Nodes.Add(parentNode);

                System.Windows.Forms.TreeNode node;
                string temp = "";
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    node = new System.Windows.Forms.TreeNode();

                    if (planState == "0" || planState == "1" || planState == "2")
                        node.Text = info.ID + "  【计划单】";
                    else
                        node.Text = info.ID + "  【采购单】";

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
        /// 采购计划单据列表
        /// </summary>
        /// <param name="privDept">权限科室</param>
        /// <param name="planState">计划状态</param>
        public void ShowBuyPlanList(FS.FrameWork.Models.NeuObject privDept, string planState)
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

            ArrayList alList = planManager.QueryInPLanList(privDept.ID, planState);
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show("获取采购计划单列表发生错误" + planManager.Err);
                return;
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("没有采购计划单", 0, 0));
            }
            else
            {
                System.Windows.Forms.TreeNode parentNode = new System.Windows.Forms.TreeNode("采购计划单列表", 0, 0);
                this.Nodes.Add(parentNode);

                System.Windows.Forms.TreeNode node;
                string temp = "";
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    node = new System.Windows.Forms.TreeNode();

                    if (planState == "3" || planState == "4" || planState == "5")
                        node.Text = info.ID + "  【采购单】";
                    else
                        node.Text = info.ID + "  【采购单】";

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
        /// 采购计划单据列表
        /// </summary>
        /// <param name="privDept">权限科室</param>
        /// <param name="stockState">采购状态</param>
        public void ShowStockPlanList(FS.FrameWork.Models.NeuObject privDept, string stockState)
        {
            //清空列表
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Material.Plan planManger = new FS.HISFC.BizLogic.Material.Plan();

            //显示采购单列表  0 状态为入库计划单
            ArrayList alList = planManger.QueryStockPLanCompanayList(privDept.ID, stockState);
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show("获取采购计划单列表发生错误" + planManger.Err);
                return;
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("没有采购计划单", 0, 0));
            }
            else
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("采购计划单列表", 0, 0));
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();

                    node.Text = "【" + info.ID + "】" + info.User01;//【入库计划单号】+供货公司名称
                    node.SelectedImageIndex = node.ImageIndex;

                    //初始入库计划制定时指定供货公司编码为'0000000000'
                    if (info.Name == null || info.Name.Trim() == "")
                    {
                        info.Name = "0000000000";
                    }

                    node.Tag = info;   //入库计划单号+供货公司ID

                    this.Nodes[0].Nodes.Add(node);
                }
                this.Nodes[0].ExpandAll();
                this.SelectedNode = this.Nodes[0];
            }
        }

        /// <summary>
        /// 入库申请单据列表
        /// </summary>
        /// <param name="privDept">权限科室</param>
        public void ShowApplyList(FS.FrameWork.Models.NeuObject privDept, DateTime dateBegin, DateTime dateEnd)
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

            ArrayList alList = storeManager.QueryApplySimpleForPlan(privDept.ID, dateBegin, dateEnd);

            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show("获取申请单列表发生错误" + storeManager.Err);
                return;
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("没有入库申请单", 0, 0));
            }
            else
            {
                System.Windows.Forms.TreeNode parentNode = new System.Windows.Forms.TreeNode("申请单列表", 0, 0);
                this.Nodes.Add(parentNode);

                System.Windows.Forms.TreeNode node;
                string temp = "";
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    node = new System.Windows.Forms.TreeNode();

                    node.Text = info.ID + "  【" + info.Name + " 申请单】";

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
