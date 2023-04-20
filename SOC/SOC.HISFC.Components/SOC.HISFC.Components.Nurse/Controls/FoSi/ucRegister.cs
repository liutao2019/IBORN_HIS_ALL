using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace FS.HISFC.Components.Nurse.FoSi
{
    /// <summary>
    /// 佛四注射管理
    /// </summary>
    public partial class ucRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public ucRegister()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.fpQuery.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpQuery_CellClick);
                fpQuery.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpQuery_CellDoubleClick);
            }
        }

        #region 定义域

        /// <summary>
        /// 院注管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 挂号管理
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 门诊收费管理
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 
        /// </summary>
        FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();

        /// <summary>
        /// 费用管理
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee interFeeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 挂号管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration interRegMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register reg = null;

        /// <summary>
        /// 常数业务层函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 药房函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy interPhaMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 医嘱函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order interOrderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 未打印病人的列设置路径
        /// </summary>
        protected string filePathInjectNoPrintPatient = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\InjectNoPrintPatient.xml";

        /// <summary>
        /// 注射单显示界面配置文件
        /// </summary>
        private string injectRegisterXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\injectRegister.xml";

        /// <summary>
        /// 已打印病人的列设置路径
        /// </summary>
        protected string filePathInjectPrintPatient = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\InjectPrintPatient.xml";

        //private ArrayList al = new ArrayList();


        FS.HISFC.Models.Pharmacy.Item drug = null;

        FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;

        /// <summary>
        /// 科室帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// 治疗单的数据
        /// </summary>
        private ArrayList alZLDPrint = null;

        /// <summary>
        /// 注射单的数据
        /// </summary>
        private ArrayList alInjectPrint = null;

        /// <summary>
        /// 是否重打
        /// </summary>
        private bool isReprint = false;

        /// <summary>
        /// 医生集合
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper emplHelper = null;

        /// <summary>
        /// 病人信息集合
        /// </summary>
        Hashtable hsInfos = new Hashtable();

        /// <summary>
        /// 是否第一次登记
        /// </summary>
        private bool IsFirstTime = false;

        /// <summary>
        /// 是否登记零院注
        /// </summary>
        private bool isRegNullNum = true;

        /// <summary>
        /// 是否自动生成序列号
        /// </summary>
        private bool isAutoBuildOrderNo = true;

        /// <summary>
        /// 是否显示已院注
        /// </summary>
        private bool isRegFinishNum = false;

        /// <summary>
        /// 是否过滤看诊科室  
        /// </summary>
        private bool isFilterDoctDept = false;

        /// <summary>
        /// 院注次数
        /// </summary>
        private int countInject = 0;

        /// <summary>
        /// 最大注射顺序号
        /// </summary>
        private int maxInjectOrder = 0;

        /// <summary>
        /// 打印在巡视卡上的用法
        /// </summary>
        private string usage = "01";

        /// <summary>
        /// 非药品打印在巡视卡上的用法
        /// </summary>
        private string unDrugUsage = "";

        /// <summary>
        /// 在当前注射室打印的科室
        /// </summary>
        private string doctDept = "";

        /// <summary>
        /// 注射用法（不包括输液项目）
        /// </summary>
        private string injectUsage = "";

        #region 注射顺序号
        /// <summary>
        /// 是否自动生成注射顺序号
        /// </summary>
        private bool IsAutoOrder = true;
        /// <summary>
        /// 当前注射顺序号
        /// </summary>
        private int currentOrder = 0;
        #endregion

        /// <summary>
        /// 是否显示患者当天可登记的全部处方
        /// </summary>
        private bool isShowAllInject = false;

        /// <summary>
        /// 是否显示有特定用法的非药品
        /// </summary>
        private bool isShowUnDrug = false;

        /// <summary>
        /// 是否是退单
        /// </summary>
        private bool isQuit = false;

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freqHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 获取注射序号接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo IGetOrderNo = null;

        #endregion

        #region 属性

        /// <summary>
        /// 是否显示患者当天可登记的全部处方
        /// </summary>
        [Description("是否显示患者当天可登记的全部处方"), Category("设置"), DefaultValue("false")]
        public bool IsShowAllInject
        {
            get
            {
                return isShowAllInject;
            }
            set
            {
                isShowAllInject = value;
            }
        }

        /// <summary>
        /// 是否自动生成序列号
        /// </summary>
        [Description("是否自动生成序列号"), Category("设置")]
        public bool IsAutoBuildOrderNo
        {
            get
            {
                return isAutoBuildOrderNo;
            }
            set
            {
                isAutoBuildOrderNo = value;
            }
        }

        /// <summary>
        /// 在当前注射室打印的科室
        /// </summary>
        [Description("在当前注射室打印的科室，不维护默认全部，以'1001','2001','3001'格式维护"), Category("设置")]
        public string DoctDept
        {
            get
            {
                return this.doctDept;
            }
            set
            {
                this.doctDept = value;
            }
        }

        /// <summary>
        /// 是否显示有特定用法的非药品
        /// </summary>
        [Description("是否显示有特定用法的非药品"), Category("设置")]
        public bool IsShowUnDrug
        {
            get
            {
                return isShowUnDrug;
            }
            set
            {
                isShowUnDrug = value;
            }
        }

        /// <summary>
        /// 是否过滤看诊科室
        /// </summary>
        [Description("是否过滤看诊科室"), Category("设置")]
        public bool IsFilterDoctDept
        {
            get
            {
                return isFilterDoctDept;
            }
            set
            {
                isFilterDoctDept = value;
            }
        }

        /// <summary>
        /// 是否登记零院注
        /// </summary>
        [Description("是否登记零院注"), Category("设置")]
        public bool IsRegNullNum
        {
            get
            {
                return isRegNullNum;
            }
            set
            {
                isRegNullNum = value;
            }
        }

        /// <summary>
        /// 是否显示已院注
        /// </summary>
        [Description("是否显示已院注"), Category("设置")]
        public bool IsRegFinishNum
        {
            get
            {
                return isRegFinishNum;
            }
            set
            {
                isRegFinishNum = value;
            }
        }

        /// <summary>
        /// 用法是否打印在巡视卡上
        /// </summary>
        [Description("用法的ID是否打印在巡视卡上，以'001','002','003'格式维护"), Category("设置")]
        public string Usage
        {
            get
            {
                return this.usage;
            }
            set
            {
                this.usage = value;
            }
        }

        /// <summary>
        /// 非药品打印在巡视卡上的用法
        /// </summary>
        [Description("非药品用法的ID打印在巡视卡上，以'001','002','003'格式维护"), Category("设置")]
        public string UnDrugUsage
        {
            get
            {
                return this.unDrugUsage;
            }
            set
            {
                this.unDrugUsage = value;
            }
        }

        /// <summary>
        /// 注射用法的ID维护（不包括输液用法,可不维护），以'001','002','003'格式维护
        /// </summary>
        [Description("注射用法的ID维护（不包括输液用法,可不维护），以'001','002','003'格式维护"), Category("设置")]
        public string InjectUsage
        {
            get
            {
                return this.injectUsage;
            }
            set
            {
                this.injectUsage = value;
            }
        }

        /// <summary>
        /// 是否自动打印巡视卡
        /// </summary>
        private bool isAutoPrint = true;

        /// <summary>
        /// 是否自动打印巡视卡
        /// </summary>
        [Description("是否自动打印巡视卡"), Category("设置")]
        public bool IsAutoPrint
        {
            get
            {
                return isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        /// <summary>
        /// 是否自动保存数据
        /// </summary>
        private bool isAutoSave = true;

        /// <summary>
        /// 是否自动保存数据
        /// </summary>
        [Description("是否自动保存数据"), Category("设置")]
        public bool IsAutoSave
        {
            get
            {
                return isAutoSave;
            }
            set
            {
                this.isAutoSave = value;
            }
        }

        /// <summary>
        /// 是否使用注射顺序号
        /// </summary>
        private bool isUserOrderNumber = false;

        /// <summary>
        /// 是否使用注射顺序号
        /// </summary>
        [Description("是否使用注射顺序号"), Category("设置")]
        public bool IsUserOrderNumber
        {
            get
            {
                return isUserOrderNumber;
            }
            set
            {
                this.isUserOrderNumber = value;
            }
        }

        /// <summary>
        /// 自动打印时保存是否提示
        /// </summary>
        private bool isMessageInSave = true;

        /// <summary>
        /// 自动打印时保存是否提示
        /// </summary>
        [Description("自动打印时保存是否提示"), Category("设置")]
        public bool IsMessageInSave
        {
            get
            {
                return isMessageInSave;
            }
            set
            {
                this.isMessageInSave = value;
            }
        }

        /// <summary>
        /// 查询的挂号的日期间隔
        /// </summary>
        private int queryRegDays = 2;

        /// <summary>
        /// 查询的挂号的日期间隔
        /// </summary>
        [Category("查询设置"), Description("查询挂号信息的日期间隔（从界面上的开始时间往前查询几天）")]
        public int QueryRegDays
        {
            get
            {
                return queryRegDays;
            }
            set
            {
                queryRegDays = value;
            }
        }

        /// <summary>
        /// 开始时间的日期间隔(距今天的间隔日期)
        /// </summary>
        private int beginDateIntervalDays = 0;

        /// <summary>
        /// 开始时间的日期间隔(距今天的间隔日期)
        /// </summary>
        [Category("查询设置"), Description("开始时间的日期间隔(距今天的间隔日期)")]
        public int BeginDateIntervalDays
        {
            get
            {
                return beginDateIntervalDays;
            }
            set
            {
                beginDateIntervalDays = value;
            }
        }


        string InvoiceNo = "";

        #region 自动刷新

        /// <summary>
        /// 当前是否可以刷新，如果正在打印则不刷
        /// </summary>
        bool isCanRefresh = true;

        /// <summary>
        /// 是否自动刷新打印
        /// </summary>
        bool isAutoRefreshPrint = false;

        /// <summary>
        /// 是否显示按卡号查询条件
        /// </summary>
        private bool isShowSelectBox = false;

        /// <summary>
        /// 是否显示按卡号查询条件
        /// </summary>
        [Description("是否显示按卡号查询条件"), Category("查询设置")]
        public bool IsShowSelectBox
        {
            get
            {
                return isShowSelectBox;
            }
            set
            {
                isShowSelectBox = value;
            }
        }

        /// <summary>
        /// 未打印
        /// </summary>
        DataView dvNoPrint;

        /// <summary>
        /// 已打印
        /// </summary>
        DataView dvPrint;

        /// <summary>
        /// 查询患者列表
        /// </summary>
        DataView dvQuery;

        /// <summary>
        /// 是否自动刷新打印
        /// </summary>
        [Description("是否自动刷新打印"), Category("设置")]
        public bool IsAutoRefreshPrint
        {
            get
            {
                return isAutoRefreshPrint;
            }
            set
            {
                isAutoRefreshPrint = value;
            }
        }

        /// <summary>
        /// 刷新间隔，默认10秒
        /// </summary>
        private int freshTimes = 10;

        /// <summary>
        /// 刷新间隔，默认10秒
        /// </summary>
        [Description("刷新间隔，单位秒，默认10秒"), Category("设置")]
        public int FreshTimes
        {
            get
            {
                return freshTimes;
            }
            set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                freshTimes = value;
                this.timer1.Interval = value * 1000;
            }
        }

        #endregion

        #endregion

        #region 初始化

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDept_Load(object sender, EventArgs e)
        {
            this.Init();
            this.SetFP();
            this.AutoPrintSet(this.isAutoRefreshPrint);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.dtpStart.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(0 - this.beginDateIntervalDays);
            this.dtpEnd.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(1).AddSeconds(-1);
            DateTime dt = this.InjMgr.GetDateTimeFromSysDateTime();
            this.dtpAutoPrintBegin.Value = new DateTime(dt.Year,dt.Month,dt.Day,0,0,0);
            this.lblName.Text = "";
            this.lblSex.Text = "";
            this.lblAge.Text = "";
            //this.initDoctor();

            //初始化人员列表
            ArrayList al = this.interMgr.QueryEmployeeAll();
            if (al == null)
            {
                MessageBox.Show(interMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                emplHelper = new FS.FrameWork.Public.ObjectHelper(al);
            }
            
            //初始化科室列表
            al = new ArrayList();
            al = this.interMgr.GetDepartment();
            if (al == null)
            {
                MessageBox.Show(interMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                deptHelper = new FS.FrameWork.Public.ObjectHelper(al);
            }
            
            //频次
            al = new ArrayList();
            al = this.interMgr.QuereyFrequencyList();
            if (al == null)
            {
                MessageBox.Show(interMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                freqHelper = new FS.FrameWork.Public.ObjectHelper(al);
            }

            this.txtCardNo.Focus();
            this.InitOrder();

            this.gbxQuery.Visible = this.isShowSelectBox;
        }


        /// <summary>
        /// 设置格式
        /// </summary>
        private void SetFP()
        {
            if (System.IO.File.Exists(injectRegisterXml))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.fpInjectInfo_Sheet1, injectRegisterXml);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.fpInjectInfo_Sheet1, injectRegisterXml);
            }

            this.fpInjectInfo.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpInjectInfo_ColumnWidthChanged);
        }

        #endregion

        #region 工具栏

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 点击工具栏事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("全选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("取消", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("打印瓶签", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印签名卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印注射单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印患者卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("暂停刷新", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            this.toolBarService.AddToolButton("启动刷新", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            //增加打印号码条
            this.toolBarService.AddToolButton("打印号码条", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            this.toolBarService.AddToolButton("修改皮试", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 点击工具栏事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "全选":
                    this.SelectAll(true);
                    break;
                case "取消":
                    this.SelectAll(false);
                    break;
                case "打印瓶签":
                    this.PrintCure();
                    break;
                case "打印签名卡":
                    this.PrintItinerate();
                    break;
                case "打印注射单":
                    this.PrintInject();
                    break;
                case "打印患者卡":
                    this.PrintPatient();
                    break;
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} 增加打印号码条
                case "打印号码条":
                    this.PrintNumber();
                    break;
                //{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
                case "修改皮试":
                    this.ModifyHytest();
                    break;
                case "暂停刷新":
                    this.timer1.Enabled = false;
                    this.lblNote.Text = "此时暂停自动刷新";
                    this.toolBarService.SetToolButtonEnabled("暂停刷新", false);
                    this.toolBarService.SetToolButtonEnabled("启动刷新", true);
                    FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【暂停刷新！】");
                    break;
                case "启动刷新":
                    this.timer1.Enabled = true;
                    this.timer1.Interval = this.freshTimes * 1000;
                    this.lblNote.Text = "此时正在自动刷新";
                    this.toolBarService.SetToolButtonEnabled("暂停刷新", true);
                    this.toolBarService.SetToolButtonEnabled("启动刷新", false);
                    FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【启动刷新！】");
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 公共

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始手动查询查询打印！】");
            this.BatchQuery();
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束手动查询查询打印！】");
            return 1;
        }

        /// <summary>
        /// 保存列宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpInjectInfo_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.fpInjectInfo_Sheet1, injectRegisterXml);
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 设置颜色(高亮度显示最后一条clinic医嘱)
        /// </summary>
        /// <returns></returns>
        private int ShowColor()
        {
            //取得最大clinic_code
            int maxClinic = 0;
            if (this.fpInjectInfo_Sheet1.RowCount <= 0) return -1;
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item =
                    (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpInjectInfo_Sheet1.Rows[i].Tag;
                if (FS.FrameWork.Function.NConvert.ToInt32(item.ID) > maxClinic)
                {
                    maxClinic = FS.FrameWork.Function.NConvert.ToInt32(item.ID);
                }
            }
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item =
                    (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpInjectInfo_Sheet1.Rows[i].Tag;
                if (item.ID == maxClinic.ToString())
                {
                    this.fpInjectInfo_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 0, false);
                }
            }
            return 0;
        }

        /// <summary>
        /// 获得最大注射顺序
        /// </summary>
        /// <returns></returns>
        private int GetMaxInjectOrder()
        {
            if (this.fpInjectInfo_Sheet1.RowCount <= 0) return 0;
            this.fpInjectInfo.StopCellEditing();
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                if (this.fpInjectInfo_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                    this.fpInjectInfo_Sheet1.GetText(i, 0) == "") continue;
                if (FS.FrameWork.Function.NConvert.ToInt32(this.fpInjectInfo_Sheet1.Cells[i, 1].Text) > maxInjectOrder)
                {
                    maxInjectOrder = FS.FrameWork.Function.NConvert.ToInt32(this.fpInjectInfo_Sheet1.Cells[i, 1].Text);
                }
            }
            return maxInjectOrder;
        }

        /// <summary>
        /// 显示病人信息
        /// </summary>
        /// <param name="reg"></param>
        private void ShowPatient(FS.HISFC.Models.Registration.Register reg, string invocieNo)
        {
            if (reg == null || reg.ID == "")
            {
                return;
            }
            else
            {
                this.lblName.Text = reg.Name;
                this.lblSex.Text = reg.Sex.Name;
                this.lblAge.Text = this.InjMgr.GetAge(reg.Birthday);
                this.txtCardNo.Text = reg.PID.CardNO;
                this.lblCardNo.Text = reg.PID.CardNO.TrimStart('0');
                this.lblInvoiceNo.Text = invocieNo;
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                this.fpInjectInfo_Sheet1.Cells[i, 0].Value = isSelected;
            }
        }


        #endregion

        #region  打印

        /// <summary>
        /// 打印患者卡
        /// </summary>
        private void PrintPatient()
        {
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint patientPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            if (patientPrint == null)
            {
                patientPrint = new FS.HISFC.Components.Nurse.Print.ucPrintPatient() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            }

            patientPrint.Init(al);
        }

        /// <summary>
        /// 打印瓶签
        /// </summary>
        private void PrintCure()
        {
            ArrayList al = this.GetPrintInjectList();

            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            //打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint curePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            if (curePrint == null)
            {
                curePrint = new FS.HISFC.Components.Nurse.Print.ucPrintCure() as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
                //FS.HISFC.Components.FS.HISFC.Components.Nurse.Print.ucPrintCure uc = new FS.HISFC.Components.FS.HISFC.Components.Nurse.Print.ucPrintCure();
            }
            curePrint.Init(al);
        }

        /// <summary>
        /// 打印注射单.
        /// </summary>
        private void PrintInject()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            //打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint injectPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            if (injectPrint == null)
            {
                injectPrint = new FS.HISFC.Components.Nurse.Print.ucPrintInject() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            }
            injectPrint.Init(al);
        }

        /// <summary>
        /// 打印签名卡
        /// </summary>
        private void PrintItinerate()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            //打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new FS.HISFC.Components.Nurse.Print.ucPrintItinerate() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = isReprint;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// 连续纸张的签名卡
        /// </summary>
        private void PrintItinerateLarge()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }

            //打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new FS.HISFC.Components.Nurse.Print.ucPrintItinerateLarge() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = isReprint;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// 打印静脉输液巡视卡
        /// </summary>
        private void PrintInjectScoutCard()
        {
            int intReturn = this.GetAllPrintInjectList();
            if (intReturn == -1)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }

            foreach (ArrayList al in hsInfos.Values)
            {
                FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Nurse.ucRegister), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                if (itineratePrint == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(injectUsage))
                {
                    itineratePrint.IsReprint = isReprint;
                    itineratePrint.Init(al);
                }
                else
                {
                    //注射单
                    ArrayList alZS = new ArrayList();

                    ArrayList alSY = new ArrayList();
                    for (int i = 0; i < al.Count; i++)
                    {
                        FS.HISFC.Models.Nurse.Inject info = al[i] as FS.HISFC.Models.Nurse.Inject;
                        if (injectUsage.Contains(info.Item.Order.Usage.ID.ToString() + ";"))
                        {
                            alZS.Add(info);
                        }
                        else
                        {
                            alSY.Add(info);
                        }
                    }
                    if (alZS.Count > 0)
                    {
                        itineratePrint.IsReprint = isReprint;
                        itineratePrint.Init(alZS);
                    }
                    if (alSY.Count > 0)
                    {
                        itineratePrint.IsReprint = isReprint;
                        itineratePrint.Init(alSY);
                    }
                }
            }

        }

        /// <summary>
        /// 增加号码条
        /// </summary>
        private void PrintNumber()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            //打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint numberPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            if (numberPrint == null)
            {
                numberPrint = new FS.HISFC.Components.Nurse.Print.ucPrintNumber() as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            }
            numberPrint.Init(al);
        }

        /// <summary>
        /// 获取要打印的数据
        /// </summary>
        /// <returns></returns>
        private ArrayList GetPrintInjectList()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            this.fpInjectInfo.StopCellEditing();

            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                if (this.fpInjectInfo_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                {
                    continue;
                }

                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpInjectInfo_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.fpInjectInfo_Sheet1.Cells[i, 11].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();

                info.Patient = reg;

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.fpInjectInfo_Sheet1.Cells[i, 2].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString();

                //医生名称
                info.Item.Order.ReciptDoctor.Name = this.emplHelper.GetName(detail.RecipeOper.ID);
                info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.fpInjectInfo_Sheet1.GetValue(i, 1) == null || this.fpInjectInfo_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.fpInjectInfo_Sheet1.GetValue(i, 1).ToString();
                }
                info.InjectOrder = strOrder;
                al.Add(info);
                //判断接瓶,如果是则添加到alJiePing中
                if (orderinfo.ExtendFlag1 == null || orderinfo.ExtendFlag1.Length < 1)
                {
                    orderinfo.ExtendFlag1 = "1|";
                }

                int inum = FS.FrameWork.Function.NConvert.ToInt32(orderinfo.ExtendFlag1.Substring(0, 1));
                info.Memo = inum.ToString();
                info.PrintNo = detail.User02;
            }
            return al;
        }

        /// <summary>
        /// 获取要打印的数据（可维护用法）
        /// </summary>
        /// <returns></returns>
        private int GetAllPrintInjectList()
        {
            this.SelectAll(true);
            this.fpInjectInfo.StopCellEditing();
            hsInfos.Clear();
            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;

            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                if (this.fpInjectInfo_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                    continue;
                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpInjectInfo_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.fpInjectInfo_Sheet1.Cells[i, 11].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();

                info.Patient.ID = detail.Patient.ID;
                info.Patient.Name = reg.Name;
                info.Patient.Sex.ID = reg.Sex.ID;
                info.Patient.Birthday = reg.Birthday;
                info.Patient.Card.ID = this.txtCardNo.Text.Trim().PadLeft(10, '0');

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.fpInjectInfo_Sheet1.Cells[i, 2].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString();

                //医生名称
                info.Item.Order.ReciptDoctor.Name = this.emplHelper.GetName(detail.RecipeOper.ID);
                info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.fpInjectInfo_Sheet1.GetValue(i, 1) == null || this.fpInjectInfo_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.fpInjectInfo_Sheet1.GetValue(i, 1).ToString();
                }
                info.Item.Days = detail.Days;
                //患者当次注射处方时间
                info.InjectOrder = strOrder;

                info.User03 = this.fpInjectInfo_Sheet1.Cells[i, 13].Text;

                string hypoTest = string.IsNullOrEmpty(this.fpInjectInfo_Sheet1.Cells[i, 11].Text) ? "" : "(" + this.fpInjectInfo_Sheet1.Cells[i, 11].Text + ")";

                if (orderinfo != null)
                {
                    //备注应该是Memo+皮试
                    //info.Memo = orderinfo.ExtendFlag1;
                    info.Memo = orderinfo.Memo;
                    info.Hypotest = orderinfo.HypoTest;
                }

                if (!hsInfos.ContainsKey(info.Item.Order.ReciptDoctor.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(info);
                    hsInfos.Add(info.Item.Order.ReciptDoctor.ID, al);
                }
                else
                {
                    ((ArrayList)hsInfos[info.Item.Order.ReciptDoctor.ID]).Add(info);
                }
            }
            return 1;
        }

        /// <summary>
        /// 获取最优先的使用方法
        /// </summary>
        /// <param name="IsInit">是否初始化</param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Const GetFirstUsage()
        {
            FS.HISFC.Models.Fee.Outpatient.FeeItemList info = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            if (this.fpInjectInfo_Sheet1.RowCount <= 0) return new FS.HISFC.Models.Base.Const();

            int FirstCodeNum = 10000;
            FS.HISFC.Models.Base.Const retobj = new FS.HISFC.Models.Base.Const();
            try
            {
                for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
                {
                    info = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpInjectInfo_Sheet1.Rows[i].Tag;
                    FS.FrameWork.Models.NeuObject obj = this.interMgr.GetConstant("SPECIAL", info.Order.Usage.ID);
                    FS.HISFC.Models.Base.Const conobj = (FS.HISFC.Models.Base.Const)obj;

                    if (conobj.SortID < FirstCodeNum)
                    {
                        FirstCodeNum = conobj.SortID;
                        retobj = conobj;
                    }
                }
            }
            catch
            {
                return retobj;
            }

            return retobj;
        }
        #endregion

        #region 注射顺序号的处理
        /// <summary>
        /// 设置默认注射顺序
        /// </summary>
        private void SetInject()
        {
            #region  没有数据就不管了,直接返回
            if (this.fpInjectInfo_Sheet1.RowCount <= 0) return;
            #endregion

            #region 设置患者今天的注射顺序号
            if (this.isAutoBuildOrderNo)
            {
                this.SetOrder();
            }
            else
            {
                this.txtOrder.Text = "0";
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                for (int i = 0; i < this.fpInjectInfo_Sheet1.Rows.Count; i++)
                {
                    this.fpInjectInfo_Sheet1.Cells[i, 14].Text = this.txtOrder.Text;
                }
            }
            #endregion

            #region 设置每组项目的注射顺序
            int InjectOrder = 1;
            this.fpInjectInfo_Sheet1.SetValue(0, 1, 1, false);
            for (int i = 1; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {

                if (this.fpInjectInfo_Sheet1.Cells[i, 7].Text == null || this.fpInjectInfo_Sheet1.Cells[i, 7].Text.Trim() == "")
                {
                    InjectOrder++;
                    this.fpInjectInfo_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else if (this.fpInjectInfo_Sheet1.Cells[i, 7].Text != null && this.fpInjectInfo_Sheet1.Cells[i, 7].Text.Trim() != ""
                    //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                    && this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() + this.fpInjectInfo_Sheet1.Cells[i, 13].Text == this.fpInjectInfo_Sheet1.Cells[i - 1, 7].Tag.ToString() + this.fpInjectInfo_Sheet1.Cells[i - 1, 13].Text)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else
                {
                    InjectOrder++;
                    this.fpInjectInfo_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
            }
            #endregion
        }

        /// <summary>
        /// 初始化注射顺序号
        /// </summary>
        private void InitOrder()
        {
            //读取是否自动生成注射顺序
            try
            {
                bool isAutoInjectOrder = false;
                isAutoInjectOrder = FS.FrameWork.Function.NConvert.ToBoolean(this.interMgr.QueryControlerInfo("900005"));
                if (isAutoInjectOrder)
                {
                    this.isAutoBuildOrderNo = true;
                    this.SetOrder();
                    this.lbLastOrder.Text = "今天最后一次注射号:" +
                        (FS.FrameWork.Function.NConvert.ToInt32(this.txtOrder.Text.Trim()) - 1).ToString();
                }
                else
                {
                    this.isAutoBuildOrderNo = false;
                    this.lbLastOrder.Text = "本机不自动生成注射顺序号!";
                    this.txtOrder.Text = "0";
                }
            }
            catch //无配置文件
            {
                this.isAutoBuildOrderNo = false;
                this.lbLastOrder.Text = "本机不自动生成注射顺序号!";
                this.txtOrder.Text = "0";
            }


        }
        /// <summary>
        /// 设置注射号
        /// </summary>
        private void SetOrder()
        {
            if (!this.isAutoBuildOrderNo)
            {
                this.txtOrder.Text = "0";
                this.lbLastOrder.Text = "现在本机没有设置自动生成序号!";
                return;
            }
            //如果自动生成,设置第一个序号,并赋值this.currentOrder
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} 改为通过接口实现，如果没有配置则按原代码生程序号
            this.CreateInterface();
            if (IGetOrderNo != null)
            {
                string orderNo = IGetOrderNo.GetOrderNo(this.reg);
                this.txtOrder.Text = orderNo;
                if (this.fpInjectInfo_Sheet1.Rows.Count == 0)
                {
                    return;
                }
                string comboAndInjectTime = this.fpInjectInfo_Sheet1.Cells[0, 7].Tag.ToString() + this.fpInjectInfo_Sheet1.Cells[0, 13].Text;
                for (int i = 0; i < this.fpInjectInfo_Sheet1.Rows.Count; i++)
                {
                    string rowComboAndInjectTime = this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() + this.fpInjectInfo_Sheet1.Cells[i, 13].Text;
                    if (comboAndInjectTime != rowComboAndInjectTime)
                    {
                        comboAndInjectTime = rowComboAndInjectTime;
                        orderNo = IGetOrderNo.GetSamePatientNextOrderNo(orderNo);
                    }
                    this.fpInjectInfo_Sheet1.Cells[i, 14].Text = orderNo;
                }
                return;
            }
            else
            {
                FS.HISFC.Models.Nurse.Inject info = this.InjMgr.QueryLast();
                if (info != null && info.Booker.OperTime != System.DateTime.MinValue)
                {
                    if (info.Booker.OperTime.ToString("yyyy-MM-dd")
                        == this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd"))
                    {
                        this.txtOrder.Text = (FS.FrameWork.Function.NConvert.ToInt32(info.OrderNO) + 1).ToString();
                    }
                    else
                    {
                        this.txtOrder.Text = "1";
                    }
                }
                else
                {
                    this.txtOrder.Text = "1";
                }
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                for (int i = 0; i < this.fpInjectInfo_Sheet1.Rows.Count; i++)
                {
                    this.fpInjectInfo_Sheet1.Cells[i, 14].Text = this.txtOrder.Text;
                }
            }
        }

        /// <summary>
        /// 创建接口
        /// {30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
        /// </summary>
        private void CreateInterface()
        {
            if (this.IGetOrderNo == null)
            {
                this.IGetOrderNo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo)) as FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 确认保存
        /// ( 1.met_nuo_inject插入记录  2.fin_ipb_feeitemlist更新已确认院注数量，确认标志)
        /// </summary>
        private int Save()
        {
            if (this.fpInjectInfo_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("没有要保存的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            this.fpInjectInfo.StopCellEditing();
            int selectNum = 0;
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                if (this.fpInjectInfo_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE" || this.fpInjectInfo_Sheet1.GetValue(i, 0).ToString() == "")
                {
                    selectNum++;
                }
            }
            if (selectNum >= this.fpInjectInfo_Sheet1.RowCount)
            {
                MessageBox.Show("请选择要保存的数据", "提示");
                return -1;
            }
            alInjectPrint = new ArrayList();
            alZLDPrint = new ArrayList();

            if (this.isUserOrderNumber)
            {
                #region 判断输入队列号的有效性
                if (this.txtOrder.Text == null || this.txtOrder.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("没有输入队列顺序号!");
                    this.txtOrder.Focus();
                    return -1;
                }
                else if (this.InjMgr.QueryInjectOrder(this.txtOrder.Text.Trim().ToString()).Count > 0)
                {
                    if (MessageBox.Show("该队列号已经使用,是否继续!", "提示", System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        this.txtOrder.Focus();
                        return -1;
                    }
                }
                #endregion


                #region 检查注射顺序号的有效性（组号相同的，注射顺序号也必须相同）
                for (int i = 1; i < this.fpInjectInfo_Sheet1.RowCount; i++)
                {
                    if (this.fpInjectInfo_Sheet1.Cells[i, 7].Tag != null && this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() != "" &&
                        //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                        this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() + this.fpInjectInfo_Sheet1.Cells[i, 13].Text == this.fpInjectInfo_Sheet1.Cells[i - 1, 7].Tag.ToString() + this.fpInjectInfo_Sheet1.Cells[i - 1, 13].Text
                        && this.fpInjectInfo_Sheet1.GetValue(i, 1).ToString() != this.fpInjectInfo_Sheet1.GetValue(i - 1, 1).ToString()
                        )
                    {
                        MessageBox.Show("相同组号的注射顺序号必须相同!", "第" + (i + 1).ToString() + "行");
                        return -1;
                    }
                }
                #endregion
            }

            #region 检查院注次数的有效性（组号相同的，注射顺序号也必须相同）
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                string strnum = this.fpInjectInfo_Sheet1.Cells[i, 2].Text;
                if (strnum == null || strnum == "")
                {
                    MessageBox.Show("院注次数不能为空!", "第" + (i + 1).ToString() + "行");
                    return -1;
                }
                if (!this.IsNum(strnum))
                {
                    MessageBox.Show("院注次数必须为数字!", "第" + (i + 1).ToString() + "行");
                    return -1;
                }
                string completenum = this.fpInjectInfo_Sheet1.Cells[i, 3].Text;
                if (this.fpInjectInfo_Sheet1.GetValue(i, 0).ToString().ToUpper() == "TRUE")
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) == 0)
                    {
                        continue;
                    }

                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) <= FS.FrameWork.Function.NConvert.ToInt32(completenum))
                    {
                        MessageBox.Show("院注次数已满!", "第" + (i + 1).ToString() + "行");
                        return -1;
                    }
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            try
            {
                DateTime confirmDate = this.InjMgr.GetDateTimeFromSysDateTime();

                FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
                FS.HISFC.Models.Nurse.Inject info = null;

                for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
                {
                    if (this.fpInjectInfo_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                        this.fpInjectInfo_Sheet1.GetText(i, 0) == "")
                    {
                        continue;
                    }
                    detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpInjectInfo_Sheet1.Rows[i].Tag;

                    info = new FS.HISFC.Models.Nurse.Inject();

                    #region 实体转化（门诊项目收费明细实体FeeItemList－->注射实体Inject）

                    info.Patient = reg;
                    info.Patient.ID = detail.Patient.ID;
                    info.Patient.Name = reg.Name;
                    info.Patient.Sex.ID = reg.Sex.ID;
                    info.Patient.Birthday = reg.Birthday;
                    info.Patient.PID.CardNO = reg.PID.CardNO;

                    //info.Item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)detail.Item;
                    info.Item = detail;
                    info.Item.ID = detail.Item.ID;
                    info.Item.Name = detail.Item.Name;
                    info.Item.Item.ItemType = detail.Item.ItemType;

                    info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.fpInjectInfo_Sheet1.Cells[i, 2].Text);
                    //开单科室名称
                    info.Item.Order.DoctorDept.Name = this.deptHelper.GetName(detail.RecipeOper.Dept.ID);
                    info.Item.Order.DoctorDept.ID = detail.RecipeOper.Dept.ID;
                    //医生名称
                    info.Item.Order.ReciptDoctor.Name = emplHelper.GetName(detail.RecipeOper.ID);
                    info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                    //是否皮试
                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && this.fpInjectInfo_Sheet1.Cells[i, 11].Tag.ToString().ToUpper() == "TRUE")
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                    }
                    else
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                    }
                    #endregion

                    info.ID = this.InjMgr.GetSequence("Nurse.Inject.GetSeq");
                    info.OrderNO = this.fpInjectInfo_Sheet1.Cells[i, 14].Text;
                    info.PrintNo = detail.User02;
                    info.Item.Order.Combo.ID = this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString();
                    info.Booker.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    info.Booker.OperTime = confirmDate;
                    info.Item.ExecOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    string strOrder = "";
                    if (this.fpInjectInfo_Sheet1.GetValue(i, 1) == null || this.fpInjectInfo_Sheet1.GetValue(i, 1).ToString() == "")
                    {
                        strOrder = "";
                    }
                    else
                    {
                        strOrder = this.fpInjectInfo_Sheet1.GetValue(i, 1).ToString();
                    }
                    info.InjectOrder = strOrder;
                    info.Item.Days = detail.Days;
                    string hypoTest = this.fpInjectInfo_Sheet1.Cells[i, 11].Text;

                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //备注--(取医嘱备注)
                        FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                            (FS.HISFC.Models.Order.OutPatient.Order)this.fpInjectInfo_Sheet1.Cells[i, 11].Tag;
                        if (orderinfo != null)
                        {
                            //备注应该是Memo+皮试
                            info.Memo = orderinfo.Memo;
                            info.Hypotest = orderinfo.HypoTest;
                        }
                    }

                    #region 向met_nuo_inject中，插入记录
                    if (this.InjMgr.Insert(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.InjMgr.Err, "提示");
                        return -1;
                    }
                    #endregion

                    #region 向fin_ipb_feeitemlist中，插入数量
                    string cancelFlag = "";
                    if (isQuit)
                    {
                        cancelFlag = "0";
                    }
                    else
                    {
                        cancelFlag = "1";
                    }
                    if (this.outFeeMgr.UpdateConfirmInject(detail.Order.ID, detail.RecipeNO, detail.SequenceNO.ToString(), 1, cancelFlag) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.outFeeMgr.Err, "提示");
                        return -1;
                    }
                    #endregion
                    info.Item.InjectCount = info.Item.InjectCount;
                    //打吊瓶的才打印治疗单---先写死-------------此段程序不用,改为由操作员选择是否打印
                    if (info.Item.Order.Usage.ID == "03" || info.Item.Order.Usage.ID == "04")
                    {
                        alZLDPrint.Add(info);
                    }
                    alInjectPrint.Add(info);
                    this.lbLastOrder.Text = "今天最后一次注射号:" + info.OrderNO;

                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            if (this.isMessageInSave)
            {
                MessageBox.Show("保存成功!", "提示");
            }
            this.Clear();

            this.txtCardNo.SelectAll();
            this.txtCardNo.Text = "";
            this.txtCardNo.Focus();
            return 0;
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            if (this.fpInjectInfo_Sheet1.RowCount > 0)
            {
                this.fpInjectInfo_Sheet1.Rows.Remove(0, this.fpInjectInfo_Sheet1.RowCount);
            }
            this.txtOrder.Text = "";
            this.lblName.Text = "";
            this.lblSex.Text = "";
            this.lblAge.Text = "";
            this.dvNoPrint = new DataView();
            this.dvPrint = new DataView();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            if (this.fpInjectInfo_Sheet1.RowCount > 0)
            {
                this.fpInjectInfo_Sheet1.Rows.Remove(0, this.fpInjectInfo_Sheet1.RowCount);
            }
            string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【按照卡号查询数据，卡号：" + cardNo + "！】");

            //获取医生开立的处方信息（没有全部执行完的）
            DateTime dtFrom = this.dtpStart.Value.Date;
            if (this.isAutoRefreshPrint)
            {
                dtFrom = this.dtpAutoPrintBegin.Value;
            }
            else
            {
                dtFrom = this.dtpStart.Value;
            }
            DateTime dtTo = registerMgr.GetDateTimeFromSysDateTime().Date.AddDays(1);

            ArrayList al = this.outFeeMgr.QueryFeeItemListsAndQuitForZs(cardNo, dtFrom, dtTo);
            if (al == null)
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【卡号：" + cardNo + " 查询费用信息失败！】" + outFeeMgr.Err);
            }
            else
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始按照卡号查询打印，卡号：" + cardNo + "！】");
                this.Query(al);
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束按照卡号查询打印，卡号：" + cardNo + "！】");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        private void Query(ArrayList al)
        {
            if (al == null || al.Count == 0)
            {
                //MessageBox.Show("该患者没有需要确认的医嘱信息!", "提示");
                this.txtCardNo.Focus();
                return;
            }

            this.AddDetail(al);

            if (this.fpInjectInfo_Sheet1.RowCount <= 0)
            {
                //MessageBox.Show("该时间段内没有该患者信息!", "提示");
                this.txtCardNo.Focus();
                return;
            }

            this.SelectAll(true);
            this.SetComb();

            this.ShowColor();
            this.txtOrder.Focus();
            if (this.isUserOrderNumber)
            {
                this.SetInject();
            }

            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始按照卡号打印数据！】");
            //打印巡视卡
            if (this.isAutoPrint)
            {
                this.PrintInjectScoutCard();
            }
            else
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【因为isAutoPrint=false而未打印！】");
            }
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束按照卡号打印数据！】");

            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始自动保存数据！】");
            if (this.isAutoSave)
            {
                this.SelectAll(true);
                this.Save();
            }
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束自动保存数据！】");
        }

        /// <summary>
        /// 添加项目明细
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(ArrayList details)
        {
            if (this.fpInjectInfo_Sheet1.RowCount > 0)
            {
                this.fpInjectInfo_Sheet1.Rows.Remove(0, this.fpInjectInfo_Sheet1.RowCount);
            }

            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> tmpFeeList = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            if (details != null)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in details)
                {
                    //因收费有问题，把所有开立科室为空的字段暂时改为2001
                    if (detail.RecipeOper.Dept.ID == null || detail.RecipeOper.Dept.ID == "")
                    {
                        detail.RecipeOper.Dept.ID = "2001";
                    }
                    #region 过滤发票
                    if (detail.Invoice.ID != this.InvoiceNo && !string.IsNullOrEmpty(InvoiceNo))
                    {
                        continue;
                    }
                    #endregion

                    #region  判断是有效单还是作废单
                    if (this.isQuit)
                    {
                        //如果是作废单不显示有效记录和作废的正记录
                        if (detail.CancelType != FS.HISFC.Models.Base.CancelTypes.Canceled)
                        {
                            continue;
                        }
                        if (detail.TransType == FS.HISFC.Models.Base.TransTypes.Positive)
                        {
                            continue;
                        }
                        if (detail.ConfirmedInjectCount > 1)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        //否则显示有效记录
                        if (detail.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid)
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region 过滤开单科室
                    //判断是否过滤开单科室，不过滤继续
                    if (this.isFilterDoctDept)
                    {
                        //判断是否为空，为空默认全部科室，继续
                        if (!string.IsNullOrEmpty(this.doctDept))
                        {
                            if (!this.doctDept.Contains("'" + detail.RecipeOper.Dept.ID + "'"))
                            {
                                continue;
                            }
                        }
                    }
                    #endregion

                    #region 过滤扣库科室

                    //佛四根据注射单注射和发药，所以此处根据药品执行科室过滤
                    if(detail.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (detail.ExecOper.Dept.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
                        {
                            continue;
                        }
                    }


                    #endregion

                    #region  已确认院注次数不等于0的不显示
                    //如果是重打则不判断
                    if (!this.isReprint)
                    {
                        //已确认院注次数不等于0的不显示
                        if (detail.ConfirmedInjectCount != 0)
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region  判断非药品是否显示
                    if (isShowUnDrug)
                    {
                        if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                        {
                            if (!string.IsNullOrEmpty(detail.Order.Usage.ID))
                            {
                                if (this.unDrugUsage.Contains("'" + detail.Order.Usage.ID + "'"))
                                {
                                    tmpFeeList.Add(detail);
                                    continue;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (detail.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region 维护那些用法显示在界面上

                    //佛四根据注射单注射和发药，所以只要是本药房的，不管什么用法都打印出来

                    //if (!usage.Contains("'" + detail.Order.Usage.ID + "'"))
                    //{
                    //    continue;
                    //}
                    #endregion

                    #region 是否显示0次数的
                    if (!this.isRegNullNum && detail.InjectCount == 0)
                    {
                        continue;
                    }
                    #endregion

                    #region 作废，今天已经登记的QD不显示，注射两次的BID不显示，当前午别注射一次的BID不再显示。(根据今天的登记时间)
                    //DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(
                    //    this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd 00:00:00"));
                    ////{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                    //ArrayList alTodayInject = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO, detail.SequenceNO.ToString(), dt);
                    //FS.HISFC.Models.Order.Frequency frequence = this.dicFrequency[detail.Order.Frequency.ID];
                    //string[] injectTime = frequence.Time.Split('-');
                    ////当天的已经全部注射完毕后跳过
                    //if (alTodayInject.Count >= injectTime.Length)
                    //{
                    //    continue;
                    //}
                    //if (this.isShowAllInject)
                    //{
                    //    for (int i = alTodayInject.Count; i < injectTime.Length; i++)
                    //    {
                    //        FS.HISFC.Models.Fee.Outpatient.FeeItemList newDetail = detail.Clone();
                    //        newDetail.User03 = injectTime[i];
                    //        tmpFeeList.Add(newDetail);
                    //    }
                    //}
                    //else
                    //{
                    //    //未过上次注射时间的话不允许再次登记
                    //    if (alTodayInject.Count > 0)
                    //    {
                    //        DateTime lastInjectTime = FrameWork.Function.NConvert.ToDateTime(dt.ToString("yyyy-MM-dd ") + injectTime[alTodayInject.Count - 1] + ":00");
                    //        if (this.InjMgr.GetDateTimeFromSysDateTime() < lastInjectTime)
                    //        {
                    //            continue;
                    //        }
                    //    }
                    //    detail.User03 = injectTime[alTodayInject.Count];
                    //    tmpFeeList.Add(detail);
                    //}
                    #endregion
                    tmpFeeList.Add(detail);
                }

                //排序
                //tmpFeeList.Sort(new FeeItemListSort());
                //获取打印序号
                this.CreateInterface();
                if (this.IGetOrderNo != null)
                {
                    this.IGetOrderNo.SetPrintNo(new ArrayList(tmpFeeList.ToArray()));
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in tmpFeeList)
                {
                    this.AddDetail(feeItem);
                }
            }
        }


        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList info)
        {
            this.fpInjectInfo_Sheet1.Rows.Add(this.fpInjectInfo_Sheet1.RowCount, 1);
            int row = this.fpInjectInfo_Sheet1.RowCount - 1;
            this.fpInjectInfo_Sheet1.Rows[row].Tag = info;

            #region "窗口赋值"
            #region 获取皮试信息
            string strTest ="";
            //获取皮试信息
            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                drug = this.interPhaMgr.GetItem(info.Item.ID);
                if (drug == null)
                {
                    MessageBox.Show("获取药品皮试信息失败!");
                    this.fpInjectInfo_Sheet1.Rows.Remove(0, this.fpInjectInfo_Sheet1.RowCount);
                    return;
                }
                strTest = "否";
                if (drug.IsAllergy)
                {
                    strTest = "是";
                }
            }
            //
            #endregion

            info.Order.DoctorDept.Name = deptHelper.GetName(info.RecipeOper.Dept.ID);

            this.fpInjectInfo_Sheet1.SetValue(row, 1, "", false);//注射顺序号
            this.fpInjectInfo_Sheet1.SetValue(row, 2, info.InjectCount.ToString(), false);//院注次数
            this.fpInjectInfo_Sheet1.SetValue(row, 3, info.ConfirmedInjectCount.ToString(), false);//已经确认的院注次数
            this.fpInjectInfo_Sheet1.SetValue(row, 4, this.emplHelper.GetName(info.RecipeOper.ID), false);//开单医生
            this.fpInjectInfo_Sheet1.Cells[row, 4].Tag = info.Order.ReciptDoctor.ID;
            this.fpInjectInfo_Sheet1.SetValue(row, 5, info.Order.DoctorDept.Name, false);//科别
            this.fpInjectInfo_Sheet1.Cells[row, 5].Tag = info.Order.DoctorDept.ID;
            this.fpInjectInfo_Sheet1.SetValue(row, 6, info.Item.Name, false);//药品名称
            this.fpInjectInfo_Sheet1.Cells[row, 7].Tag = info.Order.Combo.ID;//组合号
            this.fpInjectInfo_Sheet1.SetValue(row, 8, info.Order.DoseOnce.ToString() + info.Order.DoseUnit, false);//每次量
            this.fpInjectInfo_Sheet1.SetValue(row, 9, info.Order.Frequency.ID, false);//频次
            this.fpInjectInfo_Sheet1.Cells[row, 9].Tag = info.Order.Frequency.ID.ToString();
            this.fpInjectInfo_Sheet1.SetValue(row, 10, info.Order.Usage.Name, false);//用法
            this.fpInjectInfo_Sheet1.SetValue(row, 11, strTest, false);//皮试？
            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                this.fpInjectInfo_Sheet1.Cells[row, 11].Tag = drug.IsAllergy.ToString().ToUpper();
            }
            orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();
            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                orderinfo = this.interOrderMgr.GetOneOrder(info.Patient.ID, info.Order.ID);
                if (orderinfo != null)
                {
                    this.fpInjectInfo_Sheet1.SetText(row, 12, string.IsNullOrEmpty(orderinfo.Memo) ? " " : orderinfo.Memo);

                    //if (orderinfo.HypoTest == 1)
                    //{
                    //    if (!drug.IsAllergy)
                    //    {
                    //        orderinfo.HypoTest = 0;//免试的值为零
                    //    }
                    //}
                    this.fpInjectInfo_Sheet1.Cells[row, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());
                    this.fpInjectInfo_Sheet1.Cells[row, 11].Tag = orderinfo;
                }
                else
                {
                    orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();
                    if (drug.IsAllergy)
                    {
                        orderinfo.Item = drug;
                        orderinfo.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                    }
                    else
                    {
                        orderinfo.HypoTest = 0;
                    }

                    this.fpInjectInfo_Sheet1.Cells[row, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());
                    this.fpInjectInfo_Sheet1.Cells[row, 11].Tag = orderinfo;

                }
            }
            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            this.fpInjectInfo_Sheet1.Cells[row, 13].Text = info.User03;

            #endregion
        }

        /// <summary>
        /// 获取皮试信息
        /// </summary>
        /// <param name="hytestID"></param>
        /// <returns></returns>
        private string GetHyTestInfo(string hytestID)
        {
            switch (hytestID)
            {
                case "1":
                    return "免试";
                case "2":
                    return "需皮试";
                case "3":
                    return "[阳]";
                case "4":
                    return "[阴]";
                case "0":
                default:
                    return "否";
            }
        }

        /// <summary>
        /// 设置组合号
        /// </summary>
        private void SetComb()
        {
            int myCount = this.fpInjectInfo_Sheet1.RowCount;
            int i;
            //第一行
            this.fpInjectInfo_Sheet1.SetValue(0, 7, "┓");
            //最后行
            this.fpInjectInfo_Sheet1.SetValue(myCount - 1, 7, "┛");
            //中间行
            for (i = 1; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString();
                string priorRowCombNo = this.fpInjectInfo_Sheet1.Cells[prior, 7].Tag.ToString();
                string nextRowCombNo = this.fpInjectInfo_Sheet1.Cells[next, 7].Tag.ToString();

                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                string currentRowInjectTime = this.fpInjectInfo_Sheet1.Cells[i, 13].Text.ToString();
                string priorRowInjectTime = this.fpInjectInfo_Sheet1.Cells[prior, 13].Text.ToString();
                string nextRowInjectTime = this.fpInjectInfo_Sheet1.Cells[next, 13].Text.ToString();

                #region """""
                bool bl1 = true;
                bool bl2 = true;
                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                    bl1 = false;
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                    bl2 = false;
                //  ┃
                if (bl1 && bl2)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "┃");
                }
                //  ┛
                if (bl1 && !bl2)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "┛");
                }
                //  ┓
                if (!bl1 && bl2)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "┓");
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "");
                }
                #endregion
            }
            //把没有组号的去掉
            for (i = 0; i < myCount; i++)
            {
                if (this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() == "")
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "");
                }
            }
            //判断首末行 有组号，且只有自己一组数据的情况
            if (myCount == 1)
            {
                this.fpInjectInfo_Sheet1.SetValue(0, 7, "");
            }
            //只有首末两行，那么还要判断组号啊
            if (myCount == 2)
            {
                if (this.fpInjectInfo_Sheet1.Cells[0, 7].Tag.ToString().Trim() != this.fpInjectInfo_Sheet1.Cells[1, 7].Tag.ToString().Trim())
                {
                    this.fpInjectInfo_Sheet1.SetValue(0, 7, "");
                    this.fpInjectInfo_Sheet1.SetValue(1, 7, "");
                }
                //防止一个药bid组合在一起
                if (this.fpInjectInfo_Sheet1.Cells[0, 13].Text.ToString().Trim() != this.fpInjectInfo_Sheet1.Cells[1, 13].Text.ToString().Trim())
                {
                    this.fpInjectInfo_Sheet1.SetValue(0, 7, "");
                    this.fpInjectInfo_Sheet1.SetValue(1, 7, "");
                }
            }
            if (myCount > 2)
            {
                if (this.fpInjectInfo_Sheet1.GetValue(1, 7).ToString() != "┃"
                    && this.fpInjectInfo_Sheet1.GetValue(1, 7).ToString() != "┛")
                {
                    this.fpInjectInfo_Sheet1.SetValue(0, 7, "");
                }
                if (this.fpInjectInfo_Sheet1.GetValue(myCount - 2, 7).ToString() != "┃"
                    && this.fpInjectInfo_Sheet1.GetValue(myCount - 2, 7).ToString() != "┓")
                {
                    this.fpInjectInfo_Sheet1.SetValue(myCount - 1, 7, "");
                }
            }

        }

        /// <summary>
        /// 打印治疗单
        /// </summary>
        private void Print()
        {
            if (this.alZLDPrint == null || this.alZLDPrint.Count <= 0)
            {
                MessageBox.Show("没有需要打印的数据!");
                return;
            }
            FS.HISFC.Components.Nurse.Print.ucPrintCure uc = new FS.HISFC.Components.Nurse.Print.ucPrintCure();
            uc.Init(alZLDPrint);

            if (this.IsFirstTime)
            {
                FS.HISFC.Components.Nurse.Print.ucPrintInject uc2 = new FS.HISFC.Components.Nurse.Print.ucPrintInject();
                uc2.Init(alInjectPrint);
            }
            alZLDPrint.Clear();
            alInjectPrint.Clear();
        }

        /// <summary>
        /// 选择相同组号项目
        /// </summary>
        /// <param name="isSelect"></param>
        private void SelectedComb(bool isSelect)
        {
            int row = this.fpInjectInfo_Sheet1.ActiveRowIndex;
            string combID = this.fpInjectInfo_Sheet1.Cells[row, 7].Tag.ToString();
            //患者可以登记全天所有注射处方
            string injectTime = this.fpInjectInfo_Sheet1.Cells[row, 13].Text;
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                if (this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() == combID && this.fpInjectInfo_Sheet1.Cells[i, 13].Text == injectTime)
                {
                    this.fpInjectInfo_Sheet1.Cells[i, 0].Value = isSelect;
                }
            }
        }

        /// <summary>
        /// 修改皮试信息
        /// </summary>
        private void ModifyHytest()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.fpInjectInfo_Sheet1.Cells[i, 0].Value);
                if (isSelected)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.fpInjectInfo_Sheet1.Cells[i, 11].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (orderinfo.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest)
                    {
                        continue;
                    }
                    al.Add(orderinfo);
                }

            }

            if (al.Count == 0)
            {
                return;
            }
            FS.HISFC.Components.Nurse.Forms.frmHypoTest frmHypoTest = new FS.HISFC.Components.Nurse.Forms.frmHypoTest();
            frmHypoTest.AlOrderList = al;
            DialogResult d = frmHypoTest.ShowDialog();
            if (d == DialogResult.OK)
            {
                for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
                {
                    bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.fpInjectInfo_Sheet1.Cells[i, 0].Value);
                    if (!isSelected)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.fpInjectInfo_Sheet1.Cells[i, 11].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    this.fpInjectInfo_Sheet1.Cells[i, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());
                }
            }
        }

        #endregion

        /// <summary>
        /// 卡号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("请输入病历号!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }
                string strCardNO = this.txtCardNo.Text.Trim();//.PadLeft(10, '0');
                int iTemp = interFeeMgr.ValidMarkNO(strCardNO, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("无效卡号，请联系管理员！");
                    return;
                }
                string cardNo = objCard.Patient.PID.CardNO;

                dvQuery = new DataView();
                DataSet ds = new DataSet();
                int rev = this.registerMgr.QueryInject(cardNo, this.dtpStart.Value, this.dtpEnd.Value, true, dept, unDrugUsage, drugUsage, ref ds);
                if (rev == -1)
                {
                    MessageBox.Show(this.interRegMgr.Err);
                    return;
                }

                this.fpQuery_Sheet1.Rows.Count = 0;

                this.SetPatientList(ds, ref dvQuery, this.fpQuery_Sheet1);

                this.tabControl1.SelectedTab = tbQuery;
            }
        }



        void fpQuery_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string cardNo = this.fpQuery_Sheet1.Cells[e.Row, 3].Text;
            this.InvoiceNo = this.fpQuery_Sheet1.Cells[e.Row, 1].Tag.ToString();
            //判断当前打印的是否是退费单
            string isCancel = this.fpQuery_Sheet1.Cells[e.Row, 0].Tag.ToString();
            if (isCancel == "0")
            {
                isQuit = true;
            }
            else
            {
                isQuit = false;
            }
            this.txtCardNo.Text = cardNo;
            cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
            ArrayList alRegs = this.interRegMgr.Query(cardNo, this.dtpAutoPrintBegin.Value.AddDays(0 - queryRegDays));
            if (alRegs == null || alRegs.Count == 0)
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                this.txtCardNo.Focus();
                return;
            }
            reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
            if (reg == null || reg.ID == "")
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");

                this.txtCardNo.Focus();
                return;
            }

            this.txtCardNo.Text = cardNo;
            this.ShowPatient(reg, fpQuery_Sheet1.Cells[e.Row, (int)EnumPatientListColumn.Name].Tag.ToString());
            this.isReprint = true;
            this.isAutoSave = false;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【设置打印开关:isAutoPrint = true】");
            this.isAutoPrint = true;
            this.Query();
            this.txtCardNo.Text = "";
            this.InvoiceNo = "";
        }

        void fpQuery_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始点击患者查询费用信息，此时不自动打印】");
            string cardNo = this.fpQuery_Sheet1.Cells[e.Row, 3].Text;
            this.InvoiceNo = this.fpQuery_Sheet1.Cells[e.Row, 1].Tag.ToString();

            //判断当前打印的是否是退费单
            string isCancel = this.fpQuery_Sheet1.Cells[e.Row, 0].Tag.ToString();
            if (isCancel == "0")
            {
                isQuit = true;
            }
            else
            {
                isQuit = false;
            }
            this.txtCardNo.Text = cardNo;
            cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
            ArrayList alRegs = this.interRegMgr.Query(cardNo, this.dtpAutoPrintBegin.Value.AddDays(0 - queryRegDays));
            if (alRegs == null || alRegs.Count == 0)
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                this.txtCardNo.Focus();
                return;
            }
            reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
            if (reg == null || reg.ID == "")
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");

                this.txtCardNo.Focus();
                return;
            }

            this.txtCardNo.Text = cardNo;
            this.ShowPatient(reg, fpQuery_Sheet1.Cells[e.Row, (int)EnumPatientListColumn.Name].Tag.ToString());
            this.isReprint = true;
            this.isAutoSave = false;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【设置打印开关:isAutoPrint = true】");
            this.isAutoPrint = false;
            this.Query();
            this.txtCardNo.Text = "";
            this.InvoiceNo = "";
            this.isAutoSave = true;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【设置打印开关:isAutoPrint = true】");
            this.isAutoPrint = true;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束查询患者费用信息】");
        }

        /// <summary>
        /// 注射顺序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
        }

        /// <summary>
        /// 监控按键事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            int altKey = Keys.Alt.GetHashCode();

            if (keyData == Keys.F1)
            {
                this.SelectAll(true);
                return true;
            }
            if (keyData == Keys.F2)
            {
                this.SelectAll(false);
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.S.GetHashCode())
            {
                if (this.Save() == 0)
                {
                    this.Print();
                }
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.Q.GetHashCode())
            {
                this.Query();
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.P.GetHashCode())
            {
                //
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 组合勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.fpInjectInfo_Sheet1.ActiveRowIndex;
            bool isSelect = Convert.ToBoolean(this.fpInjectInfo_Sheet1.Cells[row, 0].Value);
            this.SelectedComb(isSelect);
        }

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {

            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint);
                return types;
            }
        }

        #endregion


        #region 自动刷新打印

        /// <summary>
        /// 自动刷新打印设置
        /// </summary>
        /// <param name="isAutoPrint"></param>
        private void AutoPrintSet(bool isAutoRefreshPrint)
        {
            this.isAutoRefreshPrint = isAutoRefreshPrint;
            //this.gbxQuery.Visible = !isAutoRefreshPrint;
            this.neuGroupBox2.Visible = isAutoRefreshPrint;
            this.timer1.Enabled = isAutoRefreshPrint;
            this.timer1.Interval = this.freshTimes * 1000;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【设置自动刷新功能，刷新间隔为" + freshTimes.ToString() + "！】");

            this.cbxAutoPringDate.Enabled = isAutoRefreshPrint;
            this.cbxAutoPringDate.Checked = isAutoRefreshPrint;
            this.dtpAutoPrintBegin.Enabled = isAutoRefreshPrint;

            if (isAutoRefreshPrint)
            {
                this.toolBarService.SetToolButtonEnabled("暂停刷新", true);
                this.toolBarService.SetToolButtonEnabled("启动刷新", false);
                this.lblNote.Text = "此时正在自动刷新";
            }
            else
            {
                this.toolBarService.SetToolButtonEnabled("暂停刷新", false);
                this.toolBarService.SetToolButtonEnabled("启动刷新", true);
                this.lblNote.Text = "此时暂停自动刷新";
            }

            //如果是管理员则不自动打印
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.timer1.Enabled = false;
                this.lblNote.Text = "此时为管理员进入不自动刷新，请按查询按钮手动刷新！";
            }
        }

        /// <summary>
        /// 自动刷新功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始刷新！】");

            if (!this.isAutoRefreshPrint)
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【未启用自动刷新，退出！】");
                return;
            }
            if (!this.isCanRefresh)
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【不能刷新，退出（因为正在打印）！】");
                return;
            }

            this.BatchQuery();
            
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束刷新！】");
        }

        /// <summary>
        /// 批量查询打印
        /// </summary>
        /// <returns></returns>
        private int BatchQuery()
        {
            this.txtCardNo.Text = "";
            this.QueryPatient();

            //打印
            while (this.sheetView_NoPrint.RowCount > 0)
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始赋值打印！】");
                this.isCanRefresh = false;
                try
                {
                    this.sheetView_NoPrint.ActiveRowIndex = this.sheetView_NoPrint.RowCount - 1;

                    string cardNo = this.sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, 3].Text;

                    if (sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, 1].Tag != null)
                    {
                        InvoiceNo = sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, 1].Tag.ToString();
                    }

                    //判断当前打印的是否是退费单
                    string isCancel = this.sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, 0].Tag.ToString();
                    if (isCancel == "0")
                    {
                        isQuit = true;
                    }
                    else
                    {
                        isQuit = false;
                    }

                    this.txtCardNo.Text = cardNo;
                    cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

                    ArrayList alRegs = this.interRegMgr.Query(cardNo, this.dtpAutoPrintBegin.Value.AddDays(0 - queryRegDays));
                    if (alRegs == null || alRegs.Count == 0)
                    {
                        //MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                        this.isCanRefresh = true;
                        this.txtCardNo.Focus();
                        return -1;
                    }

                    reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
                    if (reg == null || reg.ID == "")
                    {
                        //MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                        this.isCanRefresh = true;
                        this.txtCardNo.Focus();
                        return -1;
                    }

                    this.txtCardNo.Text = cardNo;
                    this.ShowPatient(reg, this.sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, (int)EnumPatientListColumn.Name].ToString());
                    this.isReprint = false;
                    this.isAutoSave = true;
                    FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【设置打印开关:isAutoPrint = true】");
                    this.isAutoPrint = true;

                    this.Query();
                    this.txtCardNo.Text = "";
                    InvoiceNo = "";
                    this.sheetView_NoPrint.RemoveRows(this.sheetView_NoPrint.RowCount - 1, 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【赋值打印出现错误:】" + ex.Message);
                    this.isCanRefresh = true;
                    return -1;
                }
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束赋值打印！】");
            }
            this.isCanRefresh = true;
            return 1;
        }

        /// <summary>
        /// 药品用法
        /// </summary>
        string drugUsage = "";

        /// <summary>
        /// 开单科室过滤
        /// </summary>
        string dept = "'All'";

        /// <summary>
        /// 查询注射患者列表
        /// </summary>
        private void QueryPatient()
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始查询患者列表！】");

            this.Clear();

            DateTime begin = new DateTime();
            //如果自动打印的，开始时间可以设定
            if (this.cbxAutoPringDate.Checked && this.isAutoRefreshPrint)
            {
                begin = this.dtpAutoPrintBegin.Value;
            }
            else
            {
                begin = this.InjMgr.GetDateTimeFromSysDateTime();
            }

            #region 设置开单科室
            
            dept = "'All'";

            //判断是否过滤开单科室，不过滤继续
            if (this.isFilterDoctDept)
            {
                //判断是否为空，为空默认全部科室，继续
                if (!string.IsNullOrEmpty(this.doctDept))
                {
                    dept = this.doctDept;
                }
            }
            #endregion

            #region 设置非药品用法过滤

            string unDrugUsage = "'All'";
            //判断是否判断非药品
            if (isShowUnDrug)
            {
                if (!string.IsNullOrEmpty(this.unDrugUsage))
                {
                    unDrugUsage = this.unDrugUsage;
                }
            }
            #endregion

            #region 设置药品用法过滤

            //判断是否判断非药品
            if (!string.IsNullOrEmpty(this.usage))
            {
                drugUsage = this.usage;
            }
            else
            {
                MessageBox.Show("请维护要打印的药品用法");
                return;
            }
            #endregion

            #region 患者列表

            #region 已打印

            dvPrint = new DataView();
            DataSet ds = new DataSet();
            int rev = 0;
            rev = this.registerMgr.QueryInject("All", begin, this.registerMgr.GetDateTimeFromSysDateTime(), true, dept, unDrugUsage, drugUsage, ref ds);
            if (rev == -1)
            {
                MessageBox.Show(this.interRegMgr.Err);
                return;
            }
            this.SetPatientList(ds, ref dvPrint, this.sheetView_Print);
            #endregion

            #region 未打印
            dvNoPrint = new DataView();
            ds = new DataSet();
            rev = this.registerMgr.QueryInject("All", begin, registerMgr.GetDateTimeFromSysDateTime(), false, dept, unDrugUsage,drugUsage, ref ds);
            if (rev == -1)
            {
                MessageBox.Show(this.interRegMgr.Err);
                return;
            }
            this.SetPatientList(ds, ref dvNoPrint, this.sheetView_NoPrint);
            #endregion

            #endregion

            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束查询患者列表！】");
        }

        /// <summary>
        /// 患者列表赋值
        /// </summary>
        /// <param name="patientList"></param>
        private void SetPatientList(DataSet ds, ref DataView dv, FarPoint.Win.Spread.SheetView sheet)
        {
            sheet.RowCount = 0;
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return;
            }

            //sheet.DataSource = dv;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sheet.AddRows(sheet.RowCount, 1);
                if (dr[0].ToString() == "1")
                {
                    sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.CancleFlag].Text = "";
                }
                else
                {
                    sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.CancleFlag].Text = "退";
                }
                sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.CancleFlag].Tag = dr[0].ToString();
                sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.Name].Text = dr[1].ToString();
                sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.Sex].Text = dr[2].ToString();
                sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.CardNo].Text = dr[3].ToString();
                if (ds.Tables[0].Columns.Count >= 4)
                {
                    sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.Name].Tag = dr[4].ToString();
                    //sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.InvoiceNo].Tag = dr[4].ToString();
                }
            }
            //dv = ds.Tables[0].DefaultView;
        }

        /// <summary>
        /// 过滤患者列表内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            return;
            if (string.IsNullOrEmpty(this.txtFilter.Text))
            {
                return;
            }

            this.txtFilter.Text = this.txtFilter.Text.Trim();
            string tt = "0123456789";
            string filter = "";
            //卡号过滤
            if (tt.Contains(this.txtFilter.Text.Substring(0, 1)))
            {
                filter = "卡号 like '%" + this.txtFilter.Text.TrimStart('0') + "%'";
            }
            //姓名过滤
            else
            {
                filter = "姓名 like '%" + this.txtFilter.Text.TrimStart('0') + "%'";
            }

            if (this.tabControl1.SelectedTab == this.tbPagNoPrint)
            {
                if (this.dvNoPrint == null || this.dvNoPrint.Table.Rows.Count == 0)
                {
                    return;
                }

                if (string.IsNullOrEmpty(this.txtFilter.Text))
                {
                    this.dvNoPrint.RowFilter = "";
                    return;
                }
                dvNoPrint.RowFilter = filter;
            }
            else if (this.tabControl1.SelectedTab == this.tbPagPrint)
            {
                if (this.dvPrint == null || this.dvPrint.Table.Rows.Count == 0)
                {
                    return;
                }

                if (string.IsNullOrEmpty(this.txtFilter.Text))
                {
                    this.dvPrint.RowFilter = "";
                    return;
                }
                dvPrint.RowFilter = filter;
            }
        }

        /// <summary>
        /// 双击已打印患者列表，自动打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPrinted_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string cardNo = this.sheetView_Print.Cells[e.Row, 3].Text;
            this.InvoiceNo = this.sheetView_Print.Cells[e.Row, 1].Tag.ToString();
            //判断当前打印的是否是退费单
            string isCancel = this.sheetView_Print.Cells[e.Row, 0].Tag.ToString();
            if (isCancel == "0")
            {
                isQuit = true;
            }
            else
            {
                isQuit = false;
            }
            this.txtCardNo.Text = cardNo;
            cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
            ArrayList alRegs = this.interRegMgr.Query(cardNo, this.dtpAutoPrintBegin.Value.AddDays(0 - queryRegDays));
            if (alRegs == null || alRegs.Count == 0)
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                this.txtCardNo.Focus();
                return;
            }
            reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
            if (reg == null || reg.ID == "")
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");

                this.txtCardNo.Focus();
                return;
            }

            this.txtCardNo.Text = cardNo;
            this.ShowPatient(reg, sheetView_Print.Cells[e.Row, (int)EnumPatientListColumn.Name].Tag.ToString());
            this.isReprint = true;
            this.isAutoSave = false;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【设置打印开关:isAutoPrint = true】");
            this.isAutoPrint = true;
            this.Query();
            this.txtCardNo.Text = "";
            this.InvoiceNo = "";
        }

        /// <summary>
        /// 单击已打印患者列表，查询显示内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPrinted_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【开始点击患者查询费用信息，此时不自动打印】");
            string cardNo = this.sheetView_Print.Cells[e.Row, 3].Text;
            this.InvoiceNo = this.sheetView_Print.Cells[e.Row, 1].Tag.ToString();

            //判断当前打印的是否是退费单
            string isCancel = this.sheetView_Print.Cells[e.Row, 0].Tag.ToString();
            if (isCancel == "0")
            {
                isQuit = true;
            }
            else
            {
                isQuit = false;
            }
            this.txtCardNo.Text = cardNo;
            cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
            ArrayList alRegs = this.interRegMgr.Query(cardNo, this.dtpAutoPrintBegin.Value.AddDays(0 - queryRegDays));
            if (alRegs == null || alRegs.Count == 0)
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                this.txtCardNo.Focus();
                return;
            }
            reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
            if (reg == null || reg.ID == "")
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");

                this.txtCardNo.Focus();
                return;
            }

            this.txtCardNo.Text = cardNo;
            this.ShowPatient(reg, sheetView_Print.Cells[e.Row, (int)EnumPatientListColumn.Name].Tag.ToString());
            this.isReprint = true;
            this.isAutoSave = false;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【设置打印开关:isAutoPrint = true】");
            this.isAutoPrint = false;
            this.Query();
            this.txtCardNo.Text = "";
            this.InvoiceNo = "";
            this.isAutoSave = true;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【设置打印开关:isAutoPrint = true】");
            this.isAutoPrint = true;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("【结束查询患者费用信息】");
        }

        #endregion

        /// <summary>
        /// 过滤患者列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtFilter.Text))
                {
                    return;
                }
                this.fpPrinted.Sheets[1].ClearSelection();

                if (!"0123456789".Contains(txtFilter.Text.Substring(0, 1)))
                {
                    #region 按照姓名查询

                    for (int i = 0; i < this.fpPrinted.Sheets[1].RowCount; i++)
                    {
                        if (fpPrinted.Sheets[1].Cells[i, (int)EnumPatientListColumn.Name].Text == txtFilter.Text.Trim()
                            || fpPrinted.Sheets[1].Cells[i, (int)EnumPatientListColumn.Name].Text == txtFilter.Text.TrimStart('0'))
                        {
                            this.fpPrinted.Sheets[1].ActiveRowIndex = i;
                            this.fpPrinted.Sheets[1].AddSelection(i, (int)EnumPatientListColumn.Name, 1, fpPrinted.Sheets[1].ColumnCount);
                            this.fpPrinted_CellClick(null, new FarPoint.Win.Spread.CellClickEventArgs(new FarPoint.Win.Spread.SpreadView(), i, (int)EnumPatientListColumn.Name, 0, 0, MouseButtons.Left, false, false));
                            this.fpPrinted.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                            return;
                        }
                    }

                    #endregion
                }
                else
                {
                    #region 按照卡号查询

                    string strCardNO = this.txtFilter.Text.Trim();//.PadLeft(10, '0');
                    int iTemp = interFeeMgr.ValidMarkNO(strCardNO, ref objCard);
                    if (iTemp <= 0 || objCard == null)
                    {
                        MessageBox.Show("无效卡号，请联系管理员！");
                        return;
                    }
                    string cardNo = objCard.Patient.PID.CardNO;

                    for (int i = 0; i < this.fpPrinted.Sheets[1].RowCount; i++)
                    {
                        if (fpPrinted.Sheets[1].Cells[i, (int)EnumPatientListColumn.CardNo].Text == cardNo
                            || fpPrinted.Sheets[1].Cells[i, (int)EnumPatientListColumn.CardNo].Text == cardNo.TrimStart('0'))
                        {
                            this.fpPrinted.Sheets[1].ActiveRowIndex = i;
                            this.fpPrinted.Sheets[1].AddSelection(i, (int)EnumPatientListColumn.CardNo, 1, fpPrinted.Sheets[1].ColumnCount);
                            this.fpPrinted_CellClick(null, new FarPoint.Win.Spread.CellClickEventArgs(new FarPoint.Win.Spread.SpreadView(), i, (int)EnumPatientListColumn.CardNo, 0, 0, MouseButtons.Left, false, false));
                            this.fpPrinted.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                            return;
                        }
                    }
                    #endregion
                }
            }
        }
    }

    /// <summary>
    /// 患者列表列设置
    /// </summary>
    public enum EnumPatientListColumn
    {
        /// <summary>
        /// 退费标记
        /// </summary>
        CancleFlag,
        /// <summary>
        /// 姓名
        /// </summary>
        Name,
        /// <summary>
        /// 性别
        /// </summary>
        Sex,
        /// <summary>
        /// 卡号
        /// </summary>
        CardNo,
        /// <summary>
        /// 发票号
        /// </summary>
        InvoiceNo
    }
    
    /// <summary>
    /// 排序
    /// {24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
    /// </summary>
    public class FeeItemListSort : IComparer<FS.HISFC.Models.Fee.Outpatient.FeeItemList>
    {
        public int Compare(FS.HISFC.Models.Fee.Outpatient.FeeItemList x, FS.HISFC.Models.Fee.Outpatient.FeeItemList y)
        {
            //先按照处方排序
            if (x.RecipeNO != y.RecipeNO)
            {
                return y.RecipeNO.CompareTo(x.RecipeNO);
            }
            //按注射时间排序
            if (x.User03 != y.User03)
            {
                string a = x.User03;
                string b = y.User03;
                if (a.Length < "12:00".Length)
                {
                    a = a.PadLeft("12:00".Length, '0');
                }
                if (b.Length < "12:00".Length)
                {
                    b = b.PadLeft("12:00".Length, '0');
                }
                return -b.CompareTo(a);
            }

            //按组合号排序
            if (x.Order.Combo.ID != y.Order.Combo.ID)
            {
                return y.Order.Combo.ID.CompareTo(x.Order.Combo.ID);
            }
            //处方内序号
            if (x.Order.SequenceNO != y.Order.SequenceNO)
            {
                return y.SequenceNO.CompareTo(x.SequenceNO);
            }
            //药品编码
            //if (x.Item.ID != y.Item.ID)
            //{
            //    return y.Item.ID.CompareTo(x.Item.ID);
            //}
            return 0;
        }
    }
}
