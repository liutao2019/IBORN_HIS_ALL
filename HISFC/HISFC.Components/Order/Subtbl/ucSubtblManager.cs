using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Order;

namespace FS.HISFC.Components.Order.Subtbl
{
    /// <summary>
    /// 华南附材算法维护
    /// </summary>
    public partial class ucSubtblManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSubtblManager()
        {
            InitializeComponent();
        }

        #region 系统层管理类
        FS.HISFC.BizLogic.Order.SubtblManager subtblMgr = new FS.HISFC.BizLogic.Order.SubtblManager();

        #endregion

        #region 变量

        ArrayList al = new ArrayList();

        /// <summary>
        /// 非药品项目
        /// </summary>
        private DataTable dtUndrug = null;

        /// <summary>
        /// 是否允许编辑修改 0 不允许（管理员除外);1 全部允许；2 根据登陆科室判断（目前只有登陆住院科室才能维护）
        /// </summary>
        private int isAllowEdit = 1;

        /// <summary>
        /// 是否允许编辑修改 0 不允许（管理员除外);1 全部允许；2 根据登陆科室判断（目前只有登陆住院科室才能维护）
        /// </summary>
        [Category("设置"), Description("是否允许编辑修改 0 不允许（管理员除外);1 全部允许；2 根据登陆科室判断（病区对应的医生站都可以）")]
        public int IsAllowEdit
        {
            get
            {
                return isAllowEdit;
            }
            set
            {
                isAllowEdit = value;
            }
        }

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
        /// 显示并可维护的类型（门诊、住院）
        /// </summary>
        private EnumType showType = EnumType.全部;

        /// <summary>
        /// 显示并可维护的类型（门诊、住院）
        /// </summary>
        [Category("设置"), Description("显示并可维护的类型（门诊、住院）")]
        public EnumType ShowType
        {
            get
            {
                return showType;
            }
            set
            {
                showType = value;
            }
        }

        /// <summary>
        /// 门诊范围默认科室全部
        /// </summary>
        private bool clinicAll = false;

