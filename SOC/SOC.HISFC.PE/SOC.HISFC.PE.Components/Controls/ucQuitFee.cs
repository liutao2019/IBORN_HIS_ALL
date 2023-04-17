using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Base;
using System.Collections.Generic;
using FS.FrameWork.Function;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.BizProcess.Interface.Fee;
using FS.HISFC.Models.Registration;
using FS.HISFC.Components.OutpatientFee.Controls;
using FS.HISFC.Components.OutpatientFee;
using FSSOC.HISFC.PE.Components.Service;

namespace FS.SOC.HISFC.PE.Components.Controls
{
    /// <summary>
    /// ucQuitFee<br></br>
    /// [��������: �����˷�������UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-2-28]<br></br> 
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucQuitFee : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer,FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucQuitFee()
        {
            try
            {
                InitializeComponent();
            }
            catch { }
        }

        #region ����

        /// <summary>
        /// �������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���Һ�ҵ���
        /// </summary>
        protected FS.SOC.HISFC.PE.BizLogic.Register registerIntegrate = new FS.SOC.HISFC.PE.BizLogic.Register();

        /// <summary>
        /// �����ۺ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ������ҵ���
        /// </summary>
        protected FS.SOC.HISFC.PE.BizLogic.Fee peFeeIntegrate = new FS.SOC.HISFC.PE.BizLogic.Fee();

        /// <summary>
        /// ҽ���ۺ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// �˷�����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.ReturnApply returnApplyManager = new FS.HISFC.BizLogic.Fee.ReturnApply();

        /// <summary>
        /// ��ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// �����Ŀҵ���
        /// </summary>
        //protected FS.HISFC.BizProcess.Fee.UndrugComb undrugCombManager = new FS.HISFC.BizProcess.Fee.UndrugComb();

        /// <summary>
        /// �����Ŀҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        protected FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();

        /// <summary>
        /// ������Ŀ
        /// </summary>
        protected FS.HISFC.Models.Fee.Item.Undrug undrugComb = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// ��ǰ�˷ѵĸ�����Ŀ
        /// </summary>
        protected FS.HISFC.Models.Fee.Item.Undrug currentUndrugComb = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// ����������
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// Ҫ�˷ѵķ�Ʊ����
        /// </summary>
        protected ArrayList quitInvoices = new ArrayList();

        /// <summary>
        /// ��Ʊ������Ϣ
        /// </summary>
        protected ArrayList invoiceFeeItemLists = new ArrayList();

        /// <summary>
        /// ��ǰ������Ŀ��ϸ
        /// </summary>
        protected ArrayList currentUndrugCombs = new ArrayList();

        /// <summary>
        /// ���շ���Ϣ
        /// </summary>
        protected ArrayList againFeeItemLists = new ArrayList();

        /// <summary>
        /// �˷�ǰ�������շ���Ϣ
        /// </summary>
        protected ArrayList oldFeeItemLists = new ArrayList();
        /// <summary>
        /// �˷��뻧�ķ�Ʊ��Ϣ
        /// </summary>
        protected string CancleInvoiceNo = string.Empty;
        /// <summary>
        /// ����ʱ���´�ӡ�ķ�Ʊ��Ϣ
        /// </summary>
        protected string FeeInvoiceNo = string.Empty;
        ///// <summary>
        ///// ȫ�����ݿ�����
        ///// </summary>
        //protected FS.FrameWork.Management.Transaction t = null;

        /// <summary>
        /// �˷ѵ����
        /// </summary>
        protected string backType = string.Empty;

        /// <summary>
        /// ������Ŀ�Ƿ�ȫ�� 1�� 0 ����
        /// </summary>
        protected bool isNeedAllQuit = false;

        /// <summary>
        /// �Һ���Ϣʵ��
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patient = new FS.HISFC.Models.Registration.Register();

        protected FS.HISFC.Models.Registration.Register oldPatient = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ���۹��ķ�Ʊ
        /// </summary>
        protected Hashtable hsInvoice = new Hashtable();

        /// <summary>
        /// �޸ĺ��֧����ʽ
        /// </summary>
        protected ArrayList modifiedBalancePays = new ArrayList();

        /// <summary>
        /// ������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �Ƿ����Żݽ��
        /// </summary>
        protected bool isHaveRebateCost = false;

        protected string InvoiceNoStr = string.Empty;
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        /// <summary>
        /// �����շ�
        /// </summary>
        //{143CA424-7AF9-493a-8601-2F7B1D635027}
        protected FS.HISFC.BizProcess.Integrate.Material.Material mateIntegrate = new FS.HISFC.BizProcess.Integrate.Material.Material();

        /// <summary>
        /// �Ƿ���ʾ�շѽ���
        /// </summary>
        protected bool isQuitFee = true;

        /// <summary>
        /// �Ƿ��˻��˷�
        /// </summary>
        protected bool isAccount = false;

        /// <summary>
        /// �Ƿ��շѳɹ� add by yerl
        /// </summary>
        private bool isSuccess = false;
        /// <summary>
       /// ��Ҫ�˷ѵ���Ϣ��ÿ�α���ǰ���
        /// </summary>
        private ArrayList alQuitFeeItemList = new ArrayList();

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
        /// �����ӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans iBankTrans = null;
        /// <summary>
        /// �շѵ����ؼ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee popFeeControl = null;

        /// <summary>
        /// �Ҳ���Ϣ��ʾ�ؼ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight rightControl = null;
        /// <summary>
        /// �����ӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans bankTrans = null;
        #endregion

        /// <summary>
        /// �Ƿ�������ҩ���
        /// </summary>
        protected bool isQuitDrugConfirm = false;

        /// <summary>
        /// ����ȡ���ӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff iOutPatientFeeRoundOff = null;

        /// <summary>
        /// ��������ѷ��ñ���
        /// </summary>
        protected  string roundFeeItemCode = "F00000053238";

        /// <summary>
        /// �Է�������Ŀ����
        /// </summary>
        protected string ownDiagFeeCode = string.Empty;

        #endregion

        #region ����
        //�˷�ʱ�Ƿ�������
        protected bool isQuitFeeAndOperOrder = false;
        [Category("�ؼ�����"), Description("�˷�ʱ�Ƿ�������������� false:������ true:����")]
        public bool IsQuitFeeAndOperOrder
        {
            get
            {
                return this.isQuitFeeAndOperOrder;
            }
            set
            {
                this.isQuitFeeAndOperOrder = value;
            }
        
        }

        //�Ƿ񱣴�ҽ����Ҫ֧����ҽ���Ը���������Ĭ�ϲ�����
        protected bool isSavePYfee = false;
        [Category("�ؼ�����"), Description("�Ƿ񱣴�ҽ����Ҫ֧����ҽ���Ը���������Ĭ�ϲ����� false:������ true:����")]
        public bool IsSavePYfee
        {
            get
            {
                return this.isSavePYfee;
            }
            set
            {
                this.isSavePYfee = value;
            }

        }


        //�Ƿ񱣴�ҽ����Ҫ֧����ҽ���Ը���������Ĭ�ϲ�����
        protected bool isATM = false;
        [Category("�ؼ�����"), Description("�Ƿ�atm�ɷ��˷�")]
        public bool IsAtm
        {
            get
            {
                return this.isATM;
            }
            set
            {
                this.isATM = value;
            }

        }




        protected bool isPharmacySameRecipeQuitAll = false;
        /// <summary>
        /// ������� 
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�����ҩƷͬһ������ҩƷ����ȫ�� false:������ true:����")]
        public bool IsPharmacySameRecipeQuitAll
        {
            set
            {
                this.isPharmacySameRecipeQuitAll = value;

            }
            get
            {
                return this.isPharmacySameRecipeQuitAll;
            }
        }
        protected bool isUnCAQuitAll = false;
        /// <summary>
        /// �������IsUnCAQuitAll 
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�����ԭʼ��Ʊ�а���������֧Ʊ���κ�һ��֧����ʽʱ���������˷ѣ�ֻ����ȫ�˴��� false:������ true:����")]
        public bool IsUnCAQuitAll
        {
            set
            {
                this.isUnCAQuitAll = value;

            }
            get
            {
                return this.isUnCAQuitAll;
            }
        }
        private bool isQuitSamePayMod = false;
        /// <summary>
        /// �Ƿ�ԭ֧����ʽ�˷�
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�����ԭ֧����ʽ�˷� false:������ true:����")]
        public bool IsQuitSamePayMod
        {
            set
            {
                this.isQuitSamePayMod = value;

            }
            get
            {
                //if (isUnCAQuitAll == false)
                //{
                return this.isQuitSamePayMod;
                //}
                //else
                //{
                //    if (justCA == true)
                //    {
                //        return this.isAllowQuitFeeHalf;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
            }
        }
        protected bool isAutoBankTrans = false;    //ҽ����HIS����ʱ�շ�

        /// <summary>
        /// �Ƿ�������� false:������ true:����
        /// </summary>
        private bool isAllowQuitFeeHalf = false;

        /// <summary>
        /// �������
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�������� false:������ true:����")]
        public bool IsAllowQuitFeeHalf
        {
            set
            {
                this.isAllowQuitFeeHalf = value;

            }
            get
            {
                if (isUnCAQuitAll == false)
                {
                    return this.isAllowQuitFeeHalf;
                }
                else
                {
                    if (justCA == false)
                    {
                        if (this.isAllowQuitFeeHalf == true)
                        {
                            return false;
                        }
                    }
                    return this.isAllowQuitFeeHalf;
                }
            }
        }

        /// <summary>
        /// �Ƿ��ӡ��Ʊ {E47AD522-2ACA-4482-8DC5-6F2D7C04F082}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��ӡ��Ʊ")]
        private bool isPrintBill = false;

        /// <summary>
        /// �Ƿ��ӡ��Ʊ {E47AD522-2ACA-4482-8DC5-6F2D7C04F082}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��ӡ��Ʊ")]
        public bool IsPrintQuitBill
        {
            set
            {
                this.isPrintBill = value;

            }
            get
            {
                return this.isPrintBill;
            }
        }

