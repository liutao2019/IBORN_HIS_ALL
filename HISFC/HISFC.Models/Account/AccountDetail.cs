using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    /// <summary>
    /// FS.HISFC.Models.Account.AccountDetail<br></br>
    /// [功能描述: 门诊帐户明细实体]<br></br>
    /// [创 建 者: LFHM]<br></br>
    /// [创建时间: 2017-03-14]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountDetail :FS.FrameWork.Models.NeuObject,IValidState
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountDetail()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 变量
        /// <summary>
        /// 门诊卡号
        /// </summary>
        private string cardNO = string.Empty;
        /// <summary>
        /// 账户类型
        /// </summary>
        protected FS.FrameWork.Models.NeuObject accountType;
        /// <summary>
        /// 帐户卡实体
        /// </summary>
        private AccountCard accountcard=new AccountCard();
        /// <summary>
        /// 账户交易信息列表
        /// </summary>
        private List<AccountRecord> accountRecordList = new List<AccountRecord>();
        /// <summary>
        /// 帐户交易卡实体// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private AccountRecord accountRecord = new AccountRecord();
        
        /// <summary>
        /// 帐户状态
        /// </summary>
        private EnumValidState validState= EnumValidState.Valid;
        /// <summary>
        /// 帐户密码
        /// </summary>
        private string password = string.Empty;
        /// <summary>
        /// 单日消费限制
        /// </summary>
        private decimal daylimit;

        /// <summary>
        /// 基本账户充值消费金额
        /// </summary>
        private decimal baseCost;

        /// <summary>
        /// 赠送账户充值消费金额
        /// </summary>
        private decimal donateCost;

        /// <summary>
        /// 充值积分
        /// </summary>
        private decimal couponCost;

        /// <summary>
        /// 基本账户余额
        /// </summary>
        private decimal baseVacancy;

        /// <summary>
        /// 赠送账户余额
        /// </summary>
        private decimal donateVacancy;

        /// <summary>
        /// 剩余积分
        /// </summary>
        private decimal couponVacancy;


        /// <summary>
        /// 基本账户累计金额
        /// </summary>
        private decimal baseAccumulate;

        /// <summary>
        /// 赠送账户累计金额
        /// </summary>
        private decimal donateAccumulate;

        /// <summary>
        /// 账户积分累计
        /// </summary>
        private decimal couponAccumulate;

        /// <summary>
        /// 操作信息
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// 创建信息
        /// </summary>				
        private OperEnvironment createEnvironment;


        #endregion

        #region 属性
        /// <summary>
        /// 账户赠送累计金额
        /// </summary>
        public decimal DonateAccumulate
        {
            get
            {
                return this.donateAccumulate;
            }
            set
            {
                this.donateAccumulate = value;
            }
        }
        /// <summary>
        /// 基本账户累计金额
        /// </summary>
        public decimal BaseAccumulate
        {
            get
            {
                return this.baseAccumulate;
            }
            set
            {
                this.baseAccumulate = value;
            }
        }

        /// <summary>
        /// 账户类型
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
        /// 账户积分累计
        /// </summary>
        public decimal CouponAccumulate
        {
            get
            {
                return this.couponAccumulate;
            }
            set
            {
                this.couponAccumulate = value;
            }
        }
        /// <summary>
        /// 充值积分
        /// </summary>
        public decimal CouponCost
        {
            get
            {
                return this.couponCost;
            }
            set
            {
                this.couponCost = value;
            }
        }
        /// <summary>
        /// 赠送账户充值消费金额
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
        /// 基本账户充值消费金额
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
        /// 积分账户余额
        /// </summary>
        public decimal CouponVacancy
        {
            get
            {
                return this.couponVacancy;
            }
            set
            {
                this.couponVacancy = value;
            }
        }
        /// <summary>
        /// 赠送账户余额
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
        /// 基本账户余额(充值)
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
        /// 操作环境
        /// </summary>
        public OperEnvironment OperEnvironment
        {
            get
            {
                if (operEnvironment == null)
                {
                    operEnvironment = new OperEnvironment();
                }
                return this.operEnvironment;
            }
            set
            {
                this.operEnvironment = value;
            }
        }


        /// <summary>
        /// 创建信息
        /// </summary>
        public OperEnvironment CreateEnvironment
        {
            get
            {
                if (createEnvironment == null)
                {
                    createEnvironment = new OperEnvironment();
                }
                return this.createEnvironment;
            }
            set
            {
                this.createEnvironment = value;
            }
        }
        /// <summary>
        /// 门诊卡号
        /// </summary>
        public string CardNO
        {
            get { return cardNO; }
            set { cardNO = value; }
        }


        /// <summary>
        /// 帐户卡实体
        /// </summary>
        public AccountCard AccountCard
        {
            get
            {
                return this.accountcard;
            }
            set
            {
                this.accountcard = value;
            }
        }
        /// <summary>
        /// 帐户卡实体
        /// </summary>
        public AccountRecord AccountRecord
        {
            get
            {
                return this.accountRecord;
            }
            set
            {
                this.accountRecord = value;
            }
        }
        /// <summary>
        /// 帐户交易实体
        /// </summary>
        public List<AccountRecord> AccountRecordList
        {
            get
            {
                return this.accountRecordList;
            }
            set
            {
                this.accountRecordList = value;
            }
        }

         ///<summary>
         ///帐户状态'1'正常'0'停用
         ///</summary>
        public EnumValidState IsValid
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
        }


        /// <summary>
        /// 帐户密码
        /// </summary>
        public string PassWord
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        /// <summary>
        /// 单日消费限制
        /// </summary>
        public decimal DayLimit
        {
            get
            {
                return this.daylimit;
            }
            set
            {
                this.daylimit = value;
            }
        }
        #endregion 

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new AccountDetail Clone()
        {
            AccountDetail accountDetail = base.Clone() as AccountDetail;
            accountDetail.AccountCard = this.AccountCard.Clone();
            accountDetail.AccountRecord = this.AccountRecord.Clone();
            if (AccountRecordList.Count > 0)
            {
                foreach (AccountRecord ard in accountRecordList)
                {
                    accountDetail.AccountRecordList.Add(ard.Clone());
                }
            }

            return accountDetail;
        }
        #endregion

        #region IValidState 成员
        /// <summary>
        /// 帐户状态0停用 1正常 2注销
        /// </summary>
        public EnumValidState ValidState
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
        }

        #endregion
    }

 
}
