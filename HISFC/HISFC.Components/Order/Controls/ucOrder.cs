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
    /// [��������: ҽ������]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucOrder : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOrder()
        {
            InitializeComponent();
            this.contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
        }

        #region ����
        public delegate void EventButtonHandler(bool b);
        //public event EventButtonHandler OrderCanComboChanged;//ҽ���Ƿ��������¼�

        /// <summary>
        /// ҽ���Ƿ����ȡ������¼�
        /// </summary>
        public event EventButtonHandler OrderCanCancelComboChanged;

        /// <summary>
        /// ҽ���Ƿ���Ե����������/����
        /// </summary>
        public event EventButtonHandler OrderCanOperatorChanged;
        //public event EventButtonHandler OrderCanSaveChanged;	//ҽ���Ƿ񱣴�

        /// <summary>
        /// �Ƿ�ɴ�ӡ��鵥�¼�
        /// </summary>
        public event EventButtonHandler OrderCanSetCheckChanged;

        private bool needUpdateDTBegin = true;
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = null;

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get { return this.myPatientInfo; }
            set { this.myPatientInfo = value; }
        }

        /// <summary>
        /// ����֮ǰ�ĳ���ҽ������
        /// </summary>
        public int CountLongBegin;

        /// <summary>
        /// ����֮ǰ����ʱҽ������
        /// </summary>
        public int CountShortBegin;

        /// <summary>
        /// �Ƿ����������ҩ���
        /// </summary>
        private bool enabledPass = true;

        /// <summary>
        /// �Ƿ����ú�����ҩ
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
        /// �Ƿ�������ױ༭����
        /// </summary>
        protected bool EditGroup = false;

        /// <summary>
        /// ִ�п�����Ϣ(pivas����)
        /// </summary>
        private ArrayList deptItemList = null;
        /// <summary>
        /// �÷�(pivas����)
        /// </summary>
        private ArrayList drugUsageList = null;
        /// <summary>
        /// ����ҽ���Ƿ�ͨpivas���ܱ�ʶ:1��|0��
        /// </summary>
        private string pivasCzFlag = "";
        /// <summary>
        /// ��ʱҽ���Ƿ�ͨpivas���ܱ�ʶ:1��|0��
        /// </summary>
        private string pivasLzFlag = "";

        private DataSet dataSet = null; //��ǰDataSet
        private DataView dvLong = null;//��ǰDataView
        private DataView dvShort = null;//��ǰDataView

        /// <summary>
        /// ���Sort
        /// </summary>
        private int MaxSort = 0;

        /// <summary>
        /// ��ŵ�ǰ����ķ���
        /// </summary>
        [Obsolete("���ϣ��޸Ļ�ȡ���ŷ�ʽ", true)]
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
        /// ������г�������
        /// </summary>
        [Obsolete("���ϣ��޸Ļ�ȡ���ŷ�ʽ", true)]
        private Hashtable hsLongSubCombNo = new Hashtable();

        /// <summary>
        /// ���������������
        /// </summary>
        [Obsolete("���ϣ��޸Ļ�ȡ���ŷ�ʽ", true)]
        private Hashtable hsShortSubCombNo = new Hashtable();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;
        protected FS.HISFC.BizLogic.Order.AdditionalItem AdditionalItemManagement = new FS.HISFC.BizLogic.Order.AdditionalItem();

        protected FS.HISFC.BizLogic.Order.PacsBill pacsBillManagement = new FS.HISFC.BizLogic.Order.PacsBill();

        /// <summary>
        /// �Ƿ��¼ӣ��޸�ʱ��
        /// </summary>
        protected bool dirty = false;
        protected DataSet dsAllLong;
        protected DataSet dsAllShort;
        protected FS.HISFC.Models.Order.Inpatient.Order currentOrder = null;

        /// <summary>
        /// ����������ʾ��Ч����������
        /// </summary>
        private int shortOrderShowDays = 1000;

        /// <summary>
        /// �Ƿ�Ĭ�ϳ���ҽ��// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        private bool isDefaultLONG = true;

        public string LONGSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "longordersetting.xml";
        public string SHORTSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "shortordersetting.xml";

        /// <summary>
        /// ����ҽ����ʾ������
        /// </summary>
        private int allOrderShowDays = 100;

        /// <summary>
        /// �Ƿ���������ҩ����//{EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
        /// </summary>
        private bool isOpenDrugWarn = false;

        private FS.FrameWork.Public.ObjectHelper helper; //��ǰHelper

        /// <summary>
        /// ˢ��ҽ������ 0 ˢ�³��� 1 ˢ������ 2 ��������ȫ��ˢ��
        /// </summary>
        private string refreshComboFlag = "2";

        private Order myOrderClass = new Order();
        ToolTip tooltip = new ToolTip();
        private FS.HISFC.BizProcess.Interface.Common.ICheckPrint checkPrint = null;

        /// <summary>
        /// ҽ��ԤԼ������
        /// </summary>
        FS.HISFC.BizLogic.MedicalTechnology.Appointment appMgr = new FS.HISFC.BizLogic.MedicalTechnology.Appointment();

        /// <summary>
        /// ҽ����չ��Ϣ����
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
        /// ҽ��Ȩ����֤
        /// </summary>
        //public bool isCheckPopedom = false;

        /// <summary>
        /// �Ƿ��д���Ȩ
        /// </summary>
        //public bool isHaveOrderPower = false;

        /// <summary>
        /// ҽ����Ϣ����ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IAlterOrder IAlterOrderInstance = null;

        /// <summary>
        /// ���鷽��ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST IRecipePrintST = null;

        //{6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
        //protected FS.ApplyInterface.HisInterface PACSApplyInterface = null;

        /// <summary>
        /// LIS�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.ILis lisInterface = null;

        /// <summary>
        /// ҽ��վ���Ĵ���ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IDealSubjob IDealSubjob = null;

        /// <summary>
        /// {F38618E9-7421-423d-80A9-401AFED0B855} xuc
        /// ���ˢ����ʾ����ҽ����Ϣ��־
        /// </summary>
        private bool isShowOrderFinished = true;


        /// <summary>
        /// �����Ƿ������������Ϊ��������������Ϊ����// {45652500-8594-40ac-A92E-FFFEB812655C}
        /// </summary>
        private bool isModifyOrderType = true;

        /// <summary>
        /// ��ǰ��¼��Ϣ
        /// </summary>
        Employee empl = FrameWork.Management.Connection.Operator as Employee;

        /// <summary>
        /// ������ҩ�ӿ�
        /// </summary>
        IReasonableMedicine IReasonableMedicine = null;

        /// <summary>
        /// Сʱ�Ʒѵ�ҽ��Ƶ�δ��� {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
        /// </summary>
        private string hoursFrequencyID = string.Empty;

        /// <summary>
        /// �Ƿ����õ������뵥 
        /// </summary>
        private bool isUsePACSApplySheet = false;

        /// <summary>
        /// ��������ʱˢ��������
        /// </summary>
        public event EventHandler OnRefreshGroupTree;

        private Hashtable htSubs = new Hashtable();

        public event RefreshGroupTree refreshGroup;

        /// <summary>
        /// ������Ŀǰ�����ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem IBeforeAddItem = null;

        /// <summary>
        /// ����󴦷�����ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.ISaveOrder IAfterSaveOrder = null;

        /// <summary>
        /// ���洦��ǰ����
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder IBeforeSaveOrder = null;

        /// <summary>
        /// ��������ǰ�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddOrder IBeforeAddOrder = null;

        /// <summary>
        /// �ٴ�·��
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.IClinicPath iClinicPath = null;

        /// <summary>
        /// �����������
        /// </summary>
        private ReciptPatientType patientType = ReciptPatientType.DeptPatient;

        /// <summary>
        /// �����������
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
        /// ҽ����������ҩҩƷ�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper indicationsHelper = null;

        /// <summary>
        /// ҽ����������ҩ��ҩ
        /// </summary>
        private ArrayList alIndicationsDrug = null;

        Forms.frmDCOrderAndZG frmDCOrderAndZG1 = null;
        Forms.frmDCTreatmentType frmDCTreatmentType = null;

        /// <summary>
        /// ����������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();


        /// <summary>
        /// ����������
        /// </summary>
        private static FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();



        /// <summary>
        /// �÷��б�
        /// </summary>
        ArrayList usageList = null;


        #endregion

        #region �ӿ�


        /// <summary>
        /// ҽ����ӡ�ӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint IInPatientOrderPrint = null;



        /// <summary>
        /// ����ӡ�ӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint IInPatientPacsOrderPrint = null;



        /// <summary>
        /// ���뵥�ӿ�
        /// </summary>
        private static FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInpateintPacsApply IInpateintPacsApply = null;

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ����Loading
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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ش��ڣ������Ժ�!");
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
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���������ϣ��ף���л�������ĵȴ�O(��_��)O");

                #region ��ȡ���Ʋ���  ��������ʾ��ֹͣҽ��ʱ����(��λΪ��) Ĭ����ʾ1000�����ڵ�
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

                this.lblDisplay.Text = "Ĭ����ʾ" + this.allOrderShowDays.ToString() + "����ȫ��ҽ����ȫ����Ч������" + this.shortOrderShowDays.ToString() + "������Ч����";

                #endregion
                this.hoursFrequencyID = CacheManager.ContrlManager.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.MetConstant.Hours_Frequency_ID, false, "NONE");

                #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
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

                #region �ӿ�

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
            if (currDept.HospitalName.Contains("˳��"))
            {
                hospitalname = "˳�°���������ҽԺ";
                hospitalybcode = "H44060600494";
            }
            else
            {
                hospitalname = "���ݰ���������ҽԺ";
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


            base.OnStatusBarInfo(null, "(��ɫ���¿�)(��ɫ�����)(��ɫ��ִ��)(��ɫ������)(��ɫ��Ԥֹͣ)    �������ƣ�" + hospitalname + "  ����ҽ�����룺" + hospitalybcode + "  ҽ��ҽʦ���룺" + gjcode + "");


            #region ������ҩ
            InitPass();
            #endregion

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("��л�ף����ڼ������:-)");
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
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            this.myOrderClass.fpSpread1 = this.fpOrder;

            #region ��ʼ��ucItemSelect
            this.ucItemSelect1.LongOrShort = 0;//����Ϊ����ҽ��
            this.ucItemSelect1.OperatorType = Operator.Add;//���ģʽ
            #endregion

            #region ��ʼ����������DataSet

            dsAllLong = this.InitDataSet();
            dsAllShort = this.InitDataSet();
            this.myOrderClass.dsAllLong = dsAllLong;

            this.fpOrder.Sheets[0].DataSource = dsAllLong.Tables[0];
            this.fpOrder.Sheets[1].DataSource = dsAllShort.Tables[0];
            #endregion

            //this.myOrderClass.ColumnSet();
            SetFP();
            InitFP();

            #region FarPoint �¼�

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

            #region "�д�С"

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
                    fpOrder.Sheets[index].Columns[dicColmSet["��Ч"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["ҽ������"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["ҽ������"]].Width = 63;

                    fpOrder.Sheets[index].Columns[dicColmSet["ҽ����ˮ��"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["ҽ��״̬"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["��Ϻ�"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["��ҩ"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["���"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["���"]].Width = 20;
                    fpOrder.Sheets[index].Columns[dicColmSet["����ʱ��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["����ʱ��"]].Width = 112;
                    fpOrder.Sheets[index].Columns[dicColmSet["����ҽ��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["����ҽ��"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["˳���"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["ҽ������"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["ҽ������"]].Width = 234;
                    fpOrder.Sheets[index].Columns[dicColmSet["��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["��"]].Width = 24;
                    if (index == 0)
                    {
                        fpOrder.Sheets[index].Columns[dicColmSet["������"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["������"]].Width = 40;
                        fpOrder.Sheets[index].Columns[dicColmSet["����"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["����"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["������λ"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["��鲿λ"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["��������"]].Visible = false;
                    }
                    else
                    {
                        fpOrder.Sheets[index].Columns[dicColmSet["������"]].Visible = false;
                        fpOrder.Sheets[index].Columns[dicColmSet["����"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["����"]].Width = 25;
                        fpOrder.Sheets[index].Columns[dicColmSet["����"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["����"]].Width = 27;
                        fpOrder.Sheets[index].Columns[dicColmSet["������λ"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["������λ"]].Width = 40;
                        fpOrder.Sheets[index].Columns[dicColmSet["��鲿λ"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["��鲿λ"]].Width = 63;
                        fpOrder.Sheets[index].Columns[dicColmSet["��������"]].Visible = true;
                        fpOrder.Sheets[index].Columns[dicColmSet["��������"]].Width = 63;
                    }
                    fpOrder.Sheets[index].Columns[dicColmSet["ÿ������"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["ÿ������"]].Width = 44;

                    fpOrder.Sheets[index].Columns[dicColmSet["��λ"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["��λ"]].Width = 30;
                    fpOrder.Sheets[index].Columns[dicColmSet["Ƶ��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["Ƶ��"]].Width = 30;
                    fpOrder.Sheets[index].Columns[dicColmSet["Ƶ������"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["�÷�����"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["�÷�"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["�÷�"]].Width = 54;
                    fpOrder.Sheets[index].Columns[dicColmSet["����"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["����"]].Width = 39;
                    fpOrder.Sheets[index].Columns[dicColmSet["������λ"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["������λ"]].Width = 37;
                    fpOrder.Sheets[index].Columns[dicColmSet["ϵͳ���"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["ϵͳ���"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["��ʼʱ��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["��ʼʱ��"]].Width = 111;
                    fpOrder.Sheets[index].Columns[dicColmSet["����ʱ��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["����ʱ��"]].Width = 111;
                    fpOrder.Sheets[index].Columns[dicColmSet["ֹͣʱ��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["ֹͣʱ��"]].Width = 111;
                    fpOrder.Sheets[index].Columns[dicColmSet["ִ�п��ұ���"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["ִ�п���"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["ִ�п���"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["��"]].Width = 18;
                    fpOrder.Sheets[index].Columns[dicColmSet["ȡҩҩ������"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["ȡҩҩ��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["ȡҩҩ��"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["��ע"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["��ע"]].Width = 98;
                    fpOrder.Sheets[index].Columns[dicColmSet["¼���˱���"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["¼����"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["��������"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["��������"]].Width = 63;
                    fpOrder.Sheets[index].Columns[dicColmSet["ֹͣ�˱���"]].Visible = false;
                    fpOrder.Sheets[index].Columns[dicColmSet["ֹͣ��"]].Visible = true;
                    fpOrder.Sheets[index].Columns[dicColmSet["ֹͣ��"]].Width = 50;
                }

                SaveFpStyle();
            }

            #endregion
        }

        /// <summary>
        /// ��ʼ��DataSet
        /// </summary>
        /// <returns></returns>
        private DataSet InitDataSet()
        {
            dataSet = new DataSet();
            myOrderClass.SetDataSet(ref dataSet);
            return dataSet;
        }



        /// <summary>
        /// ҽ��վ��ӡ�ӿ�
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
        /// ���뵥�ӿ�
        /// </summary>
        private void InitPacsApply()
        {
            if (IInpateintPacsApply == null)
            {
                IInpateintPacsApply = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInpateintPacsApply)) as FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInpateintPacsApply;
            }
        }

        #endregion

        #region IToolBar ��Ա

        /// <summary>
        /// �˳�ҽ������
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
        /// ɾ��ҽ��
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return Delete(this.fpOrder.ActiveSheet.ActiveRowIndex, false);
        }

        /// <summary>
        /// {D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        /// <param name="rowIndex">ɾ��������</param>
        /// <param name="isDirectDel">�Ƿ�ֱ��ɾ��������ʾ��</param>
        /// <returns></returns>
        private int Delete(int rowIndex, bool isDirectDel)
        {
            int i = rowIndex;

            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("����ѡ��һ��ҽ����");
                return 0;
            }

            DialogResult r;

            FS.HISFC.Models.Order.Inpatient.Order order = null, temp = null;
            Hashtable hsDeleteOrders = new Hashtable();

            //���������Ҫɾ����ҽ����Ϻ�
            string orderComboIDs = "";

            for (int row = 0; row < this.fpOrder.ActiveSheet.Rows.Count; row++)
            {
                if (this.fpOrder.ActiveSheet.IsSelected(row, 0))
                {
                    order = this.GetObjectFromFarPoint(row, this.fpOrder.ActiveSheetIndex);
                    if (order == null)
                    {
                        MessageBox.Show("���ҽ��ʵ�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("��ʿ������ɾ�����˿�����ҽ��!");
                    return -1;
                }
            }

            if (order.Status == 0 || order.Status == 5)
            {
                //�¼�
                #region δ���ҽ��

                if (!CheckOrderCanMove(order))
                {
                    MessageBox.Show("��" + order.Item.Name + "���Ѿ���ӡ��������ɾ����\r\n\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return -1;
                }

                r = DialogResult.OK;
                if (!isDirectDel)
                {
                    r = MessageBox.Show("�Ƿ�ɾ����ҽ��[" + order.Item.Name + "]?\n *�˲������ܳ�����", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
                                #region ������û,��ȥȡ��һ��ҽ��ԤԼ
                                appMgr.Cancle(order.ID);
                                #endregion
                            }

                            if (order.ID == "" || tmpOrder == null)
                            {
                                //��Ȼɾ��
                                this.fpOrder.ActiveSheet.Rows.Remove(row, 1);
                            }
                            else
                            {
                                if (tmpOrder != null && tmpOrder.RowNo >= 0 && tmpOrder.PageNo >= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(tmpOrder.Item.Name + "�Ѿ���ӡҽ����������ɾ���������Ҽ�ֹͣ/ȡ��ҽ����");
                                    return -1;
                                }
                                //delete from table
                                //ɾ���Ѿ��еĸ���
                                if (CacheManager.InOrderMgr.DeleteOrderSubtbl(temp.Combo.ID) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��������Ŀ��Ϣ����") + CacheManager.InOrderMgr.Err);
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
                                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ��״̬�ѷ����仯 ��ˢ������"));
                                        return -1;
                                    }
                                }
                                if (CacheManager.RadtIntegrate.SelectBQ_Info(((FS.FrameWork.Models.NeuObject)(myPatientInfo)).ID) == "1")
                                {
                                    if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("�Ѿ�����") != -1)
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
                                //ɾ������
                                parm = CacheManager.InOrderMgr.DeleteOrderSubtbl(temp.Combo.ID);
                                if (parm == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    MessageBox.Show(CacheManager.InOrderMgr.Err);
                                    return -1;
                                }

                                //{3EDB0DF8-44D5-4596-B0BB-A12E7C87399C}
                                //����ҽ����������ҩ
                                #region ����ҽ����������ҩ
                                //2019-12ͣ��ҽ���޶�����ҩ����
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
                                                MessageBox.Show("ɾ��ҽ����չ��Ϣ����\r\n" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    //�Ȳ��ܵ������뵥��...����ѡ����ɾ����ʱ�� ������
                    if (this.pacsBillManagement.DeletePacsBill(order.Combo.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        MessageBox.Show(pacsBillManagement.Err);
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    //ɾ��һ�к�ѡ����һ�� 
                    if (this.fpOrder.ActiveSheet.Rows.Count > 0)
                    {
                        this.SelectionChanged();
                    }
                }
                #endregion
            }
            else if (order.Status != 3)
            {
                //������ҽ��ȡ����ȷ�ϵ��ն���Ŀ��ҽ��
                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT && order.Status == 2 && order.Item.IsNeedConfirm)
                {
                    ArrayList execOrderList = CacheManager.InOrderMgr.QueryExecOrderByOrderNo(order.ID, order.Item.ID, "1");
                    if (execOrderList.Count > 0)
                    {
                        MessageBox.Show("[" + order.ExeDept.Name + "]�Ѿ���[" + order.Item.Name + "]�����շ�ȷ�ϣ���֪ͨ�����һ�ʿ��[" + order.ExeDept.Name + "]��ϵ��" + "\n\n" + "ȷ�ϻ����Ƿ��Ѿ�ִ��[" + order.Item.Name + "]������Ѿ�ִ�У������ҽ������������");
                        return -1;
                    }
                }
                //����ֹͣ����
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
                    MessageBox.Show("ֹͣʱ�䲻��С�ڿ�ʼʱ��");
                    return -1;
                }

                if (f.DCDateTime > CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddHours(1))
                {
                    //Ԥֹͣʱ��ָ��
                    #region ����Ԥֹͣ
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
                            //ԭ�������ڱ���Ԥֹͣʱû�ж�ͬһ��ϵ�����ҽ�����и��£��ּ���˶δ���
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

                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ҽ��״̬"]].Value = temp.Status;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣʱ��"]].Value = temp.DCOper.OperTime;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["����ʱ��"]].Value = temp.EndTime;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣ�˱���"]].Text = temp.DCOper.ID;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣ��"]].Text = temp.DCOper.Name;
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

                            #region Сʱҽ��ֹͣ�Ʒ�
                            if (this.DCHoursOrder(order, FS.FrameWork.Management.PublicTrans.Trans, true) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                CacheManager.OrderIntegrate.fee.Rollback();
                                temp.Status = tempState;
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg(order.Item.Name + "ֹͣʱ�Ƿ�ʧ�ܣ�"));
                                return -1;
                            }
                            #endregion

                            #region ֹͣҽ��

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
                                #region ������û,��ȥȡ��һ��ҽ��ԤԼ
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
                            //������Ϣ����ʿ
                            //FS.Common.Class.Message.SendMessage(this.GetPatient().Patient.Name + "��ҽ����" + temp.Item.Name + "���Ѿ�" + strTip, order.NurseStation.ID);
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ҽ��״̬"]].Value = temp.Status;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣʱ��"]].Value = temp.DCOper.OperTime;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["����ʱ��"]].Value = temp.EndTime;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣ�˱���"]].Text = temp.DCOper.ID;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣ��"]].Text = temp.DCOper.Name;
                            this.fpOrder.ActiveSheet.Rows[row].Tag = temp;

                            continue;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                //ɾ���Ͳ���Ҫˢ����
                //this.RefreshOrderState();
            }
            else
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ҽ������ɾ��,���ϻ�ȡ��!"));
            }

            this.RefreshOrderState(-1);

            #region ��ʱ��Ϣ

            this.SendMessage(SendType.Delete);

            #endregion

            #region ����������뵥
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
        /// ���
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            //if (this.Patient.PVisit.PatientLocation.Dept.ID != CacheManager.LogEmpl.Dept.ID)
            //{
            //    MessageBox.Show("�������������ǡ�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Patient.PVisit.PatientLocation.Dept.ID) + "�����½�Ŀ������ҡ�" + CacheManager.LogEmpl.Dept.Name + "����һ�£�����������", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return -1;
            //}

            if (this.IBeforeAddOrder != null)
            {
                if (this.IBeforeAddOrder.OnBeforeAddOrderForInPatient(this.Patient, this.GetReciptDept(), this.GetReciptDoc()) == -1)
                {
                    if (!string.IsNullOrEmpty(IBeforeAddOrder.ErrInfo))
                    {
                        MessageBox.Show(IBeforeAddOrder.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return -1;
                }
            }

            //{F38618E9-7421-423d-80A9-401AFED0B855}
            if (this.isShowOrderFinished == false)
            {
                //MessageBox.Show("ˢ����Ϣ��δ��ɣ����Ժ��ٵ��������");
                return -1;
            }

            //CountLongBegin = this.fpOrder_Long.Rows.Count;
            //CountShortBegin = this.fpOrder_Short.Rows.Count;
            CountLongBegin = 0;
            CountShortBegin = 0;

            // TODO:  ��� ucOrder.Add ʵ��
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

            #region ����ʱ ����״̬��ʾ

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
        /// ���û���ҽ���Ƿ��ڿ���״̬
        /// ��������ҽ��ͬʱ�༭����ӡһ�����ߵ�ҽ��
        /// </summary>
        private void SaveUserDefaultSetting(bool isAddMode)
        {
            //Ӧ���Լ���һ��סԺ������չ���������ĳ��״̬�洢��...
            //ĳ���ֶΣ��洢��ǰ����ҽ�����ڵ�״̬��������
            // 0 �޲���״̬ 1 ����״̬��2 ��ӡ״̬

            //ע��ͬ���޸�ҽ������ӡ����
        }

        /// <summary>
        /// ����ֹͣҽ������ǰҽ����ת��ҽ������Ժҽ����
        /// </summary>
        /// <returns>1��ǰҽ����2ת��ҽ�� 3 ��Ժҽ��</returns>
        public int AddDCOrder(string type)
        {
            if (!this.IsDesignMode)
            {
                MessageBox.Show("��ǰ����ҽ������״̬�����ܿ�������ǰҽ������");
                return -1;
            }

            if (this.myPatientInfo == null || this.myPatientInfo.ID == "")
            {
                MessageBox.Show("������ϢΪ�գ�������ѡ�л��ߺ������", "����", MessageBoxButtons.OK);
                return -1;
            }

            string strOrderType = "";
            string strWarn = "";

            //1����Ժҽ����2��ת��ҽ����3������ҽ����4����ǰҽ����5������ҽ��

            if (type == "1")
            {
                strOrderType = "��Ժҽ��";
                strWarn = "������Ժҽ����ϵͳ��ֹͣ��ǰ���õĳ���ҽ����";
            }
            else if (type == "2")
            {
                strOrderType = "ת��ҽ��";
                strWarn = "����ת��ҽ����ϵͳ��ֹͣ��ǰ���õĳ���ҽ����";
            }
            else if (type == "3")
            {
                strOrderType = "����ҽ��";
                strWarn = "��������ҽ����ϵͳ��ֹͣ��ǰ���õĳ���ҽ����";
            }
            else if (type == "4")
            {
                strOrderType = "��ǰҽ��";
                strWarn = "������ǰҽ����ϵͳ��ֹͣ��ǰ���õĳ���ҽ����";
            }
            else if (type == "5")
            {
                strOrderType = "����ҽ��";
                strWarn = "��������ҽ����ϵͳ��ֹͣ��ǰ���õĳ���ҽ����";
            }

            else if (type == "6")
            {
                return DcTreatmenttype();
            }

            if (MessageBox.Show("ȷ��Ҫ������" + strOrderType + "���𣿲������ɳ�����\r\n\r\n" + strWarn, "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return -1;
            }

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            //ֹͣҽ�������������Ϊ�����в�ѯ�����񣬲�ҪӰ��������¿���
            if (DcAllLongOrder(dtNow, new FS.FrameWork.Models.NeuObject("", type, "")) == -1)
            {
                return -1;
            }

            #region ������������

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
                if (strOrderType == "��Ժҽ��")
                {
                    undrugItem.SysClass.ID = "MRH";
                }
                if (strOrderType == "ת��ҽ��")
                {
                    undrugItem.SysClass.ID = "MRD";
                }
                else
                {
                    undrugItem.SysClass.ID = "M";
                }
                undrugItem.SysClass.Name = "����ҽ��";
                undrugItem.Qty = 1;
                undrugItem.PriceUnit = "��";
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
            // TODO:  ��� ucOrder.Exit ʵ��
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
        /// ����ҽ��
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //�Ƿ��������ҽ��������MQ��ʾ
            bool isContainsNewOrder = false;

            if (this.bIsDesignMode == false)
                return -1;

            this.ucItemSelect1.SetInputControlVisible(false);
            ucItemSelect1.SetFocus();

            //��黼��״̬
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

            #region ���洦��ǰ�ӿ�ʵ��

            //������ʾ������ȵ�
            if (IBeforeSaveOrder != null)
            {
                if (IBeforeSaveOrder.BeforeSaveOrderForInPatient(this.myPatientInfo, this.GetReciptDept(), this.GetReciptDoc(), alSaveOrder) == -1)
                {
                    if (!string.IsNullOrEmpty(IBeforeSaveOrder.ErrInfo))
                    {
                        MessageBox.Show(IBeforeSaveOrder.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        IBeforeSaveOrder.ErrInfo = "";
                    }
                    return -1;
                }
            }

            #endregion

            #region ��������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            #endregion

            #region ���ĵ�ҽ��
            List<FS.HISFC.Models.Order.Inpatient.Order> alOrder = new List<FS.HISFC.Models.Order.Inpatient.Order>();//���泤������
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

                        //�Զ����ҽ��
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
                        MessageBox.Show(name + "����������Ϊ�գ�");
                        return -1;
                    }

                    if (string.IsNullOrEmpty(order.ID))
                    {
                        strID = CacheManager.InOrderMgr.GetNewOrderID();
                        if (strID == "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ҽ����ˮ�ų���\r\n" + CacheManager.InOrderMgr.Err));
                            return -1;
                        }

                        order.ID = strID; //���ҽ����ˮ��

                        if (this.fpOrder.Sheets[0].Cells[i, dicColmSet["����"]].Tag != null)
                        {
                            orderExtObj = new FS.HISFC.Models.Order.Inpatient.OrderExtend();
                            orderExtObj.InPatientNo = myPatientInfo.ID;
                            orderExtObj.MoOrder = order.ID;
                            orderExtObj.Indications = this.fpOrder.Sheets[0].Cells[i, dicColmSet["����"]].Tag.ToString();
                            orderExtObj.Oper.ID = orderExtMgr.Operator.ID;

                            if (orderExtMgr.InsertOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����ҽ����չ��Ϣ����\r\n" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    #region ͳһ��Ͽ�ʼʱ�䣬���ⲡ����ҩ��������Ϻŵ�ʱ��(��)���������ϲ���������������ʾ
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

                        //�Զ����ҽ��
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
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ҽ����ˮ�ų���\r\n" + CacheManager.InOrderMgr.Err));
                            return -1;
                        }

                        order.ID = strID; //���ҽ����ˮ��

                        if (this.fpOrder.Sheets[1].Cells[i, dicColmSet["����"]].Tag != null)
                        {
                            orderExtObj = new FS.HISFC.Models.Order.Inpatient.OrderExtend();
                            orderExtObj.InPatientNo = myPatientInfo.ID;
                            orderExtObj.MoOrder = order.ID;
                            orderExtObj.Indications = this.fpOrder.Sheets[1].Cells[i, dicColmSet["����"]].Tag.ToString();
                            orderExtObj.Oper.ID = orderExtMgr.Operator.ID;

                            if (orderExtMgr.InsertOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����ҽ����չ��Ϣ����\r\n" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("����ҽ����ִ�п���ӦΪ�������,������ѡ��ִ�п���!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            #region ���ݽӿ�ʵ�ֶ�ҽ����Ϣ���в����ж�

            if (this.IAlterOrderInstance != null)
            {
                //{76FBAEE1-C996-41b4-9D77-F6CE457F6518} �����˽ӿ��ڷ���
                //if (this.IAlterOrderInstance.AlterOrderOnSaving(this.myPatientInfo, this.myReciptDoc, this.myReciptDept, ref orderList) == -1)
                //{
                //    return -1;
                //}
            }

            #endregion

            string err = "";//������Ϣ
            string strNameNotUpdate = "";//�Ѿ��仯״̬��ҽ��������

            #region ���İ�BUG addby xuewj

            foreach (string comboID in this.htSubs.Values)
            {
                if (CacheManager.InOrderMgr.DeleteOrderSubtbl(comboID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��������Ŀ��Ϣ����") + CacheManager.InOrderMgr.Err);
                    return -1;
                }
            }

            htSubs.Clear();

            #endregion

            if (CacheManager.OrderIntegrate.SaveOrder(alOrder, this.GetReciptDept().ID, out err, out strNameNotUpdate, FS.FrameWork.Management.PublicTrans.Trans) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ������ʧ�ܣ�") + "\n" + err);
                return -1;
            }

            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                if (strNameNotUpdate == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ������ɹ���" + checkMsg));
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ������ʧ�ܣ�") + "\n" + strNameNotUpdate
                        + FS.FrameWork.Management.Language.Msg("ҽ��״̬�Ѿ��������ط����ģ��޷����и��£���ˢ����Ļ��"));
                }
            }
            #endregion

            #region �ⲿ�ӿ�ʵ��

            if (IAfterSaveOrder != null)
            {
                if (IAfterSaveOrder.OnSaveOrderForInPatient(this.myPatientInfo, this.GetReciptDept(), this.GetReciptDoc(), new ArrayList(alOrder)) != 1)
                {
                    MessageBox.Show(IAfterSaveOrder.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            #endregion

            #region ���ݽӿ�ʵ�ֶ�ҽ����Ϣ���в����ж�

            if (this.IAlterOrderInstance != null)
            {
                if (this.IAlterOrderInstance.AlterOrderOnSaved(this.myPatientInfo, this.myReciptDoc, this.myReciptDept, ref orderList) == -1)
                {
                    return -1;
                }
            }

            #endregion

            #region �������뵥
            if (this.isUsePACSApplySheet)
            {
                SavePacsApply();
            }
            #endregion

            #region ��ʱ��Ϣ
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
            //�����ӡ������뵥
            RecipePrint();// {0045F3F6-1B1C-4d0a-A834-8BD07286E175}

            #region ���鷽ҽ����ӡ
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
                    DialogResult dr = MessageBox.Show("���ھ��鴦�����Ƿ���Ҫ��ӡ��", "���鴦��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

                //�ص�Ʒ�־���
                string whereImp = " where met_ipm_order.inpatient_no='{0}' and met_ipm_order.mo_stat='0' and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ('P2') and b.SPECIAL_FLAG4='13')";
                whereImp = string.Format(whereImp, this.myPatientInfo.ID);
                FS.HISFC.BizLogic.Order.Order ordMgrImp = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTempImp = ordMgr.QueryOrderBase(whereImp);
                if (alOrderTempImp.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("�����ص�Ʒ�־������Ƿ���Ҫ��ӡ��", "�ص�Ʒ�ִ���", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

                //�ص�Ʒ��ȫ��
                string whereImpM = " where met_ipm_order.inpatient_no='{0}' and met_ipm_order.mo_stat='0' and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ('Q') and b.SPECIAL_FLAG4='13')";
                whereImp = string.Format(whereImpM, this.myPatientInfo.ID);
                FS.HISFC.BizLogic.Order.Order ordMgrImpM = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTempImpM = ordMgr.QueryOrderBase(whereImp);
                if (alOrderTempImp.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("�����ص�Ʒ��ȫ�飬�Ƿ���Ҫ��ӡ��", "�ص�Ʒ�ִ���", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        #region  add by lijp 2011-11-25 �������뵥��� {102C4C01-8759-4b93-B4BA-1A2B4BB1380E}

        /// <summary>
        ///  �������뵥��Ϣ
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
        /// �༭���뵥
        /// </summary>
        public void EditPascApply()
        {
            try
            {
                if (!this.isUsePACSApplySheet)
                {
                    MessageBox.Show("δ�������뵥");
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

                // ��ҽ����ȡ���뵥�š�
                FS.HISFC.Models.Order.Inpatient.Order order =
                    this.GetObjectFromFarPoint(this.fpOrder_Short.ActiveRowIndex, this.fpOrder.ActiveSheetIndex);
                int rev = IInpateintPacsApply.Edit(this.myPatientInfo, order);
                if (rev < 0)
                {
                    MessageBox.Show(IInpateintPacsApply.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!string.IsNullOrEmpty(IInpateintPacsApply.ErrInfo))
                {
                    Classes.Function.ShowBalloonTip(3, IInpateintPacsApply.ErrInfo, "��ʾ", ToolTipIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("û�п�����Ч�ļ����Ŀҽ��");
            }
        }

        #endregion

        public int JudgeSpecialOrder()
        {
            int i = this.fpOrder.ActiveSheet.ActiveRowIndex;
            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("����ѡ��һ��ҽ����");
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
                    this.fpOrder.ActiveSheet.Cells[i, dicColmSet["ҽ��״̬"]].Text = order.Status.ToString();
                }

            }
            return 0;
        }

        public int HerbalOrder()
        {
            string orderTypeFlag = "1";		//0 ���� 1 ����

            FS.HISFC.Models.Order.Inpatient.Order ord;
            if (this.fpOrder.ActiveSheet.ActiveRowIndex >= 0 && this.fpOrder.ActiveSheet.Rows.Count > 0)
            {
                ord = this.fpOrder.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.Order;
                #region �����ҩ����{7985420C-9CF9-4dd3-BED4-A5CC0EC9D52C}
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
                        FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
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
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
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
        /// ѡ��ҽ��{D5517722-7128-4d0c-BBC4-1A5558A39A03}���ڵ�½��Ա����ҽ��ʱʹ��
        /// </summary>
        /// <returns></returns>
        public int ChooseDoctor()
        {
            try
            {
                ucChooseDoct uc = new ucChooseDoct();
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "ѡ��";
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

        #region ��������

        /// <summary>
        /// �����ҽ��
        /// </summary>
        /// <param name="sender"></param>
        public int AddNewOrder(FS.HISFC.Models.Order.Inpatient.Order inOrder, int SheetIndex)
        {
            if (this.ValidOrderBefore(inOrder, SheetIndex) == -1)
            {
                return -1;
            }

            //���� ԤԼ��Ժ ҽ������ ¼��ת���ֹͣ����ҽ����
            if (inOrder.Item.SysClass.ID.ToString().Equals("MRH")
                || inOrder.Item.Name.Contains("��Ժ")
                || inOrder.Item.Name.Contains("����"))
            {
                if (this.DcAllLongOrderAndZG() == -1)
                {
                    return -1;
                }
            }
            else if (inOrder.Item.SysClass.ID.ToString() == "MRD" || inOrder.Item.Name.Contains("ת��"))
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
                        strc = "���ڲ���ת�ƣ�ϵͳ�Զ����û�����;�˳�·����";
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
                            MessageBox.Show("�˳�·������ʧ�ܣ�");
                            return -1;
                        }
                    }
                }
            }

            #region ���ݽӿ�ʵ�ֶ�ҽ����Ϣ���в����ж�

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

            #region ��黥��
            if (CheckMutex(inOrder.Item.SysClass) == -1)
            {
                return -1;
            }
            #endregion

            #region �����ӵĶ���
            if (inOrder.Item.SysClass.ID.ToString() == "UC")
            {
                //���ÿ��ԶԸ���ҽ���ļ�鵥��д
                //this.IsPrintTest(true);
            }
            else if (inOrder.Item.SysClass.ID.ToString() == "MC")
            {
                //��ӻ�������
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

                //ҩƷ
                if (((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).IsAllergy)
                {
                    if (MessageBox.Show("��" + inOrder.Item.Name + "��\n�Ƿ���ҪƤ�ԣ�", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        inOrder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest;
                    }
                    else
                    {
                        //��ҪƤ�� 
                        inOrder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                    }
                }
                else
                {
                    inOrder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                }

                //�ж�ҩƷ�Ƿ���ҩ������ʾ
                try
                {
                    if (((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).Quality.ID.Substring(0, 1) == "S")
                    {
                        MessageBox.Show("ҩƷ��" + inOrder.Item.Name + "�����ڶ���ҩ����Ҫ���Ӵ�ӡ����ҩ������ҩ��ȡҩ!", "��ʾ", MessageBoxButtons.OK);
                    }

                    //{CB5C628A-EA63-41e7-9D38-3F3DF2E78834}


                    if (inOrder.Item.SpecialFlag == "1" || inOrder.Item.SpecialFlag == "2" || inOrder.Item.SpecialFlag == "3" || inOrder.Item.SpecialFlag == "4")
                    {
                        string level = "";
                        if (inOrder.Item.SpecialFlag == "1")
                        {
                            level = "A��";
                            MessageBox.Show("��ҩƷ��" + inOrder.Item.Name + "������" + level + "�߾�ʾҩƷ��ע��˶�ҩƷ���ơ�����÷���������ʹ��Ũ�ȼ����ٵ�!", "��ʾ", MessageBoxButtons.OK);
                        }

                        if (inOrder.Item.SpecialFlag == "2")
                        {
                            level = "B��";
                            MessageBox.Show("��ҩƷ��" + inOrder.Item.Name + "������" + level + "�߾�ʾҩƷ��ע��˶�ҩƷ���ơ�����÷���������!", "��ʾ", MessageBoxButtons.OK);
                        }

                        if (inOrder.Item.SpecialFlag == "3")
                            level = "C��";

                        if (inOrder.Item.SpecialFlag == "4")
                        {
                            level = "�׻���";
                            MessageBox.Show("��ҩƷ��" + inOrder.Item.Name + "������" + level + "ҩƷ��ע��˶�ҩƷ���ơ�����!", "��ʾ", MessageBoxButtons.OK);
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

            //���´���ִ�п���
            if (inOrder.Item.ItemType == EnumItemType.Drug)
            {
                inOrder.ExeDept = this.GetExecDept(patientType, inOrder.ReciptDept);
            }

            #region add by zhaorong at 2013-8-29 start ҽ�����÷���ִ�п��Ҿ���ά���ĳ���������ʱ��Ĭ��ִ�п���Ϊ��6030��������������ҩ����
            this.ChangeStockDept(ref inOrder);
            #endregion


            if (inOrder.Combo.ID == "")
            {
                try
                {
                    inOrder.Combo.ID = CacheManager.InOrderMgr.GetNewOrderComboID();//�����Ϻ�
                }
                catch
                {
                    MessageBox.Show("���ҽ����Ϻų���" + CacheManager.InOrderMgr.Err);
                    return -1;
                }
            }

            #region ����ʱ��

            if (inOrder.BeginTime < new DateTime(2000, 1, 1))
            {
                inOrder.BeginTime = Classes.Function.GetDefaultMoBeginDate(SheetIndex);
            }

            if (inOrder.User03 != "")
            {
                //���׵�ʱ����
                int iDays = FS.FrameWork.Function.NConvert.ToInt32(inOrder.User03);
                if (iDays > 0)
                {
                    //��ʱ����>0
                    inOrder.BeginTime = inOrder.BeginTime.AddDays(iDays);
                }
            }

            #endregion

            //����ҽ���� �ϴΡ��´ηֽ�ʱ�䲻��
            if (inOrder.ReTidyInfo == null
                || (inOrder.ReTidyInfo != null && !inOrder.ReTidyInfo.Contains("����ҽ��")))
            {
                inOrder.CurMOTime = DateTime.MinValue;
                inOrder.NextMOTime = DateTime.MinValue;
            }
            inOrder.EndTime = DateTime.MinValue;

            this.currentOrder = inOrder;

            #region �޸�Ϊ������...
            //this.fpOrder.Sheets[SheetIndex].Rows.Add(this.fpOrder.Sheets[SheetIndex].RowCount, 1);
            //this.AddObjectToFarpoint(inOrder, this.fpOrder.Sheets[SheetIndex].RowCount - 1, SheetIndex, ColmSet.ALL);
            //this.fpOrder.ActiveSheet.ActiveRowIndex = this.fpOrder.ActiveSheet.RowCount - 1;
            #endregion


            this.fpOrder.Sheets[SheetIndex].Rows.Add(0, 1);
            this.AddObjectToFarpoint(inOrder, 0, SheetIndex, ColmSet.ALL);
            this.fpOrder.ActiveSheet.ActiveRowIndex = 0;

            //���õ�ǰ�������Ϊ0
            this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;

            RefreshOrderState(ActiveRowIndex);
            this.fpOrder.ShowRow(0, this.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);

            dirty = false;

            #region ����ҽ����������ҩ
            //{A36B0D1B-C568-4d21-8507-763C9DC1E369}ҽ�����ߣ�ȡ����ȥ��ҽ������
            //����

            if (string.IsNullOrEmpty(this.Patient.Pact.PactDllName))
            {
                this.Patient.Pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(this.Patient.Pact.ID);
            }


            // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
            //��ɽ��Ҫ����ά���ĺ�ͬ��λ�ж�
            FS.FrameWork.Models.NeuObject objItem = CacheManager.ConManager.GetConstant("PactDllName", this.Patient.Pact.PactDllName);

            if (this.Patient != null
                && (this.Patient.Pact.PayKind.ID == "02"
                || (objItem != null && !string.IsNullOrEmpty(objItem.ID)))
                && this.isOpenDrugWarn)
            {
                FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(GetItemUserCode(inOrder.Item));
                if (indicationsObj != null)
                {
                    if (MessageBox.Show("ҩƷ��" + inOrder.Item.Name + "���������Ƽ�ҩƷ��\r\n\r\n����ҩƷ˵������" + indicationsObj.Name + "��\r\n\r\n��ȷ��ҽ�������趨������(��)���Է�(��)?\r\n", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        //���������е�tagֵ����
                        fpOrder.ActiveSheet.Cells[fpOrder.ActiveSheet.ActiveRowIndex, dicColmSet["����"]].Tag = "1";
                    }
                    else
                    {
                        fpOrder.ActiveSheet.Cells[fpOrder.ActiveSheet.ActiveRowIndex, dicColmSet["����"]].Tag = "0";
                    }
                }
            }

            #endregion

            #region ������

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
                            MessageBox.Show("������ʧ�ܣ�" + errText);
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


                                //�˴����ܻ�ȡID
                                //newOrder.ID = CacheManager.InOrderMgr.GetNewOrderID();
                                //newOrder.SortID = this.GetSortIDBySubCombNo(newOrder.SubCombNO);

                                this.fpOrder.ActiveSheet.Rows.Add(this.fpOrder.ActiveSheet.RowCount, 1);

                                this.AddObjectToFarpoint(newOrder, this.fpOrder.ActiveSheet.RowCount - 1, fpOrder.ActiveSheetIndex, ColmSet.ALL);
                            }
                            this.fpOrder.ActiveSheet.ActiveRowIndex = this.fpOrder.ActiveSheet.RowCount - 1;
                            //���õ�ǰ�������Ϊ0
                            this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;
                            RefreshOrderState(ActiveRowIndex);
                        }
                    }
                }
                dirty = false;
            }
            #endregion
            this.RefreshCombo();

            #region ������ҩ
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

                #region ҩƷ��ѯ
                try
                {
                    //ò������ֻ�����½ǵ�����λ�����
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

            //������Ժҽ������ʾ����������

            return 1;
        }
        /// <summary>
        /// add by zhaorong at 2013-8-29 start ҽ�����÷���ִ�п��Ҿ���ά���ĳ���������ʱ��Ĭ��ִ�п���Ϊ��6030��������������ҩ����
        /// </summary>
        /// <param name="inOrder"></param>
        private bool ChangeStockDept(ref FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            #region ����
            //ִ�п�����Ϣ(pivas����)
            if (this.deptItemList == null)
            {
                this.deptItemList = CacheManager.GetConList("DeptItemNew");
            }
            //�÷�(pivas����)
            if (this.drugUsageList == null)
            {
                this.drugUsageList = CacheManager.GetConList("USAGE#USAGE");
            }
            //����ҽ���Ƿ�ͨpivas���ܱ�ʶ
            if (string.IsNullOrEmpty(this.pivasCzFlag))
            {
                this.pivasCzFlag = CacheManager.ContrlManager.GetControlParam<string>("PIVASC", false, "0");
            }
            //��ʱҽ���Ƿ�ͨpivas���ܱ�ʶ
            if (string.IsNullOrEmpty(this.pivasLzFlag))
            {
                this.pivasLzFlag = CacheManager.ContrlManager.GetControlParam<string>("PIVASL", false, "0");
            }
            #endregion

            int isChangeStock = 0;//�Ƿ�����ۿ���ұ��
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
                //�ж��Ƿ�Ϊ�������Ƿ�ͨpivas����
                bool pivasCz = (inOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG && "1".Equals(this.pivasCzFlag));
                //�ж��Ƿ�Ϊ�������Ƿ�ͨpivas����
                bool pivasLz = (inOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT && "1".Equals(this.pivasLzFlag));

                #region �����ۿ����
                if (isChangeStock == 2)
                {
                    //����һ��(0911)����������(0921)�����ֳ���������,ȫ��ͬʱ����
                    if ("0911".Equals(inOrder.ReciptDept.ID) || "0921".Equals(inOrder.ReciptDept.ID))
                    {
                        inOrder.StockDept = SOC.HISFC.BizProcess.Cache.Common.GetDept("6030");//������������ҩ��
                        return true;
                    }
                    else if (pivasCz || pivasLz)
                    {
                        inOrder.StockDept = SOC.HISFC.BizProcess.Cache.Common.GetDept("6030");//������������ҩ��
                        return true;
                    }
                    else
                    {
                        List<FS.HISFC.Models.Pharmacy.Item> itemList = CacheManager.PhaIntegrate.QueryItemAvailableListByItemCode(inOrder.ExeDept.ID, "I", inOrder.Item.ID);
                        if (itemList.Count > 0)
                        {
                            FS.HISFC.Models.Pharmacy.Item item = itemList[0] as FS.HISFC.Models.Pharmacy.Item;
                            inOrder.StockDept = SOC.HISFC.BizProcess.Cache.Common.GetDept(item.User02);//ִ��ҩ��
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
                        inOrder.StockDept = SOC.HISFC.BizProcess.Cache.Common.GetDept(item.User02);//ִ��ҩ��
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
        /// �����������
        /// </summary>
        public void AddOperation()
        {
            //if (this.PatientInfo == null)
            //{
            //    MessageBox.Show("����ѡ���ߣ�");
            //    return;
            //}
            //frmApply dlgTempApply = new frmApply(FS.Common.Class.Main.var, this.PatientInfo);
            //dlgTempApply.SetClearButtonFasle();
            //dlgTempApply.ISCloseNow = true;
            ////��ʾ��ʱ���봰��(ģʽ)
            //dlgTempApply.ShowDialog();

            ////����Ĵ���Ǳ���
            //if (dlgTempApply.ExeDept != "")
            //{
            //    //����"ȷ��"��ť
            //    FS.FrameWork.Models.NeuObject mainOperation = new FS.FrameWork.Models.NeuObject();//���������
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
            //            //��������
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
            //    order.Unit = "��";
            //    order.Item.Name = mainOperation.Name;
            //    order.ExeDept.ID = dlgTempApply.ExeDept; /*ִ�п���*/
            //    order.Frequency.ID = "QD";
            //    //��������ҽ��Ĭ��Ϊ��ǰ����
            //    if (this.ucItemSelect1.alShort.Count > 0)
            //    {
            //        FS.HISFC.Models.Order.Inpatient.OrderType info;
            //        for (int i = 0; i < this.ucItemSelect1.alShort.Count; i++)
            //        {
            //            info = this.ucItemSelect1.alShort[i] as FS.HISFC.Models.Order.Inpatient.OrderType;
            //            if (info == null)
            //                return;
            //            if (info.ID == "SQ")
            //            {  //SQ ��ǰ���� SZ ��ǰ����
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
        /// ��Ӽ�顢��������
        /// </summary>
        public void AddTest()
        {
            if (this.myPatientInfo == null)
            {
                MessageBox.Show("����ѡ���ߣ�");
                return;
            }
            List<FS.HISFC.Models.Order.Inpatient.Order> alItems = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            int iActiveSheet = 1;//��鵥Ĭ����ʱҽ��

            //{47C187AE-F3FC-433c-AA2D-F1C146ED4F92}  ��ѡ����ҽ��ʱ�Ž��м�����뵥����
            this.fpOrder.ActiveSheetIndex = 1;
            this.OrderType = FS.HISFC.Models.Order.EnumType.SHORT;//{A762E223-39EE-4379-AADB-B5A929F85D41}

            for (int i = 0; i < this.fpOrder.Sheets[iActiveSheet].RowCount; i++)
            {
                if (this.fpOrder.Sheets[iActiveSheet].IsSelected(i, 0))
                {
                    //{47C187AE-F3FC-433c-AA2D-F1C146ED4F92}  ��ѡ����ҽ��ʱ�Ž��м�����뵥����
                    FS.HISFC.Models.Order.Inpatient.Order tempOrder = this.GetObjectFromFarPoint(i, iActiveSheet);
                    if (tempOrder.Item.SysClass.ID.ToString() == "UC")         //�����ڼ����Ŀ
                    {
                        //��alItems���ݸ�Ϊorder����
                        alItems.Add(tempOrder);
                    }
                }
            }
            if (alItems.Count <= 0)
            {
                //û��ѡ����Ŀ��Ϣ
                MessageBox.Show("��ѡ�����ļ����Ϣ!");
                return;
            }

            this.checkPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPrint)) as FS.HISFC.BizProcess.Interface.Common.ICheckPrint;
            #region {3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}
            //���{3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}pacs�ӿ�����
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
                    MessageBox.Show("��ýӿ�IcheckPrint����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿڼ���ӿ�IcheckPrint\n����ϵͳ����Ա��ϵ��");
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
            //            MessageBox.Show("����ѡ�����ĿӦ�ÿ�����ͬ�ļ�鵥\n������ѡ��", "��ʾ");
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
        /// ��ӻ���
        /// </summary>
        /// <param name="sender"></param>
        public void AddConsultation(object sender)
        {
            //�����ΰ�
            return;
            //if (this.myPatientInfo == null)
            //{
            //    MessageBox.Show("����ѡ����!");
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
        /// ���
        /// </summary>
        /// <param name="k"></param>
        private void ComboOrder(int k)
        {
            //���ʱ ����С����ʾ

            #region ���ҽ��

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
                        MessageBox.Show("��" + inOrder.Item.Name + "���Ѿ���ӡ��������ɾ����\r\n\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show("���ʱ��ѡ�ж�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //У�����ҽ��
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
                        GetOrderChanged(i, inOrder, ColmSet.Z���);
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
                    GetOrderChanged(i, inOrder, ColmSet.Z���);
                }
            }
            this.fpOrder.Sheets[k].ClearSelection();

            this.ucItemSelect1.Clear(false);

            #endregion
        }

        /// <summary>
        /// ���ҽ��
        /// </summary>
        public void ComboOrder()
        {
            ComboOrder(this.fpOrder.ActiveSheetIndex);
            this.RefreshCombo();
            //��Ϻ�ͬ�����Ŀһ��ѡ�У������������޸�����е�ĳһ��Ƶ�κͱ��ҩƷ��һ��
            this.SelectionChanged();
        }

        /// <summary>
        /// ȡ�����
        /// </summary>
        public void CancelCombo()
        {
            int iSelectionCount = 0;

            FS.HISFC.Models.Order.Inpatient.Order combOrder = null;

            //�洢�����������к�
            int combRowIndex = -1;

            for (int i = 0; i < this.fpOrder.Sheets[fpOrder.ActiveSheetIndex].Rows.Count; i++)
            {
                if (this.fpOrder.Sheets[fpOrder.ActiveSheetIndex].IsSelected(i, 0))
                {
                    iSelectionCount++;
                    FS.HISFC.Models.Order.Inpatient.Order inOrder = this.GetObjectFromFarPoint(i, fpOrder.ActiveSheetIndex);

                    if (!CheckOrderCanMove(inOrder))
                    {
                        MessageBox.Show("��" + inOrder.Item.Name + "���Ѿ���ӡ��������ȡ����ϣ�\r\n\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (inOrder.Status.ToString() != "0" && inOrder.Status.ToString() != "5")
                    {
                        MessageBox.Show("��" + inOrder.Item.Name + "�����¿���ҽ����������ȡ����ϣ�", "��ʾ");
                        return;
                    }

                    if (!String.IsNullOrEmpty(inOrder.ApplyNo))
                    {
                        MessageBox.Show("��" + inOrder.Item.Name + "���ѿ������뵥��������ȡ����ϣ�");
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
                MessageBox.Show("ȡ�����ʱ��ѡ�ж�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //��Ϻ�
            int newSubComb = 0;
            string combNo = "";

            //for (int i = 0; i < this.fpOrder.ActiveSheet.Rows.Count; i++)
            //ȡ�����ʱ����Ҫ�Զ�������Ϻ�
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

                        //this.ucItemSelect1_OrderChanged(inOrder, ColmSet.Z���);
                        GetOrderChanged(i, inOrder, ColmSet.Z���);
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

                    //this.ucItemSelect1_OrderChanged(inOrder, ColmSet.Z���);
                    GetOrderChanged(i, inOrder, ColmSet.Z���);
                }
            }
            this.fpOrder.ActiveSheet.ClearSelection();
            this.RefreshCombo();
            this.SelectionChanged();
        }

        /// <summary>
        /// �������
        /// </summary>
        public void SaveSortID()
        {
            this.SaveSortID(true);
        }

        /// <summary>
        /// ��ѯʱ��ı��棬���򱣴�
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
                        if (CacheManager.InOrderMgr.UpdateOrderSortID(fpOrder.Sheets[i].Cells[j, dicColmSet["ҽ����ˮ��"]].Text, (k++).ToString()) == -1)
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

            if (prompt) MessageBox.Show("ҽ��˳�򱣴�ɹ���");
        }

        protected void SaveSortID(int row)
        {
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(OrderManagement.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                if (CacheManager.InOrderMgr.UpdateOrderSortID(fpOrder.ActiveSheet.Cells[row, dicColmSet["ҽ����ˮ��"]].Text, fpOrder.ActiveSheet.Cells[row, dicColmSet["˳���"]].Value.ToString()) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(CacheManager.InOrderMgr.Err);
                    return;
                }

                ArrayList al = CacheManager.InOrderMgr.QuerySubtbl(fpOrder.ActiveSheet.Cells[row, dicColmSet["��Ϻ�"]].Text);
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in al)
                    {
                        if (CacheManager.InOrderMgr.UpdateOrderSortID(order.ID, fpOrder.ActiveSheet.Cells[row, dicColmSet["˳���"]].Value.ToString()) == -1)
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
                        if (fpOrder.Sheets[i].Cells[j, dicColmSet["˳���"]].Value.ToString() != (k).ToString())
                        {
                            if (CacheManager.InOrderMgr.UpdateOrderSortID(fpOrder.Sheets[i].Cells[j, dicColmSet["ҽ����ˮ��"]].Text, (k).ToString()) == -1)
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
        /// ��Ӳ�ҩҽ��{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        /// <param name="alHerbalOrder"></param>
        public void AddHerbalOrders(ArrayList alHerbalOrder)
        {
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} //��ҩ������ҩ��������
            using (FS.HISFC.Components.Order.Controls.ucHerbalOrder uc = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
            {
                uc.IsClinic = false;

                uc.Patient = new FS.HISFC.Models.RADT.PatientInfo();//
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
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
        /// �޸Ĳ�ҩ{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        public void ModifyHerbal()
        {
            if (this.fpOrder.ActiveSheet.RowCount == 0)
            {
                return;
            }

            ArrayList alModifyHerbal = new ArrayList(); //Ҫ�޸ĵĲ�ҩҽ��

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

                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
                    uc.AlOrder = alModifyHerbal;
                    uc.OpenType = FS.HISFC.Components.Order.Controls.EnumOpenType.Modified; //�޸�
                    uc.IsClinic = false;
                    DialogResult r = FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                    if (uc.IsCancel == true)
                    {
                        //ȡ����
                        return;
                    }

                    if (uc.OpenType == FS.HISFC.Components.Order.Controls.EnumOpenType.Modified)
                    {
                        //��Ϊ�¼�ģʽ�Ͳ�ɾ����
                        if (this.Delete(this.fpOrder.ActiveSheet.ActiveRowIndex, true) < 0)
                        {
                            //ɾ��ԭҽ�����ɹ�
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
                MessageBox.Show("��˲飬û�в�ҩ��Ϣ��");
                return;
            }
        }

        public void AddLevelOrders()
        {
            using (FS.HISFC.Components.Order.Controls.ucLevelOrder uc = new FS.HISFC.Components.Order.Controls.ucLevelOrder())
            {
                uc.InOutType = 2;
                uc.Patient = new FS.HISFC.Models.RADT.PatientInfo();

                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "������ҽ������";
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


        #region ճ��ҽ��

        /// <summary>
        /// ճ��ҽ��
        /// </summary>
        public void PasteOrder()
        {
            //���ָ�����ʷҽ�� ȷʵ�����⣬�����ڴ���� 
            //ע�⴦�� ��Ϻ� ��������
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
                            order.Item.Name.Replace("[����]", "").Replace("[�Ա�]", "");

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

                            #region �������ҡ�ִ�п�����Ϣ
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

                            //�����ҿ�������
                            if (!string.IsNullOrEmpty(order.ReciptDept.ID) && string.IsNullOrEmpty(order.ExeDept.ID))
                            {
                                if ("1,2".Contains((SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ReciptDept.ID).SpecialFlag)))
                                {
                                    order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ReciptDept.ID);
                                }
                            }
                            #endregion

                            //ҽ��״̬����
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

                            #region  add by liuww ����� ��������ҽ��������������
                            order.ReTidyInfo = "";
                            #endregion

                            order.FirstUseNum = Classes.Function.GetFirstOrderDays(order, dtNow).ToString();

                            //��ӵ���ǰ����а���ҽ�����ͽ��з���
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
                                #region �����������ܸ���Ϊ����ҽ��
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
                                    MessageBox.Show(order.Item.Name + "ϵͳ���Ϊ" + order.Item.SysClass.Name + "���ܸ���Ϊ����ҽ��!");
                                    continue;
                                }
                                #endregion
                            }

                            order.SubCombNO = this.GetMaxCombNo(order, this.fpOrder.ActiveSheetIndex);

                            this.AddNewOrder(order, this.fpOrder.ActiveSheetIndex);
                        }
                    }
                    this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].ClearSelection();
                    Classes.Function.ShowBalloonTip(3, "��ʾ", "��ע����ִ�п����Ƿ���ȷ��", ToolTipIcon.Info);
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����԰������ҽ������ΪסԺҽ����"));
                    return;
                }
            }
            catch { }
        }

        /// <summary>
        /// ճ��ҽ��
        /// </summary>
        public void PasteOrder(ArrayList alOrders)
        {
            //ע�⴦�� ��Ϻ� ��������
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

                        #region �������ҡ�ִ�п�����Ϣ
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

                        //�����ҿ�������
                        if (!string.IsNullOrEmpty(order.ReciptDept.ID) && string.IsNullOrEmpty(order.ExeDept.ID))
                        {
                            if ("1,2".Contains((SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ReciptDept.ID).SpecialFlag)))
                            {
                                order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ReciptDept.ID);
                            }
                        }
                        #endregion

                        //ҽ��״̬����
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

                        #region  add by liuww ����� ��������ҽ��������������
                        order.ReTidyInfo = "";
                        #endregion

                        order.FirstUseNum = Classes.Function.GetFirstOrderDays(order, dtNow).ToString();

                        //��ӵ���ǰ����а���ҽ�����ͽ��з���
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
                            #region �����������ܸ���Ϊ����ҽ��
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
                                MessageBox.Show(order.Item.Name + "ϵͳ���Ϊ" + order.Item.SysClass.Name + "���ܸ���Ϊ����ҽ��!");
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
        /// ������������ĩ����
        /// </summary>
        /// <param name="order">ҽ��</param>
        /// <param name="t"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Inpatient.Order SetFirstUseQuanlity(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order == null || order.OrderType.Type != FS.HISFC.Models.Order.EnumType.LONG)
            {
                return order;
            }

            //����ҽ���� �ϴΡ��´ηֽ�ʱ�䲻��
            if (!(order.ReTidyInfo == null ||
                (order.ReTidyInfo != null && !order.ReTidyInfo.Contains("����ҽ��"))))
            {
                return order;
            }

            #region ��ʱ����������
            string[] execTimes = order.Frequency.Times;
            //Begin��ʱ���������򣬰���С����//������Ѿ������ˣ��Ͳ�Ҫ��
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

            #region ������

            int times = 0;//�˴����ݴ���Ĵ������ж�

            if (string.IsNullOrEmpty(order.FirstUseNum))
            {
                return null;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(order.FirstUseNum) > order.Frequency.Times.Length)
            {
                order.FirstUseNum = order.Frequency.Times.Length.ToString();
                Classes.Function.ShowBalloonTip(2, order.Item.Name + "[" + order.Item.Specs + "] ����������\r\nϵͳ�Զ�����Ϊ" + order.Frequency.Times.Length.ToString(), "��ʾ��", ToolTipIcon.Info);
            }

            //�����ʼʱ�䲻�ǵ��죬ϵͳĬ��������Ϊ0
            string moTime = order.MOTime.ToString("yyyy-MM-dd");
            if (!moTime.Equals(order.BeginTime.ToString("yyyy-MM-dd")) && !"0".Equals(order.FirstUseNum))
            {
                order.FirstUseNum = "0";
                Classes.Function.ShowBalloonTip(2, order.Item.Name + "�Ŀ�ʼʱ��[" + order.BeginTime.ToString("yyyy-MM-dd") + "] ���ǵ��죬\r\nϵͳ�Զ�����������Ϊ0", "��ʾ��", ToolTipIcon.Info);
            }

            if (Int32.TryParse(order.FirstUseNum, out times) && times >= 0
                && order.Frequency.Times.Length > 0)
            {
                //��������
                //������㡢����ʼʱ����Ϊ�ڶ�������
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
                        //���ݴ�����ȷ��ҽ����ʼʱ��
                        order.NextMOTime = FS.FrameWork.Function.NConvert.ToDateTime(order.BeginTime.ToString("yyyy-MM-dd") + " " + execTimes[execTimes.Length - times]);
                    }
                }

                order.CurMOTime = order.NextMOTime;
            }
            #endregion

            return order;
        }

        #endregion

        #region �¼�

        #region ��Ŀ�仯

        /// <summary>
        /// ҽ���仯����
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
                if (!this.EditGroup)//{E679E3A6-9948-41a8-B390-DD9A57347681}�жϲ��ǿ���ҽ��ģʽ�Ͳ�������ӿ�
                {
                    #region ���ݽӿ�ʵ�ֶ�ҽ����Ϣ���в����ж�

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

                #region ����

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

                    //������ǰҽ����ȫͣ����
                    if (!this.EditGroup)
                    {
                        //��������ǰҽ����������ҽ��
                        if (!sender.OrderType.isCharge && sender.Item.Name.IndexOf("��ǰҽ��") >= 0)
                        {
                            this.DcAllLongOrder("");
                        }
                    }

                    ShowPactItem();
                }
                #endregion

                #region ɾ��
                else if (this.ucItemSelect1.OperatorType == Operator.Delete)
                {

                }
                #endregion

                #region �޸�
                else if (this.ucItemSelect1.OperatorType == Operator.Modify)
                {
                    ArrayList alRows = GetSelectedRows();
                    //�޸�
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
                                        || changedField == ColmSet.Z���
                                        || changedField == ColmSet.PƵ��
                                        || changedField == ColmSet.Y�÷�
                                        || changedField == ColmSet.F����
                                        || changedField == ColmSet.K��ʼʱ��
                                        || changedField == ColmSet.Tֹͣʱ��
                                        || changedField == ColmSet.S������)
                                    {
                                        //��ϵ�һ���޸�
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
                    //������ǰҽ����ȫͣ����
                    if (!this.EditGroup)
                    {
                        if (!sender.OrderType.isCharge && sender.Item.Name.IndexOf("��ǰҽ��") >= 0)
                        {
                            this.DcAllLongOrder("");
                        }
                    }


                    //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    if (changedField == ColmSet.Z���)
                    {
                        #region �����ͬ��һ��ѡ��
                        //���������ѡ��
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

                    if (changedField == ColmSet.Y�÷�)
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
        /// �޸ĵ�����Ŀ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        private void GetOrderChanged(int rowIndex, FS.HISFC.Models.Order.Inpatient.Order inOrder, string changedField)
        {
            if (changedField == "!")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["!"]].Text = inOrder.Note;
            }
            else if (changedField == "��Ч")
            {
                if (inOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��Ч"]].Text = "����";
                }
                else if (inOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��Ч"]].Text = "��ʱ";     //0
                }
            }
            else if (changedField == "ҽ������")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ҽ������"]].Text = inOrder.OrderType.Name;    //0
            }
            else if (changedField == "ҽ����ˮ��")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ҽ����ˮ��"]].Text = inOrder.ID;
            }
            else if (changedField == "ҽ��״̬")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ҽ��״̬"]].Text = inOrder.Status.ToString();
            }
            else if (changedField == "��Ϻ�")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��Ϻ�"]].Text = inOrder.Combo.ID;
            }
            else if (changedField == "��ҩ")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��ҩ"]].Text = FS.FrameWork.Function.NConvert.ToInt32(inOrder.Combo.IsMainDrug).ToString();
            }
            else if (changedField == "���")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["������"]].Text = inOrder.FirstUseNum;

                if (inOrder.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = inOrder.HerbalQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = "";
                }

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["Ƶ��"]].Text = inOrder.Frequency.ID.ToString();
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["Ƶ������"]].Text = inOrder.Frequency.Name;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["������λ"]].Text = inOrder.Unit;
                if (inOrder.Item.ItemType != EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.Unit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.DoseUnit;
                }

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��ʼʱ��"]].Text = inOrder.BeginTime.ToString();

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["���"]].Text = inOrder.SubCombNO.ToString();
                if (inOrder.SortID <= 0)
                {
                    inOrder.SortID = this.GetSortIDBySubCombNo(inOrder.SubCombNO);
                }
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["˳���"]].Text = inOrder.SortID.ToString();
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��Ϻ�"]].Text = inOrder.Combo.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["�÷�"]].Text = inOrder.Usage.Name;

                #region add by zhaorong at 2013-8-29 start ҽ�����÷���ִ�п��Ҿ���ά���ĳ���������ʱ��Ĭ��ִ�п���Ϊ��6030��������������ҩ����
                this.ChangeStockDept(ref inOrder);
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ȡҩҩ������"]].Text = inOrder.StockDept.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ȡҩҩ��"]].Text = inOrder.StockDept.Name;
                #endregion

                if (inOrder.EndTime > DateTime.MinValue)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ֹͣʱ��"]].Text = inOrder.DCOper.OperTime.ToString();
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����ʱ��"]].Text = inOrder.EndTime.ToString();
                }
            }
            else if (changedField == "����ʱ��")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����ʱ��"]].Text = inOrder.MOTime.ToString();
            }
            else if (changedField == "����ҽ��")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����ҽ��"]].Text = inOrder.ReciptDoctor.Name;
            }
            else if (changedField == "˳���")
            {
                if (inOrder.SortID <= 0)
                {
                    inOrder.SortID = this.GetSortIDBySubCombNo(inOrder.SubCombNO);
                }
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["˳���"]].Text = inOrder.SortID.ToString();
            }
            else if (changedField == "ҽ������")
            {
                this.AddObjectToFarpoint(inOrder, rowIndex, fpOrder.ActiveSheetIndex, "����");
            }
            else if (changedField == "��")
            {

            }
            else if (changedField == "������")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["������"]].Text = inOrder.FirstUseNum;
            }
            else if (changedField == "ÿ������" || changedField == "��λ")
            {
                if (inOrder.Item.ItemType == EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = FS.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce);
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.DoseUnit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.Unit;
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty);
                }

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["������λ"]].Text = inOrder.Unit;

            }
            else if (changedField == "Ƶ��" || changedField == "Ƶ������")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["Ƶ��"]].Text = inOrder.Frequency.ID.ToString();
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["Ƶ������"]].Text = inOrder.Frequency.Name;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["������λ"]].Text = inOrder.Unit;
                if (inOrder.Item.ItemType != EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.Unit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.DoseUnit;
                }
            }
            else if (changedField == "�÷�����" || changedField == "�÷�")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["�÷�"]].Text = inOrder.Usage.Name;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["�÷�����"]].Text = inOrder.Usage.ID;

                #region add by zhaorong at 2013-8-29 start ҽ�����÷���ִ�п��Ҿ���ά���ĳ���������ʱ��Ĭ��ִ�п���Ϊ��6030��������������ҩ����
                this.ChangeStockDept(ref inOrder);
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ȡҩҩ������"]].Text = inOrder.StockDept.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ȡҩҩ��"]].Text = inOrder.StockDept.Name;
                #endregion
            }
            else if (changedField == "����" || changedField == "������λ")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["������λ"]].Text = inOrder.Unit;
                if (inOrder.Item.ItemType != EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.Unit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.DoseUnit;
                }
            }
            else if (changedField == "����")
            {
                if (inOrder.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = inOrder.HerbalQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = "";
                }

                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["������λ"]].Text = inOrder.Unit;

                if (inOrder.Item.ItemType != EnumItemType.Drug)
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.Unit;
                }
                else
                {
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ÿ������"]].Text = inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��λ"]].Text = inOrder.DoseUnit;
                }
            }
            else if (changedField == "ϵͳ���")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ϵͳ���"]].Text = inOrder.Item.SysClass.Name;
            }
            else if (changedField == "��ʼʱ��")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��ʼʱ��"]].Text = inOrder.BeginTime.ToString();
            }
            else if (changedField == "ֹͣʱ��")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ֹͣʱ��"]].Text = inOrder.DCOper.OperTime.ToString();
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����ʱ��"]].Text = inOrder.EndTime.ToString();
            }
            else if (changedField == "ִ�п��ұ���" || changedField == "ִ�п���")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ִ�п��ұ���"]].Text = inOrder.ExeDept.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ִ�п���"]].Text = inOrder.ExeDept.Name;
            }
            else if (changedField == "��")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��"]].Value = inOrder.IsEmergency;
            }
            else if (changedField == "��鲿λ")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��鲿λ"]].Text = inOrder.CheckPartRecord;
            }
            else if (changedField == "��������")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��������"]].Text = inOrder.Sample.Name;
            }
            else if (changedField == "ȡҩҩ������" || changedField == "ȡҩҩ��")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ȡҩҩ������"]].Text = inOrder.StockDept.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ȡҩҩ��"]].Text = inOrder.StockDept.Name;
            }
            else if (changedField == "��ע")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��ע"]].Text = inOrder.Memo;
            }
            else if (changedField == "¼���˱���" || changedField == "¼����")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["¼���˱���"]].Text = inOrder.Oper.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["¼����"]].Text = inOrder.Oper.Name;
            }
            else if (changedField == "��������")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["��������"]].Text = inOrder.ReciptDept.Name;
            }
            else if (changedField == "ֹͣ�˱���" || changedField == "ֹͣ��")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ֹͣ�˱���"]].Text = inOrder.DCOper.ID;
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["ֹͣ��"]].Text = inOrder.DCOper.Name;
            }
            else if (changedField == "����")
            {
                this.AddObjectToFarpoint(inOrder, rowIndex, fpOrder.ActiveSheetIndex, "����");
            }
            else if (changedField == "����")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����"]].Text = inOrder.Dripspreed;

            }
            else if (changedField == "����ҽ������")
            {
                this.fpOrder.ActiveSheet.Cells[rowIndex, dicColmSet["����ҽ������"]].Text = inOrder.CountryCode;

            }
            this.fpOrder.ActiveSheet.Rows[rowIndex].Tag = inOrder;
        }

        #endregion

        /// <summary>
        /// ˢ��ҽ��״̬
        /// </summary>
        /// <param name="row"></param>
        /// <param name="SheetIndex"></param>
        /// <param name="reset"></param>
        private void ChangeOrderState(int row, int SheetIndex, bool reset)
        {
            try
            {
                int i = dicColmSet["ҽ��״̬"];//this.GetColumnIndexFromName("ҽ��״̬");
                int state = int.Parse(this.fpOrder.Sheets[SheetIndex].Cells[row, i].Text);

                //if (GetObjectFromFarPoint(row, SheetIndex).ID != "" && reset)
                //{
                //    state = CacheManager.InOrderMgr.QueryOneOrderState(GetObjectFromFarPoint(row, SheetIndex).ID);
                //    this.fpOrder.Sheets[SheetIndex].Cells[row, i].Value = state;
                //}

                //����Ӽ���ʾ
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpOrder.Sheets[SheetIndex].Cells[row, this.dicColmSet["��"]].Value))
                {
                    fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].Label = "��";
                }
                else
                {
                    fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].Label = "";
                }

                //����ҽ������ֹͣ��ɫ��ʾ
                if (state != 3 && state != 4
                    && this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ֹͣʱ��"]].Text != "")
                {
                    //��ɫ
                    this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(132, 72, 168);
                }
                else
                {
                    switch (state)
                    {
                        case 0: //�¿�������ɫ
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(128, 255, 128);
                            break;
                        case 1://��� ǳ��ɫ
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(106, 174, 242);
                            break;
                        case 2://ִ�С�ǳ��ɫ
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(243, 230, 105);
                            break;
                        case 3://ֹͣ��ǳ��ɫ
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(248, 120, 222);
                            break;
                        case 4://Ԥֹͣ ǳ��ɫ
                            this.fpOrder.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(248, 120, 222);
                            break;
                        default: //�����ҽ�� ��ɫ
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
        /// ѡ��ҽ���޸�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpOrder_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            SelectionChanged();

            ShowPactItem();
        }

        /// <summary>
        /// ��ʾ��Ŀ��Ϣ
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

                #region ��ʾ��Ŀ��Ϣ

                FS.HISFC.Models.Order.Inpatient.Order inOrder = GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex);
                if (inOrder == null)
                {
                    return -1;
                }
                this.pnItemInfo.Visible = true;
                txtItemInfo.ReadOnly = true;

                string showInfo = "";

                //��Ŀ��Ϣ
                if (inOrder.Item.ID == "999")
                {
                    showInfo += inOrder.Item.Name + " �����" + inOrder.Item.Specs + " �����ۡ�" + inOrder.Item.Price.ToString() + "Ԫ/" + inOrder.Item.PriceUnit;
                }
                else
                {
                    if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).UserCode + " " + inOrder.Item.Name + " �����" + inOrder.Item.Specs + " �����ۡ�" + inOrder.Item.Price.ToString() + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).PackUnit;
                        
                        if (!string.IsNullOrEmpty(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).Product.Manual))
                        {
                            showInfo += "\r\n" + "��ҩƷ˵����" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).Product.Manual;
                        }
                    }
                    else
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Item.ID).UserCode + " " + inOrder.Item.Name + " �����" + inOrder.Item.Specs + " �����ۡ�" + inOrder.Item.Price.ToString() + "Ԫ/" + inOrder.Item.PriceUnit;
                    }

                    
                }
                if (inOrder.Item.ID != "999")
                {
                    #region ��Ŀ��չ��Ϣ��ʾ

                    string itemShowInfo = "";

                    if (myPatientInfo != null && !string.IsNullOrEmpty(myPatientInfo.ID))
                    {
                        FS.HISFC.Models.SIInterface.Compare compare = Classes.Function.GetPactItem(inOrder);
                        inOrder.Patient.Pact = this.myPatientInfo.Pact;
                        if (compare != null)
                        {
                            //ҽ��������Ϣ
                            itemShowInfo += "\r\n��" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(inOrder.Patient.Pact.ID).Name + "�� " + Classes.Function.GetItemGrade(compare.CenterItem.ItemGrade) + " " + (compare.CenterItem.Rate > 0 ? compare.CenterItem.Rate.ToString("p0") : "") + (compare.CenterFlag == "1" ? "����������" : "");


                            //ҽ��������ҩ��Ϣ
                            if (!string.IsNullOrEmpty(compare.Practicablesymptomdepiction))
                            {
                                itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\r\n" : " ") + compare.Practicablesymptomdepiction;
                            }
                        }
                    }

                    //����ҩ����ʾ
                    string ss = Classes.Function.GetPhaEssentialDrugs(inOrder.Item);
                    if (!string.IsNullOrEmpty(ss))
                    {
                        itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\r\n" : " ") + "[" + ss + "]";
                    }

                    //������ҩ��ʾ
                    ss = Classes.Function.GetPhaForTumor(inOrder.Item);
                    if (!string.IsNullOrEmpty(ss))
                    {
                        itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\r\n" : " ") + "[" + ss + "]";
                    }

                    //��Ŀ�ں� ����

                    showInfo += itemShowInfo;

                    #endregion

                    //�ײ���ϸ
                    if (inOrder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Item.ID);
                        if (undrug.UnitFlag == "1")
                        {
                            showInfo += "\r\n���ײͰ�������";

                            ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(inOrder.Item.ID);
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb comb in alZt)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(comb.ID);

                                //{BC67FD5E-77CE-410f-B642-518D7420BF93}
                                //���շ���Ŀ����Ϊҽ�������Կ�����ʱ�򣬾��޷���ѯ��undrug��Ϣ������һ���û�����ʱ����β�ѯ
                                if (combUndrug == null)
                                {
                                    FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                                    combUndrug = itemMgr.GetUndrugByCode(comb.ID);
                                }

                                showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "]") + " " + comb.Qty + combUndrug.PriceUnit + "��";
                            }
                        }
                    }


                    //������Ϣ
                    FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();
                    ArrayList alSub = subMgr.GetSubtblInfoByItem("1", inOrder.ReciptDept.ID, inOrder.Item.ID, inOrder.Usage.ID);
                    if (alSub != null && alSub.Count > 0)
                    {
                        showInfo += "\r\n�����Ĵ�����(���ο�)��";
                        foreach (FS.HISFC.Models.Order.OrderSubtblNew sub in alSub)
                        {
                            FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(sub.Item.ID);
                            showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "] ") + "��";
                        }
                    }
                }
                //��ȡ��ҩƷ�ı�ע��Ϣ add by yerl
                FS.SOC.HISFC.Fee.Models.Undrug undrugInfo = new FS.SOC.HISFC.Fee.BizLogic.Undrug().GetUndrug(inOrder.Item.ID);

                if (undrugInfo != null)
                {
                    if (!string.IsNullOrEmpty(undrugInfo.Memo))
                    {
                        showInfo += "\r\n";
                        showInfo += "��ע�����" + undrugInfo.Memo;
                    }
                    showInfo += "\r\n";
                    //showInfo += "����ע�������������,���µ���ؿ��ң�[������]38379766 [�ھ�����]38254166 [�����]38286789";
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
            //ÿ��ѡ��仯ǰ���������ʾ Add By liangjz 2005-08
            this.ucItemSelect1.Clear(false);

            if (this.fpOrder.ActiveSheet.RowCount <= 0)
            {
                return;
            }

            if (!this.IsDesignMode && !this.EditGroup)
            {
                return;
            }

            //�¿��� ���ܸ���
            if (int.Parse(this.fpOrder.ActiveSheet.Cells[this.fpOrder.ActiveSheet.ActiveRowIndex, dicColmSet["ҽ��״̬"]].Text) == 0)
            {
                if (this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex).GetFlag == "0")
                {
                    #region
                    //����Ϊ��ǰ��
                    this.ucItemSelect1.CurrentRow = this.fpOrder.ActiveSheet.ActiveRowIndex;
                    this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex);

                    this.ucItemSelect1.Order = this.currentOrder;

                    //���������ѡ��
                    if (this.ucItemSelect1.Order.Combo.ID != "" && this.ucItemSelect1.Order.Combo.ID != null)
                    {
                        int comboNum = 0;//��õ�ǰѡ������
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
                            //ֻ��һ��
                            if (OrderCanCancelComboChanged != null)
                            {
                                this.OrderCanCancelComboChanged(false);//����ȡ�����
                            }
                        }
                        else
                        {
                            if (OrderCanCancelComboChanged != null)
                            {
                                this.OrderCanCancelComboChanged(true);//����ȡ�����   
                            }
                        }
                    }

                    if (OrderCanSetCheckChanged != null)
                    {
                        this.OrderCanSetCheckChanged(true);//��ӡ������뵥ʧЧ
                    }

                    #endregion
                }
                else
                {
                    Classes.Function.ShowBalloonTip(2, "��ʾ", "��Ŀ��" + this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex).Item.Name + "���Ѿ���ӡ���������޸ģ�", ToolTipIcon.Info);
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

        #region ����
        public FS.HISFC.Models.Order.Inpatient.Order SelectedOrder { get { return this.GetObjectFromFarPoint(fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex); } }
        /// <summary>
        /// �Ƿ���ģʽ
        /// </summary>
        protected bool bIsDesignMode = false;

        /// <summary>
        /// �Ƿ���ʾ�Ҽ��˵�
        /// </summary>
        protected bool bIsShowPopMenu = true;

        /// <summary>
        /// �Ƿ���ʾ�Ҽ��˵�
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
        /// �Ƿ���ģʽ
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
        /// ���߻�����Ϣ
        /// </summary>
        public void SetPatient(FS.HISFC.Models.RADT.PatientInfo value)
        {
            if (!EditGroup)//���2�����׹���ť ������ť����Ӧ
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
        /// Ĭ�ϳ���ҽ��
        /// </summary>
        protected FS.HISFC.Models.Order.EnumType myOrderType = FS.HISFC.Models.Order.EnumType.LONG;

        /// <summary>
        /// ���ó���ҽ������
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
        /// ��ǰ��������
        /// </summary>
        [DefaultValue(null)]
        public void SetReciptDept(FS.FrameWork.Models.NeuObject value)
        {

            this.myReciptDept = value;

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.myReciptDept == null)
                {
                    myReciptDept = new FS.FrameWork.Models.NeuObject();
                    this.myReciptDept.ID = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoc()).Dept.ID; //��������
                    this.myReciptDept.Name = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoc()).Dept.Name;
                }
            }
            catch { }
            return this.myReciptDept;
        }

        protected FS.FrameWork.Models.NeuObject myReciptDoc = null;

        /// <summary>
        /// ��ǰ����ҽ��
        /// </summary>
        public void SetReciptDoc(FS.FrameWork.Models.NeuObject value)
        {
            this.myReciptDoc = value;
        }

        /// <summary>
        /// ��ȡ����ҽ��
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
        /// �Ƿ��޸Ĺ�ҽ��
        /// </summary>
        private bool isEdit = false;

        /// <summary>
        /// �Ƿ�
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
        /// ��ʾindex
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
        /// �Ƿ�ʿ����
        /// </summary>
        private bool isNurseCreate = false;

        /// <summary>
        /// �Ƿ�ʿ����
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

        #region ����

        /// <summary>
        /// ���ʵ��toTable
        /// </summary>
        /// <param name="list"></param>
        private void AddObjectsToTable(ArrayList list)
        {
            if (dsAllLong != null)//�������BY zuowy 2005-9-15
                dsAllLong.Tables[0].Clear();//ԭ��û������
            if (dsAllShort != null)//�������BY zuowy 2005-9-15
                dsAllShort.Tables[0].Clear();//ԭ��û������
            foreach (object obj in list)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    //����ҽ��

                    dsAllLong.Tables[0].Rows.Add(AddObjectToRow(order, dsAllLong.Tables[0]));
                }
                else
                {
                    //��ʱҽ��
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
                row["��ҩ"] = FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug);//5
                row["ÿ������"] = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');//9
                row["��λ"] = objItem.DoseUnit;//0415 2307096 wang renyi
            }
            else if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                row["ÿ������"] = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//9
                row["��λ"] = order.Unit;
            }

            if (order.Item.SysClass.ID.ToString() == "PCC")
            {
                row["����"] = order.HerbalQty;//11
            }
            else
            {
                row["����"] = "";
            }

            if (order.Note != "")
            {
                row["!"] = order.Note;
            }
            row["��Ч"] = FS.FrameWork.Function.NConvert.ToInt32(order.OrderType.ID);     //0
            row["ҽ������"] = order.OrderType.Name;//1
            row["ҽ����ˮ��"] = order.ID;//2
            row["ҽ��״̬"] = order.Status;//�¿�������ˣ�ִ��
            row["��Ϻ�"] = order.Combo.ID;//4

            row["ϵͳ���"] = order.Item.SysClass.Name;

            row["ҽ������"] = this.ShowOrderName(order);

            //ҽ����ҩ-֪��ͬ����
            if (order.IsPermission)
                row["ҽ������"] = "���̡�" + row["ҽ������"];

            ValidNewOrder(order);
            row["������"] = order.FirstUseNum;
            row["����"] = order.Qty;//7
            row["������λ"] = order.Unit;//8
            row["Ƶ�α���"] = order.Frequency.ID;
            row["Ƶ������"] = order.Frequency.Name;
            row["�÷�����"] = order.Usage.ID;
            row["�÷�����"] = order.Usage.Name;//15
            row["��ʼʱ��"] = order.BeginTime;
            row["ִ�п��ұ���"] = order.ExeDept.ID;
            //if(order.ExeDept.Name == "" && order.ExeDept.ID !="" ) order.ExeDept.Name = this.GetDeptName(order.ExeDept);
            row["ִ�п���"] = order.ExeDept.Name;
            row["�Ӽ�"] = order.IsEmergency;
            row["��鲿λ"] = order.CheckPartRecord;
            row["��������"] = order.Sample;
            row["�ۿ���ұ���"] = order.StockDept.ID;
            row["�ۿ����"] = order.StockDept.Name;

            row["��ע"] = order.Memo;//20
            row["¼���˱���"] = order.Oper.ID;
            row["¼����"] = order.Oper.Name;
            row["����ҽ��"] = order.ReciptDoctor.Name;
            row["��������"] = order.ReciptDept.Name;
            row["����ʱ��"] = order.MOTime;
            row["���"] = order.SubCombNO.ToString();

            if (order.EndTime != DateTime.MinValue)
            {
                row["ֹͣʱ��"] = order.DCOper.OperTime;//25
                row["����ʱ��"] = order.EndTime;//25
            }

            row["ֹͣ�˱���"] = order.DCOper.ID;
            row["ֹͣ��"] = order.DCOper.Name;

            row["����"] = order.Dripspreed;

            row["����ҽ������"] = order.CountryCode;

            row["˳���"] = order.SortID;//28

            #region {1AF0EB93-27A8-462f-9A1E-E1A3ECA54ADE} ��ҽ�������ϣ������ٶ�
            if (!this.htOrder.ContainsKey(order.ID))
            {
                this.htOrder.Add(order.ID, order);
            }
            #endregion

            return row;
        }

        /// <summary>
        /// ����ʱ���
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
                    //����ҽ��
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
        /// ��ʾҽ��������
        /// </summary>
        /// <param name="inOrder"></param>
        /// <returns></returns>
        private string ShowOrderName(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            string strShowName = inOrder.Item.Name;

            string price = "";

            if (inOrder.Item.ID == "999" || !inOrder.OrderType.IsCharge)
            {
                price = "[" + "0Ԫ/" + inOrder.Item.PriceUnit + "]";
            }
            else
            {
                if (inOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (inOrder.Unit == SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).PackUnit)
                    {
                        price = "[" + inOrder.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).PriceUnit + "]";
                    }
                    else
                    {
                        price = "[" + (inOrder.Item.Price / ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).MinUnit + "]";
                    }
                }
                else
                {
                    if (inOrder.Item.Price > 0)
                    {
                        price = "[" + inOrder.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + ((FS.HISFC.Models.Fee.Item.Undrug)inOrder.Item).PriceUnit.Trim() + "]";
                    }
                }
            }

            //�Ա������б��  ���ڻ�ʿ��ӡ���ݺ�ҽ������ʾ����
            string byoStr = "";

            if (!inOrder.OrderType.IsCharge || inOrder.Item.ID == "999")
            {
                if (!strShowName.Contains("�Ա�")
                    && !strShowName.Contains("����"))
                {
                    if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        byoStr = "[�Ա�]";
                    }
                    else
                    {
                        byoStr = "[����]";
                    }
                }
            }

            strShowName = byoStr + strShowName;

            //Ƥ����ʾ
            strShowName += CacheManager.InOrderMgr.TransHypotest(inOrder.HypoTest);

            //ҽ������ 
            if (inOrder.Item.Specs == null || inOrder.Item.Specs.Trim() == "")
            {
                return strShowName + (inOrder.IsPermission ? "���̡�" : "") + price;
            }
            else
            {
                return strShowName + (inOrder.IsPermission ? "���̡�" : "") + "[" + inOrder.Item.Specs + "]" + price;
            }
        }

        /// <summary>
        /// ҽ���޸ĸ���
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row">��</param>
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
                MessageBox.Show("Clone����" + ex.Message);
                return;
            }

            try
            {
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["���"]].Text = order.SubCombNO.ToString();

                if (orderlist == ColmSet.ALL)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["!"]].Text = order.Note;
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["!"]].Note = order.Note;
                }
                //{D18CDB1B-BB1E-422d-9161-65D9CEC79C05}
                string orderitemname = ShowOrderName(order);
                if (orderitemname.Contains("������") && (DateTime.Now.Date.Subtract(this.Patient.Birthday.Date).Days + 1) > 365 * 16)//��������
                {
                    if (MessageBox.Show("��������ĿӦ�ÿ���BB����,�����Ϲ淶", "�Ƿ�ȷ��Ҫ����������", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        this.fpOrder.Sheets[SheetIndex].Rows[row].Remove();
                        return;
                    }
                }
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    //ҩƷ
                    FS.HISFC.Models.Pharmacy.Item objItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                    if (orderlist == ColmSet.ALL || orderlist == ColmSet.Mÿ������ || orderlist == ColmSet.D��λ)
                    {
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ÿ������"]].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');//9
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��λ"]].Text = order.DoseUnit;
                    }
                    if (orderlist == ColmSet.ALL || orderlist == ColmSet.F����)
                    {
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����"]].Text = order.HerbalQty.ToString();
                        }
                        else
                        {
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����"]].Text = "";
                        }
                    }

                    if (order.OrderType.IsDecompose)
                    {
                        if (orderlist == ColmSet.ALL || orderlist == ColmSet.Z���� || orderlist == ColmSet.Z������λ)
                        {
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����"]].Text = order.DoseOnce.ToString();//7
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["������λ"]].Text = order.DoseUnit;//8
                        }
                    }
                    else //��ʱ
                    {
                        if (orderlist == ColmSet.ALL || orderlist == ColmSet.Z���� || orderlist == ColmSet.Z������λ)
                        {
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����"]].Text = FS.FrameWork.Public.String.ToSimpleString(order.Qty);//7
                            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["������λ"]].Text = order.Unit;//8
                        }
                    }
                }
                else if (order.Item.ItemType == EnumItemType.UnDrug)
                {
                    //��ҩƷ
                    if (orderlist == ColmSet.ALL || orderlist == ColmSet.Z���� || orderlist == ColmSet.Z������λ)
                    {
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ÿ������"]].Text = FS.FrameWork.Public.String.ToSimpleString(order.Qty);//9
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����"]].Text = FS.FrameWork.Public.String.ToSimpleString(order.Qty);
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��λ"]].Text = order.Unit;//������λ
                        this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["������λ"]].Text = order.Unit;//8
                    }
                }

                this.ValidNewOrder(order); //��д��Ϣ

                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��Ч"]].Text = "����";

                }
                else if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��Ч"]].Text = "��ʱ";     //0
                }

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��ҩ"]].Text = FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug).ToString();//5

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ҽ������"]].Text = order.OrderType.Name; //1 ����

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ҽ������"]].Text = ShowOrderName(order);



                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ҽ����ˮ��"]].Text = order.ID;//2
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ҽ��״̬"]].Text = order.Status.ToString();//�¿�������ˣ�ִ��
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��Ϻ�"]].Text = order.Combo.ID.ToString();//4
                //}

                if (orderlist == ColmSet.ALL || orderlist == ColmSet.PƵ��)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["Ƶ��"]].Text = order.Frequency.ID.ToString();
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["Ƶ������"]].Text = order.Frequency.Name;
                }
                if (orderlist == ColmSet.ALL || orderlist == ColmSet.Y�÷�)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["�÷�����"]].Text = order.Usage.ID;
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["�÷�"]].Text = order.Usage.Name;//15
                }

                if (orderlist == ColmSet.ALL || orderlist == ColmSet.K��ʼʱ��)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��ʼʱ��"]].Text = order.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");//��ʼʱ��
                }
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����ʱ��"]].Text = order.MOTime.ToString();//����ʱ��

                if (orderlist == ColmSet.ALL || orderlist == ColmSet.Zִ�п���)
                {
                    if (string.IsNullOrEmpty(order.ExeDept.ID))
                    {
                        //order.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(order.ReciptDept, order, order.ExeDept.ID, false);
                        order.ExeDept.ID = Components.Order.Classes.Function.GetExecDept(false, order.ReciptDept.ID, order.ExeDept.ID, order.Item.ID);
                        order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(order.ExeDept.ID);
                    }

                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ִ�п��ұ���"]].Text = order.ExeDept.ID;
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ִ�п���"]].Text = order.ExeDept.Name;
                }
                if (orderlist == ColmSet.ALL || orderlist == ColmSet.J��)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��"]].Value = order.IsEmergency;
                }

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��鲿λ"]].Text = order.CheckPartRecord;//��鲿λ
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��������"]].Text = order.Sample.Name;//��������
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ȡҩҩ������"]].Text = order.StockDept.ID;//�ۿ����
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ȡҩҩ��"]].Text = order.StockDept.Name;

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��ע"]].Text = order.Memo;//20
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["¼���˱���"]].Text = order.Oper.ID;
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["¼����"]].Text = order.Oper.Name;

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����ҽ��"]].Text = order.ReciptDoctor.Name;//����ҽ��
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["��������"]].Text = order.ReciptDept.Name;//��������

                if (order.EndTime != DateTime.MinValue)
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ֹͣʱ��"]].Text = order.DCOper.OperTime.ToString();//ֹͣʱ�� 25
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����ʱ��"]].Text = order.EndTime.ToString();//ֹͣʱ�� 25
                }
                else
                {
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ֹͣʱ��"]].Text = "";//ֹͣʱ�� 25
                    this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����ʱ��"]].Text = "";//ֹͣʱ�� 25
                }
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ֹͣ�˱���"]].Text = order.DCOper.ID;
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ֹͣ�˱���"]].Text = order.DCOper.Name;
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����"]].Text = order.Dripspreed;

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

                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["����ҽ������"]].Text = string.IsNullOrEmpty(gbcode) ? order.CountryCode : gbcode;

                order.CountryCode = gbcode;



                //��������ϵͳ���
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["ϵͳ���"]].Text = order.Item.SysClass.Name;
                this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["������"]].Text = order.FirstUseNum;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��Fp�����Ϣʱ����" + ex.Message, "��ʾ");
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
                order.Frequency.Usage = order.Usage; //�÷�����
            }
            this.fpOrder.Sheets[SheetIndex].Cells[row, dicColmSet["˳���"]].Value = order.SortID;//28


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
        /// ������Ϣ
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
                //����ִ�п���Ϊ���߿���
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
            #region {D17BD9FB-F362-4755-97FE-08404D477C39} ���2�����׹���ť ������ť����Ӧ
            this.fpOrder.Sheets[0].RowCount = 0;
            this.fpOrder.Sheets[1].RowCount = 0;
            #endregion
            this.fpOrder.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
            this.fpOrder.Sheets[1].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
        }

        /// <summary>
        /// ��ȡѡ�������
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
        /// ˢ�����
        /// </summary>
        public void RefreshCombo()
        {
            try
            {
                /*- 
                 *  Edit By liangjz 2005-10 ������ϵ��ظ�ˢ�� �ڳ�����������ʱ��refreshComboFlag����ֵͬ ����ˢ��
                ---*/

                //��ǵ�ǰѡ����
                if (this.ActiveRowIndex >= 0 && this.ActiveRowIndex < this.fpOrder.ActiveSheet.RowCount)
                {
                    this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["˳���"]].Tag = "����";
                }

                if (this.refreshComboFlag == "0" || this.refreshComboFlag == "2")
                {
                    try
                    {
                        //if (!this.IsDesignMode)
                        //{
                        //    this.fpOrder.Sheets[0].SortRows(dicColmSet["˳���"], true, true);
                        //}
                        //else
                        //{
                        this.fpOrder.Sheets[0].SortRows(dicColmSet["˳���"], false, true);
                        //}
                    }
                    catch { }

                    Classes.Function.DrawCombo(this.fpOrder.Sheets[0], dicColmSet["��Ϻ�"], dicColmSet["��"]);
                }

                if (this.refreshComboFlag == "1" || this.refreshComboFlag == "2")
                {
                    try
                    {
                        //if (!this.IsDesignMode)
                        //{
                        //    this.fpOrder.Sheets[1].SortRows(dicColmSet["˳���"], true, true);
                        //}
                        //else
                        //{
                        this.fpOrder.Sheets[1].SortRows(dicColmSet["˳���"], false, true);
                        //}
                    }
                    catch { }

                    Classes.Function.DrawCombo(this.fpOrder.Sheets[1], dicColmSet["��Ϻ�"], dicColmSet["��"]);

                }

                //��ǵ�ǰѡ����
                //for (int i = this.fpOrder.ActiveSheet.RowCount - 1; i >= 0; i--)
                for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
                {
                    if (this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag != null &&
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag.ToString() == "����")
                    {
                        this.ActiveRowIndex = i;
                        this.fpOrder.ActiveSheet.ActiveRowIndex = i;
                        this.fpOrder.ActiveSheet.AddSelection(i, 0, 1, this.fpOrder.ActiveSheet.ColumnCount);
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag = null;
                        break;
                    }
                }

                //��ֵΪĬ��ֵ
                this.refreshComboFlag = "2";
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ˢ��ҽ�������Ϣʱ���ֲ���Ԥ֪�������˳������������Ի������Ա��ϵ!\n") + ex.Message);
            }
        }

        /// <summary>
        /// ����ҽ��״̬
        /// </summary>
        /// <param name="rowIndex">ˢ�µ��кţ�-1 ��ʾȫ��</param>
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
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ˢ��ҽ��״̬ʱ���ֲ���Ԥ֪�������˳������������Ի������Ա��ϵ"));
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
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ˢ��ҽ��״̬ʱ���ֲ���Ԥ֪�������˳������������Ի������Ա��ϵ"));
            }
        }

        /// <summary>
        /// ��ҽ��У��
        /// </summary>
        /// <param name="alSaveOrder">��ҽ���б�</param>
        /// <returns></returns>
        public int CheckOrder(ref ArrayList alSaveOrder)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;

            alSaveOrder = new ArrayList();

            //�Ƿ���Կ������Ϊ0 ҩƷ
            int iCheck = Classes.Function.GetIsOrderCanNoStock();
            bool IsModify = true;

            //������ҩ����ҩƷ
            ArrayList alPassOrder = new ArrayList();
            string errInfo = "";

            #region ����ҽ��
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
                    //δ��˵�ҽ��
                    IsModify = true;

                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        if (order.OrderType.IsCharge || order.Item.ID != "999")
                        {
                            if (string.IsNullOrEmpty(order.StockDept.ID))
                            {
                                ShowErr("[" + order.Item.Name + "]" + "�ۿ����Ϊ�գ�", i, 1);
                                return -1;
                            }
                        }

                        //ҩƷ
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            //�в�ҩ
                            if (order.HerbalQty == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "��������Ϊ�㣡", i, 1);
                                return -1;
                            }
                        }
                        else
                        {
                            if (order.DoseOnce == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "ÿ�μ�������Ϊ�㣡", i, 0);
                                return -1;
                            }
                            if (order.DoseUnit == "")
                            {
                                ShowErr("[" + order.Item.Name + "]" + "������λ����Ϊ�գ�", i, 0);
                                return -1;
                            }
                        }
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "Ƶ�β���Ϊ�գ�", i, 0);
                            return -1;
                        }
                        if (order.Usage.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "�÷�����Ϊ�գ�", i, 0);
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
                                                ShowErr("[" + order.Item.Name + "]" + "���ٲ���Ϊ�գ�", i, 0);
                                                return -1;
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        //if (((FS.HISFC.Models.Pharmacy.Item)order.Item).Price == 0)
                        //{
                        //    if (order.OrderType.Name.IndexOf("����") == -1)
                        //    {
                        //        ShowErr("[" + order.Item.Name + "]" + "�۸�Ϊ�㲻������ȡ��", i, 0);
                        //        return -1;
                        //    }
                        //}

                        //if (Classes.Function.HelperFrequency.GetObjectFromID(order.Frequency.ID) != null &&
                        //    (Classes.Function.HelperFrequency.GetObjectFromID(order.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Times.Length
                        //    != order.Frequency.Times.Length)
                        //{
                        //    ShowErr("[" + order.Item.Name + "]" + "Ƶ��ʱ������������ѡ��", i, 0);
                        //    return -1;
                        //}

                        if (string.IsNullOrEmpty(order.FirstUseNum))
                        {
                            ShowErr("[" + order.Item.Name + "]" + "����������Ϊ�գ�", i, 0);
                            return -1;
                        }
                        else
                        {
                            try
                            {
                                int kk = FS.FrameWork.Function.NConvert.ToInt32(order.FirstUseNum);

                                if (kk < 0)
                                {
                                    ShowErr("[" + order.Item.Name + "]" + "����������������Ч�����������룡", i, 0);
                                    return -1;
                                }
                            }
                            catch
                            {
                                ShowErr("[" + order.Item.Name + "]" + "��������������֣����������룡", i, 0);
                                return -1;
                            }
                        }

                        #region �ж�ͣ��ȱҩ

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
                        //��ҩƷ
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "Ƶ�β���Ϊ�գ�", i, 0);
                            return -1;
                        }
                        if (order.Qty == 0)
                        {

                            ShowErr("[" + order.Item.Name + "]" + "��������Ϊ�գ�", i, 0);
                            return -1;
                        }
                        if (order.ExeDept.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "ִ�п���Ϊ�գ���ѡ��ִ�п��ң�", i, 0);
                            return -1;
                        }
                        //if (order.Item.Price == 0)
                        //{
                        //    if (order.OrderType.Name.IndexOf("����") == -1)
                        //    {
                        //        ShowErr("[" + order.Item.Name + "]" + "�۸�Ϊ�㲻������ȡ��", i, 0);
                        //        return -1;
                        //    }
                        //}
                    }
                    if (order.EndTime != DateTime.MinValue)
                    {
                        if (order.EndTime < order.BeginTime)
                        {
                            ShowErr("[" + order.Item.Name + "]" + "ֹͣʱ�䲻Ӧ���ڿ�ʼʱ��", i, 0);
                            return -1;
                        }
                    }
                    if (FS.FrameWork.Public.String.ValidMaxLengh(order.Memo, 80) == false)
                    {
                        //ShowErr("[" + order.Item.Name + "]" + "�ı�ע����!", i, 0);
                        //return -1;
                        Classes.Function.ShowBalloonTip(1, "����", "[" + order.Item.Name + "]" + "�ı�ע����!\r\n\r\n���ܵ���ҽ������ʾ��ȫ", ToolTipIcon.Warning);
                    }
                    if (!order.OrderType.IsCharge && FS.FrameWork.Public.String.ValidMaxLengh(order.Item.Name, 50) == false)
                    {
                        //ShowErr("[" + order.Item.Name + "]" + "�����Ƴ���!", i, 0);
                        //return -1;
                        Classes.Function.ShowBalloonTip(1, "����", "[" + order.Item.Name + "]" + "�����Ƴ���!\r\n\r\n���ܵ���ҽ������ʾ��ȫ", ToolTipIcon.Warning);
                    }
                    if (order.Qty > 5000)
                    {
                        ShowErr("[" + order.Item.Name + "]" + "����̫��", i, 0);
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

            #region ��ʱҽ��
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
                    //δ��˵�ҽ��
                    IsModify = true;

                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        if (order.OrderType.IsCharge || order.Item.ID != "999")
                        {
                            if (string.IsNullOrEmpty(order.StockDept.ID))
                            {
                                ShowErr("[" + order.Item.Name + "]" + "�ۿ����Ϊ�գ�", i, 1);
                                return -1;
                            }
                        }

                        //ҩƷ
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            //�в�ҩ
                            if (order.HerbalQty == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "��������Ϊ�㣡", i, 1);
                                return -1;
                            }
                        }
                        else
                        {
                            //����
                            if (order.DoseOnce == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "ÿ�μ�������Ϊ�㣡", i, 1);
                                return -1;
                            }
                            if (order.DoseUnit == "")
                            {
                                ShowErr("[" + order.Item.Name + "]" + "������λ����Ϊ�գ�", i, 1);
                                return -1;
                            }
                            try
                            {
                                if (order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.').Contains("."))
                                {
                                    ShowErr("ҩƷ��" + order.Item.Name + "������������ΪС����", i, 1);
                                    return -1;
                                }
                            }
                            catch
                            {
                                ShowErr("ҩƷ��" + order.Item.Name + "������������ΪС����", i, 1);
                                return -1;
                            }
                        }
                        if (order.Item.ID != "999")
                        {
                            if ((order.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose == 0)
                            {
                                ShowErr("[" + order.Item.Name + "]" + "��������Ϊ�㣬û��ά����������", i, 1);
                                return -1;
                            }
                        }
                        if (order.Qty <= 0)
                        {
                            ShowErr("[" + order.Item.Name + "]" + "�����������0��", i, 1);
                            return -1;
                        }
                        if (order.Unit == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "��λ����Ϊ�գ�", i, 1);
                            return -1;
                        }
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "Ƶ�β���Ϊ�գ�", i, 1);
                            return -1;
                        }
                        if (order.Usage.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "�÷�����Ϊ�գ�", i, 1);
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
                                            ShowErr("[" + order.Item.Name + "]" + "���ٲ���Ϊ�գ�", i, 1);
                                            return -1;
                                        }

                                    }
                                }
                            }
                        }

                        //�����(����ҽ������)
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
                        //��ҩƷ
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "Ƶ�β���Ϊ�գ�", i, 1);
                            return -1;
                        }
                        if (order.Qty == 0)
                        {
                            ShowErr("[" + order.Item.Name + "]" + "��������Ϊ�գ�", i, 1);
                            return -1;
                        }
                        if (order.ExeDept.ID == "")
                        {
                            ShowErr("[" + order.Item.Name + "]" + "ִ�п���Ϊ�գ���ѡ��ִ�п��ң�", i, 0);
                            return -1;
                        }
                    }
                    if (!order.OrderType.IsCharge && FS.FrameWork.Public.String.ValidMaxLengh(order.Item.Name, 50) == false)
                    {
                        //ShowErr("[" + order.Item.Name + "]" + "�����Ƴ���!", i, 0);
                        //return -1;
                        Classes.Function.ShowBalloonTip(1, "����", "[" + order.Item.Name + "]" + "�����Ƴ���!\r\n\r\n���ܵ���ҽ������ʾ��ȫ", ToolTipIcon.Warning);
                    }

                    if (FS.FrameWork.Public.String.ValidMaxLengh(order.Memo, 80) == false)
                    {
                        //ShowErr("[" + order.Item.Name + "]" + "�ı�ע����!", i, 0);
                        //return -1;
                        Classes.Function.ShowBalloonTip(1, "����", "[" + order.Item.Name + "]" + "�ı�ע����!\r\n\r\n���ܵ���ҽ������ʾ��ȫ", ToolTipIcon.Warning);
                    }
                    if (order.Qty > 5000)
                    {
                        ShowErr("[" + order.Item.Name + "]" + "����̫��", i, 1);
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
                return -1;//δ����¼���ҽ��
            }

            #region ������ҩ�Զ����

            if (this.IReasonableMedicine != null)
            {
                this.IReasonableMedicine.PassShowFloatWindow(false);
                return this.PassCheckOrder(alPassOrder, true);
            }
            #endregion

            return 0;
        }

        /// <summary>
        /// ��鿪����Ϣ����ʾ����
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
        /// ��ѯҽ��
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
                MessageBox.Show("���ҽ����¼��Ϣ����", "��ʾ");
            }
            if (this.myPatientInfo == null)
            {
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯҽ��,���Ժ�!");
            Application.DoEvents();

            //��ѯ����ҽ������
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

                if (orderObj.MOTime.AddDays(allOrderShowDays).Date > dateNow.Date //7��������ҽ��
                    || ("0,1,2,5".Contains(orderObj.Status.ToString())
                    && orderObj.OrderType.IsDecompose)//��Ч����
                    || ((orderObj.MOTime.AddDays(this.shortOrderShowDays).Date > dateNow.Date)
                    && !orderObj.OrderType.IsDecompose)//������������Ч����
                    )
                {
                    alOrder.Add(orderObj);
                }
            }

            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾҽ��,���Ժ�!");
            Application.DoEvents();
            if (this.IsDesignMode)
            {
                //tooltip.SetToolTip(this.fpOrder, "����ʱ����ҽ��ֻ��ʾ��Ч�ģ���ʱҽ��ֻ��ʾ24Сʱ�ڵ�ҽ����");
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
                    //CheckSortID();//���˳���

                    dvLong.RowFilter = "����ʱ�� >'" + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddDays(1 - allOrderShowDays).Date.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " or ҽ��״̬ in('0','1','2','5')";//��Ч����
                    dvShort.RowFilter = "����ʱ�� >'" + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddDays(1 - allOrderShowDays).Date.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                        + " or (ҽ��״̬ in('0','1','2','5') and ����ʱ��>'" + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddDays(1 - this.shortOrderShowDays).Date.ToString("yyyy-MM-dd HH:mm:ss") + "')";//������������Ч����

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

            //�����ʾ��Ϣ
            this.txtItemInfo.Text = string.Empty;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ��ѯҽ��
        /// </summary>
        private void QueryOrderByTime(EnumFilterList State)
        {
            this.fpOrder.Sheets[0].RowCount = 0;
            this.fpOrder.Sheets[1].RowCount = 0;

            ArrayList alOrder = new ArrayList();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯҽ��,���Ժ�!");

            //��ѯʱ����ܹ���
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
                MessageBox.Show("��ѯҽ������\r\n" + CacheManager.InOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (MessageBox.Show("��ǰ����δ�����ҽ����ȷ���˳���", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ����ҽ����ʾ
        /// 0 All,1���� 2����Ч��3 ��Ч��4 δ���
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

                    //��ѯʱ����ܹ���
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
                            dvLong.RowFilter = "����ʱ�� >='" + dt1.ToString() + "' and ����ʱ��<='" + dt2.ToString() + "'";
                        }
                        else
                        {
                            dvShort.RowFilter = "����ʱ�� >='" + dt1.ToString() + "' and ����ʱ��<='" + dt2.ToString() + "'";
                        }
                    }

                    else if (State == EnumFilterList.Valid)
                    {
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "ҽ��״̬ ='1' or ҽ��״̬='2'";
                        }
                        else
                        {
                            dvShort.RowFilter = "ҽ��״̬ ='1' or ҽ��״̬='2'";
                        }
                    }
                    else if (State == EnumFilterList.Invalid)
                    {
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "ҽ��״̬ in ( '3','4')";
                        }
                        else
                        {
                            dvShort.RowFilter = "ҽ��״̬ in ( '3','4')";
                        }
                    }
                    else if (State == EnumFilterList.New)
                    {
                        if (this.fpOrder.ActiveSheetIndex == 0)
                        {
                            dvLong.RowFilter = "ҽ��״̬ in ( '0','5')";
                        }
                        else
                        {
                            dvShort.RowFilter = "ҽ��״̬ in ( '0','5')";
                        }
                    }
                    else if (State == EnumFilterList.UC_ULOrder)
                    {
                        this.fpOrder.ActiveSheetIndex = 1;
                        dvShort.RowFilter = "ϵͳ��� in ( '���','����')";
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

        #region MQ��Ϣ����

        /// <summary>
        /// ��ʱ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        protected override void OnSendMessage(object sender, string msg)
        {
            msg = "ҽ���仯���뼰ʱ����\n\n���ߣ�" + this.myPatientInfo.Name + "\nסԺ�ţ�" + this.myPatientInfo.PID.PatientNO + "\n���ţ�" + this.myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);

            FS.FrameWork.Models.NeuObject targetDept = new FS.FrameWork.Models.NeuObject();
            targetDept.ID = this.myPatientInfo.PVisit.PatientLocation.NurseCell.ID;
            targetDept.Name = this.myPatientInfo.PVisit.PatientLocation.NurseCell.Name;


            base.OnSendMessage(targetDept, msg);
        }

        /// <summary>
        /// ������Ϣ��� 
        /// </summary>
        private enum SendType
        {
            /// <summary>
            /// ���
            /// </summary>
            Add,

            /// <summary>
            /// ɾ��
            /// </summary>
            Delete,

            /// <summary>
            /// ȡ������
            /// </summary>
            RollBackCancel
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="type"></param>
        private void SendMessage(SendType type)
        {
            string tip = "";
            switch (type)
            {
                case SendType.Add:
                    tip = "����";
                    break;
                case SendType.Delete:
                    tip = "���ϻ�ɾ��";
                    break;
                case SendType.RollBackCancel:
                    tip = "ȡ������";
                    break;
            }

            if (this.myPatientInfo != null && this.IsDesignMode)
            {
                string msg = "�С�" + tip + "��ҽ�����뼰ʱ����\n\n����:" + myPatientInfo.PVisit.PatientLocation.Dept.Name + "\n���ߣ���" + this.myPatientInfo.Name + "��\nסԺ�ţ�" + this.myPatientInfo.PID.PatientNO + "\n���ţ�" + this.myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);

                FS.FrameWork.Models.NeuObject targetDept = new FS.FrameWork.Models.NeuObject();
                targetDept.ID = this.myPatientInfo.PVisit.PatientLocation.NurseCell.ID;
                targetDept.Name = this.myPatientInfo.PVisit.PatientLocation.NurseCell.Name;

                base.OnSendMessage(targetDept, msg);
            }
        }
        #endregion

        #region У��ҽ������ �Ƿ�֪��ͬ�⡢���顢У����

        /// <summary>
        /// У��ҽ������ �Ƿ�֪��ͬ�⡢���顢У����
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

            //�Ƿ����������״̬�¿���ҩƷ
            int iCheck = Classes.Function.GetIsOrderCanNoStock();
            try
            {
                FS.HISFC.Models.Base.Item tempItem = (order.Item) as FS.HISFC.Models.Base.Item;
                FS.HISFC.Models.Pharmacy.Item tempPharmacy = order.Item as FS.HISFC.Models.Pharmacy.Item;
                int iFlag = -1;
                if (tempItem == null)
                {
                    //MessageBox.Show("ҽ����ϸ��Ŀ����ת������");
                    errInfo = "ҽ����ϸ��Ŀ����ת������!";
                    return -1;
                }
                //�����
                //if (order.Item.IsPharmacy && order.OrderType.IsCharge)
                if (order.Item.ItemType == EnumItemType.Drug && order.OrderType.IsCharge)
                {
                    if (Classes.Function.CheckPharmercyItemStock(iCheck, order.Item.ID, order.Item.Name, this.GetReciptDept().ID, order.Qty, "I") == false)
                    {
                        //MessageBox.Show(order.Item.Name + "��治��!");
                        errInfo = "ҩƷ[" + order.Item.Name + "]Ŀǰ��治��ʹ�ã�";
                        return -1;
                    }
                }
                //�ж�ҽ��֪��ͬ��  ֻ���շѵ�ҽ�����ͽ���֪��ͬ���ж�
                if (order.OrderType.IsCharge)
                {
                    //iFlag = Classes.Function.IsCanOrder(patient, tempItem);
                }
                if (iFlag == 0) return -1;

            }
            catch (Exception ex)
            {
                //MessageBox.Show("��Ŀת������" + ex.Message, "��ʾ");
                errInfo = "��Ŀת������" + ex.Message;
            }
            return 1;
        }
        #endregion

        #region add by xuewj ����ҽ������ {1F2B9330-7A32-4da4-8D60-3A4568A2D1D8}

        /// <summary>
        /// ����ҽ������
        /// </summary>
        public void AddAssayCure()
        {
            if (this.fpOrder.ActiveSheetIndex == 1 && this.fpOrder.ActiveSheet.ActiveRowIndex > -1)
            {
                List<FS.HISFC.Models.Order.Order> alOrder = this.GetSelectedOrders();
                if (alOrder == null || alOrder.Count == 0)
                {
                    MessageBox.Show("����Ҫѡ����ʱҽ���¿�����ҩƷ!");
                    return;
                }
                ucAssayCure uc = new ucAssayCure();
                uc.Orders = new ArrayList(alOrder);
                uc.MakeSuccessed += new ucAssayCure.MakeSuccessedHandler(uc_MakeSuccessed);
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����ҽ������";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            }
        }

        /// <summary>
        /// ȡ��ʱҽ��ҳ�е�ҽ����Ŀ,�������ƴ���
        /// </summary>
        /// <returns>null��0ʧ��</returns>
        private List<FS.HISFC.Models.Order.Order> GetSelectedOrders()
        {
            List<FS.HISFC.Models.Order.Order> alOrders = new List<FS.HISFC.Models.Order.Order>();
            if (this.fpOrder.ActiveSheetIndex == 0)//����
            {
                return alOrders;
            }

            FS.HISFC.Models.Order.Inpatient.Order tempOrder = null;
            for (int i = this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].RowCount - 1; i > -1; i--)//ֻ������ʱҽ��,����ǳ���,�����ȸ��Ƶ�����
            {
                if (this.fpOrder.Sheets[this.fpOrder.ActiveSheetIndex].IsSelected(i, 0))
                {
                    tempOrder = this.GetObjectFromFarPoint(i, 1);//��ʱҽ��
                    if (tempOrder != null)
                    {
                        if ((tempOrder.Status == 0 || tempOrder.Status == 5)
                            && tempOrder.Item.ItemType == EnumItemType.Drug)//�¿�����ҩƷҽ��
                        {
                            alOrders.Add(tempOrder);
                        }
                    }
                }
            }

            return alOrders;
        }

        /// <summary>
        /// ���ɻ���ҽ��
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
                //item.PriceUnit = "��";
                //item.ID = "999";//�Զ���
                //item.SysClass.ID = "M";
                //item.Name = orderName + "ҽ��";
                //this.ucItemSelect1.FeeItem = item;
                this.AddNewOrder(orderInfo.Clone(), this.fpOrder.ActiveSheetIndex);
            }
            this.needUpdateDTBegin = true;
            this.RefreshCombo();
        }

        #endregion

        /// <summary>
        /// ֹͣСʱ�շ�ҽ�� jin
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
                        if (MessageBox.Show("ȷ��ִ��ҽ�������Ƿ������\n" + order.Item.Name + " : " + CacheManager.OrderIntegrate.Err, "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
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
                        if (MessageBox.Show("ȷ��ִ��ҽ�������Ƿ������\n" + order.Item.Name + " : " + CacheManager.OrderIntegrate.Err, "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                        {
                            return iReturn;
                        }
                    }
                }
            }
            return 1;
        }

        #region ���׿���

        //{A5409134-55B5-42d9-A264-25060169A64B}
        private FS.FrameWork.Public.ObjectHelper frequencyHelper = null;

        /// <summary>
        /// �������ҽ������
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
        /// ���׿���
        /// </summary>
        /// <param name="alOrder"></param>
        public void AddGroupOrder(ArrayList alOrders)
        {
            ArrayList alHerbal = new ArrayList(); //��ҩ

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
                        if (order.Combo.ID != "" && order.Combo.ID != comboID)//�µ�
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

                        if (order.Item.SysClass.ID.ToString() == "PCC") //��ҩ
                        {
                            if (this.fpOrder.ActiveSheetIndex == 0)
                            {
                                MessageBox.Show("��Ŀ[" + myorder.Item.Name + "]���Ϊ" + myorder.Item.SysClass.ToString() + "�������Կ���Ϊ����ҽ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                        #region �������׸��Ƴ�����

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
                                            MessageBox.Show("����ҽ��ת��Ϊ��ʱҽ������" + ex.Message);
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
                                        #region ��ʱ���׸��Ƴɳ���

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

                                            //�ж��Ƿ���Ը���
                                            bool b = false;
                                            string strSysClass = myorder.Item.SysClass.ID.ToString();
                                            myorder.HerbalQty = order.HerbalQty;// {3BEDF8C4-0BC6-494f-B7B9-E94C63399197}
                                            //��ʱҽ������Ϊ����������Ϊ0
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
                                                case "UO"://����
                                                case "PCC"://�в�ҩ
                                                case "MC"://����
                                                case "MRB"://ת��
                                                case "MRD": //ת��
                                                case "MRH": //ԤԼ��Ժ

                                                    b = false;
                                                    break;
                                                case "UL": //����
                                                case "UC"://���
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
                                                MessageBox.Show("��Ŀ[" + myorder.Item.Name + "]���Ϊ" + myorder.Item.SysClass.ToString() + "�������Կ���Ϊ����ҽ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                            MessageBox.Show("��ʱҽ��ת��Ϊ����ҽ������" + ex.Message);
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
                Classes.Function.ShowBalloonTip(3, "��ʾ", "��ע����ִ�п����Ƿ���ȷ��", ToolTipIcon.Info);
                this.RefreshCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion

        #region �˵�
        /// <summary>
        /// ȥ��ǰ��ҽ����TempID
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
        /// ��ǰѡ����
        /// </summary>
        int ActiveRowIndex = -1;

        /// <summary>
        /// Ϊ�Ҽ���Ӳ˵�
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
                    if (this.bIsDesignMode) //������
                    {
                        if (this.EditGroup == false)//������ģʽ
                        {
                            #region ճ��ҽ��
                            ToolStripMenuItem mnuPasteOrder = new ToolStripMenuItem("ճ��ҽ��");
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

                #region ��¼��ѡ����

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

                if (this.bIsDesignMode) //������
                {
                    if (this.EditGroup == false)//������ģʽ
                    {
                        #region ֹͣ�˵�
                        ToolStripMenuItem mnuDel = new ToolStripMenuItem();//ֹͣ
                        mnuDel.Click += new EventHandler(mnuDel_Click);
                        //ToolStripMenuItem mnuCancel = new ToolStripMenuItem();//ȡ��
                        //mnuCancel.Click += new EventHandler(mnuCancel_Click);
                        ToolStripMenuItem mnuBack = new ToolStripMenuItem();//ȡ��
                        mnuBack.Click += new EventHandler(mnuBack_Click); ;

                        if (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 5)
                        {
                            if (!CheckOrderCanMove(mnuSelectedOrder))
                            {
                                if (mnuSelectedOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                                {
                                    mnuBack.Text = "ֹͣҽ��[" + mnuSelectedOrder.Item.Name + "]";
                                }
                                else
                                {
                                    mnuBack.Text = "����ҽ��[" + mnuSelectedOrder.Item.Name + "]";
                                }
                                this.contextMenu1.Items.Add(mnuBack);//ȡ��
                            }
                            else
                            {
                                //����
                                mnuDel.Text = "ɾ��ҽ��[" + mnuSelectedOrder.Item.Name + "]";
                                this.contextMenu1.Items.Add(mnuDel);//ɾ��������
                            }

                            //mnuBack.Text = "ȡ��ҽ��[" + mnuSelectedOrder.Item.Name + "]";
                            //this.contextMenu1.Items.Add(mnuBack);//ȡ��
                        }
                        else
                        {
                            if (mnuSelectedOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                            {
                                if (mnuSelectedOrder.Status != 3)
                                {
                                    mnuDel.Text = "ֹͣҽ��[" + mnuSelectedOrder.Item.Name + "]";
                                    this.contextMenu1.Items.Add(mnuDel);//ɾ��������

                                    //mnuCancel.Text = "ȡ��ҽ��[" + mnuSelectedOrder.Item.Name + "]";
                                    //this.contextMenu1.Items.Add(mnuCancel);//ȡ��
                                }
                            }
                            else
                            {
                                //Edit By liangjz  ��������ִ�еĿ�������ҽ��
                                if (mnuSelectedOrder.Status == 1 || mnuSelectedOrder.Status == 2)
                                {
                                    mnuDel.Text = "����ҽ��[" + mnuSelectedOrder.Item.Name + "]";
                                    this.contextMenu1.Items.Add(mnuDel);//ɾ��������
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

                        #region ȡ��ֹͣ

                        if (mnuSelectedOrder.Status == 3 || mnuSelectedOrder.EndTime > new DateTime(2000, 1, 1))
                        {
                            ToolStripMenuItem mnuRollBack = new ToolStripMenuItem();//ȡ��ֹͣ

                            if (mnuSelectedOrder.OrderType.IsDecompose)
                            {
                                mnuRollBack.Text = mnuSelectedOrder.Status == 3 ? "ȡ��ֹͣҽ��" : "ȡ��Ԥֹͣҽ��";
                                mnuRollBack.Click += new EventHandler(mnuRollBack_Click);
                                this.contextMenu1.Items.Add(mnuRollBack);
                            }
                            else
                            {
                                mnuRollBack.Text = mnuSelectedOrder.Status == 3 ? "ȡ������ҽ��" : "ȡ��Ԥ����ҽ��";
                                mnuRollBack.Click += new EventHandler(mnuRollBack_Click);
                                this.contextMenu1.Items.Add(mnuRollBack);
                            }
                        }

                        #endregion

                        #region ҽ�������޸�
                        if (mnuSelectedOrder.Status == 0)
                        {
                            ToolStripMenuItem menuChange = new ToolStripMenuItem();
                            menuChange.Click += new EventHandler(menuChange_Click);
                            menuChange.Text = "�޸�" + "[" + mnuSelectedOrder.Item.Name + "]ҽ������";
                            //if (mnuSelectedOrder.Item.Price == 0)
                            //    menuChange.Enabled = false;
                            //else
                            //    menuChange.Enabled = true;
                            this.contextMenu1.Items.Add(menuChange);
                        }
                        #endregion

                        #region ҽ���������޸�
                        //���������ǰ
                        if (mnuSelectedOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG && mnuSelectedOrder.Status == 0)
                        {
                            ToolStripMenuItem menuFirstDayChange = new ToolStripMenuItem();
                            menuFirstDayChange.Click += new EventHandler(menuFirstDayChange_Click);
                            menuFirstDayChange.Text = "�޸�" + "[" + mnuSelectedOrder.Item.Name + "]������";
                            this.contextMenu1.Items.Add(menuFirstDayChange);
                        }
                        #endregion

                        #region ҽ������¼�ҽ������ҽ��
                        if (mnuSelectedOrder.Status == 5)
                        {
                            //��ȡ��
                            //ToolStripMenuItem menuCheckOrder = new ToolStripMenuItem();
                            //menuCheckOrder.Click += new EventHandler(menuCheckOrder_Click);
                            //menuCheckOrder.Text = "���ҽ��";

                            //this.contextMenu1.Items.Add(menuCheckOrder);
                        }
                        #endregion

                        if (mnuSelectedOrder.Status != 3)
                        {
                            ToolStripMenuItem mnuPacsPrint = new ToolStripMenuItem();//���鴦����ӡ 
                            mnuPacsPrint.Click += new EventHandler(mnuPacsPrint_Click);
                            mnuPacsPrint.Text = "������뵥��ӡ";
                            this.contextMenu1.Items.Add(mnuPacsPrint);
                        }

                        #region ���鴦����ӡ //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}

                        ToolStripMenuItem mnuOrderST = new ToolStripMenuItem();//���鴦����ӡ 
                        mnuOrderST.Click += new EventHandler(mnuOrderST_Click);
                        mnuOrderST.Text = "������ӡ";
                        this.contextMenu1.Items.Add(mnuOrderST);

                        #endregion

                        #region �ص�Ʒ��ȫ��;���������ӡ

                        ToolStripMenuItem mnuOrderST1 = new ToolStripMenuItem();//�ص�Ʒ��ȫ�鴦����ӡ 
                        mnuOrderST1.Click += new EventHandler(mnuOrderST1_Click);
                        mnuOrderST1.Text = "�ص�Ʒ��ȫ�鴦����ӡ";
                        this.contextMenu1.Items.Add(mnuOrderST1);

                        ToolStripMenuItem mnuOrderST2 = new ToolStripMenuItem();//�ص�Ʒ�־���������ӡ 
                        mnuOrderST2.Click += new EventHandler(mnuOrderST2_Click);
                        mnuOrderST2.Text = "�ص�Ʒ�־���������ӡ";
                        this.contextMenu1.Items.Add(mnuOrderST2);

                        #endregion
                    }

                    #region ����ҽ��

                    ToolStripMenuItem mnuCopy = new ToolStripMenuItem();//����ҽ��Ϊ��һ������
                    mnuCopy.Click += new EventHandler(mnuCopyAsOtherType_Click);
                    if (this.OrderType == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        mnuCopy.Text = "����ѡ��ҽ��Ϊͬ��������";
                    }
                    else
                    {
                        mnuCopy.Text = "����ѡ��ҽ��Ϊͬ���ͳ���";
                    }

                    this.contextMenu1.Items.Add(mnuCopy);

                    ToolStripMenuItem mnuCopyAs = new ToolStripMenuItem();//����ҽ��Ϊ������
                    mnuCopyAs.Click += new EventHandler(mnuCopyAsSameType_Click);
                    if (this.OrderType == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        mnuCopyAs.Text = "����ѡ��ҽ��Ϊͬ���ͳ���";
                    }
                    else
                    {
                        mnuCopyAs.Text = "����ѡ��ҽ��Ϊͬ��������";
                    }
                    this.contextMenu1.Items.Add(mnuCopyAs);
                    #endregion

                    #region ����
                    ToolStripMenuItem mnuUp = new ToolStripMenuItem("���ƶ�");//���ƶ�
                    mnuUp.Click += new EventHandler(mnuUp_Click);
                    if (this.fpOrder.ActiveSheet.ActiveRowIndex <= 0) mnuUp.Enabled = false;
                    this.contextMenu1.Items.Add(mnuUp);
                    #endregion

                    #region ����
                    ToolStripMenuItem mnuDown = new ToolStripMenuItem("���ƶ�");//���ƶ�
                    mnuDown.Click += new EventHandler(mnuDown_Click);
                    if (this.fpOrder.ActiveSheet.ActiveRowIndex >= this.fpOrder.ActiveSheet.RowCount - 1 || this.fpOrder.ActiveSheet.ActiveRowIndex < 0) mnuDown.Enabled = false;
                    this.contextMenu1.Items.Add(mnuDown);
                    #endregion

                    #region ������

                    ToolStripMenuItem mnuSaveGroup = new ToolStripMenuItem("������");//������

                    mnuSaveGroup.Click += new EventHandler(mnuSaveGroup_Click);

                    this.contextMenu1.Items.Add(mnuSaveGroup);

                    #endregion

                    #region ��Ӻ�����ҩ�Ҽ��˵�

                    if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
                    {
                        int i = 0;
                        ToolStripMenuItem menuPass = new ToolStripMenuItem("������ҩ");

                        ArrayList alMenu = new ArrayList();

                        if (IReasonableMedicine.PassShowOtherInfo(null, null, ref alMenu) == -1)
                        {
                            MessageBox.Show(IReasonableMedicine.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    #region �޸���չ��Ϣ
                    //{EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                    if (this.Patient != null
                        && (this.Patient.Pact.PayKind.ID == "02"
                            || CacheManager.ConManager.GetConstant("PactDllName", this.Patient.Pact.PactDllName) != null)//
                        && indicationsHelper.GetObjectFromID(GetItemUserCode(mnuSelectedOrder.Item)) != null)
                    {
                        ToolStripMenuItem mnuEditIndications = new ToolStripMenuItem("�޸�ҽ����������ҩ��Ϣ");//�޸�ҽ����������ҩ��Ϣ

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
                        #region �ǿ��������²˵���ʾ
                        ToolStripMenuItem mnuTip = new ToolStripMenuItem("��ע");//��ע
                        mnuTip.Click += new EventHandler(mnuTip_Click);
                        this.contextMenu1.Items.Add(mnuTip);

                        ToolStripMenuItem mnuTot = new ToolStripMenuItem("�ۼ�������ѯ");//�ۼ�����
                        mnuTot.Visible = false;//��ʱ�Ȳ���
                        mnuTot.Click += new EventHandler(mnuTot_Click);

                        try
                        {
                            string OrderID = this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["ҽ����ˮ��"]].Text;

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
        /// �����Ŀ�Զ�����
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
        /// �޸�ҽ����������ҩ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuEditIndications_Click(object sender, EventArgs e)
        {
            int i = this.fpOrder.ActiveSheet.ActiveRowIndex;
            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("����ѡ��һ��ҽ����", "��ʾ");
                return;
            }
            FS.HISFC.Models.Order.Inpatient.Order order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[i].Tag;

            if (order != null)
            {
                if (string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("���ȱ���ҽ����", "��ʾ");
                    return;
                }
                FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtObj = orderExtMgr.QueryByInpatineNoOrderID(myPatientInfo.ID, order.ID);
                FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(this.GetItemUserCode(order.Item));
                if (indicationsObj == null)
                {
                    MessageBox.Show("��ҩƷ��������ҩ���޷�ѡ��", "��ʾ");
                    return;
                }
                if (MessageBox.Show("ҩƷ��" + order.Item.Name + "���������Ƽ�ҩƷ��\r\n\r\n����ҩƷ˵������" + indicationsObj.Name + "��\r\n\r\n��ȷ��ҽ�������趨������(��)���Է�(��)?\r\n", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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
                    MessageBox.Show("����ҽ����չ��Ϣ����\r\n" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (rev == 0)
                {
                    rev = orderExtMgr.InsertOrderExtend(orderExtObj);
                    if (rev == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ҽ����չ��Ϣ����\r\n" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
        }

        /// <summary>
        /// ȡ��ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //[Obsolete("���ϣ�ͳһʹ��delete", true)]
        void mnuBack_Click(object sender, EventArgs e)
        {
            // TODO:  ��� ucOrder.Del ʵ��
            int i = this.fpOrder.ActiveSheet.ActiveRowIndex;
            DialogResult r;
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (i < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("����ѡ��һ��ҽ����");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.Rows[i].Tag;

            FS.HISFC.Models.Order.Inpatient.Order tmpOrder = CacheManager.InOrderMgr.QueryOneOrder(order.ID);

            if (tmpOrder == null || tmpOrder.PageNo < 0 || tmpOrder.RowNo < 0)
            {
                MessageBox.Show("��ҽ����δ��ӡ����ֱ��ɾ����");
                return;
            }
            //��˹������ϻ�ֹͣ
            string strTip = "";
            //������˹���ִ�й��Ķ�����ֹͣ
            if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
            {
                strTip = "ȡ��";
            }
            else
            {
                strTip = "ȡ��";
            }
            r = MessageBox.Show("�Ƿ�" + strTip + "��ҽ��[" + order.Item.Name + "]?\n *�˲������ܳ�����", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (r == DialogResult.OK)
            {
                //����ֹͣ����

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


                        #region ֹͣҽ��
                        if (CacheManager.InOrderMgr.DcOneOrder(orderTemp) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(CacheManager.InOrderMgr.Err);
                            return;
                        }

                        #endregion


                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["ҽ��״̬"]].Value = orderTemp.Status;
                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["ֹͣʱ��"]].Value = orderTemp.DCOper.OperTime;
                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["ֹͣʱ��"]].Value = orderTemp.EndTime;
                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["ֹͣ�˱���"]].Text = orderTemp.DCOper.ID;
                        this.fpOrder.ActiveSheet.Cells[j, dicColmSet["ֹͣ��"]].Text = orderTemp.DCOper.Name;
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
                //					MessageBox.Show("ҽ���Ѿ�״̬�Ѿ��仯����ˢ����Ļ��");
                return;
            }
            this.RefreshOrderState(true);

            return;
        }
        /// <summary>
        /// ����Ҽ��˵�����
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
        /// ɾ�������ϡ�ֹͣҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDel_Click(object sender, EventArgs e)
        {
            this.Delete();
        }

        /// <summary>
        /// ȡ��ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete("���ϣ�ͳһʹ��delete", true)]
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            this.Delete();
        }

        /// <summary>
        /// ȡ��ֹͣҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuRollBack_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;

            #region �ж��Ƿ����ȡ��ֹͣ

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
                        MessageBox.Show("ҽ��[" + order.Item.Name + "]ֹͣʱ�䳬��һ�죬������ȡ��ֹͣ��");
                        return;
                    }

                    string rev = CacheManager.InOrderMgr.GetDCConfirmFlag(order.ID);
                    //�鲻��Ĭ��Ϊ�����
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

                    //û��˵Ĳ�����ȡ���˷ѻ����޸�ֹͣʱ��
                    if (isConfiremed)
                    {
                        Msg += "��" + order.Item.Name + "����";
                    }
                }
                else if (order.Status == 4)
                {
                    Msg += "��" + order.Item.Name + "��Ϊ����ҽ����";
                }
            }

            if (!string.IsNullOrEmpty(Msg))
            {
                MessageBox.Show("ҽ��" + Msg + "\n ��ʿ�Ѿ���ˣ�����ȡ��ֹͣ��");
                return;
            }

            #endregion

            Hashtable hsTemp = new Hashtable();
            if (this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show(this, "����ѡ��һ��ҽ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string comboID = "";

            if (MessageBox.Show(this, "�Ƿ�ȡ��ֹͣ��ҽ��[" + order.Item.Name + "]?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
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

                //�����Ŀֻ�ж�һ�����У���Ϊ�����һ�������
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
                        MessageBox.Show(Msg, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ����������״̬������ȡ������!"));
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            this.SendMessage(SendType.RollBackCancel);

            this.RefreshOrderState(-1);

            return;
        }

        /// <summary>
        /// ������뵥��ӡ
        /// {A2ACD07E-03C1-4b5e-B6B1-7F8DE370C256}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuPacsPrint_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.ActiveSheet.ActiveRow.Index < 0)
            {
                MessageBox.Show("��ѡ��һ��ҽ����");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.ActiveRow.Tag;
            {
                if (order == null || string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("��ѡ����Чҽ����");
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
                    this.myPatientInfo.SIMainInfo.User03 = "PACS��ӡ";
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
                        this.myPatientInfo.SIMainInfo.User03 = "PACS��ӡ";
                        this.IInPatientOrderPrint.PrintInPatientOrderBill(this.myPatientInfo, "", reciptDept, reciptDoct, alOrderTemp, false);
                        this.myPatientInfo.SIMainInfo.User03 = "";
                    }

                }
            }
        }

        /// <summary>
        /// ���鷽��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuOrderST_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.ActiveSheet.ActiveRow.Index < 0)
            {
                MessageBox.Show("��ѡ��һ��ҽ����");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.ActiveRow.Tag;
            {
                if (order == null || string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("��ѡ����Чҽ����");
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
                    //��ʱ����дһ�£����濴���Ǽӽӿڻ�����ô����
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
                    MessageBox.Show("��ҽ���Ǿ��鴦����");
                    return;
                }
            }
        }

        /// <summary>
        /// �ص�Ʒ��ȫ�鴦����ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuOrderST1_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.ActiveSheet.ActiveRow.Index < 0)
            {
                MessageBox.Show("��ѡ��һ��ҽ����");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.ActiveRow.Tag;
            {
                if (order == null || string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("��ѡ����Чҽ����");
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
                    MessageBox.Show("��ҽ�����ص�Ʒ��ȫ�鴦����");
                    return;
                }
            }
        }

        /// <summary>
        /// �ص�Ʒ�־���������ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuOrderST2_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            if (this.fpOrder.ActiveSheet.ActiveRow.Index < 0)
            {
                MessageBox.Show("��ѡ��һ��ҽ����");
                return;
            }
            order = (FS.HISFC.Models.Order.Inpatient.Order)this.fpOrder.ActiveSheet.ActiveRow.Tag;
            {
                if (order == null || string.IsNullOrEmpty(order.ID))
                {
                    MessageBox.Show("��ѡ����Чҽ����");
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
                    MessageBox.Show("��ҽ�����ص�Ʒ�־���������");
                    return;
                }
            }
        }

        /// <summary>
        /// ȡ��ֹͣҽ��  ���һ��ֹͣ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="temp"></param>
        /// <param name="isDeleteCombo">�Ƿ�ɾ��һ��</param>
        /// <returns></returns>
        private int CancelStopOrder(FS.HISFC.Models.Order.Inpatient.Order order, bool isDeleteCombo, ref string errInfo)
        {
            ArrayList alTemp = new ArrayList();
            try
            {
                FS.HISFC.Models.Order.Inpatient.Order temp = new FS.HISFC.Models.Order.Inpatient.Order();

                //�˴��������һ��ֹͣ
                if (CacheManager.InOrderMgr.CancelDcOrder(order, isDeleteCombo) == -1)
                {
                    errInfo = CacheManager.InOrderMgr.Err;
                    return -1;
                }

                //���ֹͣ��ʱ��ˢ�������Ŀ��ʾ
                if (isDeleteCombo)
                {
                    for (int row = 0; row < this.fpOrder.ActiveSheet.RowCount; row++)
                    {
                        temp = this.fpOrder.ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                        //����е�������Ŀ��ֻˢ��ֹͣ��Ϣ
                        if (temp.Combo.ID == order.Combo.ID)
                        {
                            temp = CacheManager.InOrderMgr.QueryOneOrder(temp.ID);

                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ҽ��״̬"]].Value = temp.Status;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣʱ��"]].Value = "";
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["����ʱ��"]].Value = "";
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣ�˱���"]].Text = temp.DCOper.ID;
                            this.fpOrder.ActiveSheet.Cells[row, dicColmSet["ֹͣ��"]].Text = temp.DCOper.Name;

                            //�˴�û�а���Ż�ȡ���������¸�ֵ
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
        /// ��ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTip_Click(object sender, EventArgs e)
        {
            ucTip ucTip1 = new ucTip();
            ucTip1.IsCanModifyHypotest = false;
            string OrderID = this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["ҽ����ˮ��"]].Text;
            int iHypotest = CacheManager.InOrderMgr.QueryOrderHypotest(OrderID);
            if (iHypotest == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            try
            {
                ucTip1.Tip = this.fpOrder.ActiveSheet.GetNote(this.ActiveRowIndex, dicColmSet["ҽ������"]).ToString();
            }
            catch { }
            int i = dicColmSet["ҽ��״̬"];
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
        /// ����ҽ��  �ɳ�������Ϊ��������������Ϊ����
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

            #region ��ȡ��Ҫ���Ƶ�ҽ������е�һ��ҩƷ

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

            #region ������ϰ�������

            ArrayList alCopyOrders = null;

            //�ж�ȱҩ��ͣ��
            FS.HISFC.Models.Pharmacy.Item itemObj = null;
            string errInfo = "";
            FS.HISFC.Models.Order.OrderType ordertype = null;

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alCombOrder)
            {
                alCopyOrders = new ArrayList();

                #region ��ȡ��ҽ����Ϻ�
                string ComboNo;
                try
                {
                    ComboNo = CacheManager.InOrderMgr.GetNewOrderComboID();
                    if (ComboNo == null || ComboNo == "")
                    {
                        MessageBox.Show("����ҽ�������з������� ��ȡ��ҽ����ϺŹ����г���" + CacheManager.InOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("����ҽ�������з������� ��ȡ��ҽ����ϺŹ����г���" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region ��ȡ��Ҫ���Ƶ�ҽ��

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
                            MessageBox.Show("��ʿ�����������˿�����ҩƷ!");
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

                        #region ҩƷ����ҩƷ��Ŀ��ֵ

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

                        #region ҽ��������Ϣ��ֵ

                        tempOrder.OrderType.IsDecompose = !tempOrder.OrderType.IsDecompose;//������ʱ����
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

                        #region  add by liuww ����� ��������ҽ��������������
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

                #region ����ҽ��

                foreach (FS.HISFC.Models.Order.Inpatient.Order copyOrder in alCopyOrders)
                {
                    copyOrder.Item.Name.Replace("[����]", "").Replace("[�Ա�]", "");

                    if (this.fpOrder.ActiveSheetIndex == 0)
                    {
                        #region ��������Ϊ����
                        Classes.Function.SetDefaultFrequency(copyOrder);

                        if (copyOrder.Item.ItemType == EnumItemType.Drug)
                        {
                            #region ���Ƶ�ʱ���жϲ������

                            //if (itemObj.SplitType.Equals(itemObj.CDSplitType))
                            //{
                            //    //����ɲ�ֵ�����һ��
                            //}
                            //else
                            //{
                            //    //���������Բ�һ��
                            //}


                            #endregion

                            //�Զ������������� ������С��λ��ʾ 
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
                            this.refreshComboFlag = "1";		//ֻ�������������Ϻ�ˢ�¼���

                            copyOrder.SubCombNO = this.GetMaxCombNo(copyOrder, 1);

                            #region add by liuww  ����Ϊ��ͬ���͵�ҽ��������ȡ�ַ�����
                            Classes.Function.ReComputeQty(copyOrder);
                            #endregion


                            this.AddNewOrder(copyOrder, 1);//short
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("����ҽ�������з�������Ԥ֪����" + ex.Message + ex.Source);
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        //��ʱ
                        #region ��������Ϊ����

                        //�ж��Ƿ���Ը���
                        bool b = false;
                        string strSysClass = copyOrder.Item.SysClass.ID.ToString();

                        #region �����������ܸ���Ϊ����ҽ��

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
                            MessageBox.Show("��Ŀ[" + copyOrder.Item.Name + "]���Ϊ" + copyOrder.Item.SysClass.Name + "���ܸ���Ϊ����ҽ��!");
                            continue;
                        }
                        #endregion

                        this.refreshComboFlag = "0";//ֻ�Գ�����Ͻ���ˢ�¼���

                        copyOrder.SubCombNO = this.GetMaxCombNo(copyOrder, 0);

                        #region add by liuww  ����Ϊ��ͬ���͵�ҽ��������ȡ�ַ�����
                        Classes.Function.ReComputeQty(copyOrder);
                        #endregion

                        this.AddNewOrder(copyOrder, 0);//long

                        #endregion
                    }
                }
                #endregion

                Classes.Function.ShowBalloonTip(3, "��ʾ", "��ע����ִ�п����Ƿ���ȷ��", ToolTipIcon.Info);
            }
            #endregion

            this.RefreshCombo();
        }


        /// <summary>
        /// ����ҽ��
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


            #region ��ȡ��Ҫ���Ƶ�ҽ������е�һ��ҩƷ
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

            #region ������ϰ�������

            ArrayList alCopyOrders = null;

            //�ж�ȱҩ��ͣ��
            FS.HISFC.Models.Pharmacy.Item itemObj = null;
            string errInfo = "";
            FS.HISFC.Models.Order.OrderType ordertype = null;

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alCombOrder)
            {
                alCopyOrders = new ArrayList();

                #region ��ȡ��ҽ����Ϻ�
                try
                {
                    combNo = CacheManager.InOrderMgr.GetNewOrderComboID();
                    if (combNo == null || combNo == "")
                    {
                        MessageBox.Show("����ҽ�������з������� ��ȡ��ҽ����ϺŹ����г���" + CacheManager.InOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("����ҽ�������з������� ��ȡ��ҽ����ϺŹ����г���" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region ��ȡ��Ҫ���Ƶ�ҽ���б�

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
                            MessageBox.Show("��ʿ�����������˿�����ҩƷ!");
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

                        #region ҩƷ����ҩƷ��Ŀ��ֵ

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

                        #region ҽ��������Ϣ��ֵ
                        ordertype = tempOrder.OrderType;

                        //if (tempOrder.Item.Price == 0)
                        //{
                        //    Classes.OrderType.CheckChargeableOrderType(ref ordertype, false);
                        //}
                        //else
                        //{
                        //    Classes.OrderType.CheckChargeableOrderType(ref ordertype, true);
                        //}


                        tempOrder.Item.Name.Replace("[����]", "").Replace("[�Ա�]", "");

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
                        #region add by liuww ����� ��������ҽ��������������
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

                        //�����ҿ�������
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

                #region ����ҽ��

                for (int i = 0; i < alCopyOrders.Count; i++)
                {
                    #region ����Ϊ����ҽ��

                    if (this.fpOrder.ActiveSheetIndex == 0)
                    {
                        try
                        {
                            this.refreshComboFlag = "0";			//ֻ��Գ�����ϺŽ���ˢ�¼���

                            ((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]).SubCombNO = this.GetMaxCombNo(((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]), 0);

                            #region add by liuww  ����Ϊ��ͬ���͵�ҽ��������ȡ�ַ�����
                            Classes.Function.ReComputeQty((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]);
                            #endregion

                            this.AddNewOrder(((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]), 0);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("����ҽ�������з�������Ԥ֪����" + ex.Message + ex.Source);
                            return;
                        }
                    }
                    #endregion

                    #region ����Ϊ��ʱҽ��

                    else
                    {
                        tempOrder = (alCopyOrders[i] as FS.HISFC.Models.Order.Inpatient.Order).Clone();

                        //��ʱҽ������Ϊ����������Ϊ0
                        tempOrder.Qty = 0;
                        tempOrder.HerbalQty = 0;
                        Classes.Function.SetDefaultFrequency(tempOrder);
                        //��ʱ
                        try
                        {
                            this.refreshComboFlag = "1";			//ֻ���������ϺŽ���ˢ�¼���

                            ((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]).SubCombNO = this.GetMaxCombNo(((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]), 1);

                            #region add by liuww  ����Ϊ��ͬ���͵�ҽ��������ȡ�ַ�����
                            Classes.Function.ReComputeQty((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]);
                            #endregion

                            this.AddNewOrder(((FS.HISFC.Models.Order.Inpatient.Order)alCopyOrders[i]), 1);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("����ҽ�������з�������Ԥ֪����" + ex.Message + ex.Source);
                            return;
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
            Classes.Function.ShowBalloonTip(3, "��ʾ", "��ע����ִ�п����Ƿ���ȷ��", ToolTipIcon.Info);

            this.RefreshCombo();
        }

        /// <summary>
        /// ����ҽ��ǰ����֤
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

                if (strZG.Contains("����")
                    && SheetIndex == 0)
                {
                    MessageBox.Show("�������ߣ����ܿ�������ҽ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }

            string error = "";

            int ret = 1;

            //����Ȩ�ж�
            ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, CacheManager.InOrderMgr.Operator,
                (CacheManager.InOrderMgr.Operator as FS.HISFC.Models.Base.Employee).Dept, FS.HISFC.Models.Base.DoctorPrivType.SpecialDrug, false, ref error);

            if (ret == 0)
            {
                MessageBox.Show(error, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //order.Status = 5;
                //return 0;
                return -1;
            }
            else if (ret < 0)
            {
                MessageBox.Show(error, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }



            if (FS.FrameWork.Function.NConvert.ToInt32(order.FirstUseNum) > order.Frequency.Times.Length)
            {
                order.FirstUseNum = order.Frequency.Times.Length.ToString();
                Classes.Function.ShowBalloonTip(2, order.Item.Name + "[" + order.Item.Specs + "] ����������\r\nϵͳ�Զ�����Ϊ" + order.Frequency.Times.Length.ToString() + "!\r\n\r\n��ע��鿴��", "��ʾ��", ToolTipIcon.Info);
            }

            return 1;
        }

        /// <summary>
        /// ����ҽ��
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
                MessageBox.Show("��ǰ�з��¿���ҽ�����������ƶ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckOrderCanMove(upOrder))
            {
                MessageBox.Show("��" + upOrder.Item.Name + "���Ѿ���ӡ���������ƶ���\r\n\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order downOrder = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex).Clone();

            if (!"0,5".Contains(downOrder.Status.ToString()))
            {
                MessageBox.Show("��һ�з��¿���ҽ�����������ƶ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckOrderCanMove(downOrder))
            {
                MessageBox.Show("��" + downOrder.Item.Name + "���Ѿ���ӡ���������ƶ���\r\n\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //������ƶ�
            if (upOrder.Combo.ID == downOrder.Combo.ID)
            {
                upOrder.SortID -= 1;
                AddObjectToFarpoint(upOrder, this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

                downOrder.SortID += 1;
                AddObjectToFarpoint(downOrder, this.fpOrder.ActiveSheet.ActiveRowIndex - 1, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

                this.fpOrder.ActiveSheet.Cells[this.fpOrder.ActiveSheet.ActiveRowIndex - 1, dicColmSet["˳���"]].Tag = "����";
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
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag = "����";
                    }
                }
            }

            RefreshCombo();
            //this.fpOrder.Sheets[0].SortRows(dicColmSet["˳���"], false, true);
            //Classes.Function.DrawCombo(this.fpOrder.ActiveSheet, dicColmSet["��Ϻ�"], dicColmSet["��"]);

            for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
            {
                if (this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag != null
                    && this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag.ToString() == "����")
                {
                    this.fpOrder.ActiveSheet.ActiveRowIndex = i;
                    this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;
                    this.ucItemSelect1.CurrentRow = ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    this.fpOrder.ActiveSheet.AddSelection(ActiveRowIndex, 0, 1, this.fpOrder.ActiveSheet.ColumnCount);
                    this.ucItemSelect1.Order = this.currentOrder;

                    this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag = null;
                    break;
                }
            }

            this.SelectionChanged();
        }

        /// <summary>
        /// ����ҽ��
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
                MessageBox.Show("��ǰ�з��¿���ҽ�����������ƶ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckOrderCanMove(upOrder))
            {
                MessageBox.Show("��" + upOrder.Item.Name + "���Ѿ���ӡ���������ƶ���\r\n\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order downOrder = this.GetObjectFromFarPoint(this.fpOrder.ActiveSheet.ActiveRowIndex + 1, this.fpOrder.ActiveSheetIndex).Clone();

            if (!"0,5".Contains(downOrder.Status.ToString()))
            {
                MessageBox.Show("��һ�з��¿���ҽ�����������ƶ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckOrderCanMove(downOrder))
            {
                MessageBox.Show("��" + downOrder.Item.Name + "���Ѿ���ӡ���������ƶ���\r\n\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            //������ƶ�
            if (upOrder.Combo.ID == downOrder.Combo.ID)
            {
                upOrder.SortID -= 1;
                AddObjectToFarpoint(upOrder, this.fpOrder.ActiveSheet.ActiveRowIndex, this.fpOrder.ActiveSheetIndex, ColmSet.ALL);

                this.fpOrder.ActiveSheet.Cells[this.fpOrder.ActiveSheet.ActiveRowIndex, dicColmSet["˳���"]].Tag = "����";

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
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag = "����";
                    }
                }
            }

            RefreshCombo();

            //this.fpOrder.Sheets[0].SortRows(dicColmSet["˳���"], false, true);
            //Classes.Function.DrawCombo(this.fpOrder.ActiveSheet, dicColmSet["��Ϻ�"], dicColmSet["��"]);

            for (int i = 0; i < this.fpOrder.ActiveSheet.RowCount; i++)
            {
                if (this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag != null
                    && this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag.ToString() == "����")
                {
                    this.fpOrder.ActiveSheet.ActiveRowIndex = i;
                    this.ActiveRowIndex = this.fpOrder.ActiveSheet.ActiveRowIndex;
                    this.ucItemSelect1.CurrentRow = ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(i, this.fpOrder.ActiveSheetIndex);
                    this.fpOrder.ActiveSheet.AddSelection(ActiveRowIndex, 0, 1, this.fpOrder.ActiveSheet.ColumnCount);
                    this.ucItemSelect1.Order = this.currentOrder;

                    this.fpOrder.ActiveSheet.Cells[i, dicColmSet["˳���"]].Tag = null;
                    break;
                }
            }

            this.SelectionChanged();
        }

        /// <summary>
        /// ��ʾ
        /// </summary>
        /// <param name="Tip"></param>
        /// <param name="Hypotest"></param>
        private void ucTip1_OKEvent(string Tip, int Hypotest)
        {
            this.fpOrder.ActiveSheet.SetNote(this.ActiveRowIndex, dicColmSet["ҽ������"], Tip);
            string orderID = this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["ҽ����ˮ��"]].Text;
            if (CacheManager.InOrderMgr.UpdateFeedback(this.myPatientInfo.ID, orderID, Tip, Hypotest) == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                CacheManager.InOrderMgr.Err = "";
            }

        }

        /// <summary>
        /// �ۼ�������ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTot_Click(object sender, EventArgs e)
        {
            string OrderID = this.fpOrder.ActiveSheet.Cells[this.ActiveRowIndex, dicColmSet["ҽ����ˮ��"]].Text;
            FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(OrderID);
            if (order == null) return;
            //Classes.Function.TotalUseDrug(this.GetPatient().ID, order.Item.ID);
        }

        /// <summary>
        /// �޸�ҽ������ 
        /// </summary>
        private void menuChange_Click(object sender, EventArgs e)
        {
            using (ucSimpleChange uc = new ucSimpleChange())
            {
                FS.HISFC.Models.Order.Inpatient.Order order = this.fpOrder.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.Order;

                uc.TitleLabel = "ҽ�������޸�";
                uc.InfoLabel = "��Ŀ����:" + order.Item.Name;
                uc.OperInfo = "ҽ������";

                //��ȡҽ������
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

                if (this.fpOrder.ActiveSheetIndex == 0)		//����
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
                        this.fpOrder.ActiveSheet.Cells[i, this.dicColmSet["ҽ������"]].Text = tempOrderType.Name;

                        inOrder.Item.Name = inOrder.Item.Name.Replace("[����]", "").Replace("[�Ա�]", "");
                        this.fpOrder.ActiveSheet.Cells[i, dicColmSet["ҽ������"]].Text = ShowOrderName(order);

                        int iUp, iDown;
                        iUp = i;
                        iDown = i;
                        while (isUp || isDown)
                        {
                            #region ���ϲ��� �絽��ǰһ�л���ϺŲ�ͬ���ñ�־Ϊfalse
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
                                        this.fpOrder.ActiveSheet.Cells[iUp, this.dicColmSet["ҽ������"]].Text = tempOrderType.Name;

                                        (this.fpOrder.ActiveSheet.Rows[iUp].Tag as FS.HISFC.Models.Order.Inpatient.Order).Item.Name = (this.fpOrder.ActiveSheet.Rows[iUp].Tag as FS.HISFC.Models.Order.Inpatient.Order).Item.Name.Replace("[����]", "").Replace("[�Ա�]", "");
                                        this.fpOrder.ActiveSheet.Cells[iUp, dicColmSet["ҽ������"]].Text = ShowOrderName((this.fpOrder.ActiveSheet.Rows[iUp].Tag as FS.HISFC.Models.Order.Inpatient.Order));
                                    }
                                    else
                                    {
                                        isUp = false;
                                    }
                                }
                            }
                            #endregion

                            #region ���²��� ��������һ�л���ϺŲ�ͬ���ñ�־Ϊfalse
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
                                        this.fpOrder.ActiveSheet.Cells[iDown, this.dicColmSet["ҽ������"]].Text = tempOrderType.Name;

                                        (this.fpOrder.ActiveSheet.Rows[iDown].Tag as FS.HISFC.Models.Order.Inpatient.Order).Item.Name = (this.fpOrder.ActiveSheet.Rows[iDown].Tag as FS.HISFC.Models.Order.Inpatient.Order).Item.Name.Replace("[����]", "").Replace("[�Ա�]", "");
                                        this.fpOrder.ActiveSheet.Cells[iDown, dicColmSet["ҽ������"]].Text = ShowOrderName((this.fpOrder.ActiveSheet.Rows[iDown].Tag as FS.HISFC.Models.Order.Inpatient.Order));
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
        /// �޸�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFirstDayChange_Click(object sender, EventArgs e)
        {
            //��ȡѡ������
            int row = this.fpOrder.ActiveSheet.ActiveRowIndex;
            if (row < 0 || this.fpOrder.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("����ѡ��һ��ҽ����");
                return;
            }
            //��ȡҽ����Ϣ
            FS.HISFC.Models.Order.Inpatient.Order order = this.GetObjectFromFarPoint(row, this.fpOrder.ActiveSheetIndex);
            if (order == null)
            {
                MessageBox.Show("���ҽ��ʵ�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (order.Status != 0)
            {
                MessageBox.Show("��ҽ��״̬�Ѹı䣬��ˢ�����ݲ鿴��");
                return;
            }
            else
            {
                //����ֹͣ����
                Forms.frmFirstDayChange f = new Forms.frmFirstDayChange();
                f.FirstUseNum = order.FirstUseNum;
                f.Frequency = order.Frequency;
                f.ShowDialog();
                if (f.DialogResult != DialogResult.OK) return;
                //���º��������
                order.FirstUseNum = f.FirstUseNum;

                #region �����ȷ�ϡ�ʱ����
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

                this.fpOrder.ActiveSheet.Cells[row, dicColmSet["������"]].Value = order.FirstUseNum;
            }
        }
        /// <summary>
        /// �ϼ�ҽ�����ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCheckOrder_Click(object sender, EventArgs e)
        {
            this.JudgeSpecialOrder();
        }
        /// <summary>
        /// ������
        /// {C6E229AC-A1C4-4725-BBBB-4837E869754E}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSaveGroup_Click(object sender, EventArgs e)
        {
            this.SaveGroup();
        }
        /// <summary>
        /// ����fp����ʱ���ҵ�����fp������
        /// </summary>
        /// <param name="tempId">��ʱ��</param>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        private int getOrderRowIndex(string tempId, int sheetIndex)
        {
            for (int i = 0; i < this.fpOrder.Sheets[sheetIndex].RowCount; i++)
            {
                if (this.fpOrder.ActiveSheet.Cells[i, dicColmSet["������"]].Text == tempId)
                {
                    return i;
                }
            }
            return -1;
        }
        #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} ������ҩ
        /// <summary>
        /// ����fp�ϵ�˳����ҵ�alAllOrder�е�ҽ��
        /// </summary>
        /// <param name="id">fp�ϵ�˳���</param>
        /// <returns>alAllOrder�е�ҽ��</returns>
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
            return this.fpOrder.ActiveSheet.Cells[rowIndex, this.dicColmSet["������"]].Text;
        }

        //FS.HISFC.Models.Pharmacy.Item phaItem = null;

        ///// <summary>
        ///// ��ȡҩƷ�Զ�����
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
        /// �������ѯ{3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}pacs�ӿ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPacsView_Click(object sender, EventArgs e)
        {
            this.QueryPacsReport();
        }
        //{D2BDB9B8-7D50-4a66-8D1C-28EA0420592F}���뵥
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
                MessageBox.Show("����ѡ��һ��ҽ����");
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
                MessageBox.Show("����ѡ��һ��ҽ����");
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
                            MessageBox.Show("������뵥ɾ��ʧ��");
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("ɾ���ɹ�");
                }
                else
                {
                    MessageBox.Show("�����뵥��Ϣ");
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
                        MessageBox.Show("������뵥ɾ��ʧ��");
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("ɾ���ɹ�");
                }
                else
                {
                    MessageBox.Show("�����뵥��Ϣ");
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
        /// ���״洢
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
                //����ģʽ�� ֻ���湴ѡ��ҽ������
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
                    MessageBox.Show("���ҽ������");
                }
                else
                {
                    #region �ж�ҩ���Ƿ���ڸ�ҩƷ
                    if (longorder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Storage storage = CacheManager.PhaIntegrate.GetStockInfoByDrugCode(longorder.StockDept.ID, longorder.Item.ID);
                        if (storage == null || storage.Item.ID == "")
                        {
                            MessageBox.Show("��" + longorder.Item.Name + "���ڱ����Ҷ�ӦסԺϵͳ�ġ�" + longorder.StockDept.Name + "��û�и�ҩƷ�����ܴ�Ϊ����!");
                            return;
                        }
                    }
                    #endregion

                    string s = longorder.Item.Name;
                    string sno = longorder.Combo.ID;
                    //����ҽ������ Ĭ�Ͽ���ʱ��Ϊ ���
                    longorder.BeginTime = new DateTime(longorder.BeginTime.Year, longorder.BeginTime.Month, longorder.BeginTime.Day, 0, 0, 0);
                    al.Add(longorder);
                }
            }

            #region ������ ����������ͬʱ����Ĺ��ܣ�
            //��Ϊ�л�fp�󣬽������ʾѡ�������⣬���Ҳ�����������ѡ,������������ֻ��ѡ��������������

            //#region ����ҽ������

            //for (int i = 0; i < this.fpOrder.Sheets[0].Rows.Count; i++)//����ҽ��
            //{
            //    //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
            //    FS.HISFC.Models.Order.Inpatient.Order longorderTemp = null;

            //    if (!this.IsDesignMode)
            //    {
            //        longorderTemp = this.GetObjectFromFarPoint(i, 0);
            //    }
            //    //����ģʽ�� ֻ���湴ѡ��ҽ������
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
            //        MessageBox.Show("���ҽ������");
            //    }
            //    else
            //    {
            //        string s = longorder.Item.Name;
            //        string sno = longorder.Combo.ID;
            //        //����ҽ������ Ĭ�Ͽ���ʱ��Ϊ ���
            //        longorder.BeginTime = new DateTime(longorder.BeginTime.Year, longorder.BeginTime.Month, longorder.BeginTime.Day, 0, 0, 0);
            //        al.Add(longorder);
            //    }
            //}

            //#endregion

            //#region ��ʱҽ������
            //for (int i = 0; i < this.fpOrder.Sheets[1].Rows.Count; i++)//��ʱҽ��
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
            //        MessageBox.Show("���ҽ������");
            //    }
            //    else
            //    {
            //        string s = shortorder.Item.Name;
            //        string sno = shortorder.Combo.ID;
            //        //����ҽ������ Ĭ�Ͽ���ʱ��Ϊ ���
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
                    //�������ս�����ʾ
                    this.fpOrder.Sheets[0].RowCount = 0;
                    fpOrder.Sheets[1].RowCount = 0;
                }
            }
        }

        /// <summary>
        /// ճ��ҽ��{7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPasteOrder_Click(object sender, EventArgs e)
        {
            this.PasteOrder();
        }

        #endregion

        #region ���仯��Ҫ���⴦��
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
                    //����

                }
                else if (obj.ID == FS.HISFC.Models.Base.EnumSysClass.UC.ToString())
                {
                    //���

                }
                else
                {
                    return;
                }


            }
            catch { }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��黥��
        /// </summary>
        /// <param name="sysClass"></param>
        /// <returns></returns>
        private int CheckMutex(FS.HISFC.Models.Base.SysClassEnumService sysClass)
        {
            //Ŀǰû�л���Ĺ��ܣ�Ϊ���Ż�������
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
            if (sysClass.ID.ToString() == "UO")  //���������ҽ�������λ��⣬by zuowy 2005-10-13
                return 0;
            try
            {
                FS.HISFC.Models.Order.EnumMutex mutex = CacheManager.InOrderMgr.QueryMutex(sysClass.ID.ToString());//��ѯ����

                if (mutex == FS.HISFC.Models.Order.EnumMutex.SysClass)
                {
                    //ϵͳ��𻥳�
                    if (al.Count == 0) return 0;//���ϵͳ����Ƿ����ظ���

                }
                else if (mutex == FS.HISFC.Models.Order.EnumMutex.All)
                {
                    //ҽ��ȫ������
                    if (MessageBox.Show("�������µ�'" + sysClass.Name + "'ҽ�����Ƿ�ֹͣ��ǰ��ȫ��ҽ��?", "����", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
                MessageBox.Show("��û�����Ϣ����" + ex.Message, "��ʾ");
            }
            return 0;
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            //this.myOrderClass.SaveGrid();
            SaveFpStyle();
        }
        #endregion

        #region ������

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����ҽ��", 9, true, false, null);
            toolBarService.AddToolButton("���", "���ҽ��", 9, true, false, null);
            toolBarService.AddToolButton("������", "��������", 9, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��ҽ��", 9, true, false, null);
            toolBarService.AddToolButton("ȡ�����", "ȡ�����ҽ��", 9, true, false, null);
            toolBarService.AddToolButton("��ϸ", "������ϸ", 9, true, true, null);
            toolBarService.AddToolButton("�˳�ҽ������", "�˳�ҽ������", 9, true, false, null);
            return toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "���")
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
                        MessageBox.Show(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return -1;
                    }

                    decimal terminal = 0;

                    terminal = CacheManager.RadtIntegrate.QueryPatientTerminalFeeByInpatientNO(myPatientInfo.ID);

                    this.lbPatient.Text = myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "��"
                        + "  " + myPatientInfo.PVisit.PatientLocation.NurseCell.Name//����{97B3CB23-EE5A-45b0-ADBE-7B524DFC88EC}
                        + "  " + myPatientInfo.PID.PatientNO//סԺ��
                        + "  " + this.myPatientInfo.Name //����
                        + "  " + this.myPatientInfo.Sex.Name //�Ա�
                        + "  " + CacheManager.InOrderMgr.GetAge(this.myPatientInfo.Birthday)//����
                        + "  " + this.myPatientInfo.Pact.Name//��ͬ��λ
                        + "\r\n"
                        + "סԺ���ڣ�" + myPatientInfo.PVisit.InTime.ToString("yyyy.MM.dd") + " - " + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy.MM.dd") + " / " + CacheManager.RadtIntegrate.GetInDays(myPatientInfo.ID).ToString() + "��"//סԺ����
                        + "\r\n"
                        + "�ܷ���: " + myPatientInfo.FT.TotCost.ToString()
                        + "  Ԥ����: " + this.myPatientInfo.FT.PrepayCost.ToString()
                        + "  �Ը���" + (myPatientInfo.FT.OwnCost + myPatientInfo.FT.PayCost).ToString()
                        + "  ������" + myPatientInfo.FT.PubCost.ToString()
                        + "  ���: " + this.myPatientInfo.FT.LeftCost.ToString() + "  δȷ�Ͻ��: " + terminal;



                    FS.FrameWork.Models.NeuObject civilworkerObject = CacheManager.InterMgr.GetConstansObj("civilworker", myPatientInfo.Pact.ID);

                    if (myPatientInfo.Pact.PayKind.ID == "03" || (!object.Equals(neuObject, null) && !string.IsNullOrEmpty(civilworkerObject.ID)))
                    {
                        lbPatient.Text += "\r\n"
                        + "���޶" + myPatientInfo.FT.DayLimitCost.ToString()
                            //+ "  �Է�ҩ��" + myPatientInfo.FT.DrugOwnCost.ToString()
                            //+ "  ҩƷ�����" + myPatientInfo.FT.OvertopCost.ToString()
                        + "  ҩƷ�ۼƣ�" + myPatientInfo.FT.DrugFeeTotCost.ToString();


                        pnPatient.Height = 72;
                    }
                    else
                    {
                        this.pnPatient.Height = 58;
                    }

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���߱�ǩ��Ϣ,���Ժ�!");
                    //���ñ�ǩ���ܵ�his����{0F599816-C860-40e1-856A-EF5ACACBDA26}
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
        /// ֹͣȫ��ҽ��������ת����� by huangchw 2012-10-29
        /// </summary>
        /// <returns></returns>
        public int DcAllLongOrderAndZG()
        {
            if (frmDCOrderAndZG1 == null)
            {
                //{5936B0A0-598F-43a8-BB31-E812EB8D61EE}
                //�ο����漰��dblinkȥEMR��ѯ���߳�Ժ��ϣ�����Ҫ�����ύ
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

            #region ����ת�����
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (CacheManager.InPatientMgr.UpdateZG(this.myPatientInfo.ID, frmDCOrderAndZG1.ZG.ID, frmDCOrderAndZG1.DiagInfo, frmDCOrderAndZG1.HealthCareObject) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("������Ϣʧ�ܣ�" + CacheManager.InPatientMgr.Err);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            #endregion

            //{9BCBF464-EB90-4c07-AD4D-29481A069D3D}
            HISFC.Models.Base.Employee empl2 = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;

            HISFC.Models.Base.Department dept2 = empl2.Dept as HISFC.Models.Base.Department;

            #region ����ҽ���������
            if (!string.IsNullOrEmpty(frmDCOrderAndZG1.HealthCareObject))
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                if (CacheManager.InPatientMgr.UpdateYIBAODAIYU(this.myPatientInfo.ID, frmDCOrderAndZG1.HealthCareObject) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������Ϣʧ�ܣ�" + CacheManager.InPatientMgr.Err);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion


            MessageBox.Show("¼��ת�������ֹͣȫ������ҽ���ɹ�!");
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

                        MessageBox.Show("������Ϣʧ�ܣ�" + CacheManager.InPatientMgr.Err);
                        return -1;
                    }

                }
            }


            return 1;
        }

        /// <summary>
        /// ֹͣȫ��ҽ��
        /// </summary>
        public int DcAllLongOrder(string strDc)
        {
            //Add by houwb 2011-3-11 {46E8908F-4248-4a40-89B1-530CA5796CD4}
            if (MessageBox.Show(this, "ȷ��ֹͣȫ������ҽ����" + "\r\n" + strDc, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
            {
                return -1;
            }

            string strTip = "ֹͣ";
            //����ֹͣ����
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

            MessageBox.Show("ֹͣȫ������ҽ���ɹ�!");
            return 1;
        }

        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        /// <param name="dcDate">ֹͣʱ��</param>
        /// <param name="dcReason">ֹͣԭ��</param>
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
                    MessageBox.Show("��ȡҽ����Ŀʧ��,��" + i.ToString() + "�У�");
                    return -1;
                }

                //�¿����Ĳ�ֹͣ
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
                        MessageBox.Show("[" + order.Item.Name + "]ֹͣʱ�䲻��С�ڿ�ʼʱ��");
                        return -1;
                    }

                    #region Сʱҽ��ֹͣ�Ʒ�
                    if (this.DCHoursOrder(order, FS.FrameWork.Management.PublicTrans.Trans, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("[" + order.Item.Name + "]ֹͣʱ�Ƿ�ʧ�ܣ�"));
                        return -1;
                    }
                    #endregion

                    #region ֹͣҽ��

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

                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["ҽ��״̬"]].Value = order.Status;
                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["ֹͣʱ��"]].Value = order.DCOper.OperTime;
                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["����ʱ��"]].Value = order.EndTime;
                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["ֹͣ�˱���"]].Text = order.DCOper.ID;
                    this.fpOrder.Sheets[0].Cells[i, dicColmSet["ֹͣ��"]].Text = order.DCOper.Name;
                    this.fpOrder.Sheets[0].Rows[i].Tag = order;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (isHaveNewOrder)
            {
                Classes.Function.ShowBalloonTip(2, "��ʾ", "�����¿���ҽ��δֹͣ�����ֶ�����", ToolTipIcon.Warning);
            }

            this.RefreshOrderState(-1);

            this.SendMessage(SendType.Delete);

            //this.QueryOrder(); edit by liuww ��ѿ�����ҽ��ˢ��û��
            return 1;
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[7];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.IPrintOrder);
                t[1] = typeof(FS.HISFC.BizProcess.Interface.ITransferDeptApplyable);
                t[2] = typeof(FS.HISFC.BizProcess.Interface.Common.ILis);
                t[3] = typeof(FS.HISFC.BizProcess.Interface.IAlterOrder);
                t[4] = typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPrint);//������뵥
                t[5] = typeof(FS.HISFC.BizProcess.Interface.Common.IPacs);//pacs{3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}
                t[6] = typeof(FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine);
                return t;
            }
        }
        /// <summary>
        /// ��ӡ
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

        #region ����
        /// <summary>
        /// ����ѡ����Ŀ 
        /// </summary>        
        public void PrintAgain(string type)
        {
            if (this.EditGroup)
            {
                ucItemSelect1.MessageBoxShow("�����ڱ༭���ף���ʱ��֧�ִ�ӡ������");
                return;
            }
            if (this.myPatientInfo == null || string.IsNullOrEmpty(this.myPatientInfo.ID))
            {
                ucItemSelect1.MessageBoxShow("��ѡ���ߣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //��ʼ���ӿ�
            if (object.Equals(IInPatientOrderPrint, null))
            {
                this.InitOrderPrint();
            }


            #region ���ýӿ�ʵ�ִ�ӡ
            if (IInPatientOrderPrint != null && orderList.Count > 0)
            {

                if (IInPatientOrderPrint.PrintInPatientOrderBill(this.myPatientInfo, type, this.GetReciptDept(), this.GetReciptDoc(), orderList, false) != 1)
                {
                    ucItemSelect1.MessageBoxShow(IInPatientOrderPrint.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ��ʾת������
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
                    order.Unit = "��";
                    order.Item.Name = o.Dept.Name + "[ת��]";
                    order.ExeDept = o.Dept.Clone();
                    order.Frequency.ID = "QD";

                    this.AddNewOrder(order, this.fpOrder.ActiveSheetIndex);
                }
            }

        }

        /// <summary>
        /// ��ʼ��ҽ����Ϣ����ӿ�ʵ��
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
        /// �϶�ʱ����Ϊxml��ʽ
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

        #region ҽ������

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <returns></returns>
        public int ReTidyOrder()
        {
            #region {74E478F5-BDDD-4637-9F5A-E251AF9AA72F}
            if (this.myPatientInfo == null)
            {
                MessageBox.Show("����ѡ����!");
                return -1;
            }
            #endregion

            DialogResult rs = MessageBox.Show("ȷ�Ͻ���ҽ������������ҽ����ֹͣ���ؿ���ǰ��Чҽ������������ֹͣҽ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        /// ����ҽ��
        /// </summary>
        /// <param name="orderList"></param>
        /// <returns></returns>
        //internal int ReTidyOrder(List<FS.HISFC.Models.Order.Inpatient.Order> orderList)
        internal int ReTidyOrderAll()
        {
            //{D05BA7C4-3158-48aa-B581-0211E2CAAD4C} 
            #region ��ȡ����ҽ������ʽ
            /*
             * ��ʽһ������ԭҽ�� ��������״̬ҽ��   
             * ��ʽ�����޸�ԭҽ��Ϊ����״̬ ������Чҽ��
             * */

            int retidyType = 2;//Ĭ�Ϸ�ʽ��
            retidyType = CacheManager.ContrlManager.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.MetConstant.Order_RetidyType, true, 2);

            #endregion

            int maxSortID = 3000;

            ArrayList alOrder = CacheManager.InOrderMgr.QueryOrder(this.myPatientInfo.ID);
            if (alOrder == null)
            {
                MessageBox.Show("��ѯҽ����Ϣʧ�ܣ�" + CacheManager.InOrderMgr.Err);
                return -1;
            }
            ArrayList alSubOrder = CacheManager.InOrderMgr.QueryOrderSubtbl(this.myPatientInfo.ID);
            if (alSubOrder == null)
            {
                MessageBox.Show("��ѯҽ��������Ϣʧ�ܣ�" + CacheManager.InOrderMgr.Err);
                return -1;
            }
            alOrder.AddRange(alSubOrder);

            //�������������
            OrderSortIDCompare orderCompare = new OrderSortIDCompare();
            alOrder.Sort(orderCompare);

            //����һ��ҽ����¼ ���γ�ҽ�����
            FS.HISFC.Models.Order.Inpatient.Order order = alOrder[alOrder.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order;
            maxSortID = order.SortID + 10;

            //���г���
            List<FS.HISFC.Models.Order.Inpatient.Order> longOrderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            //��ǰ��Чҽ���б�
            List<FS.HISFC.Models.Order.Inpatient.Order> validOrderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            //ֹͣҽ���б�
            List<FS.HISFC.Models.Order.Inpatient.Order> DcOrderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            //��������ҽ���б�
            List<FS.HISFC.Models.Order.Inpatient.Order> newOrderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();

            #region �ж��Ƿ������������ ���γɴ�����ҽ���б�

            foreach (FS.HISFC.Models.Order.Inpatient.Order obj in alOrder)
            {
                if (obj.OrderType.IsDecompose)
                {
                    //
                    if (obj.Status == 0 && !obj.IsSubtbl)
                    {
                        MessageBox.Show("�����¿���δ��˵�ҽ�������ܽ���ҽ��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }

                    longOrderList.Add(obj);
                    if (obj.Status == 1 || obj.Status == 2)      //ԭ��Чҽ��
                    {
                        validOrderList.Add(obj);
                    }
                    else if (obj.Status == 3)
                    {
                        DcOrderList.Add(obj);
                    }
                }
            }

            //�ж�ֹͣ�ı�����ˣ���Ϊ��������ֹͣҽ����״̬Ϊ����
            ArrayList alUnconfirmOrder = CacheManager.InOrderMgr.QueryIsConfirmOrder(this.myPatientInfo.ID, FS.HISFC.Models.Order.EnumType.LONG, false);
            if (alUnconfirmOrder == null)
            {
                MessageBox.Show("��ѯδ���ҽ������:" + CacheManager.InOrderMgr.Err);
                return -1;
            }
            if (alUnconfirmOrder.Count > 0)
            {
                MessageBox.Show("�����¿���δ��˻���ֹͣδ��˵�ҽ�������ܽ���ҽ��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            #endregion

            #region ��ԭ��Чҽ���γ���ҽ��

            string comboNO = string.Empty;
            string comboNOTemp = string.Empty;

            //���
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

                newOrderTemp.ReTidyInfo = "����ҽ��  ԭҽ����ˮ�ţ�" + newOrderTemp.ID.ToString();

                newOrderTemp.ID = CacheManager.InOrderMgr.GetNewOrderID();

                newOrderList.Add(newOrderTemp);

                //��Ҫ���Σ����������ȡ��ȷ�������
                this.AddNewOrder(newOrderTemp, 0);
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            //{D05BA7C4-3158-48aa-B581-0211E2CAAD4C}
            #region ��ʽ��  ԭ��Чҽ�����Ϊ����״̬ ������ҽ��ͬԭ��Чҽ��
            if (retidyType == 2)
            {
                #region ��ԭ����Чҽ��ȫ��ͣ��

                foreach (FS.HISFC.Models.Order.Inpatient.Order info in validOrderList)
                {
                    info.Status = 3;

                    info.DCOper.ID = CacheManager.InOrderMgr.Operator.ID;
                    info.DCOper.Name = CacheManager.InOrderMgr.Operator.Name;
                    info.DCOper.OperTime = sysTime;

                    info.DcReason.ID = "RT";
                    info.DcReason.Name = "ҽ������";

                    info.EndTime = info.DCOper.OperTime;

                    if (CacheManager.InOrderMgr.DcOneOrder(info) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ֹͣԭ��Чҽ��ʧ��:" + CacheManager.InOrderMgr.Err);
                        return -1;
                    }
                }

                #endregion

                #region ����ҽ����Ϊ����״̬

                foreach (FS.HISFC.Models.Order.Inpatient.Order info in longOrderList)
                {
                    info.Status = 4;                //ҽ������״̬
                    info.Oper.ID = CacheManager.InOrderMgr.Operator.ID;
                    info.Oper.Name = CacheManager.InOrderMgr.Operator.Name;
                    info.Oper.OperTime = sysTime;

                    if (CacheManager.InOrderMgr.OrderReform(info.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ԭҽ��ʧ��:" + CacheManager.InOrderMgr.Err);
                        return -1;
                    }
                }

                #endregion

                foreach (FS.HISFC.Models.Order.Inpatient.Order info in newOrderList)
                {
                    //��ҽ����ʼʱ���״̬
                    info.Status = 0;
                    info.BeginTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                    info.MOTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

                    //��������
                    info.ReciptDoctor.ID = this.GetReciptDoc().ID;
                    info.ReciptDoctor.Name = this.GetReciptDoc().Name;
                    info.DoctorDept.ID = this.GetReciptDept().ID;
                    info.DoctorDept.Name = this.GetReciptDept().Name;

                    //��Ϊδ��ӡ
                    info.GetFlag = "0";
                    info.PageNo = -1;
                    info.RowNo = -1;
                    info.FirstUseNum = "0";

                    info.Patient.PVisit.PatientLocation.Bed.ID = this.myPatientInfo.PVisit.PatientLocation.Bed.ID;//����ʱ�Ĵ���
                }
            }
            #endregion

            #region ��ʽһ  ԭ��Чҽ����Ϣ���� ��������״̬��ҽ��(��Ϣͬԭ��Чҽ����״̬��ͬ)
            else
            {
                //ֹͣҽ����Ϊ����״̬ {A3B78606-5301-4680-9CF4-08B6545D6608} 20100528
                foreach (FS.HISFC.Models.Order.Inpatient.Order info in DcOrderList)
                {
                    info.Status = 4;                //ҽ������״̬
                    info.Oper.ID = CacheManager.InOrderMgr.Operator.ID;
                    info.Oper.Name = CacheManager.InOrderMgr.Operator.Name;
                    info.Oper.OperTime = sysTime;

                    if (CacheManager.InOrderMgr.OrderReform(info.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ԭҽ��ʧ��:" + CacheManager.InOrderMgr.Err);
                        return -1;
                    }
                }

                foreach (FS.HISFC.Models.Order.Inpatient.Order info in newOrderList)
                {
                    info.Status = 4;               //ҽ������״̬
                    info.Oper.ID = CacheManager.InOrderMgr.Operator.ID;
                    info.Oper.Name = CacheManager.InOrderMgr.Operator.Name;
                    info.Oper.OperTime = sysTime;
                }
            }

            #endregion

            #region ����ҽ�����б���

            foreach (FS.HISFC.Models.Order.Inpatient.Order info in newOrderList)
            {
                if (CacheManager.InOrderMgr.InsertOrder(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������ҽ��ʧ��:" + CacheManager.InOrderMgr.Err);
                    return -1;
                }
            }

            #endregion

            #region ����������¼

            FS.FrameWork.Management.ExtendParam extendManager = new FS.FrameWork.Management.ExtendParam();
            FS.HISFC.Models.Base.ExtendInfo extendInfo = new ExtendInfo();

            extendInfo.ExtendClass = EnumExtendClass.PATIENT;
            extendInfo.Item.ID = this.myPatientInfo.ID;
            extendInfo.PropertyCode = sysTime.ToString();
            extendInfo.PropertyName = "����ʱ��";
            extendInfo.DateProperty = sysTime;

            extendInfo.StringProperty = CacheManager.InOrderMgr.Operator.ID;
            extendInfo.DateProperty = sysTime;

            if (extendManager.InsertComExtInfo(extendInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ҽ�����������¼ʧ��:" + extendManager.Err);
                return -1;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("ҽ�������ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return 1;
        }

        #endregion

        #region ҽ������ӡ

        /// <summary>
        /// ҽ������ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintOrder IPrintOrderInstance = null;

        /// <summary>
        /// ҽ������ӡ���ӿ�ģʽ��
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
                MessageBox.Show("ҽ������ӡ�ӿ�δʵ�֣�");
                return -1;
            }

            try
            {
                IPrintOrderInstance.SetPatient(myPatientInfo);
                IPrintOrderInstance.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 1;
        }
        /// <summary>
        /// ������뵥��ӡ// {0045F3F6-1B1C-4d0a-A834-8BD07286E175}
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
        /// ���ҽ���Ƿ����ɾ�����ƶ�
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

        #region ��ȡ����

        /// <summary>
        /// ��ȡ��󷽺�
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public int GetMaxCombNo(FS.HISFC.Models.Order.Inpatient.Order order, int sheetIndex)
        {
            if (sheetIndex == -1)
            {
                sheetIndex = this.fpOrder.ActiveSheetIndex;
            }

            //�����Ϸ���
            int maxSubCombNo = 0;

            //���Ȼ�ȡ���ݿ�������
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
                    Classes.Function.ShowBalloonTip(2, "����", ex.Message, ToolTipIcon.Error);
                }
            }

            //ֻ�ӽ����ȡ���Ժ������������ͬʱ��һ�����߿��������
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
        [Obsolete("���ϣ��޸ķ��Ż�ȡģʽ", true)]
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
        /// ɾ�����
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
        /// ��ȡ��ͬ����ҽ��
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

                //���ݷ������ʱ����ȡ���µ�
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

        #region LIS��PACS�ӿ�ʵ��

        /// <summary>
        /// ��ʾLIS���
        /// </summary>
        public void QueryLisResult()
        {
            try
            {
                if (this.myPatientInfo == null || this.myPatientInfo.PID.CardNO == "" || myPatientInfo.PID.CardNO == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ѡ���ߣ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("��ѯLIS�ӿڳ��ִ���\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("û��ά��LIS�ӿڣ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// �Ͽ�LIS��ѯ����
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
        /// PACS�����ѯ�ӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.Common.IPacs pacsInterface = null;

        /// <summary>
        /// ��ʼ��PACS�ӿ�
        /// </summary>
        /// <returns></returns>
        private int InitPacsInterface()
        {
            this.pacsInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IPacs)) as FS.HISFC.BizProcess.Interface.Common.IPacs;
            if (this.pacsInterface == null)
            {
                if (string.IsNullOrEmpty(FS.FrameWork.WinForms.Classes.UtilInterface.Err))
                {

                    MessageBox.Show("��ѯPACS�ӿڳ��ִ���\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("û��ά��PACS�����ѯ�ӿڣ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }
            if (this.pacsInterface.Connect() == 0)
            {
                MessageBox.Show("��ʼ��PACSʧ�ܣ����ٲ鿴һ�Σ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("��ѡ���ߺ��ٵ����ѯ��\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("��ѯҽ�ƽ������\r\n" + IMedicalResult.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return rev;
            }
            return 1;
        }

        /// <summary>
        /// �鿴PACS��鱨�浥
        /// </summary>
        public void QueryPacsReport()
        {
            if (this.myPatientInfo == null || this.myPatientInfo.PID.CardNO == "" || myPatientInfo.PID.CardNO == null)
            {
                MessageBox.Show("��ѡ��һ�����ߣ�");
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

                        MessageBox.Show("�鿴PACS���ʧ�ܣ����ٲ鿴һ�Σ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�鿴PACS������ִ������ٲ鿴һ�Σ�\r\n" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// �Ͽ�PACS��ѯ����
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
                ////����ѿ���ҽ��������ʾ  Add By liangjz 2005-08
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

        #region ���ݻ�������ȡ��������

        /// <summary>
        /// ���ݿ������ҡ���������ȡִ�п���
        /// ������ҩƷ
        /// </summary>
        /// <param name="patientType"></param>
        /// <param name="reciptDept"></param>
        /// <returns></returns>
        private FS.FrameWork.Models.NeuObject GetExecDept(ReciptPatientType patientType, FS.FrameWork.Models.NeuObject reciptDept)
        {
            if (patientType == ReciptPatientType.ConsultationPatient//���ﻼ��
                || patientType == ReciptPatientType.AuthorizedPatient//��Ȩ����
                || patientType == ReciptPatientType.FindedPatient//���һ���
                || patientType == ReciptPatientType.MedicsPatient)//ҽ���黼��
            {
                return this.myPatientInfo.PVisit.PatientLocation.Dept;
                //return reciptDept;
            }
            else if (patientType == ReciptPatientType.PrivatePatient//�ֹܻ���
                || patientType == ReciptPatientType.DeptPatient)//���һ���
            {
                return reciptDept;
            }

            return reciptDept;
        }

        #endregion

        #region ������ҩ

        /// <summary>
        /// ��ʼ��������ҩ
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
        /// ������ҩˢ��
        /// </summary>
        private void PassRefresh()
        {
            if (IReasonableMedicine != null && IReasonableMedicine.PassEnabled)
            {
                IReasonableMedicine.PassRefresh();
            }
        }

        /// <summary>
        /// ����������ҩ
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
                //MessageBox.Show("������ҩ������δ����,���ܽ�����ҩ���,�����µ�½����վ��");
            }
        }

        /// <summary>
        /// ����ҩƷϵͳҩƷ��ѯ  Add By liangjz 2005-11
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

            #region �ɵ�����

            //if (this.IReasonableMedicine != null && !this.IReasonableMedicine.PassEnabled)
            //    return;
            //ToolStripItem muItem = sender as ToolStripItem;
            //switch (muItem.Text)
            //{

            //    #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} ��Ӻ�����ҩ�Ҽ��˵�

            //    #region һ���˵�

            //    case "����ʷ/����״̬":
            //        int iReg;
            //        this.IReasonableMedicine.PassSetPatientInfo(this.myPatientInfo, this.empl.ID, this.empl.Name);
            //        this.IReasonableMedicine.ShowFloatWin(false);
            //        iReg = this.IReasonableMedicine.DoCommand(22);
            //        if (iReg == 2)
            //        {
            //            this.PassTransOrder(1, true);
            //        }
            //        break;

            //    case "ҩ���ٴ���Ϣ�ο�":
            //        this.PassTransDrug(101);
            //        break;
            //    case "ҩƷ˵����":
            //        this.PassTransDrug(102);
            //        break;
            //    case "�й�ҩ��":
            //        this.PassTransDrug(107);
            //        break;
            //    case "������ҩ����":
            //        this.PassTransDrug(103);
            //        break;
            //    case "ҩ�����ֵ":
            //        this.PassTransDrug(104);
            //        break;
            //    case "�ٴ�������Ϣ�ο�":
            //        this.PassTransDrug(220);
            //        break;

            //    case "ҽҩ��Ϣ����":
            //        this.PassTransDrug(106);
            //        break;

            //    case "ҩƷ�����Ϣ":
            //        this.PassTransDrug(13);
            //        break;
            //    case "��ҩ;�������Ϣ":
            //        this.PassTransDrug(14);
            //        break;
            //    case "ҽԺҩƷ��Ϣ":
            //        this.PassTransDrug(105);
            //        break;

            //    case "ϵͳ����":
            //        this.PassTransDrug(11);
            //        break;

            //    case "��ҩ�о�":
            //        this.IReasonableMedicine.ShowFloatWin(false);
            //        this.PassTransOrder(12, false);
            //        break;

            //    case "����":
            //        this.PassTransDrug(6);
            //        break;

            //    case "���":
            //        this.IReasonableMedicine.ShowFloatWin(false);
            //        this.PassTransOrder(3, true);
            //        break;

            //    #endregion

            //    #region �����˵�

            //    case "ҩ��-ҩ���໥����":
            //        this.PassTransDrug(201);
            //        break;
            //    case "ҩ��-ʳ���໥����":
            //        this.PassTransDrug(202);

            //        break;
            //    case "����ע�����������":
            //        this.PassTransDrug(203);
            //        break;
            //    case "����ע�����������":
            //        this.PassTransDrug(204);
            //        break;

            //    case "����֢":
            //        this.PassTransDrug(205);
            //        break;
            //    case "������":
            //        this.PassTransDrug(206);
            //        break;

            //    case "��������ҩ":
            //        this.PassTransDrug(207);
            //        break;
            //    case "��ͯ��ҩ":
            //        this.PassTransDrug(208);
            //        break;
            //    case "��������ҩ":
            //        this.PassTransDrug(209);
            //        break;
            //    case "��������ҩ":
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
        /// ��ʼ��������ҩ�ӿ�
        /// </summary>
        private void InitReasonableMedicine()
        {
            if (this.IReasonableMedicine == null)
            {
                this.IReasonableMedicine = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine)) as FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine;
            }
        }

        /// <summary>
        /// ������ҩϵͳ�в鿴�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpOrder_CellClick(object sender, CellClickEventArgs e)
        {
            #region ������ʾ������ҩ��Ϣ��û���ˣ������Σ�����������þ���˫����

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

                        //�����������ʾҪ����ʾ
                        //if (e.Column == this.myOrderClass.GetColumnIndexFromName("ҽ������"))
                        //{
                        ////ò������ֻ�����½ǵ�����λ�����
                        //if (this.IReasonableMedicine.PassShowSingleDrugInfo(info,
                        //    new Point(MousePosition.X, MousePosition.Y - 60),
                        //    new Point(MousePosition.X + 100, MousePosition.Y + 15), false) == -1)
                        //{
                        //    MessageBox.Show(IReasonableMedicine.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                        //}
                        //���������ҩ��ʾ������ʾ��ʱ�Ծ�ʾ��������
                        //else if (e.Column == myOrderClass.GetColumnIndexFromName("��"))
                        //{
                        //    if (this.IReasonableMedicine.PassShowWarnDrug(info) == -1)
                        //    {
                        //        MessageBox.Show(IReasonableMedicine.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            #endregion
        }
        /// <summary>
        /// ȡ��ʱҽ��ҳ�е�ҽ����Ŀ,�������ƴ���
        /// </summary>
        /// <returns>null��0ʧ��</returns>
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
        /// �˳�������ҩϵͳ
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
        /// ������ҩϵͳ�в鿴�����
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

                    #region ҩƷ��ѯ
                    try
                    {
                        //ò������ֻ�����½ǵ�����λ�����
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
        /// �������ҩϵͳ���͵�ǰҽ���������
        /// </summary>
        ///<param name="checkType">��鷽ʽ 1 �Զ���� 12 ��ҩ�о�  3 �ֹ����</param>
        /// <param name="warnPicFlag">�Ƿ���ʾͼƬ������Ϣ</param>
        public int PassCheckOrder(ArrayList alPassOrder, bool isSave)
        {
            ArrayList alOrder = new ArrayList();
            FS.HISFC.Models.Order.Inpatient.Order order;
            DateTime sysTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            #region ��鳤��

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

            #region �������

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
                    MessageBox.Show("������ҩ������" + this.IReasonableMedicine.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region ��̬��ȡ������˳��

        /// <summary>
        /// ���ø�����ͷ
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
        /// ��ȡҽ��ʵ��
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
            #region �ӹ�ϣ����ȡֵ
            else if (this.htOrder != null && this.htOrder.ContainsKey(this.fpOrder.Sheets[SheetIndex].Cells[i, dicColmSet["ҽ����ˮ��"]].Text))
            {
                order = this.htOrder[this.fpOrder.Sheets[SheetIndex].Cells[i, dicColmSet["ҽ����ˮ��"]].Text] as FS.HISFC.Models.Order.Inpatient.Order;
            }
            #endregion
            else
            {
                #region ��ֵ
                order = CacheManager.InOrderMgr.QueryOneOrder(this.fpOrder.Sheets[SheetIndex].Cells[i, dicColmSet["ҽ����ˮ��"]].Text);
                #endregion
            }
            return order;
        }

        #region �洢ҽ���Ĺ�ϣ�����ҽ����ѯ�ٶ�
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
            ArrayList alHerbal = new ArrayList(); //��ҩ

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
                            if (order.Combo.ID != "" && order.Combo.ID != comboID)//�µ�
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
                            if (order.Combo.ID != "" && order.Combo.ID != comboID)//�µ�
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

                Classes.Function.ShowBalloonTip(3, "��ʾ", "��ע����ִ�п����Ƿ���ȷ��", ToolTipIcon.Info);
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
        /// �ж����ҽ��
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

                        MessageBox.Show(string.Format("�����������������Ŀ{0}״̬�������޸ģ�������ѡ��", o.Item.Name));
                        return -1;
                    }
                    if (!String.IsNullOrEmpty(o.ApplyNo))
                    {
                        MessageBox.Show(string.Format("�����������������Ŀ{0}�Ѿ��������뵥����ɾ�����¿�����ϣ�", o.Item.Name));
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
                            MessageBox.Show("Ƶ�β�ͬ������������ã�");
                            return -1;
                        }
                        if (o.OrderType.IsDecompose)
                        {
                            if (o.FirstUseNum != firstDay)
                            {
                                MessageBox.Show("��������ͬ������������ã�");
                                return -1;
                            }
                        }

                        //ֻ��ҩƷ�ж��÷��Ƿ���ͬ
                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)		//ֻ��ҩƷ�ж��÷��Ƿ���ͬ
                        {
                            if (o.Item.SysClass.ID.ToString() != "PCC" && o.Usage.ID != usage.ID)
                            {
                                MessageBox.Show("�÷���ͬ������������ã�");
                                return -1;
                            }
                            if (sysClassID == "PCC")
                            {
                                if (o.Item.SysClass.ID.ToString() != sysClassID)
                                {
                                    MessageBox.Show("��ҩ�����Ժ�����ҩƷ����ã�");
                                    return -1;
                                }
                            }
                            else
                            {
                                if (o.Item.SysClass.ID.ToString() == "PCC")
                                {
                                    MessageBox.Show("��ҩ�����Ժ�����ҩƷ����ã�");
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            if (o.Item.SysClass.ID.ToString() == "UL")//����
                            {
                                if (o.Qty != amount)
                                {
                                    MessageBox.Show("����������ͬ������������ã�");
                                    return -1;
                                }
                                if (o.Sample.Name != sample)
                                {
                                    MessageBox.Show("����������ͬ������������ã�");
                                    return -1;
                                }
                            }

                            if (o.Item.SysClass.ID.ToString() != sysClassID)
                            {
                                MessageBox.Show("ϵͳ���ͬ������������ã�");
                                return -1;
                            }
                        }


                        if (o.ExeDept.ID != exeDept.ID)
                        {
                            MessageBox.Show("ִ�п��Ҳ�ͬ���������ʹ��!", "��ʾ");
                            return -1;
                        }
                    }
                }

            }
            return 0;

        }

        public Dictionary<string, int> dicColmSet = new Dictionary<string, int>();

        /// <summary>
        /// ��̬��ȡ����Ϣ
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
            dicColmSet.Add("��Ч", 1);
            dicColmSet.Add("ҽ������", 2);
            dicColmSet.Add("ҽ����ˮ��", 3);
            dicColmSet.Add("ҽ��״̬", 4);
            dicColmSet.Add("��Ϻ�", 5);
            dicColmSet.Add("��ҩ", 6);
            dicColmSet.Add("���", 7);
            dicColmSet.Add("����ʱ��", 8);
            dicColmSet.Add("����ҽ��", 9);
            dicColmSet.Add("˳���", 10);
            dicColmSet.Add("ҽ������", 11);
            dicColmSet.Add("��", 12);
            dicColmSet.Add("������", 13);
            dicColmSet.Add("ÿ������", 14);
            dicColmSet.Add("��λ", 15);
            dicColmSet.Add("Ƶ��", 16);
            dicColmSet.Add("Ƶ������", 17);
            dicColmSet.Add("�÷�����", 18);
            dicColmSet.Add("�÷�", 19);
            dicColmSet.Add("����", 20);
            dicColmSet.Add("������λ", 21);
            dicColmSet.Add("����", 22);
            dicColmSet.Add("ϵͳ���", 23);
            dicColmSet.Add("��ʼʱ��", 24);
            dicColmSet.Add("����ʱ��", 25);
            dicColmSet.Add("ֹͣʱ��", 26);
            dicColmSet.Add("ִ�п��ұ���", 27);
            dicColmSet.Add("ִ�п���", 28);
            dicColmSet.Add("��", 29);
            dicColmSet.Add("��鲿λ", 30);
            dicColmSet.Add("��������", 31);
            dicColmSet.Add("ȡҩҩ������", 32);
            dicColmSet.Add("ȡҩҩ��", 33);
            dicColmSet.Add("��ע", 34);
            dicColmSet.Add("¼���˱���", 35);
            dicColmSet.Add("¼����", 36);
            dicColmSet.Add("��������", 37);
            dicColmSet.Add("ֹͣ�˱���", 38);
            dicColmSet.Add("ֹͣ��", 39);
            dicColmSet.Add("����", 40);
            dicColmSet.Add("����ҽ������", 41);

            //}
            return 1;
        }

        #endregion
    }

    /// <summary>
    /// �趨��ͷ��ʾ
    /// </summary>
    public class ColmSet
    {
        public static string ̾�� = "!";
        public static string Q��Ч = "��Ч";
        public static string Yҽ������ = "ҽ������";
        public static string Yҽ����ˮ�� = "ҽ����ˮ��";
        public static string Yҽ��״̬ = "ҽ��״̬";
        public static string Z��Ϻ� = "��Ϻ�";
        public static string Z��ҩ = "��ҩ";
        public static string Z��� = "���";
        public static string K����ʱ�� = "����ʱ��";
        public static string K����ҽ�� = "����ҽ��";
        public static string S˳��� = "˳���";
        public static string Yҽ������ = "ҽ������";
        public static string Z�� = "��";
        public static string S������ = "������";
        public static string Mÿ������ = "ÿ������";

        /// <summary>
        /// ÿ�������ĵ�λ
        /// </summary>
        public static string D��λ = "��λ";
        public static string PƵ�� = "Ƶ��";
        public static string PƵ������ = "Ƶ������";
        public static string Y�÷����� = "�÷�����";
        public static string Y�÷� = "�÷�";
        public static string Z���� = "����";
        public static string Z������λ = "������λ";
        public static string F���� = "����";
        public static string Xϵͳ��� = "ϵͳ���";
        public static string K��ʼʱ�� = "��ʼʱ��";
        public static string J����ʱ�� = "����ʱ��";
        public static string Tֹͣʱ�� = "ֹͣʱ��";
        public static string Zִ�п��ұ��� = "ִ�п��ұ���";
        public static string Zִ�п��� = "ִ�п���";
        public static string J�� = "��";
        public static string J��鲿λ = "��鲿λ";
        public static string Y�������� = "��������";
        public static string Qȡҩҩ������ = "ȡҩҩ������";
        public static string Qȡҩҩ�� = "ȡҩҩ��";
        public static string B��ע = "��ע";
        public static string L¼���˱��� = "¼���˱���";
        public static string L¼���� = "¼����";
        public static string K�������� = "��������";
        public static string Tֹͣ�˱��� = "ֹͣ�˱���";
        public static string Tֹͣ�� = "ֹͣ��";
        public static string D���� = "����";
        public static string G����ҽ������ = "����ҽ������";

        public static string ALL = "����";
    }

    /// <summary>
    /// �����������
    /// </summary>
    public enum ReciptPatientType
    {
        /// <summary>
        /// �ֹܻ���
        /// </summary>
        [FS.FrameWork.Public.Description("�ֹܻ���")]
        PrivatePatient = 0,

        /// <summary>
        /// ���һ���
        /// </summary>
        [FS.FrameWork.Public.Description("���һ���")]
        DeptPatient = 1,

        /// <summary>
        /// ���ﻼ��
        /// </summary>
        [FS.FrameWork.Public.Description("���ﻼ��")]
        ConsultationPatient,

        /// <summary>
        /// ��Ȩ����
        /// </summary>
        [FS.FrameWork.Public.Description("��Ȩ����")]
        AuthorizedPatient,

        /// <summary>
        /// ���һ���
        /// </summary>
        [FS.FrameWork.Public.Description("���һ���")]
        FindedPatient,

        /// <summary>
        /// ҽ�����ڻ���
        /// </summary>
        [FS.FrameWork.Public.Description("ҽ�����ڻ���")]
        MedicsPatient
    }


    /// <summary>
    /// ҽ������
    /// </summary>
    public enum EnumFilterList
    {
        /// <summary>
        /// ȫ��ҽ��
        /// </summary>
        All = 0,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        Today = 1,

        /// <summary>
        /// ��Чҽ��
        /// </summary>
        Valid = 2,

        /// <summary>
        /// ��Чҽ��
        /// </summary>
        Invalid = 3,

        /// <summary>
        /// �¿���ҽ��
        /// </summary>
        New = 4,

        /// <summary>
        /// ������ҽ��
        /// </summary>
        UC_ULOrder = 5
    }

    /// <summary>
    /// ҽ���������sortid����
    /// </summary>
    public class OrderSortIDCompare : IComparer
    {
        #region IComparer ��Ա

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
