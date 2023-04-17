using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    /// <summary>
    /// [功能描述: 药品模板维护]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-04]<br></br>
    /// </summary>
    public partial class ucStencilManager : Base.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucStencilManager()
        {
            InitializeComponent();
        }

        private FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail curIDataDetail = null;
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList curIDataChooseList = null;
        private FS.SOC.HISFC.Components.Common.Base.baseTreeView curTreeView = null;
        private FS.FrameWork.Models.NeuObject curPriveType = null;
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting curChooseDataSetting = null;

        private FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
        private FS.SOC.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

        private decimal totRowQty = 0;
        private System.Data.DataTable dtDetail = null;
        private Hashtable hsStencil = new Hashtable();
        private string settingFileName = "";

        private FS.HISFC.Models.Pharmacy.DrugStencil curStencil = new FS.HISFC.Models.Pharmacy.DrugStencil();

        private FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();

        #region 属性及所用变量

        /// <summary>
        /// 模板定义类型
        /// </summary>
        public enum enumPriveCheckType
        {
            /// <summary>
            /// 不检测
            /// </summary>
            No,
            /// <summary>
            /// 检测科室
            /// </summary>
            Dept,
            /// <summary>
            /// 存在任意科室权限即可
            /// </summary>
            Exits
        };

        private FS.HISFC.Models.Pharmacy.EnumDrugStencil curStencilType = FS.HISFC.Models.Pharmacy.EnumDrugStencil.Plan;

        private enumPriveCheckType curPriveCheckType = enumPriveCheckType.Dept;

        /// <summary>
        /// 权限检测方式
        /// </summary>
        [Description("权限检测方式"), Category("设置"), Browsable(true)]
        public enumPriveCheckType PriveCheckType
        {
            get { return curPriveCheckType; }
            set { curPriveCheckType = value; }
        }
        
        /// <summary>
        /// 模板类型
        /// </summary>
        [Description("模板类型"), Category("设置"), Browsable(true)]
        public FS.HISFC.Models.Pharmacy.EnumDrugStencil StencilType
        {
            get { return curStencilType; }
            set { curStencilType = value; }
        }

        private string customStencilTypeNO = "Custom";
        private string customStencilTypeName = "自定义模板";

        /// <summary>
        /// 自定义模板类型
        /// </summary>
        [Description("自定义模板类型名称"), Category("设置"), Browsable(true)]
      
        public string CustomStencilTypeName
        {
            get { return customStencilTypeName; }
            set { customStencilTypeName = value; }
        }

        /// <summary>
        /// 自定义模板类型
        /// </summary>
        [Description("自定义模板类型编码"), Category("设置"), Browsable(true)]
        public string CustomStencilTypeNO
        {
            get { return customStencilTypeNO; }
            set { customStencilTypeNO = value; }
        }

        /// <summary>
        /// 权限
        /// </summary>
        [Description("权限"), Category("设置"),Browsable(true)]
        public FS.FrameWork.Models.NeuObject PriveType
        {
            get
            {
                if (curPriveType == null)
                {
                    curPriveType = new FS.FrameWork.Models.NeuObject();
                    curPriveType.ID = "01";
                    curPriveType.Memo = "01";
                    curPriveType.Name = "药品维护权限0360";
                }
                return curPriveType; 
            }
            //set
            //{ 
            //    curPriveType = value;
            //}
        }

        private bool unique = true;

        /// <summary>
        /// 唯一性约束
        /// </summary>
        [Description("同模板类型内药品唯一性约束"), Category("设置"), Browsable(true)]
        public bool Unique
        {
            get { return unique; }
            set { unique = value; }
        }

        #endregion

        #region 初始化

        protected int Init()
        {
            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\StencilSetting.xml";

            int param = this.InitDataTable();

            if (param == -1)
            {
                return param;
            }

            param = this.InitChooseData();
            if (param == -1)
            {
                return param;
            }

            param = this.InitDataDetail();

            if (param == -1)
            {
                return param;
            }

            param = this.InitTreeView();

            return param;
        }

        /// <summary>
        /// 初始化数据选择关键属性
        /// </summary>
        /// <returns></returns>
        protected int InitChooseData()
        {
            string errInfo = "";
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = Function.GetBizChooseDataSetting("0360", curPriveType.Memo, curPriveType.ID, "0", ref errInfo);
            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Maintenance.StencilPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Maintenance.StencilPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
                string SQLWhere = "";
                if (this.Unique)
                {
                    //唯一性约束是在同类型的模板中同一药品只能出现一次
                    //如果某个药品不在此类型的模板中维护，则可以加载到选择列表，否则不加载
                    if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Maintenance.StencilPrive.ChooseWhere.Type", ref SQLWhere) == -1)
                    {
                        Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Maintenance.StencilPrive.ChooseWhere.Type,请与系统管理员联系！", MessageBoxIcon.Error);
                        return -1;
                    }
                    string stencilTypeNO = this.StencilType.ToString();
                    if (stencilTypeNO == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom.ToString())
                    {
                        stencilTypeNO = this.CustomStencilTypeNO;
                    }
                    string deptNO = this.priveDept.ID;
                    if (deptNO == "")
                    {
                        deptNO = "All";
                    }
                    SQLWhere = string.Format(SQLWhere, stencilTypeNO, deptNO);
                }
                else
                {
                    if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Maintenance.StencilPrive.ChooseWhere.Stencil", ref SQLWhere) == -1)
                    {
                        Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Maintenance.StencilPrive.ChooseWhere.Stencil,请与系统管理员联系！", MessageBoxIcon.Error);
                        return -1;
                    }

                    string deptNO = this.priveDept.ID;
                    if (deptNO == "")
                    {
                        deptNO = "All";
                    }
                    SQLWhere = string.Format(SQLWhere, "{1}", deptNO);
                }
                SQL = SQL + SQLWhere;

                chooseDataSetting.ListTile = "药品列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0 };
                chooseDataSetting.Filter =
                    "trade_name like '%{0}%'"
                + " or custom_code like '%{0}%'"
                + " or spell_code like '%{0}%'"
                + " or regular_spell like '%{0}%'"
                + " or regular_wb like '%{0}%'";

                chooseDataSetting.ColumnLabels = new string[] { 
                    "药品编码", 
                    "名称", 
                    "规格", 
                    "购入价", 
                    "零售价", 
                    "单位", 
                    "自定义码",
                    "拼音码", 
                    "五笔码", 
                    "通用名", 
                    "通用名拼音码", 
                    "通用名五笔码",
                    "通用名自定义码"
                };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   0f,// "药品编码", 
                   120f,// "名称", 
                   100f,// "规格", 
                   40f,// "购入价", 
                   40f,// "零售价", 
                   15f,// "单位", 
                   0f,// "自定义码",
                   0f,// "拼音码", 
                   0f,// "五笔码", 
                   0f,// "通用名", 
                   0f,// "通用名拼音码", 
                   0f,// "通用名五笔码"
                   0f// "通用名自定义码"
                };
                chooseDataSetting.IsNeedDrugType = true;

                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.ReadOnly = true;
                n.DecimalPlaces = 4;

                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "药品编码", 
                   t,// "名称", 
                   t,// "规格", 
                   n,// "购入价", 
                   n,// "零售价", 
                   t,// "单位", 
                   t,// "自定义码",
                   t,// "拼音码", 
                   t,// "五笔码", 
                   t,// "通用名", 
                   t,// "通用名拼音码", 
                   t,// "通用名五笔码"
                   t// "通用名自定义码"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\StencilL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curIDataChooseList.SettingFileName = curChooseDataSetting.SettingFileName;
                if (this.Unique)
                {
                    //唯一性约束是在同类型的模板中同一药品只能出现一次
                    //如果某个药品不在此类型的模板中维护，则可以加载到选择列表，否则不加载
                    this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, curChooseDataSetting.SQL, curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
                    this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);
                }
                this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
                this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
            }

            return 1;

        }

        /// <summary>
        /// 初始化入库明细数据的DataTable
        /// 决定FarPoint的列名称
        /// 修改列时注意明细数据显示的FarPoint初始化函数InitDetailDataFarPoint保持一致
        /// 修改列时注意设置过滤字符串的函数InitDataDetailUC保持一致
        /// 修改列时注意向明细数据显示的DataTable添加数据时的函数AddInputObjectToDataTable保持一致
        /// 请保证主键在最后一列
        /// </summary>
        /// <returns></returns>
        protected int InitDataTable()
        {
            if (this.dtDetail == null)
            {
                this.dtDetail = new System.Data.DataTable();
            }

            this.dtDetail.Columns.AddRange
                (
                new System.Data.DataColumn[]
                {
                    
                    new DataColumn("自定义码",typeof(string)),
                    new DataColumn("药品编码",typeof(string)),
                    new DataColumn("名称",typeof(string)),
                    new DataColumn("规格",typeof(string)),
                    new DataColumn("扩展信息",typeof(string)),
                    new DataColumn("序号",typeof(string)),
                    new DataColumn("应用科室",typeof(string)),
                    new DataColumn("模板类型",typeof(string)),
                    new DataColumn("模板编号",typeof(string)),
                    new DataColumn("模板名称",typeof(string)),
                    new DataColumn("操作人",typeof(string)),
                    new DataColumn("操作时间",typeof(string)),
                    new DataColumn("拼音码",typeof(string)),
                    new DataColumn("五笔码",typeof(string)),
                    new DataColumn("主键",typeof(string)),

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "模板名称" || dc.ColumnName == "扩展信息" || dc.ColumnName == "序号")
                {
                    continue;
                }
                dc.ReadOnly = true;
            }


            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtDetail.Columns["主键"];

            this.dtDetail.PrimaryKey = keys;

            this.dtDetail.DefaultView.AllowNew = true;
            this.dtDetail.DefaultView.AllowEdit = true;
            this.dtDetail.DefaultView.AllowDelete = true;

            return 1;
        }

        /// <summary>
        /// 初始化入库明细数据控件(接口)
        /// 必须在InitDataTable之后调用
        /// </summary>
        /// <returns></returns>
        protected int InitDataDetail()
        {
            if (this.curIDataDetail == null)
            {
                Function.ShowMessage("程序发生错误：明细数据显示控件没有初始化", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            this.curIDataDetail.Init();
            this.curIDataDetail.Filter = "自定义码 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码 like '%{0}%' ";
            if (this.curIDataDetail.FpSpread != null && this.curIDataDetail.FpSpread.Sheets.Count > 0 && this.dtDetail != null)
            {
                this.curIDataDetail.FpSpread.Sheets[0].DataSource = this.dtDetail.DefaultView;
                this.curIDataDetail.SettingFileName = this.settingFileName;
                this.InitFarPoint();
            }
            this.curIDataDetail.Info = "";
            return 1;
        }

        /// <summary>
        /// 初始化设置明细数据的FarPoint
        /// </summary>
        /// <returns></returns>
        protected int InitFarPoint()
        {
            //配置文件在过滤后恢复FarPoint格式用
            if (!System.IO.File.Exists(this.settingFileName))
            {
                this.curIDataDetail.FpSpread.SetColumnWith(0, "应用科室", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "模板类型", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "模板编号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "模板名称", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "自定义码", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "药品编码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "名称", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 90f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "拼音码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "五笔码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "主键", 0f);

                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nPrice = new FarPoint.Win.Spread.CellType.NumberCellType();
                nPrice.DecimalPlaces = 4;
                nPrice.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty.DecimalPlaces = 0;
                nQty.ReadOnly = true;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "拼音码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "五笔码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "主键", t);

                this.curIDataDetail.FpSpread.SaveSchema(this.settingFileName);
            }
            else
            {
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            }

            return 1;
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <returns></returns>
        protected int InitTreeView()
        {
            this.curTreeView.Nodes.Clear();
            this.curTreeView.Visible = true;
            this.toolBarService.SetToolButtonEnabled("列表", false);

            this.curTreeView.AfterSelect -= new TreeViewEventHandler(curTreeView_AfterSelect);
            this.curTreeView.AfterSelect += new TreeViewEventHandler(curTreeView_AfterSelect);

            this.curTreeView.DoubleClick -= new EventHandler(curTreeView_DoubleClick);
            this.curTreeView.DoubleClick += new EventHandler(curTreeView_DoubleClick);

            return 1;
        }

        #endregion

        #region 显示列表及明细数据
        protected int ShowList()
        {
            this.Clear();

            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            //科室为All的表示是所用科室通用的
            string deptNO = "All";
            if (!string.IsNullOrEmpty(this.priveDept.ID))
            {
                deptNO = this.priveDept.ID;
            }

            //有自定义类别的模板
            string stencilType = this.StencilType.ToString();
            if (this.StencilType == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom)
            {
                stencilType = this.customStencilTypeNO;
            }

            ArrayList alList = this.constantMgr.QueryDrugStencilList(deptNO, stencilType);
            if (alList == null)
            {
                Function.ShowMessage("获取本科室模版列表发生错误" + this.constantMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            TreeNode parentNode = new TreeNode();
            if (this.StencilType == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom)
            {
                parentNode.Text = this.CustomStencilTypeName;
            }
            else
            {
                if (this.StencilType == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Apply)
                {
                    parentNode.Text = "入库申请模板";
                }
                else if (this.StencilType == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Check)
                {
                    parentNode.Text = "盘点模板";
                }
                else if (this.StencilType == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Plan)
                {
                    parentNode.Text = "采购计划模板";
                }
            }
            parentNode.ImageIndex = 3;
            parentNode.SelectedImageIndex = 3;
            this.curTreeView.Nodes.Add(parentNode);

            foreach (FS.HISFC.Models.Pharmacy.DrugStencil info in alList)
            {
                TreeNode node = new TreeNode(info.Stencil.Name + "[" + info.Stencil.ID + "]");
                node.ImageIndex = 4;
                node.SelectedImageIndex = 4;
                node.Tag = info;
                parentNode.Nodes.Add(node);
            }

            this.curTreeView.ExpandAll();

            this.curTreeView.Visible = true;

            return 1;
        }

        protected int ShowDetail(string stencilNO)
        {
            if (stencilNO == null || stencilNO == "")
            {
                return -1;
            }

            ArrayList al = this.constantMgr.QueryDrugStencil(stencilNO);
            if (al == null)
            {
                Function.ShowMessage("根据模版编码获取模版明细信息发生错误\n" + this.constantMgr.Err,MessageBoxIcon.Error);
                return -1;
            }

            this.hsStencil.Clear();
            this.dtDetail.Rows.Clear();
            this.totRowQty = 0;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在显示模版明细 请稍候...");
            Application.DoEvents();

            ((System.ComponentModel.ISupportInitialize)(this.curIDataDetail.FpSpread)).BeginInit();

            foreach (FS.HISFC.Models.Pharmacy.DrugStencil info in al)
            {
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (item != null)
                {
                    info.Item = item;
                }
                if (this.AddObjectToDataTable(info) == -1)
                {
                    continue;
                }
            }

            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            ((System.ComponentModel.ISupportInitialize)(this.curIDataDetail.FpSpread)).EndInit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }
        #endregion

        #region DataTable数据添加

        /// <summary>
        /// 向DataTable添加入库实体，显示入库明细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected int AddObjectToDataTable(FS.HISFC.Models.Pharmacy.DrugStencil stencil)
        {
            if (stencil == null)
            {
                Function.ShowMessage("向DataTable中添加模板信息失败：模板信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtDetail == null)
            {
                Function.ShowMessage("向DataTable中添加模板信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            if (this.hsStencil.Contains(stencil.Item.ID))
            {
                Function.ShowMessage("" + stencil.Item.Name + " 已经重复，请检查是否正确！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsStencil.Add(stencil.Item.ID, stencil);
            }

            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0");
            }

            DataRow row = this.dtDetail.NewRow();

            row["自定义码"] = stencil.Item.UserCode;
            row["药品编码"] = stencil.Item.ID;
            row["名称"] = stencil.Item.Name;
            row["规格"] = stencil.Item.Specs;
            row["拼音码"] = stencil.Item.SpellCode;
            row["五笔码"] = stencil.Item.WBCode;
            row["主键"] = stencil.Item.ID;

            if (!string.IsNullOrEmpty(stencil.Dept.ID) && stencil.Dept.ID != "All")
            {
                row["应用科室"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stencil.Dept.ID);
            }
            else
            {
                row["应用科室"] = "全部";
            }
            stencil.Dept.Name = row["应用科室"].ToString();

            if (stencil.OpenType.ID == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom.ToString())
            {
                row["模板类型"] = stencil.Stencil.Memo;
            }
            else
            {
                row["模板类型"] = stencil.OpenType.ID;
            }
            row["模板编号"] = stencil.Stencil.ID;
            row["模板名称"] = stencil.Stencil.Name;
            row["扩展信息"] = stencil.Extend;
            row["序号"] = stencil.SortNO;
            row["操作人"] = stencil.Oper.ID;
            row["操作时间"] = stencil.Oper.OperTime;

            this.dtDetail.Rows.Add(row);

            return 1;
        }

        #endregion

        #region 数据选择列表刷新
        protected int FreshChooseData()
        {
            if (this.curChooseDataSetting == null)
            {
                return 0;
            }

            this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL, "{0}", this.curStencil.Stencil.ID), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
            this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);

            return 1;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected int Save()
        {
            this.dtDetail.DefaultView.RowFilter = "11=11";
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            if (this.dtDetail.Rows.Count <= 0)
            {
                return 0;
            }

            this.curIDataDetail.FpSpread.StopCellEditing();

            for (int i = 0; i < this.dtDetail.DefaultView.Count; i++)
            {
                this.dtDetail.DefaultView[i].EndEdit();
            }


            //模板编码，名称
            this.curStencil.Stencil.Name = dtDetail.Rows[0]["模板名称"].ToString();
            foreach (DataRow dr in dtDetail.Rows)
            {
                if (dr["模板名称"].ToString() != this.curStencil.Stencil.Name)
                {
                    dr["模板名称"] = this.curStencil.Stencil.Name;
                }
            }

            if (string.IsNullOrEmpty(this.curStencil.Stencil.Name))
            {
                Function.ShowMessage("请在第一行数据中输入模板名称!", MessageBoxIcon.Information);
                return 0;
            }

            string key = dtDetail.Rows[0]["主键"].ToString();
            FS.HISFC.Models.Pharmacy.DrugStencil firstDrugStencil = hsStencil[key] as FS.HISFC.Models.Pharmacy.DrugStencil;

            if (firstDrugStencil.Dept.ID != this.priveDept.ID)
            {
                DialogResult rs = MessageBox.Show("您选择的是【" + this.priveDept.Name + "】，但是当前药品是【" + firstDrugStencil.Dept.Name + "】的模板，是否复制？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                {
                    return 0;
                }
                this.curStencil.Stencil.ID = "";
            }
            if (string.IsNullOrEmpty(this.curStencil.Stencil.ID))
            {
                this.curStencil.Stencil.ID = this.constantMgr.GetNewStencilNO();
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存模版 请稍候...");
            Application.DoEvents();

            DataTable dtChange = this.dtDetail.GetChanges(System.Data.DataRowState.Modified | System.Data.DataRowState.Added);
            if (dtChange != null && dtChange.Rows.Count > 0)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                foreach (DataRow dr in dtChange.Rows)
                {

                    FS.HISFC.Models.Pharmacy.DrugStencil drugStencil = new FS.HISFC.Models.Pharmacy.DrugStencil();

                    drugStencil.Dept = this.priveDept;
                    if (string.IsNullOrEmpty(drugStencil.Dept.ID) && this.PriveCheckType == enumPriveCheckType.Exits)
                    {
                        drugStencil.Dept.ID = "All";
                    }
                    if (this.StencilType == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom)
                    {
                        drugStencil.OpenType.ID = this.CustomStencilTypeNO;
                    }
                    else
                    {
                        drugStencil.OpenType.ID = this.StencilType.ToString();
                    }
                    drugStencil.Stencil = this.curStencil.Stencil;

                    if (string.IsNullOrEmpty(drugStencil.Stencil.Name))
                    {
                        drugStencil.Stencil.Name = "未命名";
                    }

                    drugStencil.Item.ID = dr["药品编码"].ToString();
                    drugStencil.Item.Name = dr["名称"].ToString();
                    drugStencil.Item.Specs = dr["规格"].ToString();
                    drugStencil.Extend = dr["扩展信息"].ToString();
                    drugStencil.SortNO = FS.FrameWork.Function.NConvert.ToInt32(dr["序号"]);


                    if (this.constantMgr.SetDrugStencil(drugStencil) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存模版信息失败，请向系统管理员报告错误：" + this.constantMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }

                }

                this.dtDetail.AcceptChanges();
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

                FS.FrameWork.Management.PublicTrans.Commit();

                this.ShowList();
            }

            Function.ShowMessage("保存成功!", MessageBoxIcon.Information);

            return 1;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除模版明细
        /// </summary>
        protected int DelDetail()
        {
            if (this.dtDetail.Rows.Count <= 0)
            {
                return 0;
            }

            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return 0;
            }

            DialogResult rs = MessageBox.Show("确认删除该条记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            string key = this.curIDataDetail.FpSpread.GetCellText(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "主键");

            if (!string.IsNullOrEmpty(this.curStencil.Stencil.ID))
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                int parma = this.constantMgr.DelDrugStencil(this.curStencil.Stencil.ID, key);
                if (parma == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("删除失败，请向系统管理员报告错误：" + this.constantMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            DataRow drFind = this.dtDetail.Rows.Find(new string[] { key });
            if (drFind != null)
            {
                this.dtDetail.Rows.Remove(drFind);
            }

            this.hsStencil.Remove(key);

            Function.ShowMessage("删除成功", MessageBoxIcon.Information);

            return 1;
        }

        /// <summary>
        /// 删除模版
        /// </summary>
        protected int DelStencil()
        {
            if (this.dtDetail.Rows.Count <= 0)
            {
                return 0;
            }

            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return 0;
            }

            DialogResult rs = MessageBox.Show("确认删除该模板吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.constantMgr.DelDrugStencil(this.curStencil.Stencil.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("删除失败，请向系统管理员报告错误：" + this.constantMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            Function.ShowMessage("删除成功", MessageBoxIcon.Information);

            this.ShowList();


            return 1;
        }


        #endregion

        #region 移动
        /// <summary>
        /// 删除模版明细
        /// </summary>
        protected int MovDetail()
        {
            //if (!this.Unique)
            //{
            //    Function.ShowMessage("不是唯一性约束的药品模板不可以移动!", MessageBoxIcon.Information);
            //    return 0;
            //}
            if (this.dtDetail.Rows.Count <= 0)
            {
                return 0;
            }

            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return 0;
            }

            if (this.ucTreeViewChooseList.TreeView.Nodes.Count == 0 || this.ucTreeViewChooseList.TreeView.Nodes[0].Nodes.Count == 0)
            {
                return 0;
            }
            DialogResult rs = MessageBox.Show("【移动】操作将删除当前模板里的该药品,确认移动该条记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }


            ArrayList alDrugStencil = new ArrayList();
            foreach (TreeNode tempNode in this.ucTreeViewChooseList.TreeView.Nodes[0].Nodes)
            {
                if (tempNode == this.ucTreeViewChooseList.TreeView.SelectedNode)
                {
                    continue;
                }
                if (tempNode.Tag is FS.HISFC.Models.Pharmacy.DrugStencil)
                {
                    FS.HISFC.Models.Pharmacy.DrugStencil info = tempNode.Tag as FS.HISFC.Models.Pharmacy.DrugStencil;
                    alDrugStencil.Add(info.Stencil);
                }
            }

            if (alDrugStencil.Count == 0)
            {
                return 0;
            }

            FS.FrameWork.Models.NeuObject choosedNeuObject = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alDrugStencil, null, new bool[] { true, true, false, false, false, false, false, false }, null, ref choosedNeuObject) == 0)
            {
                return 0;
            }

            string key = this.curIDataDetail.FpSpread.GetCellText(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "主键");
            if (!this.hsStencil.Contains(key))
            {
                Function.ShowMessage("移动药品失败，请向系统管理员报告错误：哈希表中没有该药品的模板变量", MessageBoxIcon.Error);
                return -1;
            }

            DataRow drFind = this.dtDetail.Rows.Find(new string[] { key });
            if (drFind == null)
            {
                Function.ShowMessage("移动药品失败，请向系统管理员报告错误：当前数据中没有找到您选择药品" + this.constantMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            int param = 1;
            if (!string.IsNullOrEmpty(this.curStencil.Stencil.ID))
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                param = this.constantMgr.DelDrugStencil(this.curStencil.Stencil.ID, key);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("删除失败，请向系统管理员报告错误：" + this.constantMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }

            FS.HISFC.Models.Pharmacy.DrugStencil moveDrugStencil = this.hsStencil[key] as FS.HISFC.Models.Pharmacy.DrugStencil;

            foreach (TreeNode tempNode in this.ucTreeViewChooseList.TreeView.Nodes[0].Nodes)
            {
                if (tempNode.Tag is FS.HISFC.Models.Pharmacy.DrugStencil)
                {
                    FS.HISFC.Models.Pharmacy.DrugStencil info = tempNode.Tag as FS.HISFC.Models.Pharmacy.DrugStencil;

                    if (info.Stencil.ID == choosedNeuObject.ID)
                    {
                        //使用clone防止修改树节点tag值
                        FS.HISFC.Models.Pharmacy.DrugStencil moveToDrugStencil = info.Clone();

                        //复制的是药品信息
                        moveToDrugStencil.Item = moveDrugStencil.Item;
                        param = this.constantMgr.SetDrugStencil(moveToDrugStencil);
                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("移动失败，请向系统管理员报告错误：" + this.constantMgr.Err, MessageBoxIcon.Error);
                            return -1;
                        }
                        break;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.dtDetail.Rows.Remove(drFind);

            this.hsStencil.Remove(key);

            Function.ShowMessage("操作成功", MessageBoxIcon.Information);

            return 1;
        }

        #endregion

        #region 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        protected int Clear()
        {
            this.curTreeView.Nodes.Clear();
            this.dtDetail.Clear();
            this.hsStencil.Clear();
            this.totRowQty = 0;
            this.curStencil = new FS.HISFC.Models.Pharmacy.DrugStencil();

            return 1;
        }
        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            base.OnLoad(e);

            this.curIDataChooseList = this.ucTreeViewChooseList;
            this.curIDataDetail = ucDataDetail;
            this.curTreeView = this.ucTreeViewChooseList.TreeView;

            this.Init();
            this.ShowList();

            this.nlbPriveDept.DoubleClick += new EventHandler(nlbPriveDept_DoubleClick);
        }

        void nlbPriveDept_DoubleClick(object sender, EventArgs e)
        {
            this.SetPriveDept();
        }

        void IDataChooseList_ChooseCompletedEvent()
        {
            string[] values = this.curIDataChooseList.GetChooseData(this.curChooseDataSetting.ColumnIndexs);
            if (values == null || values.Length == 0)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                if (this.Unique)
                {
                    //唯一性约束是在同类型的模板中同一药品只能出现一次
                    //如果某个药品不在此类型的模板中维护，则可以加载到选择列表，否则不加载
                    string SQL = @"select t.stencil_name from pha_com_drugopen t where t.dept_code='{0}' and t.type = '{1}' and t.drug_code = '{2}'";
                    string stencilTypeNO = this.StencilType.ToString();

                    if (this.StencilType == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom)
                    {
                        stencilTypeNO = this.CustomStencilTypeNO;
                    }

                    string deptNO = this.priveDept.ID;
                    if (string.IsNullOrEmpty(deptNO))
                    {
                        deptNO = "All";
                    }

                    SQL = string.Format(SQL, deptNO, stencilTypeNO, values[0]);
                    string returnValue = this.constantMgr.ExecSqlReturnOne(SQL);
                    if (!string.IsNullOrEmpty(returnValue) && returnValue != "-1")
                    {
                        Function.ShowMessage("该药品已经维护到【" + returnValue + "】中，不能再添加" + this.constantMgr.Err, MessageBoxIcon.Error);
                        return;
                    }
                }
                FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(values[0]);
                if (item == null)
                {
                    Function.ShowMessage("请与系统管理员联系，获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.DrugStencil stencil = new FS.HISFC.Models.Pharmacy.DrugStencil();
                    stencil.Item = item;
                    stencil.SortNO = (int)this.totRowQty + 1;
                    stencil.Dept = this.priveDept.Clone();

                    stencil.OpenType.ID = this.StencilType.ToString();

                    if (this.StencilType == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom)
                    {
                        stencil.Stencil.Memo = this.CustomStencilTypeNO;
                    }

                    stencil.Stencil = this.curStencil;

                    this.AddObjectToDataTable(stencil);
                }
            }
        }

        void curTreeView_DoubleClick(object sender, EventArgs e)
        {
            
            this.curTreeView.Visible = false;
            this.curIDataChooseList.SetFocusToFilter();

            this.toolBarService.SetToolButtonEnabled("列表", true);
            this.toolBarService.SetToolButtonEnabled("增加", false);        

            if (!this.Unique)
            {
                this.FreshChooseData();
            }
        }

        void curTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is FS.HISFC.Models.Pharmacy.DrugStencil)
            {
                FS.HISFC.Models.Pharmacy.DrugStencil ds = e.Node.Tag as FS.HISFC.Models.Pharmacy.DrugStencil;
                this.ShowDetail(ds.Stencil.ID);
                this.curStencil.Stencil = ds.Stencil;

                this.nlbInfo.Text = "您选择的模板是【" + ds.Stencil.Name + "】";

            }
        }

        #endregion

        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("列表", "切换到树形列表", FS.FrameWork.WinForms.Classes.EnumImageList.L列表, false, false, null);
            toolBarService.AddToolButton("增加", "增加新模板", FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("删除", "", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("删除模板", "", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("移动", "剪切复制到其它模板", FS.FrameWork.WinForms.Classes.EnumImageList.Z转科, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "列表")
            {
                this.Clear();
                this.curTreeView.Visible = true;
                this.toolBarService.SetToolButtonEnabled("列表", false);
                this.toolBarService.SetToolButtonEnabled("增加", true);
                this.ShowList();
            }
            else if (e.ClickedItem.Text == "增加")
            {
                this.Clear();
                if (!this.Unique)
                {
                    this.FreshChooseData();
                }
                this.curTreeView.Visible = false;
                this.toolBarService.SetToolButtonEnabled("列表", true);
                this.toolBarService.SetToolButtonEnabled("增加", false);

                this.nlbInfo.Text = "新建模板只要在第一行数据录入模板名称即可";

                this.curIDataChooseList.SetFocusToFilter();
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.DelDetail();

            }
            else if (e.ClickedItem.Text == "删除模板")
            {
                this.DelStencil();
            }
            else if (e.ClickedItem.Text == "移动")
            {
                this.MovDetail();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.ShowList();
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }
            return this.SetPriveDept();
        }

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            if (this.PriveCheckType == enumPriveCheckType.Dept)
            {
                int param = Function.ChoosePrivDept("0360", this.PriveType.ID, ref this.priveDept);
                if (param == 0 || param == -1 || this.priveDept == null || string.IsNullOrEmpty(this.priveDept.ID))
                {
                    return -1;
                }
            }
            else if (this.PriveCheckType == enumPriveCheckType.Exits)
            {
                if (string.IsNullOrEmpty(this.PriveType.ID))
                {
                    if (((FS.HISFC.Models.Base.Employee)this.itemMgr.Operator).IsManager)
                    {
                        MessageBox.Show("药品模板维护权限设置为null，请先设置权限！");
                        return 1;
                    }
                    else
                    {
                        MessageBox.Show("请与系统管理员联系，报告错误：药品模板维护权限设置为null");
                        return -1;
                    }
                   
                }
                else if (!Function.JugePrive("0360", this.PriveType.ID))
                {
                    return -1;
                }
            }

            if (this.priveDept.ID != "" && this.priveDept.Name != "")
            {
                this.nlbPriveDept.Text = "您选择的科室是【" + this.priveDept.Name + "】";
            }
            return 1;
        }
        #endregion
    }
}
