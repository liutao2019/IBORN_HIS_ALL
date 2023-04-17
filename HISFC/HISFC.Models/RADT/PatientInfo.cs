using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// PatientInfo <br></br>
	/// [功能描述: 患者综合实体]<br></br>
	/// [创 建 者: 飞斯]<br></br>
	/// [创建时间: 2004-05]<br></br>
    /// User01 User02 User03 已被住院预约登记使用
	/// <修改记录
    /// 
	///		修改人='飞斯'
	///		修改时间='2006-09-11'
	///		修改目的='版本整合'
	///		修改描述=''
	///  />
	/// </summary>
    [Serializable]
    public class PatientInfo : FS.HISFC.Models.RADT.Patient 
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public PatientInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		
		}

		#region 变量

		/// <summary>
		/// 患者费用信息
		/// </summary>
		private FT ft;// new FT();

		/// <summary>
		/// 患者访问类
		/// </summary>
		private FS.HISFC.Models.RADT.PVisit pVisit;// new FS.HISFC.Models.RADT.PVisit();

		/// <summary>
		/// 担保类型
		/// </summary>
		private FS.HISFC.Models.RADT.Caution caution;// new FS.HISFC.Models.RADT.Caution();

		/// <summary>
		/// 家属类
		/// </summary>
        private FS.HISFC.Models.RADT.Kin kin;// new Kin();

		/// <summary>
		/// 结算类别  01-自费  02-保险 03-公费在职 04-公费退休 05-公费高干               
		/// </summary>
		private FS.HISFC.Models.Base.PayKind payKind ;// new FS.HISFC.Models.Base.PayKind();

		/// <summary>
		/// 疾病护理信息类
		/// </summary>
		private FS.HISFC.Models.RADT.PDisease disease;// new PDisease();

		/// <summary>
		/// 诊断
		/// </summary>
		private System.Collections.ArrayList diagnoses;// new System.Collections.ArrayList(); 

        /// <summary>
        /// 描述诊断
        /// </summary>
        private string msdiagnoses;

		/// <summary>
		/// 医保患者基本信息,结算信息
		/// </summary>
		private FS.HISFC.Models.SIInterface.SIMainInfo siMainInfo;// new FS.HISFC.Models.SIInterface.SIMainInfo();

		/// <summary>
		/// 扩展标记  目前用于中山一院 标志公医超日限额是否同意：0不同意，1同意
		/// </summary>
		private string extendFlag;

		/// <summary>
		/// 扩展标记1
		/// </summary>
		private string extendFlag1;

		/// <summary>
		/// 扩展标记2
		/// </summary>
		private string extendFlag2;

        /// <summary>
        /// 患者住院号类型
        /// </summary>
        private EnumPatientNOType patientNOType;
        /// <summary>
        /// 结算时间
        /// </summary>
        private DateTime balanceDate = DateTime.MinValue;
        /// <summary>
        /// 担保
        /// </summary>
        private FS.HISFC.Models.Fee.Surety surety ;// new FS.HISFC.Models.Fee.Surety();

        /// <summary>
        /// 封帐标志{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
        /// </summary>
        private bool isStopAcount;

        /// <summary>
        /// 姓（国外）{AB392EE7-0666-4456-B29F-458730318812}
        /// </summary>
        private string last_Name="";
        
        /// <summary>
        /// 名（国外）{AB392EE7-0666-4456-B29F-458730318812}
        /// </summary>
        private string first_Name="";

        /// <summary>
        /// middleName(国外){AB392EE7-0666-4456-B29F-458730318812}
        /// </summary>
        private string middle_Name="";

        /// <summary>
        /// 城市{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string city = "";
        
        /// <summary>
        /// 省{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string state = "";
       
        /// <summary>
        /// 手机{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string mobile = "";
        
        /// <summary>
        /// 有几个孩子{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string noOfChildren = "";
       
        /// <summary>
        /// 工作地址{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string workAddress = "";
        
        /// <summary>
        /// 紧急联系人（本国）{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string linkManHome = "";
        
        /// <summary>
        /// 紧急联系人（本国）{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string linkPhoneHome = "";
        
        /// <summary>
        /// 从何途径获知我们的医疗中心{61A874F4-EC08-4c82-A9AF-26B9C097A8C8}
        /// </summary>
        private string knowWay = "";


        /// <summary>
        /// 过敏源
        /// </summary>
        private string allergyInfo = "";
        /// <summary>
        /// 患者类型  // {F6204EF5-F295-4d91-B81A-736A268DD394}
        /// </summary>
        private FS.FrameWork.Models.NeuObject patientType;
        /// <summary>
        /// 分娩方式 
        /// </summary>
        private FS.FrameWork.Models.NeuObject deliveryMode;

        /// <summary>
        /// 家庭号 
        /// </summary>
        private string familyCode = string.Empty;
        /// <summary>
        /// 家庭名称 
        /// </summary>
        private string familyName = string.Empty;

        /// <summary>
        /// 其他卡号 
        /// </summary>
        private string otherCardNo = string.Empty;

        /// <summary>
        /// 转诊人 
        /// </summary>
        private string referralPerson = string.Empty;
        /// <summary>
        /// 客服专员 
        /// </summary>
        private FS.FrameWork.Models.NeuObject serviceInfo;

        /// <summary>
        /// 患者来源 
        /// </summary>
        private FS.FrameWork.Models.NeuObject patientSourceInfo;

        /// <summary>
        /// 开发渠道 
        /// </summary>
        private FS.FrameWork.Models.NeuObject channelInfo;
        
       // {23F37636-DC34-44a3-A13B-071376265450}
        /// <summary>
        /// 院区id
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// 院区名
        /// </summary>
        private string hospital_name;

		#endregion

        #region 属性


        /// <summary>
        /// 家庭名称
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
        /// 家庭号
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
        /// 其他卡号
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
        /// 转诊人
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
        /// 客服专员
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
        /// 患者来源
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
        /// 开发渠道
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
        /// 分娩方式// {F6204EF5-F295-4d91-B81A-736A268DD394}
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
        /// 患者类型// {F6204EF5-F295-4d91-B81A-736A268DD394}
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
        /// 过敏源
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
		/// 患者费用信息
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
		/// 患者访问类
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
		/// 担保类型
		/// </summary>
        [Obsolete("已经过期,替换为Surety", true)]
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
		/// 结算类别  01-自费  02-保险 03-公费在职 04-公费退休 05-公费高干
		/// </summary>
		[Obsolete("已经过期，在Patient属性中的Pact已经包含", true)]
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
		/// 诊断
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
        /// 描述诊断
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
		/// 医保患者基本信息,结算信息
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
		/// 扩展标记  目前用于中山一院 标志公医超日限额是否同意：0不同意，1同意
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
		/// 扩展标记1
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
		/// 扩展标记2
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
        /// 患者住院号类型
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
        /// 结算时间
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
        /// 担保
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
        /// 封帐标志{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
        /// </summary>
        public bool IsStopAcount
        {
            get { return isStopAcount; }
            set { isStopAcount = value; }
        }

        /// <summary>
        /// middleName(国外)
        /// </summary>
        public string Middle_Name
        {
            get { return middle_Name; }
            set { middle_Name = value; }
        }

        /// <summary>
        /// 姓（国外）
        /// </summary>
        public string Last_Name
        {
            get { return last_Name; }
            set { last_Name = value; }
        }

        /// <summary>
        /// 名（国外）
        /// </summary>
        public string First_Name
        {
            get { return first_Name; }
            set { first_Name = value; }
        }

        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        /// <summary>
        /// 省
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }


        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        /// <summary>
        /// 有几个孩子
        /// </summary>
        public string NoOfChildren
        {
            get { return noOfChildren; }
            set { noOfChildren = value; }
        }

        /// <summary>
        /// 工作地址
        /// </summary>
        public string WorkAddress
        {
            get { return workAddress; }
            set { workAddress = value; }
        }

        /// <summary>
        /// 紧急联系人（本国）
        /// </summary>
        public string LinkManHome
        {
            get { return linkManHome; }
            set { linkManHome = value; }
        }

        /// <summary>
        /// 紧急联系人（本国）
        /// </summary>
        public string LinkPhoneHome
        {
            get { return linkPhoneHome; }
            set { linkPhoneHome = value; }
        }

        /// <summary>
        /// 从何途径获知我们的医疗中心
        /// </summary>
        public string KnowWay
        {
            get { return knowWay; }
            set { knowWay = value; }
        }

        /// <summary>
        /// 医保待遇类型：MII001严重高危妊娠，MII005剖宫产，MII010阴式分娩（含妊娠7个月以上引产），MII015分娩期住院，MII020	妊娠3个月以上引产（住院），MII025妊娠3个月以下人流（住院）
        /// </summary>
        public string HealthTreamType{ get; set; }

        //{23F37636-DC34-44a3-A13B-071376265450}
        /// <summary>
        ///院区id
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
        /// 院区名
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

		#region 过期

		/// <summary>
		/// 患者费用信息
		/// </summary>
		[Obsolete("已经过期，更改为FT", true)]
		public FT Fee=new FT();

		/// <summary>
		/// 患者基本信息
        /// </summary>
        [System.Obsolete("已经过期，更改为继承",true)]
		private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

		/// <summary>
		/// 患者基本信息
		/// </summary>
		[System.Obsolete("已经过期，更改为继承",true)]
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

		#region 方法

		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns></returns>
		public new PatientInfo Clone()
        {
            #region addby xuewj 2010-8-30 浅克隆无法克隆引用类型 {7CBA264D-7659-4cfb-9329-10F60A0A753D}
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
