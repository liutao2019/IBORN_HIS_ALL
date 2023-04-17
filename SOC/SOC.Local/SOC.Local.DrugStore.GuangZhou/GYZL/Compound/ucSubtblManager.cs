using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Order;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound
{
    /// <summary>
    /// 配置中心辅材算法维护
    /// </summary>
    public partial class ucSubtblManagement : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSubtblManagement()
        {
            InitializeComponent();
        }

        #region 系统层管理类

        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        //FS.HISFC.BizLogic.Order.SubtblManager subtblMgr = new FS.HISFC.BizLogic.Order.SubtblManager();
        FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound.AdditionalItem subtblMgr = new SOC.Local.DrugStore.GuangZhou.GYZL.Compound.AdditionalItem();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region 变量

        ArrayList al = new ArrayList();

        /// <summary>
        /// 非药品项目
        /// </summary>
        private DataTable dtUndrug = null;


        /// <summary>
        /// 药品列表
        /// </summary>
        private DataTable dtDrug = null;

        FS.FrameWork.WinForms.Forms.ToolBarService toolbar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 
        /// </summary>
        ArrayList alItem = new ArrayList();

        /// <summary>
        /// 当前维护的用法或项目
        /// </summary>
        private FS.FrameWork.Models.NeuObject currentUsage = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 科室列表
        /// </summary>
        //ArrayList alDept = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        IList<string> IDepartmentList = new List<string>();
        /// <summary>
        /// 科室帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 非药品项目列表
        /// </summary>
        ArrayList alItems = new ArrayList();

        FS.FrameWork.Public.ObjectHelper itemHelper = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        private FS.HISFC.Models.Base.Employee oper = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

        /// <summary>
        /// 操作说明文档
        /// </summary>
        private string explainNote = "辅材带出维护操作说明.doc";

        /// <summary>
        /// 操作说明文档
        /// </summary>
        [Category("设置"), Description("辅材带出维护操作说明对应的文档名称，默认“辅材带出维护操作说明.doc”")]
        public string ExplainNote
        {
            get
            {
                return explainNote;
            }
            set
            {
                explainNote = value;
            }
        }

        /// <summary>
        /// 是否所有科室附材都显示
        /// </summary>
        private bool isAllDeptShow = true;

        /// <summary>
        /// 是否所有科室附材都显示
        /// </summary>
        [Category("设置"), Description("是否所有科室附材都显示")]
        public bool IsAllDeptShow
        {
            get
            {
                return isAllDeptShow;
            }
            set
            {
                isAllDeptShow = value;
            }
        }

        /// <summary>
        /// 特殊说明
        /// </summary>
        private string useExplain = "";

        /// <summary>
        /// 特殊说明 会在界面显示
        /// </summary>
        [Category("设置"), Description("特殊说明 会在界面显示")]
        public string UseExplain
        {
            get
            {
                return useExplain;
            }
            set
            {
                useExplain = value;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据请稍后....");
            Application.DoEvents();
            this.InitTree();
            this.ucInputItem1.Init();
            
            this.InitItemFp();

            ArrayList alDept = new ArrayList();
            //alDept = this.managerIntegrate.GetDepartment();
            alDept = SOC.HISFC.BizProcess.Cache.Common.GetDept();
            if (alDept == null)
            {
                MessageBox.Show(managerIntegrate.Err);
            }
            else
            {
                FS.HISFC.Models.Base.Const allObj = new FS.HISFC.Models.Base.Const();
                allObj.Name = "全部";
                allObj.ID = "ROOT";
                allObj.UserCode = "QB";
                this.deptHelper.ArrayObject.Add(allObj);
                deptHelper.ArrayObject.AddRange(alDept);
                //alDept.Add(allObj);
                //this.deptHelper.ArrayObject = alDept;
            }

            alItems = new ArrayList(feeMgr.QueryAllItemsList());
            if (alItems == null)
            {
                MessageBox.Show(feeMgr.Err);
            }
            else
            {
                this.itemHelper.ArrayObject = alItems;
            }


            #region 过滤列表

            #endregion

            this.lblUseExplain.Text = this.useExplain;
            this.pnDesign.Visible = true;
            if (string.IsNullOrEmpty(lblUseExplain.Text))
            {
                this.pnDesign.Visible = false;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            #region 初始化科室列表

            ArrayList al = managerIntegrate.QueryDepartment(oper.Nurse.ID);
            if (al == null || al.Count == 0)
            {
                this.neuListBox1.Items.Add(oper.Dept);
                this.IDepartmentList.Add(oper.Dept.ID);
            }
            else
            {
                for (int i = 0; i < al.Count; i++)
                {
                    try
                    {
                        FS.FrameWork.Models.NeuObject o = al[i] as FS.FrameWork.Models.NeuObject;
                        this.neuListBox1.Items.Add(o);
                        this.IDepartmentList.Add(o.ID);
                    }
                    catch { }
                }
            }

            if (this.neuListBox1.Items.Count > 0)
            {
                this.neuListBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("没有维护相关科室或护理站，维护后才能使用该功能");
                return;
            }
            FS.FrameWork.Models.NeuObject objdept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

            #endregion
        }

        /// <summary>
        /// 初始化带出项目列表
        /// </summary>
        private void InitItemFp()
        {
            #region 非药品
            ArrayList alUndrug = new ArrayList(this.feeMgr.QueryValidItems());
            if (alUndrug == null)
            {
                MessageBox.Show(feeMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");

            this.dtUndrug = new DataTable();

            //在myDataTable中定义列
            this.dtUndrug.Columns.AddRange(new DataColumn[] {													
                                                    new DataColumn("名称",      dtStr),
                                                    new DataColumn("编码",      dtStr),
                                                    new DataColumn("规格",     dtStr),
													new DataColumn("类别",     dtStr),
													new DataColumn("单价",   dtStr),
													new DataColumn("单位",   dtStr),
													new DataColumn("拼音码",   dtStr),
													new DataColumn("五笔码",   dtStr),
													new DataColumn("项目编码",   dtStr)
											        });

            this.dtUndrug.PrimaryKey = new DataColumn[] { this.dtUndrug.Columns["项目编码"] };

            DataRow dRow;

            foreach (FS.HISFC.Models.Fee.Item.Undrug undrugObj in alUndrug)
            {
                dRow = dtUndrug.NewRow();
                dRow["名称"] = undrugObj.Name;
                dRow["编码"] = undrugObj.UserCode;
                dRow["规格"] = undrugObj.Specs;
                dRow["类别"] = undrugObj.SysClass.Name;
                dRow["单价"] = undrugObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元";
                dRow["单位"] = undrugObj.PriceUnit;
                dRow["拼音码"] = undrugObj.SpellCode;
                dRow["五笔码"] = undrugObj.WBCode;
                dRow["项目编码"] = undrugObj.ID;
                dtUndrug.Rows.Add(dRow);
            }

            this.dtUndrug.DefaultView.Sort = "编码";
            this.fpUndrug_Sheet1.DataSource = this.dtUndrug.DefaultView;

            this.fpUndrug_Sheet1.Columns[6].Visible = false;
            this.fpUndrug_Sheet1.Columns[7].Visible = false;
            this.fpUndrug_Sheet1.Columns[8].Visible = false;
            #endregion

            #region 药品列表

            ArrayList alDrug = new ArrayList(this.phaIntegrate.QueryItemList(true));
            if (alDrug == null)
            {
                MessageBox.Show(phaIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.dtDrug = new DataTable();

            //在myDataTable中定义列
            this.dtDrug.Columns.AddRange(new DataColumn[] {													
                                                    new DataColumn("名称",      dtStr),
                                                    new DataColumn("编码",      dtStr),
                                                    new DataColumn("规格",     dtStr),
													new DataColumn("类别",     dtStr),
													new DataColumn("单价",   dtStr),
													new DataColumn("单位",   dtStr),
													new DataColumn("拼音码",   dtStr),
													new DataColumn("五笔码",   dtStr),
													new DataColumn("项目编码",   dtStr)
											        });

            this.dtDrug.PrimaryKey = new DataColumn[] { this.dtDrug.Columns["项目编码"] };


            foreach (FS.HISFC.Models.Pharmacy.Item drugObj in alDrug)
            {
                if (drugObj.Type.ID=="P"&&drugObj.IsValid)
                {
                    dRow = dtDrug.NewRow();
                    dRow["名称"] = drugObj.Name;
                    dRow["编码"] = drugObj.UserCode;
                    dRow["规格"] = drugObj.Specs;
                    dRow["类别"] = drugObj.SysClass.Name;
                    dRow["单价"] = drugObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元";
                    dRow["单位"] = drugObj.PriceUnit;
                    dRow["拼音码"] = drugObj.SpellCode;
                    dRow["五笔码"] = drugObj.WBCode;
                    dRow["项目编码"] = drugObj.ID;
                    dtDrug.Rows.Add(dRow);
                }                
            }

            this.dtDrug.DefaultView.Sort = "编码";
            this.fpDrug_Sheet1.DataSource = this.dtDrug.DefaultView;

            this.fpDrug_Sheet1.Columns[6].Visible = false;
            this.fpDrug_Sheet1.Columns[7].Visible = false;
            this.fpDrug_Sheet1.Columns[8].Visible = false;

            #endregion
        }

        /// <summary>
        /// 初始化用法TreeView
        /// </summary>
        private void InitTree()
        {
            this.tvUsage.Nodes.Clear();
            TreeNode root = new TreeNode("用法");
            root.ImageIndex = 40;
            this.tvUsage.Nodes.Add(root);
            //获得用法列表
            if (al != null)
            {
                al = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            }

            if (al != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    TreeNode node = new TreeNode(obj.Name);
                    node.Tag = obj;
                    node.ImageIndex = 41;
                    root.Nodes.Add(node);
                }
                root.Expand();
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 初始化ToolBar
        /// </summary>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolbar.AddToolButton("删除", "删除数据", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            this.toolbar.AddToolButton("操作说明", "查看操作说明", FS.FrameWork.WinForms.Classes.EnumImageList.X信息, true, false, null);
            return this.toolbar;
        }

        private void ucSubtblManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.Init();

                this.neuListBox1.SelectedIndexChanged += new EventHandler(neuListBox1_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                if (this.fpSpread1_Sheet1.Rows.Count <= 0)
                    return;

                int row = this.fpSpread1_Sheet1.ActiveRowIndex;
                if (row < 0)
                    return;
                if (Delete(row) != -1)
                {
                    this.fpSpread1_Sheet1.Rows.Remove(row, 1);
                }
            }
            else if (e.ClickedItem.Text == "操作说明")
            {
                try
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\" + explainNote);
                }
                catch
                {
                    MessageBox.Show("找不到说明文档，请联系信息科！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private int Delete(int row)
        {
            if (fpSpread1_Sheet1.Rows[row].Locked)
            {
                MessageBox.Show("你没有删除该条项目的权限！\r\n如有疑问请联系信息科！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -1;
            }

            DialogResult Result = MessageBox.Show("确认删除该数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (Result != DialogResult.OK)
            {
                return -1;
            }

            FS.FrameWork.Models.NeuObject dept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

            OrderSubtblNew objSubtbl = new OrderSubtblNew();

            objSubtbl.ID = this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemCode].Text;

            objSubtbl.Qty = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text);

            objSubtbl.Oper.ID = this.fpSpread1_Sheet1.Cells[row , (int)SubtblColumns.Oper].Text;

            objSubtbl.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OperTime].Text);
            FS.HISFC.Models.Base.Item itemObj = (FS.HISFC.Models.Base.Item)itemHelper.GetObjectFromID(objSubtbl.ID);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if(this.subtblMgr.DeleteAdditionalItem(itemObj,dept.ID,false,this.currentUsage.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Refresh(this.currentUsage);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        private void tvPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = this.tvUsage.SelectedNode;

            if (current == null || current.Parent == null)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

                this.fpSpread1_Sheet1.Tag = null;
            }
            else
            {
                FS.FrameWork.Models.NeuObject usage = current.Tag as FS.FrameWork.Models.NeuObject;

                this.Refresh(usage);
            }
        }

        private void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (this.ucInputItem1.FeeItem == null)
            {
                return;
            }

            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("请先选择用法!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //if (!oper.IsManager)
            //{
            //    if (isAllowEdit == 0 ||
            //        (isAllowEdit == 2 &&
            //        SOC.HISFC.BizProcess.Cache.Common.GetDept(oper.Dept.ID).DeptType.ID.ToString() != "I"))
            //    {
            //        MessageBox.Show("没有维护的权限！");
            //        return;
            //    }
            //}

            //if (isCheckRepeatItem)
            //{
            //    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)//检查是否重复
            //    {
            //        FS.FrameWork.Models.NeuObject obj = this.fpSpread1.ActiveSheet.Rows[i].Tag as FS.FrameWork.Models.NeuObject;
            //        if (obj.Memo == this.ucInputItem1.FeeItem.ID)
            //        {
            //            MessageBox.Show("已存在项目" + this.ucInputItem1.FeeItem.Name + "请重新选择！");
            //            return;//如果重复 返回
            //        }
            //    }
            //}

            this.AddItemToFp(this.ucInputItem1.FeeItem, 0);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.ItemName)
                {
                    this.PopItem(this.alItems, (int)SubtblColumns.ItemName);
                }
            }
        }

        public override int Export(object sender, object neuObject)
        {
            ExportToExcel();
            return base.Export(sender, neuObject);
        }

        #endregion

        #region 方法

        private FS.FrameWork.Models.NeuObject GetDept()
        {
            if (neuListBox1.SelectedItem == null)
            {
                return oper.Dept;
            }
            return this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;
        }

        /// <summary>
        /// 添加项目到farpoint
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="row"></param>
        private void AddItemToFp(FS.FrameWork.Models.NeuObject Item, int row)
        {
            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("请先选择用法!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FS.HISFC.Models.Base.Item myItem = null;

            if (Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug) ||
                Item.GetType() == typeof(FS.HISFC.Models.Base.Item))
            {
                if (itemHelper != null && itemHelper.ArrayObject.Count > 0)
                {
                    myItem = itemHelper.GetObjectFromID(Item.ID) as FS.HISFC.Models.Base.Item;
                }
                if (myItem == null)
                {
                    myItem = this.feeMgr.GetItem(Item.ID);
                    if (myItem == null)
                    {
                        MessageBox.Show(feeMgr.Err);
                        return;
                    }
                }

                if (!myItem.IsValid)
                {
                    MessageBox.Show(myItem.Name + "已经停用，请重新选择！");
                    return;
                }

                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //判断该项目是否可用
                if (!myItem.IsValid)
                {
                    MessageBox.Show("项目" + myItem.Name + "已经停用！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //在fpSpread1_Sheet1中加入数据
                obj.ID = myItem.ID;
                obj.Name = myItem.Name;
                this.fpSpread1.ActiveSheet.Rows.Add(row, 1);

                this.fpSpread1_Sheet1.Rows[row].Tag = myItem;

                
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemCode].Text = myItem.ID;

                //项目名称
                if (!string.IsNullOrEmpty(myItem.Specs))
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Text = "【" + myItem.Specs + "】" + myItem.Name + "【" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Text = myItem.Name + "【" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                }
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Tag = myItem.ID;
                //数量
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text = "1";

                //单价
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.UnitPrice].Text = myItem.Price.ToString();

                //单位
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Unit].Text = myItem.PriceUnit;

                //操作员
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Oper].Text = (this.orderManager.Operator as FS.HISFC.Models.Base.Employee).ID;
                //操作时间
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OperTime].Text = this.orderManager.GetSysDateTime().ToString();

            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.ItemName)
                    {
                        this.PopItem(this.alItems, (int)SubtblColumns.ItemName);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 弹出常数选择
        /// </summary>
        private void PopItem(ArrayList al, int index)
        {
            if (fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Locked)
            {
                return;
            }

            if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.ItemName].Locked
                && index == (int)SubtblColumns.ItemName)
            {
                return;
            }

            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                //项目
                if (index == (int)SubtblColumns.ItemName)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.ItemName].Tag = info.ID;
                    FS.HISFC.Models.Base.Item itemObj = info as FS.HISFC.Models.Base.Item;

                    //附加项目编码
                    if (!string.IsNullOrEmpty(itemObj.Specs))
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.ItemName].Value = "【" + itemObj.Specs + "】" + itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.ItemName].Value = itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                    }
                    this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag = info;
                }
            }
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="person"></param>
        private void Refresh(FS.FrameWork.Models.NeuObject usage)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);
            }

            try
            {
                //this.tabPage1.Text = usage.Name;
                this.lblDisplay.Text = usage.Name;
                this.currentUsage = usage;
                FS.FrameWork.Models.NeuObject dept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

                FS.HISFC.Models.Base.Item itemObj = new FS.HISFC.Models.Base.Item();

                alItem.Clear();
                alItem = this.subtblMgr.QueryAdditionalItem(false, usage.ID, dept.ID);
                if (alItem == null)
                {
                    MessageBox.Show(this.subtblMgr.Err);
                    return;
                }

                this.fpSpread1_Sheet1.Tag = usage;
                if (alItem != null && alItem.Count > 0)
                {
                    foreach (FS.HISFC.Models.Base.Item obj in alItem)
                    {
                        itemObj = this.itemHelper.GetObjectFromID(obj.ID).Clone() as FS.HISFC.Models.Base.Item;
                        if (itemObj == null)
                        {
                            MessageBox.Show("查找项目失败：" + obj.Name + obj.ID);
                            break;
                        }

                        this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                        int row = this.fpSpread1_Sheet1.RowCount - 1;
                        this.fpSpread1_Sheet1.Rows[row].Tag = obj;

                        //项目编码
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemCode].Text = itemObj.ID;

                        if (!string.IsNullOrEmpty(itemObj.Specs))
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Text = "【" + itemObj.Specs + "】" + itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Text = itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                        }

                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.UnitPrice].Text = itemObj.Price.ToString();

                        //单位
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Unit].Text = itemObj.PriceUnit;

                        //数量
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text = obj.Qty.ToString();

                        //操作员
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Oper].Text = obj.User01;

                        //操作时间
                        this.fpSpread1_Sheet1.SetValue(row, (int)SubtblColumns.OperTime, obj.User02);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + this.subtblMgr.Err);
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            FS.FrameWork.Models.NeuObject usage = this.currentUsage as FS.FrameWork.Models.NeuObject;

            FS.FrameWork.Models.NeuObject dept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

            this.fpSpread1.StopCellEditing();

            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("请先选择项目!", "提示");
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //开始事务
            try
            {
                this.subtblMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Base.Item objSubtbl = null;
                //在全部循环插入
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    objSubtbl = new FS.HISFC.Models.Base.Item();

                    //项目编码
                    objSubtbl.ID = (this.fpSpread1_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject).ID;
                    //数量
                    objSubtbl.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Qty].Text);
                    //操作员					
                    objSubtbl.User01 = (this.orderManager.Operator as FS.HISFC.Models.Base.Employee).ID;
                    //操作时间
                    objSubtbl.User02 = this.subtblMgr.GetDateTimeFromSysDateTime().ToString(); ;
                    //每次量单位
                    objSubtbl.PriceUnit = this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Unit].Text;

     

                    #endregion
                    if (this.subtblMgr.InsertAdditionalItem(objSubtbl, dept.ID, false, this.currentUsage.ID, "", 0) == -1)
                    {
                        if (this.subtblMgr.UpdateAdditionalItem(objSubtbl, dept.ID, false, this.currentUsage.ID, "", 0) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.subtblMgr.Err, "提示");
                            return;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();

               
                alItem.Clear();
                alItem = this.subtblMgr.QueryAdditionalItem(false, usage.ID, dept.ID);
                this.Refresh(usage);
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return;
            }
            MessageBox.Show("保存成功!", "提示");
        }

        /// <summary>
        /// 获取现在的ID
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSelectData(int row, int column)
        {
            for (int j = 0; j < (this.fpSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items.Length; j++)
            {
                string item = (this.fpSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[j];

                if (item == this.fpSpread1_Sheet1.Cells[row, column].Text)
                {
                    return j.ToString();
                }
            }
            return "0";
        }


        /// <summary>
        /// 工具条:"导出"按钮处理程序
        /// </summary>
        private void ExportToExcel()
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有要导出的数据!"), "消息");

                return;
            }

            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.xls";
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.None);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #region 过滤

        /// <summary>
        /// 附材过滤
        /// </summary>
        private void SubFilter(object sender, EventArgs e)
        {
            string area = this.cmbFilterArea.Text;
            string orderType = this.cmbFilterOrderType.Text;
            string dept = this.cmbFilterDept.Text;
            string item = this.txtFilterItem.Text.Trim();

            //DataView dView = this.fpSpread1_Sheet1.DataSource;
        }

        #endregion


        /// <summary>
        /// 非药品过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.dtUndrug.DefaultView.RowFilter = "名称 like '%" + this.txtFilter_Undrug.Text.Trim() + "%' or 编码 like '%" +
                this.txtFilter_Undrug.Text.Trim() + "%' or 项目编码 like '%" + this.txtFilter_Undrug.Text.Trim() + "%' or 拼音码 like '%"
                + this.txtFilter_Undrug.Text.Trim() + "%' or 五笔码 like '%" + this.txtFilter_Undrug.Text.Trim() + "%'";
        }

        private void fpUndrug_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.Models.NeuObject undrugObj = new FS.FrameWork.Models.NeuObject();
            undrugObj.ID = this.fpUndrug_Sheet1.GetText(e.Row, 8);
            undrugObj.Name = this.fpUndrug_Sheet1.GetText(e.Row, 0);
            this.Refresh(undrugObj);
        }

        private void fpDrug_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.Models.NeuObject drugObj = new FS.FrameWork.Models.NeuObject();
            drugObj.ID = this.fpDrug_Sheet1.GetText(e.Row, 8);
            drugObj.Name = this.fpDrug_Sheet1.GetText(e.Row, 0);
            this.Refresh(drugObj);
        }

        private void txtFilter_Drug_TextChanged(object sender, EventArgs e)
        {
            this.dtDrug.DefaultView.RowFilter = "名称 like '%" + this.txtFilter_Drug.Text.Trim() + "%' or 编码 like '%" +
                this.txtFilter_Drug.Text.Trim() + "%' or 项目编码 like '%" + this.txtFilter_Drug.Text.Trim() + "%' or 拼音码 like '%"
                + this.txtFilter_Drug.Text.Trim() + "%' or 五笔码 like '%" + this.txtFilter_Drug.Text.Trim() + "%'";
        }

        private void neuListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh(this.currentUsage);
        }
    }

    /// <summary>
    /// 列设置
    /// </summary>
    public enum SubtblColumns
    {
        /// <summary>
        /// 项目代码
        /// </summary>
        ItemCode = 0,

        /// <summary>
        /// 项目名称，规格
        /// </summary>
        ItemName,

        /// <summary>
        /// 数量
        /// </summary>
        Qty,

        /// <summary>
        /// 单位
        /// </summary>
        Unit,

        /// <summary>
        /// 单价
        /// </summary>
        UnitPrice,

        /// <summary>
        /// 操作员
        /// </summary>
        Oper,

        /// <summary>
        /// 操作时间
        /// </summary>
        OperTime,

    }
}
