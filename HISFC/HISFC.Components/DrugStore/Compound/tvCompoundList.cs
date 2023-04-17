using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// <br></br>
    /// [功能描述: 配置管理患者列表显示组件]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-08]<br></br>
    /// <说明>
    ///     1、列表数据显示时暂时屏蔽药柜管理相关部分
    /// </说明>
    /// </summary>
    public partial class tvCompoundList : Common.Controls.baseTreeView
    {
        public tvCompoundList()
        {
            InitializeComponent();
        }

        public tvCompoundList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public delegate void SelectDataHandler(ArrayList alData);

        public event SelectDataHandler SelectDataEvent;

        #region 域变量

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 药品管理类
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 药房管理类
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// 批次流水号
        /// </summary>
        private string groupCode = "U";

        /// <summary>
        /// 申请明细状态
        /// </summary>
        private string state = "0";

        /// <summary>
        /// 是否执行
        /// </summary>
        private bool isExec = false;
        #endregion

        #region 属性

        /// <summary>
        /// 批次流水号
        /// </summary>
        public string GroupCode
        {
            get
            {
                return this.groupCode;
            }
            set
            {
                if (value == null || value == "")
                {
                    this.groupCode = "U";
                }
                else
                {
                    this.groupCode = value;
                }
            }
        }

        /// <summary>
        /// 申请明细状态
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

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            try
            {
                this.ImageList = this.deptImageList;

                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptmentAll();
                if (alDept == null)
                {
                    MessageBox.Show(Language.Msg("加载科室列表发生错误" + deptManager.Err));
                }

                objHelper.ArrayObject = alDept;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("树型通知列表初始化发生错误" + ex.Message));
            }
        }

        /// <summary>
        /// 获取摆药单列表显示时患者节点
        /// </summary>
        /// <param name="nodeBill">父级节点</param>
        /// <param name="info">摆药明细信息</param>
        /// <returns>成功返回摆药单显示时的患者节点</returns>
        private TreeNode GetNodePatient(FS.HISFC.Models.Pharmacy.ApplyOut info, TreeNode parentNode)
        {
            TreeNode nodePatient;
            nodePatient = new TreeNode();
            nodePatient.Text = "【" + info.User01 + "】" + info.User02;  //【床号】姓名
            nodePatient.ImageIndex = 2;
            nodePatient.SelectedImageIndex = 2;
            nodePatient.Tag = info;
            if (parentNode == null)
            {
                this.Nodes.Add(nodePatient);
            }
            else
            {
                parentNode.Nodes.Add(nodePatient);
            }

            return nodePatient;
        }

        /// <summary>
        /// 获取摆药单列表显示科室节点
        /// </summary>
        /// <param name="info">摆药明细信息</param>
        /// <returns>成功返回摆药单列表显示时的科室节点</returns>
        private TreeNode GetNodeDept(FS.HISFC.Models.Pharmacy.ApplyOut info, TreeNode parentNode)
        {
            TreeNode nodeDept = new TreeNode();
            if (info.ApplyDept.Name == "")
            {
                info.ApplyDept.Name = objHelper.GetName(info.ApplyDept.ID);
            }

            nodeDept.Text = info.ApplyDept.Name;
            nodeDept.ImageIndex = 1;
            nodeDept.SelectedImageIndex = 1;
            nodeDept.Tag = info;
            if (parentNode == null)
            {
                this.Nodes.Add(nodeDept);
            }
            else
            {
                parentNode.Nodes.Add(nodeDept);
            }

            return nodeDept;
        }

        /// <summary>
        /// 根据传入的摆药通知数组，显示在tvCompoundList中
        /// 传入参数数组是按照科室、患者排序的
        /// </summary>
        /// <param name="alDrugMessage">摆药通知数组</param>
        public virtual void ShowList(List<FS.HISFC.Models.Pharmacy.ApplyOut> alApplyOut)
        {
            this.Nodes.Clear();

            string privDeptCode = "";				//上一个科室
            TreeNode nodePatient;
            TreeNode nodeDept = new TreeNode();

            foreach (ApplyOut info in alApplyOut)
            {
                #region 每次将节点清空重新添加

                this.SuspendLayout();


                if (info.ApplyDept.ID != privDeptCode)		//添加新的科室节点
                {
                    nodeDept = this.GetNodeDept(info, null);

                    privDeptCode = info.ApplyDept.ID;
                }

                nodePatient = this.GetNodePatient(info, nodeDept);

                this.ResumeLayout();

                #endregion
            }

            if (this.Nodes.Count > 0)
            {
                this.Nodes[0].Expand();
            }
        }
        /// <summary>
        /// 检索所有病区含有该药品的数据{4F8AF409-2ED7-4e18-954C-DE0A61A0F061}feng.ch
        /// </summary>
        /// <param name="stockDeptId">库存科室</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="list">返回申请数组</param>
        public virtual void ShowListByParmacy(string stockDeptId, ref ArrayList list)
        {
            this.Nodes.Clear();
            TreeNode nodeDept = new TreeNode();
            nodeDept.Tag = "allDept";
            nodeDept.Text = "所有病区";
            this.Nodes.Add(nodeDept);
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            list = itemManager.QueryCompoundApplyOut(stockDeptId, "ALL", "U", null, "0", false);
        }
        /// <summary>
        /// 获取选中的节点的摆药申请信息
        /// </summary>
        /// <param name="selectNode">当前选中节点</param>
        /// <param name="al">摆药明细信息</param>
        protected virtual void GetSelectData(TreeNode selectNode, ref ArrayList al)
        {
            #region 获取原始批次信息

            al = null;

            if (selectNode == null)
            {
                return;
            }

            FS.HISFC.Models.Pharmacy.ApplyOut info = selectNode.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
            if (info == null)
            {
                return;
            }

            //判断父节点是否为空，如父节点为空 说明此时点击的为科室节点
            if (selectNode.Parent == null)
            {
                //根据当前库存药房、当前申请科室获取所有申请明细信息
                al = this.itemManager.QueryCompoundApplyOut(info.StockDept.ID, info.ApplyDept.ID, this.GroupCode, null, this.state, this.isExec);
            }
            else
            {
                //根据当前库存药房 当前申请科室 当前申请患者获取所有申请明细信息
                al = this.itemManager.QueryCompoundApplyOut(info.StockDept.ID, info.ApplyDept.ID, this.GroupCode, info.PatientNO, this.state, this.isExec);
            }

            if (al == null)
            {
                MessageBox.Show(Language.Msg("获取申请明细信息发生错误") + this.itemManager.Err);
                return;
            }

            #endregion

            #region 更新原始配置批次号为流水号（挪到表现层作为接口默认实现）

            //string privCompoundGroup = "-1";
            //string privNewCompoundGroupNO = "";
            //foreach (FS.HISFC.Models.Pharmacy.ApplyOut compound in al)
            //{
            //    if (compound.CompoundGroup.IndexOf("C") != -1)      //该批次流水号未更新过
            //    {
            //        if (privCompoundGroup == compound.CompoundGroup)
            //        {
            //            compound.CompoundGroup = privNewCompoundGroupNO;
            //            continue;
            //        }
            //        else
            //        {
            //            string newCompoundGroupNO = "";
            //            if (this.itemManager.UpdateCompoundGroupNO(compound.CompoundGroup, ref newCompoundGroupNO) == -1)
            //            {
            //                MessageBox.Show(Language.Msg("更新原始配置批次号为流水号时发生错误"));
            //                return;
            //            }

            //            privCompoundGroup = compound.CompoundGroup;
            //            compound.CompoundGroup = newCompoundGroupNO;
            //            privNewCompoundGroupNO = newCompoundGroupNO;       
            //        }
            //    }
            //}

            #endregion

            #region 药柜管理的处理 暂时屏蔽此处

            /*
            if (stockMark != null && stockMark.ID != "")
            {
                //获取科室库存药品信息
                if (this.hsStockData == null)
                {
                    this.hsStockData = new Hashtable();

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在加载科室库存信息 请稍候..."));
                    Application.DoEvents();

                    ArrayList alStorage = this.itemManager.QueryStockinfoList(stockMark.ID);

                    foreach (FS.HISFC.Models.Pharmacy.Storage storage in alStorage)
                    {
                        if (storage.IsArkManager)
                        {
                            continue;
                        }
                        this.hsStockData.Add(storage.Item.ID, null);
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }                          
            }
            
             if (this.hsStockData == null)
            {
                al = alTotal;
            }
            else
            {
                //屏蔽药柜管理的药品
                if (alTotal.Count > 0)
                {
                    if (alTotal[0] is FS.HISFC.Models.Pharmacy.ApplyOut)
                    {
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alTotal)
                        {
                            if (this.hsStockData.ContainsKey(info.Item.ID))
                            {
                                al.Add(info);
                            }
                        }
                    }
                    else
                    {
                        al = alTotal;
                    }
                }
            }
             
             *
            */

            #endregion
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在加载明细数据.请稍候.."));
            Application.DoEvents();

            ArrayList al = new ArrayList();

            this.GetSelectData(e.Node, ref al);

            if (al != null)
            {
                if (this.SelectDataEvent != null)
                {
                    this.SelectDataEvent(al);
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            base.OnAfterSelect(e);
        }
        /// <summary>
        /// 刷新数据显示
        /// </summary>
        internal void RefreshData()
        {
            TreeViewEventArgs e = new TreeViewEventArgs(this.SelectedNode);

            this.OnAfterSelect(e);
        }
    }
}
