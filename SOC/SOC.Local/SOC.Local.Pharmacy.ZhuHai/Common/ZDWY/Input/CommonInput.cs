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
    /// [功能描述: 药库一般入库：供货公司或外部机构、单位供货入库]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// 说明：
    /// 1、仅仅用于供货公司、其它组织机构的供药入库，特殊入库不用
    /// 2、标志位PayState必须赋值0：因为取消财务核准入库，供货商结存不再插入数据，而是获取入库表中数据
    /// </summary>
    public class CommonInput : FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz
    {

        private SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl ucCommonInput = null;

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
        SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\CommonInputSetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "01";
                curPriveType.Name = "供货入库";
                curPriveType.Memo = "11";
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

            param = this.InitInputControl();

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
                Function.ShowMessage("供货公司为null，请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            this.curFromDept = fromDept;
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
            else if (text == "暂存")
            {
                return this.TempSave();
            }
            return 1;
        }
        #endregion

        #region IBaseBiz 成员 其它

        public void Dispose()
        {
            if (this.ucCommonInput != null && this.ucCommonInput is UserControl)
            {
                ((UserControl)this.ucCommonInput).Dispose();
            }
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
                this.curIDataDetail.FpSpread.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(FpSpread_CellDoubleClick);

            }
            catch { }
        }

        public System.Windows.Forms.UserControl InputInfoControl
        {
            get
            {
                if (ucCommonInput == null)
                {
                    ucCommonInput = SOC.HISFC.Components.Pharmacy.Function.GetInputInfoControl(this.PriveType.ID, false);
                }
                if (ucCommonInput == null || !(ucCommonInput is UserControl))
                {
                    ucCommonInput = new SOC.HISFC.Components.Pharmacy.Common.Input.ucCommonInput();
                }
                return (UserControl)ucCommonInput;
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
            this.ucCommonInput.Clear();

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

            //{F5792566-AF07-49bd-99C0-66987E89E70D}
            //ToolStripButton tb = new ToolStripButton("暂存");
            //tb.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D导入);
            //tb.ToolTipText = "暂存数据";
            //tb.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            //tb.TextImageRelation = TextImageRelation.ImageAboveText;
            //toolStrip.Items.Insert(0, tb);

            return 1;
        }

        /// <summary>
        /// 初始化入库信息录入控件
        /// </summary>
        /// <returns></returns>
        protected int InitInputControl()
        {
            if (ucCommonInput == null)
            {
                ucCommonInput = SOC.HISFC.Components.Pharmacy.Function.GetInputInfoControl(this.PriveType.ID, false);
            }
            if (ucCommonInput == null || !(ucCommonInput is UserControl))
            {
                ucCommonInput = new SOC.HISFC.Components.Pharmacy.Common.Input.ucCommonInput();
            }

            ucCommonInput.InputCompletedEven -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.InputCompletedHander(this.ucCommonInput_InputCompletedHander);
            ucCommonInput.InputCompletedEven += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.InputCompletedHander(this.ucCommonInput_InputCompletedHander);

            return this.ucCommonInput.Init();
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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.CommonPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.CommonPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                chooseDataSetting.ListTile = "药品列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0 };
                chooseDataSetting.Filter = SOC.HISFC.Components.Pharmacy.Function.GetChooseDataFilter("CommonInput");
                if (chooseDataSetting.Filter == "default")
                {
                    chooseDataSetting.Filter =
                        "trade_name like '%{0}%'"
                    + " or custom_code like '%{0}%'"
                    + " or spell_code like '%{0}%'"
                    + " or wb_code like '%{0}%'"
                    + " or regular_spell like '%{0}%'"
                    + " or regular_wb like '%{0}%'";
                }
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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\CommonInputL0Setting.xml";
            }


            string NewSql = "";
            if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.Controldrug.ChooseList", ref NewSql) != -1)
            {
                if (this.curStockDept != null)
                {
                    if (!string.IsNullOrEmpty(this.curStockDept.ID) && !string.IsNullOrEmpty(NewSql))
                    {
                        NewSql = string.Format(NewSql, this.curStockDept.ID);
                        chooseDataSetting.SQL += NewSql;
                    }
                }
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
            //{F5792566-AF07-49bd-99C0-66987E89E70D}
            this.hashFilePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\PHAM\\" + this.PriveType.ID.ToString() + "-" + this.PriveType.Memo + "-" + this.curStockDept.ID + "-" + ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID.ToString() + "-HashTable.bin";
            this.dataFilePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\PHAM\\" + this.PriveType.ID.ToString() + "-" + this.PriveType.Memo + "-" + this.curStockDept.ID + "-" + ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID.ToString() + "-DataTable.xml";
            this.LoadTemparory();

            this.curIDataDetail.Filter = "自定义码 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码 like '%{0}%' ";
            if (this.curIDataDetail.FpSpread != null && this.curIDataDetail.FpSpread.Sheets.Count > 0 && this.dtDetail != null)
            {
                this.curIDataDetail.FpSpread.Sheets[0].DataSource = this.dtDetail.DefaultView;
                this.curIDataDetail.SettingFileName = this.settingFileName;
                this.InitFarPoint();
            }
            //{F5792566-AF07-49bd-99C0-66987E89E70D}
            this.SetTotInfo();

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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "利率", 50f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "发票号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "有效期", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "货位号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批准文号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "国家医保代码", 60f);
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

                FarPoint.Win.Spread.CellType.NumberCellType nPrice = new FarPoint.Win.Spread.CellType.NumberCellType();
                nPrice.DecimalPlaces = 4;
                nPrice.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
                //{D49074A9-8311-43ce-AAC0-80108E65D204}
                nQty.DecimalPlaces = 2;
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
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "利率", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "发票号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "有效期", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "货位号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批准文号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "国家医保代码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "发票分类", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "送货单号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "发票日期", t);
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

            this.curIDataDetail.FpSpread.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(FpSpread_CellDoubleClick);
            this.curIDataDetail.FpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(FpSpread_CellDoubleClick);

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
                    //{E2613CC6-9F59-48b2-9299-D3814C6254A7}
                    new DataColumn("自定义码",typeof(string)),
                    new DataColumn("药品编码",typeof(string)),
                    new DataColumn("名称",typeof(string)),
                    new DataColumn("规格",typeof(string)),
                    new DataColumn("数量",typeof(decimal)),
                    new DataColumn("单位",typeof(string)),
                    new DataColumn("批号",typeof(string)),
                    new DataColumn("有效期",typeof(DateTime)),
                    new DataColumn("购入价",typeof(decimal)),
                    new DataColumn("购入金额",typeof(decimal)),
                    new DataColumn("加成价",typeof(decimal)),
                    new DataColumn("加成金额",typeof(decimal)),
                    new DataColumn("零售价",typeof(decimal)),
                    new DataColumn("零售金额",typeof(decimal)),
                    new DataColumn("利率",typeof(string)),
                    new DataColumn("生产厂家",typeof(string)),
                    new DataColumn("批准文号",typeof(string)),
                    new DataColumn("国家医保代码",typeof(string)),
                    new DataColumn("发票号",typeof(string)),
                    new DataColumn("发票日期",typeof(string)),
                    new DataColumn("货位号",typeof(string)),
                    new DataColumn("发票分类",typeof(string)),
                    new DataColumn("送货单号",typeof(string)),
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
        protected int AddInputObjectToDataTable(FS.HISFC.Models.Pharmacy.Input input, int insertRowIndex)
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

            if (this.hsInput.Contains(input.Item.Product.ApprovalInfo + input.Item.ID + input.BatchNO))
            {
                Function.ShowMessage("" + input.Item.Name + " 已经重复,请检查送货单号、药品批次是否正确！"
                    + "\n同单同批次的药品可以在送货单号中添加序号标记，如：\n单号后加\"1\"表示第一个药，加\"2\"表示第二个药",
                    System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsInput.Add(input.Item.Product.ApprovalInfo + input.Item.ID + input.BatchNO, input);
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
            input.PrivType = this.curPriveType.ID;
            input.SystemType = this.curPriveType.Memo;
            input.SourceCompanyType = "2";

            DataRow row = this.dtDetail.NewRow();

            row["自定义码"] = input.Item.UserCode;
            row["药品编码"] = input.Item.ID;
            row["批号"] = input.BatchNO;
            row["名称"] = input.Item.Name;
            row["规格"] = input.Item.Specs;
            //{D49074A9-8311-43ce-AAC0-80108E65D204}
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
            if (input.ShowState == "1")
            {
                row["购入价"] = input.Item.PriceCollection.PurchasePrice;
            }
            else
            {
                row["购入价"] = input.Item.PriceCollection.PurchasePrice/input.Item.PackQty;
            }
            row["购入金额"] = input.PurchaseCost;
            if (input.ShowState == "1")
            {
                row["加成价"] = input.Item.PriceCollection.WholeSalePrice;
            }
            else
            {
                row["加成价"] = input.Item.PriceCollection.WholeSalePrice / input.Item.PackQty;
            }
          
            row["加成金额"] = input.WholeSaleCost;
            if (input.ShowState == "1")
            {
                row["零售价"] = input.Item.PriceCollection.RetailPrice;
            }
            else
            {
                row["零售价"] = input.Item.PriceCollection.RetailPrice / input.Item.PackQty;
            }
            row["零售价"] = input.Item.PriceCollection.RetailPrice;
            row["零售金额"] = input.RetailCost;
            row["利率"] = input.Item.User03.Replace("购入价", "");
            row["发票号"] = input.InvoiceNO;
            row["有效期"] = input.ValidTime;

            row["货位号"] = input.PlaceNO;
            row["批准文号"] = input.Item.Product.ApprovalInfo;

            row["国家医保代码"] = input.Item.GBCode;

            //row["发票分类"] = input.ValidTime;
            row["送货单号"] = input.DeliveryNO;
            row["发票日期"] = input.InvoiceDate;
            row["生产厂家"] = input.Producer.Name;

            row["拼音码"] = input.Item.SpellCode;
            row["五笔码"] = input.Item.WBCode;
            row["主键"] = input.Item.Product.ApprovalInfo + input.Item.ID + input.BatchNO;

            if (insertRowIndex >= 0)
            {
                this.dtDetail.Rows.InsertAt(row, insertRowIndex);

                this.curIDataDetail.FpSpread.Sheets[0].SetActiveCell(insertRowIndex, 0);
                this.curIDataDetail.FpSpread.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Bottom, FarPoint.Win.Spread.HorizontalPosition.Left);
            }
            else
            {
                this.dtDetail.Rows.Add(row);
                this.curIDataDetail.FpSpread.Sheets[0].SetActiveCell(this.curIDataDetail.FpSpread.Sheets[0].Rows.Count - 1, 0);
                this.curIDataDetail.FpSpread.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Bottom, FarPoint.Win.Spread.HorizontalPosition.Left);
            }

            DataRow dr = this.dtDetail.Rows.Find("合计");

            if (dr != null)
            {
                this.dtDetail.Rows.Remove(dr);
            }

            DataRow drTmp = this.dtDetail.NewRow();

            drTmp["主键"] = "合计";

            drTmp["名称"] = "记录数：";

            drTmp["规格"] = this.totRowQty.ToString("F0");

            drTmp["购入金额"] = this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            drTmp["加成金额"] = this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            drTmp["零售金额"] = this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');

            this.dtDetail.Rows.Add(drTmp);

            //F5792566-AF07-49bd-99C0-66987E89E70D}
            try
            {
                this.SaveTemparory();
            }
            catch
            { 
            }

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
            string key = this.curIDataDetail.FpSpread.Sheets[0].Cells[rowIndex, this.curIDataDetail.FpSpread.GetColumnIndex(0, "主键")].Text;
            string tradeName = this.curIDataDetail.FpSpread.Sheets[0].Cells[rowIndex, this.curIDataDetail.FpSpread.GetColumnIndex(0, "名称")].Text;

            DialogResult dr = MessageBox.Show("确定删除第" + (rowIndex + 1).ToString() + "行的 " + tradeName + " 吗？", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }
            if (hsInput.Contains(key))
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { key });
                if (row != null)
                {
                    this.dtDetail.Rows.Remove(row);
                }
                hsInput.Remove(key);

                this.SetTotInfo();
            }

            DataRow drInfo = this.dtDetail.Rows.Find("合计");

            if (drInfo != null)
            {
                this.dtDetail.Rows.Remove(drInfo);
            }

            DataRow drTmp = this.dtDetail.NewRow();

            drTmp["主键"] = "合计";

            drTmp["名称"] = "记录数：";

            drTmp["规格"] = this.totRowQty.ToString("F0");

            drTmp["购入金额"] = this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            drTmp["加成金额"] = this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            drTmp["零售金额"] = this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');

            this.dtDetail.Rows.Add(drTmp);

            //F5792566-AF07-49bd-99C0-66987E89E70D}
            try
            {
                this.SaveTemparory();
            }
            catch
            {
            }

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
                Function.ShowMessage("请选择供货公司！", MessageBoxIcon.Information);
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
                if (key == "合计")
                {
                    continue;
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(dr["数量"]) <= 0)
                {
                    Function.ShowMessage(dr["名称"].ToString() + "  请输入入库数量 入库数量不能小于等于0", MessageBoxIcon.Information);
                    return -1;
                }
                if (dr["批号"].ToString() == "")
                {
                    Function.ShowMessage("请输入批号", MessageBoxIcon.Information);
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToDateTime(dr["有效期"]) < DateTime.Now)
                {
                    Function.ShowMessage(dr["名称"].ToString() + "  有效期应大于当前日期", MessageBoxIcon.Information);
                    return -1;
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


            //保存本次保存的药品编码 对于同一编码药品多次入库生成不同的批次号 否则退库等操作无法唯一标志一次入库
            System.Collections.Hashtable hsSameItem = new Hashtable();
            ArrayList alInput = new ArrayList();
            string errInfo = "";

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行保存操作..请稍候");
            System.Windows.Forms.Application.DoEvents();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            string strGroupNO = this.itemMgr.GetNewGroupNO();
            if (strGroupNO == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("未正确获取新批次流水号：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            int groupNO = FS.FrameWork.Function.NConvert.ToInt32(strGroupNO);


            //当天操作日期
            DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();

            //入库单据号
            string billNO = "";

            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                string key = dr["主键"].ToString();

                if (key == "合计")
                {
                    continue;
                }

                input = this.hsInput[key] as FS.HISFC.Models.Pharmacy.Input;

                //批次号
                input.GroupNO = groupNO;
                if (hsSameItem.ContainsKey(input.Item.ID))
                {
                    string strNewGroupNO = this.itemMgr.GetNewGroupNO();
                    if (strNewGroupNO == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("未正确获取新批次流水号：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    input.GroupNO = FS.FrameWork.Function.NConvert.ToInt32(strNewGroupNO);
                }
                else
                {
                    hsSameItem.Add(input.Item.ID, null);
                }

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
                input.Company = this.curFromDept;

                //目标单位 = 供货单位      
                input.TargetDept = this.curFromDept;

                //一般入库时的购入价
                input.CommonPurchasePrice = input.PriceCollection.PurchasePrice;

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


                input.State = "0";
                //已输入发票号 直接设置状态为发票入库
                if (!string.IsNullOrEmpty(input.InvoiceNO))
                {
                    //入库审批人作为发票补录人，以后只有在发票信息修改时更改
                    input.Operation.ExamQty = input.Quantity;
                    input.Operation.ExamOper.OperTime = sysTime;
                    input.Operation.ExamOper.ID = input.Operation.Oper.ID;
                    input.State = "1";
                }
                input.PayState = "0";

                input.Operation.ApplyOper.OperTime = sysTime;

                //入库时间，这个比较关键，必须赋值，月结，各种查询都需要入库时间
                input.InDate = sysTime;

                //取消控制参数控制的函数调用，这个基本上都是要更新的，没什么好设置的
                //if (this.itemMgr.UpdateBaseItemWithInputInfo(input) == -1)
                if (this.itemMgr.UpdateItemInputInfo(input) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("入库保存失败：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                //供货单位类型 1 院内科室 2 供货公司 3 扩展
                input.SourceCompanyType = "2";

                if (this.itemMgr.Input(input.Clone(), "1", "0") == -1)
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

            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "单号：" + billNO + ", 品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                     + ", 加成总金额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }

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

            hsInput.Clear();
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.ucCommonInput.Clear();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            if (this.ucCommonInput != null)
            {
                this.ucCommonInput.Clear(true, true);
            }

            //{F5792566-AF07-49bd-99C0-66987E89E70D}
            this.SaveTemparory();

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
                    wholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(purchaseCost.ToString("F" + this.costDecimals.ToString()));
                    decimal retailCost = input.Item.PriceCollection.RetailPrice * (qty / input.Item.PackQty);
                    retailCost = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()));


                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "购入金额", purchaseCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "加成金额", wholeSaleCost);
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

        /// <summary>
        /// 从入库信息录入控件ucCommonInput获取入库实体添加到FarPoint中
        /// </summary>
        /// <returns></returns>
        protected void ucCommonInput_InputCompletedHander()
        {
            int insertRowIndex = -1;
            FS.HISFC.Models.Pharmacy.Input input = this.ucCommonInput.GetInputObject();
            if (input == null)
            {
                return;
            }

            //修改原始信息
            if (hsInput.Contains(input.Item.Product.ApprovalInfo + input.Item.ID + input.BatchNO) && input.Class2Type == "0310")
            {
                DataRow dr = this.dtDetail.Rows.Find(new string[] { input.Item.Product.ApprovalInfo + input.Item.ID + input.BatchNO });
                if (dr != null)
                {
                    insertRowIndex = dtDetail.Rows.IndexOf(dr);
                    this.dtDetail.Rows.Remove(dr);
                }
                //从hsInput清除的同时重新计算全局变量totRowQty等
                FS.HISFC.Models.Pharmacy.Input oldInput = hsInput[input.Item.Product.ApprovalInfo + input.Item.ID + input.BatchNO] as FS.HISFC.Models.Pharmacy.Input;
                this.totRowQty--;
                this.totPurchaseCost -= oldInput.PurchaseCost;
                this.totWholeSaleCost -= oldInput.WholeSaleCost;
                this.totRetailCost -= oldInput.RetailCost;

                hsInput.Remove(input.Item.Product.ApprovalInfo + input.Item.ID + input.BatchNO);
            }

            if (this.AddInputObjectToDataTable(input, insertRowIndex) == 1)
            {
                //添加数据正确，通知主界面完成数据添加，以便转移焦点到药品数据选择的过滤框中
                if (this.curIDataChooseList != null)
                {
                    this.curIDataChooseList.SetFocusToFilter();
                }
                this.ucCommonInput.Clear(false, false);
            }
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
                    Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.Storage storage = this.itemMgr.GetLatestStorage(this.curStockDept.ID, item.ID);
                    if (storage == null)
                    {
                        Function.ShowMessage("请与系统管理员联系：获取药品基本库存信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(storage.StockDept.ID))
                    {
                        this.ucCommonInput.SetInputObject(item, null, false, DateTime.Now.AddYears(-1));
                    }
                    else
                    {
                        this.ucCommonInput.SetInputObject(item, storage, true, storage.ValidTime);
                    }
                    this.ucCommonInput.SetFocusToInputQty();
                }
            }
        }

        void FpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string key = this.curIDataDetail.FpSpread.GetCellText(0, e.Row, "主键");
            string drugNO = this.curIDataDetail.FpSpread.GetCellText(0, e.Row, "药品编码");

            if (this.hsInput.Contains(key))
            {
                FS.HISFC.Models.Pharmacy.Input input = hsInput[key] as FS.HISFC.Models.Pharmacy.Input;
                if (this.ucCommonInput.SetInputObject(input, this.itemMgr.GetItem(drugNO)) == 1)
                {
                    this.ucCommonInput.SetFocusToInputQty();
                }
            }
            else
            {
                Function.ShowMessage("无法修改入库信息：系统没有缓存入库实体到哈希表，请与系统管理员联系！", MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 暂存相关
        //{F5792566-AF07-49bd-99C0-66987E89E70D}

        /// <summary>
        /// 录入数据存储路径
        /// </summary>
        private string dataFilePath = "";

        /// <summary>
        /// 哈希表存储路径
        /// </summary>
        private string hashFilePath = "";


        /// <summary>
        /// 保存暂存文件
        /// </summary>
        private int SaveTemparory()
        {
            try
            {
                //删除缓存文件
                if (this.dtDetail.Rows.Count == 0)
                {
                    System.IO.File.Delete(this.dataFilePath);
                    System.IO.File.Delete(this.hashFilePath);
                    return 0;
                }
                //保存datatable
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
                System.IO.FileStream fs = new System.IO.FileStream(this.dataFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
                formatter.Serialize(fs, this.dtDetail);
                fs.Close();
                //保存hashtable
                System.Runtime.Serialization.IFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream fsh = new System.IO.FileStream(this.hashFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
                bformatter.Serialize(fsh, this.hsInput);
                fsh.Close();
            }
            catch 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 载入暂存文件
        /// </summary>
        private void LoadTemparory()
        {
            //载入datatable
            if (!System.IO.File.Exists(dataFilePath))
            {
                return;
            }
            DataTable tmpDt = null;
            System.IO.FileStream fs = null;
            try
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
                fs = new System.IO.FileStream(this.dataFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None);
                tmpDt = (DataTable)formatter.Deserialize(fs);
            }
            catch (System.IO.FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show("载入暂存文件时出错：" + ex.Message);
                return;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            if (tmpDt != null && tmpDt.Rows.Count > 0)
            {
                this.dtDetail.Rows.Clear();
                foreach (DataRow dr in tmpDt.Rows)
                {
                    this.dtDetail.ImportRow(dr);
                }
            }
            //载入hashtable
            Hashtable tmpHt = null;
            System.IO.FileStream fsh = null;
            try
            {
                System.Runtime.Serialization.IFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                fsh = new System.IO.FileStream(this.hashFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None);
                tmpHt = (Hashtable)bformatter.Deserialize(fsh);
            }
            catch (System.IO.FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show("载入暂存文件时出错：" + ex.Message);
                return;
            }
            finally
            {
                if (fsh != null)
                {
                    fsh.Close();
                }
            }
            if (tmpHt != null && tmpHt.Count > 0)
            {
                this.hsInput.Clear();
                foreach (object key in tmpHt.Keys)
                {
                    this.hsInput.Add(key, tmpHt[key]);
                }
            }
        }

        public void LoadTemparorySelectBill(string billNo)
        {
            this.hashFilePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\PHAM\\" + billNo + "BillNo" + this.PriveType.ID.ToString() + "-" + this.PriveType.Memo + "-" + this.curStockDept.ID + "-" + ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID.ToString() + "-HashTable.bin";
            this.dataFilePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\PHAM\\" + billNo + "BillNo" + this.PriveType.ID.ToString() + "-" + this.PriveType.Memo + "-" + this.curStockDept.ID + "-" + ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID.ToString() + "-DataTable.xml";
            this.LoadTemparory();
        }

        public void SaveTemparoryNewBill(string billNo)
        {
            this.hashFilePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\PHAM\\" + billNo + "BillNo" + this.PriveType.ID.ToString() + "-" + this.PriveType.Memo + "-" + this.curStockDept.ID + "-" + ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID.ToString() + "-HashTable.bin";
            this.dataFilePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\PHAM\\" + billNo + "BillNo" + this.PriveType.ID.ToString() + "-" + this.PriveType.Memo + "-" + this.curStockDept.ID + "-" + ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID.ToString() + "-DataTable.xml";
            this.curIDataDetail.FpSpread.StopCellEditing();
            //保存datatable
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
            System.IO.FileStream fs = new System.IO.FileStream(this.dataFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
            formatter.Serialize(fs, this.dtDetail);
            fs.Close();
            //保存hashtable
            System.Runtime.Serialization.IFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.FileStream fsh = new System.IO.FileStream(this.hashFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
            bformatter.Serialize(fsh, this.hsInput);
            fsh.Close();
        }

        public int TempSave()
        {
            if (this.dtDetail.Rows.Count == 0)
            {
                MessageBox.Show("没找到有效信息！");
                return -1;
            }
            string billNoStr = this.itemMgr.GetDateTimeFromSysDateTime().ToString("yyyyMMdd")
                + (this.GetMaxBillNo() + 1).ToString() + "-" + this.curStockDept.ID.ToString();
            this.SaveTemparoryNewBill(billNoStr);
            MessageBox.Show("暂存成功！");
            return 1;
        }

        public int QueryAllBill()
        {
            string folderFullName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\PHAM\\";
            System.IO.DirectoryInfo TheFolder = new System.IO.DirectoryInfo(folderFullName);
            //遍历文件
            List<string> strBill = new List<string>();
            foreach (System.IO.FileInfo NextFile in TheFolder.GetFiles())
            {
                if (NextFile.Name.Contains("BillNo") && NextFile.Name.Contains(".xml"))
                {
                    string tempC2 = NextFile.Name.Substring(NextFile.Name.IndexOf("BillNo") + 6, 2);
                    string tempC3 = NextFile.Name.Substring(NextFile.Name.IndexOf("BillNo") + 9, 2);
                    if (tempC2 == this.PriveType.ID.ToString() && tempC3 == this.PriveType.Memo.ToString())
                    {
                        strBill.Add(NextFile.Name);
                    }
                }
            }
            if (strBill.Count > 0)
            {
                FrmChooseBill FrmChooseBill = new FrmChooseBill();
                FrmChooseBill.Init(strBill);
                FrmChooseBill.ShowDialog();
                if (FrmChooseBill.isOK)
                {
                    this.LoadTemparorySelectBill(FrmChooseBill.billNo);
                }
            }
            else
            {
                MessageBox.Show("没找到相关单据！");
            }
            return 1;
        }

        private int GetMaxBillNo()
        {
            string folderFullName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\PHAM\\";
            System.IO.DirectoryInfo TheFolder = new System.IO.DirectoryInfo(folderFullName);
            //遍历文件
            List<string> strBill = new List<string>();
            foreach (System.IO.FileInfo NextFile in TheFolder.GetFiles())
            {
                if (NextFile.Name.Contains("BillNo"))
                {
                    string tempC2 = NextFile.Name.Substring(NextFile.Name.IndexOf("BillNo"), 4);
                    string tempC3 = NextFile.Name.Substring(NextFile.Name.IndexOf("BillNo") + 5, 2);
                    if (tempC2 == this.PriveType.ID.ToString() && tempC3 == this.PriveType.Memo.ToString())
                    {
                        strBill.Add(NextFile.Name);
                    }
                }
            }
            if (strBill.Count > 0)
            {
                int i = 0;
                string tempNo = "";
                foreach (string tempStrNo in strBill)
                {
                    tempNo = tempStrNo.Substring(8, tempStrNo.IndexOf('-'));
                    try
                    {
                        if (i < FS.FrameWork.Function.NConvert.ToInt32(tempNo))
                        {
                            i = FS.FrameWork.Function.NConvert.ToInt32(tempNo);
                        }
                    }
                    catch
                    { }

                }
                return i;
            }
            else
            {
                return 0;
            }
        }

        #endregion

    }
}