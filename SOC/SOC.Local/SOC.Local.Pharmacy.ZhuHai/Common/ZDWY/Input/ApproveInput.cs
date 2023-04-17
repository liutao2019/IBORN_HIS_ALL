using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Common.ZDWY.Input
{
    /// <summary>
    /// [功能描述: 核准入库]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-4]<br></br>
    /// 说明：仅用于对出库后核准，增加本科库存
    /// </summary>
    public class ApproveInput : FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz
    {
        private FS.FrameWork.Models.NeuObject curStockDept = null;
        private FS.FrameWork.Models.NeuObject curFromDept = null;
        private FS.FrameWork.Models.NeuObject curPriveType = null;

        private System.Data.DataTable dtDetail = null;

        private string settingFileName = "";
        private uint costDecimals = 2;

        private decimal totPurchaseCost = 0;
        private decimal totWholeSaleCost = 0;
        private decimal totRetailCost = 0;
        private decimal totRowQty = 0;

        private Hashtable hsInput = new Hashtable();

        /// <summary>
        /// 入库相关业务流程处理对应的数据列表选择对象(已被接口标准化)
        /// </summary>
        private SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList curIDataChooseList = null;

        /// <summary>
        /// 获取入库类别获取业务流程对应的数据明细显示控件（接口）实例
        /// </summary>
        private SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail curIDataDetail = null;

        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting curChooseDataSetting = null;

        //private FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
        SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new  FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

        FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList frmBillChooseList = null;
        int[] curBillChooseColumnIndexs = null;

        #region IBaseBiz 成员 初始化

        public int Init(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList IDataChooseList, SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail IDataDetail, ToolStrip toolStrip)
        {
            if (IDataDetail == null)
            {
                Function.ShowMessage("初始化失败：curIDataDetail为null，请与系统管理员联系!", MessageBoxIcon.Error);
                return -1;
            }
            if (IDataChooseList == null)
            {
                Function.ShowMessage("初始化失败：IDataChooseList为null，请与系统管理员联系!", MessageBoxIcon.Error);
                return -1;
            }
            this.curIDataDetail = IDataDetail;
            this.curIDataChooseList = IDataChooseList;
            this.curIDataChooseList.Clear();
            this.curIDataDetail.Clear();

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ApproveInputSetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "05";
                curPriveType.Name = "核准入库";
                curPriveType.Memo = "16";
            }

            this.costDecimals = SOC.HISFC.Components.Pharmacy.Function.GetCostDecimals("0310", curPriveType.Memo);

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

            param = this.InitToolStrip(toolStrip);


            return param;
        }
        #endregion

        #region IBaseBiz 成员 库存科室 来源 目标


        public int SetStockDept(FS.FrameWork.Models.NeuObject stockDept)
        {
            if (stockDept == null || string.IsNullOrEmpty(stockDept.ID))
            {
                Function.ShowMessage("库存科室为null，请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            this.curStockDept = stockDept;
            return 1;
        }

        public int SetTargetDept(FS.FrameWork.Models.NeuObject targetDept)
        {
            return 1;
        }

        public int SetFromDept(FS.FrameWork.Models.NeuObject fromDept)
        {
            if (fromDept == null)
            {
                Function.ShowMessage("来源科室为null，请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            this.curFromDept = fromDept;
            if (this.curIDataChooseList != null)
            {
                this.FreshDataChooseList();
            }
            return 1;
        }

        #endregion

        #region IBaseBiz 成员 工具栏按钮事件
        public int ToolbarAfterClick(string text)
        {
            if (text == "删除")
            {
                return this.Delete();
            }
            else if (text == "保存")
            {
                return this.Save();
            }
            else if (text == "出库单")
            {
                return this.ChooseOutputBill();
            }
            return 1;
        }
        #endregion

        #region IBaseBiz 成员 其它

        public void Dispose()
        {
            if (this.curStockDept != null)
            {
                this.curStockDept.Dispose();
            }
            if (this.curFromDept != null)
            {
                this.curFromDept.Dispose();
            }
            if (this.curPriveType != null)
            {
                this.curPriveType.Dispose();
            }
            try
            {
                this.curIDataDetail.FpSpread.Sheets[0].RowCount = 0;
                this.curIDataDetail.FpSpread.Sheets[0].ColumnCount = 0;
                this.curIDataDetail.FpSpread.Sheets[0].DataSource = null;
                this.hsInput.Clear();
                this.dtDetail.Clear();
                this.dtDetail.AcceptChanges();
                this.dtDetail.Dispose();
                this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
            }
            catch { }
        }

        public System.Windows.Forms.UserControl InputInfoControl
        {
            get { return null; }
        }

        public FS.FrameWork.Models.NeuObject PriveType
        {
            get
            {
                return this.curPriveType;
            }
            set
            {
                this.curPriveType = value;
            }
        }

        public int Clear()
        {
            totPurchaseCost = 0;
            totWholeSaleCost = 0;
            totRetailCost = 0;
            totRowQty = 0;

            hsInput.Clear();
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();

            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Clear();
            }

            return 1;
        }

        public ArrayList GetFromDeptArray(ref string fromDeptTypeName)
        {
            fromDeptTypeName = "来源科室：";
            return SOC.HISFC.Components.Pharmacy.Function.QueryManagerStockDept();
        }

        public ArrayList GetTargetDeptArray()
        {
            return null;
        }

        #endregion

        #region IBaseBiz 成员 设置界面的科室

        /// <summary>
        /// 设置出库界面的目标科室
        /// </summary>
        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.SetTargetDeptHander curSetTargetDeptEven;

        /// <summary>
        /// 设置入库界面的供货公司、来源科室
        /// </summary>
        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.SetFromDeptHander curSetFromDeptEven;

        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.SetFromDeptHander FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz.SetFromDeptEven
        {
            get
            {
                return curSetFromDeptEven;
            }
            set
            {
                curSetFromDeptEven = value;
            }
        }

        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.SetTargetDeptHander FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz.SetTargetDeptEven
        {
            get
            {
                return curSetTargetDeptEven;
            }
            set
            {
                curSetTargetDeptEven = value;
            }
        }

        #endregion

        #region IBaseBiz 成员 ProcessDialogKey

        public bool ProcessDialogKey(Keys keyData)
        {
            return false;
        }

        #endregion

        #region IBaseBiz 成员 领药人


        public int SetGetPerson(FS.FrameWork.Models.NeuObject getPerson)
        {
            return 1;
        }

        #endregion

        #region 方法

        #region 初始化

        protected int InitToolStrip(ToolStrip toolStrip)
        {

            for (int index = toolStrip.Items.Count - 1; index > -1; index--)
            {
                if (toolStrip.Items[index].Text == "退出")
                {
                    continue;
                }
                else if (toolStrip.Items[index].Text == "保存")
                {
                    continue;
                }
                else if (toolStrip.Items[index].Text == "删除")
                {
                    continue;
                }
                else if (toolStrip.Items[index].Text == "")
                {
                    continue;
                }
                toolStrip.Items.RemoveAt(index);
            }

            ToolStripButton tb = new ToolStripButton("出库单");
            tb.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C出库单);
            tb.ToolTipText = "显示出库单列表";
            tb.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            tb.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStrip.Items.Insert(0, tb);


            return 1;
        }

        /// <summary>
        /// 初始化数据选择关键属性
        /// </summary>
        /// <returns></returns>
        protected int InitChooseData()
        {
            string errInfo = "";
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = SOC.HISFC.Components.Pharmacy.Function.GetBizChooseDataSetting("0310", curPriveType.Memo, curPriveType.ID, "0", ref errInfo);
            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.ApprovePrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.ApprovePrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
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
                    "出库流水号", 
                    "出库单号",
                    "药品编码", 
                    "自定义码",
                    "名称", 
                    "规格", 
                    "出库数量", 
                    "单位", 
                    "出库时间", 
                    "拼音码", 
                    "五笔码", 
                    "通用名拼音码", 
                    "通用名五笔码" };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   0f,// "出库流水号", 
                   0f,// "出库单号", 
                   0f,// "药品编码", 
                   60f,// "自定义码",
                   120f,// "名称", 
                   100f,// "规格", 
                   60f,// "出库数量", 
                   15f,// "单位", 
                   60f,// "出库时间", 
                   0f,// "拼音码", 
                   0f,// "五笔码", 
                   0f,// "通用名拼音码", 
                   0f// "通用名五笔码"
                };
                chooseDataSetting.IsNeedDrugType = false;

                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;            

                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "出库流水号", 
                   t,// "出库单号", 
                   t,// "药品编码", 
                   t,// "名称", 
                   t,// "规格", 
                   t,// "出库数量", 
                   t,// "单位", 
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ApproveInputL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curIDataChooseList.SettingFileName = chooseDataSetting.SettingFileName;
            }

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
                Function.ShowMessage("程序发生错误：出库明细数据显示控件没有初始化", System.Windows.Forms.MessageBoxIcon.Error);
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "药品编码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "名称", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 90f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "数量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "加成价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "加成金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "出库单号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "出库科室", 120f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "有效期", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "备注", 60f);
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

                FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty.DecimalPlaces = 0;
                nQty.ReadOnly = true;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "数量", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "加成价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "加成金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "出库单号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "出库科室", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "有效期", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "备注", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "拼音码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "五笔码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "主键", t);

                this.curIDataDetail.FpSpread.SaveSchema(this.settingFileName);
            }
            else
            {
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            }
            //this.curIDataDetail.FpSpread.EditModePermanent = true;
            //this.curIDataDetail.FpSpread.EditMode = true;
            //this.curIDataDetail.FpSpread.EditModeReplace = true;

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
                    new DataColumn("批号",typeof(string)),
                    new DataColumn("名称",typeof(string)),
                    new DataColumn("规格",typeof(string)),
                    new DataColumn("数量",typeof(string)),
                    new DataColumn("单位",typeof(string)),
                    new DataColumn("购入价",typeof(decimal)),
                    new DataColumn("购入金额",typeof(decimal)),
                    new DataColumn("加成价",typeof(decimal)),
                    new DataColumn("加成金额",typeof(decimal)),
                    new DataColumn("零售价",typeof(decimal)),
                    new DataColumn("零售金额",typeof(decimal)),
                    new DataColumn("出库单号",typeof(string)),
                    new DataColumn("来源科室",typeof(string)),
                    new DataColumn("有效期",typeof(string)),
                    new DataColumn("备注",typeof(string)),
                    new DataColumn("拼音码",typeof(string)),
                    new DataColumn("五笔码",typeof(string)),
                    new DataColumn("主键",typeof(string)),

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                dc.ReadOnly = true;
            }

            this.dtDetail.DefaultView.AllowNew = true;
            this.dtDetail.DefaultView.AllowEdit = true;
            this.dtDetail.DefaultView.AllowDelete = true;

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtDetail.Columns["主键"];

            this.dtDetail.PrimaryKey = keys;

            return 1;
        }
        #endregion

        #region DataTable数据添加

        /// <summary>
        /// 向DataTable添加入库实体，显示入库明细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected int AddInputObjectToDataTable(FS.HISFC.Models.Pharmacy.Input input)
        {
            if (input == null)
            {
                Function.ShowMessage("向DataTable中添加入库信息失败：入库信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtDetail == null)
            {
                Function.ShowMessage("向DataTable中添加入库信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            if (this.hsInput.Contains(input.OutBillNO + input.OutSerialNO.ToString()))
            {
                Function.ShowMessage("单号" + input.InListNO + "中的" + input.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsInput.Add(input.OutBillNO + input.OutSerialNO.ToString(), input);
            }

            this.totPurchaseCost += input.PurchaseCost;
            this.totWholeSaleCost += input.WholeSaleCost;
            this.totRetailCost += input.RetailCost;
            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 加成总金额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }

            //标记一下数据，双击FarPoint修改时可以区分已经添加了的
            input.Class2Type = "0310";
            input.PrivType = curPriveType.ID;
            input.SystemType = curPriveType.Memo;
            input.SourceCompanyType = "1";

            DataRow row = this.dtDetail.NewRow();

            row["自定义码"] = input.Item.UserCode;
            row["药品编码"] = input.Item.ID;
            row["批号"] = input.BatchNO;
            row["名称"] = input.Item.Name;
            row["规格"] = input.Item.Specs;
            if (input.ShowState == "1")
            {
                row["数量"] = input.Quantity / input.Item.PackQty;
                row["单位"] = input.Item.PackUnit;
            }
            else
            {
                row["数量"] = input.Quantity;
                row["单位"] = input.Item.MinUnit;
            }
            row["购入价"] = input.Item.PriceCollection.PurchasePrice;
            row["购入金额"] = input.PurchaseCost;
            row["加成价"] = input.Item.PriceCollection.WholeSalePrice;
            row["加成金额"] = input.WholeSaleCost;
            row["零售价"] = input.Item.PriceCollection.RetailPrice;
            row["零售金额"] = input.RetailCost;
            row["出库单号"] = input.OutListNO;
            row["来源科室"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(input.Company.ID);
            row["有效期"] = input.ValidTime;

            row["拼音码"] = input.Item.SpellCode;
            row["五笔码"] = input.Item.WBCode;
            row["主键"] = input.OutBillNO + input.OutSerialNO.ToString();

            this.dtDetail.Rows.Add(row);

            return 1;
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        protected int Delete()
        {
            //{83A75FE2-89DE-4b11-9BCB-6092837FE537}
            MessageBox.Show("核准入库不允许修改内容！");
            return 0;
            if (this.curIDataDetail == null || this.curIDataDetail.FpSpread == null || this.curIDataDetail.FpSpread.Sheets.Count == 0)
            {
                return 0;
            }
            int rowIndex = this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex;
            string keys = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "主键");
            string tradeName = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "名称");

            DialogResult dr = MessageBox.Show("确定删除第" + (rowIndex + 1).ToString() + "行的 " + tradeName + " 吗？", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }
            if (hsInput.Contains(keys))
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { keys });
                if (row != null)
                {
                    this.dtDetail.Rows.Remove(row);
                }
                hsInput.Remove(keys);
                this.SetTotInfo();
            }

            return 1;
        }

        #endregion

        #region 保存

        /// <summary>
        /// 有效性判断
        /// </summary>
        /// <returns>填写有效 返回True 否则返回 False</returns>
        public virtual int CheckValid()
        {
            if (this.curFromDept == null || string.IsNullOrEmpty(this.curFromDept.ID))
            {
                Function.ShowMessage("请选择来源科室！", MessageBoxIcon.Information);
                return -2;
            }

            if (this.dtDetail.Rows.Count == 0)
            {
                Function.ShowMessage("请选择要入库的药品！", MessageBoxIcon.Information);
                return -4;
            }

        
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                string key = dr["主键"].ToString();

                FS.HISFC.Models.Pharmacy.Input input = this.hsInput[key] as FS.HISFC.Models.Pharmacy.Input;

                if (this.curFromDept != null && input.Company.ID != this.curFromDept.ID)
                {
                    Function.ShowMessage(input.Item.Name + "不是当前来源科室，请更改！", MessageBoxIcon.Information);
                    return -2;
                }
            }

            return 1;
        }

        protected int Save()
        {
            this.dtDetail.DefaultView.RowFilter = "11=11";
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            int param = this.CheckValid();

            if (param < 0)
            {
                if (param == -2)
                {
                    this.curIDataDetail.SetFocusToFilter();
                }
                return param;
            }


             ArrayList alInput = new ArrayList();
            string errInfo = "";

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行保存操作..请稍候");
            System.Windows.Forms.Application.DoEvents();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //当天操作日期
            DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();

            //入库单据号
            string billNO = "";

            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                string key = dr["主键"].ToString();

                input = this.hsInput[key] as FS.HISFC.Models.Pharmacy.Input;

                //入库单号
                if (string.IsNullOrEmpty(billNO))
                {
                    billNO = SOC.HISFC.Components.Pharmacy.Function.GetBillNO(this.curStockDept.ID, "0310", this.PriveType.ID, ref errInfo);
                    if (string.IsNullOrEmpty(billNO) || billNO == "-1")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("获取最新入库单号出错:" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                input.InListNO = billNO;

                //是否特殊入库 0 否 1 是
                input.SpecialFlag = "0";

                //库存科室
                input.StockDept = this.curStockDept;

                //用户类型    
                input.PrivType = this.PriveType.ID;

                //系统类型     
                input.SystemType = this.PriveType.Memo;

                //供货单位 
                //input.Company = this.curFromDept;

                //入库后库存数量，这个最好不要赋值，获取库存影响效率，否则得取库存汇总信息
                //可以考虑根据医院需求在本地化扩展业务中处理台账
                decimal storageNum = 0;
                if (this.itemMgr.GetStorageNum(input.StockDept.ID, input.Item.ID, out storageNum) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("获取库存数量时出错" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                input.StoreQty = storageNum + input.Quantity;               //入库后库存数量
                input.StoreCost = Math.Round(input.StoreQty / input.Item.PackQty * input.Item.PriceCollection.RetailPrice, 3);

                //入库申请人作为入库人，以后都不要更改
                input.Operation.ApplyQty = input.Quantity;                          //入库申请量
                input.Operation.ApplyOper.ID = this.itemMgr.Operator.ID;
                input.Operation.ApplyOper.OperTime = sysTime;

                input.Operation.Oper.ID = this.itemMgr.Operator.ID;
                input.Operation.Oper.OperTime = sysTime;

                input.State = "2";

                input.Operation.ExamQty = input.Quantity;
                input.Operation.ExamOper.OperTime = sysTime;
                input.Operation.ExamOper.ID = input.Operation.Oper.ID;

                input.Operation.ApproveQty = input.Quantity;
                input.Operation.ApproveOper.OperTime = sysTime;
                input.Operation.ApproveOper.ID = input.Operation.Oper.ID;

                input.PayState = "0";

                //入库时间，这个比较关键，必须赋值，月结，各种查询都需要入库时间
                input.InDate = sysTime;

                //供货单位类型 1 院内科室 2 供货公司 3 扩展
                input.SourceCompanyType = "1";

                if (this.itemMgr.ApproveInput(input.Clone(), "1") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("入库保存失败：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                alInput.Add(input);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (SOC.HISFC.Components.Pharmacy.Function.DealExtendBiz("0310", this.PriveType.ID, alInput, ref errInfo) == -1)
            {
                Function.ShowMessage("入库已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            SOC.HISFC.Components.Pharmacy.Function.PrintBill("0310", this.PriveType.ID, alInput);


            this.ClearData();
            this.FreshDataChooseList();
            return 1;
        }
        
        #endregion

        #region 出库单按钮调用
        /// <summary>
        /// 选择入库单
        /// </summary>
        public int ChooseOutputBill()
        {
            if (this.curIDataDetail.FpSpread.Sheets[0].RowCount > 0)
            {
                DialogResult dr = MessageBox.Show("选择入库单将清空现有数据，是否继续！", "提示>>", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return 0;
                }
                this.ClearData();
            }

            string errInfo = "";

            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = SOC.HISFC.Components.Pharmacy.Function.GetBizChooseDataSetting("0310", curPriveType.Memo, curPriveType.ID, "1", ref errInfo);

            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();


                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.ApprovePrive.ChooseBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.ApprovePrive.ChooseBill,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                string companyNO = "All";
                if (this.curFromDept != null && this.curFromDept.ID != "")
                {
                    companyNO = this.curFromDept.ID;
                }
                //替换库存科室和供货公司，时间和状态不替换
                SQL = string.Format(SQL, "{0}", "{1}", "{2}", companyNO, this.curStockDept.ID);

                chooseDataSetting.ListTile = "出库单列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0, 1 };
                curBillChooseColumnIndexs = chooseDataSetting.ColumnIndexs;
                chooseDataSetting.Filter = "";

                chooseDataSetting.ColumnLabels = new string[] 
                { 
                    "出库单号", 
                    "来源科室", 
                    "科室名称", 
                    "出库时间", 
                    "出库人工号"
                };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   100f,// "出库单号", 
                   60f,// "来源科室", 
                   140f,// "科室名称", 
                   120f,// "出库时间", 
                   90f,// "出库人工号"
                };
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "出库单号", 
                   t,// "来源科室", 
                   t,// "科室名称", 
                   t,// "出库时间",
                   t,// "出库人工号"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ApproveInputL1Setting.xml";
            }

            ArrayList alState = new ArrayList();

            FS.FrameWork.Models.NeuObject state1 = new FS.FrameWork.Models.NeuObject();
            state1.ID = "1";
            state1.Name = "审批";      

            alState.Add(state1);

            if (this.frmBillChooseList == null)
            {
                this.frmBillChooseList = new FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList();
            }

            frmBillChooseList.Init();
            frmBillChooseList.Set(chooseDataSetting.ListTile, chooseDataSetting.SQL, alState, chooseDataSetting.SettingFileName, chooseDataSetting.CellTypes, chooseDataSetting.ColumnLabels, chooseDataSetting.ColumnWiths);

            frmBillChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(frmBillChooseList_ChooseCompletedEvent);
            frmBillChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(frmBillChooseList_ChooseCompletedEvent);

            frmBillChooseList.ShowDialog();


            return 1;
        }
        #endregion

        #region 清空
        protected int ClearData()
        {
            totPurchaseCost = 0;
            totWholeSaleCost = 0;
            totRetailCost = 0;
            totRowQty = 0;

            hsInput.Clear();
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);


            return 1;
        }

        #endregion

        #region 刷新数据选择列表
        protected int FreshDataChooseList()
        {

            string stockDeptNO = "";
            if (this.curStockDept != null)
            {
                stockDeptNO = this.curStockDept.ID;
            }
            string fromDeptNO = "All";
            if (this.curFromDept != null && !string.IsNullOrEmpty(this.curFromDept.ID))
            {
                fromDeptNO = this.curFromDept.ID;
            }

            if (curChooseDataSetting == null)
            {
                return 0;
            }

            string SQL = string.Format(this.curChooseDataSetting.SQL, stockDeptNO, fromDeptNO);


            this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, SQL, curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
            this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);
            this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
            this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);

            return 1;
        }
        #endregion

        #region 金额计算
        protected virtual int SetTotInfo()
        {
            this.totPurchaseCost = 0;
            this.totWholeSaleCost = 0;
            this.totRetailCost = 0;
            this.totRowQty = 0;
            for (int rowIndex = 0; rowIndex < this.curIDataDetail.FpSpread.Sheets[0].Rows.Count; rowIndex++)
            {
                string keys = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "主键");

                if (hsInput.Contains(keys))
                {
                    FS.HISFC.Models.Pharmacy.Input input = hsInput[keys] as FS.HISFC.Models.Pharmacy.Input;
                    string strQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "数量");
                    decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(strQty);
                    if (input.ShowState == "1")
                    {
                        qty = input.Item.PackQty * qty;
                    }

                    decimal purchaseCost = input.Item.PriceCollection.PurchasePrice * (qty / input.Item.PackQty);
                    purchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(purchaseCost.ToString("F" + this.costDecimals.ToString()));
                    decimal wholeSaleCost = input.Item.PriceCollection.WholeSalePrice * (qty / input.Item.PackQty);
                    wholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(wholeSaleCost.ToString("F" + this.costDecimals.ToString()));
                    decimal retailCost = input.Item.PriceCollection.RetailPrice * (qty / input.Item.PackQty);
                    retailCost = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()));


                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "购入金额", purchaseCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "零售金额", retailCost);

                    this.totRowQty++;
                    this.totPurchaseCost += purchaseCost;
                    this.totWholeSaleCost += wholeSaleCost;
                    this.totRetailCost += retailCost;

                    this.curIDataDetail.Info = "当前品种数：" + this.totRowQty.ToString("F0")
                       + ", 购入金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 加成金额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 零售金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');


                }
            }

            return 1;
        }
        #endregion

        #endregion

        #region 事件


        void IDataChooseList_ChooseCompletedEvent()
        {
            string[] values = this.curIDataChooseList.GetChooseData(this.curChooseDataSetting.ColumnIndexs);
            if (values == null || values.Length == 0)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                ArrayList alOutput = this.itemMgr.QueryOutputList(values[0]);
                if (alOutput == null)
                {
                    Function.ShowMessage("请与系统管理员联系，获取入库信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (FS.HISFC.Models.Pharmacy.Output output in alOutput)
                    {
                        if (output.Quantity - output.Operation.ReturnQty <= 0)
                        {
                            continue;
                        }
                        if (output.State == "2")
                        {
                            //因为存在单内序号output.SerialNO不同但是output.ID相同，所以同一output.ID不同output.SerialNO是两行记录，可以分开核准
                            //Function.ShowMessage(output.Item.Name + "已经核准，请退出界面重试!", MessageBoxIcon.Information);
                            //return;
                            continue;
                        }
                        FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();

                        if (this.curFromDept == null || string.IsNullOrEmpty(this.curFromDept.ID))
                        {
                            this.curFromDept = output.StockDept.Clone();
                            if (this.curSetFromDeptEven != null)
                            {
                                this.curSetFromDeptEven(this.curFromDept);
                            }
                        }
                        if (this.curFromDept != null && !string.IsNullOrEmpty(this.curFromDept.ID) && this.curFromDept.ID != output.StockDept.ID)
                        {
                            Function.ShowMessage(output.Item.Name + "的出库科室不是您选择的来源科室，请修改!", MessageBoxIcon.Information);
                            return;
                        }
                        FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(output.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系，获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        input.StockDept = curStockDept;                   //申请科室
                        input.PrivType = this.curPriveType.ID;                 //入库分类
                        input.SystemType = this.curPriveType.Memo;             //系统类型
                        input.State = "2";                                              //状态 核准
                        input.Company = output.StockDept;
                        //input.TargetDept = curStockDept;                //目标单
                        input.Producer = output.Producer;                 //生产厂家
                        input.Item = output.Item;                                       //药品实体信息
                        input.OutBillNO = output.ID;                                    //出库流水号
                        input.OutListNO = output.OutListNO;                             //出库单据号
                        input.OutSerialNO = output.SerialNO;                            //序号
                        input.SerialNO = output.SerialNO;
                        input.BatchNO = output.BatchNO;                                 //批号
                        input.ValidTime = output.ValidTime;                             //有效期
                        input.Quantity = output.Quantity - output.Operation.ReturnQty;                               //数量
                        input.PlaceNO = output.PlaceNO;                                 //货位号
                        input.GroupNO = output.GroupNO;                                 //批次
                        input.Operation = output.Operation;                             //操作信息

                        //金额赋值，保证出库、入库是一致的
                        input.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(((output.Quantity - output.Operation.ReturnQty) / output.Item.PackQty * input.Item.PriceCollection.PurchasePrice).ToString("F" + this.costDecimals.ToString()));
                        input.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(((output.Quantity - output.Operation.ReturnQty) / output.Item.PackQty * input.Item.PriceCollection.RetailPrice).ToString("F" + this.costDecimals.ToString()));
                        input.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(((output.Quantity - output.Operation.ReturnQty) / output.Item.PackQty * input.Item.PriceCollection.WholeSalePrice).ToString("F" + this.costDecimals.ToString()));

                        input.Item.UserCode = item.UserCode;
                        input.Item.WBCode = item.WBCode;
                        input.Item.SpellCode = item.SpellCode;
                        input.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                        input.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                        if (this.AddInputObjectToDataTable(input) != -1)
                        {
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
        }

        void frmBillChooseList_ChooseCompletedEvent()
        {
            string[] values = this.frmBillChooseList.GetChooseData(this.curBillChooseColumnIndexs);
            if (values == null || values.Length < 2)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                string outBillNO = values[0];
                string outStockDeptNO = values[1];

                ArrayList alOutput = this.itemMgr.QueryOutputInfo(outStockDeptNO, outBillNO,"A");
                if (alOutput == null)
                {
                    Function.ShowMessage("查询入库数据发生错误：" + itemMgr.Err + "\n请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }

                foreach (FS.HISFC.Models.Pharmacy.Output output in alOutput)
                {
                    if (output.Quantity - output.Operation.ReturnQty <= 0)
                    {
                        continue;
                    }
                    if (output.State == "2")
                    {
                        continue;
                    }
                    FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();

                    if (this.curFromDept == null || string.IsNullOrEmpty(this.curFromDept.ID))
                    {
                        this.curFromDept = output.StockDept.Clone();
                        if (this.curSetFromDeptEven != null)
                        {
                            this.curSetFromDeptEven(this.curFromDept);
                        }
                    }
                    if (this.curFromDept != null && !string.IsNullOrEmpty(this.curFromDept.ID) && this.curFromDept.ID != output.StockDept.ID)
                    {
                        Function.ShowMessage(output.Item.Name + "的出库科室不是您选择的来源科室，请修改!", MessageBoxIcon.Information);
                        return;
                    }
                   
                    input.StockDept = curStockDept;                   //申请科室
                    input.PrivType = this.curPriveType.ID;                 //入库分类
                    input.SystemType = this.curPriveType.Memo;             //系统类型
                    input.State = "2";                                              //状态 核准
                    input.Company = output.StockDept;
                    //input.TargetDept = curStockDept;                //目标单
                    input.Item = output.Item;                                       //药品实体信息
                    input.OutBillNO = output.ID;                                    //出库流水号
                    input.OutListNO = output.OutListNO;                             //出库单据号
                    input.OutSerialNO = output.SerialNO;                            //序号
                    input.SerialNO = output.SerialNO;
                    input.BatchNO = output.BatchNO;                                 //批号
                    input.ValidTime = output.ValidTime;                             //有效期
                    input.Quantity = output.Quantity-output.Operation.ReturnQty;                               //数量
                    input.PlaceNO = output.PlaceNO;                                 //货位号
                    input.GroupNO = output.GroupNO;                                 //批次
                    input.Operation = output.Operation;                             //操作信息
                    input.ShowState = output.ShowState;                             //显示方式 add by cao-lin
                    input.PurchaseCost = output.PurchaseCost;
                    input.RetailCost = output.RetailCost;
                    input.WholeSaleCost = output.WholeSaleCost;
                    input.Producer = output.Producer;

                    if (alOutput.Count < 50 && SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper == null)
                    {
                        FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(output.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        input.Item.UserCode = item.UserCode;
                        input.Item.WBCode = item.WBCode;
                        input.Item.SpellCode = item.SpellCode;
                        input.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                        input.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        input.Item.UserCode = item.UserCode;
                        input.Item.WBCode = item.WBCode;
                        input.Item.SpellCode = item.SpellCode;
                        input.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                        input.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                    }
                    if (this.AddInputObjectToDataTable(input) == -1)
                    {
                        break;
                    }
                }

            }
        }

        #endregion

    }
}