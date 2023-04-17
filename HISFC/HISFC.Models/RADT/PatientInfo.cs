using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// PatientInfo <br></br>
	/// [��������: �����ۺ�ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
    /// User01 User02 User03 �ѱ�סԺԤԼ�Ǽ�ʹ��
	/// <�޸ļ�¼
    /// 
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-11'
	///		�޸�Ŀ��='�汾����'
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class PatientInfo : FS.HISFC.Models.RADT.Patient 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PatientInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		
		}

		#region ����

		/// <summary>
		/// ���߷�����Ϣ
		/// </summary>
		private FT ft;// new FT();

		/// <summary>
		/// ���߷�����
		/// </summary>
		private FS.HISFC.Models.RADT.PVisit pVisit;// new FS.HISFC.Models.RADT.PVisit();

		/// <summary>
		/// ��������
		/// </summary>
		private FS.HISFC.Models.RADT.Caution caution;// new FS.HISFC.Models.RADT.Caution();

		/// <summary>
		/// ������
		/// </summary>
        private FS.HISFC.Models.RADT.Kin kin;// new Kin();

		/// <summary>
		/// �������  01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�               
		/// </summary>
		private FS.HISFC.Models.Base.PayKind payKind ;// new FS.HISFC.Models.Base.PayKind();

		/// <summary>
		/// ����������Ϣ��
		/// </summary>
		private FS.HISFC.Models.RADT.PDisease disease;// new PDisease();

		/// <summary>
		/// ���
		/// </summary>
		private System.Collections.ArrayList diagnoses;// new System.Collections.ArrayList(); 

        /// <summary>
        /// �������
        /// </summary>
        private string msdiagnoses;

		/// <summary>
		/// ҽ�����߻�����Ϣ,������Ϣ
		/// </summary>
		private FS.HISFC.Models.SIInterface.SIMainInfo siMainInfo;// new FS.HISFC.Models.SIInterface.SIMainInfo();

		/// <summary>
		/// ��չ���  Ŀǰ������ɽһԺ ��־��ҽ�����޶��Ƿ�ͬ�⣺0��ͬ�⣬1ͬ��
		/// </summary>
		private string extendFlag;

		/// <summary>
		/// ��չ���1
		/// </summary>
		private string extendFlag1;

		/// <summary>
		/// ��չ���2
		/// </summary>
		private string extendFlag2;

        /// <summary>
        /// ����סԺ������
        /// </summary>
        private EnumPatientNOType patientNOType;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime balanceDate = DateTime.MinValue;
        /// <summary>
        /// ����
        /// </summary>
        private FS.HISFC.Models.Fee.Surety surety ;// new FS.HISFC.Models.Fee.Surety();

        /// <summary>
        /// ���ʱ�־{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
        /// </summary>
        private bool isStopAcount;

        /// <summary>
        /// �գ����⣩{AB392EE7-0666-4456-B29F-458730318812}
        /// </summary>
        private string last_Name="";
        
        /// <summary>
        /// �������⣩{AB392EE7-0666-4456-B29F-458730318812}
        /// </summary>
        private string first_Name="";

        /// <summary>
        /// middleName(����){AB392EE7-0666-4456-B29F-458730318812}
        /// </summary>
        private string middle_Name="";

        /// <summary>
        /// ����{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string city = "";
        
        /// <summary>
        /// ʡ{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string state = "";
       
        /// <summary>
        /// �ֻ�{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string mobile = "";
        
        /// <summary>
        /// �м�������{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string noOfChildren = "";
       
        /// <summary>
        /// ������ַ{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string workAddress = "";
        
        /// <summary>
        /// ������ϵ�ˣ�������{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string linkManHome = "";
        
        /// <summary>
        /// ������ϵ�ˣ�������{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string linkPhoneHome = "";
        
        /// <summary>
        /// �Ӻ�;����֪���ǵ�ҽ������{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string knowWay = "";


        /// <summary>
        /// ����Դ
        /// </summary>
        private string allergyInfo = "";
        /// <summary>
        /// ��������  // {F6204EF5-F295-4d91-B81A-736A268DD394}
        /// </summary>
        private FS.FrameWork.Models.NeuObject patientType;
        /// <summary>
        /// ���䷽ʽ 
        /// </summary>
        private FS.FrameWork.Models.NeuObject deliveryMode;

        /// <summary>
        /// ��ͥ�� 
        /// </summary>
        private string familyCode = string.Empty;
        /// <summary>
        /// ��ͥ���� 
        /// </summary>
        private string familyName = string.Empty;

        /// <summary>
        /// �������� 
        /// </summary>
        private string otherCardNo = string.Empty;

        /// <summary>
        /// ת���� 
        /// </summary>
        private string referralPerson = string.Empty;
        /// <summary>
        /// �ͷ�רԱ 
        /// </summary>
        private FS.FrameWork.Models.NeuObject serviceInfo;

        /// <summary>
        /// ������Դ 
        /// </summary>
        private FS.FrameWork.Models.NeuObject patientSourceInfo;

        /// <summary>
        /// �������� 
        /// </summary>
        private FS.FrameWork.Models.NeuObject channelInfo;
        
       // {23F37636-DC34-44a3-A13B-071376265450}
        /// <summary>
        /// Ժ��id
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// Ժ����
        /// </summary>
        private string hospital_name;

		#endregion

        #region ����


        /// <summary>
        /// ��ͥ����
        /// </summary>
        public string FamilyName
        {
            get
            {
                return this.familyName;
            }
            set
            {
                this.familyName = value;
            }
        }
        /// <summary>
        /// ��ͥ��
        /// </summary>
        public string FamilyCode
        {
            get
            {
                return this.familyCode;
            }
            set
            {
                this.familyCode = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string OtherCardNo
        {
            get
            {
                return this.otherCardNo;
            }
            set
            {
                this.otherCardNo = value;
            }
        }
        /// <summary>
        /// ת����
        /// </summary>
        public string ReferralPerson
        {
            get
            {
                return this.referralPerson;
            }
            set
            {
                this.referralPerson = value;
            }
        }
        /// <summary>
        /// �ͷ�רԱ
        /// </summary>
        public FS.FrameWork.Models.NeuObject ServiceInfo
        {
            get
            {

                if (serviceInfo == null)
                {
                    serviceInfo = new FS.FrameWork.Models.NeuObject();
                }
                return this.serviceInfo;
            }
            set
            {
                this.serviceInfo = value;
            }
        }
        /// <summary>
        /// ������Դ
        /// </summary>
        public FS.FrameWork.Models.NeuObject PatientSourceInfo
        {
            get
            {

                if (patientSourceInfo == null)
                {
                    patientSourceInfo = new FS.FrameWork.Models.NeuObject();
                }
                return this.patientSourceInfo;
            }
            set
            {
                this.patientSourceInfo = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ChannelInfo
        {
            get
            {

                if (channelInfo == null)
                {
                    channelInfo = new FS.FrameWork.Models.NeuObject();
                }
                return this.channelInfo;
            }
            set
            {
                this.channelInfo = value;
            }
        }
        /// <summary>
        /// ���䷽ʽ// {F6204EF5-F295-4d91-B81A-736A268DD394}
        /// </summary>
        public FS.FrameWork.Models.NeuObject DeliveryMode
        {
            get
            {

                if (deliveryMode == null)
                {
                    deliveryMode = new FS.FrameWork.Models.NeuObject();
                }
                return this.deliveryMode;
            }
            set
            {
                this.deliveryMode = value;
            }
        }
        /// <summary>
        /// ��������// {F6204EF5-F295-4d91-B81A-736A268DD394}
        /// </summary>
        public FS.FrameWork.Models.NeuObject PatientType
        {
            get
            {

                if (patientType == null)
                {
                    patientType = new FS.FrameWork.Models.NeuObject();
                }
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }
        /// <summary>
        /// ����Դ
        /// </summary>
        public string AllergyInfo
        {
            get
            {
                return this.allergyInfo;
            }
            set
            {
                this.allergyInfo = value;
            }
        }

		/// <summary>
		/// ���߷�����Ϣ
		/// </summary>
		public FT FT
		{
			get
			{
                if (ft == null)
                {
                    ft = new FT();
                }
				return this.ft;
			}
			set
			{
				this.ft = value;
			}
		}

		/// <summary>
		/// ���߷�����
		/// </summary>
		public FS.HISFC.Models.RADT.PVisit PVisit
		{
			get
			{
                if (pVisit == null)
                {
                    pVisit = new PVisit();
                }
				return this.pVisit;
			}
			set
			{
				this.pVisit = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
        [Obsolete("�Ѿ�����,�滻ΪSurety", true)]
		public FS.HISFC.Models.RADT.Caution Caution
		{
			get
			{
                if (caution == null)
                {
                    caution = new Caution();
                }
				return this.caution;
			}
			set
			{
				this.caution = value;
			}
		}

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
		/// �������  01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�
		/// </summary>
		[Obsolete("�Ѿ����ڣ���Patient�����е�Pact�Ѿ�����", true)]
		public FS.HISFC.Models.Base.PayKind PayKind
		{
			get
			{
                if (payKind == null)
                {
                    payKind = new PayKind();
                }
				return this.payKind;
			}
			set
			{
				this.payKind = value;
			}
		}

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
		/// ���
		/// </summary>
		public System.Collections.ArrayList Diagnoses
		{
			get
			{
                if (diagnoses == null)
                {
                    diagnoses = new System.Collections.ArrayList();
                }
				return this.diagnoses;
			}
			set
			{
				this.diagnoses = value;
			}
		}

        /// <summary>
        /// �������
        /// </summary>
        public string MSDiagnoses
        {
            get
            {
                return this.msdiagnoses;
            }
            set
            {
                this.msdiagnoses = value;
            }
        }

		/// <summary>
		/// ҽ�����߻�����Ϣ,������Ϣ
		/// </summary>
		public FS.HISFC.Models.SIInterface.SIMainInfo SIMainInfo
		{
			get
			{
                if (siMainInfo == null)
                {
                    siMainInfo = new FS.HISFC.Models.SIInterface.SIMainInfo();
                }
				return this.siMainInfo;
			}
			set
			{
				this.siMainInfo = value;
			}
		}

		/// <summary>
		/// ��չ���  Ŀǰ������ɽһԺ ��־��ҽ�����޶��Ƿ�ͬ�⣺0��ͬ�⣬1ͬ��
		/// </summary>
		public string ExtendFlag
		{
			get
			{
				return this.extendFlag;
			}
			set
			{
				this.extendFlag = value;
			}
		}

		/// <summary>
		/// ��չ���1
		/// </summary>
		public string ExtendFlag1
		{
			get
			{
				return this.extendFlag1;
			}
			set
			{
				this.extendFlag1 = value;
			}
		}

		/// <summary>
		/// ��չ���2
		/// </summary>
		public string ExtendFlag2
		{
			get
			{
				return this.extendFlag2;
			}
			set
			{
				this.extendFlag2 = value;
			}
		}

        /// <summary>
        /// ����סԺ������
        /// </summary>
        public EnumPatientNOType PatientNOType 
        {
            get 
            {
                return this.patientNOType;
            }
            set 
            {
                this.patientNOType = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime BalanceDate
        {
            get
            {
                return this.balanceDate;
            }
            set
            {
                this.balanceDate = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public FS.HISFC.Models.Fee.Surety Surety
        {
            get
            {
                if (surety == null)
                {
                    surety = new FS.HISFC.Models.Fee.Surety();
                }
                return surety;
            }
            set
            {
                surety = value;
            }
        }

        /// <summary>
        /// ���ʱ�־{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
        /// </summary>
        public bool IsStopAcount
        {
            get { return isStopAcount; }
            set { isStopAcount = value; }
        }

        /// <summary>
        /// middleName(����)
        /// </summary>
        public string Middle_Name
        {
            get { return middle_Name; }
            set { middle_Name = value; }
        }

        /// <summary>
        /// �գ����⣩
        /// </summary>
        public string Last_Name
        {
            get { return last_Name; }
            set { last_Name = value; }
        }

        /// <summary>
        /// �������⣩
        /// </summary>
        public string First_Name
        {
            get { return first_Name; }
            set { first_Name = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        /// <summary>
        /// ʡ
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }


        /// <summary>
        /// �ֻ�
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        /// <summary>
        /// �м�������
        /// </summary>
        public string NoOfChildren
        {
            get { return noOfChildren; }
            set { noOfChildren = value; }
        }

        /// <summary>
        /// ������ַ
        /// </summary>
        public string WorkAddress
        {
            get { return workAddress; }
            set { workAddress = value; }
        }

        /// <summary>
        /// ������ϵ�ˣ�������
        /// </summary>
        public string LinkManHome
        {
            get { return linkManHome; }
            set { linkManHome = value; }
        }

        /// <summary>
        /// ������ϵ�ˣ�������
        /// </summary>
        public string LinkPhoneHome
        {
            get { return linkPhoneHome; }
            set { linkPhoneHome = value; }
        }

        /// <summary>
        /// �Ӻ�;����֪���ǵ�ҽ������
        /// </summary>
        public string KnowWay
        {
            get { return knowWay; }
            set { knowWay = value; }
        }

        /// <summary>
        /// ҽ���������ͣ�MII001���ظ�Σ���MII005�ʹ�����MII010��ʽ���䣨������7����������������MII015������סԺ��MII020	����3��������������סԺ����MII025����3��������������סԺ��
        /// </summary>
        public string HealthTreamType{ get; set; }

        //{23F37636-DC34-44a3-A13B-071376265450}
        /// <summary>
        ///Ժ��id
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }


        /// <summary>
        /// Ժ����
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }

		#endregion

		#region ����

		/// <summary>
		/// ���߷�����Ϣ
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪFT", true)]
		public FT Fee=new FT();

		/// <summary>
		/// ���߻�����Ϣ
        /// </summary>
        [System.Obsolete("�Ѿ����ڣ�����Ϊ�̳�",true)]
		private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

		/// <summary>
		/// ���߻�����Ϣ
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�����Ϊ�̳�",true)]
		public FS.HISFC.Models.RADT.Patient Patient
		{
			get
			{
				return this.patient;
			}
			set
			{
				this.patient = value;
			}
		}
		
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new PatientInfo Clone()
        {
            #region addby xuewj 2010-8-30 ǳ��¡�޷���¡�������� {7CBA264D-7659-4cfb-9329-10F60A0A753D}
            //PatientInfo patientInfo = base.MemberwiseClone() as PatientInfo;
            PatientInfo patientInfo = base.Clone() as PatientInfo;
            #endregion
            patientInfo.FT = this.FT.Clone();
			patientInfo.PVisit = this.PVisit.Clone();
			//patientInfo.Caution = this.Caution.Clone();
			patientInfo.Kin = this.Kin.Clone();
			patientInfo.Disease = this.Disease.Clone();
			patientInfo.Diagnoses = ( System.Collections.ArrayList)this.Diagnoses.Clone();
            patientInfo.Surety = this.Surety.Clone();
//			obj.SIMainInfo = this.SIMainInfo.Clone();
			return patientInfo;
		}

		#endregion
	}
}
