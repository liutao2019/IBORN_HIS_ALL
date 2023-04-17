using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Fin
{
    /// <summary>
    /// [功能描述: 财务核准入库]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-6<br></br>
    /// </summary>
    public class ApproveInput : Base.IBaseBiz
    {  
        private FS.FrameWork.Models.NeuObject curStockDept = null;
        private FS.FrameWork.Models.NeuObject curFromDept = null;
        private FS.FrameWork.Models.NeuObject curPriveType = null;

        private System.Data.DataTable dtDetail = null;

        private string settingFileName = "";
        private uint costDecimals = 2;

        private decimal totPurchaseCost = 0;
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
        SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();


        ucFinBill ucFinBill = new ucFinBill();
        
        Base.frmBillChooseList frmBillChooseList = null;
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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPPhaFinApproveInputSetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "30";
                curPriveType.Name = "财务入库审核";
                curPriveType.Memo = "1F";
            }

            this.costDecimals = Function.GetCostDecimals("0310", curPriveType.Memo);

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
            this.curIDataDetail.FpSpread.EditModePermanent = true;
            this.curIDataDetail.FpSpread.EditMode = true;
            this.curIDataDetail.FpSpread.EditModeReplace = true;
            if (param == -1)
            {
                return param;
            }

            this.FreshDataChooseList();


            param = this.InitToolStrip(toolStrip);
            if (param == -1)
            {
                return param;
            }

            if (ucFinBill == null)
            {
                ucFinBill = new ucFinBill();
            }

            param = ucFinBill.InitFinBillNO(this.curStockDept.ID, "0310", "30");

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
                Function.ShowMessage("供货公司为null，请与系统管理员联系！", MessageBoxIcon.Error);
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
            if (text == "保存")
            {
                return this.Save();
            }
            else if (text == "入库单")
            {
                return this.ChooseInputBill();
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
            if (ucFinBill != null)
            {
                ucFinBill.Dispose();
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
            get
            {
                if (ucFinBill == null)
                {
                    ucFinBill = new ucFinBill();
                }
                return ucFinBill;
            }
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
            fromDeptTypeName = "供货公司：";
            SOC.HISFC.BizProcess.Cache.Pharmacy.InitCompany();
            return SOC.HISFC.BizProcess.Cache.Pharmacy.companyHelper.ArrayObject;
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
                else if (toolStrip.Items[index].Text == "")
                {
                    continue;
                }
                toolStrip.Items.RemoveAt(index);
            }

            ToolStripButton tb = new ToolStripButton("入库单");
            tb.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R入库单);
            tb.ToolTipText = "显示入库单列表";
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
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = Function.GetBizChooseDataSetting("0310", curPriveType.Memo, curPriveType.ID, "0", ref errInfo);
            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Fin.ApproveInput.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Fin.ApproveInput.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
                chooseDataSetting.ListTile = "单据列表";             
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0 };
                chooseDataSetting.Filter = "in_list_code like '%{0}%'"
                     + " or invoice_no like '%{0}%'";

                chooseDataSetting.ColumnLabels = new string[] { "入库单号", "日期", "公司名称","发票号" };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   100f,// "入库单号", 
                   120f,// "日期", 
                   120f,// "公司名称", 
                    120f// "发票号", 
                };
                chooseDataSetting.IsNeedDrugType = false;

                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "入库流水号", 
                   t,// "日期", 
                   t,// "公司名称", 
                   t// "公司名称", 
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPPhaFinApproveInputL0Setting.xml";
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "名称", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 90f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "数量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "发票号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "入库单号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "公司名称", 120f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "发票号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "有效期", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "货位号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批准文号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "发票分类", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "送货单号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "发票日期", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "生产厂家", 60f);
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
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "发票号", tWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "入库单号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "公司名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "有效期", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "货位号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批准文号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "发票分类", tWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "送货单号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "发票日期", tWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "生产厂家", t);
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
                    new DataColumn("批号",typeof(string)),
                    new DataColumn("名称",typeof(string)),
                    new DataColumn("规格",typeof(string)),
                    new DataColumn("数量",typeof(decimal)),
                    new DataColumn("单位",typeof(string)),
                    new DataColumn("购入价",typeof(decimal)),
                    new DataColumn("购入金额",typeof(decimal)),
                    new DataColumn("零售价",typeof(decimal)),
                    new DataColumn("零售金额",typeof(decimal)),
                    new DataColumn("发票号",typeof(string)),
                    new DataColumn("发票分类",typeof(string)),
                    new DataColumn("发票日期",typeof(string)),
                    new DataColumn("入库单号",typeof(string)),
                    new DataColumn("公司名称",typeof(string)),
                    new DataColumn("有效期",typeof(DateTime)),
                    new DataColumn("货位号",typeof(string)),
                    new DataColumn("批准文号",typeof(string)),
                    new DataColumn("送货单号",typeof(string)),
                    new DataColumn("生产厂家",typeof(string)),
                    new DataColumn("备注",typeof(string)),
                    new DataColumn("拼音码",typeof(string)),
                    new DataColumn("五笔码",typeof(string)),
                    new DataColumn("主键",typeof(string)),

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "发票号" || dc.ColumnName == "发票分类" || dc.ColumnName == "发票日期")
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
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

            if (this.hsInput.Contains(input.InListNO + input.Item.ID + input.GroupNO.ToString()))
            {
                Function.ShowMessage("单号" + input.InListNO + "中的" + input.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsInput.Add(input.InListNO + input.Item.ID + input.GroupNO.ToString(), input);
            }

            this.totPurchaseCost += input.PurchaseCost;
            this.totRetailCost += input.RetailCost;
            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "单号：" + input.InListNO
                    + ", 品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 零售总额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }


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
            row["零售价"] = input.Item.PriceCollection.RetailPrice;
            row["零售金额"] = input.RetailCost;
            row["发票号"] = input.InvoiceNO;
            row["入库单号"] = input.InListNO;
            row["公司名称"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(input.Company.ID);
            row["有效期"] = input.ValidTime;

            row["货位号"] = input.PlaceNO;
            row["批准文号"] = input.Item.Product.ApprovalInfo;
            row["发票分类"] = input.InvoiceType;
            row["送货单号"] = input.DeliveryNO;
            if (input.InvoiceDate > DateTime.Now.AddYears(-100))
            {
                row["发票日期"] = input.InvoiceDate;
            }
            row["生产厂家"] = input.Producer.Name;

            row["拼音码"] = input.Item.SpellCode;
            row["五笔码"] = input.Item.WBCode;
            row["主键"] = input.InListNO + input.Item.ID + input.GroupNO.ToString();

            this.dtDetail.Rows.Add(row);

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
            
            if (this.dtDetail.Rows.Count == 0)
            {
                Function.ShowMessage("请选择要核准的药品！", MessageBoxIcon.Information);
                return -4;
            }

            //bool isContinue = false;

            foreach (DataRow dr in this.dtDetail.Rows)
            {
                if (string.IsNullOrEmpty(dr["发票号"].ToString()) && !ucFinBill.InvoceNONullabe)
                {
                    Function.ShowMessage(dr["名称"].ToString() + "  没有录入发票号", MessageBoxIcon.Information);
                    return -1;
                }
               
            }

            return 1;
        }

        protected int Save()
        {
            this.dtDetail.DefaultView.RowFilter = "1=1";
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            int param = this.CheckValid();
            if (param<0)
            {
                if (param == -2)
                {
                    this.curIDataDetail.SetFocusToFilter();
                }
                return param;
            }

            for (int i = 0; i < this.dtDetail.DefaultView.Count; i++)
            {
                this.dtDetail.DefaultView[i].EndEdit();
            }


            this.curIDataDetail.FpSpread.StopCellEditing();

            ArrayList alInput = new ArrayList();
            string errInfo = "";

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行保存操作..请稍候");
            System.Windows.Forms.Application.DoEvents();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //当天操作日期
            DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();
            
            //记录单据数据
            System.Collections.Hashtable hsBill = new System.Collections.Hashtable();
            string billNO = "";

            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();
            FS.SOC.HISFC.BizLogic.Pharmacy.Financial financialMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Financial();
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                string key = dr["主键"].ToString();

                input = this.hsInput[key] as FS.HISFC.Models.Pharmacy.Input;

                
                //发票号为空或者已经核准的数据不更改
                if ((string.IsNullOrEmpty(dr["发票号"].ToString().Trim()) && !ucFinBill.InvoceNONullabe) || input.State == "2")
                {
                    continue;
                }

                input.Operation.Oper.ID = this.itemMgr.Operator.ID;
                input.Operation.Oper.OperTime = sysTime;

                //审批作为发票录入或修改人
                if (input.State == "0")
                {
                    input.Operation.ExamOper.ID = this.itemMgr.Operator.ID;                //审批人
                    input.Operation.ExamOper.OperTime = sysTime;                                //审批时间
                    input.Operation.ExamQty = input.Quantity;
                }
                input.Operation.ApproveOper.ID = this.itemMgr.Operator.ID;
                input.Operation.ApproveOper.OperTime = sysTime;
               
                DateTime dt;
                if(DateTime.TryParse(dr["发票日期"].ToString().Trim(),out dt))
                {
                    input.InvoiceDate = dt;
                }
                input.InvoiceNO = dr["发票号"].ToString().Trim();
                input.InvoiceType = dr["发票分类"].ToString().Trim();

                //不要对单价、金额更改，财务账务严格要求，此处只允许更改发票号
                int parm = financialMgr.FinApproveInput(input);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败：" + financialMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("数据可能已被审核，请刷新重试", MessageBoxIcon.Error);
                    return -1;
                }


                this.itemMgr.UpdateRecord(input);

                alInput.Add(input);

                if (!hsBill.Contains(input.InListNO))
                {
                    hsBill.Add(input.InListNO, input);
                }
            }
            if (alInput.Count > 0)
            {
                foreach (FS.HISFC.Models.Pharmacy.Input info in hsBill.Values)
                {
                    billNO = ucFinBill.GetFinBillNO(this.curStockDept.ID, "0310", this.PriveType.ID);
                    if (string.IsNullOrEmpty(billNO) || billNO == "-1")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("获取财务记账单号出错:" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }

                    if (financialMgr.SetFinBill(info, billNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("处理财务记账单号出错:" + financialMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                }


                FS.FrameWork.Management.PublicTrans.Commit();

                Function.ShowMessage("保存成功！\n记账号：" + billNO, MessageBoxIcon.None);


                if (Function.DealExtendBiz("0310", this.PriveType.ID, alInput, ref errInfo) == -1)
                {
                    Function.ShowMessage("入库已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                Function.PrintBill("0310", this.PriveType.ID, alInput);


                this.ClearData();
                this.FreshDataChooseList();
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("数据没有改变：您选择的数据都没有录入发票号!", MessageBoxIcon.Information);
            }
            return 1;
        }
        #endregion

        #region 入库单按钮调用
        /// <summary>
        /// 选择入库单
        /// </summary>
        public int ChooseInputBill()
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

            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = Function.GetBizChooseDataSetting("0310", curPriveType.Memo, curPriveType.ID, "1", ref errInfo);

            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();


                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.InvoicePrive.ChooseBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.InvoicePrive.ChooseBill,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                string companyNO = "All";
                if (this.curFromDept != null && this.curFromDept.ID != "")
                {
                    companyNO = this.curFromDept.ID;
                }
                //替换库存科室和供货公司，时间和状态不替换
                SQL = string.Format(SQL, "{0}", "{1}", "{2}", this.curStockDept.ID, companyNO);

                chooseDataSetting.ListTile = "入库单列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0, 1 };
                curBillChooseColumnIndexs = chooseDataSetting.ColumnIndexs;
                chooseDataSetting.Filter = "";

                chooseDataSetting.ColumnLabels = new string[] 
                { 
                    "入库单号", 
                    "公司编码", 
                    "公司名称", 
                    "入库时间", 
                    "入库人工号"
                };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   100f,// "入库单号", 
                   60f,// "公司编码", 
                   140f,// "公司名称", 
                   120f,// "入库时间", 
                   90f,// "入库人工号"
                };
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "入库单号", 
                   t,// "公司编码", 
                   t,// "公司名称", 
                   t,// "入库时间",
                   t,// "入库人工号"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InvoiceInputL1Setting.xml";
            }

            ArrayList alState = new ArrayList();

            FS.FrameWork.Models.NeuObject state0=new FS.FrameWork.Models.NeuObject();
            state0.ID = "0";
            state0.Name = "未录发票";
            FS.FrameWork.Models.NeuObject state1 = new FS.FrameWork.Models.NeuObject();
            state1.ID = "1";
            state1.Name = "已录发票";

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

        #region 清空
        protected int ClearData()
        {
            totPurchaseCost = 0;
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

            if (this.curChooseDataSetting == null)
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
                this.ClearData();

                string fromDeptNO = "AAAA";
                if (this.curFromDept != null)
                {
                    fromDeptNO = this.curFromDept.ID;
                }

                ArrayList alInput = this.itemMgr.QueryInputInfoByListID(this.curStockDept.ID, values[0], fromDeptNO, "AAAA");
                if (alInput == null)
                {
                    Function.ShowMessage("查询入库数据发生错误：" + itemMgr.Err + "\n请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }

                foreach (FS.HISFC.Models.Pharmacy.Input input in alInput)
                {
                    if (alInput.Count < 50 && SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper == null)
                    {
                        FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(input.Item.ID);
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
                        FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID);
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

        void frmBillChooseList_ChooseCompletedEvent()
        {
            string[] values = this.frmBillChooseList.GetChooseData(this.curBillChooseColumnIndexs);
            if (values == null || values.Length == 0)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                ArrayList alInput = this.itemMgr.QueryInputInfoByListID(this.curStockDept.ID, values[0], values[1], "AAAA");
                if (alInput == null)
                {
                    Function.ShowMessage("查询入库数据发生错误：" + itemMgr.Err + "\n请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }

                foreach (FS.HISFC.Models.Pharmacy.Input input in alInput)
                {
                    if (alInput.Count < 50 && SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper == null)
                    {
                        FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(input.Item.ID);
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
                        FS.HISFC.Models.Pharmacy.Item item =SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID);
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