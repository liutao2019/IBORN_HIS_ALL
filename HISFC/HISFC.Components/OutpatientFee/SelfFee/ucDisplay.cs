using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Registration;
using FS.FrameWork.Function;
using FS.FrameWork.Models;
using FS.FrameWork.Management;
using FarPoint.Win.Spread;
using FS.HISFC.Models.Base;
using System.Threading;
using FS.HISFC.Components.OutpatientFee.Controls;

namespace FS.HISFC.Components.OutpatientFee.SelfFee
{
    public partial class ucDisplay : UserControl, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucDisplay()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ƿ��˷ѵ���
        /// </summary>
        protected bool isQuitFee = false;

        /// <summary>
        /// ������Ŀ�ؼ��Ƿ��ý���
        /// </summary>
        protected bool isFocus = false;

        /// <summary>
        /// �۸񾯽�����ɫ
        /// </summary>
        protected int priceWarinningColor = 0;

        /// <summary>
        /// �۸񾯽���
        /// </summary>
        protected decimal priceWarnning = 0;

        /// <summary>
        /// ÿ�������Ƿ����Ϊ��
        /// </summary>
        protected bool isDoseOnceNull = true;

        /// <summary>
        /// �����Ƿ���ȡ��
        /// </summary>
        protected bool isQtyToCeiling = false;

        /// <summary>
        /// �Ƿ����������Ŀ;
        /// </summary>
        protected bool isCanAddItem = false;

        /// <summary>
        /// ��ʾȱҩҩƷ
        /// </summary>
        protected bool displayLackPha = false;

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected Register rInfo = null;

        /// <summary>
        /// ��ʱ�Һſ���
        /// </summary>
        protected string tempDept = null;

        /// <summary>
        /// û�йҺŻ���,���ŵ�һλ��־,Ĭ����9��ͷ
        /// </summary>
        protected string noRegFlagChar = "9";

        /// <summary>
        /// ��ʱ�Һŷѷ��ñ���
        /// </summary>
        protected string regFeeItemCode = string.Empty;

        /// <summary>
        /// �Է�������Ŀ����
        /// </summary>
        protected string ownDiagFeeCode = string.Empty;

        /// <summary>
        /// ͨ�ùҺż���
        /// </summary>
        protected string comRegLevel = string.Empty;

        /// <summary>
        /// Ĭ�ϵ��շѰ�װ��λ
        /// </summary>
        protected string defaultPriceUnit = "0";

        /// <summary>
        /// Ƶ����ʾ��ʽ
        /// </summary>
        protected string freqDisplayType = "0";

        /// <summary>
        /// ������Ϣ
        /// </summary>
        protected string errText = string.Empty;

        /// <summary>
        /// ҽ�����룬�ú�ͬ��λ�Ķ�����Ϣ�е�ҽ��Ŀ¼�ȼ����ڹ���ʱ��Ŀ�ķ�����Ŀ��ʾ������
        /// </summary>
        protected string ybPactCode = string.Empty;

        /// <summary>
        /// �ԷѲ�����ʾҽ�����
        /// </summary>
        protected bool isOwnDisplayYB = false;

        /// <summary>
        /// �Ƿ���Ը��Ļ�����Ϣ
        /// </summary>
        protected bool isCanModifyCharge = false;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        protected ArrayList alChargeInfo = null;

        /// <summary>
        /// �Ƿ��жϿ��
        /// </summary>
        protected bool isJudgeStore = false;

        /// <summary>
        /// ��ӵ���
        /// </summary>
        private ArrayList alAddRows = new ArrayList();

        /// <summary>
        /// ���Ѵ���
        /// </summary>
        private ArrayList alBillPact = new ArrayList();

        /// <summary>
        /// ��ǰ�շ�����
        /// </summary>
        protected string recipeSeq = string.Empty;

        /// <summary>
        /// Ժע����
        /// </summary>
        private decimal injec = 0;

        /// <summary>
        /// Ĭ�ϲ�ҩ����
        /// </summary>
        private decimal hDays = 1;

        /// <summary>
        /// ����ֵ
        /// </summary>
        private int iReturn = 0;

        /// <summary>
        /// ��ǰ�ؼ��Ƿ���Ч
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ����Ա��Ϣ ����Ա������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.Employee myOperator = new FS.HISFC.Models.Base.Employee();
        /// <summary>
        /// ������Ŀ���
        /// </summary>
        protected FS.HISFC.Models.Base.ItemKind itemKind = FS.HISFC.Models.Base.ItemKind.All;

        #region ҵ������

        /// <summary>
        /// �Һ�ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �������ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ҽ��ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ��ҩƷ�����Ŀҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// ��ͬ��λ��������ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitItemRate pactUnitItemRateManager = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

        /// <summary>
        /// ҽ���ӿ�ҵ���(����)
        /// </summary>
        FS.HISFC.BizLogic.Fee.Interface interfaceManager = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// �Ż�ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.EcoRate ecoRateManager = new FS.HISFC.BizLogic.Fee.EcoRate();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �����ۺ�ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        #endregion

        #region �б����

        /// <summary>
        /// Ƶ���б�
        /// </summary>
        private ArrayList alFreq = new ArrayList();

        /// <summary>
        /// �÷��б�
        /// </summary>
        private ArrayList alUsage = new ArrayList();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private ArrayList alDept = new ArrayList();

        /// <summary>
        /// Ժע��Ŀ����
        /// </summary>
        private ArrayList alInjec = new ArrayList();

        #endregion

        /// <summary>
        /// ��Ʊ��Ϣ
        /// </summary>
        private DataSet dsInvoice = new DataSet();

        /// <summary>
        /// ���ص���Ŀ
        /// </summary>
        DataSet dsItem = new DataSet();

        /// <summary>
        /// ��Ŀ��ͼ
        /// </summary>
        DataView dvItem = new DataView();

        /// <summary>
        /// ת���ĵ�λ
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper invertUnitHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ÿ������λ����ת������Ŀ
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper specialInvertUnitHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ����ҩ�ѵ���С���ô���
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper phaFeeCodeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���Ѵ���
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper myBillPactHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper apprItemHelper = new FS.FrameWork.Public.ObjectHelper();
        
        /// <summary>
        /// 
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper specialItemHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �÷��б�, ���ұ����������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper myHelpUsage = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// Ƶ���б�, ���ұ����������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper myHelpFreq = new FS.FrameWork.Public.ObjectHelper();
        //{21C33D5B-5583-4b1d-8023-278336C0C6C7}
        FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade myIGetSiItemGrade = null;


        #region  �ؼ�����

        /// <summary>
        /// ִ�п���ѡ��
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox lbDept = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        /// <summary>
        /// Ƶ��ѡ���б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox lbFreq = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        /// <summary>
        /// �÷�ѡ���б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox lbUsage = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        /// <summary>
        /// Ժ��ע������ؼ�
        /// </summary>
        private Controls.ucInjec myInjec = new FS.HISFC.Components.OutpatientFee.Controls.ucInjec();

        private FS.HISFC.BizProcess.Integrate.FeeInterface.IChooseItemForOutpatient chooseItemControl;

        /// <summary>
        /// ����FarPoint
        /// </summary>
        private FarPoint.Win.Spread.SheetView fpSheetItem = new SheetView();

        /// <summary>
        /// �����Ϣ��ʾ�б�
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft leftControl = null;

        /// <summary>
        /// �Ҳ���Ϣ��ʾ�б�
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight rightControl = null;

        /// <summary>
        /// �Ƿ���CellChange�¼�
        /// </summary>
        private bool isDealCellChange = true;

        #endregion

        #region �¼�����

        /// <summary>
        /// ��Ŀ�б����仯�󴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.delegateFeeItemListChanged FeeItemListChanged;

        #endregion

        //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳�����
        public System.Threading.Thread threadItemInit = null;
        //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳����� ����

        /// <summary>
        /// �Ƿ����ѡ����Ŀ�շ�{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        /// </summary>
        protected bool isCanSelectItemAndFee = false;
        //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}����

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ����ѡ����Ŀ�շ�{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        /// </summary>
        public bool IsCanSelectItemAndFee 
        {
            get 
            {
                return this.isCanSelectItemAndFee;
            }
            set 
            {
                this.isCanSelectItemAndFee = value;

                this.SetIsCanSelectItemAndFee();
            }
        }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}����

        /// <summary>
        /// �Ƿ��˷ѵ���
        /// </summary>
        public bool IsQuitFee
        {
            get
            {
                return this.isQuitFee;
            }
            set
            {
                this.isQuitFee = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public FS.HISFC.Models.Base.ItemKind ItemKind
        {
            get
            {
                return this.itemKind;
            }
            set
            {
                this.itemKind = value;
              
            }
        }

        /// <summary>
        /// ��ǰ�ؼ��Ƿ���Ч
        /// </summary>
        public bool IsValid 
        {
            get 
            {
                return this.IsInputValid();
            }
            set 
            {
                this.isValid = value;
            }
        }

        /// <summary>
        /// �Ҳ���Ϣ��ʾ�б�
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight RightControl 
        {
            get 
            {
                return this.rightControl;
            }
            set 
            {
                this.rightControl = value;
            }
        }

        /// <summary>
        /// �����Ϣ��ʾ�б�
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft LeftControl 
        {
            get 
            {
                return this.leftControl;
            }
            set 
            {
                this.leftControl = value;
            }
        }

        /// <summary>
        /// ��ǰ�շ�����
        /// </summary>
        public string RecipeSequence 
        {
            get 
            {
                return this.recipeSeq;
            }
            set 
            {
                this.recipeSeq = value;
            }
        }

        /// <summary>
        /// �Ƿ��ý���
        /// </summary>
        public bool IsFocus 
        {
            get 
            {
                return this.isFocus;
            }
            set 
            {
                this.isFocus = value;
            }
        }

        /// <summary>
        /// �۸񾯽�����ɫ
        /// </summary>
        public int PriceWarinningColor 
        {
            get 
            {
                return this.priceWarinningColor;
            }
            set 
            {
                this.priceWarinningColor = value;
            }
        }


        /// <summary>
        /// �۸񾯽���
        /// </summary>
        public decimal PriceWarnning 
        {
            get 
            {
                return this.priceWarnning;
            }
            set 
            {
                this.priceWarnning = value;
            }
        }

        /// <summary>
        /// ÿ�������Ƿ����Ϊ��
        /// </summary>
        public bool IsDoseOnceNull 
        {
            get 
            {
                return this.isDoseOnceNull;
            }
            set 
            {
                this.isDoseOnceNull = value;
            }
        }

        /// <summary>
        /// �����Ƿ���ȡ��
        /// </summary>
        public bool IsQtyToCeiling 
        {
            get 
            {
                return this.isQtyToCeiling;
            }
            set 
            {
                this.isQtyToCeiling = value;
            }
        }

        /// <summary>
        /// �Ƿ����������Ŀ;
        /// </summary>
        public bool IsCanAddItem 
        {
            get 
            {
                return this.isCanAddItem;
            }
            set 
            {
                this.isCanAddItem = value;
            }
        }

        /// <summary>
        /// ��ʾȱҩҩƷ
        /// </summary>
        public bool IsDisplayLackPha 
        {
            get 
            {
                return this.displayLackPha;
            }
            set 
            {
                this.displayLackPha = value;
            }
        }

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        public Register PatientInfo 
        {
            get 
            {
                return this.rInfo;
            }
            set 
            {
                this.rInfo = value;
            }
        }

        /// <summary>
        /// ��ʱ�Һſ���
        /// </summary>
        public string RegisterDept 
        {
            get 
            {
                return this.tempDept;
            }
            set 
            {
                this.tempDept = value;
            }
        }
        /// <summary>
        /// û�йҺŻ���,���ŵ�һλ��־,Ĭ����9��ͷ
        /// </summary>
        
        public string NoRegFlagChar 
        {
            get 
            {
                return this.noRegFlagChar;
            }
            set 
            {
                this.noRegFlagChar = value;
            }
        }

        /// <summary>
        /// ��ʱ�Һŷѷ��ñ���
        /// </summary>
        public string RegFeeItemCode 
        {
            get 
            {
                return this.regFeeItemCode;
            }
            set 
            {
                this.regFeeItemCode = value;
            }
        }

        /// <summary>
        /// �Է�������Ŀ����
        /// </summary>
        public string OwnDiagFeeCode 
        {
            get 
            {
                return this.ownDiagFeeCode;
            }
            set 
            {
                this.ownDiagFeeCode = value;
            }
        }

        /// <summary>
        /// ͨ�ùҺż���
        /// </summary>
        public string ComRegLevel 
        {
            get 
            {
                return this.comRegLevel;
            }
            set 
            {
                this.comRegLevel = value;
            }
        }

        /// <summary>
        /// Ĭ�ϵ��շѰ�װ��λ
        /// </summary>
        public string DefaultPriceUnit 
        {
            get 
            {
                return this.defaultPriceUnit;
            }
            set 
            {
                this.defaultPriceUnit = value;
            }
        }

        /// <summary>
        /// Ƶ����ʾ��ʽ
        /// </summary>
        public string FreqDisplayType 
        {
            get 
            {
                return this.freqDisplayType;
            }
            set 
            {
                this.freqDisplayType = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrText 
        {
            get 
            {
                return this.errText;
            }
            set 
            {
                this.errText = value;
            }
        }

        /// <summary>
        /// ҽ�����룬�ú�ͬ��λ�Ķ�����Ϣ�е�ҽ��Ŀ¼�ȼ����ڹ���ʱ��Ŀ�ķ�����Ŀ��ʾ������
        /// �������ҽ����YBPactCode = 2
        /// </summary>
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
        /// �ԷѲ�����ʾҽ�����
        /// </summary>
        public bool IsOwnDisplayYB 
        {
            get 
            {
                return this.isOwnDisplayYB;
            }
            set 
            {
                this.isOwnDisplayYB = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ը��Ļ�����Ϣ
        /// </summary>
        public bool IsCanModifyCharge 
        {
            get 
            {
                return this.isCanModifyCharge;
            }
            set 
            {
                this.isCanModifyCharge = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public ArrayList ChargeInfoList 
        {
            get 
            {
                return this.alChargeInfo;
            }
            set 
            {
                this.alChargeInfo = value;

                if (value == null)
                {
                    return;
                }
                
                //���θ��¼�,����ȡ������Ϣʱ,���ж���ķ��ü���
                this.isDealCellChange = false;
                //��ʾ������Ϣ.
                this.SetChargeInfo();
                //�򿪸��¼�
                this.isDealCellChange = true;
            }
        }

        /// <summary>
        /// �Ƿ��жϿ��
        /// </summary>
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
        /// �շ���Ŀ�б�
        /// </summary>
        public DataSet FeeItem
        {
            get { return this.dsItem; }
            set { this.dsItem = value; }
        }

        #endregion

        #region ö��

        /// <summary>
        /// ��ö��{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        /// </summary>
        private enum Columns
        {
            /// <summary>
            /// ѡ��
            /// </summary>
            Select = 0,
            
            /// <summary>
            /// ������
            /// </summary>
            InputCode = 1,

            /// <summary>
            /// ����
            /// </summary>
            ItemName = 2,

            /// <summary>
            /// ��ʾ���
            /// </summary>
            CombNoDisplay = 3,

            /// <summary>
            /// ����
            /// </summary>
            Amount = 4,

            /// <summary>
            /// ��λ
            /// </summary>
            PriceUnit = 5,

            /// <summary>
            /// ����
            /// </summary>
            Days = 6,

            /// <summary>
            /// ÿ������
            /// </summary>
            DoseOnce = 7,

            /// <summary>
            /// ������λ
            /// </summary>
            DoseUnit = 8,

            /// <summary>
            /// ��Ϻ�
            /// </summary>
            CombNo = 9,

            /// <summary>
            /// Ƶ��
            /// </summary>
            Freq = 10,

            /// <summary>
            /// �÷�
            /// </summary>
            Usage = 11,

            /// <summary>
            /// ִ�п���
            /// </summary>
            ExeDept = 12,

            /// <summary>
            /// ���
            /// </summary>
            Cost = 13,

            /// <summary>
            /// �Է�ҩ
            /// </summary>
            Self = 14,

            /// <summary>
            /// С��
            /// </summary>
            LittleCost = 15,

            /// <summary>
            /// ����
            /// </summary>
            Price = 16,

            /// <summary>
            /// ��ע
            /// </summary>
            Memo = 17,

            /// <summary>
            /// ��С����
            /// </summary>
            FeeCode = 18,

            /// <summary>
            /// ��Ŀ���
            /// </summary>
            ItemType = 19,

            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemCode = 20,

            /// <summary>
            /// �Ƿ����
            /// </summary>
            Change = 21
        }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}����

        #endregion

        #region ����

        #region ˽�з���

        /// <summary>
        /// ��ʼ�����Ʋ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ��-1</returns>
        protected int InitControlParams() 
        {
            //�۸񾯽�����ɫ
            this.priceWarinningColor = this.controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.TOP_PRICE_WARNNING_COLOR, true, Color.Red.ToArgb());
            
            //�۸񾯽���
            this.priceWarnning = this.controlParamIntegrate.GetControlParam<decimal>(FS.HISFC.BizProcess.Integrate.Const.TOP_PRICE_WARNNING, true, 1000000);
            
            //ÿ�������Ƿ����Ϊ��
            this.isDoseOnceNull = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL, true, true);
            
            //�����Ƿ���ȡ��
            this.isQtyToCeiling = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.QTY_TO_CEILING, true, false);
            
            //��ʾȱҩҩƷ
            this.displayLackPha = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DISPLAY_LACK_PHAMARCY, true, false);
            
            //û�йҺŻ���,���ŵ�һλ��־,Ĭ����9��ͷ
            this.noRegFlagChar = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, true, "9");

            //��ʱ�Һŷѷ��ñ���
            this.regFeeItemCode = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.AUTO_REG_FEE_ITEM_CODE, true, string.Empty);
            
            //�Է�������Ŀ����
            this.ownDiagFeeCode = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.AUTO_PUB_FEE_DIAG_FEE_CODE, true, string.Empty);
            
            //ͨ�ùҺż���
            this.comRegLevel = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.COM_REG_LEVEL, true, string.Empty);
            
