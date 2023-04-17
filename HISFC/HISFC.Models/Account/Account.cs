using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    /// <summary>
    /// FS.HISFC.Models.Account.Account<br></br>
    /// [功能描述: 门诊帐户实体]<br></br>
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
    public class Account :FS.FrameWork.Models.NeuObject,IValidState
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Account()
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
        /// 帐户卡实体
        /// </summary>
        private AccountCard accountcard=new AccountCard();
        /// <summary>
        /// 帐户交易实体
        /// </summary>
        private List<AccountRecord> accountRecord = new List<AccountRecord>();
        
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
        /// 是否授权
        /// </summary>
        private bool isEmpower=false;
        /// <summary>
        /// 基本账户充值消费金额
        /// </summary>
        private decimal baseCost;
        /// <summary>
        /// 赠送账户充值消费金额
        /// </summary>
        private decimal donateCost;

        /// <summary>
        /// 基本账户余额
        /// </summary>
        private decimal baseVacancy;

        /// <summary>
        /// 积分
        /// </summary>
        private decimal couponCost;

        /// <summary>
        /// 赠送账户余额
        /// </summary>
        private decimal donateVacancy;

        /// <summary>
        /// 积分账户余额
        /// </summary>
        private decimal couponVacancy;

        /// <summary>
        /// 警戒值(作用：基本账户余额不能低于该值)
        /// </summary>
        private decimal limit;

        /// <summary>
        /// 基本账户累计金额
        /// </summary>
        private decimal baseAccumulate;

        /// <summary>
        /// 赠送账户累计金额
        /// </summary>
        private decimal donateAccumulate;

        /// <summary>
        /// 账户积分累计// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private decimal couponAccumulate;
        /// <summary>
        /// 会员等级:1 普通会员卡；2 会员卡；3 黄金会员卡；4白金会员卡；5钻石会员卡；6至尊会员卡// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        protected FS.FrameWork.Models.NeuObject accountLevel;

        /// <summary>
        /// 操作信息// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// 创建信息// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>				
        private OperEnvironment createEnvironment;


        #endregion

        #region 属性 
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
        /// 账户赠送累计金额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 基本账户累计金额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 账户积分累计// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 警戒值(作用：基本账户余额不能低于该值)// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal Limit
        {
            get
            {
                return this.limit;
            }
            set
            {
                this.limit = value;
            }
        }
        /// <summary>
        /// 积分账户余额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 赠送账户余额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 基本账户余额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 基本账户充值消费// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 赠送账户充值消费// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 积分// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 操作环境// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 创建信息// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// 帐户交易实体
        /// </summary>
        public List<AccountRecord> AccountRecord
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
        /// 帐户状态'1'正常'0'停用
        /// </summary>
        //public EnumValidState IsValid
        //{
        //    get
        //    {
        //        return validState;
        //    }
        //    set
        //    {
        //        validState = value;
        //    }
        //}


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

        /// <summary>
        /// 是否授权
        /// </summary>
        public bool IsEmpower
        {
            get
            {
                return isEmpower;
            }
            set
            {
                isEmpower = value;
            }
        }
        #endregion 

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new Account Clone()
        {
            Account account = base.Clone() as Account;
            account.AccountCard = this.AccountCard.Clone();
            if (AccountRecord.Count > 0)
            {
                foreach (AccountRecord ard in accountRecord)
                {
                    account.AccountRecord.Add(ard.Clone());
                }
            }

            return account;
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
