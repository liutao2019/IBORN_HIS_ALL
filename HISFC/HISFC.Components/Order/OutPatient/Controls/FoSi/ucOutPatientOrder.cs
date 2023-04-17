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
    /// ����ҽ��վ
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

        #region ����

        #region �������

        /// <summary>
        /// �Ƿ�����鸽��
        /// </summary>
        protected bool isDealSubtbl = false;

        /// <summary>
        /// ��ҩƷ�������Ƿ񵥶�����Ӽ�ҽ��
        /// </summary>
        protected bool isDealEmrOrderSubtblSpecially = false;

        /// <summary>
        /// �Ӽ�ҽ��ִ�з�ʽ
        /// </summary>
        protected string emrSubtblUsage = "";

        /// <summary>
        /// ����ҽ���Ϲܵ�ִ�з�ʽ
        /// </summary>
        protected string ULOrderUsage = "";

        #endregion

        #region ����

        /// <summary>
        /// �Һ���Ч����
        /// </summary>
        public int validRegDays = 1;

        /// <summary>
        /// ��ʱ���Ʋ���
        /// </summary>
        protected object tempControler;

        /// <summary>
        /// �洢ҽ������
        /// </summary>
        protected DataSet dtOrder = null;

        /// <summary>
        /// �洢ҽ������
        /// </summary>
        protected DataView dvOrder = null;

        /// <summary>
        /// ���˳���
        /// </summary>
        private int MaxSort = 0;

        /// <summary>
        /// �Ƿ����ڱ༭����
        /// </summary>
        protected bool EditGroup = false;

        /// <summary>
        /// ��ʱ���ҽ����Ϣ
        /// </summary>
        private ArrayList alOrderTemp = new ArrayList();

        /// <summary>
        /// ȫ��ҽ����Ϣ
        /// </summary>
        public ArrayList alAllOrder = new ArrayList();

        /// <summary>
        /// ��ǰ��ҽ����Ϣ
        /// </summary>
        protected Neusoft.HISFC.Models.Order.OutPatient.Order currentOrder = null;

        /// <summary>
        /// ��ǰ����
        /// </summary>
        protected Neusoft.HISFC.Models.Registration.Register currentPatientInfo = new Neusoft.HISFC.Models.Registration.Register();

        /// <summary>
        /// ��ǰ��̨
        /// </summary>
        protected Neusoft.FrameWork.Models.NeuObject currentRoom = null;

        /// <summary>
        /// ������ʾ�����ļ�
        /// </summary>
        private string SetingFileName = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + @".\clinicordersetting.xml";

        /// <summary>
        /// ��ͣ��ʾ
        /// </summary>
        ToolTip tooltip = new ToolTip();

        /// <summary>
        /// ������ӡ�ӿ�
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.IRecipePrint iRecipePrint = null;

        /// <summary>
        /// �Ҽ��˵�
        /// </summary>
        private Neusoft.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = null;

        /// <summary>
        /// �洢��ϱ仯��ҽ���Ĺ�ϣ��
        /// {F67E089F-1993-4652-8627-300295AAED8C}
        /// </summary>
        private Hashtable hsComboChange = new Hashtable();

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        private string errInfo = "";

        /// <summary>
        /// �Ƿ��д���Ȩ
        /// </summary>
        //public bool isHaveOrderPower = false;

        #region ���Һ���

        /// <summary>
        /// ����Ŷ�Ӧ��������Ŀ
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug emergRegItem = null;

        /// <summary>
        /// ҽ��ְ�ƶ�Ӧ��������Ŀ
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug diagItem = null;

        /// <summary>
        /// �ҺŷѲ����Ŀ
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug diffDiagItem = null;

        /// <summary>
        /// ���յĹҺŷ���Ŀ
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug regItem = null;

        /// <summary>
        /// ��ҺŷѵĿ���
        /// </summary>
        private Hashtable hsNoSupplyRegDept = new Hashtable();

        /// <summary>
        /// ��ǰ����Ա
        /// ���ݿ���ȡ��
        /// </summary>
        Neusoft.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        private ArrayList alSupplyFee = new ArrayList();

        /// <summary>
        /// �Ƿ����÷���ϵͳ
        /// </summary>
        private bool isUseNurseArray;

        /// <summary>
        /// ����ҽ���Ƿ��Զ���ӡ����:0 ���Զ���ӡ��1 �Զ���ӡ��2 ���Ԥ������;3 ���Ԥ�����Զ���ӡ
        /// </summary>
        private int isAutoPrintRecipe = 0;

        /// <summary>
        /// �Ƿ�����ҽ������治���ҩƷ��0������1 ��ʾ��2 ����
        /// </summary>
        private int isCheckDrugStock = 0;

        /// <summary>
        /// ��ͬ��λ������
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper pactHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        #endregion

        /// <summary>
        /// ���Ĵ���ģʽ��0 ������Զ�������1 ���������㣬��ʾ�ڽ����������޸�
        /// </summary>
        private int dealSublMode = -1;

        #endregion

        #region ���Ʋ���

        /// <summary>
        /// �Ƿ��¼ӣ��޸�ʱ��
        /// </summary>
        protected bool dirty = false;

        /// <summary>
        /// �Ƿ����ϵͳ��� ������ҩ����ҩ��ϡ�����ͬһ����
        /// </summary>
        bool isDecSysClassWhenGetRecipeNO = false;

        /// <summary>
        /// �Ƿ��Ѳ�ѯ����isDecSysClassWhenGetRecipeNO
        /// </summary>
        bool ynGetSysClassControl = false;

        /// <summary>
        /// ����ҽ��վ�Ƿ������޸ĺ�ͬ��λ��Ϣ
        /// </summary>
        private bool isAllowChangePactInfo = false;

        /// <summary>
        /// �Ƿ񱣴�ҽ���޸ļ�¼
        /// </summary>
        protected bool isSaveOrderHistory = false;

        /// <summary>
        /// �Ƿ�ͨ��seeno�������
        /// </summary>
        protected bool isCountFeeBySeeNo = false;

        /// <summary>
        /// �Ƿ��ڽ�������ʾ���ظ���Ŀ
        /// </summary>
        protected bool isShowRepeatItemInScreen = false;

        //�Ƿ���ʾ����ҩƷ
        private bool isShowHardDrug = true;

        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();




        /// <summary>
        /// ÿ��������ֵ����С��λ������)
        /// </summary>
        private decimal doceOnceLimit = -1;

        /// <summary>
        /// �Ƿ����ҩƷ����Ȩ��
        /// </summary>
        //public bool isControlDrugOrder = false;

        /// <summary>
        /// �Ƿ���ҩƷ��������ʾ���ͼ۸�
        /// 0 ������ʾ��1 ��ʾ���2 ��ʾ�۸�3 ��񡢼۸���ʾ
        /// </summary>
        private int isShowSpecsAndPrice = 0;

        /// <summary>
        /// �Ƿ�Ĭ��ѡ�е�������Ƶ�Ρ��÷�ȫ���޸ģ������¼�������
        /// 000 ��λ���ֱַ��ʾ��������Ƶ�Ρ��÷�
        /// </summary>
        private string isChangeAllSelect = "-1";

        /// <summary>
        /// �Ƿ��ڿ���֮ǰ��ӹҺŷ���Ŀ (���dealSublMode=1 ʹ��)
        /// </summary>
        private bool isAddRegSubBeforeAddOrder = false;

        /// <summary>
        /// ��ǰ�˻������ʾ��Ϣ
        /// </summary>
        private string vacancyDisplay = "";

        /// <summary>
        /// ����ҽ��վ�Ƿ������Һ�
        /// </summary>
        //private bool isDoctRegistered = false;

        /// <summary>
        /// �Ƿ�Ԥ�ۿ��
        /// {E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
        /// </summary>
        private bool isPreUpdateStockinfo = false;

        /// <summary>
        /// ����ҽ��վƤ�Դ���ģʽ��0 ����ʾƤ�� 1����ʾ�Ƿ�2����������ѡ��
        /// {0733E2AD-EB02-4b6f-BCF8-1A6ED5A2EFAD}
        /// </summary>
        private string hypotestMode = "1";

        /// <summary>
        /// �����˻�:true�ն��շ� false�����շ�
        /// </summary>
        private bool isAccountTerimal = false;

        /// <summary>
        /// �Ƿ������˻�����
        /// </summary>
        private bool isAccountMode = false;

        /// <summary>
        /// ���洦��ʱ�Ƿ��ж����
        /// </summary>
        private bool isJudgeDiagnose = true;

        /// <summary>
        /// �Ƿ�����޸�Ժע
        /// </summary>
        private bool isCanModifiedInjectNum = true;

        /// <summary>
        /// �Ƿ��������¼��㸨��
        /// </summary>
        private bool isCalculatSubl = false;

        #endregion

        #region �ӿ�

        /// <summary>
        /// ����������뵥
        /// </summary>
        protected Neusoft.ApplyInterface.HisInterface PACSApplyInterface = null;

        /// <summary>
        /// ��ͬ��λУ��ӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Common.ICheckPactInfo iCheckPactInfo = null;

        /// <summary>
        /// ����󴦷�����ӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Order.ISaveOrder IAfterSaveOrder = null;

        /// <summary>
        /// ���洦��ǰ����
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder IBeforeSaveOrder = null;

        /// <summary>
        /// ������Ŀǰ�����ӿ�
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddItem IBeforeAddItem = null;

        /// <summary>
        /// ��������ǰ�ӿ�
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddOrder IBeforeAddOrder = null;

        /// <summary>
        /// ҽ����Ϣ����ӿ�
        /// {48E6BB8C-9EF0-48a4-9586-05279B12624D}
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.IAlterOrder IAlterOrderInstance = null;

        /// <summary>
        /// ������뵥��ӡ�ӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Common.ICheckPrint checkPrint = null;

        /// <summary>
        /// ֱ���շѽӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee IDoctFee = null;

        /// <summary>
        /// ҽ��վ���Ĵ���ӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob IDealSubjob = null;

        /// <summary>
        /// ������ҩ�ӿ�
        /// </summary>
        IReasonableMedicine IReasonableMedicine = null;

        #endregion

        #region ������

        /// <summary>
        /// ҽ��������
        /// </summary>
        protected Neusoft.FrameWork.Public.ObjectHelper orderHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// Ƶ�ΰ�����
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper frequencyHelper;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        protected Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������������ְࣺ����������Ŀ����
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper diagFeeConstHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        #endregion

        #region ί���¼�

        public delegate void EventButtonHandler(bool b);

        /// <summary>
        /// ��������ʱˢ��������
        /// </summary>
        public event EventHandler OnRefreshGroupTree;

        /// <summary>
        /// ҽ���Ƿ����ȡ������¼�
        /// </summary>
        public event EventButtonHandler OrderCanCancelComboChanged;

        /// <summary>
        /// �Ƿ�ɴ�ӡ��鵥�¼�
        /// </summary>
        public event EventButtonHandler OrderCanSetCheckChanged;

        #endregion

        #region ҵ���

        /// <summary>
        /// ��Ϲ���
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase diagnoseMgr = new Neusoft.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();

        /// <summary>
        /// ҽ��ҵ���
        /// </summary>
        protected Neusoft.HISFC.BizLogic.Order.OutPatient.Order OrderManagement = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee feeManagement = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ��ҩƷҵ��
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee itemManagement = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ҽ���ӿ�
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Interface interfaceMgr = new Neusoft.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// ���ҽ��������Ŀ
        /// </summary>
        Hashtable hsCompareItems = null;

        /// <summary>
        /// �Ƿ���ʾҽ�����ձ��
        /// </summary>
        private bool isShowPactCompareFlag = true;

        /// <summary>
        /// ��ҩƷ�����Ŀҵ��
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee undrugztManager = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// �ۺϹ���ҵ���
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �Ű����
        /// </summary>
        private Neusoft.HISFC.BizLogic.Registration.Schema schemgManager = new Neusoft.HISFC.BizLogic.Registration.Schema();

        /// <summary>
        /// סԺ���ת
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.RADT radtManger = new Neusoft.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// �Һ�ҵ���
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regManagement = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ����ҵ��
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager managerAssign = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ҩƷҵ��
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ���Ʋ�������
        /// </summary>
        protected Neusoft.FrameWork.Management.ControlParam controlMgr = new Neusoft.FrameWork.Management.ControlParam();

        /// <summary>
        /// ��������
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Constant conManager = new Neusoft.HISFC.BizLogic.Manager.Constant();

        private Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientFee = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ��Ϲ����� 
        /// </summary>
        private Neusoft.HISFC.BizLogic.HealthRecord.Diagnose myDiagnose = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// �˻�����
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Account accountMgr = new Neusoft.HISFC.BizLogic.Fee.Account();

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ��ӡԤ������
        /// </summary>
        //public bool bPrintViewRecipe = false;

        //public event EventButtonHandler OrderCanOperatorChanged;	//ҽ���Ƿ���Ե����������

        //public delegate void OrderQtyChangedHandler(Neusoft.HISFC.Models.Registration.Register rInfo, Neusoft.FrameWork.Management.Transaction trans);
        //public event OrderQtyChangedHandler SetFeeDisplay;

        /// <summary>
        /// �Ƿ��ڿ���״̬�����������β�ѯ���ݿ�
        /// </summary>
        protected bool bTempVar = false;

        private string varCombID = "";//��ʱ����Ϻű���

        private string varTempUsageID = "zuowy";//��ʱ�÷�
        private string varOrderUsageID = "maokb";//ҽ���÷�
        //protected bool bCanAddOrder = true;//�Ƿ������������Է���Ŀ������ͬһ���� 1 Yes 0 No

        /// <summary>
        /// �Ƿ��޸Ĺ�ҽ��
        /// </summary>
        //private bool isEdit = false;

        /// <summary>
        /// �Ƿ�������ҩ�ͺ���ҩ��ͬһ���� 1 ���� 0 ������
        /// </summary>
        //protected bool bCanInSameRecipe = true;

        #endregion
        #endregion

        #region ����

        /// <summary>
        /// �����˻�:true�ն��շ� false�����շ�
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
        /// �Ƿ������˻�����
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
        /// �Ƿ����ģʽ
        /// </summary>
        protected bool bIsDesignMode = false;

        /// <summary>
        /// �Ƿ���ʾ�Ҽ������˵�
        /// </summary>
        protected bool bIsShowPopMenu = true;

        /// <summary>
        /// �Ҽ��˵�
        /// </summary>
        public bool IsShowPopMenu
        {
            set
            {
                this.bIsShowPopMenu = value;
            }
        }

        /// <summary>
        /// �Ƿ�ͨ��seeno�������
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
        /// �Ƿ�ͨ��seeno�������
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
        /// �Ƿ���ʾ�����Ŀϸ��Ŀ
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
        /// �Ƿ���ģʽ
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
        /// ������Ŀ���������ʾ
        /// </summary>
        private void SetItemSelectControl()
        {
            this.ucOutPatientItemSelect1.Visible = this.bIsDesignMode;
        }

        /// <summary>
        /// ���߻�����Ϣ
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
                    //����ͬһ���ˣ������Ĭ��������Ϣ��
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
                                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ�˻�������" + accountMgr.Err);
                                    //return;
                                }
                                //û���˻����˻�ͣ��
                                else if (rev == 0)
                                {
                                    this.vacancyDisplay = "���˻�";
                                }
                                else
                                {
                                    this.vacancyDisplay = vacancy.ToString() + "Ԫ";
                                }
                            }

                            this.QueryOrder();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��ǰ��̨
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
        /// ��������
        /// </summary>
        protected Neusoft.FrameWork.Models.NeuObject reciptDept = null;

        /// <summary>
        /// ���ÿ�������
        /// </summary>
        [DefaultValue(null)]
        public void SetReciptDept(Neusoft.FrameWork.Models.NeuObject value)
        {
            this.reciptDept = value;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public Neusoft.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.reciptDept == null)
                {
                    //������Ű���Ϣ��ȥ�Ű������Ϊ�������� {231D1A80-6BF6-413f-8BBF-727DC2BF47D9}
                    Neusoft.HISFC.Models.Registration.Schema schema = this.regManagement.GetSchema(GetReciptDoct().ID, this.OrderManagement.GetDateTimeFromSysDateTime());
                    if (schema != null && schema.Templet.Dept.ID != "")
                    {
                        this.reciptDept = schema.Templet.Dept.Clone();
                    }
                    //û���Ű�ȡ��½������Ϊ��������
                    else
                    {
                        this.reciptDept = ((Neusoft.HISFC.Models.Base.Employee)this.GetReciptDoct()).Dept.Clone(); //��������
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
        /// ����ҽ��
        /// </summary>
        protected Neusoft.FrameWork.Models.NeuObject reciptDoct = null;

        /// <summary>
        /// ��ǰ����ҽ��
        /// </summary>
        public void SetReciptDoc(Neusoft.FrameWork.Models.NeuObject value)
        {
            this.reciptDoct = value;

        }

        /// <summary>
        /// ��ÿ���ҽ��
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
        /// ���߿������,�б��ڹҺſ���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject SeeDept = null;

        /// <summary>
        /// �Ƿ����ú�����ҩ���:��ʾ��ʾ��Ϣ��
        /// </summary>
        private bool enabledPass = true;

        /// <summary>
        /// �Ƿ����ú�����ҩ��飺��ʾ��ʾ��Ϣ��
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
        /// �Ƿ���ʱҽ��״̬
        /// </summary>
        public bool bOrderHistory = false;

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ����Loading
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
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ��ͬ��λ��Ϣ����" + managerIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                #region ��ȡ���Ʋ���

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

                //Ƥ�Դ���ģʽ
                //this.tempControler = Classes.Function.controlerHelper.GetObjectFromID("200201");
                //if (this.tempControler != null)
                //{
                //    this.hypotestMode = ((Neusoft.HISFC.Models.Base.ControlParam)tempControler).ControlerValue.ToString();
                //}
                hypotestMode = this.ctrlMgr.GetControlParam<string>("200201", true, "1");

                this.isCanModifiedInjectNum = this.ctrlMgr.GetControlParam<bool>("MZ5001", true, true);

                //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
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
                this.isPreUpdateStockinfo = this.controlIntegrate.GetControlParam<bool>(Neusoft.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Pre_Out, false, true);//�Ƿ�Ԥ�ۿ��
                if (this.isPreUpdateStockinfo)
                {
                    bool isFeeUpdatePre = controlIntegrate.GetControlParam<bool>("P01015");//�շ�ʱ�Ƿ�Ԥ�� true�շ�ʱԤ�� false����ҽ��Ԥ��
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
                    //������п���
                    ArrayList alDepts = this.managerIntegrate.GetDepartment();
                    this.deptHelper.ArrayObject = alDepts;

                    //�������Ƶ����Ϣ �����������ҩϵͳ����ҽ��Ƶ��               
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

                //��ʼ��PACS{3CF92484-7FB7-41d6-8F3F-38E8FF0BF76A}
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

            base.OnStatusBarInfo(null, "(��ɫ���¿�)(��ɫ���շ�)");

            Classes.Function.SethsUsageAndSub();

            #region �����ӿ�

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

            #region ������ҩ

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
                    //ucOutPatientItemSelect1.MessageBoxShow("������ҩ������δ����,���ܽ�����ҩ���,�����µ�½����վ��");
                }
            }

            #endregion
        }

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            //Ĭ�ϲ���ģʽ--ҽ������ģʽ
            this.ucOutPatientItemSelect1.OperatorType = Operator.Add;

            #region ��ʼ��dataset��Ϣ
            this.dtOrder = this.InitDataSet();
            this.neuSpread1.Sheets[0].DataSource = this.dtOrder.Tables[0];
            #endregion

            //��ʼ��FarPoint
            this.InitFP();

            this.SetItemSelectControl();

            #region FarPoint �¼�
            this.neuSpread1.MouseUp += new MouseEventHandler(neuSpread1_MouseUp);
            this.neuSpread1.Sheets[0].Columns[-1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);

            this.neuSpread1.Sheets[0].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(neuSpread1_Sheet1_CellChanged);

            #endregion

        }

        //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
        /// <summary>
        /// ��ʼ��ֱ���շѽӿ�
        /// </summary>
        private void InitDirectFee()
        {
            if (IDoctFee == null)
            {
                IDoctFee = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee)) as Neusoft.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee;
            }
        }

        /// <summary>
        /// ���Ĵ���ӿ�
        /// </summary>
        private void InitDealSubJob()
        {
            if (IDealSubjob == null)
            {
                IDealSubjob = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob)) as Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
        }

        /// <summary>
        /// ��ʼ��Fp
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
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message + "\r\n ��ɾ�������ļ�[clinicordersetting.xml]�����ԣ�");
            }
        }

        /// <summary>
        /// ��ʼ��dataset
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
                    if (ColumnsSet[i] == "�Ӽ�")
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
        /// ����������
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

            this.neuSpread1.Sheets[0].Columns[GetColumnIndexFromName("ÿ������")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1.Sheets[0].Columns[GetColumnIndexFromName("˳���")].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
        }

        #region ������

        /// <summary>
        /// ����fp����
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
        /// ��������
        /// </summary>
        /// <param name="k"></param>
        private void SetColumnName(int k)
        {
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

            this.neuSpread1.Sheets[k].Columns.Count = ColumnsSet.Length;

            for (int i = 0; i < ColumnsSet.Length; i++)
            {
                this.neuSpread1.Sheets[k].Columns[i].Label = ColumnsSet[i];

                if (ColumnsSet[i] == "ÿ������" || ColumnsSet[i] == "����")
                {
                    this.neuSpread1.Sheets[k].Columns[i].ForeColor = Color.Red;
                    this.neuSpread1.Sheets[k].Columns[i].Font = new Font(this.neuSpread1.Font.FontFamily.Name, this.neuSpread1.Font.Size, FontStyle.Bold, this.neuSpread1.Font.Unit, this.neuSpread1.Font.GdiCharSet);
                }
            }
        }

        /// <summary>
        /// ͨ���������������
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
            ucOutPatientItemSelect1.MessageBoxShow("��������ʱȱ����" + Name);
            return -1;
        }

        /// <summary>
        /// �õ�������
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetColumnNameFromIndex(int i)
        {
            return this.dtOrder.Tables[0].Columns[i].ColumnName;
        }

        /// <summary>
        /// ������
        /// </summary>
        private string[] ColumnsSet = {
                                          "!",            //0
                                          "��",           //1
                                          "ҽ������",     //2
                                          "ҽ����ˮ��",   //3
                                          "ҽ��״̬",     //4
                                          "��Ϻ�",       //5
                                          "��ҩ",         //6
                                          "��Ŀ����",     //7
                                          "ҽ������",     //8
                                          "���",         //33
                                          "ÿ������",     //11
                                          "��λ",         //12
                                          "Ƶ�α���",     //14
                                          "Ƶ������",     //15
                                          "����/����",    //13
                                          "�÷�����",     //16
                                          "�÷�����",     //17
                                          "����",         //9
                                          "������λ",     //10
                                          "Ժע����",     //18
                                          "���",
                                          "����",
                                          "���",
                                          "��ʼʱ��",     //19
                                          "ִ�п��ұ���", //20
                                          "ִ�п���",     //21
                                          "�Ӽ�",         //22
                                          "��ע",         //23
                                          "¼���˱���",   //24
                                          "¼����",       //25
                                          "��������",     //26
                                          "����ʱ��",     //27
                                          "ֹͣʱ��",     //28
                                          "ֹͣ�˱���",   //29
                                          "ֹͣ��",       //30
                                          "˳���",       //31
                                          "����ҽ��",     //32
                                          "��鲿λ",     //34
                                          "��������",     //35
                                          "�ۿ���ұ���", //36
                                          "�ۿ����",     //37
                                          "Ƥ�Դ���",     //38
                                          "Ƥ��"          //39
                                      };

        #endregion

        /// <summary>
        /// ��ʼ��ҽ����Ϣ����ӿ�ʵ��{48E6BB8C-9EF0-48a4-9586-05279B12624D}
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

        #region ˽�з���

        #region ������ݵ����

        /// <summary>
        /// ���ʵ��toTable
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
        /// ���order��row
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
                row[GetColumnIndexFromName("��ҩ")] = Neusoft.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug);//5
                row[GetColumnIndexFromName("ÿ������")] = string.IsNullOrEmpty(order.DoseOnceDisplay) ? order.DoseOnce.ToString() : order.DoseOnceDisplay;//9
                row[GetColumnIndexFromName("��λ")] = objItem.DoseUnit;
                //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                row[GetColumnIndexFromName("����/����")] = order.HerbalQty;//11
            }
            else if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Fee.Item.Undrug))
            {
                row[GetColumnIndexFromName("����/����")] = order.HerbalQty;//11
            }

            if (order.Note != "")
            {
                row[GetColumnIndexFromName("!")] = order.Note;
            }

            row[GetColumnIndexFromName("��")] = "";     //0
            row[GetColumnIndexFromName("ҽ������")] = "����ҽ��";//1
            row[GetColumnIndexFromName("ҽ����ˮ��")] = order.ID;//2
            row[GetColumnIndexFromName("ҽ��״̬")] = order.Status;//�¿�������ˣ�ִ��
            row[GetColumnIndexFromName("��Ϻ�")] = order.Combo.ID;//4

            #region ҽ������

            string specs = string.IsNullOrEmpty(order.Item.Specs) ? "" : ("[" + order.Item.Specs + "] ");
            string price = "";
            if (order.Item.Price > 0)
            {
                if (order.MinunitFlag == "1") //��С��λ�ж�
                {
                    price = "[" + (order.Item.Price / order.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + order.Item.PriceUnit + "]";//6
                }
                else
                {
                    price = "[" + order.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + order.Item.PriceUnit + "]";//6
                }
            }
            else if (order.Unit == "[������]")
            {
                if (order.MinunitFlag == "1")
                {
                    price = "[" + (OutPatient.Classes.Function.GetUndrugZtPrice(order.Item.ID) / order.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + order.Item.PriceUnit + "]";//6
                }
                else
                {
                    price = "[" + OutPatient.Classes.Function.GetUndrugZtPrice(order.Item.ID).ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + order.Item.PriceUnit + "]";//6
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

            row[GetColumnIndexFromName("ҽ������")] = "[��:" + order.SubCombNO.ToString() + "]" + order.Item.Name + specs + price;
            #endregion

            //ҽ����ҩ-֪��ͬ����
            if (order.IsPermission)
            {
                row[GetColumnIndexFromName("ҽ������")] = "���̡�" + row[GetColumnIndexFromName("ҽ������")];
            }

            this.ValidNewOrder(order);
            row[GetColumnIndexFromName("����")] = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
            row[GetColumnIndexFromName("������λ")] = order.Unit;//8
            row[GetColumnIndexFromName("Ƶ�α���")] = order.Frequency.ID;
            row[GetColumnIndexFromName("Ƶ������")] = order.Frequency.Name;
            row[GetColumnIndexFromName("�÷�����")] = order.Usage.ID;
            row[GetColumnIndexFromName("�÷�����")] = order.Usage.Name;//15
            row[GetColumnIndexFromName("��ʼʱ��")] = order.BeginTime;
            row[GetColumnIndexFromName("ִ�п��ұ���")] = order.ExeDept.ID;

            row[GetColumnIndexFromName("ִ�п���")] = order.ExeDept.Name;
            row[GetColumnIndexFromName("�Ӽ�")] = order.IsEmergency;
            row[GetColumnIndexFromName("��鲿λ")] = order.CheckPartRecord;
            row[GetColumnIndexFromName("��������")] = order.Sample;
            row[GetColumnIndexFromName("�ۿ���ұ���")] = order.StockDept.ID;
            row[GetColumnIndexFromName("�ۿ����")] = order.StockDept.Name;
            row[GetColumnIndexFromName("Ժע����")] = order.InjectCount;
            row[GetColumnIndexFromName("��ע")] = order.Memo;//20
            row[GetColumnIndexFromName("¼���˱���")] = order.Oper.ID;
            row[GetColumnIndexFromName("¼����")] = order.Oper.Name;
            row[GetColumnIndexFromName("����ҽ��")] = order.ReciptDoctor.Name;
            row[GetColumnIndexFromName("��������")] = order.ReciptDept.Name;
            row[GetColumnIndexFromName("����ʱ��")] = order.MOTime;

            if (order.EndTime != DateTime.MinValue)
            {
                row[GetColumnIndexFromName("ֹͣʱ��")] = order.EndTime;//25
            }

            row[GetColumnIndexFromName("ֹͣ�˱���")] = order.DCOper.ID;
            row[GetColumnIndexFromName("ֹͣ��")] = order.DCOper.Name;

            row[GetColumnIndexFromName("˳���")] = order.SortID;//28
            row[GetColumnIndexFromName("Ƥ�Դ���")] = order.HypoTest;
            row[GetColumnIndexFromName("Ƥ��")] = this.OrderManagement.TransHypotest(order.HypoTest);
            return row;
        }

        /// <summary>
        /// ���-����
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
        /// ���ҽ����FarPoint
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
                ucOutPatientItemSelect1.MessageBoxShow("Clone����" + ex.Message);
                this.dirty = false;
                return;
            }

            if (this.bTempVar)
            {
                # region �����÷��Զ��������Ժע
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
                                varTempUsageID = "zuowy";//��ʱ�÷�
                                varOrderUsageID = "maokb";//ҽ���÷�
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

            if (order.Note != "")//��ʾ
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("!")].Text = order.Note;
            }

            if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Pharmacy.Item))//ҩƷ
            {
                Neusoft.HISFC.Models.Pharmacy.Item objItem = order.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ÿ������")].Text = string.IsNullOrEmpty(order.DoseOnceDisplay) ? order.DoseOnce.ToString() : order.DoseOnceDisplay;

                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����/����")].Text = order.HerbalQty.ToString();//11

                if (order.DoseUnit == null || order.DoseUnit == "") order.DoseUnit = objItem.DoseUnit;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��λ")].Text = order.DoseUnit;

                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = order.Unit;//8

            }
            else if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Fee.Item.Undrug)) //��ҩƷ
            {
                Neusoft.HISFC.Models.Fee.Item.Undrug objItem = order.Item as Neusoft.HISFC.Models.Fee.Item.Undrug;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��λ")].Text = "";//������λ
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = order.Unit;//8
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����/����")].Text = order.HerbalQty.ToString();//11
            }
            else if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Base.Item))
            {
                Neusoft.HISFC.Models.Fee.Item.Undrug objItem = order.Item as Neusoft.HISFC.Models.Fee.Item.Undrug;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��λ")].Text = "";//������λ
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = order.Unit;//8
            }
            this.ValidNewOrder(order); //��д��Ϣ

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��")].Text = "";     //0

            if (order.NurseStation.Memo != null && order.NurseStation.Memo.Length > 0)
            {
                //������ҩ��أ���ʱδ�����Σ�
                //this.AddWarnPicturn(i, 0, Neusoft.FrameWork.Function.NConvert.ToInt32(order.NurseStation.Memo));
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[rowIndex, GetColumnIndexFromName("��")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[0].Cells[rowIndex, GetColumnIndexFromName("��")].Note = "";
                this.neuSpread1.Sheets[0].Cells[rowIndex, GetColumnIndexFromName("��")].Tag = "";
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��ҩ")].Text = System.Convert.ToInt16(order.Combo.IsMainDrug).ToString();//5
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ҽ������")].Text = "����ҽ��"; //1 ����

            if (order.Item.PackQty == 0)
            {
                order.Item.PackQty = 1;
            }

            #region ҽ������

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ҽ������")].Text = "[��:" + order.SubCombNO.ToString() + "]" + order.Item.Name.ToString();

            string specs = string.IsNullOrEmpty(order.Item.Specs) ? "" : ("[" + order.Item.Specs + "] ");
            string price = "";
            if (order.Item.Price > 0)
            {
                if (order.MinunitFlag == "1"&&IsDesignMode) //��С��λ�ж�
                {
                    price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(order.Item.Price / order.Item.PackQty, 2) + "Ԫ/" + order.Item.PriceUnit;
                }
                else if(order.MinunitFlag == "1"&&!IsDesignMode)
                {
                    //��������׹������ת����ҩƷʵ��Ȼ��ֵ
                    if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Pharmacy.Item))
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item baseItem = order.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                        if (!string.IsNullOrEmpty(baseItem.MinUnit))
                        {
                            price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(baseItem.Price / baseItem.PackQty, 2) + "Ԫ/" + baseItem.MinUnit;
                        }
                        else
                        {
                            price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(baseItem.Price / order.Item.PackQty, 2) + "Ԫ/" + order.Item.PriceUnit;
                        }
                    }
                    else
                    {
                        price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(order.Item.Price / order.Item.PackQty, 2) + "Ԫ/" + order.Item.PriceUnit;
                    }
                }
                else
                {
                    price = order.Item.Price.ToString() + "Ԫ/" + order.Item.PriceUnit;//6
                }
            }
            else if (order.Unit == "[������]")
            {
                if (order.MinunitFlag == "1")
                {
                    price = Neusoft.FrameWork.Public.String.FormatNumberReturnString(OutPatient.Classes.Function.GetUndrugZtPrice(order.Item.ID) / order.Item.PackQty, 2) + "Ԫ/" + order.Item.PriceUnit;//6
                }
                else
                {
                    price = OutPatient.Classes.Function.GetUndrugZtPrice(order.Item.ID).ToString() + "Ԫ/" + order.Item.PriceUnit;//6
                }
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = price;

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

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ҽ������")].Text += specs + price;

            #endregion

            //ҽ������֪��ͬ����
            if (order.IsPermission)
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ҽ������")].Text = order.SubCombNO.ToString() + "���̡�" + this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ҽ������")].Text;
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("���")].Text = order.Item.Specs;


            string totCost = "";
            if (order.MinunitFlag == "1")//������С��λ 
            {
                totCost = (order.Qty * order.Item.Price / order.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
            }
            else
            {
                totCost = (order.Qty * order.Item.Price).ToString("F4").TrimEnd('0').TrimEnd('.');
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("���")].Text = totCost;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ҽ����ˮ��")].Text = order.ID;//2
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ҽ��״̬")].Text = order.Status.ToString();//�¿�������ˣ�ִ��
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��Ϻ�")].Text = order.Combo.ID.ToString();//4

            if (order.Frequency == null || string.IsNullOrEmpty(order.Frequency.ID))
            {
                order.Frequency = Components.Order.Classes.Function.GetDefaultFrequency().Clone();
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("Ƶ�α���")].Text = order.Frequency.ID.ToString();
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("Ƶ������")].Text = order.Frequency.Name;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("�÷�����")].Text = order.Usage.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("�÷�����")].Text = order.Usage.Name;//15

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("Ժע����")].Text = order.InjectCount.ToString();//36
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��ʼʱ��")].Value = order.BeginTime;//��ʼʱ��
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����ʱ��")].Value = order.MOTime;//����ʱ��


            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ִ�п��ұ���")].Text = order.ExeDept.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ִ�п���")].Text = order.ExeDept.Name;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("�Ӽ�")].Value = order.IsEmergency;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��鲿λ")].Value = order.CheckPartRecord;//��鲿λ
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��������")].Value = order.Sample.Name;//��������
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("�ۿ���ұ���")].Value = order.StockDept.ID;//�ۿ����
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("�ۿ����")].Value = order.StockDept.Name;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��ע")].Text = order.Memo;//20
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("¼���˱���")].Text = order.Oper.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("¼����")].Text = order.Oper.Name;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����ҽ��")].Text = order.ReciptDoctor.Name;//����ҽ��
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��������")].Text = order.ReciptDept.Name;//��������

            if (order.EndTime != DateTime.MinValue)
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ֹͣʱ��")].Value = order.EndTime;//ֹͣʱ�� 25
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ֹͣ�˱���")].Text = order.DCOper.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ֹͣ��")].Text = order.DCOper.Name;

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
                order.Frequency.Usage = order.Usage; //�÷�����
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("˳���")].Value = order.SortID;//28
            if (!this.EditGroup)
            {
                if (this.currentPatientInfo.Pact.PayKind.ID == "02")//����ҽ��-��ʾ���ñ���
                {
                    //string feeStr = "";

                    //if (order.Item.PriceUnit != "[������]")
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
                else//��ʾ��Ŀҽ�����
                {
                    //this.neuSpread1.Sheets[SheetIndex].RowHeader.Columns.Get(0).Width = 50F;
                    //if (order.Item.Price > 0 && order.OrderType.IsCharge) this.neuSpread1.Sheets[SheetIndex].RowHeader.Cells[i, 0].Text = Neusoft.HISFC.Components.Common.Classes.Function.ShowItemFlag(order.Item);
                }
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("Ƥ�Դ���")].Value = order.HypoTest;//28
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("Ƥ��")].Value = this.OrderManagement.TransHypotest(order.HypoTest);//28
            this.neuSpread1.Sheets[SheetIndex].Rows[rowIndex].Tag = order;

            this.dirty = false;

            if (order.Item.ItemType == EnumItemType.Drug
                && order.Item.SysClass.ID.ToString() != "PCC"
                && order.HerbalQty > 7)
            {
                Components.Order.Classes.Function.ShowBalloonTip(8, "����", "��" + order.Item.Name + "����������7��������\r\n��ע��ע�����ɣ�", ToolTipIcon.Warning);
            }

            return;
        }

        #endregion

        #region ��������{98522448-B392-4d67-8C4D-A10F605AFDA5}

        /// <summary>
        /// ȡ��󷽺�
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
        /// �����ͬ���ҽ��
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
                    //�ų��Լ�
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
        /// ˢ��ҽ��״̬
        /// </summary>
        /// <param name="row"></param>
        /// <param name="SheetIndex"></param>
        /// <param name="reset"></param>
        private void ChangeOrderState(int row, int SheetIndex, bool reset)
        {
            try
            {
                int i = GetColumnIndexFromName("ҽ��״̬");//this.GetColumnIndexFromName("ҽ��״̬");
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

                //������Ŀб����ʾ
                if (orderTemp.IsSubtbl)
                {
                    //this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("ҽ������")].Font = new Font(this.neuSpread1.Font.FontFamily.Name, this.neuSpread1.Font.Size, FontStyle.Italic, this.neuSpread1.Font.Unit, this.neuSpread1.Font.GdiCharSet);
                    this.neuSpread1.Sheets[SheetIndex].Rows[row].BackColor = Color.Silver;
                }
                else
                {
                    //this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("ҽ������")].Font = new Font(this.neuSpread1.Font.FontFamily.Name, this.neuSpread1.Font.Size, FontStyle.Regular, this.neuSpread1.Font.Unit, this.neuSpread1.Font.GdiCharSet);
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
                        ucOutPatientItemSelect1.MessageBoxShow("��ȡҽ��������Ŀʧ�ܣ�" + interfaceMgr.Err);
                        return;
                    }
                    if (compareItem != null)
                    {
                        if (!hsCompareItems.Contains(currentPatientInfo.Pact.ID + orderTemp.Item.ID))
                        {
                            hsCompareItems.Add(currentPatientInfo.Pact.ID + orderTemp.Item.ID, compareItem);
                        }

                        this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("ҽ������")].ForeColor = Color.Red;
                    }
                    else
                    {
                        this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("ҽ������")].ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }

        }

        /// <summary>
        /// ��ѯҽ��
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

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯҽ��,���Ժ�!");
            Application.DoEvents();

            this.hsOrder.Clear();

            //��ѯ����ҽ������
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

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾҽ��,���Ժ�!");
            Application.DoEvents();

            if (this.IsDesignMode)
            {
                tooltip.SetToolTip(this.neuSpread1, "����ҽ��");
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
        /// ������Ϣ
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
                //����ִ�п���Ϊ���߿���
                if (!this.EditGroup)
                    order.ExeDept = this.GetReciptDept().Clone();//{56D98B49-A27E-487f-B331-0B9CDB04D4ED}
                else
                    order.ExeDept = ((Neusoft.HISFC.Models.Base.Employee)this.OrderManagement.Operator).Dept.Clone();
            }
            if (order.ExeDept.Name == "" && order.ExeDept.ID != "")
                order.ExeDept.Name = this.deptHelper.GetName(order.ExeDept.ID);
            //����ҽ��
            if (order.ReciptDoctor == null || order.ReciptDoctor.ID == "")
                order.ReciptDoctor = this.GetReciptDoct().Clone();
            //��������
            if (order.ReciptDept == null || order.ReciptDept.ID == "")
                order.ReciptDept = this.GetReciptDept().Clone();

            if (order.Oper.ID == null || order.Oper.ID == "")
            {
                order.Oper.ID = this.OrderManagement.Operator.ID;
                order.Oper.Name = this.OrderManagement.Operator.Name;
            }

        }

        /// <summary>
        /// ��鿪����Ϣ����ʾ����
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

            ucOutPatientItemSelect1.MessageBoxShow(strMsg, "��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        /// <summary>
        /// ѡ��仯
        /// </summary>
        private void SelectionChanged()
        {
            #region ѡ��
            //ÿ��ѡ��仯ǰ���������ʾ
            this.ucOutPatientItemSelect1.Clear(false);

            #region old add by liuww 2012-06-07
            ////�¿��� ���ܸ���
            //if (int.Parse(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("ҽ��״̬")].Text) == 0)
            //{
            //    //����Ϊ��ǰ��
            //    this.ucOutPatientItemSelect1.CurrentRow = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            //    this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            //    this.currentOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
            //    this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;
            //    //���������ѡ��
            //    if (this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != "" && this.ucOutPatientItemSelect1.CurrOrder.Combo.ID != null)// && this.IsDesignMode)
            //    {
            //        int comboNum = 0;//��õ�ǰѡ������
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
            //            //ֻ��һ��
            //            if (OrderCanCancelComboChanged != null)
            //                this.OrderCanCancelComboChanged(false);//����ȡ�����
            //        }
            //        else
            //        {
            //            if (OrderCanCancelComboChanged != null)
            //                this.OrderCanCancelComboChanged(true);//����ȡ�����
            //        }
            //    }

            //    if (OrderCanSetCheckChanged != null)
            //        this.OrderCanSetCheckChanged(false);//��ӡ������뵥ʧЧ
            //}
            #endregion

            if (int.Parse(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("ҽ��״̬")].Text) == 0)
            {
                #region new add by liuww 2012-06-07
                this.currentOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
                this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;

                int comboNum = 0;//��õ�ǰѡ������
                
                //���������ѡ��
                if (this.currentOrder.Combo.ID != ""
                    && this.currentOrder.Combo.ID != null)//&& this.IsDesignMode)
                {

                    comboNum = this.SelectionAllChanged();


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
                    this.OrderCanSetCheckChanged(false);//��ӡ������뵥ʧЧ
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
        /// ѡ����Ϻ�
        /// </summary>
        /// <returns></returns>
        private int SelectionAllChanged()
        {
            int comboNum = 0;
            //����Ϊ��ǰ��
            this.ucOutPatientItemSelect1.CurrentRow = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;

            if (this.currentOrder.Combo.ID != ""
                  && this.currentOrder.Combo.ID != null)//&& this.IsDesignMode)
            {
                //��õ�ǰѡ������
                ///����Ѱ��
                for (int i = this.ActiveRowIndex; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    string strComboNo = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex).Combo.ID;

                    //string strComboNo = this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("��Ϻ�")].Text

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

                ///����Ѱ��
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
        /// ���ҽ��
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
                    string t = "";//��Ϻ� �޸ĳɶ�����Ϻ�
                    int injectNum = 0;//Ժ��ע����
                    int iSort = -1;
                    string time = "";
                    int kk = 0;

                    if (this.ValidComboOrder() == -1)
                        return;//У�����ҽ��

                    //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    int sameSubComb = 0;
                    Neusoft.HISFC.Models.Order.OutPatient.Order ord = null;
                    for (int rowIndex = 0; rowIndex < this.neuSpread1.Sheets[sheetIndex].Rows.Count; rowIndex++)
                    {
                        ord = this.GetObjectFromFarPoint(rowIndex, sheetIndex);
                        ord.SortID = rowIndex + 1;

                        this.neuSpread1.Sheets[sheetIndex].Cells[rowIndex, GetColumnIndexFromName("˳���")].Text = ord.SortID.ToString();
                        this.neuSpread1.Sheets[sheetIndex].Cells[rowIndex, GetColumnIndexFromName("˳���")].Value = ord.SortID;

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
                                #region ������Ѿ������ҽ������ϱ仯����Ҫɾ������

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
                            //Ժ��ע����
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
                    ucOutPatientItemSelect1.MessageBoxShow("��ѡ�������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// У�����ҽ��
        /// </summary>
        /// <returns></returns>
        private int ValidComboOrder()
        {
            if (!this.ynGetSysClassControl)
            {
                isDecSysClassWhenGetRecipeNO = ctrlMgr.GetControlParam<bool>(Neusoft.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, true, false);
            }

            Neusoft.HISFC.Models.Order.Frequency frequency = null;//Ƶ��
            Neusoft.FrameWork.Models.NeuObject usage = null;//�÷�
            Neusoft.FrameWork.Models.NeuObject exeDept = null;//ִ�п���
            decimal amount = 0;//����
            string sysclass = "-1";//���
            decimal days = 0;//��ҩ����
            string sample = "";//����
            decimal injectCount = 0;//Ժע����
            string jpNum = "";
            //��ҩ�ļ�ҩ��ʽ
            string PCCUsage = "";

            ArrayList alItems = new ArrayList();

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    if (o.ID != "")
                    {
                        //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        Neusoft.HISFC.Models.Order.OutPatient.Order tem = this.OrderManagement.QueryOneOrder(this.Patient.ID, o.ID);
                        if (tem.Status != 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(o.Item.Name + "�Ѿ��շѣ�����������ã�");
                            return -1;
                        }
                    }
                    if (o.Status != 0)
                    {
                        return -1;
                    }
                    if (o.Item.SysClass.ID.ToString() == "UL")//������Ŀ�ж��Ƿ���Բ��ܣ����ԵĲſ������
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
                            ucOutPatientItemSelect1.MessageBoxShow("Ƶ�β�ͬ������������ã�");
                            return -1;
                        }
                        if (o.InjectCount != injectCount)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("Ժע������ͬ������������ã�");
                            return -1;
                        }
                        //if (o.Item.IsPharmacy)		//ֻ��ҩƷ�ж��÷��Ƿ���ͬ
                        if (o.Item.ItemType == EnumItemType.Drug)		//ֻ��ҩƷ�ж��÷��Ƿ���ͬ
                        {
                            #region �÷��ж�

                            try
                            {
                                if (o.Item.SysClass.ID.ToString() != "PCC")
                                {
                                    if (!Classes.Function.IsSameUsage(o.Usage.ID, usage.ID))
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow("�÷���ͬ�������Խ�����ϣ�");
                                        return -1;
                                    }
                                    //Neusoft.HISFC.Models.Base.Const usageObj1 = Classes.Function.usageHelper.GetObjectFromID(o.Usage.ID) as Neusoft.HISFC.Models.Base.Const;
                                    //Neusoft.HISFC.Models.Base.Const usageObj2 = Classes.Function.usageHelper.GetObjectFromID(usage.ID) as Neusoft.HISFC.Models.Base.Const;

                                    //if (!string.IsNullOrEmpty(usageObj1.UserCode) && !string.IsNullOrEmpty(usageObj2.UserCode))
                                    //{
                                    //    if (usageObj1.UserCode.Trim() != usageObj2.UserCode.Trim())
                                    //    {
                                    //        ucOutPatientItemSelect1.MessageBoxShow("�÷���ͬ�������Խ�����ϣ�");
                                    //        return -1;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (o.Usage.ID != usage.ID)
                                    //    {
                                    //        ucOutPatientItemSelect1.MessageBoxShow("�÷���ͬ������������ã�");
                                    //        return -1;
                                    //    }
                                    //}
                                }
                                else
                                {
                                    if (o.Memo != PCCUsage)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow("��ҩ��ʽ��ͬ�������Խ�����ϣ�");
                                        return -1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                                //if (o.Usage.ID != usage.ID)
                                //{
                                //    ucOutPatientItemSelect1.MessageBoxShow("�÷���ͬ������������ã�");
                                //    return -1;
                                //}
                            }
                            #endregion

                            if (o.Item.SysClass.ID.ToString() == "PCC" || o.Item.SysClass.ID.ToString() == "C")
                            {
                                if (o.HerbalQty != days)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("��ҩ������ͬ������������ã�");
                                    return -1;
                                }
                            }
                            //if (o.ExtendFlag1 != jpNum)
                            //{
                            //    ucOutPatientItemSelect1.MessageBoxShow("��ƿ����ͬ������������ã�");
                            //    return -1;
                            //}
                        }
                        else
                        {
                            if (o.Item.SysClass.ID.ToString() == "UL")//����
                            {
                                if (o.Qty != amount)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("����������ͬ������������ã�");
                                    return -1;
                                }
                                if (o.Sample.Name != sample)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("����������ͬ������������ã�");
                                    return -1;
                                }
                            }
                        }

                        if (isDecSysClassWhenGetRecipeNO)
                        {
                            if ("PCZ,P".Contains(o.Item.SysClass.ID.ToString()) &&
                                "PCZ,P".Contains(sysclass))
                            {
                                //��ҩ�ͳ�ҩ�������
                            }
                            else
                            {
                                if (o.Item.SysClass.ID.ToString() != sysclass)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("ϵͳ���ͬ������������ã�");
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            if (o.Item.SysClass.ID.ToString() != sysclass)
                            {
                                ucOutPatientItemSelect1.MessageBoxShow("��Ŀ���ͬ������������ã�");
                                return -1;
                            }
                        }

                        if (o.ExeDept.ID != exeDept.ID)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("ִ�п��Ҳ�ͬ���������ʹ��!", "��ʾ");
                            return -1;
                        }

                    }
                }
            }

            ////if (alItems.Count > 0)
            ////{
            ////    if (!fun.IsComboLab(alItems))
            ////    {
            ////        ucOutPatientItemSelect1.MessageBoxShow("������Ŀ�����ϲ��ܹ���,�������!", "��ʾ");
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
        /// ���Ժ��ע�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isCanModifiedInjectNum">�Ƿ�����޸�Ժע</param>
        private void AddInjectNum(Neusoft.HISFC.Models.Order.OutPatient.Order sender, bool isCanModifiedInjectNum)
        {
            //��ʱû�������÷�������ʲô��ĿֻҪ�����˴��÷����ո���

            if (!Classes.Function.CheckIsInjectUsage(sender.Usage.ID))
            {
                return;
            }

            //����Ժע�÷�����
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
                //����Ĭ�ϵ�Ժע����Ϊ����/ÿ����
                int injectNumTmp = Neusoft.FrameWork.Function.NConvert.ToInt32(sender.Item.Qty * ((Neusoft.HISFC.Models.Pharmacy.Item)sender.Item).BaseDose / sender.DoseOnce);
                formInputInjectNum.InjectNum = injectNumTmp;
                #endregion
                DialogResult r = ucOutPatientItemSelect1.MessageBoxShow("��ҩƷ�Ƿ�ΪԺ��ע�䣿", "[��ʾ]", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                    order.NurseStation.User02 = "C";//�޸Ĺ�Ժע

                    #region ֻҪ�Ǳ������ҽ�������Ժע����Ҫɾ��ԭ���ĸ���{F67E089F-1993-4652-8627-300295AAED8C}

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
        /// �жϷ�ҩҩ����ִ�п���
        /// ���治�ٵ��ô˷���
        /// </summary>
        /// <param name="pManager"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private int CheckOrderStockDeptAndExeDept(Neusoft.HISFC.BizProcess.Integrate.Pharmacy pManager, ref Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {

            //if (order.Item.IsPharmacy)
            if (order.Item.ItemType == EnumItemType.Drug)
            {
                //�����FillPharmacyItem ��������ȡҩƷ������Ϣ
                //Neusoft.HISFC.Models.Pharmacy.Item tempItem = null;

                //tempItem = pManager.GetItem(order.Item.ID);

                //if (tempItem == null || tempItem.IsStop)
                //{
                //    ucOutPatientItemSelect1.MessageBoxShow("ҩƷ:" + tempItem.Name + "�Ѿ�ͣ��", "��ʾ");
                //    return -1;
                //}

                Neusoft.HISFC.Models.Order.OutPatient.Order temp = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                temp.Item = order.Item;
                temp.ReciptDept = order.ReciptDept;
                temp.Patient.Pact = this.currentPatientInfo.Pact;
                temp.Patient.Birthday = this.currentPatientInfo.Birthday;

                #region ��������ָ��Ĭ��ȡҩҩ�� {ABCC78F9-826F-4f03-BB4E-1FDE2A494E1C}

                if (Classes.Function.FillPharmacyItem(pManager, ref temp) == -1)
                {
                    return -1;
                }

                //���������λ�ǰ�װ��λ����ϰ�װ��������Ϊ����ж�������С��λ�����жϵ�{3AD5A0FA-AFE4-41d9-AEDC-8A389D1424C9}
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
        /// Ԥ�ۿ��
        /// </summary>
        /// <param name="pManager"></param>
        /// <param name="qty">1ʱ�����룻-1ʱ��ɾ��</param>
        /// <returns></returns>
        private int UpdateStockPre(Neusoft.HISFC.BizProcess.Integrate.Pharmacy pManager, Neusoft.HISFC.Models.Order.OutPatient.Order order, decimal qty, ref string errInfo)
        {
            if (order.Item.ItemType == EnumItemType.Drug)
            {
                Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut = new Neusoft.HISFC.Models.Pharmacy.ApplyOut();
                applyOut.ID = order.ID;
                applyOut.StockDept.ID = order.StockDept.ID;
                applyOut.SystemType = "O1";//����ҽ������
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
        /// ȡ����ͬ��Ϻŵ�ҽ����Ŀ��ͬʱ����ʱ������ɾ��
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
        /// ������Ʒ��ҽ���������Ƴ�ҽ��
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
        /// ����ҽ��˳���
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
        /// ���и���
        /// </summary>
        ArrayList alSubOrders = new ArrayList();

        /// <summary>
        /// ��ȡ���������е�ҽ���б�
        /// </summary>
        /// <returns></returns>
        private decimal GetAllFee(ref ArrayList alFeeDetail)
        {
            try
            {
                #region ��seeno�������
                if (isCountFeeBySeeNo)
                {
                    #region ��ȡ����ҽ��

                    ArrayList alOrder = new ArrayList();

                    Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order();

                    for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                    {
                        order = this.GetObjectFromFarPoint(i, 0);

                        order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                        if (order.ID == "") //new �¼ӵ�ҽ��
                        {
                            alOrder.Add(order);
                        }
                        else //update ���µ�ҽ��
                        {
                            #region �����Ҫ���µ�ҽ��
                            //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                            //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
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
                        #region �������

                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                        {
                            if (orderObj.MinunitFlag == "")//user03Ϊ��,˵����֪��������ʲô��λ Ĭ��Ϊ��С��λ
                            {
                                orderObj.MinunitFlag = "1";//Ĭ��
                            }
                            if (orderObj.MinunitFlag != "1")//������С��λ 
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
                #region ��ReciptSequence�������
                else
                {
                    #region ��ȡ����ҽ��

                    ArrayList alOrder = new ArrayList();

                    Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order();

                    for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                    {
                        order = this.GetObjectFromFarPoint(i, 0);

                        order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                        if (order.ID == "") //new �¼ӵ�ҽ��
                        {
                            alOrder.Add(order);
                        }
                        else //update ���µ�ҽ��
                        {
                            #region �����Ҫ���µ�ҽ��
                            //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                            //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
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
                        #region �������

                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                        {
                            if (orderObj.MinunitFlag == "")//user03Ϊ��,˵����֪��������ʲô��λ Ĭ��Ϊ��С��λ
                            {
                                orderObj.MinunitFlag = "1";//Ĭ��
                            }
                            if (orderObj.MinunitFlag != "1")//������С��λ 
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
                            if (orderObj.MinunitFlag == "")//user03Ϊ��,˵����֪��������ʲô��λ Ĭ��Ϊ��С��λ
                            {
                                orderObj.MinunitFlag = "1";//Ĭ��
                            }
                            item = Classes.Function.ChangeToFeeItemList(orderObj);

                            alFeeDetail.Add(item);

                            //if (orderObj.MinunitFlag != "1")//������С��λ 
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
        /// �Ƿ�����ʾ���ñ���
        /// </summary>
        bool isShowFeeWarning = true;

        /// <summary>
        /// ҽ��������ʾ��
        /// </summary>
        /// <param name="isShowSIFeeInfo">�Ƿ���ʾҽ��������Ϣ������Ч�������������������㣬����Ͳ�ѯ��ʱ����ʾ</param>
        /// <param name="isRequery">�������շѱ�����Ϣ���Ƿ����²�ѯ��������Ŀʱ�����²�ѯ</param>
        private void SetOrderFeeDisplay(bool isShowSIFeeInfo, bool isRequery)
        {
            if (!this.EditGroup && this.currentPatientInfo != null)
            {
                if (this.currentPatientInfo.ID.Length > 0)
                {
                    //this.pnDisplay.Visible = true;
                    //{047C2448-B3D3-49eb-A40B-DF75749A4245}
                    lblDisplay.Text = "�����ţ�" + this.currentPatientInfo.PID.CardNO.TrimStart('0') + "������" + this.currentPatientInfo.Name + "  �Ա�" + this.currentPatientInfo.Sex.Name +
                        "  ���䣺" + this.OrderManagement.GetAge(this.currentPatientInfo.Birthday);
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

                    //�ܽ��
                    decimal decTotalMoney = 0;
                    //�������
                    decimal decPubMoney = 0;
                    //�Էѽ��
                    decimal decOwnMoney = 0;
                    //������
                    decimal decRebateMoney = 0;

                    // �ۼ��ܽ��
                    decimal decTotalMoneyAddUp = 0;
                    // �ۼƱ������
                    decimal decPubMoneyAddUp = 0;
                    // �ۼ��Էѽ��
                    decimal decOwnMoneyAddUp = 0;
                    // �ۼƼ�����
                    decimal decRebateMoneyAddUp = 0;

                    string errInfo = "";
                    int rev = -1;

                    //�Ż�Ч��ʱ���������Σ���ʱʵʱ��ʾ���ý���
                    //if (isShowSIFeeInfo)
                    //{
                    if (alFeeList != null && alFeeList.Count > 0)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                        //���ڷ������� ҽ���ӿڼ������ǳ�����������ֻ����ʾ����
                        //���ڳ���Ĳ��ٴ���ֱ�Ӱ����ܷ�����ʾ

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
                        this.lblDisplay.Text = "�˻���" + this.vacancyDisplay;
                    }
                    else
                    {
                        lblDisplay.Text = "";
                    }

                    //�����ۼƺ����н��ı�����������>0 �͸�����ʾ
                    if (decPubMoneyAddUp + decRebateMoneyAddUp + decPubMoney + decRebateMoney > 0)
                    {
                        this.pnTop.Height = 69;
                        lblDisplay.Text += "�����ţ�" + this.currentPatientInfo.PID.CardNO.TrimStart('0') + "  ������" + this.currentPatientInfo.Name + "  �Ա�" + this.currentPatientInfo.Sex.Name +
                           "  ���䣺" + this.OrderManagement.GetAge(this.currentPatientInfo.Birthday);
                        this.lblFeeDisplay.Text = "�����ܶ�:" + decTotalMoney.ToString("F4").TrimEnd('0').TrimEnd('.') +
                            "Ԫ �Էѽ��:" + (decOwnMoney - decRebateMoney).ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "Ԫ �������:" + decPubMoney.ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "Ԫ ������:" + decRebateMoney.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ \r\n" +

                           "�����ۼƷ����ܶ�:" + decTotalMoneyAddUp.ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "Ԫ �ۼ��Էѽ��:" + (decOwnMoneyAddUp - decRebateMoneyAddUp).ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "Ԫ �ۼƱ������:" + decPubMoneyAddUp.ToString("F4").TrimEnd('0').TrimEnd('.') +
                           "Ԫ �ۼƼ�����:" + decRebateMoneyAddUp.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ";

                        if (!isShowFeeWarning)
                        {
                            //�˴�������ʾ�����޶��
                            if (rev == 2 && !string.IsNullOrEmpty(errInfo))
                            {
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        lblDisplay.Text += "�����ţ�" + this.currentPatientInfo.PID.CardNO.TrimStart('0') +
                            "  ������" + this.currentPatientInfo.Name + "  �Ա�" + this.currentPatientInfo.Sex.Name +
                            "  ���䣺" + this.OrderManagement.GetAge(this.currentPatientInfo.Birthday) +
                               "  �����ܶ" + totcost.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ ";
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
        /// �޸Ĳ�ҩ{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        public void ModifyHerbal()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                return;
            }

            ArrayList alModifyHerbal = new ArrayList(); //Ҫ�޸ĵĲ�ҩҽ��

            Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as
                Neusoft.HISFC.Models.Order.OutPatient.Order;

            if (orderTemp == null)
            {
                return;
            }

            //{F1706DB9-376D-433e-A5A9-1E1EEA46733C}  �����޸Ĳ�ҩҽ��
            if (orderTemp.Item.ItemType == EnumItemType.Drug)
            {
                if (((Neusoft.HISFC.Models.Pharmacy.Item)orderTemp.Item).SysClass.ID.ToString() != "PCC")
                {
                    ucOutPatientItemSelect1.MessageBoxShow("��ѡ���ҩҽ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        Neusoft.FrameWork.WinForms.Classes.Function.Msg("ҽ������Ч�������޸ģ�\n�븴��ҽ��������ҽ�����޸ģ�", 411);
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
                    Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
                    uc.AlOrder = alModifyHerbal;
                    uc.OpenType = Neusoft.HISFC.Components.Order.Controls.EnumOpenType.Modified; //�޸�
                    uc.SetFocus();
                    DialogResult r = Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                    if (uc.IsCancel == true)
                    {
                        //ȡ����
                        return;
                    }

                    if (uc.OpenType == Neusoft.HISFC.Components.Order.Controls.EnumOpenType.Modified)
                    {
                        //��Ϊ�¼�ģʽ�Ͳ�ɾ����
                        if (this.Del(this.neuSpread1.ActiveSheet.ActiveRowIndex, true) < 0)
                        {
                            //ɾ��ԭҽ�����ɹ�
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
        /// ���״洢
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
                        ucOutPatientItemSelect1.MessageBoxShow("���ҽ������");
                    }
                    else
                    {
                        string s = order.Item.Name;
                        string sno = order.Combo.ID;
                        //����ҽ������ Ĭ�Ͽ���ʱ��Ϊ ���
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
        /// �������Ҽ����Ƶ�ǰ����
        /// </summary>
        /// <param name="selectRegister">��ǰ���߹Һ�ʵ��</param>
        /// <param name="copyNum">���ƴ���</param>
        public void CopyRecipeByPatientTree(Neusoft.HISFC.Models.Registration.Register selectRegister, int copyNum)
        {
            #region ��ȡ����ҽ��

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

            #region ����
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڸ���ҽ�������Ժ󡣡���");
            Application.DoEvents();
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //���ԭ����ҽ��
            ArrayList alTemp = new ArrayList();
            //����Ѿ��ı���ҽ��
            ArrayList alNewOrder = new ArrayList();
            //��ŷ�����Ϣ
            ArrayList alFeeItem = new ArrayList();
            //�����ж���Ϻţ���Ϻ�Ӧ��ȫ�����ɣ���Ҫ����ԭ��ͬ���ҩ�������µ���Ϻź�Ҳͬ��
            Hashtable hsCombNo = new Hashtable();
            //������ԭ����Ϻ�
            string combTemp = "";

            DateTime now = OrderManagement.GetDateTimeFromSysDateTime();

            string errText = "";
            #endregion

            //ѭ�����ƴ���
            for (int i = 0; i < copyNum; i++)
            {
                //ȡ����ȡ��ҽ�������ı�����һЩ��Ϣ�����棬�����µ�ҽ��
                alTemp = alOrder;
                alNewOrder = new ArrayList();
                alFeeItem = new ArrayList();
                hsCombNo = new Hashtable();
                int seeNO = this.OrderManagement.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//����µĿ������
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order newOrder in alTemp)
                {
                    #region �ı���ҽ����һЩ����,���뵽�µ�ҽ������
                    newOrder.SeeNO = seeNO.ToString();
                    newOrder.ID = Classes.Function.GetNewOrderID(ref errText);

                    if (newOrder.ID == "")
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    newOrder.ReciptNO = "";
                    newOrder.ReciptSequence = "";
                    newOrder.Status = 0;
                    newOrder.IsHaveCharged = false;
                    combTemp = newOrder.Combo.ID;
                    //��ϣ��key��ԭ������Ϻţ�value������Ϻ�
                    if (hsCombNo.Contains(combTemp))
                    {
                        newOrder.Combo.ID = hsCombNo[combTemp].ToString();
                    }
                    else
                    {
                        newOrder.Combo.ID = this.OrderManagement.GetNewOrderComboID();//�����Ϻ�
                        hsCombNo.Add(combTemp, newOrder.Combo.ID);
                    }
                    alNewOrder.Add(newOrder);
                    #endregion

                    #region ����Ԥ�ۿ��{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                    if (isPreUpdateStockinfo)
                    {
                        if (this.UpdateStockPre(phaIntegrate, newOrder, 1, ref errInfo) == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    #endregion

                    #region ת������ʵ��
                    Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemListTmp = Classes.Function.ChangeToFeeItemList(newOrder);
                    if (feeItemListTmp == null)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("ҩƷ��" + order.Item.Name + "��ҽ��ʵ��ת���ɷ���ʵ�����", "��ʾ");
                        return;
                    }
                    alFeeItem.Add(feeItemListTmp);
                    #endregion

                }
                #region �շ�
                //�����ź���ˮ�Ź����ɷ���ҵ��㺯��ͳһ����
                try
                {
                    bool bReturn = feeManagement.SetChargeInfo(this.Patient, alFeeItem, now, ref errText);
                    if (bReturn == false)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(errText + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region ���������ź��շ����к�

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

                #region ����ҽ�� �������´�����

                for (int j = 0; j < alOrder.Count; j++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = alNewOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                    if (temp == null)
                    {
                        continue;
                    }

                    #region ����ҽ����
                    if (OrderManagement.UpdateOrder(temp) == -1) //����ҽ����
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("����ҽ������" + temp.Item.Name + "�����Ѿ��շ�,���˳������������½���!");
                        return ;
                    }
                    #endregion
                }
                #endregion

            }

            #region �ύ
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //���ڲ��Һŵģ�����ɹ����ܸ������շѱ��
            if (!this.Patient.IsFee)
            {
                this.Patient.IsFee = true;
            }

            //���»���״̬Ϊ����󣬸��Ļ�����Ϣ�л��߿���״̬
            this.Patient.IsSee = true;

            #endregion
        }

        /// <summary>
        /// �������Ҽ�ɾ����ǰ����
        /// </summary>
        /// <param name="selectRegister">��ǰ���߹Һ�ʵ��</param>
        public void DeleteRecipeByPatientTree(Neusoft.HISFC.Models.Registration.Register selectRegister)
        {
            ArrayList alAllOrder = new ArrayList();
            Neusoft.HISFC.Models.Order.OutPatient.Order orderOne = new Neusoft.HISFC.Models.Order.OutPatient.Order();
            try
            {
                //��ȡ����ҽ��
                for (int i = this.neuSpread1_Sheet1.Rows.Count - 1; i >= 0; i--)
                {
                    orderOne = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.ActiveSheet.Rows[i].Tag;
                    if (orderOne != null)
                    {
                        alAllOrder.Add(orderOne);
                    }

                }

                #region �ж��Ƿ����շѣ������һ�����շѾͲ�����ɾ�����Ŵ���
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
                    ucOutPatientItemSelect1.MessageBoxShow(this, "�ô����Ѿ�ȫ���򲿷��շѣ�����������ťɾ��δ�շѵ�ҽ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region �жϿ���ҽ���Ƿ���ͬ�������һ��ҽ����ͬ������ɾ�����Ŵ���
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
                    ucOutPatientItemSelect1.MessageBoxShow(this, "�ô�����ȫ���򲿷�Ϊ����ҽ������������������ťɾ���Լ�������ҽ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region ��ȡ��Ҫɾ����ҽ��ҽ��
                Neusoft.HISFC.Models.Order.OutPatient.Order orderTemp = null;
                hsDeleteOrder = new Hashtable();
                if (this.neuSpread1.ActiveSheet.RowCount == 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("����ѡ��һ��ҽ����");
                    return ;
                }
                for (int i = this.neuSpread1_Sheet1.Rows.Count - 1; i >= 0; i--)
                {
                    orderTemp = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.ActiveSheet.Rows[i].Tag;

                    if (orderTemp == null)
                    {
                        continue;
                    }

                    if (orderTemp.ID == "") //��Ȼɾ��
                    {
                        this.neuSpread1.ActiveSheet.Rows.Remove(i, 1);
                    }

                    //�˴�ֻ�Ǽ�¼��Ҫɾ����ҽ��ID
                    else //delete from table
                    {
                        //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
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

                #region ɾ��ҽ��
                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("����ɾ��ҽ�������Ժ󡣡���");
                Application.DoEvents();
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                errInfo = "";
                if (this.DelCommit(ref errInfo) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("ɾ��ҽ��ʧ�ܣ�" + errInfo);
                    return ;
                }

                Neusoft.FrameWork.Management.PublicTrans.Commit();
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion
            }
            catch (Exception ex)
            {
                //���쳣�ˣ��������������
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return;
            }

            return ;

        }

        #endregion




        #region ���з���

        /// <summary>
        /// ������Ŀѡ������
        /// </summary>
        public void AddGroupOrder(ArrayList alOrders)
        {
            Classes.LogManager.Write("����ʼ������״�����");
            ArrayList alHerbal = new ArrayList(); //��ҩ

            ArrayList alAddOrder = new ArrayList();
            this.neuSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            Classes.LogManager.Write("���������һ��" + alOrders.Count.ToString() + "����Ŀ��");
            int i=0;
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrders)
            {
                i++;
                if (!EditGroup)
                {
                    if (this.Patient != null && IsDesignMode)
                    {
                        #region �жϿ���Ȩ��

                        string error = "";

                        int ret = 1;

                        //����Ȩ
                        ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, this.GetReciptDoct(),
                            this.GetReciptDept(), Neusoft.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref error);

                        if (ret <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(error, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        //����ʷ�ж�
                        ret = Components.Order.Classes.Function.JudgePatientAllergy("1", this.Patient.PID, order, ref error);

                        if (ret <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(error, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                ucOutPatientItemSelect1.MessageBoxShow(IBeforeAddItem.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //return;
                                continue;
                            }
                        }
                    }
                }

                if (order.Item.SysClass.ID.ToString() == "PCC") //��ҩ
                {
                    Components.Order.Classes.Function.ReComputeQty(order);

                    alHerbal.Add(order);
                }
                else
                {
                    this.AddNewOrder(order, 0);
                    Classes.LogManager.Write("����ӵ�" + i.ToString() + "����Ŀ��");
                }
            }
            if (alHerbal.Count > 0)
            {
                this.AddHerbalOrders(alHerbal);
            }
            Classes.LogManager.Write("������ˢ�´���״̬��");
            this.RefreshOrderState();
            Classes.LogManager.Write("������ˢ�´���״̬��");
            Classes.LogManager.Write("����ʼˢ�´�����Ϻš�");
            this.RefreshCombo();
            Classes.LogManager.Write("������ˢ�´�����Ϻš�");
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            Classes.LogManager.Write("������ͳһ��������Ϣ��\r\n");
        }

        /// <summary>
        /// ���ҽ��ʵ���FarPoint
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
                if (string.IsNullOrEmpty(this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("ҽ����ˮ��")].Text))
                {
                    return null;
                }

                if (this.hsOrder.Contains(this.Patient.DoctorInfo.SeeNO + this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("ҽ����ˮ��")].Text))
                {
                    order = this.hsOrder[this.Patient.DoctorInfo.SeeNO + this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("ҽ����ˮ��")].Text] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                }
                else
                {
                    //����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                    order = OrderManagement.QueryOneOrder(this.Patient.ID, this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("ҽ����ˮ��")].Text);
                }
            }

            return order;
        }

        /// <summary>
        /// ҩƷ�Զ�����
        /// </summary>
        Hashtable hsPhaUserCode = new Hashtable();

        /// <summary>
        /// �����ҽ��
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

                //�����������ҩƷ��Ŀ����Ϊ�㣬ϵͳ������¼�����Ĭ����ʾΪ1��ҽ���ڲ��޸ĵ�����±��棬��ʾ����Ϊ0
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

                //�˴��ж�ͣ�õ��÷�����ֵ
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
                    newOrder.MinunitFlag = "1";//��С��λ
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
            //�������
            if (sender.GetType() == typeof(Neusoft.HISFC.Models.Order.OutPatient.Order))
            {
                #region �����ӵĶ���
                if (((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "UC")//���
                {
                    //��ӡ������뵥
                    ////this.AddTest(sender);
                }
                else if (((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "MC")//����
                {
                    //��ӻ�������
                    ////this.AddConsultation(sender);
                }

                #region Ƥ��
                if (((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.ItemType == EnumItemType.Drug)//ҩƷ
                {
                    if (((Neusoft.HISFC.Models.Pharmacy.Item)((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item).IsAllergy)
                    {
                        //���Ʋ��������Ƿ�Ĭ��ȫ��Ժע��ȫ��Ժע���ڵ���Ժע���������
                        if (!this.isCanModifiedInjectNum)
                        {
                            if (this.hypotestMode == "1")
                            {
                                if (ucOutPatientItemSelect1.MessageBoxShow(((Neusoft.HISFC.Models.Order.OutPatient.Order)sender).Item.Name + "�Ƿ���ҪƤ�ԣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                    order.MinunitFlag = "1";//��С��λ
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

                #region ���»�ȡ�ۿ����
                // ���׿����ۿ���ҿ���Ϊ��
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

                    //���ڴ���Ŀ��ѡ���ҩƷ���Ѿ�����ȡҩ���ҵģ�������ȡ houwb 2011-5-30
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
                        //�ж�ҩƷ�Ƿ���ҩ������ʾ
                        if (((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).Quality.ID.Contains("S") ||
                            ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).Quality.ID.Contains("P"))
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("��" + order.Item.Name + "�����ڶ���ҩƷ��\r\n���ݴ�������취�涨,��ͬʱ���ӿ����ֹ�����ҩ����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                #endregion

                if (order.Combo.ID == "")
                {
                    try
                    {
                        order.Combo.ID = this.OrderManagement.GetNewOrderComboID();//�����Ϻ�
                    }
                    catch
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("���ҽ����Ϻų���\r\n" + OrderManagement.Err);
                    }
                }

                DateTime dtNow = this.OrderManagement.GetDateTimeFromSysDateTime();

                if (!this.EditGroup)
                {
                    if (this.currentPatientInfo != null)
                    {
                        order.InDept = this.currentPatientInfo.DoctorInfo.Templet.Dept;//�Һſ���
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

                #region ����ҽ������ʱ��

                //if (Order.Classes.Function.IsDefaultMoDate == false)
                //{
                //    if (dtNow.Hour >= 12)
                //        order.BeginTime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 12, 0, 0);
                //    else
                //        order.BeginTime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
                //}
                //else//��Ĭ��ʱ��
                //{
                //    order.BeginTime = dtNow;
                //}

                order.BeginTime = Order.Classes.Function.GetDefaultMoBeginDate(3);

                if (order.User03 != "")//���׵�ʱ����
                {
                    int iDays = Neusoft.FrameWork.Function.NConvert.ToInt32(order.User03);
                    if (iDays > 0)//��ʱ����>0
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

                //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                order.SubCombNO = GetMaxSubCombNo(order);

                #region ���´���Ժע��������֤���׿���Ĭ��Ϊ���Ժע

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
                ucOutPatientItemSelect1.MessageBoxShow("������Ͳ���ҽ�����ͣ�");
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
                            ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + errText);
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
        /// ��Ӳ�ҩҽ��{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        /// <param name="alHerbalOrder"></param>
        public void AddHerbalOrders(ArrayList alHerbalOrder)
        {

            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} //��ҩ������ҩ��������
            using (Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder uc = new Neusoft.HISFC.Components.Order.Controls.ucHerbalOrder(true, Neusoft.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
            {
                uc.Patient = new Neusoft.HISFC.Models.RADT.PatientInfo();//
                uc.IsClinic = true;

                Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
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

        //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} �㼶��ʽ����ҽ�� yangw 20101024
        public void AddLevelOrders()
        {
            using (Neusoft.HISFC.Components.Order.Controls.ucLevelOrder uc = new Neusoft.HISFC.Components.Order.Controls.ucLevelOrder())
            {
                uc.InOutType = 1;
                uc.Patient = new Neusoft.HISFC.Models.RADT.PatientInfo();

                Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "������ҽ������";
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
        /// �����������
        /// </summary>
        public void AddOperation()
        {
            ////���޸�
        }



        /// <summary>
        /// ��ȡѡ���ҽ��
        /// </summary>
        /// <returns></returns>
        private List<Neusoft.HISFC.Models.Order.Order> GetSelectOrders()
        {
            List<Neusoft.HISFC.Models.Order.Order> alOrders = new List<Neusoft.HISFC.Models.Order.Order>();
            int iActiveSheet = 0;//��鵥Ĭ����ʱҽ��
            for (int i = 0; i < this.neuSpread1.Sheets[iActiveSheet].RowCount; i++)
            {
                if (this.neuSpread1.Sheets[iActiveSheet].IsSelected(i, 0))
                {
                    //��alItems���ݸ�Ϊorder����
                    alOrders.Add(this.GetObjectFromFarPoint(i, iActiveSheet));
                }
            }

            return alOrders;
        }

        /// <summary>
        /// ��Ӽ�顢��������
        /// </summary>
        public void AddTest()
        {
            if (this.Patient == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("����ѡ���ߣ�");
                return;
            }

            List<Neusoft.HISFC.Models.Order.Order> alItems = this.GetSelectOrders();

            if (alItems.Count <= 0)
            {
                //û��ѡ����Ŀ��Ϣ
                ucOutPatientItemSelect1.MessageBoxShow("��ѡ�����ļ����Ϣ!");
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
                    ucOutPatientItemSelect1.MessageBoxShow("��ýӿ�IcheckPrint����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿڼ���ӿ�IcheckPrint\n����ϵͳ����Ա��ϵ��");
                    return;
                }
            }
            this.checkPrint.Reset();
            this.checkPrint.ControlValue(Patient, alInOrders);
            this.checkPrint.Show();
        }

        /// <summary>
        /// ��ӻ���
        /// </summary>
        /// <param name="sender"></param>
        public void AddConsultation(object sender)
        {
            ////���޸�
        }

        RowCompare rowCompare = new RowCompare();

        ///<summary>
        /// ˢ�����
        /// </summary>
        public void RefreshCombo()
        {
            Neusoft.HISFC.Models.Order.OutPatient.Order ord = null;
            try
            {
                //����ѡ��ͬ����Ŀ
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
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Text = hsSubCombNo[ord.SubCombNO].ToString();
                    }

                    if (i == this.neuSpread1.ActiveSheet.ActiveRowIndex)
                    {
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag = "����";
                        currentCombNo = ord.Combo.ID;
                    }
                    else
                    {
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag = null;
                    }
                }

                if (this.neuSpread1.Sheets[0].Rows.Count > 0)
                {
                    this.neuSpread1.Sheets[0].SortRows(GetColumnIndexFromName("˳���"), true, false, rowCompare);
                    Order.Classes.Function.DrawCombo(this.neuSpread1.Sheets[0], GetColumnIndexFromName("��Ϻ�"), GetColumnIndexFromName("���"));
                }

                int sortID = 1;

                //for (int i = this.neuSpread1.ActiveSheet.RowCount - 1; i >= 0; i--)
                for (int i = 0; i <= this.neuSpread1.ActiveSheet.RowCount - 1; i++)
                {
                    ord = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    ord.SortID = sortID;
                    this.AddObjectToFarpoint(ord, i, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                    this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Text = sortID.ToString();
                    sortID++;

                    if (this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag != null && this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag.ToString() == "����")
                    {
                        this.neuSpread1.ActiveSheet.ActiveRowIndex = i;
                        this.ucOutPatientItemSelect1.CurrentRow = i;
                        this.ActiveRowIndex = i;
                        this.currentOrder = ord;
                        this.neuSpread1.ActiveSheet.AddSelection(i, 0, 1, this.neuSpread1.ActiveSheet.ColumnCount);
                        this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag = null;
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
        /// ����ҽ��״̬
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
                ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("ˢ��ҽ��״̬ʱ���ֲ���Ԥ֪�������˳������������Ի������Ա��ϵ"));
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
        /// ˢ��ҽ��״̬
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
                ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("ˢ��ҽ��״̬ʱ���ֲ���Ԥ֪�������˳������������Ի������Ա��ϵ"));
            }
        }

        /// <summary>
        /// ���ҽ���Ϸ���
        /// </summary>
        /// <returns></returns>
        public int CheckOrder()
        {
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            bool IsModify = false;

            ///�Ƿ��������
            bool isHaveSublOrders = false;

            //����������ʾ
            string exceedWarning = "";

            //��ʱҽ��
            for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            {
                order = (Neusoft.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                // 2011-11-14
                // ���㸽����ʾ��ÿ�α��涼��ʾ
                //if (order.IsSubtbl && order.Memo != "�Һŷ�")
                //{
                //    isHaveSublOrders = true;
                //}

                if (order.Status == 0)
                {
                    if (order.Item.ID == "999")
                    {
                        continue;
                    }

                    //���������������+��Ŀ��ˮ�ţ���Ϊ��ֵ
                    if (!this.hsOrder.Contains(order.SeeNO + order.ID) && !(string.IsNullOrEmpty(order.SeeNO) || string.IsNullOrEmpty(order.ID)))
                    {
                        this.hsOrder.Add(order.SeeNO + order.ID, order);
                    }
                    //δ��˵�ҽ��
                    IsModify = true;
                    //if (order.Item.IsPharmacy)
                    #region ҩƷ
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {

                        #region ����ʱ�ж��Ƿ�ͣ�á�ȱҩ
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
                                ShowErr("ҩƷ��" + order.Item.Name + "��ÿ���������������ֵ " + doceOnceLimit.ToString() + "�����޸ģ�\r\n������������ϵϵͳ����Ա��\r\n", i, 0);
                                return -1;
                            }
                        }


                        #region ��ȡҩƷ������Ϣ
                        order.Item.MinFee = drugItem.MinFee;
                        //�˴�ȡ����{B9303CFE-755D-4585-B5EE-8C1901F79450}
                        //order.Item.Price = item.Price;
                        //{B9303CFE-755D-4585-B5EE-8C1901F79450} ����ԭ���Ĺ����
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice = drugItem.Price;
                        order.Item.Price = Classes.Function.GetPrice(order, drugItem, order.Patient, this.Patient.Pact, true);
                        order.Item.Name = drugItem.Name;
                        order.Item.SysClass = drugItem.SysClass.Clone();//����ϵͳ���
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = drugItem.IsAllergy;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = drugItem.PackUnit;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = drugItem.MinUnit;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = drugItem.BaseDose;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = drugItem.DosageForm;
                        ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).SplitType = drugItem.SplitType;

                        #endregion

                        //ҩƷ
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            //�в�ҩ
                            if (order.HerbalQty == 0)
                            {
                                ShowErr("ҩƷ��" + order.Item.Name + "����������Ϊ�㣡", i, 0); return -1;
                            }
                            //��ҩҪ��֤��ÿ����*����=����
                            //��ҩ���ǰ�����С��λ���װ��λ��ҩ����������λ��һ������С��λһ�£��������Ʋ�ҩֻ�ܿ�����С��λ
                            //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                            //if (order.MinunitFlag == "0") //��װ��λ
                            //{
                            //    //��װ��λ ����*��װ��λ*��������=ÿ����*����
                            //    if (order.Item.Qty != Math.Round((order.DoseOnce / drugItem.BaseDose * order.HerbalQty) / drugItem.PackQty, 2))
                            //    {
                            //        ShowErr("ҩƷ��" + order.Item.Name + "����������ȷ��", i, 0);
                            //        return -1;
                            //    }
                            //}
                            ////��С��λ
                            //else
                            //{
                            //    //��С��λ  ����*��������=ÿ����*����
                            //    if (order.Item.Qty != Math.Round(order.DoseOnce / drugItem.BaseDose * order.HerbalQty, 2))
                            //    {
                            //        ShowErr("ҩƷ��" + order.Item.Name + "����������ȷ��", i, 0);
                            //        return -1;
                            //    }
                            //}
                        }
                        else
                        {
                            //����
                            if (order.DoseOnce == 0)
                            {
                                ShowErr("ҩƷ��" + order.Item.Name + "��ÿ�μ�������Ϊ�㣡", i, 0);
                                return -1;
                            }
                            if (order.DoseUnit == "")
                            {
                                ShowErr("ҩƷ��" + order.Item.Name + "��������λ����Ϊ�գ�", i, 0);
                                return -1;
                            }

                            try
                            {
                                if (order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.').Contains("."))
                                {
                                    ShowErr("ҩƷ��" + order.Item.Name + "������������ΪС����", i, 0);
                                    return -1;
                                }
                            }
                            catch
                            {
                                ShowErr("ҩƷ��" + order.Item.Name + "������������ΪС����", i, 0);
                                return -1;
                            }
                        }
                        if (order.Unit == "")
                        {
                            ShowErr("ҩƷ��" + order.Item.Name + "����λ����Ϊ�գ�", i, 0);
                            return -1;
                        }
                        if (order.Frequency.ID == "")
                        {
                            ShowErr("Ƶ�β���Ϊ�գ�", i, 0); return -1;
                        }
                        if (order.Usage.ID == "")
                        {
                            ShowErr("ҩƷ��" + order.Item.Name + "���÷�����Ϊ�գ�", i, 0); return -1;
                        }

                        decimal doseOnce = order.DoseOnce;
                        if (order.DoseUnit == (order.Item as HISFC.Models.Pharmacy.Item).MinUnit)
                        {
                            doseOnce = order.DoseOnce * (order.Item as HISFC.Models.Pharmacy.Item).BaseDose;
                        }
                        if ((doseOnce / ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).BaseDose) > order.Qty
                            && order.Unit == order.DoseUnit)
                        {
                            ShowErr("ҩƷ��" + order.Item.Name + "��ÿ�����������Դ���������", i, 0);
                            return -1;
                        }

                        if (order.Item.SysClass.ID.ToString() != "PCC" && order.HerbalQty > 7)
                        {
                            //ShowErr("ҩƷ��" + order.Item.Name + "�� ������������7�죡", i, 0);
                            //return -1;
                            exceedWarning += "\r\n" + order.Item.Name;
                        }

                        //�����
                        if (order.StockDept != null && order.StockDept.ID != "")
                        {
                            //������С��λ���ж�
                            decimal storeNum = 0;
                            decimal orderQty = 0;
                            if (order.MinunitFlag != "1")//������С��λ !=((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).MinUnit)
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
                                        ShowErr("ҩƷ��" + order.Item.Name + "���ĵ�ǰ�����Ϊ" + stockinfo + ",����ʹ�ã�", i, 0);
                                        {
                                            return -1;
                                        }
                                    }
                                    else if (isCheckDrugStock == 1)
                                    {
                                        if (ucOutPatientItemSelect1.MessageBoxShow("ҩƷ��" + order.Item.Name + "���ĵ�ǰ�����Ϊ" + stockinfo + ",����ʹ�ã�\r\n�Ƿ�������棡", "��治��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                        {
                                            return -1;
                                        }
                                    }
                                    else
                                    {
                                        Components.Order.Classes.Function.ShowBalloonTip(8, "����", "ҩƷ��" + order.Item.Name + "���ĵ�ǰ�����Ϊ" + stockinfo + ",����ʹ�ã�", ToolTipIcon.Info);
                                    }
                                }
                            }
                            else
                            {
                                ShowErr("ҩƷ��" + order.Item.Name + "������ж�ʧ��!" + phaIntegrate.Err, i, 0);
                                return -1;
                            }
                        }
                        else
                        {
                            if (Classes.Function.CheckPharmercyItemStock(isCheckDrugStock, order.Item.ID, order.Item.Name, order.ReciptDept.ID, order.Qty, "O") == false)
                            {
                                ShowErr("ҩƷ��" + order.Item.Name + "����治��!", i, 0); return -1;
                            }
                        }
                    }
                    #endregion

                    #region ��ҩƷ
                    else
                    {
                        #region �ж�ͣ��״̬
                        Neusoft.HISFC.Models.Fee.Item.Undrug undrug = this.itemManagement.GetUndrugByCode(order.Item.ID);
                        if (undrug == null)
                        {
                            ShowErr("���ҷ�ҩƷ��Ŀ��" + order.Item.Name + "��ʧ�ܣ�" + this.itemManagement.Err, i, 0);
                            return -1;
                        }

                        if (undrug.IsValid == false)
                        {
                            ShowErr("��" + undrug.Name + "����ͣ��\n", i, 0);
                            return -1;
                        }
                        if (!this.hsOrderItem.Contains(undrug.ID))
                        {
                            this.hsOrderItem.Add(undrug.ID, undrug);
                        }

                        #endregion

                        if (order.ExeDept.ID == "")
                        {
                            ShowErr("��" + order.Item.Name + "����ѡ��ִ�п��ң�", i, 0); return -1;
                        }
                    }
                    #endregion

                    if (order.Qty == 0)
                    {
                        ShowErr("��" + order.Item.Name + "����������Ϊ�գ�", i, 0);
                        return -1;
                    }
                    if (Neusoft.FrameWork.Public.String.ValidMaxLengh(order.Memo, 80) == false)
                    {
                        ShowErr("��" + order.Item.Name + "���ı�ע����!", i, 0);
                        return -1;
                    }
                    if (order.Qty > 5000)
                    {
                        ShowErr("����̫��", i, 0); return -1;
                    }
                    if (order.Item.Price == 0)
                    {
                        ShowErr("��" + order.Item.Name + "�����۱�����ڣ���", i, 0);
                        return -1;
                    }
                    if (order.ID == "") IsModify = true;
                }
                //�ѱ����ҽ���˴�һ���ѯ
                else
                {
                    ArrayList alOrder = OrderManagement.QueryOrder(currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                    if (alOrder == null)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("��ѯҽ������" + OrderManagement.Err);
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
                Components.Order.Classes.Function.ShowBalloonTip(8, "����", "����ҩƷ��������7����������ע��ע�����ɣ�\r\n" + exceedWarning, ToolTipIcon.Warning);
            }

            //����ɾ��Ҳ���뱣�棬���ҽ��ȫɾ�ˣ����ܹ��������� houwb 2011-4-14
            if (hsDeleteOrder == null || hsDeleteOrder.Keys.Count == 0)
            {
                if (IsModify == false)
                {
                    return -1;//δ����¼���ҽ��
                }
            }
            if (isShowRepeatItemInScreen)
            {
                //��ʾ�ظ�ҩƷ
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

                    if (ucOutPatientItemSelect1.MessageBoxShow("���ظ�ҩƷ����Ŀ,�Ƿ��������?\r\n" + repeatItemName, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }

            }
            if (!isHaveSublOrders && dealSublMode == 1 && this.neuSpread1.Sheets[0].RowCount > 0)
            {
                //��Ժ����ʾ�Ƿ����¼��㸽��
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
                    if (ucOutPatientItemSelect1.MessageBoxShow("�Ƿ����¼��㸽�ģ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.CalculatSubl(false);
                        //return -1;
                    }
                }
            }

            return 0;

        }

        /// <summary>
        /// ���ҽ��
        /// </summary>
        public void ComboOrder()
        {
            ComboOrder(this.neuSpread1.ActiveSheetIndex);
            this.RefreshCombo();
            this.ucOutPatientItemSelect1.Clear(false);
        }

        /// <summary>
        /// ȡ�����
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
                ucOutPatientItemSelect1.MessageBoxShow("������ȡ����ϵ�������");
                return;
            }

            //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
            int firstRow = -1;
            int firstSubComb = 0;
            //���Ѿ��޸ķ��ŵ����ID
            string combID = "";

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            ////����ǰ��յ������е�...
            //for (int i = this.neuSpread1.ActiveSheet.Rows.Count - 1; i >= 0; i--)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                    #region �ж�������Ѿ��������ҽ������Ҫɾ��ԭ���ĸ���{F67E089F-1993-4652-8627-300295AAED8C}

                    if (o.ID != null && o.ID != "")
                    {
                        #region ҽ�����ĸ��ĵ�ɾ��

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

                    //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    if (firstRow == -1)
                    {
                        firstRow = i;
                        firstSubComb = o.SubCombNO;
                    }
                    //������ͬ�ķ��ż�1
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

                //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                //�������Ŀ�����޸�
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
            //{D96CEC1D-77BF-434f-B440-D1988F73223C}  �����ʾ
            this.ucOutPatientItemSelect1.Clear(false);
        }

        /// <summary>
        /// ��þ�����ͬ��Ϻŵ�ҽ��
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
                if (this.neuSpread1_Sheet1.Cells[i, GetColumnIndexFromName("��Ϻ�")].Text == combID)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, 0);
                    //Ϊ�� ����
                    if (temp == null)
                    {
                        continue;
                    }
                    //���
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
        /// ��鿪����Ч��
        /// </summary>
        /// <param name="alOrders"></param>
        /// <param name="order"></param>
        /// <param name="errinfo"></param>
        /// <returns></returns>
        private int CheckOrderBase(ArrayList alOrders, Neusoft.HISFC.Models.Order.OutPatient.Order order, ref string errinfo)
        {
            /* 1����ѯ��Ŀ��Ϣ
                2����ѯ�����Ϣ
                3����ѯ����Ȩ��
                4����ѯ����ʷ
                5����������Ŀ�ӿ�
                6��������ʾ����������card_no��ѯ���շ���Ϣ�� 
             * */

            if (EditGroup || Patient == null || string.IsNullOrEmpty(Patient.ID))
            {
                return 1;
            }

            int ret = 1;

            #region ���ȱҩͣ�á������Ϣ

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

            //����Ȩ
            ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, this.GetReciptDoct(),
                this.GetReciptDept(), Neusoft.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref errinfo);

            if (ret <= 0)
            {
                return -1;
            }

            //����ʷ�ж�
            ret = Components.Order.Classes.Function.JudgePatientAllergy("1", this.Patient.PID, order, ref errinfo);

            if (ret <= 0)
            {
                return -1;
            }

            #region �ӿ��ж�

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
        /// ��䴦������
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int FillNewOrder(ref Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            order.Patient.Pact = this.currentPatientInfo.Pact;
            order.Patient.Birthday = this.currentPatientInfo.Birthday;

            //�������Һ�ִ�п�����ͬ������Ϊ�Ǳ�����ִ����Ŀ��ִ�п�����ȡ
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
                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ճ��ҽ��
        /// </summary>
        public void PasteOrder()
        {
            Classes.LogManager.Write("����ʼ���ƴ�����");
            this.neuSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            try
            {
                this.neuSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
                if (Classes.Function.AlCopyOrders == null || Classes.Function.AlCopyOrders.Count <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("��������û�п���ճ����ҽ����"));
                    return;
                }
                Classes.LogManager.Write("���ô���һ��" + Classes.Function.AlCopyOrders.Count.ToString() + "����Ŀ��");

                if (Neusoft.HISFC.Components.Order.Classes.HistoryOrderClipboard.Type == ServiceTypes.C)
                {
                    string oldComb = "";
                    string newComb = "";

                    ArrayList alOrder = new ArrayList();
                    ArrayList alAddOrder = new ArrayList();//�������ӽӿ�

                    Neusoft.HISFC.Models.Order.OutPatient.Order order = null;

                    Classes.LogManager.Write("����ʼͳһ��������Ϣ��");
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
                    Classes.LogManager.Write("������ͳһ��������Ϣ��");
                    int j = 0;
                    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
                    {
                        j++;
                        Classes.LogManager.Write("�����Ƶ�" + j.ToString() + "����Ŀ��");
                        //��ӵ���ǰ����� ����ҽ�����ͽ��з���
                        this.AddNewOrder(outOrder, 0);
                    }
                }
                else
                {
                    ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("�����԰�סԺ��ҽ������Ϊ����ҽ����"));
                    return;
                }

            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                Classes.LogManager.Write("�����ƴ�������" + ex.Message + "��");
            }

            Classes.LogManager.Write("����ʼˢ�´���״̬��");
            RefreshOrderState();
            Classes.LogManager.Write("������ˢ�´���״̬��");
            Classes.LogManager.Write("����ʼˢ�´�����Ϻš�");
            this.RefreshCombo();
            Classes.LogManager.Write("������ˢ�´�����Ϻš�");
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            Classes.LogManager.Write("��������ֵ������\r\n");
        }

        #region {7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}

        /// <summary>
        /// ����ҽ��
        /// �����Ƶ�ҽ�������Ǳ�����ģ���ҽ����ˮ�ŵģ�
        /// ����ճ��ʱ������
        /// </summary>
        public void CopyOrder()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0) return;

            ArrayList list = new ArrayList();

            //��ȡѡ���е�ҽ��ID
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

            ////����ӵ�COPY�б�
            //for (int count = 0; count < list.Count; count++)
            //{
            //    HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(list[count]);
            //}

            Classes.Function.AlCopyOrders = list;

            string type = "1";

            HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(type);
            //Ȼ��copy�б�ŵ���������
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Copy();
        }

        #endregion

        #region LIS��Pacs�ӿ�

        #region LIS��PACS���뵥

        /// <summary>
        /// LIS���뵥��ӡ�ӿ�
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint IlisReportPrint = null;

        /// <summary>
        /// LIS���뵥��ӡ
        /// </summary>
        public void LisReportPrint()
        {
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ѡ���ߣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IlisReportPrint == null)
            {
                this.IlisReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint)) as Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint;
            }

            if (IlisReportPrint == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("LIS��ӡ�ӿ�δʵ�֣�����ϵ��Ϣ�ƣ�\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ucOutPatientItemSelect1.MessageBoxShow("û�д�����Ϣ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IlisReportPrint.LisReportPrintForOutPatient(this.Patient, this.GetReciptDept(), this.GetReciptDoct(), alOrders) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow(IlisReportPrint.ErrInfo);
                return;
            }
        }

        /// <summary>
        /// PACS���뵥��ӡ�ӿ�
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint IPacsReportPrint = null;

        /// <summary>
        /// PACS���뵥��ӡ
        /// </summary>
        public void PacsReportPrint()
        {
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ѡ���ߣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IPacsReportPrint == null)
            {
                this.IPacsReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint)) as Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint;
            }

            if (IPacsReportPrint == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("PACS��ӡ�ӿ�δʵ�֣�����ϵ��Ϣ�ƣ�\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ucOutPatientItemSelect1.MessageBoxShow("û�д�����Ϣ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// LIS�����ѯ�ӿ�
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Common.ILis lisInterface = null;

        /// <summary>
        /// ��ѯLIS���
        /// </summary>
        public void QueryLisResult()
        {
            if (this.Patient == null || Patient.PID.CardNO == "" || Patient.PID.CardNO == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ѡ��һ�����ߣ�");
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
                        ucOutPatientItemSelect1.MessageBoxShow("��ѯLIS�ӿڳ��ִ���\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("û��ά��LIS�ӿڣ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        protected Neusoft.HISFC.BizProcess.Interface.Common.IPacs pacsInterface = null;

        /// <summary>
        /// �鿴PACS��鱨�浥
        /// </summary>
        public void QueryPacsReport()
        {
            if (this.Patient == null || Patient.PID.CardNO == "" || Patient.PID.CardNO == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ѡ��һ�����ߣ�");
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

                            ucOutPatientItemSelect1.MessageBoxShow("��ѯPACS�ӿڳ��ִ���\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(Neusoft.FrameWork.Management.Language.Msg("û��ά��PACS�����ѯ�ӿڣ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    //����pacs��ʼ�� ʧ�ܺ� �����������ã����Դ˴����жϳ�ʼ��ʧ�ܣ�
                    this.pacsInterface.Connect();

                    //if (this.pacsInterface.Connect() != 0)
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow("��ʼ��PACSʧ�ܣ�����ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                }

                this.pacsInterface.OprationMode = "1";
                this.pacsInterface.SetPatient(Patient);
                pacsInterface.PlaceOrder(this.GetSelectOrders());

                if (this.pacsInterface.ShowResultByPatient() == 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("�鿴PACS���ʧ�ܣ�����ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("�鿴PACS������ִ�������ϵ��Ϣ�ƣ�\r\n" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #endregion

        #region �¼�

        /// <summary>
        /// ҽ���仯����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        protected virtual void ucItemSelect1_OrderChanged(Neusoft.HISFC.Models.Order.OutPatient.Order sender, EnumOrderFieldList changedField)
        {
            dirty = true;
            if (!this.EditGroup && !this.bIsDesignMode)
                return;

            if (!this.EditGroup)//{E679E3A6-9948-41a8-B390-DD9A57347681}�жϲ��ǿ���ҽ��ģʽ�Ͳ�������ӿ�
            {
                #region ���ݽӿ�ʵ�ֶ�ҽ����Ϣ���в����ж�
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
                //�м��мǣ���2011-12-27 �������ֻ�ڱ����ʱ����ʾ�������޶�
                //this.isShowFeeWarning = false;
                this.AddNewOrder(sender, this.neuSpread1.ActiveSheetIndex);
                this.neuSpread1.ActiveSheet.ClearSelection();
                //this.neuSpread1.ActiveSheet.AddSelection(0, 0, 1, 1);
                //this.neuSpread1.ActiveSheet.ActiveRowIndex = 0;
                this.neuSpread1.ActiveSheet.AddSelection(this.ActiveRowIndex, 0, 1, 1);

                #region add by liuwenwen  2012-06-08 ��ʱ����
                //this.SelectionAllChanged();
                #endregion
            }
            else if (this.ucOutPatientItemSelect1.OperatorType == Operator.Delete)
            {

            }
            else if (this.ucOutPatientItemSelect1.OperatorType == Operator.Modify)
            {
                ArrayList alRows = GetSelectedRows();
                //�޸�
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
                                    //{AA8348EF-8669-4ebf-B863-95469A7A04E2}�����޸ĵ�λ����������е�λ�����ű仯
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
                                //�޸�Ժע
                                else if (changedField == EnumOrderFieldList.InjNum
                                    || changedField == EnumOrderFieldList.ExeDept
                                    )
                                {
                                    order.InjectCount = sender.InjectCount;
                                    order.ExeDept = sender.ExeDept;

                                    this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                //���������/������Ƶ�θı�, ���������һ��ı�, �������¼�������{BE53FA00-A480-41f8-836F-915C11E0C1E4}
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
                                //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                                else if (changedField == EnumOrderFieldList.SubComb)
                                {
                                    this.RefreshCombo();

                                    #region �����ͬ��һ��ѡ��
                                    //���������ѡ��
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
                                #region ȫѡ�޸�

                                //����ȫѡ�޸�
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

                                        //���ڼ���Ժע����������ֻ��ʾ
                                        string errInfo = "";
                                        if (Classes.Function.CalculateInjNum(order, ref errInfo) == -1)
                                        {
                                            ucOutPatientItemSelect1.MessageBoxShow("����Ժע��������\r\n" + errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }

                                        this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                    }
                                    else
                                    {
                                        this.neuSpread1.ActiveSheet.RemoveSelection(System.Convert.ToInt32(alRows[i]), 0, 1, 1);
                                    }
                                }
                                //Ƶ��ȫѡ�޸�
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
                                //�÷�ȫѡ�޸�
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

                    //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    if (changedField == EnumOrderFieldList.SubComb)
                    {
                        this.neuSpread1.ActiveSheet.ClearSelection();

                        #region �����ͬ��һ��ѡ��
                        //���������ѡ��
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

            //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
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
                        case "�÷�����":
                            i = this.GetColumnIndexFromName("�÷�����");
                            this.neuSpread1.ActiveSheet.Cells[e.Row, i].Text =
                                Order.Classes.Function.HelperUsage.GetName(this.neuSpread1.ActiveSheet.Cells[e.Row, e.Column].Text);
                            break;
                        case "ҽ��״̬":
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
        /// ѡ��ҽ���޸�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            SelectionChanged();
        }

        #endregion

        #region IToolBar ��Ա

        #region ɾ��

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        public int Del()
        {
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} �ع���һ��ɾ������
            return Del(this.neuSpread1.ActiveSheet.ActiveRowIndex, false);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int DeleteSingleOrder()
        {

            #region ɾ������

            DialogResult r = DialogResult.Yes;
            bool isHavePha = false;

            if (this.neuSpread1_Sheet1.ActiveRowIndex < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("����ѡ��һ��ҽ����");
                return 0;
            }


            r = ucOutPatientItemSelect1.MessageBoxShow("�Ƿ�ɾ����ѡ��ҽ��\n ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                        ucOutPatientItemSelect1.MessageBoxShow("��ҽ�����ǵ�ǰҽ������,����ɾ��!", "��ʾ");
                        return 0;
                    }

                    //�ܿ�������ɾ��������ɾ���Ͳ��ж���
                    //if (Components.Order.Classes.Function.JudgeEmplPriv(order, this.GetReciptDoct(), this.GetReciptDept(), DoctorPrivType.RecipePriv, true, ref errInfo) <= 0)
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow(errInfo, "��ʾ");
                    //    return 0;
                    //}

                  
                    if (order.ID == "") //��Ȼɾ��
                    {
                        this.neuSpread1.ActiveSheet.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
                    }

                    //�˴�ֻ�Ǽ�¼��Ҫɾ����ҽ��ID
                    else
                    {
                        //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        Neusoft.HISFC.Models.Order.OutPatient.Order temp = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);

                        //�Ҳ���ʱ��ʾ
                        if (temp == null)
                        {
                            //ucOutPatientItemSelect1.MessageBoxShow("��ȡҽ��ʧ��:" + OrderManagement.Err);
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
                    ucOutPatientItemSelect1.MessageBoxShow("��Ŀ��" + order.Item.Name + "���Ѿ��շѣ����ܽ���ɾ��������", "��ʾ");
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
        /// ɾ���Ĵ���ID�����ڱ���ʱɾ��
        /// </summary>
        private Hashtable hsDeleteOrder = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex">������</param>
        /// <param name="isDirctDel">�Ƿ�ֱ��ɾ��������ʾ��</param>
        /// <returns></returns>
        private int Del(int rowIndex, bool isDirctDel)
        {
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} �ع���һ��ɾ������
            #region ȫ��ɾ������
            int j = rowIndex;
            DialogResult r = DialogResult.Yes;
            bool isHavePha = false;
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;//,temp=null;
            if (j < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("����ѡ��һ��ҽ����");
                return 0;
            }
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                //Clear Selected Flag
                this.neuSpread1.Sheets[0].Cells[i, GetColumnIndexFromName("��")].Tag = "";
            }
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                //��־����ѡ����
                if (this.neuSpread1.Sheets[0].IsSelected(i, 0))
                {
                    this.neuSpread1.Sheets[0].Cells[i, GetColumnIndexFromName("��")].Tag = "1";
                }
            }

            if (!isDirctDel)
            {
                r = ucOutPatientItemSelect1.MessageBoxShow("�Ƿ�ɾ����ѡ��ҽ��\n ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (r == DialogResult.Yes)
            {
                for (int i = this.neuSpread1_Sheet1.Rows.Count - 1; i >= 0; i--)
                {
                    if (this.neuSpread1.Sheets[0].Cells[i, GetColumnIndexFromName("��")].Tag != null
                        && this.neuSpread1.Sheets[0].Cells[i, GetColumnIndexFromName("��")].Tag.ToString() == "1")
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
                                ucOutPatientItemSelect1.MessageBoxShow("��ҽ�����ǵ�ǰҽ������,����ɾ��!", "��ʾ");
                                return 0;
                            }

                            //��Ȼ�ܿ�������ɾ��������ɾ������Ҫ���ж���
                            //if (Components.Order.Classes.Function.JudgeEmplPriv(order, this.GetReciptDoct(), this.GetReciptDept(), DoctorPrivType.RecipePriv, true, ref errInfo) <= 0)
                            //{
                            //    ucOutPatientItemSelect1.MessageBoxShow(errInfo, "��ʾ");
                            //    return 0;
                            //}

                            if (order.ExtendFlag1 != null)
                            {
                                string[] strSplit = order.ExtendFlag1.Split('|');
                                if (strSplit.Length == 3)
                                {
                                    if (ucOutPatientItemSelect1.MessageBoxShow("ҽ����" + order.Item.Name + "���Ѿ������˽�ƿ,ȷ��ɾ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
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
                            if (order.ID == "") //��Ȼɾ��
                            {
                                #region 2012-05-29 mad
                                //��Ӵ���ɾ����¼(��עΪ���Һŷ�)
                                if (this.isSaveOrderHistory && this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("��ע")].Text.Trim() == "�Һŷ�")
                                {

                                    if (OrderManagement.InsertOrderChangeInfo(order) < 0)
                                    {
                                        errInfo = "����" + order.Item.Name + "�޸ļ�¼����" + OrderManagement.Err;
                                       // return -1;
                                    }

                                }
                                #endregion
                                this.neuSpread1.ActiveSheet.Rows.Remove(i, 1);
                            }

                            //�˴�ֻ�Ǽ�¼��Ҫɾ����ҽ��ID
                            else //delete from table
                            {
                                //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                                Neusoft.HISFC.Models.Order.OutPatient.Order temp = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                                //Neusoft.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

                                //�Ҳ���ʱ��ʾ
                                if (temp == null)
                                {
                                    //ucOutPatientItemSelect1.MessageBoxShow("��ȡҽ��ʧ��:" + OrderManagement.Err);
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
                            ucOutPatientItemSelect1.MessageBoxShow("ҽ��:[" + order.Item.Name + "]�Ѿ��շѣ����ܽ���ɾ��������", "��ʾ");
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
        /// ɾ��һ������
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int DeleteOneOrder(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            //ɾ��ҽ��
            if (OrderManagement.DeleteOrder(order.SeeNO, Neusoft.FrameWork.Function.NConvert.ToInt32(order.ID)) <= 0)
            {
                errInfo = order.Item.Name + "�����Ѿ��շѣ����˳�������������" + OrderManagement.Err;
                return -1;
            }
            //ɾ������
            if (feeManagement.DeleteFeeItemListByMoOrder(order.ID) == -1)
            {
                errInfo = order.Item.Name + "�����Ѿ��շѣ����˳�������������" + OrderManagement.Err;
                return -1;
            }

            #region ҽ�����ĸ��ĵ�ɾ��{D256A1B3-F969-4d2c-92C3-9A5508835D5B}
            //������Ͽ�����ϺŸı䣬�޸�Ϊ���մ����Ż�ȡ������ϸ

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
                            errInfo = "��Ŀ��" + item.Name + "����Ӧ�ĸ����Ѿ��շѣ�������ɾ����\r\n���˳��������ԣ�";
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
            #region �˻�����
            if (this.isAccountMode)
            {
                int resultValue = 0;
                if (isAccountTerimal && Patient.IsAccount)
                {
                    //ɾ��ҩƷ������Ϣ
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
                        //ɾ����ҩƷ�ն�������Ϣ
                        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                        Neusoft.HISFC.BizProcess.Integrate.Terminal.Confirm confrimIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Terminal.Confirm();
                        if (order.Item.IsNeedConfirm && !order.IsHaveCharged)
                        {
                            resultValue = confrimIntegrate.DelTecApply(order.ReciptNO, order.SequenceNO.ToString());
                            if (resultValue <= 0)
                            {
                                errInfo = "ɾ���ն�������Ϣʧ�ܣ�" + confrimIntegrate.Err;
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
                    errInfo = "����" + order.Item.Name + "�޸ļ�¼����" + OrderManagement.Err;
                    return -1;
                }
            }

            #region ɾ��Ԥ�ۿ����Ϣ {E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
            if (isPreUpdateStockinfo)
            {
                if (this.UpdateStockPre(phaIntegrate, order, -1, ref errInfo) == -1)//ɾ��
                {
                    errInfo = "ɾ��Ԥ����Ϣ����  " + errInfo;
                    return -1;
                }
            }
            #endregion

            #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
            string isUseDL = feeManagement.GetControlValue("200212", "0");
            if (isUseDL == "1")
            {
                if (order.ApplyNo != null)
                {
                    if (PACSApplyInterface == null)
                    {
                        if (InitPACSApplyInterface() < 0)
                        {
                            //ucOutPatientItemSelect1.MessageBoxShow("��ʼ���������뵥�ӿ�ʱ����");
                            errInfo = "��ʼ���������뵥�ӿ�ʱ����";
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
        /// ȷ��ɾ��
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
                //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                order = hsDeleteOrder[orderID] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                if (this.DeleteOneOrder(order) == -1)
                {
                    return -1;
                }
            }

            //ɾ�������
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
        /// ��ȡ��������״̬��Ϣ
        /// </summary>
        /// <returns></returns>
        private int GetRecentPatientInfo()
        {
            try
            {
                #region ��ȡ��������״̬��Ϣ

                //���⻼��״̬�ı�����շѣ�
                //���⿴���û�л�ȡ����״̬������ҽ������Ϣ���¶���շ�
                if (this.currentPatientInfo.IsFee)  //����isSee����Ϊͨ���ҺŽ����� isSeeΪfalse
                {
                    string memo = this.currentPatientInfo.Memo;

                    //��ѯ��Ч�ĹҺż�¼
                    ArrayList alRegister = this.regManagement.QueryPatient(this.currentPatientInfo.ID);
                    if (alRegister == null)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("��ѯ���߹Һ���Ϣ����!");
                        return -1;
                    }

                    if (alRegister.Count == 0)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("�û��߹Һ���Ϣ�����ϣ���ˢ�½���!");
                        return -1;
                    }
                    ((Neusoft.HISFC.Models.Registration.Register)alRegister[0]).DoctorInfo.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO;
                    this.currentPatientInfo = alRegister[0] as Neusoft.HISFC.Models.Registration.Register;

                    if (this.currentPatientInfo != null)
                    {
                        PactInfo pactTemp = pactHelper.GetObjectFromID(currentPatientInfo.Pact.ID) as Neusoft.HISFC.Models.Base.PactInfo;
                        if (pactTemp == null)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("���Һ�ͬ��λ��" + currentPatientInfo.Pact.Name + "(���� " + currentPatientInfo.Pact.ID + ")��ʧ�ܣ�\r\n����ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// ��ӣ�����
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            //���ʱ���Ѿ����뻼����Ϣ
            if (this.currentPatientInfo == null || this.currentPatientInfo.ID == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("û��ѡ���ߣ���˫��ѡ����");
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
                    ucOutPatientItemSelect1.MessageBoxShow(IBeforeAddOrder.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }

            this.hsDeleteOrder.Clear();
            this.IsDesignMode = true;

            this.ucOutPatientItemSelect1.Clear(true);

            //this.ucOutPatientItemSelect1.Focus();
            //�˴����ڿ����������ճ�������ʾ�����޶��
            //this.isShowFeeWarning = false;

            if (this.dealSublMode == 1)
            {
                this.CalculatSubl(true);
            }

            this.PassRefresh();

            return 0;
        }

        /// <summary>
        /// �������뽹��
        /// </summary>
        public void SetInPutFocos()
        {
            this.ucOutPatientItemSelect1.Clear(true);
        }

        /// <summary>
        /// ���۵Ǽ�
        /// </summary>
        /// <returns></returns>
        public int RegisterEmergencyPatient()
        {
            //���ʱ���Ѿ����뻼����Ϣ
            if (this.currentPatientInfo == null || this.currentPatientInfo.ID == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("û��ѡ���ߣ���˫��ѡ����");
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
                    ucOutPatientItemSelect1.MessageBoxShow("�û�������" + deptObj.Name + "���ۣ�");
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
                this.currentPatientInfo.DoctorInfo.SeeNO = this.OrderManagement.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//����µĿ������
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ�������ʧ�ܣ�" + OrderManagement.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                #region ����Ƿ��Ѵ������۷�
                string strSql = string.Format(@"select count(t.item_code) from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                --and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�������۷�'", this.Patient.ID, this.GetReciptDoct().ID);
                string revStr = this.schemgManager.ExecSqlReturnOne(strSql);
                int rev = Neusoft.FrameWork.Function.NConvert.ToInt32(revStr);
                if (rev > 0)
                {
                    if (ucOutPatientItemSelect1.MessageBoxShow("�û�������ȡ���۷ѣ��Ƿ������ȡ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else if (rev == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("��ѯ���۷���ʧ�ܣ�" + this.schemgManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    #region �������չҺŷ���

                    rev = this.schemgManager.ExecNoQuery(@"delete from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�Һŷ�'", this.Patient.ID, this.GetReciptDoct().ID);
                    if (rev == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        ucOutPatientItemSelect1.MessageBoxShow("ɾ���Ѳ��յĹҺŷ�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                    //�ܹ�ɾ��δ�շѵĲ��շ���
                    else if (rev > 0)
                    {
                    }
                    //ɾ�����ɹ����������շѼ�¼
                    else if (rev == 0)
                    {
                        string sql = @"SELECT DISTINCT t.INVOICE_NO from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='1'
                                                AND t.CANCEL_FLAG='1'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�Һŷ�'";
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
                                                and (t.package_name!='�Һŷ�' or t.package_name is null)";
                            sql = string.Format(sql, this.Patient.ID, this.GetReciptDoct().ID, invoiceNo);
                            string invoiceNoTemp = this.schemgManager.ExecSqlReturnOne(sql);
                            if (invoiceNoTemp == invoiceNo && !string.IsNullOrEmpty(invoiceNo) && invoiceNo.Trim() != "-1")
                            {
                                ucOutPatientItemSelect1.MessageBoxShow("�û��ߵļ������۷��Ѳ�����Ʊ��Ϣ���뻼�ߵ��շѴ������ѣ�\n��Ʊ����Ϊ��" + invoiceNo + "����");
                                //System.Windows.Forms.Clipboard.Clear();
                                //System.Windows.Forms.Clipboard.SetDataObject(invoiceNo);
                            }
                            else
                            {
                                sql = @"SELECT DISTINCT INVOICE_SEQ FROM FIN_OPB_INVOICEINFO WHERE INVOICE_NO='{0}'";
                                sql = string.Format(sql, invoiceNo);
                                string invoiceSeq = this.schemgManager.ExecSqlReturnOne(sql);

                                //�Һŷѷ�Ʊ���ϣ�ͬʱ�˻��˻����
                                if (this.feeManagement.LogOutInvoiceByAccout(this.Patient, invoiceNo, invoiceSeq) == -1)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                    ucOutPatientItemSelect1.MessageBoxShow("�˻��������ʧ��:" + this.schemgManager.Err);
                                    return -1;
                                }
                            }
                        }
                    }
                    #endregion

                    #region ���ռ������۷���

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
                            regFeeItem.UndrugComb.Name = "�������۷�";

                            alEmergencyFee.Add(regFeeItem);
                        }
                    }
                    bool iReturn = feeManagement.SetChargeInfo(this.Patient, alEmergencyFee, now, ref errText);
                    if (iReturn == false)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        alEmergencyFee.Remove(regFeeItem);
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    ucOutPatientItemSelect1.MessageBoxShow("��������״̬ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            #region ���ۺ󣬸��¿�����Ϣ
            //����ҺŷѺ󣬸��¹Һű����շ�״̬�������β��չҺŷ�
            if (this.regManagement.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                alEmergencyFee.Remove(regFeeItem);
                dirty = false;
                ucOutPatientItemSelect1.MessageBoxShow(conManager.Err);
                return -1;
            }

            //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                if (this.regManagement.UpdateSeeDone(this.currentPatientInfo.ID) < 0)
                {
                    errInfo = "���¿����־����";

                    return -1;
                }

                if (this.regManagement.UpdateDept(this.currentPatientInfo.ID, ((Neusoft.HISFC.Models.Base.Employee)this.OrderManagement.Operator).Dept.ID, this.OrderManagement.Operator.ID) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    alEmergencyFee.Remove(regFeeItem);
                    ucOutPatientItemSelect1.MessageBoxShow("���¿�����ҡ�ҽ������");
                    return -1;
                }
            }
            #endregion

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            this.currentPatientInfo.PVisit.InState.ID = "R";
            this.currentPatientInfo.IsFee = true;

            ucOutPatientItemSelect1.MessageBoxShow("���۳ɹ���");
            return 1;
        }

        /// <summary>
        /// ��ȡ���۷�
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
                errInfo = "��ȡ����������Ŀʧ�ܣ�" + conManager.Err;
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
                    errInfo = "����������Ŀ" + errInfo;
                    return -1;
                }
                emergencyFeeItem = this.SetSupplyFeeItemListByItem(supplyItem, ref errInfo);

                if (emergencyFeeItem == null)
                {
                    //ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                    return -1;
                }

                //���ڲ��յķ����������
                //emergencyFeeItem.UndrugComb.ID = this.oper.ID;
                emergencyFeeItem.UndrugComb.Name = "�������۷�";
                alEmergencyFee.Add(emergencyFeeItem);
            }
            return 1;
        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// ���صǼ�
        /// </summary>
        /// <returns></returns>
        public int OutEmergencyPatient()
        {
            if (this.currentPatientInfo == null || this.currentPatientInfo.ID == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("û��ѡ���ߣ���˫��ѡ����");
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
                    ucOutPatientItemSelect1.MessageBoxShow("�û��߻�δ���ۣ�");
                    return -1;
                }
            }

            #region �Ȳ��жϽ���״̬
            //if (this.currentPatientInfo.PVisit.InState.ID.ToString() == "R")
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("�û��߻�δ���ﲻ�ܳ��ۣ�");
            //    return -1;
            //}

            //if (this.currentPatientInfo.PVisit.InState.ID.ToString() != "I")
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("����δ���۲��������۴���");
            //    return -1;
            //}
            #endregion
            else
            {
                #region ����Ƿ��Ѵ������۷�
                string strSql = string.Format(@"select count(t.item_code) from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                --and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�������۷�'", this.Patient.ID, this.GetReciptDoct().ID);
                string revStr = this.schemgManager.ExecSqlReturnOne(strSql);
                int rev = Neusoft.FrameWork.Function.NConvert.ToInt32(revStr);
                if (rev <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("δ�ҵ����۷��ã����ó��أ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                #endregion

                if (ucOutPatientItemSelect1.MessageBoxShow("�Ƿ�ɾ��������ȡ�����۷��ã�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                    //ȡ�����ۣ�ɾ�����۷�
                    if (this.alEmergencyFee != null && this.alEmergencyFee.Count > 0)
                    {
                        rev = this.schemgManager.ExecNoQuery(@"delete from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�������۷�'", this.Patient.ID, this.GetReciptDoct().ID);
                        if (rev == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            ucOutPatientItemSelect1.MessageBoxShow("ɾ���Ѳ��յļ������۷�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                        else if (rev == 0)
                        {
                            string sql = @"SELECT DISTINCT t.INVOICE_NO from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='1'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�������۷�'";
                            sql = string.Format(sql, this.Patient.ID, this.GetReciptDoct().ID);
                            string invoiceNo = this.schemgManager.ExecSqlReturnOne(sql);
                            if (!string.IsNullOrEmpty(invoiceNo) && invoiceNo.Trim() != "-1")
                            {
                                ucOutPatientItemSelect1.MessageBoxShow("�û��ߵļ������۷��Ѳ�����Ʊ��Ϣ�����ֹ��˷ѣ�\n��Ʊ����Ϊ��" + invoiceNo + "��");
                            }
                        }
                        else
                        {
                            //�����Ƿ�Ӧ����ȡ�ҺŷѺ�������ж�
                        }
                    }
                    ucOutPatientItemSelect1.MessageBoxShow("���Ҫ�ٴ���ȡ�ҺŷѺ�������ֹ�¼�룡");
                }
            }

            if (!this.isAccountMode)
            {
                if (radtManger.OutObservePatientManager(this.currentPatientInfo, EnumShiftType.EO, "����") < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("��������״̬ʧ�ܣ�");
                    return -1;
                }
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            ucOutPatientItemSelect1.MessageBoxShow("ȡ���������۳ɹ���");
            return 1;
        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// ����תסԺ
        /// </summary>
        /// <returns></returns>
        public int InEmergencyPatient()
        {
            //���ʱ���Ѿ����뻼����Ϣ
            if (this.currentPatientInfo == null || this.currentPatientInfo.ID == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("û��ѡ���ߣ���˫��ѡ����");
                return -1;
            }

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            if (this.currentPatientInfo.PVisit.InState.ID.ToString() == "R")
            {
                ucOutPatientItemSelect1.MessageBoxShow("�û��߻�δ���ﲻ��תסԺ��");
                return -1;
            }

            if (this.currentPatientInfo.PVisit.InState.ID.ToString() != "I")
            {
                ucOutPatientItemSelect1.MessageBoxShow("����δ���۲�����תסԺ����");
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            if (radtManger.OutObservePatientManager(this.currentPatientInfo, EnumShiftType.CPI, "����תסԺ") < 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow("��������״̬ʧ�ܣ�");
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            ucOutPatientItemSelect1.MessageBoxShow("תסԺ�ɹ���");
            return 1;
        }

        /// <summary>
        /// �˳�ҽ������
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
                if (ucOutPatientItemSelect1.MessageBoxShow("��ǰ����δ�����ҽ����ȷ���˳���", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }
            }

            this.IsDesignMode = false;
            this.bTempVar = false;
            return 0;
        }

        /// <summary>
        /// ���ҽ����Ϣ ������seeNO+ID
        /// </summary>
        private Hashtable hsOrder = new Hashtable();

        /// <summary>
        /// ���ҽ����Ŀ��Ϣ�����ݿ���ȡ��
        /// </summary>
        private Hashtable hsOrderItem = new Hashtable();

        /// <summary>
        /// �Ƿ��ѿ������
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
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.bIsDesignMode == false)
            {
                return -1;
            }

            this.ucOutPatientItemSelect1.Clear(false);

            #region ����֮ǰ���ж�

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            this.hsOrder.Clear();
            this.hsOrderItem.Clear();

            //�ڴ˴���������ҽ���б��ҽ����Ŀ�б�
            if (this.CheckOrder() == -1)
            {
                return -1;
            }

            //Ԥ������
            if (isAutoPrintRecipe == 2)
            {
                if (PrintRecipe(true) == -1)
                {
                    return -1;
                }
            }

            #region ���Һ�

            //���Ĵ���ӿ�
            if (IDealSubjob != null && dealSublMode == 0)
            {
                if (dealSublMode == 0)
                {
                    if (this.SetSupplyRegFee(ref alSupplyFee, ref errInfo, currentPatientInfo.IsFee) == -1)
                    {
                        this.alSupplyFee = new ArrayList();
                        ucOutPatientItemSelect1.MessageBoxShow("���չҺŷ�ʧ�ܣ�" + errInfo);
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

            //ҽ������ӿ�{48E6BB8C-9EF0-48a4-9586-05279B12624D}
            if (this.IAlterOrderInstance == null)
            {
                this.InitAlterOrderInstance();
            }
            #endregion

            bool isAccount = false;

            #region �˻��ж�
            if (this.isAccountMode)
            {
                #region �˴��Ȳ����˻���
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

            #region ���洦��ǰ�ӿ�ʵ��
            //������ʾ������ȵ�
            if (IBeforeSaveOrder != null)
            {
                if (IBeforeSaveOrder.BeforeSaveOrderForOutPatient(Patient, this.GetReciptDept(), this.GetReciptDoct(), alAllOrder) == -1)
                {
                    if (!string.IsNullOrEmpty(IBeforeSaveOrder.ErrInfo))
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(IBeforeSaveOrder.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return -1;
                }
            }

            #endregion

            #region ����
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���ҽ�������Ժ󡣡���");
            Application.DoEvents();
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            OrderManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans); //��������
            this.managerIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            feeManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);//��������
            undrugztManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            regManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            phaIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);//{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}

            string strID = "";
            string strNameNotUpdate = "";//û�и��µ�ҽ������
            string reciptNo = "";//������
            int rep_no = 0; //��������ˮ��
            bool bHavePha = false;//�Ƿ����ҩƷ(����Ԥ��ʹ��)

            Neusoft.HISFC.Models.Order.OutPatient.Order order;
            DateTime now = OrderManagement.GetDateTimeFromSysDateTime();
            #endregion

            errInfo = "";
            if (this.DelCommit(ref errInfo) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow("ɾ��ҽ��ʧ�ܣ�" + errInfo);
                return -1;
            }

            #region �жϿ������
            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = this.OrderManagement.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//����µĿ������
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    ucOutPatientItemSelect1.MessageBoxShow(OrderManagement.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

            }
            #endregion

            #region ����ҽ��������

            ArrayList alOrder = new ArrayList(); //����ҽ��
            ArrayList alFeeItem = new ArrayList();//�������
            //ArrayList alSupplyFee = new ArrayList();//
            ArrayList alSubOrders = new ArrayList();//��������

            this.alOrderTemp = new ArrayList();//��ʱ����
            ArrayList alOrderChangedInfo = new ArrayList();//ҽ���޸ļ�¼
            bool iReturn = false;
            string errText = "";

            //��ʾ�ظ�ҩƷ
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

                if (order.Status == 0)//δ��˵�ҽ��
                {
                    #region ����ҽ��

                    if (order.ID == "") //new �¼ӵ�ҽ��
                    {
                        strID = Classes.Function.GetNewOrderID(ref errInfo);
                        if (strID == "")
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1;
                        }

                        order.ID = strID;    //���뵥��
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

                        #region ����Ԥ�ۿ��{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                        if (isPreUpdateStockinfo)
                        {
                            if (this.UpdateStockPre(phaIntegrate, order, 1, ref errInfo) == -1)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;

                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }
                        #endregion

                        #region �˻����ߵĸ�����Ŀ������ϸ�ٻ���{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
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
                                        ucOutPatientItemSelect1.MessageBoxShow("��ѯ��ҩƷ��Ŀ��" + order.Item.Name + "����" + this.feeManagement.Err);
                                        return -1;
                                    }
                                }
                                //������Ŀ���Ȳ��Ż���
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
                                ucOutPatientItemSelect1.MessageBoxShow(order.Item.Name + "ҽ��ʵ��ת���ɷ���ʵ�����", "��ʾ");
                                return -1;
                            }
                            alFeeItem.Add(alFeeItemListTmp);
                        }
                        #endregion
                    }
                    else //update ���µ�ҽ��
                    {
                        #region �����Ҫ���µ�ҽ��
                        //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = OrderManagement.QueryOneOrder(this.Patient.ID, order.ID);
                        //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
                        if (newOrder == null || newOrder.Status == 0)
                        {
                            newOrder = order;
                        }

                        if (newOrder.Status != 0 || newOrder.IsHaveCharged)//��鲢��ҽ��״̬
                        {
                            strNameNotUpdate += "[" + order.Item.Name + "]";

                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("[" + order.Item.Name + "]�����Ѿ��շ�,���˳������������½���!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            ucOutPatientItemSelect1.MessageBoxShow(order.Item.Name + "ҽ��ʵ��ת���ɷ���ʵ�����", "��ʾ");
                            return -1;
                        }
                        alFeeItem.Add(feeitems);

                        #endregion

                        #region ����Ԥ�ۿ��{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                        if (isPreUpdateStockinfo)
                        {
                            if (this.UpdateStockPre(phaIntegrate, order, -1, ref errInfo) == -1)//��ɾ��
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;

                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                            if (this.UpdateStockPre(phaIntegrate, order, 1, ref errInfo) == -1)//�ٲ���
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;

                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Components.Order.Classes.Function.ShowBalloonTip(8, "��ʾ", "�����ظ���Ŀ��\r\n" + repeatItemName, ToolTipIcon.Info); 
                }
            }

            //���Ĵ���ӿ�
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
                            ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + errText);
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region �ж����

            if (isJudgeDiagnose)
            {
                //��ϵ��ж� ��ׯҽԺ
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
                        ucOutPatientItemSelect1.MessageBoxShow("�û��߻�û��¼�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }
            }

            #endregion

            #region δ�ҺŻ��ߣ��˴�����Һ���Ϣ

            if (this.AddRegInfo(Patient) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                dirty = false;
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                return -1;
            }

            //����ҺŷѺ󣬸��¹Һű����շ�״̬�������β��չҺŷ�
            if (this.regManagement.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                dirty = false;
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(conManager.Err);
                return -1;
            }

            #region ���չҺŷ���Ŀ

            //�����ҺŻ��߶����շ���
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

            #region �ϲ��շ�����

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order subOrder in alSubOrders)
            {
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList alFeeItemListTmp = Classes.Function.ChangeToFeeItemList(subOrder);
                if (alFeeItemListTmp == null)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(subOrder.Item.Name + "ҽ��ʵ��ת���ɷ���ʵ�����", "��ʾ");
                    return -1;
                }
                alFeeItem.Add(alFeeItemListTmp);
            }

            #endregion

            #region �շ�


            Classes.LogManager.Write("��ʼ�շ�!");

            //�����ź���ˮ�Ź����ɷ���ҵ��㺯��ͳһ����
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
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                }
                else
                {
                    //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
                    //ֱ���շ� 1�ɹ� -1ʧ�� 0��ͨ���߲���������������
                    if (IDoctFee != null && false)
                    {
                        int resultValue = IDoctFee.DoctIdirectFee(this.Patient, alFeeItem, now, ref errText);
                        if (resultValue == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                        if (resultValue == 0)
                        {
                            iReturn = feeManagement.SetChargeInfo(this.Patient, alFeeItem, now, ref errText);
                            if (iReturn == false)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(errText + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }
            Classes.LogManager.Write("�����շ�!");
            #endregion

            #region ���������źʹ�����ˮ��

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

            #region ����ҽ�� �������´�����

            #region ���ݽӿ�ʵ�ֶ�ҽ����Ϣ���в����ж�
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
                    ucOutPatientItemSelect1.MessageBoxShow("����ҽ���շѱ��ʧ�ܣ�" + errText);
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

                #region ����ҽ����
                if (OrderManagement.UpdateOrder(temp) == -1) //����ҽ����
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("����ҽ������" + temp.Item.Name + "�����Ѿ��շ�,���˳������������½���!");
                    return -1;
                }
                #endregion
            }
            #endregion

            #region ����ҽ�������¼

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

                    #region �ж��Ƿ���Ҫ����
                    //�޸�����
                    if (tem.Qty != temp.Qty)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //�޸ĵ�λ
                    else if (tem.Unit != temp.Unit)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //�޸�ÿ����
                    else if (tem.DoseOnce != temp.DoseOnce)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //ÿ�ε�λ
                    else if (tem.DoseUnit != temp.DoseUnit)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //��ҩ����
                    else if (tem.HerbalQty != temp.HerbalQty)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //�÷�
                    else if (tem.Usage.ID != temp.Usage.ID)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //Ƶ��
                    else if (tem.Frequency.ID != temp.Frequency.ID)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //ִ�п���
                    else if (tem.ExeDept.ID != temp.ExeDept.ID)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //��ע
                    else if (tem.Memo != temp.Memo)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //��ƿ
                    else if (tem.ExtendFlag1 != temp.ExtendFlag1)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //���
                    else if (tem.Combo.ID != temp.Combo.ID)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //Ժע
                    else if (tem.InjectCount != temp.InjectCount)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //�Ӽ�
                    else if (tem.IsEmergency != temp.IsEmergency)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //Ƥ��
                    else if (tem.HypoTest != temp.HypoTest)
                    {
                        alOrderChangedInfo.Add(temp);
                        continue;
                    }
                    //���鸽��
                    else if (tem.NurseStation.User01 != temp.NurseStation.User01)
                    {
                        alOrderChangedInfo.Add(tem);
                        continue;
                    }
                    #endregion

                }

                //��������¼��
                for (int i = 0; i < alOrderChangedInfo.Count; i++)
                {
                    temp = alOrderChangedInfo[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                    if (this.OrderManagement.InsertOrderChangeInfo(temp) < 0)
                    {
                        //���ڱ����¼����Ҳ����ʾ
                        //Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                        //ucOutPatientItemSelect1.MessageBoxShow("����ҽ�������¼����");
                        //Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        //return -1;
                    }
                }
            }
            #endregion

            #region ���¿�����Ϣ
            if (isUseNurseArray && this.currentPatientInfo.IsTriage)
            {
                if (this.currentRoom != null)
                {
                    if (this.managerIntegrate.UpdateAssignSaved(this.currentRoom.ID, this.currentPatientInfo.ID, now, this.OrderManagement.Operator.ID) < 0)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("���·����־����");
                        return -1;
                    }
                }
            }

            //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                if (this.regManagement.UpdateSeeDone(this.currentPatientInfo.ID) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("���¿����־����");
                    return -1;
                }

                if (this.regManagement.UpdateDept(this.currentPatientInfo.ID, ((Neusoft.HISFC.Models.Base.Employee)this.OrderManagement.Operator).Dept.ID, this.OrderManagement.Operator.ID) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("���¿�����ҡ�ҽ������");
                    return -1;
                }
            }

            #endregion

            #region �ύ
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            Classes.LogManager.Write("���������ύ�ɹ�!");

            //���ڲ��Һŵģ�����ɹ����ܸ������շѱ��
            if (!this.Patient.IsFee)
            {
                this.Patient.IsFee = true;
            }

            //���»���״̬Ϊ����󣬸��Ļ�����Ϣ�л��߿���״̬
            this.Patient.IsSee = true;

            #endregion

            #region �˻���ȡ�Һŷ�

            int iRes = 1;
            if (this.isAccountMode)
            {
                //ucOutPatientItemSelect1.MessageBoxShow("���б�����ִ����Ŀ�������ն�ˢ���۷ѣ�");
                Classes.LogManager.Write("��ʼ��ȡ�Һŷ�!");

                //�����ҺŻ��߶����շ���
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
                            //ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n ����ԭ���ǣ��˻����㣬�뻼�ߵ��շѴ���ֵ��ɷѣ�");
                        }
                        else
                        {
                            Neusoft.FrameWork.Management.PublicTrans.Commit();
                            //ucOutPatientItemSelect1.MessageBoxShow("��ȡ�ҺŷѺ����ɹ���");
                        }

                    }
                }
                Classes.LogManager.Write("������ȡ�Һŷ�!");

            }
            #endregion

            #region ��ʾ��Ϣ�ŵ�һ��

            if (strNameNotUpdate == "")//�Ѿ��仯��ҽ����Ϣ
            {
                ucOutPatientItemSelect1.MessageBoxShow("ҽ������ɹ���");
            }
            else
            {
                ucOutPatientItemSelect1.MessageBoxShow("ҽ������ɹ���\n" + strNameNotUpdate + "ҽ��״̬�Ѿ��������ط����ģ��޷����и��£���ˢ����Ļ��");
            }
            Classes.LogManager.Write("��������ɹ�!");

            //�����ҺŻ��߶����շ���
            if (this.isAccountMode)
            {
                if (this.Patient.PVisit.InState.ID.ToString() == "N")
                {
                    if (this.alSupplyFee != null && this.alSupplyFee.Count > 0)
                    {
                        if (iRes <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n ����ԭ���ǣ��˻����㣬�뻼�ߵ��շѴ���ֵ��ɷѣ�");
                        }
                        else
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("����ѿ۷ѳɹ���");
                        }
                    }
                }
            }
            #endregion

            #region ����ҽ�����
            if (this.SaveSortID(0) < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow("����ҽ����ų���");
                return -1;
            }
            #endregion

            #region ����������뵥
            string isUseDL = feeManagement.GetControlValue("200212", "0");
            if (isUseDL == "1")
            {
                if (PACSApplyInterface == null)
                {
                    if (InitPACSApplyInterface() < 0)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("��ʼ���������뵥�ӿ�ʱ����");
                        return -1;
                    }
                }
                PACSApplyInterface.SaveApplysG(this.Patient.DoctorInfo.SeeNO.ToString(), 0);
            }
            #endregion

            #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} ������ҩ�Զ����

            if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
            {
                //this.IReasonableMedicine.ShowFloatWin(false);
                this.PassTransOrder(1, false);
            }

            #endregion

            #region ���ش���
            isShowFeeWarning = false;
            this.IsDesignMode = false;
            this.bTempVar = false;

            //{F67E089F-1993-4652-8627-300295AAED8C}
            //��������
            this.hsComboChange = new Hashtable();
            this.alSupplyFee = new ArrayList();
            #endregion

            #region �Զ���ӡ����

            if (isAutoPrintRecipe == 0)
            {
            }
            else if (isAutoPrintRecipe == 1 || isAutoPrintRecipe == 3)
            {
                this.PrintRecipe(false);
            }

            #endregion

            #region �ⲿ�ӿ�ʵ��

            if (IAfterSaveOrder != null)
            {
                if (IAfterSaveOrder.OnSaveOrderForOutPatient(this.Patient, this.GetReciptDept(), this.GetReciptDoct(), alOrder) != 1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(IAfterSaveOrder.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            #endregion         

            return 0;
        }

        #region ���Һŷ�

        /// <summary>
        /// ����Һż���
        /// </summary>
        string emergRegLevl = "";

        /// <summary>
        /// ְ����Ӧ�ĹҺż���
        /// </summary>
        string regLevl_DoctLevl = "";

        /// <summary>
        /// �������۲��շ���
        /// </summary>
        ArrayList alEmergencyFee = new ArrayList();

        /// <summary>
        /// ��ȡ���Һ���Ŀ
        /// </summary>
        /// <returns></returns>
        private int GetSupplyItem()
        {
            //֮��ӿ��Ʋ�����
            oper = this.managerIntegrate.GetEmployeeInfo(this.GetReciptDoct().ID);

            #region �����

            //��������
            emergRegItem = new Neusoft.HISFC.Models.Fee.Item.Undrug();

            emergRegLevl = "";
            ArrayList emergRegLevlList = this.conManager.GetList("EmergencyLevel");
            if (emergRegLevlList == null || emergRegLevlList.Count == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ�����ʧ�ܣ�" + conManager.Err);
                //return -1;
            }
            else if (emergRegLevlList.Count > 0)
            {
                emergRegLevl = ((Neusoft.FrameWork.Models.NeuObject)emergRegLevlList[0]).ID.Trim();
            }
            if (string.IsNullOrEmpty(emergRegLevl))
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ����Ŵ�������ϵ��Ϣ�ƣ�");
                //return -1;
            }

            Neusoft.FrameWork.Models.NeuObject emergRegConst = this.conManager.GetConstant("REGLEVEL_DIAGFEE", emergRegLevl);
            if (emergRegConst == null || string.IsNullOrEmpty(emergRegConst.ID))
            {
                ucOutPatientItemSelect1.MessageBoxShow("û��ά������Ŷ�Ӧ�����ѣ�");
                //return -1;
            }

            if (this.CheckItem(emergRegConst.Name.Trim(), ref errInfo, ref emergRegItem) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("�����" + errInfo);
                //return -1;
            }

            #endregion

            #region ҽ��ְ�ƶ�Ӧ������

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
                ucOutPatientItemSelect1.MessageBoxShow("ҽ����ְ��[" + oper.Level.Name + "]��Ӧ��������Ŀ" + errInfo);
                //return -1;
            }

            #endregion

            #region ���Ҳ����Ŀ

            diffDiagItem = null;

            string diffDiagItemCode = "";
            ArrayList diffDiagConstList = this.conManager.GetList("DiffDiagItem");
            if (diffDiagConstList == null || diffDiagConstList.Count == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ�ҺŲ����Ŀʧ�ܣ�" + conManager.Err);
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
                    ucOutPatientItemSelect1.MessageBoxShow("�ҺŲ����Ŀ" + errInfo);
                    //return -1;
                }
            }

            #endregion

            #region ���յĹҺŷ���Ŀ

            regItem = new Neusoft.HISFC.Models.Fee.Item.Undrug();

            string regItemCode = "";
            ArrayList regConstList = this.conManager.GetList("RegFeeItem");
            if (regConstList == null || regConstList.Count == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ�Һŷ���Ŀʧ�ܣ�" + conManager.Err);
                //return -1;
            }
            else if (regConstList.Count > 0)
            {
                regItemCode = ((Neusoft.FrameWork.Models.NeuObject)regConstList[0]).ID.Trim();
            }

            if (this.CheckItem(regItemCode, ref errInfo, ref regItem) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("�Һŷ���Ŀ" + errInfo);
                //return -1;
            }
            #endregion

            #region ��ҺŷѵĿ���

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

            #region �������۷���

            alEmergencyFee = new ArrayList();

            Neusoft.HISFC.Models.Fee.Item.Undrug supplyItem;
            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList emergencyFeeItem = null;

            string supplyItemCode = "";
            ArrayList emergencyConstList = this.conManager.GetList("EmergencyFeeItem");
            if (emergencyConstList == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ����������Ŀʧ�ܣ�" + conManager.Err);
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
                    ucOutPatientItemSelect1.MessageBoxShow("����������Ŀ" + errInfo);
                    //return -1;
                }
                emergencyFeeItem = this.SetSupplyFeeItemListByItem(supplyItem, ref errInfo);

                if (emergencyFeeItem == null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                    //return -1;
                }

                //���ڲ��յķ����������
                //emergencyFeeItem.UndrugComb.ID = this.oper.ID;
                emergencyFeeItem.UndrugComb.Name = "�������۷�";
                alEmergencyFee.Add(emergencyFeeItem);
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// ��ǰ���ߵĹҺż�����Ϣ
        /// </summary>
        Neusoft.HISFC.Models.Registration.RegLvlFee regLevlFee = new Neusoft.HISFC.Models.Registration.RegLvlFee();

        /// <summary>
        /// ���չҺŷ���Ŀ
        /// </summary>
        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList regFeeItem = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();

        /// <summary>
        /// ���ﲹ�������ǲ���˳�¸���ģʽ���Һ�ֻ�չҺŷѣ�ҽ��վ�������Ͳ����Ŀ
        /// </summary>
        //private bool isShunDeFuYou = false;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isEmergency = false;

        /// <summary>
        /// �Ƿ�Ժ��ְ����Һŷ� 0 �����⣻1 ��Һŷѣ�2 ����� 3 ȫ��
        /// </summary>
        private int emplFreeRegType = 0;

        /// <summary>
        /// �Ƿ�Ժ��ְ�����������֤���ж�
        /// </summary>
        private bool isEmpl = false;

        /// <summary>
        /// ���չҺŷ�:�Һŷ�+����
        ///{EF052C04-D357-4409-84E5-3E6102766746}
        /// </summary>
        /// <param name="alSupplyFee">���յķ����б�</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <param name="isReged">�Ƿ����չ��Һŷ�</param>
        /// <returns></returns>
        private int SetSupplyRegFee(ref ArrayList alSupplyFee, ref string errInfo, bool isFee)
        {
            try
            {
                alSupplyFee = new ArrayList();
                if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
                {
                    errInfo = "��ȡ������Ϣ����������ѡ���ߣ�";
                    return -1;
                }

                //�ѿ�����ҿ���ҽ��Ϊ��ǰ��¼��Ա���˳�
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

                #region ��ͬ��λ�͹Һż����Ӧ�ĹҺŷ�

                if (string.IsNullOrEmpty(Patient.DoctorInfo.Templet.RegLevel.ID))
                {
                    Patient.DoctorInfo.Templet.RegLevel = this.regManagement.GetRegLevl(this.GetReciptDept().ID, oper.ID, this.oper.Level.ID);
                    if (Patient.DoctorInfo.Templet.RegLevel == null)
                    {
                        errInfo = "��ȡ�Һż������" + this.regManagement.Err;
                    }
                }

                if (string.IsNullOrEmpty(Patient.DoctorInfo.Templet.RegLevel.ID))
                {
                    errInfo = "���չҺŷѴ��󣬹Һż���Ϊ�գ�";
                }

                regLevlFee = this.regManagement.GetRegLevelByPactCode(Patient.Pact.ID, Patient.DoctorInfo.Templet.RegLevel.ID);
                if (regLevlFee == null)
                {
                    errInfo = "�ɺ�ͬ��λ�͹Һż����ȡ�Һŷ�ʧ�ܣ�����ϵ��Ϣ������ά��" + regManagement.Err;
                    return 0;
                }

                #endregion

                {
                    #region һ�����

                    #region Ժ��ְ������

                    if (isEmpl && emplFreeRegType == 3)
                    {
                        return 1;
                    }

                    #endregion

                    //����Ѿ��չ��Һŷѣ���ѯ���
                    if (isFee)
                    {
                        #region ����ȡ�����

                        //N �����Һ� R ���۵Ǽ� I �������� P ���۵Ǽ� B ���۳�Ժ��� E ����תסԺ�Ǽ� C ����תסԺ���
                        if (this.Patient.PVisit.InState.ID.ToString() != "N")
                        {
                            return 1;
                        }

                        //�Ѿ�������ģ���ҽ���ٴο��ﲻ�շ�
                        //ÿ�ο��ﶼ���µ�ǰҽ��Ϊ����ҽ��������ǰ���ҽ�������ٴ��շ�
                        if (this.Patient.SeeDoct.ID == this.GetReciptDoct().ID)
                        {
                            return 1;
                        }
                        if (this.Patient.Memo == "������")
                        {
                            return 1;
                        }

                        #endregion

                        #region ���չҺŷѲ��

                        //���߹Һ�ʱû����ȡ�Һŷѣ����ж��Ƿ���
                        if (Patient.RegLvlFee.RegFee == 0)
                        {
                            //�Ƿ��չҺŷ�
                            bool isCanSupplyRegFee = true;

                            #region �ж��Ƿ����Һŷ�

                            if (isEmpl && emplFreeRegType == 1)
                            {
                                isCanSupplyRegFee = false;
                            }

                            //����Һż����Ӧ�ĹҺŷ�Ϊ0  �򲻲���
                            if (regLevlFee.RegFee <= 0)
                            {
                                isCanSupplyRegFee = false;
                            }

                            #region Ժ��ְ��

                            ArrayList list = accountMgr.GetMarkByCardNo(Patient.PID.CardNO, "Empl_ CARD", "1");
                            if (list != null && list.Count > 0)
                            {
                                isCanSupplyRegFee = false;
                            }
                            #endregion

                            #region �Һź�ͬ��λ����

                            if (regLevlFee != null && regLevlFee.RegFee == 0)
                            {
                                isCanSupplyRegFee = false;
                            }

                            #endregion

                            #endregion

                            if (isCanSupplyRegFee && regItem != null)
                            {
                                //���ڲ��յķ����������
                                regFeeItem = this.SetSupplyFeeItemListByItem(regItem, ref errInfo);
                                if (regFeeItem == null)
                                {
                                    return -1;
                                }

                                regFeeItem.UndrugComb.Name = "�Һŷ�";
                                alSupplyFee.Add(regFeeItem);
                            }
                        }
                        #endregion

                        #region ���������

                        if (isEmpl && emplFreeRegType == 2)
                        {

                        }
                        else
                        {
                            //ֻ�к�ͬ��λ�е����Ϊ0ʱ���Ų���
                            if (regLevlFee.OwnDigFee > 0)
                            {
                                //���߹Һ�ʱ���Ϊ�㣬����ҽ��ְ����Ӧ��������Ŀ����
                                if (Patient.RegLvlFee.OwnDigFee == 0)
                                {
                                    Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList diagFeeItem = null;

                                    //����ʱ��Σ���������ְ���ͼ����Ӧ�����ͬʱ�����ռ�����ȡ
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
                                    //���ڲ��յķ����������
                                    diagFeeItem.UndrugComb.Name = "�Һŷ�";
                                    alSupplyFee.Add(diagFeeItem);
                                }
                                else
                                {
                                    //�����Ŀû��ά���򲻲���
                                    if (diffDiagItem != null & !string.IsNullOrEmpty(diffDiagItem.ID))
                                    {
                                        decimal diffCost = 0;
                                        if (isEmerg)
                                        {
                                            //diffCost = Math.Max(diagItem.Price - regLevlFee.OwnDigFee, emergRegItem.Price - regLevlFee.OwnDigFee);
                                            //���ջ���ʵ����ȡ�ĹҺŷѲ��գ�
                                            //����ĳ�໼�������ȡΪ��ģ��˴����ǻᲹ�ղ����Զ������໼����Ҫά�����Ѻ����ļ���Ϊ100%
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
                                        //���ڲ��յķ����������
                                        //diffDiagFeeItem.UndrugComb.ID = this.oper.ID;
                                        diffDiagFeeItem.UndrugComb.Name = "�Һŷ�";
                                        alSupplyFee.Add(diffDiagFeeItem);
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    //δ�Һŵ�ȫ��
                    else
                    {
                        if (this.currentPatientInfo.Memo == "������")
                        {
                            return 1;
                        }

                        #region ���չҺŷ�

                        //�Ƿ��չҺŷ�
                        bool isCanSupplyRegFee = true;

                        #region �ж��Ƿ����Һŷ�

                        if (isEmpl && emplFreeRegType == 1)
                        {
                            isCanSupplyRegFee = false;
                        }

                        //����Һż����Ӧ�ĹҺŷ�Ϊ0  �򲻲���
                        if (regLevlFee.RegFee <= 0)
                        {
                            isCanSupplyRegFee = false;
                        }


                        #region Ժ��ְ��

                        ArrayList list = accountMgr.GetMarkByCardNo(Patient.PID.CardNO, "Empl_ CARD", "1");
                        if (list != null && list.Count > 0)
                        {
                            isCanSupplyRegFee = false;
                        }
                        #endregion

                        #region �Һź�ͬ��λ����

                        if (regLevlFee != null && regLevlFee.RegFee == 0)
                        {
                            isCanSupplyRegFee = false;
                        }

                        #endregion

                        #endregion

                        if (isCanSupplyRegFee && regItem != null)
                        {
                            //���ڲ��յķ����������
                            regFeeItem = this.SetSupplyFeeItemListByItem(regItem, ref errInfo);
                            if (regFeeItem == null)
                            {
                                return -1;
                            }

                            regFeeItem.UndrugComb.Name = "�Һŷ�";
                            alSupplyFee.Add(regFeeItem);
                        }

                        #endregion

                        #region ��������

                        if (isEmpl && emplFreeRegType == 2)
                        {

                        }
                        else
                        {
                            //�û��ߺ�ͬ��λ�����ά��Ϊ0,����Ϊ�Ǽ������ѣ����ٲ���
                            if (regLevlFee.OwnDigFee > 0)
                            {
                                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList diagFeeItem = null;

                                //���Һŵģ��˴���Ҫ���¹Һ���Ϣ
                                string regLevlCode = "";
                                //����ʱ��Σ���������ְ���ͼ����Ӧ�����ͬʱ�����ռ�����ȡ
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

                                #region �޸Ļ��߹Һż�����Ϣ
                                if (regLevlCode != Patient.DoctorInfo.Templet.RegLevel.ID)
                                {
                                    Neusoft.HISFC.Models.Registration.RegLevel regLevlObj = this.regManagement.QueryRegLevelByCode(regLevlCode);
                                    if (regLevlObj == null)
                                    {
                                        ucOutPatientItemSelect1.MessageBoxShow("��ѯ�Һż�����󣬱���[" + regLevlCode + "]������ϵ��Ϣ������ά��" + regManagement.Err);
                                        return -1;
                                    }
                                    Patient.DoctorInfo.Templet.RegLevel = regLevlObj;
                                }
                                #endregion

                                if (diagFeeItem == null)
                                {
                                    return -1;
                                }
                                //���ڲ��յķ����������
                                //diagFeeItem.UndrugComb.ID = this.oper.ID;
                                diagFeeItem.UndrugComb.Name = "�Һŷ�";
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
        /// �����Ŀά���Ƿ���ȷ
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="err"></param>
        /// <param name="itemObj"></param>
        /// <returns></returns>
        private int CheckItem(string itemCode, ref string err, ref Neusoft.HISFC.Models.Fee.Item.Undrug itemObj)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                err = "ά����������ϵ��Ϣ�ƣ�";
                return -1;
            }

            itemObj = this.feeManagement.GetItem(itemCode);
            if (itemObj == null)
            {
                err = "������Ŀʧ�ܣ�" + this.feeManagement.Err;
                return -1;
            }
            else if (string.IsNullOrEmpty(itemObj.ID))
            {
                err = "û��ά��������ϵ��Ϣ�ƣ�";
            }
            else if (!itemObj.IsValid)
            {
                err = "�Ѿ����ڣ�����ϵ��Ϣ�ƣ�";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���ò��Һŵķ�����ϸ��Ϣ
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
                feeitemlist.Patient.ID = Patient.ID;//������ˮ��
                feeitemlist.Patient.PID.CardNO = Patient.PID.CardNO;//���￨�� 
                feeitemlist.Order.ID = Classes.Function.GetNewOrderID(ref errInfo);

                feeitemlist.ChargeOper.ID = this.GetReciptDoct().ID;
                feeitemlist.Order.Combo.ID = this.OrderManagement.GetNewOrderComboID();

                feeitemlist.ExecOper.Dept = this.GetReciptDept();

                feeitemlist.FT.OwnCost = Neusoft.FrameWork.Public.String.FormatNumber(feeitemlist.Item.Qty * feeitemlist.Item.Price, 2);
                feeitemlist.FT.TotCost = feeitemlist.FT.OwnCost;
                feeitemlist.FeePack = "1";

                feeitemlist.Days = 1;//��ҩ����
                feeitemlist.RecipeOper.Dept = this.GetReciptDept();//����������Ϣ
                feeitemlist.RecipeOper.Name = this.GetReciptDoct().Name;//����ҽ����Ϣ
                feeitemlist.RecipeOper.ID = this.GetReciptDoct().ID;

                feeitemlist.Order.Item.ItemType = item.ItemType;//�Ƿ�ҩƷ
                feeitemlist.PayType = Neusoft.HISFC.Models.Base.PayTypes.Charged;//����״̬

                ((Neusoft.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = this.OrderManagement.GetDateTimeFromSysDateTime();
                ((Neusoft.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = feeitemlist.RecipeOper.Dept;//�Ǽǿ���
                feeitemlist.TransType = Neusoft.HISFC.Models.Base.TransTypes.Positive;//��������

                //�շ����кţ��з���ҵ���ͳһ����
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
        /// �жϲ��յķ����Ƿ���ȷ���Ƿ��ظ�
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
                //�Һŷѵ�����
                int regFeeCount = 0;

                //���в��շѵ�����
                int totCount = 0;
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemObj in alCharge)
                {
                    if (itemObj.UndrugComb.Name == "�Һŷ�")
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
                    errInfo = "���չҺŷѴ��󣺹Һŷ��ظ�������ϵ��Ϣ�ƣ�";
                    return -1;
                }
                if (totCount > 3)
                {
                    errInfo = "���չҺŷѴ��󣺲��շ����ظ�������ϵ��Ϣ�ƣ�";
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

        #region ���

        public int DiagOut()
        {
            Classes.LogManager.Write("��ʼ�������!");

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

            //���»���״̬Ϊ����󣬸��Ļ�����Ϣ�л��߿���״̬
            this.Patient.IsSee = true;

            #region �˻���ȡ�Һŷ�

            if (this.isAccountMode)
            {
                //�����ҺŻ��߶����շ���
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
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n ����ԭ���ǣ��˻����㣬�뻼�ߵ��շѴ���ֵ��ɷѣ�");
                        }
                        else
                        {
                            Neusoft.FrameWork.Management.PublicTrans.Commit();
                            //ucOutPatientItemSelect1.MessageBoxShow("��ȡ�ҺŷѺ����ɹ���");
                            this.Patient.IsFee = true;
                        }

                    }
                }
            }
            #endregion

            Classes.LogManager.Write("�����������!");
            return 1;
        }

        /// <summary>
        /// �Ƿ������������
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
                    ucOutPatientItemSelect1.MessageBoxShow(this, "�ô����Ѿ��շѣ�����������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                //���쳣�ˣ��������������
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                return true;
            }

            return true;
        }


        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int DiagOut(ref string errInfo)
        {
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                errInfo = "��û��ѡ���ߣ�";
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
            ////��������
            //this.managerAssign.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //this.regManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = this.OrderManagement.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//����µĿ������
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    errInfo = "��ȡ�������ʧ�ܣ�" + OrderManagement.Err;
                    return -1;
                }

            }

            #region ���Һŷ�

            ArrayList alFeeItem = new ArrayList();

            #region ����Һż�¼

            if (this.AddRegInfo(Patient) == -1)
            {
                return -1;
            }

            if (this.SetSupplyRegFee(ref this.alSupplyFee, ref errInfo, this.Patient.IsFee) == -1)
            {
                this.alSupplyFee = new ArrayList();
                errInfo = "���չҺŷ�ʧ�ܣ�" + errInfo;
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

            //����ҺŷѺ󣬸��¹Һű����շ�״̬�������β��չҺŷ�
            if (this.regManagement.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                errInfo = conManager.Err;
                return -1;
            }
            //}

            #endregion

            #region �շ�

            //�����ź���ˮ�Ź����ɷ���ҵ��㺯��ͳһ����
            try
            {
                bool rev = false;
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                if (this.isAccountMode)
                {
                    bool isAccount = false;
                    #region �˻��ж�
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
                        //ֱ���շ� 1�ɹ� -1ʧ�� 0��ͨ���߲���������������
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

            #region ���·���
            if (isUseNurseArray && this.deptHelper.GetObjectFromID(empl.Dept.ID) != null && !this.Patient.IsSee)
            {
                if (this.currentRoom != null)
                {
                    iReturn = this.managerAssign.UpdateAssign(this.currentRoom.ID, this.Patient.ID, now, empl.ID);
                    if (iReturn < 0)
                    {
                        errInfo = "���·����־����";

                        return -1;
                    }
                }
            }
            #endregion

            #region ���¿���

            //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                iReturn = this.regManagement.UpdateSeeDone(this.Patient.ID);
                if (iReturn < 0)
                {
                    errInfo = "���¿����־����";

                    return -1;
                }

                iReturn = this.regManagement.UpdateDept(this.Patient.ID, empl.Dept.ID, empl.ID);
                if (iReturn < 0)
                {
                    errInfo = "���¿�����ҡ�ҽ������";

                    return -1;
                }
            }
            #endregion

            //{44832DAC-80CF-41e6-BD54-6E8DB45E4790} �������û���ύ��bug
            //Neusoft.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        #endregion

        /// <summary>
        /// ��¼�Һ���Ϣ
        /// </summary>
        /// <param name="regInfo"></param>
        /// <returns></returns>
        private int AddRegInfo(Neusoft.HISFC.Models.Registration.Register regInfo)
        {
            //���ݹҺű���isfee��ǣ��ж��ǲ���ϵͳ���Һŵļ�¼
            Neusoft.HISFC.Models.Registration.Register regTemp = this.regManagement.GetByClinic(regInfo.ID);
            if (regTemp == null || string.IsNullOrEmpty(regTemp.ID))
            {
                //���Һ�
                if (this.regManagement.Insert(regInfo) == -1)
                {
                    errInfo = "�����Һ���Ϣ����Һű����" + regManagement.Err;
                    return -1;
                }

                //����������Ϣ
                if (this.OrderManagement.UpdateHealthInfo(regInfo.Height, regInfo.Weight, regInfo.SBP, regInfo.DBP, regInfo.ID, regInfo.Temperature, regInfo.BloodGlu) == -1)
                {
                    errInfo = "���»���������Ϣ����" + OrderManagement.Err;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ��ʼ���������뵥�ӿ�
        /// {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
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
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public int Retrieve()
        {
            // TODO:  ��� ucOrder.Retrieve ʵ��
            this.QueryOrder();
            return 0;
        }

        /// <summary>
        /// ��ҩ
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
                        Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
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
                    Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
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

        #region �˵�

        int ActiveRowIndex = -1;

        /// <summary>
        /// ����Ҽ��˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            this.contextMenu1.Items.Clear();
            Neusoft.HISFC.Models.Order.OutPatient.Order mnuSelectedOrder = null;

            FarPoint.Win.Spread.Model.CellRange c = neuSpread1.GetCellFromPixel(0, 0, e.X, e.Y);

            #region ����˵�

            //�������ѡ��ͬ����Ŀ
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

            #region �Ҽ��˵�

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
                        #region ճ��ҽ��
                        //if (Neusoft.HISFC.Components.Order.Classes.HistoryOrderClipboard.IsHaveCopyData)
                        //{
                        ToolStripMenuItem mnuPasteOrder = new ToolStripMenuItem("ճ��ҽ��");
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
                    ////mnuLisCard.Text = "��ӡ�������뵥[��ݼ�:F12]";
                    ////mnuLisCard.Click += new EventHandler(mnuLisCard_Click);
                    ////this.contextMenu1.Items.Add(mnuLisCard);
                }
                if (mnuSelectedOrder != null && mnuSelectedOrder.Item.SysClass.ID.ToString() == "UZ" && mnuSelectedOrder.Status == 0)
                {
                    ////ToolStripMenuItem mnuDealCard = new ToolStripMenuItem();
                    ////mnuDealCard.Text = "��ӡ�������뵥[��ݼ�:F12]";
                    ////mnuDealCard.Click += new EventHandler(mnuDealCard_Click);
                    ////this.contextMenu1.Items.Add(mnuDealCard);
                }
                //if (mnuSelectedOrder != null && mnuSelectedOrder.Item.IsPharmacy)
                if (mnuSelectedOrder != null && mnuSelectedOrder.Item.ItemType == EnumItemType.Drug)
                {
                    ////ToolStripMenuItem mnuIMCard = new ToolStripMenuItem();
                    ////mnuIMCard.Text = "��ӡ��Һ���Ƶ�[��ݼ�:F12]";
                    ////mnuIMCard.Click += new EventHandler(mnuIMCard_Click);
                    ////this.contextMenu1.Items.Add(mnuIMCard);
                }
                if (this.bIsDesignMode)
                {
                    #region Ժע����
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
                        ToolStripMenuItem mnuInjectNum = new ToolStripMenuItem();//Ժע����
                        mnuInjectNum.Click += new EventHandler(mnumnuInjectNum_Click);

                        mnuInjectNum.Text = "���Ժע����[��ݼ�:F5]";
                        this.contextMenu1.Items.Add(mnuInjectNum);
                    }

                    //if (mnuSelectedOrder.Item.IsPharmacy && 
                    //    (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 4) && 
                    //    mnuSelectedOrder.InjectCount > 0)
                    if (mnuSelectedOrder.Item.ItemType == EnumItemType.Drug &&
                        (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 4) &&
                        mnuSelectedOrder.InjectCount > 0)
                    {
                        ToolStripMenuItem mnuInjectNum = new ToolStripMenuItem();//Ժע����
                        mnuInjectNum.Click += new EventHandler(mnumnuInjectNum_Click);

                        mnuInjectNum.Text = "�޸�Ժע����[��ݼ�:F5]";
                        this.contextMenu1.Items.Add(mnuInjectNum);
                    }
                    #endregion

                    #region ֹͣ�˵�
                    if (mnuSelectedOrder.Status == 0)
                    { //����
                        ToolStripMenuItem mnuDel = new ToolStripMenuItem();//ֹͣ
                        mnuDel.Click += new EventHandler(mnuDel_Click);
                        //mnuDel.Text = "ɾ��ҽ��[" + mnuSelectedOrder.Item.Name + "]";
                        mnuDel.Text = "ɾ����ѡ�����Ŀ";
                        this.contextMenu1.Items.Add(mnuDel);//ɾ��������
                    }
                    #region ����ҽ��{DFA920BD-AEB2-4371-B501-21CB87558147}
                    else if (mnuSelectedOrder.Status == 1)
                    {
                        ToolStripMenuItem mnuCancel = new ToolStripMenuItem();//ֹͣ
                        mnuCancel.Click += new EventHandler(mnuCancel_Click);
                        //mnuCancel.Text = "����ҽ��[" + mnuSelectedOrder.Item.Name + "]";
                        mnuCancel.Text = "������ѡ�����Ŀ";
                        this.contextMenu1.Items.Add(mnuCancel);//ɾ��������																							
                    }
                    #endregion
                    #endregion

                    #region ����ҽ��

                    ToolStripMenuItem mnuCopyAs = new ToolStripMenuItem();//����ҽ��Ϊ������
                    mnuCopyAs.Click += new EventHandler(mnuCopyAs_Click);

                    //mnuCopyAs.Text = "����" + "[" + mnuSelectedOrder.Item.Name + "]";
                    mnuCopyAs.Text = "������ѡ�����Ŀ";

                    this.contextMenu1.Items.Add(mnuCopyAs);
                    #endregion

                    #region ����
                    ToolStripMenuItem mnuUp = new ToolStripMenuItem("���ƶ�");//���ƶ�
                    mnuUp.Click += new EventHandler(mnuUp_Click);
                    if (this.neuSpread1.ActiveSheet.ActiveRowIndex <= 0) mnuUp.Enabled = false;
                    this.contextMenu1.Items.Add(mnuUp);
                    #endregion

                    #region ����
                    ToolStripMenuItem mnuDown = new ToolStripMenuItem("���ƶ�");//���ƶ�
                    mnuDown.Click += new EventHandler(mnuDown_Click);
                    if (this.neuSpread1.ActiveSheet.ActiveRowIndex >= this.neuSpread1.ActiveSheet.RowCount - 1 ||
                        this.neuSpread1.ActiveSheet.ActiveRowIndex < 0)
                    {
                        mnuDown.Enabled = false;
                    }
                    this.contextMenu1.Items.Add(mnuDown);
                    #endregion

                    #region �޸ļ۸�
                    if (mnuSelectedOrder.Status == 0)
                    {
                        ToolStripMenuItem mnuChangePrice = new ToolStripMenuItem("�޸ļ۸�");
                        mnuChangePrice.Click += new EventHandler(mnuChangePrice_Click);
                        this.contextMenu1.Items.Add(mnuChangePrice);
                    }
                    #endregion

                    #region ҽ����ƿ
                    ////if (mnuSelectedOrder.Status == 0 && mnuSelectedOrder.Item.IsPharmacy)
                    ////{
                    ////    ToolStripMenuItem mnuResumeOrder = new ToolStripMenuItem("ҽ����ƿ[��ݼ�:F6]");
                    ////    mnuResumeOrder.Click += new EventHandler(mnuResumeOrder_Click);
                    ////    this.contextMenu1.Items.Add(mnuResumeOrder);
                    ////}
                    #endregion

                    #region �����ӱ�
                    ////if (mnuSelectedOrder.Status == 0 && this.JudgeIsPCZ())
                    ////{
                    ////    ToolStripMenuItem mnuChangeQTY = new ToolStripMenuItem("�����ӱ�[��ݼ�:F7]");
                    ////    ////mnuChangeQTY.Click += new EventHandler(mnuChangeQTY_Click);
                    ////    this.contextMenu1.Items.Add(mnuChangeQTY);
                    ////}
                    #endregion

                    #region ȡ�����

                    ToolStripMenuItem mnuCancelCombo = new ToolStripMenuItem("ȡ�����");//���ƶ�
                    mnuCancelCombo.Click += new EventHandler(mnuCancelCombo_Click);
                    if (this.neuSpread1.ActiveSheet.SelectionCount < 0)
                    {
                        mnuCancelCombo.Enabled = false;
                    }
                    this.contextMenu1.Items.Add(mnuCancelCombo);

                    #endregion

                    #region ������{C6E229AC-A1C4-4725-BBBB-4837E869754E}

                    ToolStripMenuItem mnuSaveGroup = new ToolStripMenuItem("������");//������
                    mnuSaveGroup.Click += new EventHandler(mnuSaveGroup_Click);

                    this.contextMenu1.Items.Add(mnuSaveGroup);
                    #endregion

                    #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} ��Ӻ�����ҩ�Ҽ��˵�

                    if (this.IReasonableMedicine != null && this.IReasonableMedicine.PassEnabled)
                    {
                        //int i = 0;
                        //ToolStripMenuItem menuPass = new ToolStripMenuItem("������ҩ");
                        //this.contextMenu1.Items.Add(menuPass);

                        //ToolStripMenuItem m_al1ergic = new ToolStripMenuItem("����ʷ/����״̬");
                        //m_al1ergic.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_al1ergic);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("22") == 0)
                        //{
                        //    m_al1ergic.Enabled = false;
                        //}

                        //ToolStripMenuItem m_cpr = new ToolStripMenuItem("ҩ���ٴ���Ϣ�ο�");
                        //m_cpr.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_cpr);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("101") == 0)
                        //{
                        //    m_cpr.Enabled = false;
                        //}

                        //ToolStripMenuItem m_directions = new ToolStripMenuItem("ҩƷ˵����");
                        //m_directions.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_directions);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("102") == 0)
                        //{
                        //    m_directions.Enabled = false;
                        //}

                        //ToolStripMenuItem m_chp = new ToolStripMenuItem("�й�ҩ��");
                        //m_chp.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_chp);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("107") == 0)
                        //{
                        //    m_chp.Enabled = false;
                        //}

                        //ToolStripMenuItem m_cpe = new ToolStripMenuItem("������ҩ����");
                        //m_cpe.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_cpe);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("103") == 0)
                        //{
                        //    m_cpe.Enabled = false;
                        //}

                        //ToolStripMenuItem m_checkres = new ToolStripMenuItem("ҩ�����ֵ");
                        //m_checkres.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_checkres);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("104") == 0)
                        //{
                        //    m_checkres.Enabled = false;
                        //}

                        //ToolStripMenuItem m_lmim = new ToolStripMenuItem("�ٴ�������Ϣ�ο�");
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

                        //#region ҩƷר����Ϣ

                        //ToolStripMenuItem menuSpecialInfo = new ToolStripMenuItem("ר����Ϣ");
                        //menuPass.DropDownItems.Insert(i, menuSpecialInfo);
                        //i++;
                        //int j = 0;

                        //ToolStripMenuItem m_ddim = new ToolStripMenuItem("ҩ��-ҩ���໥����");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_ddim);
                        //m_ddim.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("201") == 0)
                        //{
                        //    m_ddim.Enabled = false;
                        //}

                        //ToolStripMenuItem m_dfim = new ToolStripMenuItem("ҩ��-ʳ���໥����");
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

                        //ToolStripMenuItem m_matchres = new ToolStripMenuItem("����ע�����������");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_matchres);
                        //m_matchres.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("203") == 0)
                        //{
                        //    m_matchres.Enabled = false;
                        //}

                        //ToolStripMenuItem m_trisselres = new ToolStripMenuItem("����ע�����������");
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

                        //ToolStripMenuItem m_ddcm = new ToolStripMenuItem("����֢");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_ddcm);
                        //m_ddcm.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("205") == 0)
                        //{
                        //    m_ddcm.Enabled = false;
                        //}
                        //ToolStripMenuItem m_side = new ToolStripMenuItem("������");
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

                        //ToolStripMenuItem m_geri = new ToolStripMenuItem("��������ҩ");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_geri);
                        //m_geri.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("207") == 0)
                        //{
                        //    m_geri.Enabled = false;
                        //}
                        //ToolStripMenuItem m_pedi = new ToolStripMenuItem("��ͯ��ҩ");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_pedi);
                        //m_pedi.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("208") == 0)
                        //{
                        //    m_pedi.Enabled = false;
                        //}
                        //ToolStripMenuItem m_preg = new ToolStripMenuItem("��������ҩ");
                        //menuSpecialInfo.DropDownItems.Insert(j, m_preg);
                        //m_preg.Click += new EventHandler(mnuPass_Click);
                        //j++;
                        //if (this.IReasonableMedicine.PassGetStateIn("209") == 0)
                        //{
                        //    m_preg.Enabled = false;
                        //}

                        //ToolStripMenuItem m_lact = new ToolStripMenuItem("��������ҩ");
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

                        //ToolStripMenuItem m_centerinfo = new ToolStripMenuItem("ҽҩ��Ϣ����");
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

                        //ToolStripMenuItem menuDrug = new ToolStripMenuItem("ҩƷ�����Ϣ");
                        //menuDrug.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, menuDrug);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("13") == 0)
                        //{
                        //    menuDrug.Enabled = false;
                        //}

                        //ToolStripMenuItem m_routematch = new ToolStripMenuItem("��ҩ;�������Ϣ");
                        //m_routematch.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_routematch);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("14") == 0)
                        //{
                        //    m_routematch.Enabled = false;
                        //}

                        //ToolStripMenuItem m_hospital_drug = new ToolStripMenuItem("ҽԺҩƷ��Ϣ");
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

                        //ToolStripMenuItem m_system_set = new ToolStripMenuItem("ϵͳ����");
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

                        //ToolStripMenuItem m_studydrug = new ToolStripMenuItem("��ҩ�о�");
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

                        //ToolStripMenuItem m_warn = new ToolStripMenuItem("����");
                        //m_warn.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_warn);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("11") == 0)
                        //{
                        //    m_warn.Enabled = false;
                        //}

                        //ToolStripMenuItem m_checkone = new ToolStripMenuItem("���");
                        //m_checkone.Click += new EventHandler(mnuPass_Click);
                        //menuPass.DropDownItems.Insert(i, m_checkone);
                        //i++;
                        //if (this.IReasonableMedicine.PassGetStateIn("3") == 0)
                        //{
                        //    m_checkone.Enabled = false;
                        //}

                    }

                    #endregion

                    //#region �ش�������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504 
                    //string isUseDL = feeManagement.GetControlValue("200212", "0");
                    //if (isUseDL == "1")
                    //{
                    //if (mnuSelectedOrder.ApplyNo != null && mnuSelectedOrder.ApplyNo != "")
                    //{
                    //    ToolStripMenuItem mnuPACSApply = new ToolStripMenuItem("�ش�������뵥");//���ƶ�
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
                    //    ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem("���Ƶ���������");//��ע
                    //    ////mnuCopyOrder.Click += new EventHandler(mnuCopyOrder_Click);
                    //    this.contextMenu1.Items.Add(mnuCopyOrder);
                    //}

                    #region ����ҽ��
                    ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem("����ҽ��");
                    mnuCopyOrder.Click += new EventHandler(mnuCopyOrder_Click);
                    this.contextMenu1.Items.Add(mnuCopyOrder);
                    #endregion

                    #endregion
                }
                #region ��Ӻ�����ҩ�Ҽ��˵�
                //if (this.EnabledPass && Pass.Pass.PassEnabled && mnuSelectedOrder.Item.IsPharmacy)
                //{
                //    MenuItem menuPass = new MenuItem("������ҩ");
                //    this.contextMenu1.MenuItems.Add(menuPass);

                //    MenuItem menuAllergn = new MenuItem("����ʷ/����״̬");
                //    menuAllergn.Click += new EventHandler(mnuPass_Click);
                //    menuPass.Items.Add(menuAllergn);

                //    if (Pass.Pass.PassGetState("101") != 0)
                //    {
                //        MenuItem menuCPR = new MenuItem("ҩ���ٴ���Ϣ�ο�");
                //        menuCPR.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCPR);
                //    }
                //    if (Pass.Pass.PassGetState("102") != 0)
                //    {
                //        MenuItem menuDIR = new MenuItem("ҩƷ˵����");
                //        menuDIR.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuDIR);
                //    }
                //    if (Pass.Pass.PassGetState("107") != 0)
                //    {
                //        MenuItem menuCHP = new MenuItem("�й�ҩ��");
                //        menuCHP.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCHP);
                //    }
                //    if (Pass.Pass.PassGetState("103") != 0)
                //    {
                //        MenuItem menuCPE = new MenuItem("������ҩ����");
                //        menuCPE.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCPE);
                //    }
                //    if (Pass.Pass.PassGetState("104") != 0)
                //    {
                //        MenuItem menuCHE = new MenuItem("ҩ�����ֵ");
                //        menuCHE.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuCHE);
                //    }
                //    if (Pass.Pass.PassGetState("220") != 0)
                //    {
                //        MenuItem menuLIM = new MenuItem("�ٴ�������Ϣ�ο�");
                //        menuLIM.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuLIM);
                //    }
                //    #region ҩƷר����Ϣ
                //    MenuItem menuSpecialInfo = new MenuItem("ר����Ϣ");
                //    menuPass.Items.Add(menuSpecialInfo);

                //    if (Pass.Pass.PassGetState("201") != 0)
                //    {
                //        MenuItem menuDDim = new MenuItem("ҩ��-ҩ���໥����");
                //        menuSpecialInfo.MenuItems.Add(menuDDim);
                //        menuDDim.Click += new EventHandler(mnuPass_Click);
                //    }

                //    if (Pass.Pass.PassGetState("202") != 0)
                //    {
                //        MenuItem menuDFim = new MenuItem("ҩ��-ʳ���໥����");
                //        menuSpecialInfo.Items.Add(menuDFim);
                //        menuDFim.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("203") != 0)
                //    {
                //        MenuItem menuMACH = new MenuItem("����ע�����������");
                //        menuSpecialInfo.Items.Add(menuMACH);
                //        menuMACH.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("204") != 0)
                //    {
                //        MenuItem menuTRI = new MenuItem("����ע�����������");
                //        menuSpecialInfo.Items.Add(menuTRI);
                //        menuTRI.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("205") != 0)
                //    {
                //        MenuItem menuDDCM = new MenuItem("����֢");
                //        menuSpecialInfo.MenuItems.Add(menuDDCM);
                //        menuDDCM.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("206") != 0)
                //    {
                //        MenuItem menuSID = new MenuItem("������");
                //        menuSpecialInfo.Items.Add(menuSID);
                //        menuSID.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("207") != 0)
                //    {
                //        MenuItem menuOLD = new MenuItem("��������ҩ");
                //        menuSpecialInfo.Items.Add(menuOLD);
                //        menuOLD.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("208") != 0)
                //    {
                //        MenuItem menuPED = new MenuItem("��ͯ��ҩ");
                //        menuSpecialInfo.Items.Add(menuPED);
                //        menuPED.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("209") != 0)
                //    {
                //        MenuItem menuPREG = new MenuItem("��������ҩ");
                //        menuSpecialInfo.Items.Add(menuPREG);
                //        menuPREG.Click += new EventHandler(mnuPass_Click);
                //    }
                //    if (Pass.Pass.PassGetState("210") != 0)
                //    {
                //        MenuItem menuACT = new MenuItem("��������ҩ");
                //        menuSpecialInfo.Items.Add(menuACT);
                //        menuACT.Click += new EventHandler(mnuPass_Click);
                //    }
                //    #endregion
                //    if (Pass.Pass.PassGetState("106") != 0)
                //    {
                //        MenuItem menuCENter = new MenuItem("ҽҩ��Ϣ����");
                //        menuCENter.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuCENter);
                //    }
                //    if (Pass.Pass.PassGetState("13") != 0)
                //    {
                //        MenuItem menuDrug = new MenuItem("ҩƷ�����Ϣ");
                //        menuDrug.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuDrug);
                //    }
                //    if (Pass.Pass.PassGetState("14") != 0)
                //    {
                //        MenuItem menuUsage = new MenuItem("��ҩ;�������Ϣ");
                //        menuUsage.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuUsage);
                //    }
                //    if (Pass.Pass.PassGetState("11") != 0)
                //    {
                //        MenuItem menuSystem = new MenuItem("ϵͳ����");
                //        menuSystem.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuSystem);
                //    }
                //    if (Pass.Pass.PassGetState("12") != 0)
                //    {
                //        MenuItem menuResearch = new MenuItem("��ҩ�о�");
                //        menuResearch.Click += new EventHandler(mnuPass_Click);
                //        menuPass.MenuItems.Add(menuResearch);
                //    }
                //    if (Pass.Pass.PassGetState("3") != 0)
                //    {
                //        MenuItem menuWarn = new MenuItem("����");
                //        menuWarn.Click += new EventHandler(mnuPass_Click);
                //        menuPass.Items.Add(menuWarn);

                //        if (this.fpSpread1.Sheets[0].Cells[c.Row, GetColumnIndexFromName("��")].Tag != null && this.fpSpread1.Sheets[0].Cells[c.Row, GetColumnIndexFromName("��")].Tag.ToString() != "0")
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
                //        MenuItem menuCheck = new MenuItem("���");
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
        /// �Ҽ�ȡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuCancelCombo_Click(object sender, EventArgs e)
        {
            this.CancelCombo();
        }

        /// <summary>
        /// ɾ�������ϡ�ֹͣҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDel_Click(object sender, EventArgs e)
        {
            this.Del();
        }

        #region ����ҽ�����շѺ�ҽ�����������ϣ��������������ٴ򿪣�{DFA920BD-AEB2-4371-B501-21CB87558147}
        /// <summary>
        /// ����ҽ��
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

            DialogResult r = ucOutPatientItemSelect1.MessageBoxShow("�Ƿ�ȷ��Ҫ���ϸ���ҽ��,�˲������ܳ�����", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

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
                        ucOutPatientItemSelect1.MessageBoxShow("����ҽ��" + temp.Item.Name + "ʧ��");
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
                            ucOutPatientItemSelect1.MessageBoxShow("����ҽ��" + order.Item.Name + "�޸���Ϣʧ��");
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

                    #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
                    string isUseDL = feeManagement.GetControlValue("200212", "0");
                    if (isUseDL == "1")
                    {
                        if (order.ApplyNo != null)
                        {
                            if (PACSApplyInterface == null)
                            {
                                if (InitPACSApplyInterface() < 0)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("��ʼ���������뵥�ӿ�ʱ����");
                                    return;
                                }
                            }
                            PACSApplyInterface.DeleteApply(order.ApplyNo);
                            //if (PACSApplyInterface.DeleteApply(order.ApplyNo) < 0)
                            //{
                            //    ucOutPatientItemSelect1.MessageBoxShow("���ϵ������뵥ʱ����");
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
        /// ����
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

                    //�������Һ�ִ�п�����ͬ������Ϊ�Ǳ�����ִ����Ŀ��ִ�п�����ȡ
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

                        //�ж�ȱҩ��ͣ��
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
                    ucOutPatientItemSelect1.MessageBoxShow(IBeforeAddItem.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            for (int i = 0; i < alCopyList.Count; i++)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order ord = alCopyList[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                #region �жϿ���Ȩ��

                string error = "";

                int ret = 1;

                //�ȼ�ҩƷ
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
        /// ����ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuUp_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex <= 0)
                return;

            Neusoft.HISFC.Models.Order.OutPatient.Order upOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex - 1, this.neuSpread1.ActiveSheetIndex).Clone();
            Neusoft.HISFC.Models.Order.OutPatient.Order downOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex).Clone();

            //������ƶ�
            if (upOrder.Combo.ID == downOrder.Combo.ID)
            {
                upOrder.SortID += 1;
                AddObjectToFarpoint(upOrder, this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

                downOrder.SortID -= 1;
                AddObjectToFarpoint(downOrder, this.neuSpread1.ActiveSheet.ActiveRowIndex - 1, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

                this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex - 1, GetColumnIndexFromName("˳���")].Tag = "����";
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
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag = "����";
                    }
                }
            }

            this.neuSpread1.Sheets[0].SortRows(GetColumnIndexFromName("˳���"), true, false, rowCompare);
            Order.Classes.Function.DrawCombo(this.neuSpread1.Sheets[0], GetColumnIndexFromName("��Ϻ�"), GetColumnIndexFromName("���"));

            for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
            {
                if (this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag != null
                    && this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag.ToString() == "����")
                {
                    this.neuSpread1.ActiveSheet.ActiveRowIndex = i;
                    this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
                    this.ucOutPatientItemSelect1.CurrentRow = ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    this.neuSpread1.ActiveSheet.AddSelection(ActiveRowIndex, 0, 1, this.neuSpread1.ActiveSheet.ColumnCount);
                    this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;

                    this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag = null;
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
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex >= this.neuSpread1.ActiveSheet.RowCount - 1)
                return;

            Neusoft.HISFC.Models.Order.OutPatient.Order upOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex).Clone();
            Neusoft.HISFC.Models.Order.OutPatient.Order downOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex + 1, this.neuSpread1.ActiveSheetIndex).Clone();

            //������ƶ�
            if (upOrder.Combo.ID == downOrder.Combo.ID)
            {
                upOrder.SortID += 1;
                AddObjectToFarpoint(upOrder, this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);

                this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("˳���")].Tag = "����";

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
                        this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag = "����";
                    }
                }
            }

            this.neuSpread1.Sheets[0].SortRows(GetColumnIndexFromName("˳���"), true, false, rowCompare);
            Order.Classes.Function.DrawCombo(this.neuSpread1.Sheets[0], GetColumnIndexFromName("��Ϻ�"), GetColumnIndexFromName("���"));

            for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
            {
                if (this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag != null
                    && this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag.ToString() == "����")
                {
                    this.neuSpread1.ActiveSheet.ActiveRowIndex = i;
                    this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
                    this.ucOutPatientItemSelect1.CurrentRow = ActiveRowIndex;
                    this.currentOrder = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    this.neuSpread1.ActiveSheet.AddSelection(ActiveRowIndex, 0, 1, this.neuSpread1.ActiveSheet.ColumnCount);
                    this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;

                    this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("˳���")].Tag = null;
                    break;
                }
            }

            this.SelectionChanged();
        }

        /// <summary>
        /// ��������Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangePrice_Click(object sender, EventArgs e)
        {
            Forms.frmPopShow frm = new Forms.frmPopShow();
            frm.Text = "����ĿΪ��������Ŀ��������۸�";
            frm.isPrice = true;
            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;
            if (order.Item.Price != 0 && order.Item.User03 != "������")
            {
                ucOutPatientItemSelect1.MessageBoxShow("����Ŀ������������Ŀ�������޸ļ۸�");
                return;
            }
            frm.ModuleName = order.Item.Price.ToString();
            if (order == null)
            {
                return;
            }
            frm.ShowDialog();
            order.Item.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(frm.ModuleName);
            order.Item.User03 = "������";
            this.ucOutPatientItemSelect1.OperatorType = Operator.Modify;
            this.ucItemSelect1_OrderChanged(order, EnumOrderFieldList.Item);
        }

        /// <summary>
        /// Ժע
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
        /// ճ��ҽ��{DF8058FF-72C0-404f-8F36-6B4057B6F6CD}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPasteOrder_Click(object sender, EventArgs e)
        {
            this.PasteOrder();
        }

        /// <summary>
        ///  �޸��ش�������뵥
        /// {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPACSApply_Click(object sender, EventArgs e)
        {
            if (PACSApplyInterface == null)
            {
                if (InitPACSApplyInterface() < 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("��ʼ���������뵥�ӿ�ʱ����");
                    return;
                }
            }
            Neusoft.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as Neusoft.HISFC.Models.Order.OutPatient.Order;

            if (order.ApplyNo == null || order.ApplyNo == "")
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ҽ����δ���棬���ȱ��棡");
                return;
            }

            if (PACSApplyInterface.UpdateApply(order.ApplyNo) < 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("�޸��ش�������뵥ʱ����");
                return;
            }
        }

        /// <summary>
        /// ����ҽ��
        /// {7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyOrder_Click(object sender, EventArgs e)
        {
            this.CopyOrder();
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

        #endregion

        #region ��ݼ�

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
        /// ��ݼ�
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

        #region �¼ӵĺ���
        protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����ҽ��", 9, true, false, null);
            toolBarService.AddToolButton("���", "���ҽ��", 9, true, false, null);
            ////toolBarService.AddToolButton("������", "��������", 9, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��ҽ��", 9, true, false, null);
            toolBarService.AddToolButton("ȡ�����", "ȡ�����ҽ��", 9, true, false, null);
            ////toolBarService.AddToolButton("��ϸ", "������ϸ", 9, true, true, null);
            toolBarService.AddToolButton("�˳�ҽ������", "�˳�ҽ������", 9, true, false, null);
            toolBarService.AddToolButton("����", "����", 9, true, false, null);
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
            else if (e.ClickedItem.Text == "����")
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

        #region IInterfaceContainer ��Ա
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[7];
                t[0] = typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint);
                t[1] = typeof(Neusoft.HISFC.BizProcess.Interface.Common.ICheckPrint);//������뵥
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
        /// ������ӡ
        /// </summary>
        /// <param name="isRecipeView">�Ƿ�Ԥ����ӡ</param>
        public int PrintRecipe(bool isRecipeView)
        {
            if (this.EditGroup)
            {
                ucOutPatientItemSelect1.MessageBoxShow("�����ڱ༭���ף���ʱ��֧�ִ�ӡ������");
                return -1;
            }

            if (iRecipePrint == null)
            {
                iRecipePrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint)) as Neusoft.HISFC.BizProcess.Interface.IRecipePrint;
            }

            if (iRecipePrint == null)
            {
                this.accountMgr.Err = "������ӡ�ӿ�δʵ�֣�";
                this.accountMgr.WriteErr();
                return 1;
                //ucOutPatientItemSelect1.MessageBoxShow("�ӿ�δʵ��");
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
                        if (ucOutPatientItemSelect1.MessageBoxShow("�Ƿ��ӡ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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
        /// ���ҩƷ��������
        /// </summary>
        /// <param name="isAfterSaveOrder">�Ƿ񱣴洦�������</param>
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
        /// ����Ϊxml�ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            Neusoft.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[0], SetingFileName);

        }

        #region ������ҩ

        //Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;

        ///// <summary>
        ///// ��ȡҩƷ�Զ�����
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
        /// ��ʼ��IReasonableMedicin
        /// </summary>
        private void InitReasonableMedicine()
        {
            if (this.IReasonableMedicine == null)
            {
                this.IReasonableMedicine = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IReasonableMedicine)) as Neusoft.HISFC.BizProcess.Interface.Order.IReasonableMedicine;
            }
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
        /// ������ҩϵͳ�в鿴�����
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
        /// ��ѯҩƷ������ҩ��Ϣ
        /// </summary>
        /// <param name="e"></param>
        public void PassSetQuery(FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    return;
            //}

            //if (!e.RowHeader && !e.ColumnHeader && (e.Column == GetColumnIndexFromName("ҽ������")))
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
            //    if (e.Column == GetColumnIndexFromName("ҽ������"))
            //    {
            //        #region ҩƷ��ѯ
            //        try
            //        {
            //            //ò������ֻ�����½ǵ�����λ�����
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
            //    if (e.Column == GetColumnIndexFromName("ҽ������"))
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
        /// �������ҩϵͳ���͵�ǰҽ���������
        /// </summary>
        /// <param name="warnPicFlag">�Ƿ���ʾͼƬ������Ϣ</param>
        ///<param name="checkType">��鷽ʽ 1 �Զ���� 12 ��ҩ�о�  3 �ֹ����</param>
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
        /// �˳�������ҩ
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
        /// ������ҩҽ�����
        /// </summary>
        /// <param name="alOrder">�����ҽ���б�</param>
        ///<param name="warnPicFlag">�Ƿ���ʾͼƬ������Ϣ</param>
        public void PassSaveCheck(List<Neusoft.HISFC.Models.Order.OutPatient.Order> alOrder, int checkType, bool warnPicFlag)
        {
            if (IReasonableMedicine != null && !this.IReasonableMedicine.PassEnabled)
            {
                return;
            }
            //if (this.IReasonableMedicine.PassSaveCheck(this.currentPatientInfo, alOrder, checkType) == -1)
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("���ѱ���ҽ�����к�����ҩ������!");
            //}
            //if (!warnPicFlag)//������ʾ ֱ�ӷ���
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
        /// ��Ӻ�����ҩ���������־
        /// </summary>
        /// <param name="iRow">������������</param>
        /// <param name="iSheet">������Sheet����</param>
        /// <param name="warnFlag">������־</param>
        public void AddWarnPicturn(int iRow, int iSheet, int warnFlag)
        {
            string picturePath = Application.StartupPath + "\\pic";
            switch (warnFlag)
            {
                case 0:										//0 (��ɫ)������
                    picturePath = picturePath + "\\0.gif";
                    break;
                case 1:										//1 (��ɫ)Σ���ϵͻ��в���ȷ
                    picturePath = picturePath + "\\1.gif";
                    break;
                case 2:										//2 (��ɫ)���Ƽ��������Σ��
                    picturePath = picturePath + "\\2.gif";
                    break;
                case 3:										// 3 (��ɫ)���Խ��ɡ������������Σ��
                    picturePath = picturePath + "\\3.gif";
                    break;
                case 4:										//4 (��ɫ)���û���һ��Σ�� 
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
                this.neuSpread1.Sheets[iSheet].Cells[iRow, 0].CellType = t;			//ҽ������
                this.neuSpread1.Sheets[iSheet].Cells[iRow, 0].Tag = "1";							//���������
                this.neuSpread1.Sheets[iSheet].Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("���ú�����ҩ�������ʾ�����г���!" + ex.Message);
            }
        }

        /// <summary>
        /// �������ҩϵͳ���͵�ǰ����ѯҩƷ��Ϣ
        /// </summary>
        /// <param name="checkType">��ѯ��ʽ</param>
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
        /// ����ҩƷϵͳҩƷ��ѯ
        /// </summary>
        private void mnuPass_Click(object sender, EventArgs e)
        {
            if (IReasonableMedicine != null && !this.IReasonableMedicine.PassEnabled)
                return;
            ToolStripItem muItem = sender as ToolStripItem;
            //switch (muItem.Text)
            //{

            //    #region {BF58E89A-37A8-489a-A8F6-5BA038EAE5A7} ��Ӻ�����ҩ�Ҽ��˵�

            //    #region һ���˵�

            //    case "����ʷ/����״̬":
            //        int iReg;
            //        this.IReasonableMedicine.PassSetPatientInfo(this.currentPatientInfo, this.OrderManagement.Operator.ID, this.OrderManagement.Operator.Name);
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
        }

        #endregion

        #region ������ʾ����

        /// <summary>
        /// ������ʾ����
        /// </summary>
        /// <param name="isRegFeeOnly">������ȥֻ����Һŷ�</param>
        /// <returns></returns>
        public int CalculatSubl(bool isRegFeeOnly)
        {
            if (this.dealSublMode == 1)
            {
                ArrayList alOrder = new ArrayList();
                Neusoft.HISFC.Models.Order.OutPatient.Order order = null;

                if (!isRegFeeOnly)
                {
                    #region ������

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
                            //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
                            if (newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            if (newOrder.Status != 0 || newOrder.IsHaveCharged)//��鲢��ҽ��״̬
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                ucOutPatientItemSelect1.MessageBoxShow("���㸽�Ĵ���\r\n[" + order.Item.Name + "]�����Ѿ��շ�,���˳������������½���!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }

                            if (order != null)
                            {
                                alOrder.Add(order);
                            }
                            if (order != null && order.IsSubtbl)
                            {
                                if (order.Memo == "�Һŷ�")
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
                                    ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + errText, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }

                    Neusoft.FrameWork.Management.PublicTrans.Commit();

                    #endregion
                }

                #region ����Һŷ�
                if (this.dealSublMode == 1)
                {
                    if ((this.isAddRegSubBeforeAddOrder && isRegFeeOnly)
                        || (!isAddRegSubBeforeAddOrder && !isRegFeeOnly))
                    {
                        if (this.SetSupplyRegFee(ref alSupplyFee, ref errInfo, currentPatientInfo.IsFee) == -1)
                        {
                            this.alSupplyFee = new ArrayList();
                            ucOutPatientItemSelect1.MessageBoxShow("���չҺŷ�ʧ�ܣ�" + errInfo);
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
                            //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
                            if (newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            if (newOrder.Status != 0 || newOrder.IsHaveCharged)//��鲢��ҽ��״̬
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                ucOutPatientItemSelect1.MessageBoxShow("���㸽�Ĵ���\r\n[" + order.Item.Name + "]�����Ѿ��շ�,���˳������������½���!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }

                            if (order.IsSubtbl)
                            {
                                if (order.Memo == "�Һŷ�")
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
                                //�������ҽ������
                                newOrder = new Neusoft.HISFC.Models.Order.OutPatient.Order();//��������ҽ��

                                item = feeManagement.GetItem(itemObj.Item.ID);//���������Ŀ��Ϣ
                                if (item == null)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("���㸽��ʱ��������Ŀʧ�ܣ�" + feeManagement.Err);
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

                                    newOrder.Combo = orderTemp.Combo.Clone();//��Ϻ�
                                    newOrder.ReciptSequence = orderTemp.ReciptSequence;

                                    //newOrder.ID = orderIntegrate.GetNewOrderID();//ҽ����ˮ��
                                    //if (newOrder.ID == "")
                                    //{
                                    //    ucOutPatientItemSelect1.MessageBoxShow("������Ŀ����ʱ���������ӵĸ��Ļ��ҽ����ˮ�ų���" + orderIntegrate.Err);
                                    //    return -1;
                                    //}

                                    newOrder.Item.ItemType = Neusoft.HISFC.Models.Base.EnumItemType.UnDrug;

                                    newOrder.DoseUnit = "";

                                    newOrder.IsEmergency = orderTemp.IsEmergency;
                                    newOrder.IsSubtbl = true;
                                    newOrder.Usage = new Neusoft.FrameWork.Models.NeuObject();
                                    newOrder.SequenceNO = -1;
                                    if (newOrder.ExeDept.ID == "")//ִ�п���Ĭ��
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
                                    ucOutPatientItemSelect1.MessageBoxShow("������Ŀ����ʱ����������ҽ����������" + ex.Message);
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
                                    orderObj.Memo = "�Һŷ�";

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

        #region �޸ĺ�ͬ��λ��Ϣ

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

                #region �ӿ��жϺ�ͬ��λ����

                if (this.iCheckPactInfo == null)
                {
                    this.iCheckPactInfo = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.Common.ICheckPactInfo)) as Neusoft.HISFC.BizProcess.Interface.Common.ICheckPactInfo;
                }
                if (this.iCheckPactInfo == null)
                {
                    //if (!string.IsNullOrEmpty(Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err))
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow("��ýӿ�ICheckPactInfo����,�����޷��жϺ�ͬ��λ����Ч�ԣ�\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                else
                {
                    iCheckPactInfo.PatientInfo = patientInfo;
                    if (iCheckPactInfo.CheckIsValid() == -1)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(iCheckPactInfo.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                #endregion

                if (this.regManagement.UpdateRegInfoByClinicCode(patientInfo) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    this.currentPatientInfo.Pact = pactTemp.Clone();
                    this.cmbPact.Text = pactTemp.Name;
                    ucOutPatientItemSelect1.MessageBoxShow("���º�ͬ��λ��Ϣ����" + regManagement.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ������Ŀ������Ƿ�ɼ�
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetInputItemVisible(bool isVisible)
        {
            this.ucOutPatientItemSelect1.SetInputControlVisible(isVisible);
        }
    }

    /// <summary>
    /// ���������
    /// </summary>
    class RowCompare : IComparer
    {
        #region IComparable ��Ա

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