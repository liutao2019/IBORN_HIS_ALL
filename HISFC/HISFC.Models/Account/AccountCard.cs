using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Account
{
    /// <summary>
    /// FS.HISFC.Models.Account.AccountCard<br></br>
    /// [功能描述: 门诊帐户卡实体]<br></br>
    /// [创 建 者: 路志鹏]<br></br>
    /// [创建时间: 2007-05-03]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountCard :FS.FrameWork.Models.NeuObject, Base.IValid
    {
       
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountCard()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 变量
        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 身份标识卡号
        /// </summary>
        private string markNO = string.Empty;
        /// <summary>
        /// 卡类型
        /// </summary>
        private FS.FrameWork.Models.NeuObject markType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 是否有效
        /// </summary>
        private bool isValid =true;
        /// <summary>
        /// 补发标记'1'补发,'0'新发卡
        /// </summary>
        private string reFlag = string.Empty;
        /// <summary>
        /// 防伪码
        /// </summary>
        private string securityCode = string.Empty;

        /// <summary>
        /// 帐户卡操作实体
        /// </summary>
        private List< AccountCardRecord> accountCardRecord = new List<AccountCardRecord>();
        /// <summary>
        /// 卡状态
        /// </summary>
        private MarkOperateTypes markStatus = MarkOperateTypes.Begin;
        /// <summary>
        /// 建卡人
        /// </summary>
        private OperEnvironment createOper = new OperEnvironment();
        /// <summary>
        /// 停卡人
        /// </summary>
        private OperEnvironment stopOper = new OperEnvironment();
        /// <summary>
        /// 退卡人
        /// </summary>
        private OperEnvironment backOper = new OperEnvironment();
        /// <summary>
        /// 会员等级:1 普通会员卡；2 会员卡；3 黄金会员卡；4白金会员卡；5钻石会员卡；6至尊会员卡// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        protected FS.FrameWork.Models.NeuObject accountLevel;

        /// <summary>
        /// 卡的开始时间
        /// </summary>
        private DateTime begTime = new DateTime();
        /// <summary>
        /// 卡的截止时间
        /// </summary>
        private DateTime endTime = new DateTime();

        #endregion 

        #region 属性
        /// <summary>
        /// 卡的开始时间
        /// </summary>
        public DateTime BegTime
        {
            get { return this.begTime; }
            set { this.begTime = value; }
        }

        /// <summary>
        /// 卡的截止时间
        /// </summary>
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }

        /// <summary>
        /// 会员等级:1 普通会员卡；2 黄金会员卡；3白金会员卡；4钻石会员卡；5至尊会员卡// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public FS.FrameWork.Models.NeuObject AccountLevel
        {
            get
            {
                if (accountLevel == null)
                {
                    accountLevel = new FS.FrameWork.Models.NeuObject();
                }

                return this.accountLevel;
            }
            set
            {
                this.accountLevel = value;
            }
        }
        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                patient = value;
            }
            get
            {
                return patient;
            }
        }

        /// <summary>
        /// 身份标识卡号
        /// </summary>
        public string MarkNO
        {
            get
            {
                return this.markNO;
            }
            set
            {
                this.markNO = value;
            }
        }
        /// <summary>
        /// 身份标识卡类别 1磁卡 2IC卡 3保障卡
        /// </summary>
        public FS.FrameWork.Models.NeuObject MarkType
        {
            get
            {
                return this.markType;
            }
            set
            {
                this.markType = value;
            }
        }

        /// <summary>
        /// 门诊帐户卡操作实体
        /// </summary>
        public List<AccountCardRecord> AccountCardRecord
        {
            get
            {
                return this.accountCardRecord;
            }
            set
            {
                this.accountCardRecord = value;
            }
        }
        /// <summary>
        /// 建卡人
        /// </summary>
        public OperEnvironment CreateOper
        {
            get { return createOper; }
            set { createOper = value;  }
        }
        /// <summary>
        /// 停卡人
        /// </summary>
        public OperEnvironment StopOper
        {
            get { return stopOper; }
            set
            {
                stopOper = value;
            }
        }
        /// <summary>
        /// 退卡人
        /// </summary>
        public OperEnvironment BackOper
        {
            get { return backOper; }
            set { backOper = value; }
        }
        /// <summary>
        /// 补发标记'1'补发,'0'新发卡
        /// </summary>
        public string ReFlag
        {
            get { return reFlag; }
            set { reFlag = value; }
        }
        /// <summary>
        /// 防伪码
        /// </summary>
        public string SecurityCode
        {
            get { return securityCode; }
            set { securityCode = value; }
        }
        #endregion 

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new AccountCard Clone()
        {
            AccountCard accountCard = base.Clone() as AccountCard;
            accountCard.MarkType = this.MarkType.Clone() as FS.FrameWork.Models.NeuObject;
            accountCard.CreateOper = this.CreateOper.Clone();
            accountCard.StopOper = this.StopOper.Clone();
            accountCard.BackOper = this.BackOper.Clone();

            foreach (AccountCardRecord cardRecord in this.AccountCardRecord)
            {
                accountCard.AccountCardRecord.Add(cardRecord);
            }
            return accountCard;
        }
        #endregion

        #region IValid 成员
        /// <summary>
        /// 是否有效 true有效 false作废
        /// </summary>
        [Obsolete("作废，不建议使用，请使用 MarkStatus ", false)]
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        #region 卡状态
        /// <summary>
        /// 卡状态
        /// </summary>
        public MarkOperateTypes MarkStatus
        {
            get
            {
                return this.markStatus;
            }
            set
            {
                this.markStatus = value;
            }
        }

        #endregion



    }

    /// <summary>
    /// FS.HISFC.Models.Account.AccountCardFee
    /// [功能描述: 门诊卡费用实体]
    /// </summary>
    [Serializable]
    public class AccountCardFee : FS.FrameWork.Models.NeuObject
    {
        #region 变量
        /// <summary>
        /// 发票号（流水号）
        /// </summary>
        string invoiceNo = string.Empty;
        /// <summary>
        /// 打印发票号
        /// </summary>
        string print_invoiceNo = string.Empty;
        /// <summary>
        /// 交易类型 TransTypes.Positive 正交易(1), TransTypes.Negative 负交易(2)
        /// </summary>
        private TransTypes transType;
        /// <summary>
        /// 费用类别
        /// </summary>
        AccCardFeeType feeType; 
        /// <summary>
        /// 身份标识卡号
        /// </summary>
        private string markNO = string.Empty;
        /// <summary>
        /// 卡类型
        /// </summary>
        private FS.FrameWork.Models.NeuObject markType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 费用总额
        /// </summary>
        private decimal tot_cost = 0;
        /// <summary>
        /// 收费人
        /// </summary>
        private OperEnvironment feeOper = new OperEnvironment();
        /// <summary>
        /// 操作者
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();
        /// <summary>
        /// 是否日结
        /// </summary>
        bool isBalance = false;
        /// <summary>
        /// 日结标识号
        /// </summary>
        string balanceNo = string.Empty;
        /// <summary>
        /// 日结操作环境(日结人,日结时间)
        /// </summary>
        private OperEnvironment balanceOper = new OperEnvironment();
        /// <summary>
        /// 状态， 1 = 有效 0 = 无效 2 = 退费
        /// </summary>
        int iStatus = 1;

        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 门诊号
        /// </summary>
        private string clinicNo = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        private string remark = string.Empty;
        /// <summary>
        /// 自费金额
        /// </summary>
        private decimal own_cost = 0;
        /// <summary>
        /// 自付金额
        /// </summary>
        private decimal pay_cost = 0;
        /// <summary>
        /// 报销金额
        /// </summary>
        private decimal pub_cost = 0;

        private NeuObject payType = new NeuObject();


        // {C0554E8C-39D6-48cb-983A-EDC6C1D63843}

        /// <summary>
        /// 项目编码
        /// </summary>
        private string itemCode = string.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        private string itemName = string.Empty;

        /// <summary>
        /// 项目数量
        /// </summary>
        private int itemQty = 0;

        /// <summary>
        /// 项目单位
        /// </summary>
        private string itemUnit = string.Empty;

        /// <summary>
        /// 项目价格
        /// </summary>
        private decimal itemPrice = 0;

        /// <summary>
        /// 社保标志：0未上传；1已上传
        /// </summary>
        private string siFlag = string.Empty;

        /// <summary>
        /// 社保结算日期
        /// </summary>
        private DateTime siBalanceDate;

        /// <summary>
        /// 社保结算单据
        /// </summary>
        private string siBalanceNO = string.Empty;

        /// <summary>
        /// 优惠金额
        /// </summary>
        private decimal eco_cost = 0;


        #endregion

        #region 属性
        /// <summary>
        /// 发票号（流水号）
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        /// <summary>
        /// 打印发票号
        /// </summary>
        public string Print_InvoiceNo
        {
            get { return print_invoiceNo; }
            set { print_invoiceNo = value; }
        }
        /// <summary>
        /// 交易类型 TransTypes.Positive 正交易(1), TransTypes.Negative 负交易(2)
        /// </summary>
        public TransTypes TransType
        {
            get { return transType; }
            set { transType = value; }
        }
        /// <summary>
        /// 卡费用类别
        /// </summary>
        public AccCardFeeType FeeType
        {
            get { return this.feeType; }
            set { this.feeType = value; }
        }
        /// <summary>
        /// 病历号
        /// </summary>
        public string CardNo
        {
            get { return this.patient.PID.CardNO; }
            set { this.patient.PID.CardNO = value; }
        }

        /// <summary>
        /// 身份标识卡号
        /// </summary>
        public string MarkNO
        {
            get { return markNO; }
            set { markNO = value; }
        }

        public FS.FrameWork.Models.NeuObject MarkType
        {
            get { return markType; }
            set { markType = value; }
        }
        /// <summary>
        /// 费用总额
        /// </summary>
        public decimal Tot_cost
        {
            get { return tot_cost; }
            set { tot_cost = value; }
        }
        /// <summary>
        /// 收费人
        /// </summary>
        public OperEnvironment FeeOper
        {
            get { return feeOper; }
            set { feeOper = value; }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }
        /// <summary>
        /// 是否日结
        /// </summary>
        public bool IsBalance
        {
            get { return isBalance; }
            set { isBalance = value; }
        }

        /// <summary>
        /// 日结操作环境(日结人,日结时间)
        /// </summary>
        public OperEnvironment BalnaceOper
        {
            get { return balanceOper; }
            set { balanceOper = value; }
        }
        /// <summary>
        /// 日结标识号
        /// </summary>
        public string BalanceNo
        {
            get { return balanceNo; }
            set { balanceNo = value; }
        }
        /// <summary>
        /// 状态， 1 = 有效 0 = 无效 2 = 退费
        /// </summary>
        public int IStatus
        {
            get { return iStatus; }
            set { iStatus = value; }
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                patient = value;
            }
            get
            {
                return patient;
            }
        }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string ClinicNO
        {
            get { return clinicNo; }
            set { clinicNo = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        /// <summary>
        /// 自费金额
        /// </summary>
        public decimal Own_cost
        {
            get { return this.own_cost; }
            set { own_cost = value; }
        }
        /// <summary>
        /// 自付金额
        /// </summary>
        public decimal Pay_cost
        {
            get { return this.pay_cost; }
            set { pay_cost = value; }
        }
        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal Pub_cost
        {
            get { return this.pub_cost; }
            set { pub_cost = value; }
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        public NeuObject PayType
        {
            get
            {
                return payType;
            }
            set
            {
                payType = value;
            }
        }
        // {C0554E8C-39D6-48cb-983A-EDC6C1D63843}

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        /// <summary>
        /// 项目数量
        /// </summary>
        public int ItemQty
        {
            get { return itemQty; }
            set { itemQty = value; }
        }

        /// <summary>
        /// 项目单位
        /// </summary>
        public string ItemUnit
        {
            get { return itemUnit; }
            set { itemUnit = value; }
        }

        /// <summary>
        /// 项目价格
        /// </summary>
        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        /// <summary>
        /// 社保标志：0未上传；1已上传
        /// </summary>
        public string SiFlag
        {
            get { return siFlag; }
            set { siFlag = value; }
        }

        /// <summary>
        /// 社保结算日期
        /// </summary>
        public DateTime SiBalanceDate
        {
            get { return siBalanceDate; }
            set { siBalanceDate = value; }
        }

        /// <summary>
        /// 社保结算单据
        /// </summary>
        public string SiBalanceNO
        {
            get { return siBalanceNO; }
            set { siBalanceNO = value; }
        }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal Eco_cost
        {
            get { return eco_cost; }
            set { eco_cost = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new AccountCardFee Clone()
        {
            AccountCardFee accountCardFee = base.Clone() as AccountCardFee;
            accountCardFee.MarkType = this.MarkType.Clone();
            accountCardFee.FeeOper = this.FeeOper.Clone();
            accountCardFee.Oper = this.Oper.Clone();
            accountCardFee.BalnaceOper = this.BalnaceOper.Clone();
            accountCardFee.Patient = this.Patient.Clone();

            return accountCardFee;
        }
        #endregion
    }

    /// <summary>
    /// 卡费用类别
    /// </summary>
    public enum AccCardFeeType
    {
        /// <summary>
        /// 卡费用
        /// </summary>
        CardFee = 1,
        /// <summary>
        /// 病历本费用
        /// </summary>
        CaseFee = 2,
        /// <summary>
        /// 挂号费
        /// </summary>
        RegFee = 3,
        /// <summary>
        /// 诊金
        /// </summary>
        DiaFee = 4,
        /// <summary>
        /// 检查费
        /// </summary>
        ChkFee = 5,
        /// <summary>
        /// 空调费
        /// </summary>
        AirConFee = 6,
        /// <summary>
        /// 其他费用
        /// </summary>
        OthFee = 7
    }

}
