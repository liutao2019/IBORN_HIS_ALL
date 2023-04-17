using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
using System.Collections;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// Patient <br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��һ��'
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
		private FS.HISFC.Models.RADT.PID pid = new PID();

		/// <summary>
		/// ��ᱣ�պ�
		/// </summary>
		private string ssn;

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
		private SexEnumService sex = new SexEnumService();
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
		private NeuObject country = new NeuObject();

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
		private MaritalStatusEnumService maritalStatus = new MaritalStatusEnumService();

		/// <summary>
		/// ���֤
		/// </summary>
		private string idCard;

		/// <summary>
		/// ����
		/// </summary>
		private NeuObject nationality = new NeuObject();

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime deathTime;

		/// <summary>
		/// ����֤����
		/// </summary>
		private NeuObject deathAttestor = new NeuObject();

		/// <summary>
		/// ְҵ
		/// </summary>
		private NeuObject profession = new NeuObject();

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
		/// Ѫ��
		/// </summary>
		private BloodTypeEnumService bloodType = new BloodTypeEnumService();

		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		private FS.HISFC.Models.Base.PactInfo pact = new PactInfo();

		/// <summary>
		/// Ӥ������ Patient
		/// </summary>
		private ArrayList baby = new ArrayList();

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
		private FS.FrameWork.Models.NeuObject doctorReceiver = new NeuObject();

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
		private FS.HISFC.Models.RADT.Card card = new Card();

       /// <summary>
       /// ���ܺ�����
       /// </summary>
        private string decryptName = string.Empty;
        /// <summary>
        /// �Ƿ���������
        /// </summary>
        private bool isEncrypt = false;

        /// <summary>
        /// ���ݿ���Name�ֶ�ʵ�ʴ�ŵ�����
        /// </summary>
        private string normalName = string.Empty;

        /// <summary>
        /// ֤������
        /// </summary>
        private FS.FrameWork.Models.NeuObject idCardType = new NeuObject();
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
        private string matherName = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private string caseNO = string.Empty;
     
        /// <summary>
        /// ���չ�˾
        /// </summary>
        private NeuObject insurance = new NeuObject();

        #endregion
        #endregion

        #region ����

        /// <summary>
		/// ���߸��ֱ��
		/// </summary>
		public FS.HISFC.Models.RADT.PID PID
		{
			get
			{
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
		/// Ѫ��
		/// </summary>
		public BloodTypeEnumService BloodType
		{
			get
			{
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
                if (this.pid.PatientNO.IndexOf("B") >= 0)
                    this.isBaby = true;
              
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
            get { return matherName; }
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
                return insurance;
            }
            set
            {
                insurance = value;
            }
        }
        ////{9543865B-629A-4353-A45A-99D3FC1136BB}
        private string addressHomeDoorNo = string.Empty;

        public string AddressHomeDoorNo
        {
            get { return addressHomeDoorNo; }
            set { addressHomeDoorNo = value; }
        }

        private string email = string.Empty;

        public string Email
        {
            get { return email; }
            set { email = value; }
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

			return patient;
		}
		#endregion

		#endregion
		
		#region ����

		/// <summary>
		/// ������
		/// </summary>
		[Obsolete("�Ѿ���ʱ���౾���Ѿ�ʵ�ּ����빦��", true)]
		public FS.HISFC.Models.Base.Spell RegularSpellCode=new FS.HISFC.Models.Base.Spell();

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
		public NeuObject DateIndecator = new NeuObject();

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
