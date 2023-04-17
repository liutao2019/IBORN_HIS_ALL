using System;
using FS.FrameWork.Models;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// SIMainInfo 的摘要说明。
	/// Id inpatientNo, name 患者姓名
	/// </summary>
    [Serializable]
    public class SIMainInfo : FS.FrameWork.Models.NeuObject
    {
        public SIMainInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 扩展属性
        private System.Collections.Generic.Dictionary<string, NeuObject> extendProperty = new System.Collections.Generic.Dictionary<string, NeuObject>();
        /// <summary>
        /// 扩展属性
        /// </summary>
        public System.Collections.Generic.Dictionary<string, NeuObject> ExtendProperty
        {
            get { return extendProperty; }
            set { extendProperty = value; }
        }

        private string tacCode;

        /// <summary>
        /// tac码
        /// 珠海医保交易验证码
        /// </summary>
        public string TacCode
        {
            get { return tacCode; }
            set { tacCode = value; }
        }

        #endregion

        private int feeTimes;
        /// <summary>
        /// 费用批次
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
        /// 读入标志
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
        /// 就诊登记号、铁路医保个人编号
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
        /// 医院编号
        /// </summary>
        public string HosNo
        {
            set { hosNo = value; }
            get { return hosNo; }
        }

        private string balNo;
        /// <summary>
        ///  结算序号
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
        /// 主发票号
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        private FS.FrameWork.Models.NeuObject medicalType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 医疗类别 1-住院 2 -门诊特定项目
        /// </summary>
        public FS.FrameWork.Models.NeuObject MedicalType
        {
            get { return medicalType; }
            set { medicalType = value; }
        }
        //		private string patientNo;
        //		/// <summary>
        //		/// 住院号
        //		/// </summary>
        //		public string PatientNo
        //		{
        //			get{return patientNo;}
        //			set{patientNo = value;}
        //		}
        //		private string cardNo;
        //		/// <summary>
        //		/// 就诊卡号
        //		/// </summary>
        //		public string CardNo
        //		{
        //			get{return cardNo;}
        //			set{cardNo = value;}
        //		}
        //		private string mCardNo;
        //		/// <summary>
        //		/// 医疗证号
        //		/// </summary>
        //		public string MCardNo
        //		{
        //			get{return mCardNo;}
        //			set{mCardNo = value;}
        //		}
        private string proceatePcNo;
        /// <summary>
        /// 生育保险患者电脑号
        /// </summary>
        public string ProceatePcNo
        {
            get { return proceatePcNo; }
            set { proceatePcNo = value; }
        }
        private DateTime siBeginDate;
        /// <summary>
        /// 参保日期
        /// </summary>
        public DateTime SiBegionDate
        {
            get { return siBeginDate; }
            set { siBeginDate = value; }
        }
        private string siState;
        /// <summary>
        /// 参保状态 3-参保缴费、4-暂停缴费、7-终止参保
        /// </summary>
        public string SiState
        {
            get { return siState; }
            set { siState = value; }
        }
        private string emplType;
        /// <summary>
        /// 人员类别 1-在职、2-退休
        /// </summary>
        public string EmplType
        {
            get { return emplType; }
            set { emplType = value; }
        }
        private string clinicDiagNose;
        /// <summary>
        /// 门诊诊断
        /// </summary>
        public string ClinicDiagNose
        {
            get { return clinicDiagNose; }
            set { clinicDiagNose = value; }
        }
        private DateTime inDiagnoseDate;
        /// <summary>
        /// 入院诊断日期
        /// </summary>
        public DateTime InDiagnoseDate
        {
            get { return inDiagnoseDate; }
            set { inDiagnoseDate = value; }
        }

        private FS.FrameWork.Models.NeuObject inDiagnose = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 入院诊断信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject InDiagnose
        {
            get { return inDiagnose; }
            set { inDiagnose = value; }
        }

        private decimal totCost;
        /// <summary>
        /// 住院总金额
        /// </summary>
        public decimal TotCost
        {
            get { return totCost; }
            set { totCost = value; }
        }
        private decimal addTotCost = 0;
        /// <summary>
        /// 费用累计
        /// </summary>
        public decimal AddTotCost
        {
            get { return addTotCost; }
            set { addTotCost = value; }
        }
        private decimal payCost;
        /// <summary>
        /// 帐户支付金额
        /// </summary>
        public decimal PayCost
        {
            get { return payCost; }
            set { payCost = value; }
        }

        /// <summary>
        /// 社保支付金额(除自费和账户支付的所有金额的合计)
        /// </summary>
        private decimal pubCost;
        /// <summary>
        /// 社保支付金额(除自费和账户支付的所有金额的合计)
        /// </summary>
        public decimal PubCost
        {
            get { return pubCost; }
            set { pubCost = value; }
        }
        //{06A3389F-B19E-4482-A55C-89269995B142}
        /// <summary>
        /// 医保返回的统筹金额
        /// </summary>
        private decimal siPubCost;

        /// <summary>
        /// 医保返回的统筹金额
        /// </summary>
        public decimal SiPubCost
        {
            get { return this.siPubCost; }
            set { this.siPubCost = value; }

        }

        private decimal itemPayCost;
        /// <summary>
        /// 部分项目自付金额 
        /// </summary>
        public decimal ItemPayCost
        {
            get { return itemPayCost; }
            set { itemPayCost = value; }
        }
        private decimal baseCost;
        /// <summary>
        /// 个人起付金额
        /// </summary>
        public decimal BaseCost
        {
            get { return baseCost; }
            set { baseCost = value; }
        }
        private decimal ownCost;
        /// <summary>
        /// 个人自费项目金额
        /// </summary>
        public decimal OwnCost
        {
            get { return ownCost; }
            set { ownCost = value; }
        }
        private decimal itemYLCost;
        /// <summary>
        /// 个人自付金额（乙类自付部分）
        /// </summary>
        public decimal ItemYLCost
        {
            get { return itemYLCost; }
            set { itemYLCost = value; }
        }

        private decimal pubOwnCost;
        /// <summary>
        /// 个人自负金额
        /// </summary>
        public decimal PubOwnCost
        {
            set { pubOwnCost = value; }
            get { return pubOwnCost; }
        }

        private decimal overTakeOwnCost;
        /// <summary>
        /// 超统筹支付限额个人自付金额
        /// </summary>
        public decimal OverTakeOwnCost
        {
            get { return overTakeOwnCost; }
            set { overTakeOwnCost = value; }
        }

        private decimal hosCost;
        /// <summary>
        /// 医药机构分担金额
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
        /// 操作员信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperInfo
        {
            get { return operInfo; }
            set { operInfo = value; }
        }
        private DateTime operDate;
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }
        private int appNo;
        /// <summary>
        /// 审批号
        /// </summary>
        public int AppNo
        {
            get { return appNo; }
            set { appNo = value; }
        }
        private DateTime balanceDate;
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime BalanceDate
        {
            get { return balanceDate; }
            set { balanceDate = value; }
        }
        private decimal yearCost;
        /// <summary>
        /// 本年度可用定额
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
        /// 出院诊断
        /// </summary>
        public FS.FrameWork.Models.NeuObject OutDiagnose
        {
            set { outDiagnose = value; }
            get { return outDiagnose; }
        }

        private bool isValid;
        /// <summary>
        /// 是否有效 True有效 False 无效
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
        /// 是否结算 True 结算 False 未结算
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

        #region 铁路医保附加属性
        #region 变量
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
        /// IC卡号码
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
        /// 人员类别
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
        /// 公务员级别
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
        /// 特殊照顾人群
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
        /// 职务
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
        /// 异地安置城市
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
        /// 参保人单位
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
        /// 个人帐户余额
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
        /// 已冻结基金信息
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
        /// 申请序号
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
        /// 疾病
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
        /// 申请类型
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
        /// 基金
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
        /// 业务序号
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
        /// 特殊工种
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
        /// 医院费用序列号
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
        /// 对应费用序列号
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

        #region 沈阳医保增加属性

        #region 变量

        /// <summary>
        /// 发卡机构编码
        /// </summary>
        private string cardOrgID = string.Empty;

        /// <summary>
        /// 卡有效期
        /// </summary>
        private DateTime cardValidTime = DateTime.MinValue;

        /// <summary>
        /// 变更日期
        /// </summary>
        private DateTime shiftTime = DateTime.MinValue;

        /// <summary>
        /// 是否卡已经锁定
        /// </summary>
        private bool isCardLocked = false;

        /// <summary>
        /// 本年统筹支出累计
        /// </summary>
        private decimal yearPubCost = 0;

        /// <summary>
        /// 本年救助金支出累计
        /// </summary>
        private decimal yearHelpCost = 0;

        /// <summary>
        /// 转出医院起伏标准
        /// </summary>
        private decimal turnOutHosStandardCost = 0;

        /// <summary>
        /// 转出医院起伏标准自付
        /// </summary>
        private decimal turnOutHosOnwCost = 0;

        /// <summary>
        /// 住院次数
        /// </summary>
        private int inHosTimes = 0;

        /// <summary>
        /// 账户支付累计
        /// </summary>
        private decimal payAddCost = 0;

        /// <summary>
        /// 账户支付年度
        /// </summary>
        private string payYear = string.Empty;

        /// <summary>
        /// 现金支付金额累计
        /// </summary>
        private decimal ownCashAddCost = 0;

        /// <summary>
        /// 个人自负(乙类项目)金额累计
        /// </summary>
        private decimal ownAddCost = 0;
        /// <summary>
        /// 年度个人自付累计
        /// </summary>
        private decimal yearOwnAddCost = 0;

        /// <summary>
        /// 公务员支付金额累计
        /// </summary>
        private decimal gwyPayAddCost = 0;

        /// <summary>
        /// 特殊门诊支付累计
        /// </summary>
        private decimal spOutpatientPayAddCost = 0;

        /// <summary>
        /// 门诊慢性病支付累计
        /// </summary>
        private decimal slowOutpatientPayAddCost = 0;
        /// <summary>
        /// 帐户注入累计
        /// </summary>
        private decimal yearAddPayCost = 0;
        /// <summary>
        ///  帐户注入刷新日期
        /// </summary>
        private DateTime freshAddPayDate = DateTime.MinValue;
        /// <summary>
        /// 结转帐户支出累计
        /// </summary>
        private decimal yearAddPayTurnCost = 0;

        #endregion

        #region 属性

        /// <summary>
        /// 发卡机构编码
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
        /// 卡有效期
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
        /// 变更日期
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
        /// 是否卡已经锁定
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
        /// 本年统筹支出累计
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
        /// 本年救助金支出累计
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
        /// 转出医院起伏标准
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
        /// 转出医院起伏标准自付
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
        /// 住院次数
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
        /// 账户支付累计
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
        /// 账户支付年度
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
        /// 现金支付金额累计
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
        /// 个人自负(乙类项目)金额累计
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
        /// 年度个人自付累计
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
        /// 公务员支付金额累计
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
        /// 特殊门诊支付累计
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
        /// 门诊慢性病支付累计
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
        /// 帐户注入累计
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
        /// 帐户注入刷新日期
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
        /// 结转帐户支出累计
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
        /// 是否公务员
        /// </summary>
        private bool isOffice = false;
        /// <summary>
        /// 是否公务员
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
        /// 医保住院状态
        /// </summary>
        private string inStateForYB = string.Empty;
        /// <summary>
        /// 医保住院状态
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
        /// 出生地
        /// </summary>
        private string birthPlace = string.Empty;
        /// <summary>
        /// 出生地
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
        /// 离院日期
        /// </summary>
        private DateTime leaveHosDate = DateTime.MinValue;
        /// <summary>
        /// 离院日期
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
        /// 家庭病床支出累计
        /// </summary>
        private decimal homeBedFeeAddCost = 0;
        /// <summary>
        /// 家庭病床支出累计
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
        /// 超过最高限额公务员补助支出累计(26) 
        /// </summary>
        private decimal gwyBeyondPayAddCost = 0;
        /// <summary>
        /// 超过最高限额公务员补助支出累计(26) 
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
        /// 离休统筹支出累计
        /// </summary>
        private decimal lxAddPubCost = 0;
        /// <summary>
        /// 离休统筹支出累计
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
        /// 门诊现金支出累计
        /// </summary>
        private decimal cashAddCostForMZ = 0;
        /// <summary>
        /// 门诊现金支出累计
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
        /// 门诊公务员补助支出累计
        /// </summary>

        private decimal officalSupplyCostForMZ = 0;
        /// <summary>
        /// 门诊公务员补助支出累计
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
        /// 生育保险是否最后结算标志
        /// </summary>
        private bool proceateLastFlag = false;
        /// <summary>
        /// 生育保险是否最后结算标志
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
        /// 大额补助
        /// </summary>
        private decimal overCost = 0;
        /// <summary>
        /// 大额补助
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
        /// 公务员补助支付
        /// </summary>
        private decimal officalCost = 0;
        /// <summary>
        /// 公务员补助支付
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

        #region 中日友好新增属性
        //{BA600C87-44A9-4dbc-86C7-5478796201A3}开始

        /// <summary>
        /// 是否已经变更
        /// </summary>
        private bool isShifted = false;

        /// <summary>
        /// 是否已经变更
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
        /// 变更记录
        /// </summary>
        private FS.HISFC.Models.Base.ShiftRecord shiftRecord = new FS.HISFC.Models.Base.ShiftRecord();

        /// <summary>
        /// 变更记录
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
        /// 医保上传号
        /// </summary>
        private string transNo;

        /// <summary>
        /// 医保上传号
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
        /// 普通医保内费用
        /// </summary>
        private decimal internalFee;
        /// <summary>
        /// 普通医保内费用
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
        /// 普通医保外费用
        /// </summary>
        private decimal externalFee;

        /// <summary>
        /// 普通医保外费用
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
        /// 大额/公务员自付金额
        /// </summary>
        private decimal officalOwnCost;

        /// <summary>
        /// 大额/公务员自付金额
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
        /// 本次交易统筹封顶后医保内金额
        /// </summary>
        private decimal overInterFee;

        /// <summary>
        /// 本次交易统筹封顶后医保内金额
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
        /// 个人应付总金额(个人帐户支付+现金)
        /// </summary>
        private decimal ownCountFee;
        /// <summary>
        /// 个人应付总金额(个人帐户支付+现金)
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
        /// 个人自付二金额
        /// </summary>
        private decimal ownSecondCountFee;
        /// <summary>
        /// 个人自付二金额
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
        /// 医保诊断代码
        /// </summary>
        private string siDiagnose = "";
        /// <summary>
        /// 医保诊断代码
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
        /// 医保诊断代码名称
        /// </summary>
        private string siDiagnoseName = "";
        /// <summary>
        /// 医保诊断代码名称
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
        /// 结算状态：1 结算 0 未结算
        /// </summary>
        private string balanceState = "";
        /// <summary>
        /// 结算状态：1 结算 0 未结算
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
        /// 交易类型 1：正交易 2：反交易
        /// </summary>
        private string transType = "";
        /// <summary>
        /// 交易类型 1：正交易 2：反交易
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
        /// 结算分类1-门诊2-住院
        /// </summary>
        private string typeCode = "";
        /// <summary>
        /// 结算分类1-门诊2-住院
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
        /// 统筹支付金额
        /// </summary>
        private decimal pubFeeCost = 0M;
        /// <summary>
        /// 统筹支付金额
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
        /// 是否已经交医保手册(单独使用，医保中间表不保存)
        /// </summary>
        private bool isGetSSN = false;
        /// <summary>
        /// 是否已经交医保手册(单独使用，医保中间表不保存)
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
        //{BA600C87-44A9-4dbc-86C7-5478796201A3}结束
        #endregion

        #region 佛山禅城区居民医保增加属性 
        // {4669B819-39AB-476b-B3A1-60AAF150FD45}
        private long ybMedNo = 0;
        /// <summary>
        /// 居民医保结算单号
        /// 本定点医院结算单的唯一标识。
        /// 保存以做为 注销居民门诊费用结算 参数之一
        /// </summary>
        public long YBMedNo
        {
            get { return ybMedNo; }
            set { ybMedNo = value; }
        }

        #endregion

        #region 深圳医保增加的属性

        #region 变量
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
        /// 连续参保月数
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
        /// 基本医疗保险共济基金可用余额（当前）
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
        /// 地方补充医疗保险共济基金可用余额（当前）
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
        /// //--监护人1姓名*
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
        /// //--监护人2姓名*
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
        /// 监护人1身份证号*
        /// </summary>
        private string jhr1sfzh;   //--监护人1身份证号*
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
        /// //--监护人1身份证号*
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
        /// 单据号
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
        /// 密码
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
        /// 医保人员对照码
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
        /// 医保人员对照码
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
        /// 门诊是否上传标志
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

        #region 珠海医保增加属性

        private string siType = "";
        /// <summary>
        /// 参保险种
        /// 1 未成年医保
        /// 2 居民医保
        /// 3 基本医疗
        /// 4 基本医疗+补助
        /// 5 大病医保
        /// 6 生育医保
        /// 7 工伤医保
        /// 8 门诊统筹
        /// </summary>
        public string SiType
        {
            get { return this.siType; }
            set { this.siType = value; }
        }

        #endregion

        #region 广州医保增加属性
        private bool isSIUploaded;
        /// <summary>
        ///{D3446DAF-E319-47a0-8BD5-EA748FCC4342}
        /// 是否已上传医保首页信息
        /// </summary>
        public bool IsSIUploaded
        {
            get { return isSIUploaded; }
            set { isSIUploaded = value; }
        }


        #endregion

        #region 医保改造 
        //{DC67634A-696F-4e03-8C63-447C4265817E}

        #region 广东省统一医保增加属性
        /// <summary>
        /// bka911	String	10	否	手术日期	生育
        /// </summary>
        private DateTime bka911;

        /// <summary>
        /// Bka911	String	10	否	手术日期	生育
        /// </summary>
        public DateTime Bka911
        {
            get { return bka911; }
            set { bka911 = value; }
        }

        /// <summary>
        /// bka912	String	10	否	生育类别	生育
        /// </summary>
        private string bka912 = string.Empty;

        /// <summary>
        /// Bka912	String	10	否	生育类别	生育
        /// </summary>
        public string Bka912
        {
            get { return bka912; }
            set { bka912 = value; }
        }

        /// <summary>
        ///  amc050 string 10  生育业务类型    生育
        /// </summary>
        private string amc050 = string.Empty;
        /// <summary>
        /// amc050 string 10  生育业务类型    生育
        /// </summary>
        public string Amc050
        {
            get { return amc050; }
            set { amc050 = value; }
        }
        /// <summary>
        /// amc 029 string 10 生育手术类别  生育
        /// </summary>
        private string amc029 = string.Empty;

        /// <summary>
        /// amc 029 string 10 生育手术类别  生育
        /// </summary>
        public string Amc029
        {
            get { return amc029; }
            set { amc029 = value; }
        }
        /// <summary>
        /// amc 029 string 10 生育胎次  生育
        /// </summary>
        private string amc031 = string.Empty;

        /// <summary>
        /// amc 029 string 10 生育胎次  生育
        /// </summary>
        public string Amc031
        {
            get { return amc031; }
            set { amc031 = value; }
        }
        /// <summary>
        /// bka913	String	10	否	胎儿数	生育
        /// </summary>
        private int bka913 = 0;

        /// <summary>
        /// Bka913	String	10	否	胎儿数	生育
        /// </summary>
        public int Bka913
        {
            get { return bka913; }
            set { bka913 = value; }
        }

        /// <summary>
        /// bka914	String	10	否	母亲情况	生育
        /// </summary>
        private string bka914 = string.Empty;

        /// <summary>
        /// bka914	String	10	否	母亲情况	生育
        /// </summary>
        public string Bka914
        {
            get { return bka914; }
            set { bka914 = value; }
        }

        /// <summary>
        /// bka915	String	10	否	母亲死亡时间	生育、格式：yyyyMMdd
        /// </summary>
        private DateTime bka915;

        /// <summary>
        /// Bka915	String	10	否	母亲死亡时间	生育、格式：yyyyMMdd
        /// </summary>
        public DateTime Bka915
        {
            get { return bka915; }
            set { bka915 = value; }
        }

        /// <summary>
        /// bka916	String	10	否	婴儿情况	生育
        /// </summary>
        private string bka916 = string.Empty;

        /// <summary>
        /// Bka916	String	10	否	婴儿情况	生育
        /// </summary>
        public string Bka916
        {
            get { return bka916; }
            set { bka916 = value; }
        }

        /// <summary>
        /// bka917	String	10	否	婴儿死亡时间	生育、格式：yyyyMMdd
        /// </summary>
        private DateTime bka917;

        /// <summary>
        /// Bka917	String	10	否	婴儿死亡时间	生育、格式：yyyyMMdd
        /// </summary>
        public DateTime Bka917
        {
            get { return bka917; }
            set { bka917 = value; }
        }


        /// <summary>
        /// bka042	String	20	工伤生育凭证号	只工伤、生育业务，才有此项 
        /// </summary>
        private string bka042 = string.Empty;

        /// <summary>
        /// Bka042	String	20	工伤生育凭证号	只工伤、生育业务，才有此项 
        /// </summary>
        public string Bka042
        {
            get { return bka042; }
            set { bka042 = value; }
        }

        /// <summary>
        /// aaz267	String	12	门诊选点、门慢申请序列号	 
        /// </summary>
        private string aaz267 = string.Empty;

        /// <summary>
        /// Aaz267	String	12	门诊选点、门慢申请序列号	
        /// </summary>
        public string Aaz267
        {
            get { return aaz267; }
            set { aaz267 = value; }
        }

        /// <summary>
        /// bka825	String	12	全自费费用	
        /// </summary>
        private decimal bka825 = 0m;

        /// <summary>
        /// Bka825	String	12	全自费费用	
        /// </summary>
        public decimal Bka825
        {
            get { return bka825; }
            set { bka825 = value; }
        }

        /// <summary>
        /// bka826	String	12	部分自费费用
        /// </summary>
        private decimal bka826 = 0m;

        /// <summary>
        /// Bka826	String	12	部分自费费用
        /// </summary>
        public decimal Bka826
        {
            get { return bka826; }
            set { bka826 = value; }
        }

        /// <summary>
        /// aka151	String	12	起付线费用
        /// </summary>
        private decimal aka151 = 0m;

        /// <summary>
        /// Aka151	String	12	起付线费用
        /// </summary>
        public decimal Aka151
        {
            get { return aka151; }
            set { aka151 = value; }
        }

        /// <summary>
        /// bka838	String	12	超共付段费用个人自付		
        /// </summary>
        private decimal bka838 = 0m;

        /// <summary>
        /// Bka838	String	12	超共付段费用个人自付	
        /// </summary>
        public decimal Bka838
        {
            get { return bka838; }
            set { bka838 = value; }
        }

        /// <summary>
        /// akb067	String	12	个人现金支付
        /// </summary>
        private decimal akb067 = 0m;

        /// <summary>
        /// Akb067	String	12	个人现金支付	
        /// </summary>
        public decimal Akb067
        {
            get { return akb067; }
            set { akb067 = value; }
        }

        /// <summary>
        /// akb066	String	12	个人账户支付
        /// </summary>
        private decimal akb066 = 0m;

        /// <summary>
        /// Akb066	String	12	个人账户支付
        /// </summary>
        public decimal Akb066
        {
            get { return akb066; }
            set { akb066 = value; }
        }

        /// <summary>
        /// bka821	String	12	民政救助金支付	
        /// </summary>
        private decimal bka821 = 0m;

        /// <summary>
        /// Bka821	String	12	民政救助金支付	
        /// </summary>
        public decimal Bka821
        {
            get { return bka821; }
            set { bka821 = value; }
        }

        /// <summary>
        /// bka839	String	12	其他支付	
        /// </summary>
        private decimal bka839 = 0m;

        /// <summary>
        /// Bka839	String	12	其他支付	
        /// </summary>
        public decimal Bka839
        {
            get { return bka839; }
            set { bka839 = value; }
        }

        /// <summary>
        /// ake039	String	12	医疗保险统筹基金支付
        /// </summary>
        private decimal ake039 = 0m;

        /// <summary>
        /// Ake039	String	12	医疗保险统筹基金支付
        /// </summary>
        public decimal Ake039
        {
            get { return ake039; }
            set { ake039 = value; }
        }

        /// <summary>
        /// ake035	String	12	公务员医疗补助基金支付
        /// </summary>
        private decimal ake035 = 0m;

        /// <summary>
        /// Ake035	String	12	公务员医疗补助基金支付
        /// </summary>
        public decimal Ake035
        {
            get { return ake035; }
            set { ake035 = value; }
        }

        /// <summary>
        /// ake026	String	12	企业补充医疗保险基金支付	
        /// </summary>
        private decimal ake026 = 0m;

        /// <summary>
        /// Ake026	String	12	企业补充医疗保险基金支付	
        /// </summary>
        public decimal Ake026
        {
            get { return ake026; }
            set { ake026 = value; }
        }

        /// <summary>
        /// ake029	String	12	大额医疗费用补助基金支付
        /// </summary>
        private decimal ake029 = 0m;

        /// <summary>
        /// Ake029	String	12	大额医疗费用补助基金支付
        /// </summary>
        public decimal Ake029
        {
            get { return ake029; }
            set { ake029 = value; }
        }

        /// <summary>
        /// bka841	String	12	单位支付	
        /// </summary>
        private decimal bka841 = 0m;

        /// <summary>
        /// Bka841	String	12	单位支付
        /// </summary>
        public decimal Bka841
        {
            get { return bka841; }
            set { bka841 = value; }
        }

        /// <summary>
        /// bka842	String	12	医院垫付
        /// </summary>
        private decimal bka842 = 0m;

        /// <summary>
        /// Bka842	String	12	医院垫付
        /// </summary>
        public decimal Bka842
        {
            get { return bka842; }
            set { bka842 = value; }
        }

        /// <summary>
        /// bka840	String	12	其他基金支付
        /// </summary>
        private decimal bka840 = 0m;

        /// <summary>
        /// Bka840	String	12	其他基金支付
        /// </summary>
        public decimal Bka840
        {
            get { return bka840; }
            set { bka840 = value; }
        }

        /// <summary>
        /// aaa027	String	6	是	医保分中心编码
        /// </summary>
        private string aaa027 = string.Empty;

        /// <summary>
        /// aaa027	String	6	是	医保分中心编码
        /// </summary>
        public string Aaa027
        {
            get { return aaa027; }
            set { aaa027 = value; }
        }

        /// <summary>
        /// bka438	String	2	业务场景阶段 0：业务未开始 	1：业务开始2：业务结算3：业务结束 (用于预结算) 
        /// 在住院中的费用录入时试算，传1；在出院登记保存时试算，传3；在出院结算时试算，传2
        /// </summary>
        private string bka438 = string.Empty;

        /// <summary>
        /// bka438	String	2	业务场景阶段 0：业务未开始 	1：业务开始2：业务结算3：业务结束 (用于预结算) 
        /// </summary>
        public string Bka438
        {
            get { return bka438; }
            set { bka438 = value; }
        }

        /// <summary>
        /// 参保人所属行政区划代码
        /// </summary>
        private string aab301 = string.Empty;

        /// <summary>
        /// 参保人所属行政区划代码
        /// </summary>
        public string Aab301
        {
            get { return aab301; }
            set { aab301 = value; }
        }

        /// <summary>
        /// 险种编码
        /// 310——"城镇职工基本医疗"
        /// 391——"城乡居民基本医疗"
        /// 410——"工伤"
        /// 510——"生育"
        /// </summary>
        private string aae140 = string.Empty;

        /// <summary>
        /// 险种编码
        /// 310——"城镇职工基本医疗"
        /// 391——"城乡居民基本医疗"
        /// 410——"工伤"
        /// 510——"生育"
        /// </summary>
        public string Aae140
        {
            get { return aae140; }
            set { aae140 = value; }
        }

        /// <summary>
        /// bka006	String	6	是	医疗待遇类型
        /// </summary>
        private string bka006 = string.Empty;
        /// <summary>
        /// bka006	String	6	是	医疗待遇类型
        /// </summary>
        public string Bka006
        {
            get { return bka006; }
            set { bka006 = value; }
        }
        /// <summary>
        /// aka130	String	6	是	医疗业务类型
        /// </summary>
        private string aka130 = string.Empty;
        /// <summary>
        /// aka130	String	医疗业务类型 11门诊 12住院 13门慢 16门特 41工伤门诊 42工伤住院 51生育门诊 52生育住院
        /// </summary>
        public string Aka130
        {
            get { return aka130; }
            set { aka130 = value; }
        }
        private string bka020 = string.Empty;
        /// <summary>
        /// bka020	String	 就诊科室名称
        /// </summary>
        public string Bka020
        {
            get { return bka020; }
            set { bka020 = value; }
        }
        private string bka004 = string.Empty;
        /// <summary>
        /// bka004	String	 人员类别
        /// </summary>
        public string Bka004
        {
            get { return bka004; }
            set { bka004 = value; }
        }

        /// <summary>
        /// 是否读取社保卡
        /// </summary>
        private bool isUserSICard = true;
        /// <summary>
        /// 是否读取社保卡,默认是需要读卡，由于省内异地有些患者没带卡，信息科要求这类患者不需要读卡校验
        /// </summary>
        public bool IsUserSICard
        {
            get { return isUserSICard; }
            set { isUserSICard = value; }
        }

        /// <summary>
        /// 结算召回是否要取消医保联网结算的数据
        /// </summary>
        private bool isCancelSIBanlance = true;
        /// <summary>
        /// 结算召回是否要取消医保联网结算的数据
        /// </summary>
        public bool IsCancelSIBanlance
        {
            get { return isCancelSIBanlance; }
            set { isCancelSIBanlance = value; }
        }

        /// <summary>
        /// bka841	String	12	医保支付金额	
        /// </summary>
        private decimal bka811 = 0m;

        /// <summary>
        /// Bka841	String	12	医保支付金额
        /// </summary>
        public decimal Bka811
        {
            get { return bka811; }
            set { bka811 = value; }
        }

        /// <summary>
        /// bka841	String	12	个人支付金额	
        /// </summary>
        private decimal bka812 = 0m;

        /// <summary>
        /// Bka841	String	12	个人支付金额
        /// </summary>
        public decimal Bka812
        {
            get { return bka812; }
            set { bka812 = value; }
        }

        private string bkp969 = string.Empty;
        /// <summary>
        /// 读卡
        /// </summary>
        public string Bkp969
        {
            get { return bkp969; }
            set { bkp969 = value; }
        }

        private string bkp979 = string.Empty;
        /// <summary>
        /// 读卡
        /// </summary>
        public string Bkp979
        {
            get { return bkp979; }
            set { bkp979 = value; }
        }
        #endregion 广东省统一医保增加属性

        #region 广州医保API(新)
        /// <summary>
        /// 就诊ID
        /// </summary>
        private string mdtrt_id = string.Empty;

        /// <summary>
        /// 就诊ID 
        /// </summary>
        public string Mdtrt_id
        {
            get { return mdtrt_id; }
            set { mdtrt_id = value; }
        }

        /// <summary>
        /// 结算ID
        /// </summary>
        private string setl_id = string.Empty;

        /// <summary>
        /// 结算ID
        /// </summary>
        public string Setl_id
        {
            get { return setl_id; }
            set { setl_id = value; }
        }

        /// <summary>
        /// 人员编号
        /// </summary>
        private string psn_no = string.Empty;

        /// <summary>
        /// 人员编号
        /// </summary>
        public string Psn_no
        {
            get { return psn_no; }
            set { psn_no = value; }
        }

        /// <summary>
        /// 人员姓名
        /// </summary>
        private string psn_name = string.Empty;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string Psn_name
        {
            get { return psn_name; }
            set { psn_name = value; }
        }

        /// <summary>
        /// 人员证件类型
        /// </summary>
        private string psn_cert_type = string.Empty;

        /// <summary>
        /// 人员证件类型
        /// </summary>
        public string Psn_cert_type
        {
            get { return psn_cert_type; }
            set { psn_cert_type = value; }
        }

        /// <summary>
        /// 证件号码
        /// </summary>
        private string certno = string.Empty;

        /// <summary>
        /// 证件号码
        /// </summary>
        public string Certno
        {
            get { return certno; }
            set { certno = value; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private string gend = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public string Gend
        {
            get { return gend; }
            set { gend = value; }
        }

        /// <summary>
        /// 民族
        /// </summary>
        private string naty = string.Empty;

        /// <summary>
        /// 民族
        /// </summary>
        public string Naty
        {
            get { return naty; }
            set { naty = value; }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        private DateTime brdy;

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Brdy
        {
            get { return brdy; }
            set { brdy = value; }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        private string age = string.Empty;

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        /// <summary>
        /// 险种类型
        /// </summary>
        private string insutype = string.Empty;

        /// <summary>
        /// 险种类型
        /// </summary>
        public string Insutype
        {
            get { return insutype; }
            set { insutype = value; }
        }


        /// <summary>
        /// 险种类型
        /// </summary>
        private string insuplc_admdvs = string.Empty;

        /// <summary>
        /// 险种类型
        /// </summary>
        public string Insuplc_admdvs
        {
            get { return insuplc_admdvs; }
            set { insuplc_admdvs = value; }
        }

        /// <summary>
        /// 人员类别
        /// </summary>
        private string psn_type = string.Empty;

        /// <summary>
        /// 人员类别
        /// </summary>
        public string Psn_type
        {
            get { return psn_type; }
            set { psn_type = value; }
        }

        /// <summary>
        /// 公务员标志
        /// </summary>
        private string cvlserv_flag = string.Empty;

        /// <summary>
        /// 公务员标志
        /// </summary>
        public string Cvlserv_flag
        {
            get { return cvlserv_flag; }
            set { cvlserv_flag = value; }
        }

        /// <summary>
        /// 结算时间
        /// </summary>
        private DateTime setl_time;

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime Setl_time
        {
            get { return setl_time; }
            set { setl_time = value; }
        }

        /// <summary>
        /// 个人结算方式
        /// </summary>
        private string psn_setlway = string.Empty;

        /// <summary>
        /// 个人结算方式
        /// </summary>
        public string Psn_setlway
        {
            get { return psn_setlway; }
            set { psn_setlway = value; }
        }

        /// <summary>
        /// 就诊凭证类型
        /// </summary>
        private string mdtrt_cert_type = string.Empty;

        /// <summary>
        /// 就诊凭证类型
        /// </summary>
        public string Mdtrt_cert_type
        {
            get { return mdtrt_cert_type; }
            set { mdtrt_cert_type = value; }
        }

        /// <summary>
        /// 医疗类别
        /// </summary>
        private string med_type = string.Empty;

        /// <summary>
        /// 医疗类别
        /// </summary>
        public string Med_type
        {
            get { return med_type; }
            set { med_type = value; }
        }

        /// <summary>
        /// 病种编码
        /// </summary>
        private string dise_code = string.Empty;

        /// <summary>
        /// 病种编码
        /// </summary>
        public string Dise_code
        {
            get { return dise_code; }
            set { dise_code = value; }
        }

        /// <summary>
        /// 病种名称
        /// </summary>
        private string dise_name = string.Empty;

        /// <summary>
        /// 病种名称
        /// </summary>
        public string Dise_name
        {
            get { return dise_name; }
            set { dise_name = value; }
        }

        /// <summary>
        /// 医疗费总额
        /// </summary>
        private decimal medfee_sumamt = 0m;

        /// <summary>
        /// 医疗费总额
        /// </summary>
        public decimal Medfee_sumamt
        {
            get { return medfee_sumamt; }
            set { medfee_sumamt = value; }
        }

        /// <summary>
        /// 全自费金额
        /// </summary>
        private decimal ownpay_amt = 0m;

        /// <summary>
        /// 全自费金额
        /// </summary>
        public decimal Ownpay_amt
        {
            get { return ownpay_amt; }
            set { ownpay_amt = value; }
        }

        /// <summary>
        /// 超限价自费费用
        /// </summary>
        private decimal overlmt_selfpay = 0m;

        /// <summary>
        /// 超限价自费费用
        /// </summary>
        public decimal Overlmt_selfpay
        {
            get { return overlmt_selfpay; }
            set { overlmt_selfpay = value; }
        }

        /// <summary>
        /// 先行自付金额
        /// </summary>
        private decimal preselfpay_amt = 0m;

        /// <summary>
        /// 先行自付金额
        /// </summary>
        public decimal Preselfpay_amt
        {
            get { return preselfpay_amt; }
            set { preselfpay_amt = value; }
        }

        /// <summary>
        /// 符合范围金额
        /// </summary>
        private decimal inscp_scp_amt = 0m;

        /// <summary>
        /// 符合范围金额
        /// </summary>
        public decimal Inscp_scp_amt
        {
            get { return inscp_scp_amt; }
            set { inscp_scp_amt = value; }
        }

        /// <summary>
        /// 医保认可费用总额
        /// </summary>
        private decimal med_sumfee = 0m;

        /// <summary>
        /// 医保认可费用总额
        /// </summary>
        public decimal Med_sumfee
        {
            get { return med_sumfee; }
            set { med_sumfee = value; }
        }

        /// <summary>
        /// 实际支付起付线
        /// </summary>
        private decimal act_pay_dedc = 0m;

        /// <summary>
        /// 实际支付起付线
        /// </summary>
        public decimal Act_pay_dedc
        {
            get { return act_pay_dedc; }
            set { act_pay_dedc = value; }
        }

        /// <summary>
        /// 基本医疗保险统筹基金支出
        /// </summary>
        private decimal hifp_pay = 0m;

        /// <summary>
        /// 基本医疗保险统筹基金支出
        /// </summary>
        public decimal Hifp_pay
        {
            get { return hifp_pay; }
            set { hifp_pay = value; }
        }

        /// <summary>
        /// 基本医疗保险统筹基金支付比例
        /// </summary>
        private decimal pool_prop_selfpay = 0m;

        /// <summary>
        /// 基本医疗保险统筹基金支付比例
        /// </summary>
        public decimal Pool_prop_selfpay
        {
            get { return pool_prop_selfpay; }
            set { pool_prop_selfpay = value; }
        }

        /// <summary>
        /// 公务员医疗补助资金支出
        /// </summary>
        private decimal cvlserv_pay = 0m;

        /// <summary>
        /// 公务员医疗补助资金支出
        /// </summary>
        public decimal Cvlserv_pay
        {
            get { return cvlserv_pay; }
            set { cvlserv_pay = value; }
        }

        /// <summary>
        /// 企业补充医疗保险基金支出
        /// </summary>
        private decimal hifes_pay = 0m;

        /// <summary>
        /// 企业补充医疗保险基金支出
        /// </summary>
        public decimal Hifes_pay
        {
            get { return hifes_pay; }
            set { hifes_pay = value; }
        }

        /// <summary>
        /// 居民大病保险资金支出
        /// </summary>
        private decimal hifmi_pay = 0m;

        /// <summary>
        /// 居民大病保险资金支出
        /// </summary>
        public decimal Hifmi_pay
        {
            get { return hifmi_pay; }
            set { hifmi_pay = value; }
        }

        /// <summary>
        /// 职工大额医疗费用补助基金支出
        /// </summary>
        private decimal hifob_pay = 0m;

        /// <summary>
        /// 职工大额医疗费用补助基金支出
        /// </summary>
        public decimal Hifob_pay
        {
            get { return hifob_pay; }
            set { hifob_pay = value; }
        }

        /// <summary>
        /// 伤残人员医疗保障基金支出
        /// </summary>
        private decimal hifdm_pay = 0m;

        /// <summary>
        /// 伤残人员医疗保障基金支出
        /// </summary>
        public decimal Hifdm_pay
        {
            get { return hifdm_pay; }
            set { hifdm_pay = value; }
        }

        /// <summary>
        /// 医疗救助基金支出
        /// </summary>
        private decimal maf_pay = 0m;

        /// <summary>
        /// 医疗救助基金支出
        /// </summary>
        public decimal Maf_pay
        {
            get { return maf_pay; }
            set { maf_pay = value; }
        }

        /// <summary>
        /// 其他基金支出
        /// </summary>
        private decimal oth_pay = 0m;

        /// <summary>
        /// 其他基金支出
        /// </summary>
        public decimal Oth_pay
        {
            get { return oth_pay; }
            set { oth_pay = value; }
        }
        /// <summary>
        /// 基金支付总额
        /// </summary>
        private decimal fund_pay_sumamt = 0m;

        /// <summary>
        /// 基金支付总额
        /// </summary>
        public decimal Fund_pay_sumamt
        {
            get { return fund_pay_sumamt; }
            set { fund_pay_sumamt = value; }
        }
        /// <summary>
        /// 医院负担金额
        /// </summary>
        private decimal hosp_part_amt = 0m;

        /// <summary>
        /// 医院负担金额
        /// </summary>
        public decimal Hosp_part_amt
        {
            get { return hosp_part_amt; }
            set { hosp_part_amt = value; }
        }
        /// <summary>
        /// 个人负担总金额
        /// </summary>
        private decimal psn_part_am = 0m;

        /// <summary>
        /// 个人负担总金额
        /// </summary>
        public decimal Psn_part_am
        {
            get { return psn_part_am; }
            set { psn_part_am = value; }
        }
        /// <summary>
        /// 个人账户支出
        /// </summary>
        private decimal acct_pay = 0m;

        /// <summary>
        /// 个人账户支出
        /// </summary>
        public decimal Acct_pay
        {
            get { return acct_pay; }
            set { acct_pay = value; }
        }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        private decimal cash_payamt = 0m;

        /// <summary>
        /// 现金支付金额
        /// </summary>
        public decimal Cash_payamt
        {
            get { return cash_payamt; }
            set { cash_payamt = value; }
        }
        /// <summary>
        /// 账户共济支付金额
        /// </summary>
        private decimal acct_mulaid_pay = 0m;

        /// <summary>
        /// 账户共济支付金额
        /// </summary>
        public decimal Acct_mulaid_pay
        {
            get { return acct_mulaid_pay; }
            set { acct_mulaid_pay = value; }
        }
        /// <summary>
        /// 个人账户支出后余额
        /// </summary>
        private decimal balc = 0m;

        /// <summary>
        /// 个人账户支出后余额
        /// </summary>
        public decimal Balc
        {
            get { return balc; }
            set { balc = value; }
        }
        /// <summary>
        /// 清算经办机构
        /// </summary>
        private string clr_optins = string.Empty;

        /// <summary>
        /// 清算经办机构
        /// </summary>
        public string Clr_optins
        {
            get { return clr_optins; }
            set { clr_optins = value; }
        }
        /// <summary>
        /// 清算方式
        /// </summary>
        private string clr_way = string.Empty;

        /// <summary>
        /// 清算方式
        /// </summary>
        public string Clr_way
        {
            get { return clr_way; }
            set { clr_way = value; }
        }
        /// <summary>
        /// 清算类别
        /// </summary>
        private string clr_type = string.Empty;

        /// <summary>
        /// 清算类别
        /// </summary>
        public string Clr_type
        {
            get { return clr_type; }
            set { clr_type = value; }
        }
        /// <summary>
        /// 医药机构结算ID
        /// </summary>
        private string medins_setl_id = string.Empty;

        /// <summary>
        /// 医药机构结算ID
        /// </summary>
        public string Medins_setl_id
        {
            get { return medins_setl_id; }
            set { medins_setl_id = value; }
        }

        /// <summary>
        /// 违规类型
        /// </summary>
        private string vola_type = string.Empty;

        /// <summary>
        /// 违规类型
        /// </summary>
        public string Vola_type
        {
            get { return vola_type; }
            set { vola_type = value; }
        }

        /// <summary>
        /// 违规说明
        /// </summary>
        private string vola_dscr = string.Empty;

        /// <summary>
        /// 违规说明
        /// </summary>
        public string Vola_dscr
        {
            get { return vola_dscr; }
            set { vola_dscr = value; }
        }

        /// <summary>
        /// 对账结果
        /// </summary>
        private string stmt_relt = string.Empty;

        /// <summary>
        /// 对账结果
        /// </summary>
        public string Stmt_relt
        {
            get { return stmt_relt; }
            set { stmt_relt = value; }
        }


        /// <summary>
        /// 结算经办机构
        /// </summary>
        private string setl_options = string.Empty;

        /// <summary>
        /// 结算经办机构
        /// </summary>
        public string Setl_options
        {
            get { return setl_options; }
            set { setl_options = value; }
        }


        /// <summary>
        /// 对账结果说明
        /// </summary>
        private string stmt_rslt_dscr = string.Empty;

        /// <summary>
        /// 对账结果说明
        /// </summary>
        public string Stmt_rslt_dscr
        {
            get { return stmt_rslt_dscr; }
            set { stmt_rslt_dscr = value; }
        }


        /// <summary>
        /// 对账状态
        /// </summary>
        private string stmt_state = string.Empty;

        /// <summary>
        /// 对账状态
        /// </summary>
        public string Stmt_state
        {
            get { return stmt_state; }
            set { stmt_state = value; }
        }



        /// <summary>
        /// 读卡返回的社保卡基本信息
        /// </summary>
        private string hcard_basinfo = string.Empty;

        /// <summary>
        /// 读卡返回的社保卡基本信息
        /// </summary>
        public string Hcard_basinfo
        {
            get { return hcard_basinfo; }
            set { hcard_basinfo = value; }
        }

        /// <summary>
        /// 持卡就诊登记许可号
        /// </summary>
        private string hcard_chkinfo = string.Empty;

        /// <summary>
        /// 持卡就诊登记许可号
        /// </summary>
        public string Hcard_chkinfo
        {
            get { return hcard_chkinfo; }
            set { hcard_chkinfo = value; }
        }

        private string fetus_cnt = string.Empty;
        /// <summary>
        /// 胎儿数
        /// </summary>
        public string Fetus_cnt
        {
            get { return fetus_cnt; }
            set { fetus_cnt = value; }
        }

        private string birctrl_matn_date = string.Empty;
        /// <summary>
        /// 生育或分娩日期
        /// </summary>
        public string Birctrl_matn_date
        {
            get { return birctrl_matn_date; }
            set { birctrl_matn_date = value; }
        }

        private string repeat_ipt_flag = string.Empty;

        #region // {3CCAB886-9648-4DE8-B5DC-276D9212353F}
        /// <summary>
        /// 重复入院标志
        /// </summary>
        public string Repeat_ipt_flag
        {
            get { return repeat_ipt_flag; }
            set { repeat_ipt_flag = value; }
        }

        private string ttp_resp = string.Empty;
        /// <summary>
        /// 合并结算标志
        /// </summary>
        public string Ttp_resp
        {
            get { return ttp_resp; }
            set { ttp_resp = value; }
        }

        private string merg_setl_flag = string.Empty;
        /// <summary>
        /// 是否第三方责任
        /// </summary>
        public string Merg_setl_flag
        {
            get { return merg_setl_flag; }
            set { merg_setl_flag = value; }
        }
        #endregion
        #endregion 广州医保API(新)

        #endregion
    }
}
