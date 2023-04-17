using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.HISFC.BizProcess.Interface.Order;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 医嘱管理]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucOrder : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOrder()
        {
            InitializeComponent();
            this.contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
        }

        #region 变量
        public delegate void EventButtonHandler(bool b);
        //public event EventButtonHandler OrderCanComboChanged;//医嘱是否可以组合事件

        /// <summary>
        /// 医嘱是否可以取消组合事件
        /// </summary>
        public event EventButtonHandler OrderCanCancelComboChanged;

        /// <summary>
        /// 医嘱是否可以点击手术申请/化疗
        /// </summary>
        public event EventButtonHandler OrderCanOperatorChanged;
        //public event EventButtonHandler OrderCanSaveChanged;	//医嘱是否保存

        /// <summary>
        /// 是否可打印检查单事件
        /// </summary>
        public event EventButtonHandler OrderCanSetCheckChanged;

        private bool needUpdateDTBegin = true;
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = null;

        /// <summary>
        /// 当前开立患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get { return this.myPatientInfo; }
            set { this.myPatientInfo = value; }
        }

        /// <summary>
        /// 开立之前的长期医嘱行数
        /// </summary>
        public int CountLongBegin;

        /// <summary>
        /// 开立之前的临时医嘱行数
        /// </summary>
        public int CountShortBegin;

        /// <summary>
        /// 是否允许合理用药审查
        /// </summary>
        private bool enabledPass = true;

        /// <summary>
        /// 是否启用合理用药
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
        /// 是否进行组套编辑功能
        /// </summary>
        protected bool EditGroup = false;

        /// <summary>
        /// 执行科室信息(pivas常数)
        /// </summary>
        private ArrayList deptItemList = null;
        /// <summary>
        /// 用法(pivas常数)
        /// </summary>
        private ArrayList drugUsageList = null;
        /// <summary>
        /// 长期医嘱是否开通pivas功能标识:1是|0否
        /// </summary>
        private string pivasCzFlag = "";
        /// <summary>
        /// 临时医嘱是否开通pivas功能标识:1是|0否
        /// </summary>
        private string pivasLzFlag = "";

        private DataSet dataSet = null; //当前DataSet
        private DataView dvLong = null;//当前DataView
        private DataView dvShort = null;//当前DataView

        /// <summary>
        /// 最大Sort
        /// </summary>
        private int MaxSort = 0;

        /// <summary>
        /// 存放当前界面的方号
        /// </summary>
        [Obsolete("作废，修改获取方号方式", true)]
        private Hashtable HsSubCombNo
        {
            get
            {
                if (this.fpOrder.ActiveSheetIndex == 0)
                {
                    return this.hsLongSubCombNo;
                }
                else
                {
                    return this.hsShortSubCombNo;
                }
            }
        }

        /// <summary>
        /// 存放所有长嘱方号
        /// </summary>
        [Obsolete("作废，修改获取方号方式", true)]
        private Hashtable hsLongSubCombNo = new Hashtable();

        /// <summary>
        /// 存放所有临嘱方号
        /// </summary>
        [Obsolete("作废，修改获取方号方式", true)]
        private Hashtable hsShortSubCombNo = new Hashtable();

        /// <summary>
        /// 当前开立患者
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;
        protected FS.HISFC.BizLogic.Order.AdditionalItem AdditionalItemManagement = new FS.HISFC.BizLogic.Order.AdditionalItem();

        protected FS.HISFC.BizLogic.Order.PacsBill pacsBillManagement = new FS.HISFC.BizLogic.Order.PacsBill();

        /// <summary>
        /// 是否新加，修改时间
        /// </summary>
        protected bool dirty = false;
        protected DataSet dsAllLong;
        protected DataSet dsAllShort;
        protected FS.HISFC.Models.Order.Inpatient.Order currentOrder = null;

        /// <summary>
        /// 开立界面显示有效临嘱的天数
        /// </summary>
        private int shortOrderShowDays = 1000;

        /// <summary>
        /// 是否默认长期医嘱// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        private bool isDefaultLONG = true;

        public string LONGSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "longordersetting.xml";
        public string SHORTSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "shortordersetting.xml";

        /// <summary>
        /// 所有医嘱显示的天数
        /// </summary>
        private int allOrderShowDays = 100;

        /// <summary>
        /// 是否开启限制用药提醒//{EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
        /// </summary>
        private bool isOpenDrugWarn = false;

        private FS.FrameWork.Public.ObjectHelper helper; //当前Helper

        /// <summary>
        /// 刷新医嘱类型 0 刷新长嘱 1 刷新临嘱 2 长、临嘱全部刷新
        /// </summary>
        private string refreshComboFlag = "2";

        private Order myOrderClass = new Order();
        ToolTip tooltip = new ToolTip();
        private FS.HISFC.BizProcess.Interface.Common.ICheckPrint checkPrint = null;

        /// <summary>
        /// 医技预约管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalTechnology.Appointment appMgr = new FS.HISFC.BizLogic.MedicalTechnology.Appointment();

        /// <summary>
        /// 医嘱扩展信息管理
        /// </summary>
        FS.HISFC.BizLogic.Order.OrderExtend orderExtMgr = new FS.HISFC.BizLogic.Order.OrderExtend();

        private string checkslipno;

        public string Checkslipno
        {
            get
            {
                return checkslipno;
            }
            set
            {
                checkslipno = value;
            }
        }
        /// <summary>
        /// 医疗权限验证
        /// </summary>
        //public bool isCheckPopedom = false;

        /// <summary>
        /// 是否有处方权
        /// </summary>
        //public bool isHaveOrderPower = false;

        /// <summary>
        /// 医嘱信息变更接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IAlterOrder IAlterOrderInstance = null;

        /// <summary>
        /// 精麻方打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST IRecipePrintST = null;

        //{6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504
        //protected FS.ApplyInterface.HisInterface PACSApplyInterface = null;

        /// <summary>
        /// LIS接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.ILis lisInterface = null;

        /// <summary>
        /// 医生站辅材处理接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IDealSubjob IDealSubjob = null;

        /// <summary>
        /// {F38618E9-7421-423d-80A9-401AFED0B855} xuc
        /// 完成刷新显示患者医嘱信息标志
        /// </summary>
        private bool isShowOrderFinished = true;


        /// <summary>
        /// 组套是否可以临嘱复制为长嘱，长嘱复制为临嘱// {45652500-8594-40ac-A92E-FFFEB812655C}
        /// </summary>
        private bool isModifyOrderType = true;

        /// <summary>
        /// 当前登录信息
        /// </summary>
        Employee empl = FrameWork.Management.Connection.Operator as Employee;

        /// <summary>
        /// 合理用药接口
        /// </summary>
        IReasonableMedicine IReasonableMedicine = null;

        /// <summary>
        /// 小时计费的医嘱频次代码 {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
        /// </summary>
        private string hoursFrequencyID = string.Empty;

        /// <summary>
        /// 是否启用电子申请单 
        /// </summary>
        private bool isUsePACSApplySheet = false;

        /// <summary>
        /// 保存组套时刷新组套树
        /// </summary>
        public event EventHandler OnRefreshGroupTree;

        private Hashtable htSubs = new Hashtable();

        public event RefreshGroupTree refreshGroup;

        /// <summary>
        /// 增加项目前操作接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem IBeforeAddItem = null;

        /// <summary>
        /// 保存后处方处理接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.ISaveOrder IAfterSaveOrder = null;

        /// <summary>
        /// 保存处方前调用
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder IBeforeSaveOrder = null;

        /// <summary>
        /// 开立动作前接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddOrder IBeforeAddOrder = null;

        /// <summary>
        /// 临床路径
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.IClinicPath iClinicPath = null;

        /// <summary>
        /// 开立患者类别
        /// </summary>
        private ReciptPatientType patientType = ReciptPatientType.DeptPatient;

        /// <summary>
        /// 开立患者类别
        /// </summary>
        public ReciptPatientType PatientType
        {
            get
            {
                return patientType;
            }
            set
            {
                patientType = value;
            }
        }

        /// <summary>
        /// 医保限制性用药药品列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper indicationsHelper = null;

        /// <summary>
        /// 医保限制性用药用药
        /// </summary>
        private ArrayList alIndicationsDrug = null;

        Forms.frmDCOrderAndZG frmDCOrderAndZG1 = null;
        Forms.frmDCTreatmentType frmDCTreatmentType = null;

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();


        /// <summary>
        /// 参数控制类
        /// </summary>
        private static FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();



        /// <summary>
        /// 用法列表
        /// </summary>
        ArrayList usageList = null;


        #endregion

        #region 接口


        /// <summary>
        /// 医嘱打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint IInPatientOrderPrint = null;



        /// <summary>
        /// 检查打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint IInPatientPacsOrderPrint = null;



        /// <summary>
        /// 申请单接口
        /// </summary>
        private static FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInpateintPacsApply IInpateintPacsApply = null;

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
            if (FS.FrameWork.Management.Connection.Operator.ID == "")
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载窗口，请您稍后!");
            this.myReciptDoc = null;
            this.myReciptDept = null;
            try
            {
                this.ucItemSelect1.IsNurseCreate = this.isNurseCreate;
                this.ucItemSelect1.Init();
                this.GetColmSet();

                InitControl();

                this.InitPacsApply();

                InitAlterOrderInstance();
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }

            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("窗口即将加载完毕，亲，感谢您的耐心等待O(∩_∩)O");

                #region 获取控制参数  立界面显示已停止医嘱时间间隔(单位为天) 默认显示1000天以内的
                try
                {
                    this.shortOrderShowDays = CacheManager.ContrlManager.GetControlParam<int>("HNZY04", false, 1000);
                    this.allOrderShowDays = CacheManager.ContrlManager.GetControlParam<int>("HNZY03", false, 1000);
                }
                catch
                {
                    this.shortOrderShowDays = 1000;
                    this.allOrderShowDays = 100;
                }

                // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                this.isOpenDrugWarn = CacheManager.ContrlManager.GetControlParam<bool>("ZYYS01");

                isDefaultLONG = CacheManager.ContrlManager.GetControlParam<bool>("ZYMR01", true, true);// {4D67D981-6763-4ced-814E-430B518304E2}
                // {45652500-8594-40ac-A92E-FFFEB812655C}
                isModifyOrderType = CacheManager.ContrlManager.GetControlParam<bool>("ZYYZ01", true, true);

                this.lblDisplay.Text = "默认显示" + this.allOrderShowDays.ToString() + "天内全部医嘱，全部有效长嘱，" + this.shortOrderShowDays.ToString() + "天内有效临嘱";

                #endregion
                this.hoursFrequencyID = CacheManager.ContrlManager.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.MetConstant.Hours_Frequency_ID, false, "NONE");

                #region 电子申请单 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} 接入电子申请单 yangw 20100504
                this.isUsePACSApplySheet = CacheManager.ContrlManager.GetControlParam<bool>("PACSZY", false, false);
                #endregion

                ArrayList alIndications = CacheManager.GetConList("IndicationsDrug");
                indicationsHelper = new FS.FrameWork.Public.ObjectHelper(alIndications);

                this.ucItemSelect1.OrderChanged += new ItemSelectedDelegate(ucItemSelect1_OrderChanged);
                this.ucItemSelect1.CatagoryChanged += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);
                this.ucItemSelect1.GetMaxSubCombNo += new GetMaxSubCombNoEvent(GetMaxCombNo);
                this.ucItemSelect1.GetSameSubCombNoOrder += new GetSameSubCombNoOrderEvent(ucItemSelect1_GetSameSubCombNoOrder);
                this.ucItemSelect1.DeleteSubComnNo += new DeleteSubCombNoEvent(ucItemSelect1_DeleteSubComnNo);

                this.fpOrder.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating;
                this.fpOrder.Sheets[0].DataAutoSizeColumns = false;
                this.fpOrder.Sheets[1].DataAutoSizeColumns = false;
                this.fpOrder.Sheets[0].DataAutoCellTypes = false;
                this.fpOrder.Sheets[1].DataAutoCellTypes = false;

                this.fpOrder.Sheets[0].GrayAreaBackColor = Color.White;
                this.fpOrder.Sheets[1].GrayAreaBackColor = Color.White;

                this.fpOrder.Sheets[0].RowHeader.Columns.Get(0).Width = 15;
                this.fpOrder.Sheets[1].RowHeader.Columns.Get(0).Width = 15;

                this.fpOrder.Sheets[0].RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
                this.fpOrder.Sheets[1].RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;

                //this.OrderType = FS.HISFC.Models.Order.EnumType.LONG;
                //this.fpOrder.ActiveSheetIndex = 0;
                if (isDefaultLONG)// {4D67D981-6763-4ced-814E-430B518304E2}
                {
                    this.OrderType = FS.HISFC.Models.Order.EnumType.LONG;
                    this.fpOrder.ActiveSheetIndex = 0;
                }
                else
                {
                    this.OrderType = FS.HISFC.Models.Order.EnumType.SHORT;
                    this.fpOrder.ActiveSheetIndex = 1;
                }

                this.fpOrder.Sheets[0].RowHeader.DefaultStyle.Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised);
                this.fpOrder.Sheets[0].RowHeader.DefaultStyle.CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                this.fpOrder.Sheets[1].RowHeader.DefaultStyle.Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised);
                this.fpOrder.Sheets[1].RowHeader.DefaultStyle.CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                #region 接口

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

                IDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;

                #endregion

                this.cbxPatientInfo.CheckStateChanged += new EventHandler(cbxPatientInfo_CheckStateChanged);

                usageList = constantMgr.GetList("USAGELIST");
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }

            //{FA143951-748B-4c45-9D1B-853A31B9E006}
            FS.HISFC.Models.Base.Employee curremployee = CacheManager.PersonMgr.GetEmployeeByCode(CacheManager.InOrderMgr.Operator.ID);

            FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            string hospitalname = "";
            string hospitalybcode = "";
            if (currDept.HospitalName.Contains("顺德"))
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


            base.OnStatusBarInfo(null, "(绿色：新开)(蓝色：审核)(黄色：执行)(红色：作废)(紫色：预停止)    机构名称：" + hospitalname + "  国家医保编码：" + hospitalybcode + "  医保医师代码：" + gjcode + "");


            #region 合理用药
            InitPass();
            #endregion

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("感谢亲！窗口加载完毕:-)");
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        void cbxPatientInfo_CheckStateChanged(object sender, EventArgs e)
        {
            if (!cbxPatientInfo.Checked)
            {
                this.pnPatient.Height = 23;
            }
            else
            {
                if (myPatientInfo.Pact.PayKind.ID == "03")
                {
                    pnPatient.Height = 72;
                }
                else
                {
                    this.pnPatient.Height = 58;
                }
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.myOrderClass.fpSpread1 = this.fpOrder;

            #region 初始化ucItemSelect
            this.ucItemSelect1.LongOrShort = 0;//设置为长期医嘱
            this.ucItemSelect1.OperatorType = Operator.Add;//添加模式
            #endregion

            #region 初始化长、临嘱DataSet

            dsAllLong = this.InitDataSet();
            dsAllShort = this.InitDataSet();
            this.myOrderClass.dsAllLong = dsAllLong;

            this.fpOrder.Sheets[0].DataSource = dsAllLong.Tables[0];
            this.fpOrder.Sheets[1].DataSource = dsAllShort.Tables[0];
            #endregion

            //this.myOrderClass.ColumnSet();
            SetFP();
            InitFP();

            #region FarPoint 事件

            //this.fpOrder.Sheets[0].Columns[-1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            //this.fpOrder.Sheets[1].Columns[-1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.fpOrder.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpOrder_SelectionChanged);

            this.fpOrder.ActiveSheetChanged += new EventHandler(fpOrder_ActiveSheetChanged);
            this.fpOrder.SheetTabClick += new FarPoint.Win.Spread.SheetTabClickEventHandler(fpOrder_SheetTabClick);

            this.fpOrder.CellClick += new CellClickEventHandler(fpOrder_CellClick);
            this.fpOrder.CellDoubleClick += new CellClickEventHandler(fpOrder_CellDoubleClick);

            this.fpOrder.MouseDown += new MouseEventHandler(fpOrder_MouseDown);
            this.fpOrder.MouseUp += new MouseEventHandler(fpOrder_MouseUp);
            this.fpOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpOrder_ColumnWidthChanged);

            #endregion

            this.pnPatient.Visible = false;
        }

        private void InitFP()
        {
            //this.myOrderClass.SetColumnName(0);
            //this.myOrderClass.SetColumnName(1);
            this.SetColumnNameNew(0);
            this.SetColumnNameNew(1);

            #region "列大小"

            //this.myOrderClass.SetColumnProperty();

            if (System.IO.File.Exists(LONGSETTINGFILENAME))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpOrder.Sheets[0], LONGSETTINGFILENAME);
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpOrder.Sheets[1], SHORTSETTINGFILENAME);
            }
            else
            {
                for (int index = 0; index < fpOrder.Sheets.Count; index++)
                {
                    this.fpOrder.Sheets[index].ZoomFactor = 1.2F;

                    fpOrder.Sheets[index].Columns[dicColmSet["!"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["期效"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["医嘱类型"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["医嘱类型"]].Width = 63;

                    fpOrder.Sheets[index].Columns[dicColmSet["医嘱流水号"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["医嘱状态"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["组合号"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["主药"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["组号"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["组号"]].Width = 20;
                    fpOrder.Sheets[index].Columns[dicColmSet["开立时间"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["开立时间"]].Width = 112;
                    fpOrder.Sheets[index].Columns[dicColmSet["开立医生"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["开立医生"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["顺序号"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["医嘱名称"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["医嘱名称"]].Width = 234;
                    fpOrder.Sheets[index].Columns[dicColmSet["组"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["组"]].Width = 24;
                    if (index == 0)
                    {
                        fpOrder.Sheets[index].Columns[dicColmSet["首日量"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["首日量"]].Width = 40;
                        fpOrder.Sheets[index].Columns[dicColmSet["付数"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["总量"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["总量单位"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["检查部位"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["样本类型"]].Visible = false;
                    }
                    else
                    {
                        fpOrder.Sheets[index].Columns[dicColmSet["首日量"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["付数"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["付数"]].Width = 25;
                        fpOrder.Sheets[index].Columns[dicColmSet["总量"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["总量"]].Width = 27;
                        fpOrder.Sheets[index].Columns[dicColmSet["总量单位"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["总量单位"]].Width = 40;
                        fpOrder.Sheets[index].Columns[dicColmSet["检查部位"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["检查部位"]].Width = 63;
                        fpOrder.Sheets[index].Columns[dicColmSet["样本类型"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["样本类型"]].Width = 63;
                    }
                    fpOrder.Sheets[index].Columns[dicColmSet["每次用量"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["每次用量"]].Width = 44;

                    fpOrder.Sheets[index].Columns[dicColmSet["单位"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["单位"]].Width = 30;
                    fpOrder.Sheets[index].Columns[dicColmSet["频次"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["频次"]].Width = 30;
                    fpOrder.Sheets[index].Columns[dicColmSet["频次名称"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["用法编码"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["用法"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["用法"]].Width = 54;
                    fpOrder.Sheets[index].Columns[dicColmSet["总量"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["总量"]].Width = 39;
                    fpOrder.Sheets[index].Columns[dicColmSet["总量单位"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["总量单位"]].Width = 37;
                    fpOrder.Sheets[index].Columns[dicColmSet["系统类别"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["系统类别"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["开始时间"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["开始时间"]].Width = 111;
                    fpOrder.Sheets[index].Columns[dicColmSet["结束时间"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["结束时间"]].Width = 111;
                    fpOrder.Sheets[index].Columns[dicColmSet["停止时间"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["停止时间"]].Width = 111;
                    fpOrder.Sheets[index].Columns[dicColmSet["执行科室编码"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["执行科室"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["执行科室"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["急"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["急"]].Width = 18;
                    fpOrder.Sheets[index].Columns[dicColmSet["取药药房编码"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["取药药房"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["取药药房"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["备注"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["备注"]].Width = 98;
                    fpOrder.Sheets[index].Columns[dicColmSet["录入人编码"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["录入人"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["开立科室"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["开立科室"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["停止人编码"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["停止人"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["停止人"]].Width = 50;
                }

                SaveFpStyle();
            }

            #endregion
        }

        /// <summary>
        /// 初始化DataSet
        /// </summary>
        /// <returns></returns>
        private DataSet InitDataSet()
        {
            dataSet = new DataSet();
            myOrderClass.SetDataSet(ref dataSet);
            return dataSet;
        }



        /// <summary>
        /// 医生站打印接口
        /// </summary>
        private void InitOrderPrint()
        {
            if (IInPatientOrderPrint == null)
            {
                IInPatientOrderPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint)) as FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint;
            }
            if (this.IRecipePrintST == null)
            {
                IRecipePrintST = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST)) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST;
            }
        }

        /// <summary>
        /// 申请单接口
        /// </summary>
        private void InitPacsApply()
        {
            if (IInpateintPacsApply == null)
            {
                IInpateintPacsApply = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInpateintPacsApply)) as FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInpateintPacsApply;
            }
        }

        #endregion

        #region IToolBar 成员

        /// <summary>
        /// 退出医嘱更改
        /// </summary>
        /// <returns></returns>
        public int ExitOrder()
        {
            if (!CheckNewOrder())
            {
                return -1;
            }
            this.IsDesignMode = false;

            SaveUserDefaultSetting(false);

            return 0;
        }

        /// <summary>
        /// 删除医嘱
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return Delete(this.fpOrder.ActiveSheet.ActiveRowIndex, false);
        }

        /// <summary>
        /// {D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        /// <param name="rowIndex">删除的行数</param>
        /// <param name="isDirectDel">是否直接删除（不提示）</param>
        /// <returns></returns>
        private int Delete(int rowIndex, bool isDirectDel)
        {
            int i = rowIndex;

            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("请先选择一条医嘱！");
                return 0;
            }

            DialogResult r;

            FS.HISFC.Models.Order.Inpatient.Order order = null, temp = null;
            Hashtable hsDeleteOrders = new Hashtable();

            //存放所有需要删除的医嘱组合号
            string orderComboIDs = "";

            for (int row = 0; row < this.fpOrder.ActiveSheet.Rows.Count; row++)
            {
                if (this.fpOrder.ActiveSheet.IsSelected(row, 0))
                {
                    order = this.GetObjectFromFarPoint(row, this.fpOrder.ActiveSheetIndex);
                    if (order == null)
                    {
                        MessageBox.Show("获得医嘱实体出错！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }

                    if (hsDeleteOrders.Contains(order.Combo.ID))
                    {
                        continue;
                    }
                    else
                    {
                        hsDeleteOrders.Add(order.Combo.ID, order);
                        orderComboIDs += order.Combo.ID + "|";
                    }
                }
            }


            if (this.isNurseCreate)
            {
                if (order.ReciptDoctor.ID != CacheManager.InOrderMgr.Operator.ID)
                {
                    MessageBox.Show("护士不允许删除他人开立的医嘱!");
                    return -1;
                }
            }

            if (order.Status == 0 || order.Status == 5)
            {
                //新加
                #region 未审核医嘱

                if (!CheckOrderCanMove(order))
                {
                    MessageBox.Show("【" + order.Item.Name + "】已经打印，不允许删除！\r\n\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return -1;
                }

                r = DialogResult.OK;
                if (!isDirectDel)
                {
                    r = MessageBox.Show("是否删除该医嘱[" + order.Item.Name + "]?\n *此操作不能撤消！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
                if (r == DialogResult.OK)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    pacsBillManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    CacheManager.RadtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int count = this.fpOrder.ActiveSheet.RowCount;

                    for (int row = count - 1; row >= 0; row--)
                    {
                        temp = this.fpOrder.ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        //if (temp.Combo.ID == order.Combo.ID)
                        if (orderComboIDs.Contains(temp.Combo.ID))
                        {
                            FS.HISFC.Models.Order.Inpatient.Order tmpOrder = CacheManager.InOrderMgr.QueryOneOrder(temp.ID);

                            if (order.Item.SysClass.ID.ToString() == "UC")
                            {
                                #region 无论有没,都去取消一下医技预约
                                appMgr.Cancle(order.ID);
                                #endregion
                            }

                            if (order.ID == "" || tmpOrder == null)
                            {
                                //自然删除
                                this.fpOrder.ActiveSheet.Rows.Remove(row, 1);
                            }
                            else
                            {
                                if (tmpOrder != null && tmpOrder.RowNo >= 0 && tmpOrder.PageNo >= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(tmpOrder.Item.Name + "已经打印医嘱单，不能删除，请点击右键停止/取消医嘱！");
                                    return -1;
                                }
                                //delete from table
                                //删除已经有的附材
                                if (CacheManager.InOrderMgr.DeleteOrderSubtbl(temp.Combo.ID) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除附材项目信息出错！") + CacheManager.InOrderMgr.Err);
                                    return -1;
                                }
                                int parm = CacheManager.InOrderMgr.DeleteOrder(temp);
                                if (parm == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    MessageBox.Show(CacheManager.InOrderMgr.Err);
                                    return -1;
                                }
                                else
                                {
                                    if (parm == 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("医嘱状态已发生变化 请刷新重试"));
                                        return -1;
                                    }
                                }
                                if (CacheManager.RadtIntegrate.SelectBQ_Info(((FS.FrameWork.Models.NeuObject)(myPatientInfo)).ID) == "1")
                                {
                                    if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("已经病重") != -1)
                                    {
                                        if (CacheManager.RadtIntegrate.UpdatePT_Info(((FS.FrameWork.Models.NeuObject)(myPatientInfo)).ID) == -1)
                                        {
                                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                            MessageBox.Show(CacheManager.RadtIntegrate.Err);
                                            return -1;
                                        }
                                    }
                                }
                                else
                                {
                                }
                                //删除附材
                                parm = CacheManager.InOrderMgr.DeleteOrderSubtbl(temp.Combo.ID);
                                if (parm == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    MessageBox.Show(CacheManager.InOrderMgr.Err);
                                    return -1;
                                }

                                //{3EDB0DF8-44D5-4596-B0BB-A12E7C87399C}
                                //启用医保限制性用药
                                #region 处理医保限制性用药
                                //2019-12停用医保限定性用药限制
                                //{A92CA128-BDD8-47d1-B5F7-505A0647C67D}
                                if (myPatientInfo != null
                                    && this.myPatientInfo.Pact.PayKind.ID == "02")
                                {
                                    if (indicationsHelper.GetObjectFromID(GetItemUserCode(temp.Item)) != null)
                                    {
                                        FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtObj = orderExtMgr.QueryByInpatineNoOrderID(myPatientInfo.ID, temp.ID);
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

                                this.fpOrder.ActiveSheet.Rows.Remove(row, 1);

                                //if (this.fpOrder.ActiveSheetIndex == 0)
                                //{
                                //    if (this.hsLongSubCombNo.Contains(temp.SubCombNO))
                                //    {
                                //        this.hsLongSubCombNo.Remove(temp.SubCombNO);
                                //    }
                                //}
                                //else
                                //{
                                //    if (this.hsShortSubCombNo.Contains(temp.SubCombNO))
                                //    {
                                //        this.hsShortSubCombNo.Remove(temp.SubCombNO);
                                //    }
                                //}
                            }
                        }
                    }

                    //先不管电子申请单了...这里选择多个删除的时候 有问题
                    if (this.pacsBillManagement.DeletePacsBill(order.Combo.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        MessageBox.Show(pacsBillManagement.Err);
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    //删除一行后选择下一行 
                    if (this.fpOrder.ActiveSheet.Rows.Count > 0)
                    {
                        this.SelectionChanged();
                    }
                }
                #endregion
            }
            else if (order.Status != 3)
            {
                //不允许医生取消已确认的终端项目的医嘱
                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT && order.Status == 2 && order.Item.IsNeedConfirm)
                {
                    ArrayList execOrderList = CacheManager.InOrderMgr.QueryExecOrderByOrderNo(order.ID, order.Item.ID, "1");
                    if (execOrderList.Count > 0)
                    {
                        MessageBox.Show("[" + order.ExeDept.Name + "]已经对[" + order.Item.Name + "]进行收费确认，请通知本科室护士与[" + order.ExeDept.Name + "]联系，" + "\n\n" + "确认患者是否已经执行[" + order.Item.Name + "]，如果已经执行，则该条医嘱不允许作废");
                        return -1;
                    }
                }
                //弹出停止窗口
                Forms.frmDCOrder f = new Forms.frmDCOrder();
                f.ShowDialog();
                if (f.DialogResult != DialogResult.OK) return 0;

                order.DCOper.OperTime = f.DCDateTime;
                order.DcReason = f.DCReason;
                order.DCOper.ID = CacheManager.InOrderMgr.Operator.ID;
                order.DCOper.Name = CacheManager.InOrderMgr.Operator.Name;
                order.EndTime = order.DCOper.OperTime;

                if (order.EndTime < order.BeginTime)
                {
                    MessageBox.Show("停止时间不能小于开始时间");
                    return -1;
                }

                if (f.DCDateTime > CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddHours(1))
                {
                    //预停止时间指定
                    #region 保存预停止
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    for (int row = 0; row < this.fpOrder.ActiveSheet.RowCount; row++)
                    {
                        temp = this.fpOrder.ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        //if (temp.Combo.ID == order.Combo.ID)
                        if (orderComboIDs.Contains(temp.Combo.ID))
                        {
                            temp.DCOper = order.DCOper;
                            temp.DcReason = order.DcReason;
                            temp.EndTime = order.EndTime;
                            //temp.Status = 7;
                            #region {D1A8C8BD-483D-4d10-B056-D7E4FD3F798E}
                            //原来代码在保存预停止时没有对同一组合的所有医嘱进行更新，现加入此段代码
                            ArrayList alTemp = new ArrayList();
                            alTemp = CacheManager.InOrderMgr.QueryOrderByCombNO(temp.Combo.ID, false);
                            if (alTemp != null && alTemp.Count > 1)
                            {
                                foreach (FS.HISFC.Models.Order.Inpatient.Order orderTemp in alTemp)
                                {
                                    if (orderTemp.ID == temp.ID) continue;
                                    orderTemp.DCOper = order.DCOper;
                                    orderTemp.DcReason = order.DcReason;
                                    orderTemp.EndTime = order.EndTime;

                                    if (CacheManager.InOrderMgr.UpdateOrder(orderTemp) == -1)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show(CacheManager.InOrderMgr.Err);
                                        return -1;
                                    }
                                }
                            }
                            #endregion
                            if (CacheManager.InOrderMgr.UpdateOrder(temp) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                MessageBox.Show(CacheManager.InOrderMgr.Err);
                                return -1;
                            }

                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["医嘱状态"]].Value = temp.Status;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止时间"]].Value = temp.DCOper.OperTime;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["结束时间"]].Value = temp.EndTime;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止人编码"]].Text = temp.DCOper.ID;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止人"]].Text = temp.DCOper.Name;
                            this.fpOrder.ActiveSheet.Rows[row].Tag = temp;

                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    #endregion
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    ArrayList alTemp = new ArrayList();

                    int tempState = -1;
                    for (int row = 0; row < this.fpOrder.ActiveSheet.RowCount; row++)
                    {
                        temp = this.fpOrder.ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        //if (temp.Combo.ID == order.Combo.ID)
                        if (orderComboIDs.Contains(temp.Combo.ID))
                        {
                            temp.DcReason = order.DcReason;
                            temp.DCOper = order.DCOper;
                            tempState = temp.Status;
                            temp.Status = 3;

                            #region 小时医嘱停止计费
                            if (this.DCHoursOrder(order, FS.FrameWork.Management.PublicTrans.Trans, true) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                CacheManager.OrderIntegrate.fee.Rollback();
                                temp.Status = tempState;
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg(order.Item.Name + "停止时记费失败！"));
                                return -1;
                            }
                            #endregion

                            #region 停止医嘱

                            string strReturn = "";
                            if (CacheManager.InOrderMgr.DcOrder(temp, true, out strReturn) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                CacheManager.OrderIntegrate.fee.Rollback();
                                temp.Status = tempState;
                                MessageBox.Show(CacheManager.InOrderMgr.Err);
                                return -1;
                            }

                            if (temp.Item.SysClass.ID.ToString() == "UC")
                            {
                                #region 无论有没,都去取消一下医技预约
                                appMgr.Cancle(temp.ID);
                                #endregion
                            }

                            //Add By liangjz 20005-08
                            if (strReturn != "")
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                CacheManager.OrderIntegrate.fee.Rollback();
                                temp.Status = tempState;
                                MessageBox.Show(strReturn);
                                return -1;
                            }
                            #endregion
                            //发送消息给护士
                            //FS.Common.Class.Message.SendMessage(this.GetPatient().Patient.Name + "的医嘱【" + temp.Item.Name + "】已经" + strTip, order.NurseStation.ID);
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["医嘱状态"]].Value = temp.Status;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止时间"]].Value = temp.DCOper.OperTime;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["结束时间"]].Value = temp.EndTime;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止人编码"]].Text = temp.DCOper.ID;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止人"]].Text = temp.DCOper.Name;
                            this.fpOrder.ActiveSheet.Rows[row].Tag = temp;

                            continue;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                //删除就不需要刷新了
                //this.RefreshOrderState();
            }
            else
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("已作废医嘱不可删除,作废或取消!"));
            }

            this.RefreshOrderState(-1);

            #region 即时消息

            this.SendMessage(SendType.Delete);

            #endregion

            #region 接入电子申请单
            if (this.isUsePACSApplySheet)
            {
                if (order.Status != 3 && (order.ApplyNo != null && order.ApplyNo != ""))
                {
                    if (IInpateintPacsApply == null)
                    {
                        this.InitPacsApply();
                    }
                    IInpateintPacsApply.Delete(this.myPatientInfo, order);
                }
            }
            #endregion

            return 0;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            //if (this.Patient.PVisit.PatientLocation.Dept.ID != CacheManager.LogEmpl.Dept.ID)
            //{
            //    MessageBox.Show("患者所属科室是【" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Patient.PVisit.PatientLocation.Dept.ID) + "】与登陆的开立科室【" + CacheManager.LogEmpl.Dept.Name + "】不一致，不允许开立！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return -1;
            //}

            if (this.IBeforeAddOrder != null)
            {
                if (this.IBeforeAddOrder.OnBeforeAddOrderForInPatient(this.Patient, this.GetReciptDept(), this.GetReciptDoc()) == -1)
                {
                    if (!string.IsNullOrEmpty(IBeforeAddOrder.ErrInfo))
                    {
                        MessageBox.Show(IBeforeAddOrder.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return -1;
                }
            }

            //{F38618E9-7421-423d-80A9-401AFED0B855}
            if (this.isShowOrderFinished == false)
            {
                //MessageBox.Show("刷新信息还未完成，请稍候再点击开立！");
                return -1;
            }

            //CountLongBegin = this.fpOrder_Long.Rows.Count;
            //CountShortBegin = this.fpOrder_Short.Rows.Count;
            CountLongBegin = 0;
            CountShortBegin = 0;

            // TODO:  添加 ucOrder.Add 实现
            if (this.myPatientInfo == null || this.myPatientInfo.ID == "")
            {
                return -1;
            }
            this.IsDesignMode = true;
            this.OrderType = this.myOrderType;

            if (this.OrderType == FS.HISFC.Models.Order.EnumType.LONG)
            {
                if (this.OrderCanOperatorChanged != null)
                    this.OrderCanOperatorChanged(false);
            }

            PassRefresh();

            this.ucItemSelect1.Clear(true);
            this.ucItemSelect1.Focus();

            #region 开立时 增加状态显示

            //if (SaveUserDefaultSetting())
            //{
            //}
            //else
            //{
            //    SaveUserDefaultSetting(true);
            //}

            #endregion

            return 0;
        }

        /// <summary>
        /// 设置患者医嘱是否处于开立状态
        /// 不允许多个医生同时编辑、打印一个患者的医嘱
        /// </summary>
        private void SaveUserDefaultSetting(bool isAddMode)
        {
            //应该自己建一个住院患者扩展表，用里面的某个状态存储吧...
            //某个字段，存储当前患者医嘱处于的状态及操作者
            // 0 无操作状态 1 开立状态；2 打印状态

            //注意同步修改医嘱单打印里面
        }

        /// <summary>
        /// 开立停止医嘱：术前医嘱、转科医嘱、出院医嘱等
        /// </summary>
        /// <returns>1术前医嘱；2转入医嘱 3 出院医嘱</returns>
        public int AddDCOrder(string type)
        {
            if (!this.IsDesignMode)
            {
                MessageBox.Show("当前不是医嘱开立状态，不能开立“术前医嘱”！");
                return -1;
            }

            if (this.myPatientInfo == null || this.myPatientInfo.ID == "")
            {
                MessageBox.Show("患者信息为空，请重新选中患者后操作！", "警告", MessageBoxButtons.OK);
                return -1;
            }

            string strOrderType = "";
            string strWarn = "";

            //1、出院医嘱；2、转科医嘱；3、死亡医嘱；4、术前医嘱；5、术后医嘱

            if (type == "1")
            {
                strOrderType = "出院医嘱";
                strWarn = "开立出院医嘱后，系统将停止当前在用的长期医嘱！";
            }
            else if (type == "2")
            {
                strOrderType = "转科医嘱";
                strWarn = "开立转科医嘱后，系统将停止当前在用的长期医嘱！";
            }
            else if (type == "3")
            {
                strOrderType = "死亡医嘱";
                strWarn = "开立死亡医嘱后，系统将停止当前在用的长期医嘱！";
            }
            else if (type == "4")
            {
                strOrderType = "术前医嘱";
                strWarn = "开立术前医嘱后，系统将停止当前在用的长期医嘱！";
            }
            else if (type == "5")
            {
                strOrderType = "术后医嘱";
                strWarn = "开立术后医嘱后，系统将停止当前在用的长期医嘱！";
            }

            else if (type == "6")
            {
                return DcTreatmenttype();
            }

            if (MessageBox.Show("确定要开立“" + strOrderType + "”吗？操作不可撤消！\r\n\r\n" + strWarn, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return -1;
            }

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            //停止医嘱放在这里，是因为里面有查询、事务，不要影响下面的新开立
            if (DcAllLongOrder(dtNow, new FS.FrameWork.Models.NeuObject("", type, "")) == -1)
            {
                return -1;
            }

            #region 生成嘱托临嘱

            try
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = new FS.HISFC.Models.Order.Inpatient.Order();
                inOrder.Patient = this.myPatientInfo;
                inOrder.ReciptDept = myReciptDept;
                inOrder.ReciptDoctor = myReciptDoc;
                inOrder.MOTime = dtNow;

                inOrder.OrderType = SOC.HISFC.BizProcess.Cache.Order.GetOrderType("ZL");

                inOrder.OrderType.IsDecompose = false;
                inOrder.OrderType.IsCharge = false;
                inOrder.Item.IsNeedConfirm = false;

                FS.HISFC.Models.Fee.Item.Undrug undrugItem = new FS.HISFC.Models.Fee.Item.Undrug();

                undrugItem.ID = "999";
                undrugItem.Name = strOrderType;
                if (strOrderType == "出院医嘱")
                {
                    undrugItem.SysClass.ID = "MRH";
                }
                if (strOrderType == "转科医嘱")
                {
                    undrugItem.SysClass.ID = "MRD";
                }
                else
                {
                    undrugItem.SysClass.ID = "M";
                }
                undrugItem.SysClass.Name = "描述医嘱";
                undrugItem.Qty = 1;
                undrugItem.PriceUnit = "个";
                undrugItem.IsNeedConfirm = false;

                inOrder.Item = undrugItem;

                inOrder.ExecOper.Dept = this.myReciptDept;
                inOrder.Frequency = Classes.Function.GetDefaultFrequency();
                inOrder.BeginTime = Classes.Function.GetDefaultMoBeginDate(1);

                inOrder.PageNo = -1;
                inOrder.RowNo = -1;
                inOrder.GetFlag = "0";

                inOrder.ID = "";
                inOrder.SubCombNO = this.GetMaxCombNo(inOrder, 1);

                this.fpOrder.ActiveSheetIndex = 1;

                if (this.AddNewOrder(inOrder, 1) == -1)
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            #endregion

            this.RefreshCombo();
            return 1;
        }

        /// <summary>
        /// 
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
        /// 保存医嘱
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //是否包含新增医嘱，用于MQ提示
            bool isContainsNewOrder = false;

            if (this.bIsDesignMode == false)
                return -1;

            this.ucItemSelect1.SetInputControlVisible(false);
            ucItemSelect1.SetFocus();

            //检查患者状态
            string errInfo = "";
            if (Classes.Function.CheckPatientState(this.myPatientInfo.ID, ref myPatientInfo, ref errInfo) == -1)
            {
                MessageBox.Show(errInfo);
                return -1;
            }

            ArrayList alSaveOrder = new ArrayList();
            if (this.CheckOrder(ref alSaveOrder) == -1)
            {
                return -1;
            }

            #region 保存处方前接口实现

            //用于提示、警告等等
            if (IBeforeSaveOrder != null)
            {
                if (IBeforeSaveOrder.BeforeSaveOrderForInPatient(this.myPatientInfo, this.GetReciptDept(), this.GetReciptDoc(), alSaveOrder) == -1)
                {
                    if (!string.IsNullOrEmpty(IBeforeSaveOrder.ErrInfo))
                    {
                        MessageBox.Show(IBeforeSaveOrder.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        IBeforeSaveOrder.ErrInfo = "";
                    }
                    return -1;
                }
            }

            #endregion

            #region 事务声明

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            #endregion

            #region 更改的医嘱
            List<FS.HISFC.Models.Order.Inpatient.Order> alOrder = new List<FS.HISFC.Models.Order.Inpatient.Order>();//保存长、临嘱
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            string checkMsg = "";
            List<FS.HISFC.Models.Order.Inpatient.Order> orderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();

            string strID = "";
            FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtObj = null;

            for (int i = 0; i < this.fpOrder.Sheets[0].Rows.Count; i++)
            {
                order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.Sheets[0].Rows[i].Tag;

                if (order.Status == 0 || order.Status == 5)
                {
                    isContainsNewOrder = true;
                    if (order.Status == 5)
                    {
                        string error = "";

                        //自动审核医嘱
                        int rtn = ValidOrderBefore(order, 0);

                        if (rtn == 1)
                        {
                            order.Status = 0;
                            order.ReciptDoctor.ID = FS.FrameWork.Management.Connection.Operator.ID;
                            order.ReciptDoctor.Name = FS.FrameWork.Management.Connection.Operator.Name;
                        }
                    }

                    order.SpeOrderType = Classes.Function.MakeSpeDrugType(this.myPatientInfo, order);

                    string name = order.Item.Name;
                    order = this.SetFirstUseQuanlity(order);
                    if (order == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(name + "首日量不能为空！");
                        return -1;
                    }

                    if (string.IsNullOrEmpty(order.ID))
                    {
                        strID = CacheManager.InOrderMgr.GetNewOrderID();
                        if (strID == "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("获得医嘱流水号出错！\r\n" + CacheManager.InOrderMgr.Err));
                            return -1;
                        }

                        order.ID = strID; //获得医嘱流水号

                        if (this.fpOrder.Sheets[0].Cells[i, dicColmSet["付数"]].Tag != null)
                        {
                            orderExtObj = new FS.HISFC.Models.Order.Inpatient.OrderExtend();
                            orderExtObj.InPatientNo = myPatientInfo.ID;
                            orderExtObj.MoOrder = order.ID;
                            orderExtObj.Indications = this.fpOrder.Sheets[0].Cells[i, dicColmSet["付数"]].Tag.ToString();
                            orderExtObj.Oper.ID = orderExtMgr.Operator.ID;

                            if (orderExtMgr.InsertOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("插入医嘱扩展信息错误：\r\n" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }
                    }

                    alOrder.Add(order);
                }

                orderList.Add(order);
            }

            DateTime beginTimeForShort = new DateTime();
            string comboNo = "";
            for (int i = 0; i < this.fpOrder.Sheets[1].Rows.Count; i++)
            {
                order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.Sheets[1].Rows[i].Tag;
                if (order.Status == 0 || order.Status == 5)
                {
                    #region 统一组合开始时间，避免病区发药界面因组合号的时间(秒)差异造成组合不能正常划符号显示
                    if (string.IsNullOrEmpty(comboNo))
                    {
                        comboNo = order.Combo.ID;
                    }
                    if (comboNo != order.Combo.ID)
                    {
                        beginTimeForShort = order.BeginTime;
                        comboNo = order.Combo.ID;
                    }
                    else
                    {
                        if (beginTimeForShort == null || beginTimeForShort.Year == 1)
                        {
                            beginTimeForShort = order.BeginTime;
                        }
                        else
                        {
                            order.BeginTime = beginTimeForShort;
                        }
                    }
                    #endregion

                    if (order.Status == 5)
                    {
                        string error = "";

                        //自动审核医嘱
                        int rtn = ValidOrderBefore(order, 0);

                        if (rtn == 1)
                        {
                            order.Status = 0;
                            order.ReciptDoctor.ID = FS.FrameWork.Management.Connection.Operator.ID;
                            order.ReciptDoctor.Name = FS.FrameWork.Management.Connection.Operator.Name;
                        }
                    }

                    order.SpeOrderType = Classes.Function.MakeSpeDrugType(this.myPatientInfo, order);

                    if (string.IsNullOrEmpty(order.ID))
                    {
                        strID = CacheManager.InOrderMgr.GetNewOrderID();
                        if (strID == "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("获得医嘱流水号出错！\r\n" + CacheManager.InOrderMgr.Err));
                            return -1;
                        }

                        order.ID = strID; //获得医嘱流水号

                        if (this.fpOrder.Sheets[1].Cells[i, dicColmSet["付数"]].Tag != null)
                        {
                            orderExtObj = new FS.HISFC.Models.Order.Inpatient.OrderExtend();
                            orderExtObj.InPatientNo = myPatientInfo.ID;
                            orderExtObj.MoOrder = order.ID;
                            orderExtObj.Indications = this.fpOrder.Sheets[1].Cells[i, dicColmSet["付数"]].Tag.ToString();
                            orderExtObj.Oper.ID = orderExtMgr.Operator.ID;

                            if (orderExtMgr.InsertOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("插入医嘱扩展信息错误：\r\n" + orderExtMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }
                    }

                    alOrder.Add(order);
                }

                orderList.Add(order);
            }

            int count = alOrder.Where(t => t.Item.SysClass.ID.ToString() == "MC")
                .Where(t => t.ExeDept.ID == (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                .Count();
            if (count > 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("会诊医嘱的执行科室应为会诊科室,请重新选择执行科室!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            #region 根据接口实现对医嘱信息进行补充判断

            if (this.IAlterOrderInstance != null)
            {
                //{76FBAEE1-C996-41b4-9D77-F6CE457F6518} 更改了接口内方法
                //if (this.IAlterOrderInstance.AlterOrderOnSaving(this.myPatientInfo, this.myReciptDoc, this.myReciptDept, ref orderList) == -1)
                //{
                //    return -1;
                //}
            }

            #endregion

            string err = "";//错误信息
            string strNameNotUpdate = "";//已经变化状态的医嘱不更新

            #region 附材绑定BUG addby xuewj

            foreach (string comboID in this.htSubs.Values)
            {
                if (CacheManager.InOrderMgr.DeleteOrderSubtbl(comboID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除附材项目信息出错！") + CacheManager.InOrderMgr.Err);
                    return -1;
                }
            }

            htSubs.Clear();

            #endregion

            if (CacheManager.OrderIntegrate.SaveOrder(alOrder, this.GetReciptDept().ID, out err, out strNameNotUpdate, FS.FrameWork.Management.PublicTrans.Trans) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("医嘱保存失败！") + "\n" + err);
                return -1;
            }

            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                if (strNameNotUpdate == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("医嘱保存成功！" + checkMsg));
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("部分医嘱保存失败！") + "\n" + strNameNotUpdate
                        + FS.FrameWork.Management.Language.Msg("医嘱状态已经在其它地方更改，无法进行更新，请刷新屏幕！"));
                }
            }
            #endregion

            #region 外部接口实现

            if (IAfterSaveOrder != null)
            {
                if (IAfterSaveOrder.OnSaveOrderForInPatient(this.myPatientInfo, this.GetReciptDept(), this.GetReciptDoc(), new ArrayList(alOrder)) != 1)
                {
                    MessageBox.Show(IAfterSaveOrder.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            #endregion

            #region 根据接口实现对医嘱信息进行补充判断

            if (this.IAlterOrderInstance != null)
            {
                if (this.IAlterOrderInstance.AlterOrderOnSaved(this.myPatientInfo, this.myReciptDoc, this.myReciptDept, ref orderList) == -1)
                {
                    return -1;
                }
            }

            #endregion

            #region 电子申请单
            if (this.isUsePACSApplySheet)
            {
                SavePacsApply();
            }
            #endregion

            #region 即时消息
            ////{7882B4CC-FA22-4530-9E5E-2E738DF1DEEC}
            //this.OnSendMessage(null, "");

            if (isContainsNewOrder)
            {
                this.SendMessage(SendType.Add);
            }

            #endregion

            this.IsDesignMode = false;
            this.isEdit = false;

            SaveUserDefaultSetting(false);
            //保存打印检查申请单
            RecipePrint();// {0045F3F6-1B1C-4d0a-A834-8BD07286E175}

            #region 精麻方医嘱打印
            if (this.IRecipePrintST == null)
            {
                this.IRecipePrintST = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder),
                    typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST)) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST;
            }
            if (this.IRecipePrintST != null)
            {
                string where = " where met_ipm_order.inpatient_no='{0}' and met_ipm_order.mo_stat='0' and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ( 'P1','P2','S1','SY') and nvl(b.SPECIAL_FLAG4,'0')!='13')";
                where = string.Format(where, this.myPatientInfo.ID);
                FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTemp = ordMgr.QueryOrderBase(where);
                if (alOrderTemp.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("存在精麻处方，是否需要打印？", "精麻处方", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}
                        //FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST ucRecipePrintST = new FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST();
                        //ucRecipePrintST.MakaLabel(ucRecipePrintST.ChangeOrderToOrderST(alOrderTemp));
                        //ucRecipePrintST.SetPatientInfo(this.myPatientInfo);
                        //ucRecipePrintST.PrintRecipe();
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        this.IRecipePrintST.OnInPatientPrint(this.myPatientInfo, obj, obj, alOrderTemp, alOrderTemp, false, false, "", obj);
                    }
                }

                //重点品种精二
                string whereImp = " where met_ipm_order.inpatient_no='{0}' and met_ipm_order.mo_stat='0' and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ('P2') and b.SPECIAL_FLAG4='13')";
                whereImp = string.Format(whereImp, this.myPatientInfo.ID);
                FS.HISFC.BizLogic.Order.Order ordMgrImp = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTempImp = ordMgr.QueryOrderBase(whereImp);
                if (alOrderTempImp.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("存在重点品种精二，是否需要打印？", "重点品种处方", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}
                        //FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST ucRecipePrintST = new FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST();
                        //ucRecipePrintST.MakaLabel(ucRecipePrintST.ChangeOrderToOrderST(alOrderTemp));
                        //ucRecipePrintST.SetPatientInfo(this.myPatientInfo);
                        //ucRecipePrintST.PrintRecipe();
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        this.IRecipePrintST.OnInPatientPrint(this.myPatientInfo, obj, obj, alOrderTempImp, alOrderTempImp, false, false, "", obj);
                    }
                }

                //重点品种全麻
                string whereImpM = " where met_ipm_order.inpatient_no='{0}' and met_ipm_order.mo_stat='0' and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ('Q') and b.SPECIAL_FLAG4='13')";
                whereImp = string.Format(whereImpM, this.myPatientInfo.ID);
                FS.HISFC.BizLogic.Order.Order ordMgrImpM = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTempImpM = ordMgr.QueryOrderBase(whereImp);
                if (alOrderTempImp.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("存在重点品种全麻，是否需要打印？", "重点品种处方", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}
                        //FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST ucRecipePrintST = new FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST();
                        //ucRecipePrintST.MakaLabel(ucRecipePrintST.ChangeOrderToOrderST(alOrderTemp));
                        //ucRecipePrintST.SetPatientInfo(this.myPatientInfo);
                        //ucRecipePrintST.PrintRecipe();
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        this.IRecipePrintST.OnInPatientPrint(this.myPatientInfo, obj, obj, alOrderTempImpM, alOrderTempImpM, false, false, "", obj);
                    }
                }




            }
            #endregion
            return 0;
        }

        #region  add by lijp 2011-11-25 电子申请单添加 {102C4C01-8759-4b93-B4BA-1A2B4BB1380E}

        /// <summary>
        ///  保存申请单信息
        /// </summary>
        public void SavePacsApply()
        {
            if (IInpateintPacsApply == null)
            {
                this.InitPacsApply();
            }
            IInpateintPacsApply.Save(this.myPatientInfo, null);
        }

        /// <summary>
        /// 编辑申请单
        /// </summary>
        public void EditPascApply()
        {
            try
            {
                if (!this.isUsePACSApplySheet)
                {
                    MessageBox.Show("未启用申请单");
                    return;
                }

                if (this.fpOrder.ActiveSheet != this.fpOrder_Short)
                {
                    return;
                }

                if (IInpateintPacsApply == null)
                {
                    this.InitPacsApply();
                }

                // 从医嘱获取申请单号。
                FS.HISFC.Models.Order.Inpatient.Order order =
                    this.GetObjectFromFarPoint(this.fpOrder_Short.ActiveRowIndex, this.fpOrder.ActiveSheetIndex);
                int rev = IInpateintPacsApply.Edit(this.myPatientInfo, order);
                if (rev < 0)
                {
                    MessageBox.Show(IInpateintPacsApply.ErrInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!string.IsNullOrEmpty(IInpateintPacsApply.ErrInfo))
                {
                    Classes.Function.ShowBalloonTip(3, IInpateintPacsApply.ErrInfo, "提示", ToolTipIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("没有开立有效的检查项目医嘱");
            }
        }

        #endregion

        public int JudgeSpecialOrder()
        {
            int i = this.fpOrder.ActiveSheet.ActiveRowIndex;
            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("请先选择一条医嘱！");
                return -1;
            }
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[i].Tag;
            if (order.Status == 5)
            {
                FS.HISFC.Models.Base.Employee doct = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(FS.FrameWork.Management.Connection.Operator.ID);
                string strLevel = CacheManager.ContrlManager.GetControlParam<string>("200034", true, "2");
                if (doct.Level.ID == strLevel)
                {
                    order.Status = 0;
                    this.fpOrder.ActiveSheet.Rows[i].Tag = order;
                    this.fpOrder.ActiveSheet.Cells[i, dicColmSet["医嘱状态"]].Text = order.Status.ToString();
                }

            }
            return 0;
        }

        public int HerbalOrder()
        {
            string orderTypeFlag = "1";		//0 长嘱 1 临嘱

            FS.HISFC.Models.Order.Inpatient.Order ord;
            if (this.fpOrder.ActiveSheet.ActiveRowIndex >= 0 && this.fpOrder.ActiveSheet.Rows.Count > 0)
            {
                ord = this.fpOrder.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.Order;
                #region 处理草药问题{7985420C-9CF9-4dd3-BED4-A5CC0EC9D52C}
                //if (ord != null && ord.Status != null && ord.Status == 0)
                if (ord != null && ord.Item.SysClass.ID.ToString() == "PCC" && ord.Status == 0)
                {//{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
                    this.ModifyHerbal();
                    return 1;
                }
                #endregion
                #region {7985420C-9CF9-4dd3-BED4-A5CC0EC9D52C}
                else
                {
                    using (ucHerbalOrder uc = new ucHerbalOrder(false, this.OrderType, this.GetReciptDept().ID))
                    {
                        uc.Patient = this.myPatientInfo;
                        #region {49026086-DCA3-4af4-A064-58F7479C324A}
                        uc.refreshGroup += new RefreshGroupTree(uc_refreshGroup);
                        #endregion
                        FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                        if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                        {
                            int subCombNo = -1;
                            foreach (FS.HISFC.Models.Order.Inpatient.Order info in uc.AlOrder)
                            {
                                if (subCombNo == -1)
                                {
                                    if (info.SubCombNO > 0)
                                    {
                                        subCombNo = info.SubCombNO;
                                    }
                                    else
                                    {
                                        subCombNo = this.GetMaxCombNo(info, this.fpOrder.ActiveSheetIndex);
                                    }
                                }

                                info.SubCombNO = subCombNo;
                                info.GetFlag = "0";
                                info.RowNo = -1;
                                info.PageNo = -1;

                                this.AddNewOrder(info, this.OrderType == FS.HISFC.Models.Order.EnumType.LONG ? 0 : 1);
                            }
                            uc.Clear();
                            this.RefreshCombo();
                        }
                    }
                }
                #endregion
            }
            else
            {
                using (ucHerbalOrder uc = new ucHerbalOrder(false, this.OrderType, this.GetReciptDept().ID))
                {
                    uc.Patient = this.myPatientInfo;
                    #region {49026086-DCA3-4af4-A064-58F7479C324A}
                    uc.refreshGroup += new RefreshGroupTree(uc_refreshGroup);
                    #endregion
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                    if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                    {
                        int subCombNo = -1;
                        foreach (FS.HISFC.Models.Order.Inpatient.Order info in uc.AlOrder)
                        {
                            if (subCombNo == -1)
                            {
                                if (info.SubCombNO > 0)
                                {
                                    subCombNo = info.SubCombNO;
                                }
                                else
                                {
                                    subCombNo = this.GetMaxCombNo(info, this.fpOrder.ActiveSheetIndex);
                                }
                            }

                            info.SubCombNO = subCombNo;

                            info.GetFlag = "0";
                            info.RowNo = -1;
                            info.PageNo = -1;

                            this.AddNewOrder(info, this.OrderType == FS.HISFC.Models.Order.EnumType.LONG ? 0 : 1);
                        }
                        uc.Clear();
                        this.RefreshCombo();
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 选择医生{D5517722-7128-4d0c-BBC4-1A5558A39A03}用于登陆人员不是医生时使用
        /// </summary>
        /// <returns></returns>
        public int ChooseDoctor()
        {
            try
            {
                ucChooseDoct uc = new ucChooseDoct();
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "选择";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                if (uc.ChooseDoct.ID != null && uc.ChooseDoct.ID != "")
                {
                    this.SetReciptDoc(uc.ChooseDoct);
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.F11)
            //{
            //    this.HerbalOrder();
            //}
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region 公开函数

        /// <summary>
        /// 添加新医嘱
        /// </summary>
        /// <param name="sender"></param>
        public int AddNewOrder(FS.HISFC.Models.Order.Inpatient.Order inOrder, int SheetIndex)
        {
            if (this.ValidOrderBefore(inOrder, SheetIndex) == -1)
            {
                return -1;
            }

            //开立 预约出院 医嘱弹出 录入转归和停止长期医嘱框
            if (inOrder.Item.SysClass.ID.ToString().Equals("MRH")
                || inOrder.Item.Name.Contains("出院")
                || inOrder.Item.Name.Contains("死亡"))
            {
                if (this.DcAllLongOrderAndZG() == -1)
                {
                    return -1;
                }
            }
            else if (inOrder.Item.SysClass.ID.ToString() == "MRD" || inOrder.Item.Name.Contains("转科"))
            {

                if (this.iClinicPath == null)
                {
                    iClinicPath = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IClinicPath))
                   as FS.HISFC.BizProcess.Interface.Common.IClinicPath;
                }
                string strc = string.Empty;
                if (iClinicPath != null)
                {
                    if (iClinicPath.PatientIsSelectedPath(this.myPatientInfo.ID))
                    {
                        strc = "由于病人转科，系统自动将该患者中途退出路径！";
                    }
                }

                if (DcAllLongOrder(strc) == -1)
                {
                    return -1;
                }
                if (iClinicPath != null)
                {
                    if (iClinicPath.PatientIsSelectedPath(this.myPatientInfo.ID))
                    {
                        if (!iClinicPath.StopClinicPath(this.myPatientInfo.ID))
                        {
                            MessageBox.Show("退出路径患者失败！");
                            return -1;
                        }
                    }
                }
            }

            #region 根据接口实现对医嘱信息进行补充判断

            if (!this.EditGroup)
            {
                if (this.IAlterOrderInstance != null)
                {
                    if (this.IAlterOrderInstance.AlterOrder(this.myPatientInfo, this.myReciptDoc, this.myReciptDept, ref inOrder) == -1)
                    {
                        return -1;
                    }
                }

                if (this.IBeforeAddItem != null)
                {
                    ArrayList alOrderTemp = new ArrayList();

                    alOrderTemp.Add(inOrder);
                    if (this.IBeforeAddItem.OnBeforeAddItemForInPatient(this.myPatientInfo, this.myReciptDoc, this.myReciptDept, alOrderTemp) == -1)
                    {
                        return -1;
                    }
                }
            }
            #endregion

            dirty = true;

            #region 检查互斥
            if (CheckMutex(inOrder.Item.SysClass) == -1)
            {
                return -1;
            }
            #endregion

            #region 检查添加的东西
            if (inOrder.Item.SysClass.ID.ToString() == "UC")
            {
                //设置可以对该条医嘱的检查单填写
                //this.IsPrintTest(true);
            }
            else if (inOrder.Item.SysClass.ID.ToString() == "MC")
            {
                //添加会诊申请
                this.AddConsultation(inOrder);
            }

            if (this.myPatientInfo != null)
            {
                inOrder.Patient = this.myPatientInfo;
            }

            if (inOrder.Item.ItemType == EnumItemType.Drug)
            {
                string errInfo = "";
                inOrder.StockDept.ID = string.Empty;
                if (CacheManager.OrderIntegrate.FillPharmacyItemWithStockDept(ref inOrder, out errInfo) == -1)
                {
                    MessageBox.Show(errInfo);
                    return -1;
                }

                //药品
                if (((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).IsAllergy)
                {
                    if (MessageBox.Show("【" + inOrder.Item.Name + "】\n是否需要皮试！", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        inOrder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest;
                    }
                    else
                    {
                        //需要皮试 
                        inOrder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                    }
                }
                else
                {
                    inOrder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                }

                //判断药品是否毒麻药，给提示
                try
                {
                    if (((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).Quality.ID.Substring(0, 1) == "S")
                    {
                        MessageBox.Show("药品【" + inOrder.Item.Name + "】属于毒麻药，需要附加打印毒麻药处方到药房取药!", "提示", MessageBoxButtons.OK);
                    }

                    //{CB5C628A-EA63-41e7-9D38-3F3DF2E78834}


                    if (inOrder.Item.SpecialFlag == "1" || inOrder.Item.SpecialFlag == "2" || inOrder.Item.SpecialFlag == "3" || inOrder.Item.SpecialFlag == "4")
                    {
                        string level = "";
                        if (inOrder.Item.SpecialFlag == "1")
                        {
                            level = "A级";
                            MessageBox.Show("★药品【" + inOrder.Item.Name + "】属于" + level + "高警示药品，注意核对药品名称、规格、用法、剂量、使用浓度及滴速等!", "提示", MessageBoxButtons.OK);
                        }

                        if (inOrder.Item.SpecialFlag == "2")
                        {
                            level = "B级";
                            MessageBox.Show("☆药品【" + inOrder.Item.Name + "】属于" + level + "高警示药品，注意核对药品名称、规格、用法、剂量等!", "提示", MessageBoxButtons.OK);
                        }

                        if (inOrder.Item.SpecialFlag == "3")
                            level = "C级";

                        if (inOrder.Item.SpecialFlag == "4")
                        {
                            level = "易混淆";
                            MessageBox.Show("▲药品【" + inOrder.Item.Name + "】属于" + level + "药品，注意核对药品名称、规格等!", "提示", MessageBoxButtons.OK);
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                if (!inOrder.OrderType.IsDecompose)
                {
                    inOrder.Frequency = Classes.Function.GetDefaultFrequency();
                }
            }
            #endregion

            if (this.GetReciptDept() != null)
            {
                inOrder.ReciptDept.ID = this.GetReciptDept().ID;
                inOrder.ReciptDept.Name = this.GetReciptDept().Name;
            }
            if (this.GetReciptDoc() != null)
            {
                inOrder.ReciptDoctor.ID = this.GetReciptDoc().ID;
                inOrder.ReciptDoctor.Name = this.GetReciptDoc().Name;
            }

            if (string.IsNullOrEmpty(inOrder.ExeDept.ID))
            {
                //inOrder.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(inOrder.ReciptDept, inOrder, inOrder.ExeDept.ID, false);
                //inOrder.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(inOrder.ExeDept.ID);

                inOrder.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(false, inOrder.ReciptDept.ID, inOrder.ExeDept.ID, inOrder.Item.ID);
                inOrder.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(inOrder.ExeDept.ID);


                //inOrder.ExeDept.ID = this.GetReciptDept().ID;
                //inOrder.ExeDept.Name = this.GetReciptDept().Name;
            }
            else
            {
                inOrder.ExeDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(inOrder.ExeDept.ID);
            }

            //重新处理执行科室
            if (inOrder.Item.ItemType == EnumItemType.Drug)
            {
                inOrder.ExeDept = this.GetExecDept(patientType, inOrder.ReciptDept);
            }

            #region add by zhaorong at 2013-8-29 start 医嘱的用法与执行科室均在维护的常数队列中时，默认执行科室为：6030（静脉配置中心药房）
            this.ChangeStockDept(ref inOrder);
            #endregion


            if (inOrder.Combo.ID == "")
            {
                try
                {
                    inOrder.Combo.ID = CacheManager.InOrderMgr.GetNewOrderComboID();//添加组合号
                }
                catch
                {
                    MessageBox.Show("获得医嘱组合号出错" + CacheManager.InOrderMgr.Err);
                    return -1;
                }
            }

            #region 开立时间

            if (inOrder.BeginTime < new DateTime(2000, 1, 1))
            {
                inOrder.BeginTime = Classes.Function.GetDefaultMoBeginDate(SheetIndex);
            }

            if (inOrder.User03 != "")
            {
                //组套的时间间隔
                int iDays = FS.FrameWork.Function.NConvert.ToInt32(inOrder.User03);
                if (iDays > 0)
                {
                    //是时间间隔>0
                    inOrder.BeginTime = inOrder.BeginTime.AddDays(iDays);
                }
            }

            #endregion

            //重整医嘱的 上次、下次分解时间不变
            if (inOrder.ReTidyInfo == null
                || (inOrder.ReTidyInfo != null && !inOrder.ReTidyInfo.Contains("重整医嘱")))
            {
                inOrder.CurMOTime = DateTime.MinValue;
                inOrder.NextMOTime = DateTime.MinValue;
            }
            inOrder.EndTime = DateTime.MinValue;

            this.currentOrder = inOrder;

            #region 修改为倒序了...
            //this.fpOrder.Sheets[SheetIndex].Rows.Add(this.fpOrder.Sheets[SheetIndex].RowCount, 1);
            //this.AddObjectToFarpoint(inOrder, this.fpOrder.Sheets[SheetIndex].RowCount - 1, SheetIndex, ColmSet.ALL);
            //this.fpOrder.ActiveSheet.ActiveRowIndex = this.fpOrder.ActiveSheet.RowCount - 1;
            #endregion


            this.fpOrder.Sheets[SheetIndex].Rows.Add(0, 1);
            this.AddObjectToFarpoint(inOrder, 0, SheetIndex, ColmSet.ALL);
            this.fpOrder.ActiveSheet.ActiveRowIndex = 0;

            //设置当前活动行索引为0
            this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;

            RefreshOrderState(ActiveRowIndex);
            this.fpOrder.ShowRow(0, this.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);

            dirty = false;

            #region 处理医保限制性用药
            //{A36B0D1B-C568-4d21-8507-763C9DC1E369}医保上线，取消过去的医保限制
            //开启

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
                && this.isOpenDrugWarn)
            {
                FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(GetItemUserCode(inOrder.Item));
                if (indicationsObj != null)
                {
                    if (MessageBox.Show("药品【" + inOrder.Item.Name + "】属于限制级药品，\r\n\r\n限制药品说明：【" + indicationsObj.Name + "】\r\n\r\n请确定医保报销设定。报销(是)，自费(否)?\r\n", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        //暂用名称列的tag值存标记
                        fpOrder.ActiveSheet.Cells[fpOrder.ActiveSheet.ActiveRowIndex, dicColmSet["付数"]].Tag = "1";
                    }
                    else
                    {
                        fpOrder.ActiveSheet.Cells[fpOrder.ActiveSheet.ActiveRowIndex, dicColmSet["付数"]].Tag = "0";
                    }
                }
            }

            #endregion

            #region 处理附材

            if (this.myPatientInfo != null && !string.IsNullOrEmpty(this.myPatientInfo.ID))
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
                        if (IDealSubjob.DealSubjob(this.myPatientInfo, true, inOrder, alOrder, ref alSubOrder, ref errText) <= 0)
                        {
                            MessageBox.Show("处理辅材失败！" + errText);
                            return -1;
                        }

                        if (alSubOrder != null && alSubOrder.Count > 0)
                        {
                            FS.HISFC.Models.Order.Inpatient.Order newOrder = null;
                            foreach (FS.HISFC.Models.Base.Item item in alSubOrder)
                            {
                                newOrder = new FS.HISFC.Models.Order.Inpatient.Order();
                                //newOrder = inOrder.Clone();
                                newOrder = inOrder;
                                newOrder.Item = item;
                                newOrder.Qty = item.Qty;
                                newOrder.Unit = item.PriceUnit;
                                newOrder.IsSubtbl = true;
                                newOrder.Usage = new FS.FrameWork.Models.NeuObject();
                                //newOrder.ExeDept = newOrder.Patient.PVisit.PatientLocation.Dept.Clone();
                                newOrder.ExeDept.ID = inOrder.ExeDept.ID;
                                newOrder.ExeDept.Name = inOrder.ExeDept.Name;


                                //此处不能获取ID
                                //newOrder.ID = CacheManager.InOrderMgr.GetNewOrderID();
                                //newOrder.SortID = this.GetSortIDBySubCombNo(newOrder.SubCombNO);

                                this.fpOrder.ActiveSheet.Rows.Add(this.fpOrder.ActiveSheet.RowCount, 1);

                                this.AddObjectToFarpoint(newOrder, this.fpOrder.ActiveSheet.RowCount - 1, fpOrder.ActiveSheetIndex, ColmSet.ALL);
                            }
                            this.fpOrder.ActiveSheet.ActiveRowIndex = this.fpOrder.ActiveSheet.RowCount - 1;
                            //设置当前活动行索引为0
                            this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;
                            RefreshOrderState(ActiveRowIndex);
                        }
                    }
                }
                dirty = false;
            }
            #endregion
            this.RefreshCombo();

            #region 合理用药
            if (this.IReasonableMedicine != null
                && this.IReasonableMedicine.PassEnabled
                && this.enabledPass)
            {
                this.IReasonableMedicine.PassShowFloatWindow(false);

                int iSheetIndex = this.OrderType == FS.HISFC.Models.Order.EnumType.SHORT ? 1 : 0;
                FS.HISFC.Models.Order.Inpatient.Order info = this.GetObjectFromFarPoint(FS.FrameWork.Function.NConvert.ToInt32(this.ActiveTempID), iSheetIndex);
                if (info == null)
                {
                    return 1;
                }
                if (info.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    return 1;
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
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            #endregion

            //开立出院医嘱的提示框代码放这里

            return 1;
        }
        /// <summary>
        /// add by zhaorong at 2013-8-29 start 医嘱的用法与执行科室均在维护的常数队列中时，默认执行科室为：6030（静脉配置中心药房）
        /// </summary>
        /// <param name="inOrder"></param>
        private bool ChangeStockDept(ref FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            #region 参数
            //执行科室信息(pivas常数)
            if (this.deptItemList == null)
            {
                this.deptItemList = CacheManager.GetConList("DeptItemNew");
            }
            //用法(pivas常数)
            if (this.drugUsageList == null)
            {
                this.drugUsageList = CacheManager.GetConList("USAGE#USAGE");
            }
            //长期医嘱是否开通pivas功能标识
            if (string.IsNullOrEmpty(this.pivasCzFlag))
            {
                this.pivasCzFlag = CacheManager.ContrlManager.GetControlParam<string>("PIVASC", false, "0");
            }
            //临时医嘱是否开通pivas功能标识
            if (string.IsNullOrEmpty(this.pivasLzFlag))
            {
                this.pivasLzFlag = CacheManager.ContrlManager.GetControlParam<string>("PIVASL", false, "0");
            }
            #endregion

            int isChangeStock = 0;//是否更换扣库科室标记
            if (deptItemList != null && drugUsageList != null)
            {
                foreach (FS.HISFC.Models.Base.Const deptItem in deptItemList)
                {
                    if (deptItem.ID.Equals(inOrder.ExeDept.ID))
                    {
                        isChangeStock++;
                        break;
                    }
                }
                foreach (FS.HISFC.Models.Base.Const drugUsage in drugUsageList)
                {
                    if (drugUsage.ID.Equals(inOrder.Usage.ID))
                    {
                        isChangeStock++;
                        break;
                    }
                }
                //判断是否为长嘱且是否开通pivas功能
                bool pivasCz = (inOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG && "1".Equals(this.pivasCzFlag));
                //判断是否为临嘱且是否开通pivas功能
                bool pivasLz = (inOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT && "1".Equals(this.pivasLzFlag));

                #region 更换扣库科室
                if (isChangeStock == 2)
                {
                    //肿瘤一区(0911)与肿瘤二区(0921)不区分长嘱与临嘱,全部同时上线
                    if ("0911".Equals(inOrder.ReciptDept.ID) || "0921".Equals(inOrder.ReciptDept.ID))
                    {
                        inOrder.StockDept = SOC.HISFC.BizProcess.Cache.Common.GetDept("6030");//静脉配置中心药房
                        return true;
                    }
                    else if (pivasCz || pivasLz)
                    {
                        inOrder.StockDept = SOC.HISFC.BizProcess.Cache.Common.GetDept("6030");//静脉配置中心药房
                        return true;
                    }
                    else
                    {
                        List<FS.HISFC.Models.Pharmacy.Item> itemList = CacheManager.PhaIntegrate.QueryItemAvailableListByItemCode(inOrder.ExeDept.ID, "I", inOrder.Item.ID);
                        if (itemList.Count > 0)
                        {
                            FS.HISFC.Models.Pharmacy.Item item = itemList[0] as FS.HISFC.Models.Pharmacy.Item;
                            inOrder.StockDept = SOC.HISFC.BizProcess.Cache.Common.GetDept(item.User02);//执行药房
                        }
                        return false;
                    }

                }
                else
                {
                    List<FS.HISFC.Models.Pharmacy.Item> itemList = CacheManager.PhaIntegrate.QueryItemAvailableListByItemCode(inOrder.ExeDept.ID, "I", inOrder.Item.ID);
                    if (itemList.Count > 0)
                    {
                        FS.HISFC.Models.Pharmacy.Item item = itemList[0] as FS.HISFC.Models.Pharmacy.Item;
                        inOrder.StockDept = SOC.HISFC.BizProcess.Cache.Common.GetDept(item.User02);//执行药房
                    }
                    return false;
                }
                #endregion
            }
            else
            {
                return false;
            }
        }

        /// <summary> 
        /// 添加手术申请
        /// </summary>
        public void AddOperation()
        {
            //if (this.PatientInfo == null)
            //{
            //    MessageBox.Show("请先选择患者！");
            //    return;
            //}
            //frmApply dlgTempApply = new frmApply(FS.Common.Class.Main.var, this.PatientInfo);
            //dlgTempApply.SetClearButtonFasle();
            //dlgTempApply.ISCloseNow = true;
            ////显示临时申请窗口(模式)
            //dlgTempApply.ShowDialog();

            ////下面的代码非必须
            //if (dlgTempApply.ExeDept != "")
            //{
            //    //按下"确定"按钮
            //    FS.FrameWork.Models.NeuObject mainOperation = new FS.FrameWork.Models.NeuObject();//获得主手术
            //    for (int i = 0; i < dlgTempApply.apply.OperateInfoAl.Count; i++)
            //    {
            //        FS.HISFC.Models.Operator.OperateInfo obj = dlgTempApply.apply.OperateInfoAl[i] as FS.HISFC.Models.Operator.OperateInfo;
            //        if (i == 0)
            //        {
            //            mainOperation.ID = obj.OperateItem.ID;
            //            mainOperation.Name = obj.OperateItem.Name;
            //        }
            //        if (obj.bMainFlag)
            //        {
            //            //是主手术
            //            mainOperation.ID = obj.OperateItem.ID;
            //            mainOperation.Name = obj.OperateItem.Name;
            //            break;
            //        }
            //    }
            //    FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
            //    FS.HISFC.Models.Fee.Item item = new FS.HISFC.Models.Fee.Item();
            //    Order.Inpatient.OrderType = (FS.HISFC.Models.Order.Inpatient.OrderType)this.ucItemSelect1.SelectedOrderType.Clone();

            //    order.Item = item;
            //    order.Item.SysClass.ID = "UO";

            //    order.Item.ID = mainOperation.ID;
            //    order.Qty = 1;
            //    order.Unit = "次";
            //    order.Item.Name = mainOperation.Name;
            //    order.ExeDept.ID = dlgTempApply.ExeDept; /*执行科室*/
            //    order.Frequency.ID = "QD";
            //    //设置手术医嘱默认为术前临嘱
            //    if (this.ucItemSelect1.alShort.Count > 0)
            //    {
            //        FS.HISFC.Models.Order.Inpatient.OrderType info;
            //        for (int i = 0; i < this.ucItemSelect1.alShort.Count; i++)
            //        {
            //            info = this.ucItemSelect1.alShort[i] as FS.HISFC.Models.Order.Inpatient.OrderType;
            //            if (info == null)
            //                return;
            //            if (info.ID == "SQ")
            //            {  //SQ 术前临嘱 SZ 术前嘱托
            //                Order.Inpatient.OrderType = info;
            //                break;
            //            }
            //        }
            //    }
            //    //this.ValidNewOrder(order);
            //    this.AddNewOrder(order, this.fpOrder.ActiveSheetIndex);

            //}
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            this.ucItemSelect1.Clear(false);

            this.ucItemSelect1.ucInputItem1.Select();
            this.ucItemSelect1.ucInputItem1.Focus();
        }

        /// <summary>
        /// 添加检查、检验申请
        /// </summary>
        public void AddTest()
        {
            if (this.myPatientInfo == null)
            {
                MessageBox.Show("请先选择患者！");
                return;
            }
            List<FS.HISFC.Models.Order.Inpatient.Order> alItems = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            int iActiveSheet = 1;//检查单默认临时医嘱

            //{47C187AE-F3FC-433c-AA2D-F1C146ED4F92}  仅选择检查医嘱时才进行检查申请单开立
            this.fpOrder.ActiveSheetIndex = 1;
            this.OrderType = FS.HISFC.Models.Order.EnumType.SHORT;//{A762E223-39EE-4379-AADB-B5A929F85D41}

            for (int i = 0; i < this.fpOrder.Sheets[iActiveSheet].RowCount; i++)
            {
                if (this.fpOrder.Sheets[iActiveSheet].IsSelected(i, 0))
                {
                    //{47C187AE-F3FC-433c-AA2D-F1C146ED4F92}  仅选择检查医嘱时才进行检查申请单开立
                    FS.HISFC.Models.Order.Inpatient.Order tempOrder = this.GetObjectFromFarPoint(i, iActiveSheet);
                    if (tempOrder.Item.SysClass.ID.ToString() == "UC")         //仅限于检查项目
                    {
                        //将alItems内容改为order类型
                        alItems.Add(tempOrder);
                    }
                }
            }
            if (alItems.Count <= 0)
            {
                //没有选择项目信息
                MessageBox.Show("请选择开立的检查信息!");
                return;
            }

            this.checkPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPrint)) as FS.HISFC.BizProcess.Interface.Common.ICheckPrint;
            #region {3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}
            //检查{3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}pacs接口新增
            //if (this.isInitPacs)
            //{
            FS.HISFC.Models.Order.Inpatient.Order temp = null;
            temp = this.GetObjectFromFarPoint(this.fpOrder.Sheets[iActiveSheet].ActiveRowIndex, iActiveSheet);
            if (temp.Item.SysClass.ID.ToString() == "UC")
            {
                if (this.pacsInterface == null)
                {
                    IInpateintPacsApply.Init(this.myPatientInfo);
                }
                if (this.pacsInterface != null)
                {
                    this.pacsInterface.OprationMode = "2";
                    this.pacsInterface.SetPatient(this.myPatientInfo);
                    this.pacsInterface.PlaceOrder(temp);
                    this.pacsInterface.ShowForm();

                    return;
                }
            }
            //}
            #endregion
            if (this.checkPrint == null)
            {
                this.checkPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPrint)) as FS.HISFC.BizProcess.Interface.Common.ICheckPrint;
                if (this.checkPrint == null)
                {
                    MessageBox.Show("获得接口IcheckPrint错误\n，可能没有维护相关的打印控件或打印控件没有实现接口检验接口IcheckPrint\n请与系统管理员联系。");
                    return;
                }
            }
            this.checkPrint.Reset();
            this.checkPrint.ControlValue(myPatientInfo, alItems);
            this.checkPrint.Show();


            //FS.HISFC.Models.RADT.PatientInfo p = this.GetPatient().Clone();
            //string combo = "";
            //if (alItems.Count > 1)
            //{
            //    combo = (alItems[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID;
            //    for (int i = 1; i < alItems.Count; i++)
            //    {
            //        if (combo != (alItems[i] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
            //        {
            //            MessageBox.Show("您所选择的项目应该开立不同的检查单\n请重新选择！", "提示");
            //            return;
            //        }

            //    }
            //}
            //pacsInterface.frmPacsApply f = new pacsInterface.frmPacsApply(alItems, p);
            //if (f.ShowDialog() == DialogResult.OK)
            //{

            //}
        }

        /// <summary>
        /// 添加会诊
        /// </summary>
        /// <param name="sender"></param>
        public void AddConsultation(object sender)
        {
            //先屏蔽吧
            return;
            //if (this.myPatientInfo == null)
            //{
            //    MessageBox.Show("请先选择患者!");
            //    return;
            //}
            //FS.HISFC.Models.RADT.PatientInfo p = this.GetPatient().Clone();
            //((FS.HISFC.Models.Order.Inpatient.Order)sender).Patient = p;

            //ucConsultation uc = new ucConsultation(sender as FS.HISFC.Models.Order.Inpatient.Order);
            //uc.IsApply = true;
            //uc.DisplayPatientInfo(this.myPatientInfo);
            ////			FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            //FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
        }

        /// <summary>
        /// 组合
        /// </summary>
        /// <param name="k"></param>
        private void ComboOrder(int k)
        {
            //组合时 按照小的显示

            #region 组合医嘱

            int iSelectionCount = 0;

            FS.HISFC.Models.Order.Inpatient.Order combOrder = null;
            int subCombNo = -1;

            for (int i = 0; i < this.fpOrder.Sheets[k].Rows.Count; i++)
            {
                if (this.fpOrder.Sheets[k].IsSelected(i, 0))
                {
                    iSelectionCount++;
                    FS.HISFC.Models.Order.Inpatient.Order inOrder = this.GetObjectFromFarPoint(i, k);

                    if (!CheckOrderCanMove(inOrder))
                    {
                        MessageBox.Show("【" + inOrder.Item.Name + "】已经打印，不允许删除！\r\n\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (inOrder != null && (subCombNo < 0 || subCombNo > inOrder.SubCombNO))
                    {
                        subCombNo = inOrder.SubCombNO;
                        combOrder = inOrder;
                    }
                }
            }

            if (iSelectionCount <= 1)
            {
                MessageBox.Show("组合时请选中多条！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //校验组合医嘱
            if (this.ValidComboOrder() == -1)
            {
                return;
            }

            //for (int i = 0; i < this.fpOrder.Sheets[k].Rows.Count; i++)
            int newSubComb = 0;
            string combNo = "";
            for (int i = this.fpOrder.Sheets[k].RowCount - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = this.GetObjectFromFarPoint(i, k);
                if (this.fpOrder.Sheets[k].IsSelected(i, 0))
                {
                    if (!this.htSubs.ContainsKey(inOrder.ID))
                    {
                        this.htSubs.Add(inOrder.ID, inOrder.Combo.ID);
                    }

                    if (inOrder.Combo.ID != combOrder.Combo.ID)
                    {
                        inOrder.Combo.ID = combOrder.Combo.ID;
                        inOrder.SubCombNO = combOrder.SubCombNO;
                        inOrder.SortID = 0;

                        //this.AddObjectToFarpoint(inOrder, i, k, ColmSet.ALL);
                        GetOrderChanged(i, inOrder, ColmSet.Z组号);
                    }
                }
                else if (inOrder.SubCombNO > combOrder.SubCombNO)
                {
                    if (newSubComb == 0)
                    {
                        newSubComb = combOrder.SubCombNO;
                    }
                    if (!combNo.Contains(inOrder.Combo.ID))
                    {
                        newSubComb += 1;
                        combNo += inOrder.Combo.ID + "|";
                    }

                    inOrder.SubCombNO = newSubComb;
                    inOrder.SortID = 0;
                    GetOrderChanged(i, inOrder, ColmSet.Z组号);
                }
            }
            this.fpOrder.Sheets[k].ClearSelection();

            this.ucItemSelect1.Clear(false);

            #endregion
        }

        /// <summary>
        /// 组合医嘱
        /// </summary>
        public void ComboOrder()
        {
            ComboOrder(this.fpOrder.ActiveSheetIndex);
            this.RefreshCombo();
            //组合后同组合项目一起选中，否则会出现能修改组合中的某一个频次和别的药品不一样
            this.SelectionChanged();
        }

        /// <summary>
        /// 取消组合
        /// </summary>
        public void CancelCombo()
        {
            int iSelectionCount = 0;

            FS.HISFC.Models.Order.Inpatient.Order combOrder = null;

            //存储组合最下面的行号
            int combRowIndex = -1;

            for (int i = 0; i < this.fpOrder.Sheets[fpOrder.ActiveSheetIndex].Rows.Count; i++)
            {
                if (this.fpOrder.Sheets[fpOrder.ActiveSheetIndex].IsSelected(i, 0))
                {
                    iSelectionCount++;
                    FS.HISFC.Models.Order.Inpatient.Order inOrder = this.GetObjectFromFarPoint(i, fpOrder.ActiveSheetIndex);

                    if (!CheckOrderCanMove(inOrder))
                    {
                        MessageBox.Show("【" + inOrder.Item.Name + "】已经打印，不允许取消组合！\r\n\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (inOrder.Status.ToString() != "0" && inOrder.Status.ToString() != "5")
                    {
                        MessageBox.Show("【" + inOrder.Item.Name + "】非新开立医嘱，不允许取消组合！", "提示");
                        return;
                    }

                    if (!String.IsNullOrEmpty(inOrder.ApplyNo))
                    {
                        MessageBox.Show("【" + inOrder.Item.Name + "】已开立申请单，不允许取消组合！");
                        return;
                    }

                    if (inOrder != null && (combRowIndex < 0 || combRowIndex < i))
                    {
                        combRowIndex = i;
                        combOrder = inOrder;
                    }
                }
            }

            if (iSelectionCount <= 1)
            {
                MessageBox.Show("取消组合时请选中多条！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //组合号
            int newSubComb = 0;
            string combNo = "";

            //for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
            //取消组合时，需要自动上移组合号
            for (int i = this.fpOrder.ActiveSheet.Rows.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = this.GetObjectFromFarPoint(i, fpOrder.ActiveSheetIndex);

                if (this.fpOrder.ActiveSheet.IsSelected(i, 0))
                {
                    if (!this.htSubs.ContainsKey(inOrder.ID))
                    {
                        this.htSubs.Add(inOrder.ID, inOrder.Combo.ID);
                    }

                    if (newSubComb == 0)
                    {
                        newSubComb = combOrder.SubCombNO;
                    }

                    if (i != combRowIndex)
                    {
                        newSubComb += 1;

                        inOrder.Combo.ID = CacheManager.InOrderMgr.GetNewOrderComboID();

                        inOrder.SubCombNO = newSubComb;
                        inOrder.SortID = 0;

                        //this.ucItemSelect1_OrderChanged(inOrder, ColmSet.Z组号);
                        GetOrderChanged(i, inOrder, ColmSet.Z组号);
                    }
                }
                else if (i < combRowIndex)
                {
                    if (!combNo.Contains(inOrder.Combo.ID))
                    {
                        combNo += inOrder.Combo.ID + "|";
                        newSubComb += 1;
                    }

                    inOrder.SubCombNO = newSubComb;
                    inOrder.SortID = 0;

                    //this.ucItemSelect1_OrderChanged(inOrder, ColmSet.Z组号);
                    GetOrderChanged(i, inOrder, ColmSet.Z组号);
                }
            }
            this.fpOrder.ActiveSheet.ClearSelection();
            this.RefreshCombo();
            this.SelectionChanged();
        }

        /// <summary>
        /// 保存序号
        /// </summary>
        public void SaveSortID()
        {
            this.SaveSortID(true);
        }

        /// <summary>
        /// 查询时候的保存，正序保存
        /// </summary>
        /// <param name="prompt"></param>
        public void SaveSortID(bool prompt)
        {
            return;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(OrderManagement.Connection);
            //t.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                for (int i = 0; i < 2; i++)
                {
                    int k = 1;
                    for (int j = 0; j < fpOrder.Sheets[i].RowCount; j++)
                    {
                        if (CacheManager.InOrderMgr.UpdateOrderSortID(fpOrder.Sheets[i].Cells[j, dicColmSet["医嘱流水号"]].Text, (k++).ToString()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            MessageBox.Show(CacheManager.InOrderMgr.Err);
                            return;
                        }
                    }
                }
            }
            catch { FS.FrameWork.Management.PublicTrans.RollBack(); ; return; }
            FS.FrameWork.Management.PublicTrans.Commit();

            if (prompt) MessageBox.Show("医嘱顺序保存成功！");
        }

        protected void SaveSortID(int row)
        {
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(OrderManagement.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                if (CacheManager.InOrderMgr.UpdateOrderSortID(fpOrder.ActiveSheet.Cells[row, dicColmSet["医嘱流水号"]].Text, fpOrder.ActiveSheet.Cells[row, dicColmSet["顺序号"]].Value.ToString()) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(CacheManager.InOrderMgr.Err);
                    return;
                }

                ArrayList al = CacheManager.InOrderMgr.QuerySubtbl(fpOrder.ActiveSheet.Cells[row, dicColmSet["组合号"]].Text);
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in al)
                    {
                        if (CacheManager.InOrderMgr.UpdateOrderSortID(order.ID, fpOrder.ActiveSheet.Cells[row, dicColmSet["顺序号"]].Value.ToString()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            MessageBox.Show(CacheManager.InOrderMgr.Err);
                            return;
                        }
                    }
                }
            }
            catch { FS.FrameWork.Management.PublicTrans.RollBack(); ; return; }
            FS.FrameWork.Management.PublicTrans.Commit();

        }

        protected void CheckSortID()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(OrderManagement.Connection);
            //t.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                for (int i = 0; i < 2; i++)
                {
                    int k = 0;
                    for (int j = 0; j < fpOrder.Sheets[i].RowCount; j++)
                    {
                        k = k + 1;
                        if (fpOrder.Sheets[i].Cells[j, dicColmSet["顺序号"]].Value.ToString() != (k).ToString())
                        {
                            if (CacheManager.InOrderMgr.UpdateOrderSortID(fpOrder.Sheets[i].Cells[j, dicColmSet["医嘱流水号"]].Text, (k).ToString()) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                MessageBox.Show(CacheManager.InOrderMgr.Err);
                                return;
                            }
                        }
                    }
                }
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

        }

        /// <summary>
        /// 添加草药医嘱{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        /// <param name="alHerbalOrder"></param>
        public void AddHerbalOrders(ArrayList alHerbalOrder)
        {
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} //草药弹出草药开立界面
            using (FS.HISFC.Components.Order.Controls.ucHerbalOrder uc = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
            {
                uc.IsClinic = false;

                uc.Patient = new FS.HISFC.Models.RADT.PatientInfo();//
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                uc.AlOrder = alHerbalOrder;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                int subCombNo = -1;

                if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order info in uc.AlOrder)
                    {
                        if (subCombNo == -1)
                        {
                            if (info.SubCombNO > 0)
                            {
                                subCombNo = info.SubCombNO;
                            }
                            else
                            {
                                subCombNo = this.GetMaxCombNo(info, this.fpOrder.ActiveSheetIndex);
                            }
                        }

                        info.SubCombNO = subCombNo;

                        info.GetFlag = "0";
                        info.RowNo = -1;
                        info.PageNo = -1;

                        this.AddNewOrder(info, 1);
                    }
                    uc.Clear();
                    this.RefreshCombo();
                }
            }
        }

        /// <summary>
        /// 修改草药{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        public void ModifyHerbal()
        {
            if (this.fpOrder.ActiveSheet.RowCount == 0)
            {
                return;
            }

            ArrayList alModifyHerbal = new ArrayList(); //要修改的草药医嘱

            FS.HISFC.Models.Order.Inpatient.Order orderTemp = this.fpOrder.ActiveSheet.Rows[this.fpOrder.ActiveSheet.ActiveRowIndex].Tag as
                FS.HISFC.Models.Order.Inpatient.Order;

            if (orderTemp == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(orderTemp.Combo.ID))
            {
                alModifyHerbal.Add(orderTemp);
            }
            else
            {
                FS.HISFC.Models.Order.Inpatient.Order order = null;
                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    order = this.fpOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (order == null)
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(order.Combo.ID))
                    {
                        continue;
                    }
                    //{1A93C0BB-30CD-4097-81F8-F074B22A830E}
                    if (order.Item.SysClass.ID.ToString() != "PCC")
                    {
                        continue;
                    }
                    if (order.Status != 0)
                    {
                        continue;
                    }
                    if (order.Combo.ID == orderTemp.Combo.ID)
                    {
                        alModifyHerbal.Add(order);
                    }
                }
            }

            if (alModifyHerbal.Count > 0)
            {
                using (FS.HISFC.Components.Order.Controls.ucHerbalOrder uc = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
                {
                    uc.Patient = new FS.HISFC.Models.RADT.PatientInfo();

                    uc.refreshGroup += new RefreshGroupTree(uc_refreshGroup);

                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "草药医嘱开立";
                    uc.AlOrder = alModifyHerbal;
                    uc.OpenType = FS.HISFC.Components.Order.Controls.EnumOpenType.Modified; //修改
                    uc.IsClinic = false;
                    DialogResult r = FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                    if (uc.IsCancel == true)
                    {
                        //取消了
                        return;
                    }

                    if (uc.OpenType == FS.HISFC.Components.Order.Controls.EnumOpenType.Modified)
                    {
                        //改为新加模式就不删除了
                        if (this.Delete(this.fpOrder.ActiveSheet.ActiveRowIndex, true) < 0)
                        {
                            //删除原医嘱不成功
                            return;
                        }
                    }

                    if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                    {
                        foreach (FS.HISFC.Models.Order.Inpatient.Order info in uc.AlOrder)
                        {
                            this.AddNewOrder(info, this.fpOrder.ActiveSheetIndex);
                        }
                        uc.Clear();
                        this.RefreshCombo();
                    }
                }
            }
            else//{1A93C0BB-30CD-4097-81F8-F074B22A830E}
            {
                MessageBox.Show("请核查，没有草药信息！");
                return;
            }
        }

        public void AddLevelOrders()
        {
            using (FS.HISFC.Components.Order.Controls.ucLevelOrder uc = new FS.HISFC.Components.Order.Controls.ucLevelOrder())
            {
                uc.InOutType = 2;
                uc.Patient = new FS.HISFC.Models.RADT.PatientInfo();

                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "检验检查医嘱开立";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                if (uc.AlOrder != null && uc.AlOrder.Count != 0)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order info in uc.AlOrder)
                    {
                        this.AddNewOrder(info, 1);
                    }
                    //uc.Clear();
                    this.RefreshCombo();

                }
            }
        }

        #region {49026086-DCA3-4af4-A064-58F7479C324A}
        private void uc_refreshGroup()
        {
            this.refreshGroup();
        }
        #endregion


        #region 粘贴医嘱

        /// <summary>
        /// 粘贴医嘱
        /// </summary>
        public void PasteOrder()
        {
            //发现复制历史医嘱 确实有问题，后面在处理吧 
            //注意处理 组合号 方号问题
            try
            {
                List<string> orderIdList = Classes.HistoryOrderClipboard.OrderList;
                if ((orderIdList == null) || (orderIdList.Count <= 0)) return;

                if (FS.HISFC.Components.Order.Classes.HistoryOrderClipboard.Type == ServiceTypes.I)
                {
                    DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                    string err = string.Empty;
                    for (int count = 0; count < orderIdList.Count; count++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(orderIdList[count]);
                        decimal qty = order.Qty;
                        if (order != null)
                        {
                            order.Item.Name.Replace("[嘱托]", "").Replace("[自备]", "");

                            order.Patient = this.myPatientInfo;

                            if (order.Item.ItemType == EnumItemType.Drug)
                            {
                                order.StockDept.ID = string.Empty;
                                if (CacheManager.OrderIntegrate.FillPharmacyItemWithStockDept(ref order, out err) == -1)
                                {
                                    MessageBox.Show(err);
                                    continue;
                                }
                                if (order == null) return;
                            }
                            else if (order.Item.ItemType == EnumItemType.UnDrug)
                            {
                                if (CacheManager.OrderIntegrate.FillFeeItem(ref order, out err, 1) == -1)
                                {
                                    MessageBox.Show(err);
                                    continue;
                                }
                                if (order == null)
                                {
                                    return;
                                }
                            }

                            #region 开立科室、执行科室信息
                            if (this.GetReciptDept() != null)
                            {
                                order.ReciptDept.ID = this.GetReciptDept().ID;
                                order.ReciptDept.Name = this.GetReciptDept().Name;
                            }
                            if (this.GetReciptDoc() != null)
                            {
                                order.ReciptDoctor.ID = this.GetReciptDoc().ID;
                                order.ReciptDoctor.Name = this.GetReciptDoc().Name;
                            }
                            if (this.GetReciptDoc() != null)
                            {
                                order.Oper.ID = this.GetReciptDoc().ID;
                                order.Oper.ID = this.GetReciptDoc().Name;
                            }

                            //手术室开立处理
                            if (!string.IsNullOrEmpty(order.ReciptDept.ID) && string.IsNullOrEmpty(order.ExeDept.ID))
                            {
                                if ("1,2".Contains((SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ReciptDept.ID).SpecialFlag)))
                                {
                                    order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ReciptDept.ID);
                                }
                            }
                            #endregion

                            //医嘱状态重置
                            order.Combo.ID = "";
                            order.Memo = "";
                            order.Status = 0;
                            order.ID = "";
                            order.SortID = 0;
                            order.BeginTime = dtNow;
                            order.EndTime = DateTime.MinValue;
                            order.DCOper.OperTime = DateTime.MinValue;
                            order.DcReason.ID = "";
                            order.DcReason.Name = "";
                            order.DCOper.ID = "";
                            order.DCOper.Name = "";
                            order.ConfirmTime = DateTime.MinValue;
                            order.Nurse.ID = "";
                            order.MOTime = dtNow;

                            order.PageNo = -1;
                            order.RowNo = -1;
                            order.GetFlag = "0";

                            order.ApplyNo = string.Empty;

                            #region  add by liuww 不请空 复制重整医嘱，不算首日量
                            order.ReTidyInfo = "";
                            #endregion

                            order.FirstUseNum = Classes.Function.GetFirstOrderDays(order, dtNow).ToString();

                            //添加到当前类表中按照医嘱类型进行分类
                            if (order.OrderType.IsDecompose)
                            {
                                this.fpOrder.ActiveSheetIndex = 0;
                            }
                            else
                            {
                                this.fpOrder.ActiveSheetIndex = 1;
                            }
                            if (this.fpOrder.ActiveSheetIndex == 0)
                            {
                                #region 部分临嘱不能复制为长期医嘱
                                //add by houwb 2011-4-7
                                bool isCanCopy = false;
                                if (FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(false) != null)
                                {
                                    foreach (FS.FrameWork.Models.NeuObject obj in FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(false))
                                    {
                                        if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == order.Item.SysClass.ID.ToString())
                                        {
                                            isCanCopy = true;
                                        }
                                        else if (obj.ID == order.Item.SysClass.ID.ToString())
                                        {
                                            isCanCopy = true;
                                        }
                                    }
                                }

                                if (!isCanCopy)
                                {
                                    MessageBox.Show(order.Item.Name + "系统类别为" + order.Item.SysClass.Name + "不能复制为长期医嘱!");
                                    continue;
                                }
                                #endregion
                            }

                            order.SubCombNO = this.GetMaxCombNo(order, this.fpOrder.ActiveSheetIndex);

                            this.AddNewOrder(order, this.fpOrder.ActiveSheetIndex);
                        }
                    }
                    this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].ClearSelection();
                    Classes.Function.ShowBalloonTip(3, "提示", "请注意检查执行科室是否正确！", ToolTipIcon.Info);
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("不可以把门诊的医嘱复制为住院医嘱！"));
                    return;
                }
            }
            catch { }
        }

        /// <summary>
        /// 粘贴医嘱
        /// </summary>
        public void PasteOrder(ArrayList alOrders)
        {
            //注意处理 组合号 方号问题
            try
            {
                if ((alOrders == null) || (alOrders.Count <= 0)) return;

                DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                string err = string.Empty;
                for (int count = 0; count < alOrders.Count; count++)
                {
                    FS.HISFC.Models.Order.Inpatient.Order order = alOrders[count] as FS.HISFC.Models.Order.Inpatient.Order;

                    if (order != null)
                    {
                        order.Patient = this.myPatientInfo;

                        if (order.Item.ItemType == EnumItemType.Drug)
                        {
                            order.StockDept.ID = string.Empty;
                            if (CacheManager.OrderIntegrate.FillPharmacyItemWithStockDept(ref order, out err) == -1)
                            {
                                MessageBox.Show(err);
                                continue;
                            }
                            if (order == null) return;
                        }
                        else if (order.Item.ItemType == EnumItemType.UnDrug)
                        {
                            if (CacheManager.OrderIntegrate.FillFeeItem(ref order, out err) == -1)
                            {
                                MessageBox.Show(err);
                                continue;
                            }
                            if (order == null) return;
                        }

                        #region 开立科室、执行科室信息
                        if (this.GetReciptDept() != null)
                        {
                            order.ReciptDept.ID = this.GetReciptDept().ID;
                            order.ReciptDept.Name = this.GetReciptDept().Name;
                        }
                        if (this.GetReciptDoc() != null)
                        {
                            order.ReciptDoctor.ID = this.GetReciptDoc().ID;
                            order.ReciptDoctor.Name = this.GetReciptDoc().Name;
                        }
                        if (this.GetReciptDoc() != null)
                        {
                            order.Oper.ID = this.GetReciptDoc().ID;
                            order.Oper.ID = this.GetReciptDoc().Name;
                        }

                        //手术室开立处理
                        if (!string.IsNullOrEmpty(order.ReciptDept.ID) && string.IsNullOrEmpty(order.ExeDept.ID))
                        {
                            if ("1,2".Contains((SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ReciptDept.ID).SpecialFlag)))
                            {
                                order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ReciptDept.ID);
                            }
                        }
                        #endregion

                        //医嘱状态重置
                        order.Combo.ID = "";

                        order.Memo = "";
                        order.Status = 0;
                        order.ID = "";
                        order.SortID = 0;
                        order.BeginTime = dtNow;
                        order.EndTime = DateTime.MinValue;
                        order.DCOper.OperTime = DateTime.MinValue;
                        order.DcReason.ID = "";
                        order.DcReason.Name = "";
                        order.DCOper.ID = "";
                        order.DCOper.Name = "";
                        order.ConfirmTime = DateTime.MinValue;
                        order.Nurse.ID = "";
                        order.MOTime = dtNow;

                        order.PageNo = -1;
                        order.RowNo = -1;
                        order.GetFlag = "0";

                        order.ApplyNo = string.Empty;

                        #region  add by liuww 不请空 复制重整医嘱，不算首日量
                        order.ReTidyInfo = "";
                        #endregion

                        order.FirstUseNum = Classes.Function.GetFirstOrderDays(order, dtNow).ToString();

                        //添加到当前类表中按照医嘱类型进行分类
                        if (order.OrderType.IsDecompose)
                        {
                            this.fpOrder.ActiveSheetIndex = 0;
                        }
                        else
                        {
                            this.fpOrder.ActiveSheetIndex = 1;
                        }

                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            #region 部分临嘱不能复制为长期医嘱
                            //add by houwb 2011-4-7
                            bool isCanCopy = false;
                            if (FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(false) != null)
                            {
                                foreach (FS.FrameWork.Models.NeuObject obj in FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(false))
                                {
                                    if (obj.ID != order.Item.SysClass.ID.ToString())
                                    {
                                        isCanCopy = true;
                                    }
                                }
                            }

                            if (!isCanCopy)
                            {
                                MessageBox.Show(order.Item.Name + "系统类别为" + order.Item.SysClass.Name + "不能复制为长期医嘱!");
                                continue;
                            }
                            #endregion
                        }

                        if (order.SubCombNO <= 0)
                        {
                            order.SubCombNO = this.GetMaxCombNo(order, this.fpOrder.ActiveSheetIndex);
                        }

                        //order.SortID = this.GetSortIDBySubCombNo(order.SubCombNO);
                        order.SortID = 0;

                        this.AddNewOrder(order, this.fpOrder.ActiveSheetIndex);
                    }
                }
                this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].ClearSelection();
            }
            catch { }
        }
        #endregion

        /// <summary>
        /// 设置首日量和末日量
        /// </summary>
        /// <param name="order">医嘱</param>
        /// <param name="t"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Inpatient.Order SetFirstUseQuanlity(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order == null || order.OrderType.Type != FS.HISFC.Models.Order.EnumType.LONG)
            {
                return order;
            }

            //重整医嘱的 上次、下次分解时间不变
            if (!(order.ReTidyInfo == null ||
                (order.ReTidyInfo != null && !order.ReTidyInfo.Contains("重整医嘱"))))
            {
                return order;
            }

            #region 对时间点进行排序
            string[] execTimes = order.Frequency.Times;
            //Begin对时间点进行排序，按从小到大//如果是已经排序了，就不要了
            for (int i = 0; i < execTimes.Length; i++)
            {
                DateTime dtTempi = FS.FrameWork.Function.NConvert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + execTimes[i]);

                for (int j = i + 1; j < execTimes.Length; j++)
                {
                    DateTime dtTempj = FS.FrameWork.Function.NConvert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + execTimes[j]);
                    if (dtTempj < dtTempi)
                    {
                        string temp = execTimes[i];
                        execTimes[i] = execTimes[j];
                        execTimes[j] = temp;
                    }
                }
            }
            //End
            #endregion

            #region 首日量

            int times = 0;//此处根据传入的次数来判断

            if (string.IsNullOrEmpty(order.FirstUseNum))
            {
                return null;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(order.FirstUseNum) > order.Frequency.Times.Length)
            {
                order.FirstUseNum = order.Frequency.Times.Length.ToString();
                Classes.Function.ShowBalloonTip(2, order.Item.Name + "[" + order.Item.Specs + "] 首日量有误，\r\n系统自动调整为" + order.Frequency.Times.Length.ToString(), "提示！", ToolTipIcon.Info);
            }

            //如果开始时间不是当天，系统默认首日量为0
            string moTime = order.MOTime.ToString("yyyy-MM-dd");
            if (!moTime.Equals(order.BeginTime.ToString("yyyy-MM-dd")) && !"0".Equals(order.FirstUseNum))
            {
                order.FirstUseNum = "0";
                Classes.Function.ShowBalloonTip(2, order.Item.Name + "的开始时间[" + order.BeginTime.ToString("yyyy-MM-dd") + "] 不是当天，\r\n系统自动调整首日量为0", "提示！", ToolTipIcon.Info);
            }

            if (Int32.TryParse(order.FirstUseNum, out times) && times >= 0
                && order.Frequency.Times.Length > 0)
            {
                //补首日量
                //如果是零、将开始时间置为第二天的零点
                if (times == 0)
                {
                    order.NextMOTime = FS.FrameWork.Function.NConvert.ToDateTime(order.BeginTime.AddDays(1).ToString("yyyy-MM-dd"));
                }
                else
                {
                    if (times == execTimes.Length)
                    {
                        order.NextMOTime = FS.FrameWork.Function.NConvert.ToDateTime(order.BeginTime.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        //根据次数来确定医嘱开始时间
                        order.NextMOTime = FS.FrameWork.Function.NConvert.ToDateTime(order.BeginTime.ToString("yyyy-MM-dd") + " " + execTimes[execTimes.Length - times]);
                    }
                }

                order.CurMOTime = order.NextMOTime;
            }
            #endregion

            return order;
        }

        #endregion

        #region 事件

        #region 项目变化

        /// <summary>
        /// 医嘱变化函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        protected virtual void ucItemSelect1_OrderChanged(FS.HISFC.Models.Order.Inpatient.Order sender, string changedField)
        {
            dirty = true;
            if (!this.EditGroup && !this.bIsDesignMode)
            {
                return;
            }

            try
            {
                if (!this.EditGroup)//{E679E3A6-9948-41a8-B390-DD9A57347681}判断不是开立医嘱模式就不走下面接口
                {
                    #region 根据接口实现对医嘱信息进行补充判断

                    if (this.IAlterOrderInstance != null)
                    {
                        if (this.IAlterOrderInstance.AlterOrder(this.myPatientInfo, this.myReciptDoc, this.myReciptDept, ref sender) == -1)
                        {
                            dirty = false;
                            return;
                        }
                    }

                    #endregion
                }

                #region 新增

                if (this.ucItemSelect1.OperatorType == Operator.Add)
                {
                    this.AddNewOrder(sender, this.fpOrder.ActiveSheetIndex);
                    this.fpOrder.ActiveSheet.ClearSelection();
                    //this.fpOrder.ActiveSheet.AddSelection(this.fpOrder.ActiveSheet.RowCount - 1, 0, 1, 1);
                    //this.fpOrder.ActiveSheet.ActiveRowIndex = this.fpOrder.ActiveSheet.RowCount - 1;
                    if (this.ActiveRowIndex >= 0)
                    {
                        this.fpOrder.ActiveSheet.ActiveRowIndex = this.ActiveRowIndex;
                        this.ucItemSelect1.CurrentRow = this.ActiveRowIndex;
                        //this.ucItemSelect1.Order = this.currentOrder;
                        this.fpOrder.ActiveSheet.AddSelection(this.ActiveRowIndex, 0, 1, 1);
                    }

                    //开立术前医嘱，全停长嘱
                    if (!this.EditGroup)
                    {
                        //包含“术前医嘱”的描述医嘱
                        if (!sender.OrderType.isCharge && sender.Item.Name.IndexOf("术前医嘱") >= 0)
                        {
                            this.DcAllLongOrder("");
                        }
                    }

                    ShowPactItem();
                }
                #endregion

                #region 删除
                else if (this.ucItemSelect1.OperatorType == Operator.Delete)
                {

                }
                #endregion

                #region 修改
                else if (this.ucItemSelect1.OperatorType == Operator.Modify)
                {
                    ArrayList alRows = GetSelectedRows();
                    //修改
                    if (alRows.Count > 1)
                    {
                        for (int i = 0; i < alRows.Count; i++)
                        {
                            if (this.ucItemSelect1.CurrentRow == System.Convert.ToInt32(alRows[i]))
                            {
                                GetOrderChanged(int.Parse(alRows[i].ToString()), sender, changedField);
                                //this.AddObjectToFarpoint(sender, this.ucItemSelect1.CurrentRow, this.fpOrder.ActiveSheetIndex, changedField);
                            }
                            else
                            {
                                FS.HISFC.Models.Order.Inpatient.Order order = this.GetObjectFromFarPoint(int.Parse(alRows[i].ToString()), this.fpOrder.ActiveSheetIndex);
                                if (order.Combo.ID == sender.Combo.ID)
                                {
                                    if (changedField == ColmSet.ALL
                                        || changedField == ColmSet.Z组号
                                        || changedField == ColmSet.P频次
                                        || changedField == ColmSet.Y用法
                                        || changedField == ColmSet.F付数
                                        || changedField == ColmSet.K开始时间
                                        || changedField == ColmSet.T停止时间
                                        || changedField == ColmSet.S首日量)
                                    {
                                        //组合的一起修改
                                        if (order.Item.SysClass.ID.ToString() != "PCC")
                                        {
                                            order.Usage = sender.Usage;
                                        }
                                        order.FirstUseNum = sender.FirstUseNum;
                                        order.Frequency.ID = sender.Frequency.ID;
                                        order.Frequency.Name = sender.Frequency.Name;
                                        order.Frequency.Time = sender.Frequency.Time;
                                        order.Frequency.Usage = sender.Frequency.Usage;
                                        order.BeginTime = sender.BeginTime;
                                        order.EndTime = sender.EndTime;
                                        order.HerbalQty = sender.HerbalQty;
                                        Classes.Function.ReComputeQty(order);

                                        GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                        //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.fpOrder.ActiveSheetIndex, ColmSet.ALL);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.GetOrderChanged(ucItemSelect1.CurrentRow, sender, changedField);
                    }
                    //开立术前医嘱，全停长嘱
                    if (!this.EditGroup)
                    {
                        if (!sender.OrderType.isCharge && sender.Item.Name.IndexOf("术前医嘱") >= 0)
                        {
                            this.DcAllLongOrder("");
                        }
                    }


                    //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    if (changedField == ColmSet.Z组号)
                    {
                        #region 组合相同的一起选择
                        //设置组合行选择
                        if (this.ucItemSelect1.Order.Combo.ID != "" && this.ucItemSelect1.Order.Combo.ID != null)
                        {
                            this.fpOrder.ActiveSheet.ClearSelection();
                            this.fpOrder.ActiveSheet.ActiveRowIndex = this.ActiveRowIndex;
                            this.ucItemSelect1.CurrentRow = this.ActiveRowIndex;
                            this.fpOrder.ActiveSheet.AddSelection(this.ActiveRowIndex, 0, 1, 1);

                            for (int k = 0; k < this.fpOrder.ActiveSheet.Rows.Count; k++)
                            {
                                string strComboNo = this.GetObjectFromFarPoint(k, this.fpOrder.ActiveSheetIndex).Combo.ID;
                                if (this.ucItemSelect1.Order.Combo.ID == strComboNo)
                                {
                                    this.fpOrder.ActiveSheet.AddSelection(k, 0, 1, 1);
                                }
                            }
                        }
                        #endregion
                    }

                    RefreshOrderState(-1);
                    this.RefreshCombo();

                    if (changedField == ColmSet.Y用法)
                    {
                        ShowPactItem();
                    }
                }
                #endregion

                this.fpOrder.ShowRow(0, this.fpOrder.ActiveSheet.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dirty = false;
                return;
            }

            dirty = false;

            this.isEdit = true;
        }

        /// <summary>
        /// 修改单个项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        private void GetOrderChanged(int rowIndex, FS.HISFC.Models.Order.Inpatient.Order inOrder, string changedField)
        {
            if (changedField == "!")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["!"]].Text = inOrder.Note;
            }
            else if (changedField == "期效")
            {
                if (inOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["期效"]].Text = "长期";
                }
                else if (inOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["期效"]].Text = "临时";     //0
                }
            }
            else if (changedField == "医嘱类型")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["医嘱类型"]].Text = inOrder.OrderType.Name;    //0
            }
            else if (changedField == "医嘱流水号")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["医嘱流水号"]].Text = inOrder.ID;
            }
            else if (changedField == "医嘱状态")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["医嘱状态"]].Text = inOrder.Status.ToString();
            }
            else if (changedField == "组合号")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["组合号"]].Text = inOrder.Combo.ID;
            }
            else if (changedField == "主药")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["主药"]].Text = FS.FrameWork.Function.NConvert.ToInt32(inOrder.Combo.IsMainDrug).ToString();
            }
            else if (changedField == "组号")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["首日量"]].Text = inOrder.FirstUseNum;

                if (inOrder.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["付数"]].Text = inOrder.HerbalQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["付数"]].Text = "";
                }

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["频次"]].Text = inOrder.Frequency.ID.ToString();
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["频次名称"]].Text = inOrder.Frequency.Name;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量单位"]].Text = inOrder.Unit;
                if (inOrder.Item.ItemType != EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.Unit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.DoseUnit;
                }

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["开始时间"]].Text = inOrder.BeginTime.ToString();

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["组号"]].Text = inOrder.SubCombNO.ToString();
                if (inOrder.SortID <= 0)
                {
                    inOrder.SortID = this.GetSortIDBySubCombNo(inOrder.SubCombNO);
                }
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["顺序号"]].Text = inOrder.SortID.ToString();
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["组合号"]].Text = inOrder.Combo.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["用法"]].Text = inOrder.Usage.Name;

                #region add by zhaorong at 2013-8-29 start 医嘱的用法与执行科室均在维护的常数队列中时，默认执行科室为：6030（静脉配置中心药房）
                this.ChangeStockDept(ref inOrder);
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["取药药房编码"]].Text = inOrder.StockDept.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["取药药房"]].Text = inOrder.StockDept.Name;
                #endregion

                if (inOrder.EndTime > DateTime.MinValue)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["停止时间"]].Text = inOrder.DCOper.OperTime.ToString();
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["结束时间"]].Text = inOrder.EndTime.ToString();
                }
            }
            else if (changedField == "开立时间")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["开立时间"]].Text = inOrder.MOTime.ToString();
            }
            else if (changedField == "开立医生")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["开立医生"]].Text = inOrder.ReciptDoctor.Name;
            }
            else if (changedField == "顺序号")
            {
                if (inOrder.SortID <= 0)
                {
                    inOrder.SortID = this.GetSortIDBySubCombNo(inOrder.SubCombNO);
                }
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["顺序号"]].Text = inOrder.SortID.ToString();
            }
            else if (changedField == "医嘱名称")
            {
                this.AddObjectToFarpoint(inOrder, rowIndex, fpOrder.ActiveSheetIndex, "新增");
            }
            else if (changedField == "组")
            {

            }
            else if (changedField == "首日量")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["首日量"]].Text = inOrder.FirstUseNum;
            }
            else if (changedField == "每次用量" || changedField == "单位")
            {
                if (inOrder.Item.ItemType == EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = FS.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce);
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.DoseUnit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.Unit;
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty);
                }

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量单位"]].Text = inOrder.Unit;

            }
            else if (changedField == "频次" || changedField == "频次名称")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["频次"]].Text = inOrder.Frequency.ID.ToString();
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["频次名称"]].Text = inOrder.Frequency.Name;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量单位"]].Text = inOrder.Unit;
                if (inOrder.Item.ItemType != EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.Unit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.DoseUnit;
                }
            }
            else if (changedField == "用法编码" || changedField == "用法")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["用法"]].Text = inOrder.Usage.Name;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["用法编码"]].Text = inOrder.Usage.ID;

                #region add by zhaorong at 2013-8-29 start 医嘱的用法与执行科室均在维护的常数队列中时，默认执行科室为：6030（静脉配置中心药房）
                this.ChangeStockDept(ref inOrder);
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["取药药房编码"]].Text = inOrder.StockDept.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["取药药房"]].Text = inOrder.StockDept.Name;
                #endregion
            }
            else if (changedField == "总量" || changedField == "总量单位")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量单位"]].Text = inOrder.Unit;
                if (inOrder.Item.ItemType != EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.Unit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.DoseUnit;
                }
            }
            else if (changedField == "付数")
            {
                if (inOrder.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["付数"]].Text = inOrder.HerbalQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["付数"]].Text = "";
                }

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["总量单位"]].Text = inOrder.Unit;

                if (inOrder.Item.ItemType != EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.Unit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["每次用量"]].Text = inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["单位"]].Text = inOrder.DoseUnit;
                }
            }
            else if (changedField == "系统类别")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["系统类别"]].Text = inOrder.Item.SysClass.Name;
            }
            else if (changedField == "开始时间")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["开始时间"]].Text = inOrder.BeginTime.ToString();
            }
            else if (changedField == "停止时间")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["停止时间"]].Text = inOrder.DCOper.OperTime.ToString();
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["结束时间"]].Text = inOrder.EndTime.ToString();
            }
            else if (changedField == "执行科室编码" || changedField == "执行科室")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["执行科室编码"]].Text = inOrder.ExeDept.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["执行科室"]].Text = inOrder.ExeDept.Name;
            }
            else if (changedField == "急")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["急"]].Value = inOrder.IsEmergency;
            }
            else if (changedField == "检查部位")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["检查部位"]].Text = inOrder.CheckPartRecord;
            }
            else if (changedField == "样本类型")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["样本类型"]].Text = inOrder.Sample.Name;
            }
            else if (changedField == "取药药房编码" || changedField == "取药药房")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["取药药房编码"]].Text = inOrder.StockDept.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["取药药房"]].Text = inOrder.StockDept.Name;
            }
            else if (changedField == "备注")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["备注"]].Text = inOrder.Memo;
            }
            else if (changedField == "录入人编码" || changedField == "录入人")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["录入人编码"]].Text = inOrder.Oper.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["录入人"]].Text = inOrder.Oper.Name;
            }
            else if (changedField == "开立科室")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["开立科室"]].Text = inOrder.ReciptDept.Name;
            }
            else if (changedField == "停止人编码" || changedField == "停止人")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["停止人编码"]].Text = inOrder.DCOper.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["停止人"]].Text = inOrder.DCOper.Name;
            }
            else if (changedField == "新增")
            {
                this.AddObjectToFarpoint(inOrder, rowIndex, fpOrder.ActiveSheetIndex, "新增");
            }
            else if (changedField == "滴速")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["滴速"]].Text = inOrder.Dripspreed;

            }
            else if (changedField == "国家医保代码")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["国家医保代码"]].Text = inOrder.CountryCode;

            }
            this.fpOrder.ActiveSheet.Rows[rowIndex].Tag = inOrder;
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
                int i = dicColmSet["医嘱状态"];//this.GetColumnIndexFromName("医嘱状态");
                int state = int.Parse(this.fpOrder.Sheets[SheetIndex].Cells[row, i].Text);

                //if (GetObjectFromFarPoint(row, SheetIndex).ID != "" && reset)
                //{
                //    state = CacheManager.InOrderMgr.QueryOneOrderState(GetObjectFromFarPoint(row, SheetIndex).ID);
                //    this.fpOrder.Sheets[SheetIndex].Cells[row, i].Value = state;
                //}

                //处理加急显示
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpOrder.Sheets[SheetIndex].Cells[row, this.dicColmSet["急"]].Value))
                {
                    fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].Label = "急";
                }
                else
                {
                    fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].Label = "";
                }

                //重整医嘱按照停止颜色显示
                if (state != 3 && state != 4
                    && this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["停止时间"]].Text != "")
                {
                    //紫色
                    this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(132, 72, 168);
                }
                else
                {
                    switch (state)
                    {
                        case 0: //新开立　绿色
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(128, 255, 128);
                            break;
                        case 1://审核 浅蓝色
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(106, 174, 242);
                            break;
                        case 2://执行　浅黄色
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(243, 230, 105);
                            break;
                        case 3://停止　浅红色
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(248, 120, 222);
                            break;
                        case 4://预停止 浅红色
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(248, 120, 222);
                            break;
                        default: //需审核医嘱 黑色
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.Black;
                            break;
                    }
                }

                if (this.IsDesignMode)
                {
                    this.GetObjectFromFarPoint(row, SheetIndex).Status = state;
                }
            }
            catch { }
        }

        /// <summary>
        /// 选择医嘱修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpOrder_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            SelectionChanged();

            ShowPactItem();
        }

        /// <summary>
        /// 显示项目信息
        /// </summary>
        /// <returns></returns>
        private int ShowPactItem()
        {
            try
            {
                if (fpOrder.ActiveSheet.ActiveRowIndex < 0
                    || fpOrder.ActiveSheet.RowCount == 0)
                {
                    return 1;
                }

                #region 显示项目信息

                FS.HISFC.Models.Order.Inpatient.Order inOrder = GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex);
                if (inOrder == null)
                {
                    return -1;
                }
                this.pnItemInfo.Visible = true;
                txtItemInfo.ReadOnly = true;

                string showInfo = "";

                //项目信息
                if (inOrder.Item.ID == "999")
                {
                    showInfo += inOrder.Item.Name + " 【规格】" + inOrder.Item.Specs + " 【单价】" + inOrder.Item.Price.ToString() + "元/" + inOrder.Item.PriceUnit;
                }
                else
                {
                    if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).UserCode + " " + inOrder.Item.Name + " 【规格】" + inOrder.Item.Specs + " 【单价】" + inOrder.Item.Price.ToString() + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).PackUnit;
                        
                        if (!string.IsNullOrEmpty(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).Product.Manual))
                        {
                            showInfo += "\r\n" + "【药品说明】" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).Product.Manual;
                        }
                    }
                    else
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Item.ID).UserCode + " " + inOrder.Item.Name + " 【规格】" + inOrder.Item.Specs + " 【单价】" + inOrder.Item.Price.ToString() + "元/" + inOrder.Item.PriceUnit;
                    }

                    
                }
                if (inOrder.Item.ID != "999")
                {
                    #region 项目扩展信息提示

                    string itemShowInfo = "";

                    if (myPatientInfo != null && !string.IsNullOrEmpty(myPatientInfo.ID))
                    {
                        FS.HISFC.Models.SIInterface.Compare compare = Classes.Function.GetPactItem(inOrder);
                        inOrder.Patient.Pact = this.myPatientInfo.Pact;
                        if (compare != null)
                        {
                            //医保对照信息
                            itemShowInfo += "\r\n【" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(inOrder.Patient.Pact.ID).Name + "】 " + Classes.Function.GetItemGrade(compare.CenterItem.ItemGrade) + " " + (compare.CenterItem.Rate > 0 ? compare.CenterItem.Rate.ToString("p0") : "") + (compare.CenterFlag == "1" ? "【需审批】" : "");


                            //医保限制用药信息
                            if (!string.IsNullOrEmpty(compare.Practicablesymptomdepiction))
                            {
                                itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\r\n" : " ") + compare.Practicablesymptomdepiction;
                            }
                        }
                    }

                    //基本药物提示
                    string ss = Classes.Function.GetPhaEssentialDrugs(inOrder.Item);
                    if (!string.IsNullOrEmpty(ss))
                    {
                        itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\r\n" : " ") + "[" + ss + "]";
                    }

                    //肿瘤用药提示
                    ss = Classes.Function.GetPhaForTumor(inOrder.Item);
                    if (!string.IsNullOrEmpty(ss))
                    {
                        itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\r\n" : " ") + "[" + ss + "]";
                    }

                    //项目内涵 暂无

                    showInfo += itemShowInfo;

                    #endregion

                    //套餐明细
                    if (inOrder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Item.ID);
                        if (undrug.UnitFlag == "1")
                        {
                            showInfo += "\r\n【套餐包含】：";

                            ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(inOrder.Item.ID);
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb comb in alZt)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(comb.ID);

                                //{BC67FD5E-77CE-410f-B642-518D7420BF93}
                                //当收费项目设置为医生不可以开立的时候，就无法查询到undrug信息，搜索一添加没查出来时候二次查询
                                if (combUndrug == null)
                                {
                                    FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                                    combUndrug = itemMgr.GetUndrugByCode(comb.ID);
                                }

                                showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "]") + " " + comb.Qty + combUndrug.PriceUnit + "；";
                            }
                        }
                    }


                    //附材信息
                    FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();
                    ArrayList alSub = subMgr.GetSubtblInfoByItem("1", inOrder.ReciptDept.ID, inOrder.Item.ID, inOrder.Usage.ID);
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
                FS.SOC.HISFC.Fee.Models.Undrug undrugInfo = new FS.SOC.HISFC.Fee.BizLogic.Undrug().GetUndrug(inOrder.Item.ID);

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
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        private void SelectionChanged()
        {
            //每次选择变化前清空数据显示 Add By liangjz 2005-08
            this.ucItemSelect1.Clear(false);

            if (this.fpOrder.ActiveSheet.RowCount <= 0)
            {
                return;
            }

            if (!this.IsDesignMode && !this.EditGroup)
            {
                return;
            }

            //新开立 才能更改
            if (int.Parse(this.fpOrder.ActiveSheet.Cells[this.fpOrder.ActiveSheet.ActiveRowIndex, dicColmSet["医嘱状态"]].Text) == 0)
            {
                if (this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex).GetFlag == "0")
                {
                    #region
                    //设置为当前行
                    this.ucItemSelect1.CurrentRow = this.fpOrder.ActiveSheet.ActiveRowIndex;
                    this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex);

                    this.ucItemSelect1.Order = this.currentOrder;

                    //设置组合行选择
                    if (this.ucItemSelect1.Order.Combo.ID != "" && this.ucItemSelect1.Order.Combo.ID != null)
                    {
                        int comboNum = 0;//获得当前选择行数
                        for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
                        {
                            string strComboNo = this.GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex).Combo.ID;
                            if (this.ucItemSelect1.Order.Combo.ID == strComboNo && i != this.fpOrder.ActiveSheet.ActiveRowIndex)
                            {
                                this.fpOrder.ActiveSheet.AddSelection(i, 0, 1, 1);
                                comboNum++;
                            }
                        }
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
                        this.OrderCanSetCheckChanged(true);//打印检查申请单失效
                    }

                    #endregion
                }
                else
                {
                    Classes.Function.ShowBalloonTip(2, "提示", "项目【" + this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex).Item.Name + "】已经打印，不允许修改！", ToolTipIcon.Info);
                }
            }
            else
            {
                this.ActiveRowIndex = -1;
            }
        }

        private void fpOrder_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {

        }

        #endregion

        #region 属性
        public FS.HISFC.Models.Order.Inpatient.Order SelectedOrder { get { return this.GetObjectFromFarPoint(fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex); } }
        /// <summary>
        /// 是否开立模式
        /// </summary>
        protected bool bIsDesignMode = false;

        /// <summary>
        /// 是否显示右键菜单
        /// </summary>
        protected bool bIsShowPopMenu = true;

        /// <summary>
        /// 是否显示右键菜单
        /// </summary>
        public bool IsShowPopMenu
        {
            get
            {
                return this.bIsShowPopMenu;
            }
            set
            {
                this.bIsShowPopMenu = value;
            }
        }

        /// <summary>
        /// 是否开立模式
        /// </summary>
        [DefaultValue(false), Browsable(false)]
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

                    SetFP();
                    this.QueryOrder();
                }
            }
        }

        private void SetFP()
        {
            this.ucItemSelect1.Visible = this.bIsDesignMode;
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public void SetPatient(FS.HISFC.Models.RADT.PatientInfo value)
        {
            if (!EditGroup)//点击2次组套管理按钮 开立按钮无响应
            {
                this.isShowOrderFinished = false;

                if (myPatientInfo != null && value != null && myPatientInfo.ID != value.ID)
                {
                    this.PassRefresh();
                }

                this.myPatientInfo = value;
                this.ucItemSelect1.PatientInfo = value;

                if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
                {
                    IReasonableMedicine.StationType = FS.HISFC.Models.Base.ServiceTypes.I;
                    IReasonableMedicine.PassSetPatientInfo(myPatientInfo, this.GetReciptDoc());
                }

                this.QueryOrder();

                this.isShowOrderFinished = true;
            }
        }








        /// <summary>
        /// 默认长期医嘱
        /// </summary>
        protected FS.HISFC.Models.Order.EnumType myOrderType = FS.HISFC.Models.Order.EnumType.LONG;

        /// <summary>
        /// 设置长期医嘱类型
        /// </summary>
        [DefaultValue(FS.HISFC.Models.Order.EnumType.LONG)]
        public FS.HISFC.Models.Order.EnumType OrderType
        {
            get
            {
                return this.myOrderType;
            }
            set
            {
                try
                {
                    this.myOrderType = value;
                    if (this.myOrderType == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        this.ucItemSelect1.LongOrShort = 0;
                    }
                    else
                    {
                        this.ucItemSelect1.LongOrShort = 1;
                    }
                }
                catch { }
            }
        }

        protected FS.FrameWork.Models.NeuObject myReciptDept = null;

        /// <summary>
        /// 当前开立科室
        /// </summary>
        [DefaultValue(null)]
        public void SetReciptDept(FS.FrameWork.Models.NeuObject value)
        {

            this.myReciptDept = value;

        }

        /// <summary>
        /// 开立科室
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.myReciptDept == null)
                {
                    myReciptDept = new FS.FrameWork.Models.NeuObject();
                    this.myReciptDept.ID = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoc()).Dept.ID; //开立科室
                    this.myReciptDept.Name = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoc()).Dept.Name;
                }
            }
            catch { }
            return this.myReciptDept;
        }

        protected FS.FrameWork.Models.NeuObject myReciptDoc = null;

        /// <summary>
        /// 当前开立医生
        /// </summary>
        public void SetReciptDoc(FS.FrameWork.Models.NeuObject value)
        {
            this.myReciptDoc = value;
        }

        /// <summary>
        /// 获取开立医生
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDoc()
        {
            try
            {
                if (this.myReciptDoc == null)
                {
                    myReciptDoc = new FS.FrameWork.Models.NeuObject();
                    myReciptDoc = CacheManager.InOrderMgr.Operator.Clone();
                }
            }
            catch { }
            return this.myReciptDoc;
        }

        /// <summary>
        /// 是否修改过医嘱
        /// </summary>
        private bool isEdit = false;

        /// <summary>
        /// 是否
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.isEdit;
            }
        }

        private bool bIsShowIndex = false;

        /// <summary>
        /// 显示index
        /// </summary>
        public bool IsShowIndex
        {
            set
            {
                bIsShowIndex = value;
            }
        }

        #region {2A5F9B85-CA08-4476-A5A4-56F34F0C28AC}

        /// <summary>
        /// 是否护士开立
        /// </summary>
        private bool isNurseCreate = false;

        /// <summary>
        /// 是否护士开立
        /// </summary>
        [DefaultValue(false)]
        public bool IsNurseCreate
        {
            set
            {
                this.isNurseCreate = value;
            }
        }
        #endregion

        #endregion

        #region 函数

        /// <summary>
        /// 添加实体toTable
        /// </summary>
        /// <param name="list"></param>
        private void AddObjectsToTable(ArrayList list)
        {
            if (dsAllLong != null)//条件添加BY zuowy 2005-9-15
                dsAllLong.Tables[0].Clear();//原来没有条件
            if (dsAllShort != null)//条件添加BY zuowy 2005-9-15
                dsAllShort.Tables[0].Clear();//原来没有条件
            foreach (object obj in list)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    //长期医嘱

                    dsAllLong.Tables[0].Rows.Add(AddObjectToRow(order, dsAllLong.Tables[0]));
                }
                else
                {
                    //临时医嘱
                    dsAllShort.Tables[0].Rows.Add(AddObjectToRow(order, dsAllShort.Tables[0]));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataRow AddObjectToRow(object obj, DataTable table)
        {
            DataRow row = table.NewRow();
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            try
            {
                order = obj as FS.HISFC.Models.Order.Inpatient.Order;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            if (order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                FS.HISFC.Models.Pharmacy.Item objItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                row["主药"] = FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug);//5
                row["每次用量"] = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');//9
                row["单位"] = objItem.DoseUnit;//0415 2307096 wang renyi
            }
            else if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                row["每次用量"] = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//9
                row["单位"] = order.Unit;
            }

            if (order.Item.SysClass.ID.ToString() == "PCC")
            {
                row["付数"] = order.HerbalQty;//11
            }
            else
            {
                row["付数"] = "";
            }

            if (order.Note != "")
            {
                row["!"] = order.Note;
            }
            row["期效"] = FS.FrameWork.Function.NConvert.ToInt32(order.OrderType.ID);     //0
            row["医嘱类型"] = order.OrderType.Name;//1
            row["医嘱流水号"] = order.ID;//2
            row["医嘱状态"] = order.Status;//新开立，审核，执行
            row["组合号"] = order.Combo.ID;//4

            row["系统类别"] = order.Item.SysClass.Name;

            row["医嘱名称"] = this.ShowOrderName(order);

            //医保用药-知情同意书
            if (order.IsPermission)
                row["医嘱名称"] = "【√】" + row["医嘱名称"];

            ValidNewOrder(order);
            row["首日量"] = order.FirstUseNum;
            row["总量"] = order.Qty;//7
            row["总量单位"] = order.Unit;//8
            row["频次编码"] = order.Frequency.ID;
            row["频次名称"] = order.Frequency.Name;
            row["用法编码"] = order.Usage.ID;
            row["用法名称"] = order.Usage.Name;//15
            row["开始时间"] = order.BeginTime;
            row["执行科室编码"] = order.ExeDept.ID;
            //if(order.ExeDept.Name == "" && order.ExeDept.ID !="" ) order.ExeDept.Name = this.GetDeptName(order.ExeDept);
            row["执行科室"] = order.ExeDept.Name;
            row["加急"] = order.IsEmergency;
            row["检查部位"] = order.CheckPartRecord;
            row["样本类型"] = order.Sample;
            row["扣库科室编码"] = order.StockDept.ID;
            row["扣库科室"] = order.StockDept.Name;

            row["备注"] = order.Memo;//20
            row["录入人编码"] = order.Oper.ID;
            row["录入人"] = order.Oper.Name;
            row["开立医生"] = order.ReciptDoctor.Name;
            row["开立科室"] = order.ReciptDept.Name;
            row["开立时间"] = order.MOTime;
            row["组号"] = order.SubCombNO.ToString();

            if (order.EndTime != DateTime.MinValue)
            {
                row["停止时间"] = order.DCOper.OperTime;//25
                row["结束时间"] = order.EndTime;//25
            }

            row["停止人编码"] = order.DCOper.ID;
            row["停止人"] = order.DCOper.Name;

            row["滴速"] = order.Dripspreed;

            row["国家医保代码"] = order.CountryCode;

            row["顺序号"] = order.SortID;//28

            #region {1AF0EB93-27A8-462f-9A1E-E1A3ECA54ADE} 将医嘱放入哈希表，提高速度
            if (!this.htOrder.ContainsKey(order.ID))
            {
                this.htOrder.Add(order.ID, order);
            }
            #endregion

            return row;
        }

        /// <summary>
        /// 开立时添加
        /// </summary>
        /// <param name="al"></param>
        private void AddObjectsToFarpoint(ArrayList al)
        {
            if (al == null)
            {
                return;
            }
            DateTime dtNow;
            try
            {
                dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
            }
            catch
            {
                dtNow = System.DateTime.Now;
            }


            int j = 0;
            int k = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order orderObj = al[i] as FS.HISFC.Models.Order.Inpatient.Order;

                if (orderObj.IsSubtbl)
                {
                    continue;
                }

                if (orderObj.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    //长期医嘱
                    this.fpOrder.Sheets[0].Rows.Add(j, 1);
                    this.AddObjectToFarpoint(orderObj, j, 0, ColmSet.ALL);

                    j++;
                }
                else
                {
                    this.fpOrder.Sheets[1].Rows.Add(k, 1);
                    this.AddObjectToFarpoint(orderObj, k, 1, ColmSet.ALL);

                    k++;
                }
            }
        }

        /// <summary>
        /// 显示医嘱名称列
        /// </summary>
        /// <param name="inOrder"></param>
        /// <returns></returns>
        private string ShowOrderName(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            string strShowName = inOrder.Item.Name;

            string price = "";

            if (inOrder.Item.ID == "999" || !inOrder.OrderType.IsCharge)
            {
                price = "[" + "0元/" + inOrder.Item.PriceUnit + "]";
            }
            else
            {
                if (inOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (inOrder.Unit == SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).PackUnit)
                    {
                        price = "[" + inOrder.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).PriceUnit + "]";
                    }
                    else
                    {
                        price = "[" + (inOrder.Item.Price / ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).MinUnit + "]";
                    }
                }
                else
                {
                    if (inOrder.Item.Price > 0)
                    {
                        price = "[" + inOrder.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + ((FS.HISFC.Models.Fee.Item.Undrug)inOrder.Item).PriceUnit.Trim() + "]";
                    }
                }
            }

            //自备、嘱托标记  用于护士打印单据和医嘱单显示区分
            string byoStr = "";

            if (!inOrder.OrderType.IsCharge || inOrder.Item.ID == "999")
            {
                if (!strShowName.Contains("自备")
                    && !strShowName.Contains("嘱托"))
                {
                    if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        byoStr = "[自备]";
                    }
                    else
                    {
                        byoStr = "[嘱托]";
                    }
                }
            }

            strShowName = byoStr + strShowName;

            //皮试显示
            strShowName += CacheManager.InOrderMgr.TransHypotest(inOrder.HypoTest);

            //医嘱名称 
            if (inOrder.Item.Specs == null || inOrder.Item.Specs.Trim() == "")
            {
                return strShowName + (inOrder.IsPermission ? "【√】" : "") + price;
            }
            else
            {
                return strShowName + (inOrder.IsPermission ? "【√】" : "") + "[" + inOrder.Item.Specs + "]" + price;
            }
        }

        /// <summary>
        /// 医嘱修改更新
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row">行</param>
        /// <param name="SheetIndex"></param>
        /// <param name="orderlist"></param>
        private void AddObjectToFarpoint(object obj, int row, int SheetIndex, string orderlist)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            try
            {
                order = ((FS.HISFC.Models.Order.Inpatient.Order)obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clone出错！" + ex.Message);
                return;
            }

            try
            {
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["组号"]].Text = order.SubCombNO.ToString();

                if (orderlist == ColmSet.ALL)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["!"]].Text = order.Note;
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["!"]].Note = order.Note;
                }
                //{D18CDB1B-BB1E-422d-9161-65D9CEC79C05}
                string orderitemname = ShowOrderName(order);
                if (orderitemname.Contains("新生儿") && (DateTime.Now.Date.Subtract(this.Patient.Birthday.Date).Days + 1) > 365 * 16)//非新生儿
                {
                    if (MessageBox.Show("新生儿项目应该开在BB身上,不符合规范", "是否确认要这样操作？", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        this.fpOrder.Sheets[SheetIndex].Rows[row].Remove();
                        return;
                    }
                }
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    //药品
                    FS.HISFC.Models.Pharmacy.Item objItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                    if (orderlist == ColmSet.ALL || orderlist == ColmSet.M每次用量 || orderlist == ColmSet.D单位)
                    {
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["每次用量"]].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');//9
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["单位"]].Text = order.DoseUnit;
                    }
                    if (orderlist == ColmSet.ALL || orderlist == ColmSet.F付数)
                    {
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["付数"]].Text = order.HerbalQty.ToString();
                        }
                        else
                        {
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["付数"]].Text = "";
                        }
                    }

                    if (order.OrderType.IsDecompose)
                    {
                        if (orderlist == ColmSet.ALL || orderlist == ColmSet.Z总量 || orderlist == ColmSet.Z总量单位)
                        {
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["总量"]].Text = order.DoseOnce.ToString();//7
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["总量单位"]].Text = order.DoseUnit;//8
                        }
                    }
                    else //临时
                    {
                        if (orderlist == ColmSet.ALL || orderlist == ColmSet.Z总量 || orderlist == ColmSet.Z总量单位)
                        {
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["总量"]].Text = FS.FrameWork.Public.String.ToSimpleString(order.Qty);//7
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["总量单位"]].Text = order.Unit;//8
                        }
                    }
                }
                else if (order.Item.ItemType == EnumItemType.UnDrug)
                {
                    //非药品
                    if (orderlist == ColmSet.ALL || orderlist == ColmSet.Z总量 || orderlist == ColmSet.Z总量单位)
                    {
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["每次用量"]].Text = FS.FrameWork.Public.String.ToSimpleString(order.Qty);//9
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["总量"]].Text = FS.FrameWork.Public.String.ToSimpleString(order.Qty);
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["单位"]].Text = order.Unit;//剂量单位
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["总量单位"]].Text = order.Unit;//8
                    }
                }

                this.ValidNewOrder(order); //填写信息

                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["期效"]].Text = "长期";

                }
                else if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["期效"]].Text = "临时";     //0
                }

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["主药"]].Text = FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug).ToString();//5

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["医嘱类型"]].Text = order.OrderType.Name; //1 名称

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["医嘱名称"]].Text = ShowOrderName(order);



                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["医嘱流水号"]].Text = order.ID;//2
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["医嘱状态"]].Text = order.Status.ToString();//新开立，审核，执行
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["组合号"]].Text = order.Combo.ID.ToString();//4
                //}

                if (orderlist == ColmSet.ALL || orderlist == ColmSet.P频次)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["频次"]].Text = order.Frequency.ID.ToString();
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["频次名称"]].Text = order.Frequency.Name;
                }
                if (orderlist == ColmSet.ALL || orderlist == ColmSet.Y用法)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["用法编码"]].Text = order.Usage.ID;
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["用法"]].Text = order.Usage.Name;//15
                }

                if (orderlist == ColmSet.ALL || orderlist == ColmSet.K开始时间)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["开始时间"]].Text = order.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");//开始时间
                }
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["开立时间"]].Text = order.MOTime.ToString();//开立时间

                if (orderlist == ColmSet.ALL || orderlist == ColmSet.Z执行科室)
                {
                    if (string.IsNullOrEmpty(order.ExeDept.ID))
                    {
                        //order.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(order.ReciptDept, order, order.ExeDept.ID, false);
                        order.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(false, order.ReciptDept.ID, order.ExeDept.ID, order.Item.ID);
                        order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ExeDept.ID);
                    }

                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["执行科室编码"]].Text = order.ExeDept.ID;
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["执行科室"]].Text = order.ExeDept.Name;
                }
                if (orderlist == ColmSet.ALL || orderlist == ColmSet.J急)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["急"]].Value = order.IsEmergency;
                }

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["检查部位"]].Text = order.CheckPartRecord;//检查部位
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["样本类型"]].Text = order.Sample.Name;//样本类型
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["取药药房编码"]].Text = order.StockDept.ID;//扣库科室
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["取药药房"]].Text = order.StockDept.Name;

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["备注"]].Text = order.Memo;//20
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["录入人编码"]].Text = order.Oper.ID;
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["录入人"]].Text = order.Oper.Name;

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["开立医生"]].Text = order.ReciptDoctor.Name;//开立医生
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["开立科室"]].Text = order.ReciptDept.Name;//开立科室

                if (order.EndTime != DateTime.MinValue)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["停止时间"]].Text = order.DCOper.OperTime.ToString();//停止时间 25
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["结束时间"]].Text = order.EndTime.ToString();//停止时间 25
                }
                else
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["停止时间"]].Text = "";//停止时间 25
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["结束时间"]].Text = "";//停止时间 25
                }
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["停止人编码"]].Text = order.DCOper.ID;
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["停止人编码"]].Text = order.DCOper.Name;
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["滴速"]].Text = order.Dripspreed;

                FS.HISFC.Models.Pharmacy.Item items = order.Item as FS.HISFC.Models.Pharmacy.Item;

                FS.HISFC.Models.Base.Item baseitem = order.Item as FS.HISFC.Models.Base.Item;

                string gbcode = "";
                if (items != null)
                {
                    gbcode = items.GBCode;
                }

                if (baseitem != null && string.IsNullOrEmpty(gbcode))
                {
                    gbcode = baseitem.GBCode;
                }

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["国家医保代码"]].Text = string.IsNullOrEmpty(gbcode) ? order.CountryCode : gbcode;

                order.CountryCode = gbcode;



                //首日量和系统类别
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["系统类别"]].Text = order.Item.SysClass.Name;
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["首日量"]].Text = order.FirstUseNum;
            }
            catch (Exception ex)
            {
                MessageBox.Show("向Fp添加信息时出错" + ex.Message, "提示");
            }
            if (order.SortID <= 0)
            {
                order.SortID = this.GetSortIDBySubCombNo(order.SubCombNO);
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
            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["顺序号"]].Value = order.SortID;//28


            this.fpOrder.Sheets[SheetIndex].Rows[row].Tag = order;
            this.currentOrder = order;

            if (order.OrderType.IsDecompose)
            {
                //if (!this.hsLongSubCombNo.Contains(order.SubCombNO) && !string.IsNullOrEmpty(order.SubCombNO.ToString()))
                //{
                //    this.hsLongSubCombNo.Add(order.SubCombNO, order.Clone());
                //}
                //else
                //{
                //    if ((hsLongSubCombNo[order.SubCombNO] as FS.HISFC.Models.Order.Inpatient.Order).SortID < order.SortID)
                //    {
                //        hsLongSubCombNo[order.SubCombNO] = order;
                //    }
                //}
            }
            else
            {
                //if (!this.hsShortSubCombNo.Contains(order.SubCombNO) && !string.IsNullOrEmpty(order.SubCombNO.ToString()))
                //{
                //    this.hsShortSubCombNo.Add(order.SubCombNO, order);
                //}
                //else
                //{
                //    if ((hsShortSubCombNo[order.SubCombNO] as FS.HISFC.Models.Order.Inpatient.Order).SortID < order.SortID)
                //    {
                //        hsShortSubCombNo[order.SubCombNO] = order;
                //    }
                //}
            }

            return;
        }

        /// <summary>
        /// 添满信息
        /// </summary>
        /// <param name="order"></param>
        private void ValidNewOrder(FS.HISFC.Models.Order.Inpatient.Order order)
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
                order.BeginTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
            }
            if (order.MOTime == DateTime.MinValue)
            {
                //order.MOTime = order.BeginTime;
                order.MOTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
            }

            if (!this.EditGroup && (order.Patient == null || order.Patient.ID == ""))
            {
                order.Patient = this.myPatientInfo;
            }
            if (order.ExeDept == null || order.ExeDept.ID == "")
            {
                //更改执行科室为患者科室
                //if (!this.EditGroup)
                //{
                //    order.ExeDept.ID = this.myPatientInfo.PVisit.PatientLocation.Dept.ID;
                //    order.ExeDept.Name = this.myPatientInfo.PVisit.PatientLocation.Dept.Name;
                //}
                //else
                //{
                //    order.ExeDept.ID = ((FS.HISFC.Models.Base.Employee)CacheManager.InOrderMgr.Operator).Dept.ID;
                //    order.ExeDept.Name = ((FS.HISFC.Models.Base.Employee)CacheManager.InOrderMgr.Operator).Dept.Name;
                //}
                //order.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(order.ReciptDept, order, order.ExeDept.ID, false);
                order.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(false, order.ReciptDept.ID, order.ExeDept.ID, order.Item.ID);
                order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ExeDept.ID);
            }
            if (order.ExeDept.Name == "" && order.ExeDept.ID != "")
            {
                order.ExeDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ExeDept.ID);
            }

            if (order.Oper.ID == null || order.Oper.ID == "")
            {
                order.Oper.ID = CacheManager.InOrderMgr.Operator.ID;
                order.Oper.Name = CacheManager.InOrderMgr.Operator.Name;
            }
        }

        private string GetColumnNameFromIndex(int i)
        {
            return dsAllLong.Tables[0].Columns[i].ColumnName;
        }

        public void SetEditGroup(bool isEdit)
        {
            this.EditGroup = isEdit;
            this.ucItemSelect1.Visible = isEdit;
            if (this.ucItemSelect1 != null)
                this.ucItemSelect1.EditGroup = isEdit;

            this.fpOrder.Sheets[0].DataSource = null;
            this.fpOrder.Sheets[1].DataSource = null;
            #region {D17BD9FB-F362-4755-97FE-08404D477C39} 点击2次组套管理按钮 开立按钮无响应
            this.fpOrder.Sheets[0].RowCount = 0;
            this.fpOrder.Sheets[1].RowCount = 0;
            #endregion
            this.fpOrder.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
            this.fpOrder.Sheets[1].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
        }

        /// <summary>
        /// 获取选择的行数
        /// </summary>
        /// <returns></returns>
        protected ArrayList GetSelectedRows()
        {
            ArrayList rows = new ArrayList();

            for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpOrder.ActiveSheet.IsSelected(i, 0))
                {
                    rows.Add(i);
                }
            }
            return rows;
        }

        ///<summary>
        /// 刷新组合
        /// </summary>
        public void RefreshCombo()
        {
            try
            {
                /*- 
                 *  Edit By liangjz 2005-10 减少组合的重复刷新 在长、临嘱复制时对refreshComboFlag赋不同值 减少刷新
                ---*/

                //标记当前选中行
                if (this.ActiveRowIndex >= 0 && this.ActiveRowIndex < this.fpOrder.ActiveSheet.RowCount)
                {
                    this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["顺序号"]].Tag = "哈哈";
                }

                if (this.refreshComboFlag == "0" || this.refreshComboFlag == "2")
                {
                    try
                    {
                        //if (!this.IsDesignMode)
                        //{
                        //    this.fpOrder.Sheets[0].SortRows(dicColmSet["顺序号"], true, true);
                        //}
                        //else
                        //{
                        this.fpOrder.Sheets[0].SortRows(dicColmSet["顺序号"], false, true);
                        //}
                    }
                    catch { }

                    Classes.Function.DrawCombo(this.fpOrder.Sheets[0], dicColmSet["组合号"], dicColmSet["组"]);
                }

                if (this.refreshComboFlag == "1" || this.refreshComboFlag == "2")
                {
                    try
                    {
                        //if (!this.IsDesignMode)
                        //{
                        //    this.fpOrder.Sheets[1].SortRows(dicColmSet["顺序号"], true, true);
                        //}
                        //else
                        //{
                        this.fpOrder.Sheets[1].SortRows(dicColmSet["顺序号"], false, true);
                        //}
                    }
                    catch { }

                    Classes.Function.DrawCombo(this.fpOrder.Sheets[1], dicColmSet["组合号"], dicColmSet["组"]);

                }

                //标记当前选中行
                //for (int i = this.fpOrder.ActiveSheet.RowCount - 1; i >= 0; i--)
                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    if (this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag != null &&
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag.ToString() == "哈哈")
                    {
                        this.ActiveRowIndex = i;
                        this.fpOrder.ActiveSheet.ActiveRowIndex = i;
                        this.fpOrder.ActiveSheet.AddSelection(i, 0, 1, this.fpOrder.ActiveSheet.ColumnCount);
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag = null;
                        break;
                    }
                }

                //赋值为默认值
                this.refreshComboFlag = "2";
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("刷新医嘱组合信息时出现不可预知错误！请退出开立界面重试或与管理员联系!\n") + ex.Message);
            }
        }

        /// <summary>
        /// 更新医嘱状态
        /// </summary>
        /// <param name="rowIndex">刷新的行号，-1 表示全部</param>
        public void RefreshOrderState(int rowIndex)
        {
            try
            {
                if (rowIndex >= 0)
                {
                    this.ChangeOrderState(rowIndex, fpOrder.ActiveSheetIndex, false);
                }
                else
                {
                    for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
                    {
                        this.ChangeOrderState(i, fpOrder.ActiveSheetIndex, false);
                    }
                }
            }
            catch
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("刷新医嘱状态时出现不可预知错误！请退出开立界面重试或与管理员联系"));
            }
        }

        public void RefreshOrderState(bool reset)
        {
            try
            {
                for (int i = 0; i < this.fpOrder.Sheets[0].Rows.Count; i++)
                {
                    this.ChangeOrderState(i, 0, reset);
                }
                for (int i = 0; i < this.fpOrder.Sheets[1].Rows.Count; i++)
                {
                    this.ChangeOrderState(i, 1, reset);
                }
            }
            catch
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("刷新医嘱状态时出现不可预知错误！请退出开立界面重试或与管理员联系"));
            }
        }

        /// <summary>
        /// 新医嘱校验
        /// </summary>
        /// <param name="alSaveOrder">新医嘱列表</param>
        /// <returns></returns>
        public int CheckOrder(ref ArrayList alSaveOrder)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;

            alSaveOrder = new ArrayList();

            //是否可以开立库存为0 药品
            int iCheck = Classes.Function.GetIsOrderCanNoStock();
            bool IsModify = true;

            //合理用药检查的药品
            ArrayList alPassOrder = new ArrayList();
            string errInfo = "";

            #region 长期医嘱
            for (int i = 0; i < this.fpOrder.Sheets[0].RowCount; i++)
            {
                order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.Sheets[0].Rows[i].Tag;

                if (order.Status == 3 || order.Status == 4)
                {
                    continue;
                }

                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    if (this.helper != null)
                    {
                        if (order.Frequency != null && !string.IsNullOrEmpty(order.Frequency.ID))
                        {
                            order.Frequency = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID);
                        }
                    }
                    alPassOrder.Add(order.Clone());
                }

                if (order.Status == 0 || order.Status == 5)
                {
                    //未审核的医嘱
                    IsModify = true;

                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        if (order.OrderType.IsCharge || order.Item.ID != "999")
                        {
                            if (string.IsNullOrEmpty(order.StockDept.ID))
                            {
                                ShowErr("[" + order.Item.Name + "]" + "扣库科室为空！", i, 1);
                                return -1;
                            }
                        }

                        //药品
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            //中草药
                            if (order.HerbalQty == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "付数不能为零！", i, 1);
                                return -1;
                            }
                        }
                        else
                        {
                            if (order.DoseOnce == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "每次剂量不能为零！", i, 0);
                                return -1;
                            }
                            if (order.DoseUnit == "")
                            {
                                ShowErr("[" + order.Item.Name + "]" + "剂量单位不能为空！", i, 0);
                                return -1;
                            }
                        }
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "频次不能为空！", i, 0);
                            return -1;
                        }
                        if (order.Usage.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "用法不能为空！", i, 0);
                            return -1;
                        }

                        //{6B70B558-72C9-4DEF-874F-DABD0A9B5198}
                        if (order.Item.SpecialFlag == "1")
                        {
                            if (order.Item.SpecialFlag == "1")
                            {
                                if (usageList.Count > 0)
                                {
                                    foreach (FS.HISFC.Models.Base.Const con in usageList)
                                    {

                                        if (order.Usage.Name == con.Name)
                                        {
                                            if (order.Dripspreed == "" || order.Dripspreed == null)
                                            {
                                                ShowErr("[" + order.Item.Name + "]" + "滴速不能为空！", i, 0);
                                                return -1;
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        //if (((FS.HISFC.Models.Pharmacy.Item)order.Item).Price == 0)
                        //{
                        //    if (order.OrderType.Name.IndexOf("嘱托") == -1)
                        //    {
                        //        ShowErr("[" + order.Item.Name + "]" + "价格为零不允许收取！", i, 0);
                        //        return -1;
                        //    }
                        //}

                        //if (Classes.Function.HelperFrequency.GetObjectFromID(order.Frequency.ID) != null &&
                        //    (Classes.Function.HelperFrequency.GetObjectFromID(order.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Times.Length
                        //    != order.Frequency.Times.Length)
                        //{
                        //    ShowErr("[" + order.Item.Name + "]" + "频次时间点错误，请重新选择！", i, 0);
                        //    return -1;
                        //}

                        if (string.IsNullOrEmpty(order.FirstUseNum))
                        {
                            ShowErr("[" + order.Item.Name + "]" + "首日量不能为空！", i, 0);
                            return -1;
                        }
                        else
                        {
                            try
                            {
                                int kk = FS.FrameWork.Function.NConvert.ToInt32(order.FirstUseNum);

                                if (kk < 0)
                                {
                                    ShowErr("[" + order.Item.Name + "]" + "首日量输入数字无效，请重新输入！", i, 0);
                                    return -1;
                                }
                            }
                            catch
                            {
                                ShowErr("[" + order.Item.Name + "]" + "首日量输入非数字，请重新输入！", i, 0);
                                return -1;
                            }
                        }

                        #region 判断停用缺药

                        FS.HISFC.Models.Pharmacy.Item phaItem = null;
                        //string errInfo = "";
                        if (order.StockDept != null && order.StockDept.ID != "")
                        {
                            if (order.Item.ID != "999" && order.OrderType.IsCharge)
                            {
                                if (Classes.Function.CheckDrugState(this.Patient, order.StockDept, order.Item, false, ref phaItem, ref errInfo) == -1)
                                {
                                    ShowErr(errInfo, i, 0);
                                    return -1;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //非药品
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "频次不能为空！", i, 0);
                            return -1;
                        }
                        if (order.Qty == 0)
                        {

                            ShowErr("[" + order.Item.Name + "]" + "数量不能为空！", i, 0);
                            return -1;
                        }
                        if (order.ExeDept.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "执行科室为空，请选择执行科室！", i, 0);
                            return -1;
                        }
                        //if (order.Item.Price == 0)
                        //{
                        //    if (order.OrderType.Name.IndexOf("嘱托") == -1)
                        //    {
                        //        ShowErr("[" + order.Item.Name + "]" + "价格为零不允许收取！", i, 0);
                        //        return -1;
                        //    }
                        //}
                    }
                    if (order.EndTime != DateTime.MinValue)
                    {
                        if (order.EndTime < order.BeginTime)
                        {
                            ShowErr("[" + order.Item.Name + "]" + "停止时间不应早于开始时间", i, 0);
                            return -1;
                        }
                    }
                    if (FS.FrameWork.Public.String.ValidMaxLengh(order.Memo, 80) == false)
                    {
                        //ShowErr("[" + order.Item.Name + "]" + "的备注超长!", i, 0);
                        //return -1;
                        Classes.Function.ShowBalloonTip(1, "警告", "[" + order.Item.Name + "]" + "的备注超长!\r\n\r\n可能导致医嘱单显示不全", ToolTipIcon.Warning);
                    }
                    if (!order.OrderType.IsCharge && FS.FrameWork.Public.String.ValidMaxLengh(order.Item.Name, 50) == false)
                    {
                        //ShowErr("[" + order.Item.Name + "]" + "的名称超长!", i, 0);
                        //return -1;
                        Classes.Function.ShowBalloonTip(1, "警告", "[" + order.Item.Name + "]" + "的名称超长!\r\n\r\n可能导致医嘱单显示不全", ToolTipIcon.Warning);
                    }
                    if (order.Qty > 5000)
                    {
                        ShowErr("[" + order.Item.Name + "]" + "数量太大！", i, 0);
                        return -1;
                    }
                    if (order.ID == "")
                    {
                        IsModify = true;
                    }
                    alSaveOrder.Add(order);
                }
            }

            #endregion

            #region 临时医嘱
            for (int i = 0; i < this.fpOrder.Sheets[1].RowCount; i++)
            {
                order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.Sheets[1].Rows[i].Tag;

                if (order.Status == 3 || order.Status == 4)
                {
                    continue;
                }
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    if (this.helper != null)
                    {
                        if (order.Frequency != null)
                        {
                            order.Frequency = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID);
                        }
                    }
                    alPassOrder.Add(order.Clone());
                }

                if (order.Status == 0 || order.Status == 5)
                {
                    //未审核的医嘱
                    IsModify = true;

                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        if (order.OrderType.IsCharge || order.Item.ID != "999")
                        {
                            if (string.IsNullOrEmpty(order.StockDept.ID))
                            {
                                ShowErr("[" + order.Item.Name + "]" + "扣库科室为空！", i, 1);
                                return -1;
                            }
                        }

                        //药品
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            //中草药
                            if (order.HerbalQty == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "付数不能为零！", i, 1);
                                return -1;
                            }
                        }
                        else
                        {
                            //其他
                            if (order.DoseOnce == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "每次剂量不能为零！", i, 1);
                                return -1;
                            }
                            if (order.DoseUnit == "")
                            {
                                ShowErr("[" + order.Item.Name + "]" + "剂量单位不能为空！", i, 1);
                                return -1;
                            }
                            try
                            {
                                if (order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.').Contains("."))
                                {
                                    ShowErr("药品【" + order.Item.Name + "】总量不允许为小数！", i, 1);
                                    return -1;
                                }
                            }
                            catch
                            {
                                ShowErr("药品【" + order.Item.Name + "】总量不允许为小数！", i, 1);
                                return -1;
                            }
                        }
                        if (order.Item.ID != "999")
                        {
                            if ((order.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "基本计量为零，没有维护基本计量", i, 1);
                                return -1;
                            }
                        }
                        if (order.Qty <= 0)
                        {
                            ShowErr("[" + order.Item.Name + "]" + "数量必须大于0！", i, 1);
                            return -1;
                        }
                        if (order.Unit == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "单位不能为空！", i, 1);
                            return -1;
                        }
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "频次不能为空！", i, 1);
                            return -1;
                        }
                        if (order.Usage.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "用法不能为空！", i, 1);
                            return -1;
                        }

                        //{6B70B558-72C9-4DEF-874F-DABD0A9B5198}
                        if (order.Item.SpecialFlag == "1")
                        {
                            if (usageList.Count > 0)
                            {
                                foreach (FS.HISFC.Models.Base.Const con in usageList)
                                {

                                    if (order.Usage.Name == con.Name)
                                    {
                                        if (order.Dripspreed == "" || order.Dripspreed == null)
                                        {
                                            ShowErr("[" + order.Item.Name + "]" + "滴速不能为空！", i, 1);
                                            return -1;
                                        }

                                    }
                                }
                            }
                        }

                        //检查库存(嘱托医嘱除外)
                        if (order.OrderType.IsCharge)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = null;
                            //string errInfo = "";
                            if (order.StockDept != null && order.StockDept.ID != "")
                            {
                                if (Classes.Function.CheckDrugState(this.Patient, order.StockDept, order.Item, false, ref phaItem, ref errInfo) == -1)
                                {
                                    ShowErr(errInfo, i, 0);
                                    return -1;
                                }
                            }
                        }
                    }
                    else
                    {
                        //非药品
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "频次不能为空！", i, 1);
                            return -1;
                        }
                        if (order.Qty == 0)
                        {
                            ShowErr("[" + order.Item.Name + "]" + "数量不能为空！", i, 1);
                            return -1;
                        }
                        if (order.ExeDept.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "执行科室为空，请选择执行科室！", i, 0);
                            return -1;
                        }
                    }
                    if (!order.OrderType.IsCharge && FS.FrameWork.Public.String.ValidMaxLengh(order.Item.Name, 50) == false)
                    {
                        //ShowErr("[" + order.Item.Name + "]" + "的名称超长!", i, 0);
                        //return -1;
                        Classes.Function.ShowBalloonTip(1, "警告", "[" + order.Item.Name + "]" + "的名称超长!\r\n\r\n可能导致医嘱单显示不全", ToolTipIcon.Warning);
                    }

                    if (FS.FrameWork.Public.String.ValidMaxLengh(order.Memo, 80) == false)
                    {
                        //ShowErr("[" + order.Item.Name + "]" + "的备注超长!", i, 0);
                        //return -1;
                        Classes.Function.ShowBalloonTip(1, "警告", "[" + order.Item.Name + "]" + "的备注超长!\r\n\r\n可能导致医嘱单显示不全", ToolTipIcon.Warning);
                    }
                    if (order.Qty > 5000)
                    {
                        ShowErr("[" + order.Item.Name + "]" + "数量太大！", i, 1);
                        return -1;
                    }
                    if (JudgeOrder(this.myPatientInfo, order, ref errInfo) == -1)
                    {
                        ShowErr(errInfo, i, 1);
                        return -1;
                    }

                    if (order.ID == "")
                    {
                        IsModify = true;
                    }

                    alSaveOrder.Add(order);
                }
            }
            #endregion

            if (IsModify == false)
            {
                return -1;//未有新录入的医嘱
            }

            #region 合理用药自动审查

            if (this.IReasonableMedicine != null)
            {
                this.IReasonableMedicine.PassShowFloatWindow(false);
                return this.PassCheckOrder(alPassOrder, true);
            }
            #endregion

            return 0;
        }

        /// <summary>
        /// 检查开立信息，显示错误！
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="iRow"></param>
        /// <param name="SheetIndex"></param>
        private void ShowErr(string strMsg, int iRow, int SheetIndex)
        {
            this.fpOrder.ActiveSheetIndex = SheetIndex;
            this.fpOrder.Sheets[SheetIndex].ClearSelection();
            this.fpOrder.Sheets[SheetIndex].ActiveRowIndex = iRow;
            SelectionChanged();
            this.fpOrder.Sheets[SheetIndex].AddSelection(iRow, 0, 1, 1);
            MessageBox.Show(strMsg);

            this.fpOrder.ShowRow(0, iRow, FarPoint.Win.Spread.VerticalPosition.Center);
        }

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void QueryOrder()
        {
            try
            {
                this.fpOrder.Sheets[0].RowCount = 0;
                this.fpOrder.Sheets[1].RowCount = 0;
                if (this.dsAllLong != null && this.dsAllLong.Tables[0].Rows.Count > 0)
                {
                    this.dsAllLong.Tables[0].Rows.Clear();
                }
                if (this.dsAllShort != null && this.dsAllShort.Tables[0].Rows.Count > 0)
                {
                    this.dsAllShort.Tables[0].Rows.Clear();
                }

                //this.hsLongSubCombNo = new Hashtable();
                //this.hsShortSubCombNo = new Hashtable();

                this.alIndicationsDrug = null;
            }
            catch
            {
                MessageBox.Show("清除医嘱记录信息出错！", "提示");
            }
            if (this.myPatientInfo == null)
            {
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询医嘱,请稍候!");
            Application.DoEvents();

            //查询所有医嘱类型
            ArrayList al = CacheManager.InOrderMgr.QueryOrder(this.myPatientInfo.ID);

            ArrayList alOrder = new ArrayList();

            DateTime dateNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj in al)
            {
                if (orderObj.OrderType.IsDecompose)
                {
                    //if (!this.hsLongSubCombNo.Contains(orderObj.SubCombNO) && !string.IsNullOrEmpty(orderObj.SubCombNO.ToString()))
                    //{
                    //    this.hsLongSubCombNo.Add(orderObj.SubCombNO, orderObj);
                    //}
                    //else
                    //{
                    //    if ((hsLongSubCombNo[orderObj.SubCombNO] as FS.HISFC.Models.Order.Inpatient.Order).SortID < orderObj.SortID)
                    //    {
                    //        hsLongSubCombNo[orderObj.SubCombNO] = orderObj;
                    //    }
                    //}
                }
                else
                {
                    //if (!this.hsShortSubCombNo.Contains(orderObj.SubCombNO) && !string.IsNullOrEmpty(orderObj.SubCombNO.ToString()))
                    //{
                    //    this.hsShortSubCombNo.Add(orderObj.SubCombNO, orderObj);
                    //}
                    //else
                    //{
                    //    if ((hsShortSubCombNo[orderObj.SubCombNO] as FS.HISFC.Models.Order.Inpatient.Order).SortID < orderObj.SortID)
                    //    {
                    //        hsShortSubCombNo[orderObj.SubCombNO] = orderObj;
                    //    }
                    //}
                }

                if (orderObj.MOTime.AddDays(allOrderShowDays).Date > dateNow.Date //7天内所有医嘱
                    || ("0,1,2,5".Contains(orderObj.Status.ToString())
                    && orderObj.OrderType.IsDecompose)//有效长嘱
                    || ((orderObj.MOTime.AddDays(this.shortOrderShowDays).Date > dateNow.Date)
                    && !orderObj.OrderType.IsDecompose)//设置天数内有效临嘱
                    )
                {
                    alOrder.Add(orderObj);
                }
            }

            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在显示医嘱,请稍候!");
            Application.DoEvents();
            if (this.IsDesignMode)
            {
                //tooltip.SetToolTip(this.fpOrder, "开立时候长期医嘱只显示有效的，临时医嘱只显示24小时内的医嘱。");
                //tooltip.Active = true;
                this.ucItemSelect1.Visible = true;
                try
                {
                    this.fpOrder.Sheets[0].DataSource = null;
                    this.fpOrder.Sheets[1].DataSource = null;
                    this.AddObjectsToFarpoint(alOrder);
                    this.fpOrder.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
                    this.fpOrder.Sheets[1].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
                    this.RefreshCombo();
                    this.RefreshOrderState(-1);
                    this.fpOrder.Sheets[1].DefaultStyle.BackColor = Color.White;

                    //this.fpOrder.ShowRow(0, this.fpOrder.ActiveSheet.RowCount - 1, VerticalPosition.Center);
                    this.fpOrder.ShowRow(0, 0, VerticalPosition.Center);
                }
                catch (Exception ex)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //tooltip.SetToolTip(this.fpOrder, "");
                try
                {
                    this.ucItemSelect1.Visible = false;

                    this.AddObjectsToTable(al);
                    dvLong = new DataView(dsAllLong.Tables[0]);
                    dvShort = new DataView(dsAllShort.Tables[0]);
                    this.fpOrder.Sheets[0].DataSource = dvLong;
                    this.fpOrder.Sheets[1].DataSource = dvShort;
                    this.fpOrder.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                    this.fpOrder.Sheets[1].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                    //CheckSortID();//检查顺序号

                    dvLong.RowFilter = "开立时间 >'" + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddDays(1 - allOrderShowDays).Date.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " or 医嘱状态 in('0','1','2','5')";//有效长嘱
                    dvShort.RowFilter = "开立时间 >'" + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddDays(1 - allOrderShowDays).Date.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                        + " or (医嘱状态 in('0','1','2','5') and 开立时间>'" + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddDays(1 - this.shortOrderShowDays).Date.ToString("yyyy-MM-dd HH:mm:ss") + "')";//设置天数内有效临嘱

                    this.RefreshCombo();
                    this.RefreshOrderState(-1);
                    //this.RefreshIsEmergency();//{C222F7C0-2E51-4084-AEA2-A9F1FA41AC8B}
                    //SetTip(0);
                    //SetTip(1);

                    //this.fpOrder.ShowRow(0, this.fpOrder.ActiveSheet.RowCount - 1, VerticalPosition.Center);
                    this.fpOrder.ShowRow(0, 0, VerticalPosition.Center);
                }
                catch (Exception ex)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(ex.Message);
                }
            }

            this.fpOrder.Sheets[0].ClearSelection();
            this.fpOrder.Sheets[1].ClearSelection();

            //清除提示信息
            this.txtItemInfo.Text = string.Empty;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void QueryOrderByTime(EnumFilterList State)
        {
            this.fpOrder.Sheets[0].RowCount = 0;
            this.fpOrder.Sheets[1].RowCount = 0;

            ArrayList alOrder = new ArrayList();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询医嘱,请稍候!");

            //查询时候才能过滤
            if (State == EnumFilterList.All)
            {
                alOrder = CacheManager.InOrderMgr.QueryOrder(myPatientInfo.ID);
            }
            else if (State == EnumFilterList.Today)
            {
                alOrder = CacheManager.InOrderMgr.QueryOrder(myPatientInfo.ID, CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().Date, CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddDays(1).Date);
            }
            else if (State == EnumFilterList.Valid)
            {
                alOrder = CacheManager.InOrderMgr.QueryOrderByState(myPatientInfo.ID, "'1','2'");
            }
            else if (State == EnumFilterList.Invalid)
            {
                alOrder = CacheManager.InOrderMgr.QueryOrderByState(myPatientInfo.ID, "'3','4'");
            }
            else if (State == EnumFilterList.New)
            {
                alOrder = CacheManager.InOrderMgr.QueryOrderByState(myPatientInfo.ID, "'0','5'");
            }
            else if (State == EnumFilterList.UC_ULOrder)
            {
                string whereSQL = @"where  class_code in ('UL','UC')
                                   and inpatient_no	= '{0}'	
                                   and SUBTBL_FLAG = '0'";
                whereSQL = string.Format(whereSQL, myPatientInfo.ID);

                alOrder = CacheManager.InOrderMgr.QueryOrderBase(whereSQL);

                this.fpOrder.ActiveSheetIndex = 1;
            }
            else
            {
                alOrder = CacheManager.InOrderMgr.QueryOrder(myPatientInfo.ID);
            }

            if (alOrder == null)
            {
                MessageBox.Show("查询医嘱出错！\r\n" + CacheManager.InOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.fpOrder.Sheets[0].DataSource = null;
                this.fpOrder.Sheets[1].DataSource = null;
                this.AddObjectsToFarpoint(alOrder);

                this.fpOrder.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
                this.fpOrder.Sheets[1].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
                this.RefreshCombo();
                this.RefreshOrderState(-1);

                this.fpOrder.Sheets[1].DefaultStyle.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryOrder();
            return 0;
        }

        public bool CheckNewOrder()
        {
            bool isHaveNewOrder = false;
            if (this.IsDesignMode)
            {
                for (int sheet = 0; sheet < this.fpOrder.Sheets.Count; sheet++)
                {
                    for (int i = 0; i < this.fpOrder.Sheets[sheet].Rows.Count; i++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.Sheets[sheet].Rows[i].Tag;

                        if (order.Status == 0 || order.Status == 5)
                        {
                            isHaveNewOrder = true;
                        }
                    }
                }
            }

            if (isHaveNewOrder)
            {
                if (MessageBox.Show("当前还有未保存的医嘱，确定退出？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 过滤医嘱显示
        /// 0 All,1当天 2，有效，3 无效，4 未审核
        /// </summary>
        /// <param name="State"></param>
        public void Filter(EnumFilterList State)
        {
            if (this.myPatientInfo == null)
            {
                return;
            }

            if (!CheckNewOrder())
            {
                return;
            }

            if (this.bIsDesignMode)
            {
                this.QueryOrderByTime(State);
            }
            else
            {
                try
                {
                    if (this.fpOrder.ActiveSheetIndex == 0)
                    {
                        dvLong.RowFilter = "1=2";
                    }
                    else
                    {
                        dvShort.RowFilter = "1=2";
                    }

                    //查询时候才能过滤
                    if (State == EnumFilterList.All)
                    {
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "";
                        }
                        else
                        {
                            dvShort.RowFilter = "";
                        }
                    }
                    else if (State == EnumFilterList.Today)
                    {
                        DateTime dt = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                        DateTime dt1 = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "开立时间 >='" + dt1.ToString() + "' and 开立时间<='" + dt2.ToString() + "'";
                        }
                        else
                        {
                            dvShort.RowFilter = "开立时间 >='" + dt1.ToString() + "' and 开立时间<='" + dt2.ToString() + "'";
                        }
                    }

                    else if (State == EnumFilterList.Valid)
                    {
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "医嘱状态 ='1' or 医嘱状态='2'";
                        }
                        else
                        {
                            dvShort.RowFilter = "医嘱状态 ='1' or 医嘱状态='2'";
                        }
                    }
                    else if (State == EnumFilterList.Invalid)
                    {
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "医嘱状态 in ( '3','4')";
                        }
                        else
                        {
                            dvShort.RowFilter = "医嘱状态 in ( '3','4')";
                        }
                    }
                    else if (State == EnumFilterList.New)
                    {
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "医嘱状态 in ( '0','5')";
                        }
                        else
                        {
                            dvShort.RowFilter = "医嘱状态 in ( '0','5')";
                        }
                    }
                    else if (State == EnumFilterList.UC_ULOrder)
                    {
                        this.fpOrder.ActiveSheetIndex = 1;
                        dvShort.RowFilter = "系统类别 in ( '检查','检验')";
                    }
                    else
                    {
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "";
                        }
                        else
                        {
                            dvShort.RowFilter = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace);
                }
            }
            this.RefreshCombo();
            this.RefreshOrderState(-1);
        }

        #region MQ消息发送

        /// <summary>
        /// 即时消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        protected override void OnSendMessage(object sender, string msg)
        {
            msg = "医嘱变化，请及时处理！\n\n患者：" + this.myPatientInfo.Name + "\n住院号：" + this.myPatientInfo.PID.PatientNO + "\n床号：" + this.myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);

            FS.FrameWork.Models.NeuObject targetDept = new FS.FrameWork.Models.NeuObject();
            targetDept.ID = this.myPatientInfo.PVisit.PatientLocation.NurseCell.ID;
            targetDept.Name = this.myPatientInfo.PVisit.PatientLocation.NurseCell.Name;


            base.OnSendMessage(targetDept, msg);
        }

        /// <summary>
        /// 发送消息类别 
        /// </summary>
        private enum SendType
        {
            /// <summary>
            /// 添加
            /// </summary>
            Add,

            /// <summary>
            /// 删除
            /// </summary>
            Delete,

            /// <summary>
            /// 取消作废
            /// </summary>
            RollBackCancel
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="type"></param>
        private void SendMessage(SendType type)
        {
            string tip = "";
            switch (type)
            {
                case SendType.Add:
                    tip = "新增";
                    break;
                case SendType.Delete:
                    tip = "作废或删除";
                    break;
                case SendType.RollBackCancel:
                    tip = "取消作废";
                    break;
            }

            if (this.myPatientInfo != null && this.IsDesignMode)
            {
                string msg = "有【" + tip + "】医嘱，请及时处理！\n\n科室:" + myPatientInfo.PVisit.PatientLocation.Dept.Name + "\n患者：【" + this.myPatientInfo.Name + "】\n住院号：" + this.myPatientInfo.PID.PatientNO + "\n床号：" + this.myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);

                FS.FrameWork.Models.NeuObject targetDept = new FS.FrameWork.Models.NeuObject();
                targetDept.ID = this.myPatientInfo.PVisit.PatientLocation.NurseCell.ID;
                targetDept.Name = this.myPatientInfo.PVisit.PatientLocation.NurseCell.Name;

                base.OnSendMessage(targetDept, msg);
            }
        }
        #endregion

        #region 校验医嘱开立 是否知情同意、毒麻、校验库存

        /// <summary>
        /// 校验医嘱开立 是否知情同意、毒麻、校验库存
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int JudgeOrder(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.Inpatient.Order order, ref string errInfo)
        {
            if (patient == null)
            {
                return -1;
            }
            if (order == null)
            {
                return -1;
            }

            //是否允许对零库存状态下开立药品
            int iCheck = Classes.Function.GetIsOrderCanNoStock();
            try
            {
                FS.HISFC.Models.Base.Item tempItem = (order.Item) as FS.HISFC.Models.Base.Item;
                FS.HISFC.Models.Pharmacy.Item tempPharmacy = order.Item as FS.HISFC.Models.Pharmacy.Item;
                int iFlag = -1;
                if (tempItem == null)
                {
                    //MessageBox.Show("医嘱明细项目类型转换错误");
                    errInfo = "医嘱明细项目类型转换错误!";
                    return -1;
                }
                //库存检查
                //if (order.Item.IsPharmacy && order.OrderType.IsCharge)
                if (order.Item.ItemType == EnumItemType.Drug && order.OrderType.IsCharge)
                {
                    if (Classes.Function.CheckPharmercyItemStock(iCheck, order.Item.ID, order.Item.Name, this.GetReciptDept().ID, order.Qty, "I") == false)
                    {
                        //MessageBox.Show(order.Item.Name + "库存不足!");
                        errInfo = "药品[" + order.Item.Name + "]目前库存不足使用！";
                        return -1;
                    }
                }
                //判断医保知情同意  只对收费的医嘱类型进行知情同意判断
                if (order.OrderType.IsCharge)
                {
                    //iFlag = Classes.Function.IsCanOrder(patient, tempItem);
                }
                if (iFlag == 0) return -1;

            }
            catch (Exception ex)
            {
                //MessageBox.Show("项目转换错误" + ex.Message, "提示");
                errInfo = "项目转换错误" + ex.Message;
            }
            return 1;
        }
        #endregion

        #region add by xuewj 化疗医嘱开立 {1F2B9330-7A32-4da4-8D60-3A4568A2D1D8}

        /// <summary>
        /// 化疗医嘱开立
        /// </summary>
        public void AddAssayCure()
        {
            if (this.fpOrder.ActiveSheetIndex == 1 && this.fpOrder.ActiveSheet.ActiveRowIndex > -1)
            {
                List<FS.HISFC.Models.Order.Order> alOrder = this.GetSelectedOrders();
                if (alOrder == null || alOrder.Count == 0)
                {
                    MessageBox.Show("您需要选择临时医嘱新开立的药品!");
                    return;
                }
                ucAssayCure uc = new ucAssayCure();
                uc.Orders = new ArrayList(alOrder);
                uc.MakeSuccessed += new ucAssayCure.MakeSuccessedHandler(uc_MakeSuccessed);
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "化疗医嘱开立";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            }
        }

        /// <summary>
        /// 取临时医嘱页中的医嘱项目,传给化疗窗口
        /// </summary>
        /// <returns>null或0失败</returns>
        private List<FS.HISFC.Models.Order.Order> GetSelectedOrders()
        {
            List<FS.HISFC.Models.Order.Order> alOrders = new List<FS.HISFC.Models.Order.Order>();
            if (this.fpOrder.ActiveSheetIndex == 0)//长嘱
            {
                return alOrders;
            }

            FS.HISFC.Models.Order.Inpatient.Order tempOrder = null;
            for (int i = this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].RowCount - 1; i > -1; i--)//只处理临时医嘱,如果是长嘱,必须先复制到临嘱
            {
                if (this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].IsSelected(i, 0))
                {
                    tempOrder = this.GetObjectFromFarPoint(i, 1);//临时医嘱
                    if (tempOrder != null)
                    {
                        if ((tempOrder.Status == 0 || tempOrder.Status == 5)
                            && tempOrder.Item.ItemType == EnumItemType.Drug)//新开立的药品医嘱
                        {
                            alOrders.Add(tempOrder);
                        }
                    }
                }
            }

            return alOrders;
        }

        /// <summary>
        /// 生成化疗医嘱
        /// </summary>
        /// <param name="alOrders"></param>
        private void uc_MakeSuccessed(ArrayList alOrders)
        {
            this.needUpdateDTBegin = false;
            #region {69CD0AA2-FD34-46fd-BD94-96ED500A6E08}
            FS.HISFC.Models.Order.Inpatient.Order ordtmp = this.GetObjectFromFarPoint(this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].ActiveRowIndex, this.fpOrder.ActiveSheetIndex);
            if (ordtmp == null)
            {
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(ordtmp.ID))
                {
                    Delete(this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].ActiveRowIndex, true);
                }
            }
            //Delete(this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].ActiveRowIndex, true);//{0AAB51FC-0258-48e7-B3E5-1721F7C53474}
            #endregion
            foreach (FS.HISFC.Models.Order.Inpatient.Order orderInfo in alOrders)
            {
                //this.ucItemSelect1.OrderType = orderType;
                //FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();
                //item.Qty = 1.0M;
                //item.PriceUnit = "个";
                //item.ID = "999";//自定义
                //item.SysClass.ID = "M";
                //item.Name = orderName + "医嘱";
                //this.ucItemSelect1.FeeItem = item;
                this.AddNewOrder(orderInfo.Clone(), this.fpOrder.ActiveSheetIndex);
            }
            this.needUpdateDTBegin = true;
            this.RefreshCombo();
        }

        #endregion

        /// <summary>
        /// 停止小时收费医嘱 jin
        /// </summary>
        /// <param name="order"></param>
        /// <param name="CacheManager.OrderIntegrate"></param>
        /// <param name="trans"></param>
        /// <param name="isCharge"></param>
        /// <returns></returns>
        protected virtual int DCHoursOrder(FS.HISFC.Models.Order.Inpatient.Order order, IDbTransaction trans, bool isCharge)
        {
            int iReturn = 0;
            if (order.Frequency.ID == this.hoursFrequencyID)
            {
                FS.FrameWork.Models.NeuObject nurseStation = ((FS.HISFC.Models.Base.Employee)CacheManager.InOrderMgr.Operator).Nurse.Clone();

                //ArrayList alMyOrder = CacheManager.OrderIntegrate.QueryOrderAndSubtblByOrderID(order.ID);
                ArrayList alMyOrder = CacheManager.InOrderMgr.QuerySubtbl(order.Combo.ID);
                alMyOrder.Add(order);
                ArrayList alNeedFeeExecOrderDrug = new ArrayList();
                ArrayList alNeedFeeExecOrderUnDrug = new ArrayList();
                foreach (FS.HISFC.Models.Order.Inpatient.Order objOrder in alMyOrder)
                {
                    iReturn = CacheManager.InOrderMgr.DecomposeOrderToNow(objOrder, 0, false, order.EndTime);
                    if (iReturn < 0)
                    {
                        return iReturn;
                    }
                    ArrayList alTmp = new ArrayList();
                    if (objOrder.Item.ItemType == EnumItemType.Drug)
                    {
                        alTmp = CacheManager.InOrderMgr.QueryUnFeeExecOrderByOrderID(this.myPatientInfo.ID, "1", objOrder.ID, order.NextMOTime, order.EndTime);
                        if (alTmp.Count > 0)
                        {
                            alNeedFeeExecOrderDrug.AddRange(alTmp);
                        }
                    }
                    else
                    {
                        alTmp = CacheManager.InOrderMgr.QueryUnFeeExecOrderByOrderID(this.myPatientInfo.ID, "2", objOrder.ID, order.NextMOTime, order.EndTime);
                        if (alTmp.Count > 0)
                        {
                            alNeedFeeExecOrderUnDrug.AddRange(alTmp);
                        }
                    }

                }
                if (alNeedFeeExecOrderDrug.Count > 0)
                {
                    List<FS.HISFC.Models.Order.ExecOrder> listFeeOrder = new List<FS.HISFC.Models.Order.ExecOrder>();
                    foreach (FS.HISFC.Models.Order.ExecOrder obj in alNeedFeeExecOrderDrug)
                    {
                        listFeeOrder.Add(obj);
                    }
                    iReturn = CacheManager.OrderIntegrate.ComfirmExec(this.myPatientInfo, listFeeOrder, nurseStation.ID, order.EndTime, true, isCharge, false);
                    if (iReturn < 0)
                    {
                        if (MessageBox.Show("确认执行医嘱出错！是否继续？\n" + order.Item.Name + " : " + CacheManager.OrderIntegrate.Err, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                        {
                            return iReturn;
                        }
                    }
                }
                if (alNeedFeeExecOrderUnDrug.Count > 0)
                {
                    List<FS.HISFC.Models.Order.ExecOrder> listFeeOrder = new List<FS.HISFC.Models.Order.ExecOrder>();
                    foreach (FS.HISFC.Models.Order.ExecOrder obj in alNeedFeeExecOrderUnDrug)
                    {
                        listFeeOrder.Add(obj);
                    }
                    iReturn = CacheManager.OrderIntegrate.ComfirmExec(this.myPatientInfo, listFeeOrder, nurseStation.ID, order.EndTime, false, isCharge, false);
                    if (iReturn < 0)
                    {
                        if (MessageBox.Show("确认执行医嘱出错！是否继续？\n" + order.Item.Name + " : " + CacheManager.OrderIntegrate.Err, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                        {
                            return iReturn;
                        }
                    }
                }
            }
            return 1;
        }

        #region 组套开立

        //{A5409134-55B5-42d9-A264-25060169A64B}
        private FS.FrameWork.Public.ObjectHelper frequencyHelper = null;

        /// <summary>
        /// 填充组套医嘱内容
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int fillOrder(ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string err = "";
            //if (order.Item.IsPharmacy)
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            //if (order.Item.IsPharmacy)
            {
                order.StockDept.ID = string.Empty;
                if (CacheManager.OrderIntegrate.FillPharmacyItemWithStockDept(ref order, out err) == -1)
                {
                    MessageBox.Show(err);
                    return -1;
                }
            }
            else
            {
                if (CacheManager.OrderIntegrate.FillFeeItem(ref order, out err) == -1)
                {
                    MessageBox.Show(err);
                    return -1;
                }
            }


            if (frequencyHelper == null)
            {
                ArrayList alFrequency = FS.HISFC.Components.Order.Classes.Function.HelperFrequency.ArrayObject.Clone() as ArrayList;
                if (alFrequency != null)
                {
                    this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alFrequency);
                }
            }

            if (order.Frequency == null)
            {
                order.Frequency = new FS.HISFC.Models.Order.Frequency();
            }
            if (string.IsNullOrEmpty(order.Frequency.ID))
            {
                order.Frequency.ID = Classes.Function.GetDefaultFrequencyID();
            }

            FS.FrameWork.Models.NeuObject trueFrequency = this.frequencyHelper.GetObjectFromID(order.Frequency.ID);
            if (trueFrequency != null)
            {
                order.Frequency = (trueFrequency as FS.HISFC.Models.Order.Frequency);
            }

            return 0;
        }

        /// <summary>
        /// 组套开立
        /// </summary>
        /// <param name="alOrder"></param>
        public void AddGroupOrder(ArrayList alOrders)
        {
            ArrayList alHerbal = new ArrayList(); //草药

            string comboID = "";
            int subCombNo = 0;
            FS.HISFC.Models.Order.Inpatient.Order myorder = null;

            try
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
                {
                    myorder = order.Clone();
                    if (myorder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).DoseUnit = myorder.DoseUnit;
                    }

                    myorder.Patient.PVisit.PatientLocation.Dept.ID = CacheManager.LogEmpl.Dept.ID;
                    if (fillOrder(ref myorder) != -1)
                    {
                        if (order.Combo.ID != "" && order.Combo.ID != comboID)//新的
                        {
                            //{3BE26864-0779-4ee1-8D7A-9B1DA4744BF3}
                            if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                            {
                                subCombNo = GetMaxCombNo(order, 0);
                            }
                            else
                            {
                                subCombNo = GetMaxCombNo(order, 1);
                            }
                        }
                        comboID = order.Combo.ID;
                        myorder.SubCombNO = subCombNo;

                        if (myorder.Item.ID == "999")
                        {
                            myorder.ExeDept.ID = "";
                        }

                        if (order.Item.SysClass.ID.ToString() == "PCC") //草药
                        {
                            if (this.fpOrder.ActiveSheetIndex == 0)
                            {
                                MessageBox.Show("项目[" + myorder.Item.Name + "]类别为" + myorder.Item.SysClass.ToString() + "，不可以开立为长期医嘱！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                continue;
                            }
                            alHerbal.Add(order);
                        }
                        else
                        {
                            if (isModifyOrderType)// {45652500-8594-40ac-A92E-FFFEB812655C}
                            {
                                if (myorder.OrderType.IsDecompose)
                                {
                                    if (this.fpOrder.ActiveSheetIndex == 0)
                                    {
                                        //{2D788D8F-D5DB-447d-A3E1-282F0A446F1F}
                                        //if (myorder.FirstUseNum == null || myorder.FirstUseNum == "")
                                        //{
                                        myorder.FirstUseNum = Classes.Function.GetFirstOrderDays(myorder, CacheManager.InOrderMgr.GetDateTimeFromSysDateTime()).ToString();
                                        //}

                                        myorder.GetFlag = "0";
                                        myorder.RowNo = -1;
                                        myorder.PageNo = -1;

                                        this.AddNewOrder(myorder, 0);
                                    }
                                    else
                                    {
                                        #region 长期组套复制成临嘱

                                        try
                                        {
                                            if (myorder.OrderType.IsCharge)
                                            {
                                                myorder.OrderType = SOC.HISFC.BizProcess.Cache.Order.GetOrderType("LZ");
                                            }
                                            else
                                            {
                                                myorder.OrderType = SOC.HISFC.BizProcess.Cache.Order.GetOrderType("ZL");
                                            }

                                            if (myorder.OrderType == null)
                                            {
                                                continue;
                                            }

                                            FS.HISFC.Components.Order.Classes.Function.SetDefaultFrequency(myorder);

                                            if (myorder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                            {
                                                myorder.Unit = ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit;
                                                Classes.Function.ReComputeQty(myorder);
                                            }
                                            else
                                            {
                                                myorder.Unit = myorder.Unit;

                                                myorder.Qty = order.Qty;
                                            }

                                            myorder.GetFlag = "0";
                                            myorder.RowNo = -1;
                                            myorder.PageNo = -1;

                                            this.AddNewOrder(myorder, 1);

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("长期医嘱转换为临时医嘱出错！" + ex.Message);
                                        }
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (this.fpOrder.ActiveSheetIndex == 1)
                                    {
                                        myorder.GetFlag = "0";
                                        myorder.RowNo = -1;
                                        myorder.PageNo = -1;

                                        this.AddNewOrder(myorder, 1);
                                    }
                                    else
                                    {
                                        #region 临时组套复制成长嘱

                                        try
                                        {
                                            if (myorder.OrderType.IsCharge)
                                            {
                                                myorder.OrderType = SOC.HISFC.BizProcess.Cache.Order.GetOrderType("CZ");
                                            }
                                            else
                                            {
                                                myorder.OrderType = SOC.HISFC.BizProcess.Cache.Order.GetOrderType("ZC");
                                            }

                                            if (myorder.OrderType == null)
                                            {
                                                continue;
                                            }

                                            //判断是否可以复制
                                            bool b = false;
                                            string strSysClass = myorder.Item.SysClass.ID.ToString();
                                            myorder.HerbalQty = order.HerbalQty;// {3BEDF8C4-0BC6-494f-B7B9-E94C63399197}
                                            //临时医嘱复制为长嘱，总量为0
                                            if (myorder.Item.ItemType == EnumItemType.UnDrug)
                                            {
                                                myorder.Qty = order.Qty;// {3BEDF8C4-0BC6-494f-B7B9-E94C63399197}
                                            }
                                            else
                                            {
                                                myorder.Qty = order.Qty;// {3BEDF8C4-0BC6-494f-B7B9-E94C63399197}
                                            }

                                            switch (strSysClass)
                                            {
                                                case "UO"://手术
                                                case "PCC"://中草药
                                                case "MC"://会诊
                                                case "MRB"://转床
                                                case "MRD": //转科
                                                case "MRH": //预约出院

                                                    b = false;
                                                    break;
                                                case "UL": //检验
                                                case "UC"://检查
                                                    if (Components.Common.Classes.Function.isUCUCForLong(myorder.Item))
                                                    {
                                                        b = true;
                                                    }
                                                    else
                                                    {
                                                        b = false;
                                                    }
                                                    break;

                                                default:
                                                    FS.HISFC.Components.Order.Classes.Function.SetDefaultFrequency(myorder);
                                                    b = true;
                                                    break;
                                            }
                                            if (b == false)
                                            {
                                                MessageBox.Show("项目[" + myorder.Item.Name + "]类别为" + myorder.Item.SysClass.ToString() + "，不可以开立为长期医嘱！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                continue;
                                            }

                                            //{2D788D8F-D5DB-447d-A3E1-282F0A446F1F}
                                            //if (myorder.FirstUseNum == null || myorder.FirstUseNum == "")
                                            //{
                                            myorder.FirstUseNum = Classes.Function.GetFirstOrderDays(myorder, CacheManager.InOrderMgr.GetDateTimeFromSysDateTime()).ToString();
                                            //}

                                            myorder.GetFlag = "0";
                                            myorder.RowNo = -1;
                                            myorder.PageNo = -1;

                                            this.AddNewOrder(myorder, 0);
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("临时医嘱转换为长期医嘱出错！" + ex.Message);
                                        }

                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                if (myorder.OrderType.IsDecompose)// {45652500-8594-40ac-A92E-FFFEB812655C}
                                {

                                    this.fpOrder.ActiveSheetIndex = 0;
                                    if (myorder.FirstUseNum == null || myorder.FirstUseNum == "")
                                    {
                                        myorder.FirstUseNum = Classes.Function.GetFirstOrderDays(myorder, CacheManager.InOrderMgr.GetDateTimeFromSysDateTime()).ToString();
                                    }

                                    myorder.GetFlag = "0";
                                    myorder.RowNo = -1;
                                    myorder.PageNo = -1;

                                    this.AddNewOrder(myorder, 0);
                                }
                                else
                                {
                                    this.fpOrder.ActiveSheetIndex = 1;
                                    myorder.GetFlag = "0";
                                    myorder.RowNo = -1;
                                    myorder.PageNo = -1;

                                    this.AddNewOrder(myorder, 1);
                                }
                            }
                        }
                    }
                }

                if (alHerbal.Count > 0)
                {
                    this.AddHerbalOrders(alHerbal);
                }
                Classes.Function.ShowBalloonTip(3, "提示", "请注意检查执行科室是否正确！", ToolTipIcon.Info);
                this.RefreshCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion

        #region 菜单
        /// <summary>
        /// 去当前行医嘱的TempID
        /// </summary>
        /// <returns></returns>
        public string ActiveTempID
        {
            get
            {
                return this.fpOrder.ActiveSheet.ActiveRowIndex.ToString();
            }
        }

        /// <summary>
        /// 当前选择行
        /// </summary>
        int ActiveRowIndex = -1;

        /// <summary>
        /// 为右键添加菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpOrder_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.bIsShowPopMenu && e.Button == MouseButtons.Right)
            {
                try
                {
                    this.contextMenu1.Items.Clear();
                }
                catch { }

                if (!Classes.HistoryOrderClipboard.isReaded)
                {
                    if (this.bIsDesignMode) //设计情况
                    {
                        if (this.EditGroup == false)//非组套模式
                        {
                            #region 粘贴医嘱
                            ToolStripMenuItem mnuPasteOrder = new ToolStripMenuItem("粘贴医嘱");
                            mnuPasteOrder.Click += new EventHandler(mnuPasteOrder_Click);
                            this.contextMenu1.Items.Add(mnuPasteOrder);
                            this.contextMenu1.Show(this.fpOrder, new Point(e.X, e.Y));
                            #endregion
                        }
                    }
                }

                if (this.fpOrder.ActiveSheet.RowCount <= 0)
                {
                    return;
                }

                #region 记录勾选的行

                string rows = "";

                for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
                {
                    if (this.fpOrder.ActiveSheet.IsSelected(i, 0))
                    {
                        rows += "$" + i.ToString() + "|";
                    }
                }
                #endregion

                FarPoint.Win.Spread.Model.CellRange c = fpOrder.GetCellFromPixel(0, 0, e.X, e.Y);
                if (c.Row >= 0)
                {
                    this.fpOrder.ActiveSheet.ClearSelection();
                    this.fpOrder.ActiveSheet.ActiveRowIndex = c.Row;
                    this.fpOrder.ActiveSheet.AddSelection(c.Row, 0, 1, 1);
                    ActiveRowIndex = c.Row;
                }

                for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
                {
                    if (rows.Contains("$" + i.ToString() + "|") && !this.fpOrder.ActiveSheet.IsSelected(i, 0))
                    {
                        this.fpOrder.ActiveSheet.AddSelection(i, 0, 1, 1);
                    }
                }

                if (ActiveRowIndex < 0)
                {
                    return;
                }

                FS.HISFC.Models.Order.Inpatient.Order mnuSelectedOrder = null;
                mnuSelectedOrder = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[ActiveRowIndex].Tag;

                if (this.bIsDesignMode) //设计情况
                {
                    if (this.EditGroup == false)//非组套模式
                    {
                        #region 停止菜单
                        ToolStripMenuItem mnuDel = new ToolStripMenuItem();//停止
                        mnuDel.Click += new EventHandler(mnuDel_Click);
                        //ToolStripMenuItem mnuCancel = new ToolStripMenuItem();//取消
                        //mnuCancel.Click += new EventHandler(mnuCancel_Click);
                        ToolStripMenuItem mnuBack = new ToolStripMenuItem();//取消
                        mnuBack.Click += new EventHandler(mnuBack_Click); ;

                        if (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 5)
                        {
                            if (!CheckOrderCanMove(mnuSelectedOrder))
                            {
                                if (mnuSelectedOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                                {
                                    mnuBack.Text = "停止医嘱[" + mnuSelectedOrder.Item.Name + "]";
                                }
                                else
                                {
                                    mnuBack.Text = "作废医嘱[" + mnuSelectedOrder.Item.Name + "]";
                                }
                                this.contextMenu1.Items.Add(mnuBack);//取消
                            }
                            else
                            {
                                //开立
                                mnuDel.Text = "删除医嘱[" + mnuSelectedOrder.Item.Name + "]";
                                this.contextMenu1.Items.Add(mnuDel);//删除、作废
                            }

                            //mnuBack.Text = "取消医嘱[" + mnuSelectedOrder.Item.Name + "]";
                            //this.contextMenu1.Items.Add(mnuBack);//取消
                        }
                        else
                        {
                            if (mnuSelectedOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                            {
                                if (mnuSelectedOrder.Status != 3)
                                {
                                    mnuDel.Text = "停止医嘱[" + mnuSelectedOrder.Item.Name + "]";
                                    this.contextMenu1.Items.Add(mnuDel);//删除、作废

                                    //mnuCancel.Text = "取消医嘱[" + mnuSelectedOrder.Item.Name + "]";
                                    //this.contextMenu1.Items.Add(mnuCancel);//取消
                                }
                            }
                            else
                            {
                                //Edit By liangjz  对临嘱已执行的可以作废医嘱
                                if (mnuSelectedOrder.Status == 1 || mnuSelectedOrder.Status == 2)
                                {
                                    mnuDel.Text = "作废医嘱[" + mnuSelectedOrder.Item.Name + "]";
                                    this.contextMenu1.Items.Add(mnuDel);//删除、作废
                                }
                            }
                        }


                        if (mnuSelectedOrder.Status == 3 || mnuSelectedOrder.Status == 4)
                        {
                            mnuDel.Enabled = false;
                            mnuBack.Enabled = false;
                            //mnuCancel.Enabled = false;
                        }

                        #endregion

                        #region 取消停止

                        if (mnuSelectedOrder.Status == 3 || mnuSelectedOrder.EndTime > new DateTime(2000, 1, 1))
                        {
                            ToolStripMenuItem mnuRollBack = new ToolStripMenuItem();//取消停止

                            if (mnuSelectedOrder.OrderType.IsDecompose)
                            {
                                mnuRollBack.Text = mnuSelectedOrder.Status == 3 ? "取消停止医嘱" : "取消预停止医嘱";
                                mnuRollBack.Click += new EventHandler(mnuRollBack_Click);
                                this.contextMenu1.Items.Add(mnuRollBack);
                            }
                            else
                            {
                                mnuRollBack.Text = mnuSelectedOrder.Status == 3 ? "取消作废医嘱" : "取消预作废医嘱";
                                mnuRollBack.Click += new EventHandler(mnuRollBack_Click);
                                this.contextMenu1.Items.Add(mnuRollBack);
                            }
                        }

                        #endregion

                        #region 医嘱类型修改
                        if (mnuSelectedOrder.Status == 0)
                        {
                            ToolStripMenuItem menuChange = new ToolStripMenuItem();
                            menuChange.Click += new EventHandler(menuChange_Click);
                            menuChange.Text = "修改" + "[" + mnuSelectedOrder.Item.Name + "]医嘱类型";
                            //if (mnuSelectedOrder.Item.Price == 0)
                            //    menuChange.Enabled = false;
                            //else
                            //    menuChange.Enabled = true;
                            this.contextMenu1.Items.Add(menuChange);
                        }
                        #endregion

                        #region 医嘱首日量修改
                        //长嘱且审核前
                        if (mnuSelectedOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG && mnuSelectedOrder.Status == 0)
                        {
                            ToolStripMenuItem menuFirstDayChange = new ToolStripMenuItem();
                            menuFirstDayChange.Click += new EventHandler(menuFirstDayChange_Click);
                            menuFirstDayChange.Text = "修改" + "[" + mnuSelectedOrder.Item.Name + "]首日量";
                            this.contextMenu1.Items.Add(menuFirstDayChange);
                        }
                        #endregion

                        #region 医生审核下级医生开立医嘱
                        if (mnuSelectedOrder.Status == 5)
                        {
                            //先取消
                            //ToolStripMenuItem menuCheckOrder = new ToolStripMenuItem();
                            //menuCheckOrder.Click += new EventHandler(menuCheckOrder_Click);
                            //menuCheckOrder.Text = "审核医嘱";

                            //this.contextMenu1.Items.Add(menuCheckOrder);
                        }
                        #endregion

                        if (mnuSelectedOrder.Status != 3)
                        {
                            ToolStripMenuItem mnuPacsPrint = new ToolStripMenuItem();//精麻处方打印 
                            mnuPacsPrint.Click += new EventHandler(mnuPacsPrint_Click);
                            mnuPacsPrint.Text = "检查申请单打印";
                            this.contextMenu1.Items.Add(mnuPacsPrint);
                        }

                        #region 精麻处方打印 //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}

                        ToolStripMenuItem mnuOrderST = new ToolStripMenuItem();//精麻处方打印 
                        mnuOrderST.Click += new EventHandler(mnuOrderST_Click);
                        mnuOrderST.Text = "处方打印";
                        this.contextMenu1.Items.Add(mnuOrderST);

                        #endregion

                        #region 重点品种全麻和精二处方打印

                        ToolStripMenuItem mnuOrderST1 = new ToolStripMenuItem();//重点品种全麻处方打印 
                        mnuOrderST1.Click += new EventHandler(mnuOrderST1_Click);
                        mnuOrderST1.Text = "重点品种全麻处方打印";
                        this.contextMenu1.Items.Add(mnuOrderST1);

                        ToolStripMenuItem mnuOrderST2 = new ToolStripMenuItem();//重点品种精二处方打印 
                        mnuOrderST2.Click += new EventHandler(mnuOrderST2_Click);
                        mnuOrderST2.Text = "重点品种精二处方打印";
                        this.contextMenu1.Items.Add(mnuOrderST2);

                        #endregion
                    }

                    #region 复制医嘱

                    ToolStripMenuItem mnuCopy = new ToolStripMenuItem();//复制医嘱为另一个类型
                    mnuCopy.Click += new EventHandler(mnuCopyAsOtherType_Click);
                    if (this.OrderType == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        mnuCopy.Text = "复制选中医嘱为同类型临嘱";
                    }
                    else
                    {
                        mnuCopy.Text = "复制选中医嘱为同类型长嘱";
                    }

                    this.contextMenu1.Items.Add(mnuCopy);

                    ToolStripMenuItem mnuCopyAs = new ToolStripMenuItem();//复制医嘱为本类型
                    mnuCopyAs.Click += new EventHandler(mnuCopyAsSameType_Click);
                    if (this.OrderType == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        mnuCopyAs.Text = "复制选中医嘱为同类型长嘱";
                    }
                    else
                    {
                        mnuCopyAs.Text = "复制选中医嘱为同类型临嘱";
                    }
                    this.contextMenu1.Items.Add(mnuCopyAs);
                    #endregion

                    #region 上移
                    ToolStripMenuItem mnuUp = new ToolStripMenuItem("上移动");//上移动
                    mnuUp.Click += new EventHandler(mnuUp_Click);
                    if (this.fpOrder.ActiveSheet.ActiveRowIndex <= 0) mnuUp.Enabled = false;
                    this.contextMenu1.Items.Add(mnuUp);
                    #endregion

                    #region 下移
                    ToolStripMenuItem mnuDown = new ToolStripMenuItem("下移动");//下移动
                    mnuDown.Click += new EventHandler(mnuDown_Click);
                    if (this.fpOrder.ActiveSheet.ActiveRowIndex >= this.fpOrder.ActiveSheet.RowCount - 1 || this.fpOrder.ActiveSheet.ActiveRowIndex < 0) mnuDown.Enabled = false;
                    this.contextMenu1.Items.Add(mnuDown);
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
                            MessageBox.Show(IReasonableMedicine.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            ToolStripMenuItem m_passItem = null;
                            ToolStripMenuItem m_passItemSecond = null;

                            if (alMenu != null && alMenu.Count > 0)
                            {
                                this.contextMenu1.Items.Add(menuPass);
                            }

                            int j = 0;
                            foreach (TreeNode node in alMenu)
                            {
                                m_passItem = new ToolStripMenuItem(node.Text);
                                m_passItem.Click += new EventHandler(mnuPass_Click);
                                menuPass.DropDownItems.Insert(i, m_passItem);

                                if (node.Tag == null)
                                {
                                    foreach (TreeNode secondNode in node.Nodes)
                                    {
                                        m_passItemSecond = new ToolStripMenuItem(secondNode.Text);
                                        m_passItemSecond.Click += new EventHandler(mnuPass_Click);
                                        m_passItem.DropDownItems.Insert(j, m_passItemSecond);
                                        j += 1;
                                    }
                                }
                                menuPass.DropDownItems.Insert(i, m_passItem);
                                i += 1;
                            }
                        }
                    }
                    #endregion

                    #region 修改扩展信息
                    //{EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                    if (this.Patient != null
                        && (this.Patient.Pact.PayKind.ID == "02"
                            || CacheManager.ConManager.GetConstant("PactDllName", this.Patient.Pact.PactDllName) != null)//
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
                    //{5D9302B2-9B71-4530-86EA-350063AF56F0}
                    if (!this.EditGroup)
                    {
                        #region 非开立界面下菜单显示
                        ToolStripMenuItem mnuTip = new ToolStripMenuItem("批注");//批注
                        mnuTip.Click += new EventHandler(mnuTip_Click);
                        this.contextMenu1.Items.Add(mnuTip);

                        ToolStripMenuItem mnuTot = new ToolStripMenuItem("累计用量查询");//累计用量
                        mnuTot.Visible = false;//暂时先不用
                        mnuTot.Click += new EventHandler(mnuTot_Click);

                        try
                        {
                            string OrderID = this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["医嘱流水号"]].Text;

                            if (CacheManager.InOrderMgr.QueryOneOrder(OrderID).Item.ItemType == EnumItemType.Drug)
                            {
                                this.contextMenu1.Items.Add(mnuTot);
                            }
                        }
                        catch { }
                        #endregion
                    }
                }
                this.contextMenu1.Show(this.fpOrder, new Point(e.X, e.Y));
            }
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

                //{9088AFD6-93A7-4d72-BC1D-8969F28C1511}
                if (pha != null)
                {

                    return pha.UserCode;
                }
            }
            else
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);

                //{9088AFD6-93A7-4d72-BC1D-8969F28C1511}
                if (undrug != null)
                {
                    return undrug.UserCode;
                }
            }

            return "";
        }

        /// <summary>
        /// 修改医保限制性用药信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuEditIndications_Click(object sender, EventArgs e)
        {
            int i = this.fpOrder.ActiveSheet.ActiveRowIndex;
            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("请先选择一条医嘱！", "提示");
                return;
            }
            FS.HISFC.Models.Order.Inpatient.Order order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[i].Tag;

            if (order != null)
            {
                if (string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("请先保存医嘱！", "提示");
                    return;
                }
                FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtObj = orderExtMgr.QueryByInpatineNoOrderID(myPatientInfo.ID, order.ID);
                FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(this.GetItemUserCode(order.Item));
                if (indicationsObj == null)
                {
                    MessageBox.Show("该药品非限制用药，无法选择！", "提示");
                    return;
                }
                if (MessageBox.Show("药品【" + order.Item.Name + "】属于限制级药品，\r\n\r\n限制药品说明：【" + indicationsObj.Name + "】\r\n\r\n请确定医保报销设定。报销(是)，自费(否)?\r\n", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (orderExtObj == null)
                    {
                        orderExtObj = new FS.HISFC.Models.Order.Inpatient.OrderExtend();
                    }
                    orderExtObj.InPatientNo = this.myPatientInfo.ID;
                    orderExtObj.Indications = "1";
                    orderExtObj.MoOrder = order.ID;
                    orderExtObj.Oper.ID = orderExtMgr.Operator.ID;
                }
                else
                {
                    if (orderExtObj == null)
                    {
                        orderExtObj = new FS.HISFC.Models.Order.Inpatient.OrderExtend();
                        orderExtObj.InPatientNo = myPatientInfo.ID;
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
        /// 取消医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //[Obsolete("作废，统一使用delete", true)]
        void mnuBack_Click(object sender, EventArgs e)
        {
            // TODO:  添加 ucOrder.Del 实现
            int i = this.fpOrder.ActiveSheet.ActiveRowIndex;
            DialogResult r;
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("请先选择一条医嘱！");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[i].Tag;

            FS.HISFC.Models.Order.Inpatient.Order tmpOrder = CacheManager.InOrderMgr.QueryOneOrder(order.ID);

            if (tmpOrder == null || tmpOrder.PageNo < 0 || tmpOrder.RowNo < 0)
            {
                MessageBox.Show("该医嘱并未打印，请直接删除！");
                return;
            }
            //审核过的作废或停止
            string strTip = "";
            //长嘱审核过、执行过的都可以停止
            if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
            {
                strTip = "取消";
            }
            else
            {
                strTip = "取消";
            }
            r = MessageBox.Show("是否" + strTip + "该医嘱[" + order.Item.Name + "]?\n *此操作不能撤消！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (r == DialogResult.OK)
            {
                //弹出停止窗口

                Forms.frmDCOrder f = new FS.HISFC.Components.Order.Forms.frmDCOrder();
                f.ShowDialog();
                if (f.DialogResult != DialogResult.OK) return;


                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Order.Inpatient.Order orderTemp = null;
                for (int j = 0; j < this.fpOrder.ActiveSheet.RowCount; j++)
                {
                    orderTemp = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[j].Tag;

                    if (!string.IsNullOrEmpty(orderTemp.ID) && orderTemp.Combo.ID == order.Combo.ID)
                    {
                        orderTemp.Status = 3;
                        orderTemp.DCOper.OperTime = f.DCDateTime;
                        //order.DcReason = f.DCReason.Clone();
                        orderTemp.DcReason.ID = f.DCReason.ID;
                        orderTemp.DcReason.Name = f.DCReason.Name;
                        orderTemp.DCOper.ID = CacheManager.InOrderMgr.Operator.ID;
                        orderTemp.DCOper.Name = CacheManager.InOrderMgr.Operator.Name;
                        orderTemp.EndTime = order.DCOper.OperTime;


                        #region 停止医嘱
                        if (CacheManager.InOrderMgr.DcOneOrder(orderTemp) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(CacheManager.InOrderMgr.Err);
                            return;
                        }

                        #endregion


                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["医嘱状态"]].Value = orderTemp.Status;
                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["停止时间"]].Value = orderTemp.DCOper.OperTime;
                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["停止时间"]].Value = orderTemp.EndTime;
                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["停止人编码"]].Text = orderTemp.DCOper.ID;
                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["停止人"]].Text = orderTemp.DCOper.Name;
                        this.fpOrder.ActiveSheet.Rows[j].Tag = orderTemp;



                    }

                }

                if (CacheManager.InOrderMgr.DeleteOrderSubtbl(order.Combo.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(CacheManager.InOrderMgr.Err);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                //					MessageBox.Show("医嘱已经状态已经变化，请刷新屏幕！");
                return;
            }
            this.RefreshOrderState(true);

            return;
        }
        /// <summary>
        /// 清除右键菜单内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpOrder_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                this.contextMenu1.Items.Clear();
            }
            catch { }
        }

        /// <summary>
        /// 删除，作废、停止医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDel_Click(object sender, EventArgs e)
        {
            this.Delete();
        }

        /// <summary>
        /// 取消医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete("作废，统一使用delete", true)]
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            this.Delete();
        }

        /// <summary>
        /// 取消停止医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuRollBack_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;

            #region 判断是否符合取消停止

            DateTime sysTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
            string Msg = "";

            for (int j = this.fpOrder.ActiveSheet.RowCount - 1; j >= 0; j--)
            {
                if (!this.fpOrder.ActiveSheet.IsSelected(j, 0))
                {
                    continue;
                }
                order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[j].Tag;
                if (order == null)
                {
                    return;
                }

                if (order.Status == 3)
                {
                    if (order.EndTime.AddDays(1) < sysTime)
                    {
                        MessageBox.Show("医嘱[" + order.Item.Name + "]停止时间超过一天，不允许取消停止！");
                        return;
                    }

                    string rev = CacheManager.InOrderMgr.GetDCConfirmFlag(order.ID);
                    //查不到默认为已审核
                    if (rev == null)
                    {
                        MessageBox.Show(CacheManager.InOrderMgr.Err);
                        return;
                    }
                    if (string.IsNullOrEmpty(rev))
                    {
                        rev = "1";
                    }
                    bool isConfiremed = FS.FrameWork.Function.NConvert.ToBoolean(rev);

                    //没审核的才允许取消退费或者修改停止时间
                    if (isConfiremed)
                    {
                        Msg += "《" + order.Item.Name + "》、";
                    }
                }
                else if (order.Status == 4)
                {
                    Msg += "《" + order.Item.Name + "》为重整医嘱、";
                }
            }

            if (!string.IsNullOrEmpty(Msg))
            {
                MessageBox.Show("医嘱" + Msg + "\n 护士已经审核，不能取消停止！");
                return;
            }

            #endregion

            Hashtable hsTemp = new Hashtable();
            if (this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show(this, "请先选择一条医嘱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string comboID = "";

            if (MessageBox.Show(this, "是否取消停止该医嘱[" + order.Item.Name + "]?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            for (int j = this.fpOrder.ActiveSheet.RowCount - 1; j >= 0; j--)
            {
                if (!this.fpOrder.ActiveSheet.IsSelected(j, 0))
                {
                    continue;
                }
                order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[j].Tag;
                if (order == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

                //组合项目只判断一个就行，因为是组合一起操作的
                if (comboID == order.Combo.ID)
                {
                    continue;
                }
                else
                {
                    comboID = order.Combo.ID;
                }

                if (order.Status == 3 || order.EndTime > DateTime.MinValue)
                {
                    if (this.CancelStopOrder(order, true, ref Msg) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //if (this.passEnable)
                    //{
                    //    DTRationalDrug.RationalInpatientDrug(null, RationalType.Refresh, "");
                    //}
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("医嘱不是作废状态，不能取消作废!"));
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            this.SendMessage(SendType.RollBackCancel);

            this.RefreshOrderState(-1);

            return;
        }

        /// <summary>
        /// 检查申请单打印
        /// {A2ACD07E-03C1-4b5e-B6B1-7F8DE370C256}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuPacsPrint_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.ActiveSheet.ActiveRow.Index < 0)
            {
                MessageBox.Show("请选择一条医嘱！");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.ActiveRow.Tag;
            {
                if (order == null || string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("请选择有效医嘱！");
                    return;
                }
            }

            bool IsPscsInUse = ctlMgr.QueryControlerInfo("PSCS01") == "1";

            string istemp = "";

            if (IsPscsInUse)
            {
                FS.FrameWork.Models.NeuObject objt = constantMgr.GetConstant("PSCSINFORMED", order.Item.ID);
                if (objt != null && !string.IsNullOrEmpty(objt.ID))
                {
                    istemp = objt.Memo;
                }
            }

            if (!string.IsNullOrEmpty(istemp))
            {
                FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint orderPrint = new FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.PacsBillPrint.ucPacsInformedBillPrintIBORNA4();
                string where = " where met_ipm_order.mo_order='{0}'";
                where = string.Format(where, order.ID);
                FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTemp = ordMgr.QueryOrderBase(where);
                FS.FrameWork.Models.NeuObject reciptDept = new FS.FrameWork.Models.NeuObject();
                FS.FrameWork.Models.NeuObject reciptDoct = new FS.FrameWork.Models.NeuObject();
                reciptDept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                reciptDept.Name = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name;
                reciptDoct = FS.FrameWork.Management.Connection.Operator;
                if (alOrderTemp.Count > 0)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order item in alOrderTemp)
                    {
                        item.RefundReason = istemp;
                    }
                    this.myPatientInfo.SIMainInfo.User03 = "PACS打印";
                    orderPrint.PrintInPatientOrderBill(this.myPatientInfo, "", reciptDept, reciptDoct, alOrderTemp, false);
                    this.myPatientInfo.SIMainInfo.User03 = "";
                }
            }
            else
            {

                if (this.IInPatientOrderPrint == null)
                {
                    this.IInPatientOrderPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder),
                        typeof(FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint)) as FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint;
                }
                if (this.IInPatientOrderPrint != null)
                {
                    string where = " where met_ipm_order.mo_order='{0}'";
                    where = string.Format(where, order.ID);
                    FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                    ArrayList alOrderTemp = ordMgr.QueryOrderBase(where);
                    FS.FrameWork.Models.NeuObject reciptDept = new FS.FrameWork.Models.NeuObject();
                    FS.FrameWork.Models.NeuObject reciptDoct = new FS.FrameWork.Models.NeuObject();
                    reciptDept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    reciptDept.Name = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name;
                    reciptDoct = FS.FrameWork.Management.Connection.Operator;
                    if (alOrderTemp.Count > 0)
                    {
                        this.myPatientInfo.SIMainInfo.User03 = "PACS打印";
                        this.IInPatientOrderPrint.PrintInPatientOrderBill(this.myPatientInfo, "", reciptDept, reciptDoct, alOrderTemp, false);
                        this.myPatientInfo.SIMainInfo.User03 = "";
                    }

                }
            }
        }

        /// <summary>
        /// 精麻方打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuOrderST_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.ActiveSheet.ActiveRow.Index < 0)
            {
                MessageBox.Show("请选择一条医嘱！");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.ActiveRow.Tag;
            {
                if (order == null || string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("请选择有效医嘱！");
                    return;
                }
            }
            if (this.IRecipePrintST == null)
            {
                this.IRecipePrintST = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder),
                    typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST)) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST;
            }
            if (this.IRecipePrintST != null)
            {
                string where = " where met_ipm_order.mo_order='{0}'  and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ( 'P1','P2','S1','SY','O1') and nvl(b.SPECIAL_FLAG4,'0')!='13')";
                where = string.Format(where, order.ID);
                FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTemp = ordMgr.QueryOrderBase(where);
                if (alOrderTemp.Count > 0)
                {
                    //临时这样写一下，后面看看是加接口还是怎么样吧
                    //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}
                    //FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST ucRecipePrintST = new FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST();
                    //ucRecipePrintST.MakaLabel(ucRecipePrintST.ChangeOrderToOrderST(alOrderTemp));
                    //ucRecipePrintST.SetPatientInfo(this.myPatientInfo);
                    //ucRecipePrintST.PrintRecipe();
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    this.IRecipePrintST.OnInPatientPrint(this.myPatientInfo, obj, obj, alOrderTemp, alOrderTemp, false, false, "", obj);
                }
                else
                {
                    MessageBox.Show("此医嘱非精麻处方！");
                    return;
                }
            }
        }

        /// <summary>
        /// 重点品种全麻处方打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuOrderST1_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.ActiveSheet.ActiveRow.Index < 0)
            {
                MessageBox.Show("请选择一条医嘱！");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.ActiveRow.Tag;
            {
                if (order == null || string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("请选择有效医嘱！");
                    return;
                }
            }
            if (this.IRecipePrintST == null)
            {
                this.IRecipePrintST = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder),
                    typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST)) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST;
            }
            if (this.IRecipePrintST != null)
            {
                string where = " where met_ipm_order.mo_order='{0}'  and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ('Q') and  nvl(b.SPECIAL_FLAG4,'0')='13')";
                where = string.Format(where, order.ID);
                FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTemp = ordMgr.QueryOrderBase(where);
                if (alOrderTemp.Count > 0)
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    this.IRecipePrintST.OnInPatientPrint(this.myPatientInfo, obj, obj, alOrderTemp, alOrderTemp, false, false, "", obj);
                }
                else
                {
                    MessageBox.Show("此医嘱非重点品种全麻处方！");
                    return;
                }
            }
        }

        /// <summary>
        /// 重点品种精二处方打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuOrderST2_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.ActiveSheet.ActiveRow.Index < 0)
            {
                MessageBox.Show("请选择一条医嘱！");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.ActiveRow.Tag;
            {
                if (order == null || string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("请选择有效医嘱！");
                    return;
                }
            }
            if (this.IRecipePrintST == null)
            {
                this.IRecipePrintST = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder),
                    typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST)) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST;
            }
            if (this.IRecipePrintST != null)
            {
                string where = " where met_ipm_order.mo_order='{0}'  and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ('P2') and  nvl(b.SPECIAL_FLAG4,'0')='13')";
                where = string.Format(where, order.ID);
                FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTemp = ordMgr.QueryOrderBase(where);
                if (alOrderTemp.Count > 0)
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    this.IRecipePrintST.OnInPatientPrint(this.myPatientInfo, obj, obj, alOrderTemp, alOrderTemp, false, false, "", obj);
                }
                else
                {
                    MessageBox.Show("此医嘱非重点品种精二处方！");
                    return;
                }
            }
        }

        /// <summary>
        /// 取消停止医嘱  组合一起停止
        /// </summary>
        /// <param name="order"></param>
        /// <param name="temp"></param>
        /// <param name="isDeleteCombo">是否删除一组</param>
        /// <returns></returns>
        private int CancelStopOrder(FS.HISFC.Models.Order.Inpatient.Order order, bool isDeleteCombo, ref string errInfo)
        {
            ArrayList alTemp = new ArrayList();
            try
            {
                FS.HISFC.Models.Order.Inpatient.Order temp = new FS.HISFC.Models.Order.Inpatient.Order();

                //此处处理组合一起停止
                if (CacheManager.InOrderMgr.CancelDcOrder(order, isDeleteCombo) == -1)
                {
                    errInfo = CacheManager.InOrderMgr.Err;
                    return -1;
                }

                //组合停止的时候，刷新组合项目显示
                if (isDeleteCombo)
                {
                    for (int row = 0; row < this.fpOrder.ActiveSheet.RowCount; row++)
                    {
                        temp = this.fpOrder.ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                        //组合中的其他项目，只刷新停止信息
                        if (temp.Combo.ID == order.Combo.ID)
                        {
                            temp = CacheManager.InOrderMgr.QueryOneOrder(temp.ID);

                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["医嘱状态"]].Value = temp.Status;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止时间"]].Value = "";
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["结束时间"]].Value = "";
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止人编码"]].Text = temp.DCOper.ID;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["停止人"]].Text = temp.DCOper.Name;

                            //此处没有把组号获取出来，重新赋值
                            temp.SubCombNO = order.SubCombNO;
                            this.fpOrder.ActiveSheet.Rows[row].Tag = temp;

                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTip_Click(object sender, EventArgs e)
        {
            ucTip ucTip1 = new ucTip();
            ucTip1.IsCanModifyHypotest = false;
            string OrderID = this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["医嘱流水号"]].Text;
            int iHypotest = CacheManager.InOrderMgr.QueryOrderHypotest(OrderID);
            if (iHypotest == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            try
            {
                ucTip1.Tip = this.fpOrder.ActiveSheet.GetNote(this.ActiveRowIndex, dicColmSet["医嘱名称"]).ToString();
            }
            catch { }
            int i = dicColmSet["医嘱状态"];
            int state = FS.FrameWork.Function.NConvert.ToInt32(this.fpOrder.ActiveSheet.Cells[fpOrder_Long.ActiveRowIndex, i].Text);
            if (state != 0)
            {
                ucTip1.btnCancel.Enabled = false;
                ucTip1.btnSave.Enabled = false;
            }
            ucTip1.Hypotest = iHypotest;
            ucTip1.OKEvent += new myTipEvent(ucTip1_OKEvent);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucTip1);
        }

        /// <summary>
        /// 复制医嘱  由长嘱复制为临嘱或临嘱复制为长嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyAsOtherType_Click(object sender, EventArgs e)
        {
            if (this.fpOrder.ActiveSheet.RowCount <= 0)
            {
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order tempOrder = null;

            string combNo = "";

            #region 获取需要复制的医嘱组合中的一个药品

            ArrayList alCombOrder = new ArrayList();

            for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
            {
                if (!this.fpOrder.ActiveSheet.IsSelected(i, 0))
                {
                    continue;
                }

                tempOrder = this.fpOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (tempOrder == null)
                {
                    return;
                }

                if (combNo != tempOrder.Combo.ID)
                {
                    combNo = tempOrder.Combo.ID;
                    alCombOrder.Add(tempOrder.Clone());
                }
                else
                {
                    continue;
                }
            }
            #endregion

            #region 按照组合挨个复制

            ArrayList alCopyOrders = null;

            //判断缺药、停用
            FS.HISFC.Models.Pharmacy.Item itemObj = null;
            string errInfo = "";
            FS.HISFC.Models.Order.OrderType ordertype = null;

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alCombOrder)
            {
                alCopyOrders = new ArrayList();

                #region 获取新医嘱组合号
                string ComboNo;
                try
                {
                    ComboNo = CacheManager.InOrderMgr.GetNewOrderComboID();
                    if (ComboNo == null || ComboNo == "")
                    {
                        MessageBox.Show("复制医嘱过程中发生错误 获取新医嘱组合号过程中出错" + CacheManager.InOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("复制医嘱过程中发生错误 获取新医嘱组合号过程中出错" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region 获取需要复制的医嘱

                DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    tempOrder = this.GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex).Clone();
                    if (tempOrder == null)
                        continue;

                    if (this.isNurseCreate)
                    {
                        if (tempOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            MessageBox.Show("护士不允许复制他人开立的药品!");
                            return;
                        }
                    }

                    if (tempOrder.Combo.ID == order.Combo.ID)
                    {
                        tempOrder.Patient = this.myPatientInfo.Clone();

                        if (tempOrder.Item.ID == "999"
                            || tempOrder.ExeDept.ID == tempOrder.ReciptDept.ID)
                        {
                            tempOrder.ExeDept.ID = "";
                        }

                        #region 药品、非药品项目赋值

                        if (tempOrder.Item.ItemType == EnumItemType.Drug)
                        {
                            tempOrder.StockDept.ID = string.Empty;
                            if (CacheManager.OrderIntegrate.FillPharmacyItemWithStockDept(ref tempOrder, out errInfo) == -1)
                            {
                                MessageBox.Show(errInfo);
                                return;
                            }
                            if (tempOrder == null)
                            {
                                return;
                            }


                            if (tempOrder.Item.ID != "999" && tempOrder.OrderType.IsCharge)
                            {
                                if (Components.Order.Classes.Function.CheckDrugState(this.Patient, tempOrder.StockDept,
                                    tempOrder.Item, false, ref itemObj, ref errInfo) == -1)
                                {
                                    MessageBox.Show(errInfo);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (CacheManager.OrderIntegrate.FillFeeItem(ref tempOrder, out errInfo) == -1)
                            {
                                MessageBox.Show(errInfo);
                                return;
                            }
                            if (tempOrder == null)
                                return;
                        }
                        #endregion

                        #region 医嘱基本信息赋值

                        tempOrder.OrderType.IsDecompose = !tempOrder.OrderType.IsDecompose;//长期临时互换
                        ordertype = tempOrder.OrderType;

                        if (tempOrder.Item.Price == 0)
                        {
                            Classes.OrderType.CheckChargeableOrderType(ref ordertype, false, true);
                        }
                        else
                        {
                            Classes.OrderType.CheckChargeableOrderType(ref ordertype, true, true);
                        }

                        tempOrder.OrderType = ordertype;

                        if (!tempOrder.OrderType.IsDecompose && object.Equals(tempOrder.Frequency, null))
                        {
                            tempOrder.Frequency = Classes.Function.GetDefaultFrequency();
                        }

                        tempOrder.Memo = "";
                        tempOrder.Status = 0;
                        tempOrder.ID = "";
                        tempOrder.SortID = 0;
                        tempOrder.Combo.ID = ComboNo;
                        tempOrder.BeginTime = dtNow;
                        tempOrder.EndTime = DateTime.MinValue;
                        tempOrder.DCOper.OperTime = DateTime.MinValue;
                        tempOrder.DcReason.ID = "";
                        tempOrder.DcReason.Name = "";
                        tempOrder.DCOper.ID = "";
                        tempOrder.DCOper.Name = "";
                        tempOrder.ConfirmTime = DateTime.MinValue;
                        tempOrder.Nurse.ID = "";
                        tempOrder.MOTime = dtNow;

                        tempOrder.PageNo = -1;
                        tempOrder.RowNo = -1;
                        tempOrder.GetFlag = "0";

                        tempOrder.ApplyNo = string.Empty;

                        #region  add by liuww 不请空 复制重整医嘱，不算首日量
                        tempOrder.ReTidyInfo = "";
                        #endregion

                        tempOrder.FirstUseNum = Classes.Function.GetFirstOrderDays(tempOrder, dtNow).ToString();

                        if (this.GetReciptDept() != null)
                        {
                            //{BF80E3A9-1238-4ca9-8890-3C64F518F520}
                            tempOrder.ReciptDept = new FS.FrameWork.Models.NeuObject();
                            tempOrder.ReciptDept.ID = this.GetReciptDept().ID;
                            tempOrder.ReciptDept.Name = this.GetReciptDept().Name;
                        }
                        if (this.GetReciptDoc() != null)
                        {
                            //{BF80E3A9-1238-4ca9-8890-3C64F518F520}
                            tempOrder.ReciptDoctor = new FS.FrameWork.Models.NeuObject();
                            tempOrder.ReciptDoctor.ID = this.GetReciptDoc().ID;
                            tempOrder.ReciptDoctor.Name = this.GetReciptDoc().Name;
                        }
                        if (this.GetReciptDoc() != null)
                        {
                            tempOrder.Oper.ID = this.GetReciptDoc().ID;
                            tempOrder.Oper.Name = this.GetReciptDoc().Name;
                        }

                        tempOrder.NextMOTime = tempOrder.BeginTime;
                        tempOrder.CurMOTime = tempOrder.BeginTime;

                        #endregion
                        alCopyOrders.Add(tempOrder.Clone());
                    }
                }
                #endregion

                #region 复制医嘱

                foreach (FS.HISFC.Models.Order.Inpatient.Order copyOrder in alCopyOrders)
                {
                    copyOrder.Item.Name.Replace("[嘱托]", "").Replace("[自备]", "");

                    if (this.fpOrder.ActiveSheetIndex == 0)
                    {
                        #region 长嘱复制为临嘱
                        Classes.Function.SetDefaultFrequency(copyOrder);

                        if (copyOrder.Item.ItemType == EnumItemType.Drug)
                        {
                            #region 复制的时候判断拆分属性

                            //if (itemObj.SplitType.Equals(itemObj.CDSplitType))
                            //{
                            //    //如果可拆分的属性一样
                            //}
                            //else
                            //{
                            //    //如果拆分属性不一样
                            //}


                            #endregion

                            //自动计算临嘱总量 并按最小单位显示 
                            try
                            {
                                copyOrder.Qty = System.Math.Round(copyOrder.DoseOnce / ((FS.HISFC.Models.Pharmacy.Item)copyOrder.Item).BaseDose, 0);
                            }
                            catch
                            {
                                copyOrder.Qty = 0;
                            }


                            copyOrder.Unit = ((FS.HISFC.Models.Pharmacy.Item)copyOrder.Item).MinUnit;//???

                        }

                        try
                        {
                            this.refreshComboFlag = "1";		//只需对临嘱进行组合号刷新即可

                            copyOrder.SubCombNO = this.GetMaxCombNo(copyOrder, 1);

                            #region add by liuww  复制为不同类型的医嘱不重新取分方规则
                            Classes.Function.ReComputeQty(copyOrder);
                            #endregion


                            this.AddNewOrder(copyOrder, 1);//short
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("复制医嘱过程中发生不可预知错误！" + ex.Message + ex.Source);
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        //临时
                        #region 临嘱复制为长嘱

                        //判断是否可以复制
                        bool b = false;
                        string strSysClass = copyOrder.Item.SysClass.ID.ToString();

                        #region 部分临嘱不能复制为长期医嘱

                        bool isCanCopy = false;
                        if (FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(false) != null)
                        {
                            foreach (FS.FrameWork.Models.NeuObject obj in FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(false))
                            {
                                if (obj.ID == strSysClass)
                                {
                                    isCanCopy = true;
                                }
                            }
                        }

                        if (!isCanCopy)
                        {
                            MessageBox.Show("项目[" + copyOrder.Item.Name + "]类别为" + copyOrder.Item.SysClass.Name + "不能复制为长期医嘱!");
                            continue;
                        }
                        #endregion

                        this.refreshComboFlag = "0";//只对长嘱组合进行刷新即可

                        copyOrder.SubCombNO = this.GetMaxCombNo(copyOrder, 0);

                        #region add by liuww  复制为不同类型的医嘱不重新取分方规则
                        Classes.Function.ReComputeQty(copyOrder);
                        #endregion

                        this.AddNewOrder(copyOrder, 0);//long

                        #endregion
                    }
                }
                #endregion

                Classes.Function.ShowBalloonTip(3, "提示", "请注意检查执行科室是否正确！", ToolTipIcon.Info);
            }
            #endregion

            this.RefreshCombo();
        }


        /// <summary>
        /// 复制医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyAsSameType_Click(object sender, EventArgs e)
        {
            if (this.fpOrder.ActiveSheet.RowCount <= 0)
            {
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order tempOrder = null;

            string combNo = "";


            #region 获取需要复制的医嘱组合中的一个药品
            ArrayList alCombOrder = new ArrayList();

            for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
            {
                if (!this.fpOrder.ActiveSheet.IsSelected(i, 0))
                {
                    continue;
                }

                tempOrder = this.fpOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (tempOrder == null)
                {
                    return;
                }

                if (combNo != tempOrder.Combo.ID)
                {
                    combNo = tempOrder.Combo.ID;
                    alCombOrder.Add(tempOrder.Clone());
                }
                else
                {
                    continue;
                }
            }
            #endregion

            #region 按照组合挨个复制

            ArrayList alCopyOrders = null;

            //判断缺药、停用
            FS.HISFC.Models.Pharmacy.Item itemObj = null;
            string errInfo = "";
            FS.HISFC.Models.Order.OrderType ordertype = null;

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alCombOrder)
            {
                alCopyOrders = new ArrayList();

                #region 获取新医嘱组合号
                try
                {
                    combNo = CacheManager.InOrderMgr.GetNewOrderComboID();
                    if (combNo == null || combNo == "")
                    {
                        MessageBox.Show("复制医嘱过程中发生错误 获取新医嘱组合号过程中出错" + CacheManager.InOrderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("复制医嘱过程中发生错误 获取新医嘱组合号过程中出错" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region 获取需要复制的医嘱列表

                DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    tempOrder = this.GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex).Clone();
                    if (tempOrder == null)
                        continue;

                    if (this.isNurseCreate)
                    {
                        if (tempOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            MessageBox.Show("护士不允许复制他人开立的药品!");
                            return;
                        }
                    }

                    if (tempOrder.Combo.ID == order.Combo.ID)
                    {
                        if (this.myPatientInfo != null)
                        {
                            tempOrder.Patient = this.myPatientInfo;
                        }

                        //tempOrder.ExeDept = new FS.FrameWork.Models.NeuObject();
                        if (tempOrder.Item.ID == "999" || tempOrder.ReciptDept.ID == tempOrder.ExeDept.ID)
                        {
                            tempOrder.ExeDept.ID = "";
                        }

                        #region 药品、非药品项目赋值

                        if (tempOrder.Item.ItemType == EnumItemType.Drug)
                        {
                            tempOrder.StockDept.ID = string.Empty;
                            if (CacheManager.OrderIntegrate.FillPharmacyItemWithStockDept(ref tempOrder, out errInfo) == -1)
                            {
                                MessageBox.Show(errInfo);
                                return;
                            }
                            if (tempOrder == null)
                                return;

                            if (tempOrder.Item.ID != "999" && tempOrder.OrderType.IsCharge)
                            {
                                if (Components.Order.Classes.Function.CheckDrugState(this.Patient, tempOrder.StockDept, tempOrder.Item,
                                    false, ref itemObj, ref errInfo) == -1)
                                {
                                    MessageBox.Show(errInfo);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (CacheManager.OrderIntegrate.FillFeeItem(ref tempOrder, out errInfo) == -1)
                            {
                                MessageBox.Show(errInfo);
                                return;
                            }
                            if (tempOrder == null)
                                return;
                        }
                        #endregion

                        #region 医嘱基本信息赋值
                        ordertype = tempOrder.OrderType;

                        //if (tempOrder.Item.Price == 0)
                        //{
                        //    Classes.OrderType.CheckChargeableOrderType(ref ordertype, false);
                        //}
                        //else
                        //{
                        //    Classes.OrderType.CheckChargeableOrderType(ref ordertype, true);
                        //}


                        tempOrder.Item.Name.Replace("[嘱托]", "").Replace("[自备]", "");

                        tempOrder.OrderType = ordertype;
                        tempOrder.Memo = "";
                        tempOrder.Status = 0;
                        tempOrder.ID = "";
                        tempOrder.SortID = 0;
                        tempOrder.Combo.ID = combNo;
                        tempOrder.BeginTime = dtNow;
                        tempOrder.EndTime = DateTime.MinValue;
                        tempOrder.DCOper.OperTime = DateTime.MinValue;
                        tempOrder.DcReason.ID = "";
                        tempOrder.DcReason.Name = "";
                        tempOrder.DCOper.ID = "";
                        tempOrder.DCOper.Name = "";
                        tempOrder.ConfirmTime = DateTime.MinValue;
                        tempOrder.Nurse.ID = "";
                        tempOrder.MOTime = dtNow;

                        tempOrder.PageNo = -1;
                        tempOrder.RowNo = -1;
                        tempOrder.GetFlag = "0";
                        #region add by liuww 不请空 复制重整医嘱，不算首日量
                        tempOrder.ReTidyInfo = "";
                        #endregion
                        tempOrder.FirstUseNum = Classes.Function.GetFirstOrderDays(tempOrder, dtNow).ToString();
                        tempOrder.ApplyNo = string.Empty;

                        if (this.GetReciptDept() != null)
                        {
                            //{BF80E3A9-1238-4ca9-8890-3C64F518F520}
                            tempOrder.ReciptDept = new FS.FrameWork.Models.NeuObject();
                            tempOrder.ReciptDept.ID = this.GetReciptDept().ID;
                            tempOrder.ReciptDept.Name = this.GetReciptDept().Name;
                        }
                        if (this.GetReciptDoc() != null)
                        {
                            //{BF80E3A9-1238-4ca9-8890-3C64F518F520}
                            tempOrder.ReciptDoctor = new FS.FrameWork.Models.NeuObject();
                            tempOrder.ReciptDoctor.ID = this.GetReciptDoc().ID;
                            tempOrder.ReciptDoctor.Name = this.GetReciptDoc().Name;
                        }
                        if (this.GetReciptDoc() != null)
                        {
                            tempOrder.Oper.ID = this.GetReciptDoc().ID;
                            tempOrder.Oper.Name = this.GetReciptDoc().Name;
                        }

                        //手术室开立处理
                        if (!string.IsNullOrEmpty(tempOrder.ReciptDept.ID) && string.IsNullOrEmpty(tempOrder.ExeDept.ID))
                        {
                            if ("1,2".Contains((SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.ReciptDept.ID).SpecialFlag)))
                            {
                                tempOrder.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.ReciptDept.ID);
                            }
                        }

                        tempOrder.NextMOTime = tempOrder.BeginTime;
                        tempOrder.CurMOTime = tempOrder.BeginTime;

                        #endregion

                        alCopyOrders.Add(tempOrder);
                    }
                }
                #endregion

                #region 复制医嘱

                for (int i = 0; i < alCopyOrders.Count; i++)
                {
                    #region 复制为长期医嘱

                    if (this.fpOrder.ActiveSheetIndex == 0)
                    {
                        try
                        {
                            this.refreshComboFlag = "0";			//只需对长嘱组合号进行刷新即可

                            ((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]).SubCombNO = this.GetMaxCombNo(((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]), 0);

                            #region add by liuww  复制为不同类型的医嘱不重新取分方规则
                            Classes.Function.ReComputeQty((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]);
                            #endregion

                            this.AddNewOrder(((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]), 0);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("复制医嘱过程中发生不可预知错误！" + ex.Message + ex.Source);
                            return;
                        }
                    }
                    #endregion

                    #region 复制为临时医嘱

                    else
                    {
                        tempOrder = (alCopyOrders[i] as FS.HISFC.Models.Order.Inpatient.Order).Clone();

                        //临时医嘱复制为长嘱，总量为0
                        tempOrder.Qty = 0;
                        tempOrder.HerbalQty = 0;
                        Classes.Function.SetDefaultFrequency(tempOrder);
                        //临时
                        try
                        {
                            this.refreshComboFlag = "1";			//只需对临嘱组合号进行刷新即可

                            ((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]).SubCombNO = this.GetMaxCombNo(((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]), 1);

                            #region add by liuww  复制为不同类型的医嘱不重新取分方规则
                            Classes.Function.ReComputeQty((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]);
                            #endregion

                            this.AddNewOrder(((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]), 1);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("复制医嘱过程中发生不可预知错误！" + ex.Message + ex.Source);
                            return;
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
            Classes.Function.ShowBalloonTip(3, "提示", "请注意检查执行科室是否正确！", ToolTipIcon.Info);

            this.RefreshCombo();
        }

        /// <summary>
        /// 开立医嘱前的验证
        /// </summary>
        /// <param name="order"></param>
        /// <param name="SheetIndex"></param>
        /// <returns></returns>
        public int ValidOrderBefore(FS.HISFC.Models.Order.Inpatient.Order order, int SheetIndex)
        {
            if (this.myPatientInfo != null)
            {
                string strZG = "";
                if (myPatientInfo.PVisit.ZG != null
                    && !string.IsNullOrEmpty(myPatientInfo.PVisit.ZG.ID))
                {
                    //strZG = FS.SOC.HISFC.BizProcess.Cache.Order.GetZGInfo(myPatientInfo.PVisit.ZG.ID).Name;
                    FS.HISFC.Models.Base.Const cTemp = FS.SOC.HISFC.BizProcess.Cache.Order.GetZGInfo(myPatientInfo.PVisit.ZG.ID);
                    if (cTemp != null)
                    {
                        strZG = cTemp.Name;
                    }
                }

                if (strZG.Contains("死亡")
                    && SheetIndex == 0)
                {
                    MessageBox.Show("死亡患者，不能开立长期医嘱！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }

            string error = "";

            int ret = 1;

            //处方权判断
            ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, CacheManager.InOrderMgr.Operator,
                (CacheManager.InOrderMgr.Operator as FS.HISFC.Models.Base.Employee).Dept, FS.HISFC.Models.Base.DoctorPrivType.SpecialDrug, false, ref error);

            if (ret == 0)
            {
                MessageBox.Show(error, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //order.Status = 5;
                //return 0;
                return -1;
            }
            else if (ret < 0)
            {
                MessageBox.Show(error, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }



            if (FS.FrameWork.Function.NConvert.ToInt32(order.FirstUseNum) > order.Frequency.Times.Length)
            {
                order.FirstUseNum = order.Frequency.Times.Length.ToString();
                Classes.Function.ShowBalloonTip(2, order.Item.Name + "[" + order.Item.Specs + "] 首日量有误，\r\n系统自动调整为" + order.Frequency.Times.Length.ToString() + "!\r\n\r\n请注意查看！", "提示！", ToolTipIcon.Info);
            }

            return 1;
        }

        /// <summary>
        /// 上移医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuUp_Click(object sender, EventArgs e)
        {
            this.fpOrder.ActiveSheet.ClearSelection();
            this.fpOrder.ActiveSheet.AddSelection(this.fpOrder.ActiveSheet.ActiveRowIndex, 0, 1, this.fpOrder.ActiveSheet.ColumnCount);

            if (this.fpOrder.ActiveSheet.ActiveRowIndex <= 0)
            {
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order upOrder = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex - 1, this.fpOrder.ActiveSheetIndex).Clone();

            if (!"0,5".Contains(upOrder.Status.ToString()))
            {
                MessageBox.Show("当前行非新开立医嘱，不允许移动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckOrderCanMove(upOrder))
            {
                MessageBox.Show("【" + upOrder.Item.Name + "】已经打印，不允许移动！\r\n\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order downOrder = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex).Clone();

            if (!"0,5".Contains(downOrder.Status.ToString()))
            {
                MessageBox.Show("上一行非新开立医嘱，不允许移动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckOrderCanMove(downOrder))
            {
                MessageBox.Show("【" + downOrder.Item.Name + "】已经打印，不允许移动！\r\n\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //组合内移动
            if (upOrder.Combo.ID == downOrder.Combo.ID)
            {
                upOrder.SortID -= 1;
                AddObjectToFarpoint(upOrder, this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

                downOrder.SortID += 1;
                AddObjectToFarpoint(downOrder, this.fpOrder.ActiveSheet.ActiveRowIndex - 1, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

                this.fpOrder.ActiveSheet.Cells[this.fpOrder.ActiveSheet.ActiveRowIndex - 1, dicColmSet["顺序号"]].Tag = "哈哈";
            }
            else
            {
                //int upNum = 0;
                //int downNum = 0;
                FS.HISFC.Models.Order.Inpatient.Order oTmp = null;
                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    oTmp = GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    if (oTmp.Combo.ID == upOrder.Combo.ID)
                    {
                        //upNum++;
                        oTmp.SortID = -1;
                        //if (HsSubCombNo.Contains(oTmp.SubCombNO))
                        //{
                        //    HsSubCombNo.Remove(oTmp.SubCombNO);
                        //}
                    }
                    if (oTmp.Combo.ID == downOrder.Combo.ID)
                    {
                        //downNum++;
                        oTmp.SortID = -1;
                        //if (HsSubCombNo.Contains(oTmp.SubCombNO))
                        //{
                        //    HsSubCombNo.Remove(oTmp.SubCombNO);
                        //}
                    }
                }

                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    oTmp = GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    if (oTmp.Combo.ID == upOrder.Combo.ID)
                    {
                        oTmp.SubCombNO = downOrder.SubCombNO;
                        //oTmp.SortID = oTmp.SortID + downNum;
                        //oTmp.SortID = this.GetSortIDBySubCombNo(oTmp.SubCombNO);
                    }
                    else if (oTmp.Combo.ID == downOrder.Combo.ID)
                    {
                        oTmp.SubCombNO = upOrder.SubCombNO;
                        //oTmp.SortID = oTmp.SortID - upNum;
                        //oTmp.SortID = this.GetSortIDBySubCombNo(oTmp.SubCombNO);
                    }
                    oTmp.SortID = 0;
                    this.AddObjectToFarpoint(oTmp, i, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

                    if (i == this.ActiveRowIndex)
                    {
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag = "哈哈";
                    }
                }
            }

            RefreshCombo();
            //this.fpOrder.Sheets[0].SortRows(dicColmSet["顺序号"], false, true);
            //Classes.Function.DrawCombo(this.fpOrder.ActiveSheet, dicColmSet["组合号"], dicColmSet["组"]);

            for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
            {
                if (this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag != null
                    && this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag.ToString() == "哈哈")
                {
                    this.fpOrder.ActiveSheet.ActiveRowIndex = i;
                    this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;
                    this.ucItemSelect1.CurrentRow = ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    this.fpOrder.ActiveSheet.AddSelection(ActiveRowIndex, 0, 1, this.fpOrder.ActiveSheet.ColumnCount);
                    this.ucItemSelect1.Order = this.currentOrder;

                    this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag = null;
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
            this.fpOrder.ActiveSheet.ClearSelection();
            this.fpOrder.ActiveSheet.AddSelection(this.fpOrder.ActiveSheet.ActiveRowIndex, 0, 1, this.fpOrder.ActiveSheet.ColumnCount);

            if (this.fpOrder.ActiveSheet.ActiveRowIndex >= this.fpOrder.ActiveSheet.RowCount - 1)
            {
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order upOrder = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex).Clone();

            if (!"0,5".Contains(upOrder.Status.ToString()))
            {
                MessageBox.Show("当前行非新开立医嘱，不允许移动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckOrderCanMove(upOrder))
            {
                MessageBox.Show("【" + upOrder.Item.Name + "】已经打印，不允许移动！\r\n\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order downOrder = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex + 1, this.fpOrder.ActiveSheetIndex).Clone();

            if (!"0,5".Contains(downOrder.Status.ToString()))
            {
                MessageBox.Show("下一行非新开立医嘱，不允许移动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckOrderCanMove(downOrder))
            {
                MessageBox.Show("【" + downOrder.Item.Name + "】已经打印，不允许移动！\r\n\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            //组合内移动
            if (upOrder.Combo.ID == downOrder.Combo.ID)
            {
                upOrder.SortID -= 1;
                AddObjectToFarpoint(upOrder, this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

                this.fpOrder.ActiveSheet.Cells[this.fpOrder.ActiveSheet.ActiveRowIndex, dicColmSet["顺序号"]].Tag = "哈哈";

                downOrder.SortID += 1;
                AddObjectToFarpoint(downOrder, this.fpOrder.ActiveSheet.ActiveRowIndex + 1, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

            }
            else
            {
                //int upNum = 0;
                //int downNum = 0;
                FS.HISFC.Models.Order.Inpatient.Order oTmp = null;
                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    oTmp = GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    if (oTmp.Combo.ID == upOrder.Combo.ID)
                    {
                        //upNum++;
                        oTmp.SortID = -1;
                        //if (HsSubCombNo.Contains(oTmp.SubCombNO))
                        //{
                        //    HsSubCombNo.Remove(oTmp.SubCombNO);
                        //}
                    }
                    if (oTmp.Combo.ID == downOrder.Combo.ID)
                    {
                        //downNum++;
                        oTmp.SortID = -1;
                        //if (HsSubCombNo.Contains(oTmp.SubCombNO))
                        //{
                        //    HsSubCombNo.Remove(oTmp.SubCombNO);
                        //}
                    }
                }

                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    oTmp = GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    if (oTmp.Combo.ID == upOrder.Combo.ID)
                    {
                        oTmp.SubCombNO = downOrder.SubCombNO;
                        //oTmp.SortID = oTmp.SortID + downNum;
                        oTmp.SortID = 0;
                        //oTmp.SortID = this.GetSortIDBySubCombNo(oTmp.SubCombNO);
                    }
                    else if (oTmp.Combo.ID == downOrder.Combo.ID)
                    {
                        oTmp.SubCombNO = upOrder.SubCombNO;
                        //oTmp.SortID = oTmp.SortID - upNum;
                        oTmp.SortID = 0;
                        //oTmp.SortID = this.GetSortIDBySubCombNo(oTmp.SubCombNO);
                    }
                    this.AddObjectToFarpoint(oTmp, i, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

                    if (i == this.ActiveRowIndex)
                    {
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag = "哈哈";
                    }
                }
            }

            RefreshCombo();

            //this.fpOrder.Sheets[0].SortRows(dicColmSet["顺序号"], false, true);
            //Classes.Function.DrawCombo(this.fpOrder.ActiveSheet, dicColmSet["组合号"], dicColmSet["组"]);

            for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
            {
                if (this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag != null
                    && this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag.ToString() == "哈哈")
                {
                    this.fpOrder.ActiveSheet.ActiveRowIndex = i;
                    this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;
                    this.ucItemSelect1.CurrentRow = ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    this.fpOrder.ActiveSheet.AddSelection(ActiveRowIndex, 0, 1, this.fpOrder.ActiveSheet.ColumnCount);
                    this.ucItemSelect1.Order = this.currentOrder;

                    this.fpOrder.ActiveSheet.Cells[i, dicColmSet["顺序号"]].Tag = null;
                    break;
                }
            }

            this.SelectionChanged();
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="Tip"></param>
        /// <param name="Hypotest"></param>
        private void ucTip1_OKEvent(string Tip, int Hypotest)
        {
            this.fpOrder.ActiveSheet.SetNote(this.ActiveRowIndex, dicColmSet["医嘱名称"], Tip);
            string orderID = this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["医嘱流水号"]].Text;
            if (CacheManager.InOrderMgr.UpdateFeedback(this.myPatientInfo.ID, orderID, Tip, Hypotest) == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                CacheManager.InOrderMgr.Err = "";
            }

        }

        /// <summary>
        /// 累计用量查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTot_Click(object sender, EventArgs e)
        {
            string OrderID = this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["医嘱流水号"]].Text;
            FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(OrderID);
            if (order == null) return;
            //Classes.Function.TotalUseDrug(this.GetPatient().ID, order.Item.ID);
        }

        /// <summary>
        /// 修改医嘱类型 
        /// </summary>
        private void menuChange_Click(object sender, EventArgs e)
        {
            using (ucSimpleChange uc = new ucSimpleChange())
            {
                FS.HISFC.Models.Order.Inpatient.Order order = this.fpOrder.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.Order;

                uc.TitleLabel = "医嘱类型修改";
                uc.InfoLabel = "项目名称:" + order.Item.Name;
                uc.OperInfo = "医嘱类型";

                //获取医嘱类型
                FS.HISFC.BizLogic.Manager.OrderType orderType = new FS.HISFC.BizLogic.Manager.OrderType();
                ArrayList alOrderType = orderType.GetList();
                ArrayList alLong = new ArrayList();
                ArrayList alShort = new ArrayList();

                foreach (FS.HISFC.Models.Order.OrderType info in alOrderType)
                {
                    if (info.IsDecompose)
                    {
                        alLong.Add(info);
                    }
                    else
                    {
                        alShort.Add(info);
                    }
                }

                if (this.fpOrder.ActiveSheetIndex == 0)		//长嘱
                    uc.InfoItems = alLong;
                else
                    uc.InfoItems = alShort;

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                try
                {
                    if (uc.IReturn == 1)
                    {
                        FS.HISFC.Models.Order.OrderType tempOrderType = uc.ReturnInfo as FS.HISFC.Models.Order.OrderType;


                        bool isUp = true;
                        bool isDown = true;
                        int i = this.fpOrder.ActiveSheet.ActiveRowIndex;

                        FS.HISFC.Models.Order.Inpatient.Order inOrder = (this.fpOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order);

                        inOrder.OrderType = tempOrderType;

                        if (!object.Equals(this.ucItemSelect1.Order, null))
                        {
                            this.ucItemSelect1.Order.OrderType = tempOrderType;
                        }
                        this.fpOrder.ActiveSheet.Cells[i, this.dicColmSet["医嘱类型"]].Text = tempOrderType.Name;

                        inOrder.Item.Name = inOrder.Item.Name.Replace("[嘱托]", "").Replace("[自备]", "");
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["医嘱名称"]].Text = ShowOrderName(order);

                        int iUp, iDown;
                        iUp = i;
                        iDown = i;
                        while (isUp || isDown)
                        {
                            #region 向上查找 如到最前一行或组合号不同则置标志为false
                            if (isUp)
                            {
                                iUp = iUp - 1;
                                if (iUp < 0)
                                {
                                    isUp = false;
                                }
                                else
                                {
                                    if (((FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[iUp].Tag).Combo.ID == order.Combo.ID)
                                    {
                                        (this.fpOrder.ActiveSheet.Rows[iUp].Tag as FS.HISFC.Models.Order.Inpatient.Order).OrderType = tempOrderType;
                                        this.fpOrder.ActiveSheet.Cells[iUp, this.dicColmSet["医嘱类型"]].Text = tempOrderType.Name;

                                        (this.fpOrder.ActiveSheet.Rows[iUp].Tag as FS.HISFC.Models.Order.Inpatient.Order).Item.Name = (this.fpOrder.ActiveSheet.Rows[iUp].Tag as FS.HISFC.Models.Order.Inpatient.Order).Item.Name.Replace("[嘱托]", "").Replace("[自备]", "");
                                        this.fpOrder.ActiveSheet.Cells[iUp, dicColmSet["医嘱名称"]].Text = ShowOrderName((this.fpOrder.ActiveSheet.Rows[iUp].Tag as FS.HISFC.Models.Order.Inpatient.Order));
                                    }
                                    else
                                    {
                                        isUp = false;
                                    }
                                }
                            }
                            #endregion

                            #region 向下查找 如遇最下一行或组合号不同则置标志为false
                            if (isDown)
                            {
                                iDown = iDown + 1;
                                if (iDown >= this.fpOrder.ActiveSheet.Rows.Count)
                                    isDown = false;
                                else
                                {
                                    if (((FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[iDown].Tag).Combo.ID == order.Combo.ID)
                                    {
                                        (this.fpOrder.ActiveSheet.Rows[iDown].Tag as FS.HISFC.Models.Order.Inpatient.Order).OrderType = tempOrderType;
                                        this.fpOrder.ActiveSheet.Cells[iDown, this.dicColmSet["医嘱类型"]].Text = tempOrderType.Name;

                                        (this.fpOrder.ActiveSheet.Rows[iDown].Tag as FS.HISFC.Models.Order.Inpatient.Order).Item.Name = (this.fpOrder.ActiveSheet.Rows[iDown].Tag as FS.HISFC.Models.Order.Inpatient.Order).Item.Name.Replace("[嘱托]", "").Replace("[自备]", "");
                                        this.fpOrder.ActiveSheet.Cells[iDown, dicColmSet["医嘱名称"]].Text = ShowOrderName((this.fpOrder.ActiveSheet.Rows[iDown].Tag as FS.HISFC.Models.Order.Inpatient.Order));
                                    }
                                    else
                                    {
                                        isDown = false;
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 修改首日量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFirstDayChange_Click(object sender, EventArgs e)
        {
            //获取选择行数
            int row = this.fpOrder.ActiveSheet.ActiveRowIndex;
            if (row < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("请先选择一条医嘱！");
                return;
            }
            //获取医嘱信息
            FS.HISFC.Models.Order.Inpatient.Order order = this.GetObjectFromFarPoint(row, this.fpOrder.ActiveSheetIndex);
            if (order == null)
            {
                MessageBox.Show("获得医嘱实体出错！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (order.Status != 0)
            {
                MessageBox.Show("该医嘱状态已改变，请刷新数据查看！");
                return;
            }
            else
            {
                //弹出停止窗口
                Forms.frmFirstDayChange f = new Forms.frmFirstDayChange();
                f.FirstUseNum = order.FirstUseNum;
                f.Frequency = order.Frequency;
                f.ShowDialog();
                if (f.DialogResult != DialogResult.OK) return;
                //更新后的首日量
                order.FirstUseNum = f.FirstUseNum;

                #region 点击【确认】时更新
                //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //if (CacheManager.InOrderMgr.UpdateOrder(order) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                //    MessageBox.Show(CacheManager.InOrderMgr.Err);
                //    return;
                //}
                //FS.FrameWork.Management.PublicTrans.Commit();
                #endregion

                this.fpOrder.ActiveSheet.Cells[row, dicColmSet["首日量"]].Value = order.FirstUseNum;
            }
        }
        /// <summary>
        /// 上级医生审核医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCheckOrder_Click(object sender, EventArgs e)
        {
            this.JudgeSpecialOrder();
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
        /// <summary>
        /// 根据fp中临时号找到所在fp的行数
        /// </summary>
        /// <param name="tempId">临时号</param>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        private int getOrderRowIndex(string tempId, int sheetIndex)
        {
            for (int i = 0; i < this.fpOrder.Sheets[sheetIndex].RowCount; i++)
            {
                if (this.fpOrder.ActiveSheet.Cells[i, dicColmSet["首日量"]].Text == tempId)
                {
                    return i;
                }
            }
            return -1;
        }
        #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} 合理用药
        /// <summary>
        /// 跟据fp上的顺序号找到alAllOrder中的医嘱
        /// </summary>
        /// <param name="id">fp上的顺序号</param>
        /// <returns>alAllOrder中的医嘱</returns>
        public FS.HISFC.Models.Order.Inpatient.Order getOrderById(string id, int sheetIndex)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = this.fpOrder.Sheets[sheetIndex].Rows[id].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            //if (sheetIndex == 0)
            //{
            //    for (int i = 0; i < alAllLongOrder.Count; i++)
            //    {
            //        if (((FS.HISFC.Models.Order.Inpatient.Order)alAllLongOrder[i]).Oper.User03 == id)
            //            return alAllLongOrder[i] as FS.HISFC.Models.Order.Inpatient.Order;
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < alAllShortOrder.Count; i++)
            //    {
            //        if (((FS.HISFC.Models.Order.Inpatient.Order)alAllShortOrder[i]).Oper.User03 == id)
            //            return alAllShortOrder[i] as FS.HISFC.Models.Order.Inpatient.Order;
            //    }
            //}
            return null;
        }
        private string ActiveTempIDByRowIndex(int rowIndex)
        {
            return this.fpOrder.ActiveSheet.Cells[rowIndex, this.dicColmSet["首日量"]].Text;
        }

        //FS.HISFC.Models.Pharmacy.Item phaItem = null;

        ///// <summary>
        ///// 获取药品自定义码
        ///// </summary>
        ///// <param name="itemCode"></param>
        ///// <returns></returns>
        //private string GetPhaUserCode(string itemCode)
        //{
        //    //if (hsPhaUserCode != null && hsPhaUserCode.Contains(itemCode))
        //    //{
        //    //    return hsPhaUserCode[itemCode].ToString();
        //    //}
        //    //else
        //    //{
        //        phaItem = CacheManager.PhaIntegrate.GetItem(itemCode);
        //        if (phaItem != null)
        //        {
        //            return phaItem.UserCode;
        //        }
        //    //}
        //    return null;
        //}

        #endregion
        /// <summary>
        /// 检查结果查询{3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}pacs接口新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPacsView_Click(object sender, EventArgs e)
        {
            this.QueryPacsReport();
        }
        //{D2BDB9B8-7D50-4a66-8D1C-28EA0420592F}申请单
        private void checkSlip_Click(object sender, EventArgs e)
        {
            this.CheckSlip(this.fpOrder.ActiveSheet.ActiveRowIndex);
        }

        private void cancelSlip_Click(object sender, EventArgs e)
        {
            this.CancelSlip(this.fpOrder.ActiveSheet.ActiveRowIndex);
        }

        public void CheckSlip(int Index)
        {
            int i = Index;
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("请先选择一条医嘱！");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[i].Tag;
            FS.HISFC.Components.Order.Forms.frmCheckSlip ucCheckSlip = new FS.HISFC.Components.Order.Forms.frmCheckSlip();
            ucCheckSlip.Order = order;
            ucCheckSlip.MyPatientInfo = this.myPatientInfo;
            ucCheckSlip.handler += new FS.HISFC.Components.Order.Forms.frmCheckSlip.EventHandler(ucCheckSlip_handler);
            ucCheckSlip.ShowDialog();

        }

        public void CancelSlip(int Index)
        {
            int i = Index;
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("请先选择一条医嘱！");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[i].Tag;
            FS.HISFC.BizLogic.Order.CheckSlip checkSlip = new FS.HISFC.BizLogic.Order.CheckSlip();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            checkSlip.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            List<FS.HISFC.Models.Order.CheckSlip> list = new List<FS.HISFC.Models.Order.CheckSlip>();
            if ((((FS.FrameWork.Models.NeuObject)(order)).ID).ToString() != "")
            {
                list = checkSlip.QuerySlip(checkSlip.QueryByMoOrder(((FS.FrameWork.Models.NeuObject)(order)).ID).ToString());
                if (list.Count != 0)
                {
                    if (checkSlip.Delete(list[0].ToString()) == -1)
                    {
                        if (checkSlip.UpdateMetIpmOrder(((FS.FrameWork.Models.NeuObject)(order)).ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("检查申请单删除失败");
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("删除成功");
                }
                else
                {
                    MessageBox.Show("无申请单信息");
                }
            }
            else
            {
                if (order.ApplyNo.ToString() != "")
                {
                    list = checkSlip.QuerySlip(order.ApplyNo.ToString());
                    if (checkSlip.Delete(list[0].ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("检查申请单删除失败");
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("删除成功");
                }
                else
                {
                    MessageBox.Show("无申请单信息");
                }
            }
        }

        void ucCheckSlip_handler(FS.HISFC.Models.Order.CheckSlip obj)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[this.fpOrder.ActiveSheet.ActiveRowIndex].Tag;

            order.ApplyNo = obj.CheckSlipNo;
            this.AddObjectToFarpoint(order, this.fpOrder.ActiveSheet.RowCount - 1, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);
        }

        /// <summary>
        /// 组套存储
        /// </summary>
        public void SaveGroup()
        {
            FS.HISFC.Components.Common.Forms.frmOrderGroupManager group = new FS.HISFC.Components.Common.Forms.frmOrderGroupManager();

            try
            {
                group.IsManager = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();

            string stockDept = "";
            //for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
            for (int i = fpOrder.ActiveSheet.RowCount - 1; i >= 0; i--)
            {
                //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
                FS.HISFC.Models.Order.Inpatient.Order longorderTemp = null;

                if (!this.IsDesignMode)
                {
                    longorderTemp = this.GetObjectFromFarPoint(i, fpOrder.ActiveSheetIndex);
                }
                //开立模式下 只保存勾选的医嘱组套
                else
                {
                    if (this.fpOrder.ActiveSheet.IsSelected(i, 0))
                    {
                        longorderTemp = this.GetObjectFromFarPoint(i, fpOrder.ActiveSheetIndex);
                    }
                }

                if (longorderTemp == null)
                {
                    continue;
                }

                //FS.HISFC.Models.Order.Inpatient.Order longorder = this.ucOrder1.GetObjectFromFarPoint(i, 0).Clone();
                FS.HISFC.Models.Order.Inpatient.Order longorder = longorderTemp.Clone();
                if (longorder == null)
                {
                    MessageBox.Show("获得医嘱出错！");
                }
                else
                {
                    #region 判断药房是否存在该药品
                    if (longorder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Storage storage = CacheManager.PhaIntegrate.GetStockInfoByDrugCode(longorder.StockDept.ID, longorder.Item.ID);
                        if (storage == null || storage.Item.ID == "")
                        {
                            MessageBox.Show("【" + longorder.Item.Name + "】在本科室对应住院系统的【" + longorder.StockDept.Name + "】没有该药品，不能存为组套!");
                            return;
                        }
                    }
                    #endregion

                    string s = longorder.Item.Name;
                    string sno = longorder.Combo.ID;
                    //保存医嘱组套 默认开立时间为 零点
                    longorder.BeginTime = new DateTime(longorder.BeginTime.Year, longorder.BeginTime.Month, longorder.BeginTime.Day, 0, 0, 0);
                    al.Add(longorder);
                }
            }

            #region 先作废 长嘱、临嘱同时保存的功能，
            //因为切换fp后，界面的显示选择有问题，而且不能做到不勾选,比如点击长嘱后，只勾选保存临嘱的组套

            //#region 长期医嘱保存

            //for (int i = 0; i < this.fpOrder.Sheets[0].Rows.Count; i++)//长期医嘱
            //{
            //    //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
            //    FS.HISFC.Models.Order.Inpatient.Order longorderTemp = null;

            //    if (!this.IsDesignMode)
            //    {
            //        longorderTemp = this.GetObjectFromFarPoint(i, 0);
            //    }
            //    //开立模式下 只保存勾选的医嘱组套
            //    else
            //    {
            //        if (this.fpOrder.Sheets[0].IsSelected(i, 0))
            //        {
            //            longorderTemp = this.GetObjectFromFarPoint(i, 0);
            //        }
            //    }

            //    if (longorderTemp == null)
            //    {
            //        continue;
            //    }

            //    //FS.HISFC.Models.Order.Inpatient.Order longorder = this.ucOrder1.GetObjectFromFarPoint(i, 0).Clone();
            //    FS.HISFC.Models.Order.Inpatient.Order longorder = longorderTemp.Clone();
            //    if (longorder == null)
            //    {
            //        MessageBox.Show("获得医嘱出错！");
            //    }
            //    else
            //    {
            //        string s = longorder.Item.Name;
            //        string sno = longorder.Combo.ID;
            //        //保存医嘱组套 默认开立时间为 零点
            //        longorder.BeginTime = new DateTime(longorder.BeginTime.Year, longorder.BeginTime.Month, longorder.BeginTime.Day, 0, 0, 0);
            //        al.Add(longorder);
            //    }
            //}

            //#endregion

            //#region 临时医嘱保存
            //for (int i = 0; i < this.fpOrder.Sheets[1].Rows.Count; i++)//临时医嘱
            //{
            //    //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
            //    FS.HISFC.Models.Order.Inpatient.Order shortorderTemp = null;

            //    if (!IsDesignMode)
            //    {
            //        shortorderTemp = this.GetObjectFromFarPoint(i, 1);
            //    }
            //    else
            //    {
            //        if (this.fpOrder.Sheets[1].IsSelected(i, 0))
            //        {
            //            shortorderTemp = GetObjectFromFarPoint(i, 1);
            //        }
            //    }
            //    if (shortorderTemp == null)
            //    {
            //        continue;
            //    }
            //    //FS.HISFC.Models.Order.Inpatient.Order shortorder = this.ucOrder1.GetObjectFromFarPoint(i, 1).Clone();
            //    FS.HISFC.Models.Order.Inpatient.Order shortorder = shortorderTemp.Clone();
            //    if (shortorder == null)
            //    {
            //        MessageBox.Show("获得医嘱出错！");
            //    }
            //    else
            //    {
            //        string s = shortorder.Item.Name;
            //        string sno = shortorder.Combo.ID;
            //        //保存医嘱组套 默认开立时间为 零点
            //        shortorder.BeginTime = new DateTime(shortorder.BeginTime.Year, shortorder.BeginTime.Month, shortorder.BeginTime.Day, 0, 0, 0);
            //        al.Add(shortorder);
            //    }
            //}

            //#endregion

            #endregion

            if (al.Count > 0)
            {
                group.alItems = al;
                group.ShowDialog();

                if (OnRefreshGroupTree != null)
                {
                    this.OnRefreshGroupTree(null, null);
                }

                if (!this.IsDesignMode)
                {
                    //保存后清空界面显示
                    this.fpOrder.Sheets[0].RowCount = 0;
                    fpOrder.Sheets[1].RowCount = 0;
                }
            }
        }

        /// <summary>
        /// 粘贴医嘱{7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPasteOrder_Click(object sender, EventArgs e)
        {
            this.PasteOrder();
        }

        #endregion

        #region 类别变化需要特殊处理
        private void ucInputItem1_CatagoryChanged(FS.FrameWork.Models.NeuObject sender)
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = sender as FS.FrameWork.Models.NeuObject;
                if (obj.ID == FS.HISFC.Models.Base.EnumSysClass.MRD.ToString())
                {
                    this.ShowTransferDept();

                }
                else if (obj.ID == FS.HISFC.Models.Base.EnumSysClass.UN.ToString())
                {
                    //护理

                }
                else if (obj.ID == FS.HISFC.Models.Base.EnumSysClass.UC.ToString())
                {
                    //检查

                }
                else
                {
                    return;
                }


            }
            catch { }
        }

        #endregion

        #region 互斥
        /// <summary>
        /// 检查互斥
        /// </summary>
        /// <param name="sysClass"></param>
        /// <returns></returns>
        private int CheckMutex(FS.HISFC.Models.Base.SysClassEnumService sysClass)
        {
            //目前没有互斥的功能，为了优化屏蔽了
            return 1;

            if (sysClass == null)
            {
                return -1;
            }

            ArrayList al = new ArrayList();
            if (this.fpOrder.ActiveSheet.RowCount <= 0)
                return 0;
            for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = this.GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                if (order != null)
                {
                    if (order.Item.SysClass.ID.ToString() == sysClass.ID.ToString()
                        && (order.Status == 1 || order.Status == 2))
                    {
                        al.Add(order);
                    }
                }
            }
            if (sysClass.ID.ToString() == "UO")  //如果是手术医嘱，屏蔽互斥，by zuowy 2005-10-13
                return 0;
            try
            {
                FS.HISFC.Models.Order.EnumMutex mutex = CacheManager.InOrderMgr.QueryMutex(sysClass.ID.ToString());//查询互斥

                if (mutex == FS.HISFC.Models.Order.EnumMutex.SysClass)
                {
                    //系统类别互斥
                    if (al.Count == 0) return 0;//获得系统类别是否有重复的

                }
                else if (mutex == FS.HISFC.Models.Order.EnumMutex.All)
                {
                    //医嘱全部互斥
                    if (MessageBox.Show("开立的新的'" + sysClass.Name + "'医嘱，是否停止以前的全部医嘱?", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        if (CacheManager.InOrderMgr.DcOrder(this.myPatientInfo.ID, CacheManager.InOrderMgr.GetDateTimeFromSysDateTime()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            MessageBox.Show(CacheManager.InOrderMgr.Err);
                            return -1;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();

                        RefreshOrderState(true);
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获得互斥信息出错！" + ex.Message, "提示");
            }
            return 0;
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            //this.myOrderClass.SaveGrid();
            SaveFpStyle();
        }
        #endregion

        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("开立", "开立医嘱", 9, true, false, null);
            toolBarService.AddToolButton("组合", "组合医嘱", 9, true, false, null);
            toolBarService.AddToolButton("手术单", "手术申请", 9, true, false, null);
            toolBarService.AddToolButton("删除", "删除医嘱", 9, true, false, null);
            toolBarService.AddToolButton("取消组合", "取消组合医嘱", 9, true, false, null);
            toolBarService.AddToolButton("明细", "检验明细", 9, true, true, null);
            toolBarService.AddToolButton("退出医嘱更改", "退出医嘱更改", 9, true, false, null);
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
        }

        private object currentObject = null;
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
            {
                if (currentObject != neuObject)
                {
                    this.SetPatient(neuObject as FS.HISFC.Models.RADT.PatientInfo);
                }
                currentObject = neuObject;

                if (this.myPatientInfo != null)
                {
                    FS.HISFC.Models.RADT.PatientInfo pInfo = null;
                    string errInfo = "";
                    if (Classes.Function.CheckPatientState(myPatientInfo.ID, ref pInfo, ref errInfo) == -1)
                    {
                        MessageBox.Show(errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return -1;
                    }

                    decimal terminal = 0;

                    terminal = CacheManager.RadtIntegrate.QueryPatientTerminalFeeByInpatientNO(myPatientInfo.ID);

                    this.lbPatient.Text = myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床"
                        + "  " + myPatientInfo.PVisit.PatientLocation.NurseCell.Name//病区{97B3CB23-EE5A-45b0-ADBE-7B524DFC88EC}
                        + "  " + myPatientInfo.PID.PatientNO//住院号
                        + "  " + this.myPatientInfo.Name //姓名
                        + "  " + this.myPatientInfo.Sex.Name //性别
                        + "  " + CacheManager.InOrderMgr.GetAge(this.myPatientInfo.Birthday)//年龄
                        + "  " + this.myPatientInfo.Pact.Name//合同单位
                        + "\r\n"
                        + "住院日期：" + myPatientInfo.PVisit.InTime.ToString("yyyy.MM.dd") + " - " + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy.MM.dd") + " / " + CacheManager.RadtIntegrate.GetInDays(myPatientInfo.ID).ToString() + "天"//住院日期
                        + "\r\n"
                        + "总费用: " + myPatientInfo.FT.TotCost.ToString()
                        + "  预交金: " + this.myPatientInfo.FT.PrepayCost.ToString()
                        + "  自付：" + (myPatientInfo.FT.OwnCost + myPatientInfo.FT.PayCost).ToString()
                        + "  报销：" + myPatientInfo.FT.PubCost.ToString()
                        + "  余额: " + this.myPatientInfo.FT.LeftCost.ToString() + "  未确认金额: " + terminal;



                    FS.FrameWork.Models.NeuObject civilworkerObject = CacheManager.InterMgr.GetConstansObj("civilworker", myPatientInfo.Pact.ID);

                    if (myPatientInfo.Pact.PayKind.ID == "03" || (!object.Equals(neuObject, null) && !string.IsNullOrEmpty(civilworkerObject.ID)))
                    {
                        lbPatient.Text += "\r\n"
                        + "日限额：" + myPatientInfo.FT.DayLimitCost.ToString()
                            //+ "  自费药金额：" + myPatientInfo.FT.DrugOwnCost.ToString()
                            //+ "  药品超标金额：" + myPatientInfo.FT.OvertopCost.ToString()
                        + "  药品累计：" + myPatientInfo.FT.DrugFeeTotCost.ToString();


                        pnPatient.Height = 72;
                    }
                    else
                    {
                        this.pnPatient.Height = 58;
                    }

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询患者标签信息,请稍候!");
                    //设置标签功能的his卡号{0F599816-C860-40e1-856A-EF5ACACBDA26}
                    //{D88A3D9E-5B33-4e66-8030-D44BCEC73646}
                    ucPatientLabel1.getUserLabelByHisCardNo(this.myPatientInfo.PID.CardNO);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    this.pnPatient.Visible = true;
                }
                else
                {
                    this.pnPatient.Visible = false;
                }
            }
            return 0;
        }

        /// <summary>
        /// 停止全部医嘱及保存转归情况 by huangchw 2012-10-29
        /// </summary>
        /// <returns></returns>
        public int DcAllLongOrderAndZG()
        {
            if (frmDCOrderAndZG1 == null)
            {
                //{5936B0A0-598F-43a8-BB31-E812EB8D61EE}
                //次框中涉及到dblink去EMR查询患者出院诊断，必须要进行提交
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                frmDCOrderAndZG1 = new FS.HISFC.Components.Order.Forms.frmDCOrderAndZG();
                frmDCOrderAndZG1.Patient = this.Patient;
                frmDCOrderAndZG1.Init();
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            frmDCOrderAndZG1.ShowDialog();

            if (frmDCOrderAndZG1.DialogResult != DialogResult.OK)
            {
                return 0;
            }

            if (DcAllLongOrder(frmDCOrderAndZG1.DCDateTime, frmDCOrderAndZG1.DCReason) <= 0)
            {
                return -1;
            }

            #region 保存转归情况
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (CacheManager.InPatientMgr.UpdateZG(this.myPatientInfo.ID, frmDCOrderAndZG1.ZG.ID, frmDCOrderAndZG1.DiagInfo, frmDCOrderAndZG1.HealthCareObject) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存信息失败：" + CacheManager.InPatientMgr.Err);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            #endregion

            //{9BCBF464-EB90-4c07-AD4D-29481A069D3D}
            HISFC.Models.Base.Employee empl2 = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;

            HISFC.Models.Base.Department dept2 = empl2.Dept as HISFC.Models.Base.Department;

            #region 保存医保待遇情况
            if (!string.IsNullOrEmpty(frmDCOrderAndZG1.HealthCareObject))
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                if (CacheManager.InPatientMgr.UpdateYIBAODAIYU(this.myPatientInfo.ID, frmDCOrderAndZG1.HealthCareObject) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存信息失败：" + CacheManager.InPatientMgr.Err);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion


            MessageBox.Show("录入转归情况和停止全部长期医嘱成功!");
            return 1;
        }

        public int DcTreatmenttype() //{d88ca0f0-6235-4a5d-b04e-4eac0f7a78e7}
        {
            if (frmDCTreatmentType == null)
            {
                frmDCTreatmentType = new FS.HISFC.Components.Order.Forms.frmDCTreatmentType();
                frmDCTreatmentType.Patient = this.Patient;
                frmDCTreatmentType.Init();
            }

            frmDCTreatmentType.ShowDialog();

            if (frmDCTreatmentType.DialogResult != DialogResult.OK)
            {
                return -1;
            }
            else
            {
                if (!string.IsNullOrEmpty(frmDCTreatmentType.HealthCareObject))
                {


                    if (CacheManager.InPatientMgr.UpdateYIBAODAIYU(this.myPatientInfo.ID, frmDCTreatmentType.HealthCareObject) == -1)
                    {

                        MessageBox.Show("保存信息失败：" + CacheManager.InPatientMgr.Err);
                        return -1;
                    }

                }
            }


            return 1;
        }

        /// <summary>
        /// 停止全部医嘱
        /// </summary>
        public int DcAllLongOrder(string strDc)
        {
            //Add by houwb 2011-3-11 {46E8908F-4248-4a40-89B1-530CA5796CD4}
            if (MessageBox.Show(this, "确定停止全部长期医嘱？" + "\r\n" + strDc, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
            {
                return -1;
            }

            string strTip = "停止";
            //弹出停止窗口
            Forms.frmDCOrder f = new Forms.frmDCOrder();
            f.ShowDialog();
            if (f.DialogResult != DialogResult.OK)
            {
                return 0;
            }

            if (DcAllLongOrder(f.DCDateTime, f.DCReason) <= 0)
            {
                return -1;
            }

            MessageBox.Show("停止全部长期医嘱成功!");
            return 1;
        }

        /// <summary>
        /// 停止医嘱
        /// </summary>
        /// <param name="dcDate">停止时间</param>
        /// <param name="dcReason">停止原因</param>
        /// <returns></returns>
        public int DcAllLongOrder(DateTime dcDate, FS.FrameWork.Models.NeuObject dcReason)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            bool isHaveNewOrder = false;

            for (int i = 0; i < this.fpOrder.Sheets[0].RowCount; i++)
            {
                order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.Sheets[0].Rows[i].Tag;
                order = this.GetObjectFromFarPoint(i, 0);
                if (order == null)
                {
                    MessageBox.Show("获取医嘱项目失败,第" + i.ToString() + "行！");
                    return -1;
                }

                //新开立的不停止
                if (order.Status == 0 || order.Status == 5 || order.Status == 4)
                {
                    isHaveNewOrder = true;
                }
                else if (order.Status != 3)
                {
                    order.DCOper.OperTime = dtNow;
                    order.DcReason = dcReason;
                    order.DCOper.ID = CacheManager.InOrderMgr.Operator.ID;
                    order.DCOper.Name = CacheManager.InOrderMgr.Operator.Name;
                    order.EndTime = dcDate;
                    order.Status = 3;

                    if (order.EndTime < order.BeginTime)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("[" + order.Item.Name + "]停止时间不能小于开始时间");
                        return -1;
                    }

                    #region 小时医嘱停止计费
                    if (this.DCHoursOrder(order, FS.FrameWork.Management.PublicTrans.Trans, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("[" + order.Item.Name + "]停止时记费失败！"));
                        return -1;
                    }
                    #endregion

                    #region 停止医嘱

                    string strReturn = "";
                    if (CacheManager.InOrderMgr.DcOrder(order, true, out strReturn) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(CacheManager.InOrderMgr.Err);
                        return -1;
                    }

                    if (strReturn != "")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(strReturn);
                        return -1;
                    }
                    #endregion

                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["医嘱状态"]].Value = order.Status;
                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["停止时间"]].Value = order.DCOper.OperTime;
                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["结束时间"]].Value = order.EndTime;
                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["停止人编码"]].Text = order.DCOper.ID;
                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["停止人"]].Text = order.DCOper.Name;
                    this.fpOrder.Sheets[0].Rows[i].Tag = order;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (isHaveNewOrder)
            {
                Classes.Function.ShowBalloonTip(2, "提示", "存在新开立医嘱未停止，请手动处理！", ToolTipIcon.Warning);
            }

            this.RefreshOrderState(-1);

            this.SendMessage(SendType.Delete);

            //this.QueryOrder(); edit by liuww 会把开立的医嘱刷新没有
            return 1;
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[7];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.IPrintOrder);
                t[1] = typeof(FS.HISFC.BizProcess.Interface.ITransferDeptApplyable);
                t[2] = typeof(FS.HISFC.BizProcess.Interface.Common.ILis);
                t[3] = typeof(FS.HISFC.BizProcess.Interface.IAlterOrder);
                t[4] = typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPrint);//检查申请单
                t[5] = typeof(FS.HISFC.BizProcess.Interface.Common.IPacs);//pacs{3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}
                t[6] = typeof(FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine);
                return t;
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            FS.HISFC.BizProcess.Interface.IPrintOrder o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.IPrintOrder)) as FS.HISFC.BizProcess.Interface.IPrintOrder;
            if (o == null)
            {
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.PrintPreview(this.panelOrder);
            }
            else
            {
                o.SetPatient(this.myPatientInfo);
                o.ShowPrintSet();
            }


        }

        #region 补打
        /// <summary>
        /// 补打选中项目 
        /// </summary>        
        public void PrintAgain(string type)
        {
            if (this.EditGroup)
            {
                ucItemSelect1.MessageBoxShow("您正在编辑组套，此时不支持打印处方！");
                return;
            }
            if (this.myPatientInfo == null || string.IsNullOrEmpty(this.myPatientInfo.ID))
            {
                ucItemSelect1.MessageBoxShow("请选择患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FS.HISFC.Models.Order.Order order;
            ArrayList orderList = new ArrayList();

            for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpOrder.ActiveSheet.IsSelected(i, 0))
                {
                    order = GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    orderList.Add(order);
                }
            }
            //初始化接口
            if (object.Equals(IInPatientOrderPrint, null))
            {
                this.InitOrderPrint();
            }


            #region 调用接口实现打印
            if (IInPatientOrderPrint != null && orderList.Count > 0)
            {

                if (IInPatientOrderPrint.PrintInPatientOrderBill(this.myPatientInfo, type, this.GetReciptDept(), this.GetReciptDoc(), orderList, false) != 1)
                {
                    ucItemSelect1.MessageBoxShow(IInPatientOrderPrint.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        #endregion



        protected override int OnPrint(object sender, object neuObject)
        {
            Print();
            return 0;
        }

        /// <summary>
        /// 显示转科申请
        /// </summary>
        public void ShowTransferDept()
        {
            if (this.ucItemSelect1.SelectedOrderType == null) return;
            FS.HISFC.BizProcess.Interface.ITransferDeptApplyable o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.ITransferDeptApplyable)) as FS.HISFC.BizProcess.Interface.ITransferDeptApplyable;
            if (o == null)
            {
                return;
            }
            else
            {
                o.SetPatientInfo(this.myPatientInfo);
                if (o.ShowDialog() == DialogResult.OK)
                {
                    FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                    FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();

                    order.OrderType = (FS.HISFC.Models.Order.OrderType)this.ucItemSelect1.SelectedOrderType.Clone();
                    order.Item = item;
                    order.Item.SysClass.ID = "MRD";
                    order.Item.ID = "999";
                    order.Qty = 1;
                    order.Unit = "次";
                    order.Item.Name = o.Dept.Name + "[转科]";
                    order.ExeDept = o.Dept.Clone();
                    order.Frequency.ID = "QD";

                    this.AddNewOrder(order, this.fpOrder.ActiveSheetIndex);
                }
            }

        }

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

        /// <summary>
        /// 拖动时保存为xml格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            SaveFpStyle();
        }

        private void SaveFpStyle()
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpOrder.Sheets[0], this.LONGSETTINGFILENAME);
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpOrder.Sheets[1], this.SHORTSETTINGFILENAME);
        }

        #region 医嘱重整

        /// <summary>
        /// 重整医嘱
        /// </summary>
        /// <returns></returns>
        public int ReTidyOrder()
        {
            #region {74E478F5-BDDD-4637-9F5A-E251AF9AA72F}
            if (this.myPatientInfo == null)
            {
                MessageBox.Show("请先选择患者!");
                return -1;
            }
            #endregion

            DialogResult rs = MessageBox.Show("确认进行医嘱重整吗？重整医嘱将停止并重开当前有效医嘱，过滤所有停止医嘱", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            //List<FS.HISFC.Models.Order.Inpatient.Order> orderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();

            //for (int i = 0; i < this.fpOrder_Long.Rows.Count; i++)
            //{
            //    FS.HISFC.Models.Order.Inpatient.Order info = this.fpOrder_Long.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            //    orderList.Add(info);
            //}
            //int result = this.ReTidyOrder(orderList);
            int result = ReTidyOrderAll();
            this.QueryOrder();
            return result;
        }

        /// <summary>
        /// 重整医嘱
        /// </summary>
        /// <param name="orderList"></param>
        /// <returns></returns>
        //internal int ReTidyOrder(List<FS.HISFC.Models.Order.Inpatient.Order> orderList)
        internal int ReTidyOrderAll()
        {
            //{D05BA7C4-3158-48aa-B581-0211E2CAAD4C} 
            #region 获取重整医嘱处理方式
            /*
             * 方式一：保留原医嘱 新增重整状态医嘱   
             * 方式二：修改原医嘱为重整状态 新增有效医嘱
             * */

            int retidyType = 2;//默认方式二
            retidyType = CacheManager.ContrlManager.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.MetConstant.Order_RetidyType, true, 2);

            #endregion

            int maxSortID = 3000;

            ArrayList alOrder = CacheManager.InOrderMgr.QueryOrder(this.myPatientInfo.ID);
            if (alOrder == null)
            {
                MessageBox.Show("查询医嘱信息失败！" + CacheManager.InOrderMgr.Err);
                return -1;
            }
            ArrayList alSubOrder = CacheManager.InOrderMgr.QueryOrderSubtbl(this.myPatientInfo.ID);
            if (alSubOrder == null)
            {
                MessageBox.Show("查询医嘱附材信息失败！" + CacheManager.InOrderMgr.Err);
                return -1;
            }
            alOrder.AddRange(alSubOrder);

            //按照序号排序下
            OrderSortIDCompare orderCompare = new OrderSortIDCompare();
            alOrder.Sort(orderCompare);

            //最新一条医嘱记录 并形成医嘱序号
            FS.HISFC.Models.Order.Inpatient.Order order = alOrder[alOrder.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order;
            maxSortID = order.SortID + 10;

            //所有长嘱
            List<FS.HISFC.Models.Order.Inpatient.Order> longOrderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            //当前有效医嘱列表
            List<FS.HISFC.Models.Order.Inpatient.Order> validOrderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            //停止医嘱列表
            List<FS.HISFC.Models.Order.Inpatient.Order> DcOrderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            //重整后新医嘱列表
            List<FS.HISFC.Models.Order.Inpatient.Order> newOrderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();

            #region 判读是否允许进行重整 并形成待重整医嘱列表

            foreach (FS.HISFC.Models.Order.Inpatient.Order obj in alOrder)
            {
                if (obj.OrderType.IsDecompose)
                {
                    //
                    if (obj.Status == 0 && !obj.IsSubtbl)
                    {
                        MessageBox.Show("存在新开立未审核的医嘱，不能进行医嘱重整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }

                    longOrderList.Add(obj);
                    if (obj.Status == 1 || obj.Status == 2)      //原有效医嘱
                    {
                        validOrderList.Add(obj);
                    }
                    else if (obj.Status == 3)
                    {
                        DcOrderList.Add(obj);
                    }
                }
            }

            //判断停止的必须审核，因为下面会更新停止医嘱的状态为重整
            ArrayList alUnconfirmOrder = CacheManager.InOrderMgr.QueryIsConfirmOrder(this.myPatientInfo.ID, FS.HISFC.Models.Order.EnumType.LONG, false);
            if (alUnconfirmOrder == null)
            {
                MessageBox.Show("查询未审核医嘱出错:" + CacheManager.InOrderMgr.Err);
                return -1;
            }
            if (alUnconfirmOrder.Count > 0)
            {
                MessageBox.Show("存在新开立未审核或已停止未审核的医嘱，不能进行医嘱重整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            #endregion

            #region 对原有效医嘱形成新医嘱

            string comboNO = string.Empty;
            string comboNOTemp = string.Empty;

            //组号
            int subCombNo = -1;

            foreach (FS.HISFC.Models.Order.Inpatient.Order info in validOrderList)
            {
                FS.HISFC.Models.Order.Inpatient.Order newOrderTemp = info.Clone();

                if (newOrderTemp.Combo.ID == comboNO)
                {
                    newOrderTemp.Combo.ID = comboNOTemp;
                    //subCombNo = this.GetMaxCombNo(0);
                }
                else
                {
                    comboNO = newOrderTemp.Combo.ID;
                    comboNOTemp = CacheManager.InOrderMgr.GetNewOrderComboID();
                    newOrderTemp.Combo.ID = comboNOTemp;

                    if (subCombNo == -1)
                    {
                        subCombNo = this.GetMaxCombNo(newOrderTemp, 0);
                    }
                    else
                    {
                        subCombNo++;
                    }

                    maxSortID++;
                }

                //newOrderTemp.SortID = maxSortID;

                newOrderTemp.SubCombNO = subCombNo;
                //newOrderTemp.SortID = this.GetSortIDBySubCombNo(newOrderTemp.SubCombNO);
                newOrderTemp.SortID = 0;

                newOrderTemp.ReTidyInfo = "重整医嘱  原医嘱流水号：" + newOrderTemp.ID.ToString();

                newOrderTemp.ID = CacheManager.InOrderMgr.GetNewOrderID();

                newOrderList.Add(newOrderTemp);

                //不要屏蔽，这个用来获取正确的排序号
                this.AddNewOrder(newOrderTemp, 0);
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            //{D05BA7C4-3158-48aa-B581-0211E2CAAD4C}
            #region 方式二  原有效医嘱变更为重整状态 增加新医嘱同原有效医嘱
            if (retidyType == 2)
            {
                #region 将原有有效医嘱全部停掉

                foreach (FS.HISFC.Models.Order.Inpatient.Order info in validOrderList)
                {
                    info.Status = 3;

                    info.DCOper.ID = CacheManager.InOrderMgr.Operator.ID;
                    info.DCOper.Name = CacheManager.InOrderMgr.Operator.Name;
                    info.DCOper.OperTime = sysTime;

                    info.DcReason.ID = "RT";
                    info.DcReason.Name = "医嘱重整";

                    info.EndTime = info.DCOper.OperTime;

                    if (CacheManager.InOrderMgr.DcOneOrder(info) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("停止原有效医嘱失败:" + CacheManager.InOrderMgr.Err);
                        return -1;
                    }
                }

                #endregion

                #region 所有医嘱置为重整状态

                foreach (FS.HISFC.Models.Order.Inpatient.Order info in longOrderList)
                {
                    info.Status = 4;                //医嘱重整状态
                    info.Oper.ID = CacheManager.InOrderMgr.Operator.ID;
                    info.Oper.Name = CacheManager.InOrderMgr.Operator.Name;
                    info.Oper.OperTime = sysTime;

                    if (CacheManager.InOrderMgr.OrderReform(info.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("重整原医嘱失败:" + CacheManager.InOrderMgr.Err);
                        return -1;
                    }
                }

                #endregion

                foreach (FS.HISFC.Models.Order.Inpatient.Order info in newOrderList)
                {
                    //新医嘱开始时间和状态
                    info.Status = 0;
                    info.BeginTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                    info.MOTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

                    //开方科室
                    info.ReciptDoctor.ID = this.GetReciptDoc().ID;
                    info.ReciptDoctor.Name = this.GetReciptDoc().Name;
                    info.DoctorDept.ID = this.GetReciptDept().ID;
                    info.DoctorDept.Name = this.GetReciptDept().Name;

                    //置为未打印
                    info.GetFlag = "0";
                    info.PageNo = -1;
                    info.RowNo = -1;
                    info.FirstUseNum = "0";

                    info.Patient.PVisit.PatientLocation.Bed.ID = this.myPatientInfo.PVisit.PatientLocation.Bed.ID;//开立时的床号
                }
            }
            #endregion

            #region 方式一  原有效医嘱信息不变 增加重整状态的医嘱(信息同原有效医嘱但状态不同)
            else
            {
                //停止医嘱置为重整状态 {A3B78606-5301-4680-9CF4-08B6545D6608} 20100528
                foreach (FS.HISFC.Models.Order.Inpatient.Order info in DcOrderList)
                {
                    info.Status = 4;                //医嘱重整状态
                    info.Oper.ID = CacheManager.InOrderMgr.Operator.ID;
                    info.Oper.Name = CacheManager.InOrderMgr.Operator.Name;
                    info.Oper.OperTime = sysTime;

                    if (CacheManager.InOrderMgr.OrderReform(info.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("重整原医嘱失败:" + CacheManager.InOrderMgr.Err);
                        return -1;
                    }
                }

                foreach (FS.HISFC.Models.Order.Inpatient.Order info in newOrderList)
                {
                    info.Status = 4;               //医嘱重整状态
                    info.Oper.ID = CacheManager.InOrderMgr.Operator.ID;
                    info.Oper.Name = CacheManager.InOrderMgr.Operator.Name;
                    info.Oper.OperTime = sysTime;
                }
            }

            #endregion

            #region 对新医嘱进行保存

            foreach (FS.HISFC.Models.Order.Inpatient.Order info in newOrderList)
            {
                if (CacheManager.InOrderMgr.InsertOrder(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("生成新医嘱失败:" + CacheManager.InOrderMgr.Err);
                    return -1;
                }
            }

            #endregion

            #region 保存重整记录

            FS.FrameWork.Management.ExtendParam extendManager = new FS.FrameWork.Management.ExtendParam();
            FS.HISFC.Models.Base.ExtendInfo extendInfo = new ExtendInfo();

            extendInfo.ExtendClass = EnumExtendClass.PATIENT;
            extendInfo.Item.ID = this.myPatientInfo.ID;
            extendInfo.PropertyCode = sysTime.ToString();
            extendInfo.PropertyName = "重整时间";
            extendInfo.DateProperty = sysTime;

            extendInfo.StringProperty = CacheManager.InOrderMgr.Operator.ID;
            extendInfo.DateProperty = sysTime;

            if (extendManager.InsertComExtInfo(extendInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("医嘱重整变更记录失败:" + extendManager.Err);
                return -1;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("医嘱重整成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return 1;
        }

        #endregion

        #region 医嘱单打印

        /// <summary>
        /// 医嘱单打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintOrder IPrintOrderInstance = null;

        /// <summary>
        /// 医嘱单打印（接口模式）
        /// </summary>
        /// <returns></returns>
        public int PrintOrder()
        {
            if (this.IPrintOrderInstance == null)
            {
                this.IPrintOrderInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.IPrintOrder)) as FS.HISFC.BizProcess.Interface.IPrintOrder;
            }
            if (IPrintOrderInstance == null)
            {
                MessageBox.Show("医嘱单打印接口未实现！");
                return -1;
            }

            try
            {
                IPrintOrderInstance.SetPatient(myPatientInfo);
                IPrintOrderInstance.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 1;
        }
        /// <summary>
        /// 检查申请单打印// {0045F3F6-1B1C-4d0a-A834-8BD07286E175}
        /// </summary>
        /// <returns></returns>
        public void RecipePrint()
        {
            FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrint)) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrint;
            if (o == null)
            {
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.PrintPreview(this.panelOrder);
            }
            else
            {
                o.SetPatientInfo(this.myPatientInfo);
            }

        }

        /// <summary>
        /// 检查医嘱是否可以删除或移动
        /// </summary>
        /// <param name="inOrder"></param>
        /// <returns></returns>
        private bool CheckOrderCanMove(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            if (inOrder.GetFlag.Trim() == "1")
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 获取方号

        /// <summary>
        /// 获取最大方号
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public int GetMaxCombNo(FS.HISFC.Models.Order.Inpatient.Order order, int sheetIndex)
        {
            if (sheetIndex == -1)
            {
                sheetIndex = this.fpOrder.ActiveSheetIndex;
            }

            //界面上方号
            int maxSubCombNo = 0;

            //首先获取数据库最大组号
            string sql = @"select nvl(max(to_number(f.subcombno)),0) from met_ipm_order f
                        where f.inpatient_no='{0}'
                        and f.decmps_state='{1}'";

            if (myPatientInfo != null)
            {
                try
                {
                    maxSubCombNo = FS.FrameWork.Function.NConvert.ToInt32(CacheManager.InOrderMgr.ExecSqlReturnOne(string.Format(sql, myPatientInfo.ID, (sheetIndex == 0 ? "1" : "0")), "0"));
                }
                catch (Exception ex)
                {
                    maxSubCombNo = 0;
                    Classes.Function.ShowBalloonTip(2, "错误", ex.Message, ToolTipIcon.Error);
                }
            }

            //只从界面获取，以后会限制两个人同时给一个患者开立的情况
            FS.HISFC.Models.Order.Inpatient.Order inOrder = null;
            for (int i = 0; i < fpOrder.Sheets[sheetIndex].RowCount; i++)
            {
                inOrder = this.GetObjectFromFarPoint(i, sheetIndex);
                if (inOrder != null && inOrder.SubCombNO > 0)
                {
                    if (order != null && inOrder.Combo.ID == order.Combo.ID)
                    {
                        return inOrder.SubCombNO;
                    }
                    else
                    {
                        if (maxSubCombNo < inOrder.SubCombNO)
                        {
                            maxSubCombNo = inOrder.SubCombNO;
                        }
                        return maxSubCombNo + 1;
                    }
                }
            }



            return maxSubCombNo + 1;

            //if (sheetIndex == 0)
            //{
            //if (this.hsLongSubCombNo == null || hsLongSubCombNo.Count == 0)
            //{
            //    maxSubCombNo = 0;
            //}
            //else
            //{
            //    foreach (int keys in hsLongSubCombNo.Keys)
            //    {
            //        if (maxSubCombNo < keys)
            //        {
            //            maxSubCombNo = keys;
            //        }
            //    }
            //}
            //}
            //else
            //{
            //if (this.hsShortSubCombNo == null || hsShortSubCombNo.Count == 0)
            //{
            //    maxSubCombNo = 0;
            //}
            //else
            //{
            //    foreach (int keys in hsShortSubCombNo.Keys)
            //    {
            //        if (maxSubCombNo < keys)
            //        {
            //            maxSubCombNo = keys;
            //        }
            //    }
            //}
            //}

            return maxSubCombNo + 1;
        }

        private int GetSortIDBySubCombNo(int subCombNo)
        {
            FS.HISFC.Models.Order.Inpatient.Order subOrder = null;
            if (this.ucItemSelect1_GetSameSubCombNoOrder(subCombNo, ref subOrder) == -1)
            {
                return FS.FrameWork.Function.NConvert.ToInt32(subCombNo.ToString() + "00001");
            }
            else if (subOrder == null)
            {
                return FS.FrameWork.Function.NConvert.ToInt32(subCombNo.ToString() + "00001");
            }
            else
            {
                return subOrder.SortID + 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete("作废，修改方号获取模式", true)]
        private Hashtable GetActiveSubCombNo()
        {
            return null;
            //if (this.fpOrder.ActiveSheetIndex == 0)
            //{
            //    return this.hsLongSubCombNo;
            //}
            //else
            //{
            //    return this.hsShortSubCombNo;
            //}
        }

        /// <summary>
        /// 删除组号
        /// </summary>
        /// <param name="subCombNo"></param>
        /// <param name="isLong"></param>
        /// <returns></returns>
        int ucItemSelect1_DeleteSubComnNo(int subCombNo, bool isLong)
        {
            if (this.fpOrder.ActiveSheet.SelectionCount > 1)
            {
                return 1;
            }
            //if (isLong)
            //{
            //    if (this.hsLongSubCombNo.Contains(subCombNo))
            //    {
            //        this.hsLongSubCombNo.Remove(subCombNo);
            //    }
            //}
            //else
            //{
            //    if (this.hsShortSubCombNo.Contains(subCombNo))
            //    {
            //        this.hsShortSubCombNo.Remove(subCombNo);
            //    }
            //}
            return 1;
        }

        /// <summary>
        /// 获取相同方号医嘱
        /// </summary>
        /// <param name="sortID"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        int ucItemSelect1_GetSameSubCombNoOrder(int subCombNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            try
            {
                //if (this.fpOrder.ActiveSheetIndex == 0)
                //{
                //    if (hsLongSubCombNo.Contains(subCombNo))
                //    {
                //        order = hsLongSubCombNo[subCombNo] as FS.HISFC.Models.Order.Inpatient.Order;
                //    }
                //}
                //else
                //{
                //    if (this.hsShortSubCombNo.Contains(subCombNo))
                //    {
                //        order = hsShortSubCombNo[subCombNo] as FS.HISFC.Models.Order.Inpatient.Order;
                //    }
                //}
                //if (object.Equals(order, null))
                //{
                //    return 1;
                //}

                //根据方号组合时，获取最新的
                FS.HISFC.Models.Order.Inpatient.Order orderTemp = null;

                int sortID = 0;
                //for (int i = sheet.RowCount - 1; i > 0; i--)
                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    orderTemp = fpOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (orderTemp != null)
                    {
                        if (orderTemp.SubCombNO < subCombNo)
                        {
                            break;
                        }

                        if (orderTemp.SubCombNO == subCombNo && sortID < orderTemp.SortID)
                        {
                            sortID = orderTemp.SortID;
                            order = orderTemp.Clone();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }

        #endregion

        #region LIS、PACS接口实现

        /// <summary>
        /// 显示LIS结果
        /// </summary>
        public void QueryLisResult()
        {
            try
            {
                if (this.myPatientInfo == null || this.myPatientInfo.PID.CardNO == "" || myPatientInfo.PID.CardNO == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请先选择患者！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lisInterface == null)
                {
                    lisInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.Common.ILis)) as FS.HISFC.BizProcess.Interface.Common.ILis;
                }

                if (lisInterface == null)
                {
                    if (string.IsNullOrEmpty(FS.FrameWork.WinForms.Classes.UtilInterface.Err))
                    {
                        MessageBox.Show("查询LIS接口出现错误：\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有维护LIS接口！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    lisInterface.PatientType = FS.HISFC.Models.RADT.EnumPatientType.I;
                    lisInterface.SetPatient(this.myPatientInfo);
                    lisInterface.PlaceOrder(this.GetSelectedOrders());

                    if (lisInterface.ShowResultByPatient() == -1)
                    {
                        MessageBox.Show(lisInterface.ErrMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 初始化PACS接口
        /// </summary>
        /// <returns></returns>
        private int InitPacsInterface()
        {
            this.pacsInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IPacs)) as FS.HISFC.BizProcess.Interface.Common.IPacs;
            if (this.pacsInterface == null)
            {
                if (string.IsNullOrEmpty(FS.FrameWork.WinForms.Classes.UtilInterface.Err))
                {

                    MessageBox.Show("查询PACS接口出现错误：\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有维护PACS结果查询接口！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }
            if (this.pacsInterface.Connect() == 0)
            {
                MessageBox.Show("初始化PACS失败！请再查看一次！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            return 1;
        }

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
                ArrayList alSelectOrder = new ArrayList(this.GetSelectedOrders());

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

        /// <summary>
        /// 查看PACS检查报告单
        /// </summary>
        public void QueryPacsReport()
        {
            if (this.myPatientInfo == null || this.myPatientInfo.PID.CardNO == "" || myPatientInfo.PID.CardNO == null)
            {
                MessageBox.Show("请选择一个患者！");
                return;
            }

            try
            {
                if (pacsInterface == null)
                {
                    if (this.InitPacsInterface() == -1)
                    {
                        return;
                    }
                }

                this.pacsInterface.OprationMode = "1";
                this.pacsInterface.SetPatient(myPatientInfo);
                pacsInterface.PlaceOrder(this.GetSelectedOrders());

                if (this.pacsInterface.ShowResultByPatient() == 0)
                {


                    if (this.pacsInterface.ShowResultByPatient() == 0)
                    {

                        MessageBox.Show("查看PACS结果失败！请再查看一次！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查看PACS结果出现错误！请再查看一次！\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void fpOrder_ActiveSheetChanged(object sender, EventArgs e)
        {
            ucItemSelect1.LongOrShort = fpOrder.ActiveSheetIndex;
            if (fpOrder.ActiveSheetIndex == 0)
            {
                //this.plPatient.BackColor = Color.AliceBlue;// Color.FromArgb(255, 255, 192);
                this.OrderType = FS.HISFC.Models.Order.EnumType.LONG;
                this.ActiveRowIndex = -1;
                if (this.OrderCanOperatorChanged != null)
                    this.OrderCanOperatorChanged(false);
            }
            else
            {
                //this.plPatient.BackColor = Color.AliceBlue; //Color.FromArgb(225, 255, 255);
                this.OrderType = FS.HISFC.Models.Order.EnumType.SHORT;
                this.ActiveRowIndex = -1;
                if (this.bIsDesignMode)
                {
                    if (this.OrderCanOperatorChanged != null)
                        this.OrderCanOperatorChanged(true);
                }
                else
                {
                    if (this.OrderCanOperatorChanged != null)
                        this.OrderCanOperatorChanged(false);
                }
            }
            try
            {
                ////清空已开立医嘱数据显示  Add By liangjz 2005-08
                //this.ucItemSelect1.Clear();
                this.fpOrder.Sheets[fpOrder.ActiveSheetIndex].ClearSelection();
                this.RefreshOrderState(-1);
                if (this.IsDesignMode)
                {
                    this.ucItemSelect1.Clear(false);
                }

                this.fpOrder.ShowRow(0, 0, VerticalPosition.Center);
                //this.fpOrder.ShowRow(0, this.fpOrder.ActiveSheet.RowCount - 1, VerticalPosition.Center);
            }
            catch { }
        }

        #region 根据患者类别获取开立科室

        /// <summary>
        /// 根据开立科室、患者类别获取执行科室
        /// 仅用于药品
        /// </summary>
        /// <param name="patientType"></param>
        /// <param name="reciptDept"></param>
        /// <returns></returns>
        private FS.FrameWork.Models.NeuObject GetExecDept(ReciptPatientType patientType, FS.FrameWork.Models.NeuObject reciptDept)
        {
            if (patientType == ReciptPatientType.ConsultationPatient//会诊患者
                || patientType == ReciptPatientType.AuthorizedPatient//授权患者
                || patientType == ReciptPatientType.FindedPatient//查找患者
                || patientType == ReciptPatientType.MedicsPatient)//医疗组患者
            {
                return this.myPatientInfo.PVisit.PatientLocation.Dept;
                //return reciptDept;
            }
            else if (patientType == ReciptPatientType.PrivatePatient//分管患者
                || patientType == ReciptPatientType.DeptPatient)//科室患者
            {
                return reciptDept;
            }

            return reciptDept;
        }

        #endregion

        #region 合理用药

        /// <summary>
        /// 初始化合理用药
        /// </summary>
        private void InitPass()
        {
            this.InitReasonableMedicine();

            if (this.IReasonableMedicine == null)
            {
                return;
            }
            StartReasonableMedicine();
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
            iReturn = this.IReasonableMedicine.PassInit(empl, empl.Dept, "10");
            if (iReturn == -1)
            {
                this.enabledPass = false;
                if (!string.IsNullOrEmpty(IReasonableMedicine.Err))
                {
                    MessageBox.Show(IReasonableMedicine.Err);
                }
            }
            if (iReturn == 0)
            {
                this.enabledPass = false;
                //MessageBox.Show("合理用药服务器未启动,不能进行用药审查,请重新登陆工作站！");
            }
        }

        /// <summary>
        /// 合理药品系统药品查询  Add By liangjz 2005-11
        /// </summary>
        private void mnuPass_Click(object sender, EventArgs e)
        {
            if (this.IReasonableMedicine != null
                && !this.IReasonableMedicine.PassEnabled)
            {
                return;
            }

            this.IReasonableMedicine.PassShowFloatWindow(false);


            FS.HISFC.Models.Order.Inpatient.Order info = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, fpOrder.ActiveSheetIndex);
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
            IReasonableMedicine.PassShowOtherInfo(info, new FS.FrameWork.Models.NeuObject("", muItem.Text, ""), ref alMenu);

            #region 旧的作废

            //if (this.IReasonableMedicine != null && !this.IReasonableMedicine.PassEnabled)
            //    return;
            //ToolStripItem muItem = sender as ToolStripItem;
            //switch (muItem.Text)
            //{

            //    #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} 添加合理用药右键菜单

            //    #region 一级菜单

            //    case "过敏史/病生状态":
            //        int iReg;
            //        this.IReasonableMedicine.PassSetPatientInfo(this.myPatientInfo, this.empl.ID, this.empl.Name);
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

            #endregion
        }

        /// <summary>
        /// 初始化合理用药接口
        /// </summary>
        private void InitReasonableMedicine()
        {
            if (this.IReasonableMedicine == null)
            {
                this.IReasonableMedicine = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine)) as FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine;
            }
        }

        /// <summary>
        /// 合理用药系统中查看审查结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpOrder_CellClick(object sender, CellClickEventArgs e)
        {
            #region 单击显示合理用药信息的没用了，先屏蔽，如果主动调用就用双击吧

            if (false)
            {
                if (this.IReasonableMedicine != null
                    && IReasonableMedicine.PassEnabled)
                {
                    if (e.RowHeader || e.ColumnHeader)
                    {
                        return;
                    }
                    try
                    {
                        this.IReasonableMedicine.PassShowFloatWindow(false);

                        int iSheetIndex = this.OrderType == FS.HISFC.Models.Order.EnumType.SHORT ? 1 : 0;

                        FS.HISFC.Models.Order.Inpatient.Order info = this.GetObjectFromFarPoint(e.Row, iSheetIndex);

                        if (info == null)
                        {
                            return;
                        }
                        if (info.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            return;
                        }

                        //点击名称列显示要点提示
                        //if (e.Column == this.myOrderClass.GetColumnIndexFromName("医嘱名称"))
                        //{
                        ////貌似他们只和右下角的坐标位置相关
                        //if (this.IReasonableMedicine.PassShowSingleDrugInfo(info,
                        //    new Point(MousePosition.X, MousePosition.Y - 60),
                        //    new Point(MousePosition.X + 100, MousePosition.Y + 15), false) == -1)
                        //{
                        //    MessageBox.Show(IReasonableMedicine.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                        //}
                        //点击合理用药警示级别显示及时性警示浮动窗口
                        //else if (e.Column == myOrderClass.GetColumnIndexFromName("警"))
                        //{
                        //    if (this.IReasonableMedicine.PassShowWarnDrug(info) == -1)
                        //    {
                        //        MessageBox.Show(IReasonableMedicine.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            #endregion
        }
        /// <summary>
        /// 取临时医嘱页中的医嘱项目,传给化疗窗口
        /// </summary>
        /// <returns>null或0失败</returns>
        public ArrayList GetUnSavedOrders()
        {
            ArrayList alOrders = new ArrayList();

            for (int i = 0; i < this.fpOrder.Sheets.Count; i++)
            {
                for (int j = 0; j < this.fpOrder.Sheets[i].Rows.Count; j++)
                {
                    FS.HISFC.Models.Order.Inpatient.Order tempOrder = this.GetObjectFromFarPoint(j, i);

                    if (tempOrder != null && tempOrder.ID.Length <= 0)
                    {
                        alOrders.Add(tempOrder);
                    }
                }
            }

            return alOrders;
        }
        /// <summary>
        /// 退出合理用药系统
        /// </summary>
        public void QuitPass()
        {
            try
            {
                if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
                {
                    this.IReasonableMedicine.PassClose();
                }
            }
            catch { }
        }

        /// <summary>
        /// 合理用药系统中查看审查结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpOrder_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.IReasonableMedicine != null
                && this.IReasonableMedicine.PassEnabled)
            {
                this.IReasonableMedicine.PassShowFloatWindow(false);

                if (!e.RowHeader && !e.ColumnHeader)
                {
                    int iSheetIndex = this.OrderType == FS.HISFC.Models.Order.EnumType.SHORT ? 1 : 0;
                    FS.HISFC.Models.Order.Inpatient.Order info = this.GetObjectFromFarPoint(FS.FrameWork.Function.NConvert.ToInt32(this.ActiveTempID), iSheetIndex);
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
                        MessageBox.Show(ex.Message);
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 向合理用药系统传送当前医嘱进行审查
        /// </summary>
        ///<param name="checkType">审查方式 1 自动审查 12 用药研究  3 手工审查</param>
        /// <param name="warnPicFlag">是否显示图片警世信息</param>
        public int PassCheckOrder(ArrayList alPassOrder, bool isSave)
        {
            ArrayList alOrder = new ArrayList();
            FS.HISFC.Models.Order.Inpatient.Order order;
            DateTime sysTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            #region 审查长嘱

            for (int i = 0; i < this.fpOrder_Long.Rows.Count; i++)
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
                if (this.helper != null)
                {
                    if (order.Frequency != null)
                    {
                        order.Frequency = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID);
                    }
                }
                order.ApplyNo = CacheManager.InOrderMgr.GetSequence("Order.Pass.Sequence");
                alOrder.Add(order);
            }
            #endregion

            #region 审查临嘱

            for (int i = 0; i < this.fpOrder_Short.Rows.Count; i++)
            {
                order = this.GetObjectFromFarPoint(i, 1);

                if (order == null)
                {
                    continue;
                }
                if (order.Status == 3)
                {
                    continue;
                }
                if (order.MOTime.Date != sysTime.Date)
                {
                    continue;
                }
                if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    continue;
                }
                if (this.helper != null)
                {
                    if (order.Frequency != null)
                    {
                        order.Frequency = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID);
                    }
                }
                order.ApplyNo = CacheManager.InOrderMgr.GetSequence("Order.Pass.Sequence");
                alOrder.Add(order);
            }
            #endregion

            if (alOrder.Count > 0)
            {
                ArrayList diagList = CacheManager.DiagMgr.QueryCaseDiagnose(this.Patient.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);


                if (this.IReasonableMedicine.PassSetDiagnoses(diagList) == -1)
                {


                }


                int rev = this.IReasonableMedicine.PassDrugCheck(alOrder, isSave);

                if (rev == -1)
                {
                    MessageBox.Show("合理用药审查出错：" + this.IReasonableMedicine.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else
                {
                    if (rev == 0)
                    {
                        return -1;
                    }
                }
            }
            return 1;
        }

        #endregion

        #region 动态获取列名和顺序

        /// <summary>
        /// 设置各列列头
        /// </summary>
        /// <param name="sheetIndex"></param>
        private void SetColumnNameNew(int sheetIndex)
        {
            foreach (string colmName in dicColmSet.Keys)
            {
                this.fpOrder.Sheets[sheetIndex].Columns[dicColmSet[colmName]].Label = colmName;
            }
        }

        private void SetDataSet(ref System.Data.DataSet dataSet)
        {
            DataTable table = new DataTable("Table");
            //table.Columns.Count = dicColmSet.Count;

            foreach (string colmName in dicColmSet.Keys)
            {
                table.Columns.Add(new DataColumn(colmName));
            }
            dataSet.Tables.Add(table);
        }

        /// <summary>
        /// 获取医嘱实体
        /// </summary>
        /// <param name="i"></param>
        /// <param name="SheetIndex"></param>
        /// <param name="OrderManagement"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Inpatient.Order GetObjectFromFarPoint(int i, int SheetIndex)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.Sheets[SheetIndex].Rows[i].Tag != null)
            {
                order = this.fpOrder.Sheets[SheetIndex].Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
            }
            #region 从哈希表中取值
            else if (this.htOrder != null && this.htOrder.ContainsKey(this.fpOrder.Sheets[SheetIndex].Cells[i, dicColmSet["医嘱流水号"]].Text))
            {
                order = this.htOrder[this.fpOrder.Sheets[SheetIndex].Cells[i, dicColmSet["医嘱流水号"]].Text] as FS.HISFC.Models.Order.Inpatient.Order;
            }
            #endregion
            else
            {
                #region 付值
                order = CacheManager.InOrderMgr.QueryOneOrder(this.fpOrder.Sheets[SheetIndex].Cells[i, dicColmSet["医嘱流水号"]].Text);
                #endregion
            }
            return order;
        }

        #region 存储医嘱的哈希表、提高医嘱查询速度
        private System.Collections.Hashtable htOrder = new System.Collections.Hashtable();

        public System.Collections.Hashtable HtOrder
        {
            get
            {
                return htOrder;
            }
            set
            {
                htOrder = value;
            }
        }

        #endregion


        #region

        #region

        public void AddPathwayOrders(ArrayList alOrders)
        {
            ArrayList alHerbal = new ArrayList(); //草药

            string comboID = "";
            int subCombNo = 0;
            FS.HISFC.Models.Order.Inpatient.Order myorder = null;

            try
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
                {
                    myorder = order.Clone();
                    if (myorder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).DoseUnit = myorder.DoseUnit;
                    }

                    myorder.Patient.PVisit.PatientLocation.Dept.ID = CacheManager.LogEmpl.Dept.ID;
                    if (fillOrder(ref myorder) != -1)
                    {


                        if (myorder.Item.ID == "999")
                        {
                            myorder.ExeDept.ID = "";
                        }


                        if (myorder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                        {
                            if (order.Combo.ID != "" && order.Combo.ID != comboID)//新的
                            {
                                subCombNo = GetMaxCombNo(order, 0);
                            }
                            comboID = order.Combo.ID;
                            myorder.SubCombNO = subCombNo;

                            myorder.FirstUseNum = Classes.Function.GetFirstOrderDays(myorder, CacheManager.InOrderMgr.GetDateTimeFromSysDateTime()).ToString();
                            myorder.GetFlag = "0";
                            myorder.RowNo = -1;
                            myorder.PageNo = -1;
                            this.AddNewOrder(myorder, 0);

                        }
                        else
                        {
                            if (order.Combo.ID != "" && order.Combo.ID != comboID)//新的
                            {
                                subCombNo = GetMaxCombNo(order, 1);
                            }
                            comboID = order.Combo.ID;
                            myorder.SubCombNO = subCombNo;

                            myorder.GetFlag = "0";
                            myorder.RowNo = -1;
                            myorder.PageNo = -1;
                            this.AddNewOrder(myorder, 1);
                        }

                    }
                }

                Classes.Function.ShowBalloonTip(3, "提示", "请注意检查执行科室是否正确！", ToolTipIcon.Info);
                this.RefreshCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion



        /// <summary>
        /// 判断组合医嘱
        /// </summary>
        /// <param name="fpOrder"></param>
        /// <returns></returns>
        public int ValidComboOrder()
        {
            FS.HISFC.Models.Order.Frequency frequency = null;
            FS.FrameWork.Models.NeuObject usage = null;
            FS.FrameWork.Models.NeuObject exeDept = null;
            string sample = "";
            string firstDay = "";
            decimal amount = 0;
            int sysclass = -1;
            string sysClassID = string.Empty;
            DateTime dtBegin = new DateTime();


            for (int i = 0; i < fpOrder.ActiveSheet.Rows.Count; i++)
            {
                if (fpOrder.ActiveSheet.IsSelected(i, 0))
                {
                    FS.HISFC.Models.Order.Inpatient.Order o = this.GetObjectFromFarPoint(i, fpOrder.ActiveSheetIndex);

                    if (o.Status != 0)
                    {

                        MessageBox.Show(string.Format("不符合组合条件，项目{0}状态不允许修改，请重新选择！", o.Item.Name));
                        return -1;
                    }
                    if (!String.IsNullOrEmpty(o.ApplyNo))
                    {
                        MessageBox.Show(string.Format("不符合组合条件，项目{0}已经开立申请单，请删除重新开立组合！", o.Item.Name));
                        return -1;

                    }


                    if (frequency == null)
                    {
                        frequency = o.Frequency.Clone();
                        usage = o.Usage.Clone();
                        sysclass = o.Item.SysClass.ID.GetHashCode();
                        sysClassID = o.Item.SysClass.ID.ToString();
                        exeDept = o.ExeDept.Clone();
                        sample = o.Sample.Name;
                        amount = o.Qty;
                        dtBegin = o.BeginTime;
                        firstDay = o.FirstUseNum;
                    }
                    else
                    {
                        o.BeginTime = dtBegin;
                        if (o.Frequency.ID != frequency.ID)
                        {
                            MessageBox.Show("频次不同，不可以组合用！");
                            return -1;
                        }
                        if (o.OrderType.IsDecompose)
                        {
                            if (o.FirstUseNum != firstDay)
                            {
                                MessageBox.Show("首日量不同，不可以组合用！");
                                return -1;
                            }
                        }

                        //只对药品判断用法是否相同
                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)		//只对药品判断用法是否相同
                        {
                            if (o.Item.SysClass.ID.ToString() != "PCC" && o.Usage.ID != usage.ID)
                            {
                                MessageBox.Show("用法不同，不可以组合用！");
                                return -1;
                            }
                            if (sysClassID == "PCC")
                            {
                                if (o.Item.SysClass.ID.ToString() != sysClassID)
                                {
                                    MessageBox.Show("草药不可以和其他药品组合用！");
                                    return -1;
                                }
                            }
                            else
                            {
                                if (o.Item.SysClass.ID.ToString() == "PCC")
                                {
                                    MessageBox.Show("草药不可以和其他药品组合用！");
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
                                    MessageBox.Show("检验数量不同，不可以组合用！");
                                    return -1;
                                }
                                if (o.Sample.Name != sample)
                                {
                                    MessageBox.Show("检验样本不同，不可以组合用！");
                                    return -1;
                                }
                            }

                            if (o.Item.SysClass.ID.ToString() != sysClassID)
                            {
                                MessageBox.Show("系统类别不同，不可以组合用！");
                                return -1;
                            }
                        }


                        if (o.ExeDept.ID != exeDept.ID)
                        {
                            MessageBox.Show("执行科室不同，不能组合使用!", "提示");
                            return -1;
                        }
                    }
                }

            }
            return 0;

        }

        public Dictionary<string, int> dicColmSet = new Dictionary<string, int>();

        /// <summary>
        /// 动态获取列信息
        /// </summary>
        /// <returns></returns>
        private int GetColmSet()
        {
            //if (System.IO.File.Exists(""))
            //{
            //}
            //else
            //{
            dicColmSet.Add("!", 0);
            dicColmSet.Add("期效", 1);
            dicColmSet.Add("医嘱类型", 2);
            dicColmSet.Add("医嘱流水号", 3);
            dicColmSet.Add("医嘱状态", 4);
            dicColmSet.Add("组合号", 5);
            dicColmSet.Add("主药", 6);
            dicColmSet.Add("组号", 7);
            dicColmSet.Add("开立时间", 8);
            dicColmSet.Add("开立医生", 9);
            dicColmSet.Add("顺序号", 10);
            dicColmSet.Add("医嘱名称", 11);
            dicColmSet.Add("组", 12);
            dicColmSet.Add("首日量", 13);
            dicColmSet.Add("每次用量", 14);
            dicColmSet.Add("单位", 15);
            dicColmSet.Add("频次", 16);
            dicColmSet.Add("频次名称", 17);
            dicColmSet.Add("用法编码", 18);
            dicColmSet.Add("用法", 19);
            dicColmSet.Add("总量", 20);
            dicColmSet.Add("总量单位", 21);
            dicColmSet.Add("付数", 22);
            dicColmSet.Add("系统类别", 23);
            dicColmSet.Add("开始时间", 24);
            dicColmSet.Add("结束时间", 25);
            dicColmSet.Add("停止时间", 26);
            dicColmSet.Add("执行科室编码", 27);
            dicColmSet.Add("执行科室", 28);
            dicColmSet.Add("急", 29);
            dicColmSet.Add("检查部位", 30);
            dicColmSet.Add("样本类型", 31);
            dicColmSet.Add("取药药房编码", 32);
            dicColmSet.Add("取药药房", 33);
            dicColmSet.Add("备注", 34);
            dicColmSet.Add("录入人编码", 35);
            dicColmSet.Add("录入人", 36);
            dicColmSet.Add("开立科室", 37);
            dicColmSet.Add("停止人编码", 38);
            dicColmSet.Add("停止人", 39);
            dicColmSet.Add("滴速", 40);
            dicColmSet.Add("国家医保代码", 41);

            //}
            return 1;
        }

        #endregion
    }

    /// <summary>
    /// 设定列头显示
    /// </summary>
    public class ColmSet
    {
        public static string 叹号 = "!";
        public static string Q期效 = "期效";
        public static string Y医嘱类型 = "医嘱类型";
        public static string Y医嘱流水号 = "医嘱流水号";
        public static string Y医嘱状态 = "医嘱状态";
        public static string Z组合号 = "组合号";
        public static string Z主药 = "主药";
        public static string Z组号 = "组号";
        public static string K开立时间 = "开立时间";
        public static string K开立医生 = "开立医生";
        public static string S顺序号 = "顺序号";
        public static string Y医嘱名称 = "医嘱名称";
        public static string Z组 = "组";
        public static string S首日量 = "首日量";
        public static string M每次用量 = "每次用量";

        /// <summary>
        /// 每次用量的单位
        /// </summary>
        public static string D单位 = "单位";
        public static string P频次 = "频次";
        public static string P频次名称 = "频次名称";
        public static string Y用法编码 = "用法编码";
        public static string Y用法 = "用法";
        public static string Z总量 = "总量";
        public static string Z总量单位 = "总量单位";
        public static string F付数 = "付数";
        public static string X系统类别 = "系统类别";
        public static string K开始时间 = "开始时间";
        public static string J结束时间 = "结束时间";
        public static string T停止时间 = "停止时间";
        public static string Z执行科室编码 = "执行科室编码";
        public static string Z执行科室 = "执行科室";
        public static string J急 = "急";
        public static string J检查部位 = "检查部位";
        public static string Y样本类型 = "样本类型";
        public static string Q取药药房编码 = "取药药房编码";
        public static string Q取药药房 = "取药药房";
        public static string B备注 = "备注";
        public static string L录入人编码 = "录入人编码";
        public static string L录入人 = "录入人";
        public static string K开立科室 = "开立科室";
        public static string T停止人编码 = "停止人编码";
        public static string T停止人 = "停止人";
        public static string D滴速 = "滴速";
        public static string G国家医保代码 = "国家医保代码";

        public static string ALL = "新增";
    }

    /// <summary>
    /// 开立患者类别
    /// </summary>
    public enum ReciptPatientType
    {
        /// <summary>
        /// 分管患者
        /// </summary>
        [FS.FrameWork.Public.Description("分管患者")]
        PrivatePatient = 0,

        /// <summary>
        /// 科室患者
        /// </summary>
        [FS.FrameWork.Public.Description("科室患者")]
        DeptPatient = 1,

        /// <summary>
        /// 会诊患者
        /// </summary>
        [FS.FrameWork.Public.Description("会诊患者")]
        ConsultationPatient,

        /// <summary>
        /// 授权患者
        /// </summary>
        [FS.FrameWork.Public.Description("授权患者")]
        AuthorizedPatient,

        /// <summary>
        /// 查找患者
        /// </summary>
        [FS.FrameWork.Public.Description("查找患者")]
        FindedPatient,

        /// <summary>
        /// 医疗组内患者
        /// </summary>
        [FS.FrameWork.Public.Description("医疗组内患者")]
        MedicsPatient
    }


    /// <summary>
    /// 医嘱过滤
    /// </summary>
    public enum EnumFilterList
    {
        /// <summary>
        /// 全部医嘱
        /// </summary>
        All = 0,

        /// <summary>
        /// 当天医嘱
        /// </summary>
        Today = 1,

        /// <summary>
        /// 有效医嘱
        /// </summary>
        Valid = 2,

        /// <summary>
        /// 无效医嘱
        /// </summary>
        Invalid = 3,

        /// <summary>
        /// 新开立医嘱
        /// </summary>
        New = 4,

        /// <summary>
        /// 检查检验医嘱
        /// </summary>
        UC_ULOrder = 5
    }

    /// <summary>
    /// 医嘱按照序号sortid排序
    /// </summary>
    public class OrderSortIDCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.Order order1 = x as FS.HISFC.Models.Order.Order;
                FS.HISFC.Models.Order.Order order2 = y as FS.HISFC.Models.Order.Order;
                if (order1.SortID > order2.SortID)
                {
                    return 1;
                }
                else if (order1.SortID == order2.SortID)
                {
                    return string.Compare(order1.ID, order2.ID);
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
