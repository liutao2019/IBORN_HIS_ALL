using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
using System.Collections;
using System.Collections.Generic;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// Patient <br></br>
	/// [功能描述: 患者实体]<br></br>
	/// [创 建 者: 飞斯]<br></br>
	/// [创建时间: 2004-05]<br></br>
	/// <修改记录
	///		修改人='飞斯'
	///		修改时间='2006-09-11'
	///		修改目的='版本整合'
	///		修改描述=''
	///  />
	/// </summary>
    [System.ComponentModel.DisplayName("患者基本信息")]
    [Serializable]
	public class Patient : FS.HISFC.Models.Base.Spell
	{
		/// <summary>
		///患者类
		/// </summary>
		public Patient()
		{
		
		}

		
		#region 变量

		/// <summary>
		/// 患者各种编号
		/// </summary>
        private FS.HISFC.Models.RADT.PID pid;

		/// <summary>
		/// 社会保险号
		/// </summary>
		private string ssn;

        /// <summary>
        /// 记账单号
        /// </summary>
        private string lsh;

		/// <summary>
		/// 出生日期
		/// </summary>
		private System.DateTime birthday;

		/// <summary>
		/// 年龄
		/// </summary>
		private string age;

		/// <summary>
		/// 性别
		/// </summary>
        private SexEnumService sex;
		/// <summary>
		/// 家庭地址
		/// </summary>
		private string addressHome;

		/// <summary>
		/// 单位地址
		/// </summary>
		private string addressBusiness;

		/// <summary>
		/// 国家 
		/// </summary>
        private NeuObject country;

		/// <summary>
		/// 家庭邮编
		/// </summary>
		private string homeZip;

		/// <summary>
		/// 单位邮编
		/// </summary>
		private string businessZip;

		/// <summary>
		/// 家庭电话
		/// </summary>
		private string phoneHome;

		/// <summary>
		/// 单位电话
		/// </summary>
		private string phoneBusiness;

		/// <summary>
		/// 婚姻状态 
		/// </summary>
        private MaritalStatusEnumService maritalStatus;

		/// <summary>
		/// 身份证
		/// </summary>
		private string idCard;

		/// <summary>
		/// 民族
		/// </summary>
        private NeuObject nationality;

		/// <summary>
		/// 死亡时间
		/// </summary>
		private DateTime deathTime;

		/// <summary>
		/// 死亡证明人
		/// </summary>
        private NeuObject deathAttestor;

		/// <summary>
		/// 职业
		/// </summary>
        private NeuObject profession;

		/// <summary>
		/// 籍贯
		/// </summary>
		private string dist;

		/// <summary>
		/// 地区
		/// </summary>
		private string areaCode;

		/// <summary>
		/// 工作单位
		/// </summary>
		private string companyName;

		/// <summary>
		/// 身高
		/// </summary>
		private string height;

		/// <summary>
		/// 体重
		/// </summary>
		private string weight;

        /// <summary>
        /// 出生孕周(胎龄){A9327F43-F6C2-4230-BA64-48A11CEB6DD3}
        /// </summary>
        private string gestation;

        /// <summary>
        /// 血糖
        /// </summary>
        private string bloodGlu;

        /// <summary>
        /// 收缩压
        /// </summary>
        private string sbp;

        /// <summary>
        /// 收缩压
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
        /// 舒张压
        /// </summary>
        private string dbp;

        /// <summary>
        /// 舒张压
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
        /// 体温
        /// </summary>
        private string temperature;

        /// <summary>
        /// 体温
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
		/// 血型
		/// </summary>
        private BloodTypeEnumService bloodType;

		/// <summary>
		/// 合同单位
		/// </summary>
        private FS.HISFC.Models.Base.PactInfo pact;

		/// <summary>
		/// 婴儿数组 Patient
		/// </summary>
        private ArrayList baby;

		/// <summary>
		/// 是否是婴儿
		/// </summary>
		private bool isBaby;

		/// <summary>
		/// 是否有婴儿
		/// </summary>
		private bool isHasBaby;

		/// <summary>
		/// 婴儿个数
		/// </summary>
		private int babyCount;

		/// <summary>
		/// 最后结算序号
		/// </summary>
		private int balanceNO;

		/// <summary>
		/// 门诊诊断
		/// </summary>
		private string clinicDiagnose;

		/// <summary>
		/// 住院次数
		/// </summary>
		private int inTimes;

		/// <summary>
		/// 开据医师
		/// </summary>
        private FS.FrameWork.Models.NeuObject doctorReceiver;

		/// <summary>
		/// 病案状态: 0 无需病案 1 需要病案 2 医生站形成病案 3 病案室形成病案 4病案封存
		/// </summary>
		private string caseState;

		/// <summary>
		/// 患者住院主诊断
		/// </summary>
		private string mainDiagnose;

		/// <summary>
		/// 生育保险电脑号
		/// </summary>
		private string proCreateNO;

		/// <summary>
		/// 患者磁卡
		/// </summary>
        private FS.HISFC.Models.RADT.Card card;

       /// <summary>
       /// 解密后姓名
       /// </summary>
        private string decryptName;
        /// <summary>
        /// 是否姓名加密
        /// </summary>
        private bool isEncrypt = false;

        /// <summary>
        /// 数据库中Name字段实际存放的数据
        /// </summary>
        private string normalName;

        /// <summary>
        /// 证件类型
        /// </summary>
        private FS.FrameWork.Models.NeuObject idCardType;

        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        #region 
        /// <summary>
        /// 是否急诊患者
        /// </summary>
        private bool isTreatment = false;

        /// <summary>
        /// 是否VIP
        /// </summary>
        private bool vipFlag = false;

        /// <summary>
        /// 母亲姓名
        /// </summary>
        private string matherName;
        /// <summary>
        /// 病案号
        /// </summary>
        private string caseNO;
     
        /// <summary>
        /// 保险公司
        /// </summary>
        private NeuObject insurance;

        #endregion

        /// <summary>
        /// 是否是路径患者
        /// </summary>
        private bool isPtjtState;
        


        #endregion

        #region 属性

        /// <summary>
		/// 患者各种编号
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
		/// 社会保险号
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
        /// 记账单号
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

        [System.ComponentModel.DisplayName("出生日期")]
        [System.ComponentModel.Description("患者出生日期")]
		/// <summary>
		/// 出生日期
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

        [System.ComponentModel.DisplayName("年龄")]
        [System.ComponentModel.Description("患者年龄")]
		/// <summary>
		/// 年龄
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

        [System.ComponentModel.DisplayName("性别")]
        [System.ComponentModel.Description("患者性别")]
		/// <summary>
		/// 性别
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
		/// 家庭地址
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
		/// 单位地址
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
        [System.ComponentModel.DisplayName("国籍")]
        [System.ComponentModel.Description("患者国籍")]
		/// <summary>
		/// 国家
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
		/// 家庭邮编
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
		/// 单位邮编
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

        [System.ComponentModel.DisplayName("家庭电话")]
        [System.ComponentModel.Description("患者家庭电话")]
		/// <summary>
		/// 家庭电话
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

        [System.ComponentModel.DisplayName("单位电话")]
        [System.ComponentModel.Description("患者单位电话")]
		/// <summary>
		/// 单位电话
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

        [System.ComponentModel.DisplayName("婚姻状态")]
        [System.ComponentModel.Description("患者婚姻状态")]
		/// <summary>
		/// 婚姻状态
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

        [System.ComponentModel.DisplayName("身份证号")]
        [System.ComponentModel.Description("患者身份证号")]
		/// <summary>
		/// 证件号
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
        [System.ComponentModel.DisplayName("民族")]
        [System.ComponentModel.Description("患者民族")]
		/// <summary>
		/// 民族
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
		/// 死亡时间
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
		/// 死亡证明人
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

        [System.ComponentModel.DisplayName("职业")]
        [System.ComponentModel.Description("患者职业")]
		/// <summary>
		/// 职业
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

        [System.ComponentModel.DisplayName("籍贯")]
        [System.ComponentModel.Description("患者籍贯")]
		/// <summary>
		/// 籍贯
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
        [System.ComponentModel.DisplayName("出生地")]
        [System.ComponentModel.Description("患者出生地")]
		/// <summary>
		/// 地区
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

        [System.ComponentModel.DisplayName("工作单位")]
        [System.ComponentModel.Description("患者工作单位")]
		/// <summary>
		/// 工作单位
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
		/// 身高
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
		/// 体重
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
        /// 出生孕周{A9327F43-F6C2-4230-BA64-48A11CEB6DD3}
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
        /// 血糖
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
		/// 血型
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

        [System.ComponentModel.DisplayName("合同单位")]
        [System.ComponentModel.Description("患者合同单位")]
		/// <summary>
		/// 合同单位
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
		/// 婴儿数组
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
		/// 是否婴儿
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
		/// 是否有婴儿
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
		/// 婴儿个数
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
		/// 最后结算序号
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
		/// 门诊诊断
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
		/// 住院次数
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
		///  住院证开据医师
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
		/// 病案状态: 0 无需病案 1 需要病案 2 医生站形成病案 3 病案室形成病案 4病案封存
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
		/// 患者住院主诊断
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
		/// 生育保险电脑号
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
		/// 患者磁卡
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
//		/// 血型
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
        /// 解密后姓名(真实姓名)
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
        /// 是否加密
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
        /// 姓名和姓名密文
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
        /// 证件类型
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
        [System.ComponentModel.DisplayName("姓名")]
        [System.ComponentModel.Description("患者姓名")]
        /// <summary>
        /// 患者姓名
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
        /// 患者类别（VIP、院内职工等特殊优先患者标记）
        /// </summary>
        private string patientType;

        /// <summary>
        /// 患者类别（VIP、院内职工等特殊优先患者标记）
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
        /// 是否急诊患者
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
        /// 是否VIP
        /// </summary>
        public bool VipFlag
        {
            get { return vipFlag; }
            set { vipFlag = value; }
        }

        /// <summary>
        /// 母亲姓名
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
        /// 病案号
        /// </summary>
        [Obsolete("已经过时，更改为PID.CaseNO", true)]
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
        /// 保险公司
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
        /// 家属类
        /// </summary>
        private FS.HISFC.Models.RADT.Kin kin ;// new FS.HISFC.Models.RADT.Kin();

        /// <summary>
        /// 家属类
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
        /// 疾病护理信息类
        /// </summary>
        private FS.HISFC.Models.RADT.PDisease disease ;// new FS.HISFC.Models.RADT.PDisease();

        /// <summary>
        /// 疾病护理信息类
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
        /// 医保人员信息
        /// </summary>
        private FS.HISFC.Models.SIInterface.SIPersonInfo siPerson ;//new  FS.HISFC.Models.SIInterface.SIPersonInfo();
        /// <summary>
        /// 医保人员信息
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
        /// 是否是路径患者
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
        /// 通用实体
        /// </summary>
        private Object usualObject = null;
        /// <summary>
        /// 通用实体
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
        /// 商业保险公司编号
        /// </summary>
      //  public string BICompanyID { set; get; }

        /// <summary>
        /// 商业保险公司名称
        /// </summary>
        // public string BICompanyName { set; get; }

        #region 会员系统相关

        private string crmID = string.Empty;

        /// <summary>
        /// 会员系统ID
        /// </summary>
        public string CrmID
        {
            get { return crmID; }
            set { crmID = value; }
        }

        private NeuObject channel1 = new NeuObject();

        /// <summary>
        /// 一级渠道
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
        /// 二级渠道
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
        /// 三级渠道
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
        /// 客服
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
        /// 咨询
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
        /// 销售
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
        /// 医美销售
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
        /// 儿童标记
        /// </summary>
        public string ChildFlag
        {
            get { return childFlag; }
            set { childFlag = value; }
        }

        private string packageFlag = string.Empty;

        /// <summary>
        /// 套餐标记
        /// </summary>
        public string PackageFlag
        {
            get { return packageFlag; }
            set { packageFlag = value; }
        }

        private NeuObject familyInfo = new NeuObject();

        /// <summary>
        /// 家庭信息
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
        /// 家庭角色
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

        #region 方法

        #region 克隆
        /// <summary>
		/// 克隆
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
		
		#region 过期

		/// <summary>
		/// 检索码
		/// </summary>
        [Obsolete("已经过时，类本身已经实现检索码功能", true)]
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
		/// 身份证号
		/// </summary>
		[Obsolete("已经过时，更改为IDCard",true)]
		public string IDNo;

		/// <summary>
		/// 死亡时间
		/// </summary>
		[Obsolete("已经过时，更改为DeathTime", true)]
		public DateTime DeathDateTime;

		/// <summary>
		/// 死亡证明人
		/// </summary>
        [Obsolete("已经过时，更改为DeathAttestor", true)]
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
		/// 是否有婴儿
		/// </summary>
		[Obsolete("已经过时，更改为IsHasBaby", true)]
		public bool HasBaby=false;

		/// <summary>
		/// 最后结算序号
		/// </summary>
		[Obsolete("已经过时，更改为BalanceNO", true)]
		public int BalanceNo;

		/// <summary>
		/// 住院次数
		/// </summary>
		[Obsolete("已经过时，更改为InTimes", true)]
		public int In_Times;

		/// <summary>
		/// 生育保险电脑号
		/// </summary>
		[Obsolete("已经过时，更改为ProcreateNO", true)]
		public string ProCeatePcNo;

		#endregion
	}
}
