using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Base
{
    /// <summary>
    /// [功能描述: 住院药房摆药通知树]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、优先方式是为了方便药房摆药分工：有按照科室分工的，也有按照口服、针剂分工的
    /// 2、不要在此添加业务逻辑
    /// </summary>
    public partial class tvMessageBaseTree : FS.SOC.HISFC.Components.Common.Base.baseTreeView
    {
        public tvMessageBaseTree()
        {
            InitializeComponent();
            this.ImageList = this.deptImageList;
        }

        public tvMessageBaseTree(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.ImageList = this.deptImageList;
        }

        public void SelectDefaultNode(string deptNo)
        {
            TreeNode node = this.FindDeptNode(deptNo);
            if (node != null)
            {
                node.ExpandAll();
            }
        }

        //{CC289C41-7C69-42e2-9945-972F3EFAB663}
        public void SelectDefaultNodeEx(string deptNo)
        {
            TreeNode node = this.FindDeptNodeEx(deptNo);
            if (node != null)
            {
                node.ExpandAll();
            }
        }

        #region 配药时显示

        /// <summary>
        /// 显示摆药通知
        /// 优先方式是为了方便药房摆药分工
        /// </summary>
        /// <param name="listDrugMessage">摆药通知数组</param>
        /// <param name="drugControl">摆药台</param>
        /// <param name="isDrugClassBillFirst">是否摆药单优先</param>
        /// <param name="isExpand">是否展开节点</param>
        public void ShowDrugMessage(ArrayList alDrugMessage, FS.HISFC.Models.Pharmacy.DrugControl drugControl, bool isDrugClassBillFirst, ArrayList alInpatient, bool isExpand, bool isCheckConcentratedSendInfo)
        {
            if (alInpatient == null)
            {
                this.ShowDrugMessage(alDrugMessage, drugControl, isDrugClassBillFirst, isCheckConcentratedSendInfo);
            }
            else 
            {
                this.ShowDrugMessageWithPatient(alDrugMessage, drugControl, isDrugClassBillFirst, alInpatient, isCheckConcentratedSendInfo);
            }

            if (isExpand)
            {
                this.ExpandAll();
            }
        }
        private void ShowDrugMessageWithPatient(ArrayList alDrugMessage, FS.HISFC.Models.Pharmacy.DrugControl drugControl, bool isDrugClassBillFirst, ArrayList alInpatient,bool isCheckConcentratedSendInfo)
        {
            //drugControl.ShowLevel： 0科室汇总 1科室明细 2患者明细摆药单优先 3患者明细 患者优先
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage patient in alInpatient)
            {
                FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = this.GetDrugMessage(patient.DrugBillClass.ID, patient.ApplyDept.ID,patient.SendType, alDrugMessage);
                if (drugMessage == null)
                {
                    //数据不一致，摆药通知形成后作废申请可能会产生这样的问题
                    continue;
                }
                //取得科室名称，显示科室名称用，甚至可以传递给打印，在打印接口中不必再获取申请科室名称
                drugMessage.ApplyDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugMessage.ApplyDept.ID);

                if (isDrugClassBillFirst)
                {
                    //第一级节点摆药单分类
                    TreeNode node1 = this.FindBillTypeNode(drugMessage.DrugBillClass.ID, drugMessage.StockDept.ID, drugMessage.SendType,drugMessage.User02);
                    node1.Tag = drugMessage;
                    node1.Text =  Common.Function.GetDrugBillClassName(drugMessage);
                    if (isCheckConcentratedSendInfo)
                    {
                        node1.Text =  Common.Function.GetDrugBillClassName(drugMessage) + "【" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugApplySendTypeName(drugMessage.SendType) + "】";
                        if (node1.Text.Contains("紧急"))
                        {
                            node1.BackColor = System.Drawing.Color.Green;
                        }
                    }

                    //第二级节点科室
                    if (drugControl.ShowLevel > 0)
                    {
                        TreeNode node2 = this.FindDeptNode(drugMessage.ApplyDept.ID, node1);
                        node2.Tag = drugMessage;
                        node2.Text = drugMessage.ApplyDept.Name;

                        //第三级节点患者
                        if (drugControl.ShowLevel > 1)
                        {
                            TreeNode node3 = this.FindPatientNode(patient.User01, node2);
                            string bedNO = patient.Memo;
                            if (bedNO.Length > 4)
                            {
                                bedNO = bedNO.Substring(4);
                            }

                            node3.Text = "【" + bedNO + "床】" + patient.Name;
                            node3.ImageIndex = 6;
                            node3.SelectedImageIndex = 6;
                            node3.Tag = patient;
                            //node2.Nodes.Add(node3);
                        }
                    }

                  
                }
                else
                {
                    //第一级节点科室
                    TreeNode node1 = this.FindDeptNode(drugMessage.ApplyDept.ID);
                    node1.Tag = drugMessage;
                    node1.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugMessage.ApplyDept.ID);

                    //第二级节点摆药单分类
                    if (drugControl.ShowLevel == 2 || drugControl.ShowLevel == 1)
                    {
                        TreeNode node2 = this.FindBillTypeNode(drugMessage.DrugBillClass.ID,drugMessage.StockDept.ID, drugMessage.SendType, drugMessage.User02,node1);
                        node2.Tag = drugMessage;
                        node2.Text =  Common.Function.GetDrugBillClassName(drugMessage);
                        if (isCheckConcentratedSendInfo)
                        {
                            node2.Text =  Common.Function.GetDrugBillClassName(drugMessage) + "【" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugApplySendTypeName(drugMessage.SendType) + "】";
                            if (node2.Text.Contains("紧急"))
                            {
                                node2.BackColor = System.Drawing.Color.Green;
                            }
                        }

                        //第三级节点患者
                        if (drugControl.ShowLevel >1)
                        {
                            TreeNode node3 = this.FindPatientNode(patient.User01, node2);
                            string bedNO = patient.Memo;
                            if (bedNO.Length > 4)
                            {
                                bedNO = bedNO.Substring(4);
                            }

                            node3.Text = "【" + bedNO + "床】" + patient.Name;
                            node3.ImageIndex = 6;
                            node3.SelectedImageIndex = 6;
                            node3.Tag = patient;
                            //node2.Nodes.Add(node3);
                        }
                    }
                    else if (drugControl.ShowLevel == 3)
                    {
                        //第二级节点患者
                        TreeNode node2 = this.FindPatientNode(patient.User01, node1);
                        string bedNO = patient.Memo;
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }

                        node2.Text = "【" + bedNO + "床】" + patient.Name;
                        node2.ImageIndex = 6;
                        node2.SelectedImageIndex = 6;
                        node2.Tag = patient;
                        //node1.Nodes.Add(node2);

                        //第三级节点摆药单
                        TreeNode node3 = this.FindBillTypeNode(drugMessage.DrugBillClass.ID,drugMessage.StockDept.ID,drugMessage.SendType,drugMessage.User02, node2);
                        node3.Tag = drugMessage;
                        node3.Text =  Common.Function.GetDrugBillClassName(drugMessage);
                        if (isCheckConcentratedSendInfo)
                        {
                            node3.Text =  Common.Function.GetDrugBillClassName(drugMessage) + "【" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugApplySendTypeName(drugMessage.SendType) + "】";
                            if (node3.Text.Contains("紧急"))
                            {
                                node3.BackColor = System.Drawing.Color.Green;
                            }
                        }

                    }
                }
            }

            
        }

        private void ShowDrugMessage(ArrayList alDrugMessage, FS.HISFC.Models.Pharmacy.DrugControl drugControl, bool isDrugClassBillFirst, bool isCheckConcentratedSendInfo)
        {
            //drugControl.ShowLevel： 0科室汇总 1科室明细 2患者明细摆药单优先 3患者明细 患者优先
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage drugMessage in alDrugMessage)
            {
                //取得科室名称，显示科室名称用，甚至可以传递给打印，在打印接口中不必再获取申请科室名称
                drugMessage.ApplyDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugMessage.ApplyDept.ID);

                if (isDrugClassBillFirst)
                {
                    //第一级节点摆药单分类
                    TreeNode node1 = this.FindBillTypeNode(drugMessage.DrugBillClass.ID,drugMessage.StockDept.ID,drugMessage.SendType,drugMessage.User02);
                    node1.Tag = drugMessage;
                    node1.Text =  Common.Function.GetDrugBillClassName(drugMessage);
                    if (isCheckConcentratedSendInfo)
                    {
                        node1.Text =  Common.Function.GetDrugBillClassName(drugMessage) + "【" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugApplySendTypeName(drugMessage.SendType) + "】";
                        if (node1.Text.Contains("紧急"))
                        {
                            node1.BackColor = System.Drawing.Color.Green;
                        }
                    }
                    //第二级节点科室
                    if (drugControl.ShowLevel > 0)
                    {
                        TreeNode node2 = this.FindDeptNode(drugMessage.ApplyDept.ID, node1);
                        node2.Tag = drugMessage;
                        node2.Text = drugMessage.ApplyDept.Name;

                        //第三级节点患者
                        if (drugControl.ShowLevel > 1)
                        {
                           
                        }
                    }                   
                }
                else
                {
                    //第一级节点科室
                    TreeNode node1 = this.FindDeptNode(drugMessage.ApplyDept.ID);
                    node1.Tag = drugMessage;
                    node1.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugMessage.ApplyDept.ID);

                    //第二级节点摆药单分类
                    if (drugControl.ShowLevel == 2 || drugControl.ShowLevel == 1)
                    {
                        TreeNode node2 = this.FindBillTypeNode(drugMessage.DrugBillClass.ID,drugMessage.StockDept.ID,drugMessage.SendType,drugMessage.User02, node1);
                        node2.Tag = drugMessage;
                        node2.Text = Common.Function.GetDrugBillClassName(drugMessage);

                        if (isCheckConcentratedSendInfo)
                        {
                            node2.Text =  Common.Function.GetDrugBillClassName(drugMessage) + "【" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugApplySendTypeName(drugMessage.SendType) + "】";
                            if (node2.Text.Contains("紧急"))
                            {
                                node2.BackColor = System.Drawing.Color.Green;
                            }
                        }

                        //第三级节点患者
                        if (drugControl.ShowLevel == 2)
                        {

                        }
                    }
                    else if (drugControl.ShowLevel == 3)
                    {
                        //第二级节点患者

                        //第三级节点摆药单
                    }
                }
            }

            
        }
        private FS.HISFC.Models.Pharmacy.DrugMessage GetDrugMessage(string drugBillClassNO, string applyDeptNO, int sendType, ArrayList alDrugMessage)
        {
            if (alDrugMessage == null)
            {
                return null;
            }
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage drugMessage in alDrugMessage)
            {
                if (drugMessage.DrugBillClass.ID == drugBillClassNO && drugMessage.ApplyDept.ID == applyDeptNO && drugMessage.SendType == sendType)
                {
                    return drugMessage;
                }
            }
            return null;
        }

        /// <summary>
        /// 从父节点的子节点中获取摆药通知信息
        /// </summary>
        /// <param name="nodeParent">父节点</param>
        /// <returns>alDrugMessage实体</returns>
        public ArrayList GetDrugMessageList(TreeNode nodeParent)
        {
            ArrayList alDrugMessage = new ArrayList();
            if (nodeParent == null)
            {
                return alDrugMessage;
            }
            foreach (TreeNode node in nodeParent.Nodes)
            {
                if (node.Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                {
                    alDrugMessage.Add(node.Tag);
                }
            }
            return alDrugMessage;
        }

        /// <summary>
        /// 从父节点的子节点中获取摆药通知信息
        /// </summary>
        /// <param name="nodeParent">父节点</param>
        /// <returns>alDrugMessage实体</returns>
        public ArrayList GetDrugBill(TreeNode nodeParent)
        {
            ArrayList alDrugBill = new ArrayList();
            if (nodeParent == null)
            {
                return alDrugBill;
            }
            foreach (TreeNode node in nodeParent.Nodes)
            {
                if (node.Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                {
                    alDrugBill.Add(node.Tag);
                }
            }
            return alDrugBill;
        }
       
        #endregion

        #region 节点查找

        /// <summary>
        /// 查找科室为基础的树节点
        /// 提供给以科室优先显示的设置方式
        /// 没找到则新建node
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <returns>树节点</returns>
        private TreeNode FindDeptNode(string deptNO)
        {
            foreach (TreeNode node in this.Nodes)
            {
                if (node.Tag != null)
                {
                    if (node.Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                    {
                        //摆药单保存前是DrugMessage
                        FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                        if (drugMessage.ApplyDept.ID == deptNO)
                        {
                            return node;
                        }
                    }
                    else if (node.Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                    {
                        //摆药单补打到时候实体是DrugBillClass
                        FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = node.Tag as FS.HISFC.Models.Pharmacy.DrugBillClass;
                        if (drugBillClass.ApplyDept.ID == deptNO)
                        {
                            return node;
                        }
                    }
                }
            }

            TreeNode nodeDept = new TreeNode();
            nodeDept.ImageIndex = 0;
            nodeDept.SelectedImageIndex = 1;
            this.Nodes.Add(nodeDept);
            return nodeDept;
        }

         /// <summary>
        /// 查找患者为基础的树节点
        /// 没找到则新建node
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <returns>树节点</returns>
        private TreeNode FindPatientNode(string patientNO, TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Tag != null)
                {
                    if (node.Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                    {
                        //摆药单保存前是DrugMessage
                        FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                        if (drugMessage.User01 == patientNO)
                        {
                            return node;
                        }
                    }

                }
            }

            TreeNode nodePatient = new TreeNode();
            nodePatient.ImageIndex = 6;
            nodePatient.SelectedImageIndex = 6;
            parentNode.Nodes.Add(nodePatient);
            return nodePatient;
        }

        /// <summary>
        /// 查找科室为基础的树节点
        /// 提供给以科室优先显示的设置方式
        /// 没找到则新建node
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="parentNode">树中的父节点</param>
        /// <returns>树节点</returns>
        private TreeNode FindDeptNode(string deptNO, TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Tag != null)
                {
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                    if (drugMessage.ApplyDept.ID == deptNO)
                    {
                        return node;
                    }
                }
            }

            TreeNode nodeDept = new TreeNode();
            nodeDept.ImageIndex = 0;
            nodeDept.SelectedImageIndex = 1;
            parentNode.Nodes.Add(nodeDept);
            return nodeDept;
        }


        //{CC289C41-7C69-42e2-9945-972F3EFAB663}
        private TreeNode FindDeptNodeEx(string deptNO)
        {
            foreach (TreeNode node in this.Nodes)
            {
                if (node.Tag != null)
                {
                    if (node.Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                    {
                        //摆药单保存前是DrugMessage
                        FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                        if (drugMessage.ApplyDept.ID == deptNO)
                        {
                            return node;
                        }
                    }
                    else if (node.Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                    {
                        //摆药单补打到时候实体是DrugBillClass
                        FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = node.Tag as FS.HISFC.Models.Pharmacy.DrugBillClass;
                        if (drugBillClass.ApplyDept.ID == deptNO)
                        {
                            return node;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 查找摆药单为基础的树节点
        /// 提供给以摆药单优先显示的设置方式
        /// 没找到则新建node
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <returns>树节点</returns>
        private TreeNode FindBillTypeNode(string billClassNO, string stockDeptNO, int sendType, string user02)
        {
            foreach (TreeNode node in this.Nodes)
            {
                if (node.Tag != null)
                {
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                    if (drugMessage.DrugBillClass.ID == billClassNO && drugMessage.StockDept.ID == stockDeptNO && drugMessage.SendType == sendType && user02 == drugMessage.User02)
                    {
                        return node;
                    }
                }
            }

            TreeNode nodeBillClass = new TreeNode();
            //图片
            nodeBillClass.ImageIndex = 11;
            nodeBillClass.SelectedImageIndex = 5;
            this.Nodes.Add(nodeBillClass);
            return nodeBillClass;
        }

        /// <summary>
        /// 查找科室为基础的树节点
        /// 提供给以科室优先显示的设置方式
        /// 没找到则新建node
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="parentNode">树中的父节点</param>
        /// <returns>树节点</returns>
        private TreeNode FindBillTypeNode(string billClassNO, string stockDeptNO, int sendType,string user02, TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Tag != null)
                {
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                    if (drugMessage.DrugBillClass.ID == billClassNO && drugMessage.StockDept.ID == stockDeptNO && drugMessage.SendType == sendType && user02 == drugMessage.User02)
                    {
                        return node;
                    }
                }
            }

            TreeNode nodeBillClass = new TreeNode();
            //图片
            nodeBillClass.ImageIndex = 11;
            nodeBillClass.SelectedImageIndex = 5;
            parentNode.Nodes.Add(nodeBillClass);
            return nodeBillClass;
        }

        /// <summary>
        /// 查找患者节点
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="parentNode">父节点：在该节点中查找</param>
        /// <returns>null 没有找到</returns>
        private TreeNode FindPatientTypeNode(string inpatientNO, TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Tag != null)
                {
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                    if (drugMessage.ID == inpatientNO)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据住院流水号查找患者节点
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        public void FindPatient(string inpatientNO, FS.HISFC.Models.Pharmacy.DrugControl drugControl, bool isDrugClassBillFirst)
        {
            if (this.Nodes.Count == 0 || this.Nodes[0].Nodes.Count == 0)
            {
                return;
            }

            bool isFinded = false;
            TreeNode curNode = this.SelectedNode;

            #region 当前选中的节点是查找的患者节点，查找下一个节点
            if (curNode != null && curNode.Tag != null)
            {
                FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = curNode.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                if (drugMessage.ID == inpatientNO)
                {
                    TreeNode curParentNode = curNode;
                    //回溯到最高父
                    while (curParentNode.Parent != null)
                    {
                        curParentNode = curParentNode.Parent;
                    }

                    #region 当前节点之后的节点查找患者
                    if (isDrugClassBillFirst)
                    {
                        //第三级节点患者
                        if (drugControl.ShowLevel > 1)
                        {
                            bool continued = true;
                            foreach (TreeNode levelOndeNode in this.Nodes)
                            {
                                if (levelOndeNode == curParentNode)
                                {
                                    continued = false;
                                    continue;
                                }
                                if (continued)
                                {
                                    continue;
                                }
                                foreach (TreeNode levelTwoNode in levelOndeNode.Nodes)
                                {
                                    TreeNode findedNode = this.FindPatientTypeNode(inpatientNO, levelTwoNode);
                                    if (findedNode != null)
                                    {
                                        this.SelectedNode = findedNode;
                                        isFinded = true;
                                        break;
                                    }
                                }
                                if (isFinded)
                                {
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (drugControl.ShowLevel == 3)
                        {
                            //第二级节点患者
                            bool continued = true;
                            foreach (TreeNode levelOndeNode in this.Nodes)
                            {
                                if (levelOndeNode == curParentNode)
                                {
                                    continued = false;
                                    continue;
                                }
                                if (continued)
                                {
                                    continue;
                                }
                                TreeNode findedNode = this.FindPatientTypeNode(inpatientNO, levelOndeNode);
                                if (findedNode != null)
                                {
                                    this.SelectedNode = findedNode;
                                    isFinded = true;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion

                    #region 当前节点之前的节点查找患者
                    if (isDrugClassBillFirst)
                    {
                        //第三级节点患者
                        if (drugControl.ShowLevel > 1)
                        {
                            foreach (TreeNode levelOndeNode in this.Nodes)
                            {
                                if (levelOndeNode == curParentNode)
                                {
                                    break;
                                }
                                foreach (TreeNode levelTwoNode in levelOndeNode.Nodes)
                                {
                                    TreeNode findedNode = this.FindPatientTypeNode(inpatientNO, levelTwoNode);
                                    if (findedNode != null)
                                    {
                                        this.SelectedNode = findedNode;
                                        isFinded = true;
                                        break;
                                    }
                                }
                                if (isFinded)
                                {
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (drugControl.ShowLevel == 3)
                        {
                            //第二级节点患者
                            foreach (TreeNode levelOndeNode in this.Nodes)
                            {
                                if (levelOndeNode == curParentNode)
                                {
                                    break;
                                }
                                TreeNode findedNode = this.FindPatientTypeNode(inpatientNO, levelOndeNode);
                                if (findedNode != null)
                                {
                                    this.SelectedNode = findedNode;
                                    isFinded = true;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }

            }
            #endregion

            if (isFinded)
            {
                return;
            }

            #region 当前选中的节点不是查找的患者节点或者没有选中节点
            if (isDrugClassBillFirst)
            {
                //第三级节点患者
                if (drugControl.ShowLevel > 1)
                {
                    foreach (TreeNode levelOndeNode in this.Nodes)
                    {
                        foreach (TreeNode levelTwoNode in levelOndeNode.Nodes)
                        {
                            TreeNode findedNode = this.FindPatientTypeNode(inpatientNO, levelTwoNode);
                            if (findedNode != null)
                            {
                                this.SelectedNode = findedNode;
                                isFinded = true;
                                break;
                            }
                        }
                        if (isFinded)
                        {
                            break;
                        }
                    }
                }

            }
            else
            {
                if (drugControl.ShowLevel == 3)
                {
                    //第二级节点患者
                    foreach (TreeNode levelOndeNode in this.Nodes)
                    {
                        TreeNode findedNode = this.FindPatientTypeNode(inpatientNO, levelOndeNode);
                        if (findedNode != null)
                        {
                            this.SelectedNode = findedNode;
                            isFinded = true;
                            break;
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region 配药后显示

        /// <summary>
        /// 摆药单显示
        /// </summary>
        /// <param name="alDrugBillClass">摆药单分类数组实体</param>
        /// <param name="isExpand">展开所有节点</param>
        public void ShowDrugBillClass(ArrayList alDrugBillClass, bool isExpand)
        {
            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in alDrugBillClass)
            {
                if (string.IsNullOrEmpty(drugBillClass.ApplyState) || drugBillClass.ApplyState == "0")
                {
                    drugBillClass.ApplyState = "1";
                }
                TreeNode nodeBillClass = new TreeNode();
                //图片
                nodeBillClass.ImageIndex = 11;
                nodeBillClass.SelectedImageIndex = 5;
                nodeBillClass.Text = "【" + drugBillClass.DrugBillNO + "】" + drugBillClass.Name + "(" + drugBillClass.Oper.OperTime.ToString("HH:mm:ss") + ")";
                nodeBillClass.Tag = drugBillClass;

                TreeNode nodeDept = this.FindDeptNode(drugBillClass.ApplyDept.ID);
                nodeDept.Text = drugBillClass.ApplyDept.Name;
                nodeDept.Tag = drugBillClass;

                nodeDept.Nodes.Add(nodeBillClass);
            }

            if (isExpand)
            {
                this.ExpandAll();
            }
        }
        #endregion

        /// <summary>
        /// 清除数据显示
        /// </summary>
        public void Clear()
        {
            this.Nodes.Clear();
        }
    }
}
