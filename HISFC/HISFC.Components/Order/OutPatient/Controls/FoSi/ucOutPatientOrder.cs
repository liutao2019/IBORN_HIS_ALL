using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Base;
using Neusoft.HISFC.BizProcess.Interface.Order;
using Neusoft.FrameWork.Management;
using Neusoft.HISFC.Components.Order.OutPatient.Controls;

namespace Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi
{
    /// <summary>
    /// 门诊医生站
    /// </summary>
    public partial class ucOutPatientOrder : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOutPatientOrder()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.contextMenu1 = new Neusoft.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            }

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

        #endregion

        #region 变量

        /// <summary>
        /// 挂号有效天数
        /// </summary>
        public int validRegDays = 1;

        /// <summary>
        /// 临时控制参数
        /// </summary>
        protected object tempControler;

        /// <summary>
        /// 存储医嘱内容
        /// </summary>
        protected DataSet dtOrder = null;

        /// <summary>
        /// 存储医嘱内容
        /// </summary>
        protected DataView dvOrder = null;

        /// <summary>
        /// 最大顺序号
        /// </summary>
        private int MaxSort = 0;

        /// <summary>
        /// 是否整在编辑组套
        /// </summary>
        protected bool EditGroup = false;

        /// <summary>
        /// 临时存的医嘱信息
        /// </summary>
        private ArrayList alOrderTemp = new ArrayList();

        /// <summary>
        /// 全部医嘱信息
        /// </summary>
        public ArrayList alAllOrder = new ArrayList();

        /// <summary>
        /// 当前的医嘱信息
        /// </summary>
        protected Neusoft.HISFC.Models.Order.OutPatient.Order currentOrder = null;

        /// <summary>
        /// 当前患者
        /// </summary>
        protected Neusoft.HISFC.Models.Registration.Register currentPatientInfo = new Neusoft.HISFC.Models.Registration.Register();

        /// <summary>
        /// 当前诊台
        /// </summary>
        protected Neusoft.FrameWork.Models.NeuObject currentRoom = null;

        /// <summary>
        /// 界面显示配置文件
        /// </summary>
        private string SetingFileName = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + @".\clinicordersetting.xml";

        /// <summary>
        /// 悬停提示
        /// </summary>
        ToolTip tooltip = new ToolTip();

        /// <summary>
        /// 处方打印接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.IRecipePrint iRecipePrint = null;

        /// <summary>
        /// 右键菜单
        /// </summary>
        private Neusoft.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = null;

        /// <summary>
        /// 存储组合变化的医嘱的哈希表
        /// {F67E089F-1993-4652-8627-300295AAED8C}
        /// </summary>
        private Hashtable hsComboChange = new Hashtable();

        /// <summary>
        /// 错误提示信息
        /// </summary>
        private string errInfo = "";

        /// <summary>
        /// 是否有处方权
        /// </summary>
        //public bool isHaveOrderPower = false;

        #region 补挂号用

        /// <summary>
        /// 急诊号对应的诊查费项目
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug emergRegItem = null;

        /// <summary>
        /// 医生职称对应的诊查费项目
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug diagItem = null;

        /// <summary>
        /// 挂号费差额项目
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug diffDiagItem = null;

        /// <summary>
        /// 补收的挂号费项目
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug regItem = null;

        /// <summary>
        /// 免挂号费的科室
        /// </summary>
        private Hashtable hsNoSupplyRegDept = new Hashtable();

        /// <summary>
        /// 当前操作员
        /// 数据库重取的
        /// </summary>
        Neusoft.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 补收项目列表
        /// </summary>
        private ArrayList alSupplyFee = new ArrayList();

        /// <summary>
        /// 是否启用分诊系统
        /// </summary>
        private bool isUseNurseArray;

        /// <summary>
        /// 门诊医生是否自动打印处方:0 不自动打印；1 自动打印；2 审核预览处方;3 审核预览后自动打印
        /// </summary>
        private int isAutoPrintRecipe = 0;

        /// <summary>
        /// 是否允许医生开库存不足的药品：0不允许，1 提示，2 允许
        /// </summary>
        private int isCheckDrugStock = 0;

        /// <summary>
        /// 合同单位帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper pactHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        #endregion

        /// <summary>
        /// 附材处理模式，0 保存后自动带出；1 界面点击计算，显示在界面上允许修改
        /// </summary>
        private int dealSublMode = -1;

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

        //是否显示毒麻药品
        private bool isShowHardDrug = true;

        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();




        /// <summary>
        /// 每次量限制值（最小单位的数量)
        /// </summary>
        private decimal doceOnceLimit = -1;

        /// <summary>
        /// 是否控制药品开立权限
        /// </summary>
        //public bool isControlDrugOrder = false;

        /// <summary>
        /// 是否在药品名称列显示规格和价格
        /// 0 都不显示，1 显示规格，2 显示价格，3 规格、价格都显示
        /// </summary>
        private int isShowSpecsAndPrice = 0;

        /// <summary>
        /// 是否默认选中的天数、频次、用法全部修改，并重新计算总量
        /// 000 三位数字分别表示：天数、频次、用法
        /// </summary>
        private string isChangeAllSelect = "-1";

        /// <summary>
        /// 是否在开立之前添加挂号费项目 (配合dealSublMode=1 使用)
        /// </summary>
        private bool isAddRegSubBeforeAddOrder = false;

        /// <summary>
        /// 当前账户余额显示信息
        /// </summary>
        private string vacancyDisplay = "";

        /// <summary>
        /// 门诊医生站是否允许补挂号
        /// </summary>
        //private bool isDoctRegistered = false;

        /// <summary>
        /// 是否预扣库存
        /// {E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
        /// </summary>
        private bool isPreUpdateStockinfo = false;

        /// <summary>
        /// 门诊医生站皮试处理模式（0 不提示皮试 1：提示是否2：弹出界面选择）
        /// {0733E2AD-EB02-4b6f-BCF8-1A6ED5A2EFAD}
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
        /// </summary>
        private bool isJudgeDiagnose = true;

        /// <summary>
        /// 是否可以修改院注
        /// </summary>
        private bool isCanModifiedInjectNum = true;

        /// <summary>
        /// 是否跳出重新计算辅材
        /// </summary>
        private bool isCalculatSubl = false;

        #endregion

        #region 接口

        /// <summary>
        /// 接入电子申请单
        /// </summary>
        protected Neusoft.ApplyInterface.HisInterface PACSApplyInterface = null;

        /// <summary>
        /// 合同单位校验接口
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Common.ICheckPactInfo iCheckPactInfo = null;

        /// <summary>
        /// 保存后处方处理接口
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Order.ISaveOrder IAfterSaveOrder = null;

        /// <summary>
        /// 保存处方前调用
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder IBeforeSaveOrder = null;

        /// <summary>
        /// 增加项目前操作接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddItem IBeforeAddItem = null;

        /// <summary>
        /// 开立动作前接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddOrder IBeforeAddOrder = null;

        /// <summary>
        /// 医嘱信息变更接口
        /// {48E6BB8C-9EF0-48a4-9586-05279B12624D}
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.IAlterOrder IAlterOrderInstance = null;

        /// <summary>
        /// 检查申请单打印接口
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Common.ICheckPrint checkPrint = null;

        /// <summary>
        /// 直接收费接口
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee IDoctFee = null;

        /// <summary>
        /// 医生站辅材处理接口
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob IDealSubjob = null;

        /// <summary>
        /// 合理用药接口
        /// </summary>
        IReasonableMedicine IReasonableMedicine = null;

        #endregion

        #region 帮助类

        /// <summary>
        /// 医嘱帮助类
        /// </summary>
        protected Neusoft.FrameWork.Public.ObjectHelper orderHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper frequencyHelper;

        /// <summary>
        /// 科室帮助类
        /// </summary>
        protected Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 常数管理帮助类：职级和诊查费项目对照
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper diagFeeConstHelper = new Neusoft.FrameWork.Public.ObjectHelper();

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
        /// 诊断管理
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase diagnoseMgr = new Neusoft.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();

        /// <summary>
        /// 医嘱业务层
        /// </summary>
        protected Neusoft.HISFC.BizLogic.Order.OutPatient.Order OrderManagement = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee feeManagement = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 非药品业务
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee itemManagement = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 医保接口
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Interface interfaceMgr = new Neusoft.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// 存放医保对照项目
        /// </summary>
        Hashtable hsCompareItems = null;

        /// <summary>
        /// 是否显示医保对照标记
        /// </summary>
        private bool isShowPactCompareFlag = true;

        /// <summary>
        /// 非药品组合项目业务
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee undrugztManager = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 综合管理业务层
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 排版管理
        /// </summary>
        private Neusoft.HISFC.BizLogic.Registration.Schema schemgManager = new Neusoft.HISFC.BizLogic.Registration.Schema();

        /// <summary>
        /// 住院入出转
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.RADT radtManger = new Neusoft.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 挂号业务层
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regManagement = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 分诊业务
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager managerAssign = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 药品业务
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 控制参数管理
        /// </summary>
        protected Neusoft.FrameWork.Management.ControlParam controlMgr = new Neusoft.FrameWork.Management.ControlParam();

        /// <summary>
        /// 常数管理
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Constant conManager = new Neusoft.HISFC.BizLogic.Manager.Constant();

        private Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientFee = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 诊断管理类 
        /// </summary>
        private Neusoft.HISFC.BizLogic.HealthRecord.Diagnose myDiagnose = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 账户管理
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Account accountMgr = new Neusoft.HISFC.BizLogic.Fee.Account();

        #endregion

        #region 作废
        /// <summary>
        /// 是否打印预览处方
        /// </summary>
        //public bool bPrintViewRecipe = false;

        //public event EventButtonHandler OrderCanOperatorChanged;	//医嘱是否可以点击手术申请

        //public delegate void OrderQtyChangedHandler(Neusoft.HISFC.Models.Registration.Register rInfo, Neusoft.FrameWork.Management.Transaction trans);
        //public event OrderQtyChangedHandler SetFeeDisplay;

        /// <summary>
        /// 是否在开立状态，用作避免多次查询数据库
        /// </summary>
        protected bool bTempVar = false;

        private string varCombID = "";//临时的组合号变量

        private string varTempUsageID = "zuowy";//临时用法
        private string varOrderUsageID = "maokb";//医嘱用法
        //protected bool bCanAddOrder = true;//是否允许甲乙类和自费项目开立在同一处方 1 Yes 0 No

        /// <summary>
        /// 是否修改过医嘱
        /// </summary>
        //private bool isEdit = false;

        /// <summary>
        /// 是否允许中药和和西药在同一处方 1 可以 0 不可以
        /// </summary>
        //protected bool bCanInSameRecipe = true;

        #endregion
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
        /// 是否通过seeno计算费用
        /// </summary>
        public bool IsShowHardDrug
        {
            get
            {
                return isShowHardDrug;
            }
            set
            {
                isShowHardDrug = value;
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
        public Neusoft.HISFC.Models.Registration.Register Patient
        {
            get
            {
                return this.currentPatientInfo;
            }
            set
            {
                if (value != null)
                {
                    //不是同一个人，则清空默认天数信息等
                    if (value.ID != currentPatientInfo.ID)
                    {
                        this.ucOutPatientItemSelect1.ClearDays();
                    }

                    currentPatientInfo = value;
                    if (this.GetRecentPatientInfo() == 1)
                    {
                        value = currentPatientInfo;

                        if (currentPatientInfo != null)
                        {
                            if (currentPatientInfo.Pact != null)
                            {
                                currentPatientInfo.Pact = pactHelper.GetObjectFromID(currentPatientInfo.Pact.ID) as Neusoft.HISFC.Models.Base.PactInfo;
                            }
                            this.ucOutPatientItemSelect1.PatientInfo = currentPatientInfo;

                            if (this.isAccountMode)
                            {
                                decimal vacancy = 0;
                                int rev = accountMgr.GetVacancy(currentPatientInfo.PID.CardNO, ref vacancy);
                                if (rev == -1)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("获取账户余额出错：" + accountMgr.Err);
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

                            this.QueryOrder();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当前诊台
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Neusoft.FrameWork.Models.NeuObject CurrentRoom
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
        protected Neusoft.FrameWork.Models.NeuObject reciptDept = null;

        /// <summary>
        /// 设置开方科室
        /// </summary>
        [DefaultValue(null)]
        public void SetReciptDept(Neusoft.FrameWork.Models.NeuObject value)
        {
            this.reciptDept = value;
        }

        /// <summary>
        /// 获取开方科室
        /// </summary>
        /// <returns></returns>
        public Neusoft.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.reciptDept == null)
                {
                    //如果有排班信息，去排班科室作为开立科室 {231D1A80-6BF6-413f-8BBF-727DC2BF47D9}
                    Neusoft.HISFC.Models.Registration.Schema schema = this.regManagement.GetSchema(GetReciptDoct().ID, this.OrderManagement.GetDateTimeFromSysDateTime());
                    if (schema != null && schema.Templet.Dept.ID != "")
                    {
                        this.reciptDept = schema.Templet.Dept.Clone();
                    }
                    //没有排版取登陆科室作为开立科室
                    else
                    {
                        this.reciptDept = ((Neusoft.HISFC.Models.Base.Employee)this.GetReciptDoct()).Dept.Clone(); //开立科室
                    }
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
        protected Neusoft.FrameWork.Models.NeuObject reciptDoct = null;

        /// <summary>
        /// 当前开立医生
        /// </summary>
        public void SetReciptDoc(Neusoft.FrameWork.Models.NeuObject value)
        {
            this.reciptDoct = value;

        }

        /// <summary>
        /// 获得开方医生
        /// </summary>
        /// <returns></returns>
        public Neusoft.FrameWork.Models.NeuObject GetReciptDoct()
        {
            try
            {
                if (this.reciptDoct == null)
                    this.reciptDoct = OrderManagement.Operator.Clone();
            }
            catch { }
            return this.reciptDoct;
        }

        /// <summary>
        /// 患者看诊科室,有别于挂号科室
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject SeeDept = null;

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
        /// 是否历时医嘱状态
        /// </summary>
        public bool bOrderHistory = false;

        #endregion

        #region 初始化

        /// <summary>
        /// 窗口Loading
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode) return;
            if (Neusoft.FrameWork.Management.Connection.Operator.ID == "") return;

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
            //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
            InitDirectFee();

            //this.isDoctRegistered = Neusoft.FrameWork.Function.NConvert.ToBoolean(controlMgr.QueryControlerInfo("200030"));

            //this.pValue = controlMgr.QueryControlerInfo("200018");
            this.isUseNurseArray = Classes.Function.IsUseNurseArray();

            InitDealSubJob();

            this.GetSupplyItem();

            ArrayList alPact = this.managerIntegrate.QueryPactUnitOutPatient();
            if (alPact == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取合同单位信息错误：" + managerIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.cmbPact.AddItems(alPact);
            pactHelper.ArrayObject = alPact;

            if (Classes.Function.usageHelper == null)
            {
                ArrayList alUsage = this.conManager.GetList("USAGE");
                Classes.Function.usageHelper = new Neusoft.FrameWork.Public.ObjectHelper(alUsage);
            }

            try
            {
                #region 获取控制参数

                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlParamManager = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

                this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200019");
                this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200020");
                this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200005");
                if (this.tempControler != null)
                {
                    this.isDealEmrOrderSubtblSpecially = Neusoft.FrameWork.Function.NConvert.ToBoolean(((Neusoft.HISFC.Models.Base.ControlParam)tempControler).ControlerValue);
                }
                this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200006");
                if (this.tempControler != null)
                {
                    this.emrSubtblUsage = ((Neusoft.HISFC.Models.Base.ControlParam)tempControler).ControlerValue;
                }
                this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200007");
                if (this.tempControler != null)
                {
                    this.ULOrderUsage = ((Neusoft.HISFC.Models.Base.ControlParam)tempControler).ControlerValue;
                }
                this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200022");
                if (this.tempControler != null)
                {
                    this.validRegDays = Neusoft.FrameWork.Function.NConvert.ToInt32(((Neusoft.HISFC.Models.Base.ControlParam)tempControler).ControlerValue);
                }
                this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200000");
                if (this.tempControler != null)
                {
                    this.isDealSubtbl = Neusoft.FrameWork.Function.NConvert.ToBoolean(((Neusoft.HISFC.Models.Base.ControlParam)tempControler).ControlerValue);
                }
                this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200023");

                isSaveOrderHistory = controlParamManager.GetControlParam<bool>("200021", true, false);

                isAllowChangePactInfo = controlParamManager.GetControlParam<bool>("HNMZ25", true, false);

                if (!isAllowChangePactInfo)
                {
                    this.pnPactInfo.Visible = false;
                }

                dealSublMode = controlParamManager.GetControlParam<int>("HNMZ26", true, 0);

                //this.enabledPacs = controlParamManager.GetControlParam<bool>("200202");

                //皮试处理模式
                //this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200201");
                //if (this.tempControler != null)
                //{
                //    this.hypotestMode = ((Neusoft.HISFC.Models.Base.ControlParam)tempControler).ControlerValue.ToString();
                //}
                hypotestMode = this.ctrlMgr.GetControlParam<string>("200201", true, "1");

                this.isCanModifiedInjectNum = this.ctrlMgr.GetControlParam<bool>("MZ5001", true, true);

                //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                this.ucOutPatientItemSelect1.GetMaxSubCombNo += new ucOutPatientItemSelect.GetMaxSubCombNoEvent(GetMaxSubCombNo);
                this.ucOutPatientItemSelect1.GetSameSubCombNoOrder += new ucOutPatientItemSelect.GetSameSubCombNoOrderEvent(ucOutPatientItemSelect1_GetSameSortIDOrder);

                isJudgeDiagnose = this.ctrlMgr.GetControlParam<bool>("200302", false, false);

                //isShunDeFuYou = this.ctrlMgr.GetControlParam<bool>("MZ0090", false, false);
                isShowPactCompareFlag = this.ctrlMgr.GetControlParam<bool>("HNMZ27", true, false);

                isAutoPrintRecipe = this.ctrlMgr.GetControlParam<Int32>("HNMZ23", true, 0);

                this.isCheckDrugStock = ctrlMgr.GetControlParam<int>("200001", false, 0);

                emplFreeRegType = this.ctrlMgr.GetControlParam<int>("HNMZ24", true, 0);

                isCountFeeBySeeNo = this.ctrlMgr.GetControlParam<bool>("HNMZ98", true, false);

                isShowHardDrug = this.ctrlMgr.GetControlParam<bool>("HNMZ89", true, true);

                isShowRepeatItemInScreen = this.ctrlMgr.GetControlParam<bool>("HNMZ96", true, false);

                isShowSpecsAndPrice = this.ctrlMgr.GetControlParam<int>("HNMZ31", true, 3);

                isChangeAllSelect = this.ctrlMgr.GetControlParam<string>("HNMZ32", true, "-1");

                isAddRegSubBeforeAddOrder = ctrlMgr.GetControlParam<bool>("HNMZ33", true, false);

                isCalculatSubl = ctrlMgr.GetControlParam<bool>("HNMZ50", true, false);
                //this.isAccountTerimal = controlIntegrate.GetControlParam<bool>("S00031", true, false);

                //{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                this.isPreUpdateStockinfo = this.controlIntegrate.GetControlParam<bool>(Neusoft.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Pre_Out, false, true);//是否预扣库存
                if (this.isPreUpdateStockinfo)
                {
                    bool isFeeUpdatePre = controlIntegrate.GetControlParam<bool>("P01015");//收费时是否预扣 true收费时预扣 false门诊医生预扣
                    if (!isFeeUpdatePre)
                    {
                        this.isPreUpdateStockinfo = true;
                    }
                }
                #endregion

                try
                {
                    ArrayList al = managerAssign.GetConstantList("DoceOnceLimit");
                    if (al != null)
                    {
                        foreach (Neusoft.HISFC.Models.Base.Const con in al)
                        {
                            doceOnceLimit = Neusoft.FrameWork.Function.NConvert.ToDecimal(con.Name);
                        }
                    }
                }
                catch
                {
                    doceOnceLimit = -1;
                }

                try
                {
                    //获得所有科室
                    ArrayList alDepts = this.managerIntegrate.GetDepartment();
                    this.deptHelper.ArrayObject = alDepts;

                    //获得所以频次信息 用于向合理用药系统传送医嘱频次               
                    ArrayList alFrequency = this.managerIntegrate.QuereyFrequencyList();
                    if (alFrequency != null)
                        frequencyHelper = new Neusoft.FrameWork.Public.ObjectHelper(alFrequency);

                    ArrayList alTemp = new ArrayList();
                    alTemp = this.managerIntegrate.GetConstantList("LOCAL_DOCLEVEL_DIG");
                    if (alTemp == null)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(this.managerIntegrate.Err);
                        return;
                    }
                    this.diagFeeConstHelper.ArrayObject = alTemp;
                }
                catch (Exception ex)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                    return;
                }

                this.ucOutPatientItemSelect1.OrderChanged += new ItemSelectedDelegate(ucItemSelect1_OrderChanged);

                this.neuSpread1.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating;
                this.neuSpread1.Sheets[0].DataAutoSizeColumns = false;

                this.neuSpread1.Sheets[0].DataAutoCellTypes = false;

                this.neuSpread1.Sheets[0].GrayAreaBackColor = Color.White;

                this.neuSpread1.Sheets[0].RowHeader.Columns.Get(0).Width = 30;

                //this.neuSpread1.Sheets[0].RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;

                this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised);
                this.neuSpread1_Sheet1.RowHeader.DefaultStyle.CellType = new FarPoint.Win.Spread.CellType.TextCellType();


                this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellClick);
                this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);

                //初始化PACS{3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}
                //if (this.enabledPacs)
                //{
                //    this.InitPacsInterface();
                //}
                ////this.OrderType = Neusoft.HISFC.Models.Order.EnumType.SHORT;
                this.neuSpread1.ActiveSheetIndex = 0;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }

            base.OnStatusBarInfo(null, "(绿色：新开)(蓝色：收费)");

            Classes.Function.SethsUsageAndSub();

            #region 新增接口

            if (IAfterSaveOrder == null)
            {
                IAfterSaveOrder = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.ISaveOrder)) as Neusoft.HISFC.BizProcess.Interface.Order.ISaveOrder;
            }

            if (IBeforeSaveOrder == null)
            {
                IBeforeSaveOrder = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder)) as Neusoft.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder;
            }

            if (IBeforeAddItem == null)
            {
                IBeforeAddItem = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddItem)) as Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddItem;
            }

            if (IBeforeAddOrder == null)
            {
                IBeforeAddOrder = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddOrder)) as Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddOrder;
            }

            #endregion

            #region 合理用药

            this.InitReasonableMedicine();

            if (this.IReasonableMedicine != null)
            {
                int iReturn = 0;
                Employee empl = FrameWork.Management.Connection.Operator as Employee;
                iReturn = this.IReasonableMedicine.PassInit(empl, empl.Dept, "10");
                if (iReturn == -1)
                {
                    this.EnabledPass = false;
                    ucOutPatientItemSelect1.MessageBoxShow(IReasonableMedicine.Err);
                }
                if (iReturn == 0)
                {
                    this.EnabledPass = false;
                    //ucOutPatientItemSelect1.MessageBoxShow("合理用药服务器未启动,不能进行用药审查,请重新登陆工作站！");
                }
            }

            #endregion
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

        }

        //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
        /// <summary>
        /// 初始化直接收费接口
        /// </summary>
        private void InitDirectFee()
        {
            if (IDoctFee == null)
            {
                IDoctFee = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee)) as Neusoft.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee;
            }
        }

        /// <summary>
        /// 辅材处理接口
        /// </summary>
        private void InitDealSubJob()
        {
            if (IDealSubjob == null)
            {
                IDealSubjob = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob)) as Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob;
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

        int[] iColumns;
        int[] iColumnWidth;
        bool[] iColumnVisible;

        /// <summary>
        /// 设置列属性
        /// </summary>
        private void SetColumnProperty()
        {
            if (System.IO.File.Exists(SetingFileName))
            {
                if (iColumnWidth == null || iColumnWidth.Length <= 0)
                {
                    Neusoft.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1.Sheets[0], SetingFileName);

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
                Neusoft.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[0], SetingFileName);
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

                if (ColumnsSet[i] == "每次用量" || ColumnsSet[i] == "总量")
                {
                    this.neuSpread1.Sheets[k].Columns[i].ForeColor = Color.Red;
                    this.neuSpread1.Sheets[k].Columns[i].Font = new Font(this.neuSpread1.Font.FontFamily.Name, this.neuSpread1.Font.Size, FontStyle.Bold, this.neuSpread1.Font.Unit, this.neuSpread1.Font.GdiCharSet);
                }
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
                                          "每次用量",     //11
                                          "单位",         //12
                                          "频次编码",     //14
                                          "频次名称",     //15
                                          "付数/天数",    //13
                                          "用法编码",     //16
                                          "用法名称",     //17
                                          "总量",         //9
                                          "总量单位",     //10
                                          "院注次数",     //18
                                          "规格",
                                          "单价",
                                          "金额",
                                          "开始时间",     //19
                                          "执行科室编码", //20
                                          "执行科室",     //21
                                          "加急",         //22
                                          "备注",         //23
                                          "录入人编码",   //24
                                          "录入人",       //25
                                          "开立科室",     //26
                                          "开立时间",     //27
                                          "停止时间",     //28
                                          "停止人编码",   //29
                                          "停止人",       //30
                                          "顺序号",       //31
                                          "开立医生",     //32
                                          "检查部位",     //34
                                          "样本类型",     //35
                                          "扣库科室编码", //36
                                          "扣库科室",     //37
                                          "皮试代码",     //38
                                          "皮试"          //39
                                      };

        #endregion

        /// <summary>
        /// 初始化医嘱信息变更接口实例{48E6BB8C-9EF0-48a4-9586-05279B12624D}
        /// </summary>
        protected void InitAlterOrderInstance()
        {
            if (this.IAlterOrderInstance == null)
            {
                this.IAlterOrderInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(Neusoft.HISFC.BizProcess.Interface.IAlterOrder)) as Neusoft.HISFC.BizProcess.Interface.IAlterOrder;
            }

            //TestAlterInsterface t = new TestAlterInsterface();
            //this.IAlterOrderInstance = t as Neusoft.HISFC.BizProcess.Integrate.IAlterOrder;
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
            foreach (object obj in list)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order order = obj as Neusoft.HISFC.Models.Order.OutPatient.Order;

                this.dtOrder.Tables[0].Rows.Add(AddObjectToRow(order, this.dtOrder.Tables[0]));

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
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            try
            {
                order = obj as Neusoft.HISFC.Models.Order.OutPatient.Order;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return null;
            }

            if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Pharmacy.Item))
            {
                Neusoft.HISFC.Models.Pharmacy.Item objItem = order.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                row[GetColumnIndexFromName("主药")] = Neusoft.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug);//5
                row[GetColumnIndexFromName("每次用量")] = string.IsNullOrEmpty(order.DoseOnceDisplay) ? order.DoseOnce.ToString() : order.DoseOnceDisplay;//9
                row[GetColumnIndexFromName("单位")] = objItem.DoseUnit;
                //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                row[GetColumnIndexFromName("付数/天数")] = order.HerbalQty;//11
            }
            else if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Fee.Item.Undrug))
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

            string specs = string.IsNullOrEmpty(order.Item.Specs) ? "" : ("[" + order.Item.Specs + "] ");
            string price = "";
            if (order.Item.Price > 0)
            {
                if (order.MinunitFlag == "1") //最小单位判断
                {
                    price = "[" + (order.Item.Price / order.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + order.Item.PriceUnit + "]";//6
                }
                else
                {
                    price = "[" + order.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + order.Item.PriceUnit + "]";//6
                }
            }
            else if (order.Unit == "[复合项]")
            {
                if (order.MinunitFlag == "1")
                {
                    price = "[" + (OutPatient.Classes.Function.GetUndrugZtPrice(order.Item.ID) / order.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + order.Item.PriceUnit + "]";//6
                }
                else
                {
                    price = "[" + OutPatient.Classes.Function.GetUndrugZtPrice(order.Item.ID).ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + order.Item.PriceUnit + "]";//6
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

            row[GetColumnIndexFromName("医嘱名称")] = "[组:" + order.SubCombNO.ToString() + "]" + order.Item.Name + specs + price;
            #endregion

            //医保用药-知情同意书
            if (order.IsPermission)
            {
                row[GetColumnIndexFromName("医嘱名称")] = "【√】" + row[GetColumnIndexFromName("医嘱名称")];
            }

            this.ValidNewOrder(order);
            row[GetColumnIndexFromName("总量")] = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
            row[GetColumnIndexFromName("总量单位")] = order.Unit;//8
            row[GetColumnIndexFromName("频次编码")] = order.Frequency.ID;
            row[GetColumnIndexFromName("频次名称")] = order.Frequency.Name;
            row[GetColumnIndexFromName("用法编码")] = order.Usage.ID;
            row[GetColumnIndexFromName("用法名称")] = order.Usage.Name;//15
            row[GetColumnIndexFromName("开始时间")] = order.BeginTime;
            row[GetColumnIndexFromName("执行科室编码")] = order.ExeDept.ID;

            row[GetColumnIndexFromName("执行科室")] = order.ExeDept.Name;
            row[GetColumnIndexFromName("加急")] = order.IsEmergency;
            row[GetColumnIndexFromName("检查部位")] = order.CheckPartRecord;
            row[GetColumnIndexFromName("样本类型")] = order.Sample;
            row[GetColumnIndexFromName("扣库科室编码")] = order.StockDept.ID;
            row[GetColumnIndexFromName("扣库科室")] = order.StockDept.Name;
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
            row[GetColumnIndexFromName("皮试")] = this.OrderManagement.TransHypotest(order.HypoTest);
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
                Neusoft.HISFC.Models.Order.OutPatient.Order order = al[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

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

            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            try
            {
                order = ((Neusoft.HISFC.Models.Order.OutPatient.Order)obj);//.Clone();
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("Clone出错！" + ex.Message);
                this.dirty = false;
                return;
            }

            if (this.bTempVar)
            {
                # region 根据用法自动弹出添加院注
                try
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(rowIndex, SheetIndex);

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

                                    if (Classes.Function.CheckIsInjectUsage(order.Usage.ID))
                                    {
                                        ArrayList al = (ArrayList)Classes.Function.HsUsageAndSub[order.Usage.ID];//this.feemanagement.GetInjectInfoByUsage(order.Usage.ID);
                                        if (al != null && al.Count > 0)
                                        {
                                            this.AddInjectNum(order, this.isCanModifiedInjectNum);
                                        }
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

            if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Pharmacy.Item))//药品
            {
                Neusoft.HISFC.Models.Pharmacy.Item objItem = order.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("每次用量")].Text = string.IsNullOrEmpty(order.DoseOnceDisplay) ? order.DoseOnce.ToString() : order.DoseOnceDisplay;

                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("付数/天数")].Text = order.HerbalQty.ToString();//11

                if (order.DoseUnit == null || order.DoseUnit == "") order.DoseUnit = objItem.DoseUnit;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单位")].Text = order.DoseUnit;

                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = order.Unit;//8

            }
            else if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Fee.Item.Undrug)) //非药品
            {
                Neusoft.HISFC.Models.Fee.Item.Undrug objItem = order.Item as Neusoft.HISFC.Models.Fee.Item.Undrug;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单位")].Text = "";//剂量单位
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = order.Unit;//8
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("付数/天数")].Text = order.HerbalQty.ToString();//11
            }
            else if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Base.Item))
            {
                Neusoft.HISFC.Models.Fee.Item.Undrug objItem = order.Item as Neusoft.HISFC.Models.Fee.Item.Undrug;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单位")].Text = "";//剂量单位
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("总量单位")].Text = order.Unit;//8
            }
            this.ValidNewOrder(order); //填写信息

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("警")].Text = "";     //0

            if (order.NurseStation.Memo != null && order.NurseStation.Memo.Length > 0)
            {
                //合理用药相关（暂时未改屏蔽）
                //this.AddWarnPicturn(i, 0, Neusoft.FrameWork.Function.NConvert.ToInt32(order.NurseStation.Memo));
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

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("医嘱名称")].Text = "[组:" + order.SubCombNO.ToString() + "]" + order.Item.Name.ToString();

            string specs = string.IsNullOrEmpty(order.Item.Specs) ? "" : ("[" + order.Item.Specs + "] ");
            string price = "";
            if (order.Item.Price > 0)
            {
                if (order.MinunitFlag == "1"&&IsDesignMode) //最小单位判断
                {
                    price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(order.Item.Price / order.Item.PackQty, 2) + "元/" + order.Item.PriceUnit;
                }
                else if(order.MinunitFlag == "1"&&!IsDesignMode)
                {
                    //如果在组套管理界面转换成药品实体然后赋值
                    if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Pharmacy.Item))
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item baseItem = order.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                        if (!string.IsNullOrEmpty(baseItem.MinUnit))
                        {
                            price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(baseItem.Price / baseItem.PackQty, 2) + "元/" + baseItem.MinUnit;
                        }
                        else
                        {
                            price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(baseItem.Price / order.Item.PackQty, 2) + "元/" + order.Item.PriceUnit;
                        }
                    }
                    else
                    {
                        price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(order.Item.Price / order.Item.PackQty, 2) + "元/" + order.Item.PriceUnit;
                    }
                }
                else
                {
                    price = order.Item.Price.ToString() + "元/" + order.Item.PriceUnit;//6
                }
            }
            else if (order.Unit == "[复合项]")
            {
                if (order.MinunitFlag == "1")
                {
                    price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(OutPatient.Classes.Function.GetUndrugZtPrice(order.Item.ID) / order.Item.PackQty, 2) + "元/" + order.Item.PriceUnit;//6
                }
                else
                {
                    price = OutPatient.Classes.Function.GetUndrugZtPrice(order.Item.ID).ToString() + "元/" + order.Item.PriceUnit;//6
                }
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("单价")].Text = price;

            if (!string.IsNullOrEmpty(price))
            {
                price = "[" + price + "]";
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

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("医嘱名称")].Text += specs + price;

            #endregion

            //医保患者知情同意书
            if (order.IsPermission)
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("医嘱名称")].Text = order.SubCombNO.ToString() + "【√】" + this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("医嘱名称")].Text;
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("规格")].Text = order.Item.Specs;


            string totCost = "";
            if (order.MinunitFlag == "1")//开立最小单位 
            {
                totCost = (order.Qty * order.Item.Price / order.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
            }
            else
            {
                totCost = (order.Qty * order.Item.Price).ToString("F4").TrimEnd('0').TrimEnd('.');
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("金额")].Text = totCost;

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
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("开始时间")].Value = order.BeginTime;//开始时间
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("开立时间")].Value = order.MOTime;//开立时间


            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("执行科室编码")].Text = order.ExeDept.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("执行科室")].Text = order.ExeDept.Name;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("加急")].Value = order.IsEmergency;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("检查部位")].Value = order.CheckPartRecord;//检查部位
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("样本类型")].Value = order.Sample.Name;//样本类型
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("扣库科室编码")].Value = order.StockDept.ID;//扣库科室
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("扣库科室")].Value = order.StockDept.Name;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("备注")].Text = order.Memo;//20
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("录入人编码")].Text = order.Oper.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("录入人")].Text = order.Oper.Name;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("开立医生")].Text = order.ReciptDoctor.Name;//开立医生
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("开立科室")].Text = order.ReciptDept.Name;//开立科室

            if (order.EndTime != DateTime.MinValue)
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("停止时间")].Value = order.EndTime;//停止时间 25
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("停止人编码")].Text = order.DCOper.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("停止人")].Text = order.DCOper.Name;

            if (order.SortID == 0)
            {
                order.SortID = MaxSort + 1;
                MaxSort = MaxSort + 1;
            }
            else
            {
                if (order.SortID > MaxSort)
                {
                    MaxSort = order.SortID;
                }
            }
            if (order.Frequency.Usage.ID == "")
            {
                order.Frequency.Usage = order.Usage; //用法付给
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("顺序号")].Value = order.SortID;//28
            if (!this.EditGroup)
            {
                if (this.currentPatientInfo.Pact.PayKind.ID == "02")//广州医保-显示费用比率
                {
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
                    //if (order.Item.Price > 0 && order.OrderType.IsCharge) this.neuSpread1.Sheets[SheetIndex].RowHeader.Cells[i, 0].Text = Neusoft.HISFC.Components.Common.Classes.Function.ShowItemFlag(order.Item);
                }
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("皮试代码")].Value = order.HypoTest;//28
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("皮试")].Value = this.OrderManagement.TransHypotest(order.HypoTest);//28
            this.neuSpread1.Sheets[SheetIndex].Rows[rowIndex].Tag = order;

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
        public int GetMaxSubCombNo(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            int maxNum = 0;
            foreach (FarPoint.Win.Spread.Row row in this.neuSpread1.Sheets[0].Rows)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(row.Index, 0).Clone();
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

        /// <summary>
        /// 获得相同组号医嘱
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int ucOutPatientItemSelect1_GetSameSortIDOrder(int sortID, ref Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            try
            {
                for (int i = this.neuSpread1.ActiveSheet.RowCount - 1; i >= 0; i--)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    //if (temp.SortID.ToString().Substring(0, temp.SortID.ToString().Length - 2) == sortID)
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

                Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp = GetObjectFromFarPoint(row, SheetIndex);
                if (orderTemp == null)
                {
                    return;
                }

                if (Components.Common.Classes.Function.HsItemPactInfo != null
                    && Components.Common.Classes.Function.HsItemPactInfo.Contains(Patient.Pact.ID + orderTemp.Item.ID))
                {
                    string ss = Neusoft.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(Components.Common.Classes.Function.HsItemPactInfo[Patient.Pact.ID + orderTemp.Item.ID].ToString());
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

                //附材项目斜体显示
                if (orderTemp.IsSubtbl)
                {
                    //this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("医嘱名称")].Font = new Font(this.neuSpread1.Font.FontFamily.Name, this.neuSpread1.Font.Size, FontStyle.Italic, this.neuSpread1.Font.Unit, this.neuSpread1.Font.GdiCharSet);
                    this.neuSpread1.Sheets[SheetIndex].Rows[row].BackColor = Color.Silver;
                }
                else
                {
                    //this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("医嘱名称")].Font = new Font(this.neuSpread1.Font.FontFamily.Name, this.neuSpread1.Font.Size, FontStyle.Regular, this.neuSpread1.Font.Unit, this.neuSpread1.Font.GdiCharSet);
                    this.neuSpread1.Sheets[SheetIndex].Rows[row].BackColor = Color.White;
                }

                if (isShowPactCompareFlag && this.currentPatientInfo != null && this.currentPatientInfo.Pact != null)
                {
                    if (hsCompareItems == null)
                    {
                        hsCompareItems = new Hashtable();
                    }
                    Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;

                    if (interfaceMgr.GetCompareSingleItem(this.currentPatientInfo.Pact.ID, orderTemp.Item.ID, ref compareItem) == -1)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("获取医保对照项目失败：" + interfaceMgr.Err);
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
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }

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

            hsPhaUserCode = new Hashtable();
            if (IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
            {
                //this.IReasonableMedicine.ShowFloatWin(false);
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询医嘱,请稍候!");
            Application.DoEvents();

            this.hsOrder.Clear();

            //查询所有医嘱类型
            if (this.currentPatientInfo.DoctorInfo.SeeNO == 0)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = -1;
            }

            //ArrayList al = OrderManagement.QueryOrder(this.currentPatientInfo.DoctorInfo.SeeNO.ToString());
            ArrayList al = OrderManagement.QueryOrder(currentPatientInfo.ID,currentPatientInfo.DoctorInfo.SeeNO.ToString());

            if (al == null)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(OrderManagement.Err);
                return;
            }

            if (this.IsDesignMode)
            {
                isShowFeeWarning = false;
            }
            else if (!this.IsDesignMode && !this.isShowFeeWarning)
            {
                isShowFeeWarning = false;
            }
            else
            {
                isShowFeeWarning = true;
            }
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp in al)
            {
                if (orderTemp != null)
                {
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

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在显示医嘱,请稍候!");
            Application.DoEvents();

            if (this.IsDesignMode)
            {
                tooltip.SetToolTip(this.neuSpread1, "开立医嘱");
                tooltip.Active = true;
                this.bTempVar = true;
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
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
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

                    this.neuSpread1.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

                    this.RefreshCombo();
                    this.RefreshOrderState(1);

                }
                catch (Exception ex)
                {
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                }
            }

            //this.SetOrderFeeDisplay(true);

            this.hsOrder.Clear();
            this.neuSpread1.ActiveSheet.ClearSelection();

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
        private void ValidNewOrder(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            if (order.ReciptDept.Name == "" && order.ReciptDept.ID != "") order.ReciptDept.Name = this.deptHelper.GetName(order.ReciptDept.ID);
            if (order.StockDept.Name == "" && order.StockDept.ID != "") order.StockDept.Name = this.deptHelper.GetName(order.StockDept.ID);
            if (order.BeginTime == DateTime.MinValue) order.BeginTime = this.OrderManagement.GetDateTimeFromSysDateTime();
            if (order.MOTime == DateTime.MinValue) order.MOTime = order.BeginTime;
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
                    order.InDept = this.currentPatientInfo.DoctorInfo.Templet.Dept;
            }
            if (order.ExeDept == null || order.ExeDept.ID == "")
            {
                //更改执行科室为患者科室
                if (!this.EditGroup)
                    order.ExeDept = this.GetReciptDept().Clone();//{56D98B49-A27E-487f-B331-0B9CDB04D4ED}
                else
                    order.ExeDept = ((Neusoft.HISFC.Models.Base.Employee)this.OrderManagement.Operator).Dept.Clone();
            }
            if (order.ExeDept.Name == "" && order.ExeDept.ID != "")
                order.ExeDept.Name = this.deptHelper.GetName(order.ExeDept.ID);
            //开单医生
            if (order.ReciptDoctor == null || order.ReciptDoctor.ID == "")
                order.ReciptDoctor = this.GetReciptDoct().Clone();
            //开单科室
            if (order.ReciptDept == null || order.ReciptDept.ID == "")
                order.ReciptDept = this.GetReciptDept().Clone();

            if (order.Oper.ID == null || order.Oper.ID == "")
            {
                order.Oper.ID = this.OrderManagement.Operator.ID;
                order.Oper.Name = this.OrderManagement.Operator.Name;
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
            #region 选择
            //每次选择变化前清空数据显示
            this.ucOutPatientItemSelect1.Clear(false);

            #region old add by liuww 2012-06-07
            ////新开立 才能更改
            //if (int.Parse(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("医嘱状态")].Text) == 0)
            //{
            //    //设置为当前行
            //    this.ucOutPatientItemSelect1.CurrentRow = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            //    this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            //    this.currentOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
            //    this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;
            //    //设置组合行选择
            //    if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != "" && this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != null)// && this.IsDesignMode)
            //    {
            //        int comboNum = 0;//获得当前选择行数
            //        for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            //        {
            //            string strComboNo = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Combo.ID;
            //            if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo
            //                //&& i != this.neuSpread1.ActiveSheet.ActiveRowIndex
            //                )
            //            {
            //                this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, 1);
            //                comboNum++;
            //            }
            //        }
            //        if (comboNum == 0)
            //        {
            //            //只有一行
            //            if (OrderCanCancelComboChanged != null)
            //                this.OrderCanCancelComboChanged(false);//不能取消组合
            //        }
            //        else
            //        {
            //            if (OrderCanCancelComboChanged != null)
            //                this.OrderCanCancelComboChanged(true);//可以取消组合
            //        }
            //    }

            //    if (OrderCanSetCheckChanged != null)
            //        this.OrderCanSetCheckChanged(false);//打印检查申请单失效
            //}
            #endregion

            if (int.Parse(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("医嘱状态")].Text) == 0)
            {
                #region new add by liuww 2012-06-07
                this.currentOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
                this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;

                int comboNum = 0;//获得当前选择行数
                
                //设置组合行选择
                if (this.currentOrder.Combo.ID != ""
                    && this.currentOrder.Combo.ID != null)//&& this.IsDesignMode)
                {

                    comboNum = this.SelectionAllChanged();


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
                }

                if (OrderCanSetCheckChanged != null)
                {
                    this.OrderCanSetCheckChanged(false);//打印检查申请单失效
                }
                #endregion
            }
            else
            {
                this.ActiveRowIndex = -1;
            }
            #endregion
        }



        #region
        /// <summary>
        /// 选中组合号
        /// </summary>
        /// <returns></returns>
        private int SelectionAllChanged()
        {
            int comboNum = 0;
            //设置为当前行
            this.ucOutPatientItemSelect1.CurrentRow = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;

            if (this.currentOrder.Combo.ID != ""
                  && this.currentOrder.Combo.ID != null)//&& this.IsDesignMode)
            {
                //获得当前选择行数
                ///向下寻找
                for (int i = this.ActiveRowIndex; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    string strComboNo = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Combo.ID;

                    //string strComboNo = this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("组合号")].Text

                    if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo) //&& i != this.neuSpread1.ActiveSheet.ActiveRowIndex
                    {
                        this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, 1);
                        comboNum++;
                    }
                    else
                    {
                        break;
                    }

                }

                ///向上寻找
                for (int i = this.ActiveRowIndex; i >= 0; i--)
                {
                    string strComboNo = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Combo.ID;
                    if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo) //&& i != this.neuSpread1.ActiveSheet.ActiveRowIndex
                    {
                        this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, 1);
                        comboNum++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return comboNum;
        }
        #endregion




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
                        iSelectionCount++;
                }

                if (iSelectionCount > 1)
                {
                    string t = "";//组合号 修改成都有组合号
                    int injectNum = 0;//院内注次数
                    int iSort = -1;
                    string time = "";
                    int kk = 0;

                    if (this.ValidComboOrder() == -1)
                        return;//校验组合医嘱

                    //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    int sameSubComb = 0;
                    Neusoft.HISFC.Models.Order.OutPatient.Order ord = null;
                    for (int rowIndex = 0; rowIndex < this.neuSpread1.Sheets[sheetIndex].Rows.Count; rowIndex++)
                    {
                        ord = this.GetObjectFromFarPoint(rowIndex, sheetIndex);
                        ord.SortID = rowIndex + 1;

                        this.neuSpread1.Sheets[sheetIndex].Cells[rowIndex, GetColumnIndexFromName("顺序号")].Text = ord.SortID.ToString();
                        this.neuSpread1.Sheets[sheetIndex].Cells[rowIndex, GetColumnIndexFromName("顺序号")].Value = ord.SortID;

                        if (this.neuSpread1.Sheets[sheetIndex].IsSelected(rowIndex, 0))
                        {
                            if (t == "")
                            {
                                t = ord.Combo.ID;
                                time = ord.Frequency.Time;
                                sameSubComb = ord.SubCombNO;
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
                        else
                        {
                            if (kk > 0)
                            {
                                ord.SortID = ord.SortID + iSelectionCount - kk;
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
                isDecSysClassWhenGetRecipeNO = ctrlMgr.GetControlParam<bool>(Neusoft.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, true, false);
            }

            Neusoft.HISFC.Models.Order.Frequency frequency = null;//频次
            Neusoft.FrameWork.Models.NeuObject usage = null;//用法
            Neusoft.FrameWork.Models.NeuObject exeDept = null;//执行科室
            decimal amount = 0;//数量
            string sysclass = "-1";//类别
            decimal days = 0;//草药付数
            string sample = "";//样本
            decimal injectCount = 0;//院注次数
            string jpNum = "";
            //草药的煎药方式
            string PCCUsage = "";

            ArrayList alItems = new ArrayList();

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    if (o.ID != "")
                    {
                        //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        Neusoft.HISFC.Models.Order.OutPatient.Order tem = this.OrderManagement.QueryOneOrder(this.Patient.ID, o.ID);
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
                                    //Neusoft.HISFC.Models.Base.Const usageObj1 = Classes.Function.usageHelper.GetObjectFromID(o.Usage.ID) as Neusoft.HISFC.Models.Base.Const;
                                    //Neusoft.HISFC.Models.Base.Const usageObj2 = Classes.Function.usageHelper.GetObjectFromID(usage.ID) as Neusoft.HISFC.Models.Base.Const;

                                    //if (!string.IsNullOrEmpty(usageObj1.UserCode) && !string.IsNullOrEmpty(usageObj2.UserCode))
                                    //{
                                    //    if (usageObj1.UserCode.Trim() != usageObj2.UserCode.Trim())
                                    //    {
                                    //        ucOutPatientItemSelect1.MessageBoxShow("用法不同，不可以进行组合！");
                                    //        return -1;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (o.Usage.ID != usage.ID)
                                    //    {
                                    //        ucOutPatientItemSelect1.MessageBoxShow("用法不同，不可以组合用！");
                                    //        return -1;
                                    //    }
                                    //}
                                }
                                else
                                {
                                    if (o.Memo != PCCUsage)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow("煎药方式不同，不可以进行组合！");
                                        return -1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                                //if (o.Usage.ID != usage.ID)
                                //{
                                //    ucOutPatientItemSelect1.MessageBoxShow("用法不同，不可以组合用！");
                                //    return -1;
                                //}
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
                            //if (o.ExtendFlag1 != jpNum)
                            //{
                            //    ucOutPatientItemSelect1.MessageBoxShow("接瓶数不同，不可以组合用！");
                            //    return -1;
                            //}
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
                            if ("PCZ,P".Contains(o.Item.SysClass.ID.ToString()) &&
                                "PCZ,P".Contains(sysclass))
                            {
                                //西药和成药允许组合
                            }
                            else
                            {
                                if (o.Item.SysClass.ID.ToString() != sysclass)
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

            ////if (alItems.Count > 0)
            ////{
            ////    if (!fun.IsComboLab(alItems))
            ////    {
            ////        ucOutPatientItemSelect1.MessageBoxShow("化验项目不符合并管规则,不能组合!", "提示");
            ////        return -1;
            ////    }
            ////}

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
        private void AddInjectNum(Neusoft.HISFC.Models.Order.OutPatient.Order sender, bool isCanModifiedInjectNum)
        {
            //暂时没有限制用法，不管什么项目只要开立了此用法都收辅材

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
                formInputInjectNum.Order.DoseUnit = ((Neusoft.HISFC.Models.Pharmacy.Item)formInputInjectNum.Order.Item).DoseUnit;
            }
            formInputInjectNum.InjectNum = sender.InjectCount;
            if (sender.InjectCount == 0)
            {
                #region {8D4A8FD5-0231-4701-9990-3B2A83503D95}
                //设置默认的院注次数为总量/每次量
                int injectNumTmp = Neusoft.FrameWork.Function.NConvert.ToInt32(sender.Item.Qty * ((Neusoft.HISFC.Models.Pharmacy.Item)sender.Item).BaseDose / sender.DoseOnce);
                formInputInjectNum.InjectNum = injectNumTmp;
                #endregion
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

                Neusoft.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
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
            #region {66C96B33-F371-4796-ADB4-92C66376327A}
            this.RefreshOrderState();
            #endregion

        }

        /// <summary>
        /// 判断发药药房和执行科室
        /// 保存不再调用此方法
        /// </summary>
        /// <param name="pManager"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private int CheckOrderStockDeptAndExeDept(Neusoft.HISFC.BizProcess.Integrate.Pharmacy pManager, ref Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {

            //if (order.Item.IsPharmacy)
            if (order.Item.ItemType == EnumItemType.Drug)
            {
                //下面的FillPharmacyItem 方法会重取药品基本信息
                //Neusoft.HISFC.Models.Pharmacy.Item tempItem = null;

                //tempItem = pManager.GetItem(order.Item.ID);

                //if (tempItem == null || tempItem.IsStop)
                //{
                //    ucOutPatientItemSelect1.MessageBoxShow("药品:" + tempItem.Name + "已经停用", "提示");
                //    return -1;
                //}

                Neusoft.HISFC.Models.Order.OutPatient.Order temp = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                temp.Item = order.Item;
                temp.ReciptDept = order.ReciptDept;
                temp.Patient.Pact = this.currentPatientInfo.Pact;
                temp.Patient.Birthday = this.currentPatientInfo.Birthday;

                #region 屏蔽重新指定默认取药药房 {ABCC78F9-826F-4f03-BB4E-1FDE2A494E1C}

                if (Classes.Function.FillPharmacyItem(pManager, ref temp) == -1)
                {
                    return -1;
                }

                //如果开立单位是包装单位则乘上包装数量，因为库存判断是用最小单位数量判断的{3AD5A0FA-AFE4-41d9-AEDC-8A389D1424C9}
                decimal itemQty = 0;
                if (order.MinunitFlag == "0")
                {
                    itemQty = order.Qty * order.Item.PackQty;
                }
                else
                {
                    itemQty = order.Qty;
                }


                if (Classes.Function.CheckPharmercyItemStock(1, order.Item.ID, order.Item.Name, order.ReciptDept.ID, itemQty, "O") == false)
                {
                    return -1;
                }


                //if (Classes.Function.FillPharmacyItemWithStockDept(pManager, ref temp) == -1)
                //{
                //    return -1;
                //}
                //order.StockDept.ID = temp.StockDept.ID;
                //if (temp.StockDept.Name == "" && temp.StockDept.ID != "")
                //{
                //    order.StockDept.Name = this.GetDeptName(temp.StockDept);
                //}
                #endregion
            }
            return 0;
        }

        //{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
        /// <summary>
        /// 预扣库存
        /// </summary>
        /// <param name="pManager"></param>
        /// <param name="qty">1时，插入；-1时，删除</param>
        /// <returns></returns>
        private int UpdateStockPre(Neusoft.HISFC.BizProcess.Integrate.Pharmacy pManager, Neusoft.HISFC.Models.Order.OutPatient.Order order, decimal qty, ref string errInfo)
        {
            if (order.Item.ItemType == EnumItemType.Drug)
            {
                Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut = new Neusoft.HISFC.Models.Pharmacy.ApplyOut();
                applyOut.ID = order.ID;
                applyOut.StockDept.ID = order.StockDept.ID;
                applyOut.SystemType = "O1";//门诊医嘱类型
                applyOut.Item.ID = order.Item.ID;
                applyOut.Item.Name = order.Item.Name;
                applyOut.Item.Specs = order.Item.Specs;
                applyOut.Operation.ApplyQty = order.Qty;
                applyOut.Days = order.HerbalQty;
                applyOut.Operation.ApplyOper.ID = order.ReciptDoctor.ID;
                applyOut.Operation.ApplyOper.OperTime = order.MOTime;
                applyOut.PatientNO = order.Patient.ID;
                if (pManager.UpdateStockinfoPreOutNum(applyOut, qty, applyOut.Days) == -1)
                {
                    errInfo = pManager.Err;
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 取具有同组合号的医嘱数目，同时在临时数组里删除
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetNumHaveSameComb(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            if (this.alOrderTemp.Count <= 0)
            {
                return 0;
            }

            if (order == null)
            {
                return 0;
            }

            int count = 0;
            ArrayList al = new ArrayList();
            for (int i = 0; i < alOrderTemp.Count; i++)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order temp
                    = alOrderTemp[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                if (temp.Combo.ID == order.Combo.ID)
                {
                    count++;
                    al.Add(temp);
                }
            }

            for (int j = 0; j < al.Count; j++)
            {
                alOrderTemp.Remove(al[j]);
            }

            return count;
        }

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
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item = alOrderAndSub[i] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                for (int j = 0; j < alOrder.Count; j++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                    if (temp.ID == item.Order.ID)
                    {
                        item.Item.MinFee.User03 = "1";
                    }
                }
            }
            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
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
            //return 1;
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction t = new Neusoft.FrameWork.Management.Transaction(OrderManagement.Connection);
            //t.BeginTransaction();
            OrderManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.neuSpread1.Sheets[k].Rows.Count; i++)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order ord = this.GetObjectFromFarPoint(i, k);
                ord.SortID = this.neuSpread1.Sheets[k].Rows.Count - i;
                int iReturn = -1;
                iReturn = OrderManagement.UpdateOrderSortID(ord.ID, ord.SortID, this.Patient.ID);
                if (iReturn < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                    ucOutPatientItemSelect1.MessageBoxShow(OrderManagement.Err);
                    return -1;
                }
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
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

                    Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order();

                    for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                    {
                        order = this.GetObjectFromFarPoint(i, 0);

                        order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                        if (order.ID == "") //new 新加的医嘱
                        {
                            alOrder.Add(order);
                        }
                        else //update 更新的医嘱
                        {
                            #region 获得需要更新的医嘱
                            //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                            //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                            if (newOrder == null || newOrder.Status == 0)
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
                            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                            {
                                ArrayList alTemp = new ArrayList();
                                if (!string.IsNullOrEmpty(orderObj.SeeNO) && !hsSeeNO.Contains(orderObj.SeeNO))
                                {
                                    hsSeeNO.Add(orderObj.SeeNO, orderObj);

                                    alTemp = this.feeManagement.QueryFeeDetailByClinicCodeAndSeeNO(this.Patient.ID, orderObj.SeeNO, "ALL");
                                    if (alTemp == null)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow(feeManagement.Err);
                                        return 0;
                                    }
                                    alFeeDetail.AddRange(alTemp);
                                }
                            }
                        }
                        else
                        {
                            ArrayList alTemp = new ArrayList();
                            alTemp = this.feeManagement.QueryFeeDetailByClinicCodeAndSeeNONotNull(this.Patient.ID, "ALL");
                            if (alTemp == null)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(feeManagement.Err);
                                return 0;
                            }
                            alFeeDetail.AddRange(alTemp);
                        }

                        foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeDetail)
                        {
                            totCost += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                        }
                        return totCost;
                    }

                    else
                    {
                        #region 计算费用

                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
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
                                totCost = Neusoft.FrameWork.Public.String.FormatNumber(totCost, 2);
                            }
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

                    Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order();

                    for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                    {
                        order = this.GetObjectFromFarPoint(i, 0);

                        order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                        if (order.ID == "") //new 新加的医嘱
                        {
                            alOrder.Add(order);
                        }
                        else //update 更新的医嘱
                        {
                            #region 获得需要更新的医嘱
                            //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                            //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                            if (newOrder == null || newOrder.Status == 0)
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
                        Hashtable hsRecipeSeq = new Hashtable();
                        //ArrayList 
                        alFeeDetail = new ArrayList();
                        if (alOrder != null && alOrder.Count > 0)
                        {
                            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                            {
                                ArrayList alTemp = new ArrayList();
                                if (!string.IsNullOrEmpty(orderObj.ReciptSequence) && !hsRecipeSeq.Contains(orderObj.ReciptSequence))
                                {
                                    hsRecipeSeq.Add(orderObj.ReciptSequence, orderObj);

                                    alTemp = this.feeManagement.QueryFeeDetailByClinicCodeAndRecipeSeq(this.Patient.ID, orderObj.ReciptSequence, "ALL");
                                    if (alTemp == null)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow(feeManagement.Err);
                                        return 0;
                                    }
                                    alFeeDetail.AddRange(alTemp);
                                }
                            }
                        }
                        else
                        {
                            ArrayList alTemp = new ArrayList();
                            alTemp = this.outPatientFee.QueryFeeDetailByClinicCode(this.Patient.ID, "ALL");
                            if (alTemp == null)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(feeManagement.Err);
                                return 0;
                            }
                            alFeeDetail.AddRange(alTemp);
                        }

                        foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeDetail)
                        {
                            totCost += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                        }
                        return totCost;
                    }
                    else
                    {
                        #region 计算费用

                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
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
                                totCost = Neusoft.FrameWork.Public.String.FormatNumber(totCost, 2);
                            }
                        }

                        #endregion

                        alFeeDetail = null;


                        alFeeDetail = new ArrayList();

                        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item = null;
                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                        {
                            if (orderObj.MinunitFlag == "")//user03为空,说明不知道开立的什么单位 默认为最小单位
                            {
                                orderObj.MinunitFlag = "1";//默认
                            }
                            item = Classes.Function.ChangeToFeeItemList(orderObj);

                            alFeeDetail.Add(item);

                            //if (orderObj.MinunitFlag != "1")//开立最小单位 
                            //{
                            //    totCost = orderObj.Qty * orderObj.Item.Price + totCost;
                            //}
                            //else
                            //{
                            //    totCost = orderObj.Qty * orderObj.Item.Price / orderObj.Item.PackQty + totCost;
                            //    totCost = Neusoft.FrameWork.Public.String.FormatNumber(totCost, 2);
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
        bool isShowFeeWarning = true;

        /// <summary>
        /// 医嘱费用提示条
        /// </summary>
        /// <param name="isShowSIFeeInfo">是否显示医保报销信息，考虑效率问题正常开立不计算，保存和查询的时候显示</param>
        /// <param name="isRequery">对于已收费报销信息，是否重新查询，开立项目时不重新查询</param>
        private void SetOrderFeeDisplay(bool isShowSIFeeInfo, bool isRequery)
        {
            if (!this.EditGroup && this.currentPatientInfo != null)
            {
                if (this.currentPatientInfo.ID.Length > 0)
                {
                    //this.pnDisplay.Visible = true;
                    //{047C2448-B3D3-49eb-A40B-DF75749A4245}
                    lblDisplay.Text = "病历号：" + this.currentPatientInfo.PID.CardNO.TrimStart('0') + "姓名：" + this.currentPatientInfo.Name + "  性别：" + this.currentPatientInfo.Sex.Name +
                        "  年龄：" + this.OrderManagement.GetAge(this.currentPatientInfo.Birthday);
                    decimal totcost = 0;

                    this.cmbPact.Tag = this.currentPatientInfo.Pact.ID;

                    if (this.currentPatientInfo.IsSee)
                    {
                        ArrayList alFee = feeManagement.QueryAllFeeItemListsByClinicNO(this.currentPatientInfo.ID, "1", "ALL", "ALL");
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

                    if (!isAllowChangePactInfo)
                    {
                        this.pnPactInfo.Visible = false;
                    }
                    else
                    {
                        this.pnPactInfo.Visible = true;
                    }

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
                        Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                        //由于费用那里 医保接口计算总是出错，而我这里只是显示费用
                        //对于出错的不再处理，直接按照总费用显示

                        ArrayList arlMoneyInfo = null;
                        rev = feeManagement.BudgetFeeByPactUnit(this.Patient, alFeeList, isRequery, out arlMoneyInfo, out errInfo);
                        if (rev <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            decOwnMoney = 0;
                            decPubMoney = 0;
                            decTotalMoney = 0;
                            decRebateMoney = 0;
                            decPubMoneyAddUp = 0;
                            //ucOutPatientItemSelect1.MessageBoxShow(feeManagement.Err + errInfo);
                            //ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                            //return;
                        }
                        else
                        {
                            Neusoft.FrameWork.Management.PublicTrans.Commit();

                            if (arlMoneyInfo != null && arlMoneyInfo.Count >= 8)
                            {
                                decTotalMoneyAddUp = Neusoft.FrameWork.Function.NConvert.ToDecimal(arlMoneyInfo[0]);
                                decPubMoneyAddUp = Neusoft.FrameWork.Function.NConvert.ToDecimal(arlMoneyInfo[1]);
                                decOwnMoneyAddUp = Neusoft.FrameWork.Function.NConvert.ToDecimal(arlMoneyInfo[2]);
                                decRebateMoneyAddUp = Neusoft.FrameWork.Function.NConvert.ToDecimal(arlMoneyInfo[3]);

                                decTotalMoney = Neusoft.FrameWork.Function.NConvert.ToDecimal(arlMoneyInfo[4]);
                                decPubMoney = Neusoft.FrameWork.Function.NConvert.ToDecimal(arlMoneyInfo[5]);
                                decOwnMoney = Neusoft.FrameWork.Function.NConvert.ToDecimal(arlMoneyInfo[6]);
                                decRebateMoney = Neusoft.FrameWork.Function.NConvert.ToDecimal(arlMoneyInfo[7]);
                            }
                        }
                    }

                    if (this.isAccountMode)
                    {
                        this.lblDisplay.Text = "账户余额：" + this.vacancyDisplay;
                    }
                    else
                    {
                        lblDisplay.Text = "";
                    }

                    //所有累计和现有金额的报销金额、减免金额>0 就给出提示
                    if (decPubMoneyAddUp + decRebateMoneyAddUp + decPubMoney + decRebateMoney > 0)
                    {
                        this.pnTop.Height = 69;
                        lblDisplay.Text += "病历号：" + this.currentPatientInfo.PID.CardNO.TrimStart('0') + "  姓名：" + this.currentPatientInfo.Name + "  性别：" + this.currentPatientInfo.Sex.Name +
                           "  年龄：" + this.OrderManagement.GetAge(this.currentPatientInfo.Birthday);
                        this.lblFeeDisplay.Text = "费用总额:" + decTotalMoney.ToString("F4").TrimEnd('0').TrimEnd('.') +
                            "元 自费金额:" + (decOwnMoney - decRebateMoney).ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "元 报销金额:" + decPubMoney.ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "元 减免金额:" + decRebateMoney.ToString("F4").TrimEnd('0').TrimEnd('.') + "元 \r\n" +

                           "当日累计费用总额:" + decTotalMoneyAddUp.ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "元 累计自费金额:" + (decOwnMoneyAddUp - decRebateMoneyAddUp).ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "元 累计报销金额:" + decPubMoneyAddUp.ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "元 累计减免金额:" + decRebateMoneyAddUp.ToString("F4").TrimEnd('0').TrimEnd('.') + "元";

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
                        this.pnTop.Height = 23;
                        //this.pnDisplay.Size = new Size(709, 23);
                        lblDisplay.Text += "病历号：" + this.currentPatientInfo.PID.CardNO.TrimStart('0') +
                            "  姓名：" + this.currentPatientInfo.Name + "  性别：" + this.currentPatientInfo.Sex.Name +
                            "  年龄：" + this.OrderManagement.GetAge(this.currentPatientInfo.Birthday) +
                               "  费用总额：" + totcost.ToString("F4").TrimEnd('0').TrimEnd('.') + "元 ";
                    }

                    //this.pnTop.Height = this.pnDisplay.Height;
                }
                else
                {
                    lblDisplay.Text = "";
                    lblFeeDisplay.Text = "";
                }
            }
            else
            {
                lblDisplay.Text = "";
                lblFeeDisplay.Text = "";
            }
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

            Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as
                Neusoft.HISFC.Models.Order.OutPatient.Order;

            if (orderTemp == null)
            {
                return;
            }

            //{F1706DB9-376D-433e-A5A9-1E1EEA46733C}  仅能修改草药医嘱
            if (orderTemp.Item.ItemType == EnumItemType.Drug)
            {
                if (((Neusoft.HISFC.Models.Pharmacy.Item)orderTemp.Item).SysClass.ID.ToString() != "PCC")
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
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[i].Tag as
                        Neusoft.HISFC.Models.Order.OutPatient.Order;
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
                        Neusoft.FrameWork.WinForms.Classes.Function.Msg("医嘱已生效，不可修改！\n请复制医嘱后在新医嘱上修改！", 411);
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
                using (Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder uc = new Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder(true, Neusoft.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
                {
                    uc.Patient = new Neusoft.HISFC.Models.RADT.PatientInfo();//
                    uc.IsClinic = true;
                    Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                    uc.AlOrder = alModifyHerbal;
                    uc.OpenType = Neusoft.HISFC.Components.Order.Controls.EnumOpenType.Modified; //修改
                    uc.SetFocus();
                    DialogResult r = Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                    if (uc.IsCancel == true)
                    {
                        //取消了
                        return;
                    }

                    if (uc.OpenType == Neusoft.HISFC.Components.Order.Controls.EnumOpenType.Modified)
                    {
                        //改为新加模式就不删除了
                        if (this.Del(this.neuSpread1.ActiveSheet.ActiveRowIndex, true) < 0)
                        {
                            //删除原医嘱不成功
                            return;
                        }
                    }

                    if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                    {
                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order info in uc.AlOrder)
                        {
                            //{AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                            //info.DoseOnce = info.Qty;
                            //info.Qty = info.Qty * info.HerbalQty;

                            this.AddNewOrder(info, 0);
                        }
                        uc.Clear();

                        RefreshOrderState();
                        this.RefreshCombo();
                    }
                }
            }

        }

        #region {C6E229AC-A1C4-4725-BBBB-4837E869754E}

        /// <summary>
        /// 组套存储
        /// </summary>
        private void SaveGroup()
        {
            Neusoft.HISFC.Components.Common.Forms.frmOrderGroupManager group = new Neusoft.HISFC.Components.Common.Forms.frmOrderGroupManager();
            group.InpatientType = Neusoft.HISFC.Models.Base.ServiceTypes.C;
            try
            {
                group.IsManager = (Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Clone();
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

        /// <summary>
        /// 患者树右键复制当前处方
        /// </summary>
        /// <param name="selectRegister">当前患者挂号实体</param>
        /// <param name="copyNum">复制次数</param>
        public void CopyRecipeByPatientTree(Neusoft.HISFC.Models.Registration.Register selectRegister, int copyNum)
        {
            #region 获取所有医嘱

            ArrayList alOrder = new ArrayList();

            Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order();

            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = this.GetObjectFromFarPoint(i, 0);

                order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();

                alOrder.Add(order);
                
            }
            #endregion

            if (alOrder==null|| alOrder.Count==0)
            {
                return;
            }

            #region 声明
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在复制医嘱，请稍后。。。");
            Application.DoEvents();
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //存放原来的医嘱
            ArrayList alTemp = new ArrayList();
            //存放已经改变后的医嘱
            ArrayList alNewOrder = new ArrayList();
            //存放费用信息
            ArrayList alFeeItem = new ArrayList();
            //用来判断组合号，组合号应该全部生成，但要考虑原来同组的药，生成新的组合号后也同组
            Hashtable hsCombNo = new Hashtable();
            //用来放原来组合号
            string combTemp = "";

            DateTime now = OrderManagement.GetDateTimeFromSysDateTime();

            string errText = "";
            #endregion

            //循环复制次数
            for (int i = 0; i < copyNum; i++)
            {
                //取出获取的医嘱，并改变其中一些信息，保存，产生新的医嘱
                alTemp = alOrder;
                alNewOrder = new ArrayList();
                alFeeItem = new ArrayList();
                hsCombNo = new Hashtable();
                int seeNO = this.OrderManagement.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//获得新的看诊序号
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order newOrder in alTemp)
                {
                    #region 改变新医嘱的一些属性,放入到新的医嘱数组
                    newOrder.SeeNO = seeNO.ToString();
                    newOrder.ID = Classes.Function.GetNewOrderID(ref errText);

                    if (newOrder.ID == "")
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    newOrder.ReciptNO = "";
                    newOrder.ReciptSequence = "";
                    newOrder.Status = 0;
                    newOrder.IsHaveCharged = false;
                    combTemp = newOrder.Combo.ID;
                    //哈希表key存原来的组合号，value存新组合号
                    if (hsCombNo.Contains(combTemp))
                    {
                        newOrder.Combo.ID = hsCombNo[combTemp].ToString();
                    }
                    else
                    {
                        newOrder.Combo.ID = this.OrderManagement.GetNewOrderComboID();//添加组合号
                        hsCombNo.Add(combTemp, newOrder.Combo.ID);
                    }
                    alNewOrder.Add(newOrder);
                    #endregion

                    #region 插入预扣库存{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                    if (isPreUpdateStockinfo)
                    {
                        if (this.UpdateStockPre(phaIntegrate, newOrder, 1, ref errInfo) == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    #endregion

                    #region 转换费用实体
                    Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemListTmp = Classes.Function.ChangeToFeeItemList(newOrder);
                    if (feeItemListTmp == null)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("药品【" + order.Item.Name + "】医嘱实体转换成费用实体出错。", "提示");
                        return;
                    }
                    alFeeItem.Add(feeItemListTmp);
                    #endregion

                }
                #region 收费
                //处方号和流水号规则由费用业务层函数统一生成
                try
                {
                    bool bReturn = feeManagement.SetChargeInfo(this.Patient, alFeeItem, now, ref errText);
                    if (bReturn == false)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(errText + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region 回馈处方号和收费序列号

                Neusoft.HISFC.Models.Order.OutPatient.Order tempOrder = null;
                for (int k = 0; k < alNewOrder.Count; k++)
                {
                    tempOrder = alNewOrder[k] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                    if (tempOrder.ReciptNO == null || tempOrder.ReciptNO == "")
                    {
                        foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFeeItem)
                        {
                            if (tempOrder.ID == feeitem.Order.ID)
                            {
                                tempOrder.ReciptNO = feeitem.RecipeNO;
                                tempOrder.ReciptSequence = feeitem.RecipeSequence;
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region 保存医嘱 插入或更新处方表

                for (int j = 0; j < alOrder.Count; j++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = alNewOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                    if (temp == null)
                    {
                        continue;
                    }

                    #region 插入医嘱表
                    if (OrderManagement.UpdateOrder(temp) == -1) //保存医嘱档
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("插入医嘱出错！" + temp.Item.Name + "可能已经收费,请退出开立界面重新进入!");
                        return ;
                    }
                    #endregion
                }
                #endregion

            }

            #region 提交
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //对于补挂号的，保存成功才能更新已收费标记
            if (!this.Patient.IsFee)
            {
                this.Patient.IsFee = true;
            }

            //更新患者状态为已诊后，更改基本信息中患者看诊状态
            this.Patient.IsSee = true;

            #endregion
        }

        /// <summary>
        /// 患者树右键删除当前处方
        /// </summary>
        /// <param name="selectRegister">当前患者挂号实体</param>
        public void DeleteRecipeByPatientTree(Neusoft.HISFC.Models.Registration.Register selectRegister)
        {
            ArrayList alAllOrder = new ArrayList();
            Neusoft.HISFC.Models.Order.OutPatient.Order orderOne = new Neusoft.HISFC.Models.Order.OutPatient.Order();
            try
            {
                //获取所有医嘱
                for (int i = this.neuSpread1_Sheet1.Rows.Count - 1; i >= 0; i--)
                {
                    orderOne = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.ActiveSheet.Rows[i].Tag;
                    if (orderOne != null)
                    {
                        alAllOrder.Add(orderOne);
                    }

                }

                #region 判断是否有收费，如果有一条已收费就不可以删除整张处方
                bool isFee = false;
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alAllOrder)
                {
                    if (order.Status == 1)
                    {
                        isFee = true;
                        break;
                    }
                }

                if (isFee && alAllOrder.Count > 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(this, "该处方已经全部或部分收费，请点击开立按钮删除未收费的医嘱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region 判断开立医生是否相同，如果有一个医生不同不可以删除整张处方
                bool isSameDocter = true;
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alAllOrder)
                {
                    if (order.ReciptDoctor.ID != this.OrderManagement.Operator.ID)
                    {
                        isSameDocter = false;
                        break;
                    }
                }

                if (!isSameDocter && alAllOrder.Count > 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(this, "该处方有全部或部分为其他医生开立，请点击开立按钮删除自己开立的医嘱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region 获取需要删除的医嘱医嘱
                Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp = null;
                hsDeleteOrder = new Hashtable();
                if (this.neuSpread1.ActiveSheet.RowCount == 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("请先选择一条医嘱！");
                    return ;
                }
                for (int i = this.neuSpread1_Sheet1.Rows.Count - 1; i >= 0; i--)
                {
                    orderTemp = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.ActiveSheet.Rows[i].Tag;

                    if (orderTemp == null)
                    {
                        continue;
                    }

                    if (orderTemp.ID == "") //自然删除
                    {
                        this.neuSpread1.ActiveSheet.Rows.Remove(i, 1);
                    }

                    //此处只是记录需要删除的医嘱ID
                    else //delete from table
                    {
                        //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        Neusoft.HISFC.Models.Order.OutPatient.Order temp = OrderManagement.QueryOneOrder(this.Patient.ID, orderTemp.ID);
                        if (temp == null)
                        {
                        }
                        else
                        {
                            if (!this.hsDeleteOrder.Contains(temp.ID))
                            {
                                hsDeleteOrder.Add(temp.ID, temp);
                            }
                        }
                        this.neuSpread1.ActiveSheet.Rows.Remove(i, 1);
                    }
                }
                this.ucOutPatientItemSelect1.Clear(false);
                this.RefreshCombo();
                this.RefreshOrderState();
                #endregion

                #region 删除医嘱
                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在删除医嘱，请稍后。。。");
                Application.DoEvents();
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                errInfo = "";
                if (this.DelCommit(ref errInfo) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("删除医嘱失败：" + errInfo);
                    return ;
                }

                Neusoft.FrameWork.Management.PublicTrans.Commit();
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion
            }
            catch (Exception ex)
            {
                //出异常了，还允许继续开立
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return;
            }

            return ;

        }

        #endregion




        #region 公有方法

        /// <summary>
        /// 组套项目选择增加
        /// </summary>
        public void AddGroupOrder(ArrayList alOrders)
        {
            Classes.LogManager.Write("【开始添加组套处方】");
            ArrayList alHerbal = new ArrayList(); //草药

            ArrayList alAddOrder = new ArrayList();
            this.neuSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            Classes.LogManager.Write("【本次添加一共" + alOrders.Count.ToString() + "个项目】");
            int i=0;
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrders)
            {
                i++;
                if (!EditGroup)
                {
                    if (this.Patient != null && IsDesignMode)
                    {
                        #region 判断开立权限

                        string error = "";

                        int ret = 1;

                        //处方权
                        ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, this.GetReciptDoct(),
                            this.GetReciptDept(), Neusoft.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref error);

                        if (ret <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(error, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        //过敏史判断
                        ret = Components.Order.Classes.Function.JudgePatientAllergy("1", this.Patient.PID, order, ref error);

                        if (ret <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(error, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return;
                            continue;
                        }
                        #endregion

                        if (this.IBeforeAddItem != null)
                        {
                            alAddOrder.Clear();
                            alAddOrder.Add(order);

                            if (this.IBeforeAddItem.OnBeforeAddItemForOutPatient(this.Patient, this.GetReciptDoct(), this.GetReciptDept(), alAddOrder) == -1)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(IBeforeAddItem.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //return;
                                continue;
                            }
                        }
                    }
                }

                if (order.Item.SysClass.ID.ToString() == "PCC") //草药
                {
                    Components.Order.Classes.Function.ReComputeQty(order);

                    alHerbal.Add(order);
                }
                else
                {
                    this.AddNewOrder(order, 0);
                    Classes.LogManager.Write("【添加第" + i.ToString() + "个项目】");
                }
            }
            if (alHerbal.Count > 0)
            {
                this.AddHerbalOrders(alHerbal);
            }
            Classes.LogManager.Write("【结束刷新处方状态】");
            this.RefreshOrderState();
            Classes.LogManager.Write("【结束刷新处方状态】");
            Classes.LogManager.Write("【开始刷新处方组合号】");
            this.RefreshCombo();
            Classes.LogManager.Write("【结束刷新处方组合号】");
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            Classes.LogManager.Write("【结束统一处理处方信息】\r\n");
        }

        /// <summary>
        /// 获得医嘱实体从FarPoint
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Order.OutPatient.Order GetObjectFromFarPoint(int i, int SheetIndex)
        {
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            if (this.neuSpread1.Sheets[SheetIndex].Rows[i].Tag != null)
            {
                order = this.neuSpread1.Sheets[SheetIndex].Rows[i].Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
            }
            else
            {
                if (string.IsNullOrEmpty(this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("医嘱流水号")].Text))
                {
                    return null;
                }

                if (this.hsOrder.Contains(this.Patient.DoctorInfo.SeeNO + this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("医嘱流水号")].Text))
                {
                    order = this.hsOrder[this.Patient.DoctorInfo.SeeNO + this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("医嘱流水号")].Text] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                }
                else
                {
                    //增加clinic_code优化查询速率{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                    order = OrderManagement.QueryOneOrder(this.Patient.ID, this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("医嘱流水号")].Text);
                }
            }

            return order;
        }

        /// <summary>
        /// 药品自定义码
        /// </summary>
        Hashtable hsPhaUserCode = new Hashtable();

        /// <summary>
        /// 添加新医嘱
        /// </summary>
        /// <param name="sender"></param>
        public void AddNewOrder(object sender, int SheetIndex)
        {
            dirty = true;
            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = null;
            if (sender.GetType() == typeof(Neusoft.HISFC.Models.Order.OutPatient.Order))
            {
                newOrder = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                newOrder.Name = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Name;
                newOrder.Memo = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Memo;
                newOrder.Combo = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Combo;
                newOrder.DoseOnce = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).DoseOnce;
                newOrder.DoseUnit = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).DoseUnit;
                newOrder.ExeDept = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).ExeDept.Clone();
                newOrder.Frequency = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Frequency.Clone();
                newOrder.StockDept = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).StockDept.Clone();

                newOrder.HerbalQty = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).HerbalQty;
                newOrder.IsEmergency = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).IsEmergency;
                newOrder.Item = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item;
                newOrder.Qty = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Qty;

                //组套中如果非药品项目数量为零，系统在上面录入界面默认显示为1，医生在不修改的情况下保存，提示数量为0
                //modified by  houwb 2011-3-18 0:02:54
                if (newOrder.Item.ItemType != EnumItemType.Drug && newOrder.Qty == 0)
                {
                    newOrder.Qty = 1;
                }
                newOrder.Note = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Note;
                if (((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "UL")
                {
                    newOrder.Sample.Name = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).CheckPartRecord;
                }
                newOrder.Unit = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Unit;

                //此处判断停用的用法赋空值
                newOrder.Usage = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Usage;
                if (Classes.Function.usageHelper == null)
                {
                    Neusoft.HISFC.BizLogic.Manager.Constant conManager = new Neusoft.HISFC.BizLogic.Manager.Constant();
                    ArrayList alUsage = conManager.GetList("USAGE");
                    Classes.Function.usageHelper = new Neusoft.FrameWork.Public.ObjectHelper(alUsage);
                }
                if (Classes.Function.usageHelper.GetObjectFromID(newOrder.Usage.ID) == null)
                {
                    newOrder.Usage = new Neusoft.FrameWork.Models.NeuObject();
                }

                newOrder.IsNeedConfirm = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).IsNeedConfirm;
                if (((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).MinunitFlag == "" || ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).MinunitFlag == null)
                {
                    newOrder.MinunitFlag = "1";//最小单位
                }
                else
                {
                    newOrder.MinunitFlag = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).MinunitFlag;
                }

                newOrder.Sample = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Sample;
                newOrder.CheckPartRecord = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).CheckPartRecord;
                newOrder.InjectCount = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).InjectCount;
                newOrder.DoseOnceDisplay = ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).DoseOnceDisplay;
                sender = newOrder;

            }
            //添加新行
            if (sender.GetType() == typeof(Neusoft.HISFC.Models.Order.OutPatient.Order))
            {
                #region 检查添加的东西
                if (((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "UC")//检查
                {
                    //打印检查申请单
                    ////this.AddTest(sender);
                }
                else if (((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "MC")//会诊
                {
                    //添加会诊申请
                    ////this.AddConsultation(sender);
                }

                #region 皮试
                if (((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.ItemType == EnumItemType.Drug)//药品
                {
                    if (((Neusoft.HISFC.Models.Pharmacy.Item)((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item).IsAllergy)
                    {
                        //控制参数控制是否默认全部院注，全部院注不在弹出院注次数输入框
                        if (!this.isCanModifiedInjectNum)
                        {
                            if (this.hypotestMode == "1")
                            {
                                if (ucOutPatientItemSelect1.MessageBoxShow(((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.Name + "是否需要皮试！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).HypoTest = Neusoft.HISFC.Models.Order.EnumHypoTest.NoHypoTest;
                                    ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.Name += OrderManagement.TransHypotest(Neusoft.HISFC.Models.Order.EnumHypoTest.NoHypoTest);
                                }
                                else
                                {
                                    (sender as Neusoft.HISFC.Models.Order.OutPatient.Order).HypoTest = Neusoft.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                                    ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.Name += OrderManagement.TransHypotest(Neusoft.HISFC.Models.Order.EnumHypoTest.NeedHypoTest);
                                }
                            }
                            else if (this.hypotestMode == "2")//{0733E2AD-EB02-4b6f-BCF8-1A6ED5A2EFAD}
                            {

                                HISFC.Components.Order.OutPatient.Forms.frmHypoTest frmHypotest = new Neusoft.HISFC.Components.Order.OutPatient.Forms.frmHypoTest();

                                frmHypotest.IsEditMode = true;
                                frmHypotest.Hypotest = 1;
                                frmHypotest.ItemName = ((Neusoft.HISFC.Models.Pharmacy.Item)((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item).Name + " " + ((Neusoft.HISFC.Models.Pharmacy.Item)((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item).Specs;
                                frmHypotest.ShowDialog();

                                ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).HypoTest = (Neusoft.HISFC.Models.Order.EnumHypoTest)frmHypotest.Hypotest;
                            }
                        }
                    }
                }
                else
                {
                    ((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).HypoTest = Neusoft.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                }
                #endregion

                #endregion

                Neusoft.HISFC.Models.Order.OutPatient.Order order = sender as Neusoft.HISFC.Models.Order.OutPatient.Order;

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

                #region 重新获取扣库科室
                // 组套开立扣库科室可能为空
                if (newOrder.Item.ItemType == EnumItemType.Drug)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                    temp.Item = newOrder.Item;
                    temp.ReciptDept = newOrder.ReciptDept;

                    if (Classes.Function.FillPharmacyItemWithStockDept(phaIntegrate, ref temp) == -1)
                    {
                        return;
                    }

                    if (!string.IsNullOrEmpty(temp.Item.UserCode) && !hsPhaUserCode.Contains(temp.Item.ID))
                    {
                        hsPhaUserCode.Add(temp.Item.ID, temp.Item.UserCode);
                    }
                    else
                    {
                        hsPhaUserCode[temp.Item.ID] = temp.Item.UserCode;
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
                    newOrder.StockDept.Name = this.deptHelper.GetName(temp.StockDept.ID);

                    if (isShowHardDrug)
                    {
                        //判断药品是否毒麻药，给提示
                        if (((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).Quality.ID.Contains("S") ||
                            ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).Quality.ID.Contains("P"))
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("【" + order.Item.Name + "】属于毒麻药品，\r\n根据处方管理办法规定,请同时附加开立手工毒麻药处方!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                #endregion

                if (order.Combo.ID == "")
                {
                    try
                    {
                        order.Combo.ID = this.OrderManagement.GetNewOrderComboID();//添加组合号
                    }
                    catch
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("获得医嘱组合号出错：\r\n" + OrderManagement.Err);
                    }
                }

                DateTime dtNow = this.OrderManagement.GetDateTimeFromSysDateTime();

                if (!this.EditGroup)
                {
                    if (this.currentPatientInfo != null)
                    {
                        order.InDept = this.currentPatientInfo.DoctorInfo.Templet.Dept;//挂号科室
                        Neusoft.HISFC.Models.Base.PactInfo pactInfo = this.currentPatientInfo.Pact as Neusoft.HISFC.Models.Base.PactInfo;
                        decimal price = Classes.Function.GetPrice(order, order.Item, this.currentPatientInfo, pactInfo, false);
                        if (order.Item.ItemType == EnumItemType.Drug)
                        {
                            ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).Price = price;
                        }
                        else
                        {
                            ((Neusoft.HISFC.Models.Fee.Item.Undrug)order.Item).Price = price;
                        }
                        order.Item.Price = price;
                    }
                }

                #region 设置医嘱开立时间

                //if (Order.Classes.Function.IsDefaultMoDate == false)
                //{
                //    if (dtNow.Hour >= 12)
                //        order.BeginTime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 12, 0, 0);
                //    else
                //        order.BeginTime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
                //}
                //else//用默认时间
                //{
                //    order.BeginTime = dtNow;
                //}

                order.BeginTime = Order.Classes.Function.GetDefaultMoBeginDate(3);

                if (order.User03 != "")//组套的时间间隔
                {
                    int iDays = Neusoft.FrameWork.Function.NConvert.ToInt32(order.User03);
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
                    if (Classes.Function.CheckIsInjectUsage(newOrder.Usage.ID))
                    {
                        decimal Frequence = 0;

                        foreach (Neusoft.HISFC.Models.Order.Frequency freObj in Neusoft.HISFC.Components.Order.Classes.Function.HelperFrequency.ArrayObject)
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
                                Frequence = Math.Round(newOrder.Frequency.Times.Length / Neusoft.FrameWork.Function.NConvert.ToDecimal(newOrder.Frequency.Days[0]), 2);
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
                this.neuSpread1.Sheets[SheetIndex].Rows.Add(0, 1);
                this.AddObjectToFarpoint(order, 0, SheetIndex, EnumOrderFieldList.Item);

                this.neuSpread1.ActiveSheet.ActiveRowIndex = 0;
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

            //if (dealSublMode == 1)
            //{
            if (this.currentPatientInfo != null && !string.IsNullOrEmpty(this.currentPatientInfo.ID))
            {
                dirty = true;
                if (this.IDealSubjob != null)
                {
                    IDealSubjob.IsPopForChose = true;
                    ArrayList alOrder = new ArrayList();
                    ArrayList alSubOrder = new ArrayList();
                    string errText = "";
                    alOrder.Add(currentOrder);
                    if (alOrder.Count > 0)
                    {
                        if (IDealSubjob.DealSubjob(this.Patient, alOrder, ref alSubOrder, ref errText) <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败！" + errText);
                            return;
                        }

                        if (alSubOrder != null && alSubOrder.Count > 0)
                        {
                            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alSubOrder)
                            {
                                orderObj.Combo.ID = this.OrderManagement.GetNewOrderComboID();
                                orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                orderObj.SortID = 0;
                                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                                this.AddObjectToFarpoint(orderObj, this.neuSpread1_Sheet1.RowCount - 1, 0, EnumOrderFieldList.Item);
                            }
                        }
                    }
                }
                dirty = false;
            }
            //}
        }

        /// <summary>
        /// 添加草药医嘱{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        /// <param name="alHerbalOrder"></param>
        public void AddHerbalOrders(ArrayList alHerbalOrder)
        {

            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} //草药弹出草药开立界面
            using (Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder uc = new Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder(true, Neusoft.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
            {
                uc.Patient = new Neusoft.HISFC.Models.RADT.PatientInfo();//
                uc.IsClinic = true;

                Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                uc.AlOrder = alHerbalOrder;
                uc.SetFocus();

                Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                {
                    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order info in uc.AlOrder)
                    {
                        //{AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                        //info.DoseOnce = info;
                        //info.Qty = info.Qty * info.HerbalQty;

                        this.AddNewOrder(info, 0);
                    }
                    uc.Clear();
                }

                RefreshOrderState();
                this.RefreshCombo();
            }
        }

        //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 层级形式开立医嘱 yangw 20101024
        public void AddLevelOrders()
        {
            using (Neusoft.HISFC.Components.Order.Controls.ucLevelOrder uc = new Neusoft.HISFC.Components.Order.Controls.ucLevelOrder())
            {
                uc.InOutType = 1;
                uc.Patient = new Neusoft.HISFC.Models.RADT.PatientInfo();

                Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "检验检查医嘱开立";
                Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                {
                    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order info in uc.AlOrder)
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
        private List<Neusoft.HISFC.Models.Order.Order> GetSelectOrders()
        {
            List<Neusoft.HISFC.Models.Order.Order> alOrders = new List<Neusoft.HISFC.Models.Order.Order>();
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

            List<Neusoft.HISFC.Models.Order.Order> alItems = this.GetSelectOrders();

            if (alItems.Count <= 0)
            {
                //没有选择项目信息
                ucOutPatientItemSelect1.MessageBoxShow("请选择开立的检查信息!");
                return;
            }

            List<Neusoft.HISFC.Models.Order.Inpatient.Order> alInOrders = new List<Neusoft.HISFC.Models.Order.Inpatient.Order>();
            foreach (Neusoft.HISFC.Models.Order.Order inorder in alItems)
            {
                alInOrders.Add(inorder as Neusoft.HISFC.Models.Order.Inpatient.Order);
            }

            if (this.checkPrint == null)
            {
                this.checkPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Common.ICheckPrint)) as Neusoft.HISFC.BizProcess.Interface.Common.ICheckPrint;
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

        ///<summary>
        /// 刷新组合
        /// </summary>
        public void RefreshCombo()
        {
            Neusoft.HISFC.Models.Order.OutPatient.Order ord = null;
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
                            hsSubCombNo[ord.SubCombNO] = Neusoft.FrameWork.Function.NConvert.ToInt32(hsSubCombNo[ord.SubCombNO]) + 1;
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
                ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("刷新医嘱状态时出现不可预知错误！请退出开立界面重试或与管理员联系"));
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
                ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("刷新医嘱状态时出现不可预知错误！请退出开立界面重试或与管理员联系"));
            }
        }

        /// <summary>
        /// 检查医嘱合法性
        /// </summary>
        /// <returns></returns>
        public int CheckOrder()
        {
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            bool IsModify = false;

            ///是否包含附材
            bool isHaveSublOrders = false;

            //超量开立提示
            string exceedWarning = "";

            //临时医嘱
            for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            {
                order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                // 2011-11-14
                // 计算附材提示，每次保存都提示
                //if (order.IsSubtbl && order.Memo != "挂号费")
                //{
                //    isHaveSublOrders = true;
                //}

                if (order.Status == 0)
                {
                    if (order.Item.ID == "999")
                    {
                        continue;
                    }

                    //用主键（看诊序号+项目流水号）作为键值
                    if (!this.hsOrder.Contains(order.SeeNO + order.ID) && !(string.IsNullOrEmpty(order.SeeNO) || string.IsNullOrEmpty(order.ID)))
                    {
                        this.hsOrder.Add(order.SeeNO + order.ID, order);
                    }
                    //未审核的医嘱
                    IsModify = true;
                    //if (order.Item.IsPharmacy)
                    #region 药品
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {

                        #region 保存时判断是否停用、缺药
                        string errInfo = "";
                        Neusoft.HISFC.Models.Pharmacy.Item drugItem = null;
                        if (Components.Order.Classes.Function.CheckDrugState(order.StockDept, order.Item, true, ref drugItem, ref errInfo) == -1)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo);
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


                        #region 重取药品基本信息
                        order.Item.MinFee = drugItem.MinFee;
                        //此处取零差价{B9303CFE-755D-4585-B5EE-8C1901F79450}
                        //order.Item.Price = item.Price;
                        //{B9303CFE-755D-4585-B5EE-8C1901F79450} 保存原来的购入价
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice = drugItem.Price;
                        order.Item.Price = Classes.Function.GetPrice(order, drugItem, order.Patient, this.Patient.Pact, true);
                        order.Item.Name = drugItem.Name;
                        order.Item.SysClass = drugItem.SysClass.Clone();//付给系统类别
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = drugItem.IsAllergy;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = drugItem.PackUnit;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = drugItem.MinUnit;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = drugItem.BaseDose;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = drugItem.DosageForm;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).SplitType = drugItem.SplitType;

                        #endregion

                        //药品
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            //中草药
                            if (order.HerbalQty == 0)
                            {
                                ShowErr("药品【" + order.Item.Name + "】付数不能为零！", i, 0); return -1;
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
                            ShowErr("频次不能为空！", i, 0); return -1;
                        }
                        if (order.Usage.ID == "")
                        {
                            ShowErr("药品【" + order.Item.Name + "】用法不能为空！", i, 0); return -1;
                        }

                        decimal doseOnce = order.DoseOnce;
                        if (order.DoseUnit == (order.Item as HISFC.Models.Pharmacy.Item).MinUnit)
                        {
                            doseOnce = order.DoseOnce * (order.Item as HISFC.Models.Pharmacy.Item).BaseDose;
                        }
                        if ((doseOnce / ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).BaseDose) > order.Qty
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
                            decimal orderQty = 0;
                            if (order.MinunitFlag != "1")//开立最小单位 !=((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).MinUnit)
                            {
                                orderQty = order.Item.PackQty * order.Qty;
                            }
                            else
                            {
                                orderQty = order.Qty;
                            }
                            if (phaIntegrate.GetStorageNum(order.StockDept.ID, order.Item.ID, out storeNum) == 1)
                            {
                                if (orderQty > storeNum)
                                {
                                    string stockinfo =
                                        ((storeNum / ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).PackQty) > 0 ? (Math.Floor(storeNum / ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).PackQty).ToString() + ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).PackUnit) : "") + ((storeNum % ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).PackQty > 0) ? ((storeNum % ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).PackQty).ToString() + ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).MinUnit) : "");

                                    if (isCheckDrugStock == 0)
                                    {
                                        ShowErr("药品【" + order.Item.Name + "】的当前库存量为" + stockinfo + ",不足使用！", i, 0);
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
                                ShowErr("药品【" + order.Item.Name + "】库存判断失败!" + phaIntegrate.Err, i, 0);
                                return -1;
                            }
                        }
                        else
                        {
                            if (Classes.Function.CheckPharmercyItemStock(isCheckDrugStock, order.Item.ID, order.Item.Name, order.ReciptDept.ID, order.Qty, "O") == false)
                            {
                                ShowErr("药品【" + order.Item.Name + "】库存不足!", i, 0); return -1;
                            }
                        }
                    }
                    #endregion

                    #region 非药品
                    else
                    {
                        #region 判断停用状态
                        Neusoft.HISFC.Models.Fee.Item.Undrug undrug = this.itemManagement.GetUndrugByCode(order.Item.ID);
                        if (undrug == null)
                        {
                            ShowErr("查找非药品项目【" + order.Item.Name + "】失败：" + this.itemManagement.Err, i, 0);
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
                            ShowErr("【" + order.Item.Name + "】请选择执行科室！", i, 0); return -1;
                        }
                    }
                    #endregion

                    if (order.Qty == 0)
                    {
                        ShowErr("【" + order.Item.Name + "】数量不能为空！", i, 0);
                        return -1;
                    }
                    if (Neusoft.FrameWork.Public.String.ValidMaxLengh(order.Memo, 80) == false)
                    {
                        ShowErr("【" + order.Item.Name + "】的备注超长!", i, 0);
                        return -1;
                    }
                    if (order.Qty > 5000)
                    {
                        ShowErr("数量太大！", i, 0); return -1;
                    }
                    if (order.Item.Price == 0)
                    {
                        ShowErr("【" + order.Item.Name + "】单价必须大于０！", i, 0);
                        return -1;
                    }
                    if (order.ID == "") IsModify = true;
                }
                //已保存的医嘱此处一起查询
                else
                {
                    ArrayList alOrder = OrderManagement.QueryOrder(currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                    if (alOrder == null)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("查询医嘱出错" + OrderManagement.Err);
                        return -1;
                    }
                    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp in alOrder)
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
                    return -1;//未有新录入的医嘱
                }
            }
            if (isShowRepeatItemInScreen)
            {
                //提示重复药品
                string repeatItemName = "";
                Hashtable hsOrderItem = new Hashtable();

                for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                {
                    order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

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
                //四院不提示是否重新计算附材
                if (isCalculatSubl)
                {
                    if (!this.ucOutPatientItemSelect1.isChangeSubComb)
                    {
                        this.CalculatSubl(false);
                    }
                    else
                    {
                        this.ucOutPatientItemSelect1.changeChkDrugEmerce();
                    }
                }
                else
                {
                    if (ucOutPatientItemSelect1.MessageBoxShow("是否重新计算附材？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.CalculatSubl(false);
                        //return -1;
                    }
                }
            }

            return 0;

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
            //if (this.neuSpread1.ActiveSheet.SelectionCount <= 1) return;
            int iSelectionCount = 0;//{6532D2B8-A636-4a5a-8443-2FC0C6878ECC}
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                    iSelectionCount++;
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
                    Neusoft.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

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

                    o.Combo.ID = this.OrderManagement.GetNewOrderComboID();
                    #region {4F5BEF6C-48FE-4abb-84F2-091838D7BA03}
                    //o.SortID = MaxSort + 1;
                    //MaxSort = MaxSort + 1;
                    #endregion

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
                    Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    if (orderTemp != null)
                    {
                        if (!combID.Contains("|" + orderTemp.Combo.ID + "|"))
                        {
                            orderTemp.SubCombNO = firstSubComb + 1;
                            firstSubComb += 1;

                            this.AddObjectToFarpoint(orderTemp, i, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                            combID = combID + "|" + orderTemp.Combo.ID + "|";
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
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, 0);
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
            this.bTempVar = false;
            this.ucOutPatientItemSelect1.Visible = isEdit;
            if (this.ucOutPatientItemSelect1 != null)
                this.ucOutPatientItemSelect1.EditGroup = isEdit;

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
        private int CheckOrderBase(ArrayList alOrders, Neusoft.HISFC.Models.Order.OutPatient.Order order, ref string errinfo)
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
                if (Classes.Function.FillDrugItem(null, ref order) <= 0)
                {
                    return -1;
                }
            }
            else
            {
                if (Classes.Function.FillUndrugItem(ref order) <= 0)
                {
                    return -1;
                }
            }

            #endregion

            //处方权
            ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, this.GetReciptDoct(),
                this.GetReciptDept(), Neusoft.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref errinfo);

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
        /// 填充处方内容
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int FillNewOrder(ref Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            order.Patient.Pact = this.currentPatientInfo.Pact;
            order.Patient.Birthday = this.currentPatientInfo.Birthday;

            //开立科室和执行科室相同，则认为是本科室执行项目，执行科室重取
            if (order.ReciptDept.ID == order.ExeDept.ID)
            {
                order.ExeDept = new Neusoft.FrameWork.Models.NeuObject();
            }
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
            dtNow = this.OrderManagement.GetDateTimeFromSysDateTime();
            order.MOTime = dtNow;
            if (this.GetReciptDept() != null)
            {
                order.ReciptDept = this.GetReciptDept().Clone();
            }
            if (this.GetReciptDoct() != null)
            {
                order.ReciptDoctor = this.GetReciptDoct().Clone();
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
                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 粘贴医嘱
        /// </summary>
        public void PasteOrder()
        {
            Classes.LogManager.Write("【开始复制处方】");
            this.neuSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            try
            {
                this.neuSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
                if (Classes.Function.AlCopyOrders == null || Classes.Function.AlCopyOrders.Count <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("剪贴板中没有可以粘贴的医嘱！"));
                    return;
                }
                Classes.LogManager.Write("【该处方一共" + Classes.Function.AlCopyOrders.Count.ToString() + "个项目】");

                if (Neusoft.HISFC.Components.Order.Classes.HistoryOrderClipboard.Type == ServiceTypes.C)
                {
                    string oldComb = "";
                    string newComb = "";

                    ArrayList alOrder = new ArrayList();
                    ArrayList alAddOrder = new ArrayList();//用于增加接口

                    Neusoft.HISFC.Models.Order.OutPatient.Order order = null;

                    Classes.LogManager.Write("【开始统一处理处方信息】");
                    for (int i = 0; i < Classes.Function.AlCopyOrders.Count; i++)
                    {
                        order = Classes.Function.AlCopyOrders[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                        if (order == null)
                        {
                            continue;
                        }

                        if (this.FillNewOrder(ref order) == -1)
                        {
                            continue;
                        }

                        if (order.Combo.ID != oldComb)
                        {
                            newComb = this.OrderManagement.GetNewOrderComboID();
                            oldComb = order.Combo.ID;
                            order.Combo.ID = newComb;
                        }
                        else
                        {
                            order.Combo.ID = newComb;
                        }

                        alOrder.Add(order);
                    }
                    Classes.LogManager.Write("【结束统一处理处方信息】");
                    int j = 0;
                    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
                    {
                        j++;
                        Classes.LogManager.Write("【复制第" + j.ToString() + "个项目】");
                        //添加到当前类表中 按照医嘱类型进行分类
                        this.AddNewOrder(outOrder, 0);
                    }
                }
                else
                {
                    ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("不可以把住院的医嘱复制为门诊医嘱！"));
                    return;
                }

            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                Classes.LogManager.Write("【复制处方出错：" + ex.Message + "】");
            }

            Classes.LogManager.Write("【开始刷新处方状态】");
            RefreshOrderState();
            Classes.LogManager.Write("【结束刷新处方状态】");
            Classes.LogManager.Write("【开始刷新处方组合号】");
            this.RefreshCombo();
            Classes.LogManager.Write("【结束刷新处方组合号】");
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            Classes.LogManager.Write("【结束赋值处方】\r\n");
        }

        #region {7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}

        /// <summary>
        /// 复制医嘱
        /// 被复制的医嘱必须是保存过的（有医嘱流水号的）
        /// 否则粘贴时有问题
        /// </summary>
        public void CopyOrder()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0) return;

            ArrayList list = new ArrayList();

            //获取选中行的医嘱ID
            for (int row = 0; row < this.neuSpread1_Sheet1.Rows.Count; row++)
            {
                if (this.neuSpread1_Sheet1.IsSelected(row, 0))
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order ord = this.GetObjectFromFarPoint(row, 0);

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

        #endregion

        #region LIS、Pacs接口

        #region LIS、PACS申请单

        /// <summary>
        /// LIS申请单打印接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint IlisReportPrint = null;

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
                this.IlisReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint)) as Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint;
            }

            if (IlisReportPrint == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("LIS打印接口未实现！请联系信息科！\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArrayList alOrders = new ArrayList();
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;
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
        Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint IPacsReportPrint = null;

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
                this.IPacsReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint)) as Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint;
            }

            if (IPacsReportPrint == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("PACS打印接口未实现！请联系信息科！\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArrayList alOrders = new ArrayList();
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;
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
        Neusoft.HISFC.BizProcess.Interface.Common.ILis lisInterface = null;

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
                    lisInterface = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Common.ILis)) as Neusoft.HISFC.BizProcess.Interface.Common.ILis;
                }

                if (lisInterface == null)
                {
                    if (string.IsNullOrEmpty(Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err))
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("查询LIS接口出现错误：\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("没有维护LIS接口！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //lisInterface.ShowResultByPatient(patient.ID);
                    lisInterface.PatientType = Neusoft.HISFC.Models.RADT.EnumPatientType.C;
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
        protected Neusoft.HISFC.BizProcess.Interface.Common.IPacs pacsInterface = null;

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
                if (pacsInterface == null)
                {
                    this.pacsInterface = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Common.IPacs)) as Neusoft.HISFC.BizProcess.Interface.Common.IPacs;
                    if (this.pacsInterface == null)
                    {
                        if (string.IsNullOrEmpty(Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err))
                        {

                            ucOutPatientItemSelect1.MessageBoxShow("查询PACS接口出现错误：\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("没有维护PACS结果查询接口！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    //由于pacs初始化 失败后 还能正常条用，所以此处不判断初始化失败！
                    this.pacsInterface.Connect();

                    //if (this.pacsInterface.Connect() != 0)
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow("初始化PACS失败！请联系信息科！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                }

                this.pacsInterface.OprationMode = "1";
                this.pacsInterface.SetPatient(Patient);
                pacsInterface.PlaceOrder(this.GetSelectOrders());

                if (this.pacsInterface.ShowResultByPatient() == 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("查看PACS结果失败！请联系信息科！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("查看PACS结果出现错误！请联系信息科！\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 医嘱变化函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        protected virtual void ucItemSelect1_OrderChanged(Neusoft.HISFC.Models.Order.OutPatient.Order sender, EnumOrderFieldList changedField)
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

            if (this.ucOutPatientItemSelect1.OperatorType == Operator.Add)
            {
                //切记切记！！2011-12-27 佛四提出只在保存的时候提示报销超限额
                //this.isShowFeeWarning = false;
                this.AddNewOrder(sender, this.neuSpread1.ActiveSheetIndex);
                this.neuSpread1.ActiveSheet.ClearSelection();
                //this.neuSpread1.ActiveSheet.AddSelection(0, 0, 1, 1);
                //this.neuSpread1.ActiveSheet.ActiveRowIndex = 0;
                this.neuSpread1.ActiveSheet.AddSelection(this.ActiveRowIndex, 0, 1, 1);

                #region add by liuwenwen  2012-06-08 暂时屏蔽
                //this.SelectionAllChanged();
                #endregion
            }
            else if (this.ucOutPatientItemSelect1.OperatorType == Operator.Delete)
            {

            }
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
                            this.AddObjectToFarpoint(sender, this.ucOutPatientItemSelect1.CurrentRow, this.neuSpread1.ActiveSheetIndex, changedField);
                        }
                        else
                        {
                            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex);

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

                                    this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
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

                                    this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                //修改院注
                                else if (changedField == EnumOrderFieldList.InjNum
                                    || changedField == EnumOrderFieldList.ExeDept
                                    )
                                {
                                    order.InjectCount = sender.InjectCount;
                                    order.ExeDept = sender.ExeDept;

                                    this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                //如果是天数/付数、频次改变, 则整个组合一起改变, 并且重新计算数量{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                                else if (changedField == EnumOrderFieldList.Fu
                                    || changedField == EnumOrderFieldList.Day
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
                                                ucOutPatientItemSelect1.MessageBoxShow(this.OrderManagement.Err);
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

                                    this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                                else if (changedField == EnumOrderFieldList.SubComb)
                                {
                                    this.RefreshCombo();

                                    #region 组合相同的一起选择
                                    //设置组合行选择
                                    if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != "" && this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != null)
                                    {
                                        for (int k = 0; k < this.neuSpread1.ActiveSheet.Rows.Count; k++)
                                        {
                                            string strComboNo = this.GetObjectFromFarPoint(k, this.neuSpread1.ActiveSheetIndex).Combo.ID;
                                            if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo && k != this.neuSpread1.ActiveSheet.ActiveRowIndex)
                                            {
                                                this.neuSpread1.ActiveSheet.AddSelection(k, 0, 1, 1);
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                #region 全选修改

                                //天数全选修改
                                if (changedField == EnumOrderFieldList.Day)
                                {
                                    if (isChangeAllSelect == "100" || isChangeAllSelect == "110"
                                        || isChangeAllSelect == "111" || isChangeAllSelect == "101")
                                    {
                                        order.HerbalQty = sender.HerbalQty;

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

                                        this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                    }
                                    else
                                    {
                                        this.neuSpread1.ActiveSheet.RemoveSelection(System.Convert.ToInt32(alRows[i]), 0, 1, 1);
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
                                        this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                    }
                                    else
                                    {
                                        this.neuSpread1.ActiveSheet.RemoveSelection(System.Convert.ToInt32(alRows[i]), 0, 1, 1);
                                    }
                                }
                                //用法全选修改
                                else if (changedField == EnumOrderFieldList.Usage)
                                {
                                    if (isChangeAllSelect == "001" || isChangeAllSelect == "101"
                                        || isChangeAllSelect == "111" || isChangeAllSelect == "011")
                                    {
                                        order.Usage = sender.Usage;

                                        this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                    }
                                    else
                                    {
                                        this.neuSpread1.ActiveSheet.RemoveSelection(System.Convert.ToInt32(alRows[i]), 0, 1, 1);
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
                    this.AddObjectToFarpoint(sender, this.ucOutPatientItemSelect1.CurrentRow, this.neuSpread1.ActiveSheetIndex, changedField);

                    //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    if (changedField == EnumOrderFieldList.SubComb)
                    {
                        this.neuSpread1.ActiveSheet.ClearSelection();

                        #region 组合相同的一起选择
                        //设置组合行选择
                        if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != "" && this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != null)
                        {
                            this.neuSpread1.ActiveSheet.ClearSelection();
                            for (int k = 0; k < this.neuSpread1.ActiveSheet.Rows.Count; k++)
                            {
                                string strComboNo = this.GetObjectFromFarPoint(k, this.neuSpread1.ActiveSheetIndex).Combo.ID;
                                if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID == strComboNo)
                                {
                                    this.neuSpread1.ActiveSheet.AddSelection(k, 0, 1, 1);
                                }
                            }
                        }
                        #endregion
                    }
                }
                RefreshOrderState();
            }

            //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
            this.RefreshCombo();
            //this.SelectionChanged();

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
        /// 删除一个处方
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int DeleteSingleOrder()
        {

            #region 删除功能

            DialogResult r = DialogResult.Yes;
            bool isHavePha = false;

            if (this.neuSpread1_Sheet1.ActiveRowIndex < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("请先选择一条医嘱！");
                return 0;
            }


            r = ucOutPatientItemSelect1.MessageBoxShow("是否删除所选定医嘱\n ！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.No)
            {
                return 0;
            }

            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag
                as Neusoft.HISFC.Models.Order.OutPatient.Order;

            if (order == null)
            {
                return 0;
            }

            if (r == DialogResult.Yes)
            {

                if (order.Status == 0)
                {
                    if (order.ReciptDoctor.ID != this.OrderManagement.Operator.ID)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("该医嘱不是当前医生开立,不能删除!", "提示");
                        return 0;
                    }

                    //能开立就能删除，所以删除就不判断了
                    //if (Components.Order.Classes.Function.JudgeEmplPriv(order, this.GetReciptDoct(), this.GetReciptDept(), DoctorPrivType.RecipePriv, true, ref errInfo) <= 0)
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow(errInfo, "提示");
                    //    return 0;
                    //}

                  
                    if (order.ID == "") //自然删除
                    {
                        this.neuSpread1.ActiveSheet.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
                    }

                    //此处只是记录需要删除的医嘱ID
                    else
                    {
                        //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        Neusoft.HISFC.Models.Order.OutPatient.Order temp = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);

                        //找不到时提示
                        if (temp == null)
                        {
                            //ucOutPatientItemSelect1.MessageBoxShow("获取医嘱失败:" + OrderManagement.Err);
                            //return -1;
                        }
                        else
                        {
                            if (!this.hsDeleteOrder.Contains(temp.ID))
                            {
                                hsDeleteOrder.Add(temp.ID, temp);
                            }
                        }

                        this.neuSpread1.ActiveSheet.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
                    }
                }
                else
                {
                    ucOutPatientItemSelect1.MessageBoxShow("项目【" + order.Item.Name + "】已经收费，不能进行删除操作！", "提示");
                    return 0;
                }
            }

            this.ucOutPatientItemSelect1.Clear(false);

            this.RefreshCombo();
            this.RefreshOrderState();
            #endregion

            return 0;
        }

        /// <summary>
        /// 删除的处方ID，用于保存时删除
        /// </summary>
        private Hashtable hsDeleteOrder = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="isDirctDel">是否直接删除（不提示）</param>
        /// <returns></returns>
        private int Del(int rowIndex, bool isDirctDel)
        {
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} 重构了一个删除函数
            #region 全部删除功能
            int j = rowIndex;
            DialogResult r = DialogResult.Yes;
            bool isHavePha = false;
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;//,temp=null;
            if (j < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("请先选择一条医嘱！");
                return 0;
            }
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                //Clear Selected Flag
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

            if (!isDirctDel)
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
                        order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.ActiveSheet.Rows[i].Tag;

                        if (order == null)
                        {
                            continue;
                        }
                        if (order.Status == 0)
                        {
                            if (order.ReciptDoctor.ID != this.OrderManagement.Operator.ID)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow("该医嘱不是当前医生开立,不能删除!", "提示");
                                return 0;
                            }

                            //既然能开立就能删除，所以删除不需要做判断了
                            //if (Components.Order.Classes.Function.JudgeEmplPriv(order, this.GetReciptDoct(), this.GetReciptDept(), DoctorPrivType.RecipePriv, true, ref errInfo) <= 0)
                            //{
                            //    ucOutPatientItemSelect1.MessageBoxShow(errInfo, "提示");
                            //    return 0;
                            //}

                            if (order.ExtendFlag1 != null)
                            {
                                string[] strSplit = order.ExtendFlag1.Split('|');
                                if (strSplit.Length == 3)
                                {
                                    if (ucOutPatientItemSelect1.MessageBoxShow("医嘱【" + order.Item.Name + "】已经设置了接瓶,确定删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        return 0;
                                    }
                                    for (int kk = 0; kk < this.neuSpread1_Sheet1.Rows.Count; kk++)
                                    {
                                        Neusoft.HISFC.Models.Order.OutPatient.Order tem = this.GetObjectFromFarPoint(kk, 0);

                                        if (tem != null && tem.ExtendFlag1 != null && tem.Combo.ID != order.Combo.ID && tem.ExtendFlag1.Split('|').Length == 3 && tem.ExtendFlag1.Split('|')[1] == order.Combo.ID)
                                        {
                                            tem.NurseStation.User02 = "C";
                                            tem.ExtendFlag1 = tem.ExtendFlag1.Split('|')[0];
                                        }
                                    }
                                }
                            }
                            if (order.ID == "") //自然删除
                            {
                                #region 2012-05-29 mad
                                //添加处方删除记录(备注为：挂号费)
                                if (this.isSaveOrderHistory && this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("备注")].Text.Trim() == "挂号费")
                                {

                                    if (OrderManagement.InsertOrderChangeInfo(order) < 0)
                                    {
                                        errInfo = "保存" + order.Item.Name + "修改纪录出错！" + OrderManagement.Err;
                                       // return -1;
                                    }

                                }
                                #endregion
                                this.neuSpread1.ActiveSheet.Rows.Remove(i, 1);
                            }

                            //此处只是记录需要删除的医嘱ID
                            else //delete from table
                            {
                                //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                                Neusoft.HISFC.Models.Order.OutPatient.Order temp = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                                //Neusoft.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                                //找不到时提示
                                if (temp == null)
                                {
                                    //ucOutPatientItemSelect1.MessageBoxShow("获取医嘱失败:" + OrderManagement.Err);
                                    //return -1;
                                }
                                else
                                {
                                    if (!this.hsDeleteOrder.Contains(temp.ID))
                                    {
                                        hsDeleteOrder.Add(temp.ID, temp);
                                     

                                    }
                                }

                                this.neuSpread1.ActiveSheet.Rows.Remove(i, 1);
                            }
                        }
                        else
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("医嘱:[" + order.Item.Name + "]已经收费，不能进行删除操作！", "提示");
                            continue;
                        }
                    }
                }
                if (this.EnabledPass && isHavePha)
                {
                    ////this.PassSaveCheck(this.GetPhaOrderArray(), 1, true);
                }
                ////SetFeeDisplay(this.Patient, null);
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
        private int DeleteOneOrder(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            //删除医嘱
            if (OrderManagement.DeleteOrder(order.SeeNO, Neusoft.FrameWork.Function.NConvert.ToInt32(order.ID)) <= 0)
            {
                errInfo = order.Item.Name + "可能已经收费，请退出开立界面重试" + OrderManagement.Err;
                return -1;
            }
            //删除费用
            if (feeManagement.DeleteFeeItemListByMoOrder(order.ID) == -1)
            {
                errInfo = order.Item.Name + "可能已经收费，请退出开立界面重试" + OrderManagement.Err;
                return -1;
            }

            #region 医嘱带的附材的删除{D256A1B3-F969-4d2c-92C3-9A5508835D5B}
            //重新组合可能组合号改变，修改为按照处方号获取费用明细

            ArrayList alSubAndOrder = feeManagement.QueryValidFeeDetailbyClinicCodeAndRecipeSeq(this.currentPatientInfo.ID, order.ReciptSequence);
            if (alSubAndOrder == null)
            {
                errInfo = feeManagement.Err;
                return -1;
            }
            else
            {
                int rev = -1;
                for (int s = 0; s < alSubAndOrder.Count; s++)
                {
                    Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item = alSubAndOrder[s] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                    if (item.Item.IsMaterial)
                    {
                        rev = this.feeManagement.DeleteFeeItemListByRecipeNO(item.RecipeNO, item.SequenceNO.ToString());

                        if (rev == 0)
                        {
                            errInfo = "项目【" + item.Name + "】对应的附材已经收费，不允许删除！\r\n请退出界面重试！";
                            return -1;
                        }
                        else if (rev < 0)
                        {
                            errInfo = feeManagement.Err;
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
                            resultValue = this.phaIntegrate.DelApplyOut(order);
                            if (resultValue < 0)
                            {
                                errInfo = phaIntegrate.Err;
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        //删除非药品终端申请信息
                        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                        Neusoft.HISFC.BizProcess.Integrate.Terminal.Confirm confrimIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Terminal.Confirm();
                        if (order.Item.IsNeedConfirm && !order.IsHaveCharged)
                        {
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

            order.DCOper.ID = this.OrderManagement.Operator.ID;
            order.DCOper.OperTime = this.OrderManagement.GetDateTimeFromSysDateTime();
            if (this.isSaveOrderHistory)
            {
                if (OrderManagement.InsertOrderChangeInfo(order) < 0)
                {
                    errInfo = "保存" + order.Item.Name + "修改纪录出错！" + OrderManagement.Err;
                    return -1;
                }
            }

            #region 删除预扣库存信息 {E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
            if (isPreUpdateStockinfo)
            {
                if (this.UpdateStockPre(phaIntegrate, order, -1, ref errInfo) == -1)//删除
                {
                    errInfo = "删除预扣信息出错  " + errInfo;
                    return -1;
                }
            }
            #endregion

            #region 电子申请单 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504
            string isUseDL = feeManagement.GetControlValue("200212", "0");
            if (isUseDL == "1")
            {
                if (order.ApplyNo != null)
                {
                    if (PACSApplyInterface == null)
                    {
                        if (InitPACSApplyInterface() < 0)
                        {
                            //ucOutPatientItemSelect1.MessageBoxShow("初始化电子申请单接口时出错！");
                            errInfo = "初始化电子申请单接口时出错！";
                            return -1;
                        }
                    }
                    PACSApplyInterface.DeleteApply(order.ApplyNo);
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 确认删除
        /// </summary>
        /// <returns></returns>
        private int DelCommit(ref string errInfo)
        {
            if (hsDeleteOrder == null || hsDeleteOrder.Keys.Count == 0)
            {
                return 1;
            }
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            foreach (string orderID in new ArrayList(hsDeleteOrder.Keys))
            {
                //优化查询速率 {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                order = hsDeleteOrder[orderID] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                if (this.DeleteOneOrder(order) == -1)
                {
                    return -1;
                }
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
                if (this.currentPatientInfo.IsFee)  //不用isSee是因为通过挂号进来的 isSee为false
                {
                    string memo = this.currentPatientInfo.Memo;

                    //查询有效的挂号记录
                    ArrayList alRegister = this.regManagement.QueryPatient(this.currentPatientInfo.ID);
                    if (alRegister == null)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("查询患者挂号信息出错!");
                        return -1;
                    }

                    if (alRegister.Count == 0)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("该患者挂号信息已作废，请刷新界面!");
                        return -1;
                    }
                    ((Neusoft.HISFC.Models.Registration.Register)alRegister[0]).DoctorInfo.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO;
                    this.currentPatientInfo = alRegister[0] as Neusoft.HISFC.Models.Registration.Register;

                    if (this.currentPatientInfo != null)
                    {
                        PactInfo pactTemp = pactHelper.GetObjectFromID(currentPatientInfo.Pact.ID) as Neusoft.HISFC.Models.Base.PactInfo;
                        if (pactTemp == null)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("查找合同单位【" + currentPatientInfo.Pact.Name + "(编码 " + currentPatientInfo.Pact.ID + ")】失败！\r\n请联系信息科！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            this.currentPatientInfo.Pact = pactHelper.GetObjectFromID(currentPatientInfo.Pact.ID) as Neusoft.HISFC.Models.Base.PactInfo;
                        }
                    }

                    this.currentPatientInfo.Memo = memo;
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
        /// <returns></returns>
        public int Add()
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

            this.hsDeleteOrder.Clear();
            this.IsDesignMode = true;

            this.ucOutPatientItemSelect1.Clear(true);

            //this.ucOutPatientItemSelect1.Focus();
            //此处用于开立过程首日超过，提示报销限额等
            //this.isShowFeeWarning = false;

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

            string dept = this.schemgManager.ExecSqlReturnOne(string.Format(@"select see_dpcd from fin_opr_register t
                                                                    where t.card_no='{0}'
                                                                    and t.in_state!='N' 
                                                                    and t.in_state is not null", this.Patient.PID.CardNO));
            if (!string.IsNullOrEmpty(dept) && dept != "-1" && dept != this.GetReciptDept().ID)
            {
                Neusoft.HISFC.Models.Base.Department deptObj = this.managerAssign.GetDepartment(dept);
                if (deptObj != null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("该患者已在" + deptObj.Name + "留观！");
                }
                return -1;
            }

            DateTime now = OrderManagement.GetDateTimeFromSysDateTime();
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            //Neusoft.FrameWork.Management.Transaction t = new Neusoft.FrameWork.Management.Transaction(OrderManagement.Connection);
            //t.BeginTransaction();
            radtManger.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = this.OrderManagement.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//获得新的看诊序号
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("获取看诊序号失败：" + OrderManagement.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

            }

            if (this.AddRegInfo(Patient) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                string revStr = this.schemgManager.ExecSqlReturnOne(strSql);
                int rev = Neusoft.FrameWork.Function.NConvert.ToInt32(revStr);
                if (rev > 0)
                {
                    if (ucOutPatientItemSelect1.MessageBoxShow("该患者已收取留观费，是否继续收取？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else if (rev == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("查询留观费用失败：" + this.schemgManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                #endregion

                if (GetEmergencyFee(ref alEmergencyFee) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                    return -1;
                }
                if (this.alEmergencyFee != null && this.alEmergencyFee.Count > 0)
                {
                    #region 处理已收挂号费用

                    rev = this.schemgManager.ExecNoQuery(@"delete from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='挂号费'", this.Patient.ID, this.GetReciptDoct().ID);
                    if (rev == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                        string invoiceNo = this.schemgManager.ExecSqlReturnOne(sql);
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
                            string invoiceNoTemp = this.schemgManager.ExecSqlReturnOne(sql);
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
                                string invoiceSeq = this.schemgManager.ExecSqlReturnOne(sql);

                                //挂号费发票作废，同时退还账户金额
                                if (this.feeManagement.LogOutInvoiceByAccout(this.Patient, invoiceNo, invoiceSeq) == -1)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                    ucOutPatientItemSelect1.MessageBoxShow("退还已收诊金失败:" + this.schemgManager.Err);
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
                    bool iReturn = feeManagement.SetChargeInfo(this.Patient, alEmergencyFee, now, ref errText);
                    if (iReturn == false)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        alEmergencyFee.Remove(regFeeItem);
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                    #endregion
                }
            }
            else
            {
                if (this.radtManger.RegisterObservePatient(this.currentPatientInfo) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    alEmergencyFee.Remove(regFeeItem);
                    ucOutPatientItemSelect1.MessageBoxShow("更新留观状态失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            #region 留观后，更新看诊信息
            //收完挂号费后，更新挂号表已收费状态，避免多次补收挂号费
            if (this.regManagement.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                alEmergencyFee.Remove(regFeeItem);
                dirty = false;
                ucOutPatientItemSelect1.MessageBoxShow(conManager.Err);
                return -1;
            }

            //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                if (this.regManagement.UpdateSeeDone(this.currentPatientInfo.ID) < 0)
                {
                    errInfo = "更新看诊标志出错！";

                    return -1;
                }

                if (this.regManagement.UpdateDept(this.currentPatientInfo.ID, ((Neusoft.HISFC.Models.Base.Employee)this.OrderManagement.Operator).Dept.ID, this.OrderManagement.Operator.ID) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    alEmergencyFee.Remove(regFeeItem);
                    ucOutPatientItemSelect1.MessageBoxShow("更新看诊科室、医生出错！");
                    return -1;
                }
            }
            #endregion

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            this.currentPatientInfo.PVisit.InState.ID = "R";
            this.currentPatientInfo.IsFee = true;

            ucOutPatientItemSelect1.MessageBoxShow("留观成功！");
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

            Neusoft.HISFC.Models.Fee.Item.Undrug supplyItem;
            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList emergencyFeeItem = null;

            string supplyItemCode = "";
            ArrayList emergencyConstList = this.conManager.GetList("EmergencyFeeItem");
            if (emergencyConstList == null)
            {
                errInfo = "获取急诊留观项目失败！" + conManager.Err;
                return -1;
            }
            else if (emergencyConstList.Count == 0)
            {
                return 0;
            }

            for (int i = 0; i < emergencyConstList.Count; i++)
            {
                supplyItem = new Neusoft.HISFC.Models.Fee.Item.Undrug();
                supplyItemCode = ((Neusoft.FrameWork.Models.NeuObject)emergencyConstList[i]).ID.Trim();

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
                string revStr = this.schemgManager.ExecSqlReturnOne(strSql);
                int rev = Neusoft.FrameWork.Function.NConvert.ToInt32(revStr);
                if (rev <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("未找到留观费用，不用出关！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                #endregion

                if (ucOutPatientItemSelect1.MessageBoxShow("是否删除今天收取的留观费用？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                    //取消留观，删除留观费
                    if (this.alEmergencyFee != null && this.alEmergencyFee.Count > 0)
                    {
                        rev = this.schemgManager.ExecNoQuery(@"delete from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='急诊留观费'", this.Patient.ID, this.GetReciptDoct().ID);
                        if (rev == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                            string invoiceNo = this.schemgManager.ExecSqlReturnOne(sql);
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
                if (radtManger.OutObservePatientManager(this.currentPatientInfo, EnumShiftType.EO, "出关") < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("更新留观状态失败！");
                    return -1;
                }
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            ucOutPatientItemSelect1.MessageBoxShow("取消急诊留观成功！");
            return 1;
        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
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
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            if (radtManger.OutObservePatientManager(this.currentPatientInfo, EnumShiftType.CPI, "留观转住院") < 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow("更新留观状态失败！");
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
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
                Neusoft.HISFC.Models.Order.OutPatient.Order ord = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

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
            this.bTempVar = false;
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
        /// 是否已开立诊断
        /// </summary>
        /// <returns></returns>
        public bool isHaveDiag()
        {
            if (this.Patient != null && this.IsDesignMode)
            {
                ArrayList alDiagnose = myDiagnose.QueryCaseDiagnoseForClinic(this.Patient.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);

                if (alDiagnose == null || alDiagnose.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.bIsDesignMode == false)
            {
                return -1;
            }

            this.ucOutPatientItemSelect1.Clear(false);

            #region 保存之前的判断

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            this.hsOrder.Clear();
            this.hsOrderItem.Clear();

            //在此处加载所有医嘱列表和医嘱项目列表
            if (this.CheckOrder() == -1)
            {
                return -1;
            }

            //预览处方
            if (isAutoPrintRecipe == 2)
            {
                if (PrintRecipe(true) == -1)
                {
                    return -1;
                }
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
                        if (feeManagement.GetAccountVacancy(this.Patient.PID.CardNO, ref vacancy) <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(feeManagement.Err);
                            return -1;
                        }
                        isAccount = true;
                    }
                }
            }
                #endregion

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
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存医嘱，请稍后。。。");
            Application.DoEvents();
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            OrderManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans); //设置事务
            this.managerIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            feeManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);//设置事务
            undrugztManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            regManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            phaIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);//{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}

            string strID = "";
            string strNameNotUpdate = "";//没有更新的医嘱名称
            string reciptNo = "";//处方号
            int rep_no = 0; //处方内流水号
            bool bHavePha = false;//是否包含药品(处方预览使用)

            Neusoft.HISFC.Models.Order.OutPatient.Order order;
            DateTime now = OrderManagement.GetDateTimeFromSysDateTime();
            #endregion

            errInfo = "";
            if (this.DelCommit(ref errInfo) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow("删除医嘱失败：" + errInfo);
                return -1;
            }

            #region 判断看诊序号
            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = this.OrderManagement.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//获得新的看诊序号
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    ucOutPatientItemSelect1.MessageBoxShow(OrderManagement.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

            }
            #endregion

            #region 处理医嘱及辅材

            ArrayList alOrder = new ArrayList(); //保存医嘱
            ArrayList alFeeItem = new ArrayList();//保存费用
            //ArrayList alSupplyFee = new ArrayList();//
            ArrayList alSubOrders = new ArrayList();//附材数组

            this.alOrderTemp = new ArrayList();//临时保存
            ArrayList alOrderChangedInfo = new ArrayList();//医嘱修改纪录
            bool iReturn = false;
            string errText = "";

            //提示重复药品
            string repeatItemName = "";
            Hashtable hsOrderItem = new Hashtable();

            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

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

                if (order.Status == 0)//未审核的医嘱
                {
                    #region 保存医嘱

                    if (order.ID == "") //new 新加的医嘱
                    {
                        strID = Classes.Function.GetNewOrderID(ref errInfo);
                        if (strID == "")
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1;
                        }

                        order.ID = strID;    //申请单号
                        order.ReciptNO = reciptNo;
                        order.SequenceNO = 0;
                        order.ReciptSequence = "";
                        //if (order.Item.IsPharmacy)
                        if (order.Item.ItemType == EnumItemType.Drug)
                        {
                            bHavePha = true;
                        }
                        alOrder.Add(order);
                        alOrderTemp.Add(order);

                        #region 插入预扣库存{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                        if (isPreUpdateStockinfo)
                        {
                            if (this.UpdateStockPre(phaIntegrate, order, 1, ref errInfo) == -1)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;

                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }
                        #endregion

                        #region 账户患者的复合项目需拆成明细再划价{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                        bool isExist = false;
                        if (this.Patient.IsAccount)
                        {
                            if (order.Item is Neusoft.HISFC.Models.Fee.Item.Undrug)
                            {
                                Neusoft.HISFC.Models.Fee.Item.Undrug undrugInfo = new Neusoft.HISFC.Models.Fee.Item.Undrug();
                                if (this.hsOrderItem.Contains(order.Item.ID))
                                {
                                    undrugInfo = hsOrderItem[order.Item.ID] as Neusoft.HISFC.Models.Fee.Item.Undrug; ;
                                }
                                else
                                {
                                    undrugInfo = this.feeManagement.GetItem(order.Item.ID);
                                    if (undrugInfo == null)
                                    {
                                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                        ucOutPatientItemSelect1.MessageBoxShow("查询非药品项目：" + order.Item.Name + "出错！" + this.feeManagement.Err);
                                        return -1;
                                    }
                                }
                                //复合项目的先不优化了
                                if (undrugInfo.UnitFlag == "1")
                                {
                                    ArrayList alOrderDetails = Classes.Function.ChangeZtToSingle(order, this.Patient, this.Patient.Pact);
                                    if (alOrderDetails != null)
                                    {
                                        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList tmpFeeItemList = null;

                                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order tmpOrder in alOrderDetails)
                                        {
                                            tmpFeeItemList = Classes.Function.ChangeToFeeItemList(tmpOrder);
                                            if (tmpFeeItemList != null)
                                            {
                                                alFeeItem.Add(tmpFeeItemList.Clone());
                                                isExist = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!isExist)
                        {
                            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList alFeeItemListTmp = Classes.Function.ChangeToFeeItemList(order);
                            if (alFeeItemListTmp == null)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
                        Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                        //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                        if (newOrder == null || newOrder.Status == 0)
                        {
                            newOrder = order;
                        }

                        if (newOrder.Status != 0 || newOrder.IsHaveCharged)//检查并发医嘱状态
                        {
                            strNameNotUpdate += "[" + order.Item.Name + "]";

                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("[" + order.Item.Name + "]可能已经收费,请退出开立界面重新进入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1;
                            continue;
                        }

                        //if (newOrder.Item.IsPharmacy)
                        if (newOrder.Item.ItemType == EnumItemType.Drug)
                        {
                            bHavePha = true;
                        }
                        alOrder.Add(newOrder);
                        alOrderTemp.Add(newOrder);

                        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeitems = Classes.Function.ChangeToFeeItemList(order);
                        if (feeitems == null)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(order.Item.Name + "医嘱实体转换成费用实体出错！", "提示");
                            return -1;
                        }
                        alFeeItem.Add(feeitems);

                        #endregion

                        #region 插入预扣库存{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                        if (isPreUpdateStockinfo)
                        {
                            if (this.UpdateStockPre(phaIntegrate, order, -1, ref errInfo) == -1)//先删除
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;

                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                            if (this.UpdateStockPre(phaIntegrate, order, 1, ref errInfo) == -1)//再插入
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;

                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }

                        }
                        #endregion
                    }

                    #endregion
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
                        if (IDealSubjob.DealSubjob(this.Patient, alOrder, ref alSubOrders, ref errText) <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败！" + errText);
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 判断诊断

            if (isJudgeDiagnose)
            {
                //诊断的判断 南庄医院
                bool flag = false;
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeItem)
                {
                    if (feeItem.Order.Item.ItemType == EnumItemType.Drug)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    if (!this.isHaveDiag())
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("该患者还没有录入诊断", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }
            }

            #endregion

            #region 未挂号患者，此处插入挂号信息

            if (this.AddRegInfo(Patient) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                dirty = false;
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                return -1;
            }

            //收完挂号费后，更新挂号表已收费状态，避免多次补收挂号费
            if (this.regManagement.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                dirty = false;
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(conManager.Err);
                return -1;
            }

            #region 补收挂号费项目

            //正常挂号患者都补收费用
            if (this.Patient.PVisit.InState.ID.ToString() == "N")
            {
                if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
                {
                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj in alSupplyFee)
                    {
                        alFeeItem.Add(feeItemObj);
                    }

                    //alFeeItem.AddRange(this.alSupplyFee);
                }
            }
            #endregion

            #endregion

            #region 合并收费数组

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order subOrder in alSubOrders)
            {
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList alFeeItemListTmp = Classes.Function.ChangeToFeeItemList(subOrder);
                if (alFeeItemListTmp == null)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(subOrder.Item.Name + "医嘱实体转换成费用实体出错。", "提示");
                    return -1;
                }
                alFeeItem.Add(alFeeItemListTmp);
            }

            #endregion

            #region 收费


            Classes.LogManager.Write("开始收费!");

            //处方号和流水号规则由费用业务层函数统一生成
            try
            {
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                if (isAccountTerimal && isAccount)
                {
                    iReturn = feeManagement.SetChargeInfoToAccount(this.Patient, alFeeItem, now, ref errText);
                    if (iReturn == false)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
                            Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                        if (resultValue == 0)
                        {
                            iReturn = feeManagement.SetChargeInfo(this.Patient, alFeeItem, now, ref errText);
                            if (iReturn == false)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        iReturn = feeManagement.SetChargeInfo(this.Patient, alFeeItem, now, ref errText);
                        if (iReturn == false)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(errText + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }
            Classes.LogManager.Write("结束收费!");
            #endregion

            #region 回馈处方号和处方流水号

            Neusoft.HISFC.Models.Order.OutPatient.Order tempOrder = null;
            for (int k = 0; k < alOrder.Count; k++)
            {
                tempOrder = alOrder[k] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                if (tempOrder.ReciptNO == null || tempOrder.ReciptNO == "")
                {
                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFeeItem)
                    {
                        if (tempOrder.ID == feeitem.Order.ID)
                        {
                            tempOrder.ReciptNO = feeitem.RecipeNO;
                            tempOrder.SequenceNO = feeitem.SequenceNO;
                            tempOrder.ReciptSequence = feeitem.RecipeSequence;

                            break;
                        }
                    }
                }
            }
            #endregion

            #region 保存医嘱 插入或更新处方表

            #region 根据接口实现对医嘱信息进行补充判断
            //{48E6BB8C-9EF0-48a4-9586-05279B12624D}
            if (this.IAlterOrderInstance != null)
            {
                List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                for (int j = 0; j < alOrder.Count; j++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp
                    = alOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                    if (temp == null)
                    {
                        continue;
                    }
                    orderList.Add(temp);
                }
                if (this.IAlterOrderInstance.AlterOrder(this.currentPatientInfo, this.reciptDoct, this.reciptDept, ref orderList) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }
            }

            #endregion

            //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
            if (!isAccount && IDoctFee != null)
            {
                if (IDoctFee.UpdateOrderFee(this.Patient, alOrder, now, ref errText) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("更新医嘱收费标记失败！" + errText);
                    return -1;
                }
            }

            for (int j = 0; j < alOrder.Count; j++)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                if (temp == null)
                {
                    continue;
                }

                #region 插入医嘱表
                if (OrderManagement.UpdateOrder(temp) == -1) //保存医嘱档
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("插入医嘱出错！" + temp.Item.Name + "可能已经收费,请退出开立界面重新进入!");
                    return -1;
                }
                #endregion
            }
            #endregion

            #region 插入医嘱变更纪录

            if (this.isSaveOrderHistory)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order temp = null;

                for (int j = 0; j < alOrder.Count; j++)
                {
                    temp = alOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                    if (this.alAllOrder == null || this.alAllOrder.Count <= 0 || temp == null)
                    {
                        continue;
                    }

                    Neusoft.HISFC.Models.Order.OutPatient.Order tem
                        = this.orderHelper.GetObjectFromID(temp.ID) as Neusoft.HISFC.Models.Order.OutPatient.Order;

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
                    temp = alOrderChangedInfo[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                    if (this.OrderManagement.InsertOrderChangeInfo(temp) < 0)
                    {
                        //对于变更记录出错也不提示
                        //Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                        //ucOutPatientItemSelect1.MessageBoxShow("插入医嘱变更纪录出错！");
                        //Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
                    if (this.managerIntegrate.UpdateAssignSaved(this.currentRoom.ID, this.currentPatientInfo.ID, now, this.OrderManagement.Operator.ID) < 0)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("更新分诊标志出错！");
                        return -1;
                    }
                }
            }

            //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                if (this.regManagement.UpdateSeeDone(this.currentPatientInfo.ID) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("更新看诊标志出错！");
                    return -1;
                }

                if (this.regManagement.UpdateDept(this.currentPatientInfo.ID, ((Neusoft.HISFC.Models.Base.Employee)this.OrderManagement.Operator).Dept.ID, this.OrderManagement.Operator.ID) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("更新看诊科室、医生出错！");
                    return -1;
                }
            }

            #endregion

            #region 提交
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            Classes.LogManager.Write("处方保存提交成功!");

            //对于补挂号的，保存成功才能更新已收费标记
            if (!this.Patient.IsFee)
            {
                this.Patient.IsFee = true;
            }

            //更新患者状态为已诊后，更改基本信息中患者看诊状态
            this.Patient.IsSee = true;

            #endregion

            #region 账户扣取挂号费

            int iRes = 1;
            if (this.isAccountMode)
            {
                //ucOutPatientItemSelect1.MessageBoxShow("如有本科室执行项目，请在终端刷卡扣费！");
                Classes.LogManager.Write("开始扣取挂号费!");

                //正常挂号患者都补收费用
                if (this.Patient.PVisit.InState.ID.ToString() == "N")
                {
                    if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
                    {
                        Neusoft.HISFC.BizProcess.Integrate.AccountFee.OutPatientFeeManage accountManager = new Neusoft.HISFC.BizProcess.Integrate.AccountFee.OutPatientFeeManage();
                        Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                        accountManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

                        errInfo = "";
                        iRes = accountManager.ChargeFee(this.Patient, alSupplyFee, out errInfo);
                        if (iRes <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            //ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n 可能原因是：账户余额不足，请患者到收费处充值后缴费！");
                        }
                        else
                        {
                            Neusoft.FrameWork.Management.PublicTrans.Commit();
                            //ucOutPatientItemSelect1.MessageBoxShow("扣取挂号费和诊金成功！");
                        }

                    }
                }
                Classes.LogManager.Write("结束扣取挂号费!");

            }
            #endregion

            #region 提示信息放到一起

            if (strNameNotUpdate == "")//已经变化的医嘱信息
            {
                ucOutPatientItemSelect1.MessageBoxShow("医嘱保存成功！");
            }
            else
            {
                ucOutPatientItemSelect1.MessageBoxShow("医嘱保存成功！\n" + strNameNotUpdate + "医嘱状态已经在其它地方更改，无法进行更新，请刷新屏幕！");
            }
            Classes.LogManager.Write("处方保存成功!");

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
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow("更新医嘱序号出错！");
                return -1;
            }
            #endregion

            #region 接入电子申请单
            string isUseDL = feeManagement.GetControlValue("200212", "0");
            if (isUseDL == "1")
            {
                if (PACSApplyInterface == null)
                {
                    if (InitPACSApplyInterface() < 0)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("初始化电子申请单接口时出错！");
                        return -1;
                    }
                }
                PACSApplyInterface.SaveApplysG(this.Patient.DoctorInfo.SeeNO.ToString(), 0);
            }
            #endregion

            #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} 合理用药自动审查

            if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
            {
                //this.IReasonableMedicine.ShowFloatWin(false);
                this.PassTransOrder(1, false);
            }

            #endregion

            #region 返回处理
            isShowFeeWarning = false;
            this.IsDesignMode = false;
            this.bTempVar = false;

            //{F67E089F-1993-4652-8627-300295AAED8C}
            //保存后清空
            this.hsComboChange = new Hashtable();
            this.alSupplyFee = new ArrayList();
            #endregion

            #region 自动打印处方

            if (isAutoPrintRecipe == 0)
            {
            }
            else if (isAutoPrintRecipe == 1 || isAutoPrintRecipe == 3)
            {
                this.PrintRecipe(false);
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

            return 0;
        }

        #region 补挂号费

        /// <summary>
        /// 急诊挂号级别
        /// </summary>
        string emergRegLevl = "";

        /// <summary>
        /// 职级对应的挂号级别
        /// </summary>
        string regLevl_DoctLevl = "";

        /// <summary>
        /// 急诊留观补收费用
        /// </summary>
        ArrayList alEmergencyFee = new ArrayList();

        /// <summary>
        /// 获取补挂号项目
        /// </summary>
        /// <returns></returns>
        private int GetSupplyItem()
        {
            //之后加控制参数吧
            oper = this.managerIntegrate.GetEmployeeInfo(this.GetReciptDoct().ID);

            #region 急诊费

            //急诊诊查费
            emergRegItem = new Neusoft.HISFC.Models.Fee.Item.Undrug();

            emergRegLevl = "";
            ArrayList emergRegLevlList = this.conManager.GetList("EmergencyLevel");
            if (emergRegLevlList == null || emergRegLevlList.Count == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取急诊号失败！" + conManager.Err);
                //return -1;
            }
            else if (emergRegLevlList.Count > 0)
            {
                emergRegLevl = ((Neusoft.FrameWork.Models.NeuObject)emergRegLevlList[0]).ID.Trim();
            }
            if (string.IsNullOrEmpty(emergRegLevl))
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取急诊号错误，请联系信息科！");
                //return -1;
            }

            Neusoft.FrameWork.Models.NeuObject emergRegConst = this.conManager.GetConstant("REGLEVEL_DIAGFEE", emergRegLevl);
            if (emergRegConst == null || string.IsNullOrEmpty(emergRegConst.ID))
            {
                ucOutPatientItemSelect1.MessageBoxShow("没有维护急诊号对应的诊查费！");
                //return -1;
            }

            if (this.CheckItem(emergRegConst.Name.Trim(), ref errInfo, ref emergRegItem) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("急诊号" + errInfo);
                //return -1;
            }

            #endregion

            #region 医生职称对应的诊查费

            diagItem = new Neusoft.HISFC.Models.Fee.Item.Undrug();
            string regLevl = "";
            string diagItemCode = "";
            if (this.regManagement.GetSupplyRegInfo(oper.ID, oper.Level.ID.ToString(), GetReciptDept().ID, ref regLevl, ref diagItemCode) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow(regManagement.Err);
                //return -1;
            }
            regLevl_DoctLevl = regLevl;

            if (this.CheckItem(diagItemCode, ref errInfo, ref diagItem) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("医生的职级[" + oper.Level.Name + "]对应的诊查费项目" + errInfo);
                //return -1;
            }

            #endregion

            #region 查找差额项目

            diffDiagItem = null;

            string diffDiagItemCode = "";
            ArrayList diffDiagConstList = this.conManager.GetList("DiffDiagItem");
            if (diffDiagConstList == null || diffDiagConstList.Count == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取挂号差额项目失败！" + conManager.Err);
                return -1;
            }
            else if (diffDiagConstList.Count > 0)
            {
                diffDiagItemCode = ((Neusoft.FrameWork.Models.NeuObject)diffDiagConstList[0]).ID.Trim();
            }
            if (!string.IsNullOrEmpty(diffDiagItemCode))
            {
                if (this.CheckItem(diffDiagItemCode, ref errInfo, ref diffDiagItem) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("挂号差额项目" + errInfo);
                    //return -1;
                }
            }

            #endregion

            #region 补收的挂号费项目

            regItem = new Neusoft.HISFC.Models.Fee.Item.Undrug();

            string regItemCode = "";
            ArrayList regConstList = this.conManager.GetList("RegFeeItem");
            if (regConstList == null || regConstList.Count == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取挂号费项目失败！" + conManager.Err);
                //return -1;
            }
            else if (regConstList.Count > 0)
            {
                regItemCode = ((Neusoft.FrameWork.Models.NeuObject)regConstList[0]).ID.Trim();
            }

            if (this.CheckItem(regItemCode, ref errInfo, ref regItem) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("挂号费项目" + errInfo);
                //return -1;
            }
            #endregion

            #region 免挂号费的科室

            ArrayList alNoSupplyRegDept = this.conManager.GetList("NoSupplyRegDept");
            if (alNoSupplyRegDept == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow(this.conManager.Err);
                //return -1;
            }

            foreach (Neusoft.HISFC.Models.Base.Const obj in alNoSupplyRegDept)
            {
                if (!hsNoSupplyRegDept.Contains(obj.ID) && obj.IsValid)
                {
                    hsNoSupplyRegDept.Add(obj.ID, obj);
                }
            }

            #endregion

            #region 急诊留观费用

            alEmergencyFee = new ArrayList();

            Neusoft.HISFC.Models.Fee.Item.Undrug supplyItem;
            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList emergencyFeeItem = null;

            string supplyItemCode = "";
            ArrayList emergencyConstList = this.conManager.GetList("EmergencyFeeItem");
            if (emergencyConstList == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("获取急诊留观项目失败！" + conManager.Err);
                //return -1;
            }
            else if (emergencyConstList.Count == 0)
            {
                return 0;
            }

            for (int i = 0; i < emergencyConstList.Count; i++)
            {
                supplyItem = new Neusoft.HISFC.Models.Fee.Item.Undrug();
                supplyItemCode = ((Neusoft.FrameWork.Models.NeuObject)emergencyConstList[i]).ID.Trim();

                if (this.CheckItem(supplyItemCode, ref errInfo, ref supplyItem) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("急诊留观项目" + errInfo);
                    //return -1;
                }
                emergencyFeeItem = this.SetSupplyFeeItemListByItem(supplyItem, ref errInfo);

                if (emergencyFeeItem == null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                    //return -1;
                }

                //对于补收的费用做个标记
                //emergencyFeeItem.UndrugComb.ID = this.oper.ID;
                emergencyFeeItem.UndrugComb.Name = "急诊留观费";
                alEmergencyFee.Add(emergencyFeeItem);
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 当前患者的挂号级别信息
        /// </summary>
        Neusoft.HISFC.Models.Registration.RegLvlFee regLevlFee = new Neusoft.HISFC.Models.Registration.RegLvlFee();

        /// <summary>
        /// 补收挂号费项目
        /// </summary>
        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList regFeeItem = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();

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
        /// 补收挂号费:挂号费+诊查费
        ///{EF052C04-D357-4409-84E5-3E6102766746}
        /// </summary>
        /// <param name="alSupplyFee">补收的费用列表</param>
        /// <param name="errInfo">错误信息</param>
        /// <param name="isReged">是否已收过挂号费</param>
        /// <returns></returns>
        private int SetSupplyRegFee(ref ArrayList alSupplyFee, ref string errInfo, bool isFee)
        {
            try
            {
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

                if (!string.IsNullOrEmpty(Patient.IDCard) && this.regManagement.CheckIsEmployee(Patient.IDCard))
                {
                    isEmpl = true;
                }
                else
                {
                    isEmpl = false;
                }

                bool isEmerg = this.regManagement.IsEmergency(this.GetReciptDept().ID);

                this.isEmergency = Patient.DoctorInfo.Templet.RegLevel.IsEmergency;

                #region 合同单位和挂号级别对应的挂号费

                if (string.IsNullOrEmpty(Patient.DoctorInfo.Templet.RegLevel.ID))
                {
                    Patient.DoctorInfo.Templet.RegLevel = this.regManagement.GetRegLevl(this.GetReciptDept().ID, oper.ID, this.oper.Level.ID);
                    if (Patient.DoctorInfo.Templet.RegLevel == null)
                    {
                        errInfo = "获取挂号级别出错：" + this.regManagement.Err;
                    }
                }

                if (string.IsNullOrEmpty(Patient.DoctorInfo.Templet.RegLevel.ID))
                {
                    errInfo = "补收挂号费错误，挂号级别为空！";
                }

                regLevlFee = this.regManagement.GetRegLevelByPactCode(Patient.Pact.ID, Patient.DoctorInfo.Templet.RegLevel.ID);
                if (regLevlFee == null)
                {
                    errInfo = "由合同单位和挂号级别获取挂号费失败！请联系信息科重新维护" + regManagement.Err;
                    return 0;
                }

                #endregion

                {
                    #region 一般情况

                    #region 院内职工处理

                    if (isEmpl && emplFreeRegType == 3)
                    {
                        return 1;
                    }

                    #endregion

                    //如果已经收过挂号费，查询差价
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

                            //如果挂号级别对应的挂号费为0  则不补收
                            if (regLevlFee.RegFee <= 0)
                            {
                                isCanSupplyRegFee = false;
                            }

                            #region 院内职工

                            ArrayList list = accountMgr.GetMarkByCardNo(Patient.PID.CardNO, "Empl_ CARD", "1");
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

                        if (isEmpl && emplFreeRegType == 2)
                        {

                        }
                        else
                        {
                            //只有合同单位中的诊金不为0时，才补收
                            if (regLevlFee.OwnDigFee > 0)
                            {
                                //患者挂号时诊金为零，则按照医生职级对应的诊查费项目补收
                                if (Patient.RegLvlFee.OwnDigFee == 0)
                                {
                                    Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList diagFeeItem = null;

                                    //急诊时间段，存在其他职级和急诊对应诊金相同时，按照急诊收取
                                    if (isEmerg && emergRegItem != null && diagItem != null
                                        && emergRegItem.Price >= diagItem.Price)
                                    {
                                        diagFeeItem = this.SetSupplyFeeItemListByItem(emergRegItem, ref errInfo);
                                        if (diagFeeItem == null)
                                        {
                                            return -1;
                                        }
                                    }
                                    else if (diagItem != null)
                                    {
                                        diagFeeItem = this.SetSupplyFeeItemListByItem(diagItem, ref errInfo);
                                        if (diagFeeItem == null)
                                        {
                                            return -1;
                                        }
                                    }

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
                                    if (diffDiagItem != null & !string.IsNullOrEmpty(diffDiagItem.ID))
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

                                        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList diffDiagFeeItem = null;

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

                        #endregion
                    }
                    //未挂号的全补
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

                        ArrayList list = accountMgr.GetMarkByCardNo(Patient.PID.CardNO, "Empl_ CARD", "1");
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

                        if (isEmpl && emplFreeRegType == 2)
                        {

                        }
                        else
                        {
                            //该患者合同单位中诊金维护为0,则认为是减免诊查费，不再补收
                            if (regLevlFee.OwnDigFee > 0)
                            {
                                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList diagFeeItem = null;

                                //补挂号的，此处需要更新挂号信息
                                string regLevlCode = "";
                                //急诊时间段，存在其他职级和急诊对应诊金相同时，按照急诊收取
                                if (isEmerg && emergRegItem != null && diagItem != null && emergRegItem.Price >= diagItem.Price)
                                {
                                    diagFeeItem = this.SetSupplyFeeItemListByItem(emergRegItem, ref errInfo);
                                    if (diagFeeItem == null)
                                    {
                                        return -1;
                                    }

                                    regLevlCode = this.emergRegLevl;
                                }
                                else if (diagItem != null)
                                {
                                    diagFeeItem = this.SetSupplyFeeItemListByItem(diagItem, ref errInfo);
                                    if (diagFeeItem == null)
                                    {
                                        return -1;
                                    }

                                    regLevlCode = this.regLevl_DoctLevl;
                                }

                                #region 修改患者挂号级别信息
                                if (regLevlCode != Patient.DoctorInfo.Templet.RegLevel.ID)
                                {
                                    Neusoft.HISFC.Models.Registration.RegLevel regLevlObj = this.regManagement.QueryRegLevelByCode(regLevlCode);
                                    if (regLevlObj == null)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow("查询挂号级别错误，编码[" + regLevlCode + "]！请联系信息科重新维护" + regManagement.Err);
                                        return -1;
                                    }
                                    Patient.DoctorInfo.Templet.RegLevel = regLevlObj;
                                }
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
                        #endregion
                    }
                    #endregion
                }
                Patient.DoctorInfo.Templet.RegLevel.IsEmergency = isEmergency;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
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
        private int CheckItem(string itemCode, ref string err, ref Neusoft.HISFC.Models.Fee.Item.Undrug itemObj)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                err = "维护错误，请联系信息科！";
                return -1;
            }

            itemObj = this.feeManagement.GetItem(itemCode);
            if (itemObj == null)
            {
                err = "查找项目失败！" + this.feeManagement.Err;
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
            return 1;
        }

        /// <summary>
        /// 设置补挂号的费用明细信息
        /// {FB95CE54-97CE-467a-865F-4B0A6FD01BB3}
        /// </summary>
        /// <param name="item"></param>
        private Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList SetSupplyFeeItemListByItem(Neusoft.HISFC.Models.Fee.Item.Undrug item, ref string errInfo)
        {
            try
            {
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();

                if (item.Qty == 0)
                {
                    item.Qty = 1;
                }
                feeitemlist.Item = item;
                feeitemlist.Item.Qty = item.Qty;
                feeitemlist.Item.PackQty = 1;
                feeitemlist.CancelType = Neusoft.HISFC.Models.Base.CancelTypes.Valid;
                feeitemlist.Patient.ID = Patient.ID;//门诊流水号
                feeitemlist.Patient.PID.CardNO = Patient.PID.CardNO;//门诊卡号 
                feeitemlist.Order.ID = Classes.Function.GetNewOrderID(ref errInfo);

                feeitemlist.ChargeOper.ID = this.GetReciptDoct().ID;
                feeitemlist.Order.Combo.ID = this.OrderManagement.GetNewOrderComboID();

                feeitemlist.ExecOper.Dept = this.GetReciptDept();

                feeitemlist.FT.OwnCost = Neusoft.FrameWork.Public.String.FormatNumber(feeitemlist.Item.Qty * feeitemlist.Item.Price, 2);
                feeitemlist.FT.TotCost = feeitemlist.FT.OwnCost;
                feeitemlist.FeePack = "1";

                feeitemlist.Days = 1;//草药付数
                feeitemlist.RecipeOper.Dept = this.GetReciptDept();//开方科室信息
                feeitemlist.RecipeOper.Name = this.GetReciptDoct().Name;//开方医生信息
                feeitemlist.RecipeOper.ID = this.GetReciptDoct().ID;

                feeitemlist.Order.Item.ItemType = item.ItemType;//是否药品
                feeitemlist.PayType = Neusoft.HISFC.Models.Base.PayTypes.Charged;//划价状态

                ((Neusoft.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = this.OrderManagement.GetDateTimeFromSysDateTime();
                ((Neusoft.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = feeitemlist.RecipeOper.Dept;//登记科室
                feeitemlist.TransType = Neusoft.HISFC.Models.Base.TransTypes.Positive;//交易类型

                //收费序列号，有费用业务层统一生成
                //feeitemlist.RecipeSequence = feeManagement.GetRecipeSequence();
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
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemObj in alCharge)
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

        #region 诊出

        public int DiagOut()
        {
            Classes.LogManager.Write("开始诊出功能!");

            string errInfo = "";

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.managerAssign.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            this.regManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (this.DiagOut(ref errInfo) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            //更新患者状态为已诊后，更改基本信息中患者看诊状态
            this.Patient.IsSee = true;

            #region 账户扣取挂号费

            if (this.isAccountMode)
            {
                //正常挂号患者都补收费用
                if (this.Patient.PVisit.InState.ID.ToString() == "N")
                {
                    if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
                    {
                        Neusoft.HISFC.BizProcess.Integrate.AccountFee.OutPatientFeeManage accountManager = new Neusoft.HISFC.BizProcess.Integrate.AccountFee.OutPatientFeeManage();
                        Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                        accountManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

                        int iRes = accountManager.ChargeFee(this.Patient, alSupplyFee, out errInfo);
                        if (iRes <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n 可能原因是：账户余额不足，请患者到收费处充值后缴费！");
                        }
                        else
                        {
                            Neusoft.FrameWork.Management.PublicTrans.Commit();
                            //ucOutPatientItemSelect1.MessageBoxShow("扣取挂号费和诊金成功！");
                            this.Patient.IsFee = true;
                        }

                    }
                }
            }
            #endregion

            Classes.LogManager.Write("结束诊出功能!");
            return 1;
        }

        /// <summary>
        /// 是否允许继续开立
        /// </summary>
        /// <returns></returns>
        public bool CheckCanAdd()
        {
            try
            {
                string strSQL = @"select count(*)
                                    from met_ord_recipedetail m
                                    where m.clinic_code='{0}'
                                    and m.status!='0'
                                    and m.see_no='{1}'";
                strSQL = string.Format(strSQL, currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                string rev = this.OrderManagement.ExecSqlReturnOne(strSQL, "0");
                if (Neusoft.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(this, "该处方已经收费，请新增处方开立！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
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

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            Employee empl = this.OrderManagement.Operator as Employee;

            int iReturn = -1;
            DateTime now = this.OrderManagement.GetDateTimeFromSysDateTime();
            //Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            //Neusoft.FrameWork.Management.Transaction t = new Neusoft.FrameWork.Management.Transaction(orderManagement.Connection);
            //t.BeginTransaction();
            ////设置事务
            //this.managerAssign.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //this.regManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = this.OrderManagement.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//获得新的看诊序号
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    errInfo = "获取看诊序号失败：" + OrderManagement.Err;
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
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj in alSupplyFee)
                {
                    alFeeItem.Add(feeItemObj);
                }

                //alFeeItem.AddRange(this.alSupplyFee);
            }

            //收完挂号费后，更新挂号表已收费状态，避免多次补收挂号费
            if (this.regManagement.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                errInfo = conManager.Err;
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
                if (this.isAccountMode)
                {
                    bool isAccount = false;
                    #region 账户判断
                    if (isAccountTerimal)
                    {
                        decimal vacancy = 0m;
                        if (this.Patient.IsAccount)
                        {

                            if (feeManagement.GetAccountVacancy(this.Patient.PID.CardNO, ref vacancy) <= 0)
                            {
                                errInfo = feeManagement.Err;
                                return -1;
                            }
                            isAccount = true;
                        }
                    }
                    #endregion

                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    if (isAccountTerimal && isAccount)
                    {
                        rev = feeManagement.SetChargeInfoToAccount(this.Patient, alFeeItem, now, ref errInfo);
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
                                rev = feeManagement.SetChargeInfo(this.Patient, alFeeItem, now, ref errInfo);
                                if (rev == false)
                                {
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            rev = feeManagement.SetChargeInfo(this.Patient, alFeeItem, now, ref errInfo);
                            if (rev == false)
                            {
                                return -1;
                            }
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
            if (isUseNurseArray && this.deptHelper.GetObjectFromID(empl.Dept.ID) != null && !this.Patient.IsSee)
            {
                if (this.currentRoom != null)
                {
                    iReturn = this.managerAssign.UpdateAssign(this.currentRoom.ID, this.Patient.ID, now, empl.ID);
                    if (iReturn < 0)
                    {
                        errInfo = "更新分诊标志出错！";

                        return -1;
                    }
                }
            }
            #endregion

            #region 更新看诊

            //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                iReturn = this.regManagement.UpdateSeeDone(this.Patient.ID);
                if (iReturn < 0)
                {
                    errInfo = "更新看诊标志出错！";

                    return -1;
                }

                iReturn = this.regManagement.UpdateDept(this.Patient.ID, empl.Dept.ID, empl.ID);
                if (iReturn < 0)
                {
                    errInfo = "更新看诊科室、医生出错！";

                    return -1;
                }
            }
            #endregion

            //{44832DAC-80CF-41e6-BD54-6E8DB45E4790} 修正最后没有提交的bug
            //Neusoft.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        #endregion

        /// <summary>
        /// 补录挂号信息
        /// </summary>
        /// <param name="regInfo"></param>
        /// <returns></returns>
        private int AddRegInfo(Neusoft.HISFC.Models.Registration.Register regInfo)
        {
            //根据挂号表里isfee标记，判断是不是系统补挂号的记录
            Neusoft.HISFC.Models.Registration.Register regTemp = this.regManagement.GetByClinic(regInfo.ID);
            if (regTemp == null || string.IsNullOrEmpty(regTemp.ID))
            {
                //补挂号
                if (this.regManagement.Insert(regInfo) == -1)
                {
                    errInfo = "将补挂号信息插入挂号表出错" + regManagement.Err;
                    return -1;
                }

                //更新体征信息
                if (this.OrderManagement.UpdateHealthInfo(regInfo.Height, regInfo.Weight, regInfo.SBP, regInfo.DBP, regInfo.ID, regInfo.Temperature, regInfo.BloodGlu) == -1)
                {
                    errInfo = "更新患者体征信息错误：" + OrderManagement.Err;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 初始化电子申请单接口
        /// {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504
        /// </summary>
        private int InitPACSApplyInterface()
        {
            try
            {
                PACSApplyInterface = new Neusoft.ApplyInterface.HisInterface();
                return 0;
            }
            catch
            {
                return -1;
            }
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
            Neusoft.HISFC.Models.Order.OutPatient.Order ord;
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex >= 0 && this.neuSpread1.ActiveSheet.Rows.Count > 0)
            {
                ord = this.neuSpread1.ActiveSheet.ActiveRow.Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
                #region {071AEF5B-B38D-4061-A460-B0137A01E812}
                //if (ord != null && ord.Status != null && ord.Status == 0)
                if (ord != null && ord.Item.SysClass.ID.ToString() == "PCC" && ord.Status == 0)
                #endregion
                {//{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
                    this.ModifyHerbal();
                }
                #region {071AEF5B-B38D-4061-A460-B0137A01E812}
                else
                {
                    using (Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder uc = new Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder(true, Neusoft.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
                    {
                        uc.refreshGroup += new Neusoft.HISFC.Components.Order.Controls.RefreshGroupTree(uc_refreshGroup);
                        uc.Patient = new Neusoft.HISFC.Models.RADT.PatientInfo();//
                        uc.IsClinic = true;
                        Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                        uc.SetFocus();

                        Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                        if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                        {
                            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order info in uc.AlOrder)
                            {
                                //{AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                                //info.DoseOnce = info.Qty;
                                //info.Qty = info.Qty * info.HerbalQty;

                                this.AddNewOrder(info, 0);
                            }
                            uc.Clear();

                            RefreshOrderState();
                            this.RefreshCombo();
                        }
                    }
                }
                #endregion
            }
            else
            {
                using (Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder uc = new Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder(true, Neusoft.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
                {
                    uc.refreshGroup += new Neusoft.HISFC.Components.Order.Controls.RefreshGroupTree(uc_refreshGroup);
                    uc.Patient = new Neusoft.HISFC.Models.RADT.PatientInfo();//
                    uc.IsClinic = true;
                    Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                    uc.SetFocus();

                    Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                    if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                    {
                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order info in uc.AlOrder)
                        {
                            //{AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                            //info.DoseOnce = info.Qty;
                            //info.Qty = info.Qty * info.HerbalQty;

                            this.AddNewOrder(info, 0);
                        }
                        uc.Clear();

                        RefreshOrderState();
                        this.RefreshCombo();
                    }
                }
            }
            return 1;
        }

        void uc_refreshGroup()
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
            Neusoft.HISFC.Models.Order.OutPatient.Order mnuSelectedOrder = null;

            FarPoint.Win.Spread.Model.CellRange c = neuSpread1.GetCellFromPixel(0, 0, e.X, e.Y);

            #region 左键菜单

            //左键用于选择同组项目
            if (IsDesignMode || EditGroup)
            {
                if (c.Row > 0)
                {
                    string combNo = "";
                    Neusoft.HISFC.Models.Order.OutPatient.Order orderObj = null;
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
                //FarPoint.Win.Spread.Model.CellRange c = neuSpread1.GetCellFromPixel(0, 0, e.X, e.Y);
                if (c.Row >= 0)
                {
                    //this.neuSpread1.ActiveSheet.ActiveRowIndex = c.Row;
                    this.neuSpread1.ActiveSheet.AddSelection(c.Row, 0, 1, 1);
                    ActiveRowIndex = c.Row;
                    this.neuSpread1.ActiveSheet.ActiveRowIndex = c.Row;
                    this.SelectionChanged();
                }
                else
                {
                    ActiveRowIndex = -1;
                }
                if (ActiveRowIndex < 0)
                {
                    #region {DF8058FF-72C0-404f-8F36-6B4057B6F6CD}
                    if (this.bIsDesignMode)
                    {
                        #region 粘贴医嘱
                        //if (Neusoft.HISFC.Components.Order.Classes.HistoryOrderClipboard.IsHaveCopyData)
                        //{
                        ToolStripMenuItem mnuPasteOrder = new ToolStripMenuItem("粘贴医嘱");
                        mnuPasteOrder.Click += new EventHandler(mnuPasteOrder_Click);
                        this.contextMenu1.Items.Add(mnuPasteOrder);
                        this.contextMenu1.Show(this.neuSpread1, new Point(e.X, e.Y));
                        //}
                        #endregion
                    }
                    #endregion
                    return;
                }

                mnuSelectedOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, 0);//(Neusoft.HISFC.Models.Order.Order)this.fpSpread1.ActiveSheet.Rows[ActiveRowIndex].Tag;

                if (mnuSelectedOrder != null && mnuSelectedOrder.Item.SysClass.ID.ToString() == "UL" && mnuSelectedOrder.Status == 0)
                {
                    ////ToolStripMenuItem mnuLisCard = new ToolStripMenuItem();
                    ////mnuLisCard.Text = "打印检验申请单[快捷键:F12]";
                    ////mnuLisCard.Click += new EventHandler(mnuLisCard_Click);
                    ////this.contextMenu1.Items.Add(mnuLisCard);
                }
                if (mnuSelectedOrder != null && mnuSelectedOrder.Item.SysClass.ID.ToString() == "UZ" && mnuSelectedOrder.Status == 0)
                {
                    ////ToolStripMenuItem mnuDealCard = new ToolStripMenuItem();
                    ////mnuDealCard.Text = "打印治疗申请单[快捷键:F12]";
                    ////mnuDealCard.Click += new EventHandler(mnuDealCard_Click);
                    ////this.contextMenu1.Items.Add(mnuDealCard);
                }
                //if (mnuSelectedOrder != null && mnuSelectedOrder.Item.IsPharmacy)
                if (mnuSelectedOrder != null && mnuSelectedOrder.Item.ItemType == EnumItemType.Drug)
                {
                    ////ToolStripMenuItem mnuIMCard = new ToolStripMenuItem();
                    ////mnuIMCard.Text = "打印输液治疗单[快捷键:F12]";
                    ////mnuIMCard.Click += new EventHandler(mnuIMCard_Click);
                    ////this.contextMenu1.Items.Add(mnuIMCard);
                }
                if (this.bIsDesignMode)
                {
                    #region 院注次数
                    //if (mnuSelectedOrder.Item.IsPharmacy && 
                    //    (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 4) && 
                    //    mnuSelectedOrder.InjectCount == 0 &&
                    //    Classes.Function.hsUsageAndSub.Contains(mnuSelectedOrder.Usage.ID))
                    if (mnuSelectedOrder.Item.ItemType == EnumItemType.Drug &&
                      (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 4) &&
                      mnuSelectedOrder.InjectCount == 0 &&
                      Classes.Function.CheckIsInjectUsage(mnuSelectedOrder.Usage.ID)
                        )
                    {
                        ToolStripMenuItem mnuInjectNum = new ToolStripMenuItem();//院注次数
                        mnuInjectNum.Click += new EventHandler(mnumnuInjectNum_Click);

                        mnuInjectNum.Text = "添加院注次数[快捷键:F5]";
                        this.contextMenu1.Items.Add(mnuInjectNum);
                    }

                    //if (mnuSelectedOrder.Item.IsPharmacy && 
                    //    (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 4) && 
                    //    mnuSelectedOrder.InjectCount > 0)
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
                    if (mnuSelectedOrder.Status == 0)
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

                    #region 存组套{C6E229AC-A1C4-4725-BBBB-4837E869754E}

                    ToolStripMenuItem mnuSaveGroup = new ToolStripMenuItem("存组套");//存组套
                    mnuSaveGroup.Click += new EventHandler(mnuSaveGroup_Click);

                    this.contextMenu1.Items.Add(mnuSaveGroup);
                    #endregion

                    #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} 添加合理用药右键菜单

                    if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
                    {
                        //int i = 0;
                        //ToolStripMenuItem menuPass = new ToolStripMenuItem("合理用药");
                        //this.contextMenu1.Items.Add(menuPass);

                        //ToolStripMenuItem m_al1ergic = new ToolStripMenuItem("过敏史/病生状态");
                        //m_al1ergic.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_al1ergic);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("22") == 0)
                        //{
                        //    m_al1ergic.Enabled = false;
                        //}

                        //ToolStripMenuItem m_cpr = new ToolStripMenuItem("药物临床信息参考");
                        //m_cpr.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_cpr);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("101") == 0)
                        //{
                        //    m_cpr.Enabled = false;
                        //}

                        //ToolStripMenuItem m_directions = new ToolStripMenuItem("药品说明书");
                        //m_directions.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_directions);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("102") == 0)
                        //{
                        //    m_directions.Enabled = false;
                        //}

                        //ToolStripMenuItem m_chp = new ToolStripMenuItem("中国药典");
                        //m_chp.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_chp);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("107") == 0)
                        //{
                        //    m_chp.Enabled = false;
                        //}

                        //ToolStripMenuItem m_cpe = new ToolStripMenuItem("病人用药教育");
                        //m_cpe.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_cpe);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("103") == 0)
                        //{
                        //    m_cpe.Enabled = false;
                        //}

                        //ToolStripMenuItem m_checkres = new ToolStripMenuItem("药物检验值");
                        //m_checkres.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_checkres);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("104") == 0)
                        //{
                        //    m_checkres.Enabled = false;
                        //}

                        //ToolStripMenuItem m_lmim = new ToolStripMenuItem("临床检验信息参考");
                        //m_lmim.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_lmim);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("220") == 0)
                        //{
                        //    m_lmim.Enabled = false;
                        //}

                        //ToolStripMenuItem menuAllergn = new ToolStripMenuItem("-");
                        //menuAllergn.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, menuAllergn);
                        //i++;

                        //#region 药品专项信息

                        //ToolStripMenuItem menuSpecialInfo = new ToolStripMenuItem("专项信息");
                        //menuPass.DropDownItems.Insert(i, menuSpecialInfo);
                        //i++;
                        //int j = 0;

                        //ToolStripMenuItem m_ddim = new ToolStripMenuItem("药物-药物相互作用");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_ddim);
                        //m_ddim.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("201") == 0)
                        //{
                        //    m_ddim.Enabled = false;
                        //}

                        //ToolStripMenuItem m_dfim = new ToolStripMenuItem("药物-食物相互作用");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_dfim);
                        //m_dfim.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("202") == 0)
                        //{
                        //    m_dfim.Enabled = false;
                        //}

                        //ToolStripMenuItem m_line7 = new ToolStripMenuItem("-");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_line7);
                        //j++;

                        //ToolStripMenuItem m_matchres = new ToolStripMenuItem("国内注射剂体外配伍");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_matchres);
                        //m_matchres.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("203") == 0)
                        //{
                        //    m_matchres.Enabled = false;
                        //}

                        //ToolStripMenuItem m_trisselres = new ToolStripMenuItem("国外注射剂体外配伍");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_trisselres);
                        //m_trisselres.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("204") == 0)
                        //{
                        //    m_trisselres.Enabled = false;
                        //}

                        //ToolStripMenuItem m_line8 = new ToolStripMenuItem("-");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_line8);
                        //j++;

                        //ToolStripMenuItem m_ddcm = new ToolStripMenuItem("禁忌症");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_ddcm);
                        //m_ddcm.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("205") == 0)
                        //{
                        //    m_ddcm.Enabled = false;
                        //}
                        //ToolStripMenuItem m_side = new ToolStripMenuItem("副作用");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_side);
                        //m_side.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("206") == 0)
                        //{
                        //    m_side.Enabled = false;
                        //}

                        //ToolStripMenuItem m_line9 = new ToolStripMenuItem("-");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_line9);
                        //j++;

                        //ToolStripMenuItem m_geri = new ToolStripMenuItem("老年人用药");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_geri);
                        //m_geri.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("207") == 0)
                        //{
                        //    m_geri.Enabled = false;
                        //}
                        //ToolStripMenuItem m_pedi = new ToolStripMenuItem("儿童用药");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_pedi);
                        //m_pedi.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("208") == 0)
                        //{
                        //    m_pedi.Enabled = false;
                        //}
                        //ToolStripMenuItem m_preg = new ToolStripMenuItem("妊娠期用药");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_preg);
                        //m_preg.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("209") == 0)
                        //{
                        //    m_preg.Enabled = false;
                        //}

                        //ToolStripMenuItem m_lact = new ToolStripMenuItem("哺乳期用药");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_lact);
                        //m_lact.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("210") == 0)
                        //{
                        //    m_lact.Enabled = false;
                        //}

                        //#endregion

                        //ToolStripMenuItem m_line2 = new ToolStripMenuItem("-");
                        //menuPass.DropDownItems.Insert(i, m_line2);
                        //i++;

                        //ToolStripMenuItem m_centerinfo = new ToolStripMenuItem("医药信息中心");
                        //m_centerinfo.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_centerinfo);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("106") == 0)
                        //{
                        //    m_centerinfo.Enabled = false;
                        //}

                        //ToolStripMenuItem m_line3 = new ToolStripMenuItem("-");
                        //menuPass.DropDownItems.Insert(i, m_line3);
                        //i++;

                        //ToolStripMenuItem menuDrug = new ToolStripMenuItem("药品配对信息");
                        //menuDrug.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, menuDrug);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("13") == 0)
                        //{
                        //    menuDrug.Enabled = false;
                        //}

                        //ToolStripMenuItem m_routematch = new ToolStripMenuItem("给药途径配对信息");
                        //m_routematch.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_routematch);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("14") == 0)
                        //{
                        //    m_routematch.Enabled = false;
                        //}

                        //ToolStripMenuItem m_hospital_drug = new ToolStripMenuItem("医院药品信息");
                        //m_hospital_drug.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_hospital_drug);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("105") == 0)
                        //{
                        //    m_hospital_drug.Enabled = false;
                        //}

                        //ToolStripMenuItem m_line4 = new ToolStripMenuItem("-");
                        //menuPass.DropDownItems.Insert(i, m_line4);
                        //i++;

                        //ToolStripMenuItem m_system_set = new ToolStripMenuItem("系统设置");
                        //m_system_set.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_system_set);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("11") == 0)
                        //{
                        //    m_system_set.Enabled = false;
                        //}

                        //ToolStripMenuItem m_line5 = new ToolStripMenuItem("-");
                        //menuPass.DropDownItems.Insert(i, m_line5);
                        //i++;

                        //ToolStripMenuItem m_studydrug = new ToolStripMenuItem("用药研究");
                        //m_studydrug.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_studydrug);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("12") == 0)
                        //{
                        //    m_studydrug.Enabled = false;
                        //}

                        //ToolStripMenuItem m_line6 = new ToolStripMenuItem("-");
                        //menuPass.DropDownItems.Insert(i, m_line6);
                        //i++;

                        //ToolStripMenuItem m_warn = new ToolStripMenuItem("警告");
                        //m_warn.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_warn);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("11") == 0)
                        //{
                        //    m_warn.Enabled = false;
                        //}

                        //ToolStripMenuItem m_checkone = new ToolStripMenuItem("审查");
                        //m_checkone.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_checkone);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("3") == 0)
                        //{
                        //    m_checkone.Enabled = false;
                        //}

                    }

                    #endregion

                    //#region 重打电子申请单 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504 
                    //string isUseDL = feeManagement.GetControlValue("200212", "0");
                    //if (isUseDL == "1")
                    //{
                    //if (mnuSelectedOrder.ApplyNo != null && mnuSelectedOrder.ApplyNo != "")
                    //{
                    //    ToolStripMenuItem mnuPACSApply = new ToolStripMenuItem("重打电子申请单");//下移动
                    //    mnuPACSApply.Click += new EventHandler(mnuPACSApply_Click);
                    //    this.contextMenu1.Items.Add(mnuPACSApply);
                    //}
                    //}
                    //#endregion

                }
                else
                {
                    #region {7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}
                    //if (this.bOrderHistory)
                    //{
                    //    ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem("复制到开立界面");//批注
                    //    ////mnuCopyOrder.Click += new EventHandler(mnuCopyOrder_Click);
                    //    this.contextMenu1.Items.Add(mnuCopyOrder);
                    //}

                    #region 复制医嘱
                    ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem("复制医嘱");
                    mnuCopyOrder.Click += new EventHandler(mnuCopyOrder_Click);
                    this.contextMenu1.Items.Add(mnuCopyOrder);
                    #endregion

                    #endregion
                }
                #region 添加合理用药右键菜单
                //if (this.EnabledPass && Pass.Pass.PassEnabled && mnuSelectedOrder.Item.IsPharmacy)
                //{
                //    MenuItem menuPass = new MenuItem("合理用药");
                //    this.contextMenu1.MenuItems.Add(menuPass);

                //    MenuItem menuAllergn = new MenuItem("过敏史/病生状态");
                //    menuAllergn.Click += new EventHandler(mnuPass_Click);
                //    menuPass.Items.Add(menuAllergn);

                //    if (Pass.Pass.PassGetState("101") != 0)
                //    {
                //        MenuItem menuCPR = new MenuItem("药物临床信息参考");
                //        menuCPR.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCPR);
                //    }
                //    if (Pass.Pass.PassGetState("102") != 0)
                //    {
                //        MenuItem menuDIR = new MenuItem("药品说明书");
                //        menuDIR.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuDIR);
                //    }
                //    if (Pass.Pass.PassGetState("107") != 0)
                //    {
                //        MenuItem menuCHP = new MenuItem("中国药典");
                //        menuCHP.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCHP);
                //    }
                //    if (Pass.Pass.PassGetState("103") != 0)
                //    {
                //        MenuItem menuCPE = new MenuItem("病人用药教育");
                //        menuCPE.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCPE);
                //    }
                //    if (Pass.Pass.PassGetState("104") != 0)
                //    {
                //        MenuItem menuCHE = new MenuItem("药物检验值");
                //        menuCHE.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCHE);
                //    }
                //    if (Pass.Pass.PassGetState("220") != 0)
                //    {
                //        MenuItem menuLIM = new MenuItem("临床检验信息参考");
                //        menuLIM.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuLIM);
                //    }
                //    #region 药品专项信息
                //    MenuItem menuSpecialInfo = new MenuItem("专项信息");
                //    menuPass.Items.Add(menuSpecialInfo);

                //    if (Pass.Pass.PassGetState("201") != 0)
                //    {
                //        MenuItem menuDDim = new MenuItem("药物-药物相互作用");
                //        menuSpecialInfo.MenuItems.Add(menuDDim);
                //        menuDDim.Click += new EventHandler(mnuPass_Click);
                //    }

                //    if (Pass.Pass.PassGetState("202") != 0)
                //    {
                //        MenuItem menuDFim = new MenuItem("药物-食物相互作用");
                //        menuSpecialInfo.Items.Add(menuDFim);
                //        menuDFim.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("203") != 0)
                //    {
                //        MenuItem menuMACH = new MenuItem("国内注射剂体外配伍");
                //        menuSpecialInfo.Items.Add(menuMACH);
                //        menuMACH.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("204") != 0)
                //    {
                //        MenuItem menuTRI = new MenuItem("国外注射剂体外配伍");
                //        menuSpecialInfo.Items.Add(menuTRI);
                //        menuTRI.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("205") != 0)
                //    {
                //        MenuItem menuDDCM = new MenuItem("禁忌症");
                //        menuSpecialInfo.MenuItems.Add(menuDDCM);
                //        menuDDCM.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("206") != 0)
                //    {
                //        MenuItem menuSID = new MenuItem("副作用");
                //        menuSpecialInfo.Items.Add(menuSID);
                //        menuSID.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("207") != 0)
                //    {
                //        MenuItem menuOLD = new MenuItem("老年人用药");
                //        menuSpecialInfo.Items.Add(menuOLD);
                //        menuOLD.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("208") != 0)
                //    {
                //        MenuItem menuPED = new MenuItem("儿童用药");
                //        menuSpecialInfo.Items.Add(menuPED);
                //        menuPED.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("209") != 0)
                //    {
                //        MenuItem menuPREG = new MenuItem("妊娠期用药");
                //        menuSpecialInfo.Items.Add(menuPREG);
                //        menuPREG.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("210") != 0)
                //    {
                //        MenuItem menuACT = new MenuItem("哺乳期用药");
                //        menuSpecialInfo.Items.Add(menuACT);
                //        menuACT.Click += new EventHandler(mnuPass_Click);
                //    }
                //    #endregion
                //    if (Pass.Pass.PassGetState("106") != 0)
                //    {
                //        MenuItem menuCENter = new MenuItem("医药信息中心");
                //        menuCENter.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuCENter);
                //    }
                //    if (Pass.Pass.PassGetState("13") != 0)
                //    {
                //        MenuItem menuDrug = new MenuItem("药品配对信息");
                //        menuDrug.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuDrug);
                //    }
                //    if (Pass.Pass.PassGetState("14") != 0)
                //    {
                //        MenuItem menuUsage = new MenuItem("给药途径配对信息");
                //        menuUsage.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuUsage);
                //    }
                //    if (Pass.Pass.PassGetState("11") != 0)
                //    {
                //        MenuItem menuSystem = new MenuItem("系统设置");
                //        menuSystem.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuSystem);
                //    }
                //    if (Pass.Pass.PassGetState("12") != 0)
                //    {
                //        MenuItem menuResearch = new MenuItem("用药研究");
                //        menuResearch.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuResearch);
                //    }
                //    if (Pass.Pass.PassGetState("3") != 0)
                //    {
                //        MenuItem menuWarn = new MenuItem("警告");
                //        menuWarn.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuWarn);

                //        if (this.fpSpread1.Sheets[0].Cells[c.Row, GetColumnIndexFromName("警")].Tag != null && this.fpSpread1.Sheets[0].Cells[c.Row, GetColumnIndexFromName("警")].Tag.ToString() != "0")
                //        {
                //            menuWarn.Enabled = true;
                //        }
                //        else
                //        {
                //            menuWarn.Enabled = false;
                //        }
                //    }
                //    if (Pass.Pass.PassGetState("3") != 0)
                //    {
                //        MenuItem menuCheck = new MenuItem("审查");
                //        menuCheck.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCheck);
                //    }

                //}

                #endregion
                this.contextMenu1.Show(this.neuSpread1, new Point(e.X, e.Y));

                Neusoft.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(c.Row, this.neuSpread1.ActiveSheetIndex);
                if (temp != null)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order orderDelete = null;
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

        #region 作废医嘱（收费后医嘱不允许作废，遇到特殊需求再打开）{DFA920BD-AEB2-4371-B501-21CB87558147}
        /// <summary>
        /// 作废医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(this.neuSpread1_Sheet1.ActiveRowIndex, 0);

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

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            this.OrderManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                if (temp == null)
                    continue;

                if (temp.Combo.ID == order.Combo.ID)
                {
                    if (this.OrderManagement.UpdateOrderBeCaceled(temp) < 0)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        ucOutPatientItemSelect1.MessageBoxShow("作废医嘱" + temp.Item.Name + "失败");
                        return;
                    }
                    int oldState = temp.Status;
                    temp.Status = 3;
                    temp.DCOper.ID = this.OrderManagement.Operator.ID;
                    temp.DCOper.OperTime = this.OrderManagement.GetDateTimeFromSysDateTime();
                    this.AddObjectToFarpoint(temp, i, 0, EnumOrderFieldList.Item);

                    if (this.isSaveOrderHistory)
                    {
                        if (this.OrderManagement.InsertOrderChangeInfo(temp) < 0)
                        {
                            temp.Status = oldState;
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            ucOutPatientItemSelect1.MessageBoxShow(errText);
                            return;
                        }
                    }

                    Neusoft.FrameWork.Management.PublicTrans.Commit();

                    #region 电子申请单 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504
                    string isUseDL = feeManagement.GetControlValue("200212", "0");
                    if (isUseDL == "1")
                    {
                        if (order.ApplyNo != null)
                        {
                            if (PACSApplyInterface == null)
                            {
                                if (InitPACSApplyInterface() < 0)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("初始化电子申请单接口时出错！");
                                    return;
                                }
                            }
                            PACSApplyInterface.DeleteApply(order.ApplyNo);
                            //if (PACSApplyInterface.DeleteApply(order.ApplyNo) < 0)
                            //{
                            //    ucOutPatientItemSelect1.MessageBoxShow("作废电子申请单时出错！");
                            //    return -1;
                            //}
                        }
                    }
                    #endregion
                }
            }

            this.RefreshOrderState();
        }
        #endregion

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyAs_Click(object sender, EventArgs e)
        {
            this.neuSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
            if (order == null)
            {
                return;
            }
            ArrayList alCopyList = new ArrayList();
            string ComboNo = this.OrderManagement.GetNewOrderComboID();

            string oldComb = "";
            string newComb = "";

            for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
            {
                //{0817AFF8-A0DC-4a06-BEAD-015BC49AE973}
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                //if (this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Combo.ID == order.Combo.ID)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Clone();
                    o.Patient.Pact = this.currentPatientInfo.Pact;
                    o.Patient.Birthday = this.currentPatientInfo.Birthday;

                    //开立科室和执行科室相同，则认为是本科室执行项目，执行科室重取
                    if (o.ReciptDept.ID == o.ExeDept.ID)
                    {
                        o.ExeDept = new Neusoft.FrameWork.Models.NeuObject();
                    }

                    //if (o.Item.IsPharmacy)
                    if (o.Item.ItemType == EnumItemType.Drug)
                    {
                        if (Classes.Function.FillPharmacyItem(phaIntegrate, ref o) == -1)
                        {
                            return;
                        }

                        //判断缺药、停用
                        Neusoft.HISFC.Models.Pharmacy.Item itemObj = null;
                        string errInfo = "";
                        if (Components.Order.Classes.Function.CheckDrugState(o.StockDept, o.Item, true, ref itemObj, ref errInfo) == -1)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                            return;
                        }
                    }
                    else
                    {
                        if (Classes.Function.FillFeeItem(itemManagement, ref o) == -1)
                        {
                            return;
                        }
                    }
                    DateTime dtNow = DateTime.MinValue;

                    o.Status = 0;
                    o.ID = "";
                    o.SortID = 0;
                    //o.Combo.ID = ComboNo;

                    if (o.Combo.ID != oldComb)
                    {
                        newComb = OrderManagement.GetNewOrderComboID();
                        oldComb = o.Combo.ID;
                        o.Combo.ID = newComb;
                    }
                    else
                    {
                        o.Combo.ID = newComb;
                    }

                    o.EndTime = DateTime.MinValue;
                    o.DCOper.OperTime = DateTime.MinValue;
                    o.DcReason.ID = "";
                    o.DcReason.Name = "";
                    o.DCOper.ID = "";
                    o.DCOper.Name = "";
                    o.ConfirmTime = DateTime.MinValue;
                    o.Nurse.ID = "";
                    dtNow = this.OrderManagement.GetDateTimeFromSysDateTime();
                    o.MOTime = dtNow;
                    if (this.GetReciptDept() != null)
                        o.ReciptDept = this.GetReciptDept().Clone();
                    if (this.GetReciptDoct() != null)
                        o.ReciptDoctor = this.GetReciptDoct().Clone();

                    if (this.GetReciptDoct() != null)
                    {
                        o.Oper.ID = this.GetReciptDoct().ID;
                        o.Oper.ID = this.GetReciptDoct().Name;
                    }

                    o.CurMOTime = o.BeginTime;
                    o.NextMOTime = o.BeginTime;

                    alCopyList.Add(o);
                }
            }

            if (this.IBeforeAddItem != null)
            {
                if (this.IBeforeAddItem.OnBeforeAddItemForOutPatient(this.Patient, this.GetReciptDoct(), this.GetReciptDept(), alCopyList) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(IBeforeAddItem.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            for (int i = 0; i < alCopyList.Count; i++)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order ord = alCopyList[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                #region 判断开立权限

                string error = "";

                int ret = 1;

                //等级药品
                ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(ord, this.OrderManagement.Operator,
                    (OrderManagement.Operator as Neusoft.HISFC.Models.Base.Employee).Dept, Neusoft.HISFC.Models.Base.DoctorPrivType.LevelDrug, true, ref error);

                if (ret <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(error);
                    continue;
                }

                #endregion

                this.AddNewOrder(ord, 0);
            }
            ////SetFeeDisplay(this.Patient, null);

            RefreshOrderState();
            this.RefreshCombo();
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
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

            Neusoft.HISFC.Models.Order.OutPatient.Order upOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex - 1, this.neuSpread1.ActiveSheetIndex).Clone();
            Neusoft.HISFC.Models.Order.OutPatient.Order downOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex).Clone();

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
                Neusoft.HISFC.Models.Order.OutPatient.Order oTmp = null;
                for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                {
                    oTmp = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
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
                    oTmp = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
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

            Neusoft.HISFC.Models.Order.OutPatient.Order upOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex).Clone();
            Neusoft.HISFC.Models.Order.OutPatient.Order downOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex + 1, this.neuSpread1.ActiveSheetIndex).Clone();

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
                Neusoft.HISFC.Models.Order.OutPatient.Order oTmp = null;
                for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                {
                    oTmp = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
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
                    oTmp = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
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
            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
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
            order.Item.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(frm.ModuleName);
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
            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;

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
            if (PACSApplyInterface == null)
            {
                if (InitPACSApplyInterface() < 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("初始化电子申请单接口时出错！");
                    return;
                }
            }
            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;

            if (order.ApplyNo == null || order.ApplyNo == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("此医嘱尚未保存，请先保存！");
                return;
            }

            if (PACSApplyInterface.UpdateApply(order.ApplyNo) < 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("修改重打电子申请单时出错！");
                return;
            }
        }

        /// <summary>
        /// 复制医嘱
        /// {7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyOrder_Click(object sender, EventArgs e)
        {
            this.CopyOrder();
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

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
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

        #region 新加的函数
        protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
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
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "开立")
            {
                this.Add();
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

        private object currentObject = null;
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject == null)
            {
                currentObject = new object();
                lblDisplay.Text = "";
                lblFeeDisplay.Text = "";
                return 0;
            }
            if (neuObject.GetType() == typeof(Neusoft.HISFC.Models.Registration.Register))
            {
                if (currentObject != neuObject)
                    this.Patient = neuObject as Neusoft.HISFC.Models.Registration.Register;
                currentObject = neuObject;
            }
            return 0;
        }
        #endregion

        #region IInterfaceContainer 成员
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[7];
                t[0] = typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint);
                t[1] = typeof(Neusoft.HISFC.BizProcess.Interface.Common.ICheckPrint);//检查申请单
                //{48E6BB8C-9EF0-48a4-9586-05279B12624D}
                t[2] = typeof(Neusoft.HISFC.BizProcess.Interface.IAlterOrder);
                t[3] = typeof(Neusoft.HISFC.BizProcess.Interface.Common.IPacs);
                t[4] = typeof(Neusoft.HISFC.BizProcess.Interface.Order.IReasonableMedicine);
                t[5] = typeof(Neusoft.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee);
                t[6] = typeof(Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob);
                return t;
            }
        }

        /// <summary>
        /// 处方打印
        /// </summary>
        /// <param name="isRecipeView">是否预览打印</param>
        public int PrintRecipe(bool isRecipeView)
        {
            if (this.EditGroup)
            {
                ucOutPatientItemSelect1.MessageBoxShow("您正在编辑组套，此时不支持打印处方！");
                return -1;
            }

            if (iRecipePrint == null)
            {
                iRecipePrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint)) as Neusoft.HISFC.BizProcess.Interface.IRecipePrint;
            }

            if (iRecipePrint == null)
            {
                this.accountMgr.Err = "处方打印接口未实现！";
                this.accountMgr.WriteErr();
                return 1;
                //ucOutPatientItemSelect1.MessageBoxShow("接口未实现");
            }
            else
            {
                ArrayList alRecipe = new ArrayList();
                alRecipe = this.GetRecipeArray(true);
                if (isRecipeView)
                {
                    alRecipe = this.GetRecipeArray(false);
                    if (alRecipe.Count > 0)
                    {
                        if (iRecipePrint.PrintRecipeView(alRecipe) == -1)
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    alRecipe = this.GetRecipeArray(true);
                    if (alRecipe.Count > 0)
                    {
                        if (ucOutPatientItemSelect1.MessageBoxShow("是否打印处方？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            foreach (Neusoft.FrameWork.Models.NeuObject fuck in alRecipe)
                            {
                                iRecipePrint.SetPatientInfo(this.currentPatientInfo);
                                iRecipePrint.RecipeNO = fuck.ID;
                                iRecipePrint.PrintRecipe();
                            }
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 获得药品处方数组
        /// </summary>
        /// <param name="isAfterSaveOrder">是否保存处方后调用</param>
        /// <returns></returns>
        private ArrayList GetRecipeArray(bool isAfterSaveOrder)
        {
            ArrayList alRecipe = new ArrayList();

            if (isAfterSaveOrder)
            {
                alRecipe = this.OrderManagement.GetPhaRecipeNoByClinicNoAndSeeNo(this.currentPatientInfo.ID, this.Patient.DoctorInfo.SeeNO.ToString());
            }
            else
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
                for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
                {
                    order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;
                    if (order != null)
                    {
                        alRecipe.Add(order);
                    }
                }
            }

            return alRecipe;
        }

        #endregion

        /// <summary>
        /// 保存为xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            Neusoft.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[0], SetingFileName);

        }

        #region 合理用药

        //Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;

        ///// <summary>
        ///// 获取药品自定义码
        ///// </summary>
        ///// <param name="itemCode"></param>
        ///// <returns></returns>
        //private string GetPhaUserCode(string itemCode)
        //{
        //    if (hsPhaUserCode != null && hsPhaUserCode.Contains(itemCode))
        //    {
        //        return hsPhaUserCode[itemCode].ToString();
        //    }
        //    else
        //    {
        //        phaItem = this.phaIntegrate.GetItem(itemCode);
        //        if (phaItem != null)
        //        {
        //            return phaItem.UserCode;
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// 初始化IReasonableMedicin
        /// </summary>
        private void InitReasonableMedicine()
        {
            if (this.IReasonableMedicine == null)
            {
                this.IReasonableMedicine = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IReasonableMedicine)) as Neusoft.HISFC.BizProcess.Interface.Order.IReasonableMedicine;
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
        /// 合理用药系统中查看审查结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if (!e.RowHeader && !e.ColumnHeader && e.Column == 0)
            //{
            //    if (!this.IReasonableMedicine.PassEnabled)
            //    {
            //        return;
            //    }

            //    int iSheetIndex = 0;
            //    Neusoft.HISFC.Models.Order.OutPatient.Order info = this.GetObjectFromFarPoint(e.Row, iSheetIndex);
            //    if (info == null)
            //    {
            //        return;
            //    }
            //    if (info.Item.ItemType.ToString() != Neusoft.HISFC.Models.Base.EnumItemType.Drug.ToString())
            //    {
            //        this.IReasonableMedicine.ShowFloatWin(false);
            //        return;
            //    }
            //    this.IReasonableMedicine.ShowFloatWin(false);
            //    if (e.Column == 0)
            //    {
            //        if (this.neuSpread1.Sheets[iSheetIndex].Cells[e.Row, e.Column].Tag != null && this.neuSpread1.Sheets[iSheetIndex].Cells[e.Row, e.Column].Tag.ToString() != "0")
            //        {
            //            this.IReasonableMedicine.PassGetWarnInfo(info.ApplyNo, "1");
            //        }
            //    }
            //}
            //else
            //{
            //    this.IReasonableMedicine.ShowFloatWin(false);
            //}
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.IReasonableMedicine != null && IReasonableMedicine.PassEnabled && this.enabledPass)
            {
                this.PassSetQuery(e);
            }
        }

        /// <summary>
        /// 查询药品合理用药信息
        /// </summary>
        /// <param name="e"></param>
        public void PassSetQuery(FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    return;
            //}

            //if (!e.RowHeader && !e.ColumnHeader && (e.Column == GetColumnIndexFromName("医嘱名称")))
            //{
            //    if (IReasonableMedicine != null&&!this.IReasonableMedicine.PassEnabled)
            //    {
            //        return;
            //    }
            //    int iSheetIndex = 0;
            //    Neusoft.HISFC.Models.Order.OutPatient.Order info = this.GetObjectFromFarPoint(e.Row, iSheetIndex);
            //    if (info == null)
            //    {
            //        return;
            //    }
            //    if (info.Item.ItemType.ToString() != Neusoft.HISFC.Models.Base.EnumItemType.Drug.ToString())
            //    {
            //        this.IReasonableMedicine.ShowFloatWin(false);
            //        return;
            //    }
            //    this.IReasonableMedicine.ShowFloatWin(false);
            //    if (e.Column == GetColumnIndexFromName("医嘱名称"))
            //    {
            //        #region 药品查询
            //        try
            //        {
            //            //貌似他们只和右下角的坐标位置相关
            //            this.IReasonableMedicine.PassQueryDrug(info.Item.ID, info.Item.Name, info.DoseUnit,
            //                info.Usage.Name, System.Windows.Forms.Control.MousePosition.X,
            //                System.Windows.Forms.Control.MousePosition.Y - 60, System.Windows.Forms.Control.MousePosition.X + 100,
            //                System.Windows.Forms.Control.MousePosition.Y + 15);
            //        }
            //        catch (Exception ex)
            //        {
            //            ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            //        }
            //        #endregion
            //    }
            //    if (e.Column == GetColumnIndexFromName("医嘱名称"))
            //    {
            //        if (this.neuSpread1.Sheets[iSheetIndex].Cells[e.Row, e.Column].Tag != null && this.neuSpread1.Sheets[iSheetIndex].Cells[e.Row, e.Column].Tag.ToString() != "0")
            //        {
            //            this.IReasonableMedicine.PassGetWarnInfo(info.ApplyNo, "0");
            //        }
            //    }
            //}
            //else
            //{
            //    this.IReasonableMedicine.ShowFloatWin(false);
            //}
        }

        /// <summary>
        /// 向合理用药系统传送当前医嘱进行审查
        /// </summary>
        /// <param name="warnPicFlag">是否显示图片警世信息</param>
        ///<param name="checkType">审查方式 1 自动审查 12 用药研究  3 手工审查</param>
        public void PassTransOrder(int checkType, bool warnPicFlag)
        {
            List<Neusoft.HISFC.Models.Order.OutPatient.Order> alOrder = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
            Neusoft.HISFC.Models.Order.OutPatient.Order order;
            DateTime sysTime = this.OrderManagement.GetDateTimeFromSysDateTime();
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
                if (order.Item.ItemType.ToString() != Neusoft.HISFC.Models.Base.EnumItemType.Drug.ToString())
                {
                    continue;
                }
                if (this.frequencyHelper != null)
                {
                    order.Frequency = (Neusoft.HISFC.Models.Order.Frequency)frequencyHelper.GetObjectFromID(order.Frequency.ID).Clone();
                }
                order.ApplyNo = this.OrderManagement.GetSequence("Order.Pass.Sequence");
                alOrder.Add(order);
            }
            if (alOrder.Count > 0)
            {
                this.PassSaveCheck(alOrder, checkType, warnPicFlag);
            }
        }

        /// <summary>
        /// 退出合理用药
        /// </summary>
        public void QuitPass()
        {
            if (IReasonableMedicine != null && IReasonableMedicine.PassEnabled)
            {
                //IReasonableMedicine.ShowFloatWin(false);
                //IReasonableMedicine.PassQuitIn();
            }
        }

        /// <summary>
        /// 合理用药医嘱审查
        /// </summary>
        /// <param name="alOrder">待审查医嘱列表</param>
        ///<param name="warnPicFlag">是否显示图片警世信息</param>
        public void PassSaveCheck(List<Neusoft.HISFC.Models.Order.OutPatient.Order> alOrder, int checkType, bool warnPicFlag)
        {
            if (IReasonableMedicine != null && !this.IReasonableMedicine.PassEnabled)
            {
                return;
            }
            //if (this.IReasonableMedicine.PassSaveCheck(this.currentPatientInfo, alOrder, checkType) == -1)
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("对已保存医嘱进行合理用药审查出错!");
            //}
            //if (!warnPicFlag)//不需显示 直接返回
            //{
            //    return;
            //}

            //Neusoft.HISFC.Models.Order.OutPatient.Order tempOrder;
            //string orderId = "";
            //int iWarn = -1;

            //for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            //{
            //    //string orderId = alOrder[i].ApplyNo;
            //    tempOrder = this.GetObjectFromFarPoint(i, 0);
            //    //orderId = this.GetPhaUserCode(tempOrder.Item.ID);
            //    orderId = tempOrder.Item.ID;

            //    if (tempOrder == null)
            //    {
            //        continue;
            //    }

            //    if (tempOrder.Status == 3 || tempOrder.Item.SysClass.ID.ToString().Substring(0, 1) != "P")
            //    {
            //        continue;
            //    }

            //    iWarn = this.IReasonableMedicine.PassGetWarnFlag(orderId);
            //    this.AddWarnPicturn(i, 0, iWarn);
            //}
        }

        /// <summary>
        /// 添加合理用药结果警世标志
        /// </summary>
        /// <param name="iRow">欲更改行索引</param>
        /// <param name="iSheet">欲更改Sheet索引</param>
        /// <param name="warnFlag">警世标志</param>
        public void AddWarnPicturn(int iRow, int iSheet, int warnFlag)
        {
            string picturePath = Application.StartupPath + "\\pic";
            switch (warnFlag)
            {
                case 0:										//0 (蓝色)无问题
                    picturePath = picturePath + "\\0.gif";
                    break;
                case 1:										//1 (黄色)危害较低或尚不明确
                    picturePath = picturePath + "\\1.gif";
                    break;
                case 2:										//2 (红色)不推荐或较严重危害
                    picturePath = picturePath + "\\2.gif";
                    break;
                case 3:										// 3 (黑色)绝对禁忌、错误或致死性危害
                    picturePath = picturePath + "\\3.gif";
                    break;
                case 4:										//4 (澄色)慎用或有一定危害 
                    picturePath = picturePath + "\\4.gif";
                    break;
                default:
                    break;
            }
            if (!System.IO.File.Exists(picturePath))
            {
                return;
            }
            try
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                FarPoint.Win.Picture pic = new FarPoint.Win.Picture();
                pic.Image = System.Drawing.Image.FromFile(picturePath, true);
                pic.TransparencyColor = System.Drawing.Color.Empty;
                t.BackgroundImage = pic;
                this.neuSpread1.Sheets[iSheet].Cells[iRow, 0].CellType = t;			//医嘱名称
                this.neuSpread1.Sheets[iSheet].Cells[iRow, 0].Tag = "1";							//已做过审查
                this.neuSpread1.Sheets[iSheet].Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("设置合理用药审查结果显示过程中出错!" + ex.Message);
            }
        }

        /// <summary>
        /// 向合理用药系统传送当前欲查询药品信息
        /// </summary>
        /// <param name="checkType">查询方式</param>
        public void PassTransDrug(int checkType)
        {
            if (IReasonableMedicine != null && !this.IReasonableMedicine.PassEnabled)
            {
                return;
            }
            //int iSheetIndex = 0;
            //int iRow = this.neuSpread1.Sheets[iSheetIndex].ActiveRowIndex;
            //Neusoft.HISFC.Models.Order.OutPatient.Order info = this.GetObjectFromFarPoint(iRow, iSheetIndex);
            //if (info == null)
            //{
            //    return;
            //}
            //if (info.Item.ItemType.ToString() != Neusoft.HISFC.Models.Base.EnumItemType.Drug.ToString())
            //{
            //    this.IReasonableMedicine.ShowFloatWin(false);
            //    return;
            //}
            //this.IReasonableMedicine.ShowFloatWin(false);
            //this.IReasonableMedicine.PassSetDrug(info.Item.ID, info.Item.Name, ((Neusoft.HISFC.Models.Pharmacy.Item)info.Item).DoseUnit,
            //    info.Usage.Name);
            //this.IReasonableMedicine.DoCommand(checkType);
        }

        /// <summary>
        /// 合理药品系统药品查询
        /// </summary>
        private void mnuPass_Click(object sender, EventArgs e)
        {
            if (IReasonableMedicine != null && !this.IReasonableMedicine.PassEnabled)
                return;
            ToolStripItem muItem = sender as ToolStripItem;
            //switch (muItem.Text)
            //{

            //    #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} 添加合理用药右键菜单

            //    #region 一级菜单

            //    case "过敏史/病生状态":
            //        int iReg;
            //        this.IReasonableMedicine.PassSetPatientInfo(this.currentPatientInfo, this.OrderManagement.Operator.ID, this.OrderManagement.Operator.Name);
            //        this.IReasonableMedicine.ShowFloatWin(false);
            //        iReg = this.IReasonableMedicine.DoCommand(22);
            //        if (iReg == 2)
            //        {
            //            this.PassTransOrder(1, true);
            //        }
            //        break;

            //    case "药物临床信息参考":
            //        this.PassTransDrug(101);
            //        break;
            //    case "药品说明书":
            //        this.PassTransDrug(102);
            //        break;
            //    case "中国药典":
            //        this.PassTransDrug(107);
            //        break;
            //    case "病人用药教育":
            //        this.PassTransDrug(103);
            //        break;
            //    case "药物检验值":
            //        this.PassTransDrug(104);
            //        break;
            //    case "临床检验信息参考":
            //        this.PassTransDrug(220);
            //        break;

            //    case "医药信息中心":
            //        this.PassTransDrug(106);
            //        break;

            //    case "药品配对信息":
            //        this.PassTransDrug(13);
            //        break;
            //    case "给药途径配对信息":
            //        this.PassTransDrug(14);
            //        break;
            //    case "医院药品信息":
            //        this.PassTransDrug(105);
            //        break;

            //    case "系统设置":
            //        this.PassTransDrug(11);
            //        break;

            //    case "用药研究":
            //        this.IReasonableMedicine.ShowFloatWin(false);
            //        this.PassTransOrder(12, false);
            //        break;

            //    case "警告":
            //        this.PassTransDrug(6);
            //        break;

            //    case "审查":
            //        this.IReasonableMedicine.ShowFloatWin(false);
            //        this.PassTransOrder(3, true);
            //        break;

            //    #endregion

            //    #region 二级菜单

            //    case "药物-药物相互作用":
            //        this.PassTransDrug(201);
            //        break;
            //    case "药物-食物相互作用":
            //        this.PassTransDrug(202);

            //        break;
            //    case "国内注射剂体外配伍":
            //        this.PassTransDrug(203);
            //        break;
            //    case "国外注射剂体外配伍":
            //        this.PassTransDrug(204);
            //        break;

            //    case "禁忌症":
            //        this.PassTransDrug(205);
            //        break;
            //    case "副作用":
            //        this.PassTransDrug(206);
            //        break;

            //    case "老年人用药":
            //        this.PassTransDrug(207);
            //        break;
            //    case "儿童用药":
            //        this.PassTransDrug(208);
            //        break;
            //    case "妊娠期用药":
            //        this.PassTransDrug(209);
            //        break;
            //    case "哺乳期用药":
            //        this.PassTransDrug(210);
            //        break;

            //    #endregion

            //    #endregion
            //    default:
            //        break;
            //}
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
                Neusoft.HISFC.Models.Order.OutPatient.Order order = null;

                if (!isRegFeeOnly)
                {
                    #region 处理附材

                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                    try
                    {
                        for (int i = this.neuSpread1.Sheets[0].Rows.Count - 1; i >= 0; i--)
                        {
                            order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                            if (order == null)
                            {
                                continue;
                            }

                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                            //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                            if (newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            if (newOrder.Status != 0 || newOrder.IsHaveCharged)//检查并发医嘱状态
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                ucOutPatientItemSelect1.MessageBoxShow("计算附材错误！\r\n[" + order.Item.Name + "]可能已经收费,请退出开立界面重新进入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }

                            if (order != null)
                            {
                                alOrder.Add(order);
                            }
                            if (order != null && order.IsSubtbl)
                            {
                                if (order.Memo == "挂号费")
                                {
                                    if (!this.isAddRegSubBeforeAddOrder)
                                    {
                                        this.neuSpread1.Sheets[0].Rows.Remove(i, 1);
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
                                if (IDealSubjob.DealSubjob(this.Patient, alOrder, ref alSubOrders, ref errText) <= 0)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                    ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败：" + errText, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return -1;
                                }

                                if (alSubOrders != null && alSubOrders.Count > 0)
                                {
                                    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alSubOrders)
                                    {
                                        orderObj.Combo.ID = this.OrderManagement.GetNewOrderComboID();
                                        orderObj.SubCombNO = this.GetMaxSubCombNo(orderObj);
                                        orderObj.SortID = 0;

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
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        ucOutPatientItemSelect1.MessageBoxShow("处理辅材失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }

                    Neusoft.FrameWork.Management.PublicTrans.Commit();

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
                            order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                            if (order == null)
                            {
                                continue;
                            }

                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                            //如果没有取到，可能是已经生成了流水号却出错的情况或者是数据库出错的情况
                            if (newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            if (newOrder.Status != 0 || newOrder.IsHaveCharged)//检查并发医嘱状态
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                ucOutPatientItemSelect1.MessageBoxShow("计算附材错误！\r\n[" + order.Item.Name + "]可能已经收费,请退出开立界面重新进入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = null;
                            Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp = null;
                            if (alOrder.Count > 0)
                            {
                                orderTemp = alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                            }

                            if (orderTemp == null)
                            {
                                orderTemp = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                                orderTemp.HerbalQty = 1;
                                orderTemp.Combo = new Neusoft.HISFC.Models.Order.Combo();
                            }

                            Neusoft.HISFC.Models.Fee.Item.Undrug item = null;

                            Neusoft.HISFC.BizProcess.Integrate.Order orderIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Order();
                            ArrayList alSupplyOrder = new ArrayList();

                            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemObj in alSupplyFee)
                            {
                                //定义个新医嘱对象
                                newOrder = new Neusoft.HISFC.Models.Order.OutPatient.Order();//重新设置医嘱

                                item = feeManagement.GetItem(itemObj.Item.ID);//获得最新项目信息
                                if (item == null)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("计算附材时，查找项目失败：" + feeManagement.Err);
                                    return -1;
                                }

                                if (item.UnitFlag == "1")
                                {
                                    item.Price = feeManagement.GetUndrugCombPrice(itemObj.Item.ID);
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

                                    //newOrder.ID = orderIntegrate.GetNewOrderID();//医嘱流水号
                                    //if (newOrder.ID == "")
                                    //{
                                    //    ucOutPatientItemSelect1.MessageBoxShow("计算项目附材时，对新增加的附材获得医嘱流水号出错！" + orderIntegrate.Err);
                                    //    return -1;
                                    //}

                                    newOrder.Item.ItemType = Neusoft.HISFC.Models.Base.EnumItemType.UnDrug;

                                    newOrder.DoseUnit = "";

                                    newOrder.IsEmergency = orderTemp.IsEmergency;
                                    newOrder.IsSubtbl = true;
                                    newOrder.Usage = new Neusoft.FrameWork.Models.NeuObject();
                                    newOrder.SequenceNO = -1;
                                    if (newOrder.ExeDept.ID == "")//执行科室默认
                                    {
                                        newOrder.ExeDept = this.GetReciptDept();
                                    }

                                    //newOrder.HerbalQty = orderTemp.HerbalQty;
                                    //newOrder.Frequency = orderTemp.Frequency;
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
                                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alSupplyOrder)
                                {
                                    orderObj.Combo.ID = this.OrderManagement.GetNewOrderComboID();
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
                PactInfo pactTemp = this.currentPatientInfo.Pact.Clone();

                string pactCode = this.cmbPact.Tag.ToString();
                if (string.IsNullOrEmpty(pactCode))
                {
                    return;
                }

                Neusoft.HISFC.Models.Registration.Register patientInfo = new Neusoft.HISFC.Models.Registration.Register();
                patientInfo.ID = currentPatientInfo.ID;
                patientInfo.PID = this.currentPatientInfo.PID;
                patientInfo.Name = currentPatientInfo.Name;
                patientInfo.Sex = currentPatientInfo.Sex;
                patientInfo.Birthday = currentPatientInfo.Birthday;
                patientInfo.IDCard = currentPatientInfo.IDCard;
                patientInfo.Pact = pactHelper.GetObjectFromID(pactCode) as Neusoft.HISFC.Models.Base.PactInfo;
                this.currentPatientInfo.Pact = patientInfo.Pact.Clone();


                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                #region 接口判断合同单位限制

                if (this.iCheckPactInfo == null)
                {
                    this.iCheckPactInfo = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Common.ICheckPactInfo)) as Neusoft.HISFC.BizProcess.Interface.Common.ICheckPactInfo;
                }
                if (this.iCheckPactInfo == null)
                {
                    //if (!string.IsNullOrEmpty(Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err))
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow("获得接口ICheckPactInfo错误,导致无法判断合同单位的有效性！\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                else
                {
                    iCheckPactInfo.PatientInfo = patientInfo;
                    if (iCheckPactInfo.CheckIsValid() == -1)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(iCheckPactInfo.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                #endregion

                if (this.regManagement.UpdateRegInfoByClinicCode(patientInfo) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    this.currentPatientInfo.Pact = pactTemp.Clone();
                    this.cmbPact.Text = pactTemp.Name;
                    ucOutPatientItemSelect1.MessageBoxShow("更新合同单位信息错误：" + regManagement.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();

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

        /// <summary>
        /// 设置项目输入框是否可见
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetInputItemVisible(bool isVisible)
        {
            this.ucOutPatientItemSelect1.SetInputControlVisible(isVisible);
        }
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
                int a = Neusoft.FrameWork.Function.NConvert.ToInt32(x);
                int b = Neusoft.FrameWork.Function.NConvert.ToInt32(y);
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