using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;


namespace FS.SOC.Local.Pharmacy.ZhuHai.Common.ZDWY.Plan
{
    /// <summary>
    /// 入库退库申请
    /// </summary>
    public class InnerBackInputApply : FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz
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

        private Hashtable hsInnerInputApply = new Hashtable();
        private ArrayList alFormulaPlan = new ArrayList();

        FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList frmBillChooseList = null;
        int[] curBillChooseColumnIndexs = null;

        /// <summary>
        /// 入库相关业务流程处理对应的数据列表选择对象(已被接口标准化)
        /// </summary>
        private SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList curIDataChooseList = null;

        /// <summary>
        /// 获取入库类别获取业务流程对应的数据明细显示控件（接口）实例
        /// </summary>
        private SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail curIDataDetail = null;

        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting curChooseDataSetting = null;
        
        private FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

        #region IBaseBiz 成员 初始化

        public int Init(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList IDataChooseList, SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail IDataDetail, ToolStrip toolStrip)
        {
            if (IDataDetail == null)
            {
                Function.ShowMessage("初始化失败：IDataDetail为null，请与系统管理员联系!", MessageBoxIcon.Error);
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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InnerBackInputApply.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "03";
                curPriveType.Name = "内部退库申请";
                curPriveType.Memo = "18";
            }

            this.costDecimals = FS.SOC.HISFC.Components.Pharmacy.Function.GetCostDecimals("0310", curPriveType.Memo);

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
            else if (text == "申请单")
            {
                return this.ChooseApplyBill();
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
                this.hsInnerInputApply.Clear();
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

            hsInnerInputApply.Clear();
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
            if (this.curStockDept == null)
            {
                return null;
            }
            return FS.SOC.HISFC.Components.Pharmacy.Function.QueryManagerStockDept();
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
            if (keyData == Keys.Enter)
            {
                this.SetTotInfo();
            }
            else if (keyData == Keys.F12)
            {
                //失去焦点，保证按键事件脱离Farpoint
                this.curIDataDetail.FpSpread.Parent.Select();
                this.curIDataDetail.FpSpread.Parent.Focus();

                this.ConvertUnit();

                //将焦点返回
                this.curIDataDetail.FpSpread.Select();
                this.curIDataDetail.FpSpread.Focus();
            }
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

            for (int index = 0; index < toolStrip.Items.Count; index++)
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


            ToolStripButton tb2 = new ToolStripButton("申请单");
            tb2.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S申请单);
            tb2.ToolTipText = "显示申请单列表";
            tb2.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            tb2.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStrip.Items.Insert(0, tb2);


            return 1;
        }


