using System;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Account;
using System.Collections.Generic;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// Register<br></br>
    /// [功能描述: 挂号信息实体]<br></br>
    /// [创 建 者: 黄小卫]<br></br>
    /// [创建时间: 2007-2-1]<br></br>
    /// <修改记录
    ///		修改人='飞斯'
    ///		修改时间='2007-03-8'
    ///		修改目的='聚合增加患者优惠情况实体'
    ///		修改描述=''
    ///  />
    /// </summary>
    /// </summary>
    /// <修改记录
    ///		修改人='周雪松'
    ///		修改时间='2007-10-22'
    ///		修改目的='聚合患者访问类'
    ///		修改描述=''
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
            // TODO: 在此处添加构造函数逻辑
            // 
        }

        #region 变量

        /// <summary>
        /// 证件类型
        /// </summary>
        private FS.FrameWork.Models.NeuObject cardType;

        /// <summary>
        /// 患者来源
        /// </summary>
        private FS.FrameWork.Models.NeuObject inSource;

        /// <summary>
        /// 交易类型
        /// </summary>
        private TransTypes tranType = TransTypes.Positive;

        /// <summary>
        /// 看诊信息
        /// </summary>
        private Schema doctor;

        /// <summary>
        /// 每日流水号
        /// </summary>
        private int orderNO = 0;

        /// <summary>
        /// 挂号费
        /// </summary>
        private RegLvlFee regLvlFee;

        /// <summary>
        /// 自费
        /// </summary>
        private decimal ownCost;
        /// <summary>
        /// 自负
        /// </summary>
        private decimal payCost;
        /// <summary>
        /// 记帐
        /// </summary>
        private decimal pubCost;

        /// <summary>
        /// 是否急诊
        /// </summary>
        private bool isEmergency;

        /// <summary>
        /// 是否收费
        /// </summary>
        private bool isFee;

        /// <summary>
        /// 挂号类别
        /// </summary>
        private EnumRegType regType = EnumRegType.Reg;

        /// <summary>
        /// 是否初诊
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// 是否看诊
        /// </summary>
        private bool isSee = false;

        /// <summary>
        /// 挂号状态
        /// </summary>
        private EnumRegisterStatus status = EnumRegisterStatus.Valid;

        /// <summary>
        /// 发票号/处方号
        /// </summary>
        private string invoiceNO;
        /// <summary>
        /// 处方号 by niuxinyuan  2007-05-15
        /// </summary>
        private string recipeNO;

        /// <summary>
        /// 已打印发票数量
        /// </summary>
        private int printInvoiceCnt = 0;

        /// <summary>
        /// 录入人
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment inputOper;

        /// <summary>
        /// 作废人
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment cancelOper;

        /// <summary>
        /// 日结算操作信息
        /// </summary>
        private OperStat balanceOperStat;

        /// <summary>
        /// 核查日结操作信息
        /// </summary>
        private OperStat checkOperStat;

        /// <summary>
        /// 是否分诊
        /// </summary>
        private bool isTriage = false;

        /// <summary>
        /// 分诊操作员环境变量
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment triageOper;

        /// <summary>
        /// 门诊患者优惠情况
        /// </summary>
        private FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate;
        ///// <summary>
        /////是向病案室去传递病案标记
        ///// </summary>
        //private bool isSendInhosCase;

        /// <summary>
        /// 患者访问类
        /// </summary>
        private FS.HISFC.Models.RADT.PVisit pVisit;

        /// <summary>
        /// 优惠金额{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        private decimal ecoCost = 0m;

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// 是否是账户流程挂号
        /// </summary>
        private bool isAccount;

        /// <summary>
        /// 扩展字段1{921FBFCA-3D0D-4bc6-8EEA-A9BBE152E69A}
        /// </summary>
        private string mark1;


        /// <summary>
        /// 医生看诊信息
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment seeDoct;

        /// <summary>
        /// 上传标志
        /// </summary>
        private string upFlag;

        private List<AccountCardFee> lstCardFee;

        private RegisterExtend regExtend;


        /// <summary>
        /// 院区id{3515892E-1541-47de-8E0B-E306798A358C}
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// 院区名
        /// </summary>
        private string hospital_name;


        #endregion

        #region 属性

        /// <summary>
        /// 预约挂号的业务单号
        /// </summary>
        private string bookNo = string.Empty;

        /// <summary>
        /// 预约挂号的业务单号
        /// </summary>
        public string BookNo
        {
            get { return bookNo; }
            set { bookNo = value; }
        }

        /// <summary>
        /// 门诊患者优惠情况
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
        /// 证件类型
        /// </summary>
        [Obsolete("CardType已经作废，证件类型请使用IDCardType", false)]
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
        /// 患者来源
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
        ///交易类型
        ///</summary>
        public TransTypes TranType
        {
            get { return tranType; }
            set { tranType = value; }
        }

        /// <summary>
        /// 看诊信息
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
        /// 每日流水号
        /// </summary>
        public int OrderNO
        {
            get { return orderNO; }
            set { orderNO = value; }
        }

        /// <summary>
        /// 挂号费
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
        /// 自费
        /// </summary>
        public decimal OwnCost
        {
            get { return this.ownCost; }
            set { this.ownCost = value; }
        }

        /// <summary>
        /// 自负
        /// </summary>
        public decimal PayCost
        {
            get { return this.payCost; }
            set { this.payCost = value; }
        }

        /// <summary>
        /// 记帐
        /// </summary>
        public decimal PubCost
        {
            get { return this.pubCost; }
            set { this.pubCost = value; }
        }

        /// <summary>
        /// 是否急诊
        /// </summary>
        /// {156C449B-60A9-4536-B4FB-D00BC6F476A1}
        [Obsolete("更改为：DoctorInfo.Templet.RegLevel.IsEmergency", true)]
        public bool IsEmergency
        {
            get { return isEmergency; }
            set { isEmergency = value; }
        }

        /// <summary>
        /// 是否收费
        /// </summary>
        public bool IsFee
        {
            get { return isFee; }
            set { isFee = value; }
        }

        /// <summary>
        /// 挂号类别
        /// </summary>
        public EnumRegType RegType
        {
            get { return this.regType; }
            set { this.regType = value; }
        }

        /// <summary>
        /// 是否初诊
        /// </summary>
        public bool IsFirst
        {
            get { return isFirst; }
            set { isFirst = value; }
        }

        /// <summary>
        /// 是否看诊
        /// </summary>
        public bool IsSee
        {
            get { return isSee; }
            set { isSee = value; }
        }

        /// <summary>
        /// 挂号状态
        /// </summary>
        public EnumRegisterStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        /// <summary>
        /// 发票号/处方号
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
        /// 发票号/处方号
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
        /// 已打印发票次数
        /// </summary>
        public int PrintInvoiceCnt
        {
            get { return this.printInvoiceCnt; }
            set { this.printInvoiceCnt = value; }
        }

        /// <summary>
        /// 挂号操作员
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
        /// 作废操作员
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
        /// 日结核查操作信息
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
        /// 日结算操作信息
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
        /// 是否分诊
        /// </summary>
        public bool IsTriage
        {
            get { return this.isTriage; }
            set { this.isTriage = value; }
        }

        /// <summary>
        /// 分诊人
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

        //----------------------------------------------------------可自行在下面添加

        /// <summary>
        /// 医疗类别
        /// </summary>        
        public string MedicalType = "";

        /// <summary>
        /// 体检类型, 团体、个人
        /// </summary>        
        public string ChkKind = "";

        /// <summary>
        /// 医保登记信息
        /// </summary>
        private FS.HISFC.Models.SIInterface.SIMainInfo siInfo;

        /// <summary>
        /// 医保登记信息
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
        /// 看诊医生信息：看诊医生、看诊时间、看诊医生科室
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
        /// 患者访问类
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
        /// 优惠金额{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        public decimal EcoCost
        {
            get { return ecoCost; }
            set { ecoCost = value; }
        }

        ////{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// 是否是账户流程挂号
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
        /// 扩展字段1{921FBFCA-3D0D-4bc6-8EEA-A9BBE152E69A}
        /// </summary>
        public string Mark1
        {
            get { return mark1; }
            set { mark1 = value; }
        }

        //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
        /// <summary>
        /// 一级病种
        /// </summary>
        private string class1desease;

        public string Class1Desease
        {
            get { return this.class1desease; }
            set { this.class1desease = value; }
        }

        //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
        /// <summary>
        /// 二级病种
        /// </summary>
        private string class2desease;

        public string Class2Desease
        {
            get { return this.class2desease; }
            set { this.class2desease = value; }
        }

        /// <summary>
        /// 上传标志 应急库上传到正式库
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
        /// 此挂号记录相关费用信息
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

        // //{AE399953-4F87-4199-8060-EFDC16AFAAF3} 综合门诊添加实际医生
        /// <summary>
        /// 实际挂号医生名称
        /// </summary>
        public string RealDoctorName { set; get; }

        /// <summary>
        /// 实际挂号医生id
        /// </summary>
        public string RealDoctorID { set; get; }


        /// <summary>
        /// 院区id{3515892E-1541-47de-8E0B-E306798A358C}
        /// </summary>
        public string Hospital_id
        {
            get { return this.hospital_id; }
            set { this.hospital_id = value; }
        }


        /// <summary>
        /// 院区名
        /// </summary>
        public string Hospital_name
        {
            get { return this.hospital_name; }
            set { this.hospital_name = value; }
        }




        #region
        //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 
        //增加院级初诊，大科级初诊，医生初诊属性

        /// <summary>
        /// 院级初诊
        /// </summary>
        private string hospitalFirstVisit = "0";

        public string HospitalFirstVisit
        {
            get { return hospitalFirstVisit; }
            set { hospitalFirstVisit = value; }
        }

        /// <summary>
        /// 大科级初诊
        /// </summary>
        private string rootDeptFirstVisit = "0";

        public string RootDeptFirstVisit
        {
            get { return rootDeptFirstVisit; }
            set { rootDeptFirstVisit = value; }
        }

        /// <summary>
        /// 医生级初诊
        /// </summary>
        private string doctFirstVist = "0";

        public string DoctFirstVist
        {
            get { return doctFirstVist; }
            set { doctFirstVist = value; }
        }

        #endregion

        #region CRM分诊信息
        /// <summary>
        /// 分诊标识 0-未分诊 1-分诊
        /// </summary>
        public string AssignFlag { get; set; }
        /// <summary>
        /// 分诊状态 0-未分诊 1-待诊 2-进诊 3-诊出 4-过号
        /// </summary>
        public string AssignStatus { get; set; }
        /// <summary>
        /// 首诊标识1-是 0-否
        /// </summary>
        public string FirstSeeFlag { get; set; }
        /// <summary>
        /// 优先标识1-是 0-否
        /// </summary>
        public string PreferentialFlag { get; set; }
        /// <summary>
        /// 看诊序号
        /// </summary>
        public int SequenceNO { get; set; }
        #endregion

        #endregion

        #region 方法
        ///// <summary>
        /////  挂号的副本
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

        #region  作废

        /// <summary>
        /// 病历号
        /// </summary>
        [Obsolete("更改为：PID.CardNO", true)]
        public string CardNo;

        /// <summary>
        /// 身份证号
        /// </summary>
        [Obsolete("更改为:IDCard", true)]
        public string IdenNo;

        /// <summary>
        /// 性别代码
        /// </summary>
        [Obsolete("更改为：Sex.ID", true)]
        public string SexID;

        /// <summary>
        /// 联系电话
        /// </summary>
        [Obsolete("更改为：PhoneHome", true)]
        public string Phone;

        /// <summary>
        /// 地址
        /// </summary>
        [Obsolete("更改为：AddressHome", true)]
        public string Address;

        /// <summary>
        /// 挂号日期
        /// </summary>
        [Obsolete("更改为:DoctorInfo.SeeDate", true)]
        public DateTime RegDate;

        /// <summary>
        /// 午别
        /// </summary>
        [Obsolete("更改为：DoctorInfo.Templet.Noon.ID", true)]
        public string Noon;

        /// <summary>
        /// 开始时间
        /// </summary>
        [Obsolete("更改为：DoctorInfo.Templet.Begin", true)]
        public DateTime BeginTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        [Obsolete("更改为：DoctorInfo.Templet.End", true)]
        public DateTime EndTime;

        /// <summary>
        /// 结算类别
        /// </summary>
        [Obsolete("更改为：Pact.PayKind", true)]
        public FS.FrameWork.Models.NeuObject PayKind;

        /// <summary>
        /// 挂号级别
        /// </summary>
        [Obsolete("更改为：DoctorInfo.Templet.RegLevel", true)]
        public FS.FrameWork.Models.NeuObject RegLevel;

        /// <summary>
        /// 挂号科室
        /// </summary>
        [Obsolete("更改为：DoctorInfo.Templet.Dept", true)]
        public FS.FrameWork.Models.NeuObject RegDept;

        /// <summary>
        /// 看诊医生
        /// </summary>
        [Obsolete("更改为：DoctorInfo.Templet.Doct", true)]
        public FS.FrameWork.Models.NeuObject RegDoct;

        /// <summary>
        /// 看诊序号
        /// </summary>
        [Obsolete("更改为：DoctorInfo.SeeNO", true)]
        public int SeeID;

        /// <summary>
        /// 排班序号
        /// </summary>
        [Obsolete("更改为：DoctorInfo.ID", true)]
        public string SchemaNo;

        /// <summary>
        /// 是否加号
        /// </summary>
        [Obsolete("更改为：DoctorInfo.Templet.IsAppend", true)]
        public bool IsAppend;

        /// <summary>
        /// 挂号费
        /// </summary>
        [Obsolete("更改为：RegLvlFee.RegFee", true)]
        public decimal RegFee;

        /// <summary>
        /// 检查费
        /// </summary>
        [Obsolete("更改为：RegLvlFee.ChkFee", true)]
        public decimal ChkFee;

        /// <summary>
        /// 诊察费
        /// </summary>
        [Obsolete("更改为：RegLvlFee.OwnDigFee", true)]
        public decimal DigFee;

        /// <summary>
        /// 其他费
        /// </summary>
        [Obsolete("更改为：RegLvlFee.OthFee", true)]
        public decimal OthFee;

        /// <summary>
        /// 是否急诊
        /// </summary>
        [Obsolete("更改为：IsEmergency", true)]
        public bool IsUrg;

        /// <summary>
        /// 是否有效
        /// </summary>
        [Obsolete("更改为：Status", true)]
        public bool IsValid;

        /// <summary>
        /// 挂号类别
        /// </summary>
        [Obsolete("更改为：RegType", true)]
        public bool IsPre;

        /// <summary>
        /// 医疗证号
        /// </summary>
        [Obsolete("更改为：SSN", true)]
        public string McardID;
        #endregion
    }
}
