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

        private string tacCode;

        /// <summary>
        /// tac��
        /// �麣ҽ��������֤��
        /// </summary>
        public string TacCode
        {
            get { return tacCode; }
            set { tacCode = value; }
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

        #region �����Ѻ���������
        //{BA600C87-44A9-4dbc-86C7-5478796201A3}��ʼ

        /// <summary>
        /// �Ƿ��Ѿ����
        /// </summary>
        private bool isShifted = false;

        /// <summary>
        /// �Ƿ��Ѿ����
        /// </summary>
        public bool IsShifted
        {
            get
            {
                return this.isShifted;
            }
            set
            {
                this.isShifted = value;
            }
        }

        /// <summary>
        /// �����¼
        /// </summary>
        private FS.HISFC.Models.Base.ShiftRecord shiftRecord = new FS.HISFC.Models.Base.ShiftRecord();

        /// <summary>
        /// �����¼
        /// </summary>
        public FS.HISFC.Models.Base.ShiftRecord ShiftRecord
        {
            get
            {
                return this.shiftRecord;
            }
            set
            {
                this.shiftRecord = value;
            }
        }

        /// <summary>
        /// ҽ���ϴ���
        /// </summary>
        private string transNo;

        /// <summary>
        /// ҽ���ϴ���
        /// </summary>
        public string TransNo
        {
            get
            {
                return transNo;
            }
            set
            {
                this.transNo = value;
            }
        }

        /// <summary>
        /// ��ͨҽ���ڷ���
        /// </summary>
        private decimal internalFee;
        /// <summary>
        /// ��ͨҽ���ڷ���
        /// </summary>
        public decimal InternalFee
        {
            get
            {
                return internalFee;
            }
            set
            {
                this.internalFee = value;
            }
        }

        /// <summary>
        /// ��ͨҽ�������
        /// </summary>
        private decimal externalFee;

        /// <summary>
        /// ��ͨҽ�������
        /// </summary>
        public decimal ExternalFee
        {
            get
            {
                return externalFee;
            }
            set
            {
                this.externalFee = value;
            }
        }
        /// <summary>
        /// ���/����Ա�Ը����
        /// </summary>
        private decimal officalOwnCost;

        /// <summary>
        /// ���/����Ա�Ը����
        /// </summary>
        public decimal OfficalOwnCost
        {
            get
            {
                return officalCost;
            }
            set
            {
                this.officalCost = value;
            }
        }

        /// <summary>
        /// ���ν���ͳ��ⶥ��ҽ���ڽ��
        /// </summary>
        private decimal overInterFee;

        /// <summary>
        /// ���ν���ͳ��ⶥ��ҽ���ڽ��
        /// </summary>
        public decimal OverInterFee
        {
            get
            {
                return this.overInterFee;
            }
            set
            {
                this.overInterFee = value;
            }
        }

        /// <summary>
        /// ����Ӧ���ܽ��(�����ʻ�֧��+�ֽ�)
        /// </summary>
        private decimal ownCountFee;
        /// <summary>
        /// ����Ӧ���ܽ��(�����ʻ�֧��+�ֽ�)
        /// </summary>
        public decimal OwnCountFee
        {
            set
            {
                this.ownCountFee = value;
            }
            get
            {
                return ownCountFee;
            }
        }

        /// <summary>
        /// �����Ը������
        /// </summary>
        private decimal ownSecondCountFee;
        /// <summary>
        /// �����Ը������
        /// </summary>
        public decimal OwnSecondCountFee
        {
            get
            {
                return ownSecondCountFee;
            }
            set
            {
                this.ownSecondCountFee = value;
            }
        }

        /// <summary>
        /// ҽ����ϴ���
        /// </summary>
        private string siDiagnose = "";
        /// <summary>
        /// ҽ����ϴ���
        /// </summary>
        public string SIDiagnose
        {
            set
            {
                this.siDiagnose = value;
            }
            get
            {
                return this.siDiagnose;
            }
        }
        /// <summary>
        /// ҽ����ϴ�������
        /// </summary>
        private string siDiagnoseName = "";
        /// <summary>
        /// ҽ����ϴ�������
        /// </summary>
        public string SIDiagnoseName
        {
            set
            {
                this.siDiagnoseName = value;
            }
            get
            {
                return this.siDiagnoseName;
            }
        }
        /// <summary>
        /// ����״̬��1 ���� 0 δ����
        /// </summary>
        private string balanceState = "";
        /// <summary>
        /// ����״̬��1 ���� 0 δ����
        /// </summary>
        public string BalanceState
        {
            get
            {
                return this.balanceState;
            }
            set
            {
                this.balanceState = value;
            }
        }
        /// <summary>
        /// �������� 1�������� 2��������
        /// </summary>
        private string transType = "";
        /// <summary>
        /// �������� 1�������� 2��������
        /// </summary>
        public string TransType
        {
            get
            {
                return this.transType;
            }
            set
            {
                this.transType = value;
            }
        }
        /// <summary>
        /// �������1-����2-סԺ
        /// </summary>
        private string typeCode = "";
        /// <summary>
        /// �������1-����2-סԺ
        /// </summary>
        public string TypeCode
        {
            get
            {
                return this.typeCode;
            }
            set
            {
                this.typeCode = value;
            }
        }
        /// <summary>
        /// ͳ��֧�����
        /// </summary>
        private decimal pubFeeCost = 0M;
        /// <summary>
        /// ͳ��֧�����
        /// </summary>
        public decimal PubFeeCost
        {
            get
            {
                return this.pubFeeCost;
            }
            set
            {
                this.pubFeeCost = value;
            }
        }
        /// <summary>
        /// �Ƿ��Ѿ���ҽ���ֲ�(����ʹ�ã�ҽ���м������)
        /// </summary>
        private bool isGetSSN = false;
        /// <summary>
        /// �Ƿ��Ѿ���ҽ���ֲ�(����ʹ�ã�ҽ���м������)
        /// </summary>
        public bool IsGetSSN
        {
            get
            {
                return this.isGetSSN;
            }
            set
            {
                this.isGetSSN = value;
            }
        }
        //{BA600C87-44A9-4dbc-86C7-5478796201A3}����
        #endregion

        #region ��ɽ����������ҽ���������� 
        // {4669B819-39AB-476b-B3A1-60AAF150FD45}
        private long ybMedNo = 0;
        /// <summary>
        /// ����ҽ�����㵥��
        /// ������ҽԺ���㵥��Ψһ��ʶ��
        /// ��������Ϊ ע������������ý��� ����֮һ
        /// </summary>
        public long YBMedNo
        {
            get { return ybMedNo; }
            set { ybMedNo = value; }
        }

        #endregion

        #region ����ҽ�����ӵ�����

        #region ����
        private string cblx = "";
        public string lxcbys = "";
        private string jbkyye = "";
        private string bckyye = "";

        #endregion

        public string CBLX
        {
            get
            {
                return cblx;
            }
            set
            {
                cblx = value;
            }
        }

        /// <summary>
        /// �����α�����
        /// </summary>


        public string LXCBYS
        {
            get
            {
                return lxcbys;
            }
            set
            {
                lxcbys = value;
            }

        }
        /// <summary>
        /// ����ҽ�Ʊ��չ��û����������ǰ��
        /// </summary>

        public string JBKYYE
        {
            get
            {
                return jbkyye;
            }
            set
            {
                jbkyye = value;
            }

        }
        /// <summary>
        /// �ط�����ҽ�Ʊ��չ��û����������ǰ��
        /// </summary>
        public string BCKYYE
        {
            get
            {
                return bckyye;
            }
            set
            {
                bckyye = value;

            }

        }
        /// <summary>
        /// //--�໤��1����*
        /// </summary>
        private string jhr1xm;
        public string JHR1XM
        {
            get
            {
                return jhr1xm;
            }
            set
            {
                jhr1xm = value;
            }
        }
        /// <summary>
        /// //--�໤��2����*
        /// </summary>
        private string jhr2xm;
        public string JHR2XM
        {
            get
            {
                return jhr2xm;
            }
            set
            {
                jhr2xm = value;
            }
        }
        /// <summary>
        /// �໤��1���֤��*
        /// </summary>
        private string jhr1sfzh;   //--�໤��1���֤��*
        public string JHR1SFZH
        {
            get
            {
                return jhr1sfzh;
            }

            set
            {
                jhr1sfzh = value;
            }
        }
        /// <summary>
        /// //--�໤��1���֤��*
        /// </summary>
        private string jhr2sfzh;
        public string JHR2SFZH
        {
            get
            {
                return jhr2sfzh;
            }

            set
            {
                jhr2sfzh = value;
            }

        }
        /// <summary>
        /// ���ݺ�
        /// </summary>
        private string djh;
        public string DJH
        {
            get
            {

                return djh;
            }

            set
            {
                djh = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        private string cardpassword;
        public string CardPassWord
        {
            get
            {
                return cardpassword;
            }
            set
            {
                cardpassword = value;
            }

        }
        /// <summary>
        /// ҽ����Ա������
        /// </summary>
        private string siemployeecode;
        public string SiEmployeeCode
        {
            get
            {
                return siemployeecode;
            }
            set
            {
                siemployeecode = value;
            }
        }

        /// <summary>
        /// ҽ����Ա������
        /// </summary>
        private string siemployeename;
        public string SiEmployeeName
        {
            get
            {
                return siemployeename;
            }
            set
            {
                siemployeename = value;
            }
        }

        /// <summary>
        /// �����Ƿ��ϴ���־
        /// </summary>
        private string siUploadFeeFlag= "";
        public string SiUploadFeeFlag
        {
            get
            {
                return siUploadFeeFlag;
            }
            set
            {
                siUploadFeeFlag = value;
            }
        
        }
        #endregion

        #region �麣ҽ����������

        private string siType = "";
        /// <summary>
        /// �α�����
        /// 1 δ����ҽ��
        /// 2 ����ҽ��
        /// 3 ����ҽ��
        /// 4 ����ҽ��+����
        /// 5 ��ҽ��
        /// 6 ����ҽ��
        /// 7 ����ҽ��
        /// 8 ����ͳ��
        /// </summary>
        public string SiType
        {
            get { return this.siType; }
            set { this.siType = value; }
        }

        #endregion

        #region ����ҽ����������
        private bool isSIUploaded;
        /// <summary>
        ///{D3446DAF-E319-47a0-8BD5-EA748FCC4342}
        /// �Ƿ����ϴ�ҽ����ҳ��Ϣ
        /// </summary>
        public bool IsSIUploaded
        {
            get { return isSIUploaded; }
            set { isSIUploaded = value; }
        }


        #endregion

        #region ҽ������ 
        //{DC67634A-696F-4e03-8C63-447C4265817E}

        #region �㶫ʡͳһҽ����������
        /// <summary>
        /// bka911	String	10	��	��������	����
        /// </summary>
        private DateTime bka911;

        /// <summary>
        /// Bka911	String	10	��	��������	����
        /// </summary>
        public DateTime Bka911
        {
            get { return bka911; }
            set { bka911 = value; }
        }

        /// <summary>
        /// bka912	String	10	��	�������	����
        /// </summary>
        private string bka912 = string.Empty;

        /// <summary>
        /// Bka912	String	10	��	�������	����
        /// </summary>
        public string Bka912
        {
            get { return bka912; }
            set { bka912 = value; }
        }

        /// <summary>
        ///  amc050 string 10  ����ҵ������    ����
        /// </summary>
        private string amc050 = string.Empty;
        /// <summary>
        /// amc050 string 10  ����ҵ������    ����
        /// </summary>
        public string Amc050
        {
            get { return amc050; }
            set { amc050 = value; }
        }
        /// <summary>
        /// amc 029 string 10 �����������  ����
        /// </summary>
        private string amc029 = string.Empty;

        /// <summary>
        /// amc 029 string 10 �����������  ����
        /// </summary>
        public string Amc029
        {
            get { return amc029; }
            set { amc029 = value; }
        }
        /// <summary>
        /// amc 029 string 10 ����̥��  ����
        /// </summary>
        private string amc031 = string.Empty;

        /// <summary>
        /// amc 029 string 10 ����̥��  ����
        /// </summary>
        public string Amc031
        {
            get { return amc031; }
            set { amc031 = value; }
        }
        /// <summary>
        /// bka913	String	10	��	̥����	����
        /// </summary>
        private int bka913 = 0;

        /// <summary>
        /// Bka913	String	10	��	̥����	����
        /// </summary>
        public int Bka913
        {
            get { return bka913; }
            set { bka913 = value; }
        }

        /// <summary>
        /// bka914	String	10	��	ĸ�����	����
        /// </summary>
        private string bka914 = string.Empty;

        /// <summary>
        /// bka914	String	10	��	ĸ�����	����
        /// </summary>
        public string Bka914
        {
            get { return bka914; }
            set { bka914 = value; }
        }

        /// <summary>
        /// bka915	String	10	��	ĸ������ʱ��	��������ʽ��yyyyMMdd
        /// </summary>
        private DateTime bka915;

        /// <summary>
        /// Bka915	String	10	��	ĸ������ʱ��	��������ʽ��yyyyMMdd
        /// </summary>
        public DateTime Bka915
        {
            get { return bka915; }
            set { bka915 = value; }
        }

        /// <summary>
        /// bka916	String	10	��	Ӥ�����	����
        /// </summary>
        private string bka916 = string.Empty;

        /// <summary>
        /// Bka916	String	10	��	Ӥ�����	����
        /// </summary>
        public string Bka916
        {
            get { return bka916; }
            set { bka916 = value; }
        }

        /// <summary>
        /// bka917	String	10	��	Ӥ������ʱ��	��������ʽ��yyyyMMdd
        /// </summary>
        private DateTime bka917;

        /// <summary>
        /// Bka917	String	10	��	Ӥ������ʱ��	��������ʽ��yyyyMMdd
        /// </summary>
        public DateTime Bka917
        {
            get { return bka917; }
            set { bka917 = value; }
        }


        /// <summary>
        /// bka042	String	20	��������ƾ֤��	ֻ���ˡ�����ҵ�񣬲��д��� 
        /// </summary>
        private string bka042 = string.Empty;

        /// <summary>
        /// Bka042	String	20	��������ƾ֤��	ֻ���ˡ�����ҵ�񣬲��д��� 
        /// </summary>
        public string Bka042
        {
            get { return bka042; }
            set { bka042 = value; }
        }

        /// <summary>
        /// aaz267	String	12	����ѡ�㡢�����������к�	 
        /// </summary>
        private string aaz267 = string.Empty;

        /// <summary>
        /// Aaz267	String	12	����ѡ�㡢�����������к�	
        /// </summary>
        public string Aaz267
        {
            get { return aaz267; }
            set { aaz267 = value; }
        }

        /// <summary>
        /// bka825	String	12	ȫ�Էѷ���	
        /// </summary>
        private decimal bka825 = 0m;

        /// <summary>
        /// Bka825	String	12	ȫ�Էѷ���	
        /// </summary>
        public decimal Bka825
        {
            get { return bka825; }
            set { bka825 = value; }
        }

        /// <summary>
        /// bka826	String	12	�����Էѷ���
        /// </summary>
        private decimal bka826 = 0m;

        /// <summary>
        /// Bka826	String	12	�����Էѷ���
        /// </summary>
        public decimal Bka826
        {
            get { return bka826; }
            set { bka826 = value; }
        }

        /// <summary>
        /// aka151	String	12	���߷���
        /// </summary>
        private decimal aka151 = 0m;

        /// <summary>
        /// Aka151	String	12	���߷���
        /// </summary>
        public decimal Aka151
        {
            get { return aka151; }
            set { aka151 = value; }
        }

        /// <summary>
        /// bka838	String	12	�������η��ø����Ը�		
        /// </summary>
        private decimal bka838 = 0m;

        /// <summary>
        /// Bka838	String	12	�������η��ø����Ը�	
        /// </summary>
        public decimal Bka838
        {
            get { return bka838; }
            set { bka838 = value; }
        }

        /// <summary>
        /// akb067	String	12	�����ֽ�֧��
        /// </summary>
        private decimal akb067 = 0m;

        /// <summary>
        /// Akb067	String	12	�����ֽ�֧��	
        /// </summary>
        public decimal Akb067
        {
            get { return akb067; }
            set { akb067 = value; }
        }

        /// <summary>
        /// akb066	String	12	�����˻�֧��
        /// </summary>
        private decimal akb066 = 0m;

        /// <summary>
        /// Akb066	String	12	�����˻�֧��
        /// </summary>
        public decimal Akb066
        {
            get { return akb066; }
            set { akb066 = value; }
        }

        /// <summary>
        /// bka821	String	12	����������֧��	
        /// </summary>
        private decimal bka821 = 0m;

        /// <summary>
        /// Bka821	String	12	����������֧��	
        /// </summary>
        public decimal Bka821
        {
            get { return bka821; }
            set { bka821 = value; }
        }

        /// <summary>
        /// bka839	String	12	����֧��	
        /// </summary>
        private decimal bka839 = 0m;

        /// <summary>
        /// Bka839	String	12	����֧��	
        /// </summary>
        public decimal Bka839
        {
            get { return bka839; }
            set { bka839 = value; }
        }

        /// <summary>
        /// ake039	String	12	ҽ�Ʊ���ͳ�����֧��
        /// </summary>
        private decimal ake039 = 0m;

        /// <summary>
        /// Ake039	String	12	ҽ�Ʊ���ͳ�����֧��
        /// </summary>
        public decimal Ake039
        {
            get { return ake039; }
            set { ake039 = value; }
        }

        /// <summary>
        /// ake035	String	12	����Աҽ�Ʋ�������֧��
        /// </summary>
        private decimal ake035 = 0m;

        /// <summary>
        /// Ake035	String	12	����Աҽ�Ʋ�������֧��
        /// </summary>
        public decimal Ake035
        {
            get { return ake035; }
            set { ake035 = value; }
        }

        /// <summary>
        /// ake026	String	12	��ҵ����ҽ�Ʊ��ջ���֧��	
        /// </summary>
        private decimal ake026 = 0m;

        /// <summary>
        /// Ake026	String	12	��ҵ����ҽ�Ʊ��ջ���֧��	
        /// </summary>
        public decimal Ake026
        {
            get { return ake026; }
            set { ake026 = value; }
        }

        /// <summary>
        /// ake029	String	12	���ҽ�Ʒ��ò�������֧��
        /// </summary>
        private decimal ake029 = 0m;

        /// <summary>
        /// Ake029	String	12	���ҽ�Ʒ��ò�������֧��
        /// </summary>
        public decimal Ake029
        {
            get { return ake029; }
            set { ake029 = value; }
        }

        /// <summary>
        /// bka841	String	12	��λ֧��	
        /// </summary>
        private decimal bka841 = 0m;

        /// <summary>
        /// Bka841	String	12	��λ֧��
        /// </summary>
        public decimal Bka841
        {
            get { return bka841; }
            set { bka841 = value; }
        }

        /// <summary>
        /// bka842	String	12	ҽԺ�渶
        /// </summary>
        private decimal bka842 = 0m;

        /// <summary>
        /// Bka842	String	12	ҽԺ�渶
        /// </summary>
        public decimal Bka842
        {
            get { return bka842; }
            set { bka842 = value; }
        }

        /// <summary>
        /// bka840	String	12	��������֧��
        /// </summary>
        private decimal bka840 = 0m;

        /// <summary>
        /// Bka840	String	12	��������֧��
        /// </summary>
        public decimal Bka840
        {
            get { return bka840; }
            set { bka840 = value; }
        }

        /// <summary>
        /// aaa027	String	6	��	ҽ�������ı���
        /// </summary>
        private string aaa027 = string.Empty;

        /// <summary>
        /// aaa027	String	6	��	ҽ�������ı���
        /// </summary>
        public string Aaa027
        {
            get { return aaa027; }
            set { aaa027 = value; }
        }

        /// <summary>
        /// bka438	String	2	ҵ�񳡾��׶� 0��ҵ��δ��ʼ 	1��ҵ��ʼ2��ҵ�����3��ҵ����� (����Ԥ����) 
        /// ��סԺ�еķ���¼��ʱ���㣬��1���ڳ�Ժ�ǼǱ���ʱ���㣬��3���ڳ�Ժ����ʱ���㣬��2
        /// </summary>
        private string bka438 = string.Empty;

        /// <summary>
        /// bka438	String	2	ҵ�񳡾��׶� 0��ҵ��δ��ʼ 	1��ҵ��ʼ2��ҵ�����3��ҵ����� (����Ԥ����) 
        /// </summary>
        public string Bka438
        {
            get { return bka438; }
            set { bka438 = value; }
        }

        /// <summary>
        /// �α�������������������
        /// </summary>
        private string aab301 = string.Empty;

        /// <summary>
        /// �α�������������������
        /// </summary>
        public string Aab301
        {
            get { return aab301; }
            set { aab301 = value; }
        }

        /// <summary>
        /// ���ֱ���
        /// 310����"����ְ������ҽ��"
        /// 391����"����������ҽ��"
        /// 410����"����"
        /// 510����"����"
        /// </summary>
        private string aae140 = string.Empty;

        /// <summary>
        /// ���ֱ���
        /// 310����"����ְ������ҽ��"
        /// 391����"����������ҽ��"
        /// 410����"����"
        /// 510����"����"
        /// </summary>
        public string Aae140
        {
            get { return aae140; }
            set { aae140 = value; }
        }

        /// <summary>
        /// bka006	String	6	��	ҽ�ƴ�������
        /// </summary>
        private string bka006 = string.Empty;
        /// <summary>
        /// bka006	String	6	��	ҽ�ƴ�������
        /// </summary>
        public string Bka006
        {
            get { return bka006; }
            set { bka006 = value; }
        }
        /// <summary>
        /// aka130	String	6	��	ҽ��ҵ������
        /// </summary>
        private string aka130 = string.Empty;
        /// <summary>
        /// aka130	String	ҽ��ҵ������ 11���� 12סԺ 13���� 16���� 41�������� 42����סԺ 51�������� 52����סԺ
        /// </summary>
        public string Aka130
        {
            get { return aka130; }
            set { aka130 = value; }
        }
        private string bka020 = string.Empty;
        /// <summary>
        /// bka020	String	 �����������
        /// </summary>
        public string Bka020
        {
            get { return bka020; }
            set { bka020 = value; }
        }
        private string bka004 = string.Empty;
        /// <summary>
        /// bka004	String	 ��Ա���
        /// </summary>
        public string Bka004
        {
            get { return bka004; }
            set { bka004 = value; }
        }

        /// <summary>
        /// �Ƿ��ȡ�籣��
        /// </summary>
        private bool isUserSICard = true;
        /// <summary>
        /// �Ƿ��ȡ�籣��,Ĭ������Ҫ����������ʡ�������Щ����û��������Ϣ��Ҫ�����໼�߲���Ҫ����У��
        /// </summary>
        public bool IsUserSICard
        {
            get { return isUserSICard; }
            set { isUserSICard = value; }
        }

        /// <summary>
        /// �����ٻ��Ƿ�Ҫȡ��ҽ���������������
        /// </summary>
        private bool isCancelSIBanlance = true;
        /// <summary>
        /// �����ٻ��Ƿ�Ҫȡ��ҽ���������������
        /// </summary>
        public bool IsCancelSIBanlance
        {
            get { return isCancelSIBanlance; }
            set { isCancelSIBanlance = value; }
        }

        /// <summary>
        /// bka841	String	12	ҽ��֧�����	
        /// </summary>
        private decimal bka811 = 0m;

        /// <summary>
        /// Bka841	String	12	ҽ��֧�����
        /// </summary>
        public decimal Bka811
        {
            get { return bka811; }
            set { bka811 = value; }
        }

        /// <summary>
        /// bka841	String	12	����֧�����	
        /// </summary>
        private decimal bka812 = 0m;

        /// <summary>
        /// Bka841	String	12	����֧�����
        /// </summary>
        public decimal Bka812
        {
            get { return bka812; }
            set { bka812 = value; }
        }

        private string bkp969 = string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        public string Bkp969
        {
            get { return bkp969; }
            set { bkp969 = value; }
        }

        private string bkp979 = string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        public string Bkp979
        {
            get { return bkp979; }
            set { bkp979 = value; }
        }
        #endregion �㶫ʡͳһҽ����������

        #region ����ҽ��API(��)
        /// <summary>
        /// ����ID
        /// </summary>
        private string mdtrt_id = string.Empty;

        /// <summary>
        /// ����ID 
        /// </summary>
        public string Mdtrt_id
        {
            get { return mdtrt_id; }
            set { mdtrt_id = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        private string setl_id = string.Empty;

        /// <summary>
        /// ����ID
        /// </summary>
        public string Setl_id
        {
            get { return setl_id; }
            set { setl_id = value; }
        }

        /// <summary>
        /// ��Ա���
        /// </summary>
        private string psn_no = string.Empty;

        /// <summary>
        /// ��Ա���
        /// </summary>
        public string Psn_no
        {
            get { return psn_no; }
            set { psn_no = value; }
        }

        /// <summary>
        /// ��Ա����
        /// </summary>
        private string psn_name = string.Empty;

        /// <summary>
        /// ��Ա����
        /// </summary>
        public string Psn_name
        {
            get { return psn_name; }
            set { psn_name = value; }
        }

        /// <summary>
        /// ��Ա֤������
        /// </summary>
        private string psn_cert_type = string.Empty;

        /// <summary>
        /// ��Ա֤������
        /// </summary>
        public string Psn_cert_type
        {
            get { return psn_cert_type; }
            set { psn_cert_type = value; }
        }

        /// <summary>
        /// ֤������
        /// </summary>
        private string certno = string.Empty;

        /// <summary>
        /// ֤������
        /// </summary>
        public string Certno
        {
            get { return certno; }
            set { certno = value; }
        }

        /// <summary>
        /// �Ա�
        /// </summary>
        private string gend = string.Empty;

        /// <summary>
        /// �Ա�
        /// </summary>
        public string Gend
        {
            get { return gend; }
            set { gend = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        private string naty = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        public string Naty
        {
            get { return naty; }
            set { naty = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime brdy;

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime Brdy
        {
            get { return brdy; }
            set { brdy = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        private string age = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string insutype = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        public string Insutype
        {
            get { return insutype; }
            set { insutype = value; }
        }


        /// <summary>
        /// ��������
        /// </summary>
        private string insuplc_admdvs = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        public string Insuplc_admdvs
        {
            get { return insuplc_admdvs; }
            set { insuplc_admdvs = value; }
        }

        /// <summary>
        /// ��Ա���
        /// </summary>
        private string psn_type = string.Empty;

        /// <summary>
        /// ��Ա���
        /// </summary>
        public string Psn_type
        {
            get { return psn_type; }
            set { psn_type = value; }
        }

        /// <summary>
        /// ����Ա��־
        /// </summary>
        private string cvlserv_flag = string.Empty;

        /// <summary>
        /// ����Ա��־
        /// </summary>
        public string Cvlserv_flag
        {
            get { return cvlserv_flag; }
            set { cvlserv_flag = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime setl_time;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime Setl_time
        {
            get { return setl_time; }
            set { setl_time = value; }
        }

        /// <summary>
        /// ���˽��㷽ʽ
        /// </summary>
        private string psn_setlway = string.Empty;

        /// <summary>
        /// ���˽��㷽ʽ
        /// </summary>
        public string Psn_setlway
        {
            get { return psn_setlway; }
            set { psn_setlway = value; }
        }

        /// <summary>
        /// ����ƾ֤����
        /// </summary>
        private string mdtrt_cert_type = string.Empty;

        /// <summary>
        /// ����ƾ֤����
        /// </summary>
        public string Mdtrt_cert_type
        {
            get { return mdtrt_cert_type; }
            set { mdtrt_cert_type = value; }
        }

        /// <summary>
        /// ҽ�����
        /// </summary>
        private string med_type = string.Empty;

        /// <summary>
        /// ҽ�����
        /// </summary>
        public string Med_type
        {
            get { return med_type; }
            set { med_type = value; }
        }

        /// <summary>
        /// ���ֱ���
        /// </summary>
        private string dise_code = string.Empty;

        /// <summary>
        /// ���ֱ���
        /// </summary>
        public string Dise_code
        {
            get { return dise_code; }
            set { dise_code = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string dise_name = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        public string Dise_name
        {
            get { return dise_name; }
            set { dise_name = value; }
        }

        /// <summary>
        /// ҽ�Ʒ��ܶ�
        /// </summary>
        private decimal medfee_sumamt = 0m;

        /// <summary>
        /// ҽ�Ʒ��ܶ�
        /// </summary>
        public decimal Medfee_sumamt
        {
            get { return medfee_sumamt; }
            set { medfee_sumamt = value; }
        }

        /// <summary>
        /// ȫ�Էѽ��
        /// </summary>
        private decimal ownpay_amt = 0m;

        /// <summary>
        /// ȫ�Էѽ��
        /// </summary>
        public decimal Ownpay_amt
        {
            get { return ownpay_amt; }
            set { ownpay_amt = value; }
        }

        /// <summary>
        /// ���޼��Էѷ���
        /// </summary>
        private decimal overlmt_selfpay = 0m;

        /// <summary>
        /// ���޼��Էѷ���
        /// </summary>
        public decimal Overlmt_selfpay
        {
            get { return overlmt_selfpay; }
            set { overlmt_selfpay = value; }
        }

        /// <summary>
        /// �����Ը����
        /// </summary>
        private decimal preselfpay_amt = 0m;

        /// <summary>
        /// �����Ը����
        /// </summary>
        public decimal Preselfpay_amt
        {
            get { return preselfpay_amt; }
            set { preselfpay_amt = value; }
        }

        /// <summary>
        /// ���Ϸ�Χ���
        /// </summary>
        private decimal inscp_scp_amt = 0m;

        /// <summary>
        /// ���Ϸ�Χ���
        /// </summary>
        public decimal Inscp_scp_amt
        {
            get { return inscp_scp_amt; }
            set { inscp_scp_amt = value; }
        }

        /// <summary>
        /// ҽ���Ͽɷ����ܶ�
        /// </summary>
        private decimal med_sumfee = 0m;

        /// <summary>
        /// ҽ���Ͽɷ����ܶ�
        /// </summary>
        public decimal Med_sumfee
        {
            get { return med_sumfee; }
            set { med_sumfee = value; }
        }

        /// <summary>
        /// ʵ��֧������
        /// </summary>
        private decimal act_pay_dedc = 0m;

        /// <summary>
        /// ʵ��֧������
        /// </summary>
        public decimal Act_pay_dedc
        {
            get { return act_pay_dedc; }
            set { act_pay_dedc = value; }
        }

        /// <summary>
        /// ����ҽ�Ʊ���ͳ�����֧��
        /// </summary>
        private decimal hifp_pay = 0m;

        /// <summary>
        /// ����ҽ�Ʊ���ͳ�����֧��
        /// </summary>
        public decimal Hifp_pay
        {
            get { return hifp_pay; }
            set { hifp_pay = value; }
        }

        /// <summary>
        /// ����ҽ�Ʊ���ͳ�����֧������
        /// </summary>
        private decimal pool_prop_selfpay = 0m;

        /// <summary>
        /// ����ҽ�Ʊ���ͳ�����֧������
        /// </summary>
        public decimal Pool_prop_selfpay
        {
            get { return pool_prop_selfpay; }
            set { pool_prop_selfpay = value; }
        }

        /// <summary>
        /// ����Աҽ�Ʋ����ʽ�֧��
        /// </summary>
        private decimal cvlserv_pay = 0m;

        /// <summary>
        /// ����Աҽ�Ʋ����ʽ�֧��
        /// </summary>
        public decimal Cvlserv_pay
        {
            get { return cvlserv_pay; }
            set { cvlserv_pay = value; }
        }

        /// <summary>
        /// ��ҵ����ҽ�Ʊ��ջ���֧��
        /// </summary>
        private decimal hifes_pay = 0m;

        /// <summary>
        /// ��ҵ����ҽ�Ʊ��ջ���֧��
        /// </summary>
        public decimal Hifes_pay
        {
            get { return hifes_pay; }
            set { hifes_pay = value; }
        }

        /// <summary>
        /// ����󲡱����ʽ�֧��
        /// </summary>
        private decimal hifmi_pay = 0m;

        /// <summary>
        /// ����󲡱����ʽ�֧��
        /// </summary>
        public decimal Hifmi_pay
        {
            get { return hifmi_pay; }
            set { hifmi_pay = value; }
        }

        /// <summary>
        /// ְ�����ҽ�Ʒ��ò�������֧��
        /// </summary>
        private decimal hifob_pay = 0m;

        /// <summary>
        /// ְ�����ҽ�Ʒ��ò�������֧��
        /// </summary>
        public decimal Hifob_pay
        {
            get { return hifob_pay; }
            set { hifob_pay = value; }
        }

        /// <summary>
        /// �˲���Աҽ�Ʊ��ϻ���֧��
        /// </summary>
        private decimal hifdm_pay = 0m;

        /// <summary>
        /// �˲���Աҽ�Ʊ��ϻ���֧��
        /// </summary>
        public decimal Hifdm_pay
        {
            get { return hifdm_pay; }
            set { hifdm_pay = value; }
        }

        /// <summary>
        /// ҽ�ƾ�������֧��
        /// </summary>
        private decimal maf_pay = 0m;

        /// <summary>
        /// ҽ�ƾ�������֧��
        /// </summary>
        public decimal Maf_pay
        {
            get { return maf_pay; }
            set { maf_pay = value; }
        }

        /// <summary>
        /// ��������֧��
        /// </summary>
        private decimal oth_pay = 0m;

        /// <summary>
        /// ��������֧��
        /// </summary>
        public decimal Oth_pay
        {
            get { return oth_pay; }
            set { oth_pay = value; }
        }
        /// <summary>
        /// ����֧���ܶ�
        /// </summary>
        private decimal fund_pay_sumamt = 0m;

        /// <summary>
        /// ����֧���ܶ�
        /// </summary>
        public decimal Fund_pay_sumamt
        {
            get { return fund_pay_sumamt; }
            set { fund_pay_sumamt = value; }
        }
        /// <summary>
        /// ҽԺ�������
        /// </summary>
        private decimal hosp_part_amt = 0m;

        /// <summary>
        /// ҽԺ�������
        /// </summary>
        public decimal Hosp_part_amt
        {
            get { return hosp_part_amt; }
            set { hosp_part_amt = value; }
        }
        /// <summary>
        /// ���˸����ܽ��
        /// </summary>
        private decimal psn_part_am = 0m;

        /// <summary>
        /// ���˸����ܽ��
        /// </summary>
        public decimal Psn_part_am
        {
            get { return psn_part_am; }
            set { psn_part_am = value; }
        }
        /// <summary>
        /// �����˻�֧��
        /// </summary>
        private decimal acct_pay = 0m;

        /// <summary>
        /// �����˻�֧��
        /// </summary>
        public decimal Acct_pay
        {
            get { return acct_pay; }
            set { acct_pay = value; }
        }
        /// <summary>
        /// �ֽ�֧�����
        /// </summary>
        private decimal cash_payamt = 0m;

        /// <summary>
        /// �ֽ�֧�����
        /// </summary>
        public decimal Cash_payamt
        {
            get { return cash_payamt; }
            set { cash_payamt = value; }
        }
        /// <summary>
        /// �˻�����֧�����
        /// </summary>
        private decimal acct_mulaid_pay = 0m;

        /// <summary>
        /// �˻�����֧�����
        /// </summary>
        public decimal Acct_mulaid_pay
        {
            get { return acct_mulaid_pay; }
            set { acct_mulaid_pay = value; }
        }
        /// <summary>
        /// �����˻�֧�������
        /// </summary>
        private decimal balc = 0m;

        /// <summary>
        /// �����˻�֧�������
        /// </summary>
        public decimal Balc
        {
            get { return balc; }
            set { balc = value; }
        }
        /// <summary>
        /// ���㾭�����
        /// </summary>
        private string clr_optins = string.Empty;

        /// <summary>
        /// ���㾭�����
        /// </summary>
        public string Clr_optins
        {
            get { return clr_optins; }
            set { clr_optins = value; }
        }
        /// <summary>
        /// ���㷽ʽ
        /// </summary>
        private string clr_way = string.Empty;

        /// <summary>
        /// ���㷽ʽ
        /// </summary>
        public string Clr_way
        {
            get { return clr_way; }
            set { clr_way = value; }
        }
        /// <summary>
        /// �������
        /// </summary>
        private string clr_type = string.Empty;

        /// <summary>
        /// �������
        /// </summary>
        public string Clr_type
        {
            get { return clr_type; }
            set { clr_type = value; }
        }
        /// <summary>
        /// ҽҩ��������ID
        /// </summary>
        private string medins_setl_id = string.Empty;

        /// <summary>
        /// ҽҩ��������ID
        /// </summary>
        public string Medins_setl_id
        {
            get { return medins_setl_id; }
            set { medins_setl_id = value; }
        }

        /// <summary>
        /// Υ������
        /// </summary>
        private string vola_type = string.Empty;

        /// <summary>
        /// Υ������
        /// </summary>
        public string Vola_type
        {
            get { return vola_type; }
            set { vola_type = value; }
        }

        /// <summary>
        /// Υ��˵��
        /// </summary>
        private string vola_dscr = string.Empty;

        /// <summary>
        /// Υ��˵��
        /// </summary>
        public string Vola_dscr
        {
            get { return vola_dscr; }
            set { vola_dscr = value; }
        }

        /// <summary>
        /// ���˽��
        /// </summary>
        private string stmt_relt = string.Empty;

        /// <summary>
        /// ���˽��
        /// </summary>
        public string Stmt_relt
        {
            get { return stmt_relt; }
            set { stmt_relt = value; }
        }


        /// <summary>
        /// ���㾭�����
        /// </summary>
        private string setl_options = string.Empty;

        /// <summary>
        /// ���㾭�����
        /// </summary>
        public string Setl_options
        {
            get { return setl_options; }
            set { setl_options = value; }
        }


        /// <summary>
        /// ���˽��˵��
        /// </summary>
        private string stmt_rslt_dscr = string.Empty;

        /// <summary>
        /// ���˽��˵��
        /// </summary>
        public string Stmt_rslt_dscr
        {
            get { return stmt_rslt_dscr; }
            set { stmt_rslt_dscr = value; }
        }


        /// <summary>
        /// ����״̬
        /// </summary>
        private string stmt_state = string.Empty;

        /// <summary>
        /// ����״̬
        /// </summary>
        public string Stmt_state
        {
            get { return stmt_state; }
            set { stmt_state = value; }
        }



        /// <summary>
        /// �������ص��籣��������Ϣ
        /// </summary>
        private string hcard_basinfo = string.Empty;

        /// <summary>
        /// �������ص��籣��������Ϣ
        /// </summary>
        public string Hcard_basinfo
        {
            get { return hcard_basinfo; }
            set { hcard_basinfo = value; }
        }

        /// <summary>
        /// �ֿ�����Ǽ���ɺ�
        /// </summary>
        private string hcard_chkinfo = string.Empty;

        /// <summary>
        /// �ֿ�����Ǽ���ɺ�
        /// </summary>
        public string Hcard_chkinfo
        {
            get { return hcard_chkinfo; }
            set { hcard_chkinfo = value; }
        }

        private string fetus_cnt = string.Empty;
        /// <summary>
        /// ̥����
        /// </summary>
        public string Fetus_cnt
        {
            get { return fetus_cnt; }
            set { fetus_cnt = value; }
        }

        private string birctrl_matn_date = string.Empty;
        /// <summary>
        /// �������������
        /// </summary>
        public string Birctrl_matn_date
        {
            get { return birctrl_matn_date; }
            set { birctrl_matn_date = value; }
        }

        private string repeat_ipt_flag = string.Empty;

        #region // {3CCAB886-9648-4DE8-B5DC-276D9212353F}
        /// <summary>
        /// �ظ���Ժ��־
        /// </summary>
        public string Repeat_ipt_flag
        {
            get { return repeat_ipt_flag; }
            set { repeat_ipt_flag = value; }
        }

        private string ttp_resp = string.Empty;
        /// <summary>
        /// �ϲ������־
        /// </summary>
        public string Ttp_resp
        {
            get { return ttp_resp; }
            set { ttp_resp = value; }
        }

        private string merg_setl_flag = string.Empty;
        /// <summary>
        /// �Ƿ����������
        /// </summary>
        public string Merg_setl_flag
        {
            get { return merg_setl_flag; }
            set { merg_setl_flag = value; }
        }
        #endregion
        #endregion ����ҽ��API(��)

        #endregion
    }
}
