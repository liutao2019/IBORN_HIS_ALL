﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;


namespace FS.SOC.HISFC.Components.Pharmacy.Common.Input
{
    /// <summary>
    /// [功能描述: 特殊入库：供货公司或外部机构、单位供货入库以外的入库]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public class SpecialInput : Base.IBaseBiz
    {

        private SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl ucSpecialInput = null;

        private FS.FrameWork.Models.NeuObject curStockDept = null;
        private FS.FrameWork.Models.NeuObject curFromDept = null;
        private FS.FrameWork.Models.NeuObject curPriveType = null;

        private System.Data.DataTable dtDetail = null;

        private string settingFileName = "";
        private string settingFromDeptFileName = "";
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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\SpecialInputSetting.xml";
            this.settingFromDeptFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\SpecialFromDeptSetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "07";
                curPriveType.Name = "药品回笼";
                curPriveType.Memo = "1C";
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
            if (fromDept == null )
            {
                Function.ShowMessage("药品来源为null，请与系统管理员联系！", MessageBoxIcon.Error);
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
            return 1;
        }
        #endregion

        #region IBaseBiz 成员 其它

        public void Dispose()
        {
            if (this.ucSpecialInput != null && this.ucSpecialInput is UserControl)
            {
                ((UserControl)this.ucSpecialInput).Dispose();
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
                if (ucSpecialInput == null)
                {
                    ucSpecialInput = Function.GetInputInfoControl(this.PriveType.ID, true);
                }
                if (ucSpecialInput == null || !(ucSpecialInput is UserControl))
                {
                    ucSpecialInput = new ucSpecialInput();
                }

                return (UserControl)ucSpecialInput; 
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
            this.ucSpecialInput.Clear();

            return 1;
        }

        public ArrayList GetFromDeptArray(ref string fromDeptTypeName)
        {
            fromDeptTypeName = "药品来源：";
            if (this.PriveType != null && this.PriveType.ID == "07" && this.curStockDept != null && !string.IsNullOrEmpty(this.curStockDept.ID))
            {
                ArrayList alFromDept = new ArrayList();
                alFromDept.Add(this.curStockDept);
                return alFromDept;
            }

            //盘盈入库权限
            if (this.PriveType != null && this.PriveType.ID == "55" && this.curStockDept != null && !string.IsNullOrEmpty(this.curStockDept.ID))
            {
                ArrayList alFromDept = new ArrayList();
                alFromDept.Add(this.curStockDept);
                return alFromDept;
            }

            if (this.PriveType != null && this.PriveType.ID == "57" && this.curStockDept != null && !string.IsNullOrEmpty(this.curStockDept.ID))
            {
                ArrayList alFromDept = new ArrayList();
                alFromDept.Add(this.curStockDept);
                return alFromDept;
            }

            if (System.IO.File.Exists(this.settingFromDeptFileName))
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(this.settingFromDeptFileName, "PrivType", "SpecialInputType" + this.PriveType.ID, "0")))
                {
                    ArrayList alFromDept = new ArrayList();
                    alFromDept.Add(this.curStockDept);
                    return alFromDept;
                }
            }
            return Function.QueryCommonDept();
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

