using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    /// <summary>
    /// [功能描述: 零售价调价]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-12]<br></br>
    /// 说明：
    /// 协定处方的调价没有考虑
    /// </summary>
    public partial class ucAdjustRetailPrice : ucBaseAdjustPrice, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucAdjustRetailPrice()
        {
            InitializeComponent();
        }

        protected FS.HISFC.Models.Pharmacy.AdjustPrice curAdjustPriceInfo = new FS.HISFC.Models.Pharmacy.AdjustPrice();

        #region 属性及其变量
        /// <summary>
        /// 单科调价标识
        /// </summary>
        private bool isSingleDept = false;

        /// <summary>
        /// 单科调价标识
        /// </summary>
        [Description("单科调价标识"), Browsable(true), DefaultValue(true)]
        public bool IsSingleDept
        {
            get { return isSingleDept; }
            set { isSingleDept = value; }
        }



        /// <summary>
        /// 是否启用最高零售价限制
        /// </summary>
        private bool isUseTopPrice = true;

        /// <summary>
        /// 是否启用最高零售价限制
        /// </summary>
        [Description("是否启用最高零售价限制"), Category("设置"), Browsable(true)]
        public bool IsUseTopPrice
        {
            get { return isUseTopPrice; }
            set { isUseTopPrice = value; }
        }



        #endregion

        #region 初始化

        protected int Init()
        {
            this.ncbExecuteTime.Visible = !this.IsSingleDept;
            this.ndtpExecuteTime.Visible = !this.IsSingleDept;
            this.nlbExcuteInfo1.Visible = !this.IsSingleDept;
            this.nlbExcuteInfo1.Visible = !this.IsSingleDept;

            this.curPriveType = new FS.FrameWork.Models.NeuObject();
            if (string.IsNullOrEmpty(this.Class3Code))
            {
                this.curPriveType.ID = "01";
                this.curPriveType.Memo = "01";
            }
            else
            {
                this.curPriveType.ID = this.Class3Code;
                this.curPriveType.Memo = this.Class3Code;
            }
            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PhaAdjustPrice" + this.Class2Code + "Setting.xml";

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

            return param;
        }

        /// <summary>
        /// 初始化数据选择关键属性
        /// </summary>
        /// <returns></returns>
        protected int InitChooseData()
        {
            string errInfo = "";
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = Function.GetBizChooseDataSetting(this.Class2Code, curPriveType.Memo, curPriveType.ID, "0", ref errInfo);
            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.adjustMgr.Sql.GetSql("SOC.Pharmacy.Input.CommonPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.CommonPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PhaAdjustPriceL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curIDataChooseList.SettingFileName = chooseDataSetting.SettingFileName;

                this.curIDataChooseList.ShowChooseList(chooseDataSetting.ListTile, chooseDataSetting.SQL, chooseDataSetting.IsNeedDrugType, chooseDataSetting.Filter);
                this.curIDataChooseList.SetFormat(chooseDataSetting.CellTypes, chooseDataSetting.ColumnLabels, chooseDataSetting.ColumnWiths);
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
                    
                    new DataColumn("类别",    dtStr),
                    new DataColumn("自定义码",    dtStr),
                    new DataColumn("商品名称",	  dtStr),
                    new DataColumn("规格",        dtStr),
                    new DataColumn("价格单位",        dtStr),
                    new DataColumn("批发价",      dtDec),
                    new DataColumn("购入价",      dtDec),
                    new DataColumn("公式",      dtStr),
                    new DataColumn("调价前零售价",      dtDec),
                    new DataColumn("调价后零售价",      dtStr),
                    new DataColumn("差价",      dtDec),
                    new DataColumn("库存",	  dtStr),
                    new DataColumn("盈亏金额",	  dtDec),
                    new DataColumn("供货公司",    dtStr),
                    new DataColumn("生产厂家",    dtStr),
                    new DataColumn("调价文号",    dtStr),
                    new DataColumn("备注",        dtStr),
                    new DataColumn("药品编码",	  dtStr),
                    new DataColumn("拼音码",      dtStr),
                    new DataColumn("五笔码",      dtStr),  
                    new DataColumn("主键",	  dtStr)

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "公式" || dc.ColumnName == "调价后零售价" || dc.ColumnName == "调价文号" || dc.ColumnName == "备注" || dc.ColumnName == "差价")
                {
                    continue;
                }
                dc.ReadOnly = true;
            }


            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtDetail.Columns["主键"];

            this.dtDetail.PrimaryKey = keys;
            this.dtDetail.CaseSensitive = true;

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
                this.curIDataDetail.FilterTextChanged += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.FilterTextChangeHander(curIDataDetail_FilterTextChanged);
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "类别", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "自定义码", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "商品名称", 120f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "价格单位", 40f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批发价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "公式", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "调价前零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "调价后零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "差价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "库存", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "盈亏金额", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "供货公司", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "生产厂家", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "调价文号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "备注", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "药品编码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "拼音码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "五笔码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "主键", 0f);


                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                FarPoint.Win.Spread.CellType.TextCellType tWrite = new FarPoint.Win.Spread.CellType.TextCellType();
                tWrite.ReadOnly = false;

                FarPoint.Win.Spread.CellType.NumberCellType nPrice = new FarPoint.Win.Spread.CellType.NumberCellType();
                nPrice.DecimalPlaces = 4;
                nPrice.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nPriceWrite = new FarPoint.Win.Spread.CellType.NumberCellType();
                nPriceWrite.DecimalPlaces = 4;
                nPriceWrite.ReadOnly = false;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "商品名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "价格单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批发价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "公式", tWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "调价前零售价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "调价后零售价", tWrite);
                int columnIndex = this.curIDataDetail.FpSpread.GetColumnIndex(0, "调价后零售价");
                this.curIDataDetail.FpSpread.Sheets[0].Columns[columnIndex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "差价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "库存", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "盈亏金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "供货公司", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "生产厂家", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "调价文号", tWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "备注", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "拼音码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "五笔码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "主键", t);

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

        #endregion

        #region 清空数据
        protected void Clear()
        {
            this.ncbExecuteTime.Checked = false;
            this.ndtpExecuteTime.Value = DateTime.Now;

            this.curTreeView.Nodes.Clear();
            this.dtDetail.Rows.Clear();
            this.dtDetail.AcceptChanges();
            this.hsAdjustPrice.Clear();
            this.curAdjustPriceInfo = new FS.HISFC.Models.Pharmacy.AdjustPrice();
        }
        #endregion

        #region DataTable数据添加
        /// <summary>
        /// 调价明细数据添加到DataTable
        /// </summary>
        /// <param name="adjustPrice"></param>
        /// <returns></returns>
        protected int AddObjectToDataTable(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {
            if (adjustPrice == null)
            {
                Function.ShowMessage("向DataTable中添加调价信息失败：调价信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtDetail == null)
            {
                Function.ShowMessage("向DataTable中添加调价信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            if (this.hsAdjustPrice.Contains(adjustPrice.Item.ID + adjustPrice.BatchNO + adjustPrice.ValidTime.ToString()))
            {
                Function.ShowMessage("" + adjustPrice.Item.Name + " 已经重复，请检查是否正确！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                this.hsAdjustPrice.Add(adjustPrice.Item.ID + adjustPrice.BatchNO + adjustPrice.ValidTime.ToString(), adjustPrice);
            }


            DataRow row = this.dtDetail.NewRow();

            row["类别"] = SOC.HISFC.BizProcess.Cache.Common.GetDrugTypeName(adjustPrice.Item.Type.ID);
            row["自定义码"] = adjustPrice.Item.UserCode;
            row["商品名称"] = adjustPrice.Item.Name;
            row["规格"] = adjustPrice.Item.Specs;
            row["价格单位"] = adjustPrice.Item.PackUnit;
            row["批发价"] = adjustPrice.Item.PriceCollection.WholeSalePrice;
            row["购入价"] = adjustPrice.Item.PriceCollection.PurchasePrice;
            if (string.IsNullOrEmpty(this.curAdjustPriceInfo.State))
            {
                row["公式"] = this.GetRetailPriceComputeFormula(adjustPrice.Item);
                try
                {
                    row["调价后零售价"] = FS.FrameWork.Public.String.ExpressionVal(row["公式"].ToString());
                }
                catch { }
            }
            else
            {
                row["调价后零售价"] = adjustPrice.AfterRetailPrice;
            }
            row["调价前零售价"] = adjustPrice.Item.PriceCollection.RetailPrice;

            row["差价"] = FS.FrameWork.Function.NConvert.ToDecimal(row["调价后零售价"]) - adjustPrice.Item.PriceCollection.RetailPrice;

            row["库存"] = adjustPrice.StoreQty / adjustPrice.Item.PackQty;
            row["供货公司"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(adjustPrice.Company.ID);
            row["生产厂家"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(adjustPrice.Producer.ID);
            row["调价文号"] = adjustPrice.FileNO;
            row["备注"] = adjustPrice.Memo;
            row["药品编码"] = adjustPrice.Item.ID;
            row["拼音码"] = adjustPrice.Item.SpellCode;
            row["五笔码"] = adjustPrice.Item.WBCode;

            row["主键"] = adjustPrice.Item.ID + adjustPrice.BatchNO + adjustPrice.ValidTime.ToString();

            this.dtDetail.Rows.Add(row);

            return 1;
        }
        #endregion

        #region 获取调价的公式

        /// <summary>
        /// 获取调价的公式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetRetailPriceComputeFormula(FS.HISFC.Models.Pharmacy.Item item)
        {
            string formula = "";

            FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula specialDrugFormula = this.adjustMgr.GetAdjustPriceSpeFormula(item.ID);

            if (specialDrugFormula == null)
            {
                Function.ShowMessage("获取调价公式发生错误，请与系统管理员联系并报告错误信息：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                return "";
            }
            if (specialDrugFormula.ValidState == "1" && !string.IsNullOrEmpty(specialDrugFormula.Formula))
            {
                formula = specialDrugFormula.Formula;
            }
            else
            {
                ArrayList alFormula = this.adjustMgr.QueryAdjustPriceFormula();
                if (alFormula == null)
                {
                    Function.ShowMessage("获取调价公式发生错误，请与系统管理员联系并报告错误信息：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return "";
                }

                foreach (FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula retailPriceFormula in alFormula)
                {
                    if (retailPriceFormula.ValidState != "1")
                    {
                        continue;
                    }
                    if (retailPriceFormula.DrugType.ID == item.Type.ID)
                    {
                        if (retailPriceFormula.PriceType == "0" && item.PriceCollection.PurchasePrice >= retailPriceFormula.PriceLower && item.PriceCollection.PurchasePrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                        }
                        else if (retailPriceFormula.PriceType == "1" && item.PriceCollection.WholeSalePrice >= retailPriceFormula.PriceLower && item.PriceCollection.WholeSalePrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                        }
                        else if (retailPriceFormula.PriceType == "2" && item.PriceCollection.RetailPrice >= retailPriceFormula.PriceLower && item.PriceCollection.RetailPrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                        }
                    }
                }
            }

            formula = formula.Replace("购入价", item.PriceCollection.PurchasePrice.ToString("F4").TrimEnd('0').TrimEnd('.'));
            formula = formula.Replace("批发价", item.PriceCollection.WholeSalePrice.ToString("F4").TrimEnd('0').TrimEnd('.'));
            formula = formula.Replace("零售价", item.PriceCollection.RetailPrice.ToString("F4").TrimEnd('0').TrimEnd('.'));

            return formula;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除明细
        /// </summary>
        protected int DeleteDetail()
        {
            if (this.dtDetail.Rows.Count <= 0 || this.curAdjustPriceInfo == null)
            {
                return 0;
            }

            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return 0;
            }

            if (this.curAdjustPriceInfo.State == "1")
            {
                Function.ShowMessage("对已经执行的调价单删除无效！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.curAdjustPriceInfo.State == "2")
            {
                Function.ShowMessage("对已经作废的调价单删除无效！", MessageBoxIcon.Information);
                return 0;
            }
            DialogResult rs = MessageBox.Show("确认删除该条记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            string key = this.curIDataDetail.FpSpread.GetCellText(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "主键");

            if (this.hsAdjustPrice.Contains(key))
            {
                FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = this.hsAdjustPrice[key] as FS.HISFC.Models.Pharmacy.AdjustPrice;
                if (!string.IsNullOrEmpty(adjustPrice.ID))
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    int param = this.adjustMgr.DeleteAdjustPriceInfo(adjustPrice.ID, adjustPrice.Item.ID);
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("删除失败，请向系统管理员报告错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    else if (param == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("调价单状态已经发生变化，请刷新界面！", MessageBoxIcon.Error);
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

            this.hsAdjustPrice.Remove(key);

            //逐步删除到整单删除时考虑刷新列表，在制单的时候不刷新，对已经保存后的数据整单删除则刷新
            if (this.dtDetail.Rows.Count == 0 && !string.IsNullOrEmpty(this.curAdjustPriceInfo.ID))
            {
                this.ShowList();
            }
            //Function.ShowMessage("删除成功", MessageBoxIcon.Information);

            return 1;
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected int DeleteBill()
        {
            if (this.dtDetail.Rows.Count <= 0 || this.curAdjustPriceInfo == null)
            {
                return 0;
            }

            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return 0;
            }
            if (this.curAdjustPriceInfo.State == "1")
            {
                Function.ShowMessage("对已经执行的调价单删除无效！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.curAdjustPriceInfo.State == "2")
            {
                Function.ShowMessage("对已经作废的调价单删除无效！", MessageBoxIcon.Information);
                return 0;
            }
            DialogResult rs = MessageBox.Show("确认整单删除吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }
            if (!string.IsNullOrEmpty(this.curAdjustPriceInfo.ID))
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int param = this.adjustMgr.DeleteAdjustPriceInfo(this.curAdjustPriceInfo.ID);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("删除失败，请向系统管理员报告错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                else if (param == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("调价单状态已经发生变化，请刷新界面！", MessageBoxIcon.Error);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                //在制单的时候不刷新，对已经保存后的数据整单删除则刷新
                this.ShowList();
            }
            else
            {
                this.hsAdjustPrice.Clear();
                this.dtDetail.Rows.Clear();
                this.dtDetail.AcceptChanges();
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            }

            Function.ShowMessage("删除成功", MessageBoxIcon.Information);

            return 1;
        }

        #endregion

        #region 保存

        /// <summary>
        /// 有效性
        /// </summary>
        /// <returns></returns>
        protected bool CheckValid()
        {
            if (this.curAdjustPriceInfo.State == "1")
            {
                Function.ShowMessage("调价单已经执行", MessageBoxIcon.Information);
                return false;
            }
            if (this.curAdjustPriceInfo.State == "2")
            {
                Function.ShowMessage("调价单已经作废", MessageBoxIcon.Information);
                return false;
            }

            if (!this.ncbExecuteTime.Checked)
            {
                DialogResult rs = MessageBox.Show("调价会立即执行，确认调价吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.Cancel)
                {
                    return false;
                }
            }
            else
            {
                if (this.ndtpExecuteTime.Value < this.adjustMgr.GetDateTimeFromSysDateTime())
                {
                    Function.ShowMessage("调价生效时间必须大于当前时间", MessageBoxIcon.Information);
                    return false;
                }
            }

            bool isIgoreZero = false;
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                decimal retailPrice = 0;
                if (!decimal.TryParse(dr["调价后零售价"].ToString(), out retailPrice))
                {
                    Function.ShowMessage(dr["商品名称"].ToString() + "调价后零售价不正确，请录入数字", MessageBoxIcon.Information);
                    return false;
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(dr["调价后零售价"]) < 0)
                {
                    Function.ShowMessage(dr["商品名称"].ToString() + "调价后零售价不能小于0", MessageBoxIcon.Information);
                    return false;
                }
                if (!isIgoreZero && FS.FrameWork.Function.NConvert.ToDecimal(dr["调价后零售价"]) == 0)
                {
                    SOC.Windows.Forms.CheckDialogResult cdr = SOC.Windows.Forms.MessageCheckBox.Show(dr["商品名称"].ToString() + " 的价格为0，是否继续！");
                    if (cdr == FS.SOC.Windows.Forms.CheckDialogResult.No)
                    {
                        return false;
                    }
                    else if (cdr == FS.SOC.Windows.Forms.CheckDialogResult.NoChecked)
                    {
                        return false;
                    }
                    else if (cdr == FS.SOC.Windows.Forms.CheckDialogResult.YesChecked)
                    {
                        isIgoreZero = true;
                    }
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(dr["调价后零售价"]) == FS.FrameWork.Function.NConvert.ToDecimal(dr["调价前零售价"]))
                {
                    Function.ShowMessage(dr["商品名称"].ToString() + "价格没有变化", MessageBoxIcon.Information);
                    return false;
                }
                string keys = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.AdjustPrice info = this.hsAdjustPrice[keys] as FS.HISFC.Models.Pharmacy.AdjustPrice;
                if (this.IsUseTopPrice && info.Item.PriceCollection.TopRetailPrice > 0 && info.AfterRetailPrice > info.Item.PriceCollection.TopRetailPrice)
                {
                    Function.ShowMessage(dr["商品名称"].ToString() + "调价后零售价" + info.AfterRetailPrice.ToString() + "大于最高零售价" + info.Item.PriceCollection.TopRetailPrice.ToString(), MessageBoxIcon.Information);
                    return false;
                }
                if (!this.ContinueAfterCheckReadjust(info.Item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        protected int Print()
        {
            if (!(this.curTreeView.SelectedNode.Tag is FS.HISFC.Models.Pharmacy.AdjustPrice))
            {
                Function.ShowMessage("您还没有选取有效调价单，请确认！",MessageBoxIcon.Information);
                return -1;
            }

            this.curAdjustPriceInfo = this.curTreeView.SelectedNode.Tag as FS.HISFC.Models.Pharmacy.AdjustPrice;

            if (this.curAdjustPriceInfo.State != "1")
            {
                Function.ShowMessage("未执行的调价单不能打印，请确认！",MessageBoxIcon.Information);
                return -1;
            }
            ArrayList alAdjustPriceInfo = this.adjustMgr.QueryAdjustPriceInfoList(this.curAdjustPriceInfo.ID);
            if (alAdjustPriceInfo == null)
            {
                Function.ShowMessage("获取调价单数据发生错误：" + this.adjustMgr.Err + "请联系信息科！", MessageBoxIcon.Error);
                return -1;
            }
            return Function.PrintBill(this.Class2Code, this.Class3Code, alAdjustPriceInfo);
            
        }

        /// <summary>
        /// 保存
        /// </summary>
        protected int Save()
        {
            this.curIDataDetail.FpSpread.StopCellEditing();

            #region 有效性判断
            if (!this.CheckValid())
            {
                return 0;
            }
            #endregion

            for (int i = 0; i < this.dtDetail.Rows.Count; i++)
            {
                this.dtDetail.Rows[i].EndEdit();
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存...");
            Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            string adjustPriceBillNO = this.curAdjustPriceInfo.ID;

            #region 对修改调价单删除原调价单信息 对新调价单获取调价单号

            if (!string.IsNullOrEmpty(adjustPriceBillNO))
            {
                if (this.adjustMgr.DeleteAdjustPriceInfo(this.curAdjustPriceInfo.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：删除原调价单信息发生错误，" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }
            else
            {
                adjustPriceBillNO = this.adjustMgr.GetAdjustPriceBillNO();
                if (adjustPriceBillNO == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：获取新调价单号出错，" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }

            #endregion

            int serialNO = 0;
            ArrayList alAdjustData = new ArrayList();
            //系统时间
            DateTime sysTime = this.adjustMgr.GetDateTimeFromSysDateTime();

            foreach (DataRow dr in this.dtDetail.Rows)
            {
                #region 调价信息处理

                string keys = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.AdjustPrice info = this.hsAdjustPrice[keys] as FS.HISFC.Models.Pharmacy.AdjustPrice;

                //获取最新的避免界面打开过久发送价格变动
                FS.HISFC.Models.Pharmacy.Item item = this.adjustMgr.GetItem(info.Item.ID);
                if (info.Item.PriceCollection.RetailPrice != item.PriceCollection.RetailPrice)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败：" + item.UserCode + " " + item.Name + " " + item.Specs + " 价格已经调整为" + item.PriceCollection.RetailPrice.ToString(), MessageBoxIcon.Information);
                    return -1;
                }

                info.ID = adjustPriceBillNO;                        //调价单号
                info.SerialNO = serialNO;                       //调价单内序号
                info.StockDept = this.priveDept;                 //调价科室
                info.State = "0";                               //调价单状态：0、未调价；1、已调价；2、无效
                if (this.IsSingleDept)
                {
                    info.State = "1";
                }
                info.Operation.Oper.ID = this.adjustMgr.Operator.ID;       //操作员
                info.Operation.Oper.Name = this.adjustMgr.Operator.Name;  //操作员姓名
                info.Operation.Oper.OperTime = sysTime;                     //操作时间

                if (this.ncbExecuteTime.Checked)
                {
                    info.InureTime = this.ndtpExecuteTime.Value;
                }
                else
                {
                    info.InureTime = sysTime;                    //生效时间
                }
                info.IsDDAdjust = this.IsSingleDept;

                info.AfterRetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(dr["调价后零售价"]); //调价后零售价
                info.AfterWholesalePrice = info.Item.PriceCollection.PurchasePrice;
                info.Item.PriceCollection.WholeSalePrice = item.PriceCollection.PurchasePrice;
                info.Memo = dr["公式"].ToString();
                info.FileNO = dr["调价文号"].ToString();

                if (info.Item.PriceCollection.RetailPrice > info.AfterRetailPrice)
                {
                    info.ProfitFlag = "0";
                }
                else
                {
                    info.ProfitFlag = "1";
                }

                if (this.adjustMgr.InsertAdjustPriceInfo(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                if (this.IsSingleDept)
                {
                    if (this.adjustMgr.InsertAdjustPriceDetail(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }

                    int param = this.adjustMgr.UpdateStoragePrice(info.StockDept.ID, info.Item.ID, info.AfterRetailPrice);
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    else if (param == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败，" + item.UserCode + " " + item.Name + " " + item.Specs + "当前药品在本科无库存记录", MessageBoxIcon.Error);
                        return -1;
                    }
                }
                #endregion

                alAdjustData.Add(info);

                serialNO++;
            }


            FS.FrameWork.Management.PublicTrans.Commit();

            #region 对立即生效的全院调价执行存储过程

            if (!this.ncbExecuteTime.Checked && !this.IsSingleDept)
            {
                if (this.adjustMgr.ExecProcedureChangPrice() == -1)
                {
                    Function.ShowMessage("调价失败：执行存储过程发生错误，" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }

            #endregion

            string errInfo = "";
            if (Function.DealExtendBiz(this.Class2Code, this.Class3Code, alAdjustData, ref errInfo) == -1)
            {
                Function.ShowMessage("调价已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
            }

            Function.PrintBill(this.Class2Code, this.Class3Code, alAdjustData);

            Function.ShowMessage("保存成功！", MessageBoxIcon.Information);

            this.ShowList();
            this.toolBarService.SetToolButtonEnabled("列表", false);
            this.toolBarService.SetToolButtonEnabled("制单", true);

            return 1;
        }
        #endregion

        #region 显示单据

        protected int ShowList()
        {
            this.Clear();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据...");
            Application.DoEvents();

            ArrayList alAdjustPriceList = null;
            if (this.IsSingleDept)
            {
                alAdjustPriceList = this.adjustMgr.QueryBillList(this.priveDept.ID, this.ndtpBegin.Value, this.ndtpEnd.Value);
            }
            else
            {
                alAdjustPriceList = this.adjustMgr.QueryGlobalBillList(this.ndtpBegin.Value, this.ndtpEnd.Value);
            }
            if (alAdjustPriceList == null)
            {
                Function.ShowMessage("获取调价单列表发生错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            TreeNode rootUnExcuted = new TreeNode("未执行", 2, 2);
            this.curTreeView.Nodes.Add(rootUnExcuted);

            TreeNode rootExcuted = new TreeNode("已执行", 4, 4);
            this.curTreeView.Nodes.Add(rootExcuted);

            TreeNode rootInvalid = new TreeNode("已作废", 3, 3);

            foreach (FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice in alAdjustPriceList)
            {
                string text = "单号：" + adjustPrice.ID + " 制单：" + adjustPrice.Operation.Oper.OperTime.ToString() + " 执行：" + adjustPrice.InureTime.ToString();

                TreeNode node = new TreeNode(text, 0, 1);
                if (adjustPrice.State == "0")
                {
                    rootUnExcuted.Nodes.Add(node);
                }
                else if (adjustPrice.State == "1")
                {
                    rootExcuted.Nodes.Add(node);
                }
                else if (adjustPrice.State == "2")
                {
                    rootInvalid.Nodes.Add(node);
                    if (this.curTreeView.Nodes.Count <= 2)
                    {
                        this.curTreeView.Nodes.Add(rootInvalid);
                    }
                }

                node.Tag = adjustPrice;
            }
            this.curTreeView.Visible = true;
            this.curTreeView.ExpandAll();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        protected int ShowDetail(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPriceInfo)
        {
            this.dtDetail.Rows.Clear();
            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            this.hsAdjustPrice.Clear();

            this.nlbAdustInfo.Text = "单号：" + adjustPriceInfo.ID + "，制单人(最后修改单据人)：" + adjustPriceInfo.Operation.Name;

            ArrayList alAdjustPriceInfo = this.adjustMgr.QueryAdjustPriceInfoList(adjustPriceInfo.ID);
            if (alAdjustPriceInfo == null)
            {
                Function.ShowMessage("获取调价单数据发生错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            foreach (FS.HISFC.Models.Pharmacy.AdjustPrice info in alAdjustPriceInfo)
            {
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (item != null)
                {
                    info.Item.UserCode = item.UserCode;
                    info.Item.SpellCode = item.SpellCode;
                    info.Item.WBCode = item.WBCode;
                    if (this.curAdjustPriceInfo.State == "0")
                    {
                        info.Item.PriceCollection.WholeSalePrice = item.PriceCollection.WholeSalePrice;
                        info.Item.PriceCollection.PurchasePrice = item.PriceCollection.PurchasePrice;
                    }
                    info.Item.PriceCollection.TopRetailPrice = item.PriceCollection.TopRetailPrice;
                }

                if (this.AddObjectToDataTable(info) == -1)
                {
                    continue;
                }
                this.curAdjustPriceInfo = info;
            }
            try
            {
                this.ndtpExecuteTime.Value = adjustPriceInfo.InureTime;
                this.ncbExecuteTime.Checked = true;
            }
            catch
            { }

            this.SetColor();
            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            return 1;
        }

        protected int SetColor()
        {
            for (int rowIndex = 0; rowIndex < this.curIDataDetail.FpSpread.Sheets[0].RowCount; rowIndex++)
            {
                this.SetColor(rowIndex);
            }

            return 1;
        }

        protected int SetColor(int rowIndex)
        {
            if (rowIndex < 0)
            {
                return 0;
            }

            string prePrice = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "调价前零售价");
            string curPrice = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "调价后零售价");
            this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "差价", FS.FrameWork.Function.NConvert.ToDecimal(curPrice) - FS.FrameWork.Function.NConvert.ToDecimal(prePrice));

            if (FS.FrameWork.Function.NConvert.ToDecimal(curPrice) > FS.FrameWork.Function.NConvert.ToDecimal(prePrice))
            {
                this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = Color.Blue;
            }
            else if (FS.FrameWork.Function.NConvert.ToDecimal(curPrice) == FS.FrameWork.Function.NConvert.ToDecimal(prePrice))
            {
                this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = Color.Black;
            }
            else
            {
                this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].ForeColor = Color.Red;
            }

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
            //删除数据在删除操作时就已经确认过了，而且对于已经保存的数据在删除时候就已经从数据库删除了，所以只需要新增和修改的
            if (this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified) != null && (this.curAdjustPriceInfo.State == "0" || string.IsNullOrEmpty(this.curAdjustPriceInfo.State)))
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

        #region 重复调价提示
        private bool ContinueAfterCheckReadjust(FS.HISFC.Models.Pharmacy.Item item)
        {
            ArrayList alInfo = null;
            if (this.IsSingleDept)
            {
                FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
                FS.HISFC.Models.Pharmacy.Storage storage = storageMgr.GetStockInfoByDrugCode(this.priveDept.ID, item.ID);
                if (storage == null)
                {
                    Function.ShowMessage("单科调价判断库存时发生错误：" + storageMgr.Err, MessageBoxIcon.Error);
                    return false;
                }
                if (storage.StoreQty > 0)
                {
                    Function.ShowMessage(item.UserCode + "   " + item.Name + "   " + item.Specs + "   目前还有库存，不允许单科调价", MessageBoxIcon.Information);
                    return false;
                }
                alInfo = this.adjustMgr.QueryUnexcutedBillList(this.priveDept.ID, item.ID);
            }
            else
            {
                alInfo = this.adjustMgr.QueryGlobalUnexcutedBillList(item.ID);
            }
            if (alInfo == null)
            {
                Function.ShowMessage("检测是否已经存在调价信息时获取调价单列表发生错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                return false;
            }
            if (alInfo.Count > 0)
            {
                for (int index = alInfo.Count - 1; index > -1; index--)
                {
                    if ((alInfo[index] as FS.HISFC.Models.Pharmacy.AdjustPrice).ID == this.curAdjustPriceInfo.ID)
                    {
                        alInfo.RemoveAt(index);
                    }
                }
                if (alInfo.Count > 0)
                {
                    frmRetailHistoryAdjust frmRetailHistoryAdjust = new frmRetailHistoryAdjust(alInfo, item.Name + "[" + item.Specs + "]存在未执行的调价记录");
                    if (frmRetailHistoryAdjust.ShowDialog() != DialogResult.OK)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }
            if (this.IsSingleDept)
            {
                return this.SetPriveDept();
            }
            if (!Function.JugePrive(this.Class2Code, this.Class3Code))
            {
                Function.ShowMessage("您没有权限！", MessageBoxIcon.Information);
                return -1;
            }
            this.priveDept = ((FS.HISFC.Models.Base.Employee)this.adjustMgr.Operator).Dept.Clone();
            this.nlbInfo.Text = "您选择的是【" + this.priveDept.Name + "】，全院调价方式";

            return 1;
        }
        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            int param = Function.ChoosePrivDept(this.Class2Code, this.Class3Code, ref this.priveDept);
            if (param == 0 || param == -1 || this.priveDept == null || string.IsNullOrEmpty(this.priveDept.ID))
            {
                return -1;
            }
            this.nlbInfo.Text = "您选择的是【" + this.priveDept.Name + "】，单科调价方式";
            return 1;
        }
        #endregion

        #region 事件

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "制单")
            {
                //新建的数据要保留，否则清空
                if (this.curAdjustPriceInfo != null && !string.IsNullOrEmpty(this.curAdjustPriceInfo.State))
                {
                    this.nlbAdustInfo.Text = "新建调价单，制单人：" + this.adjustMgr.Operator.Name;
                    this.ncbExecuteTime.Checked = false;
                    this.ndtpExecuteTime.Value = DateTime.Now;
                    this.hsAdjustPrice.Clear();
                    this.dtDetail.Clear();
                    this.dtDetail.AcceptChanges();
                    this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
                    this.curAdjustPriceInfo = new FS.HISFC.Models.Pharmacy.AdjustPrice();
                }

                this.curTreeView.Visible = false;
                this.curIDataChooseList.SetFocusToFilter();

                this.toolBarService.SetToolButtonEnabled("列表", true);
                this.toolBarService.SetToolButtonEnabled("制单", false);
            }
            else if (e.ClickedItem.Text == "列表")
            {
                this.curTreeView.Visible = true;
                this.toolBarService.SetToolButtonEnabled("列表", false);
                this.toolBarService.SetToolButtonEnabled("制单", true);
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
                frmSetRetailPrice frmSetRetailPrice = new frmSetRetailPrice();
                frmSetRetailPrice.StartPosition = FormStartPosition.CenterScreen;
                frmSetRetailPrice.ShowDialog();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (!this.GiveUpChanges())
            {
                return 0;
            }
            this.ShowList();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Init();
            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            this.ShowList();
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
                FS.HISFC.Models.Pharmacy.Item item = this.adjustMgr.GetItem(values[0]);
                if (item == null)
                {
                    Function.ShowMessage("请与系统管理员联系，获取药品基本信息出错：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    if (!this.ContinueAfterCheckReadjust(item))
                    {
                        return;
                    }
                    FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new FS.HISFC.Models.Pharmacy.AdjustPrice();
                    adjustPrice.Item = item;

                    if (this.AddObjectToDataTable(adjustPrice) == 1)
                    {
                        this.curIDataDetail.FpSpread.Sheets[0].SetActiveCell(this.curIDataDetail.FpSpread.Sheets[0].RowCount - 1, 0);
                        this.curIDataDetail.SetFocus();
                        this.SetColor(this.curIDataDetail.FpSpread.Sheets[0].RowCount - 1);
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
            if (this.curAdjustPriceInfo != null && this.curAdjustPriceInfo.State == "0")
            {
                this.curTreeView.Visible = false;
                this.curIDataChooseList.SetFocusToFilter();

                this.toolBarService.SetToolButtonEnabled("列表", true);
                this.toolBarService.SetToolButtonEnabled("制单", false);
            }
        }

        void curTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is FS.HISFC.Models.Pharmacy.AdjustPrice)
            {
                if (this.GiveUpChanges())
                {
                    this.curAdjustPriceInfo = e.Node.Tag as FS.HISFC.Models.Pharmacy.AdjustPrice;
                    this.ShowDetail(this.curAdjustPriceInfo);
                }

            }
        }

        void curIDataDetail_FilterTextChanged()
        {
            this.SetColor();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.curIDataDetail.FpSpread.Sheets[0].RowCount > 0)
                {
                    if (string.IsNullOrEmpty(this.curAdjustPriceInfo.State) || this.curAdjustPriceInfo.State == "0")
                    {
                        int colIndexFormula = this.curIDataDetail.FpSpread.GetColumnIndex(0, "公式");
                        if (this.curIDataDetail.IsContainsFocus && this.curIDataDetail.FpSpread.Sheets[0].ActiveColumnIndex == colIndexFormula)
                        {
                            string formula = this.curIDataDetail.FpSpread.GetCellText(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "公式");
                            if (!string.IsNullOrEmpty(formula))
                            {
                                this.curIDataDetail.FpSpread.SetCellValue(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "调价后零售价", FS.FrameWork.Public.String.ExpressionVal(formula));
                            }
                        }
                        string prePrice = this.curIDataDetail.FpSpread.GetCellText(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "调价前零售价");
                        string curPrice = this.curIDataDetail.FpSpread.GetCellText(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "调价后零售价");
                        this.curIDataDetail.FpSpread.SetCellValue(0, this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex, "差价", FS.FrameWork.Function.NConvert.ToDecimal(curPrice) - FS.FrameWork.Function.NConvert.ToDecimal(prePrice));

                        this.SetColor(this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }


        #endregion
    }
}
