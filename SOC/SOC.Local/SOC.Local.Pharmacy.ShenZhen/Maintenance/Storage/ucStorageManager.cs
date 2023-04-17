using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.Components.Pharmacy;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Maintenance.Storage
{
    public partial class ucStorageManager : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucStorageManager()
        {
            InitializeComponent();
        }

        #region 变量属性
        /// <summary>
        /// 权限编码
        /// </summary>
        private string privePowerString = "0302+01";

        /// <summary>
        /// 权限编码
        /// </summary>
        [Description("权限编码"), Category("设置"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }

        private bool isCheckPrivePower = true;

        /// <summary>
        /// 是否检测权限
        /// </summary>
        [Description("是否检测权限"), Category("设置"), Browsable(true)]
        public bool IsCheckPrivePower
        {
            get { return isCheckPrivePower; }
            set { isCheckPrivePower = value; }
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        private string settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPStockInfoManagerSetting.xml";

        /// <summary>
        /// 汇总信息FarPoint配置文件
        /// </summary>
        [Description("汇总信息FarPoint配置文件"), Category("设置"), Browsable(true)]
        public string SettingFile
        {
            get { return settingFile; }
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        private string storageSettingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPStorageManagerSetting.xml";

        /// <summary>
        /// 明细信息FarPoint配置文件
        /// </summary>
        [Description("明细信息FarPoint配置文件"), Category("设置"), Browsable(true)]
        public string StorageSettingFile
        {
            get { return storageSettingFile; }
        }

        private int days = 90;

        [Description("默认90天内没有出入账的药品不打印盘点表"), Category("设置"), Browsable(true)]
        public int Days
        {
            get { return this.days; }
            set { this.days = value; }
        }

        /// <summary>
        /// 权限科室 库存科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 所有有效期达警戒日期的库存明细
        /// </summary>
        private ArrayList alValidDateWarnStorage = null;

        /// <summary>
        /// 获取所有有效期达警戒日期的库存明细的时间
        /// </summary>
        private DateTime getValidDateWarnStorageTime = DateTime.Now.Date;

        /// <summary>
        /// 保留有效期警戒天数
        /// </summary>
        private int lastValidDateDays = 0;

        /// <summary>
        /// 库存汇总信息的数据集
        /// </summary>
        private System.Data.DataTable dtStockInfo = null;

        /// <summary>
        /// 库存汇总信息的数据视图
        /// </summary>
        private DataView dvStockInfo = null;

        /// <summary>
        /// 库存明细信息的数据集
        /// </summary>
        private System.Data.DataTable dtStorage = null;

        /// <summary>
        /// 库存明细信息的数据视图
        /// </summary>
        private DataView dvStorage = null;

        /// <summary>
        /// 药品类别帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }

            return this.SetPriveDept();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            if (this.IsCheckPrivePower)
            {
                if (string.IsNullOrEmpty(PrivePowerString))
                {
                    PrivePowerString = "0302+01";
                }
                if (PrivePowerString.Split('+').Length < 2)
                {
                    PrivePowerString = PrivePowerString + "+01";
                }
                string[] prives = PrivePowerString.Split('+');
                int param = FS.SOC.HISFC.Components.Pharmacy.Function.ChoosePrivDept(prives[0], prives[1], ref this.priveDept);
                if (param == 0 || param == -1)
                {
                    return -1;
                }
            }
            else
            {
                this.priveDept = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept;
            }

            this.ShowStatusBarTip("库存科室：" + priveDept.Name);
            this.nlbPriveDept.Text = "您选择的科室是【" + this.priveDept.Name + "】";

            return 1;

        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alItemType = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            if (alItemType == null)
            {
                this.ShowMessage("获取药品类别常数失败：" + constantMgr.Err, MessageBoxIcon.Error);
                alItemType = new ArrayList();
            }
            itemTypeHelper.ArrayObject = alItemType;
            this.ncmbDrugType.AddItems(alItemType);

            SOC.HISFC.BizProcess.Cache.Common.GetDrugQualityName("01");
            this.ncmbDrugQuality.AddItems(SOC.HISFC.BizProcess.Cache.Common.drugQualityHelper.ArrayObject);

            SOC.HISFC.BizProcess.Cache.Common.GetDrugDoseModualName("01");
            this.ncmbDoseMode.AddItems(SOC.HISFC.BizProcess.Cache.Common.drugDoseModualHelper.ArrayObject);

        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void SetFormat()
        {
            if (dtStockInfo == null)
            {
                dtStockInfo = new DataTable();
                this.SetStockInfoDataTable();
            }
            if (this.dtStorage == null)
            {
                dtStorage = new DataTable();
                this.SetStorageDataTable();
            }

            this.neuStockInfoSpread_Sheet1.ColumnHeader.Rows[0].Height = 34F;
            this.neuStockInfoSpread_Sheet1.ColumnHeader.Rows.Default.Font = new Font("宋体", 9.5f, FontStyle.Bold);

            if (System.IO.File.Exists(this.settingFile))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuStockInfoSpread_Sheet1, this.settingFile);
            }
            for (int columnIndex = 0; columnIndex < dtStockInfo.Columns.Count; columnIndex++)
            {
                this.dtStockInfo.Columns[columnIndex].ReadOnly = true;
            }

            //如果没有数据，则返回，后续设置不处理
            if (this.neuStockInfoSpread_Sheet1.RowCount <= 0)
            {
                return;
            }

            bool isHavePrive = FS.SOC.HISFC.Components.Pharmacy.Function.JugePrive(this.priveDept.ID, "0302", "02");
            this.dtStockInfo.Columns["停用"].ReadOnly = !isHavePrive;
            this.dtStockInfo.Columns["门诊用药"].ReadOnly = !isHavePrive;
            this.dtStockInfo.Columns["住院用药"].ReadOnly = !isHavePrive;
            this.dtStockInfo.Columns["基数药"].ReadOnly = !isHavePrive;
            this.dtStockInfo.Columns["最高库存量"].ReadOnly = !isHavePrive;
            this.dtStockInfo.Columns["最低库存量"].ReadOnly = !isHavePrive;
            this.dtStockInfo.Columns["货位号"].ReadOnly = !isHavePrive;
            this.dtStockInfo.Columns["日盘点"].ReadOnly = !isHavePrive;
            this.dtStockInfo.Columns["库存性质"].ReadOnly = !isHavePrive;

            this.dtStockInfo.Columns["门诊缺药"].ReadOnly = false;
            this.dtStockInfo.Columns["住院缺药"].ReadOnly = false;


            bool isManger = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager;
            this.dtStockInfo.Columns["库房编码"].ReadOnly = isManger;
            this.dtStockInfo.Columns["药品编码"].ReadOnly = isManger;
            this.dtStockInfo.Columns["自定义码"].ReadOnly = isManger;

            //这个必须是查询到数据后才可以用，要不SOC.HISFC.BizProcess.Cache.Common.drugQualityHelper.ArrayObject是没有赋值的
            FarPoint.Win.Spread.CellType.ComboBoxCellType cmbCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            string[] items = new string[SOC.HISFC.BizProcess.Cache.Common.drugQualityHelper.ArrayObject.Count];
            int index = 0;
            foreach (FS.HISFC.Models.Base.Const con in SOC.HISFC.BizProcess.Cache.Common.drugQualityHelper.ArrayObject)
            {
                items[index] = con.Name;
                index++;
            }
            cmbCellType.Items = items;
            int column = this.GetColumnIndex(0, "库存性质");
            if (column > -1)
            {
                this.neuStockInfoSpread_Sheet1.Columns[column].CellType = cmbCellType;
            }

            #region 颜色显示
            DateTime sysDate = (new FS.SOC.HISFC.BizLogic.Pharmacy.Storage()).GetDateTimeFromSysDateTime().Date;
            if (this.neuStockInfoSpread_Sheet1.Rows.Count >= 1)
            {
                for (int i = 0; i < this.neuStockInfoSpread_Sheet1.Rows.Count; i++)
                {
                    if (this.neuStockInfoSpread_Sheet1.Cells[i, this.GetColumnIndex("停用")].Text.ToUpper() == "TRUE")
                    {
                        this.neuStockInfoSpread_Sheet1.SetRowLabel(i, 0, "停");
                        this.neuStockInfoSpread_Sheet1.RowHeader.Cells[i, 0].BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        this.neuStockInfoSpread_Sheet1.SetRowLabel(i, 0, null);
                        this.neuStockInfoSpread_Sheet1.RowHeader.Cells[i, 0].BackColor = System.Drawing.Color.Empty;
                    }
                    //if (this.neuStockInfoSpread_Sheet1.Cells[i, this.GetColumnIndex("缺药")].Text.ToUpper() == "TRUE")
                    //{
                    //    this.neuStockInfoSpread_Sheet1.SetRowLabel(i, 0, "缺");
                    //    this.neuStockInfoSpread_Sheet1.RowHeader.Cells[i, 0].BackColor = System.Drawing.Color.Crimson;
                    //}
                    DateTime tempDate = NConvert.ToDateTime(this.neuStockInfoSpread_Sheet1.Cells[i, this.GetColumnIndex("有效期")].Text);
                    if (this.neuStockInfoSpread_Sheet1.Cells[i, this.GetColumnIndex("库存数量")].Text == "0.00")
                    {
                        this.neuStockInfoSpread_Sheet1.Rows[i].BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (tempDate < sysDate.AddDays(this.days))
                    {
                        this.neuStockInfoSpread_Sheet1.Rows[i].BackColor = System.Drawing.Color.Moccasin;
                    }
                    else
                    {
                        this.neuStockInfoSpread_Sheet1.Rows[i].BackColor = System.Drawing.Color.Empty;
                    }
                    
                }
            }
            #endregion

            this.neuStockInfoSpread_Sheet2.ColumnHeader.Rows[0].Height = 34F;
            this.neuStockInfoSpread_Sheet2.ColumnHeader.Rows.Default.Font = new Font("宋体", 9.5f, FontStyle.Bold);

            if (System.IO.File.Exists(this.storageSettingFile))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuStockInfoSpread_Sheet2, this.storageSettingFile);
            }
            for (int columnIndex = 0; columnIndex < dtStorage.Columns.Count; columnIndex++)
            {
                this.dtStorage.Columns[columnIndex].ReadOnly = true;
            }

            //如果没有数据，则返回，后续设置不处理
            if (this.neuStockInfoSpread_Sheet2.RowCount <= 0)
            {
                return;
            }

            this.dtStorage.Columns["停用"].ReadOnly = !isHavePrive;

            this.dtStorage.Columns["库房编码"].ReadOnly = isManger;
            this.dtStorage.Columns["药品编码"].ReadOnly = isManger;
            this.dtStorage.Columns["自定义码"].ReadOnly = isManger;
            this.dtStorage.Columns["批次号"].ReadOnly = isManger;
        }

        /// <summary>
        /// 按照默认设置单据格式
        /// </summary>
        private void SetStockInfoDataTable()
        {
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDTime = System.Type.GetType("System.DateTime");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //在myDataTable中添加列
            this.dtStockInfo.Columns.AddRange(new DataColumn[] {
														new DataColumn("库房编码",    dtStr),
														new DataColumn("药品编码",    dtStr),
														new DataColumn("自定义码",    dtStr),
                                                        new DataColumn("国家编码",    dtStr),
														new DataColumn("名称",    dtStr),
														new DataColumn("通用名",      dtStr),																	
														new DataColumn("规格",        dtStr),
                                                        new DataColumn("剂型",        dtStr),
														new DataColumn("零售价",      dtDec),
                                                        new DataColumn("购入价",      dtDec),
                                                        new DataColumn("库存量",      dtStr),
														new DataColumn("库存数量",    dtDec),
														new DataColumn("最小单位",    dtStr),
														new DataColumn("包装数量",    dtStr),
														new DataColumn("包装单位",    dtStr),
														new DataColumn("货位号",      dtStr),
				                                        new DataColumn("全院停用",dtBool),
														new DataColumn("停用",        dtBool),
				                                        new DataColumn("门诊用药",dtBool),
				                                        new DataColumn("住院用药",dtBool),
				                                        new DataColumn("门诊缺药",dtBool),
				                                        new DataColumn("住院缺药",dtBool),
														new DataColumn("日盘点",      dtBool),
														new DataColumn("基数药",      dtBool),
														new DataColumn("最低库存量",  dtDec),
														new DataColumn("最高库存量",  dtDec),
														new DataColumn("有效期",      dtDTime),                                                        
														new DataColumn("预扣数量",    dtDec),
														new DataColumn("预扣金额",    dtDec),
                                                        new DataColumn("库存性质",    dtStr),
														new DataColumn("库存金额",    dtDec),
														new DataColumn("药品类别",    dtStr),
														new DataColumn("药品性质",    dtStr),
														new DataColumn("备注",        dtStr),
														new DataColumn("操作人",      dtStr),
														new DataColumn("操作日期",    dtDTime),
														new DataColumn("拼音码",      dtStr),
														new DataColumn("五笔码",      dtStr),																			
														new DataColumn("通用名拼音码",dtStr),
														new DataColumn("通用名五笔码",dtStr),
														new DataColumn("通用名自定义码",dtStr),
                                                        new DataColumn("学名",dtStr),
                                                        new DataColumn("别名",dtStr),
                                                        new DataColumn("学名拼音码",dtStr),
                                                        new DataColumn("别名拼音码",dtStr)
                    								});

            this.neuStockInfoSpread_Sheet1.DataSource = this.dtStockInfo;

        }

        /// <summary>
        /// 按照默认设置单据格式
        /// </summary>
        private void SetStorageDataTable()
        {
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDTime = System.Type.GetType("System.DateTime");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //在myDataTable中添加列
            this.dtStorage.Columns.AddRange(new DataColumn[] {
														new DataColumn("库房编码",    dtStr),
														new DataColumn("药品编码",    dtStr),
														new DataColumn("自定义码",    dtStr),
                                                        new DataColumn("国家编码",    dtStr),
														new DataColumn("名称",    dtStr),
														new DataColumn("通用名",      dtStr),																	
														new DataColumn("规格",        dtStr),
														new DataColumn("批次号",        dtStr),
														new DataColumn("生产批号",        dtStr),
														new DataColumn("有效期",      dtDTime),                                                        
                                                        //new DataColumn("剂型",        dtStr),
														new DataColumn("零售价",      dtDec),
                                                        new DataColumn("购入价",      dtDec),
                                                        new DataColumn("批发价",      dtDec),
                                                        new DataColumn("库存量",      dtStr),
														new DataColumn("库存数量",    dtDec),
														new DataColumn("最小单位",    dtStr),
														new DataColumn("包装数量",    dtStr),
														new DataColumn("包装单位",    dtStr),
														new DataColumn("零售金额",      dtDec),
														new DataColumn("购入金额",      dtDec),
														new DataColumn("批发金额",      dtDec),
														new DataColumn("货位号",      dtStr),
				                                        new DataColumn("全院停用",dtBool),
				                                        new DataColumn("本科停用",dtBool),
														new DataColumn("停用",        dtBool),
														new DataColumn("预扣数量",    dtDec),
														new DataColumn("预扣金额",    dtDec),
														new DataColumn("药品类别",    dtStr),
														new DataColumn("药品性质",    dtStr),
														new DataColumn("备注",        dtStr),
														new DataColumn("操作人",      dtStr),
														new DataColumn("操作日期",    dtDTime),
														new DataColumn("拼音码",      dtStr),
														new DataColumn("五笔码",      dtStr),																			
														new DataColumn("通用名拼音码",dtStr),
														new DataColumn("通用名五笔码",dtStr),
														new DataColumn("通用名自定义码",dtStr),
                                                        new DataColumn("学名",dtStr),
                                                        new DataColumn("别名",dtStr),
                                                        new DataColumn("学名拼音码",dtStr),
                                                        new DataColumn("别名拼音码",dtStr)
                    								});

            this.neuStockInfoSpread_Sheet2.DataSource = this.dtStorage;

        }

        /// <summary>
        /// 根据库存信息 设置DataRow
        /// </summary>
        /// <param name="storage">库存信息</param>
        /// <returns>成功返回数据行信息</returns>
        private DataRow ConvertToStockInfoDataRow(FS.HISFC.Models.Pharmacy.Storage storage)
        {
            DataRow row = this.dtStockInfo.NewRow();
            try
            {
                row["库房编码"] = storage.StockDept.ID;
                row["药品编码"] = storage.Item.ID;
                row["自定义码"] = storage.Item.UserCode;
                row["国家编码"] = storage.Item.GBCode;
                row["名称"] = storage.Item.Name;
                row["规格"] = storage.Item.Specs;
                row["零售价"] = storage.Item.PriceCollection.RetailPrice;
                row["库存数量"] = storage.StoreQty;

                decimal packQty = Math.Floor(storage.StoreQty / storage.Item.PackQty);
                decimal minQty = storage.StoreQty - packQty * storage.Item.PackQty;
                if (packQty == 0)
                {
                    row["库存量"] = string.Format("{0}{1}", minQty, storage.Item.MinUnit);
                }
                else if (minQty == 0)
                {
                    row["库存量"] = string.Format("{0}{1}", packQty, storage.Item.PackUnit);
                }
                else
                {
                    row["库存量"] = string.Format("{0}{1} {2}{3}", packQty, storage.Item.PackUnit, minQty, storage.Item.MinUnit);
                }

                row["最小单位"] = storage.Item.MinUnit;
                row["包装数量"] = storage.Item.PackQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                row["包装单位"] = storage.Item.PackUnit;
                row["通用名"] = storage.Item.NameCollection.RegularName;
                row["货位号"] = storage.PlaceNO;
                row["停用"] = storage.IsStop;
                row["最低库存量"] = storage.LowQty;
                row["最高库存量"] = storage.TopQty;
                row["日盘点"] = storage.IsCheck;
                row["有效期"] = storage.ValidTime;
                row["库存金额"] = SOC.Public.Math.Round(storage.StoreQty / storage.Item.PackQty * storage.Item.PriceCollection.RetailPrice, 2);
                row["预扣数量"] = storage.PreOutQty;
                row["预扣金额"] = SOC.Public.Math.Round(storage.PreOutQty / storage.Item.PackQty * storage.Item.PriceCollection.RetailPrice, 2);
                row["药品类别"] = this.itemTypeHelper.GetName(storage.Item.Type.ID);
                row["药品性质"] = SOC.HISFC.BizProcess.Cache.Common.GetDrugQualityName(storage.Item.Quality.ID);
                row["备注"] = storage.Memo;
                row["操作人"] = storage.Operation.Oper.ID;
                row["操作日期"] = storage.Operation.Oper.OperTime;
                row["拼音码"] = storage.Item.NameCollection.SpellCode;
                row["五笔码"] = storage.Item.NameCollection.WBCode;
                row["通用名拼音码"] = storage.Item.NameCollection.RegularSpell.SpellCode;
                row["通用名五笔码"] = storage.Item.NameCollection.RegularSpell.WBCode;
                row["通用名自定义码"] = storage.Item.NameCollection.RegularSpell.UserCode;

                row["全院停用"] = storage.Item.IsStop;
                row["门诊用药"] = storage.IsUseForOutpatient;
                row["住院用药"] = storage.IsUseForInpatient;
                row["门诊缺药"] = storage.IsLack;
                row["住院缺药"] = storage.IsLackForInpatient;
                row["基数药"] = storage.IsRadix;

                row["库存性质"] = SOC.HISFC.BizProcess.Cache.Common.GetDrugQualityName(storage.ManageQuality.ID);

                row["学名"] = storage.Item.NameCollection.FormalName;
                row["学名拼音码"] = storage.Item.NameCollection.FormalSpell.SpellCode;
                row["别名"] = storage.Item.NameCollection.OtherName;
                row["别名拼音码"] = storage.Item.NameCollection.OtherSpell.SpellCode;

                row["剂型"] = SOC.HISFC.BizProcess.Cache.Common.GetDrugDoseModualName(storage.Item.DosageForm.ID);

                row["购入价"] = storage.Item.PriceCollection.PurchasePrice;
            }
            catch (Exception ex)
            {
                this.ShowMessage("根据库存信息对数据行进行赋值时发生错误!", MessageBoxIcon.Error);
            }

            return row;
        }

        /// <summary>
        /// 根据行信息 返回库存信息
        /// </summary>
        /// <param name="row">DataRow信息</param>
        /// <returns>成功返回库存信息</returns>
        private FS.HISFC.Models.Pharmacy.Storage ConvertStockInfoDataRowToStorage(DataRow row)
        {
            FS.HISFC.Models.Pharmacy.Storage storage = new FS.HISFC.Models.Pharmacy.Storage();
            //这里的信息保存到数据库，注意不要取太多信息
            try
            {
                storage.StockDept.ID = this.priveDept.ID;

                storage.Item.ID = row["药品编码"].ToString();
                storage.PlaceNO = row["货位号"].ToString();

                storage.IsLack = FS.FrameWork.Function.NConvert.ToBoolean(row["门诊缺药"]);
                storage.IsLackForInpatient = FS.FrameWork.Function.NConvert.ToBoolean(row["住院缺药"]);

                if (FS.FrameWork.Function.NConvert.ToBoolean(row["停用"]))
                {
                    storage.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
                }
                else
                {
                    storage.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                }
                storage.LowQty = FS.FrameWork.Function.NConvert.ToDecimal(row["最低库存量"]);
                storage.TopQty = FS.FrameWork.Function.NConvert.ToDecimal(row["最高库存量"]);
                storage.IsCheck = FS.FrameWork.Function.NConvert.ToBoolean(row["日盘点"]);
                storage.IsUseForInpatient = FS.FrameWork.Function.NConvert.ToBoolean(row["住院用药"]);
                storage.IsUseForOutpatient = FS.FrameWork.Function.NConvert.ToBoolean(row["门诊用药"]);
                storage.IsRadix = FS.FrameWork.Function.NConvert.ToBoolean(row["基数药"]);
                storage.ManageQuality.ID = SOC.HISFC.BizProcess.Cache.Common.drugQualityHelper.GetID(row["库存性质"].ToString());
                storage.Memo = row["备注"].ToString();
            }
            catch (Exception ex)
            {
                this.ShowMessage("根据库存信息对数据行进行赋值时发生错误：" + ex.Message, MessageBoxIcon.Error);
                return null;
            }

            return storage;
        }

        /// <summary>
        /// 根据库存信息 设置DataRow
        /// </summary>
        /// <param name="storage">库存信息</param>
        /// <param name="curGlobalValid">全院有效性，药品基本信息中的停用</param>
        /// <param name="curDeptValid">本科有效性，库存汇总信息中的停用</param>
        /// <returns>成功返回数据行信息</returns>
        private DataRow ConvertToStorageDataRow(FS.HISFC.Models.Pharmacy.Storage storage, bool curGlobalValid, bool curDeptValid)
        {
            DataRow row = this.dtStorage.NewRow();
            try
            {
                row["库房编码"] = storage.StockDept.ID;
                row["药品编码"] = storage.Item.ID;
                row["自定义码"] = storage.Item.UserCode;
                row["国家编码"] = storage.Item.GBCode;
                row["名称"] = storage.Item.Name;
                row["规格"] = storage.Item.Specs;
                row["批次号"] = storage.GroupNO.ToString();
                row["生产批号"] = storage.BatchNO;
                row["规格"] = storage.Item.Specs;
                row["零售价"] = storage.Item.PriceCollection.RetailPrice;
                row["购入价"] = storage.Item.PriceCollection.PurchasePrice;
                row["批发价"] = storage.Item.PriceCollection.WholeSalePrice;
                row["库存数量"] = storage.StoreQty;

                decimal packQty = Math.Floor(storage.StoreQty / storage.Item.PackQty);
                decimal minQty = storage.StoreQty - packQty * storage.Item.PackQty;
                if (packQty == 0)
                {
                    row["库存量"] = string.Format("{0}{1}", minQty, storage.Item.MinUnit);
                }
                else if (minQty == 0)
                {
                    row["库存量"] = string.Format("{0}{1}", packQty, storage.Item.PackUnit);
                }
                else
                {
                    row["库存量"] = string.Format("{0}{1} {2}{3}", packQty, storage.Item.PackUnit, minQty, storage.Item.MinUnit);
                }

                row["最小单位"] = storage.Item.MinUnit;
                row["包装数量"] = storage.Item.PackQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                row["包装单位"] = storage.Item.PackUnit;
                row["通用名"] = storage.Item.NameCollection.RegularName;

                row["货位号"] = storage.PlaceNO;
                row["全院停用"] = !curGlobalValid;
                row["本科停用"] = !curDeptValid;
                row["停用"] = (storage.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid);
                row["有效期"] = storage.ValidTime;
                row["零售金额"] = SOC.Public.Math.Round(storage.StoreQty / storage.Item.PackQty * storage.Item.PriceCollection.RetailPrice, 2);
                row["购入金额"] = SOC.Public.Math.Round(storage.StoreQty / storage.Item.PackQty * storage.Item.PriceCollection.PurchasePrice, 2);
                row["批发金额"] = SOC.Public.Math.Round(storage.StoreQty / storage.Item.PackQty * storage.Item.PriceCollection.WholeSalePrice, 2);
                row["预扣数量"] = storage.PreOutQty;
                row["预扣金额"] = SOC.Public.Math.Round(storage.PreOutQty / storage.Item.PackQty * storage.Item.PriceCollection.RetailPrice, 2);
                row["药品类别"] = this.itemTypeHelper.GetName(storage.Item.Type.ID);
                row["药品性质"] = SOC.HISFC.BizProcess.Cache.Common.GetDrugQualityName(storage.Item.Quality.ID);
                row["备注"] = storage.Memo;
                row["操作人"] = storage.Operation.Oper.ID;
                row["操作日期"] = storage.Operation.Oper.OperTime;
                row["拼音码"] = storage.Item.NameCollection.SpellCode;
                row["五笔码"] = storage.Item.NameCollection.WBCode;
                row["通用名拼音码"] = storage.Item.NameCollection.RegularSpell.SpellCode;
                row["通用名五笔码"] = storage.Item.NameCollection.RegularSpell.WBCode;
                row["通用名自定义码"] = storage.Item.NameCollection.RegularSpell.UserCode;


                //row["库存性质"] = SOC.HISFC.BizProcess.Cache.Common.GetDrugQualityName(storage.ManageQuality.ID);

                row["学名"] = storage.Item.NameCollection.FormalName;
                row["学名拼音码"] = storage.Item.NameCollection.FormalSpell.SpellCode;
                row["别名"] = storage.Item.NameCollection.OtherName;
                row["别名拼音码"] = storage.Item.NameCollection.OtherSpell.SpellCode;

                //row["剂型"] = SOC.HISFC.BizProcess.Cache.Common.GetDrugDoseModualName(storage.Item.DosageForm.ID);

            }
            catch (Exception ex)
            {
                this.ShowMessage("根据库存信息对数据行进行赋值时发生错误!", MessageBoxIcon.Error);
            }

            return row;
        }

        /// <summary>
        /// 根据行信息 返回库存信息
        /// </summary>
        /// <param name="row">DataRow信息</param>
        /// <returns>成功返回库存信息</returns>
        private FS.HISFC.Models.Pharmacy.Storage ConvertStorageDataRowToStorage(DataRow row)
        {
            FS.HISFC.Models.Pharmacy.Storage storage = new FS.HISFC.Models.Pharmacy.Storage();
            //这里的信息保存到数据库，注意不要取太多信息
            try
            {
                storage.StockDept.ID = this.priveDept.ID;

                storage.Item.ID = row["药品编码"].ToString();
                storage.PlaceNO = row["货位号"].ToString();
                storage.GroupNO = FS.FrameWork.Function.NConvert.ToDecimal(row["批次号"]);

                if (FS.FrameWork.Function.NConvert.ToBoolean(row["停用"]))
                {
                    storage.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
                }
                else
                {
                    storage.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage("根据库存信息对数据行进行赋值时发生错误：" + ex.Message, MessageBoxIcon.Error);
                return null;
            }

            return storage;
        }

        /// <summary>
        /// 根据列名称获取列索引
        /// </summary>
        /// <param name="colName">列名称</param>
        /// <returns>成功返回列索引 失败返回-1</returns>
        private int GetColumnIndex(int sheetIndex, string colName)
        {
            if (sheetIndex >= this.neuStockInfoSpread.Sheets.Count || sheetIndex < 0)
            {
                throw new Exception("无效的SheetIndex,索引必须介于-1和" + this.neuStockInfoSpread.Sheets.Count.ToString() + "之间");
            }
            for (int columnIndex = 0; columnIndex < this.neuStockInfoSpread.Sheets[sheetIndex].Columns.Count; columnIndex++)
            {
                if (this.neuStockInfoSpread.Sheets[sheetIndex].Columns[columnIndex].Label == colName)
                {
                    return columnIndex;
                }
            }
            return -1;
        }


        /// <summary>
        /// 根据科室编码检索库存信息并向DataTable内设置数据
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="isReSetDataTable">是否重置DataTable</param>
        private void ShowStockInfoData()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在检索显示库存信息...请稍候");
            Application.DoEvents();

            FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
            ArrayList alStockInfoData = storageMgr.QueryStockinfoList(this.priveDept.ID);
            if (alStockInfoData == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                this.ShowMessage("获取科室库存信息发生错误：" + storageMgr.Err, MessageBoxIcon.Error);
                return;
            }
            ArrayList alStorageData = storageMgr.QueryStorageList(this.priveDept.ID);
            if (alStorageData == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                this.ShowMessage("获取科室库存信息发生错误：" + storageMgr.Err, MessageBoxIcon.Error);
                return;
            }
            this.neuStockInfoSpread_Sheet1.DataSource = null;
            this.dtStockInfo.Clear();

            this.neuStockInfoSpread_Sheet2.DataSource = null;
            this.dtStorage.Clear();


            //计算库存总金额
            decimal totRetailCost = 0;
            decimal totPurchaseCost = 0;
            decimal totWholeSaleCost = 0;

            DateTime validDate = DateTime.MaxValue;
            if (this.ncbMutiQuery.Checked)
            {
                if (ncbValidDate.Checked && (int)this.nnudValidDays.Value != lastValidDateDays)
                {
                    validDate = storageMgr.GetDateTimeFromSysDateTime().AddDays((int)this.nnudValidDays.Value);
                }
            }


            foreach (FS.HISFC.Models.Pharmacy.Storage stockInfo in alStockInfoData)
            {
                #region 简单查询设置时显示库存信息处理
                //停用选择后没有停用的不显示
                if (this.ncbStop.Checked && stockInfo.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid && stockInfo.Item.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    continue;
                }

                //门诊缺药
                if (this.ncbOutpatientLack.Checked && !stockInfo.IsLack)
                {
                    continue;
                }
                //住院缺药
                if (this.ncbInpatientLack.Checked && !stockInfo.IsLackForInpatient)
                {
                    continue;
                }

                //门诊使用
                if (this.ncbUseForOutpatient.Checked && !stockInfo.IsUseForOutpatient)
                {
                    continue;
                }
                //住院使用
                if (this.ncbUseForInpatient.Checked && !stockInfo.IsUseForInpatient)
                {
                    continue;
                }
                //基数药
                if (this.ncbRadix.Checked && !stockInfo.IsRadix)
                {
                    continue;
                }

                //日盘点
                if (this.ncbDailtyCheck.Checked && !stockInfo.IsCheck)
                {
                    continue;
                }

                #endregion

                #region 复杂查询设置时显示库存信息处理
                if (this.ncbMutiQuery.Checked)
                {
                    if (!string.IsNullOrEmpty(ncmbDrugType.Text) && this.ncmbDrugType.Tag != null && stockInfo.Item.Type.ID != this.ncmbDrugType.Tag.ToString())
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(ncmbDrugQuality.Text) && this.ncmbDrugQuality.Tag != null && stockInfo.Item.Quality.ID != this.ncmbDrugQuality.Tag.ToString())
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(ncmbDoseMode.Text) && this.ncmbDoseMode.Tag != null && stockInfo.Item.DosageForm.ID != this.ncmbDoseMode.Tag.ToString())
                    {
                        continue;
                    }

                    #region 库存量
                    if (!string.IsNullOrEmpty(ncmbStockNumCond.Text) && !string.IsNullOrEmpty(ncmbStoraeQty.Text))
                    {
                        decimal stockQty = stockInfo.StoreQty;
                        decimal stockCondQty = 0;
                        if (ncmbStoraeQty.Text == "最高库存")
                        {
                            stockCondQty = stockInfo.TopQty;
                        }
                        else if (ncmbStoraeQty.Text == "最低库存")
                        {
                            stockCondQty = stockInfo.LowQty;
                        }
                        else
                        {
                            try
                            {
                                FS.FrameWork.Function.NConvert.ToDecimal(this.ncmbStoraeQty.Text);
                            }
                            catch { continue; }
                            stockCondQty = FS.FrameWork.Function.NConvert.ToDecimal(this.ncmbStoraeQty.Text);
                        }
                        if (stockInfo.Item.PackQty == 0)
                        {
                            stockInfo.Item.PackQty = 1;
                        }
                        if (this.nrbPackUnit.Checked)
                        {
                            stockQty = stockQty / stockInfo.Item.PackQty;
                        }

                        if (ncmbStockNumCond.Text == ">" && stockQty <= stockCondQty)
                        {
                            continue;
                        }
                        if (ncmbStockNumCond.Text == ">=" && stockQty < stockCondQty)
                        {
                            continue;
                        }
                        if (ncmbStockNumCond.Text == "=" && stockQty != stockCondQty)
                        {
                            continue;
                        }
                        if (ncmbStockNumCond.Text == "<" && stockQty >= stockCondQty)
                        {
                            continue;
                        }
                        if (ncmbStockNumCond.Text == "<=" && stockQty > stockCondQty)
                        {
                            continue;
                        }
                    }

                    #endregion

                    #region 有效期
                    if (ncbValidDate.Checked)
                    {
                        if (alValidDateWarnStorage == null || this.getValidDateWarnStorageTime.Date < DateTime.Now.Date || (int)this.nnudValidDays.Value != lastValidDateDays)
                        {
                            alValidDateWarnStorage = storageMgr.QueryWarnValidDateStockInfoList(this.priveDept.ID, (int)this.nnudValidDays.Value);
                            this.getValidDateWarnStorageTime = DateTime.Now.Date;
                            lastValidDateDays = (int)this.nnudValidDays.Value;
                        }
                        if (alValidDateWarnStorage == null)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.ShowMessage("根据有效期获取科室库存信息发生错误：" + storageMgr.Err, MessageBoxIcon.Error);
                        }
                        else
                        {
                            bool isValid = true;
                            foreach (FS.HISFC.Models.Pharmacy.Storage s in alValidDateWarnStorage)
                            {
                                if (s.Item.ID == stockInfo.Item.ID)
                                {
                                    isValid = false;
                                }
                            }
                            if (isValid)
                            {
                                continue;
                            }
                        }
                    }
                    #endregion

                }
                #endregion


                stockInfo.ValidTime = DateTime.MaxValue;

                #region 库存明细处理
                foreach (FS.HISFC.Models.Pharmacy.Storage storage in alStorageData)
                {
                    if (storage.Item.ID == stockInfo.Item.ID)
                    {
                        if (storage.ValidTime < stockInfo.ValidTime && storage.StoreQty > 0)
                        {
                            stockInfo.ValidTime = storage.ValidTime;
                        }
                        if (this.ncbMutiQuery.Checked && this.ncbValidDate.Checked && storage.ValidTime >= this.getValidDateWarnStorageTime.AddDays((double)this.nnudValidDays.Value))
                        {
                            continue;
                        }
                        totPurchaseCost += SOC.Public.Math.Round(storage.StoreQty * storage.Item.PriceCollection.PurchasePrice / storage.Item.PackQty, 2);
                        totWholeSaleCost += SOC.Public.Math.Round(storage.StoreQty * storage.Item.PriceCollection.WholeSalePrice / storage.Item.PackQty, 2);
                        totRetailCost += SOC.Public.Math.Round(storage.StoreQty * storage.Item.PriceCollection.RetailPrice / storage.Item.PackQty, 2);

                        storage.Item.UserCode = stockInfo.Item.UserCode;
                        storage.Item.NameCollection.RegularName = stockInfo.Item.NameCollection.RegularName;
                        storage.Item.NameCollection.SpellCode = stockInfo.Item.NameCollection.SpellCode;
                        storage.Item.NameCollection.WBCode = stockInfo.Item.NameCollection.WBCode;
                        storage.Item.NameCollection.RegularSpell.SpellCode = stockInfo.Item.NameCollection.RegularSpell.SpellCode;
                        storage.Item.NameCollection.RegularSpell.WBCode = stockInfo.Item.NameCollection.RegularSpell.WBCode;
                        storage.Item.NameCollection.RegularSpell.UserCode = stockInfo.Item.NameCollection.RegularSpell.UserCode;


                        storage.Item.NameCollection.FormalName = stockInfo.Item.NameCollection.FormalName;
                        storage.Item.NameCollection.FormalSpell.SpellCode = stockInfo.Item.NameCollection.FormalSpell.SpellCode;
                        storage.Item.NameCollection.OtherName = stockInfo.Item.NameCollection.OtherName;
                        storage.Item.NameCollection.OtherSpell.SpellCode = stockInfo.Item.NameCollection.OtherSpell.SpellCode;

                        this.dtStorage.Rows.Add(this.ConvertToStorageDataRow(storage, !stockInfo.IsStop, stockInfo.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid));
                    }
                }
                #endregion

                #region 再次校验有效期
                
                if (this.ncbMutiQuery.Checked)
                {
                    if (ncbValidDate.Checked)
                    {
                        if (stockInfo.ValidTime >= validDate)
                            continue;
                    }
                }

                #endregion

                this.dtStockInfo.Rows.Add(this.ConvertToStockInfoDataRow(stockInfo));
            }

            this.nlbInfo.Text = "当前查询的品种数：" + dtStockInfo.Rows.Count.ToString()
                + "，零售金额：" + totRetailCost.ToString("F4")
                + "，购入金额：" + totPurchaseCost.ToString("F4")
                + "，批发金额：" + totWholeSaleCost.ToString("F4");

            this.dtStockInfo.AcceptChanges();

            this.dvStockInfo = this.dtStockInfo.DefaultView;
            this.dvStockInfo.AllowNew = true;
            this.neuStockInfoSpread_Sheet1.DataSource = this.dvStockInfo;

            this.dtStorage.AcceptChanges();

            this.dvStorage = this.dtStorage.DefaultView;
            this.dvStorage.AllowNew = true;
            this.neuStockInfoSpread_Sheet2.DataSource = this.dvStorage;

            this.SetFormat();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 根据列名称获取列索引
        /// </summary>
        /// <param name="colName">列名称</param>
        /// <returns>成功返回列索引 失败返回-1</returns>
        private int GetColumnIndex(string colName)
        {
            for (int i = 0; i < this.neuStockInfoSpread_Sheet1.Columns.Count; i++)
            {
                if (this.neuStockInfoSpread_Sheet1.Columns[i].Label == colName)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 显示库存明细信息
        /// </summary>
        private void ShowSingleDrugStorageData()
        {
            this.ncbHideStorageInfo.Checked = false;
            this.neuStorageSpread1_Sheet1.Rows.Count = 0;

            if (this.neuStockInfoSpread.ActiveSheet.RowCount == 0)
            {
                return;
            }
            string drugCode = this.neuStockInfoSpread.ActiveSheet.Cells[this.neuStockInfoSpread.ActiveSheet.ActiveRowIndex, this.GetColumnIndex(neuStockInfoSpread.ActiveSheetIndex, "药品编码")].Text;
            string userCode = this.neuStockInfoSpread.ActiveSheet.Cells[this.neuStockInfoSpread.ActiveSheet.ActiveRowIndex, this.GetColumnIndex(neuStockInfoSpread.ActiveSheetIndex, "自定义码")].Text;
            FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
            ArrayList alStorage = storageMgr.QueryStorageList(this.priveDept.ID, drugCode);
            if (alStorage == null)
            {
                this.ShowMessage("获取药品库存明细失败：" + storageMgr.Err, MessageBoxIcon.Error);
            }
            foreach (FS.HISFC.Models.Pharmacy.Storage info in alStorage)
            {
                if (info.StoreQty <= 0 && !this.ncbShowZero.Checked)
                {
                    continue;
                }

                this.neuStorageSpread1_Sheet1.Rows.Add(this.neuStorageSpread1_Sheet1.RowCount, 1);

                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "自定义码", userCode);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "批号", info.BatchNO);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "名称", info.Item.Name);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "规格", info.Item.Specs);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "购入价", info.Item.PriceCollection.PurchasePrice.ToString() + "/" + info.Item.PackUnit);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "零售价", info.Item.PriceCollection.RetailPrice.ToString() + "/" + info.Item.PackUnit);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "有效期", info.ValidTime);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "数量", info.StoreQty);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "单位", info.Item.MinUnit);
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "生产厂家", SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(info.Producer.ID));
                this.neuStorageSpread1.SetCellValue(0, this.neuStorageSpread1_Sheet1.RowCount - 1, "供应商", SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).Product.Company.ID));
            }
        }

        /// <summary>
        /// 过滤
        /// </summary>
        private void Filter()
        {
            if (this.dtStockInfo.Rows.Count <= 0)
            {
                return;
            }
            string txtfilter = this.ntxtFilter.Text.Trim();

            txtfilter = FS.FrameWork.Public.String.TakeOffSpecialChar(txtfilter);

            try
            {
                string queryCode = "%" + txtfilter + "%";
                string filter = FS.SOC.HISFC.Components.Pharmacy.Function.GetFilterStr(this.dvStockInfo, queryCode);

                this.dvStockInfo.RowFilter = filter;

                filter = FS.SOC.HISFC.Components.Pharmacy.Function.GetFilterStr(this.dvStorage, queryCode);

                this.dvStorage.RowFilter = filter;

                this.SetFormat();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 科室库存数据保存
        /// </summary>
        private void Save()
        {
            this.neuStockInfoSpread.StopCellEditing();

            this.dvStockInfo.RowFilter = "1=1";
            this.SetFormat();

            for (int i = 0; i < this.dvStockInfo.Count; i++)
            {
                this.dvStockInfo[i].EndEdit();
            }

            DataTable dtModify = this.dtStockInfo.GetChanges(DataRowState.Modified);
            if (dtModify == null || dtModify.Rows.Count <= 0)
            {
                return;
            }

            ArrayList alStorage = new ArrayList();

            foreach (DataRow dr in dtModify.Rows)
            {
                FS.HISFC.Models.Pharmacy.Storage storage = this.ConvertStockInfoDataRowToStorage(dr);
                if (storage == null)
                {
                    return;
                }
                if (storage.LowQty > storage.TopQty)
                {
                    this.ShowMessage("保存失败：【" + storage.Item.Name + "】最低库存量不能大于库存最高量！", MessageBoxIcon.Error);
                    return;
                }

                alStorage.Add(storage);
            }

            FS.SOC.HISFC.BizLogic.Pharmacy.Storage itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (FS.HISFC.Models.Pharmacy.Storage storage in alStorage)
            {
                if (itemMgr.UpdateStockinfoModifyData(storage) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMessage("保存操作 更新库存失败" + itemMgr.Err, MessageBoxIcon.Error);
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();


            this.ShowMessage("保存成功");
        }

        /// <summary>
        /// 打印空白盘点表
        /// </summary>
        private void PrintEmptyCheckBill()
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.priveDept.ID;
            obj.Name = days.ToString();
            ArrayList al = new ArrayList();
            al.Add(obj);

            FS.SOC.HISFC.Components.Pharmacy.Function.PrintBill("0305", "01", al);
        }

        #endregion

        #region 事件

        public override int Export(object sender, object neuObject)
        {
            this.neuStockInfoSpread.Export();
            return base.Export(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ShowStockInfoData();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            this.SetFormat();
            this.ShowStockInfoData();

            this.nlbPriveDept.DoubleClick += new EventHandler(nlbPriveDept_DoubleClick);
            this.neuStockInfoSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuStockInfoSpread_ColumnWidthChanged);

            this.ncbMutiQuery.CheckedChanged += new EventHandler(ncbMutiQuery_CheckedChanged);
            this.ncbHideStorageInfo.CheckedChanged += new EventHandler(ncbStorage_CheckedChanged);

            this.ntxtFilter.TextChanged += new EventHandler(ntxtFilter_TextChanged);

            //this.neuStockInfoSpread.Sheets.Remove(this.neuStockInfoSpread_Sheet2);

            base.OnLoad(e);
        }

        void ntxtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        void ncbStorage_CheckedChanged(object sender, EventArgs e)
        {
            this.npanelStorageInfo.Visible = !this.ncbHideStorageInfo.Checked;
        }

        void ncbMutiQuery_CheckedChanged(object sender, EventArgs e)
        {
            this.ngbQuerySet.Visible = ncbMutiQuery.Checked;
        }

        void neuStockInfoSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.neuStockInfoSpread.ActiveSheet == this.neuStockInfoSpread_Sheet1)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuStockInfoSpread_Sheet1, this.settingFile);
            }
            else if (this.neuStockInfoSpread.ActiveSheet == this.neuStockInfoSpread_Sheet2)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuStockInfoSpread_Sheet2, this.storageSettingFile);
            }
        }

        void nlbPriveDept_DoubleClick(object sender, EventArgs e)
        {
            this.SetPriveDept();
            this.ShowStockInfoData();
        }

        #endregion

        #region 工具栏
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("刷新", "刷新库存信息药品显示", FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            toolBarService.AddToolButton("查看明细", "查询当前药品的库存明细", FS.FrameWork.WinForms.Classes.EnumImageList.X信息, true, false, null);
            toolBarService.AddToolButton("打印盘点表", "打印空白盘点表", FS.FrameWork.WinForms.Classes.EnumImageList.X信息, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "刷新")
            {
                this.ShowStockInfoData();
            }
            else if (e.ClickedItem.Text == "查看明细")
            {
                this.ShowSingleDrugStorageData();
            }
            else if (e.ClickedItem.Text == "打印盘点表")
            {
                this.PrintEmptyCheckBill();
            }

        }

        #endregion

        #region 提示信息

        /// <summary>
        /// 提供在状态栏第一panel显示信息的功能
        /// </summary>
        /// <param name="text">显示信息的文本</param>
        public void ShowStatusBarTip(string text)
        {
            if (this.ParentForm != null)
            {
                if (this.ParentForm is FS.FrameWork.WinForms.Forms.frmBaseForm)
                {
                    ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm).statusBar1.Panels[1].Text = "       " + text + "       ";
                }
            }
        }

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        public void ShowMessage(string text)
        {
            ShowMessage(text, MessageBoxIcon.Information);
        }

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        public void ShowMessage(string text, MessageBoxIcon messageBoxIcon)
        {
            string caption = "";
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }

            MessageBox.Show(text, caption, MessageBoxButtons.OK, messageBoxIcon);
        }

        #endregion
    }
}
