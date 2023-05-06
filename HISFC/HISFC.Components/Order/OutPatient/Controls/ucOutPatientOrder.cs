using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.HISFC.BizProcess.Interface.Order;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// 门诊医生站
    /// </summary>
    public partial class ucOutPatientOrder : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOutPatientOrder()
        {
            InitializeComponent();
            if (this.DesignMode) return;

            this.contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        #region 变量

        #region 附材相关

        /// <summary>
        /// 是否处理检验附材
        /// </summary>
        protected bool isDealSubtbl = false;

        /// <summary>
        /// 非药品带附材是否单独处理加急医嘱
        /// </summary>
        protected bool isDealEmrOrderSubtblSpecially = false;

        /// <summary>
        /// 加急医嘱执行方式
        /// </summary>
        protected string emrSubtblUsage = "";

        /// <summary>
        /// 检验医嘱合管的执行方式
        /// </summary>
        protected string ULOrderUsage = "";


        /// <summary>
        /// 每次量限制值（最小单位的数量)
        /// </summary>
        private decimal doceOnceLimit = -1;

        #endregion

        #region 变量

        /// <summary>
        /// 挂号有效天数
        /// </summary>
        public int validRegDays = 1;

        /// <summary>
        /// 存储医嘱内容
        /// </summary>
        protected DataSet dtOrder = null;

        /// <summary>
        /// 存储医嘱内容
        /// </summary>
        protected DataView dvOrder = null;

        /// <summary>
        /// 是否整在编辑组套
        /// </summary>
        protected bool EditGroup = false;

        /// <summary>
        /// 全部医嘱信息
        /// </summary>
        public ArrayList alAllOrder = new ArrayList();

        /// <summary>
        /// 当前的医嘱信息
        /// </summary>
        protected FS.HISFC.Models.Order.OutPatient.Order currentOrder = null;

        /// <summary>
        /// 当前患者
        /// </summary>
        protected FS.HISFC.Models.Registration.Register currentPatientInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// 药品业务
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 当前诊台
        /// </summary>
        protected FS.FrameWork.Models.NeuObject currentRoom = null;

        /// <summary>
        /// 界面显示配置文件
        /// </summary>
        private string SetingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\clinicordersetting.xml";

        /// <summary>
        /// 悬停提示
        /// </summary>
        ToolTip tooltip = new ToolTip();

        /// <summary>
        /// 处方打印接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.IRecipePrint iRecipePrint = null;

        /// <summary>
        /// 右键菜单
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = null;

        /// <summary>
        /// 存储组合变化的医嘱的哈希表
        /// </summary>
        private Hashtable hsComboChange = new Hashtable();

        /// <summary>
        /// 错误提示信息
        /// </summary>
        private string errInfo = "";

        /// <summary>
        /// 草药开立界面
        /// </summary>
        FS.HISFC.Components.Order.Controls.ucHerbalOrder ucHerbal = null;

        #region 补挂号用

        /// <summary>
        /// 急诊号对应的诊查费项目
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug emergRegItem = null;

        /// <summary>
        /// 医生职称对应的诊查费项目
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug diagItem = null;

        /// <summary>
        /// 挂号费差额项目
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug diffDiagItem = null;

        /// <summary>
        /// 补收的挂号费项目
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug regItem = null;

        /// <summary>
        /// 免挂号费的科室
        /// </summary>
        private Hashtable hsNoSupplyRegDept = new Hashtable();

        /// <summary>
        /// 本次就诊已开立的医嘱
        /// </summary>
        private ArrayList SameOrderList = new ArrayList();

        /// <summary>
        /// 过往就诊已收费但未执行的医嘱
        /// </summary>
        private ArrayList LastOrderList = new ArrayList();

        /// <summary>
        /// 当前操作员
        /// 数据库重取的
        /// </summary>
        FS.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 补收项目列表
        /// </summary>
        private ArrayList alSupplyFee = new ArrayList();

        /// <summary>
        /// 是否启用分诊系统 1 启用 其他 不启用
        /// </summary>
        private bool isUseNurseArray;

        /// <summary>
        /// 门诊医生是否自动打印处方
        /// </summary>
        private bool isAutoPrintRecipe = false;

        /// <summary>
        /// 合同单位帮助类
        /// </summary>
        //private FS.FrameWork.Public.ObjectHelper pactHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 检查目的1
        /// </summary>
        public Dictionary<string, string> PurPose = null;

        #endregion

        /// <summary>
        /// 附材处理模式，0 保存后自动带出；1 界面点击计算，显示在界面上允许修改
        /// </summary>
        private int dealSublMode = -1;

        /// <summary>
        /// 是否已经初始化挂号费等项目？ 为了加载快一些，把初始化挂号费放到保存或诊出的时候
        /// </summary>
        private bool isInitSupplyItem = false;

        /// <summary>
        /// 是否自动带出挂号费和诊金、差额等
        /// 0 不带出；1 自动带出；2 只补收诊金和挂号费；3 只补收差额费
        /// </summary>
        private int isAutoAddSupplyFee = 1;

        #endregion

        #region 控制参数

        /// <summary>
        /// 是否新加，修改时间
        /// </summary>
        protected bool dirty = false;

        /// <summary>
        /// 是否忽略系统类别 允许西药、成药组合、分在同一处方
        /// </summary>
        bool isDecSysClassWhenGetRecipeNO = false;

        /// <summary>
        /// 是否已查询参数isDecSysClassWhenGetRecipeNO
        /// </summary>
        bool ynGetSysClassControl = false;

        /// <summary>
        /// 门诊医生站是否允许修改合同单位信息
        /// </summary>
        private bool isAllowChangePactInfo = false;

        /// <summary>
        /// 是否允许修改每次开立保存时的处方合同单位信息
        /// houwb 医保方自费方问题 {A1FE7734-267C-43f1-A824-BF73B482E0B2}
        /// </summary>
        private bool isAllowChangeRecipePactInfo = false;

        /// <summary>
        /// 是否保存医嘱修改纪录
        /// </summary>
        protected bool isSaveOrderHistory = false;

        /// <summary>
        /// 是否通过seeno计算费用
        /// </summary>
        protected bool isCountFeeBySeeNo = false;

        /// <summary>
        /// 是否在界面上显示有重复项目
        /// </summary>
        protected bool isShowRepeatItemInScreen = false;

        /// <summary>
        /// 是否提示同一次就诊有开立重复项目或未执行项目
        /// </summary>
        protected bool isShowSameOrder = false;

        /// <summary>
        /// 是否在药品名称列显示规格和价格
        /// 0 都不显示，1 显示规格，2 显示价格，3 规格、价格都显示
        /// </summary>
        private int isShowSpecsAndPrice = 0;


        /// <summary>
        /// 是否允许医生开库存不足的药品：0不允许，1 提示，2 允许
        /// </summary>
        private int isCheckDrugStock = 0;

        /// <summary>
        /// 是否默认选中的天数、频次、用法全部修改，并重新计算总量
        /// 000 三位数字分别表示：天数、频次、用法
        /// </summary>
        private string isChangeAllSelect = "-1";

        /// <summary>
        /// 是否在开立之前添加挂号费项目 (配合dealSublMode=1 界面显示附材模式 使用)
        /// </summary>
        private bool isAddRegSubBeforeAddOrder = false;

        /// <summary>
        /// 当前账户余额显示信息
        /// </summary>
        private string vacancyDisplay = "";


        /// <summary>
        /// 是否医生站预扣库存
        /// </summary>
        private bool isPreUpdateStockinfoByOrder = false;

        /// <summary>
        /// 门诊医生站皮试处理模式（0 不提示皮试信息 1：提示是否2：弹出界面选择）
        /// </summary>
        private string hypotestMode = "1";

        /// <summary>
        /// 门诊账户:true终端收费 false门诊收费
        /// </summary>
        private bool isAccountTerimal = false;

        /// <summary>
        /// 是否门诊账户流程
        /// </summary>
        private bool isAccountMode = false;

        /// <summary>
        /// 保存处方时是否判断诊断
        /// 0 不判断；1 判断药品；2 判断药品和非药品；3 判断药品、非药品、诊出
        /// </summary>
        private string isJudgeDiagnose = "0";

        /// <summary>
        /// 是否可以修改院注
        /// </summary>
        private bool isCanModifiedInjectNum = true;

        /// <summary>
        /// 药品和非药品的价格为0是否收费：0不收取；1收取。默认值为0不收取
        /// </summary>
        private string isFeeWhenPriceZero = "-1";

        #endregion

        #region 接口

        /// <summary>
        /// 接入电子申请单
        /// </summary>
        //protected FS.ApplyInterface.HisInterface PACSApplyInterface = null;

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 合同单位校验接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Common.ICheckPactInfo iCheckPactInfo = null;

        /// <summary>
        /// 医嘱信息变更接口
        /// {48E6BB8C-9EF0-48a4-9586-05279B12624D}
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IAlterOrder IAlterOrderInstance = null;

        /// <summary>
        /// 检查申请单打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Common.ICheckPrint checkPrint = null;

        /// <summary>
        /// 直接收费接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee IDoctFee = null;

        /// <summary>
        /// 医生站辅材处理接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IDealSubjob IDealSubjob = null;

        /// <summary>
        /// 合理用药接口
        /// </summary>
        IReasonableMedicine IReasonableMedicine = null;

        /// <summary>
        /// 保存后处方处理接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.ISaveOrder IAfterSaveOrder = null;

        /// <summary>
        /// 保存处方前调用
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder IBeforeSaveOrder = null;

        /// <summary>
        /// 增加项目前操作接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem IBeforeAddItem = null;

        /// <summary>
        /// 开立动作前接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddOrder IBeforeAddOrder = null;

        /// <summary>
        /// 门诊处方打印
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint IOutPatientPrint = null;
        /// <summary>
        /// 预约入院// {6BF1F99D-7307-4d05-B747-274D24174895}
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IPrePayIn IPrePayIn = null;

        /// <summary>
        /// 四舍五舍接口配置
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;

        /// <summary>
        /// 门诊分诊接收叫号信息
        /// </summary>
        private FS.SOC.HISFC.CallQueue.Interface.INurseAssign INurseAssign = null;

        #endregion

        #region 帮助类

        /// <summary>
        /// 医嘱帮助类
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper orderHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        //private FS.FrameWork.Public.ObjectHelper frequencyHelper;

        /// <summary>
        /// 科室帮助类
        /// </summary>
        //protected FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 常数管理帮助类：职级和诊查费项目对照
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper diagFeeConstHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region 委托事件

        public delegate void EventButtonHandler(bool b);

        /// <summary>
        /// 保存组套时刷新组套树
        /// </summary>
        public event EventHandler OnRefreshGroupTree;

        /// <summary>
        /// 医嘱是否可以取消组合事件
        /// </summary>
        public event EventButtonHandler OrderCanCancelComboChanged;

        /// <summary>
        /// 是否可打印检查单事件
        /// </summary>
        public event EventButtonHandler OrderCanSetCheckChanged;

        #endregion

        #region 业务层
        /// <summary>
        /// 医保接口
        /// </summary>
        //FS.HISFC.BizLogic.Fee.Interface CacheManager.InterfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
        /// <summary>
        /// 医嘱扩展信息管理{97B9173B-834D-49a1-936D-E4D04F98E4BA}
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.OrderExtend orderExtMgr = new FS.HISFC.BizLogic.Order.OutPatient.OrderExtend();

        /// 医嘱扩展检验信息管理
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.OrderLisExtend orderExtMgr2 = new FS.HISFC.BizLogic.Order.OutPatient.OrderLisExtend();

        /// <summary>
        /// 存放医保对照项目
        /// </summary>
        Hashtable hsCompareItems = null;

        /// <summary>
        /// 是否显示医保对照标记
        /// </summary>
        private bool isShowPactCompareFlag = true;

        /// <summary>
        /// 排版管理
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schemgManager = new FS.HISFC.BizLogic.Registration.Schema();
        #endregion

        /// <summary>
        /// 是否在开立状态，用作避免多次查询数据库
        /// </summary>
        protected bool isAddMode = false;

        private string varCombID = "";//临时的组合号变量

        private string varTempUsageID = "zuowy";//临时用法
        private string varOrderUsageID = "maokb";//医嘱用法


        private string previousClinicNo = ""; //前一个挂号编码


        /// <summary>
        /// 医保限制性用药药品列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper indicationsHelper = null;

        /// <summary>
        /// 医保限制性用药用药
        /// </summary>
        private ArrayList alIndicationsDrug = null;

        /// <summary>
        /// 存储列号
        /// </summary>
        int[] iColumns;

        /// <summary>
        /// 存储列宽
        /// </summary>
        int[] iColumnWidth;

        /// <summary>
        /// 存储列的可见性
        /// </summary>
        bool[] iColumnVisible;

        #endregion

        #region 属性

        /// <summary>
        /// 门诊账户:true终端收费 false门诊收费
        /// </summary>
        public bool IsAccountTerimal
        {
            get
            {
                return isAccountTerimal;
            }
            set
            {
                isAccountTerimal = value;
            }
        }

        /// <summary>
        /// 是否门诊账户流程
        /// </summary>
        public bool IsAccountMode
        {
            get
            {
                return isAccountMode;
            }
            set
            {
                isAccountMode = value;
            }
        }

        /// <summary>
        /// 是否设计模式
        /// </summary>
        protected bool bIsDesignMode = false;

        /// <summary>
        /// 是否显示右键弹出菜单
        /// </summary>
        protected bool bIsShowPopMenu = true;

        /// <summary>
        /// 右键菜单
        /// </summary>
        public bool IsShowPopMenu
        {
            set
            {
                this.bIsShowPopMenu = value;
            }
        }

        /// <summary>
        /// 是否通过seeno计算费用
        /// </summary>
        public bool IsCountFeeBySeeNo
        {
            get
            {
                return isCountFeeBySeeNo;
            }
            set
            {
                isCountFeeBySeeNo = value;
            }
        }

        /// <summary>
        /// 是否显示组合项目细项目
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        public bool IsLisDetail
        {
            set
            {
                this.ucOutPatientItemSelect1.IsLisDetail = value;
            }
        }

        /// <summary>
        /// 是否开立模式
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDesignMode
        {
            get
            {
                return this.bIsDesignMode;
            }
            set
            {
                if (this.bIsDesignMode != value)
                {
                    this.bIsDesignMode = value;

                    this.SetItemSelectControl();
                    this.QueryOrder();
                }
            }
        }

        /// <summary>
        /// 设置项目检索框的显示
        /// </summary>
        private void SetItemSelectControl()
        {
            this.ucOutPatientItemSelect1.Visible = this.bIsDesignMode;
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Registration.Register Patient
        {
            get
            {
                return this.currentPatientInfo;
            }
            set
            {
                if (value != null)
                {
                    currentPatientInfo = value;

                    //不是同一个人，则清空默认天数信息等
                    if (value.ID != currentPatientInfo.ID)
                    {
                        this.ucOutPatientItemSelect1.ClearDays();

                        this.PassRefresh();

                        if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
                        {
                            IReasonableMedicine.StationType = FS.HISFC.Models.Base.ServiceTypes.C;
                            IReasonableMedicine.PassSetPatientInfo(currentPatientInfo, this.GetReciptDoct());
                        }
                    }
                    if (this.GetRecentPatientInfo() == 1)
                    {
                        value = currentPatientInfo;

                        if (currentPatientInfo != null)
                        {
                            if (currentPatientInfo.Pact != null)
                            {
                                currentPatientInfo.Pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(currentPatientInfo.Pact.ID);
                            }
                            this.ucOutPatientItemSelect1.PatientInfo = currentPatientInfo;

                            if (this.isAccountMode)
                            {
                                decimal vacancy = 0;
                                int rev = CacheManager.AccountMgr.GetVacancy(currentPatientInfo.PID.CardNO, ref vacancy);
                                if (rev == -1)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("获取账户余额出错：" + CacheManager.AccountMgr.Err);
                                    //return;
                                }
                                //没有账户或账户停用
                                else if (rev == 0)
                                {
                                    this.vacancyDisplay = "无账户";
                                }
                                else
                                {
                                    this.vacancyDisplay = vacancy.ToString() + "元";
                                }
                            }

                            #region 设置合同单位信息

                            if (isAllowChangePactInfo)
                            {
                                this.cmbPact.Tag = this.currentPatientInfo.Pact.ID;

                                if (this.currentPatientInfo.IsSee)
                                {
                                    ArrayList alFee = CacheManager.FeeIntegrate.QueryAllFeeItemListsByClinicNO(this.currentPatientInfo.ID, "1", "ALL", "ALL");
                                    if (alFee != null && alFee.Count > 0)
                                    {
                                        this.cmbPact.Enabled = false;
                                    }
                                    else
                                    {
                                        this.cmbPact.Enabled = true;
                                    }
                                }
                                else
                                {
                                    this.cmbPact.Enabled = true;
                                }
                            }
                            #endregion


                            #region 处理开立时自行选择医保或自费方问题

                            //houwb 医保方自费方问题 {A1FE7734-267C-43f1-A824-BF73B482E0B2}

                            pnOrderPactInfo.Visible = false;

                            if (isAllowChangeRecipePactInfo)
                            {
                                this.pnOrderPactInfo.Visible = true;

                                if (CacheManager.AccountMgr.GetPatientPactInfo(currentPatientInfo) == -1)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("获取患者合同单位信息失败：" + CacheManager.AccountMgr.Err);
                                    //return;
                                }
                                else
                                {
                                    if (currentPatientInfo.MutiPactInfo.Count > 0)
                                    {
                                        this.rdPact1.Visible = false;
                                        this.rdPact2.Visible = false;
                                        this.rdPact3.Visible = false;
                                        this.rdPact4.Visible = false;

                                        for (int i = 0; i < currentPatientInfo.MutiPactInfo.Count; i++)
                                        {
                                            if (i == 0)
                                            {
                                                this.rdPact1.Visible = true;
                                                rdPact1.Text = currentPatientInfo.MutiPactInfo[i].Name;
                                                rdPact1.Tag = currentPatientInfo.MutiPactInfo[i];

                                                if (currentPatientInfo.MutiPactInfo[i].ID == currentPatientInfo.Pact.ID)
                                                {
                                                    rdPact1.Checked = true;
                                                }
                                            }
                                            else if (i == 1)
                                            {
                                                this.rdPact2.Visible = true;
                                                rdPact2.Text = currentPatientInfo.MutiPactInfo[i].Name;
                                                rdPact2.Tag = currentPatientInfo.MutiPactInfo[i];
                                                if (currentPatientInfo.MutiPactInfo[i].ID == currentPatientInfo.Pact.ID)
                                                {
                                                    rdPact2.Checked = true;
                                                }
                                            }
                                            else if (i == 2)
                                            {
                                                this.rdPact3.Visible = true;
                                                rdPact3.Text = currentPatientInfo.MutiPactInfo[i].Name;
                                                rdPact3.Tag = currentPatientInfo.MutiPactInfo[i];

                                                if (currentPatientInfo.MutiPactInfo[i].ID == currentPatientInfo.Pact.ID)
                                                {
                                                    rdPact3.Checked = true;
                                                }
                                            }
                                            else if (i == 3)
                                            {
                                                this.rdPact4.Visible = true;
                                                rdPact4.Text = currentPatientInfo.MutiPactInfo[i].Name;
                                                rdPact4.Tag = currentPatientInfo.MutiPactInfo[i];

                                                if (currentPatientInfo.MutiPactInfo[i].ID == currentPatientInfo.Pact.ID)
                                                {
                                                    rdPact4.Checked = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //end houwb

                            #endregion
                        }
                    }

                    this.SetOrderFeeDisplay(false, false);

                    this.QueryOrder();
                }
            }
        }

        /// <summary>
        /// 当前诊台
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.FrameWork.Models.NeuObject CurrentRoom
        {
            get
            {
                return this.currentRoom;
            }
            set
            {
                this.currentRoom = value;
            }
        }

        /// <summary>
        /// 开方科室
        /// </summary>
        protected FS.FrameWork.Models.NeuObject reciptDept = null;

        /// <summary>
        /// 设置开方科室
        /// </summary>
        [DefaultValue(null)]
        public void SetReciptDept(FS.FrameWork.Models.NeuObject value)
        {
            this.reciptDept = value;
        }

        /// <summary>
        /// 获取开方科室
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.reciptDept == null)
                {
                    //2012-10-9 11:20:56 houwb 
                    //中山六存在一个医生同时出诊多个科室的情况，而潘班可能只有一个
                    //修改为开立科室根据登陆科室取，医生登陆错误就是自己的问题了！

                    ////如果有排班信息，去排班科室作为开立科室 {231D1A80-6BF6-413f-8BBF-727DC2BF47D9}
                    //FS.HISFC.Models.Registration.Schema schema = CacheManager.RegInterMgr.GetSchema(GetReciptDoct().ID, this.CacheManager.OrderMgr.GetDateTimeFromSysDateTime());
                    //if (schema != null && schema.Templet.Dept.ID != "")
                    //{
                    //    this.reciptDept = schema.Templet.Dept.Clone();
                    //}
                    ////没有排版取登陆科室作为开立科室
                    //else
                    //{
                    this.reciptDept = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoct()).Dept.Clone(); //开立科室
                    //}
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }
            return this.reciptDept;
        }

        /// <summary>
        /// 开方医生
        /// </summary>
        protected FS.FrameWork.Models.NeuObject reciptDoct = null;

        /// <summary>
        /// 当前开立医生
        /// </summary>
        public void SetReciptDoc(FS.FrameWork.Models.NeuObject value)
        {
            this.reciptDoct = value;

        }

        /// <summary>
        /// 获得开方医生
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDoct()
        {
            try
            {
                if (this.reciptDoct == null)
                    this.reciptDoct = CacheManager.OutOrderMgr.Operator.Clone();
            }
            catch { }
            return this.reciptDoct;
        }

        /// <summary>
        /// 患者看诊科室,有别于挂号科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject SeeDept = null;

        /// <summary>
        /// 是否启用合理用药审查:显示提示信息框
        /// </summary>
        private bool enabledPass = true;

        /// <summary>
        /// 是否启用合理用药审查：显示提示信息框
        /// </summary>
        public bool EnabledPass
        {
            get
            {
                return enabledPass;
            }

            set
            {
                enabledPass = value;
            }
        }

        /// <summary>
        /// 是否历史医嘱状态
        /// </summary>
        public bool bOrderHistory = false;


        /// <summary>
        /// 药品和非药品的价格为0是否收费：0不收取；1收取。默认值为0不收取
        /// </summary>
        public string IsFeeWhenPriceZero
        {
            get
            {
                if (this.isFeeWhenPriceZero == "-1")
                {
                    this.isFeeWhenPriceZero = Classes.Function.GetBatchControlParam("FEE001", false, "0");
                }
                return this.isFeeWhenPriceZero;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 窗口Loading
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            Classes.LogManager.Write(currentPatientInfo.Name + "【开始初始化门诊医生主界面】");
            if (FS.FrameWork.Management.Connection.Operator.ID == "")
            {
                return;
            }

            this.reciptDoct = null;
            this.reciptDept = null;
            try
            {
                this.ucOutPatientItemSelect1.Init();
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }
            InitControl();

            InitDirectFee();

            InitDealSubJob();

            //ArrayList alPact = CacheManager.InterMgr.QueryPactUnitOutPatient();
            ArrayList alPact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitOutPatient();
            if (alPact == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取合同单位信息错误：" + CacheManager.InterMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.cmbPact.AddItems(alPact);
            //pactHelper.ArrayObject = alPact;

            if (Classes.Function.usageHelper == null)
            {
                ArrayList alUsage = CacheManager.GetConList("USAGE");
                Classes.Function.usageHelper = new FS.FrameWork.Public.ObjectHelper(alUsage);
            }

            try
            {
                #region 获取控制参数

                #region Useless 检验附材相关

                isDealSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("200000", false, "0"));
                //非药品带附材加急医嘱是否单独处理
                isDealEmrOrderSubtblSpecially = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("200005", false, "0"));
                emrSubtblUsage = Classes.Function.GetBatchControlParam("200006", false, "1");
                ULOrderUsage = Classes.Function.GetBatchControlParam("200007", false, "1");

                #endregion

                validRegDays = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("200022", false, "1"));

                isSaveOrderHistory = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("200021", false, "0"));


                //houwb 医保方自费方问题 {A1FE7734-267C-43f1-A824-BF73B482E0B2}
                isAllowChangeRecipePactInfo = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ43", false, "0"));
                //end houwb

                isAllowChangePactInfo = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ25", false, "0"));

                if (!isAllowChangePactInfo)
                {
                    this.pnPactInfo.Visible = false;
                }
                else
                {
                    this.pnPactInfo.Visible = true;
                }

                dealSublMode = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("HNMZ26", false, "0"));

                isAutoAddSupplyFee = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("HNMZ42", false, "1"));

                hypotestMode = Classes.Function.GetBatchControlParam("200201", false, "1");

                this.isCanModifiedInjectNum = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("MZ5001", false, "1"));

                isJudgeDiagnose = Classes.Function.GetBatchControlParam("200302", false, "0");
                isShowPactCompareFlag = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ27", false, "0"));

                isAutoPrintRecipe = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ23", false, "0"));

                this.isCheckDrugStock = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("200001", false, "0"));

                emplFreeRegType = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("HNMZ24", false, "0"));

                isCountFeeBySeeNo = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ98", false, "0"));

                isShowRepeatItemInScreen = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ96", false, "0"));

                isShowSpecsAndPrice = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("HNMZ31", false, "0"));

                isChangeAllSelect = Classes.Function.GetBatchControlParam("HNMZ32", false, "-1");

                isAddRegSubBeforeAddOrder = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ33", false, "0"));

                isShowSameOrder = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ97", false, "0"));

                string outDrugPreType = Classes.Function.GetBatchControlParam("P01015", false, "");
                if (outDrugPreType == "1")
                {
                    isPreUpdateStockinfoByOrder = false;
                }
                else
                {
                    isPreUpdateStockinfoByOrder = true;
                }

                try
                {
                    ArrayList al = CacheManager.GetConList("DoceOnceLimit");
                    if (al != null)
                    {
                        foreach (FS.HISFC.Models.Base.Const con in al)
                        {
                            doceOnceLimit = FS.FrameWork.Function.NConvert.ToDecimal(con.Name);
                        }
                    }
                }
                catch
                {
                    doceOnceLimit = -1;
                }

                #endregion

                try
                {
                    //获得所有科室
                    //ArrayList alTemp = CacheManager.InterMgr.GetDepartment();
                    //this.deptHelper.ArrayObject = alTemp;

                    //获得所有频次信息               
                    //alTemp = CacheManager.InterMgr.QuereyFrequencyList();
                    //if (alTemp != null)
                    //{
                    //    frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alTemp);
                    //}
                }
                catch (Exception ex)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region Farpoint设置

                this.neuSpread1.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating;
                this.neuSpread1.Sheets[0].DataAutoSizeColumns = false;

                this.neuSpread1.Sheets[0].DataAutoCellTypes = false;

                this.neuSpread1.Sheets[0].GrayAreaBackColor = Color.White;

                this.neuSpread1.Sheets[0].RowHeader.Columns.Get(0).Width = 30;

                //this.neuSpread1.Sheets[0].RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;

                this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised);
                this.neuSpread1_Sheet1.RowHeader.DefaultStyle.CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                this.neuSpread1.ActiveSheetIndex = 0;

                this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellClick);
                this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);

                #endregion

                //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                this.ucOutPatientItemSelect1.GetMaxSubCombNo += new ucOutPatientItemSelect.GetMaxSubCombNoEvent(GetMaxSubCombNo);
                this.ucOutPatientItemSelect1.GetSameSubCombNoOrder += new ucOutPatientItemSelect.GetSameSubCombNoOrderEvent(ucOutPatientItemSelect1_GetSameSortIDOrder);

                this.ucOutPatientItemSelect1.OrderChanged += new ItemSelectedDelegate(ucItemSelect1_OrderChanged);
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }


            //{FA143951-748B-4c45-9D1B-853A31B9E006}
            FS.HISFC.Models.Base.Employee curremployee = CacheManager.PersonMgr.GetEmployeeByCode(CacheManager.InOrderMgr.Operator.ID);

            FS.HISFC.Models.Base.Department currDepts = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            string hospitalname = "";
            string hospitalybcode = "";
            if (currDepts.HospitalName.Contains("顺德"))
            {
                hospitalname = "顺德爱博恩妇产医院";
                hospitalybcode = "H44060600494";
            }
            else
            {
                hospitalname = "广州爱博恩妇产医院";
                hospitalybcode = "H44010600124";
            }

            string gjcode = "";
            if (curremployee != null)
            {

                if (string.IsNullOrEmpty(curremployee.InterfaceCode))
                {
                    gjcode = curremployee.UserCode;
                }
                else
                {
                    gjcode = curremployee.InterfaceCode;
                }
            }

            base.OnStatusBarInfo(null, "(绿色：新开)(蓝色：收费)   机构名称：" + hospitalname + "  国家医保编码：" + hospitalybcode + "  医保医师代码：" + gjcode + "");

            Classes.Function.SethsUsageAndSub();

            #region 新增接口

            if (IAfterSaveOrder == null)
            {
                IAfterSaveOrder = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.ISaveOrder)) as FS.HISFC.BizProcess.Interface.Order.ISaveOrder;
            }

            if (IBeforeSaveOrder == null)
            {
                IBeforeSaveOrder = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder)) as FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder;
            }

            if (IBeforeAddItem == null)
            {
                IBeforeAddItem = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem)) as FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem;
            }

            if (IBeforeAddOrder == null)
            {
                IBeforeAddOrder = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IBeforeAddOrder)) as FS.HISFC.BizProcess.Interface.Order.IBeforeAddOrder;
            }

            if (IOutPatientPrint == null)
            {
                IOutPatientPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint)) as FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint;
            }

            if (ITruncFee == null)
            {
                ITruncFee = (FS.HISFC.BizProcess.Interface.Fee.ITruncFee)FS.HISFC.BizProcess.Interface.Fee.InterfaceManager.GetTruncFeeType();
            }

            if (INurseAssign == null)
            {
                INurseAssign = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.SOC.HISFC.CallQueue.Interface.INurseAssign)) as FS.SOC.HISFC.CallQueue.Interface.INurseAssign;
            }

            #endregion


            #region 合理用药
            Classes.LogManager.Write(currentPatientInfo.Name + "【开始初始化合理用药】");
            //{9DB64486-4398-4944-85FC-48F63A21CD7E}
            this.InitReasonableMedicine();

            if (this.IReasonableMedicine != null)
            {
                StartReasonableMedicine();
            }
            Classes.LogManager.Write(currentPatientInfo.Name + "【结束初始化合理用药】");

            #endregion
            Classes.LogManager.Write(currentPatientInfo.Name + "【结束初始化门诊医生主界面】");

            //查询是否启用分诊
            isUseNurseArray = Classes.Function.IsUseNurseArray();

            //对于门、急诊停诊的医生，登陆门诊医生站后，提示:今天没有您的排班，请与门诊办联系
            //DateTime nowTime = schemgManager.GetDateTimeFromSysDateTime();
            //string doctId = FS.FrameWork.Management.Connection.Operator.ID;
            //string deptId = CacheManager.LogEmpl.Dept.ID;
            //if (schemgManager.QueryByDoct(nowTime, deptId, doctId).Count <= 0)
            //{
            //    this.pnTop.Height = 23;
            //    this.pnDisplay.Visible = true;
            //    this.lblDisplay.Text = "今天没有您的排班，请与门诊办联系";
            //    this.lblDisplay.ForeColor = Color.Red;
            //    lblFeeInfo.Text = "";
            //}
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            //默认操作模式--医嘱开立模式
            this.ucOutPatientItemSelect1.OperatorType = Operator.Add;

            #region 初始化dataset信息
            this.dtOrder = this.InitDataSet();
            this.neuSpread1.Sheets[0].DataSource = this.dtOrder.Tables[0];
            #endregion

            //初始化FarPoint
            this.InitFP();

            this.SetItemSelectControl();

            #region FarPoint 事件
            this.neuSpread1.MouseUp += new MouseEventHandler(neuSpread1_MouseUp);
            this.neuSpread1.Sheets[0].Columns[-1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);

            this.neuSpread1.Sheets[0].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(neuSpread1_Sheet1_CellChanged);

            #endregion

            ArrayList alIndications = CacheManager.GetConList("IndicationsDrug");
            indicationsHelper = new FS.FrameWork.Public.ObjectHelper(alIndications);

        }

        /// <summary>
        /// 初始化直接收费接口
        /// </summary>
        private void InitDirectFee()
        {
            if (IDoctFee == null)
            {
                IDoctFee = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee)) as FS.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee;
            }
        }

        /// <summary>
        /// 辅材处理接口
        /// </summary>
        private void InitDealSubJob()
        {
            if (IDealSubjob == null)
            {
                IDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
        }

        /// <summary>
        /// 初始化Fp
        /// </summary>
        private void InitFP()
        {
            this.ColumnSet();

            this.SetColumnName(0);

            try
            {
                this.neuSpread1.Sheets[0].ZoomFactor = 1;

                this.SetColumnProperty();
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message + "\r\n 请删除配置文件[clinicordersetting.xml]后重试！");
            }
        }

        /// <summary>
        /// 计算显示的总金额
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        private decimal GetCost(decimal cost)
        {
            if (ITruncFee != null)
            {
                return FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(cost));
            }
            else
            {
                return FS.FrameWork.Public.String.FormatNumber(cost, 2);
            }
        }

        /// <summary>
        /// 初始化dataset
        /// </summary>
        /// <returns></returns>
        private DataSet InitDataSet()
        {
            try
            {
                DataSet dtOrder = new DataSet();
                Type dtStr = System.Type.GetType("System.String");
                Type dtDbl = typeof(System.Double);
                Type dtInt = typeof(System.Decimal);
                Type dtBoolean = typeof(System.Boolean);
                Type dtDate = typeof(System.DateTime);

                DataTable table = new DataTable("Table");
                DataColumn[] dc = new DataColumn[ColumnsSet.Length];

                for (int i = 0; i < ColumnsSet.Length; i++)
                {
                    if (ColumnsSet[i] == "加急")
                    {
                        dc[i] = new DataColumn(ColumnsSet[i], dtBoolean);
                    }
                    else
                    {
                        dc[i] = new DataColumn(ColumnsSet[i], dtStr);
                    }
                }
                table.Columns.AddRange(dc);

                dtOrder.Tables.Add(table);

                return dtOrder;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 设置列属性
        /// </summary>
        private void SetColumnProperty()
        {
            if (System.IO.File.Exists(SetingFileName))
            {
                if (iColumnWidth == null || iColumnWidth.Length <= 0)
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1.Sheets[0], SetingFileName);

                    iColumnWidth = new int[ColumnsSet.Length];
                    iColumnVisible = new bool[ColumnsSet.Length];
                    for (int i = 0; i < this.neuSpread1.Sheets[0].Columns.Count; i++)
                    {
                        iColumnWidth[i] = (int)this.neuSpread1.Sheets[0].Columns[i].Width;
                        iColumnVisible[i] = this.neuSpread1.Sheets[0].Columns[i].Visible;
                    }
                }
                else
                {
                    for (int i = 0; i < this.neuSpread1.Sheets[0].Columns.Count; i++)
                    {
                        this.neuSpread1.Sheets[0].Columns[i].Width = iColumnWidth[i];
                        this.neuSpread1.Sheets[0].Columns[i].Visible = iColumnVisible[i];
                    }
                }
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[0], SetingFileName);
            }

            this.neuSpread1.Sheets[0].Columns[GetColumnIndexFromName("每次用量")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1.Sheets[0].Columns[GetColumnIndexFromName("顺序号")].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
        }

        #region 列设置

        /// <summary>
        /// 加载fp的列
        /// </summary>
        private void ColumnSet()
        {
            iColumns = new int[ColumnsSet.Length];
            for (int i = 0; i < ColumnsSet.Length; i++)
            {
                iColumns[i] = this.GetColumnIndexFromName(ColumnsSet[i]);
            }
        }

        /// <summary>
        /// 设置列名
        /// </summary>
        /// <param name="k"></param>
        private void SetColumnName(int k)
        {
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

            this.neuSpread1.Sheets[k].Columns.Count = ColumnsSet.Length;

            for (int i = 0; i < ColumnsSet.Length; i++)
            {
                this.neuSpread1.Sheets[k].Columns[i].Label = ColumnsSet[i];
            }
        }

        /// <summary>
        /// 通过列名获得列索引
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private int GetColumnIndexFromName(string Name)
        {
            for (int i = 0; i < this.dtOrder.Tables[0].Columns.Count; i++)
            {
                if (this.dtOrder.Tables[0].Columns[i].ColumnName == Name)
                {
                    return i;
                }
            }
            ucOutPatientItemSelect1.MessageBoxShow("查找数据时缺少列" + Name);
            return -1;
        }

        /// <summary>
        /// 得到列索引
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetColumnNameFromIndex(int i)
        {
            return this.dtOrder.Tables[0].Columns[i].ColumnName;
        }

        /// <summary>
        /// 列设置
        /// </summary>
        private string[] ColumnsSet = {
                                          "!",            //0
                                          "警",           //1
                                          "医嘱类型",     //2
                                          "医嘱流水号",   //3
                                          "医嘱状态",     //4
                                          "组合号",       //5
                                          "主药",         //6
                                          "项目编码",     //7
                                          "医嘱名称",     //8
                                          "组合",         //33
                                          "总量",         //9
                                          "总量单位",     //10
                                          "每次用量",     //11
                                          "单位",         //12
                                          "付数/天数",    //13
                                          "频次编码",     //14
                                          "频次名称",     //15
                                          "用法编码",     //16
                                          "用法名称",     //17
                                          "院注次数",     //18
                                          "规格",
                                          "单价",
                                          "金额",
                                          "开始时间",     //19
                                          "开立医生",     //32
                                          "执行科室编码", //20
                                          "执行科室",     //21
                                          "加急",         //22
                                          "检查部位",     //34
                                          "样本类型",     //35
                                          "取药药房编码", //36
                                          "取药药房",     //37
                                          "备注",         //23
                                          "录入人编码",   //24
                                          "录入人",       //25
                                          "开立科室",     //26
                                          "开立时间",     //27
                                          "停止时间",     //28
                                          "停止人编码",   //29
                                          "停止人",       //30
                                          "顺序号",       //31
                                          "皮试代码",     //38
                                          "皮试"          //39
                                      };

        #endregion

        /// <summary>
        /// 初始化医嘱信息变更接口实例
        /// </summary>
        protected void InitAlterOrderInstance()
        {
            if (this.IAlterOrderInstance == null)
            {
                this.IAlterOrderInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.IAlterOrder)) as FS.HISFC.BizProcess.Interface.IAlterOrder;
            }
        }

        #endregion

        #region 私有方法

        #region 添加数据到表格

        /// <summary>
        /// 添加实体toTable
        /// </summary>
        /// <param name="list"></param>
        private void AddObjectsToTable(ArrayList list)
        {
            this.dtOrder.Tables[0].Clear();

            FS.HISFC.Models.Order.OutPatient.Order order = null;
            foreach (object obj in list)
            {
                order = obj as FS.HISFC.Models.Order.OutPatient.Order;
                this.dtOrder.Tables[0].Rows.Add(AddObjectToRow(order, this.dtOrder.Tables[0]));
            }
        }

        /// <summary>
        /// 显示医嘱名称列
        /// </summary>
        /// <param name="inOrder"></param>
        /// <returns></returns>
        private string ShowOrderName(FS.HISFC.Models.Order.OutPatient.Order outOrder)
        {
            string specs = string.IsNullOrEmpty(outOrder.Item.Specs) ? "" : ("[" + outOrder.Item.Specs + "] ");
            string price = "";
            if (outOrder.Item.ID == "999")
            {
                price = "[" + "0元/" + outOrder.Item.PriceUnit + "]";
            }
            else
            {
                if (outOrder.Item.Price > 0)
                {
                    if (outOrder.MinunitFlag == "1") //最小单位判断
                    {
                        price = "[" + FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / outOrder.Item.PackQty) + "元/" + outOrder.Item.PriceUnit + "]";//6
                    }
                    else
                    {
                        price = "[" + FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price) + "元/" + outOrder.Item.PriceUnit + "]";//6
                    }
                }
                else if (outOrder.Unit == "[复合项]")
                {
                    if (outOrder.MinunitFlag == "1")
                    {
                        price = "[" + FS.FrameWork.Public.String.ToSimpleString(OutPatient.Classes.Function.GetUndrugZtPrice(outOrder.Item.ID) / outOrder.Item.PackQty) + "元/" + outOrder.Item.PriceUnit + "]";//6
                    }
                    else
                    {
                        price = "[" + FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price) + "元/" + outOrder.Item.PriceUnit + "]";//6
                    }
                }
            }

            if (this.isShowSpecsAndPrice == 0)
            {
                specs = "";
                price = "";
            }
            else if (isShowSpecsAndPrice == 1)
            {
                price = "";
            }
            else if (isShowSpecsAndPrice == 2)
            {
                specs = "";
            }

            //医嘱名称 
            if (outOrder.Item.Specs == null || outOrder.Item.Specs.Trim() == "")
            {
                return "[组:" + outOrder.SubCombNO.ToString() + "]" + (outOrder.IsPermission ? "【√】" : "") + outOrder.Item.Name + price;
            }
            else
            {
                return "[组:" + outOrder.SubCombNO.ToString() + "]" + (outOrder.IsPermission ? "【√】" : "") + outOrder.Item.Name + "[" + outOrder.Item.Specs + "]" + price;
            }
        }

        /// <summary>
        /// 添加order到row
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataRow AddObjectToRow(object obj, DataTable table)
        {
            DataRow row = table.NewRow();
            FS.HISFC.Models.Order.OutPatient.Order order = null;
            try
            {
                order = obj as FS.HISFC.Models.Order.OutPatient.Order;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return null;
            }

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item objItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                row[GetColumnIndexFromName("主药")] = FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug);//5
                row[GetColumnIndexFromName("每次用量")] = string.IsNullOrEmpty(order.DoseOnceDisplay) ? FS.FrameWork.Public.String.ToSimpleString(order.DoseOnce) : order.DoseOnceDisplay;//9
                row[GetColumnIndexFromName("单位")] = objItem.DoseUnit;
                //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                row[GetColumnIndexFromName("付数/天数")] = order.HerbalQty;//11
            }
            else if (order.Item.ItemType == EnumItemType.UnDrug)
            {
                row[GetColumnIndexFromName("付数/天数")] = order.HerbalQty;//11
            }

            if (order.Note != "")
            {
                row[GetColumnIndexFromName("!")] = order.Note;
            }

            row[GetColumnIndexFromName("警")] = "";     //0
            row[GetColumnIndexFromName("医嘱类型")] = "门诊医嘱";//1
            row[GetColumnIndexFromName("医嘱流水号")] = order.ID;//2
            row[GetColumnIndexFromName("医嘱状态")] = order.Status;//新开立，审核，执行
            row[GetColumnIndexFromName("组合号")] = order.Combo.ID;//4

            #region 医嘱名称

            row[GetColumnIndexFromName("医嘱名称")] = ShowOrderName(order);

            #endregion

            this.ValidNewOrder(order);
            row[GetColumnIndexFromName("总量")] = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
            row[GetColumnIndexFromName("总量单位")] = order.Unit;//8
            row[GetColumnIndexFromName("频次编码")] = order.Frequency.ID;
            row[GetColumnIndexFromName("频次名称")] = order.Frequency.Name;
            row[GetColumnIndexFromName("用法编码")] = order.Usage.ID;
            row[GetColumnIndexFromName("用法名称")] = order.Usage.Name;//15
            row[GetColumnIndexFromName("开始时间")] = order.BeginTime;
            row[GetColumnIndexFromName("执行科室编码")] = order.ExeDept.ID;

            //2012-10-4 新增
            if (order.Item.ItemType == EnumItemType.Drug)
            {
                if (!string.Equals(order.Item.ID, "999"))
                {
                    row[GetColumnIndexFromName("项目编码")] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).UserCode;
                    if (order.MinunitFlag != "1")//开立最小单位 
                    {
                        row[GetColumnIndexFromName("单价")] = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackUnit;
                        row[GetColumnIndexFromName("金额")] = FS.FrameWork.Public.String.ToSimpleString(order.Qty * order.Item.Price, 2);
                    }
                    else
                    {
                        row[GetColumnIndexFromName("单价")] = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackQty, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).MinUnit;
                        row[GetColumnIndexFromName("金额")] = FS.FrameWork.Public.String.ToSimpleString(order.Qty * order.Item.Price / order.Item.PackQty, 2);
                    }
                }
            }
            else
            {
                if (!string.Equals(order.Item.ID, "999"))
                {
                    row[GetColumnIndexFromName("项目编码")] = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).UserCode;
                }
                row[GetColumnIndexFromName("单价")] = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price, 2) + "元/" + order.Item.PriceUnit;
                row[GetColumnIndexFromName("金额")] = FS.FrameWork.Public.String.ToSimpleString(order.Qty * order.Item.Price, 2);
            }
            row[GetColumnIndexFromName("规格")] = order.Item.Specs;


            row[GetColumnIndexFromName("执行科室")] = order.ExeDept.Name;
            row[GetColumnIndexFromName("加急")] = order.IsEmergency;
            row[GetColumnIndexFromName("检查部位")] = order.CheckPartRecord;
            row[GetColumnIndexFromName("样本类型")] = order.Sample.Name;
            row[GetColumnIndexFromName("取药药房编码")] = order.StockDept.ID;
            row[GetColumnIndexFromName("取药药房")] = order.StockDept.Name;
            row[GetColumnIndexFromName("院注次数")] = order.InjectCount;
            row[GetColumnIndexFromName("备注")] = order.Memo;//20
            row[GetColumnIndexFromName("录入人编码")] = order.Oper.ID;
            row[GetColumnIndexFromName("录入人")] = order.Oper.Name;
            row[GetColumnIndexFromName("开立医生")] = order.ReciptDoctor.Name;
            row[GetColumnIndexFromName("开立科室")] = order.ReciptDept.Name;
            row[GetColumnIndexFromName("开立时间")] = order.MOTime;

            if (order.EndTime != DateTime.MinValue)
            {
                row[GetColumnIndexFromName("停止时间")] = order.EndTime;//25
            }

            row[GetColumnIndexFromName("停止人编码")] = order.DCOper.ID;
            row[GetColumnIndexFromName("停止人")] = order.DCOper.Name;

            row[GetColumnIndexFromName("顺序号")] = order.SortID;//28
            row[GetColumnIndexFromName("皮试代码")] = order.HypoTest;
            row[GetColumnIndexFromName("皮试")] = CacheManager.OutOrderMgr.TransHypotest(order.HypoTest);
            return row;
        }

        /// <summary>
        /// 添加-待改
        /// </summary>
        /// <param name="al"></param>
        private void AddObjectsToFarpoint(ArrayList al)
        {
            if (al == null) return;

            int k = 0;

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = al[i] as FS.HISFC.Models.Order.OutPatient.Order;

                this.neuSpread1.Sheets[0].Rows.Add(k, 1);
                this.AddObjectToFarpoint(al[i], k, 0, EnumOrderFieldList.Item);

                k++;

            }
        }

        /// <summary>
        /// 添加医嘱到FarPoint
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="i"></param>
        /// <param name="SheetIndex"></param>
        /// <param name="orderlist"></param>
        private void AddObjectToFarpoint(object obj, int rowIndex, int SheetIndex, EnumOrderFieldList orderlist)
        {
            this.dirty = true;

            FS.HISFC.Models.Order.OutPatient.Order order = null;
            try
            {
                order = ((FS.HISFC.Models.Order.OutPatient.Order)obj);//.Clone();
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("Clone出错！" + ex.Message);
                this.dirty = false;
                return;
            }

            if (this.isAddMode)
            {
                # region 根据用法自动弹出添加院注
                try
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(rowIndex, SheetIndex);

                    if (temp != null && order.Usage.ID != "")
                    {
                        if (temp.Usage.ID != order.Usage.ID)
                        {
                            if (this.varCombID != order.Combo.ID)
                            {
                                this.varCombID = order.Combo.ID;
                                varTempUsageID = "zuowy";//临时用法
                                varOrderUsageID = "maokb";//医嘱用法
                            }

                            if (temp.Item.ItemType == EnumItemType.Drug)
                            {
                                if (temp.Usage.ID == this.varTempUsageID && order.Usage.ID == this.varOrderUsageID)
                                {

                                }
                                else
                                {
                                    this.varTempUsageID = temp.Usage.ID;
                                    this.varOrderUsageID = order.Usage.ID;

                                    //if (Classes.Function.hsUsageAndSub.Contains(order.Usage.ID))
                                    if (Classes.Function.CheckIsInjectUsage(order.Usage.ID))
                                    {
                                        this.AddInjectNum(order, this.isCanModifiedInjectNum);

                                        //ArrayList al = (ArrayList)Classes.Function.hsUsageAndSub[order.Usage.ID];
                                        //if (al != null && al.Count > 0)
                                        //{
                                        //    this.AddInjectNum(order, this.isCanModifiedInjectNum);
                                        //}
                                    }
                                }

                            }
                        }
                    }
                }
                catch
                {
                    this.dirty = false;
                }
                #endregion
            }

            if (order.Note != "")//提示
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("!")].Text = order.Note;
            }

            if (order.Item.ItemType == EnumItemType.Drug)//药品
            {
                FS.HISFC.Models.Pharmacy.Item objItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("每次用量")].Text = string.IsNullOrEmpty(order.DoseOnceDisplay) ? FS.FrameWork.Public.String.ToSimpleString(order.DoseOnce) : order.DoseOnceDisplay;

                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("付数/天数")].Text = order.HerbalQty.ToString();//11

                if (order.DoseUnit == null || order.DoseUnit == "")
                {
                    order.DoseUnit = objItem.DoseUnit;
                }
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单位")].Text = order.DoseUnit;

                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = order.Unit;//8

            }
            //else if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug)) //非药品
            else if (order.Item.ItemType == EnumItemType.UnDrug) //非药品
            {
                FS.HISFC.Models.Fee.Item.Undrug objItem = order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单位")].Text = "";//剂量单位
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = order.Unit;//8
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("付数/天数")].Text = order.HerbalQty.ToString();//11
            }

            this.ValidNewOrder(order); //填写信息

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("警")].Text = "";     //0

            if (order.NurseStation.Memo != null && order.NurseStation.Memo.Length > 0)
            {
                //合理用药相关（暂时未改屏蔽）
                //this.AddWarnPicturn(i, 0, FS.FrameWork.Function.NConvert.ToInt32(order.NurseStation.Memo));
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[rowIndex, GetColumnIndexFromName("警")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[0].Cells[rowIndex, GetColumnIndexFromName("警")].Note = "";
                this.neuSpread1.Sheets[0].Cells[rowIndex, GetColumnIndexFromName("警")].Tag = "";
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("主药")].Text = System.Convert.ToInt16(order.Combo.IsMainDrug).ToString();//5
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("医嘱类型")].Text = "门诊医嘱"; //1 名称

            if (order.Item.PackQty == 0)
            {
                order.Item.PackQty = 1;
            }

            #region 医嘱名称

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("医嘱名称")].Text = ShowOrderName(order);

            #endregion

            string totCost = "";
            if (order.MinunitFlag == "1")//开立最小单位 
            {
                totCost = (order.Qty * order.Item.Price / order.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
            }
            else
            {
                totCost = (order.Qty * order.Item.Price).ToString("F4").TrimEnd('0').TrimEnd('.');
            }

            //this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("金额")].Text = totCost;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("医嘱流水号")].Text = order.ID;//2
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("医嘱状态")].Text = order.Status.ToString();//新开立，审核，执行
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("组合号")].Text = order.Combo.ID.ToString();//4

            if (order.Frequency == null || string.IsNullOrEmpty(order.Frequency.ID))
            {
                order.Frequency = Components.Order.Classes.Function.GetDefaultFrequency().Clone();
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("频次编码")].Text = order.Frequency.ID.ToString();
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("频次名称")].Text = order.Frequency.Name;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("用法编码")].Text = order.Usage.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("用法名称")].Text = order.Usage.Name;//15

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("院注次数")].Text = order.InjectCount.ToString();//36
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("开始时间")].Value = order.MOTime;//开始时间
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("开立时间")].Value = order.MOTime;//开立时间


            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("执行科室编码")].Text = order.ExeDept.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("执行科室")].Text = order.ExeDept.Name;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("加急")].Value = order.IsEmergency;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("检查部位")].Value = order.CheckPartRecord;//检查部位
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("样本类型")].Value = order.Sample.Name;//样本类型
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("取药药房编码")].Value = order.StockDept.ID;//取药药房
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("取药药房")].Value = order.StockDept.Name;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("备注")].Text = order.Memo;//20
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("录入人编码")].Text = order.Oper.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("录入人")].Text = order.Oper.Name;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("开立医生")].Text = order.ReciptDoctor.Name;//开立医生
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("开立科室")].Text = order.ReciptDept.Name;//开立科室

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                if (!string.Equals(order.Item.ID, "999"))
                {
                    this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("项目编码")].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).UserCode;
                    if (order.MinunitFlag != "1")//开立最小单位 
                    {
                        neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackUnit;

                        neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(order.Qty * order.Item.Price).ToString();
                    }
                    else
                    {
                        neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackQty, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).MinUnit;

                        neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(order.Qty * order.Item.Price / order.Item.PackQty).ToString();
                    }
                }
            }
            else
            {
                if (!string.Equals(order.Item.ID, "999"))
                {
                    neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("项目编码")].Text = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).UserCode;

                }
                neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price, 2) + "元/" + order.Item.PriceUnit;

                neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(order.Qty * order.Item.Price).ToString();
            }
            neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("规格")].Text = order.Item.Specs;

            if (order.EndTime != DateTime.MinValue)
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("停止时间")].Value = order.EndTime;//停止时间 25
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("停止人编码")].Text = order.DCOper.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("停止人")].Text = order.DCOper.Name;

            if (order.SortID == 0)
            {
                order.SortID = GetSortIDByBubCombNo(order.SubCombNO);
                //order.SortID = MaxSort + 1;
                //MaxSort = MaxSort + 1;
            }
            //else
            //{
            //    if (order.SortID > MaxSort)
            //    {
            //        MaxSort = order.SortID;
            //    }
            //}
            if (order.Frequency.Usage.ID == "")
            {
                order.Frequency.Usage = order.Usage; //用法付给
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("顺序号")].Value = order.SortID;//28
            if (!this.EditGroup)
            {
                if (this.currentPatientInfo.Pact.PayKind.ID == "02")//广州医保-显示费用比率
                {
                    //没用暂时屏蔽了 因为影响其他显示
                    //string feeStr = "";

                    //if (order.Item.PriceUnit != "[复合项]")
                    //{
                    //    this.neuSpread1.Sheets[SheetIndex].RowHeader.Columns.Get(0).Width = 15;

                    //    this.neuSpread1.Sheets[SheetIndex].RowHeader.Cells[rowIndex, 0].Text = feeStr;
                    //}
                    //else
                    //{
                    //    this.neuSpread1.Sheets[SheetIndex].RowHeader.Columns.Get(0).Width = 15;
                    //    this.neuSpread1.Sheets[SheetIndex].RowHeader.Cells[rowIndex, 0].Text = "";
                    //}
                }
                else//显示项目医保标记
                {
                    //this.neuSpread1.Sheets[SheetIndex].RowHeader.Columns.Get(0).Width = 50F;
                    //if (order.Item.Price > 0 && order.OrderType.IsCharge) this.neuSpread1.Sheets[SheetIndex].RowHeader.Cells[i, 0].Text = FS.HISFC.Components.Common.Classes.Function.ShowItemFlag(order.Item);
                }
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("皮试代码")].Value = order.HypoTest;//28
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("皮试")].Value = CacheManager.OutOrderMgr.TransHypotest(order.HypoTest);//28
            this.neuSpread1.Sheets[SheetIndex].Rows[rowIndex].Tag = order.Clone();

            this.dirty = false;

            if (order.Item.ItemType == EnumItemType.Drug
                && order.Item.SysClass.ID.ToString() != "PCC"
                && order.HerbalQty > 7)
            {
                Components.Order.Classes.Function.ShowBalloonTip(8, "警告", "【" + order.Item.Name + "】开立超过7日用量！\r\n请注意注明理由！", ToolTipIcon.Warning);
            }

            return;
        }

        #endregion

        #region 方号问题{98522448-B392-4d67-8C4D-A10F605AFDA5}

        /// <summary>
        /// 取最大方号
        /// </summary>
        /// <returns></returns>
        public int GetMaxSubCombNo(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            int maxNum = 0;

            FS.HISFC.Models.Order.OutPatient.Order o = null;
            foreach (FarPoint.Win.Spread.Row row in this.neuSpread1.Sheets[0].Rows)
            {
                o = this.GetObjectFromFarPoint(row.Index, 0);
                if (o != null)
                {
                    if (order != null && order.Combo != null && order.Combo.ID == o.Combo.ID)
                    {
                        return o.SubCombNO;
                    }
                    int sortID = 0;
                    try
                    {
                        sortID = o.SubCombNO;
                    }
                    catch
                    {
                        sortID = 1;
                    }

                    if (sortID > maxNum)
                    {
                        maxNum = sortID;
                    }
                }
            }
            return maxNum + 1;
        }

        private int GetSortIDByBubCombNo(int subCombNO)
        {
            int maxNum = 0;

            FS.HISFC.Models.Order.OutPatient.Order order = null;
            foreach (FarPoint.Win.Spread.Row row in this.neuSpread1.Sheets[0].Rows)
            {
                order = this.GetObjectFromFarPoint(row.Index, 0);
                if (order != null)
                {
                    if (order != null && order.SubCombNO == subCombNO)
                    {
                        if (order.SortID > maxNum)
                        {
                            maxNum = order.SortID;
                        }
                    }
                }
            }
            maxNum = maxNum + 1;
            if (maxNum < 99)
            {
                maxNum = subCombNO * 100 + maxNum;
            }

            return maxNum;
        }

        /// <summary>
        /// 获得相同组号医嘱
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int ucOutPatientItemSelect1_GetSameSortIDOrder(int sortID, ref FS.HISFC.Models.Order.OutPatient.Order order)
        {
            try
            {
                FS.HISFC.Models.Order.OutPatient.Order temp = null;
                for (int i = this.neuSpread1.ActiveSheet.RowCount - 1; i >= 0; i--)
                {
                    temp = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                    //排除自己
                    if (temp.SubCombNO == sortID && i != this.neuSpread1.ActiveSheet.ActiveRowIndex)
                    {
                        order = temp.Clone();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return -1;
            }
            return 1;
        }

        #endregion

        /// <summary>
        /// 刷新医嘱状态
        /// </summary>
        /// <param name="row"></param>
        /// <param name="SheetIndex"></param>
        /// <param name="reset"></param>
        private void ChangeOrderState(int row, int SheetIndex, bool reset)
        {
            try
            {
                int i = GetColumnIndexFromName("医嘱状态");//this.GetColumnIndexFromName("医嘱状态");
                int state = int.Parse(this.neuSpread1.Sheets[SheetIndex].Cells[row, i].Text);

                //直接根据界面的状态显示刷新
                FS.HISFC.Models.Order.OutPatient.Order orderTemp = GetObjectFromFarPoint(row, SheetIndex);
                if (orderTemp == null)
                {
                    return;
                }


                if (Components.Common.Classes.Function.HsItemPactInfo != null
                    && Components.Common.Classes.Function.HsItemPactInfo.Contains(Patient.Pact.ID + orderTemp.Item.ID))
                {
                    string ss = FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(Components.Common.Classes.Function.HsItemPactInfo[Patient.Pact.ID + orderTemp.Item.ID].ToString());
                    neuSpread1.Sheets[SheetIndex].Rows[row].Label = ss.Length > 2 ? ss.Substring(0, 1) : ss;
                }

                if (orderTemp.ID != "" && reset)
                {
                    this.neuSpread1.Sheets[SheetIndex].Cells[row, i].Value = state;
                }

                switch (state)
                {
                    case 0:
                        this.neuSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(128, 255, 128);
                        break;
                    case 1:
                        this.neuSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(106, 174, 242);
                        break;
                    case 2:
                        this.neuSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(243, 230, 105);
                        break;
                    case 3:
                        this.neuSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(248, 120, 222);
                        break;
                    default:
                        this.neuSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.Black;
                        break;
                }
                if (this.IsDesignMode)
                {
                    orderTemp.Status = state;
                }

                //附材项目灰色背景显示
                if (orderTemp.IsSubtbl)
                {
                    this.neuSpread1.Sheets[SheetIndex].Rows[row].BackColor = Color.Silver;
                }
                else
                {
                    this.neuSpread1.Sheets[SheetIndex].Rows[row].BackColor = Color.White;
                }

                if (isShowPactCompareFlag
                    && this.currentPatientInfo != null
                    && this.currentPatientInfo.Pact != null)
                {
                    if (hsCompareItems == null)
                    {
                        hsCompareItems = new Hashtable();
                    }
                    FS.HISFC.Models.SIInterface.Compare compareItem = null;

                    if (hsCompareItems.Contains(currentPatientInfo.Pact.ID + orderTemp.Item.ID))
                    {
                        this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("医嘱名称")].ForeColor = Color.Red;
                    }
                    else
                    {
                        if (CacheManager.InterfaceMgr.GetCompareSingleItem(this.currentPatientInfo.Pact.ID, orderTemp.Item.ID, ref compareItem) == -1)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("获取医保对照项目失败：" + CacheManager.InterfaceMgr.Err);
                            return;
                        }
                        if (compareItem != null)
                        {
                            if (!hsCompareItems.Contains(currentPatientInfo.Pact.ID + orderTemp.Item.ID))
                            {
                                hsCompareItems.Add(currentPatientInfo.Pact.ID + orderTemp.Item.ID, compareItem);
                            }

                            this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("医嘱名称")].ForeColor = Color.Red;
                        }
                        else
                        {
                            this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("医嘱名称")].ForeColor = Color.Black;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }

        }

        /// <summary>
        /// 清空查询医嘱列表
        /// </summary>
        public void ClearOrder()
        {
            try
            {
                this.neuSpread1.Sheets[0].RowCount = 0;

                if (this.dtOrder != null && this.dtOrder.Tables[0].Rows.Count > 0)
                {
                    this.dtOrder.Tables[0].Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return;
            }
            if (this.currentPatientInfo == null || string.IsNullOrEmpty(this.currentPatientInfo.ID))
            {
                return;
            }

            this.hsOrder.Clear();
        }

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void QueryOrder()
        {
            try
            {
                this.neuSpread1.Sheets[0].RowCount = 0;

                if (this.dtOrder != null && this.dtOrder.Tables[0].Rows.Count > 0)
                {
                    this.dtOrder.Tables[0].Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return;
            }
            if (this.currentPatientInfo == null || string.IsNullOrEmpty(this.currentPatientInfo.ID))
            {
                return;
            }

            //alFeeMoneyInfo = null;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询医嘱,请稍候!");
            Application.DoEvents();

            this.hsOrder.Clear();

            //查询所有医嘱类型
            if (this.currentPatientInfo.DoctorInfo.SeeNO == 0)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = -1;
            }
            ArrayList al = CacheManager.OutOrderMgr.QueryOrder(currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
            if (al == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.OutOrderMgr.Err);
                return;
            }

            //查询本次就诊已开立的医嘱
            this.SameOrderList = CacheManager.OutOrderMgr.QueryOrderByClinicCode(currentPatientInfo.ID);
            //查询过往就诊已开立未执行的检查或检验医嘱
            this.LastOrderList = CacheManager.OutOrderMgr.QueryLastOrderListByCardNo(currentPatientInfo.PID.CardNO, currentPatientInfo.ID);

            if (this.IsDesignMode)
            {
                isShowFeeWarning = false;
            }
            else
            {
                //isShowFeeWarning = true;
                isShowFeeWarning = false;
            }

            foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in al)
            {
                if (orderTemp != null)
                {
                    //houwb 医保方自费方问题 {A1FE7734-267C-43f1-A824-BF73B482E0B2}
                    try
                    {
                        if (rdPact1.Tag != null && ((PactInfo)rdPact1.Tag).ID == orderTemp.Patient.Pact.ID)
                        {
                            rdPact1.Checked = true;
                        }
                        else if (rdPact2.Tag != null && ((PactInfo)rdPact2.Tag).ID == orderTemp.Patient.Pact.ID)
                        {
                            rdPact2.Checked = true;
                        }
                        else if (rdPact3.Tag != null && ((PactInfo)rdPact3.Tag).ID == orderTemp.Patient.Pact.ID)
                        {
                            rdPact3.Checked = true;
                        }
                        else if (rdPact4.Tag != null && ((PactInfo)rdPact4.Tag).ID == orderTemp.Patient.Pact.ID)
                        {
                            rdPact4.Checked = true;
                        }
                    }
                    catch { }
                    //end houwb

                    if (orderTemp.IsHaveCharged)
                    {
                        isShowFeeWarning = true;
                    }
                    if (!this.hsOrder.Contains(orderTemp.SeeNO + orderTemp.ID) &&
                        !(string.IsNullOrEmpty(orderTemp.SeeNO) || string.IsNullOrEmpty(orderTemp.ID)))
                    {
                        this.hsOrder.Add(orderTemp.SeeNO + orderTemp.ID, orderTemp);
                    }
                }
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在显示医嘱,请稍候!");
            Application.DoEvents();

            if (this.IsDesignMode)
            {
                tooltip.SetToolTip(this.neuSpread1, "开立医嘱");
                tooltip.Active = true;
                this.isAddMode = true;
                try
                {
                    this.neuSpread1.Sheets[0].DataSource = null;

                    this.AddObjectsToFarpoint(al);
                    this.neuSpread1.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;

                    this.RefreshCombo();
                    this.RefreshOrderState();
                }
                catch (Exception ex)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                }
            }
            else
            {
                tooltip.SetToolTip(this.neuSpread1, "");
                try
                {
                    this.AddObjectsToTable(al);
                    this.dvOrder = new DataView(this.dtOrder.Tables[0]);

                    this.neuSpread1.Sheets[0].DataSource = dvOrder;

                    this.neuSpread1.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;

                    this.RefreshCombo();
                    this.RefreshOrderState(1);

                }
                catch (Exception ex)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                }
            }

            //this.SetOrderFeeDisplay(true);

            this.hsOrder.Clear();
            this.neuSpread1.ActiveSheet.ClearSelection();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.neuSpread1.ShowRow(0, this.neuSpread1.ActiveSheet.RowCount - 1, FarPoint.Win.Spread.VerticalPosition.Center);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryOrder();
            return 0;
        }

        /// <summary>
        /// 添满信息
        /// </summary>
        /// <param name="order"></param>
        private void ValidNewOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            if (order.ReciptDept.Name == "" && order.ReciptDept.ID != "")
            {
                order.ReciptDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ReciptDept.ID);
            }
            if (order.StockDept.Name == "" && order.StockDept.ID != "")
            {
                order.StockDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.StockDept.ID);
            }
            if (order.BeginTime == DateTime.MinValue)
            {
                order.BeginTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            }
            if (order.MOTime == DateTime.MinValue)
            {
                order.MOTime = order.BeginTime;
            }
            if (!this.EditGroup)
            {
                if (order.Patient == null || order.Patient.ID == "")
                {
                    order.Patient.ID = this.currentPatientInfo.ID;
                    order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                    order.RegTime = this.currentPatientInfo.DoctorInfo.SeeDate;
                    order.Patient.PID = this.currentPatientInfo.PID;
                }
                if (order.InDept.ID == null || order.InDept.ID == "")
                {
                    order.InDept = this.currentPatientInfo.DoctorInfo.Templet.Dept;
                }
            }
            if (order.ExeDept == null || order.ExeDept.ID == "")
            {
                //更改执行科室为患者科室
                if (!this.EditGroup)
                {
                    order.ExeDept.ID = this.GetReciptDept().ID;
                    order.ExeDept.Name = this.GetReciptDept().Name;
                }
                else
                {
                    order.ExeDept.ID = CacheManager.LogEmpl.Dept.ID;
                    order.ExeDept.Name = CacheManager.LogEmpl.Dept.Name;
                }
            }
            if (!string.IsNullOrEmpty(order.ExeDept.ID))
            {
                order.ExeDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ExeDept.ID);
            }

            //开单医生
            if (order.ReciptDoctor == null || order.ReciptDoctor.ID == "")
            {
                order.ReciptDoctor = this.GetReciptDoct().Clone();
            }
            //开单科室
            if (order.ReciptDept == null || order.ReciptDept.ID == "")
            {
                order.ReciptDept = this.GetReciptDept().Clone();
            }
            if (order.Oper.ID == null || order.Oper.ID == "")
            {
                order.Oper.ID = CacheManager.OutOrderMgr.Operator.ID;
                order.Oper.Name = CacheManager.OutOrderMgr.Operator.Name;
            }
        }

        /// <summary>
        /// 检查开立信息，显示错误！
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="iRow"></param>
        /// <param name="SheetIndex"></param>
        private void ShowErr(string strMsg, int iRow, int SheetIndex)
        {
            this.neuSpread1.ActiveSheetIndex = SheetIndex;
            this.neuSpread1.Sheets[SheetIndex].ClearSelection();
            this.neuSpread1.Sheets[SheetIndex].ActiveRowIndex = iRow;
            this.SelectionChanged();
            this.neuSpread1.Sheets[SheetIndex].AddSelection(iRow, 0, 1, 1);

            this.neuSpread1.ShowRow(SheetIndex, iRow, FarPoint.Win.Spread.VerticalPosition.Center);

            ucOutPatientItemSelect1.MessageBoxShow(strMsg, "信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        /// <summary>
        /// 选择变化
        /// </summary>
        private void SelectionChanged()
        {

            /*
            if (!this.IsDesignMode && !EditGroup)
            {
                return;
            }
            */

            #region 选择
            //每次选择变化前清空数据显示
            this.ucOutPatientItemSelect1.Clear(false);
            decimal totalPrice = 0;
            int comboNum = 0;//获得当前选择行数

            //设置为当前行
            this.ucOutPatientItemSelect1.CurrentRow = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            this.currentOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
            this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;
            //设置组合行选择
            if (this.currentOrder.Combo.ID != ""
                && this.currentOrder.Combo.ID != null)//&& this.IsDesignMode)
            {

                ///向下寻找
                for (int i = this.ActiveRowIndex; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order tempOrder = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    string strComboNo = tempOrder.Combo.ID;

                    //string strComboNo = this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("组合号")].Text;


                    if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo) //&& i != this.neuSpread1.ActiveSheet.ActiveRowIndex
                    {
                        this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, 1);
                        comboNum++;
                        totalPrice += tempOrder.FT.OwnCost + tempOrder.FT.PayCost + tempOrder.FT.PubCost;
                    }
                    else
                    {
                        break;
                    }

                }

                ///向上寻找
                for (int i = this.ActiveRowIndex - 1; i >= 0; i--)
                {
                    FS.HISFC.Models.Order.OutPatient.Order tempOrder = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                    string strComboNo = tempOrder.Combo.ID;
                    if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo)//&& i != this.neuSpread1.ActiveSheet.ActiveRowIndex
                    {
                        this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, 1);
                        comboNum++;
                        totalPrice += tempOrder.FT.OwnCost + tempOrder.FT.PayCost + tempOrder.FT.PubCost;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (int.Parse(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("医嘱状态")].Text) == 0)
            {
                if (comboNum == 0)
                {
                    //只有一行
                    if (OrderCanCancelComboChanged != null)
                    {
                        this.OrderCanCancelComboChanged(false);//不能取消组合
                    }
                }
                else
                {
                    if (OrderCanCancelComboChanged != null)
                    {
                        this.OrderCanCancelComboChanged(true);//可以取消组合
                    }
                }


                if (OrderCanSetCheckChanged != null)
                {
                    this.OrderCanSetCheckChanged(false);//打印检查申请单失效
                }
            }
            else
            {
                this.ActiveRowIndex = -1;
            }
            #endregion
            this.tooltip.SetToolTip(this.neuSpread1, "药品金额：" + totalPrice.ToString() + "元");


        }

        /// <summary>
        /// 组合医嘱
        /// </summary>
        /// <param name="k"></param>
        private void ComboOrder(int sheetIndex)
        {
            try
            {
                int iSelectionCount = 0;
                for (int i = 0; i < this.neuSpread1.Sheets[sheetIndex].Rows.Count; i++)
                {
                    if (this.neuSpread1.Sheets[sheetIndex].IsSelected(i, 0))
                    {
                        iSelectionCount++;
                    }
                }

                if (iSelectionCount > 1)
                {
                    string t = "";//组合号 修改成都有组合号
                    int injectNum = 0;//院内注次数
                    int iSort = -1;
                    string time = "";
                    int kk = 0;

                    if (this.ValidComboOrder() == -1)
                    {
                        return;//校验组合医嘱
                    }

                    //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    int sameSubComb = 0;
                    FS.HISFC.Models.Order.OutPatient.Order ord = null;

                    //用于记录修改过的放号
                    string combID = "";
                    int preSubCombNo = 0;

                    for (int rowIndex = 0; rowIndex < this.neuSpread1.Sheets[sheetIndex].Rows.Count; rowIndex++)
                    {
                        ord = this.GetObjectFromFarPoint(rowIndex, sheetIndex);
                        ord.SortID = rowIndex + 1;
                        /*
                         * 注释by  zhaorong  at 2013-8-5 中草药重新组合保存后数量有double计算现象
                         */
                        //if (ord.Item.ItemType == EnumItemType.Drug)
                        //{
                        //    if (ord.Item.SysClass.ID != null)
                        //    {
                        //        if (ord.Item.SysClass.ID.ToString() == "PCC")
                        //        {
                        //            ord.ReciptNO = "";
                        //            ord.ReciptSequence = "";
                        //        }
                        //    }
                        //}

                        this.neuSpread1.Sheets[sheetIndex].Cells[rowIndex, GetColumnIndexFromName("顺序号")].Text = ord.SortID.ToString();
                        this.neuSpread1.Sheets[sheetIndex].Cells[rowIndex, GetColumnIndexFromName("顺序号")].Value = ord.SortID;

                        if (this.neuSpread1.Sheets[sheetIndex].IsSelected(rowIndex, 0))
                        {
                            if (t == "")
                            {
                                t = ord.Combo.ID;
                                time = ord.Frequency.Time;
                                sameSubComb = ord.SubCombNO;
                                preSubCombNo = ord.SubCombNO;
                            }
                            else
                            {
                                #region 如果是已经保存的医嘱，组合变化后需要删除附材

                                if (ord.ID != null && ord.ID != null)
                                {
                                    if (!hsComboChange.ContainsKey(ord.ID))
                                    {
                                        hsComboChange.Add(ord.ID, ord.Combo.ID);
                                    }
                                }
                                ord.NurseStation.User02 = "C";
                                #endregion

                                ord.Combo.ID = t;
                                ord.Frequency.Time = time;
                                ord.SubCombNO = sameSubComb;
                            }
                            //院内注次数
                            if (injectNum == 0)
                            {
                                injectNum = ord.InjectCount;
                            }
                            else
                            {
                                ord.InjectCount = injectNum;
                            }
                            if (iSort == -1)
                            {
                                iSort = ord.SortID;
                            }
                            else
                            {
                                ord.SortID = iSort + kk;
                            }
                            kk++;

                            this.AddObjectToFarpoint(ord, rowIndex, sheetIndex, EnumOrderFieldList.Item);
                        }
                        else if (kk > 0)
                        {
                            ord.SortID = ord.SortID + iSelectionCount - kk;

                            if (preSubCombNo >= 0)
                            {
                                if (!combID.Contains("|" + ord.Combo.ID + "|"))
                                {
                                    preSubCombNo += 1;
                                    ord.SubCombNO = preSubCombNo;

                                    combID = combID + "|" + ord.Combo.ID + "|";
                                }
                                else
                                {
                                    ord.SubCombNO = preSubCombNo;
                                }
                            }

                            this.AddObjectToFarpoint(ord, rowIndex, sheetIndex, EnumOrderFieldList.Item);
                        }
                    }

                    this.neuSpread1.Sheets[sheetIndex].ClearSelection();
                }
                else
                {
                    ucOutPatientItemSelect1.MessageBoxShow("请选择多条！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 校验组合医嘱
        /// </summary>
        /// <returns></returns>
        private int ValidComboOrder()
        {
            if (!this.ynGetSysClassControl)
            {
                //isDecSysClassWhenGetRecipeNO = ctrlMgr.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, false);
                isDecSysClassWhenGetRecipeNO = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, "0"));
            }

            FS.HISFC.Models.Order.Frequency frequency = null;//频次
            FS.FrameWork.Models.NeuObject usage = null;//用法
            FS.FrameWork.Models.NeuObject exeDept = null;//执行科室

            decimal amount = 0;//数量
            string sysclass = "-1";//类别
            decimal days = 0;//草药付数
            string sample = "";//样本
            decimal injectCount = 0;//院注次数
            string jpNum = "";
            //草药的煎药方式
            string PCCUsage = "";

            ArrayList alItems = new ArrayList();

            FS.HISFC.Models.Order.OutPatient.Order o = null;
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    if (o.ID != "")
                    {
                        //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        FS.HISFC.Models.Order.OutPatient.Order tem = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, o.ID);
                        if (tem.Status != 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(o.Item.Name + "已经收费，不可以组合用！");
                            return -1;
                        }
                    }
                    if (o.Status != 0)
                    {
                        return -1;
                    }
                    if (o.IsSubtbl)
                    {
                        continue;
                    }
                    if (o.Item.SysClass.ID.ToString() == "UL")//化验项目判断是否可以并管，可以的才可以组合
                    {
                        alItems.Add(o.Item.ID);
                    }

                    if (frequency == null)
                    {
                        frequency = o.Frequency.Clone();
                        usage = o.Usage.Clone();
                        sysclass = o.Item.SysClass.ID.ToString();
                        exeDept = o.ExeDept.Clone();
                        amount = o.Qty;
                        days = o.HerbalQty;
                        sample = o.Sample.Name;
                        injectCount = o.InjectCount;
                        jpNum = o.ExtendFlag1;
                        PCCUsage = o.Memo;
                    }
                    else
                    {
                        if (o.Frequency.ID != frequency.ID)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("频次不同，不可以组合用！");
                            return -1;
                        }
                        if (o.InjectCount != injectCount)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("院注次数不同，不可以组合用！");
                            return -1;
                        }
                        //if (o.Item.IsPharmacy)		//只对药品判断用法是否相同
                        if (o.Item.ItemType == EnumItemType.Drug)		//只对药品判断用法是否相同
                        {
                            #region 用法判断

                            try
                            {
                                if (o.Item.SysClass.ID.ToString() != "PCC")
                                {
                                    if (!Classes.Function.IsSameUsage(o.Usage.ID, usage.ID))
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow("用法不同，不可以进行组合！");
                                        return -1;
                                    }
                                }
                                //注释 by zhaorong at 2013-7-29 煎药方式不同的情况允许组合
                                //else
                                //{
                                //    if (o.Memo != PCCUsage)
                                //    {
                                //        ucOutPatientItemSelect1.MessageBoxShow("煎药方式不同，不可以进行组合！");
                                //        return -1;
                                //    }
                                //}
                            }
                            catch (Exception ex)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                            }
                            #endregion

                            if (o.Item.SysClass.ID.ToString() == "PCC" || o.Item.SysClass.ID.ToString() == "C")
                            {
                                if (o.HerbalQty != days)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("草药付数不同，不可以组合用！");
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            if (o.Item.SysClass.ID.ToString() == "UL")//检验
                            {
                                if (o.Qty != amount)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("检验数量不同，不可以组合用！");
                                    return -1;
                                }
                                if (o.Sample.Name != sample)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("检验样本不同，不可以组合用！");
                                    return -1;
                                }
                            }
                        }

                        if (isDecSysClassWhenGetRecipeNO)
                        {
                            if ("PCZ,P.PCC".Contains(o.Item.SysClass.ID.ToString()) &&
                                "PCZ,P.PCC".Contains(sysclass))
                            {
                                //西药和成药允许组合
                            }
                            else
                            {
                                if (("PCZ,P,PCC,UL".Contains(o.Item.SysClass.ID.ToString()) || ("PCZ,P,PCC,UL".Contains(sysclass)) && o.Item.SysClass.ID.ToString() != sysclass))
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("系统类别不同，不可以组合用！");
                                    return -1;

                                }
                                else if (o.Item.SysClass.ID.ToString() != sysclass && !o.Item.SysClass.ID.Equals("UT") && !sysclass.Equals("UT"))
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("系统类别不同，不可以组合用！");
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            if (o.Item.SysClass.ID.ToString() != sysclass)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow("项目类别不同，不可以组合用！");
                                return -1;
                            }
                        }

                        if (o.ExeDept.ID != exeDept.ID)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("执行科室不同，不能组合使用!", "提示");
                            return -1;
                        }

                    }
                }
            }

            return 0;

        }

        protected ArrayList GetSelectedRows()
        {
            ArrayList rows = new ArrayList();

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    rows.Add(i);
                }
            }
            return rows;
        }

        /// <summary>
        /// 添加院内注射次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isCanModifiedInjectNum">是否可以修改院注</param>
        private void AddInjectNum(FS.HISFC.Models.Order.OutPatient.Order sender, bool isCanModifiedInjectNum)
        {
            //暂时没有限制用法，不管什么项目只要开立了此用法都收辅材
            //if (!Classes.Function.hsUsageAndSub.Contains(sender.Usage.ID))
            if (!Classes.Function.CheckIsInjectUsage(sender.Usage.ID))
            {
                return;
            }

            //弹出院注用法窗口
            Forms.frmInputInjectNum formInputInjectNum = new Forms.frmInputInjectNum();
            formInputInjectNum.Order = sender;
            //if (formInputInjectNum.Order.DoseUnit == null && formInputInjectNum.Order.Item.IsPharmacy)
            if (formInputInjectNum.Order.DoseUnit == null && formInputInjectNum.Order.Item.ItemType == EnumItemType.Drug)
            {
                formInputInjectNum.Order.DoseUnit = ((FS.HISFC.Models.Pharmacy.Item)formInputInjectNum.Order.Item).DoseUnit;
            }
            formInputInjectNum.InjectNum = sender.InjectCount;
            if (sender.InjectCount == 0)
            {
                //设置默认的院注次数为总量/每次量
                int injectNumTmp = FS.FrameWork.Function.NConvert.ToInt32(sender.Item.Qty * ((FS.HISFC.Models.Pharmacy.Item)sender.Item).BaseDose / sender.DoseOnce);
                formInputInjectNum.InjectNum = injectNumTmp;

                DialogResult r = ucOutPatientItemSelect1.MessageBoxShow("该药品是否为院内注射？", "[提示]", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.No)
                {
                    this.ucOutPatientItemSelect1.ucInputItem1.Focus();
                    return;
                }
            }
            if (isCanModifiedInjectNum)
            {
                formInputInjectNum.ShowDialog();
            }
            if (this.ucOutPatientItemSelect1.ucOrderInputByType1.Order != null)
            {
                this.ucOutPatientItemSelect1.ucOrderInputByType1.Order.InjectCount = sender.InjectCount;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                if (order == null)
                    continue;
                if (order.Combo.ID == sender.Combo.ID)
                {
                    order.ExtendFlag1 = sender.ExtendFlag1;
                    order.InjectCount = sender.InjectCount;
                    order.NurseStation.User02 = "C";//修改过院注

                    #region 只要是保存过的医嘱，添加院注就需要删除原来的附材{F67E089F-1993-4652-8627-300295AAED8C}

                    if (sender.ID != null && sender.ID != null)
                    {
                        if (!hsComboChange.ContainsKey(sender.ID))
                        {
                            hsComboChange.Add(sender.ID, sender.Combo.ID);
                        }
                    }
                    #endregion

                    this.ucOutPatientItemSelect1.CurrOrder.NurseStation.User02 = "C";
                    this.ucOutPatientItemSelect1.CurrOrder.ExtendFlag1 = sender.ExtendFlag1;
                    this.AddObjectToFarpoint(order, i, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                }
            }

            this.RefreshOrderState();
        }

        #region 预扣库存
        ///// <summary>
        ///// 预扣库存
        ///// </summary>
        ///// <param name="CacheManager.PhaIntegrate"></param>
        ///// <param name="qty">1时，插入；-1时，删除</param>
        ///// <returns></returns>
        //private int UpdateStockPre(FS.HISFC.Models.Order.OutPatient.Order order, decimal qty, ref string errInfo)
        //{
        //    if (order.Item.ItemType == EnumItemType.Drug)
        //    {
        //        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
        //        applyOut.ID = order.ID;
        //        applyOut.StockDept.ID = order.StockDept.ID;
        //        applyOut.SystemType = "O1";//门诊医嘱类型
        //        applyOut.Item.ID = order.Item.ID;
        //        applyOut.Item.Name = order.Item.Name;
        //        applyOut.Item.Specs = order.Item.Specs;
        //        applyOut.Operation.ApplyQty = order.Qty;
        //        applyOut.Days = order.HerbalQty;
        //        applyOut.Operation.ApplyOper.ID = order.ReciptDoctor.ID;
        //        applyOut.Operation.ApplyOper.OperTime = order.MOTime;
        //        applyOut.PatientNO = order.Patient.ID;
        //        if (CacheManager.PhaIntegrate.UpdateStockinfoPreOutNum(applyOut, qty, applyOut.Days) == -1)
        //        {
        //            errInfo = CacheManager.PhaIntegrate.Err;
        //            return -1;
        //        }
        //    }
        //    return 0;
        //}


        /// <summary>
        /// 处理预扣库存
        /// </summary>
        /// <param name="isDelete"></param>
        /// <param name="outOrder"></param>
        /// <returns></returns>
        private int DealPreStock(bool isDelete, FS.HISFC.Models.Order.OutPatient.Order outOrder)
        {
            #region 非药品就别往这个方法走
            if (outOrder.Item.ItemType == EnumItemType.UnDrug || outOrder.Item.ItemType == EnumItemType.MatItem)
            {
                return 1;
            }
            #endregion


            //2013-6-20 整理，采用新方式实现
            int rev = this.phaIntegrate.DeletePreoutStore(outOrder);
            if (rev == -1)
            {
                errInfo = "处理门诊药品预扣失败！\r\n" + phaIntegrate.Err;
                return -1;
            }

            if (!isDelete)
            {
                rev = phaIntegrate.InsertPreoutStore(outOrder);
                if (rev == -1)
                {
                    errInfo = "处理门诊药品预扣失败！\r\n" + phaIntegrate.Err;
                    return -1;
                }
            }
            return 1;
        }
        #endregion

        /// <summary>
        /// 从消耗品和医嘱数组中移除医嘱
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="alOrderAndSub"></param>
        private void RemoveOrderFromArray(ArrayList alOrder, ref ArrayList alOrderAndSub)
        {
            if (alOrder == null || alOrder.Count == 0)
            {
                return;
            }
            if (alOrderAndSub == null || alOrderAndSub.Count == 0)
            {
                return;
            }
            ArrayList alTemp = new ArrayList();
            for (int i = 0; i < alOrderAndSub.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item = alOrderAndSub[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                for (int j = 0; j < alOrder.Count; j++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as FS.HISFC.Models.Order.OutPatient.Order;
                    if (temp.ID == item.Order.ID)
                    {
                        item.Item.MinFee.User03 = "1";
                    }
                }
            }
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
            {
                if (item.Item.MinFee.User03 != "1")
                {
                    alTemp.Add(item);
                }
            }
            alOrderAndSub = alTemp;
        }

        /// <summary>
        /// 保存医嘱顺序号
        /// </summary>
        /// <returns></returns>
        private int SaveSortID(int k)
        {
            return 1;
            //return 1;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(CacheManager.OrderMgr.Connection);
            //t.BeginTransaction();
            CacheManager.OutOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.neuSpread1.Sheets[k].Rows.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order ord = this.GetObjectFromFarPoint(i, k);
                ord.SortID = this.neuSpread1.Sheets[k].Rows.Count - i;
                int iReturn = -1;
                iReturn = CacheManager.OutOrderMgr.UpdateOrderSortID(ord.ID, ord.SortID, this.Patient.ID);
                if (iReturn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    ucOutPatientItemSelect1.MessageBoxShow(CacheManager.OutOrderMgr.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }

        /// <summary>
        /// 所有附材
        /// </summary>
        ArrayList alSubOrders = new ArrayList();

        /// <summary>
        /// 获取界面上所有的医嘱列表
        /// </summary>
        /// <returns></returns>
        private decimal GetAllFee(ref ArrayList alFeeDetail)
        {
            try
            {
                #region 用seeno计算费用
                if (isCountFeeBySeeNo)
                {
                    #region 获取所有医嘱

                    ArrayList alOrder = new ArrayList();

                    FS.HISFC.Models.Order.OutPatient.Order order = new FS.HISFC.Models.Order.OutPatient.Order();


                    for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                    {
                        order = this.GetObjectFromFarPoint(i, 0);
                        order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                        if (order.ID == "" && !string.Equals(order.Item.ID, "999")) //new 新加的医嘱
                        {
                            alOrder.Add(order);
                        }
                        else //update 更新的医嘱
                        {
                            #region 获得需要更新的医嘱
                            //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                            FS.HISFC.Models.Order.OutPatient.Order newOrder = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                            //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                            if (!string.Equals(order.Item.ID, "999") && newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            alOrder.Add(newOrder);

                            #endregion
                        }
                    }
                    #endregion

                    decimal totCost = 0;

                    if (!this.IsDesignMode)
                    {
                        Hashtable hsSeeNO = new Hashtable();
                        //ArrayList 
                        alFeeDetail = new ArrayList();

                        if (alOrder != null && alOrder.Count > 0)
                        {
                            foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                            {
                                ArrayList alTemp = new ArrayList();
                                if (!string.IsNullOrEmpty(orderObj.SeeNO) && !hsSeeNO.Contains(orderObj.SeeNO))
                                {
                                    hsSeeNO.Add(orderObj.SeeNO, orderObj);

                                    alTemp = CacheManager.FeeIntegrate.QueryFeeDetailByClinicCodeAndSeeNO(this.Patient.ID, orderObj.SeeNO, "ALL");
                                    if (alTemp == null)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow(CacheManager.FeeIntegrate.Err);
                                        return 0;
                                    }
                                    alFeeDetail.AddRange(alTemp);
                                }
                            }
                        }
                        else
                        {
                            ArrayList alTemp = new ArrayList();
                            alTemp = CacheManager.FeeIntegrate.QueryFeeDetailByClinicCodeAndSeeNONotNull(this.Patient.ID, "ALL");
                            if (alTemp == null)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.FeeIntegrate.Err);
                                return 0;
                            }
                            alFeeDetail.AddRange(alTemp);
                        }

                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeDetail)
                        {
                            totCost += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                        }
                        return totCost;
                    }

                    else
                    {
                        #region 计算费用

                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                        {
                            if (orderObj.MinunitFlag == "")//user03为空,说明不知道开立的什么单位 默认为最小单位
                            {
                                orderObj.MinunitFlag = "1";//默认
                            }
                            if (orderObj.MinunitFlag != "1")//开立最小单位 
                            {
                                totCost = orderObj.Qty * orderObj.Item.Price + totCost;
                            }
                            else
                            {
                                totCost = orderObj.Qty * orderObj.Item.Price / orderObj.Item.PackQty + totCost;
                            }

                            totCost = GetCost(totCost);

                        }

                        #endregion

                        alFeeDetail = null;
                    }

                    return totCost;
                }
                #endregion

                #region 用ReciptSequence计算费用
                else
                {
                    #region 获取所有医嘱

                    ArrayList alOrder = new ArrayList();

                    FS.HISFC.Models.Order.OutPatient.Order order = new FS.HISFC.Models.Order.OutPatient.Order();

                    for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                    {
                        order = this.GetObjectFromFarPoint(i, 0);

                        order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                        if (!string.Equals(order.Item.ID, "999"))
                        {
                            alOrder.Add(order);
                        }
                    }
                    #endregion

                    decimal totCost = 0;

                    //查询模式的时候（非开立状态）查询所对应的费用信息
                    if (!this.IsDesignMode)
                    {
                        Hashtable hsRecipeSeq = new Hashtable();
                        //ArrayList 
                        alFeeDetail = new ArrayList();
                        if (alOrder != null && alOrder.Count > 0)
                        {
                            foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                            {
                                ArrayList alTemp = new ArrayList();
                                if (!string.IsNullOrEmpty(orderObj.ReciptSequence) && !hsRecipeSeq.Contains(orderObj.ReciptSequence))
                                {
                                    hsRecipeSeq.Add(orderObj.ReciptSequence, orderObj);

                                    alTemp = CacheManager.FeeIntegrate.QueryFeeDetailByClinicCodeAndRecipeSeq(this.Patient.ID, orderObj.ReciptSequence, "ALL");
                                    if (alTemp == null)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow(CacheManager.FeeIntegrate.Err);
                                        return 0;
                                    }
                                    alFeeDetail.AddRange(alTemp);
                                }
                            }
                        }
                        else
                        {
                            ArrayList alTemp = new ArrayList();
                            alTemp = CacheManager.OutFeeMgr.QueryFeeDetailByClinicCode(this.Patient.ID, "ALL");
                            if (alTemp == null)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.FeeIntegrate.Err);
                                return 0;
                            }
                            alFeeDetail.AddRange(alTemp);
                        }

                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeDetail)
                        {
                            totCost += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                        }
                        return totCost;
                    }
                    //开立状态显示界面上的费用信息
                    else
                    {
                        #region 计算费用

                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                        {
                            if (orderObj.MinunitFlag == "")//user03为空,说明不知道开立的什么单位 默认为最小单位
                            {
                                orderObj.MinunitFlag = "1";//默认
                            }
                            if (orderObj.MinunitFlag != "1")//开立最小单位 
                            {
                                totCost = orderObj.Qty * orderObj.Item.Price + totCost;
                            }
                            else
                            {
                                totCost = orderObj.Qty * orderObj.Item.Price / orderObj.Item.PackQty + totCost;
                            }

                            totCost = GetCost(totCost);
                        }

                        #endregion

                        alFeeDetail = null;


                        alFeeDetail = new ArrayList();

                        FS.HISFC.Models.Fee.Outpatient.FeeItemList item = null;
                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                        {
                            if (orderObj.MinunitFlag == "")//user03为空,说明不知道开立的什么单位 默认为最小单位
                            {
                                orderObj.MinunitFlag = "1";//默认
                            }
                            item = Classes.Function.ChangeToFeeItemList(GetOrderPactInfo(orderObj), currentPatientInfo);

                            alFeeDetail.Add(item);

                            //if (orderObj.MinunitFlag != "1")//开立最小单位 
                            //{
                            //    totCost = orderObj.Qty * orderObj.Item.Price + totCost;
                            //}
                            //else
                            //{
                            //    totCost = orderObj.Qty * orderObj.Item.Price / orderObj.Item.PackQty + totCost;
                            //    totCost = FS.FrameWork.Public.String.FormatNumber(totCost, 2);
                            //}
                        }
                    }

                    return totCost;
                }
                #endregion
            }
            catch (Exception ex)
            {
                alFeeDetail = null;
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 是否已提示费用报警
        /// </summary>
        bool isShowFeeWarning = false;

        /// <summary>
        /// 计算的费用信息
        /// </summary>
        decimal[] alFeeMoneyInfo = null;

        FS.HISFC.BizLogic.Manager.PactStatRelation myRelation = new FS.HISFC.BizLogic.Manager.PactStatRelation();

        /// <summary>
        /// 医嘱费用提示条
        /// </summary>
        /// <param name="isShowSIFeeInfo">是否显示医保报销信息，考虑效率问题正常开立不计算，保存和查询的时候显示</param>
        /// <param name="isRequery">对于已收费报销信息，是否重新查询，开立项目时不重新查询</param>
        private void SetOrderFeeDisplay(bool isShowSIFeeInfo, bool isRequery)
        {
            decimal totcost = 0;
            if (!this.EditGroup && this.currentPatientInfo != null)
            {
                if (this.currentPatientInfo.ID.Length > 0)
                {
                    this.pnDisplay.Visible = true;

                    ArrayList alFeeList = new ArrayList();
                    totcost = this.GetAllFee(ref alFeeList);

                    //总金额
                    decimal decTotalMoney = 0;
                    //报销金额
                    decimal decPubMoney = 0;
                    //自费金额
                    decimal decOwnMoney = 0;
                    //减免金额
                    decimal decRebateMoney = 0;

                    // 累计总金额
                    decimal decTotalMoneyAddUp = 0;
                    // 累计报销金额
                    decimal decPubMoneyAddUp = 0;
                    // 累计自费金额
                    decimal decOwnMoneyAddUp = 0;
                    // 累计减免金额
                    decimal decRebateMoneyAddUp = 0;

                    string errInfo = "";
                    int rev = -1;

                    //优化效率时，可以屏蔽，有时实时显示费用较慢
                    //if (isShowSIFeeInfo)
                    //{
                    if (alFeeList != null && alFeeList.Count > 0)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        //由于费用那里 医保接口计算总是出错，而我这里只是显示费用
                        //对于出错的不再处理，直接按照总费用显示

                        //ArrayList alFeeMoneyInfo = null;

                        rev = CacheManager.FeeIntegrate.CalculatOrderFee(this.Patient, alFeeList, isRequery, ref alFeeMoneyInfo, ref errInfo);
                    }
                    else
                    {
                        rev = CacheManager.FeeIntegrate.CalculatOrderFee(this.Patient, alFeeList, false, ref alFeeMoneyInfo, ref errInfo);
                    }

                    //发票类别显示金额
                    string displayTotFee = Classes.Function.GetFeeInfo(alFeeList);

                    if (rev <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        decOwnMoney = 0;
                        decPubMoney = 0;
                        decTotalMoney = 0;
                        decRebateMoney = 0;
                        decPubMoneyAddUp = 0;
                        //ucOutPatientItemSelect1.MessageBoxShow(CacheManager.FeeIntegrate.Err + errInfo);
                        //ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                        //return;
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();

                        if (alFeeMoneyInfo != null && alFeeMoneyInfo.Length >= 8)
                        {
                            decTotalMoneyAddUp = FS.FrameWork.Function.NConvert.ToDecimal(alFeeMoneyInfo[0]);
                            decPubMoneyAddUp = FS.FrameWork.Function.NConvert.ToDecimal(alFeeMoneyInfo[1]);
                            decOwnMoneyAddUp = FS.FrameWork.Function.NConvert.ToDecimal(alFeeMoneyInfo[2]);
                            decRebateMoneyAddUp = FS.FrameWork.Function.NConvert.ToDecimal(alFeeMoneyInfo[3]);

                            decTotalMoney = FS.FrameWork.Function.NConvert.ToDecimal(alFeeMoneyInfo[4]);
                            decPubMoney = FS.FrameWork.Function.NConvert.ToDecimal(alFeeMoneyInfo[5]);
                            decOwnMoney = FS.FrameWork.Function.NConvert.ToDecimal(alFeeMoneyInfo[6]);
                            decRebateMoney = FS.FrameWork.Function.NConvert.ToDecimal(alFeeMoneyInfo[7]);
                        }
                    }

                    string accountVacancy = "";
                    if (this.isAccountMode)
                    {
                        accountVacancy = "账户余额：" + this.vacancyDisplay;
                        this.pnTop.Height = 80;
                    }
                    else
                    {
                        this.pnTop.Height = 60;
                    }

                    string showInfo = currentPatientInfo.PID.CardNO + "  " + currentPatientInfo.Name + "  " + this.currentPatientInfo.Sex.Name + "  " + CacheManager.OutOrderMgr.GetAge(this.currentPatientInfo.Birthday) + "  " + currentPatientInfo.Pact.Name;

                    showInfo += "\r\n诊断:" + GetDiagInfo();

                    //所有累计和现有金额的报销金额、减免金额>0 就给出提示
                    if (decPubMoneyAddUp + decRebateMoneyAddUp + decPubMoney + decRebateMoney > 0)
                    {
                        showInfo += "\r\n" + accountVacancy + "总费用:" + FS.FrameWork.Public.String.ToSimpleString(decTotalMoney, 2) +
                            "元 自费金额:" + FS.FrameWork.Public.String.ToSimpleString((decOwnMoney - decRebateMoney), 2) +
                           "元 报销金额:" + FS.FrameWork.Public.String.ToSimpleString(decPubMoney, 2) +
                           "元 减免金额:" + FS.FrameWork.Public.String.ToSimpleString(decRebateMoney, 2) + "元 \r\n" +

                           "当日累计费用总额:" + FS.FrameWork.Public.String.ToSimpleString(decTotalMoneyAddUp, 2) +
                           "元 累计自费金额:" + FS.FrameWork.Public.String.ToSimpleString((decOwnMoneyAddUp - decRebateMoneyAddUp), 2) +
                           "元 累计报销金额:" + FS.FrameWork.Public.String.ToSimpleString(decPubMoneyAddUp, 2) +
                           "元 累计减免金额:" + FS.FrameWork.Public.String.ToSimpleString(decRebateMoneyAddUp, 2) + "元 ";

                        if (!string.IsNullOrEmpty(displayTotFee))
                        {
                            showInfo += "\r\n" + displayTotFee;
                        }
                        if (!isShowFeeWarning)
                        {
                            //此处用于提示报销限额等
                            if (rev == 2 && !string.IsNullOrEmpty(errInfo))
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.isShowFeeWarning = true;
                            }
                            else
                            {
                                this.isShowFeeWarning = false;
                            }
                        }
                    }
                    else
                    {
                        showInfo += "\r\n" + accountVacancy + "总费用:" + FS.FrameWork.Public.String.ToSimpleString(totcost, 2) + "元 ";
                        if (!string.IsNullOrEmpty(displayTotFee))
                        {
                            showInfo += " 其中" + displayTotFee;
                        }
                    }

                    this.txtInfo.Text = showInfo;
                }
                else
                {
                    txtInfo.Text = "";
                    pnTop.Visible = false;
                }
            }
            else
            {
                txtInfo.Text = "";
                pnTop.Visible = false;
            }
        }

        /// <summary>
        /// 获得诊断信息
        /// </summary>
        /// <returns></returns>
        private string GetDiagInfo()
        {
            ArrayList al = CacheManager.DiagMgr.QueryCaseDiagnoseForClinic(this.Patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + CacheManager.DiagMgr.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }

            string strDiag = "";
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "、";
                }
            }
            strDiag = strDiag.TrimEnd('、');
            if (string.IsNullOrEmpty(strDiag))
            {
                strDiag = "无";
            }
            return strDiag;
        }

        /// <summary>
        /// 修改草药{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        public void ModifyHerbal()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                return;
            }

            ArrayList alModifyHerbal = new ArrayList(); //要修改的草药医嘱

            FS.HISFC.Models.Order.OutPatient.Order orderTemp = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as
                FS.HISFC.Models.Order.OutPatient.Order;

            if (orderTemp == null)
            {
                return;
            }

            //{F1706DB9-376D-433e-A5A9-1E1EEA46733C}  仅能修改草药医嘱
            if (orderTemp.Item.ItemType == EnumItemType.Drug)
            {
                if (((FS.HISFC.Models.Pharmacy.Item)orderTemp.Item).SysClass.ID.ToString() != "PCC")
                {
                    ucOutPatientItemSelect1.MessageBoxShow("请选择草药医嘱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            if (string.IsNullOrEmpty(orderTemp.Combo.ID))
            {
                alModifyHerbal.Add(orderTemp);
            }
            else
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[i].Tag as
                        FS.HISFC.Models.Order.OutPatient.Order;
                    if (order == null)
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(order.Combo.ID))
                    {
                        continue;
                    }
                    if (order.Status != 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("医嘱已收费，不可修改！\n请复制医嘱后在新医嘱上修改！", 411);
                        return;
                    }
                    if (order.Combo.ID == orderTemp.Combo.ID)
                    {
                        alModifyHerbal.Add(order);
                    }
                }
            }

            if (alModifyHerbal.Count > 0)
            {
                if (ucHerbal == null)
                {
                    ucHerbal = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID);
                }

                //using (FS.HISFC.Components.Order.Controls.ucHerbalOrder uc = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
                //{
                ucHerbal.Patient = new FS.HISFC.Models.RADT.PatientInfo();//
                ucHerbal.IsClinic = true;
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                ucHerbal.AlOrder = alModifyHerbal;
                ucHerbal.OpenType = FS.HISFC.Components.Order.Controls.EnumOpenType.Modified; //修改
                ucHerbal.SetFocus();
                DialogResult r = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucHerbal);

                if (ucHerbal.IsCancel == true)
                {
                    //取消了
                    return;
                }

                if (ucHerbal.OpenType == FS.HISFC.Components.Order.Controls.EnumOpenType.Modified)
                {
                    //改为新加模式就不删除了
                    if (this.Del(this.neuSpread1.ActiveSheet.ActiveRowIndex, true) < 0)
                    {
                        //删除原医嘱不成功
                        return;
                    }
                }


                AddNewHerbalOrder();
                //}
            }

        }

        #region {C6E229AC-A1C4-4725-BBBB-4837E869754E}

        /// <summary>
        /// 组套存储
        /// </summary>
        private void SaveGroup()
        {
            FS.HISFC.Components.Common.Forms.frmOrderGroupManager group = new FS.HISFC.Components.Common.Forms.frmOrderGroupManager();
            group.InpatientType = FS.HISFC.Models.Base.ServiceTypes.C;
            try
            {
                group.IsManager = CacheManager.LogEmpl.IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    FS.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Clone();
                    if (order == null)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("获得医嘱出错！");
                    }
                    else
                    {
                        string s = order.Item.Name;
                        string sno = order.Combo.ID;
                        //保存医嘱组套 默认开立时间为 零点
                        order.BeginTime = new DateTime(order.BeginTime.Year, order.BeginTime.Month, order.BeginTime.Day, 0, 0, 0);
                        al.Add(order);
                    }
                }
            }
            if (al.Count > 0)
            {
                group.alItems = al;
                group.ShowDialog();
                if (OnRefreshGroupTree != null)
                {
                    this.OnRefreshGroupTree(null, null);
                }
            }
        }

        #endregion

        #endregion

        #region 公有方法
        /// <summary>
        /// 获得医嘱实体从FarPoint
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order GetObjectFromFarPoint(int i, int SheetIndex)
        {
            FS.HISFC.Models.Order.OutPatient.Order order = null;
            if (this.neuSpread1.Sheets[SheetIndex].Rows[i].Tag != null)
            {
                order = this.neuSpread1.Sheets[SheetIndex].Rows[i].Tag as FS.HISFC.Models.Order.OutPatient.Order;
            }
            else
            {
                if (string.IsNullOrEmpty(this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("医嘱流水号")].Text))
                {
                    return null;
                }

                if (this.hsOrder.Contains(this.Patient.DoctorInfo.SeeNO + this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("医嘱流水号")].Text))
                {
                    order = this.hsOrder[this.Patient.DoctorInfo.SeeNO + this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("医嘱流水号")].Text] as FS.HISFC.Models.Order.OutPatient.Order;
                }
                else
                {
                    //增加clinic_code优化查询速率{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                    order = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("医嘱流水号")].Text);
                }
            }

            return order;
        }

        /// <summary>
        /// 添加新医嘱
        /// </summary>
        /// <param name="sender"></param>
        public void AddNewOrder(object sender, int SheetIndex)
        {
            dirty = true;
            FS.HISFC.Models.Order.OutPatient.Order newOrder = null;
            if (sender.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
            {
                newOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                newOrder.Name = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Name;
                newOrder.Memo = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Memo;
                newOrder.Combo = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Combo;
                newOrder.DoseOnce = ((FS.HISFC.Models.Order.OutPatient.Order)sender).DoseOnce;
                newOrder.DoseUnit = ((FS.HISFC.Models.Order.OutPatient.Order)sender).DoseUnit;
                newOrder.ExeDept = ((FS.HISFC.Models.Order.OutPatient.Order)sender).ExeDept.Clone();
                newOrder.Frequency = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Frequency.Clone();
                newOrder.StockDept = ((FS.HISFC.Models.Order.OutPatient.Order)sender).StockDept.Clone();
                newOrder.ApplyNo = ((FS.HISFC.Models.Order.OutPatient.Order)sender).ApplyNo;

                newOrder.HerbalQty = ((FS.HISFC.Models.Order.OutPatient.Order)sender).HerbalQty;
                newOrder.IsEmergency = ((FS.HISFC.Models.Order.OutPatient.Order)sender).IsEmergency;
                newOrder.Item = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Item;
                newOrder.Qty = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Qty;

                //组套中如果非药品项目数量为零，系统在上面录入界面默认显示为1，医生在不修改的情况下保存，提示数量为0
                //modified by  houwb 2011-3-18 0:02:54
                if (newOrder.Item.ItemType != EnumItemType.Drug && newOrder.Qty == 0)
                {
                    newOrder.Qty = 1;
                }
                newOrder.Note = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Note;
                if (((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "UL")
                {
                    newOrder.Sample.Name = ((FS.HISFC.Models.Order.OutPatient.Order)sender).CheckPartRecord;
                }
                else if (((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "UC")
                {
                    newOrder.CheckPartRecord = ((FS.HISFC.Models.Order.OutPatient.Order)sender).CheckPartRecord;
                }
                newOrder.Unit = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Unit;

                //此处判断停用的用法赋空值
                newOrder.Usage = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Usage;
                if (Classes.Function.usageHelper == null)
                {
                    ArrayList alUsage = CacheManager.GetConList("USAGE");
                    Classes.Function.usageHelper = new FS.FrameWork.Public.ObjectHelper(alUsage);
                }
                if (Classes.Function.usageHelper.GetObjectFromID(newOrder.Usage.ID) == null)
                {
                    newOrder.Usage = new FS.FrameWork.Models.NeuObject();
                }

                newOrder.IsNeedConfirm = ((FS.HISFC.Models.Order.OutPatient.Order)sender).IsNeedConfirm;
                if (((FS.HISFC.Models.Order.OutPatient.Order)sender).MinunitFlag == ""
                    || ((FS.HISFC.Models.Order.OutPatient.Order)sender).MinunitFlag == null)
                {
                    newOrder.MinunitFlag = "1";//最小单位
                }
                else
                {
                    newOrder.MinunitFlag = ((FS.HISFC.Models.Order.OutPatient.Order)sender).MinunitFlag;
                }

                newOrder.Sample = ((FS.HISFC.Models.Order.OutPatient.Order)sender).Sample;
                newOrder.CheckPartRecord = ((FS.HISFC.Models.Order.OutPatient.Order)sender).CheckPartRecord;
                newOrder.InjectCount = ((FS.HISFC.Models.Order.OutPatient.Order)sender).InjectCount;
                newOrder.DoseOnceDisplay = ((FS.HISFC.Models.Order.OutPatient.Order)sender).DoseOnceDisplay;
                sender = newOrder;

            }
            //添加新行
            if (sender.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
            {

                #region 检查添加的东西
                if (((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "UC")//检查
                {
                    //打印检查申请单
                    ////this.AddTest(sender);
                }
                else if (((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "MC")//会诊
                {
                    //添加会诊申请
                    ////this.AddConsultation(sender);
                }

                #region 皮试
                if (((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.ItemType == EnumItemType.Drug)//药品
                {
                    if (((FS.HISFC.Models.Pharmacy.Item)((FS.HISFC.Models.Order.OutPatient.Order)sender).Item).IsAllergy)
                    {
                        //控制参数控制是否默认全部院注，全部院注不在弹出院注次数输入框
                        if (!this.isCanModifiedInjectNum)
                        {
                            if (this.hypotestMode == "1")
                            {
                                if (ucOutPatientItemSelect1.MessageBoxShow(((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.Name + "是否需要皮试！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    ((FS.HISFC.Models.Order.OutPatient.Order)sender).HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest;
                                }
                                else
                                {
                                    (sender as FS.HISFC.Models.Order.OutPatient.Order).HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                                }

                                //((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.Name += CacheManager.OrderMgr.TransHypotest(((FS.HISFC.Models.Order.OutPatient.Order)sender).HypoTest);
                            }
                            else if (this.hypotestMode == "2")//{0733E2AD-EB02-4b6f-BCF8-1A6ED5A2EFAD}
                            {

                                HISFC.Components.Order.OutPatient.Forms.frmHypoTest frmHypotest = new FS.HISFC.Components.Order.OutPatient.Forms.frmHypoTest();

                                frmHypotest.IsEditMode = true;
                                frmHypotest.Hypotest = 1;
                                frmHypotest.ItemName = ((FS.HISFC.Models.Pharmacy.Item)((FS.HISFC.Models.Order.OutPatient.Order)sender).Item).Name + " " + ((FS.HISFC.Models.Pharmacy.Item)((FS.HISFC.Models.Order.OutPatient.Order)sender).Item).Specs;
                                frmHypotest.ShowDialog();

                                ((FS.HISFC.Models.Order.OutPatient.Order)sender).HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)frmHypotest.Hypotest;
                            }
                        }
                    }
                }
                else
                {
                    ((FS.HISFC.Models.Order.OutPatient.Order)sender).HypoTest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                }
                #endregion
                #endregion

                FS.HISFC.Models.Order.OutPatient.Order order = sender as FS.HISFC.Models.Order.OutPatient.Order;

                if (order.MinunitFlag == "")
                {
                    order.MinunitFlag = "1";//最小单位
                }
                if (this.GetReciptDept() != null)
                {
                    order.ReciptDept = this.GetReciptDept().Clone();
                }
                if (this.GetReciptDoct() != null)
                {
                    order.ReciptDoctor = this.GetReciptDoct().Clone();
                }
                if (order.ExeDept == null || string.IsNullOrEmpty(order.ExeDept.ID))
                {
                    order.ExeDept = this.GetReciptDept().Clone();
                }
                else
                {
                    order.ExeDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ExeDept.ID);
                }

                #region 重新获取取药药房
                // 组套开立取药药房可能为空
                if (newOrder.Item.ItemType == EnumItemType.Drug)
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp = new FS.HISFC.Models.Order.OutPatient.Order();
                    temp.Item = newOrder.Item;
                    temp.ReciptDept = newOrder.ReciptDept;

                    if (!this.EditGroup)
                    {
                        if (Classes.Function.FillDrugItem(null, currentPatientInfo, ref temp, ref errInfo) <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    //对于从项目中选择的药品，已经存在取药科室的，不再重取 houwb 2011-5-30
                    if (!string.IsNullOrEmpty(newOrder.Item.User02))
                    {
                        newOrder.StockDept.ID = newOrder.Item.User02;
                    }
                    else if (!string.IsNullOrEmpty(temp.StockDept.ID))
                    {
                        newOrder.StockDept.ID = temp.StockDept.ID;
                    }
                    newOrder.StockDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(temp.StockDept.ID);

                    //判断药品是否毒麻药，给提示 
                    //string classCode = FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality.ID);
                    //if (classCode == "P" || classCode == "P1" || classCode == "S1")//classCode.Contains("S") || 
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow("【" + order.Item.Name + "】属于毒麻药品，\r\n根据处方管理办法规定,请同时附加开立手工毒麻药处方!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
                #endregion


                //if (Components.Order.Classes.Function.ReComputeQty(order) == -1)
                //{
                //    ucOutPatientItemSelect1.MessageBoxShow(CacheManager.OrderMgr.Err);
                //    return;
                //}


                if (order.Combo.ID == "")
                {
                    try
                    {
                        order.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();//添加组合号
                    }
                    catch
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("获得医嘱组合号出错：\r\n" + CacheManager.OutOrderMgr.Err);
                    }
                }

                DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

                if (!this.EditGroup)
                {
                    if (this.currentPatientInfo != null)
                    {
                        order.InDept = this.currentPatientInfo.DoctorInfo.Templet.Dept;//挂号科室

                        //价格已经在获取项目最新信息中获取到了

                        //新增加的才获取最新价格
                        if (string.IsNullOrEmpty(order.ID))
                        {
                            if (order.Item.ItemType != EnumItemType.Drug)
                            {
                                FS.HISFC.Models.Base.PactInfo pactInfo = this.currentPatientInfo.Pact as FS.HISFC.Models.Base.PactInfo;
                                decimal orgPrice = 0;

                                decimal price = CacheManager.FeeIntegrate.GetPrice(order.Item.ID, currentPatientInfo, 0, order.Item.Price, order.Item.ChildPrice, order.Item.SpecialPrice, 0, ref orgPrice);
                                if (order.Item.ItemType == EnumItemType.Drug)
                                {
                                    ((FS.HISFC.Models.Pharmacy.Item)order.Item).Price = price;
                                }
                                else
                                {
                                    ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).Price = price;
                                }
                                order.Item.Price = price;
                            }
                        }
                    }
                }

                #region 设置医嘱开立时间

                order.BeginTime = Order.Classes.Function.GetDefaultMoBeginDate(3);

                if (order.User03 != "")//组套的时间间隔
                {
                    int iDays = FS.FrameWork.Function.NConvert.ToInt32(order.User03);
                    if (iDays > 0)//是时间间隔>0
                    {
                        order.BeginTime = order.BeginTime.AddDays(iDays);
                    }
                }

                #endregion

                order.CurMOTime = DateTime.MinValue;
                order.NextMOTime = DateTime.MinValue;
                order.EndTime = DateTime.MinValue;

                if (order.Sample.Name.Length <= 0 && order.Item.SysClass.ID.ToString() == "UL")
                {
                    order.Sample.Name = order.CheckPartRecord;
                }

                //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                order.SubCombNO = GetMaxSubCombNo(order);

                #region 重新处理院注次数，保证组套开立默认为最大院注

                if (newOrder.InjectCount == 0)
                {
                    //if (Classes.Function.hsUsageAndSub.Contains(newOrder.Usage.ID))
                    if (Classes.Function.CheckIsInjectUsage(newOrder.Usage.ID))
                    {
                        decimal Frequence = 0;

                        foreach (FS.HISFC.Models.Order.Frequency freObj in FS.HISFC.Components.Order.Classes.Function.HelperFrequency.ArrayObject)
                        {
                            if (newOrder.Frequency.ID == freObj.ID)
                            {
                                newOrder.Frequency = freObj.Clone();
                            }
                        }

                        if (newOrder.Frequency.Days[0] == "0" || string.IsNullOrEmpty(newOrder.Frequency.Days[0]))
                        {
                            newOrder.Frequency.Days[0] = "1";
                            Frequence = newOrder.Frequency.Times.Length;
                        }
                        else
                        {
                            try
                            {
                                Frequence = Math.Round(newOrder.Frequency.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(newOrder.Frequency.Days[0]), 2);
                            }
                            catch
                            {
                                Frequence = newOrder.Frequency.Times.Length;
                            }
                        }
                        newOrder.InjectCount = (int)Math.Ceiling((double)(Frequence * newOrder.HerbalQty));
                    }
                }

                #endregion

                this.currentOrder = order;
                //this.neuSpread1.Sheets[SheetIndex].Rows.Add(0, 1);
                this.neuSpread1.Sheets[SheetIndex].Rows.Add(neuSpread1.Sheets[SheetIndex].RowCount, 1);
                this.neuSpread1.ActiveSheet.ActiveRowIndex = neuSpread1.Sheets[SheetIndex].RowCount - 1;
                this.AddObjectToFarpoint(order, neuSpread1.ActiveSheet.ActiveRowIndex, SheetIndex, EnumOrderFieldList.Item);
                //this.AddObjectToFarpoint(order, 0, SheetIndex, EnumOrderFieldList.Item);

                #region 处理医保限制性用药
                if (Patient != null && this.Patient.Pact.PayKind.ID == "02")
                {
                    FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(GetItemUserCode(order.Item));
                    if (indicationsObj != null)
                    {
                        if (MessageBox.Show("药品【" + order.Item.Name + "】属于限制级药品，\r\n\r\n限制药品说明：【" + indicationsObj.Name + "】\r\n\r\n请确定医保报销设定。报销(是)，自费(否)?\r\n", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            //暂用名称列的tag值存标记
                            neuSpread1.ActiveSheet.Cells[neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("付数/天数")].Tag = "1";
                        }
                        else
                        {
                            //暂用名称列的tag值存标记
                            neuSpread1.ActiveSheet.Cells[neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("付数/天数")].Tag = "0";
                        }
                    }
                }

                #endregion

                //this.neuSpread1.ActiveSheet.ActiveRowIndex = 0;
                this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;

                RefreshOrderState();

                //this.RefreshCombo();
                //this.SelectionChanged();

                this.neuSpread1.ShowRow(this.neuSpread1.ActiveSheetIndex, this.neuSpread1.ActiveSheet.RowCount - 1, FarPoint.Win.Spread.VerticalPosition.Center);
            }
            else
            {
                ucOutPatientItemSelect1.MessageBoxShow("获得类型不是医嘱类型！");
            }
            dirty = false;

            #region 合理用药提示

            if (this.IReasonableMedicine != null
                && this.IReasonableMedicine.PassEnabled
                && this.enabledPass)
            {
                this.IReasonableMedicine.PassShowFloatWindow(false);

                FS.HISFC.Models.Order.OutPatient.Order info = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, 0);
                if (info == null)
                {
                    return;
                }
                if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    #region 药品查询
                    try
                    {
                        //貌似他们只和右下角的坐标位置相关
                        this.IReasonableMedicine.PassShowSingleDrugInfo(info, new Point(MousePosition.X, MousePosition.Y - 60),
                            new Point(MousePosition.X + 100, MousePosition.Y + 15), false);
                    }
                    catch (Exception ex)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                    }
                    #endregion
                }
            }

            #endregion

            #region 处理附材

            //if (dealSublMode == 1)
            //{
            if (this.currentPatientInfo != null && !string.IsNullOrEmpty(this.currentPatientInfo.ID) && currentOrder.Item.ItemType == EnumItemType.UnDrug && currentOrder.Item.SysClass.ID.ToString() != "UL" && !currentOrder.Item.MinFee.ID.Equals("028"))
            {
                dirty = true;
                if (this.IDealSubjob != null)
                {
                    IDealSubjob.IsPopForChose = false;
                    ArrayList alOrder = new ArrayList();
                    ArrayList alSubOrder = new ArrayList();
                    string errText = "";
                    alOrder.Add(currentOrder);
                    if (alOrder.Count > 0)
                    {
                        if (IDealSubjob.DealSubjob(this.Patient, alOrder, currentOrder, ref alSubOrder, ref errText) <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败！" + errText);
                            return;
                        }

                        if (alSubOrder != null && alSubOrder.Count > 0)
                        {
                            foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alSubOrder)
                            {
                                //orderObj.Combo.ID = CacheManager.OrderMgr.GetNewOrderComboID();
                                //orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                orderObj.SortID = 0;
                                orderObj.ID = "";
                                if (orderObj.SubCombNO == 0)
                                {
                                    orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                }
                                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                                this.AddObjectToFarpoint(orderObj, this.neuSpread1_Sheet1.RowCount - 1, 0, EnumOrderFieldList.Item);
                            }
                        }
                    }
                }
                dirty = false;
            }
            //}
            #endregion
        }

        /// <summary>
        /// 获得项目自定义码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetItemUserCode(Item item)
        {
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item pha = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                return pha.UserCode;
            }
            else
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                return undrug.UserCode;
            }
        }

        /// <summary>
        /// 添加草药医嘱{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        /// <param name="alHerbalOrder"></param>
        public void AddHerbalOrders(ArrayList alHerbalOrder)
        {
            //草药弹出草药开立界面
            if (ucHerbal == null)
            {
                ucHerbal = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID);
            }
            //using (FS.HISFC.Components.Order.Controls.ucHerbalOrder uc = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
            //{
            ucHerbal.Patient = new FS.HISFC.Models.RADT.PatientInfo();//
            ucHerbal.IsClinic = true;

            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
            ucHerbal.AlOrder = alHerbalOrder;
            ucHerbal.SetFocus();

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucHerbal);

            AddNewHerbalOrder();
            //}
        }

        private void AddNewHerbalOrder()
        {
            if (!ucHerbal.IsCancel && ucHerbal.AlOrder != null && ucHerbal.AlOrder.Count != 0)
            {
                //foreach (FS.HISFC.Models.Order.OutPatient.Order info in ucHerbal.AlOrder)
                for (int i = 0; i < ucHerbal.AlOrder.Count; i++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order info = ucHerbal.AlOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;
                    this.AddNewOrder(info, 0);
                }
                ucHerbal.Clear();

                RefreshOrderState();
                this.RefreshCombo();
            }
        }

        /// <summary>
        /// 层级形式开立医嘱
        /// </summary>
        public void AddLevelOrders()
        {
            using (FS.HISFC.Components.Order.Controls.ucLevelOrder uc = new FS.HISFC.Components.Order.Controls.ucLevelOrder())
            {
                uc.InOutType = 1;
                uc.Patient = new FS.HISFC.Models.RADT.PatientInfo();

                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "检验检查医嘱开立";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                {
                    foreach (FS.HISFC.Models.Order.OutPatient.Order info in uc.AlOrder)
                    {
                        this.AddNewOrder(info, 0);
                    }
                    //uc.Clear();
                    RefreshOrderState();
                    this.RefreshCombo();

                }
            }
        }

        /// <summary> 
        /// 添加手术申请
        /// </summary>
        public void AddOperation()
        {
            ////待修改
        }

        /// <summary>
        /// 获取选择的医嘱
        /// </summary>
        /// <returns></returns>
        private List<FS.HISFC.Models.Order.Order> GetSelectOrders()
        {
            List<FS.HISFC.Models.Order.Order> alOrders = new List<FS.HISFC.Models.Order.Order>();
            int iActiveSheet = 0;//检查单默认临时医嘱
            for (int i = 0; i < this.neuSpread1.Sheets[iActiveSheet].RowCount; i++)
            {
                if (this.neuSpread1.Sheets[iActiveSheet].IsSelected(i, 0))
                {
                    //将alItems内容改为order类型
                    alOrders.Add(this.GetObjectFromFarPoint(i, iActiveSheet));
                }
            }

            return alOrders;
        }

        /// <summary>
        /// 添加检查、检验申请
        /// </summary>
        public void AddTest()
        {
            if (this.Patient == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("请先选择患者！");
                return;
            }

            List<FS.HISFC.Models.Order.Order> alItems = this.GetSelectOrders();

            if (alItems.Count <= 0)
            {
                //没有选择项目信息
                ucOutPatientItemSelect1.MessageBoxShow("请选择开立的检查信息!");
                return;
            }

            List<FS.HISFC.Models.Order.Inpatient.Order> alInOrders = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            foreach (FS.HISFC.Models.Order.Order inorder in alItems)
            {
                alInOrders.Add(inorder as FS.HISFC.Models.Order.Inpatient.Order);
            }

            if (this.checkPrint == null)
            {
                this.checkPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPrint)) as FS.HISFC.BizProcess.Interface.Common.ICheckPrint;
                if (this.checkPrint == null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("获得接口IcheckPrint错误\n，可能没有维护相关的打印控件或打印控件没有实现接口检验接口IcheckPrint\n请与系统管理员联系。");
                    return;
                }
            }
            this.checkPrint.Reset();
            this.checkPrint.ControlValue(Patient, alInOrders);
            this.checkPrint.Show();
        }

        /// <summary>
        /// 添加会诊
        /// </summary>
        /// <param name="sender"></param>
        public void AddConsultation(object sender)
        {
            ////待修改
        }

        RowCompare rowCompare = new RowCompare();

        /// <summary>
        /// 刷新组合
        /// </summary>
        /// <param name="isReSort">是否重新排序</param>
        public void RefreshCombo()
        {
            FS.HISFC.Models.Order.OutPatient.Order ord = null;
            try
            {
                //用于选择同组项目
                string currentCombNo = "";

                Hashtable hsSubCombNo = new Hashtable();
                for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                {
                    ord = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                    if (ord != null)
                    {
                        if (!hsSubCombNo.Contains(ord.SubCombNO))
                        {
                            hsSubCombNo.Add(ord.SubCombNO, ord.SubCombNO + "01");
                        }
                        else
                        {
                            hsSubCombNo[ord.SubCombNO] = FS.FrameWork.Function.NConvert.ToInt32(hsSubCombNo[ord.SubCombNO]) + 1;
                        }
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Text = hsSubCombNo[ord.SubCombNO].ToString();
                    }

                    if (i == this.neuSpread1.ActiveSheet.ActiveRowIndex)
                    {
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag = "哈哈";
                        currentCombNo = ord.Combo.ID;
                    }
                    else
                    {
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag = null;
                    }
                }
                if (this.neuSpread1.Sheets[0].Rows.Count > 0)
                {
                    this.neuSpread1.Sheets[0].SortRows(GetColumnIndexFromName("顺序号"), true, false, rowCompare);
                    Order.Classes.Function.DrawCombo(this.neuSpread1.Sheets[0], GetColumnIndexFromName("组合号"), GetColumnIndexFromName("组合"));
                }

                int sortID = 1;

                //for (int i = this.neuSpread1.ActiveSheet.RowCount - 1; i >= 0; i--)
                for (int i = 0; i <= this.neuSpread1.ActiveSheet.RowCount - 1; i++)
                {
                    ord = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    ord.SortID = sortID;
                    this.AddObjectToFarpoint(ord, i, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                    this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Text = sortID.ToString();
                    sortID++;

                    if (this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag != null && this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag.ToString() == "哈哈")
                    {
                        this.neuSpread1.ActiveSheet.ActiveRowIndex = i;
                        this.ucOutPatientItemSelect1.CurrentRow = i;
                        this.ActiveRowIndex = i;
                        this.currentOrder = ord;
                        this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, this.neuSpread1.ActiveSheet.ColumnCount);
                        this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag = null;
                    }
                    else if (ord.Combo.ID == currentCombNo)
                    {
                        this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, this.neuSpread1.ActiveSheet.ColumnCount);
                    }
                }
                this.neuSpread1.ShowRow(this.neuSpread1.ActiveSheetIndex, this.neuSpread1.ActiveSheet.RowCount - 1, FarPoint.Win.Spread.VerticalPosition.Center);
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return;
            }
        }

        /// <summary>
        /// reset
        /// </summary>
        public void Reset()
        {
            this.ucOutPatientItemSelect1.Clear(false);

            this.ucOutPatientItemSelect1.ucInputItem1.Select();
            this.ucOutPatientItemSelect1.ucInputItem1.Focus();
        }

        public void RefreshOrderState()
        {
            this.RefreshOrderState(0);
        }

        /// <summary>
        /// 更新医嘱状态
        /// </summary>
        public void RefreshOrderState(int isRequeryFee)
        {
            try
            {
                if (!this.dirty)
                {
                    for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                    {
                        this.ChangeOrderState(i, 0, false);
                    }
                }
            }
            catch
            {
                ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("刷新医嘱状态时出现不可预知错误！请退出开立界面重试或与管理员联系"));
            }

            if (isRequeryFee == 1)
            {
                this.SetOrderFeeDisplay(false, true);
            }
            else
            {
                this.SetOrderFeeDisplay(false, false);
            }
        }

        /// <summary>
        /// 刷新医嘱状态
        /// </summary>
        /// <param name="reset"></param>
        public void RefreshOrderState(bool reset)
        {
            try
            {
                for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                {
                    this.ChangeOrderState(i, 0, reset);
                }

            }
            catch
            {
                ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("刷新医嘱状态时出现不可预知错误！请退出开立界面重试或与管理员联系"));
            }
        }

        /// <summary>
        /// 检查医嘱合法性
        /// </summary>
        /// <returns></returns>
        public int CheckOrder()
        {
            //处方内是否存在药品
            bool drugFlag = false;

            FS.HISFC.Models.Order.OutPatient.Order order = null;

            //是否有变动更改
            bool IsModify = false;

            ///是否包含附材
            bool isHaveSublOrders = false;

            FS.HISFC.Models.Pharmacy.Item drugItem = null;

            //超量开立提示
            string exceedWarning = "";

            //保存草药项目：限制草药项目不允许重复
            Hashtable hsPCCItem = new Hashtable();

            //临时医嘱
            for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            {
                order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    drugFlag = true;
                }
                if (order.Status == 0)
                {
                    //未审核的医嘱
                    IsModify = true;

                    if (order.Item.ID == "999")
                    {
                        continue;
                    }

                    //用主键（看诊序号+项目流水号）作为键值
                    if (!this.hsOrder.Contains(order.SeeNO + order.ID)
                        && !(string.IsNullOrEmpty(order.SeeNO)
                        || string.IsNullOrEmpty(order.ID)))
                    {
                        this.hsOrder.Add(order.SeeNO + order.ID, order);
                    }
                    //if (order.Item.IsPharmacy)
                    #region 药品
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {

                        #region 保存时判断是否停用、缺药

                        string errInfo = "";
                        if (Classes.Function.CheckDrugState(this.Patient, order.StockDept, order.ReciptDept, order.Item, true, ref drugItem, ref errInfo) <= 0)
                        {
                            ShowErr(errInfo, i, 0);
                            return -1;
                        }

                        #endregion

                        if (doceOnceLimit > 0)
                        {
                            decimal doceOnce = order.DoseOnce;

                            if (order.DoseUnit == drugItem.DoseUnit)
                            {
                                doceOnce = doceOnce / drugItem.BaseDose;
                            }
                            if (doceOnce > doceOnceLimit)
                            {
                                ShowErr("药品【" + order.Item.Name + "】每剂量超过最大限制值 " + doceOnceLimit.ToString() + "，请修改！\r\n如有疑问请联系系统管理员！\r\n", i, 0);
                                return -1;
                            }
                        }

                        #region 判断取整问题
                        //对于总量单位为包装单位的，判断单位不等于包装单位，报错
                        //0 包装单位；1 最小单位
                        if (order.MinunitFlag == "0" && order.Unit != drugItem.PackUnit)
                        {
                            ShowErr("药品【" + order.Item.Name + "】总量单位错误，门诊只能按照包装单位【drugItem.PackUnit】使用！\r\n\r\n", i, 0);
                            return -1;
                        }

                        if (Classes.Function.CheckLimitQty(order, order.Qty, order.Unit, ref errInfo) == -1)
                        {
                            ShowErr(errInfo + "\r\n\r\n", i, 0);
                            return -1;
                        }

                        #endregion

                        #region 重取药品基本信息

                        order.Item.MinFee = drugItem.MinFee;

                        //((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice = drugItem.Price;
                        //decimal orgPrice = 0;

                        //order.Item.Price = CacheManager.FeeIntegrate.GetPrice(order.Item.ID, currentPatientInfo, 0, order.Item.Price, order.Item.ChildPrice, order.Item.SpecialPrice, 0, ref orgPrice);

                        order.Item.Name = drugItem.Name;
                        order.Item.SysClass = drugItem.SysClass.Clone();//付给系统类别
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = drugItem.IsAllergy;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = drugItem.PackUnit;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = drugItem.MinUnit;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = drugItem.BaseDose;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = drugItem.DosageForm;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).SplitType = drugItem.SplitType;

                        #endregion

                        //药品
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            if (!hsPCCItem.Contains(order.Item.ID))
                            {
                                hsPCCItem.Add(order.Item.ID, null);
                            }
                            else
                            {
                                ShowErr("草药【" + order.Item.Name + "】不允许重复开立！\r\n存在多个相同项目！", i, 0);
                            }

                            //中草药
                            if (order.HerbalQty == 0)
                            {
                                ShowErr("药品【" + order.Item.Name + "】付数不能为零！", i, 0);
                                return -1;
                            }
                            //草药要保证，每次量*付数=总量
                            //草药都是按照最小单位或包装单位发药，而剂量单位不一定和最小单位一致，所以限制草药只能开立最小单位
                            //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                            //if (order.MinunitFlag == "0") //包装单位
                            //{
                            //    //包装单位 总量*包装单位*基本剂量=每次量*付数
                            //    if (order.Item.Qty != Math.Round((order.DoseOnce / drugItem.BaseDose * order.HerbalQty) / drugItem.PackQty, 2))
                            //    {
                            //        ShowErr("药品【" + order.Item.Name + "】总量不正确！", i, 0);
                            //        return -1;
                            //    }
                            //}
                            ////最小单位
                            //else
                            //{
                            //    //最小单位  总量*基本剂量=每次量*付数
                            //    if (order.Item.Qty != Math.Round(order.DoseOnce / drugItem.BaseDose * order.HerbalQty, 2))
                            //    {
                            //        ShowErr("药品【" + order.Item.Name + "】总量不正确！", i, 0);
                            //        return -1;
                            //    }
                            //}
                        }
                        else
                        {
                            //其他
                            if (order.DoseOnce == 0)
                            {
                                ShowErr("药品【" + order.Item.Name + "】每次剂量不能为零！", i, 0);
                                return -1;
                            }
                            if (order.DoseUnit == "")
                            {
                                ShowErr("药品【" + order.Item.Name + "】剂量单位不能为空！", i, 0);
                                return -1;
                            }

                            try
                            {
                                if (order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.').Contains("."))
                                {
                                    ShowErr("药品【" + order.Item.Name + "】总量不允许为小数！", i, 0);
                                    return -1;
                                }
                            }
                            catch
                            {
                                ShowErr("药品【" + order.Item.Name + "】总量不允许为小数！", i, 0);
                                return -1;
                            }
                        }
                        if (order.Unit == "")
                        {
                            ShowErr("药品【" + order.Item.Name + "】单位不能为空！", i, 0);
                            return -1;
                        }
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("药品【" + order.Item.Name + "】频次不能为空！", i, 0);
                            return -1;
                        }
                        if (order.Usage.ID == "")
                        {
                            ShowErr("药品【" + order.Item.Name + "】用法不能为空！", i, 0);
                            return -1;
                        }

                        decimal doseOnce = order.DoseOnce;
                        if (order.DoseUnit == (order.Item as HISFC.Models.Pharmacy.Item).MinUnit)
                        {
                            doseOnce = order.DoseOnce * (order.Item as HISFC.Models.Pharmacy.Item).BaseDose;
                        }
                        if ((doseOnce / ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose) > order.Qty
                            && order.Unit == order.DoseUnit)
                        {
                            ShowErr("药品【" + order.Item.Name + "】每次用量不可以大于总量！", i, 0);
                            return -1;
                        }

                        if (order.Item.SysClass.ID.ToString() != "PCC" && order.HerbalQty > 7)
                        {
                            //ShowErr("药品【" + order.Item.Name + "】 开立不允许超过7天！", i, 0);
                            //return -1;
                            exceedWarning += "\r\n" + order.Item.Name;
                        }

                        //检查库存
                        if (order.StockDept != null && order.StockDept.ID != "")
                        {
                            //都用最小单位来判断
                            decimal storeNum = 0;
                            decimal preNum = 0;
                            decimal orderQty = 0;
                            if (order.MinunitFlag != "1")//开立最小单位 !=((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit)
                            {
                                orderQty = order.Item.PackQty * order.Qty;
                            }
                            else
                            {
                                orderQty = order.Qty;
                            }
                            if (CacheManager.PhaIntegrate.GetStorageNum(order.StockDept.ID, order.Item.ID, order.ID, out storeNum, out preNum) == 1)
                            {
                                if (orderQty > storeNum - preNum)
                                {
                                    string stockinfo =
                                        ((storeNum / ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackQty) > 0 ? (Math.Floor(storeNum / ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackQty).ToString() + ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit) : "") + ((storeNum % ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackQty > 0) ? ((storeNum % ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackQty).ToString() + ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit) : "");

                                    string preStockinfo =
                                        ((preNum / ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackQty) > 0 ? (Math.Floor(preNum / ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackQty).ToString() + ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit) : "") + ((preNum % ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackQty > 0) ? ((preNum % ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackQty).ToString() + ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit) : "");

                                    if (isCheckDrugStock == 0)
                                    {
                                        ShowErr("药品【" + order.Item.Name + "】的当前库存量为" + stockinfo + ",已预扣" + preStockinfo + ",不足使用！", i, 0);
                                        {
                                            return -1;
                                        }
                                    }
                                    else if (isCheckDrugStock == 1)
                                    {
                                        if (ucOutPatientItemSelect1.MessageBoxShow("药品【" + order.Item.Name + "】的当前库存量为" + stockinfo + ",不足使用！\r\n是否继续保存！", "库存不足", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                        {
                                            return -1;
                                        }
                                    }
                                    else
                                    {
                                        Components.Order.Classes.Function.ShowBalloonTip(8, "警告", "药品【" + order.Item.Name + "】的当前库存量为" + stockinfo + ",不足使用！", ToolTipIcon.Info);
                                    }
                                }
                            }
                            else
                            {
                                ShowErr("药品【" + order.Item.Name + "】库存判断失败!" + CacheManager.PhaIntegrate.Err, i, 0);
                                return -1;
                            }
                        }
                    }
                    #endregion

                    #region 非药品
                    else
                    {
                        #region 判断停用状态

                        FS.HISFC.Models.Fee.Item.Undrug undrug = CacheManager.FeeIntegrate.GetUndrugByCode(order.Item.ID);
                        if (undrug == null)
                        {
                            ShowErr("查找非药品项目【" + order.Item.Name + "】失败：" + CacheManager.FeeIntegrate.Err, i, 0);
                            return -1;
                        }

                        if (undrug.IsValid == false)
                        {
                            ShowErr("【" + undrug.Name + "】已停用\n", i, 0);
                            return -1;
                        }
                        if (!this.hsOrderItem.Contains(undrug.ID))
                        {
                            this.hsOrderItem.Add(undrug.ID, undrug);
                        }

                        #endregion

                        if (order.ExeDept.ID == "")
                        {
                            ShowErr("项目【" + order.Item.Name + "】请选择执行科室！", i, 0); return -1;
                        }
                    }
                    #endregion

                    if (order.Qty == 0)
                    {
                        ShowErr("项目【" + order.Item.Name + "】数量不能为空！", i, 0);
                        return -1;
                    }
                    if (FS.FrameWork.Public.String.ValidMaxLengh(order.Memo, 80) == false)
                    {
                        ShowErr("项目【" + order.Item.Name + "】的备注超长!", i, 0);
                        return -1;
                    }
                    if (order.Qty > 5000)
                    {
                        ShowErr("数量太大！", i, 0); return -1;
                    }

                    if (this.IsFeeWhenPriceZero == "0")
                    {
                        if (order.Item.Price == 0)
                        {
                            ShowErr("项目【" + order.Item.Name + "】单价必须大于０！", i, 0);
                            return -1;
                        }
                    }

                    if (order.ID == "") IsModify = true;
                }
                //已保存的医嘱此处一起查询
                else
                {
                    ArrayList alOrder = CacheManager.OutOrderMgr.QueryOrder(currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                    if (alOrder == null)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("查询医嘱出错" + CacheManager.OutOrderMgr.Err);
                        return -1;
                    }
                    foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in alOrder)
                    {
                        if (orderTemp != null)
                        {
                            if (!this.hsOrder.Contains(orderTemp.SeeNO + orderTemp.ID) && !(string.IsNullOrEmpty(orderTemp.SeeNO) || string.IsNullOrEmpty(orderTemp.ID)))
                            {
                                this.hsOrder.Add(orderTemp.SeeNO + orderTemp.ID, orderTemp);
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(exceedWarning))
            {
                Components.Order.Classes.Function.ShowBalloonTip(8, "警告", "如下药品开立超过7日用量，请注意注明理由！\r\n" + exceedWarning, ToolTipIcon.Warning);
            }

            //现在删除也必须保存，如果医嘱全删了，则能够继续保存 houwb 2011-4-14
            if (hsDeleteOrder == null || hsDeleteOrder.Keys.Count == 0)
            {
                if (IsModify == false)
                {
                    Components.Order.Classes.Function.ShowBalloonTip(4, "提示", "\r\n没有开立任何项目，不需要保存！", ToolTipIcon.Info);
                    return -2;//未有新录入的医嘱
                }
            }

            //如果全部删除时候的保存，这里就不判断是否有有效诊断了
            if (IsModify)
            {
                if (drugFlag)
                {
                    if (CheckDiag(0) == -1)
                    {
                        return -1;
                    }
                }
                else
                {
                    if (CheckDiag(1) == -1)
                    {
                        return -1;
                    }
                }
            }

            if (isShowRepeatItemInScreen)
            {
                //提示重复药品
                string repeatItemName = "";
                Hashtable hsOrderItem = new Hashtable();

                for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                {
                    order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                    order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();

                    if (!hsOrderItem.Contains(order.Item.ID))
                    {
                        hsOrderItem.Add(order.Item.ID, null);
                    }
                    else
                    {
                        if (!repeatItemName.Contains(order.Item.Name))
                        {
                            repeatItemName = string.IsNullOrEmpty(repeatItemName) ? order.Item.Name : (repeatItemName + "\r\n" + order.Item.Name);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(repeatItemName))
                {
                    if (ucOutPatientItemSelect1.MessageBoxShow("有重复药品或项目,是否继续保存?\r\n" + repeatItemName, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }

            }

            if (!isHaveSublOrders && dealSublMode == 1 && this.neuSpread1.Sheets[0].RowCount > 0)
            {
                //{A1BAF267-6053-44e3-B96E-36E2C48DE4BD}
                //if (ucOutPatientItemSelect1.MessageBoxShow("是否重新计算附材？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //{
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                if (this.CalculatSubl(false) == -1)
                {
                    return -1;
                }
                //}
            }

            #region 合理用药自动审查

            if (this.IReasonableMedicine != null
                && this.IReasonableMedicine.PassEnabled)
            {
                this.IReasonableMedicine.PassShowFloatWindow(false);

                if (this.PassCheckOrder(true) == -1)
                {
                    return -1;
                }
            }

            #endregion

            return 0;
        }


        /// <summary>
        /// 是否已开立诊断
        /// </summary>
        /// <returns></returns>
        public bool isHaveDiag()
        {
            if (this.Patient != null && this.IsDesignMode)
            {
                ArrayList alDiagnose = CacheManager.DiagMgr.QueryCaseDiagnoseForClinic(this.Patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);

                if (alDiagnose == null || alDiagnose.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断诊断
        /// </summary>
        /// <param name="type">0 处方保存(含药品）；1 处方保存（不包含药品）；2 诊出</param>
        /// <returns></returns>
        private int CheckDiag(int type)
        {
            #region 判断诊断是否录入

            if (!this.isHaveDiag())
            {
                switch (isJudgeDiagnose)
                {
                    //0 不判断；
                    case "0":
                        return 1;
                        break;
                    //1 判断药品；
                    case "1":
                        if (type == 0)
                        {

                            ucOutPatientItemSelect1.MessageBoxShow("患者【" + Patient.Name + "】没有有效诊断，请先录入诊断！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return -1;
                        }
                        break;
                    //2 判断药品和非药品；
                    case "2":
                        if (type == 0 || type == 1)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("患者【" + Patient.Name + "】没有有效诊断，请先录入诊断！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return -1;
                        break;
                    //3 判断药品、非药品+诊出
                    case "3":
                        ucOutPatientItemSelect1.MessageBoxShow("患者【" + Patient.Name + "】没有有效诊断，请先录入诊断！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                        break;
                    //1 判断药品；
                    default:
                        if (type == 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("患者【" + Patient.Name + "】没有有效诊断，请先录入诊断！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return -1;
                        }
                        break;
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 组合医嘱
        /// </summary>
        public void ComboOrder()
        {
            ComboOrder(this.neuSpread1.ActiveSheetIndex);
            this.RefreshCombo();
            this.ucOutPatientItemSelect1.Clear(false);
        }

        /// <summary>
        /// 取消组合
        /// </summary>
        public void CancelCombo()
        {
            int iSelectionCount = 0;//{6532D2B8-A636-4a5a-8443-2FC0C6878ECC}

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                // edit by liuww
                //if (((FS.HISFC.Models.Order.OutPatient.Order)neuSpread1.ActiveSheet.Rows[i].Tag).IsSubtbl)
                //{
                //    continue;
                //}
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    iSelectionCount++;
                }
            }
            if (iSelectionCount <= 1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("不符合取消组合的条件！");
                return;
            }

            //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
            int firstRow = -1;
            int firstSubComb = 0;

            //存已经修改方号的组合ID
            string combID = "";

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            ////这个是按照倒序排列的...
            //for (int i = this.neuSpread1.ActiveSheet.Rows.Count - 1; i >= 0; i--)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    FS.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                    #region 判断如果是已经保存过的医嘱，需要删除原来的附材{F67E089F-1993-4652-8627-300295AAED8C}

                    if (o.ID != null && o.ID != "")
                    {
                        #region 医嘱带的附材的删除

                        if (!hsComboChange.ContainsKey(o.ID))
                        {
                            hsComboChange.Add(o.ID, o.Combo.ID);
                        }

                        o.NurseStation.User02 = "C";

                        #endregion
                    }

                    #endregion

                    o.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();

                    //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    if (firstRow == -1)
                    {
                        firstRow = i;
                        firstSubComb = o.SubCombNO;
                    }
                    //方号相同的方号加1
                    else
                    {
                        o.SubCombNO = firstSubComb + 1;
                        firstSubComb += 1;
                        if (firstRow > i)
                        {
                            firstRow = i;
                        }
                    }

                    this.AddObjectToFarpoint(o, i, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                }

                //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                //下面的项目方号修改
                if (firstRow > -1 && !this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderTemp = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    if (orderTemp != null)
                    {
                        if (!combID.Contains("|" + orderTemp.Combo.ID + "|"))
                        {
                            orderTemp.SubCombNO = firstSubComb + 1;
                            firstSubComb += 1;

                            this.AddObjectToFarpoint(orderTemp, i, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                            combID = combID + "|" + orderTemp.Combo.ID + "|";
                        }
                        else
                        {
                            orderTemp.SubCombNO = firstSubComb;
                        }
                    }
                }
            }
            this.neuSpread1.ActiveSheet.ClearSelection();
            this.RefreshCombo();
            //{D96CEC1D-77BF-434f-B440-D1988F73223C}  清空显示
            this.ucOutPatientItemSelect1.Clear(false);
        }

        /// <summary>
        /// 获得具有相同组合号的医嘱
        /// </summary>
        /// <returns></returns>
        public ArrayList GetOrderHaveSameCombID(string combID)
        {
            if (combID == "" || combID == null)
            {
                return null;
            }
            ArrayList alOrder = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, GetColumnIndexFromName("组合号")].Text == combID)
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, 0);
                    //为空 继续
                    if (temp == null)
                    {
                        continue;
                    }
                    //添加
                    alOrder.Add(temp);
                }
            }
            return alOrder;
        }

        public void SetEditGroup(bool isEdit)
        {
            this.EditGroup = isEdit;
            this.ucOutPatientItemSelect1.IsEditGroup = isEdit;

            this.isAddMode = false;
            this.ucOutPatientItemSelect1.Visible = isEdit;
            if (this.ucOutPatientItemSelect1 != null)
            {
                this.ucOutPatientItemSelect1.EditGroup = isEdit;
            }

            this.SetOrderFeeDisplay(false, true);

            this.neuSpread1.Sheets[0].DataSource = null;

            this.neuSpread1.Sheets[0].Rows.Remove(0, this.neuSpread1.Sheets[0].Rows.Count);

            this.neuSpread1.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;

        }

        public void PrintOrder()
        {
            this.neuSpread1.PrintSheet(this.neuSpread1_Sheet1);
        }

        /// <summary>
        /// 检查开立有效性
        /// </summary>
        /// <param name="alOrders"></param>
        /// <param name="order"></param>
        /// <param name="errinfo"></param>
        /// <returns></returns>
        private int CheckOrderBase(ArrayList alOrders, FS.HISFC.Models.Order.OutPatient.Order order, ref string errinfo)
        {
            /* 1、查询项目信息
                2、查询库存信息
                3、查询处方权限
                4、查询过敏史
                5、处理开立项目接口
                6、计算显示金额（包括根据card_no查询已收费信息） 
             * */

            if (EditGroup || Patient == null || string.IsNullOrEmpty(Patient.ID))
            {
                return 1;
            }

            int ret = 1;

            #region 检查缺药停用、库存信息

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                if (Classes.Function.FillDrugItem(null, currentPatientInfo, ref order, ref errInfo) <= 0)
                {
                    return -1;
                }
            }
            else
            {
                if (Classes.Function.FillUndrugItem(currentPatientInfo, ref order, ref errInfo) <= 0)
                {
                    return -1;
                }
            }

            #endregion

            //处方权
            ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, this.GetReciptDoct(),
                this.GetReciptDept(), FS.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref errinfo);
            //ret = Components.Order.Classes.Function.JudgeEmplPriv(order, this.GetReciptDoct(),
            //    this.GetReciptDept(), FS.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref errinfo);

            if (ret <= 0)
            {
                return -1;
            }

            //过敏史判断
            ret = Components.Order.Classes.Function.JudgePatientAllergy("1", this.Patient.PID, order, ref errinfo);

            if (ret <= 0)
            {
                return -1;
            }

            #region 接口判断

            if (this.IBeforeAddItem != null)
            {
                ArrayList alOrderTemp = new ArrayList();

                alOrderTemp.Add(order);
                if (this.IBeforeAddItem.OnBeforeAddItemForOutPatient(this.Patient, this.GetReciptDoct(), this.GetReciptDept(), alOrderTemp) == -1)
                {
                    errinfo = IBeforeAddItem.ErrInfo;
                    return -1;
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 粘贴医嘱
        /// </summary>
        public void PasteOrder()
        {
            try
            {
                if (Classes.Function.AlCopyOrders == null || Classes.Function.AlCopyOrders.Count <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("剪贴板中没有可以粘贴的医嘱！"));
                    return;
                }

                if (FS.HISFC.Components.Order.Classes.HistoryOrderClipboard.Type == ServiceTypes.C)
                {
                    string oldComb = "";
                    string newComb = "";

                    ArrayList alOrder = new ArrayList();
                    ArrayList alAddOrder = new ArrayList();//用于增加接口

                    FS.HISFC.Models.Order.OutPatient.Order order = null;
                    for (int i = 0; i < Classes.Function.AlCopyOrders.Count; i++)
                    {
                        order = Classes.Function.AlCopyOrders[i] as FS.HISFC.Models.Order.OutPatient.Order;
                        if (order == null)
                        {
                            continue;
                        }

                        if (order.Item.ID == "999" || order.ReciptDept.ID == order.ExeDept.ID)
                        {
                            order.ExeDept.ID = "";
                        }

                        if (this.FillNewOrder(ref order) == -1)
                        {
                            continue;
                        }

                        if (order.Combo.ID != oldComb)
                        {
                            newComb = CacheManager.OutOrderMgr.GetNewOrderComboID();
                            oldComb = order.Combo.ID;
                            order.Combo.ID = newComb;
                        }
                        else
                        {
                            order.Combo.ID = newComb;
                        }

                        //{37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
                        if (Components.Order.Classes.Function.ReComputeQty(order) == -1)
                        {
                        }

                        alOrder.Add(order);
                    }

                    foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
                    {
                        //添加到当前类表中 按照医嘱类型进行分类
                        this.AddNewOrder(outOrder, 0);
                    }
                }
                else
                {
                    ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("不可以把住院的医嘱复制为门诊医嘱！"));
                    return;
                }

            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }

            RefreshOrderState();
            this.RefreshCombo();
        }

        /// <summary>
        /// 复制医嘱
        /// 被复制的医嘱必须是保存过的（有医嘱流水号的）
        /// 否则粘贴时有问题
        /// </summary>
        public void CopyOrder()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            ArrayList list = new ArrayList();

            //获取选中行的医嘱ID
            for (int row = 0; row < this.neuSpread1_Sheet1.Rows.Count; row++)
            {
                if (this.neuSpread1_Sheet1.IsSelected(row, 0))
                {
                    FS.HISFC.Models.Order.OutPatient.Order ord = this.GetObjectFromFarPoint(row, 0);

                    if (ord == null || string.IsNullOrEmpty(ord.ID))
                    {
                        continue;
                    }
                    else
                    {
                        list.Add(ord.ID);
                    }

                }
            }

            if (list.Count <= 0)
            {
                return;
            }
            ////先添加到COPY列表
            //for (int count = 0; count < list.Count; count++)
            //{
            //    HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(list[count]);
            //}

            Classes.Function.AlCopyOrders = list;

            string type = "1";

            HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(type);
            //然后将copy列表放到剪贴板上
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Copy();
        }
        #region LIS、Pacs接口

        FS.SOC.HISFC.BizProcess.OrderInterface.Common.IMedicalResult IMedicalResult = null;

        public int QueryMedicalResult(FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType resultType)
        {
            if (Patient == null
                || string.IsNullOrEmpty(Patient.ID))
            {
                MessageBox.Show("请选择患者后再点击查询！\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 1;
            }

            if (IMedicalResult == null)
            {
                IMedicalResult = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IMedicalResult)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IMedicalResult;
            }

            if (IMedicalResult != null)
            {
                ArrayList alSelectOrder = new ArrayList(this.GetSelectOrders());

                IMedicalResult.ResultType = resultType;
                int rev = IMedicalResult.ShowResult(Patient, alSelectOrder);
                if (rev < 0)
                {
                    MessageBox.Show("查询医疗结果出错！\r\n" + IMedicalResult.ErrInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return rev;
            }
            return 1;
        }

        #region LIS、PACS申请单

        /// <summary>
        /// LIS申请单打印接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.ILisReportPrint IlisReportPrint = null;

        /// <summary>
        /// LIS申请单打印
        /// </summary>
        public void LisReportPrint()
        {
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                ucOutPatientItemSelect1.MessageBoxShow("请选择患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IlisReportPrint == null)
            {
                this.IlisReportPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.ILisReportPrint)) as FS.HISFC.BizProcess.Interface.Order.ILisReportPrint;
            }

            if (IlisReportPrint == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("LIS打印接口未实现！请联系信息科！\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArrayList alOrders = new ArrayList();
            FS.HISFC.Models.Order.OutPatient.Order order = null;
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;
                alOrders.Add(order);
            }

            if (alOrders.Count <= 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("没有处方信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IlisReportPrint.LisReportPrintForOutPatient(this.Patient, this.GetReciptDept(), this.GetReciptDoct(), alOrders) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow(IlisReportPrint.ErrInfo);
                return;
            }
        }

        /// <summary>
        /// PACS申请单打印接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint IPacsReportPrint = null;

        /// <summary>
        /// PACS申请单打印
        /// </summary>
        public void PacsReportPrint()
        {
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                ucOutPatientItemSelect1.MessageBoxShow("请选择患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IPacsReportPrint == null)
            {
                this.IPacsReportPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint)) as FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint;
            }

            if (IPacsReportPrint == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("PACS打印接口未实现！请联系信息科！\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArrayList alOrders = new ArrayList();
            FS.HISFC.Models.Order.OutPatient.Order order = null;
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;
                if (this.neuSpread1.Sheets[0].IsSelected(i, 0))
                {
                    order.User03 = "1";
                }

                alOrders.Add(order);
            }

            if (alOrders.Count <= 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("没有处方信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IPacsReportPrint.PacsReportPrintForOutPatient(this.Patient, this.GetReciptDept(), this.GetReciptDoct(), alOrders) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow(IPacsReportPrint.ErrInfo);
                return;
            }
        }
        #endregion

        /// <summary>
        /// LIS结果查询接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.ILis lisInterface = null;

        /// <summary>
        /// 查询LIS结果
        /// </summary>
        public void QueryLisResult()
        {
            if (this.Patient == null || Patient.PID.CardNO == "" || Patient.PID.CardNO == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("请选择一个患者！");
                return;
            }

            try
            {
                if (lisInterface == null)
                {
                    lisInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.Common.ILis)) as FS.HISFC.BizProcess.Interface.Common.ILis;
                }

                if (lisInterface == null)
                {
                    if (string.IsNullOrEmpty(FS.FrameWork.WinForms.Classes.UtilInterface.Err))
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("查询LIS接口出现错误：\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("没有维护LIS接口！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //lisInterface.ShowResultByPatient(patient.ID);
                    lisInterface.PatientType = FS.HISFC.Models.RADT.EnumPatientType.C;
                    lisInterface.SetPatient(Patient);
                    lisInterface.PlaceOrder(this.GetSelectOrders());

                    if (lisInterface.ShowResultByPatient() == -1)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(lisInterface.ErrMsg);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 断开LIS查询连接
        /// </summary>
        /// <returns></returns>
        public int ReleaseLisInterface()
        {
            if (this.lisInterface != null)
            {
                return this.lisInterface.Disconnect();
            }
            return 1;
        }

        /// <summary>
        /// PACS结果查询接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.Common.IPacs pacsInterface = null;

        /// <summary>
        /// 申请单接口
        /// </summary>
        private static FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientPacsApply IOutPatientPacsApply = null;

        /// <summary>
        /// 申请单接口
        /// </summary>
        private void InitPacsApply()
        {
            if (IOutPatientPacsApply == null)
            {
                IOutPatientPacsApply = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientPacsApply)) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientPacsApply;
            }
        }

        /// <summary>
        /// 查看PACS检查报告单
        /// </summary>
        public void QueryPacsReport()
        {
            if (this.Patient == null || Patient.PID.CardNO == "" || Patient.PID.CardNO == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("请选择一个患者！");
                return;
            }

            try
            {
                //医护站已经够乱了，以后别的系统或模块的缺陷，强势的要求他们自己解决！！
                //#region Kill掉Pacs 进程 Pacs 自己不关进程

                //string s = "Display";
                //System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcessesByName(s);
                //if (proc.Length > 0)
                //{
                //    for (int i = 0; i < proc.Length; i++)
                //    {
                //        proc[i].Kill();
                //    }
                //}
                //#endregion

                if (pacsInterface == null)
                {
                    this.pacsInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IPacs)) as FS.HISFC.BizProcess.Interface.Common.IPacs;
                    if (this.pacsInterface == null)
                    {
                        if (string.IsNullOrEmpty(FS.FrameWork.WinForms.Classes.UtilInterface.Err))
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("查询PACS接口出现错误：\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("没有维护PACS结果查询接口！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }


                    // this.pacsInterface.Connect();

                    if (this.pacsInterface.Connect() == 0)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("初始化PACS失败！请联系信息科！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                this.pacsInterface.OprationMode = "1";
                this.pacsInterface.SetPatient(Patient);
                pacsInterface.PlaceOrder(this.GetSelectOrders());

                if (this.pacsInterface.ShowResultByPatient() == 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("查看PACS结果失败！请在按一次按钮！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("查看PACS结果出现错误！请在按一次按钮！\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// 断开PACS查询连接
        /// </summary>
        /// <returns></returns>
        public int ReleasePacsInterface()
        {
            if (this.pacsInterface != null)
            {
                return this.pacsInterface.Disconnect();
            }
            return 1;
        }

        #endregion

        #endregion

        #region 事件

        /// <summary>
        /// 修改单个项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        private void GetOrderChanged(int rowIndex, FS.HISFC.Models.Order.OutPatient.Order outOrder, EnumOrderFieldList changedField)
        {
            if (changedField == EnumOrderFieldList.WarningFlag)
            {
                //this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("!")].Text = inOrder.Note;
            }
            if (changedField == EnumOrderFieldList.Warning)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("警")].Text = outOrder.Note;
            }
            else if (changedField == EnumOrderFieldList.OrderType)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("医嘱类型")].Text = outOrder.OrderType;
            }
            else if (changedField == EnumOrderFieldList.OrderID)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("医嘱流水号")].Text = outOrder.ID;
            }
            else if (changedField == EnumOrderFieldList.Status)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("医嘱状态")].Text = outOrder.Status.ToString();
            }
            else if (changedField == EnumOrderFieldList.CombNo)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("组合号")].Text = outOrder.Combo.ID;
            }
            else if (changedField == EnumOrderFieldList.MainDrug)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("主药")].Text = FS.FrameWork.Function.NConvert.ToInt32(outOrder.Combo.IsMainDrug).ToString();
            }
            else if (changedField == EnumOrderFieldList.ItemCode)
            {
                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("项目编码")].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).UserCode;
                    }
                }
                else
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("项目编码")].Text = SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrder.Item.ID).UserCode;
                    }
                }
            }
            else if (changedField == EnumOrderFieldList.ItemName
                || changedField == EnumOrderFieldList.Item)
            {
                this.AddObjectToFarpoint(outOrder, rowIndex, neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
            }
            else if (changedField == EnumOrderFieldList.CombFlag)
            {

            }
            else if (changedField == EnumOrderFieldList.Qty || changedField == EnumOrderFieldList.Unit)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("总量")].Text = outOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = outOrder.Unit;

                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//开立最小单位 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;


                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;


                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + outOrder.Item.PriceUnit;

                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.DoseOnce
                || changedField == EnumOrderFieldList.DoseUnit)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("每次用量")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.DoseOnce);
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单位")].Text = outOrder.DoseUnit;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("总量")].Text = outOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = outOrder.Unit;

                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//开立最小单位 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;
                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + outOrder.Item.PriceUnit;
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.HerbalQty)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("付数/天数")].Text = outOrder.HerbalQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("总量")].Text = outOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = outOrder.Unit;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("院注次数")].Text = outOrder.InjectCount.ToString();

                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//开立最小单位 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;
                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + outOrder.Item.PriceUnit;

                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.FrequencyCode
                || changedField == EnumOrderFieldList.Frequency)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("频次编码")].Text = outOrder.Frequency.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("频次名称")].Text = outOrder.Frequency.Name;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("总量")].Text = outOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = outOrder.Unit;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("院注次数")].Text = outOrder.InjectCount.ToString();

                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//开立最小单位 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + outOrder.Item.PriceUnit;

                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.UsageCode
                || changedField == EnumOrderFieldList.Usage)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("用法名称")].Text = outOrder.Usage.Name;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("用法编码")].Text = outOrder.Usage.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("院注次数")].Text = outOrder.InjectCount.ToString();
            }
            else if (changedField == EnumOrderFieldList.InjNum)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("院注次数")].Text = outOrder.InjectCount.ToString();
            }
            else if (changedField == EnumOrderFieldList.Specs)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("规格")].Text = outOrder.Item.Specs;
            }
            else if (changedField == EnumOrderFieldList.Price)
            {
                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//开立最小单位 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + outOrder.Item.PriceUnit;

                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.TotalCost)
            {
                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//包装单位 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;
                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Qty * outOrder.Item.Price, 2);
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / outOrder.Item.PackQty, 2) + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("单价")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "元/" + outOrder.Item.PriceUnit;


                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("金额")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.BeginDate)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("开始时间")].Text = outOrder.BeginTime.ToString();
            }
            else if (changedField == EnumOrderFieldList.ReciptDoct)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("开立医生")].Text = outOrder.ReciptDoctor.Name;
            }
            else if (changedField == EnumOrderFieldList.ExecDeptCode
                || changedField == EnumOrderFieldList.ExeDept)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("执行科室编码")].Text = outOrder.ExeDept.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("执行科室")].Text = outOrder.ExeDept.Name;
            }
            else if (changedField == EnumOrderFieldList.Emc)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("加急")].Value = outOrder.IsEmergency;
            }
            else if (changedField == EnumOrderFieldList.CheckBody)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("检查部位")].Text = outOrder.CheckPartRecord;
            }
            else if (changedField == EnumOrderFieldList.Sample)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("样本类型")].Text = outOrder.Sample.Name;
            }
            else if (changedField == EnumOrderFieldList.DrugDeptCode
                || changedField == EnumOrderFieldList.DrugDept)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("取药药房编码")].Text = outOrder.StockDept.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("取药药房")].Text = outOrder.StockDept.Name;
            }
            else if (changedField == EnumOrderFieldList.Memo)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("备注")].Text = outOrder.Memo;
            }
            else if (changedField == EnumOrderFieldList.InputOperCode
                || changedField == EnumOrderFieldList.InputOper)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("录入人编码")].Text = outOrder.Oper.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("录入人")].Text = outOrder.Oper.Name;
            }
            else if (changedField == EnumOrderFieldList.ReciptDept)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("开立科室")].Text = outOrder.ReciptDept.Name;
            }
            else if (changedField == EnumOrderFieldList.MoDate)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("开立时间")].Text = outOrder.MOTime.ToString();
            }
            else if (changedField == EnumOrderFieldList.EndDate)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("停止时间")].Text = outOrder.EndTime.ToString();
            }
            else if (changedField == EnumOrderFieldList.DCOperCode
                || changedField == EnumOrderFieldList.DCOper)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("停止人编码")].Text = outOrder.DCOper.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("停止人")].Text = outOrder.DCOper.Name;
            }
            else if (changedField == EnumOrderFieldList.SubComb)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("医嘱名称")].Text = ShowOrderName(outOrder);

                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("顺序号")].Text = outOrder.SortID.ToString();
                neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("组合号")].Text = outOrder.Combo.ID;
            }
            else if (changedField == EnumOrderFieldList.OrderNo)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("顺序号")].Text = outOrder.SortID.ToString();
                neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("组合号")].Text = outOrder.Combo.ID;
            }
            else if (changedField == EnumOrderFieldList.HypoTestCode)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("皮试代码")].Text = outOrder.Item.SysClass.Name;
            }
            else if (changedField == EnumOrderFieldList.HypoTest)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("皮试")].Text = outOrder.Item.SysClass.Name;
            }

            neuSpread1.ActiveSheet.Rows[rowIndex].Tag = outOrder;
        }

        /// <summary>
        /// 医嘱变化函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        protected virtual void ucItemSelect1_OrderChanged(FS.HISFC.Models.Order.OutPatient.Order sender, EnumOrderFieldList changedField)
        {
            dirty = true;
            if (!this.EditGroup && !this.bIsDesignMode)
                return;

            if (!this.EditGroup)//{E679E3A6-9948-41a8-B390-DD9A57347681}判断不是开立医嘱模式就不走下面接口
            {
                #region 根据接口实现对医嘱信息进行补充判断
                //{48E6BB8C-9EF0-48a4-9586-05279B12624D}
                if (this.IAlterOrderInstance == null)
                {
                    this.InitAlterOrderInstance();
                }

                if (this.IAlterOrderInstance != null)
                {
                    if (this.IAlterOrderInstance.AlterOrder(this.currentPatientInfo, this.reciptDoct, this.reciptDept, ref sender) == -1)
                    {
                        return;
                    }
                }

                #endregion
            }

            #region 重复医嘱提示
            if (isShowSameOrder)
            {
                #region 提示当次就诊已开立过相同项目的医嘱
                if (this.SameOrderList != null && this.SameOrderList.Count > 0)
                {
                    foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in this.SameOrderList)
                    {
                        if (orderTemp.Item.ID == ((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.ID)
                        {
                            if (MessageBox.Show("项目【" + orderTemp.Item.Name + "】在【" + orderTemp.MOTime.ToString() + "】已开立,是否继续开立？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return;
                            }

                            break;
                        }
                    }
                }

                #endregion

                #region 提示检查或检验项目是否存在已收费但未出报告的记录（历往就诊）
                if (this.LastOrderList != null && this.LastOrderList.Count > 0)
                {
                    foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in this.LastOrderList)
                    {
                        //仅针对检验项目以及超声项目
                        if (orderTemp.Item.SysClass.ID.ToString() == "UL")
                        {

                            if (orderTemp.Item.ID == ((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.ID && orderTemp.User01 != "已出报告")
                            {
                                if (MessageBox.Show("项目【" + orderTemp.Item.Name + "】在【" + orderTemp.MOTime.ToString() + "】已开立且为" + orderTemp.User01 + "状态 ,是否继续开立？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                {
                                    return;
                                }

                                break;
                            }

                        }
                        //超声检查不存在“已报告”状态  
                        else if ((orderTemp.Item.SysClass.ID.ToString() == "UC" && orderTemp.ExeDept.ID == "6003"))
                        {
                            if (orderTemp.Item.ID == ((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.ID && orderTemp.User01 == "未执行")
                            {
                                if (MessageBox.Show("项目【" + orderTemp.Item.Name + "】在【" + orderTemp.MOTime.ToString() + "】已开立，且为已过费未执行状态 ,是否继续开立？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                {
                                    return;
                                }

                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region 新增
            if (this.ucOutPatientItemSelect1.OperatorType == Operator.Add)
            {
                this.AddNewOrder(sender, this.neuSpread1.ActiveSheetIndex);


                //this.



                this.neuSpread1.ActiveSheet.ClearSelection();
                //this.neuSpread1.ActiveSheet.AddSelection(0, 0, 1, 1);
                //this.neuSpread1.ActiveSheet.ActiveRowIndex = 0;
                this.neuSpread1.ActiveSheet.AddSelection(this.ActiveRowIndex, 0, 1, 1);

                //this.SelectionChanged();

                ShowPactItem();
            }
            #endregion

            #region 删除
            else if (this.ucOutPatientItemSelect1.OperatorType == Operator.Delete)
            {

            }
            #endregion

            #region 修改
            else if (this.ucOutPatientItemSelect1.OperatorType == Operator.Modify)
            {
                ArrayList alRows = GetSelectedRows();

                //修改
                if (alRows.Count > 1)
                {
                    for (int i = 0; i < alRows.Count; i++)
                    {
                        if (this.ucOutPatientItemSelect1.CurrentRow == System.Convert.ToInt32(alRows[i]))
                        {
                            GetOrderChanged(int.Parse(alRows[i].ToString()), sender, changedField);
                            //this.AddObjectToFarpoint(sender, this.ucOutPatientItemSelect1.CurrentRow, this.neuSpread1.ActiveSheetIndex, changedField);
                        }
                        else
                        {
                            FS.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex);

                            if (order.Combo.ID == sender.Combo.ID)
                            {
                                if (changedField == EnumOrderFieldList.Item
                                    //|| changedField == EnumOrderFieldList.Frequency
                                    || changedField == EnumOrderFieldList.BeginDate
                                    || changedField == EnumOrderFieldList.EndDate
                                    || changedField == EnumOrderFieldList.Emc
                                    )
                                {
                                    order.BeginTime = sender.BeginTime;
                                    order.EndTime = sender.EndTime;
                                    //{AA8348EF-8669-4ebf-B863-95469A7A04E2}屏蔽修改单位，组合内所有单位都跟着变化
                                    //order.Unit = sender.Unit;
                                    order.IsEmergency = sender.IsEmergency;

                                    GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                    //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                else if (changedField == EnumOrderFieldList.Sample)
                                {
                                    order.Sample.ID = sender.Sample.ID;
                                    order.Sample.Name = sender.Sample.Name;
                                    GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                }
                                else if (changedField == EnumOrderFieldList.CheckBody)
                                {
                                    order.CheckPartRecord = sender.CheckPartRecord;
                                    GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                }
                                else if (changedField == EnumOrderFieldList.Usage)
                                {
                                    if (!Classes.Function.IsSameUsage(order.Usage.ID, sender.Usage.ID))
                                    {
                                        order.Usage = sender.Usage.Clone();
                                        order.Frequency.Usage = sender.Frequency.Usage.Clone();
                                    }

                                    order.InjectCount = sender.InjectCount;

                                    if (!Classes.Function.CheckIsInjectUsage(order.Usage.ID))
                                    {
                                        order.InjectCount = 0;
                                    }

                                    GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                    //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                //修改院注
                                else if (changedField == EnumOrderFieldList.InjNum
                                    || changedField == EnumOrderFieldList.ExeDept
                                    )
                                {
                                    order.InjectCount = sender.InjectCount;
                                    order.ExeDept = sender.ExeDept;

                                    GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                    //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                //如果是天数/付数、频次改变, 则整个组合一起改变, 并且重新计算数量
                                else if (changedField == EnumOrderFieldList.HerbalQty
                                    || changedField == EnumOrderFieldList.Frequency)
                                {
                                    order.Frequency.ID = sender.Frequency.ID;
                                    order.Frequency.Name = sender.Frequency.Name;
                                    order.Frequency.Time = sender.Frequency.Time;
                                    order.HerbalQty = sender.HerbalQty;
                                    if (order.Item.ID != "999")
                                    {
                                        try
                                        {
                                            //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                                            if (Components.Order.Classes.Function.ReComputeQty(order) == -1)
                                            {
                                                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.OutOrderMgr.Err);
                                                return;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                                            return;
                                        }
                                    }
                                    order.InjectCount = sender.InjectCount;

                                    GetOrderChanged(int.Parse(alRows[i].ToString()), order, EnumOrderFieldList.Item);
                                    //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                                else if (changedField == EnumOrderFieldList.SubComb)
                                {
                                    #region 组合相同的一起选择
                                    //设置组合行选择
                                    //if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != "" && this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != null)
                                    //{
                                    //    this.neuSpread1.ActiveSheet.ClearSelection();

                                    //    for (int k = 0; k < this.neuSpread1.ActiveSheet.Rows.Count; k++)
                                    //    {
                                    //        string strComboNo = this.GetObjectFromFarPoint(k, this.neuSpread1.ActiveSheetIndex).Combo.ID;
                                    //        if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo && k != this.neuSpread1.ActiveSheet.ActiveRowIndex)
                                    //        {
                                    //            this.neuSpread1.ActiveSheet.AddSelection(k, 0, 1, 1);
                                    //        }
                                    //    }
                                    //}
                                    #endregion
                                }

                                if (changedField == EnumOrderFieldList.Usage)
                                {
                                    ShowPactItem();
                                }
                            }
                            else
                            {
                                #region 全选修改

                                //天数全选修改
                                if (changedField == EnumOrderFieldList.HerbalQty)
                                {
                                    if (isChangeAllSelect == "100" || isChangeAllSelect == "110"
                                        || isChangeAllSelect == "111" || isChangeAllSelect == "101")
                                    {
                                        order.HerbalQty = sender.HerbalQty;

                                        if (Components.Order.Classes.Function.ReComputeQty(order) == -1)
                                        {
                                            return;
                                        }

                                        GetOrderChanged(int.Parse(alRows[i].ToString()), order, EnumOrderFieldList.Item);
                                        //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                    }
                                }
                                //频次全选修改
                                else if (changedField == EnumOrderFieldList.Frequency)
                                {
                                    if (isChangeAllSelect == "010" || isChangeAllSelect == "110"
                                        || isChangeAllSelect == "111" || isChangeAllSelect == "011")
                                    {
                                        order.Frequency = sender.Frequency.Clone();

                                        if (Components.Order.Classes.Function.ReComputeQty(order) == -1)
                                        {
                                            return;
                                        }

                                        //对于计算院注次数，报错只提示
                                        string errInfo = "";
                                        if (Classes.Function.CalculateInjNum(order, ref errInfo) == -1)
                                        {
                                            ucOutPatientItemSelect1.MessageBoxShow("计算院注次数错误：\r\n" + errInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }

                                        GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                        //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                    }
                                }
                                //用法全选修改
                                else if (changedField == EnumOrderFieldList.Usage)
                                {
                                    if (isChangeAllSelect == "001" || isChangeAllSelect == "101"
                                        || isChangeAllSelect == "111" || isChangeAllSelect == "011")
                                    {
                                        order.Usage = sender.Usage;

                                        GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                        //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                    }
                                }

                                #endregion

                                else
                                {
                                    this.neuSpread1.ActiveSheet.RemoveSelection(System.Convert.ToInt32(alRows[i]), 0, 1, 1);
                                }
                            }
                        }
                    }
                }
                else
                {
                    GetOrderChanged(this.ucOutPatientItemSelect1.CurrentRow, sender, changedField);
                    //this.AddObjectToFarpoint(sender, this.ucOutPatientItemSelect1.CurrentRow, this.neuSpread1.ActiveSheetIndex, changedField);

                    //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    if (changedField == EnumOrderFieldList.SubComb)
                    {
                        #region 组合相同的一起选择
                        //设置组合行选择
                        //if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != "" && this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != null)
                        //{
                        //    this.neuSpread1.ActiveSheet.ClearSelection();
                        //    for (int k = 0; k < this.neuSpread1.ActiveSheet.Rows.Count; k++)
                        //    {
                        //        string strComboNo = this.GetObjectFromFarPoint(k, this.neuSpread1.ActiveSheetIndex).Combo.ID;
                        //        if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo)
                        //        {
                        //            this.neuSpread1.ActiveSheet.AddSelection(k, 0, 1, 1);
                        //        }
                        //    }
                        //}
                        #endregion
                    }

                    if (changedField == EnumOrderFieldList.Usage)
                    {
                        ShowPactItem();
                    }
                }

                #region 处理附材

                //if (dealSublMode == 1)
                //{
                if (this.currentPatientInfo != null && !string.IsNullOrEmpty(this.currentPatientInfo.ID))
                {
                    dirty = true;
                    if (this.IDealSubjob != null
                        && IDealSubjob.IsAllowUsageSubPopChoose
                        )
                    {
                        ////因为只要更改每次量就计算附材，会导致程序不停查询数据库，很慢，
                        ////所以取消每次量更改时，计算附材的功能
                        //if (changedField == EnumOrderFieldList.DoseOnce)
                        //{

                        //}
                        if (changedField == EnumOrderFieldList.Qty
                            || changedField == EnumOrderFieldList.Frequency
                            || changedField == EnumOrderFieldList.Usage
                            || changedField == EnumOrderFieldList.HerbalQty
                            || changedField == EnumOrderFieldList.InjNum
                            || changedField == EnumOrderFieldList.SubComb
                            || changedField == EnumOrderFieldList.DoseOnce)
                        {
                            #region 弹出附材

                            IDealSubjob.IsPopForChose = true;
                            ArrayList alOrder = new ArrayList();
                            ArrayList alSubOrder = new ArrayList();
                            string errText = "";

                            ArrayList alOrderForSub = new ArrayList();

                            FS.HISFC.Models.Order.OutPatient.Order order = null;
                            for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                            {
                                order = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                                alOrderForSub.Add(order.Clone());
                            }

                            for (int i = alOrderForSub.Count - 1; i >= 0; i--)
                            {
                                order = new FS.HISFC.Models.Order.OutPatient.Order();
                                order = alOrderForSub[i] as FS.HISFC.Models.Order.OutPatient.Order;
                                if (order.Combo.ID == sender.Combo.ID)
                                {
                                    if (order.IsSubtbl)
                                    {
                                        this.DeleteSingleOrder(i);
                                    }
                                    else
                                    {
                                        alOrder.Add(order.Clone());
                                    }
                                }
                                else
                                {
                                    alOrder.Add(order.Clone());
                                }
                            }

                            if (alOrder.Count > 0)
                            {
                                if (IDealSubjob.DealSubjob(this.Patient, alOrder, sender, ref alSubOrder, ref errText) <= 0)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败！" + errText);
                                    return;
                                }

                                if (alSubOrder != null && alSubOrder.Count > 0)
                                {
                                    foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alSubOrder)
                                    {
                                        //orderObj.Combo.ID = CacheManager.OrderMgr.GetNewOrderComboID();
                                        //orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                        orderObj.SortID = 0;
                                        orderObj.ID = "";
                                        if (orderObj.SubCombNO == 0)
                                        {
                                            orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                        }
                                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                                        this.AddObjectToFarpoint(orderObj, this.neuSpread1_Sheet1.RowCount - 1, 0, EnumOrderFieldList.Item);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    dirty = false;
                }
                //}
                #endregion
            }
            #endregion

            //有些时候不需要刷新状态和组合号的
            if (changedField == EnumOrderFieldList.Item
                || changedField == EnumOrderFieldList.CombNo
                || changedField == EnumOrderFieldList.DoseOnce
                || changedField == EnumOrderFieldList.DoseUnit
                || changedField == EnumOrderFieldList.Frequency
                || changedField == EnumOrderFieldList.FrequencyCode
                || changedField == EnumOrderFieldList.HerbalQty
                || changedField == EnumOrderFieldList.ItemCode
                || changedField == EnumOrderFieldList.ItemName
                || changedField == EnumOrderFieldList.Price
                || changedField == EnumOrderFieldList.Qty
                || changedField == EnumOrderFieldList.TotalCost
                || changedField == EnumOrderFieldList.Unit
                || changedField == EnumOrderFieldList.Usage
                || changedField == EnumOrderFieldList.UsageCode
                || changedField == EnumOrderFieldList.SubComb
                )
            {
                RefreshOrderState();
            }
            if (changedField == EnumOrderFieldList.Item
                || changedField == EnumOrderFieldList.CombNo
                || changedField == EnumOrderFieldList.ItemCode
                || changedField == EnumOrderFieldList.ItemName
                || changedField == EnumOrderFieldList.SubComb
                || ucOutPatientItemSelect1.OperatorType == Operator.Add
                )
            {

                this.RefreshCombo();
            }

            dirty = false;
        }

        /// <summary>
        /// cellchange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            try
            {
                if (this.bIsDesignMode && dirty == false)
                {
                    int i = 0;
                    switch (GetColumnNameFromIndex(e.Column))
                    {
                        case "用法名称":
                            i = this.GetColumnIndexFromName("用法编码");
                            this.neuSpread1.ActiveSheet.Cells[e.Row, i].Text =
                                Order.Classes.Function.HelperUsage.GetName(this.neuSpread1.ActiveSheet.Cells[e.Row, e.Column].Text);
                            break;
                        case "医嘱状态":
                            RefreshOrderState();

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }
        }

        /// <summary>
        /// 选择医嘱修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            SelectionChanged();

            ShowPactItem();
        }

        /// <summary>
        /// 自动保存列宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[0], SetingFileName);
        }

        /// <summary>
        /// 显示项目信息
        /// </summary>
        /// <returns></returns>
        private int ShowPactItem()
        {
            try
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex < 0)
                {
                    return 1;
                }

                #region 显示项目信息

                FS.HISFC.Models.Order.OutPatient.Order outOrder = GetObjectFromFarPoint(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
                if (outOrder == null)
                {
                    return -1;
                }
                this.pnItemInfo.Visible = true;
                txtItemInfo.ReadOnly = true;

                string showInfo = "";

                //项目信息
                if (outOrder.Item.ID == "999")
                {
                    showInfo += outOrder.Item.Name + " 【规格】" + outOrder.Item.Specs + " 【单价】" + outOrder.Item.Price.ToString() + "元/" + outOrder.Item.PriceUnit;
                }
                else
                {
                    if (outOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).UserCode + " " + outOrder.Item.Name + " 【规格】" + outOrder.Item.Specs + " 【单价】" + outOrder.Item.Price.ToString() + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;
                        if (!string.IsNullOrEmpty(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).Product.Manual))
                        {
                            showInfo += "\r\n" + "【药品说明】" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).Product.Manual;
                        }
                    }
                    else
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrder.Item.ID).UserCode + " " + outOrder.Item.Name + " 【规格】" + outOrder.Item.Specs + " 【单价】" + outOrder.Item.Price.ToString() + "元/" + outOrder.Item.PriceUnit;
                    }
                  
                  
                }
                if (outOrder.Item.ID != "999")
                {
                    #region 项目扩展信息提示

                    string itemShowInfo = "";

                    if (this.Patient != null && !string.IsNullOrEmpty(this.Patient.ID))
                    {
                        FS.HISFC.Models.SIInterface.Compare compare = Order.Classes.Function.GetPactItem(outOrder);
                        outOrder.Patient.Pact = this.Patient.Pact;
                        if (compare != null)
                        {
                            //医保对照信息
                            itemShowInfo += "\r\n【" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(outOrder.Patient.Pact.ID).Name + "】 " + Order.Classes.Function.GetItemGrade(compare.CenterItem.ItemGrade) + " " + (compare.CenterItem.Rate > 0 ? compare.CenterItem.Rate.ToString("p0") : "") + (compare.CenterFlag == "1" ? "【需审批】" : "");


                            //医保限制用药信息
                            if (!string.IsNullOrEmpty(compare.Practicablesymptomdepiction))
                            {
                                itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\r\n" : " ") + compare.Practicablesymptomdepiction;
                            }
                        }
                    }

                    //基本药物提示
                    string ss = Order.Classes.Function.GetPhaEssentialDrugs(outOrder.Item);
                    if (!string.IsNullOrEmpty(ss))
                    {
                        itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\t" : " ") + "[" + ss + "]";
                    }

                    //肿瘤用药提示
                    ss = Order.Classes.Function.GetPhaForTumor(outOrder.Item);
                    if (!string.IsNullOrEmpty(ss))
                    {
                        itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\t" : " ") + "[" + ss + "]";
                    }

                    //项目内涵 暂无

                    showInfo += itemShowInfo;

                    #endregion

                    //套餐明细
                    if (outOrder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrder.Item.ID);
                        if (undrug.UnitFlag == "1")
                        {
                            showInfo += "\r\n【套餐包含】：";

                            ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(outOrder.Item.ID);
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb comb in alZt)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(comb.ID);
                                showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "]") + " " + comb.Qty + combUndrug.PriceUnit + "；";
                            }
                        }
                    }


                    //附材信息
                    FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();
                    ArrayList alSub = subMgr.GetSubtblInfoByItem("0", outOrder.ReciptDept.ID, outOrder.Item.ID, outOrder.Usage.ID);
                    if (alSub != null && alSub.Count > 0)
                    {
                        showInfo += "\r\n【附材带出】(供参考)：";
                        foreach (FS.HISFC.Models.Order.OrderSubtblNew sub in alSub)
                        {
                            FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(sub.Item.ID);
                            showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "] ") + "；";
                        }
                    }
                }
                //获取非药品的备注信息 add by yerl
                FS.SOC.HISFC.Fee.Models.Undrug undrugInfo = new FS.SOC.HISFC.Fee.BizLogic.Undrug().GetUndrug(outOrder.Item.ID);

                if (undrugInfo != null)
                {
                    if (!string.IsNullOrEmpty(undrugInfo.Memo))
                    {
                        showInfo += "\r\n";
                        showInfo += "【注意事项】" + undrugInfo.Memo;
                    }
                    showInfo += "\r\n";
                    //showInfo += "【备注】如有特殊情况,请致电相关科室：[超声科]38379766 [内镜中心]38254166 [放射科]38286789";
                }
                txtItemInfo.Text = showInfo;

                if (string.IsNullOrEmpty(txtItemInfo.Text))
                {
                    this.pnItemInfo.Visible = false;
                }

                #endregion

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ucOutPatientOrder" + ex.Message);
                return -1;
            }
        }
        #endregion

        #region IToolBar 成员

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int Del()
        {
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} 重构了一个删除函数
            return Del(this.neuSpread1.ActiveSheet.ActiveRowIndex, false);
        }

        /// <summary>
        /// 删除的处方ID，用于保存时删除
        /// </summary>
        private Hashtable hsDeleteOrder = new Hashtable();

        /// <summary>
        /// 删除选定项目
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="isDirctDel">是否提示删除，否则不提示直接删除</param>
        /// <returns></returns>
        private int Del(int rowIndex, bool isNotice)
        {
            #region 全部删除功能
            int j = rowIndex;
            DialogResult r = DialogResult.Yes;
            bool isHavePha = false;
            FS.HISFC.Models.Order.OutPatient.Order order = null;//,temp=null;
            if (j < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("请先选择一条医嘱！");
                return 0;
            }
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                this.neuSpread1.Sheets[0].Cells[i, GetColumnIndexFromName("警")].Tag = "";
            }
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                //标志所有选择行
                if (this.neuSpread1.Sheets[0].IsSelected(i, 0))
                {
                    this.neuSpread1.Sheets[0].Cells[i, GetColumnIndexFromName("警")].Tag = "1";
                }
            }

            if (!isNotice)
            {
                r = ucOutPatientItemSelect1.MessageBoxShow("是否删除所选定医嘱\n ！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (r == DialogResult.Yes)
            {
                for (int i = this.neuSpread1_Sheet1.Rows.Count - 1; i >= 0; i--)
                {
                    if (this.neuSpread1.Sheets[0].Cells[i, GetColumnIndexFromName("警")].Tag != null
                        && this.neuSpread1.Sheets[0].Cells[i, GetColumnIndexFromName("警")].Tag.ToString() == "1")
                    {
                        this.DeleteSingleOrder(i);
                    }
                }
            }
            this.ucOutPatientItemSelect1.Clear(false);

            this.RefreshCombo();
            this.RefreshOrderState();
            #endregion

            return 0;
        }

        /// <summary>
        /// 删除一个处方
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int DeleteOneOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            //附材可能在接口里面已经删除过了
            if (order.IsSubtbl)
            {
                FS.HISFC.Models.Order.OutPatient.Order orderTemp = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                if (orderTemp == null)
                {
                    return 1;
                }
            }

            //删除医嘱
            if (CacheManager.OutOrderMgr.DeleteOrder(order.SeeNO, FS.FrameWork.Function.NConvert.ToInt32(order.ID)) <= 0)
            {
                errInfo = "项目【" + order.Item.Name + "】可能已经收费，请退出开立界面重试!\r\n" + CacheManager.OutOrderMgr.Err;
                return -1;
            }
            //删除费用
            if (CacheManager.FeeIntegrate.DeleteFeeItemListByMoOrder(order.ID) == -1)
            {
                errInfo = "项目【" + order.Item.Name + "】可能已经收费，请退出开立界面重试!\r\n" + CacheManager.OutOrderMgr.Err;
                return -1;
            }

            #region 医嘱带的附材的删除{D256A1B3-F969-4d2c-92C3-9A5508835D5B}
            //重新组合可能组合号改变，修改为按照处方号获取费用明细

            ArrayList alSubAndOrder = CacheManager.FeeIntegrate.QueryValidFeeDetailbyClinicCodeAndRecipeSeq(this.currentPatientInfo.ID, order.ReciptSequence);
            if (alSubAndOrder == null)
            {
                errInfo = CacheManager.FeeIntegrate.Err;
                return -1;
            }
            else
            {
                int rev = -1;
                for (int s = 0; s < alSubAndOrder.Count; s++)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList item = alSubAndOrder[s] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    if (item.Item.IsMaterial)
                    {
                        rev = CacheManager.FeeIntegrate.DeleteFeeItemListByRecipeNO(item.RecipeNO, item.SequenceNO.ToString());

                        if (rev == 0)
                        {
                            errInfo = "项目【" + item.Name + "】对应的附材已经收费，不允许删除！\r\n请退出界面重试！";
                            return -1;
                        }
                        else if (rev < 0)
                        {
                            errInfo = CacheManager.FeeIntegrate.Err;
                        }
                    }
                }
            }
            #endregion

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            #region 账户新增
            if (this.isAccountMode)
            {
                int resultValue = 0;
                if (isAccountTerimal && Patient.IsAccount)
                {
                    //删除药品申请信息
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!order.IsHaveCharged)
                        {
                            resultValue = CacheManager.PhaIntegrate.DelApplyOut(order);
                            if (resultValue < 0)
                            {
                                errInfo = CacheManager.PhaIntegrate.Err;
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        if (order.Item.IsNeedConfirm && !order.IsHaveCharged)
                        {
                            //删除非药品终端申请信息
                            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                            FS.HISFC.BizProcess.Integrate.Terminal.Confirm confrimIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
                            resultValue = confrimIntegrate.DelTecApply(order.ReciptNO, order.SequenceNO.ToString());
                            if (resultValue <= 0)
                            {
                                errInfo = "删除终端申请信息失败！" + confrimIntegrate.Err;
                                return -1;
                            }
                        }
                    }
                }
            }

            #endregion

            order.DCOper.ID = CacheManager.OutOrderMgr.Operator.ID;
            order.DCOper.OperTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            if (this.isSaveOrderHistory)
            {
                if (CacheManager.OutOrderMgr.InsertOrderChangeInfo(order) < 0)
                {
                    errInfo = "保存" + order.Item.Name + "修改纪录出错！" + CacheManager.OutOrderMgr.Err;
                    return -1;
                }
            }

            #region 删除预扣库存信息 {E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
            if (isPreUpdateStockinfoByOrder)
            {
                if (DealPreStock(true, order) == -1)
                {
                    return -1;
                }
            }
            #endregion

            #region 电子申请单 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504

            string isUsePacsApply = Classes.Function.GetBatchControlParam("200212", false, "0");
            if (isUsePacsApply == "1")
            {
                if (order.ApplyNo != null)
                {
                    IOutPatientPacsApply.Delete(this.Patient, order);
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 删除单条
        /// </summary>
        /// <returns></returns>
        public int DeleteSingleOrder()
        {
            #region 删除功能

            DialogResult r = DialogResult.Yes;

            if (neuSpread1.ActiveSheet.ActiveRowIndex < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("请先选择一条医嘱！");
                return 0;
            }

            FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[neuSpread1.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.OutPatient.Order;

            if (order == null)
            {
                return 0;
            }
            r = ucOutPatientItemSelect1.MessageBoxShow("是否删除所选定医嘱【" + order.Item.Name + "】\n ！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.No)
            {
                return 0;
            }

            if (r == DialogResult.Yes)
            {
                this.DeleteSingleOrder(neuSpread1.ActiveSheet.ActiveRowIndex);
            }

            this.ucOutPatientItemSelect1.Clear(false);

            this.RefreshCombo();
            this.RefreshOrderState();
            #endregion

            return 1;
        }


        /// <summary>
        /// 删除一个处方
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public int DeleteSingleOrder(int rowIndex)
        {
            FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.Order.OutPatient.Order;

            if (order == null)
            {
                return 0;
            }

            if (order.ReciptDoctor.ID != CacheManager.OutOrderMgr.Operator.ID)
            {
                ucOutPatientItemSelect1.MessageBoxShow("该医嘱不是当前医生开立,不能删除!", "提示");
                return 0;
            }

            if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, this.GetReciptDoct(), this.GetReciptDept(), DoctorPrivType.RecipePriv, true, ref errInfo) <= 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "提示");
                return 0;
            }

            if (order.ID == "") //自然删除
            {
                this.neuSpread1.ActiveSheet.Rows.Remove(rowIndex, 1);
            }
            else
            {
                //FS.HISFC.Models.Order.OutPatient.Order orderTemp = CacheManager.OrderMgr.QueryOneOrder(this.Patient.ID, order.ID);

                if (order.Status == 0)
                {
                    //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                    FS.HISFC.Models.Order.OutPatient.Order temp = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);

                    //找不到时提示
                    if (temp == null)
                    {
                        //ucOutPatientItemSelect1.MessageBoxShow("删除医嘱失败！\r\n" + CacheManager.OrderMgr.Err, "错误");
                        //return -1;
                    }
                    else
                    {
                        if (!this.hsDeleteOrder.Contains(temp.ID))
                        {
                            hsDeleteOrder.Add(temp.ID, temp);
                        }
                    }
                    this.neuSpread1.ActiveSheet.Rows.Remove(rowIndex, 1);
                }
                else
                {
                    ucOutPatientItemSelect1.MessageBoxShow("医嘱:[" + order.Item.Name + "]已经收费，不能进行删除操作！", "提示");
                    return 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// 设置控费数量
        /// </summary>
        /// <param name="alExce"></param>
        private void ResetNum(Dictionary<string, decimal> alExce)
        {
            if (hsDeleteOrder == null || hsDeleteOrder.Keys.Count == 0 || alExce == null || alExce.Keys.Count == 0)
            {
                return;
            }
            FS.HISFC.Models.Order.OutPatient.Order order = null;

            foreach (string orderID in new ArrayList(hsDeleteOrder.Keys))
            {
                order = hsDeleteOrder[orderID] as FS.HISFC.Models.Order.OutPatient.Order;

                if (order != null && !string.IsNullOrEmpty(order.ReciptSequence) && alExce.ContainsKey(order.Item.ID))
                {
                    alExce[order.Item.ID] = alExce[order.Item.ID] + order.Qty;
                }
            }


        }

        /// <summary>
        /// 确认删除
        /// </summary>
        /// <param name="errInfo"></param>
        /// <param name="feeSeq">返回 收费序列，用于门诊处方全删时，记录原有收费序列，以显示正确总费用</param>
        /// <returns></returns>
        private int DelCommit(ref string errInfo, ref string feeSeq)
        {
            feeSeq = "";

            if (hsDeleteOrder == null || hsDeleteOrder.Keys.Count == 0)
            {
                return 1;
            }
            FS.HISFC.Models.Order.OutPatient.Order order = null;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            orderExtMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            orderExtMgr2.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //{97B9173B-834D-49a1-936D-E4D04F98E4BA}
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            CacheManager.RadtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (string orderID in new ArrayList(hsDeleteOrder.Keys))
            {
                //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                order = hsDeleteOrder[orderID] as FS.HISFC.Models.Order.OutPatient.Order;

                if (!string.IsNullOrEmpty(order.ReciptSequence))
                {
                    feeSeq = order.ReciptSequence;
                }

                if (this.DeleteOneOrder(order) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }

                #region 处理医保限制性用药

                if (Patient != null && this.Patient.Pact.PayKind.ID == "02")
                {
                    if (indicationsHelper.GetObjectFromID(GetItemUserCode(order.Item)) != null)
                    {
                        FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = this.orderExtMgr.QueryByClinicCodOrderID(order.Patient.ID, order.ID);
                        if (orderExtObj != null)
                        {
                            if (orderExtMgr.DeleteOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除医嘱扩展信息错误：\r\n" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }
                    }
                }

                #endregion
            }

            //删除后清空
            this.hsDeleteOrder.Clear();
            return 1;
        }

        #endregion

        /// <summary>
        /// exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Exit(object sender, object neuObject)
        {
            // TODO:  添加 ucOrder.Exit 实现
            if (this.IsDesignMode)
            {

            }
            else
            {
                this.FindForm().Close();
            }

            return 0;
        }

        /// <summary>
        /// 获取患者最新状态信息
        /// </summary>
        /// <returns></returns>
        private int GetRecentPatientInfo()
        {
            try
            {
                #region 获取患者最新状态信息

                //避免患者状态改变后还能收费，
                //避免看诊后没有获取看诊状态、看诊医生等信息导致多次收费

                string memo = this.currentPatientInfo.Memo;

                //查询有效的挂号记录
                ArrayList alRegister = CacheManager.RegInterMgr.QueryPatientByState(this.currentPatientInfo.ID, "all");
                if (alRegister == null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("查询患者挂号信息出错!可能是患者已经退号！\r\n" + CacheManager.RegInterMgr.Err);
                    return -1;
                }

                if (alRegister.Count > 1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("该患者挂号信息已作废，请刷新界面!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else if (alRegister.Count == 1)
                {
                    ((FS.HISFC.Models.Registration.Register)alRegister[0]).DoctorInfo.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO;

                    ////已经看诊 并且不是当前医生
                    //if (
                    //    //!currentPatientInfo.IsSee
                    //    //&& 
                    //    ((FS.HISFC.Models.Registration.Register)alRegister[0]).IsSee
                    //    && ((FS.HISFC.Models.Registration.Register)alRegister[0]).SeeDoct.ID != this.GetReciptDoct().ID
                    //    )
                    //{
                    //    if (ucOutPatientItemSelect1.MessageBoxShow("患者【" + currentPatientInfo.Name + "】已经看诊，\r\n看诊医生为:" + ((FS.HISFC.Models.Registration.Register)alRegister[0]).SeeDoct.Name + "，\r\n看诊时间为" + ((FS.HISFC.Models.Registration.Register)alRegister[0]).SeeDoct.OperTime.ToString() + "，\r\n\r\n是否继续看诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //    {
                    //        return -1;
                    //    }
                    //}

                    this.currentPatientInfo = alRegister[0] as FS.HISFC.Models.Registration.Register;

                    if (this.currentPatientInfo != null)
                    {
                        this.currentPatientInfo.Pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(currentPatientInfo.Pact.ID);
                    }

                    this.currentPatientInfo.Memo = memo;
                }
                else
                {

                }
                #endregion

                return 1;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 添加，开立
        /// </summary>
        /// <param name="IsNew">是否强制新开立</param>
        /// <returns></returns>
        public int Add(bool IsNew)
        {
            //检查时候已经传入患者信息
            if (this.currentPatientInfo == null || this.currentPatientInfo.ID == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("没有选择患者，请双击选择患者");
                return -1;
            }

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            if (this.IBeforeAddOrder != null)
            {
                if (this.IBeforeAddOrder.OnBeforeAddOrderForOutPatient(this.Patient, this.GetReciptDept(), this.GetReciptDoct()) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(IBeforeAddOrder.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }

            if (IsNew)
            {
                Patient.DoctorInfo.SeeNO = -1;
                QueryOrder();
            }

            this.hsDeleteOrder.Clear();
            this.IsDesignMode = true;

            this.ucOutPatientItemSelect1.Clear(true);

            //this.ucOutPatientItemSelect1.Focus();

            //此处处理只在第一次超过限额时提示
            this.isShowFeeWarning = false;

            if (this.dealSublMode == 1)
            {
                this.CalculatSubl(true);
            }

            this.PassRefresh();

            return 0;
        }

        /// <summary>
        /// 设置输入焦点
        /// </summary>
        public void SetInPutFocos()
        {
            this.ucOutPatientItemSelect1.Clear(true);
        }

        /// <summary>
        /// 留观登记
        /// </summary>
        /// <returns></returns>
        public int RegisterEmergencyPatient()
        {
            //Classes.LogManager.Write(currentPatientInfo.Name + "开始急诊功能!");
            //检查时候已经传入患者信息
            if (this.currentPatientInfo == null || this.currentPatientInfo.ID == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("没有选择患者，请双击选择患者");
                return -1;
            }

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            string dept = CacheManager.OutOrderMgr.ExecSqlReturnOne(string.Format(@"select see_dpcd from fin_opr_register t
                                                                    where t.card_no='{0}'
                                                                    and t.in_state!='N' 
                                                                    and t.in_state is not null", this.Patient.PID.CardNO));
            if (!string.IsNullOrEmpty(dept) && dept != "-1" && dept != this.GetReciptDept().ID)
            {
                FS.HISFC.Models.Base.Department deptObj = CacheManager.InterMgr.GetDepartment(dept);
                if (deptObj != null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("该患者已在" + deptObj.Name + "留观！");
                }
                return -1;
            }

            DateTime now = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(CacheManager.OrderMgr.Connection);
            //t.BeginTransaction();
            CacheManager.RadtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = CacheManager.OutOrderMgr.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//获得新的看诊序号
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("获取看诊序号失败：" + CacheManager.OutOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

            }

            if (this.AddRegInfo(Patient) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                return -1;
            }

            if (this.isAccountMode)
            {
                #region 检查是否已存在留观费
                string strSql = string.Format(@"select count(t.item_code) from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                --and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='急诊留观费'", this.Patient.ID, this.GetReciptDoct().ID);
                string revStr = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSql);
                int rev = FS.FrameWork.Function.NConvert.ToInt32(revStr);
                if (rev > 0)
                {
                    if (ucOutPatientItemSelect1.MessageBoxShow("该患者已收取留观费，是否继续收取？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else if (rev == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("查询留观费用失败：" + CacheManager.OutOrderMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                #endregion

                if (GetEmergencyFee(ref alEmergencyFee) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                    return -1;
                }
                if (this.alEmergencyFee != null && this.alEmergencyFee.Count > 0)
                {
                    #region 处理已收挂号费用

                    rev = CacheManager.OutOrderMgr.ExecNoQuery(@"delete from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='挂号费'", this.Patient.ID, this.GetReciptDoct().ID);
                    if (rev == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        ucOutPatientItemSelect1.MessageBoxShow("删除已补收的挂号费失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                    //能够删除未收费的补收费用
                    else if (rev > 0)
                    {
                    }
                    //删除不成功，查找已收费记录
                    else if (rev == 0)
                    {
                        string sql = @"SELECT DISTINCT t.INVOICE_NO from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='1'
                                                AND t.CANCEL_FLAG='1'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='挂号费'";
                        sql = string.Format(sql, this.Patient.ID, this.GetReciptDoct().ID);
                        string invoiceNo = CacheManager.OutOrderMgr.ExecSqlReturnOne(sql);
                        if (!string.IsNullOrEmpty(invoiceNo) && invoiceNo.Trim() != "-1")
                        {
                            sql = @"SELECT DISTINCT t.INVOICE_NO from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='1'
                                                and t.DOCT_CODE='{1}'
                                                AND t.CANCEL_FLAG='1'
                                                and t.invoice_no='{2}'
                                                and (t.package_name!='挂号费' or t.package_name is null)";
                            sql = string.Format(sql, this.Patient.ID, this.GetReciptDoct().ID, invoiceNo);
                            string invoiceNoTemp = CacheManager.OutOrderMgr.ExecSqlReturnOne(sql);
                            if (invoiceNoTemp == invoiceNo && !string.IsNullOrEmpty(invoiceNo) && invoiceNo.Trim() != "-1")
                            {
                                ucOutPatientItemSelect1.MessageBoxShow("该患者的急诊留观费已产生发票信息，请患者到收费处退诊查费！\n发票号码为“" + invoiceNo + "”！");
                                //System.Windows.Forms.Clipboard.Clear();
                                //System.Windows.Forms.Clipboard.SetDataObject(invoiceNo);
                            }
                            else
                            {
                                sql = @"SELECT DISTINCT INVOICE_SEQ FROM FIN_OPB_INVOICEINFO WHERE INVOICE_NO='{0}'";
                                sql = string.Format(sql, invoiceNo);
                                string invoiceSeq = CacheManager.OutOrderMgr.ExecSqlReturnOne(sql);

                                //挂号费发票作废，同时退还账户金额
                                if (CacheManager.FeeIntegrate.LogOutInvoiceByAccout(this.Patient, invoiceNo, invoiceSeq) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    ucOutPatientItemSelect1.MessageBoxShow("退还已收诊金失败:" + CacheManager.OutOrderMgr.Err);
                                    return -1;
                                }
                            }
                        }
                    }
                    #endregion

                    #region 补收急诊留观费用

                    string errText = "";
                    if (alEmergencyFee != null && alEmergencyFee.Count > 0)
                    {
                        if (regLevlFee.RegFee != 0)
                        {
                            regFeeItem = this.SetSupplyFeeItemListByItem(regItem, ref errInfo);
                            if (regFeeItem == null)
                            {
                                return -1;
                            }
                            regFeeItem.UndrugComb.Name = "急诊留观费";

                            alEmergencyFee.Add(regFeeItem);
                        }
                    }
                    bool iReturn = CacheManager.FeeIntegrate.SetChargeInfo(this.Patient, alEmergencyFee, now, ref errText);
                    if (iReturn == false)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        alEmergencyFee.Remove(regFeeItem);
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                    #endregion
                }
            }
            else
            {
                if (CacheManager.RadtIntegrate.RegisterObservePatient(this.currentPatientInfo) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    alEmergencyFee.Remove(regFeeItem);
                    ucOutPatientItemSelect1.MessageBoxShow("更新留观状态失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            #region 留观后，更新看诊信息
            //收完挂号费后，更新挂号表已收费状态，避免多次补收挂号费
            if (CacheManager.RegInterMgr.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                alEmergencyFee.Remove(regFeeItem);
                dirty = false;
                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.ConManager.Err);
                return -1;
            }

            //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                if (CacheManager.RegInterMgr.UpdateSeeDone(this.currentPatientInfo.ID) < 0)
                {
                    errInfo = "更新看诊标志出错！";

                    return -1;
                }

                if (CacheManager.RegInterMgr.UpdateDept(this.currentPatientInfo.ID, CacheManager.LogEmpl.Dept.ID, CacheManager.LogEmpl.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    alEmergencyFee.Remove(regFeeItem);
                    ucOutPatientItemSelect1.MessageBoxShow("更新看诊科室、医生出错！");
                    return -1;
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            this.currentPatientInfo.PVisit.InState.ID = "R";
            this.currentPatientInfo.IsFee = true;

            ucOutPatientItemSelect1.MessageBoxShow("留观成功！");
            //Classes.LogManager.Write(currentPatientInfo.Name + "结束急诊功能!");
            return 1;
        }

        /// <summary>
        /// 获取留观费
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private int GetEmergencyFee(ref ArrayList alEmergencyFee)
        {
            alEmergencyFee = new ArrayList();

            FS.HISFC.Models.Fee.Item.Undrug supplyItem;
            FS.HISFC.Models.Fee.Outpatient.FeeItemList emergencyFeeItem = null;

            string supplyItemCode = "";
            ArrayList emergencyConstList = CacheManager.GetConList("EmergencyFeeItem");
            if (emergencyConstList == null)
            {
                errInfo = "获取急诊留观项目失败！" + CacheManager.ConManager.Err;
                return -1;
            }
            else if (emergencyConstList.Count == 0)
            {
                return 0;
            }

            for (int i = 0; i < emergencyConstList.Count; i++)
            {
                supplyItem = new FS.HISFC.Models.Fee.Item.Undrug();
                supplyItemCode = ((FS.FrameWork.Models.NeuObject)emergencyConstList[i]).ID.Trim();

                if (this.CheckItem(supplyItemCode, ref errInfo, ref supplyItem) == -1)
                {
                    errInfo = "急诊留观项目" + errInfo;
                    return -1;
                }
                emergencyFeeItem = this.SetSupplyFeeItemListByItem(supplyItem, ref errInfo);

                if (emergencyFeeItem == null)
                {
                    //ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                    return -1;
                }

                //对于补收的费用做个标记
                //emergencyFeeItem.UndrugComb.ID = this.oper.ID;
                emergencyFeeItem.UndrugComb.Name = "急诊留观费";
                alEmergencyFee.Add(emergencyFeeItem);
            }
            return 1;
        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// 出关登记
        /// </summary>
        /// <returns></returns>
        public int OutEmergencyPatient()
        {
            if (this.currentPatientInfo == null || this.currentPatientInfo.ID == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("没有选择患者，请双击选择患者");
                return -1;
            }

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            if (!this.isAccountMode)
            {
                if (this.currentPatientInfo.PVisit.InState.ID.ToString() == "N")
                {
                    ucOutPatientItemSelect1.MessageBoxShow("该患者还未留观！");
                    return -1;
                }
            }

            #region 先不判断接诊状态
            //if (this.currentPatientInfo.PVisit.InState.ID.ToString() == "R")
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("该患者还未接诊不能出观！");
            //    return -1;
            //}

            //if (this.currentPatientInfo.PVisit.InState.ID.ToString() != "I")
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("患者未留观不能做出观处理！");
            //    return -1;
            //}
            #endregion
            else
            {
                #region 检查是否已存在留观费
                string strSql = string.Format(@"select count(t.item_code) from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                --and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='急诊留观费'", this.Patient.ID, this.GetReciptDoct().ID);
                string revStr = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSql);
                int rev = FS.FrameWork.Function.NConvert.ToInt32(revStr);
                if (rev <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("未找到留观费用，不用出关！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                #endregion

                if (ucOutPatientItemSelect1.MessageBoxShow("是否删除今天收取的留观费用？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //取消留观，删除留观费
                    if (this.alEmergencyFee != null && this.alEmergencyFee.Count > 0)
                    {
                        rev = CacheManager.OutOrderMgr.ExecNoQuery(@"delete from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='急诊留观费'", this.Patient.ID, this.GetReciptDoct().ID);
                        if (rev == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            ucOutPatientItemSelect1.MessageBoxShow("删除已补收的急诊留观费失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                        else if (rev == 0)
                        {
                            string sql = @"SELECT DISTINCT t.INVOICE_NO from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='1'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='急诊留观费'";
                            sql = string.Format(sql, this.Patient.ID, this.GetReciptDoct().ID);
                            string invoiceNo = CacheManager.OutOrderMgr.ExecSqlReturnOne(sql);
                            if (!string.IsNullOrEmpty(invoiceNo) && invoiceNo.Trim() != "-1")
                            {
                                ucOutPatientItemSelect1.MessageBoxShow("该患者的急诊留观费已产生发票信息，请手工退费！\n发票号码为“" + invoiceNo + "！");
                            }
                        }
                        else
                        {
                            //对于是否应该收取挂号费和诊金不在判断
                        }
                    }
                    ucOutPatientItemSelect1.MessageBoxShow("如果要再次收取挂号费和诊金，请手工录入！");
                }
            }

            if (!this.isAccountMode)
            {
                if (CacheManager.RadtIntegrate.OutObservePatientManager(this.currentPatientInfo, EnumShiftType.EO, "出关") < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("更新留观状态失败！\r\n" + CacheManager.RadtIntegrate.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            ucOutPatientItemSelect1.MessageBoxShow("取消急诊留观成功！");
            return 1;
        }

        /// <summary>
        /// 留观转住院
        /// </summary>
        /// <returns></returns>
        public int InEmergencyPatient()
        {
            //检查时候已经传入患者信息
            if (this.currentPatientInfo == null || this.currentPatientInfo.ID == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("没有选择患者，请双击选择患者");
                return -1;
            }

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            if (this.currentPatientInfo.PVisit.InState.ID.ToString() == "R")
            {
                ucOutPatientItemSelect1.MessageBoxShow("该患者还未接诊不能转住院！");
                return -1;
            }

            if (this.currentPatientInfo.PVisit.InState.ID.ToString() != "I")
            {
                ucOutPatientItemSelect1.MessageBoxShow("患者未留观不能做转住院处理！");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (CacheManager.RadtIntegrate.OutObservePatientManager(this.currentPatientInfo, EnumShiftType.CPI, "留观转住院") < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow("更新留观状态失败！");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            ucOutPatientItemSelect1.MessageBoxShow("转住院成功！");
            return 1;
        }

        /// <summary>
        /// 退出医嘱更改
        /// </summary>
        /// <returns></returns>
        public int ExitOrder()
        {
            bool isHaveNew = false;

            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order ord = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                if (ord != null && ord.ID.Length <= 0)
                {
                    isHaveNew = true;
                    break;
                }
            }

            if (isHaveNew || hsDeleteOrder.Count > 0)
            {
                if (ucOutPatientItemSelect1.MessageBoxShow("当前还有未保存的医嘱，确定退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }
            }

            this.IsDesignMode = false;
            this.isAddMode = false;
            return 0;
        }

        /// <summary>
        /// 存放医嘱信息 主键是seeNO+ID
        /// </summary>
        private Hashtable hsOrder = new Hashtable();

        /// <summary>
        /// 存放医嘱项目信息（数据库重取）
        /// </summary>
        private Hashtable hsOrderItem = new Hashtable();

        /// <summary>
        /// 获取处方合同单位
        /// houwb 医保方自费方问题 {A1FE7734-267C-43f1-A824-BF73B482E0B2}
        /// </summary>
        /// <param name="outOrder"></param>
        private FS.HISFC.Models.Order.OutPatient.Order GetOrderPactInfo(FS.HISFC.Models.Order.OutPatient.Order outOrder)
        {
            if (!this.pnOrderPactInfo.Visible)
            {
                return outOrder;
            }

            if (rdPact1.Checked && rdPact1.Visible &&
                rdPact1.Tag != null &&
                rdPact1.Tag.GetType() == typeof(FS.HISFC.Models.Base.PactInfo))
            {
                outOrder.Patient.Pact = rdPact1.Tag as FS.HISFC.Models.Base.PactInfo;
                return outOrder;
            }
            else if (rdPact2.Checked && rdPact2.Visible &&
                rdPact2.Tag != null &&
                rdPact2.Tag.GetType() == typeof(FS.HISFC.Models.Base.PactInfo))
            {
                outOrder.Patient.Pact = rdPact2.Tag as FS.HISFC.Models.Base.PactInfo;
                return outOrder;
            }
            else if (rdPact3.Checked && rdPact3.Visible &&
                rdPact3.Tag != null &&
                rdPact3.Tag.GetType() == typeof(FS.HISFC.Models.Base.PactInfo))
            {
                outOrder.Patient.Pact = rdPact3.Tag as FS.HISFC.Models.Base.PactInfo;
                return outOrder;
            }
            else if (rdPact4.Checked && rdPact4.Visible &&
                rdPact4.Tag != null &&
                rdPact4.Tag.GetType() == typeof(FS.HISFC.Models.Base.PactInfo))
            {
                outOrder.Patient.Pact = rdPact4.Tag as FS.HISFC.Models.Base.PactInfo;
                return outOrder;
            }
            return outOrder;
        }

        Dictionary<string, decimal> alExce = new Dictionary<string, decimal>();

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            Classes.LogManager.Write(currentPatientInfo.Name + "开始保存函数!");
            if (this.bIsDesignMode == false)
            {
                return -1;
            }

            this.ucOutPatientItemSelect1.SetInputControlVisible(false);

            string strID = "";
            string strNameNotUpdate = "";//没有更新的医嘱名称
            string reciptNo = "";//处方号
            bool bHavePha = false;//是否包含药品(处方预览使用)


            FS.HISFC.Models.Order.OutPatient.Order order;
            DateTime now = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();


            #region 获取处方列表

            ArrayList alAllOrder = new ArrayList();

            string seenno = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
            alExce = CacheManager.OutOrderMgr.GetRecipExceededItem(this.Patient.PID.CardNO, seenno, this.Patient.ID);

            ResetNum(alExce);

            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                if (order != null && alExce != null && alExce.ContainsKey(order.Item.ID))
                {
                    decimal num = alExce[order.Item.ID];
                    if (num > 0)
                    {
                        order.IsExceeded = true;

                        alExce[order.Item.ID] = alExce[order.Item.ID] - order.Qty;
                    }
                }

                if (order != null)
                {
                    alAllOrder.Add(order.Clone());
                }
            }

            #endregion

            #region 保存之前的判断

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            this.hsOrder.Clear();
            this.hsOrderItem.Clear();

            //在此处加载所有医嘱列表和医嘱项目列表

            int iReturnCheckOrder = this.CheckOrder();
            if (iReturnCheckOrder == -1)
            {
                return -1;
            }
            else if (iReturnCheckOrder == -2)
            {
                #region 返回处理
                this.IsDesignMode = false;
                this.isAddMode = false;

                //保存后清空
                this.hsComboChange = new Hashtable();
                //this.alSupplyFee = new ArrayList();

                Classes.LogManager.Write(currentPatientInfo.Name + "结束保存函数!");
                #endregion
                return -2;
            }

            #region 补挂号

            //辅材处理接口
            if (IDealSubjob != null && dealSublMode == 0)
            {
                if (dealSublMode == 0)
                {
                    if (this.SetSupplyRegFee(ref alSupplyFee, ref errInfo, currentPatientInfo.IsFee) == -1)
                    {
                        this.alSupplyFee = new ArrayList();
                        ucOutPatientItemSelect1.MessageBoxShow("补收挂号费失败：" + errInfo);
                        return -1;
                    }

                    if (this.CheckChargedRegFeeIsRight(alSupplyFee, ref errInfo) == -1)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                        return -1;
                    }

                    Classes.LogManager.Write(currentPatientInfo.Name + "初始化补收时，补收挂号费项目为：\r\n");
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alSupplyFee)
                    {
                        Classes.LogManager.Write(currentPatientInfo.Name + item.Item.ID + " " + item.Item.Name + "  数量：" + item.Item.Qty.ToString() + "\r\n");
                    }
                }
            }
            else
            {
                alSupplyFee = new ArrayList();
            }

            #endregion

            //医嘱变更接口{48E6BB8C-9EF0-48a4-9586-05279B12624D}
            if (this.IAlterOrderInstance == null)
            {
                this.InitAlterOrderInstance();
            }
            #endregion

            bool isAccount = false;

            #region 账户判断
            if (this.isAccountMode)
            {
                #region 此处先不扣账户费
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                if (isAccountTerimal)
                {
                    decimal vacancy = 0m;
                    if (this.Patient.IsAccount)
                    {
                        if (CacheManager.FeeIntegrate.GetAccountVacancy(this.Patient.PID.CardNO, ref vacancy) <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(CacheManager.FeeIntegrate.Err);
                            return -1;
                        }
                        isAccount = true;
                    }
                }
                #endregion
            }

            #endregion

            #region 保存处方前接口实现
            //用于提示、警告等等
            if (IBeforeSaveOrder != null)
            {
                if (IBeforeSaveOrder.BeforeSaveOrderForOutPatient(Patient, this.GetReciptDept(), this.GetReciptDoct(), alAllOrder) == -1)
                {
                    if (!string.IsNullOrEmpty(IBeforeSaveOrder.ErrInfo))
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(IBeforeSaveOrder.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return -1;
                }
            }

            #endregion

            #region 声明
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存医嘱，请稍后。。。");
            Application.DoEvents();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.OutOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //设置事务
            CacheManager.InterMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            CacheManager.FeeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);//设置事务
            CacheManager.RegInterMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            CacheManager.PhaIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);//{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}

            #endregion

            errInfo = "";

            //收费序列，用于门诊处方全删时，记录原有收费序列，以显示正确总费用
            string feeSeq = "";
            if (this.DelCommit(ref errInfo, ref feeSeq) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow("删除医嘱失败：" + errInfo);
                return -1;
            }

            #region 判断看诊序号
            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = CacheManager.OutOrderMgr.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//获得新的看诊序号
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    ucOutPatientItemSelect1.MessageBoxShow(CacheManager.OutOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }

            #endregion

            #region 处理医嘱及辅材

            ArrayList alOrder = new ArrayList(); //保存医嘱
            ArrayList alFeeItem = new ArrayList();//保存费用
            ArrayList alSubOrders = new ArrayList();//附材数组
            ArrayList alOrderChangedInfo = new ArrayList();//医嘱修改纪录
            ArrayList alDrugOrders = new ArrayList();
            bool iReturn = false;
            string errText = "";

            //提示重复药品
            string repeatItemName = "";
            Hashtable hsOrderItem = new Hashtable();

            string itemKey = "";

            //存储医嘱流水号与限制性用药标记
            Dictionary<string, string> dicIndications = new Dictionary<string, string>();

            //检查项目科室
            Dictionary<string, string> dicPacsExetDepts = new Dictionary<string, string>();
            

            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();

                itemKey = order.Item.ID;
                if (order.Item.ID == "999")
                {
                    itemKey = order.Item.Name;
                }

                if (!hsOrderItem.Contains(itemKey))
                {
                    hsOrderItem.Add(itemKey, null);
                }
                else
                {
                    if (!repeatItemName.Contains(order.Item.Name))
                    {
                        repeatItemName = string.IsNullOrEmpty(repeatItemName) ? order.Item.Name : (repeatItemName + "\r\n" + order.Item.Name);
                    }
                }

                if (order.Status == 0)//未审核的医嘱
                {
                    #region 保存医嘱

                    if (order.ID == "") //new 新加的医嘱
                    {
                        strID = Classes.Function.GetNewOrderID(ref errInfo);
                        if (strID == "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1;
                        }

                        order.ID = strID;    //申请单号
                        order.ReciptNO = reciptNo;
                        order.SequenceNO = 0;

                        if (!string.IsNullOrEmpty(feeSeq) && !order.IsExceeded)
                        {
                            order.ReciptSequence = feeSeq;
                        }
                        else
                        {
                            order.ReciptSequence = "";
                        }

                        if (order.Item.ItemType == EnumItemType.Drug || order.Item.SysClass.ID.ToString() == "UL" || order.Item.MinFee.ID.Equals("028"))
                        {
                            bHavePha = true;
                            //Add by liuww 2013-06-05
                            alDrugOrders.Add(order);
                        }
                        alOrder.Add(order);

                        #region 插入预扣库存{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                        if (this.isPreUpdateStockinfoByOrder)
                        {
                            if (DealPreStock(false, order) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }

                        #endregion

                        #region 账户患者的复合项目需拆成明细再划价

                        bool isExist = false;
                        //if (this.Patient.IsAccount)
                        //{
                        //    if (order.Item is FS.HISFC.Models.Fee.Item.Undrug)
                        //    {
                        //        FS.HISFC.Models.Fee.Item.Undrug undrugInfo = new FS.HISFC.Models.Fee.Item.Undrug();

                        //        if (this.hsOrderItem.Contains(order.Item.ID))
                        //        {
                        //            undrugInfo = hsOrderItem[order.Item.ID] as FS.HISFC.Models.Fee.Item.Undrug; ;

                        //            if (undrugInfo == null)
                        //            {
                        //                undrugInfo = this.CacheManager.FeeIntegrate.GetItem(order.Item.ID);
                        //            }

                        //            if (undrugInfo == null)
                        //            {
                        //                FS.FrameWork.Management.PublicTrans.RollBack();
                        //                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        //                ucOutPatientItemSelect1.MessageBoxShow("查询非药品项目：" + order.Item.Name + "出错！" + this.CacheManager.FeeIntegrate.Err);
                        //                return -1;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            undrugInfo = this.CacheManager.FeeIntegrate.GetItem(order.Item.ID);
                        //            if (undrugInfo == null)
                        //            {
                        //                FS.FrameWork.Management.PublicTrans.RollBack();
                        //                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        //                ucOutPatientItemSelect1.MessageBoxShow("查询非药品项目：" + order.Item.Name + "出错！" + this.CacheManager.FeeIntegrate.Err);
                        //                return -1;
                        //            }
                        //        }

                        //        order.Item.IsNeedConfirm = undrugInfo.IsNeedConfirm;
                        //        ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).NeedConfirm = undrugInfo.NeedConfirm;

                        //        //复合项目的先不优化了
                        //        if (undrugInfo.UnitFlag == "1")
                        //        {
                        //            ArrayList alOrderDetails = Classes.Function.ChangeZtToSingle(order, this.Patient, this.Patient.Pact);
                        //            if (alOrderDetails != null)
                        //            {
                        //                FS.HISFC.Models.Fee.Outpatient.FeeItemList tmpFeeItemList = null;

                        //                foreach (FS.HISFC.Models.Order.OutPatient.Order tmpOrder in alOrderDetails)
                        //                {
                        //                    tmpFeeItemList = Classes.Function.ChangeToFeeItemList(GetOrderPactInfo(tmpOrder), currentPatientInfo);
                        //                    if (tmpFeeItemList != null)
                        //                    {
                        //                        alFeeItem.Add(tmpFeeItemList.Clone());
                        //                        isExist = true;
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        if (!isExist)
                        {
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList alFeeItemListTmp = Classes.Function.ChangeToFeeItemList(GetOrderPactInfo(order), currentPatientInfo);
                            if (alFeeItemListTmp == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(order.Item.Name + "医嘱实体转换成费用实体出错。", "提示");
                                return -1;
                            }
                            alFeeItem.Add(alFeeItemListTmp);
                        }
                        #endregion
                    }
                    else //update 更新的医嘱
                    {
                        #region 获得需要更新的医嘱
                        //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        FS.HISFC.Models.Order.OutPatient.Order newOrder = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                        //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                        if (newOrder == null || newOrder.Status == 0)
                        {
                            newOrder = order;
                        }

                        if (newOrder.Status != 0 || newOrder.IsHaveCharged)//检查并发医嘱状态
                        {
                            strNameNotUpdate += "[" + order.Item.Name + "]";

                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("[" + order.Item.Name + "]可能已经收费,请退出开立界面重新进入!\r\n" + CacheManager.OutOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1;
                            continue;
                        }

                        if (newOrder.Item.ItemType == EnumItemType.Drug || newOrder.Item.SysClass.ID.ToString() == "UL" || newOrder.Item.MinFee.ID.Equals("028"))
                        {
                            bHavePha = true;
                            //Add by liuww 2013-06-05
                            alDrugOrders.Add(newOrder);

                        }
                        alOrder.Add(newOrder);

                        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitems = Classes.Function.ChangeToFeeItemList(GetOrderPactInfo(order), currentPatientInfo);
                        if (feeitems == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(order.Item.Name + "医嘱实体转换成费用实体出错！", "提示");
                            return -1;
                        }
                        alFeeItem.Add(feeitems);

                        #endregion

                        #region 插入预扣库存{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                        if (this.isPreUpdateStockinfoByOrder)
                        {
                            if (DealPreStock(false, order) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }
                        #endregion
                    }

                    #endregion

                    string noHypo = CacheManager.OutOrderMgr.TransHypotest(FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest);

                    string Hypo = CacheManager.OutOrderMgr.TransHypotest(FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest);

                    order.Memo = order.Memo.Replace(noHypo, "").Replace(Hypo, "");

                    order.Memo += CacheManager.OutOrderMgr.TransHypotest(order.HypoTest);

                    order.Item.Name = order.Item.Name.Replace(noHypo, "").Replace(Hypo, "");

                    //记录医保限制性用药信息

                    if (neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("付数/天数")].Tag != null)
                    {
                        string flag = neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("付数/天数")].Tag.ToString();
                        if (!dicIndications.ContainsKey(order.ID))
                        {
                            dicIndications.Add(order.ID, flag);
                        }
                        else
                        {
                            dicIndications[order.ID] = flag;
                        }
                    }


                    //记录检查执行科室和医嘱
                    if (order.Item.SysClass.ID.ToString() == "UC" && (order.ExeDept.ID == "6003" || order.ExeDept.ID == "6004"))
                    {
                        if (dicPacsExetDepts.ContainsKey(order.ExeDept.ID))
                        {
                            dicPacsExetDepts[order.ExeDept.ID] = order.ExeDept.Name;
                        }
                        else
                        {
                            dicPacsExetDepts.Add(order.ExeDept.ID, order.ExeDept.Name);
                        }
                    }

                }
            }

            if (!string.IsNullOrEmpty(repeatItemName))
            {
                if (this.ParentForm != null)
                {
                    Components.Order.Classes.Function.ShowBalloonTip(8, "提示", "存在重复项目：\r\n" + repeatItemName, ToolTipIcon.Info);
                }
            }


            //辅材处理接口
            if (IDealSubjob != null)
            {
                if (dealSublMode == 0)
                {
                    IDealSubjob.IsPopForChose = false;
                    if (alOrder.Count > 0)
                    {
                        if (IDealSubjob.DealSubjob(this.Patient, alOrder, null, ref alSubOrders, ref errText) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败！" + errText);
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 检查申请目的

            if (dicPacsExetDepts.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                frmPacsApply frmPacsApply = new frmPacsApply();
                frmPacsApply.setInfo(this.Patient.ID,dicPacsExetDepts, alOrder);
                frmPacsApply.ShowDialog();
                this.PurPose = frmPacsApply.PurPoses;
                //执行科室数量应等于检查目的数量
                if (PurPose.Count != dicPacsExetDepts.Count)
                {
                    return -1;
                }

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存医嘱，请稍后。。。");
            }

            #endregion 

            #region 未挂号患者，此处插入挂号信息

            if (this.AddRegInfo(Patient) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                dirty = false;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                return -1;
            }

            //收完挂号费后，更新挂号表已收费状态，避免多次补收挂号费
            if (CacheManager.RegInterMgr.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                dirty = false;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.ConManager.Err);
                return -1;
            }

            #region 补收挂号费项目

            //正常挂号患者都补收费用
            if (this.Patient.PVisit.InState.ID.ToString() == "N")
            {
                if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj in alSupplyFee)
                    {
                        alFeeItem.Add(feeItemObj);
                    }

                    //alFeeItem.AddRange(this.alSupplyFee);
                }
            }
            #endregion

            #endregion

            #region 合并收费数组


            Classes.LogManager.Write(currentPatientInfo.Name + "附材接口项目为：\r\n");
            foreach (FS.HISFC.Models.Order.OutPatient.Order subOrder in alSubOrders)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList alFeeItemListTmp = Classes.Function.ChangeToFeeItemList(GetOrderPactInfo(subOrder), currentPatientInfo);
                if (alFeeItemListTmp == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(subOrder.Item.Name + "医嘱实体转换成费用实体出错。", "提示");
                    return -1;
                }
                Classes.LogManager.Write(currentPatientInfo.Name + alFeeItemListTmp.Item.ID + " " + alFeeItemListTmp.Item.Name + "  数量：" + alFeeItemListTmp.Item.Qty.ToString() + "\r\n");

                alFeeItem.Add(alFeeItemListTmp);
            }

            #endregion

            #region 收费之前记录所有项目

            Classes.LogManager.Write(currentPatientInfo.Name + "收费项目为：\r\n");

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFeeItem)
            {
                Classes.LogManager.Write(currentPatientInfo.Name + item.Item.ID + " " + item.Item.Name + "  数量：" + item.Item.Qty.ToString() + "\r\n");

            }

            #endregion

            #region 收费
            Classes.LogManager.Write(currentPatientInfo.Name + "开始收费!");
            //处方号和流水号规则由费用业务层函数统一生成
            try
            {
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                if (isAccountTerimal && isAccount)
                {
                    iReturn = CacheManager.FeeIntegrate.SetChargeInfoToAccount(this.Patient, alFeeItem, now, ref errText);
                    if (iReturn == false)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                }
                else
                {
                    //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
                    //直接收费 1成功 -1失败 0普通患者不处理走正常划价
                    if (IDoctFee != null && false)
                    {
                        int resultValue = IDoctFee.DoctIdirectFee(this.Patient, alFeeItem, now, ref errText);
                        if (resultValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                        if (resultValue == 0)
                        {
                            iReturn = CacheManager.FeeIntegrate.SetChargeInfo(this.Patient, alFeeItem, now, ref errText);
                            if (iReturn == false)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        iReturn = CacheManager.FeeIntegrate.SetChargeInfo(this.Patient, alFeeItem, now, ref errText);
                        if (iReturn == false)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(errText + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            Classes.LogManager.Write(currentPatientInfo.Name + "结束收费!");
            #endregion

            #region 回馈处方号和处方流水号

            FS.HISFC.Models.Order.OutPatient.Order tempOrder = null;
            for (int k = 0; k < alOrder.Count; k++)
            {
                tempOrder = alOrder[k] as FS.HISFC.Models.Order.OutPatient.Order;

                //用于新的分方函数，多次修改保存可能会更改处方号，所以每次都要回馈
                //if (tempOrder.ReciptNO == null || tempOrder.ReciptNO == "")
                //{
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFeeItem)
                {
                    if (tempOrder.ID == feeitem.Order.ID)
                    {
                        tempOrder.ReciptNO = feeitem.RecipeNO;
                        tempOrder.SequenceNO = feeitem.SequenceNO;
                        tempOrder.ReciptSequence = feeitem.RecipeSequence;

                        break;
                    }
                }
                //}
            }
            #endregion

            #region 保存医嘱 插入或更新处方表

            #region 根据接口实现对医嘱信息进行补充判断
            //{48E6BB8C-9EF0-48a4-9586-05279B12624D}
            if (this.IAlterOrderInstance != null)
            {
                List<FS.HISFC.Models.Order.OutPatient.Order> orderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                for (int j = 0; j < alOrder.Count; j++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp
                    = alOrder[j] as FS.HISFC.Models.Order.OutPatient.Order;
                    if (temp == null)
                    {
                        continue;
                    }
                    orderList.Add(temp);
                }
                if (this.IAlterOrderInstance.AlterOrder(this.currentPatientInfo, this.reciptDoct, this.reciptDept, ref orderList) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }
            }

            #endregion

            //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
            if (!isAccount && IDoctFee != null)
            {
                if (IDoctFee.UpdateOrderFee(this.Patient, alOrder, now, ref errText) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("更新医嘱收费标记失败！" + errText);
                    return -1;
                }
            }

            for (int j = 0; j < alOrder.Count; j++)
            {
                FS.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as FS.HISFC.Models.Order.OutPatient.Order;

                if (temp == null)
                {
                    continue;
                }

                #region 金额四舍五舍

                temp.FT.PubCost = GetCost(temp.FT.PubCost);
                temp.FT.PayCost = GetCost(temp.FT.PayCost);
                temp.FT.OwnCost = GetCost(temp.FT.OwnCost);

                #endregion

                #region 插入医嘱表
                if (CacheManager.OutOrderMgr.UpdateOrder(temp) == -1) //保存医嘱档
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("插入医嘱出错！" + temp.Item.Name + "可能已经收费,请退出开立界面重新进入!\r\n" + CacheManager.OutOrderMgr.Err);
                    return -1;
                }
                #endregion

                #region 医嘱扩展表保存唐氏相关必填信息{97B9173B-834D-49a1-936D-E4D04F98E4BA}
                if (temp.Item.ID == "F00000000716" || temp.Item.ID == "F00000023411")
                {
                    // FS.HISFC.Models.Order.OutPatient.OrderLisExtend orderExtObj = new FS.HISFC.Models.Order.OutPatient.OrderLisExtend();
                    FS.HISFC.Components.Order.OutPatient.Forms.frmTanShiInfoSet tanshi;
                    FS.HISFC.Models.Order.OutPatient.OrderLisExtend orderExtObj = orderExtMgr2.QueryByClinicCodOrderID(this.Patient.ID, temp.ID);
                    if (orderExtObj == null)
                    {
                        orderExtObj = new FS.HISFC.Models.Order.OutPatient.OrderLisExtend();
                        tanshi = new FS.HISFC.Components.Order.OutPatient.Forms.frmTanShiInfoSet(orderExtObj);
                        tanshi.ShowDialog();
                        orderExtObj = tanshi.orderExtObj;
                    }
                    else
                    {
                        tanshi = new FS.HISFC.Components.Order.OutPatient.Forms.frmTanShiInfoSet(orderExtObj);
                        tanshi.InitInfo();
                        tanshi.ShowDialog();
                        orderExtObj = tanshi.orderExtObj;
                    }
                    orderExtObj.ClinicCode = this.Patient.ID;
                    orderExtObj.Indications = temp.Item.Name;
                    orderExtObj.MoOrder = temp.ID;
                    orderExtObj.Oper.ID = orderExtMgr2.Operator.ID;
                    if (tanshi.confirmSave == true)
                    {
                        if (orderExtMgr2.InsertOrderExtend(orderExtObj) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("插入扩展信息表出错！" + orderExtMgr2.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                #endregion

                #region 医嘱扩展表 //医保限制性用药已废弃
                if (dicIndications.ContainsKey(temp.ID))
                {
                    FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(this.Patient.ID, temp.ID);
                    FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(GetItemUserCode(temp.Item));
                    if (indicationsObj != null
                        && !string.IsNullOrEmpty(indicationsObj.ID))
                    {
                        if (orderExtObj == null)
                        {
                            orderExtObj = new FS.HISFC.Models.Order.OutPatient.OrderExtend();
                        }
                        orderExtObj.ClinicCode = this.Patient.ID;
                        orderExtObj.Indications = dicIndications[temp.ID];
                        orderExtObj.MoOrder = temp.ID;
                        orderExtObj.Oper.ID = orderExtMgr.Operator.ID;
                        int rev = orderExtMgr.UpdateOrderExtend(orderExtObj);
                        if (rev == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("更新扩展信息表出错！" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (rev == 0)
                        {
                            if (orderExtMgr.InsertOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow("插入扩展信息表出错！" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }


                #endregion

                #region 医嘱拓展表//保存检查目的
                if (PurPose != null)
                {
                    if (temp.Item.SysClass.ID.ToString() == "UC")
                    {
                        string strpurpose = string.Empty;

                        foreach (var key in PurPose.Keys)
                        {
                            if (temp.ExeDept.Name == key)
                            {
                                strpurpose = this.PurPose[key].ToString();
                            }
                        }


                        FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(this.Patient.ID, temp.ID);

                        if (orderExtObj == null)
                        {
                            orderExtObj = new FS.HISFC.Models.Order.OutPatient.OrderExtend();
                        }
                        orderExtObj.ClinicCode = this.Patient.ID;
                        orderExtObj.Indications = "";
                        orderExtObj.MoOrder = temp.ID;
                        orderExtObj.Oper.ID = orderExtMgr.Operator.ID;
                        orderExtObj.Extend1 = strpurpose;//备注1保存检查目的
                        int rev = orderExtMgr.UpdateOrderExtend(orderExtObj);
                        if (rev == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("更新扩展信息表出错！" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (rev == 0)
                        {
                            if (orderExtMgr.InsertOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow("插入扩展信息表出错！" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }   
                    }
                }
                #endregion 

            }
            #endregion

            #region 插入医嘱变更纪录

            if (this.isSaveOrderHistory)
            {
                FS.HISFC.Models.Order.OutPatient.Order temp = null;

                for (int j = 0; j < alOrder.Count; j++)
                {
                    temp = alOrder[j] as FS.HISFC.Models.Order.OutPatient.Order;

                    if (this.alAllOrder == null || this.alAllOrder.Count <= 0 || temp == null)
                    {
                        continue;
                    }

                    FS.HISFC.Models.Order.OutPatient.Order tem
                        = this.orderHelper.GetObjectFromID(temp.ID) as FS.HISFC.Models.Order.OutPatient.Order;

                    if (tem == null)
                    {
                        continue;
                    }

                    #region 判断是否需要保存
                    //修改总量
                    if (tem.Qty != temp.Qty)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //修改单位
                    else if (tem.Unit != temp.Unit)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //修改每次量
                    else if (tem.DoseOnce != temp.DoseOnce)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //每次单位
                    else if (tem.DoseUnit != temp.DoseUnit)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //草药付数
                    else if (tem.HerbalQty != temp.HerbalQty)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //用法
                    else if (tem.Usage.ID != temp.Usage.ID)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //频次
                    else if (tem.Frequency.ID != temp.Frequency.ID)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //执行科室
                    else if (tem.ExeDept.ID != temp.ExeDept.ID)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //备注
                    else if (tem.Memo != temp.Memo)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //接瓶
                    else if (tem.ExtendFlag1 != temp.ExtendFlag1)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //组合
                    else if (tem.Combo.ID != temp.Combo.ID)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //院注
                    else if (tem.InjectCount != temp.InjectCount)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //加急
                    else if (tem.IsEmergency != temp.IsEmergency)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //皮试
                    else if (tem.HypoTest != temp.HypoTest)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //检验附材
                    else if (tem.NurseStation.User01 != temp.NurseStation.User01)
                    {
                        alOrderChangedInfo.Add(tem);
                        continue;
                    }
                    #endregion

                }

                //插入变更记录表
                for (int i = 0; i < alOrderChangedInfo.Count; i++)
                {
                    temp = alOrderChangedInfo[i] as FS.HISFC.Models.Order.OutPatient.Order;

                    if (CacheManager.OutOrderMgr.InsertOrderChangeInfo(temp) < 0)
                    {
                        //对于变更记录出错也不提示
                        //FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        //ucOutPatientItemSelect1.MessageBoxShow("插入医嘱变更纪录出错！");
                        //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        //return -1;
                    }
                }
            }
            #endregion

            #region 更新看诊信息
            if (isUseNurseArray && this.currentPatientInfo.IsTriage)
            {
                if (this.currentRoom != null)
                {
                    if (CacheManager.InterMgr.UpdateAssignSaved(this.currentRoom.ID, this.currentPatientInfo.ID, now, CacheManager.OutOrderMgr.Operator.ID) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("更新分诊标志出错！");
                        return -1;
                    }
                }

                #region 接口结束看诊

                if (this.INurseAssign != null)
                {
                    int rInt = this.INurseAssign.DiagOut(this.currentPatientInfo, null, null, null, null, null, ref errInfo);
                }

                #endregion

            }

            //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                if (CacheManager.RegInterMgr.UpdateSeeDone(this.currentPatientInfo.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("更新看诊标志出错！");
                    return -1;
                }

                if (CacheManager.RegInterMgr.UpdateDept(this.currentPatientInfo.ID, CacheManager.LogEmpl.Dept.ID, CacheManager.LogEmpl.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("更新看诊科室、医生出错！");
                    return -1;
                }
            }

            #endregion

            #region 提交
            FS.FrameWork.Management.PublicTrans.Commit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //对于补挂号的，保存成功才能更新已收费标记
            if (!this.Patient.IsFee)
            {
                this.Patient.IsFee = true;
            }

            //更新患者状态为已诊后，更改基本信息中患者看诊状态
            this.Patient.IsSee = true;

            #endregion

            #region 账户扣取挂号费


            Classes.LogManager.Write(currentPatientInfo.Name + "开始扣除挂号费!");

            int iRes = 1;
            if (this.isAccountMode)
            {
                //ucOutPatientItemSelect1.MessageBoxShow("如有本科室执行项目，请在终端刷卡扣费！");

                //正常挂号患者都补收费用
                if (this.Patient.PVisit.InState.ID.ToString() == "N")
                {
                    if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
                    {
                        FS.HISFC.BizProcess.Integrate.AccountFee.OutPatientFeeManage accountManager = new FS.HISFC.BizProcess.Integrate.AccountFee.OutPatientFeeManage();
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        errInfo = "";
                        iRes = accountManager.ChargeFee(this.Patient, alSupplyFee, out errInfo);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            //ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n 可能原因是：账户余额不足，请患者到收费处充值后缴费！");
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.Commit();
                            //ucOutPatientItemSelect1.MessageBoxShow("扣取挂号费和诊金成功！");
                        }

                    }
                }
            }
            Classes.LogManager.Write(currentPatientInfo.Name + "结束扣除挂号费!");
            #endregion

            #region 提示信息放到一起

            Classes.LogManager.Write(currentPatientInfo.Name + "处方保存成功!");

            if (strNameNotUpdate == "")//已经变化的医嘱信息
            {
                ucOutPatientItemSelect1.MessageBoxShow("医嘱保存成功！");
            }
            else
            {
                ucOutPatientItemSelect1.MessageBoxShow("医嘱保存成功！\n" + strNameNotUpdate + "医嘱状态已经在其它地方更改，无法进行更新，请刷新屏幕！");
            }

            //正常挂号患者都补收费用
            if (this.isAccountMode)
            {
                if (this.Patient.PVisit.InState.ID.ToString() == "N")
                {
                    if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
                    {
                        if (iRes <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n 可能原因是：账户余额不足，请患者到收费处充值后缴费！");
                        }
                        else
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("看诊费扣费成功！");
                        }
                    }
                }
            }
            #endregion

            #region 更新医嘱序号
            if (this.SaveSortID(0) < 0)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow("更新医嘱序号出错！");
                return -1;
            }
            #endregion

            #region 接入电子申请单
            string isUsePacsApply = CacheManager.FeeIntegrate.GetControlValue("200212", "0");
            if (isUsePacsApply == "1" && !object.Equals(IOutPatientPacsApply, null))
            {
                IOutPatientPacsApply.Save(this.Patient, null);
            }
            #endregion

            #region 返回处理
            this.IsDesignMode = false;
            this.isAddMode = false;

            //保存后清空
            this.hsComboChange = new Hashtable();
            this.alSupplyFee = new ArrayList();
            #endregion

            #region 自动打印处方

            if (isAutoPrintRecipe)
            {
                //this.PrintRecipe();
                PrintAllBill("0", true);
            }

            #endregion

            #region 外部接口实现

            if (IAfterSaveOrder != null)
            {
                if (IAfterSaveOrder.OnSaveOrderForOutPatient(this.Patient, this.GetReciptDept(), this.GetReciptDoct(), alOrder) != 1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(IAfterSaveOrder.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            #endregion

            isShowFeeWarning = false;


            Classes.LogManager.Write(currentPatientInfo.Name + "结束保存函数!");
            return 0;
        }

        #region  add by lijp 2011-11-25 电子申请单添加 {102C4C01-8759-4b93-B4BA-1A2B4BB1380E}

        /// <summary>
        /// 是否启用电子申请单 
        /// </summary>
        private bool isUsePacsApply = false;

        /// <summary>
        ///  保存申请单信息
        /// </summary>
        public void SavePacsApply()
        {
            if (IOutPatientPacsApply == null)
            {
                this.InitPacsApply();
            }
            IOutPatientPacsApply.Save(this.Patient, null);
        }

        /// <summary>
        /// 编辑申请单
        /// </summary>
        public void EditPascApply()
        {
            try
            {
                if (!this.isUsePacsApply)
                {
                    MessageBox.Show("未启用申请单");
                    return;
                }

                if (IOutPatientPacsApply == null)
                {
                    this.InitPacsApply();
                }

                // 从医嘱获取申请单号。
                FS.HISFC.Models.Order.OutPatient.Order order =
                    this.GetObjectFromFarPoint(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
                IOutPatientPacsApply.Edit(this.Patient, order);
            }
            catch
            {
                MessageBox.Show("没有开立有效的检查项目医嘱");
            }
        }

        #endregion

        #region 补挂号费

        /// <summary>
        /// 急诊挂号级别
        /// </summary>
        string emergRegLevl = "";

        /// <summary>
        /// 急诊号是否有效
        /// </summary>
        bool isValideEmergReglevl = true;

        /// <summary>
        /// 职级对应的挂号级别
        /// </summary>
        string regLevl_DoctLevl = "";

        /// <summary>
        /// 急诊留观补收费用
        /// </summary>
        ArrayList alEmergencyFee = new ArrayList();

        /// <summary>
        /// 获取急诊挂号级别编码
        /// </summary>
        /// <returns></returns>
        private string GetEmergRegLevlCode()
        {
            string emergencyLevlCode = "";

            //获取所有的挂号级别
            ArrayList al = CacheManager.RegInterMgr.QueryAllRegLevel();

            if (al == null || al.Count == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("查询所有挂号级别错误！会导致补收挂号费错误!\r\n请联系信息科重新维护" + CacheManager.RegInterMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            else
            {
                bool isValidEmergency = true;
                foreach (FS.HISFC.Models.Registration.RegLevel regLevl in al)
                {
                    if (regLevl.IsEmergency)
                    {
                        emergencyLevlCode = regLevl.ID;
                        if (regLevl.IsValid)
                        {
                            isValidEmergency = true;
                        }
                        else if (regLevl.IsEmergency)
                        {
                            isValidEmergency = false;
                        }
                        break;
                    }
                }

                if (string.IsNullOrEmpty(emergencyLevlCode))
                {
                    ucOutPatientItemSelect1.MessageBoxShow("急诊挂号级别没有维护！会导致补收挂号费错误!\r\n如果无急诊挂号级别，请增加维护并停用即可！\r\n请联系信息科重新维护" + CacheManager.RegInterMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                isValideEmergReglevl = isValidEmergency;
                return emergencyLevlCode;
            }
        }

        /// <summary>
        /// 获取补挂号项目
        /// </summary>
        /// <returns></returns>
        private int InitSupplyItem()
        {
            //{61CC27CE-6E87-4412-8CCF-051A3862DDBD}
            //if (isInitSupplyItem)
            //{
            //    return 1;
            //}

            //之后加控制参数吧
            oper = CacheManager.InterMgr.GetEmployeeInfo(this.GetReciptDoct().ID);

            #region 查找差额项目

            diffDiagItem = null;

            string diffDiagItemCode = "";
            ArrayList diffDiagConstList = CacheManager.GetConList("DiffDiagItem");
            if (diffDiagConstList == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取挂号差额项目失败！" + CacheManager.ConManager.Err);
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                return -1;
            }
            else if (diffDiagConstList.Count > 0)
            {
                diffDiagItemCode = ((FS.FrameWork.Models.NeuObject)diffDiagConstList[0]).ID.Trim();
            }
            if (!string.IsNullOrEmpty(diffDiagItemCode))
            {
                if (this.CheckItem(diffDiagItemCode, ref errInfo, ref diffDiagItem) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("挂号差额项目" + errInfo);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }
            }

            #endregion

            #region 急诊费

            //急诊诊查费
            emergRegItem = new FS.HISFC.Models.Fee.Item.Undrug();

            emergRegLevl = GetEmergRegLevlCode();

            //if (string.IsNullOrEmpty(emergRegLevl))
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("获取急诊号错误，请联系信息科！");
            //    //return -1;
            //}

            if (!string.IsNullOrEmpty(emergRegLevl))
            {
                string emgergItemCode = "";
                if (CacheManager.RegInterMgr.GetSupplyRegInfo(this.GetReciptDept().ID, emergRegLevl, ref emgergItemCode) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(CacheManager.RegInterMgr.Err);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }

                if (this.CheckItem(emgergItemCode, ref errInfo, ref emergRegItem) == -1)
                {
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    ucOutPatientItemSelect1.MessageBoxShow("获取急诊挂号对应的诊查费项目失败！\r\n" + errInfo);
                    return -1;
                }

                //FS.FrameWork.Models.NeuObject emergRegConst = this.CacheManager.ConManager.GetConstant("REGLEVEL_DIAGFEE", emergRegLevl);
                //if (emergRegConst == null || string.IsNullOrEmpty(emergRegConst.ID))
                //{
                //    ucOutPatientItemSelect1.MessageBoxShow("没有维护急诊号对应的诊查费！");
                //    //return -1;
                //}

                //if (this.CheckItem(emergRegConst.Name.Trim(), ref errInfo, ref emergRegItem) == -1)
                //{
                //    ucOutPatientItemSelect1.MessageBoxShow("急诊号" + errInfo);
                //    //return -1;
                //}
            }

            #endregion

            #region 医生职称对应的诊查费

            diagItem = new FS.HISFC.Models.Fee.Item.Undrug();
            //{A1BAF267-6053-44e3-B96E-36E2C48DE4BD}
            string regLevl = Patient.DoctorInfo.Templet.RegLevel.ID;
            string diagItemCode = "";

            //改为按患者挂号级别收
            if (CacheManager.RegInterMgr.GetSupplyRegInfo(oper.ID, oper.Level.ID.ToString(), this.GetReciptDept().ID, ref regLevl, ref diagItemCode) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.RegInterMgr.Err);
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                return -1;
            }
            regLevl_DoctLevl = regLevl;

            if (this.CheckItem(diagItemCode, ref errInfo, ref diagItem) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("医生的职级[" + oper.Level.Name + "]对应的诊查费项目" + errInfo);
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                return -1;
            }

            #endregion

            #region 补收的挂号费项目

            regItem = new FS.HISFC.Models.Fee.Item.Undrug();

            //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
            {
                string regItemCode = "";
                ArrayList regConstList = CacheManager.GetConList("RegFeeItem");
                if (regConstList == null || regConstList.Count == 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("获取挂号费项目失败！" + CacheManager.ConManager.Err);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }
                else if (regConstList.Count > 0)
                {
                    regItemCode = ((FS.FrameWork.Models.NeuObject)regConstList[0]).ID.Trim();
                }

                if (this.CheckItem(regItemCode, ref errInfo, ref regItem) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("挂号费项目" + errInfo);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }
            }
            #endregion

            #region 免挂号费的科室

            ArrayList alNoSupplyRegDept = CacheManager.GetConList("NoSupplyRegDept");
            if (alNoSupplyRegDept == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.ConManager.Err);
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                return -1;
            }

            foreach (FS.HISFC.Models.Base.Const obj in alNoSupplyRegDept)
            {
                if (!hsNoSupplyRegDept.Contains(obj.ID) && obj.IsValid)
                {
                    hsNoSupplyRegDept.Add(obj.ID, obj);
                }
            }

            #endregion

            #region 急诊留观费用

            alEmergencyFee = new ArrayList();

            FS.HISFC.Models.Fee.Item.Undrug supplyItem;
            FS.HISFC.Models.Fee.Outpatient.FeeItemList emergencyFeeItem = null;

            string supplyItemCode = "";
            ArrayList emergencyConstList = CacheManager.GetConList("EmergencyFeeItem");
            if (emergencyConstList == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取急诊留观项目失败！" + CacheManager.ConManager.Err);
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                return -1;
            }
            else if (emergencyConstList.Count == 0)
            {
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                return 0;
            }

            for (int i = 0; i < emergencyConstList.Count; i++)
            {
                supplyItem = new FS.HISFC.Models.Fee.Item.Undrug();
                supplyItemCode = ((FS.FrameWork.Models.NeuObject)emergencyConstList[i]).ID.Trim();

                if (this.CheckItem(supplyItemCode, ref errInfo, ref supplyItem) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("急诊留观项目" + errInfo);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }
                emergencyFeeItem = this.SetSupplyFeeItemListByItem(supplyItem, ref errInfo);

                if (emergencyFeeItem == null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }

                //对于补收的费用做个标记
                //emergencyFeeItem.UndrugComb.ID = this.oper.ID;
                emergencyFeeItem.UndrugComb.Name = "急诊留观费";
                alEmergencyFee.Add(emergencyFeeItem);
            }

            #endregion

            isInitSupplyItem = true;

            return 1;
        }

        /// <summary>
        /// 当前患者的挂号级别信息
        /// </summary>
        FS.HISFC.Models.Registration.RegLvlFee regLevlFee = new FS.HISFC.Models.Registration.RegLvlFee();

        /// <summary>
        /// 补收挂号费项目
        /// </summary>
        FS.HISFC.Models.Fee.Outpatient.FeeItemList regFeeItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

        /// <summary>
        /// 门诊补收诊查费是不是顺德妇幼模式：挂号只收挂号费，医生站补收诊金和差额项目
        /// </summary>
        //private bool isShunDeFuYou = false;

        /// <summary>
        /// 是否急诊号
        /// </summary>
        private bool isEmergency = false;

        /// <summary>
        /// 是否院内职工免挂号费 0 不减免；1 免挂号费；2 免诊金 3 全免
        /// </summary>
        private int emplFreeRegType = 0;

        /// <summary>
        /// 是否院内职工，根据身份证号判断
        /// </summary>
        private bool isEmpl = false;

        /// <summary>
        /// 是否收取诊金，0表示不收取；1表示收取 其他表示还未查询
        /// </summary>
        private int payDiageFee = -1;

        /// <summary>
        /// 是否补收诊金（有一些简易门诊不收诊金）
        /// </summary>
        /// <returns></returns>
        private bool isPayDiagFee()
        {
            if (payDiageFee == -1)
            {
                ArrayList alNoDiageFeeDept = CacheManager.ConManager.GetList("NoDiageFeeDept");
                if (alNoDiageFeeDept == null || alNoDiageFeeDept.Count == 0)
                {
                    payDiageFee = 1;
                }
                else
                {
                    foreach (FS.HISFC.Models.Base.Const constObj in alNoDiageFeeDept)
                    {
                        if (constObj.IsValid
                            && constObj.ID == GetReciptDept().ID)
                        {
                            payDiageFee = 0;
                            break;
                        }
                    }

                    if (payDiageFee == -1)
                    {
                        payDiageFee = 1;
                    }
                }
            }
            if (payDiageFee == 0)
            {
                return false;
            }
            else if (payDiageFee == 1)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 补收挂号费:挂号费+诊查费
        ///{EF052C04-D357-4409-84E5-3E6102766746}
        /// </summary>
        /// <param name="alSupplyFee">补收的费用列表</param>
        /// <param name="errInfo">错误信息</param>
        /// <param name="isReged">是否已收过挂号费</param>
        /// <returns></returns>
        private int SetSupplyRegFee(ref ArrayList alSupplyFee, ref string errInfo, bool isFee)
        {
            /*
             * 存在三个诊金费用：
             * 1、挂号级别对应的诊金
             * 2、医生职级对应的诊金
             * 3、急诊时候对应的急诊诊金
             * 
             * 急诊收急诊诊金，按挂号级别收，挂号级别没维护，按医生职称 
             * 
             * */
            try
            {
                if (isAutoAddSupplyFee == 0)
                {
                    alSupplyFee = new ArrayList();
                    return 1;
                }

                if (this.InitSupplyItem() == -1)
                {
                    errInfo = "挂号费获取处错";
                    return -1;
                }

                alSupplyFee = new ArrayList();
                if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
                {
                    errInfo = "获取患者信息出错，请重新选择患者！";
                    return -1;
                }

                //已看诊，并且看诊医生为当前登录人员，退出
                if (this.Patient.IsSee && this.Patient.SeeDoct.ID == this.oper.ID)
                {
                    return 1;
                }

                if (this.hsNoSupplyRegDept != null && this.hsNoSupplyRegDept.Contains(this.GetReciptDept().ID))
                {
                    return 1;
                }

                if (!string.IsNullOrEmpty(Patient.IDCard) && CacheManager.RegInterMgr.CheckIsEmployee(Patient))
                {
                    isEmpl = true;
                }
                else
                {
                    isEmpl = false;
                }

                bool isEmerg = CacheManager.RegInterMgr.IsEmergency(this.GetReciptDept().ID);

                this.isEmergency = Patient.DoctorInfo.Templet.RegLevel.IsEmergency;

                #region 合同单位和挂号级别对应的挂号费

                if (string.IsNullOrEmpty(Patient.DoctorInfo.Templet.RegLevel.ID))
                {
                    Patient.DoctorInfo.Templet.RegLevel = CacheManager.RegInterMgr.GetRegLevl(this.GetReciptDept().ID, oper.ID, this.oper.Level.ID);
                    if (Patient.DoctorInfo.Templet.RegLevel == null)
                    {
                        errInfo = "获取挂号级别出错：" + CacheManager.RegInterMgr.Err;
                    }
                }

                if (string.IsNullOrEmpty(Patient.DoctorInfo.Templet.RegLevel.ID))
                {
                    errInfo = "补收挂号费错误，挂号级别为空！";
                }

                regLevlFee = CacheManager.RegInterMgr.GetRegLevelByPactCode(Patient.Pact.ID, Patient.DoctorInfo.Templet.RegLevel.ID);
                if (regLevlFee == null)
                {
                    errInfo = "由合同单位和挂号级别获取挂号费失败！请联系信息科重新维护" + CacheManager.RegInterMgr.Err;
                    return 0;
                }

                #endregion

                #region 一般情况

                #region 院内职工处理

                if (isEmpl && emplFreeRegType == 3)
                {
                    return 1;
                }

                #endregion

                //如果挂号时候已经收取过费用则补收差价
                //按照诊金最高收取：挂号级别对应诊金、医生职级对应诊金、急诊对应诊金（急诊时间段）
                //如果收取诊金为0，则按照上面的诊金项目全额收取；如果诊金不为0，则收取差价
                //如果是院内职工，则会根据设置是否收取诊金
                if (isFee)
                {
                    #region 不收取的情况

                    //N 正常挂号 R 留观登记 I 正在留观 P 出观登记 B 留观出院完成 E 留观转住院登记 C 留观转住院完成
                    if (this.Patient.PVisit.InState.ID.ToString() != "N")
                    {
                        return 1;
                    }

                    //已经看诊过的，此医生再次看诊不收费
                    //每次看诊都更新当前医生为看诊医生，导致前面的医生还会再次收费
                    if (this.Patient.SeeDoct.ID == this.GetReciptDoct().ID)
                    {
                        return 1;
                    }
                    if (this.Patient.Memo == "不补收")
                    {
                        return 1;
                    }

                    #endregion

                    #region 补收挂号费差额

                    //患者挂号时没有收取挂号费，则判断是否补收
                    if (Patient.RegLvlFee.RegFee == 0)
                    {
                        //是否补收挂号费
                        bool isCanSupplyRegFee = true;

                        #region 判断是否减免挂号费

                        if (isEmpl && emplFreeRegType == 1)
                        {
                            isCanSupplyRegFee = false;
                        }

                        if (Patient.Birthday.AddYears(65) < CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime())
                        {
                            isCanSupplyRegFee = false;
                        }


                        //如果挂号级别对应的挂号费为0  则不补收
                        if (regLevlFee.RegFee <= 0)
                        {
                            isCanSupplyRegFee = false;
                        }

                        #region 院内职工

                        ArrayList list = CacheManager.AccountMgr.GetMarkByCardNo(Patient.PID.CardNO, "Empl_CARD", "1");
                        if (list != null && list.Count > 0)
                        {
                            isCanSupplyRegFee = false;
                        }
                        #endregion

                        #region 挂号合同单位减免

                        if (regLevlFee != null && regLevlFee.RegFee == 0)
                        {
                            isCanSupplyRegFee = false;
                        }

                        #endregion

                        #endregion

                        if (isCanSupplyRegFee && regItem != null)
                        {
                            //对于补收的费用做个标记
                            regFeeItem = this.SetSupplyFeeItemListByItem(regItem, ref errInfo);
                            if (regFeeItem == null)
                            {
                                return -1;
                            }

                            regFeeItem.UndrugComb.Name = "挂号费";
                            alSupplyFee.Add(regFeeItem);
                        }
                    }
                    #endregion

                    #region 补收诊金差额

                    if (isPayDiagFee())
                    {
                        if (isEmpl && emplFreeRegType == 2)
                        {

                        }
                        else
                        {
                            FS.FrameWork.Models.NeuObject neuObject = CacheManager.InterMgr.GetConstansObj("civilworker", currentPatientInfo.Pact.ID);


                            //只有合同单位中的诊金不为0时，才补收
                            if (regLevlFee.OwnDigFee > 0 || (!object.Equals(neuObject, null) && !string.IsNullOrEmpty(neuObject.ID)))
                            {
                                //患者挂号时诊金为零，则按照医生职级对应的诊查费项目补收
                                if (Patient.RegLvlFee.OwnDigFee == 0)
                                {
                                    #region 获取挂号级别对应诊查费项目
                                    FS.HISFC.Models.Fee.Item.Undrug regDiagItem = new FS.HISFC.Models.Fee.Item.Undrug();
                                    //{2F018544-DADF-4a77-B00E-668D49BE8297}
                                    string tempRegLevl = Patient.DoctorInfo.Templet.RegLevel.ID;
                                    string regDiagItemCode = "";

                                    //{61CC27CE-6E87-4412-8CCF-051A3862DDBD}
                                    //if (CacheManager.RegInterMgr.GetSupplyRegInfo(GetReciptDept().ID, oper.Level.ID.ToString(), currentPatientInfo.DoctorInfo.Templet.RegLevel.ID, ref regDiagItemCode) == -1)
                                    if (CacheManager.RegInterMgr.GetSupplyRegInfo(oper.ID, oper.Level.ID.ToString(), this.GetReciptDept().ID, ref tempRegLevl, ref regDiagItemCode) == -1)
                                    {
                                        //ucOutPatientItemSelect1.MessageBoxShow(CacheManager.RegInterMgr.Err);
                                        //return -1;
                                    }
                                    if (this.CheckItem(regDiagItemCode, ref errInfo, ref regDiagItem) == -1)
                                    {
                                        //ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                                        //return -1;
                                    }
                                    #endregion

                                    #region 获取诊金最大值

                                    //FS.HISFC.Models.Fee.Outpatient.FeeItemList diagFeeItem = null;

                                    //decimal diagPrice = 0;
                                    //if (isEmerg && emergRegItem != null)
                                    //{
                                    //    diagPrice = Math.Max(emergRegItem.Price, Math.Max(regDiagItem.Price, diagItem.Price));
                                    //}
                                    //else
                                    //{
                                    //    diagPrice = Math.Max(regDiagItem.Price, diagItem.Price);
                                    //}

                                    //if (emergRegItem.Price == diagPrice)
                                    //{
                                    //    diagFeeItem = this.SetSupplyFeeItemListByItem(emergRegItem, ref errInfo);
                                    //}
                                    //else if (diagPrice == diagItem.Price)
                                    //{
                                    //    diagFeeItem = this.SetSupplyFeeItemListByItem(diagItem, ref errInfo);
                                    //}
                                    //else
                                    //{
                                    //    diagFeeItem = this.SetSupplyFeeItemListByItem(regDiagItem, ref errInfo);
                                    //}

                                    #endregion




                                    #region 获取诊金最大值

                                    FS.HISFC.Models.Fee.Outpatient.FeeItemList diagFeeItem = null;

                                    decimal diagPrice = 0;
                                    //if (isEmerg && emergRegItem != null)
                                    //{
                                    //    if (regDiagItem.Price > emergRegItem.Price)
                                    //    {
                                    //        diagFeeItem = this.SetSupplyFeeItemListByItem(regDiagItem, ref errInfo);
                                    //    }
                                    //    else
                                    //    {
                                    //        diagFeeItem = this.SetSupplyFeeItemListByItem(emergRegItem, ref errInfo);
                                    //    }
                                    //}
                                    //else 
                                    if (regDiagItem != null)
                                    {
                                        diagFeeItem = this.SetSupplyFeeItemListByItem(regDiagItem, ref errInfo);
                                    }
                                    else
                                    {
                                        diagFeeItem = this.SetSupplyFeeItemListByItem(diagItem, ref errInfo);
                                    }
                                    #endregion


                                    if (diagFeeItem == null)
                                    {
                                        return -1;
                                    }
                                    //对于补收的费用做个标记
                                    diagFeeItem.UndrugComb.Name = "挂号费";
                                    alSupplyFee.Add(diagFeeItem);
                                }
                                else
                                {
                                    //差价项目没有维护则不补收
                                    if (diffDiagItem != null && !string.IsNullOrEmpty(diffDiagItem.ID))
                                    {
                                        decimal diffCost = 0;
                                        if (isEmerg)
                                        {
                                            //diffCost = Math.Max(diagItem.Price - regLevlFee.OwnDigFee, emergRegItem.Price - regLevlFee.OwnDigFee);
                                            //按照患者实际收取的挂号费补收！
                                            //对于某类患者诊金都收取为零的，此处还是会补收差额，所以对于这类患者需要维护差额费和诊金的减免为100%
                                            diffCost = Math.Max(diagItem.Price - this.Patient.RegLvlFee.OwnDigFee, emergRegItem.Price - this.Patient.RegLvlFee.OwnDigFee);
                                        }
                                        else
                                        {
                                            diffCost = diagItem.Price - this.Patient.RegLvlFee.OwnDigFee;
                                        }
                                        if (diffCost <= 0)
                                        {
                                            return 1;
                                        }

                                        diffDiagItem.Qty = diffCost / diffDiagItem.Price;

                                        FS.HISFC.Models.Fee.Outpatient.FeeItemList diffDiagFeeItem = null;

                                        diffDiagFeeItem = this.SetSupplyFeeItemListByItem(diffDiagItem, ref errInfo);

                                        if (diffDiagFeeItem == null)
                                        {
                                            return -1;
                                        }
                                        //对于补收的费用做个标记
                                        //diffDiagFeeItem.UndrugComb.ID = this.oper.ID;
                                        diffDiagFeeItem.UndrugComb.Name = "挂号费";
                                        alSupplyFee.Add(diffDiagFeeItem);
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }

                //未挂号的全补
                //按照开立医生对应的职级收取诊查费
                else
                {
                    if (this.currentPatientInfo.Memo == "不补收")
                    {
                        return 1;
                    }

                    #region 补收挂号费

                    //是否补收挂号费
                    bool isCanSupplyRegFee = true;

                    #region 判断是否减免挂号费

                    if (isEmpl && emplFreeRegType == 1)
                    {
                        isCanSupplyRegFee = false;
                    }

                    //如果挂号级别对应的挂号费为0  则不补收
                    if (regLevlFee.RegFee <= 0)
                    {
                        isCanSupplyRegFee = false;
                    }


                    #region 院内职工

                    ArrayList list = CacheManager.AccountMgr.GetMarkByCardNo(Patient.PID.CardNO, "Empl_ CARD", "1");
                    if (list != null && list.Count > 0)
                    {
                        isCanSupplyRegFee = false;
                    }
                    #endregion

                    #region 挂号合同单位减免

                    if (regLevlFee != null && regLevlFee.RegFee == 0)
                    {
                        isCanSupplyRegFee = false;
                    }

                    #endregion

                    #endregion

                    if (isCanSupplyRegFee && regItem != null)
                    {
                        //对于补收的费用做个标记
                        regFeeItem = this.SetSupplyFeeItemListByItem(regItem, ref errInfo);
                        if (regFeeItem == null)
                        {
                            return -1;
                        }

                        regFeeItem.UndrugComb.Name = "挂号费";
                        alSupplyFee.Add(regFeeItem);
                    }

                    #endregion

                    #region 补收诊查费
                    if (isPayDiagFee())
                    {
                        if (isEmpl && emplFreeRegType == 2)
                        {

                        }
                        else
                        {
                            //该患者合同单位中诊金维护为0,则认为是减免诊查费，不再补收
                            if (regLevlFee.OwnDigFee > 0)
                            {
                                FS.HISFC.Models.Fee.Outpatient.FeeItemList diagFeeItem = null;

                                //补挂号的，此处需要更新挂号信息
                                string regLevlCode = string.Empty;
                                string regDiagItemCode = string.Empty;
                                FS.HISFC.Models.Fee.Item.Undrug regDiagItem = new FS.HISFC.Models.Fee.Item.Undrug();
                                //急诊时间段，存在其他职级和急诊对应诊金相同时，按照急诊收取
                                if (isEmerg && emergRegItem != null)
                                {
                                    diagFeeItem = this.SetSupplyFeeItemListByItem(emergRegItem, ref errInfo);
                                    if (diagFeeItem == null)
                                    {
                                        return -1;
                                    }

                                    // regLevlCode = this.emergRegLevl;
                                }
                                else
                                {
                                    //{A1BAF267-6053-44e3-B96E-36E2C48DE4BD}
                                    string tempRegLevl = this.Patient.DoctorInfo.Templet.RegLevel.ID;
                                    //{61CC27CE-6E87-4412-8CCF-051A3862DDBD}
                                    //if (CacheManager.RegInterMgr.GetSupplyRegInfo(GetReciptDept().ID, oper.Level.ID.ToString(), currentPatientInfo.DoctorInfo.Templet.RegLevel.ID, ref regDiagItemCode) == -1)
                                    if (CacheManager.RegInterMgr.GetSupplyRegInfo(oper.ID, oper.Level.ID.ToString(), this.GetReciptDept().ID, ref tempRegLevl, ref regDiagItemCode) == -1)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow(CacheManager.RegInterMgr.Err);
                                        return -1;
                                    }
                                    if (this.CheckItem(regDiagItemCode, ref errInfo, ref regDiagItem) == -1)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                                        return -1;
                                    }
                                    diagFeeItem = this.SetSupplyFeeItemListByItem(regDiagItem, ref errInfo);
                                    if (diagFeeItem == null)
                                    {
                                        return -1;
                                    }

                                    //regLevlCode = this.regLevl_DoctLevl;regDiagItem.
                                }

                                #region 修改患者挂号级别信息
                                //if (regLevlCode != Patient.DoctorInfo.Templet.RegLevel.ID)
                                //{
                                //    FS.HISFC.Models.Registration.RegLevel regLevlObj = CacheManager.RegInterMgr.QueryRegLevelByCode(regLevlCode);
                                //    if (regLevlObj == null)
                                //    {
                                //        ucOutPatientItemSelect1.MessageBoxShow("查询挂号级别错误，编码[" + regLevlCode + "]！请联系信息科重新维护" + CacheManager.RegInterMgr.Err);
                                //        return -1;
                                //    }
                                //    Patient.DoctorInfo.Templet.RegLevel = regLevlObj;
                                //}
                                #endregion

                                if (diagFeeItem == null)
                                {
                                    return -1;
                                }
                                //对于补收的费用做个标记
                                //diagFeeItem.UndrugComb.ID = this.oper.ID;
                                diagFeeItem.UndrugComb.Name = "挂号费";
                                alSupplyFee.Add(diagFeeItem);
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                Patient.DoctorInfo.Templet.RegLevel.IsEmergency = isEmergency;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("补收挂号费失败！\r\n\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return 1;
        }

        /// <summary>
        /// 检查项目维护是否正确
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="err"></param>
        /// <param name="itemObj"></param>
        /// <returns></returns>
        private int CheckItem(string itemCode, ref string err, ref FS.HISFC.Models.Fee.Item.Undrug itemObj)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                err = "维护错误，请联系信息科！";
                return -1;
            }

            itemObj = SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemCode);
            if (itemObj == null)
            {
                err = "查找项目失败！" + CacheManager.FeeIntegrate.Err;
                return -1;
            }
            else if (string.IsNullOrEmpty(itemObj.ID))
            {
                err = "没有维护，请联系信息科！";
            }
            else if (!itemObj.IsValid)
            {
                err = "已经过期，请联系信息科！";
                return -1;
            }
            decimal orgPrice = 0;
            if (itemObj.ItemType == EnumItemType.Drug)
            {
                itemObj.Price = CacheManager.FeeIntegrate.GetPrice(itemObj.ID, this.currentPatientInfo, 0, itemObj.Price, itemObj.Price, itemObj.Price, 0, ref orgPrice);
            }
            else
            {
                itemObj.Price = CacheManager.FeeIntegrate.GetPrice(itemObj.ID, this.currentPatientInfo, 0, itemObj.Price, itemObj.ChildPrice, itemObj.SpecialPrice, 0, ref orgPrice);
            }
            return 1;
        }

        /// <summary>
        /// 设置补挂号的费用明细信息
        /// {FB95CE54-97CE-467a-865F-4B0A6FD01BB3}
        /// </summary>
        /// <param name="item"></param>
        private FS.HISFC.Models.Fee.Outpatient.FeeItemList SetSupplyFeeItemListByItem(FS.HISFC.Models.Fee.Item.Undrug item, ref string errInfo)
        {
            try
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

                if (item.Qty == 0)
                {
                    item.Qty = 1;
                }
                feeitemlist.Item = item;
                feeitemlist.Item.Qty = item.Qty;
                feeitemlist.Item.PackQty = 1;
                feeitemlist.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                feeitemlist.Patient.ID = Patient.ID;//门诊流水号
                feeitemlist.Patient.PID.CardNO = Patient.PID.CardNO;//门诊卡号 
                feeitemlist.Order.ID = Classes.Function.GetNewOrderID(ref errInfo);

                feeitemlist.ChargeOper.ID = this.GetReciptDoct().ID;
                feeitemlist.Order.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();

                feeitemlist.ExecOper.Dept = this.GetReciptDept();

                feeitemlist.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.Item.Qty * feeitemlist.Item.Price, 2);
                feeitemlist.FT.TotCost = feeitemlist.FT.OwnCost;
                feeitemlist.FeePack = "1";

                feeitemlist.Days = 1;//草药付数
                feeitemlist.RecipeOper.Dept = this.GetReciptDept();//开方科室信息
                feeitemlist.RecipeOper.Name = this.GetReciptDoct().Name;//开方医生信息
                feeitemlist.RecipeOper.ID = this.GetReciptDoct().ID;

                feeitemlist.Order.Item.ItemType = item.ItemType;//是否药品
                feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//划价状态

                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = feeitemlist.RecipeOper.Dept;//登记科室
                feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型

                //收费序列号，有费用业务层统一生成
                //feeitemlist.RecipeSequence = CacheManager.FeeIntegrate.GetRecipeSequence();
                feeitemlist.FTSource = "0";
                //feeitemlist.SeeNo = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();

                return feeitemlist;
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 判断补收的费用是否正确，是否重复
        /// </summary>
        /// <returns></returns>
        private int CheckChargedRegFeeIsRight(ArrayList alCharge, ref string errInfo)
        {
            if (alCharge == null || alCharge.Count <= 0)
            {
                return 1;
            }
            try
            {
                //挂号费的数量
                int regFeeCount = 0;

                //所有补收费的数量
                int totCount = 0;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemObj in alCharge)
                {
                    if (itemObj.UndrugComb.Name == "挂号费")
                    {
                        if (regFeeItem != null && regFeeItem.Item != null && itemObj.Item.ID == regFeeItem.Item.ID)
                        {
                            regFeeCount += 1;
                        }
                        totCount += 1;
                    }
                }

                if (regFeeCount > 1)
                {
                    errInfo = "补收挂号费错误：挂号费重复！请联系信息科！";
                    return -1;
                }
                if (totCount > 3)
                {
                    errInfo = "补收挂号费错误：补收费用重复！请联系信息科！";
                    return -1;
                }

                return 1;
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region 取消看诊

        /// <summary>
        /// 取消看诊
        /// </summary>
        /// <returns></returns>
        public int CanCelDiag()
        {
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                errInfo = "您没有选择患者！";
                return -1;
            }

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            if (!Patient.IsSee)
            {
                ucOutPatientItemSelect1.MessageBoxShow("取消看诊失败:该患者还未看诊！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            if (oper == null)
            {
                oper = CacheManager.InterMgr.GetEmployeeInfo(this.GetReciptDoct().ID);
            }

            if (!string.IsNullOrEmpty(Patient.SeeDoct.ID) &&
                !string.IsNullOrEmpty(oper.ID) &&
                Patient.SeeDoct.ID != oper.ID)
            {
                ucOutPatientItemSelect1.MessageBoxShow("取消看诊失败:您不是该患者的初诊医生！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            #region 判断是否已经有收费

            string sqlStr = @" select count(*) from fin_opb_feedetail f
                             where f.clinic_code='{0}'
                             and f.pay_flag='1'";

            sqlStr = string.Format(sqlStr, this.Patient.ID);

            string count = CacheManager.OutOrderMgr.ExecSqlReturnOne(sqlStr, "0");
            if (FS.FrameWork.Function.NConvert.ToInt32(count) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("取消看诊失败：查询费用信息出错！\r\n" + CacheManager.OutOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else if (FS.FrameWork.Function.NConvert.ToInt32(count) > 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("取消看诊失败:该患者已经收费！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            sqlStr = @" select count(*) from fin_opb_feedetail f
                             where f.clinic_code='{0}'
                             and f.pay_flag='0'
                             and cost_source='1'";

            sqlStr = string.Format(sqlStr, this.Patient.ID);

            count = CacheManager.OutOrderMgr.ExecSqlReturnOne(sqlStr, "0");
            if (FS.FrameWork.Function.NConvert.ToInt32(count) > 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("患者存在未收费医嘱，请删除后再取消看诊！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            #region 更新看诊状态

            sqlStr = @" UPDATE fin_opr_register
                       set see_dpcd = null,
                           see_docd =  null,
                           see_date = null,
                           ynsee = '0'
                     WHERE clinic_code = '{0}'
                       AND trans_type = '1'
                       AND valid_flag = '1'";

            int rev = CacheManager.OutOrderMgr.ExecNoQuery(sqlStr, Patient.ID);
            if (rev == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow("取消看诊失败：查询费用信息出错！\r\n" + CacheManager.OutOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else if (rev == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow("取消看诊失败：可能是该患者已经退号！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            #endregion

            #region 更新分诊

            if (isUseNurseArray
                && SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(CacheManager.LogEmpl.Dept.ID) != null)
            {
                //1、查找分诊队列
                FS.HISFC.Models.Nurse.Assign info = CacheManager.InterMgr.QueryAssignByClinicID(Patient.ID);

                if (info == null)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //ucOutPatientItemSelect1.MessageBoxShow(managerAssign.Err);
                    //return -1;
                    //没有队列就不处理了，有些是非分诊科室
                    return 1;
                }
                //2、增加队列中count
                //其实这快应该有问题的，如果患者是进诊状态就不需要减一了
                //暂时后面处理修改患者状态为已进诊状态
                //if (this.UpdateQueue(info.Queue.ID, "1") == -1)
                //{
                //    this.ErrCode = "更新诊台就诊患者数量出错";
                //    return -1;
                //}

                //3、取消分诊表中诊出状态，注意修改为进诊状态了
                //如果患者不想在这里看了，找护士取消进诊、取消分诊吧
                sqlStr = @"UPDATE met_nuo_assignrecord
                            SET out_date = null,
                               --doct_code = null,
                               assign_flag = '1'
                            WHERE clinic_code = '{0}'";

                rev = CacheManager.OutOrderMgr.ExecNoQuery(sqlStr, Patient.ID);
                if (rev == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("取消看诊失败：更新分诊表信息失败！\r\n" + CacheManager.OutOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else if (rev == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("取消看诊失败：可能是该患者已经取消分诊！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            //{AFA934B4-BA14-47c7-B228-BA8E48D60767}
            if (this.INurseAssign != null)
            {
                int rInt = this.INurseAssign.ReCall(this.currentPatientInfo.ID);
            }

            return 1;
        }

        #endregion

        #region 诊出

        /// <summary>
        /// 诊出
        /// </summary>
        /// <returns></returns>
        public int DiagOut()
        {
            Classes.LogManager.Write(currentPatientInfo.Name + "开始诊出操作！");
            string errInfo = "";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InterMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            CacheManager.RegInterMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.DiagOut(ref errInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            //更新患者状态为已诊后，更改基本信息中患者看诊状态
            this.Patient.IsSee = true;

            //{AFA934B4-BA14-47c7-B228-BA8E48D60767}
            if (this.INurseAssign != null)
            {
                int rInt = this.INurseAssign.DiagOut(this.currentPatientInfo, null, null, null, null, null, ref errInfo);
            }

            #region 账户扣取挂号费

            if (this.isAccountMode)
            {
                //正常挂号患者都补收费用
                if (this.Patient.PVisit.InState.ID.ToString() == "N")
                {
                    if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
                    {
                        FS.HISFC.BizProcess.Integrate.AccountFee.OutPatientFeeManage accountManager = new FS.HISFC.BizProcess.Integrate.AccountFee.OutPatientFeeManage();
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        int iRes = accountManager.ChargeFee(this.Patient, alSupplyFee, out errInfo);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n 可能原因是：账户余额不足，请患者到收费处充值后缴费！");
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.Commit();
                            //ucOutPatientItemSelect1.MessageBoxShow("扣取挂号费和诊金成功！");
                            this.Patient.IsFee = true;
                        }

                    }
                }
            }
            #endregion

            Classes.LogManager.Write(currentPatientInfo.Name + "结束诊出操作！");

            this.SetOrderFeeDisplay(false, true);

            return 1;
        }


        /// <summary>
        /// 诊出
        /// </summary>
        /// <returns></returns>
        public int DiagOut(ref string errInfo)
        {
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                errInfo = "您没有选择患者！";
                return -1;
            }

            if (CheckDiag(2) == -1)
            {
                return -1;
            }

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            Employee empl = CacheManager.OutOrderMgr.Operator as Employee;

            int iReturn = -1;
            DateTime now = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = CacheManager.OutOrderMgr.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//获得新的看诊序号
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    errInfo = "获取看诊序号失败：" + CacheManager.OutOrderMgr.Err;
                    return -1;
                }

            }

            #region 补挂号费

            ArrayList alFeeItem = new ArrayList();

            #region 插入挂号记录

            if (this.AddRegInfo(Patient) == -1)
            {
                return -1;
            }

            if (this.SetSupplyRegFee(ref this.alSupplyFee, ref errInfo, this.Patient.IsFee) == -1)
            {
                this.alSupplyFee = new ArrayList();
                errInfo = "补收挂号费失败：" + errInfo;
                return -1;
            }

            if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj in alSupplyFee)
                {
                    alFeeItem.Add(feeItemObj);
                }

                //alFeeItem.AddRange(this.alSupplyFee);
            }

            //收完挂号费后，更新挂号表已收费状态，避免多次补收挂号费
            if (CacheManager.RegInterMgr.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                errInfo = CacheManager.ConManager.Err;
                return -1;
            }
            //}

            #endregion

            #region 收费

            //处方号和流水号规则由费用业务层函数统一生成
            try
            {
                bool rev = false;
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                bool isAccount = false;
                if (this.isAccountMode)
                {
                    #region 账户判断
                    if (isAccountTerimal)
                    {
                        decimal vacancy = 0m;
                        if (this.Patient.IsAccount)
                        {

                            if (CacheManager.FeeIntegrate.GetAccountVacancy(this.Patient.PID.CardNO, ref vacancy) <= 0)
                            {
                                errInfo = CacheManager.FeeIntegrate.Err;
                                return -1;
                            }
                            isAccount = true;
                        }
                    }
                    #endregion
                }
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                if (isAccountTerimal && isAccount)
                {
                    rev = CacheManager.FeeIntegrate.SetChargeInfoToAccount(this.Patient, alFeeItem, now, ref errInfo);
                    if (rev == false)
                    {
                        return -1;
                    }
                }
                else
                {
                    //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
                    //直接收费 1成功 -1失败 0普通患者不处理走正常划价
                    if (IDoctFee != null)
                    {
                        int resultValue = IDoctFee.DoctIdirectFee(this.Patient, alFeeItem, now, ref errInfo);
                        if (resultValue == -1)
                        {
                            return -1;
                        }
                        if (resultValue == 0)
                        {
                            rev = CacheManager.FeeIntegrate.SetChargeInfo(this.Patient, alFeeItem, now, ref errInfo);
                            if (rev == false)
                            {
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        rev = CacheManager.FeeIntegrate.SetChargeInfo(this.Patient, alFeeItem, now, ref errInfo);
                        if (rev == false)
                        {
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return -1;
            }
            #endregion

            #endregion

            #region 更新分诊
            if (isUseNurseArray
                && currentPatientInfo.IsTriage
                && SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(empl.Dept.ID) != null)//不管是否已经看诊，都更新分诊表 by lijp 2012-06-30 && !this.Patient.IsSee)
            {
                if (this.currentRoom != null)
                {
                    iReturn = CacheManager.InterMgr.UpdateAssign(this.currentRoom.ID, this.Patient.ID, now, empl.ID);
                    if (iReturn < 0)
                    {
                        errInfo = "更新分诊标志出错！\r\n" + CacheManager.InterMgr.Err;

                        return -1;
                    }
                }
            }
            #endregion

            #region 更新看诊

            //按照首诊医生更新患者的看诊医生
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                iReturn = CacheManager.RegInterMgr.UpdateSeeDone(this.Patient.ID);
                if (iReturn < 0)
                {
                    errInfo = "更新看诊标志出错！";

                    return -1;
                }

                iReturn = CacheManager.RegInterMgr.UpdateDept(this.Patient.ID, empl.Dept.ID, empl.ID);
                if (iReturn < 0)
                {
                    errInfo = "更新看诊科室、医生出错！";

                    return -1;
                }
            }
            #endregion

            return 1;
        }

        #endregion

        #region 进诊
        //{82FC5ABE-B85B-4011-AE0F-8042A89CD327}
        //public int DiagIn()
        //{
        //    DateTime now = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
        //    Employee empl = CacheManager.OutOrderMgr.Operator as Employee;

        //    FS.HISFC.Models.Nurse.Assign assignPatient = null;
        //    assignPatient = CacheManager.InterMgr.QueryAssignByClinicID(this.Patient.ID);

        //    if (assignPatient.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
        //    {
        //        return 1;
        //    }

        //    if (this.Patient.IsSee)
        //    {
        //        return 1;
        //    }

        //    if (isUseNurseArray
        //                   && currentPatientInfo.IsTriage
        //                   && SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(empl.Dept.ID) != null)
        //    {
        //        if (this.currentRoom != null)
        //        {
        //            object[] args = {this.Patient.ID, this.currentRoom.ID,this.currentRoom.Name, empl.ID,
        //                                    CacheManager.AssignMgr.GetDateTimeFromSysDateTime() , (int)FS.HISFC.Models.Nurse.EnuTriageStatus.In};
        //            if (CacheManager.AssignMgr.UpdateAssignAfterCall(args) == -1)
        //            {
        //                errInfo = "更新分诊标志出错！\r\n" + CacheManager.InterMgr.Err;

        //                return -1;

        //            }
        //        }
        //    }

        //    return 1;
        //}
        #endregion

        /// <summary>
        /// 是否允许继续开立
        /// </summary>
        /// <returns></returns>
        /* public bool CheckCanAdd()
         {
             try
             {
                 #region 检查患者最新信息

                 #endregion

                 #region 检查是否收费处方
                 string strSQL = @"select count(*)
                                     from met_ord_recipedetail m
                                     where m.clinic_code='{0}'
                                     and m.status!='0'
                                     and m.see_no='{1}'";
                 strSQL = string.Format(strSQL, currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                 string rev = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSQL, "0");
                 if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                 {
                     ucOutPatientItemSelect1.MessageBoxShow(this, "该处方已经收费，请新增处方开立！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     this.RefreshOrderState();
                     return false;
                 }
                 #endregion
             }
             catch (Exception ex)
             {
                 //出异常了，还允许继续开立
                 ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                 return true;
             }

             return true;
         }*/

        /// <summary>
        /// {BD313419-E718-4ABC-A9B9-5DF05709E309} 部分未收费的项目可以删除
        /// 是否允许继续开立2
        /// </summary>
        /// <returns></returns>
        public bool CheckCanAdd()
        {
            try
            {

                #region 检查是否有未收费项目
                string strSQL = @"select m.sequence_no  
                                from met_ord_recipedetail m 
                                where m.clinic_code='{0}' 
                                and m.see_no='{1}' 
                                and m.status='0'";

                strSQL = string.Format(strSQL, currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                string rev = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSQL, "0");
                if (rev == null || rev == "")
                {
                    ucOutPatientItemSelect1.MessageBoxShow(this, "该处方已经收费，请新增处方开立！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.RefreshOrderState();
                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                //出异常了，还允许继续开立
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return true;
            }

            return true;
        }



        /// <summary>
        /// 补录挂号信息
        /// </summary>
        /// <param name="regInfo"></param>
        /// <returns></returns>
        private int AddRegInfo(FS.HISFC.Models.Registration.Register regInfo)
        {
            //根据挂号表里isfee标记，判断是不是系统补挂号的记录
            FS.HISFC.Models.Registration.Register regTemp = CacheManager.RegInterMgr.GetByClinic(regInfo.ID);
            if (regTemp == null || string.IsNullOrEmpty(regTemp.ID))
            {
                //补挂号
                if (CacheManager.RegInterMgr.Insert(regInfo) == -1)
                {
                    errInfo = "将补挂号信息插入挂号表出错" + CacheManager.RegInterMgr.Err;
                    return -1;
                }

                //更新体征信息
                if (CacheManager.OutOrderMgr.UpdateHealthInfo(regInfo.Height, regInfo.Weight, regInfo.SBP, regInfo.DBP, regInfo.ID, regInfo.Temperature, regInfo.BloodGlu) == -1)
                {
                    errInfo = "更新患者体征信息错误：" + CacheManager.OutOrderMgr.Err;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public int Retrieve()
        {
            // TODO:  添加 ucOrder.Retrieve 实现
            this.QueryOrder();
            return 0;
        }

        /// <summary>
        /// 草药
        /// </summary>
        /// <returns></returns>
        public int HerbalOrder()
        {
            FS.HISFC.Models.Order.OutPatient.Order ord;
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex >= 0
                && this.neuSpread1.ActiveSheet.Rows.Count > 0
               && neuSpread1.ActiveSheet.IsSelected(neuSpread1.ActiveSheet.ActiveRowIndex, 0))
            {
                ord = this.neuSpread1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.OutPatient.Order;

                if (ord != null && ord.Item.SysClass.ID.ToString() == "PCC" && ord.Status == 0)
                {
                    this.ModifyHerbal();
                }
                else
                {
                    if (ucHerbal == null)
                    {
                        ucHerbal = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID);
                    }
                    //using (FS.HISFC.Components.Order.Controls.ucHerbalOrder uc = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
                    //{
                    ucHerbal.refreshGroup += new FS.HISFC.Components.Order.Controls.RefreshGroupTree(RefreshGroup);
                    ucHerbal.Patient = new FS.HISFC.Models.RADT.PatientInfo();//
                    ucHerbal.IsClinic = true;
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                    ucHerbal.SetFocus();

                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucHerbal);

                    AddNewHerbalOrder();
                    //}
                }
            }
            else
            {
                if (ucHerbal == null)
                {
                    ucHerbal = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID);
                }

                //using (FS.HISFC.Components.Order.Controls.ucHerbalOrder uc = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
                //{
                ucHerbal.refreshGroup += new FS.HISFC.Components.Order.Controls.RefreshGroupTree(RefreshGroup);
                ucHerbal.Patient = new FS.HISFC.Models.RADT.PatientInfo();//
                ucHerbal.IsClinic = true;
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                ucHerbal.SetFocus();

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucHerbal);

                AddNewHerbalOrder();
                //}
            }
            return 1;
        }

        /// <summary>
        /// 刷新组套列表
        /// </summary>
        void RefreshGroup()
        {
            OnRefreshGroupTree(null, null);
        }

        #endregion

        #region 菜单

        int ActiveRowIndex = -1;

        /// <summary>
        /// 添加右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            this.contextMenu1.Items.Clear();
            FS.HISFC.Models.Order.OutPatient.Order mnuSelectedOrder = null;
            FarPoint.Win.Spread.Model.CellRange c = neuSpread1.GetCellFromPixel(0, 0, e.X, e.Y);

            #region 左键菜单

            //左键用于选择同组项目
            if (IsDesignMode || EditGroup)
            {
                if (c.Row > 0)
                {
                    string combNo = "";
                    FS.HISFC.Models.Order.OutPatient.Order orderObj = null;
                    for (int i = this.neuSpread1.ActiveSheet.RowCount - 1; i >= 0; i--)
                    {
                        orderObj = this.GetObjectFromFarPoint(i, neuSpread1.ActiveSheetIndex);
                        if (orderObj != null)
                        {
                            if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                            {
                                combNo = "|" + orderObj.Combo.ID + "|";
                            }
                            else
                            {
                                if (combNo.Contains("|" + orderObj.Combo.ID + "|"))
                                {
                                    this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, 1);
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region 右键菜单
            if (this.bIsShowPopMenu && e.Button == MouseButtons.Right)
            {
                this.contextMenu1.Items.Clear();
                if (c.Row >= 0)
                {
                    //先记录目前的勾选状态，后面加进来
                    Hashtable hsSelected = new Hashtable();
                    for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                    {
                        if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                        {
                            if (!hsSelected.Contains(i))
                            {
                                hsSelected.Add(i, null);
                            }
                        }
                    }

                    //this.neuSpread1.ActiveSheet.AddSelection(c.Row, 0, 1, 1);
                    ActiveRowIndex = c.Row;
                    this.neuSpread1.ActiveSheet.ActiveRowIndex = c.Row;
                    this.SelectionChanged();

                    foreach (int row in hsSelected.Keys)
                    {
                        neuSpread1.ActiveSheet.AddSelection(row, 0, 1, 1);
                    }
                }
                else
                {
                    ActiveRowIndex = -1;
                }

                if (ActiveRowIndex < 0)
                {
                    if (this.bIsDesignMode)
                    {
                        #region 粘贴医嘱
                        //if (FS.HISFC.Components.Order.Classes.HistoryOrderClipboard.IsHaveCopyData)
                        //{
                        ToolStripMenuItem mnuPasteOrder = new ToolStripMenuItem("粘贴医嘱");
                        mnuPasteOrder.Click += new EventHandler(mnuPasteOrder_Click);
                        this.contextMenu1.Items.Add(mnuPasteOrder);
                        this.contextMenu1.Show(this.neuSpread1, new Point(e.X, e.Y));
                        //}
                        #endregion
                    }
                    return;
                }

                mnuSelectedOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, 0);
                if (this.bIsDesignMode)
                {
                    #region 院注次数
                    if (mnuSelectedOrder.Item.ItemType == EnumItemType.Drug &&
                      (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 4) &&
                      mnuSelectedOrder.InjectCount == 0 &&
                        //Classes.Function.hsUsageAndSub.Contains(mnuSelectedOrder.Usage.ID)
                        Classes.Function.CheckIsInjectUsage(mnuSelectedOrder.Usage.ID)
                        )
                    {
                        ToolStripMenuItem mnuInjectNum = new ToolStripMenuItem();//院注次数
                        mnuInjectNum.Click += new EventHandler(mnumnuInjectNum_Click);

                        mnuInjectNum.Text = "添加院注次数[快捷键:F5]";
                        this.contextMenu1.Items.Add(mnuInjectNum);
                    }

                    if (mnuSelectedOrder.Item.ItemType == EnumItemType.Drug &&
                        (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 4) &&
                        mnuSelectedOrder.InjectCount > 0)
                    {
                        ToolStripMenuItem mnuInjectNum = new ToolStripMenuItem();//院注次数
                        mnuInjectNum.Click += new EventHandler(mnumnuInjectNum_Click);

                        mnuInjectNum.Text = "修改院注次数[快捷键:F5]";
                        this.contextMenu1.Items.Add(mnuInjectNum);
                    }
                    #endregion

                    #region 停止菜单
                    if (mnuSelectedOrder.Status == 0)
                    { //开立
                        ToolStripMenuItem mnuDel = new ToolStripMenuItem();//停止
                        mnuDel.Click += new EventHandler(mnuDel_Click);
                        //mnuDel.Text = "删除医嘱[" + mnuSelectedOrder.Item.Name + "]";
                        mnuDel.Text = "删除所选择的项目";
                        this.contextMenu1.Items.Add(mnuDel);//删除、作废
                    }
                    #region 作废医嘱{DFA920BD-AEB2-4371-B501-21CB87558147}
                    else if (mnuSelectedOrder.Status == 1)
                    {
                        ToolStripMenuItem mnuCancel = new ToolStripMenuItem();//停止
                        mnuCancel.Click += new EventHandler(mnuCancel_Click);
                        //mnuCancel.Text = "作废医嘱[" + mnuSelectedOrder.Item.Name + "]";
                        mnuCancel.Text = "作废所选择的项目";
                        this.contextMenu1.Items.Add(mnuCancel);//删除、作废																							
                    }
                    #endregion
                    #endregion

                    #region 复制医嘱

                    ToolStripMenuItem mnuCopyAs = new ToolStripMenuItem();//复制医嘱为本类型
                    mnuCopyAs.Click += new EventHandler(mnuCopyAs_Click);

                    //mnuCopyAs.Text = "复制" + "[" + mnuSelectedOrder.Item.Name + "]";
                    mnuCopyAs.Text = "复制所选择的项目";

                    this.contextMenu1.Items.Add(mnuCopyAs);
                    #endregion

                    #region 上移
                    ToolStripMenuItem mnuUp = new ToolStripMenuItem("上移动");//上移动
                    mnuUp.Click += new EventHandler(mnuUp_Click);
                    if (this.neuSpread1.ActiveSheet.ActiveRowIndex <= 0) mnuUp.Enabled = false;
                    this.contextMenu1.Items.Add(mnuUp);
                    #endregion

                    #region 下移
                    ToolStripMenuItem mnuDown = new ToolStripMenuItem("下移动");//下移动
                    mnuDown.Click += new EventHandler(mnuDown_Click);
                    if (this.neuSpread1.ActiveSheet.ActiveRowIndex >= this.neuSpread1.ActiveSheet.RowCount - 1 ||
                        this.neuSpread1.ActiveSheet.ActiveRowIndex < 0)
                    {
                        mnuDown.Enabled = false;
                    }
                    this.contextMenu1.Items.Add(mnuDown);
                    #endregion

                    #region 修改价格
                    if (mnuSelectedOrder.Status == 0
                        && mnuSelectedOrder.Item.Price == 0)
                    {
                        ToolStripMenuItem mnuChangePrice = new ToolStripMenuItem("修改价格");
                        mnuChangePrice.Click += new EventHandler(mnuChangePrice_Click);
                        this.contextMenu1.Items.Add(mnuChangePrice);
                    }
                    #endregion

                    #region 医嘱接瓶
                    ////if (mnuSelectedOrder.Status == 0 && mnuSelectedOrder.Item.IsPharmacy)
                    ////{
                    ////    ToolStripMenuItem mnuResumeOrder = new ToolStripMenuItem("医嘱接瓶[快捷键:F6]");
                    ////    mnuResumeOrder.Click += new EventHandler(mnuResumeOrder_Click);
                    ////    this.contextMenu1.Items.Add(mnuResumeOrder);
                    ////}
                    #endregion

                    #region 数量加倍
                    ////if (mnuSelectedOrder.Status == 0 && this.JudgeIsPCZ())
                    ////{
                    ////    ToolStripMenuItem mnuChangeQTY = new ToolStripMenuItem("数量加倍[快捷键:F7]");
                    ////    ////mnuChangeQTY.Click += new EventHandler(mnuChangeQTY_Click);
                    ////    this.contextMenu1.Items.Add(mnuChangeQTY);
                    ////}
                    #endregion

                    #region 取消组合

                    ToolStripMenuItem mnuCancelCombo = new ToolStripMenuItem("取消组合");//上移动
                    mnuCancelCombo.Click += new EventHandler(mnuCancelCombo_Click);
                    if (this.neuSpread1.ActiveSheet.SelectionCount < 0)
                    {
                        mnuCancelCombo.Enabled = false;
                    }
                    this.contextMenu1.Items.Add(mnuCancelCombo);

                    #endregion

                    #region 存组套

                    ToolStripMenuItem mnuSaveGroup = new ToolStripMenuItem("存组套");//存组套
                    mnuSaveGroup.Click += new EventHandler(mnuSaveGroup_Click);

                    this.contextMenu1.Items.Add(mnuSaveGroup);
                    #endregion

                    #region 添加合理用药右键菜单

                    if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
                    {
                        int i = 0;
                        ToolStripMenuItem menuPass = new ToolStripMenuItem("合理用药");

                        ArrayList alMenu = new ArrayList();

                        if (IReasonableMedicine.PassShowOtherInfo(null, null, ref alMenu) == -1)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(IReasonableMedicine.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            ToolStripMenuItem m_passItem = null;
                            ToolStripMenuItem m_passItemSecond = null;
                            ToolStripSeparator tSep = null;

                            int j = 0;
                            if (alMenu != null && alMenu.Count > 0)
                            {
                                this.contextMenu1.Items.Add(menuPass);
                            }

                            foreach (TreeNode node in alMenu)
                            {
                                tSep = new ToolStripSeparator();
                                if (string.IsNullOrEmpty(node.Text))
                                {
                                    //menuPass.DropDownItems.Insert(i, tSep);
                                    menuPass.DropDownItems.Add(tSep);
                                }
                                else
                                {
                                    m_passItem = new ToolStripMenuItem(node.Text);
                                    m_passItem.Click += new EventHandler(mnuPass_Click);
                                    if (node.Tag != null && node.Tag.ToString() == "不可用")
                                    {
                                        m_passItem.Enabled = false;
                                    }
                                    //menuPass.DropDownItems.Insert(i, m_passItem);
                                    menuPass.DropDownItems.Add(m_passItem);

                                    if (node.Tag == null)
                                    {
                                        foreach (TreeNode secondNode in node.Nodes)
                                        {
                                            tSep = new ToolStripSeparator();
                                            if (string.IsNullOrEmpty(secondNode.Text))
                                            {
                                                //m_passItem.DropDownItems.Insert(0, tSep);
                                                m_passItem.DropDownItems.Add(tSep);
                                            }
                                            else
                                            {
                                                m_passItemSecond = new ToolStripMenuItem(secondNode.Text);
                                                m_passItemSecond.Click += new EventHandler(mnuPass_Click);
                                                if (secondNode.Tag != null && secondNode.Tag.ToString() == "不可用")
                                                {
                                                    m_passItemSecond.Enabled = false;
                                                }
                                                //m_passItem.DropDownItems.Insert(j, m_passItemSecond);
                                                m_passItem.DropDownItems.Add(m_passItemSecond);
                                            }
                                            j += 1;
                                        }
                                    }
                                }
                                i += 1;
                            }
                        }
                    }
                    #endregion

                    #region 修改扩展信息
                    if (string.IsNullOrEmpty(this.Patient.Pact.PactDllName))
                    {
                        this.Patient.Pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(this.Patient.Pact.ID);
                    }

                    // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                    //佛山需要根据维护的合同单位判断
                    FS.FrameWork.Models.NeuObject objItem = CacheManager.ConManager.GetConstant("PactDllName", this.Patient.Pact.PactDllName);
                    if (this.Patient != null
                        && (this.Patient.Pact.PayKind.ID == "02"
                          || (objItem != null && !string.IsNullOrEmpty(objItem.ID)))
                        && indicationsHelper.GetObjectFromID(GetItemUserCode(mnuSelectedOrder.Item)) != null)
                    {
                        ToolStripMenuItem mnuEditIndications = new ToolStripMenuItem("修改医保限制性用药信息");//修改医保限制性用药信息

                        mnuEditIndications.Click += new EventHandler(mnuEditIndications_Click);
                        this.contextMenu1.Items.Add(mnuEditIndications);
                    }

                    #endregion
                }
                else
                {
                    #region 复制医嘱
                    ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem("复制医嘱");
                    mnuCopyOrder.Click += new EventHandler(mnuCopyOrder_Click);
                    this.contextMenu1.Items.Add(mnuCopyOrder);
                    #endregion
                }

                this.contextMenu1.Show(this.neuSpread1, new Point(e.X, e.Y));

                FS.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(c.Row, this.neuSpread1.ActiveSheetIndex);
                if (temp != null)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderDelete = null;
                    for (int k = 0; k < this.neuSpread1.ActiveSheet.Rows.Count; k++)
                    {
                        orderDelete = this.GetObjectFromFarPoint(k, this.neuSpread1.ActiveSheetIndex);
                        if (temp.Combo.ID == orderDelete.Combo.ID)
                        {
                            this.neuSpread1.ActiveSheet.AddSelection(k, 0, 1, 1);
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 右键取消组合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuCancelCombo_Click(object sender, EventArgs e)
        {
            this.CancelCombo();
        }

        /// <summary>
        /// 删除，作废、停止医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDel_Click(object sender, EventArgs e)
        {
            this.Del();
        }

        #region 作废医嘱（收费后医嘱不允许作废，遇到特殊需求再打开

        /// <summary>
        /// 作废医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(this.neuSpread1_Sheet1.ActiveRowIndex, 0);

            if (order == null)
            {
                return;
            }

            if (order.Status != 1)
            {
                return;
            }

            DialogResult r = ucOutPatientItemSelect1.MessageBoxShow("是否确定要作废该条医嘱,此操作不能撤销！", "警示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (r == DialogResult.Cancel)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            CacheManager.OutOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                if (temp == null)
                    continue;

                if (temp.Combo.ID == order.Combo.ID)
                {
                    if (CacheManager.OutOrderMgr.UpdateOrderBeCaceled(temp) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        ucOutPatientItemSelect1.MessageBoxShow("作废医嘱" + temp.Item.Name + "失败");
                        return;
                    }
                    int oldState = temp.Status;
                    temp.Status = 3;
                    temp.DCOper.ID = CacheManager.OutOrderMgr.Operator.ID;
                    temp.DCOper.OperTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                    this.AddObjectToFarpoint(temp, i, 0, EnumOrderFieldList.Item);

                    if (this.isSaveOrderHistory)
                    {
                        if (CacheManager.OutOrderMgr.InsertOrderChangeInfo(temp) < 0)
                        {
                            temp.Status = oldState;
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            ucOutPatientItemSelect1.MessageBoxShow("插入医嘱" + order.Item.Name + "修改信息失败");
                            return;
                        }
                    }
                    //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
                    if (IDoctFee != null)
                    {
                        string errText = string.Empty;
                        if (IDoctFee.CancelOrder(this.Patient, temp, ref errText) < 0)
                        {
                            temp.Status = oldState;
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            ucOutPatientItemSelect1.MessageBoxShow(errText);
                            return;
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    #region 电子申请单 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504
                    string isUsePacsApply = CacheManager.FeeIntegrate.GetControlValue("200212", "0");
                    if (this.isUsePacsApply)
                    {
                        if (order.ApplyNo != null)
                        {
                            IOutPatientPacsApply.Delete(this.Patient, order);
                        }
                    }
                    #endregion
                }
            }

            this.RefreshOrderState();
        }

        #endregion

        /// <summary>
        /// 填充处方内容
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int FillNewOrder(ref FS.HISFC.Models.Order.OutPatient.Order order)
        {
            order.Patient.Pact = this.currentPatientInfo.Pact;
            order.Patient.Birthday = this.currentPatientInfo.Birthday;

            //开立科室和执行科室相同，则认为是本科室执行项目，执行科室重取
            //if (order.ReciptDept.ID == order.ExeDept.ID)
            //{
            //    order.ExeDept = new FS.FrameWork.Models.NeuObject();
            //}
            //if (string.IsNullOrEmpty(order.ExeDept.ID))
            //{
            //order.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(this.GetReciptDept(), order, order.ExeDept.ID, false);
            order.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(true, GetReciptDept().ID, order.ExeDept.ID, order.Item.ID);
            //}

            DateTime dtNow = DateTime.MinValue;

            order.Status = 0;
            order.ID = "";
            order.SortID = 0;

            order.EndTime = DateTime.MinValue;
            order.DCOper.OperTime = DateTime.MinValue;
            order.DcReason.ID = "";
            order.DcReason.Name = "";
            order.DCOper.ID = "";
            order.DCOper.Name = "";
            order.ConfirmTime = DateTime.MinValue;
            order.Nurse.ID = "";
            dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            order.MOTime = dtNow;
            if (this.GetReciptDept() != null)
            {
                order.ReciptDept.ID = this.GetReciptDept().ID;
                order.ReciptDept.Name = this.GetReciptDept().Name;
            }
            if (this.GetReciptDoct() != null)
            {
                order.ReciptDoctor.ID = this.GetReciptDoct().ID;
                order.ReciptDoctor.Name = this.GetReciptDoct().Name;
            }

            if (this.GetReciptDoct() != null)
            {
                order.Oper.ID = this.GetReciptDoct().ID;
                order.Oper.ID = this.GetReciptDoct().Name;
            }

            order.CurMOTime = order.BeginTime;
            order.NextMOTime = order.BeginTime;

            if (this.CheckOrderBase(null, order, ref errInfo) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 同界面复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyAs_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.OutPatient.Order;
            if (order == null)
            {
                return;
            }
            ArrayList alCopyList = new ArrayList();
            string ComboNo = CacheManager.OutOrderMgr.GetNewOrderComboID();

            string oldComb = "";
            string newComb = "";

            for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
            {
                //{0817AFF8-A0DC-4a06-BEAD-015BC49AE973}
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    FS.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Clone();

                    if (FillNewOrder(ref o) == -1)
                    {
                        continue;
                    }

                    if (o.Combo.ID != oldComb)
                    {
                        newComb = CacheManager.OutOrderMgr.GetNewOrderComboID();
                        oldComb = o.Combo.ID;
                        o.Combo.ID = newComb;
                    }
                    else
                    {
                        o.Combo.ID = newComb;
                    }
                    //{37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
                    if (Components.Order.Classes.Function.ReComputeQty(o) == -1)
                    {
                    }
                    alCopyList.Add(o);
                }
            }

            for (int i = 0; i < alCopyList.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order ord = alCopyList[i] as FS.HISFC.Models.Order.OutPatient.Order;

                this.AddNewOrder(ord, 0);
            }
            ////SetFeeDisplay(this.Patient, null);

            RefreshOrderState();
            this.RefreshCombo();
        }

        /// <summary>
        /// 组套项目选择增加
        /// </summary>
        public void AddGroupOrder(ArrayList alOrders)
        {
            ArrayList alHerbal = new ArrayList(); //草药

            ArrayList alAddOrder = new ArrayList();
            FS.HISFC.BizLogic.Manager.UndrugztManager ztManager = new FS.HISFC.BizLogic.Manager.UndrugztManager();

            frmChoseSublItem frmChose = new frmChoseSublItem();
            for (int i = 0; i < alOrders.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = alOrders[i] as FS.HISFC.Models.Order.OutPatient.Order;

                #region 重复医嘱提示
                bool saveflag = true;
                if (isShowSameOrder)
                {
                    #region 提示当次就诊已开立过相同项目的医嘱
                    if (this.SameOrderList != null && this.SameOrderList.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in this.SameOrderList)
                        {
                            if (orderTemp.Item.ID == order.Item.ID)
                            {
                                if (MessageBox.Show("项目【" + orderTemp.Item.Name + "】在【" + orderTemp.MOTime.ToString() + "】已开立,是否继续开立？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                {
                                    saveflag = false;
                                }

                                break;
                            }
                        }
                    }

                    #endregion

                    #region 提示检查或检验项目是否存在已收费但未出报告的记录（历往就诊）
                    if (this.LastOrderList != null && this.LastOrderList.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in this.LastOrderList)
                        {
                            //仅针对检验项目以及超声项目
                            if (orderTemp.Item.SysClass.ID.ToString() == "UL")
                            {

                                if (orderTemp.Item.ID == order.Item.ID && orderTemp.User01 != "已出报告")
                                {
                                    if (MessageBox.Show("项目【" + orderTemp.Item.Name + "】在【" + orderTemp.MOTime.ToString() + "】已开立且为" + orderTemp.User01 + "状态 ,是否继续开立？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                    {
                                        saveflag = false;
                                    }

                                    break;
                                }

                            }
                            //超声检查不存在“已报告”状态  
                            else if ((orderTemp.Item.SysClass.ID.ToString() == "UC" && orderTemp.ExeDept.ID == "6003"))
                            {
                                if (orderTemp.Item.ID == order.Item.ID && orderTemp.User01 == "未执行")
                                {
                                    if (MessageBox.Show("项目【" + orderTemp.Item.Name + "】在【" + orderTemp.MOTime.ToString() + "】已开立，且为已过费未执行状态 ,是否继续开立？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                    {
                                        saveflag = false;
                                    }

                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }

                if (saveflag == false)
                {
                    continue;
                }

                #endregion

                if (order.Item.ID == "999")
                {
                    order.ExeDept.ID = "";
                }
                //{37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
                if (Components.Order.Classes.Function.ReComputeQty(order, true) == -1)
                {
                }
                if (this.FillNewOrder(ref order) == -1)
                {
                    continue;
                }

                if (order.Item.SysClass.ID.ToString() == "PCC") //草药
                {
                    alHerbal.Add(order);
                }
                else
                {
                    if (order.Item.ItemType == EnumItemType.UnDrug)
                    {
                        #region 检验组套
                        if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag == "1"
                            && (order.Item.SysClass.ID.ToString() == "UL")
                            && Classes.Function.IsLisSelectDetail("UL"))
                        {
                            List<FS.HISFC.Models.Fee.Item.UndrugComb> lstzt = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
                            if (ztManager.QueryUnDrugztDetail(order.Item.ID, ref lstzt) == -1)
                            {
                                MessageBox.Show(ztManager.Err);
                                return;
                            }

                            ArrayList alLisOrder = new ArrayList();
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrug in lstzt)
                            {
                                if (undrug.ValidState == "无效")
                                {
                                    continue;
                                }
                                FS.HISFC.Models.Order.OutPatient.Order orderTmp = order.Clone();
                                orderTmp.Item = undrug;
                                orderTmp.Item.Qty = undrug.Qty * order.Qty;
                                orderTmp.ApplyNo = undrug.Package.ID;
                                orderTmp.User02 = undrug.Package.Name;
                                orderTmp.Unit = undrug.PriceUnit;
                                orderTmp.HerbalQty = 1;
                                orderTmp.Sample.Name = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(((FS.HISFC.Models.Order.OutPatient.Order)alOrders[0]).Item.ID).CheckBody;//重新获取复合项目的样本赋值到子项目
                                if (this.FillNewOrder(ref orderTmp) == -1)
                                {
                                    continue;
                                }

                                alLisOrder.Add(orderTmp);
                            }
                            frmChose.AlSublOrders = alLisOrder;
                            frmChose.Text = order.Item.Name;
                            ArrayList alLis = new ArrayList();
                            if (alLisOrder.Count > 0)
                            {
                                frmChose.ShowDialog();
                                if (frmChose.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    alLis = frmChose.AlSublOrders;
                                }
                                else
                                {
                                    alLis.Clear();
                                }
                            }

                            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alLis)
                            {
                                AddNewOrder(ord, 0);
                            }
                        }
                        #endregion

                        else if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag == "1"
                            && order.Item.SysClass.ID.ToString() == "UC"
                            && Classes.Function.IsLisSelectDetail("UC"))
                        {
                            List<FS.HISFC.Models.Fee.Item.UndrugComb> lstzt = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
                            if (ztManager.QueryUnDrugztDetail(order.Item.ID, ref lstzt) == -1)
                            {
                                MessageBox.Show(ztManager.Err);
                                return;
                            }

                            ArrayList alPacsOrder = new ArrayList();
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrug in lstzt)
                            {
                                if (undrug.ValidState == "无效")
                                {
                                    continue;
                                }
                                FS.HISFC.Models.Order.OutPatient.Order orderTmp = order.Clone();
                                orderTmp.Item = undrug;
                                orderTmp.Item.Qty = undrug.Qty * order.Qty;
                                orderTmp.ApplyNo = undrug.Package.ID;
                                orderTmp.User02 = undrug.Package.Name;
                                orderTmp.Unit = undrug.PriceUnit;
                                orderTmp.HerbalQty = 1;
                                orderTmp.Combo = order.Combo;

                                if (this.FillNewOrder(ref orderTmp) == -1)
                                {
                                    continue;
                                }

                                alPacsOrder.Add(orderTmp);
                            }

                            frmChose.AlSublOrders = alPacsOrder;
                            frmChose.Text = order.Item.Name;
                            ArrayList alPacs = new ArrayList();
                            if (alPacsOrder.Count > 0)
                            {
                                frmChose.ShowDialog();
                                if (frmChose.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    alPacs = frmChose.AlSublOrders;
                                }
                                else
                                {
                                    alPacs.Clear();
                                }
                            }

                            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alPacs)
                            {
                                AddNewOrder(ord, 0);
                            }

                            //foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alPacsOrder)
                            //{
                            //    AddNewOrder(ord, 0);
                            //}
                        }
                        else
                        {
                            AddNewOrder(order, 0);
                        }
                    }
                    else
                    {
                        this.AddNewOrder(order, 0);
                    }
                }
            }
            if (alHerbal.Count > 0)
            {
                this.AddHerbalOrders(alHerbal);
            }
            this.RefreshOrderState();
            this.RefreshCombo();
        }

        /// <summary>
        /// 上移医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuUp_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex <= 0)
                return;

            FS.HISFC.Models.Order.OutPatient.Order upOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex - 1, this.neuSpread1.ActiveSheetIndex).Clone();
            FS.HISFC.Models.Order.OutPatient.Order downOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex).Clone();

            //组合内移动
            if (upOrder.Combo.ID == downOrder.Combo.ID)
            {
                upOrder.SortID += 1;
                AddObjectToFarpoint(upOrder, this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

                downOrder.SortID -= 1;
                AddObjectToFarpoint(downOrder, this.neuSpread1.ActiveSheet.ActiveRowIndex - 1, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

                this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex - 1, GetColumnIndexFromName("顺序号")].Tag = "哈哈";
            }
            else
            {
                int upNum = 0;
                int downNum = 0;
                FS.HISFC.Models.Order.OutPatient.Order oTmp = null;
                for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                {
                    oTmp = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (oTmp.Combo.ID == upOrder.Combo.ID)
                    {
                        upNum++;
                    }
                    if (oTmp.Combo.ID == downOrder.Combo.ID)
                    {
                        downNum++;
                    }
                }

                for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                {
                    oTmp = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (oTmp.Combo.ID == upOrder.Combo.ID)
                    {
                        oTmp.SortID = oTmp.SortID + downNum;
                        oTmp.SubCombNO = downOrder.SubCombNO;
                    }
                    else if (oTmp.Combo.ID == downOrder.Combo.ID)
                    {
                        oTmp.SortID = oTmp.SortID - upNum;
                        oTmp.SubCombNO = upOrder.SubCombNO;
                    }
                    this.AddObjectToFarpoint(oTmp, i, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

                    if (i == this.ActiveRowIndex)
                    {
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag = "哈哈";
                    }
                }
            }

            this.neuSpread1.Sheets[0].SortRows(GetColumnIndexFromName("顺序号"), true, false, rowCompare);
            Order.Classes.Function.DrawCombo(this.neuSpread1.Sheets[0], GetColumnIndexFromName("组合号"), GetColumnIndexFromName("组合"));

            for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
            {
                if (this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag != null
                    && this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag.ToString() == "哈哈")
                {
                    this.neuSpread1.ActiveSheet.ActiveRowIndex = i;
                    this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
                    this.ucOutPatientItemSelect1.CurrentRow = ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    this.neuSpread1.ActiveSheet.AddSelection(ActiveRowIndex, 0, 1, this.neuSpread1.ActiveSheet.ColumnCount);
                    this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;

                    this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag = null;
                    break;
                }
            }

            this.SelectionChanged();
        }

        /// <summary>
        /// 下移医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDown_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex >= this.neuSpread1.ActiveSheet.RowCount - 1)
                return;

            FS.HISFC.Models.Order.OutPatient.Order upOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex).Clone();
            FS.HISFC.Models.Order.OutPatient.Order downOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex + 1, this.neuSpread1.ActiveSheetIndex).Clone();

            //组合内移动
            if (upOrder.Combo.ID == downOrder.Combo.ID)
            {
                upOrder.SortID += 1;
                AddObjectToFarpoint(upOrder, this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

                this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("顺序号")].Tag = "哈哈";

                downOrder.SortID -= 1;
                AddObjectToFarpoint(downOrder, this.neuSpread1.ActiveSheet.ActiveRowIndex + 1, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

            }
            else
            {
                int upNum = 0;
                int downNum = 0;
                FS.HISFC.Models.Order.OutPatient.Order oTmp = null;
                for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                {
                    oTmp = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (oTmp.Combo.ID == upOrder.Combo.ID)
                    {
                        upNum++;
                    }
                    if (oTmp.Combo.ID == downOrder.Combo.ID)
                    {
                        downNum++;
                    }
                }

                for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                {
                    oTmp = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (oTmp.Combo.ID == upOrder.Combo.ID)
                    {
                        oTmp.SortID = oTmp.SortID + downNum;
                        oTmp.SubCombNO = downOrder.SubCombNO;
                    }
                    else if (oTmp.Combo.ID == downOrder.Combo.ID)
                    {
                        oTmp.SortID = oTmp.SortID - upNum;
                        oTmp.SubCombNO = upOrder.SubCombNO;
                    }
                    this.AddObjectToFarpoint(oTmp, i, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

                    if (i == this.ActiveRowIndex)
                    {
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag = "哈哈";
                    }
                }
            }

            this.neuSpread1.Sheets[0].SortRows(GetColumnIndexFromName("顺序号"), true, false, rowCompare);
            Order.Classes.Function.DrawCombo(this.neuSpread1.Sheets[0], GetColumnIndexFromName("组合号"), GetColumnIndexFromName("组合"));

            for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
            {
                if (this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag != null
                    && this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag.ToString() == "哈哈")
                {
                    this.neuSpread1.ActiveSheet.ActiveRowIndex = i;
                    this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
                    this.ucOutPatientItemSelect1.CurrentRow = ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    this.neuSpread1.ActiveSheet.AddSelection(ActiveRowIndex, 0, 1, this.neuSpread1.ActiveSheet.ColumnCount);
                    this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;

                    this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("顺序号")].Tag = null;
                    break;
                }
            }

            this.SelectionChanged();
        }

        /// <summary>
        /// 自批价项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangePrice_Click(object sender, EventArgs e)
        {
            Forms.frmPopShow frm = new Forms.frmPopShow();
            frm.Text = "此项目为自批价项目，请输入价格";
            frm.isPrice = true;
            FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Order.OutPatient.Order;
            if (order.Item.Price != 0 && order.Item.User03 != "自批价")
            {
                ucOutPatientItemSelect1.MessageBoxShow("该项目不是自批价项目，不能修改价格");
                return;
            }
            frm.ModuleName = order.Item.Price.ToString();
            if (order == null)
            {
                return;
            }
            frm.ShowDialog();
            order.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(frm.ModuleName);
            order.Item.User03 = "自批价";
            this.ucOutPatientItemSelect1.OperatorType = Operator.Modify;
            this.ucItemSelect1_OrderChanged(order, EnumOrderFieldList.Item);
        }

        /// <summary>
        /// 院注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnumnuInjectNum_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }
            FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.OutPatient.Order;

            this.AddInjectNum(order, true);
        }

        /// <summary>
        /// 粘贴医嘱{DF8058FF-72C0-404f-8F36-6B4057B6F6CD}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPasteOrder_Click(object sender, EventArgs e)
        {
            this.PasteOrder();
        }

        /// <summary>
        ///  修改重打电子申请单
        /// {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPACSApply_Click(object sender, EventArgs e)
        {
            //if (PACSApplyInterface == null)
            //{
            //    if (InitPACSApplyInterface() < 0)
            //    {
            //        ucOutPatientItemSelect1.MessageBoxShow("初始化电子申请单接口时出错！");
            //        return;
            //    }
            //}
            //FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.OutPatient.Order;

            //if (order.ApplyNo == null || order.ApplyNo == "")
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("此医嘱尚未保存，请先保存！");
            //    return;
            //}

            //if (PACSApplyInterface.UpdateApply(order.ApplyNo) < 0)
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("修改重打电子申请单时出错！");
            //    return;
            //}
        }

        /// <summary>
        /// 通过历史医嘱复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyOrder_Click(object sender, EventArgs e)
        {
            this.CopyOrder();
        }

        /// <summary>
        /// 修改医保限制性用药信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuEditIndications_Click(object sender, EventArgs e)
        {
            int i = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            if (i < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("请先选择一条医嘱！");
                return;
            }
            FS.HISFC.Models.Order.OutPatient.Order order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.ActiveSheet.Rows[i].Tag;

            if (order != null)
            {
                // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                if (string.IsNullOrEmpty(order.ID))//这里不能为空，否则更改不了，报错
                {
                    MessageBox.Show("请先保存医嘱！", "提示");
                    return;
                }
                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(this.Patient.ID, order.ID);
                FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(GetItemUserCode(order.Item));
                if (indicationsObj == null)
                {
                    MessageBox.Show("该药品非限制用药，无法选择！", "提示");
                    return;
                }
                if (MessageBox.Show("药品【" + order.Item.Name + "】属于限制级药品，\r\n\r\n限制药品说明：【" + indicationsObj.Name + "】\r\n\r\n请确定医保报销设定。报销(是)，自费(否)?\r\n", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (orderExtObj == null)
                    {
                        orderExtObj = new FS.HISFC.Models.Order.OutPatient.OrderExtend();
                    }
                    orderExtObj.ClinicCode = this.Patient.ID;
                    orderExtObj.Indications = "1";
                    orderExtObj.MoOrder = order.ID;
                    orderExtObj.Oper.ID = orderExtMgr.Operator.ID;
                }
                else
                {
                    if (orderExtObj == null)
                    {
                        orderExtObj = new FS.HISFC.Models.Order.OutPatient.OrderExtend();
                        orderExtObj.ClinicCode = this.Patient.ID;
                        orderExtObj.MoOrder = order.ID;
                    }

                    orderExtObj.Indications = "0";
                    orderExtObj.Oper.ID = orderExtMgr.Operator.ID;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int rev = orderExtMgr.UpdateOrderExtend(orderExtObj);
                if (rev == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新医嘱扩展信息错误：\r\n" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (rev == 0)
                {
                    rev = orderExtMgr.InsertOrderExtend(orderExtObj);
                    if (rev == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入医嘱扩展信息错误：\r\n" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
        }

        /// <summary>
        /// 存组套
        /// {C6E229AC-A1C4-4725-BBBB-4837E869754E}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSaveGroup_Click(object sender, EventArgs e)
        {
            this.SaveGroup();
        }

        #endregion

        #region 快捷键

        /// <summary>
        /// 此处只处理处方列表的上下键选择
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (this.ucOutPatientItemSelect1.IsCanChangeSelectOrder())
            {
                if (this.IsDesignMode || EditGroup)
                {
                    if (keyData == Keys.Down)
                    {
                        if (this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.RowCount - 1)
                        {
                            this.neuSpread1_Sheet1.ActiveRowIndex += 1;
                            neuSpread1_Sheet1.AddSelection(neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);
                            this.neuSpread1.ShowRow(0, this.neuSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                            this.SelectionChanged();
                        }
                    }
                    else if (keyData == Keys.Up)
                    {
                        if (this.neuSpread1_Sheet1.ActiveRowIndex > 0)
                        {
                            this.neuSpread1_Sheet1.ActiveRowIndex -= 1;
                            neuSpread1_Sheet1.AddSelection(neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);
                            this.neuSpread1.ShowRow(0, this.neuSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                            this.SelectionChanged();
                        }
                    }
                    else if (keyData == Keys.Tab)
                    {
                        if (this.ucOutPatientItemSelect1.RecycleTab())
                        {
                            return true;
                        }
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                this.mnumnuInjectNum_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region 初始化工具栏

        /// <summary>
        /// 工具栏
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 工具栏初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("开立", "开立医嘱", 9, true, false, null);
            toolBarService.AddToolButton("组合", "组合医嘱", 9, true, false, null);
            ////toolBarService.AddToolButton("手术单", "手术申请", 9, true, false, null);
            toolBarService.AddToolButton("删除", "删除医嘱", 9, true, false, null);
            toolBarService.AddToolButton("取消组合", "取消组合医嘱", 9, true, false, null);
            ////toolBarService.AddToolButton("明细", "检验明细", 9, true, true, null);
            toolBarService.AddToolButton("退出医嘱更改", "退出医嘱更改", 9, true, false, null);
            toolBarService.AddToolButton("留观", "留观", 9, true, false, null);
            return toolBarService;
        }

        /// <summary>
        /// 工具栏按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "开立")
            {
                this.Add(false);
            }
            else if (e.ClickedItem.Text == "组合")
            {
                this.ComboOrder();
            }
            else if (e.ClickedItem.Text == "留观")
            {
                this.RegisterEmergencyPatient();
            }
        }
        #endregion

        #region 新加的函数

        private object currentObject = null;

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.pnTop.Visible = true;
            if (neuObject == null)
            {
                currentObject = new object();
                this.txtInfo.Text = "";
                this.pnTop.Visible = false;
                return 0;
            }
            if (neuObject.GetType() == typeof(FS.HISFC.Models.Registration.Register))
            {
                if (currentObject != neuObject)
                {
                    this.Patient = neuObject as FS.HISFC.Models.Registration.Register;

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询患者标签信息,请稍候!");
                    //{0F599816-C860-40e1-856A-EF5ACACBDA26}
                    //{91AB66D4-38D1-448f-B2AF-FA9D1F114A67}
                    ucPatientLabel1.getUserLabelByHisCardNo(this.Patient.PID.CardNO);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                }

                currentObject = neuObject;
            }
            return 0;
        }

        /// <summary>
        /// 设置项目输入框是否可见
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetInputItemVisible(bool isVisible)
        {
            this.ucOutPatientItemSelect1.SetInputControlVisible(isVisible);
        }
        #endregion

        #region 处方打印

        /// <summary>
        /// 处方打印
        /// </summary>
        /// <param name="recipeNO"></param>
        public void PrintRecipe()
        {
            if (this.EditGroup)
            {
                ucOutPatientItemSelect1.MessageBoxShow("您正在编辑组套，此时不支持打印处方！");
                return;
            }
            ArrayList alRecipe = new ArrayList();

            alRecipe = this.GetRecipeArray();

            if (iRecipePrint == null)
            {
                iRecipePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.IRecipePrint)) as FS.HISFC.BizProcess.Interface.IRecipePrint;
            }

            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = this.GetCaseInfo();

            if (iRecipePrint == null)
            {
                CacheManager.AccountMgr.Err = "处方打印接口未实现！";
                CacheManager.AccountMgr.WriteErr();
                return;
                //ucOutPatientItemSelect1.MessageBoxShow("接口未实现");
            }
            else
            {
                if (alRecipe.Count > 0 || caseHistory != null)
                {
                    if (ucOutPatientItemSelect1.MessageBoxShow("是否打印处方？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        foreach (FS.FrameWork.Models.NeuObject fuck in alRecipe)
                        {
                            iRecipePrint.SetPatientInfo(this.currentPatientInfo);
                            iRecipePrint.RecipeNO = fuck.ID;
                            iRecipePrint.PrintRecipe();
                        }
                    }
                }
            }
        }

        #region 预约入院

        public void PrePayIn()// {6BF1F99D-7307-4d05-B747-274D24174895}
        {
            if (this.currentPatientInfo == null || string.IsNullOrEmpty(this.currentPatientInfo.PID.CardNO))
            {
                MessageBox.Show("没有选中患者，请选中患者！");
                return;
            }
            IPrePayIn = null;
            IPrePayIn = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Fee.IPrePayIn)) as FS.HISFC.BizProcess.Interface.Fee.IPrePayIn;

            if (IPrePayIn != null)
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                patientInfo = managerIntegrate.QueryComPatientInfo(this.currentPatientInfo.PID.CardNO);
                IPrePayIn.PatientInfo = patientInfo;
                IPrePayIn.IsShowButton = true;
                IPrePayIn.ShowDialog();
            }
        }

        #endregion
        /// <summary>
        /// 获得药品处方数组
        /// </summary>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        private ArrayList GetRecipeArray()
        {
            ArrayList alRecipe = new ArrayList();

            alRecipe = CacheManager.OutOrderMgr.GetPhaRecipeNoByClinicNoAndSeeNo(this.currentPatientInfo.ID, this.Patient.DoctorInfo.SeeNO.ToString());

            return alRecipe;
        }

        /// <summary>
        /// 获取病历信息
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory GetCaseInfo()
        {
            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;
            caseHistory = CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.currentPatientInfo.ID);

            return caseHistory;
        }
        #endregion

        #region 打印所有单据

        /// <summary>
        /// 打印所有单据 
        /// </summary>        
        public void PrintAllBill(string type, bool IsPreview)
        {
            if (this.EditGroup)
            {
                ucOutPatientItemSelect1.MessageBoxShow("您正在编辑组套，此时不支持打印处方！");
                return;
            }
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                ucOutPatientItemSelect1.MessageBoxShow("请选择患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FS.HISFC.Models.Order.OutPatient.Order order;
            List<FS.HISFC.Models.Order.OutPatient.Order> alOrder = new List<FS.HISFC.Models.Order.OutPatient.Order>(); //保存医嘱

            List<FS.HISFC.Models.Order.OutPatient.Order> alSelectOrder = new List<FS.HISFC.Models.Order.OutPatient.Order>(); //保存医嘱

            FS.HISFC.Models.Registration.Register patient;

            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = ((FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag).Clone();
                order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                // 根据项目编号查询申请单类型{D793A341-AD35-4685-8817-5614217969AD} 2014-12-16 by lixuelong
                order.Item.Extend1 = CacheManager.OutOrderMgr.QueryApplyTypeByItemCode(order.Item.ID).ID;
                order.Item.Extend2 = CacheManager.OutOrderMgr.QueryApplyTypeByItemCode(order.Item.ID).Name;
                if (this.neuSpread1.Sheets[0].IsSelected(i, 0))
                {
                    alSelectOrder.Add(order);
                }
                alOrder.Add(order);
            }

            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = this.GetCaseInfo();

            #region 调用接口实现打印
            if (IOutPatientPrint != null && (alOrder.Count > 0 || caseHistory != null))
            {
                patient = this.Patient.Clone();
                if (IOutPatientPrint.OnOutPatientPrint(patient, this.GetReciptDept(), this.GetReciptDoct(), alOrder, alSelectOrder, false, IsPreview, type, false) != 1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(IOutPatientPrint.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        #endregion

        #region 合理用药

        /// <summary>
        /// 初始化IReasonableMedicin
        /// </summary>
        private void InitReasonableMedicine()
        {
            //此处是否增加控制参数，是否启用合理用药...
            if (this.IReasonableMedicine == null)
            {
                this.IReasonableMedicine = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine)) as FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine;
            }
        }

        /// <summary>
        /// 合理用药刷新
        /// </summary>
        private void PassRefresh()
        {
            if (IReasonableMedicine != null && IReasonableMedicine.PassEnabled)
            {
                IReasonableMedicine.PassRefresh();
            }
        }

        /// <summary>
        /// 启动合理用药
        /// </summary>
        private void StartReasonableMedicine()
        {
            int iReturn = 0;
            Employee empl = FrameWork.Management.Connection.Operator as Employee;
            iReturn = this.IReasonableMedicine.PassInit(empl, empl.Dept, "10");

            if (iReturn == -1)
            {
                this.EnabledPass = false;
                if (!string.IsNullOrEmpty(IReasonableMedicine.Err))
                {
                    ucOutPatientItemSelect1.MessageBoxShow(IReasonableMedicine.Err);
                }
            }
            if (iReturn == 0)
            {
                this.EnabledPass = false;
            }
        }

        /// <summary>
        /// 双击查看合理用药要点提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.IReasonableMedicine != null
                && this.IReasonableMedicine.PassEnabled)
            {

                this.IReasonableMedicine.PassShowFloatWindow(false);

                if (!e.RowHeader && !e.ColumnHeader)
                {
                    FS.HISFC.Models.Order.OutPatient.Order info = this.GetObjectFromFarPoint(e.Row, 0);
                    if (info == null)
                    {
                        return;
                    }
                    if (info.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        return;
                    }

                    #region 药品查询
                    try
                    {
                        //貌似他们只和右下角的坐标位置相关
                        this.IReasonableMedicine.PassShowSingleDrugInfo(info, new Point(MousePosition.X, MousePosition.Y - 60),
                            new Point(MousePosition.X + 100, MousePosition.Y + 15), false);
                    }
                    catch (Exception ex)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 单击显示要点提示和及时性警示浮动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                #region 单击调用合理用药的貌似没用了

                if (false)
                {
                    if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
                    {
                        if (e.RowHeader || e.ColumnHeader)
                        {
                            return;
                        }
                        try
                        {
                            this.IReasonableMedicine.PassShowFloatWindow(false);

                            FS.HISFC.Models.Order.OutPatient.Order info = this.GetObjectFromFarPoint(e.Row, 0);
                            if (info == null)
                            {
                                return;
                            }
                            if (info.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                return;
                            }

                            //点击名称列显示要点提示
                            if (e.Column == GetColumnIndexFromName("医嘱名称") && this.enabledPass)
                            {
                                //点击名称不再显示要点提示，在开立时或双击时可以看到要点提示

                                ////貌似他们只和右下角的坐标位置相关
                                //if (this.IReasonableMedicine.PassShowSingleDrugInfo(info,
                                //    new Point(MousePosition.X, MousePosition.Y - 60),
                                //    new Point(MousePosition.X + 100, MousePosition.Y + 15), false) == -1)
                                //{
                                //    ucOutPatientItemSelect1.MessageBoxShow(IReasonableMedicine.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //}
                            }
                            //点击合理用药警示级别显示及时性警示浮动窗口
                            else if (e.Column == GetColumnIndexFromName("警"))
                            {
                                if (this.IReasonableMedicine.PassShowWarnDrug(info) == -1)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow(IReasonableMedicine.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 向合理用药系统传送当前医嘱进行审查
        /// </summary>
        /// <param name="isSave">是否保存时调用</param>
        public int PassCheckOrder(bool isSave)
        {
            ArrayList alOrder = new ArrayList();

            //1表示当前界面开立的处方
            //2表示当前挂号记录的处方
            //3表示当前患者有效期内的处方
            int type = 1;

            if (type == 1)
            {
                FS.HISFC.Models.Order.OutPatient.Order order;
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    order = this.GetObjectFromFarPoint(i, 0);
                    if (order == null)
                    {
                        continue;
                    }
                    if (order.Status == 3)
                    {
                        continue;
                    }
                    if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        continue;
                    }

                    order.Frequency = SOC.HISFC.BizProcess.Cache.Order.GetFrequency(order.Frequency.ID);

                    //之前用于美康合理用药的，现在没用了
                    //order.ApplyNo = CacheManager.OrderMgr.GetSequence("Order.Pass.Sequence");
                    alOrder.Add(order);
                }
            }
            else if (type == 2)
            {
                string whereSQL = @"
                          where clinic_code='{0}'";

                alOrder = CacheManager.OutOrderMgr.QueryOrder(whereSQL, this.Patient.ID, string.Empty);
                if (alOrder == null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("查询患者门诊处方出错！\r\n" + CacheManager.OutOrderMgr.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DateTime dtDate = Classes.Function.GetRegValideDate(Patient.DoctorInfo.Templet.RegLevel.IsEmergency);
                string whereSQL = @"
                          where card_no='{0}'
                          and reg_date>to_date('{1}','yyyy-mm-dd hh24:mi:ss')";

                alOrder = CacheManager.OutOrderMgr.QueryOrder(whereSQL, this.Patient.PID.CardNO, dtDate.ToString());
                if (alOrder == null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("查询患者门诊处方出错！\r\n" + CacheManager.OutOrderMgr.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (alOrder.Count > 0)
            {
                ArrayList alDiag = CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.Patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);

                this.IReasonableMedicine.PassSetDiagnoses(alDiag);
                this.IReasonableMedicine.PassSetPatientInfo(this.Patient, this.GetReciptDoct());

                int rev = this.IReasonableMedicine.PassDrugCheck(alOrder, isSave);
                if (rev == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("合理用药审查出错：" + this.IReasonableMedicine.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                //人为选择不通过
                else if (rev == 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 退出合理用药
        /// </summary>
        public void QuitPass()
        {
            if (this.IReasonableMedicine != null && IReasonableMedicine.PassEnabled)
            {
                IReasonableMedicine.PassShowFloatWindow(false);
                IReasonableMedicine.PassClose();
            }
        }

        /// <summary>
        /// 合理药品系统查询
        /// </summary>
        private void mnuPass_Click(object sender, EventArgs e)
        {
            if (this.IReasonableMedicine != null && !this.IReasonableMedicine.PassEnabled)
                return;
            this.IReasonableMedicine.PassShowFloatWindow(false);

            FS.HISFC.Models.Order.OutPatient.Order info = this.GetObjectFromFarPoint(this.neuSpread1.Sheets[0].ActiveRowIndex, 0);
            if (info == null)
            {
                return;
            }

            if (info.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return;
            }

            ToolStripItem muItem = sender as ToolStripItem;

            ArrayList alMenu = new ArrayList();

            this.IReasonableMedicine.PassSetPatientInfo(this.Patient, this.GetReciptDoct());
            IReasonableMedicine.PassShowOtherInfo(info, new FS.FrameWork.Models.NeuObject("", muItem.Text, ""), ref alMenu);
        }

        #endregion

        #region 计算显示附材

        /// <summary>
        /// 计算显示附材
        /// </summary>
        /// <param name="isRegFeeOnly">开立进去只计算挂号费</param>
        /// <returns></returns>
        public int CalculatSubl(bool isRegFeeOnly)
        {
            if (this.dealSublMode == 1)
            {
                ArrayList alOrder = new ArrayList();
                FS.HISFC.Models.Order.OutPatient.Order order = null;

                if (!isRegFeeOnly)
                {
                    #region 处理附材
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    try
                    {
                        for (int i = this.neuSpread1.Sheets[0].Rows.Count - 1; i >= 0; i--)
                        {
                            order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                            if (order == null)
                            {
                                continue;
                            }

                            FS.HISFC.Models.Order.OutPatient.Order newOrder = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                            //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                            if (newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            //注释后可以保存已收费的处方
                            /* if (newOrder.Status != 0 || newOrder.IsHaveCharged)//检查并发医嘱状态
                             {
                                 {7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                                 FS.FrameWork.Management.PublicTrans.RollBack();
                                 ucOutPatientItemSelect1.MessageBoxShow("计算附材错误！\r\n[" + order.Item.Name + "]可能已经收费,请退出开立界面重新进入!\r\n" + CacheManager.OutOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                             }*/


                            alOrder.Add(order);
                            if (order != null && order.IsSubtbl)
                            {
                                if (order.Memo == "挂号费")
                                {
                                    if (!this.isAddRegSubBeforeAddOrder)
                                    {
                                        //{2F018544-DADF-4a77-B00E-668D49BE8297}
                                        //&& this.Patient.SeeDoct.ID == FS.FrameWork.Management.Connection.Operator.ID
                                        if (this.Patient.IsSee)
                                        {
                                        }
                                        else
                                        {
                                            this.neuSpread1.Sheets[0].Rows.Remove(i, 1);
                                        }
                                    }
                                }
                                else
                                {
                                    this.neuSpread1.Sheets[0].Rows.Remove(i, 1);
                                }
                            }
                        }

                        if (this.IDealSubjob != null)
                        {
                            dirty = true;
                            this.IDealSubjob.IsPopForChose = false;
                            if (alOrder.Count > 0)
                            {
                                string errText = "";
                                if (IDealSubjob.DealSubjob(this.Patient, alOrder, null, ref alSubOrders, ref errText) <= 0)
                                {
                                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                                    //FS.FrameWork.Management.PublicTrans.RollBack();
                                    ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败：" + errText, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return -1;
                                }

                                if (alSubOrders != null && alSubOrders.Count > 0)
                                {
                                    foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alSubOrders)
                                    {
                                        //orderObj.Combo.ID = CacheManager.OrderMgr.GetNewOrderComboID();
                                        //orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                        orderObj.SortID = 0;
                                        orderObj.ID = "";
                                        if (orderObj.SubCombNO == 0)
                                        {
                                            orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                        }

                                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                                        this.AddObjectToFarpoint(orderObj, this.neuSpread1_Sheet1.RowCount - 1, 0, EnumOrderFieldList.Item);
                                    }
                                }
                            }
                            dirty = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }

                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    //FS.FrameWork.Management.PublicTrans.Commit();

                    #endregion
                }

                #region 处理挂号费
                if (this.dealSublMode == 1)
                {
                    if ((this.isAddRegSubBeforeAddOrder && isRegFeeOnly)
                        || (!isAddRegSubBeforeAddOrder && !isRegFeeOnly))
                    {
                        if (this.SetSupplyRegFee(ref alSupplyFee, ref errInfo, currentPatientInfo.IsFee) == -1)
                        {
                            this.alSupplyFee = new ArrayList();
                            ucOutPatientItemSelect1.MessageBoxShow("补收挂号费失败：" + errInfo);
                            return -1;
                        }

                        for (int i = this.neuSpread1.Sheets[0].Rows.Count - 1; i >= 0; i--)
                        {
                            order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;
                            if (order == null)
                            {
                                continue;
                            }

                            FS.HISFC.Models.Order.OutPatient.Order newOrder = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                            //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                            if (newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            if (newOrder.Status != 0 || newOrder.IsHaveCharged)//检查并发医嘱状态
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                ucOutPatientItemSelect1.MessageBoxShow("计算附材错误！\r\n[" + order.Item.Name + "]可能已经收费,请退出开立界面重新进入!\r\n" + CacheManager.OutOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                            if (order.IsSubtbl)
                            {
                                if (order.Memo == "挂号费")
                                {
                                    alSupplyFee = new ArrayList();
                                    break;
                                }
                            }
                        }

                        if (alSupplyFee.Count > 0)
                        {
                            FS.HISFC.Models.Order.OutPatient.Order newOrder = null;
                            FS.HISFC.Models.Order.OutPatient.Order orderTemp = null;
                            if (alOrder.Count > 0)
                            {
                                orderTemp = alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order;
                            }

                            if (orderTemp == null)
                            {
                                orderTemp = new FS.HISFC.Models.Order.OutPatient.Order();
                                orderTemp.HerbalQty = 1;
                                orderTemp.Combo = new FS.HISFC.Models.Order.Combo();
                            }

                            FS.HISFC.Models.Fee.Item.Undrug item = null;
                            ArrayList alSupplyOrder = new ArrayList();

                            //{0BEB97B8-373D-45d0-A186-12502DD0AADE}
                            if (MessageBox.Show("是否为看诊号！", "挂号费自动带出提示", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                ArrayList regConstList = CacheManager.GetConList("RegFeeItem");
                                if (regConstList == null || regConstList.Count == 0)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("获取开单号挂号费项目失败！" + CacheManager.ConManager.Err);
                                    return -1;
                                }

                                alSupplyFee.Clear();

                                FS.HISFC.Models.Fee.Item.Undrug regItem = new FS.HISFC.Models.Fee.Item.Undrug();
                                string regItemCode = ((FS.FrameWork.Models.NeuObject)regConstList[0]).ID.Trim();

                                if (this.CheckItem(regItemCode, ref errInfo, ref regItem) == -1)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("获取开单号挂号费项目失败" + errInfo);
                                    return -1;
                                }

                                string ErrInfo = string.Empty;
                                regFeeItem = SetSupplyFeeItemListByItem(regItem, ref ErrInfo);

                                alSupplyFee.Add(regFeeItem);
                            }
                            else
                            {
                                //{B923E235-2301-4a94-906C-1207AC04B4D6}
                                //处理特殊专家出诊的会诊费,改用memo字段
                                FrameWork.Models.NeuObject hzDoct = CacheManager.ConManager.GetConstant("HZDoctList", oper.ID);
                                //ArrayList regHZConstList = CacheManager.GetConList("RegFeeItemHZ"); //regHZConstList.Count > 0

                                if (hzDoct != null && !string.IsNullOrEmpty(hzDoct.ID) && !string.IsNullOrEmpty(hzDoct.Memo))
                                {
                                    alSupplyFee.Clear();
                                    FS.HISFC.Models.Fee.Item.Undrug regItem = new FS.HISFC.Models.Fee.Item.Undrug();
                                    //string regItemCode = ((FS.FrameWork.Models.NeuObject)regHZConstList[0]).ID.Trim();
                                    string regItemCode = hzDoct.Memo.Trim();

                                    if (this.CheckItem(regItemCode, ref errInfo, ref regItem) == -1)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow("获取专家会诊费项目失败" + errInfo);
                                        return -1;
                                    }

                                    string ErrInfo = string.Empty;
                                    regFeeItem = SetSupplyFeeItemListByItem(regItem, ref ErrInfo);

                                    alSupplyFee.Add(regFeeItem);
                                }
                            }

                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemObj in alSupplyFee)
                            {
                                //定义个新医嘱对象
                                newOrder = new FS.HISFC.Models.Order.OutPatient.Order();//重新设置医嘱

                                item = CacheManager.FeeIntegrate.GetItem(itemObj.Item.ID);//获得最新项目信息
                                if (item == null)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("计算附材时，查找项目失败：" + CacheManager.FeeIntegrate.Err);
                                    return -1;
                                }

                                if (item.UnitFlag == "1")
                                {
                                    item.Price = CacheManager.FeeIntegrate.GetUndrugCombPrice(itemObj.Item.ID);
                                }

                                item.Qty = itemObj.Item.Qty;

                                newOrder = orderTemp.Clone();

                                try
                                {
                                    newOrder.ReciptNO = "";
                                    newOrder.SequenceNO = -1;

                                    newOrder.Item = item.Clone();
                                    newOrder.Qty = item.Qty;

                                    newOrder.Unit = item.PriceUnit;

                                    newOrder.Combo = orderTemp.Combo.Clone();//组合号
                                    newOrder.ReciptSequence = orderTemp.ReciptSequence;

                                    newOrder.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;

                                    newOrder.DoseUnit = "";

                                    newOrder.IsEmergency = orderTemp.IsEmergency;
                                    newOrder.IsSubtbl = true;
                                    newOrder.Usage = new FS.FrameWork.Models.NeuObject();
                                    newOrder.SequenceNO = -1;
                                    if (newOrder.ExeDept.ID == "")//执行科室默认
                                    {
                                        newOrder.ExeDept = this.GetReciptDept();
                                    }

                                    newOrder.HerbalQty = 1;
                                    newOrder.Frequency = Components.Order.Classes.Function.GetDefaultFrequency().Clone();
                                    newOrder.InjectCount = 0;
                                    newOrder.IsSubtbl = true;

                                    alSupplyOrder.Add(newOrder);
                                }
                                catch (Exception ex)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("计算项目附材时，创建附材医嘱发生错误：" + ex.Message);
                                    return -1;
                                }
                            }

                            if (alSupplyOrder != null && alSupplyOrder.Count > 0)
                            {
                                foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alSupplyOrder)
                                {
                                    orderObj.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();
                                    orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                    orderObj.SortID = 0;
                                    orderObj.Status = 0;
                                    orderObj.Memo = "挂号费";

                                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                                    this.AddObjectToFarpoint(orderObj, this.neuSpread1_Sheet1.RowCount - 1, 0, EnumOrderFieldList.Item);
                                }
                            }
                        }
                    }
                }

                #endregion

                this.ucOutPatientItemSelect1.Clear(false);
                this.ActiveRowIndex = 0;

                RefreshOrderState();

                this.neuSpread1.ShowRow(this.neuSpread1.ActiveSheetIndex, this.neuSpread1.ActiveSheet.RowCount - 1, FarPoint.Win.Spread.VerticalPosition.Center);
            }
            return 1;
        }

        #endregion

        #region 修改合同单位信息

        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbPact.Tag.ToString() != this.currentPatientInfo.Pact.ID)
            {
                if (isAllowChangePactInfo)
                {
                    if (this.currentPatientInfo.IsSee)
                    {
                        ArrayList alFee = CacheManager.FeeIntegrate.QueryAllFeeItemListsByClinicNO(this.currentPatientInfo.ID, "1", "ALL", "ALL");
                        if (alFee != null && alFee.Count > 0)
                        {
                            this.cmbPact.Enabled = false;
                            this.cmbPact.Tag = currentPatientInfo.Pact.ID;
                            ucOutPatientItemSelect1.MessageBoxShow("患者已经有收费信息，不能修改合同单位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                PactInfo pactTemp = this.currentPatientInfo.Pact.Clone();

                string pactCode = this.cmbPact.Tag.ToString();
                if (string.IsNullOrEmpty(pactCode))
                {
                    return;
                }

                FS.HISFC.Models.Registration.Register patientInfo = new FS.HISFC.Models.Registration.Register();
                patientInfo.ID = currentPatientInfo.ID;
                patientInfo.PID = this.currentPatientInfo.PID;
                patientInfo.Name = currentPatientInfo.Name;
                patientInfo.Sex = currentPatientInfo.Sex;
                patientInfo.Birthday = currentPatientInfo.Birthday;
                patientInfo.IDCard = currentPatientInfo.IDCard;
                patientInfo.Pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(pactCode);
                this.currentPatientInfo.Pact = patientInfo.Pact.Clone();


                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                #region 接口判断合同单位限制

                if (this.iCheckPactInfo == null)
                {
                    this.iCheckPactInfo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPactInfo)) as FS.HISFC.BizProcess.Interface.Common.ICheckPactInfo;
                }
                if (this.iCheckPactInfo == null)
                {
                    //if (!string.IsNullOrEmpty(FS.FrameWork.WinForms.Classes.UtilInterface.Err))
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow("获得接口ICheckPactInfo错误,导致无法判断合同单位的有效性！\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                else
                {
                    iCheckPactInfo.PatientInfo = patientInfo;
                    if (iCheckPactInfo.CheckIsValid() == -1)
                    {
                        this.cmbPact.Tag = pactCode;
                        this.cmbPact.Text = pactTemp.Name;
                        ucOutPatientItemSelect1.MessageBoxShow(iCheckPactInfo.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                #endregion

                if (CacheManager.RegInterMgr.UpdateRegInfoByClinicCode(patientInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.currentPatientInfo.Pact = pactTemp.Clone();
                    this.cmbPact.Tag = pactCode;
                    this.cmbPact.Text = pactTemp.Name;
                    ucOutPatientItemSelect1.MessageBoxShow("更新合同单位信息错误：" + CacheManager.RegInterMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                this.SetOrderFeeDisplay(false, true);
            }
        }

        private void cmbPact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ucOutPatientItemSelect1.Clear(true);
            }
        }

        #endregion

        #region IInterfaceContainer成员
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[7];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.IRecipePrint);
                t[1] = typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPrint);//检查申请单
                //{48E6BB8C-9EF0-48a4-9586-05279B12624D}
                t[2] = typeof(FS.HISFC.BizProcess.Interface.IAlterOrder);
                t[3] = typeof(FS.HISFC.BizProcess.Interface.Common.IPacs);
                t[4] = typeof(FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine);
                t[5] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee);
                t[6] = typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob);
                return t;
            }
        }
        #endregion
    }

    /// <summary>
    /// 列序号排序
    /// </summary>
    class RowCompare : IComparer
    {
        #region IComparable 成员

        public int Compare(object x, object y)
        {
            try
            {
                int a = FS.FrameWork.Function.NConvert.ToInt32(x);
                int b = FS.FrameWork.Function.NConvert.ToInt32(y);
                if (a > b)
                {
                    return 1;
                }
                else if (a == b)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}