using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using FS.FrameWork.Management;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;


namespace FS.SOC.HISFC.Components.Register.SDFY
{
    /// <summary>
    /// ����Һ�
    /// </summary>
    public partial class ucRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer, FS.HISFC.BizProcess.Interface.FeeInterface.ISIReadCard
    {
        public ucRegister()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucRegister_Load);
            this.cmbRegLevel.SelectedIndexChanged += new EventHandler(cmbRegLevel_SelectedIndexChanged);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDoctor.SelectedIndexChanged += new EventHandler(cmbDoctor_SelectedIndexChanged);

            this.cmbCardType.KeyDown += new KeyEventHandler(cmbCardType_KeyDown);
            this.dtBookingDate.ValueChanged += new EventHandler(dtBookingDate_ValueChanged);
            this.dtBookingDate.KeyDown += new KeyEventHandler(dtBookingDate_KeyDown);
            this.dtBegin.ValueChanged += new EventHandler(dtBegin_ValueChanged);
            this.dtBegin.KeyDown += new KeyEventHandler(dtBegin_KeyDown);
            this.dtEnd.ValueChanged += new EventHandler(dtEnd_ValueChanged);
            this.dtEnd.KeyDown += new KeyEventHandler(dtEnd_KeyDown);
            this.txtOrder.KeyDown += new KeyEventHandler(txtOrder_KeyDown);
            this.cmbUnit.SelectedIndexChanged += new EventHandler(cmbUnit_SelectedIndexChanged);
            this.txtOrder.TextChanged += new EventHandler(txtOrder_TextChanged);
            this.llPd.Click += new EventHandler(llPd_Click);
            this.txtRecipeNo.KeyDown += new KeyEventHandler(txtRecipeNo_KeyDown);
            this.txtRecipeNo.Validating += new CancelEventHandler(txtRecipeNo_Validating);
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellClick);
            this.cmbDoctor.TextChanged += new EventHandler(cmbDoctor_TextChanged);
            this.txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPhone_KeyDown);
            this.txtPhone.Enter += new System.EventHandler(this.txtCardNo_Enter);
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            this.cmbDept.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDept_KeyDown);
            this.cmbDept.TextChanged += new System.EventHandler(this.cmbDept_TextChanged);
            this.cmbDept.Enter += new System.EventHandler(this.cmbDept_Enter);
            this.cmbDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDoctor_KeyDown);
            this.cmbDoctor.Enter += new System.EventHandler(this.cmbDoctor_Enter);
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            //this.txtAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAge_KeyDown);
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
            this.txtAddress.Enter += new System.EventHandler(this.txtAddress_Enter);
            this.txtMcardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMcardNo_KeyDown);
            this.txtMcardNo.Enter += new System.EventHandler(this.txtCardNo_Enter);
            this.cmbPayKind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPayKind_KeyDown);
            this.cmbPayKind.Enter += new System.EventHandler(this.cmbPayKind_Enter);
            this.cmbPayKind.SelectedIndexChanged += new EventHandler(cmbPayKind_SelectedIndexChanged);//{D845A003-62E2-44da-8612-81C4F6A1037E}
            this.cmbSex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSex_KeyDown);
            this.cmbSex.Enter += new System.EventHandler(this.txtCardNo_Enter);
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.txtCardNo.Enter += new System.EventHandler(this.txtCardNo_Enter);
            this.cmbRegLevel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRegLevel_KeyDown);
            this.cmbRegLevel.Enter += new System.EventHandler(this.cmbRegLevel_Enter);
            this.dtBirthday.KeyDown += new KeyEventHandler(dtBirthday_KeyDown);

            this.ucAssign.getAssinID += new ucAssignNo.GetAssignID(ucAssign_getAssinID);
        }

        #region ����

        #region ������
        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �Һ�ԱȨ����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Permission permissMgr = new FS.HISFC.BizLogic.Registration.Permission();
        /// <summary>
        /// ����������
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// �Ű������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema SchemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// ���߹�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT patientMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// ��ͬ��λ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// �Һż��������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLevel RegLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
        /// <summary>
        /// ԤԼ������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Booking bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();
        /// <summary>
        /// �Һŷѹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
        /// <summary>
        /// ��ʿ������Ϣ
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Assign assMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        /// <summary>
        /// �Һ��ۺϹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regInteMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// ҽ���ӿ���
        /// </summary>
        //private MedicareInterface.Class.Clinic SIMgr = new MedicareInterface.Class.Clinic();
        //private FS.HISFC.BizLogic.Fee.Interface InterfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
        ////
        #endregion

        /// <summary>
        /// �ҺŽ���Ĭ�ϵ��������뷨
        /// </summary>
        private InputLanguage CHInput = null;
        //// <summary>
        //// �Һ�Ʊ�Ƿ񰴷�Ʊ����
        //// </summary>
        //private bool IsGetInvoice = false;
        /// <summary>
        /// �Һ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Registration.Register regObj;
        /// <summary>
        /// ��������б�
        /// </summary>
        private ArrayList alDept = new ArrayList();
        /// <summary>
        /// ����Һ�Ա�ҺŵĿ���
        /// </summary>
        private ArrayList alAllowedDept = new ArrayList();
        /// <summary>
        /// ҽ���б�
        /// </summary>
        private ArrayList alDoct = new ArrayList();
        /// <summary>
        /// ���
        /// </summary>
        private ArrayList alNoon = new ArrayList();
        /// <summary>
        /// �Ƿ񴥷�SelectedIndexChanged�¼�
        /// </summary>
        private bool IsTriggerSelectedIndexChanged = true;
        private bool isBirthdayEnd = true;

        /// <summary>
        /// �Ƿ���ʾ�˻���ҽ���Ȼ�����Ϣ�� {54603DD0-3484-4dba-B88A-B89F2F59EA40}
        /// </summary>
        private bool isShowSIBalanceCost = true;

        #region ����
        /// <summary>
        /// Ĭ����ʾ�ĺ�ͬ��λ����
        /// </summary>
        private string DefaultPactID = "";
        /// <summary>
        /// ���ѻ��������չҺ��޶�
        /// </summary>
        private int DayRegNumOfPub = 10;
        /// <summary>
        /// ����Ƿ����
        /// </summary>
        private bool IsPubDiagFee = false;
        /// <summary>
        /// ר�Һ��Ƿ���ѡ�����
        /// </summary>
        private bool IsSelectDeptFirst = false;
        /// <summary>
        /// �Һ��Ƿ�¼������
        /// </summary>
        private bool IsInputName = true;
        //{920686B9-AD51-496e-9240-5A6DA098404E}
        /// <summary>
        /// ҽ�������������б��Ƿ���ʾȫԺ��ҽ�������ң�������˭�ܿ����ף�˭�񾭲�
        /// </summary>
        //private bool ComboxIsListAll = true;
        /// <summary>
        /// �Һſ�����ʾ����
        /// </summary>
        private int DisplayDeptColumnCnt = 1;
        /// <summary>
        /// �Һ�ҽ����ʾ����
        /// </summary>
        private int DisplayDoctColumnCnt = 1;
        /// <summary>
        /// �Һ��Ƿ��������Ű��޶�
        /// </summary>
        private bool IsAllowOverrun = true;
        /// <summary>
        /// 2�����ŶԲ���Ա������1�ɲ���Ա�Լ�¼�봦����
        /// </summary>
        private int GetRecipeType = 1;

        private int GetInvoiceType = 1;
        /// <summary>
        /// �س��Ƿ�����Ԥ����ˮ�Ŵ�
        /// </summary>
        private bool IsInputOrder = true;
        /// <summary>
        /// ����Ƿ�����ԤԼʱ��δ�
        /// </summary>
        private bool IsInputTime = true;
        /// <summary>
        /// ����ʱ�Ƿ���ʾ
        /// </summary>
        private bool IsPrompt = true;
        /// <summary>
        /// �Ƿ�ԤԼ����������ֳ���ǰ��
        /// </summary>
        private bool IsPreFirst = false;

        /// <summary>
        /// �Ƿ���ȡ�յ���
        /// </summary>
        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        //private bool IsKTF = true;

        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        /// <summary>
        /// �������ѡ�����0���յ���1��������2��������
        /// </summary>
        private string otherFeeType = string.Empty;

        /// <summary>
        /// ר�Һ��Ƿ����ֽ��ڼ���
        /// </summary>
        private bool IsDivLevel = false;
        /// <summary>
        /// ���ź��Ƿ���Ϊ�ǼӺ�
        /// </summary>
        private bool MultIsAppend = true;
        /// <summary>
        /// �����б�
        /// </summary>
        private ArrayList alProfessor = new ArrayList();

        /// <summary>
        /// �����źŷ�ʽ 1ҽ�� 2���� 3����
        /// </summary>
        string strGetSeeNoType = string.Empty;

        #endregion

        /// <summary>
        /// ѡ��ԤԼʱ���
        /// </summary>
        private FS.HISFC.Components.Registration.ucChooseBookingDate ucChooseDate;

        /// <summary>
        /// ҽ���ӿڴ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        private bool isReadCard = false;
        /// <summary>
        /// �Һ���Ϣʵ�壺ҽ�����ʹ��
        /// </summary>
        //FS.HISFC.Models.Registration.Register myYBregObj = new FS.HISFC.Models.Registration.Register ();

        /// <summary>
        /// �Ƿ񵯳����㴰��{F0661633-4754-4758-B683-CB0DC983922B}
        /// </summary>
        private bool isShowChangeCostForm = false;

        private Function interMgr = new Function();

        #region ��Ʊ
        /// <summary>
        /// ��ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.IRegPrint IRegPrint = null;
        #endregion

        private DataSet dsItems;
        private DataView dvDepts;
        private DataView dvDocts;

        private ArrayList al = new ArrayList();
        /// <summary>
        /// ��ʾ���Ƿ�ʹ���ʻ�֧��
        /// </summary>
        private bool isAccountMessage = true;

        #region ҽ���������Կ��ƺ��޸����ҽ���б�Ϳ����б�{920686B9-AD51-496e-9240-5A6DA098404E}
        /// <summary>
        /// �Ƿ��������ҽ��
        /// </summary>
        private bool isAddAllDoct = false;

        /// <summary>
        /// �Ƿ��г����п���
        /// </summary>
        private bool isAddAllDept = false;

        /// <summary>
        /// ��ͨ��ʱ��ҽ���ؼ��Ƿ��ý���
        /// </summary>
        private bool isSetDoctFocusForCommon = false;

        /// <summary>
        /// ����ʱ����{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter iProcessRegiter = null;

        #endregion

        /// <summary>
        /// adt�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IHE.IADT adt = null;

        #region �˻�����
        ///// <summary>
        ///// �˻��Ƿ��ն˿۷�
        ///// </summary>
        //bool isAccountTerimalFee = false;
        #endregion

        ucShowPatients ucShow = new ucShowPatients();

        ucAssignNo ucAssign = new ucAssignNo();

        string assignNO = string.Empty;

        /// <summary>
        /// �໼�ߵ�������
        /// </summary>
        protected Form fPopWin = new Form();

        /// <summary>
        /// ��ʱ���滼�߹Һ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Registration.Register regObjTemp = null;

        /// <summary>
        /// �������뷽ʽ
        /// </summary>
        private EnumBirthdayInputType enumBirthdayInputType = EnumBirthdayInputType.Age;

        /// <summary>
        /// �����ӡ���˿ں�
        /// </summary>
        int printer_port = 1;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ����")]
        public bool IsShowEncrpt
        {
            get
            {
                return this.chbEncrpt.Visible;
            }
            set
            {
                this.chbEncrpt.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ�ֱ�Ӵ�ӡ
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// �Ƿ��Զ���ӡ{D623D221-1472-4dc9-B84C-F3E0F4D0C256}�޸�ע��
        /// </summary>
        [Category("�ؼ�����"), Description("������Ƿ��Զ���ӡ�Һŵ�"), DefaultValue(true)]
        public bool IsAutoPrint
        {
            get
            {
                return this.isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }
        /// <summary>
        /// ���������Ƿ��ý���{F0661633-4754-4758-B683-CB0DC983922B}
        /// </summary>
        //[Category("�ؼ�����"), Description("���տؼ��Ƿ��ý��� True:���տؼ�����ý��㣬����ؼ�������ý��㣻False:���տؼ�������ý��㣬����ؼ�����ý���"), DefaultValue(true)]
        [Obsolete("����ʹ�ã���EnumBirthdayInputType����")]
        public bool IsBirthdayEnd
        {
            get
            {
                return isBirthdayEnd;
            }
            set
            {
                isBirthdayEnd = value;
            }
        }

        [Category("�ؼ�����"), Description("��ʾ���Ƿ�ʹ���ʻ�֧��True:��ʾ,False:����ʾ��ȡ�ʻ�")]
        public bool IsAccountMessage
        {
            get
            {
                return isAccountMessage;
            }
            set
            {
                isAccountMessage = value;
            }
        }

        // {54603DD0-3484-4dba-B88A-B89F2F59EA40}
        [Category("�ؼ�����"), Description("��ʾ����ʾ�����˻���ҽ�������True:��ʾ,False:����ʾ")]
        public bool IsShowSIBalanceCost
        {
            get
            {
                return this.isShowSIBalanceCost;
            }
            set
            {
                this.isShowSIBalanceCost = value;
                this.lblSIBalanceTEXT.Visible = value;
                this.tbSIBalanceCost.Visible = value;
            }
        }
        #region ҽ���������Կ��ƺ��޸����ҽ���б�Ϳ����б�{920686B9-AD51-496e-9240-5A6DA098404E}
        /// <summary>
        /// �Һ�ҽ���Ƿ����ſ��ұ仯
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ����ȫԺҽ����True:���ȫԺҽ����ѡ�����ʱҽ���б����ű仯,False:�仯"), DefaultValue(true)]
        public bool IsAddAllDoct
        {
            get { return isAddAllDoct; }
            set { isAddAllDoct = value; }
        }

        /// <summary>
        /// �Һ�ҽ���Ƿ����ſ��ұ仯
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ����ȫԺ���ң�True:���,False:ֻ��ӹҺſ���"), DefaultValue(false)]
        public bool IsAddAllDept
        {
            get { return isAddAllDept; }
            set { isAddAllDept = value; }
        }


        /// <summary>
        /// ��ͨ��ʱ��ҽ���ؼ��Ƿ��ý���
        /// </summary>
        [Category("�ؼ�����"), Description("��ͨ��ʱ��ҽ���ؼ��Ƿ��ý��㣬True:���,False:�����"), DefaultValue(false)]
        public bool IsSetDoctFocusForCommon
        {
            get { return isSetDoctFocusForCommon; }
            set { isSetDoctFocusForCommon = value; }
        }

        #endregion

        /// <summary>
        /// �Ƿ񵯳����㴰��{F0661633-4754-4758-B683-CB0DC983922B}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ񵯳����㴰��"), DefaultValue(false)]
        public bool IsShowChangeCostForm
        {
            get { return isShowChangeCostForm; }
            set { isShowChangeCostForm = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ�����ſؼ� {63858620-21A6-4080-8520-E5B948C5EE13}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ�����ſؼ�"), DefaultValue(false)]
        public bool IsShowRecipeNO
        {
            set
            {
                this.label11.Visible = value;
                this.txtRecipeNo.Visible = value;
            }
            get
            {
                return this.txtRecipeNo.Visible && this.label11.Visible;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�����ſؼ� {63858620-21A6-4080-8520-E5B948C5EE13}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ���˿ؼ�"), DefaultValue(true)]
        public bool IsShowAcquaintance
        {
            set
            {
                this.chSpecialPerson.Visible = value;
            }
            get
            {
                return this.chSpecialPerson.Visible;
            }
        }

        #region �Ƿ����Ƶ绰��סַ������һ��
        private bool isLimit = false;
        [Category("�ؼ�����"), Description("�Ƿ����Ƶ绰��סַ������һ��")]
        public bool IsLimit
        {
            set
            {
                this.isLimit = value;
            }
            get
            {
                return this.isLimit;
            }
        }

        #endregion

        /// <summary>
        /// �������뷽ʽ
        /// </summary>
        [Category("�ؼ�����"), Description("�������뷽ʽ"), DefaultValue(EnumBirthdayInputType.Age)]
        public EnumBirthdayInputType EnumBirthdayInputType
        {
            get
            {
                return this.enumBirthdayInputType;
            }
            set
            {
                this.enumBirthdayInputType = value;
            }
        }

        /// <summary>
        /// �����ӡ���˿ں�
        /// </summary>
        [Category("�ؼ�����"), Description("�����ӡ���˿ں�")]
        public int Printer_port
        {
            get
            {
                return this.printer_port;
            }
            set
            {
                this.printer_port = value;
            }
        }

        #endregion

        #region ��ʼ��
        private void ucRegister_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            this.init();
            this.SetRegLevelDefault();
            this.clear();
            this.initInputMenu();
            this.readInputLanguage();
            this.ChangeRecipe();
            this.SetDefaultCardType();

            if (Screen.PrimaryScreen.Bounds.Height == 600)
            {
                this.panel5.Height = 29;
            }

            this.LoadPrint();

            this.FindForm().FormClosing += new FormClosingEventHandler(ucRegister_FormClosing);
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void init()
        {
            //FS.neuFC.Interface.Classes.Function.ShowWaitForm("") ;
            //Application.DoEvents() ;
            this.GetParameter();
            this.initDataSet();
            this.setStyle();
            this.initRegLevel();
            this.alDept = this.GetClinicDepts();
            if (this.alDept == null) this.alDept = new ArrayList();

            this.InitRegDept();
            this.InitDoct();
            this.initPact();
            this.InitBookingDate();
            this.InitNoon();
            this.cmbCountry.AddItems(this.conMgr.GetConstantList(FS.HISFC.Models.Base.EnumConstant.COUNTRY));
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            this.InitCardType();
            this.Retrieve();
            this.GetRecipeNo(regMgr.Operator.ID);

            //FS.neuFC.Interface.Classes.Function.HideWaitForm() ;

            this.cmbRegLevel.IsFlat = true;
            this.cmbDept.IsFlat = true;
            this.cmbDoctor.IsFlat = true;
            this.cmbPayKind.IsFlat = true;
            this.cmbSex.IsFlat = true;
            this.cmbUnit.IsFlat = true;
            this.cmbCardType.IsFlat = true;
            this.cmbPayKind.IsLike = false;//������ģ����ѯ
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            //Ϊ����������ʱ��ʾ
            if (this.otherFeeType == "1")
            {
                this.chbBookFee.Visible = true;
            }
            else
            {
                this.chbBookFee.Visible = false;
            }
            //{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
            this.InitInterface();
            ChangeInvoiceNOMessage();
            InitPopShowPatient();
            this.InitLedDisplay();
        }
        /// <summary>
        /// init DataSet
        /// </summary>
        private void initDataSet()
        {
            dsItems = new DataSet();
            dsItems.Tables.Add("Dept");
            dsItems.Tables.Add("Doct");

            dsItems.Tables["Dept"].Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("ID",System.Type.GetType("System.String")),
                    new DataColumn("Name",System.Type.GetType("System.String")),
                    new DataColumn("Spell_Code",System.Type.GetType("System.String")),
                    new DataColumn("Wb_code",System.Type.GetType("System.String")),
                    new DataColumn("Input_Code",System.Type.GetType("System.String")),
                    new DataColumn("RegLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Reged",System.Type.GetType("System.Decimal")),
                    new DataColumn("TelLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Teled",System.Type.GetType("System.Decimal")),
                    new DataColumn("BeginTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("EndTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("Noon",System.Type.GetType("System.String")),
                    new DataColumn("IsAppend",System.Type.GetType("System.Boolean"))
                });

            dsItems.Tables["Doct"].Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("ID",System.Type.GetType("System.String")),
                    new DataColumn("Name",System.Type.GetType("System.String")),
                    new DataColumn("Spell_Code",System.Type.GetType("System.String")),
                    new DataColumn("Wb_code",System.Type.GetType("System.String")),					
                    new DataColumn("RegLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Reged",System.Type.GetType("System.Decimal")),
                    new DataColumn("TelLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Teled",System.Type.GetType("System.Decimal")),
                    new DataColumn("SpeLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Sped",System.Type.GetType("System.Decimal")),
                    new DataColumn("BeginTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("EndTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("Noon",System.Type.GetType("System.String")),
                    new DataColumn("IsAppend",System.Type.GetType("System.Boolean")),
                    new DataColumn("Memo",System.Type.GetType("System.String")),
                    new DataColumn("IsProfessor",System.Type.GetType("System.Boolean")),
                    new DataColumn("user_code",System.Type.GetType("System.String")),
                    //����һ�б���������� ygch {8BF9E56B-828E-4264-9D2F-B0B74FB920B5}
                    new DataColumn("num",System.Type.GetType("System.String"))
                });

            dsItems.CaseSensitive = false;

            dvDepts = new DataView(dsItems.Tables["Dept"]);
            dvDocts = new DataView(dsItems.Tables["Doct"]);
        }
        /// <summary>
        /// ����farpoint�ĸ�ʽ
        /// </summary>
        private void setStyle()
        {
            FarPoint.Win.Spread.CellType.TextCellType txt = new FarPoint.Win.Spread.CellType.TextCellType();

            #region �Һż���
            //�������ùҺż�����ʾ����
            string colCount = this.ctlMgr.QueryControlerInfo("400001");
            //û��Ĭ����ʾһ��
            if (colCount == null || colCount == "-1" || colCount == "")
                colCount = "1";


            this.fpRegLevel.ColumnCount = int.Parse(colCount) * 2;
            int width = /*this.fpSpread1.Width*/500 * 2 / this.fpRegLevel.ColumnCount;
            //������
            for (int i = 0; i < this.fpRegLevel.ColumnCount; i++)
            {
                if (i % 2 == 0)
                {
                    this.fpRegLevel.ColumnHeader.Cells[0, i].Text = "����";
                    this.fpRegLevel.Columns[i].Width = width / 3;
                    this.fpRegLevel.Columns[i].BackColor = Color.Linen;
                    this.fpRegLevel.Columns[i].CellType = txt;
                }
                else
                {
                    this.fpRegLevel.ColumnHeader.Cells[0, i].Text = "�Һż�������";
                    this.fpRegLevel.Columns[i].Width = width * 2 / 3;
                }
            }

            this.fpRegLevel.GrayAreaBackColor = System.Drawing.SystemColors.Window;
            this.fpRegLevel.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpRegLevel.RowHeader.Visible = false;
            this.fpRegLevel.RowCount = 0;
            #endregion

            #region �������
            colCount = this.ctlMgr.QueryControlerInfo("400003");
            if (colCount == null || colCount == "-1" || colCount == "") colCount = "1";

            this.fpPayKind.ColumnCount = int.Parse(colCount) * 2;
            width = /*this.fpSpread1.Width*/500 * 2 / this.fpPayKind.ColumnCount;

            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtType.StringTrim = System.Drawing.StringTrimming.EllipsisCharacter;

            //������
            for (int i = 0; i < this.fpPayKind.ColumnCount; i++)
            {
                if (i % 2 == 0)
                {
                    this.fpPayKind.ColumnHeader.Cells[0, i].Text = "����";
                    this.fpPayKind.Columns[i].Width = width / 3;
                    this.fpPayKind.Columns[i].BackColor = Color.Linen;
                    this.fpPayKind.Columns[i].CellType = txt;
                }
                else
                {
                    this.fpPayKind.ColumnHeader.Cells[0, i].Text = "�������";
                    this.fpPayKind.Columns[i].Width = width * 2 / 3;
                    this.fpPayKind.Columns[i].CellType = txtType;
                }
            }

            this.fpPayKind.GrayAreaBackColor = SystemColors.Window;
            this.fpPayKind.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpPayKind.RowHeader.Visible = false;
            this.fpPayKind.RowCount = 0;
            #endregion

            #region ���߹Һ���Ϣ
            this.fpList.ColumnHeader.Cells[0, 0].Text = "������";
            this.fpList.Columns[0].Width = 100F;
            this.fpList.Columns[0].CellType = txt;
            this.fpList.ColumnHeader.Cells[0, 1].Text = "����";
            this.fpList.Columns[1].Width = 90F;
            this.fpList.ColumnHeader.Cells[0, 2].Text = "�������";
            this.fpList.Columns[2].Width = 90F;
            this.fpList.ColumnHeader.Cells[0, 3].Text = "�Һż���";
            this.fpList.Columns[3].Width = 80F;
            this.fpList.ColumnHeader.Cells[0, 4].Text = "�Һſ���";
            this.fpList.Columns[4].Width = 80F;
            this.fpList.ColumnHeader.Cells[0, 5].Text = "����ҽ��";
            this.fpList.Columns[5].Width = 78F;
            this.fpList.ColumnHeader.Cells[0, 6].Text = "���";
            this.fpList.Columns[6].Width = 40;
            this.fpList.ColumnHeader.Cells[0, 7].Text = "�Һŷ�(�Է��ܶ�)";
            this.fpList.Columns[7].Width = 120;
            this.fpList.ColumnHeader.Cells[0, 8].Text = "���������";
            this.fpList.Columns[8].Width = 80;
            this.fpList.Columns.Count = 9;

            this.fpList.GrayAreaBackColor = SystemColors.Window;
            this.fpList.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpList.RowCount = 0;
            #endregion

            //��ʼ����ʾ�Ű����
            this.SetDeptFpStyle(false);

            this.SetDoctFpStyle(false);
        }
        /// <summary>
        /// ���ÿ����б���ʾ�ĸ�ʽ
        /// </summary>
        /// <param name="IsDisplaySchema"></param>
        private void SetDeptFpStyle(bool IsDisplaySchema)
        {
            //��ʾר���Ű����,��ʾ���롢�������ơ����ʱ��Ρ��Һ��޶�ѹ�������ԤԼ�޶ԤԼ�ѹ�
            this.fpDept.Reset();
            this.fpDept.SheetName = "�Һſ���";

            FarPoint.Win.Spread.CellType.TextCellType txt = new FarPoint.Win.Spread.CellType.TextCellType();

            if (IsDisplaySchema)
            {
                this.fpDept.ColumnCount = 7;
                this.fpDept.ColumnHeader.Cells[0, 0].Text = "����";
                this.fpDept.ColumnHeader.Columns[0].Width = 45;
                this.fpDept.Columns[0].CellType = txt;
                this.fpDept.ColumnHeader.Cells[0, 1].Text = "��������";
                this.fpDept.ColumnHeader.Columns[1].Width = 95;
                this.fpDept.ColumnHeader.Cells[0, 2].Text = "����ʱ��";
                this.fpDept.ColumnHeader.Columns[2].Width = 120;
                this.fpDept.ColumnHeader.Cells[0, 3].Text = "�Һ��޶�";
                this.fpDept.Columns[3].ForeColor = Color.Red;
                this.fpDept.Columns[3].Font = new Font("����", 10, FontStyle.Bold);
                this.fpDept.ColumnHeader.Cells[0, 4].Text = "�ѹҺ���";
                this.fpDept.ColumnHeader.Cells[0, 5].Text = "ԤԼ�޶�";
                this.fpDept.Columns[5].ForeColor = Color.Blue;
                this.fpDept.Columns[5].Font = new Font("����", 10, FontStyle.Bold);
                this.fpDept.ColumnHeader.Cells[0, 6].Text = "ԤԼ�ѹ�";
            }
            else//����ר�ҡ������û���Ű�Ŀ���,ֻ��ʾ���������
            {
                this.fpDept.ColumnCount = this.DisplayDeptColumnCnt * 2;
                int width = /*this.fpSpread1.Width*/500 * 2 / this.fpDept.ColumnCount;

                //������
                for (int i = 0; i < this.fpDept.ColumnCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        this.fpDept.ColumnHeader.Cells[0, i].Text = "����";
                        this.fpDept.Columns[i].Width = width / 3;
                        this.fpDept.Columns[i].BackColor = Color.Linen;
                        this.fpDept.Columns[i].CellType = txt;
                    }
                    else
                    {
                        this.fpDept.ColumnHeader.Cells[0, i].Text = "��������";
                        this.fpDept.Columns[i].Width = width * 2 / 3;
                    }
                }
            }
            this.fpDept.GrayAreaBackColor = SystemColors.Window;
            this.fpDept.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpDept.RowHeader.Visible = false;
            this.fpDept.RowCount = 0;
        }
        /// <summary>
        /// ����ҽ���б���ʾ�ĸ�ʽ
        /// </summary>
        /// <param name="IsDisplaySchema"></param>
        private void SetDoctFpStyle(bool IsDisplaySchema)
        {
            this.fpDoctor.Reset();
            this.fpDoctor.SheetName = "�������";

            FarPoint.Win.Spread.CellType.TextCellType txt = new FarPoint.Win.Spread.CellType.TextCellType();

            if (IsDisplaySchema)
            {
                //this.fpDoctor.ColumnCount = 10;
                //ygch ��ʾר�ҵĺ�������{8BF9E56B-828E-4264-9D2F-B0B74FB920B5}
                this.fpDoctor.ColumnCount = 11;
                this.fpDoctor.ColumnHeader.Rows[0].Height = 30;

                this.fpDoctor.ColumnHeader.Cells[0, 0].Text = "����";
                this.fpDoctor.ColumnHeader.Columns[0].Width = 40;
                this.fpDoctor.Columns[0].CellType = txt;
                this.fpDoctor.ColumnHeader.Cells[0, 1].Text = "ר������";
                this.fpDoctor.ColumnHeader.Columns[1].Width = 60;
                this.fpDoctor.ColumnHeader.Cells[0, 2].Text = "����ʱ��";
                this.fpDoctor.ColumnHeader.Columns[2].Width = 120;
                this.fpDoctor.ColumnHeader.Cells[0, 3].Text = "�Һ��޶�";
                this.fpDoctor.ColumnHeader.Columns[3].Width = 35;
                this.fpDoctor.Columns[3].ForeColor = Color.Red;
                this.fpDoctor.Columns[3].Font = new Font("����", 10, FontStyle.Bold);
                this.fpDoctor.ColumnHeader.Cells[0, 4].Text = "ʣ�����";
                this.fpDoctor.ColumnHeader.Columns[4].Width = 35;
                this.fpDoctor.ColumnHeader.Cells[0, 5].Text = "ԤԼ�޶�";
                this.fpDoctor.ColumnHeader.Columns[5].Width = 35;
                this.fpDoctor.Columns[5].ForeColor = Color.Blue;
                this.fpDoctor.Columns[5].Font = new Font("����", 10, FontStyle.Bold);
                this.fpDoctor.ColumnHeader.Cells[0, 6].Text = "��ԤԼ��";
                this.fpDoctor.ColumnHeader.Columns[6].Width = 35;
                this.fpDoctor.ColumnHeader.Cells[0, 7].Text = "�����޶�";
                this.fpDoctor.ColumnHeader.Columns[7].Width = 35;
                this.fpDoctor.Columns[7].ForeColor = Color.Magenta;
                this.fpDoctor.Columns[7].Font = new Font("����", 10, FontStyle.Bold);
                this.fpDoctor.ColumnHeader.Cells[0, 8].Text = "�����ѹ�";
                this.fpDoctor.ColumnHeader.Columns[8].Width = 35;
                this.fpDoctor.ColumnHeader.Cells[0, 9].Text = "ר��";
                this.fpDoctor.ColumnHeader.Columns[9].Width = 100;
                this.fpDoctor.ColumnHeader.Columns[9].Visible = false;
                //ygch
                this.fpDoctor.ColumnHeader.Cells[0, 10].Text = "��������";
                this.fpDoctor.ColumnHeader.Columns[10].Width = 35;
            }
            else
            {
                this.fpDoctor.ColumnCount = this.DisplayDoctColumnCnt * 2;
                int width = /*this.fpSpread1.Width*/500 * 2 / this.fpDoctor.ColumnCount;

                //������
                for (int i = 0; i < this.fpDoctor.ColumnCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        this.fpDoctor.ColumnHeader.Cells[0, i].Text = "����";
                        this.fpDoctor.Columns[i].Width = width / 3;
                        this.fpDoctor.Columns[i].BackColor = Color.Linen;
                        this.fpDoctor.Columns[i].CellType = txt;
                    }
                    else
                    {
                        this.fpDoctor.ColumnHeader.Cells[0, i].Text = "��������";
                        this.fpDoctor.Columns[i].Width = width * 2 / 3;
                    }
                }
            }
            this.fpDoctor.GrayAreaBackColor = SystemColors.Window;
            this.fpDoctor.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpDoctor.RowHeader.Visible = false;
            this.fpDoctor.RowCount = 0;
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private void GetParameter()
        {
            //Ĭ����ʾ��ͬ��λ
            this.DefaultPactID = this.ctlMgr.QueryControlerInfo("400005");
            if (DefaultPactID == null || DefaultPactID == "-1") DefaultPactID = "";
            //���ѻ��߹Һ�����
            string rtn = this.ctlMgr.QueryControlerInfo("400007");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "10";

            this.DayRegNumOfPub = int.Parse(rtn);
            //����Ƿ���
            rtn = this.ctlMgr.QueryControlerInfo("400008");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsPubDiagFee = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            //ר�Һ��Ƿ�ѡ�����
            rtn = this.ctlMgr.QueryControlerInfo("400010");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsSelectDeptFirst = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            //			//��ר�ƺ��Ƿ�ֻ��ʾ����ר��
            //			rtn = this.ctlMgr.QueryControlerInfo("400011") ;
            //			if( rtn == null || rtn == "-1" || rtn == "") rtn = "0" ;
            //			this.IsDisplaySchemaDept = FS.neuFC.Function.NConvert.ToBoolean(rtn) ;
            //�Һ��Ƿ��������Ű��޶�
            rtn = this.ctlMgr.QueryControlerInfo("400015");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsAllowOverrun = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            //�Һſ�����ʾ����
            rtn = this.ctlMgr.QueryControlerInfo("400002");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.DisplayDeptColumnCnt = int.Parse(rtn);
            //�Һ�ҽ����ʾ����
            rtn = this.ctlMgr.QueryControlerInfo("400004");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.DisplayDoctColumnCnt = int.Parse(rtn);
            //��ӡ�վ�?
            //			rtn = this.ctlMgr.QueryControlerInfo("400017");
            //			if( rtn == null || rtn == "-1" || rtn == "") rtn = "Invoice" ;
            //			this.PrintWhat = rtn ;

            //��ȡ���������ͣ�1����Ʊ��,2����Ʊ�ţ����Һ��վݺ�,3����Ʊ�ţ��������վݺţ�
            rtn = this.ctlMgr.QueryControlerInfo("400019");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.GetRecipeType = int.Parse(rtn);


            //��ȡ����Ƿ�����ԤԼ��ˮ�Ŵ�
            rtn = this.ctlMgr.QueryControlerInfo("400020");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsInputOrder = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            //ҽ�������������б��Ƿ���ʾȫԺ��ҽ��������
            //{920686B9-AD51-496e-9240-5A6DA098404E}
            //rtn = this.ctlMgr.QueryControlerInfo("400022");
            //if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            //this.ComboxIsListAll = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            //����Ƿ�����ԤԼʱ��δ�
            rtn = this.ctlMgr.QueryControlerInfo("400023");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsInputTime = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            //����ʱ�Ƿ���ʾ
            rtn = this.ctlMgr.QueryControlerInfo("400024");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsPrompt = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            ///�Ƿ�ԤԼ�ſ�����������ֳ���ǰ���
            rtn = this.ctlMgr.QueryControlerInfo("400026");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsPreFirst = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            ///����������0���յ���1��������2��������
            rtn = this.ctlMgr.QueryControlerInfo("400027");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            //this.IsKTF = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            this.otherFeeType = rtn;

            //ר�Һ��Ƿ����ֽ��ڼ���
            rtn = this.ctlMgr.QueryControlerInfo("400028");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsDivLevel = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            if (this.IsDivLevel)
            {
                this.alProfessor = this.conMgr.QueryConstantList("Professor");
            }

            //���źŵڶ����Ƿ����Ӻ�
            rtn = this.ctlMgr.QueryControlerInfo("400029");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.MultIsAppend = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            //�˻�����
            //rtn = this.ctlMgr.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process);
            //if (string.IsNullOrEmpty(rtn) || rtn == "-1") rtn = "0";
            //this.isAccountTerimalFee = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            //�����źŷ�ʽ 1ҽ�� 2���� 3����
            rtn = this.ctlMgr.QueryControlerInfo("ZZGH01");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.strGetSeeNoType = rtn;
        }
        /// <summary>
        /// ������ʹ��ֱ���շ����ɵĺ��ٽ��йҺ�
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParams = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            string cardRule = controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˺Ŷ�Ϊֱ���շ�ʹ�ã���ѡ�������Ŷ�"), FS.FrameWork.Management.Language.Msg("��ʾ"));
                    return -1;
                }
            }
            return 1;
        }

        #region regLevel
        /// <summary>
        /// ��ʼ���Һż���
        /// </summary>
        /// <returns></returns>
        private int initRegLevel()
        {
            al = this.getRegLevelFromXML();
            if (al == null) return -1;

            ///�������û������,�����ݿ��ж�ȡ 
            if (al.Count == 0)
            {
                al = this.RegLevelMgr.Query(true);
            }

            if (al == null)
            {
                MessageBox.Show("��ѯ�Һż������!" + this.RegLevelMgr.Err, "��ʾ");
                return -1;
            }

            this.AddRegLevelToFp(al);
            this.AddRegLevelToCombox(al);
            return 0;
        }

        /// <summary>
        /// �ӱ��ض�ȡ�Һż���,Ȩ�޿���
        /// </summary>
        /// <returns></returns>
        private ArrayList getRegLevelFromXML()
        {
            ArrayList alLists = new ArrayList();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/RegLevelList.xml");
            }
            catch { return alLists; }


            try
            {
                XmlNodeList nodes = doc.SelectNodes(@"//Level");

                foreach (XmlNode node in nodes)
                {
                    FS.HISFC.Models.Registration.RegLevel level = new FS.HISFC.Models.Registration.RegLevel();
                    level.ID = node.Attributes["ID"].Value;//
                    level.Name = node.Attributes["Name"].Value;
                    level.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsExpert"].Value);
                    level.IsFaculty = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsFaculty"].Value);
                    level.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsSpecial"].Value);
                    level.IsDefault = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsDefault"].Value);

                    alLists.Add(level);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡ�Һż������!" + e.Message);
                return null;
            }

            return alLists;
        }
        /// <summary>
        /// ���Һż�����ӵ�FarPoint�б���
        /// </summary>
        /// <param name="regLevels"></param>
        /// <returns></returns>
        private int AddRegLevelToFp(ArrayList regLevels)
        {
            int count = 0, row = 0, colCount = 0;

            colCount = this.fpRegLevel.ColumnCount / 2;

            if (this.fpRegLevel.RowCount > 0)
                this.fpRegLevel.Rows.Remove(0, this.fpRegLevel.RowCount);

            foreach (FS.FrameWork.Models.NeuObject obj in regLevels)
            {
                if (count % colCount == 0)
                {
                    this.fpRegLevel.Rows.Add(this.fpRegLevel.RowCount, 1);
                    row = this.fpRegLevel.RowCount - 1;
                }

                this.fpRegLevel.SetValue(row, 2 * (count % colCount), obj.ID, false);
                this.fpRegLevel.SetValue(row, 2 * (count % colCount) + 1, obj.Name, false);

                count++;
            }

            return 0;
        }
        /// <summary>
        /// ���Һż�����ӵ�Combox��
        /// </summary>
        /// <param name="regLevels"></param>
        /// <returns></returns>
        private int AddRegLevelToCombox(ArrayList regLevels)
        {
            //��ӵ������б�
            this.cmbRegLevel.AddItems(al);

            return 0;
        }

        #endregion

        #region dept
        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <returns></returns>
        private ArrayList GetClinicDepts()
        {
            al = this.conMgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show("��ȡ�������ʱ����!" + this.conMgr.Err, "��ʾ");
                return null;
            }

            return al;
        }
        /// <summary>
        /// ��ȡ����Ա�Һſ���
        /// </summary>
        private int InitRegDept()
        {
            //��ȡ�������Ա�ҺŵĿ����б�
            this.alAllowedDept = this.GetAllowedDepts();

            //����
            if (alAllowedDept == null)
            {
                this.alAllowedDept = new ArrayList();
                return -1;
            }
            //��ӵ�DataSet��
            this.AddAllowedDeptToDataSet(this.alAllowedDept);

            //û��ά������Ա��Ӧ�ĹҺſ���,Ĭ�Ͽɹ������������
            if (alAllowedDept.Count == 0)
            {
                this.AddClinicDeptsToDataSet(this.alDept);
            }

            //��dataset��ӵ�farpoint
            this.addRegDeptToFp(false);
            //��dataset��ӵ�combox
            this.addRegDeptToCombox();

            return 0;
        }
        /// <summary>
        /// ��ȡ�������Ա�ҺŵĿ����б�
        /// </summary>
        /// <returns></returns>
        private ArrayList GetAllowedDepts()
        {
            al = this.permissMgr.Query((FS.FrameWork.Models.NeuObject)this.regMgr.Operator);
            if (al == null)
            {
                MessageBox.Show("��ȡ����Ա�Һſ���ʱ����!" + this.permissMgr.Err, "��ʾ");
                return null;
            }

            //{8AB04EE1-0A7B-45f9-A897-8CD01CE29ED1}

            if (al.Count > 0)
            {
                FS.FrameWork.Models.NeuObject obj = al[0] as FS.FrameWork.Models.NeuObject;
                if (obj.Memo == "0") //�ų���
                {
                    al = this.permissMgr.QueryOutContain((FS.FrameWork.Models.NeuObject)this.regMgr.Operator);
                    if (al == null)
                    {
                        MessageBox.Show("��ȡ����Ա�Һſ���ʱ����(�ų�)!" + this.permissMgr.Err, "��ʾ");
                        return null;
                    }
                }

            }

            return al;
        }

        /// <summary>
        /// ���������Ա�ҺŵĿ�����ӵ�DataSet
        /// </summary>
        /// <param name="allowedDepts"></param>
        private void AddAllowedDeptToDataSet(ArrayList allowedDepts)
        {
            this.dsItems.Tables[0].Rows.Clear();

            //����Һſ������鷵�ص���neuobjectʵ��
            foreach (FS.FrameWork.Models.NeuObject obj in allowedDepts)
            {
                //��ת��ΪDeptartment ʵ��,
                FS.HISFC.Models.Base.Department dept;
                //���ݴ������ʵ��
                dept = this.getDeptByID(obj.User01);
                //��ʵ����ӵ�DataSet��
                if (dept != null)
                    this.addDeptToDataSet(dept);
            }
        }
        /// <summary>
        /// ���ҿ���-���ݿ��Ҵ���
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Department getDeptByID(string ID)
        {
            #region no used
            //			IEnumerator index=this.alDept.GetEnumerator();
            //			
            //			while(index.MoveNext())
            //			{
            //				if((index.Current as FS.HISFC.Models.Base.Department).ID==ID)
            //					return (index.Current;
            //			}
            //			return null;
            #endregion

            foreach (FS.HISFC.Models.Base.Department obj in this.alDept)
            {
                if (obj.ID == ID)
                    return obj;
            }
            return null;
        }
        /// <summary>
        /// Add deptartment to DataSet,����ʵ�ֶ�̬���˹���
        /// </summary>
        /// <param name="dept"></param>
        private void addDeptToDataSet(FS.HISFC.Models.Base.Department dept)
        {
            dsItems.Tables["Dept"].Rows.Add(new object[]
                {
                    dept.ID,
                    dept.Name,
                    dept.SpellCode,
                    dept.WBCode,
                    dept.UserCode,
                    0,
                    0,
                    0,
                    0,
                    DateTime.MinValue,
                    DateTime.MinValue,
                    "",
                    false});
        }

        /// <summary>
        /// �����������ӵ�Dataset
        /// </summary>
        /// <param name="depts"></param>
        private void AddClinicDeptsToDataSet(ArrayList depts)
        {
            this.dsItems.Tables[0].Rows.Clear();

            foreach (FS.HISFC.Models.Base.Department dept in depts)
            {
                this.addDeptToDataSet(dept);
            }
        }
        /// <summary>
        /// ���ɹҺſ����б�-FarPoint
        /// </summary>
        /// <returns></returns>
        private int addRegDeptToFp(bool IsDisplaySchema)
        {
            //��ӵ�farpoint
            if (this.fpDept.RowCount > 0)
                this.fpDept.Rows.Remove(0, this.fpDept.RowCount);

            DataRowView dataRow;

            if (IsDisplaySchema)
            {
                for (int i = 0; i < dvDepts.Count; i++)
                {
                    dataRow = dvDepts[i];
                    this.fpDept.Rows.Add(this.fpDept.RowCount, 1);

                    this.fpDept.SetValue(i, 0, dataRow["ID"], false);
                    this.fpDept.SetValue(i, 1, dataRow["Name"], false);

                    if (dataRow["IsAppend"].ToString().ToUpper() == "TRUE")//�Ӻ�
                    {
                        this.fpDept.SetValue(i, 2, this.getNoon(dataRow["Noon"].ToString()) + "[�Ӻ�]", false);
                    }
                    else
                    {
                        this.fpDept.SetValue(i, 2, this.getNoon(dataRow["Noon"].ToString()) +
                            "[" + DateTime.Parse(dataRow["BeginTime"].ToString()).ToString("HH:mm") + "��" +
                            DateTime.Parse(dataRow["EndTime"].ToString()).ToString("HH:mm") + "]", false);
                    }

                    this.fpDept.SetValue(i, 3, dataRow["RegLmt"], false);
                    this.fpDept.SetValue(i, 4, dataRow["Reged"], false);
                    this.fpDept.SetValue(i, 5, dataRow["TelLmt"], false);
                    this.fpDept.SetValue(i, 6, dataRow["Teled"], false);
                }
                this.fpDept.Tag = "1";
            }
            else
            {
                #region ""
                int count = 0, colCount = 0, row = 0;

                colCount = this.fpDept.Columns.Count / 2;

                for (int i = 0; i < dvDepts.Count; i++)
                {
                    if (count % colCount == 0)
                    {
                        this.fpDept.Rows.Add(this.fpDept.RowCount, 1);
                        row = this.fpDept.RowCount - 1;
                    }

                    dataRow = dvDepts[i];
                    this.fpDept.SetValue(row, 2 * (count % colCount), dataRow[0].ToString(), false);
                    this.fpDept.SetValue(row, 2 * (count % colCount) + 1, dataRow[1].ToString(), false);
                    count++;
                }
                #endregion
                this.fpDept.Tag = "0";
            }
            return 0;
        }

        /// <summary>
        /// init Reg department combox
        /// </summary>
        private void addRegDeptToCombox()
        {
            DataRow row;
            al = new ArrayList();

            for (int i = 0; i < this.dsItems.Tables["Dept"].Rows.Count; i++)
            {
                row = this.dsItems.Tables["Dept"].Rows[i];
                //�ظ��Ĳ����
                if (i > 0 && row["ID"].ToString() == dsItems.Tables["Dept"].Rows[i - 1]["ID"].ToString()) continue;

                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                dept.ID = row["ID"].ToString();
                dept.Name = row["Name"].ToString();
                dept.SpellCode = row["Spell_Code"].ToString();
                dept.WBCode = row["Wb_Code"].ToString();
                dept.UserCode = row["Input_Code"].ToString();

                this.al.Add(dept);
            }

            this.cmbDept.AddItems(this.al);
        }
        #endregion

        #region doct
        /// <summary>
        /// ��ʼ��ҽ���б�
        /// </summary>
        /// <returns></returns>
        private int InitDoct()
        {
            alDoct = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alDoct == null)
            {
                MessageBox.Show("��ȡ����ҽ���б�ʱ����!" + conMgr.Err, "��ʾ");
                alDoct = new ArrayList();
                //return -1;
            }

            this.cmbDoctor.AddItems(alDoct);

            this.AddDoctToDataSet(alDoct);
            this.AddDoctToFp(false);

            return 0;
        }
        /// <summary>
        /// ��ҽ����ӵ�DataSet 
        /// </summary>
        /// <param name="alPersons"></param>
        /// <returns></returns>
        private int AddDoctToDataSet(ArrayList alPersons)
        {
            dsItems.Tables["Doct"].Rows.Clear();

            foreach (FS.HISFC.Models.Base.Employee person in alPersons)
            {
                this.dsItems.Tables["Doct"].Rows.Add(new object[]
                    {
                        person.ID,	//ҽ������
                        person.Name,//ҽ������
                        person.SpellCode,
                        person.WBCode,
                        0,0,0,0,0,0,DateTime.MinValue,DateTime.MinValue,"",false,"",false
                    });
            }

            return 0;
        }

        /// <summary>
        /// ������ҽ����ӵ�ҽ���б�
        /// </summary>
        /// <param name="ds"></param>
        private void AddDoctToDataSet(DataSet ds)
        {
            dsItems.Tables["Doct"].Rows.Clear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];

                dsItems.Tables["Doct"].Rows.Add(new object[]
                    {
                        row[0],//ҽ������
                        row[1],//ҽ������
                        row[12],//ƴ����
                        row[13],//�����						
                        row[5],//�Һ��޶�
                        row[6],//�ѹҺ���
                        row[7],//ԤԼ�޶�
                        row[8],//��ԤԼ��
                        row[9],//�����޶�
                        row[10],//�����ѹ�
                        row[3],//��ʼʱ��
                        row[4],//����ʱ��
                        row[2],//���
                        FS.FrameWork.Function.NConvert.ToBoolean(row[11]),
                        row[14],
                        FS.FrameWork.Function.NConvert.ToBoolean(row[15]),//�Ƿ����
                        //row[16]
                        //ygch row[16]��ҽ����������� row[17]��ר�ҵĺ�������{8BF9E56B-828E-4264-9D2F-B0B74FB920B5} 
                        row[16],
                        row[17]
                    });
            }
        }
        /// <summary>
        /// ��ҽ��������ӵ�FarPoint��
        /// </summary>	
        /// <param name="IsDisplaySchema"></param>	
        /// <returns></returns>
        private int AddDoctToFp(bool IsDisplaySchema)
        {
            //���
            if (this.fpDoctor.RowCount > 0)
                this.fpDoctor.Rows.Remove(0, this.fpDoctor.RowCount);

            DataRowView row;

            if (IsDisplaySchema)
            {
                #region ""

                FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

                if (this.IsProfessor(level))//�ҽ��ںţ���������ǰ��
                {
                    this.dvDocts.Sort = "IsProfessor Desc, ID, Noon, IsAppend, BeginTime";
                }
                else//�����ں�,����������ǰ��
                {
                    this.dvDocts.Sort = "IsProfessor, ID, Noon, IsAppend, BeginTime";
                }

                for (int i = 0; i < dvDocts.Count; i++)
                {
                    row = dvDocts[i];

                    this.fpDoctor.Rows.Add(this.fpDoctor.RowCount, 1);

                    this.fpDoctor.SetValue(i, 0, row["ID"], false);
                    this.fpDoctor.SetValue(i, 1, row["Name"], false);

                    if (row["IsAppend"].ToString().ToUpper() == "TRUE")//�Ӻ�
                    {
                        this.fpDoctor.SetValue(i, 2, this.getNoon(row["Noon"].ToString()) + "[�Ӻ�]", false);
                    }
                    else
                    {
                        this.fpDoctor.SetValue(i, 2, this.getNoon(row["Noon"].ToString()) +
                            "[" + DateTime.Parse(row["BeginTime"].ToString()).ToString("HH:mm") + "��" +
                            DateTime.Parse(row["EndTime"].ToString()).ToString("HH:mm") + "]", false);
                    }

                    this.fpDoctor.SetValue(i, 3, row["RegLmt"], false);
                    this.fpDoctor.SetValue(i, 4, FS.FrameWork.Function.NConvert.ToInt32(row["RegLmt"]) - FS.FrameWork.Function.NConvert.ToInt32(row["Reged"]), false);
                    this.fpDoctor.SetValue(i, 5, row["TelLmt"], false);
                    this.fpDoctor.SetValue(i, 6, row["Teled"], false);
                    this.fpDoctor.SetValue(i, 7, row["SpeLmt"], false);
                    this.fpDoctor.SetValue(i, 8, row["Sped"], false);
                    this.fpDoctor.SetValue(i, 9, row["Memo"], false);
                    // ygch ��ʾר�ҵĺ�������{8BF9E56B-828E-4264-9D2F-B0B74FB920B5}
                    this.fpDoctor.SetValue(i, 10, row["num"], false);

                    //���ڡ���������ɫ����
                    if (row["IsProfessor"].ToString().ToUpper() == "TRUE")
                    {
                        this.fpDoctor.Rows[i].BackColor = Color.LightGreen;
                    }
                }
                this.Span();

                #endregion
                this.fpDoctor.Tag = "1";
            }
            else
            {
                int RowCount = 0, ColumnCount, Row = 0;

                ColumnCount = this.fpDoctor.ColumnCount / 2;
                foreach (DataRowView dv in this.dvDocts)
                {
                    if (RowCount % ColumnCount == 0)
                    {
                        this.fpDoctor.Rows.Add(this.fpDoctor.RowCount, 1);
                        Row = this.fpDoctor.RowCount - 1;
                    }

                    this.fpDoctor.SetValue(Row, 2 * (RowCount % ColumnCount), dv["ID"].ToString(), false);
                    this.fpDoctor.SetValue(Row, 2 * (RowCount % ColumnCount) + 1, dv["Name"].ToString(), false);

                    RowCount++;
                }
                this.fpDoctor.Tag = "0";
            }

            return 0;
        }
        /// <summary>
        /// ѹ����ʾҽ������
        /// </summary>
        private void Span()
        {
            int rowLastDoct = 0;

            int rowCnt = this.fpDoctor.RowCount;

            for (int i = 0; i < rowCnt; i++)
            {
                if (i > 0 && this.fpDoctor.GetText(i, 0) != this.fpDoctor.GetText(i - 1, 0))
                {
                    if (i - rowLastDoct > 1)
                    {
                        this.fpDoctor.Models.Span.Add(rowLastDoct, 0, i - rowLastDoct, 1);
                        this.fpDoctor.Models.Span.Add(rowLastDoct, 1, i - rowLastDoct, 1);
                    }

                    rowLastDoct = i;
                }

                //���һ�д���
                if (i > 0 && i == rowCnt - 1 && this.fpDoctor.GetText(i, 0) == this.fpDoctor.GetText(i - 1, 0))
                {
                    this.fpDoctor.Models.Span.Add(rowLastDoct, 0, i - rowLastDoct + 1, 1);
                    this.fpDoctor.Models.Span.Add(rowLastDoct, 1, i - rowLastDoct + 1, 1);
                }
            }
        }
        /// <summary>
        /// add doctor to combox
        /// </summary>
        private void AddDoctToCombox()
        {
            DataRow row;
            al = new ArrayList();

            for (int i = 0; i < this.dsItems.Tables["Doct"].Rows.Count; i++)
            {
                row = this.dsItems.Tables["Doct"].Rows[i];
                //�ظ��Ĳ����
                if (i > 0 && row["ID"].ToString() == dsItems.Tables["Doct"].Rows[i - 1]["ID"].ToString()) continue;

                FS.HISFC.Models.Base.Employee p = new FS.HISFC.Models.Base.Employee();
                p.ID = row["ID"].ToString();
                p.Name = row["Name"].ToString();
                p.SpellCode = row["Spell_Code"].ToString();
                p.WBCode = row["Wb_Code"].ToString();
                p.UserCode = row["user_code"].ToString();
                p.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(row["IsProfessor"].ToString());//�Ƿ�ר��
                p.Memo = "[" + this.getNoon(row["Noon"].ToString()) + "] " + row["Memo"].ToString();

                this.al.Add(p);
            }

            this.cmbDoctor.AddItems(this.al);
        }
        #endregion

        /// <summary>
        /// ��ʼ��֤�����
        /// </summary>
        /// <returns></returns>
        private int InitCardType()
        {
            al = this.conMgr.QueryConstantList("IDCard");
            if (al == null)
            {
                MessageBox.Show("��ȡ֤������ʱ����!" + this.conMgr.Err, "��ʾ");
                return -1;
            }

            this.cmbCardType.AddItems(al);

            return 0;
        }

        /// <summary>
        /// ����Ĭ��֤������
        /// </summary>
        private void SetDefaultCardType()
        {
            this.cmbCardType.SelectedIndex = 0;
        }

        /// <summary>
        /// ���ɽ�������б�
        /// </summary>
        /// <returns></returns>
        private int initPact()
        {
            int count = 0, colCount = 0, row = 0;

            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //this.al = this.conMgr.QueryConstantList("PACTUNIT");
            //this.al = this.pactMgr.GetPactUnitInfo() ;
            this.al = feeMgr.QueryPactUnitAll();
            if (al == null)
            {
                MessageBox.Show("��ȡ���ߺ�ͬ��λ��Ϣʱ����!" + this.conMgr.Err, "��ʾ");
                return -1;
            }

            colCount = this.fpPayKind.ColumnCount / 2;

            if (this.fpPayKind.RowCount > 0)
                this.fpPayKind.Rows.Remove(0, this.fpPayKind.RowCount);
            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //foreach (FS.HISFC.Models.Base.Const obj in this.al)
            foreach (FS.FrameWork.Models.NeuObject obj in this.al)
            {
                //if (obj.IsValid == false) continue;//����

                if (count % colCount == 0)
                {
                    this.fpPayKind.Rows.Add(this.fpPayKind.RowCount, 1);
                    row = this.fpPayKind.RowCount - 1;
                }

                this.fpPayKind.SetValue(row, 2 * (count % colCount), obj.ID, false);
                this.fpPayKind.SetValue(row, 2 * (count % colCount) + 1, obj.Name, false);

                count++;
            }

            this.cmbPayKind.AddItems(this.al);

            return 0;
        }
        /// <summary>
        /// �������뷨�б�
        /// </summary>
        private void initInputMenu()
        {

            for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                InputLanguage t = InputLanguage.InstalledInputLanguages[i];
                System.Windows.Forms.ToolStripMenuItem m = new ToolStripMenuItem();
                m.Text = t.LayoutName;
                //m.Checked = true;
                m.Click += new EventHandler(m_Click);

                this.neuContextMenuStrip1.Items.Add(m);
            }
        }

        /// <summary>
        /// ��ʼ��ԤԼʱ��ؼ�
        /// </summary>
        private void InitBookingDate()
        {
            this.ucChooseDate = new FS.HISFC.Components.Registration.ucChooseBookingDate();

            this.panel1.Controls.Add(ucChooseDate);

            this.ucChooseDate.BringToFront();
            this.ucChooseDate.Location = new Point(this.dtBookingDate.Left, this.dtBookingDate.Top + this.dtBookingDate.Height);
            this.ucChooseDate.Visible = false;
            this.ucChooseDate.SelectedItem += new FS.HISFC.Components.Registration.ucChooseBookingDate.dSelectedItem(ucChooseDate_SelectedItem);
        }
        /// <summary>
        /// ����
        /// </summary>
        private void clear()
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            this.regObj = null;
            //�趨Ĭ��
            //this.SetRegLevelDefault() ;

            this.cmbDept.Tag = "";
            this.cmbDoctor.Tag = "";
            this.txtCardNo.Text = "";
            this.cmbSex.Text = "��";

            //this.txtAge.Text = "";
            this.txtAgee.Text = "___��__��__��";
            this.txtName.Text = "";
            this.cmbUnit.SelectedIndex = 0;
            this.cmbPayKind.Tag = this.DefaultPactID;
            this.txtMcardNo.Text = "";
            this.txtPhone.Text = "";
            this.txtAddress.Text = "";
            this.cmbCardType.Tag = "";
            this.dtBirthday.Value = current;
            //this.lbSum.Text = this.fpList.RowCount.ToString(); 
            this.lbSum.Text = this.SetRegNum();
            //this.lbTot.Text = "";
            //this.lbReceive.Text = "";
            this.lbTip.Text = "";

            this.ClearBookingInfo();
            this.SetBookingDate(current);
            this.SetDefaultBookingTime(current);
            this.txtCardNo.Focus();
            this.chbEncrpt.Checked = false;
            this.isReadCard = false;
            this.chbBookFee.Checked = true;
            this.txtIdNO.Text = "";
            this.tbSIBalanceCost.Text = string.Empty;
            this.cmbCountry.Tag = null;

            // this.myYBregObj = null;
            this.SetEnabled(true);
            //{0C30F7F0-2BCF-4c03-BA6E-D7E22A638E97}
            this.txtCardNo.Enabled = true;

            this.assignNO = string.Empty;
            this.chSpecialPerson.Checked = false;

        }

        /// <summary>
        /// ���ԤԼ��Ϣ
        /// </summary>
        private void ClearBookingInfo()
        {
            this.txtOrder.Text = "";
            this.txtOrder.Tag = null;
        }

        /// <summary>
        /// �趨�Һż����Ĭ��ֵ
        /// </summary>
        private void SetRegLevelDefault()
        {
            if (this.cmbRegLevel.alItems != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in this.cmbRegLevel.alItems)
                {
                    if ((obj as FS.HISFC.Models.Registration.RegLevel).IsDefault)
                    {
                        this.cmbRegLevel.Text = (obj as FS.HISFC.Models.Registration.RegLevel).Name;
                        this.cmbRegLevel.Tag = (obj as FS.HISFC.Models.Registration.RegLevel).ID;
                        return;
                    }
                }
            }
            this.cmbRegLevel.Tag = "";//�˵��ǻ���,���û��Ĭ��ֵ��س��������ʾ�޹Һż���,��
        }

        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private void InitNoon()
        {
            FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

            this.alNoon = noonMgr.Query();
            if (alNoon == null)
            {
                MessageBox.Show("��ȡ�����Ϣʱ����!" + noonMgr.Err, "��ʾ");
                return;
            }
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string getNoon(DateTime current)
        {
            if (this.alNoon == null) return "";
            /*
             * ��������Ϊ���Ӧ���ǰ���һ��ȫ��ʱ�����磺06~12,����:12~18����Ϊ����,
             * ʵ�����Ϊҽ������ʱ���,�������Ϊ08~11:30������Ϊ14~17:30
             * ��������Һ�Ա����������ʱ��ιҺ�,���п�����ʾ���δά��
             * ���Ը�Ϊ���ݴ���ʱ�����ڵ�������磺9��30��06~12֮�䣬��ô���ж��Ƿ��������
             * 06~12֮�䣬ȫ������˵��9:30���Ǹ�������
             */
            //			foreach(FS.HISFC.Models.Registration.Noon obj in alNoon)
            //			{
            //				if(int.Parse(current.ToString("HHmmss"))>=int.Parse(obj.BeginTime.ToString("HHmmss"))&&
            //					int.Parse(current.ToString("HHmmss"))<int.Parse(obj.EndTime.ToString("HHmmss")))
            //				{
            //					return obj.ID;
            //				}
            //			}

            int[,] zones = new int[,] { { 0, 120000 }, { 120000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(current.ToString("HHmmss"));
            int begin = 0, end = 0;

            for (int i = 0; i < 3; i++)
            {
                if (zones[i, 0] <= time && zones[i, 1] > time)
                {
                    begin = zones[i, 0];
                    end = zones[i, 1];
                    break;
                }
            }

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (int.Parse(obj.StartTime.ToString("HHmmss")) >= begin &&
                    int.Parse(obj.EndTime.ToString("HHmmss")) <= end)
                {
                    return obj.ID;
                }
            }

            return "";
        }
        /// <summary>
        /// �����������ȡ�������
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string getNoon(string ID)
        {
            if (this.alNoon == null) return ID;

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (obj.ID == ID) return obj.Name;
            }

            return ID;
        }
        private string QeryNoonName(string noonid)
        {
            FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();
            return noonMgr.Query(noonid);

        }

        #region Get��Set Oper's Recipe
        /// <summary>
        /// ��ȡ��ǰ������
        /// </summary>
        /// <param name="OperID"></param>		
        private void GetRecipeNo(string OperID)
        {
            if (this.GetRecipeType == 1)
            {
                this.txtRecipeNo.Text = "";//ÿ�ε�½�Լ�¼�봦����
            }
            else if (this.GetRecipeType == 2)
            {
                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstansObj("RegRecipeNo", OperID);
                if (obj == null)
                {
                    MessageBox.Show("��ȡ�����ų���!" + this.conMgr.Err, "��ʾ");
                    return;
                }
                if (obj.Name == "")
                {
                    this.txtRecipeNo.Text = "0";
                }
                else
                {
                    this.txtRecipeNo.Text = obj.Name;
                }
            }
            //{B0B20CE3-195C-4aee-AB13-CEBB5EA9BB94}
            else
            {
                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstansObj("RegRecipeNo", OperID);
                if (obj == null)
                {
                    MessageBox.Show("��ȡ�����ų���!" + this.conMgr.Err, "��ʾ");
                    return;
                }
                if (obj.Name == "")
                {
                    this.txtRecipeNo.Text = "0";
                }
                else
                {
                    this.txtRecipeNo.Text = obj.Name;
                }
            }
        }

        /// <summary>
        /// �޸Ĵ�����
        /// </summary>
        private void ChangeRecipe()
        {
            //this.txtRecipeNo.TabStop = true ;
            this.txtRecipeNo.BorderStyle = BorderStyle.Fixed3D;
            this.txtRecipeNo.BackColor = SystemColors.Window;
            this.txtRecipeNo.ReadOnly = false;
            this.txtRecipeNo.ForeColor = SystemColors.WindowText;
            this.txtRecipeNo.Font = new Font("����", 10);
            this.txtRecipeNo.Location = new Point(381, 10);

            this.txtRecipeNo.Focus();
        }
        private void txtRecipeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        /// <summary>
        /// ���ô���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>		
        private void txtRecipeNo_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtRecipeNo.ReadOnly == false)
            {
                string r = this.txtRecipeNo.Text.Trim();

                try
                {
                    if (long.Parse(r) < 0)
                    {
                        MessageBox.Show("�����Ų���С����!", "��ʾ");
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    MessageBox.Show("�����ű���������!", "��ʾ");
                    e.Cancel = true;
                    return;
                }
                this.SetRecipeNo();
            }
        }

        /// <summary>
        /// ���ô�����ֻ��
        /// </summary>
        private void SetRecipeNo()
        {
            //this.txtRecipeNo.TabStop = false ;
            this.txtRecipeNo.ReadOnly = true;
            this.txtRecipeNo.Location = new Point(381, 14);
            this.txtRecipeNo.BackColor = SystemColors.AppWorkspace;
            this.txtRecipeNo.ForeColor = Color.Yellow;
            this.txtRecipeNo.Font = new Font("����", 11, FontStyle.Bold);
            this.txtRecipeNo.BorderStyle = BorderStyle.None;
        }


        /// <summary>
        /// �رմ���ʱ����Һ�Ա�Ĵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.regMgr.Connection.State == ConnectionState.Closed) return;
            string recipeNO = this.txtRecipeNo.Text.Trim();
            if ((recipeNO != "" && recipeNO != string.Empty))
            {
                if (this.SaveRecipeNo() == -1)
                {
                    //e.Cancel = true ;
                }
            }
        }
        /// <summary>
        /// ���洦����¼
        /// </summary>
        /// <returns></returns>
        private int SaveRecipeNo()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.regMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.conMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                con.ID = this.regMgr.Operator.ID;//����Ա
                con.Name = this.txtRecipeNo.Text.Trim();//������
                con.IsValid = true;

                int rtn = this.conMgr.UpdateConstant("RegRecipeNo", con);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.conMgr.Err, "��ʾ");
                    return -1;
                }
                if (rtn == 0)//����û�����ݡ�����
                {
                    if (this.conMgr.InsertConstant("RegRecipeNo", con) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.conMgr.Err, "��ʾ");
                        return -1;
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            return 0;
        }
        #endregion

        #region Query operator's registration information of today
        /// <summary>
        /// ������Ա�������չҺ���Ϣ
        /// </summary>
        private void Retrieve()
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            al = this.regMgr.Query(current.Date, current.Date.AddDays(1), this.regMgr.Operator.ID);
            if (al == null)
            {
                MessageBox.Show("�����Һ�Ա���չҺ���Ϣʱ����!" + regMgr.Err, "��ʾ");
                return;
            }

            if (this.fpList.RowCount > 0)
                this.fpList.Rows.Remove(0, this.fpList.RowCount);

            foreach (FS.HISFC.Models.Registration.Register obj in al)
            {
                this.addRegister(obj);
            }
            this.lbSum.Text = this.SetRegNum();

        }
        /// <summary>
        /// ������Ч�Һ���

        /// </summary>
        /// <returns></returns>
        private string SetRegNum()
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            string result = this.regMgr.QueryValidRegNumByOperAndOperDT(this.regMgr.Operator.ID, current.Date.ToString(), current.Date.AddDays(1).ToString());
            if (result == "-1")
            {
                MessageBox.Show(this.regMgr.Err);
                result = "0";
            }

            return result;

        }
        /// <summary>
        /// ��ӹҺż�¼
        /// </summary>
        /// <param name="obj"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register obj)
        {
            this.fpList.Rows.Add(this.fpList.RowCount, 1);
            int cnt = this.fpList.RowCount - 1;
            this.fpList.ActiveRowIndex = cnt;

            this.fpList.SetValue(cnt, 0, obj.PID.CardNO, false);//������
            this.fpList.SetValue(cnt, 1, obj.Name, false);//����
            this.fpList.SetValue(cnt, 2, obj.Pact.Name, false);
            this.fpList.SetValue(cnt, 3, obj.DoctorInfo.Templet.RegLevel.Name, false);
            this.fpList.SetValue(cnt, 4, obj.DoctorInfo.Templet.Dept.Name, false);
            this.fpList.SetValue(cnt, 5, obj.DoctorInfo.Templet.Doct.Name, false);
            this.fpList.SetValue(cnt, 6, obj.OrderNO, false);
            this.fpList.SetValue(cnt, 7, obj.OwnCost, false);
            this.fpList.SetValue(cnt, 8, obj.PubCost, false);
            if (obj.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back ||
                obj.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.fpList.Rows[cnt].BackColor = Color.MistyRose;
            }

            this.fpList.Rows[cnt].Tag = obj;
        }

        #endregion

        /// <summary>
        /// װ�ش�ӡ�ؼ�
        /// </summary>
        /// <returns></returns>
        private int LoadPrint()
        {
            //��ȡ��ӡ�ؼ�������   

            //object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(UFC.Registration.ucRegister), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint));
            //if (o == null)
            //{
            //    MessageBox.Show("��ά��UFC.Registration.ucRegister����ӿ�FS.HISFC.BizProcess.Interface.Registration.IRegPrint��ʵ������!");                
            //}
            //else
            //{
            //    IRegPrint = o as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            //}

            return 0;
        }

        /// <summary>
        /// ��ʼ������Ļ��ʾҽ���Ű���Ϣ
        /// </summary>
        /// <returns></returns>
        private int InitLedDisplay()
        {
            DateTime dtNow = this.SchemaMgr.GetDateTimeFromSysDateTime();
            DateTime dtNoon = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 12, 00, 00);
            string deptID = "0409','0705','8244','8247','0301','0014','0050";
            string strDispaly = string.Empty;
            string strAM = "����";
            string strPM = "����";
            int i = strAM.Length;
            int j = strPM.Length;
            ArrayList al = this.interMgr.QueryDoctByMultiDeptID(dtNow.Date, dtNow, deptID);

            if (al == null || al.Count == 0)
            {
                return -1;
            }
            foreach (FS.HISFC.Models.Registration.Schema schema in al)
            {
                if (schema.Templet.Noon.ID == "1")
                {
                    if (20 - i < 5)
                    {
                        strAM += "\n" + "���� " + schema.Templet.Doct.Name.PadRight(4, '��');
                        i = strAM.Substring(strAM.LastIndexOf("\n") + 1).Length;
                    }
                    else
                    {
                        strAM += " " + schema.Templet.Doct.Name.PadRight(4, '��');
                        if (strAM.Contains("\n"))
                        {
                            i = strAM.Substring(strAM.LastIndexOf("\n") + 1).Length;
                        }
                        else
                        {
                            i = strAM.Length;
                        }
                    }
                }
                else if (schema.Templet.Noon.ID == "2")
                {
                    if (20 - j < 5)
                    {
                        strPM += "\n" + "���� " + schema.Templet.Doct.Name.PadRight(4, '��');
                        j = strPM.Substring(strPM.LastIndexOf("\n") + 1).Length;
                    }
                    else
                    {
                        strPM += " " + schema.Templet.Doct.Name.PadRight(4, '��');
                        if (strPM.Contains("\n"))
                        {
                            j = strPM.Substring(strPM.LastIndexOf("\n") + 1).Length;
                        }
                        else
                        {
                            j = strPM.Length;
                        }
                    }
                }
            }

            if (dtNow <= dtNoon)
            {
                strDispaly = strAM;
            }
            else
            {
                strDispaly = strPM;
            }

            StreamWriter sw;
            if (!System.IO.File.Exists("D:\\load_led.txt"))
            {
                //�����ھ��½�һ���ı��ļ�,��д��һЩ����
                sw = new StreamWriter(System.IO.File.Open("D:\\load_led.txt", FileMode.Create), System.Text.Encoding.Default);
                sw.Write(strDispaly);
                sw.Close();
            }
            else
            {
                System.IO.File.Delete("D:\\load_led.txt");
                sw = new StreamWriter(System.IO.File.Open("D:\\load_led.txt", FileMode.Create), System.Text.Encoding.Default);
                sw.Write(strDispaly);
                sw.Close();
            }

            return 1;
        }
        #endregion

        #region Set booking Date
        /// <summary>
        /// set booking date
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetBookingDate(DateTime seeDate)
        {
            this.dtBookingDate.Value = seeDate.Date;
            this.lbWeek.Text = this.getWeek(seeDate);
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string getWeek(DateTime current)
        {
            string[] week = new string[] { "������", "����һ", "���ڶ�", "������", "������", "������", "������" };

            return week[(int)current.DayOfWeek];
        }

        /// <summary>
        /// ����Ĭ�������,���ﰲ��ʱ�����ʾ
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetDefaultBookingTime(DateTime seeDate)
        {
            FS.HISFC.Models.Registration.Schema schema = new FS.HISFC.Models.Registration.Schema();
            schema.Templet.Begin = seeDate.Date;
            schema.Templet.End = seeDate.Date;

            this.SetBookingTime(schema);

            this.SetBookingTag(null);
        }

        /// <summary>
        /// ���þ���ʱ���
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void SetBookingTime(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBegin.Value = schema.Templet.Begin;
            this.dtEnd.Value = schema.Templet.End;

            this.SetBookingTag(schema);
        }
        /// <summary>
        /// ��������ʱ���ʵ����Ϣ
        /// </summary>
        /// <param name="schema"></param>
        private void SetBookingTag(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBookingDate.Tag = schema;

            if (schema == null)
            {
                this.lbRegLmt.Text = "";
                this.lbReg.Text = "";
                this.lbTelLmt.Text = "";
                this.lbTel.Text = "";
                this.lbSpeLmt.Text = "";
                this.lbSpe.Text = "";
            }
            else
            {
                this.lbRegLmt.Text = schema.Templet.RegQuota.ToString();//���˹Һ��޶�
                this.lbReg.Text = schema.RegedQTY.ToString();//�ѹҺ�����
                this.lbTelLmt.Text = schema.Templet.TelQuota.ToString();//�����޶�
                this.lbTel.Text = schema.TeledQTY.ToString();
                this.lbSpeLmt.Text = schema.Templet.SpeQuota.ToString();//�����޶�
                this.lbSpe.Text = schema.SpedQTY.ToString();
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �Һż���õ�����,��ʾ�Һż����б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_Enter(object sender, System.EventArgs e)
        {
            this.QueryRegLevl();
            if (this.fpSpread1.ActiveSheetIndex != 0) this.fpSpread1.ActiveSheetIndex = 0;

            this.setEnterColor(this.cmbRegLevel);
        }
        /// <summary>
        /// �Һſ��ҵõ����㣬��ʾ�Һſ����б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_Enter(object sender, System.EventArgs e)
        {
            this.setEnterColor(this.cmbDept);

            if (this.fpSpread1.ActiveSheetIndex != 1) this.fpSpread1.ActiveSheetIndex = 1;
        }
        /// <summary>
        /// ҽ���õ����㣬��ʾҽ���б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_Enter(object sender, System.EventArgs e)
        {
            if (this.fpSpread1.ActiveSheetIndex != 2) this.fpSpread1.ActiveSheetIndex = 2;

            this.setEnterColor(this.cmbDoctor);
        }
        /// <summary>
        /// �������õ����㣬��ʾ�б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPayKind_Enter(object sender, System.EventArgs e)
        {
            if (this.fpSpread1.ActiveSheetIndex != 3) this.fpSpread1.ActiveSheetIndex = 3;

            this.setEnterColor(this.cmbPayKind);
        }

        //{D845A003-62E2-44da-8612-81C4F6A1037E}
        private void cmbPayKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject neuObj = conMgr.GetConstant("regcomparepact", this.cmbPayKind.Tag.ToString());
            if (neuObj != null && neuObj.Name != string.Empty)
            {
                this.cmbRegLevel.Tag = neuObj.Name.ToString();
            }
        }
        /// <summary>
        /// �����ŵõ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_Enter(object sender, System.EventArgs e)
        {
            if (this.fpSpread1.ActiveSheetIndex != 4) this.fpSpread1.ActiveSheetIndex = 4;

            this.setEnterColor(this.txtCardNo);
        }
        private void txtName_Enter(object sender, System.EventArgs e)
        {
            if (this.CHInput != null) InputLanguage.CurrentInputLanguage = this.CHInput;
            if (this.fpSpread1.ActiveSheetIndex != 4) this.fpSpread1.ActiveSheetIndex = 4;

            this.setEnterColor(this.txtName);
        }
        private void txtName_Leave(object sender, System.EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }
        private void txtAddress_Enter(object sender, System.EventArgs e)
        {
            if (this.CHInput != null) InputLanguage.CurrentInputLanguage = this.CHInput;
            if (this.fpSpread1.ActiveSheetIndex != 4) this.fpSpread1.ActiveSheetIndex = 4;

            this.setEnterColor(this.txtAddress);
        }

        private void txtAddress_Leave(object sender, System.EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }


        #endregion

        #region ���õ�ǰ�ؼ���ɫ
        private void setEnterColor(Control ctl)
        {
            ctl.BackColor = Color.OldLace;
        }
        private void setLeaveColor(Control ctl)
        {
            ctl.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region �س�

        #region reglevel
        /// <summary>
        /// ������Ӧ�Һ���Ϣ(ģ��,�ѹ�,ʣ�����Ϣ)
        /// </summary>
        private void QueryRegLevl()
        {
            //�ָ���ʼ״̬
            this.cmbDept.Tag = "";
            this.cmbDoctor.Tag = "";
            this.lbTip.Text = "";

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            #region ���ɹҺż����Ӧ�Ŀ��ҡ�ҽ���б�

            //{9C164CC2-29C6-4471-B53B-07853A82F9DF} �޸ĳ�ʼ��bug
            if (this.cmbRegLevel.SelectedItem == null)
            {
                return;
            }
            FS.HISFC.Models.Registration.RegLevel Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            if (Level.IsExpert || Level.IsSpecial)//ר�ҡ�����
            {
                #region ר��
                if (this.IsSelectDeptFirst)//�����ѡ����,���ɿ����Ű��б�
                {
                    this.SetDeptFpStyle(false);
                    //�����Ҳ����ר�ҵĿ����б�
                    this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Doct);
                    this.addRegDeptToFp(false);

                    //����Combox�����б�
                    //{920686B9-AD51-496e-9240-5A6DA098404E}
                    //if (!this.ComboxIsListAll)
                    if (!this.isAddAllDept)
                    {
                        this.addRegDeptToCombox();
                    }
                    else
                    {
                        this.cmbDept.AddItems(this.alDept);
                    }

                    //���ҽ���б�,��ѡ����Һ��ټ�������ר��
                    ArrayList al = new ArrayList();

                    this.AddDoctToDataSet(al);
                    this.AddDoctToFp(true);
                    this.cmbDoctor.AddItems(al);
                }
                else
                {
                    //ר�Һ�ֱ��ѡ��ҽ��,���������Ҵ�,����ȫ����������б�
                    this.SetDeptFpStyle(false);
                    this.AddClinicDeptsToDataSet(this.alDept);
                    this.addRegDeptToFp(false);
                    this.cmbDept.AddItems(this.alDept);
                    //
                    this.GetDoct();//���ȫ������ҽ��
                }
                #endregion
            }
            else if (Level.IsFaculty)//ר��
            {
                #region ר��
                //��ȡ����ר���б�
                this.SetDeptFpStyle(true);
                this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Dept);
                this.addRegDeptToFp(true);

                //����Combox���������б�
                //{920686B9-AD51-496e-9240-5A6DA098404E}
                //if (this.ComboxIsListAll)
                if (this.isAddAllDept)
                {
                    this.cmbDept.AddItems(this.alDept);
                }
                else
                {
                    this.addRegDeptToCombox();
                }

                //���ҽ���б�,ר�Ʋ���Ҫѡ��ҽ��
                ArrayList al = new ArrayList();

                this.AddDoctToDataSet(al);
                this.AddDoctToFp(false);
                this.cmbDoctor.AddItems(al);
                #endregion
            }
            else//��ͨ
            {
                //��ʾ�����б�
                this.SetDeptFpStyle(false);
                if (this.alAllowedDept != null && this.alAllowedDept.Count > 0)
                {
                    this.AddAllowedDeptToDataSet(this.alAllowedDept);
                    this.addRegDeptToCombox();
                }
                else//��ʾȫ������
                {
                    this.AddClinicDeptsToDataSet(this.alDept);
                    this.cmbDept.AddItems(this.alDept);
                }
                this.addRegDeptToFp(false);

            }
            #endregion

            //���ԤԼ��Ϣ
            this.ClearBookingInfo();

            //�趨Ĭ�Ͼ���ʱ���
            this.SetDefaultBookingTime(this.dtBookingDate.Value);

        }

        /// <summary>
        /// ѡ��Һż���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////�ָ���ʼ״̬
            //this.cmbDept.Tag = "";
            //this.cmbDoctor.Tag = "";
            //this.lbTip.Text = "";

            //if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            //#region ���ɹҺż����Ӧ�Ŀ��ҡ�ҽ���б�
            //FS.HISFC.Models.Registration.RegLevel Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            //if (Level.IsExpert || Level.IsSpecial)//ר�ҡ�����
            //{
            //    #region ר��
            //    if (this.IsSelectDeptFirst)//�����ѡ����,���ɿ����Ű��б�
            //    {
            //        this.SetDeptFpStyle(false);
            //        //�����Ҳ����ר�ҵĿ����б�
            //        this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Doct);
            //        this.addRegDeptToFp(false);

            //        //����Combox�����б�
            //        if (!this.ComboxIsListAll)
            //        {
            //            this.addRegDeptToCombox();
            //        }
            //        else
            //        {
            //            this.cmbDept.AddItems(this.alDept);
            //        }

            //        //���ҽ���б�,��ѡ����Һ��ټ�������ר��
            //        ArrayList al = new ArrayList();

            //        this.AddDoctToDataSet(al);
            //        this.AddDoctToFp(true);
            //        this.cmbDoctor.AddItems(al);
            //    }
            //    else
            //    {
            //        //ר�Һ�ֱ��ѡ��ҽ��,���������Ҵ�,����ȫ����������б�
            //        this.SetDeptFpStyle(false);
            //        this.AddClinicDeptsToDataSet(this.alDept);
            //        this.addRegDeptToFp(false);
            //        this.cmbDept.AddItems(this.alDept);
            //        //
            //        this.GetDoct();//���ȫ������ҽ��
            //    }
            //    #endregion
            //}
            //else if (Level.IsFaculty)//ר��
            //{
            //    #region ר��
            //    //��ȡ����ר���б�
            //    this.SetDeptFpStyle(true);
            //    this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Dept);
            //    this.addRegDeptToFp(true);

            //    //����Combox���������б�
            //    if (this.ComboxIsListAll)
            //    {
            //        this.cmbDept.AddItems(this.alDept);
            //    }
            //    else
            //    {
            //        this.addRegDeptToCombox();
            //    }

            //    //���ҽ���б�,ר�Ʋ���Ҫѡ��ҽ��
            //    ArrayList al = new ArrayList();

            //    this.AddDoctToDataSet(al);
            //    this.AddDoctToFp(false);
            //    this.cmbDoctor.AddItems(al);
            //    #endregion
            //}
            //else//��ͨ
            //{
            //    //��ʾ�����б�
            //    this.SetDeptFpStyle(false);
            //    if (this.alAllowedDept != null && this.alAllowedDept.Count > 0)
            //    {
            //        this.AddAllowedDeptToDataSet(this.alAllowedDept);
            //        this.addRegDeptToCombox();
            //    }
            //    else//��ʾȫ������
            //    {
            //        this.AddClinicDeptsToDataSet(this.alDept);
            //        this.cmbDept.AddItems(this.alDept);
            //    }
            //    this.addRegDeptToFp(false);

            //}
            //#endregion

            ////���ԤԼ��Ϣ
            //this.ClearBookingInfo();

            ////�趨Ĭ�Ͼ���ʱ���
            //this.SetDefaultBookingTime(this.dtBookingDate.Value);
            this.QueryRegLevl();
        }
        /// <summary>
        /// �Һż���س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
                {
                    MessageBox.Show("��ѡ��Һż���!", "��ʾ");
                    this.cmbRegLevel.Focus();
                    return;
                }

                //�ж���ר�Һ�,������ҽ����
                FS.HISFC.Models.Registration.RegLevel Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                //���ɷ���
                if (this.getCost() == -1)
                {
                    this.cmbRegLevel.Focus();
                    return;
                }

                //������ת
                //ר�ҡ�����Ų���ѡ��Һſ���,ֱ������ҽ����
                if (Level.IsExpert || Level.IsSpecial)
                {
                    if (this.IsSelectDeptFirst)
                    {
                        this.cmbDept.Focus();
                    }
                    else
                    {
                        this.cmbDoctor.Focus();
                    }
                }
                else if (Level.IsFaculty)//ר�ƺ�,ֱ���������Ҵ�
                {
                    this.cmbDept.Focus();
                }
                else//��������3��,����Ҫ�����Ű��޶�,���ò��Ű�ҽԺ,����Ӳ���,�趨��ת˳��,�Լ��Ƿ�Ҫ¼�뿴��ҽ��
                {
                    this.cmbDept.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType type)
        {
            DataSet ds = new DataSet();

            ds = this.SchemaMgr.QueryDept(this.dtBookingDate.Value.Date,
                                        this.regMgr.GetDateTimeFromSysDateTime(), type);
            if (ds == null)
            {
                MessageBox.Show(this.SchemaMgr.Err, "��ʾ");
                return -1;
            }

            this.addDeptToDataSet(ds, type);

            return 0;
        }
        /// <summary>
        /// ��ר�ơ�ר�ҳ��������ӵ�DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="type"></param>
        private void addDeptToDataSet(DataSet ds, FS.HISFC.Models.Base.EnumSchemaType type)
        {
            dsItems.Tables[0].Rows.Clear();
            //DateTime current = this.regMgr.GetDateTimeFromSysDateTime() ;

            if (type == FS.HISFC.Models.Base.EnumSchemaType.Dept)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dsItems.Tables["Dept"].Rows.Add(new object[]
                        {
                            row[0],//���Ҵ���
                            row[1],//��������
                            row[10],//ƴ����
                            row[11],//�����
                            row[12],//�Զ�����
                            row[5],//�Һ��޶�
                            row[6],//�ѹҺ���
                            row[7],//ԤԼ�޶�
                            row[8],//��ԤԼ��
                            row[3],//��ʼʱ��
                            row[4],//����ʱ��
                            row[2],//���
                            FS.FrameWork.Function.NConvert.ToBoolean(row[9])
                        });
                }
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dsItems.Tables["Dept"].Rows.Add(new object[]
                        {
                            row[0],//���Ҵ���
                            row[1],//��������
                            row[2],//ƴ����
                            row[3],//�����
                            row[4],//�Զ�����
                            0,//�Һ��޶�
                            0,//�ѹҺ���
                            0,//ԤԼ�޶�
                            0,//��ԤԼ��
                            DateTime.MinValue,//��ʼʱ��
                            DateTime.MinValue,//����ʱ��
                            "",//���
                            false
                        });
                }
            }
        }
        #endregion

        #region dept
        /// <summary>
        /// ѡ��Һſ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsTriggerSelectedIndexChanged == false) return;

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            //���ԤԼ��Ϣ
            this.ClearBookingInfo();
            //���ҽ��
            this.cmbDoctor.Tag = "";

            //ר�ҡ�ר�ơ�����Ŷ���Ҫ���Ű��޶�
            FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if (regLevel == null)//û��ѡ��Һż���
            {
                MessageBox.Show("����ѡ��Һż���!", "��ʾ");
                this.cmbRegLevel.Focus();
                return;
            }

            //��ʾ�ÿ�����ҽ���б�
            if (regLevel.IsSpecial || regLevel.IsExpert)
            {
                this.GetDoctByDept(this.cmbDept.Tag.ToString(), true);
            }
            else
            {
                //˳�¸���ҽԺ���󣬹���ͨ��Ҳѡ��ҽ������ѡ����Һ�ֱ�Ӽ������ÿ��ҵ��Ű�ҽ�����ʺ�ѡ��ר�Һ�һ������  ygch {7DB89B62-4DC3-4827-9035-1B25B190A44D}
                //this.GetDoctByDept(this.cmbDept.Tag.ToString(), false);
                this.GetDoctByDept(this.cmbDept.Tag.ToString(), true);
            }

            if (regLevel.IsExpert || regLevel.IsSpecial || regLevel.IsFaculty)
            {
                //�趨һ����Ч�ľ���ʱ���
                this.SetDeptZone(this.cmbDept.Tag.ToString(), this.dtBookingDate.Value, regLevel);
            }
            else
            {
                //�趨Ĭ��ԤԼʱ���
                this.SetDefaultBookingTime(this.dtBookingDate.Value);
            }
        }
        /// <summary>
        /// �Һſ��һس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (regLevel == null)
                {
                    MessageBox.Show("����ѡ��Һż���!", "��ʾ");
                    this.cmbRegLevel.Focus();
                    return;
                }

                //û��ѡ�����,��ʾ���е�ҽ��
                if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
                {
                    if (regLevel.IsExpert || regLevel.IsSpecial)
                    {
                        this.GetDoct();//���ȫ������ҽ��
                    }
                    else
                    {
                        this.SetDoctFpStyle(false);
                        this.cmbDoctor.AddItems(this.alDoct);
                        this.AddDoctToDataSet(this.alDoct);
                        this.AddDoctToFp(false);
                    }
                    //�趨Ĭ��ԤԼʱ���
                    this.SetDefaultBookingTime(this.dtBookingDate.Value);
                }

                this.cmbDoctor.Tag = "";

                if (regLevel.IsFaculty)
                {
                    //��ȡ�����Ч��һ���Ű���Ϣ ��ҽ������ֲ����ˣ�������������
                    /*if(this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Dept,regLevel,
                            this.cmbDept.Tag.ToString(), "") == -1)
                        {
                            this.cmbDept.Focus() ;
                            return ;
                        }
                    }*/
                    if (this.cmbDept.Tag != null & this.cmbDept.Tag.ToString() != "")
                    {
                        if (this.DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType.Dept) == -1)
                        {
                            this.cmbDept.Focus();
                            return;
                        }
                    }
                    this.dtBookingDate.Focus();
                }
                else if (regLevel.IsSpecial || regLevel.IsExpert)
                {
                    this.cmbDoctor.Focus();
                }
                else//����ר�ҡ�ר�ơ�����Ų������뿴��ҽ���;���ʱ��,��Ȼ�����ò���Ҫ��������ҽ����
                {
                    //{920686B9-AD51-496e-9240-5A6DA098404E} ��������ά���Ƿ��������ҽ��
                    if (this.IsSetDoctFocusForCommon)
                    {
                        this.cmbDoctor.Focus();
                    }
                    else
                    {
                        if (this.cmbDept.Tag == null || string.IsNullOrEmpty(this.cmbDept.Tag.ToString()))
                        {
                            MessageBox.Show("��ѡ��Һſ��ң�");
                            return;
                        }
                        else
                        {
                            this.save();
                        }
                        //this.txtCardNo.Focus();
                    }
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
                return;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// ���ݿ��Ҵ����ѯ����ҽ��
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="IsDisplaySchema"></param>
        /// <returns></returns>
        private int GetDoctByDept(string deptID, bool IsDisplaySchema)
        {
            if (IsDisplaySchema)
            {
                DataSet ds;

                //ds = this.SchemaMgr.QueryDoct(this.dtBookingDate.Value,
                //                                this.regMgr.GetDateTimeFromSysDateTime(), deptID);
                string noon = this.getNoon(this.regMgr.GetDateTimeFromSysDateTime());
                ds = this.interMgr.QueryDoctSD(this.dtBookingDate.Value,
                                                this.regMgr.GetDateTimeFromSysDateTime(), deptID, this.dtBookingDate.Value.AddDays(1), noon);

                if (ds == null)
                {
                    MessageBox.Show(this.SchemaMgr.Err, "��ʾ");
                    return -1;
                }

                this.SetDoctFpStyle(true);
                this.AddDoctToDataSet(ds);
                //{920686B9-AD51-496e-9240-5A6DA098404E} ��������ά���Ƿ��������ҽ��
                //if (this.ComboxIsListAll)
                if (this.isAddAllDoct)
                {
                    this.cmbDoctor.AddItems(this.alDoct);
                }
                else
                {
                    this.AddDoctToCombox();
                }
            }
            else
            {
                //{920686B9-AD51-496e-9240-5A6DA098404E} ��������ά���Ƿ��������ҽ��
                if (this.isAddAllDoct)
                {
                    //this.cmbDoctor.AddItems(this.alDoct);
                    this.SetDoctFpStyle(false);
                    this.AddDoctToDataSet(this.alDoct);
                }
                else
                {
                    //al = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, deptID);
                    al = this.interMgr.GetEmployeeForArray(FS.HISFC.Models.Base.EnumEmployeeType.D, deptID, this.regMgr.GetDateTimeFromSysDateTime().Date);
                    if (al == null)
                    {
                        MessageBox.Show("��ȡ����ҽ��ʱ����!" + this.conMgr.Err, "��ʾ");
                        return -1;
                    }
                    this.cmbDoctor.AddItems(al);
                    this.SetDoctFpStyle(false);
                    this.AddDoctToDataSet(al);
                }
            }



            this.AddDoctToFp(IsDisplaySchema);

            return 0;
        }
        /// <summary>
        /// ��ȡ���ճ���ȫ��ҽ��
        /// </summary>
        /// <returns></returns>
        private int GetDoct()
        {
            DataSet ds;

            #region {3F42A388-0810-4387-A749-44A79EEE0247}
            //ds = this.SchemaMgr.QueryDoct(this.dtBookingDate.Value, this.regMgr.GetDateTimeFromSysDateTime());
            //ds = this.SdSchema.QueryDoct(this.dtBookingDate.Value, this.regMgr.GetDateTimeFromSysDateTime());
            //ygch ��ѯ����ҽ��ʱҲʹ���µ�sql��� �Ի�ȡ��������� {8BF9E56B-828E-4264-9D2F-B0B74FB920B5}
            string noon = this.getNoon(this.regMgr.GetDateTimeFromSysDateTime());
            ds = this.interMgr.QueryDoctSD(this.dtBookingDate.Value, this.regMgr.GetDateTimeFromSysDateTime(), "ALL", this.dtBookingDate.Value.AddDays(1), noon);
            #endregion

            if (ds == null)
            {
                MessageBox.Show(this.SchemaMgr.Err, "��ʾ");
                return -1;
            }

            this.SetDoctFpStyle(true);
            this.AddDoctToDataSet(ds);
            this.AddDoctToFp(true);
            //{920686B9-AD51-496e-9240-5A6DA098404E}
            //if (this.ComboxIsListAll)
            if (this.isAddAllDoct)
            {
                this.cmbDoctor.AddItems(this.alDoct);
            }
            else
            {
                this.AddDoctToCombox();
            }

            return 0;
        }

        /// <summary>
        /// �ҵ��ǽ��ں�or�����ں�
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool IsProfessor(FS.HISFC.Models.Registration.RegLevel level)
        {
            bool rtn = false;

            if (level.IsExpert || level.IsSpecial)
            {
                foreach (FS.HISFC.Models.Base.Const con in this.alProfessor)
                {
                    if (con.ID == level.ID)
                    {
                        return true;
                    }
                }
            }

            return rtn;
        }
        /// <summary>
        /// �趨����Ĭ�Ͼ���ʱ���
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="bookingDate"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private int SetDeptZone(string deptID, DateTime bookingDate, FS.HISFC.Models.Registration.RegLevel level)
        {
            FS.HISFC.Components.Registration.RegTypeNUM regType = FS.HISFC.Components.Registration.RegTypeNUM.Faculty;

            #region Set regType value
            regType = this.getRegType(level);
            #endregion

            this.ucChooseDate.QueryDeptBooking(bookingDate, deptID, regType);

            //Ĭ����ʾ��һ������������ʱ��δ���ڡ��޶�δ�������Ű���Ϣ
            FS.HISFC.Models.Registration.Schema schema = this.ucChooseDate.GetValidBooking(regType);

            if (schema == null)//û�з���������
            {
                this.SetDefaultBookingTime(bookingDate.Date);
            }
            else
            {
                this.SetBookingTime(schema);
            }

            return 0;
        }
        /// <summary>
        /// ���ݹҺż���ת��Ϊö��,�Һż������Ϊר�ҡ�ר�ơ�����
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private FS.HISFC.Components.Registration.RegTypeNUM getRegType(FS.HISFC.Models.Registration.RegLevel level)
        {
            FS.HISFC.Components.Registration.RegTypeNUM regType = FS.HISFC.Components.Registration.RegTypeNUM.Faculty;

            if (level.IsExpert)
            {
                regType = FS.HISFC.Components.Registration.RegTypeNUM.Expert;
            }
            else if (level.IsFaculty)
            {
                regType = FS.HISFC.Components.Registration.RegTypeNUM.Faculty;
            }
            else if (level.IsSpecial)
            {
                regType = FS.HISFC.Components.Registration.RegTypeNUM.Special;
            }

            return regType;
        }

        /// <summary>
        /// ��ȡ�������Ч�Ű���Ϣ
        /// </summary>
        /// <param name="schemaType"></param>
        /// <param name="regLevel"></param>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        private int getLastSchema(FS.HISFC.Models.Base.EnumSchemaType schemaType,
            FS.HISFC.Models.Registration.RegLevel regLevel, string deptID, string doctID)
        {
            FS.HISFC.Models.Registration.Schema schema = this.SchemaMgr.Query(schemaType,
                                                    this.regMgr.GetDateTimeFromSysDateTime(), deptID, doctID);
            if (schema == null)
            {
                //����
                MessageBox.Show("��ȡ����Ű���Ϣ����!" + this.SchemaMgr.Err, "��ʾ");
                return -1;
            }


            if (schema.Templet.ID == "")
            {
                MessageBox.Show("û����Ч���Ű��¼!", "��ʾ");
                return -1;
            }

            this.IsTriggerSelectedIndexChanged = false;
            this.cmbDept.Tag = schema.Templet.Dept.ID;
            this.IsTriggerSelectedIndexChanged = true;

            this.SetBookingDate(schema.SeeDate);
            this.SetBookingTime(schema);

            return 0;
        }

        /// <summary>
        /// ��ʾҽ��һ�ܳ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private int DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType schemaType)
        {
            this.lbTip.Text = "";

            //����û�г���ҽ��
            if (this.dtBookingDate.Tag == null)
            {
                DateTime current = this.dtBookingDate.Value.Date;

                DateTime end = current.AddDays(6 - (int)current.DayOfWeek);

                //��дҵ����ˣ����췳
                //string sql = "SELECT distinct week FROM fin_opr_schema WHERE " +
                //    " see_date>to_date('" + current.ToString() + "','yyyy-mm-dd hh24:mi:ss') AND " +
                //    " see_date<=to_date('" + end.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";

                DataSet ds = new DataSet();

                if (schemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
                {
                    //sql = sql + " AND schema_type = '0' AND dept_code = '" + this.cmbDept.Tag.ToString() + "'" +
                    //    " AND valid_flag = '1' ";
                    ds = this.SchemaMgr.QuerySchemaForRegister(current.ToString(), end.ToString(), "0", this.cmbDept.Tag.ToString(), "A");
                    if (ds == null)
                    {
                        MessageBox.Show(this.SchemaMgr.Err);
                        return -1;
                    }
                }
                else
                {
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        //sql = sql + " AND schema_type = '1' AND doct_code = '" + this.cmbDoctor.Tag.ToString() + "'" +
                        //    " AND dept_code = '" + this.cmbDept.Tag.ToString() + "' AND valid_flag = '1' ";
                        ds = this.SchemaMgr.QuerySchemaForRegister(current.ToString(), end.ToString(), "1", this.cmbDept.Tag.ToString(), this.cmbDoctor.Tag.ToString());
                        if (ds == null)
                        {
                            MessageBox.Show(this.SchemaMgr.Err);
                            return -1;
                        }
                    }
                    else
                    {
                        //sql = sql + " AND schema_type = '1' AND doct_code = '" + this.cmbDoctor.Tag.ToString() + "'" +
                        //    " AND valid_flag = '1' ";
                        ds = this.SchemaMgr.QuerySchemaForRegister(current.ToString(), end.ToString(), "1", "A", this.cmbDoctor.Tag.ToString());
                        if (ds == null)
                        {
                            MessageBox.Show(this.SchemaMgr.Err);
                            return -1;
                        }
                    }
                }

                //DataSet ds = new DataSet();

                //if (this.SchemaMgr.ExecQuery(sql, ref ds) == -1)
                //{
                //    MessageBox.Show("��ȡ�Ű���Ϣ�����!" + this.SchemaMgr.Err, "��ʾ");
                //    return -1;
                //}

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    if (schemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
                    {
                        if (this.fpDept.RowCount == 0)
                        {
                            this.lbTip.Text = "��ר��һ���޳���";
                            MessageBox.Show("��������Ч�Ű��¼!", "��ʾ");
                            return -1;
                        }
                        else
                        {
                            this.lbTip.Text = "�����ѹ���,һ���޳���";
                            return 0;
                        }
                    }
                    else
                    {
                        if (this.fpDoctor.RowCount == 0)
                        {
                            this.lbTip.Text = "��ҽ��һ��δ�Ű�";
                            MessageBox.Show("��������Ч�Ű��¼!", "��ʾ");
                            return -1;
                        }
                        else
                        {
                            this.lbTip.Text = "�����ѹ���,һ���޳���";
                            return 0;
                        }
                    }
                }

                string[] week = new string[] { "��", "һ", "��", "��", "��", "��", "��" };
                string tip = "��";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tip = tip + week[FS.FrameWork.Function.NConvert.ToInt32(row[0])] + "��";
                }
                this.lbTip.Text = tip.Substring(0, tip.Length - 1) + "����";

                //MessageBox.Show("��������Ч�Ű��¼!","��ʾ") ;

                return 0;
            }

            return 0;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_TextChanged(object sender, System.EventArgs e)
        {
            string strFilter = "ID like '%" + this.cmbDept.Text + "%' or Spell_Code like '%" + this.cmbDept.Text + "%'"
                    + " or Name like '%" + this.cmbDept.Text + "%'";
            /* or Wb_Code like '%"+this.cmbDept.Text
            +"%' or Input_Code like '%"+this.cmbDept.Text+"%'";*/

            try
            {
                dvDepts.RowFilter = strFilter;
            }
            catch { }

            this.addRegDeptToFp(FS.FrameWork.Function.NConvert.ToBoolean(this.fpDept.Tag));
        }
        #endregion

        #region doctor
        /// <summary>
        /// ѡ��ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {


            //ѡ��һ�������������Ű���Ϣ��ΪԤԼʱ��
            if (this.IsTriggerSelectedIndexChanged == false) return;

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
            //���ԤԼ��Ϣ
            this.ClearBookingInfo();

            //ר�ҡ�ר�ơ�����Ŷ���Ҫ���Ű��޶�
            FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if (regLevel == null)//û��ѡ��Һż���
            {
                MessageBox.Show("����ѡ��Һż���!", "��ʾ");
                this.cmbRegLevel.Focus();
                return;
            }

            if (regLevel.IsExpert || regLevel.IsSpecial)
            {
                //�趨һ����Ч�ľ���ʱ���
                this.SetDoctZone(this.cmbDoctor.Tag.ToString(), this.dtBookingDate.Value, regLevel);
            }
            else if (regLevel.IsFaculty) { }
            else
            {
                //�趨Ĭ��ԤԼʱ���
                this.SetDefaultBookingTime(this.dtBookingDate.Value);
            }

            if (this.chSpecialPerson.Checked && this.cmbDoctor.Tag != "")
            {
                this.SetSpecialPerson();
            }
        }

        /// <summary>
        /// �趨ר��Ĭ�Ͼ���ʱ���
        /// </summary>
        /// <param name="doctID"></param>
        /// <param name="bookingDate"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private int SetDoctZone(string doctID, DateTime bookingDate, FS.HISFC.Models.Registration.RegLevel level)
        {
            FS.HISFC.Components.Registration.RegTypeNUM regType = FS.HISFC.Components.Registration.RegTypeNUM.Faculty;

            #region Set regType value
            regType = this.getRegType(level);
            #endregion

            if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
            {
                this.ucChooseDate.QueryDoctBooking(bookingDate, doctID, this.cmbDept.Tag.ToString(), regType);
            }
            else
            {
                this.ucChooseDate.QueryDoctBooking(bookingDate, doctID, regType);
            }

            //Ĭ����ʾ��һ������������ʱ��δ���ڡ��޶�δ�������Ű���Ϣ
            FS.HISFC.Models.Registration.Schema schema = this.ucChooseDate.GetValidBooking(regType);

            if (schema == null)//û�з���������
            {
                this.SetDefaultBookingTime(bookingDate.Date);
            }
            else
            {
                this.IsTriggerSelectedIndexChanged = false;
                this.cmbDept.Tag = schema.Templet.Dept.ID;
                this.IsTriggerSelectedIndexChanged = true;

                this.SetBookingTime(schema);
            }

            return 0;
        }

        /// <summary>
        /// ҽ���س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (regLevel == null)
                {
                    MessageBox.Show("����ѡ��Һż���!", "��ʾ");
                    this.cmbRegLevel.Focus();
                    return;
                }
                //��Ϊ��������ԤԼ��,���Բ����Ʊ�������ҽ��
                //				if((regLevel.IsExpert || regLevel.IsSpecial)&&(this.cmbDoctor.Tag == null||this.cmbDoctor.Tag.ToString() == ""))
                //				{
                //					MessageBox.Show("ר�Һű���ָ������ҽ��!","��ʾ") ;
                //					this.cmbDoctor.Focus();
                //					return ;
                //				}

                //�����ѡ��ҽ��������ֱ�ӹҺţ� ���������ҽ���������������һЩ�����������Ҫ��֤����Ķ������б��е�ĳ��ҽ�� {9EA8A9C5-1F2F-4c65-A2B9-1C158E2CFB0C}  ygch
                if (this.cmbDoctor.Text != "" && this.cmbDoctor.Text != string.Empty)
                {
                    if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag == "")
                    {
                        MessageBox.Show("ҽ����Ϣ����������������");
                        return;
                    }
                }

                if (regLevel.IsFaculty)
                {
                    //��ȡ�����Ч��һ���Ű���Ϣ
                    #region
                    /*if(this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Dept,regLevel,
                            this.cmbDept.Tag.ToString(), "") == -1)
                        {
                            this.cmbDept.Focus() ;
                            return ;
                        }
                    }*/

                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        if (this.DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType.Dept) == -1)
                        {
                            this.cmbDept.Focus();
                            return;
                        }
                    }

                    this.dtBookingDate.Focus();
                    #endregion
                }
                else if (regLevel.IsExpert)
                {
                    //��ȡ�����Ч��һ���Ű���Ϣ
                    #region
                    /*if(this.cmbDoctor.Tag != null && this.cmbDoctor.Tag.ToString() != "")
                    {
                        if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Doct,regLevel,
                            "",this.cmbDoctor.Tag.ToString()) == -1)
                        {
                            this.cmbDoctor.Focus() ;
                            return ;
                        }
                    }*/

                    if (this.cmbDoctor.Tag != null && this.cmbDoctor.Tag.ToString() != "")
                    {
                        ///�жϽ��ں�¼��Һż����Ƿ���ȷ
                        ///

                        //						if(!this.VerifyIsProfessor(regLevel,(FS.HISFC.Models.RADT.Person)this.cmbDoctor.SelectedItem))
                        //						{
                        //							this.cmbDoctor.Focus() ;
                        //							return ;
                        //						}

                        FS.HISFC.Models.Registration.Schema schema = (FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag;
                        if (schema != null)
                        {
                            if (this.VerifyIsProfessor(regLevel, schema) == false)
                            {
                                this.cmbDoctor.Focus();
                                return;
                            }
                        }

                        if (this.DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType.Doct) == -1)
                        {
                            this.cmbDoctor.Focus();
                            return;
                        }
                    }

                    #endregion
                    if (this.IsInputOrder)
                    {
                        this.txtOrder.Focus();
                    }
                    else
                    {
                        if (this.cmbDoctor.Tag == null || string.IsNullOrEmpty(cmbDoctor.Tag.ToString()))
                        {
                            MessageBox.Show("��ѡ����ڣ�");
                            return;
                        }
                        else
                        {
                            this.save();
                        }
                        //this.dtBookingDate.Focus();
                    }
                }
                else if (regLevel.IsSpecial)
                {
                    //��ȡ�����Ч��һ���Ű���Ϣ
                    #region
                    /*if(this.cmbDoctor.Tag != null && this.cmbDoctor.Tag.ToString() != "")
                    {
                        if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Doct,regLevel,
                            "",this.cmbDoctor.Tag.ToString()) == -1)
                        {
                            this.cmbDoctor.Focus() ;
                            return ;
                        }
                    }*/
                    if (this.cmbDoctor.Tag != null && this.cmbDoctor.Tag.ToString() != "")
                    {
                        if (this.DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType.Doct) == -1)
                        {
                            this.cmbDoctor.Focus();
                            return;
                        }
                    }
                    #endregion
                    this.dtBookingDate.Focus();
                }
                else
                {
                    this.save();
                    //this.txtCardNo.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_TextChanged(object sender, EventArgs e)
        {
            string strFilter = "ID like '%" + this.cmbDoctor.Text + "%' or Spell_Code like '%" + this.cmbDoctor.Text + "%'"
                    + " or Name like '%" + this.cmbDoctor.Text + "%'";
            /* or Wb_Code like '%"+this.cmbDept.Text
            +"%' or Input_Code like '%"+this.cmbDept.Text+"%'";*/

            try
            {
                dvDocts.RowFilter = strFilter;
            }
            catch { }

            this.AddDoctToFp(FS.FrameWork.Function.NConvert.ToBoolean(this.fpDoctor.Tag));
        }

        /// <summary>
        /// ��֤���ںŹҵ��Ƿ��ǽ��ڣ������ںŹҵ��Ƿ��Ǹ�����
        /// </summary>
        /// <param name="level"></param>
        /// <param name="doct"></param>
        /// <returns></returns>
        private bool VerifyIsProfessor(FS.HISFC.Models.Registration.RegLevel level, FS.HISFC.Models.Base.Employee doct)
        {
            if (this.IsDivLevel)
            {
                if (!level.IsSpecial)//����Ų����ж�
                {
                    if (this.IsProfessor(level))//���ں�
                    {
                        if (!doct.IsExpert)
                        {
                            MessageBox.Show("��ҽ���Ǹ�����,���ܹҽ��ں�!", "��ʾ");
                            return false;
                        }
                    }
                    else//������
                    {
                        if (doct.IsExpert)
                        {
                            MessageBox.Show("��ҽ���ǽ���,���ܹҸ����ں�!", "��ʾ");
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool VerifyIsProfessor(FS.HISFC.Models.Registration.RegLevel level, string doctID)
        {
            if (this.IsDivLevel)
            {
                if (!level.IsSpecial)//����Ų����ж�
                {
                    FS.HISFC.Models.Base.Employee p = this.conMgr.GetEmployeeInfo(doctID);
                    if (p == null)
                    {
                        MessageBox.Show("��ȡ��Ա��Ϣ����!" + this.conMgr.Err, "��ʾ");
                        return false;
                    }

                    if (this.IsProfessor(level))//���ں�
                    {
                        if (!(p.Level.ID == "2" || p.Level.ID == "21" || p.Level.ID == "17" || p.Level.ID == "33"))
                        {
                            MessageBox.Show("��ҽ���Ǹ�����,���ܹҽ��ں�!", "��ʾ");
                            return false;
                        }
                    }
                    else//������
                    {
                        if (p.Level.ID == "2" || p.Level.ID == "21" || p.Level.ID == "17" || p.Level.ID == "33")
                        {
                            MessageBox.Show("��ҽ���ǽ���,���ܹҸ����ں�!", "��ʾ");
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        private bool VerifyIsProfessor(FS.HISFC.Models.Registration.RegLevel level, FS.HISFC.Models.Registration.Schema schema)
        {
            if (this.IsDivLevel)
            {
                if (schema.Templet.RegLevel.ID != null && schema.Templet.RegLevel.ID != "" &&
                    level.ID != schema.Templet.RegLevel.ID)
                {
                    MessageBox.Show(schema.Templet.Doct.Name + "ҽ���Ű༶��Ϊ:" + schema.Templet.RegLevel.Name + ",���ܹ�:" +
                        level.Name + ",���޸�!", "��ʾ");
                    return false;
                }
            }

            return true;
        }

        private bool VerifyIsProfessor(FS.HISFC.Models.Registration.RegLevel level, FS.HISFC.Models.Registration.Booking booking)
        {
            FS.HISFC.Models.Registration.Schema schema = this.SchemaMgr.GetByID(booking.DoctorInfo.Templet.ID);

            if (schema == null || schema.Templet.ID == "")
            {
                MessageBox.Show("�޴���Ϊ:" + schema.Templet.ID + "���Ű���Ϣ!", "��ʾ");
                return false;
            }

            if (this.VerifyIsProfessor(level, schema) == false) return false;

            return true;
        }
        #endregion

        #region Set booking zone
        /// <summary>
        /// ���ԤԼ��ˮ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_TextChanged(object sender, EventArgs e)
        {
            this.txtOrder.Tag = null;
        }
        /// <summary>
        /// ԤԼ��ˮ�Żس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string ID = this.txtOrder.Text.Trim();

                if (ID != "")
                {
                    FS.HISFC.Models.Registration.Booking booking = this.bookingMgr.GetByID(ID);

                    FS.HISFC.Models.Registration.RegLevel Level = null;
                    if (booking.DoctorInfo.Templet.RegLevel.ID == null || booking.DoctorInfo.Templet.RegLevel.ID == string.Empty)
                    {
                        //{6F15CA5C-3610-4c29-B4B0-DBFA5EB39A4F}
                        //{0B4C5A74-98EB-4adc-9E52-47295201EB97}
                        if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
                        {
                            MessageBox.Show("��ѡ��Һż���!", "��ʾ");
                            this.cmbRegLevel.Focus();
                            return;
                        }

                        //�ж���ר�Һ�,������ҽ����
                        Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                        if (!(Level.IsSpecial || Level.IsExpert || Level.IsFaculty))
                        {
                            MessageBox.Show("ԤԼ�ű�����ר��/ר�ƺ�!", "��ʾ");
                            this.txtOrder.Text = "";
                            this.cmbRegLevel.Focus();
                            return;
                        }
                    }
                    else
                    {



                        //add by niuxinyuan

                        //{0B4C5A74-98EB-4adc-9E52-47295201EB97}

                        this.cmbRegLevel.SelectedValueChanged -= new EventHandler(cmbRegLevel_SelectedIndexChanged);
                        this.cmbRegLevel.Tag = booking.DoctorInfo.Templet.RegLevel.ID;
                        this.cmbRegLevel.SelectedValueChanged += new EventHandler(cmbRegLevel_SelectedIndexChanged);
                        Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                    }


                    if (booking == null)
                    {
                        MessageBox.Show("��ȡԤԼ�Һ���Ϣ����!" + this.bookingMgr.Err, "��ʾ");
                        this.txtOrder.Focus();
                        return;
                    }

                    if (booking.ID == null || booking.ID == "")
                    {
                        MessageBox.Show("û����ˮ��Ϊ:" + ID + "��ԤԼ��Ϣ!", "��ʾ");
                        this.txtOrder.Focus();
                        return;
                    }

                    if (booking.IsSee)
                    {
                        MessageBox.Show("��ԤԼ��Ϣ�ѿ���,��ѡ������ԤԼ��Ϣ!", "��ʾ");
                        this.txtOrder.Focus();
                        return;
                    }

                    if (Level.IsExpert && (booking.DoctorInfo.Templet.Doct.ID == null || booking.DoctorInfo.Templet.Doct.ID == ""))
                    {
                        MessageBox.Show("��ԤԼ��ϢΪר�ƺ�,���ܹ�ר�Һ�!", "��ʾ");
                        this.cmbRegLevel.Focus();
                        return;
                    }

                    if (!Level.IsExpert && booking.DoctorInfo.Templet.Doct.ID != null && booking.DoctorInfo.Templet.Doct.ID != "")
                    {
                        MessageBox.Show("��ԤԼ��ϢΪר�Һ�,���ܹ�ר�ƺ�!", "��ʾ");
                        this.cmbRegLevel.Focus();
                        return;
                    }

                    if (this.IsInputTime)//��ɽ�����ж��Ƿ�ʱ
                    {
                        if (!booking.DoctorInfo.Templet.IsAppend)
                        {
                            DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime();

                            if (booking.DoctorInfo.Templet.End < current)
                            {
                                MessageBox.Show("��ԤԼ��Ϣ�Ѿ�����,����ʹ��!", "��ʾ");
                                this.txtOrder.Focus();
                                return;
                            }

                            if (booking.DoctorInfo.Templet.Begin > current)
                            {
                                DialogResult dr = MessageBox.Show("��û�е�ԤԼʱ��,�Ƿ����?", "��ʾ",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                if (dr == DialogResult.No)
                                {
                                    this.txtOrder.Focus();
                                    return;
                                }
                            }

                            if (booking.DoctorInfo.Templet.Begin < current &&
                                booking.DoctorInfo.Templet.End > current)
                            {
                                DialogResult dr = MessageBox.Show("�Ѿ�����ԤԼ��ʼʱ��,�Ƿ����?", "��ʾ",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                if (dr == DialogResult.No)
                                {
                                    this.txtOrder.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (booking.DoctorInfo.SeeDate.Date < this.bookingMgr.GetDateTimeFromSysDateTime().Date)
                            {
                                MessageBox.Show("��ԤԼ��Ϣ�Ѿ�����,����ʹ��!", "��ʾ");
                                this.txtOrder.Focus();
                                return;
                            }
                        }
                    }
                    //��ֵ					
                    this.IsTriggerSelectedIndexChanged = false;
                    this.cmbDept.AddItems(this.alDept);
                    this.cmbDept.Tag = booking.DoctorInfo.Templet.Dept.ID;//ԤԼ����	

                    this.cmbDoctor.AddItems(this.alDoct);
                    this.AddDoctToDataSet(this.alDoct);
                    this.AddDoctToFp(false);
                    this.cmbDoctor.Tag = booking.DoctorInfo.Templet.Doct.ID;//ԤԼҽ��



                    this.IsTriggerSelectedIndexChanged = true;

                    ///�жϽ��ں�¼���Ƿ���ȷ
                    ///
                    if (Level.IsExpert)
                    {
                        if (this.VerifyIsProfessor(Level, booking) == false)
                        {
                            this.cmbRegLevel.Focus();
                            return;
                        }
                    }

                    this.dtBookingDate.Value = booking.DoctorInfo.SeeDate;
                    this.dtBegin.Value = booking.DoctorInfo.Templet.Begin;
                    this.dtEnd.Value = booking.DoctorInfo.Templet.End;
                    this.txtOrder.Text = ID;//Text��Tag˳���ܵߵ�
                    this.txtOrder.Tag = booking;

                    this.txtCardNo.Text = booking.PID.CardNO;
                    this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
                else
                {
                    this.dtBookingDate.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBookingDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
            this.SetBookingTag(null);
            //���ԤԼ��Ϣ
            this.ClearBookingInfo();

            this.lbWeek.Text = this.getWeek(this.dtBookingDate.Value);
        }
        /// <summary>
        /// ԤԼ���ڻس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBookingDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dtBookingDate.Value.Date < this.regMgr.GetDateTimeFromSysDateTime().Date)
                {
                    MessageBox.Show("ԤԼ���ڲ���С�ڵ�ǰ����", "��ʾ");
                    this.dtBookingDate.Focus();
                    return;
                }

                if (this.IsInputTime)
                {
                    this.dtBegin.Focus();
                }
                else
                {
                    this.txtCardNo.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.llPd_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        /// <summary>
        /// �����ʼʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {
            //���ԤԼ��Ϣ
            this.ClearBookingInfo();
            this.SetBookingTag(null);
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }

        /// <summary>
        /// ��ʼʱ��س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtEnd.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.llPd_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            //���ԤԼ��Ϣ
            this.ClearBookingInfo();
            this.SetBookingTag(null);
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }

        /// <summary>
        /// ����ʱ��س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dtBookingDate.Tag == null)
                {
                    FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                    if (level != null)
                    {
                        FS.HISFC.Models.Registration.Schema schema = this.GetValidSchema(level);

                        this.SetBookingTag(schema);
                    }
                }

                this.txtCardNo.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.llPd_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
        }
        /// <summary>
        /// ѡ��ԤԼʱ���
        /// </summary>
        /// <param name="sender"></param>
        private void ucChooseDate_SelectedItem(FS.HISFC.Models.Registration.Schema sender)
        {
            this.ucChooseDate.Visible = false;

            if (sender == null) return;

            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if (level == null)
            {
                MessageBox.Show("����ѡ��Һż���!", "��ʾ");
                this.cmbRegLevel.Focus();
                return;
            }

            if (!level.IsSpecial && !level.IsExpert && !level.IsFaculty) return;

            FS.HISFC.Components.Registration.RegTypeNUM regType = this.getRegType(level);

            #region ���Σ����һ���ж��Ƿ񳬳��޶�
            /* 
            if((regType == Registration.RegTypeNUM.Faculty ||regType == Registration.RegTypeNUM.Expert)
                &&sender.Templet.RegLmt<=sender.RegedQty)
            {
                if(MessageBox.Show("�ֳ��Һ����Ѿ����������,�Ƿ����?","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    this.dtBookingDate.Focus() ;
                    return ;
                }
            }

            if(regType == Registration.RegTypeNUM.Special &&sender.Templet.SpeLmt<=sender.SpeReged)
            {
                if(MessageBox.Show("����Һ����Ѿ����������,�Ƿ����?","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    this.dtBookingDate.Focus() ;
                    return ;
                }
            }*/
            #endregion

            //ר�ҡ�ר�ƺ��ֳ��ѹҺ��������ֳ������
            if ((((regType == FS.HISFC.Components.Registration.RegTypeNUM.Faculty || regType == FS.HISFC.Components.Registration.RegTypeNUM.Expert) && sender.Templet.RegQuota <= sender.RegedQTY) ||
                //��������š������ѹҺ����������������
                (regType == FS.HISFC.Components.Registration.RegTypeNUM.Special && sender.Templet.SpeQuota <= sender.SpedQTY)) &&
                //���Ҳ��ǼӺ�
                !sender.Templet.IsAppend)
            {
                if (!this.IsAllowOverrun)
                {
                    MessageBox.Show("�Ű೬�޶����Һ�!", "��ʾ");
                    this.dtBookingDate.Focus();
                    return;
                }
            }

            //����
            this.IsTriggerSelectedIndexChanged = false;
            this.cmbDept.Tag = sender.Templet.Dept.ID;
            //ҽ��
            if (sender.Templet.Doct.ID == "None")//ר�ƺ�
            {
                this.cmbDoctor.Tag = "";
            }
            else
            {
                this.cmbDoctor.Tag = sender.Templet.Doct.ID;
            }
            this.IsTriggerSelectedIndexChanged = true;

            //ԤԼʱ���
            this.SetBookingTime(sender);
            this.txtCardNo.Focus();
        }
        /// <summary>
        /// ��ʾԤԼʱ����б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llPd_Click(object sender, EventArgs e)
        {
            if (this.ucChooseDate.Visible)
            {
                this.ucChooseDate.Visible = false;
                this.dtBookingDate.Focus();
            }
            else
            {
                DateTime bookingDate = this.dtBookingDate.Value;
                DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime();

                if (bookingDate.Date < current.Date)
                {
                    MessageBox.Show("ԤԼ���ڲ���С�ڵ�ǰ����!", "��ʾ");
                    this.dtBookingDate.Focus();
                    return;
                }

                FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (level == null)
                {
                    MessageBox.Show("����ѡ��Һż���!", "��ʾ");
                    this.cmbRegLevel.Focus();
                    return;
                }

                if (!level.IsFaculty && !level.IsExpert && !level.IsSpecial) return;

                string deptID = this.cmbDept.Tag.ToString();
                string doctID = this.cmbDoctor.Tag.ToString();

                //ר�ƺ�,�Һſ��Ҳ���Ϊ��
                if (level.IsFaculty)
                {
                    #region dept
                    if (deptID == null || deptID == "")
                    {
                        MessageBox.Show("ר�ƺű���ָ���Һſ���!", "��ʾ");
                        this.cmbDept.Focus();
                        return;
                    }

                    this.SetDeptZone(deptID, bookingDate, level);

                    if (this.ucChooseDate.Count > 0)
                    {
                        this.ucChooseDate.Visible = true;
                        this.ucChooseDate.Focus();
                    }
                    else if (this.ucChooseDate.Bookings.Count > 0)
                    {
                        MessageBox.Show("û�з����������Ű���Ϣ,������ѡ��ԤԼ����", "��ʾ");
                        this.dtBookingDate.Focus();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("ר��û���Ű�!", "��ʾ");
                        this.dtBookingDate.Focus();
                        return;
                    }
                    #endregion
                }
                //ר�Һ�,����ָ������ҽ��
                if (level.IsExpert || level.IsSpecial)
                {
                    #region doct
                    if (doctID == null || doctID == "")
                    {
                        MessageBox.Show("ר�Һű���ָ������ר��!", "��ʾ");
                        this.cmbDoctor.Focus();
                        return;
                    }

                    this.SetDoctZone(doctID, bookingDate, level);

                    if (this.ucChooseDate.Count > 0)
                    {
                        this.ucChooseDate.Visible = true;
                        this.ucChooseDate.Focus();
                    }
                    else if (this.ucChooseDate.Bookings.Count > 0)
                    {
                        MessageBox.Show("û�з����������Ű���Ϣ,������ѡ��ԤԼ����", "��ʾ");
                        this.dtBookingDate.Focus();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("ר��û���Ű�!", "��ʾ");
                        this.dtBookingDate.Focus();
                        return;
                    }
                    #endregion
                }
            }
        }

        #endregion

        #region txtCardNo
        /// <summary>
        /// �����Żس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text.Trim();
                if (cardNo == "")
                {
                    //MessageBox.Show("�����Ų���Ϊ��!","��ʾ");
                    //this.txtCardNo.Focus();
                    //return ;
                    //ֱ������������,�ɸ����������������������Ϣ
                    this.txtName.Focus();
                    return;
                }

                if (this.ValidCardNO(cardNo) < 0)
                {
                    this.txtCardNo.Focus();
                    return;
                }

                this.clear();
                this.SetDefaultCardType();

                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                if (this.isReadCard == true) //|| this.myYBregObj.SIMainInfo.Memo != null || this.myYBregObj.SIMainInfo.Memo.Trim() != "")
                {
                    //ҽ������
                    //this.regObj = this.getRegInfo(cardNo);
                    this.regObj.PID.CardNO = cardNo;
                    #region ������¼�뺺��
                    if (FS.FrameWork.Public.String.ValidMaxLengh(cardNo, 10) == false)
                    {
                        MessageBox.Show("�����Ų������뺺��!", "��ʾ");
                        this.txtCardNo.Focus();
                        return;
                    }
                    #endregion
                    this.txtPhone.Focus();
                }
                else
                {
                    //�������ҿ���¼ʱ�ĹҺű��
                    accountCard.Memo = "1";
                    if (this.feeMgr.ValidMarkNO(cardNo, ref accountCard) > 0)
                    {
                        cardNo = accountCard.Patient.PID.CardNO;
                        decimal vacancy = 0m;
                        if (feeMgr.GetAccountVacancy(cardNo, ref vacancy) > 0)
                        {
                            this.cmbCardType.Enabled = false;
                            this.txtIdNO.Enabled = false;
                            this.tbSIBalanceCost.Text = vacancy.ToString();
                        }
                        else
                        {
                            this.tbSIBalanceCost.Text = string.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show(feeMgr.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                        //string cardNOTemp = cardNo;
                        //cardNo = cardNo.PadLeft(10, '0');
                        //this.txtCardNo.Text = cardNo;
                        //this.cmbCardType.Enabled = true;
                        //this.txtIdNO.Enabled = true;
                        //this.tbSIBalanceCost.Text = string.Empty;
                    }

                    cardNo = cardNo.PadLeft(10, '0');
                    #region ������¼�뺺��
                    if (FS.FrameWork.Public.String.ValidMaxLengh(cardNo, 10) == false)
                    {
                        MessageBox.Show("�����Ų������뺺��!", "��ʾ");
                        this.txtCardNo.Focus();
                        return;
                    }
                    #endregion

                    #region ����������Ϣ
                    this.regObj = this.getRegInfo(cardNo);
                    if (regObj == null)
                    {
                        this.txtCardNo.Focus();
                        return;
                    }
                    #endregion

                    #region ��ֵ
                    this.txtCardNo.Text = cardNo;
                    this.txtName.Text = this.regObj.Name;
                    this.cmbSex.Tag = this.regObj.Sex.ID;
                    this.cmbPayKind.Tag = this.regObj.Pact.ID;
                    this.txtMcardNo.Text = this.regObj.SSN;
                    this.txtPhone.Text = this.regObj.PhoneHome;
                    this.txtAddress.Text = this.regObj.AddressHome;
                    this.cmbCountry.Tag = this.regObj.Country.ID;
                    //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}
                    this.txtIdNO.Text = this.regObj.IDCard;
                    if (this.regObj.Birthday != DateTime.MinValue)
                        this.dtBirthday.Value = this.regObj.Birthday;
                    if (this.regObj.CardType.ID == "")
                    {
                        this.SetDefaultCardType();
                    }
                    else
                    {
                        this.cmbCardType.Tag = this.regObj.CardType.ID;
                    }

                    //this.setAgee(this.regObj.Birthday);
                    this.getCost();

                    FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
                    this.getPayRate(pact);

                    #endregion

                }

                ucShow.CardNO = this.txtCardNo.Text;
                ucShow.operType = "1";//ֱ������
                if (ucShow.PersonCount > 0)
                {
                    fPopWin.Show();
                    fPopWin.Hide();
                    fPopWin.Location = ((Control)this).PointToScreen(new Point(this.txtCardNo.Location.X, this.txtCardNo.Location.Y + this.txtCardNo.Height));
                    fPopWin.ShowDialog();
                }

                if (this.IsInputName) this.txtName.Focus();
                else { this.cmbSex.Focus(); }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.Space)
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                if (FS.HISFC.Components.Common.Classes.Function.QueryComPatientInfo(ref p) == 1)
                {
                    this.txtCardNo.Text = p.PID.CardNO;
                    this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
                }
            }
        }

        /// <summary>
        /// ֻ������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {
            //if (!Regex.IsMatch(this.txtCardNo.Text, @"^\d*$"))
            //{
            //    this.txtCardNo.Text = this.txtCardNo.Text.Remove(this.txtCardNo.Text.Length - 1);
            //}
            //this.txtCardNo.SelectionStart = this.txtCardNo.Text.Length;
        }

        /// <summary>
        /// ��ʼ���໼�ߵ�������
        /// </summary>
        protected virtual void InitPopShowPatient()
        {
            fPopWin.Width = ucShow.Width + 10;
            fPopWin.MinimizeBox = false;
            fPopWin.MaximizeBox = false;
            fPopWin.Controls.Add(ucShow);
            ucShow.Dock = DockStyle.Fill;
            fPopWin.Height = 200;
            fPopWin.Visible = false;
        }

        /// <summary>
        /// ���ݲ����Ż�û��߹Һ���Ϣ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register getRegInfo(string CardNo)
        {
            FS.HISFC.Models.Registration.Register ObjReg = new FS.HISFC.Models.Registration.Register();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo p;
            int regCount = this.regMgr.QueryRegiterByCardNO(CardNo);


            if (regCount == 1)
            {
                ObjReg.IsFirst = false;
            }
            else
            {
                if (regCount == 0)
                {
                    ObjReg.IsFirst = true;

                }
                else
                {
                    return null;
                }
            }
            //�ȼ������߻�����Ϣ��,���Ƿ���ڸû�����Ϣ
            p = radt.QueryComPatientInfo(CardNo);





            if (p == null || p.Name == "")
            {
                //�����ڻ�����Ϣ
                ObjReg.PID.CardNO = CardNo;
                //ObjReg.IsFirst = true;
                ObjReg.Sex.ID = "M";
                ObjReg.Pact.ID = this.DefaultPactID;
            }
            else
            {
                //���ڻ��߻�����Ϣ,ȡ������Ϣ

                ObjReg.PID.CardNO = CardNo;
                ObjReg.Name = p.Name;
                ObjReg.Sex.ID = p.Sex.ID;
                ObjReg.Birthday = p.Birthday;
                ObjReg.Pact.ID = p.Pact.ID;
                ObjReg.Pact.PayKind.ID = p.Pact.PayKind.ID;
                ObjReg.SSN = p.SSN;
                ObjReg.PhoneHome = p.PhoneHome;
                ObjReg.AddressHome = p.AddressHome;
                ObjReg.IDCard = p.IDCard;
                ObjReg.NormalName = p.NormalName;
                ObjReg.IsEncrypt = p.IsEncrypt;
                //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}
                ObjReg.IDCard = p.IDCard;

                if (p.IsEncrypt == true)
                {
                    ObjReg.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(p.NormalName);
                }
                this.chbEncrpt.Checked = p.IsEncrypt;
                //ObjReg.IsFirst = false;

                if (this.validCardType(p.IDCardType.ID))//����Memo�洢֤�����
                {
                    ObjReg.CardType.ID = p.IDCardType.ID;

                }
            }

            return ObjReg;
        }

        /// <summary>
        /// ��֤֤������Ƿ���Ч
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        private bool validCardType(string cardType)
        {
            bool found = false;

            foreach (FS.FrameWork.Models.NeuObject obj in this.cmbCardType.alItems)
            {
                if (obj.ID == cardType)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
        /// <summary>
        /// ��������ԤԼ��Ϣ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register getBookingInfo(string CardNo)
        {
            FS.HISFC.Models.Registration.Booking booking = null;// this.bookingMgr.Query(CardNo);

            if (booking == null)
            {
                MessageBox.Show("��������ԤԼ��Ϣʱ����!" + this.bookingMgr.Err, "��ʾ");
                return null;
            }

            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
            //û��ԤԼ��Ϣ
            //if (booking.ID == null || booking.ID == "")
            //{
            //    regInfo.PID.CardNO = CardNo;
            //    regInfo.IsFirst = true;
            //    regInfo.Sex.ID = "M";
            //    regInfo.Pact.ID = this.DefaultPactID;
            //}
            //else
            //{
            //    regInfo = (FS.HISFC.Models.RADT.Patient)booking;
            //    regInfo.PID.CardNO = CardNo;
            //    regInfo.IsFirst = true;
            //    regInfo.Pact.ID = this.DefaultPactID;
            //}

            return regInfo;
        }
        /// <summary>
        /// Set Age
        /// </summary>
        /// <param name="birthday"></param>
        private void setAge(DateTime birthday)
        {
            this.txtAge.Text = "";

            if (birthday == DateTime.MinValue)
            {
                return;
            }

            DateTime current;
            int year, month, day;

            current = this.regMgr.GetDateTimeFromSysDateTime();
            year = current.Year - birthday.Year;
            month = current.Month - birthday.Month;
            day = current.Day - birthday.Day;

            if (year > 1)
            {
                this.txtAge.Text = year.ToString();
                this.cmbUnit.SelectedIndex = 0;
            }
            else if (year == 1)
            {
                if (month >= 0)//һ��
                {
                    this.txtAge.Text = year.ToString();
                    this.cmbUnit.SelectedIndex = 0;
                }
                else
                {
                    this.txtAge.Text = Convert.ToString(12 + month);
                    this.cmbUnit.SelectedIndex = 1;
                }
            }
            else if (month > 0)
            {
                this.txtAge.Text = month.ToString();
                this.cmbUnit.SelectedIndex = 1;
            }
            else if (day > 0)
            {
                this.txtAge.Text = day.ToString();
                this.cmbUnit.SelectedIndex = 2;
            }

        }
        /// <summary>
        /// �õ�����Ӧ��
        /// </summary>		
        /// <returns></returns>
        private int getCost()
        {
            this.lbReceive.Text = "";

            if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "" ||
                this.cmbPayKind.Tag == null || this.cmbPayKind.Tag.ToString() == "")
                return 0;//û¼����ȫ������

            string regLvlID, pactID;
            decimal regfee = 0, chkfee = 0, digfee = 0, othfee = 0, owncost = 0, pubcost = 0;

            regLvlID = this.cmbRegLevel.Tag.ToString();
            pactID = this.cmbPayKind.Tag.ToString();

            int rtn = this.GetRegFee(pactID, regLvlID, ref regfee, ref chkfee, ref digfee, ref othfee);
            if (rtn == -1 || rtn == 1) return 0;

            //��û���Ӧ�ա�����
            if (this.regObj == null || this.regObj.PID.CardNO == "")
            {
                this.getCost(regfee, chkfee, digfee, ref othfee, ref owncost, ref pubcost, "");
            }
            else
            {
                this.getCost(regfee, chkfee, digfee, ref othfee, ref owncost, ref pubcost, this.regObj.PID.CardNO);
            }

            if (pactID != "2" && pactID != "3")
            {
                owncost -= digfee;
            }
            this.lbReceive.Text = owncost.ToString();

            return 0;
        }
        /// <summary>
        /// ��ȡ�Һŷ�
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="regLvlID"></param>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <param name="digPubFee"></param>
        /// <returns></returns>
        private int GetRegFee(string pactID, string regLvlID, ref decimal regFee, ref decimal chkFee,
            ref decimal digFee, ref decimal othFee)
        {
            FS.HISFC.Models.Registration.RegLvlFee p = this.regFeeMgr.Get(pactID, regLvlID);
            if (p == null)//����
            {
                return -1;
            }
            if (p.ID == null || p.ID == "")//û��ά���Һŷ�
            {
                return 1;
            }

            regFee = p.RegFee;
            chkFee = p.ChkFee;
            digFee = p.OwnDigFee;
            othFee = p.OthFee;

            return 0;
        }

        /// <summary>
        /// ��Ӧ�ɽ��תΪ�Һ�ʵ��,
        /// ���Բ�����Ϊref�������� TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int ConvertRegFeeToObject(FS.HISFC.Models.Registration.Register obj)
        {
            decimal regFee = 0, chkFee = 0, digFee = 0, othFee = 0;

            int rtn = this.GetRegFee(obj.Pact.ID, obj.DoctorInfo.Templet.RegLevel.ID,
                          ref regFee, ref chkFee, ref digFee, ref othFee);

            obj.RegLvlFee.RegFee = regFee;
            obj.RegLvlFee.ChkFee = chkFee;
            if (obj.Pact.ID != "2" && obj.Pact.ID != "3")
            {
                digFee = 0;
            }
            obj.RegLvlFee.OwnDigFee = digFee;
            obj.RegLvlFee.OthFee = othFee;

            return rtn;
        }

        /// <summary>
        /// ��û���Ӧ�����������
        /// </summary>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <param name="digPub"></param>
        /// <param name="ownCost"></param>
        /// <param name="pubCost"></param>
        /// <param name="cardNo"></param>		
        private void getCost(decimal regFee, decimal chkFee, decimal digFee, ref decimal othFee,
            ref decimal ownCost, ref decimal pubCost, string cardNo)
        {
            if (this.IsPubDiagFee)
            {
                ownCost = regFee + chkFee + othFee;//�Һŷ��Է�
                pubCost = digFee;//������
            }
            else
            {
                /*
                 * �յ�����ȡ�㷨
                 * �����ϡ�����Һŷֱ���ȡһ�οյ��ѡ�
                 * ͬһ����ͬһ���Ҷ��ź�ֻ��ȡһ�οյ���
                 * �յ�����othFee��ʾ
                 */

                //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
                //if (this.IsKTF)
                if (this.otherFeeType == "0") //�յ���
                {

                    //û�����뻼����Ϣʱ��Ĭ����ʾ��ȡ�յ���
                    if (cardNo == null || cardNo == "")
                    {
                        ///
                    }
                    else
                    {
                        DateTime regDate = this.dtBookingDate.Value.Date;

                        if (this.dtBegin.Value.ToString("HHmm") == "0000")
                        {
                            regDate = DateTime.Parse(regDate.ToString("yyyy-MM-dd") + " " + this.regMgr.GetSysDateTime("HH24:mi:ss"));
                        }
                        else
                        {
                            regDate = DateTime.Parse(regDate.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
                        }

                        ///�������Ų�ѯ�������һ�ιҺ���Ϣ
                        ArrayList alRegs = this.regMgr.Query(cardNo, regDate.Date);

                        string currentNoon = this.getNoon(regDate);

                        if (alRegs != null)
                        {
                            foreach (FS.HISFC.Models.Registration.Register obj in alRegs)
                            {
                                //δ�ҺŻ������һ�ιҺ�ʱ��ͬ��ǰʱ�����ͬ,����ȡ�Һŷ�
                                if (obj.DoctorInfo.SeeDate.Date == regDate.Date)
                                {
                                    if (obj.DoctorInfo.Templet.Noon.ID != currentNoon)
                                    {
                                        ///
                                    }
                                    else
                                    {
                                        othFee = 0;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
                if (this.otherFeeType == "1") //��������
                {
                    if (!this.chbBookFee.Checked) //ͨ������ѡ��
                    {
                        othFee = 0;
                    }
                }

                ownCost = regFee + chkFee + othFee + digFee;
                pubCost = 0;
            }

            //			ownCost = regFee + chkFee + othFee + digFee ;
            //			pubCost = digPub ;
        }

        /// <summary>
        /// ��Ӧ�ɽ��תΪ�Һ�ʵ��,
        /// ���Բ�����Ϊref�������� TNND
        /// </summary>
        /// <param name="obj"></param>
        private void ConvertCostToObject(FS.HISFC.Models.Registration.Register obj)
        {
            decimal othFee = 0, ownCost = 0, pubCost = 0;
            othFee = obj.RegLvlFee.OthFee; //add by niux
            this.getCost(obj.RegLvlFee.RegFee, obj.RegLvlFee.ChkFee, obj.RegLvlFee.OwnDigFee,
                    ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);

            obj.RegLvlFee.OthFee = othFee;
            obj.OwnCost = ownCost;
            obj.PubCost = pubCost;

        }
        #endregion

        #region txtName
        /// <summary>
        /// ���������س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.IsInputName && this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show("�����뻼������!", "��ʾ");
                    this.txtName.Focus();
                    return;
                }

                //û�����벡����,����ݻ������������Һ���Ϣ
                if (this.txtCardNo.Text.Trim() == "")
                {
                    string CardNo = this.GetCardNoByName(this.txtName.Text.Trim());

                    if (CardNo == "")
                    {
                        this.txtName.Focus();
                        return;
                    }
                    else
                    {
                        //{0C30F7F0-2BCF-4c03-BA6E-D7E22A638E97}
                        this.txtCardNo.Enabled = false;
                    }
                    this.txtCardNo.Text = CardNo;

                    this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
                else
                {
                    //this.cmbSex.Focus();
                    this.setNextControlFocus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }

        }
        /// <summary>
        /// ͨ�����������������߹Һ���Ϣ
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetCardNoByName(string Name)
        {
            FS.HISFC.Components.Registration.frmQueryPatientByName f = new FS.HISFC.Components.Registration.frmQueryPatientByName();

            f.QueryByName(Name);
            DialogResult dr = f.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string CardNo = f.SelectedCardNo;
                f.Dispose();
                return CardNo;
            }

            f.Dispose();

            return "";
        }
        #endregion

        #region KeyEnter
        private void cmbSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
                {
                    MessageBox.Show("��ѡ�����Ա�!", "��ʾ");
                    this.cmbSex.Focus();
                    return;
                }
                if (this.enumBirthdayInputType == EnumBirthdayInputType.Birthday
                    || this.enumBirthdayInputType == EnumBirthdayInputType.Both)//IsBirthdayEnd)
                {
                    this.dtBirthday.Focus();
                }
                else
                {
                    cmbPayKind.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        private void txtAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.getBirthday();

            //    this.cmbUnit.Focus();
            //}
            //else if (e.KeyCode == Keys.PageUp)
            //{
            //    //������ת
            //    this.setPriorControlFocus();
            //}
            //else if (e.KeyCode == Keys.PageDown)
            //{
            //    this.setNextControlFocus();
            //}
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private void getBirthday()
        {
            string age = this.txtAgee.Text.Trim();
            int i = 0;

            if (age == "") age = "0";

            try
            {
                i = int.Parse(age);
            }
            catch (Exception e)
            {
                string error = e.Message;
                MessageBox.Show("�������䲻��ȷ,����������!", "��ʾ");
                //this.txtAge.Focus();
                this.txtAgee.Focus();
                return;
            }

            ///
            ///

            DateTime birthday = DateTime.MinValue;

            this.getBirthday(i, this.cmbUnit.Text, ref birthday);

            if (birthday < this.dtBirthday.MinDate)
            {
                MessageBox.Show("���䲻�ܹ���!", "��ʾ");
                //this.txtAge.Focus();
                this.txtAgee.Focus();
                return;
            }

            //this.dtBirthday.Value = birthday ;

            if (this.cmbUnit.Text == "��")
            {

                //���ݿ��д���ǳ�������,������䵥λ����,��������ĳ������ں����ݿ��г������������ͬ
                //�Ͳ��������¸�ֵ,��Ϊ����ĳ�����������Ϊ����,���������ݿ���Ϊ׼

                if (this.dtBirthday.Value.Year != birthday.Year)
                {
                    this.dtBirthday.Value = birthday;
                }
            }
            else
            {
                this.dtBirthday.Value = birthday;
            }
        }
        /// <summary>
        /// ��������õ���������
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <param name="birthday"></param>
        private void getBirthday(int age, string ageUnit, ref DateTime birthday)
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            if (ageUnit == "��")
            {
                birthday = current.AddYears(-age);
            }
            else if (ageUnit == "��")
            {
                birthday = current.AddMonths(-age);
            }
            else if (ageUnit == "��")
            {
                birthday = current.AddDays(-age);
            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.getBirthday();
        }
        private void cmbUnit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.txtPhone.Focus();
            //}
            //else if (e.KeyCode == Keys.PageUp)
            //{
            //    //������ת
            //    this.setPriorControlFocus();
            //}
            //else if (e.KeyCode == Keys.PageDown)
            //{
            //    this.setNextControlFocus();
            //}
        }

        private void cmbPayKind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbPayKind.Tag == null || this.cmbPayKind.Tag.ToString() == "")
                {
                    MessageBox.Show("��ѡ���߽������!", "��ʾ");
                    this.cmbPayKind.Focus();
                    return;
                }

                if (this.ValidCombox("��ѡ��ĺ�ͬ��λ������ں�ͬ��λ�������б���,������ѡ��") < 0)
                {
                    //this.cmbPayKind.Focus();
                    return;
                }

                //�ж��Ƿ���Ҫ����ҽ��֤��,�����Ҫ,��������ҽ��֤�Ŵ�
                FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.cmbPayKind.Tag.ToString());
                if (pact == null)
                {
                    MessageBox.Show("������ͬ��λʱ����!" + conMgr.Err, "��ʾ");
                    this.cmbPayKind.Focus();
                    return;
                }

                if (pact.ID == null || pact.ID == "")//û�м�����
                {
                    MessageBox.Show("���ݿ��Ѿ��䶯,���˳��������µ�½!", "��ʾ");
                    return;
                }

                this.getCost();

                this.getPayRate(pact);

                if (pact.IsNeedMCard && (this.enumBirthdayInputType == EnumBirthdayInputType.Birthday
                    || this.enumBirthdayInputType == EnumBirthdayInputType.Both))//IsBirthdayEnd)
                {
                    this.txtMcardNo.Focus();
                }
                else if (pact.IsNeedMCard && (this.enumBirthdayInputType == EnumBirthdayInputType.Age)
                    || this.enumBirthdayInputType == EnumBirthdayInputType.Both)//!IsBirthdayEnd)
                {
                    //txtAge.Focus();
                    //this.txtAgee.Focus();
                    this.numTxtYear.Focus();
                }
                else
                {
                    if (this.enumBirthdayInputType == EnumBirthdayInputType.Birthday
                    || this.enumBirthdayInputType == EnumBirthdayInputType.Both)
                    {
                        if (this.txtAgee.Text.Trim() == "")
                        {
                            //this.txtAge.Focus();
                            //this.txtAgee.Focus();
                            this.numTxtYear.Focus();
                        }
                        else
                        {
                            this.txtPhone.Focus();
                        }
                    }
                    else
                    {
                        //txtAge.Focus();
                        //this.txtAgee.Focus();
                        this.numTxtYear.Focus();
                    }
                }
                //{D845A003-62E2-44da-8612-81C4F6A1037E}
                FS.FrameWork.Models.NeuObject neuObj = conMgr.GetConstant("regcomparepact", this.cmbPayKind.Tag.ToString());
                if (neuObj != null && neuObj.Name != string.Empty)
                {
                    this.cmbRegLevel.Tag = neuObj.Name.ToString();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                //this.setPriorControlFocus() ;
                this.dtBirthday.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// ��ʾ��ͬ��λ֧������
        /// </summary>
        /// <param name="pact"></param>
        private void getPayRate(FS.HISFC.Models.Base.PactInfo pact)
        {
            this.lbTot.Text = "";

            if (pact != null && pact.Rate.PayRate != 0)
            {
                decimal rate = pact.Rate.PayRate * 100;
                this.lbTot.Text = rate.ToString("###") + "%";
            }
        }
        private void txtMcardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtAgee.Text.Trim() == "")
                {
                    //this.txtAge.Focus();
                    //this.txtAgee.Focus();
                    this.numTxtYear.Focus();
                }
                else
                {
                    this.txtPhone.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void dtBirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;

                if (this.dtBirthday.Value.Date > current)
                {
                    MessageBox.Show("�������ڲ��ܴ��ڵ�ǰʱ��!", "��ʾ");
                    this.dtBirthday.Focus();
                    return;
                }

                //��������
                if (this.dtBirthday.Value.Date != current)
                {
                    this.setAgee(this.dtBirthday.Value);
                }

                this.cmbPayKind.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.cmbSex.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.cmbPayKind.Focus();
            }
        }
        private void txtPhone_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAddress.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void txtAddress_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbCardType.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void cmbCardType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbRegLevel.Focus();
                //if (this.save() == -1)
                //{
                //    cmbRegLevel.Focus();
                //}

                return;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        #endregion
        #endregion

        #region �Զ���ȡ���￨������ǿ2007-09-24
        /// <summary>
        /// �Զ���ȡ���￨��
        /// </summary>
        private void AutoGetCardNO()
        {
            int autoGetCardNO;
            autoGetCardNO = regMgr.AutoGetCardNO();
            if (autoGetCardNO == -1)
            {
                MessageBox.Show("δ�ܳɹ��Զ��������ţ����ֶ����룡", "��ʾ");
            }
            else
            {
                this.txtCardNo.Text = autoGetCardNO.ToString().PadLeft(10, '0');
            }
            this.txtCardNo.Focus();
        }
        #endregion

        #region PageUp,PageDown�л�������ת
        /// <summary>
        /// ������һ���ؼ���ý���
        /// </summary>		
        private void setPriorControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("+{TAB}");

        }

        /// <summary>
        /// ������һ���ؼ���ý���
        /// </summary>		
        private void setNextControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("{TAB}");
        }
        #endregion

        #region ���뷨�˵��¼�
        private void m_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
            {
                if (sender == m)
                {
                    m.Checked = true;
                    this.CHInput = this.getInputLanguage(m.Text);
                    //�������뷨
                    this.saveInputLanguage();
                }
                else
                {
                    m.Checked = false;
                }
            }
        }
        /// <summary>
        /// ��ȡ��ǰĬ�����뷨
        /// </summary>
        private void readInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting();

            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                this.CHInput = this.getInputLanguage(node.Attributes["currentmodel"].Value);

                if (this.CHInput != null)
                {
                    foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
                    {
                        if (m.Text == this.CHInput.LayoutName)
                        {
                            m.Checked = true;
                        }
                    }
                }

                //��ӵ�������

            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡ�Һ�Ĭ���������뷨����!" + e.Message);
                return;
            }
        }

        private void addContextToToolbar()
        {
            FS.FrameWork.WinForms.Controls.NeuToolBar main = null;

            foreach (Control c in FindForm().Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuToolBar))
                {
                    main = (FS.FrameWork.WinForms.Controls.NeuToolBar)c;
                }
            }

            ToolBarButton button = null;

            if (main != null)
            {
                foreach (ToolBarButton b in main.Buttons)
                {
                    if (b.Text == "���뷨") button = b;
                }
            }

            //if(button != null)
            //{
            //    ToolStripDropDownButton drop = (ToolStripDropDownButton)button;
            //    foreach(ToolStripMenuItem m in neuContextMenuStrip1.Items)
            //    {
            //        drop.DropDownItems.Add(m);
            //    }
            //}
        }

        /// <summary>
        /// �������뷨���ƻ�ȡ���뷨
        /// </summary>
        /// <param name="LanName"></param>
        /// <returns></returns>
        private InputLanguage getInputLanguage(string LanName)
        {
            foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
            {
                if (input.LayoutName == LanName)
                {
                    return input;
                }
            }
            return null;
        }
        /// <summary>
        /// ���浱ǰ���뷨
        /// </summary>
        private void saveInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting();
            }
            if (this.CHInput == null) return;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                node.Attributes["currentmodel"].Value = this.CHInput.LayoutName;

                doc.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("����Һ�Ĭ���������뷨����!" + e.Message);
                return;
            }
        }
        #endregion

        #region �Զ����������Ÿ�ֵ
        private void ucAssign_getAssinID(string assignID)
        {
            this.assignNO = assignID;
        }

        private int TriageForRegistration(FS.HISFC.Models.Registration.Register reg, string type, int seq, bool isUseBookNum)
        {
            if (this.conMgr.TriageForRegistration(reg, type, seq, isUseBookNum) == -1)
            {
                this.assignNO = string.Empty;
                return -1;
            }
            return 1;
        }

        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int save()
        {
            if (this.valid() == -1) return 2;
            if (this.JuageMedicareStatus() == -1) return -2;
            if (this.getValue() == -1) return 2;

            if (this.ValidCardNO(this.regObj.PID.CardNO) < 0)
            {
                return -1;
            }

            if (this.IsPrompt)
            {
                //ȷ����ʾ
                if (MessageBox.Show("¼�������Ƿ���ȷ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    this.cmbRegLevel.Focus();
                    return -1;
                }
            }


            int rtn;
            string Err = "";
            //�ӿ�ʵ��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
            if (this.iProcessRegiter != null)
            {
                rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

                if (rtn < 0)
                {
                    MessageBox.Show(Err);
                    return -1;
                }
            }

            this.MedcareInterfaceProxy.SetPactCode(this.regObj.Pact.ID);

            #region �˻�����
            bool isAccountFee = false;
            decimal vacancy = 0;
            int result = this.feeMgr.GetAccountVacancy(this.regObj.PID.CardNO, ref vacancy);
            if (result > 0)
            {
                if (!feeMgr.CheckAccountPassWord(this.regObj)) return -1;
                if (vacancy > 0)
                {
                    isAccountFee = true;
                }

            }
            #endregion


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.regMgr.con);
            //t.BeginTransaction();

            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SchemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.SIMgr.SetTrans(t);
            //this.InterfaceMgr.SetTrans(t.Trans);
            #region ȡ��Ʊ
            if (this.GetRecipeType == 2)
            {
                //this.regObj.InvoiceNO = this.feeMgr.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.R);
                this.regObj.InvoiceNO = this.feeMgr.GetNewInvoiceNO("R");

                if (this.regObj.InvoiceNO == null || this.regObj.InvoiceNO == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�ò���Աû�п���ʹ�õ�����Һŷ�Ʊ������ȡ");
                    return -1;
                }
            }
            else if (this.GetRecipeType == 3)
            {
                //this.regObj.InvoiceNO = this.feeMgr.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.C);
                //ȡ�����վ�
                this.regObj.InvoiceNO = this.feeMgr.GetNewInvoiceNO("C");
                if (this.regObj.InvoiceNO == null || this.regObj.InvoiceNO == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�ò���Աû�п��Կ���ʹ�õ������շѷ�Ʊ������ȡ");
                    return -1;
                }
            }
            else if (this.GetRecipeType == 1)
            {
                this.regObj.InvoiceNO = this.regObj.RecipeNO.ToString().PadLeft(12, '0');
            }
            #endregion

            decimal OwnCostTot = this.regObj.OwnCost;

            #region �˻�����
            if (isAccountFee)
            {
                decimal cost = 0m;

                if (vacancy < OwnCostTot)
                {
                    cost = vacancy;
                    this.regObj.PayCost = vacancy;
                    this.regObj.OwnCost = this.regObj.OwnCost - vacancy;
                }
                else
                {
                    cost = OwnCostTot;
                    this.regObj.PayCost = this.regObj.OwnCost;
                    this.regObj.OwnCost = 0;
                }
                if (this.feeMgr.AccountPay(this.regObj, cost, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R") < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.feeMgr.Err);
                    return -1;
                }
                this.regObj.IsAccount = true;
            }
            #endregion

            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            try
            {

                #region ���¿������
                int orderNo = 0;
                string deptID = string.Empty;

                //2�������
                if (this.strGetSeeNoType == "2")//2�������ź�
                {
                    deptID = this.regObj.DoctorInfo.Templet.Dept.ID;
                }
                else if (this.strGetSeeNoType == "3")//�������ź�
                {
                    ArrayList alNurse = this.conMgr.QueryNurseStationByDept(this.regObj.DoctorInfo.Templet.Dept, "14");
                    if (alNurse == null || alNurse.Count <= 0)
                    {
                        this.strGetSeeNoType = "2";
                        deptID = this.regObj.DoctorInfo.Templet.Dept.ID;
                    }
                    else
                    {
                        if (this.regObj.DoctorInfo.Templet.Doct.ID == "" || this.regObj.DoctorInfo.Templet.Doct.ID == null)
                        {
                            this.strGetSeeNoType = "2";
                            deptID = this.regObj.DoctorInfo.Templet.Dept.ID;
                        }
                        else
                        {
                            FS.FrameWork.Models.NeuObject nurseObj = alNurse[0] as FS.FrameWork.Models.NeuObject;
                            deptID = nurseObj.ID;
                        }
                    }
                }

                if (this.UpdateSeeID(deptID, this.regObj.DoctorInfo.Templet.Doct.ID,
                    this.regObj.DoctorInfo.Templet.Noon.ID, this.regObj.DoctorInfo.SeeDate, ref orderNo,
                    ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.strGetSeeNoType = "3";
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                this.strGetSeeNoType = "3";
                regObj.DoctorInfo.SeeNO = orderNo;

                //ר�ҡ�ר�ơ����ԤԼ�Ÿ����Ű��޶�
                #region schema
                if (this.UpdateSchema(this.SchemaMgr, this.regObj.RegType, ref orderNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (Err != "") MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                regObj.DoctorInfo.SeeNO = orderNo;
                #endregion

                //1ȫԺ��ˮ��			
                if (this.Update(this.regMgr, current, ref orderNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                regObj.OrderNO = orderNo;
                #endregion

                //ԤԼ�Ÿ����ѿ����־
                #region booking
                if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                {
                    //���¿����޶�
                    rtn = this.bookingMgr.Update((this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).ID,
                                true, regMgr.Operator.ID, current);
                    if (rtn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ԤԼ������Ϣ����!" + this.bookingMgr.Err, "��ʾ");
                        return -1;
                    }
                    if (rtn == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ԤԼ�Һ���Ϣ״̬�Ѿ����,�����¼���!", "��ʾ");
                        return -1;
                    }
                }
                #endregion

                #region �����ӿ�ʵ��
                this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.MedcareInterfaceProxy.Connect();

                this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.MedcareInterfaceProxy.BeginTranscation();

                this.regObj.SIMainInfo.InvoiceNo = this.regObj.InvoiceNO;
                int returnValue = this.MedcareInterfaceProxy.UploadRegInfoOutpatient(this.regObj);
                if (returnValue == -1)
                {
                    this.MedcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ϴ��Һ���Ϣʧ��!") + this.MedcareInterfaceProxy.ErrMsg);

                    return -1;
                }
                ////ҽ�����ߵǼ�ҽ����Ϣ
                //if (this.regObj.Pact.PayKind.ID == "02")
                //{
                this.regObj.OwnCost = this.regObj.SIMainInfo.OwnCost;  //�Էѽ��
                this.regObj.PubCost = this.regObj.SIMainInfo.PubCost;  //ͳ����
                this.regObj.PayCost = this.regObj.SIMainInfo.PayCost;  //�ʻ����
                //}
                #endregion

                #region addby xuewj 2010-3-15

                if (this.adt == null)
                {
                    this.adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
                }
                if (this.adt != null)
                {
                    this.adt.RegOutPatient(this.regObj);
                }

                #endregion

                //�ǼǹҺ���Ϣ
                int i = this.regMgr.Insert(this.regObj);
                if (i != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.MedcareInterfaceProxy.CancelRegInfoOutpatient(this.regObj);
                    //this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show(this.regMgr.Err, "��ʾ");
                    return -1;
                }

                //���»��߻�����Ϣ
                if (this.UpdatePatientinfo(this.regObj, this.patientMgr, this.regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.MedcareInterfaceProxy.CancelRegInfoOutpatient(this.regObj);
                    //this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                #region �Զ�����
                if (this.cmbDoctor.Tag != "")
                {
                    if (chSpecialPerson.Checked)//���˹Һ�
                    {
                        #region ����У��
                        if (this.assignNO != string.Empty)
                        {
                            bool isSameDoct = false;
                            string noon = this.getNoon(this.regObj.DoctorInfo.SeeDate);
                            ArrayList list = this.interMgr.GetBookNumForRegistration(this.cmbDoctor.Tag.ToString(), this.regObj.DoctorInfo.SeeDate, noon);
                            foreach (string s in list)
                            {
                                if (this.assignNO == s)
                                {
                                    isSameDoct = true;
                                    break;
                                }
                            }
                            if (!isSameDoct)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.MedcareInterfaceProxy.Rollback();
                                MessageBox.Show("�ú��Ѿ����ã������¹�ѡ���˿�ѡ���ºţ�");
                                return -1;
                            }
                        }
                        #endregion
                        if (this.assignNO != string.Empty)
                        {
                            if (TriageForRegistration(this.regObj, "0", FS.FrameWork.Function.NConvert.ToInt32(this.assignNO), true) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.MedcareInterfaceProxy.CancelRegInfoOutpatient(this.regObj);
                                //this.MedcareInterfaceProxy.Rollback();
                                MessageBox.Show("�Զ�����ʧ�ܣ�\r��ɸô����ԭ�������δά����ҽ���ķ�����С�");
                                return -1;
                            }
                        }
                        else
                        {
                            if (TriageForRegistration(this.regObj, "0", 0, true) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.MedcareInterfaceProxy.CancelRegInfoOutpatient(this.regObj);
                                //this.MedcareInterfaceProxy.Rollback();
                                MessageBox.Show("�Զ�����ʧ�ܣ�\r��ɸô����ԭ�������δά����ҽ���ķ�����С�");
                                return -1;
                            }
                        }
                    }
                    else//�����Һ�
                    {
                        if (TriageForRegistration(this.regObj, "0", 0, false) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.MedcareInterfaceProxy.CancelRegInfoOutpatient(this.regObj);
                            //this.MedcareInterfaceProxy.Rollback();
                            MessageBox.Show("�Զ�����ʧ�ܣ�\r��ɸô����ԭ�������δά����ҽ���ķ�����С�");
                            return -1;
                        }
                    }
                }
                #endregion

                //�ӿ�ʵ��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
                if (this.iProcessRegiter != null)
                {
                    //{4F5BD7B2-27AF-490b-9F09-9DB107EA7AA0}
                    //rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

                    rtn = this.iProcessRegiter.SaveEnd(ref regObj, ref Err);
                    if (rtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.MedcareInterfaceProxy.Rollback();
                        MessageBox.Show(Err);
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.MedcareInterfaceProxy.Commit();
                this.MedcareInterfaceProxy.Disconnect();

                //�����´�����,�� 1,��ֹ��;��������
                this.UpdateRecipeNo(1);

                this.QueryRegLevl();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            try
            {
                FS.HISFC.Models.Registration.Register regTemp = this.regMgr.GetByClinic(this.regObj.ID);
                if (regTemp == null || regTemp.ID == "")
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    #region ȡ������
                    FS.HISFC.Models.Nurse.Assign assignObj = null;
                    assignObj = this.conMgr.QueryAssignByClinicID(this.regObj.ID);
                    if (assignObj != null)
                    {
                        //ɾ��������Ϣ
                        rtn = this.assMgr.Delete(this.regObj.ID);
                        if (rtn == -1)//����
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.assMgr.Err, "��ʾ");
                            return -1;
                        }
                        //��������-1
                        if (this.assMgr.UpdateQueue(assignObj.Queue.ID, "-1") == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.assMgr.Err, "��ʾ");
                            return -1;
                        }
                    }
                    #endregion

                    #region ���¿������
                    int orderNo = 0;
                    string deptID = string.Empty;

                    if (this.strGetSeeNoType == "2")//2�������ź�
                    {
                        deptID = this.regObj.DoctorInfo.Templet.Dept.ID;
                    }
                    else if (this.strGetSeeNoType == "3")//�������ź�
                    {
                        ArrayList alNurse = this.conMgr.QueryNurseStationByDept(this.regObj.DoctorInfo.Templet.Dept, "14");
                        if (alNurse == null || alNurse.Count <= 0)
                        {
                            this.strGetSeeNoType = "2";
                            deptID = this.regObj.DoctorInfo.Templet.Dept.ID;
                        }
                        else
                        {
                            if (this.regObj.DoctorInfo.Templet.Doct.ID == "" || this.regObj.DoctorInfo.Templet.Doct.ID == null)
                            {
                                this.strGetSeeNoType = "2";
                                deptID = this.regObj.DoctorInfo.Templet.Dept.ID;
                            }
                            else
                            {
                                FS.FrameWork.Models.NeuObject nurseObj = alNurse[0] as FS.FrameWork.Models.NeuObject;
                                deptID = nurseObj.ID;
                            }
                        }
                    }

                    if (this.UpdateSeeID(deptID, this.regObj.DoctorInfo.Templet.Doct.ID,
                        this.regObj.DoctorInfo.Templet.Noon.ID, this.regObj.DoctorInfo.SeeDate, ref orderNo,
                        ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.strGetSeeNoType = "3";
                        MessageBox.Show(Err, "��ʾ");
                        return -1;
                    }

                    this.strGetSeeNoType = "3";
                    this.regObj.DoctorInfo.SeeNO = orderNo;
                    #endregion

                    #region ����Һż�¼
                    //�ǼǹҺ���Ϣ
                    if (this.regMgr.Insert(this.regObj) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.regMgr.Err, "��ʾ");
                        return -1;
                    }
                    //���»��߻�����Ϣ
                    if (this.UpdatePatientinfo(this.regObj, this.patientMgr, this.regMgr, ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Err, "��ʾ");
                        return -1;
                    }
                    #endregion

                    #region ��������¼
                    if (this.regObj.DoctorInfo.Templet.Doct.ID != ""
                        && this.regObj.DoctorInfo.Templet.Doct != null)
                    {
                        if (TriageForRegistration(this.regObj, "0", 0, true) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��������¼ʧ�ܣ�");
                            return -1;
                        }
                    }
                    #endregion
                    ////���¹Һŷ���״̬
                    //FS.HISFC.Models.Nurse.Assign assObj = new FS.HISFC.Models.Nurse.Assign();
                    //assObj = this.assMgr.QueryByClinicID(this.regObj.ID);
                    //if (assObj == null || assObj.Register.ID == "")
                    //{
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show(this.assMgr.Err, "��ʾ");
                    //    return -1;
                    //}
                    //if (this.regInteMgr.Update(this.regObj.ID, FS.FrameWork.Management.Connection.Operator.ID, assObj.TirageTime) == -1)
                    //{
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show(this.regInteMgr.Err, "��ʾ");
                    //    return -1;
                    //}
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }
            //����{F0661633-4754-4758-B683-CB0DC983922B}
            if (this.isShowChangeCostForm)
            {
                rtn = this.ShowChangeForm(this.regObj);
                {
                    if (rtn < 0)
                    {
                        return -1;
                    }
                }
            }

            //if (this.isAutoPrint)
            //{
            //    this.Print(this.regObj, this.regMgr);
            //}
            //else
            //{
            //    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ���Ƿ��ӡ�Һ�Ʊ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //    if (rs == DialogResult.Yes)
            //    {
            //        this.Print(this.regObj, this.regMgr);
            //    }
            //}


            this.addRegister(this.regObj);

            this.regObjTemp = new FS.HISFC.Models.Registration.Register();
            this.regObjTemp = this.regObj.Clone();
            //this.PrintRecipe(regObj.ID.ToString());
            this.clear();

            ChangeInvoiceNOMessage();

            if (this.regObjTemp.Pact.PayKind.ID == "02")
            {
                MessageBox.Show("�籣�Һųɹ���");
            }

            return 0;


        }

        /// <summary>
        /// ������ӡ{088448CC-1CC9-4303-A85B-66B207F4DBAE}
        /// </summary>
        /// <param name="recipeNO"></param>
        public void PrintRecipe(string recipeNO)
        {
            FS.HISFC.BizProcess.Interface.IRecipePrint o =
                FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(
                typeof(FS.SOC.HISFC.Components.Register.SDFY.ucRegister),
                typeof(FS.HISFC.BizProcess.Interface.IRecipePrint))
                as FS.HISFC.BizProcess.Interface.IRecipePrint;
            if (o == null)
            {
                //{4EBC33FA-8E0B-46be-A3B0-D9832D73B173}
                //MessageBox.Show("�ӿ�δʵ��");
                return;
            }
            else
            {
                o.RecipeNO = recipeNO;
                o.SetPatientInfo(this.regObjTemp);
                o.PrintRecipe();
            }
        }

        /// <summary>
        /// ��Ʊ���л��ж�
        /// </summary>
        private void ChangeInvoiceNOMessage()
        {
            string invoiceNO = string.Empty;
            string invoiceType = string.Empty;
            if (this.GetRecipeType == 2)
            {
                invoiceType = "R";
            }
            else if (this.GetRecipeType == 3)
            {
                //ȡ�����վ�
                invoiceType = "C";
            }
            else
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            invoiceNO = this.feeMgr.GetNewInvoiceNO(invoiceType);
            if (string.IsNullOrEmpty(invoiceNO))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�ò���Աû�п���ʹ�õ�����Һŷ�Ʊ������ȡ");
                return;
            }
            string errText = string.Empty;
            FS.FrameWork.Management.PublicTrans.RollBack();
            //int resultValue = feeMgr.InvoiceMessage(regFeeMgr.Operator.ID, invoiceType, invoiceNO, 1, ref errText);
            //if (resultValue < 0)
            //{
            //    MessageBox.Show(errText);
            //}
        }

        #region ��Ч����֤
        /// <summary>
        /// ��Ч����֤
        /// </summary>
        /// <returns></returns>
        private int valid()
        {
            this.txtAddress.Focus();//��ֹ��combox�²��س��ͱ������

            if (this.txtRecipeNo.Text.Trim() == "")
            {
                MessageBox.Show("�����봦����!", "��ʾ");
                this.ChangeRecipe();
                return -1;
            }

            if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ��Һż���!", "��ʾ");
                this.cmbRegLevel.Focus();
                return -1;
            }

            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if ((this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == ""))
            {
                MessageBox.Show("������Һſ���!", "��ʾ");
                this.cmbDept.Focus();
                return -1;
            }

            if (this.cmbDept.SelectedItem == null)
            {
                MessageBox.Show("��ѡ��Һſ���!", "��ʾ");
                this.cmbDept.Focus();
                return -1;
            }
            if (this.cmbDept.Text != this.cmbDept.SelectedItem.Name && this.cmbDept.Text != this.cmbDept.Tag.ToString())
            {
                MessageBox.Show("��������ȷ�ĹҺſ���!", "��ʾ");
                this.cmbDept.Focus();
                return -1;
            }

            if ((level.IsExpert || level.IsSpecial) &&
                (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == ""))
            {
                MessageBox.Show("ר�Һű���ָ������ҽ��!", "��ʾ");
                this.cmbDoctor.Focus();
                return -1;
            }

            //���ҽ���ؼ�����������Ϣ�������Ҫ��ҽ���ؼ���ѡ�е�ҽ����Ч {9EA8A9C5-1F2F-4c65-A2B9-1C158E2CFB0C} ygch
            if (this.cmbDoctor.Text != "" && this.cmbDoctor.Text != String.Empty)
            {
                if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag == "")
                {
                    MessageBox.Show("ҽ����Ϣ����������������");
                    this.cmbDoctor.Focus();
                    return -1;
                }
            }

            if (this.regObj == null)
            {
                MessageBox.Show("��¼��ҺŻ���!", "��ʾ");
                this.txtCardNo.Focus();
                return -1;
            }

            //{05B4AB01-C7FC-4e1b-9A77-80B83E77F77F} �жϲ������Ƿ�Ϊ�� xuc
            if (string.IsNullOrEmpty(this.regObj.PID.CardNO) == true)
            {
                MessageBox.Show("��¼�벡����!", "��ʾ");
                this.txtCardNo.Focus();
                return -1;
            }

            if (this.IsInputName && this.txtName.Text.Trim() == "")
            {
                MessageBox.Show("�����뻼������!", "��ʾ");
                this.txtName.Focus();
                return -1;
            }

            if (this.dtBegin.Value.TimeOfDay > this.dtEnd.Value.TimeOfDay)
            {
                MessageBox.Show("�Һſ�ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");
                this.dtEnd.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 40) == false)
            {
                MessageBox.Show("������������¼��20������!", "��ʾ");
                this.txtName.Focus();
                return -1;
            }
            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ�����Ա�!", "��ʾ");
                this.cmbSex.Focus();
                return -1;
            }

            if (this.cmbPayKind.Tag == null || this.cmbPayKind.Tag.ToString() == "")
            {
                MessageBox.Show("���߽��������Ϊ��!", "��ʾ");
                this.cmbPayKind.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text.Trim(), 20) == false)
            {
                MessageBox.Show("��ϵ�绰����¼��20λ����!", "��ʾ");
                this.txtPhone.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAddress.Text.Trim(), 50) == false)
            {
                MessageBox.Show("��ϵ�˵�ַ����¼��30������!", "��ʾ");
                this.txtAddress.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtMcardNo.Text.Trim(), 18) == false)
            {
                MessageBox.Show("ҽ��֤������¼��18λ����!", "��ʾ");
                this.txtMcardNo.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAge.Text.Trim(), 3) == false)
            {
                MessageBox.Show("��������¼��3λ����!", "��ʾ");
                this.txtAge.Focus();
                return -1;
            }
            if (IsLimit)
            {
                if ((this.txtPhone.Text == null || this.txtPhone.Text.Trim() == "") &&
                    (this.txtAddress.Text == null || this.txtAddress.Text.Trim() == ""))
                {
                    MessageBox.Show("��ϵ�绰�͵�ַ����ͬʱΪ��,��������һ��!", "��ʾ");
                    this.txtPhone.Focus();
                    return -1;
                }
            }

            if (this.txtAge.Text.Trim().Length > 0)
            {
                try
                {
                    int age = int.Parse(this.txtAge.Text.Trim());
                    if (age <= 0)
                    {
                        MessageBox.Show("���䲻��Ϊ����!", "��ʾ");
                        this.txtAge.Focus();
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("����¼���ʽ����ȷ!" + e.Message, "��ʾ");
                    this.txtAge.Focus();
                    return -1;
                }
            }
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;
            if (this.dtBirthday.Value.Date > current)
            {
                MessageBox.Show("�������ڲ��ܴ��ڵ�ǰʱ��!", "��ʾ");
                this.dtBirthday.Focus();
                return -1;
            }

            //У���ͬ��λ
            if (this.ValidCombox("��ѡ��ĺ�ͬ��λ������ں�ͬ��λ�������б���,������ѡ��") < 0)
            {
                this.cmbPayKind.Focus();
                return -1;
            }

            //У�����֤��
            //if (!string.IsNullOrEmpty(this.txtIdNO.Text))
            //{

            //    int reurnValue = this.ProcessIDENNO(this.txtIdNO.Text, EnumCheckIDNOType.Saveing);

            //    if (reurnValue < 0)
            //    {
            //        return -1;
            //    }
            //}

            string idno = this.txtIdNO.Text.Trim();
            //��У��۰����֤�ţ�wangsc��2010-11-05
            if (!(idno.StartsWith("810000") || idno.StartsWith("820000")))
            {
                //У�����֤��
                if (!string.IsNullOrEmpty(this.txtIdNO.Text))
                {

                    int reurnValue = this.ProcessIDENNO(this.txtIdNO.Text, EnumCheckIDNOType.Saveing);

                    if (reurnValue < 0)
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        #region У���ͬ��λ
        /// <summary>
        /// У��combox
        /// </summary>
        private int ValidCombox(string ErrMsg)
        {
            int j = 0;
            for (int i = 0; i < this.cmbPayKind.Items.Count; i++)
            {
                if (this.cmbPayKind.Text.Trim() == this.cmbPayKind.Items[i].ToString())
                {

                    this.cmbPayKind.SelectedIndex = i;
                    j++;
                    break;

                }


            }
            //"��ѡ��ĺ�ͬ��λ������ں�ͬ��λ�������б���,������ѡ��"
            if (j == 0)
            {
                MessageBox.Show(ErrMsg);

                return -1;
            }
            return 1;


        }
        #endregion

        #endregion

        #region ��֤�˾��￨���Ƿ�ס��Ժ
        ///// <summary>
        ///// ��֤�˾��￨���Ƿ�ס��Ժ
        ///// </summary>
        ///// <param name="cardNO"></param>
        ///// <returns></returns>
        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        //private ArrayList ValidIsSendInhosCase(string cardNO)
        //{


        //    return patientMgr.GetPatientInfoHaveCaseByCardNO(cardNO);

        //}
        #endregion

        #region ��ȡ�Һ���Ϣ
        /// <summary>
        /// ��ȡ�Һ���Ϣ
        /// </summary>
        /// <returns></returns>
        private int getValue()
        {
            //�����
            this.regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
            this.regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//������

            this.regObj.DoctorInfo.Templet.RegLevel.ID = this.cmbRegLevel.Tag.ToString();
            this.regObj.DoctorInfo.Templet.RegLevel.Name = this.cmbRegLevel.Text;
            //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
            this.regObj.DoctorInfo.Templet.RegLevel.IsEmergency = (this.cmbRegLevel.SelectedItem as FS.HISFC.Models.Registration.RegLevel).IsEmergency;

            this.regObj.DoctorInfo.Templet.Dept.ID = this.cmbDept.Tag.ToString();
            this.regObj.DoctorInfo.Templet.Dept.Name = this.cmbDept.Text;

            this.regObj.DoctorInfo.Templet.Doct.ID = this.cmbDoctor.Tag.ToString();
            this.regObj.DoctorInfo.Templet.Doct.Name = this.cmbDoctor.Text;

            //{0BA561B1-376F-4412-AAD0-F19A0C532A03}
            this.regObj.Name = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'");//��������
            this.regObj.Sex.ID = this.cmbSex.Tag.ToString();//�Ա�

            string strAge = this.regMgr.GetAge(this.dtBirthday.Value, true);
            int age = FS.FrameWork.Function.NConvert.ToInt32(strAge.Substring(0, strAge.IndexOf('��')));
            if (age > 150)
            {
                MessageBox.Show("��������Ƿ������������룡");
                this.dtBirthday.Focus();
                return -1;
            }
            this.regObj.Birthday = this.dtBirthday.Value;//��������


            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;
            //��Ϊ��˵����ԤԼ��
            if (this.txtOrder.Tag != null)
            {
                this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Pre;
            }
            else if (level.IsSpecial)
            {
                this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Spe;
            }

            FS.HISFC.Models.Registration.Schema schema = null;

            //ֻ��ר�ҡ�ר�ơ�������Ҫ���뿴��ʱ��Ρ������޶�
            if (this.regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre
                        && (level.IsSpecial || level.IsFaculty || level.IsExpert))
            {
                schema = this.GetValidSchema(level);
                if (schema == null)
                {
                    MessageBox.Show("ԤԼʱ��ָ������,û�з����������Ű���Ϣ!", "��ʾ");
                    this.dtBookingDate.Focus();
                    return -1;
                }
                this.SetBookingTag(schema);
            }

            if (level.IsExpert && this.regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre)
            {
                if (this.VerifyIsProfessor(level, schema) == false)
                {
                    this.cmbRegLevel.Focus();
                    return -1;
                }
            }

            #region �������
            this.regObj.Pact.ID = this.cmbPayKind.Tag.ToString();//��ͬ��λ
            //this.regObj.Pact.Name = this.cmbPayKind.Text;

            FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
            if (pact == null || pact.ID == "")
            {
                MessageBox.Show("��ȡ����Ϊ:" + this.regObj.Pact.ID + "�ĺ�ͬ��λ��Ϣ����!" + this.conMgr.Err, "��ʾ");
                return -1;
            }
            this.regObj.Pact.Name = pact.Name;
            this.regObj.Pact.PayKind.Name = pact.PayKind.Name;
            this.regObj.Pact.PayKind.ID = pact.PayKind.ID;
            this.regObj.SSN = this.txtMcardNo.Text.Trim();//ҽ��֤��

            if (pact.IsNeedMCard && this.regObj.SSN == "")
            {
                MessageBox.Show("��Ҫ����ҽ��֤��!", "��ʾ");
                this.txtMcardNo.Focus();
                return -1;
            }
            //��Ա�������ж�
            if (this.validMcardNo(this.regObj.Pact.ID, this.regObj.SSN) == -1) return -1;

            #endregion

            this.regObj.PhoneHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtPhone.Text.Trim(), "'");//��ϵ�绰
            this.regObj.AddressHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtAddress.Text.Trim(), "'");//��ϵ��ַ
            this.regObj.CardType.ID = this.cmbCardType.Tag.ToString();

            #region ԤԼʱ���
            if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)//ԤԼ�ſ��Ű��޶�
            {
                this.regObj.IDCard = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).IDCard;
                this.regObj.DoctorInfo.Templet.Noon.ID = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.Noon.ID;
                this.regObj.DoctorInfo.Templet.IsAppend = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.IsAppend;
                this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                    this.dtBegin.Value.ToString("HH:mm:ss"));//�Һ�ʱ��
                this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
                this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                    this.dtEnd.Value.ToString("HH:mm:ss"));//����ʱ��
                this.regObj.DoctorInfo.Templet.ID = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.ID;
            }
            else if (level.IsSpecial || level.IsExpert || level.IsFaculty)//ר�ҡ�ר�ơ�����ſ��Ű��޶�
            {
                this.regObj.DoctorInfo.Templet.Noon.ID = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.Noon.ID;
                this.regObj.DoctorInfo.Templet.IsAppend = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.IsAppend;
                this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                    this.dtBegin.Value.ToString("HH:mm:ss"));//�Һ�ʱ��
                this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
                this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                    this.dtEnd.Value.ToString("HH:mm:ss"));//����ʱ��
                this.regObj.DoctorInfo.Templet.ID = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID;
            }
            else//�����Ų����޶�
            {
                this.regObj.DoctorInfo.SeeDate = this.regMgr.GetDateTimeFromSysDateTime();
                this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
                        this.dtBegin.Value.ToString("HH:mm:ss"));
                this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
                        this.dtEnd.Value.ToString("HH:mm:ss"));

                ///����Һ����ڴ��ڽ���,ΪԤԼ�����յĺ�,���¹Һ�ʱ��
                ///
                if (this.regObj.DoctorInfo.SeeDate.Date < this.dtBookingDate.Value.Date)
                {
                    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                        this.dtBegin.Value.ToString("HH:mm:ss"));//�Һ�ʱ��
                    this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
                    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                        this.dtEnd.Value.ToString("HH:mm:ss"));//����ʱ��

                    this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.Templet.Begin);
                }
                else
                {
                    this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.SeeDate);
                }


                if (this.regObj.DoctorInfo.Templet.Noon.ID == "")
                {
                    MessageBox.Show("δά�������Ϣ,����ά��!", "��ʾ");
                    return -1;
                }
                this.regObj.DoctorInfo.Templet.ID = "";
            }
            #endregion

            if (this.regObj.Pact.PayKind.ID == "03")//���������ж�
            {
                if (this.IsAllowPubReg(this.regObj.PID.CardNO, this.regObj.DoctorInfo.SeeDate) == -1) return -1;
            }

            #region �Һŷ�
            int rtn = ConvertRegFeeToObject(regObj);
            if (rtn == -1)
            {
                MessageBox.Show("��ȡ�Һŷѳ���!" + this.regFeeMgr.Err, "��ʾ");
                this.cmbRegLevel.Focus();
                return -1;
            }
            if (rtn == 1)
            {
                MessageBox.Show("�ùҺż���δά���Һŷ�,����ά���Һŷ�!", "��ʾ");
                this.cmbRegLevel.Focus();
                return -1;
            }

            //��û���Ӧ�ա�����
            ConvertCostToObject(regObj);

            #endregion

            //������
            //  this.regObj.InvoiceNO = this.txtRecipeNo.Text.Trim();
            this.regObj.RecipeNO = this.txtRecipeNo.Text.Trim();


            this.regObj.IsFee = false;
            this.regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.regObj.IsSee = false;
            this.regObj.InputOper.ID = this.regMgr.Operator.ID;
            this.regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
            //add by niuxinyuan
            this.regObj.DoctorInfo.SeeDate = this.regObj.InputOper.OperTime;
            this.regObj.DoctorInfo.Templet.Noon.Name = this.QeryNoonName(this.regObj.DoctorInfo.Templet.Noon.ID);
            // add by niuxinyuan
            this.regObj.CancelOper.ID = "";
            this.regObj.CancelOper.OperTime = DateTime.MinValue;
            ArrayList al = new ArrayList();
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            //al = this.ValidIsSendInhosCase(this.regObj.PID.CardNO);
            //if (al != null && al.Count > 0)
            //{
            //    DialogResult result;
            //    result = MessageBox.Show("���ڴ˲����ŵ�סԺ��¼���Ƿ��򲡰��Ҵ���׼����ȡ������־", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //    {
            //        this.regObj.CaseState = "1";
            //    }
            //    else
            //    {
            //        this.regObj.CaseState = "0";
            //    }
            //}
            //���ܴ���

            if (this.chbEncrpt.Checked)
            {
                this.regObj.IsEncrypt = true;
                this.regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.regObj.Name);
                this.regObj.Name = "******";
            }

            this.regObj.IDCard = this.txtIdNO.Text;
            if (cmbCardType.Tag != null && !string.IsNullOrEmpty(cmbCardType.Tag.ToString()))
            {
                this.regObj.IDCardType.ID = this.cmbCardType.Tag.ToString();
            }
            this.regObj.IsFee = true;
            //ҽ��
            //if (this.regObj.Pact.ID == "2")
            //{
            //this.regObj.SIMainInfo = this.myYBregObj.SIMainInfo;

            //}
            return 0;
        }
        #endregion

        #region �ж��Ƿ��ʻ���������ʻ��������ʻ�
        ///// <summary>
        ///// �ж��Ƿ��ʻ���������ʻ��������ʻ�
        ///// </summary>
        ///// <returns></returns>
        //private int AccountPatient()
        //{
        //    decimal vacancy = 0;
        //    decimal OwnCostTot = this.regObj.OwnCost;
        //    int result = this.feeMgr.GetAccountVacancy(this.regObj.PID.CardNO, ref vacancy);
        //    if (result < 0)
        //    {
        //        MessageBox.Show(this.feeMgr.Err);
        //        return -1;
        //    }



        //    if (result > 0)
        //    {   //����ʻ�������0���ԷѴ���ֱ��������
        //        if (vacancy == 0)
        //        {
        //            return 1;
        //        }
        //        //if (IsAccountMessage)
        //        //{
        //        //    DialogResult diaResult = MessageBox.Show("�Ƿ�ʹ���ʻ�֧����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        //    if (diaResult == DialogResult.No)
        //        //    {
        //        //        return 1;
        //        //    }
        //        //}
        //        //������ 
        //        if (vacancy < this.regObj.OwnCost)
        //        {
        //            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        //            //bool returnValue = this.feeMgr.AccountPay(this.regObj.PID.CardNO, vacancy, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID);
        //            if (!feeMgr.CheckAccountPassWord(this.regObj))
        //            {
        //                return -1;
        //            }
        //            int returnValue = this.feeMgr.AccountPay(this.regObj, vacancy, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R");
        //            {
        //                if (returnValue < 0)
        //                {
        //                    MessageBox.Show(this.feeMgr.Err);
        //                    return -1;
        //                }
        //                this.regObj.PayCost = vacancy;
        //                this.regObj.OwnCost = this.regObj.OwnCost - vacancy;
        //            }

        //        }
        //        else //����
        //        {
        //            if (!feeMgr.CheckAccountPassWord(this.regObj))
        //            {
        //                return -1;
        //            }

        //            int returnValue = this.feeMgr.AccountPay(this.regObj,this.regObj.OwnCost, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R");
        //            //if (returnValue == false)
        //            if (returnValue < 0)
        //            {
        //                MessageBox.Show(this.feeMgr.Err);
        //                return -1;
        //            }
        //            this.regObj.PayCost = this.regObj.OwnCost;
        //            this.regObj.OwnCost = 0;

        //        }

        //    }


        //    return 1;
        //}
        #endregion

        #region У��ҽ������
        /// <summary>
        /// �ж�ҽ��֤���Ƿ��Ѿ�����
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="mcardNo"></param>
        /// <returns></returns>
        private int validMcardNo(string pactID, string mcardNo)
        {
            //��Ժְ���ж�ҽ��֤���Ƿ񶳽�
            //FS.HISFC.BizLogic.Medical.MedicalCard mCardMgr = new FS.HISFC.BizLogic.Medical.MedicalCard();

            //if (!mCardMgr.isValidInnerEmployee(pactID, mcardNo))
            //{
            //    MessageBox.Show("��Ժְ��ҽ��֤�ѱ�����,����ʹ��!");
            //    this.cmbPayKind.Focus();
            //    return -1;
            //}

            ////�ж�ҽ��֤�Ƿ���ȫԺ������
            //FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();

            //if (interfaceMgr.ExistBlackList(pactID, mcardNo))
            //{
            //    MessageBox.Show("�û���ҽ��֤����Ա��������,���ܹҺ�!");
            //    this.cmbPayKind.Focus();
            //    return -1;
            //}

            return 0;
        }
        #endregion

        #region �жϻ����籣���
        /// <summary>
        /// �жϻ����籣���
        /// </summary>
        /// <returns></returns>
        private int JuageMedicareStatus()
        {
            try
            {
                this.MedcareInterfaceProxy.SetPactCode(this.cmbPayKind.Tag.ToString());
                this.MedcareInterfaceProxy.Connect();
                FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
                r.Name = this.txtName.Text;
                r.IDCard = this.txtIdNO.Text;
                if (this.MedcareInterfaceProxy.QueryCanMedicare(r) == -1)
                {
                    MessageBox.Show("�籣�Һ�ʧ�ܣ�\n���黼����Ϣ�Ƿ�����");
                    return -1;
                }
                else
                {
                    if ((this.cmbPayKind.Tag.ToString() == "2" || this.cmbPayKind.Tag.ToString() == "3")
                    && r.Pact.ID != this.cmbPayKind.Tag.ToString())
                    {
                        this.cmbPayKind.Tag = r.Pact.ID;
                    }
                }
                this.MedcareInterfaceProxy.Disconnect();
            }
            catch
            {
                return 1;
            }

            return 1;
        }
        #endregion

        #region ���»��߻�����Ϣ
        /// <summary>
        /// ���»��߻�����Ϣ
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="patMgr"></param>
        /// <param name="registerMgr"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdatePatientinfo(FS.HISFC.Models.Registration.Register regInfo,
            FS.HISFC.BizProcess.Integrate.RADT patMgr, FS.HISFC.BizLogic.Registration.Register registerMgr,
            ref string Err)
        {
            int rtn = registerMgr.Update(FS.HISFC.BizLogic.Registration.EnumUpdateStatus.PatientInfo,
                                            regInfo);

            if (rtn == -1)
            {
                Err = registerMgr.Err;
                return -1;
            }

            if (rtn == 0)//û�и��µ�������Ϣ������
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

                p.PID.CardNO = regInfo.PID.CardNO;
                p.Name = regInfo.Name;
                p.Sex.ID = regInfo.Sex.ID;
                p.Birthday = regInfo.Birthday;
                p.Pact = regInfo.Pact;
                p.Pact.PayKind.ID = regInfo.Pact.PayKind.ID;
                p.SSN = regInfo.SSN;
                p.PhoneHome = regInfo.PhoneHome;
                p.AddressHome = regInfo.AddressHome;
                p.IDCard = regInfo.IDCard;
                p.IDCardType = regInfo.CardType;
                p.NormalName = regInfo.NormalName;
                p.IsEncrypt = regInfo.IsEncrypt;

                if (patientMgr.RegisterComPatient(p) == -1)
                {
                    Err = patientMgr.Err;
                    return -1;
                }

            }
            if (feeMgr.UpdatePatientIden(regInfo) < 0)
            {
                Err = "���滼��֤����Ϣʧ�ܣ�" + feeMgr.Err;
                return -1;
            }

            return 0;
        }
        #endregion

        #region ҽ���ӿ�,������
        /*
        /// <summary>
        /// ҽ���ӿ�
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="MedMgr"></param>
        /// <param name="ifeMgr"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int RegSI(FS.HISFC.Models.Registration.Register reg,
            MedicareInterface.Class.Clinic MedMgr, FS.HISFC.BizLogic.Fee.Interface ifeMgr, ref string Err)
        {
            //����ҽ��
            //if (MedMgr.Connect(reg.Pact.ID) == false)
            //{
            //    Err = MedMgr.ErrMsg;
            //    return -1;
            //}

            ////��ȡҽ���Ǽ���Ϣ
            //if (!MedMgr.Reg(ref reg))
            //{
            //    Err = MedMgr.ErrMsg;
            //    return -1;
            //}

            //���浽����
            //			if( ifeMgr.InsertSIMainInfo(reg) == -1)
            //			{
            //				Err = ifeMgr.Err ;
            //				return -1 ;
            //			}

            //�Ͽ�����
            //if (!MedMgr.DisConnect(reg.Pact.ID))
            //{
            //    Err = MedMgr.ErrMsg;
            //    return -1;
            //}

            return 0;
        }*/

        #endregion

        #region ��֤
        /// <summary>
        /// ��ȡ��Ч���Ű���Ϣ��������
        /// ���Ǵ���Ŀ�б�ѡ����ʱ���,����ֱ������
        /// ����ʱ���
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema(FS.HISFC.Models.Registration.RegLevel level)
        {
            FS.HISFC.Models.Registration.Schema schema = (FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag;
            if (schema != null) return schema;

            DateTime bookingDate = this.dtBookingDate.Value.Date;
            al = null;

            if (level.IsFaculty)//ר�ƺ�
            {
                al = this.SchemaMgr.QueryByDept(bookingDate.Date, this.cmbDept.Tag.ToString());
            }
            else if (level.IsExpert)//ר�Һ�
            {
                al = this.SchemaMgr.QueryByDoct(bookingDate.Date, this.cmbDoctor.Tag.ToString());
            }
            else if (level.IsSpecial)//�����
            {
                al = this.SchemaMgr.QueryByDoct(bookingDate.Date, this.cmbDoctor.Tag.ToString());
            }

            if (al == null || al.Count == 0) return null;

            return this.GetValidSchema(al, level);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Schemas"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema(ArrayList Schemas,
            FS.HISFC.Models.Registration.RegLevel level)
        {
            DateTime current = this.SchemaMgr.GetDateTimeFromSysDateTime();
            DateTime begin = this.dtBegin.Value;
            DateTime end = this.dtEnd.Value;

            string currentNoon = this.getNoon(current);

            foreach (FS.HISFC.Models.Registration.Schema obj in Schemas)
            {
                if (obj.SeeDate < current.Date) continue;//С�ڵ�ǰ����

                //ֻ�е��յĲ��ж�ʱ��
                if (obj.SeeDate == current.Date)
                {
                    if (obj.Templet.Begin.TimeOfDay != begin.TimeOfDay) continue;//��ʼʱ�䲻��
                    if (obj.Templet.End.TimeOfDay != end.TimeOfDay) continue;//����ʱ�䲻��
                }

                #region ��Ϊ�����޹Һ�,���Բ�����
                /*
                if(level.IsFaculty || level.IsExpert)
                {
                    if(!obj.Templet.IsAppend && obj.Templet.RegLmt == 0)continue ;//û���趨ԤԼ�޶�				
                    if(!obj.Templet.IsAppend && obj.Templet.RegLmt <= obj.RegedQty) continue;//�����޶�
                }
                else if(level.IsSpecial)
                {
                    if(!obj.Templet.IsAppend && obj.Templet.SpeLmt == 0)continue ;//û���趨ԤԼ�޶�				
                    if(!obj.Templet.IsAppend && obj.Templet.SpeLmt <= obj.SpeReged) continue;//�����޶�
                }*/
                #endregion

                if (!obj.Templet.IsAppend)
                {
                    //
                    //ֻ��������ͬ,���ж�ʱ���Ƿ�ʱ,�������ԤԼ���Ժ�����,ʱ�䲻���ж�
                    //
                    if (current.Date == obj.SeeDate)
                    {
                        if (obj.Templet.End.TimeOfDay < current.TimeOfDay) continue;//ʱ��С�ڵ�ǰʱ��
                    }
                }
                else
                {
                    if (obj.SeeDate.Date == current.Date)//���չҺ�,�ӺŲ���ȫΪ����,����ݵ�ǰʱ���ж�Ӧ�����绹������Ӻ�
                    {
                        if (currentNoon != obj.Templet.Noon.ID) continue;
                    }
                }

                return obj;
            }
            return null;
        }


        /// <summary>
        /// ���ѻ��������ж�
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        private int IsAllowPubReg(string cardNo, DateTime regDate)
        {
            int num = this.regMgr.QuerySeeNum(cardNo, regDate);
            if (num == -1)
            {
                MessageBox.Show(this.regMgr.Err, "��ʾ");
                return -1;
            }

            if (num >= this.DayRegNumOfPub)
            {
                DialogResult dr = MessageBox.Show("���ѻ��߹Һ�����:" + this.DayRegNumOfPub.ToString() + ", �û����ѹҺ�����:" +
                    num.ToString() + ",�Ƿ���������Һ�?", "��ʾ", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.No)
                {
                    this.txtCardNo.Focus();
                    return -1;
                }
            }

            return 0;
        }


        /// <summary>
        /// ���´�����		
        /// </summary>
        /// <param name="Cnt"></param>
        private void UpdateRecipeNo(int Cnt)
        {
            this.txtRecipeNo.Text = Convert.ToString(long.Parse(this.txtRecipeNo.Text.Trim()) + Cnt);
        }
        #endregion

        #region ���¿������
        /// <summary>
        /// ����ȫԺ�������
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(FS.HISFC.BizLogic.Registration.Register rMgr, DateTime current, ref int seeNo,
            ref string Err)
        {
            //���¿������
            //ȫԺ��ȫ����������������Ч��Ĭ�� 1
            if (rMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            //��ȡȫԺ�������
            if (rMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ����ҽ������ҵĿ������
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            #region �����źŷ�ʽ

            if (this.strGetSeeNoType == "1" &&
                doctID != null && doctID != "")
            {
                Type = "1";//ҽ��
                Subject = doctID;
            }
            else if (this.strGetSeeNoType == "2")
            {
                Type = "2";//����
                Subject = deptID;
            }
            else if (this.strGetSeeNoType == "3")
            {
                Type = "3";//����
                Subject = deptID;
            }

            #endregion

            //���¿������
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //��ȡ�������		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        #endregion

        #region ���¿����޶�
        /// <summary>
        /// ���¿����޶�
        /// </summary>
        /// <param name="SchMgr"></param>
        /// <param name="regType"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSchema(FS.HISFC.BizLogic.Registration.Schema SchMgr,
            FS.HISFC.Models.Base.EnumRegType regType, ref int seeNo, ref string Err)
        {
            int rtn = 1;
            //�Һż���
            FS.HISFC.Models.Registration.RegLevel level =
                                (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            if (regType == FS.HISFC.Models.Base.EnumRegType.Pre)//ԤԼ��,����ԤԼ�޶�
            {
                FS.HISFC.Models.Registration.Booking booking =
                                        (FS.HISFC.Models.Registration.Booking)this.txtOrder.Tag;

                rtn = SchMgr.Increase(booking.DoctorInfo.Templet.ID, false, false, true, false);

                //�ж��޶��Ƿ�����Һ�

                if (this.IsPermitOverrun(SchMgr, regType, booking.DoctorInfo.Templet.ID, level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }
            //else if(regType == FS.HISFC.Models.Registration.RegTypeNUM.Reg) 
            else if (level.IsFaculty || level.IsExpert)//ר�ҡ�ר��,�۹Һ��޶�
            {
                rtn = SchMgr.Increase(
                    (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                    true, false, false, false);

                //�ж��޶��Ƿ�����Һ�

                if (this.IsPermitOverrun(SchMgr, regType, (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                                            level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }
            //else if(regType == FS.HISFC.Models.Registration.RegTypeNUM.Spe) 
            else if (level.IsSpecial)//����������޶�
            {
                rtn = SchMgr.Increase(
                    (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                    false, false, false, true);

                //�ж��޶��Ƿ�����Һ�

                if (this.IsPermitOverrun(SchMgr, regType, (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                                    level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }

            if (rtn == -1)
            {
                Err = "�����Ű࿴���޶�ʱ����!" + SchMgr.Err;
                return -1;
            }

            if (rtn == 0)
            {
                Err = "ҽ���Ű���Ϣ�Ѿ��ı�,������ѡ����ʱ��!";
                return -1;
            }

            return 0;
        }
        #endregion

        #region �жϳ����Һ��޶��Ƿ�����Һ�
        /// <summary>
        /// �жϳ����Һ��޶��Ƿ�����Һ�
        /// </summary>
        /// <param name="schMgr"></param>
        /// <param name="regType"></param>
        /// <param name="schemaID"></param>
        /// <param name="level"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int IsPermitOverrun(FS.HISFC.BizLogic.Registration.Schema schMgr,
                    FS.HISFC.Models.Base.EnumRegType regType,
                    string schemaID, FS.HISFC.Models.Registration.RegLevel level,
                    ref int seeNo, ref string Err)
        {
            bool isOverrun = false;//�Ƿ񳬶�

            FS.HISFC.Models.Registration.Schema schema = schMgr.GetByID(schemaID);
            if (schema == null || schema.Templet.ID == "")
            {
                Err = "��ѯ�Ű���Ϣ����!" + schMgr.Err;
                return -1;
            }

            if (regType == FS.HISFC.Models.Base.EnumRegType.Pre)//ԤԼ��,�����ж��޶�,��ΪԤԼʱ�Ѿ��ж�
            {
                if (this.IsPreFirst)
                {
                    seeNo = int.Parse(schema.TeledQTY.ToString());
                }
                else
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReged + schema.SpeReged)) ;//��õ�ǰʱ���ѿ�����,��Ϊ�������к�
                    seeNo = schema.SeeNO;
                }
            }
            else if (level.IsExpert || level.IsFaculty)//ר�ҡ�ר���ж��޶��Ƿ�����ѹҺ�
            {
                if (schema.Templet.RegQuota - schema.RegedQTY < 0)
                {
                    isOverrun = true;
                }

                if (this.IsPreFirst)
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReging + schema.SpeReged)) ;//��õ�ǰʱ���ѿ�����,��Ϊ�������к�
                    seeNo = int.Parse(Convert.ToString(schema.SeeNO + schema.TelingQTY - schema.TeledQTY));
                }
                else
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReged + schema.SpeReged)) ;//��õ�ǰʱ���ѿ�����,��Ϊ�������к�
                    seeNo = schema.SeeNO;
                }
            }
            else if (level.IsSpecial)//�����ж������޶��Ƿ񳬱�
            {
                if (schema.Templet.SpeQuota - schema.SpedQTY < 0)
                {
                    isOverrun = true;
                }

                if (this.IsPreFirst)
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReging + schema.SpeReged)) ;//��õ�ǰʱ���ѿ�����,��Ϊ�������к�
                    seeNo = int.Parse(Convert.ToString(schema.SeeNO + schema.TelingQTY - schema.TeledQTY));
                }
                else
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReged + schema.SpeReged)) ;//��õ�ǰʱ���ѿ�����,��Ϊ�������к�
                    seeNo = schema.SeeNO;
                }
            }

            if (isOverrun)
            {
                //�ӺŲ�����ʾ
                if (schema.Templet.IsAppend) return 0;

                if (!this.IsAllowOverrun)
                {
                    Err = "�Ѿ����������Ű��޶�,���ܹҺ�!";
                    return -1;
                }
                else
                {
                    FS.HISFC.Components.Registration.frmWaitingAnswer f = new FS.HISFC.Components.Registration.frmWaitingAnswer();
                    DialogResult dr = f.ShowDialog();//��ֹ������3���ر�
                    f.Dispose();

                    //DialogResult dr = MessageBox.Show("�Һ����Ѿ����������,�Ƿ����?","��ʾ",MessageBoxButtons.YesNo,
                    //	MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) ;

                    //ѡ��No
                    if (dr == DialogResult.No)
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }
        #endregion

        #region ��ӡ�Һ�Ʊ
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj, FS.HISFC.BizLogic.Registration.Register regmr)
        {
            #region ����
            /*if( this.PrintWhat == "Invoice")//��ӡ��Ʊ
            {
                this.ucInvoice.Registeration = regObj ;
			
                System.Drawing.Printing.PaperSize size ;

                if( PrintCnt % 2 == 0)
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice1", 425 ,288);
                }
                else
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice2",425,280) ;
                }

                PrintCnt ++ ;

                printer.SetPageSize(size);
                printer.PrintPage(0,0,ucInvoice) ;
            }
            else//��ӡ����
            {
                //fuck
                FS.neuFC.Object.neuObject obj = this.conMgr.Get("PrintRecipe",regObj.RegDept.ID) ;

                //�������ģ�����ӡ
                if( obj == null || obj.ID == "")
                {
                    this.ucBill.Register = regObj ;
					
                    System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("Recipe", 670 ,1120);
                    printer.SetPageSize(size);
                    printer.PrintPage(0,0,this.ucBill) ;
                }
            }*/
            #endregion
            #region by niuxy
            /*
            try
            {
                if (IRegPrint != null)
                {
                    this.IRegPrint.RegInfo = regObj;
                    this.IRegPrint.Print();
                }
            }
            catch { }
             */
            #endregion

            //ʹ�ô�ӡ�������Ľӿ� ygch {2CF72240-2263-4de3-999E-3318956851BB}
            //FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            //regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            //regprint.SetPrintValue(regObj,regmr);
            FS.HISFC.BizProcess.Interface.IRecipePrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(
                typeof(FS.SOC.HISFC.Components.Register.SDFY.ucRegister), 
                typeof(FS.HISFC.BizProcess.Interface.IRecipePrint)) 
                as FS.HISFC.BizProcess.Interface.IRecipePrint;

            if (regObj.IsEncrypt)
            {
                regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.regObj.NormalName);
            }
            if (regprint == null) return;
            //����ӡ�Һ�Ʊ ��ӡ������ ygch {2CF72240-2263-4de3-999E-3318956851BB}
            //regprint.SetPrintValue(regObj);
            //regprint.Print();
            regprint.SetPatientInfo(regObj);
            regprint.PrintRecipe();
            //regprint.PrintView();
        }
        #endregion

        #region ����
        /// <summary>
        /// �ش�
        /// </summary>
        private void Reprint()
        {
            string Err = "";

            int row = this.fpList.ActiveRowIndex;

            //			if(row <0 || this.fpList.RowCount == 0) return ;

            FS.HISFC.Models.Registration.Register obj;

            //ʹ�ñ��ػ����ش򴦷����ؼ�(ԭΪ�Һŵ��ش�ؼ�)  ygch {2CF72240-2263-4de3-999E-3318956851BB}
            //FS.HISFC.Components.Registration.frmModifyRegistration f = new FS.HISFC.Components.Registration.frmModifyRegistration();
            frmModifyRegistration f = new frmModifyRegistration();
            DialogResult dr = f.ShowDialog();

            if (dr != DialogResult.OK) return;
            obj = f.Register;
            f.Dispose();

            //���»�ȡ�Һ���Ϣ
            /*obj = this.regMgr.QueryByClinic(obj.ID) ;

            if( obj == null|| obj.ID == null || obj.ID == "")
            {
                MessageBox.Show(this.regMgr.Err,"��ʾ") ;
                return ;
            }
				
            if(obj.IsValid != FS.HISFC.Models.Registration.RegisterStatusNUM.Valid||obj.IsBalance)
            {			
                MessageBox.Show("�ùҺ���Ϣ�Ѿ����ϻ����ս�,�����ش�!","��ʾ") ;
                return ;
            }		*/

            FS.HISFC.BizLogic.Registration.EnumUpdateStatus flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Cancel;
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //this.SIMgr.SetTrans(SQLCA);
                //this.InterfaceMgr.SetTrans(SQLCA.Trans);
                //this.assMgr.SetTrans(SQLCA.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (obj.InputOper.ID == regMgr.Operator.ID && obj.BalanceOperStat.IsCheck == false)
                {
                    #region ����
                    #endregion
                }
                else
                {
                    #region �˺�
                    FS.HISFC.Models.Registration.Register objReturn = obj.Clone();
                    objReturn.RegLvlFee.ChkFee = -obj.RegLvlFee.ChkFee;//����
                    objReturn.RegLvlFee.OwnDigFee = -obj.RegLvlFee.OwnDigFee;//����
                    objReturn.RegLvlFee.OthFee = -obj.RegLvlFee.OthFee;//������
                    objReturn.RegLvlFee.RegFee = -obj.RegLvlFee.RegFee;//�Һŷ�
                    objReturn.BalanceOperStat.IsCheck = false;//�Ƿ����
                    objReturn.BalanceOperStat.ID = "";
                    objReturn.BalanceOperStat.Oper.ID = "";
                    //objReturn.BeginTime = DateTime.MinValue; 
                    objReturn.CheckOperStat.IsCheck = false;//�Ƿ�˲�
                    objReturn.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Back;//�˺�
                    objReturn.InputOper.OperTime = current;//����ʱ��
                    objReturn.InputOper.ID = regMgr.Operator.ID;//������
                    objReturn.CancelOper.ID = regMgr.Operator.ID;//�˺���
                    objReturn.CancelOper.OperTime = current;//�˺�ʱ��
                    objReturn.OwnCost = -obj.OwnCost;//�Է�
                    objReturn.PayCost = -obj.PayCost;
                    objReturn.PubCost = -obj.PubCost;
                    objReturn.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                    if (this.regMgr.Insert(objReturn) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.regMgr.Err, "��ʾ");
                        return;
                    }

                    flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Return;
                    #endregion
                }

                obj.CancelOper.ID = regMgr.Operator.ID;
                obj.CancelOper.OperTime = current;

                //����ԭ�з�Ʊ
                int rtn = this.regMgr.Update(flag, obj);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "��ʾ");
                    return;
                }
                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�ùҺ���Ϣ�Ѿ�����,���ܲ���!", "��ʾ");
                    return;
                }
                //��շ�����Ϣ
                //if (this.assMgr.Delete(obj.ID) == -1)
                //{
                //    SQLCA.RollBack();
                //    MessageBox.Show("ɾ�����߷�����Ϣ����!" + this.assMgr.Err, "��ʾ");
                //    return;
                //}
                //��ȡ�µĴ�����
                obj.CancelOper.ID = "";
                obj.CancelOper.OperTime = DateTime.MinValue;
                obj.InvoiceNO = this.txtRecipeNo.Text.Trim();

                obj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
                obj.InputOper.ID = regMgr.Operator.ID;
                obj.InputOper.OperTime = current;

                //ҽ�����ߵǼ�ҽ����Ϣ
                if (obj.Pact.PayKind.ID == "02")
                {
                    //if (this.RegSI(obj, this.SIMgr, this.InterfaceMgr, ref Err) == -1)
                    //{
                    //    SQLCA.RollBack();
                    //    MessageBox.Show(Err, "��ʾ");
                    //    return;
                    //}
                }

                //�Ǽ��µĹҺ���Ϣ
                if (this.regMgr.Insert(obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "��ʾ");
                    return;
                }

                //���»��߻�����Ϣ
                if (this.UpdatePatientinfo(obj, this.patientMgr, this.regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return;
                }


                FS.FrameWork.Management.PublicTrans.Commit();

                //���Ӵ�����,��ֹ����
                this.UpdateRecipeNo(1);
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            this.Print(obj, this.regMgr);

            this.Retrieve();
            this.cmbRegLevel.Focus();
        }
        #endregion

        #region �Ҷ��ź�
        /// <summary>
        /// �Ҷ��ź�
        /// </summary>
        private void MultiReg()
        {

            if (this.valid() == -1)
            {

                return;//��֤������
            }

            int regNum = 0, rtn = 0;
            string Err = "";


            regNum = this.GetRegNum();
            if (regNum == -1)
            {

                return;
            }

            if (this.getValue() == -1)
            {

                return;
            }
            if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
            {

                MessageBox.Show("ԤԼ���߲��ܹҶ��ź�", "��ʾ");
                return;
            }

            if (this.regObj.Pact.PayKind.ID == "02")
            {

                MessageBox.Show("ҽ�����߲�����Ҷ��ź�!", "��ʾ");
                return;
            }

            ArrayList alRegs = new ArrayList();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.SchemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //��ǰ����
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
                //�Һż���
                FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                //�Ű�ʵ��
                FS.HISFC.Models.Registration.Schema schema = new FS.HISFC.Models.Registration.Schema();

                //��һ����,Ϊ������,������Ϊ�Ӻ�,�ۼӺ��Ű��޶�,���û�мӺ��Ű�,������

                for (int i = 1; i <= regNum; i++)
                {
                    //���˺�һ������2��
                    if (i == 2)//�ӵ�2����ʼ,�޸ĹҺ���ϢΪ�Ӻ�,���Ҷ�����ͬ���Ű���Ϣ���޶�
                    {
                        #region ""
                        this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;//�ӺŲ���Ϊ��ԤԼ��
                        //						this.regObj.RegDate = this.regObj.RegDate ;
                        //						this.regObj.BeginTime = this.regObj.BeginTime.Date ;
                        //						this.regObj.EndTime = this.regObj.EndTime.Date ;

                        //��ȥ�յ���
                        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
                        //if (this.IsKTF)
                        if (this.otherFeeType == "0")
                        {
                            this.regObj.OwnCost = this.regObj.OwnCost - this.regObj.RegLvlFee.OthFee;
                            this.regObj.RegLvlFee.OthFee = 0;
                        }

                        if (this.MultIsAppend)
                        {
                            this.regObj.DoctorInfo.Templet.IsAppend = true;//�Ӻ�						

                            ///����Һż�����Ҫ�����޶���¼���һ���Ӻ��Ű�ʵ��,��1���Ÿ���ԭ���Ű�ʵ�壬
                            ///��2���Ժ󶼸����µļӺ��Ű�ʵ��
                            ///
                            if (level.IsSpecial || level.IsExpert || level.IsFaculty)
                            {
                                string doctID = this.regObj.DoctorInfo.Templet.Doct.ID;

                                if (doctID == null || doctID == "") doctID = "None";

                                ///
                                ///���˺��Ժ���Ϊ�Ӻ�,�����ȼ���һ���Ӻ�,�Ժ�͸��¸��Ű���Ϣ
                                ///							

                                schema = SchemaMgr.QueryAppend(regObj.DoctorInfo.SeeDate.Date, regObj.DoctorInfo.Templet.Dept.ID,
                                    doctID, regObj.DoctorInfo.Templet.Noon.ID);

                                if (schema == null || schema.Templet.ID == "")
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�޼Ӻ��Ű���Ϣ!" + this.SchemaMgr.Err, "��ʾ");
                                    return;
                                }

                                this.regObj.DoctorInfo.Templet.ID = schema.Templet.ID;
                                this.regObj.DoctorInfo.Templet.Begin = schema.Templet.Begin;
                                this.regObj.DoctorInfo.Templet.End = schema.Templet.End;

                                this.SetBookingTag(schema);
                            }
                        }
                        #endregion
                    }

                    #region ���¿������
                    int orderNo = 0;

                    //2�������		
                    if (this.UpdateSeeID(regObj.DoctorInfo.Templet.Dept.ID, regObj.DoctorInfo.Templet.Doct.ID,
                        regObj.DoctorInfo.Templet.Noon.ID, regObj.DoctorInfo.SeeDate, ref orderNo, ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Err, "��ʾ");
                        return;
                    }

                    regObj.DoctorInfo.SeeNO = orderNo;
                    //ר�ҡ�ר�ơ����ԤԼ�Ÿ����Ű��޶�
                    #region schema
                    if (this.UpdateSchema(SchemaMgr, regObj.RegType, ref orderNo, ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        if (Err != "") MessageBox.Show(Err, "��ʾ");
                        return;
                    }

                    regObj.DoctorInfo.SeeNO = orderNo;
                    #endregion

                    //1ȫԺ��ˮ��			
                    if (this.Update(this.regMgr, current, ref orderNo, ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Err, "��ʾ");
                        return;
                    }

                    regObj.OrderNO = orderNo;
                    #endregion

                    //ԤԼ�Ÿ����ѿ����־
                    #region booking
                    if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                    {
                        //���¿����޶�
                        rtn = this.bookingMgr.Update((this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).ID,
                            true, regMgr.Operator.ID, current);
                        if (rtn == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ԤԼ������Ϣ����!" + this.bookingMgr.Err, "��ʾ");
                            return;
                        }
                        if (rtn == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("ԤԼ�Һ���Ϣ״̬�Ѿ����,�����¼���!", "��ʾ");
                            return;
                        }
                    }
                    #endregion

                    if (i > 1)
                    {
                        //this.regObj.InvoiceNO = Convert.ToString(long.Parse(this.regObj.InvoiceNO) + 1);//������					
                        this.regObj.RecipeNO = Convert.ToString(long.Parse(this.regObj.RecipeNO) + 1);
                    }

                    #region �ǼǹҺ���Ϣ


                    if (this.GetInvoiceType == 1)
                    {
                        //this.regObj.InvoiceNO = this.feeMgr.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.R);
                        this.regObj.InvoiceNO = this.feeMgr.GetNewInvoiceNO("R");
                        if (this.regObj.InvoiceNO == null || this.regObj.InvoiceNO == "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�ò���Աû�п���ʹ�õ�����Һŷ�Ʊ������ȡ");
                            return;
                        }
                    }
                    else if (this.GetInvoiceType == 2)
                    {
                        //this.regObj.InvoiceNO = this.feeMgr.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.C);
                        this.regObj.InvoiceNO = this.feeMgr.GetNewInvoiceNO("C");
                        if (this.regObj.InvoiceNO == null || this.regObj.InvoiceNO == "")
                        {
                            MessageBox.Show("�ò���Աû�п���ʹ�õ������շѷ�Ʊ������ȡ");
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return;
                        }
                    }
                    else
                    {
                        this.regObj.InvoiceNO = "";
                    }

                    if (i != 1) this.regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
                    if (this.regMgr.Insert(this.regObj) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.regMgr.Err, "��ʾ");
                        return;
                    }
                    #endregion

                    #region ���»�����Ϣ,ֻ����һ��
                    if (i == 1)
                    {
                        if (this.UpdatePatientinfo(this.regObj, this.patientMgr, this.regMgr,
                            ref Err) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Err, "��ʾ");
                            return;
                        }
                    }
                    #endregion

                    alRegs.Add(this.regObj.Clone());

                }
                FS.FrameWork.Management.PublicTrans.Commit();

                //���Ӵ�����,��ֹ����
                this.UpdateRecipeNo(regNum);
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            foreach (FS.HISFC.Models.Registration.Register obj in alRegs)
            {
                this.addRegister(obj);
                this.Print(obj, this.regMgr);
            }

            this.clear();
        }
        /// <summary>
        /// ��ȡ�Һ�����
        /// </summary>
        /// <returns></returns>
        private int GetRegNum()
        {
            int regNum = 0;

            FS.HISFC.Components.Registration.frmMultiReg f = new FS.HISFC.Components.Registration.frmMultiReg();
            DialogResult dr = f.ShowDialog();

            if (dr == DialogResult.OK)
            {
                regNum = f.RegNumber;
                f.Dispose();
                return regNum;
            }
            else
            {
                f.Dispose();
                return -1;
            }
        }
        #endregion

        #region ���޶�
        /// <summary>
        /// ���޶�
        /// </summary>
        /// <returns></returns>
        private int Reduce()
        {
            #region ��֤
            if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ��Һż���!", "��ʾ");
                this.cmbRegLevel.Focus();
                return -2;
            }

            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            //������ר�ҺŲ��ܿ��޶�
            if (!(level.IsExpert || level.IsFaculty || level.IsSpecial))
            {
                MessageBox.Show("��ר��/ר�ƺŲ��ܿ��޶�!", "��ʾ");
                this.cmbRegLevel.Focus();
                return -2;
            }

            if ((this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == ""))
            {
                MessageBox.Show("������Һſ���!", "��ʾ");
                this.cmbDept.Focus();
                return -2;
            }

            if ((level.IsExpert || level.IsSpecial) &&
                (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == ""))
            {
                MessageBox.Show("ר�Һű���ָ������ҽ��!", "��ʾ");
                this.cmbDoctor.Focus();
                return -2;
            }

            FS.HISFC.Models.Registration.Schema schema;//�Ű�ʵ��

            //��ѯ�����������Ű���Ϣ
            schema = this.GetValidSchema(level);
            if (schema == null)
            {
                MessageBox.Show("ԤԼʱ��ָ������,û�з����������Ű���Ϣ!", "��ʾ");
                this.dtBookingDate.Focus();
                return -2;
            }
            this.SetBookingTag(schema);

            #endregion

            int seeNo = 0;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.SchemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                string Err = "";

                #region ���¿������

                //��ȡ�������
                string noon = schema.Templet.Noon.ID;//���

                if (this.UpdateSeeID(this.cmbDept.Tag.ToString(), this.cmbDoctor.Tag.ToString(), noon, this.dtBookingDate.Value.Date,
                    ref seeNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                #endregion

                FS.HISFC.Models.Base.EnumRegType regType = FS.HISFC.Models.Base.EnumRegType.Reg;
                //��Ϊ��˵����ԤԼ��
                if (this.txtOrder.Tag != null)
                {
                    regType = FS.HISFC.Models.Base.EnumRegType.Pre;
                }
                else if (level.IsSpecial)
                {
                    regType = FS.HISFC.Models.Base.EnumRegType.Spe;
                }

                //�����Ű��޶�
                if (this.UpdateSchema(this.SchemaMgr, regType, ref seeNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (Err != "") MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                //��ȡȫԺ�������
                int i = 0;

                if (this.Update(this.regMgr, this.regMgr.GetDateTimeFromSysDateTime(), ref i, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            string Msg = "";

            Msg = "[" + seeNo.ToString() + "]";

            MessageBox.Show("���³ɹ�! ��ˮ��Ϊ:" + Msg, "��ʾ");
            this.clear();

            return 0;
        }
        #endregion

        #region �ݴ滼�߻�����Ϣ
        /// <summary>
        /// ���滼����Ϣ
        /// </summary>
        /// <returns></returns>
        private int SavePatient()
        {
            #region ��֤
            if (this.regObj == null)
            {
                MessageBox.Show("��¼��ҺŻ���!", "��ʾ");
                this.txtCardNo.Focus();
                return -1;
            }

            if (this.txtName.Text.Trim() == "")
            {
                MessageBox.Show("�����뻼������!", "��ʾ");
                this.txtName.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 40) == false)
            {
                MessageBox.Show("������������¼��20������!", "��ʾ");
                this.txtName.Focus();
                return -1;
            }

            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ�����Ա�!", "��ʾ");
                this.cmbSex.Focus();
                return -1;
            }

            if (this.cmbPayKind.Tag == null || this.cmbPayKind.Tag.ToString() == "")
            {
                MessageBox.Show("���߽��������Ϊ��!", "��ʾ");
                this.cmbPayKind.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text.Trim(), 20) == false)
            {
                MessageBox.Show("��ϵ�绰����¼��20λ����!", "��ʾ");
                this.txtPhone.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAddress.Text.Trim(), 60) == false)
            {
                MessageBox.Show("��ϵ�˵�ַ����¼��30������!", "��ʾ");
                this.txtAddress.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtMcardNo.Text.Trim(), 18) == false)
            {
                MessageBox.Show("ҽ��֤������¼��18λ����!", "��ʾ");
                this.txtMcardNo.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAge.Text.Trim(), 3) == false)
            {
                MessageBox.Show("��������¼��3λ����!", "��ʾ");
                this.txtAge.Focus();
                return -1;
            }

            if (this.txtAge.Text.Trim().Length > 0)
            {
                try
                {
                    int age = int.Parse(this.txtAge.Text.Trim());
                    if (age <= 0)
                    {
                        MessageBox.Show("���䲻��Ϊ����!", "��ʾ");
                        this.txtAge.Focus();
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("����¼���ʽ����ȷ!" + e.Message, "��ʾ");
                    this.txtAge.Focus();
                    return -1;
                }
            }
            #endregion

            this.regObj.Name = this.txtName.Text.Trim();//�������� 
            this.regObj.Sex.ID = this.cmbSex.Tag.ToString();//�Ա�
            this.regObj.Birthday = this.dtBirthday.Value;//��������
            this.regObj.Pact.ID = this.cmbPayKind.Tag.ToString();

            FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
            if (pact == null || pact.ID == "")
            {
                MessageBox.Show("��ȡ����Ϊ:" + this.regObj.Pact.ID + "�ĺ�ͬ��λ��Ϣ����!" + conMgr.Err, "��ʾ");
                return -1;
            }

            this.regObj.Pact.Name = pact.Name;
            this.regObj.Pact.PayKind = pact.PayKind;
            this.regObj.SSN = this.txtMcardNo.Text.Trim();
            this.regObj.PhoneHome = this.txtPhone.Text.Trim();
            this.regObj.AddressHome = this.txtAddress.Text.Trim();
            if (this.cmbCountry.Tag != null)
            {
                this.regObj.Country.ID = this.cmbCountry.Tag.ToString();
            }
            //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}
            this.regObj.IDCard = this.txtIdNO.Text;
            if (this.cmbCardType.Tag != null)
            {
                this.regObj.CardType.ID = this.cmbCardType.Tag.ToString();
            }
            else
            {
                this.regObj.CardType.ID = "";
            }
            if (this.chbEncrpt.Checked)
            {

                this.regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.regObj.Name);
                this.regObj.IsEncrypt = true;
                this.regObj.Name = "******";
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regMgr.con);
            //SQLCA.BeginTransaction();

            string Err = "";
            try
            {
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.UpdatePatientinfo(regObj, this.patientMgr, this.regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            MessageBox.Show("�ݴ�ɹ�!", "��ʾ");
            this.clear();

            return 0;
        }
        #endregion

        /// <summary>
        /// �ӿڳ�ʼ�� {E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        protected virtual int InitInterface()
        {
            this.iProcessRegiter = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter)) as FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter;

            return 1;
        }

        /// <summary>
        /// ���㴰��{F0661633-4754-4758-B683-CB0DC983922B}
        /// </summary>
        /// <returns></returns>
        protected virtual int ShowChangeForm(FS.HISFC.Models.Registration.Register regObj)
        {
            FS.HISFC.Components.Registration.Forms.frmReturnCost frmOpen = new FS.HISFC.Components.Registration.Forms.frmReturnCost();
            frmOpen.RegObj = regObj;
            DialogResult r = frmOpen.ShowDialog();


            return 1;
        }
        #endregion

        #region �¼�
        /// <summary>
        /// ��ݼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.F12)
            //{
            //    if (this.save() == -1)
            //    {
            //        this.cmbRegLevel.Focus();
            //    }
            //    return true;
            //}
            //else if (keyData == Keys.F3)
            //{
            //    if (this.Reduce() == -1)
            //    {
            //        this.cmbRegLevel.Focus();
            //    }
            //    return true;
            //}
            //else if (keyData == Keys.F8)
            //{
            //    this.clear();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();
            //    return true;
            //}
            if (keyData == Keys.F11)
            {
                this.txtOrder.Focus();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                #region ""
                bool IsSelect = false;

                if (this.ucChooseDate.Visible)
                {
                    IsSelect = true;
                    this.ucChooseDate.Visible = false;
                    this.dtBookingDate.Focus();
                }

                if (!IsSelect)//���ʲô��û��ʾ��Esc�رմ���
                {
                    this.FindForm().Close();
                }

                #endregion
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.P.GetHashCode())
            {
                this.Reprint();
                return true;
            }
            //else if (keyData == Keys.F4)
            //{
            //    this.MultiReg();

            //    return true;
            //}
            else if (keyData == Keys.F5)
            {
                System.Diagnostics.Process.Start("CALC.EXE");
                return true;
            }
            //else if (keyData == Keys.F6)
            //{
            //    SavePatient();

            //    return true;
            //}
            else if (keyData == Keys.F10)
            {
                this.cmbRegLevel.Focus();

                return true;
            }
            //else if (keyData == Keys.F1)
            //{
            //    this.AutoGetCardNO();
            //    this.txtCardNo.Focus();
            //}

            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// ���õ�ǰ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || this.fpList.RowCount == 0) return;
            this.fpList.ActiveRowIndex = e.Row;
        }

        /// <summary>
        /// ��ӡ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.regObjTemp == null || this.regObjTemp.ID == string.Empty)
            {
                MessageBox.Show("δѡ��ҺŻ��ߣ����ȱ���Һ���Ϣ��");
                return -1;
            }
            else
            {
                this.PrintRecipe(this.regObjTemp.ID);
            }
            return base.OnPrint(sender, neuObject);
        }
        #endregion

        #region IInterfaceContainer ��Ա

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint);
                //{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
                type[1] = typeof(FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter);

                return type;
            }
        }

        #endregion

        #region �˵�
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A����, true, false, null);
            toolBarService.AddToolButton("���ź�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolBarService.AddToolButton("���޶�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F�ֽ�, true, false, null);
            toolBarService.AddToolButton("�Ĵ�����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolBarService.AddToolButton("�ݴ�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z�ݴ�, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C�ش�, true, false, null);
            toolBarService.AddToolButton("���ɿ���", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.QȨ��, true, false, null);
            //{5BF35827-FF8E-4e23-A581-DFDE73EB95BE}
            toolBarService.AddToolButton("������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A����, true, false, null);
            toolBarService.AddToolButton("��ӡ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);

            return toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.save() == -1)
            {
                this.cmbRegLevel.Focus();
            }
            return base.OnSave(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                //case "����":
                //    if (this.save() == -1)
                //    {
                //        this.cmbRegLevel.Focus();
                //    }
                //    break;
                case "���ź�":
                    this.MultiReg();
                    break;
                case "���޶�":
                    if (this.Reduce() == -1)
                    {
                        this.cmbRegLevel.Focus();
                    }
                    break;
                case "�Ĵ�����":
                    this.ChangeRecipe();
                    break;
                case "�ݴ�":
                    SavePatient();
                    break;
                case "����":
                    clear();
                    break;
                case "����":
                    Reprint();
                    break;
                case "���ɿ���":
                    this.AutoGetCardNO();
                    break;
                //{5BF35827-FF8E-4e23-A581-DFDE73EB95BE}
                case "������":
                    {
                        this.chbBookFee.Checked = !this.chbBookFee.Checked;
                        break;
                    }
                case "����":
                    {
                        this.chbEncrpt.Checked = !this.chbEncrpt.Checked;
                        break;
                    }
                case "����":
                    this.GetIDInfo();
                    break;
                case "��ӡ����":
                    this.PrintBarCode();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        #region //ҽ���ӿ�
        #region ISIReadCard ��Ա
        /// <summary>
        /// ͨ��toolBar�Ķ��������ӿ�
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        public int ReadCard(string pactCode)
        {
            long returnValue = 0;
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            regObj = new FS.HISFC.Models.Registration.Register();

            //{04102034-382D-488e-BC45-F5B8CDBDE70D}
            regObj.Pact.ID = pactCode;

            returnValue = this.MedcareInterfaceProxy.SetPactCode(pactCode);
            if (returnValue != 1)
            {
                MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                // {DBCB798D-2F21-449e-BBE7-8F95E0F08B8A}
                if (this.MedcareInterfaceProxy.Rollback() < 0)
                {
                    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                    return -1;
                }

                return -1;
            }

            returnValue = this.MedcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                // {DBCB798D-2F21-449e-BBE7-8F95E0F08B8A}
                if (this.MedcareInterfaceProxy.Rollback() < 0)
                {
                    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                return -1;
            }

            returnValue = this.MedcareInterfaceProxy.GetRegInfoOutpatient(this.regObj);
            if (returnValue != 1)
            {
                MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                // {DBCB798D-2F21-449e-BBE7-8F95E0F08B8A}
                if (this.MedcareInterfaceProxy.Rollback() < 0)
                {
                    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                return -1;
            }

            returnValue = this.MedcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                // {DBCB798D-2F21-449e-BBE7-8F95E0F08B8A}
                if (this.MedcareInterfaceProxy.Rollback() < 0)
                {
                    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                    return -1;
                }

                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo p = null;

            p = radt.QueryComPatientInfoByMcardNO(this.regObj.SSN);
            if (p != null)
            {
                this.regObj.PID.CardNO = p.PID.CardNO;
                this.regObj.PhoneHome = p.PhoneHome;
                this.regObj.AddressHome = p.AddressHome;
            }
            this.regObj.User01 = "1";
            // this.regObj = myYBregObj;

            this.SetSIPatientInfo();
            this.SetEnabled(false);
            this.isReadCard = true;
            //this.registerControl.SetRegInfo();

            return 1;
        }
        /// <summary>
        /// ���ý��滼�߻�����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        public int SetSIPatientInfo()
        {
            this.txtCardNo.Text = this.regObj.PID.CardNO;
            this.txtName.Text = this.regObj.Name;
            this.cmbSex.Tag = this.regObj.Sex.ID;
            this.cmbPayKind.Tag = this.regObj.Pact.ID;
            this.txtMcardNo.Text = this.regObj.SSN;
            this.txtPhone.Text = this.regObj.PhoneHome;
            this.txtAddress.Text = this.regObj.AddressHome;
            this.cmbCountry.Tag = this.regObj.Country.ID;
            if (this.regObj.Birthday != DateTime.MinValue)
                this.dtBirthday.Value = this.regObj.Birthday;

            this.cmbCardType.Tag = this.regObj.CardType.ID;

            //{54603DD0-3484-4dba-B88A-B89F2F59EA40}
            if (this.isShowSIBalanceCost == true)
            {
                this.tbSIBalanceCost.Text = this.regObj.SIMainInfo.IndividualBalance.ToString();
            }

            this.setAgee(this.regObj.Birthday);
            this.getCost();
            return 1;
        }
        /// <summary>
        /// �Ƿ����
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetEnabled(bool Value)
        {
            //this.txtCardNo.Enabled = Value;
            this.txtName.Enabled = Value;
            this.cmbSex.Enabled = Value;
            this.txtMcardNo.Enabled = Value;
            this.cmbPayKind.Enabled = Value;
            this.dtBirthday.Enabled = Value;
            this.cmbCountry.Enabled = Value;
            //this.txtAge.Enabled = Value;
            this.txtAgee.Enabled = Value;
            this.cmbUnit.Enabled = Value;
            this.txtIdNO.Enabled = Value;
            this.cmbCardType.Enabled = Value;
            return 1;
        }
        #endregion

        private void cmbRegLevel_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        #endregion

        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        private void chbBookFee_CheckedChanged(object sender, EventArgs e)
        {
            //��ϴ�Һŷ�
            this.getCost();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}���֤��Ϣ
        private void txtIdNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string idNO = txtIdNO.Text.Trim();
            //01Ϊ���֤��
            if (!string.IsNullOrEmpty(cmbCardType.Tag.ToString()) && cmbCardType.Tag.ToString() == "01" && !string.IsNullOrEmpty(idNO))
            {
                //��У��۰����֤�ţ�wangsc��2010-11-05
                if (!(idNO.StartsWith("810000") || idNO.StartsWith("820000")))
                {
                    int returnValue = this.ProcessIDENNO(idNO, EnumCheckIDNOType.BeforeSave);
                    if (returnValue < 0)
                    {
                        return;
                    }
                }
                else
                {
                    this.setNextControlFocus();
                }
                //int returnValue = this.ProcessIDENNO(idNO, EnumCheckIDNOType.BeforeSave);
                //if (returnValue < 0)
                //{
                //    return;
                //}
            }
            else
            {
                this.setNextControlFocus();
            }
        }

        private int ProcessIDENNO(string idNO, EnumCheckIDNOType enumType)
        {
            string errText = string.Empty;

            //У�����֤��


            //{99BDECD8-A6FC-44fc-9AAA-7F0B166BB752}

            //string idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            string idNOTmp = string.Empty;
            if (idNO.Length == 15)
            {
                idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else
            {
                idNOTmp = idNO;
            }

            //У�����֤��
            int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);



            if (returnValue < 0)
            {
                MessageBox.Show(errText);
                this.txtIdNO.Focus();
                return -1;
            }
            string[] reurnString = errText.Split(',');
            if (enumType == EnumCheckIDNOType.BeforeSave)
            {
                this.dtBirthday.Text = reurnString[1];
                this.cmbSex.Text = reurnString[2];

                this.setAge(this.dtBirthday.Value);
                this.cmbPayKind.Focus();
            }
            else
            {
                if (this.dtBirthday.Text != reurnString[1])
                {
                    MessageBox.Show("������������������֤�����е����ղ���");
                    this.dtBirthday.Focus();
                    return -1;
                }

                if (this.cmbSex.Text != reurnString[2])
                {
                    MessageBox.Show("������Ա������֤�кŵ��Ա𲻷�");
                    this.cmbSex.Focus();
                    return -1;
                }
            }
            return 1;
        }

        ////{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}���֤��Ϣ
        private void dtBirthday_ValueChanged(object sender, EventArgs e)
        {
            //{AE0D67EA-32C9-46e2-8036-2EC797A13B94}
            //this.setAge(this.dtBirthday.Value);

            //��������ת��Ϊ��ȷ���� ygch{ABB75FA8-BE32-4cb6-AC22-1BE2928A7840}
            this.setAgee(this.dtBirthday.Value);
        }

        /// <summary>
        /// �ж����֤//{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}���֤��Ϣ
        /// </summary>
        private enum EnumCheckIDNOType
        {
            /// <summary>
            /// ����֮ǰУ��
            /// </summary>
            BeforeSave = 0,

            /// <summary>
            /// ����ʱУ��
            /// </summary>
            Saveing
        }
        /// <summary>
        /// ����ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chSpecialPerson_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chSpecialPerson.CheckState == CheckState.Checked && this.cmbDoctor.Tag != "")
            {
                this.SetSpecialPerson();
            }
            else
            {
                this.assignNO = string.Empty;
            }
        }

        /// <summary>
        /// �������˺������
        /// </summary>
        private void SetSpecialPerson()
        {
            this.assignNO = string.Empty;
            DateTime dt = this.regFeeMgr.GetDateTimeFromSysDateTime();
            string noon = this.getNoon(dt);
            ArrayList list = this.interMgr.GetBookNumForRegistration(this.cmbDoctor.Tag.ToString(), dt, noon);
            if (list != null && list.Count != 0)
            {
                this.ucAssign.Init(list);
                this.ucAssign.ShowDialog();

                if (this.assignNO == string.Empty)
                {
                    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("���˲�ʹ��Ԥ���ţ�"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.No)
                    {
                        MessageBox.Show("�����¹�ѡ���˿�!");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("û��Ԥ���ţ�");
            }
        }

        #region ���������վ�ȷ��Ӧ ygch{ABB75FA8-BE32-4cb6-AC22-1BE2928A7840}

        private void setAgee(DateTime birthday)
        {
            string age = this.regMgr.GetAge(birthday, true);
            string sYear = age.Substring(0, age.IndexOf("��"));
            string sMonth = age.Substring(age.IndexOf("��") + 1, age.IndexOf("��") - age.IndexOf("��") - 1);
            string sDay = age.Substring(age.IndexOf("��") + 1, age.IndexOf("��") - age.IndexOf("��") - 1);
            if (sYear.Length < 3)
            {
                sYear = sYear.PadLeft(3, '0');
            }
            if (sMonth.Length < 2)
            {
                sMonth = sMonth.PadLeft(2, '0');
            }
            if (sDay.Length < 2)
            {
                sDay = sDay.PadLeft(2, '0');
            }
            this.numTxtYear.TextChanged -= new EventHandler(numTxtYear_TextChanged);
            this.numTxtMonth.TextChanged -= new EventHandler(numTxtMonth_TextChanged);
            this.numTxtDay.TextChanged -= new EventHandler(numTxtDay_TextChanged);
            this.numTxtYear.Text = sYear;
            this.numTxtMonth.Text = sMonth;
            this.numTxtDay.Text = sDay;
            this.numTxtYear.TextChanged += new EventHandler(numTxtYear_TextChanged);
            this.numTxtMonth.TextChanged += new EventHandler(numTxtMonth_TextChanged);
            this.numTxtDay.TextChanged += new EventHandler(numTxtDay_TextChanged);
            //age = sYear + "��" + sMonth + "��" + sDay + "��";
            //this.txtAgee.TextChanged -= new EventHandler(txtAgee_TextChanged);
            //this.txtAgee.Text = age;
            //this.txtAgee.TextChanged += new EventHandler(txtAgee_TextChanged);
        }

        private void txtAgee_TextChanged(object sender, EventArgs e)
        {
            DateTime birthDay = this.regMgr.GetDateTimeFromSysDateTime();
            string ageStr = this.txtAgee.Text.Trim();
            ageStr = ageStr.Replace("_", "");
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(0, ageStr.IndexOf("��")));
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(ageStr.IndexOf("��") + 1, ageStr.IndexOf("��") - ageStr.IndexOf("��") - 1));
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(ageStr.IndexOf("��") + 1, ageStr.IndexOf("��") - ageStr.IndexOf("��") - 1));
            birthDay = birthDay.AddYears(-iyear).AddMonths(-iMonth).AddDays(-iDay - 1);
            this.dtBirthday.ValueChanged -= new EventHandler(dtBirthday_ValueChanged);
            if (birthDay < dtBirthday.MinDate || birthDay > dtBirthday.MaxDate)
            {
                MessageBox.Show("��������������������룡");
                this.txtAgee.Text = "___��__��__��";
                this.txtAgee.Focus();
                return;
            }
            this.dtBirthday.Value = birthDay;
            this.dtBirthday.ValueChanged += new EventHandler(dtBirthday_ValueChanged);
        }

        private void txtAgee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.getBirthday();

                this.txtPhone.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void numTxtYear_TextChanged(object sender, EventArgs e)
        {
            DateTime birthDay = this.regMgr.GetDateTimeFromSysDateTime();
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtYear.Text);
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtMonth.Text);
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtDay.Text);
            birthDay = birthDay.AddYears(-iyear).AddMonths(-iMonth).AddDays(-iDay - 1);
            this.dtBirthday.ValueChanged -= new EventHandler(dtBirthday_ValueChanged);
            if (birthDay < dtBirthday.MinDate || birthDay > dtBirthday.MaxDate)
            {
                MessageBox.Show("��������������������룡");
                this.numTxtYear.Focus();
                return;
            }
            this.dtBirthday.Value = birthDay;
            this.dtBirthday.ValueChanged += new EventHandler(dtBirthday_ValueChanged);
        }

        private void numTxtYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.numTxtMonth.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void numTxtMonth_TextChanged(object sender, EventArgs e)
        {
            DateTime birthDay = this.regMgr.GetDateTimeFromSysDateTime();
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtYear.Text);
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtMonth.Text);
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtDay.Text);
            birthDay = birthDay.AddYears(-iyear).AddMonths(-iMonth).AddDays(-iDay - 1);
            this.dtBirthday.ValueChanged -= new EventHandler(dtBirthday_ValueChanged);
            if (birthDay < dtBirthday.MinDate || birthDay > dtBirthday.MaxDate)
            {
                MessageBox.Show("��������������������룡");
                this.numTxtYear.Focus();
                return;
            }
            this.dtBirthday.Value = birthDay;
            this.dtBirthday.ValueChanged += new EventHandler(dtBirthday_ValueChanged);
        }

        private void numTxtMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.numTxtDay.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void numTxtDay_TextChanged(object sender, EventArgs e)
        {
            DateTime birthDay = this.regMgr.GetDateTimeFromSysDateTime();
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtYear.Text);
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtMonth.Text);
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(this.numTxtDay.Text);
            birthDay = birthDay.AddYears(-iyear).AddMonths(-iMonth).AddDays(-iDay - 1);
            this.dtBirthday.ValueChanged -= new EventHandler(dtBirthday_ValueChanged);
            if (birthDay < dtBirthday.MinDate || birthDay > dtBirthday.MaxDate)
            {
                MessageBox.Show("��������������������룡");
                this.numTxtYear.Focus();
                return;
            }
            this.dtBirthday.Value = birthDay;
            this.dtBirthday.ValueChanged += new EventHandler(dtBirthday_ValueChanged);
        }

        private void numTxtDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPhone.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        #endregion

        #region ��ȡ���֤�ӿ�
        private int GetIDInfo()
        {
            string IDCode = string.Empty;
            string Name = string.Empty;
            string Sex = string.Empty;
            DateTime BirthDay = DateTime.Now;
            string Nation = string.Empty;
            string Address = string.Empty;
            string Agency = string.Empty;
            DateTime ExpireStart = DateTime.Now;
            DateTime ExpireEnd = DateTime.Now;
            string Message = string.Empty;

            if (this.interMgr.GetIDCardInfo(ref IDCode, ref Name, ref Sex, ref BirthDay, ref Nation, ref Address, ref Agency, ref ExpireStart, ref ExpireEnd, ref Message) == -1)
            {
                MessageBox.Show(Message);
                return -1;
            }
            this.txtName.Text = Name;
            this.txtIdNO.Text = IDCode;
            if (Sex == "1")
            {
                this.cmbSex.Tag = "M";
            }
            else if (Sex == "2")
            {
                this.cmbSex.Tag = "F";
            }
            else
            {
                this.cmbSex.Tag = "O";
            }
            this.dtBirthday.Value = BirthDay;
            this.txtAddress.Text = Address;

            //MessageBox.Show(IDCode + Name + Sex + BirthDay.ToString() + Nation + Address + Agency + ExpireStart.ToString() + ExpireEnd.ToString() + Message);
            return 1;
        }
        #endregion

        #region ��ӡ����������
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <returns></returns>
        private int PrintBarCode()
        {
            //PPLADLL Test code start
            int nUSBDataLen;
            byte[] pbuf;

            nUSBDataLen = PrintLab.A_GetUSBBufferLen() + 1;
            pbuf = new byte[nUSBDataLen];
            PrintLab.A_EnumUSB(pbuf);
            PrintLab.A_CreateUSBPort(printer_port);

            PrintLab.A_Clear_Memory();
            PrintLab.A_Set_Darkness(12);
            PrintLab.A_Prn_Text_TrueType(62, 35, 24, "����", 1, 600, 0, 0, 0, "AL", this.txtCardNo.Text, 1);
            PrintLab.A_Prn_Barcode(28, 50, 1, 'e', 0, 4, 40, 'N', 0, this.txtCardNo.Text);
            //A_Prn_Barcode(15, 68, 1, 'e', 0, 4, 40, 'N', 0, "C" + bar_code);
            PrintLab.A_Print_Out(1, 1, 1, 1);
            PrintLab.A_ClosePrn();

            return 1;
        }

        private uint UmmTodot(int mm)
        {
            return (uint)(mm / 0.125);
        }

        private int MmTodot(int mm)
        {
            return (int)(mm / 0.125);
        }
        #endregion
    }
    /// <summary>
    /// �������뷽ʽ
    /// </summary>
    public enum EnumBirthdayInputType
    {
        /// <summary>
        /// ֻ�����������
        /// </summary>
        Birthday = 0,
        /// <summary>
        /// ֻ��������
        /// </summary>
        Age = 1,
        /// <summary>
        /// ���߶�����
        /// </summary>
        Both = 2
    }

    /// <summary>
    /// �����źŷ�ʽ
    /// </summary>
    public enum EnumGetSeeNoType
    {
        /// <summary>
        /// ҽ��
        /// </summary>
        Doct = 0,
        /// <summary>
        /// ����
        /// </summary>
        Dept,
        /// <summary>
        /// ����
        /// </summary>
        Nurse
    }

    /// <summary>
    /// ��ӡ����������
    /// </summary>
    public class PrintLab
    {
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_Maxi(int x, int y, int primary, int secondary, int country, int service, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_Maxi_Ori(int x, int y, int ori, int primary, int secondary, int country, int service, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_PDF417(int x, int y, int narrow, int width, char normal, int security, int aspect, int row, int column, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_PDF417_Ori(int x, int y, int ori, int narrow, int width, char normal, int security, int aspect, int row, int column, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_DataMatrix(int x, int y, int rotation, int hor_mul, int ver_mul, int ECC, int data_format, int num_rows, int num_col, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern void A_Clear_Memory();
        [DllImport("Winppla.dll")]
        public static extern void A_ClosePrn();
        [DllImport("Winppla.dll")]
        public static extern int A_CreatePrn(int selection, string filename);
        [DllImport("Winppla.dll")]
        public static extern int A_Del_Graphic(int mem_mode, string graphic);
        [DllImport("Winppla.dll")]
        public static extern int A_Draw_Box(char mode, int x, int y, int width, int height, int top, int side);
        [DllImport("Winppla.dll")]
        public static extern int A_Draw_Line(char mode, int x, int y, int width, int height);
        [DllImport("Winppla.dll")]
        public static extern void A_Feed_Label();
        [DllImport("Winppla.dll")]
        public static extern int A_Get_DLL_VersionA(int nShowMessage);
        [DllImport("Winppla.dll")]
        public static extern int A_Get_Graphic(int x, int y, int mem_mode, char format, string filename);
        [DllImport("Winppla.dll")]
        public static extern int A_Get_Graphic_ColorBMP(int x, int y, int mem_mode, char format, string filename);
        [DllImport("Winppla.dll")]
        public static extern int A_Initial_Setting(int Type, string Source);
        [DllImport("Winppla.dll")]
        public static extern int A_Load_Graphic(int x, int y, string graphic_name);
        [DllImport("Winppla.dll")]
        public static extern int A_Open_ChineseFont(string path);
        [DllImport("Winppla.dll")]
        public static extern int A_Print_Form(int width, int height, int copies, int amount, string form_name);
        [DllImport("Winppla.dll")]
        public static extern int A_Print_Out(int width, int height, int copies, int amount);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Barcode(int x, int y, int ori, char type, int narrow, int width, int height, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text(int x, int y, int ori, int font, int type, int hor_factor, int ver_factor, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text_Chinese(int x, int y, int fonttype, string id_name, string data, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text_TrueType(int x, int y, int FSize, string FType, int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut, string id_name, string data, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text_TrueType_W(int x, int y, int FHeight, int FWidth, string FType, int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut, string id_name, string data, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Backfeed(int back);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_BMPSave(int nSave, string pstrBMPFName);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Cutting(int cutting);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Darkness(int heat);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_DebugDialog(int nEnable);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Feed(char rate);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Form(string formfile, string form_name, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Margin(int position, int margin);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Prncomport(int baud, int parity, int data, int stop);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Prncomport_PC(int nBaudRate, int nByteSize, int nParity, int nStopBits, int nDsr, int nCts, int nXonXoff);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Sensor_Mode(char type, int continuous);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Speed(char speed);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Syssetting(int transfer, int cut_peel, int length, int zero, int pause);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Unit(char unit);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Logic(int logic);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_ProcessDlg(int nShow);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_LabelVer(int centiInch);
        [DllImport("Winppla.dll")]
        public static extern int A_GetUSBBufferLen();
        [DllImport("Winppla.dll")]
        public static extern int A_EnumUSB(byte[] buf);
        [DllImport("Winppla.dll")]
        public static extern int A_CreateUSBPort(int nPort);
        [DllImport("Winppla.dll")]
        public static extern int A_CreatePort(int nPortType, int nPort, string filename);
    }
}