        /// <summary>
        /// �Ƿ��շ� {C6F1CFDA-7848-47c4-905E-E161B9F5C4C8}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ�շѽ���"), DefaultValue(true)]
        public bool IsQuitFee
        {
            set
            {
                this.isQuitFee = value;

            }
            get
            {
                return this.isQuitFee;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����ѯ��(ͨ�����Ų�ѯ)
        /// </summary>
        private bool isShowRegTree = false;

        /// <summary>
        /// �Ƿ���ʾ����ѯ��(ͨ�����Ų�ѯ)
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ����ѯ��(ͨ�����Ų�ѯ)"), DefaultValue(false)]
        public bool IsShowRegTree
        {
            get { return isShowRegTree; }
            set { isShowRegTree = value; }
        }

        /// <summary>
        /// �Ƿ�������ҩ��˽��棬��ҩ��˽��治�жϷ�Ʊ����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�������ҩ��˽��棬��ҩ��˽��治�жϷ�Ʊ����"), DefaultValue(false)]
        public bool IsQuitDrugConfirm
        {
            get
            {
                return isQuitDrugConfirm;
            }
            set
            {
                this.isQuitDrugConfirm = value;

                //��ҩ��˽��� ����ʾ���շ��ô��ڣ�����ʾ��ҩƷ�˷ѽ���
                this.isQuitFee = false;
            }
        }

        /// <summary>
        /// �Ƿ�����������˷���
        /// </summary>
        private bool isQuitOtherFee = false;

        /// <summary>
        /// �Ƿ�����������˷���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�����������˷���"), DefaultValue(false)]
        public bool IsQuitOtherFee
        {
            get { return isQuitOtherFee; }
            set { isQuitOtherFee = value; }
        }


        /// <summary>
        /// �Ƿ�����������˷���
        /// </summary>
        private bool isUseLogout = false;

        /// <summary>
        /// �Ƿ�����������˷���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��������Ϲ��ܣ�True = �� ��False = ��"), DefaultValue(false)]
        public bool IsUseLogout
        {
            get { return isUseLogout; }
            set { isUseLogout = value; }
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        private string operationPriv = string.Empty;

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        [Category("�ؼ�����"), Description("�������Ȩ�ޣ����磺0820+21��0820 �������Ȩ�ޣ�21 ��������Ȩ�ޣ�Ϊ���������ҪȨ��Ҳ����ʹ��")]
        public string OperationPriv
        {
            get
            {
                return operationPriv;
            }
            set
            {
                operationPriv = value;
            }
        }


        /// <summary>
        /// ���Ѱ���ʱ�Ƿ�����޸ķ��ñ���
        /// </summary>
        private bool isCanModifyRate = false;

        /// <summary>
        /// ���Ѱ���ʱ�Ƿ�����޸ķ��ñ���
        /// </summary>
        [Category("�ؼ�����"), Description("���Ѱ���ʱ�Ƿ�����޸ķ��ñ�����True = �� ��False = ��"), DefaultValue(false)]
        public bool IsCanModifyRate
        {
            get { return isCanModifyRate; }
            set { isCanModifyRate = value; }
        }

        /// <summary>
        /// ���Ѱ���ʱ�Ƿ�����޸Ļ�����Ϣ�ͽ��㷽ʽ
        /// </summary>
        private bool isCanModifyPatientInfo = false;

        /// <summary>
        /// ���Ѱ���ʱ�Ƿ�����޸Ļ�����Ϣ�ͽ��㷽ʽ
        /// </summary>
        [Category("�ؼ�����"), Description("���Ѱ���ʱ�Ƿ�����޸Ļ�����Ϣ�ͽ��㷽ʽ��True = �� ��False = ��"), DefaultValue(false)]
        public bool IsCanModifyPatientInfo
        {
            get { return isCanModifyPatientInfo; }
            set { isCanModifyPatientInfo = value; }
        }

        /// <summary>
        /// ���Ѱ���ʱ�Ƿ���Ҫ���˵���
        /// </summary>
        private bool isNeedJZD = false;

        [Category("�ؼ�����"), Description("���Ѱ���ʱ�Ƿ���Ҫ���˵���,true:�ǣ�false����")]
        public bool IsNeedJZD
        {
            get { return this.isNeedJZD; }
            set { this.isNeedJZD = value; }
        }

        /// <summary>
        /// ���۱���ʱ�Ƿ񱣴����������
        /// </summary>
        private bool isSaveChargeRoundFee = false;

        /// <summary>
        /// ���۱���ʱ�Ƿ񱣴����������
        /// </summary>
        [Category("�ؼ�����"), Description("���۱���ʱ�Ƿ񱣴����������,true:�ǣ�false����")]
        public bool IsSaveChargeRoundFee
        {
            get { return this.isSaveChargeRoundFee; }
            set { this.isSaveChargeRoundFee = value; }
        }

        /// <summary>
        /// ���۱���ʱ�Ƿ񱣴�����
        /// </summary>
        private bool isSaveChargeDiagFee = false;

        [Category("�ؼ�����"), Description("���۱���ʱ�Ƿ񱣴�����,true:�ǣ�false����")]
        public bool IsSaveChargeDiagFee
        {
            get { return this.isSaveChargeDiagFee; }
            set { this.isSaveChargeDiagFee = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ���շ�
        /// </summary>
        private bool isShowReChargeTab = true;

        /// <summary>
        /// �Ƿ���ʾ���շ�
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ���շ�,true:�ǣ�false����")]
        public bool IsShowReChargeTab
        {
            get { return this.isShowReChargeTab; }
            set { this.isShowReChargeTab = value; }
        }

        /// <summary>
        /// �Ƿ����˷�ʱ��ʾ���۱���
        /// </summary>
        private bool isShowSaveChargeHits = false;

        /// <summary>
        /// �Ƿ����˷�ʱ��ʾ���۱���
        /// </summary>
        [Category("��������"), Description("�Ƿ����˷�ʱ��ʾ���۱���,trus:��;false:��")]
        public bool IsShowSaveChargeHits
        {
            get { return this.isShowSaveChargeHits; }
            set { this.isShowSaveChargeHits = value; }
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

        #region  ö��
        /// <summary>
        /// ����ҩ��ʾ��
        /// </summary>
        protected enum DrugList
        {
            /// <summary>
            /// ����
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// ��
            /// </summary>
            Comb = 1,

            /// <summary>
            /// ���
            /// </summary>
            CombNo = 2,

            /// <summary>
            /// ���
            /// </summary>
            Specs = 3,

            /// <summary>
            /// ����
            /// </summary>
            Amount = 4,

            /// <summary>
            /// ��λ
            /// </summary>
            PriceUnit = 5,

            /// <summary>
            /// ��������
            /// </summary>
            NoBackQty = 6,

            /// <summary>
            /// ���
            /// </summary>
            Cost = 7,

            /// <summary>
            /// ÿ�����͸���
            /// </summary>
            DoseAndDays = 8
        }
        /// <summary>
        /// ���˷�ҩ��ʾ��
        /// </summary>
        protected enum UndrugList
        {
            /// <summary>
            /// ����
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// ��
            /// </summary>
            Comb = 1,

            /// <summary>
            /// ���
            /// </summary>
            CombNo = 2,

            /// <summary>
            /// ����
            /// </summary>
            Amount = 3,

            /// <summary>
            /// ��λ
            /// </summary>
            PriceUnit = 4,

            /// <summary>
            /// ��������
            /// </summary>
            NoBackQty = 5,

            /// <summary>
            /// ���
            /// </summary>
            Cost = 6,

            /// <summary>
            /// �����Ŀ����
            /// </summary>
            PackageName = 7
        }
        /// <summary>
        /// ����ҩ��ʾ��
        /// </summary>
        protected enum DrugListQuit
        {
            /// <summary>
            /// ����
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// ���
            /// </summary>
            Specs = 1,

            /// <summary>
            /// ����
            /// </summary>
            Amount = 2,

            /// <summary>
            /// ��λ
            /// </summary>
            PriceUnit = 3,

            /// <summary>
            /// ��־
            /// </summary>
            Flag = 4,

            Price = 5,

            Cost = 6
        }
        /// <summary>
        /// ���˷�ҩ��ʾ��
        /// </summary>
        protected enum UndrugListQuit
        {
            /// <summary>
            /// ����
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// ����
            /// </summary>
            Amount = 1,

            /// <summary>
            /// ��λ
            /// </summary>
            PriceUnit = 2,

            /// <summary>
            /// ��־
            /// </summary>
            Flag = 3
        }

        #endregion

        #region ����

        #region ˽�з���



        /// <summary>
        /// ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int LoadPlugins()
        {
            //this.itemInputControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay>
            //       (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_ITEM_INPUT, new ucDisplay());

            this.itemInputControl = this.ucDisplay1;
            this.ucDisplay1.IsQuitFee = true;

            this.itemInputControl.ItemKind = ItemKind.All;

            //this.leftControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft>
            //        (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_LEFT, new ucInvoicePreview());

            this.leftControl = this.ucInvoicePreview1;

            //{1B220814-0243-4725-882C-012E831C0DA1}
            this.leftControl.InvoiceUpdated += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(leftControl_InvoiceUpdated);



            //�����ж��շѻ��ǻ���
           
            this.leftControl.IsPreFee = false;
            this.itemInputControl.LeftControl = this.leftControl;

            //this.rightControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight>
            //    (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_RIGHT, new ucCostDisplay());

            this.rightControl = this.ucCostDisplay1;
            this.rightControl.IsPreFee = false;

            this.itemInputControl.RightControl = this.rightControl;

            if (this.isQuitFee)
            {
                this.leftControl.IsValidFee = true;
                this.leftControl.Init();

                if (this.neuTabControl1.TabPages.Count > 1)
                {
                    this.leftControl.InitInvoice();
                }

                this.rightControl.Init();

                if (!isShowReChargeTab)
                {
                    this.neuTabControl1.Controls.Remove(this.tpFee);
                }
            }
            else//{C6F1CFDA-7848-47c4-905E-E161B9F5C4C8}
            {
                this.neuTabControl1.Controls.Remove(this.tpFee);
            }
            //�����ӿ�
            bankTrans = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans>(this.GetType());
            if (bankTrans == null)
            {
                bankTrans = new FS.HISFC.Components.OutpatientFee.Forms.frmBankTrans();
            }

            #region {2E5139C9-52D8-4fec-A96B-09BECFDDFBD1}

            if (this.isShowRegTree)
            {
                ucInvoiceView.Visible = true;
                ucInvoiceView.Focus();
            }
            else
            {
                ucInvoiceView.Visible = false;
            }

            #endregion

            return 1;
        }

        //{1B220814-0243-4725-882C-012E831C0DA1}
        void leftControl_InvoiceUpdated()
        {
            this.cmbRegDept.Focus();
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

            if (this.isQuitFee)
            {
                this.itemInputControl.Init();
            }

            return 1;
        }
        bool justCA = true;
        /// <summary>
        /// ͨ����Ʊ�Ż�÷�Ʊ��Ϣ����
        /// </summary>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <returns>�ɹ� ��Ʊ��Ϣ���� ʧ�� null</returns>
        protected virtual ArrayList QueryBalancesByInvoiceNO(string invoiceNO)
        {
            //ͨ�����뷢Ʊ�ţ���÷�Ʊ�����У���ͨ�����л�����з�Ʊ���ϡ�
            ArrayList balances = this.outpatientManager.QueryBalancesSameInvoiceCombNOByInvoiceNO(invoiceNO);
            ArrayList pays = this.outpatientManager.QueryBalancePaysByInvoiceNO(invoiceNO);
            //-		[0]	{}	object {FS.HISFC.Models.Fee.Outpatient.BalancePay}
            justCA = true;
            foreach (FS.HISFC.Models.Fee.Outpatient.BalancePay bp in pays)
            {
                if (bp != null)
                {
                    if (bp.PayType.ID != "CA")
                    {
                        justCA = false;
                        break;
                    }
                }
            }

            //��ѯҵ������
            if (balances == null)
            {
                tbInvoiceNO.SelectAll();
                MessageBox.Show("��ѯ��Ʊ����!" + this.outpatientManager.Err);
                tbInvoiceNO.Focus();

                return null;
            }
            //û���ҵ���¼
            if (balances.Count == 0)
            {
                tbInvoiceNO.SelectAll();
                MessageBox.Show("��Ʊ�Ų�����,������¼��");
                tbInvoiceNO.Focus();

                return null;
            }

            return balances;
        }

        /// <summary>
        /// ��������෢Ʊʱ����˷����,��Ʊ���
        /// </summary>
        /// <param name="balances">��ǰ��Ʊ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1 </returns>
        protected virtual int DealMulityBalancesCount(ref ArrayList balances)
        {
            bool isSelect = false;//Ĭ�ϲ���Ҫ����ѡ��Ʊ����.
            string SeqNo = string.Empty;//��Ʊ���к�
            //ѭ��������ǰ��õ����з�Ʊ��Ϣ.
            foreach (Balance balance in balances)
            {
                if (SeqNo == string.Empty)
                {
                    SeqNo = balance.CombNO;

                    continue;
                }
                else
                {
                    //��������з�Ʊ���в�ͬ�����,˵�������ظ���Ʊ�����,��Ҫ������Ʊѡ�񴰿�.
                    if (SeqNo != balance.CombNO)
                    {
                        isSelect = true;
                    }
                }
            }
            if (isSelect) //�ж��Ƿ���Ҫѡ��Ʊ
            {
                //����ѡ��Ʊ����ʵ��
                ucInvoiceSelect uc = new ucInvoiceSelect();
                //װ�ر��μ��������з�Ʊ��Ϣ
                uc.Add(balances);
                //������Ʊѡ�񴰿�
                FS.FrameWork.WinForms.Classes.Function.PopForm.TopMost = true;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                //�������Աû�н���ѡ�������ʾ
                if (uc.SelectedBalance == null || uc.SelectedBalance.CombNO == string.Empty)
                {
                    MessageBox.Show("��ѡ��Ҫ�˵ķ�Ʊ");

                    return -1;
                }
                //ͨ������Աѡ��ķ�Ʊ��Ϣ,ѡ����Ψһ��Ʊ����,�ٸ��ݷ�Ʊ���л�ȡ����Ӧ�����˷ѵ����з�Ʊ��Ϣ.
                balances = outpatientManager.QueryBalancesByInvoiceSequence(uc.SelectedBalance.CombNO);
                if (balances == null)
                {
                    MessageBox.Show("��ѯ��Ʊʧ��" + outpatientManager.Err);

                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// �Ƿ����Ա�����˵�ǰ��Ʊ
        /// </summary>
        /// <param name="balances">��ǰ��Ʊ����</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        protected virtual bool IsOperCanQuitTheseBalances(ArrayList balances)
        {
            //��ȡ���Ʋ���
            //��ȡ�Ƿ���������������Ա�ķ��á�
            bool isCanQuitOterhOper = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_OTHER_OPER_INVOICE, true, false);

            if (this.isQuitDrugConfirm == false)        //����������ҩ��� ����Ҫ���Ӵ�У��
            {
                //����������������Ա����,�жϵ�ǰ��Ʊ���շѲ���Ա�Ƿ�Ϊ��ǰ����Ա
                //������ǣ���ô����������˷�;
                if (!isCanQuitOterhOper)
                {
                    Balance tempBalance = balances[0] as Balance;

                    if (tempBalance == null)
                    {
                        MessageBox.Show("��Ʊ��ʽת������!");
                        tbInvoiceNO.SelectAll();
                        tbInvoiceNO.Focus();

                        return false;
                    }

                    if (tempBalance.BalanceOper.ID != this.outpatientManager.Operator.ID)
                    {
                        MessageBox.Show("�÷�Ʊ���շ�ԱΪ: " + tempBalance.BalanceOper.ID + "��û��Ȩ�޽����˷�!");
                        tbInvoiceNO.SelectAll();
                        tbInvoiceNO.Focus();

                        return false;
                    }

                    tempBalance = null;
                }
            }

            //����Ƿ�������ս�����ÿ��Ʋ���
            bool isCanQuitDayBlanced = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_DAYBALANCED_INVOICE, true, false);

            if (!isCanQuitDayBlanced)//���������ս������
            {
                Balance tmpInvoice = balances[0] as Balance;

                if (tmpInvoice == null)
                {
                    MessageBox.Show("��Ʊ��ʽת������!");
                    tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }
                if (tmpInvoice.IsDayBalanced)
                {
                    MessageBox.Show("�÷�Ʊ�Ѿ��ս�,��û��Ȩ�޽����˷�!");
                    tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }
            }

            int canQuitDays = this.controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.VALID_QUIT_DAYS, true, 10000);

            DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();

            Balance tmpInvoiceValid = balances[0] as Balance;

            if (tmpInvoiceValid == null)
            {
                MessageBox.Show("��Ʊ��ʽת������!");
                tbInvoiceNO.SelectAll();
                tbInvoiceNO.Focus();

                return false;
            }

            int tempDays = (nowTime - tmpInvoiceValid.BalanceOper.OperTime).Days;

            if (tempDays >= canQuitDays)
            {
                MessageBox.Show("�÷�Ʊ�Ѿ��������˷�����,�������˷�!");

                tbInvoiceNO.SelectAll();
                tbInvoiceNO.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// ��ѯ��Ʊ��Ϣ
        /// </summary>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <returns>null���� Ҫ����ķ�Ʊ����</returns>
        protected virtual ArrayList QueryInvoices(string invoiceNO)
        {
            //��Ʊ��λ
            invoiceNO = invoiceNO.PadLeft(12, '0');
            InvoiceNoStr = invoiceNO;
            //ͨ����Ʊ�Ż�÷�Ʊ��Ϣ����
            ArrayList balances = this.QueryBalancesByInvoiceNO(invoiceNO);
            if (balances == null)
            {
                return null;
            }

            //�Ƿ����Ա�����˵�ǰ��Ʊ
            if (!this.IsOperCanQuitTheseBalances(balances))
            {
                return null;
            }

            //�����õķ�Ʊ����һ��(��Ϊ�ַ�Ʊ���������),��ô�ж��Ƿ�Ҫ������Ʊѡ�����
            //����Ķ��ŷ�Ʊ�������������� 1 �ַ�Ʊ������,��Ʊ�Ų�ͬ,���Ƿ�Ʊ������ͬ,�������赯��ѡ��Ʊ����.
            // 2 ���������õķ�Ʊ���ظ�(ϵͳ����Ʊ���ظ�),���Ƿ�Ʊ���в�ͬ,���ʱ��Ҫ����ѡ��Ʊ����,�ò���Ա
            //����ѡ��.��֮���Ƿ�Ʊ���о����˴��˷ѵķ�Ʊ��Ϣ.
            if (balances.Count > 1)
            {
                if (this.DealMulityBalancesCount(ref balances) == -1)
                {
                    return null;
                }
            }

            return balances;
        }

        /// <summary>
        /// ��ʼ�� 
        /// </summary>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        protected virtual int Init()
        {
            if (this.LoadPlugins() < 0)
            {
                return -1;
            }

            if (this.InitItemInputControl() < 0)
            {
                return 1;
            }

            //��ʼ�� �Һſ���
            ArrayList regDeptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
            if (regDeptList == null)
            {
                MessageBox.Show("��ʼ���Һſ��ҳ���!" + this.managerIntegrate.Err);

                return -1;
            }
            this.cmbRegDept.AddItems(regDeptList);

            //��ʼ��ҽ���б�����һ���޹���ҽ�������999
            ArrayList doctList = new ArrayList();
            doctList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctList == null)
            {
                MessageBox.Show("��ʼ��ҽ���б����!" + this.managerIntegrate.Err);

                return -1;
            }
            FS.HISFC.Models.Base.Employee pNone = new FS.HISFC.Models.Base.Employee();
            pNone.ID = "999";
            pNone.Name = "�޹���";
            pNone.SpellCode = "WGS";
            pNone.UserCode = "999";
            doctList.Add(pNone);

            this.cmbDoct.AddItems(doctList);

            //��������ѷ��ñ���
            ArrayList lst = this.constManager.GetList("ROUNDFEEITEMCODE");
            if (lst.Count > 0)
            {
                roundFeeItemCode = ((FS.HISFC.Models.Base.Const)lst[0]).ID;
            }

            //�Է�������Ŀ����
            this.ownDiagFeeCode = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.AUTO_PUB_FEE_DIAG_FEE_CODE, true, string.Empty);

            return 1;
        }

        #endregion

        /// <summary>
        /// ��÷�����ϸ
        /// </summary>
        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
        protected virtual int GetItemList()
        {
            try
            {
                //��ñ����˷����з�Ʊ�ĵ�һ����Ϊ��ʱ��Ʊ��Ϣ
                Balance tempBalance = quitInvoices[0] as Balance;

                isHaveRebateCost = false;

                //ͨ����Ʊ���к�,�������Ӧ�����˷ѵ�ҩƷ��Ϣ
                ArrayList drugItemLists = this.outpatientManager.QueryDrugFeeItemListByInvoiceSequence(tempBalance.CombNO);
                if (drugItemLists == null)
                {
                    MessageBox.Show("���ҩƷ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
                //ͨ����Ʊ���к�,�������Ӧ�����˷ѵķ�ҩƷ��Ϣ
                ArrayList undrugItemLists = outpatientManager.QueryUndrugFeeItemListByInvoiceSequence(tempBalance.CombNO);
                if (undrugItemLists == null)
                {
                    MessageBox.Show("��÷�ҩƷ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }

                #region ����
                //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                ArrayList mateItemLists = outpatientManager.QueryMateFeeItemListByInvoiceSequence(tempBalance.CombNO);
                if (mateItemLists == null)
                {
                    MessageBox.Show("���������Ϣ����" + outpatientManager.Err);
                    return -1;
                }
                //��ʱ�����ʷ��ڷ�ҩƷ�д���
                undrugItemLists.AddRange(mateItemLists.ToArray());
                #endregion

                if (drugItemLists.Count + undrugItemLists.Count == 0)
                {
                    MessageBox.Show("û�з�����Ϣ!");

                    return -1;
                }

                this.invoiceFeeItemLists = outpatientManager.QueryFeeItemListsByInvoiceNO(tempBalance.Invoice.ID);

                ArrayList drugConfirmList = new ArrayList();//�Ѿ���׼����ҩ��Ϣ
                ArrayList undrugConfirmList = new ArrayList();//�Ѿ���׼�˷ѵķ�ҩƷ��Ϣ
                //ѭ�����в����˷ѵķ�Ʊ,��ѯ�Ѿ���׼��ҩƷ�ͷ�ҩƷ��Ϣ
                //���ڶ��ŷ�Ʊ�Ĵ���,����ϸֻ��Ӧһ����Ʊ��,���Ա����еĲ����˷ѵķ�Ʊ,����ֻ��һ����Ʊ�ŷ��ϲ�ѯ����.
                foreach (Balance balance in this.quitInvoices)
                {
                    //����Ѿ�������Ѿ���׼�˷ѵ�ҩƷ��Ϣ,�Ͳ��ٻ�ȡ
                    if (drugConfirmList == null || drugConfirmList.Count == 0)
                    {
                        //����Ѿ���׼����ҩ��Ϣ
                        drugConfirmList = returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, true, false, "1");
                        if (drugConfirmList == null)
                        {
                            MessageBox.Show("���ȷ��ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                            return -1;
                        }
                    }
                    //����Ѿ�������Ѿ���׼�˷ѵķ�ҩƷ��Ϣ,�Ͳ��ٻ�ȡ
                    if (undrugConfirmList == null || undrugConfirmList.Count == 0)
                    {
                        //����Ѿ���׼�˷ѵķ�ҩƷ��Ϣ
                        undrugConfirmList = returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, true, false, "0");
                        if (undrugConfirmList == null)
                        {
                            MessageBox.Show("���ȷ�Ϸ�ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                            return -1;
                        }
                    }
                }



                //��ʾ����ҩƷ��Ϣ
                this.fpSpread1_Sheet1.RowCount = drugItemLists.Count;

                FeeItemList drugItem = null;//ҩƷ��ʱʵ��
                for (int i = 0; i < drugItemLists.Count; i++)
                {
                    drugItem = drugItemLists[i] as FeeItemList;

                    if (drugItem.FT.RebateCost > 0)
                    {
                        isHaveRebateCost = true;
                    }


                    //���¼��㱾��ҩƷ���ܽ��,�����Ժ����������
                    drugItem.FT.TotCost = drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost;

                    this.fpSpread1_Sheet1.Rows[i].Tag = drugItem;
                    //��Ϊ���ܴ���ͬһ��Ʊ�в�ͬ������ҵ����,���ҹҺ���Ϣ�еĿ�����Ϣ��һ����ʵ���շѵĿ���
                    //������ͬ,��������ѹҺ�ʵ��Ŀ�����Ǹ�ֵΪ�շ���ϸʱ�Ŀ��������Ϣ.
                    this.patient.DoctorInfo.Templet.Dept = drugItem.RecipeOper.Dept;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.ItemName].Text = drugItem.Item.Name;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.CombNo].Text = drugItem.Order.Combo.ID;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Specs].Text = drugItem.Item.Specs;
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text = drugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty / drugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.PriceUnit].Text = drugItem.Item.PriceUnit;
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = drugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty / drugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty, 2).ToString();

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Cost].Text = (drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost).ToString();

                    if (drugItem.Item.SysClass.ID.ToString() == "PCC")
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit + " " + "����:" + drugItem.Days.ToString();
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit;
                    }

                    FS.HISFC.Components.OutpatientFee.Class.Function.DrawCombo(this.fpSpread1_Sheet1, (int)DrugList.CombNo, (int)DrugList.Comb, 0);
                }

                //��ʾ��ҩƷ��Ϣ
                this.fpSpread1_Sheet2.RowCount = undrugItemLists.Count;

                FeeItemList undrugItem = null;
                for (int i = 0; i < undrugItemLists.Count; i++)
                {
                    undrugItem = undrugItemLists[i] as FeeItemList;

                    #region ����������Ϣ
                    ////{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    //if (undrugItem.Item.ItemType == EnumItemType.UnDrug)
                    //{
                    //    //{143CA424-7AF9-493a-8601-2F7B1D635027}
                    //    string outNo = undrugItem.UpdateSequence.ToString();
                    //    List<HISFC.Models.FeeStuff.Output> list = mateIntegrate.QueryOutput(outNo);
                    //    undrugItem.MateList = list;
                    //}
                    #endregion

                    if (undrugItem.FT.RebateCost > 0)
                    {
                        isHaveRebateCost = true;
                    }

                    undrugItem.FT.TotCost = undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost;
                    this.fpSpread1_Sheet2.Rows[i].Tag = undrugItem;
                    this.patient.DoctorInfo.Templet.Dept = undrugItem.RecipeOper.Dept;

                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.ItemName].Text = undrugItem.Item.Name;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.CombNo].Text = undrugItem.Order.Combo.ID;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text = undrugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty / undrugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PriceUnit].Text = undrugItem.Item.PriceUnit;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.NoBackQty].Text = undrugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty / undrugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Cost].Text = (undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost).ToString();
                    if (undrugItem.UndrugComb.ID != null && undrugItem.UndrugComb.ID.Length > 0)
                    {
                        this.undrugComb = this.undrugManager.GetValidItemByUndrugCode(undrugItem.UndrugComb.ID);
                        if (this.undrugComb == null)
                        {
                            MessageBox.Show("���������Ϣ�����޷���ʾ�����Զ����룬���ǲ�Ӱ���˷Ѳ�����");
                        }
                        else
                        {
                            undrugItem.UndrugComb.UserCode = this.undrugComb.UserCode;
                        }

                        FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                        if (item == null)
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name;
                        }
                        else
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name + "[" + item.UserCode + "]";
                        }

                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                        if (item != null)
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = item.UserCode;
                        }
                    }

                    FS.HISFC.Components.OutpatientFee.Class.Function.DrawCombo(this.fpSpread1_Sheet2, (int)UndrugList.CombNo, (int)UndrugList.Comb, 0);
                    //��ʾ������Ϣ
                    SetMateData(undrugItem, i);
                }
                //��ʾȷ����ҩ��Ϣ
                this.fpSpread2_Sheet1.RowCount = drugItemLists.Count + drugConfirmList.Count;
                FS.HISFC.Models.Fee.ReturnApply drugReturn = null;
                for (int i = 0; i < drugConfirmList.Count; i++)
                {
                    drugReturn = drugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                    this.fpSpread2_Sheet1.Rows[i].Tag = drugReturn;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.ItemName].Text = drugReturn.Item.Name;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Amount].Text = drugReturn.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty / drugReturn.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.PriceUnit].Text = drugReturn.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Specs].Text = drugReturn.Item.Specs;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Flag].Text = "ȷ��";
                    

                    int findRow = FindItem(drugReturn.RecipeNO, drugReturn.SequenceNO, this.fpSpread1_Sheet1);
                    if (findRow == -1)
                    {
                        MessageBox.Show("����δ��ҩ��Ŀ����!");

                        return -1;
                    }
                    FeeItemList modifyDrug = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;

                    modifyDrug.NoBackQty = modifyDrug.NoBackQty - drugReturn.Item.Qty;
                    modifyDrug.Item.Qty = modifyDrug.Item.Qty - drugReturn.Item.Qty;
                    modifyDrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Price * modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2);
                    modifyDrug.FT.OwnCost = modifyDrug.FT.TotCost;

                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = modifyDrug.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = modifyDrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = modifyDrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.NoBackQty / modifyDrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.NoBackQty, 2).ToString();
                }
                this.fpSpread2_Sheet2.RowCount = undrugItemLists.Count + undrugConfirmList.Count;
                FS.HISFC.Models.Fee.ReturnApply undrugReturn = null;
                for (int i = 0; i < undrugConfirmList.Count; i++)
                {
                    undrugReturn = undrugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                    this.fpSpread2_Sheet2.Rows[i].Tag = undrugReturn;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.ItemName].Text = undrugReturn.Item.Name;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Amount].Text = undrugReturn.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugReturn.Item.Qty / undrugReturn.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugReturn.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.PriceUnit].Text = undrugReturn.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Flag].Text = "ȷ��";

                    int findRow = FindItem(undrugReturn.RecipeNO, undrugReturn.SequenceNO, this.fpSpread1_Sheet2);
                    if (findRow == -1)
                    {
                        MessageBox.Show("����δ�˷�ҩ��Ŀ����!");

                        return -1;
                    }
                    FeeItemList modifyUndrug = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;

                    modifyUndrug.NoBackQty = modifyUndrug.NoBackQty - undrugReturn.Item.Qty;
                    modifyUndrug.Item.Qty = modifyUndrug.Item.Qty - undrugReturn.Item.Qty;
                    modifyUndrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Price * modifyUndrug.Item.Qty / modifyUndrug.Item.PackQty, 2);
                    modifyUndrug.FT.OwnCost = modifyUndrug.FT.TotCost;

                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Cost].Text = modifyUndrug.FT.TotCost.ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Amount].Text = modifyUndrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Qty / modifyUndrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = modifyUndrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.NoBackQty / modifyUndrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.NoBackQty, 2).ToString();

                }

                if (isHaveRebateCost)
                {
                    this.ckbAllQuit.Checked = true;
                    this.ckbAllQuit.Enabled = false;
                }
                else
                {
                    this.ckbAllQuit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ʾ��������
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="rowIndex"></param>
        //{143CA424-7AF9-493a-8601-2F7B1D635026}
        protected virtual void SetMateData(FeeItemList feeItemList, int rowIndex)
        {

            int index = 0;
            //{4D6501CB-D2A4-4204-8CBA-F34F28D5300A} ��ҩƷ-���ʶ��շ�ʽ�˷��޸�
            if (feeItemList.MateList == null )
            {
                return;
            }
            if (feeItemList.MateList.Count < 1) return;

            fpSpread1_Sheet2.RowHeader.Cells[rowIndex, 0].Text = "+";
            fpSpread1_Sheet2.RowHeader.Cells[rowIndex, 0].BackColor = Color.YellowGreen;

            foreach (FS.HISFC.Models.FeeStuff.Output outItem in feeItemList.MateList)
            {
                fpSpread1_Sheet2.Rows.Add(fpSpread1_Sheet2.Rows.Count, 1);
                index = fpSpread1_Sheet2.Rows.Count - 1;
                this.fpSpread1_Sheet2.Cells[index, 0].Text = outItem.StoreBase.Item.Name;
                this.fpSpread1_Sheet2.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                outItem.StoreBase.Item.Qty = outItem.StoreBase.Quantity - outItem.ReturnApplyNum - outItem.StoreBase.Returns;
                this.fpSpread1_Sheet2.Cells[index, 3].Text = outItem.StoreBase.Item.Qty.ToString();
                this.fpSpread1_Sheet2.Cells[index, 4].Text = outItem.StoreBase.Item.PriceUnit;
                this.fpSpread1_Sheet2.Cells[index, 5].Text = outItem.StoreBase.Item.Qty.ToString();
                this.fpSpread1_Sheet2.Cells[index, 6].Text = (outItem.StoreBase.Item.Qty * outItem.StoreBase.Item.Price).ToString();
                this.fpSpread1_Sheet2.Cells[index, 7].Text = outItem.StoreBase.Item.UserCode;
                this.fpSpread1_Sheet2.RowHeader.Cells[index, 0].Text = ".";
                this.fpSpread1_Sheet2.RowHeader.Cells[index, 0].BackColor = System.Drawing.Color.SkyBlue;
                this.fpSpread1_Sheet2.Rows[index].Tag = outItem;
                this.fpSpread1_Sheet2.Rows[index].Visible = false;

            }
        }

        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequence">������ˮ��</param>
        /// <param name="sv">��ǰSheetView</param>
        /// <returns></returns>
        protected virtual int FindItem(string recipeNO, int sequence, FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.RowCount; i++)
            {
                if (sv.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = sv.Rows[i].Tag as FeeItemList;
                    if (f.RecipeNO == recipeNO && f.SequenceNO == sequence)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequence">������ˮ��</param>
        /// <param name="sv">��ǰSheetView</param>
        /// <returns></returns>
        protected virtual int FindItem(string recipeNO, FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.RowCount; i++)
            {
                if (sv.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = sv.Rows[i].Tag as FeeItemList;
                    if (f.RecipeNO == recipeNO)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        /// <summary>
        /// ����������������
        /// </summary>
        /// <param name="sv">��ǰSheetViewҳ</param>
        /// <returns>�ɹ� ��ǰ�������ӵ��� ʧ�� -1</returns>
        protected virtual int FindNullRow(FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.RowCount; i++)
            {
                if (sv.Rows[i].Tag == null || !(sv.Rows[i].Tag is FS.FrameWork.Models.NeuObject))
                {
                    return i;
                }
                else if(string.IsNullOrEmpty(sv.Cells[i,(int)DrugList.ItemName].Text))
                {
                    return i;
                }

            }

            return -1;
        }

        /// <summary>
        /// ȫ�˲���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int AllQuit()
        {
            this.ckbAllQuit.Checked = true;

            int temp = 0;
            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)
            {
                temp = 1;
            }
            else
            {
                temp = 2;
            }

            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.fpSpread1_Sheet1.ActiveRowIndex = i;
                if (this.QuitOperation() == -1)
                {
                    return -1;
                }
            }
            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                this.fpSpread1_Sheet2.ActiveRowIndex = i;
                if (this.QuitOperation() == -1)
                {
                    return -1;
                }
            }

            if (temp == 1)
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
            }
            else
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
            }

            return 1;
        }

        /// <summary>
        /// ����˫��,�س�ѡ����Ŀ�˷�
        /// </summary>
        protected virtual void DealQuitOperation()
        {
            //bool isNeedGroupAllQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT, false, false);

            ////tmpValue = bValue;

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)//��ҩƷ
            {
                #region ��ҩƷ
                int currRow = this.fpSpread1_Sheet2.ActiveRowIndex;

                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;
                    if (f.UndrugComb.ID != string.Empty && this.isNeedAllQuit && this.ckbAllQuit.Checked)
                    {
                        for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
                        {
                            if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                            {
                                FeeItemList fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;
                                if (fTemp.UndrugComb.ID == f.UndrugComb.ID && fTemp.Order.ID == f.Order.ID)
                                {
                                    this.QuitUndrugOperation(i);
                                }
                            }
                        }

                        return;
                    }
                    else
                    {
                        QuitOperation();
                    }
                }
                //{143CA424-7AF9-493a-8601-2F7B1D635026}
                //������Ŀ
                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FS.HISFC.Models.FeeStuff.Output)
                {
                    QuitOperationMate(currRow);
                }
                #endregion
            }
            else
            {
                //if (this.isPharmacySameRecipeQuitAll == false)
                //{
                QuitOperation();
                //}
                //else
                //{
                //    QuitOperationPharmacySameRecipeQuitAll();
                //}
            }
        }

        /// <summary>
        ///��ȥҩƷ���� 
        /// </summary>
        /// <param name="currRow">��ǰ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QuitDrugOperation(int currRow)
        {
            if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                if (f.NoBackQty <= 0)
                {
                    return -2;
                }
                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                //û���ҵ�����ô����һ��;
                if (findRow == -1)
                {
                    findRow = FindNullRow(this.fpSpread2_Sheet1);
                    FeeItemList fClone = f.Clone();
                    this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                    //��ʾҩƷ���ۣ���ʾ���������в���
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();
                }
                else //�ҵ����ۼ�����
                {

                    FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty = fFind.Item.Qty + f.Item.Qty;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                    //��ʾҩƷ���ۣ���ʾ���������в���
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                }
                f.Item.Qty = f.Item.Qty - f.NoBackQty;
                f.NoBackQty = 0;
                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// �����ҩƷ��ǰ���˷�
        /// </summary>
        /// <param name="currRow">��ǰ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QuitUndrugOperation(int currRow)
        {
            if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;

                if (f.NoBackQty <= 0)
                {
                    return -2;
                }
                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                //û���ҵ�����ô����һ��;
                if (findRow == -1)
                {
                    findRow = FindNullRow(this.fpSpread2_Sheet2);
                    FeeItemList fClone = f.Clone();
                    this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                }
                else //�ҵ����ۼ�����
                {

                    FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty = fFind.Item.Qty + f.Item.Qty;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                }
                f.Item.Qty = f.Item.Qty - f.NoBackQty;
                f.NoBackQty = 0;
                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = "0";


            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// �����˷Ѳ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QuitOperation()
        {
            #region ҩƷ
            bool isBack = false;

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)//��ҩƷ
            {
                int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

                if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                    if (this.outpatientManager.Operator.ID.Equals(f.FeeOper.ID) == false)
                    {
                        //�ж�Ȩ��,�Ƿ����������Һ�Ա������Ȩ��
                        if (!CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivQuitOtherOperFee))
                        {
                            CommonController.CreateInstance().MessageBox("��û������������Ա�շѼ�¼��Ȩ�ޣ�������ȡ�����÷��õĲ���Ա�ǣ�" + CommonController.CreateInstance().GetEmployeeName(f.FeeOper.ID), MessageBoxIcon.Warning);
                            return -1;
                        }
                    }

                    if (this.ckbAllQuit.Checked)
                    {
                        if (!this.isNeedAllQuit || f.Item.SysClass.ID.ToString() != "PCC")
                        {
                            if (f.NoBackQty <= 0)
                            {
                                if (f.IsConfirmed)
                                {
                                   
                                   for (int i = 0;i<=this.fpSpread2_Sheet1.Rows.Count-1;++i)
                                   {
                                       if (this.fpSpread2_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                                       {
                                           FS.HISFC.Models.Fee.ReturnApply drugReturn = (FS.HISFC.Models.Fee.ReturnApply)this.fpSpread2_Sheet1.Rows[i].Tag;
                                           if (drugReturn.RecipeNO ==f.RecipeNO && drugReturn.SequenceNO==f.SequenceNO)
                                           {
                                               isBack = true;
                                               break;
                                           }

                                       }
                                       else
                                       {
                                           continue;
                                       }
                                   }
                                   if (!isBack)
                                   {
                                       MessageBox.Show(f.Item.Name + "�Ѿ���ҩ���뵽ҩ������ҩ��˺������˷�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                       return -1;
                                       
                                   }

                                  
                                }
                                else
                                {
                                    return 1;
                                }
                            }

                            int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                            //û���ҵ�����ô����һ��;
                            if (findRow == -1)
                            {
                                if (!isBack) //û����ҩ
                                {
                                    findRow = FindNullRow(this.fpSpread2_Sheet1);
                                    FeeItemList fClone = f.Clone();
                                    this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fClone.Item.Specs;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                                    //��ʾҩƷ���ۣ���ʾ���������в���
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();
                                }
                            }
                            else //�ҵ����ۼ�����
                            {
                              
                                    FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                                    fFind.Item.Qty = fFind.Item.Qty + f.NoBackQty;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                                    //��ʾҩƷ���ۣ���ʾ���������в���
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                                    
                               

                               
                            }

                            f.Item.Qty = f.Item.Qty - f.NoBackQty;
                            f.NoBackQty = 0;
                            f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                            f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                            f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;
                            f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
                        }
                        else
                        {
                            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                    {
                                        this.QuitDrugOperation(i);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.Combo.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }

                            txtReturnItemName.Text = "��ҩ���";
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PCC";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                        }
                    }
                }
            }

            #endregion

            #region ��ҩƷ

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)//��ҩƷ
            {
                int currRow = this.fpSpread1_Sheet2.ActiveRowIndex;

                // bool isNeedGroupAllQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT, false, false);

                //tmpValue = bValue;

                #region ����
                //{143CA424-7AF9-493a-8601-2F7B1D635026}
                //�Ƿ��Ƕ�������
                //List<HISFC.Models.FeeStuff.Output> outitemList = new List<FS.HISFC.Models.FeeStuff.Output>();
                //string headerText = this.fpSpread1.ActiveSheet.RowHeader.Cells[currRow, 0].Text;
                //if (headerText == "+" || headerText == "-")
                //{
                //    if (!this.ckbAllQuit.Checked)
                //    {
                //        if (!this.ckbAllQuit.Checked && headerText != ".")
                //        {
                //            MessageBox.Show("��ѡ��Ҫ�˷ѵ�������Ϣ��");
                //            if (this.fpSpread1_Sheet2.RowHeader.Cells[currRow, 0].Text == "+")
                //            {
                //                this.ExpandOrCloseRow(false, currRow + 1);
                //            }
                //            return -1;
                //        }
                //    }
                //}
                #endregion

                #region ��ҩƷ
                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;

                    if (this.outpatientManager.Operator.ID.Equals(f.FeeOper.ID) == false)
                    {
                        //�ж�Ȩ��,�Ƿ����������Һ�Ա������Ȩ��
                        if (!CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivQuitOtherOperFee))
                        {
                            CommonController.CreateInstance().MessageBox("��û������������Ա�շѼ�¼��Ȩ�ޣ�������ȡ�����÷��õĲ���Ա�ǣ�" + CommonController.CreateInstance().GetEmployeeName(f.FeeOper.ID), MessageBoxIcon.Warning);
                            return -1;
                        }
                    }
                    //if (f.Item.ID == this.roundFeeItemCode)
                    //{
                    //    CommonController.CreateInstance().MessageBox("��������Ѳ����ˣ�", MessageBoxIcon.Warning);
                    //    return -1;

                    //}
                    if (this.ckbAllQuit.Checked)
                    {
                        if (f.NoBackQty == 0)//�������������ѿ����Ǹ���
                        {
                            return 1;
                            //MessageBox.Show(f.Item.Name + "�Ѿ�û�п����������������˷�!");
                            //return -2;
                        }

                        int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                        //û���ҵ�����ô����һ��;
                        if (findRow == -1)
                        {
                            findRow = FindNullRow(this.fpSpread2_Sheet2);
                            FeeItemList fClone = new FeeItemList();
                            fClone = f.Clone();
                            this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                        }
                        else //�ҵ����ۼ�����
                        {
                            FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                            fFind.Item.Qty = fFind.Item.Qty + f.NoBackQty;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                        }
                        f.Item.Qty = f.Item.Qty - f.NoBackQty;
                        f.NoBackQty = 0;

                        f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                        f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                        f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;

                        f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = "0";

                        #region ����
                        //{143CA424-7AF9-493a-8601-2F7B1D635026}
                        int mateIndex = 0;
                        if (f.MateList.Count > 0)
                        {
                            foreach (FS.HISFC.Models.FeeStuff.Output tempOut in f.MateList)
                            {
                                mateIndex = GetMateRowIndex(tempOut);
                                if (mateIndex == -1)
                                {
                                    MessageBox.Show("����������Ϣʧ�ܣ�");
                                    return -1;
                                }
                                tempOut.StoreBase.Item.Qty = 0;
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.NoBackQty].Text = "0";
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Amount].Text = "0";
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Cost].Text = "0";
                                this.fpSpread1_Sheet2.Rows[mateIndex].Tag = tempOut;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //������Ŀ
                        if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            this.currentUndrugComb = this.undrugManager.GetValidItemByUndrugCode(f.UndrugComb.ID);
                            if (this.currentUndrugComb == null)
                            {
                                MessageBox.Show("��ø�����Ŀ����" + this.undrugManager.Err);

                                return -1;
                            }

                            this.currentUndrugCombs = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(this.currentUndrugComb.ID);

                            if (currentUndrugCombs == null)
                            {
                                MessageBox.Show("��ø�����Ŀ��ϸ����" + this.undrugPackAgeManager.Err);

                                return -1;
                            }

                            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;
                                    if (fTemp.UndrugComb.ID == f.UndrugComb.ID && fTemp.Order.ID == f.Order.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }

                            txtReturnItemName.Text = f.UndrugComb.Name;
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PACKAGE";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                            this.backType = string.Empty;
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                    }
                }
                #endregion
            }

            #endregion

            ComputCost();

            return 1;
        }

        /// <summary>
        /// �����˷Ѳ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QuitOperationPharmacySameRecipeQuitAll()
        {
            #region ҩƷ

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)//��ҩƷ
            {
                int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

                if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                    if (true)
                    {
                        if (!true)
                        {
                            if (f.NoBackQty <= 0)
                            {
                                MessageBox.Show(f.Item.Name + "�Ѿ�û�п����������������˷�!");

                                return -1;
                            }
                            int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                            //û���ҵ�����ô����һ��;
                            if (findRow == -1)
                            {
                                findRow = FindNullRow(this.fpSpread2_Sheet1);
                                FeeItemList fClone = f.Clone();
                                this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fClone.Item.Specs;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                                //��ʾҩƷ���ۣ���ʾ���������в���
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();
                            }
                            else //�ҵ����ۼ�����
                            {

                                FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                                fFind.Item.Qty = fFind.Item.Qty + f.NoBackQty;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                                //��ʾҩƷ���ۣ���ʾ���������в���
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                            }
                            f.Item.Qty = f.Item.Qty - f.NoBackQty;
                            f.NoBackQty = 0;
                            f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                            f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                            f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;
                            f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
                        }
                        else
                        {
                            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.RecipeNO == f.RecipeNO)
                                    {
                                        this.QuitDrugOperation(i);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.Combo.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }

                            txtReturnItemName.Text = "��ҩ���";
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PCC";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                        }
                    }
                }
            }

            #endregion

            #region ��ҩƷ

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)//��ҩƷ
            {
                int currRow = this.fpSpread1_Sheet2.ActiveRowIndex;

                // bool isNeedGroupAllQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT, false, false);

                //tmpValue = bValue;

                #region ����
                //{143CA424-7AF9-493a-8601-2F7B1D635026}
                //�Ƿ��Ƕ�������
                List<FS.HISFC.Models.FeeStuff.Output> outitemList = new List<FS.HISFC.Models.FeeStuff.Output>();
                string headerText = this.fpSpread1.ActiveSheet.RowHeader.Cells[currRow, 0].Text;
                if (headerText == "+" || headerText == "-")
                {
                    if (!this.ckbAllQuit.Checked)
                    {
                        if (!this.ckbAllQuit.Checked && headerText != ".")
                        {
                            MessageBox.Show("��ѡ��Ҫ�˷ѵ�������Ϣ��");
                            if (this.fpSpread1_Sheet2.RowHeader.Cells[currRow, 0].Text == "+")
                            {
                                this.ExpandOrCloseRow(false, currRow + 1);
                            }
                            return -1;
                        }
                    }
                }
                #endregion
                #region ��ҩƷ
                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;

                    if (this.ckbAllQuit.Checked)
                    {
                        if (f.NoBackQty <= 0)
                        {
                            MessageBox.Show(f.Item.Name + "�Ѿ�û�п����������������˷�!");
                            return -2;
                        }
                        int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                        //û���ҵ�����ô����һ��;
                        if (findRow == -1)
                        {
                            findRow = FindNullRow(this.fpSpread2_Sheet2);
                            FeeItemList fClone = f.Clone();
                            this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                        }
                        else //�ҵ����ۼ�����
                        {
                            FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                            fFind.Item.Qty = fFind.Item.Qty + f.NoBackQty;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                        }
                        f.Item.Qty = f.Item.Qty - f.NoBackQty;
                        f.NoBackQty = 0;

                        f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                        f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                        f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;

                        f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = "0";
                        #region ����
                        //{143CA424-7AF9-493a-8601-2F7B1D635026}
                        int mateIndex = 0;
                        if (f.MateList.Count > 0)
                        {
                            foreach (FS.HISFC.Models.FeeStuff.Output tempOut in f.MateList)
                            {
                                mateIndex = GetMateRowIndex(tempOut);
                                if (mateIndex == -1)
                                {
                                    MessageBox.Show("����������Ϣʧ�ܣ�");
                                    return -1;
                                }
                                tempOut.StoreBase.Item.Qty = 0;
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.NoBackQty].Text = "0";
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Amount].Text = "0";
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Cost].Text = "0";
                                this.fpSpread1_Sheet2.Rows[mateIndex].Tag = tempOut;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //������Ŀ
                        if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            this.currentUndrugComb = this.undrugManager.GetValidItemByUndrugCode(f.UndrugComb.ID);
                            if (this.currentUndrugComb == null)
                            {
                                MessageBox.Show("��ø�����Ŀ����" + this.undrugManager.Err);

                                return -1;
                            }

                            this.currentUndrugCombs = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(this.currentUndrugComb.ID);

                            if (currentUndrugCombs == null)
                            {
                                MessageBox.Show("��ø�����Ŀ��ϸ����" + this.undrugPackAgeManager.Err);

                                return -1;
                            }

                            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;
                                    if (fTemp.UndrugComb.ID == f.UndrugComb.ID && fTemp.Order.ID == f.Order.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }


                            txtReturnItemName.Text = f.UndrugComb.Name;
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PACKAGE";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                            this.backType = string.Empty;
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                    }
                }
                #endregion
            }

            #endregion

            ComputCost();

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex">��ǰѡ�ֵ���</param>
        /// <returns></returns>
        //{143CA424-7AF9-493a-8601-2F7B1D635026}
        private int QuitOperationMate(int rowIndex)
        {
            int undrugIndex = this.FinItemRowIndex(rowIndex);
            FeeItemList feeItem = this.fpSpread1_Sheet2.Rows[undrugIndex].Tag as FeeItemList;
            FeeItemList f = feeItem.Clone();
            FS.HISFC.Models.FeeStuff.Output outItem = this.fpSpread1_Sheet2.Rows[rowIndex].Tag as FS.HISFC.Models.FeeStuff.Output;
            List<FS.HISFC.Models.FeeStuff.Output> list = new List<FS.HISFC.Models.FeeStuff.Output>();
            list.Add(outItem);
            f.MateList = list;
            int mateListIndex = 0;
            if (this.ckbAllQuit.Checked)
            {
                if (f.NoBackQty <= 0)
                {
                    MessageBox.Show(f.Item.Name + "�Ѿ�û�п����������������˷�!");

                    return -2;
                }
                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                //{4D6501CB-D2A4-4204-8CBA-F34F28D5300A}
                FeeItemList fClone = f.Clone();
                f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? outItem.StoreBase.Item.Qty * f.Item.PackQty : outItem.StoreBase.Item.Qty);
                f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? outItem.StoreBase.Item.Qty * f.Item.PackQty : outItem.StoreBase.Item.Qty);
                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                f.FT.OwnCost = f.FT.TotCost;
                if (findRow == -1)
                {
                    findRow = FindNullRow(this.fpSpread2_Sheet2);
                    //{4D6501CB-D2A4-4204-8CBA-F34F28D5300A}
                    //FeeItemList fClone = f.Clone();
                    fClone.Item.Qty = (f.FeePack == "1" ? outItem.StoreBase.Item.Qty * f.Item.PackQty : outItem.StoreBase.Item.Qty);
                    this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                }
                else //�ҵ����ۼ�����
                {
                    FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty = fFind.Item.Qty + (f.FeePack == "1" ? outItem.StoreBase.Item.Qty * f.Item.PackQty : outItem.StoreBase.Item.Qty);
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                    //�����˷ѷ��õ�������Ϣ
                    this.SetFeeItemList(fFind, outItem.Clone(), true);
                }

                f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;

                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                this.fpSpread1_Sheet2.Cells[undrugIndex, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[undrugIndex, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                this.fpSpread1_Sheet2.Cells[undrugIndex, (int)UndrugList.NoBackQty].Text = f.NoBackQty.ToString();
                this.fpSpread1_Sheet2.Cells[rowIndex, (int)UndrugList.NoBackQty].Text = "0";
                this.fpSpread1_Sheet2.Cells[rowIndex, (int)UndrugList.Amount].Text = "0";
                this.fpSpread1_Sheet2.Cells[rowIndex, (int)UndrugList.Cost].Text = "0";
                //����Ϊ�˷ѷ��õ�������Ϣ
                feeItem.Item.Qty = f.Item.Qty;
                feeItem.FT.OwnCost = f.FT.OwnCost;
                feeItem.FT.PubCost = f.FT.PubCost;
                feeItem.FT.PayCost = f.FT.PayCost;
                feeItem.FT.TotCost = f.FT.TotCost;
                feeItem.NoBackQty = f.NoBackQty;
                this.SetFeeItemList(feeItem, outItem, false, ref mateListIndex);
                this.fpSpread1_Sheet2.Rows[rowIndex].Tag = feeItem.MateList[mateListIndex];

            }
            else
            {
                txtReturnItemName.Text = outItem.StoreBase.Item.Name;
                txtReturnNum.Tag = f;
                txtRetSpecs.Text = outItem.StoreBase.Item.Specs;
                this.backType = string.Empty;
                txtReturnNum.Select();
                txtReturnNum.Focus();
            }
            ComputCost();
            return 1;

        }

        /// <summary>
        /// ����FeeItemList�е�������Ϣ
        /// </summary>
        /// <param name="f">������Ϣ</param>
        /// <param name="outItem">���ʳ���</param>
        /// <param name="isQuiteOperation">�Ƿ����˷Ѳ���</param>
        //{143CA424-7AF9-493a-8601-2F7B1D635026}
        protected virtual void SetFeeItemList(FeeItemList f, FS.HISFC.Models.FeeStuff.Output outItem, bool isQuiteOperation)
        {
            if (f.MateList.Count == 0)
            {
                f.MateList.Add(outItem);
                return;
            }
            bool isFind = false;
            foreach (FS.HISFC.Models.FeeStuff.Output item in f.MateList)
            {
                if (item.ID == outItem.ID && item.StoreBase.StockNO == outItem.StoreBase.StockNO)
                {
                    isFind = true;
                    if (isQuiteOperation)
                    {
                        item.StoreBase.Item.Qty += outItem.StoreBase.Item.Qty;

                    }
                    else
                    {
                        item.StoreBase.Item.Qty -= outItem.StoreBase.Item.Qty;
                    }
                    return;
                }
            }
            if (!isFind)
            {
                f.MateList.Add(outItem);
                return;
            }
        }

        /// <summary>
        /// ����FeeItemList�е�������Ϣ
        /// </summary>
        /// <param name="f">������Ϣ</param>
        /// <param name="outItem">���ʳ���</param>
        /// <param name="isQuiteOperation">�Ƿ����˷Ѳ���</param>
        //{143CA424-7AF9-493a-8601-2F7B1D635026}
        protected virtual void SetFeeItemList(FeeItemList f, FS.HISFC.Models.FeeStuff.Output outItem, bool isQuiteOperation, ref int mateListindex)
        {
            mateListindex = 0;
            if (f.MateList.Count == 0)
            {
                f.MateList.Add(outItem);
                return;
            }
            bool isFind = false;
            FS.HISFC.Models.FeeStuff.Output item = null;
            for (int i = 0; i < f.MateList.Count; i++)
            {
                item = f.MateList[i];
                if (item.ID == outItem.ID && item.StoreBase.StockNO == outItem.StoreBase.StockNO)
                {
                    isFind = true;
                    if (isQuiteOperation)
                    {
                        item.StoreBase.Item.Qty += outItem.StoreBase.Item.Qty;
                    }
                    else
                    {
                        item.StoreBase.Item.Qty -= outItem.StoreBase.Item.Qty;
                    }
                    mateListindex = i;
                    return;
                }
            }
            if (!isFind)
            {
                f.MateList.Add(outItem);
                mateListindex = f.MateList.Count - 1;
                return;
            }
        }

        /// <summary>
        /// ���������˷�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QuitItemByNum()
        {
            if (this.txtReturnNum.Tag == null)
            {
                MessageBox.Show("��ѡ����Ŀ!");

                return -1;
            }
            decimal quitQty = 0;
            try
            {
                quitQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtReturnNum.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������벻�Ϸ�!" + ex.Message);
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }
            if (quitQty == 0)
            {
                MessageBox.Show("��������Ϊ��");
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }
            if (quitQty < 0)
            {
                MessageBox.Show("��������ΪС����");
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }

            object objQuit = this.txtReturnNum.Tag;

            #region TagΪ������Ŀʱ

            if (objQuit is FeeItemList)
            {
                FeeItemList f = objQuit as FeeItemList;
                //{143CA424-7AF9-493a-8601-2F7B1D635027}
                if (f.MateList.Count > 0)
                {
                    if (quitQty > f.MateList[0].StoreBase.Item.Qty)
                    {
                        MessageBox.Show("������������ڿ�������!");
                        this.txtReturnNum.SelectAll();
                        this.txtReturnNum.Focus();
                        return -1;
                    }
                }
                if (f.FeePack == "1")//��װ��λ
                {
                    if (quitQty > f.NoBackQty / f.Item.PackQty)
                    {
                        MessageBox.Show("������������ڿ�������!");
                        this.txtReturnNum.SelectAll();
                        this.txtReturnNum.Focus();

                        return -1;
                    }
                }
                else
                {
                    if (quitQty > f.NoBackQty)
                    {
                        MessageBox.Show("������������ڿ�������!");
                        this.txtReturnNum.SelectAll();
                        this.txtReturnNum.Focus();

                        return -1;
                    }
                }
                int currRow = 0;
                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);
                    if (currRow == -1)
                    {
                        MessageBox.Show("����ҩƷʧ�ܣ�");

                        return -1;
                    }
                    if (f.Item.SysClass.ID.ToString() == "PCC")
                    {
                        decimal doseOnce = (f.NoBackQty - quitQty) / f.Days;

                        (this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList).Order.DoseOnce = doseOnce;

                        this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.DoseAndDays].Text = "ÿ����:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "����:" + f.Days.ToString();
                    }
                    else
                    {
                        //{A04F2877-2E51-4d6b-BA7C-6D4010EA4A00}
                        //ҩƷ���ǲ�ҩ�˷Ѳ���������С��
                        if (f.Item.SysClass.ID.ToString() != "PCC")
                        {
                            int intQty = FS.FrameWork.Function.NConvert.ToInt32(quitQty);
                            if (quitQty > intQty)
                            {
                                MessageBox.Show("ҩƷ�˷Ѳ���������С��,���������룡");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();
                                return -1;
                            }
                        }
                    }
                }
                else
                {
                    currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);
                    if (currRow == -1)
                    {
                        MessageBox.Show("���ҷ�ҩƷʧ�ܣ�");

                        return -1;
                    }
                }

                f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty);
                f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty);
                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                f.FT.OwnCost = f.FT.TotCost;

                //if (f.Item.IsPharmacy)//ҩƷ
                if (f.Item.ItemType == EnumItemType.Drug)//ҩƷ
                {
                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                    //û���ҵ�����ô����һ��;
                    if (findRow == -1)
                    {
                        findRow = FindNullRow(this.fpSpread2_Sheet1);

                        FeeItemList fClone = f.Clone();
                        fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty : quitQty;

                        this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fClone.Item.Specs;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                        //��ʾҩƷ���ۣ���ʾ���������в���
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();

                    }
                    else //�ҵ����ۼ�����
                    {
                        FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                        fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty : quitQty);
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                        //��ʾҩƷ���ۣ���ʾ���������в���
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                    }

                    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
                }
                else //��ҩƷ
                {
                    FS.HISFC.Models.FeeStuff.Output outItem = null;

                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                    //û���ҵ�����ô����һ��;
                    if (findRow == -1)
                    {
                        findRow = FindNullRow(this.fpSpread2_Sheet2);

                        FeeItemList fClone = f.Clone();
                        fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty : quitQty;

                        //fClone.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty);
                        fClone.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2);
                        fClone.FT.OwnCost = fClone.FT.TotCost;

                        this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                        //{143CA424-7AF9-493a-8601-2F7B1D635026}
                        //�����շ�
                        if (fClone.MateList.Count > 0)
                        {
                            outItem = fClone.MateList[0];
                            outItem.StoreBase.Item.Qty = quitQty;
                        }

                    }
                    else //�ҵ����ۼ�����
                    {
                        FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                        fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty : quitQty);
                        fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2);
                        fFind.FT.OwnCost = fFind.FT.TotCost;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                        //{143CA424-7AF9-493a-8601-2F7B1D635026}
                        //�����շ�
                        if (f.MateList.Count > 0)
                        {
                            FS.HISFC.Models.FeeStuff.Output tempoutItem = f.MateList[0].Clone();
                            tempoutItem.StoreBase.Item.Qty = quitQty;
                            this.SetFeeItemList(fFind, tempoutItem, true);
                        }
                    }


                    this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                    this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
                    //��������
                    //{143CA424-7AF9-493a-8601-2F7B1D635026}
                    FeeItemList tempItemList = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;
                    tempItemList.Item.Qty = f.Item.Qty;
                    tempItemList.NoBackQty = f.NoBackQty;
                    tempItemList.FT.TotCost = f.FT.TotCost;
                    tempItemList.FT.OwnCost = f.FT.OwnCost;

                    if (f.MateList.Count > 0)
                    {
                        outItem = f.MateList[0].Clone();

                        outItem.StoreBase.Item.Qty = quitQty;

                        int matelistIndex = 0;
                        this.SetFeeItemList(tempItemList, outItem, false, ref matelistIndex);

                        int mateRow = GetMateRowIndex(outItem);
                        outItem = tempItemList.MateList[matelistIndex];

                        this.fpSpread1_Sheet2.Cells[mateRow, (int)UndrugList.Amount].Text = outItem.StoreBase.Item.Qty.ToString();
                        this.fpSpread1_Sheet2.Cells[mateRow, (int)UndrugList.Cost].Text = (outItem.StoreBase.Item.Qty * outItem.StoreBase.Item.Price).ToString();
                        this.fpSpread1_Sheet2.Cells[mateRow, (int)UndrugList.NoBackQty].Text = outItem.StoreBase.Item.Qty.ToString();

                        this.fpSpread1_Sheet2.Rows[mateRow].Tag = outItem;
                    }
                }

            }

            #endregion

            else if (objQuit is ArrayList)
            {
                ArrayList alTemp = objQuit as ArrayList;

                if (this.backType == "PACKAGE")
                {
                    foreach (FeeItemList item in alTemp)
                    {
                        FS.HISFC.Models.Fee.Item.UndrugComb info = null;

                        foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugComb in this.currentUndrugCombs)
                        {
                            if (undrugComb.ID == item.ID)
                            {
                                info = undrugComb;

                                break;
                            }
                        }

                        if (info == null)
                        {
                            MessageBox.Show("��ά����������û��" + item.Item.Name + "��ִ��ȫ��");

                            return -1;
                        }

                        #region ������ϸ

                        FeeItemList f = item;
                        if (f.FeePack == "1")//��װ��λ
                        {
                            if (quitQty * info.Qty > f.NoBackQty / f.Item.PackQty)
                            {
                                MessageBox.Show("������������ڿ�������!");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();

                                return -1;
                            }
                        }
                        else
                        {
                            if (quitQty * info.Qty > f.NoBackQty)
                            {
                                MessageBox.Show("������������ڿ�������!");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();

                                return -1;
                            }
                        }
                        int currRow = 0;
                        //if (!f.Item.IsPharmacy)
                        if (f.Item.ItemType != EnumItemType.Drug)
                        {
                            currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);
                            if (currRow == -1)
                            {
                                MessageBox.Show("���ҷ�ҩƷʧ�ܣ�");

                                return -1;
                            }
                        }

                        f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * info.Qty : quitQty * info.Qty);
                        f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * info.Qty : quitQty * info.Qty);
                        f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                        //if (!f.Item.IsPharmacy) //��ҩƷ
                        if (f.Item.ItemType != EnumItemType.Drug)
                        {
                            int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                            //û���ҵ�����ô����һ��;
                            if (findRow == -1)
                            {
                                findRow = FindNullRow(this.fpSpread2_Sheet2);

                                FeeItemList fClone = f.Clone();
                                fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty * info.Qty : quitQty * info.Qty;

                                this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                            }
                            else //�ҵ����ۼ�����
                            {
                                FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                                fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty * info.Qty : quitQty * info.Qty);
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                            }

                            this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                            this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                            this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
                        }

                        #endregion
                    }
                }
                if (this.backType == "PCC")
                {
                    foreach (FeeItemList item in alTemp)
                    {
                        #region ������ϸ

                        FeeItemList f = item;
                        if (f.FeePack == "1")//��װ��λ
                        {
                            if (quitQty * f.Order.DoseOnce > f.NoBackQty / f.Item.PackQty)
                            {
                                MessageBox.Show("������������ڿ�������!");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();

                                return -1;
                            }
                        }
                        else
                        {
                            if (quitQty * f.Order.DoseOnce > f.NoBackQty)
                            {
                                MessageBox.Show("������������ڿ�������!");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();

                                return -1;
                            }
                        }
                        int currRow = 0;
                        //if (f.Item.IsPharmacy)
                        if (f.Item.ItemType == EnumItemType.Drug)
                        {
                            currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);
                            if (currRow == -1)
                            {
                                MessageBox.Show("����ҩƷʧ�ܣ�");

                                return -1;
                            }
                        }

                        f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce);
                        f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce);
                        f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                        //if (f.Item.IsPharmacy) //��ҩƷ
                        if (f.Item.ItemType == EnumItemType.Drug) //��ҩƷ
                        {
                            int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                            //û���ҵ�����ô����һ��;
                            if (findRow == -1)
                            {
                                findRow = FindNullRow(this.fpSpread2_Sheet1);

                                FeeItemList fClone = f.Clone();
                                fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce;

                                this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                                //��ʾҩƷ���ۣ���ʾ���������в���
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();
                            }
                            else //�ҵ����ۼ�����
                            {
                                FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
                                fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty * fFind.Order.DoseOnce : quitQty * fFind.Order.DoseOnce);
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                //��ʾҩƷ���ۣ���ʾ���������в���
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                            }

                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
                        }

                        #endregion
                    }
                }
            }

            this.fpSpread1.Select();
            this.fpSpread1.Focus();
            if (this.fpSpread1.ActiveSheet.RowCount > 0)
            {
                this.fpSpread1.ActiveSheet.ActiveRowIndex = 0;
            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// ������������fp�е���
        /// </summary>
        /// <param name="outItem">���ʳ�����Ϣ</param>
        /// <returns>-1  ʧ��</returns>
        protected virtual int GetMateRowIndex(FS.HISFC.Models.FeeStuff.Output outItem)
        {
            string headText = string.Empty;
            FS.HISFC.Models.FeeStuff.Output tempOut = null;
            for (int i = 0; i < this.fpSpread1_Sheet2.Rows.Count; i++)
            {
                headText = this.fpSpread1_Sheet2.RowHeader.Cells[i, 0].Text;
                if (headText != ".") continue;
                tempOut = this.fpSpread1_Sheet2.Rows[i].Tag as FS.HISFC.Models.FeeStuff.Output;
                if (tempOut.StoreBase.StockNO == outItem.StoreBase.StockNO &&
                    tempOut.ID == outItem.ID)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// ����ȡ���˷Ѳ���
        /// </summary>
        protected virtual void DealCancelQuitOperation()
        {

            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet2)//��ҩƷ
            {
                int currRow = this.fpSpread2_Sheet2.ActiveRowIndex;

                if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread2_Sheet2.Rows[currRow].Tag as FeeItemList;
                    if (f.UndrugComb.ID != string.Empty && isNeedAllQuit)
                    {
                        for (int i = 0; i < this.fpSpread2_Sheet2.RowCount; i++)
                        {
                            if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FeeItemList)
                            {
                                FeeItemList fTemp = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;
                                if (fTemp != null && fTemp.UndrugComb.ID == f.UndrugComb.ID && f.Order.ID == fTemp.Order.ID)
                                {
                                    CancelUndrugQuitOperation(i);
                                }
                            }
                        }
                        return;
                    }
                    else
                    {
                        CancelQuitOperation();
                    }
                }
            }
            else
            {
                if (isPharmacySameRecipeQuitAll == false)
                {
                    int currRow = this.fpSpread2_Sheet1.ActiveRowIndex;

                    if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FeeItemList;

                        if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.Combo.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            for (int i = 0; i < this.fpSpread2_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread2_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread2_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                    {
                                        CancelQuitDrugOperation(i);
                                    }
                                }
                            }
                        }
                        else
                        {
                            CancelQuitOperation();
                        }
                    }
                }
                else
                {
                    int currRow = this.fpSpread2_Sheet1.ActiveRowIndex;

                    if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FeeItemList;

                        if (true)
                        {
                            for (int i = 0; i < this.fpSpread2_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread2_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread2_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.RecipeNO == f.RecipeNO)
                                    {
                                        CancelQuitDrugOperation(i);
                                    }
                                }
                            }
                        }
                        else
                        {
                            CancelQuitOperation();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ����ȡ�����˷�ҩƷ����
        /// </summary>
        /// <param name="currRow">��ǰ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int CancelUndrugQuitOperation(int currRow)
        {
            if (this.fpSpread2_Sheet2.Rows[currRow].Tag == null)
            {
                return -1;
            }
            if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply)
            {
                MessageBox.Show("�Ѿ���׼��ҩƷ����ȡ��!");

                return -1;
            }
            if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread2_Sheet2.Rows[currRow].Tag as FeeItemList;

                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);

                if (findRow == -1)
                {
                    MessageBox.Show("����δ�˷�ҩƷʧ��!");

                    return -1;
                }
                FeeItemList fFind = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;
                fFind.Item.Qty += f.Item.Qty;
                fFind.NoBackQty += f.Item.Qty;
                fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2);
                this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Amount].Text = fFind.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Cost].Text = fFind.FT.TotCost.ToString();
                this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                f.Item.Qty = 0;
                this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.Amount].Text = string.Empty;
                this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.Flag].Text = string.Empty;
                this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.ItemName].Text = string.Empty;
                this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.PriceUnit].Text = string.Empty;
            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// ����ҩƷȡ���˷�
        /// </summary>
        /// <param name="currRow">��ǰ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int CancelQuitDrugOperation(int currRow)
        {
            if (this.fpSpread2_Sheet1.Rows[currRow].Tag == null)
            {
                return -1;
            }
            if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply)
            {
                MessageBox.Show("�Ѿ���׼ҩƷ����ȡ��!");

                return -1;
            }
            if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FeeItemList;

                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);

                if (findRow == -1)
                {
                    MessageBox.Show("����δ��ҩƷʧ��!");

                    return -1;
                }
                FeeItemList fFind = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;
                fFind.Item.Qty += f.Item.Qty;
                fFind.NoBackQty += f.Item.Qty;
                fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2);
                fFind.FT.OwnCost = fFind.FT.TotCost;
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = fFind.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = fFind.FT.TotCost.ToString();
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                f.Item.Qty = 0;

                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.ItemName].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.PriceUnit].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Specs].Text = string.Empty;
                //��ʾ���ۣ����Ϊ�գ�
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Price].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Cost].Text = string.Empty;
            }

            ComputCost();

            return 0;
        }

        /// <summary>
        /// ȡ���˷Ѳ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int CancelQuitOperation()
        {
            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet1)//ҩƷ
            {
                int currRow = this.fpSpread2_Sheet1.ActiveRowIndex;

                if (this.fpSpread2_Sheet1.Rows[currRow].Tag == null)
                {
                    return -1;
                }
                if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply)
                {
                    MessageBox.Show("�Ѿ���׼ҩƷ����ȡ��!");

                    return -1;
                }
                if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FeeItemList;

                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);

                    if (findRow == -1)
                    {
                        MessageBox.Show("����δ��ҩƷʧ��!");

                        return -1;
                    }
                    FeeItemList fFind = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty += f.Item.Qty;
                    fFind.NoBackQty += f.Item.Qty;
                    fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2) - fFind.FT.RebateCost;
                    fFind.FT.OwnCost = fFind.FT.TotCost;
                    //fFind.FT.TotCost += f.FT.TotCost;
                    //fFind.FT.PubCost += f.FT.PubCost;
                    //fFind.FT.PayCost += f.FT.PayCost;
                    //fFind.FT.OwnCost += f.FT.OwnCost;

                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = fFind.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                    f.Item.Qty = 0;
                    if (f.Item.SysClass.ID.ToString() == "PCC")
                    {
                        decimal doseOnce = (fFind.NoBackQty) / fFind.Days;

                        (this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList).Order.DoseOnce = doseOnce;

                        this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.DoseAndDays].Text = "ÿ����:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "����:" + f.Days.ToString();
                    }
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = "0";
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.ItemName].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.PriceUnit].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Specs].Text = string.Empty;
                    //��ʾ���ۣ����Ϊ�գ�
                   this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Price].Text = string.Empty;
                   this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Cost].Text = string.Empty;
                    //{433AA56A-264F-4c8c-BC7E-52DAEAFDC605}
                    this.fpSpread2_Sheet1.Rows[currRow].Tag = null;
                    this.fpSpread1_Sheet1.Rows[findRow].Tag = fFind;
                }
            }
            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet2)//��ҩƷ
            {
                int currRow = this.fpSpread2_Sheet2.ActiveRowIndex;

                if (this.fpSpread2_Sheet2.Rows[currRow].Tag == null)
                {
                    return -1;
                }
                if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply)
                {
                    MessageBox.Show("�Ѿ���׼��ҩƷ����ȡ��!");

                    return -1;
                }
                if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread2_Sheet2.Rows[currRow].Tag as FeeItemList;

                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);

                    if (findRow == -1)
                    {
                        MessageBox.Show("����δ�˷�ҩƷʧ��!");

                        return -1;
                    }
                    FeeItemList fFind = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty += f.Item.Qty;
                    fFind.NoBackQty += f.Item.Qty;
                    fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2) - fFind.FT.RebateCost;
                    fFind.FT.OwnCost = fFind.FT.TotCost;
                    //fFind.FT.TotCost += f.FT.TotCost;
                    //fFind.FT.PubCost += f.FT.PubCost;
                    //fFind.FT.PayCost += f.FT.PayCost;
                    //fFind.FT.OwnCost += f.FT.OwnCost; 
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Cost].Text = fFind.FT.TotCost.ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                    f.Item.Qty = 0;
                    //this.fpSpread2_Sheet2.Cells[currRow,(int)UndrugListQuit.Item.Qty].Text = "0";
                    this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.Amount].Text = string.Empty;
                    this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.Flag].Text = string.Empty;
                    this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.ItemName].Text = string.Empty;
                    this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.PriceUnit].Text = string.Empty;
                    this.fpSpread2_Sheet2.Rows[currRow].Tag = null;
                    #region ����
                    //{143CA424-7AF9-493a-8601-2F7B1D635026}
                    int mateListIndex = 0;
                    if (f.MateList.Count > 0)
                    {
                        int mateIndex = 0;
                        foreach (FS.HISFC.Models.FeeStuff.Output outItem in f.MateList)
                        {
                            mateIndex = this.GetMateRowIndex(outItem);
                            if (mateIndex == -1)
                            {
                                MessageBox.Show("����������Ϣʧ�ܣ�");
                                return -1;
                            }
                            this.SetFeeItemList(fFind, outItem, true, ref mateListIndex);

                            this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Amount].Text = fFind.MateList[mateListIndex].StoreBase.Item.Qty.ToString();
                            this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Cost].Text = (fFind.MateList[mateListIndex].StoreBase.Item.Qty * fFind.MateList[mateListIndex].StoreBase.Item.Price).ToString();
                            this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.NoBackQty].Text = fFind.MateList[mateListIndex].StoreBase.Item.Qty.ToString();
                            this.fpSpread1_Sheet2.Rows[mateIndex].Tag = fFind.MateList[mateListIndex];
                        }
                    }
                    #endregion
                }
            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual bool IsValid()
        {
            if (this.quitInvoices == null || this.quitInvoices.Count == 0)
            {
                MessageBox.Show("�����뷢Ʊ��Ϣ");

                return false;
            }

            if (!IsQuitItem())
            {
                MessageBox.Show("��ѡ���˷���Ŀ!");

                return false;
            }

            bool isCanQuitOtherOper = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_OTHER_OPER_INVOICE, true, false);
            if (!isCanQuitOtherOper)//���������˷�
            {
                Balance tmpInvoice = quitInvoices[0] as Balance;
                if (tmpInvoice == null)
                {
                    MessageBox.Show("��Ʊ��ʽת������!");
                    this.tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }

                if (tmpInvoice.BalanceOper.ID != this.outpatientManager.Operator.ID)
                {
                    MessageBox.Show("�÷�ƱΪ����Ա" + tmpInvoice.BalanceOper.ID + "��ȡ,��û��Ȩ�޽��ش�!");
                    tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }
            }

            bool isCanQuitDayBalanced = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_DAYBALANCED_INVOICE, true, false);
            if (!isCanQuitDayBalanced)//�������˷��ս�����
            {
                Balance tmpInvoice = quitInvoices[0] as Balance;
                if (tmpInvoice == null)
                {
                    MessageBox.Show("��Ʊ��ʽת������!");
                    this.tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }

                if (tmpInvoice.IsDayBalanced)
                {
                    MessageBox.Show("�÷�Ʊ�Ѿ��ս�,���ܽ����˷�!");
                    tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }
            }

            Balance invoice = quitInvoices[0] as Balance;
            if (invoice != null && invoice.IsAccount)
            {
                if (!IsAllQuit() && !this.isAllowQuitFeeHalf)
                {
                    MessageBox.Show("�˻����д�ӡ�ķ�Ʊ����ȫ�ˣ�");

                    return false;
                }
            }

            if (this.patient.Pact.PayKind.ID == "02")//ҽ��������Ҫȫ��!
            {
                bool isSICanHalfQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.SI_CAN_HALF_QUIT, false, false);
                if (!isSICanHalfQuit)
                {
                    if (!IsAllQuit())
                    {
                        MessageBox.Show("���ѻ���ҽ������Ҫ��ȫ��!����δ�˹�ķ���!");

                        return false;
                    }
                }
            }
            if (this.patient.Pact.PayKind.ID == "03")//���ѻ�����Ҫȫ��!
            {
                string tmpControl = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.PUB_CAN_HALF_QUIT, "0");
                if (tmpControl == "0")
                {
                    if (!IsAllQuit())
                    {
                        MessageBox.Show("���ѻ���ҽ������Ҫ��ȫ��!����δ�˹�ķ���!");

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// �Ƿ���Ŀȫ��
        /// </summary>
        /// <returns>�ɹ� true ʧ�� false</returns>
        protected virtual bool IsQuitItem()
        {
            decimal qty = 0;

            for (int i = 0; i < this.fpSpread2_Sheet1.RowCount; i++)
            {
                if (this.fpSpread2_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread2_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList fTemp = this.fpSpread2_Sheet1.Rows[i].Tag as FeeItemList;

                        qty += fTemp.Item.Qty;
                    }
                }
                if (this.fpSpread2_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                {
                    FS.HISFC.Models.Fee.ReturnApply fTemp = this.fpSpread2_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;

                    qty += fTemp.Item.Qty;
                }
            }
            for (int i = 0; i < this.fpSpread2_Sheet2.RowCount; i++)
            {
                if (this.fpSpread2_Sheet2.Rows[i].Tag != null)
                {
                    if (this.fpSpread2_Sheet2.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList fTemp = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;

                        qty += fTemp.Item.Qty;
                    }
                }
                if (this.fpSpread2_Sheet2.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                {
                    FS.HISFC.Models.Fee.ReturnApply fTemp = this.fpSpread2_Sheet2.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;

                    qty += fTemp.Item.Qty;
                }
            }
            if (qty > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �Ƿ�ȫ��
        /// </summary>
        /// <returns>�ɹ�true ʧ�� false</returns>
        protected virtual bool IsAllQuit()
        {
            decimal qty = 0;

            FeeItemList fTemp = null;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                    qty += fTemp.Item.Qty;
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;

                    qty += fTemp.Item.Qty;
                }
            }
            if (qty > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        
        /// <summary>
        /// �Ƿ��˻���ʱ��Ʊ
        /// {69245A77-FB7A-42ed-844B-855E7ABC612F}
        /// </summary>
        bool blnIsAccountInvoice = false;

        /// <summary>
        /// �����˷�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Save()
        {
            DialogResult result = MessageBox.Show("�Ƿ�Ҫ�˷�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return -1;
            }

            if (this.oldPatient.Pact.PayKind.ID == "03" && this.IsCanModifyPatientInfo)
            {
                this.patient.Pact = this.oldPatient.Pact.Clone();
                this.patient.SSN = this.oldPatient.SSN;
                this.patient.LSH = this.oldPatient.LSH;
                this.patient.User03 = this.oldPatient.User03;
            }

            //�ж���Ч��
            if (!IsValid())
            {
                return -1;
            }

            long returnValue = 0;//����ֵ,��Ҫ��ҽ����

            this.medcareInterfaceProxy.SetPactCode(this.patient.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            int iReturn = 0;

            //��ø���Ʊ��ˮ��
            string invoiceSeqNegative = outpatientManager.GetInvoiceCombNO();
            if (invoiceSeqNegative == null || invoiceSeqNegative == string.Empty)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err);

                return -1;
            }
            #region ��¼���Ϸ�Ʊ�Ľ��
            decimal CancelTotCost = 0; //���Ϸ�Ʊ���ܽ��
            decimal CancelOwnCost = 0;//���Ϸ�Ʊ���Էѽ��
            decimal CancelPayCost = 0;//���Ϸ�Ʊ���Ը����
            decimal CancelPubCost = 0;//���Ϸ�Ʊ�Ĺ��ѽ��
            decimal CancelRebateCost = 0; // �����Żݼ�����
            string InvoiceNO = "";
            #endregion

            // {69245A77-FB7A-42ed-844B-855E7ABC612F}
            blnIsAccountInvoice = false;

            //�˷ѱ��
            FS.HISFC.Models.Base.CancelTypes cancelType = CancelTypes.Canceled;

            //Ϊ�˴���Ʊ������Ʊ��ϸ������ {BB77678F-A3E1-4f62-9D8D-8D52C1C17F8B}
            ArrayList alInvoiceDetails = new ArrayList();

            
            foreach (Balance invoice in this.quitInvoices)
            {
                // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                blnIsAccountInvoice = invoice.IsAccount;

                #region ��Ʊ������

                InvoiceNO = invoice.Invoice.ID;
                //����ǵ�ǰ����Ա����û���ս� ��Ϊ����
                if (isUseLogout && invoice.IsDayBalanced == false && invoice.BalanceOper.ID.Equals(outpatientManager.Operator.ID))
                {
                    cancelType = FS.HISFC.Models.Base.CancelTypes.LogOut;
                }

                iReturn = outpatientManager.UpdateBalanceCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, cancelType);
                if (iReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ԭʼ��Ʊ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
                if (iReturn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�÷�Ʊ�Ѿ�����!");

                    return -1;
                }

                //���븺��¼����
                Balance invoClone = invoice.Clone();

                CancelTotCost += invoClone.FT.TotCost;
                CancelOwnCost += invoClone.FT.OwnCost;
                CancelPayCost += invoClone.FT.PayCost;
                CancelPubCost += invoClone.FT.PubCost;

                invoClone.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                invoClone.FT.TotCost = -invoClone.FT.TotCost;
                invoClone.FT.OwnCost = -invoClone.FT.OwnCost;
                invoClone.FT.PayCost = -invoClone.FT.PayCost;
                invoClone.FT.PubCost = -invoClone.FT.PubCost;
                invoClone.CancelType = cancelType;
                
                invoClone.CanceledInvoiceNO = invoice.ID;
                invoClone.CancelOper.ID = outpatientManager.Operator.ID;
                invoClone.BalanceOper.ID = outpatientManager.Operator.ID;//�ս���Ҫ ��Ϊ��ǰ�˷���
                invoClone.BalanceOper.OperTime = nowTime;
                invoClone.CancelOper.OperTime = nowTime;
                invoClone.IsAuditing = false;
                invoClone.AuditingOper.ID = string.Empty;
                invoClone.AuditingOper.OperTime = DateTime.MinValue;
                invoClone.IsDayBalanced = false;
                invoClone.BalanceID = string.Empty;
                invoClone.DayBalanceOper.OperTime = DateTime.MinValue;

                invoClone.CombNO = invoiceSeqNegative;

                iReturn = outpatientManager.InsertBalance(invoClone);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���뷢Ʊ������Ϣ����!!" + outpatientManager.Err);

                    return -1;
                }
                #endregion

                #region ��Ʊ��ϸ��Ϣ����
                //����Ʊ��ϸ����Ϣ
                ArrayList alInvoiceDetail = outpatientManager.QueryBalanceListsByInvoiceNOAndInvoiceSequence(invoice.Invoice.ID, invoice.CombNO);
                if (alInvoiceDetail == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��÷�Ʊ��ϸ����!" + outpatientManager.Err);

                    return -1;
                }
                

                //���Ϸ�Ʊ��ϸ����Ϣ
                iReturn = outpatientManager.UpdateBalanceListCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, cancelType);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���Ϸ�Ʊ��ϸ����!" + outpatientManager.Err);

                    return -1;
                }
                
                foreach (BalanceList d in alInvoiceDetail)
                {
                    d.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    d.BalanceBase.FT.OwnCost = -d.BalanceBase.FT.OwnCost;
                    d.BalanceBase.FT.PubCost = -d.BalanceBase.FT.PubCost;
                    d.BalanceBase.FT.PayCost = -d.BalanceBase.FT.PayCost;
                    d.BalanceBase.BalanceOper.OperTime = nowTime;
                    d.BalanceBase.BalanceOper.ID = outpatientManager.Operator.ID;
                    d.BalanceBase.CancelType = cancelType;
                    d.BalanceBase.IsDayBalanced = false;
                    d.BalanceBase.DayBalanceOper.ID = string.Empty;
                    d.BalanceBase.DayBalanceOper.OperTime = DateTime.MinValue;
                    //d.CombNO = invoiceSeqNegative;
                    ((Balance)d.BalanceBase).CombNO = invoiceSeqNegative;

                    iReturn = outpatientManager.InsertBalanceList(d);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���뷢Ʊ��ϸ������Ϣ����!" + outpatientManager.Err);

                        return -1;
                    }
                }
                #endregion

                //Ϊ�˴���Ʊ������Ʊ��ϸ������ {D5FA97FA-8DBB-48e7-BF5B-8DF4049EEE2B}
                alInvoiceDetails.Add(alInvoiceDetail);
            }

            Balance invoiceInfo = ((Balance)quitInvoices[0]);

            #region ����֧����Ϣ
            ArrayList payList = new ArrayList();
            string choosePayMode = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.QUIT_PAY_MODE_SELECT, "1");
            ArrayList feePayMods = this.outpatientManager.QueryBalancePaysByInvoiceSequence(invoiceInfo.CombNO);

            if (feePayMods.Count >= 0)
            {
                // {4E2CD49B-1D3B-43cb-A931-C5503A68373C}
                //if (!this.IsQuitSamePayMod || invoiceInfo.IsAccount)
                if (!this.IsQuitSamePayMod)
                {
                    CancelRebateCost = 0;
                    int paySquence = 100;
                    BalancePay objPay = new BalancePay();

                    #region ���ڼ��⡢���ʻ��ߣ�������⡢��������
                    //��Ӳ��������Ƿ񱣴�ҽ�������Ը�����
                    foreach (BalancePay payRebat in feePayMods)
                    {
                        if (payRebat.PayType.ID == "RC" || payRebat.PayType.ID == "JZ"||payRebat.PayType.ID=="PB"||(isSavePYfee && payRebat.PayType.ID=="PY"))
                        {

                            paySquence--;

                            // ���⡢�������ݴ���
                            if (payRebat.PayType.ID == "RC" || payRebat.PayType.ID == "JZ")
                            {
                                CancelRebateCost += payRebat.FT.TotCost;
                            }

                            objPay = payRebat.Clone();
                            objPay.TransType = TransTypes.Negative;
                            objPay.FT.TotCost = -objPay.FT.TotCost;
                            objPay.FT.RealCost = -objPay.FT.RealCost;
                            objPay.FT.OwnCost = -objPay.FT.OwnCost;
                            objPay.InputOper.OperTime = nowTime;
                            objPay.Invoice.ID = InvoiceNO;
                            objPay.Squence = paySquence.ToString();

                            objPay.InputOper.ID = outpatientManager.Operator.ID;
                            objPay.InvoiceCombNO = invoiceSeqNegative;
                            objPay.CancelType = cancelType;
                            objPay.IsChecked = false;
                            objPay.CheckOper.ID = string.Empty;
                            objPay.CheckOper.OperTime = DateTime.MinValue;
                            objPay.BalanceOper.ID = string.Empty;
                            objPay.IsDayBalanced = false;
                            objPay.IsAuditing = false;
                            objPay.AuditingOper.OperTime = DateTime.MinValue;
                            objPay.AuditingOper.ID = string.Empty;
                            iReturn = outpatientManager.InsertBalancePay(objPay);
                            if (iReturn <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����֧������Ϣ����!" + outpatientManager.Err);

                                return -1;
                            }
                        }
                    }

                    #endregion

                    if (CancelRebateCost - CancelOwnCost - CancelPayCost < 0)
                    {
                        paySquence--;

                        // {B176923A-5C7E-46a9-A4C6-ED6313ACC4E5}
                        // �Ƿ�����ԭ֧����ʽ�˷� false:������ true:����
                        #region ԭ����
                        //ArrayList payList = new ArrayList();
                        objPay = new BalancePay();
                        objPay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                        objPay.FT.TotCost = -(CancelPayCost + CancelOwnCost - CancelRebateCost);
                        objPay.FT.RealCost = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(-(CancelPayCost + CancelOwnCost - CancelRebateCost));
                        objPay.FT.OwnCost = -(CancelOwnCost - CancelRebateCost);
                        objPay.InputOper.OperTime = nowTime;
                        objPay.Invoice.ID = InvoiceNO;
                        objPay.Squence = paySquence.ToString();
                        if (invoiceInfo.IsAccount)
                        {
                            objPay.PayType.ID = "YS";
                        }
                        else
                        {
                            objPay.PayType.ID = "CA";
                        }

                        if (isATM && Function.CheckAtmFee(InvoiceNO))
                        {
                            objPay.PayType.ID = "YS";
                        }
                        objPay.InputOper.ID = outpatientManager.Operator.ID;
                        objPay.InvoiceCombNO = invoiceSeqNegative;
                        objPay.CancelType = cancelType;
                        objPay.IsChecked = false;
                        objPay.CheckOper.ID = string.Empty;
                        objPay.CheckOper.OperTime = DateTime.MinValue;
                        objPay.BalanceOper.ID = string.Empty;
                        //p.BalanceNo = 0;
                        objPay.IsDayBalanced = false;
                        objPay.IsAuditing = false;
                        objPay.AuditingOper.OperTime = DateTime.MinValue;
                        objPay.AuditingOper.ID = string.Empty;
                        if (patient.Pact.PayKind.ID == "02")
                        {
                            objPay.FT.OwnCost = -CancelOwnCost;
                        }

                        // string choosePayMode = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.QUIT_PAY_MODE_SELECT, "1");
                        if (!invoiceInfo.IsAccount && choosePayMode == "0") //ѡ��֧����ʽ
                        {
                            ArrayList payLists = new ArrayList();

                            payLists.Add(objPay);

                            FS.HISFC.Components.OutpatientFee.Froms.frmChooseBalancePay frmTemp = new FS.HISFC.Components.OutpatientFee.Froms.frmChooseBalancePay();
                            frmTemp.Init();
                            frmTemp.QuitPayModes = payLists;
                            frmTemp.InitQuitPayModes();
                            frmTemp.StartPosition = FormStartPosition.CenterScreen;
                            frmTemp.ShowDialog();

                            if (frmTemp.IsSelect == false)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("û��ѡ���˷ѵ�֧����ʽ���������˷�!");

                                return -1;
                            }

                            payLists = new ArrayList();
                            payLists = frmTemp.ModifiedPayModes;

                            objPay = payLists[0] as FS.HISFC.Models.Fee.Outpatient.BalancePay;
                        }

                        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                        #region �˻�����(�˻�����۷ѽ��)
                        if (objPay.PayType.ID == "YS")
                        {
                            if (feeIntegrate.AccountCancelPay(patient, objPay.FT.OwnCost, InvoiceNO, (outpatientManager.Operator as Employee).Dept.ID, "C") < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�˻��˷��뻧ʧ�ܣ�" + feeIntegrate.Err);
                                return -1;
                            }
                            CancleInvoiceNo = InvoiceNO;
                        }
                        #endregion

                        iReturn = outpatientManager.InsertBalancePay(objPay);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����֧������Ϣ����!" + outpatientManager.Err);

                            return -1;
                        }
                        payList.Add(objPay);
                        #endregion
                    }
                }
                else
                {
                    #region �¼ӵ�

                    int returnJValue = this.outpatientManager.UpdateBalancePayModeCancelType(invoiceInfo.Invoice.ID, invoiceInfo.CombNO, nowTime,cancelType);
                    if (returnJValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���Ϸ�Ʊ֧����ʽ����!" + outpatientManager.Err);
                        return -1;
                    }

                    int bpIdx = 0;
                    foreach (FS.HISFC.Models.Fee.Outpatient.BalancePay bp in feePayMods)
                    {
                        if (bp != null)
                        {
                            BalancePay objPay = bp.Clone();
                            if (bp.PayType.ID == "CD" || bp.PayType.ID == "DB")
                            {
                                decimal bankTransTot = 0m;
                                bankTransTot = -objPay.FT.TotCost;
                                bool isBankTransOK = false;

                                try
                                {
                                    bankTrans.InputListInfo.Clear();
                                    bankTrans.OutputListInfo.Clear();
                                    /// 0:�������ͣ�1�����׽��
                                    bankTrans.InputListInfo.Add("1");
                                    bankTrans.InputListInfo.Add(bankTransTot);
                                    isBankTransOK = bankTrans.Do();
                                }
                                catch (Exception ex)
                                {
                                    isBankTransOK = false;
                                }
                                if (isBankTransOK == false)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(bankTrans.OutputListInfo[0].ToString());
                                    return -1;
                                }
                                if (bankTrans.OutputListInfo.Count >= 4)
                                {
                                    if (bankTransTot != NConvert.ToDecimal(bankTrans.OutputListInfo[3]))
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("����������" + bankTransTot.ToString() + "�����ڽ��׽��" + NConvert.ToDecimal(bankTrans.OutputListInfo[3]) + ",����ʧ�ܣ�");
                                        return -1;
                                    }
                                    else
                                    {
                                        //MessageBox.Show("���׳ɹ������" + bankTransTot.ToString());
                                        objPay.Bank.Name = bankTrans.OutputListInfo[0].ToString();
                                        objPay.Bank.Account = bankTrans.OutputListInfo[1].ToString();
                                        objPay.Squence = bankTrans.OutputListInfo[2].ToString();
                                    }
                                }
                                //   FS.HISFC.Components.OutpatientFee.Forms.frmBankTrans fbt = new FS.HISFC.Components.OutpatientFee.Forms.frmBankTrans();
                                //fbt.TotCost = bankTransTot;
                                //  FS.FrameWork.Models.NeuObject no = new FS.FrameWork.Models.NeuObject();
                                //  no.ID = objPay.Bank.Name;
                                //  no.Memo = objPay.Bank.Account;
                                //  no.User01 = objPay.Squence;
                                //  no.Name = objPay.FT.TotCost.ToString();

                                //  fbt.ListTransInfo.Add(no);
                                //fbt.ShowDialog();
                                //if (fbt.ListTransInfo.Count <= 0)
                                //{
                                //    FS.FrameWork.Management.PublicTrans.RollBack();
                                //    MessageBox.Show("�����˷�ʧ��!" + outpatientManager.Err);
                                //    return -1;
                                //}
                                //else
                                //{
                                //    if (fbt.ListTransInfo.Count == 1)
                                //    {
                                //        //this.fpPayMode_Sheet1.CellChanged -= new SheetViewEventHandler(fpPayMode_Sheet1_CellChanged);
                                //        //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Bank].Text =
                                //        //    fbt.ListTransInfo[0].ID;
                                //        //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Account].Text = 
                                //        //    fbt.ListTransInfo[0].Memo;
                                //        //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.PosNo].Text = 
                                //        //    fbt.ListTransInfo[0].User01;
                                //        //this.fpPayMode_Sheet1.ActiveRow.Locked = true;
                                //        //this.fpPayMode_Sheet1.CellChanged += new SheetViewEventHandler(fpPayMode_Sheet1_CellChanged);
                                //        //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Text = 
                                //        //    fbt.ListTransInfo[0].Name;
                                //        objPay.Bank.Name = no.ID;
                                //        objPay.Bank.Account = no.Memo;
                                //        objPay.Squence = no.User01;
                                //        // objPay.FT.TotCost = no.Name;
                                //        }
                                //}
                            }
                            #region
                            objPay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                            objPay.FT.TotCost = -objPay.FT.TotCost;
                            objPay.FT.RealCost = -objPay.FT.RealCost;
                            objPay.FT.OwnCost = -objPay.FT.OwnCost;
                            objPay.InputOper.OperTime = nowTime;
                            objPay.Invoice.ID = InvoiceNO;
                            objPay.Squence = (99 - bpIdx).ToString();
                            objPay.InputOper.ID = outpatientManager.Operator.ID;
                            objPay.InvoiceCombNO = invoiceSeqNegative;
                            objPay.CancelType = cancelType;
                            objPay.IsChecked = false;
                            objPay.CheckOper.ID = string.Empty;
                            objPay.CheckOper.OperTime = DateTime.MinValue;
                            objPay.BalanceOper.ID = string.Empty;
                            //p.BalanceNo = 0;
                            objPay.IsDayBalanced = false;
                            objPay.IsAuditing = false;
                            objPay.AuditingOper.OperTime = DateTime.MinValue;
                            objPay.AuditingOper.ID = string.Empty;
                            #endregion
                            iReturn = outpatientManager.InsertBalancePay(objPay);
                            if (iReturn <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����֧������Ϣ����!" + outpatientManager.Err);
                                return -1;
                            }
                            #region �˻�����(�˻�����۷ѽ��)
                            if (objPay.PayType.ID == "YS")
                            {
                                if (feeIntegrate.AccountCancelPay(patient, objPay.FT.TotCost, InvoiceNO, (outpatientManager.Operator as Employee).Dept.ID, "C") < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�˻��˷��뻧ʧ�ܣ�" + feeIntegrate.Err);
                                    return -1;
                                }
                                CancleInvoiceNo = InvoiceNO;
                            }
                            #endregion


                            bpIdx++;

                            #region ���ڼ��⡢���ʻ��ߣ�������⡢��������

                            if (objPay.PayType.ID != "RC" || objPay.PayType.ID != "JZ")
                            {
                                payList.Add(objPay);
                            }

                            #endregion
                        }
                    }
                    #endregion
                }
            }
            #endregion
            
            bool isCashPay = false;//�Ƿ��ֽ����

            #region ��¼�˷���Ϣ
            alQuitFeeItemList.Clear();
            FS.HISFC.Models.Fee.ReturnApply returnApply = null;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        returnApply = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(returnApply.RecipeNO, returnApply.SequenceNO);
                        if (feeItemList == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��ȡ�˷������Ӧ������ϸʧ�ܣ�" + returnApplyManager.Err);
                            return -1;
                        }
                        alQuitFeeItemList.Add(feeItemList);
                    }
                    else if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                    {
                        alQuitFeeItemList.Add((FS.HISFC.Models.Fee.Outpatient.FeeItemList)sv.Rows[i].Tag);
                    }
                }
            }

            if (this.isQuitFeeAndOperOrder)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemTemp in alQuitFeeItemList)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderTemp = orderIntegrate.GetOneOrder(feeItemTemp.Patient.ID, feeItemTemp.Order.ID.ToString());
                    if (orderTemp != null && orderTemp.Status == 1)
                    {
                        this.orderIntegrate.UpdateOrderBeCaceled(orderTemp);
                    }
                }
            }

            #endregion

            //���������ϸ
            ArrayList alFeeDetail = outpatientManager.QueryFeeItemListsByInvoiceSequence(invoiceInfo.CombNO);
            if (alFeeDetail == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��û��߷�����ϸ����!" + outpatientManager.Err);

                return -1;
            }
            iReturn = outpatientManager.UpdateFeeItemListCancelType(invoiceInfo.CombNO, nowTime, cancelType);
            if (iReturn <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���ϻ�����ϸ����!" + outpatientManager.Err);

                return -1;
            }

            oldFeeItemLists = new ArrayList();
            foreach (FeeItemList f in alFeeDetail)
            {
                oldFeeItemLists.Add(f.Clone());
                f.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                f.FT.OwnCost = -f.FT.OwnCost;
                f.FT.PayCost = -f.FT.PayCost;
                f.FT.PubCost = -f.FT.PubCost;
                f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                f.Item.Qty = -f.Item.Qty;
                f.CancelType = cancelType;
                f.FeeOper.ID = outpatientManager.Operator.ID;
                f.FeeOper.OperTime = nowTime;
                f.ChargeOper.OperTime = nowTime;
                f.InvoiceCombNO = invoiceSeqNegative;
                f.ConfirmedInjectCount = 0;

                iReturn = outpatientManager.InsertFeeItemList(f);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������ϸ������Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
            }

            //this.t.BeginTransaction();


            if (this.patient.Pact.PayKind.ID=="02"&&DialogResult.Yes == MessageBox.Show("�Ƿ�ѡ��ҽ���Ǽǻ��ߣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
            this.medcareInterfaceProxy.BeginTranscation();
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            //long returnValue = medcareInterfaceProxy.SetPactCode(this.patient.Pact.ID);


            returnValue = medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + medcareInterfaceProxy.ErrMsg);
                return -1;
            }
            
                returnValue = medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
                if (returnValue != 1)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڻ�û�����Ϣʧ��") + medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                returnValue = medcareInterfaceProxy.DeleteUploadedFeeDetailsOutpatient(this.patient, ref alFeeDetail);
                if (returnValue != 1)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ��ϴ��˷���ϸʧ��") + medcareInterfaceProxy.ErrMsg);

                    return -1;
                }
                this.patient.SIMainInfo.InvoiceNo = ((Balance)quitInvoices[0]).Invoice.ID;
                returnValue = medcareInterfaceProxy.CancelBalanceOutpatient(this.patient, ref alFeeDetail);
                if (returnValue != 1)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڽ���ʧ��") + medcareInterfaceProxy.ErrMsg);

                    return -1;
                }
            }



            //���δ��׼��ҩ��Ϣ
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList fQuit = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        //��δȷ�ϵ���ҩ��������ҩ����!
                        if (fQuit.IsConfirmed == false)
                        {
                            iReturn = pharmacyIntegrate.CancelApplyOutClinic(fQuit.RecipeNO, fQuit.SequenceNO);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                medcareInterfaceProxy.Rollback();
                                MessageBox.Show("���Ϸ�ҩ�������!ҩƷ�����Ѿ���ҩ����ˢ�´�������");

                                return -1;
                            }
                        }
                    }
                }
            }
            //�����ն�����
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag != null && this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList fQuit = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;

                    //��δȷ�ϵ���ҩ��������ҩ����!
                    if (fQuit.IsConfirmed == false)
                    {
                        iReturn = confirmIntegrate.CancelConfirmTerminal(fQuit.Order.ID, fQuit.Item.ID);
                        if (iReturn < 0)
                        {
                            medcareInterfaceProxy.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�����ն��������!" + confirmIntegrate.Err);

                            return -1;
                        }
                    }
                }
            }

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            #region �����˷������˷ѱ��
            //alQuitFeeItemList.Clear();
            //FS.HISFC.Models.Fee.ReturnApply returnApply = null;
            //DateTime operDate = outpatientManager.GetDateTimeFromSysDateTime();
            string operCode = outpatientManager.Operator.ID;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        returnApply = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        returnApply.CancelType = CancelTypes.Valid;
                        returnApply.CancelOper.ID = operCode;
                        returnApply.CancelOper.OperTime = nowTime;
                        if (returnApplyManager.UpdateApplyCharge(returnApply) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����������˷ѱ��ʧ�ܣ�" + returnApplyManager.Err);
                            return -1;
                        }
                        //FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListAndQuitForFee(returnApply.RecipeNO, returnApply.SequenceNO);
                        //if (feeItemList == null)
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    MessageBox.Show("��ȡ�˷������Ӧ������ϸʧ�ܣ�" + returnApplyManager.Err);
                        //    return -1;
                        //}
                        //alQuitFeeItemList.Add(feeItemList);
                    }
                    //else if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                    //{
                    //    alQuitFeeItemList.Add((FS.HISFC.Models.Fee.Outpatient.FeeItemList)sv.Rows[i].Tag);
                    //}
                }
            }

            #endregion

            #region �������˷Ѳ��ֽ����˿�
            //{143CA424-7AF9-493a-8601-2F7B1D635027}
            ArrayList alMate = new ArrayList();
            for (int i = 0; i < this.fpSpread2_Sheet2.RowCount; i++)
            {
                if (this.fpSpread2_Sheet2.Rows[i].Tag != null && this.fpSpread2_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList fQuit = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;

                    //�Ƕ��յ����� {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    if (fQuit.Item.ItemType == EnumItemType.MatItem)
                    {
                        alMate.Add(fQuit);
                    }
                    else
                    {
                        if (fQuit.MateList.Count > 0)
                        {
                            alMate.Add(fQuit);
                        }
                    }
                }
            }
            if (alMate.Count > 0)
            {
                //�˿�
                if (mateIntegrate.MaterialFeeOutputBack(alMate) < 0)
                {
                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show("�����˿�ʧ��,\n" + mateIntegrate.Err);

                    return -1;
                }
            }
            #endregion

           
           ////��������ѷ��ñ���
           //string roundFeeItemCode = "F00000053238";
           //ArrayList lst = this.constManager.GetList("ROUNDFEEITEMCODE");
           //if (lst.Count > 0)
           //{
           //    roundFeeItemCode = ((FS.HISFC.Models.Base.Const)lst[0]).ID;
           //}
            //��ʣ����Ŀ�շ�!
            ArrayList feeDetails = new ArrayList();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = (this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList).Clone();
                    f.FT.OwnCost = f.FT.PubCost = f.FT.PayCost = 0;
                    f.FT.OwnCost = f.FT.TotCost;
                    //f.ConfirmedQty = 0;
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text) > 0)//by yuyun ��������ѷ�ҩ֮�������ˣ��˷�ʱȫ�˵����

                    //if (f.Item.Qty > 0)
                    {
                        f.User03 = "HalfQuit";
                        if (f.Item.ID != roundFeeItemCode)//��������ѷ��ò�����
                        feeDetails.Add(f);
                    }
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = (this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList).Clone();
                    f.FT.OwnCost = f.FT.PubCost = f.FT.PayCost = 0;
                    f.FT.OwnCost = f.FT.TotCost;
                    //f.IsConfirmed = false;
                    //f.ConfirmedQty = 0;
                    // if (f.Item.Qty > 0)

                    //{06212A22-5FD4-4db3-838C-1790F75FF286}
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text) > 0)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug unDrugTemp = this.undrugManager.GetUndrugByCode(f.Item.ID);
                        if (unDrugTemp != null)
                        {
                            f.Item.IsNeedConfirm = unDrugTemp.IsNeedConfirm;
                            f.Item.NeedConfirm = unDrugTemp.NeedConfirm;
                            f.Item.IsNeedBespeak = unDrugTemp.IsNeedBespeak;
                        }

                        //{06212A22-5FD4-4db3-838C-1790F75FF286}
                        if (f.IsConfirmed == true)
                        {
                            int row = this.FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                            if (row != -1)
                            {
                                FeeItemList quitItem = this.fpSpread2_Sheet2.Rows[row].Tag as FeeItemList;
                                if (confirmIntegrate.UpdateOrDeleteTerminalConfirmApply(f.Order.ID, (int)(f.Item.Qty + quitItem.Item.Qty), (int)quitItem.Item.Qty, FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty, 2)) == -1)
                                {
                                    medcareInterfaceProxy.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�����ն�ȷ����Ϣ����!" + confirmIntegrate.Err);
                                    return -1;
                                }
                            }
                        }


                        f.User03 = "HalfQuit";
                        if (f.Item.ID != roundFeeItemCode)//��������ѷ��ò�����
                        feeDetails.Add(f);
                    }
                }
            }
            string returnCostString = string.Empty;

            //���շ�����ϸ!;
            //{BBE9766A-A539-485e-A03B-9972DC675538} �˷Ѳ���
            ArrayList addFeeItemList = this.ucDisplay1.GetFeeItemList();
            if (addFeeItemList != null && addFeeItemList.Count > 0)
            {
                if (this.cmbRegDept.Tag == null || this.cmbRegDept.Tag.ToString() == string.Empty || this.cmbRegDept.Text.Trim() == string.Empty)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��ѡ���շ��õĿ������!" + confirmIntegrate.Err);

                    return -1;
                }

                if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == string.Empty || this.cmbDoct.Text.Trim() == string.Empty)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��ѡ���շ��õĿ���ҽ��!" + confirmIntegrate.Err);

                    return -1;
                }

                foreach (FeeItemList f in addFeeItemList)
                {
                    string doctCode = string.Empty;
                    doctCode = this.cmbDoct.Tag.ToString();
                    FS.HISFC.Models.Base.Employee empl = this.managerIntegrate.GetEmployeeInfo(doctCode);
                    if (empl != null)
                    {
                        f.RecipeOper.Dept.ID = empl.Dept.ID;
                    }
                    //����ҽ�� {83283AE6-6D16-4b69-9B42-F2E0754FC8B2}
                    ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID = this.cmbDoct.Tag.ToString();

                    f.RecipeOper.ID = doctCode;
                    (f.Patient as FS.HISFC.Models.Registration.Register).DoctorInfo.Templet.Dept.ID = this.cmbRegDept.Tag.ToString();
                    f.NoBackQty = f.Item.Qty;
                }

                feeDetails.AddRange(addFeeItemList);
            }
            //{BBE9766A-A539-485e-A03B-9972DC675538} ����

            
            if (feeDetails.Count > 0)
            {
                #region ����
                if (isHaveRebateCost)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    medcareInterfaceProxy.Rollback();
                    MessageBox.Show("���ŷ�Ʊ�����Żݽ��,��ȫ��!");
                    return -1;
                }

                string errText = string.Empty, invoiceNo = string.Empty, realInvoiceNo = string.Empty;


                FS.HISFC.Models.Registration.Register tmpReg = null;

                //���������컼��,��ô���»�û��߹Һ���Ϣ
                if (!(this.patient.ChkKind == "1" || this.patient.ChkKind == "2"))
                {

                    tmpReg = registerIntegrate.GetByClinic(this.patient.ID);
                    if (tmpReg == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        medcareInterfaceProxy.Rollback();
                        medcareInterfaceProxy.Disconnect();
                        MessageBox.Show("��ùҺ���Ϣʧ��!" + this.registerIntegrate.Err);

                        return -1;
                    }

                    #region �޸Ĺ��ѻ�����Ϣ������

                    /*���β�����,�����ȷ���շѽ���ȡ���շѣ����߹Һ���Ϣʵ���ѱ��޸�
                     *��������޸Ľ�������Ϊ�Էѻ���ҽ�������߸���ҽ��֤�ţ����˵��ž����е�����ˣ�
                     *���Դ˹��������ε�
                    */
                    if (this.patient.Pact.PayKind.ID == "03" &&��this.IsCanModifyPatientInfo == true)
                    {
                        DialogResult _Reu;
                        _Reu = MessageBox.Show("��Ҫ�޸Ľ��㻼����Ϣ��?", "�޸Ľ��㻼����Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (_Reu == DialogResult.Yes)
                        {
                            if (this.patient != null && this.patient.Pact.PayKind.ID == "03")
                            {
                                FS.HISFC.Components.OutpatientFee.Forms.frmPatientInfo frmPatientInfo = new FS.HISFC.Components.OutpatientFee.Forms.frmPatientInfo();
                                frmPatientInfo.Init();
                                frmPatientInfo.IsNeedJZD = this.IsNeedJZD;
                                FS.HISFC.Models.Registration.Register tmpOrgReg = null;
                                tmpOrgReg = this.patient.Clone();

                                frmPatientInfo.PatientInfo = tmpOrgReg;

                                frmPatientInfo.ShowDialog();
                                if (frmPatientInfo.IsConfirm)
                                {
                                    this.patient.Pact = frmPatientInfo.PatientInfo.Pact;
                                    this.patient.SSN = frmPatientInfo.PatientInfo.SSN;
                                    this.patient.LSH = frmPatientInfo.PatientInfo.LSH;
                                    this.patient.User03 = frmPatientInfo.PatientInfo.User03;
                                    //�������ñ����
                                    this.medcareInterfaceProxy.SetPactCode(this.patient.Pact.ID);
                                }
                                frmPatientInfo.Clear();
                            }
                        }
                    }

                    #endregion

                    tmpReg.Pact = this.patient.Pact;
                    tmpReg.User03 = this.patient.User03;//�޶�ȼ���GROUPID
                    tmpReg.LSH = this.patient.LSH;//���˵��ţ���ҽ����������ҽԺû�õ�
                    tmpReg.SSN = this.patient.SSN;//ҽ��֤��
                    this.patient = tmpReg.Clone();
                }
                returnValue = medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
                if (returnValue == -1)
                {
                    medcareInterfaceProxy.Rollback();
                    medcareInterfaceProxy.Disconnect();
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show("�����ӿڻ�ýӿڻ��߻�����Ϣʧ��!" + medcareInterfaceProxy.ErrMsg);

                    return -1;
                }
                if (tmpReg != null && tmpReg.IDCard != this.patient.IDCard)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    medcareInterfaceProxy.Rollback();
                    medcareInterfaceProxy.Disconnect();
                    MessageBox.Show("���֤���ϴ��շ���Ϣ����,����ѡ�����!�����˷�!");

                    return -1;
                }

                #region �޸Ĺ��ѱ���

                if (this.patient.Pact.PayKind.ID == "03" && IsCanModifyRate == true)
                {
                    DialogResult _Reu;
                    _Reu = MessageBox.Show("��Ҫ�޸Ĺ�������������?", "�����޸ı���", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (_Reu == DialogResult.Yes)
                    {
                        if (this.patient == null)
                        {
                            return -1;
                        }
                        if (this.patient != null && this.patient.Pact.PayKind.ID == "03")
                        {
                            this.Focus();
                            ArrayList alFee = feeDetails;
                            ucModifyItemRate modifyRate = new ucModifyItemRate();
                            modifyRate.FeeDetails = alFee;
                            modifyRate.InitFeeDetails();
                            FS.FrameWork.WinForms.Classes.Function.PopShowControl(modifyRate);
                            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�޸ı���";
                        }
                    }
                }

                #endregion

                foreach (FeeItemList f in feeDetails)
                {
                    f.FeeOper.OperTime = nowTime;
                    f.FTSource = "3";
                    f.ConfirmedInjectCount = 0;
                }
                if (choosePayMode == "0")//ѡ��֧����ʽ
                {
                    #region ѡ��֧����ʽ
                    decimal ownCost = 0, payCost = 0, pubCost = 0, totCostPayMode = 0; decimal overDrugFee = 0; decimal selfDrugFee = 0;
                    decimal rebateCost = 0;

                    if (this.patient.Pact.PayKind.ID == "01")//�Էѣ�ֱ���ۼӸ���Ŀ���
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            ownCost += f.FT.OwnCost;
                            //payCost += f.FT.PayCost;
                            //pubCost += f.FT.PubCost;
                            totCostPayMode += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                            //if (f.Item.IsPharmacy)
                            if (f.Item.ItemType == EnumItemType.Drug)
                            {
                                overDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.ExcessCost);
                                selfDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.DrugOwnCost);
                            }

                            //f.NoBackQty = f.Item.Qty;
                        }
                    }
                    if (this.patient.Pact.PayKind.ID == "02")//ҽ��
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            totCostPayMode += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        }
                        ownCost = totCostPayMode - this.patient.SIMainInfo.PubCost - this.patient.SIMainInfo.PayCost;
                        payCost += this.patient.SIMainInfo.PayCost;
                        pubCost += this.patient.SIMainInfo.PubCost;
                    }

                    #region �������¼���

                    if (this.patient.Pact.PayKind.ID == "03")//����
                    {
                        returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.patient, ref feeDetails);
                        if (returnValue == -1)
                        {
                            MessageBox.Show("��ù��ѽ�����Ϣʧ��!" + this.medcareInterfaceProxy.ErrMsg);
                            this.medcareInterfaceProxy.Rollback();
                            this.medcareInterfaceProxy.Disconnect();

                            return -1;
                        }

                        foreach (FeeItemList f in feeDetails)
                        {
                            overDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.ExcessCost);
                            selfDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.DrugOwnCost);
                        }

                        totCostPayMode = this.patient.SIMainInfo.TotCost;
                        ownCost = this.patient.SIMainInfo.OwnCost;
                        payCost = this.patient.SIMainInfo.PayCost;
                        pubCost = this.patient.SIMainInfo.PubCost;

                    }

                    #endregion                  

                    ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);
                    payCost = FS.FrameWork.Public.String.FormatNumber(payCost, 2);
                    pubCost = FS.FrameWork.Public.String.FormatNumber(pubCost, 2);
                    totCostPayMode = FS.FrameWork.Public.String.FormatNumber(totCostPayMode, 2);

                    #region �շѽ��ȡ��������20121022


                    //-------------------------------delete 2012��10��22�� XF-----------------------------
                    /*
                    #region �շѽ��ȡ��
                    string s = this.controlParamIntegrate.GetControlParam<string>("MZ9927", true, string.Empty);
                    bool isGetByFirstItemFeeCode = this.controlParamIntegrate.GetControlParam<bool>("MZ9925", true, true);
                    if (s != string.Empty)
                    {
                        bool isInsertItemList = NConvert.ToBoolean(s);

                        if (isInsertItemList)
                        {
                            iOutPatientFeeRoundOff = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                                    FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff>(this.GetType());
                            if (iOutPatientFeeRoundOff == null)
                            {
                                MessageBox.Show("����ȡ���ӿ�δ���ã�");
                                return -1;
                            }
                            FeeItemList feeItemList = new FeeItemList();

                            // ��������С���ã��÷����б��һ����¼��С����
                            string drugFeeCode = "";

                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in feeDetails)
                            {
                                if (string.IsNullOrEmpty(item.Item.MinFee.ID))
                                {
                                    continue;
                                }

                                drugFeeCode = item.Item.MinFee.ID;
                                break;
                            }
                            if (!string.IsNullOrEmpty(drugFeeCode) && isGetByFirstItemFeeCode)
                            {
                                feeItemList.User03 = drugFeeCode;
                            }

                            iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.patient, ref ownCost, ref feeItemList, (feeDetails[0] as FeeItemList).RecipeSequence);
                            if (feeItemList.Item.ID != "")
                            {
                                totCostPayMode = ownCost + payCost + pubCost;
                                this.patient.SIMainInfo.OwnCost = ownCost;
                                this.patient.SIMainInfo.TotCost = totCostPayMode;
                                feeDetails.Add(feeItemList);
                            }
                        }
                    }
                    #endregion
                    */
                    //-------------------------------delete 2012��10��22�� XF-----------------------------
                    #endregion

                    #region �����շѵ��շѽ��ȡ��

                    decimal shouldPayCost = 0;
                    if (this.patient.Pact.PayKind.ID == "03")
                    {
                        shouldPayCost = ownCost + payCost - rebateCost;
                    }
                    else
                    {
                        shouldPayCost = ownCost - rebateCost;
                    }

                    string isRoundFeeByDetail = this.controlParamIntegrate.GetControlParam<string>("MZ9927", true, string.Empty);

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

                                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in feeDetails)
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

                                if (this.patient.Pact.PayKind.ID == "03")
                                //���Ѳ��ֶ�pay_costҲ������������
                                {
                                    iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.patient, ref shouldPayCost, ref feeItemList, (feeDetails[0] as FeeItemList).RecipeSequence);
                                    if (feeItemList.Item.ID != "")
                                    {
                                        {
                                            ownCost = shouldPayCost - payCost + rebateCost;//�����Żݽ��
                                            totCostPayMode = ownCost + payCost + pubCost;
                                            feeItemList.ItemRateFlag = "1";
                                            this.patient.SIMainInfo.OwnCost = ownCost;
                                            this.patient.SIMainInfo.TotCost = totCostPayMode;
                                            feeDetails.Add(feeItemList);
                                        }
                                    }
                                }
                                else
                                {
                                    iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.patient, ref shouldPayCost, ref feeItemList, (feeDetails[0] as FeeItemList).RecipeSequence);
                                    if (feeItemList.Item.ID != "")
                                    {
                                        ownCost = shouldPayCost + rebateCost;//�����Żݽ��
                                        totCostPayMode = ownCost + payCost + pubCost;
                                        this.patient.SIMainInfo.OwnCost = ownCost;
                                        this.patient.SIMainInfo.TotCost = totCostPayMode;
                                        feeDetails.Add(feeItemList);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #endregion

                    FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee frmBalance = this.popFeeControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee>
                  (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_POP_FEE, new FS.HISFC.Components.OutpatientFee.Froms.frmDealBalance());
                    //frmBalance.Trans = t;
                    //this.frmBalance.ucDealBalance1.FrmDisplay = frmDisplay;
                    this.isAutoBankTrans = this.controlParamIntegrate.GetControlParam<bool>("MZ9001", true, false);
                    //�����ӿ�
                    iBankTrans = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                        FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans>(this.GetType());
                    if (iBankTrans == null)
                    {
                        iBankTrans = new FS.HISFC.Components.OutpatientFee.Forms.frmBankTrans();
                    }
                    frmBalance.BankTrans = iBankTrans;
                    frmBalance.IsAutoBankTrans = this.isAutoBankTrans;
                    frmBalance.Init();
                    //������������˷�,������ʹ���˻����Ǯ,���ڸĽӿ�̫�鷳��,ֻ���÷���  add by yerl
                    if (IsAtm)
                    {
                        try
                        {
                            frmBalance.GetType().GetProperty("IsAccountPay").SetValue(frmBalance, true, null);
                        }
                        catch{ }
                    }
                    frmBalance.IsCashPay = isCashPay;
                    frmBalance.FeeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee(frmBalance_FeeButtonClicked);
                    frmBalance.PatientInfo = this.patient;
                    frmBalance.SelfDrugCost = selfDrugFee;
                    frmBalance.OverDrugCost = overDrugFee;
                    frmBalance.RealCost = ownCost + payCost;
                    frmBalance.OwnCost = ownCost;
                    frmBalance.PayCost = payCost;
                    frmBalance.PubCost = pubCost;
                    frmBalance.TotCost = totCostPayMode;
                    frmBalance.TotOwnCost = ownCost + payCost;
                    frmBalance.FeeDetails = feeDetails;
                    frmBalance.IsQuitFee = true;
                    //frmBalance.Trans = this.t;

                    FS.HISFC.Models.Base.Employee employee = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

                    // �˻���ʱ��Ʊ����ʱȡ��ʱ��Ʊ��
                    // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                    int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee, "C", blnIsAccountInvoice, ref invoiceNo, ref realInvoiceNo, ref errText);
                    if (iReturnValue == -1)
                    {
                        medcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return -1;
                    }

                    //{18B0895D-9F55-4d93-B374-69E96F296D0D}  ����ȡ��Ʊ������Bug����
                    FS.HISFC.Components.OutpatientFee.Class.Function.IsQuitFee = true;

                    ArrayList alInvoiceAndDetails = FS.HISFC.Components.OutpatientFee.Class.Function.MakeInvoice(this.feeIntegrate, this.patient, feeDetails, invoiceNo, realInvoiceNo, ref errText);
                    if (alInvoiceAndDetails == null)
                    {
                        medcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return -1;
                    }
                    #region liuq 2007-8-27 ��Ʊ������ϸ
                    frmBalance.InvoiceFeeDetails = (ArrayList)alInvoiceAndDetails[2];
                    #endregion
                    frmBalance.InvoiceDetails = (ArrayList)alInvoiceAndDetails[1];

                    // ���÷�Ʊ״̬
                    // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                    ArrayList invoices = (ArrayList)alInvoiceAndDetails[0];
                    foreach (Balance invoice in invoices)
                    {
                        invoice.IsAccount = blnIsAccountInvoice;
                        invoice.User02 = this.patient.LSH;//�����շ�ʱȡ�ϴ��շѵļ��˵���
                        invoice.User03 = this.patient.User03;//�޶�ȼ�
                        invoice.Patient.SSN = this.patient.SSN;//ҽ��֤��
                    }
                    if (this.patient.Pact.PayKind.ID != "03")
                    {
                        #region �籣����
                        // ����Ҫ���ǻ������
                        //if (this.patient.Pact.PayKind.ID == "02" || this.patient.Pact.PayKind.ID == "03")
                        //{
                        ownCost = decimal.Zero;
                        payCost = decimal.Zero;
                        pubCost = decimal.Zero;
                        totCostPayMode = decimal.Zero;

                        rebateCost = 0;

                        //foreach (Balance invoice in (ArrayList)alInvoiceAndDetails[0])
                        //{
                        //    if (invoice.Memo == "4")//���˷�Ʊ!
                        //    {
                        //        invoice.FT.PubCost = pubCost;
                        //        invoice.FT.PayCost = payCost;
                        //        invoice.FT.OwnCost = invoice.FT.TotCost - pubCost - payCost;
                        //    }
                        //      ArrayList tempFeeItemListArray = (ArrayList)alInvoiceAndDetails[2];
                        //    for (int i = 0; i < tempFeeItemListArray.Count; i++)
                        //    {

                        //        FeeItemList tempFeeItemList = ((ArrayList)tempFeeItemListArray[i])[0] as FeeItemList;

                        //        if (invoice.Invoice.ID == tempFeeItemList.Invoice.ID)
                        //        {
                        //            ArrayList myFeeItemlist = new ArrayList();
                        //            myFeeItemlist = (ArrayList)tempFeeItemListArray[i];
                        //ɾ��������Ϊ�����������ԭ���ϴ�����ϸ
                        returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsAllOutpatient(this.patient);

                        //�����ϴ�������ϸ
                        returnValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.patient, ref feeDetails);
                        if (returnValue == -1)
                        {
                            this.medcareInterfaceProxy.Rollback();
                            MessageBox.Show("�ϴ�������ϸʧ��!" + this.medcareInterfaceProxy.ErrMsg);

                            return -1;
                        }
                        returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.patient, ref feeDetails);
                        if (returnValue == -1)
                        {
                            MessageBox.Show("���ҽ��Ԥ������Ϣʧ��!" + this.medcareInterfaceProxy.ErrMsg);
                            this.medcareInterfaceProxy.Rollback();
                            this.medcareInterfaceProxy.Disconnect();

                            return -1;
                        }

                        foreach (FeeItemList f in feeDetails)
                        {
                            rebateCost += f.FT.RebateCost;
                        }


                        ownCost += this.patient.SIMainInfo.OwnCost;
                        payCost += this.patient.SIMainInfo.PayCost;
                        pubCost += this.patient.SIMainInfo.PubCost;
                        //{21EEC08E-53DA-458b-BEA3-0036EF6E3D37}
                        //    + this.patient.SIMainInfo.OfficalCost
                        //    + this.patient.SIMainInfo.OverCost;
                        totCostPayMode += this.patient.SIMainInfo.PayCost;
                        totCostPayMode += this.patient.SIMainInfo.OwnCost;
                        totCostPayMode += this.patient.SIMainInfo.PubCost;
                        //+ this.patient.SIMainInfo.OfficalCost
                        //+ this.patient.SIMainInfo.OverCost;
                        frmBalance.RealCost = ownCost;
                        frmBalance.OwnCost = ownCost;
                        frmBalance.PayCost = payCost;
                        frmBalance.PubCost = pubCost;
                        frmBalance.TotCost = totCostPayMode;
                        frmBalance.TotOwnCost = ownCost;

                        frmBalance.RebateRate = rebateCost;
                        ////�Ͽ������ӿ�����
                        //this.medcareInterfaceProxy.Rollback();
                        //this.medcareInterfaceProxy.Disconnect();
                        //FS.FrameWork.Management.PublicTrans.RollBack();

                        ////���¸�ֵ
                        //invoice.FT.OwnCost = this.patient.SIMainInfo.OwnCost;
                        //invoice.FT.PubCost = this.patient.SIMainInfo.PubCost;
                        //invoice.FT.PayCost = this.patient.SIMainInfo.PayCost;
                        //        }
                        //    }

                        //}
                        //}

                        #endregion
                    }

                        againFeeItemLists = new ArrayList();

                    againFeeItemLists = feeDetails;

                    frmBalance.Invoices = invoices;

                    modifiedBalancePays = payList;

                    if (!((Form)frmBalance).Visible)
                    {
                        this.Focus();
                        frmBalance.SetControlFocus();
                        isSuccess = false;
                        frmBalance.IsPushCancelButton = true;
                        ((Form)frmBalance).Location = new Point(this.Location.X + 150, this.Location.Y + 200);
                        ((Form)frmBalance).ShowDialog();
                    }
                    //ԭ��ʹ��frmBalance.IsPushCancelButton���ж���û�շѳɹ�,��������������BUG,�������һ��isSuccess�������ж�add by yerl
                    if (!isSuccess || frmBalance.IsPushCancelButton == true)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        medcareInterfaceProxy.Rollback();
                        return -1;
                    }

                    #endregion
                }
                else
                {

                    #region ��֧����ʽ��
                    decimal ownCost = 0, payCost = 0, pubCost = 0; decimal totCost = 0;
                    decimal overDrugFee = 0; decimal selfDrugFee = 0;//�ӳ���ҩ�����Է�ҩ���
                    if (this.patient.Pact.PayKind.ID == "01") //�Էѣ�ֱ���ۼӸ���Ŀ���
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            ownCost += f.FT.OwnCost;
                            payCost += f.FT.PayCost;
                            pubCost += f.FT.PubCost;
                            totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        }
                    }
                    if (this.patient.Pact.PayKind.ID == "02")//ҽ��
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        }
                        ownCost = totCost - this.patient.SIMainInfo.PubCost - this.patient.PayCost;
                        payCost += this.patient.SIMainInfo.PayCost;
                        pubCost += this.patient.SIMainInfo.PubCost;
                    }

                    #region �������¼���

                    if (this.patient.Pact.PayKind.ID == "03")//����
                    {
                        returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.patient, ref feeDetails);
                        if (returnValue == -1)
                        {
                            MessageBox.Show("��ù��ѽ�����Ϣʧ��!" + this.medcareInterfaceProxy.ErrMsg);
                            this.medcareInterfaceProxy.Rollback();
                            this.medcareInterfaceProxy.Disconnect();

                            return -1;
                        }

                        foreach (FeeItemList f in feeDetails)
                        {
                            overDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.ExcessCost);
                            selfDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.DrugOwnCost);
                        }

                        totCost = this.patient.SIMainInfo.TotCost;
                        ownCost = this.patient.SIMainInfo.OwnCost;
                        payCost = this.patient.SIMainInfo.PayCost;
                        pubCost = this.patient.SIMainInfo.PubCost;

                    }
                    #endregion                   

                    FS.HISFC.Models.Base.Employee employee = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

                    // �˻���ʱ��Ʊ����ʱȡ��ʱ��Ʊ��
                    // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                    iReturn = this.feeIntegrate.GetInvoiceNO(employee, "C", this.blnIsAccountInvoice, ref invoiceNo, ref realInvoiceNo, ref errText);
                    if (iReturn < 0)
                    {
                        medcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return -1;
                    }
                    //�����·�Ʊ

                    //{18B0895D-9F55-4d93-B374-69E96F296D0D}  ����ȡ��Ʊ������Bug����
                    FS.HISFC.Components.OutpatientFee.Class.Function.IsQuitFee = true;

                    ArrayList invoicesAndDetails = FS.HISFC.Components.OutpatientFee.Class.Function.MakeInvoice(this.feeIntegrate, this.patient, feeDetails, invoiceNo, realInvoiceNo, ref errText, FS.FrameWork.Management.PublicTrans.Trans);
                    if (invoicesAndDetails == null || invoicesAndDetails.Count == 0)
                    {
                        medcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return -1;
                    }
                    ArrayList invoices = (ArrayList)invoicesAndDetails[0];
                    foreach (Balance invoice in invoices)
                    {
                        invoice.IsAccount = this.blnIsAccountInvoice;
                        invoice.User02 = this.patient.LSH;//���Ѽ��˵���
                        invoice.User03 = this.patient.User03;//�޶�ȼ�
                        invoice.Patient.SSN = this.patient.SSN;//ҽ��֤��
                    }


                    if (this.patient.Pact.PayKind.ID == "02")
                    {
                        foreach (Balance invoice in invoices)
                        {
                            if (invoice.Memo == "4")//���˷�Ʊ!
                            {
                                invoice.FT.PubCost = pubCost;
                                invoice.FT.PayCost = payCost;
                                invoice.FT.OwnCost = invoice.FT.TotCost - pubCost - payCost;
                            }
                        }
                    }
                    ArrayList alTempInvoiceDetails = new ArrayList();
                    ArrayList alFinalInvoiceDetails = new ArrayList();
                    foreach (ArrayList alTemp in ((ArrayList)invoicesAndDetails[1]))
                    {
                        alTempInvoiceDetails.Add(alTemp[0]);
                    }
                    alFinalInvoiceDetails.Add(alTempInvoiceDetails);



                    BalancePay pFinal = new BalancePay();

                    //					foreach(FeeItemList f in feeDetails)
                    //					{
                    //						totCost += f.FT.OwnCost + f.FT.PayCost;
                    //					}
                    decimal orgCost = 0;
                    foreach (BalancePay p in payList)
                    {
                        //��Ϊ��ʱ��֧����ʽΪ��
                        orgCost += -p.FT.RealCost;
                    }
                    decimal returnCost = orgCost - totCost;
                    decimal returnCostCent = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(returnCost);
                    decimal centCost = returnCost - returnCostCent;
                    pFinal.FT.TotCost = totCost;
                    pFinal.FT.RealCost = totCost + centCost;
                    pFinal.PayType.Name = "�ֽ�";
                    pFinal.PayType.ID = "CA";

                    ArrayList alPay = new ArrayList();
                    alPay.Add(pFinal);

                    //�˷�,������Ĭ�Ϸ�Ʊ�ŷ�ʽʱ,����Ҫ�ٴθ��·�Ʊ��
                    this.feeIntegrate.IsNeedUpdateInvoiceNO = false;

                    // �շ�
                    // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                    bool bReturn = this.peFeeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, this.blnIsAccountInvoice, this.patient,
                        invoices, alFinalInvoiceDetails, feeDetails, feeDetails, alPay, ref errText);

                    if (!bReturn)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        medcareInterfaceProxy.Rollback();
                        if (errText != string.Empty)
                        {
                            MessageBox.Show(errText);
                        }

                        return -1;
                    }

                     //if (InterfaceManager.GetIOrder() != null)
                     //{
                     //    if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, alQuitFeeItemList, false) < 0)
                     //    {
                     //        FS.FrameWork.Management.PublicTrans.RollBack();
                     //        this.medcareInterfaceProxy.Rollback();
                     //        MessageBox.Show(this, "�˷�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��"   +InterfaceManager.GetIOrder().Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     //        return -1;
                     //    }
                     //}
                    //{014680EC-6381-408b-98FB-A549DAA49B82}
                    if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().SendFeeInfo(this.patient, oldFeeItemLists, false) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(this, "�˷�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                    //{014680EC-6381-408b-98FB-A549DAA49B82}
                    if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().SendFeeInfo(this.patient, feeDetails, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(this, "�˷�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
 
                     this.medcareInterfaceProxy.Commit();
                     FS.FrameWork.Management.PublicTrans.Commit();

                    returnCostString = "Ӧ�˽��: " + FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(returnCost).ToString();
                    tbQuitCash.Text = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(returnCost).ToString();
                    #endregion
                }

                //FS.FrameWork.Management.PublicTrans.Commit();
                //this.medcareInterfaceProxy.Commit();
                MessageBox.Show("�˷ѳɹ�!" + "\n" + returnCostString);

                #endregion

                #region ��������˷�(����)�ӿ�
                tjwebforhis webServiceForPe = new tjwebforhis();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in feeDetails)
                {
                    string peResult = webServiceForPe.GetItemChargeStatusPart(this.patient.ID, this.patient.PID.CardNO, item.Item.ID, "0");
                    if (!"0".Equals(peResult))
                    {
                        MessageBox.Show("���û������ӿ��˷ѳ�������ϵ�Է������ˣ��˷���Ŀ��"+ item.Item.Name);
                    }
                }
                #endregion
            }
            
            else
            {
                #region ȫ��

                decimal orgCost = 0;
                decimal otherCost = 0m;
                bool isHaveCard = false;
                #region liu.xq1008
                //foreach (BalancePay p in payModes)
                //{
                //    //��Ϊ��ʱ��֧����ʽΪ��
                //    if (p.PayType.ID.ToString() == "CA")
                //    {
                //        orgCost += -p.FT.RealCost;
                //    }
                //    if (p.PayType.ID.ToString() != "CA")
                //    {
                //        isHaveCard = true;
                //        otherCost += -p.FT.RealCost;
                //    }
                //}
                #endregion

                decimal caCost = 0m;
                decimal chCost = 0m;
                decimal cdCost = 0m;
                decimal dbCost = 0m;
                decimal pbCost = 0m;
                decimal xxCost = 0m;

                if (isHaveCard)
                {
                    if (otherCost > 0)
                    {
                        returnCostString = "Ӧ�˽��:�ֽ� " + (CancelOwnCost - CancelRebateCost).ToString() + "  ����֧����ʽ:" + CancelPubCost.ToString();
                    }
                    else
                    {
                        returnCostString = "Ӧ�˽��: " + (CancelOwnCost - CancelRebateCost).ToString();
                    }
                }
                else
                {
                    foreach (BalancePay p in feePayMods)
                    {
                        if (p.PayType.ID == "CA")
                        {
                            caCost += p.FT.RealCost;
                        }
                        else if (p.PayType.ID == "CH")
                        {
                            chCost += p.FT.RealCost;
                        }
                        else if (p.PayType.ID == "CD")
                        {
                            cdCost += p.FT.RealCost;
                        }
                        else if(p.PayType.ID == "DB")
                        {
                            dbCost += p.FT.RealCost;
                        }
                        else if (p.PayType.ID == "PB")
                        {
                            pbCost += p.FT.RealCost;
                        }
                        else
                        {
                            xxCost += p.FT.RealCost;
                        }
                    }
                    returnCostString = "Ӧ�˽��: " + (caCost + chCost + cdCost + dbCost + xxCost).ToString("F2") + System.Environment.NewLine +
                        "���У�" + System.Environment.NewLine +
                        "�ֽ�{0}" + System.Environment.NewLine +
                        "���дſ�{1}" + System.Environment.NewLine +
                        "���дſ�{2}" + System.Environment.NewLine +
                        "֧Ʊ{3}" + System.Environment.NewLine +
                        "����{4}" + System.Environment.NewLine
                        ;
                    returnCostString = string.Format(returnCostString, caCost.ToString("F2"), cdCost.ToString("F2"), dbCost.ToString("F2"), chCost.ToString("F2"), pbCost.ToString("F2"), xxCost.ToString("F2"));
                }

                //{014680EC-6381-408b-98FB-A549DAA49B82}
                if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder() != null)
                {
                    //{014680EC-6381-408b-98FB-A549DAA49B82}
                    if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().SendFeeInfo(this.patient, alQuitFeeItemList, false) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        //{014680EC-6381-408b-98FB-A549DAA49B82}
                        MessageBox.Show(this, "�˷�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                this.medcareInterfaceProxy.Commit();

                this.ucCostDisplay1.Clear();
                this.ucDisplay1.Clear();
                this.ucInvoicePreview1.Clear();

                tbQuitCash.Text = (caCost + chCost + cdCost + dbCost + xxCost).ToString("F2");

                MessageBox.Show("�˷ѳɹ�!" + "\n" + returnCostString);

                if (this.isCanSaveCharge)
                {
                    this.SaveCharge();
                }

                #endregion

                #region ��������˷�(ȫ��)�ӿ�
                tjwebforhis webServiceForPe = new tjwebforhis();
                string peResult = webServiceForPe.GetItemChargeStatusAll(this.patient.ID, this.patient.PID.CardNO, "0");
                if (!"0".Equals(peResult))
                {
                    MessageBox.Show("���û������ӿ��˷ѳ�������ϵ�Է������ˣ�");
                }
                #endregion
            }

            //��ӡ��Ʊ {EC3C448A-2E7C-4eff-9348-0AC37B40F438}
            if (this.isPrintBill)
            {
                string invoicePrintDll = null;

                invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

                // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
                // 2011-08-04
                // �˴�������ʾ
                //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                //{
                //    MessageBox.Show("û�����÷�Ʊ��ӡ�������շ���ά��!");

                //}
                //��Ʊ
                ArrayList alInvoices = new ArrayList();
                Balance tmpInvoice = quitInvoices[0] as Balance;
                tmpInvoice.Invoice.ID += "(�˷�)";
                tmpInvoice.FT.TotCost = -tmpInvoice.FT.TotCost;
                tmpInvoice.FT.OwnCost = -tmpInvoice.FT.OwnCost;
                tmpInvoice.FT.PayCost = -tmpInvoice.FT.PayCost;
                tmpInvoice.FT.PubCost = -tmpInvoice.FT.PubCost;
                tmpInvoice.PrintTime = outpatientManager.GetDateTimeFromSysDateTime();
                alInvoices.Add(tmpInvoice);

                //��Ʊ��ϸ
                ArrayList alIDetails = new ArrayList();
                foreach (ArrayList alInvoiceDetail in alInvoiceDetails)
                {
                    int sort = 0;
                    foreach (BalanceList balList in alInvoiceDetail)
                    {
                        sort++;
                        balList.BalanceBase.FT.TotCost = balList.BalanceBase.FT.OwnCost + balList.BalanceBase.FT.PayCost + balList.BalanceBase.FT.PubCost;
                        balList.FeeCodeStat.SortID = sort;
                        //balList.BalanceBase.FT.TotCost = -balList.BalanceBase.FT.TotCost;
                        //balList.BalanceBase.FT.OwnCost = -balList.BalanceBase.FT.OwnCost;
                        //balList.BalanceBase.FT.PayCost = -balList.BalanceBase.FT.PayCost;
                        //balList.BalanceBase.FT.PubCost = -balList.BalanceBase.FT.PubCost;
                    }
                }
                alIDetails.Add(alInvoiceDetails);

                //������Ϣ
                this.patient.SIMainInfo.TotCost = -this.patient.SIMainInfo.TotCost;
                this.patient.SIMainInfo.OwnCost = -this.patient.SIMainInfo.OwnCost;
                this.patient.SIMainInfo.PayCost = -this.patient.SIMainInfo.PayCost;
                this.patient.SIMainInfo.PubCost = -this.patient.SIMainInfo.PubCost;
                this.patient.SIMainInfo.OfficalCost = -this.patient.SIMainInfo.OfficalCost;
                this.patient.SIMainInfo.OverCost = -this.patient.SIMainInfo.OverCost;

                string errText = "";

                this.feeIntegrate.PrintInvoice(invoicePrintDll, this.patient, alInvoices, alIDetails, alFeeDetail, payList, false, ref errText);

            }

            // �˷ѳɹ�
            // {2E5139C9-52D8-4fec-A96B-09BECFDDFBD1}
            //if (this.trvInvoice.SelectedNode != null)
            //{
            //    if(string.IsNullOrEmpty(txtCardNO.Text.Trim()))
            //    {
            //        trvInvoice.Nodes.Clear();
            //    }
            //    else
            //    {
            //        txtCardNO_KeyDown(null, new KeyEventArgs(Keys.Enter));
            //    }
            //}
            if (IsAtm)
            {
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Models.Account.AccountRecord CancleRecord = accountMgr.GetAccountRecord(patient.PID.CardNO, CancleInvoiceNo);
                FS.HISFC.Models.Account.AccountRecord FeeRecord=accountMgr.GetAccountRecord(patient.PID.CardNO,FeeInvoiceNo);
                if (FeeRecord == null)
                {
                    decimal vacancy=0;
                    accountMgr.GetVacancy(patient.PID.CardNO,ref vacancy);
                    //{014680EC-6381-408b-98FB-A549DAA49B82}
                    //�˴���Ա�˻��ѸĶ����˷���Ҫ���´�����ʱ����
                    //CancleRecord.Vacancy = vacancy;
                }
                CancleInvoiceNo = string.Empty;
                FeeInvoiceNo = string.Empty;
                IPrintCancleFee Iprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintCancleFee)) as IPrintCancleFee;
                if (Iprint != null)
                {
                    Iprint.SetValue(CancleRecord, FeeRecord);
                    Iprint.Print();
                }
                else
                {
                    MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");

                }
            }
            blnShowInvoiceNoFind = false;
            this.ucInvoiceView.RefurbishInvoice();
            blnShowInvoiceNoFind = true;

            return 1;
        }

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
                print.SetValue(rInfo, invoices, feeDetails);
                print.Print();
            }
        }

        #endregion

        /// <summary>
        /// ��ʾ������
        /// </summary>
        /// <returns></returns>
        protected virtual int DisplayCalc()
        {
            string tempValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CALCTYPE, "0");

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
        /// �շѰ�ť����
        /// </summary>
        /// <param name="alPayModes">֧����ʽ��Ϣ</param>
        /// <param name="invoices">��Ʊ��Ϣ��������Ӧ��Ʊ�������Ϣ��ÿ�������Ӧһ����Ʊ��</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ��Ϣ����Ӧ���ν����ȫ��������ϸ��</param>
        /// <param name="invoiceFeeItemDetails">��Ʊ������ϸ��Ϣ������Ʊ�����ķ�����ϸ��ÿ�������Ӧ�÷�Ʊ�µķ�����ϸ��</param>
        void frmBalance_FeeButtonClicked(ArrayList balancePays, ArrayList invoices, ArrayList invoiceDetails, ArrayList invoiceFeeDetails)
        {
            string errText = string.Empty;

            this.feeIntegrate.IsNeedUpdateInvoiceNO = false;
            //============��ʱ�������Ӱ� luzhp@nuesoft.com
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in invoices)
            {
                invoice.CanceledInvoiceNO = InvoiceNoStr;
            }
            if (this.patient.Pact.PayKind.ID == "02"
                 || this.patient.Pact.PayKind.ID == "03"
                )
            {
                foreach (Balance myBalance in invoices)
                {
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
                    #region �ϴ�ҽ����Ϣ

                    this.patient.SIMainInfo.InvoiceNo = myBalance.Invoice.ID;
                    //���ú�ͬ��λ

                    long returnMedcareValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.patient, ref myFeeItemListArray);
                    if (returnMedcareValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ��ϴ���ϸʧ��") + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }
                    returnMedcareValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
                    returnMedcareValue = this.medcareInterfaceProxy.BalanceOutpatient(this.patient, ref myFeeItemListArray);
                    if (returnMedcareValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ��������ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }

                    #endregion

                }
            }
            //==========
            bool bReturn = this.peFeeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, this.blnIsAccountInvoice, this.patient, invoices, invoiceDetails, againFeeItemLists, invoiceFeeDetails, balancePays, ref errText);

            if (!bReturn)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (errText != string.Empty)
                {
                    MessageBox.Show(errText);
                }
                return;
            }


            //{014680EC-6381-408b-98FB-A549DAA49B82}
            if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder() != null)
            {
                //{014680EC-6381-408b-98FB-A549DAA49B82}
                if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().SendFeeInfo(this.patient, oldFeeItemLists, false) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(this, "�˷�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //{014680EC-6381-408b-98FB-A549DAA49B82}
                if (FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().SendFeeInfo(this.patient, againFeeItemLists, true) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(this, "�˷�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + FS.HISFC.Components.OutpatientFee.InterfaceManager.GetIOrder().Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            isSuccess = true;
            FS.FrameWork.Management.PublicTrans.Commit();

            this.medcareInterfaceProxy.Commit();

            #region ��Ʊ��ӡ

            if (!this.blnIsAccountInvoice)
            {
                string invoicePrintDll = null;

                invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

                // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
                // 2011-08-04
                // �˴�������ʾ
                //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                //{
                //    MessageBox.Show("û�����÷�Ʊ��ӡ�������շ���ά��!");

                //    //return false;
                //}

                this.feeIntegrate.PrintInvoice(invoicePrintDll, this.patient, invoices, invoiceDetails, againFeeItemLists, invoiceFeeDetails, balancePays, false, ref errText);
            }
            //�����շѵķ�Ʊ��Ϣ
            if (balancePays != null && balancePays.Count > 0)
            {
                FS.HISFC.Models.Fee.BalancePayBase bpb = balancePays[0] as FS.HISFC.Models.Fee.BalancePayBase;
                if (bpb != null)
                    this.FeeInvoiceNo = bpb.Invoice.ID;
            }
            
            #endregion

            #region ����ָ������ӡ

            this.PrintGuide(this.patient, invoices, againFeeItemLists);

            #endregion

            decimal orgCost = 0;
            decimal newCost = 0;
            bool isHaveCard = false;
            decimal returnCost = 0;
            decimal accountOrgCost = 0m;

            decimal accountNewCost = 0m;
            decimal returnAccountCost = 0m;
            foreach (BalancePay p in modifiedBalancePays)
            {
                //if (p.PayType.ID == "RC")
                //{
                //    orgCost += p.FT.OwnCost;
                //}


                //��Ϊ��ʱ��֧����ʽΪ��
                if (p.PayType.ID.ToString() == "CA")
                {
                    orgCost += -p.FT.RealCost;
                }
                if (p.PayType.ID.ToString() != "CA")
                {
                    isHaveCard = true;
                }
                if (p.PayType.ID.ToString() == "YS")
                {
                    accountOrgCost += -p.FT.RealCost;
                }
            }
            foreach (BalancePay p in balancePays)
            {
                //��Ϊ��ʱ��֧����ʽΪ��
                if (p.PayType.ID.ToString() == "CA")
                {
                    newCost += p.FT.RealCost;
                }
                if (p.PayType.ID.ToString() == "YS")
                {
                    accountNewCost += p.FT.RealCost;
                }
            }
            returnCost = orgCost - newCost;
            returnAccountCost = accountOrgCost - accountNewCost;
            returnCost = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(returnCost);
            string messageText = string.Empty;

            if (returnCost == 0)
            {
                if (returnAccountCost >= 0)
                {
                    messageText = "�˻��˷�" + returnAccountCost.ToString();
                }
                else
                {
                    messageText = "�˻���ȡ" + (-returnAccountCost).ToString();
                }
            }
            else if (returnCost > 0)
            {
                messageText = "Ӧ�˽��: " + returnCost.ToString();
            }
            else
            {
                messageText = "Ӧ���ֽ�: " + (-returnCost).ToString();
            }
            MessageBox.Show(messageText);
            tbQuitCash.Text = returnCost.ToString();

            this.Clear();
        }

        /// <summary>
        /// ������
        /// </summary>
        protected virtual void ComputCost()
        {
            decimal realQuitCost = 0;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                        realQuitCost += f.FT.TotCost;

                        //    realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) - 
                        //        (FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) / f.FT.TotCost) * f.FT.RebateCost;
                    }
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;

                        realQuitCost += f.FT.TotCost;

                        //realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) - 
                        //    (FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) / f.FT.TotCost) * f.FT.RebateCost;
                    }
                }
            }



            //			for(int i = 0; i < this.fpSpread2_Sheet1.RowCount; i ++)
            //			{
            //				if(this.fpSpread2_Sheet1.Rows[i].Tag != null)
            //				{
            //					if(this.fpSpread2_Sheet1.Rows[i].Tag is FeeItemList)
            //					{
            //						FeeItemList f = this.fpSpread2_Sheet1.Rows[i].Tag as FeeItemList;
            //
            //						realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //					}
            //					if(this.fpSpread2_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
            //					{
            //						FS.HISFC.Models.Fee.ReturnApply f = this.fpSpread2_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
            //						
            //						realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //					}
            //				}
            //				
            //			}
            //			for(int i = 0; i < this.fpSpread2_Sheet2.RowCount; i ++)
            //			{
            //				if(this.fpSpread2_Sheet2.Rows[i].Tag != null)
            //				{
            //					if(this.fpSpread2_Sheet2.Rows[i].Tag is FeeItemList)
            //					{
            //						FeeItemList f = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;
            //
            //						realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //					}
            //					if(this.fpSpread2_Sheet2.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
            //					{
            //						FS.HISFC.Models.Fee.ReturnApply f = this.fpSpread2_Sheet2.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
            //						
            //						realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //					}
            //				}
            //			}
            decimal totCost = 0;
            totCost = FS.FrameWork.Function.NConvert.ToDecimal(tbTotCost.Text);
            this.tbQuitCost.Text = (totCost - realQuitCost).ToString();
            this.tbReturnCost.Text = realQuitCost.ToString();
        }

        /// <summary>
        /// ���
        /// </summary>
        protected virtual void Clear()
        {
            //�������ҩȷ��ʱ����ˢ���շ���ؿؼ�
            if (!this.isQuitDrugConfirm)
            {
                this.ucDisplay1.Clear();
                this.ucCostDisplay1.Clear();
                this.ucInvoicePreview1.Clear();
            }
            this.quitInvoices = null;
            
            this.tbInvoiceNO.Text = string.Empty;
            this.tbCardNo.Text = string.Empty;
            this.tbName.Text = string.Empty;
            this.tbPactName.Text = string.Empty;
            this.tbQuitCost.Text = string.Empty;
            tbTotCost.Text = string.Empty;
            tbOwnCost.Text = string.Empty;
            tbPayCost.Text = string.Empty;
            tbPubCost.Text = string.Empty;
            tbReturnCost.Text = string.Empty;
            this.txtReturnItemName.Text = string.Empty;
            this.txtRetSpecs.Text = string.Empty;
            this.txtReturnNum.Text = string.Empty;
            this.txtUnit.Text = string.Empty;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.RowCount = 5;
            this.fpSpread1_Sheet2.RowCount = 0;
            this.fpSpread1_Sheet2.RowCount = 5;
            this.fpSpread2_Sheet1.RowCount = 0;
            this.fpSpread2_Sheet1.RowCount = 5;
            this.fpSpread2_Sheet2.RowCount = 0;
            this.fpSpread2_Sheet2.RowCount = 5;
            this.Focus();
            this.tbInvoiceNO.Focus();
            this.cmbDoct.Tag = string.Empty;
            this.cmbRegDept.Tag = string.Empty;
            this.isAccount = false;
        }

        /// <summary>
        /// ���滮����Ϣ
        /// </summary>
        protected virtual void SaveCharge()
        {
            DialogResult result;

            result = MessageBox.Show("�Ƿ�ȷ��Ҫ���ۣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                return;
            }
            if (this.quitInvoices != null && this.quitInvoices.Count > 0)
            {
                if (hsInvoice.Contains(quitInvoices[0]))
                {
                    DialogResult r = MessageBox.Show("�÷�Ʊ������Ϣ�Ѿ����۱����,�Ƿ����»���?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (r == DialogResult.Cancel)
                    {
                        return;
                    }
                    hsInvoice.Remove(quitInvoices[0]);
                }
            }

            if (this.invoiceFeeItemLists == null || this.invoiceFeeItemLists.Count <= 0)
            {
                MessageBox.Show("û�л�����Ϣ��");
                return;
            }
            ArrayList alTemp = new ArrayList();
            if (!isSaveChargeRoundFee || !isSaveChargeDiagFee)
            {
                foreach (FeeItemList f in invoiceFeeItemLists)
                {
                    if (!isSaveChargeRoundFee && f.Item.ID.Equals(roundFeeItemCode))
                    {
                        continue;
                    }
                    if (!isSaveChargeDiagFee && f.Item.ID.Equals(ownDiagFeeCode))
                    {
                        continue;
                    }
                    alTemp.Add(f.Clone());
                }
            }
            else
            {
                foreach (FeeItemList f in invoiceFeeItemLists)
                {
                    alTemp.Add(f.Clone());
                }
            }

            System.Collections.Hashtable hsCombNos = new Hashtable();
            int combNo = 100;

            foreach (FeeItemList item in alTemp)
            {
                if (item.UndrugComb.ID != null && item.UndrugComb.ID.Length > 0)
                {
                    if (hsCombNos.ContainsKey(item.UndrugComb.ID))
                    {
                        item.Order.Combo.ID = hsCombNos[item.UndrugComb.ID].ToString();
                    }
                    else
                    {
                        hsCombNos.Add(item.UndrugComb.ID, combNo.ToString());
                        combNo++;
                    }
                }
                item.FTSource = "3";//���۱��棬������ԴΪ��� 

                item.FT.TotCost = item.FT.PayCost + item.FT.OwnCost + item.FT.PubCost;

                item.FT.PayCost = 0m;
                item.FT.PubCost = 0m;
                item.FT.OwnCost = item.FT.TotCost;
                item.PayType = FS.HISFC.Models.Base.PayTypes.Charged;
                item.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                item.RecipeSequence = string.Empty;
                item.RecipeNO = string.Empty;
                item.SequenceNO = -1;
                item.Invoice.ID = string.Empty;
                item.InvoiceCombNO = null;
                item.Order.ID = string.Empty;
                item.ConfirmedQty = 0;
                item.IsConfirmed = false;
                item.PayType = FS.HISFC.Models.Base.PayTypes.Charged;
                item.NoBackQty = item.Item.Qty;
                item.ConfirmedInjectCount = 0;
                //item.ConfirmOper = new FS.HISFC.Models.Base.OperEnvironment();

                item.ChargeOper.ID = this.outpatientManager.Operator.ID;

                item.FeeOper.OperTime = System.DateTime.MinValue;

                if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    bool flag = outpatientManager.IsHaveTechApplyNo(item.RecipeNO, item.SequenceNO);
                    if (flag)
                    {
                        item.Item.IsNeedConfirm = true;
                        item.Item.NeedConfirm = FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Outpatient;
                    }
                }
                else
                {
                    item.IsConfirmed = false;
                }

                item.Item.SpecialFlag2 = FS.FrameWork.Function.NConvert.ToInt32(item.IsConfirmed).ToString();
            }

            bool iReturn = false;
            DateTime dtNow = outpatientManager.GetDateTimeFromSysDateTime();
            string errText = string.Empty;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            iReturn = this.feeIntegrate.SetChargeInfo(this.patient, alTemp, dtNow, ref errText);

            if (iReturn == false)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���۳���" + errText);
                return;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���۳ɹ���");
                if (this.quitInvoices != null && this.quitInvoices.Count > 0)
                {
                    Balance invo = this.quitInvoices[0] as Balance;

                    hsInvoice.Add(invo, null);
                }
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ����toolBar��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("�˷�", "ȷ���˷���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("ȡ��", "ȡ���Ѿ�ѡ����˷���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            toolBarService.AddToolButton("ȫѡ", "ѡ��ȫ������", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��, true, false, null);
            toolBarService.AddToolButton("������", "�򿪼�����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            //{F28E9BBB-D37E-4d8b-B25A-24F834290FBC}���ӻ��۱��湦��
            toolBarService.AddToolButton("���۱���", "�ѵ�ǰ�˷���Ŀ����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H���۱���, true, false, null);
            //{F28E9BBB-D37E-4d8b-B25A-24F834290FBC}���
            toolBarService.AddToolButton("ˢ��", "ˢ��", FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�˷�":
                    string tempinvoiceNO = tbInvoiceNO.Text.Trim();
                    if (!checkisAtm(tempinvoiceNO))
                    {
                        return ;
                    }
                    //if (isATM && Function.CheckAtmFee(tempinvoiceNO))
                    //{
                    //    isAccount = true;
                    //}

                    if (this.isCanSaveCharge && isShowSaveChargeHits)
                    {
                        if (this.quitInvoices != null && this.quitInvoices.Count > 0)
                        {
                            if (!hsInvoice.Contains(quitInvoices[0]))
                            {
                                DialogResult r = MessageBox.Show("�Ƿ���Ҫ���۱���÷�Ʊ������Ϣ?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                if (r == DialogResult.OK)
                                {
                                    this.SaveCharge();
                                }
                            }
                        }
                    }
                    if (patient != null && isAccount)
                    {
                        if (SaveAccountQuiteFee() == 1)
                        {
                            this.Clear();
                        }
                    }
                    else
                    {
                        if (this.Save() == 1)
                        {
                            this.Clear();
                        }
                    }
                    break;
                case "����":
                    this.Clear();
                    break;
                case "ȡ��":
                    this.CancelQuitOperation();
                    break;

                case "������":
                    this.DisplayCalc();
                    break;

                case "ȫѡ":
                    this.AllQuit();
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
                case "ˢ��":
                    {
                        string cardNo = "";
                        string error = "";
                        if (Function.OperCard(ref cardNo, ref error) == -1)
                        {
                            MessageBox.Show(error, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        this.ucInvoiceView.MCardNo = cardNo;
                        break;
                    }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                //				this.panel1.Focus();
                //				this.panel2.Focus();
                //				this.fpSpread1.Focus();
                //				this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
                //				if(this.fpSpread1_Sheet1.RowCount > 0)
                //				{
                //					this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                //				}
                if (this.isCanSaveCharge)
                {
                    this.SaveCharge();
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("û�л���Ȩ��"));
                }
            }
            if (keyData == Keys.F6)
            {
                //				this.panel1.Focus();
                //				this.panel2.Focus();
                //				this.fpSpread1.Focus();
                //				this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
                //				if(this.fpSpread1_Sheet2.RowCount > 0)
                //				{
                //					this.fpSpread1_Sheet2.ActiveRowIndex = 0;
                //				}
                if (this.fpSpread1.Focused)
                {
                    if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)
                    {
                        this.fpSpread2.Focus();
                        if (this.fpSpread2_Sheet1.RowCount > 0)
                        {
                            this.fpSpread2_Sheet1.ActiveRowIndex = 0;
                        }
                    }
                    if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)
                    {
                        this.fpSpread2.Focus();
                        if (this.fpSpread2_Sheet2.RowCount > 0)
                        {
                            this.fpSpread2_Sheet2.ActiveRowIndex = 0;
                        }
                    }
                }
                else
                {
                    if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet1)
                    {
                        this.fpSpread1.Focus();
                        if (this.fpSpread1_Sheet1.RowCount > 0)
                        {
                            this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                        }
                    }
                    if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet2)
                    {
                        this.fpSpread1.Focus();
                        if (this.fpSpread1_Sheet2.RowCount > 0)
                        {
                            this.fpSpread1_Sheet2.ActiveRowIndex = 0;
                        }
                    }
                }
            }
            if (keyData == Keys.F7)
            {
                this.panel1.Focus();
                this.fpSpread2.Focus();
                this.fpSpread2.ActiveSheet = this.fpSpread2_Sheet1;
                if (this.fpSpread2_Sheet1.RowCount > 0)
                {
                    this.fpSpread2_Sheet1.ActiveRowIndex = 0;
                }
            }
            if (keyData == Keys.F8)
            {
                this.panel1.Focus();
                this.fpSpread2.Focus();
                this.fpSpread2.ActiveSheet = this.fpSpread2_Sheet2;
                if (this.fpSpread2_Sheet2.RowCount > 0)
                {
                    this.fpSpread2_Sheet2.ActiveRowIndex = 0;
                }
            }
            if (keyData == Keys.F11)
            {
                this.ckbAllQuit.Checked = !this.ckbAllQuit.Checked;
                if (this.ckbAllQuit.Checked)
                {
                    this.txtReturnNum.Enabled = false;
                }
                else
                {
                    this.txtReturnNum.Enabled = true;
                }
            }
            if (keyData == Keys.F2)
            {
                this.neuTabControl1.SelectedTab = this.tpQuit;
                this.tpQuit.Focus();
                this.tbInvoiceNO.Select();
                this.tbInvoiceNO.Focus();
            }
            if (keyData == Keys.F3)
            {
                this.neuTabControl1.SelectedTab = this.tpFee;
                this.tpFee.Focus();
                this.ucDisplay1.Select();
                this.ucDisplay1.Focus();
            }
            if (keyData == Keys.F4)
            {
                this.AllQuit();
            }
            if (keyData == Keys.F9)
            {
                this.Clear();
            }
            if (keyData == Keys.F12)
            {
                this.FindForm().Close();
            }
            if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }
            if (keyData == Keys.F1)
            {
                //FS.FrameWork.WinForms.Classes.Function.PopShowControl(new FS.Common.Controls.ucCalc());
            }

            return base.ProcessDialogKey(keyData);
        }

        protected bool checkisAtm(string invoiceno)
        {
            if (invoiceno.StartsWith("T"))
            {
                MessageBox.Show("����ȥ��ӡ��ʽ��Ʊ���˷ѡ�");
                this.tbInvoiceNO.Focus();
                return false;
            }
            if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).CurrentGroup.Name.Contains("ҩ��"))
                return true;

            if (isATM)
            {
                if (Function.CheckAtmFee(invoiceno))
                {

                    return true;
                }
                else
                {
                    MessageBox.Show("�������������Ʊ����ȥ��ͨ�շѴ��ڰ���!");
                    this.tbInvoiceNO.Focus();
                    return false;
                }
            }
            else
            {
                if (Function.CheckAtmFee(invoiceno))
                {
                    MessageBox.Show("�������������Ʊ����ȥר������!");
                    this.tbInvoiceNO.Focus();
                    return false;
                  
                }
            }
            return true;
        }


        protected virtual void tbInvoiceNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string invoiceNO = this.tbInvoiceNO.Text;

                this.Clear();
                this.tbQuitCash.Text = string.Empty;

                if (invoiceNO == string.Empty)
                {
                    MessageBox.Show("�����뷢Ʊ��!");
                    this.tbInvoiceNO.Focus();
                    return;
                }
                this.tbInvoiceNO.Text = invoiceNO;
                this.quitInvoices = QueryInvoices(invoiceNO);
                if (quitInvoices == null)
                {
                    return;
                }
                if (quitInvoices.Count == 0)
                {
                    return;
                }

                if (!checkisAtm(invoiceNO))
                {
                    return;
                }



                if (quitInvoices.Count > 1)
                {
                    string invoiceNoTemp = string.Empty;
                    foreach (Balance invoice in quitInvoices)
                    {
                        invoiceNoTemp += invoice.Invoice.ID + "\n";
                    }
                    MessageBox.Show("�˴��˷���:" + invoiceNoTemp + "��ȫ���ջ�!");
                }


                Balance invoiceTemp = quitInvoices[0] as Balance;

                if (invoiceTemp.BalanceOper.ID != this.outpatientManager.Operator.ID)
                {
                    //�ж�Ȩ��,�Ƿ����������Һ�Ա������Ȩ��
                    if (!CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivQuitOtherOperFee))
                    {
                        CommonController.CreateInstance().MessageBox("��û������������Ա�շѼ�¼��Ȩ�ޣ�������ȡ�����÷��õĲ���Ա�ǣ�" + CommonController.CreateInstance().GetEmployeeName(invoiceTemp.BalanceOper.ID), MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (invoiceTemp.BalanceOper.OperTime < this.outpatientManager.GetDateTimeFromSysDateTime().Date)
                {
                    //�����˷�
                    if (!CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivQuitLastDayFee))
                    {
                        CommonController.CreateInstance().MessageBox("��û�и����˷ѵ�Ȩ�ޣ�������ȡ��", MessageBoxIcon.Warning);
                        return;
                    }
                }



                FS.HISFC.Models.Registration.Register tmpReg = registerIntegrate.GetByClinic(invoiceTemp.Patient.ID);
                if (tmpReg == null)
                {
                    MessageBox.Show("��ùҺ���Ϣ����!" + this.registerIntegrate.Err);

                    this.tbInvoiceNO.Focus();

                    return;
                }

                this.ucDisplay1.PatientInfo = tmpReg.Clone();
                this.ucDisplay1.PatientInfo.Pact = this.managerIntegrate.GetPactUnitInfoByPactCode(tmpReg.Pact.ID);

                this.tbCardNo.Text = invoiceTemp.Patient.PID.CardNO;
                this.tbName.Text = invoiceTemp.Patient.Name;
                this.tbPactName.Text = invoiceTemp.Patient.Pact.Name;

                this.patient.PID.CardNO = invoiceTemp.Patient.PID.CardNO;
                this.patient.Name = invoiceTemp.Patient.Name;
                this.patient.Pact.PayKind.ID = invoiceTemp.Patient.Pact.PayKind.ID;
                this.patient.Pact.ID = invoiceTemp.Patient.Pact.ID;
                this.patient.Pact.Name = invoiceTemp.Patient.Pact.Name;
                this.patient.ID = invoiceTemp.Patient.ID;
                this.patient.DoctorInfo.SeeDate = ((FS.HISFC.Models.Registration.Register)invoiceTemp.Patient).DoctorInfo.SeeDate;
                this.patient.SSN = invoiceTemp.Patient.SSN;
                this.patient.ChkKind = invoiceTemp.ExamineFlag;
                this.patient.User03 = invoiceTemp.User03;//�����޶�ȼ�
                this.patient.LSH = invoiceTemp.User02;//���Ѽ��˵���
                this.oldPatient = this.patient.Clone();
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                isAccount = false;
                //this.patient.IsAccount = false;
                decimal totCost = 0;
                decimal ownCost = 0;
                decimal payCost = 0;
                decimal pubCost = 0;
                foreach (Balance invoice in quitInvoices)
                {
                    totCost += invoice.FT.TotCost;
                    ownCost += invoice.FT.OwnCost;
                    payCost += invoice.FT.PayCost;
                    pubCost += invoice.FT.PubCost;
                }
                this.tbTotCost.Text = totCost.ToString();
                this.tbOwnCost.Text = ownCost.ToString();
                this.tbPayCost.Text = payCost.ToString();
                this.tbPubCost.Text = pubCost.ToString();

                if (this.GetItemList() == -1)
                {
                    return;
                }

                if (this.fpSpread1_Sheet1.RowCount > 0)
                {
                    this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                }
                else
                {
                    this.fpSpread1.Focus();
                    this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
                    this.fpSpread1_Sheet2.ActiveRowIndex = 0;
                }
                //Ĭ��ȫ�˲���
               // AllQuit();
            }
        }

        protected virtual void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //�Ƿ��������
            if (IsAllowQuitFeeHalf == false)
            {
                MessageBox.Show("û�а���Ȩ��");
                return;
            }


            //��¼Ŀǰ�Ƿ���Ҫȫ��
            bool isNeedAllQuitStatus = this.isNeedAllQuit;
            bool isCheckAll = this.ckbAllQuit.Checked;

            try
            {
                //��ҩ��Ҫȫ��
                if (this.fpSpread1.ActiveSheetIndex == 0)
                {
                    FeeItemList drugItem = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FeeItemList;
                    if (drugItem != null && drugItem.Item.ItemType == EnumItemType.Drug && drugItem.Item.SysClass.ID.Equals(FS.HISFC.Models.Base.EnumSysClass.PCC.ToString()))
                    {
                        this.isNeedAllQuit = true;
                        this.ckbAllQuit.Checked = true;
                    }
                }

                if (this.fpSpread1.ActiveSheet.RowCount > 0)
                {
                    //if (this.patient.Pact.PayKind.ID == "02")
                    //{
                    //    MessageBox.Show("ҽ�����߱����ȫ��");
                    //    //this.Clear();
                    //    return;
                    //}
                    this.DealQuitOperation();
                }
            }
            finally
            {
                this.isNeedAllQuit = isNeedAllQuitStatus;
                this.ckbAllQuit.Checked = isCheckAll;
            }


        }

        protected virtual void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //�Ƿ��������
            if (IsAllowQuitFeeHalf == false)
            {
                MessageBox.Show("û�а���Ȩ��");
                return;
            }

            //��¼Ŀǰ�Ƿ���Ҫȫ��
            bool isNeedAllQuitStatus = this.isNeedAllQuit;


            try
            {
                //��ҩ��Ҫȫ��
                if (this.fpSpread2.ActiveSheetIndex == 0)
                {
                    FeeItemList drugItem = this.fpSpread2_Sheet1.Rows[e.Row].Tag as FeeItemList;
                    if (drugItem != null && drugItem.Item.ItemType == EnumItemType.Drug && drugItem.Item.SysClass.ID.Equals(FS.HISFC.Models.Base.EnumSysClass.PCC.ToString()))
                    {
                        this.isNeedAllQuit = true;
                    }
                }

                if (this.fpSpread2.ActiveSheet.RowCount > 0)
                {
                    this.DealCancelQuitOperation();
                }
            }
            finally
            {
                this.isNeedAllQuit = isNeedAllQuitStatus;
            }
        }

        protected virtual void ckbAllQuit_Click(object sender, System.EventArgs e)
        {
            if (this.ckbAllQuit.Checked)
            {
                this.txtReturnNum.Enabled = false;
            }
            else
            {
                this.txtReturnNum.Enabled = true;
            }
        }

        protected virtual void txtReturnNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QuitItemByNum();
            }
        }

        protected virtual void fpSpread1_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)
            {
                this.fpSpread2.ActiveSheet = this.fpSpread2_Sheet1;
            }
            else
            {
                if (this.fpSpread2.ActiveSheet != null)
                {
                    this.fpSpread2.ActiveSheet = this.fpSpread2_Sheet2;
                }
            }
        }

        protected virtual void fpSpread2_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet1)
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
            }
            else
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
            }
        }

        protected virtual void frmQuitFee_Load(object sender, System.EventArgs e)
        {
            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
            try
            {
                this.isNeedAllQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT, false, false);
                this.chkGoupAllQuit.Checked = isNeedAllQuit;
                if (this.Init() < 0)
                {
                    return;
                }

                this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            }
            catch
            {
            }
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳�����
                //�رմ���֮ǰ,���������Ŀ�б��߳�û�н���,ǿ�н���,�����߳�����
                this.ucDisplay1.threadItemInit.Abort();
            }
            catch { }
        }

        protected virtual void fpSpread1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //�Ƿ��������
                if (IsAllowQuitFeeHalf == false)
                {
                    MessageBox.Show("û�а���Ȩ��");
                    return;
                }
                if (this.fpSpread1.ActiveSheet.RowCount > 0)
                {
                    //if (this.patient.Pact.PayKind.ID == "02")
                    //{
                    //    MessageBox.Show("ҽ�����߱����ȫ��");
                    //    //this.Clear();
                    //    return;
                    //}
                    this.DealQuitOperation();
                }

            }
        }

        protected virtual void fpSpread2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //�Ƿ��������
                if (IsAllowQuitFeeHalf == false)
                {
                    MessageBox.Show("û�а���Ȩ��");
                    return;
                }
                if (this.fpSpread2.ActiveSheet.RowCount > 0)
                {
                    this.DealCancelQuitOperation();
                }
            }
        }

        protected virtual void chkGoupAllQuit_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chkGoupAllQuit.Checked)
            {
                this.isNeedAllQuit = true;
            }
            else
            {
                this.isNeedAllQuit = false;
            }
        }

        protected virtual void ckbAllQuit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbAllQuit.Checked)
            {
                this.txtReturnNum.Enabled = false;
                this.txtReturnNum.Text = string.Empty;
                this.txtReturnNum.Tag = null;
            }
            else
            {
                this.txtReturnNum.Enabled = true;
            }
        }

        #endregion


        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader && this.fpSpread1_Sheet2.RowHeader.Cells[e.Row, 0].Text == "+" &&
                this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)
            {
                ExpandOrCloseRow(false, e.Row + 1);
                return;
            }
            if (e.RowHeader && fpSpread1_Sheet2.RowHeader.Cells[e.Row, 0].Text == "-" &&
                this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)
            {
                ExpandOrCloseRow(true, e.Row + 1);
                return;
            }
        }
        /// <summary>
        /// �۵���ʾ��������
        /// </summary>
        /// <param name="isExpand"></param>
        /// <param name="index"></param>
        private void ExpandOrCloseRow(bool isExpand, int index)
        {

            for (int i = index; i < fpSpread1_Sheet2.Rows.Count; i++)
            {
                if (this.fpSpread1_Sheet2.RowHeader.Cells[i, 0].Text == "." && this.fpSpread1_Sheet2.Rows[i].Visible == isExpand)
                {
                    this.fpSpread1_Sheet2.Rows[i].Visible = !isExpand;
                }
                else
                {
                    break;
                }
            }
            if (isExpand)
            {
                fpSpread1_Sheet2.RowHeader.Cells[index - 1, 0].Text = "+";
            }
            else
            {
                fpSpread1_Sheet2.RowHeader.Cells[index - 1, 0].Text = "-";
            }
        }

        /// <summary>
        /// �������������յķ�ҩƷ����Ӧ����
        /// </summary>
        /// <param name="rowIndex">�������ڵ���</param>
        /// <returns></returns>
        private int FinItemRowIndex(int rowIndex)
        {
            for (int i = rowIndex; i >= 0; i--)
            {
                if (this.fpSpread1_Sheet2.RowHeader.Cells[i, 0].Text != ".")
                    return i;
            }
            return -1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.ucDisplay1.DeleteRow();
        }

        private void neuTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            ArrayList addItemList = this.ucDisplay1.GetFeeItemList();
            if (addItemList == null)
            {
                return;
            }

            if (addItemList.Count > 0)
            {

                if (this.cmbRegDept.Tag == null || this.cmbRegDept.Tag.ToString() == string.Empty || this.cmbRegDept.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("��ѡ���շ��õĿ������!");

                    return;
                }

                if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == string.Empty || this.cmbDoct.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("��ѡ���շ��õĿ���ҽ��!");

                    return;
                }
            }

            ArrayList phList = new ArrayList(); //ҩƷ�б�
            ArrayList itemList = new ArrayList();//��ҩƷ�б�

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in addItemList)
            {
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    phList.Add(f);
                }
                else
                {
                    itemList.Add(f);
                }
            }

            for (int i = this.fpSpread1_Sheet1.RowCount - 1; i >= 0; i--)
            {
                if (this.fpSpread1_Sheet1.RowHeader.Cells[i, 0].Text == "����")
                {
                    this.fpSpread1_Sheet1.Rows.Remove(i, 1);
                }
            }
            for (int i = this.fpSpread1_Sheet2.RowCount - 1; i >= 0; i--)
            {
                if (this.fpSpread1_Sheet2.RowHeader.Cells[i, 0].Text == "����")
                {
                    this.fpSpread1_Sheet2.Rows.Remove(i, 1);
                }
            }

            int phOrgCount = this.fpSpread1_Sheet1.RowCount;

            this.fpSpread1_Sheet1.RowCount += phList.Count; //ҩƷ.

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList drugItem in phList)
            {
                //this.fpSpread1_Sheet1.Rows[phOrgCount].Tag = drugItem;


                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.ItemName].Text = drugItem.Item.Name;

                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.CombNo].Text = drugItem.Order.Combo.ID;

                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.Specs].Text = drugItem.Item.Specs;
                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.Amount].Text = drugItem.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty / drugItem.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.PriceUnit].Text = drugItem.Item.PriceUnit;
                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.NoBackQty].Text = drugItem.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty / drugItem.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.Cost].Text = (drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost).ToString();

                if (drugItem.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit + " " + "����:" + drugItem.Days.ToString();
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit;
                }

                FS.HISFC.Components.OutpatientFee.Class.Function.DrawCombo(this.fpSpread1_Sheet1, (int)DrugList.CombNo, (int)DrugList.Comb, 0);

                this.fpSpread1_Sheet1.RowHeader.Cells[phOrgCount, 0].Text = "����";

                phOrgCount++;
            }

            int unDrugOrgCount = this.fpSpread1_Sheet2.RowCount;

            this.fpSpread1_Sheet2.RowCount += itemList.Count; //ҩƷ.

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList undrugItem in itemList)
            {

                #region ����������Ϣ
                //{143CA424-7AF9-493a-8601-2F7B1D635027}
                string outNo = undrugItem.UpdateSequence.ToString();
                List<FS.HISFC.Models.FeeStuff.Output> list = mateIntegrate.QueryOutput(outNo);
                undrugItem.MateList = list;
                #endregion

                if (undrugItem.FT.RebateCost > 0)
                {
                    isHaveRebateCost = true;
                }

                undrugItem.FT.TotCost = undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost;
                //this.fpSpread1_Sheet2.Rows[unDrugOrgCount].Tag = undrugItem;


                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.ItemName].Text = undrugItem.Item.Name;
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.CombNo].Text = undrugItem.Order.Combo.ID;
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.Amount].Text = undrugItem.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty / undrugItem.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.PriceUnit].Text = undrugItem.Item.PriceUnit;
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.NoBackQty].Text = undrugItem.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty / undrugItem.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.Cost].Text = (undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost).ToString();
                if (undrugItem.UndrugComb.ID != null && undrugItem.UndrugComb.ID.Length > 0)
                {
                    this.undrugComb = this.undrugManager.GetValidItemByUndrugCode(undrugItem.UndrugComb.ID);
                    if (this.undrugComb == null)
                    {
                        MessageBox.Show("���������Ϣ�����޷���ʾ�����Զ����룬���ǲ�Ӱ���˷Ѳ�����");
                    }
                    else
                    {
                        undrugItem.UndrugComb.UserCode = this.undrugComb.UserCode;
                    }

                    FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                    if (item == null)
                    {
                        this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name;
                    }
                    else
                    {
                        this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name + "[" + item.UserCode + "]";
                    }

                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                    if (item != null)
                    {
                        this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.PackageName].Text = item.UserCode;
                    }
                }

                FS.HISFC.Components.OutpatientFee.Class.Function.DrawCombo(this.fpSpread1_Sheet2, (int)UndrugList.CombNo, (int)UndrugList.Comb, 0);
                //��ʾ������Ϣ
                SetMateData(undrugItem, unDrugOrgCount);

                this.fpSpread1_Sheet2.RowHeader.Cells[unDrugOrgCount, 0].Text = "����";

                unDrugOrgCount++;
            }

        }
        //{E3C20659-CA54-457b-A907-650EEA30516C} ���������س��¼�
        private void cmbRegDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbDoct.Focus();
            }
        }

        //{B5A6A688-2711-4b8a-9029-7D5C29436E81}
        private void cmbRegDept_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.ucDisplay1.PatientInfo != null)
            {
                this.ucDisplay1.PatientInfo.DoctorInfo.Templet.Dept.ID = this.cmbRegDept.Tag.ToString();
            }
        }


        private void cmbDoct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ucDisplay1.SetFocus();
            }
        }

        //{B5A6A688-2711-4b8a-9029-7D5C29436E81}
        private void cmbDoct_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.ucDisplay1.PatientInfo != null)
            {
                this.ucDisplay1.PatientInfo.DoctorInfo.Templet.Doct.ID = this.cmbDoct.Tag.ToString();
            }
        }

        //{E3C20659-CA54-457b-A907-650EEA30516C} ���

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}

        #region �˻�����

        protected virtual void tbCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string markNO = this.tbCardNo.Text.Trim();
            if (string.IsNullOrEmpty(markNO))
            {
                MessageBox.Show("��������￨�ţ�");
                this.tbCardNo.Focus();
                return;
            }
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            if (feeIntegrate.ValidMarkNO(markNO, ref accountCard) <= 0)
            {
                MessageBox.Show(feeIntegrate.Err);
                return;
            }
            GetFeeList(accountCard.Patient);


        }

        protected virtual int GetFeeList(FS.HISFC.Models.RADT.PatientInfo p)
        {
            DateTime beginTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;
            int returnValues = FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref beginTime, ref endTime);
            if (returnValues < 0)
            {
                return -1;
            }

            this.patient.PID = p.PID;
            this.patient.Name = p.Name;
            this.patient.Pact = p.Pact;
            this.patient.Birthday = p.Birthday;
            this.patient.Sex = p.Sex;

            FT ft = new FT();
            if (GetList(p.PID.CardNO, beginTime, endTime, ref ft) < 0)
            {
                return -1;
            }
            this.tbName.Text = p.Name;
            this.tbPactName.Text = p.Pact.Name;
            this.tbPayCost.Text = ft.PayCost.ToString();
            this.tbOwnCost.Text = ft.OwnCost.ToString();
            this.tbPubCost.Text = ft.PubCost.ToString();
            this.tbTotCost.Text = ft.TotCost.ToString();
            isAccount = true;
            return 1;
        }

        /// <summary>
        /// ��ʾ���߷�����Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        protected virtual int GetList(string cardNO, DateTime beginDate, DateTime endDate, ref FT ft)
        {
            try
            {
                ArrayList drugItemLists = outpatientManager.GetDrugFeeByCardNODate(cardNO, beginDate, endDate, true);
                if (drugItemLists == null)
                {
                    MessageBox.Show("���ҩƷ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
                ArrayList undrugItemLists = outpatientManager.GetDrugFeeByCardNODate(cardNO, beginDate, endDate, false);
                if (undrugItemLists == null)
                {
                    MessageBox.Show("��÷�ҩƷ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
                if (drugItemLists.Count + undrugItemLists.Count == 0)
                {
                    MessageBox.Show("û�з�����Ϣ!");

                    return -1;
                }

                ArrayList drugConfirmList = new ArrayList();//�Ѿ���׼����ҩ��Ϣ
                ArrayList undrugConfirmList = new ArrayList();//�Ѿ���׼�˷ѵķ�ҩƷ��Ϣ
                //ѭ�����в����˷ѵķ�Ʊ,��ѯ�Ѿ���׼��ҩƷ�ͷ�ҩƷ��Ϣ
                //���ڶ��ŷ�Ʊ�Ĵ���,����ϸֻ��Ӧһ����Ʊ��,���Ա����еĲ����˷ѵķ�Ʊ,����ֻ��һ����Ʊ�ŷ��ϲ�ѯ����.
                //foreach (Balance balance in this.quitInvoices)
                //{
                //����Ѿ�������Ѿ���׼�˷ѵ�ҩƷ��Ϣ,�Ͳ��ٻ�ȡ
                if (drugConfirmList == null || drugConfirmList.Count == 0)
                {
                    //����Ѿ���׼����ҩ��Ϣ
                    drugConfirmList = returnApplyManager.GetApplyReturn(cardNO, true, false, true);
                    if (drugConfirmList == null)
                    {
                        MessageBox.Show("���ȷ��ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                        return -1;
                    }
                }
                //����Ѿ�������Ѿ���׼�˷ѵķ�ҩƷ��Ϣ,�Ͳ��ٻ�ȡ
                if (undrugConfirmList == null || undrugConfirmList.Count == 0)
                {
                    //����Ѿ���׼�˷ѵķ�ҩƷ��Ϣ
                    undrugConfirmList = returnApplyManager.GetApplyReturn(cardNO, true, false, false);
                    if (undrugConfirmList == null)
                    {
                        MessageBox.Show("���ȷ�Ϸ�ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                        return -1;
                    }
                }
                //}
                //��ʾ����ҩƷ��Ϣ
                this.fpSpread1_Sheet1.RowCount = drugItemLists.Count;

                FeeItemList drugItem = null;//ҩƷ��ʱʵ��
                for (int i = 0; i < drugItemLists.Count; i++)
                {
                    drugItem = drugItemLists[i] as FeeItemList;

                    if (drugItem.FT.RebateCost > 0)
                    {
                        isHaveRebateCost = true;
                    }


                    //���¼��㱾��ҩƷ���ܽ��,�����Ժ����������
                    drugItem.FT.TotCost = drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost;

                    this.fpSpread1_Sheet1.Rows[i].Tag = drugItem;
                    //��Ϊ���ܴ���ͬһ��Ʊ�в�ͬ������ҵ����,���ҹҺ���Ϣ�еĿ�����Ϣ��һ����ʵ���շѵĿ���
                    //������ͬ,��������ѹҺ�ʵ��Ŀ�����Ǹ�ֵΪ�շ���ϸʱ�Ŀ��������Ϣ.
                    this.patient.DoctorInfo.Templet.Dept = drugItem.RecipeOper.Dept;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.ItemName].Text = drugItem.Item.Name;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.CombNo].Text = drugItem.Order.Combo.ID;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Specs].Text = drugItem.Item.Specs;
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text = drugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty / drugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.PriceUnit].Text = drugItem.Item.PriceUnit;
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = drugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty / drugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Cost].Text = (drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost).ToString();

                    if (drugItem.Item.SysClass.ID.ToString() == "PCC")
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit + " " + "����:" + drugItem.Days.ToString();
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit;
                    }
                    //cost += drugItem.FT.TotCost;
                    ft.TotCost += drugItem.FT.TotCost;
                    ft.OwnCost += drugItem.FT.OwnCost;
                    ft.PubCost += drugItem.FT.PubCost;
                    ft.PayCost += drugItem.FT.PayCost;
                    FS.HISFC.Components.OutpatientFee.Class.Function.DrawCombo(this.fpSpread1_Sheet1, (int)DrugList.CombNo, (int)DrugList.Comb, 0);
                }

                //��ʾ��ҩƷ��Ϣ
                this.fpSpread1_Sheet2.RowCount = undrugItemLists.Count;

                FeeItemList undrugItem = null;
                for (int i = 0; i < undrugItemLists.Count; i++)
                {
                    undrugItem = undrugItemLists[i] as FeeItemList;

                    #region ����������Ϣ
                    //{143CA424-7AF9-493a-8601-2F7B1D635027}
                    //string outNo = undrugItem.UpdateSequence.ToString();
                    //List<HISFC.Object.Material.Output> list = mateIntegrate.QueryOutput(outNo);
                    //undrugItem.MateList = list;
                    #endregion

                    if (undrugItem.FT.RebateCost > 0)
                    {
                        isHaveRebateCost = true;
                    }

                    undrugItem.FT.TotCost = undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost;
                    this.fpSpread1_Sheet2.Rows[i].Tag = undrugItem;
                    this.patient.DoctorInfo.Templet.Dept = undrugItem.RecipeOper.Dept;

                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.ItemName].Text = undrugItem.Item.Name;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.CombNo].Text = undrugItem.Order.Combo.ID;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text = undrugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty / undrugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PriceUnit].Text = undrugItem.Item.PriceUnit;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.NoBackQty].Text = undrugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty / undrugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Cost].Text = (undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost).ToString();
                    if (undrugItem.UndrugComb.ID != null && undrugItem.UndrugComb.ID.Length > 0)
                    {
                        this.undrugComb = this.undrugManager.GetValidItemByUndrugCode(undrugItem.UndrugComb.ID);
                        if (this.undrugComb == null)
                        {
                            MessageBox.Show("���������Ϣ�����޷���ʾ�����Զ����룬���ǲ�Ӱ���˷Ѳ�����");
                        }
                        else
                        {
                            undrugItem.UndrugComb.UserCode = this.undrugComb.UserCode;
                        }

                        FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                        if (item == null)
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name;
                        }
                        else
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name + "[" + item.UserCode + "]";
                        }

                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                        if (item != null)
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = item.UserCode;
                        }
                    }
                    //cost += undrugItem.FT.TotCost;
                    ft.TotCost += undrugItem.FT.TotCost;
                    ft.OwnCost += undrugItem.FT.OwnCost;
                    ft.PubCost += undrugItem.FT.PubCost;
                    ft.PayCost += undrugItem.FT.PayCost;
                    FS.HISFC.Components.OutpatientFee.Class.Function.DrawCombo(this.fpSpread1_Sheet2, (int)UndrugList.CombNo, (int)UndrugList.Comb, 0);
                    //��ʾ������Ϣ
                    //SetMateData(undrugItem, i);
                }
                //��ʾȷ����ҩ��Ϣ
                this.fpSpread2_Sheet1.RowCount = drugItemLists.Count + drugConfirmList.Count;
                FS.HISFC.Models.Fee.ReturnApply drugReturn = null;
                for (int i = 0; i < drugConfirmList.Count; i++)
                {
                    drugReturn = drugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                    this.fpSpread2_Sheet1.Rows[i].Tag = drugReturn;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.ItemName].Text = drugReturn.Item.Name;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Amount].Text = drugReturn.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty / drugReturn.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.PriceUnit].Text = drugReturn.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Specs].Text = drugReturn.Item.Specs;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Flag].Text = "ȷ��";

                    int findRow = FindItem(drugReturn.RecipeNO, drugReturn.SequenceNO, this.fpSpread1_Sheet1);
                    if (findRow == -1)
                    {
                        MessageBox.Show("����δ��ҩ��Ŀ����!");

                        return -1;
                    }
                    FeeItemList modifyDrug = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;

                    modifyDrug.NoBackQty = modifyDrug.NoBackQty - drugReturn.Item.Qty;
                    modifyDrug.Item.Qty = modifyDrug.Item.Qty - drugReturn.Item.Qty;
                    modifyDrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Price * modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2);
                    modifyDrug.FT.OwnCost = modifyDrug.FT.TotCost;

                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = modifyDrug.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = modifyDrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = modifyDrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.NoBackQty / modifyDrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.NoBackQty, 2).ToString();
                }
                this.fpSpread2_Sheet2.RowCount = undrugItemLists.Count + undrugConfirmList.Count;
                FS.HISFC.Models.Fee.ReturnApply undrugReturn = null;
                for (int i = 0; i < undrugConfirmList.Count; i++)
                {
                    undrugReturn = undrugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                    this.fpSpread2_Sheet2.Rows[i].Tag = undrugReturn;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.ItemName].Text = undrugReturn.Item.Name;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Amount].Text = undrugReturn.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugReturn.Item.Qty / undrugReturn.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugReturn.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.PriceUnit].Text = undrugReturn.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Flag].Text = "ȷ��";

                    int findRow = FindItem(undrugReturn.RecipeNO, undrugReturn.SequenceNO, this.fpSpread1_Sheet2);
                    if (findRow == -1)
                    {
                        MessageBox.Show("����δ�˷�ҩ��Ŀ����!");

                        return -1;
                    }
                    FeeItemList modifyUndrug = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;

                    modifyUndrug.NoBackQty = modifyUndrug.NoBackQty - undrugReturn.Item.Qty;
                    modifyUndrug.Item.Qty = modifyUndrug.Item.Qty - undrugReturn.Item.Qty;
                    modifyUndrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Price * modifyUndrug.Item.Qty / modifyUndrug.Item.PackQty, 2);
                    modifyUndrug.FT.OwnCost = modifyUndrug.FT.TotCost;

                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Cost].Text = modifyUndrug.FT.TotCost.ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Amount].Text = modifyUndrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Qty / modifyUndrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = modifyUndrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.NoBackQty / modifyUndrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.NoBackQty, 2).ToString();

                }

                if (isHaveRebateCost)
                {
                    this.ckbAllQuit.Checked = true;
                    this.ckbAllQuit.Enabled = false;
                }
                else
                {
                    this.ckbAllQuit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }


            return 1;

        }

        /// <summary>
        /// ���Ϸ�����Ϣ
        /// </summary>
        /// <param name="f"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        private int SaveAccountQuiteFee()
        {

            DialogResult diaResult = MessageBox.Show("�Ƿ�Ҫ�˷�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (diaResult == DialogResult.No)
            {
                return -1;
            }

            if (!feeIntegrate.CheckAccountPassWord(this.patient))
            {
                return -1;
            }

            if (!IsQuitItem())
            {
                return -1;
            }

            ArrayList alFee = new ArrayList();
            FeeItemList tempf = null;
            DateTime nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            int iReturn;
            FeeItemList f = null;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread1.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag == null) continue;

                    f = (sv.Rows[i].Tag as FeeItemList).Clone();

                    #region ������������
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (f.IsConfirmed == false)
                        {
                            iReturn = pharmacyIntegrate.CancelApplyOutClinic(f.RecipeNO, f.SequenceNO);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���Ϸ�ҩ�������!" + pharmacyIntegrate.Err);
                                return -1;
                            }
                        }

                        tempf = f.Clone();
                        tempf.FT.OwnCost = tempf.FT.PubCost = tempf.FT.PayCost = 0;
                        tempf.FT.OwnCost = tempf.FT.TotCost;
                        //if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text) > 0)//by yuyun ��������ѷ�ҩ֮�������ˣ��˷�ʱȫ�˵����
                        if (f.Item.Qty > 0)
                        {
                            tempf.User03 = "HalfQuit";
                            alFee.Add(tempf);
                        }
                    }
                    else
                    {
                        //��δȷ�ϵ���ҩ��������ҩ����!
                        if (f.IsConfirmed == false)
                        {
                            iReturn = confirmIntegrate.CancelConfirmTerminal(f.Order.ID, f.Item.ID);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�����ն��������!" + confirmIntegrate.Err);

                                return -1;
                            }
                        }
                        else
                        {
                            #region �����ն�ȷ����Ϣ
                            tempf = f.Clone();
                            tempf.FT.OwnCost = tempf.FT.PubCost = tempf.FT.PayCost = 0;
                            tempf.FT.OwnCost = tempf.FT.TotCost;

                            //{06212A22-5FD4-4db3-838C-1790F75FF286}
                            //if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text) > 0)
                            if (f.Item.Qty > 0)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug unDrugTemp = this.undrugManager.GetUndrugByCode(f.Item.ID);
                                if (unDrugTemp != null)
                                {
                                    tempf.Item.IsNeedConfirm = unDrugTemp.IsNeedConfirm;
                                    tempf.Item.NeedConfirm = unDrugTemp.NeedConfirm;
                                    tempf.Item.IsNeedBespeak = unDrugTemp.IsNeedBespeak;
                                }

                                //{06212A22-5FD4-4db3-838C-1790F75FF286}
                                if (tempf.IsConfirmed == true)
                                {
                                    int row = this.FindItem(tempf.RecipeNO, tempf.SequenceNO, this.fpSpread2_Sheet2);
                                    if (row != -1)
                                    {
                                        FeeItemList quitItem = this.fpSpread2_Sheet2.Rows[row].Tag as FeeItemList;
                                        if (confirmIntegrate.UpdateOrDeleteTerminalConfirmApply(tempf.Order.ID, (int)(tempf.Item.Qty + quitItem.Item.Qty), (int)quitItem.Item.Qty, FS.FrameWork.Public.String.FormatNumber(tempf.Item.Price * tempf.Item.Qty, 2)) == -1)
                                        {
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("�����ն�ȷ����Ϣ����!" + confirmIntegrate.Err);
                                            return -1;
                                        }
                                    }
                                }
                                tempf.User03 = "HalfQuit";
                                alFee.Add(tempf);
                            }
                            #endregion
                        }

                    }

                    #endregion

                    #region ���·����˷ѱ��
                    if (outpatientManager.UpdateFeeItemListCancelType(f.RecipeNO, f.SequenceNO, CancelTypes.Canceled) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���ϻ�����ϸ����!" + outpatientManager.Err);
                        return -1;
                    }
                    #endregion

                    #region �帺��¼

                    FeeItemList feeItem = outpatientManager.GetFeeItemListForFee(f.RecipeNO, f.SequenceNO);
                    if (feeItem == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���ϻ�����ϸ����!" + outpatientManager.Err);
                        return -1;
                    }
                    feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    feeItem.FT.OwnCost = -feeItem.FT.OwnCost;
                    feeItem.FT.PayCost = -feeItem.FT.PayCost;
                    feeItem.FT.PubCost = -feeItem.FT.PubCost;
                    feeItem.FT.TotCost = feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost;
                    feeItem.Item.Qty = -feeItem.Item.Qty;
                    feeItem.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                    feeItem.FeeOper.ID = outpatientManager.Operator.ID;
                    feeItem.FeeOper.OperTime = nowTime;
                    feeItem.ChargeOper.OperTime = nowTime;
                    feeItem.ConfirmedInjectCount = 0;
                    feeItem.InvoiceCombNO = this.outpatientManager.GetTempInvoiceComboNO();
                    iReturn = outpatientManager.InsertFeeItemList(feeItem);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���������ϸ������Ϣ����!" + outpatientManager.Err);
                        return -1;
                    }
                    #endregion
                }
            }

            #region ����
            ArrayList drugList = new ArrayList();
            if (alFee.Count > 0)
            {
                foreach (FeeItemList item in alFee)
                {
                    item.FeeOper.OperTime = nowTime;
                    item.PayType = PayTypes.Balanced;
                    item.TransType = TransTypes.Positive;
                    item.InvoiceCombNO = outpatientManager.GetTempInvoiceComboNO();
                    if (outpatientManager.InsertFeeItemList(item) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���������ϸʧ�ܣ�" + outpatientManager.Err);
                        return -1;
                    }

                    //��ҩ����
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!f.IsConfirmed)
                        {
                            if (!f.Item.IsNeedConfirm)
                            {
                                drugList.Add(f);
                            }
                        }
                    }
                    else
                    {
                        //�ն�����
                        if (!f.IsConfirmed)
                        {
                            if (f.Item.NeedConfirm== FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Outpatient||f.Item.NeedConfirm== FS.HISFC.Models.Fee.Item.EnumNeedConfirm.All)
                            {
                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, this.patient);
                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�����ն�����ȷ�ϱ�ʧ��!" + confirmIntegrate.Err);

                                    return -1;
                                }
                            }
                        }
                    }

                }
                if (drugList.Count > 0)
                {
                    string drugSendInfo = string.Empty;
                    iReturn = this.pharmacyIntegrate.ApplyOut(patient, drugList, string.Empty, nowTime, false, out drugSendInfo);
                    if (iReturn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ҩƷ��ϸʧ��!" + pharmacyIntegrate.Err);
                        return -1;
                    }
                }
            }
            #endregion

            #region �����˷������˷ѱ��
            FS.HISFC.Models.Fee.ReturnApply returnApply = null;
            DateTime operDate = outpatientManager.GetDateTimeFromSysDateTime();
            string operCode = outpatientManager.Operator.ID;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        returnApply = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        returnApply.CancelType = CancelTypes.Valid;
                        returnApply.CancelOper.ID = operCode;
                        returnApply.CancelOper.OperTime = operDate;
                        if (returnApplyManager.UpdateApplyCharge(returnApply) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����������˷ѱ��ʧ�ܣ�" + returnApplyManager.Err);
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region �����˻�
            decimal cost = 0m;
            FS.HISFC.Models.Fee.ReturnApply applyItem = null;
            FS.HISFC.Models.Fee.Outpatient.FeeItemList fitem = null;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag == null) continue;
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        applyItem = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        cost += FS.FrameWork.Public.String.FormatNumber(applyItem.Item.Price * applyItem.Item.Qty / applyItem.Item.PackQty, 2);
                    }
                    if (sv.Rows[i].Tag is FeeItemList)
                    {
                        fitem = sv.Rows[i].Tag as FeeItemList;
                        cost += FS.FrameWork.Public.String.FormatNumber(fitem.Item.Price * fitem.Item.Qty / fitem.Item.PackQty, 2);
                    }
                }
            }
            if (feeIntegrate.AccountCancelPay(patient, -cost, "�����˷�", (outpatientManager.Operator as Employee).Dept.ID, string.Empty) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�˻��˷��뻧ʧ�ܣ�" + feeIntegrate.Err);
                return -1;
            }

            #endregion
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�����˻����" + cost.ToString() + "Ԫ");
            return 1;
        }
        #endregion


        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans);
                return type;
            }
        }
        #endregion


        #region ����ͨ�����Ų��ҷ�Ʊ��Ϣ,ʵ��Ԥ�����˷ѹ��� {2E5139C9-52D8-4fec-A96B-09BECFDDFBD1}

        #region ���÷�Ʊ����ؼ� ����

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    panelWidth = this.pnlLeft.Width;
        //    this.pnlLeft.Width = this.btnShow.Width;
        //    this.panelTree.Visible = false;
        //    this.dtpInvoiceDate.Visible = false;
        //    this.txtCardNO.Visible = false;
        //    this.btnClose.Visible = false;
        //    this.btnShow.Visible = true;
        //}
        //private int panelWidth = 0;
        //private void btnShow_Click(object sender, EventArgs e)
        //{
        //    if (panelWidth == 0)
        //    {
        //        panelWidth = 210;
        //    }
        //    this.pnlLeft.Width = panelWidth;
        //    this.btnShow.Visible = false;
        //    this.panelTree.Visible = true;
        //    this.dtpInvoiceDate.Visible = true;
        //    this.txtCardNO.Visible = true;
        //    this.btnClose.Visible = true;
        //}

        //private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode != Keys.Enter)
        //        return;

        //    string strCard = txtCardNO.Text.Trim();
        //    if (string.IsNullOrEmpty(strCard))
        //        return;

        //    HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
        //    int iTemp = feeIntegrate.ValidMarkNO(strCard, ref objCard);
        //    if (iTemp <= 0 || objCard == null)
        //    {
        //        MessageBox.Show("��Ч���ţ�����ϵ����Ա��");
        //        return ;
        //    }

        //    this.trvInvoice.Nodes.Clear();
        //    this.Clear();

        //    DateTime dtReg = this.dtpInvoiceDate.Value.Date;
        //    List<Balance> lstInvoice = null;
        //    iTemp = outpatientManager.QueryInvoiceInfoByCardNo(objCard.Patient.PID.CardNO, dtReg, DateTime.Now, out lstInvoice);
        //    if (iTemp <= 0)
        //    {
        //        MessageBox.Show(outpatientManager.Err);
        //        return;
        //    }

        //    if (lstInvoice == null || lstInvoice.Count <= 0)
        //    {
        //        MessageBox.Show("δ�ҵ���Ʊ��Ϣ��");
        //        return;
        //    }

        //    AddInvoiceToTree(lstInvoice);


        //}
        ///// <summary>
        ///// ��ӷ�Ʊ��Ϣ����
        ///// </summary>
        ///// <param name="lstInvoice"></param>
        //private void AddInvoiceToTree(List<Balance> lstInvoice)
        //{
        //    this.trvInvoice.Nodes.Clear();
        //    if (lstInvoice == null || lstInvoice.Count <= 0)
        //        return;

        //    foreach (Balance invoice in lstInvoice)
        //    {
        //        AddInvoiceToTree(invoice);
        //    }

        //}

        //private void AddInvoiceToTree(Balance invoice)
        //{
        //    if (invoice == null)
        //        return;

        //    TreeNode[] tnArr = trvInvoice.Nodes.Find(invoice.Patient.ID, true);

        //    TreeNode tn = null;
        //    TreeNode tnTemp = null;
        //    if (tnArr == null || tnArr.Length <= 0)
        //    {
        //        tn = new TreeNode();
        //        tn.Name = invoice.Patient.ID;
        //        tn.Text = ((FS.HISFC.Models.Registration.Register)invoice.Patient).DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");

        //        trvInvoice.Nodes.Add(tn);

        //        tnTemp = new TreeNode();
        //        tnTemp.Name = invoice.Invoice.ID + "-" + invoice.CombNO;
        //        tnTemp.Text = invoice.Invoice.ID;
        //        tnTemp.Tag = invoice;

        //        tn.Nodes.Add(tnTemp);
        //    }
        //    else
        //    {
        //        tnTemp = new TreeNode();
        //        tnTemp.Name = invoice.Invoice.ID + "-" + invoice.CombNO;
        //        tnTemp.Text = invoice.Invoice.ID;
        //        tnTemp.Tag = invoice;

        //        tn = tnArr[0];
        //        tn.Nodes.Add(tnTemp);
        //    }
        //}

        //private void trvInvoice_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    if (e.Node != null && e.Node.Tag != null)
        //    {
        //        this.Clear();

        //        Balance invoice = e.Node.Tag as Balance;
        //        if (invoice != null)
        //        {
        //            tbInvoiceNO.Text = invoice.Invoice.ID;

        //            System.Windows.Forms.KeyEventArgs keyEvent = new KeyEventArgs(Keys.Enter);

        //            tbInvoiceNo_KeyDown(sender, keyEvent);
        //        }

        //    }
        //}

        #endregion

        private void ucInvoiceView_evnInvoiceSelectChange(object sender, Balance invoice)
        {
            if (invoice != null)
            {
                tbInvoiceNO.Text = invoice.Invoice.ID;
                System.Windows.Forms.KeyEventArgs keyEvent = new KeyEventArgs(Keys.Enter);
                tbInvoiceNo_KeyDown(sender, keyEvent);
            }
        }

        bool blnShowInvoiceNoFind = true;
        private void ucInvoiceView_evnInvoiceNoFind()
        {
            if (blnShowInvoiceNoFind)
            {
                MessageBox.Show("δ�ҵ���Ʊ��Ϣ��");
            }
        }
        #endregion

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            if (string.IsNullOrEmpty(this.operationPriv))
            {
                return 1;
            }
            else
            {
                //����ǹ���Ա�������ֱ�ӽ���
                if (((FS.HISFC.Models.Base.Employee)outpatientManager.Operator).IsManager)
                {
                    return 1;
                }

                string[] privs = this.operationPriv.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                if (privs.Length==0)
                {
                    return 1;
                }
                else if (privs.Length == 1)//ֻ�ж���û�ж���Ȩ��
                {
                    if (CommonController.Instance.JugePrive(privs[0]) == false)
                    {
                        CommonController.Instance.MessageBox(this, "��û�в����˷ѵ�Ȩ�ޣ�������ȡ����", MessageBoxIcon.Stop);
                        return -1;
                    }
                }
                else
                {
                    string class2Code = privs[0];
                    string class3Code = privs[1];
                    if (CommonController.Instance.JugePrive(privs[0], privs[1]) == false)
                    {
                        CommonController.Instance.MessageBox(this, "��û�в����˷ѵ�Ȩ�ޣ�������ȡ����", MessageBoxIcon.Stop);
                        return -1;
                    }
                }
            }

            return 1;
        }

        #endregion
    }
}
