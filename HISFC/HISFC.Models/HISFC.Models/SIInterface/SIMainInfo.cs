using System;
using FS.FrameWork.Models;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// SIMainInfo ��ժҪ˵����
	/// Id inpatientNo, name ��������
	/// </summary>
    [Serializable]
    public class SIMainInfo : FS.FrameWork.Models.NeuObject
    {
        public SIMainInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ��չ����
        private System.Collections.Generic.Dictionary<string, NeuObject> extendProperty = new System.Collections.Generic.Dictionary<string, NeuObject>();
        /// <summary>
        /// ��չ����
        /// </summary>
        public System.Collections.Generic.Dictionary<string, NeuObject> ExtendProperty
        {
            get { return extendProperty; }
            set { extendProperty = value; }
        } 
        #endregion

        private int feeTimes;
        /// <summary>
        /// ��������
        /// </summary>
        public int FeeTimes
        {
            set
            {
                feeTimes = value;
            }
            get
            {
                return feeTimes;
            }
        }
        private int readFlag;
        /// <summary>
        /// �����־
        /// </summary>
        public int ReadFlag
        {
            get
            {
                return readFlag;
            }
            set
            {
                readFlag = value;
            }
        }

        private string regNo;
        /// <summary>
        /// ����ǼǺš���·ҽ�����˱��
        /// </summary>
        public string RegNo
        {
            set
            {
                regNo = value;
            }
            get
            {
                return regNo;
            }
        }

        private string hosNo;
        /// <summary>
        /// ҽԺ���
        /// </summary>
        public string HosNo
        {
            set { hosNo = value; }
            get { return hosNo; }
        }

        private string balNo;
        /// <summary>
        ///  �������
        /// </summary>
        public string BalNo
        {
            get
            {
                if (balNo == null || balNo == "")
                {
                    balNo = "0";
                }
                return balNo;
            }
            set { balNo = value; }
        }
        private string invoiceNo;
        /// <summary>
        /// ����Ʊ��
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        private FS.FrameWork.Models.NeuObject medicalType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ҽ����� 1-סԺ 2 -�����ض���Ŀ
        /// </summary>
        public FS.FrameWork.Models.NeuObject MedicalType
        {
            get { return medicalType; }
            set { medicalType = value; }
        }
        //		private string patientNo;
        //		/// <summary>
        //		/// סԺ��
        //		/// </summary>
        //		public string PatientNo
        //		{
        //			get{return patientNo;}
        //			set{patientNo = value;}
        //		}
        //		private string cardNo;
        //		/// <summary>
        //		/// ���￨��
        //		/// </summary>
        //		public string CardNo
        //		{
        //			get{return cardNo;}
        //			set{cardNo = value;}
        //		}
        //		private string mCardNo;
        //		/// <summary>
        //		/// ҽ��֤��
        //		/// </summary>
        //		public string MCardNo
        //		{
        //			get{return mCardNo;}
        //			set{mCardNo = value;}
        //		}
        private string proceatePcNo;
        /// <summary>
        /// �������ջ��ߵ��Ժ�
        /// </summary>
        public string ProceatePcNo
        {
            get { return proceatePcNo; }
            set { proceatePcNo = value; }
        }
        private DateTime siBeginDate;
        /// <summary>
        /// �α�����
        /// </summary>
        public DateTime SiBegionDate
        {
            get { return siBeginDate; }
            set { siBeginDate = value; }
        }
        private string siState;
        /// <summary>
        /// �α�״̬ 3-�α��ɷѡ�4-��ͣ�ɷѡ�7-��ֹ�α�
        /// </summary>
        public string SiState
        {
            get { return siState; }
            set { siState = value; }
        }
        private string emplType;
        /// <summary>
        /// ��Ա��� 1-��ְ��2-����
        /// </summary>
        public string EmplType
        {
            get { return emplType; }
            set { emplType = value; }
        }
        private string clinicDiagNose;
        /// <summary>
        /// �������
        /// </summary>
        public string ClinicDiagNose
        {
            get { return clinicDiagNose; }
            set { clinicDiagNose = value; }
        }
        private DateTime inDiagnoseDate;
        /// <summary>
        /// ��Ժ�������
        /// </summary>
        public DateTime InDiagnoseDate
        {
            get { return inDiagnoseDate; }
            set { inDiagnoseDate = value; }
        }

        private FS.FrameWork.Models.NeuObject inDiagnose = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ��Ժ�����Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject InDiagnose
        {
            get { return inDiagnose; }
            set { inDiagnose = value; }
        }

        private decimal totCost;
        /// <summary>
        /// סԺ�ܽ��
        /// </summary>
        public decimal TotCost
        {
            get { return totCost; }
            set { totCost = value; }
        }
        private decimal addTotCost = 0;
        /// <summary>
        /// �����ۼ�
        /// </summary>
        public decimal AddTotCost
        {
            get { return addTotCost; }
            set { addTotCost = value; }
        }
        private decimal payCost;
        /// <summary>
        /// �ʻ�֧�����
        /// </summary>
        public decimal PayCost
        {
            get { return payCost; }
            set { payCost = value; }
        }

        /// <summary>
        /// �籣֧�����(���ԷѺ��˻�֧�������н��ĺϼ�)
        /// </summary>
        private decimal pubCost;
        /// <summary>
        /// �籣֧�����(���ԷѺ��˻�֧�������н��ĺϼ�)
        /// </summary>
        public decimal PubCost
        {
            get { return pubCost; }
            set { pubCost = value; }
        }
        //{06A3389F-B19E-4482-A55C-89269995B142}
        /// <summary>
        /// ҽ�����ص�ͳ����
        /// </summary>
        private decimal siPubCost;

        /// <summary>
        /// ҽ�����ص�ͳ����
        /// </summary>
        public decimal SiPubCost
        {
            get { return this.siPubCost; }
            set { this.siPubCost = value; }

        }

        private decimal itemPayCost;
        /// <summary>
        /// ������Ŀ�Ը���� 
        /// </summary>
        public decimal ItemPayCost
        {
            get { return itemPayCost; }
            set { itemPayCost = value; }
        }
        private decimal baseCost;
        /// <summary>
        /// �����𸶽��
        /// </summary>
        public decimal BaseCost
        {
            get { return baseCost; }
            set { baseCost = value; }
        }
        private decimal ownCost;
        /// <summary>
        /// �����Է���Ŀ���
        /// </summary>
        public decimal OwnCost
        {
            get { return ownCost; }
            set { ownCost = value; }
        }
        private decimal itemYLCost;
        /// <summary>
        /// �����Ը��������Ը����֣�
        /// </summary>
        public decimal ItemYLCost
        {
            get { return itemYLCost; }
            set { itemYLCost = value; }
        }

        private decimal pubOwnCost;
        /// <summary>
        /// �����Ը����
        /// </summary>
        public decimal PubOwnCost
        {
            set { pubOwnCost = value; }
            get { return pubOwnCost; }
        }

        private decimal overTakeOwnCost;
        /// <summary>
        /// ��ͳ��֧���޶�����Ը����
        /// </summary>
        public decimal OverTakeOwnCost
        {
            get { return overTakeOwnCost; }
            set { overTakeOwnCost = value; }
        }

        private decimal hosCost;
        /// <summary>
        /// ҽҩ�����ֵ����
        /// </summary>
        public decimal HosCost
        {
            set
            {
                hosCost = value;
            }
            get
            {
                return hosCost;
            }
        }

        private FS.FrameWork.Models.NeuObject operInfo = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperInfo
        {
            get { return operInfo; }
            set { operInfo = value; }
        }
        private DateTime operDate;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }
        private int appNo;
        /// <summary>
        /// ������
        /// </summary>
        public int AppNo
        {
            get { return appNo; }
            set { appNo = value; }
        }
        private DateTime balanceDate;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime BalanceDate
        {
            get { return balanceDate; }
            set { balanceDate = value; }
        }
        private decimal yearCost;
        /// <summary>
        /// ����ȿ��ö���
        /// </summary>
        public decimal YearCost
        {
            get
            {
                return yearCost;
            }
            set
            {
                yearCost = value;
            }
        }
        private FS.FrameWork.Models.NeuObject outDiagnose = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ��Ժ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject OutDiagnose
        {
            set { outDiagnose = value; }
            get { return outDiagnose; }
        }

        private bool isValid;
        /// <summary>
        /// �Ƿ���Ч True��Ч False ��Ч
        /// </summary>
        public bool IsValid
        {
            set
            {
                isValid = value;
            }
            get
            {
                return isValid;
            }
        }

        private bool isBalanced;
        /// <summary>
        /// �Ƿ���� True ���� False δ����
        /// </summary>
        public bool IsBalanced
        {
            get
            {
                return isBalanced;
            }
            set
            {
                isBalanced = value;
            }
        }


        #region ��·ҽ����������
        #region ����
        string icCardCode = "";
        FS.FrameWork.Models.NeuObject personType = new NeuObject();
        FS.FrameWork.Models.NeuObject civilianGrade = new NeuObject();
        FS.FrameWork.Models.NeuObject specialCare = new NeuObject();
        string duty = "";
        FS.FrameWork.Models.NeuObject anotherCity = new NeuObject();
        FS.FrameWork.Models.NeuObject corporation = new NeuObject();
        decimal individualBalance = 0;
        string freezeMessage = "";
        string applySequence = "";
        FS.FrameWork.Models.NeuObject disease = new NeuObject();
        FS.FrameWork.Models.NeuObject applyType = new NeuObject();
        FS.FrameWork.Models.NeuObject fund = new NeuObject();
        string businessSequence = "";
        FS.FrameWork.Models.NeuObject specialWorkKind = new NeuObject();
        string hospitalBusinessSequence = "";
        string opositeBusinessSequence = "";
        #endregion
        /// <summary>
        /// IC������
        /// </summary>
        public string ICCardCode
        {
            get
            {
                return this.icCardCode;
            }
            set
            {
                this.icCardCode = value;
            }
        }

        /// <summary>
        /// ��Ա���
        /// </summary>
        public FS.FrameWork.Models.NeuObject PersonType
        {
            get
            {
                return this.personType;
            }
            set
            {
                this.personType = value;
            }
        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        public FS.FrameWork.Models.NeuObject CivilianGrade
        {
            get
            {
                return this.civilianGrade;
            }
            set
            {
                this.civilianGrade = value;
            }
        }
        /// <summary>
        /// �����չ���Ⱥ
        /// </summary>
        public FS.FrameWork.Models.NeuObject SpecialCare
        {
            get
            {
                return this.specialCare;
            }
            set
            {
                this.specialCare = value;
            }
        }
        /// <summary>
        /// ְ��
        /// </summary>
        public string Duty
        {
            get
            {
                return this.duty;
            }
            set
            {
                this.duty = value;
            }
        }
        /// <summary>
        /// ��ذ��ó���
        /// </summary>
        public FS.FrameWork.Models.NeuObject AnotherCity
        {
            get
            {
                return this.anotherCity;
            }
            set
            {
                this.anotherCity = value;
            }
        }
        /// <summary>
        /// �α��˵�λ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Corporation
        {
            get
            {
                return this.corporation;
            }
            set
            {
                this.corporation = value;
            }
        }
        /// <summary>
        /// �����ʻ����
        /// </summary>
        public decimal IndividualBalance
        {
            get
            {
                return this.individualBalance;
            }
            set
            {
                this.individualBalance = value;
            }
        }
        /// <summary>
        /// �Ѷ��������Ϣ
        /// </summary>
        public string FreezeMessage
        {
            get
            {
                return this.freezeMessage;
            }
            set
            {
                this.freezeMessage = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string ApplySequence
        {
            get
            {
                return this.applySequence;
            }
            set
            {
                this.applySequence = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Disease
        {
            get
            {
                return this.disease;
            }
            set
            {
                this.disease = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ApplyType
        {
            get
            {
                return this.applyType;
            }
            set
            {
                this.applyType = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Fund
        {
            get
            {
                return this.fund;
            }
            set
            {
                this.fund = value;
            }
        }
        /// <summary>
        /// ҵ�����
        /// </summary>
        public string BusinessSequence
        {
            get
            {
                return this.businessSequence;
            }
            set
            {
                this.businessSequence = value;
            }
        }
        /// <summary>
        /// ���⹤��
        /// </summary>
        public FS.FrameWork.Models.NeuObject SpecialWorkKind
        {
            get
            {
                return this.specialWorkKind;
            }
            set
            {
                this.specialWorkKind = value;
            }
        }
        /// <summary>
        /// ҽԺ�������к�
        /// </summary>
        public string HospitalBusinessSequence
        {
            get
            {
                return this.hospitalBusinessSequence;
            }
            set
            {
                this.hospitalBusinessSequence = value;
            }
        }
        /// <summary>
        /// ��Ӧ�������к�
        /// </summary>
        public string OpositeBusinessSequence
        {
            get
            {
                return this.opositeBusinessSequence;
            }
            set
            {
                this.opositeBusinessSequence = value;
            }
        }

        public new SIMainInfo Clone()
        {
            SIMainInfo obj = base.Clone() as SIMainInfo;
            obj.medicalType = this.medicalType.Clone();
            obj.inDiagnose = this.inDiagnose.Clone();
            obj.operInfo = this.operInfo.Clone();
            obj.PersonType = this.PersonType.Clone();
            obj.CivilianGrade = this.CivilianGrade.Clone();
            obj.SpecialCare = this.SpecialCare.Clone();
            obj.AnotherCity = this.AnotherCity.Clone();
            obj.Corporation = this.Corporation.Clone();
            obj.Disease = this.Disease.Clone();
            obj.ApplyType = this.ApplyType.Clone();
            obj.Fund = this.Fund.Clone();
            obj.SpecialWorkKind = this.SpecialWorkKind.Clone();
            System.Collections.Generic.Dictionary<string, NeuObject> ep = new System.Collections.Generic.Dictionary<string, NeuObject>();
            foreach (string s in this.ExtendProperty.Keys)
            {
                ep.Add(s, this.ExtendProperty[s].Clone());
            }
            obj.ExtendProperty = ep;
            return obj;
        }
        #endregion

        #region ����ҽ����������

        #region ����

        /// <summary>
        /// ������������
        /// </summary>
        private string cardOrgID = string.Empty;

        /// <summary>
        /// ����Ч��
        /// </summary>
        private DateTime cardValidTime = DateTime.MinValue;

        /// <summary>
        /// �������
        /// </summary>
        private DateTime shiftTime = DateTime.MinValue;

        /// <summary>
        /// �Ƿ��Ѿ�����
        /// </summary>
        private bool isCardLocked = false;

        /// <summary>
        /// ����ͳ��֧���ۼ�
        /// </summary>
        private decimal yearPubCost = 0;

        /// <summary>
        /// ���������֧���ۼ�
        /// </summary>
        private decimal yearHelpCost = 0;

        /// <summary>
        /// ת��ҽԺ�����׼
        /// </summary>
        private decimal turnOutHosStandardCost = 0;

        /// <summary>
        /// ת��ҽԺ�����׼�Ը�
        /// </summary>
        private decimal turnOutHosOnwCost = 0;

        /// <summary>
        /// סԺ����
        /// </summary>
        private int inHosTimes = 0;

        /// <summary>
        /// �˻�֧���ۼ�
        /// </summary>
        private decimal payAddCost = 0;

        /// <summary>
        /// �˻�֧�����
        /// </summary>
        private string payYear = string.Empty;

        /// <summary>
        /// �ֽ�֧������ۼ�
        /// </summary>
        private decimal ownCashAddCost = 0;

        /// <summary>
        /// �����Ը�(������Ŀ)����ۼ�
        /// </summary>
        private decimal ownAddCost = 0;
        /// <summary>
        /// ��ȸ����Ը��ۼ�
        /// </summary>
        private decimal yearOwnAddCost = 0;

        /// <summary>
        /// ����Ա֧������ۼ�
        /// </summary>
        private decimal gwyPayAddCost = 0;

        /// <summary>
        /// ��������֧���ۼ�
        /// </summary>
        private decimal spOutpatientPayAddCost = 0;

        /// <summary>
        /// �������Բ�֧���ۼ�
        /// </summary>
        private decimal slowOutpatientPayAddCost = 0;
        /// <summary>
        /// �ʻ�ע���ۼ�
        /// </summary>
        private decimal yearAddPayCost = 0;
        /// <summary>
        ///  �ʻ�ע��ˢ������
        /// </summary>
        private DateTime freshAddPayDate = DateTime.MinValue;
        /// <summary>
        /// ��ת�ʻ�֧���ۼ�
        /// </summary>
        private decimal yearAddPayTurnCost = 0;

        #endregion

        #region ����

        /// <summary>
        /// ������������
        /// </summary>
        public string CardOrgID
        {
            get
            {
                return this.cardOrgID;
            }
            set
            {
                this.cardOrgID = value;
            }
        }

        /// <summary>
        /// ����Ч��
        /// </summary>
        public DateTime CardValidTime
        {
            get
            {
                return this.cardValidTime;
            }
            set
            {
                this.cardValidTime = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public DateTime ShiftTime
        {
            get
            {
                return this.shiftTime;
            }
            set
            {
                this.shiftTime = value;
            }
        }

        /// <summary>
        /// �Ƿ��Ѿ�����
        /// </summary>
        public bool IsCardLocked
        {
            get
            {
                return this.isCardLocked;
            }
            set
            {
                this.isCardLocked = value;
            }
        }

        /// <summary>
        /// ����ͳ��֧���ۼ�
        /// </summary>
        public decimal YearPubCost
        {
            get
            {
                return this.yearPubCost;
            }
            set
            {
                this.yearPubCost = value;
            }
        }

        /// <summary>
        /// ���������֧���ۼ�
        /// </summary>
        public decimal YearHelpCost
        {
            get
            {
                return this.yearHelpCost;
            }
            set
            {
                this.yearHelpCost = value;
            }
        }

        /// <summary>
        /// ת��ҽԺ�����׼
        /// </summary>
        public decimal TurnOutHosStandardCost
        {
            get
            {
                return this.turnOutHosStandardCost;
            }
            set
            {
                this.turnOutHosStandardCost = value;
            }
        }

        /// <summary>
        /// ת��ҽԺ�����׼�Ը�
        /// </summary>
        public decimal TurnOutHosOnwCost
        {
            get
            {
                return this.turnOutHosOnwCost;
            }
            set
            {
                this.turnOutHosOnwCost = value;
            }
        }

        /// <summary>
        /// סԺ����
        /// </summary>
        public int InHosTimes
        {
            get
            {
                return this.inHosTimes;
            }
            set
            {
                this.inHosTimes = value;
            }
        }

        /// <summary>
        /// �˻�֧���ۼ�
        /// </summary>
        public decimal PayAddCost
        {
            get
            {
                return this.payAddCost;
            }
            set
            {
                this.payAddCost = value;
            }
        }

        /// <summary>
        /// �˻�֧�����
        /// </summary>
        public string PayYear
        {
            get
            {
                return this.payYear;
            }
            set
            {
                this.payYear = value;
            }
        }

        /// <summary>
        /// �ֽ�֧������ۼ�
        /// </summary>
        public decimal OwnCashAddCost
        {
            get
            {
                return this.ownCashAddCost;
            }
            set
            {
                this.ownCashAddCost = value;
            }
        }

        /// <summary>
        /// �����Ը�(������Ŀ)����ۼ�
        /// </summary>
        public decimal OwnAddCost
        {
            get
            {
                return this.ownAddCost;
            }
            set
            {
                this.ownAddCost = value;
            }
        }
        /// <summary>
        /// ��ȸ����Ը��ۼ�
        /// </summary>
        public decimal YearOwnAddCost
        {
            get
            {
                return this.yearAddPayCost;
            }
            set
            {
                this.yearAddPayCost = value;
            }
        }
        /// <summary>
        /// ����Ա֧������ۼ�
        /// </summary>
        public decimal GwyPayAddCost
        {
            get
            {
                return this.gwyPayAddCost;
            }
            set
            {
                this.gwyPayAddCost = value;
            }
        }



        /// <summary>
        /// ��������֧���ۼ�
        /// </summary>
        public decimal SpOutpatientPayAddCost
        {
            get
            {
                return this.spOutpatientPayAddCost;
            }
            set
            {
                this.spOutpatientPayAddCost = value;
            }
        }

        /// <summary>
        /// �������Բ�֧���ۼ�
        /// </summary>
        public decimal SlowOutpatientPayAddCost
        {
            get
            {
                return this.slowOutpatientPayAddCost;
            }
            set
            {
                this.slowOutpatientPayAddCost = value;
            }
        }
        /// <summary>
        /// �ʻ�ע���ۼ�
        /// </summary>
        public decimal YearAddPayCost
        {
            set
            {
                this.yearAddPayCost = value;
            }
            get
            {
                return this.yearAddPayCost;
            }
        }
        /// <summary>
        /// �ʻ�ע��ˢ������
        /// </summary>
        public DateTime FreshAddPayDate
        {
            set
            {
                this.freshAddPayDate = value;
            }
            get
            {
                return this.freshAddPayDate;
            }
        }
        /// <summary>
        /// ��ת�ʻ�֧���ۼ�
        /// </summary>
        public decimal YearAddPayTurnCost
        {
            set
            {
                this.yearAddPayCost = value;
            }
            get
            {
                return this.yearAddPayCost;
            }

        }
        /// <summary>
        /// �Ƿ���Ա
        /// </summary>
        private bool isOffice = false;
        /// <summary>
        /// �Ƿ���Ա
        /// </summary>
        public bool IsOffice
        {
            set
            {
                this.isOffice = value;
            }
            get
            {
                return this.isOffice;
            }

        }

        /// <summary>
        /// ҽ��סԺ״̬
        /// </summary>
        private string inStateForYB = string.Empty;
        /// <summary>
        /// ҽ��סԺ״̬
        /// </summary>
        public String InStateForYB
        {
            set
            {
                this.inStateForYB = value;
            }
            get
            {
                return this.inStateForYB;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        private string birthPlace = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        public string BirthPlace
        {
            set
            {
                this.birthPlace = value;
            }
            get
            {
                return this.birthPlace;
            }
        }
        /// <summary>
        /// ��Ժ����
        /// </summary>
        private DateTime leaveHosDate = DateTime.MinValue;
        /// <summary>
        /// ��Ժ����
        /// </summary>
        public DateTime LeaveHosDate
        {
            set
            {
                this.leaveHosDate = value;
            }
            get
            {
                return this.leaveHosDate;
            }
        }
        /// <summary>
        /// ��ͥ����֧���ۼ�
        /// </summary>
        private decimal homeBedFeeAddCost = 0;
        /// <summary>
        /// ��ͥ����֧���ۼ�
        /// </summary>
        public decimal HomeBedFeeAddCost
        {
            set
            {
                this.homeBedFeeAddCost = value;
            }
            get
            {
                return this.homeBedFeeAddCost;
            }
        }
        /// <summary>
        /// ��������޶��Ա����֧���ۼ�(26) 
        /// </summary>
        private decimal gwyBeyondPayAddCost = 0;
        /// <summary>
        /// ��������޶��Ա����֧���ۼ�(26) 
        /// </summary>
        public decimal GwyBeyondPayAddCost
        {
            get
            {
                return this.gwyBeyondPayAddCost;
            }
            set
            {
                this.gwyBeyondPayAddCost = value;
            }
        }
        /// <summary>
        /// ����ͳ��֧���ۼ�
        /// </summary>
        private decimal lxAddPubCost = 0;
        /// <summary>
        /// ����ͳ��֧���ۼ�
        /// </summary>
        public decimal LxAddPubCost
        {
            set
            {
                this.lxAddPubCost = value;
            }
            get
            {
                return this.lxAddPubCost;
            }
        }
        /// <summary>
        /// �����ֽ�֧���ۼ�
        /// </summary>
        private decimal cashAddCostForMZ = 0;
        /// <summary>
        /// �����ֽ�֧���ۼ�
        /// </summary>
        public decimal CashAddCostForMZ
        {
            set
            {
                this.cashAddCostForMZ = value;
            }
            get
            {
                return this.cashAddCostForMZ;
            }
        }
        /// <summary>
        /// ���﹫��Ա����֧���ۼ�
        /// </summary>

        private decimal officalSupplyCostForMZ = 0;
        /// <summary>
        /// ���﹫��Ա����֧���ۼ�
        /// </summary>
        public decimal OfficalSupplyCostForMZ
        {
            set
            {
                this.officalSupplyCostForMZ = value;
            }
            get
            {
                return this.officalSupplyCostForMZ;
            }
        }
        /// <summary>
        /// ���������Ƿ��������־
        /// </summary>
        private bool proceateLastFlag = false;
        /// <summary>
        /// ���������Ƿ��������־
        /// </summary>
        public bool ProceateLastFlag
        {
            get
            {
                return proceateLastFlag;
            }
            set
            {
                proceateLastFlag = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        private decimal overCost = 0;
        /// <summary>
        /// ����
        /// </summary>
        public decimal OverCost
        {
            set
            {
                this.overCost = value;
            }
            get
            {
                return this.overCost;
            }
        }

        /// <summary>
        /// ����Ա����֧��
        /// </summary>
        private decimal officalCost = 0;
        /// <summary>
        /// ����Ա����֧��
        /// </summary>
        public decimal OfficalCost
        {
            set
            {
                this.officalCost = value;
            }
            get
            {
                return this.officalCost;
            }
        }


        //private string reimbFlag = string.Empty;
        //public string ReimbFlag
        //{
        //    set
        //    {
        //        this.reimbFlag = value;
        //    }
        //    get
        //    {
        //        return this.reimbFlag;
        //    }
        //}

        //private int transType = 0;
        //public int TransType
        //{
        //    set
        //    {
        //        this.transType = value;
        //    }
        //    get
        //    {
        //        return this.transType;
        //    }
        //}

        #endregion

        #endregion
    }
}
