using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base
{
    /// <summary>
    /// [功能描述: 门诊配发药树型组件]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2006-11]<br></br>
    /// <修改记录 
    ///		
    ///  />
    /// </summary>
    public partial class tvRecipeBaseTree : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvRecipeBaseTree()
        {
            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        public tvRecipeBaseTree(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        /// <summary>
        /// 本次添加数据的树节点状态
        /// </summary>
        private string state = "0";

        /// <summary>
        /// 医嘱管理业务层，用于判断是电子方还是手工方
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.Order orderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 树节点所在的TabPage
        /// </summary>
        //private System.Windows.Forms.TabPage parentTab = null;

        /// <summary>
        /// 本次添加数据的树节点状态
        /// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        /// <summary>
        /// 树节点所在的TabPage
        /// </summary>
        //public System.Windows.Forms.TabPage ParentTab
        //{
        //    get
        //    {
        //        if (this.parentTab == null)
        //            this.parentTab = new System.Windows.Forms.TabPage();
        //        return this.parentTab;
        //    }
        //    set
        //    {
        //        this.parentTab = value;
        //    }
        //}

        /// <summary>
        /// 显示患者列表 向AddTree内增加数据
        /// </summary>
        /// <param name="alDrugRecipe">列表数组</param>
        /// <param name="isSupplemental">是否再列表内追加显示</param>
        /// <param name="isAutoShow">是否自动选中新增节点</param>
        public void ShowList(System.Windows.Forms.TreeNode rootNode, ArrayList alDrugRecipe, bool isSupplemental, bool isAutoShow, bool isShowInvoice)
        {
            if (!isSupplemental)
            {
                this.Nodes.Clear();
            }

            if (rootNode != null)
            {
                this.Nodes.Add(rootNode);
            }

            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in alDrugRecipe)
            {
                System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
                this.FindNode(drugRecipe.RecipeNO, isShowInvoice);
                if (!isShowInvoice)
                {
                    node = this.FindNode(drugRecipe.RecipeNO, isShowInvoice);
                    if (node == null)
                    {
                        node = new System.Windows.Forms.TreeNode();
                        this.Nodes.Add(node);
                    }
                }
                else
                {
                    node = this.FindNode(drugRecipe.InvoiceNO, isShowInvoice);
                    if (node == null)
                    {
                        node = new System.Windows.Forms.TreeNode();
                        this.Nodes.Add(node);
                    }

                    //{DF70D8FF-A1DD-421b-8E4A-4637745F1927}
                    //给树节点添加键值
                    node.Name = drugRecipe.RecipeNO;

                    node.Text = drugRecipe.PatientName;
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 4;
                    node.Tag = drugRecipe;
                    if (rootNode != null)
                    {
                        rootNode.Nodes.Add(node);
                    }
                    else
                    {
                        this.Nodes.Add(node);
                    }
                }

                if (isAutoShow)
                {
                    if (this.Nodes.Count > 0)
                    {
                        this.SelectedNode = this.Nodes[this.Nodes.Count - 1];
                    }
                }
            }
        }

        /// <summary>
        /// 显示患者列表 向AddTree内增加数据
        /// </summary>
        /// <param name="alDrugRecipe">列表数组</param>
        /// <param name="isSupplemental">是否再列表内追加显示</param>
        /// <param name="isAutoShow">是否自动选中新增节点</param>
        public void ShowList(ArrayList alDrugRecipe, bool isSupplemental, bool isAutoShow,bool isShowInvoice)
        {
            List<FS.HISFC.Models.Pharmacy.DrugRecipe> alInvoiceData = null;
            //增加用于发药时排除自动刷新重复数据
            Hashtable hsDrugRecipe = new Hashtable(); ;
            if (!isSupplemental)
            {
                this.Nodes.Clear();
            }
            //用于标记是否手工方,默认为手工
            bool isHandleReicpe = true;

            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in alDrugRecipe)
            {
                hsDrugRecipe.Clear();
              
                //查找是否已经添加，避免重复
                if (isShowInvoice)
                {
                    System.Windows.Forms.TreeNode node = this.FindNode(drugRecipe.InvoiceNO, isShowInvoice);
                    if (node == null)
                    {
                        node = new System.Windows.Forms.TreeNode();
                        isHandleReicpe = true;
                        this.Nodes.Add(node);
                        alInvoiceData = new List<FS.HISFC.Models.Pharmacy.DrugRecipe>();
                        hsDrugRecipe.Clear();
                    }
                    else
                    { 
                        alInvoiceData = node.Tag as List<FS.HISFC.Models.Pharmacy.DrugRecipe>;
                        foreach (FS.HISFC.Models.Pharmacy.DrugRecipe dr in alInvoiceData)
                        {
                            hsDrugRecipe.Add(dr.RecipeNO, dr);
                        }
                    }
                    ArrayList allOrderData = orderMgr.QueryOrderByRecipeNO(drugRecipe.ClinicNO, drugRecipe.RecipeNO);
                    if (allOrderData.Count != 0)
                    {
                        isHandleReicpe = false;
                    }
                    else
                    {
                        isHandleReicpe = true;
                    }
                    if (!hsDrugRecipe.ContainsKey(drugRecipe.RecipeNO))
                    {
                        alInvoiceData.Add(drugRecipe);
                    }
                    node.Name = drugRecipe.InvoiceNO;
                    if (!isHandleReicpe)
                    {
                        node.Text = drugRecipe.PatientName;
                    }
                    else
                    {
                        node.Text = drugRecipe.PatientName + "(手工方)";
                    }
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 4;

                    node.Tag = alInvoiceData;
                   
                }
                else
                {
                    System.Windows.Forms.TreeNode node = this.FindNode(drugRecipe.RecipeNO, isShowInvoice);
                    if (node == null)
                    {
                        node = new System.Windows.Forms.TreeNode();
                        this.Nodes.Add(node);
                    }
                    //{DF70D8FF-A1DD-421b-8E4A-4637745F1927}
                    //给树节点添加键值
                    node.Name = drugRecipe.RecipeNO;

                    node.Text = drugRecipe.PatientName;
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 4;

                    node.Tag = drugRecipe;
                }
               
            }

            if (isAutoShow)
            {
                if (this.Nodes.Count > 0)
                {
                    this.SelectedNode = this.Nodes[this.Nodes.Count - 1];
                }
            }
        }

        /// <summary>
        /// 显示患者列表 向AddTree内增加数据
        /// </summary>
        /// <param name="alDrugRecipe">列表数组</param>
        /// <param name="isSupplemental">是否再列表内追加显示</param>
        public void ShowList(ArrayList alDrugRecipe, bool isSupplemental,bool isShowInvoice)
        {
            this.ShowList(alDrugRecipe, isSupplemental, false, isShowInvoice);
        }

        /// <summary>
        /// 节点选择移动
        /// </summary>
        /// <param name="isDown">是否向下移动</param>
        public void SelectNext(bool isDown)
        {
            if (this.Nodes.Count <= 0)
                return;
            if (this.SelectedNode == null)
            {
                this.SelectedNode = this.Nodes[0];
                return;
            }
            int iIndex = this.SelectedNode.Index;
            if (isDown)
            {
                if (iIndex == this.Nodes.Count - 1)
                {
                    this.SelectedNode = this.Nodes[0];
                }
                else
                {
                    this.SelectedNode = this.Nodes[iIndex + 1];
                }
            }
            else
            {
                if (iIndex == 0)
                {
                    this.SelectedNode = this.Nodes[this.Nodes.Count - 1];
                }
                else
                {
                    this.SelectedNode = this.Nodes[iIndex - 1];
                }
            }
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public System.Windows.Forms.TreeNode FindNode(string queryCode, bool isShowInvoice)
        {
            if (!isShowInvoice)
            {
                foreach (System.Windows.Forms.TreeNode node in this.Nodes)
                {
                    if (node.Tag != null && node.Tag is FS.HISFC.Models.Pharmacy.DrugRecipe)
                    {
                        if (((FS.HISFC.Models.Pharmacy.DrugRecipe)node.Tag).RecipeNO == queryCode)
                        {
                            return node;
                        }
                    }
                }
                return null;
            }
            else
            {
                foreach (System.Windows.Forms.TreeNode node in this.Nodes)
                {
                    if (node.Tag != null)
                    {
                        if (node.Name == queryCode)
                        {
                            return node;
                        }
                    }
                }
                return null;
            }
        }
    }
}
