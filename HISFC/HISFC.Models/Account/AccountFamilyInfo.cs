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
    public class AccountFamilyInfo :FS.FrameWork.Models.NeuObject,IValidState
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountFamilyInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 变量
        /// <summary>
        /// 被关联人病历号
        /// </summary>
        private string cardNO = string.Empty;
        /// <summary>
        /// 关联人病历号
        /// </summary>
        private string linkedCardNO = string.Empty;

        /// <summary>
        /// 被关联人账户
        /// </summary>
        private string accountNo = string.Empty;

        /// <summary>
        /// 关联人账户
        /// </summary>
        private string linkedAccountNo = string.Empty;
        /// <summary>
        /// 关系
        /// </summary>
        protected FS.FrameWork.Models.NeuObject relation;
        /// <summary>
        /// 性别
        /// </summary>
        protected FS.FrameWork.Models.NeuObject sex;

        /// <summary>
        /// 出生日期
        /// </summary>
        private DateTime birthday = new DateTime();

        /// <summary>
        /// 证件类型
        /// </summary>
        protected FS.FrameWork.Models.NeuObject cardType;

        /// <summary>
        /// 证件号码
        /// </summary>
        private string idCardNo = string.Empty;

        /// <summary>
        /// 联系人电话
        /// </summary>
        private string phone = string.Empty;

        /// <summary>
        /// 联系人地址
        /// </summary>
        private string address = string.Empty; 

        /// <summary>
        /// 帐户卡实体
        /// </summary>
        private AccountCard accountcard = new AccountCard();

        /// <summary>
        /// 是否有效
        /// </summary>
        private EnumValidState validState= EnumValidState.Valid;
        /// <summary>
        /// 操作信息
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// 创建信息
        /// </summary>				
        private OperEnvironment createEnvironment;


        /// <summary>
        /// 家庭号 
        /// </summary>
        private string familyCode = string.Empty;
        /// <summary>
        /// 家庭名称 
        /// </summary>
        private string familyName = string.Empty;
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
        /// 被关联人病历号
        /// </summary>
        public string CardNO
        {
            get { return cardNO; }
            set { cardNO = value; }
        }

        /// <summary>
        /// 被关联人账号
        /// </summary>
        public string AccountNo
        {
            get { return this.accountNo; }
            set { accountNo = value; }
        }
        /// <summary>
        /// 关联人账号
        /// </summary>
        public string LinkedAccountNo
        {
            get { return this.linkedAccountNo; }
            set { linkedAccountNo = value; }
        }
        /// <summary>
        /// 关联人病历号
        /// </summary>
        public string LinkedCardNO
        {
            get { return this.linkedCardNO; }
            set { linkedCardNO = value; }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday
        {
            get { return this.birthday; }
            set { birthday = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public FS.FrameWork.Models.NeuObject Sex
        {
            get
            {
                if (this.sex == null)
                {
                    sex = new FS.FrameWork.Models.NeuObject();
                }

                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }

        /// <summary>
        /// 关系
        /// </summary>
        public FS.FrameWork.Models.NeuObject Relation
        {
            get
            {
                if (this.relation == null)
                {
                    relation = new FS.FrameWork.Models.NeuObject();
                }

                return this.relation;
            }
            set
            {
                this.relation = value;
            }
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        public FS.FrameWork.Models.NeuObject CardType
        {
            get
            {
                if (this.cardType == null)
                {
                    cardType = new FS.FrameWork.Models.NeuObject();
                }

                return this.cardType;
            }
            set
            {
                this.cardType = value;
            }
        }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDCardNo
        {
            get { return this.idCardNo; }
            set { idCardNo = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone
        {
            get { return this.phone; }
            set { phone = value; }
        }

        /// <summary>
        /// 现住址
        /// </summary>
        public string Address
        {
            get { return this.address; }
            set { address = value; }
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


        #endregion 

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new AccountFamilyInfo Clone()
        {
            AccountFamilyInfo accountFamilyInfo = base.Clone() as AccountFamilyInfo;
            accountFamilyInfo.AccountCard = this.AccountCard.Clone();
            return accountFamilyInfo;
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
