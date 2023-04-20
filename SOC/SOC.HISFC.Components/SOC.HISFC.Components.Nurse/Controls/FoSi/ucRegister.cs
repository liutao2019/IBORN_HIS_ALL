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
    /// ����ע�����
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

        #region ������

        /// <summary>
        /// Ժע������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// �ҺŹ���
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// �����շѹ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 
        /// </summary>
        FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();

        /// <summary>
        /// ���ù���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee interFeeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// �ҺŹ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration interRegMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// �Һ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Registration.Register reg = null;

        /// <summary>
        /// ����ҵ��㺯��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ҩ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy interPhaMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ҽ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order interOrderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// δ��ӡ���˵�������·��
        /// </summary>
        protected string filePathInjectNoPrintPatient = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\InjectNoPrintPatient.xml";

        /// <summary>
        /// ע�䵥��ʾ���������ļ�
        /// </summary>
        private string injectRegisterXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\injectRegister.xml";

        /// <summary>
        /// �Ѵ�ӡ���˵�������·��
        /// </summary>
        protected string filePathInjectPrintPatient = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\InjectPrintPatient.xml";

        //private ArrayList al = new ArrayList();


        FS.HISFC.Models.Pharmacy.Item drug = null;

        FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// ���Ƶ�������
        /// </summary>
        private ArrayList alZLDPrint = null;

        /// <summary>
        /// ע�䵥������
        /// </summary>
        private ArrayList alInjectPrint = null;

        /// <summary>
        /// �Ƿ��ش�
        /// </summary>
        private bool isReprint = false;

        /// <summary>
        /// ҽ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper emplHelper = null;

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        Hashtable hsInfos = new Hashtable();

        /// <summary>
        /// �Ƿ��һ�εǼ�
        /// </summary>
        private bool IsFirstTime = false;

        /// <summary>
        /// �Ƿ�Ǽ���Ժע
        /// </summary>
        private bool isRegNullNum = true;

        /// <summary>
        /// �Ƿ��Զ��������к�
        /// </summary>
        private bool isAutoBuildOrderNo = true;

        /// <summary>
        /// �Ƿ���ʾ��Ժע
        /// </summary>
        private bool isRegFinishNum = false;

        /// <summary>
        /// �Ƿ���˿������  
        /// </summary>
        private bool isFilterDoctDept = false;

        /// <summary>
        /// Ժע����
        /// </summary>
        private int countInject = 0;

        /// <summary>
        /// ���ע��˳���
        /// </summary>
        private int maxInjectOrder = 0;

        /// <summary>
        /// ��ӡ��Ѳ�ӿ��ϵ��÷�
        /// </summary>
        private string usage = "01";

        /// <summary>
        /// ��ҩƷ��ӡ��Ѳ�ӿ��ϵ��÷�
        /// </summary>
        private string unDrugUsage = "";

        /// <summary>
        /// �ڵ�ǰע���Ҵ�ӡ�Ŀ���
        /// </summary>
        private string doctDept = "";

        /// <summary>
        /// ע���÷�����������Һ��Ŀ��
        /// </summary>
        private string injectUsage = "";

        #region ע��˳���
        /// <summary>
        /// �Ƿ��Զ�����ע��˳���
        /// </summary>
        private bool IsAutoOrder = true;
        /// <summary>
        /// ��ǰע��˳���
        /// </summary>
        private int currentOrder = 0;
        #endregion

        /// <summary>
        /// �Ƿ���ʾ���ߵ���ɵǼǵ�ȫ������
        /// </summary>
        private bool isShowAllInject = false;

        /// <summary>
        /// �Ƿ���ʾ���ض��÷��ķ�ҩƷ
        /// </summary>
        private bool isShowUnDrug = false;

        /// <summary>
        /// �Ƿ����˵�
        /// </summary>
        private bool isQuit = false;

        /// <summary>
        /// Ƶ�ΰ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freqHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ȡע����Žӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo IGetOrderNo = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ���ߵ���ɵǼǵ�ȫ������
        /// </summary>
        [Description("�Ƿ���ʾ���ߵ���ɵǼǵ�ȫ������"), Category("����"), DefaultValue("false")]
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
        /// �Ƿ��Զ��������к�
        /// </summary>
        [Description("�Ƿ��Զ��������к�"), Category("����")]
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
        /// �ڵ�ǰע���Ҵ�ӡ�Ŀ���
        /// </summary>
        [Description("�ڵ�ǰע���Ҵ�ӡ�Ŀ��ң���ά��Ĭ��ȫ������'1001','2001','3001'��ʽά��"), Category("����")]
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
        /// �Ƿ���ʾ���ض��÷��ķ�ҩƷ
        /// </summary>
        [Description("�Ƿ���ʾ���ض��÷��ķ�ҩƷ"), Category("����")]
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
        /// �Ƿ���˿������
        /// </summary>
        [Description("�Ƿ���˿������"), Category("����")]
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
        /// �Ƿ�Ǽ���Ժע
        /// </summary>
        [Description("�Ƿ�Ǽ���Ժע"), Category("����")]
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
        /// �Ƿ���ʾ��Ժע
        /// </summary>
        [Description("�Ƿ���ʾ��Ժע"), Category("����")]
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
        /// �÷��Ƿ��ӡ��Ѳ�ӿ���
        /// </summary>
        [Description("�÷���ID�Ƿ��ӡ��Ѳ�ӿ��ϣ���'001','002','003'��ʽά��"), Category("����")]
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
        /// ��ҩƷ��ӡ��Ѳ�ӿ��ϵ��÷�
        /// </summary>
        [Description("��ҩƷ�÷���ID��ӡ��Ѳ�ӿ��ϣ���'001','002','003'��ʽά��"), Category("����")]
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
        /// ע���÷���IDά������������Һ�÷�,�ɲ�ά��������'001','002','003'��ʽά��
        /// </summary>
        [Description("ע���÷���IDά������������Һ�÷�,�ɲ�ά��������'001','002','003'��ʽά��"), Category("����")]
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
        /// �Ƿ��Զ���ӡѲ�ӿ�
        /// </summary>
        private bool isAutoPrint = true;

        /// <summary>
        /// �Ƿ��Զ���ӡѲ�ӿ�
        /// </summary>
        [Description("�Ƿ��Զ���ӡѲ�ӿ�"), Category("����")]
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
        /// �Ƿ��Զ���������
        /// </summary>
        private bool isAutoSave = true;

        /// <summary>
        /// �Ƿ��Զ���������
        /// </summary>
        [Description("�Ƿ��Զ���������"), Category("����")]
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
        /// �Ƿ�ʹ��ע��˳���
        /// </summary>
        private bool isUserOrderNumber = false;

        /// <summary>
        /// �Ƿ�ʹ��ע��˳���
        /// </summary>
        [Description("�Ƿ�ʹ��ע��˳���"), Category("����")]
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
        /// �Զ���ӡʱ�����Ƿ���ʾ
        /// </summary>
        private bool isMessageInSave = true;

        /// <summary>
        /// �Զ���ӡʱ�����Ƿ���ʾ
        /// </summary>
        [Description("�Զ���ӡʱ�����Ƿ���ʾ"), Category("����")]
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
        /// ��ѯ�ĹҺŵ����ڼ��
        /// </summary>
        private int queryRegDays = 2;

        /// <summary>
        /// ��ѯ�ĹҺŵ����ڼ��
        /// </summary>
        [Category("��ѯ����"), Description("��ѯ�Һ���Ϣ�����ڼ�����ӽ����ϵĿ�ʼʱ����ǰ��ѯ���죩")]
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
        /// ��ʼʱ������ڼ��(�����ļ������)
        /// </summary>
        private int beginDateIntervalDays = 0;

        /// <summary>
        /// ��ʼʱ������ڼ��(�����ļ������)
        /// </summary>
        [Category("��ѯ����"), Description("��ʼʱ������ڼ��(�����ļ������)")]
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

        #region �Զ�ˢ��

        /// <summary>
        /// ��ǰ�Ƿ����ˢ�£�������ڴ�ӡ��ˢ
        /// </summary>
        bool isCanRefresh = true;

        /// <summary>
        /// �Ƿ��Զ�ˢ�´�ӡ
        /// </summary>
        bool isAutoRefreshPrint = false;

        /// <summary>
        /// �Ƿ���ʾ�����Ų�ѯ����
        /// </summary>
        private bool isShowSelectBox = false;

        /// <summary>
        /// �Ƿ���ʾ�����Ų�ѯ����
        /// </summary>
        [Description("�Ƿ���ʾ�����Ų�ѯ����"), Category("��ѯ����")]
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
        /// δ��ӡ
        /// </summary>
        DataView dvNoPrint;

        /// <summary>
        /// �Ѵ�ӡ
        /// </summary>
        DataView dvPrint;

        /// <summary>
        /// ��ѯ�����б�
        /// </summary>
        DataView dvQuery;

        /// <summary>
        /// �Ƿ��Զ�ˢ�´�ӡ
        /// </summary>
        [Description("�Ƿ��Զ�ˢ�´�ӡ"), Category("����")]
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
        /// ˢ�¼����Ĭ��10��
        /// </summary>
        private int freshTimes = 10;

        /// <summary>
        /// ˢ�¼����Ĭ��10��
        /// </summary>
        [Description("ˢ�¼������λ�룬Ĭ��10��"), Category("����")]
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

        #region ��ʼ��

        /// <summary>
        /// ���ؽ���
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
        /// ��ʼ��
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

            //��ʼ����Ա�б�
            ArrayList al = this.interMgr.QueryEmployeeAll();
            if (al == null)
            {
                MessageBox.Show(interMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                emplHelper = new FS.FrameWork.Public.ObjectHelper(al);
            }
            
            //��ʼ�������б�
            al = new ArrayList();
            al = this.interMgr.GetDepartment();
            if (al == null)
            {
                MessageBox.Show(interMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                deptHelper = new FS.FrameWork.Public.ObjectHelper(al);
            }
            
            //Ƶ��
            al = new ArrayList();
            al = this.interMgr.QuereyFrequencyList();
            if (al == null)
            {
                MessageBox.Show(interMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ���ø�ʽ
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

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ����������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ȫѡ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("��ӡƿǩ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡǩ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡע�䵥", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡ���߿�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ͣˢ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            this.toolBarService.AddToolButton("����ˢ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            //���Ӵ�ӡ������
            this.toolBarService.AddToolButton("��ӡ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);

            this.toolBarService.AddToolButton("�޸�Ƥ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// ����������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "ȫѡ":
                    this.SelectAll(true);
                    break;
                case "ȡ��":
                    this.SelectAll(false);
                    break;
                case "��ӡƿǩ":
                    this.PrintCure();
                    break;
                case "��ӡǩ����":
                    this.PrintItinerate();
                    break;
                case "��ӡע�䵥":
                    this.PrintInject();
                    break;
                case "��ӡ���߿�":
                    this.PrintPatient();
                    break;
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} ���Ӵ�ӡ������
                case "��ӡ������":
                    this.PrintNumber();
                    break;
                //{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
                case "�޸�Ƥ��":
                    this.ModifyHytest();
                    break;
                case "��ͣˢ��":
                    this.timer1.Enabled = false;
                    this.lblNote.Text = "��ʱ��ͣ�Զ�ˢ��";
                    this.toolBarService.SetToolButtonEnabled("��ͣˢ��", false);
                    this.toolBarService.SetToolButtonEnabled("����ˢ��", true);
                    FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ͣˢ�£���");
                    break;
                case "����ˢ��":
                    this.timer1.Enabled = true;
                    this.timer1.Interval = this.freshTimes * 1000;
                    this.lblNote.Text = "��ʱ�����Զ�ˢ��";
                    this.toolBarService.SetToolButtonEnabled("��ͣˢ��", true);
                    this.toolBarService.SetToolButtonEnabled("����ˢ��", false);
                    FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("������ˢ�£���");
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ����
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
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼ�ֶ���ѯ��ѯ��ӡ����");
            this.BatchQuery();
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�������ֶ���ѯ��ѯ��ӡ����");
            return 1;
        }

        /// <summary>
        /// �����п�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpInjectInfo_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.fpInjectInfo_Sheet1, injectRegisterXml);
        }

        /// <summary>
        /// �ж��Ƿ�������
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
        /// ������ɫ(��������ʾ���һ��clinicҽ��)
        /// </summary>
        /// <returns></returns>
        private int ShowColor()
        {
            //ȡ�����clinic_code
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
        /// ������ע��˳��
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
        /// ��ʾ������Ϣ
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
        /// ȫѡ
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                this.fpInjectInfo_Sheet1.Cells[i, 0].Value = isSelected;
            }
        }


        #endregion

        #region  ��ӡ

        /// <summary>
        /// ��ӡ���߿�
        /// </summary>
        private void PrintPatient()
        {
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint patientPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            if (patientPrint == null)
            {
                patientPrint = new FS.HISFC.Components.Nurse.Print.ucPrintPatient() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            }

            patientPrint.Init(al);
        }

        /// <summary>
        /// ��ӡƿǩ
        /// </summary>
        private void PrintCure()
        {
            ArrayList al = this.GetPrintInjectList();

            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            //��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint curePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            if (curePrint == null)
            {
                curePrint = new FS.HISFC.Components.Nurse.Print.ucPrintCure() as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
                //FS.HISFC.Components.FS.HISFC.Components.Nurse.Print.ucPrintCure uc = new FS.HISFC.Components.FS.HISFC.Components.Nurse.Print.ucPrintCure();
            }
            curePrint.Init(al);
        }

        /// <summary>
        /// ��ӡע�䵥.
        /// </summary>
        private void PrintInject()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            //��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint injectPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            if (injectPrint == null)
            {
                injectPrint = new FS.HISFC.Components.Nurse.Print.ucPrintInject() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            }
            injectPrint.Init(al);
        }

        /// <summary>
        /// ��ӡǩ����
        /// </summary>
        private void PrintItinerate()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            //��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new FS.HISFC.Components.Nurse.Print.ucPrintItinerate() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = isReprint;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// ����ֽ�ŵ�ǩ����
        /// </summary>
        private void PrintItinerateLarge()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }

            //��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new FS.HISFC.Components.Nurse.Print.ucPrintItinerateLarge() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = isReprint;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// ��ӡ������ҺѲ�ӿ�
        /// </summary>
        private void PrintInjectScoutCard()
        {
            int intReturn = this.GetAllPrintInjectList();
            if (intReturn == -1)
            {
                MessageBox.Show("û��ѡ������!");
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
                    //ע�䵥
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
        /// ���Ӻ�����
        /// </summary>
        private void PrintNumber()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            //��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint numberPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            if (numberPrint == null)
            {
                numberPrint = new FS.HISFC.Components.Nurse.Print.ucPrintNumber() as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            }
            numberPrint.Init(al);
        }

        /// <summary>
        /// ��ȡҪ��ӡ������
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

                //ҽ������
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
                //�жϽ�ƿ,���������ӵ�alJiePing��
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
        /// ��ȡҪ��ӡ�����ݣ���ά���÷���
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

                //ҽ������
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
                //���ߵ���ע�䴦��ʱ��
                info.InjectOrder = strOrder;

                info.User03 = this.fpInjectInfo_Sheet1.Cells[i, 13].Text;

                string hypoTest = string.IsNullOrEmpty(this.fpInjectInfo_Sheet1.Cells[i, 11].Text) ? "" : "(" + this.fpInjectInfo_Sheet1.Cells[i, 11].Text + ")";

                if (orderinfo != null)
                {
                    //��עӦ����Memo+Ƥ��
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
        /// ��ȡ�����ȵ�ʹ�÷���
        /// </summary>
        /// <param name="IsInit">�Ƿ��ʼ��</param>
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

        #region ע��˳��ŵĴ���
        /// <summary>
        /// ����Ĭ��ע��˳��
        /// </summary>
        private void SetInject()
        {
            #region  û�����ݾͲ�����,ֱ�ӷ���
            if (this.fpInjectInfo_Sheet1.RowCount <= 0) return;
            #endregion

            #region ���û��߽����ע��˳���
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

            #region ����ÿ����Ŀ��ע��˳��
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
                    //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
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
        /// ��ʼ��ע��˳���
        /// </summary>
        private void InitOrder()
        {
            //��ȡ�Ƿ��Զ�����ע��˳��
            try
            {
                bool isAutoInjectOrder = false;
                isAutoInjectOrder = FS.FrameWork.Function.NConvert.ToBoolean(this.interMgr.QueryControlerInfo("900005"));
                if (isAutoInjectOrder)
                {
                    this.isAutoBuildOrderNo = true;
                    this.SetOrder();
                    this.lbLastOrder.Text = "�������һ��ע���:" +
                        (FS.FrameWork.Function.NConvert.ToInt32(this.txtOrder.Text.Trim()) - 1).ToString();
                }
                else
                {
                    this.isAutoBuildOrderNo = false;
                    this.lbLastOrder.Text = "�������Զ�����ע��˳���!";
                    this.txtOrder.Text = "0";
                }
            }
            catch //�������ļ�
            {
                this.isAutoBuildOrderNo = false;
                this.lbLastOrder.Text = "�������Զ�����ע��˳���!";
                this.txtOrder.Text = "0";
            }


        }
        /// <summary>
        /// ����ע���
        /// </summary>
        private void SetOrder()
        {
            if (!this.isAutoBuildOrderNo)
            {
                this.txtOrder.Text = "0";
                this.lbLastOrder.Text = "���ڱ���û�������Զ��������!";
                return;
            }
            //����Զ�����,���õ�һ�����,����ֵthis.currentOrder
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} ��Ϊͨ���ӿ�ʵ�֣����û��������ԭ�����������
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
        /// �����ӿ�
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

        #region ����

        /// <summary>
        /// ȷ�ϱ���
        /// ( 1.met_nuo_inject�����¼  2.fin_ipb_feeitemlist������ȷ��Ժע������ȷ�ϱ�־)
        /// </summary>
        private int Save()
        {
            if (this.fpInjectInfo_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("û��Ҫ���������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("��ѡ��Ҫ���������", "��ʾ");
                return -1;
            }
            alInjectPrint = new ArrayList();
            alZLDPrint = new ArrayList();

            if (this.isUserOrderNumber)
            {
                #region �ж�������кŵ���Ч��
                if (this.txtOrder.Text == null || this.txtOrder.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("û���������˳���!");
                    this.txtOrder.Focus();
                    return -1;
                }
                else if (this.InjMgr.QueryInjectOrder(this.txtOrder.Text.Trim().ToString()).Count > 0)
                {
                    if (MessageBox.Show("�ö��к��Ѿ�ʹ��,�Ƿ����!", "��ʾ", System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        this.txtOrder.Focus();
                        return -1;
                    }
                }
                #endregion


                #region ���ע��˳��ŵ���Ч�ԣ������ͬ�ģ�ע��˳���Ҳ������ͬ��
                for (int i = 1; i < this.fpInjectInfo_Sheet1.RowCount; i++)
                {
                    if (this.fpInjectInfo_Sheet1.Cells[i, 7].Tag != null && this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() != "" &&
                        //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                        this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() + this.fpInjectInfo_Sheet1.Cells[i, 13].Text == this.fpInjectInfo_Sheet1.Cells[i - 1, 7].Tag.ToString() + this.fpInjectInfo_Sheet1.Cells[i - 1, 13].Text
                        && this.fpInjectInfo_Sheet1.GetValue(i, 1).ToString() != this.fpInjectInfo_Sheet1.GetValue(i - 1, 1).ToString()
                        )
                    {
                        MessageBox.Show("��ͬ��ŵ�ע��˳��ű�����ͬ!", "��" + (i + 1).ToString() + "��");
                        return -1;
                    }
                }
                #endregion
            }

            #region ���Ժע��������Ч�ԣ������ͬ�ģ�ע��˳���Ҳ������ͬ��
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                string strnum = this.fpInjectInfo_Sheet1.Cells[i, 2].Text;
                if (strnum == null || strnum == "")
                {
                    MessageBox.Show("Ժע��������Ϊ��!", "��" + (i + 1).ToString() + "��");
                    return -1;
                }
                if (!this.IsNum(strnum))
                {
                    MessageBox.Show("Ժע��������Ϊ����!", "��" + (i + 1).ToString() + "��");
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
                        MessageBox.Show("Ժע��������!", "��" + (i + 1).ToString() + "��");
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

                    #region ʵ��ת����������Ŀ�շ���ϸʵ��FeeItemList��->ע��ʵ��Inject��

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
                    //������������
                    info.Item.Order.DoctorDept.Name = this.deptHelper.GetName(detail.RecipeOper.Dept.ID);
                    info.Item.Order.DoctorDept.ID = detail.RecipeOper.Dept.ID;
                    //ҽ������
                    info.Item.Order.ReciptDoctor.Name = emplHelper.GetName(detail.RecipeOper.ID);
                    info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                    //�Ƿ�Ƥ��
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
                        //��ע--(ȡҽ����ע)
                        FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                            (FS.HISFC.Models.Order.OutPatient.Order)this.fpInjectInfo_Sheet1.Cells[i, 11].Tag;
                        if (orderinfo != null)
                        {
                            //��עӦ����Memo+Ƥ��
                            info.Memo = orderinfo.Memo;
                            info.Hypotest = orderinfo.HypoTest;
                        }
                    }

                    #region ��met_nuo_inject�У������¼
                    if (this.InjMgr.Insert(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.InjMgr.Err, "��ʾ");
                        return -1;
                    }
                    #endregion

                    #region ��fin_ipb_feeitemlist�У���������
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
                        MessageBox.Show(this.outFeeMgr.Err, "��ʾ");
                        return -1;
                    }
                    #endregion
                    info.Item.InjectCount = info.Item.InjectCount;
                    //���ƿ�ĲŴ�ӡ���Ƶ�---��д��-------------�˶γ�����,��Ϊ�ɲ���Աѡ���Ƿ��ӡ
                    if (info.Item.Order.Usage.ID == "03" || info.Item.Order.Usage.ID == "04")
                    {
                        alZLDPrint.Add(info);
                    }
                    alInjectPrint.Add(info);
                    this.lbLastOrder.Text = "�������һ��ע���:" + info.OrderNO;

                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            if (this.isMessageInSave)
            {
                MessageBox.Show("����ɹ�!", "��ʾ");
            }
            this.Clear();

            this.txtCardNo.SelectAll();
            this.txtCardNo.Text = "";
            this.txtCardNo.Focus();
            return 0;
        }

        /// <summary>
        /// ���
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
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            if (this.fpInjectInfo_Sheet1.RowCount > 0)
            {
                this.fpInjectInfo_Sheet1.Rows.Remove(0, this.fpInjectInfo_Sheet1.RowCount);
            }
            string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����տ��Ų�ѯ���ݣ����ţ�" + cardNo + "����");

            //��ȡҽ�������Ĵ�����Ϣ��û��ȫ��ִ����ģ�
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
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����ţ�" + cardNo + " ��ѯ������Ϣʧ�ܣ���" + outFeeMgr.Err);
            }
            else
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼ���տ��Ų�ѯ��ӡ�����ţ�" + cardNo + "����");
                this.Query(al);
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("���������տ��Ų�ѯ��ӡ�����ţ�" + cardNo + "����");
            }
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="id"></param>
        private void Query(ArrayList al)
        {
            if (al == null || al.Count == 0)
            {
                //MessageBox.Show("�û���û����Ҫȷ�ϵ�ҽ����Ϣ!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }

            this.AddDetail(al);

            if (this.fpInjectInfo_Sheet1.RowCount <= 0)
            {
                //MessageBox.Show("��ʱ�����û�иû�����Ϣ!", "��ʾ");
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

            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼ���տ��Ŵ�ӡ���ݣ���");
            //��ӡѲ�ӿ�
            if (this.isAutoPrint)
            {
                this.PrintInjectScoutCard();
            }
            else
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ΪisAutoPrint=false��δ��ӡ����");
            }
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("���������տ��Ŵ�ӡ���ݣ���");

            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼ�Զ��������ݣ���");
            if (this.isAutoSave)
            {
                this.SelectAll(true);
                this.Save();
            }
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�������Զ��������ݣ���");
        }

        /// <summary>
        /// �����Ŀ��ϸ
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(ArrayList details)
        {
            if (this.fpInjectInfo_Sheet1.RowCount > 0)
            {
                this.fpInjectInfo_Sheet1.Rows.Remove(0, this.fpInjectInfo_Sheet1.RowCount);
            }

            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> tmpFeeList = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            if (details != null)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in details)
                {
                    //���շ������⣬�����п�������Ϊ�յ��ֶ���ʱ��Ϊ2001
                    if (detail.RecipeOper.Dept.ID == null || detail.RecipeOper.Dept.ID == "")
                    {
                        detail.RecipeOper.Dept.ID = "2001";
                    }
                    #region ���˷�Ʊ
                    if (detail.Invoice.ID != this.InvoiceNo && !string.IsNullOrEmpty(InvoiceNo))
                    {
                        continue;
                    }
                    #endregion

                    #region  �ж�����Ч���������ϵ�
                    if (this.isQuit)
                    {
                        //��������ϵ�����ʾ��Ч��¼�����ϵ�����¼
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
                        //������ʾ��Ч��¼
                        if (detail.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid)
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region ���˿�������
                    //�ж��Ƿ���˿������ң������˼���
                    if (this.isFilterDoctDept)
                    {
                        //�ж��Ƿ�Ϊ�գ�Ϊ��Ĭ��ȫ�����ң�����
                        if (!string.IsNullOrEmpty(this.doctDept))
                        {
                            if (!this.doctDept.Contains("'" + detail.RecipeOper.Dept.ID + "'"))
                            {
                                continue;
                            }
                        }
                    }
                    #endregion

                    #region ���˿ۿ����

                    //���ĸ���ע�䵥ע��ͷ�ҩ�����Դ˴�����ҩƷִ�п��ҹ���
                    if(detail.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (detail.ExecOper.Dept.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
                        {
                            continue;
                        }
                    }


                    #endregion

                    #region  ��ȷ��Ժע����������0�Ĳ���ʾ
                    //������ش����ж�
                    if (!this.isReprint)
                    {
                        //��ȷ��Ժע����������0�Ĳ���ʾ
                        if (detail.ConfirmedInjectCount != 0)
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region  �жϷ�ҩƷ�Ƿ���ʾ
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

                    #region ά����Щ�÷���ʾ�ڽ�����

                    //���ĸ���ע�䵥ע��ͷ�ҩ������ֻҪ�Ǳ�ҩ���ģ�����ʲô�÷�����ӡ����

                    //if (!usage.Contains("'" + detail.Order.Usage.ID + "'"))
                    //{
                    //    continue;
                    //}
                    #endregion

                    #region �Ƿ���ʾ0������
                    if (!this.isRegNullNum && detail.InjectCount == 0)
                    {
                        continue;
                    }
                    #endregion

                    #region ���ϣ������Ѿ��Ǽǵ�QD����ʾ��ע�����ε�BID����ʾ����ǰ���ע��һ�ε�BID������ʾ��(���ݽ���ĵǼ�ʱ��)
                    //DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(
                    //    this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd 00:00:00"));
                    ////{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                    //ArrayList alTodayInject = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO, detail.SequenceNO.ToString(), dt);
                    //FS.HISFC.Models.Order.Frequency frequence = this.dicFrequency[detail.Order.Frequency.ID];
                    //string[] injectTime = frequence.Time.Split('-');
                    ////������Ѿ�ȫ��ע����Ϻ�����
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
                    //    //δ���ϴ�ע��ʱ��Ļ��������ٴεǼ�
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

                //����
                //tmpFeeList.Sort(new FeeItemListSort());
                //��ȡ��ӡ���
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
        /// �����ϸ
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList info)
        {
            this.fpInjectInfo_Sheet1.Rows.Add(this.fpInjectInfo_Sheet1.RowCount, 1);
            int row = this.fpInjectInfo_Sheet1.RowCount - 1;
            this.fpInjectInfo_Sheet1.Rows[row].Tag = info;

            #region "���ڸ�ֵ"
            #region ��ȡƤ����Ϣ
            string strTest ="";
            //��ȡƤ����Ϣ
            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                drug = this.interPhaMgr.GetItem(info.Item.ID);
                if (drug == null)
                {
                    MessageBox.Show("��ȡҩƷƤ����Ϣʧ��!");
                    this.fpInjectInfo_Sheet1.Rows.Remove(0, this.fpInjectInfo_Sheet1.RowCount);
                    return;
                }
                strTest = "��";
                if (drug.IsAllergy)
                {
                    strTest = "��";
                }
            }
            //
            #endregion

            info.Order.DoctorDept.Name = deptHelper.GetName(info.RecipeOper.Dept.ID);

            this.fpInjectInfo_Sheet1.SetValue(row, 1, "", false);//ע��˳���
            this.fpInjectInfo_Sheet1.SetValue(row, 2, info.InjectCount.ToString(), false);//Ժע����
            this.fpInjectInfo_Sheet1.SetValue(row, 3, info.ConfirmedInjectCount.ToString(), false);//�Ѿ�ȷ�ϵ�Ժע����
            this.fpInjectInfo_Sheet1.SetValue(row, 4, this.emplHelper.GetName(info.RecipeOper.ID), false);//����ҽ��
            this.fpInjectInfo_Sheet1.Cells[row, 4].Tag = info.Order.ReciptDoctor.ID;
            this.fpInjectInfo_Sheet1.SetValue(row, 5, info.Order.DoctorDept.Name, false);//�Ʊ�
            this.fpInjectInfo_Sheet1.Cells[row, 5].Tag = info.Order.DoctorDept.ID;
            this.fpInjectInfo_Sheet1.SetValue(row, 6, info.Item.Name, false);//ҩƷ����
            this.fpInjectInfo_Sheet1.Cells[row, 7].Tag = info.Order.Combo.ID;//��Ϻ�
            this.fpInjectInfo_Sheet1.SetValue(row, 8, info.Order.DoseOnce.ToString() + info.Order.DoseUnit, false);//ÿ����
            this.fpInjectInfo_Sheet1.SetValue(row, 9, info.Order.Frequency.ID, false);//Ƶ��
            this.fpInjectInfo_Sheet1.Cells[row, 9].Tag = info.Order.Frequency.ID.ToString();
            this.fpInjectInfo_Sheet1.SetValue(row, 10, info.Order.Usage.Name, false);//�÷�
            this.fpInjectInfo_Sheet1.SetValue(row, 11, strTest, false);//Ƥ�ԣ�
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
                    //        orderinfo.HypoTest = 0;//���Ե�ֵΪ��
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
            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            this.fpInjectInfo_Sheet1.Cells[row, 13].Text = info.User03;

            #endregion
        }

        /// <summary>
        /// ��ȡƤ����Ϣ
        /// </summary>
        /// <param name="hytestID"></param>
        /// <returns></returns>
        private string GetHyTestInfo(string hytestID)
        {
            switch (hytestID)
            {
                case "1":
                    return "����";
                case "2":
                    return "��Ƥ��";
                case "3":
                    return "[��]";
                case "4":
                    return "[��]";
                case "0":
                default:
                    return "��";
            }
        }

        /// <summary>
        /// ������Ϻ�
        /// </summary>
        private void SetComb()
        {
            int myCount = this.fpInjectInfo_Sheet1.RowCount;
            int i;
            //��һ��
            this.fpInjectInfo_Sheet1.SetValue(0, 7, "��");
            //�����
            this.fpInjectInfo_Sheet1.SetValue(myCount - 1, 7, "��");
            //�м���
            for (i = 1; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString();
                string priorRowCombNo = this.fpInjectInfo_Sheet1.Cells[prior, 7].Tag.ToString();
                string nextRowCombNo = this.fpInjectInfo_Sheet1.Cells[next, 7].Tag.ToString();

                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                string currentRowInjectTime = this.fpInjectInfo_Sheet1.Cells[i, 13].Text.ToString();
                string priorRowInjectTime = this.fpInjectInfo_Sheet1.Cells[prior, 13].Text.ToString();
                string nextRowInjectTime = this.fpInjectInfo_Sheet1.Cells[next, 13].Text.ToString();

                #region """""
                bool bl1 = true;
                bool bl2 = true;
                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                    bl1 = false;
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                    bl2 = false;
                //  ��
                if (bl1 && bl2)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "��");
                }
                //  ��
                if (bl1 && !bl2)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "��");
                }
                //  ��
                if (!bl1 && bl2)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "��");
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "");
                }
                #endregion
            }
            //��û����ŵ�ȥ��
            for (i = 0; i < myCount; i++)
            {
                if (this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() == "")
                {
                    this.fpInjectInfo_Sheet1.SetValue(i, 7, "");
                }
            }
            //�ж���ĩ�� ����ţ���ֻ���Լ�һ�����ݵ����
            if (myCount == 1)
            {
                this.fpInjectInfo_Sheet1.SetValue(0, 7, "");
            }
            //ֻ����ĩ���У���ô��Ҫ�ж���Ű�
            if (myCount == 2)
            {
                if (this.fpInjectInfo_Sheet1.Cells[0, 7].Tag.ToString().Trim() != this.fpInjectInfo_Sheet1.Cells[1, 7].Tag.ToString().Trim())
                {
                    this.fpInjectInfo_Sheet1.SetValue(0, 7, "");
                    this.fpInjectInfo_Sheet1.SetValue(1, 7, "");
                }
                //��ֹһ��ҩbid�����һ��
                if (this.fpInjectInfo_Sheet1.Cells[0, 13].Text.ToString().Trim() != this.fpInjectInfo_Sheet1.Cells[1, 13].Text.ToString().Trim())
                {
                    this.fpInjectInfo_Sheet1.SetValue(0, 7, "");
                    this.fpInjectInfo_Sheet1.SetValue(1, 7, "");
                }
            }
            if (myCount > 2)
            {
                if (this.fpInjectInfo_Sheet1.GetValue(1, 7).ToString() != "��"
                    && this.fpInjectInfo_Sheet1.GetValue(1, 7).ToString() != "��")
                {
                    this.fpInjectInfo_Sheet1.SetValue(0, 7, "");
                }
                if (this.fpInjectInfo_Sheet1.GetValue(myCount - 2, 7).ToString() != "��"
                    && this.fpInjectInfo_Sheet1.GetValue(myCount - 2, 7).ToString() != "��")
                {
                    this.fpInjectInfo_Sheet1.SetValue(myCount - 1, 7, "");
                }
            }

        }

        /// <summary>
        /// ��ӡ���Ƶ�
        /// </summary>
        private void Print()
        {
            if (this.alZLDPrint == null || this.alZLDPrint.Count <= 0)
            {
                MessageBox.Show("û����Ҫ��ӡ������!");
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
        /// ѡ����ͬ�����Ŀ
        /// </summary>
        /// <param name="isSelect"></param>
        private void SelectedComb(bool isSelect)
        {
            int row = this.fpInjectInfo_Sheet1.ActiveRowIndex;
            string combID = this.fpInjectInfo_Sheet1.Cells[row, 7].Tag.ToString();
            //���߿��ԵǼ�ȫ������ע�䴦��
            string injectTime = this.fpInjectInfo_Sheet1.Cells[row, 13].Text;
            for (int i = 0; i < this.fpInjectInfo_Sheet1.RowCount; i++)
            {
                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                if (this.fpInjectInfo_Sheet1.Cells[i, 7].Tag.ToString() == combID && this.fpInjectInfo_Sheet1.Cells[i, 13].Text == injectTime)
                {
                    this.fpInjectInfo_Sheet1.Cells[i, 0].Value = isSelect;
                }
            }
        }

        /// <summary>
        /// �޸�Ƥ����Ϣ
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
        /// ���Ų�ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("�����벡����!", "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }
                string strCardNO = this.txtCardNo.Text.Trim();//.PadLeft(10, '0');
                int iTemp = interFeeMgr.ValidMarkNO(strCardNO, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("��Ч���ţ�����ϵ����Ա��");
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
            //�жϵ�ǰ��ӡ���Ƿ����˷ѵ�
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
                MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }
            reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
            if (reg == null || reg.ID == "")
            {
                MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");

                this.txtCardNo.Focus();
                return;
            }

            this.txtCardNo.Text = cardNo;
            this.ShowPatient(reg, fpQuery_Sheet1.Cells[e.Row, (int)EnumPatientListColumn.Name].Tag.ToString());
            this.isReprint = true;
            this.isAutoSave = false;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����ô�ӡ����:isAutoPrint = true��");
            this.isAutoPrint = true;
            this.Query();
            this.txtCardNo.Text = "";
            this.InvoiceNo = "";
        }

        void fpQuery_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼ������߲�ѯ������Ϣ����ʱ���Զ���ӡ��");
            string cardNo = this.fpQuery_Sheet1.Cells[e.Row, 3].Text;
            this.InvoiceNo = this.fpQuery_Sheet1.Cells[e.Row, 1].Tag.ToString();

            //�жϵ�ǰ��ӡ���Ƿ����˷ѵ�
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
                MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }
            reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
            if (reg == null || reg.ID == "")
            {
                MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");

                this.txtCardNo.Focus();
                return;
            }

            this.txtCardNo.Text = cardNo;
            this.ShowPatient(reg, fpQuery_Sheet1.Cells[e.Row, (int)EnumPatientListColumn.Name].Tag.ToString());
            this.isReprint = true;
            this.isAutoSave = false;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����ô�ӡ����:isAutoPrint = true��");
            this.isAutoPrint = false;
            this.Query();
            this.txtCardNo.Text = "";
            this.InvoiceNo = "";
            this.isAutoSave = true;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����ô�ӡ����:isAutoPrint = true��");
            this.isAutoPrint = true;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("��������ѯ���߷�����Ϣ��");
        }

        /// <summary>
        /// ע��˳���
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
        /// ��ذ����¼�
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
        /// ��Ϲ�ѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.fpInjectInfo_Sheet1.ActiveRowIndex;
            bool isSelect = Convert.ToBoolean(this.fpInjectInfo_Sheet1.Cells[row, 0].Value);
            this.SelectedComb(isSelect);
        }

        #region IInterfaceContainer ��Ա

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


        #region �Զ�ˢ�´�ӡ

        /// <summary>
        /// �Զ�ˢ�´�ӡ����
        /// </summary>
        /// <param name="isAutoPrint"></param>
        private void AutoPrintSet(bool isAutoRefreshPrint)
        {
            this.isAutoRefreshPrint = isAutoRefreshPrint;
            //this.gbxQuery.Visible = !isAutoRefreshPrint;
            this.neuGroupBox2.Visible = isAutoRefreshPrint;
            this.timer1.Enabled = isAutoRefreshPrint;
            this.timer1.Interval = this.freshTimes * 1000;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�������Զ�ˢ�¹��ܣ�ˢ�¼��Ϊ" + freshTimes.ToString() + "����");

            this.cbxAutoPringDate.Enabled = isAutoRefreshPrint;
            this.cbxAutoPringDate.Checked = isAutoRefreshPrint;
            this.dtpAutoPrintBegin.Enabled = isAutoRefreshPrint;

            if (isAutoRefreshPrint)
            {
                this.toolBarService.SetToolButtonEnabled("��ͣˢ��", true);
                this.toolBarService.SetToolButtonEnabled("����ˢ��", false);
                this.lblNote.Text = "��ʱ�����Զ�ˢ��";
            }
            else
            {
                this.toolBarService.SetToolButtonEnabled("��ͣˢ��", false);
                this.toolBarService.SetToolButtonEnabled("����ˢ��", true);
                this.lblNote.Text = "��ʱ��ͣ�Զ�ˢ��";
            }

            //����ǹ���Ա���Զ���ӡ
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.timer1.Enabled = false;
                this.lblNote.Text = "��ʱΪ����Ա���벻�Զ�ˢ�£��밴��ѯ��ť�ֶ�ˢ�£�";
            }
        }

        /// <summary>
        /// �Զ�ˢ�¹���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼˢ�£���");

            if (!this.isAutoRefreshPrint)
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("��δ�����Զ�ˢ�£��˳�����");
                return;
            }
            if (!this.isCanRefresh)
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("������ˢ�£��˳�����Ϊ���ڴ�ӡ������");
                return;
            }

            this.BatchQuery();
            
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("������ˢ�£���");
        }

        /// <summary>
        /// ������ѯ��ӡ
        /// </summary>
        /// <returns></returns>
        private int BatchQuery()
        {
            this.txtCardNo.Text = "";
            this.QueryPatient();

            //��ӡ
            while (this.sheetView_NoPrint.RowCount > 0)
            {
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼ��ֵ��ӡ����");
                this.isCanRefresh = false;
                try
                {
                    this.sheetView_NoPrint.ActiveRowIndex = this.sheetView_NoPrint.RowCount - 1;

                    string cardNo = this.sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, 3].Text;

                    if (sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, 1].Tag != null)
                    {
                        InvoiceNo = sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, 1].Tag.ToString();
                    }

                    //�жϵ�ǰ��ӡ���Ƿ����˷ѵ�
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
                        //MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                        this.isCanRefresh = true;
                        this.txtCardNo.Focus();
                        return -1;
                    }

                    reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
                    if (reg == null || reg.ID == "")
                    {
                        //MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                        this.isCanRefresh = true;
                        this.txtCardNo.Focus();
                        return -1;
                    }

                    this.txtCardNo.Text = cardNo;
                    this.ShowPatient(reg, this.sheetView_NoPrint.Cells[this.sheetView_NoPrint.RowCount - 1, (int)EnumPatientListColumn.Name].ToString());
                    this.isReprint = false;
                    this.isAutoSave = true;
                    FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����ô�ӡ����:isAutoPrint = true��");
                    this.isAutoPrint = true;

                    this.Query();
                    this.txtCardNo.Text = "";
                    InvoiceNo = "";
                    this.sheetView_NoPrint.RemoveRows(this.sheetView_NoPrint.RowCount - 1, 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ֵ��ӡ���ִ���:��" + ex.Message);
                    this.isCanRefresh = true;
                    return -1;
                }
                FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("��������ֵ��ӡ����");
            }
            this.isCanRefresh = true;
            return 1;
        }

        /// <summary>
        /// ҩƷ�÷�
        /// </summary>
        string drugUsage = "";

        /// <summary>
        /// �������ҹ���
        /// </summary>
        string dept = "'All'";

        /// <summary>
        /// ��ѯע�仼���б�
        /// </summary>
        private void QueryPatient()
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼ��ѯ�����б���");

            this.Clear();

            DateTime begin = new DateTime();
            //����Զ���ӡ�ģ���ʼʱ������趨
            if (this.cbxAutoPringDate.Checked && this.isAutoRefreshPrint)
            {
                begin = this.dtpAutoPrintBegin.Value;
            }
            else
            {
                begin = this.InjMgr.GetDateTimeFromSysDateTime();
            }

            #region ���ÿ�������
            
            dept = "'All'";

            //�ж��Ƿ���˿������ң������˼���
            if (this.isFilterDoctDept)
            {
                //�ж��Ƿ�Ϊ�գ�Ϊ��Ĭ��ȫ�����ң�����
                if (!string.IsNullOrEmpty(this.doctDept))
                {
                    dept = this.doctDept;
                }
            }
            #endregion

            #region ���÷�ҩƷ�÷�����

            string unDrugUsage = "'All'";
            //�ж��Ƿ��жϷ�ҩƷ
            if (isShowUnDrug)
            {
                if (!string.IsNullOrEmpty(this.unDrugUsage))
                {
                    unDrugUsage = this.unDrugUsage;
                }
            }
            #endregion

            #region ����ҩƷ�÷�����

            //�ж��Ƿ��жϷ�ҩƷ
            if (!string.IsNullOrEmpty(this.usage))
            {
                drugUsage = this.usage;
            }
            else
            {
                MessageBox.Show("��ά��Ҫ��ӡ��ҩƷ�÷�");
                return;
            }
            #endregion

            #region �����б�

            #region �Ѵ�ӡ

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

            #region δ��ӡ
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

            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("��������ѯ�����б���");
        }

        /// <summary>
        /// �����б�ֵ
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
                    sheet.Cells[sheet.RowCount - 1, (int)EnumPatientListColumn.CancleFlag].Text = "��";
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
        /// ���˻����б�����
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
            //���Ź���
            if (tt.Contains(this.txtFilter.Text.Substring(0, 1)))
            {
                filter = "���� like '%" + this.txtFilter.Text.TrimStart('0') + "%'";
            }
            //��������
            else
            {
                filter = "���� like '%" + this.txtFilter.Text.TrimStart('0') + "%'";
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
        /// ˫���Ѵ�ӡ�����б��Զ���ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPrinted_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string cardNo = this.sheetView_Print.Cells[e.Row, 3].Text;
            this.InvoiceNo = this.sheetView_Print.Cells[e.Row, 1].Tag.ToString();
            //�жϵ�ǰ��ӡ���Ƿ����˷ѵ�
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
                MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }
            reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
            if (reg == null || reg.ID == "")
            {
                MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");

                this.txtCardNo.Focus();
                return;
            }

            this.txtCardNo.Text = cardNo;
            this.ShowPatient(reg, sheetView_Print.Cells[e.Row, (int)EnumPatientListColumn.Name].Tag.ToString());
            this.isReprint = true;
            this.isAutoSave = false;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����ô�ӡ����:isAutoPrint = true��");
            this.isAutoPrint = true;
            this.Query();
            this.txtCardNo.Text = "";
            this.InvoiceNo = "";
        }

        /// <summary>
        /// �����Ѵ�ӡ�����б���ѯ��ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPrinted_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("����ʼ������߲�ѯ������Ϣ����ʱ���Զ���ӡ��");
            string cardNo = this.sheetView_Print.Cells[e.Row, 3].Text;
            this.InvoiceNo = this.sheetView_Print.Cells[e.Row, 1].Tag.ToString();

            //�жϵ�ǰ��ӡ���Ƿ����˷ѵ�
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
                MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }
            reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
            if (reg == null || reg.ID == "")
            {
                MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");

                this.txtCardNo.Focus();
                return;
            }

            this.txtCardNo.Text = cardNo;
            this.ShowPatient(reg, sheetView_Print.Cells[e.Row, (int)EnumPatientListColumn.Name].Tag.ToString());
            this.isReprint = true;
            this.isAutoSave = false;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����ô�ӡ����:isAutoPrint = true��");
            this.isAutoPrint = false;
            this.Query();
            this.txtCardNo.Text = "";
            this.InvoiceNo = "";
            this.isAutoSave = true;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("�����ô�ӡ����:isAutoPrint = true��");
            this.isAutoPrint = true;
            FS.SOC.HISFC.Components.Nurse.Classes.LogManager.Write("��������ѯ���߷�����Ϣ��");
        }

        #endregion

        /// <summary>
        /// ���˻����б�
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
                    #region ����������ѯ

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
                    #region ���տ��Ų�ѯ

                    string strCardNO = this.txtFilter.Text.Trim();//.PadLeft(10, '0');
                    int iTemp = interFeeMgr.ValidMarkNO(strCardNO, ref objCard);
                    if (iTemp <= 0 || objCard == null)
                    {
                        MessageBox.Show("��Ч���ţ�����ϵ����Ա��");
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
    /// �����б�������
    /// </summary>
    public enum EnumPatientListColumn
    {
        /// <summary>
        /// �˷ѱ��
        /// </summary>
        CancleFlag,
        /// <summary>
        /// ����
        /// </summary>
        Name,
        /// <summary>
        /// �Ա�
        /// </summary>
        Sex,
        /// <summary>
        /// ����
        /// </summary>
        CardNo,
        /// <summary>
        /// ��Ʊ��
        /// </summary>
        InvoiceNo
    }
    
    /// <summary>
    /// ����
    /// {24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
    /// </summary>
    public class FeeItemListSort : IComparer<FS.HISFC.Models.Fee.Outpatient.FeeItemList>
    {
        public int Compare(FS.HISFC.Models.Fee.Outpatient.FeeItemList x, FS.HISFC.Models.Fee.Outpatient.FeeItemList y)
        {
            //�Ȱ��մ�������
            if (x.RecipeNO != y.RecipeNO)
            {
                return y.RecipeNO.CompareTo(x.RecipeNO);
            }
            //��ע��ʱ������
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

            //����Ϻ�����
            if (x.Order.Combo.ID != y.Order.Combo.ID)
            {
                return y.Order.Combo.ID.CompareTo(x.Order.Combo.ID);
            }
            //���������
            if (x.Order.SequenceNO != y.Order.SequenceNO)
            {
                return y.SequenceNO.CompareTo(x.SequenceNO);
            }
            //ҩƷ����
            //if (x.Item.ID != y.Item.ID)
            //{
            //    return y.Item.ID.CompareTo(x.Item.ID);
            //}
            return 0;
        }
    }
}
