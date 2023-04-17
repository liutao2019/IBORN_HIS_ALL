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
    /// [功能描述: 药库出库审批：可扩展为所有科室出库审批]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-4]<br></br>
    /// 说明：
    /// 1、出库审批不增加对方库存，待对方核准入库时增加库存
    /// 2、出库审批不允许向不管理库存的科室操作
    /// 3、出库审批后应立即核准入库，避免其中调价、盘点
    /// 4、出库审批的退库应该根据对方是否核准进行
    /// </summary>
    public class ExamOutput : FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz
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
        private Hashtable hsApplyOut = new Hashtable();

        FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseListCanDelete frmBillChooseList = null;
        int[] curBillChooseColumnIndexs = null;

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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ExamOutputSetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "04";
                curPriveType.Name = "出库审批";
                curPriveType.Memo = "25";
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
                this.hsApplyOut.Clear();

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

            this.hsOutput.Clear();
            this.hsApplyOut.Clear();
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
            //{7F3455DA-D5D6-4424-981D-E30AF7EB20E5}
            //ArrayList alOutDept = SOC.HISFC.Components.Pharmacy.Function.QueryManagerStockDept();
            ArrayList alOutDept = SOC.HISFC.Components.Pharmacy.Function.QueryManagerStockDept(this.curStockDept.ID, "0320");
            if (alOutDept == null)
            {
                Function.ShowMessage("获取出库科室发生错误：", MessageBoxIcon.Error);
            }

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
            //MessageBox.Show(keyData.ToString());
            if (keyData == Keys.Enter)
            {
                this.SetTotInfo();
            }
            else if ( keyData == Keys.F12)
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


            ToolStripButton tb = new ToolStripButton("申请单");
            tb.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S申请单);
            tb.ToolTipText = "显示申请单列表";
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
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = SOC.HISFC.Components.Pharmacy.Function.GetBizChooseDataSetting("0320", curPriveType.Memo, curPriveType.ID, "0", ref errInfo);
            if (chooseDataSetting == null)
            {
                Function.ShowMessage("本地化数据选择列表发生错误：" + errInfo + "请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            if (chooseDataSetting.IsDefault)
            {
                chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                string SQL = "";
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Output.ExamPrive.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Output.ExamPrive.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
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
                    "单位", 
                    //"购入价", 
                    "零售价", 
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
                   40f,// "库存量",
                   //40f,// "购入价", 
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
                   //n,// "购入价", 
                   n,// "零售价", 
                   t,// "单位", 
                   t,// "拼音码", 
                   t,// "五笔码", 
                   t,// "通用名", 
                   t,// "通用名拼音码", 
                   t,// "通用名五笔码"
                   t// "通用名自定义码"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ExamOutputL0Setting.xml";
            }

            this.curChooseDataSetting = chooseDataSetting;
            if (this.curIDataChooseList != null)
            {
                this.curIDataChooseList.Init();
                this.curIDataChooseList.SettingFileName = chooseDataSetting.SettingFileName;


                this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(this.curChooseDataSetting.SQL, this.curStockDept.ID, "{0}"), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
                this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);
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
                this.curIDataDetail.FpSpread.SetColumnWith(0, "审批", 26f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "自定义码", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "药品编码", 0f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "批号", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "名称", 100f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "规格", 90f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "库存量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "出库量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "单位", 60f);
                //this.curIDataDetail.FpSpread.SetColumnWith(0, "购入价", 60f);
                //this.curIDataDetail.FpSpread.SetColumnWith(0, "购入金额", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "零售价", 60f);
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

                FarPoint.Win.Spread.CellType.CheckBoxCellType c = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "审批", c);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                //this.curIDataDetail.FpSpread.SetColumnCellType(0, "批号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "库存量", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "出库量", tWrite);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                //this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                //this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入金额", nPrice);
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
                    new DataColumn("审批",typeof(bool)),
                    new DataColumn("自定义码",typeof(string)),
                    new DataColumn("药品编码",typeof(string)),
                    //new DataColumn("批号",typeof(string)),
                    new DataColumn("名称",typeof(string)),
                    new DataColumn("规格",typeof(string)),
                    new DataColumn("库存量",typeof(decimal)),
                    new DataColumn("申请量",typeof(decimal)),
                    new DataColumn("出库量",typeof(string)),
                    new DataColumn("单位",typeof(string)),
                    //new DataColumn("购入价",typeof(decimal)),
                    //new DataColumn("购入金额",typeof(decimal)),
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
                if (dc.ColumnName == "审批" || dc.ColumnName == "出库量" || dc.ColumnName == "零售金额"||dc.ColumnName=="备注")
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

            if (this.hsOutput.Contains(output.Item.ID))
            {
                Function.ShowMessage("" + output.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsOutput.Add(output.Item.ID, output);
            }

            this.totPurchaseCost += output.PurchaseCost;
            this.totWholeSaleCost += output.WholeSaleCost;
            this.totRetailCost += output.RetailCost;
            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "当前录入品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 加成总金额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }

            output.Class2Type = "0320";
            output.PrivType = this.curPriveType.ID;
            output.SystemType = this.curPriveType.Memo;

            DataRow row = this.dtDetail.NewRow();

            if (SOC.HISFC.BizProcess.Cache.Common.GetDept(this.curStockDept.ID).DeptType.ID.ToString() == (FS.HISFC.Models.Base.EnumDepartmentType.P).ToString())
            {
                row["审批"] = true;
            }
            else
            {
                row["审批"] = false;
            }
            row["自定义码"] = output.Item.UserCode;
            row["药品编码"] = output.Item.ID;
            //row["批号"] = output.BatchNO;
            row["名称"] = output.Item.Name;
            row["规格"] = output.Item.Specs;

            decimal storageNum = 0;
            if (this.itemMgr.GetStorageNum(this.curStockDept.ID, output.Item.ID, out storageNum) == -1)
            {
                Function.ShowMessage("获取库存数量时出错，请与系统管理员联系！\n报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            if (output.ShowState == "1")
            {
                row["库存量"] = storageNum / output.Item.PackQty;
                row["申请量"] = output.Operation.ApplyQty / output.Item.PackQty;
                if (storageNum < output.Quantity)
                {
                    row["出库量"] = row["库存量"];
                }
                else
                {
                    row["出库量"] = output.Quantity / output.Item.PackQty;
                }
                row["单位"] = output.Item.PackUnit;
            }
            else
            {
                row["库存量"] = storageNum;
                row["申请量"] = output.Operation.ApplyQty;
                if (storageNum < output.Quantity)
                {
                    row["出库量"] = row["库存量"];
                }
                else
                {
                    row["出库量"] = output.Quantity;
                }
                row["单位"] = output.Item.MinUnit;
            }
            //row["购入价"] = output.Item.PriceCollection.PurchasePrice;
            //row["购入金额"] = output.PurchaseCost;
            row["零售价"] = output.Item.PriceCollection.RetailPrice;
            row["零售金额"] = output.RetailCost;
            row["备注"] = output.Memo;

            row["拼音码"] = output.Item.SpellCode;
            row["五笔码"] = output.Item.WBCode;
            row["主键"] = output.Item.ID;

            this.dtDetail.Rows.Add(row);

            this.curIDataDetail.FpSpread.Sheets[0].ActiveRowIndex = this.curIDataDetail.FpSpread.Sheets[0].RowCount - 1;

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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Output.ExamPrive.ChooseBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Output.ExamPrive.ChooseBill,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                string targetDeptNO = "All";
                if (this.curTargetDept != null && this.curTargetDept.ID != "")
                {
                    targetDeptNO = this.curTargetDept.ID;
                }
                //替换库存科室和供货公司，时间和状态不替换
                SQL = string.Format(SQL, targetDeptNO, this.curStockDept.ID, "{0}", "{1}", "{2}");

                chooseDataSetting.ListTile = "申请单列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0, 1, 3 };
                curBillChooseColumnIndexs = chooseDataSetting.ColumnIndexs;
                chooseDataSetting.Filter = "";

                chooseDataSetting.ColumnLabels = new string[] 
                { 
                    "申请单号", 
                    "申请科室", 
                    "科室名称", 
                    "状态", 
                    "申请时间", 
                    "申请人工号"
                };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   100f,// "申请单号", 
                   60f,// "申请科室", 
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
                   t,// "申请科室", 
                   t,// "科室名称", 
                   t,// "状态", 
                   t,// "申请时间",
                   t,// "申请人工号"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ExamOutputL1Setting.xml";
            }

            ArrayList alState = new ArrayList();

            FS.FrameWork.Models.NeuObject state0 = new FS.FrameWork.Models.NeuObject();

            //对方发送后才能看到
            state0.ID = "1";
            state0.Name = "未审批";

            alState.Add(state0);
            //alState.Add(state1);

            if (this.frmBillChooseList == null)
            {
                this.frmBillChooseList = new FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseListCanDelete();
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
            if (curChooseDataSetting == null)
            {
                return 0;
            }
            this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL, this.curStockDept.ID, "{0}"), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
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
                Function.ShowMessage("目标科室不能是本科室！", MessageBoxIcon.Information);
                return -3;
            }

            //{01EF8F9C-2923-4795-B62C-6D1C5CDE2529}
            //if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.curTargetDept.ID).DeptType.ID.ToString() == "PI")
            //{
            //    Function.ShowMessage("目标科室不能为药库", MessageBoxIcon.Information);
            //    return -3;
            //}


            bool isContinue = false;
            foreach (DataRow dr in this.dtDetail.Rows)
            {
                if (NConvert.ToDecimal(dr["出库量"]) < 0)
                {
                    if (!isContinue && NConvert.ToBoolean(dr["审批"]))
                    {
                        SOC.Windows.Forms.CheckDialogResult cdr = SOC.Windows.Forms.MessageCheckBox.Show(dr["名称"].ToString() + " 出库数量为0，是否继续审批！");
                        if (cdr == FS.SOC.Windows.Forms.CheckDialogResult.No)
                        {
                            return -1;
                        }
                        else if (cdr == FS.SOC.Windows.Forms.CheckDialogResult.NoChecked)
                        {
                            return -1;
                        }
                        else if (cdr == FS.SOC.Windows.Forms.CheckDialogResult.Yes)
                        {
                            continue;
                        }
                        else if (cdr == FS.SOC.Windows.Forms.CheckDialogResult.YesChecked)
                        {
                            isContinue = true;
                        }
                    }
                    else if (isContinue && NConvert.ToBoolean(dr["审批"]))
                    {
                        continue;
                    }
                    else
                    {
                        Function.ShowMessage(dr["名称"].ToString() + " 出库量不能小于零", MessageBoxIcon.Information);
                        return -1;
                    }
                }
                if (NConvert.ToDecimal(dr["库存量"]) - NConvert.ToDecimal(dr["出库量"]) < -0.0001m)
                {
                    Function.ShowMessage(dr["名称"].ToString() + " 出库量不能大于当前库存量", MessageBoxIcon.Information);
                    return -1;
                }

                string key = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.Output output = this.hsOutput[key] as FS.HISFC.Models.Pharmacy.Output;

                if (output.ShowState == "0")                  //使用最小单位
                {
                    output.Quantity = NConvert.ToDecimal(dr["出库量"]);                       //实发数量
                }
                else                                    //使用包装单位
                {
                    output.Quantity = NConvert.ToDecimal(dr["出库量"]) * output.Item.PackQty; //实发数量
                }

                if (output.Quantity - Math.Round(output.Quantity, 0) != 0)
                {
                    Function.ShowMessage(dr["名称"].ToString() + " 出库量不能为小数", MessageBoxIcon.Information);
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

            if (this.curTargetDept == null || string.IsNullOrEmpty(this.curTargetDept.ID))
            {
                Function.ShowMessage("请选择目标科室！", MessageBoxIcon.Information);
                return -3;
            }

            if (this.curGetPerson == null || string.IsNullOrEmpty(this.curGetPerson.ID))// {47F1129F-CC17-41fa-9F1A-6D4E9085FD2E}
            {
                Function.ShowMessage("请选择领用人！", MessageBoxIcon.Information);
                return -5;
            }

            DialogResult rs = MessageBox.Show("确认向" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.curTargetDept.ID) + "进行出库操作吗?", "提示>>", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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
                    output.Quantity = NConvert.ToDecimal(dr["出库量"]);                       //实发数量
                }
                else                                    //使用包装单位
                {
                    output.Quantity = NConvert.ToDecimal(dr["出库量"]) * output.Item.PackQty; //实发数量
                }

                output.Memo = dr["备注"].ToString();

                output.Operation.Oper.ID = this.itemMgr.Operator.ID;
                output.Operation.Oper.OperTime = sysTime;

                output.Operation.ExamOper.ID = this.itemMgr.Operator.ID;  //审核人
                output.Operation.ExamOper.OperTime = sysTime;                   //审核日期
                output.Operation.ApproveQty = output.Quantity;                     //审核数量

                output.OutDate = sysTime;

                output.PrivType = this.curPriveType.ID;               //出库类型
                output.SystemType = this.curPriveType.Memo;           //系统类型
                output.StockDept.ID = this.curStockDept.ID;               //当前科室
                output.TargetDept.ID = this.curTargetDept.ID;              //目标科室
                output.State = "1";                                             //状态 审批

                //领药人
                if (this.curGetPerson != null)
                {
                    output.GetPerson = this.curGetPerson.ID;
                }

                //金额的小数位数，在出库的业务层函数中计算金额用
                output.CostDecimals = this.costDecimals;

                #region 一般出库后出库库存数
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

                if (!string.IsNullOrEmpty(output.Operation.ApplyOper.ID))
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsApplyOut[output.DrugedBillNO] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (applyOut == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败，请与系统管理员联系！\n报告错误：目标科室的申请信息在哈希表中不存在", MessageBoxIcon.Error);
                        return -1;
                    }

                    applyOut.Operation.Oper.ID = this.itemMgr.Operator.ID;
                    applyOut.Operation.Oper.OperTime = sysTime;

                    //applyOut没有审批量，核准即是发药量
                    applyOut.Operation.ApproveOper.ID = this.itemMgr.Operator.ID;
                    applyOut.Operation.ApproveOper.OperTime = sysTime;
                    applyOut.Operation.ApproveQty += output.Quantity;

                    if (applyOut.Operation.ApproveQty >= applyOut.Operation.ApplyQty || NConvert.ToBoolean(dr["审批"]))
                    {
                        applyOut.State = "2";
                    }
                    if (applyOut.Item.PackQty == 0)
                    {
                        applyOut.Item.PackQty = 1;
                    }
                    applyOut.Memo = "已发" + (applyOut.Operation.ApproveQty / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.PackUnit
                        + "，共申请" + (applyOut.Operation.ApplyQty / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.PackUnit;

                    param = this.itemMgr.UpdateApplyOut(applyOut, "1");
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败，更新目标科室申请信息发生错误，请与系统管理员联系！\n报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    else if (param == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败，目标科室申请信息已经审批，请刷新！", MessageBoxIcon.Information);
                        return -1;
                    }
                }
                else
                {
                    output.Operation.ApplyOper.ID = this.itemMgr.Operator.ID;     //申请人
                    output.Operation.ApplyQty = output.Quantity;            //申请数量
                    output.Operation.ApplyOper.OperTime = sysTime;
                }

                output.DrugedBillNO = "0";                                      //摆药单号 不能为空

                if (this.itemMgr.Output(output, null, false) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，出库保存发生错误，请与系统管理员联系！\n报告错误：" + itemMgr.Err, MessageBoxIcon.Information);
                    return -1;
                }

                alOutput.Add(output);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (SOC.HISFC.Components.Pharmacy.Function.DealExtendBiz("0320", this.curPriveType.ID, alOutput, ref errInfo) == -1)
            {
                Function.ShowMessage("出库已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            SOC.HISFC.Components.Pharmacy.Function.PrintBill("0320", this.PriveType.ID, alOutput);

            string adjustNote = "以下药品：\n";

            ArrayList needAdjust = new ArrayList();

            foreach (FS.HISFC.Models.Pharmacy.Output info in alOutput)
            {
                if (info.PriceCollection.RetailPrice != info.PriceCollection.WholeSalePrice)
                {
                    needAdjust.Add(info);
                    adjustNote += info.Item.Name + "(当前售价：" + info.PriceCollection.RetailPrice + ")" +
                                    "出库到了参考价为" + info.PriceCollection.WholeSalePrice + "的批次,请确认是否需要调价！";
                }
            }

            if (needAdjust.Count > 0)
            {
                Function.ShowMessage(adjustNote, MessageBoxIcon.Information);
            }

            this.curIDataDetail.Info = "单号：" + ", 品种数：" + this.totRowQty.ToString("F0")
                     + ", 购入总额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                     + ", 加成总额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                     + ", 出库总额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');


            this.ClearData();
            this.FreshDataChooseList();


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
                    string strQty = this.curIDataDetail.FpSpread.GetCellText(0, rowIndex, "出库量");
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
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "加成金额", wholeSaleCost);
                    this.curIDataDetail.FpSpread.SetCellValue(0, rowIndex, "零售金额", retailCost);

                    this.totRowQty++;
                    this.totPurchaseCost += purchaseCost;
                    this.totWholeSaleCost += totWholeSaleCost;
                    this.totRetailCost += retailCost;

                    this.curIDataDetail.Info = "当前品种数：" + this.totRowQty.ToString("F0")
                       + ", 购入总额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 加成总额：" + this.totWholeSaleCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                       + ", 零售总额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');


                }
            }

            return 1;
        }
        #endregion

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
                FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(values[0]);
                if (item == null)
                {
                    Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.Output output = new FS.HISFC.Models.Pharmacy.Output();
                    output.Item = item;
                    output.ShowState = "1";
                    output.ShowUnit = item.PackUnit;
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
        }

        void frmBillChooseList_ChooseCompletedEvent()
        {
            this.hsApplyOut.Clear();

            string[] values = this.frmBillChooseList.GetChooseData(this.curBillChooseColumnIndexs);
            if (values == null || values.Length < 3)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
               
                ArrayList alApplyout = this.itemMgr.QueryApplyOutInfoByListCode(values[1], values[0], values[2]);
                if (alApplyout == null)
                {
                    Function.ShowMessage("查询内部入库申请数据发生错误：" + itemMgr.Err + "\n请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyout)
                {
                    if (applyOut.State != "1")
                    {
                        continue;
                    }
                    FS.HISFC.Models.Pharmacy.Item item = this.itemMgr.GetItem(applyOut.Item.ID);
                    if (item == null)
                    {
                        Function.ShowMessage("请与系统管理员联系：获取药品基本信息出错：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }

                    //这里记录下来，保存的时候还需更新申请的状态
                    if (!hsApplyOut.Contains(applyOut.ID))
                    {
                        this.hsApplyOut.Add(applyOut.ID, applyOut);
                    }

                    if (this.curTargetDept == null || this.curTargetDept.ID == "")
                    {
                        FS.FrameWork.Models.NeuObject targetDept = new FS.FrameWork.Models.NeuObject();
                        targetDept.ID = applyOut.ApplyDept.ID;
                        this.curTargetDept = targetDept;

                        if (this.curSetTargetDeptEven != null)
                        {
                            this.curSetTargetDeptEven(this.curTargetDept);
                        }
                    }

                    FS.HISFC.Models.Pharmacy.Output output = new FS.HISFC.Models.Pharmacy.Output();

                    output.Item = item;

                    output.Operation.ApplyOper = applyOut.Operation.ApplyOper;
                    output.Operation.ApplyQty = applyOut.Operation.ApplyQty - applyOut.Operation.ApproveQty;   //申请量

                    //出库量为剩余申请量，库存不足时则为库存量
                    output.Quantity = applyOut.Operation.ApplyQty - applyOut.Operation.ApproveQty;

                    output.Memo = applyOut.Memo;                               //备注信息
                   // if (this.curStockDept.Memo == "P" && !string.IsNullOrEmpty(applyOut.ShowState))
                    if(!string.IsNullOrEmpty(applyOut.ShowState))
                    {
                        output.ShowState = applyOut.ShowState;
                        output.ShowUnit = applyOut.ShowUnit;
                    }
                    else
                    {
                        output.ShowState = "1";
                        output.ShowUnit = item.PackUnit;
                    }
                    output.DrugedBillNO = applyOut.ID;

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
                    this.dtDetail.Columns["库存量"].ReadOnly = false;
                    this.dtDetail.Columns["申请量"].ReadOnly = false;
                    this.dtDetail.Columns["出库量"].ReadOnly = false;
                    if (output.ShowState == "1")
                    {
                        output.ShowUnit = output.Item.MinUnit;
                        output.ShowState = "0";

                        row["库存量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["库存量"]) * output.Item.PackQty;
                        row["申请量"] = output.Operation.ApplyQty;
                        row["出库量"] = output.Quantity;
                    }
                    else
                    {
                        output.ShowUnit = output.Item.PackUnit;
                        output.ShowState = "1";
                        row["库存量"] = FS.FrameWork.Function.NConvert.ToDecimal(row["库存量"]) / output.Item.PackQty;
                        row["申请量"] = output.Operation.ApplyQty / output.Item.PackQty;
                        row["出库量"] = output.Quantity / output.Item.PackQty;
                    }

                    row["单位"] = output.ShowUnit;

                    this.dtDetail.Columns["单位"].ReadOnly = true;
                    this.dtDetail.Columns["库存量"].ReadOnly = true;
                    this.dtDetail.Columns["申请量"].ReadOnly = true;
                }
            }
        }
        #endregion
    }
}
