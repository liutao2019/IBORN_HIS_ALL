using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;
using FS.HISFC.Models.Base;
using FS.HISFC.BizProcess.Interface.Fee;
using FS.HISFC.Models.Registration;
using FS.HISFC.Components.OutpatientFee.Forms;
using System.Collections.Generic;
using FS.HISFC.Components.OutpatientFee.Controls;
using FS.HISFC.Components.OutpatientFee;
using FSSOC.HISFC.PE.Components;
using FSSOC.HISFC.PE.Components.Service;

namespace FS.SOC.HISFC.PE.Components.Controls
{
    /// <summary>
    /// ucCharge<br></br>
    /// [��������: �����շ�������UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-2-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCharge : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.FeeInterface.ISIReadCard, FS.FrameWork.WinForms.Forms.IInterfaceContainer, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucCharge()
        {
            InitializeComponent();
        }

        #region ����
        
        #region �������

        /// <summary>
        /// �Һ���Ϣ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientInfomation registerControl = null;

        /// <summary>
        /// ��Ŀ¼����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay itemInputControl = null;

        /// <summary>
        /// �����Ϣ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft leftControl = null;

        /// <summary>
        /// �շѵ����ؼ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee popFeeControl = null;

        /// <summary>
        /// �Ҳ���Ϣ��ʾ�ؼ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight rightControl = null;

        FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee afterFee = null;
        /// <summary>
        /// ҽԺ����
        /// {8B5D1E31-3BD5-4003-B35C-25D920B0D9EE}
        /// </summary>
        private string hospitalCode = "";
        /// <summary>
        /// ҽԺ����
        /// {8B5D1E31-3BD5-4003-B35C-25D920B0D9EE}
        /// </summary>
        public string HospitalCode
        {
            get
            {
                if (string.IsNullOrEmpty(hospitalCode))
                {
                    hospitalCode = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.HosCode, true, "");
                }
                return hospitalCode;
            }
        }

        #endregion

        #region �ؼ�����

        /// <summary>
        /// �໼�ߵ�������
        /// </summary>
        protected Form fPopWin = new Form();

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        protected ucShowPatients ucShow = new ucShowPatients();

        /// <summary>
        /// toolBar
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region ҵ������

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ������ҵ���
        /// </summary>
        protected FS.SOC.HISFC.PE.BizLogic.Fee peFeeIntegrate = new FS.SOC.HISFC.PE.BizLogic.Fee();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ��ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// �������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �����շ�
        /// </summary>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        protected FS.HISFC.BizProcess.Integrate.Material.Material materialManager = new FS.HISFC.BizProcess.Integrate.Material.Material();
        /// <summary>
        /// �����˻�ҵ���
        /// {8B5D1E31-3BD5-4003-B35C-25D920B0D9EE}
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        #endregion

        #region ��ͨ����

        /// <summary>
        /// �շ���Ϣ
        /// </summary>
        protected ArrayList comFeeItemLists = new ArrayList();
        #region {DBA4A9CD-4484-4a95-9946-F7C291DDB813}
        private int leftControlWith = 0;
        #endregion
        /// <summary>
        /// toolBarӳ��
        /// </summary>
        protected Hashtable hsToolBar = new Hashtable();

        /// <summary>
        /// ������Ŀ���
        /// </summary>
        protected FS.HISFC.Models.Base.ItemKind itemKind = FS.HISFC.Models.Base.ItemKind.All;
        /// <summary>
        /// �Ƿ����ۼƲ���
        /// </summary>
        private bool isAddUp = false;
        #endregion

        #region ҽ�ƴ����ӿڱ���

        /// <summary>
        /// ҽ�ƴ����ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        #endregion

        #region ���Ʊ���

        /// <summary>
        /// ҽ����HIS����ʱ�շ�
        /// </summary>
        protected bool isCanFeeWhenTotCostDiff = false;
        protected bool isAutoBankTrans = false;
        /// <summary>
        /// �Ƿ��շ�
        /// </summary>
        protected bool isFee = false;
        /// <summary>
        /// ҽ�����룬�ú�ͬ��λ�Ķ�����Ϣ�е�ҽ��Ŀ¼�ȼ����ڹ���ʱ��Ŀ�ķ�����Ŀ��ʾ������
        /// </summary>
        protected string ybPactCode = string.Empty;
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        protected string msgInfo = string.Empty;
        /// <summary>
        /// ��ݼ�����·��
        /// </summary>
        protected string filePath = Application.StartupPath + @".\" + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\clinicShotcut.xml";
        /// <summary>
        /// �Ƿ�ؼ��ڲ�Ԥ����
        /// </summary>
        protected bool isPreFee = false;
        /// <summary>
        /// �Ƿ���ʾ���������Ϣ
        /// </summary>
        protected bool isSetDiag = false;
        /// <summary>
        /// �Ƿ����ѡ����Ŀ�շ�//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        /// </summary>
        protected bool isCanSelectItemAndFee = false;
        /// <summary>
        /// �����շ��Ƿ�ֻ�����ȡ�ʻ���1-�ǣ�0-��
        /// {B1B1CC9F-BFC3-4b64-B16E-AECC8B6FAEF4}
        /// </summary>
        private bool isAccountPayOnly = false;
        /// <summary>
        /// �շѽ��ȡ���Ƿ���ò�����ϸ��ʽ��1�ǣ�0��
        /// </summary>
        string isRoundFeeByDetail = string.Empty;

        private bool isOpenLisCulate = false;

        #endregion

        #region �������뵥�ӿ�

        //FS.ApplyInterface.HisInterface PACSApplyInterface = null;

        #endregion

        #region �����ӿ�

        /// <summary>
        /// �����жϽӿ�{DA12F709-B696-4eb9-AD3B-6C9DB7D780CF}
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IFeeExtendOutpatient iFeeExtendOutpatient = null;
        /// <summary>
        /// �����ӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen = null;
        /// <summary>
        /// �����ӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans iBankTrans = null;
        /// <summary>
        /// ����ȡ���ӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff iOutPatientFeeRoundOff = null;
        /// <summary>
        /// ����Lis�Թܽӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.ILisCalculateTube iLisCalculateTube = null;
        #endregion

        #endregion

        #region ����
        private int promptingDayBalanceDays = -1;

        public int PromptingDayBalanceDays
        {
            get
            {
                //isCanSelectEndDatetime = this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Enabled;
                return promptingDayBalanceDays;
            }
            set
            {
                promptingDayBalanceDays = value;
                //this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Enabled = isCanSelectEndDatetime;
            }
        }
        /// <summary>
        /// �Ƿ����ѡ����Ŀ�շ�//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ����ѡ����Ŀ�շ�")]
        public bool IsCanSelectItemAndFee
        {
            get
            {
                return this.isCanSelectItemAndFee;
            }
            set
            {
                this.isCanSelectItemAndFee = value;
            }
        }
        /// <summary>
        /// �Ƿ�ؼ��ڲ�Ԥ����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ؼ��ڲ�Ԥ����")]
        public bool IsPreFee
        {
            get
            {
                return this.isPreFee;
            }
            set
            {
                this.isPreFee = value;
            }
        }
        /// <summary>
        /// ҽ�����룬�ú�ͬ��λ�Ķ�����Ϣ�е�ҽ��Ŀ¼�ȼ����ڹ���ʱ��Ŀ�ķ�����Ŀ��ʾ������
        /// �������ҽ����YBPactCode = 2
        /// </summary>
        [Category("�ؼ�����"), Description("���ѷ�����ĿĿ¼�ȼ����ո�ҽ������Ķ�����Ϣ�ĵȼ�")]
        public string YBPactCode
        {
            get
            {
                return this.ybPactCode;
            }
            set
            {
                this.ybPactCode = value;
            }
        }
        /// <summary>
        /// �Ƿ���ʾ���������Ϣ
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ���������Ϣ")]
        public bool IsSetDiag
        {
            get
            {
                return this.isSetDiag;
            }
            set
            {
                this.isSetDiag = value;
            }
        }
        private bool isShowMultScreenAll = false;
        /// <summary>
        /// �Ƿ�һֱ��ʾ������Ϣ�����ǹر������շѽ���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�һֱ��ʾ������Ϣ�����ǹر������շѽ���")]
        public bool IsShowMultScreenAll
        {
            get { return isShowMultScreenAll; }
            set
            {
                this.isShowMultScreenAll = value;
            }
        }
        /// <summary>
        /// ������Ŀ���
        /// </summary>
        [Category("�ؼ�����"), Description("���ص���Ŀ��� All���� Undrug��ҩƷ drugҩƷ")]
        public FS.HISFC.Models.Base.ItemKind ItemKind
        {
            set
            {
                this.itemKind = value;

            }
            get
            {
                return this.itemKind;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        private bool isValidFee = false;
        [Category("�ؼ�����"), Description("false:���� true:�շ�")]
        public bool IsValidFee
        {
            set
            {
                this.isValidFee = value;

            }
            get
            {
                return this.isValidFee;
            }
        }
        private bool isShowSiPerson = true;
        [Category("�ؼ�����"), Description("��������ʱ����ҽ���Ǽǻ��ߣ� false:������ true:����")]
        public bool IsShowSiPerson
        {
            set { isShowSiPerson = value; }
            get { return isShowSiPerson; }
 
        }

        /// <summary>
        /// �Ƿ����ۼƲ���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ����ۼƲ��� true���� false����")]
        public bool IsAddUp
        {
            get
            {
                return isAddUp;
            }
            set
            {
                isAddUp = value;
                //if (!value)
                //{
                //    ToolStripButton tempTb = null;
                //    tempTb = toolBarService.GetToolButton("��ʼ�ۼ�");
                //    if (tempTb != null)
                //    {
                //        tempTb.Visible = false;
                //    }
                //    tempTb = toolBarService.GetToolButton("ȡ���ۼ�");
                //    if (tempTb != null)
                //    {
                //        tempTb.Visible = false;
                //    }
                //    tempTb = toolBarService.GetToolButton("�����ۼ�");
                //    if (tempTb != null)
                //    {
                //        tempTb.Visible = false;
                //    }
                //}
            }
        }

        [Category("�ؼ�����"), Description("���ۻ��շ�ʱ���Ƿ���ʾ�ײ����úͷ�Ʊ��Ϣ true=��  false=��")]
        public bool IsShowFeeInfo
        {
            get
            {
                return this.plBottom.Visible;
            }
            set
            {
                this.plBottom.Visible = value;
            }
        }

        private bool isShowDeptFeeDetail = false;
        [Category("�ؼ�����"), Description("����ʱ���Ƿ���ʾ��ǰ���ҵ���Ŀ true=��  false=��")]
        public bool IsShowDeptFeeDetail
        {
            set
            {
                this.isShowDeptFeeDetail = value;

            }
            get
            {
                return this.isShowDeptFeeDetail;
            }
        }

        /// <summary>
        /// ����ʱ�Ƿ��ӡ
        /// </summary>
        private bool isChargePrint = false;
        [Category("�ؼ�����"), Description("����ʱ���Ƿ��ӡ������Ŀ true=��  false=��")]
        public bool IsChargePrint
        {
            set
            {
                this.isChargePrint = value;

            }
            get
            {
                return this.isChargePrint;
            }
        }

        /// <summary>
        /// �Ƿ��жϿ��
        /// </summary>
        protected bool isJudgeStore = false;

        [Category("�ؼ�����"), Description("���ȷ���շ�ʱ������Ʊǰ�Ƿ��жϿ�� true=��  false=��")]
        public bool IsJudgeStore
        {
            get
            {
                return this.isJudgeStore;
            }
            set
            {
                this.isJudgeStore = value;
            }
        }
        /// <summary>
        /// �Ƿ������۱���
        /// </summary>
        protected bool isCanSaveCharge = false;

        [Category("�ؼ�����"), Description("���ȷ���շ�ʱ���Ƿ������۱��� true=��  false=��")]
        public bool IsCanSaveCharge
        {
            get
            {
                return this.isCanSaveCharge;
            }
            set
            {
                this.isCanSaveCharge = value;
            }
        }
        #endregion

        #region ����

        #region ˽�з���

        /// <summary>
        /// ��ʼ�����Ʋ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� 01</returns>
        protected virtual int InitControlParams()
        {
            //ҽ����HIS����ʱ�շ�
            this.isCanFeeWhenTotCostDiff = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.FEE_WHEN_TOTDIFF, true, false);
            //ҽ����HIS����ʱ�շ�
            this.isAutoBankTrans = this.controlParamIntegrate.GetControlParam<bool>("MZ9001", true, false);
            // �����շ��Ƿ�ֻ�����ȡ�ʻ���
            // {B1B1CC9F-BFC3-4b64-B16E-AECC8B6FAEF4}
            this.isAccountPayOnly = false;//this.controlParamIntegrate.GetControlParam<bool>("MZ2011", true, false);
            //�շѽ��ȡ���Ƿ���ò�����ϸ��ʽ
            this.isRoundFeeByDetail = this.controlParamIntegrate.GetControlParam<string>("MZ9927", true, string.Empty);

            //�Ƿ�����lis �����㷨
            this.isOpenLisCulate = this.controlParamIntegrate.GetControlParam<bool>("MZ9929", true, false);

            return 1;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Init()
        {
            this.InitControlParams();

            if (this.LoadPulgIns() == -1)
            {
                return -1;
            }

            this.InitRegisterControl();

            this.InitItemInputControl();

            this.InitRightControl();

            this.InitLeftControl();

            this.InitPopFeeControl();

            this.InitPopShowPatient();

            this.Refresh();

            #region {DBA4A9CD-4484-4a95-9946-F7C291DDB813}
            this.plBLeft.Width = leftControlWith;
            this.neuSplitter2.Left = leftControlWith;
            this.plBRight.Width = this.Parent.Parent.Parent.Parent.Width - leftControlWith;
            #endregion
            //{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            //////this.FindForm().FormClosed += new FormClosedEventHandler(ucCharge_FormClosed);
            //////this.iMultiScreen.ShowScreen();


            if (this.undrugManager.Hospital.User01 == "2")
            {
                MessageBox.Show("����Ӧ������ע���޸�ȷ�Ϸ�Ʊ���룡", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return 1;
        }

        void ucCharge_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.iMultiScreen.CloseScreen();

        }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void ChangeRecipe()
        {
            ArrayList feeDetails = this.itemInputControl.GetFeeItemListForCharge(false);
            this.registerControl.ModifyFeeDetails = (ArrayList)feeDetails.Clone();
            this.registerControl.AddNewRecipe();
        }

        /// <summary>
        /// ��ʼ���໼�ߵ�������
        /// </summary>
        protected virtual void InitPopShowPatient()
        {
            fPopWin.Width = ucShow.Width + 10;
            fPopWin.MinimizeBox = false;
            fPopWin.MaximizeBox = false;

            ucShow.IsCanReRegister = this.controlParamIntegrate.GetControlParam<bool>("MZ0203", true, false);

            fPopWin.Controls.Add(ucShow);
            ucShow.Dock = DockStyle.Fill;
            fPopWin.Height = 200;
            fPopWin.Visible = false;
            fPopWin.KeyDown += new KeyEventHandler(fPopWin_KeyDown);
            this.ucShow.SelectedPatient += new ucShowPatients.GetPatient(ucShow_SelectedPatient);
        }

        /// <summary>
        /// ѡ�����¼�
        /// </summary>
        /// <param name="register"></param>
        protected virtual void ucShow_SelectedPatient(FS.HISFC.Models.Registration.Register register)
        {
            ((Control)this.registerControl).Focus();
            //this.registerControl.PatientInfo = register;

            if (register == null)
            {
                return;
            }

            //this.itemInputControl.PatientInfo = register;
            //�շ��ж�
            if (this.IsValidFee && this.IsShowSiPerson)
            {
                this.medcareInterfaceProxy.SetPactCode(register.Pact.ID);
                // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
                this.medcareInterfaceProxy.IsLocalProcess = false;

                long returnValue = this.medcareInterfaceProxy.Connect();
                if (returnValue == -1)
                {
                    MessageBox.Show(Language.Msg("���Ӵ����������ݿ�ʧ��!") + this.medcareInterfaceProxy.ErrCode);

                    this.Clear();
                    this.medcareInterfaceProxy.Disconnect();

                    return;
                }

                returnValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(register);
                if (returnValue != 1)
                {
                    MessageBox.Show(Language.Msg("��ô������߻�����Ϣʧ��!") + this.medcareInterfaceProxy.ErrCode);

                    // this.Clear();
                    this.medcareInterfaceProxy.Disconnect();

                    // return; //��ҽ���ţ����Էѽ��㣬���в��ܷ���
                }
            }
            //by niuxinyuan
            this.registerControl.PatientInfo = register;
            if (register == null)
            {
                return;
            }
            #region ����Ƿ��Ǹ߱��շѲ��� add by yerl

            //DataSet ds = new DataSet();
            //outpatientManager.ExecQuery("select aa.code from com_dictionary aa where aa.type='YkDept'", ref ds);

            //List<string> YkDeptID = new List<string>();
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    YkDeptID.Add(dr.ItemArray[0].ToString());
            //}
            //int regType=YkDeptID.Where(a => a == register.DoctorInfo.Templet.Dept.ID).Count();
            //int operType=YkDeptID.Where(a => a == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID).Count();
            //if (regType > 0 && operType < 1)
            //{
            //    MessageBox.Show("�˲���Ϊ�˿���������,���½�˿����ҽ����շ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.Clear();
            //    return;
            //}
            //if (operType > 0 && regType < 1)
            //{
            //    MessageBox.Show("�˲���Ϊ��ͨ��������,���½��ͨ���ҽ����շ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.Clear();
            //    return;
            //}

            #endregion

            //{21659409-F380-421f-954A-5C3378BB9FD6}
            this.rightControl.SetInfomation(this.registerControl.PatientInfo, null, null, null, "4");

            this.itemInputControl.PatientInfo = register;
            //this.medcareInterfaceProxy.Disconnect();

            //��û��ߵĻ�����Ϣ
            ArrayList feeItemLists = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(register.ID);
            if (feeItemLists == null)
            {
                MessageBox.Show(Language.Msg("������Ŀʧ��!") + outpatientManager.Err);

                return;
            }
            this.itemInputControl.RecipeSequence = this.registerControl.RecipeSequence;

            if (isShowDeptFeeDetail && this.isValidFee == false)
            {
                ArrayList alCurrentDeptFeeItemList = new ArrayList();
                string currentDept = ((FS.HISFC.Models.Base.Employee)this.outpatientManager.Operator).Dept.ID;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeItemLists)
                {
                    if (currentDept.Equals(feeItemList.ExecOper.Dept.ID)
                        ||
                        (currentDept.Equals(feeItemList.ConfirmOper.Dept.ID) && feeItemList.FTSource == "0")
                        )
                    {
                        alCurrentDeptFeeItemList.Add(feeItemList);
                    }
                }
                this.registerControl.FeeDetails = alCurrentDeptFeeItemList;
            }
            else
            {
                //��ʾ���ߵķַ���Ϣ
                this.registerControl.FeeDetails = (ArrayList)feeItemLists.Clone();
            }
            //ֻ��ʾ�����ҵ���Ŀ

            this.itemInputControl.IsCanAddItem = this.registerControl.IsCanAddItem;
            //�õ���ǰ�����շ����к�
            this.itemInputControl.RecipeSequence = this.registerControl.RecipeSequence;
            //���շѿؼ���ʾ���߻��۵���Ϣ
            this.itemInputControl.ChargeInfoList = this.registerControl.FeeDetailsSelected;
            this.registerControl_SeeDoctChanged(this.registerControl.RecipeSequence, this.registerControl.PatientInfo.DoctorInfo.Templet.Dept.ID.ToString(), this.registerControl.PatientInfo.DoctorInfo.Templet.Doct.Clone());
        }

        /// <summary>
        /// �ж�����շ���Ŀ�Ƿ�ͣ�õ�
        /// </summary>
        /// <param name="feeItemLists">Ҫ�жϵķ�����ϸ</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        protected virtual bool IsItemValid(ArrayList feeItemLists)
        {
            string tmpValue = "0";

            bool isJudgeValid = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.STOP_ITEM_WARNNING, false, false);

            if (!isJudgeValid) //�������Ҫ�жϣ�Ĭ�϶�û��ͣ��
            {
                return true;
            }

            foreach (FeeItemList f in feeItemLists)
            {
                if (f.Item.ID == "999")
                {
                    continue;
                }

                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item drugItem = this.pharmacyIntegrate.GetItem(f.Item.ID);
                    if (drugItem == null)
                    {
                        MessageBox.Show(Language.Msg("��ѯҩƷ��Ŀ����!") + pharmacyIntegrate.Err);

                        return false;
                    }
                    if (drugItem.IsStop)
                    {
                        MessageBox.Show("[" + drugItem.Name + Language.Msg("]�Ѿ�ͣ��!����֤���շ�!"));

                        return false;
                    }
                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrugItem = this.undrugManager.GetUndrugByCode(f.Item.ID);
                    if (undrugItem == null)
                    {
                        MessageBox.Show(Language.Msg("��ѯ��ҩƷ��Ŀ����!") + undrugManager.Err);

                        return false;
                    }
                    if (undrugItem.ValidState != "1")//ͣ��
                    {
                        MessageBox.Show("[" + undrugItem.Name + Language.Msg("]�Ѿ�ͣ�û����������֤���շ�!"));

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ���۱���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SaveCharge()
        {
            if (this.registerControl.PatientInfo == null || this.registerControl.PatientInfo.PID.CardNO == "")
            {
                MessageBox.Show(Language.Msg("û�л�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return -1;
            }
            this.registerControl.GetRegInfo();
            try
            {
                if (this.registerControl.PatientInfo.PID.CardNO == null || this.registerControl.PatientInfo.PID.CardNO == "")
                {
                    MessageBox.Show(Language.Msg("û�л�����Ϣ!"));
                    ((Control)this.registerControl).Focus();

                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ((Control)this.registerControl).Focus();

                return -1;
            }
            if (!this.registerControl.IsPatientInfoValid())
            {
                ((Control)this.registerControl).Focus();

                return -1;
            }

            if (this.registerControl.PatientInfo.ChkKind == "1" || this.registerControl.PatientInfo.ChkKind == "2")
            {
                MessageBox.Show(Language.Msg("��컼����ʱ��֧�ֻ��۱���!"));

                return -1;
            }

            if (!this.itemInputControl.IsValid)
            {
                return -1;
            }

            this.itemInputControl.StopEdit();

            ArrayList feeDetails = this.registerControl.FeeSameDetails;//���л�����Ϣ
            ArrayList feeSelectedList = this.registerControl.FeeDetailsSelected;//��ѡ������Ϣ

            if (feeDetails == null)
            {
                MessageBox.Show(Language.Msg("��÷�����Ϣ����!"));

                return -1;
            }

            int count = 0;

            foreach (ArrayList temp in feeDetails)
            {
                count += temp.Count;
            }

            if (count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return -1;
            }

            string errText = "";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool returnValue = false;
            ArrayList printInfo = new ArrayList();

            foreach (ArrayList temp in feeDetails)
            {
                //zhouxs 2007-11-25
                ArrayList a = new ArrayList();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in temp)
                {
                    f.Invoice.ID = "";
                    f.FeeOper.OperTime = DateTime.MinValue;
                    f.InvoiceCombNO = "";
                    f.FeeOper.ID = "";


                    if (this.isValidFee==false)
                    {
                        if (f.FTSource == "0")
                        {
                            //�����ҩƷ
                            if (f.Item.ItemType == EnumItemType.Drug && string.IsNullOrEmpty(f.StockOper.Dept.ID))
                            {
                                if (string.IsNullOrEmpty(f.ConfirmOper.Dept.ID))
                                {
                                    f.ConfirmOper.Dept.ID = f.ExecOper.Dept.ID;
                                    f.ConfirmOper.Dept.Name = f.ExecOper.Dept.Name;

                                }

                                f.StockOper.Dept.ID = f.ConfirmOper.Dept.ID;
                                f.StockOper.Dept.Name = f.ConfirmOper.Dept.Name;
                            }

                            //ִ�п���=��ǰ����
                            f.ExecOper.Dept.ID = ((FS.HISFC.Models.Base.Employee)this.outpatientManager.Operator).Dept.ID;
                            f.ExecOper.Dept.Name = ((FS.HISFC.Models.Base.Employee)this.outpatientManager.Operator).Dept.Name;
                        }
                    }
                    a.Add(f);
                }
                returnValue = this.peFeeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Save, this.registerControl.PatientInfo, null, null, a, null, null, ref errText);
                //returnValue = feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Save, this.registerControl.PatientInfo, null, null,temp, null, ref errText);
                //end zhouxs

                //printInfo.AddRange(a);
            }
            if (!returnValue)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                isFee = false;

                this.itemInputControl.SetFocus();//�ȼ��ϣ���֪���в���
                MessageBox.Show(errText);

                return -1;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            isFee = false;

            if (isChargePrint)
            {
                foreach (FS.HISFC .Models .Fee .Outpatient.FeeItemList tempFeeItem in feeSelectedList)
                {
                    printInfo.Add(tempFeeItem);
                }
                this.PrintGuide(this.registerControl.PatientInfo, null, printInfo);
            }

            msgInfo = Language.Msg("���۳ɹ�!");

            MessageBox.Show(msgInfo);


            this.Clear();

            this.Refresh();
            return 1;
        }

        /// <summary>
        /// �շ�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SaveFee()
        {
            //Ӧ�����ж�
            if (this.outpatientManager.Hospital.User01.Trim() == "2")
            {
                DateTime recentUpdate = new DateTime();
                int rev = this.feeIntegrate.GetRecentUpdateInvoiceTime(this.outpatientManager.Operator.ID, "INVOICE-C", ref recentUpdate);
                if (recentUpdate < this.outpatientManager.GetDateTimeFromSysDateTime().AddMinutes(-30))
                {
                    MessageBox.Show("����ķ�Ʊ����ʱ���ѳ���30���ӣ�\n�����¸��·�Ʊ����ʹ��Ӧ�����շѣ�", "����", MessageBoxButtons.OK);
                    return -1;
                }
            }

            decimal selfDrugCost = 0;//�Է�ҩ���
            decimal overDrugCost = 0;//����ҩ���
            decimal ownCost = 0;//�Էѽ��
            decimal pubCost = 0;//�籣֧�����
            decimal totCost = 0;//�ܽ��
            decimal payCost = 0;//�Ը����
            string errText = "";//������Ϣ
            decimal formerTotCost = 0;//�Աȵ��ܽ��

            if (this.registerControl.PatientInfo == null || this.registerControl.PatientInfo.PID.CardNO == null || this.registerControl.PatientInfo.PID.CardNO == string.Empty)
            {
                MessageBox.Show(Language.Msg("û�л�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return -1;
            }

            //�жϻ���¼�����Ƿ���Ϣ����
            if (!this.registerControl.IsPatientInfoValid())
            {
                ((Control)this.registerControl).Focus();

                return -1;
            }

            //���»�ùҺ���Ϣ
            this.registerControl.GetRegInfo();

            if (!this.itemInputControl.IsValid)
            {
                return -1;
            }

            //��Ŀ¼��ؼ�ֹͣ�༭
            this.itemInputControl.StopEdit();

            //��֤����������Ƿ�Ϸ�
            if (!this.leftControl.IsValid())
            {
                MessageBox.Show(this.leftControl.ErrText);
                this.leftControl.SetFocus();

                return -1;
            }

            //��õ�ǰ¼����Ŀ��Ϣ����
            this.comFeeItemLists = this.itemInputControl.GetFeeItemList();
            if (comFeeItemLists == null)
            {
                MessageBox.Show(this.itemInputControl.ErrText);
                ((Control)this.registerControl).Focus();

                return -1;
            }
            if (comFeeItemLists.Count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return -1;
            }
            #region ����Lis�Թ�
            if (isOpenLisCulate)
            {
                ArrayList alLisTube = new ArrayList();
                decimal dCost = 0;
                this.iLisCalculateTube = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                                FS.HISFC.BizProcess.Interface.FeeInterface.ILisCalculateTube>(this.GetType());
                if (this.iLisCalculateTube != null)
                {
                    this.iLisCalculateTube.LisCalculateTubeForOutPatient(this.registerControl.PatientInfo, comFeeItemLists,
                        (this.comFeeItemLists[0] as FeeItemList).RecipeSequence, ref dCost, ref alLisTube);
                    if (alLisTube != null && alLisTube.Count > 0)
                    {
                        ownCost += dCost;
                        totCost = ownCost + payCost + pubCost;
                        comFeeItemLists.AddRange(alLisTube);

                        //��ʾLIS�Թ����
                        string mess = "";
                        decimal dlisCost = 0m;
                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alLisTube)
                        {
                            dlisCost += feeitem.FT.TotCost;
                            mess += "[" + feeitem.Item.ID + ":" + feeitem.Item.Name + "] " + feeitem.Item.Qty + "��";
                            mess += System.Environment.NewLine;

                        }
                        mess += System.Environment.NewLine + "Lis �Թ� �ܽ� " + dlisCost.ToString();
                        if (!string.IsNullOrEmpty(mess))
                        {
                            if (DialogResult.No == MessageBox.Show(mess, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            #endregion
            //�ж��Ƿ�����Ŀͣ��
            if (!this.IsItemValid(comFeeItemLists))
            {
                this.itemInputControl.SetFocus();

                return -1;
            }

            if (this.IsJudgeStore)
            {
                for (int row = 0; row < comFeeItemLists.Count; row++)
                {
                    FeeItemList f = comFeeItemLists[row] as FeeItemList;
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!IsStoreEnough(f, f.Item.Qty.ToString()))
                        {
                            this.itemInputControl.SetFocus();

                            return -1;
                        }
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //��ʼ��������
            this.medcareInterfaceProxy.BeginTranscation();
            //���ô����ĺ�ͬ��λ����
            this.medcareInterfaceProxy.SetPactCode(this.registerControl.PatientInfo.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;
            //���Ӵ����ӿ�
            long returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //ҽ���ع����ܳ����˴���ʾ
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                MessageBox.Show(Language.Msg("ҽ�ƴ����ӿ�����ʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            //�������жϣ���ׯ�����жϵ��ձ�������
            //{DA7BE83E-5103-4766-8553-B369775DDD59}
            if (this.medcareInterfaceProxy.IsInBlackList(this.registerControl.PatientInfo))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                // ҽ���ع����ܳ����˴���ʾ
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.Disconnect();
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //{6C0AA776-45DA-48e6-9612-4722B360A8A5}����ҽ��Ԥ����ǰ,��ձ���Ԥ�������ֶ�.
            this.registerControl.PatientInfo.SIMainInfo.OwnCost = 0;
            this.registerControl.PatientInfo.SIMainInfo.PayCost = 0;
            this.registerControl.PatientInfo.SIMainInfo.PubCost = 0;
            this.registerControl.PatientInfo.SIMainInfo.TotCost = 0;
            //{6C0AA776-45DA-48e6-9612-4722B360A8A5}���

            //ɾ��������Ϊ�����������ԭ���ϴ�����ϸ
            returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsAllOutpatient(this.registerControl.PatientInfo);

            //�����ϴ�������ϸ
            returnValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
            if (returnValue == -1)
            {
                #region {88364E78-EC32-450a-95E4-A589AD361F34}
                FS.FrameWork.Management.PublicTrans.RollBack();
                //ҽ���ع����ܳ����˴���ʾ
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.Disconnect();
                #endregion
                MessageBox.Show(Language.Msg("�ϴ�������ϸʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            ////�ύ���Ͽ������ӿ�
            //this.medcareInterfaceProxy.Commit();

            //�����ӿڽ������,Ӧ�ù��Ѻ�ҽ��
            //returnValue = this.medcareInterfaceProxy.BalanceOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
            returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
            if (returnValue == -1)
            {
                #region {88364E78-EC32-450a-95E4-A589AD361F34}
                FS.FrameWork.Management.PublicTrans.RollBack();
                //ҽ���ع����ܳ����˴���ʾ
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.Disconnect();
                #endregion
                MessageBox.Show(Language.Msg("���ҽ��Ԥ������Ϣʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }
            //�Ͽ������ӿ����� �ŵ����� {AFEDD473-052A-4c8a-9EA4-9D002443DF52}
            //this.medcareInterfaceProxy.Rollback();
            //this.medcareInterfaceProxy.Disconnect();
            FS.FrameWork.Management.PublicTrans.RollBack();



            //��õ�ǰϵͳʱ��
            DateTime nowTime = this.undrugManager.GetDateTimeFromSysDateTime();

            //����û�н��д�������ʱ�ķ����ܽ��
            foreach (FeeItemList f in comFeeItemLists)
            {
                //������Ѿ�����ϸ�˻�֧����,���ȿ���ֻ���Էѻ���,��ô���Էѵ���Ϊ0, �˻�֧������Ϊ�Էѽ��.
                if (this.registerControl.PatientInfo.Pact.ID == "1" && f.IsAccounted)
                {
                    if (f.FT.OwnCost > 0)
                    {
                        f.FT.PayCost += f.FT.OwnCost;
                        f.FT.OwnCost = 0;
                    }
                }

                f.FeeOper.OperTime = nowTime;

                // ͨ�������㷨�������ܲ����������
                // {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
                formerTotCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
            }

            //���¼�����������ķ��ý��
            decimal rebateRate = 0;//{9A0B9D4F-9D7D-4cfe-9024-41030D6D75A7}
            totCost = 0;
            foreach (FeeItemList f in comFeeItemLists)
            {
                // ͨ�������㷨�������ܲ����������
                totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                //if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    overDrugCost += f.FT.ExcessCost;
                    selfDrugCost += f.FT.DrugOwnCost;
                }

                f.NoBackQty = f.Item.Qty;
                rebateRate += f.FT.RebateCost;
            }
            ownCost = totCost - this.registerControl.PatientInfo.SIMainInfo.PubCost - this.registerControl.PatientInfo.SIMainInfo.PayCost;
            payCost += this.registerControl.PatientInfo.SIMainInfo.PayCost;
            pubCost += this.registerControl.PatientInfo.SIMainInfo.PubCost;

            //�жϴ�������ǰ�ͼ�����Ƿ����
            if (!this.isCanFeeWhenTotCostDiff && this.registerControl.PatientInfo.Pact.PayKind.ID == "02" && this.registerControl.PatientInfo.SIMainInfo.TotCost != formerTotCost)//��������
            {
                // {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
                // ��Ҫ�ع�����
                string strMsg = "��Ժ�շ�ϵͳ���ܷ�����ҽ��ϵͳ���ܽ�����,������˶ԣ�";
                FS.FrameWork.Management.PublicTrans.RollBack();
                //ҽ���ع����ܳ����˴���ʾ
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg + " " + strMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.Disconnect();

                MessageBox.Show(Language.Msg(strMsg), Language.Msg("��ʾ--Lis�Թ���Ϣ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.itemInputControl.SetFocus();
                return -1;
            }

            //���н���2λС��
            ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);
            payCost = FS.FrameWork.Public.String.FormatNumber(payCost, 2);
            pubCost = FS.FrameWork.Public.String.FormatNumber(pubCost, 2);
            totCost = FS.FrameWork.Public.String.FormatNumber(totCost, 2);
            decimal shouldPayCost = 0;
            if (this.registerControl.PatientInfo.Pact.PayKind.ID == "03")
            {
                 shouldPayCost = ownCost + payCost - rebateRate;
            }
            else
            {
                 shouldPayCost = ownCost  - rebateRate;
            }

            //���ʹ���˻����򲻽�����������
            if (this.isAccountPayOnly)
            {
                decimal vacancy = 0;
                returnValue = this.accountManager.GetVacancy(this.registerControl.PatientInfo.PID.CardNO, ref vacancy);
                if (returnValue == -1)
                {
                    //ҽ���ع����ܳ����˴���ʾ
                    if (this.medcareInterfaceProxy.Rollback() == -1)
                    {
                        MessageBox.Show(this.medcareInterfaceProxy.ErrMsg + " ");
                        return -1;
                    }
                    this.medcareInterfaceProxy.Disconnect();

                    this.itemInputControl.SetFocus();
                    MessageBox.Show(this.accountManager.Err);

                    return -1;
                }

                while (vacancy < shouldPayCost)
                {
                    if (MessageBox.Show("�ʻ����㣬�Ƿ����ڳ�ֵ��\r\n�ʻ����Ϊ��" + vacancy.ToString(), "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        //ҽ���ع����ܳ����˴���ʾ
                        if (this.medcareInterfaceProxy.Rollback() == -1)
                        {
                            MessageBox.Show(this.medcareInterfaceProxy.ErrMsg + " ");
                            return -1;
                        }
                        this.medcareInterfaceProxy.Disconnect();

                        return -1;
                    }
                    else
                    {
                        FS.HISFC.Models.RADT.Patient patient = (FS.HISFC.Models.RADT.Patient)this.registerControl.PatientInfo;
                        FS.HISFC.Components.Common.Forms.frmAccountPerPay perPay = null;
                        perPay = new FS.HISFC.Components.Common.Forms.frmAccountPerPay(patient, vacancy, shouldPayCost);

                        if (perPay.ShowDialog() != DialogResult.OK)
                        {
                            //ҽ���ع����ܳ����˴���ʾ
                            if (this.medcareInterfaceProxy.Rollback() == -1)
                            {
                                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg + " ");
                                return -1;
                            }
                            this.medcareInterfaceProxy.Disconnect();
                            return -1;
                        }
                    }

                    returnValue = this.accountManager.GetVacancy(this.registerControl.PatientInfo.PID.CardNO, ref vacancy);
                    if (returnValue == -1)
                    {
                        //ҽ���ع����ܳ����˴���ʾ
                        if (this.medcareInterfaceProxy.Rollback() == -1)
                        {
                            MessageBox.Show(this.medcareInterfaceProxy.ErrMsg + " ");
                            return -1;
                        }
                        this.medcareInterfaceProxy.Disconnect();

                        MessageBox.Show(this.accountManager.Err);

                        return -1;
                    }
                }
            }
            else
            {
                #region �շѽ��ȡ��
                if (isRoundFeeByDetail != string.Empty)
                {
                    bool isInsertItemList = NConvert.ToBoolean(isRoundFeeByDetail);
                    if (isInsertItemList)
                    {
                        iOutPatientFeeRoundOff = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                                FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff>(this.GetType());
                        if (iOutPatientFeeRoundOff != null)
                        {
                            FeeItemList feeItemList = new FeeItemList();
                            // ��������С���ã��÷����б��һ����¼��С����
                            string drugFeeCode = "";

                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in comFeeItemLists)
                            {
                                if (string.IsNullOrEmpty(item.Item.MinFee.ID))
                                {
                                    continue;
                                }

                                drugFeeCode = item.Item.MinFee.ID;
                                break;
                            }
                            if (!string.IsNullOrEmpty(drugFeeCode))
                            {
                                feeItemList.User03 = drugFeeCode;
                            }

                            if (this.registerControl.PatientInfo.Pact.PayKind.ID == "03")
                            //���Ѳ��ֶ�pay_costҲ������������
                            {
                                iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.registerControl.PatientInfo, ref shouldPayCost, ref feeItemList, (this.comFeeItemLists[0] as FeeItemList).RecipeSequence);
                                if (feeItemList.Item.ID != "")
                                {
                                    {
                                        ownCost = shouldPayCost - payCost + rebateRate;//�����Żݽ��
                                        totCost = ownCost + payCost + pubCost;
                                        feeItemList.ItemRateFlag = "1";
                                        this.registerControl.PatientInfo.SIMainInfo.OwnCost = ownCost;
                                        this.registerControl.PatientInfo.SIMainInfo.TotCost = totCost;
                                        this.comFeeItemLists.Add(feeItemList);
                                    }
                                }
                            }
                            else
                            {
                                iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.registerControl.PatientInfo, ref shouldPayCost, ref feeItemList, (this.comFeeItemLists[0] as FeeItemList).RecipeSequence);
                                if (feeItemList.Item.ID != "")
                                {
                                    ownCost = shouldPayCost + rebateRate;//�����Żݽ��
                                    totCost = ownCost + payCost + pubCost;
                                    this.registerControl.PatientInfo.SIMainInfo.OwnCost = ownCost;
                                    this.registerControl.PatientInfo.SIMainInfo.TotCost = totCost;
                                    this.comFeeItemLists.Add(feeItemList);
                                }
                            }
                        }
                    }
                }
                #endregion
            }
           

            this.popFeeControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee>
                 (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_POP_FEE, null/*new Froms.frmDealBalance()*/);
            //���¶����շѵ������
            if (this.popFeeControl == null)
            {
                this.popFeeControl = new FS.HISFC.Components.OutpatientFee.Froms.frmDealBalance();
            }
            this.popFeeControl.BankTrans = this.iBankTrans;
            this.popFeeControl.IsAutoBankTrans = this.isAutoBankTrans;

            //�շѵ��������ֵ
            this.popFeeControl.PatientInfo = this.registerControl.PatientInfo;

            this.popFeeControl.Init();
            this.popFeeControl.FeeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee(popFeeControl_FeeButtonClicked);
            this.popFeeControl.ChargeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(popFeeControl_ChargeButtonClicked);
            //ʵ�ս��ı䣬����ͬ����ʾ{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            this.popFeeControl.RealCostChange += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateRealCost(popFeeControl_RealCostChange);
            
            this.popFeeControl.SelfDrugCost = selfDrugCost;
            this.popFeeControl.OverDrugCost = overDrugCost;
            if (this.registerControl.PatientInfo.Pact.PayKind.ID != "03")
            {
                this.popFeeControl.RealCost = ownCost;//del xf                 
            }
            else
            {
                this.popFeeControl.RealCost = ownCost + payCost;//add xf 
            }
            //this.popFeeControl.OwnCost = ownCost - rebateRate;//{9A0B9D4F-9D7D-4cfe-9024-41030D6D75A7}
            this.popFeeControl.OwnCost = ownCost; // {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
            this.popFeeControl.PayCost = payCost;
            //this.popFeeControl.PubCost = pubCost + rebateRate;

            this.popFeeControl.PubCost = pubCost; // {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
            this.popFeeControl.TotCost = totCost;
            //this.popFeeControl.TotOwnCost = ownCost - rebateRate;//{9A0B9D4F-9D7D-4cfe-9024-41030D6D75A7}
             
            this.popFeeControl.RebateRate = rebateRate; // {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
            if (this.registerControl.PatientInfo.Pact.PayKind.ID != "03")
            {
                this.popFeeControl.TotOwnCost = ownCost; // {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}//del xf 
                //this.popFeeControl.TotOwnCost = ownCost + payCost;//add xf 
            }
            else
            {
                this.popFeeControl.TotOwnCost = ownCost + payCost;//add xf 
            }
            //********************
            #region ������ϸ��ֵ
            this.popFeeControl.FeeDetails = comFeeItemLists;
            #endregion
            //********************



            string invoiceNO = "";//��ǰ�շѷ�Ʊ��
            string realInvoiceNO = this.leftControl.InvoiceNO;//��ǰ��ʾ��Ʊ��

            FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(this.undrugManager.Operator.ID);

            //��ñ����շ���ʼ��Ʊ��
            int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee, "C", ref invoiceNO, ref realInvoiceNO, ref errText);
            if (iReturnValue == -1)
            {
                MessageBox.Show(errText);

                return -1;
            }

            //������з�Ʊ�ͷ�Ʊ��ϸ�ļ���
            //********************
            #region ���ɷ�Ʊ����Ʊ��ϸ

            //{18B0895D-9F55-4d93-B374-69E96F296D0D}  ����ȡ��Ʊ������Bug����
            FS.HISFC.Components.OutpatientFee.Class.Function.IsQuitFee = false;

            ArrayList balancesAndBalanceLists = FS.HISFC.Components.OutpatientFee.Class.Function.MakeInvoice(this.feeIntegrate, this.registerControl.PatientInfo, comFeeItemLists, invoiceNO, realInvoiceNO, ref errText);
            #endregion
            //********************
            if (balancesAndBalanceLists == null)
            {
                MessageBox.Show(errText);

                return -1;
            }

            ArrayList alInvoice = (ArrayList)balancesAndBalanceLists[0];
            if (alInvoice.Count <= 0)
            {
                MessageBox.Show("��Ʊ����Ϊ0��");

                return -1;
            }


            this.popFeeControl.InvoiceFeeDetails = (ArrayList)balancesAndBalanceLists[2];


            //���շѵ��������ֵ�շѷ�Ʊ��ϸ��Ϣ
            //********************
            #region ��Ʊ��ϸ��ֵ
            this.popFeeControl.InvoiceDetails = (ArrayList)balancesAndBalanceLists[1];
            #endregion
            //********************
            ///�����ҽ������ҽ����Ʊ�����⴦��,����Ϊ��ʱ����

            #region ��δ���
            if (this.registerControl.PatientInfo.Pact.PayKind.ID == "02")
            {
                foreach (Balance balance in (ArrayList)balancesAndBalanceLists[0])
                {
                    //if (balance.Memo == "4")//���˷�Ʊ!
                    {
                        balance.FT.PubCost = pubCost;
                        balance.FT.PayCost = payCost;
                        balance.FT.OwnCost = balance.FT.TotCost - pubCost - payCost;
                    }
                    ArrayList tempFeeItemListArray = (ArrayList)balancesAndBalanceLists[2];
                    for (int i = 0; i < tempFeeItemListArray.Count; i++)
                    {

                        FeeItemList tempFeeItemList = ((ArrayList)tempFeeItemListArray[i])[0] as FeeItemList;

                        if (balance.Invoice.ID == tempFeeItemList.Invoice.ID)
                        {

                        }
                    }
                }
            }

            #endregion
            ////���շѵ��������ֵ�շѷ�Ʊ��Ϣ
            //********************
            #region ��Ʊ��ֵ
            this.popFeeControl.Invoices = (ArrayList)balancesAndBalanceLists[0];
            #endregion


            //�����ж��շ��Ƿ�Ϸ�:{DA12F709-B696-4eb9-AD3B-6C9DB7D780CF}
            if (this.iFeeExtendOutpatient != null)
            {
                //bool isValid = iFeeExtendOutpatient.IsValid(this.registerControl.PatientInfo, (ArrayList)balancesAndBalanceLists[0], comFeeItemLists, new ArrayList(), (ArrayList)balancesAndBalanceLists[1]);

                //if (!isValid)
                //{
                //    MessageBox.Show(iFeeExtendOutpatient.Err);

                //    return -1;
                //}
            }//{DA12F709-B696-4eb9-AD3B-6C9DB7D780CF}����

            this.rightControl.SetInfomation(this.registerControl.PatientInfo, this.popFeeControl.FTFeeInfo, comFeeItemLists, null,"3");
            string patientId = this.registerControl.PatientInfo.ID;//���������ӿ�ʹ��
            string cardNo = this.registerControl.PatientInfo.PID.CardNO;//���������ӿ�ʹ��

            //********************
            //��ʾ�����շѲ��
            if (!((Control)this.popFeeControl).Visible)
            {
                this.popFeeControl.IsSuccessFee = false;
                ((Control)this.registerControl).Focus();
                this.popFeeControl.SetControlFocus();
                ((Control)this.popFeeControl).Location = new Point(this.Location.X + 150, this.Location.Y + 50);
                ((Form)this.popFeeControl).ShowDialog();
            }
            if (this.popFeeControl.IsPushCancelButton)
            {
                this.itemInputControl.SetFocus();
            }
            //ȡ�������ҽ���ع�{AFEDD473-052A-4c8a-9EA4-9D002443DF52}
            if (!this.popFeeControl.IsSuccessFee)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //ҽ���ع����ܳ����˴���ʾ
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.Disconnect();
                this.popFeeControl.IsSuccessFee = false;
            }
            else
            {
                if (isFee)
                {
                    this.Clear();
                }
            }
            #region ��������շѽӿ�
            tjwebforhis webServiceForPe = new tjwebforhis();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in comFeeItemLists)
            {
                string peResult = webServiceForPe.GetItemChargeStatusPart(patientId, cardNo, item.Item.ID, "1");
                if (!"0".Equals(peResult))
                {
                    MessageBox.Show("���û������ӿ��շѳ�������ϵ�Է������ˣ��շ���Ŀ��" + item.Item.Name);
                }
            }
            #endregion

            return 1;
        }

        //������ʾʵ�ս�Ӧ�ҽ��{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
        public void popFeeControl_RealCostChange(string realcost, string returncost)
        {
            this.popFeeControl.FTFeeInfo.RealCost = FS.FrameWork.Function.NConvert.ToDecimal(realcost.ToString());
            string[] str = returncost.Split('|');
            string cost = str[0];
            if (str.Length > 1)
            {
                this.popFeeControl.FTFeeInfo.Memo = str[1];
            }
            this.popFeeControl.FTFeeInfo.ReturnCost = FS.FrameWork.Function.NConvert.ToDecimal(cost);
            this.rightControl.SetInfomation(this.registerControl.PatientInfo, this.popFeeControl.FTFeeInfo, comFeeItemLists, null, "3");
        }

        /// <summary>
        /// ˢ����Ŀ�б�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int RefreshItem()
        {
            this.itemInputControl.RefreshItem();

            return 1;
        }

        public override void Refresh()
        {
            if (this.tv != null)
            {
                this.tv.Refresh();
            }
            //base.Refresh();
        }

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Clear()
        {
            this.itemInputControl.Clear();
            this.registerControl.Clear();
            this.leftControl.Clear();
            this.rightControl.Clear();

            //if (Screen.AllScreens.Length > 1)
            if(isValidFee)
            {
                //��ʾ��ʼ������{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                FS.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as FS.HISFC.Models.Base.Employee;
                System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                 
                lo.Add("");//Register
                lo.Add("");//FS.HISFC.Models.Base.FT,
                lo.Add("");//feeItemLists
                lo.Add("");//diagLists
                //otherinformation
                string[] feePerson = new string[10];
                feePerson[0] = currentOperator.ID;
                feePerson[1] = currentOperator.Name;
                lo.Add(feePerson);
                this.iMultiScreen.ListInfo = lo;
            }
        }
        protected void Pact_Foucs(object sender, EventArgs e)
        {
            (registerControl as FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientInfomation).CustomMethod();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int LoadPulgIns()
        {
            //��ʼ�����߻�����Ϣ���;

            try
            {
                this.registerControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientInfomation>
                    (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_REGINFO, null/*new ucPatientInfo()*/);
                if (this.registerControl == null)
                {
                    this.registerControl = new ucPatientInfo();
                }
                //�������޸Ľӿ������IsCanModifyPact���ԣ�so,����object��SetValue()������ֵ��ucPatientInfo�����ԣ�~
                (this.registerControl).GetType().GetProperty("IsCanModifyPact").SetValue(this.registerControl, false, null);
                

                //this.itemInputControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay>
                //    ("TJ0081", null/* new ucDisplay()*/);//FS.HISFC.BizProcess.Integrate.Const.INTERFACE_ITEM_INPUT
                if (this.itemInputControl == null)
                {
                    this.itemInputControl = new ucDisplay();
                }
                this.itemInputControl.ItemKind = itemKind;
                itemInputControl.CustomEvent += new EventHandler(Pact_Foucs);


                this.leftControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft>
                    (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_LEFT, null/*new ucInvoicePreview()*/);
                if (this.leftControl == null)
                {
                    this.leftControl = new ucInvoicePreview();
                }
                //�����ж��շѻ��ǻ���
                this.leftControl.IsValidFee = this.IsValidFee;
                this.leftControl.IsPreFee = this.isPreFee;
                this.itemInputControl.LeftControl = this.leftControl;

                this.popFeeControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee>
                    (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_POP_FEE, null/*new Froms.frmDealBalance()*/);
                if (this.popFeeControl == null)
                {
                    this.popFeeControl = new FS.HISFC.Components.OutpatientFee.Froms.frmDealBalance();
                }
                this.rightControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight>
                    (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_RIGHT, null/*new ucCostDisplay()*/);
                if (this.rightControl == null)
                {
                    this.rightControl = new ucCostDisplay();
                }
                this.rightControl.IsPreFee = this.isPreFee;

                this.itemInputControl.RightControl = this.rightControl;
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.itemInputControl.IsCanSelectItemAndFee = this.isCanSelectItemAndFee;
                this.itemInputControl.YBPactCode = this.ybPactCode;
                this.popFeeControl.IsAutoBankTrans = this.isAutoBankTrans;
                this.rightControl.SetMedcareInterfaceProxy(this.medcareInterfaceProxy);

                //��ʼ���շѺ����жϽӿ�{DA12F709-B696-4eb9-AD3B-6C9DB7D780CF}
                iFeeExtendOutpatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<FS.HISFC.BizProcess.Interface.FeeInterface.IFeeExtendOutpatient>(this.GetType());

                //
                this.afterFee = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.OutpatientFee.Controls.ucCharge), typeof(FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee)) as FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee;

                //�����ӿ�{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                //if (Screen.AllScreens.Length > 1) 
                if(isValidFee)
                {
                    iMultiScreen = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                        FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen>(this.GetType());
                    if (iMultiScreen == null)
                    {
                        iMultiScreen = new FS.HISFC.Components.OutpatientFee.Forms.frmMiltScreen();

                    }
                    
                    //��ʾ��ʼ������
                    FS.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as FS.HISFC.Models.Base.Employee;
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add("");//register
                    lo.Add("");//FT
                    lo.Add("");//feeitemlist
                    lo.Add("");//diagitemlist
                    //otherinformation
                    string[] feePerson = new string[10];
                    feePerson[0] = currentOperator.ID;
                    feePerson[1] = currentOperator.Name;
                    lo.Add(feePerson );
                    this.iMultiScreen.ListInfo = lo; 
                    //
                    iMultiScreen.ShowScreen();

                    this.rightControl.MultiScreen = this.iMultiScreen;
                    this.FindForm().Activated += new EventHandler(ucCharge_Activated);
                    this.FindForm().Deactivate += new EventHandler(ucCharge_Deactivate);
                }
                //�����ӿ�
                iBankTrans = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                    FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans>(this.GetType());
                if (iBankTrans == null)
                {
                    iBankTrans = new FS.HISFC.Components.OutpatientFee.Forms.frmBankTrans();
                }
                this.popFeeControl.BankTrans = iBankTrans;
            }
            catch (Exception e)
            {
                MessageBox.Show(Language.Msg("���� ���߻�����Ϣ���ʧ��!") + e.Message);

                return -1;
            }

            return 1;
        }
        #region �������{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
        
       
        public void ucCharge_Deactivate(object sender, EventArgs e)
        {
            if (!isShowMultScreenAll)
            {
                this.iMultiScreen.CloseScreen();
            }
        }

        public void ucCharge_Activated(object sender, EventArgs e)
        {
            if (iMultiScreen == null)
            {
                iMultiScreen = new FS.HISFC.Components.OutpatientFee.Forms.frmMiltScreen();
                //��ʾ��ʼ������
                FS.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as FS.HISFC.Models.Base.Employee;
                System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                lo.Add("");//register
                lo.Add("");//FT
                lo.Add("");//feeitemlist
                lo.Add("");//diagitemlist
                //otherinformation
                string[] feePerson = new string[10];
                feePerson[0] = currentOperator.ID;
                feePerson[1] = currentOperator.Name;
                lo.Add(feePerson);
                this.iMultiScreen.ListInfo = lo; 

            }


            iMultiScreen.ShowScreen();
        }
        #endregion
        /// <summary>
        /// ��ʼ�������շѲ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitPopFeeControl()
        {
            if (this.popFeeControl == null)
            {
                return -1;
            }

            this.popFeeControl.Init();
            //this.popFeeControl.FeeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee(popFeeControl_FeeButtonClicked);
            //this.popFeeControl.ChargeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(popFeeControl_ChargeButtonClicked);

            return 1;
        }

        /// <summary>
        /// ��Ӧ�����շѿؼ��Ļ��۱����¼�
        /// </summary>
        protected virtual void popFeeControl_ChargeButtonClicked()
        {
            if (this.isCanSaveCharge)
            {
                this.SaveCharge();
            }
            else
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û�л���Ȩ��"));
            }
        }

        /// <summary>
        /// �շѰ�ť����
        /// </summary>
        /// <param name="balancePays">֧����ʽ��Ϣ</param>
        /// <param name="invoices">��Ʊ��Ϣ��������Ӧ��Ʊ�������Ϣ��ÿ�������Ӧһ����Ʊ��</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ��Ϣ����Ӧ���ν����ȫ��������ϸ��</param>
        /// <param name="invoiceFeeDetails">��Ʊ������ϸ��Ϣ������Ʊ�����ķ�����ϸ��ÿ�������Ӧ�÷�Ʊ�µķ�����ϸ��</param>
        protected virtual void popFeeControl_FeeButtonClicked(ArrayList balancePays, ArrayList invoices, ArrayList invoiceDetails, ArrayList invoiceFeeDetails)
        {
            // ��Ʊ�ų��ȴ��ڵ���12�����ԡ�9����ͷΪ��ʱ��Ʊ��
            bool isTempInvoice = false;
            string strInvoiceTemp = ((Balance)invoices[invoices.Count - 1]).Invoice.ID;
            if (strInvoiceTemp.Length >= 12 && strInvoiceTemp.StartsWith("9"))
            {
                isTempInvoice = true;


            }

            //��ŵ�ǰ���ߵ���ˮ��
            string clincCode = registerControl.PatientInfo.ID;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            string errText = "";
            this.medcareInterfaceProxy.SetPactCode(this.registerControl.PatientInfo.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            this.feeIntegrate.IsNeedUpdateInvoiceNO = true;

            long returnMedcareValue = this.medcareInterfaceProxy.Connect();
            if (returnMedcareValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return;
            }
            //�Ƿ������ϴ�
            if (this.medcareInterfaceProxy.IsUploadAllFeeDetailsOutpatient)
            {
                //�����ϴ��ߺ��ĵ�����
                #region his45 ����

                #region �����շ�
                //{143CA424-7AF9-493a-8601-2F7B1D635027}
                foreach (FeeItemList temfItem in comFeeItemLists)
                {
                    if (temfItem.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        temfItem.StockOper.Dept.ID = temfItem.ExecOper.Dept.ID;
                    }
                }
                //�����շѴ���
                //if (materialManager.MaterialFeeOutput(comFeeItemLists) < 0)
                //{
                //    //errText = materialManager.Err;
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����շ�ʧ�ܣ�") + materialManager.Err);
                //    return;
                //}
                #endregion

                bool returnValue = this.peFeeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, isTempInvoice, this.registerControl.PatientInfo,
                   invoices, invoiceDetails, comFeeItemLists, invoiceFeeDetails, balancePays, ref errText);
                this.registerControl.PatientInfo.SIMainInfo.InvoiceNo = ((FS.HISFC.Models.Fee.Outpatient.Balance)invoices[0]).Invoice.ID;

                #region ������Ϣ

                //{014680EC-6381-408b-98FB-A549DAA49B82}
                if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder() != null)
                {
                    //{014680EC-6381-408b-98FB-A549DAA49B82}
                    if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().SendFeeInfo(this.registerControl.PatientInfo, comFeeItemLists, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        //{014680EC-6381-408b-98FB-A549DAA49B82}
                        MessageBox.Show(this, "�շ�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        isFee = false;
                        return;
                    }
                }

                #endregion

                #region  �����ӿ���(����ǿ���Ϻ�����);
                //���ú�ͬ��λ

                this.medcareInterfaceProxy.SetPactCode(this.registerControl.PatientInfo.Pact.ID);
                // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
                this.medcareInterfaceProxy.IsLocalProcess = false;

                returnMedcareValue = this.medcareInterfaceProxy.Connect();
                if (returnMedcareValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return;
                }
                returnMedcareValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
                if (returnMedcareValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ��ϴ���ϸʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return;
                }
                returnMedcareValue = this.medcareInterfaceProxy.BalanceOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
                if (returnMedcareValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ��������ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return;
                }
                #endregion

                if (!returnValue)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    if (errText != "")
                    {
                        MessageBox.Show(errText);
                    }

                    isFee = false;

                    return;
                }

                this.medcareInterfaceProxy.Commit();
                this.medcareInterfaceProxy.Disconnect();
                FS.FrameWork.Management.PublicTrans.Commit();

                #region ������ȡ�ֽ��·־��
                ArrayList balancePaysClone = new ArrayList();
                foreach (BalancePay balancePay in balancePays)
                {
                    //�Ƿ�ʼ�ۼ�
                    if (registerControl.IsBeginAddUpCost)
                    {
                        if (balancePay.PayType.Name == "�ֽ�")
                        {
                            this.registerControl.AddUpCost += balancePay.FT.TotCost;
                        }
                    }
                    balancePaysClone.Add(balancePay.Clone());
                }
                #endregion

                #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
                string isUseDL = controlParamIntegrate.GetControlParam<string>("200212");
                if (!string.IsNullOrEmpty(isUseDL) && isUseDL == "1")
                {
                    //if (PACSApplyInterface == null)
                    //{
                    //    PACSApplyInterface = new FS.ApplyInterface.HisInterface();
                    //}
                    foreach (FeeItemList f in comFeeItemLists)
                    {
                        if (f.Item.SysClass.ID.ToString() == "UC" && f.Order.ID != "")
                        {
                            try
                            {
                                string applyNo = outpatientManager.GetApplyNoByRecipeFeeSeq(f);
                                //int a = PACSApplyInterface.Charge(applyNo, "1");
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("���µ������뵥�շѱ�־ʱ����" + e.Message);
                            }
                        }
                    }
                }
                #endregion

                #region//��Ʊ��ӡ
                if (!isTempInvoice)
                {
                    string invoicePrintDll = null;
                    invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

                    // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
                    // 2011-08-04
                    // ��ʱ������ʾ
                    //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                    //{
                    //    MessageBox.Show("û�����÷�Ʊ��ӡ�������շ���ά��!");

                    //}

                    this.feeIntegrate.PrintInvoice(invoicePrintDll, this.registerControl.PatientInfo, invoices, invoiceDetails, comFeeItemLists, balancePaysClone, false, ref errText);
                }
                #endregion

                #region ����ָ������ӡ

                this.PrintGuide(this.registerControl.PatientInfo, invoices, comFeeItemLists);

                #endregion

                this.popFeeControl.FTFeeInfo.User01 = ((FS.HISFC.Models.Fee.Outpatient.Balance)invoices[0]).DrugWindowsNO;

                //{21659409-F380-421f-954A-5C3378BB9FD6}
                this.rightControl.SetInfomation(this.registerControl.PatientInfo, this.popFeeControl.FTFeeInfo, comFeeItemLists, null, "4");

                isFee = true;
                if (this.afterFee != null)
                {
                    this.afterFee.AfterFee(comFeeItemLists, "0");
                }
                msgInfo = Language.Msg("�շѳɹ�!");

                MessageBox.Show(msgInfo);


                this.Clear();

                #region ��ʾ��ҩ���� ��LIS�ӿ�

                /*
                 * ��ͬҽԺ����ʵ�ӿ�ʵ��
                 * 
                if (System.IO.File.Exists(Application.StartupPath + "\\chargeLED.exe") == true)
                {
                    try
                    {
                        if (this.frmBalance.ucDealBalance1.FTFeeInfo.User01 != null && this.frmBalance.ucDealBalance1.FTFeeInfo.User01.Length > 0)
                        {
                            FS.Common.Controls.Function.ShowPatientFee("�뵽" + this.frmBalance.ucDealBalance1.FTFeeInfo.User01 + "ȡҩ", this.frmBalance.ucDealBalance1.PayCost + this.frmBalance.ucDealBalance1.OwnCost);
                        }
                    }
                    catch
                    { }
                }
                if (this.dataToLis)
                {
                    #region ����LIS�ӿ�

                    foreach (FS.HISFC.Models.Fee.OutPatient.FeeItemList feeItem in this.GetArrayToLis(this.ucChargeDisplay1.GetFeeItemListForCharge(), alFee))
                    {
                        if (feeItem.SysClass.ID.ToString() == "UL")
                        {
                            lisInterface.Function.LisSetClinicData(this.ucRegInfo1.RInfo, feeItem, FS.FrameWork.Management.PublicTrans.Trans);
                        }
                    }
                    #endregion
                }
                */
                #endregion
                #endregion
            }
            else
            {
                // ��֧���ʻ����ܣ��������ߴ�����
                //�������ϴ���С�汾������
                #region his4.5.0.1
                #region ҽ���ӿڳɹ���־λ
                Boolean isSucc = true;
                #endregion

                //ҽ������
                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                #region �ϴ�ҽ����Ϣ
                //ȫ���߲����ϴ�����
                if (true)
                {
                    #region ��¡һ��֧����Ϣ
                    ArrayList balancePaysClone = new ArrayList();
                    BalancePay balancePayCA = null;
                    //��ͷ�ۼ�
                    decimal changeCost = decimal.Zero;

                    #region ���ֽ�֧���ģ���ͳ��֧���ģ����ʻ�֧���ı��浽��¡��֧����Ϣ�����У�����¼�ֽ�֧������Ϣ��balancePayCA������
                    foreach (BalancePay balancePay in balancePays)
                    {
                        //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                        //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PS.ToString() ||
                        //    balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PB.ToString())

                        //����Ǳ����˻��� ͳ��(ҽԺ�渶)
                        if (balancePay.PayType.ID.ToString() == "PS" ||
                                balancePay.PayType.ID.ToString() == "PB")
                        {
                            balancePaysClone.Add(balancePay.Clone());
                        }
                        // �ֽ�
                        //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.CA.ToString())
                        if (balancePay.PayType.ID.ToString() == "CA")
                        {
                            balancePayCA = balancePay.Clone();
                            balancePaysClone.Add(balancePayCA);
                        }
                        changeCost += balancePay.FT.TotCost - balancePay.FT.RealCost;
                    }
                    #endregion

                    #region ��������֧����Ϣ�����ֽ�֧��������
                    // {93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                    foreach (BalancePay balancePay in balancePaysClone)
                    {
                        //if (!(balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PS.ToString() ||
                        //   balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PB.ToString() ||
                        //   balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.CA.ToString()))
                        //�����ʻ�,ͳ��(ҽԺ�渶),�ֽ�
                        if (!(balancePay.PayType.ID.ToString() == "PS" ||
                            balancePay.PayType.ID.ToString() == "PB" ||
                            balancePay.PayType.ID.ToString() == "CA"))
                        {
                            balancePayCA.FT.TotCost = balancePay.FT.TotCost;
                            balancePayCA.FT.RealCost = balancePay.FT.RealCost;
                        }
                    }
                    #endregion

                    #endregion

                    #region ����֧����ʽ��Ϣ
                    string mainInvoiceNO = string.Empty;
                    string mainInvoiceCombNO = string.Empty;
                    foreach (Balance balance in invoices)
                    {
                        //����Ʊ��Ϣ,������ֻ����ʾ��
                        if (balance.Memo == "5")
                        {
                            mainInvoiceNO = balance.ID;

                            continue;
                        }

                        //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                        if (mainInvoiceNO == string.Empty)
                        {
                            mainInvoiceNO = balance.Invoice.ID;
                            mainInvoiceCombNO = balance.CombNO;
                        }
                    }

                    int payModeSeq = 1;

                    // ������ҵ���
                    FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
                    inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    foreach (BalancePay p in balancePays)
                    {
                        p.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                        p.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                        p.Squence = payModeSeq.ToString();
                        p.IsDayBalanced = false;
                        p.IsAuditing = false;
                        p.IsChecked = false;
                        p.InputOper.ID = inpatientManager.Operator.ID;
                        p.InputOper.OperTime = inpatientManager.GetDateTimeFromSysDateTime();
                        if (string.IsNullOrEmpty(p.InvoiceCombNO))
                        {
                            p.InvoiceCombNO = mainInvoiceCombNO;
                        }
                        p.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;

                        payModeSeq++;

                        //realCost += p.FT.RealCost;
                        int iReturn;
                        if (FS.FrameWork.Management.PublicTrans.Trans != null)
                        {
                            outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }
                        iReturn = outpatientManager.InsertBalancePay(p);
                        if (iReturn == -1)
                        {

                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����֧����ʽ�����!");
                            return;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();//��߲帺��¼,��˴��ύû�����⡣




                        #region �����ʻ�����ȡ��
                        //if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                        //{
                        //    bool returnValue = feeIntegrate.AccountPay(this.registerControl.PatientInfo.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);
                        //    if (!returnValue)
                        //    {
                        //        MessageBox.Show("��ȡ�����˻�ʧ��!");

                        //        return;
                        //    }
                        //} 
                        #endregion
                    }
                    #endregion
                    //�������ս����־
                    bool ProCreateFlag = false;
                    if (registerControl.PatientInfo.SIMainInfo.ProceateLastFlag)
                    {
                        ProCreateFlag = true;
                        registerControl.PatientInfo.SIMainInfo.ProceateLastFlag = false;
                    }
                    //����ز������Ϣ
                    registerControl.PatientInfo.SIMainInfo.OutDiagnose.ID = string.Empty;
                    registerControl.PatientInfo.SIMainInfo.OutDiagnose.Name = string.Empty;

                    int invoicesIndex = 0;
                    int InvoiceCount = invoices.Count;
                    foreach (Balance myBalance in invoices)
                    {


                        InvoiceCount--;
                        if (InvoiceCount == 0 && ProCreateFlag)//��������������һ�ν��� ���һ�ŷ�Ʊ���������
                        {
                            registerControl.PatientInfo.SIMainInfo.ProceateLastFlag = true;
                        }
                        if (isSucc)//�ϴ��ύδ������ܼ���
                        {
                            #region ���½�������
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            #endregion

                            #region ���������ϸ
                            ArrayList myFeeItemListArray = new ArrayList();
                            for (int i = 0; i < invoiceFeeDetails.Count; i++)
                            {
                                ArrayList tempAarry = new ArrayList();
                                tempAarry = (ArrayList)invoiceFeeDetails[i];
                                for (int j = 0; j < tempAarry.Count; j++)
                                {

                                    ArrayList tempAarry2 = new ArrayList();
                                    tempAarry2 = (ArrayList)tempAarry[j];
                                    for (int k = 0; k < tempAarry2.Count; k++)
                                    {
                                        FeeItemList myFeeItemList = new FeeItemList();
                                        myFeeItemList = (FeeItemList)tempAarry2[k];
                                        if (myBalance.Invoice.ID == myFeeItemList.Invoice.ID)
                                        {
                                            myFeeItemListArray.Add(myFeeItemList);

                                        }
                                    }
                                }
                            }
                            #endregion

                            #region ���÷�Ʊ��
                            this.registerControl.PatientInfo.SIMainInfo.InvoiceNo = myBalance.Invoice.ID;
                            #endregion

                            #region ��ȡҽ��������Ϣ
                            returnMedcareValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(this.registerControl.PatientInfo);
                            #endregion

                            #region �����ӿڶ�������
                            if (returnMedcareValue != 1)
                            {
                                errText = "�����ӿڶ�������" + this.medcareInterfaceProxy.ErrMsg;
                                isSucc = false;
                            }
                            #endregion
                            #region  �����ӿ��ϴ���ϸʧ��
                            //{BE0275DB-0F17-453d-A122-C59D2FBF6B2C}�������ʧ�ܺ���Ȼ�ϴ���ϸ
                            if (isSucc)
                            {
                                returnMedcareValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.registerControl.PatientInfo, ref myFeeItemListArray);
                                if (returnMedcareValue != 1 /*&& isSucc*/)
                                {
                                    errText = "�����ӿ��ϴ���ϸʧ��" + this.medcareInterfaceProxy.ErrMsg;
                                    isSucc = false;
                                }
                            }
                            #endregion
                            #region �����ӿ�������� ������ fin_ipr_siinmaininfo
                            //{9E434E9D-FC87-4d85-BC0B-5D0EE99C6EEC}
                            if (isSucc)
                            {
                                returnMedcareValue = this.medcareInterfaceProxy.BalanceOutpatient(this.registerControl.PatientInfo, ref myFeeItemListArray);
                                if (returnMedcareValue != 1/* && isSucc*/)
                                {
                                    errText = "�����ӿ��������ʧ��" + this.medcareInterfaceProxy.ErrMsg;
                                    isSucc = false;
                                }
                            }

                            #endregion
                            if (isSucc)
                            {

                                #region liuq 2007-9-7 �´��룬�����ύ���㣮

                                ArrayList invoicesClinicFee;
                                ArrayList invoiceDetailsClinicFee;
                                ArrayList invoiceFeeDetailsClinicFee;

                                invoicesClinicFee = new ArrayList();
                                invoiceDetailsClinicFee = new ArrayList();
                                invoiceFeeDetailsClinicFee = new ArrayList();

                                invoicesClinicFee.Add(myBalance);
                                ArrayList invoiceDetailsClinicFeeTemp = new ArrayList();
                                invoiceDetailsClinicFeeTemp.Add((invoiceDetails[0] as ArrayList)[invoicesIndex]);
                                invoiceDetailsClinicFee.Add(invoiceDetailsClinicFeeTemp);
                                ArrayList invoiceFeeDetailsClinicFeeTemp = new ArrayList();
                                invoiceFeeDetailsClinicFeeTemp.Add((invoiceFeeDetails[0] as ArrayList)[invoicesIndex]);
                                invoiceFeeDetailsClinicFee.Add(invoiceFeeDetailsClinicFeeTemp);


                                decimal payCost = decimal.Zero;
                                decimal pubCost = decimal.Zero;
                                decimal ownCost = decimal.Zero;


                                ownCost = this.registerControl.PatientInfo.SIMainInfo.OwnCost;

                                payCost = this.registerControl.PatientInfo.SIMainInfo.PayCost;

                                pubCost = this.registerControl.PatientInfo.SIMainInfo.PubCost;
                                //{21EEC08E-53DA-458b-BEA3-0036EF6E3D37}
                                //+ this.registerControl.PatientInfo.SIMainInfo.OfficalCost
                                //+ this.registerControl.PatientInfo.SIMainInfo.OverCost;
                                #region �շѽ��ȡ��
                                if (isRoundFeeByDetail != string.Empty)
                                {
                                    bool isInsertItemList = NConvert.ToBoolean(isRoundFeeByDetail);
                                    if (isInsertItemList)
                                    {
                                        iOutPatientFeeRoundOff = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                                                FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff>(this.GetType());
                                        if (iOutPatientFeeRoundOff == null)
                                        {
                                            MessageBox.Show("����ȡ���ӿ�δ���ã�");
                                            return;
                                        }
                                        FeeItemList feeItemList = new FeeItemList();
                                        iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.registerControl.PatientInfo, ref ownCost, ref feeItemList, this.registerControl.RecipeSequence);
                                        if (feeItemList.Item.ID != "")
                                        {
                                            this.registerControl.PatientInfo.SIMainInfo.OwnCost = ownCost;
                                            myFeeItemListArray.Add(feeItemList);
                                        }
                                    }
                                }
                                #endregion
                                myBalance.FT.OwnCost = ownCost;
                                myBalance.FT.PayCost = payCost;
                                myBalance.FT.PubCost = pubCost;


                                bool returnValue = false;
                                try
                                {
                                    returnValue = this.feeIntegrate.ClinicFeeSaveFee(
                                                           FS.HISFC.Models.Base.ChargeTypes.Fee,
                                                           this.registerControl.PatientInfo,
                                                           invoicesClinicFee,
                                                           invoiceDetailsClinicFee,
                                                           myFeeItemListArray,
                                                           invoiceFeeDetailsClinicFee, null, ref errText);
                                }
                                catch (Exception ex)
                                {
                                    isFee = false;
                                    isSucc = false;
                                }
                                if (!returnValue)
                                {

                                    isFee = false;
                                    isSucc = false;
                                }
                                #endregion
                                if (isSucc)
                                {
                                    if (this.medcareInterfaceProxy.Commit() < 0)
                                    {
                                        #region ҽ�����ύ ��ʧ�� ���� ҽ������������
                                        isSucc = false;
                                        errText = "ҽ���ӿ��ύ���������������������Ƿ���ȷ";
                                        this.medcareInterfaceProxy.Rollback();
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        #endregion
                                    }
                                    else
                                    {
                                        #region �ύ���أ���ʱ�����Ǳ����ύ���ɹ������
                                        FS.FrameWork.Management.PublicTrans.Commit();
                                        #endregion
                                        #region ��Ʊ��ӡ
                                        foreach (BalancePay balancePay in balancePaysClone)
                                        {
                                            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                                            //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PS.ToString())
                                            if (balancePay.PayType.ID.ToString() == "PS") //�����˻� 
                                            {
                                                balancePay.FT.TotCost = balancePay.FT.TotCost - payCost;
                                            }
                                            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                                            //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.CA.ToString())
                                            if (balancePay.PayType.ID.ToString() == "CA") //�ֽ�
                                            {
                                                balancePay.FT.TotCost = balancePay.FT.TotCost - ownCost;
                                            }
                                            ////{93E6443C-1FB5-45a7-B89D-F21A92200CF6}

                                            //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PB.ToString()) 
                                            if (balancePay.PayType.ID.ToString() == "PB")//ͳ��(ҽԺ�渶)
                                            {
                                                balancePay.FT.TotCost = balancePay.FT.TotCost - pubCost;
                                            }
                                        }
                                        string invoicePrintDll = null;

                                        invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);
                                        this.feeIntegrate.PrintInvoice(invoicePrintDll, this.registerControl.PatientInfo, invoicesClinicFee, invoiceDetailsClinicFee, myFeeItemListArray, invoiceFeeDetailsClinicFee, balancePays, false, ref errText);
                                        #endregion

                                        #region ����ָ������ӡ

                                        this.PrintGuide(this.registerControl.PatientInfo, invoicesClinicFee, myFeeItemListArray);

                                        #endregion
                                    }
                                }
                                else
                                {
                                    this.medcareInterfaceProxy.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                }

                            }
                            else
                            {
                                this.medcareInterfaceProxy.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }

                            invoicesIndex++;
                        }
                    }
                    if (!isSucc)
                    {
                        #region ���½�������
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        #endregion

                        #region liuq 2007-9-7 �´��룬�����帺֧����ʽ��Ϣ��
                        #region ����֧����ʽ��Ϣ

                        //zjy ˵�˸�����99
                        payModeSeq = 99;

                        // ������ҵ���
                        foreach (BalancePay p in balancePaysClone)
                        {
                            p.FT.RealCost = p.FT.TotCost - changeCost;
                            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                            //if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.CA.ToString())
                            if (p.PayType.ID.ToString() == "CA")//�ֽ�
                            {
                                //���ʵ�ʽ�Ϊ��
                                if (p.FT.TotCost != decimal.Zero)
                                {
                                    //����ʵ�����,��������ͷ
                                    p.FT.RealCost = p.FT.TotCost - changeCost;
                                }
                            }

                            p.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                            p.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                            p.Squence = payModeSeq.ToString();
                            p.IsDayBalanced = false;
                            p.IsAuditing = false;
                            p.IsChecked = false;
                            p.InputOper.ID = inpatientManager.Operator.ID;
                            p.InputOper.OperTime = inpatientManager.GetDateTimeFromSysDateTime();
                            if (string.IsNullOrEmpty(p.InvoiceCombNO))
                            {
                                p.InvoiceCombNO = mainInvoiceCombNO;
                            }
                            p.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;

                            if (p.FT.RealCost != 0)
                            {
                                p.FT.TotCost = -p.FT.TotCost;
                                p.FT.RealCost = -p.FT.RealCost;
                                int iReturn;
                                iReturn = outpatientManager.InsertBalancePay(p);
                                if (iReturn == -1)
                                {
                                    MessageBox.Show("����֧����ʽ�����!");
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                }
                            }

                            #region �����ʻ�����ȡ��
                            //if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                            //{
                            //    returnValue = feeIntegrate.AccountPay(this.registerControl.PatientInfo.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);
                            //    if (!returnValue)
                            //    {
                            //        MessageBox.Show("��ȡ�����˻�ʧ��!");

                            //        return;
                            //    }
                            //} 
                            #endregion
                            FS.FrameWork.Management.PublicTrans.Commit();
                        }
                        #endregion
                        #endregion
                    }
                }
                #endregion

                this.medcareInterfaceProxy.Disconnect();


                #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
                string isUseDL = controlParamIntegrate.GetControlParam<string>("200212");
                if (!string.IsNullOrEmpty(isUseDL) && isUseDL == "1")
                {
                    //if (PACSApplyInterface == null)
                    //{
                    //    PACSApplyInterface = new FS.ApplyInterface.HisInterface();
                    //}
                    foreach (FeeItemList f in comFeeItemLists)
                    {
                        if (f.Item.SysClass.ID.ToString() == "UC" && f.Order.ID != "")
                        {
                            try
                            {
                                string applyNo = outpatientManager.GetApplyNoByRecipeFeeSeq(f);
                                //int a = PACSApplyInterface.Charge(applyNo, "1");
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("���µ������뵥�շѱ�־ʱ����" + e.Message);
                            }
                        }
                    }
                }
                #endregion


                this.popFeeControl.FTFeeInfo.User01 = ((FS.HISFC.Models.Fee.Outpatient.Balance)invoices[0]).DrugWindowsNO;

                //{21659409-F380-421f-954A-5C3378BB9FD6}
                this.rightControl.SetInfomation(this.registerControl.PatientInfo, this.popFeeControl.FTFeeInfo, comFeeItemLists, null, "1");

                //���Ʊ��ιҺŻ�����Ϣ
                this.registerControl.PrePatientInfo = this.registerControl.PatientInfo.Clone();
                this.leftControl.InitInvoice();

                isFee = true;

                if (isSucc)
                {
                    msgInfo = Language.Msg("�շѳɹ�!");
                }
                else
                {
                    msgInfo = Language.Msg("�շ�ʧ��!" + errText);
                }
                if (this.afterFee != null)
                {
                    this.afterFee.AfterFee(comFeeItemLists, "0");
                }
                MessageBox.Show(msgInfo);

                this.Clear();

                #region ��ʾ��ҩ���� ��LIS�ӿ�, ��������
                //if (System.IO.File.Exists(Application.StartupPath + "\\chargeLED.exe") == true)
                //{
                //    try
                //    {
                //        if (this.frmBalance.ucDealBalance1.FTFeeInfo.User01 != null && this.frmBalance.ucDealBalance1.FTFeeInfo.User01.Length > 0)
                //        {
                //            FS.Common.Controls.Function.ShowPatientFee("�뵽" + this.frmBalance.ucDealBalance1.FTFeeInfo.User01 + "ȡҩ", this.frmBalance.ucDealBalance1.PayCost + this.frmBalance.ucDealBalance1.OwnCost);
                //        }
                //    }
                //    catch
                //    { }
                //}
                //if (this.dataToLis)
                //{
                //    #region ����LIS�ӿ�

                //    foreach (FS.HISFC.Models.Fee.OutPatient.FeeItemList feeItem in this.GetArrayToLis(this.ucChargeDisplay1.GetFeeItemListForCharge(), alFee))
                //    {
                //        if (feeItem.SysClass.ID.ToString() == "UL")
                //        {
                //            lisInterface.Function.LisSetClinicData(this.ucRegInfo1.RInfo, feeItem, t.Trans);
                //        }
                //    }
                //    #endregion
                //}

                #endregion

                #endregion
            }

            //�˴�������ʾ�Ƿ���δ�۷���Ŀ 2011-10-26 houwb
            if (!string.IsNullOrEmpty(clincCode))
            {
                ArrayList feeItemLists = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(clincCode);
                if (feeItemLists == null)
                {
                    MessageBox.Show(Language.Msg("������Ŀʧ��!") + outpatientManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (feeItemLists.Count > 0)
                {
                    MessageBox.Show(Language.Msg("�û��߻���δ�շ���Ŀ��������շѣ�") + outpatientManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        /// <summary>
        /// ��ʼ���Ҳ�ؼ�
        /// </summary>
        /// <returns>�ɹ� 1ʧ�� -1</returns>
        protected virtual int InitRightControl()
        {
            if (this.rightControl == null)
            {
                return -1;
            }

            this.plBottom.Height = ((Control)this.rightControl).Height + 6;

            this.plBRight.Controls.Add((Control)this.rightControl);
            this.plBRight.Height = ((Control)this.rightControl).Height + 5;
            this.plBRight.Width = ((Control)this.rightControl).Width + 5;

            this.rightControl.Init();

            return 1;
        }

        /// <summary>
        /// ��ʼ�������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitLeftControl()
        {
            if (this.leftControl == null)
            {
                return -1;
            }
            leftControlWith = ((System.Windows.Forms.UserControl)(leftControl)).Width + 5;
            if (this.plBottom.Height < ((Control)this.leftControl).Height + 5)
            {
                this.plBottom.Height = ((Control)this.leftControl).Height + 5;
            }

            this.plBLeft.Controls.Add((Control)this.leftControl);
            //this.plBLeft.Height = ((Control)this.leftControl).Height;
            //this.plBLeft.Width = ((Control)this.leftControl).Width;
            ((Control)this.leftControl).Dock = DockStyle.Fill;

            this.plBottom.Height = this.plBRight.Height;

            this.leftControl.Init();


                FS.HISFC.Models.Base.Employee emplObj = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;


                FS.HISFC.Models.Base.Employee oper = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;
                if (emplObj.IsManager || emplObj.EmployeeType.ID.ToString() == "F")
                {
                    this.leftControl.InitInvoice();
                }
            

            this.leftControl.InvoiceUpdated += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(leftControl_InvoiceUpdated);

            return 1;
        }

        /// <summary>
        /// ���ؼ��ķ�Ʊ����������Ϣ�����¼�
        /// </summary>
        protected virtual void leftControl_InvoiceUpdated()
        {
            if (!((Control)this.registerControl).Focus())
            {
                ((Control)this.registerControl).Focus();
            }
            if (this.itemInputControl.IsFocus)
            {
                ((Control)this.registerControl).Focus();
            }
        }

        /// <summary>
        /// ��ʼ�����߻�����Ϣ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitRegisterControl()
        {
            if (this.registerControl == null)
            {
                return -1;
            }

            this.plTop.Controls.Add((Control)this.registerControl);
            ((Control)this.registerControl).Focus();
            this.plTop.Height = ((Control)this.registerControl).Height + 5;
            ((Control)this.registerControl).Dock = DockStyle.Fill;

            this.registerControl.Init();

            this.registerControl.ChangeFocus += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(registerControl_ChangeFocus);
            this.registerControl.PactChanged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(registerControl_PactChanged);
            this.registerControl.PriceRuleChanaged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(registerControl_PriceRuleChanaged);
            this.registerControl.RecipeSeqChanged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(registerControl_RecipeSeqChanged);
            this.registerControl.RecipeSeqDeleted += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateRecipeDeleted(registerControl_RecipeSeqDeleted);
            this.registerControl.SeeDeptChanaged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeDoctAndDept(registerControl_SeeDeptChanaged);
            this.registerControl.SeeDoctChanged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeDoctAndDept(registerControl_SeeDoctChanged);
            this.registerControl.InputedCardAndEnter += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateEnter(registerControl_InputedCardAndEnter);

            this.registerControl.IsAddUp = this.IsAddUp;

            return 1;
        }

        /// <summary>
        /// ��ʼ����Ŀ¼����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitItemInputControl()
        {
            if (this.itemInputControl == null)
            {
                return -1;
            }

            this.plMain.Controls.Add((Control)this.itemInputControl);

            ((Control)this.itemInputControl).Dock = DockStyle.Fill;

            this.itemInputControl.Init();

            this.itemInputControl.FeeItemListChanged += new FS.HISFC.BizProcess.Integrate.FeeInterface.delegateFeeItemListChanged(itemInputControl_FeeItemListChanged);

            return 1;
        }

        /// <summary>
        /// ��¼��ؼ���,��Ŀ�����仯�󴥷�
        /// </summary>
        /// <param name="al">�仯����Ŀ����</param>
        protected virtual void itemInputControl_FeeItemListChanged(System.Collections.ArrayList al)
        {
            if (this.registerControl.PatientInfo == null)
            {
                return;
            }

            this.registerControl.ModifyFeeDetails = (ArrayList)al.Clone();
            this.registerControl.DealModifyDetails();
        }

      

        /// <summary>
        /// �������뻼�߿��Żس�����¼�
        /// </summary>
        /// <param name="cardNO">����</param>
        /// <param name="orgNO">ԭʼ����</param>
        /// <param name="cardLocation">���ŵ�λ��</param>
        /// <param name="cardHeight">���ŵĸ߶�</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual bool registerControl_InputedCardAndEnter(string cardNO, string orgNO, Point cardLocation, int cardHeight)
        {
            ucShow.OrgCardNO = orgNO;
            ucShow.CardNO = cardNO;
            ucShow.operType = "1";//ֱ������
            if (ucShow.PersonCount == 0 && ucShow.PatientInfo == null)
            {
                this.itemInputControl.Clear();
                MessageBox.Show(Language.Msg("�û���û�йҺ���Ϣ!"));

                return false;
            }
            if (ucShow.PersonCount > 1 || (ucShow.PersonCount == 1 && ucShow.IsCanReRegister))
            {
                fPopWin.Show();
                fPopWin.Hide();
                fPopWin.Location = ((Control)this.registerControl).PointToScreen(new Point(cardLocation.X, cardLocation.Y + cardHeight));
                fPopWin.ShowDialog();
            }
            if (this.registerControl.PatientInfo == null)
            {
                return false;
            }

            this.registerControl.IsCanModifyChargeInfo = this.itemInputControl.IsCanModifyCharge;
            //FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            //if (Function.IsContainYKDept(employee.Dept.ID))
            //{
            //    if (string.IsNullOrEmpty(this.registerControl.PatientInfo.SeeDoct.ID))
            //    {
            //        //�ж�Ȩ��,�Ƿ���ҽ��δ����Ҳ�����շѵ�Ȩ��
            //        if (!FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivFeeWhenNoSeeDoc))
            //        {
            //            FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("��û��ҽ��δ����Ҳ�����շѵ�Ȩ�ޣ�������ȡ����", MessageBoxIcon.Warning);
            //            this.Clear();
            //            return false;
            //        }

            //        DialogResult dResult = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("����δ��ҽ������Ƿ�����շѣ�", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //        if (dResult != DialogResult.Yes)
            //        {
            //            this.Clear();
            //            return false;
            //        }
            //    }
            //}
            this.itemInputControl.PatientInfo = this.itemInputControl.PatientInfo;

            return true;
        }

        /// <summary>
        /// ������Ϣ¼��ؼ��Ŀ���ҽ�������仯�󴥷�
        /// </summary>
        /// <param name="recipeSeq">��ǰ�շ�����</param>
        /// <param name="deptCode">ҽ�����ڿ��Ҵ���</param>
        /// <param name="changeObj">�仯��ҽ��ID������</param>
        protected virtual void registerControl_SeeDoctChanged(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject changeObj)
        {
            this.itemInputControl.RefreshSeeDoc(recipeSeq, deptCode, changeObj);
            this.rightControl.SetInfomation(this.registerControl.PatientInfo, null, null, null, "1");
        }

        /// <summary>
        /// ������Ϣ¼��ؼ��Ŀ�����ҷ����仯�󴥷�
        /// </summary>
        /// <param name="recipeSeq">��ǰ�շ�����</param>
        /// <param name="deptCode">ҽ�����ڿ��Ҵ���</param>
        /// <param name="changeObj">�仯�Ŀ���ID������</param>
        protected virtual void registerControl_SeeDeptChanaged(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject changeObj)
        {
            this.itemInputControl.RefreshSeeDept(recipeSeq, changeObj);
        }

        /// <summary>
        /// ɾ���շ����е�ʱ�򴥷�
        /// </summary>
        /// <param name="al">ɾ�������а�������Ŀ</param>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        protected virtual int registerControl_RecipeSeqDeleted(System.Collections.ArrayList al)
        {
            int iReturn = 0;
            foreach (FeeItemList f in al)
            {
                iReturn = this.itemInputControl.DeleteRow(f);
                if (iReturn == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// �շ����б仯�󴥷�
        /// </summary>
        protected virtual void registerControl_RecipeSeqChanged()
        {
            this.itemInputControl.Clear();
            this.itemInputControl.PatientInfo = this.registerControl.PatientInfo;

            this.rightControl.SetInfomation(this.registerControl.PatientInfo, null, null, null, "4");

            this.itemInputControl.ChargeInfoList = this.registerControl.FeeDetailsSelected;
            this.itemInputControl.RecipeSequence = this.registerControl.RecipeSequence;
            this.itemInputControl.IsCanAddItem = this.registerControl.IsCanAddItem;

            this.registerControl_SeeDoctChanged(this.registerControl.RecipeSequence, this.registerControl.PatientInfo.DoctorInfo.Templet.Doct.User01.Clone().ToString(), this.registerControl.PatientInfo.DoctorInfo.Templet.Doct.Clone());

            //this.registerControl_SeeDoctChanged(this.registerControl.RecipeSequence,
        }

        /// <summary>
        /// �۸�������仯�󴥷�,��������,������
        /// </summary>
        protected virtual void registerControl_PriceRuleChanaged()
        {
            this.itemInputControl.ModifyPrice();
        }

        /// <summary>
        /// ��ͬ��λ�仯�󴥷�
        /// </summary>
        protected virtual void registerControl_PactChanged()
        {
            this.itemInputControl.PatientInfo = this.registerControl.PatientInfo;
            this.itemInputControl.RefreshItemForPact();
            this.itemInputControl.SetFocus();
            // ����patientinfo��sex��user01��ʾ���Ժ�����  xingz
            this.rightControl.SetInfomation(this.registerControl.PatientInfo, null,null, null,"2");
        }


        /// <summary>
        /// ����¼��ؼ������л��󴥷�
        /// </summary>
        protected virtual void registerControl_ChangeFocus()
        {
            ((Control)this.itemInputControl).Focus();
            this.itemInputControl.SetFocus();
            this.itemInputControl.IsFocus = true;

        }

        /// <summary>
        /// ��ʾ��һ������Ϣ
        /// </summary>
        /// <returns></returns>
        protected virtual void DisplayPreRegInfo()
        {
            if (this.registerControl == null || this.itemInputControl == null)
            {
                return;
            }

            if (this.registerControl.PrePatientInfo != null)
            {
                this.registerControl.Clear();
                this.itemInputControl.Clear();
                this.registerControl.PatientInfo = this.registerControl.PrePatientInfo.Clone();
                if (this.registerControl.PatientInfo.ID != null && this.registerControl.PatientInfo.ID != "")
                {
                    this.registerControl.AddNewRecipe();
                }

            }
        }

        /// <summary>
        /// ��ʾ������
        /// </summary>
        /// <returns></returns>
        protected virtual int DisplayCalc()
        {
            string tempValue = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.CALCTYPE, false, "0");

            if (tempValue == "0")
            {
                System.Diagnostics.Process.Start("CALC.EXE");
            }
            else if (tempValue == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(
                    new FS.HISFC.Components.Common.Controls.ucCalc());
            }
            else
            {
                System.Diagnostics.Process.Start("CALC.EXE");
            }

            return 1;
        }

        /// <summary>
        /// �л�����
        /// </summary>
        public void ChangeFocus()
        {
            if (this.itemInputControl.IsFocus)
            {
                ((Control)this.registerControl).Focus();
            }
            else
            {
                this.itemInputControl.SetFocus();
            }
        }

        /// <summary>
        /// ������ݼ�XML
        /// </summary>
        /// <param name="hashCode">��ǰ������HashCode</param>
        /// <returns>�ɹ���ǰֵ,ʧ�� string.Empty</returns>
        public string Operation(string hashCode)
        {
            XmlDocument doc = new XmlDocument();
            if (filePath == "") return "";
            try
            {
                StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
            }
            catch
            {
                return "";
            }
            XmlNodeList nodes = doc.SelectNodes("//Column");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["hash"].Value == hashCode)
                {
                    return node.Attributes["opCode"].Value;
                }
            }

            return "";
        }

        /// <summary>
        /// ִ�п�ݼ�
        /// </summary>
        /// <param name="key">��ǰ����</param>
        public bool ExecuteShotCut(Keys key)
        {
            int iReturn = -1;

            string code = Operation(key.GetHashCode().ToString());

            if (code == "") return false;

            switch (code)
            {
                case "1":
                    iReturn = this.SaveFee();

                    if (iReturn == -1)
                    {

                        return true;
                    }
                    if (this.isFee)
                    {
                        //MessageBox.Show(Language.Msg("�շѳɹ�!"));
                        this.Focus();
                        this.Clear();
                        ((Control)this.registerControl).Focus();
                        this.isFee = false;
                    };
                    this.Refresh();//�շѺ�ˢ�»�����
                    break;
                case "2":
                    if (this.isCanSaveCharge)
                    {
                        iReturn = this.SaveCharge();

                        if (iReturn == -1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("û�л���Ȩ��"));
                    }
                    break;
                case "3":

                    if (this.itemInputControl == null)
                    {
                        return true;
                    }

                    this.itemInputControl.AddNewRow();

                    break;
                case "4"://ɾ��

                    if (this.itemInputControl == null)
                    {
                        return true;
                    }

                    this.itemInputControl.DeleteRow();

                    break;
                case "5"://���
                    this.Clear();

                    break;
                case "6"://����
                    break;
                case "7"://�˳�
                    //this.FindForm().Close();
                    break;
                case "8"://������
                    this.DisplayCalc();

                    break;
                case "9"://�����޸ı���
                    //this.ucChargeFee1.ModifyRate();
               
                    break;
                case "10"://�ݴ�
                    this.ChangeRecipe();

                    break;
                case "11"://��ʷ��Ʊ��ѯ
                    //frmPre = new frmPreCountInvos();
                    //frmPre.Show();
                    //this.Focus();
                    break;
                case "12"://����������Ϣ
                    //this.ucChargeFee1.DisplayPubFeeBills();
                    break;
                case "13"://��һ�շѻ���
                    this.DisplayPreRegInfo();

                    break;
                case "14"://С��
                    this.itemInputControl.SumLittleCost();

                    break;
                case "15"://�޸Ĳ�ҩ����
                    this.itemInputControl.ModifyDays();

                    break;
                case "16":
                    this.ChangeFocus();

                    break;
                case "17":
                    //this.ucChargeFee1.DisplayPatientFeeList();
                    break;
                case "18":
                    //frmQuitFee frmQuitFee = new frmQuitFee();
                    //frmQuitFee.Show();
                    break;
                case "19":
                    //this.ucChargeFee1.ChangeQueryType();
                    break;
                case "20":
                    // this.ucChargeFee1.ucChargeDisplay1.ucInvoicePreview1.SetFocusToInvo();
                    break;
            }

            return true;

        }

        /// <summary>
        /// ����ˢ��ToolBar
        /// </summary>
        public void RefreshToolBar()
        {
            XmlDocument doc = new XmlDocument();
            if (filePath == "")
            {
                return;
            }
            try
            {
                StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
            }
            catch
            {
                return;
            }
            XmlNodeList nodes = doc.SelectNodes("//Column");
            foreach (XmlNode node in nodes)
            {
                string opKey = node.Attributes["opKey"].Value;
                string cuKey = node.Attributes["cuKey"].Value;
                if (opKey != "")
                {
                    opKey = "Ctrl+";
                }
                if (cuKey == "")
                {
                    cuKey = "";
                }
                else
                {
                    cuKey = "(" + opKey + cuKey + ")";
                }

                ToolStripButton tempButton = new ToolStripButton();

                switch (node.Attributes["opCode"].Value)
                {
                    case "1"://�շ�
                        tempButton = this.toolBarService.GetToolButton("ȷ���շ�");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "ȷ���շ�" + cuKey;

                        this.hsToolBar.Add(tempButton.Text, "ȷ���շ�");

                        break;
                    case "2"://���۱���
                        tempButton = this.toolBarService.GetToolButton("���۱���");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "���۱���" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "���۱���");

                        break;
                    case "10"://�ݴ�
                        tempButton = this.toolBarService.GetToolButton("�ݴ�");
                        if (tempButton == null)
                        {
                            return;
                        }

                        tempButton.Text = "�ݴ�" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "�ݴ�");

                        break;
                    case "3"://����
                        tempButton = this.toolBarService.GetToolButton("����");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "����" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "����");

                        break;
                    case "4"://ɾ��
                        tempButton = this.toolBarService.GetToolButton("ɾ��");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "ɾ��" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "ɾ��");

                        break;
                    case "5"://���
                        tempButton = this.toolBarService.GetToolButton("����");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "����" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "����");

                        break;
                    case "6"://����
                        tempButton = this.toolBarService.GetToolButton("����");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "����" + cuKey;

                        this.hsToolBar.Add(tempButton.Text, "����");

                        break;
                    case "7"://�˳�
                        tempButton = this.toolBarService.GetToolButton("�˳�");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "�˳�" + cuKey;

                        this.hsToolBar.Add(tempButton.Text, "�˳�");

                        break;
                    case "9"://�����޸ı���
                        tempButton = this.toolBarService.GetToolButton("�����޸ı���");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "�����޸ı���" + cuKey;

                        this.hsToolBar.Add(tempButton.Text, "�����޸ı���");

                        break;
                    case "12"://���Ѽ��˵���Ϣ
                        tempButton = this.toolBarService.GetToolButton("���Ѽ��˵���Ϣ");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "���Ѽ��˵���Ϣ" + cuKey;

                        this.hsToolBar.Add(tempButton.Text, "���Ѽ��˵���Ϣ");

                        break;
                }
            }
        }

        /// <summary>
        /// �����˺ŷ�Ʊ�ۼ�
        /// </summary>
        protected void AccountInvoiceCount()
        {
            FS.HISFC.Components.OutpatientFee.Forms.frmCountAccountInvoices frmAccount = new FS.HISFC.Components.OutpatientFee.Forms.frmCountAccountInvoices();
            frmAccount.ShowDialog();
        }

        /// <summary>
        /// ��ѯ�������﷢Ʊ��Ϣ
        /// </summary>
        protected void OutPatientInvoiceInfo()
        {
            if (this.registerControl.PatientInfo == null || string.IsNullOrEmpty(this.registerControl.PatientInfo.PID.CardNO))
            {
                MessageBox.Show(Language.Msg("û�л�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return;
            }

            FS.HISFC.Components.OutpatientFee.Forms.frmShowOutPatientInvoiceInfo frmShowInvoice = new FS.HISFC.Components.OutpatientFee.Forms.frmShowOutPatientInvoiceInfo();

            frmShowInvoice.RegInfo = this.registerControl.PatientInfo;
            frmShowInvoice.IsAccount = this.isAccountPayOnly;

            frmShowInvoice.ShowDialog();

        }

        /// <summary>
        /// ��ʼ�ۼ�
        /// </summary>
        protected virtual void BeginAddUpCost()
        {
            /*
            this.registerControl.IsBeginAddUpCost = true;
            toolBarService.SetToolButtonEnabled("��ʼ�ۼ�", false);
            toolBarService.SetToolButtonEnabled("ȡ���ۼ�", true);
            toolBarService.SetToolButtonEnabled("�����ۼ�", true);
             * */

            FS.UFC.OutpatientFee.Forms.frmPreCountInvos frm = new FS.UFC.OutpatientFee.Forms.frmPreCountInvos();

            frm.ShowDialog();
        }

        /// <summary>
        /// ȡ���ۼ�
        /// </summary>
        protected virtual void CancelAddUpCost()
        {
            this.registerControl.IsBeginAddUpCost = false;

            toolBarService.SetToolButtonEnabled("��ʼ�ۼ�", true);
            toolBarService.SetToolButtonEnabled("ȡ���ۼ�", false);
            toolBarService.SetToolButtonEnabled("�����ۼ�", false);
        }

        /// <summary>
        /// ȡ���ۼ�
        /// </summary>
        protected virtual void EndAddUpCost()
        {
            MessageBox.Show(this.registerControl.AddUpCost.ToString());
            this.registerControl.IsBeginAddUpCost = false;
            toolBarService.SetToolButtonEnabled("��ʼ�ۼ�", true);
            toolBarService.SetToolButtonEnabled("ȡ���ۼ�", false);
            toolBarService.SetToolButtonEnabled("�����ۼ�", false);
        }
        #endregion

        #region �¼�

        /// <summary>
        /// �򿪻��߶�ιҺ�UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void fPopWin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.fPopWin.Close();
            }
        }

        /// <summary>
        /// �����ؼ�Init�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("ȷ���շ�", "ȷ���շ���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȷ���շ�, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��¼��ķ�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("���۱���", "���滮����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H���۱���, true, false, null);
            toolBarService.AddToolButton("�ݴ�", "��ʱ�����շ���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z�ݴ�, true, false, null);
            toolBarService.AddToolButton("����", "����һ���շ���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť
            toolBarService.AddToolButton("ˢ����Ŀ", "ˢ����Ŀ�б�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("ˢ�»���", "ˢ�»����б�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ���
            toolBarService.AddToolButton("��Ʊ�ۼ�", "��Ʊ�ۼ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L�ۼƿ�ʼ, true, false, null);
            toolBarService.AddToolButton("������", "������", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L�ۼƿ�ʼ, true, false, null);
            toolBarService.AddToolButton("�����޸ı���", "�����޸ı���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolBarService.AddToolButton("�����˺ŷ�Ʊ�ۼ�", "�����˺ŷ�Ʊ�ۼ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L�ۼƿ�ʼ, true, false, null);
            toolBarService.AddToolButton("���߷�Ʊ��Ϣ", "�������﷢Ʊ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);
            toolBarService.AddToolButton("���Ѽ��˵���Ϣ", "���Ѽ��˵���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);
            toolBarService.AddToolButton("��ʷ����", "����������ʷ�շ���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "��Ʊ�ۼ�":
                    {
                        this.BeginAddUpCost();
                        break;
                    }
                case "ȡ���ۼ�":
                    {
                        this.CancelAddUpCost();
                        break;
                    }
                case "�����ۼ�":
                    {
                        this.EndAddUpCost();
                        break;
                    }
            }

            switch (e.ClickedItem.Text)
            {
                case "ȷ���շ�":
                    this.SaveFee();
                    this.Refresh();//�շѺ�ˢ�»�����
                    break;
                case "���۱���":
                    if (this.isCanSaveCharge)
                    {
                        this.SaveCharge();
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("û�л���Ȩ��"));
                    }
                    break;
                case "����":
                    this.Clear();
                    break;
                case "ɾ��":
                    this.itemInputControl.DeleteRow();
                    break;
                case "����":
                    this.itemInputControl.AddNewRow();
                    break;
                case "�ݴ�":
                    this.ChangeRecipe();
                    break;
                case "ˢ����Ŀ":
                    //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť
                    this.RefreshItem();
                    //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť ���
                    break;
                case "ˢ�»���":
                    //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť
                    this.Refresh();
                    //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť ���
                    break;
                case "������":
                    System.Diagnostics.Process.Start("calc.exe");
                    break;
                case "�����޸ı���":
                    this.ModifyItemRate();
                    break;
                case "�����˺ŷ�Ʊ�ۼ�":
                    this.AccountInvoiceCount();
                    break; 
                case "���߷�Ʊ��Ϣ":
                    this.OutPatientInvoiceInfo();
                    break;
                case "���Ѽ��˵���Ϣ":
                    this.DisplayPubFeeBills();
                    break;
                case "��ʷ����":
                    this.PreCountInvos();
                    break;                     
             }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveFee();
            this.Refresh();//�շѺ�ˢ�»�����
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// �򿪴���֮ǰִ�е��¼�
        /// </summary>
        protected virtual void OnLoad()
        {

        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject is Register)
            {
                this.ucShow_SelectedPatient(neuObject as Register);
            }

            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// �򿪴��ڳ�ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ucCharge_Load(object sender, EventArgs e)
        {

            if (this.DesignMode)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�������,���Ժ�...");

            Application.DoEvents();

            //RefreshToolBar();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);

            this.OnLoad();

            if (this.Init() == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                return;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳�����
                //�رմ���ǰ,���������Ŀ�б��̻߳�û�н���,ǿ�н���,��������
                (this.itemInputControl as ucDisplay).threadItemInit.Abort();
            }
            catch { }

        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintGuide(this.registerControl.PatientInfo, null, this.itemInputControl.GetFeeItemList());
            return base.OnPrint(sender, neuObject);
        }
        #endregion

        #region ISIReadCard ��Ա

        /// <summary>
        /// ͨ��toolBar�Ķ��������ӿ�
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        public int ReadCard(string pactCode)
        {
            long returnValue = 0;

            returnValue = this.medcareInterfaceProxy.SetPactCode(pactCode);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;
            if (returnValue != 1)
            {
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            if (this.registerControl.PatientInfo == null)
            {
                this.registerControl.PatientInfo = new FS.HISFC.Models.Registration.Register();
            }

            returnValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(this.registerControl.PatientInfo);
            if (returnValue != 1)
            {
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            this.registerControl.SetRegInfo();

            return 1;
        }

        /// <summary>
        /// ���ý��滼�߻�����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        public int SetSIPatientInfo()
        {
            this.registerControl.SetRegInfo();

            return 1;
        }

        #endregion

        #region IInterfaceContainer ��Ա

        /// <summary>
        /// �����ӿ�����{DA12F709-B696-4eb9-AD3B-6C9DB7D780CF}
        /// </summary>
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[3];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IFeeExtendOutpatient);
                type[1] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen);
                type[2] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans);
                return type;
            }
        }

        #endregion

        #region IPreArrange ��Ա

        public int PreArrange()
        {

            if (this.promptingDayBalanceDays > 0)
            {
                //FS.FrameWork.
                DateTime dt = DateTime.MinValue;
                string dtString = string.Empty;
                if (outpatientManager.GetLastBalanceDate(outpatientManager.Operator, ref dtString) < 0)
                {
                    MessageBox.Show(Language.Msg("ȡ�ϴ��ս�ʱ�����!") + outpatientManager.Err);

                    return -1;
                }
                dt = NConvert.ToDateTime(dtString);
                bool hasFee = true;
                ArrayList al = this.outpatientManager.QueryBalancesByCount(this.outpatientManager.Operator.ID, 1);
                if (al == null || al.Count == 0)
                {
                    hasFee = false;
                }
                else
                {
                    Balance balance = al[0] as Balance;
                    if (DateTime.Compare(balance.BalanceOper.OperTime, dt) > 0)
                    {
                        hasFee = true;
                    }
                    else
                    {
                        hasFee = false;
                    }
                }
                if (hasFee && dt != DateTime.MinValue)
                {
                    if (DateTime.Compare(NConvert.ToDateTime(outpatientManager.GetSysDateTime()), dt.AddDays(this.promptingDayBalanceDays)) > 0)
                    {
                        MessageBox.Show(Language.Msg("���ϴ��ս�ʱ�䳬��" + this.promptingDayBalanceDays + "�죬���ս�����շѣ�"));

                        return -1;
                    }
                }
            }
            return 1;
        }

        #endregion

        #region ��ӡ����ָ����
        /// <summary>
        /// ��ӡ����ָ����
        /// </summary>
        /// <param name="rInfo"></param>
        /// <param name="invoices"></param>
        /// <param name="feeDetails"></param>
        private void PrintGuide(Register rInfo, ArrayList invoices, ArrayList feeDetails)
        {
            IOutpatientGuide print = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IOutpatientGuide)) as IOutpatientGuide;
            if (print != null)
            {
                if (!this.IsChargePrint)//�շѴ�ӡ���ִ���
                {
                    print.SetValue(rInfo, invoices, feeDetails);
                    print.Print();
                }
                else//���۴�ӡ���������ֱ��ӡ
                {
                    ArrayList alList = new ArrayList();
                    ArrayList tempList = new ArrayList();
                    for (int m = 0; m < feeDetails.Count; m++)
                    {
                        tempList.Add((feeDetails[m]));
                    }
                    //���鴦��
                    while (tempList.Count > 0)
                    {
                        ArrayList sameNotes = new ArrayList();
                        FeeItemList compareItem = tempList[0] as FeeItemList;
                        foreach (FeeItemList f in tempList)
                        {
                            if (f.RecipeNO == compareItem.RecipeNO)
                            {
                                sameNotes.Add(f.Clone());
                            }
                        }
                        alList.Add(sameNotes);
                        foreach (FeeItemList f in sameNotes)
                        {
                            for (int n = tempList.Count - 1; n >= 0;n--)
                            {
                                FeeItemList b = tempList[n] as FeeItemList;
                                if (f.RecipeNO == b.RecipeNO)
                                {
                                    tempList.Remove(b);
                                }
                            }
                        }
                    }
                    //�������ֱ��ӡ
                    for (int i = 0; i < alList.Count; i++)
                    {
                        ArrayList a = new ArrayList();
                        a.Clear();
                        if (((ArrayList)alList[i]).Count > 1)
                        {
                            for (int j = 0; j < ((ArrayList)alList[i]).Count; j++)
                            {
                                a.Add(((ArrayList)alList[i])[j]);
                            }
                        }
                        else
                        {
                            a.Add(((ArrayList)alList[i])[0]);
                        }
                        print.SetValue(rInfo, invoices, a);
                        print.Print();
                    }
                }
            }
        }
        
        #endregion

        #region ��������ĺ�ͬ��λ�޸ı���
        /// <summary>
        /// ��������ĺ�ͬ��λ�޸ı���
        /// </summary>
        private int ModifyItemRate()
        {
            if (this.registerControl.PatientInfo == null)
            {
                return -1;
            }
            if (registerControl.PatientInfo != null && (registerControl.PatientInfo.Pact.PayKind.ID == "03"))
            {
                this.Focus();
                ArrayList alFee = this.itemInputControl.GetFeeItemListForCharge(true);
                ucModifyItemRate modifyRate = new ucModifyItemRate();
                //modifyRate.Relations = this.relations;
                modifyRate.FeeDetails = alFee;
                modifyRate.Register = this.registerControl.PatientInfo;
                modifyRate.InitFeeDetails();
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�޸ı���";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(modifyRate);
                if (modifyRate.IsConfirm)
                {
                    this.itemInputControl.RefreshNewRate(modifyRate.FeeDetails);
                }
            }
            else if (registerControl.PatientInfo != null && (registerControl.PatientInfo.Pact.PayKind.ID == "02"))
            {
                this.Focus();
                ArrayList alFee = this.itemInputControl.GetFeeItemListForCharge(true);
                ucApproveItem modifyRate = new ucApproveItem();
                modifyRate.FeeDetails = alFee;
                modifyRate.Register = this.registerControl.PatientInfo;
                modifyRate.InitFeeDetails();
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ҽҽ������";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(modifyRate);
                this.itemInputControl.RefreshNewRate(modifyRate.FeeDetails);
            }
            return 0;


        }

       #endregion

        /// <summary>
        /// ��ʾ�������յ���Ϣ
        /// </summary>
        /// <returns>-1ʧ�� 0 �ɹ�</returns>
        public int DisplayPubFeeBills()
        {
            try
            {
                if (this.registerControl.PatientInfo == null)
                {
                    return -1;
                }
                if (registerControl.PatientInfo != null && registerControl.PatientInfo.Pact.PayKind.ID == "03")
                {
                    ////ArrayList alFee = this.itemInputControl.GetFeeItemList();
                    ////string errText = "";
                    ////if (Clinic.Charge.Funciton.ComputePubFee(this.ucChargeDisplay1.PubFeeInstance, this.ucRegInfo1.RInfo, ref alFee, this.relations, ref errText) == -1)
                    ////{
                    ////    MessageBox.Show(errText);
                    ////    return -1;
                    ////}
                    ////string invoiceNo = "", realInvoiceNo = "";

                    ////int iReturnValue = Charge.Funciton.GetInvoiceNO(myCtrl, ref invoiceNo, ref realInvoiceNo, null, ref errText);
                    ////if (iReturnValue == -1)
                    ////{
                    ////    MessageBox.Show(errText);
                    ////    return -1;
                    ////}//this.medcareInterfaceProxy.PreBalanceOutpatient
                    ////ArrayList invoiceAndDetails = Clinic.Charge.Funciton.MakeInvoice(this.ucRegInfo1.RInfo,
                    ////    alFee, invoiceNo, realInvoiceNo, ref errText);
                    ArrayList alFee = this.itemInputControl.GetFeeItemList();
                    if (alFee == null)
                    {
                        MessageBox.Show(this.itemInputControl.ErrText);
                        ((Control)this.registerControl).Focus();

                        return -1;
                    }
                    if (alFee.Count <= 0)
                    {
                        MessageBox.Show(Language.Msg("û�з�����Ϣ!"));
                        ((Control)this.registerControl).Focus();

                        return -1;
                    }
                    //���ô����ĺ�ͬ��λ����
                    this.medcareInterfaceProxy.SetPactCode(this.registerControl.PatientInfo.Pact.ID);
                    int returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.registerControl.PatientInfo, ref alFee);
                    if (returnValue == -1)
                    {
                        return -1;
                    }
                    FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(this.outpatientManager.Operator.ID);
                    if (employee == null)
                    {
                        MessageBox.Show("��ȡ��Ա��Ϣʧ�ܣ�" + managerIntegrate.Err);
                        return -1;
                    }

                    #region ��ȡ��Ʊ��
                    string invoiceNO = string.Empty, realInvoiceNO = string.Empty;
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    string errText = string.Empty;
                    //��ñ����շ���ʼ��Ʊ��
                    int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee, "C", ref invoiceNO, ref realInvoiceNO, ref errText);
                    if (iReturnValue == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.RollBack();
                    #endregion
                    ArrayList invoiceAndDetails = FS.HISFC.Components.OutpatientFee.Class.Function.MakeInvoice(this.feeIntegrate, this.registerControl.PatientInfo, alFee, invoiceNO, realInvoiceNO, ref errText);

                    FS.HISFC.Components.OutpatientFee.InvoicePrint.ucInvoicePreviewGFAll ucPreview = new FS.HISFC.Components.OutpatientFee.InvoicePrint.ucInvoicePreviewGFAll();
                    ucPreview.InvoiceAndInvoiceDetails = invoiceAndDetails;
                    ucPreview.Init();
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPreview);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            return 0;
        }

        private void PreCountInvos()
        {
            if (this.registerControl.PatientInfo == null || this.registerControl.PatientInfo.PID.CardNO == null || this.registerControl.PatientInfo.PID.CardNO == string.Empty)
            {
                MessageBox.Show(Language.Msg("û�л�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return;
            }
            this.itemInputControl.PreCountInvos();
        }

        /// <summary>
        /// �жϿ���Ƿ���
        /// </summary>
        /// <returns></returns>
        private bool IsStoreEnough(FeeItemList feeItem, string row)
        {
            //begin�����жϿ����� zhouxs by 2007-10-17
            decimal storeSum = 0;
            decimal storeSumTemp = 0;
            int iReturn = this.pharmacyIntegrate.GetStorageNum(feeItem.ExecOper.Dept.ID, feeItem.Item.ID, out storeSum);
            if (iReturn <= 0)
            {
                MessageBox.Show("���ҿ��ʧ��!");
                return false;
            }
            for (int i = 0; i < this.comFeeItemLists.Count; i++)
            {
                FeeItemList feeItem1 = this.comFeeItemLists[i] as FeeItemList;
                if (feeItem1 != null)
                {

                    if (feeItem1.Item.ID == feeItem.Item.ID && feeItem1.ExecOper.Dept.ID == feeItem.ExecOper.Dept.ID)
                    {
                        storeSumTemp = storeSumTemp + feeItem1.Item.Qty;
                    }
                }
            }
   
            if (storeSum <= 0 || storeSum - storeSumTemp < 0)
            {
                if (feeItem.FeePack == "1")
                {
                    int outTemp = 0;
                    int outTemp1 = 0;

                    int store = Math.DivRem(NConvert.ToInt32(storeSum), NConvert.ToInt32(feeItem.Item.PackQty), out outTemp);
                    int storeTemp = Math.DivRem(NConvert.ToInt32(storeSumTemp), NConvert.ToInt32(feeItem.Item.PackQty), out outTemp1);
                    MessageBox.Show("��"+feeItem.Item.Name+"��"+"��ǰ�����:" + store.ToString() +
                        (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit + outTemp.ToString() + (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit +
                        "|��������:" + storeTemp.ToString() + (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit + outTemp1.ToString() + (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit + "   ��治��!����ϵҩ����");
                }
                else
                {
                    MessageBox.Show("��" + feeItem.Item.Name + "��" + "��ǰ�����:" + storeSum.ToString() + (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit + "|��������:"
                        + storeSumTemp.ToString() + (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit + "   ��治��!����ϵҩ����");
                }
                //////this.fpSpread1_Sheet1.SetActiveCell(row, (int)Columns.Amount, true);

                return false;
            }
            if (feeItem.User01 == "1")
            {
                MessageBox.Show("����Ŀ�Ѿ�ȱҩ,����ѡ��!");
                return false;
            }
            return true;
            //end zhouxs
        }
    }
}