        /// <summary>
        /// 显示并可维护的类型（门诊、住院）
        /// </summary>
        [Category("设置"), Description("门诊范围辅材科室是否默认为全部 false否true是")]
        public bool ClinicAll
        {
            get
            {
                return clinicAll;
            }
            set
            {
                clinicAll = value;
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
            //alDept = CacheManager.InterMgr.GetDepartment();
            alDept = SOC.HISFC.BizProcess.Cache.Common.GetDept();
            if (alDept == null)
            {
                MessageBox.Show(CacheManager.InterMgr.Err);
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

            alItems = new ArrayList(CacheManager.FeeIntegrate.QueryAllItemsList());
            if (alItems == null)
            {
                MessageBox.Show(CacheManager.FeeIntegrate.Err);
            }
            else
            {
                this.itemHelper.ArrayObject = alItems;
            }

            #region 下拉列表
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

            //适用范围：0 门诊；1 住院；2 全部
            string[] arrayTemp = new string[3] { "门诊", "住院", "全部" };
            comCellType1.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.Area].CellType = comCellType1;

            //医嘱类别：0 全部；1 长嘱；2 临嘱
            arrayTemp = new string[3] { "全部", "长嘱", "临嘱" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType5 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType5.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.OrderType].CellType = comCellType5;

            //组范围：0 每组收取、1 第一组收取、2 第二组起加收
            arrayTemp = new string[3] { "每组收取", "第一组收取", "第二组起加收" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType2.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.CombArea].CellType = comCellType2;

            //收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次
            arrayTemp = new string[8] { "×固定数量", "×最大院注次数", "×组内品种数", "×应执行次数", "×频次数", "×医嘱数量", "×天数", "×院注天数" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType3 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType3.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.FeeRule].CellType = comCellType3;

            //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
            arrayTemp = new string[3] { "不限制", "儿童", "成人" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType4 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType4.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.LimitType].CellType = comCellType4;

            #endregion

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

            ArrayList al = CacheManager.InterMgr.QueryDepartment(oper.Nurse.ID);
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

        void fpSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)SubtblColumns.Area)
            {
                if (this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.Area].Text == "门诊")
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Text = "全部";
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Locked = true;
                }
                else if (this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.Area].Text == "全部")
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Text = "全部";
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Locked = true;
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Locked = false;
                }
            }
        }

        /// <summary>
        /// 初始化带出项目列表
        /// </summary>
        private void InitItemFp()
        {
            #region 非药品
            ArrayList alUndrug = new ArrayList(CacheManager.FeeIntegrate.QueryValidItems());
            if (alUndrug == null)
            {
                MessageBox.Show(CacheManager.FeeIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            ArrayList alDrug = new ArrayList(CacheManager.PhaIntegrate.QueryItemList(true));
            if (alDrug == null)
            {
                MessageBox.Show(CacheManager.PhaIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                al = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE);
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

            OrderSubtblNew objSubtbl = new OrderSubtblNew();

            //适用范围：0 门诊；1 住院；3 全部
            objSubtbl.Area = this.GetSelectData(row, (int)SubtblColumns.Area);


            //医嘱类别：全部、长嘱、临嘱
            objSubtbl.OrderType = this.GetSelectData(row, (int)SubtblColumns.OrderType);

            //用法分类，0 药品按用法，1 非药品按项目代码
            objSubtbl.TypeCode = ((FS.FrameWork.Models.NeuObject)this.fpSpread1_Sheet1.Tag).ID;
            //科室代码，全院统一附材'ROOT'
            objSubtbl.Dept_code = this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag.ToString();
            //项目编码
            objSubtbl.Item.ID = (this.fpSpread1_Sheet1.Rows[row].Tag as FS.FrameWork.Models.NeuObject).ID;
            //数量
            objSubtbl.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text);
            //组范围：0 每组收取、1 第一组收取、2 第二组起加收
            objSubtbl.CombArea = this.GetSelectData(row, (int)SubtblColumns.CombArea);
            //收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次、*组内医嘱数量、*天数、*院注天数（院注次数/频次 上取整）
            objSubtbl.FeeRule = this.GetSelectData(row, (int)SubtblColumns.FeeRule);
            //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
            objSubtbl.LimitType = this.GetSelectData(row, (int)SubtblColumns.LimitType);
            //首次收取项目
            objSubtbl.FirstFeeFlag = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FirstFeeFlag].Text).ToString();
            //操作员					
            objSubtbl.Oper.ID = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).ID;
            //操作时间
            objSubtbl.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OperTime].Text);

            //是否重复收取
            objSubtbl.IsAllowReFee = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowReFee].Text);
            //是否弹出选择
            objSubtbl.IsAllowPopChose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowPopChose].Text);
            //是否每次量限制
            objSubtbl.IsCalculateByOnceDose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsCalculateByOnceDose].Text);
            //每次量单位
            objSubtbl.DoseUnit = this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.DoseUnit].Text;
            //每次量开始值
            objSubtbl.OnceDoseFrom = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseFrom].Text);
            //每次量结束值
            objSubtbl.OnceDoseTo = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseTo].Text);

            //扩展1
            objSubtbl.Extend1 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend1].Text) ? "1" : "0";
            //扩展2
            objSubtbl.Extend2 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend2].Text) ? "1" : "0";
            //扩展3
            objSubtbl.Extend3 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend3].Text) ? "1" : "0";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //先删除 后插入
            if (this.subtblMgr.DelSubtblInfo(objSubtbl) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.fpSpread1.ShowRow(0, row, FarPoint.Win.Spread.VerticalPosition.Center);
                MessageBox.Show(this.subtblMgr.Err, "提示");
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
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.Dept)
                {
                    this.PopItem(this.deptHelper.ArrayObject, (int)SubtblColumns.Dept);
                }
                else if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.Item)
                {
                    this.PopItem(this.alItems, (int)SubtblColumns.Item);
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
                    myItem = CacheManager.FeeIntegrate.GetItem(Item.ID);
                    if (myItem == null)
                    {
                        MessageBox.Show(CacheManager.FeeIntegrate.Err);
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

                //适用范围：0 门诊；1 住院；3 全部
                if (showType == EnumType.全部
                    || oper.IsManager)
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Area].Text = "全部";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Area].Text = showType.ToString();
                }

                //医嘱类别：全部、长嘱、临嘱
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OrderType].Text = "全部";

                if (!oper.IsManager)
                {
                    if (ClinicAll && showType == EnumType.门诊)
                    {
                        //科室代码，全院统一附材'ROOT'
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Text = "全部";
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag = "ROOT";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Text = this.GetDept().Name;
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag = GetDept().ID;
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Locked = true;
                    }
                }
                else
                {
                    //科室代码，全院统一附材'ROOT'
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Text = "全部";
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag = "ROOT";
                }

                //附加项目编码
                if (!string.IsNullOrEmpty(myItem.Specs))
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Text = "【" + myItem.Specs + "】" + myItem.Name + "【" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Text = myItem.Name + "【" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                }
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Tag = myItem.ID;
                //数量
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text = "1";
                //组范围：0 每组收取、1 第一组收取、2 第二组起加收
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.CombArea].Text = "每组收取";
                //收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次、*组内医嘱数量、*天数、*院注天数（院注次数/频次 上取整）
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FeeRule].Text = "×固定数量";
                //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.LimitType].Text = "不限制";
                //首次收取项目
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FirstFeeFlag].Value = FS.FrameWork.Function.NConvert.ToInt32(0);
                //操作员
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Oper].Text = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).Name;
                //操作时间
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OperTime].Text = CacheManager.OutOrderMgr.GetSysDateTime().ToString();

                //是否重复收取
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowReFee].Value = true;
                //是否弹出选择
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowPopChose].Value = false;
                //是否每次量限制
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsCalculateByOnceDose].Value = false;
                //每次量单位
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.DoseUnit].Text = "";
                //每次量开始值
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseFrom].Text = "";
                //每次量结束值
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseTo].Text = "";


                //扩展1
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend1].Value = false;
                //扩展2
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend2].Value = false;
                //扩展3
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend3].Value = false;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.Dept)
                    {
                        this.PopItem(this.deptHelper.ArrayObject, (int)SubtblColumns.Dept);
                    }
                    else if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.Item)
                    {
                        this.PopItem(this.alItems, (int)SubtblColumns.Item);
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

            if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Dept].Locked
                && index == (int)SubtblColumns.Dept)
            {
                return;
            }
            if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Item].Locked
                && index == (int)SubtblColumns.Item)
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
                //科室
                if (index == (int)SubtblColumns.Dept)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Dept].Tag = info.ID;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Dept].Value = info.Name;
                }
                //项目
                else if (index == (int)SubtblColumns.Item)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Item].Tag = info.ID;
                    FS.HISFC.Models.Base.Item itemObj = info as FS.HISFC.Models.Base.Item;

                    //附加项目编码
                    if (!string.IsNullOrEmpty(itemObj.Specs))
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Item].Value = "【" + itemObj.Specs + "】" + itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Item].Value = itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍后……");
            Application.DoEvents();

            try
            {
                //this.tabPage1.Text = usage.Name;
                this.lblDisplay.Text = usage.Name;
                this.currentUsage = usage;

                FS.HISFC.Models.Base.Item itemObj = new FS.HISFC.Models.Base.Item();

                alItem.Clear();
                alItem = this.subtblMgr.GetSubtblInfo("2", usage.ID, "ROOT");
                if (alItem == null)
                {
                    MessageBox.Show(this.subtblMgr.Err);
                    return;
                }

                this.fpSpread1_Sheet1.Tag = usage;
                if (alItem != null && alItem.Count > 0)
                {
                    foreach (OrderSubtblNew obj in alItem)
                    {
                        itemObj = this.itemHelper.GetObjectFromID(obj.Item.ID) as FS.HISFC.Models.Base.Item;
                        if (itemObj == null)
                        {
                            MessageBox.Show("查找项目失败：" + obj.Item.Name + obj.Item.ID);
                            break;
                        }

                        this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                        int row = this.fpSpread1_Sheet1.RowCount - 1;
                        this.fpSpread1_Sheet1.Rows[row].Tag = obj.Item;

                        //范围
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Area].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.Area].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.Area)];
                        //医嘱类别：全部、长嘱、临嘱
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OrderType].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.OrderType].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.OrderType)];
                        //科室
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Text = this.deptHelper.GetName(obj.Dept_code);
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag = obj.Dept_code;

                        if (SOC.HISFC.BizProcess.Cache.Common.GetDept(obj.Dept_code) != null
                            && SOC.HISFC.BizProcess.Cache.Common.GetDept(obj.Dept_code).ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            fpSpread1_Sheet1.Rows[row].BackColor = Color.OrangeRed;
                        }

                        //项目
                        //this.fpSpread1_Sheet1.Cells[row, 2].Text = itemObj.Name + "【" + itemObj.Specs + "】";
                        if (!string.IsNullOrEmpty(itemObj.Specs))
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Text = "【" + itemObj.Specs + "】" + itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Text = itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                        }

                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Tag = obj.Item.ID;
                        //数量
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text = obj.Qty.ToString();

                        //规则：组范围
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.CombArea].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.CombArea].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.CombArea)];
                        //规则：收取规则
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FeeRule].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.FeeRule].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.FeeRule)];
                        //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.LimitType].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.LimitType].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.LimitType)];

                        //首次收取项目
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FirstFeeFlag].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.FirstFeeFlag);


                        //操作员
                        this.fpSpread1_Sheet1.SetValue(row, (int)SubtblColumns.Oper, CacheManager.InterMgr.GetEmployeeInfo(obj.Oper.ID).Name, false);
                        //操作时间
                        this.fpSpread1_Sheet1.SetValue(row, (int)SubtblColumns.OperTime, obj.OperDate);

                        //是否重复收取
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowReFee].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.IsAllowReFee);
                        //是否弹出选择
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowPopChose].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.IsAllowPopChose);
                        //是否每次量限制
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsCalculateByOnceDose].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.IsCalculateByOnceDose);
                        //每次量单位
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.DoseUnit].Text = obj.DoseUnit;
                        //每次量开始值
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseFrom].Text = obj.OnceDoseFrom.ToString();
                        //每次量结束值
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseTo].Text = obj.OnceDoseTo.ToString();


                        //扩展1
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend1].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.Extend1);
                        //扩展2
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend2].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.Extend2);
                        //扩展3
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend3].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.Extend3);

                        if (!oper.IsManager)
                        {
                            fpSpread1_Sheet1.Columns[(int)SubtblColumns.Area].Locked = true;

                            if (!isAllDeptShow)
                            {
                                if (obj.Dept_code != this.GetDept().ID
                                    && obj.Dept_code != "ROOT")
                                {
                                    fpSpread1_Sheet1.Rows[row].Visible = false;
                                }
                            }

                            if (isAllowEdit == 0 ||
                                (isAllowEdit == 2 && !IDepartmentList.Contains(obj.Dept_code)))
                            {
                                fpSpread1_Sheet1.Rows[row].Locked = true;
                                fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Locked = true;
                                fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Locked = true;
                            }
                            else
                            {
                                fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Locked = false;
                            }

                            if (showType != EnumType.全部)
                            {
                                if (fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Area].Text != showType.ToString())
                                {
                                    fpSpread1_Sheet1.Rows[row].Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message + this.subtblMgr.Err);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
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

                //先全部删除
                if (alItem.Count > 0)
                {
                    if (this.subtblMgr.DelSubtblInfo((this.fpSpread1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.subtblMgr.Err, "提示");
                        return;
                    }
                }

                OrderSubtblNew objSubtbl = null;
                //在全部循环插入
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    objSubtbl = new OrderSubtblNew();

                    //适用范围：0 门诊；1 住院；3 全部
                    objSubtbl.Area = this.GetSelectData(i, (int)SubtblColumns.Area);


                    //医嘱类别：全部、长嘱、临嘱
                    objSubtbl.OrderType = this.GetSelectData(i, (int)SubtblColumns.OrderType);

                    //用法分类，0 药品按用法，1 非药品按项目代码
                    objSubtbl.TypeCode = ((FS.FrameWork.Models.NeuObject)this.fpSpread1_Sheet1.Tag).ID;
                    //科室代码，全院统一附材'ROOT'
                    objSubtbl.Dept_code = this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Dept].Tag.ToString();
                    //项目编码
                    objSubtbl.Item.ID = (this.fpSpread1_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject).ID;
                    //数量
                    objSubtbl.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Qty].Text);
                    //组范围：0 每组收取、1 第一组收取、2 第二组起加收
                    objSubtbl.CombArea = this.GetSelectData(i, (int)SubtblColumns.CombArea);
                    //收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次、*组内医嘱数量、*天数、*院注天数（院注次数/频次 上取整）
                    objSubtbl.FeeRule = this.GetSelectData(i, (int)SubtblColumns.FeeRule);
                    //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                    objSubtbl.LimitType = this.GetSelectData(i, (int)SubtblColumns.LimitType);
                    //首次收取项目
                    objSubtbl.FirstFeeFlag = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.FirstFeeFlag].Text).ToString();
                    //操作员					
                    objSubtbl.Oper.ID = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).ID;
                    //操作时间
                    objSubtbl.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.OperTime].Text);

                    //是否重复收取
                    objSubtbl.IsAllowReFee = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.IsAllowReFee].Text);
                    //是否弹出选择
                    objSubtbl.IsAllowPopChose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.IsAllowPopChose].Text);
                    //是否每次量限制
                    objSubtbl.IsCalculateByOnceDose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.IsCalculateByOnceDose].Text);
                    //每次量单位
                    objSubtbl.DoseUnit = this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.DoseUnit].Text;
                    //每次量开始值
                    objSubtbl.OnceDoseFrom = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.OnceDoseFrom].Text);
                    //每次量结束值
                    objSubtbl.OnceDoseTo = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.OnceDoseTo].Text);

                    //扩展1
                    objSubtbl.Extend1 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Extend1].Text) ? "1" : "0";
                    //扩展2
                    objSubtbl.Extend2 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Extend2].Text) ? "1" : "0";
                    //扩展3
                    objSubtbl.Extend3 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Extend3].Text) ? "1" : "0";


                    #region 验证数据

                    if (objSubtbl.Area != "0" && (objSubtbl.FeeRule == "1" || objSubtbl.FeeRule == "6"))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.fpSpread1.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                        MessageBox.Show("院注次数或天数只能用于门诊！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (objSubtbl.Area != "1" && objSubtbl.OrderType != "0")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.fpSpread1.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                        MessageBox.Show("非住院类别，医嘱类别请选择为“全部”！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    #endregion

                    //先删除 后插入
                    //if (this.subtblMgr.DelSubtblInfo(objSubtbl) == -1)
                    //{
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    this.fpSpread1.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                    //    MessageBox.Show(this.subtblMgr.Err, "提示");
                    //    return;
                    //}

                    if (this.subtblMgr.InsertSubtblInfo(objSubtbl) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.fpSpread1.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                        MessageBox.Show(this.subtblMgr.Err, "提示");
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                FS.FrameWork.Models.NeuObject usage = this.currentUsage as FS.FrameWork.Models.NeuObject;
                alItem.Clear();
                alItem = this.subtblMgr.GetSubtblInfo("2", usage.ID, "ROOT");
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
        /// 范围：门诊、住院
        /// </summary>
        Area = 0,

        /// <summary>
        /// 医嘱类别：全部、长嘱、临嘱
        /// </summary>
        OrderType,

        /// <summary>
        /// 科室代码，全院统一附材'ROOT'
        /// </summary>
        Dept,

        /// <summary>
        /// 附加项目编码
        /// </summary>
        Item,

        /// <summary>
        /// 数量
        /// </summary>
        Qty,

        /// <summary>
        /// 组范围：0 每组收取、1 第一组收取、2 第二组起加收
        /// </summary>
        CombArea,

        /// <summary>
        /// 收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次、*组内医嘱数量、*天数、*院注天数（院注次数/频次 上取整）
        /// </summary>
        FeeRule,

        /// <summary>
        /// 限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
        /// </summary>
        LimitType,

        /// <summary>
        /// 首次收取项目
        /// </summary>
        FirstFeeFlag,

        /// <summary>
        /// 是否允许重复收取
        /// </summary>
        IsAllowReFee,

        /// <summary>
        /// 是否允许弹出选择
        /// </summary>
        IsAllowPopChose,

        /// <summary>
        /// 是否根据每次量限制带出
        /// </summary>
        IsCalculateByOnceDose,

        /// <summary>
        /// 每次量单位
        /// </summary>
        DoseUnit,

        /// <summary>
        /// 每次量开始值
        /// </summary>
        OnceDoseFrom,

        /// <summary>
        /// 每次量结束值
        /// </summary>
        OnceDoseTo,

        /// <summary>
        /// 操作员
        /// </summary>
        Oper,

        /// <summary>
        /// 操作时间
        /// </summary>
        OperTime,

        /// <summary>
        /// 扩展1 妇幼用于药袋打印
        /// </summary>
        Extend1,

        /// <summary>
        /// 扩展2
        /// </summary>
        Extend2,

        /// <summary>
        /// 扩展3
        /// </summary>
        Extend3
    }

    public enum EnumType
    {
        全部,
        门诊,
        住院
    }
}
