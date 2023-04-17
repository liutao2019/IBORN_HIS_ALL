using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Common.ZDWY.Output
{
    /// <summary>
    /// [功能描述: 出库退库]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-4]<br></br>
    /// </summary>
    public class BackOutput : FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz
    {
        private FS.FrameWork.Models.NeuObject curStockDept = null;
        private FS.FrameWork.Models.NeuObject curTargetDept = null;
        private FS.FrameWork.Models.NeuObject curPriveType = null;
        private FS.FrameWork.Models.NeuObject curGetPerson = null;

        private System.Data.DataTable dtDetail = null;

        private string settingFileName = "";
        private uint costDecimals = 2;

        private decimal totPurchaseCost = 0;
        private decimal totWholeSaleCost = 0;
        private decimal totRetailCost = 0;
        private decimal totRowQty = 0;

        private Hashtable hsOutput = new Hashtable();
        private ArrayList allApplyData = new ArrayList();

        FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList frmBillChooseList = null;
        FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList frmBillApplyChooseList = null;
        int[] curBillChooseColumnIndexs = null;
        int[] curBillApplyChooseColumnIndex = null;


        /// <summary>
        /// 出库相关业务流程处理对应的数据列表选择对象(已被接口标准化)
        /// </summary>
        private SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList curIDataChooseList = null;

        /// <summary>
        /// 获取出库类别获取业务流程对应的数据明细显示控件（接口）实例
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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\BackOutputSetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "02";
                curPriveType.Name = "出库退库";
                curPriveType.Memo = "22";
            }

            this.costDecimals = SOC.HISFC.Components.Pharmacy.Function.GetCostDecimals("0320", curPriveType.Memo);

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
            if (targetDept == null)
            {
                Function.ShowMessage("目标科室为null，请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            this.curTargetDept = targetDept;
            if (this.curTargetDept != null)
            {
                this.FreshDataChooseList();
            }
            return 1;
        }

        public int SetFromDept(FS.FrameWork.Models.NeuObject fromDept)
        {
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
            else if (text == "申请单")
            {
                return this.ChooseBackOuptApplyBill();
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
            if (this.curTargetDept != null)
            {
                this.curTargetDept.Dispose();
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
                this.hsOutput.Clear();
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

            hsOutput.Clear();
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
            return null;
        }

        public ArrayList GetTargetDeptArray()
        {
            //ArrayList alOutDept = Function.QueryInOutDept(this.curStockDept.ID, "0320");
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alOutDept = deptMgr.GetDeptmentAll();
            if (alOutDept == null)
            {
                Function.ShowMessage("获取出库科室发生错误：", MessageBoxIcon.Error);
            }

            SOC.HISFC.BizProcess.Cache.Pharmacy.InitUint();
            alOutDept.AddRange(SOC.HISFC.BizProcess.Cache.Pharmacy.unitHelper.ArrayObject);
            return alOutDept;
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

        #region IBaseBiz 成员

        public int SetGetPerson(FS.FrameWork.Models.NeuObject getPerson)
        {
            this.curGetPerson = getPerson;
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

            ToolStripButton tb1 = new ToolStripButton("申请单");
            tb1.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C出库单);
            tb1.ToolTipText = "显示退库申请单列表";
            tb1.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            tb1.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStrip.Items.Insert(0, tb1);

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
            string targetDeptNO = "";
            if (this.curTargetDept != null)
            {
                targetDeptNO = this.curTargetDept.ID; 
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Output.BackPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Output.BackPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
                chooseDataSetting.ListTile = "药品列表";
                chooseDataSetting.SQL = SQL;
                //{31676173-439F-4df2-AFC5-D519A78969FD}
                chooseDataSetting.ColumnIndexs = new int[] { 0, 10 };
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
                    "批号",
                    "有效期",   //{31676173-439F-4df2-AFC5-D519A78969FD}  
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
                {
                   0f,// "药品编码", 
                   60f,// "自定义码",
                   120f,// "名称", 
                   100f,// "规格", 
                   60f,// "批号",
                   120f,      //有效期//{31676173-439F-4df2-AFC5-D519A78969FD} 
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
                //{31676173-439F-4df2-AFC5-D519A78969FD}
                FarPoint.Win.Spread.CellType.DateTimeCellType d = new FarPoint.Win.Spread.CellType.DateTimeCellType();
                d.ReadOnly = true;

                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "药品编码", 
                   t,// "自定义码",
                   t,// "名称", 
                   t,// "规格", 
                   t,// "批号",
                   d,//有效期//{31676173-439F-4df2-AFC5-D519A78969FD}
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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\BackOutputL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curIDataChooseList.SettingFileName = chooseDataSetting.SettingFileName;

                FS.SOC.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
                if (constantMgr.IsManageStore(targetDeptNO))
                {
                    this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL, targetDeptNO, "{0}"), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
                }
                else
                {
                    this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL, this.curStockDept.ID, "{0}"), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
                } 
                
                this.curIDataChooseList.SetFormat(chooseDataSetting.CellTypes, chooseDataSetting.ColumnLabels, chooseDataSetting.ColumnWiths);
                this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
                this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
            }

            return 1;


        }

        /// <summary>
        /// 初始化出库明细数据控件(接口)
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "名称", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 90f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "可退量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "申请量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "退库量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "有效期", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "备注", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "拼音码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "五笔码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "主键", 0f);

                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nPrice = new FarPoint.Win.Spread.CellType.NumberCellType();
                nPrice.DecimalPlaces = 4;
                nPrice.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nQty1 = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty1.DecimalPlaces = 3;
                nQty1.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nQty2 = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty2.DecimalPlaces = 0;
                nQty2.ReadOnly = true;

                FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty.DecimalPlaces = 3;
                nQty.ReadOnly = false;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "可退量", nQty2);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "申请量", nQty1);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "退库量", nQty2);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "加成价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "加成金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售价", nPrice);
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

            this.curIDataDetail.FpSpread.EditModePermanent = true;
            this.curIDataDetail.FpSpread.EditMode = true;
            this.curIDataDetail.FpSpread.EditModeReplace = true;

            return 1;
        }

        /// <summary>
        /// 初始化出库明细数据的DataTable
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
                    new DataColumn("可退量",typeof(decimal)),
                    new DataColumn("申请量",typeof(decimal)),
                    new DataColumn("退库量",typeof(decimal)),
                    new DataColumn("单位",typeof(string)),
                    new DataColumn("购入价",typeof(decimal)),
                    new DataColumn("购入金额",typeof(decimal)),
                    new DataColumn("加成价",typeof(decimal)),
                    new DataColumn("加成金额",typeof(decimal)),
                    new DataColumn("零售价",typeof(decimal)),
                    new DataColumn("零售金额",typeof(decimal)),
                    new DataColumn("有效期",typeof(DateTime)),
                    new DataColumn("备注",typeof(string)),
                    new DataColumn("拼音码",typeof(string)),
                    new DataColumn("五笔码",typeof(string)),
                    new DataColumn("主键",typeof(string)),

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "退库量" || dc.ColumnName == "购入金额" || dc.ColumnName == "零售金额"||dc.ColumnName == "加成金额")
                {
                    continue;
                }
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
        /// 向DataTable添加出库实体，显示出库明细信息
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        protected int AddOutputObjectToDataTable(FS.HISFC.Models.Pharmacy.Output output)
        {
            if (output == null)
            {
                Function.ShowMessage("向DataTable中添加出库信息失败：出库信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtDetail == null)
            {
                Function.ShowMessage("向DataTable中添加出库信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            if (this.hsOutput.Contains(output.Item.ID + output.GroupNO.ToString()))
            {
                Function.ShowMessage("" + output.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsOutput.Add(output.Item.ID + output.GroupNO.ToString(), output);
            }

           
            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 加成总金额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }

            //output.Class2Type = "0320";
            //output.PrivType = this.curPriveType.ID;
            //output.SystemType = this.curPriveType.Memo;

            DataRow row = this.dtDetail.NewRow();

            row["自定义码"] = output.Item.UserCode;
            row["药品编码"] = output.Item.ID;
            row["批号"] = output.BatchNO;
            row["名称"] = output.Item.Name;
            row["规格"] = output.Item.Specs;

            if (output.ShowState == "1")
            {
                decimal a = (output.Quantity - output.Operation.ReturnQty) / output.Item.PackQty; 
                row["可退量"] = (output.Quantity-output.Operation.ReturnQty) / output.Item.PackQty;
                if (output.User01 == "1")
                {
                    row["退库量"] = output.Operation.ApplyQty / output.Item.PackQty;
                }
                else
                { 
                    row["退库量"] = (output.Quantity-output.Operation.ReturnQty) / output.Item.PackQty;
                }
                row["申请量"] = (output.Operation.ApplyQty) / output.Item.PackQty;
                row["单位"] = output.Item.PackUnit;
            }
            else
            {
                row["可退量"] = output.Quantity - output.Operation.ReturnQty;
                if (output.User01 == "1")
                {
                    row["退库量"] = output.Operation.ApplyQty;
                }
                else
                {
                    row["退库量"] = output.Quantity - output.Operation.ReturnQty;
                }
                row["申请量"] = output.Operation.ApplyQty;
                row["单位"] = output.Item.MinUnit;
            }
            row["购入价"] = output.Item.PriceCollection.PurchasePrice;
            row["购入金额"] = output.PurchaseCost;
            row["加成价"] = output.Item.PriceCollection.WholeSalePrice;
            row["加成金额"] = output.WholeSaleCost;
            row["零售价"] = output.Item.PriceCollection.RetailPrice;
            row["零售金额"] = output.RetailCost;
            row["有效期"] = output.ValidTime.ToShortDateString();
            row["拼音码"] = output.Item.SpellCode;
            row["五笔码"] = output.Item.WBCode;
            row["主键"] = output.Item.ID + output.GroupNO.ToString();

            this.dtDetail.Rows.Add(row);

            return 1;
        }

        #endregion

        #region 刷新数据选择列表
        protected int FreshDataChooseList()
        {
            string targetDeptNO = "";
            if (this.curTargetDept != null)
            {
                targetDeptNO = this.curTargetDept.ID;
            }
            if (curChooseDataSetting == null)
            {
                return 0;
            }

            FS.SOC.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            if (constantMgr.IsManageStore(targetDeptNO))
            {
                this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL, targetDeptNO, "{0}"), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
            }
            else
            {
                this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL, this.curStockDept.ID, "{0}"), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
            }
            this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);
            this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
            this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);

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

            FS.HISFC.Models.Pharmacy.ApplyOut applyInfo = null;

            if (this.curIDataDetail.FpSpread.Sheets[0].ActiveRow.Tag is FS.HISFC.Models.Pharmacy.ApplyOut)
            {
                applyInfo = this.curIDataDetail.FpSpread.Sheets[0].ActiveRow.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
            }

            DialogResult dr = MessageBox.Show("确定删除第" + (rowIndex + 1).ToString() + "行的 " + tradeName + " 吗？", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }
            if (hsOutput.Contains(keys))
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { keys });
                if (row != null)
                {
                    this.dtDetail.Rows.Remove(row);
                }
                this.hsOutput.Remove(keys);

                FS.HISFC.Models.Pharmacy.ApplyOut applyTmp = new FS.HISFC.Models.Pharmacy.ApplyOut();

                if (applyInfo != null)
                {
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in allApplyData)
                    {
                        if (info.ID == applyInfo.ID)
                        {
                            applyTmp = info;
                            break;
                        }
                    }
                }

                allApplyData.Remove(applyTmp);

                this.SetTotInfo();
            }

            return 1;
        }

        #endregion

        #region 保存

        /// <summary>
        /// 有效性判断
        /// </summary>
        /// <returns>1 有效</returns>
        public int CheckValid()
        {
            if (this.curTargetDept.ID == this.curStockDept.ID)
            {
                if (this.curPriveType.ID != "05" && this.curPriveType.ID != "09")
                {
                    Function.ShowMessage("目标科室不能是本科室！", MessageBoxIcon.Information);
                    return -3;
                }
                
            }
            else
            {
                if (this.curPriveType.ID == "05" || this.curPriveType.ID  == "09")
                {
                    Function.ShowMessage("目标科室必须是本科室！", MessageBoxIcon.Information);
                    return -3;
                }
               
            }
            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.curTargetDept.ID).DeptType.ID.ToString() == "PI")
            {
                Function.ShowMessage("目标科室不能为药库", MessageBoxIcon.Information);
                return -3;
            }
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                //if (NConvert.ToDecimal(dr["退库量"]) <= 0)
                //{
                //    Function.ShowMessage(dr["名称"].ToString() + " 退库量不能小于等于零", MessageBoxIcon.Information);
                //    return -1;
                //}
                //if (NConvert.ToDecimal(dr["可退量"]) < NConvert.ToDecimal(dr["退库量"]))
                //{
                //    Function.ShowMessage(dr["名称"].ToString() + " 退库量不能大于当前可退量", MessageBoxIcon.Information);
                //    return -1;

                //}

                string key = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.Output output = this.hsOutput[key] as FS.HISFC.Models.Pharmacy.Output;

                if (output.ShowState == "0")                  //使用最小单位
                {
                    output.Quantity = NConvert.ToDecimal(dr["退库量"]);                       //实发数量
                }
                else                                    //使用包装单位
                {
                    output.Quantity = NConvert.ToDecimal(dr["退库量"]) * output.Item.PackQty; //实发数量
                }
                if (!string.IsNullOrEmpty(output.TargetDept.ID) && output.TargetDept.ID != this.curTargetDept.ID)
                {
                    Function.ShowMessage("目标科室必须是原来出库时选择的科室！", MessageBoxIcon.Information);
                    return -1;
                }
                if (output.User01 == "1" && output.Quantity != output.Operation.ApplyQty)
                {
                    Function.ShowMessage("药房内退的单据必须全部进行确认！", MessageBoxIcon.Information);
                    return -1;
                }
                //if (output.Quantity - Math.Round(output.Quantity, 0) != 0)
                //{
                //    Function.ShowMessage(dr["名称"].ToString() + " 退库量不能为小数", MessageBoxIcon.Information);
                //    return -1;
                //}
            }
            return 1;
        }

        protected int Save()
        {
            if (this.dtDetail.Rows.Count == 0)
            {
                return 0;
            }

            if (this.curTargetDept == null || string.IsNullOrEmpty(this.curTargetDept.ID))
            {
                Function.ShowMessage("请选择目标科室！", MessageBoxIcon.Information);
                return -3;
            }

            string targetName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.curTargetDept.ID);
            if (string.IsNullOrEmpty(targetName))
            {
                targetName = SOC.HISFC.BizProcess.Cache.Pharmacy.GetUnitName(this.curTargetDept.ID);
            }

            DialogResult rs = MessageBox.Show("是否确认" + targetName + "的本次退库操作吗?", "提示>>", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            this.dtDetail.DefaultView.RowFilter = "11=11";
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            string errInfo = "";
            ArrayList alOutput = new ArrayList();

            int param = this.CheckValid();

            if (param < 0)
            {
                if (param == -3)
                {
                    this.curIDataDetail.SetFocusToFilter();
                }
                return param;
            }

            for (int i = 0; i < this.dtDetail.DefaultView.Count; i++)
            {
                this.dtDetail.DefaultView[i].EndEdit();
            }

            DataTable dtAddMofity = this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity == null || dtAddMofity.Rows.Count <= 0)
            {
                return 0;
            }

            this.curIDataDetail.FpSpread.StopCellEditing();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行保存操作..请稍候");
            System.Windows.Forms.Application.DoEvents();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();

            string outBillNO = "";

            foreach (DataRow dr in dtAddMofity.Rows)
            {
                #region 本科室出库记录
                string key = dr["主键"].ToString();

                FS.HISFC.Models.Pharmacy.Output output = this.hsOutput[key] as FS.HISFC.Models.Pharmacy.Output;

                if (outBillNO == "")
                {
                    outBillNO = SOC.HISFC.Components.Pharmacy.Function.GetBillNO(this.curStockDept.ID, "0320", this.PriveType.ID, ref errInfo);
                    if (string.IsNullOrEmpty(outBillNO) || outBillNO == "-1")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("获取最新出库单号出错:" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                output.OutListNO = outBillNO;

                if (output.ShowState == "0")                  //使用最小单位
                {
                    output.Quantity = -NConvert.ToDecimal(dr["退库量"]);                       //实发数量
                }
                else                                    //使用包装单位
                {
                    output.Quantity = -NConvert.ToDecimal(dr["退库量"]) * output.Item.PackQty; //实发数量
                }

                if (output.Quantity == 0)
                {
                    continue;
                }

                output.Operation.ExamQty = output.Quantity;                     //审核数量
                output.Memo = dr["备注"].ToString();

                //直接退库是2
                //内部入库退库申请则是1，暂时不处理这个流程
                output.State = "2";

                output.Operation.Oper.ID = this.itemMgr.Operator.ID;              //操作信息
                output.Operation.Oper.OperTime = sysTime;              //操作信息
                output.Operation.ApplyOper.ID = this.itemMgr.Operator.ID;
                output.Operation.ApplyOper.OperTime = sysTime;
                output.Operation.ExamOper.ID = this.itemMgr.Operator.ID;  //审核人
                output.Operation.ExamOper.OperTime = sysTime;                   //审核日期
                output.Operation.ApproveOper.OperTime = sysTime;
                output.Operation.ApproveOper.ID = this.itemMgr.Operator.ID;
                output.OutDate = sysTime;

                //原来使用报损，现在也使用报损，保证出库类型相同在报表统计时冲账
                if (string.IsNullOrEmpty(output.InListNO) || string.IsNullOrEmpty(output.PrivType))
                {
                    output.PrivType = this.curPriveType.ID;               //出库类型
                }
                output.Class2Type = "0320";
                //系统类别绝对不能改，必须是出库退库
                output.SystemType = this.curPriveType.Memo;           //系统类型
                output.StockDept.ID = this.curStockDept.ID;               //当前科室
                output.TargetDept.ID = this.curTargetDept.ID;              //目标科室
                
                //领药人
                if (this.curGetPerson!=null)
                {
                    output.GetPerson = this.curGetPerson.ID;
                }

                output.DrugedBillNO = "0";                                      //摆药单号 不能为空

                //金额的小数位数，在出库的业务层函数中计算金额用
                output.CostDecimals = this.costDecimals;

                #region 出库后出库库存数
                //出库
                decimal storageNum = 0;
                if (this.itemMgr.GetStorageNum(output.StockDept.ID, output.Item.ID, out storageNum) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("获取库存数量时出错" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                output.StoreQty = storageNum - output.Quantity;               //出库后库存数量
                output.StoreCost = Math.Round(output.StoreQty / output.Item.PackQty * output.Item.PriceCollection.RetailPrice, 3);
                #endregion

                #endregion

                if (string.IsNullOrEmpty(output.ID))
                {
                    bool isTargetArk = false;
                    if (!string.IsNullOrEmpty(output.InListNO))
                    {
                        isTargetArk = true;
                    }
                    //手工选择的数据
                    if (this.itemMgr.OutputReturnForSingleDrug(output.Clone(), -output.Quantity, this.curTargetDept, false, this.curStockDept, isTargetArk) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败，" + output.Item.Name + "出库退库发生错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                else
                {
                    //出库时可能是管理库存的，退库时可能是不管理库存的，也可能反之
                    //对于出库审批还未核准的数据不需要处理入库信息
                    //始终根据出库时有没有形成入库记录处理
                    bool isManagerInput = !string.IsNullOrEmpty(output.InListNO);

                    if (this.itemMgr.OutputReturn(output.Clone(), output.ID, output.SerialNO, isManagerInput) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败，" + output.Item.Name + "出库退库发生错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                }

                alOutput.Add(output);
            }

            if (alOutput.Count > 0)
            {

                if (allApplyData != null && allApplyData.Count > 0)
                {
                    int parameter = 0;
                    foreach(FS.HISFC.Models.Pharmacy.ApplyOut applyOutInfo in allApplyData)
                    {
                        applyOutInfo.State = "2";
                        parameter = this.itemMgr.UpdateApplyOut(applyOutInfo, "1");
                        if (parameter <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存失败，" + applyOutInfo.Item.Name + "更新申请信息错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return -1;
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                allApplyData = new ArrayList();

                if (SOC.HISFC.Components.Pharmacy.Function.DealExtendBiz("0320", this.curPriveType.ID, alOutput, ref errInfo) == -1)
                {
                    Function.ShowMessage("出库已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                SOC.HISFC.Components.Pharmacy.Function.PrintBill("0320", this.PriveType.ID, alOutput);

            
                this.curIDataDetail.Info = "单号：" + outBillNO + ", 品种数：" + this.totRowQty.ToString("F0")
                     + ", 退库购入总额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                     + ", 退库加成总额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                     + ", 退库零售总额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
                
                this.ClearData();
                this.FreshDataChooseList();

            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("请填写退库数量！" + errInfo, MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }
        #endregion

        #region 内部入库退库申请单调用
        public int ChooseBackOuptApplyBill()
        {
            if (this.curIDataDetail.FpSpread.Sheets[0].RowCount > 0)
            {
                DialogResult dr = MessageBox.Show("选择出库单将清空现有数据，是否继续！", "提示>>", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return 0;
                }
                this.ClearData();
            }
            string errInfo = string.Empty;
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = SOC.HISFC.Components.Pharmacy.Function.GetBizChooseDataSetting("0310", curPriveType.Memo, curPriveType.ID, "1", ref errInfo);

            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }

            string SQL = "";

            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();


                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Output.BackPrive.ChooseApplyBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Output.BackPrive.ChooseApplyBill,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                chooseDataSetting.ListTile = "出库单列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0, 1 };
                curBillApplyChooseColumnIndex = chooseDataSetting.ColumnIndexs;
                chooseDataSetting.Filter = "";

                chooseDataSetting.ColumnLabels = new string[] 
                { 
                    "申请单号", 
                    "科室编码", 
                    "科室名称", 
                    "申请时间", 
                    "申请人工号"
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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\BackOutputL2Setting.xml";
            }

            ArrayList alState = new ArrayList();

            FS.FrameWork.Models.NeuObject state0 = new FS.FrameWork.Models.NeuObject();
            state0.ID = "1";
            state0.Name = "申请";

            alState.Add(state0);

            if (this.frmBillApplyChooseList == null)
            {
                this.frmBillApplyChooseList = new FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList();
            }

            string targetDeptNO = "All";
            if (this.curTargetDept != null && this.curTargetDept.ID != "")
            {
                targetDeptNO = this.curTargetDept.ID;
            }
            //替换库存科室和供货公司，时间不替换
            SQL = string.Format(chooseDataSetting.SQL, "{0}", "{1}", "{2}", this.curStockDept.ID, targetDeptNO);

            frmBillApplyChooseList.Init();
            frmBillApplyChooseList.Set(chooseDataSetting.ListTile, SQL, alState, chooseDataSetting.SettingFileName, chooseDataSetting.CellTypes, chooseDataSetting.ColumnLabels, chooseDataSetting.ColumnWiths);

            frmBillApplyChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(frmBillApplyChooseList_ChooseCompletedEvent);
            frmBillApplyChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(frmBillApplyChooseList_ChooseCompletedEvent);


            frmBillApplyChooseList.ShowDialog();


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
                DialogResult dr = MessageBox.Show("选择出库单将清空现有数据，是否继续！", "提示>>", MessageBoxButtons.YesNo);
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

            string SQL = "";

            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();


                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Output.BackPrive.ChooseBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Output.BackPrive.ChooseBill,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
             
                chooseDataSetting.ListTile = "出库单列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0, 1 };
                curBillChooseColumnIndexs = chooseDataSetting.ColumnIndexs;
                chooseDataSetting.Filter = "";

                chooseDataSetting.ColumnLabels = new string[] 
                { 
                    "出库单号", 
                    "科室编码", 
                    "科室名称", 
                    "出库时间", 
                    "出库人工号"
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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\BackOutputL1Setting.xml";
            }

            ArrayList alState = new ArrayList();

            FS.FrameWork.Models.NeuObject state0 = new FS.FrameWork.Models.NeuObject();
            state0.ID = "1";
            state0.Name = "审批";
            FS.FrameWork.Models.NeuObject state1 = new FS.FrameWork.Models.NeuObject();
            state1.ID = "2";
            state1.Name = "核准";

            alState.Add(state0);
            alState.Add(state1);
            if (this.frmBillChooseList == null)
            {
                this.frmBillChooseList = new FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList();
            }

            string targetDeptNO = "All";
            if (this.curTargetDept != null && this.curTargetDept.ID != "")
            {
                targetDeptNO = this.curTargetDept.ID;
            }
            //替换库存科室和供货公司，时间不替换
            SQL = string.Format(chooseDataSetting.SQL, "{0}", "{1}", "{2}", this.curStockDept.ID, targetDeptNO);

            frmBillChooseList.Init();
            frmBillChooseList.Set(chooseDataSetting.ListTile, SQL, alState, chooseDataSetting.SettingFileName, chooseDataSetting.CellTypes, chooseDataSetting.ColumnLabels, chooseDataSetting.ColumnWiths);

            frmBillChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(frmBillChooseList_ChooseCompletedEvent);
            frmBillChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(frmBillChooseList_ChooseCompletedEvent);

            frmBillChooseList.ShowDialog();


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

                if (hsOutput.Contains(keys))
                {
                    FS.HISFC.Models.Pharmacy.Output output = hsOutput[keys] as FS.HISFC.Models.Pharmacy.Output;
                    string strQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "退库量");
                    decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(strQty);
                    if (output.ShowState == "1")
                    {
                        qty = output.Item.PackQty * qty;
                    }
                    decimal purchaseCost = output.Item.PriceCollection.PurchasePrice * (qty / output.Item.PackQty);
                    purchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(purchaseCost.ToString("F" + this.costDecimals.ToString()));
                    decimal wholeSaleCost = output.Item.PriceCollection.WholeSalePrice * (qty / output.Item.PackQty);
                    wholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(wholeSaleCost.ToString("F" + this.costDecimals.ToString()));
                    decimal retailCost = output.Item.PriceCollection.RetailPrice * (qty / output.Item.PackQty);
                    retailCost = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()));


                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "购入金额", purchaseCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "加成金额", purchaseCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "零售金额", retailCost);

                    this.totRowQty++;
                    this.totPurchaseCost += purchaseCost;
                    this.totWholeSaleCost += wholeSaleCost;
                    this.totRetailCost += retailCost;

                    this.curIDataDetail.Info = "当前品种数：" + this.totRowQty.ToString("F0")
                       + ", 退库购入总额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 退库加成总额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 退库零售总额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');


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

            this.hsOutput.Clear();
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);


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
                if (values.Length < 2)
                {
                    Function.ShowMessage("药品选择列表返回的选择数据不符合程序要求，请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }

                string drugNO = values[0];
                string groupNO = values[1];

                if (this.hsOutput.Contains(drugNO + groupNO))
                {
                    Function.ShowMessage("该药品已添加!", MessageBoxIcon.Information);
                    return;
                }

                //{31676173-439F-4df2-AFC5-D519A78969FD}
                if (this.curTargetDept == null)
                {
                    Function.ShowMessage("请选择科室!", MessageBoxIcon.Information);
                    return;
                }

                ArrayList alDetail = this.itemMgr.QueryStorageList(this.curTargetDept.ID, drugNO, NConvert.ToDecimal(groupNO));

                if (alDetail == null || alDetail.Count == 0)
                {
                    Function.ShowMessage("未获取有效的库存明细信息" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return;
                }

                FS.HISFC.Models.Pharmacy.Storage storage = alDetail[0] as FS.HISFC.Models.Pharmacy.Storage;

                FS.HISFC.Models.Pharmacy.Output output = new FS.HISFC.Models.Pharmacy.Output();

                output.StockDept = storage.StockDept;                //库存科室
                output.Item = storage.Item;
                output.GroupNO = storage.GroupNO;
                output.BatchNO = storage.BatchNO;
                output.ValidTime = storage.ValidTime;
                output.PlaceNO = storage.PlaceNO;
                output.InvoiceNO = storage.InvoiceNO;
                output.Producer = storage.Producer;

                output.Quantity = storage.StoreQty;

                if (this.curStockDept.Memo == "PI")
                {
                    output.ShowState = "1";
                    output.ShowUnit = output.Item.PackUnit;
                }
                else
                {
                    output.ShowState = "0";
                    output.ShowUnit = output.Item.MinUnit;
                }
                output.Memo = storage.Memo;

                FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(output.Item.ID);
                if (item == null)
                {
                    Function.ShowMessage("请与系统管理员联系,获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return;
                }
                output.Item.UserCode = item.UserCode;
                output.Item.WBCode = item.WBCode;
                output.Item.SpellCode = item.SpellCode;
                output.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                output.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                if (this.AddOutputObjectToDataTable(output) != -1)
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

        void frmBillApplyChooseList_ChooseCompletedEvent()
        {
            string[] values = this.frmBillApplyChooseList.GetChooseData(this.curBillApplyChooseColumnIndex);
            if (values == null || values.Length == 0)
            {
                Function.ShowMessage("单据选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                ArrayList allApplyOut = this.itemMgr.QueryApplyOutInfoByListCode(values[1], values[0], "1");
                allApplyData = new ArrayList();
                if (allApplyOut == null || allApplyOut.Count == 0)
                {
                    return;
                }
                bool setTDept = false;
                if (!setTDept)
                {
                    if (this.curSetTargetDeptEven != null)
                    {
                        FS.FrameWork.Models.NeuObject tDept = new FS.FrameWork.Models.NeuObject();
                        tDept.ID = values[1];
                        tDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(values[1]);
                        this.curTargetDept = tDept;
                        this.curSetTargetDeptEven(tDept);
                    }
                    setTDept = true;
                }
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in allApplyOut)
                {
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

                        string drugNO = applyInfo.Item.ID;
                        string groupNO = applyInfo.GroupNO.ToString();

                        if (this.hsOutput.Contains(drugNO + groupNO))
                        {
                            Function.ShowMessage("该药品已添加!", MessageBoxIcon.Information);
                            return;
                        }

                        ArrayList alDetail = this.itemMgr.QueryStorageList(this.curTargetDept.ID, drugNO,applyInfo.GroupNO);

                        if (alDetail == null || alDetail.Count == 0)
                        {
                            Function.ShowMessage("未获取有效的库存明细信息" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }

                        FS.HISFC.Models.Pharmacy.Storage storage = alDetail[0] as FS.HISFC.Models.Pharmacy.Storage;

                        FS.HISFC.Models.Pharmacy.Output output = new FS.HISFC.Models.Pharmacy.Output();

                        output.StockDept = storage.StockDept;                //库存科室
                        output.Item = storage.Item;
                        output.GroupNO = storage.GroupNO;
                        output.BatchNO = storage.BatchNO;
                        output.ValidTime = storage.ValidTime;
                        output.PlaceNO = storage.PlaceNO;
                        output.InvoiceNO = storage.InvoiceNO;
                        output.Producer = storage.Producer;
                        output.Operation.ApplyQty = applyInfo.Operation.ApplyQty;
                        output.InListNO = applyInfo.BillNO;
                       
                        output.Quantity = storage.StoreQty;

                        if (this.curStockDept.Memo == "PI")
                        {
                            output.ShowState = "1";
                            output.ShowUnit = output.Item.PackUnit;
                        }
                        else
                        {
                            output.ShowState = "0";
                            output.ShowUnit = output.Item.MinUnit;
                        }
                        output.Memo = storage.Memo;

                        FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(output.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系,获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        output.Item.UserCode = item.UserCode;
                        output.Item.WBCode = item.WBCode;
                        output.Item.SpellCode = item.SpellCode;
                        output.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                        output.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                        //借用字段用来标记属于调取申请单
                        output.User01 = "1";

                        if (this.AddOutputObjectToDataTable(output) != -1)
                        {
                            allApplyData.Add(applyInfo.Clone());

                            this.curIDataDetail.FpSpread.Sheets[0].SetActiveCell(this.curIDataDetail.FpSpread.Sheets[0].RowCount - 1, 0);
                            this.curIDataDetail.FpSpread.Sheets[0].ActiveRow.Tag = applyInfo;
                            if (storage.StoreQty == 0||storage.StoreQty < applyInfo.Operation.ApplyQty)
                            {
                                this.curIDataDetail.FpSpread.Sheets[0].ActiveRow.BackColor = System.Drawing.Color.Red;
                            }
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
            if (values == null || values.Length == 0)
            {
                Function.ShowMessage("单据选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                ArrayList alOutput = this.itemMgr.QueryOutputInfo(this.curStockDept.ID, values[0], "A");
                if (alOutput == null)
                {
                    Function.ShowMessage("查询出库数据发生错误：" + itemMgr.Err + "\n请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }
                bool setTDept = false;
                foreach (FS.HISFC.Models.Pharmacy.Output output in alOutput)
                {
                    if (output.Quantity - output.Operation.ReturnQty <= 0)
                    {
                        continue;
                    }
                    if (!setTDept)
                    {
                        if (this.curSetTargetDeptEven != null)
                        {
                            FS.FrameWork.Models.NeuObject tDept = new FS.FrameWork.Models.NeuObject();
                            tDept.ID = values[1];
                            tDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(values[1]);
                            this.curTargetDept = tDept;
                            this.curSetTargetDeptEven(tDept);
                        }
                        setTDept = true;
                    }
                    if (alOutput.Count < 50 && SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper == null)
                    {
                        FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(output.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        output.Item.UserCode = item.UserCode;
                        output.Item.WBCode = item.WBCode;
                        output.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                        output.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        output.Item.UserCode = item.UserCode;
                        output.Item.WBCode = item.WBCode;
                        output.Item.SpellCode = item.SpellCode;
                        output.Item.NameCollection.RegularName = item.NameCollection.RegularName;
                        output.Item.NameCollection.RegularSpell = item.NameCollection.RegularSpell;

                    }
                    if (this.AddOutputObjectToDataTable(output) == -1)
                    {
                        break;
                    }
                }

            }
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

            if (this.hsOutput.Contains(keys))
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { keys });
                if (row != null)
                {
                    FS.HISFC.Models.Pharmacy.Output output = hsOutput[keys] as FS.HISFC.Models.Pharmacy.Output;

                    this.dtDetail.Columns["单位"].ReadOnly = false;
                    this.dtDetail.Columns["可退量"].ReadOnly = false;
                    this.dtDetail.Columns["申请量"].ReadOnly = false;
                    if (output.ShowState == "1")
                    {
                        output.ShowUnit = output.Item.MinUnit;
                        output.ShowState = "0";

                        row["可退量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["可退量"]) * output.Item.PackQty;
                        row["申请量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["申请量"]) * output.Item.PackQty;
                    }
                    else
                    {
                        output.ShowUnit = output.Item.PackUnit;
                        output.ShowState = "1";
                        row["可退量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["可退量"]) / output.Item.PackQty;
                        row["申请量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["申请量"]) / output.Item.PackQty;
                    }

                    row["单位"] = output.ShowUnit;

                    this.dtDetail.Columns["单位"].ReadOnly = true;
                    this.dtDetail.Columns["可退量"].ReadOnly = true;
                    this.dtDetail.Columns["申请量"].ReadOnly = true;
                }
            }
        }
        #endregion
   
    }
}
