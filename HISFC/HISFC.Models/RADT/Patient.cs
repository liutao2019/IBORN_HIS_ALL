using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
using System.Collections;
using System.Collections.Generic;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// Patient <br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-11'
	///		�޸�Ŀ��='�汾����'
	///		�޸�����=''
	///  />
	/// </summary>
    [System.ComponentModel.DisplayName("���߻�����Ϣ")]
    [Serializable]
	public class Patient : FS.HISFC.Models.Base.Spell
	{
		/// <summary>
		///������
		/// </summary>
		public Patient()
		{
		
		}

		
		#region ����

		/// <summary>
		/// ���߸��ֱ��
		/// </summary>
        private FS.HISFC.Models.RADT.PID pid;

		/// <summary>
		/// ��ᱣ�պ�
		/// </summary>
		private string ssn;

        /// <summary>
        /// ���˵���
        /// </summary>
        private string lsh;

		/// <summary>
		/// ��������
		/// </summary>
		private System.DateTime birthday;

		/// <summary>
		/// ����
		/// </summary>
		private string age;

		/// <summary>
		/// �Ա�
		/// </summary>
        private SexEnumService sex;
		/// <summary>
		/// ��ͥ��ַ
		/// </summary>
		private string addressHome;

		/// <summary>
		/// ��λ��ַ
		/// </summary>
		private string addressBusiness;

		/// <summary>
		/// ���� 
		/// </summary>
        private NeuObject country;

		/// <summary>
		/// ��ͥ�ʱ�
		/// </summary>
		private string homeZip;

		/// <summary>
		/// ��λ�ʱ�
		/// </summary>
		private string businessZip;

		/// <summary>
		/// ��ͥ�绰
		/// </summary>
		private string phoneHome;

		/// <summary>
		/// ��λ�绰
		/// </summary>
		private string phoneBusiness;

		/// <summary>
		/// ����״̬ 
		/// </summary>
        private MaritalStatusEnumService maritalStatus;

		/// <summary>
		/// ���֤
		/// </summary>
		private string idCard;

		/// <summary>
		/// ����
		/// </summary>
        private NeuObject nationality;

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime deathTime;

		/// <summary>
		/// ����֤����
		/// </summary>
        private NeuObject deathAttestor;

		/// <summary>
		/// ְҵ
		/// </summary>
        private NeuObject profession;

		/// <summary>
		/// ����
		/// </summary>
		private string dist;

		/// <summary>
		/// ����
		/// </summary>
		private string areaCode;

		/// <summary>
		/// ������λ
		/// </summary>
		private string companyName;

		/// <summary>
		/// ���
		/// </summary>
		private string height;

		/// <summary>
		/// ����
		/// </summary>
		private string weight;

        /// <summary>
        /// ��������(̥��){A9327F43-F6C2-4230-BA64-48A11CEB6DD3}
        /// </summary>
        private string gestation;

        /// <summary>
        /// Ѫ��
        /// </summary>
        private string bloodGlu;

        /// <summary>
        /// ����ѹ
        /// </summary>
        private string sbp;

        /// <summary>
        /// ����ѹ
        /// </summary>
        public string SBP
        {
            get
            {
                return sbp;
            }
            set
            {
                sbp = value;
            }
        }

        /// <summary>
        /// ����ѹ
        /// </summary>
        private string dbp;

        /// <summary>
        /// ����ѹ
        /// </summary>
        public string DBP
        {
            get
            {
                return dbp;
            }
            set
            {
                dbp = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private string temperature;

        /// <summary>
        /// ����
        /// </summary>
        public string Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                temperature = value;
            }
        }

		/// <summary>
		/// Ѫ��
		/// </summary>
        private BloodTypeEnumService bloodType;

		/// <summary>
		/// ��ͬ��λ
		/// </summary>
        private FS.HISFC.Models.Base.PactInfo pact;

		/// <summary>
		/// Ӥ������ Patient
		/// </summary>
        private ArrayList baby;

		/// <summary>
		/// �Ƿ���Ӥ��
		/// </summary>
		private bool isBaby;

		/// <summary>
		/// �Ƿ���Ӥ��
		/// </summary>
		private bool isHasBaby;

		/// <summary>
		/// Ӥ������
		/// </summary>
		private int babyCount;

		/// <summary>
		/// ���������
		/// </summary>
		private int balanceNO;

		/// <summary>
		/// �������
		/// </summary>
		private string clinicDiagnose;

		/// <summary>
		/// סԺ����
		/// </summary>
		private int inTimes;

		/// <summary>
		/// ����ҽʦ
		/// </summary>
        private FS.FrameWork.Models.NeuObject doctorReceiver;

		/// <summary>
		/// ����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4�������
		/// </summary>
		private string caseState;

		/// <summary>
		/// ����סԺ�����
		/// </summary>
		private string mainDiagnose;

		/// <summary>
		/// �������յ��Ժ�
		/// </summary>
		private string proCreateNO;

		/// <summary>
		/// ���ߴſ�
		/// </summary>
        private FS.HISFC.Models.RADT.Card card;

       /// <summary>
       /// ���ܺ�����
       /// </summary>
        private string decryptName;
        /// <summary>
        /// �Ƿ���������
        /// </summary>
        private bool isEncrypt = false;

        /// <summary>
        /// ���ݿ���Name�ֶ�ʵ�ʴ�ŵ�����
        /// </summary>
        private string normalName;

        /// <summary>
        /// ֤������
        /// </summary>
        private FS.FrameWork.Models.NeuObject idCardType;

        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        #region 
        /// <summary>
        /// �Ƿ��ﻼ��
        /// </summary>
        private bool isTreatment = false;

        /// <summary>
        /// �Ƿ�VIP
        /// </summary>
        private bool vipFlag = false;

        /// <summary>
        /// ĸ������
        /// </summary>
        private string matherName;
        /// <summary>
        /// ������
        /// </summary>
        private string caseNO;
     
        /// <summary>
        /// ���չ�˾
        /// </summary>
        private NeuObject insurance;

        #endregion

        /// <summary>
        /// �Ƿ���·������
        /// </summary>
        private bool isPtjtState;
        


        #endregion

        #region ����

        /// <summary>
		/// ���߸��ֱ��
		/// </summary>
		public FS.HISFC.Models.RADT.PID PID
		{
			get
			{
                if (pid == null)
                {
                    pid = new PID();
                }
				return this.pid;
			}
			set
			{
				this.pid = value;
			}
		}

		/// <summary>
		/// ��ᱣ�պ�
		/// </summary>
		public string SSN
		{
			get
			{
				return this.ssn;
			}
			set
			{
				this.ssn = value;
			}
		}

        /// <summary>
        /// ���˵���
        /// </summary>
        public string LSH
        {
            get
            {
                return this.lsh;
            }
            set
            {
                this.lsh = value;
            }
        }

        [System.ComponentModel.DisplayName("��������")]
        [System.ComponentModel.Description("���߳�������")]
		/// <summary>
		/// ��������
		/// </summary>
		public System.DateTime Birthday
		{
			get
			{
				return this.birthday;
			}
			set
			{
				this.birthday = value;
			}
		}

        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("��������")]
		/// <summary>
		/// ����
		/// </summary>
		public string Age
		{
			get
			{
				return this.age;
			}
			set
			{
				this.age = value;
			}
		}

        [System.ComponentModel.DisplayName("�Ա�")]
        [System.ComponentModel.Description("�����Ա�")]
		/// <summary>
		/// �Ա�
		/// </summary>
		public SexEnumService Sex
		{
			get
			{
                if (sex == null)
                {
                    sex = new SexEnumService();
                }
				return this.sex;
			}
			set
			{
				this.sex = value;
			}
		}

		/// <summary>
		/// ��ͥ��ַ
		/// </summary>
		public string AddressHome
		{
			get
			{
				return this.addressHome;
			}
			set
			{
				this.addressHome = value;
			}
		}

		/// <summary>
		/// ��λ��ַ
		/// </summary>
		public string AddressBusiness
		{
			get
			{
				return this.addressBusiness;
			}
			set
			{
				this.addressBusiness = value;
			}
		}
        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("���߹���")]
		/// <summary>
		/// ����
		/// </summary>
		public NeuObject Country
		{
			get
			{
                if (country == null)
                {
                    country = new NeuObject();
                }
				return this.country;
			}
			set
			{
				this.country = value;
			}
		}

		/// <summary>
		/// ��ͥ�ʱ�
		/// </summary>
		public string HomeZip
		{
			get
			{
				return this.homeZip;
			}
			set
			{
				this.homeZip = value;
			}
		}

		/// <summary>
		/// ��λ�ʱ�
		/// </summary>
		public string BusinessZip
		{
			get
			{
				return this.businessZip;
			}
			set
			{
				this.businessZip = value;
			}
		}

        [System.ComponentModel.DisplayName("��ͥ�绰")]
        [System.ComponentModel.Description("���߼�ͥ�绰")]
		/// <summary>
		/// ��ͥ�绰
		/// </summary>
		public string PhoneHome
		{
			get
			{
				return this.phoneHome;
			}
			set
			{
				this.phoneHome = value;
			}
		}

        [System.ComponentModel.DisplayName("��λ�绰")]
        [System.ComponentModel.Description("���ߵ�λ�绰")]
		/// <summary>
		/// ��λ�绰
		/// </summary>
		public string PhoneBusiness
		{
			get
			{
				return this.phoneBusiness;
			}
			set
			{
				this.phoneBusiness = value;
			}
		}

        [System.ComponentModel.DisplayName("����״̬")]
        [System.ComponentModel.Description("���߻���״̬")]
		/// <summary>
		/// ����״̬
		/// </summary>
		public MaritalStatusEnumService MaritalStatus
		{
			get
			{
                if (maritalStatus == null)
                {
                    maritalStatus = new MaritalStatusEnumService();
                }
				return this.maritalStatus;
			}
			set
			{
				this.maritalStatus = value;
			}
		}

        [System.ComponentModel.DisplayName("���֤��")]
        [System.ComponentModel.Description("�������֤��")]
		/// <summary>
		/// ֤����
		/// </summary>
		public string IDCard
		{
			get
			{
				return this.idCard;
			}
			set
			{
				this.idCard = value;
			}
		}
        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("��������")]
		/// <summary>
		/// ����
		/// </summary>
		public NeuObject Nationality
		{
			get
			{
                if (nationality == null)
                {
                    nationality = new NeuObject();
                }
				return this.nationality;
			}
			set
			{
				this.nationality = value;
			}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime DeathTime
		{
			get
			{
				return this.deathTime;
			}
			set
			{
				this.deathTime = value;
			}
		}

		/// <summary>
		/// ����֤����
		/// </summary>
		public NeuObject DeathAttestor
		{
			get
			{
                if (this.deathAttestor == null)
                {
                    this.deathAttestor = new NeuObject();
                }
				return this.deathAttestor;
			}
			set
			{
				this.deathAttestor = value;
			}
		}

        [System.ComponentModel.DisplayName("ְҵ")]
        [System.ComponentModel.Description("����ְҵ")]
		/// <summary>
		/// ְҵ
		/// </summary>
		public NeuObject Profession
		{
			get
			{
                if (profession == null)
                {
                    profession = new NeuObject();
                }
				return this.profession;
			}
			set
			{
				this.profession = value;
			}
		}

        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("���߼���")]
		/// <summary>
		/// ����
		/// </summary>
		public string DIST
		{
			get
			{
				return this.dist;
			}
			set
			{
				this.dist = value;
			}
		}
        [System.ComponentModel.DisplayName("������")]
        [System.ComponentModel.Description("���߳�����")]
		/// <summary>
		/// ����
		/// </summary>
		public string AreaCode
		{
			get
			{
				return this.areaCode;
			}
			set
			{
				this.areaCode = value;
			}
		}

        [System.ComponentModel.DisplayName("������λ")]
        [System.ComponentModel.Description("���߹�����λ")]
		/// <summary>
		/// ������λ
		/// </summary>
		public string CompanyName
		{
			get
			{
				return this.companyName;
			}
			set
			{
				this.companyName = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		public string Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Weight
		{
			get
			{
				return this.weight;
			}
			set
			{
				this.weight = value;
			}
		}

        /// <summary>
        /// ��������{A9327F43-F6C2-4230-BA64-48A11CEB6DD3}
        /// </summary>
        public string Gestation
        {
            get
            {
                return this.gestation;
            }
            set
            {
                this.gestation = value;
            }
        }

        /// <summary>
        /// Ѫ��
        /// </summary>
        public string BloodGlu
        {
            get
            {
                return this.bloodGlu;
            }
            set
            {
                this.bloodGlu = value;
            }
        }

		/// <summary>
		/// Ѫ��
		/// </summary>
		public BloodTypeEnumService BloodType
		{
			get
			{
                if (bloodType == null)
                {
                    bloodType = new BloodTypeEnumService();
                }
				return this.bloodType;
			}
			set
			{
				this.bloodType = value;
			}
		}

        [System.ComponentModel.DisplayName("��ͬ��λ")]
        [System.ComponentModel.Description("���ߺ�ͬ��λ")]
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public FS.HISFC.Models.Base.PactInfo Pact
		{
			get
			{
                if (pact == null)
                {
                    pact = new PactInfo();
                }

				return this.pact;
			}
			set
			{
				this.pact = value;
			}
		}

		/// <summary>
		/// Ӥ������
		/// </summary>
		public ArrayList Baby
		{
			get
			{
                if (baby == null)
                {
                    baby = new ArrayList();
                }
				return this.baby;
			}
			set
			{
				this.baby = value;
			}
		}

		/// <summary>
		/// �Ƿ�Ӥ��
		/// </summary>
		public bool IsBaby
		{
			get
			{
                if (pid != null)
                {
                    if (this.pid.PatientNO.IndexOf("B") >= 0)
                        this.isBaby = true;
                }
				return this.isBaby;
			}
            set
            {
                this.isBaby = value;
            }
		}

		/// <summary>
		/// �Ƿ���Ӥ��
		/// </summary>
		public bool IsHasBaby
		{
			get
			{
				return this.isHasBaby;
			}
			set
			{
				this.isHasBaby = value;
			}
		}

		/// <summary>
		/// Ӥ������
		/// </summary>
		public int BabyCount
		{
			get
			{
				return this.babyCount;
			}
			set
			{
				this.babyCount = value;
			}
		}

		/// <summary>
		/// ���������
		/// </summary>
		public int BalanceNO
		{
			get
			{
				return this.balanceNO;
			}
			set
			{
				this.balanceNO = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public string ClinicDiagnose
		{
			get
			{
				return this.clinicDiagnose;
			}
			set
			{
				this.clinicDiagnose = value;
			}
		}

		/// <summary>
		/// סԺ����
		/// </summary>
		public int InTimes
		{
			get
			{
				return this.inTimes;
			}
			set
			{
				this.inTimes = value;
			}
		}

		/// <summary>
		///  סԺ֤����ҽʦ
		/// </summary>
		public FS.FrameWork.Models.NeuObject DoctorReceiver
		{
			get
			{
                if (doctorReceiver == null)
                {
                    doctorReceiver = new NeuObject();
                }
				return this.doctorReceiver;
			}
			set
			{
				this.doctorReceiver = value;
			}
		}

		/// <summary>
		/// ����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4�������
		/// </summary>
		public string CaseState
		{
			get
			{
				return this.caseState;
			}
			set
			{
				this.caseState = value;
			}
		}

		/// <summary>
		/// ����סԺ�����
		/// </summary>
		public string MainDiagnose
		{
			get
			{
				return this.mainDiagnose;
			}
			set
			{
				this.mainDiagnose = value;
			}
		}

		/// <summary>
		/// �������յ��Ժ�
		/// </summary>
		public string ProCreateNO
		{
			get
			{
				return this.proCreateNO;
			}
			set
			{
				this.proCreateNO = value;
			}
		}

		/// <summary>
		/// ���ߴſ�
		/// </summary>
		public FS.HISFC.Models.RADT.Card Card
		{
			get
			{
                if (card == null)
                {
                    card = new Card();
                }

				return this.card;
			}
			set
			{
				this.card = value;
			}
		}

        //		/// <summary>
//		/// Ѫ��
//		/// </summary>
//		public EnumBloodType BloodType
//		{
//			get
//			{
//				return this.bloodType ;
//			}
//			set
//			{
//				this.bloodType = value ;
//			}
//		}
        /// <summary>
        /// ���ܺ�����(��ʵ����)
        /// </summary>
        public string DecryptName
        {
            get
            {
                if (decryptName == null)
                {
                    decryptName = string.Empty;
                }
                return this.decryptName;
            }
            set
            {
                this.decryptName = value;
                
            }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsEncrypt
        {
            get
            {
                return this.isEncrypt;
            }
            set
            {
                this.isEncrypt = value;
            }
        }

        /// <summary>
        /// ��������������
        /// </summary>
        public string NormalName
        {
            get
            {
                if (normalName == null)
                {
                    normalName = string.Empty;
                }
                return normalName;
            }
            set
            {
                normalName = value;
            }
        }

        /// <summary>
        /// ֤������
        /// </summary>
        public NeuObject IDCardType
        {
            get
            {
                if (idCardType == null)
                {
                    idCardType = new NeuObject();
                }
                return idCardType;
            }
            set
            {
                idCardType = value;
            }
        }
        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("��������")]
        /// <summary>
        /// ��������
        /// </summary>
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        /// <summary>
        /// �������VIP��Ժ��ְ�����������Ȼ��߱�ǣ�
        /// </summary>
        private string patientType;

        /// <summary>
        /// �������VIP��Ժ��ְ�����������Ȼ��߱�ǣ�
        /// </summary>
        public string PatientType
        {
            get
            {
                return patientType;
            }
            set
            {
                patientType = value;
            }
        }

        #region {9543865B-629A-4353-A45A-99D3FC1136BB}

        /// <summary>
        /// �Ƿ��ﻼ��
        /// </summary>
        public bool IsTreatment
        {
            get
            {
                return isTreatment;
            }
            set
            {
                isTreatment = value;
            }
        }

        /// <summary>
        /// �Ƿ�VIP
        /// </summary>
        public bool VipFlag
        {
            get { return vipFlag; }
            set { vipFlag = value; }
        }

        /// <summary>
        /// ĸ������
        /// </summary>
        public string MatherName
        {
            get {
                if (matherName == null)
                {
                    matherName = string.Empty;
                }
                return matherName; }
            set { matherName = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        [Obsolete("�Ѿ���ʱ������ΪPID.CaseNO", true)]
        public string CaseNO
        {
            get
            {
                if (caseNO == null)
                {
                    caseNO = string.Empty;
                }

                return caseNO;
            }
            set
            {
                caseNO = value;
            }
        }

        /// <summary>
        /// ���չ�˾
        /// </summary>
        public NeuObject Insurance
        {
            get
            {
                if (insurance == null)
                {
                    insurance = new NeuObject();
                }

                return insurance;
            }
            set
            {
                insurance = value;
            }
        }
        ////{9543865B-629A-4353-A45A-99D3FC1136BB}
        private string addressHomeDoorNo;

        public string AddressHomeDoorNo
        {
            get {
                if (addressHomeDoorNo == null)
                {
                    addressHomeDoorNo = string.Empty;
                }
                return addressHomeDoorNo; }
            set { addressHomeDoorNo = value; }
        }

        private string email;

        public string Email
        {
            get {
                if (email == null)
                {
                    email = string.Empty;
                }
                return email; }
            set { email = value; }
        }

        private List<FS.HISFC.Models.Base.PactInfo> mutiPactInfo ;
        public List<FS.HISFC.Models.Base.PactInfo> MutiPactInfo
        {
            get
            {
                return mutiPactInfo;
            }
            set
            {
                mutiPactInfo = value;
            }
        }


        #endregion

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.Models.RADT.Kin kin ;// new FS.HISFC.Models.RADT.Kin();

        /// <summary>
        /// ������
        /// </summary>
        public FS.HISFC.Models.RADT.Kin Kin
        {
            get
            {
                if (kin == null)
                {
                    kin = new Kin();
                }
                return this.kin;
            }
            set
            {
                this.kin = value;
            }
        }

        /// <summary>
        /// ����������Ϣ��
        /// </summary>
        private FS.HISFC.Models.RADT.PDisease disease ;// new FS.HISFC.Models.RADT.PDisease();

        /// <summary>
        /// ����������Ϣ��
        /// </summary>
        public FS.HISFC.Models.RADT.PDisease Disease
        {
            get
            {
                if (disease == null)
                {
                    disease = new PDisease();
                }
                return this.disease;
            }
            set
            {
                this.disease = value;
            }
        }
        
        /// <summary>
        /// ҽ����Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.SIInterface.SIPersonInfo siPerson ;//new  FS.HISFC.Models.SIInterface.SIPersonInfo();
        /// <summary>
        /// ҽ����Ա��Ϣ
        /// </summary>
        public FS.HISFC.Models.SIInterface.SIPersonInfo SIPerson
        {
            get
            {
                if (siPerson == null)
                {
                    siPerson = new FS.HISFC.Models.SIInterface.SIPersonInfo();
                }
                return siPerson;
            }
            set
            {
                siPerson = value;
            }
        }

        /// <summary>
        /// �Ƿ���·������
        /// </summary>
        public bool IsPtjtState
        {
            get
            {
                return isPtjtState;
            }
            set
            {
                isPtjtState = value;
            }
        }
        // {473865F9-C2E6-4f05-BEB3-7CD1F0349126}
        /// <summary>
        /// ͨ��ʵ��
        /// </summary>
        private Object usualObject = null;
        /// <summary>
        /// ͨ��ʵ��
        /// </summary>
        public Object UsualObject
        {
            get
            {
                return usualObject;
            }
            set
            {
                usualObject = value;
            }
        }
        //6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A

        /// <summary>
        /// ��ҵ���չ�˾���
        /// </summary>
      //  public string BICompanyID { set; get; }

        /// <summary>
        /// ��ҵ���չ�˾����
        /// </summary>
        // public string BICompanyName { set; get; }

        #region ��Աϵͳ���

        private string crmID = string.Empty;

        /// <summary>
        /// ��ԱϵͳID
        /// </summary>
        public string CrmID
        {
            get { return crmID; }
            set { crmID = value; }
        }

        private NeuObject channel1 = new NeuObject();

        /// <summary>
        /// һ������
        /// </summary>
        public NeuObject Channel1
        {
            get
            {
                if (channel1 == null)
                {
                    channel1 = new NeuObject();
                }
                return channel1;
            }

            set { channel1 = value; }
        }

        private NeuObject channel2 = new NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        public NeuObject Channel2
        {
            get
            {
                if (channel2 == null)
                {
                    channel2 = new NeuObject();
                }
                return channel2;
            }

            set { channel2 = value; }
        }

        private NeuObject channel3 = new NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        public NeuObject Channel3
        {
            get 
            {
                if (channel3 == null)
                {
                    channel3 = new NeuObject();
                }
                return channel3; 
            }

            set { channel3 = value; }
        }

        private NeuObject clientmanager = new NeuObject();

        /// <summary>
        /// �ͷ�
        /// </summary>
        public NeuObject Clientmanager
        {
            get
            {
                if (clientmanager == null)
                {
                    clientmanager = new NeuObject();
                }
                return clientmanager;
            }

            set { clientmanager = value; }
        }

        private NeuObject consultmanager = new NeuObject();
        
        /// <summary>
        /// ��ѯ
        /// </summary>
        public NeuObject Consultmanager
        {
            get
            {
                if (consultmanager == null)
                {
                    consultmanager = new NeuObject();
                }
                return consultmanager;
            }

            set { consultmanager = value; }
        }


        private NeuObject sellmanager = new NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        public NeuObject Sellmanager
        {
            get
            {
                if (sellmanager == null)
                {
                    sellmanager = new NeuObject();
                }
                return sellmanager;
            }

            set { sellmanager = value; }
        }


        private NeuObject bcsellmanager = new NeuObject();

        /// <summary>
        /// ҽ������
        /// </summary>
        public NeuObject Bcsellmanager
        {
            get
            {
                if (bcsellmanager == null)
                {
                    bcsellmanager = new NeuObject();
                }
                return bcsellmanager;
            }

            set { bcsellmanager = value; }
        }

        private string childFlag = string.Empty;

        /// <summary>
        /// ��ͯ���
        /// </summary>
        public string ChildFlag
        {
            get { return childFlag; }
            set { childFlag = value; }
        }

        private string packageFlag = string.Empty;

        /// <summary>
        /// �ײͱ��
        /// </summary>
        public string PackageFlag
        {
            get { return packageFlag; }
            set { packageFlag = value; }
        }

        private NeuObject familyInfo = new NeuObject();

        /// <summary>
        /// ��ͥ��Ϣ
        /// </summary>
        public NeuObject FamilyInfo
        {
            get
            {
                if (familyInfo == null)
                {
                    familyInfo = new NeuObject();
                }
                return familyInfo;
            }

            set { bcsellmanager = value; }
        }

        private NeuObject familyRole = new NeuObject();

        /// <summary>
        /// ��ͥ��ɫ
        /// </summary>
        public NeuObject FamilyRole
        {
            get
            {
                if (familyRole == null)
                {
                    familyRole = new NeuObject();
                }
                return familyRole;
            }

            set { familyRole = value; }
        }

        #endregion

        #endregion

        #region ����

        #region ��¡
        /// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Patient Clone()
		{
			Patient patient = base.Clone() as Patient;
			
			patient.PID = this.PID.Clone();
			patient.Country = this.Country.Clone();
			patient.Nationality = this.Nationality.Clone();
			patient.DeathAttestor = this.DeathAttestor.Clone();
			patient.Profession = this.Profession.Clone();
			patient.Sex = this.Sex.Clone();
			patient.Pact = this.Pact.Clone();
			patient.DoctorReceiver = this.DoctorReceiver.Clone();
			patient.Card = this.Card.Clone();
            patient.IDCardType = this.IDCardType.Clone();
            //{9543865B-629A-4353-A45A-99D3FC1136BB}
            patient.Insurance = this.Insurance.Clone();
            patient.kin = this.Kin.Clone();
            patient.siPerson = this.SIPerson.Clone();
            patient.disease = this.Disease.Clone();
            List<FS.HISFC.Models.Base.PactInfo> list = null;
            if (this.MutiPactInfo != null)
            {
                list = new List<PactInfo>();
                foreach (FS.HISFC.Models.Base.PactInfo pactinfo in this.MutiPactInfo)
                {
                    list.Add(pactinfo.Clone());
                }
            }
            patient.mutiPactInfo = list;

			return patient;
		}
		#endregion

		#endregion
		
		#region ����

		/// <summary>
		/// ������
		/// </summary>
        [Obsolete("�Ѿ���ʱ���౾���Ѿ�ʵ�ּ����빦��", true)]
        public FS.HISFC.Models.Base.Spell RegularSpellCode
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// ���֤��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪIDCard",true)]
		public string IDNo;

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪDeathTime", true)]
		public DateTime DeathDateTime;

		/// <summary>
		/// ����֤����
		/// </summary>
        [Obsolete("�Ѿ���ʱ������ΪDeathAttestor", true)]
        public NeuObject DateIndecator
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// �Ƿ���Ӥ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪIsHasBaby", true)]
		public bool HasBaby=false;

		/// <summary>
		/// ���������
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪBalanceNO", true)]
		public int BalanceNo;

		/// <summary>
		/// סԺ����
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪInTimes", true)]
		public int In_Times;

		/// <summary>
		/// �������յ��Ժ�
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪProcreateNO", true)]
		public string ProCeatePcNo;

		#endregion
	}
}
