using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Common.ZDWY.Fin
{
    /// <summary>
    /// [功能描述: 财务核准出库]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-6]<br></br>
    /// </summary>
    public class ApproveOuput : FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz
    {
        private FS.FrameWork.Models.NeuObject curStockDept = null;
        private FS.FrameWork.Models.NeuObject curTargetDept = null;
        private FS.FrameWork.Models.NeuObject curPriveType = null;
        private FS.FrameWork.Models.NeuObject curGetPerson = null;

        private System.Data.DataTable dtDetail = null;

        private string settingFileName = "";
        private uint costDecimals = 2;

        private decimal totPurchaseCost = 0;
        private decimal totRetailCost = 0;
        private decimal totRowQty = 0;

        private Hashtable hsOutput = new Hashtable();

        FS.SOC.HISFC.Components.Pharmacy.Base.frmBillChooseList frmBillChooseList = null;
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

        FS.SOC.HISFC.Components.Pharmacy.Common.Fin.ucFinBill ucFinBill = new FS.SOC.HISFC.Components.Pharmacy.Common.Fin.ucFinBill();

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

            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPPhaFinApproveOutputSetting.xml";

            if (this.curPriveType == null)
            {
                this.curPriveType = new FS.FrameWork.Models.NeuObject();
                curPriveType.ID = "30";
                curPriveType.Name = "出库核准";
                curPriveType.Memo = "1F";
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

            this.FreshDataChooseList();


            param = this.InitToolStrip(toolStrip);
            if (param == -1)
            {
                return param;
            }

            if (ucFinBill == null)
            {
                ucFinBill = new FS.SOC.HISFC.Components.Pharmacy.Common.Fin.ucFinBill();
            }

            param = ucFinBill.InitFinBillNO(this.curStockDept.ID, "0320", "30");

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
            if (this.curIDataChooseList != null)
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
            if (text == "保存")
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
            if (this.curTargetDept != null)
            {
                this.curTargetDept.Dispose();
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
                this.hsOutput.Clear();

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
                    ucFinBill = new FS.SOC.HISFC.Components.Pharmacy.Common.Fin.ucFinBill();
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

            this.hsOutput.Clear();
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
            ArrayList alOutDept = SOC.HISFC.Components.Pharmacy.Function.QueryManagerStockDept();
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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Fin.ApproveOutput.ChooseList", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Fin.ApproveOutput.ChooseList,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }
                chooseDataSetting.ListTile = "单据列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0 };
                chooseDataSetting.Filter = "out_list_code like '%{0}%'";

                chooseDataSetting.ColumnLabels = new string[] { "出库单号", "日期", "领料单位" };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   100f,// "出库单号", 
                   120f,// "日期", 
                   120f,// "领料单位", 
                };
                chooseDataSetting.IsNeedDrugType = false;

                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "出库单号", 
                   t,// "日期", 
                   t// "领料单位", 
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPPhaFinApproveOutputL0Setting.xml";
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
            this.curIDataDetail.Filter = "自定义码 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码 like '%{0}%'";
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
                //this.curIDataDetail.FpSpread.SetColumnWith(0, "库存量", 60f);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "出库量", 60f);
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

                FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
                nQty.DecimalPlaces = 0;
                nQty.ReadOnly = true;

                this.curIDataDetail.FpSpread.SetColumnCellType(0, "自定义码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "药品编码", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "批号", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "名称", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "规格", t);
                //this.curIDataDetail.FpSpread.SetColumnCellType(0, "库存量", nQty);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "出库量", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "单位", t);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入价", nPrice);
                this.curIDataDetail.FpSpread.SetColumnCellType(0, "购入金额", nPrice);
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
                    //new DataColumn("库存量",typeof(decimal)),
                    new DataColumn("出库量",typeof(string)),
                    new DataColumn("单位",typeof(string)),
                    new DataColumn("购入价",typeof(decimal)),
                    new DataColumn("购入金额",typeof(decimal)),
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

            if (this.hsOutput.Contains(output.Item.ID + output.ID + output.SerialNO.ToString()))
            {
                Function.ShowMessage("" + output.Item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                hsOutput.Add(output.Item.ID + output.ID + output.SerialNO.ToString(), output);
            }

            this.totPurchaseCost += output.PurchaseCost;
            this.totRetailCost += output.RetailCost;
            this.totRowQty++;
            if (this.curIDataDetail != null)
            {
                this.curIDataDetail.Info = "单据号：" + output.OutListNO
                    + ", 当前录入品种数：" + this.totRowQty.ToString("F0")
                    + ", 购入总金额：" + this.totPurchaseCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.')
                    + ", 零售总金额：" + this.totRetailCost.ToString("F" + this.costDecimals.ToString()).TrimEnd('0').TrimEnd('.');
            }


            DataRow row = this.dtDetail.NewRow();

            row["自定义码"] = output.Item.UserCode;
            row["药品编码"] = output.Item.ID;
            row["批号"] = output.BatchNO;
            row["名称"] = output.Item.Name;
            row["规格"] = output.Item.Specs;

            if (output.ShowState == "1")
            {
                row["出库量"] = output.Quantity / output.Item.PackQty;
                row["单位"] = output.Item.PackUnit;
            }
            else
            {
                row["出库量"] = output.Quantity;
                row["单位"] = output.Item.MinUnit;
            }
            row["购入价"] = output.Item.PriceCollection.PurchasePrice;
            row["购入金额"] = output.PurchaseCost;
            row["零售价"] = output.Item.PriceCollection.RetailPrice;
            row["零售金额"] = output.RetailCost;

            row["拼音码"] = output.Item.SpellCode;
            row["五笔码"] = output.Item.WBCode;
            row["主键"] = output.Item.ID + output.ID + output.SerialNO.ToString();

            this.dtDetail.Rows.Add(row);

            return 1;
        }

        #endregion

        #region 出库单按钮调用
        /// <summary>
        /// 选择出库单
        /// </summary>
        public int ChooseOutputBill()
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
                if (this.itemMgr.Sql.GetSql("SOC.Pharmacy.Fin.ApproveOutput.ChooseBill", ref SQL) == -1)
                {
                    Function.ShowMessage("没有找到SQL：SOC.Pharmacy.Fin.ApproveOutput.ChooseBill,请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                string targetDeptNO = "All";
                if (this.curTargetDept != null && this.curTargetDept.ID != "")
                {
                    targetDeptNO = this.curTargetDept.ID;
                }
                //替换库存科室和目标单位，时间不替换
                SQL = string.Format(SQL, this.curStockDept.ID, targetDeptNO, "{0}", "{1}");

                chooseDataSetting.ListTile = "出库单列表";
                chooseDataSetting.SQL = SQL;
                chooseDataSetting.ColumnIndexs = new int[] { 0,1};
                curBillChooseColumnIndexs = chooseDataSetting.ColumnIndexs;
                chooseDataSetting.Filter = "";

                chooseDataSetting.ColumnLabels = new string[] 
                { 
                    "出库单号", 
                    "领料科室", 
                    "科室名称", 
                    //"状态", 
                    "出库时间", 
                    "出库人工号"
                };
                chooseDataSetting.ColumnWiths = new float[]
                {
                   100f,// "出库单号", 
                   60f,// "领料科室", 
                   100f,// "科室名称", 
                   //40f,// "状态", 
                   120f,// "出库时间", 
                   90f,// "出库人工号"
                };
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                chooseDataSetting.CellTypes = new FarPoint.Win.Spread.CellType.BaseCellType[] 
                {
                   t,// "出库单号", 
                   t,// "领料科室", 
                   t,// "科室名称", 
                   //t,// "状态", 
                   t,// "出库时间",
                   t,// "出库人工号"
                };

                chooseDataSetting.SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ExamOutputL1Setting.xml";
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


        #region 刷新数据选择列表
        protected int FreshDataChooseList()
        {
            if (curChooseDataSetting == null)
            {
                return 0;
            }
            string targetDeptNO = "All";
            if (this.curTargetDept != null)
            {
                targetDeptNO = this.curTargetDept.ID;
            }
            this.curIDataChooseList.ShowChooseList(curChooseDataSetting.ListTile, string.Format(curChooseDataSetting.SQL, this.curStockDept.ID, targetDeptNO), curChooseDataSetting.IsNeedDrugType, curChooseDataSetting.Filter);
            this.curIDataChooseList.SetFormat(curChooseDataSetting.CellTypes, curChooseDataSetting.ColumnLabels, curChooseDataSetting.ColumnWiths);
            this.curIDataChooseList.ChooseCompletedEvent -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);
            this.curIDataChooseList.ChooseCompletedEvent += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander(IDataChooseList_ChooseCompletedEvent);

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
            return 1;
        }

        protected int Save()
        {
            if (this.dtDetail.Rows.Count == 0)
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


            FS.SOC.HISFC.BizLogic.Pharmacy.Financial financialMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Financial();

            //记录单据数据
            System.Collections.Hashtable hsBill = new System.Collections.Hashtable();
            string billNO = "";

            foreach (DataRow dr in dtAddMofity.Rows)
            {
                string key = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.Output output = this.hsOutput[key] as FS.HISFC.Models.Pharmacy.Output;

                int parm = financialMgr.FinApproveOutput(output);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("数据可能已被审核，请刷新重试", MessageBoxIcon.Error);
                    return -1;
                }

                alOutput.Add(output);

                if (!hsBill.Contains(output.OutListNO))
                {
                    hsBill.Add(output.OutListNO, output);
                }
            }

            foreach (FS.HISFC.Models.Pharmacy.Output info in hsBill.Values)
            {
                billNO = ucFinBill.GetFinBillNO(this.curStockDept.ID, "0320", this.PriveType.ID);
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


            if (SOC.HISFC.Components.Pharmacy.Function.DealExtendBiz("0320", this.curPriveType.ID, alOutput, ref errInfo) == -1)
            {
                Function.ShowMessage("出库已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            SOC.HISFC.Components.Pharmacy.Function.PrintBill("0320", this.PriveType.ID, alOutput);

            this.ClearData();
            this.FreshDataChooseList();

            return 1;
        }
        #endregion

        #endregion

        #region 清空
        protected int ClearData()
        {
            totPurchaseCost = 0;
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
            this.ClearData();

            string[] values = this.curIDataChooseList.GetChooseData(this.curChooseDataSetting.ColumnIndexs);
            if (values == null || values.Length == 0)
            {
                Function.ShowMessage("药品选择列表没有返回选择数据，请与系统管理员联系!", MessageBoxIcon.Error);
            }
            else
            {
                ArrayList alOutput = this.itemMgr.QueryOutputInfo(this.curStockDept.ID, values[0], "A");
                if (alOutput == null)
                {
                    Function.ShowMessage("查询出库数据发生错误：" + itemMgr.Err + "\n请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }

                foreach (FS.HISFC.Models.Pharmacy.Output output in alOutput)
                {

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
                        output.Item.SpellCode = item.SpellCode;
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

                ArrayList alOutput = this.itemMgr.QueryOutputInfo(this.curStockDept.ID, outBillNO, "A");
                if (alOutput == null)
                {
                    Function.ShowMessage("查询出库数据发生错误：" + itemMgr.Err + "\n请与系统管理员联系!", MessageBoxIcon.Error);
                    return;
                }

                foreach (FS.HISFC.Models.Pharmacy.Output output in alOutput)
                {

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
                        output.Item.SpellCode = item.SpellCode;
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
    }
}
