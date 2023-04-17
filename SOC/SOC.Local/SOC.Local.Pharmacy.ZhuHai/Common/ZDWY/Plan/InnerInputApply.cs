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
    /// [功能描述: 内部入库申请：可扩展为科室入库申请]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public class InnerInputApply : FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz
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

        //private FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
        private FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

        private FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();

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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InnerInputApplySetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "02";
                curPriveType.Name = "内部入库申请";
                curPriveType.Memo = "13";
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
            else if (text == "公式")
            {
                return this.SetApply();
            }
            else if (text == "申请单")
            {
                return this.ChooseApplyBill();
            }
            else if (text == "打印")
            {
                return this.PrintApplyBill();
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
            //{7F3455DA-D5D6-4424-981D-E30AF7EB20E5}
            //return FS.SOC.HISFC.Components.Pharmacy.Function.QueryManagerStockDept();
            return FS.SOC.HISFC.Components.Pharmacy.Function.QueryManagerStockDept(this.curStockDept.ID, "0310");
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
                else if (toolStrip.Items[index].Text == "打印")
                {
                    continue;
                }
                else if (toolStrip.Items[index].Text == "")
                {
                    continue;
                }
                toolStrip.Items.RemoveAt(index);


            }

            ToolStripButton tb1 = new ToolStripButton("公式");
            tb1.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R日消耗);
            tb1.ToolTipText = "调用公式生成申请";
            tb1.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            tb1.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStrip.Items.Insert(0, tb1);

            ToolStripButton tb2 = new ToolStripButton("申请单");
            tb2.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S申请单);
            tb2.ToolTipText = "显示申请单列表";
            tb2.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            tb2.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStrip.Items.Insert(0, tb2);

            ToolStripButton tb3 = new ToolStripButton("打印");
            tb3.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);
            tb3.ToolTipText = "打印申请单";
            tb3.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            tb3.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStrip.Items.Insert(2, tb3);

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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.InnerApplyPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.InnerApplyPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
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
                + " or regular_wb like '%{0}%'" + " or wb_code like '%{0}%'"; ;

                chooseDataSetting.ColumnLabels = new string[] { 
                     "药品编码", 
                    "自定义码",
                    "名称", 
                    "规格", 
                    "库存数",
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
                { 0f,// "药品编码",                    
                   60f,// "自定义码",
                   120f,// "名称", 
                   100f,// "规格", 
                   40f, //"库存数",
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
                   t,// "库存量", 
                   n,// "零售价", 
                   t,// "单位", 
                   t,// "拼音码", 
                   t,// "五笔码", 
                   t,// "通用名", 
                   t,// "通用名拼音码", 
                   t,// "通用名五笔码"
                   t// "通用名自定义码"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InnerInputApplyL0Setting.xml";
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
                this.curIDataChooseList.ShowChooseList(chooseDataSetting.ListTile, string.Format(chooseDataSetting.SQL, fromDeptNO, "{0}", this.curStockDept.ID, "{1}"), chooseDataSetting.IsNeedDrugType, chooseDataSetting.Filter);
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "消耗量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "本科库存", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "参考申请量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "申领科室库存", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "申请量", 60f);
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
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "消耗量", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "本科库存", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "参考申请量", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "申领科室库存", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "申请量", nQty);
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
                    new DataColumn("单位",typeof(string)),
                    new DataColumn("消耗量",typeof(string)),
                    new DataColumn("本科库存",typeof(string)),
                    new DataColumn("参考申请量",typeof(string)),
                    new DataColumn("申领科室库存",typeof(string)),
                    new DataColumn("申请量",typeof(decimal)),
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

            keys[0] = this.dtDetail.Columns["药品编码"];

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

            if (this.hsInnerInputApply.Contains(applyOut.Item.ID))
            {
                Function.ShowMessage("" + applyOut.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsInnerInputApply.Add(applyOut.Item.ID, applyOut);
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

            decimal consumeQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.ExtFlag);
            decimal fitQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.ExtFlag1);

            decimal storageQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.StockDept.User01);
             FS.HISFC.Models.Pharmacy.Storage storageInfo = new FS.HISFC.Models.Pharmacy.Storage();
             if (!string.IsNullOrEmpty(this.curFromDept.ID))
            {
                storageInfo = this.storageMgr.GetStockInfoByDrugCode(this.curFromDept.ID, applyOut.Item.ID);
            }

            if (applyOut.ShowState == "1")
            {
                row["申请量"] = applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                row["消耗量"] = (consumeQty / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                row["参考申请量"] = (fitQty / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                row["单位"] = applyOut.Item.PackUnit;
                row["本科库存"] = (storageQty / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                row["申领科室库存"] = (storageInfo.StoreQty / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
            }
            else
            {
                row["申请量"] = applyOut.Operation.ApplyQty;
                row["消耗量"] = (consumeQty).ToString("F4").TrimEnd('0').TrimEnd('.'); ;
                row["参考申请量"] = (fitQty).ToString("F4").TrimEnd('0').TrimEnd('.'); ;
                row["单位"] = applyOut.Item.MinUnit;
                row["本科库存"] = storageQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                row["申领科室库存"] = storageInfo.StoreQty.ToString("F4").TrimEnd('0').TrimEnd('.');
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
            row["主键"] = applyOut.Item.ID;

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
            string allWarning = string.Empty;
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
                        Function.ShowMessage(dr["名称"].ToString() + "  来源科室是"+SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.StockDept.ID)+"，和您选择的不同", MessageBoxIcon.Information);
                        return -1;
                    }
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(dr["申请量"]) < 0)
                {
                    Function.ShowMessage(dr["名称"].ToString() + "  请输入申请量 申请量不能小于0", MessageBoxIcon.Information);
                    return -1;
                }

                if (FS.FrameWork.Function.NConvert.ToDecimal(dr["申请量"]) == 0)
                {
                    allWarning = allWarning + dr["名称"] + "\n";
                }

            }

            if (!string.IsNullOrEmpty(allWarning))
            {
                allWarning = "以下药品" + allWarning + "申请量为0,是否继续？";
                if ((DialogResult)(MessageBox.Show(allWarning, "提示", MessageBoxButtons.YesNo)) == DialogResult.No)
                {
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
                Function.ShowMessage("内部入库申请的来源科室和库存科室不能相同,请核对", MessageBoxIcon.Error);
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

            DialogResult dialogResult = MessageBox.Show("申请单需要发送才能生效，是否发送到" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.curFromDept.ID) + "？\n注意：发送后不允许修改", "提示>>", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int parm = this.itemMgr.ExamApplyOut(this.curStockDept.ID, billNO);
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

                FS.FrameWork.Management.PublicTrans.Commit();
            }

            if (FS.SOC.HISFC.Components.Pharmacy.Function.DealExtendBiz("0310", this.PriveType.ID, alInnerInputApply, ref errInfo) == -1)
            {
                Function.ShowMessage("入库申请已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            FS.SOC.HISFC.Components.Pharmacy.Function.PrintBill("0310", this.PriveType.ID, alInnerInputApply);


            this.ClearData();

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

        #region 补打申请单

        private int PrintApplyBill()
        {
            ArrayList allApplyOutData = new ArrayList();
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsInnerInputApply[dr["主键"]] as FS.HISFC.Models.Pharmacy.ApplyOut;

                if (applyOut.State == "0")
                {
                    Function.ShowMessage("申请信息还没有保存，无法打印！", MessageBoxIcon.Information);
                    return -1;
                }
                allApplyOutData.Add(applyOut);
            }
            if (allApplyOutData.Count > 0)
            {
                FS.SOC.HISFC.Components.Pharmacy.Function.PrintBill("0310", this.PriveType.ID, allApplyOutData);
            }
            return 1;
        }
        #endregion

        #region 公式调用

        protected int SetApply()
        {
            if (this.curFromDept==null||string.IsNullOrEmpty(this.curFromDept.ID))
            {
                MessageBox.Show("请先选择来源科室！");
                return -1;
            }

            ArrayList alData = new ArrayList();
            if (this.dtDetail.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("是否清空当前数据", "提示>>", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in this.hsInnerInputApply.Values)
                    {
                        alData.Add(applyOut);
                    }
                }
                else
                {
                    this.ClearData();
                }
            }
            if (this.alFormulaPlan != null)
            {
                this.alFormulaPlan.Clear();
            }
            //通过本地化算法获取内部入库申请数据表
            //各项目可以用医院自定义规则生成计划
            this.alFormulaPlan = FS.SOC.HISFC.Components.Pharmacy.Function.SetInnerInputApply(this.curStockDept.ID, this.curFromDept.ID, alData);
            if (alFormulaPlan == null)
            {
                this.alFormulaPlan = this.GetApply(this.curStockDept.ID, this.curFromDept.ID, alData);
            }
            if (alFormulaPlan == null)
            {
                Function.ShowMessage("系统没有实现公式生成计划的功能，请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alFormulaPlan)
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { applyOut.Item.ID});
                if (row == null)
                {
                    if (this.AddApplyObjectToDataTable(applyOut) == -1)
                    {
                        break;
                    }
                }
                else
                {
                    decimal purchaseCost = applyOut.Item.PriceCollection.PurchasePrice * (applyOut.Operation.ApplyQty / applyOut.Item.PackQty);
                    decimal retailCost = applyOut.Item.PriceCollection.RetailPrice * (applyOut.Operation.ApplyQty / applyOut.Item.PackQty);
                    decimal consumeQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.ExtFlag);
                    decimal fitQty = FS.FrameWork.Function.NConvert.ToDecimal(applyOut.ExtFlag1);
                    if (applyOut.ShowState == "1")
                    {
                        row["申请量"] = applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                        row["消耗量"] = (consumeQty / applyOut.Item.PackQty).ToString().TrimEnd('0').TrimEnd('.');
                        row["参考申请量"] = (fitQty / applyOut.Item.PackQty).ToString().TrimEnd('0').TrimEnd('.');
                        row["单位"] = applyOut.Item.PackUnit;
                    }
                    else
                    {
                        row["申请量"] = applyOut.Operation.ApplyQty;
                        row["消耗量"] = (consumeQty).ToString().TrimEnd('0').TrimEnd('.'); ;
                        row["参考申请量"] = (fitQty).ToString().TrimEnd('0').TrimEnd('.'); ;
                        row["单位"] = applyOut.Item.MinUnit;
                    }
                    row["购入价"] = applyOut.Item.PriceCollection.PurchasePrice;
                    row["购入金额"] = FS.FrameWork.Function.NConvert.ToDecimal(purchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.'));
                    row["零售价"] = applyOut.Item.PriceCollection.RetailPrice;
                    row["零售金额"] = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.'));

                }
            }

            for (int rowIndex = 0; rowIndex < this.curIDataDetail.FpSpread.Sheets[0].Rows.Count; rowIndex++)
            {
                string strQty1 = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "参考申请量");
                string strQty2 = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "申领科室库存");
                if (FS.FrameWork.Function.NConvert.ToDecimal(strQty1) > FS.FrameWork.Function.NConvert.ToDecimal(strQty2))
                {
                    this.curIDataDetail.FpSpread.Sheets[0].Rows[rowIndex].BackColor = System.Drawing.Color.Red;
                }
                
            }
            this.curIDataDetail.FpSpread.Sheets[0].SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Cell;
            return 1;
        }

        private ArrayList GetApply(string applyDeptNO, string stockDeptNO, System.Collections.ArrayList alData)
        {
            using (frmSetPlan frmSetPlan = new frmSetPlan())
            {
                frmSetPlan.SetCompletedEven -= new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.SetCompletedEven += new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.Init(applyDeptNO, FS.HISFC.Models.Pharmacy.EnumDrugStencil.Apply);
                frmSetPlan.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frmSetPlan.ShowDialog();


                if (alFormulaPlan == null)
                {
                    //MessageBox.Show("生成内部入库申请发生错误", "错误>>");
                    return null;
                }
                if (alData != null && alData.Count > 0)
                {
                    //界面加载了项目，根据现有项目生成计划
                    for (int index1 = 0; index1 < alData.Count; index1++)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut plan1 = alData[index1] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        for (int index2 = 0; index2 < alFormulaPlan.Count; index2++)
                        {
                            FS.HISFC.Models.Pharmacy.InPlan plan2 = alFormulaPlan[index2] as FS.HISFC.Models.Pharmacy.InPlan;
                            if (plan1.Item.ID == plan2.Item.ID)
                            {
                                plan1.ExtFlag = plan2.OutputQty.ToString();//消耗量
                                plan1.ExtFlag1 = plan2.Extend;//参考量
                                plan1.StockDept.User01 = plan2.StoreQty.ToString();
                                alFormulaPlan.RemoveAt(index2);

                                break;
                            }
                        }
                    }
                    return alData;
                }
                else
                {
                    ArrayList alAppply = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.InPlan plan in alFormulaPlan)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                        applyOut.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(plan.Item.ID);

                        applyOut.StockDept.ID = stockDeptNO;
                        applyOut.ApplyDept.ID = applyDeptNO;
                        applyOut.ExtFlag = plan.OutputQty.ToString();//消耗量
                        applyOut.ExtFlag1 = plan.Extend;//参考量
                        applyOut.StockDept.User01 = plan.StoreQty.ToString();

                        applyOut.Class2Type = "0310";
                        applyOut.PrivType = "02";
                        applyOut.SystemType = "13";

                        applyOut.State = "0";                                   //状态 申请
                        applyOut.ShowState = "1";
                        applyOut.ShowUnit = applyOut.Item.PackUnit;

                        alAppply.Add(applyOut);
                    }

                    return alAppply;
                }
            }
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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.InnerApplyPrive.ChooseBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.InnerApplyPrive.ChooseBill,请与系统管理员联系！", MessageBoxIcon.Error);
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
                chooseDataSetting.ColumnIndexs = new int[] { 0, 1, 3};
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

            if (this.curChooseDataSetting == null)
            {
                return 0;
            }

            string SQL = string.Format(this.curChooseDataSetting.SQL, fromDeptNO, "{0}",curStockDept.ID,"{1}");


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
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "加成金额", wholeSaleCost);
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
                        row["本科库存"] = (storageQty/applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
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
                FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(values[0]);
                if (item == null)
                {
                    Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();

                    applyOut.Item = item;
                    applyOut.Class2Type = "0310";
                    applyOut.PrivType = this.curPriveType.ID;
                    applyOut.SystemType = this.curPriveType.Memo;

                    applyOut.ApplyDept.ID = this.curStockDept.ID;
                    applyOut.StockDept.ID = this.curFromDept.ID;      //库存科室 (目标科室)

                    applyOut.State = "0";                                   //状态 申请
                    applyOut.ShowState = "1";
                    applyOut.ShowUnit = applyOut.Item.PackUnit;

                    decimal storageNum = 0;
                    if (this.itemMgr.GetStorageNum(this.curStockDept.ID, applyOut.Item.ID, out storageNum) == -1)
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
                if(string.IsNullOrEmpty(this.curFromDept.ID))
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