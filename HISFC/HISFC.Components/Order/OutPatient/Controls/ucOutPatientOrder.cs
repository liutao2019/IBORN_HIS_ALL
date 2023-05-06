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
    /// ����ҽ��վ
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


        /// <summary>
        /// ÿ��������ֵ����С��λ������)
        /// </summary>
        private decimal doceOnceLimit = -1;

        #endregion

        #region ����

        /// <summary>
        /// �Һ���Ч����
        /// </summary>
        public int validRegDays = 1;

        /// <summary>
        /// �洢ҽ������
        /// </summary>
        protected DataSet dtOrder = null;

        /// <summary>
        /// �洢ҽ������
        /// </summary>
        protected DataView dvOrder = null;

        /// <summary>
        /// �Ƿ����ڱ༭����
        /// </summary>
        protected bool EditGroup = false;

        /// <summary>
        /// ȫ��ҽ����Ϣ
        /// </summary>
        public ArrayList alAllOrder = new ArrayList();

        /// <summary>
        /// ��ǰ��ҽ����Ϣ
        /// </summary>
        protected FS.HISFC.Models.Order.OutPatient.Order currentOrder = null;

        /// <summary>
        /// ��ǰ����
        /// </summary>
        protected FS.HISFC.Models.Registration.Register currentPatientInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ҩƷҵ��
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ��ǰ��̨
        /// </summary>
        protected FS.FrameWork.Models.NeuObject currentRoom = null;

        /// <summary>
        /// ������ʾ�����ļ�
        /// </summary>
        private string SetingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\clinicordersetting.xml";

        /// <summary>
        /// ��ͣ��ʾ
        /// </summary>
        ToolTip tooltip = new ToolTip();

        /// <summary>
        /// ������ӡ�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.IRecipePrint iRecipePrint = null;

        /// <summary>
        /// �Ҽ��˵�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = null;

        /// <summary>
        /// �洢��ϱ仯��ҽ���Ĺ�ϣ��
        /// </summary>
        private Hashtable hsComboChange = new Hashtable();

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        private string errInfo = "";

        /// <summary>
        /// ��ҩ��������
        /// </summary>
        FS.HISFC.Components.Order.Controls.ucHerbalOrder ucHerbal = null;

        #region ���Һ���

        /// <summary>
        /// ����Ŷ�Ӧ��������Ŀ
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug emergRegItem = null;

        /// <summary>
        /// ҽ��ְ�ƶ�Ӧ��������Ŀ
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug diagItem = null;

        /// <summary>
        /// �ҺŷѲ����Ŀ
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug diffDiagItem = null;

        /// <summary>
        /// ���յĹҺŷ���Ŀ
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug regItem = null;

        /// <summary>
        /// ��ҺŷѵĿ���
        /// </summary>
        private Hashtable hsNoSupplyRegDept = new Hashtable();

        /// <summary>
        /// ���ξ����ѿ�����ҽ��
        /// </summary>
        private ArrayList SameOrderList = new ArrayList();

        /// <summary>
        /// �����������շѵ�δִ�е�ҽ��
        /// </summary>
        private ArrayList LastOrderList = new ArrayList();

        /// <summary>
        /// ��ǰ����Ա
        /// ���ݿ���ȡ��
        /// </summary>
        FS.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        private ArrayList alSupplyFee = new ArrayList();

        /// <summary>
        /// �Ƿ����÷���ϵͳ 1 ���� ���� ������
        /// </summary>
        private bool isUseNurseArray;

        /// <summary>
        /// ����ҽ���Ƿ��Զ���ӡ����
        /// </summary>
        private bool isAutoPrintRecipe = false;

        /// <summary>
        /// ��ͬ��λ������
        /// </summary>
        //private FS.FrameWork.Public.ObjectHelper pactHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���Ŀ��1
        /// </summary>
        public Dictionary<string, string> PurPose = null;

        #endregion

        /// <summary>
        /// ���Ĵ���ģʽ��0 ������Զ�������1 ���������㣬��ʾ�ڽ����������޸�
        /// </summary>
        private int dealSublMode = -1;

        /// <summary>
        /// �Ƿ��Ѿ���ʼ���Һŷѵ���Ŀ�� Ϊ�˼��ؿ�һЩ���ѳ�ʼ���Һŷѷŵ�����������ʱ��
        /// </summary>
        private bool isInitSupplyItem = false;

        /// <summary>
        /// �Ƿ��Զ������ҺŷѺ���𡢲���
        /// 0 ��������1 �Զ�������2 ֻ�������͹Һŷѣ�3 ֻ���ղ���
        /// </summary>
        private int isAutoAddSupplyFee = 1;

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
        /// �Ƿ������޸�ÿ�ο�������ʱ�Ĵ�����ͬ��λ��Ϣ
        /// houwb ҽ�����Էѷ����� {A1FE7734-267C-43f1-A824-BF73B482E0B2}
        /// </summary>
        private bool isAllowChangeRecipePactInfo = false;

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

        /// <summary>
        /// �Ƿ���ʾͬһ�ξ����п����ظ���Ŀ��δִ����Ŀ
        /// </summary>
        protected bool isShowSameOrder = false;

        /// <summary>
        /// �Ƿ���ҩƷ��������ʾ���ͼ۸�
        /// 0 ������ʾ��1 ��ʾ���2 ��ʾ�۸�3 ��񡢼۸���ʾ
        /// </summary>
        private int isShowSpecsAndPrice = 0;


        /// <summary>
        /// �Ƿ�����ҽ������治���ҩƷ��0������1 ��ʾ��2 ����
        /// </summary>
        private int isCheckDrugStock = 0;

        /// <summary>
        /// �Ƿ�Ĭ��ѡ�е�������Ƶ�Ρ��÷�ȫ���޸ģ������¼�������
        /// 000 ��λ���ֱַ��ʾ��������Ƶ�Ρ��÷�
        /// </summary>
        private string isChangeAllSelect = "-1";

        /// <summary>
        /// �Ƿ��ڿ���֮ǰ��ӹҺŷ���Ŀ (���dealSublMode=1 ������ʾ����ģʽ ʹ��)
        /// </summary>
        private bool isAddRegSubBeforeAddOrder = false;

        /// <summary>
        /// ��ǰ�˻������ʾ��Ϣ
        /// </summary>
        private string vacancyDisplay = "";


        /// <summary>
        /// �Ƿ�ҽ��վԤ�ۿ��
        /// </summary>
        private bool isPreUpdateStockinfoByOrder = false;

        /// <summary>
        /// ����ҽ��վƤ�Դ���ģʽ��0 ����ʾƤ����Ϣ 1����ʾ�Ƿ�2����������ѡ��
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
        /// 0 ���жϣ�1 �ж�ҩƷ��2 �ж�ҩƷ�ͷ�ҩƷ��3 �ж�ҩƷ����ҩƷ�����
        /// </summary>
        private string isJudgeDiagnose = "0";

        /// <summary>
        /// �Ƿ�����޸�Ժע
        /// </summary>
        private bool isCanModifiedInjectNum = true;

        /// <summary>
        /// ҩƷ�ͷ�ҩƷ�ļ۸�Ϊ0�Ƿ��շѣ�0����ȡ��1��ȡ��Ĭ��ֵΪ0����ȡ
        /// </summary>
        private string isFeeWhenPriceZero = "-1";

        #endregion

        #region �ӿ�

        /// <summary>
        /// ����������뵥
        /// </summary>
        //protected FS.ApplyInterface.HisInterface PACSApplyInterface = null;

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��ͬ��λУ��ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Common.ICheckPactInfo iCheckPactInfo = null;

        /// <summary>
        /// ҽ����Ϣ����ӿ�
        /// {48E6BB8C-9EF0-48a4-9586-05279B12624D}
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IAlterOrder IAlterOrderInstance = null;

        /// <summary>
        /// ������뵥��ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Common.ICheckPrint checkPrint = null;

        /// <summary>
        /// ֱ���շѽӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee IDoctFee = null;

        /// <summary>
        /// ҽ��վ���Ĵ���ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IDealSubjob IDealSubjob = null;

        /// <summary>
        /// ������ҩ�ӿ�
        /// </summary>
        IReasonableMedicine IReasonableMedicine = null;

        /// <summary>
        /// ����󴦷�����ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.ISaveOrder IAfterSaveOrder = null;

        /// <summary>
        /// ���洦��ǰ����
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder IBeforeSaveOrder = null;

        /// <summary>
        /// ������Ŀǰ�����ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem IBeforeAddItem = null;

        /// <summary>
        /// ��������ǰ�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddOrder IBeforeAddOrder = null;

        /// <summary>
        /// ���ﴦ����ӡ
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint IOutPatientPrint = null;
        /// <summary>
        /// ԤԼ��Ժ// {6BF1F99D-7307-4d05-B747-274D24174895}
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IPrePayIn IPrePayIn = null;

        /// <summary>
        /// ��������ӿ�����
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;

        /// <summary>
        /// ���������սк���Ϣ
        /// </summary>
        private FS.SOC.HISFC.CallQueue.Interface.INurseAssign INurseAssign = null;

        #endregion

        #region ������

        /// <summary>
        /// ҽ��������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper orderHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// Ƶ�ΰ�����
        /// </summary>
        //private FS.FrameWork.Public.ObjectHelper frequencyHelper;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        //protected FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������������ְࣺ����������Ŀ����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper diagFeeConstHelper = new FS.FrameWork.Public.ObjectHelper();

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
        /// ҽ���ӿ�
        /// </summary>
        //FS.HISFC.BizLogic.Fee.Interface CacheManager.InterfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
        /// <summary>
        /// ҽ����չ��Ϣ����{97B9173B-834D-49a1-936D-E4D04F98E4BA}
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.OrderExtend orderExtMgr = new FS.HISFC.BizLogic.Order.OutPatient.OrderExtend();

        /// ҽ����չ������Ϣ����
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.OrderLisExtend orderExtMgr2 = new FS.HISFC.BizLogic.Order.OutPatient.OrderLisExtend();

        /// <summary>
        /// ���ҽ��������Ŀ
        /// </summary>
        Hashtable hsCompareItems = null;

        /// <summary>
        /// �Ƿ���ʾҽ�����ձ��
        /// </summary>
        private bool isShowPactCompareFlag = true;

        /// <summary>
        /// �Ű����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schemgManager = new FS.HISFC.BizLogic.Registration.Schema();
        #endregion

        /// <summary>
        /// �Ƿ��ڿ���״̬�����������β�ѯ���ݿ�
        /// </summary>
        protected bool isAddMode = false;

        private string varCombID = "";//��ʱ����Ϻű���

        private string varTempUsageID = "zuowy";//��ʱ�÷�
        private string varOrderUsageID = "maokb";//ҽ���÷�


        private string previousClinicNo = ""; //ǰһ���Һű���


        /// <summary>
        /// ҽ����������ҩҩƷ�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper indicationsHelper = null;

        /// <summary>
        /// ҽ����������ҩ��ҩ
        /// </summary>
        private ArrayList alIndicationsDrug = null;

        /// <summary>
        /// �洢�к�
        /// </summary>
        int[] iColumns;

        /// <summary>
        /// �洢�п�
        /// </summary>
        int[] iColumnWidth;

        /// <summary>
        /// �洢�еĿɼ���
        /// </summary>
        bool[] iColumnVisible;

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

                    //����ͬһ���ˣ������Ĭ��������Ϣ��
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
                                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ�˻�������" + CacheManager.AccountMgr.Err);
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

                            #region ���ú�ͬ��λ��Ϣ

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


                            #region ������ʱ����ѡ��ҽ�����Էѷ�����

                            //houwb ҽ�����Էѷ����� {A1FE7734-267C-43f1-A824-BF73B482E0B2}

                            pnOrderPactInfo.Visible = false;

                            if (isAllowChangeRecipePactInfo)
                            {
                                this.pnOrderPactInfo.Visible = true;

                                if (CacheManager.AccountMgr.GetPatientPactInfo(currentPatientInfo) == -1)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ���ߺ�ͬ��λ��Ϣʧ�ܣ�" + CacheManager.AccountMgr.Err);
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
        /// ��ǰ��̨
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
        /// ��������
        /// </summary>
        protected FS.FrameWork.Models.NeuObject reciptDept = null;

        /// <summary>
        /// ���ÿ�������
        /// </summary>
        [DefaultValue(null)]
        public void SetReciptDept(FS.FrameWork.Models.NeuObject value)
        {
            this.reciptDept = value;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.reciptDept == null)
                {
                    //2012-10-9 11:20:56 houwb 
                    //��ɽ������һ��ҽ��ͬʱ���������ҵ���������˰����ֻ��һ��
                    //�޸�Ϊ�������Ҹ��ݵ�½����ȡ��ҽ����½��������Լ��������ˣ�

                    ////������Ű���Ϣ��ȥ�Ű������Ϊ�������� {231D1A80-6BF6-413f-8BBF-727DC2BF47D9}
                    //FS.HISFC.Models.Registration.Schema schema = CacheManager.RegInterMgr.GetSchema(GetReciptDoct().ID, this.CacheManager.OrderMgr.GetDateTimeFromSysDateTime());
                    //if (schema != null && schema.Templet.Dept.ID != "")
                    //{
                    //    this.reciptDept = schema.Templet.Dept.Clone();
                    //}
                    ////û���Ű�ȡ��½������Ϊ��������
                    //else
                    //{
                    this.reciptDept = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoct()).Dept.Clone(); //��������
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
        /// ����ҽ��
        /// </summary>
        protected FS.FrameWork.Models.NeuObject reciptDoct = null;

        /// <summary>
        /// ��ǰ����ҽ��
        /// </summary>
        public void SetReciptDoc(FS.FrameWork.Models.NeuObject value)
        {
            this.reciptDoct = value;

        }

        /// <summary>
        /// ��ÿ���ҽ��
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
        /// ���߿������,�б��ڹҺſ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject SeeDept = null;

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
        /// �Ƿ���ʷҽ��״̬
        /// </summary>
        public bool bOrderHistory = false;


        /// <summary>
        /// ҩƷ�ͷ�ҩƷ�ļ۸�Ϊ0�Ƿ��շѣ�0����ȡ��1��ȡ��Ĭ��ֵΪ0����ȡ
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
            Classes.LogManager.Write(currentPatientInfo.Name + "����ʼ��ʼ������ҽ�������桿");
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
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ��ͬ��λ��Ϣ����" + CacheManager.InterMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                #region ��ȡ���Ʋ���

                #region Useless ���鸽�����

                isDealSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("200000", false, "0"));
                //��ҩƷ�����ļӼ�ҽ���Ƿ񵥶�����
                isDealEmrOrderSubtblSpecially = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("200005", false, "0"));
                emrSubtblUsage = Classes.Function.GetBatchControlParam("200006", false, "1");
                ULOrderUsage = Classes.Function.GetBatchControlParam("200007", false, "1");

                #endregion

                validRegDays = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("200022", false, "1"));

                isSaveOrderHistory = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("200021", false, "0"));


                //houwb ҽ�����Էѷ����� {A1FE7734-267C-43f1-A824-BF73B482E0B2}
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
                    //������п���
                    //ArrayList alTemp = CacheManager.InterMgr.GetDepartment();
                    //this.deptHelper.ArrayObject = alTemp;

                    //�������Ƶ����Ϣ               
                    //alTemp = CacheManager.InterMgr.QuereyFrequencyList();
                    //if (alTemp != null)
                    //{
                    //    frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alTemp);
                    //}
                }
                catch (Exception ex)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region Farpoint����

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

                //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
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
            if (currDepts.HospitalName.Contains("˳��"))
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

            base.OnStatusBarInfo(null, "(��ɫ���¿�)(��ɫ���շ�)   �������ƣ�" + hospitalname + "  ����ҽ�����룺" + hospitalybcode + "  ҽ��ҽʦ���룺" + gjcode + "");

            Classes.Function.SethsUsageAndSub();

            #region �����ӿ�

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


            #region ������ҩ
            Classes.LogManager.Write(currentPatientInfo.Name + "����ʼ��ʼ��������ҩ��");
            //{9DB64486-4398-4944-85FC-48F63A21CD7E}
            this.InitReasonableMedicine();

            if (this.IReasonableMedicine != null)
            {
                StartReasonableMedicine();
            }
            Classes.LogManager.Write(currentPatientInfo.Name + "��������ʼ��������ҩ��");

            #endregion
            Classes.LogManager.Write(currentPatientInfo.Name + "��������ʼ������ҽ�������桿");

            //��ѯ�Ƿ����÷���
            isUseNurseArray = Classes.Function.IsUseNurseArray();

            //�����š�����ͣ���ҽ������½����ҽ��վ����ʾ:����û�������Ű࣬�����������ϵ
            //DateTime nowTime = schemgManager.GetDateTimeFromSysDateTime();
            //string doctId = FS.FrameWork.Management.Connection.Operator.ID;
            //string deptId = CacheManager.LogEmpl.Dept.ID;
            //if (schemgManager.QueryByDoct(nowTime, deptId, doctId).Count <= 0)
            //{
            //    this.pnTop.Height = 23;
            //    this.pnDisplay.Visible = true;
            //    this.lblDisplay.Text = "����û�������Ű࣬�����������ϵ";
            //    this.lblDisplay.ForeColor = Color.Red;
            //    lblFeeInfo.Text = "";
            //}
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

            ArrayList alIndications = CacheManager.GetConList("IndicationsDrug");
            indicationsHelper = new FS.FrameWork.Public.ObjectHelper(alIndications);

        }

        /// <summary>
        /// ��ʼ��ֱ���շѽӿ�
        /// </summary>
        private void InitDirectFee()
        {
            if (IDoctFee == null)
            {
                IDoctFee = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee)) as FS.HISFC.BizProcess.Interface.FeeInterface.IDoctIdirectFee;
            }
        }

        /// <summary>
        /// ���Ĵ���ӿ�
        /// </summary>
        private void InitDealSubJob()
        {
            if (IDealSubjob == null)
            {
                IDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
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
        /// ������ʾ���ܽ��
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

        /// <summary>
        /// ����������
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
                                          "����",         //9
                                          "������λ",     //10
                                          "ÿ������",     //11
                                          "��λ",         //12
                                          "����/����",    //13
                                          "Ƶ�α���",     //14
                                          "Ƶ������",     //15
                                          "�÷�����",     //16
                                          "�÷�����",     //17
                                          "Ժע����",     //18
                                          "���",
                                          "����",
                                          "���",
                                          "��ʼʱ��",     //19
                                          "����ҽ��",     //32
                                          "ִ�п��ұ���", //20
                                          "ִ�п���",     //21
                                          "�Ӽ�",         //22
                                          "��鲿λ",     //34
                                          "��������",     //35
                                          "ȡҩҩ������", //36
                                          "ȡҩҩ��",     //37
                                          "��ע",         //23
                                          "¼���˱���",   //24
                                          "¼����",       //25
                                          "��������",     //26
                                          "����ʱ��",     //27
                                          "ֹͣʱ��",     //28
                                          "ֹͣ�˱���",   //29
                                          "ֹͣ��",       //30
                                          "˳���",       //31
                                          "Ƥ�Դ���",     //38
                                          "Ƥ��"          //39
                                      };

        #endregion

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

        #region ˽�з���

        #region ������ݵ����

        /// <summary>
        /// ���ʵ��toTable
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
        /// ��ʾҽ��������
        /// </summary>
        /// <param name="inOrder"></param>
        /// <returns></returns>
        private string ShowOrderName(FS.HISFC.Models.Order.OutPatient.Order outOrder)
        {
            string specs = string.IsNullOrEmpty(outOrder.Item.Specs) ? "" : ("[" + outOrder.Item.Specs + "] ");
            string price = "";
            if (outOrder.Item.ID == "999")
            {
                price = "[" + "0Ԫ/" + outOrder.Item.PriceUnit + "]";
            }
            else
            {
                if (outOrder.Item.Price > 0)
                {
                    if (outOrder.MinunitFlag == "1") //��С��λ�ж�
                    {
                        price = "[" + FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / outOrder.Item.PackQty) + "Ԫ/" + outOrder.Item.PriceUnit + "]";//6
                    }
                    else
                    {
                        price = "[" + FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price) + "Ԫ/" + outOrder.Item.PriceUnit + "]";//6
                    }
                }
                else if (outOrder.Unit == "[������]")
                {
                    if (outOrder.MinunitFlag == "1")
                    {
                        price = "[" + FS.FrameWork.Public.String.ToSimpleString(OutPatient.Classes.Function.GetUndrugZtPrice(outOrder.Item.ID) / outOrder.Item.PackQty) + "Ԫ/" + outOrder.Item.PriceUnit + "]";//6
                    }
                    else
                    {
                        price = "[" + FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price) + "Ԫ/" + outOrder.Item.PriceUnit + "]";//6
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

            //ҽ������ 
            if (outOrder.Item.Specs == null || outOrder.Item.Specs.Trim() == "")
            {
                return "[��:" + outOrder.SubCombNO.ToString() + "]" + (outOrder.IsPermission ? "���̡�" : "") + outOrder.Item.Name + price;
            }
            else
            {
                return "[��:" + outOrder.SubCombNO.ToString() + "]" + (outOrder.IsPermission ? "���̡�" : "") + outOrder.Item.Name + "[" + outOrder.Item.Specs + "]" + price;
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
                row[GetColumnIndexFromName("��ҩ")] = FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug);//5
                row[GetColumnIndexFromName("ÿ������")] = string.IsNullOrEmpty(order.DoseOnceDisplay) ? FS.FrameWork.Public.String.ToSimpleString(order.DoseOnce) : order.DoseOnceDisplay;//9
                row[GetColumnIndexFromName("��λ")] = objItem.DoseUnit;
                //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                row[GetColumnIndexFromName("����/����")] = order.HerbalQty;//11
            }
            else if (order.Item.ItemType == EnumItemType.UnDrug)
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

            row[GetColumnIndexFromName("ҽ������")] = ShowOrderName(order);

            #endregion

            this.ValidNewOrder(order);
            row[GetColumnIndexFromName("����")] = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
            row[GetColumnIndexFromName("������λ")] = order.Unit;//8
            row[GetColumnIndexFromName("Ƶ�α���")] = order.Frequency.ID;
            row[GetColumnIndexFromName("Ƶ������")] = order.Frequency.Name;
            row[GetColumnIndexFromName("�÷�����")] = order.Usage.ID;
            row[GetColumnIndexFromName("�÷�����")] = order.Usage.Name;//15
            row[GetColumnIndexFromName("��ʼʱ��")] = order.BeginTime;
            row[GetColumnIndexFromName("ִ�п��ұ���")] = order.ExeDept.ID;

            //2012-10-4 ����
            if (order.Item.ItemType == EnumItemType.Drug)
            {
                if (!string.Equals(order.Item.ID, "999"))
                {
                    row[GetColumnIndexFromName("��Ŀ����")] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).UserCode;
                    if (order.MinunitFlag != "1")//������С��λ 
                    {
                        row[GetColumnIndexFromName("����")] = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackUnit;
                        row[GetColumnIndexFromName("���")] = FS.FrameWork.Public.String.ToSimpleString(order.Qty * order.Item.Price, 2);
                    }
                    else
                    {
                        row[GetColumnIndexFromName("����")] = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackQty, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).MinUnit;
                        row[GetColumnIndexFromName("���")] = FS.FrameWork.Public.String.ToSimpleString(order.Qty * order.Item.Price / order.Item.PackQty, 2);
                    }
                }
            }
            else
            {
                if (!string.Equals(order.Item.ID, "999"))
                {
                    row[GetColumnIndexFromName("��Ŀ����")] = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).UserCode;
                }
                row[GetColumnIndexFromName("����")] = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price, 2) + "Ԫ/" + order.Item.PriceUnit;
                row[GetColumnIndexFromName("���")] = FS.FrameWork.Public.String.ToSimpleString(order.Qty * order.Item.Price, 2);
            }
            row[GetColumnIndexFromName("���")] = order.Item.Specs;


            row[GetColumnIndexFromName("ִ�п���")] = order.ExeDept.Name;
            row[GetColumnIndexFromName("�Ӽ�")] = order.IsEmergency;
            row[GetColumnIndexFromName("��鲿λ")] = order.CheckPartRecord;
            row[GetColumnIndexFromName("��������")] = order.Sample.Name;
            row[GetColumnIndexFromName("ȡҩҩ������")] = order.StockDept.ID;
            row[GetColumnIndexFromName("ȡҩҩ��")] = order.StockDept.Name;
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
            row[GetColumnIndexFromName("Ƥ��")] = CacheManager.OutOrderMgr.TransHypotest(order.HypoTest);
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
                FS.HISFC.Models.Order.OutPatient.Order order = al[i] as FS.HISFC.Models.Order.OutPatient.Order;

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

            FS.HISFC.Models.Order.OutPatient.Order order = null;
            try
            {
                order = ((FS.HISFC.Models.Order.OutPatient.Order)obj);//.Clone();
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("Clone����" + ex.Message);
                this.dirty = false;
                return;
            }

            if (this.isAddMode)
            {
                # region �����÷��Զ��������Ժע
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

            if (order.Note != "")//��ʾ
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("!")].Text = order.Note;
            }

            if (order.Item.ItemType == EnumItemType.Drug)//ҩƷ
            {
                FS.HISFC.Models.Pharmacy.Item objItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ÿ������")].Text = string.IsNullOrEmpty(order.DoseOnceDisplay) ? FS.FrameWork.Public.String.ToSimpleString(order.DoseOnce) : order.DoseOnceDisplay;

                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����/����")].Text = order.HerbalQty.ToString();//11

                if (order.DoseUnit == null || order.DoseUnit == "")
                {
                    order.DoseUnit = objItem.DoseUnit;
                }
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��λ")].Text = order.DoseUnit;

                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = order.Unit;//8

            }
            //else if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug)) //��ҩƷ
            else if (order.Item.ItemType == EnumItemType.UnDrug) //��ҩƷ
            {
                FS.HISFC.Models.Fee.Item.Undrug objItem = order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��λ")].Text = "";//������λ
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');//7
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = order.Unit;//8
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����/����")].Text = order.HerbalQty.ToString();//11
            }

            this.ValidNewOrder(order); //��д��Ϣ

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��")].Text = "";     //0

            if (order.NurseStation.Memo != null && order.NurseStation.Memo.Length > 0)
            {
                //������ҩ��أ���ʱδ�����Σ�
                //this.AddWarnPicturn(i, 0, FS.FrameWork.Function.NConvert.ToInt32(order.NurseStation.Memo));
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

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ҽ������")].Text = ShowOrderName(order);

            #endregion

            string totCost = "";
            if (order.MinunitFlag == "1")//������С��λ 
            {
                totCost = (order.Qty * order.Item.Price / order.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
            }
            else
            {
                totCost = (order.Qty * order.Item.Price).ToString("F4").TrimEnd('0').TrimEnd('.');
            }

            //this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("���")].Text = totCost;

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
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��ʼʱ��")].Value = order.MOTime;//��ʼʱ��
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����ʱ��")].Value = order.MOTime;//����ʱ��


            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ִ�п��ұ���")].Text = order.ExeDept.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ִ�п���")].Text = order.ExeDept.Name;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("�Ӽ�")].Value = order.IsEmergency;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��鲿λ")].Value = order.CheckPartRecord;//��鲿λ
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��������")].Value = order.Sample.Name;//��������
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ȡҩҩ������")].Value = order.StockDept.ID;//ȡҩҩ��
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ȡҩҩ��")].Value = order.StockDept.Name;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��ע")].Text = order.Memo;//20
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("¼���˱���")].Text = order.Oper.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("¼����")].Text = order.Oper.Name;

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����ҽ��")].Text = order.ReciptDoctor.Name;//����ҽ��
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��������")].Text = order.ReciptDept.Name;//��������

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                if (!string.Equals(order.Item.ID, "999"))
                {
                    this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��Ŀ����")].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).UserCode;
                    if (order.MinunitFlag != "1")//������С��λ 
                    {
                        neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackUnit;

                        neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(order.Qty * order.Item.Price).ToString();
                    }
                    else
                    {
                        neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackQty, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).MinUnit;

                        neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(order.Qty * order.Item.Price / order.Item.PackQty).ToString();
                    }
                }
            }
            else
            {
                if (!string.Equals(order.Item.ID, "999"))
                {
                    neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("��Ŀ����")].Text = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).UserCode;

                }
                neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(order.Item.Price, 2) + "Ԫ/" + order.Item.PriceUnit;

                neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(order.Qty * order.Item.Price).ToString();
            }
            neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("���")].Text = order.Item.Specs;

            if (order.EndTime != DateTime.MinValue)
            {
                this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ֹͣʱ��")].Value = order.EndTime;//ֹͣʱ�� 25
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ֹͣ�˱���")].Text = order.DCOper.ID;
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("ֹͣ��")].Text = order.DCOper.Name;

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
                order.Frequency.Usage = order.Usage; //�÷�����
            }

            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("˳���")].Value = order.SortID;//28
            if (!this.EditGroup)
            {
                if (this.currentPatientInfo.Pact.PayKind.ID == "02")//����ҽ��-��ʾ���ñ���
                {
                    //û����ʱ������ ��ΪӰ��������ʾ
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
                    //if (order.Item.Price > 0 && order.OrderType.IsCharge) this.neuSpread1.Sheets[SheetIndex].RowHeader.Cells[i, 0].Text = FS.HISFC.Components.Common.Classes.Function.ShowItemFlag(order.Item);
                }
            }
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("Ƥ�Դ���")].Value = order.HypoTest;//28
            this.neuSpread1.Sheets[SheetIndex].Cells[rowIndex, GetColumnIndexFromName("Ƥ��")].Value = CacheManager.OutOrderMgr.TransHypotest(order.HypoTest);//28
            this.neuSpread1.Sheets[SheetIndex].Rows[rowIndex].Tag = order.Clone();

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
        /// �����ͬ���ҽ��
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

                //ֱ�Ӹ��ݽ����״̬��ʾˢ��
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

                //������Ŀ��ɫ������ʾ
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
                        this.neuSpread1.Sheets[SheetIndex].Cells[row, GetColumnIndexFromName("ҽ������")].ForeColor = Color.Red;
                    }
                    else
                    {
                        if (CacheManager.InterfaceMgr.GetCompareSingleItem(this.currentPatientInfo.Pact.ID, orderTemp.Item.ID, ref compareItem) == -1)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("��ȡҽ��������Ŀʧ�ܣ�" + CacheManager.InterfaceMgr.Err);
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
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
            }

        }

        /// <summary>
        /// ��ղ�ѯҽ���б�
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

            //alFeeMoneyInfo = null;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯҽ��,���Ժ�!");
            Application.DoEvents();

            this.hsOrder.Clear();

            //��ѯ����ҽ������
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

            //��ѯ���ξ����ѿ�����ҽ��
            this.SameOrderList = CacheManager.OutOrderMgr.QueryOrderByClinicCode(currentPatientInfo.ID);
            //��ѯ���������ѿ���δִ�еļ������ҽ��
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
                    //houwb ҽ�����Էѷ����� {A1FE7734-267C-43f1-A824-BF73B482E0B2}
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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾҽ��,���Ժ�!");
            Application.DoEvents();

            if (this.IsDesignMode)
            {
                tooltip.SetToolTip(this.neuSpread1, "����ҽ��");
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
        /// ������Ϣ
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
                //����ִ�п���Ϊ���߿���
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

            //����ҽ��
            if (order.ReciptDoctor == null || order.ReciptDoctor.ID == "")
            {
                order.ReciptDoctor = this.GetReciptDoct().Clone();
            }
            //��������
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

            /*
            if (!this.IsDesignMode && !EditGroup)
            {
                return;
            }
            */

            #region ѡ��
            //ÿ��ѡ��仯ǰ���������ʾ
            this.ucOutPatientItemSelect1.Clear(false);
            decimal totalPrice = 0;
            int comboNum = 0;//��õ�ǰѡ������

            //����Ϊ��ǰ��
            this.ucOutPatientItemSelect1.CurrentRow = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            this.ActiveRowIndex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            this.currentOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
            this.ucOutPatientItemSelect1.CurrOrder = this.currentOrder;
            //���������ѡ��
            if (this.currentOrder.Combo.ID != ""
                && this.currentOrder.Combo.ID != null)//&& this.IsDesignMode)
            {

                ///����Ѱ��
                for (int i = this.ActiveRowIndex; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order tempOrder = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
                    string strComboNo = tempOrder.Combo.ID;

                    //string strComboNo = this.neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("��Ϻ�")].Text;


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

                ///����Ѱ��
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

            if (int.Parse(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("ҽ��״̬")].Text) == 0)
            {
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


                if (OrderCanSetCheckChanged != null)
                {
                    this.OrderCanSetCheckChanged(false);//��ӡ������뵥ʧЧ
                }
            }
            else
            {
                this.ActiveRowIndex = -1;
            }
            #endregion
            this.tooltip.SetToolTip(this.neuSpread1, "ҩƷ��" + totalPrice.ToString() + "Ԫ");


        }

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
                    {
                        iSelectionCount++;
                    }
                }

                if (iSelectionCount > 1)
                {
                    string t = "";//��Ϻ� �޸ĳɶ�����Ϻ�
                    int injectNum = 0;//Ժ��ע����
                    int iSort = -1;
                    string time = "";
                    int kk = 0;

                    if (this.ValidComboOrder() == -1)
                    {
                        return;//У�����ҽ��
                    }

                    //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    int sameSubComb = 0;
                    FS.HISFC.Models.Order.OutPatient.Order ord = null;

                    //���ڼ�¼�޸Ĺ��ķź�
                    string combID = "";
                    int preSubCombNo = 0;

                    for (int rowIndex = 0; rowIndex < this.neuSpread1.Sheets[sheetIndex].Rows.Count; rowIndex++)
                    {
                        ord = this.GetObjectFromFarPoint(rowIndex, sheetIndex);
                        ord.SortID = rowIndex + 1;
                        /*
                         * ע��by  zhaorong  at 2013-8-5 �в�ҩ������ϱ����������double��������
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

                        this.neuSpread1.Sheets[sheetIndex].Cells[rowIndex, GetColumnIndexFromName("˳���")].Text = ord.SortID.ToString();
                        this.neuSpread1.Sheets[sheetIndex].Cells[rowIndex, GetColumnIndexFromName("˳���")].Value = ord.SortID;

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
                //isDecSysClassWhenGetRecipeNO = ctrlMgr.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, false);
                isDecSysClassWhenGetRecipeNO = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, "0"));
            }

            FS.HISFC.Models.Order.Frequency frequency = null;//Ƶ��
            FS.FrameWork.Models.NeuObject usage = null;//�÷�
            FS.FrameWork.Models.NeuObject exeDept = null;//ִ�п���

            decimal amount = 0;//����
            string sysclass = "-1";//���
            decimal days = 0;//��ҩ����
            string sample = "";//����
            decimal injectCount = 0;//Ժע����
            string jpNum = "";
            //��ҩ�ļ�ҩ��ʽ
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
                        //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                        FS.HISFC.Models.Order.OutPatient.Order tem = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, o.ID);
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
                    if (o.IsSubtbl)
                    {
                        continue;
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
                                }
                                //ע�� by zhaorong at 2013-7-29 ��ҩ��ʽ��ͬ������������
                                //else
                                //{
                                //    if (o.Memo != PCCUsage)
                                //    {
                                //        ucOutPatientItemSelect1.MessageBoxShow("��ҩ��ʽ��ͬ�������Խ�����ϣ�");
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
                                    ucOutPatientItemSelect1.MessageBoxShow("��ҩ������ͬ������������ã�");
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
                            if ("PCZ,P.PCC".Contains(o.Item.SysClass.ID.ToString()) &&
                                "PCZ,P.PCC".Contains(sysclass))
                            {
                                //��ҩ�ͳ�ҩ�������
                            }
                            else
                            {
                                if (("PCZ,P,PCC,UL".Contains(o.Item.SysClass.ID.ToString()) || ("PCZ,P,PCC,UL".Contains(sysclass)) && o.Item.SysClass.ID.ToString() != sysclass))
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("ϵͳ���ͬ������������ã�");
                                    return -1;

                                }
                                else if (o.Item.SysClass.ID.ToString() != sysclass && !o.Item.SysClass.ID.Equals("UT") && !sysclass.Equals("UT"))
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
        private void AddInjectNum(FS.HISFC.Models.Order.OutPatient.Order sender, bool isCanModifiedInjectNum)
        {
            //��ʱû�������÷�������ʲô��ĿֻҪ�����˴��÷����ո���
            //if (!Classes.Function.hsUsageAndSub.Contains(sender.Usage.ID))
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
                formInputInjectNum.Order.DoseUnit = ((FS.HISFC.Models.Pharmacy.Item)formInputInjectNum.Order.Item).DoseUnit;
            }
            formInputInjectNum.InjectNum = sender.InjectCount;
            if (sender.InjectCount == 0)
            {
                //����Ĭ�ϵ�Ժע����Ϊ����/ÿ����
                int injectNumTmp = FS.FrameWork.Function.NConvert.ToInt32(sender.Item.Qty * ((FS.HISFC.Models.Pharmacy.Item)sender.Item).BaseDose / sender.DoseOnce);
                formInputInjectNum.InjectNum = injectNumTmp;

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
                FS.HISFC.Models.Order.OutPatient.Order order = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);
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

            this.RefreshOrderState();
        }

        #region Ԥ�ۿ��
        ///// <summary>
        ///// Ԥ�ۿ��
        ///// </summary>
        ///// <param name="CacheManager.PhaIntegrate"></param>
        ///// <param name="qty">1ʱ�����룻-1ʱ��ɾ��</param>
        ///// <returns></returns>
        //private int UpdateStockPre(FS.HISFC.Models.Order.OutPatient.Order order, decimal qty, ref string errInfo)
        //{
        //    if (order.Item.ItemType == EnumItemType.Drug)
        //    {
        //        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
        //        applyOut.ID = order.ID;
        //        applyOut.StockDept.ID = order.StockDept.ID;
        //        applyOut.SystemType = "O1";//����ҽ������
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
        /// ����Ԥ�ۿ��
        /// </summary>
        /// <param name="isDelete"></param>
        /// <param name="outOrder"></param>
        /// <returns></returns>
        private int DealPreStock(bool isDelete, FS.HISFC.Models.Order.OutPatient.Order outOrder)
        {
            #region ��ҩƷ�ͱ������������
            if (outOrder.Item.ItemType == EnumItemType.UnDrug || outOrder.Item.ItemType == EnumItemType.MatItem)
            {
                return 1;
            }
            #endregion


            //2013-6-20 ���������·�ʽʵ��
            int rev = this.phaIntegrate.DeletePreoutStore(outOrder);
            if (rev == -1)
            {
                errInfo = "��������ҩƷԤ��ʧ�ܣ�\r\n" + phaIntegrate.Err;
                return -1;
            }

            if (!isDelete)
            {
                rev = phaIntegrate.InsertPreoutStore(outOrder);
                if (rev == -1)
                {
                    errInfo = "��������ҩƷԤ��ʧ�ܣ�\r\n" + phaIntegrate.Err;
                    return -1;
                }
            }
            return 1;
        }
        #endregion

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
        /// ����ҽ��˳���
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

                    FS.HISFC.Models.Order.OutPatient.Order order = new FS.HISFC.Models.Order.OutPatient.Order();


                    for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
                    {
                        order = this.GetObjectFromFarPoint(i, 0);
                        order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                        if (order.ID == "" && !string.Equals(order.Item.ID, "999")) //new �¼ӵ�ҽ��
                        {
                            alOrder.Add(order);
                        }
                        else //update ���µ�ҽ��
                        {
                            #region �����Ҫ���µ�ҽ��
                            //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                            FS.HISFC.Models.Order.OutPatient.Order newOrder = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                            //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
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
                        #region �������

                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
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
                            }

                            totCost = GetCost(totCost);

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

                    //��ѯģʽ��ʱ�򣨷ǿ���״̬����ѯ����Ӧ�ķ�����Ϣ
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
                    //����״̬��ʾ�����ϵķ�����Ϣ
                    else
                    {
                        #region �������

                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
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
                            }

                            totCost = GetCost(totCost);
                        }

                        #endregion

                        alFeeDetail = null;


                        alFeeDetail = new ArrayList();

                        FS.HISFC.Models.Fee.Outpatient.FeeItemList item = null;
                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                        {
                            if (orderObj.MinunitFlag == "")//user03Ϊ��,˵����֪��������ʲô��λ Ĭ��Ϊ��С��λ
                            {
                                orderObj.MinunitFlag = "1";//Ĭ��
                            }
                            item = Classes.Function.ChangeToFeeItemList(GetOrderPactInfo(orderObj), currentPatientInfo);

                            alFeeDetail.Add(item);

                            //if (orderObj.MinunitFlag != "1")//������С��λ 
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
        /// �Ƿ�����ʾ���ñ���
        /// </summary>
        bool isShowFeeWarning = false;

        /// <summary>
        /// ����ķ�����Ϣ
        /// </summary>
        decimal[] alFeeMoneyInfo = null;

        FS.HISFC.BizLogic.Manager.PactStatRelation myRelation = new FS.HISFC.BizLogic.Manager.PactStatRelation();

        /// <summary>
        /// ҽ��������ʾ��
        /// </summary>
        /// <param name="isShowSIFeeInfo">�Ƿ���ʾҽ��������Ϣ������Ч�������������������㣬����Ͳ�ѯ��ʱ����ʾ</param>
        /// <param name="isRequery">�������շѱ�����Ϣ���Ƿ����²�ѯ��������Ŀʱ�����²�ѯ</param>
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
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        //���ڷ������� ҽ���ӿڼ������ǳ�����������ֻ����ʾ����
                        //���ڳ���Ĳ��ٴ���ֱ�Ӱ����ܷ�����ʾ

                        //ArrayList alFeeMoneyInfo = null;

                        rev = CacheManager.FeeIntegrate.CalculatOrderFee(this.Patient, alFeeList, isRequery, ref alFeeMoneyInfo, ref errInfo);
                    }
                    else
                    {
                        rev = CacheManager.FeeIntegrate.CalculatOrderFee(this.Patient, alFeeList, false, ref alFeeMoneyInfo, ref errInfo);
                    }

                    //��Ʊ�����ʾ���
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
                        accountVacancy = "�˻���" + this.vacancyDisplay;
                        this.pnTop.Height = 80;
                    }
                    else
                    {
                        this.pnTop.Height = 60;
                    }

                    string showInfo = currentPatientInfo.PID.CardNO + "  " + currentPatientInfo.Name + "  " + this.currentPatientInfo.Sex.Name + "  " + CacheManager.OutOrderMgr.GetAge(this.currentPatientInfo.Birthday) + "  " + currentPatientInfo.Pact.Name;

                    showInfo += "\r\n���:" + GetDiagInfo();

                    //�����ۼƺ����н��ı�����������>0 �͸�����ʾ
                    if (decPubMoneyAddUp + decRebateMoneyAddUp + decPubMoney + decRebateMoney > 0)
                    {
                        showInfo += "\r\n" + accountVacancy + "�ܷ���:" + FS.FrameWork.Public.String.ToSimpleString(decTotalMoney, 2) +
                            "Ԫ �Էѽ��:" + FS.FrameWork.Public.String.ToSimpleString((decOwnMoney - decRebateMoney), 2) +
                           "Ԫ �������:" + FS.FrameWork.Public.String.ToSimpleString(decPubMoney, 2) +
                           "Ԫ ������:" + FS.FrameWork.Public.String.ToSimpleString(decRebateMoney, 2) + "Ԫ \r\n" +

                           "�����ۼƷ����ܶ�:" + FS.FrameWork.Public.String.ToSimpleString(decTotalMoneyAddUp, 2) +
                           "Ԫ �ۼ��Էѽ��:" + FS.FrameWork.Public.String.ToSimpleString((decOwnMoneyAddUp - decRebateMoneyAddUp), 2) +
                           "Ԫ �ۼƱ������:" + FS.FrameWork.Public.String.ToSimpleString(decPubMoneyAddUp, 2) +
                           "Ԫ �ۼƼ�����:" + FS.FrameWork.Public.String.ToSimpleString(decRebateMoneyAddUp, 2) + "Ԫ ";

                        if (!string.IsNullOrEmpty(displayTotFee))
                        {
                            showInfo += "\r\n" + displayTotFee;
                        }
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
                        showInfo += "\r\n" + accountVacancy + "�ܷ���:" + FS.FrameWork.Public.String.ToSimpleString(totcost, 2) + "Ԫ ";
                        if (!string.IsNullOrEmpty(displayTotFee))
                        {
                            showInfo += " ����" + displayTotFee;
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
        /// ��������Ϣ
        /// </summary>
        /// <returns></returns>
        private string GetDiagInfo()
        {
            ArrayList al = CacheManager.DiagMgr.QueryCaseDiagnoseForClinic(this.Patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("��ѯ�����Ϣ����\r\n" + CacheManager.DiagMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }

            string strDiag = "";
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "��";
                }
            }
            strDiag = strDiag.TrimEnd('��');
            if (string.IsNullOrEmpty(strDiag))
            {
                strDiag = "��";
            }
            return strDiag;
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

            FS.HISFC.Models.Order.OutPatient.Order orderTemp = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as
                FS.HISFC.Models.Order.OutPatient.Order;

            if (orderTemp == null)
            {
                return;
            }

            //{F1706DB9-376D-433e-A5A9-1E1EEA46733C}  �����޸Ĳ�ҩҽ��
            if (orderTemp.Item.ItemType == EnumItemType.Drug)
            {
                if (((FS.HISFC.Models.Pharmacy.Item)orderTemp.Item).SysClass.ID.ToString() != "PCC")
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
                        FS.FrameWork.WinForms.Classes.Function.Msg("ҽ�����շѣ������޸ģ�\n�븴��ҽ��������ҽ�����޸ģ�", 411);
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
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
                ucHerbal.AlOrder = alModifyHerbal;
                ucHerbal.OpenType = FS.HISFC.Components.Order.Controls.EnumOpenType.Modified; //�޸�
                ucHerbal.SetFocus();
                DialogResult r = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucHerbal);

                if (ucHerbal.IsCancel == true)
                {
                    //ȡ����
                    return;
                }

                if (ucHerbal.OpenType == FS.HISFC.Components.Order.Controls.EnumOpenType.Modified)
                {
                    //��Ϊ�¼�ģʽ�Ͳ�ɾ����
                    if (this.Del(this.neuSpread1.ActiveSheet.ActiveRowIndex, true) < 0)
                    {
                        //ɾ��ԭҽ�����ɹ�
                        return;
                    }
                }


                AddNewHerbalOrder();
                //}
            }

        }

        #region {C6E229AC-A1C4-4725-BBBB-4837E869754E}

        /// <summary>
        /// ���״洢
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

        #endregion

        #region ���з���
        /// <summary>
        /// ���ҽ��ʵ���FarPoint
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
                if (string.IsNullOrEmpty(this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("ҽ����ˮ��")].Text))
                {
                    return null;
                }

                if (this.hsOrder.Contains(this.Patient.DoctorInfo.SeeNO + this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("ҽ����ˮ��")].Text))
                {
                    order = this.hsOrder[this.Patient.DoctorInfo.SeeNO + this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("ҽ����ˮ��")].Text] as FS.HISFC.Models.Order.OutPatient.Order;
                }
                else
                {
                    //����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                    order = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, this.neuSpread1.Sheets[SheetIndex].Cells[i, GetColumnIndexFromName("ҽ����ˮ��")].Text);
                }
            }

            return order;
        }

        /// <summary>
        /// �����ҽ��
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

                //�����������ҩƷ��Ŀ����Ϊ�㣬ϵͳ������¼�����Ĭ����ʾΪ1��ҽ���ڲ��޸ĵ�����±��棬��ʾ����Ϊ0
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

                //�˴��ж�ͣ�õ��÷�����ֵ
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
                    newOrder.MinunitFlag = "1";//��С��λ
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
            //�������
            if (sender.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
            {

                #region �����ӵĶ���
                if (((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "UC")//���
                {
                    //��ӡ������뵥
                    ////this.AddTest(sender);
                }
                else if (((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.SysClass.ID.ToString() == "MC")//����
                {
                    //��ӻ�������
                    ////this.AddConsultation(sender);
                }

                #region Ƥ��
                if (((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.ItemType == EnumItemType.Drug)//ҩƷ
                {
                    if (((FS.HISFC.Models.Pharmacy.Item)((FS.HISFC.Models.Order.OutPatient.Order)sender).Item).IsAllergy)
                    {
                        //���Ʋ��������Ƿ�Ĭ��ȫ��Ժע��ȫ��Ժע���ڵ���Ժע���������
                        if (!this.isCanModifiedInjectNum)
                        {
                            if (this.hypotestMode == "1")
                            {
                                if (ucOutPatientItemSelect1.MessageBoxShow(((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.Name + "�Ƿ���ҪƤ�ԣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                else
                {
                    order.ExeDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ExeDept.ID);
                }

                #region ���»�ȡȡҩҩ��
                // ���׿���ȡҩҩ������Ϊ��
                if (newOrder.Item.ItemType == EnumItemType.Drug)
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp = new FS.HISFC.Models.Order.OutPatient.Order();
                    temp.Item = newOrder.Item;
                    temp.ReciptDept = newOrder.ReciptDept;

                    if (!this.EditGroup)
                    {
                        if (Classes.Function.FillDrugItem(null, currentPatientInfo, ref temp, ref errInfo) <= 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
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
                    newOrder.StockDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(temp.StockDept.ID);

                    //�ж�ҩƷ�Ƿ���ҩ������ʾ 
                    //string classCode = FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality.ID);
                    //if (classCode == "P" || classCode == "P1" || classCode == "S1")//classCode.Contains("S") || 
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow("��" + order.Item.Name + "�����ڶ���ҩƷ��\r\n���ݴ�������취�涨,��ͬʱ���ӿ����ֹ�����ҩ����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        order.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();//�����Ϻ�
                    }
                    catch
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("���ҽ����Ϻų���\r\n" + CacheManager.OutOrderMgr.Err);
                    }
                }

                DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

                if (!this.EditGroup)
                {
                    if (this.currentPatientInfo != null)
                    {
                        order.InDept = this.currentPatientInfo.DoctorInfo.Templet.Dept;//�Һſ���

                        //�۸��Ѿ��ڻ�ȡ��Ŀ������Ϣ�л�ȡ����

                        //�����ӵĲŻ�ȡ���¼۸�
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

                #region ����ҽ������ʱ��

                order.BeginTime = Order.Classes.Function.GetDefaultMoBeginDate(3);

                if (order.User03 != "")//���׵�ʱ����
                {
                    int iDays = FS.FrameWork.Function.NConvert.ToInt32(order.User03);
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

                #region ����ҽ����������ҩ
                if (Patient != null && this.Patient.Pact.PayKind.ID == "02")
                {
                    FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(GetItemUserCode(order.Item));
                    if (indicationsObj != null)
                    {
                        if (MessageBox.Show("ҩƷ��" + order.Item.Name + "���������Ƽ�ҩƷ��\r\n\r\n����ҩƷ˵������" + indicationsObj.Name + "��\r\n\r\n��ȷ��ҽ�������趨������(��)���Է�(��)?\r\n", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            //���������е�tagֵ����
                            neuSpread1.ActiveSheet.Cells[neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("����/����")].Tag = "1";
                        }
                        else
                        {
                            //���������е�tagֵ����
                            neuSpread1.ActiveSheet.Cells[neuSpread1.ActiveSheet.ActiveRowIndex, GetColumnIndexFromName("����/����")].Tag = "0";
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
                ucOutPatientItemSelect1.MessageBoxShow("������Ͳ���ҽ�����ͣ�");
            }
            dirty = false;

            #region ������ҩ��ʾ

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
                    #region ҩƷ��ѯ
                    try
                    {
                        //ò������ֻ�����½ǵ�����λ�����
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

            #region ������

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
                            ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + errText);
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
        /// �����Ŀ�Զ�����
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
        /// ��Ӳ�ҩҽ��{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// </summary>
        /// <param name="alHerbalOrder"></param>
        public void AddHerbalOrders(ArrayList alHerbalOrder)
        {
            //��ҩ������ҩ��������
            if (ucHerbal == null)
            {
                ucHerbal = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID);
            }
            //using (FS.HISFC.Components.Order.Controls.ucHerbalOrder uc = new FS.HISFC.Components.Order.Controls.ucHerbalOrder(true, FS.HISFC.Models.Order.EnumType.SHORT, this.GetReciptDept().ID))
            //{
            ucHerbal.Patient = new FS.HISFC.Models.RADT.PatientInfo();//
            ucHerbal.IsClinic = true;

            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
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
        /// �㼶��ʽ����ҽ��
        /// </summary>
        public void AddLevelOrders()
        {
            using (FS.HISFC.Components.Order.Controls.ucLevelOrder uc = new FS.HISFC.Components.Order.Controls.ucLevelOrder())
            {
                uc.InOutType = 1;
                uc.Patient = new FS.HISFC.Models.RADT.PatientInfo();

                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "������ҽ������";
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
        private List<FS.HISFC.Models.Order.Order> GetSelectOrders()
        {
            List<FS.HISFC.Models.Order.Order> alOrders = new List<FS.HISFC.Models.Order.Order>();
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

            List<FS.HISFC.Models.Order.Order> alItems = this.GetSelectOrders();

            if (alItems.Count <= 0)
            {
                //û��ѡ����Ŀ��Ϣ
                ucOutPatientItemSelect1.MessageBoxShow("��ѡ�����ļ����Ϣ!");
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

        /// <summary>
        /// ˢ�����
        /// </summary>
        /// <param name="isReSort">�Ƿ���������</param>
        public void RefreshCombo()
        {
            FS.HISFC.Models.Order.OutPatient.Order ord = null;
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
                            hsSubCombNo[ord.SubCombNO] = FS.FrameWork.Function.NConvert.ToInt32(hsSubCombNo[ord.SubCombNO]) + 1;
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
                ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("ˢ��ҽ��״̬ʱ���ֲ���Ԥ֪�������˳������������Ի������Ա��ϵ"));
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
                ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("ˢ��ҽ��״̬ʱ���ֲ���Ԥ֪�������˳������������Ի������Ա��ϵ"));
            }
        }

        /// <summary>
        /// ���ҽ���Ϸ���
        /// </summary>
        /// <returns></returns>
        public int CheckOrder()
        {
            //�������Ƿ����ҩƷ
            bool drugFlag = false;

            FS.HISFC.Models.Order.OutPatient.Order order = null;

            //�Ƿ��б䶯����
            bool IsModify = false;

            ///�Ƿ��������
            bool isHaveSublOrders = false;

            FS.HISFC.Models.Pharmacy.Item drugItem = null;

            //����������ʾ
            string exceedWarning = "";

            //�����ҩ��Ŀ�����Ʋ�ҩ��Ŀ�������ظ�
            Hashtable hsPCCItem = new Hashtable();

            //��ʱҽ��
            for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            {
                order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;

                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    drugFlag = true;
                }
                if (order.Status == 0)
                {
                    //δ��˵�ҽ��
                    IsModify = true;

                    if (order.Item.ID == "999")
                    {
                        continue;
                    }

                    //���������������+��Ŀ��ˮ�ţ���Ϊ��ֵ
                    if (!this.hsOrder.Contains(order.SeeNO + order.ID)
                        && !(string.IsNullOrEmpty(order.SeeNO)
                        || string.IsNullOrEmpty(order.ID)))
                    {
                        this.hsOrder.Add(order.SeeNO + order.ID, order);
                    }
                    //if (order.Item.IsPharmacy)
                    #region ҩƷ
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {

                        #region ����ʱ�ж��Ƿ�ͣ�á�ȱҩ

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
                                ShowErr("ҩƷ��" + order.Item.Name + "��ÿ���������������ֵ " + doceOnceLimit.ToString() + "�����޸ģ�\r\n������������ϵϵͳ����Ա��\r\n", i, 0);
                                return -1;
                            }
                        }

                        #region �ж�ȡ������
                        //����������λΪ��װ��λ�ģ��жϵ�λ�����ڰ�װ��λ������
                        //0 ��װ��λ��1 ��С��λ
                        if (order.MinunitFlag == "0" && order.Unit != drugItem.PackUnit)
                        {
                            ShowErr("ҩƷ��" + order.Item.Name + "��������λ��������ֻ�ܰ��հ�װ��λ��drugItem.PackUnit��ʹ�ã�\r\n\r\n", i, 0);
                            return -1;
                        }

                        if (Classes.Function.CheckLimitQty(order, order.Qty, order.Unit, ref errInfo) == -1)
                        {
                            ShowErr(errInfo + "\r\n\r\n", i, 0);
                            return -1;
                        }

                        #endregion

                        #region ��ȡҩƷ������Ϣ

                        order.Item.MinFee = drugItem.MinFee;

                        //((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice = drugItem.Price;
                        //decimal orgPrice = 0;

                        //order.Item.Price = CacheManager.FeeIntegrate.GetPrice(order.Item.ID, currentPatientInfo, 0, order.Item.Price, order.Item.ChildPrice, order.Item.SpecialPrice, 0, ref orgPrice);

                        order.Item.Name = drugItem.Name;
                        order.Item.SysClass = drugItem.SysClass.Clone();//����ϵͳ���
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = drugItem.IsAllergy;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = drugItem.PackUnit;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = drugItem.MinUnit;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = drugItem.BaseDose;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = drugItem.DosageForm;
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).SplitType = drugItem.SplitType;

                        #endregion

                        //ҩƷ
                        if (order.Item.SysClass.ID.ToString() == "PCC")
                        {
                            if (!hsPCCItem.Contains(order.Item.ID))
                            {
                                hsPCCItem.Add(order.Item.ID, null);
                            }
                            else
                            {
                                ShowErr("��ҩ��" + order.Item.Name + "���������ظ�������\r\n���ڶ����ͬ��Ŀ��", i, 0);
                            }

                            //�в�ҩ
                            if (order.HerbalQty == 0)
                            {
                                ShowErr("ҩƷ��" + order.Item.Name + "����������Ϊ�㣡", i, 0);
                                return -1;
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
                            ShowErr("ҩƷ��" + order.Item.Name + "��Ƶ�β���Ϊ�գ�", i, 0);
                            return -1;
                        }
                        if (order.Usage.ID == "")
                        {
                            ShowErr("ҩƷ��" + order.Item.Name + "���÷�����Ϊ�գ�", i, 0);
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
                            decimal preNum = 0;
                            decimal orderQty = 0;
                            if (order.MinunitFlag != "1")//������С��λ !=((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit)
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
                                        ShowErr("ҩƷ��" + order.Item.Name + "���ĵ�ǰ�����Ϊ" + stockinfo + ",��Ԥ��" + preStockinfo + ",����ʹ�ã�", i, 0);
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
                                ShowErr("ҩƷ��" + order.Item.Name + "������ж�ʧ��!" + CacheManager.PhaIntegrate.Err, i, 0);
                                return -1;
                            }
                        }
                    }
                    #endregion

                    #region ��ҩƷ
                    else
                    {
                        #region �ж�ͣ��״̬

                        FS.HISFC.Models.Fee.Item.Undrug undrug = CacheManager.FeeIntegrate.GetUndrugByCode(order.Item.ID);
                        if (undrug == null)
                        {
                            ShowErr("���ҷ�ҩƷ��Ŀ��" + order.Item.Name + "��ʧ�ܣ�" + CacheManager.FeeIntegrate.Err, i, 0);
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
                            ShowErr("��Ŀ��" + order.Item.Name + "����ѡ��ִ�п��ң�", i, 0); return -1;
                        }
                    }
                    #endregion

                    if (order.Qty == 0)
                    {
                        ShowErr("��Ŀ��" + order.Item.Name + "����������Ϊ�գ�", i, 0);
                        return -1;
                    }
                    if (FS.FrameWork.Public.String.ValidMaxLengh(order.Memo, 80) == false)
                    {
                        ShowErr("��Ŀ��" + order.Item.Name + "���ı�ע����!", i, 0);
                        return -1;
                    }
                    if (order.Qty > 5000)
                    {
                        ShowErr("����̫��", i, 0); return -1;
                    }

                    if (this.IsFeeWhenPriceZero == "0")
                    {
                        if (order.Item.Price == 0)
                        {
                            ShowErr("��Ŀ��" + order.Item.Name + "�����۱�����ڣ���", i, 0);
                            return -1;
                        }
                    }

                    if (order.ID == "") IsModify = true;
                }
                //�ѱ����ҽ���˴�һ���ѯ
                else
                {
                    ArrayList alOrder = CacheManager.OutOrderMgr.QueryOrder(currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                    if (alOrder == null)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("��ѯҽ������" + CacheManager.OutOrderMgr.Err);
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
                Components.Order.Classes.Function.ShowBalloonTip(8, "����", "����ҩƷ��������7����������ע��ע�����ɣ�\r\n" + exceedWarning, ToolTipIcon.Warning);
            }

            //����ɾ��Ҳ���뱣�棬���ҽ��ȫɾ�ˣ����ܹ��������� houwb 2011-4-14
            if (hsDeleteOrder == null || hsDeleteOrder.Keys.Count == 0)
            {
                if (IsModify == false)
                {
                    Components.Order.Classes.Function.ShowBalloonTip(4, "��ʾ", "\r\nû�п����κ���Ŀ������Ҫ���棡", ToolTipIcon.Info);
                    return -2;//δ����¼���ҽ��
                }
            }

            //���ȫ��ɾ��ʱ��ı��棬����Ͳ��ж��Ƿ�����Ч�����
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
                //��ʾ�ظ�ҩƷ
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
                    if (ucOutPatientItemSelect1.MessageBoxShow("���ظ�ҩƷ����Ŀ,�Ƿ��������?\r\n" + repeatItemName, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }

            }

            if (!isHaveSublOrders && dealSublMode == 1 && this.neuSpread1.Sheets[0].RowCount > 0)
            {
                //{A1BAF267-6053-44e3-B96E-36E2C48DE4BD}
                //if (ucOutPatientItemSelect1.MessageBoxShow("�Ƿ����¼��㸽�ģ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //{
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                if (this.CalculatSubl(false) == -1)
                {
                    return -1;
                }
                //}
            }

            #region ������ҩ�Զ����

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
        /// �Ƿ��ѿ������
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
        /// �ж����
        /// </summary>
        /// <param name="type">0 ��������(��ҩƷ����1 �������棨������ҩƷ����2 ���</param>
        /// <returns></returns>
        private int CheckDiag(int type)
        {
            #region �ж�����Ƿ�¼��

            if (!this.isHaveDiag())
            {
                switch (isJudgeDiagnose)
                {
                    //0 ���жϣ�
                    case "0":
                        return 1;
                        break;
                    //1 �ж�ҩƷ��
                    case "1":
                        if (type == 0)
                        {

                            ucOutPatientItemSelect1.MessageBoxShow("���ߡ�" + Patient.Name + "��û����Ч��ϣ�����¼����ϣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return -1;
                        }
                        break;
                    //2 �ж�ҩƷ�ͷ�ҩƷ��
                    case "2":
                        if (type == 0 || type == 1)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("���ߡ�" + Patient.Name + "��û����Ч��ϣ�����¼����ϣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return -1;
                        break;
                    //3 �ж�ҩƷ����ҩƷ+���
                    case "3":
                        ucOutPatientItemSelect1.MessageBoxShow("���ߡ�" + Patient.Name + "��û����Ч��ϣ�����¼����ϣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                        break;
                    //1 �ж�ҩƷ��
                    default:
                        if (type == 0)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow("���ߡ�" + Patient.Name + "��û����Ч��ϣ�����¼����ϣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return -1;
                        }
                        break;
                }
            }
            #endregion

            return 1;
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
                    FS.HISFC.Models.Order.OutPatient.Order o = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

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

                    o.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();

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
                    FS.HISFC.Models.Order.OutPatient.Order temp = this.GetObjectFromFarPoint(i, 0);
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
        /// ��鿪����Ч��
        /// </summary>
        /// <param name="alOrders"></param>
        /// <param name="order"></param>
        /// <param name="errinfo"></param>
        /// <returns></returns>
        private int CheckOrderBase(ArrayList alOrders, FS.HISFC.Models.Order.OutPatient.Order order, ref string errinfo)
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

            //����Ȩ
            ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, this.GetReciptDoct(),
                this.GetReciptDept(), FS.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref errinfo);
            //ret = Components.Order.Classes.Function.JudgeEmplPriv(order, this.GetReciptDoct(),
            //    this.GetReciptDept(), FS.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref errinfo);

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
        /// ճ��ҽ��
        /// </summary>
        public void PasteOrder()
        {
            try
            {
                if (Classes.Function.AlCopyOrders == null || Classes.Function.AlCopyOrders.Count <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("��������û�п���ճ����ҽ����"));
                    return;
                }

                if (FS.HISFC.Components.Order.Classes.HistoryOrderClipboard.Type == ServiceTypes.C)
                {
                    string oldComb = "";
                    string newComb = "";

                    ArrayList alOrder = new ArrayList();
                    ArrayList alAddOrder = new ArrayList();//�������ӽӿ�

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
                        //��ӵ���ǰ����� ����ҽ�����ͽ��з���
                        this.AddNewOrder(outOrder, 0);
                    }
                }
                else
                {
                    ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("�����԰�סԺ��ҽ������Ϊ����ҽ����"));
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
        /// ����ҽ��
        /// �����Ƶ�ҽ�������Ǳ�����ģ���ҽ����ˮ�ŵģ�
        /// ����ճ��ʱ������
        /// </summary>
        public void CopyOrder()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            ArrayList list = new ArrayList();

            //��ȡѡ���е�ҽ��ID
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
        #region LIS��Pacs�ӿ�

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
                ArrayList alSelectOrder = new ArrayList(this.GetSelectOrders());

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

        #region LIS��PACS���뵥

        /// <summary>
        /// LIS���뵥��ӡ�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.ILisReportPrint IlisReportPrint = null;

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
                this.IlisReportPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.ILisReportPrint)) as FS.HISFC.BizProcess.Interface.Order.ILisReportPrint;
            }

            if (IlisReportPrint == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("LIS��ӡ�ӿ�δʵ�֣�����ϵ��Ϣ�ƣ�\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint IPacsReportPrint = null;

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
                this.IPacsReportPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint)) as FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint;
            }

            if (IPacsReportPrint == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("PACS��ӡ�ӿ�δʵ�֣�����ϵ��Ϣ�ƣ�\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        FS.HISFC.BizProcess.Interface.Common.ILis lisInterface = null;

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
                    lisInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.Common.ILis)) as FS.HISFC.BizProcess.Interface.Common.ILis;
                }

                if (lisInterface == null)
                {
                    if (string.IsNullOrEmpty(FS.FrameWork.WinForms.Classes.UtilInterface.Err))
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("��ѯLIS�ӿڳ��ִ���\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("û��ά��LIS�ӿڣ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        protected FS.HISFC.BizProcess.Interface.Common.IPacs pacsInterface = null;

        /// <summary>
        /// ���뵥�ӿ�
        /// </summary>
        private static FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientPacsApply IOutPatientPacsApply = null;

        /// <summary>
        /// ���뵥�ӿ�
        /// </summary>
        private void InitPacsApply()
        {
            if (IOutPatientPacsApply == null)
            {
                IOutPatientPacsApply = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientPacsApply)) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientPacsApply;
            }
        }

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
                //ҽ��վ�Ѿ������ˣ��Ժ���ϵͳ��ģ���ȱ�ݣ�ǿ�Ƶ�Ҫ�������Լ��������
                //#region Kill��Pacs ���� Pacs �Լ����ؽ���

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
                            ucOutPatientItemSelect1.MessageBoxShow("��ѯPACS�ӿڳ��ִ���\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(FS.FrameWork.Management.Language.Msg("û��ά��PACS�����ѯ�ӿڣ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }


                    // this.pacsInterface.Connect();

                    if (this.pacsInterface.Connect() == 0)
                    {
                        ucOutPatientItemSelect1.MessageBoxShow("��ʼ��PACSʧ�ܣ�����ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                this.pacsInterface.OprationMode = "1";
                this.pacsInterface.SetPatient(Patient);
                pacsInterface.PlaceOrder(this.GetSelectOrders());

                if (this.pacsInterface.ShowResultByPatient() == 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("�鿴PACS���ʧ�ܣ����ڰ�һ�ΰ�ť��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("�鿴PACS������ִ������ڰ�һ�ΰ�ť��\r\n" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// �޸ĵ�����Ŀ��Ϣ
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
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��")].Text = outOrder.Note;
            }
            else if (changedField == EnumOrderFieldList.OrderType)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ҽ������")].Text = outOrder.OrderType;
            }
            else if (changedField == EnumOrderFieldList.OrderID)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ҽ����ˮ��")].Text = outOrder.ID;
            }
            else if (changedField == EnumOrderFieldList.Status)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ҽ��״̬")].Text = outOrder.Status.ToString();
            }
            else if (changedField == EnumOrderFieldList.CombNo)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��Ϻ�")].Text = outOrder.Combo.ID;
            }
            else if (changedField == EnumOrderFieldList.MainDrug)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��ҩ")].Text = FS.FrameWork.Function.NConvert.ToInt32(outOrder.Combo.IsMainDrug).ToString();
            }
            else if (changedField == EnumOrderFieldList.ItemCode)
            {
                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��Ŀ����")].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).UserCode;
                    }
                }
                else
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��Ŀ����")].Text = SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrder.Item.ID).UserCode;
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
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = outOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = outOrder.Unit;

                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//������С��λ 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;


                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;


                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + outOrder.Item.PriceUnit;

                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.DoseOnce
                || changedField == EnumOrderFieldList.DoseUnit)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ÿ������")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.DoseOnce);
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��λ")].Text = outOrder.DoseUnit;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = outOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = outOrder.Unit;

                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//������С��λ 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;
                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + outOrder.Item.PriceUnit;
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.HerbalQty)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����/����")].Text = outOrder.HerbalQty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = outOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = outOrder.Unit;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("Ժע����")].Text = outOrder.InjectCount.ToString();

                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//������С��λ 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;
                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + outOrder.Item.PriceUnit;

                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.FrequencyCode
                || changedField == EnumOrderFieldList.Frequency)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("Ƶ�α���")].Text = outOrder.Frequency.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("Ƶ������")].Text = outOrder.Frequency.Name;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = outOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("������λ")].Text = outOrder.Unit;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("Ժע����")].Text = outOrder.InjectCount.ToString();

                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//������С��λ 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + outOrder.Item.PriceUnit;

                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.UsageCode
                || changedField == EnumOrderFieldList.Usage)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("�÷�����")].Text = outOrder.Usage.Name;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("�÷�����")].Text = outOrder.Usage.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("Ժע����")].Text = outOrder.InjectCount.ToString();
            }
            else if (changedField == EnumOrderFieldList.InjNum)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("Ժע����")].Text = outOrder.InjectCount.ToString();
            }
            else if (changedField == EnumOrderFieldList.Specs)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = outOrder.Item.Specs;
            }
            else if (changedField == EnumOrderFieldList.Price)
            {
                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//������С��λ 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackQty, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + outOrder.Item.PriceUnit;

                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.TotalCost)
            {
                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    if (!string.Equals(outOrder.Item.ID, "999"))
                    {
                        if (outOrder.MinunitFlag != "1")//��װ��λ 
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;
                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Qty * outOrder.Item.Price, 2);
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price / outOrder.Item.PackQty, 2) + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).MinUnit;

                            neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty).ToString();
                        }
                    }
                }
                else
                {
                    this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����")].Text = FS.FrameWork.Public.String.ToSimpleString(outOrder.Item.Price, 2) + "Ԫ/" + outOrder.Item.PriceUnit;


                    neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("���")].Text = GetCost(outOrder.Qty * outOrder.Item.Price).ToString();
                }
            }
            else if (changedField == EnumOrderFieldList.BeginDate)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��ʼʱ��")].Text = outOrder.BeginTime.ToString();
            }
            else if (changedField == EnumOrderFieldList.ReciptDoct)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����ҽ��")].Text = outOrder.ReciptDoctor.Name;
            }
            else if (changedField == EnumOrderFieldList.ExecDeptCode
                || changedField == EnumOrderFieldList.ExeDept)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ִ�п��ұ���")].Text = outOrder.ExeDept.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ִ�п���")].Text = outOrder.ExeDept.Name;
            }
            else if (changedField == EnumOrderFieldList.Emc)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("�Ӽ�")].Value = outOrder.IsEmergency;
            }
            else if (changedField == EnumOrderFieldList.CheckBody)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��鲿λ")].Text = outOrder.CheckPartRecord;
            }
            else if (changedField == EnumOrderFieldList.Sample)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��������")].Text = outOrder.Sample.Name;
            }
            else if (changedField == EnumOrderFieldList.DrugDeptCode
                || changedField == EnumOrderFieldList.DrugDept)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ȡҩҩ������")].Text = outOrder.StockDept.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ȡҩҩ��")].Text = outOrder.StockDept.Name;
            }
            else if (changedField == EnumOrderFieldList.Memo)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��ע")].Text = outOrder.Memo;
            }
            else if (changedField == EnumOrderFieldList.InputOperCode
                || changedField == EnumOrderFieldList.InputOper)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("¼���˱���")].Text = outOrder.Oper.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("¼����")].Text = outOrder.Oper.Name;
            }
            else if (changedField == EnumOrderFieldList.ReciptDept)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��������")].Text = outOrder.ReciptDept.Name;
            }
            else if (changedField == EnumOrderFieldList.MoDate)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("����ʱ��")].Text = outOrder.MOTime.ToString();
            }
            else if (changedField == EnumOrderFieldList.EndDate)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ֹͣʱ��")].Text = outOrder.EndTime.ToString();
            }
            else if (changedField == EnumOrderFieldList.DCOperCode
                || changedField == EnumOrderFieldList.DCOper)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ֹͣ�˱���")].Text = outOrder.DCOper.ID;
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ֹͣ��")].Text = outOrder.DCOper.Name;
            }
            else if (changedField == EnumOrderFieldList.SubComb)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("ҽ������")].Text = ShowOrderName(outOrder);

                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("˳���")].Text = outOrder.SortID.ToString();
                neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��Ϻ�")].Text = outOrder.Combo.ID;
            }
            else if (changedField == EnumOrderFieldList.OrderNo)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("˳���")].Text = outOrder.SortID.ToString();
                neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("��Ϻ�")].Text = outOrder.Combo.ID;
            }
            else if (changedField == EnumOrderFieldList.HypoTestCode)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("Ƥ�Դ���")].Text = outOrder.Item.SysClass.Name;
            }
            else if (changedField == EnumOrderFieldList.HypoTest)
            {
                this.neuSpread1.ActiveSheet.Cells[rowIndex, GetColumnIndexFromName("Ƥ��")].Text = outOrder.Item.SysClass.Name;
            }

            neuSpread1.ActiveSheet.Rows[rowIndex].Tag = outOrder;
        }

        /// <summary>
        /// ҽ���仯����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        protected virtual void ucItemSelect1_OrderChanged(FS.HISFC.Models.Order.OutPatient.Order sender, EnumOrderFieldList changedField)
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

            #region �ظ�ҽ����ʾ
            if (isShowSameOrder)
            {
                #region ��ʾ���ξ����ѿ�������ͬ��Ŀ��ҽ��
                if (this.SameOrderList != null && this.SameOrderList.Count > 0)
                {
                    foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in this.SameOrderList)
                    {
                        if (orderTemp.Item.ID == ((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.ID)
                        {
                            if (MessageBox.Show("��Ŀ��" + orderTemp.Item.Name + "���ڡ�" + orderTemp.MOTime.ToString() + "���ѿ���,�Ƿ����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return;
                            }

                            break;
                        }
                    }
                }

                #endregion

                #region ��ʾ���������Ŀ�Ƿ�������շѵ�δ������ļ�¼���������
                if (this.LastOrderList != null && this.LastOrderList.Count > 0)
                {
                    foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in this.LastOrderList)
                    {
                        //����Լ�����Ŀ�Լ�������Ŀ
                        if (orderTemp.Item.SysClass.ID.ToString() == "UL")
                        {

                            if (orderTemp.Item.ID == ((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.ID && orderTemp.User01 != "�ѳ�����")
                            {
                                if (MessageBox.Show("��Ŀ��" + orderTemp.Item.Name + "���ڡ�" + orderTemp.MOTime.ToString() + "���ѿ�����Ϊ" + orderTemp.User01 + "״̬ ,�Ƿ����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                {
                                    return;
                                }

                                break;
                            }

                        }
                        //������鲻���ڡ��ѱ��桱״̬  
                        else if ((orderTemp.Item.SysClass.ID.ToString() == "UC" && orderTemp.ExeDept.ID == "6003"))
                        {
                            if (orderTemp.Item.ID == ((FS.HISFC.Models.Order.OutPatient.Order)sender).Item.ID && orderTemp.User01 == "δִ��")
                            {
                                if (MessageBox.Show("��Ŀ��" + orderTemp.Item.Name + "���ڡ�" + orderTemp.MOTime.ToString() + "���ѿ�������Ϊ�ѹ���δִ��״̬ ,�Ƿ����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
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

            #region ����
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

            #region ɾ��
            else if (this.ucOutPatientItemSelect1.OperatorType == Operator.Delete)
            {

            }
            #endregion

            #region �޸�
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
                                    //{AA8348EF-8669-4ebf-B863-95469A7A04E2}�����޸ĵ�λ����������е�λ�����ű仯
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
                                //�޸�Ժע
                                else if (changedField == EnumOrderFieldList.InjNum
                                    || changedField == EnumOrderFieldList.ExeDept
                                    )
                                {
                                    order.InjectCount = sender.InjectCount;
                                    order.ExeDept = sender.ExeDept;

                                    GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                    //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                }
                                //���������/������Ƶ�θı�, ���������һ��ı�, �������¼�������
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
                                //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                                else if (changedField == EnumOrderFieldList.SubComb)
                                {
                                    #region �����ͬ��һ��ѡ��
                                    //���������ѡ��
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
                                #region ȫѡ�޸�

                                //����ȫѡ�޸�
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

                                        //���ڼ���Ժע����������ֻ��ʾ
                                        string errInfo = "";
                                        if (Classes.Function.CalculateInjNum(order, ref errInfo) == -1)
                                        {
                                            ucOutPatientItemSelect1.MessageBoxShow("����Ժע��������\r\n" + errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }

                                        GetOrderChanged(int.Parse(alRows[i].ToString()), order, changedField);
                                        //this.AddObjectToFarpoint(order, int.Parse(alRows[i].ToString()), this.neuSpread1.ActiveSheetIndex, EnumOrderFieldList.Item);
                                    }
                                }
                                //�÷�ȫѡ�޸�
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

                    //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    if (changedField == EnumOrderFieldList.SubComb)
                    {
                        #region �����ͬ��һ��ѡ��
                        //���������ѡ��
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

                #region ������

                //if (dealSublMode == 1)
                //{
                if (this.currentPatientInfo != null && !string.IsNullOrEmpty(this.currentPatientInfo.ID))
                {
                    dirty = true;
                    if (this.IDealSubjob != null
                        && IDealSubjob.IsAllowUsageSubPopChoose
                        )
                    {
                        ////��ΪֻҪ����ÿ�����ͼ��㸽�ģ��ᵼ�³���ͣ��ѯ���ݿ⣬������
                        ////����ȡ��ÿ��������ʱ�����㸽�ĵĹ���
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
                            #region ��������

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
                                    ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + errText);
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

            //��Щʱ����Ҫˢ��״̬����Ϻŵ�
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

            ShowPactItem();
        }

        /// <summary>
        /// �Զ������п�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[0], SetingFileName);
        }

        /// <summary>
        /// ��ʾ��Ŀ��Ϣ
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

                #region ��ʾ��Ŀ��Ϣ

                FS.HISFC.Models.Order.OutPatient.Order outOrder = GetObjectFromFarPoint(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
                if (outOrder == null)
                {
                    return -1;
                }
                this.pnItemInfo.Visible = true;
                txtItemInfo.ReadOnly = true;

                string showInfo = "";

                //��Ŀ��Ϣ
                if (outOrder.Item.ID == "999")
                {
                    showInfo += outOrder.Item.Name + " �����" + outOrder.Item.Specs + " �����ۡ�" + outOrder.Item.Price.ToString() + "Ԫ/" + outOrder.Item.PriceUnit;
                }
                else
                {
                    if (outOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).UserCode + " " + outOrder.Item.Name + " �����" + outOrder.Item.Specs + " �����ۡ�" + outOrder.Item.Price.ToString() + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).PackUnit;
                        if (!string.IsNullOrEmpty(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).Product.Manual))
                        {
                            showInfo += "\r\n" + "��ҩƷ˵����" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID).Product.Manual;
                        }
                    }
                    else
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrder.Item.ID).UserCode + " " + outOrder.Item.Name + " �����" + outOrder.Item.Specs + " �����ۡ�" + outOrder.Item.Price.ToString() + "Ԫ/" + outOrder.Item.PriceUnit;
                    }
                  
                  
                }
                if (outOrder.Item.ID != "999")
                {
                    #region ��Ŀ��չ��Ϣ��ʾ

                    string itemShowInfo = "";

                    if (this.Patient != null && !string.IsNullOrEmpty(this.Patient.ID))
                    {
                        FS.HISFC.Models.SIInterface.Compare compare = Order.Classes.Function.GetPactItem(outOrder);
                        outOrder.Patient.Pact = this.Patient.Pact;
                        if (compare != null)
                        {
                            //ҽ��������Ϣ
                            itemShowInfo += "\r\n��" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(outOrder.Patient.Pact.ID).Name + "�� " + Order.Classes.Function.GetItemGrade(compare.CenterItem.ItemGrade) + " " + (compare.CenterItem.Rate > 0 ? compare.CenterItem.Rate.ToString("p0") : "") + (compare.CenterFlag == "1" ? "����������" : "");


                            //ҽ��������ҩ��Ϣ
                            if (!string.IsNullOrEmpty(compare.Practicablesymptomdepiction))
                            {
                                itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\r\n" : " ") + compare.Practicablesymptomdepiction;
                            }
                        }
                    }

                    //����ҩ����ʾ
                    string ss = Order.Classes.Function.GetPhaEssentialDrugs(outOrder.Item);
                    if (!string.IsNullOrEmpty(ss))
                    {
                        itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\t" : " ") + "[" + ss + "]";
                    }

                    //������ҩ��ʾ
                    ss = Order.Classes.Function.GetPhaForTumor(outOrder.Item);
                    if (!string.IsNullOrEmpty(ss))
                    {
                        itemShowInfo += (string.IsNullOrEmpty(itemShowInfo) ? "\t" : " ") + "[" + ss + "]";
                    }

                    //��Ŀ�ں� ����

                    showInfo += itemShowInfo;

                    #endregion

                    //�ײ���ϸ
                    if (outOrder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrder.Item.ID);
                        if (undrug.UnitFlag == "1")
                        {
                            showInfo += "\r\n���ײͰ�������";

                            ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(outOrder.Item.ID);
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb comb in alZt)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(comb.ID);
                                showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "]") + " " + comb.Qty + combUndrug.PriceUnit + "��";
                            }
                        }
                    }


                    //������Ϣ
                    FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();
                    ArrayList alSub = subMgr.GetSubtblInfoByItem("0", outOrder.ReciptDept.ID, outOrder.Item.ID, outOrder.Usage.ID);
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
                FS.SOC.HISFC.Fee.Models.Undrug undrugInfo = new FS.SOC.HISFC.Fee.BizLogic.Undrug().GetUndrug(outOrder.Item.ID);

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
                MessageBox.Show("ucOutPatientOrder" + ex.Message);
                return -1;
            }
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
        /// ɾ���Ĵ���ID�����ڱ���ʱɾ��
        /// </summary>
        private Hashtable hsDeleteOrder = new Hashtable();

        /// <summary>
        /// ɾ��ѡ����Ŀ
        /// </summary>
        /// <param name="rowIndex">������</param>
        /// <param name="isDirctDel">�Ƿ���ʾɾ����������ʾֱ��ɾ��</param>
        /// <returns></returns>
        private int Del(int rowIndex, bool isNotice)
        {
            #region ȫ��ɾ������
            int j = rowIndex;
            DialogResult r = DialogResult.Yes;
            bool isHavePha = false;
            FS.HISFC.Models.Order.OutPatient.Order order = null;//,temp=null;
            if (j < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("����ѡ��һ��ҽ����");
                return 0;
            }
            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
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

            if (!isNotice)
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
        /// ɾ��һ������
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int DeleteOneOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            //���Ŀ����ڽӿ������Ѿ�ɾ������
            if (order.IsSubtbl)
            {
                FS.HISFC.Models.Order.OutPatient.Order orderTemp = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                if (orderTemp == null)
                {
                    return 1;
                }
            }

            //ɾ��ҽ��
            if (CacheManager.OutOrderMgr.DeleteOrder(order.SeeNO, FS.FrameWork.Function.NConvert.ToInt32(order.ID)) <= 0)
            {
                errInfo = "��Ŀ��" + order.Item.Name + "�������Ѿ��շѣ����˳�������������!\r\n" + CacheManager.OutOrderMgr.Err;
                return -1;
            }
            //ɾ������
            if (CacheManager.FeeIntegrate.DeleteFeeItemListByMoOrder(order.ID) == -1)
            {
                errInfo = "��Ŀ��" + order.Item.Name + "�������Ѿ��շѣ����˳�������������!\r\n" + CacheManager.OutOrderMgr.Err;
                return -1;
            }

            #region ҽ�����ĸ��ĵ�ɾ��{D256A1B3-F969-4d2c-92C3-9A5508835D5B}
            //������Ͽ�����ϺŸı䣬�޸�Ϊ���մ����Ż�ȡ������ϸ

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
                            errInfo = "��Ŀ��" + item.Name + "����Ӧ�ĸ����Ѿ��շѣ�������ɾ����\r\n���˳��������ԣ�";
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
                            //ɾ����ҩƷ�ն�������Ϣ
                            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                            FS.HISFC.BizProcess.Integrate.Terminal.Confirm confrimIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
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

            order.DCOper.ID = CacheManager.OutOrderMgr.Operator.ID;
            order.DCOper.OperTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            if (this.isSaveOrderHistory)
            {
                if (CacheManager.OutOrderMgr.InsertOrderChangeInfo(order) < 0)
                {
                    errInfo = "����" + order.Item.Name + "�޸ļ�¼����" + CacheManager.OutOrderMgr.Err;
                    return -1;
                }
            }

            #region ɾ��Ԥ�ۿ����Ϣ {E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
            if (isPreUpdateStockinfoByOrder)
            {
                if (DealPreStock(true, order) == -1)
                {
                    return -1;
                }
            }
            #endregion

            #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504

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
        /// ɾ������
        /// </summary>
        /// <returns></returns>
        public int DeleteSingleOrder()
        {
            #region ɾ������

            DialogResult r = DialogResult.Yes;

            if (neuSpread1.ActiveSheet.ActiveRowIndex < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("����ѡ��һ��ҽ����");
                return 0;
            }

            FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[neuSpread1.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.OutPatient.Order;

            if (order == null)
            {
                return 0;
            }
            r = ucOutPatientItemSelect1.MessageBoxShow("�Ƿ�ɾ����ѡ��ҽ����" + order.Item.Name + "��\n ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
        /// ɾ��һ������
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
                ucOutPatientItemSelect1.MessageBoxShow("��ҽ�����ǵ�ǰҽ������,����ɾ��!", "��ʾ");
                return 0;
            }

            if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(order, this.GetReciptDoct(), this.GetReciptDept(), DoctorPrivType.RecipePriv, true, ref errInfo) <= 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "��ʾ");
                return 0;
            }

            if (order.ID == "") //��Ȼɾ��
            {
                this.neuSpread1.ActiveSheet.Rows.Remove(rowIndex, 1);
            }
            else
            {
                //FS.HISFC.Models.Order.OutPatient.Order orderTemp = CacheManager.OrderMgr.QueryOneOrder(this.Patient.ID, order.ID);

                if (order.Status == 0)
                {
                    //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
                    FS.HISFC.Models.Order.OutPatient.Order temp = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);

                    //�Ҳ���ʱ��ʾ
                    if (temp == null)
                    {
                        //ucOutPatientItemSelect1.MessageBoxShow("ɾ��ҽ��ʧ�ܣ�\r\n" + CacheManager.OrderMgr.Err, "����");
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
                    ucOutPatientItemSelect1.MessageBoxShow("ҽ��:[" + order.Item.Name + "]�Ѿ��շѣ����ܽ���ɾ��������", "��ʾ");
                    return 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// ���ÿط�����
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
        /// ȷ��ɾ��
        /// </summary>
        /// <param name="errInfo"></param>
        /// <param name="feeSeq">���� �շ����У��������ﴦ��ȫɾʱ����¼ԭ���շ����У�����ʾ��ȷ�ܷ���</param>
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
                //�Ż���ѯ���� {BE4B33A4-D86A-47da-87EF-1A9923780A5C}
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

                #region ����ҽ����������ҩ

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
                                MessageBox.Show("ɾ��ҽ����չ��Ϣ����\r\n" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }
                    }
                }

                #endregion
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

                string memo = this.currentPatientInfo.Memo;

                //��ѯ��Ч�ĹҺż�¼
                ArrayList alRegister = CacheManager.RegInterMgr.QueryPatientByState(this.currentPatientInfo.ID, "all");
                if (alRegister == null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("��ѯ���߹Һ���Ϣ����!�����ǻ����Ѿ��˺ţ�\r\n" + CacheManager.RegInterMgr.Err);
                    return -1;
                }

                if (alRegister.Count > 1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("�û��߹Һ���Ϣ�����ϣ���ˢ�½���!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else if (alRegister.Count == 1)
                {
                    ((FS.HISFC.Models.Registration.Register)alRegister[0]).DoctorInfo.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO;

                    ////�Ѿ����� ���Ҳ��ǵ�ǰҽ��
                    //if (
                    //    //!currentPatientInfo.IsSee
                    //    //&& 
                    //    ((FS.HISFC.Models.Registration.Register)alRegister[0]).IsSee
                    //    && ((FS.HISFC.Models.Registration.Register)alRegister[0]).SeeDoct.ID != this.GetReciptDoct().ID
                    //    )
                    //{
                    //    if (ucOutPatientItemSelect1.MessageBoxShow("���ߡ�" + currentPatientInfo.Name + "���Ѿ����\r\n����ҽ��Ϊ:" + ((FS.HISFC.Models.Registration.Register)alRegister[0]).SeeDoct.Name + "��\r\n����ʱ��Ϊ" + ((FS.HISFC.Models.Registration.Register)alRegister[0]).SeeDoct.OperTime.ToString() + "��\r\n\r\n�Ƿ�������", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
        /// ��ӣ�����
        /// </summary>
        /// <param name="IsNew">�Ƿ�ǿ���¿���</param>
        /// <returns></returns>
        public int Add(bool IsNew)
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

            if (IsNew)
            {
                Patient.DoctorInfo.SeeNO = -1;
                QueryOrder();
            }

            this.hsDeleteOrder.Clear();
            this.IsDesignMode = true;

            this.ucOutPatientItemSelect1.Clear(true);

            //this.ucOutPatientItemSelect1.Focus();

            //�˴�����ֻ�ڵ�һ�γ����޶�ʱ��ʾ
            this.isShowFeeWarning = false;

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
            //Classes.LogManager.Write(currentPatientInfo.Name + "��ʼ���﹦��!");
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

            string dept = CacheManager.OutOrderMgr.ExecSqlReturnOne(string.Format(@"select see_dpcd from fin_opr_register t
                                                                    where t.card_no='{0}'
                                                                    and t.in_state!='N' 
                                                                    and t.in_state is not null", this.Patient.PID.CardNO));
            if (!string.IsNullOrEmpty(dept) && dept != "-1" && dept != this.GetReciptDept().ID)
            {
                FS.HISFC.Models.Base.Department deptObj = CacheManager.InterMgr.GetDepartment(dept);
                if (deptObj != null)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("�û�������" + deptObj.Name + "���ۣ�");
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
                this.currentPatientInfo.DoctorInfo.SeeNO = CacheManager.OutOrderMgr.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//����µĿ������
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ�������ʧ�ܣ�" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                #region ����Ƿ��Ѵ������۷�
                string strSql = string.Format(@"select count(t.item_code) from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                --and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�������۷�'", this.Patient.ID, this.GetReciptDoct().ID);
                string revStr = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSql);
                int rev = FS.FrameWork.Function.NConvert.ToInt32(revStr);
                if (rev > 0)
                {
                    if (ucOutPatientItemSelect1.MessageBoxShow("�û�������ȡ���۷ѣ��Ƿ������ȡ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else if (rev == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("��ѯ���۷���ʧ�ܣ�" + CacheManager.OutOrderMgr.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    #region �������չҺŷ���

                    rev = CacheManager.OutOrderMgr.ExecNoQuery(@"delete from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�Һŷ�'", this.Patient.ID, this.GetReciptDoct().ID);
                    if (rev == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
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
                        string invoiceNo = CacheManager.OutOrderMgr.ExecSqlReturnOne(sql);
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
                            string invoiceNoTemp = CacheManager.OutOrderMgr.ExecSqlReturnOne(sql);
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
                                string invoiceSeq = CacheManager.OutOrderMgr.ExecSqlReturnOne(sql);

                                //�Һŷѷ�Ʊ���ϣ�ͬʱ�˻��˻����
                                if (CacheManager.FeeIntegrate.LogOutInvoiceByAccout(this.Patient, invoiceNo, invoiceSeq) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    ucOutPatientItemSelect1.MessageBoxShow("�˻��������ʧ��:" + CacheManager.OutOrderMgr.Err);
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
                    bool iReturn = CacheManager.FeeIntegrate.SetChargeInfo(this.Patient, alEmergencyFee, now, ref errText);
                    if (iReturn == false)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        alEmergencyFee.Remove(regFeeItem);
                        ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    ucOutPatientItemSelect1.MessageBoxShow("��������״̬ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            #region ���ۺ󣬸��¿�����Ϣ
            //����ҺŷѺ󣬸��¹Һű����շ�״̬�������β��չҺŷ�
            if (CacheManager.RegInterMgr.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                alEmergencyFee.Remove(regFeeItem);
                dirty = false;
                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.ConManager.Err);
                return -1;
            }

            //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                if (CacheManager.RegInterMgr.UpdateSeeDone(this.currentPatientInfo.ID) < 0)
                {
                    errInfo = "���¿����־����";

                    return -1;
                }

                if (CacheManager.RegInterMgr.UpdateDept(this.currentPatientInfo.ID, CacheManager.LogEmpl.Dept.ID, CacheManager.LogEmpl.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    alEmergencyFee.Remove(regFeeItem);
                    ucOutPatientItemSelect1.MessageBoxShow("���¿�����ҡ�ҽ������");
                    return -1;
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            this.currentPatientInfo.PVisit.InState.ID = "R";
            this.currentPatientInfo.IsFee = true;

            ucOutPatientItemSelect1.MessageBoxShow("���۳ɹ���");
            //Classes.LogManager.Write(currentPatientInfo.Name + "�������﹦��!");
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

            FS.HISFC.Models.Fee.Item.Undrug supplyItem;
            FS.HISFC.Models.Fee.Outpatient.FeeItemList emergencyFeeItem = null;

            string supplyItemCode = "";
            ArrayList emergencyConstList = CacheManager.GetConList("EmergencyFeeItem");
            if (emergencyConstList == null)
            {
                errInfo = "��ȡ����������Ŀʧ�ܣ�" + CacheManager.ConManager.Err;
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
                string revStr = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSql);
                int rev = FS.FrameWork.Function.NConvert.ToInt32(revStr);
                if (rev <= 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("δ�ҵ����۷��ã����ó��أ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                #endregion

                if (ucOutPatientItemSelect1.MessageBoxShow("�Ƿ�ɾ��������ȡ�����۷��ã�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //ȡ�����ۣ�ɾ�����۷�
                    if (this.alEmergencyFee != null && this.alEmergencyFee.Count > 0)
                    {
                        rev = CacheManager.OutOrderMgr.ExecNoQuery(@"delete from fin_opb_feedetail t
                                                where t.clinic_code='{0}'
                                                and t.pay_flag='0'
                                                and t.DOCT_CODE='{1}'
                                                and t.package_name='�������۷�'", this.Patient.ID, this.GetReciptDoct().ID);
                        if (rev == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
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
                            string invoiceNo = CacheManager.OutOrderMgr.ExecSqlReturnOne(sql);
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
                if (CacheManager.RadtIntegrate.OutObservePatientManager(this.currentPatientInfo, EnumShiftType.EO, "����") < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("��������״̬ʧ�ܣ�\r\n" + CacheManager.RadtIntegrate.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            ucOutPatientItemSelect1.MessageBoxShow("ȡ���������۳ɹ���");
            return 1;
        }

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
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (CacheManager.RadtIntegrate.OutObservePatientManager(this.currentPatientInfo, EnumShiftType.CPI, "����תסԺ") < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow("��������״̬ʧ�ܣ�");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
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
                FS.HISFC.Models.Order.OutPatient.Order ord = this.GetObjectFromFarPoint(i, this.neuSpread1.ActiveSheetIndex);

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
            this.isAddMode = false;
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
        /// ��ȡ������ͬ��λ
        /// houwb ҽ�����Էѷ����� {A1FE7734-267C-43f1-A824-BF73B482E0B2}
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
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            Classes.LogManager.Write(currentPatientInfo.Name + "��ʼ���溯��!");
            if (this.bIsDesignMode == false)
            {
                return -1;
            }

            this.ucOutPatientItemSelect1.SetInputControlVisible(false);

            string strID = "";
            string strNameNotUpdate = "";//û�и��µ�ҽ������
            string reciptNo = "";//������
            bool bHavePha = false;//�Ƿ����ҩƷ(����Ԥ��ʹ��)


            FS.HISFC.Models.Order.OutPatient.Order order;
            DateTime now = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();


            #region ��ȡ�����б�

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

            #region ����֮ǰ���ж�

            if (this.GetRecentPatientInfo() == -1)
            {
                return -1;
            }

            this.hsOrder.Clear();
            this.hsOrderItem.Clear();

            //�ڴ˴���������ҽ���б��ҽ����Ŀ�б�

            int iReturnCheckOrder = this.CheckOrder();
            if (iReturnCheckOrder == -1)
            {
                return -1;
            }
            else if (iReturnCheckOrder == -2)
            {
                #region ���ش���
                this.IsDesignMode = false;
                this.isAddMode = false;

                //��������
                this.hsComboChange = new Hashtable();
                //this.alSupplyFee = new ArrayList();

                Classes.LogManager.Write(currentPatientInfo.Name + "�������溯��!");
                #endregion
                return -2;
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

                    Classes.LogManager.Write(currentPatientInfo.Name + "��ʼ������ʱ�����չҺŷ���ĿΪ��\r\n");
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alSupplyFee)
                    {
                        Classes.LogManager.Write(currentPatientInfo.Name + item.Item.ID + " " + item.Item.Name + "  ������" + item.Item.Qty.ToString() + "\r\n");
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
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���ҽ�������Ժ󡣡���");
            Application.DoEvents();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.OutOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //��������
            CacheManager.InterMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            CacheManager.FeeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);//��������
            CacheManager.RegInterMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            CacheManager.PhaIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);//{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}

            #endregion

            errInfo = "";

            //�շ����У��������ﴦ��ȫɾʱ����¼ԭ���շ����У�����ʾ��ȷ�ܷ���
            string feeSeq = "";
            if (this.DelCommit(ref errInfo, ref feeSeq) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow("ɾ��ҽ��ʧ�ܣ�" + errInfo);
                return -1;
            }

            #region �жϿ������
            if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
            {
                this.currentPatientInfo.DoctorInfo.SeeNO = CacheManager.OutOrderMgr.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//����µĿ������
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    ucOutPatientItemSelect1.MessageBoxShow(CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }

            #endregion

            #region ����ҽ��������

            ArrayList alOrder = new ArrayList(); //����ҽ��
            ArrayList alFeeItem = new ArrayList();//�������
            ArrayList alSubOrders = new ArrayList();//��������
            ArrayList alOrderChangedInfo = new ArrayList();//ҽ���޸ļ�¼
            ArrayList alDrugOrders = new ArrayList();
            bool iReturn = false;
            string errText = "";

            //��ʾ�ظ�ҩƷ
            string repeatItemName = "";
            Hashtable hsOrderItem = new Hashtable();

            string itemKey = "";

            //�洢ҽ����ˮ������������ҩ���
            Dictionary<string, string> dicIndications = new Dictionary<string, string>();

            //�����Ŀ����
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

                if (order.Status == 0)//δ��˵�ҽ��
                {
                    #region ����ҽ��

                    if (order.ID == "") //new �¼ӵ�ҽ��
                    {
                        strID = Classes.Function.GetNewOrderID(ref errInfo);
                        if (strID == "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1;
                        }

                        order.ID = strID;    //���뵥��
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

                        #region ����Ԥ�ۿ��{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                        if (this.isPreUpdateStockinfoByOrder)
                        {
                            if (DealPreStock(false, order) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                            }
                        }

                        #endregion

                        #region �˻����ߵĸ�����Ŀ������ϸ�ٻ���

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
                        //                ucOutPatientItemSelect1.MessageBoxShow("��ѯ��ҩƷ��Ŀ��" + order.Item.Name + "����" + this.CacheManager.FeeIntegrate.Err);
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
                        //                ucOutPatientItemSelect1.MessageBoxShow("��ѯ��ҩƷ��Ŀ��" + order.Item.Name + "����" + this.CacheManager.FeeIntegrate.Err);
                        //                return -1;
                        //            }
                        //        }

                        //        order.Item.IsNeedConfirm = undrugInfo.IsNeedConfirm;
                        //        ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).NeedConfirm = undrugInfo.NeedConfirm;

                        //        //������Ŀ���Ȳ��Ż���
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
                        FS.HISFC.Models.Order.OutPatient.Order newOrder = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                        //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
                        if (newOrder == null || newOrder.Status == 0)
                        {
                            newOrder = order;
                        }

                        if (newOrder.Status != 0 || newOrder.IsHaveCharged)//��鲢��ҽ��״̬
                        {
                            strNameNotUpdate += "[" + order.Item.Name + "]";

                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("[" + order.Item.Name + "]�����Ѿ��շ�,���˳������������½���!\r\n" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            ucOutPatientItemSelect1.MessageBoxShow(order.Item.Name + "ҽ��ʵ��ת���ɷ���ʵ�����", "��ʾ");
                            return -1;
                        }
                        alFeeItem.Add(feeitems);

                        #endregion

                        #region ����Ԥ�ۿ��{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                        if (this.isPreUpdateStockinfoByOrder)
                        {
                            if (DealPreStock(false, order) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    //��¼ҽ����������ҩ��Ϣ

                    if (neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("����/����")].Tag != null)
                    {
                        string flag = neuSpread1.ActiveSheet.Cells[i, GetColumnIndexFromName("����/����")].Tag.ToString();
                        if (!dicIndications.ContainsKey(order.ID))
                        {
                            dicIndications.Add(order.ID, flag);
                        }
                        else
                        {
                            dicIndications[order.ID] = flag;
                        }
                    }


                    //��¼���ִ�п��Һ�ҽ��
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
                        if (IDealSubjob.DealSubjob(this.Patient, alOrder, null, ref alSubOrders, ref errText) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + errText);
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region �������Ŀ��

            if (dicPacsExetDepts.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                frmPacsApply frmPacsApply = new frmPacsApply();
                frmPacsApply.setInfo(this.Patient.ID,dicPacsExetDepts, alOrder);
                frmPacsApply.ShowDialog();
                this.PurPose = frmPacsApply.PurPoses;
                //ִ�п�������Ӧ���ڼ��Ŀ������
                if (PurPose.Count != dicPacsExetDepts.Count)
                {
                    return -1;
                }

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���ҽ�������Ժ󡣡���");
            }

            #endregion 

            #region δ�ҺŻ��ߣ��˴�����Һ���Ϣ

            if (this.AddRegInfo(Patient) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                dirty = false;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(errInfo);
                return -1;
            }

            //����ҺŷѺ󣬸��¹Һű����շ�״̬�������β��չҺŷ�
            if (CacheManager.RegInterMgr.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                dirty = false;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.ConManager.Err);
                return -1;
            }

            #region ���չҺŷ���Ŀ

            //�����ҺŻ��߶����շ���
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

            #region �ϲ��շ�����


            Classes.LogManager.Write(currentPatientInfo.Name + "���Ľӿ���ĿΪ��\r\n");
            foreach (FS.HISFC.Models.Order.OutPatient.Order subOrder in alSubOrders)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList alFeeItemListTmp = Classes.Function.ChangeToFeeItemList(GetOrderPactInfo(subOrder), currentPatientInfo);
                if (alFeeItemListTmp == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow(subOrder.Item.Name + "ҽ��ʵ��ת���ɷ���ʵ�����", "��ʾ");
                    return -1;
                }
                Classes.LogManager.Write(currentPatientInfo.Name + alFeeItemListTmp.Item.ID + " " + alFeeItemListTmp.Item.Name + "  ������" + alFeeItemListTmp.Item.Qty.ToString() + "\r\n");

                alFeeItem.Add(alFeeItemListTmp);
            }

            #endregion

            #region �շ�֮ǰ��¼������Ŀ

            Classes.LogManager.Write(currentPatientInfo.Name + "�շ���ĿΪ��\r\n");

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFeeItem)
            {
                Classes.LogManager.Write(currentPatientInfo.Name + item.Item.ID + " " + item.Item.Name + "  ������" + item.Item.Qty.ToString() + "\r\n");

            }

            #endregion

            #region �շ�
            Classes.LogManager.Write(currentPatientInfo.Name + "��ʼ�շ�!");
            //�����ź���ˮ�Ź����ɷ���ҵ��㺯��ͳһ����
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
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                        if (resultValue == 0)
                        {
                            iReturn = CacheManager.FeeIntegrate.SetChargeInfo(this.Patient, alFeeItem, now, ref errText);
                            if (iReturn == false)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            ucOutPatientItemSelect1.MessageBoxShow(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow(errText + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            Classes.LogManager.Write(currentPatientInfo.Name + "�����շ�!");
            #endregion

            #region ���������źʹ�����ˮ��

            FS.HISFC.Models.Order.OutPatient.Order tempOrder = null;
            for (int k = 0; k < alOrder.Count; k++)
            {
                tempOrder = alOrder[k] as FS.HISFC.Models.Order.OutPatient.Order;

                //�����µķַ�����������޸ı�����ܻ���Ĵ����ţ�����ÿ�ζ�Ҫ����
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

            #region ����ҽ�� �������´�����

            #region ���ݽӿ�ʵ�ֶ�ҽ����Ϣ���в����ж�
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
                    ucOutPatientItemSelect1.MessageBoxShow("����ҽ���շѱ��ʧ�ܣ�" + errText);
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

                #region �����������

                temp.FT.PubCost = GetCost(temp.FT.PubCost);
                temp.FT.PayCost = GetCost(temp.FT.PayCost);
                temp.FT.OwnCost = GetCost(temp.FT.OwnCost);

                #endregion

                #region ����ҽ����
                if (CacheManager.OutOrderMgr.UpdateOrder(temp) == -1) //����ҽ����
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("����ҽ������" + temp.Item.Name + "�����Ѿ��շ�,���˳������������½���!\r\n" + CacheManager.OutOrderMgr.Err);
                    return -1;
                }
                #endregion

                #region ҽ����չ����������ر�����Ϣ{97B9173B-834D-49a1-936D-E4D04F98E4BA}
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
                            ucOutPatientItemSelect1.MessageBoxShow("������չ��Ϣ�����" + orderExtMgr2.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                #endregion

                #region ҽ����չ�� //ҽ����������ҩ�ѷ���
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
                            ucOutPatientItemSelect1.MessageBoxShow("������չ��Ϣ�����" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (rev == 0)
                        {
                            if (orderExtMgr.InsertOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow("������չ��Ϣ�����" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }


                #endregion

                #region ҽ����չ��//������Ŀ��
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
                        orderExtObj.Extend1 = strpurpose;//��ע1������Ŀ��
                        int rev = orderExtMgr.UpdateOrderExtend(orderExtObj);
                        if (rev == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            ucOutPatientItemSelect1.MessageBoxShow("������չ��Ϣ�����" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (rev == 0)
                        {
                            if (orderExtMgr.InsertOrderExtend(orderExtObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                ucOutPatientItemSelect1.MessageBoxShow("������չ��Ϣ�����" + orderExtMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }   
                    }
                }
                #endregion 

            }
            #endregion

            #region ����ҽ�������¼

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
                    temp = alOrderChangedInfo[i] as FS.HISFC.Models.Order.OutPatient.Order;

                    if (CacheManager.OutOrderMgr.InsertOrderChangeInfo(temp) < 0)
                    {
                        //���ڱ����¼����Ҳ����ʾ
                        //FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        //ucOutPatientItemSelect1.MessageBoxShow("����ҽ�������¼����");
                        //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
                    if (CacheManager.InterMgr.UpdateAssignSaved(this.currentRoom.ID, this.currentPatientInfo.ID, now, CacheManager.OutOrderMgr.Operator.ID) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        ucOutPatientItemSelect1.MessageBoxShow("���·����־����");
                        return -1;
                    }
                }

                #region �ӿڽ�������

                if (this.INurseAssign != null)
                {
                    int rInt = this.INurseAssign.DiagOut(this.currentPatientInfo, null, null, null, null, null, ref errInfo);
                }

                #endregion

            }

            //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                if (CacheManager.RegInterMgr.UpdateSeeDone(this.currentPatientInfo.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("���¿����־����");
                    return -1;
                }

                if (CacheManager.RegInterMgr.UpdateDept(this.currentPatientInfo.ID, CacheManager.LogEmpl.Dept.ID, CacheManager.LogEmpl.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    ucOutPatientItemSelect1.MessageBoxShow("���¿�����ҡ�ҽ������");
                    return -1;
                }
            }

            #endregion

            #region �ύ
            FS.FrameWork.Management.PublicTrans.Commit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //���ڲ��Һŵģ�����ɹ����ܸ������շѱ��
            if (!this.Patient.IsFee)
            {
                this.Patient.IsFee = true;
            }

            //���»���״̬Ϊ����󣬸��Ļ�����Ϣ�л��߿���״̬
            this.Patient.IsSee = true;

            #endregion

            #region �˻���ȡ�Һŷ�


            Classes.LogManager.Write(currentPatientInfo.Name + "��ʼ�۳��Һŷ�!");

            int iRes = 1;
            if (this.isAccountMode)
            {
                //ucOutPatientItemSelect1.MessageBoxShow("���б�����ִ����Ŀ�������ն�ˢ���۷ѣ�");

                //�����ҺŻ��߶����շ���
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
                            //ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n ����ԭ���ǣ��˻����㣬�뻼�ߵ��շѴ���ֵ��ɷѣ�");
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.Commit();
                            //ucOutPatientItemSelect1.MessageBoxShow("��ȡ�ҺŷѺ����ɹ���");
                        }

                    }
                }
            }
            Classes.LogManager.Write(currentPatientInfo.Name + "�����۳��Һŷ�!");
            #endregion

            #region ��ʾ��Ϣ�ŵ�һ��

            Classes.LogManager.Write(currentPatientInfo.Name + "��������ɹ�!");

            if (strNameNotUpdate == "")//�Ѿ��仯��ҽ����Ϣ
            {
                ucOutPatientItemSelect1.MessageBoxShow("ҽ������ɹ���");
            }
            else
            {
                ucOutPatientItemSelect1.MessageBoxShow("ҽ������ɹ���\n" + strNameNotUpdate + "ҽ��״̬�Ѿ��������ط����ģ��޷����и��£���ˢ����Ļ��");
            }

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
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                ucOutPatientItemSelect1.MessageBoxShow("����ҽ����ų���");
                return -1;
            }
            #endregion

            #region ����������뵥
            string isUsePacsApply = CacheManager.FeeIntegrate.GetControlValue("200212", "0");
            if (isUsePacsApply == "1" && !object.Equals(IOutPatientPacsApply, null))
            {
                IOutPatientPacsApply.Save(this.Patient, null);
            }
            #endregion

            #region ���ش���
            this.IsDesignMode = false;
            this.isAddMode = false;

            //��������
            this.hsComboChange = new Hashtable();
            this.alSupplyFee = new ArrayList();
            #endregion

            #region �Զ���ӡ����

            if (isAutoPrintRecipe)
            {
                //this.PrintRecipe();
                PrintAllBill("0", true);
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

            isShowFeeWarning = false;


            Classes.LogManager.Write(currentPatientInfo.Name + "�������溯��!");
            return 0;
        }

        #region  add by lijp 2011-11-25 �������뵥��� {102C4C01-8759-4b93-B4BA-1A2B4BB1380E}

        /// <summary>
        /// �Ƿ����õ������뵥 
        /// </summary>
        private bool isUsePacsApply = false;

        /// <summary>
        ///  �������뵥��Ϣ
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
        /// �༭���뵥
        /// </summary>
        public void EditPascApply()
        {
            try
            {
                if (!this.isUsePacsApply)
                {
                    MessageBox.Show("δ�������뵥");
                    return;
                }

                if (IOutPatientPacsApply == null)
                {
                    this.InitPacsApply();
                }

                // ��ҽ����ȡ���뵥�š�
                FS.HISFC.Models.Order.OutPatient.Order order =
                    this.GetObjectFromFarPoint(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex);
                IOutPatientPacsApply.Edit(this.Patient, order);
            }
            catch
            {
                MessageBox.Show("û�п�����Ч�ļ����Ŀҽ��");
            }
        }

        #endregion

        #region ���Һŷ�

        /// <summary>
        /// ����Һż���
        /// </summary>
        string emergRegLevl = "";

        /// <summary>
        /// ������Ƿ���Ч
        /// </summary>
        bool isValideEmergReglevl = true;

        /// <summary>
        /// ְ����Ӧ�ĹҺż���
        /// </summary>
        string regLevl_DoctLevl = "";

        /// <summary>
        /// �������۲��շ���
        /// </summary>
        ArrayList alEmergencyFee = new ArrayList();

        /// <summary>
        /// ��ȡ����Һż������
        /// </summary>
        /// <returns></returns>
        private string GetEmergRegLevlCode()
        {
            string emergencyLevlCode = "";

            //��ȡ���еĹҺż���
            ArrayList al = CacheManager.RegInterMgr.QueryAllRegLevel();

            if (al == null || al.Count == 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ѯ���йҺż�����󣡻ᵼ�²��չҺŷѴ���!\r\n����ϵ��Ϣ������ά��" + CacheManager.RegInterMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    ucOutPatientItemSelect1.MessageBoxShow("����Һż���û��ά�����ᵼ�²��չҺŷѴ���!\r\n����޼���Һż���������ά����ͣ�ü��ɣ�\r\n����ϵ��Ϣ������ά��" + CacheManager.RegInterMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                isValideEmergReglevl = isValidEmergency;
                return emergencyLevlCode;
            }
        }

        /// <summary>
        /// ��ȡ���Һ���Ŀ
        /// </summary>
        /// <returns></returns>
        private int InitSupplyItem()
        {
            //{61CC27CE-6E87-4412-8CCF-051A3862DDBD}
            //if (isInitSupplyItem)
            //{
            //    return 1;
            //}

            //֮��ӿ��Ʋ�����
            oper = CacheManager.InterMgr.GetEmployeeInfo(this.GetReciptDoct().ID);

            #region ���Ҳ����Ŀ

            diffDiagItem = null;

            string diffDiagItemCode = "";
            ArrayList diffDiagConstList = CacheManager.GetConList("DiffDiagItem");
            if (diffDiagConstList == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ�ҺŲ����Ŀʧ�ܣ�" + CacheManager.ConManager.Err);
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
                    ucOutPatientItemSelect1.MessageBoxShow("�ҺŲ����Ŀ" + errInfo);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }
            }

            #endregion

            #region �����

            //��������
            emergRegItem = new FS.HISFC.Models.Fee.Item.Undrug();

            emergRegLevl = GetEmergRegLevlCode();

            //if (string.IsNullOrEmpty(emergRegLevl))
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("��ȡ����Ŵ�������ϵ��Ϣ�ƣ�");
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
                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ����ҺŶ�Ӧ��������Ŀʧ�ܣ�\r\n" + errInfo);
                    return -1;
                }

                //FS.FrameWork.Models.NeuObject emergRegConst = this.CacheManager.ConManager.GetConstant("REGLEVEL_DIAGFEE", emergRegLevl);
                //if (emergRegConst == null || string.IsNullOrEmpty(emergRegConst.ID))
                //{
                //    ucOutPatientItemSelect1.MessageBoxShow("û��ά������Ŷ�Ӧ�����ѣ�");
                //    //return -1;
                //}

                //if (this.CheckItem(emergRegConst.Name.Trim(), ref errInfo, ref emergRegItem) == -1)
                //{
                //    ucOutPatientItemSelect1.MessageBoxShow("�����" + errInfo);
                //    //return -1;
                //}
            }

            #endregion

            #region ҽ��ְ�ƶ�Ӧ������

            diagItem = new FS.HISFC.Models.Fee.Item.Undrug();
            //{A1BAF267-6053-44e3-B96E-36E2C48DE4BD}
            string regLevl = Patient.DoctorInfo.Templet.RegLevel.ID;
            string diagItemCode = "";

            //��Ϊ�����߹Һż�����
            if (CacheManager.RegInterMgr.GetSupplyRegInfo(oper.ID, oper.Level.ID.ToString(), this.GetReciptDept().ID, ref regLevl, ref diagItemCode) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow(CacheManager.RegInterMgr.Err);
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                return -1;
            }
            regLevl_DoctLevl = regLevl;

            if (this.CheckItem(diagItemCode, ref errInfo, ref diagItem) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("ҽ����ְ��[" + oper.Level.Name + "]��Ӧ��������Ŀ" + errInfo);
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                return -1;
            }

            #endregion

            #region ���յĹҺŷ���Ŀ

            regItem = new FS.HISFC.Models.Fee.Item.Undrug();

            //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
            {
                string regItemCode = "";
                ArrayList regConstList = CacheManager.GetConList("RegFeeItem");
                if (regConstList == null || regConstList.Count == 0)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ�Һŷ���Ŀʧ�ܣ�" + CacheManager.ConManager.Err);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }
                else if (regConstList.Count > 0)
                {
                    regItemCode = ((FS.FrameWork.Models.NeuObject)regConstList[0]).ID.Trim();
                }

                if (this.CheckItem(regItemCode, ref errInfo, ref regItem) == -1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow("�Һŷ���Ŀ" + errInfo);
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    return -1;
                }
            }
            #endregion

            #region ��ҺŷѵĿ���

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

            #region �������۷���

            alEmergencyFee = new ArrayList();

            FS.HISFC.Models.Fee.Item.Undrug supplyItem;
            FS.HISFC.Models.Fee.Outpatient.FeeItemList emergencyFeeItem = null;

            string supplyItemCode = "";
            ArrayList emergencyConstList = CacheManager.GetConList("EmergencyFeeItem");
            if (emergencyConstList == null)
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ȡ����������Ŀʧ�ܣ�" + CacheManager.ConManager.Err);
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
                    ucOutPatientItemSelect1.MessageBoxShow("����������Ŀ" + errInfo);
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

                //���ڲ��յķ����������
                //emergencyFeeItem.UndrugComb.ID = this.oper.ID;
                emergencyFeeItem.UndrugComb.Name = "�������۷�";
                alEmergencyFee.Add(emergencyFeeItem);
            }

            #endregion

            isInitSupplyItem = true;

            return 1;
        }

        /// <summary>
        /// ��ǰ���ߵĹҺż�����Ϣ
        /// </summary>
        FS.HISFC.Models.Registration.RegLvlFee regLevlFee = new FS.HISFC.Models.Registration.RegLvlFee();

        /// <summary>
        /// ���չҺŷ���Ŀ
        /// </summary>
        FS.HISFC.Models.Fee.Outpatient.FeeItemList regFeeItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

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
        /// �Ƿ���ȡ���0��ʾ����ȡ��1��ʾ��ȡ ������ʾ��δ��ѯ
        /// </summary>
        private int payDiageFee = -1;

        /// <summary>
        /// �Ƿ��������һЩ�������ﲻ�����
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
        /// ���չҺŷ�:�Һŷ�+����
        ///{EF052C04-D357-4409-84E5-3E6102766746}
        /// </summary>
        /// <param name="alSupplyFee">���յķ����б�</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <param name="isReged">�Ƿ����չ��Һŷ�</param>
        /// <returns></returns>
        private int SetSupplyRegFee(ref ArrayList alSupplyFee, ref string errInfo, bool isFee)
        {
            /*
             * �������������ã�
             * 1���Һż����Ӧ�����
             * 2��ҽ��ְ����Ӧ�����
             * 3������ʱ���Ӧ�ļ������
             * 
             * �����ռ�����𣬰��Һż����գ��Һż���ûά������ҽ��ְ�� 
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
                    errInfo = "�Һŷѻ�ȡ����";
                    return -1;
                }

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

                #region ��ͬ��λ�͹Һż����Ӧ�ĹҺŷ�

                if (string.IsNullOrEmpty(Patient.DoctorInfo.Templet.RegLevel.ID))
                {
                    Patient.DoctorInfo.Templet.RegLevel = CacheManager.RegInterMgr.GetRegLevl(this.GetReciptDept().ID, oper.ID, this.oper.Level.ID);
                    if (Patient.DoctorInfo.Templet.RegLevel == null)
                    {
                        errInfo = "��ȡ�Һż������" + CacheManager.RegInterMgr.Err;
                    }
                }

                if (string.IsNullOrEmpty(Patient.DoctorInfo.Templet.RegLevel.ID))
                {
                    errInfo = "���չҺŷѴ��󣬹Һż���Ϊ�գ�";
                }

                regLevlFee = CacheManager.RegInterMgr.GetRegLevelByPactCode(Patient.Pact.ID, Patient.DoctorInfo.Templet.RegLevel.ID);
                if (regLevlFee == null)
                {
                    errInfo = "�ɺ�ͬ��λ�͹Һż����ȡ�Һŷ�ʧ�ܣ�����ϵ��Ϣ������ά��" + CacheManager.RegInterMgr.Err;
                    return 0;
                }

                #endregion

                #region һ�����

                #region Ժ��ְ������

                if (isEmpl && emplFreeRegType == 3)
                {
                    return 1;
                }

                #endregion

                //����Һ�ʱ���Ѿ���ȡ���������ղ��
                //������������ȡ���Һż����Ӧ���ҽ��ְ����Ӧ��𡢼����Ӧ��𣨼���ʱ��Σ�
                //�����ȡ���Ϊ0����������������Ŀȫ����ȡ��������Ϊ0������ȡ���
                //�����Ժ��ְ���������������Ƿ���ȡ���
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

                        if (Patient.Birthday.AddYears(65) < CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime())
                        {
                            isCanSupplyRegFee = false;
                        }


                        //����Һż����Ӧ�ĹҺŷ�Ϊ0  �򲻲���
                        if (regLevlFee.RegFee <= 0)
                        {
                            isCanSupplyRegFee = false;
                        }

                        #region Ժ��ְ��

                        ArrayList list = CacheManager.AccountMgr.GetMarkByCardNo(Patient.PID.CardNO, "Empl_CARD", "1");
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

                    if (isPayDiagFee())
                    {
                        if (isEmpl && emplFreeRegType == 2)
                        {

                        }
                        else
                        {
                            FS.FrameWork.Models.NeuObject neuObject = CacheManager.InterMgr.GetConstansObj("civilworker", currentPatientInfo.Pact.ID);


                            //ֻ�к�ͬ��λ�е����Ϊ0ʱ���Ų���
                            if (regLevlFee.OwnDigFee > 0 || (!object.Equals(neuObject, null) && !string.IsNullOrEmpty(neuObject.ID)))
                            {
                                //���߹Һ�ʱ���Ϊ�㣬����ҽ��ְ����Ӧ��������Ŀ����
                                if (Patient.RegLvlFee.OwnDigFee == 0)
                                {
                                    #region ��ȡ�Һż����Ӧ������Ŀ
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

                                    #region ��ȡ������ֵ

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




                                    #region ��ȡ������ֵ

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
                                    //���ڲ��յķ����������
                                    diagFeeItem.UndrugComb.Name = "�Һŷ�";
                                    alSupplyFee.Add(diagFeeItem);
                                }
                                else
                                {
                                    //�����Ŀû��ά���򲻲���
                                    if (diffDiagItem != null && !string.IsNullOrEmpty(diffDiagItem.ID))
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

                                        FS.HISFC.Models.Fee.Outpatient.FeeItemList diffDiagFeeItem = null;

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
                    }
                    #endregion
                }

                //δ�Һŵ�ȫ��
                //���տ���ҽ����Ӧ��ְ����ȡ����
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

                    ArrayList list = CacheManager.AccountMgr.GetMarkByCardNo(Patient.PID.CardNO, "Empl_ CARD", "1");
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
                    if (isPayDiagFee())
                    {
                        if (isEmpl && emplFreeRegType == 2)
                        {

                        }
                        else
                        {
                            //�û��ߺ�ͬ��λ�����ά��Ϊ0,����Ϊ�Ǽ������ѣ����ٲ���
                            if (regLevlFee.OwnDigFee > 0)
                            {
                                FS.HISFC.Models.Fee.Outpatient.FeeItemList diagFeeItem = null;

                                //���Һŵģ��˴���Ҫ���¹Һ���Ϣ
                                string regLevlCode = string.Empty;
                                string regDiagItemCode = string.Empty;
                                FS.HISFC.Models.Fee.Item.Undrug regDiagItem = new FS.HISFC.Models.Fee.Item.Undrug();
                                //����ʱ��Σ���������ְ���ͼ����Ӧ�����ͬʱ�����ռ�����ȡ
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

                                #region �޸Ļ��߹Һż�����Ϣ
                                //if (regLevlCode != Patient.DoctorInfo.Templet.RegLevel.ID)
                                //{
                                //    FS.HISFC.Models.Registration.RegLevel regLevlObj = CacheManager.RegInterMgr.QueryRegLevelByCode(regLevlCode);
                                //    if (regLevlObj == null)
                                //    {
                                //        ucOutPatientItemSelect1.MessageBoxShow("��ѯ�Һż�����󣬱���[" + regLevlCode + "]������ϵ��Ϣ������ά��" + CacheManager.RegInterMgr.Err);
                                //        return -1;
                                //    }
                                //    Patient.DoctorInfo.Templet.RegLevel = regLevlObj;
                                //}
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
                    }
                    #endregion
                }
                #endregion

                Patient.DoctorInfo.Templet.RegLevel.IsEmergency = isEmergency;
            }
            catch (Exception ex)
            {
                ucOutPatientItemSelect1.MessageBoxShow("���չҺŷ�ʧ�ܣ�\r\n\r\n" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private int CheckItem(string itemCode, ref string err, ref FS.HISFC.Models.Fee.Item.Undrug itemObj)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                err = "ά����������ϵ��Ϣ�ƣ�";
                return -1;
            }

            itemObj = SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemCode);
            if (itemObj == null)
            {
                err = "������Ŀʧ�ܣ�" + CacheManager.FeeIntegrate.Err;
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
        /// ���ò��Һŵķ�����ϸ��Ϣ
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
                feeitemlist.Patient.ID = Patient.ID;//������ˮ��
                feeitemlist.Patient.PID.CardNO = Patient.PID.CardNO;//���￨�� 
                feeitemlist.Order.ID = Classes.Function.GetNewOrderID(ref errInfo);

                feeitemlist.ChargeOper.ID = this.GetReciptDoct().ID;
                feeitemlist.Order.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();

                feeitemlist.ExecOper.Dept = this.GetReciptDept();

                feeitemlist.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.Item.Qty * feeitemlist.Item.Price, 2);
                feeitemlist.FT.TotCost = feeitemlist.FT.OwnCost;
                feeitemlist.FeePack = "1";

                feeitemlist.Days = 1;//��ҩ����
                feeitemlist.RecipeOper.Dept = this.GetReciptDept();//����������Ϣ
                feeitemlist.RecipeOper.Name = this.GetReciptDoct().Name;//����ҽ����Ϣ
                feeitemlist.RecipeOper.ID = this.GetReciptDoct().ID;

                feeitemlist.Order.Item.ItemType = item.ItemType;//�Ƿ�ҩƷ
                feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//����״̬

                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = feeitemlist.RecipeOper.Dept;//�Ǽǿ���
                feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//��������

                //�շ����кţ��з���ҵ���ͳһ����
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
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemObj in alCharge)
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

        #region ȡ������

        /// <summary>
        /// ȡ������
        /// </summary>
        /// <returns></returns>
        public int CanCelDiag()
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

            if (!Patient.IsSee)
            {
                ucOutPatientItemSelect1.MessageBoxShow("ȡ������ʧ��:�û��߻�δ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ucOutPatientItemSelect1.MessageBoxShow("ȡ������ʧ��:�����Ǹû��ߵĳ���ҽ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            #region �ж��Ƿ��Ѿ����շ�

            string sqlStr = @" select count(*) from fin_opb_feedetail f
                             where f.clinic_code='{0}'
                             and f.pay_flag='1'";

            sqlStr = string.Format(sqlStr, this.Patient.ID);

            string count = CacheManager.OutOrderMgr.ExecSqlReturnOne(sqlStr, "0");
            if (FS.FrameWork.Function.NConvert.ToInt32(count) == -1)
            {
                ucOutPatientItemSelect1.MessageBoxShow("ȡ������ʧ�ܣ���ѯ������Ϣ����\r\n" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else if (FS.FrameWork.Function.NConvert.ToInt32(count) > 0)
            {
                ucOutPatientItemSelect1.MessageBoxShow("ȡ������ʧ��:�û����Ѿ��շѣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ucOutPatientItemSelect1.MessageBoxShow("���ߴ���δ�շ�ҽ������ɾ������ȡ�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            #region ���¿���״̬

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
                ucOutPatientItemSelect1.MessageBoxShow("ȡ������ʧ�ܣ���ѯ������Ϣ����\r\n" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else if (rev == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                ucOutPatientItemSelect1.MessageBoxShow("ȡ������ʧ�ܣ������Ǹû����Ѿ��˺ţ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            #endregion

            #region ���·���

            if (isUseNurseArray
                && SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(CacheManager.LogEmpl.Dept.ID) != null)
            {
                //1�����ҷ������
                FS.HISFC.Models.Nurse.Assign info = CacheManager.InterMgr.QueryAssignByClinicID(Patient.ID);

                if (info == null)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //ucOutPatientItemSelect1.MessageBoxShow(managerAssign.Err);
                    //return -1;
                    //û�ж��оͲ������ˣ���Щ�ǷǷ������
                    return 1;
                }
                //2�����Ӷ�����count
                //��ʵ���Ӧ��������ģ���������ǽ���״̬�Ͳ���Ҫ��һ��
                //��ʱ���洦���޸Ļ���״̬Ϊ�ѽ���״̬
                //if (this.UpdateQueue(info.Queue.ID, "1") == -1)
                //{
                //    this.ErrCode = "������̨���ﻼ����������";
                //    return -1;
                //}

                //3��ȡ������������״̬��ע���޸�Ϊ����״̬��
                //������߲��������￴�ˣ��һ�ʿȡ�����ȡ�������
                sqlStr = @"UPDATE met_nuo_assignrecord
                            SET out_date = null,
                               --doct_code = null,
                               assign_flag = '1'
                            WHERE clinic_code = '{0}'";

                rev = CacheManager.OutOrderMgr.ExecNoQuery(sqlStr, Patient.ID);
                if (rev == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("ȡ������ʧ�ܣ����·������Ϣʧ�ܣ�\r\n" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else if (rev == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    ucOutPatientItemSelect1.MessageBoxShow("ȡ������ʧ�ܣ������Ǹû����Ѿ�ȡ�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region ���

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int DiagOut()
        {
            Classes.LogManager.Write(currentPatientInfo.Name + "��ʼ���������");
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

            //���»���״̬Ϊ����󣬸��Ļ�����Ϣ�л��߿���״̬
            this.Patient.IsSee = true;

            //{AFA934B4-BA14-47c7-B228-BA8E48D60767}
            if (this.INurseAssign != null)
            {
                int rInt = this.INurseAssign.DiagOut(this.currentPatientInfo, null, null, null, null, null, ref errInfo);
            }

            #region �˻���ȡ�Һŷ�

            if (this.isAccountMode)
            {
                //�����ҺŻ��߶����շ���
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
                            ucOutPatientItemSelect1.MessageBoxShow(errInfo + "\r\n ����ԭ���ǣ��˻����㣬�뻼�ߵ��շѴ���ֵ��ɷѣ�");
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.Commit();
                            //ucOutPatientItemSelect1.MessageBoxShow("��ȡ�ҺŷѺ����ɹ���");
                            this.Patient.IsFee = true;
                        }

                    }
                }
            }
            #endregion

            Classes.LogManager.Write(currentPatientInfo.Name + "�������������");

            this.SetOrderFeeDisplay(false, true);

            return 1;
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
                this.currentPatientInfo.DoctorInfo.SeeNO = CacheManager.OutOrderMgr.GetNewSeeNo(this.currentPatientInfo.PID.CardNO);//����µĿ������
                if (this.currentPatientInfo.DoctorInfo.SeeNO == -1)
                {
                    errInfo = "��ȡ�������ʧ�ܣ�" + CacheManager.OutOrderMgr.Err;
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
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj in alSupplyFee)
                {
                    alFeeItem.Add(feeItemObj);
                }

                //alFeeItem.AddRange(this.alSupplyFee);
            }

            //����ҺŷѺ󣬸��¹Һű����շ�״̬�������β��չҺŷ�
            if (CacheManager.RegInterMgr.UpdateYNSeeAndCharge(Patient.ID) == -1)
            {
                errInfo = CacheManager.ConManager.Err;
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
                bool isAccount = false;
                if (this.isAccountMode)
                {
                    #region �˻��ж�
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

            #region ���·���
            if (isUseNurseArray
                && currentPatientInfo.IsTriage
                && SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(empl.Dept.ID) != null)//�����Ƿ��Ѿ���������·���� by lijp 2012-06-30 && !this.Patient.IsSee)
            {
                if (this.currentRoom != null)
                {
                    iReturn = CacheManager.InterMgr.UpdateAssign(this.currentRoom.ID, this.Patient.ID, now, empl.ID);
                    if (iReturn < 0)
                    {
                        errInfo = "���·����־����\r\n" + CacheManager.InterMgr.Err;

                        return -1;
                    }
                }
            }
            #endregion

            #region ���¿���

            //��������ҽ�����»��ߵĿ���ҽ��
            if (!this.currentPatientInfo.IsSee || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.ID) || string.IsNullOrEmpty(this.currentPatientInfo.SeeDoct.Dept.ID))
            {
                iReturn = CacheManager.RegInterMgr.UpdateSeeDone(this.Patient.ID);
                if (iReturn < 0)
                {
                    errInfo = "���¿����־����";

                    return -1;
                }

                iReturn = CacheManager.RegInterMgr.UpdateDept(this.Patient.ID, empl.Dept.ID, empl.ID);
                if (iReturn < 0)
                {
                    errInfo = "���¿�����ҡ�ҽ������";

                    return -1;
                }
            }
            #endregion

            return 1;
        }

        #endregion

        #region ����
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
        //                errInfo = "���·����־����\r\n" + CacheManager.InterMgr.Err;

        //                return -1;

        //            }
        //        }
        //    }

        //    return 1;
        //}
        #endregion

        /// <summary>
        /// �Ƿ������������
        /// </summary>
        /// <returns></returns>
        /* public bool CheckCanAdd()
         {
             try
             {
                 #region ��黼��������Ϣ

                 #endregion

                 #region ����Ƿ��շѴ���
                 string strSQL = @"select count(*)
                                     from met_ord_recipedetail m
                                     where m.clinic_code='{0}'
                                     and m.status!='0'
                                     and m.see_no='{1}'";
                 strSQL = string.Format(strSQL, currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                 string rev = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSQL, "0");
                 if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                 {
                     ucOutPatientItemSelect1.MessageBoxShow(this, "�ô����Ѿ��շѣ�����������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     this.RefreshOrderState();
                     return false;
                 }
                 #endregion
             }
             catch (Exception ex)
             {
                 //���쳣�ˣ��������������
                 ucOutPatientItemSelect1.MessageBoxShow(ex.Message);
                 return true;
             }

             return true;
         }*/

        /// <summary>
        /// {BD313419-E718-4ABC-A9B9-5DF05709E309} ����δ�շѵ���Ŀ����ɾ��
        /// �Ƿ������������2
        /// </summary>
        /// <returns></returns>
        public bool CheckCanAdd()
        {
            try
            {

                #region ����Ƿ���δ�շ���Ŀ
                string strSQL = @"select m.sequence_no  
                                from met_ord_recipedetail m 
                                where m.clinic_code='{0}' 
                                and m.see_no='{1}' 
                                and m.status='0'";

                strSQL = string.Format(strSQL, currentPatientInfo.ID, currentPatientInfo.DoctorInfo.SeeNO.ToString());
                string rev = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSQL, "0");
                if (rev == null || rev == "")
                {
                    ucOutPatientItemSelect1.MessageBoxShow(this, "�ô����Ѿ��շѣ�����������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.RefreshOrderState();
                    return false;
                }
                #endregion
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
        /// ��¼�Һ���Ϣ
        /// </summary>
        /// <param name="regInfo"></param>
        /// <returns></returns>
        private int AddRegInfo(FS.HISFC.Models.Registration.Register regInfo)
        {
            //���ݹҺű���isfee��ǣ��ж��ǲ���ϵͳ���Һŵļ�¼
            FS.HISFC.Models.Registration.Register regTemp = CacheManager.RegInterMgr.GetByClinic(regInfo.ID);
            if (regTemp == null || string.IsNullOrEmpty(regTemp.ID))
            {
                //���Һ�
                if (CacheManager.RegInterMgr.Insert(regInfo) == -1)
                {
                    errInfo = "�����Һ���Ϣ����Һű����" + CacheManager.RegInterMgr.Err;
                    return -1;
                }

                //����������Ϣ
                if (CacheManager.OutOrderMgr.UpdateHealthInfo(regInfo.Height, regInfo.Weight, regInfo.SBP, regInfo.DBP, regInfo.ID, regInfo.Temperature, regInfo.BloodGlu) == -1)
                {
                    errInfo = "���»���������Ϣ����" + CacheManager.OutOrderMgr.Err;
                    return -1;
                }
            }
            return 1;
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
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
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
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҩҽ������";
                ucHerbal.SetFocus();

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucHerbal);

                AddNewHerbalOrder();
                //}
            }
            return 1;
        }

        /// <summary>
        /// ˢ�������б�
        /// </summary>
        void RefreshGroup()
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
            FS.HISFC.Models.Order.OutPatient.Order mnuSelectedOrder = null;
            FarPoint.Win.Spread.Model.CellRange c = neuSpread1.GetCellFromPixel(0, 0, e.X, e.Y);

            #region ����˵�

            //�������ѡ��ͬ����Ŀ
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

            #region �Ҽ��˵�
            if (this.bIsShowPopMenu && e.Button == MouseButtons.Right)
            {
                this.contextMenu1.Items.Clear();
                if (c.Row >= 0)
                {
                    //�ȼ�¼Ŀǰ�Ĺ�ѡ״̬������ӽ���
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
                        #region ճ��ҽ��
                        //if (FS.HISFC.Components.Order.Classes.HistoryOrderClipboard.IsHaveCopyData)
                        //{
                        ToolStripMenuItem mnuPasteOrder = new ToolStripMenuItem("ճ��ҽ��");
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
                    #region Ժע����
                    if (mnuSelectedOrder.Item.ItemType == EnumItemType.Drug &&
                      (mnuSelectedOrder.Status == 0 || mnuSelectedOrder.Status == 4) &&
                      mnuSelectedOrder.InjectCount == 0 &&
                        //Classes.Function.hsUsageAndSub.Contains(mnuSelectedOrder.Usage.ID)
                        Classes.Function.CheckIsInjectUsage(mnuSelectedOrder.Usage.ID)
                        )
                    {
                        ToolStripMenuItem mnuInjectNum = new ToolStripMenuItem();//Ժע����
                        mnuInjectNum.Click += new EventHandler(mnumnuInjectNum_Click);

                        mnuInjectNum.Text = "���Ժע����[��ݼ�:F5]";
                        this.contextMenu1.Items.Add(mnuInjectNum);
                    }

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
                    if (mnuSelectedOrder.Status == 0
                        && mnuSelectedOrder.Item.Price == 0)
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
                            ucOutPatientItemSelect1.MessageBoxShow(IReasonableMedicine.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    if (node.Tag != null && node.Tag.ToString() == "������")
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
                                                if (secondNode.Tag != null && secondNode.Tag.ToString() == "������")
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

                    #region �޸���չ��Ϣ
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
                    #region ����ҽ��
                    ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem("����ҽ��");
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

        #region ����ҽ�����շѺ�ҽ�����������ϣ��������������ٴ�

        /// <summary>
        /// ����ҽ��
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

            DialogResult r = ucOutPatientItemSelect1.MessageBoxShow("�Ƿ�ȷ��Ҫ���ϸ���ҽ��,�˲������ܳ�����", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

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
                        ucOutPatientItemSelect1.MessageBoxShow("����ҽ��" + temp.Item.Name + "ʧ��");
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
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            ucOutPatientItemSelect1.MessageBoxShow(errText);
                            return;
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
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
        /// ��䴦������
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int FillNewOrder(ref FS.HISFC.Models.Order.OutPatient.Order order)
        {
            order.Patient.Pact = this.currentPatientInfo.Pact;
            order.Patient.Birthday = this.currentPatientInfo.Birthday;

            //�������Һ�ִ�п�����ͬ������Ϊ�Ǳ�����ִ����Ŀ��ִ�п�����ȡ
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
                ucOutPatientItemSelect1.MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ͬ���渴��
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
        /// ������Ŀѡ������
        /// </summary>
        public void AddGroupOrder(ArrayList alOrders)
        {
            ArrayList alHerbal = new ArrayList(); //��ҩ

            ArrayList alAddOrder = new ArrayList();
            FS.HISFC.BizLogic.Manager.UndrugztManager ztManager = new FS.HISFC.BizLogic.Manager.UndrugztManager();

            frmChoseSublItem frmChose = new frmChoseSublItem();
            for (int i = 0; i < alOrders.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = alOrders[i] as FS.HISFC.Models.Order.OutPatient.Order;

                #region �ظ�ҽ����ʾ
                bool saveflag = true;
                if (isShowSameOrder)
                {
                    #region ��ʾ���ξ����ѿ�������ͬ��Ŀ��ҽ��
                    if (this.SameOrderList != null && this.SameOrderList.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in this.SameOrderList)
                        {
                            if (orderTemp.Item.ID == order.Item.ID)
                            {
                                if (MessageBox.Show("��Ŀ��" + orderTemp.Item.Name + "���ڡ�" + orderTemp.MOTime.ToString() + "���ѿ���,�Ƿ����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                {
                                    saveflag = false;
                                }

                                break;
                            }
                        }
                    }

                    #endregion

                    #region ��ʾ���������Ŀ�Ƿ�������շѵ�δ������ļ�¼���������
                    if (this.LastOrderList != null && this.LastOrderList.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in this.LastOrderList)
                        {
                            //����Լ�����Ŀ�Լ�������Ŀ
                            if (orderTemp.Item.SysClass.ID.ToString() == "UL")
                            {

                                if (orderTemp.Item.ID == order.Item.ID && orderTemp.User01 != "�ѳ�����")
                                {
                                    if (MessageBox.Show("��Ŀ��" + orderTemp.Item.Name + "���ڡ�" + orderTemp.MOTime.ToString() + "���ѿ�����Ϊ" + orderTemp.User01 + "״̬ ,�Ƿ����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                    {
                                        saveflag = false;
                                    }

                                    break;
                                }

                            }
                            //������鲻���ڡ��ѱ��桱״̬  
                            else if ((orderTemp.Item.SysClass.ID.ToString() == "UC" && orderTemp.ExeDept.ID == "6003"))
                            {
                                if (orderTemp.Item.ID == order.Item.ID && orderTemp.User01 == "δִ��")
                                {
                                    if (MessageBox.Show("��Ŀ��" + orderTemp.Item.Name + "���ڡ�" + orderTemp.MOTime.ToString() + "���ѿ�������Ϊ�ѹ���δִ��״̬ ,�Ƿ����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
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

                if (order.Item.SysClass.ID.ToString() == "PCC") //��ҩ
                {
                    alHerbal.Add(order);
                }
                else
                {
                    if (order.Item.ItemType == EnumItemType.UnDrug)
                    {
                        #region ��������
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
                                if (undrug.ValidState == "��Ч")
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
                                orderTmp.Sample.Name = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(((FS.HISFC.Models.Order.OutPatient.Order)alOrders[0]).Item.ID).CheckBody;//���»�ȡ������Ŀ��������ֵ������Ŀ
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
                                if (undrug.ValidState == "��Ч")
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
        /// ����ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuUp_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex <= 0)
                return;

            FS.HISFC.Models.Order.OutPatient.Order upOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex - 1, this.neuSpread1.ActiveSheetIndex).Clone();
            FS.HISFC.Models.Order.OutPatient.Order downOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex).Clone();

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

            FS.HISFC.Models.Order.OutPatient.Order upOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex, this.neuSpread1.ActiveSheetIndex).Clone();
            FS.HISFC.Models.Order.OutPatient.Order downOrder = this.GetObjectFromFarPoint(this.neuSpread1.ActiveSheet.ActiveRowIndex + 1, this.neuSpread1.ActiveSheetIndex).Clone();

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
            FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Order.OutPatient.Order;
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
            order.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(frm.ModuleName);
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
            FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.OutPatient.Order;

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
            //if (PACSApplyInterface == null)
            //{
            //    if (InitPACSApplyInterface() < 0)
            //    {
            //        ucOutPatientItemSelect1.MessageBoxShow("��ʼ���������뵥�ӿ�ʱ����");
            //        return;
            //    }
            //}
            //FS.HISFC.Models.Order.OutPatient.Order order = this.neuSpread1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.OutPatient.Order;

            //if (order.ApplyNo == null || order.ApplyNo == "")
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("��ҽ����δ���棬���ȱ��棡");
            //    return;
            //}

            //if (PACSApplyInterface.UpdateApply(order.ApplyNo) < 0)
            //{
            //    ucOutPatientItemSelect1.MessageBoxShow("�޸��ش�������뵥ʱ����");
            //    return;
            //}
        }

        /// <summary>
        /// ͨ����ʷҽ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyOrder_Click(object sender, EventArgs e)
        {
            this.CopyOrder();
        }

        /// <summary>
        /// �޸�ҽ����������ҩ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuEditIndications_Click(object sender, EventArgs e)
        {
            int i = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            if (i < 0 || this.neuSpread1.ActiveSheet.RowCount == 0)
            {
                MessageBox.Show("����ѡ��һ��ҽ����");
                return;
            }
            FS.HISFC.Models.Order.OutPatient.Order order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.ActiveSheet.Rows[i].Tag;

            if (order != null)
            {
                // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                if (string.IsNullOrEmpty(order.ID))//���ﲻ��Ϊ�գ�������Ĳ��ˣ�����
                {
                    MessageBox.Show("���ȱ���ҽ����", "��ʾ");
                    return;
                }
                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(this.Patient.ID, order.ID);
                FS.FrameWork.Models.NeuObject indicationsObj = indicationsHelper.GetObjectFromID(GetItemUserCode(order.Item));
                if (indicationsObj == null)
                {
                    MessageBox.Show("��ҩƷ��������ҩ���޷�ѡ��", "��ʾ");
                    return;
                }
                if (MessageBox.Show("ҩƷ��" + order.Item.Name + "���������Ƽ�ҩƷ��\r\n\r\n����ҩƷ˵������" + indicationsObj.Name + "��\r\n\r\n��ȷ��ҽ�������趨������(��)���Է�(��)?\r\n", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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

        /// <summary>
        /// �˴�ֻ�������б�����¼�ѡ��
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

        #region ��ʼ��������

        /// <summary>
        /// ������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ��������ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
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

        /// <summary>
        /// ��������ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.Add(false);
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
        #endregion

        #region �¼ӵĺ���

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

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���߱�ǩ��Ϣ,���Ժ�!");
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
        /// ������Ŀ������Ƿ�ɼ�
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetInputItemVisible(bool isVisible)
        {
            this.ucOutPatientItemSelect1.SetInputControlVisible(isVisible);
        }
        #endregion

        #region ������ӡ

        /// <summary>
        /// ������ӡ
        /// </summary>
        /// <param name="recipeNO"></param>
        public void PrintRecipe()
        {
            if (this.EditGroup)
            {
                ucOutPatientItemSelect1.MessageBoxShow("�����ڱ༭���ף���ʱ��֧�ִ�ӡ������");
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
                CacheManager.AccountMgr.Err = "������ӡ�ӿ�δʵ�֣�";
                CacheManager.AccountMgr.WriteErr();
                return;
                //ucOutPatientItemSelect1.MessageBoxShow("�ӿ�δʵ��");
            }
            else
            {
                if (alRecipe.Count > 0 || caseHistory != null)
                {
                    if (ucOutPatientItemSelect1.MessageBoxShow("�Ƿ��ӡ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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

        #region ԤԼ��Ժ

        public void PrePayIn()// {6BF1F99D-7307-4d05-B747-274D24174895}
        {
            if (this.currentPatientInfo == null || string.IsNullOrEmpty(this.currentPatientInfo.PID.CardNO))
            {
                MessageBox.Show("û��ѡ�л��ߣ���ѡ�л��ߣ�");
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
        /// ���ҩƷ��������
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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory GetCaseInfo()
        {
            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;
            caseHistory = CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.currentPatientInfo.ID);

            return caseHistory;
        }
        #endregion

        #region ��ӡ���е���

        /// <summary>
        /// ��ӡ���е��� 
        /// </summary>        
        public void PrintAllBill(string type, bool IsPreview)
        {
            if (this.EditGroup)
            {
                ucOutPatientItemSelect1.MessageBoxShow("�����ڱ༭���ף���ʱ��֧�ִ�ӡ������");
                return;
            }
            if (this.Patient == null || string.IsNullOrEmpty(this.Patient.ID))
            {
                ucOutPatientItemSelect1.MessageBoxShow("��ѡ���ߣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FS.HISFC.Models.Order.OutPatient.Order order;
            List<FS.HISFC.Models.Order.OutPatient.Order> alOrder = new List<FS.HISFC.Models.Order.OutPatient.Order>(); //����ҽ��

            List<FS.HISFC.Models.Order.OutPatient.Order> alSelectOrder = new List<FS.HISFC.Models.Order.OutPatient.Order>(); //����ҽ��

            FS.HISFC.Models.Registration.Register patient;

            for (int i = 0; i < this.neuSpread1.Sheets[0].Rows.Count; i++)
            {
                order = ((FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag).Clone();
                order.SeeNO = this.currentPatientInfo.DoctorInfo.SeeNO.ToString();
                // ������Ŀ��Ų�ѯ���뵥����{D793A341-AD35-4685-8817-5614217969AD} 2014-12-16 by lixuelong
                order.Item.Extend1 = CacheManager.OutOrderMgr.QueryApplyTypeByItemCode(order.Item.ID).ID;
                order.Item.Extend2 = CacheManager.OutOrderMgr.QueryApplyTypeByItemCode(order.Item.ID).Name;
                if (this.neuSpread1.Sheets[0].IsSelected(i, 0))
                {
                    alSelectOrder.Add(order);
                }
                alOrder.Add(order);
            }

            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = this.GetCaseInfo();

            #region ���ýӿ�ʵ�ִ�ӡ
            if (IOutPatientPrint != null && (alOrder.Count > 0 || caseHistory != null))
            {
                patient = this.Patient.Clone();
                if (IOutPatientPrint.OnOutPatientPrint(patient, this.GetReciptDept(), this.GetReciptDoct(), alOrder, alSelectOrder, false, IsPreview, type, false) != 1)
                {
                    ucOutPatientItemSelect1.MessageBoxShow(IOutPatientPrint.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        #endregion

        #region ������ҩ

        /// <summary>
        /// ��ʼ��IReasonableMedicin
        /// </summary>
        private void InitReasonableMedicine()
        {
            //�˴��Ƿ����ӿ��Ʋ������Ƿ����ú�����ҩ...
            if (this.IReasonableMedicine == null)
            {
                this.IReasonableMedicine = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine)) as FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine;
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
        /// ����������ҩ
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
        /// ˫���鿴������ҩҪ����ʾ
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

                    #region ҩƷ��ѯ
                    try
                    {
                        //ò������ֻ�����½ǵ�����λ�����
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
        /// ������ʾҪ����ʾ�ͼ�ʱ�Ծ�ʾ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                #region �������ú�����ҩ��ò��û����

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

                            //�����������ʾҪ����ʾ
                            if (e.Column == GetColumnIndexFromName("ҽ������") && this.enabledPass)
                            {
                                //������Ʋ�����ʾҪ����ʾ���ڿ���ʱ��˫��ʱ���Կ���Ҫ����ʾ

                                ////ò������ֻ�����½ǵ�����λ�����
                                //if (this.IReasonableMedicine.PassShowSingleDrugInfo(info,
                                //    new Point(MousePosition.X, MousePosition.Y - 60),
                                //    new Point(MousePosition.X + 100, MousePosition.Y + 15), false) == -1)
                                //{
                                //    ucOutPatientItemSelect1.MessageBoxShow(IReasonableMedicine.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //}
                            }
                            //���������ҩ��ʾ������ʾ��ʱ�Ծ�ʾ��������
                            else if (e.Column == GetColumnIndexFromName("��"))
                            {
                                if (this.IReasonableMedicine.PassShowWarnDrug(info) == -1)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow(IReasonableMedicine.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ucOutPatientItemSelect1.MessageBoxShow(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// �������ҩϵͳ���͵�ǰҽ���������
        /// </summary>
        /// <param name="isSave">�Ƿ񱣴�ʱ����</param>
        public int PassCheckOrder(bool isSave)
        {
            ArrayList alOrder = new ArrayList();

            //1��ʾ��ǰ���濪���Ĵ���
            //2��ʾ��ǰ�Һż�¼�Ĵ���
            //3��ʾ��ǰ������Ч���ڵĴ���
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

                    //֮ǰ��������������ҩ�ģ�����û����
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
                    ucOutPatientItemSelect1.MessageBoxShow("��ѯ�������ﴦ������\r\n" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ucOutPatientItemSelect1.MessageBoxShow("��ѯ�������ﴦ������\r\n" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ucOutPatientItemSelect1.MessageBoxShow("������ҩ������" + this.IReasonableMedicine.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                //��Ϊѡ��ͨ��
                else if (rev == 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// �˳�������ҩ
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
        /// ����ҩƷϵͳ��ѯ
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
                FS.HISFC.Models.Order.OutPatient.Order order = null;

                if (!isRegFeeOnly)
                {
                    #region ������
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
                            //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
                            if (newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            //ע�ͺ���Ա������շѵĴ���
                            /* if (newOrder.Status != 0 || newOrder.IsHaveCharged)//��鲢��ҽ��״̬
                             {
                                 {7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                                 FS.FrameWork.Management.PublicTrans.RollBack();
                                 ucOutPatientItemSelect1.MessageBoxShow("���㸽�Ĵ���\r\n[" + order.Item.Name + "]�����Ѿ��շ�,���˳������������½���!\r\n" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return -1;
                             }*/


                            alOrder.Add(order);
                            if (order != null && order.IsSubtbl)
                            {
                                if (order.Memo == "�Һŷ�")
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
                                    ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + errText, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        ucOutPatientItemSelect1.MessageBoxShow("������ʧ�ܣ�" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }

                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    //FS.FrameWork.Management.PublicTrans.Commit();

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
                            order = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1.Sheets[0].Rows[i].Tag;
                            if (order == null)
                            {
                                continue;
                            }

                            FS.HISFC.Models.Order.OutPatient.Order newOrder = CacheManager.OutOrderMgr.QueryOneOrder(this.Patient.ID, order.ID);
                            //���û��ȡ�����������Ѿ���������ˮ��ȴ�����������������ݿ��������
                            if (newOrder == null || newOrder.Status == 0)
                            {
                                newOrder = order;
                            }

                            if (newOrder.Status != 0 || newOrder.IsHaveCharged)//��鲢��ҽ��״̬
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                ucOutPatientItemSelect1.MessageBoxShow("���㸽�Ĵ���\r\n[" + order.Item.Name + "]�����Ѿ��շ�,���˳������������½���!\r\n" + CacheManager.OutOrderMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            if (MessageBox.Show("�Ƿ�Ϊ����ţ�", "�Һŷ��Զ�������ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                ArrayList regConstList = CacheManager.GetConList("RegFeeItem");
                                if (regConstList == null || regConstList.Count == 0)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ�����ŹҺŷ���Ŀʧ�ܣ�" + CacheManager.ConManager.Err);
                                    return -1;
                                }

                                alSupplyFee.Clear();

                                FS.HISFC.Models.Fee.Item.Undrug regItem = new FS.HISFC.Models.Fee.Item.Undrug();
                                string regItemCode = ((FS.FrameWork.Models.NeuObject)regConstList[0]).ID.Trim();

                                if (this.CheckItem(regItemCode, ref errInfo, ref regItem) == -1)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("��ȡ�����ŹҺŷ���Ŀʧ��" + errInfo);
                                    return -1;
                                }

                                string ErrInfo = string.Empty;
                                regFeeItem = SetSupplyFeeItemListByItem(regItem, ref ErrInfo);

                                alSupplyFee.Add(regFeeItem);
                            }
                            else
                            {
                                //{B923E235-2301-4a94-906C-1207AC04B4D6}
                                //��������ר�ҳ���Ļ����,����memo�ֶ�
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
                                        ucOutPatientItemSelect1.MessageBoxShow("��ȡר�һ������Ŀʧ��" + errInfo);
                                        return -1;
                                    }

                                    string ErrInfo = string.Empty;
                                    regFeeItem = SetSupplyFeeItemListByItem(regItem, ref ErrInfo);

                                    alSupplyFee.Add(regFeeItem);
                                }
                            }

                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemObj in alSupplyFee)
                            {
                                //�������ҽ������
                                newOrder = new FS.HISFC.Models.Order.OutPatient.Order();//��������ҽ��

                                item = CacheManager.FeeIntegrate.GetItem(itemObj.Item.ID);//���������Ŀ��Ϣ
                                if (item == null)
                                {
                                    ucOutPatientItemSelect1.MessageBoxShow("���㸽��ʱ��������Ŀʧ�ܣ�" + CacheManager.FeeIntegrate.Err);
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

                                    newOrder.Combo = orderTemp.Combo.Clone();//��Ϻ�
                                    newOrder.ReciptSequence = orderTemp.ReciptSequence;

                                    newOrder.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;

                                    newOrder.DoseUnit = "";

                                    newOrder.IsEmergency = orderTemp.IsEmergency;
                                    newOrder.IsSubtbl = true;
                                    newOrder.Usage = new FS.FrameWork.Models.NeuObject();
                                    newOrder.SequenceNO = -1;
                                    if (newOrder.ExeDept.ID == "")//ִ�п���Ĭ��
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
                                    ucOutPatientItemSelect1.MessageBoxShow("������Ŀ����ʱ����������ҽ����������" + ex.Message);
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
                if (isAllowChangePactInfo)
                {
                    if (this.currentPatientInfo.IsSee)
                    {
                        ArrayList alFee = CacheManager.FeeIntegrate.QueryAllFeeItemListsByClinicNO(this.currentPatientInfo.ID, "1", "ALL", "ALL");
                        if (alFee != null && alFee.Count > 0)
                        {
                            this.cmbPact.Enabled = false;
                            this.cmbPact.Tag = currentPatientInfo.Pact.ID;
                            ucOutPatientItemSelect1.MessageBoxShow("�����Ѿ����շ���Ϣ�������޸ĺ�ͬ��λ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                #region �ӿ��жϺ�ͬ��λ����

                if (this.iCheckPactInfo == null)
                {
                    this.iCheckPactInfo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPactInfo)) as FS.HISFC.BizProcess.Interface.Common.ICheckPactInfo;
                }
                if (this.iCheckPactInfo == null)
                {
                    //if (!string.IsNullOrEmpty(FS.FrameWork.WinForms.Classes.UtilInterface.Err))
                    //{
                    //    ucOutPatientItemSelect1.MessageBoxShow("��ýӿ�ICheckPactInfo����,�����޷��жϺ�ͬ��λ����Ч�ԣ�\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                else
                {
                    iCheckPactInfo.PatientInfo = patientInfo;
                    if (iCheckPactInfo.CheckIsValid() == -1)
                    {
                        this.cmbPact.Tag = pactCode;
                        this.cmbPact.Text = pactTemp.Name;
                        ucOutPatientItemSelect1.MessageBoxShow(iCheckPactInfo.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ucOutPatientItemSelect1.MessageBoxShow("���º�ͬ��λ��Ϣ����" + CacheManager.RegInterMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region IInterfaceContainer��Ա
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[7];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.IRecipePrint);
                t[1] = typeof(FS.HISFC.BizProcess.Interface.Common.ICheckPrint);//������뵥
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
    /// ���������
    /// </summary>
    class RowCompare : IComparer
    {
        #region IComparable ��Ա

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