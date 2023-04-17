using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    /// <summary>
    /// [功能描述: 盘点基类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-04]<br></br>
    /// 说明：
    /// 1、virtual提供了重写成员的机会，不允许在此增加不通用的业务逻辑
    /// 2、本地化盘点时请继承整个uc
    /// </summary>
    public partial class ucBaseCheck : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        
        public ucBaseCheck()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取入库类别获取业务流程对应的数据明细显示控件（接口）实例
        /// </summary>
        protected SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail curIDataDetail = null;
        protected SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList curIDataChooseList = null;
        protected SOC.HISFC.Components.Pharmacy.Base.ucTreeViewChooseList ucTreeViewChooseList = null;
        protected FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting curChooseDataSetting = null;
        protected SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.CheckFilterSetting curCheckFilterSetting = null;

        protected FS.FrameWork.Models.NeuObject curStockDept = null;

        protected System.Data.DataTable dtDetail = null;
        protected Hashtable hsCheck = new Hashtable();

        protected string detailDataSettingFileName = "";
        protected string settingFileName = "";
        protected uint costDecimals = 2;

        protected decimal totPurchaseCost = 0;
        protected decimal totPLPurchaseCost = 0;
        protected decimal totRetailCost = 0;
        protected decimal totPLRetailCost = 0;
        protected decimal totRowQty = 0;

        //protected FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
        protected FS.SOC.HISFC.BizLogic.Pharmacy.Check itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();

        protected DateTime curFOperTime = new DateTime();

        protected string curCheckBillNO = "";


        enum ColorSetType
        {
            回车重置,
            实时重置
        }

        ColorSetType curColorSetType = ColorSetType.回车重置;

        #region 属性及其变量
        
        private string customCodeColumn;

        [Description("自定以码列要显示的属性,默认UserCode"), Category("设置"), Browsable(true)]
        public string CustomCodeColumn
        {
            get { return this.customCodeColumn; }
            set { this.customCodeColumn = value; }
        }


        private bool isShowChooseList = false;

        [Description("是否显示药品选择列表"), Category("设置"), Browsable(true)]
        public bool IsShowChooseList
        {
            get { return isShowChooseList; }
            set { isShowChooseList = value; }
        }

        private string fStorePriveCode = "0305+01";

        [Description("封账权限编码"), Category("设置"), Browsable(true)]
        public string FStorePriveCode
        {
            get { return fStorePriveCode; }
            set { fStorePriveCode = value; }
        }

        #endregion

        protected bool CheckFStorePrive()
        {
            if (string.IsNullOrEmpty(this.FStorePriveCode))
            {
                FStorePriveCode = "0305+01";
            }
            if (FStorePriveCode.Split('+').Length < 2)
            {
                FStorePriveCode = FStorePriveCode + "+01";
            }
            string[] prives = FStorePriveCode.Split('+');

            return Function.JugePrive(this.curStockDept.ID, prives[0], prives[1]);
        }

        #region 初始化

        protected virtual int Init()
        {
            this.detailDataSettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPCheckSetting.xml";
            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyCheck.xml";
            this.costDecimals = Function.GetCostDecimals("0305", "01");

            int param = this.InitDataTable();

            if (param == -1)
            {
                return param;
            }

            param = this.InitDataDetail();
            if (param == -1)
            {
                return -1;
            }

            param = this.InitChooseList();
            if (param == -1)
            {
                return -1;
            }

            param = this.InitColorSetType();
           
            return param;
        }

        /// <summary>
        /// 初始化入库明细数据控件(接口)
        /// 必须在InitDataTable之后调用
        /// </summary>
        /// <returns></returns>
        protected virtual int InitDataDetail()
        {
            if (this.curIDataDetail == null)
            {
                object interfaceImplement = InterfaceManager.GetDataDetailControl();
                if (interfaceImplement == null || !(interfaceImplement is SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail))
                {
                    this.curIDataDetail = new Base.ucDataDetail();
                }
                if (interfaceImplement is System.Windows.Forms.Control)
                {
                    this.curIDataDetail = interfaceImplement as SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail;
                }

                if (curIDataDetail == null || !(curIDataDetail is System.Windows.Forms.Control))
                {
                    Function.ShowMessage("系统设置错误：盘点数据录入控件初始化失败！请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
                int param = this.curIDataDetail.Init();

                if (param == -1)
                {
                    Function.ShowMessage("系统设置错误：盘点数据录入控件初始化失败！请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                this.npanelDetail.Controls.Add(this.curIDataDetail as System.Windows.Forms.Control);
            }

            this.curIDataDetail.Filter = "自定义码 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码 like '%{0}%' or 名称 like '%{0}%' ";
            if (this.curIDataDetail.FpSpread != null && this.curIDataDetail.FpSpread.Sheets.Count > 0 && this.dtDetail != null)
            {
                this.curIDataDetail.FpSpread.Sheets[0].DataSource = this.dtDetail.DefaultView;
                this.curIDataDetail.SettingFileName = this.detailDataSettingFileName;
                this.InitFarPoint();
                this.curIDataDetail.FilterTextChanged += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.FilterTextChangeHander(curIDataDetail_FilterTextChanged);
            }
            return 1;
        }

        /// <summary>
        /// 初始化设置明细数据的FarPoint
        /// </summary>
        /// <returns></returns>
        protected virtual int InitFarPoint()
        {
            //配置文件在过滤后恢复FarPoint格式用
            if (!System.IO.File.Exists(this.detailDataSettingFileName))
            {
                this.curIDataDetail.FpSpread.Sheets[0].RowHeader.Visible = true;

                this.curIDataDetail.FpSpread.SetColumnWith(0, "货位号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "自定义码", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "名称", 120f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "包装数量", 40f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批次", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "有效期", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "盘点数量1", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "包装单位", 20f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "盘点数量2", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "最小单位", 20f);

                this.curIDataDetail.FpSpread.SetColumnWith(0, "发药机数量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "发药机单位", 20f);

                this.curIDataDetail.FpSpread.SetColumnWith(0, "盘点库存", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "封账库存", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 20f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "盘点购额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "盘点零额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "盈亏数量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "盈亏购额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "盈亏零额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "备注", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "药品编码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "拼音码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "五笔码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "主键", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "生产厂家", 120f);


                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nPrice = new FarPoint.Win.Spread.CellType.NumberCellType();
                nPrice.DecimalPlaces = 4;
                nPrice.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = (int)this.costDecimals;
                nCost.ReadOnly = true;


                FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty.DecimalPlaces = 0;
                nQty.ReadOnly = true;


                FarPoint.Win.Spread.CellType.NumberCellType nQtyWrite = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQtyWrite.DecimalPlaces = 0;
                nQtyWrite.ReadOnly = false;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "货位号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "包装数量", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批次", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "有效期", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盘点数量1", nQtyWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "包装单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盘点数量2", nQtyWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "最小单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "发药机库存", nQtyWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "发药机单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盘点库存", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "封账库存", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盘点购额", nCost);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盘点零额", nCost);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盈亏数量", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盈亏购额", nCost);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盈亏零额", nCost);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "备注", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "拼音码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "五笔码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "主键", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "生产厂家", t);

                this.curIDataDetail.FpSpread.SaveSchema(this.detailDataSettingFileName);
            }
            else
            {
                this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);
            }

            this.curIDataDetail.FpSpread.EditModePermanent = true;
            this.curIDataDetail.FpSpread.EditMode = true;
            this.curIDataDetail.FpSpread.EditModeReplace = true;

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
        protected virtual int InitDataTable()
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
                    new DataColumn("货位号",      dtStr),
                    new DataColumn("自定义码",	  dtStr),
                    new DataColumn("名称",	      dtStr),
                    new DataColumn("规格",        dtStr),
                    new DataColumn("包装数量",    dtDec),
                    new DataColumn("批号",		  dtStr),
                    new DataColumn("批次",		  dtStr),
                    new DataColumn("有效期",	  dtStr),
                    new DataColumn("购入价",      dtDec),
                    new DataColumn("零售价",      dtDec),
                    new DataColumn("盘点数量1",	  dtDec),
                    new DataColumn("包装单位",    dtStr),
                    new DataColumn("盘点数量2",	  dtDec),
                    new DataColumn("最小单位",	  dtStr),
                    new DataColumn("发药机库存",  dtDec),
                    new DataColumn("发药机单位",	  dtStr),
                    new DataColumn("盘点库存",    dtDec),
                    new DataColumn("封账库存",    dtDec),
                    new DataColumn("单位",		  dtStr),
                    new DataColumn("盘点购额",	  dtDec),
                    new DataColumn("盘点零额",	  dtDec),
                    new DataColumn("盈亏数量",	  dtDec),
                    new DataColumn("盈亏购额",	  dtDec),
                    new DataColumn("盈亏零额",	  dtDec),
                    new DataColumn("备注",        dtStr),
                    new DataColumn("药品编码",	  dtStr),
                    new DataColumn("拼音码",      dtStr),
                    new DataColumn("五笔码",      dtStr),
                    new DataColumn("主键",        dtStr),
                    new DataColumn("生产厂家",        dtStr),   //{83FC79A3-09AD-40c3-A442-98D89A169669}

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "盘点数量1" || dc.ColumnName == "盘点数量2"
                     ||dc.ColumnName == "盘点库存" || dc.ColumnName == "盘点购额"
                    || dc.ColumnName == "盘点零额" || dc.ColumnName == "盈亏数量"
                    || dc.ColumnName == "盈亏购额"   || dc.ColumnName == "盈亏零额"
                    || dc.ColumnName == "备注"||dc.ColumnName=="发药机库存")
                {
                    continue;
                }
                dc.ReadOnly = true;
            }


            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtDetail.Columns["主键"];

            this.dtDetail.PrimaryKey = keys;
            this.dtDetail.CaseSensitive = true;

            return 1;
        }

        protected virtual int InitChooseList()
        {
            if (!this.IsShowChooseList)
            {
                this.npanelChooseList.Width = 0;
                this.neuSplitter1.Visible = false;
                return 1;
            }
            if (this.ucTreeViewChooseList == null)
            {
                this.ucTreeViewChooseList = new FS.SOC.HISFC.Components.Pharmacy.Base.ucTreeViewChooseList();
                this.curIDataChooseList = this.ucTreeViewChooseList as FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList;
                this.npanelChooseList.Controls.Add(ucTreeViewChooseList);
                this.ucTreeViewChooseList.TreeView.Visible = true;
            }

            string errInfo = "";
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = Function.GetBizChooseDataSetting("0305", "01", "01", "0", ref errInfo);
            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Check.ChooseList", ref SQL) == -1)
                {
                    if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Output.CommonPrive.ChooseList", ref SQL) == -1)
                    {
                        Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Check.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                        return -1;
                    }
                 
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
                    "单位", 
                    //"购入价", 
                    "零售价", 
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
                   40f,// "库存量",
                   15f,// "单位", 
                   //40f,// "购入价", 
                   40f,// "零售价", 
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
                   t,// "库存量",
                   t,// "单位", 
                   //n,// "购入价", 
                   n,// "零售价", 
                   t,// "拼音码", 
                   t,// "五笔码", 
                   t,// "通用名", 
                   t,// "通用名拼音码", 
                   t,// "通用名五笔码"
                   t// "通用名自定义码"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\CommonOutputL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curIDataChooseList.SettingFileName = chooseDataSetting.SettingFileName;
            }

            return 1;
        }

        protected virtual int InitColorSetType()
        {
            string colorSetType = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "Check", "ColorSetType", ((int)this.curColorSetType).ToString());
            try
            {
                this.curColorSetType = (ColorSetType)(FS.FrameWork.Function.NConvert.ToInt32(colorSetType));
                this.nlbColorReset.Text = this.curColorSetType.ToString() + "：";
            }
            catch { }

            return 1;
        }

        #endregion

        #region 刷新数据选择列表
        protected int FreshDataChooseList()
        {
            if (curChooseDataSetting == null)
            {
                return 0;
            }
            this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL, this.curStockDept.ID, "{0}"), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
            this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);
            this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
            this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);

            return 1;
        }
        #endregion

        #region 显示单据

        protected virtual int ShowList()
        {
            List<FS.HISFC.Models.Pharmacy.Check> checkList = this.itemMgr.QueryCheckList(this.curStockDept.ID, "0", "ALL");
            if (checkList == null)
            {
                Function.ShowMessage("获取封账列表发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

          
            if (this.IsShowChooseList)
            {
                this.tv = this.ucTreeViewChooseList.TreeView;
            }
            else
            {
                if (this.tv == null || !(this.tv is FS.SOC.HISFC.Components.Common.Base.baseTreeView))
                {
                    FS.SOC.HISFC.Components.Common.Base.baseTreeView baseTreeView = new FS.SOC.HISFC.Components.Common.Base.baseTreeView();
                    this.tv = baseTreeView;
                }               
            }

            if (this.tv == null)
            {
                return 0;
            }

            if (this.tv is FS.SOC.HISFC.Components.Common.Base.baseTreeView)
            {
                this.tv.ImageList = ((FS.SOC.HISFC.Components.Common.Base.baseTreeView)this.tv).groupImageList;
            }

            this.tv.Nodes.Clear();
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.hsCheck.Clear();

            TreeNode rootNode = new TreeNode("盘点单列表", 2, 2);

            this.tv.Nodes.Add(rootNode);



            foreach (FS.HISFC.Models.Pharmacy.Check checkStatic in checkList)
            {
                TreeNode nodeBill = new TreeNode();
                nodeBill.ImageIndex = 0;
                nodeBill.SelectedImageIndex = 1;
                nodeBill.Text = checkStatic.CheckNO + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(checkStatic.FOper.ID) + checkStatic.FOper.OperTime.ToString("yyyy-MM-dd HH:mm");

                nodeBill.Tag = checkStatic;

                rootNode.Nodes.Add(nodeBill);

                List<FS.HISFC.Models.Pharmacy.Check> checkDisposeWayList = this.itemMgr.QueryCheckDisposeWayList(this.curStockDept.ID, checkStatic.CheckNO);
                if (checkDisposeWayList == null)
                {
                    Function.ShowMessage("获取封账列表发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (FS.HISFC.Models.Pharmacy.Check check in checkDisposeWayList)
                    {
                        TreeNode nodePage = new TreeNode();
                        nodePage.Tag = check;
                        nodePage.Text = check.DisposeWay;

                        nodeBill.Nodes.Add(nodePage);
                    }
                }
            }

            

            this.tv.ExpandAll();

            return 1;
        }

        /// <summary>
        /// 显示单据明细情况
        /// 如果disepose_way存有内容当页码处理，tree分页显示，这个放在一起处理是为了减少循环
        /// </summary>
        /// <param name="checkBillNO">盘点单号</param>
        /// <returns></returns>
        protected virtual int ShowDetail(string checkBillNO)
        {
            this.curCheckBillNO = checkBillNO;

            this.curIDataDetail.FpSpread.DataSource = null;
            this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex = -1;
            this.curIDataDetail.FpSpread.Sheets[0].ActiveColumnIndex = -1;

            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.hsCheck.Clear();

            if (string.IsNullOrEmpty(this.curCheckBillNO))
            {
                this.curIDataDetail.FpSpread.DataSource = this.dtDetail.DefaultView;
                this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);
                return 0;
            }

            ArrayList alCheckDetail = this.itemMgr.QueryCheckDetailByCheckCode(this.curStockDept.ID, this.curCheckBillNO);
            Hashtable hsPage = new Hashtable();
            ArrayList alPage = new ArrayList();

            foreach (FS.HISFC.Models.Pharmacy.Check checkDetail in alCheckDetail)
            {
               
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(checkDetail.Item.ID);
                checkDetail.Item.DosageForm.ID = item.DosageForm.ID;
                checkDetail.Item.UserCode = item.UserCode;
                checkDetail.Item.SpellCode = item.SpellCode;
                checkDetail.Item.WBCode = item.WBCode;
                checkDetail.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                checkDetail.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;
                checkDetail.Item.NameCollection.OtherSpell.SpellCode = item.NameCollection.OtherSpell.SpellCode;

                //将用户筛选设置条件外的数据过滤掉
                if (this.ncbApplyFilterSetting.Checked && this.FilterOut(checkDetail))
                {
                    continue;
                }

                #region 分页情况处理
                string key = checkDetail.DisposeWay;
                if (string.IsNullOrEmpty(key))
                {
                    key = "####";
                }
                if (!hsPage.Contains(key))
                {
                    hsPage.Add(key, checkDetail);
                    if (!string.IsNullOrEmpty(checkDetail.DisposeWay) && checkDetail.DisposeWay != "0")
                    {
                        alPage.Add(checkDetail.DisposeWay);
                    }
                }

                //盘点单节点的DisposeWay是空值，分页的不是
                if (this.tv.SelectedNode.Tag is FS.HISFC.Models.Pharmacy.Check)
                {
                    FS.HISFC.Models.Pharmacy.Check checkStatic = this.tv.SelectedNode.Tag as FS.HISFC.Models.Pharmacy.Check;
                    if (!string.IsNullOrEmpty(checkStatic.DisposeWay))
                    {
                        if (checkDetail.DisposeWay != checkStatic.DisposeWay)
                        {
                            continue;
                        }
                    }
                }
                #endregion

                if (this.AddObjectToDataTable(checkDetail) == -1)
                {
                    continue;
                }
            }

            this.curIDataDetail.FpSpread.DataSource = this.dtDetail.DefaultView;

            this.SetTotInfoAndColor();
            this.dtDetail.AcceptChanges();

            #region 分页情况处理

            this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);
            if (this.tv.SelectedNode.Tag is FS.HISFC.Models.Pharmacy.Check)
            {
                FS.HISFC.Models.Pharmacy.Check checkStatic = this.tv.SelectedNode.Tag as FS.HISFC.Models.Pharmacy.Check;
                if (string.IsNullOrEmpty(checkStatic.DisposeWay))
                {
                    TreeNode parentNode = this.tv.SelectedNode;
                    parentNode.Nodes.Clear();

                    alPage.Sort(new CompareByValue());

                    foreach (string page in alPage)
                    {
                        TreeNode nodePage = new TreeNode();
                        nodePage.Text = page;
                        nodePage.Tag = hsPage[page];
                        parentNode.Nodes.Add(nodePage);
                    }
                }
            }

            #endregion

            return 1;
        }

        #endregion

        #region DataTable数据添加

        /// <summary>
        /// 向DataTable添加出库实体，显示出库明细信息
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        protected virtual int AddObjectToDataTable(FS.HISFC.Models.Pharmacy.Check check)
        {
            if (check == null)
            {
                Function.ShowMessage("向DataTable中添加盘点信息失败：盘点信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtDetail == null)
            {
                Function.ShowMessage("向DataTable中添加盘点信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            string key = check.Item.ID + check.Item.PriceCollection.PurchasePrice.ToString("F4") + check.BatchNO + check.ValidTime.ToString("yyyy-MM-dd") + check.GroupNO.ToString();

            if (this.hsCheck.Contains(key))
            {
                Function.ShowMessage("" + check.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsCheck.Add(key, check);
            }

            if (check.Item.PackQty == 0)
            {
                check.Item.PackQty = 1;
            }

            decimal adjustPurchaseCost = check.Item.PriceCollection.PurchasePrice * (check.AdjustQty / check.Item.PackQty);
            adjustPurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(adjustPurchaseCost.ToString("F" + this.costDecimals.ToString()));
            
            decimal adjustRetailCost = check.Item.PriceCollection.PurchasePrice * (check.AdjustQty / check.Item.PackQty);
            adjustRetailCost = FS.FrameWork.Function.NConvert.ToDecimal(adjustRetailCost.ToString("F" + this.costDecimals.ToString()));

            this.totPurchaseCost += adjustPurchaseCost;
            this.totRetailCost += adjustRetailCost;

            decimal plPurchaseCost = check.Item.PriceCollection.PurchasePrice * ((check.AdjustQty - check.FStoreQty) / check.Item.PackQty);
            plPurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(plPurchaseCost.ToString("F" + this.costDecimals.ToString()));

            decimal plRetailCost = check.Item.PriceCollection.PurchasePrice * ((check.AdjustQty - check.FStoreQty) / check.Item.PackQty);
            plRetailCost = FS.FrameWork.Function.NConvert.ToDecimal(plRetailCost.ToString("F" + this.costDecimals.ToString()));


            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前品种数：" + this.totRowQty.ToString("F0")
                    + ", 参考购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 参考零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }

            DataRow row = this.dtDetail.NewRow();

            row["货位号"] = check.PlaceNO;
            if (customCodeColumn == "OtherSpell")
            {
                row["自定义码"] = check.Item.NameCollection.OtherSpell.SpellCode;
            }
            else
            {
                row["自定义码"] = check.Item.UserCode;
            }
            row["名称"] = check.Item.Name;
            row["规格"] = check.Item.Specs;
            row["包装数量"] = check.Item.PackQty;
            row["批号"] = check.BatchNO;
            row["批次"] = check.GroupNO;
            row["有效期"] = check.ValidTime.ToShortDateString();
            row["购入价"] = check.Item.PriceCollection.PurchasePrice;
            row["零售价"] = check.Item.PriceCollection.RetailPrice;
            row["盘点数量1"] = check.PackQty;
            row["包装单位"] = check.Item.PackUnit;
            row["盘点数量2"] = check.MinQty;
            row["最小单位"] = check.Item.MinUnit;
            row["发药机库存"] = check.OtherAdjustQty;
            row["发药机单位"] = check.Item.PackUnit;
            row["盘点库存"] = check.AdjustQty;
            row["封账库存"] = check.FStoreQty;
            row["单位"] = check.Item.MinUnit;
            row["盘点购额"] = adjustPurchaseCost;
            row["盘点零额"] = adjustRetailCost;
            row["盈亏数量"] = check.AdjustQty - check.FStoreQty;
            row["盈亏购额"] = plPurchaseCost;
            row["盈亏零额"] = plRetailCost;
            row["备注"] = check.Memo;
            row["药品编码"] = check.Item.ID;
            row["拼音码"] = check.Item.SpellCode.ToLower();
            row["五笔码"] = check.Item.WBCode.ToLower();
            row["主键"] = key;
            //{EDEEC46A-6C3C-4115-BB20-A6DA1C7798E6}
            row["生产厂家"] = HISFC.BizProcess.Cache.Pharmacy.GetProducerNameByBatchNoAndItemNo(this.curStockDept.ID, check.Item.ID, check.BatchNO);
            this.dtDetail.Rows.Add(row);

            return 1;
        }

        #endregion

        #region 保存

        protected virtual bool CheckValid(DataTable dtAddMofity)
        {
            foreach (DataRow dr in dtAddMofity.Rows)
            {
                decimal packQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["盘点数量1"]);

                if (packQty < 0)
                {
                    Function.ShowMessage(dr["名称"].ToString() + "盘点数量1小于0，请修改!", MessageBoxIcon.Information);
                    return false;
                }
                decimal minQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["盘点数量2"]);

                if (minQty < 0)
                {
                    Function.ShowMessage(dr["名称"].ToString() + "盘点数量2小于0，请修改!", MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }

        protected virtual int Save()
        {
            this.dtDetail.DefaultView.RowFilter = "1=1";
            this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);

          
            for (int i = 0; i < this.dtDetail.DefaultView.Count; i++)
            {
                this.dtDetail.DefaultView[i].EndEdit();
            }

            DataTable dtAddMofity = this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified);

            //{6BC360B9-9472-407c-9A4C-F57C31312C56}
            //if (dtAddMofity == null || dtAddMofity.Rows.Count <= 0)
            //{
            //    Function.ShowMessage("数据没有改变!", MessageBoxIcon.Information);
            //    return 0;
            //}

            //if (!this.CheckValid(dtAddMofity))
            //{
            //    return 0;
            //}

            this.curIDataDetail.FpSpread.StopCellEditing();


            //定义事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            string errDrug = "";

            DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行保存.请稍候...");
            Application.DoEvents();

            try
            {
                //盘点单号
                if (!string.IsNullOrEmpty(this.curCheckBillNO))
                {
                    FS.HISFC.Models.Pharmacy.Check checkStatic = this.itemMgr.GetCheckStat(this.curStockDept.ID, curCheckBillNO);
                    if (checkStatic == null)
                    {
                        Function.ShowMessage("获取盘点汇总信息失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    if (checkStatic.State == "1")
                    {
                        Function.ShowMessage("该盘点单已经结存，请退出界面!", MessageBoxIcon.Information);
                        return -1;
                    }
                    else if (checkStatic.State == "2")
                    {
                        Function.ShowMessage("该盘点单已经取消，请退出界面!", MessageBoxIcon.Information);
                        return -1;
                    }
                }
                else
                {
                    #region 对新建盘点单向盘点统计表插入数据
                    this.curCheckBillNO = this.itemMgr.GetCheckCode(this.curStockDept.ID);
                    if (string.IsNullOrEmpty(this.curCheckBillNO))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("获取盘点单号失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }


                    FS.HISFC.Models.Pharmacy.Check checkStatic = new FS.HISFC.Models.Pharmacy.Check();

                    checkStatic.CheckNO = this.curCheckBillNO;				            //盘点单号
                    checkStatic.StockDept = this.curStockDept;			        //库房编码
                    checkStatic.State = "0";					            //封帐状态
                    checkStatic.User01 = "0";						        //盘亏金额
                    checkStatic.User02 = "0";						        //盘盈金额

                    checkStatic.FOper.ID = this.itemMgr.Operator.ID;   //封帐人
                    checkStatic.FOper.OperTime = sysTime;				    //封帐时间
                    checkStatic.Operation.Oper = checkStatic.FOper;               //操作人

                    if (this.itemMgr.InsertCheckStatic(checkStatic) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("插入盘点汇总信息失败，;：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }

                    #endregion
                }


                if (dtAddMofity != null)
                {
                    #region 对发生变动的记录进行更新

                    foreach (DataRow dr in dtAddMofity.Rows)
                    {
                        FS.HISFC.Models.Pharmacy.Check checkDetail = hsCheck[dr["主键"].ToString()] as FS.HISFC.Models.Pharmacy.Check;

                        errDrug = dr["名称"].ToString();

                        if (string.IsNullOrEmpty(checkDetail.CheckNO))
                        {
                            checkDetail.CheckNO = this.curCheckBillNO;
                            checkDetail.State = "0";
                        }
                        checkDetail.Operation.Oper.ID = this.itemMgr.Operator.ID;   
                        checkDetail.Operation.Oper.OperTime = sysTime;			        

                        checkDetail.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["盘点数量1"]);
                        checkDetail.MinQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["盘点数量2"]);
                        checkDetail.OtherAdjustQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["发药机库存"]);

                        checkDetail.AdjustQty = checkDetail.PackQty * checkDetail.Item.PackQty + checkDetail.MinQty + checkDetail.OtherAdjustQty * checkDetail.Item.PackQty;
                        checkDetail.CStoreQty = checkDetail.AdjustQty;
                        checkDetail.ProfitLossQty = checkDetail.AdjustQty - checkDetail.FStoreQty;
                        

                        if (checkDetail.ProfitLossQty < 0)
                        {
                            checkDetail.ProfitStatic = "0";
                        }
                        else if (checkDetail.ProfitLossQty == 0)
                        {
                            checkDetail.ProfitStatic = "2";
                        }
                        else 
                        {
                            checkDetail.ProfitStatic = "1";
                        }

                        //对新增数据该字段（流水号）由FarPoint取到的为空，设为－1
                        if (checkDetail.ID == "")
                        {
                            checkDetail.ID = "-1";
                        }

                        //先进行更新操作，如更新失败则插入
                        int parm = this.itemMgr.UpdateCheckDetail(checkDetail);

                        //对盘点明细表更新数据
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("更新盘点明细信息失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return -1;
                        }
                        else if (parm == 0)
                        {
                            //对盘点明细表插入数据
                            if (this.itemMgr.InsertCheckDetail(checkDetail) != 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                Function.ShowMessage("插入盘点明细信息失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                                return -1;
                            }
                        }

                    }
                    #endregion
                }


                //计算盘点盈亏更新盈亏数量、盈亏标记
                //if (this.itemMgr.SaveCheck(this.curStockDept.ID, this.curCheckBillNO) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    Function.ShowMessage("更新盘点数量失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                //    return -1;
                //}
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("处理" + errDrug + "失败，请与系统管理员联系并报告错误：" + ex.Message, MessageBoxIcon.Error);
                return -1;
            }

            //提交事务
            FS.FrameWork.Management.PublicTrans.Commit();

            this.SetTotInfoAndColor();

            Function.ShowMessage("保存成功!",MessageBoxIcon.Information);

            if (this.IsShowChooseList && !this.ucTreeViewChooseList.TreeView.Visible)
            {
                this.ucTreeViewChooseList.TreeView.Visible = true;
                this.ShowList();
            }
            else
            {
                this.dtDetail.AcceptChanges();
                this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);
            }


            return 1;
        }

        #endregion

        #region 打印

        protected virtual int PrintData()
        {
            if (this.dtDetail.Rows.Count == 0)
            {
                return Function.PrintBill("0305", "01", null);

            }
            ArrayList alDetail = new ArrayList();
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                FS.HISFC.Models.Pharmacy.Check checkDetail = hsCheck[dr["主键"].ToString()] as FS.HISFC.Models.Pharmacy.Check;
                alDetail.Add(checkDetail);
            }
            return Function.PrintBill("0305", "01", alDetail);
        }

        #endregion

        #region 数据筛选
        protected int FilterData()
        {
            DataTable dtAddMofity = this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity != null && dtAddMofity.Rows.Count > 0)
            {
                Function.ShowMessage("数据更改后应用筛选会丢失更改内容，请注意保存!", MessageBoxIcon.Information);
                return 0;
            }
            using(frmSetFilter frmSetFilter = new frmSetFilter())
            {
                frmSetFilter.StartPosition = FormStartPosition.CenterScreen;
                frmSetFilter.Init();
                frmSetFilter.CheckFilterSetCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.CheckFilterSetCompletedHander(frmSetFilter_CheckFilterSetCompletedEvent);
                frmSetFilter.CheckFilterSetCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.CheckFilterSetCompletedHander(frmSetFilter_CheckFilterSetCompletedEvent);
                frmSetFilter.ShowDialog(this);
            }

            return 1;
        }


        void frmSetFilter_CheckFilterSetCompletedEvent(SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.CheckFilterSetting checkFilterSetting)
        {
            this.curCheckFilterSetting = checkFilterSetting;
            if (string.IsNullOrEmpty(this.curCheckBillNO) || !this.ncbApplyFilterSetting.Checked)
            {
                Function.ShowMessage("你可能没有选择单据或者启用筛选\n本次设置在界面退出前系统一直为您暂存，您可以在界面下方设置是否启用筛选！", MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(this.curCheckBillNO) && this.ncbApplyFilterSetting.Checked)
            {
                this.ShowDetail(this.curCheckBillNO);
            }
        }

        private bool FilterOut(FS.HISFC.Models.Pharmacy.Check checkDetail)
        {
            if (this.curCheckFilterSetting == null)
            {
                return false;
            }

            #region 货位号，自定义码范围判断
            bool isValidStart = true;
            bool isValidEnd = true;

            if (!string.IsNullOrEmpty(this.curCheckFilterSetting.StartPlaceNO))
            {
                if (this.curCheckFilterSetting.StartPlaceNO.CompareTo(checkDetail.PlaceNO) > 0)
                {
                    isValidStart = false;
                }
            }
            if (!string.IsNullOrEmpty(this.curCheckFilterSetting.EndPlaceNO))
            {
                if (this.curCheckFilterSetting.EndPlaceNO.CompareTo(checkDetail.PlaceNO) < 0)
                {
                    isValidEnd = false;
                }
            }
            if (isValidStart & isValidEnd)
            {
                if (!string.IsNullOrEmpty(this.curCheckFilterSetting.StartCustomNO))
                {
                    if (this.curCheckFilterSetting.StartCustomNO.CompareTo(checkDetail.Item.UserCode) > 0)
                    {
                        isValidStart = false;
                    }
                }
                if (!string.IsNullOrEmpty(this.curCheckFilterSetting.EndCustomNO))
                {
                    if (this.curCheckFilterSetting.EndCustomNO.CompareTo(checkDetail.Item.UserCode) < 0)
                    {
                        isValidEnd = false;
                    }
                }

                if ((isValidStart & isValidEnd) == false)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            #endregion

            #region 药品类别、性质、剂型判断
            bool isValidDrugType = false;
            bool isValidDrugQuality = false;
            bool isValidDrugDosage = false;

            if (this.curCheckFilterSetting.AlDrugType != null && this.curCheckFilterSetting.AlDrugType.Count > 0)
            {
                foreach (FS.FrameWork.Models.NeuObject neuObject in this.curCheckFilterSetting.AlDrugType)
                {
                    if (neuObject.ID == checkDetail.Item.Type.ID)
                    {
                        isValidDrugType = true;
                        break;
                    }
                }
            }
            else
            {
                isValidDrugType = true;
            }
            if (!isValidDrugType)
            {
                return true;
            }
            if (this.curCheckFilterSetting.AlDrugQuality != null && this.curCheckFilterSetting.AlDrugQuality.Count > 0)
            {
                foreach (FS.FrameWork.Models.NeuObject neuObject in this.curCheckFilterSetting.AlDrugQuality)
                {
                    if (neuObject.ID == checkDetail.Item.Quality.ID)
                    {
                        isValidDrugQuality = true;
                        break;
                    }
                }
            }
            else
            {
                isValidDrugQuality = true;
            }
            if (!isValidDrugQuality)
            {
                return true;
            }
            if (this.curCheckFilterSetting.AlDrugDosage != null && this.curCheckFilterSetting.AlDrugDosage.Count > 0)
            {
                foreach (FS.FrameWork.Models.NeuObject neuObject in this.curCheckFilterSetting.AlDrugDosage)
                {
                    if (neuObject.ID == checkDetail.Item.DosageForm.ID)
                    {
                        isValidDrugDosage = true;
                        break;
                    }
                }
            }
            else
            {
                isValidDrugDosage = true;
            }
            if (!isValidDrugDosage)
            {
                return true;
            }
            #endregion

            return false;
        }

        #endregion

        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("全盘", "全盘", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            toolBarService.AddToolButton("数据筛选", "根据可设置的条件筛选数据", FS.FrameWork.WinForms.Classes.EnumImageList.G过滤, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "全盘")
            {
                this.CheckAll();
            }
            else if (e.ClickedItem.Text == "数据筛选")
            {
                this.FilterData();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        public override int Export(object sender, object neuObject)
        {
            curIDataDetail.FpSpread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            return base.Export(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            return this.PrintData();
        }

        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.Init();
            this.ShowList();
            this.tv.AfterSelect += new TreeViewEventHandler(tv_AfterSelect);
            this.tv.AfterCheck += new TreeViewEventHandler(tv_AfterCheck);
           
            this.nlbColorReset.DoubleClick += new EventHandler(nlbColorReset_DoubleClick);
            base.OnLoad(e);
        }

        void nlbColorReset_DoubleClick(object sender, EventArgs e)
        {
            if (this.curColorSetType== ColorSetType.回车重置)
            {
                this.nlbColorReset.Text = ColorSetType.实时重置.ToString() + "：";
                this.curColorSetType = ColorSetType.实时重置;
            }
            else if (this.curColorSetType == ColorSetType.实时重置)
            {
                this.nlbColorReset.Text = ColorSetType.回车重置.ToString() + "：";
                this.curColorSetType = ColorSetType.回车重置;
            }

            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml","Check", "ColorSetType", ((int)this.curColorSetType).ToString());
        }

        void tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }

        void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is FS.HISFC.Models.Pharmacy.Check)
            {
                this.ShowDetail(((FS.HISFC.Models.Pharmacy.Check)e.Node.Tag).CheckNO);
            }
            else if (e.Node.Tag == null)
            {
                this.ShowDetail("");
            }
        }

        void curIDataDetail_FilterTextChanged()
        {
            this.SetFlag();
            if (this.curColorSetType == ColorSetType.实时重置)
            {
                this.SetTotInfoAndColor();
            }
        }

        void IDataChooseList_ChooseCompletedEvent()
        {
            if (!this.CheckFStorePrive())
            {
                Function.ShowMessage("您没有权限！", MessageBoxIcon.Information);
                return;
            }
            string[] values = this.curIDataChooseList.GetChooseData(this.curChooseDataSetting.ColumnIndexs);
            if (values == null || values.Length == 0)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
                return;
            }

            if (this.itemMgr.JudgeCheckState(values[0], this.curStockDept.ID, "0", this.curCheckBillNO))
            {
                Function.ShowMessage("该药品已经添加到盘点表中，不能再封账！", MessageBoxIcon.Information);
                this.curIDataChooseList.SetFocusToFilter();
                return;
            }

            ArrayList alCheckDetail = new ArrayList();

            //从现有数据中获取是否按批盘点，否则获取部门控制参数设置情况
            bool isCheckBatch = false;
            if (hsCheck != null && hsCheck.Count > 0)
            {
                foreach (FS.HISFC.Models.Pharmacy.Check checkDetail in hsCheck.Values)
                {
                    if (checkDetail.BatchNO.ToUpper() != "ALL")
                    {
                        isCheckBatch = true;
                    }
                    break;
                }
            }
            else
            {
                FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConstantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
                isCheckBatch = phaConstantMgr.IsManageBatch(this.curStockDept.ID);
            }

            //单个药品封账，如果是按批的则需要参考天数
            string daysValue = SOC.Public.XML.SettingFile.ReadSetting(this.settingFileName, "FStoreSetting", "Days", "90");
            int days = 90;
            if (!int.TryParse(daysValue, out days))
            {
                Function.ShowMessage("配置文件" + this.settingFileName + "记录的参数Days不是整数！\n系统将默认90天内无出入库的药品不进行封账，\n您可以在批量封账中更改这个设置。", MessageBoxIcon.Information);
                days = 90;
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFileName, "FStoreSetting", "Days", "90");
            }
            alCheckDetail = this.itemMgr.CloseSingleDrug(this.curStockDept.ID, isCheckBatch, days, values[0]);
            if (alCheckDetail == null)
            {
                Function.ShowMessage("请与系统管理员联系，封账发生错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return;
            }
            if (alCheckDetail.Count == 0)
            {
                Function.ShowMessage("该药已经" + days.ToString() + "天内没有出入库，不能进行封账！\n您可以在批量封账中更改天数的设置。", MessageBoxIcon.Information);
                return;
            }
            foreach (FS.HISFC.Models.Pharmacy.Check checkDetail in alCheckDetail)
            {
                checkDetail.Operation.ID = this.itemMgr.Operator.ID;
                checkDetail.Operation.Oper.OperTime = System.DateTime.Now;
                checkDetail.ProfitLossQty = checkDetail.AdjustQty - checkDetail.FStoreQty;

                FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(checkDetail.Item.ID);

                FS.HISFC.Models.IMA.PriceService priceService = checkDetail.Item.PriceCollection;

                checkDetail.Item = item;
                if (isCheckBatch)
                {
                    checkDetail.Item.PriceCollection = priceService;
                }

                if (this.AddObjectToDataTable(checkDetail) != -1)
                {
                    if (this.curColorSetType == ColorSetType.实时重置)
                    {
                        this.SetTotInfoAndColor(this.curIDataDetail.FpSpread.Sheets[0].RowCount - 1);
                    }
                    this.curIDataDetail.FpSpread.Sheets[0].SetActiveCell(this.curIDataDetail.FpSpread.Sheets[0].RowCount - 1, 0);
                    this.curIDataDetail.SetFocus();
                }
                else
                {
                    this.curIDataChooseList.SetFocusToFilter();
                }
            }
            this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);
        }

        #endregion

        #region 按键处理

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                
                if (this.totRowQty == this.curIDataDetail.FpSpread.Sheets[0].Rows.Count)
                {
                    this.SetTotInfoAndColor(this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex);
                }
                else
                {
                    this.SetTotInfoAndColor();
                }

                if (this.curIDataDetail.IsContainsFocus)
                {
                    if (this.curIDataDetail.SetFocus() == 0)
                    {
                        this.curIDataDetail.SetFocusToFilter();
                    }
                }
            }
           
            return base.ProcessDialogKey(keyData);
        }

        protected virtual int SetTotInfoAndColor()
        {
            this.totPLPurchaseCost = 0;
            this.totPLRetailCost = 0;
            this.totPurchaseCost = 0;
            this.totRetailCost = 0;
            this.totRowQty = 0;
            for (int rowIndex = 0; rowIndex < this.curIDataDetail.FpSpread.Sheets[0].Rows.Count; rowIndex++)
            {
                string strPackQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "盘点数量1");
                string strMinQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "盘点数量2");
                string strItemPackQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "包装数量");
                string strFStoreQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "封账库存");
                string strRetailPrice = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "零售价");
                string strPurchasePrice = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "购入价");
                string strOtherAdjustQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "发药机库存");

                decimal packQty = FS.FrameWork.Function.NConvert.ToDecimal(strPackQty);
                decimal minQty = FS.FrameWork.Function.NConvert.ToDecimal(strMinQty);
                decimal itemPackQty = FS.FrameWork.Function.NConvert.ToDecimal(strItemPackQty);
                decimal fStoreQty = FS.FrameWork.Function.NConvert.ToDecimal(strFStoreQty);
                decimal retailPrice = FS.FrameWork.Function.NConvert.ToDecimal(strRetailPrice);
                decimal purchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(strPurchasePrice);
                decimal otherAdjustQty = FS.FrameWork.Function.NConvert.ToDecimal(strOtherAdjustQty);
                if (itemPackQty == 0)
                {
                    itemPackQty = 1;
                }

                decimal adjustQty = packQty * itemPackQty + minQty + otherAdjustQty * itemPackQty;
                decimal plQty = adjustQty - fStoreQty;
                decimal adjustPurchaseCost = purchasePrice * (adjustQty / itemPackQty);
                adjustPurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(adjustPurchaseCost.ToString("F" + this.costDecimals.ToString()));
                decimal adjustRetailCost = retailPrice * (adjustQty / itemPackQty);
                adjustRetailCost = FS.FrameWork.Function.NConvert.ToDecimal(adjustRetailCost.ToString("F" + this.costDecimals.ToString()));

                decimal plPurchaseCost = purchasePrice * (plQty / itemPackQty);
                plPurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(plPurchaseCost.ToString("F" + this.costDecimals.ToString()));
                decimal plRetailCost = retailPrice * (plQty / itemPackQty);
                plRetailCost = FS.FrameWork.Function.NConvert.ToDecimal(plRetailCost.ToString("F" + this.costDecimals.ToString()));

                this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点库存", adjustQty);
                this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点购额", adjustPurchaseCost);
                this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点零额", adjustRetailCost);
                this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盈亏数量", plQty);
                this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盈亏购额", plPurchaseCost);
                this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盈亏零额", plRetailCost);

                this.totRowQty++;
                this.totPurchaseCost += adjustPurchaseCost;
                this.totRetailCost += adjustRetailCost;
                this.totPLPurchaseCost += plPurchaseCost;
                this.totPLRetailCost += plRetailCost;

                this.curIDataDetail.Info = "当前品种数：" + this.totRowQty.ToString("F0")
                  + ", 参考购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                  + ", 参考零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                  + ", 参考购入盈亏：" + this.totPLPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                  + ", 参考零售盈亏：" + this.totPLRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');

                if (plQty < 0)
                {
                    this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = System.Drawing.Color.Red;
                }
                else if (plQty == 0)
                {
                    this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = System.Drawing.Color.Blue;
                }

            }

            return 1;
        }

        protected virtual int SetTotInfoAndColor(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex > this.curIDataDetail.FpSpread.Sheets[0].Rows.Count - 1)
            {
                return 0;
            }
            string strPackQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "盘点数量1");
            string strMinQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "盘点数量2");
            string strItemPackQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "包装数量");
            string strFStoreQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "封账库存");
            string strRetailPrice = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "零售价");
            string strPurchasePrice = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "购入价");
            string strPrePurchaseCost = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "盘点购额");
            string strPreRetailCost = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "盘点零额");
            string strPrePLPurchaseCost = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "盈亏购额");
            string strPrePLRetailCost = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "盈亏零额");
            string strOtherAdjustQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "发药机库存");

            decimal packQty = FS.FrameWork.Function.NConvert.ToDecimal(strPackQty);
            decimal minQty = FS.FrameWork.Function.NConvert.ToDecimal(strMinQty);
            decimal itemPackQty = FS.FrameWork.Function.NConvert.ToDecimal(strItemPackQty);
            decimal fStoreQty = FS.FrameWork.Function.NConvert.ToDecimal(strFStoreQty);
            decimal retailPrice = FS.FrameWork.Function.NConvert.ToDecimal(strRetailPrice);
            decimal purchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(strPurchasePrice);
            decimal otherAdjustQty = FS.FrameWork.Function.NConvert.ToDecimal(strOtherAdjustQty);
            if (itemPackQty == 0)
            {
                itemPackQty = 1;
            }

            decimal adjustQty = packQty * itemPackQty + minQty + otherAdjustQty * itemPackQty;
            decimal plQty = adjustQty - fStoreQty;
            decimal adjustPurchaseCost = purchasePrice * (adjustQty / itemPackQty);
            adjustPurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(adjustPurchaseCost.ToString("F" + this.costDecimals.ToString()));
            decimal adjustRetailCost = retailPrice * (adjustQty / itemPackQty);
            adjustRetailCost = FS.FrameWork.Function.NConvert.ToDecimal(adjustRetailCost.ToString("F" + this.costDecimals.ToString()));

            decimal plPurchaseCost = purchasePrice * (plQty / itemPackQty);
            plPurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(plPurchaseCost.ToString("F" + this.costDecimals.ToString()));
            decimal plRetailCost = retailPrice * (plQty / itemPackQty);
            plRetailCost = FS.FrameWork.Function.NConvert.ToDecimal(plRetailCost.ToString("F" + this.costDecimals.ToString()));

            this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点库存", adjustQty);
            this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点购额", adjustPurchaseCost);
            this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点零额", adjustRetailCost);
            this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盈亏数量", plQty);
            this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盈亏购额", plPurchaseCost);
            this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盈亏零额", plRetailCost);

            this.totPurchaseCost += adjustPurchaseCost;
            this.totRetailCost += adjustRetailCost;

            this.totPLPurchaseCost += plPurchaseCost;
            this.totPLRetailCost += plRetailCost;

            this.totPurchaseCost -= FS.FrameWork.Function.NConvert.ToDecimal(strPrePurchaseCost);
            this.totRetailCost -= FS.FrameWork.Function.NConvert.ToDecimal(strPreRetailCost);

            this.totPLPurchaseCost -= FS.FrameWork.Function.NConvert.ToDecimal(strPrePLPurchaseCost);
            this.totPLRetailCost -= FS.FrameWork.Function.NConvert.ToDecimal(strPrePLRetailCost);

            this.curIDataDetail.Info = "当前品种数：" + this.totRowQty.ToString("F0")
               + ", 参考购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
               + ", 参考零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
               + ", 参考购入盈亏：" + this.totPLPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
               + ", 参考零售盈亏：" + this.totPLRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');


            if (plQty < 0)
            {
                this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = System.Drawing.Color.Red;
            }
            else if (plQty == 0)
            {
                this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = System.Drawing.Color.Blue;
            }



            return 1;
        }

        /// <summary>
        /// 设置颜色标记
        /// </summary>
        protected void SetFlag()
        {
            try
            {
                for (int i = 0; i < this.curIDataDetail.FpSpread.Sheets[0].Rows.Count; i++)
                {
                    string strPackQty = this.curIDataDetail.FpSpread.GetCellText(0, i, "盘点数量1");
                    string strMinQty = this.curIDataDetail.FpSpread.GetCellText(0, i, "盘点数量2");
                    string strItemPackQty = this.curIDataDetail.FpSpread.GetCellText(0, i, "包装数量");
                    string strFStoreQty = this.curIDataDetail.FpSpread.GetCellText(0, i, "封账库存");
                    string strOtherAdjustQty = this.curIDataDetail.FpSpread.GetCellText(0, i, "发药机库存");

                    decimal packQty = FS.FrameWork.Function.NConvert.ToDecimal(strPackQty);
                    decimal minQty = FS.FrameWork.Function.NConvert.ToDecimal(strMinQty);
                    decimal itemPackQty = FS.FrameWork.Function.NConvert.ToDecimal(strItemPackQty);
                    decimal fStoreQty = FS.FrameWork.Function.NConvert.ToDecimal(strFStoreQty);
                    decimal otherAdjustQty = FS.FrameWork.Function.NConvert.ToDecimal(strOtherAdjustQty);

                    if (itemPackQty == 0)
                    {
                        itemPackQty = 1;
                    }

                    decimal adjustQty = packQty * itemPackQty + minQty + otherAdjustQty * itemPackQty;
                    decimal plQty = adjustQty - fStoreQty;

                    if (plQty < 0)
                    {
                        this.curIDataDetail.FpSpread.Sheets[0].Rows[i].ForeColor = System.Drawing.Color.Red;
                    }
                    else if (plQty == 0)
                    {
                        this.curIDataDetail.FpSpread.Sheets[0].Rows[i].ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        this.curIDataDetail.FpSpread.Sheets[0].Rows[i].ForeColor = System.Drawing.Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("格式化盈亏颜色显示时发生错误" + ex.Message);
                return;
            }
        }
        #endregion

        #region 全盘
        protected void CheckAll()
        {
            for (int rowIndex = 0; rowIndex < this.curIDataDetail.FpSpread.Sheets[0].Rows.Count; rowIndex++)
            {
                string strItemPackQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "包装数量");
                string strFStoreQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "封账库存");
                string strOtherAdjustQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "发药机库存");

                decimal packQty = 0;
                decimal minQty = 0;
                decimal itemPackQty = FS.FrameWork.Function.NConvert.ToDecimal(strItemPackQty);
                decimal fStoreQty = FS.FrameWork.Function.NConvert.ToDecimal(strFStoreQty);
                decimal otherAdjustQty = FS.FrameWork.Function.NConvert.ToDecimal(strOtherAdjustQty);

                int colIndex = this.curIDataDetail.FpSpread.GetColumnIndex(0, "盘点数量1");
                if (this.curIDataDetail.FpSpread.Sheets[0].Columns[colIndex].Visible && this.curIDataDetail.FpSpread.Sheets[0].Columns[colIndex].Width > 0)
                {
                    if (itemPackQty == 0)
                    {
                        itemPackQty = 1;
                    }
                    try
                    {
                        Function.GetDrugQtys(fStoreQty - otherAdjustQty * itemPackQty, itemPackQty, ref packQty, ref minQty);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("转换盘点数量出错，错误信息：" + ex.Message);
                    }

                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点数量1", packQty);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点数量2", minQty);
                }
                else
                {
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点数量1", 0);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "盘点数量2", fStoreQty);
                }
            }
            SetTotInfoAndColor();
        }
        #endregion

        internal class CompareByValue : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                string oX = x.ToString();
                string oY = y.ToString();
                return string.Compare(oX.ToString(), oY.ToString());
            }

        }

    }
}
