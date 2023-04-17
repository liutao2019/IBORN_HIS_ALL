using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Report.Maintenance
{
    public partial class ucItemQueryManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucItemQueryManager()
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
        private FS.HISFC.BizLogic.Manager.ItemInfoQuery queryManager = new FS.HISFC.BizLogic.Manager.ItemInfoQuery();

        /// <summary>
        /// 权限类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PowerLevelManager myPriv = new FS.HISFC.BizLogic.Manager.PowerLevelManager();

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
        /// 权限
        /// </summary>
        ArrayList alPrivs = new ArrayList();

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
        [Description("是否允许维护全院所有科室"),Category("设置")]
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

        private void InitFpSheet()
        {
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编号";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "查询类型";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "科室编码";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "科室";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "项目代码";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "项目名称";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "项目内码";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "顺序号";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "权限";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "权限名称";
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(0).Label = "编号";
            this.neuSpread2_Sheet1.Columns.Get(0).Width = 42F;
            this.neuSpread2_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(1).Label = "查询类型";
            this.neuSpread2_Sheet1.Columns.Get(1).Width = 86F;
            this.neuSpread2_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(2).Label = "科室编码";
            this.neuSpread2_Sheet1.Columns.Get(2).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(3).Label = "科室";
            this.neuSpread2_Sheet1.Columns.Get(3).Width = 69F;
            this.neuSpread2_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(4).Label = "项目代码";
            this.neuSpread2_Sheet1.Columns.Get(4).Width = 78F;
            this.neuSpread2_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(5).Label = "项目名称";
            this.neuSpread2_Sheet1.Columns.Get(5).Width = 173F;
            this.neuSpread2_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(6).Label = "项目内码";
            this.neuSpread2_Sheet1.Columns.Get(6).Width = 77F;
            this.neuSpread2_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(7).Label = "顺序号";
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 46F;
            this.neuSpread2_Sheet1.Columns.Get(8).Label = "权限";
            this.neuSpread2_Sheet1.Columns.Get(8).Width = 46F;
            this.neuSpread2_Sheet1.Columns.Get(9).Label = "权限名称";
            this.neuSpread2_Sheet1.Columns.Get(9).Width = 65F;
            this.neuSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread2_Sheet1.RowHeader.Columns.Get(0).Width = 23F;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread2_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            else
            {
                FS.HISFC.Models.Base.Department all = new FS.HISFC.Models.Base.Department();
                all.ID = "ALL";
                all.Name = "全部科室";
                all.UserCode = "ALL";

                deptList.Add(all);
            }

            deptHelper.ArrayObject = deptList;

            ///加载非药品项目列表
            undrugList = this.itemManager.QueryAllItemList();
            if (undrugList == null)
            {
                MessageBox.Show(this.queryManager.Err);
                return;
            }

            drugList = this.phaManager.QueryItemList();
            if (drugList == null)
            {
                MessageBox.Show(this.queryManager.Err);
                return;
            }

            itemHelper.ArrayObject = new ArrayList(undrugList);
            itemHelper.ArrayObject.AddRange(new ArrayList(drugList));

            ///初始化左侧树
            InitTree();

            ///加载已维护的项目查询类型
            dsItem.Clear();
            if (this.queryManager.QueryItemQueryMend_Const("ALL", "ALL", ref dsItem) == -1)
            {
                MessageBox.Show(queryManager.Err);
                return;
            }
            if (dsItem == null || dsItem.Tables.Count <= 0)
            {
                MessageBox.Show(this.queryManager.Err);
                return;
            }

            dvItem = new DataView(dsItem.Tables[0]);

            SetAmentValueToSheet();

            ArrayList alTemp = this.myPriv.LoadLevel3ByLevel1("81");

            FS.HISFC.Models.Admin.PowerLevelClass3 priv = new FS.HISFC.Models.Admin.PowerLevelClass3();

            priv.Class3Code = "";
            priv.Class3Name = "不限";

            alTemp.Add(priv);

            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 pri in alTemp)
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                obj.ID = pri.ID;
                obj.Name = pri.Name;

                alPrivs.Add(obj);
            }
        }

        /// <summary>
        /// 初始化查询类型
        /// </summary>
        private void InitTree()
        {
            ///加载查询类别常数
            ArrayList queryTypeArray = new ArrayList();

            queryTypeArray = this.constmanager.GetList("ITEMQUERYTYPE");

            if (queryTypeArray == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("加载常数[ITEMQUERYTYPE]列表发生错误！"));
                return;
            }

            this.queryHelper.ArrayObject = queryTypeArray;

            System.Windows.Forms.TreeNode root = new TreeNode();
            root.Tag = "All";
            root.Text = "所有查询类型";
            this.tvItemQuery.Nodes.Add(root);
            foreach (FS.HISFC.Models.Base.Const cons in queryTypeArray)
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
                //return;
            }

            string _filter = this.txtResultFilter.Text.TrimStart().TrimEnd();   //取掉首尾空字符

            try
            {
                if (dvItem != null)
                {
                    dvItem.RowFilter = "项目代码 like '%" + _filter + "%' OR 项目内码 like '%" + _filter + "%' OR 项目名称 like '%" + _filter
                        + "%' OR 科室代码 like '%" + _filter + "%' OR 科室名称 like '%" + _filter + "%'";
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

            #region 屏蔽
            //Hashtable hsPriv = new Hashtable();

            //for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
            //{
            //    if (!hsPriv.Contains(this.neuSpread2_Sheet1.Cells[i, 3].Text))
            //    {
            //        hsPriv.Add(this.neuSpread2_Sheet1.Cells[i, 3].Text, this.neuSpread2_Sheet1.Cells[i, 9].Text);
            //    }
            //    else
            //    {
            //        string priv = hsPriv[this.neuSpread2_Sheet1.Cells[i, 3].Text].ToString();

            //        if (this.neuSpread2_Sheet1.Cells[i, 9].Text != priv)
            //        {
            //            MessageBox.Show("同一报表内同一科室的权限只能有一种，请修改！");
            //            SetAmentValueToSheet();
            //            return -1;
            //        }
            //    }
            //}
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            queryManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Base.Const amentObj = new FS.HISFC.Models.Base.Const();
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
                        FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(queryManager.Err);
                        return -1;
                    }
                }
                else
                {
                    if (this.queryManager.UpdateItemInfoAment(amentObj) <= 0)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(queryManager.Err);
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
            if (this.queryManager.QueryItemQueryMend_Const("ALL", "ALL", ref dsItem) == -1)
            {
                MessageBox.Show(queryManager.Err);
                return -1;
            }

            dvItem = new DataView(dsItem.Tables[0]);

            SetAmentValueToSheet();

            MessageBox.Show("保存成功!");
            return 1;
        }

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

            this.InitFpSheet();
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
            //this.neuSpread2_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread2_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.InitFpSheet();
            
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
                MessageBox.Show("未添加新项目，无需保存！","提示");
                return -1;
            }
            if (obj.User01 == "")
            {
                MessageBox.Show("第" + (i+1) + "行" + "科室编码不能为空，请选择科室！","提示");
                return -1;
            }
            if (obj.User02 == "")
            {
                MessageBox.Show("第" + (i+1) + "行" + "项目代码不能为空，请选择项目！", "提示");
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取新行的编号
        /// </summary>
        /// <returns></returns>
        private string GetNewRowNumber()
        {
            string ruleSeq = this.queryManager.GetSequence();
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
                MessageBox.Show("请选择报表节点添加！", "提示");
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

            if (!string.IsNullOrEmpty(this.neuSpread2_Sheet1.Cells[activeRow, 0].Text) && !string.Equals(this.neuSpread2_Sheet1.Cells[activeRow,0].Text,"0"))
            {
                FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();

                tempObj = GetRowObj(activeRow);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                queryManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.queryManager.DeleteItemInfoAment(tempObj) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除失败" + queryManager.Err);
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
        private FS.HISFC.Models.Base.Const GetRowObj(int rowindex)
        {
            FS.HISFC.Models.Base.Const amentObj = new FS.HISFC.Models.Base.Const();

            if (rowindex < 0)
            {
                return null;
            }
            amentObj.ID = this.neuSpread2_Sheet1.Cells[rowindex, 0].Text.TrimEnd();
            amentObj.Name = this.queryHelper.GetObjectFromName(this.neuSpread2_Sheet1.Cells[rowindex, 1].Value.ToString()).ID.ToString();
            amentObj.User01 = this.neuSpread2_Sheet1.Cells[rowindex, 2].Text.TrimEnd();
            amentObj.User02 = this.neuSpread2_Sheet1.Cells[rowindex, 4].Text.TrimEnd();
            amentObj.User03 = this.neuSpread2_Sheet1.Cells[rowindex, 5].Text.TrimEnd();
            amentObj.Memo = this.neuSpread2_Sheet1.Cells[rowindex, 7].Text.TrimEnd();
            amentObj.SpellCode = this.neuSpread2_Sheet1.Cells[rowindex, 8].Text;

            return amentObj;
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
                        this.neuSpread2_Sheet1.SetValue(e.Row, 4, obj.ID);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 5, obj.Name);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 6, "");
                        return;
                    }
                    FS.HISFC.Models.Base.ISpell spell = obj as FS.HISFC.Models.Base.ISpell;
                    DataRow[] drCol = dsItem.Tables[0].Select("科室代码 = '" + this.neuSpread2_Sheet1.GetValue(e.Row, 2).ToString()
                        + "' and 项目内码 = '" + spell.UserCode + "' and 查询类型 = '" + this.neuSpread2_Sheet1.GetValue(e.Row, 1).ToString() + "'");
                    if (drCol.Length > 0)
                    {
                        MessageBox.Show("已经添加的项");
                        return;
                    }
                    this.neuSpread2_Sheet1.SetValue(e.Row, 4, obj.ID);
                    this.neuSpread2_Sheet1.SetValue(e.Row, 5, obj.Name);
                    this.neuSpread2_Sheet1.SetValue(e.Row, 6, spell.UserCode);
                }
            }

            if (e.Column == 8)
            {
                if (this.alPrivs != null && alPrivs.Count > 0)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    FS.FrameWork.WinForms.Classes.Function.ChooseItem(alPrivs, ref obj);

                    if (obj != null)
                    {
                        this.neuSpread2_Sheet1.SetValue(e.Row, 8, obj.ID);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 9, obj.Name);
                    }
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
                if (this.queryManager.QueryItemQueryMend_Const("ALL", "ALL", ref dsItem) == -1)
                {
                    MessageBox.Show(queryManager.Err);
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

        #endregion

        private void tbAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void tbDel_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void tbSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }      
}