            //Ĭ�ϵ��շѰ�װ��λ
            this.defaultPriceUnit = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.PRICEUNIT, true, "0");
           
            //Ƶ����ʾ��ʽ
            this.freqDisplayType = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.FREQ_DISPLAY_TYPE, true, "0");

            //�ԷѲ�����ʾҽ�����
            this.isOwnDisplayYB = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.OWN_DISPLAY_YB, true, false);

            //�Ƿ���Ը��Ļ�����Ϣ
            this.isCanModifyCharge = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.MODIFY_CHARGE_INFO, true, false);

            //�Ƿ��жϿ��
            this.isJudgeStore = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.JUDGE_STORE, true, false);

            return 1;
        }

        /// <summary>
        /// ��ʼ����ͬ��λ���Ѵ���
        /// </summary>
        /// <returns></returns>
        private int InitBillPact()
        {
            try
            {
                ArrayList al = this.managerIntegrate.GetConstantList("BILLPACT");
                this.alBillPact = al;
            }
            catch (Exception ex)
            {
                MessageBox.Show("���غ�ͬ��λ���Ѵ�������!" + ex.Message, "��ʾ");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ��÷�Ʊ��Ϣ
        /// </summary>
        /// <returns>0 �ɹ� -1 ʧ��</returns>
        private int GetInvoiceClass()
        {
            int iReturn = this.outpatientManager.GetInvoiceClass("MZ01", ref dsInvoice);
            
            if (iReturn != -1)
            {
                dsInvoice.Tables[0].PrimaryKey = new DataColumn[] { dsInvoice.Tables[0].Columns["FEE_CODE"] };
            }

            return iReturn;
        }

        /// <summary>
        /// ���¼�������С�ƽ��
        /// �����²���һ����ɾ��һ�������߱����Ŀ��
        /// </summary>
        private void SumLittleCostAll()
        {
            decimal littleCost = 0;
            string tempName = string.Empty;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                tempName = this.fpSpread1_Sheet1.Cells[i, (int)Columns.ItemName].Text;
                if (tempName == "С��")
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Cost].Text = littleCost.ToString();
                    littleCost = 0;
                }
                else
                {
                    littleCost += NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Columns.Cost].Text);
                }
            }
        }

        /// <summary>
        /// ��ѯ��Ŀ��ȫ��ģ����ѯ ����ƴ������ʣ��Զ��壬��Ŀ���ƣ�����������ƴ������ʣ��Զ���
        /// </summary>
        /// <param name="inputCode">����ı���</param>
        /// <param name="row">��ǰ��</param>
        private void QueryItem(string inputCode, int row)
        {
            ClearRow(row);
            SumCost();
            string sFilter = string.Empty;

            this.chooseItemControl.IsSelectItem = false;

            switch (this.chooseItemControl.QueryType)
            {
                case "0":
                    sFilter = "SPELL_CODE like '%" + inputCode + "'" +
                    " OR " + "WB_CODE like '%" + inputCode + "'" +
                    " OR " + "User_Code like '%" + inputCode.PadLeft(6, '0') + "'" +
                    " OR " + "ITEM_NAME like '%" + inputCode + "'" +
                    " OR " + "CUS_SPELL_CODE like '%" + inputCode + "'" +
                    " OR " + "CUS_WB_CODE like '%" + inputCode + "'" +
                    " OR " + "CUS_User_Code like '%" + inputCode + "'" +
                    " OR " + "CUS_NAME like '%" + inputCode + "'" +
                    " OR " + "OTHER_NAME like '%" + inputCode + "'" +
                    " OR " + "OTHER_SPELL like '%" + inputCode + "'" +
                    " OR " + "EN_NAME like '%" + inputCode + "'";
                    break;
                case "1":
                    sFilter = "SPELL_CODE like '" + inputCode + "%'" +
                    " OR " + "WB_CODE like '" + inputCode + "%'" +
                    " OR " + "User_Code like '" + inputCode.PadLeft(6, '0') + "%'" +
                    " OR " + "ITEM_NAME like '" + inputCode + "%'" +
                    " OR " + "CUS_SPELL_CODE like '" + inputCode + "%'" +
                    " OR " + "CUS_WB_CODE like '" + inputCode + "%'" +
                    " OR " + "CUS_User_Code like '" + inputCode + "%'" +
                    " OR " + "CUS_NAME like '" + inputCode + "%'" +
                    " OR " + "OTHER_NAME like '" + inputCode + "%'" +
                    " OR " + "OTHER_SPELL like '" + inputCode + "%'" +
                    " OR " + "EN_NAME like '" + inputCode + "%'";
                    break;
                case "2":
                    sFilter = "SPELL_CODE like '%" + inputCode + "%'" +
                    " OR " + "WB_CODE like '%" + inputCode + "%'" +
                    " OR " + "User_Code like '%" + inputCode.PadLeft(6, '0') + "%'" +
                    " OR " + "ITEM_NAME like '%" + inputCode + "%'" +
                    " OR " + "CUS_SPELL_CODE like '%" + inputCode + "%'" +
                    " OR " + "CUS_WB_CODE like '%" + inputCode + "%'" +
                    " OR " + "CUS_User_Code like '%" + inputCode + "%'" +
                    " OR " + "CUS_NAME like '%" + inputCode + "%'" +
                    " OR " + "OTHER_NAME like '%" + inputCode + "%'" +
                    " OR " + "OTHER_SPELL like '%" + inputCode + "%'" +
                    " OR " + "EN_NAME like '%" + inputCode + "%'";
                    break;
                case "3":
                    sFilter = "SPELL_CODE like '" + inputCode + "'" +
                    " OR " + "WB_CODE like '" + inputCode + "'" +
                    " OR " + "User_Code like '" + inputCode.PadLeft(6, '0') + "'" +
                    " OR " + "ITEM_NAME like '" + inputCode + "'" +
                    " OR " + "CUS_SPELL_CODE like '" + inputCode + "'" +
                    " OR " + "CUS_WB_CODE like '" + inputCode + "'" +
                    " OR " + "CUS_User_Code like '" + inputCode + "'" +
                    " OR " + "CUS_NAME like '" + inputCode + "'" +
                    " OR " + "OTHER_NAME like '" + inputCode + "'" +
                    " OR " + "OTHER_SPELL like '" + inputCode + "'" +
                    " OR " + "EN_NAME like '" + inputCode + "'";
                    break;
                default:
                    sFilter = "SPELL_CODE like '" + inputCode + "%'" +
                    " OR " + "WB_CODE like '" + inputCode + "%'" +
                    " OR " + "User_Code like '" + inputCode.PadLeft(6, '0') + "%'" +
                    " OR " + "ITEM_NAME like '" + inputCode + "%'" +
                    " OR " + "CUS_SPELL_CODE like '" + inputCode + "%'" +
                    " OR " + "CUS_WB_CODE like '" + inputCode + "%'" +
                    " OR " + "CUS_User_Code like '" + inputCode + "%'" +
                    " OR " + "CUS_NAME like '" + inputCode + "%'" +
                    " OR " + "OTHER_NAME like '" + inputCode + "%'" +
                    " OR " + "OTHER_SPELL like '" + inputCode + "%'" +
                    " OR " + "EN_NAME like '" + inputCode + "%'";
                    break;
            }
            //�������ı���Ϊ�գ���յ�ǰ��
            if (inputCode == string.Empty)
            {
                ClearRow(row);
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.SetActiveCell(row, (int)Columns.InputCode, false);
                return;
            }
            else//������Ŀ
            {

                sFilter = FS.FrameWork.Public.String.TakeOffSpecialChar(sFilter, new string[] { "[", "]", "#", "@", "^", "&", "$", "*" });
                this.chooseItemControl.SetInputChar(this.fpSpread1, inputCode, FS.HISFC.Models.Base.InputTypes.Spell);
                dvItem.RowFilter = sFilter;
                ///this.chooseItemControl.i.ucItem.InitPrev();
                if (this.chooseItemControl.InputPrev.Length <= 0)
                {
                    dvItem.Sort = "DRUG_FLAG DESC";
                }
                else
                {
                    dvItem.Sort = "DRUG_FLAG DESC," + this.chooseItemControl.InputPrev;
                }
                //ѡ��ؼ���ѡ��һ����Ŀ�󴥷�
                //���ֻ��һ�У��ؼ�����ʾ��ֱ����д��Ŀ��Ϣ

                //ѡ����Ŀ�ؼ����չ��˺����Ŀ��Ϣ
                this.chooseItemControl.DeptCode = myOperator.Dept.ID;
                this.chooseItemControl.ObjectFilterObject = this.fpSheetItem;

                if (this.chooseItemControl.IsSelectItem == false)
                {
                    this.fpSpread1.Select();
                    this.fpSpread1.Focus();
                    //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    this.fpSpread1_Sheet1.SetActiveCell(row, (int)Columns.InputCode, false);
                    this.SumCost();
                }
                if (this.fpSheetItem.RowCount > 1)
                {
                    ((Form)this.chooseItemControl).ShowDialog();
                }

                if (this.chooseItemControl.IsSelectItem == false)
                {
                    this.fpSpread1.Select();
                    this.fpSpread1.Focus();
                    //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    this.fpSpread1_Sheet1.SetValue(row, (int)Columns.InputCode, inputCode);
                    //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    this.fpSpread1_Sheet1.SetActiveCell(row, (int)Columns.InputCode, false);

                    if (this.fpSpread1.EditingControl != null)
                    {
                        this.fpSpread1.EditingControl.Select();
                    }
                   
                   
                }
            }
        }

        /// <summary>
        /// �����Ŀ�б�
        /// </summary>
        /// <param name="deptCode">�շѿ��Ҵ���</param>
        /// <returns> -1 ʧ�� >=0 �ɹ�</returns>
        private int LoadItem(string deptCode)
        {
            int iReturn = 0;

            //����շ�Ա���ڿ��ҵ�ά��ҩ���е�ҩƷ����ҩƷ�������Ŀ������ȫ�����
           // iReturn = this.outpatientManager.QueryItemList(deptCode, ref dsItem);

            dsItem = new DataSet();

            iReturn = this.outpatientManager.QueryItemList(deptCode, this.itemKind, ref dsItem);
            if (iReturn == -1)
            {
                MessageBox.Show("�����Ŀ�б����!" + this.outpatientManager.Err);

                return -1;
            }

            //���ݲ��������Ƿ����ȱҩҩƷ
            if (this.displayLackPha)
            {
                DataSet dsItemSupply = new DataSet();
                ////iReturn = this.outpatientManager.GetItemListSupply(deptCode, ref dsItemSupply);
                if (iReturn == -1)
                {
                    MessageBox.Show("�����Ŀ�б�(ȱҩ����)����!");
                    return -1;
                }

                dsItem.Merge(dsItemSupply);
            }
            try
            {
                //������Ŀ�б������Ϊ��Ŀ���루ҩƷ����ҩƷ���룬������Ŀ��package_code)
                //dsItem.Tables[0].PrimaryKey = new DataColumn[] { dsItem.Tables[0].Columns["ITEM_CODE"], dsItem.Tables[0].Columns["EXE_DEPT"] };
                //dsItem.Tables[0].Clear();
                //dsItem.Tables[0].PrimaryKey = null;
                dvItem = new DataView(dsItem.Tables[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return -1;
            }

            return iReturn;
        }

        /// <summary>
        /// ����Ƶ����Ϣ
        /// </summary>
        /// <returns></returns>
        private int ProcessFreq()
        {
            if (this.lbFreq.Visible == false)
            {
                return -1;
            }
            int CurrentRow = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0)
            {
                return 0;
            }
            fpSpread1.StopCellEditing();
            string IsDeptChange = fpSpread1_Sheet1.GetText(CurrentRow, (int)Columns.Change);
            if ((IsDeptChange == "0" || IsDeptChange == string.Empty) && fpSpread1_Sheet1.GetText(CurrentRow, (int)Columns.Freq) == string.Empty)
            {
                MessageBox.Show(Language.Msg("Ƶ�β���Ϊ��,������!"), Language.Msg("��ʾ"));
                fpSpread1.Focus();
                fpSpread1_Sheet1.SetActiveCell(CurrentRow, (int)Columns.Freq, true);

                return -1;
            }

            NeuObject item1 = null;
            FS.HISFC.Models.Order.Frequency item = null;
            int rtn = lbFreq.GetSelectedItem(out item1);
            //{565BF156-98AB-41ae-B657-93BC408FF641}
            if (item1 == null || string.IsNullOrEmpty(item1.ID)) 
            {
                return 0;
            }//{565BF156-98AB-41ae-B657-93BC408FF641}���

            item = (FS.HISFC.Models.Order.Frequency)item1;
            if (rtn == -1) 
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }

            if (freqDisplayType == "0")//����
            {
                if (item.UserCode != null && item.UserCode.Length > 0)
                {
                    fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.Freq, item.User03);
                }
                else
                {
                    fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.Freq, item.Name);
                }
            }
            else //����
            {
                if (item.UserCode != null && item.UserCode.Length > 0)
                {
                    fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.Freq, item.Name);
                }
                else
                {
                    fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.Freq, item.ID);
                }
            }

            fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.Change, "0");
            if (item.UserCode != null && item.UserCode.Length > 0)
            {
                ((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).Order.Frequency.ID = item.Name;
                ((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).Order.Frequency.Name = item.User03;
            }
            else
            {
                ((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).Order.Frequency.ID = item.ID;
                ((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).Order.Frequency.Name = item.Name;
            }
            lbFreq.Visible = false;
            this.fpSpread1_Sheet1.Cells[CurrentRow, (int)Columns.Usage].Locked = false;
            this.fpSpread1_Sheet1.SetActiveCell(CurrentRow, (int)Columns.Usage, false);

            return 1;
        }

        /// <summary>
        /// �÷��س��¼�
        /// </summary>
        /// <returns></returns>
        private int ProcessUsage()
        {
            if (this.lbUsage.Visible == false)
            {
                return -1;
            }
            int CurrentRow = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0)
            {
                return 0;
            }
            fpSpread1.StopCellEditing();
            string IsDeptChange = fpSpread1_Sheet1.GetText(CurrentRow, (int)Columns.Change);
            if ((IsDeptChange == "0" || IsDeptChange == string.Empty) && fpSpread1_Sheet1.GetText(CurrentRow, (int)Columns.Usage) == string.Empty)
            {
                MessageBox.Show(Language.Msg("�÷�����Ϊ��,������!"), Language.Msg("��ʾ"));
                fpSpread1.Focus();
                fpSpread1_Sheet1.SetActiveCell(CurrentRow, (int)Columns.Usage, true);
                return -1;
            }

            NeuObject item = null;
            int rtn = lbUsage.GetSelectedItem(out item);
            if (item != null)
            {
                string usageCode = item.ID;

                NeuObject obj = this.managerIntegrate.GetConstansObj("MZUSAGECODE", usageCode);

                if (obj != null && obj.Name != string.Empty)
                {
                    try
                    {
                        this.fpSpread1_Sheet1.RowHeader.Cells[CurrentRow, 0].BackColor = Color.FromArgb(NConvert.ToInt32(obj.Name));
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        this.fpSpread1_Sheet1.RowHeader.Cells[CurrentRow, 0].BackColor = Color.FromArgb(-1250856);
                    }
                    catch { }
                }
            }
            else
            {
                try
                {
                    this.fpSpread1_Sheet1.RowHeader.Cells[CurrentRow, 0].BackColor = Color.FromArgb(-1250856);
                }
                catch { }
            }
            if (rtn == -1)
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }
            
            fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.Usage, item.Name);
            fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.Change, "0");
            ((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).Order.Usage.ID = item.ID;
            ((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).Order.Usage.Name = item.Name;

            //if (((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).Item.IsPharmacy)
            if (((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).Item.ItemType == EnumItemType.Drug)
            {
                //ȥ�����÷����жϷǿյ��ж� 2007-08-24 luzhp@FS.com
                //if (this.fpSpread1_Sheet1.Cells[CurrentRow, (int)Columns.Usage].Text == string.Empty)
                //{
                //    MessageBox.Show(Language.Msg("������ҩƷ���÷�!"));
                //    this.fpSpread1.Focus();
                //    this.fpSpread1_Sheet1.SetActiveCell(CurrentRow, (int)Columns.Usage);

                //    return -1;
                //}
                //else
                //{
                if(this.fpSpread1_Sheet1.Cells[CurrentRow, (int)Columns.Usage].Text != string.Empty)
                {
                    string usageCode = item.ID;

                    alInjec = this.outpatientManager.GetInjectInfoByUsage(usageCode);
                    if (alInjec == null)
                    {
                        MessageBox.Show("���Ժע��Ŀ����!" + this.outpatientManager.Err);

                        return -1;
                    }
                    if (alInjec.Count > 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(myInjec);
                    }
                }
            }

            lbUsage.Visible = false;

            this.fpSpread1_Sheet1.Cells[CurrentRow, (int)Columns.ExeDept].Locked = false;
            this.fpSpread1_Sheet1.SetActiveCell(CurrentRow, (int)Columns.ExeDept, false);

            return 0;
        }

        /// <summary>
        /// ִ�п��ҵĻس�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int ProcessDept()
        {
            if (lbDept.Visible == false)
            {
                return 1;
            }
            int CurrentRow = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0)
            {
                return 1;
            }
            fpSpread1.StopCellEditing();
            string IsDeptChange = fpSpread1_Sheet1.GetText(CurrentRow, (int)Columns.Change);
            if ((IsDeptChange == "0" || IsDeptChange == string.Empty) && fpSpread1_Sheet1.GetText(CurrentRow, (int)Columns.ExeDept) == string.Empty)
            {
                MessageBox.Show(Language.Msg("ִ�п��Ҳ���Ϊ��,������!"));
                fpSpread1.Focus();
                fpSpread1_Sheet1.SetActiveCell(CurrentRow, (int)Columns.ExeDept, true);

                return -1;
            }

            NeuObject item = null;
            int rtn = lbDept.GetSelectedItem(out item);
            if (rtn == -1)
            {
                MessageBox.Show(Language.Msg("����ı��벻��ȷ,����������"));
                
                return -1;
            }
            if (item == null)
            {
                MessageBox.Show(Language.Msg("����ı��벻��ȷ,����������"));

                return -1;
            }

           
            fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.ExeDept, item.Name);
            fpSpread1_Sheet1.SetValue(CurrentRow, (int)Columns.Change, "0");
            ((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).ExecOper.Dept.ID = item.ID;
            ((FeeItemList)this.fpSpread1_Sheet1.Rows[CurrentRow].Tag).ExecOper.Dept.Name = item.Name;
            
            lbDept.Visible = false;
            //fpSpread1.StopCellEditing();
            if (isJudgeStore)
            {
                FeeItemList f = this.fpSpread1_Sheet1.Rows[CurrentRow].Tag as FeeItemList;
                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    if (!IsStoreEnough(f, CurrentRow))
                    {
                        //f.ExecOper.Dept.ID = string.Empty;
                        //f.ExecOper.Dept.Name = string.Empty;
                        this.fpSpread1.Focus();
                        this.fpSpread1_Sheet1.SetActiveCell(CurrentRow, (int)Columns.Amount);
                        return -1;
                    };
                }
            }
            this.AddRow(CurrentRow);

            return 1;
        }

        /// <summary>
        /// ��ʼ��Ƶ����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitFreq()
        {
            ArrayList alTemp = new ArrayList();

            alFreq = this.managerIntegrate.QuereyFrequencyList();

            if (alFreq == null) 
            {
                MessageBox.Show("���Ƶ���б����!" + this.managerIntegrate.Err);

                return -1;
            }
            
            foreach (FS.HISFC.Models.Order.Frequency f in alFreq)
            {
                FS.HISFC.Models.Order.Frequency temFre = f.Clone();
                string temp = string.Empty;
                if (f.UserCode != null && f.UserCode.Length > 0)
                {
                    temp = temFre.UserCode;
                    temFre.User03 = temFre.Name;
                    temFre.Name = temFre.ID;
                    temFre.ID = temp;
                }
                alTemp.Add(temFre);
            }
            
            lbFreq.AddItems(alTemp);
            Controls.Add(lbFreq);
            lbFreq.Hide();
            lbFreq.BorderStyle = BorderStyle.FixedSingle;
            lbFreq.BringToFront();
            lbFreq.Width = 80;

            lbFreq.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbFreq_SelectItem);

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int lbFreq_SelectItem(Keys key)
        {
            ProcessFreq();
            fpSpread1.Focus();
            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.Freq, true);
            return 0;
        }

        /// <summary>
        /// ��ʼ��ִ�п����б�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDept()
        {
            alDept = this.managerIntegrate.GetDepartment();
            if (alDept == null) 
            {
                MessageBox.Show("��ÿ����б����!" + this.managerIntegrate.Err);

                return -1;
            }
            lbDept.AddItems(alDept);
            Controls.Add(lbDept);
            lbDept.Hide();
            lbDept.BorderStyle = BorderStyle.FixedSingle;
            lbDept.BringToFront();

            lbDept.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbDept_SelectItem);
            
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int lbDept_SelectItem(Keys key)
        {
            ProcessDept();
            fpSpread1.Focus();
            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.ExeDept, true);
            return 0;
        }

        /// <summary>
        /// ��ʼ���÷��б�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitUsage()
        {
            alUsage = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            if (alUsage == null) 
            {
                MessageBox.Show("�����÷��б����!" + this.managerIntegrate.Err);

                return -1;
            }
            lbUsage.AddItems(alUsage);
            Controls.Add(lbUsage);
            lbUsage.Hide();
            lbUsage.BorderStyle = BorderStyle.FixedSingle;
            lbUsage.BringToFront();
            lbUsage.Width = 90;

            lbUsage.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbUsage_SelectItem);
            
            return 1;
        }

        int lbUsage_SelectItem(Keys key)
        {   
            ProcessUsage();
            fpSpread1.Focus();
            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.Usage, true);
            return 0;
        }

        /// <summary>
        /// ����ִ�п���λ��
        /// </summary>
        private void SetLocation()
        {
            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)Columns.ExeDept)
            {
                Control cell = this.fpSpread1.EditingControl;
                lbDept.Location = new Point(this.fpSpread1.Location.X + cell.Location.X,
                    this.fpSpread1.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbDept.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
            }
            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)Columns.Freq)
            {
                Control cell = this.fpSpread1.EditingControl;
                lbFreq.Location = new Point(this.fpSpread1.Location.X + cell.Location.X,
                    this.fpSpread1.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbFreq.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
            }
            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)Columns.Usage)
            {
                Control cell = this.fpSpread1.EditingControl;
                lbUsage.Location = new Point(this.fpSpread1.Location.X + cell.Location.X,
                    this.fpSpread1.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbUsage.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
            }
        }

        /// <summary>
        /// ��һ��
        /// </summary>
        /// <param name="key">��ǰ�İ���</param>
        private void PutArrow(Keys key)
        {
            int currCol = this.fpSpread1_Sheet1.ActiveColumnIndex;
            int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (key == Keys.Right)
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
                {
                    if (i > currCol && this.fpSpread1_Sheet1.Cells[currRow, i].Locked == false)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(currRow, i, false);

                        return;
                    }
                }
            }
            if (key == Keys.Left)
            {
                for (int i = this.fpSpread1_Sheet1.Columns.Count - 1; i >= 0; i--)
                {
                    if (i < currCol && this.fpSpread1_Sheet1.Cells[currRow, i].Locked == false)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(currRow, i, false);

                        return;
                    }
                }
            }
        }

        /// <summary>
        /// ��ʼ��farpoint,����һЩ�ȼ�
        /// </summary>
        private void InitFp()
        {
            InputMap im;
            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }




        /// <summary>
        /// ��ʾ���ߵĻ�����Ϣ
        /// </summary>
        private void SetChargeInfo()
        {
            this.Clear();
            int rowCount = this.fpSpread1_Sheet1.RowCount;
            int currRow = 0;
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                this.fpSpread1_Sheet1.Rows.Add(0, 1);
                currRow = 0;
            }
            ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            if (this.fpSpread1_Sheet1.Cells[rowCount - 1, (int)Columns.InputCode].Text == string.Empty)
            {
                currRow = rowCount - 1;
            }
            else
            {
                this.fpSpread1_Sheet1.Rows.Add(currRow, 1);
                currRow = rowCount;
            }

            string userCode = string.Empty;
            decimal totDisplayCost = 0;
            decimal price = 0;
            string minUnit = string.Empty;
            string packUnit = string.Empty;
            string specs = string.Empty;

            foreach (FeeItemList f in alChargeInfo)
            {
                DataRow rowFind;
                string drugFlag = "0";

                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    drugFlag = "1";
                }
                else if (f.Item.ID.Substring(0, 1) == "F")
                {
                    drugFlag = "0";
                }
                else
                {
                    drugFlag = "2";
                }
                string strExp = "ITEM_CODE = " + "'" + f.Item.ID + "'";// +" and DRUG_FLAG =" + "'" + drugFlag + "'";
                DataRow[] rowFinds = dsItem.Tables[0].Select(strExp);
                if (rowFinds == null || rowFinds.Length == 0)
                {
                    MessageBox.Show("������Ŀʧ��!");

                    return;
                }
                rowFind = rowFinds[0];
                if (rowFind == null)
                {
                    MessageBox.Show("������Ŀʧ��!");

                    return;
                }
                
                userCode = rowFind["User_Code"].ToString(); //�Զ������
                try
                {
                    DateTime nowDate = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowDate.Ticks - this.rInfo.Birthday.Ticks)).TotalDays / 365);

                    if (age > 14)
                    {
                        price = NConvert.ToDecimal(rowFind["UNIT_PRICE"].ToString());
                    }
                    else
                    {
                        price = NConvert.ToDecimal(rowFind["CHILD_PRICE"].ToString());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);

                    return;
                }
                decimal pactQty = 0;
                pactQty = NConvert.ToDecimal(rowFind["PACK_QTY"].ToString());
                if (f.Item.PackQty == 0)
                {
                    f.Item.PackQty = pactQty;
                }
                if (f.Item.PackQty == 0)
                {
                    f.Item.PackQty = 1;
                }
                if (f.Item.Price == 0)
                {
                    f.Item.Price = price;
                }
                f.SpecialPrice = f.Item.Price;

                totDisplayCost = f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;
                if (totDisplayCost == 0)
                {
                    totDisplayCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                }

                string itemType = rowFind["DRUG_FLAG"].ToString();
                if (itemType == "0")
                {
                    //f.Item.IsPharmacy = false;
                    f.Item.ItemType = EnumItemType.UnDrug;
                    f.IsGroup = false;
                }
                if (itemType == "1")
                {
                    //f.Item.IsPharmacy = true;
                    f.Item.ItemType = EnumItemType.Drug;
                    f.IsGroup = false;
                }
                if (itemType == "2")
                {
                    //f.Item.IsPharmacy = false;
                    f.Item.ItemType = EnumItemType.UnDrug;
                    f.IsGroup = true;
                }
                ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.InputCode].Text = userCode;
                f.Item.UserCode = userCode;

                if (f.Item.Specs == null || f.Item.Specs == string.Empty)
                {
                    specs = string.Empty;
                }
                else
                {
                    specs = "[" + f.Item.Specs + "]";
                }

                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ItemName].Text = f.Item.Name + specs;

                FarPoint.Win.Spread.CellType.ComboBoxCellType unitCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

                unitCell.Editable = true;
                //Ĭ�ϵ�λΪ��
                minUnit = rowFind["MIN_UNIT"].ToString();
                if (minUnit == string.Empty)
                {
                    minUnit = "��";
                }
                packUnit = rowFind["PACK_UNIT"].ToString();
                if (packUnit == string.Empty)
                {
                    packUnit = "��";
                }
                unitCell.Items = new string[] { minUnit, packUnit };


                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.PriceUnit].CellType = unitCell;
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.PriceUnit].Text = f.Item.PriceUnit;

                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.DoseOnce].Text = f.Order.DoseOnce == 0 ? string.Empty : f.Order.DoseOnce.ToString();
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.DoseUnit].Text = f.Order.DoseUnit;
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.CombNo].Text = f.Order.Combo.ID;
                if (freqDisplayType == "0")//����
                {
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text = myHelpFreq.GetName(f.Order.Frequency.ID);
                }
                else//����
                {
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text = f.Order.Frequency.ID;
                }
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Usage].Text = myHelpUsage.GetName(f.Order.Usage.ID);

                //if (!f.Item.IsPharmacy)
                if (f.Item.ItemType != EnumItemType.Drug)
                {
                    string usageCode = f.Item.SysClass.ID.ToString();

                    NeuObject obj = this.managerIntegrate.GetConstansObj("MZUSAGECODE", usageCode);

                    if (obj != null && obj.Name != string.Empty)
                    {
                        try
                        {
                            this.fpSpread1_Sheet1.RowHeader.Cells[currRow, 0].BackColor = Color.FromArgb(NConvert.ToInt32(obj.Name));
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            this.fpSpread1_Sheet1.RowHeader.Cells[currRow, 0].BackColor = Color.FromArgb(-1250856);
                        }
                        catch { }
                    }
                }
                else
                {
                    if (f.Order.Usage != null)
                    {
                        string usageCode = f.Order.Usage.ID;

                        NeuObject obj = this.managerIntegrate.GetConstansObj("MZUSAGECODE", usageCode);

                        if (obj != null && obj.Name != string.Empty)
                        {
                            try
                            {
                                this.fpSpread1_Sheet1.RowHeader.Cells[currRow, 0].BackColor = Color.FromArgb(NConvert.ToInt32(obj.Name));
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                this.fpSpread1_Sheet1.RowHeader.Cells[currRow, 0].BackColor = Color.FromArgb(-1250856);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        try
                        {   
                            this.fpSpread1_Sheet1.RowHeader.Cells[currRow, 0].BackColor = Color.FromArgb(-1250856);
                        }
                        catch { }
                    }
                }
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ExeDept].Text = f.ExecOper.Dept.Name;

                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Cost].Text = totDisplayCost.ToString();
                
                if (f.FeePack == "1")//��װ��λ
                {
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Price].Text = f.Item.Price.ToString();
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Amount].Text = (f.Item.Qty / f.Item.PackQty).ToString();
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Price].Text =
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Price / f.Item.PackQty, 4).ToString();
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Amount].Text = (f.Item.Qty).ToString();
                }
                if (f.FeePack == "1")//��װ��λ
                {
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Price].Text = f.Item.Price.ToString();
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Price].Text = FS.FrameWork.Public.String.FormatNumber((f.Item.Price / f.Item.PackQty), 4).ToString();
                }

                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Memo].Text = f.Memo;
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ItemCode].Text = f.Item.ID;
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.FeeCode].Text = f.Item.MinFee.ID;
                //this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ItemType].Text = f.Item.IsPharmacy == true ? "1" : "0";
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ItemType].Text = f.Item.ItemType == EnumItemType.Drug ? "1" : "0";
                this.fpSpread1_Sheet1.Rows.Add(currRow + 1, 1);
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.Cells[currRow + 1, (int)Columns.Select].Value = true;
                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Select].Value = true;
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}����
                this.fpSpread1_Sheet1.Rows[currRow].Tag = f;
                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    if (f.Item.SysClass.ID.ToString() == "PCC")
                    {
                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Days].Text = f.Days == 0 ? "1" : f.Days.ToString();
                    }
                }
                if (f.Days == 0)
                {
                    f.Days = 1;
                }
                f.FT.TotCost = totDisplayCost;
                f.FT.OwnCost = totDisplayCost;
                f.FT.PayCost = 0;
                f.FT.PubCost = 0;
                f.Item.IsNeedBespeak = NConvert.ToBoolean(rowFind["NEEDBESPEAK"].ToString());
                if (rowFind["CONFIRM_FLAG"].ToString() == "2" || rowFind["CONFIRM_FLAG"].ToString() == "3" || rowFind["CONFIRM_FLAG"].ToString() == "1")
                {
                    f.Item.IsNeedConfirm = true;
                }
                else
                {
                    f.Item.IsNeedConfirm = false;
                }

                //ҽ����Ϣ
                try
                {
                    this.interfaceManager.GetCompareSingleItem("2", f.Item.ID, ref f.Compare);
                }
                catch { }

                if (this.rInfo.Pact.PayKind.ID == "01")//�Է�
                {
                    f.OrgItemRate = 1;
                    f.NewItemRate = 1;
                    f.FT.ExcessCost = 0;
                    f.FT.DrugOwnCost = 0;
                    f.ItemRateFlag = "1";
                    if (this.isOwnDisplayYB)
                    {
                        if (f.Compare == null)
                        {
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                        }
                        else
                        {
                            if (f.Compare.CenterItem.ItemGrade == "1")
                            {
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                            }
                            else if (f.Compare.CenterItem.ItemGrade == "2")
                            {
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                            }
                            else
                            {
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                            }
                        }
                        
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                    }
                }
                #region ����
                if (this.rInfo.Pact.PayKind.ID == "03")
                {
                    FS.HISFC.Models.Base.PactItemRate pactRate = null;
                    //Ĭ��ȡ����
                    if (pactRate == null)
                    {
                        pactRate = this.pactUnitItemRateManager.GetOnepPactUnitItemRateByItem(this.rInfo.Pact.ID, f.Item.ID);
                    }
                    if (pactRate != null)
                    {
                        #region  ��Ϊ��
                        if (f.ItemRateFlag != "3")
                        {
                            if (pactRate.Rate.PayRate != this.rInfo.Pact.Rate.PayRate)
                            {
                                if (pactRate.Rate.PayRate == 1)//�Է�
                                {
                                    f.ItemRateFlag = "1";
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                                }
                                else
                                {
                                    f.ItemRateFlag = "3";
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Blue;

                                }
                            }
                            else
                            {
                                f.ItemRateFlag = "2";
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            }
                            f.OrgItemRate = this.rInfo.Pact.Rate.PayRate;
                            f.NewItemRate = pactRate.Rate.PayRate;
                        }
                        else
                        {
                            f.ItemRateFlag = "3";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Blue;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Ϊ��
                        if (f.ItemRateFlag != "3")
                        {
                            f.OrgItemRate = this.rInfo.Pact.Rate.PayRate;
                            f.NewItemRate = this.rInfo.Pact.Rate.PayRate;
                            f.ItemRateFlag = "2";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                        }
                        else
                        {
                            f.ItemRateFlag = "3";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Blue;
                            //f.NewItemRate = this.rInfo.Pact.Rate.PayRate;
                        }
                        #endregion
                    }
                
                }
                #endregion
                if (this.rInfo.Pact.PayKind.ID == "02")
                {
                    if (f.Compare == null)
                    {
                        f.ItemRateFlag = "1";
                        f.NewItemRate = 1;
                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                    }
                    else
                    {
                        if (f.Compare.CenterItem.ItemGrade == "1")
                        {
                            f.ItemRateFlag = "1";
                            f.NewItemRate = f.Compare.CenterItem.Rate;
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                        }
                        else if (f.Compare.CenterItem.ItemGrade == "2")
                        {
                            f.ItemRateFlag = "1";
                            f.NewItemRate = f.Compare.CenterItem.Rate;
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                        }
                        else
                        {
                            f.ItemRateFlag = "1";
                            f.NewItemRate = 1;
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                        }
                    }
                }

                currRow++;
            }

            decimal totCost = SumCost();

            FeeItemList feeItem = new FeeItemList();

            if (!this.isCanModifyCharge || this.rInfo.ChkKind == "1" || this.rInfo.ChkKind == "2")//�������޸Ļ�����Ϣ
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag != null && this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        feeItem = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                        if (feeItem.FTSource == "0")//�Լ�����,��������޸�
                        {
                            for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                            {   //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                                if (j == (int)Columns.InputCode || j == (int)Columns.Select)
                                {
                                    this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                                }
                                else
                                {
                                    this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                                }
                            }
                            //if (feeItem.Item.IsPharmacy)
                            if (feeItem.Item.ItemType == EnumItemType.Drug)    
                            {
                                if (feeItem.Item.SysClass.ID.ToString() == "PCC")
                                {
                                    //this.fpSpread1_Sheet1.Cells[i, (int)Columns.Days].Locked = false;
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Days].Locked = true;
                                }
                                //this.fpSpread1_Sheet1.Cells[i, (int)Columns.Amount].Locked = false;
                                //this.fpSpread1_Sheet1.Cells[i, (int)Columns.DoseOnce].Locked = false;
                                //this.fpSpread1_Sheet1.Cells[i, (int)Columns.Freq].Locked = false;
                                //this.fpSpread1_Sheet1.Cells[i, (int)Columns.Usage].Locked = false;
                                //this.fpSpread1_Sheet1.Cells[i, (int)Columns.ExeDept].Locked = false;
                                //this.fpSpread1_Sheet1.Cells[i, (int)Columns.PriceUnit].Locked = false;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Amount].Locked = true;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.DoseOnce].Locked = true;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Freq].Locked = true;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Usage].Locked = true;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.ExeDept].Locked = true;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.PriceUnit].Locked = true;
                            }
                            else
                            {
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Amount].Locked = true;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.ExeDept].Locked = true;

                            }
                            if (feeItem.Item.Price == 0)
                            {
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Price].Locked = true;
                            }
                            if (feeItem.Order.Combo.ID == null || feeItem.Order.Combo.ID == string.Empty)
                            {
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.CombNo].Locked = true;
                            }
                        }
                        else//�����Լ�����,�������޸ĳ���Ժע�������κ���Ϣ
                        {

                            for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                            {
                                ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                                if (j == (int)Columns.InputCode)
                                {
                                    FarPoint.Win.Spread.CellType.TextCellType textCellType = new FarPoint.Win.Spread.CellType.TextCellType();
                                    textCellType.ReadOnly = true;
                                    this.fpSpread1_Sheet1.Cells[i, j].CellType = textCellType;
                                }
                                else if (j == (int)Columns.Usage)
                                {
                                    FarPoint.Win.Spread.CellType.TextCellType textCellType = new FarPoint.Win.Spread.CellType.TextCellType();
                                    textCellType.ReadOnly = true;
                                    this.fpSpread1_Sheet1.Cells[i, j].CellType = textCellType;
                                    this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                                }
                                else if (j == (int)Columns.ExeDept)
                                {
                                    FarPoint.Win.Spread.CellType.TextCellType textCellType = new FarPoint.Win.Spread.CellType.TextCellType();
                                    textCellType.ReadOnly = true;
                                    this.fpSpread1_Sheet1.Cells[i, j].CellType = textCellType;
                                    this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                                }
                                else
                                {
                                    this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                        {   //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                            if (j == (int)Columns.InputCode|| j== (int)Columns.Select)
                            {
                                this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                            }
                            else
                            {
                                this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                            }
                        }
                    }
                }
            }
            else //�����޸Ļ�����Ϣ
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                    {   //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                        if (j == (int)Columns.InputCode || j == (int)Columns.Select)
                        {
                            this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                        }
                    }

                    if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                    {
                        if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                        {
                            feeItem = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                            //����Ѿ������˻�֧����ϸ,�����Ը����κ���Ϣ.
                            if (feeItem.IsAccounted) 
                            {
                                continue;
                            }

                            //if (feeItem.Item.IsPharmacy)
                            if (feeItem.Item.ItemType == EnumItemType.Drug)
                            {
                                if (feeItem.Item.SysClass.ID.ToString() == "PCC")
                                {
                                    //this.fpSpread1_Sheet1.Cells[i, (int)Columns.Days].Locked = false;
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Days].Locked = true;
                                }
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Amount].Locked = false;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.DoseOnce].Locked = false;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Freq].Locked = false;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Usage].Locked = false;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.ExeDept].Locked = false;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.PriceUnit].Locked = false;
                            }
                            else
                            {
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Amount].Locked = false;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.ExeDept].Locked = false;

                            }
                            if (feeItem.Item.Price == 0)
                            {
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Price].Locked = false;
                            }
                            if (feeItem.Order.Combo.ID == null || feeItem.Order.Combo.ID == string.Empty)
                            {
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.CombNo].Locked = false;
                            }
                        }
                        else
                        {   ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.InputCode].Locked = false;
                            //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Select].Locked = false;
                        }
                    }
                    else
                    {   ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.InputCode].Locked = false;
                        //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.Select].Locked = false;
                    }
                }
            }
            rowCount = this.fpSpread1_Sheet1.Rows.Count;

            this.DrawCombo(this.fpSpread1_Sheet1, (int)Columns.CombNo, (int)Columns.CombNoDisplay, 0);

            for (int i = 0; i < this.fpSpread1_Sheet1.ColumnCount; i++)
            {
                this.fpSpread1_Sheet1.Columns[i].Locked = true;
            }
            ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            this.fpSpread1_Sheet1.SetActiveCell(rowCount - 1, (int)Columns.InputCode, false);
        }

        /// <summary>
        /// �������Ϣ
        /// </summary>
        /// <param name="sender">�����farpointSheetView</param>
        /// <param name="column">��˳��</param>
        /// <param name="DrawColumn">����˳��</param>
        /// <param name="ChildViewLevel"></param>
        private void DrawCombo(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = string.Empty, curComboNo = string.Empty;
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "��"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = string.Empty;
                            tmp = o.Cells[i, column].Text + string.Empty;
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != string.Empty) //��ͷ
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "��";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = string.Empty;
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != string.Empty)
                            {
                                o.Cells[i, DrawColumn].Text = "��";
                            }
                            else if (curComboNo != tmp && tmp == string.Empty)
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = string.Empty;
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = string.Empty;
                                curComboNo = string.Empty;
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "��";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = string.Empty;
                            o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "��"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = string.Empty;
                                tmp = c.Cells[j, column].Text + string.Empty;

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != string.Empty) //��ͷ
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "��";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = string.Empty;
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != string.Empty)
                                {
                                    c.Cells[j, DrawColumn].Text = "��";
                                }
                                else if (curComboNo != tmp && tmp == string.Empty)
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = string.Empty;
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = string.Empty;
                                    curComboNo = string.Empty;
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "��";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = string.Empty;
                                c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }

        /// <summary>
        /// ��֤�����Ƿ�����Ϸ�
        /// </summary>
        /// <param name="row">��ǰ��</param>
        /// <param name="col">��ǰ��</param>
        /// <param name="colName">������</param>
        /// <param name="maxValue">���ֵ</param>
        /// <param name="minValue">��Сֵ</param>
        /// <param name="currValue">���صĵ�ǰ����ֵ</param>
        /// <returns>true�Ϸ� false���Ϸ�</returns>
        private bool InputDataIsValid(int row, int col, string colName, decimal maxValue, decimal minValue, ref decimal currValue)
        {
            try
            {
                currValue = NConvert.ToDecimal(
                    FS.FrameWork.Public.String.ExpressionVal(
                    this.fpSpread1_Sheet1.Cells[row, col].Text.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(colName + Language.Msg("������ļ��㹫ʽ����ȷ������������!") + ex.Message);
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.SetActiveCell(row, col);

                return false;
            }

            if (currValue <= minValue)
            {
                MessageBox.Show(colName + Language.Msg("��ֵ����С��") + minValue.ToString() + Language.Msg("�����������ֵ���󳬳�����Χ!"));
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.SetActiveCell(row, col);
                
                return false;
            }
            if (currValue > maxValue)
            {
                MessageBox.Show(colName + Language.Msg("��ֵ���ܴ���") + maxValue.ToString() + "!");
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.SetActiveCell(row, col);
                
                return false;
            }

            return true;
        }

        /// <summary>
        /// ��������Ϻ�
        /// </summary>
        /// <returns></returns>
        private string GetMaxCombNo()
        {
            double combNO = 0;
            double tempCombNO = 0;
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null && this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList feeItem = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                    try
                    {
                        tempCombNO = System.Convert.ToInt64(feeItem.Order.Combo.ID);
                    }
                    catch
                    {

                    }

                    if (tempCombNO > combNO)
                    {
                        combNO = tempCombNO;
                    }
                }
            }

            return (combNO + 1).ToString();
        }
        
        /// <summary>
        /// �������
        /// </summary>
        /// <returns> -1 ʧ�� �����ɹ�</returns>
        private int GetNewRow()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Cells[i, (int)Columns.ItemName].Text == string.Empty)
                {
                    return i;
                }
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null && this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    continue;
                }
                if (this.fpSpread1_Sheet1.Cells[i, (int)Columns.ItemName].Text == "С��")
                {
                    continue;
                }

                return i;
            }

            return -1;
        }

        /// <summary>
        /// �ж�����Ŀ�Ƿ���������ҩ֮��
        /// </summary>
        /// <param name="row"></param>
        /// <param name="days"></param>
        /// <param name="combNO"></param>
        /// <returns></returns>
        private bool JudgeInPCC(int row, ref decimal days, ref string combNO)
        {
            int tempRow = row - 1;

            if (tempRow < 0)
            {
                return false;
            }

            if (this.fpSpread1_Sheet1.Rows[tempRow].Tag == null)
            {
                return false;
            }

            if ((FeeItemList)this.fpSpread1_Sheet1.Rows[tempRow].Tag == null)
            {
                return false;
            }

            if (((FeeItemList)this.fpSpread1_Sheet1.Rows[tempRow].Tag).Item.SysClass.ID.ToString() != "PCC")
            {
                return false;
            }

            tempRow = row + 1;

            if (tempRow > this.fpSpread1_Sheet1.Rows.Count - 1)
            {
                return false;
            }

            if (this.fpSpread1_Sheet1.Rows[tempRow].Tag == null)
            {
                return false;
            }

            if ((FeeItemList)this.fpSpread1_Sheet1.Rows[tempRow].Tag == null)
            {
                return false;
            }

            if (((FeeItemList)this.fpSpread1_Sheet1.Rows[tempRow].Tag).Item.SysClass.ID.ToString() != "PCC")
            {
                return false;
            }

            days = ((FeeItemList)this.fpSpread1_Sheet1.Rows[row - 1].Tag).Days;
            combNO = ((FeeItemList)this.fpSpread1_Sheet1.Rows[row - 1].Tag).Order.Combo.ID;

            return true;
        }

        /// <summary>
        /// ѡ����Ŀ
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="drugFlag"></param>
        /// <param name="exeDeptCode"></param>
        /// <param name="row"></param>
        /// <param name="amount"></param>
        /// <param name="saleprice">{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}���ۼ�--�����շ�����</param>
        private void SetItem(string itemCode, string drugFlag, string exeDeptCode, int row, decimal amount, decimal saleprice)
        {
            if (this.rInfo == null)
            {
                MessageBox.Show(Language.Msg("��ѡ����"));

                this.isDealCellChange = true;

                return ;
            }

            if (this.rInfo.DoctorInfo.Templet.Dept.ID == null || this.rInfo.DoctorInfo.Templet.Dept.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("��ѡ�������!"));

                this.isDealCellChange = true;

                return ;
            }
            this.isDealCellChange = false;

            DataRow findRow;
            DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "' and drug_flag = '" + drugFlag + "'");

            if (rowFinds == null || rowFinds.Length == 0)
            {
                MessageBox.Show("����Ϊ: [" + itemCode + " ] ����Ŀ����ʧ��!");
                this.isDealCellChange = true;
                
                return;
            }

            findRow = rowFinds[0];
            #region {5D62CB1F-6134-48f4-B905-02AD69D6A433}���ǵĳ���Ӧ������ȡ���¼۸�
            //����շ�Ա���ڿ��ҵ�ά��ҩ���е�ҩƷ����ҩƷ�������Ŀ������ȫ�����

            DataSet dsItemNow = new DataSet();
            iReturn = this.outpatientManager.QueryItemList(myOperator.Dept.ID, itemCode, ref dsItemNow);
            if (iReturn == -1)
            {
                MessageBox.Show("�����Ŀ����!" + this.outpatientManager.Err);
                return;
            }
            DataRow findRowNow;
            DataRow[] rowFindsNow = dsItemNow.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "' and drug_flag = '" + drugFlag + "'");

            if (rowFindsNow == null || rowFindsNow.Length == 0)
            {
                MessageBox.Show("����Ϊ: [" + itemCode + " ] ����Ŀ����ʧ��!");
                this.isDealCellChange = true;

                return;
            }

            findRowNow = rowFindsNow[0];

            bool isPriceChange = false;

            if (NConvert.ToDecimal(findRowNow["UNIT_PRICE"].ToString()) != NConvert.ToDecimal(findRow["UNIT_PRICE"].ToString()))
            {
                findRow["UNIT_PRICE"] = findRowNow["UNIT_PRICE"];
                isPriceChange = true;
            }
            if (NConvert.ToDecimal(findRowNow["SP_PRICE"].ToString()) != NConvert.ToDecimal(findRow["SP_PRICE"].ToString()))
            {
                findRow["SP_PRICE"] = findRowNow["SP_PRICE"];
                isPriceChange = true;
            }
            if (NConvert.ToDecimal(findRowNow["CHILD_PRICE"].ToString()) != NConvert.ToDecimal(findRow["CHILD_PRICE"].ToString()))
            {
                findRow["CHILD_PRICE"] = findRowNow["CHILD_PRICE"];
                isPriceChange = true;
            }
            if (isPriceChange)
            {
                FillFilterControl();
            }
            #endregion

            //�����������Ŀ�����о�ȷ����,��Ϊ���ܴ��ڶ�����Ŀ
            //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
            if (findRow["DRUG_FLAG"].ToString() == "6")
            {
                DataRow[] mateRow = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "'" + " and unit_price = " + saleprice + "" + " and EXE_DEPT = '" + exeDeptCode + "'");
                if (mateRow == null || mateRow.Length ==  0)
                {
                    MessageBox.Show("����Ϊ: [" + itemCode + " ] ����Ŀ����ʧ��!");
                    this.isDealCellChange = true;

                    return;
                }
                findRow = mateRow[0];
            }

            //�����ҩƷ,���о�ȷ����,��Ϊ���ܴ��ڶ�����Ŀ
            if (findRow["DRUG_FLAG"].ToString() == "1")
            {
                DataRow[] rowFindAgain = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "'" + " and EXE_DEPT = '" + exeDeptCode + "'");

                if (rowFindAgain == null || rowFindAgain.Length == 0)
                {
                    MessageBox.Show("����Ϊ: [" + itemCode + " ] ����Ŀ����ʧ��!");
                    this.isDealCellChange = true;
                    
                    return;
                }
                findRow = rowFindAgain[0];
            }

            //��Ŀ������Ϣʵ��
            FeeItemList feeItemList = new FeeItemList();
            
            //����ҵ���Ŀ
            if (findRow != null)
            {
                decimal price = 0;		//����
                decimal pactQty = 0;	//��װ����
                string specs = string.Empty;		//���
                string exeDept = string.Empty;	//ִ�п���
                string itemType = string.Empty;	//��Ŀ���
                string minUnit = string.Empty;	//��С��λ
                string packUnit = string.Empty;   //��װ��λ
                string freqCode = string.Empty;	//Ƶ�δ���
                string usageCode = string.Empty;	//�÷�����
                decimal baseDose = 0m;//��������

                //������ӵ���
                this.alAddRows.Add(row);

                #region ��Ŀ���

                itemType = findRow["DRUG_FLAG"].ToString();

                //��ҩƷ
                if (itemType == "0")
                {
                    feeItemList.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                    //feeItemList.Item.IsPharmacy = false;
                    feeItemList.Item.ItemType =  EnumItemType.UnDrug;
                    feeItemList.IsGroup = false;
                }
                //ҩƷ
                if (itemType == "1")
                {
                    feeItemList.Item = new FS.HISFC.Models.Pharmacy.Item();
                    //feeItemList.Item.IsPharmacy = true;
                    feeItemList.Item.ItemType = EnumItemType.Drug;

                    feeItemList.IsGroup = false;
                }
                //�����Ŀ
                if (itemType == "2")
                {
                    //feeItemList.Item.IsPharmacy = false;
                    feeItemList.Item.ItemType = EnumItemType.UnDrug;
                    feeItemList.IsGroup = true;
                }

                //����
                if (itemType == "3")//����
                {
                    ArrayList groupDetails = this.managerIntegrate.QueryGroupDetailByGroupCode(itemCode);
                    if (groupDetails == null)
                    {
                        MessageBox.Show("���������ϸ����!" + this.managerIntegrate.Err);
                        this.isDealCellChange = true;

                        return;
                    }
                    int actIndex = row;
                    ucInputTimes uc = new ucInputTimes();
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                    int times = uc.Times;

                    foreach (FS.HISFC.Models.Fee.ComGroupTail detail in groupDetails)
                    {
                        string drugflag = "1";

                        if (detail.drugFlag == "2")
                        {
                            drugflag = "0";
                        }
                        else if (detail.drugFlag == "3")
                        {
                            drugflag = "2";
                        }
                        //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                        this.SetItem(detail.itemCode, drugflag, detail.deptCode, actIndex, detail.qty * times, 0);
                        actIndex = GetNewRow();
                        if (actIndex == -1)
                        {
                            this.fpSpread1.StopCellEditing();
                            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                            actIndex = this.fpSpread1_Sheet1.RowCount - 1;
                        }
                    }

                    return;
                }

                #region �����շ�(�����յ�����)
                //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                if (itemType == "6")
                {
                    feeItemList.Item = new FS.HISFC.Models.FeeStuff.MaterialItem();
                    feeItemList.Item.ItemType = EnumItemType.MatItem;
                }
                #endregion
     
                #endregion

                #region ����

                feeItemList.Item.ID = itemCode;
                feeItemList.ID = itemCode;

                #endregion

                #region �Զ������

                //��������ƴ���뻹����ʵȣ��������ʾ�Զ�����
                ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.Cells[row, (int)Columns.InputCode].Text = findRow["User_Code"].ToString();
                feeItemList.Item.UserCode = findRow["User_Code"].ToString();

                #endregion

                #region ���
                
                //��ʾ��Ŀ���ƣ������ҩƷ�͹��һ����ʾ
                specs = findRow["SPECS"].ToString();
                feeItemList.Item.Specs = specs;
                if (specs == null || specs == string.Empty)
                {
                    specs = string.Empty;
                }
                else
                {
                    specs = "[" + specs + "]";
                }
                this.fpSpread1_Sheet1.Cells[row, (int)Columns.ItemName].Text = findRow["ITEM_NAME"].ToString() + specs;

                #endregion

                #region ����
                
                feeItemList.Item.Name = findRow["ITEM_NAME"].ToString();
                feeItemList.Name = feeItemList.Item.Name;
                
                #endregion

                #region ����
                
                //����
                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Days].Text = string.Empty;
                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Days].Locked = true;
                feeItemList.Days = 1;

                #endregion

                #region ϵͳ�����������
                
                feeItemList.Item.SysClass.ID = findRow["SYS_CLASS"].ToString();
                feeItemList.Order.Sample.Name = findRow["DEFAULT_SAMPLE"].ToString();//����
                
                #endregion

                #region ҩƷ����

                //�������ҩƷ
                //if (feeItemList.Item.IsPharmacy)
                if (feeItemList.Item.ItemType == EnumItemType.Drug)    
                {
                    //����ǲ�ҩ
                    if (feeItemList.Item.SysClass.ID.ToString() == "PCC")
                    {
                        decimal tempDays = 0m;
                        string tempCombNO = string.Empty;
                        if (this.JudgeInPCC(row, ref tempDays, ref tempCombNO))
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Days].Value = tempDays;//Ĭ������Ϊ1
                            feeItemList.Days = tempDays;
                            feeItemList.Order.Combo.ID = tempCombNO;
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.CombNo].Value = tempCombNO;
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Days].Locked = false;
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Days].Value = hDays;//Ĭ������Ϊ1
                            feeItemList.Days = hDays;
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Days].Locked = false;
                        }
                    }
                    
                    //������λ
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.DoseUnit].Text = findRow["DOSE_UNIT"].ToString();

                    //��Ҫת����λ
                    if (this.invertUnitHelper.GetObjectFromName(findRow["MIN_UNIT"].ToString()) != null || this.specialInvertUnitHelper.GetObjectFromID(findRow["ITEM_CODE"].ToString()) != null)
                    {
                        feeItemList.Order.DoseUnit = findRow["MIN_UNIT"].ToString();
                    }
                    else
                    {
                        feeItemList.Order.DoseUnit = findRow["DOSE_UNIT"].ToString();
                    }

                    //������λ
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.DoseUnit].Text = feeItemList.Order.DoseUnit;

                    //Ƶ��(ҩƷ)
                    freqCode = findRow["FREQ_CODE"].ToString();
                    if (freqCode == string.Empty)
                    {
                        freqCode = "QD";
                    }
                    string freqName = myHelpFreq.GetName(freqCode);
                    if (freqDisplayType == "0")//����
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Freq].Text = freqName;
                    }
                    else//����
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Freq].Text = freqCode;
                    }
                    feeItemList.Order.Frequency.ID = freqCode;
                    feeItemList.Order.Frequency.Name = freqName;
                    if (this.invertUnitHelper.GetObjectFromName(findRow["MIN_UNIT"].ToString()) != null || this.specialInvertUnitHelper.GetObjectFromID(findRow["ITEM_CODE"].ToString()) != null)
                    {
                        //��������
                        baseDose = NConvert.ToDecimal(findRow["ONCE_DOSE"].ToString());
                        if (baseDose <= 0)
                        {
                            baseDose = NConvert.ToDecimal(findRow["BASE_DOSE"].ToString());
                        }

                        if (NConvert.ToDecimal(findRow["ONCE_DOSE"].ToString()) > 0)
                        {
                            baseDose = baseDose / NConvert.ToDecimal(findRow["BASE_DOSE"].ToString());
                        }

                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.DoseOnce].Text = baseDose.ToString();
                        feeItemList.Order.DoseOnce = baseDose;
                    }
                    else
                    {
                        //��������
                        baseDose = NConvert.ToDecimal(findRow["ONCE_DOSE"].ToString());
                        if (baseDose <= 0)
                        {
                            baseDose = NConvert.ToDecimal(findRow["BASE_DOSE"].ToString());
                        }
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.DoseOnce].Text = baseDose.ToString();
                        feeItemList.Order.DoseOnce = baseDose;
                    }
                    //  {1FAD3FA2-C7D8-4cac-845F-B9EBECDE2312}
                    (feeItemList.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose = NConvert.ToDecimal(findRow["BASE_DOSE"].ToString());

                    //�÷�(ҩƷ)
                    usageCode = findRow["USAGE_CODE"].ToString();
                    string useName = myHelpUsage.GetName(usageCode);
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Usage].Text = useName;
                    feeItemList.Order.Usage.ID = usageCode;
                    feeItemList.Order.Usage.Name = useName;
                    feeItemList.Invoice.User01 = findRow["SPLIT_TYPE"].ToString();

                }
                //if (!feeItemList.Item.IsPharmacy)
                if(feeItemList.Item.ItemType != EnumItemType.Drug)
                {
                    string idCode = feeItemList.Item.SysClass.ID.ToString();

                    FS.FrameWork.Models.NeuObject obj = this.managerIntegrate.GetConstansObj("MZUSAGECODE", idCode);

                    if (obj != null && obj.Name != string.Empty)
                    {
                        try
                        {
                            this.fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.FromArgb(NConvert.ToInt32(obj.Name));
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            this.fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.FromArgb(-1250856);
                        }
                        catch { }
                    }
                }
                else
                {
                    if (feeItemList.Order.Usage != null)
                    {
                        string idCode = feeItemList.Order.Usage.ID;

                        FS.FrameWork.Models.NeuObject obj = this.managerIntegrate.GetConstansObj("MZUSAGECODE", idCode);

                        if (obj != null && obj.Name != string.Empty)
                        {
                            try
                            {
                                this.fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.FromArgb(NConvert.ToInt32(obj.Name));
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                this.fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.FromArgb(-1250856);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        try
                        {
                            this.fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.FromArgb(-1250856);
                        }
                        catch { }
                    }
                }
                #endregion

                #region ִ�п���
                exeDept = findRow["EXE_DEPT"].ToString();

                //�Ƿ���ҪԤԼ
                feeItemList.Item.IsNeedBespeak = NConvert.ToBoolean(findRow["NEEDBESPEAK"].ToString());
                if (findRow["CONFIRM_FLAG"].ToString() == "2" || findRow["CONFIRM_FLAG"].ToString() == "3" || findRow["CONFIRM_FLAG"].ToString() == "1")
                {
                    feeItemList.Item.IsNeedConfirm = true;
                }
                else
                {
                    feeItemList.Item.IsNeedConfirm = false;
                }
                
                feeItemList.RecipeOper.ID = this.rInfo.DoctorInfo.Templet.Doct.ID;
                feeItemList.RecipeOper.Name = this.rInfo.DoctorInfo.Templet.Doct.Name;
                //{33607355-C383-4271-B46C-0FBBAC251382} ����ҽ���������ұ���
                feeItemList.RecipeOper.Dept.ID = this.rInfo.DoctorInfo.Templet.Dept.ID;

                feeItemList.FTSource = "0";//�շ�Ա�Լ��շ�

                if (exeDept != string.Empty)
                {
                    string[] s = exeDept.Split('|');
                    FS.HISFC.Models.Base.Department dept = null;
                    if (s.Length == 0)
                    {
                        lbDept.Items.Clear();
                        lbDept.AddItems(alDept);
                    }
                    else if (s.Length == 1 && s[0] != "��")
                    {
                        dept = this.managerIntegrate.GetDepartment(s[0]);
                        if (dept == null)
                        {
                            MessageBox.Show("���ִ�п��ҳ���!" + this.managerIntegrate.Err);
                            this.isDealCellChange = true;

                            return;
                        }
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.ExeDept].Text = dept.Name;
                        feeItemList.ExecOper.Dept.ID = dept.ID;
                        feeItemList.ExecOper.Dept.Name = dept.Name;
                        //lbDept.alItems = null;
                        //lbDept.AddItems(alDept);
                    }
                    else if (s.Length > 1)
                    {
                        dept = this.managerIntegrate.GetDepartment(s[0]);
                        if (dept == null)
                        {
                            MessageBox.Show("���ִ�п��ҳ���!" + this.managerIntegrate.Err);
                            this.isDealCellChange = true;

                            return;
                        }
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.ExeDept].Text = dept.Name;
                        feeItemList.ExecOper.Dept.ID = dept.ID;
                        feeItemList.ExecOper.Dept.Name = dept.Name;
                        ArrayList deptListTemp = new ArrayList();

                        foreach (string sDeptCode in s)
                        {
                            dept = this.managerIntegrate.GetDepartment(sDeptCode);
                            if (dept == null)
                            {
                                MessageBox.Show("���ִ�п��ҳ���!" + this.managerIntegrate.Err);
                                this.isDealCellChange = true;

                                return;
                            }
                            deptListTemp.Add((FS.FrameWork.Models.NeuObject)dept);
                        }
                        
                        //lbDept.AddItems(deptListTemp);
                    }
                }
                if (exeDept == "��")
                {
                    feeItemList.ExecOper.Dept.ID = this.rInfo.DoctorInfo.Templet.Dept.ID;
                    feeItemList.ExecOper.Dept.Name = this.rInfo.DoctorInfo.Templet.Dept.Name;
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.ExeDept].Text = this.rInfo.DoctorInfo.Templet.Dept.Name;
                }

                #endregion

                #region ����
                try
                {
                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowTime.Ticks - this.rInfo.Birthday.Ticks)).TotalDays / 365);
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}���ӻ�ȡ�����
                    string priceForm = this.rInfo.Pact.PriceForm;
                    decimal unitPrice = NConvert.ToDecimal(findRow["UNIT_PRICE"]);
                    decimal childPrice = NConvert.ToDecimal(findRow["CHILD_PRICE"]);
                    decimal SPPrice = NConvert.ToDecimal(findRow["SP_PRICE"]);                    
                    decimal purchasePrice = NConvert.ToDecimal(findRow["PUCHASE_PRICE"]);

                    // ����ԭʼĬ�ϼ۸�
                    feeItemList.Item.ChildPrice = unitPrice;
                    price = this.feeIntegrate.GetPrice(priceForm, age, unitPrice, childPrice, SPPrice, purchasePrice);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    this.isDealCellChange = true;

                    return;
                }

                ////�ж��Żݼ۸�
                //FS.HISFC.Models.Base.PactItemRate pRate = Class.Function.PactRate(this.rInfo, feeItemList, ref errText);

                //if (pRate == null)
                //{
                //    MessageBox.Show(Language.Msg(errText));
                //    this.fpSpread1_Sheet1.SetActiveCell(row, 0);
                //    this.isDealCellChange = true;

                //    return;
                //}
                //price *= 1-pRate.Rate.RebateRate;
                //------


                //��װ��������ҩƷ�������ĿΪ1
                pactQty = NConvert.ToDecimal(findRow["PACK_QTY"].ToString());
                feeItemList.Item.PackQty = pactQty;
                if (pactQty == 0)
                {
                    MessageBox.Show(Language.Msg("����Ŀû��ά����װ����!"));
                    this.fpSpread1_Sheet1.SetActiveCell(row, 0);
                    this.isDealCellChange = true;

                    return;
                }

                #region �շѵ�λ
                
                FarPoint.Win.Spread.CellType.ComboBoxCellType unitCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                unitCell.Editable = true;
                //Ĭ�ϵ�λΪ��
                minUnit = findRow["MIN_UNIT"].ToString();
                if (minUnit == string.Empty)
                {
                    minUnit = "��";
                }
                packUnit = findRow["PACK_UNIT"].ToString();
                if (packUnit == string.Empty)
                {
                    packUnit = "��";
                }
                unitCell.Items = new string[] { minUnit, packUnit };
                unitCell.Editable = true;

                this.fpSpread1_Sheet1.Cells[row, (int)Columns.PriceUnit].CellType = unitCell;

                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Amount].Text = amount.ToString();

                if (this.defaultPriceUnit == "0")//��С��λ
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.PriceUnit].Text = minUnit;
                    feeItemList.Item.PriceUnit = minUnit;
                    feeItemList.FeePack = "0";
                    feeItemList.Item.Qty = amount;
                    //������cell��ֵ,Ĭ����С��λ
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Price].Value = FS.FrameWork.Public.String.FormatNumber((price / pactQty), 4);
                }
                else //��װ��λ
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.PriceUnit].Text = packUnit;
                    feeItemList.Item.PriceUnit = packUnit;
                    feeItemList.FeePack = "1";
                    feeItemList.Item.Qty = amount * feeItemList.Item.PackQty;
                    //������cell��ֵ,Ĭ����С��λ
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Price].Value = price;
                    if (feeItemList.Item.SysClass.ID.ToString() == "PCC")//��ҩһֱ����С��λ
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.PriceUnit].Text = minUnit;
                        feeItemList.Item.PriceUnit = minUnit;
                        feeItemList.FeePack = "0";
                        feeItemList.Item.Qty = 1;
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Price].Value = FS.FrameWork.Public.String.FormatNumber((price / pactQty), 4);
                    }
                }
                //if (feeItemList.Item.IsPharmacy)
                if (feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    if (feeItemList.Invoice.User01 == "1")// ���ܲ�ְ�װ��λ
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.PriceUnit].Text = packUnit;
                        feeItemList.Item.PriceUnit = packUnit;
                        feeItemList.FeePack = "1";
                        feeItemList.Item.Qty = amount * feeItemList.Item.PackQty;
                        //������cell��ֵ,Ĭ����С��λ
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Price].Value = price;
                    }
                }

                #endregion
                //����ԭʼ����(��װ��λ)
                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Price].Tag = price;
                //��װ��λ���ۣ�����4λС��
                price = FS.FrameWork.Public.String.FormatNumber(price, 4);
                feeItemList.Item.Price = price;
                feeItemList.SpecialPrice = price;
                #endregion

                this.fpSpread1_Sheet1.Cells[row, (int)Columns.FeeCode].Value = findRow["FEE_CODE"].ToString();
                feeItemList.Item.MinFee.ID = findRow["FEE_CODE"].ToString();
                this.fpSpread1_Sheet1.Cells[row, (int)Columns.ItemType].Value = findRow["DRUG_FLAG"].ToString();
                this.fpSpread1_Sheet1.Cells[row, (int)Columns.ItemCode].Value = findRow["ITEM_CODE"].ToString();

                feeItemList.RecipeSequence = this.recipeSeq;

                feeItemList.Patient = this.rInfo.Clone();


                //ҽ����Ϣ
                try
                {
                    this.interfaceManager.GetCompareSingleItem("2", feeItemList.Item.ID, ref feeItemList.Compare);
                }
                catch { }

                #region �Է�

                if (this.rInfo.Pact.PayKind.ID == "01")//�Է�
                {
                    feeItemList.OrgItemRate = 1;
                    feeItemList.NewItemRate = 1;
                    feeItemList.ItemRateFlag = "1";
                    if (this.isOwnDisplayYB)
                    {
                        if (feeItemList.Compare == null)
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "�Է�";
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Red;
                        }
                        else
                        {
                            if (feeItemList.Compare.CenterItem.ItemGrade == "1")
                            {
                                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Black;
                            }
                            else if (feeItemList.Compare.CenterItem.ItemGrade == "2")
                            {
                                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Black;
                            }
                            else
                            {
                                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "�Է�";
                                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Red;
                            }
                        }
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "�Է�";
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Red;
                    }
                }

                #endregion

                #region ����
                if (this.rInfo.Pact.PayKind.ID == "03")
                {
                    FS.HISFC.Models.Base.PactItemRate pactRate = null;
                    //Ĭ��ȡ����
                    if (pactRate == null)
                    {
                        pactRate = this.pactUnitItemRateManager.GetOnepPactUnitItemRateByItem(this.rInfo.Pact.ID, feeItemList.Item.ID);
                    }
                    if (pactRate != null)
                    {
                        #region  ��Ϊ��
                        if (feeItemList.ItemRateFlag != "3")
                        {
                            if (pactRate.Rate.PayRate != this.rInfo.Pact.Rate.PayRate)
                            {
                                if (pactRate.Rate.PayRate == 1)//�Է�
                                {
                                    feeItemList.ItemRateFlag = "1";
                                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "�Է�";
                                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Red;
                                }
                                else
                                {
                                    feeItemList.ItemRateFlag = "3";
                                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Blue;

                                }
                            }
                            else
                            {
                                feeItemList.ItemRateFlag = "2";
                                this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                            }
                            feeItemList.OrgItemRate = this.rInfo.Pact.Rate.PayRate;
                            feeItemList.NewItemRate = pactRate.Rate.PayRate;
                        }
                        else
                        {
                            feeItemList.ItemRateFlag = "3";
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Blue;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Ϊ��
                        if (feeItemList.ItemRateFlag != "3")
                        {
                            feeItemList.OrgItemRate = this.rInfo.Pact.Rate.PayRate;
                            feeItemList.NewItemRate = this.rInfo.Pact.Rate.PayRate;
                            feeItemList.ItemRateFlag = "2";
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                        }
                        else
                        {
                            feeItemList.ItemRateFlag = "3";
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                            this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Blue;
                        }
                        #endregion
                    }

                }

                #endregion

                #region ҽ��
                if (this.rInfo.Pact.PayKind.ID == "02")
                {
                    if (feeItemList.Compare == null)
                    {
                        feeItemList.ItemRateFlag = "1";
                        feeItemList.NewItemRate = 1;
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "�Է�";
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Red;
                    }
                    else
                    {
                        //if (feeItemList.Compare.CenterItem.ItemGrade == "1")
                        //{
                        //    feeItemList.ItemRateFlag = "1";
                        //    feeItemList.NewItemRate = feeItemList.Compare.CenterItem.Rate;
                        //    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                        //    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Black;
                        //}
                        //else if (feeItemList.Compare.CenterItem.ItemGrade == "2")
                        //{
                        //    feeItemList.ItemRateFlag = "1";
                        //    feeItemList.NewItemRate = feeItemList.Compare.CenterItem.Rate;
                        //    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "����";
                        //    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Black;
                        //}
                        //else
                        //{
                        //    feeItemList.ItemRateFlag = "1";
                        //    feeItemList.NewItemRate = 1;
                        //    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = "�Է�";
                        //    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Red;
                        //}
                        //{21C33D5B-5583-4b1d-8023-278336C0C6C7}
                        string strGrade = feeItemList.Compare.CenterItem.ItemGrade; 
                        //this.myIGetSiItemGrade.GetSiItemGrade(this.rInfo.Pact.ID,feeItemList.Item.ID ,ref strGrade);
                        strGrade = FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode( strGrade );
                        feeItemList.ItemRateFlag = "1";
                        feeItemList.NewItemRate = 1;
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].Text = strGrade; //"�Է�";
                        this.fpSpread1_Sheet1.Cells[row, (int)Columns.Self].ForeColor = Color.Red;

                    }
                }
                #endregion

                #region �ж���Ӧ֢
                //��Ӧ֢�ӿ�{01DD7186-50F0-40fb-A91E-02A1A8358A83}
                FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient iAdptIllnessOutPatient = null;
                iAdptIllnessOutPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient)) as FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient;
                if (iAdptIllnessOutPatient != null)
                {
                    int returnValue = iAdptIllnessOutPatient.ProcessOutPatientFeeDetail(this.PatientInfo, ref feeItemList);
                    if (returnValue < 0)
                    {
                        return;
                    }
                    
                }

                #endregion

                this.fpSpread1_Sheet1.Rows[row].Tag = feeItemList;


                #region �Żݱ��� add by zuowy

                //FS.HISFC.Models.Fee.Outpatient..EcoRate ecoRate = new EcoRate();
                //ecoRate.RateType.ID = this.rInfo.User02;

                //if (this.rInfo.User02 == "NO" || this.rInfo.User02 == null || this.rInfo.User02 == string.Empty)
                //{
                //    ecoRate.Rate.RebateRate = 100;
                //}
                //else
                //{
                //    ecoRate.Item.ID = ((FeeItemList)this.fpSpread1_Sheet1.Rows[row].Tag).ID;
                //    //int iReturn = this.ecoRateManager.GetRate(ecoRate, false);
                //    ecoRate.Rate.RebateRate = 100;
                //    int varReturn = this.ecoRateManager.GetRateByItem(ecoRate);
                //    if (varReturn == -1)
                //    {
                //        MessageBox.Show(this.ecoRateManager.Err + "����ѡ����Ż���Ч!");
                //    }
                //    else if (varReturn == 0)
                //    {
                //        DataRow findRowAgain;
                //        DataRow[] rowFindsAgain = this.dvItem.Table.Select("ITEM_CODE = " + "'" + ecoRate.Item.ID + "'");

                //        if (rowFinds != null && rowFinds.Length > 0)
                //        {
                //            findRowAgain = rowFindsAgain[0];

                //            string feeCode = findRowAgain["FEE_CODE"].ToString();

                //            ecoRate.Item.ID = feeCode;

                //            varReturn = this.ecoRateManager.GetRateByMinFee(ecoRate);

                //            if (varReturn == -1)
                //            {
                //                MessageBox.Show(this.ecoRateManager.Err + "����ѡ����Ż���Ч!");
                //            }
                //        }
                //    }
                //}

                //FS.FrameWork.Public.String.FormatNumber(((FeeItemList)this.fpSpread1_Sheet1.Rows[row].Tag).Price =
                //    ((FeeItemList)this.fpSpread1_Sheet1.Rows[row].Tag).Price1 * ecoRate.Rate.RebateRate / 100, 4);
                //FS.HISFC.Models.Base.FT ft = this.ComputCost(feeItemList.Price,
                //    0, feeItemList);

                //((FeeItemList)this.fpSpread1_Sheet1.Rows[row].Tag).FT.TotCost = ft.TotCost;


                //if (feeItemList.FeePack == "1")
                //{
                //    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Price].Value = feeItemList.Price;
                //}
                //else
                //{
                //    this.fpSpread1_Sheet1.Cells[row, (int)Columns.Price].Value = feeItemList.Price / feeItemList.PackQty;
                //}
                //this.fpSpread1_Sheet1.Cells[row, (int)Columns.Cost].Value = ft.TotCost;

                #endregion

                for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
                {   //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    if (i == (int)Columns.InputCode || i == (int)Columns.Select)
                    {
                        this.fpSpread1_Sheet1.Cells[row, i].Locked = false;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[row, i].Locked = true;
                    }
                }
            }
            RefreshItemInfo();
            this.isDealCellChange = true;
        }

        /// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		private void RefreshItemInfo()
		{
			int row = this.fpSpread1_Sheet1.ActiveRowIndex;
			if(this.fpSpread1_Sheet1.Rows[row].Tag != null)
			{
				if(this.fpSpread1_Sheet1.Rows[row].Tag is FeeItemList)
				{
					FeeItemList f = this.fpSpread1_Sheet1.Rows[row].Tag as FeeItemList;
					string siType = string.Empty;
					decimal siRate = 0;
					if(f.FeePack == "1")
					{
						this.fpSpread1_Sheet1.Cells[row,(int)Columns.PriceUnit].Locked = true;
					}
					else
					{
						this.fpSpread1_Sheet1.Cells[row,(int)Columns.PriceUnit].Locked = false;
					}
                    ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
					this.fpSpread1_Sheet1.Cells[row,(int)Columns.InputCode].Text = f.Item.UserCode;


					if(f.Compare == null)
					{
						siType = "�Է�";
						siRate = 100;
					}
					else
					{
                        if (f.Compare.CenterItem.ItemGrade == "1")
						{
							siType = "����";
							siRate = 0;
						}
                        if (f.Compare.CenterItem.ItemGrade == "2")
						{
							siType = "����";
                            siRate = f.Compare.CenterItem.Rate * 100;
						}
                        if (f.Compare.CenterItem.ItemGrade == "3")
						{
							siType = "�Է�";
							siRate = 100;
						}
                        if (f.Compare.CenterItem.ID.Length <= 0)
						{
							siType = "�Է�";
							siRate = 100;
						}
					}
					//if(f.Item.IsPharmacy)
                    if (f.Item.ItemType == EnumItemType.Drug)
					{
                        //string itemCode = f.ID;
                        //DataRow findRow;

                        //DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "'");
								
                        //if(rowFinds == null||rowFinds.Length == 0)
                        //{
                        //    MessageBox.Show("����Ϊ: [" + itemCode +" ] ����Ŀ����ʧ��!");
                        //    return;
                        //}
                        //findRow = rowFinds[0];

                        //this.lbItemInfo.Text = "ҽ�����: " + siType + " ����:" + siRate.ToString() + "%"+"\n"
                        //    +"ͨ����:"+findRow["cus_name"].ToString()+" Ӣ����:"+findRow["en_name"].ToString().ToLower()+"\n"+
                        //    "����:"+findRow["OTHER_NAME"].ToString()+"\n"+
                        //    "���:"+f.Item.Specs+" ����:"+f.DoseInfo.Name;
					}
					else
					{
						//this.lbItemInfo.Text = "ҽ�����: " + siType + " ����:" + siRate.ToString() + "%";
					}
				}
			}
		}

        /// <summary>
        /// ��Ŀ��Ϣ
        /// </summary>
        /// <param name="row"></param>
        private void RefreshItemInfo(int row)
        {
            if (this.fpSpread1_Sheet1.Rows[row].Tag != null)
            {
                if (this.fpSpread1_Sheet1.Rows[row].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet1.Rows[row].Tag as FeeItemList;
                    string siType = string.Empty;
                    decimal siRate = 0;
                    //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    this.fpSpread1_Sheet1.Cells[row, (int)Columns.InputCode].Text = f.Item.UserCode;

                    if (f.Compare == null)
                    {
                        siType = "�Է�";
                        siRate = 100;
                    }
                    else
                    {
                        if (f.Compare.CenterItem.ItemGrade == "1")
                        {
                            siType = "����";
                            siRate = 0;
                        }
                        if (f.Compare.CenterItem.ItemGrade == "2")
                        {
                            siType = "����";
                            siRate = f.Compare.CenterItem.Rate * 100;
                        }
                        if (f.Compare.CenterItem.ItemGrade == "3")
                        {
                            siType = "�Է�";
                            siRate = 100;
                        }
                    }
                    //if (f.Item.IsPharmacy)
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        string itemCode = f.Item.ID;
                        DataRow findRow;

                        DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "'");

                        if (rowFinds == null || rowFinds.Length == 0)
                        {
                            MessageBox.Show("����Ϊ: [" + itemCode + " ] ����Ŀ����ʧ��!");
                            return;
                        }
                        findRow = rowFinds[0];

                        //this.lbItemInfo.Text = "ҽ�����: " + siType + " ����:" + siRate.ToString() + "%" + "\n"
                        //    + "ͨ����:" + findRow["cus_name"].ToString() + " Ӣ����:" + findRow["en_name"].ToString().ToLower() + "\n" +
                        //    "����:" + findRow["OTHER_NAME"].ToString() + "\n" +
                        //    "���:" + f.Specs + " ����:" + f.DoseInfo.Name;
                    }
                    else
                    {
                        //this.lbItemInfo.Text = "ҽ�����: " + siType + " ����:" + siRate.ToString() + "%";
                    }
                }
            }
        }
        
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="price">����</param>
        /// <param name="qty">����</param>
        /// <param name="f">��ǰ�շ���Ŀ��Ϣ</param>
        /// <returns>�ɹ�����FT��Ϣ,ʧ��null</returns>
        private FS.HISFC.Models.Base.FT ComputCost(decimal price, decimal qty, FeeItemList f)
        {
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();

            if (this.rInfo.Pact.PayKind.ID == "01")//�Է�
            {
                //if (f.FT.RebateCost > 0)
                //{
                //    ft = f.FT.Clone();
                //}
                //else
                //{
                    
                    ft.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                    if (ft.TotCost > 999999) 
                    {
                        MessageBox.Show(Language.Msg("���ܳ���999999�������Ŀ������!"));

                        return null;
                    }

                    ft.PayCost = 0;
                    ft.PubCost = 0;
                    ft.OwnCost = ft.TotCost;
                    //add by Niuxy�޸ļ���  
                    ft.RebateCost = FS.FrameWork.Public.String.FormatNumber(f.FT.RebateCost * f.Item.Qty / f.Item.PackQty, 2);
                    
                //}
            }
            if (this.rInfo.Pact.PayKind.ID == "02")//ҽ��
            {
                //if (f.FT.RebateCost > 0)
                //{
                //    ft = f.FT.Clone();
                //}
                //else
                //{
                    ft.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                    if (ft.TotCost > 999999)
                    {
                        MessageBox.Show(Language.Msg("���ܳ���999999�������Ŀ������!"));

                        return null;
                    }

                    ft.PayCost = 0;
                    ft.PubCost = 0;
                    ft.OwnCost = ft.TotCost;
                    //add by Niuxy�޸ļ���  
                    ft.RebateCost = FS.FrameWork.Public.String.FormatNumber(f.FT.RebateCost * f.Item.Qty / f.Item.PackQty, 2);
                //}

            }
            if (this.rInfo.Pact.PayKind.ID == "03")//����
            {
                if (f.FT.RebateCost > 0)
                {
                    ft = f.FT.Clone();
                }
                else if (f.IsGroup)
                {
                    ft.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                    if (ft.TotCost > 999999)
                    {
                        MessageBox.Show(Language.Msg("���ܳ���999999�������Ŀ������!"));

                        return null;
                    }

                    ft.OwnCost = ft.TotCost;
                }
                else
                {
                    ft.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                    if (ft.TotCost > 999999)
                    {
                        MessageBox.Show(Language.Msg("���ܳ���999999�������Ŀ������!"));

                        return null;
                    }

                    f.FT.TotCost = ft.TotCost;
                }
            }

            return ft;
        }

        /// <summary>
        /// ���㵱ǰ�շ��ܶ��ʱ��ȫ�ԷѴ����Ժ���ҽ���͹��Ѻ�ͬ��λ
        /// </summary>
        private decimal SumCost()
        {
            decimal sumCost = 0;
            //���������Ŀ��Ϣ,���������Ŀ����ϸ
            ArrayList alFee = this.GetFeeItemList();

            this.rightControl.SetInfomation(this.rInfo, null, alFee, null);

            if (this.leftControl != null) 
            {
                this.leftControl.PatientInfo = this.rInfo;
                this.leftControl.RefreshDisplayInfomation(alFee);
            }

            //ArrayList alCharge = this.GetFeeItemListForCharge(false);
            ArrayList alCharge = this.GetFeeItemListForCharge();
            //{BBE9766A-A539-485e-A03B-9972DC675538} �˷Ѳ���
            if (this.FeeItemListChanged != null)
            {
                this.FeeItemListChanged(alCharge);
            }
            //{BBE9766A-A539-485e-A03B-9972DC675538} ����
            this.SumLittleCostAll();
            
            return sumCost;
        }

        /// <summary>
        /// �����ײ�ֳ���ϸ
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private ArrayList ConvertGroupToDetail(FeeItemList f)
        {
            ArrayList undrugCombList = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(f.Item.ID);
            ArrayList alTemp = new ArrayList();
            if (undrugCombList == null)
            {
                errText = "���������ϸ����!" + undrugPackAgeManager.Err;

                return null;
            }
            decimal price = 0;
            decimal count = 0;
            string feeCode = string.Empty;
            string itemType = string.Empty;
            decimal totCost = 0;
            FeeItemList feeDetail = null;
            if (f.Order.ID == null || f.Order.ID == string.Empty)
            {
                f.Order.ID = this.orderIntegrate.GetNewOrderID();
                if (f.Order.ID == null || f.Order.ID == string.Empty)
                {
                    this.errText = "���ҽ����ˮ�ų���!";

                    return null;
                }
            }

            //������Ŀ��ϸ�ļӳɣ����⣩����
            decimal itemRate = 1;

            foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugCombo in undrugCombList)
            {
                DataRow rowFindZT;
                DataRow[] rowFindZTs = dsItem.Tables[0].Select("ITEM_CODE = " + "'" + undrugCombo.ID + "'");
                if (rowFindZTs == null || rowFindZTs.Length == 0)
                {
                    this.errText = "����������ϸ����!";

                    continue;
                }
                rowFindZT = rowFindZTs[0];

                feeDetail = new FeeItemList();

                feeCode = rowFindZT["FEE_CODE"].ToString();
                try
                {
                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowTime.Ticks - this.rInfo.Birthday.Ticks)).TotalDays / 365);

                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}���ӻ�ȡ�����
                    string priceForm = this.rInfo.Pact.PriceForm;
                    decimal unitPrice = NConvert.ToDecimal(rowFindZT["UNIT_PRICE"]);
                    decimal childPrice = NConvert.ToDecimal(rowFindZT["CHILD_PRICE"]);
                    decimal SPPrice = NConvert.ToDecimal(rowFindZT["SP_PRICE"]);
                    decimal purchasePrice = NConvert.ToDecimal(rowFindZT["PUCHASE_PRICE"]);

                    // ����ԭʼĬ�ϼ۸�
                    feeDetail.Item.ChildPrice = unitPrice;
                    //ԭʼ�۸�
                    decimal orgPrice = price;

                    itemRate = feeIntegrate.GetItemRateForZT(f.Item.ID, undrugCombo.ID);
                    price = this.feeIntegrate.GetPrice(undrugCombo.ID, this.rInfo, age, unitPrice, childPrice, SPPrice, purchasePrice, ref orgPrice, itemRate);
                    feeDetail.OrgPrice = orgPrice;
                }
                catch (Exception e)
                {
                    this.errText = e.Message;

                    return null;
                }

                //�����Żݱ������¼��㵥��------------------------- 
                string errMsg = string.Empty;
                PactItemRate myRate = Class.Function.PactRate(this.rInfo, feeDetail, ref errMsg);
                if (myRate == null)
                {
                    this.errText = errMsg;
                    return null;
                }

                price *= 1 - myRate.Rate.RebateRate;
                //--------------------------------------------------
                count = NConvert.ToDecimal(f.Item.Qty) * undrugCombo.Qty;
                totCost = price * count;

                feeDetail.Patient = f.Patient.Clone();
                feeDetail.Item.ID = rowFindZT["ITEM_CODE"].ToString();
                feeDetail.Item.Name = rowFindZT["ITEM_NAME"].ToString();
                feeDetail.Name = feeDetail.Item.Name;
                feeDetail.ID = feeDetail.Item.ID;
                itemType = rowFindZT["DRUG_FLAG"].ToString();
                if (itemType == "0")
                {
                    //feeDetail.Item.IsPharmacy = false;
                    feeDetail.Item.ItemType =  EnumItemType.UnDrug;
                    feeDetail.IsGroup = false;
                }
                if (itemType == "1")
                {
                    //feeDetail.Item.IsPharmacy = true;
                    feeDetail.Item.ItemType = EnumItemType.Drug;
                    feeDetail.IsGroup = false;
                }
                if (itemType == "2")
                {
                    //feeDetail.Item.IsPharmacy = false;
                    feeDetail.Item.ItemType = EnumItemType.UnDrug;
                    feeDetail.IsGroup = true;
                }
                feeDetail.RecipeOper = f.RecipeOper.Clone();
                feeDetail.Item.Price = price;
                feeDetail.Item.Specs = rowFindZT["SPECS"].ToString();
                feeDetail.Item.SysClass.ID = rowFindZT["SYS_CLASS"].ToString();
                feeDetail.Item.MinFee.ID = feeCode;
                feeDetail.Item.PackQty = NConvert.ToDecimal(rowFindZT["PACK_QTY"].ToString());
                feeDetail.Item.Qty = count;
                feeDetail.Days = NConvert.ToDecimal(f.Days);
                feeDetail.FT.TotCost = totCost;
                //�Է���ˣ�������Ϲ�����Ҫ���¼���!!!
                feeDetail.FT.OwnCost = totCost;
                feeDetail.ExecOper = f.ExecOper.Clone();
                feeDetail.Item.PriceUnit = rowFindZT["MIN_UNIT"].ToString() == string.Empty ? "��" : rowFindZT["MIN_UNIT"].ToString();
                if (rowFindZT["CONFIRM_FLAG"].ToString() == "2" || rowFindZT["CONFIRM_FLAG"].ToString() == "3" || rowFindZT["CONFIRM_FLAG"].ToString() == "1")
                {
                    feeDetail.Item.IsNeedConfirm = true;
                }
                else
                {
                    feeDetail.Item.IsNeedConfirm = false;
                }
                
                feeDetail.Item.IsNeedBespeak = NConvert.ToBoolean(rowFindZT["NEEDBESPEAK"].ToString());

                feeDetail.Order.ID = f.Order.ID;
                feeDetail.UndrugComb.ID = f.Item.ID;
                feeDetail.UndrugComb.Name = f.Item.Name;
                feeDetail.Order.Combo.ID = f.Order.Combo.ID;
                feeDetail.Item.IsMaterial = f.Item.IsMaterial;
                feeDetail.RecipeSequence = f.RecipeSequence;
                feeDetail.FTSource = f.FTSource;
                feeDetail.FeePack = f.FeePack;
                if (this.rInfo.Pact.PayKind.ID == "03")
                {
                    FS.HISFC.Models.Base.PactItemRate pactRate = null;
                                    
                    if (pactRate == null)
                    {
                        pactRate = this.pactUnitItemRateManager.GetOnepPactUnitItemRateByItem(this.rInfo.Pact.ID, feeDetail.Item.ID);
                    }
                    if (pactRate != null)
                    {
                        if (pactRate.Rate.PayRate != this.rInfo.Pact.Rate.PayRate)
                        {
                            if (pactRate.Rate.PayRate == 1)//�Է�
                            {
                                feeDetail.ItemRateFlag = "1";
                            }
                            else
                            {
                                feeDetail.ItemRateFlag = "3";
                            }
                        }
                        else
                        {
                            feeDetail.ItemRateFlag = "2";

                        }
                        if (f.ItemRateFlag == "3")
                        {                            
                            feeDetail.OrgItemRate = f.OrgItemRate;
                            feeDetail.NewItemRate = f.NewItemRate;
                            feeDetail.ItemRateFlag = "2";
                        }
                    }
                    else
                    {
                        if (f.ItemRateFlag == "3")
                        {
                            
                            if (rowFindZT["ZF"].ToString() != "1")
                            {
                                feeDetail.OrgItemRate = f.OrgItemRate;
                                feeDetail.NewItemRate = f.NewItemRate;
                                feeDetail.ItemRateFlag = "2";
                            }
                        }
                        else
                        {
                            feeDetail.OrgItemRate = f.OrgItemRate;
                            feeDetail.NewItemRate = f.NewItemRate;
                            feeDetail.ItemRateFlag = f.ItemRateFlag;
                        }
                    }
                }

                alTemp.Add(feeDetail);
            }
            if (alTemp.Count > 0)
            {
                if (f.FT.RebateCost > 0)//�м���
                {
                    if (this.rInfo.Pact.PayKind.ID != "01")
                    {
                        MessageBox.Show(Language.Msg("��ʱ��������Էѻ��߼���!"));

                        return null;
                    }
                    //decimal rebateRate =
                    //    FS.FrameWork.Public.String.FormatNumber(
                    //    f.FT.RebateCost / (f.FT.OwnCost + f.FT.RebateCost), 2);
                    //decimal tempFix = 0;
                    //decimal tempRebateCost = 0;
                    //foreach (FeeItemList feeTemp in alTemp)
                    //{
                    //    feeTemp.FT.RebateCost = (feeTemp.FT.OwnCost + feeTemp.FT.RebateCost) * rebateRate;
                    //    tempRebateCost += feeTemp.FT.RebateCost;
                    //    feeTemp.FT.OwnCost = feeTemp.FT.OwnCost - feeTemp.FT.RebateCost;
                    //    feeTemp.FT.TotCost = feeTemp.FT.TotCost - feeTemp.FT.RebateCost;
                    //}
                    //tempFix = f.FT.RebateCost - tempRebateCost;
                    //FeeItemList fFix = alTemp[0] as FeeItemList;
                    //fFix.FT.RebateCost = fFix.FT.RebateCost + tempFix;
                    //fFix.FT.OwnCost = fFix.FT.OwnCost - tempFix;
                    //fFix.FT.TotCost = fFix.FT.TotCost - tempFix;
                    //���ⵥ����
                    decimal rebateRate =
                        FS.FrameWork.Public.String.FormatNumber(f.FT.RebateCost / f.FT.OwnCost, 2);
                    decimal tempFix = 0;
                    decimal tempRebateCost = 0;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        feeTemp.FT.RebateCost = (feeTemp.FT.OwnCost ) * rebateRate;
                        tempRebateCost += feeTemp.FT.RebateCost;
                        //feeTemp.FT.OwnCost = feeTemp.FT.OwnCost - feeTemp.FT.RebateCost;
                        //feeTemp.FT.TotCost = feeTemp.FT.TotCost - feeTemp.FT.RebateCost;
                    }
                    tempFix = f.FT.RebateCost - tempRebateCost;
                    FeeItemList fFix = alTemp[0] as FeeItemList;
                    fFix.FT.RebateCost = fFix.FT.RebateCost + tempFix;
                    //fFix.FT.OwnCost = fFix.FT.OwnCost - tempFix;
                    //fFix.FT.TotCost = fFix.FT.TotCost - tempFix;
                }
            }
            if (alTemp.Count > 0)
            {
                if (f.SpecialPrice > 0)//�������Է�
                {
                    decimal tempPrice = 0m;
                    string id = string.Empty;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        if (feeTemp.Item.Price > tempPrice)
                        {
                            id = feeTemp.Item.ID;
                            tempPrice = feeTemp.Item.Price;
                        }
                    }

                    foreach (FeeItemList fee in alTemp)
                    {
                        if (fee.Item.ID == id)
                        {
                            fee.SpecialPrice = f.SpecialPrice;

                            break;
                        }
                    }
                }
            }

            return alTemp;
        }

        /// <summary>
        /// �ж�ִ�п���Ϊ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int JudegExeDept()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Cells[i, (int)Columns.ExeDept].Text == string.Empty ||
                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.ExeDept].Text == "��")
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                    {
                        if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(i, (int)Columns.ExeDept);

                            return -1;
                        }
                    }

                }
            }

            return 1;
        }

        /// <summary>
        /// ��֤�Ƿ�������Ŀ
        /// </summary>
        /// <param name="row">��ǰ��</param>
        /// <param name="f">��Ŀʵ��</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        private bool IsInputItem(int row, ref FeeItemList f)
        {
            if (this.fpSpread1_Sheet1.Rows[row].Tag == null)
            {
                MessageBox.Show(Language.Msg("����������Ŀ"));
                this.fpSpread1.Focus();
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.SetActiveCell(row, (int)Columns.InputCode);
                
                return false;
            }
            if (this.fpSpread1_Sheet1.Rows[row].Tag is FeeItemList)
            {
                f = this.fpSpread1_Sheet1.Rows[row].Tag as FeeItemList;
            }
            else
            {
                MessageBox.Show(Language.Msg("����������Ŀ"));
                this.fpSpread1.Focus();
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.SetActiveCell(row, (int)Columns.InputCode);
            
                return false;
            }

            return true;
        }

        /// <summary>
        /// ������ͬ��Ϻŵ�ҩƷ��Ժע����Ϊͬһ
        /// </summary>
        /// <param name="combNo">��Ϻ�</param>
        /// <param name="injects">ע�����</param>
        private void RefreshSameCombNoInjects(string combNo, int injects)
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        //if (f.Item.IsPharmacy && f.Order.Combo.ID == combNo)
                        if (f.Item.ItemType == EnumItemType.Drug && f.Order.Combo.ID == combNo)
                        {
                            f.InjectCount = injects;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ������ͬ��ϵ�Ժע����
        /// </summary>
        /// <param name="combNo"></param>
        /// <returns></returns>
        private int GetInjectSameCombs(string combNo)
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                FeeItemList feeItem = null;

                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        feeItem = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        if (feeItem.Order.Combo.ID == combNo)
                        {
                            if (feeItem.InjectCount > 0)
                            {
                                return feeItem.InjectCount;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// ʹ��ͬ��ϵ�Ƶ�λ��÷���ͬ
        /// </summary>
        /// <param name="currRow">��ǰ��</param>
        /// <param name="combNO">��Ϻ�</param>
        /// <param name="obj">���ʵ��</param>
        /// <param name="type">����</param>
        private void DealFreqOrUsageHaveSameCombNo(int currRow, string combNO, NeuObject obj, string type)
        {
            if (combNO == null || combNO.Length <= 0)
            {
                return;
            }

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                FeeItemList feeItem = null;

                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        feeItem = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        if (feeItem.Order.Combo.ID == combNO && i != currRow)
                        {
                            if (type == "1")
                            {
                                feeItem.Order.Frequency.ID = obj.ID;
                                feeItem.Order.Frequency.Name = obj.Name;
                                if (freqDisplayType == "0")//����
                                {
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Freq].Text = obj.Name;
                                }
                                else
                                {
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Freq].Text = obj.ID;
                                }
                            }
                            else
                            {
                                feeItem.Order.Usage.ID = obj.ID;
                                feeItem.Order.Usage.Name = obj.Name;
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Usage].Text = feeItem.Order.Usage.Name;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��֤�����Ƿ�����Ϸ�
        /// </summary>
        /// <param name="row">��ǰ��</param>
        /// <param name="col">��ǰ��</param>
        /// <param name="colName">������</param>
        /// <param name="maxValue">���ֵ</param>
        /// <param name="minValue">��Сֵ</param>
        /// <param name="currValue">���صĵ�ǰ����ֵ</param>
        /// <param name="showErr">�Ƿ���ʾ����</param>
        /// <returns>true�Ϸ� false���Ϸ�</returns>
        private bool InputDataIsValid(int row, int col, string colName, decimal maxValue, decimal minValue, ref decimal currValue, bool showErr)
        {
            if (this.fpSpread1_Sheet1.Cells[row, col].Text.ToString() == string.Empty)
            {
                currValue = 0m;
            }
            else
            {
                try
                {
                    currValue = NConvert.ToDecimal(
                        FS.FrameWork.Public.String.ExpressionVal(
                        this.fpSpread1_Sheet1.Cells[row, col].Text.ToString()));
                }
                catch
                { }
            }
            if (currValue < minValue)
            {
                MessageBox.Show(colName + Language.Msg("��ֵ����С��") + minValue.ToString() + "!");
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.SetActiveCell(row, col);
                
                return false;
            }
            if (currValue > maxValue)
            {
                MessageBox.Show(colName + Language.Msg("��ֵ���ܴ���") + maxValue.ToString() + "!");
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.SetActiveCell(row, col);
            
                return false;
            }

            return true;
        }

        private bool IsInputValid() 
        {
            this.isDealCellChange = false;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++) 
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null && this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList) 
                {   
                    decimal qty = 0;
                    //�ж�����
                    try
                    {
                        qty = NConvert.ToDecimal(FS.FrameWork.Public.String.ExpressionVal(this.fpSpread1_Sheet1.Cells[i, (int)Columns.Amount].Text.ToString()));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Language.Msg("����ļ��㹫ʽ����ȷ������������!") + ex.Message);
                        this.fpSpread1.Focus();
                        this.fpSpread1_Sheet1.SetActiveCell(i, (int)Columns.Amount);

                        this.isDealCellChange = true;

                        return false;
                    }

                    qty = FS.FrameWork.Public.String.FormatNumber(qty, 2);

                    if (qty <= 0)
                    {
                        MessageBox.Show(Language.Msg("��������С�ڻ��ߵ�����,����������"));
                        this.fpSpread1.Select();
                        this.fpSpread1.Focus();
                        this.fpSpread1_Sheet1.SetActiveCell(i, (int)Columns.Amount, false);

                        this.isDealCellChange = true;

                        return false;
                    }

                    if (qty > 99999)
                    {
                        MessageBox.Show(Language.Msg("�������ܴ���99999������������"));
                        this.fpSpread1.Select();
                        this.fpSpread1.Focus();
                        this.fpSpread1_Sheet1.SetActiveCell(i, (int)Columns.Amount, false);

                        this.isDealCellChange = true;

                        return false;
                    }


                    //�жϸ���

                    FeeItemList feeTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                    //if (feeTemp.Item.IsPharmacy && feeTemp.Item.SysClass.ID.ToString() == "PCC")//��ҩ
                    if (feeTemp.Item.ItemType == EnumItemType.Drug && feeTemp.Item.SysClass.ID.ToString() == "PCC")//��ҩ
                    {

                        decimal days = 0;

                        try
                        {
                            days = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Columns.Days].Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Language.Msg("������������Ϸ�") + ex.Message);
                            this.fpSpread1.Focus();
                            this.fpSpread1_Sheet1.SetActiveCell(i, (int)Columns.Days);

                            this.isDealCellChange = true;

                            return false;
                        }
                        if (days <= 0)
                        {
                            MessageBox.Show(Language.Msg("����ĸ������Ϸ�, �����������0"));
                            this.fpSpread1.Focus();
                            this.fpSpread1_Sheet1.SetActiveCell(i, (int)Columns.Days);

                            this.isDealCellChange = true;

                            return false;
                        }
                    }
                }
            }

            this.isDealCellChange = true;
            
            return true;
        }

        /// <summary>
        /// �жϿ���Ƿ���
        /// </summary>
        /// <returns></returns>
        private bool IsStoreEnough(FeeItemList feeItem,int row)
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
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                FeeItemList feeItem1 = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
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
                    MessageBox.Show("��ǰ�����:" + (storeSum / feeItem.Item.PackQty).ToString() + "|��������:" + this.fpSpread1_Sheet1.Cells[row, 4].Value.ToString() + "   ��治��!");
                }
                else
                {
                    MessageBox.Show("��ǰ�����:" + storeSum.ToString() + "|��������:" + this.fpSpread1_Sheet1.Cells[row, 4].Value.ToString() + "   ��治��!");
                }
                this.fpSpread1_Sheet1.SetActiveCell(row, 3, true);
                //if (feeItem.FeePack == "1")
                //{
                //    this.fpSpread1_Sheet1.Cells[row, 3].Value = FS.FrameWork.Function.NConvert.ToDecimal(storeSum / feeItem.Item.PackQty).ToString();
                //}
                //else
                //{
                //    //this.fpSpread1_Sheet1.Cells[row, 3].Value = storeSum;
                //}

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

        /// <summary>
        /// �����Ƿ����ѡ���շ�{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        /// </summary>
        private void SetIsCanSelectItemAndFee() 
        {
            if (this.isCanSelectItemAndFee)
            {   
                this.fpSpread1_Sheet1.Columns[(int)Columns.Select].Visible = true;
            }
            else 
            {
                this.fpSpread1_Sheet1.Columns[(int)Columns.Select].Visible = false;
            }
        }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}����

        #endregion

        #region ���з���

        /// <summary>
        /// �޸ĺ�ͬ��λ��ˢ����Ŀ��Ϣ.
        /// </summary>
        public void RefreshItemForPact()
        {
            this.isDealCellChange = false;

            for (int currRow = 0; currRow < this.fpSpread1_Sheet1.RowCount; currRow++)
            {
                if (this.fpSpread1_Sheet1.Rows[currRow].Tag != null && this.fpSpread1_Sheet1.Rows[currRow].Tag.GetType() == typeof(FeeItemList))
                {
                    EcoRate ecoRate = new EcoRate();
                    ecoRate = this.rInfo.EcoRate.Clone();

                    if (ecoRate.RateType.ID == null || ecoRate.RateType.ID == "NO" || ecoRate.RateType.ID == string.Empty)
                    {
                        ecoRate.Rate.RebateRate = 100;
                        //string errMsg = string.Empty;
                        //ecoRate.Rate = (Class.Function.PactRate(this.rInfo.Pact.ID, (FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag, ref errMsg)).Rate;

                        //ecoRate.Rate.RebateRate = (1 - ecoRate.Rate.RebateRate) * 100; 
                    }
                    else
                    {
                        ecoRate.Item.ID = ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).ID;
         
                        ecoRate.Rate.RebateRate = 100;
                        
         
                        int iReturn = this.ecoRateManager.GetRateByItem(ecoRate);
                        
                        if (iReturn == -1)
                        {
                            MessageBox.Show(this.ecoRateManager.Err + Language.Msg("����ѡ����Ż���Ч!"));
                        }
                        else if (iReturn == 0)
                        {
                            DataRow findRow;
                            DataRow[] rowFinds = this.dvItem.Table.Select("ITEM_CODE = " + "'" + ecoRate.Item.ID + "'");

                            if (rowFinds != null && rowFinds.Length > 0)
                            {
                                findRow = rowFinds[0];

                                string feeCode = findRow["FEE_CODE"].ToString();

                                ecoRate.Item.ID = feeCode;

                                iReturn = this.ecoRateManager.GetRateByMinFee(ecoRate);

                                if (iReturn == -1)
                                {
                                    MessageBox.Show(this.ecoRateManager.Err + Language.Msg("����ѡ����Ż���Ч!"));
                                }
                            }
                        }
                    }

                    FS.FrameWork.Public.String.FormatNumber(((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).Item.Price =
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).OrgPrice * ecoRate.Rate.RebateRate / 100, 4);
                    FS.HISFC.Models.Base.FT ft = this.ComputCost(((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).Item.Price,
                        0, ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag));

                    if (ft == null) 
                    {
                        return;
                    }

                    ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).FT.TotCost = ft.TotCost;

                    if (((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).FeePack == "1")
                    {
                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Price].Value = FS.FrameWork.Public.String.FormatNumber(((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).Item.Price, 4);
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Price].Value = FS.FrameWork.Public.String.FormatNumber(((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).Item.Price
                            / ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).Item.PackQty, 4);
                    }
                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Cost].Value = ft.TotCost;

                    #region �Է�
                    if (this.rInfo.Pact.PayKind.ID == "01")//�Է�
                    {
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).OrgItemRate = 1;
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).NewItemRate = 1;
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).ItemRateFlag = "1";
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).FT.OwnCost =
                            ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).FT.TotCost;
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).FT.PayCost = 0;
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).FT.PubCost = 0;
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).FT.ExcessCost = 0;
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).FT.DrugOwnCost = 0;
                        FeeItemList feeItemList = ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag);
                        
                        if (this.isOwnDisplayYB)
                        {
                            if (feeItemList.Compare == null)
                            {
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                            }
                            else
                            {
                                if (feeItemList.Compare.CenterItem.ItemGrade == "1")
                                {
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                                }
                                else if (feeItemList.Compare.CenterItem.ItemGrade == "2")
                                {
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                                }
                                else
                                {
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                                }
                            }
                        }
                        
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                        }
                    }
                    #endregion
                    #region ����
                    if (this.rInfo.Pact.PayKind.ID == "03")
                    {
                        string itemCode = ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).Item.ID;

                        FeeItemList feeTempItem = (FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag;

                        DataRow findRow;
                        DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "'");

                        if (rowFinds == null || rowFinds.Length == 0)
                        {
                            MessageBox.Show("����Ϊ: [" + itemCode + " ] ����Ŀ����ʧ��!");
                            this.isDealCellChange = true;
                            return;
                        }
                        findRow = rowFinds[0];
                        
                        
                        if (feeTempItem.ItemRateFlag != "3")
                        {
                            ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).OrgItemRate = this.rInfo.Pact.Rate.PayRate;
                            ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).NewItemRate = this.rInfo.Pact.Rate.PayRate;
                            ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).ItemRateFlag = "2";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                        }
                        else
                        {
                            ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag).ItemRateFlag = "3";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Blue;
                            
                        }
                    }
                    
                    #endregion
                    #region ҽ��
                    if (this.rInfo.Pact.PayKind.ID == "02")
                    {
                        FeeItemList feeItemList = ((FeeItemList)this.fpSpread1_Sheet1.Rows[currRow].Tag);
                        if (feeItemList.Compare == null)
                        {
                            feeItemList.ItemRateFlag = "1";
                            feeItemList.NewItemRate = 1;
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                        }
                        else
                        {
                            //if (feeItemList.Compare.CenterItem.ItemGrade == "1")
                            //{
                            //    feeItemList.ItemRateFlag = "1";
                            //    feeItemList.NewItemRate = feeItemList.Compare.CenterItem.Rate;
                            //    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            //    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                            //}
                            //else if (feeItemList.Compare.CenterItem.ItemGrade == "2")
                            //{
                            //    feeItemList.ItemRateFlag = "1";
                            //    feeItemList.NewItemRate = feeItemList.Compare.CenterItem.Rate;
                            //    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "����";
                            //    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Black;
                            //}
                            //else
                            //{
                            //    feeItemList.ItemRateFlag = "1";
                            //    feeItemList.NewItemRate = 1;
                            //    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = "�Է�";
                            //    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
                            //}
                            //{21C33D5B-5583-4b1d-8023-278336C0C6C7}
                            string strGrade = feeItemList.Compare.CenterItem.ItemGrade;
                            //this.myIGetSiItemGrade.GetSiItemGrade(this.rInfo.Pact.ID,feeItemList.Item.ID ,ref strGrade);
                            strGrade = FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode( strGrade );
                            feeItemList.ItemRateFlag = "1";
                            feeItemList.NewItemRate = 1;
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].Text = strGrade; //"�Է�";
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Self].ForeColor = Color.Red;
        
                        }
                    #endregion
                    }
                }
            }
            SumCost();
            this.isDealCellChange = true;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Init() 
        {
            if (this.InitControlParams() == -1) 
            {
                MessageBox.Show("��ʼ�������б����!");

                return -1;
            }
            
            //��ò���Ա������Ϣ
            myOperator = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            if (myOperator == null)
            {
                MessageBox.Show("��ò���Ա������Ϣ����!");

                return -1;
            }
            ArrayList alApprItem = this.managerIntegrate.GetConstantList("ApprItem");
            if (alApprItem != null)
            {
                this.apprItemHelper.ArrayObject = alApprItem;
            }

            ArrayList alPhaFeeCode = this.managerIntegrate.GetConstantList("DrugMinFee");
            if (alPhaFeeCode != null)
            {
                this.phaFeeCodeHelper.ArrayObject = alPhaFeeCode;
            }

            ArrayList alSpecialItem = this.managerIntegrate.GetConstantList("DrugRate");
            if (alSpecialItem != null)
            {
                this.specialItemHelper.ArrayObject = alSpecialItem;
            }
            ArrayList alInvertUnit = this.managerIntegrate.GetConstantList("InvertUnit");
            if (alInvertUnit != null)
            {
                this.invertUnitHelper.ArrayObject = alInvertUnit;
            }

            ArrayList alInvertUnitDrug = this.managerIntegrate.GetConstantList("InvertDrug");
            if (alInvertUnit != null)
            {
                this.specialInvertUnitHelper.ArrayObject = alInvertUnitDrug;
            }

            

            //������Ŀ�б�
            this.LoadItem(myOperator.Dept.ID);

            this.fpSheetItem.DataSource = dvItem;
            
            //�����б���ʾ���
            InitFp();

            //�����շ���Ŀ������ʾ���
            this.chooseItemControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IChooseItemForOutpatient>
                (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_CHOOSE_ITEM, new ucPopSelected());
                chooseItemControl.ItemKind = this.itemKind;
                //{21C33D5B-5583-4b1d-8023-278336C0C6C7}
                myIGetSiItemGrade = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade)) as FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade;

                if (myIGetSiItemGrade != null)
                {
                    this.chooseItemControl.IGetSiItemGrade = myIGetSiItemGrade;
                }

            this.chooseItemControl.Init();

            //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳�����
            threadItemInit = new Thread(FillFilterControl);
            threadItemInit.Name = "add";
            threadItemInit.IsBackground = true;
            threadItemInit.Start();
            //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳����� ����

            //this.chooseItemControl.SetDataSet(this.dsItem);

            if(this.chooseItemControl.ChooseItemType == FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemChanging)
            {
                this.Parent.Parent.Controls.Add((Control)this.chooseItemControl);
            }

            //����ѡ����Ŀ�����¼�
            this.chooseItemControl.SelectedItem += new FS.HISFC.BizProcess.Integrate.FeeInterface.WhenGetItem(chooseItemControl_SelectedItem);
            //����ִ�п���
            
            InitDept();

            //����Ƶ��
            InitFreq();

            myHelpFreq.ArrayObject = alFreq;
            //�����÷�
            InitUsage();
            myHelpUsage.ArrayObject = alUsage;
            //��ͬ��λ���Ѵ���
            InitBillPact();
            myBillPactHelper.ArrayObject = alBillPact;
            
            myInjec.WhenInputInjecs += new ucInjec.myDelegate(myInjec_WhenInputInjecs);

            if (this.rightControl != null) 
            {
                this.rightControl.Init();
                this.rightControl.SetDataSet(this.dsItem);
                this.rightControl.SetFeeCodeIsDrugArrayListObj(this.phaFeeCodeHelper);
            }
            //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Select].Value = true;

            return 1;
        }

        //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť

        /// <summary>
        /// ˢ����Ŀ�б�
        /// </summary>
        public void RefreshItem() 
        {
            //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳�����,�����߳̽�����,����ִ��ˢ��
            if (this.threadItemInit.ThreadState == ThreadState.Stopped) 
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("����ˢ����Ŀ�Լ����,��ȴ�...");
                Application.DoEvents();

                (this.chooseItemControl as Control).Visible = false;
                int row = this.fpSpread1_Sheet1.ActiveRowIndex;

                this.LoadItem(myOperator.Dept.ID);
                this.fpSheetItem.DataSource = dvItem;
                this.chooseItemControl.Init();
                FillFilterControl();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.Focus();
                this.fpSpread1.Select();
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.SetActiveCell(row, 0);
            }
        }

        // //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť ���

        //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳�����
        /// <summary>
        /// ��������Ŀ�ؼ�
        /// </summary>
        private void FillFilterControl()
        {
            this.chooseItemControl.SetDataSet(this.dsItem);
        }
        //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳����� ����

        //�����շ� {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
        void chooseItemControl_SelectedItem(string itemCode, string drugFlag, string exeDeptCode, decimal price)
        {
            //���
            this.alAddRows.Clear();

            if (!this.isCanAddItem && !isQuitFee)
            {
                MessageBox.Show(Language.Msg("�뵥ѡ��һ����������������Ŀ!"));
                this.fpSpread1.Focus();
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.InputCode, false);

                //this.isDealCellChange = true;

                return;
            }
            //�����շ� {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
            SetItem(itemCode, drugFlag, exeDeptCode, this.fpSpread1_Sheet1.ActiveRowIndex, 1, price);
            this.Focus();
            this.fpSpread1.Focus();

            DataRow rowFind;
            DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "'");

            if (rowFinds == null || rowFinds.Length == 0)
            {
                MessageBox.Show("������Ŀ����!");
                return;
            }
            rowFind = rowFinds[0];

            if (rowFind == null)
            {
                MessageBox.Show("������Ŀʧ��!");
                return;
            }
            for (int i = 0; i < this.alAddRows.Count; i++)
            {
                string itemType = rowFind["DRUG_FLAG"].ToString();
                if (itemType == "0")//��ҩƷ
                {
                    this.fpSpread1_Sheet1.Cells[(int)alAddRows[i], (int)Columns.Amount].Locked = false;
                    this.fpSpread1_Sheet1.SetActiveCell((int)alAddRows[i], (int)Columns.Amount, false);
                    this.fpSpread1_Sheet1.Cells[(int)alAddRows[i], (int)Columns.CombNo].Locked = false;
                }
                else
                {
                    if (rowFind["SYS_CLASS"].ToString() == "PCC")//��ҩֱ����������
                    {
                        this.fpSpread1_Sheet1.Cells[(int)alAddRows[i], (int)Columns.DoseOnce].Locked = false;
                        this.fpSpread1_Sheet1.SetActiveCell((int)alAddRows[i], (int)Columns.DoseOnce, false);
                        this.fpSpread1_Sheet1.Cells[(int)alAddRows[i], (int)Columns.CombNo].Locked = false;
                    }
                    else//����ҩƷ��ת����������λ��
                    {
                        this.fpSpread1_Sheet1.Cells[(int)alAddRows[i], (int)Columns.Amount].Locked = false;
                        this.fpSpread1_Sheet1.SetActiveCell((int)alAddRows[i], (int)Columns.Amount, false);
                        this.fpSpread1_Sheet1.Cells[(int)alAddRows[i], (int)Columns.CombNo].Locked = false;
                    }
                }
            }
            if (alAddRows.Count > 1)
            {
                if ((int)alAddRows[alAddRows.Count - 2] <= this.fpSpread1_Sheet1.Rows.Count - 2)
                {    //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    this.fpSpread1_Sheet1.SetActiveCell((int)alAddRows[alAddRows.Count - 1] + 1, (int)Columns.InputCode, false);
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void AddNewRow()
        {
            int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;
            this.fpSpread1_Sheet1.Rows.Add(currRow + 1, 1);
            //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            this.fpSpread1_Sheet1.SetActiveCell(currRow + 1, (int)Columns.InputCode);
            //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            this.fpSpread1_Sheet1.Cells[currRow + 1, (int)Columns.Select].Value = true;

            SumLittleCostAll();
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        public void DeleteRow()
        {
            //this.fpSpread1.StopCellEditing();
            int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (this.fpSpread1_Sheet1.Rows[currRow].Tag != null)
            {
                if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList feeTemp = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;
                    if (feeTemp.RecipeNO != null && feeTemp.RecipeNO != string.Empty && feeTemp.Order.ID != string.Empty)
                    {
                        ArrayList alTemp = this.outpatientManager.QueryFeeDetailFromMOOrder(feeTemp.Order.ID);
                        if (alTemp != null && alTemp.Count > 0)
                        {
                            feeTemp = alTemp[0] as FeeItemList;

                            if (feeTemp.IsAccounted) 
                            {
                                MessageBox.Show(Language.Msg("����Ŀ�Ѿ���ȡ�����˻�,����ɾ��!"));

                                return;
                            }

                            if (feeTemp.IsConfirmed)
                            {
                                MessageBox.Show(Language.Msg("����Ŀ�Ѿ����ն�ȷ�ϣ�����ɾ��!"));

                                return;
                            }
                            if (feeTemp.PayType != FS.HISFC.Models.Base.PayTypes.Charged)
                            {
                                MessageBox.Show(Language.Msg("����Ŀ���ǻ���״̬������ɾ��!"));

                                return;
                            }
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                            int iReturn = this.outpatientManager.DeleteFeeItemListByRecipeNO(feeTemp.RecipeNO, feeTemp.SequenceNO.ToString());
                            if (iReturn <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("ɾ����ϸʧ��!" + this.outpatientManager.Err);
                                return;
                            }
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.Commit();
                            }
                        }
                    }
                }
            }
            this.fpSpread1_Sheet1.Rows.Remove(currRow, 1);
            currRow = this.fpSpread1_Sheet1.ActiveRowIndex;
            //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.InputCode);

            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                this.AddRow(-1);
            }

            SumCost();
        }

        /// <summary>
        /// ɾ��ָ����Ŀ
        /// </summary>
        /// <param name="feeTemp"></param>
        /// <returns></returns>
        public int DeleteRow(FeeItemList feeTemp)
        {
            if (feeTemp.RecipeNO != null && feeTemp.RecipeNO != string.Empty && feeTemp.Order.ID != string.Empty)
            {
                ArrayList alTemp = null;//// this.outpatientManager.GetFeeDetailFromMOOrder(feeTemp.Order.ID);
                if (alTemp != null && alTemp.Count > 0)
                {
                    feeTemp = alTemp[0] as FeeItemList;

                    if (feeTemp.IsAccounted) 
                    {
                        MessageBox.Show(Language.Msg("����Ŀ�Ѿ���ȡ�����˻�,����ɾ��!"));

                        return -1;
                    }

                    if (feeTemp.IsConfirmed)
                    {
                        MessageBox.Show(Language.Msg("����Ŀ�Ѿ����ն�ȷ�ϣ�����ɾ��!"));

                        return -1;
                    }
                    if (feeTemp.PayType != FS.HISFC.Models.Base.PayTypes.Charged)
                    {
                        MessageBox.Show(Language.Msg("����Ŀ���ǻ���״̬������ɾ��!"));

                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    iReturn = this.outpatientManager.DeleteFeeItemListByRecipeNO(feeTemp.RecipeNO, feeTemp.SequenceNO.ToString());
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ɾ����ϸʧ��!" + outpatientManager.Err);

                        return -1;
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
            }

            return iReturn;
        }

        /// <summary>
        /// ֹͣ�༭
        /// </summary>
        public void StopEdit()
        {
            this.fpSpread1.StopCellEditing();
        }

        /// <summary>
        /// ��û�����ϸ
        /// </summary>
        /// <returns>�ɹ� ������ϸ ʧ�� null</returns>
        public ArrayList GetFeeItemListForCharge()
        {
            ArrayList alFeeItemList = new ArrayList();

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag == null || !(this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList))
                {
                    continue;
                }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                if (this.IsCanSelectItemAndFee && this.fpSpread1_Sheet1.Cells[i, (int)Columns.Select].Text.ToLower() == "false")
                {
                    continue;
                }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}����

                alFeeItemList.Add(((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag));
            }

            return alFeeItemList;
        }

        /// <summary>
        /// ��û�����ϸ
        /// </summary>
        /// <returns>�ɹ� ������ϸ ʧ�� null</returns>
        public ArrayList GetFeeItemListForCharge(bool isGroupDetail)
        {
            ArrayList alFeeItemList = new ArrayList();

            //for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            //{
            //    if (this.fpSpread1_Sheet1.Rows[i].Tag == null || !(this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList))
            //    {
            //        continue;
            //    }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            //    if (this.IsCanSelectItemAndFee && this.fpSpread1_Sheet1.Cells[i, (int)Columns.Select].Text.ToLower() == "false")
            //    {
            //        continue;
            //    }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}����

            //    alFeeItemList.Add(((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag));
            //}

            return alFeeItemList;
        }

        /// <summary>
        /// ���һ��С�ƽ��
        /// </summary>
        public void SumLittleCost()
        {
            string tempName = string.Empty; //�жϵ�ǰ�Ƿ���С��
            string tempNameSec = string.Empty;//�ж���һ���Ƿ���С��
            if (this.fpSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }
            int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

            tempName = this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ItemName].Text;
            if (tempName == "С��")
            {
                return;
            }
            if (this.fpSpread1_Sheet1.RowCount > currRow + 1)
            {
                tempNameSec = this.fpSpread1_Sheet1.Cells[currRow + 1, (int)Columns.ItemName].Text;
                if (tempNameSec == "С��")
                {
                    return;
                }
            }
            if (tempName != string.Empty)
            {
                this.fpSpread1_Sheet1.Rows.Add(currRow + 1, 1);
            }
            this.fpSpread1_Sheet1.ActiveRowIndex = currRow + 1;

            decimal sumTotCost = 0;//�ܽ��
            decimal nowCost = 0;//��ǰ���
            int nowCount = 0;

            for (int i = currRow; i >= 0; i--)
            {
                tempName = this.fpSpread1_Sheet1.Cells[i, (int)Columns.ItemName].Text;
                if (tempName == "С��")
                {
                    break;
                }
                nowCost = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Columns.Cost].Text);
                if (nowCost > 0 && nowCount == 0)
                {
                    nowCount = i + 1;
                }
                sumTotCost += nowCost;
            }
            if (sumTotCost > 0)
            {
                nowCount = this.fpSpread1_Sheet1.ActiveRowIndex;
                int rowCount = this.fpSpread1_Sheet1.RowCount;
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.Cells[nowCount, (int)Columns.InputCode].Locked = true;
                this.fpSpread1_Sheet1.Cells[nowCount, (int)Columns.ItemName].Text = "С��";
                this.fpSpread1_Sheet1.Cells[nowCount, (int)Columns.Cost].Text = sumTotCost.ToString();
                if (nowCount + 1 == this.fpSpread1_Sheet1.RowCount)
                {
                    this.AddRow(nowCount);
                }
            }

            this.SumLittleCostAll();
        }

        /// <summary>
        /// ��յ�ǰ�е�����
        /// </summary>
        /// <param name="row">��ǰ��</param>
        public void ClearRow(int row)
        {
            this.fpSpread1_Sheet1.Rows[row].Tag = null;

            for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
            {
                this.fpSpread1_Sheet1.Cells[row, i].Value = null;
            }
        }

        /// <summary>
        /// ����ʾ��Ŀ��Ϣ�ؼ����һ�У�����Ѿ������������һ�еı�������λ��
        /// </summary>
        /// <param name="row">��ǰ��</param>
        public void AddRow(int row)
        {
            if (JudegExeDept() == -1)
            {
                return;
            }
            if (row == this.fpSpread1_Sheet1.RowCount - 1)
            {
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                this.fpSpread1.Focus();

                this.fpSpread1_Sheet1.ActiveRowIndex = this.fpSpread1_Sheet1.RowCount - 1;
                for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
                {
                    //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    if (i != (int)Columns.InputCode && i != (int)Columns.Select)
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, i].Locked = true;
                    }
                }
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.Select].Value = true;
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.InputCode);
            }
            else
            {
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.ActiveRowIndex++;
                if (this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag != null)
                {
                    this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, this.fpSpread1_Sheet1.ActiveColumnIndex);
                }
                else
                {
                    ////{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.Select].Value = true;
                    //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.InputCode);
                }
            }
        }

        /// <summary>
        /// �շѻ򻮼۳�������ý���
        /// </summary>
        public void SetFocus()
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1.Focus();
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.SetActiveCell(0, (int)Columns.InputCode);
            }
        }

        /// <summary>
        /// �շѻ򻮼۳�������ý���
        /// </summary>
        public void SetFocusToInputCode()
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1.Focus();
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.Rows.Count - 1, (int)Columns.InputCode);
            }
        }

        /// <summary>
        /// ��������շ���Ϣ
        /// </summary>
        public void Clear()
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);
            }
            hDays = 1;
            this.fpSpread1_Sheet1.Rows.Add(0, 1);
            //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Select].Value = true;
           
            ////this.tbDrugCost.Text = "0.00";
            ////this.lbItemInfo.Text = string.Empty;
         
            ////this.ucInvoicePreview1.Clear();
            ////this.fpSpread2_Sheet1.RowCount = 0;
            ////this.fpSpread2_Sheet1.RowCount = 2;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                {//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                    if (j == (int)Columns.InputCode|| j == (int)Columns.Select)
                    {
                        this.fpSpread1_Sheet1.Cells[i, j].Locked = false;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, j].Locked = true;
                    }
                }
            }
        }

        /// <summary>
        /// �޸ĸ���
        /// </summary>
        public void ModifyDays()
        {
            bool isHavePCC = false;
            FeeItemList fTemp = null;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        //if (fTemp.Item.IsPharmacy)
                        if (fTemp.Item.ItemType == EnumItemType.Drug)
                        {
                            if (fTemp.Item.SysClass.ID.ToString() == "PCC")
                            {
                                isHavePCC = true;
                            }
                        }
                    }
                }
            }
            if (isHavePCC)
            {
                ucInputDays frmDays = new ucInputDays();
                int index = this.fpSpread1_Sheet1.ActiveRowIndex;
                string existCombNO = string.Empty;
                if (this.fpSpread1_Sheet1.Rows[index].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[index].Tag is FeeItemList)
                    {
                        FeeItemList fTempIndex = this.fpSpread1_Sheet1.Rows[index].Tag as FeeItemList;
                        existCombNO = fTempIndex.Order.Combo.ID;
                    }
                }

                int day = 0;
                string combNo = string.Empty;
                this.Focus();
                if (existCombNO.Length > 0)
                {
                    frmDays.CombNO = existCombNO;
                }
                else
                {
                    frmDays.CombNO = this.GetMaxCombNo();
                }

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(frmDays);

                if (frmDays.IsSelect)
                {
                    day = frmDays.Days;
                    combNo = frmDays.CombNO;
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                        {
                            if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                            {
                                fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                //if (fTemp.Item.IsPharmacy)
                                if (fTemp.Item.ItemType == EnumItemType.Drug)
                                {
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && (fTemp.Order.Combo.ID == existCombNO))
                                    {
                                        fTemp.Days = day;
                                        decimal days = 0;
                                        decimal price = 0;
                                        decimal qty = 0;
                                        decimal totQty = 0;
                                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.Days].Text = day.ToString();

                                        bool bReturn = InputDataIsValid(i, (int)Columns.Days, "����", 9999, 0, ref days);
                                        if (!bReturn)
                                        {
                                            return;
                                        }
                                        fTemp.Days = days;
                                        bReturn = InputDataIsValid(i, (int)Columns.Price, "����", 99999, 0, ref price);
                                        if (!bReturn)
                                        {
                                            return;
                                        }

                                        bReturn = InputDataIsValid(i, (int)Columns.DoseOnce, "ÿ������", 99999, 0, ref qty);
                                        if (!bReturn)
                                        {
                                            return;
                                        }

                                        qty = FS.FrameWork.Public.String.FormatNumber(qty, 2);
                                        // {1FAD3FA2-C7D8-4cac-845F-B9EBECDE2312}
                                        totQty = qty * days / ((fTemp.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose == 0 ? 1 : (fTemp.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose);
                       
                                        //totQty = qty * days;
                                        fTemp.Item.Qty = totQty;
                                        fTemp.Order.Combo.ID = combNo;
                                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.CombNo].Text = fTemp.Order.Combo.ID;

                                        FS.HISFC.Models.Base.FT ft = this.ComputCost(price, totQty, fTemp);

                                        if (ft == null) 
                                        {
                                            return;
                                        }

                                        fTemp.FT.TotCost = ft.TotCost;
                                        fTemp.FT.OwnCost = ft.OwnCost;
                                        fTemp.FT.PayCost = ft.PayCost;
                                        fTemp.FT.PubCost = ft.PubCost;

                                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.Amount].Value = totQty;
                                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.Cost].Value = ft.TotCost;
                                    }
                                }
                            }
                        }
                    }
                    this.SumCost();
                }
            }
        }

        /// <summary>
        /// ���ļ۸�
        /// </summary>
        public void ModifyPrice()
        {
            FeeItemList fTemp = null;
            this.isDealCellChange = false;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                        int age = (int)((new TimeSpan(nowTime.Ticks - this.rInfo.Birthday.Ticks)).TotalDays / 365);

                        fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        
                        DataRow findRow;

                        DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + fTemp.Item.ID + "'");

                        if (rowFinds == null || rowFinds.Length == 0)
                        {
                            MessageBox.Show("����Ϊ: [" + fTemp.Item.ID + " ] ����Ŀ����ʧ��!");
                            this.isDealCellChange = true;

                            return;
                        }
                        findRow = rowFinds[0];

                        //{B9303CFE-755D-4585-B5EE-8C1901F79450}���ӻ�ȡ�����
                        string priceForm = this.rInfo.Pact.PriceForm;
                        decimal unitPrice = NConvert.ToDecimal(findRow["UNIT_PRICE"]);
                        decimal childPrice = NConvert.ToDecimal(findRow["CHILD_PRICE"]);
                        decimal SPPrice = NConvert.ToDecimal(findRow["SP_PRICE"]);
                        decimal purchasePrice = NConvert.ToDecimal(findRow["PUCHASE_PRICE"]);
                        if (unitPrice != 0)
                        {
                            string msgErr = string.Empty;
                            PactItemRate pRate = Class.Function.PactRate(this.rInfo, fTemp, ref msgErr);
                            if (pRate == null)
                            {
                                MessageBox.Show("��ѯ"+fTemp.Item.Name+"���Żݱ�������" + msgErr);
                                return;
                                
                            }
                            //{B9303CFE-755D-4585-B5EE-8C1901F79450}

                            // ����ԭʼĬ�ϼ۸�
                            fTemp.Item.ChildPrice = unitPrice;

                            decimal price = this.feeIntegrate.GetPrice(priceForm, age, unitPrice, childPrice, SPPrice, purchasePrice); 
                            price *= 1 - pRate.Rate.RebateRate;
                            fTemp.Item.Price = price;
                            fTemp.SpecialPrice = fTemp.Item.Price;
                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Price].Tag = fTemp.Item.Price;

                            FS.HISFC.Models.Base.FT ft = this.ComputCost(fTemp.Item.Price, fTemp.Item.Qty, fTemp);

                            if (ft == null) 
                            {
                                this.fpSpread1.Select();
                                this.fpSpread1.Focus();
                                this.fpSpread1_Sheet1.SetActiveCell(i, (int)Columns.Amount, false);

                                return;
                            }

                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Cost].Value = ft.TotCost;
                            fTemp.FT.OwnCost = ft.OwnCost;
                            fTemp.FT.TotCost = ft.TotCost;
                            fTemp.FT.PayCost = ft.PayCost;
                            fTemp.FT.PubCost = ft.PubCost;
                            if (fTemp.FeePack == "1")
                            {
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Price].Text = fTemp.Item.Price.ToString();
                            }
                            else
                            {
                                this.fpSpread1_Sheet1.Cells[i, (int)Columns.Price].Text = FS.FrameWork.Public.String.FormatNumber(fTemp.Item.Price / fTemp.Item.PackQty, 4).ToString();
                            }
                        }
                        
                        if (this.rInfo.Pact.PayKind.ID == "03")
                        {
                            FeeItemList feeTempItem = (FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag;
                            
                            FS.HISFC.Models.Base.PactItemRate pactRate = null;
                                                     
                            if (findRow["ZF"].ToString() == "1")
                            {
                                pactRate = new FS.HISFC.Models.Base.PactItemRate();
                                pactRate.Rate.PayRate = 1;
                            }
                            
                            if (pactRate == null)
                            {
                                pactRate = this.pactUnitItemRateManager.GetOnepPactUnitItemRateByItem(this.rInfo.Pact.ID, fTemp.Item.ID);
                            }
                            if (pactRate != null)
                            {
                                if (feeTempItem.ItemRateFlag != "3")
                                {
                                    if (pactRate.Rate.PayRate != this.rInfo.Pact.Rate.PayRate)
                                    {
                                        if (pactRate.Rate.PayRate == 1)//�Է�
                                        {
                                            feeTempItem.ItemRateFlag = "1";
                                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].Text = "�Է�";
                                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Red;
                                        }
                                        else
                                        {
                                            feeTempItem.ItemRateFlag = "3";
                                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].Text = "����";
                                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Blue;
                                        }
                                    }
                                    else
                                    {
                                        feeTempItem.ItemRateFlag = "2";
                                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].Text = "����";
                                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Black;
                                    }
                                    feeTempItem.OrgItemRate = this.rInfo.Pact.Rate.PayRate;
                                    feeTempItem.NewItemRate = pactRate.Rate.PayRate;
                                }
                                else
                                {
                                    feeTempItem.ItemRateFlag = "3";
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].Text = "����";
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Blue;
                                }
                            }
                            else
                            {
                                if (feeTempItem.ItemRateFlag != "3")
                                {
                                    feeTempItem.OrgItemRate = this.rInfo.Pact.Rate.PayRate;
                                    feeTempItem.NewItemRate = this.rInfo.Pact.Rate.PayRate;
                                    feeTempItem.ItemRateFlag = "2";
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].Text = "����";
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Black;
                                }
                                else
                                {
                                    feeTempItem.ItemRateFlag = "3";
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].Text = "����";
                                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Blue;
                                }
                            }
                        }
                    }
                }
            }
            this.SumCost();
            this.isDealCellChange = true;
        }

        /// <summary>
        /// ����ˢ����ʾ����
        /// </summary>
        public void RefreshNewRate()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null && this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    string tmpFlag = null;
                    switch (((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag)
                    {
                        case "1":
                            tmpFlag = "�Է�";
                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Red;
                            break;
                        case "2":
                            tmpFlag = "����";
                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Black;
                            break;
                        case "3":
                            tmpFlag = "����";
                            this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].ForeColor = Color.Blue;
                            break;
                    }
                    this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].Text = tmpFlag;
                }             
            }
        }

        /// <summary>
        /// �����޸ı�����ķ���.
        /// </summary>
        /// <param name="feeDetails"></param>
        public void RefreshNewRate(ArrayList feeDetails)
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null && this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    if (((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).Item.ID == ((FeeItemList)feeDetails[i]).Item.ID)
                    {
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).NewItemRate = ((FeeItemList)feeDetails[i]).NewItemRate;
                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = ((FeeItemList)feeDetails[i]).ItemRateFlag;
                        string tmpFlag = null;
                        switch (((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag)
                        {
                            case "1":
                                tmpFlag = "�Է�";
                                break;
                            case "2":
                                tmpFlag = "����";
                                break;
                            case "3":
                                tmpFlag = "����";
                                break;
                        }
                        this.fpSpread1_Sheet1.Cells[i, (int)Columns.Self].Text = tmpFlag;
                    }
                }
            }

            SumCost();
        }

        /// <summary>
        /// ����շ���Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList GetFeeItemList()
        {
            ArrayList feeItemLists = new ArrayList();

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag == null || !(this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList))
                {
                    continue;
                }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                if (this.IsCanSelectItemAndFee && this.fpSpread1_Sheet1.Cells[i, (int)Columns.Select].Text.ToLower() == "false") 
                {
                    continue;
                }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}����
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    if (((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).IsGroup)
                    {
                        ArrayList alDetail = ConvertGroupToDetail(((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag));
                        if (alDetail == null)
                        {
                            errText = "���������ϸ����!";
                            return null;
                        }

                        if (alDetail.Count == 0) 
                        {
                            MessageBox.Show(((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).Item.Name + "�������Ŀ,����û��ά����ϸ������ϸ�Ѿ�ͣ�ã�������Ϣ����ϵ��");
                        }

                        feeItemLists.AddRange(alDetail);
                    }
                    else
                    {
                        feeItemLists.Add(((FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag));
                    }
                }
            }

            return feeItemLists;
        }

        /// <summary>
        /// ˢ�¿���ҽ��
        /// </summary>
        /// <param name="recipeSeq">��ǰ�շ�����</param>
        /// <param name="deptCode">�����Ŀ������</param>
        /// <param name="obj">������ҽ������</param>
        public void RefreshSeeDoc(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject obj)
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag == null || !(this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList))
                {
                    continue;
                }
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList tempFeeItemList = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                    if (tempFeeItemList.RecipeSequence == recipeSeq)
                    {
                        tempFeeItemList.RecipeOper.Dept.ID = deptCode;
                        tempFeeItemList.RecipeOper.ID = obj.ID;
                        tempFeeItemList.RecipeOper.Name = obj.Name;
                    }
                }
            }
        }

        /// <summary>
        /// ˢ�¿������
        /// </summary>
        /// <param name="recipeSeq">�շ�����</param>
        /// <param name="obj">�޸ĺ�Ŀ�����Ϣ</param>
        public void RefreshSeeDept(string recipeSeq, FS.FrameWork.Models.NeuObject obj)
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag == null || !(this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList))
                {
                    continue;
                }
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList tempFeeItemList = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                    if (tempFeeItemList.RecipeSequence == recipeSeq)
                    {
                        ((Register)tempFeeItemList.Patient).DoctorInfo.Templet.Dept = obj.Clone();
                    }
                }
            }
        }

        /// <summary>
        /// ��ӹҺŷ�
        /// </summary>
        public void AddRegFee()
        {
            if (this.rInfo == null || this.tempDept == null)
            {
                return;
            }
            //�������ֱ���շѻ���,�����ӹҺŷ�
            if (this.rInfo.PID.CardNO.Substring(0, 1) != this.noRegFlagChar)
            {
                return;
            }

            //����Һŷ�����Ŀû��ά��,��ô��ֹ
            if (this.regFeeItemCode == string.Empty) 
            {
                return;
            }

            //�ж���������˹Һŷ���Ŀ,��ô����������
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                    {
                        if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                        {
                            FeeItemList fSame = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                            if (fSame.Item.ID == this.regFeeItemCode)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            //����շ���Ŀ�в�����ά���ĹҺŷ���Ŀ,��ô����
            DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + this.regFeeItemCode + "'");

            if (rowFinds == null || rowFinds.Length == 0)
            {
                return;
            }
            //���
            this.fpSpread1_Sheet1.Rows.Add(0, 1);
            this.alAddRows.Clear();
            this.SetItem(this.regFeeItemCode, "0", this.rInfo.DoctorInfo.Templet.Dept.ID, 0, 1, 0);

            RegLvlFee tempRegFeeOfPact = this.registerIntegrate.GetRegLevelByPactCode(this.rInfo.Pact.ID, this.comRegLevel);
            if (tempRegFeeOfPact == null)
            {
                return;
            }
            if (tempRegFeeOfPact.RegFee <= 0)
            {
                return;
            }
          
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Price].Locked = false;
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.ExeDept].Locked = false;
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Amount].Locked = false;
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.CombNo].Locked = false;
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Price].Text = tempRegFeeOfPact.RegFee.ToString();

            FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[0].Tag as FeeItemList;

            fTemp.Item.Price = tempRegFeeOfPact.RegFee;

            FS.HISFC.Models.Base.FT ft = this.ComputCost(fTemp.Item.Price, fTemp.Item.Qty, fTemp);

            if (ft == null)
            {
                this.fpSpread1.Select();
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.SetActiveCell(0, (int)Columns.Amount, false);

                return;
            }

            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Cost].Value = ft.TotCost;
            fTemp.FT.OwnCost = ft.OwnCost;
            fTemp.FT.TotCost = ft.TotCost;
            fTemp.FT.PayCost = ft.PayCost;
            fTemp.FT.PubCost = ft.PubCost;

            this.fpSpread1_Sheet1.SetActiveCell(0, (int)Columns.Price, false);
        }

        /// <summary>
        /// ����Է����
        /// </summary>
        public void AddOwnDiagFee()
        {
            if (this.rInfo == null || this.tempDept == null)
            {
                return;
            }

            RegLvlFee tempRegFeeOfPact = this.registerIntegrate.GetRegLevelByPactCode(this.rInfo.Pact.ID, this.comRegLevel);
            if (tempRegFeeOfPact == null)
            {
                return;
            }
            if (tempRegFeeOfPact.OwnDigFee <= 0)
            {
                return;
            }

            if (this.ownDiagFeeCode == null || this.ownDiagFeeCode == "��" || this.ownDiagFeeCode == string.Empty || this.ownDiagFeeCode == "-1")
            {
                return;
            }

            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                    {
                        if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                        {
                            FeeItemList fSame = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                            if (fSame.Item.ID == this.ownDiagFeeCode && fSame.NewItemRate == 1)
                            {
                                return;
                            }
                        }
                    }
                }
            }
            if (this.rInfo.PID.CardNO.Substring(0, 1) != this.noRegFlagChar)
            {
                return;
            }

            DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + this.ownDiagFeeCode + "'");
            if (rowFinds == null || rowFinds.Length == 0)
            {
                return;
            }

            this.fpSpread1_Sheet1.Rows.Add(0, 1);
            //���
            this.alAddRows.Clear();
            //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
            this.SetItem(this.ownDiagFeeCode, "0", this.rInfo.DoctorInfo.Templet.Dept.ID, 0, 1, 0);
         
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Price].Locked = false;
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.ExeDept].Locked = false;
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Amount].Locked = false;
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.CombNo].Locked = false;
            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Price].Text = tempRegFeeOfPact.OwnDigFee.ToString();
            this.fpSpread1_Sheet1.SetValue(0, (int)Columns.Price, tempRegFeeOfPact.OwnDigFee.ToString());

            FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[0].Tag as FeeItemList;

            fTemp.Item.Price = tempRegFeeOfPact.OwnDigFee;

            FS.HISFC.Models.Base.FT ft = this.ComputCost(fTemp.Item.Price, fTemp.Item.Qty, fTemp);

            if (ft == null)
            {
                this.fpSpread1.Select();
                this.fpSpread1.Focus();
                this.fpSpread1_Sheet1.SetActiveCell(0, (int)Columns.Amount, false);

                return;
            }

            this.fpSpread1_Sheet1.Cells[0, (int)Columns.Cost].Value = ft.TotCost;
            fTemp.FT.OwnCost = ft.OwnCost;
            fTemp.FT.TotCost = ft.TotCost;
            fTemp.FT.PayCost = ft.PayCost;
            fTemp.FT.PubCost = ft.PubCost;
            fTemp.ItemRateFlag = "1";
            fTemp.OrgItemRate = this.rInfo.Pact.Rate.PayRate;
            fTemp.NewItemRate = 1;
            this.fpSpread1_Sheet1.SetActiveCell(0, (int)Columns.Price, false);

            this.SumCost();
        }

        #endregion
        
        #endregion

        #region �¼�

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Left)
                {
                    PutArrow(Keys.Left);
                }
                if (keyData == Keys.Right)
                {
                    PutArrow(Keys.Right);
                }

                if (this.fpSpread1.ContainsFocus)
                {
                    if (keyData == Keys.Escape)
                    {
                        if (lbDept.Visible)
                        {
                            lbDept.Visible = false;
                        }
                        if (lbFreq.Visible)
                        {
                            lbFreq.Visible = false;
                        }
                        if (lbUsage.Visible)
                        {
                            lbUsage.Visible = false;
                        }
                        if (this.chooseItemControl.ChooseItemType == FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemChanging && ((Control)this.chooseItemControl).Visible) 
                        {
                            ((Control)this.chooseItemControl).Visible = false;
                        }
                    }

                    if (keyData == Keys.Down)
                    {
                        if (lbDept.Visible)
                        {
                            lbDept.NextRow();
                        }
                        else if (lbFreq.Visible)
                        {
                            lbFreq.NextRow();
                        }
                        else if (lbUsage.Visible)
                        {
                            lbUsage.NextRow();
                        }
                        else if (((Control)this.chooseItemControl).Visible && this.chooseItemControl.ChooseItemType == FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemChanging)
                        {
                            this.chooseItemControl.NextRow();
                        }
                        else
                        {
                            string temp = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Columns.ItemName].Text;
                            if (temp != string.Empty)
                            {
                                AddRow(this.fpSpread1_Sheet1.ActiveRowIndex);
                            }
                            RefreshItemInfo();
                        }
                    }
                    if (keyData == Keys.Up)
                    {
                        if (lbDept.Visible)
                        {
                            lbDept.PriorRow();
                        }
                        else if (lbFreq.Visible)
                        {
                            lbFreq.PriorRow();
                        }
                        else if (lbUsage.Visible)
                        {
                            lbUsage.PriorRow();
                        }
                        else if (((Control)this.chooseItemControl).Visible && this.chooseItemControl.ChooseItemType == FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemChanging) 
                        {
                            this.chooseItemControl.PriorRow();
                        }
                        else
                        {
                            int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;
                            if (currRow > 0)
                            {
                                this.fpSpread1_Sheet1.ActiveRowIndex = currRow - 1;
                                this.fpSpread1_Sheet1.SetActiveCell(currRow - 1, this.fpSpread1_Sheet1.ActiveColumnIndex);
                            }
                            RefreshItemInfo();
                            //this.fpSpread1.StopCellEditing();
                        }
                    }

                    #region �ȼ�
                    if (keyData.GetHashCode() == Keys.Control.GetHashCode() + Keys.I.GetHashCode())
                    {
                        int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;
                        this.fpSpread1_Sheet1.Rows.Add(currRow + 1, 1);
                        this.fpSpread1_Sheet1.SetActiveCell(currRow + 1, 0);

                        SumLittleCostAll();
                    }
                    if (keyData.GetHashCode() == Keys.Control.GetHashCode() + Keys.E.GetHashCode())
                    {
                        this.ModifyDays();
                    }

                    #endregion

                    if (keyData == Keys.Enter)
                    {
                        int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;
                        int currColumn = this.fpSpread1_Sheet1.ActiveColumnIndex;

                        this.isDealCellChange = false;

                        this.fpSpread1.StopCellEditing();
                        FeeItemList feeItem = null;//��ǰ��Ŀ��Ϣ
                        //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                        if (currColumn != (int)Columns.InputCode && currColumn != (int)Columns.Select)
                        {
                            if (!IsInputItem(currRow, ref feeItem))
                            {
                                this.isDealCellChange = true;
                                
                                return false;
                            }
                        }
                        #region ������

                        //�����ǰ������Ŀ���룬�������Ŀ
                        //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                        if (currColumn == (int)Columns.InputCode)
                        {
                            if (this.rInfo == null) 
                            {
                                MessageBox.Show(Language.Msg("��ѡ����"));

                                this.isDealCellChange = true;

                                return false;
                            }
                            
                            if (this.rInfo.DoctorInfo.Templet.Dept.ID == null || this.rInfo.DoctorInfo.Templet.Dept.ID == string.Empty)
                            {
                                MessageBox.Show(Language.Msg("��ѡ�������!"));

                                this.isDealCellChange = true;

                                return false;
                            }
                            if (this.fpSpread1_Sheet1.Rows[currRow].Tag != null)
                            {
                                if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
                                {
                                    feeItem = (FeeItemList)fpSpread1_Sheet1.Rows[currRow].Tag;
                                }
                            }
                            if (!this.isCanModifyCharge)
                            {
                                if (feeItem != null)
                                {
                                    if (feeItem.Order.ID != null && feeItem.Order.ID != string.Empty)
                                    {
                                        this.isDealCellChange = true;
                                        
                                        return false;
                                    }
                                }
                            }
                            if (!this.isCanAddItem && !this.isQuitFee)
                            {
                                MessageBox.Show(Language.Msg("�뵥ѡ��һ����������������Ŀ!"));
                                this.fpSpread1.Focus();
                                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.InputCode, false);

                                this.isDealCellChange = true;

                                return false;
                            }
                            if (feeItem != null)
                            {//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                                string sTempText = this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.InputCode].Text;
                                if (sTempText == feeItem.Item.UserCode)
                                {
                                    //if (feeItem.Item.IsPharmacy && feeItem.Item.SysClass.ID.ToString() == "PCC")//��ҩ
                                    if (feeItem.Item.ItemType == EnumItemType.Drug && feeItem.Item.SysClass.ID.ToString() == "PCC")//��ҩ
                                    {
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.DoseOnce, false);
                                    }
                                    else
                                    {
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Amount, false);
                                    }
                                }
                                else
                                {
                                    if (feeItem.PayType == FS.HISFC.Models.Base.PayTypes.Charged)
                                    {
                                        if (DeleteRow(feeItem) == -1)
                                        {
                                            return false;
                                        }
                                    }

                                    if (this.chooseItemControl.ChooseItemType == FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemInputEnd)
                                    {  //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                                        QueryItem(this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.InputCode].Text, currRow);
                                    }
                                    else 
                                    {
                                        this.chooseItemControl.GetSelectedItem();
                                    }
                                }
                            }
                            else
                            {
                                if (this.chooseItemControl.ChooseItemType == FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemInputEnd)
                                {//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                                    QueryItem(this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.InputCode].Text, currRow);
                                }
                                else
                                {//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                                    //if (this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.InputCode].Text.Trim() == string.Empty) 
                                    //{
                                    //    return false;
                                    //}
                                    
                                    this.chooseItemControl.GetSelectedItem();
                                }
                            }
                        }

                        #endregion

                        #region ����

                        //����
                        if (currColumn == (int)Columns.Amount)
                        {
                            decimal price = 0;
                            try
                            {
                                price = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Price].Text);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("�۸��������!" + ex.Message);
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Amount, false);

                                this.isDealCellChange = true;

                                return false;
                            }
                            decimal qty = 0;
                            if (price == 0)//��Ŀû�м۸�ֱ����ת������۸��λ��
                            {
                                //if (feeItem.Item.IsPharmacy)
                                if (feeItem.Item.ItemType == EnumItemType.Drug)
                                {
                                    FarPoint.Win.Spread.CellType.ComboBoxCellType type =
                                        (FarPoint.Win.Spread.CellType.ComboBoxCellType)this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.PriceUnit].CellType;
                                    type.ListControl.SelectedIndex = 0;
                                }
                                bool bReturn = InputDataIsValid(currRow, (int)Columns.Amount, "����", 99999, 0, ref qty);
                                if (!bReturn)
                                {
                                    this.isDealCellChange = true;
                                    
                                    return false;
                                }

                                #region �ж��Ƿ���ȡ��

                                //if (this.isQtyToCeiling && feeItem.Item.IsPharmacy)
                                if (this.isQtyToCeiling && feeItem.Item.ItemType == EnumItemType.Drug)
                                {
                                    double qtyValue = System.Convert.ToDouble(qty);

                                    qtyValue = System.Math.Ceiling(qtyValue);

                                    qty = NConvert.ToDecimal(qtyValue);
                                }

                                #endregion

                                qty = FS.FrameWork.Public.String.FormatNumber(qty, 2);
                                if (feeItem.FeePack == "1")//��װ��λ
                                {
                                    feeItem.Item.Qty = qty * feeItem.Item.PackQty;
                                }
                                else
                                {
                                    feeItem.Item.Qty = qty;
                                }
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Amount].Text = qty.ToString();
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Price].Locked = false;
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Price);
                            }
                            else
                            {
                                bool bReturn = InputDataIsValid(currRow, (int)Columns.Amount, "����", 99999, 0, ref qty);
                                if (!bReturn)
                                {
                                    this.isDealCellChange = true;
                                    
                                    return false;
                                }

                                #region �ж��Ƿ���ȡ��

                                //if (this.isQtyToCeiling && feeItem.Item.IsPharmacy)
                                if (this.isQtyToCeiling && feeItem.Item.ItemType == EnumItemType.Drug)
                                {
                                    double qtyValue = System.Convert.ToDouble(qty);

                                    qtyValue = System.Math.Ceiling(qtyValue);

                                    qty = NConvert.ToDecimal(qtyValue);
                                }

                                #endregion

                                qty = FS.FrameWork.Public.String.FormatNumber(qty, 2);
                                if (feeItem.FeePack == "1")//��װ��λ
                                {
                                    feeItem.Item.Qty = qty * feeItem.Item.PackQty;
                                }
                                else
                                {
                                    feeItem.Item.Qty = qty;
                                }
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Amount].Text = qty.ToString();

                                FS.HISFC.Models.Base.FT ft = this.ComputCost(price, qty, feeItem);

                                if (ft == null)
                                {
                                    this.fpSpread1.Select();
                                    this.fpSpread1.Focus();
                                    this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Amount, false);

                                    this.isDealCellChange = true;

                                    return false;
                                }

                                feeItem.FT.TotCost = ft.TotCost;
                                feeItem.FT.OwnCost = ft.OwnCost;
                                feeItem.FT.PayCost = ft.PayCost;
                                feeItem.FT.PubCost = ft.PubCost;

                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Cost].Value = ft.TotCost;

                                //if (feeItem.Item.IsPharmacy)
                                if (feeItem.Item.ItemType == EnumItemType.Drug)
                                {
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.DoseOnce].Locked = false;
                                    if (feeItem.Invoice.User01 == "1")//�����Բ�ְ�װ��λ
                                    {
                                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.PriceUnit].Locked = true;
                                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.DoseOnce].Locked = false;
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.DoseOnce);
                                    }
                                    else
                                    {
                                        this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.PriceUnit].Locked = false;
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.DoseOnce, false);
                                    }
                                    //begin�����жϿ����� zhouxs by 2007-10-17
                                    if (!IsStoreEnough(feeItem, currRow))
                                    {
                                        return false;
                                    }
                                    //end zhouxs
                                }
                                else//��ҩƷ
                                {
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ExeDept].Locked = false;
                                    if (feeItem.Item.SysClass.ID.ToString() == "UL")
                                    {
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.CombNo, false);
                                    }
                                    else
                                    {
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.ExeDept, false);
                                    }
                                }
                            }

                         
                            this.SumCost();
                        }
                        #endregion

                        #region ����
                        if (currColumn == (int)Columns.Days)
                        {
                            decimal qty = 0; //����
                            decimal days = 0; //����
                            decimal price = 0; //����
                            decimal totQty = 0; //������(���㸶����)

                            //if (feeItem.Item.IsPharmacy)
                            if (feeItem.Item.ItemType == EnumItemType.Drug)
                            {
                                //��ҩ
                                if (feeItem.Item.SysClass.ID.ToString() == "PCC")
                                {

                                    bool bReturn = InputDataIsValid(currRow, (int)Columns.Days, "����", 9999, 0, ref days);
                                    if (!bReturn)
                                    {
                                        this.isDealCellChange = true;
                                        
                                        return false;
                                    }

                                    feeItem.Days = days;
                                    if (days != this.hDays)
                                    {
                                        hDays = days;
                                    }

                                    bReturn = InputDataIsValid(currRow, (int)Columns.Price, "����", 99999, 0, ref price);
                                    if (!bReturn)
                                    {
                                        this.isDealCellChange = true;
                                        
                                        return false;
                                    }

                                    bReturn = InputDataIsValid(currRow, (int)Columns.DoseOnce, "ÿ������", 99999, 0, ref qty);
                                    if (!bReturn)
                                    {
                                        this.isDealCellChange = true;
                                        
                                        return false;
                                    }

                                    qty = FS.FrameWork.Public.String.FormatNumber(qty, 2);
                                    // {1FAD3FA2-C7D8-4cac-845F-B9EBECDE2312}
                                    totQty = qty * days / ((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose == 0 ? 1 : (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose);
                                   // totQty = qty * days;
                                    feeItem.Item.Qty = totQty;
                                    //{73AA7783-8B97-45f5-B430-0C7311E952C8}  
                                    this.SumCost();
                                    this.isDealCellChange = true;
                                    FS.HISFC.Models.Base.FT ft = this.ComputCost(price, totQty, feeItem);

                                    if (ft == null)
                                    {
                                        this.fpSpread1.Select();
                                        this.fpSpread1.Focus();
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Amount, false);

                                        this.isDealCellChange = true;
                                        
                                        return false;
                                    }

                                    feeItem.FT.TotCost = ft.TotCost;
                                    feeItem.FT.OwnCost = ft.OwnCost;
                                    feeItem.FT.PayCost = ft.PayCost;
                                    feeItem.FT.PubCost = ft.PubCost;


                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Amount].Value = totQty;
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Cost].Value = ft.TotCost;
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.CombNo].Locked = false;
                                    this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.CombNo, false);
                                }
                            }
                        }
                        #endregion

                        #region ÿ������
                        if (currColumn == (int)Columns.DoseOnce)
                        {
                            //if (feeItem.Item.IsPharmacy)
                            if (feeItem.Item.ItemType == EnumItemType.Drug)
                            {
                                decimal doseOnce = 0;

                                if (!this.isDoseOnceNull)//ÿ����������Ϊ��
                                {
                                    bool bReturn = InputDataIsValid(currRow, (int)Columns.DoseOnce, "ÿ������", 99999, 0, ref doseOnce);
                                    if (!bReturn)
                                    {
                                        this.isDealCellChange = true;
                                        
                                        return false;
                                    }
                                }
                                else
                                {
                                    InputDataIsValid(currRow, (int)Columns.DoseOnce, "ÿ������", 99999, 0, ref doseOnce, false);
                                }
                                if (feeItem.Item.SysClass.ID.ToString() == "PCC")
                                {
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Days].Locked = false;
                                    this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Days, false);

                                    this.isDealCellChange = true;

                                    #region {46DA2449-F37C-45bf-B39F-8B8EEF5A6F00} ��ʵ��д��ÿ������
                                    feeItem.Order.DoseOnce = doseOnce;
                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.DoseOnce].Value = feeItem.Order.DoseOnce;
                                    #endregion

                                    return false;
                                }

                                feeItem.Order.DoseOnce = doseOnce;
                                
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.DoseOnce].Value = feeItem.Order.DoseOnce;
                            }

                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.CombNo].Locked = false;
                            this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.CombNo, false);
                        }
                        #endregion

                        #region ��Ϻ�
                        if (currColumn == (int)Columns.CombNo)
                        {
                            string strCombNo = this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.CombNo].Text;
                            if (strCombNo.Length > 14)
                            {
                                MessageBox.Show("��Ϻ����벻�ܳ���14λ!");
                                this.fpSpread1.Focus();
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.CombNo);

                                this.isDealCellChange = true;

                                return false;
                            }
                            feeItem.Order.Combo.ID = strCombNo;
                           
                            this.SumCost();
                            //if (feeItem.Item.IsPharmacy)
                            if (feeItem.Item.ItemType == EnumItemType.Drug)
                            {
                                if (currRow > 0)
                                {
                                    #region ��õ�һ���͵�ǰ�о�����ͬ��Ϻŵ��к�
                                    
                                    int combNoIndex = -1;
                                    for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                                    {
                                        if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                        {
                                            if (i == currRow)
                                            {
                                                continue;
                                            }

                                            FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                            //if (fTemp.Item.IsPharmacy)
                                            if (fTemp.Item.ItemType == EnumItemType.Drug)
                                            {
                                                if (feeItem.Order.Combo.ID == fTemp.Order.Combo.ID && feeItem.Order.Combo.ID != string.Empty)
                                                {
                                                    combNoIndex = i;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    #endregion
                                    if (combNoIndex != -1)
                                    {
                                        FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[combNoIndex].Tag as FeeItemList;
                                        //if (fTemp.Item.IsPharmacy)
                                        if (fTemp.Item.ItemType == EnumItemType.Drug)
                                        {
                                            if (feeItem.Order.Combo.ID == fTemp.Order.Combo.ID && feeItem.Order.Combo.ID != string.Empty)
                                            {
                                                feeItem.Order.Frequency.ID = fTemp.Order.Frequency.ID;
                                                feeItem.Order.Frequency.Name = fTemp.Order.Frequency.Name;
                                                feeItem.Order.Usage.ID = fTemp.Order.Usage.ID;
                                                feeItem.Order.Usage.Name = fTemp.Order.Usage.Name;
                                                if (freqDisplayType == "0")//����
                                                {
                                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text = feeItem.Order.Frequency.Name;
                                                }
                                                else
                                                {
                                                    this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text = feeItem.Order.Frequency.ID;
                                                }

                                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Usage].Text = feeItem.Order.Usage.Name;
                                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Locked = false;
                                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Usage].Locked = false;
                                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ExeDept].Locked = false;
                                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.ExeDept, false);

                                                this.isDealCellChange = true;

                                                return true;
                                            }
                                        }
                                    }
                                }
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Locked = false;
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Freq, false);
                            }
                            else
                            {
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ExeDept].Locked = false;
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.ExeDept, false);
                            }

                        }

                        #endregion

                        #region Ƶ��
                        if (currColumn == (int)Columns.Freq)
                        {
                            if (this.ProcessFreq() == -1)
                            {
                                try
                                {
                                    //if (feeItem.Item.IsPharmacy)
                                    if (feeItem.Item.ItemType == EnumItemType.Drug)
                                    {
                                        //ȥ����Ƶ�ηǿյ��жϡ�2007-8-24 luzhp@FS.com
                                        if (!this.isDoseOnceNull)
                                        {
                                            if (this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text == string.Empty)
                                            {
                                                MessageBox.Show("������ҩƷ��Ƶ��!");
                                                this.fpSpread1.Focus();
                                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Freq);

                                                this.isDealCellChange = true;

                                                return false;
                                            }
                                        }
                                        
                                        if (this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text != string.Empty)
                                        {
                                            if (freqDisplayType == "0")//����
                                            {
                                                feeItem.Order.Frequency.ID =
                                                    myHelpFreq.GetID(this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text);
                                            }
                                            else
                                            {
                                                string tmpName = myHelpFreq.GetName(this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text);
                                                if (tmpName == null || tmpName == string.Empty)
                                                {
                                                    MessageBox.Show("Ƶ�δ����������!");
                                                    this.fpSpread1.Focus();
                                                    this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Freq);

                                                    this.isDealCellChange = true;

                                                    return false;
                                                }
                                                feeItem.Order.Frequency.ID = this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Freq].Text;
                                            }
                                            if (feeItem.Order.Frequency.ID == null || feeItem.Order.Frequency.ID == string.Empty)
                                            {
                                                MessageBox.Show("Ƶ�δ����������!");
                                                this.fpSpread1.Focus();
                                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Freq);

                                                this.isDealCellChange = true;

                                                return false;
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);

                                    this.isDealCellChange = true;

                                    return false;
                                }
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Usage].Locked = false;
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Usage, false);
                            }
                            this.DealFreqOrUsageHaveSameCombNo(currRow, feeItem.Order.Combo.ID, feeItem.Order.Frequency, "1");
                        }
                        #endregion

                        #region �÷�
                        if (currColumn == (int)Columns.Usage)
                        {
                            if (this.ProcessUsage() == -1)
                            {
                                try
                                {
                                    //if (feeItem.Item.IsPharmacy)
                                    if (feeItem.Item.ItemType == EnumItemType.Drug)
                                    {
                                        // ͨ�������������ж��÷��Ƿ����Ϊ�ա�2007-8-24 ·־��
                                        if (!this.isDoseOnceNull)
                                        {
                                            if (this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Usage].Text == string.Empty)
                                            {
                                                MessageBox.Show("������ҩƷ���÷�!");
                                                this.fpSpread1.Focus();
                                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Usage);

                                                this.isDealCellChange = true;

                                                return false;
                                            }
                                        }
                                        if (this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Usage].Text != string.Empty)
                                        {
                                            feeItem.Order.Usage.ID = myHelpUsage.GetID(this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Usage].Text);
                                            if (feeItem.Order.Usage.ID == null || feeItem.Order.Usage.ID == string.Empty)
                                            {
                                                MessageBox.Show("ҩƷ���÷����벻��ȷ");
                                                this.fpSpread1.Focus();
                                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Usage);

                                                this.isDealCellChange = true;

                                                return false;
                                            }

                                            alInjec = this.outpatientManager.GetInjectInfoByUsage(feeItem.Order.Usage.ID);
                                            if (alInjec == null)
                                            {
                                                MessageBox.Show("���Ժע��Ŀ����!");

                                                this.isDealCellChange = true;

                                                return false;
                                            }
                                            if (alInjec.Count > 0)
                                            {
                                                FS.FrameWork.WinForms.Classes.Function.PopShowControl(myInjec);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);

                                    this.isDealCellChange = true;

                                    return false;
                                }

                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ExeDept].Locked = false;
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.ExeDept, false);
                            }

                            this.DealFreqOrUsageHaveSameCombNo(currRow, feeItem.Order.Combo.ID, feeItem.Order.Usage, "2");
                        }

                        #endregion

                        #region ִ�п���
                        if (currColumn == (int)Columns.ExeDept)
                        {
                            if (ProcessDept() == -1)
                            {
                                this.isDealCellChange = true;
                                
                                return false;
                            }

                            if (injec > 0)
                            {

                                int actIndex = this.fpSpread1_Sheet1.RowCount - 1;
                                //int tmpRow = currRow;
                                foreach (NeuObject obj in alInjec)
                                {
                                    DataRow rowFind;
                                    DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + obj.ID + "'");

                                    if (rowFinds == null || rowFinds.Length == 0)
                                    {
                                        MessageBox.Show("����Ժע��Ŀ����!");

                                        this.isDealCellChange = true;

                                        return false;
                                    }
                                    rowFind = rowFinds[0];
                                    try
                                    {
                                        feeItem.InjectCount = NConvert.ToInt32(injec);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Ժע�������벻�Ϸ�!" + ex.Message);
                                        this.fpSpread1.Focus();
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Usage, false);

                                        this.isDealCellChange = true;

                                        return false;
                                    }
                                    if (feeItem.InjectCount > 99)
                                    {
                                        MessageBox.Show("Ժ��ע��������ܴ���99!");
                                        this.fpSpread1.Focus();
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Usage, false);

                                        this.isDealCellChange = true;

                                        return false;

                                    }
                                    if (feeItem.Order.Combo.ID != null && feeItem.Order.Combo.ID != string.Empty)
                                    {
                                        RefreshSameCombNoInjects(feeItem.Order.Combo.ID, feeItem.InjectCount);
                                    }

                                    actIndex = GetNewRow();
                                    if (actIndex == -1)
                                    {
                                        this.fpSpread1.StopCellEditing();
                                        this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                                        actIndex = this.fpSpread1_Sheet1.RowCount - 1;
                                        //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Select].Value = true;
                                    }

                                    //���
                                    this.alAddRows.Clear();
                                    string drugflag = "0";
                                    if (obj.ID.Substring(0, 1) != "F")
                                    {
                                        drugflag = "2";
                                    }
                                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                                    SetItem(rowFind["ITEM_CODE"].ToString(), drugflag, rowFind["EXE_DEPT"].ToString(), actIndex, 1, NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Price].Text));
                                    this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Amount].Text = injec.ToString();
                                    ((FeeItemList)this.fpSpread1_Sheet1.Rows[actIndex].Tag).Item.Qty = injec;
                                    ((FeeItemList)this.fpSpread1_Sheet1.Rows[actIndex].Tag).Item.IsMaterial = true;
                                    //if (((FeeItemList)this.fpSpread1_Sheet1.Rows[actIndex].Tag).Item.IsPharmacy)
                                    if (((FeeItemList)this.fpSpread1_Sheet1.Rows[actIndex].Tag).Item.ItemType == EnumItemType.Drug)
                                    {
                                        if (((FeeItemList)this.fpSpread1_Sheet1.Rows[actIndex].Tag).Item.SysClass.ID.ToString() == "PCC")
                                        {
                                            this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Days].Locked = false;
                                        }
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Amount].Locked = false;
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.DoseOnce].Locked = false;
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Freq].Locked = false;
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Usage].Locked = false;
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.ExeDept].Locked = false;
                                    }
                                    else
                                    {
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Amount].Locked = false;
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.ExeDept].Locked = false;
                                    }


                                    decimal price = 0;
                                    try
                                    {
                                        price = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Price].Text);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("�۸����벻�Ϸ�" + ex.Message);
                                        this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Days, false);

                                        this.isDealCellChange = true;

                                        return false;
                                    }

                                    decimal qty = 0;
                                    decimal cost = 0;
                                    if (price == 0)//��Ŀû�м۸�ֱ����ת������۸��λ��
                                    {
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Price].Locked = false;
                                        this.fpSpread1_Sheet1.SetActiveCell(actIndex, (int)Columns.Price);
                                    }
                                    else
                                    {
                                        qty = injec;
                                        cost = FS.FrameWork.Public.String.FormatNumber(price * qty, 2);
                                        ((FeeItemList)this.fpSpread1_Sheet1.Rows[actIndex].Tag).FT.TotCost = cost;
                                        this.fpSpread1_Sheet1.Cells[actIndex, (int)Columns.Cost].Value = cost;
                                    }

                                }

                                ((FeeItemList)this.fpSpread1_Sheet1.Rows[actIndex].Tag).InjectCount = (int)injec;
                            }
                            if (injec == 0)
                            {
                                AddRow(currRow);
                            }
                            else
                            {
                                AddRow(this.fpSpread1_Sheet1.RowCount - 1);
                            }
                            injec = 0;
                            alInjec = new ArrayList();

                            //ArrayList alFee = this.GetFeeItemListForCharge();
                            //this.FeeItemListChanged(alFee);
                            this.SumCost();

                            this.isDealCellChange = true;

                            return true;
                        }
                        #endregion

                        #region ����

                        //û�м۸����Ŀ����۸�󣬼��㵱ǰ����Ŀ���
                        if (currColumn == (int)Columns.Price)
                        {

                            decimal price = 0;
                            decimal qty = 0;

                            bool bReturn = InputDataIsValid(currRow, (int)Columns.Price, "����", 999999, 0, ref price);
                            if (!bReturn)
                            {
                                this.isDealCellChange = true;
                                
                                return false;
                            }
                            if (feeItem.FeePack == "0")//��С��λ
                            {
                                price = price * feeItem.Item.PackQty;
                            }

                            feeItem.Item.Price = price;

                            if (feeItem.Item.Price >= this.priceWarnning)
                            {
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ItemName].ForeColor =
                                    Color.FromArgb(this.priceWarinningColor);
                            }
                            else
                            {
                                this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ItemName].ForeColor =
                                    Color.Black;
                            }

                            FS.HISFC.Models.Base.FT ft = this.ComputCost(price, qty, feeItem);

                             if (ft == null) 
                            {
                                this.fpSpread1.Select();
                                this.fpSpread1.Focus();
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.Amount, false);

                                this.isDealCellChange = true;

                                return false;
                            }

                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.Cost].Value = ft.TotCost;

                            feeItem.FT.OwnCost = ft.OwnCost;
                            feeItem.FT.TotCost = ft.TotCost;
                            feeItem.FT.PayCost = ft.PayCost;
                            feeItem.FT.PubCost = ft.PubCost;
                            this.fpSpread1_Sheet1.Cells[currRow, (int)Columns.ExeDept].Locked = false;
                            this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.ExeDept, false);
                            this.SumCost();
                        }
                        #endregion

                        #region �Ƽ۵�λ
                        if (currColumn == (int)Columns.PriceUnit)
                        {
                            //if (feeItem.Item.IsPharmacy)
                            if (feeItem.Item.ItemType == EnumItemType.Drug)
                            {
                                this.fpSpread1_Sheet1.SetActiveCell(currRow, (int)Columns.DoseOnce);
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Focus();
                this.fpSpread1.Focus();

                this.isDealCellChange = true;

                return false;
            }

            this.isDealCellChange = true;

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ���������ݷ����仯ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            if (!isDealCellChange) 
            {
                return;
            }
            
            if (e == null || sender == null)
            {
                return;
            }

            if (this.fpSpread1_Sheet1.Rows[e.Row].Tag != null)
            {
                if (this.fpSpread1_Sheet1.Rows[e.Row].Tag.GetType() == typeof(FeeItemList))
                {
                    FeeItemList feeItem = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FeeItemList;
                    if (e.Column == (int)Columns.Amount)
                    {
                        decimal price = 0;
                        price = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Price].Value);
                        decimal qty = 0;

                        if (price == 0)//��Ŀû�м۸�ֱ����ת������۸��λ��
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Price);
                        }
                        else
                        {
                            try
                            {
                                qty = NConvert.ToDecimal(FS.FrameWork.Public.String.ExpressionVal(this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Amount].Text.ToString()));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("����ļ��㹫ʽ����ȷ������������!" + ex.Message);
                                this.Enter -= new System.EventHandler(this.ucDisplay_Enter);
                                this.fpSpread1.Focus();
                                this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Amount);
                                this.Enter += new System.EventHandler(this.ucDisplay_Enter);
                              
                                return;
                            }

                            qty = FS.FrameWork.Public.String.FormatNumber(qty, 2);

                            if (qty <= 0) 
                            {
                                MessageBox.Show("��������С�ڻ��ߵ�����,����������");
                                this.Enter -= new System.EventHandler(this.ucDisplay_Enter);
                                this.fpSpread1.Select();
                                this.fpSpread1.Focus();
                                this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Amount, false);
                                this.Enter += new System.EventHandler(this.ucDisplay_Enter);
                               
                                return;
                            }

                            if (qty > 99999) 
                            {
                                MessageBox.Show("�������ܴ���99999����������");
                                this.Enter -= new System.EventHandler(this.ucDisplay_Enter);
                                this.fpSpread1.Select();
                                this.fpSpread1.Focus();
                                this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Amount, false);
                                this.Enter += new System.EventHandler(this.ucDisplay_Enter);
                    
                                return;
                            }

                            #region �ж��Ƿ���ȡ��

                            //if (this.isQtyToCeiling && feeItem.Item.IsPharmacy)
                            if (this.isQtyToCeiling && feeItem.Item.ItemType == EnumItemType.Drug)
                            {
                                double qtyValue = System.Convert.ToDouble(qty);

                                qtyValue = System.Math.Ceiling(qtyValue);

                                qty = NConvert.ToDecimal(qtyValue);
                            }

                            this.isDealCellChange = false;

                            this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Amount].Text = qty.ToString();

                            this.isDealCellChange = true;

                            #endregion

                            if (feeItem.FeePack == "1")//��װ��λ
                            {
                                feeItem.Item.Qty = qty * feeItem.Item.PackQty;
                            }
                            else//��С��λ
                            {
                                feeItem.Item.Qty = qty;
                            }

                            FS.HISFC.Models.Base.FT ft = this.ComputCost(price, qty, feeItem);

                            if (ft == null)
                            {
                                this.fpSpread1.Select();
                                this.fpSpread1.Focus();
                                this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Amount, false);
                                this.isValid = false;

                                return;
                            }

                            feeItem.FT.TotCost = ft.TotCost;
                            feeItem.FT.OwnCost = ft.OwnCost;
                            feeItem.FT.PubCost = ft.PubCost;
                            feeItem.FT.PayCost = ft.PayCost;
                            //add by niuxy�����Ż�
                            feeItem.FT.RebateCost = ft.RebateCost;

                            this.isDealCellChange = false;
                            this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Cost].Value = ft.TotCost;
                            SumCost();
                            this.isDealCellChange = true;
                            this.Focus();
                        }
                    }
                    if (e.Column == (int)Columns.CombNo)
                    {
                        string combNo = this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.CombNo].Text;
                        feeItem.Order.Combo.ID = combNo;
                        if (feeItem.InjectCount == 0)
                        {
                            int injectCount = GetInjectSameCombs(combNo);
                            feeItem.InjectCount = injectCount;
                        }
                        this.DrawCombo(this.fpSpread1_Sheet1, (int)Columns.CombNo, (int)Columns.CombNoDisplay, 0);
                    }
                    if (e.Column == (int)Columns.Usage)
                    {
                       
                    }

                   

                    if (e.Column == (int)Columns.Days)
                    {
                        decimal days = 0;
                        decimal qty = 0;
                        decimal totQty = 0;

                        try
                        {
                            days = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Days].Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("������������Ϸ�" + ex.Message);
                            this.Enter -= new System.EventHandler(this.ucDisplay_Enter);
                            this.fpSpread1.Focus();
                            this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Days);
                            this.Enter += new System.EventHandler(this.ucDisplay_Enter);
                           

                            return;
                        }
                        if (days <= 0)
                        {
                            MessageBox.Show("������������Ϸ�, �����������0");
                            this.Enter -= new System.EventHandler(this.ucDisplay_Enter);
                            this.fpSpread1.Focus();
                            this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Days);
                            this.Enter += new System.EventHandler(this.ucDisplay_Enter);
                            

                            return;
                        }
                        qty = NConvert.ToDecimal(FS.FrameWork.Public.String.ExpressionVal(this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.DoseOnce].Text.ToString()));
                        qty = FS.FrameWork.Public.String.FormatNumber(qty, 2);

                        feeItem.Days = days;

                        //{73AA7783-8B97-45f5-B430-0C7311E952C8}    
                        this.hDays = days;
                        // {1FAD3FA2-C7D8-4cac-845F-B9EBECDE2312}
                        totQty = qty * days / ((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose == 0 ? 1 : (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose);
                        //totQty = qty * days;
                        feeItem.Item.Qty = totQty;

                        FS.HISFC.Models.Base.FT ft = this.ComputCost(feeItem.Item.Price, totQty, feeItem);

                        if (ft == null)
                        {
                            this.fpSpread1.Select();
                            this.fpSpread1.Focus();
                            this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Amount, false);
                            

                            return;
                        }
                        
                        feeItem.FT.TotCost = ft.TotCost;
                        feeItem.FT.OwnCost = ft.OwnCost;
                        feeItem.FT.PubCost = ft.PubCost;
                        feeItem.FT.PayCost = ft.PayCost;
                        //add by niuxy�����Ż�
                        feeItem.FT.RebateCost = ft.RebateCost;
                        this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Cost].Value = ft.TotCost;
                        this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Amount].Value = totQty;
                        //{73AA7783-8B97-45f5-B430-0C7311E952C8}    
                        SumCost();
                        this.isDealCellChange = true;
                    }
                    if (e.Column == (int)Columns.Price)
                    {
                        if (feeItem.Item.Price >= this.priceWarnning)
                        {
                            this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.ItemName].ForeColor =
                                Color.FromArgb(this.priceWarinningColor);
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.ItemName].ForeColor =
                                Color.Black;
                        }
                        decimal price = 0;
                        decimal qty = 0;

                        price =
                            FS.FrameWork.Public.String.FormatNumber(
                            NConvert.ToDecimal(
                            FS.FrameWork.Public.String.ExpressionVal(this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Price].Value.ToString())), 4);

                        if (price <= 0)
                        {
                            price = 0;
                        }
                        if (feeItem.FeePack == "0")//��С��λ
                        {
                            price = price * feeItem.Item.PackQty;
                        }

                        feeItem.Item.Price = price;

                        FS.HISFC.Models.Base.FT ft = this.ComputCost(price, qty, feeItem);

                        if (ft == null)
                        {
                            this.fpSpread1.Select();
                            this.fpSpread1.Focus();
                            this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Amount, false);
                            

                            return;
                        }

                        this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Cost].Value = ft.TotCost;


                        feeItem.FT.OwnCost = ft.OwnCost;
                        feeItem.FT.TotCost = ft.TotCost;
                        feeItem.FT.PayCost = ft.PayCost;
                        feeItem.FT.PubCost = ft.PubCost;
                        //add by niuxy�����Ż�
                        feeItem.FT.RebateCost = ft.RebateCost;
                        this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.ExeDept].Locked = false;
                        this.SumCost();
                    }

                    if (e.Column == (int)Columns.DoseOnce)
                    {
                        try
                        {
                            //if (((FeeItemList)this.fpSpread1_Sheet1.Rows[e.Row].Tag).Item.IsPharmacy)
                            if (((FeeItemList)this.fpSpread1_Sheet1.Rows[e.Row].Tag).Item.ItemType == EnumItemType.Drug)
                            {
                                if (this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.DoseOnce].Text == string.Empty)
                                {

                                }
                                else
                                {
                                    feeItem.Order.DoseOnce =
                                        FS.FrameWork.Public.String.FormatNumber(
                                            NConvert.ToDecimal(
                                                FS.FrameWork.Public.String.ExpressionVal(this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.DoseOnce].Text)), 3);
                                    this.isDealCellChange = false;
                                    this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.DoseOnce].Value = feeItem.Order.DoseOnce;
                                    this.isDealCellChange = true;
                                }
                                if (((FeeItemList)this.fpSpread1_Sheet1.Rows[e.Row].Tag).Item.SysClass.ID.ToString() == "PCC")
                                {
                                    decimal days = 0;
                                    decimal qty = 0;
                                    decimal totQty = 0;

                                    try
                                    {
                                        days = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Days].Text);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("������������Ϸ�" + ex.Message);
                                        this.Enter -= new System.EventHandler(this.ucDisplay_Enter);
                                        this.fpSpread1.Focus();
                                        this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Days);
                                        this.Enter += new System.EventHandler(this.ucDisplay_Enter);

                                        return;
                                    }

                                    qty = NConvert.ToDecimal(FS.FrameWork.Public.String.ExpressionVal(this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.DoseOnce].Text.ToString()));
                                    qty = FS.FrameWork.Public.String.FormatNumber(qty, 3);

                                    feeItem.Order.DoseOnce = qty;
                                    // {1FAD3FA2-C7D8-4cac-845F-B9EBECDE2312}
                                    totQty = qty * days / ((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose == 0 ? 1 : (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose);
                                 
                                   // totQty = qty * days;
                                    feeItem.Item.Qty = totQty;

                                    FS.HISFC.Models.Base.FT ft = this.ComputCost(feeItem.Item.Price, totQty, feeItem);

                                    if (ft == null)
                                    {
                                        this.fpSpread1.Select();
                                        this.fpSpread1.Focus();
                                        this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Amount, false);
                                        

                                        return;
                                    }

                                    feeItem.FT.TotCost = ft.TotCost;
                                    feeItem.FT.OwnCost = ft.OwnCost;
                                    feeItem.FT.PubCost = ft.PubCost;
                                    feeItem.FT.PayCost = ft.PayCost;
                                    //add by niuxy�����Ż�
                                    feeItem.FT.RebateCost = ft.RebateCost;
                                    this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Cost].Value = ft.TotCost;
                                    this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Amount].Value = totQty;
                                }
                            }
                        }
                        catch
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void fpSpread1_EditModeOn(object sender, EventArgs e)
        {
            if (e == null || sender == null)
            {
                return;
            }
            SetLocation();
            if (fpSpread1_Sheet1.ActiveColumnIndex != (int)Columns.ExeDept)
                lbDept.Visible = false;
            if (fpSpread1_Sheet1.ActiveColumnIndex != (int)Columns.Freq)
            {
                lbFreq.Visible = false;
            }
            if (fpSpread1_Sheet1.ActiveColumnIndex != (int)Columns.Usage)
            {
                lbUsage.Visible = false;
            }
            this.fpSpread1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
        }

        void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                PutArrow(Keys.Left);
            }
            if (e.KeyCode == Keys.Right)
            {
                PutArrow(Keys.Right);
            }
            if (e.KeyCode == Keys.PageUp)
            {
                if (this.fpSpread1_Sheet1.ActiveRowIndex >= 9)
                {
                    this.fpSpread1_Sheet1.ActiveRowIndex = this.fpSpread1_Sheet1.ActiveRowIndex - 9;
                }
                else
                {
                    this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                }
            }
            if (e.KeyCode == Keys.PageDown)
            {
                if (this.fpSpread1_Sheet1.ActiveRowIndex + 9 <= this.fpSpread1_Sheet1.Rows.Count - 1)
                {
                    this.fpSpread1_Sheet1.ActiveRowIndex = this.fpSpread1_Sheet1.ActiveRowIndex + 9;
                }
                else
                {
                    this.fpSpread1_Sheet1.ActiveRowIndex = this.fpSpread1_Sheet1.Rows.Count - 1;
                }
            }
        }

        private void fpSpread1_EditChange(object sender, EditorNotifyEventArgs e)
        {
            if (e == null || sender == null)
            {
                return;
            }
            //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳�����
            //����߳�û�н���,����Ӧ��Ŀ¼��
            if (this.threadItemInit.ThreadState != ThreadState.Stopped)
            {
                return;
            }
            //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
            if (e.Column == (int)Columns.InputCode && this.chooseItemControl.ChooseItemType == FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemChanging) 
            {
                string inputChar = e.EditingControl.Text;
                //{7FAF97A6-736D-428d-9932-26563EBDD324}
                inputChar = FS.FrameWork.Public.String.TakeOffSpecialChar(inputChar);
                 Control cell = e.EditingControl;

                this.chooseItemControl.SetLocation(new Point(SystemInformation.Border3DSize.Height * 2 + this.fpSpread1.Location.X + cell.Location.X,
                    this.Parent.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2));

                this.chooseItemControl.SetInputChar(this.fpSpread1, inputChar, FS.HISFC.Models.Base.InputTypes.Spell);
            }

            if (e.Column == (int)Columns.ExeDept)
            {
                string text = fpSpread1_Sheet1.ActiveCell.Text;
                //{7FAF97A6-736D-428d-9932-26563EBDD324}
                text = FS.FrameWork.Public.String.TakeOffSpecialChar(text);

                lbDept.Filter(text);
                //��¼ִ�п����Ѿ��޸ģ�Ҫ���¸�ֵ
                fpSpread1_Sheet1.SetValue(e.Row, (int)Columns.Change, "1", false);

                if (lbDept.Visible == false) lbDept.Visible = true;
            }
            if (e.Column == (int)Columns.Freq)
            {
                string text = fpSpread1_Sheet1.ActiveCell.Text;
                //{7FAF97A6-736D-428d-9932-26563EBDD324}
                text = FS.FrameWork.Public.String.TakeOffSpecialChar(text);

                lbFreq.Filter(text);
                //��¼Ƶ���Ѿ��޸ģ�Ҫ���¸�ֵ
                fpSpread1_Sheet1.SetValue(e.Row, (int)Columns.Change, "1", false);

                if (lbFreq.Visible == false) lbFreq.Visible = true;
            }
            if (e.Column == (int)Columns.Usage)
            {
                string text = fpSpread1_Sheet1.ActiveCell.Text;
                //{7FAF97A6-736D-428d-9932-26563EBDD324}
                text = FS.FrameWork.Public.String.TakeOffSpecialChar(text);
                lbUsage.Filter(text);
                //��¼Ƶ���Ѿ��޸ģ�Ҫ���¸�ֵ
                fpSpread1_Sheet1.SetValue(e.Row, (int)Columns.Change, "1", false);

                if (lbUsage.Visible == false) lbUsage.Visible = true;
            }
            if (e.Column == (int)Columns.PriceUnit)
            {
                try
                {
                    string tempString = e.EditingControl.Text;

                    if (((FarPoint.Win.FpCombo)e.EditingControl).List.IndexOf(tempString) == -1)
                    {
                        if (this.fpSpread1_Sheet1.Rows[e.Row].Tag != null)
                        {
                            FeeItemList f = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FeeItemList;
                            ((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex = NConvert.ToInt32(f.FeePack);
                        }
                    }
                }
                catch { }
            }
        }

        void myInjec_WhenInputInjecs(decimal injecs)
        {
            injec = injecs;
        }

        private void fpSpread1_Enter(object sender, EventArgs e)
        {
            if (e == null || sender == null)
            {
                return;
            }
            isFocus = true;
        }

        private void fpSpread1_Leave(object sender, EventArgs e)
        {
            if (e == null || sender == null)
            {
                return;
            }
            this.fpSpread1.StopCellEditing();

            isFocus = false;
        }

        private void ucDisplay_Enter(object sender, EventArgs e)
        {
            if (e == null || sender == null)
            {
                return;
            }
            int rowCount = this.fpSpread1_Sheet1.RowCount;
            if (rowCount > 0)
            {
                try
                {
                    this.fpSpread1_Sheet1.SetActiveCell(rowCount - 1, 0, false);
                }
                catch { }
            }
        }

        private void fpSpread1_ComboSelChange(object sender, EditorNotifyEventArgs e)
        {
            if (e == null || sender == null)
            {
                return;
            }
            if (e.Column == (int)Columns.PriceUnit)
            {
                try
                {
                    FeeItemList feeItem = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FeeItemList;
                    decimal price = 0;
                    decimal qty = 0;

                    qty = NConvert.ToDecimal
                        (this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Amount].Text);

                    if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 1)//��װ��λ
                    {
                        feeItem.FeePack = "1";//��װ��λ
                        this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Price].Value = feeItem.Item.Price;
                        feeItem.Item.Qty = qty * feeItem.Item.PackQty;
                    }
                    else
                    {
                        feeItem.FeePack = "0"; //��С��λ
                        this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Price].Value =
                            FS.FrameWork.Public.String.FormatNumber(
                            NConvert.ToDecimal(feeItem.Item.Price / feeItem.Item.PackQty), 4);
                        feeItem.Item.Qty = qty;
                    }

                    FS.HISFC.Models.Base.FT ft = this.ComputCost(price, qty, feeItem);

                    if (ft == null)
                    {
                        this.fpSpread1.Select();
                        this.fpSpread1.Focus();
                        this.fpSpread1_Sheet1.SetActiveCell(e.Row, (int)Columns.Amount, false);

                        return;
                    }

                    this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.Cost].Value = ft.TotCost;

                    feeItem.FT.OwnCost = ft.OwnCost;
                    feeItem.FT.TotCost = ft.TotCost;
                    feeItem.FT.PayCost = ft.PayCost;
                    feeItem.FT.PubCost = ft.PubCost;
                    feeItem.Item.PriceUnit = ((FarPoint.Win.Spread.CellType.ComboBoxCellType)this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.PriceUnit].CellType).Items[((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex];
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)Columns.PriceUnit].Text = feeItem.Item.PriceUnit;
                    SumCost();
                    if (!this.ContainsFocus && !this.fpSpread1.ContainsFocus)
                    {
                        this.Focus();
                        this.fpSpread1.Focus();
                    }
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void fpSpread1_CellClick(object sender, CellClickEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange c = this.fpSpread1.GetCellFromPixel(0, 0, e.X, e.Y);

            this.RefreshItemInfo(c.Row);

            
        }

        #endregion

        //����ӿ�
        #region IInterfaceContainer ��Ա
        //{21C33D5B-5583-4b1d-8023-278336C0C6C7}
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient);
                type[1] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade);

                return type;
            }
        }

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshItem();
        }

        //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        private void fpSpread1_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == (int)Columns.Select)
            {
                this.SumCost();
            }
        }

        public void PreCountInvos()
        {
        }
    }
}
