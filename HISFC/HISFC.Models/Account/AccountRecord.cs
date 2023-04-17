using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Account
{
    
    /// <summary>
    /// FS.HISFC.Models.Account.AccountRecord<br></br>
    /// [功能描述: 门诊帐户交易实体]<br></br>
    /// [创 建 者: 路志鹏]<br></br>
    /// [创建时间: 2007-05-04]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountRecord : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountRecord()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 变量
        
        /// <summary>
        /// 帐户患者信息
        /// </summary>
        private HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 帐号
        /// </summary>
        private string accountNO = string.Empty;

        /// <summary>
        /// 操作类型0预交金1建帐户2停帐户3重启帐户4支付5退费入户
        /// </summary>
        private EnumOperTypesService operType = new EnumOperTypesService();

        /// <summary>
        /// 消费类型：P购买套餐；R门诊挂号；C门诊消费；I住院结算；// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private EnumPayTypesService payType = new EnumPayTypesService();

        /// <summary>
        /// 交费科室
        /// {68539124-2891-4358-8EF2-D8500CCCD28A}
        /// </summary>
        private FS.FrameWork.Models.NeuObject feeDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 操作员
        /// {68539124-2891-4358-8EF2-D8500CCCD28A}
        /// </summary>
        private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 操作时间
        /// </summary>
        private DateTime operTime = new DateTime();

        /// <summary>
        /// 备注
        /// </summary>
        private string reMark = string.Empty;

        /// <summary>
        /// 交易状态
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// 基本账户充值消费金额
        /// </summary>
        private decimal baseCost;
        /// <summary>
        /// 赠送充值消费金额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private decimal donateCost;

        /// <summary>
        /// 基本账户交易后余额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private decimal baseVacancy;

        /// <summary>
        /// 赠送账户交易后余额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private decimal donateVacancy;

        /// <summary>
        /// 被授权患者基本信息
        /// </summary>
        private HISFC.Models.RADT.Patient empwoerPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 授权金额
        /// </summary>
        private decimal empowerCost = 0m;

        /// <summary>
        /// 发票类型// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        FS.FrameWork.Models.NeuObject invoiceType = new FS.FrameWork.Models.NeuObject();



        /// <summary>
        /// 账户类型// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private FS.FrameWork.Models.NeuObject accountType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 充值发票号// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private string invoiceNo;

        /// <summary>
        /// 对应的消费发票号// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private string payInvoiceNo;
        /// <summary>
        /// 授权人病历号
        /// </summary>
        private string cardNo;
        
        #endregion

        #region 属性
        /// <summary>
        /// 授权人病历号
        /// </summary>
        public string CardNo
        {
            get
            {
                return this.cardNo;
            }
            set
            {
                this.cardNo = value;
            }
        }
        /// <summary>
        /// 充值发票号// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public string InvoiceNo
        {
            get
            {
                return this.invoiceNo;
            }
            set
            {
                this.invoiceNo = value;
            }
        }
        /// <summary>
        /// 对应的消费发票号// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public string PayInvoiceNo
        {
            get
            {
                return this.payInvoiceNo;
            }
            set
            {
                this.payInvoiceNo = value;
            }
        }
        /// <summary>
        /// 账户类型// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public FS.FrameWork.Models.NeuObject AccountType
        {
            get
            {
                if (accountType == null)
                {
                    accountType = new FS.FrameWork.Models.NeuObject();
                }

                return this.accountType;
            }
            set
            {
                this.accountType = value;
            }
        }
        /// <summary>
        /// 基本账户充值消费金额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal BaseCost
        {
            get
            {
                return this.baseCost;
            }
            set
            {
                this.baseCost = value;
            }
        }
        /// <summary>
        /// 赠送充值消费金额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal DonateCost
        {
            get
            {
                return this.donateCost;
            }
            set
            {
                this.donateCost = value;
            }
        }
        /// <summary>
        /// 赠送账户交易后余额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal DonateVacancy
        {
            get
            {
                return this.donateVacancy;
            }
            set
            {
                this.donateVacancy = value;
            }
        }
        /// <summary>
        /// 基本账户交易后余额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal BaseVacancy
        {
            get
            {
                return this.baseVacancy;
            }
            set
            {
                this.baseVacancy = value;
            }
        }
        /// <summary>
        /// 帐户患者信息
        /// </summary>
        public RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        /// <summary>
        /// 帐号
        /// </summary>
        public string AccountNO
        {
            get
            {
                return accountNO;
            }
            set
            {
                accountNO = value;
            }
        }

        /// <summary>
        /// 操作类型0预交金1建帐户2停帐户3重启帐户4支付5退费入户
        /// </summary>
        public EnumOperTypesService OperType
        {
            get
            {
                return operType;
            }
            set
            {
                operType = value;
            }
        }
        /// <summary>
        /// 消费类型：P购买套餐；R门诊挂号；C门诊消费；I住院结算；// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public EnumPayTypesService PayType
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

        /// <summary>
        /// 交费科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject FeeDept
        {
            get
            {
                return feeDept;
            }
            set
            {
                feeDept = value;
            }
        }

        /// <summary>
        /// 操作员
        /// </summary>
        public FS.FrameWork.Models.NeuObject Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime
        {
            get
            {
                return operTime;
            }
            set
            {
                operTime = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark
        {
            get
            {
                return reMark;
            }
            set
            {
                reMark = value;
            }
        }

        /// <summary>
        /// 交易状态
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        /// <summary>
        /// 被授权患者基本信息
        /// </summary>
        public HISFC.Models.RADT.Patient EmpowerPatient
        {
            get
            {
                return empwoerPatient;
            }
            set
            {
                empwoerPatient = value;
            }

        }

        /// <summary>
        /// 授权金额
        /// </summary>
        public decimal EmpowerCost
        {
            get
            {
                return empowerCost;
            }
            set
            {
                empowerCost = value;
            }
        }

        /// <summary>
        /// 发票类型
        /// </summary>
        public FS.FrameWork.Models.NeuObject InvoiceType
        {
            get
            {
                return invoiceType;
            }
            set
            {
                invoiceType = value;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new AccountRecord Clone()
        {
            AccountRecord accountCard = base.Clone() as AccountRecord;
            accountCard.patient = this.Patient.Clone();
            accountCard.empwoerPatient = this.EmpowerPatient.Clone();
            accountCard.operType = this.OperType.Clone() as EnumOperTypesService;
            accountCard.invoiceType = this.InvoiceType.Clone();
            return accountCard;
        }

        #endregion

    }
}
