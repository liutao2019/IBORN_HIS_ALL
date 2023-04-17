using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.Base
{
    public partial class ucWorkLoadQueryManagerMaintenance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucWorkLoadQueryManagerMaintenance()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 科室管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 项目管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品管理类
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item phaManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constmanager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 项目查询类型维护管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.FeeReport queryManager = new FS.HISFC.BizLogic.Fee.FeeReport();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 查询类型帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper queryHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 项目帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 非药品项目集合
        /// </summary>
        List<FS.HISFC.Models.Fee.Item.Undrug> undrugList = new List<FS.HISFC.Models.Fee.Item.Undrug>();

        /// <summary>
        /// 药品项目集合
        /// </summary>
        List<FS.HISFC.Models.Pharmacy.Item> drugList = new List<FS.HISFC.Models.Pharmacy.Item>();

        /// <summary>
        /// 科室列表
        /// </summary>
        ArrayList deptList = new ArrayList();

        /// <summary>
        /// 非药品项目列表
        /// </summary>
        DataSet dsItem = new DataSet();

        /// <summary>
        /// 非药品项目列表视图
        /// </summary>
        DataView dvItem = new DataView();
        #endregion

        #region 属性

        bool isAllowAllDept = true;
        [Description("是否允许维护全院所有科室"), Category("设置")]
        public bool IsAllowAllDept
        {
            get
            {
                return this.isAllowAllDept;
            }
            set
            {
                this.isAllowAllDept = value;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPan()
        {
            InitData();
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitData()
        {
            ///加载科室列表
            deptList = this.deptManager.GetDeptmentAll();
            if (deptList == null || deptList.Count <= 0)
            {
                MessageBox.Show("加载科室列表失败！" + this.deptManager.Err, "提示");
                return;
            }
            deptHelper.ArrayObject = deptList;

            ///加载非药品项目列表
            undrugList = this.itemManager.QueryAllItemList();
            if (undrugList == null)
            {
                MessageBox.Show(this.itemManager.Err);
                return;
            }

            //加载药品列表
            drugList = this.phaManager.QueryItemList();
            if (drugList == null)
            {
                MessageBox.Show(this.phaManager.Err);
                return;
            }
            itemHelper.ArrayObject = new ArrayList(undrugList);
            itemHelper.ArrayObject.AddRange(new ArrayList(drugList));

            ///初始化左侧树
            InitTree();

            ///加载已维护的项目查询类型
            dsItem.Clear();
            if (this.queryManager.QueryItemQueryMend("ALL", "ALL", ref dsItem) == -1)
            {
                MessageBox.Show(this.queryManager.Err);
                return;
            }
            if (dsItem == null || dsItem.Tables.Count <= 0)
            {
                MessageBox.Show(this.queryManager.Err);
                return;
            }

            dvItem = new DataView(dsItem.Tables[0]);

            SetAmentValueToSheet();
        }

        /// <summary>
        /// 初始化查询类型
        /// </summary>
        private void InitTree()
        {
            ///加载查询类别常数
            ArrayList queryTypeList = new ArrayList();
            queryTypeList = this.constmanager.GetAllList("ITEMQUERY");
            if (queryTypeList == null || queryTypeList.Count <= 0)
            {
                MessageBox.Show("加载查询类型失败！" + this.constmanager.Err, "提示");
                return;
            }
            this.queryHelper.ArrayObject = queryTypeList;

            System.Windows.Forms.TreeNode root = new TreeNode();
            root.Tag = "All";
            root.Text = "所有查询类型";
            this.tvItemQuery.Nodes.Add(root);
            foreach (FS.HISFC.Models.Base.Const cons in queryTypeList)
            {
                System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
                node.Tag = cons;
                node.Text = cons.Name;
                root.Nodes.Add(node);
            }

            ArrayList al = null;
            foreach (System.Windows.Forms.TreeNode node in root.Nodes)
            {
                al = new ArrayList();
                FS.HISFC.Models.Base.Const cons = node.Tag as FS.HISFC.Models.Base.Const;
                al = this.queryManager.GetDeptByItemQueryType(cons.ID);
                if (al == null)
                {
                    MessageBox.Show(this.queryManager.Err);
                    return;
                }

                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    System.Windows.Forms.TreeNode childNode = new System.Windows.Forms.TreeNode();
                    childNode.Tag = obj.ID;
                    childNode.Text = obj.Name;

                    if ((this.itemManager.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                    {
                        node.Nodes.Add(childNode);
                    }
                    else
                    {
                        if ((this.itemManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID == obj.ID)
                        {
                            node.Nodes.Add(childNode);
                        }
                    }
                }
            }
            root.Expand();
        }

        DataSet dsUndrug = new DataSet();
        DataView dvUndrug = new DataView();

        /// <summary>
        /// 项目列表过滤框
        /// </summary>
        private void ItemListRowsFilter()
        {
            if (string.IsNullOrEmpty(this.txtResultFilter.Text.Trim()))
            {
                return;
            }

            string _filter = this.txtResultFilter.Text.TrimStart().TrimEnd();   //取掉首尾空字符

            try
            {
                if (dvItem != null)
                {
                    dvItem.RowFilter = "项目代码 like '%" + _filter + "%' OR 项目名称 like '%" + _filter + "%' OR 科室代码 like '%" + _filter + "%' OR 科室名称 like '%" + _filter + "%'";
                }
                //FarPoint.Win.Spread.SheetView dvSheet = this.neuSpread1_Sheet1.DataSource;

                //dvSheet.row = "项目编码 like '%" + _filter + "%' OR 项目名称 like '%" + _filter + "%' OR 拼音码 like '%" + _filter + "%' OR 五笔码 like '%" + _filter + "%' OR 科室编码 like '%" + _filter + "%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show("过滤出现问题" + ex.Message);
                return;
            }

            this.SetAmentValueToSheet();
        }
        #endregion

        #region 工具栏事件

        FS.FrameWork.WinForms.Forms.ToolBarService toolBaseService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 重载工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBaseService.AddToolButton("添加项目", "新加项", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBaseService.AddToolButton("删除项目", "删除行", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            //toolBaseService.AddToolButton("保存", "保存数据", FS.FrameWork.WinForms.Classes.EnumImageList.A保存, true, false, null);
            toolBaseService.AddToolButton("科室同步", "其他行设为相同科室", (int)FS.FrameWork.WinForms.Classes.EnumImageList.K科室, true, false, null);
            toolBaseService.AddToolButton("添加查询类型", "添加查询类型", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBaseService.AddToolButton("删除查询类型", "删除查询类型", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return toolBaseService;
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            return Save();
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            dsItem.Clear();

            if (this.queryManager.QueryItemQueryMend("ALL", "ALL", ref dsItem) == -1)
            {
                MessageBox.Show(queryManager.Err);
                return 0;
            }

            dvItem = new DataView(dsItem.Tables[0]);

            SetAmentValueToSheet();

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 保存数据 -- 逐行插入再更新
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.neuSpread2_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据");
                return 0;
            }

            this.neuSpread2.StopCellEditing();
            this.dsItem.AcceptChanges();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            queryManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
            {
                FS.FrameWork.Models.NeuObject amentObj = new FS.FrameWork.Models.NeuObject();
                amentObj = GetRowObj(i);

                if (this.isValidate(amentObj, i) == -1)
                {
                    return -1;
                }

                if (string.IsNullOrEmpty(amentObj.ID) || FS.FrameWork.Function.NConvert.ToInt32(amentObj.ID) == 0)
                {
                    amentObj.ID = GetNewRowNumber();
                    if (this.queryManager.InsertItemInfoAment(amentObj) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.queryManager.Err);
                        return -1;
                    }
                }
                else
                {
                    if (this.queryManager.UpdateItemInfoAment(amentObj) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.queryManager.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            ///刷新树列表
            this.tvItemQuery.Nodes.Clear();
            this.InitTree();

            ///重新加载已维护的项目查询类型
            dsItem.Clear();
            if (this.queryManager.QueryItemQueryMend("ALL", "ALL", ref dsItem) == -1)
            {
                MessageBox.Show(this.queryManager.Err);
                return -1;
            }

            dvItem = new DataView(dsItem.Tables[0]);

            SetAmentValueToSheet();

            MessageBox.Show("保存成功!");
            return 1;
        }

        /// <summary>
        /// 科室同步
        /// </summary>
        /// <returns></returns>
        private int SameDepartment()
        {
            //if (this.neuSpread2_Sheet1.RowCount <= 0)
            //{
            //    return 0;
            //}
            //string deptcode = "";

            //if (string.IsNullOrEmpty(deptcode) || string.Equals("ALL",deptcode))
            //{
            //    deptcode = this.neuSpread2_Sheet1.Cells[0, 2].Text.Trim();
            //}

            //if (string.IsNullOrEmpty(deptcode))
            //{
            //    MessageBox.Show("目标科室为空,请为第一行科室赋值,或在科室列表中进行选择");
            //    return -1;
            //}

            //for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
            //{
            //    this.neuSpread2_Sheet1.Cells[i, 2].Text = deptcode;
            //    this.neuSpread2_Sheet1.Cells[i, 2].Tag = deptHelper.GetObjectFromID(deptcode);
            //    this.neuSpread2_Sheet1.Cells[i, 3].Text = deptHelper.GetName(deptcode);
            //}

            return 1;
        }

        /// <summary>
        /// 工具栏按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "保存":
                    Save();
                    break;
                case "添加查询类型":
                    AddQueryType();
                    break;
                case "删除查询类型":
                    DeleteQueryType();
                    break;
                case "科室同步":
                    SameDepartment();
                    break;
                case "添加项目":
                    AddItem();
                    break;
                case "删除项目":
                    DeleteRow();
                    break;
                default:
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        #region 方法

        /// <summary>
        /// 初始化已维护的查询信息
        /// </summary>
        private void SetAmentValueToSheet()
        {
            this.neuSpread2_Sheet1.DataSource = dvItem;

            this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;
            SetAmentSheetWidth();
        }

        /// <summary>
        /// 设置维护列表列宽
        /// </summary>
        private void SetAmentSheetWidth()
        {
            this.neuSpread2_Sheet1.Columns.Get(0).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread2_Sheet1.Columns.Get(0).Visible = false;

            this.neuSpread2_Sheet1.Columns.Get(1).Width = 200F;
            this.neuSpread2_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread2_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(2).Width = 80F;
            this.neuSpread2_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread2_Sheet1.Columns.Get(2).Visible = false;

            this.neuSpread2_Sheet1.Columns.Get(3).Width = 120F;    //科室
            this.neuSpread2_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;

            this.neuSpread2_Sheet1.Columns.Get(4).Width = 110F;
            this.neuSpread2_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread2_Sheet1.Columns.Get(5).Width = 160F;
            this.neuSpread2_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;

            this.neuSpread2_Sheet1.Columns.Get(6).Width = 80F;
            this.neuSpread2_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread2_Sheet1.Columns.Get(6).Visible = false;
            //this.neuSpread2_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }

        /// <summary>
        /// 锁定列
        /// </summary>
        private void SetSheetColumnLocked()
        {
            this.neuSpread2_Sheet1.Columns.Get(0).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(1).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(2).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(3).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(4).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(5).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(6).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(7).Locked = false;
        }
        /// <summary>
        /// 有效性判断
        /// </summary>
        /// <returns></returns>
        private int isValidate(FS.FrameWork.Models.NeuObject obj, int i)
        {
            if (obj == null)
            {
                MessageBox.Show("未添加新项目，无需保存！", "提示");
                return -1;
            }
            if (obj.User01 == "")
            {
                MessageBox.Show("第" + (i + 1) + "行" + "科室编码不能为空，请选择科室！", "提示");
                return -1;
            }
            if (obj.User02 == "")
            {
                MessageBox.Show("第" + (i + 1) + "行" + "项目代码不能为空，请选择项目！", "提示");
                return -1;
            }
            //for (int i = 0; i <= this.neuSpread2_Sheet1.Rows.Count; i++)
            //{
            //    if (string.IsNullOrEmpty(this.neuSpread2_Sheet1.Cells[i, 1].Value.ToString()))
            //    {
            //        MessageBox.Show("科室不可以为空,请填写科室");
            //        return -1;
            //    }

            //    if (string.IsNullOrEmpty(this.neuSpread2_Sheet1.Cells[i, 5].Value.ToString()))
            //    {
            //        MessageBox.Show("项目信息不能为空,请选择项目!\n   可删除重新添加   ");
            //        return -1;
            //    }

            //    for (int j = 0; j < i; j++)
            //    {
            //        if (neuSpread2_Sheet1.Cells[j, 5].Text == neuSpread2_Sheet1.Cells[i,5].Text)
            //        {
            //            if (neuSpread2_Sheet1.Cells[j, 2].Text == neuSpread2_Sheet1.Cells[i,2].Text)
            //            {
            //                if (neuSpread2_Sheet1.Cells[j, 1].Text == this.txtListType.Text)
            //                {
            //                    MessageBox.Show("已经添加的项");
            //                    this.neuSpread2_Sheet1.SetActiveCell(i, 0, false);
            //                    return 0;
            //                }
            //            }
            //        }
            //    }

            //}

            return 1;
        }

        /// <summary>
        /// 获取新行的编号
        /// </summary>
        /// <returns></returns>
        private string GetNewRowNumber()
        {
            string ruleSeq = this.queryManager.GetTypeSequence();
            return ruleSeq;
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <returns></returns>
        private int AddItem()
        {
            if (this.tvItemQuery.SelectedNode == null)
            {
                return -1;
            }

            if (this.tvItemQuery.SelectedNode.Level == 1)
            {
                int rowcount = this.dsItem.Tables[0].Rows.Count;

                DataRow drv = dsItem.Tables[0].NewRow();

                drv["编号"] = 0;
                drv["查询类型"] = this.tvItemQuery.SelectedNode.Text;
                drv["科室代码"] = "";
                drv["科室名称"] = "";
                drv["项目代码"] = "";
                drv["项目名称"] = "";
                drv["项目内码"] = "";
                drv["顺序号"] = rowcount;

                dsItem.Tables[0].Rows.Add(drv);

                this.SetSheetColumnLocked();
                this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;
                this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;
            }
            else if (this.tvItemQuery.SelectedNode.Level == 2)
            {
                int rowcount = this.dsItem.Tables[0].Rows.Count;

                DataRow drv = dsItem.Tables[0].NewRow();

                drv["编号"] = 0;
                drv["查询类型"] = this.tvItemQuery.SelectedNode.Parent.Text;
                drv["科室代码"] = this.tvItemQuery.SelectedNode.Tag.ToString();
                drv["科室名称"] = this.tvItemQuery.SelectedNode.Text;
                drv["项目代码"] = "";
                drv["项目名称"] = "";
                drv["项目内码"] = "";
                drv["顺序号"] = rowcount;

                dsItem.Tables[0].Rows.Add(drv);

                this.SetSheetColumnLocked();
                this.neuSpread2_Sheet1.Columns.Get(2).Locked = true;
                this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;
                this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;
            }
            else
            {
                MessageBox.Show("请选择子节点添加！", "提示");
                return -1;
            }

            SetAmentSheetWidth();
            return 1;
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <returns></returns>
        private int DeleteRow()
        {
            if (this.neuSpread2_Sheet1.ActiveRowIndex < 0)
            {
                MessageBox.Show("请选择行!");
                return 0;
            }

            if (MessageBox.Show("是否删除该项目", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return 0;
            }

            int activeRow = this.neuSpread2_Sheet1.ActiveRowIndex;

            if (!string.IsNullOrEmpty(this.neuSpread2_Sheet1.Cells[activeRow, 0].Text) && !string.Equals(this.neuSpread2_Sheet1.Cells[activeRow, 0].Text, "0"))
            {
                FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();

                tempObj = GetRowObj(activeRow);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                queryManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.queryManager.DeleteItemInfoAment(tempObj) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除失败" + this.queryManager.Err);
                    return -1;
                }
                this.neuSpread2_Sheet1.RemoveRows(activeRow, 1);

                FS.FrameWork.Management.PublicTrans.Commit();
                return 1;
            }
            else
            {
                this.neuSpread2_Sheet1.RemoveRows(activeRow, 1);
            }

            return 0;
        }

        /// <summary>
        /// 行取值
        /// </summary>
        /// <param name="rowindex"></param>
        /// <returns></returns>
        private FS.FrameWork.Models.NeuObject GetRowObj(int rowindex)
        {
            FS.FrameWork.Models.NeuObject amentObj = new FS.FrameWork.Models.NeuObject();

            if (rowindex < 0)
            {
                return null;
            }
            amentObj.ID = this.neuSpread2_Sheet1.Cells[rowindex, 0].Text.TrimEnd();
            amentObj.Name = this.queryHelper.GetObjectFromName(this.neuSpread2_Sheet1.Cells[rowindex, 1].Value.ToString()).ID.ToString();
            amentObj.User01 = this.neuSpread2_Sheet1.Cells[rowindex, 2].Text.TrimEnd();
            amentObj.User02 = this.neuSpread2_Sheet1.Cells[rowindex, 6].Text.TrimEnd();
            amentObj.User03 = this.neuSpread2_Sheet1.Cells[rowindex, 5].Text.TrimEnd();
            amentObj.Memo = this.neuSpread2_Sheet1.Cells[rowindex, 7].Text.TrimEnd();

            return amentObj;
        }

        /// <summary>
        /// 添加查询类型
        /// </summary>
        private void AddQueryType()
        {
            ucAddQueryType uc = new ucAddQueryType();
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc, FormBorderStyle.Fixed3D);
            if (uc.DiaglogResult)
            {
                this.tvItemQuery.Nodes.Clear();
                this.InitTree();
            }
        }

        /// <summary>
        /// 删除查询类型
        /// </summary>
        private void DeleteQueryType()
        {
            string deptCode = string.Empty;
            string queryType = string.Empty;

            if (this.tvItemQuery.SelectedNode == null)
            {
                MessageBox.Show("请选择要删除的具体查询类型！");
                return;
            }

            if (this.tvItemQuery.SelectedNode.Level == 0)
            {
                MessageBox.Show("请选择具体查询类型！");
                return;
            }
            else if (this.tvItemQuery.SelectedNode.Level == 1)
            {
                FS.HISFC.Models.Base.Const cons = this.tvItemQuery.SelectedNode.Tag as FS.HISFC.Models.Base.Const;
                queryType = cons.ID;
                deptCode = "ALL";

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.queryManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.tvItemQuery.SelectedNode.Nodes.Count > 0)
                {
                    if (MessageBox.Show("删除此查询类型下的所有科室项目，是否继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (this.queryManager.DeleteItemInfoAment(queryType, deptCode) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除查询类型下的项目失败" + this.queryManager.Err);
                            return;
                        }
                        if (this.queryManager.DeleteItemQueryConst(queryType) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除查询类型失败" + queryManager.Err);
                            return;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                        MessageBox.Show("删除成功！");

                        this.tvItemQuery.Nodes.Clear();
                        this.InitTree();

                        this.OnQuery(null, null);
                    }
                }
                else
                {
                    if (MessageBox.Show("删除该查询类型，是否继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (this.queryManager.DeleteItemQueryConst(queryType) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除查询类型失败" + this.queryManager.Err);
                            return;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                        MessageBox.Show("删除成功！");

                        this.tvItemQuery.Nodes.Clear();
                        this.InitTree();

                        this.OnQuery(null, null);
                    }
                }
            }
            else if (this.tvItemQuery.SelectedNode.Level == 2)
            {
                FS.HISFC.Models.Base.Const cons = this.tvItemQuery.SelectedNode.Parent.Tag as FS.HISFC.Models.Base.Const;
                queryType = cons.ID;
                deptCode = this.tvItemQuery.SelectedNode.Tag.ToString();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                queryManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.neuSpread2.ActiveSheet.RowCount > 0)
                {
                    if (MessageBox.Show("该科室下已经维护查询项目，是否继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (this.queryManager.DeleteItemInfoAment(queryType, deptCode) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除查询类型下的项目失败" + queryManager.Err);
                            return;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        MessageBox.Show("删除成功！");

                        this.tvItemQuery.Nodes.Clear();
                        this.InitTree();

                        this.OnQuery(null, null);
                    }
                }
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 加载窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucItemQueryManagerNew_Load(object sender, EventArgs e)
        {
            InitPan();
        }

        /// <summary>
        /// 已维护项目过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtResultFilter_TextChanged(object sender, EventArgs e)
        {
            ItemListRowsFilter();
        }

        /// <summary>
        /// 添加具体项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj;

            if (e.Column == 2)
            {
                if (deptList != null && deptList.Count > 0)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    FS.FrameWork.WinForms.Classes.Function.ChooseItem(deptList, ref obj);
                    if (obj.ID == "")
                    {
                        this.neuSpread2_Sheet1.SetValue(e.Row, 2, obj.ID);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 3, obj.Name);
                        return;
                    }
                    else
                    {
                        if ((this.deptManager.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                        {
                            this.neuSpread2_Sheet1.SetValue(e.Row, 2, obj.ID);
                            this.neuSpread2_Sheet1.SetValue(e.Row, 3, obj.Name);
                        }
                        else
                        {
                            if ((this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID != obj.ID)
                            {
                                MessageBox.Show("请选择登陆科室：" + (this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Dept.Name);
                                return;
                            }
                            else
                            {
                                this.neuSpread2_Sheet1.SetValue(e.Row, 2, obj.ID);
                                this.neuSpread2_Sheet1.SetValue(e.Row, 3, obj.Name);
                            }
                        }
                    }
                }
            }
            if (e.Column == 4)
            {
                ArrayList al = new ArrayList(undrugList);
                al.AddRange(new ArrayList(drugList));

                if (al != null && al.Count > 0)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref obj);
                    if (obj.ID == "")
                    {
                        this.neuSpread2_Sheet1.SetValue(e.Row, 4, "");
                        this.neuSpread2_Sheet1.SetValue(e.Row, 5, obj.Name);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 6, obj.ID);
                        return;
                    }
                    FS.HISFC.Models.Base.ISpell spell = obj as FS.HISFC.Models.Base.ISpell;
                    DataRow[] drCol = dsItem.Tables[0].Select("科室代码 = '" + this.neuSpread2_Sheet1.GetValue(e.Row, 2).ToString()
                        + "' and 项目内码 = '" + obj.ID + "' and 查询类型 = '" + this.neuSpread2_Sheet1.GetValue(e.Row, 1).ToString() + "'");
                    if (drCol.Length > 0)
                    {
                        MessageBox.Show("已经添加的项");
                        return;
                    }
                    this.neuSpread2_Sheet1.SetValue(e.Row, 4, spell.UserCode);   //自定义码
                    this.neuSpread2_Sheet1.SetValue(e.Row, 5, obj.Name);
                    this.neuSpread2_Sheet1.SetValue(e.Row, 6, obj.ID);          //主键，item_code或者drug_code
                }
            }
        }

        /// <summary>
        /// 单击树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvItemQuery_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                ///加载已维护的项目查询类型
                dsItem.Clear();
                if (this.queryManager.QueryItemQueryMend("ALL", "ALL", ref dsItem) == -1)
                {
                    MessageBox.Show(this.queryManager.Err);
                    return;
                }

                dvItem = new DataView(dsItem.Tables[0]);

                SetAmentValueToSheet();
            }
            else if (e.Node.Level == 1)
            {
                dvItem.RowFilter = "查询类型 = '" + e.Node.Text + "'";

                SetAmentValueToSheet();
            }
            else if (e.Node.Level == 2)
            {
                dvItem.RowFilter = "查询类型 = '" + e.Node.Parent.Text + "' and 科室名称 = '" + e.Node.Text + "'";

                SetAmentValueToSheet();
            }
        }

        /// <summary>
        /// 回车事件,查询类型节点过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQueryFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                List<TreeNode> list = new List<TreeNode>();
                foreach (TreeNode node in this.tvItemQuery.Nodes[0].Nodes)
                {
                    if (node.Level == 1)
                    {
                        list.Add(node);
                    }
                }

                foreach (TreeNode node in list)
                {
                    FS.HISFC.Models.Base.Const cons = node.Tag as FS.HISFC.Models.Base.Const;
                    if (cons.SpellCode == this.txtQueryFilter.Text.Trim().ToUpper()
                        || cons.Name == this.txtQueryFilter.Text.Trim())
                    {
                        this.tvItemQuery.SelectedNode = node;
                        return;
                    }
                }
            }
        }
        #endregion
    }
}
