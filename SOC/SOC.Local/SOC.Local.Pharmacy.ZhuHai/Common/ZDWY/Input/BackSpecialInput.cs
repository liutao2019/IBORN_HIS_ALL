using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Common.ZDWY.Input
{
    /// <summary>
    /// [功能描述: 入库退库：特殊入库的退库]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-4]<br></br>
    /// </summary>
    public class BackSpecialInput : FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz
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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPBackSpecialInputSetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "06";
                curPriveType.Name = "入库退库";
                curPriveType.Memo = "19";
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
            fromDeptTypeName = "药品来源：";

            if (this.PriveType != null && this.PriveType.ID == "06" && this.curStockDept != null && !string.IsNullOrEmpty(this.curStockDept.ID))
            {
                ArrayList alFromDept = new ArrayList();
                alFromDept.Add(this.curStockDept);
                return alFromDept;
            }
            return SOC.HISFC.Components.Pharmacy.Function.QueryCommonDept();
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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.BackPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.BackPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
                chooseDataSetting.ListTile = "药品列表";
                chooseDataSetting.SQL = string.Format(SQL, this.curStockDept.ID, "{0}");
                chooseDataSetting.ColumnIndexs = new int[] { 0, 9 };
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
                {
                   0f,// "药品编码", 
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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPBackSpecialInputL0Setting.xml";
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "可退数量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "退库数量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "退库购额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "加成价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "加成退库金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售价", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "退库零额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "有效期", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "货位号", 60f);
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

                //[2011-07-21] zhaozf 修改为可退小数
                FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty.DecimalPlaces = 2;
                nQty.ReadOnly = false;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "可退数量", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "退库数量", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "退库购额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "加成价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "加成退库金额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "零售价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "退库零额", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "有效期", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "货位号", t);
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
                    new DataColumn("可退数量",typeof(string)),
                    new DataColumn("退库数量",typeof(decimal)),
                    new DataColumn("单位",typeof(string)),
                    new DataColumn("购入价",typeof(decimal)),
                    new DataColumn("退库购额",typeof(decimal)),
                    new DataColumn("加成价",typeof(decimal)),
                    new DataColumn("加成退库金额",typeof(decimal)),
                    new DataColumn("零售价",typeof(decimal)),
                    new DataColumn("退库零额",typeof(decimal)),
                    new DataColumn("有效期",typeof(DateTime)),
                    new DataColumn("货位号",typeof(string)),
                    new DataColumn("生产厂家",typeof(string)),
                    new DataColumn("备注",typeof(string)),
                    new DataColumn("拼音码",typeof(string)),
                    new DataColumn("五笔码",typeof(string)),
                    new DataColumn("主键",typeof(string)),

                }
                );

            foreach (DataColumn dc in this.dtDetail.Columns)
            {
                if (dc.ColumnName == "退库数量" || dc.ColumnName == "发票号" || dc.ColumnName == "退库购额" || dc.ColumnName == "退库零额" || dc.ColumnName == "加成退库金额")
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

            if (this.hsInput.Contains(input.Item.ID + input.GroupNO.ToString()))
            {
                Function.ShowMessage("" + input.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsInput.Add(input.Item.ID + input.GroupNO.ToString(), input);
            }

          
            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 加成总金额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }

            DataRow row = this.dtDetail.NewRow();

            row["自定义码"] = input.Item.UserCode;
            row["药品编码"] = input.Item.ID;
            row["批号"] = input.BatchNO;
            row["名称"] = input.Item.Name;
            row["规格"] = input.Item.Specs;
            if (input.ShowState == "1")
            {
                row["可退数量"] = (input.Quantity - input.Operation.ReturnQty) / input.Item.PackQty;
                row["单位"] = input.Item.PackUnit;
            }
            else
            {
                row["可退数量"] = input.Quantity - input.Operation.ReturnQty;
                row["单位"] = input.Item.MinUnit;
            }
            row["购入价"] = input.Item.PriceCollection.PurchasePrice;
            row["加成价"] = input.Item.PriceCollection.WholeSalePrice;
            row["零售价"] = input.Item.PriceCollection.RetailPrice;
            row["有效期"] = input.ValidTime;

            row["货位号"] = input.PlaceNO;
            row["生产厂家"] = input.Producer.Name;

            row["拼音码"] = input.Item.SpellCode;
            row["五笔码"] = input.Item.WBCode;
            row["主键"] = input.Item.ID + input.GroupNO.ToString();

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
            string key = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex,"主键");
            string tradeName = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "名称");

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
                if (FS.FrameWork.Function.NConvert.ToDecimal(dr["退库数量"]) <= 0)
                {
                    Function.ShowMessage(dr["名称"].ToString() + "  请输入退库数量 退库数量不能小于等于0", MessageBoxIcon.Information);
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(dr["可退数量"]) < FS.FrameWork.Function.NConvert.ToDecimal(dr["退库数量"]))
                {
                    Function.ShowMessage(dr["名称"].ToString() + "  退库数量不能大于可退数量，请修改！", MessageBoxIcon.Information);
                    return -1;
                }


                string key = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.Input input = this.hsInput[key] as FS.HISFC.Models.Pharmacy.Input;

                if (input.ShowState == "0")                  //使用最小单位
                {
                    input.Quantity = NConvert.ToDecimal(dr["退库数量"]);                       //实发数量
                }
                else                                    //使用包装单位
                {
                    input.Quantity = NConvert.ToDecimal(dr["退库数量"]) * input.Item.PackQty; //实发数量
                }

                //[2011-07-21] zhaozf 修改为可退小数
                //if (input.Quantity - Math.Round(input.Quantity, 0) != 0)
                //{
                //    Function.ShowMessage(dr["名称"].ToString() + " 退库数量不能为小数", MessageBoxIcon.Information);
                //    return -1;
                //}
            }

            return 1;
        }

        protected int Save()
        {
            this.curIDataDetail.SetFocusToFilter();
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

            DataTable dtAddMofity = this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity == null || dtAddMofity.Rows.Count <= 0)
            {
                return 0;
            }

            string billNO = "";
            string errInfo = "";

            //定义事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();

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

            //标志是否存在保存操作
            bool isSaveOperation = false;
            ArrayList alInput = new ArrayList();
            foreach (DataRow dr in dtAddMofity.Rows)
            {
                decimal backQty = NConvert.ToDecimal(dr["退库数量"]);
                if (backQty == 0)
                {
                    continue;
                }

                string key = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.Input input = (this.hsInput[key] as FS.HISFC.Models.Pharmacy.Input).Clone();
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

                input.InListNO = billNO;                                      //退库单号
                input.Quantity = -backQty;                                     //入库数量(负数)

                decimal retailCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;
                retailCost = FS.FrameWork.Function.NConvert.ToDecimal(retailCost.ToString("F" + this.costDecimals.ToString()));
                input.RetailCost = retailCost;

                input.InDate = sysTime;
                input.Operation.ApplyOper.ID = this.itemMgr.Operator.ID;
                input.Operation.ApplyOper.OperTime = sysTime;
                input.Operation.Oper = input.Operation.ApplyOper;
                input.Operation.ExamQty = input.Quantity;
                input.Operation.ExamOper = input.Operation.Oper;
                input.Operation.ExamOper.OperTime = sysTime;
                input.State = "1";

                input.SpecialFlag = "1";

                //供货单位 
                input.Company = this.curFromDept;

                //目标单位 = 供货单位      
                input.TargetDept = this.curFromDept;
                input.SourceCompanyType = "1";

                input.PrivType = this.curPriveType.ID;            //入库类型
                input.SystemType = this.curPriveType.Memo;         //系统类型


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

                int parm = this.itemMgr.Input(input, "1", "1");
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存 " + input.Item.Name + " 发生错误: " + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                isSaveOperation = true;
                alInput.Add(input);
            }
            if (isSaveOperation)
            {
                FS.FrameWork.Management.PublicTrans.Commit();

                if (SOC.HISFC.Components.Pharmacy.Function.DealExtendBiz("0310", this.PriveType.ID, alInput, ref errInfo) == -1)
                {
                    Function.ShowMessage("退库已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                SOC.HISFC.Components.Pharmacy.Function.PrintBill("0310", this.PriveType.ID, alInput);

                if (this.curIDataDetail != null)
                {
                    this.curIDataDetail.Info = "单号：" + billNO + ", 品种数：" + this.totRowQty.ToString("F0")
                         + ", 退库购入总额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                         + ", 退库加成总额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                         + ", 退库零售总额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
                }

                this.ClearData();
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Input.BackPrive.Special.ChooseBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Input.BackPrive.Special.ChooseBill,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                string companyNO = "All";
                if (this.curFromDept != null && this.curFromDept.ID != "")
                {
                    companyNO = this.curFromDept.ID;
                }
                //替换库存科室和供货公司，时间不替换
                SQL = string.Format(SQL, "{0}", "{1}", this.curStockDept.ID, companyNO);

                chooseDataSetting.ListTile = "入库单列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0, 1 };
                curBillChooseColumnIndexs = chooseDataSetting.ColumnIndexs;
                chooseDataSetting.Filter = "";

                chooseDataSetting.ColumnLabels = new string[] 
                { 
                    "入库单号", 
                    "科室编码", 
                    "科室名称", 
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

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPBackSpecialInputL1Setting.xml";
            }


            if (this.frmBillChooseList == null)
            {
                this.frmBillChooseList = new FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList();
            }

            frmBillChooseList.Init();
            frmBillChooseList.Set(chooseDataSetting.ListTile, chooseDataSetting.SQL, null, chooseDataSetting.SettingFileName, chooseDataSetting.CellTypes, chooseDataSetting.ColumnLabels, chooseDataSetting.ColumnWiths);

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
                    string strQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "退库数量");
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


                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "退库购额", purchaseCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "加成退库金额", wholeSaleCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "退库零额", retailCost);

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

                if (this.hsInput.Contains(drugNO + groupNO))
                {
                    Function.ShowMessage("该药品已添加!", MessageBoxIcon.Information);
                    return;
                }

                ArrayList alDetail = this.itemMgr.QueryStorageList(this.curStockDept.ID, drugNO, NConvert.ToDecimal(groupNO));

                if (alDetail == null || alDetail.Count == 0)
                {
                    Function.ShowMessage("未获取有效的库存明细信息" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return;
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
                    return;
                }
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

                if (!string.IsNullOrEmpty(values[1]))
                {
                    if (SOC.HISFC.BizProcess.Cache.Common.deptHelper==null)
                    {
                        SOC.HISFC.BizProcess.Cache.Common.InitDept();
                    }
                    if (SOC.HISFC.BizProcess.Cache.Common.deptHelper != null)
                    {

                        FS.FrameWork.Models.NeuObject companyObj = SOC.HISFC.BizProcess.Cache.Common.deptHelper.GetObjectFromID(values[1]);
                        if (companyObj != null && !string.IsNullOrEmpty(companyObj.ID) && this.curSetFromDeptEven != null)
                        {
                            this.curSetFromDeptEven(companyObj);
                            this.curFromDept = companyObj;
                        }
                    }
                }

                foreach (FS.HISFC.Models.Pharmacy.Input input in alInput)
                {
                    if (alInput.Count < 50 && SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper == null)
                    {
                        FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(input.Item.ID);
                        if (item == null)
                        {
                            Function.ShowMessage("请与系统管理员联系，获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
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
                            Function.ShowMessage("请与系统管理员联系，获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
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

        #region 单位手工转换
        private void ConvertUnit()
        {
            if (this.curIDataDetail.FpSpread.Sheets[0].Rows.Count <= 0)
            {
                return;
            }
            int rowIndex = this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex;

            string keys = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "主键");

            if (this.hsInput.Contains(keys))
            {
                DataRow row = this.dtDetail.Rows.Find(new string[] { keys });
                if (row != null)
                {
                    FS.HISFC.Models.Pharmacy.Input input = hsInput[keys] as FS.HISFC.Models.Pharmacy.Input;

                    this.dtDetail.Columns["单位"].ReadOnly = false;
                    this.dtDetail.Columns["可退量"].ReadOnly = false;
                    if (input.ShowState == "1")
                    {
                        input.ShowUnit = input.Item.MinUnit;
                        input.ShowState = "0";

                        row["可退量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["可退量"]) * input.Item.PackQty;
                    }
                    else
                    {
                        input.ShowUnit = input.Item.PackUnit;
                        input.ShowState = "1";
                        row["可退量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["可退量"]) / input.Item.PackQty;
                    }

                    row["单位"] = input.ShowUnit;

                    this.dtDetail.Columns["单位"].ReadOnly = true;
                    this.dtDetail.Columns["可退量"].ReadOnly = true;
                }
            }
        }
        #endregion

    }
}