using System;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Account;
using System.Collections.Generic;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// Register<br></br>
    /// [��������: �Һ���Ϣʵ��]<br></br>
    /// [�� �� ��: ��С��]<br></br>
    /// [����ʱ��: 2007-2-1]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='��˹'
    ///		�޸�ʱ��='2007-03-8'
    ///		�޸�Ŀ��='�ۺ����ӻ����Ż����ʵ��'
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// </summary>
    /// <�޸ļ�¼
    ///		�޸���='��ѩ��'
    ///		�޸�ʱ��='2007-10-22'
    ///		�޸�Ŀ��='�ۺϻ��߷�����'
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class Register : Patient
    {
        /// <summary>
        /// 
        /// </summary>
        public Register()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            // 
        }

        #region ����

        /// <summary>
        /// ֤������
        /// </summary>
        private FS.FrameWork.Models.NeuObject cardType;

        /// <summary>
        /// ������Դ
        /// </summary>
        private FS.FrameWork.Models.NeuObject inSource;

        /// <summary>
        /// ��������
        /// </summary>
        private TransTypes tranType = TransTypes.Positive;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Schema doctor;

        /// <summary>
        /// ÿ����ˮ��
        /// </summary>
        private int orderNO = 0;

        /// <summary>
        /// �Һŷ�
        /// </summary>
        private RegLvlFee regLvlFee;

        /// <summary>
        /// �Է�
        /// </summary>
        private decimal ownCost;
        /// <summary>
        /// �Ը�
        /// </summary>
        private decimal payCost;
        /// <summary>
        /// ����
        /// </summary>
        private decimal pubCost;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        private bool isEmergency;

        /// <summary>
        /// �Ƿ��շ�
        /// </summary>
        private bool isFee;

        /// <summary>
        /// �Һ����
        /// </summary>
        private EnumRegType regType = EnumRegType.Reg;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        private bool isSee = false;

        /// <summary>
        /// �Һ�״̬
        /// </summary>
        private EnumRegisterStatus status = EnumRegisterStatus.Valid;

        /// <summary>
        /// ��Ʊ��/������
        /// </summary>
        private string invoiceNO;
        /// <summary>
        /// ������ by niuxinyuan  2007-05-15
        /// </summary>
        private string recipeNO;

        /// <summary>
        /// �Ѵ�ӡ��Ʊ����
        /// </summary>
        private int printInvoiceCnt = 0;

        /// <summary>
        /// ¼����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment inputOper;

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment cancelOper;

        /// <summary>
        /// �ս��������Ϣ
        /// </summary>
        private OperStat balanceOperStat;

        /// <summary>
        /// �˲��ս������Ϣ
        /// </summary>
        private OperStat checkOperStat;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isTriage = false;

        /// <summary>
        /// �������Ա��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment triageOper;

        /// <summary>
        /// ���ﻼ���Ż����
        /// </summary>
        private FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate;
        ///// <summary>
        /////���򲡰���ȥ���ݲ������
        ///// </summary>
        //private bool isSendInhosCase;

        /// <summary>
        /// ���߷�����
        /// </summary>
        private FS.HISFC.Models.RADT.PVisit pVisit;

        /// <summary>
        /// �Żݽ��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        private decimal ecoCost = 0m;

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// �Ƿ����˻����̹Һ�
        /// </summary>
        private bool isAccount;

        /// <summary>
        /// ��չ�ֶ�1{921FBFCA-3D0D-4bc6-8EEA-A9BBE152E69A}
        /// </summary>
        private string mark1;


        /// <summary>
        /// ҽ��������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment seeDoct;

        /// <summary>
        /// �ϴ���־
        /// </summary>
        private string upFlag;

        private List<AccountCardFee> lstCardFee;

        private RegisterExtend regExtend;


        /// <summary>
        /// Ժ��id{3515892E-1541-47de-8E0B-E306798A358C}
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// Ժ����
        /// </summary>
        private string hospital_name;


        #endregion

        #region ����

        /// <summary>
        /// ԤԼ�Һŵ�ҵ�񵥺�
        /// </summary>
        private string bookNo = string.Empty;

        /// <summary>
        /// ԤԼ�Һŵ�ҵ�񵥺�
        /// </summary>
        public string BookNo
        {
            get { return bookNo; }
            set { bookNo = value; }
        }

        /// <summary>
        /// ���ﻼ���Ż����
        /// </summary>
        public FS.HISFC.Models.Fee.Outpatient.EcoRate EcoRate
        {
            get
            {
                if (this.ecoRate == null)
                {
                    this.ecoRate = new FS.HISFC.Models.Fee.Outpatient.EcoRate();
                }
                return this.ecoRate;
            }
            set
            {
                this.ecoRate = value;
            }
        }

        /// <summary>
        /// ֤������
        /// </summary>
        [Obsolete("CardType�Ѿ����ϣ�֤��������ʹ��IDCardType", false)]
        public FS.FrameWork.Models.NeuObject CardType
        {
            get
            {
                if (this.cardType == null)
                {
                    this.cardType = new FS.FrameWork.Models.NeuObject();
                }

                return this.cardType;
            }
            set
            {
                base.IDCardType = value;
                this.cardType = value;
            }
        }

        /// <summary>
        /// ������Դ
        /// </summary>
        public FS.FrameWork.Models.NeuObject InSource
        {
            get
            {
                if (this.inSource == null)
                {
                    this.inSource = new FS.FrameWork.Models.NeuObject();
                }
                return this.inSource;
            }
            set { this.inSource = value; }
        }

        ///<summary>
        ///��������
        ///</summary>
        public TransTypes TranType
        {
            get { return tranType; }
            set { tranType = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Schema DoctorInfo
        {
            get
            {
                if (this.doctor == null)
                {
                    this.doctor = new Schema();
                }
                return this.doctor;
            }
            set { this.doctor = value; }
        }

        /// <summary>
        /// ÿ����ˮ��
        /// </summary>
        public int OrderNO
        {
            get { return orderNO; }
            set { orderNO = value; }
        }

        /// <summary>
        /// �Һŷ�
        /// </summary>
        public RegLvlFee RegLvlFee
        {
            get
            {
                if (this.regLvlFee == null)
                {
                    this.regLvlFee = new RegLvlFee();
                }
                return regLvlFee;
            }
            set { regLvlFee = value; }
        }

        /// <summary>
        /// �Է�
        /// </summary>
        public decimal OwnCost
        {
            get { return this.ownCost; }
            set { this.ownCost = value; }
        }

        /// <summary>
        /// �Ը�
        /// </summary>
        public decimal PayCost
        {
            get { return this.payCost; }
            set { this.payCost = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal PubCost
        {
            get { return this.pubCost; }
            set { this.pubCost = value; }
        }

        /// <summary>
        /// �Ƿ���
        /// </summary>
        /// {156C449B-60A9-4536-B4FB-D00BC6F476A1}
        [Obsolete("����Ϊ��DoctorInfo.Templet.RegLevel.IsEmergency", true)]
        public bool IsEmergency
        {
            get { return isEmergency; }
            set { isEmergency = value; }
        }

        /// <summary>
        /// �Ƿ��շ�
        /// </summary>
        public bool IsFee
        {
            get { return isFee; }
            set { isFee = value; }
        }

        /// <summary>
        /// �Һ����
        /// </summary>
        public EnumRegType RegType
        {
            get { return this.regType; }
            set { this.regType = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsFirst
        {
            get { return isFirst; }
            set { isFirst = value; }
        }

        /// <summary>
        /// �Ƿ���
        /// </summary>
        public bool IsSee
        {
            get { return isSee; }
            set { isSee = value; }
        }

        /// <summary>
        /// �Һ�״̬
        /// </summary>
        public EnumRegisterStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        /// <summary>
        /// ��Ʊ��/������
        /// </summary>
        public string InvoiceNO
        {
            get
            {
                if (this.invoiceNO == null)
                {
                    this.invoiceNO = string.Empty;
                }
                return this.invoiceNO;
            }
            set { this.invoiceNO = value; }
        }

        /// <summary>
        /// ��Ʊ��/������
        /// </summary>
        public string RecipeNO
        {
            get
            {
                if (this.recipeNO == null)
                {
                    this.recipeNO = string.Empty;
                }
                return this.recipeNO;
            }
            set { this.recipeNO = value; }
        }

        /// <summary>
        /// �Ѵ�ӡ��Ʊ����
        /// </summary>
        public int PrintInvoiceCnt
        {
            get { return this.printInvoiceCnt; }
            set { this.printInvoiceCnt = value; }
        }

        /// <summary>
        /// �ҺŲ���Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment InputOper
        {
            get
            {
                if (this.inputOper == null)
                {
                    this.inputOper = new OperEnvironment();
                }

                return inputOper;
            }
            set { inputOper = value; }
        }

        /// <summary>
        /// ���ϲ���Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CancelOper
        {
            get
            {
                if (this.cancelOper == null)
                {
                    this.cancelOper = new OperEnvironment();
                }
                return cancelOper;
            }
            set { cancelOper = value; }
        }

        /// <summary>
        /// �ս�˲������Ϣ
        /// </summary>
        public OperStat CheckOperStat
        {
            get
            {
                if (this.checkOperStat == null)
                {
                    this.checkOperStat = new OperStat();
                }
                return checkOperStat;
            }
            set { checkOperStat = value; }
        }

        /// <summary>
        /// �ս��������Ϣ
        /// </summary>
        public OperStat BalanceOperStat
        {
            get
            {
                if (this.balanceOperStat == null)
                {
                    this.balanceOperStat = new OperStat();
                }
                return balanceOperStat;
            }
            set { balanceOperStat = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsTriage
        {
            get { return this.isTriage; }
            set { this.isTriage = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment TriageOper
        {
            get
            {
                if (this.triageOper == null)
                {
                    this.triageOper = new OperEnvironment();
                }
                return this.triageOper;
            }
            set { this.triageOper = value; }
        }

        //----------------------------------------------------------���������������

        /// <summary>
        /// ҽ�����
        /// </summary>        
        public string MedicalType = "";

        /// <summary>
        /// �������, ���塢����
        /// </summary>        
        public string ChkKind = "";

        /// <summary>
        /// ҽ���Ǽ���Ϣ
        /// </summary>
        private FS.HISFC.Models.SIInterface.SIMainInfo siInfo;

        /// <summary>
        /// ҽ���Ǽ���Ϣ
        /// </summary>
        public FS.HISFC.Models.SIInterface.SIMainInfo SIMainInfo
        {
            get
            {
                if (siInfo == null)
                {
                    this.siInfo = new FS.HISFC.Models.SIInterface.SIMainInfo();
                }
                return siInfo;
            }
            set
            {
                siInfo = value;
            }
        }

        /// <summary>
        /// ����ҽ����Ϣ������ҽ��������ʱ�䡢����ҽ������
        /// </summary>
        public OperEnvironment SeeDoct
        {
            get
            {
                if (this.seeDoct == null)
                {
                    this.seeDoct = new OperEnvironment();
                }
                return this.seeDoct;
            }
            set
            {
                this.seeDoct = value;
            }
        }
        /// <summary>
        /// ���߷�����
        /// </summary>
        public FS.HISFC.Models.RADT.PVisit PVisit
        {
            get
            {
                if (this.pVisit == null)
                {
                    this.pVisit = new PVisit();
                }
                return pVisit;
            }
            set { pVisit = value; }
        }



        /// <summary>
        /// �Żݽ��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        public decimal EcoCost
        {
            get { return ecoCost; }
            set { ecoCost = value; }
        }

        ////{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// �Ƿ����˻����̹Һ�
        /// </summary>
        public bool IsAccount
        {
            get
            {
                return this.isAccount;
            }
            set
            {
                isAccount = value;

            }
        }

        /// <summary>
        /// ��չ�ֶ�1{921FBFCA-3D0D-4bc6-8EEA-A9BBE152E69A}
        /// </summary>
        public string Mark1
        {
            get { return mark1; }
            set { mark1 = value; }
        }

        //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
        /// <summary>
        /// һ������
        /// </summary>
        private string class1desease;

        public string Class1Desease
        {
            get { return this.class1desease; }
            set { this.class1desease = value; }
        }

        //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
        /// <summary>
        /// ��������
        /// </summary>
        private string class2desease;

        public string Class2Desease
        {
            get { return this.class2desease; }
            set { this.class2desease = value; }
        }

        /// <summary>
        /// �ϴ���־ Ӧ�����ϴ�����ʽ��
        /// </summary>
        public string UpFlag
        {
            get
            {
                if (this.upFlag == null)
                {
                    this.upFlag = "0";
                }
                return upFlag;
            }
            set
            {
                upFlag = value;
            }
        }
        /// <summary>
        /// �˹Һż�¼��ط�����Ϣ
        /// </summary>
        public List<AccountCardFee> LstCardFee
        {
            get
            {
                if (this.lstCardFee == null)
                {
                    this.lstCardFee = new List<AccountCardFee>();
                }
                return this.lstCardFee;
            }
            set { this.lstCardFee = value; }
        }

        public RegisterExtend RegExtend
        {
            get
            {
                return regExtend;
            }
            set
            {
                if (regExtend == null)
                {
                    regExtend = new RegisterExtend();
                }
                regExtend = value;
            }
        }

        // //{AE399953-4F87-4199-8060-EFDC16AFAAF3} �ۺ��������ʵ��ҽ��
        /// <summary>
        /// ʵ�ʹҺ�ҽ������
        /// </summary>
        public string RealDoctorName { set; get; }

        /// <summary>
        /// ʵ�ʹҺ�ҽ��id
        /// </summary>
        public string RealDoctorID { set; get; }


        /// <summary>
        /// Ժ��id{3515892E-1541-47de-8E0B-E306798A358C}
        /// </summary>
        public string Hospital_id
        {
            get { return this.hospital_id; }
            set { this.hospital_id = value; }
        }


        /// <summary>
        /// Ժ����
        /// </summary>
        public string Hospital_name
        {
            get { return this.hospital_name; }
            set { this.hospital_name = value; }
        }




        #region
        //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 
        //����Ժ�������Ƽ����ҽ����������

        /// <summary>
        /// Ժ������
        /// </summary>
        private string hospitalFirstVisit = "0";

        public string HospitalFirstVisit
        {
            get { return hospitalFirstVisit; }
            set { hospitalFirstVisit = value; }
        }

        /// <summary>
        /// ��Ƽ�����
        /// </summary>
        private string rootDeptFirstVisit = "0";

        public string RootDeptFirstVisit
        {
            get { return rootDeptFirstVisit; }
            set { rootDeptFirstVisit = value; }
        }

        /// <summary>
        /// ҽ��������
        /// </summary>
        private string doctFirstVist = "0";

        public string DoctFirstVist
        {
            get { return doctFirstVist; }
            set { doctFirstVist = value; }
        }

        #endregion

        #region CRM������Ϣ
        /// <summary>
        /// �����ʶ 0-δ���� 1-����
        /// </summary>
        public string AssignFlag { get; set; }
        /// <summary>
        /// ����״̬ 0-δ���� 1-���� 2-���� 3-��� 4-����
        /// </summary>
        public string AssignStatus { get; set; }
        /// <summary>
        /// �����ʶ1-�� 0-��
        /// </summary>
        public string FirstSeeFlag { get; set; }
        /// <summary>
        /// ���ȱ�ʶ1-�� 0-��
        /// </summary>
        public string PreferentialFlag { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public int SequenceNO { get; set; }
        #endregion

        #endregion

        #region ����
        ///// <summary>
        /////  �Һŵĸ���
        ///// </summary>
        ///// <returns></returns>
        public new Register Clone()
        {
            Register reg = base.Clone() as Register;
            if (this.cardType != null)
            {
                reg.cardType = this.CardType.Clone();
            }
            if (this.inSource != null)
            {
                reg.InSource = this.inSource.Clone();
            }
            if (this.doctor != null)
            {
                reg.DoctorInfo = this.doctor.Clone();
            }
            if (this.regLvlFee != null)
            {
                reg.regLvlFee = this.regLvlFee.Clone();
            }

            if (this.inputOper != null)
            {
                reg.InputOper = this.inputOper.Clone();
            }

            if (this.cancelOper != null)
            {
                reg.CancelOper = this.cancelOper.Clone();
            }
            if (this.balanceOperStat != null)
            {
                reg.BalanceOperStat = this.balanceOperStat.Clone();
            }
            if (this.checkOperStat != null)
            {
                reg.CheckOperStat = this.checkOperStat.Clone();
            }
            if (this.triageOper != null)
            {
                reg.TriageOper = this.triageOper.Clone();
            }
            if (this.pVisit != null)
            {
                reg.PVisit = this.pVisit.Clone();
            }
            if (this.ecoRate != null)
            {
                reg.ecoRate = this.ecoRate.Clone();
            }
            if (this.regExtend != null)
            {
                reg.RegExtend = this.regExtend.Clone();
            }
            return reg;
        }
        #endregion

        #region  ����

        /// <summary>
        /// ������
        /// </summary>
        [Obsolete("����Ϊ��PID.CardNO", true)]
        public string CardNo;

        /// <summary>
        /// ���֤��
        /// </summary>
        [Obsolete("����Ϊ:IDCard", true)]
        public string IdenNo;

        /// <summary>
        /// �Ա����
        /// </summary>
        [Obsolete("����Ϊ��Sex.ID", true)]
        public string SexID;

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        [Obsolete("����Ϊ��PhoneHome", true)]
        public string Phone;

        /// <summary>
        /// ��ַ
        /// </summary>
        [Obsolete("����Ϊ��AddressHome", true)]
        public string Address;

        /// <summary>
        /// �Һ�����
        /// </summary>
        [Obsolete("����Ϊ:DoctorInfo.SeeDate", true)]
        public DateTime RegDate;

        /// <summary>
        /// ���
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.Noon.ID", true)]
        public string Noon;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.Begin", true)]
        public DateTime BeginTime;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.End", true)]
        public DateTime EndTime;

        /// <summary>
        /// �������
        /// </summary>
        [Obsolete("����Ϊ��Pact.PayKind", true)]
        public FS.FrameWork.Models.NeuObject PayKind;

        /// <summary>
        /// �Һż���
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.RegLevel", true)]
        public FS.FrameWork.Models.NeuObject RegLevel;

        /// <summary>
        /// �Һſ���
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.Dept", true)]
        public FS.FrameWork.Models.NeuObject RegDept;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.Doct", true)]
        public FS.FrameWork.Models.NeuObject RegDoct;

        /// <summary>
        /// �������
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.SeeNO", true)]
        public int SeeID;

        /// <summary>
        /// �Ű����
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.ID", true)]
        public string SchemaNo;

        /// <summary>
        /// �Ƿ�Ӻ�
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.IsAppend", true)]
        public bool IsAppend;

        /// <summary>
        /// �Һŷ�
        /// </summary>
        [Obsolete("����Ϊ��RegLvlFee.RegFee", true)]
        public decimal RegFee;

        /// <summary>
        /// ����
        /// </summary>
        [Obsolete("����Ϊ��RegLvlFee.ChkFee", true)]
        public decimal ChkFee;

        /// <summary>
        /// ����
        /// </summary>
        [Obsolete("����Ϊ��RegLvlFee.OwnDigFee", true)]
        public decimal DigFee;

        /// <summary>
        /// ������
        /// </summary>
        [Obsolete("����Ϊ��RegLvlFee.OthFee", true)]
        public decimal OthFee;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        [Obsolete("����Ϊ��IsEmergency", true)]
        public bool IsUrg;

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        [Obsolete("����Ϊ��Status", true)]
        public bool IsValid;

        /// <summary>
        /// �Һ����
        /// </summary>
        [Obsolete("����Ϊ��RegType", true)]
        public bool IsPre;

        /// <summary>
        /// ҽ��֤��
        /// </summary>
        [Obsolete("����Ϊ��SSN", true)]
        public string McardID;
        #endregion
    }
}