        /// <summary>
        /// 初始化数据选择关键属性
        /// </summary>
        /// <returns></returns>
        protected int InitChooseData()
        {
            string errInfo = "";
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = FS.SOC.HISFC.Components.Pharmacy.Function.GetBizChooseDataSetting("0310", curPriveType.Memo, curPriveType.ID, "0", ref errInfo);
            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.InnerBackInputApplyPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.InnerBackInputApplyPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
                chooseDataSetting.ListTile = "药品列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0 ,9,4};
                chooseDataSetting.Filter =
                    "trade_name like '%{0}%'"
                + " or custom_code like '%{0}%'"
                + " or spell_code like '%{0}%'"
                + " or regular_spell like '%{0}%'"
                + " or regular_wb like '%{0}%'";

                chooseDataSetting.ColumnLabels = new string[] { 
                    "药品编码", 
                    "自定义码",
                    "名称", 
                    "规格", 
                    "批号",
                    "数量",
                    "购入价", 
                    "零售价", 
                    "单位", 
                    "批次",
                    "拼音码", 
                    "五笔码", 
                    "通用名", 
                    "通用名拼音码", 
                    "通用名五笔码",
                    "通用名自定义码"
                };
                chooseDataSetting.ColumnWiths = new float[]
                {  0f,// "药品编码", 
                   60f,// "自定义码",
                   120f,// "名称", 
                   100f,// "规格", 
                   60f,// "批号",
                   60f,// "数量",  
                   40f,// "购入价", 
                   40f,// "零售价", 
                   15f,// "单位", 
                   60f,//  "批次",
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
                   t,// "批号",
                   t,// "数量",  
                   n,// "购入价", 
                   n,// "零售价", 
                   t,// "单位", 
                   t,//  "批次",
                   t,// "拼音码", 
                   t,// "五笔码", 
                   t,// "通用名", 
                   t,// "通用名拼音码", 
                   t,// "通用名五笔码"
                   t// "通用名自定义码"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InnerBackInputApplyL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curIDataChooseList.SettingFileName = chooseDataSetting.SettingFileName;

                string fromDeptNO = "";
                if (this.curFromDept != null)
                {
                    fromDeptNO = this.curFromDept.ID;
                }
                this.curIDataChooseList.ShowChooseList(chooseDataSetting.ListTile, string.Format(chooseDataSetting.SQL, this.curStockDept.ID, "{0}",curStockDept.ID,"{1}"), chooseDataSetting.IsNeedDrugType, chooseDataSetting.Filter);
                this.curIDataChooseList.SetFormat(chooseDataSetting.CellTypes, chooseDataSetting.ColumnLabels, chooseDataSetting.ColumnWiths);
                this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
                this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
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
                Function.ShowMessage("程序发生错误：入库明细数据显示控件没有初始化", System.Windows.Forms.MessageBoxIcon.Error);
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "名称", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 90f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批号", 90f);

                this.curIDataDetail.FpSpread.SetColumnWith(0, "申请量", 60f);
     
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "加成价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "加成金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "备注", 60f);
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
                nQty.ReadOnly = false;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批号", t);

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "申请量", nQty);

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "加成价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "加成金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售价", nPrice);
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
                    new DataColumn("批号",typeof(string)),
                    new DataColumn("申请量",typeof(decimal)),
                    new DataColumn("本科库存",typeof(string)),
                    new DataColumn("单位",typeof(string)),
                    new DataColumn("购入价",typeof(decimal)),
                    new DataColumn("购入金额",typeof(decimal)),
                    new DataColumn("加成价",typeof(decimal)),
                    new DataColumn("加成金额",typeof(decimal)),
                    new DataColumn("零售价",typeof(decimal)),
                    new DataColumn("零售金额",typeof(decimal)),
                    new DataColumn("备注",typeof(string)),
                    new DataColumn("拼音码",typeof(string)),
                    new DataColumn("五笔码",typeof(string)),
                    new DataColumn("主键",typeof(string)),

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "申请量")
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }


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
        protected int AddApplyObjectToDataTable(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            if (applyOut == null)
            {
                Function.ShowMessage("向DataTable中添加入库申请信息失败：入库信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtDetail == null)
            {
                Function.ShowMessage("向DataTable中添加入库申请信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            if (this.hsInnerInputApply.Contains(applyOut.Item.ID + applyOut.GroupNO.ToString()))
            {
                Function.ShowMessage("" + applyOut.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsInnerInputApply.Add(applyOut.Item.ID + applyOut.GroupNO.ToString(), applyOut);
            }

            decimal purchaseCost = applyOut.Item.PriceCollection.PurchasePrice * (applyOut.Operation.ApplyQty / applyOut.Item.PackQty);
            decimal wholeSaleCost = applyOut.Item.PriceCollection.WholeSalePrice * (applyOut.Operation.ApplyQty / applyOut.Item.PackQty);
            decimal retailCost = applyOut.Item.PriceCollection.RetailPrice * (applyOut.Operation.ApplyQty / applyOut.Item.PackQty);

            this.totPurchaseCost += purchaseCost;
            this.totWholeSaleCost += wholeSaleCost;
            this.totRetailCost += retailCost;
            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 加成总金额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }

            DataRow row = this.dtDetail.NewRow();

            row["自定义码"] = applyOut.Item.UserCode;
            row["药品编码"] = applyOut.Item.ID;
            row["名称"] = applyOut.Item.Name;
            row["规格"] = applyOut.Item.Specs;
            row["批号"] = applyOut.BatchNO;

            decimal consumeQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.ExtFlag);
            decimal fitQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.ExtFlag1);

            decimal storageQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.StockDept.User01);

            if (applyOut.ShowState == "1")
            {
                row["申请量"] = applyOut.Operation.ApplyQty / applyOut.Item.PackQty;            
                row["单位"] = applyOut.Item.PackUnit;
                row["本科库存"] = (storageQty / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
            }
            else
            {
                row["申请量"] = applyOut.Operation.ApplyQty;              
                row["单位"] = applyOut.Item.MinUnit;
                row["本科库存"] = storageQty.ToString("F4").TrimEnd('0').TrimEnd('.');
            }
            row["购入价"] = applyOut.Item.PriceCollection.PurchasePrice;
            row["购入金额"] = FS.FrameWork.Function.NConvert.ToDecimal(purchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.'));
            row["加成价"] = applyOut.Item.PriceCollection.WholeSalePrice;
            row["加成金额"] = FS.FrameWork.Function.NConvert.ToDecimal(wholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.'));
            row["零售价"] = applyOut.Item.PriceCollection.RetailPrice;
            row["零售金额"] = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.'));

            row["备注"] = applyOut.Memo;

            row["拼音码"] = applyOut.Item.SpellCode;
            row["五笔码"] = applyOut.Item.WBCode;
            row["主键"] = applyOut.Item.ID + applyOut.GroupNO.ToString();
;

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
            if (this.curIDataDetail == null || this.curIDataDetail.FpSpread == null || this.curIDataDetail.FpSpread.Sheets.Count == 0)
            {
                return 0;
            }
            int rowIndex = this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex;
            string keys = this.curIDataDetail.FpSpread.Sheets[0].Cells[rowIndex, this.curIDataDetail.FpSpread.GetColumnIndex(0, "主键")].Text;
            string tradeName = this.curIDataDetail.FpSpread.Sheets[0].Cells[rowIndex, this.curIDataDetail.FpSpread.GetColumnIndex(0, "名称")].Text;

            DialogResult dr = MessageBox.Show("确定删除第" + (rowIndex + 1).ToString() + "行的 " + tradeName + " 吗？", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }
            if (hsInnerInputApply.Contains(keys))
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { keys });
                if (row != null)
                {
                    this.dtDetail.Rows.Remove(row);
                }

                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = hsInnerInputApply[keys] as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (!string.IsNullOrEmpty(applyOut.ID))
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    int param = this.itemMgr.DeleteApplyOut(applyOut.ID);
                    if (param == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("数据可能已经发送或者核准，请退出界面刷新重试！", MessageBoxIcon.Information);
                    }
                    else if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("删除申请单发送错误！请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
                hsInnerInputApply.Remove(keys);

            }
            this.SetTotInfo();

            return 1;
        }

        #endregion

        #region 保存

        /// <summary>
        /// 有效性判断
        /// </summary>
        /// <returns>填写有效 返回True 否则返回 False</returns>
        public int CheckValid()
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

                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsInnerInputApply[dr["主键"]] as FS.HISFC.Models.Pharmacy.ApplyOut;

                if (applyOut.State == "1")
                {
                    Function.ShowMessage("已经发送的数据不可以更改", MessageBoxIcon.Information);
                    return -1;
                }
                if (!string.IsNullOrEmpty(applyOut.ID))
                {
                    if (applyOut.StockDept.ID != this.curFromDept.ID)
                    {
                        Function.ShowMessage(dr["名称"].ToString() + "  来源科室是" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.StockDept.ID) + "，和您选择的不同", MessageBoxIcon.Information);
                        return -1;
                    }
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(dr["申请量"]) <= 0)
                {
                    Function.ShowMessage(dr["名称"].ToString() + "  请输入申请量 申请量不能小于等于0", MessageBoxIcon.Information);
                    return -1;
                }

            }

            return 1;
        }

        protected int Save()
        {
            if (this.dtDetail.Rows.Count == 0)
            {
                return 0;
            }

            if (this.curStockDept.ID == this.curFromDept.ID)
            {
                Function.ShowMessage("内部退库申请的来源科室和库存科室不能相同,请核对", MessageBoxIcon.Error);
                return 0;
            }

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

            string errString = string.Empty;
            param = this.CheckConvertUnit(ref errString);
            if (param < 0)
            {
                Function.ShowMessage(errString, MessageBoxIcon.Information);
                return param;
            }

            for (int i = 0; i < this.dtDetail.DefaultView.Count; i++)
            {
                this.dtDetail.DefaultView[i].EndEdit();
            }

            //定义事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();
            ArrayList alInnerInputApply = new ArrayList();

            string billNO = "";
            string errInfo = "";

            foreach (DataRow dr in this.dtDetail.Rows)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsInnerInputApply[dr["主键"]] as FS.HISFC.Models.Pharmacy.ApplyOut;

                applyOut.Days = 1;
                applyOut.Operation.Oper.ID = this.itemMgr.Operator.ID;
                applyOut.Operation.ApplyOper.OperTime = sysTime;

                applyOut.Memo = dr["备注"].ToString();

                if (applyOut.ShowState == "1")
                {
                    applyOut.Operation.ApplyQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["申请量"]) * applyOut.Item.PackQty;
                }
                else
                {
                    applyOut.Operation.ApplyQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["申请量"]);
                }

                if (applyOut.ID == "")
                {
                    #region 新产生数据

                    //设置单号
                    if (billNO == "")
                    {
                        billNO = FS.SOC.HISFC.Components.Pharmacy.Function.GetBillNO(this.curStockDept.ID, "0310", this.curPriveType.ID, ref errInfo);
                        if (string.IsNullOrEmpty(billNO) || billNO == "-1")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("获取最新入库申请单号出错:" + errInfo, MessageBoxIcon.Error);
                            return -1;
                        }
                    }

                    applyOut.BillNO = billNO;              //申请单据号

                    if (this.itemMgr.InsertApplyOut(applyOut) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("插入入库申请失败：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }

                    #endregion
                }
                else
                {
                    billNO = applyOut.BillNO;

                    if (applyOut.State == "1" || applyOut.State == "2")
                    {
                        continue;
                    }

                    int parm = this.itemMgr.UpdateApplyOutNum(applyOut.ID, applyOut.Operation.ApplyQty);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("更新入库申请失败：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("申请单可能已经发送或者核准，请退出界面以便刷新数据", MessageBoxIcon.Error);
                        return 0;
                    }

                }

                alInnerInputApply.Add(applyOut);
            }



            FS.FrameWork.Management.PublicTrans.Commit();

            ArrayList allInput = new ArrayList();

            DialogResult dialogResult = MessageBox.Show("申请单需要发送才能生效，是否发送到" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.curFromDept.ID) + "？\n注意：发送后不允许修改", "提示>>", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int parm = this.itemMgr.ExamApplyOut(this.curStockDept.ID, billNO);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("更新入库退库申请失败：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("申请单可能已经发送或者核准，请退出界面以便刷新数据", MessageBoxIcon.Error);
                    return 0;
                }

                #region 本科室的入库退库记录 
                string inputBillNo = string.Empty;
                if (string.IsNullOrEmpty(inputBillNo))
                {
                    inputBillNo = SOC.HISFC.Components.Pharmacy.Function.GetBillNO(this.curStockDept.ID, "0310", "01", ref errInfo);
                    if (string.IsNullOrEmpty(inputBillNo) || inputBillNo == "-1")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("获取最新入库单号出错:" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alInnerInputApply)
                {
                    string drugNO = applyInfo.Item.ID;

                    string groupNO = applyInfo.GroupNO.ToString();

                    ArrayList alDetail = this.itemMgr.QueryStorageList(this.curStockDept.ID, drugNO,FS.FrameWork.Function.NConvert.ToDecimal(groupNO));

                    if (alDetail == null || alDetail.Count == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("未获取有效的库存明细信息" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }

                    FS.HISFC.Models.Pharmacy.Storage storage = alDetail[0] as FS.HISFC.Models.Pharmacy.Storage;

                    FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();

                    input.StockDept = storage.StockDept;                //库存科室
                    //input.Company.ID = this.curFromDept.ID;
                    input.Item = storage.Item;
                    input.GroupNO = storage.GroupNO;
                    input.Quantity = storage.StoreQty;                  //入库量 = 库存量
                    input.BatchNO = storage.BatchNO;
                    input.ValidTime = storage.ValidTime;
                    input.PlaceNO = storage.PlaceNO;
                    input.InvoiceNO = storage.InvoiceNO;
                    input.Producer = storage.Producer;
                    input.OutListNO = applyInfo.BillNO;
                    if (this.curStockDept.Memo == "PI")
                    {
                        input.ShowState = "1";
                        input.ShowUnit = input.Item.PackUnit;
                    }
                    else
                    {
                        input.ShowState = "0";
                        input.ShowUnit = input.Item.MinUnit;
                    }
                    input.Memo = storage.Memo;

                    FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(input.Item.ID);
                    if (item == null)
                    {
                        Function.ShowMessage("请与系统管理员联系,获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    input.Item.UserCode = item.UserCode;
                    input.Item.SpellCode = item.SpellCode;
                    input.Item.WBCode = item.WBCode;
                    input.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                    input.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;
                    decimal backQty = applyInfo.Operation.ApplyQty;

                    if (input.ShowState == "1")
                    {
                        backQty = backQty * input.Item.PackQty;
                    }

                    decimal storeQty = 0;
                    this.itemMgr.GetStorageNum(this.curStockDept.ID, input.Item.ID, input.GroupNO, out storeQty);
                    if (storeQty < backQty)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage(input.Item.Name + " 库存数量不足 退库数量过大", MessageBoxIcon.Information);
                        return -1;
                    }

                    input.InListNO = inputBillNo;                                      //退库单号
                    input.Quantity = -backQty;                                     //入库数量(负数)
                    input.Operation.ApplyQty = -backQty;
                    input.Operation.ExamQty = -backQty;


                    decimal retailCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;
                    retailCost = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()));
                    input.RetailCost = retailCost;

                    input.InDate = sysTime;
                    input.Operation.ApplyOper.ID = this.itemMgr.Operator.ID;
                    input.Operation.ApplyOper.OperTime = sysTime;
                    input.Operation.Oper = input.Operation.ApplyOper;
                    input.Operation.ExamOper.ID = this.itemMgr.Operator.ID;
                    input.Operation.ExamOper.OperTime = sysTime;
                    input.Operation.ApproveOper.ID = this.itemMgr.Operator.ID;
                    input.Operation.ApproveOper.OperTime = sysTime;

                    input.State = "2";

                    input.SpecialFlag = "0";

                    //供货单位 
                    input.Company = this.curFromDept;

                    //目标单位 = 供货单位      
                    input.TargetDept = this.curFromDept;
                    input.SourceCompanyType = "1";

                    input.PrivType = "01";            //入库类型
                    input.SystemType = "18";         //系统类型

                    //购入金额
                    input.PurchaseCost = input.Item.PriceCollection.PurchasePrice * (input.Quantity / input.Item.PackQty);
                    input.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(input.PurchaseCost.ToString("F" + this.costDecimals.ToString()));

                    //零售金额
                    input.RetailCost = input.Item.PriceCollection.RetailPrice * (input.Quantity / input.Item.PackQty);
                    input.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(input.RetailCost.ToString("F" + this.costDecimals.ToString()));

                    //批发金额
                    input.WholeSaleCost = input.Item.PriceCollection.WholeSalePrice * (input.Quantity / input.Item.PackQty);
                    input.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(input.WholeSaleCost.ToString("F" + this.costDecimals.ToString()));
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

                    input.Operation.ReturnQty = 0;
                    input.Memo = "内部入库退库申请";
                    parm = this.itemMgr.Input(input, "1", "1");
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存 " + input.Item.Name + " 发生错误: " + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    allInput.Add(input);
                }
                #endregion

                FS.FrameWork.Management.PublicTrans.Commit();
            }

            if (FS.SOC.HISFC.Components.Pharmacy.Function.DealExtendBiz("0310", this.PriveType.ID, alInnerInputApply, ref errInfo) == -1)
            {
                Function.ShowMessage("入库申请已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            FS.SOC.HISFC.Components.Pharmacy.Function.PrintBill("0310", "01", allInput);


            this.ClearData();

            return 1;
        }

        /// <summary>
        /// 不允许以最小单位发送到药库
        /// </summary>
        /// <returns></returns>
        private int CheckConvertUnit(ref string errInfo)
        {
            //如果是退回给药库不允许以最小单位退库
            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.curFromDept.ID).DeptType.ID.ToString() == "PI")
            {
                foreach (DictionaryEntry de in hsInnerInputApply)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyInfo = de.Value as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (applyInfo.ShowState == "0")
                    {
                        errInfo = applyInfo.Item.Name + "不能以最小单位发送到" +SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.curFromDept.ID) + ",请确认！";
                        return -1;
                    }
                }
            }
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

            hsInnerInputApply.Clear();
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);


            return 1;
        }

        #endregion



        #region 申请单按钮调用
        /// <summary>
        /// 选择入库单
        /// </summary>
        public int ChooseApplyBill()
        {
            if (this.curIDataDetail.FpSpread.Sheets[0].RowCount > 0)
            {
                DialogResult dr = MessageBox.Show("选择申请单将清空现有数据，是否继续！", "提示>>", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return 0;
                }
                this.ClearData();
            }

            string errInfo = "";

            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = FS.SOC.HISFC.Components.Pharmacy.Function.GetBizChooseDataSetting("0310", curPriveType.Memo, curPriveType.ID, "1", ref errInfo);

            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();


                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.InnerBackInputApply.ChooseBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.InnerBackInputApply.ChooseBill,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                string companyNO = "All";
                if (this.curFromDept != null && this.curFromDept.ID != "")
                {
                    companyNO = this.curFromDept.ID;
                }
                //替换库存科室和供货公司，时间和状态不替换
                SQL = string.Format(SQL, this.curStockDept.ID, companyNO, "{0}", "{1}", "{2}");

                chooseDataSetting.ListTile = "申请单列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0, 1, 3 };
                curBillChooseColumnIndexs = chooseDataSetting.ColumnIndexs;
                chooseDataSetting.Filter = "";

                chooseDataSetting.ColumnLabels = new string[] 
                { 
                    "申请单号", 
                    "来源科室", 
                    "科室名称", 
                    "状态", 
                    "申请时间", 
                    "申请人工号"
                };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   100f,// "申请单号", 
                   60f,// "来源科室", 
                   100f,// "科室名称", 
                   40f,// "状态", 
                   120f,// "申请时间", 
                   90f,// "申请人工号"
                };
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "申请单号", 
                   t,// "来源科室", 
                   t,// "科室名称", 
                   t,// "状态", 
                   t,// "申请时间",
                   t,// "申请人工号"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InnerInputApplyL1Setting.xml";
            }

            ArrayList alState = new ArrayList();

            FS.FrameWork.Models.NeuObject state0 = new FS.FrameWork.Models.NeuObject();
            state0.ID = "0";
            state0.Name = "未发送";
            FS.FrameWork.Models.NeuObject state1 = new FS.FrameWork.Models.NeuObject();
            state1.ID = "1";
            state1.Name = "已发送";

            alState.Add(state0);
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

        #region 刷新数据选择列表
        protected int FreshDataChooseList()
        {
            string fromDeptNO = "";
            if (this.curFromDept != null)
            {
                fromDeptNO = this.curFromDept.ID;
            }

            //string fromDeptNO = "";
            //if (this.curStockDept != null)
            //{
            //    fromDeptNO = this.curStockDept.ID;
            //}

            if (this.curChooseDataSetting == null)
            {
                return 0;
            }

            string SQL = string.Format(this.curChooseDataSetting.SQL, curStockDept.ID, "{0}", curStockDept.ID, "{1}");


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


            this.curIDataDetail.FpSpread.Sheets[0].Columns[this.curIDataDetail.FpSpread.GetColumnIndex(0, "购入金额")].Locked = false;

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "购入金额" || dc.ColumnName == "零售金额"||dc.ColumnName == "加成金额")
                {
                    dc.ReadOnly = false;
                }
            }

            for (int rowIndex = 0; rowIndex < this.curIDataDetail.FpSpread.Sheets[0].Rows.Count; rowIndex++)
            {
                string keys = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "主键");

                if (this.hsInnerInputApply.Contains(keys))
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsInnerInputApply[keys] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    string strQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "申请量");
                    decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(strQty);
                    if (applyOut.ShowState == "1")
                    {
                        qty = applyOut.Item.PackQty * qty;
                    }
                    decimal purchaseCost = applyOut.Item.PriceCollection.PurchasePrice * (qty / applyOut.Item.PackQty);
                    purchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(purchaseCost.ToString("F" + this.costDecimals.ToString()));
                    decimal wholeSaleCost = applyOut.Item.PriceCollection.WholeSalePrice * (qty / applyOut.Item.PackQty);
                    wholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(wholeSaleCost.ToString("F" + this.costDecimals.ToString()));
                    decimal retailCost = applyOut.Item.PriceCollection.RetailPrice * (qty / applyOut.Item.PackQty);
                    retailCost = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()));



                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "购入金额", purchaseCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "加成金额", purchaseCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "零售金额", retailCost);

                    this.totRowQty++;
                    this.totPurchaseCost += purchaseCost;
                    this.totWholeSaleCost += wholeSaleCost;
                    this.totRetailCost += retailCost;

                    this.curIDataDetail.Info = "当前品种数：" + this.totRowQty.ToString("F0")
                       + ", 购入总额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 加成总额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 零售总额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');


                }
            }

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "购入金额" || dc.ColumnName == "零售金额"||dc.ColumnName == "加成金额")
                {
                    dc.ReadOnly = true;
                }
            }
            return 1;
        }
        #endregion

        #region 单位手工转换
        private void ConvertUnit()
        {
            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return;
            }
            int rowIndex = this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex;

            string keys = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "主键");

            if (this.hsInnerInputApply.Contains(keys))
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { keys });
                if (row != null)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = hsInnerInputApply[keys] as FS.HISFC.Models.Pharmacy.ApplyOut;

                    this.dtDetail.Columns["单位"].ReadOnly = false;
                    this.dtDetail.Columns["本科库存"].ReadOnly = false;
                    this.dtDetail.Columns["申请量"].ReadOnly = false;
                    decimal storageQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.StockDept.User01);

                    if (applyOut.ShowState == "1")
                    {
                        applyOut.ShowUnit = applyOut.Item.MinUnit;
                        applyOut.ShowState = "0";


                        row["本科库存"] = storageQty.ToString("F4".TrimEnd('0').TrimEnd('.'));
                        row["单位"] = applyOut.Item.MinUnit;
                        row["申请量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["申请量"]) * applyOut.Item.PackQty;
                    }
                    else
                    {
                        applyOut.ShowUnit = applyOut.Item.PackUnit;
                        applyOut.ShowState = "1";
                        row["本科库存"] = (storageQty / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                        row["单位"] = applyOut.Item.PackUnit;
                        row["申请量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["申请量"]) / applyOut.Item.PackQty;
                    }

                    this.dtDetail.Columns["本科库存"].ReadOnly = true;
                    this.dtDetail.Columns["单位"].ReadOnly = true;
                }
            }
        }
        #endregion
        #endregion

        #region 事件


        void IDataChooseList_ChooseCompletedEvent()
        {
            if (this.curFromDept == null || string.IsNullOrEmpty(this.curFromDept.ID))
            {
                Function.ShowMessage("请选择目标科室!", MessageBoxIcon.Information);
                return;
            }
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsInnerInputApply[dr["主键"]] as FS.HISFC.Models.Pharmacy.ApplyOut;

                if (applyOut.State == "1")
                {
                    Function.ShowMessage("已经发送的数据不可以更改", MessageBoxIcon.Information);
                    return;
                }
            }
            string[] values = this.curIDataChooseList.GetChooseData(this.curChooseDataSetting.ColumnIndexs);
            if (values == null || values.Length == 0)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                if (values.Length < 2)
                {
                    Function.ShowMessage("药品选择列表返回的选择数据不符合程序要求，请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }
                string drugNO = values[0];
                string groupNO = values[1];
                string batchNO = values[2];

                if (this.hsInnerInputApply.Contains(drugNO + groupNO))
                {
                    Function.ShowMessage("该药品已添加!", MessageBoxIcon.Information);
                    return;
                }

                ArrayList alDetail = this.itemMgr.QueryStorageList(this.curStockDept.ID, drugNO, FS.FrameWork.Function.NConvert.ToDecimal(groupNO));

                if (alDetail == null || alDetail.Count == 0)
                {
                    Function.ShowMessage("未获取有效的库存明细信息" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return;
                }

                FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(values[0]);


                if (item == null)
                {
                    Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.Storage storage = alDetail[0] as FS.HISFC.Models.Pharmacy.Storage;

                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();

                    applyOut.Item = item;
                    applyOut.Class2Type = "0310";
                    applyOut.PrivType = this.curPriveType.ID;
                    applyOut.SystemType = this.curPriveType.Memo;

                    applyOut.ApplyDept.ID = this.curStockDept.ID;
                    applyOut.StockDept.ID = this.curFromDept.ID;      //库存科室 (目标科室)

                    applyOut.BatchNO = batchNO;
                    applyOut.GroupNO = FS.FrameWork.Function.NConvert.ToDecimal(groupNO);

                    applyOut.State = "0";                                   //状态 申请
                    applyOut.ShowState = "1";
                    applyOut.ShowUnit = applyOut.Item.PackUnit;

                    decimal storageNum = 0;
                    if (this.itemMgr.GetStorageNum(this.curStockDept.ID, applyOut.Item.ID,FS.FrameWork.Function.NConvert.ToDecimal(groupNO), out storageNum) == -1)
                    {
                        Function.ShowMessage("获取库存数量时出错，请与系统管理员联系！\n报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }

                    applyOut.StockDept.User01 = storageNum.ToString();

                    if (this.AddApplyObjectToDataTable(applyOut) != -1)
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

        void frmBillChooseList_ChooseCompletedEvent()
        {
            string[] values = this.frmBillChooseList.GetChooseData(this.curBillChooseColumnIndexs);
            if (values == null || values.Length < 3)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                if (this.curFromDept == null)
                {
                    this.curFromDept = new FS.FrameWork.Models.NeuObject();
                }
                if (string.IsNullOrEmpty(this.curFromDept.ID))
                {
                    this.curFromDept.ID = values[1];
                    this.FreshDataChooseList();
                }

                ArrayList alApplyout = this.itemMgr.QueryApplyOutInfoByListCode(this.curStockDept.ID, values[0], values[2]);
                if (alApplyout == null)
                {
                    Function.ShowMessage("查询内部入库申请数据发生错误：" + itemMgr.Err + "\n请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyout)
                {
                    if (alApplyout.Count < 50 && SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper == null)
                    {
                        FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(applyOut.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        applyOut.Item.UserCode = item.UserCode;
                        applyOut.Item.WBCode = item.WBCode;
                        applyOut.Item.SpellCode = item.SpellCode;
                        applyOut.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                        applyOut.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        applyOut.Item.UserCode = item.UserCode;
                        applyOut.Item.WBCode = item.WBCode;
                        applyOut.Item.SpellCode = item.SpellCode;
                        applyOut.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                        applyOut.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                    }
                    if (this.AddApplyObjectToDataTable(applyOut) == -1)
                    {
                        break;
                    }
                }

            }
        }

        void frmSetPlan_SetCompletedHander(frmSetPlan.CreatePlanType type, string formula, params string[] param)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Plan planMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Plan();
            if (type == frmSetPlan.CreatePlanType.Consume)
            {
                //GetPlan(string deptNO, DateTime consumeBeginTime, DateTime consumeEndTime, int lowDays, int upDays, string drugType, string stencilNO)
                alFormulaPlan = planMgr.GetPlan(param[0],
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
                alFormulaPlan = planMgr.GetPlan(param[0], param[1], param[2]);
            }
        }

        #endregion

    }
}