            return 1;
        }

        /// <summary>
        /// 初始化入库信息录入控件
        /// </summary>
        /// <returns></returns>
        protected int InitInputControl()
        {
            if (ucSpecialInput == null)
            {
                ucSpecialInput = Function.GetInputInfoControl(this.PriveType.ID, true);
            }
            if (ucSpecialInput == null || !(ucSpecialInput is UserControl))
            {
                ucSpecialInput = new ucSpecialInput();
            }

            ucSpecialInput.InputCompletedEven -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.InputCompletedHander(this.ucSpecialInput_InputCompletedHander);
            ucSpecialInput.InputCompletedEven += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.InputCompletedHander(this.ucSpecialInput_InputCompletedHander);
           
            string unitShowState = "1";
            if (this.curStockDept.Memo == "PI")
            {
                unitShowState = "0";
            }
            return this.ucSpecialInput.Init(unitShowState);
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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.SpecialPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.SpecialPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
                chooseDataSetting.ListTile = "药品列表";

                //SQL中有库存科室和药品类别，药品类别需要在药品选择列表控件中赋值
                chooseDataSetting.SQL = string.Format(SQL, this.curStockDept.ID, "{0}");

                chooseDataSetting.ColumnIndexs = new int[] { 0 };
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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\SpecialInputL0Setting.xml";
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "有效期", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "货位号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "记录号", 60f);
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
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "有效期", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "货位号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "记录号", t);
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
                    new DataColumn("有效期",typeof(DateTime)),
                    new DataColumn("货位号",typeof(string)),
                    new DataColumn("记录号",typeof(string)),
                    new DataColumn("生产厂家",typeof(string)),
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


            DataColumn[] keys = new DataColumn[3];

            //keys[0] = this.dtDetail.Columns["记录号"];
            keys[0] = this.dtDetail.Columns["药品编码"];
            keys[1] = this.dtDetail.Columns["批号"];

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

            if (this.hsInput.Contains( input.Item.ID + input.BatchNO))
            {
                Function.ShowMessage("" + input.Item.Name + " 已经重复,请检查记录号、药品批次是否正确！"
                    + "\n同单同批次的药品可以在记录号中添加序号标记，如：\n加\"1\"表示第一个药，加\"2\"表示第二个药",
                    System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsInput.Add(input.Item.ID + input.BatchNO, input);
            }

            this.totPurchaseCost += input.PurchaseCost;
            this.totRetailCost += input.RetailCost;
            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
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
            row["有效期"] = input.ValidTime;

            row["货位号"] = input.PlaceNO;
            row["记录号"] = input.DeliveryNO;
            row["生产厂家"] = input.Producer.Name;

            row["拼音码"] = input.Item.SpellCode;
            row["五笔码"] = input.Item.WBCode;
            row["主键"] = input.DeliveryNO + input.Item.ID + input.BatchNO;

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
            //string deliveryNO = this.curIDataDetail.FpSpread.Sheets[0].Cells[rowIndex, this.curIDataDetail.FpSpread.GetColumnIndex(0, "送货单号")].Text;
            string drugNO = this.curIDataDetail.FpSpread.Sheets[0].Cells[rowIndex, this.curIDataDetail.FpSpread.GetColumnIndex(0, "药品编码")].Text;
            string batchNO = this.curIDataDetail.FpSpread.Sheets[0].Cells[rowIndex, this.curIDataDetail.FpSpread.GetColumnIndex(0, "批号")].Text;
            string tradeName = this.curIDataDetail.FpSpread.Sheets[0].Cells[rowIndex, this.curIDataDetail.FpSpread.GetColumnIndex(0, "名称")].Text;

            DialogResult dr = MessageBox.Show("确定删除第" + (rowIndex + 1).ToString() + "行的 " + tradeName + " 吗？", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }
            if (hsInput.Contains( drugNO + batchNO))
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { drugNO, batchNO });
                if (row != null)
                {
                    this.dtDetail.Rows.Remove(row);
                }
                hsInput.Remove( drugNO + batchNO);

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
        public int CheckValid()
        {
            if (this.curFromDept == null || string.IsNullOrEmpty(this.curFromDept.ID))
            {
                Function.ShowMessage("请选择药品来源！", MessageBoxIcon.Information);
                return -2;
            }

            if (this.dtDetail.Rows.Count == 0)
            {
                Function.ShowMessage("请选择要入库的药品！", MessageBoxIcon.Information);
                return -4;
            }

            foreach (DataRow dr in this.dtDetail.Rows)
            {
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
                string key = dr["记录号"].ToString() + dr["药品编码"].ToString() + dr["批号"].ToString();

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
                    billNO = Function.GetBillNO(this.curStockDept.ID, "0310", this.PriveType.ID, ref errInfo);
                    if (string.IsNullOrEmpty(billNO) || billNO == "-1")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("获取最新入库单号出错:" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                input.InListNO = billNO;

                //是否特殊入库 0 否 1 是
                input.SpecialFlag = "1";

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

                //入库时的购入价
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



                //入库审批人作为发票补录人，以后只有在发票信息修改时更改
                input.Operation.ExamQty = input.Quantity;
                input.Operation.ExamOper.OperTime = sysTime;
                input.Operation.ExamOper.ID = input.Operation.Oper.ID;
                input.State = "1";

                input.PayState = "0";

                input.Operation.ApplyOper.OperTime = sysTime;

                //入库时间，这个比较关键，必须赋值，月结，各种查询都需要入库时间
                input.InDate = sysTime;

                //供货单位类型 1 院内科室 2 供货公司 3 扩展
                input.SourceCompanyType = "1";

                if (this.itemMgr.Input(input.Clone(), "1", "0") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("入库保存失败：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                alInput.Add(input);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (Function.DealExtendBiz("0310", this.PriveType.ID, alInput, ref errInfo) == -1)
            {
                Function.ShowMessage("入库已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            Function.PrintBill("0310", this.PriveType.ID, alInput);
            

            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "单号：" + billNO + ", 品种数：" + this.totRowQty.ToString("F0")
                     + ", 购入总额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                     + ", 零售总额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }

            this.ClearData();


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

            if (this.ucSpecialInput != null)
            {
                this.ucSpecialInput.Clear();
            }

            return 1;
        }

        #endregion

        #region 金额计算
        protected virtual int SetTotInfo()
        {
            this.totPurchaseCost = 0;
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

                    decimal purchaseCost = input.Item.PriceCollection.PurchasePrice * (qty / input.Item.PackQty);
                    purchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(purchaseCost.ToString("F" + this.costDecimals.ToString()));
                    decimal retailCost = input.Item.PriceCollection.RetailPrice * (qty / input.Item.PackQty);
                    retailCost = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()));


                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "购入金额", purchaseCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "零售金额", retailCost);

                    this.totRowQty++;
                    this.totPurchaseCost += purchaseCost;
                    this.totRetailCost += retailCost;

                    this.curIDataDetail.Info = "当前品种数：" + this.totRowQty.ToString("F0")
                       + ", 购入金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 零售金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');


                }
            }

            return 1;
        }
        #endregion

        #endregion

        #region 事件

        /// <summary>
        /// 从入库信息录入控件ucSpecialInput获取入库实体添加到FarPoint中
        /// </summary>
        /// <returns></returns>
        protected void ucSpecialInput_InputCompletedHander()
        {
            FS.HISFC.Models.Pharmacy.Input input = this.ucSpecialInput.GetInputObject();
            if (input == null)
            {
                return;
            }

            //修改原始信息
            if (hsInput.Contains(input.Item.ID + input.BatchNO) && input.Class2Type == "0310")
            {
                DataRow dr = this.dtDetail.Rows.Find(new string[] { input.Item.ID, input.BatchNO });
                if (dr != null)
                {
                    this.dtDetail.Rows.Remove(dr);
                }

                //从hsInput清除的同时重新计算全局变量totRowQty等
                FS.HISFC.Models.Pharmacy.Input oldInput = hsInput[input.Item.ID + input.BatchNO] as FS.HISFC.Models.Pharmacy.Input;
                this.totRowQty--;
                this.totPurchaseCost -= oldInput.PurchaseCost;
                this.totRetailCost -= oldInput.RetailCost;

                hsInput.Remove( input.Item.ID + input.BatchNO);
            }

            if (this.AddInputObjectToDataTable(input) == 1)
            {
                //添加数据正确，通知主界面完成数据添加，以便转移焦点到药品数据选择的过滤框中
                if (this.curIDataChooseList != null)
                {
                    this.curIDataChooseList.SetFocusToFilter();
                }
                this.ucSpecialInput.Clear();
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
                    this.ucSpecialInput.SetInputObject(item);
                    this.ucSpecialInput.SetFocusToInputQty();
                }
            }
        }

        void FpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string deliverNO = this.curIDataDetail.FpSpread.Sheets[0].Cells[e.Row, this.curIDataDetail.FpSpread.GetColumnIndex(0, "送货单号")].Text;
            string drugNO = this.curIDataDetail.FpSpread.Sheets[0].Cells[e.Row, this.curIDataDetail.FpSpread.GetColumnIndex(0, "药品编码")].Text;
            string batchNO = this.curIDataDetail.FpSpread.Sheets[0].Cells[e.Row, this.curIDataDetail.FpSpread.GetColumnIndex(0, "批号")].Text;

            if (this.hsInput.Contains(drugNO + batchNO))
            {
                FS.HISFC.Models.Pharmacy.Input input = hsInput[deliverNO + drugNO + batchNO] as FS.HISFC.Models.Pharmacy.Input;
                if (this.ucSpecialInput.SetInputObject(input, this.itemMgr.GetItem(drugNO)) == 1)
                {
                    this.ucSpecialInput.SetFocusToInputQty();
                }
            }
            else
            {
                Function.ShowMessage("无法修改入库信息：系统没有缓存入库实体到哈希表，请与系统管理员联系！", MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}