using System;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// Register<br></br>
    /// [��������: �Һ���Ϣʵ��]<br></br>
    /// [�� �� ��: ��С��]<br></br>
    /// [����ʱ��: 2007-2-1]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����'
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
        private FS.FrameWork.Models.NeuObject cardType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������Դ
        /// </summary>
        private FS.FrameWork.Models.NeuObject inSource = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private TransTypes tranType = TransTypes.Positive;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Schema doctor = new Schema();

        /// <summary>
        /// ÿ����ˮ��
        /// </summary>
        private int orderNO = 0;        

        /// <summary>
        /// �Һŷ�
        /// </summary>
		private RegLvlFee regLvlFee = new RegLvlFee();

        /// <summary>
        /// �Է�
        /// </summary>
        private decimal ownCost = 0m;
        /// <summary>
        /// �Ը�
        /// </summary>
        private decimal payCost = 0m;
        /// <summary>
        /// ����
        /// </summary>
        private decimal pubCost = 0m;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        private bool isEmergency = false;

        /// <summary>
        /// �Ƿ��շ�
        /// </summary>
        private bool isFee = false;

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
        private string invoiceNO = "";
        /// <summary>
        /// ������ by niuxinyuan  2007-05-15
        /// </summary>
        private string recipeNO = "";

        /// <summary>
        /// �Ѵ�ӡ��Ʊ����
        /// </summary>
        private int printInvoiceCnt = 0;

        /// <summary>
        /// ¼����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment inputOper = new OperEnvironment();

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment cancelOper = new OperEnvironment();

        /// <summary>
        /// �ս��������Ϣ
        /// </summary>
        private OperStat balanceOperStat = new OperStat();

        /// <summary>
        /// �˲��ս������Ϣ
        /// </summary>
        private OperStat checkOperStat = new OperStat();        

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isTriage = false;
                
        /// <summary>
        /// �������Ա��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment triageOper = new OperEnvironment();

        /// <summary>
        /// ���ﻼ���Ż����
        /// </summary>
        private FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate = new FS.HISFC.Models.Fee.Outpatient.EcoRate();
        ///// <summary>
        /////���򲡰���ȥ���ݲ������
        ///// </summary>
        //private bool isSendInhosCase;
        /// <summary>
        /// �������
        /// </summary>
        private string seeDPCD;
        /// <summary>
        /// ����ҽ��
        /// </summary>
        private string seeDOCD;
        /// <summary>
        /// ���߷�����
        /// </summary>
        private FS.HISFC.Models.RADT.PVisit pVisit = new FS.HISFC.Models.RADT.PVisit();

        /// <summary>
        /// �Żݽ��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        private decimal ecoCost = 0m;

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// �Ƿ����˻����̹Һ�
        /// </summary>
        private bool isAccount = false;

        /// <summary>
        /// ��չ�ֶ�1{921FBFCA-3D0D-4bc6-8EEA-A9BBE152E69A}
        /// </summary>
        private string mark1;        

        #endregion
             
        #region ����

        /// <summary>
        /// ���ﻼ���Ż����
        /// </summary>
        public FS.HISFC.Models.Fee.Outpatient.EcoRate EcoRate 
        {
            get 
            {
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
        public FS.FrameWork.Models.NeuObject CardType
        {
            get { return this.cardType; }
            set { this.cardType = value; }
        }

        /// <summary>
        /// ������Դ
        /// </summary>
        public FS.FrameWork.Models.NeuObject InSource
        {
            get { return this.inSource; }
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
            get { return this.doctor; }
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
            get { return regLvlFee; }
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
            get { return this.invoiceNO; }
            set { this.invoiceNO = value; }
        }

        /// <summary>
        /// ��Ʊ��/������
        /// </summary>
        public string RecipeNO
        {
            get { return this.recipeNO; }
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
            get { return inputOper; }
            set { inputOper = value; }
        }

        /// <summary>
        /// ���ϲ���Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CancelOper
        {
            get { return cancelOper; }
            set { cancelOper = value; }
        }

        /// <summary>
        /// �ս�˲������Ϣ
        /// </summary>
        public OperStat CheckOperStat
        {
            get { return checkOperStat; }
            set { checkOperStat = value; }
        }

        /// <summary>
        /// �ս��������Ϣ
        /// </summary>
        public OperStat BalanceOperStat
        {
            get { return balanceOperStat; }
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
            get { return this.triageOper; }
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
        private FS.HISFC.Models.SIInterface.SIMainInfo siInfo = new FS.HISFC.Models.SIInterface.SIMainInfo();

        /// <summary>
        /// ҽ���Ǽ���Ϣ
        /// </summary>
        public FS.HISFC.Models.SIInterface.SIMainInfo SIMainInfo
        {
            get
            {
                return siInfo;
            }
            set
            {
                siInfo = value;
            }
        }
        ///// <summary>
        ///// ���򲡰���ȥ���ݲ������
        ///// by niuxinyuan
        ///// </summary>
        //public bool IsSentInhosCase
        //{
        //    get 
        //    {
        //        return this.isSendInhosCase;
        //    }
        //    set
        //    {
        //        this.isSendInhosCase = value;
        //    }
        //}
        /// <summary>
        /// ������Ҵ���
        /// </summary>
        public string SeeDPCD
        {
            set 
            {
                this.seeDPCD = value;
            }
            get
            {
                return this.seeDPCD;
            }
        }
        /// <summary>
        /// ����ҽ������
        /// </summary>
        public string SeeDOCD
        {
            set 
            {
                this.seeDOCD = value;
            }
            get
            {
                return this.seeDOCD;
            }
        }
        /// <summary>
        /// ���߷�����
        /// </summary>
        public FS.HISFC.Models.RADT.PVisit PVisit
        {
            get { return pVisit; }
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
        #endregion

        #region ����
        ///// <summary>
        /////  �Һŵĸ���
        ///// </summary>
        ///// <returns></returns>
        public new Register Clone()
        {
            Register reg = base.Clone() as Register;

            reg.CardType = this.cardType.Clone();
            reg.InSource = this.inSource.Clone();
            reg.DoctorInfo = this.doctor.Clone();
            reg.regLvlFee = this.regLvlFee.Clone();
                        
            reg.InputOper = this.inputOper.Clone();
            reg.CancelOper = this.cancelOper.Clone();
            reg.BalanceOperStat = this.balanceOperStat.Clone();
            reg.CheckOperStat = this.checkOperStat.Clone();
            reg.TriageOper = this.triageOper.Clone();
            reg.PVisit = this.pVisit.Clone();

            return reg;
        }
        #endregion

        #region  ����
		
        /// <summary>
		/// ������
		/// </summary>
		[Obsolete("����Ϊ��PID.CardNO",true)]
        public string CardNo;
		
        /// <summary>
		/// ���֤��
		/// </summary>
        [Obsolete("����Ϊ:IDCard",true)]
		public string IdenNo;
		        
		/// <summary>
		/// �Ա����
		/// </summary>
		[Obsolete("����Ϊ��Sex.ID",true)]
        public string SexID;
		
		/// <summary>
		/// ��ϵ�绰
		/// </summary>
        [Obsolete("����Ϊ��PhoneHome",true)]
		public string Phone;
		
        /// <summary>
		/// ��ַ
		/// </summary>
        [Obsolete("����Ϊ��AddressHome", true)]
        public string Address;	        

        /// <summary>
        /// �Һ�����
        /// </summary>
        [Obsolete("����Ϊ:DoctorInfo.SeeDate",true)]
        public DateTime RegDate = DateTime.MaxValue;

        /// <summary>
        /// ���
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.Noon.ID",true)]
        public string Noon = "";

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.Begin",true)]
        public DateTime BeginTime = DateTime.MinValue;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.End",true)]
        public DateTime EndTime = DateTime.MinValue;

        /// <summary>
        /// �������
        /// </summary>
        [Obsolete("����Ϊ��Pact.PayKind",true)]
        public FS.FrameWork.Models.NeuObject PayKind;

        /// <summary>
        /// �Һż���
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.RegLevel",true)]
        public FS.FrameWork.Models.NeuObject RegLevel;

        /// <summary>
        /// �Һſ���
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.Dept",true)]
        public FS.FrameWork.Models.NeuObject RegDept;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.Doct", true)]
        public FS.FrameWork.Models.NeuObject RegDoct;

        /// <summary>
        /// �������
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.SeeNO",true)]
        public int SeeID;

        /// <summary>
        /// �Ű����
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.ID",true)]
        public string SchemaNo;

        /// <summary>
        /// �Ƿ�Ӻ�
        /// </summary>
        [Obsolete("����Ϊ��DoctorInfo.Templet.IsAppend",true)]
        public bool IsAppend;

        /// <summary>
		/// �Һŷ�
		/// </summary>
		[Obsolete("����Ϊ��RegLvlFee.RegFee",true)]
        public decimal RegFee;

		/// <summary>
		/// ����
		/// </summary>
        [Obsolete("����Ϊ��RegLvlFee.ChkFee",true)]
		public decimal ChkFee;

		/// <summary>
		/// ����
		/// </summary>
		[Obsolete("����Ϊ��RegLvlFee.OwnDigFee",true)]
        public decimal DigFee;

		/// <summary>
		/// ������
		/// </summary>
		[Obsolete("����Ϊ��RegLvlFee.OthFee",true)]
        public decimal OthFee;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        [Obsolete("����Ϊ��IsEmergency", true)]
        public bool IsUrg;

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        [Obsolete("����Ϊ��Status",true)]
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
