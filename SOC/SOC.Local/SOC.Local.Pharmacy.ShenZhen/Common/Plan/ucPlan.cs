using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.Components.Pharmacy.Base;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Common.Plan
{
    /// <summary>
    /// [功能描述: 入库计划]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-04]<br></br>
    /// </summary>
    public partial class ucPlan :ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucPlan()
        {
            InitializeComponent();
        }

        private FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail curIDataDetail = null;
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList curIDataChooseList = null;
        private FS.SOC.HISFC.Components.Common.Base.baseTreeView curTreeView = null;
        private FS.FrameWork.Models.NeuObject curPriveType = null;
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting curChooseDataSetting = null;

        //private FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
        private FS.SOC.HISFC.BizLogic.Pharmacy.Plan itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Plan();

        private decimal totRowQty = 0;
        private System.Data.DataTable dtDetail = null;
        private Hashtable hsPlan = new Hashtable();
        private string settingFileName = "";

        private Hashtable hsCompare = new Hashtable();

        private FS.HISFC.Models.Pharmacy.InPlan curPlan = new FS.HISFC.Models.Pharmacy.InPlan();

        private FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();

        #region 属性及所用变量
        string stencilNOUsedCompare = "";

        /// <summary>
        /// 对照编码使用的模板名称
        /// </summary>
        [Description("对照编码使用的模板名称"),Category("设置"),Browsable(true)]
        public string StencilNOUsedCompare
        {
            get { return stencilNOUsedCompare; }
            set { stencilNOUsedCompare = value; }
        }

        string class2Code = "0311";


        /// <summary>
        /// 二级权限
        /// </summary>
        [Description("二级权限,0311制单,其它编码审核"), Category("设置"), Browsable(true)]
        public string Class2Code
        {
            get { return class2Code; }
            set { class2Code = value; }
        }

        private uint daySpan = 30;

        /// <summary>
        /// 时间间隔天数
        /// </summary>
        [Description("时间间隔天数"), Category("设置"), Browsable(true)]
        public uint DaySpan
        {
            get { return daySpan; }
            set { daySpan = value; }
        }

        bool isLockedBeforExport = true;

        /// <summary>
        /// 导出前是否需要封账
        /// </summary>
        [Description("导出前是否需要封账"), Category("设置"), Browsable(true)]
        public bool IsLockedBeforExport
        {
            get { return isLockedBeforExport; }
            set { isLockedBeforExport = value; }
        }
        #endregion

        #region 初始化

        protected int Init()
        {
            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PhaPlan" + this.class2Code + "Setting.xml";

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

            if (param == -1)
            {
                return param;
            }

            param = this.InitCompareData();
          

            //设置开始和结束时间
            this.ndtpEnd.Value = this.ndtpEnd.Value.Date.AddDays(1).AddSeconds(-1);
            this.ndtpBegin.Value = this.ndtpEnd.Value.Date.AddDays(-this.DaySpan);

            this.nrbSortByCustomNO.Checked = FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "Plan", "SortType", "False"));
           

            return param;
        }

        /// <summary>
        /// 初始化数据选择关键属性
        /// </summary>
        /// <returns></returns>
        protected int InitChooseData()
        {
            string errInfo = "";
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = SOC.HISFC.Components.Pharmacy.Function.GetBizChooseDataSetting("0360", curPriveType.Memo, curPriveType.ID, "0", ref errInfo);
            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Common.InPlanPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Common.InPlanPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
               
                chooseDataSetting.ListTile = "药品列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0 };
                chooseDataSetting.Filter =
                   "trade_name like '%{0}%'"
               + " or custom_code like '%{0}%'"
               + " or spell_code like '%{0}%'"
               + " or wb_code like '%{0}%'"
               + " or regular_spell like '%{0}%'"
               + " or regular_wb like '%{0}%'";

                chooseDataSetting.ColumnLabels = new string[] { 
                    "药品编码", 
                    "自定义码",
                    "名称", 
                    "规格", 
                    "库存量", 
                    "购入价", 
                    "零售价", 
                    "单位", 
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
                   60f,// "自定义码",
                   120f,// "名称", 
                   100f,// "规格", 
                   40f,//"库存量",
                   40f,// "购入价", 
                   40f,// "零售价", 
                   15f,// "单位", 
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
                   t,// "自定义码",
                   t,// "名称", 
                   t,// "规格", 
                   n,// "库存量", 
                   n,// "购入价", 
                   n,// "零售价", 
                   t,// "单位", 
                   t,// "拼音码", 
                   t,// "五笔码", 
                   t,// "通用名", 
                   t,// "通用名拼音码", 
                   t,// "通用名五笔码"
                   t// "通用名自定义码"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PhaPlanL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curIDataChooseList.SettingFileName = curChooseDataSetting.SettingFileName;
                this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL,this.priveDept.ID,"{0}"), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
                this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);
                this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
                this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
            }

            return 1;

        }

        /// <summary>
        /// 初始化明细数据的DataTable
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

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            this.dtDetail.Columns.AddRange
                (
                new System.Data.DataColumn[]
                {
                    
                    new DataColumn("产品ID",    dtStr),
                   // new DataColumn("对照码",      dtStr),
                    new DataColumn("药品名称",	  dtStr),
                    new DataColumn("英文名称",    dtStr),
                    new DataColumn("规格",        dtStr),
                    //new DataColumn("包装数量",    dtDec),
                    new DataColumn("消耗量",	  dtDec),
                    new DataColumn("参考量",	  dtDec),
                    new DataColumn("计划量",	  dtDec),
                    new DataColumn("审批量",	  dtDec),
                    new DataColumn("入库量",	  dtDec),
                    new DataColumn("单位",        dtStr),
                    new DataColumn("本科库存",	  dtDec),
                    new DataColumn("全院库存",	  dtDec),
                    new DataColumn("供货公司",    dtStr),
                    new DataColumn("购入价",      dtDec),
                    new DataColumn("金额",	      dtDec),
                    new DataColumn("生产厂家",    dtStr),
                    new DataColumn("备注",        dtStr),
                    new DataColumn("药品编码",	  dtStr),
                    new DataColumn("计划人",	  dtStr),
                    new DataColumn("制定时间",	  dtStr),
                    new DataColumn("审批人",	  dtStr),
                    new DataColumn("审批时间",	  dtStr),
                    new DataColumn("拼音码",      dtStr),
                    new DataColumn("五笔码",      dtStr)   

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "计划量" || dc.ColumnName == "审批量"|| dc.ColumnName == "备注" )
                {
                    continue;
                }
                dc.ReadOnly = true;
            }


            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtDetail.Columns["药品编码"];

            this.dtDetail.PrimaryKey = keys;

            this.dtDetail.DefaultView.AllowNew = true;
            this.dtDetail.DefaultView.AllowEdit = true;
            this.dtDetail.DefaultView.AllowDelete = true;

            return 1;
        }

        /// <summary>
        /// 初始化明细数据控件(接口)
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
            //>>{E6615860-1B31-4f36-8371-85F5312AD339}修改过滤20121126
            //this.curIDataDetail.Filter = "自定义码 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码 like '%{0}%' ";
            this.curIDataDetail.Filter = "药品名称 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码 like '%{0}%' ";
            //<<
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "产品ID", 60f);
              //  this.curIDataDetail.FpSpread.SetColumnWith(0, "对照码", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "商品名称", 120f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "英文名称", 120f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 100f);
                //this.curIDataDetail.FpSpread.SetColumnWith(0, "包装数量", 40f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "参考量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "计划量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "审批量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "入库量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 40f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "本科库存", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "全院库存", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "供货公司", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "生产厂家", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "备注", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "计划人", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "制定时间", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "审批人", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "审批时间", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "药品编码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "拼音码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "五笔码", 0f);


                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nPrice = new FarPoint.Win.Spread.CellType.NumberCellType();
                nPrice.DecimalPlaces = 4;
                nPrice.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = 2;
                nCost.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty.DecimalPlaces = 0;
                nQty.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nQtyWrite = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQtyWrite.DecimalPlaces = 0;
                nQtyWrite.ReadOnly = false;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "产品ID", t);
               // this.curIDataDetail.FpSpread.SetColumnCellType(0, "对照码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "商品名称", t);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "英文名称", 120f);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                //this.curIDataDetail.FpSpread.SetColumnCellType(0, "包装数量", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "参考量", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "计划量", nQtyWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "审批量", nQtyWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "入库量", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "本科库存", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "全院库存", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "供货公司", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "金额", nCost);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "生产厂家", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "计划人", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "制定时间", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "审批人", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "审批时间", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "拼音码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "五笔码", t);


                this.curIDataDetail.FpSpread.SaveSchema(this.settingFileName);
            }
            else
            {
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            }

            this.curIDataDetail.FpSpread.EditModePermanent = true;
            this.curIDataDetail.FpSpread.EditMode = true;
            this.curIDataDetail.FpSpread.EditModeReplace = true;

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

            this.curTreeView.AfterSelect -= new TreeViewEventHandler(curTreeView_AfterSelect);
            this.curTreeView.AfterSelect += new TreeViewEventHandler(curTreeView_AfterSelect);

            this.curTreeView.DoubleClick -= new EventHandler(curTreeView_DoubleClick);
            this.curTreeView.DoubleClick += new EventHandler(curTreeView_DoubleClick);

            return 1;
        }

        protected int InitCompareData()
        {
            if (string.IsNullOrEmpty(StencilNOUsedCompare))
            {
                return 0;
            }

            FS.SOC.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            ArrayList al = constantMgr.QueryDrugStencil(StencilNOUsedCompare);
            foreach (FS.HISFC.Models.Pharmacy.DrugStencil stencil in al)
            {
                if (!hsCompare.Contains(stencil.Item.ID))
                {
                    hsCompare.Add(stencil.Item.ID, stencil.Extend);
                }
            }

            return 1;
        }

        #endregion

        #region 显示列表及明细数据
        protected int ShowList()
        {
            this.Clear();

            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            TreeNode parentNode = new TreeNode();
            parentNode.Text = "采购计划【未审】";
            parentNode.ImageIndex = 4;
            parentNode.SelectedImageIndex = 4;
            this.curTreeView.Nodes.Add(parentNode);

            ArrayList al = this.itemMgr.QueryInPLanList(this.priveDept.ID, "0");
            if (al == null)
            {
                Function.ShowMessage("获取计划单列表失败，请想系统管理员报告：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                TreeNode node = new TreeNode(info.ID);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 1;
                node.Tag = info;
                parentNode.Nodes.Add(node);
            }

            parentNode = new TreeNode();
            parentNode.Text = "采购计划【已审】";
            parentNode.ImageIndex = 2;
            parentNode.SelectedImageIndex = 2;
            this.curTreeView.Nodes.Add(parentNode);

            al = this.itemMgr.QueryInPLanList(this.priveDept.ID, "1");
            if (al == null)
            {
                  Function.ShowMessage("获取计划单列表失败，请想系统管理员报告：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                TreeNode node = new TreeNode(info.ID);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 1;
                node.Tag = info;
                parentNode.Nodes.Add(node);
            }

            parentNode = new TreeNode();
            parentNode.Text = "采购计划【封账】";
            parentNode.ImageIndex = 3;
            parentNode.SelectedImageIndex = 3;
            this.curTreeView.Nodes.Add(parentNode);

            al = this.itemMgr.QueryInPLanList(this.priveDept.ID, "2", this.ndtpBegin.Value, this.ndtpEnd.Value);
            if (al == null)
            {
                Function.ShowMessage("获取计划单列表失败，请想系统管理员报告：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                TreeNode node = new TreeNode(info.ID);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 1;
                node.Tag = info;
                parentNode.Nodes.Add(node);
            }
            this.curTreeView.ExpandAll();

            this.curTreeView.Visible = true;

            return 1;
        }

        protected int ShowDetail(string planBillNO)
        {
            if (planBillNO == null || planBillNO == "")
            {
                return -1;
            }

            List<FS.HISFC.Models.Pharmacy.InPlan> al = this.itemMgr.QueryInPlanDetail(this.priveDept.ID, planBillNO);
            if (al == null)
            {
                Function.ShowMessage("根据计划单号获取模版明细信息发生错误\n" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            ArrayList alInplan = new ArrayList(al.ToArray());
            if (this.nrbSortByCustomNO.Checked)
            {
                alInplan.Sort(new CompareByCustomerCode());
            }
            this.hsPlan.Clear();
            this.dtDetail.Rows.Clear();
            this.totRowQty = 0;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在显示计划明细 请稍候...");
            Application.DoEvents();

            ((System.ComponentModel.ISupportInitialize)(this.curIDataDetail.FpSpread)).BeginInit();
            SOC.HISFC.BizProcess.Cache.Pharmacy.ClearProducerCache();
            SOC.HISFC.BizProcess.Cache.Pharmacy.ClearCompanyCache();

            foreach (FS.HISFC.Models.Pharmacy.InPlan info in alInplan)
            {
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (item != null)
                {
                    info.Item.UserCode = item.UserCode;
                    info.Item.SpellCode = item.SpellCode;
                    info.Item.WBCode = item.WBCode;
                    info.Item.GBCode = item.GBCode;
                    //如果供货公司或生成厂家为空
                    if (string.IsNullOrEmpty(info.Item.Product.Company.ID) || string.IsNullOrEmpty(info.Item.Product.Producer.ID))
                    {
                        if (string.IsNullOrEmpty(item.Product.Company.ID) || string.IsNullOrEmpty(item.Product.Producer.ID))
                        {
                            item = this.itemMgr.GetItem(info.Item.ID);
                        }

                        if (item != null)
                        {
                            if (string.IsNullOrEmpty(item.Product.Company.ID) && item.TenderOffer.IsTenderOffer)
                            {
                                info.Company.ID = item.TenderOffer.Company.ID;
                            }
                            else
                            {
                                info.Company.ID = item.Product.Company.ID;
                            }

                            info.Item.Product.Producer.ID = item.Product.Producer.ID;
                        }
                    }

                }
                else
                {
                    item = this.itemMgr.GetItem(info.Item.ID);
                    if (item != null)
                    {
                        info.Item.UserCode = item.UserCode;
                        info.Item.SpellCode = item.SpellCode;
                        info.Item.WBCode = item.WBCode;
                        info.Item.GBCode = item.GBCode;
                        if (string.IsNullOrEmpty(info.Item.Product.Company.ID) || string.IsNullOrEmpty(info.Item.Product.Producer.ID))
                        {
                            if (string.IsNullOrEmpty(item.Product.Company.ID) && item.TenderOffer.IsTenderOffer)
                            {
                                info.Company.ID = item.TenderOffer.Company.ID;
                            }
                            else
                            {
                                info.Company.ID = item.Product.Company.ID;
                            }

                            info.Item.Product.Producer.ID = item.Product.Producer.ID;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(info.Company.ID))
                {
                    info.Company.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
                }

                if (this.class2Code == "0313" && info.State == "0")
                {
                    info.StockQty = info.PlanQty;
                }
                if (this.AddObjectToDataTable(info) == -1)
                {
                    continue;
                }
                this.curPlan = info;
            }
            if (this.curPlan.State == "2")
            {
                this.dtDetail.Columns["计划量"].ReadOnly = true;
                this.dtDetail.Columns["审批量"].ReadOnly = true;
                this.dtDetail.Columns["备注"].ReadOnly = true;
            }
            else
            {
                if (this.class2Code == "0311")
                {
                    this.dtDetail.Columns["计划量"].ReadOnly = false;
                    this.dtDetail.Columns["审批量"].ReadOnly = true;
                }
                else if (this.class2Code == "0313")
                {
                    this.dtDetail.Columns["计划量"].ReadOnly = true;
                    this.dtDetail.Columns["审批量"].ReadOnly = false;

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
        /// 向DataTable添加实体，显示明细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected int AddObjectToDataTable(FS.HISFC.Models.Pharmacy.InPlan plan)
        {
            if (plan == null)
            {
                Function.ShowMessage("向DataTable中添加入库计划信息失败：入库计划信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtDetail == null)
            {
                Function.ShowMessage("向DataTable中添加入库计划信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            if (this.hsPlan.Contains(plan.Item.ID))
            {
                Function.ShowMessage("" + plan.Item.Name + " 已经重复，请检查是否正确！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsPlan.Add(plan.Item.ID, plan);
            }

            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0");
            }

            DataRow row = this.dtDetail.NewRow();

            row["产品ID"] = plan.Item.GBCode;
            row["药品名称"] = plan.Item.Name;
            row["英文名称"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(plan.Item.ID).NameCollection.EnglishName;

            row["规格"] = plan.Item.Specs;
            //row["包装数量"] = plan.Item.PackQty;
            
            row["单位"] = plan.Item.PackUnit;
            row["本科库存"] = plan.StoreQty / plan.Item.PackQty;
            row["全院库存"] = plan.StoreTotQty / plan.Item.PackQty;
            row["供货公司"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(plan.Company.ID);
            row["购入价"] = plan.Item.PriceCollection.PurchasePrice;
            decimal qty = 0;
            if (this.class2Code == "0311")
            {
                qty = plan.PlanQty;
            }
            else if (this.class2Code == "0313")
            {
                qty = plan.StockQty;
            }
            if (plan.Item.PackQty == 0)
            {
                plan.Item.PackQty = 1;
            }

            row["消耗量"] = plan.OutputQty / plan.Item.PackQty;
            if (!string.IsNullOrEmpty(plan.Extend))
            {
                row["参考量"] = FS.FrameWork.Function.NConvert.ToDecimal(plan.Extend) / plan.Item.PackQty;
            }
            row["计划量"] = plan.PlanQty / plan.Item.PackQty;
            row["审批量"] = plan.StockQty / plan.Item.PackQty;
            row["入库量"] = plan.InQty / plan.Item.PackQty;

            row["金额"] = FS.FrameWork.Function.NConvert.ToDecimal((plan.Item.PriceCollection.PurchasePrice * (qty / plan.Item.PackQty)).ToString("F2"));
            if (!string.IsNullOrEmpty(plan.Item.Product.Producer.ID))
            {
                row["生产厂家"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(plan.Item.Product.Producer.ID);
            }
            else
            {
                row["生产厂家"] = "";
            }
            row["备注"] = plan.Memo;
            row["药品编码"] = plan.Item.ID;
            row["计划人"] = plan.PlanOper.ID;
            row["制定时间"] = plan.PlanOper.OperTime.ToString();
            row["审批人"] = plan.StockOper.ID;
            row["审批时间"] = plan.StockOper.OperTime.ToString();
            row["拼音码"] = plan.Item.SpellCode;
            row["五笔码"] = plan.Item.WBCode;

            this.dtDetail.Rows.Add(row);

            return 1;
        }

        #endregion

        #region 数据修改保存提示
        /// <summary>
        /// 数据没有保存，放弃本次更改吗
        /// </summary>
        /// <returns>true 放弃 false 不放弃</returns>
        protected bool GiveUpChanges()
        {
            bool isChanged = false;
            //删除数据在删除操作时就已经确认过了，而且对于已经保存的数据在删除时候就已经从数据库删除了，所以只需要新增和修改的
            if (this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified) != null)
            {
                //制单的时候只能对新增的计划单或者未审核前的计划进行更改，审核的时候只有对新增的或者审核但是未封账的进行更改
                if (this.class2Code == "0311" && (this.curPlan.State == "0" || string.IsNullOrEmpty(this.curPlan.State)))
                {
                    isChanged = true;
                }
                else if (this.class2Code != "0311" && (this.curPlan.State == "0" ||this.curPlan.State == "1"))
                {
                    isChanged = true;
                }
            }
            if (isChanged)
            {
                DialogResult rs = MessageBox.Show("数据修改后没有保存，放弃本次更改吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 公式调用

        private ArrayList alFormulaPlan = new ArrayList();

        protected int SetPlan()
        {
            if (this.curPlan.State == "2")
            {
                Function.ShowMessage("对已经封账的计划单不可以调用公式！", MessageBoxIcon.Information);
                return 0;
            }
            ArrayList alData = new ArrayList();
            if (this.dtDetail.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("是否清空当前数据", "提示>>", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    foreach (FS.HISFC.Models.Pharmacy.InPlan plan in this.hsPlan.Values)
                    {
                        alData.Add(plan);
                    }
                }
                else
                {
                    this.dtDetail.Rows.Clear();
                    this.hsPlan.Clear();
                    this.dtDetail.AcceptChanges();
                    this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
                }
            }

            if (this.alFormulaPlan != null)
            {
                this.alFormulaPlan.Clear();

            }
            //通过本地化算法获取内部入库申请数据表
            //各项目可以用医院自定义规则生成计划
            this.alFormulaPlan = SOC.HISFC.Components.Pharmacy.Function.SetInputPlan(this.priveDept.ID, alData);
            if (alFormulaPlan == null)
            {
                this.alFormulaPlan = this.GetPlan(this.priveDept.ID, alData);
            }
            if (alFormulaPlan == null)
            {
                return -1;
            }
            this.dtDetail.Columns["消耗量"].ReadOnly = false;
            this.dtDetail.Columns["参考量"].ReadOnly = false;
            this.dtDetail.Columns["金额"].ReadOnly = false;

            SOC.HISFC.BizProcess.Cache.Pharmacy.ClearProducerCache();
            SOC.HISFC.BizProcess.Cache.Pharmacy.ClearCompanyCache();

            foreach (FS.HISFC.Models.Pharmacy.InPlan plan in alFormulaPlan)
            {
                if (string.IsNullOrEmpty(plan.Company.ID) || string.IsNullOrEmpty(plan.Item.Product.Producer.ID))
                {
                    FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(plan.Item.ID);
                    if (item != null)
                    {

                        if (string.IsNullOrEmpty(item.Product.Company.ID) && item.TenderOffer.IsTenderOffer)
                        {
                            plan.Company.ID = item.TenderOffer.Company.ID;
                        }
                        else
                        {
                            plan.Company.ID = item.Product.Company.ID;
                        }
                        plan.Item.Product.Producer.ID = item.Product.Producer.ID;
                    }
                }

                if (!string.IsNullOrEmpty(plan.Company.ID))
                {
                    plan.Company.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(plan.Company.ID);
                }
                DataRow row = this.dtDetail.Rows.Find(new string[] { plan.Item.ID });
                if (row == null)
                {
                    if (this.AddObjectToDataTable(plan) == -1)
                    {
                        break;
                    }
                }
                else
                {
                    decimal purchaseCost = 0;
                    if (plan.Item.PackQty == 0)
                    {
                        plan.Item.PackQty = 1;
                    }
                    purchaseCost = plan.Item.PriceCollection.PurchasePrice * (plan.PlanQty / plan.Item.PackQty);
                    row["计划量"] = plan.PlanQty / plan.Item.PackQty;
                    row["消耗量"] = plan.OutputQty / plan.Item.PackQty;
                    if (!string.IsNullOrEmpty(plan.Extend))
                    {
                        row["参考量"] = FS.FrameWork.Function.NConvert.ToDecimal(plan.Extend) / plan.Item.PackQty;
                    }
                    row["金额"] = FS.FrameWork.Function.NConvert.ToDecimal(purchaseCost.ToString("F2").TrimEnd('0').TrimEnd('.'));

                }
            }
            this.dtDetail.Columns["消耗量"].ReadOnly = true;
            this.dtDetail.Columns["参考量"].ReadOnly = true;
            this.dtDetail.Columns["金额"].ReadOnly = true;

            return 1;
        }

        private ArrayList GetPlan(string stockDeptNO, System.Collections.ArrayList alData)
        {
            using (frmSetPlan frmSetPlan = new frmSetPlan())
            {
                frmSetPlan.SetCompletedEven -= new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.SetCompletedEven += new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.Init(stockDeptNO, FS.HISFC.Models.Pharmacy.EnumDrugStencil.Plan);
                frmSetPlan.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frmSetPlan.ShowDialog();

                if (alFormulaPlan == null)
                {
                    MessageBox.Show("生成入库计划发生错误", "错误>>");
                    return null;
                }
                if (alData != null && alData.Count > 0)
                {
                    //界面加载了项目，根据现有项目生成计划
                    for (int index1 = 0; index1 < alData.Count; index1++)
                    {
                        FS.HISFC.Models.Pharmacy.InPlan plan1 = alData[index1] as FS.HISFC.Models.Pharmacy.InPlan;
                        for (int index2 = 0; index2 < alFormulaPlan.Count; index2++)
                        {
                            FS.HISFC.Models.Pharmacy.InPlan plan2 = alFormulaPlan[index2] as FS.HISFC.Models.Pharmacy.InPlan;
                            if (plan1.Item.ID == plan2.Item.ID)
                            {
                                plan1.PlanQty = plan2.PlanQty;
                                plan1.Formula = plan2.Formula;
                                plan1.Extend = plan2.Extend;//公式生成的参考计划量，将保存到数据库字段
                                alFormulaPlan.RemoveAt(index2);
                                break;
                            }
                        }
                    }
                    return alData;
                }
                else
                {
                    foreach (FS.HISFC.Models.Pharmacy.InPlan plan in alFormulaPlan)
                    {
                        plan.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(plan.Item.ID);
                        plan.Dept.ID = stockDeptNO;
                        //plan.Extend;//公式生成的参考计划量，将保存到数据库字段
                        if (string.IsNullOrEmpty(plan.Item.Product.Company.ID) && plan.Item.TenderOffer.IsTenderOffer)
                        {
                            plan.Company = plan.Item.TenderOffer.Company;
                        }
                        else
                        {
                            plan.Company = plan.Item.Product.Company;
                        }
                        if (plan.Company != null && !string.IsNullOrEmpty(plan.Company.ID))
                        {
                            plan.Company.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(plan.Company.ID);
                        }
                    }
                }
            }
            return alFormulaPlan;
        }

        void frmSetPlan_SetCompletedHander(frmSetPlan.CreatePlanType type, string formula, params string[] param)
        {
            if (type == frmSetPlan.CreatePlanType.Consume)
            {
                //GetPlan(string deptNO, DateTime consumeBeginTime, DateTime consumeEndTime, int lowDays, int upDays, string drugType, string stencilNO)
                alFormulaPlan = this.itemMgr.GetPlan(param[0],
                    FS.FrameWork.Function.NConvert.ToDateTime(param[1]),
                    FS.FrameWork.Function.NConvert.ToDateTime(param[2]),
                    FS.FrameWork.Function.NConvert.ToInt32(param[3]),
                    FS.FrameWork.Function.NConvert.ToInt32(param[4]),
                    param[5],
                    param[6]
                    );
            }
            else if (type == frmSetPlan.CreatePlanType.Warning)
            {
                //GetPlan(string deptNO, string drugType, string stencilNO)
                alFormulaPlan = this.itemMgr.GetPlan(param[0], param[1], param[2]);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected int Save()
        {
            if (this.curPlan.State == "2")
            {
                Function.ShowMessage("对已经封账的计划单修改无效！",MessageBoxIcon.Information);
                return 0;
            }
            if (this.curPlan.State == "1" && this.class2Code=="0311")
            {
                Function.ShowMessage("对已经审核的计划单修改无效！", MessageBoxIcon.Information);
                return 0;
            }
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

            //系统时间
            DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();

            //定义数据库处理事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            #region 如果是修改的入库计划单，则先删除原入库计划单数据

            if (!string.IsNullOrEmpty(this.curPlan.BillNO))
            {
                List<FS.HISFC.Models.Pharmacy.InPlan> alCount = this.itemMgr.QueryInPlanDetail(this.priveDept.ID, this.curPlan.BillNO);

                //删除未审核的计划信息               
                int param = this.itemMgr.DeleteInPlan(this.priveDept.ID, this.curPlan.BillNO, this.curPlan.State);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，删除计划发生错误，请向系统管理员报告：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                else if (param < alCount.Count)
                { //处理并发
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("计划单可能已通过采购审核，请刷新窗口", MessageBoxIcon.Information);
                    return -1;
                }
            }
            else
            {
                //如果是新增加的入库计划单，则取入库计划单号
                this.curPlan.BillNO = this.itemMgr.GetPlanBillNO(this.priveDept.ID);
                //入库计划单号的操作
                if (string.IsNullOrEmpty(this.curPlan.BillNO))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，获取计划单号出错，请向系统管理员报告："+this.itemMgr.Err, MessageBoxIcon.Information);
                    return -1;
                }
            }

            #endregion

            DialogResult dialogResult = DialogResult.No;
            if (this.class2Code == "0311")
            {
                dialogResult = MessageBox.Show("是否删除计划量为0的药品？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            int count = 1;

            ArrayList alPlan = new ArrayList();
           
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(count, this.dtDetail.Rows.Count);
                Application.DoEvents();

                #region 入库计划赋值 保存

              
                FS.HISFC.Models.Pharmacy.InPlan plan = this.hsPlan[dr["药品编码"].ToString()] as FS.HISFC.Models.Pharmacy.InPlan;

                plan.BillNO = this.curPlan.BillNO;                     //计划单号
                plan.PlanType = this.curPlan.PlanType;                    //采购类型
                //单据状态
                if (plan.State != "2")
                {
                    if (this.class2Code == "0311")
                    {
                        if (FS.FrameWork.Function.NConvert.ToDecimal(dr["计划量"])==0)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                continue;
                            }
                        }

                        if (string.IsNullOrEmpty(plan.State))
                        {
                            plan.State = "0";
                        }
                        plan.PlanQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["计划量"]) * plan.Item.PackQty;//计划数量
                        plan.PlanOper.ID = this.itemMgr.Operator.ID;
                        plan.PlanOper.OperTime = sysTime;                 //操作信息
                    }
                    else if (this.class2Code == "0313")
                    {                        
                        plan.State = "1";                                 //单据状态
                        plan.StockQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["审批量"]) * plan.Item.PackQty;
                        plan.StockOper.ID = this.itemMgr.Operator.ID;
                        plan.StockOper.OperTime = sysTime;                 //操作信息
                    }

                }
                plan.Oper = plan.PlanOper;
                plan.Memo = dr["备注"].ToString();                //备注

                if (this.itemMgr.InsertInPlan(plan) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，插入计划发生错误，请向系统管理员报告：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                #endregion

                alPlan.Add(plan);
           
                count++;
            }

            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            FS.FrameWork.Management.PublicTrans.Commit();

            string errInfo = "";

            if (SOC.HISFC.Components.Pharmacy.Function.DealExtendBiz(this.class2Code, curPriveType.ID, alPlan, ref errInfo) == -1)
            {
                Function.ShowMessage("保存数据已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("保存成功","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.ShowList();

            SOC.HISFC.Components.Pharmacy.Function.PrintBill(this.class2Code, curPriveType.ID, alPlan);
            

            return 1;
        }
        #endregion

        #region 封账
        protected int LockPlan()
        {
            if (this.curPlan == null || string.IsNullOrEmpty(this.curPlan.BillNO))
            {
                return 0;
            }
            if (this.curPlan.State == "0" || string.IsNullOrEmpty(this.curPlan.State))
            {
                Function.ShowMessage("计划单没有审核，不可以封账！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.curPlan.State == "2")
            {
                Function.ShowMessage("计划单已经封账！", MessageBoxIcon.Information);
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int param = this.itemMgr.FInPlan(this.curPlan.BillNO, this.priveDept.ID);
            if (param == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("计划单封账出错，请向系统管理员报告："+this.itemMgr.Err,MessageBoxIcon.Error);
                return -1;
            }
            if (param == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("计划单已经修改，请刷新数据" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            Function.ShowMessage("操作成功",MessageBoxIcon.Information);

            this.ShowList();

            return 1;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除模版明细
        /// </summary>
        protected int DeleteDetail()
        {
            if (this.dtDetail.Rows.Count <= 0)
            {
                return 0;
            }

            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return 0;
            }
            if (this.class2Code != "0311")
            {
                Function.ShowMessage("操作无效，审核时不可以删除！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.curPlan.State == "1")
            {
                Function.ShowMessage("对已经审核的计划单删除无效！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.curPlan.State == "2")
            {
                Function.ShowMessage("对已经封账的计划单删除无效！", MessageBoxIcon.Information);
                return 0;
            }
            DialogResult rs = MessageBox.Show("确认删除该条记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            string key = this.curIDataDetail.FpSpread.GetCellText(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "药品编码");

            if (this.hsPlan.Contains(key))
            {
                FS.HISFC.Models.Pharmacy.InPlan plan = hsPlan[key] as FS.HISFC.Models.Pharmacy.InPlan;
                if (!string.IsNullOrEmpty(plan.ID))
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    int param = this.itemMgr.DeleteInPlan(plan.ID);
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("删除失败，请向系统管理员报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    else if (param == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("计划单已经被审批，请刷新界面！", MessageBoxIcon.Error);
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            DataRow drFind = this.dtDetail.Rows.Find(new string[] { key });
            if (drFind != null)
            {
                this.dtDetail.Rows.Remove(drFind);
            }

            this.hsPlan.Remove(key);

            //逐步删除到整单删除时考虑刷新列表，在制单的时候不刷新，对已经保存后的数据整单删除则刷新
            if (this.dtDetail.Rows.Count == 0 && !string.IsNullOrEmpty(this.curPlan.ID))
            {
                this.ShowList();
            }
            //Function.ShowMessage("删除成功", MessageBoxIcon.Information);

            return 1;
        }

        /// <summary>
        /// 删除计划
        /// </summary>
        protected int DeleteBill()
        {
            if (this.dtDetail.Rows.Count <= 0)
            {
                return 0;
            }

            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return 0;
            }
            if (this.class2Code != "0311")
            {
                Function.ShowMessage("操作无效，审核时不可以删除！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.curPlan.State == "1")
            {
                Function.ShowMessage("对已经审核的计划单删除无效！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.curPlan.State == "2")
            {
                Function.ShowMessage("对已经封账的计划单删除无效！", MessageBoxIcon.Information);
                return 0;
            }
            DialogResult rs = MessageBox.Show("确认删除整个计划吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }
            if (!string.IsNullOrEmpty(this.curPlan.ID))
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int param = this.itemMgr.DeleteInPlan(this.priveDept.ID, this.curPlan.BillNO, "0");
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("删除失败，请向系统管理员报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                else if (param == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("计划单已经被审批，请刷新界面！", MessageBoxIcon.Error);
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                this.ShowList();
            }
            else
            {
                this.dtDetail.Rows.Clear();
                this.dtDetail.AcceptChanges();
                this.hsPlan.Clear();
                this.totRowQty = 0;
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            }
            Function.ShowMessage("删除成功", MessageBoxIcon.Information);


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
            this.dtDetail.Rows.Clear();
            this.dtDetail.AcceptChanges();
            this.hsPlan.Clear();
            this.totRowQty = 0;
            this.curPlan = new FS.HISFC.Models.Pharmacy.InPlan();

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
            this.nrbSortByCustomNO.CheckedChanged += new EventHandler(nrbSortByCustomNO_CheckedChanged);
            this.Init();
            this.ShowList();
        }

        void nrbSortByCustomNO_CheckedChanged(object sender, EventArgs e)
        {
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "Plan", "SortType", nrbSortByCustomNO.Checked.ToString());
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
                
                FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(values[0]);
                if (item == null)
                {
                    Function.ShowMessage("请与系统管理员联系，获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.InPlan plan = new FS.HISFC.Models.Pharmacy.InPlan();
                    plan.Item = item;
                    plan.Dept = priveDept;
                    decimal storageNum = 0;
                    if (this.itemMgr.GetStorageNum(this.priveDept.ID, plan.Item.ID, out storageNum) == -1)
                    {
                        Function.ShowMessage("获取库存数量时出错，请与系统管理员联系！\n报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }

                    plan.StoreQty = storageNum;

                    if (string.IsNullOrEmpty(item.Product.Company.ID) && item.TenderOffer.IsTenderOffer)
                    {
                        plan.Company = item.TenderOffer.Company;
                    }
                    else
                    {
                        plan.Company = item.Product.Company;
                    }
                    if (plan.Company != null && !string.IsNullOrEmpty(plan.Company.ID))
                    {
                        plan.Company.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(plan.Company.ID);

                    }


                    if (this.AddObjectToDataTable(plan) == 1)
                    {
                        this.dtDetail.Columns["计划量"].ReadOnly = false;
                        this.dtDetail.Columns["审批量"].ReadOnly = true;
                        this.curIDataDetail.FpSpread.Sheets[0].SetActiveCell(this.curIDataDetail.FpSpread.Sheets[0].RowCount - 1, 0);
                        this.curIDataDetail.SetFocus();
                    }
                    else
                    {
                        this.curIDataChooseList.SetFocusToFilter();
                    }
                }
            }
        }

        void curTreeView_DoubleClick(object sender, EventArgs e)
        {
            if (this.class2Code != "0311")
            {
                return;
            }
            if (!string.IsNullOrEmpty(this.curPlan.State) && this.curPlan.State != "0")
            {
                return;
            }
            this.curTreeView.Visible = false;
            this.curIDataChooseList.SetFocusToFilter();

            this.toolBarService.SetToolButtonEnabled("列表", true);
            this.toolBarService.SetToolButtonEnabled("增加", false);

        }

        void curTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is FS.FrameWork.Models.NeuObject)
            {
                if (!this.GiveUpChanges())
                {
                    return;
                }
                FS.FrameWork.Models.NeuObject plan = e.Node.Tag as FS.FrameWork.Models.NeuObject;
                this.ShowDetail(plan.ID);
            }
        }

        #endregion

        #region 打印
        protected virtual int Print()
        {
            if (this.dtDetail.Rows.Count == 0)
            {
                return SOC.HISFC.Components.Pharmacy.Function.PrintBill("0311", "01", null);
            }
            ArrayList alDetail = new ArrayList();
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                FS.HISFC.Models.Pharmacy.InPlan checkDetail = hsPlan[dr["药品编码"].ToString()] as FS.HISFC.Models.Pharmacy.InPlan;
                alDetail.Add(checkDetail);
            }
            return SOC.HISFC.Components.Pharmacy.Function.PrintBill("0311", "01", alDetail);
        }
        #endregion

        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("列表", "切换到树形列表", FS.FrameWork.WinForms.Classes.EnumImageList.L列表, false, false, null);
            toolBarService.AddToolButton("增加", "增加新计划", FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("删除", "", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("全部删除", "", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("公式", "", FS.FrameWork.WinForms.Classes.EnumImageList.R日消耗, false, false, null);

            toolBarService.AddToolButton("封账", "封存数据，不再修改", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "列表")
            {
                this.curTreeView.Visible = true;
                this.toolBarService.SetToolButtonEnabled("列表", false);
                this.toolBarService.SetToolButtonEnabled("增加", true);
                this.toolBarService.SetToolButtonEnabled("公式", false);
            }
            else if (e.ClickedItem.Text == "增加")
            {
                if (!string.IsNullOrEmpty(this.curPlan.State))
                {
                    this.dtDetail.Rows.Clear();
                    this.dtDetail.AcceptChanges();
                    this.hsPlan.Clear();
                    this.totRowQty = 0;
                    this.curPlan = new FS.HISFC.Models.Pharmacy.InPlan();
                }
                this.curTreeView.Visible = false;
                this.toolBarService.SetToolButtonEnabled("列表", true);
                this.toolBarService.SetToolButtonEnabled("增加", false);
                this.toolBarService.SetToolButtonEnabled("公式", true);

                this.curIDataChooseList.SetFocusToFilter();
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.DeleteDetail();
            }
            else if (e.ClickedItem.Text == "全部删除")
            {
                this.DeleteBill();
            }
            else if (e.ClickedItem.Text == "公式")
            {
                this.SetPlan();
            }
            else if (e.ClickedItem.Text == "封账")
            {
                this.LockPlan();
            }
            else if (e.ClickedItem.Text == "打印")
            {
                this.Print();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        public override int Print(object sender, object neuObject)
        {
            return this.Print();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.ShowList();
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.curPlan == null)
            {
                return 0;
            }
            if (this.curPlan.State != "2" && this.isLockedBeforExport)
            {
                Function.ShowMessage("请封账后导出", MessageBoxIcon.Information);
                return 0;
            }
            this.curIDataDetail.FpSpread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return base.Export(sender, neuObject);
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }
            this.curPriveType = new FS.FrameWork.Models.NeuObject();
            this.curPriveType.ID = "01";

            return this.SetPriveDept();
        }

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {

            int param = SOC.HISFC.Components.Pharmacy.Function.ChoosePrivDept(this.class2Code, this.curPriveType.ID, ref this.priveDept);
            if (param == 0 || param == -1 || this.priveDept == null || string.IsNullOrEmpty(this.priveDept.ID))
            {
                return -1;
            }
            this.nlbInfo.Text = "您选择的是【" + this.priveDept.Name + "】";
            return 1;
        }
        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (((Control)this.curIDataDetail).ContainsFocus)
                {
                    if (this.curIDataDetail.SetFocus() == 0 && !this.curTreeView.Visible)
                    {
                        this.curIDataChooseList.SetFocusToFilter();
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        internal class CompareByCustomerCode : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                string oX = "";
                string oY = "";
                if (x is FS.HISFC.Models.Pharmacy.InPlan && y is FS.HISFC.Models.Pharmacy.InPlan)
                {
                    oX = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem((x as FS.HISFC.Models.Pharmacy.InPlan).Clone().Item.ID).UserCode;
                    oY = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem((y as FS.HISFC.Models.Pharmacy.InPlan).Clone().Item.ID).UserCode;
                }
                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? 1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }
    }
}
