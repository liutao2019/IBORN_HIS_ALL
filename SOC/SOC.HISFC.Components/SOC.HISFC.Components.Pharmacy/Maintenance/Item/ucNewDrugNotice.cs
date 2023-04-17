using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance.Item
{
    /// <summary>
    /// [功能描述: 新药通知单]<br></br>
    /// [创 建 者: cao-lin]<br></br>
    /// [创建时间: 2014-04]<br></br>
    /// </summary>
    public partial class ucNewDrugNotice : Base.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucNewDrugNotice()
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
        private Hashtable hsNewDrug = new Hashtable();
        private string settingFileName = "";

        private Hashtable hsCompare = new Hashtable();

        private FS.HISFC.Models.Pharmacy.InPlan curPlan = new FS.HISFC.Models.Pharmacy.InPlan();

        private FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();

        #region 属性及所用变量


        private string class2Code = "0301";
        /// <summary>
        /// 二级权限
        /// </summary>
        [Description("二级权限,0301制单"), Category("设置"), Browsable(true)]
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

        #endregion

        #region 初始化

        protected int Init()
        {
            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PhaNewDrug" + this.class2Code + "Setting.xml";

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

            this.nrbSortByCustomNO.Checked = FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "NewDrugNotice", "SortType", "False"));
           

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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PhaNewDrugNoticeL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curChooseDataSetting.IsNeedDrugType = true;
                this.curIDataChooseList.SettingFileName = curChooseDataSetting.SettingFileName;
                this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile,curChooseDataSetting.SQL, curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
                this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);
            }

            this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);

            this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);

            this.curTreeView.Visible = false;

            this.curIDataChooseList.Init();

            this.curIDataChooseList.SetFocusToFilter();

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
                    
                    new DataColumn("自定义码",    dtStr),
                    new DataColumn("商品名称",	  dtStr),
                    new DataColumn("规格",        dtStr),
                    //new DataColumn("包装数量",    dtDec),
                    new DataColumn("单位",        dtStr),
                    new DataColumn("供货公司",    dtStr),
                    new DataColumn("购入价",      dtDec),
                    new DataColumn("零售价",	      dtDec),
                    new DataColumn("生产厂家",    dtStr),
                    new DataColumn("备注",        dtStr),
                    new DataColumn("药品编码",	  dtStr),
                    new DataColumn("操作员",	  dtStr),
                    new DataColumn("制定时间",	  dtStr),
                    new DataColumn("拼音码",      dtStr),
                    new DataColumn("五笔码",      dtStr)   

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "备注" )
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "自定义码", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "对照码", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "商品名称", 120f);
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "生产厂家", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "备注", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "操作员", 60f);
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

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "对照码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "商品名称", t);
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
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售价", nCost);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "生产厂家", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "操作员", t);
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

            if (this.hsNewDrug.Contains(plan.Item.ID))
            {
                Function.ShowMessage("" + plan.Item.Name + " 已经重复，请检查是否正确！", System.Windows.Forms.MessageBoxIcon.Information);
                //以下增加出现重复项目时默认到原来的项目，避免工作人员逐条查找
                int rowIndex = 0;
                foreach(DataRow dr in this.dtDetail.Rows)
                {
                    if(plan.Item.ID == dr["药品编码"].ToString())
                    {
                       rowIndex = this.dtDetail.Rows.IndexOf(dr);
                        break;
                    }
                }
                this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex = rowIndex;
                return -1;
            }
            else
            {
                hsNewDrug.Add(plan.Item.ID, plan);
            }

            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0");
            }

            DataRow row = this.dtDetail.NewRow();

            row["自定义码"] = plan.Item.UserCode;
            if (hsCompare.Contains(plan.Item.ID))
            {
                row["对照码"] = hsCompare[plan.Item.ID].ToString();
            }
            row["商品名称"] = plan.Item.Name;
            row["规格"] = plan.Item.Specs;
            //row["包装数量"] = plan.Item.PackQty;
            
            row["单位"] = plan.Item.PackUnit;

            row["供货公司"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(plan.Company.ID);
            row["购入价"] = plan.Item.PriceCollection.PurchasePrice;
            row["零售价"] = plan.Item.PriceCollection.RetailPrice;
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
            row["操作员"] = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(this.itemMgr.Operator.ID);
            row["制定时间"] = this.itemMgr.GetDateTimeFromSysDateTime().ToShortDateString();
            row["拼音码"] = plan.Item.SpellCode;
            row["五笔码"] = plan.Item.WBCode;

            this.dtDetail.Rows.Add(row);

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
           
            DialogResult rs = MessageBox.Show("确认删除该条记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            string key = this.curIDataDetail.FpSpread.GetCellText(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "药品编码");
            
            DataRow drFind = this.dtDetail.Rows.Find(new string[] { key });
            if (drFind != null)
            {
                this.dtDetail.Rows.Remove(drFind);
            }

            this.hsNewDrug.Remove(key);

            //逐步删除到整单删除时考虑刷新列表，在制单的时候不刷新，对已经保存后的数据整单删除则刷新
            if (this.dtDetail.Rows.Count == 0 && !string.IsNullOrEmpty(this.curPlan.ID))
            {
            }
            //Function.ShowMessage("删除成功", MessageBoxIcon.Information);

            return 1;
        }

        /// <summary>
        /// 删除计划
        /// </summary>
        protected int DeleteBill()
        {
            this.dtDetail.Rows.Clear();
            this.dtDetail.AcceptChanges();
            this.hsNewDrug.Clear();
            this.totRowQty = 0;
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
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
            this.hsNewDrug.Clear();
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
            this.curIDataChooseList = this.curIDataChooseList;
            this.curIDataDetail = ucDataDetail;
            this.curTreeView = this.ucTreeViewChooseList.TreeView;
            this.nrbSortByCustomNO.CheckedChanged += new EventHandler(nrbSortByCustomNO_CheckedChanged);
            this.Init();
        }

        void nrbSortByCustomNO_CheckedChanged(object sender, EventArgs e)
        {
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "NewDrugNotice", "SortType", nrbSortByCustomNO.Checked.ToString());
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
                        this.curIDataDetail.SetFocus();
                    }
                    else
                    {
                        this.curIDataChooseList.SetFocusToFilter();
                    }
                }
            }
        }
        #endregion

        #region 打印
        protected virtual int Print()
        {
            if (this.dtDetail.Rows.Count == 0)
            {
                return Function.PrintBill("0301", "01", null);
            }
            ArrayList alDetail = new ArrayList();
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                FS.HISFC.Models.Pharmacy.InPlan checkDetail = hsNewDrug[dr["药品编码"].ToString()] as FS.HISFC.Models.Pharmacy.InPlan;
                alDetail.Add(checkDetail);
            }
            return Function.PrintBill("0301", "01", alDetail);
        }
        #endregion

        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("删除", "", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("全部删除", "", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           

            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            if (e.ClickedItem.Text == "删除")
            {
                this.DeleteDetail();
            }
            else if (e.ClickedItem.Text == "全部删除")
            {
                this.DeleteBill();
            }
            else if (e.ClickedItem.Text == "打印")
            {
                this.Print();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return 1;
        }

        public override int Print(object sender, object neuObject)
        {
            return this.Print();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.curPlan == null)
            {
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

            int param = Function.ChoosePrivDept(this.class2Code, this.curPriveType.ID, ref this.priveDept);
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
