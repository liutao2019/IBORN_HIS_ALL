using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.MedicalPackage.Fee
{
    public class Deposit : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// 交易类型 1-正交易，2-逆交易
        /// </summary>
        private string trans_Type = "1";

        public string Trans_Type
        {
            get { return this.trans_Type; }
            set { this.trans_Type = value; }
        }

        private string cardNO = string.Empty;

        /// <summary>
        /// 病人病历号
        /// </summary>
        public string CardNO
        {
            get { return this.cardNO; }
            set 
            {
                this.cardNO = value;
            }
        }

        /// <summary>
        /// 套餐划价单据号[3]
        /// </summary>
        private string recipeNO = string.Empty;

        public string RecipeNO
        {
            get { return this.recipeNO; }
            set { this.recipeNO = value; }
        }

        private DepositType depositType = DepositType.JYJ;

        /// <summary>
        /// 记录类型
        /// </summary>
        public DepositType DepositType
        {
            get { return this.depositType; }
            set { this.depositType = value; }
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        private string mode_code = string.Empty;

        public string Mode_Code
        {
            get { return this.mode_code; }
            set { this.mode_code = value; }
        }

        /// <summary>
        /// 金额
        /// </summary>
        private decimal amount = 0;

        public decimal Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }
        
        /// <summary>
        /// 银行信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject Bank = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 账号账号
        /// </summary>
        private string account = string.Empty;

        public string Account
        {
            get { return this.account; }
            set { this.account = value; }
        }

        /// <summary>
        /// 账号类型
        /// </summary>
        private string accountType = string.Empty;

        public string AccountType
        {
            get { return this.accountType; }
            set { this.accountType = value; }
        }

        /// <summary>
        /// 账户标识(0-充值账户，1-赠送账户)
        /// </summary>
        private string accountFlag = string.Empty;

        public string AccountFlag
        {
            get { return this.accountFlag; }
            set { this.accountFlag = value; }
        }

        /// <summary>
        /// POS号
        /// </summary>
        private string posNO = string.Empty;

        public string PosNO
        {
            get { return this.posNO; }
            set { this.posNO = value; }
        }

        /// <summary>
        /// 支票号
        /// </summary>
        private string checkNO = string.Empty;

        public string CheckNO
        {
            get { return this.checkNO; }
            set { this.checkNO = value; }
        }

        /// <summary>
        /// 操作人
        /// </summary>
        private OperEnvironment OperInfo = new OperEnvironment();

        /// <summary>
        /// 操作人
        /// </summary>
        public string Oper
        {
            get
            {
                if (this.OperInfo == null)
                {
                    this.OperInfo = new OperEnvironment();
                }
                return this.OperInfo.ID;
            }
            set
            {
                if (this.OperInfo == null)
                {
                    this.OperInfo = new OperEnvironment();
                }
                this.OperInfo.ID = value;
            }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime
        {
            get
            {
                if (this.OperInfo == null)
                {
                    this.OperInfo = new OperEnvironment();
                }
                return this.OperInfo.OperTime;
            }
            set
            {
                if (this.OperInfo == null)
                {
                    this.OperInfo = new OperEnvironment();
                }
                this.OperInfo.OperTime = value;
            }
        }

        /// <summary>
        /// 核查标识
        /// </summary>
        private string checkFlag = string.Empty;

        public string CheckFlag
        {
            get { return this.checkFlag; }
            set { this.checkFlag = value; }
        }

        /// <summary>
        /// 核查人
        /// </summary>
        private OperEnvironment CheckInfo = new OperEnvironment();

        /// <summary>
        /// 核查人
        /// </summary>
        public string CheckOper
        {
            get
            {
                if (this.CheckInfo == null)
                {
                    this.CheckInfo = new OperEnvironment();
                }
                return this.CheckInfo.ID;
            }
            set
            {
                if (this.CheckInfo == null)
                {
                    this.CheckInfo = new OperEnvironment();
                }
                this.CheckInfo.ID = value;
            }
        }

        /// <summary>
        /// 核查时间
        /// </summary>
        public DateTime CheckTime
        {
            get
            {
                if (this.CheckInfo == null)
                {
                    this.CheckInfo = new OperEnvironment();
                }
                return this.CheckInfo.OperTime;
            }
            set
            {
                if (this.CheckInfo == null)
                {
                    this.CheckInfo = new OperEnvironment();
                }
                this.CheckInfo.OperTime = value;
            }
        }

        /// <summary>
        /// 日结标记
        /// </summary>
        private string balanceFlag = string.Empty;

        public string BalanceFlag
        {
            get { return this.balanceFlag; }
            set { this.balanceFlag = value; }
        }

        /// <summary>
        /// 日结号
        /// </summary>
        private string balanceNO = string.Empty;

        public string BalanceNO
        {
            get { return this.balanceNO; }
            set { this.balanceNO = value; }
        }

        /// <summary>
        /// 日结信息
        /// </summary>
        private OperEnvironment BalanceInfo = new OperEnvironment();

        /// <summary>
        /// 日结人
        /// </summary>
        public string BalanceOper
        {
            get
            {
                if (this.BalanceInfo == null)
                {
                    this.BalanceInfo = new OperEnvironment();
                }
                return this.BalanceInfo.ID;
            }
            set
            {
                if (this.BalanceInfo == null)
                {
                    this.BalanceInfo = new OperEnvironment();
                }
                this.BalanceInfo.ID = value;
            }
        }

        /// <summary>
        /// 日结时间
        /// </summary>
        public DateTime BalanceTime
        {
            get
            {
                if (this.BalanceInfo == null)
                {
                    this.BalanceInfo = new OperEnvironment();
                }
                return this.BalanceInfo.OperTime;
            }
            set
            {
                if (this.BalanceInfo == null)
                {
                    this.BalanceInfo = new OperEnvironment();
                }
                this.BalanceInfo.OperTime = value;
            }
        }

        /// <summary>
        /// 对账标记
        /// </summary>
        private string correctFlag = string.Empty;

        public string CorrectFlag
        {
            get { return this.correctFlag; }
            set { this.correctFlag = value; }
        }

        /// <summary>
        /// 对账信息
        /// </summary>
        private OperEnvironment CorrectInfo = new OperEnvironment();

        /// <summary>
        /// 对账人
        /// </summary>
        public string CorrectOper
        {
            get
            {
                if (this.CorrectInfo == null)
                {
                    this.CorrectInfo = new OperEnvironment();
                }
                return this.CorrectInfo.ID;
            }
            set
            {
                if (this.CorrectInfo == null)
                {
                    this.CorrectInfo = new OperEnvironment();
                }
                this.CorrectInfo.ID = value;
            }
        }

        /// <summary>
        /// 对账时间
        /// </summary>
        public DateTime CorrectTime
        {
            get
            {
                if (this.CorrectInfo == null)
                {
                    this.CorrectInfo = new OperEnvironment();
                }
                return this.CorrectInfo.OperTime;
            }
            set
            {
                if (this.CorrectInfo == null)
                {
                    this.CorrectInfo = new OperEnvironment();
                }
                this.CorrectInfo.OperTime = value;
            }
        }

        /// <summary>
        /// 有效标志0-有效，1-退款，2-半退
        /// </summary>
        private string cancel_flag = string.Empty;

        public string Cancel_Flag
        {
            get { return this.cancel_flag; }
            set { this.cancel_flag = value; }
        }

        /// <summary>
        /// 取消人
        /// </summary>
        private OperEnvironment CancelInfo = new OperEnvironment();

        /// <summary>
        /// 取消人
        /// </summary>
        public string CancelOper
        {
            get
            {
                if (this.CancelInfo == null)
                {
                    this.CancelInfo = new OperEnvironment();
                }
                return this.CancelInfo.ID;
            }
            set
            {
                if (this.CancelInfo == null)
                {
                    this.CancelInfo = new OperEnvironment();
                }
                this.CancelInfo.ID = value;
            }
        }

        /// <summary>
        /// 取消时间
        /// </summary>
        public DateTime CancelTime
        {
            get
            {
                if (this.CancelInfo == null)
                {
                    this.CancelInfo = new OperEnvironment();
                }
                return this.CancelInfo.OperTime;
            }
            set
            {
                if (this.CancelInfo == null)
                {
                    this.CancelInfo = new OperEnvironment();
                }
                this.CancelInfo.OperTime = value;
            }
        }

        /// <summary>
        /// 原始单据号
        /// </summary>
        private string originalClinic = string.Empty;

        public string OriginalClinic
        {
            get { return this.originalClinic; }
            set { this.originalClinic = value; }
        }

        //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
        /// <summary>
        /// 分院id
        /// </summary>
        private string hospitalID = string.Empty;
        public string HospitalID
        {
            get { return this.hospitalID; }
            set { this.hospitalID = value; }
        }
        //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
        /// <summary>
        /// 分院名
        /// </summary>
        private string hospitalName = string.Empty;
        public string HospitalName
        {
            get { return this.hospitalName; }
            set { this.hospitalName = value; }
        }


        public new Deposit Clone(bool cloneExt)
        {
            Deposit deposit = new Deposit();
            deposit.ID = this.ID;
            deposit.Trans_Type = this.Trans_Type;
            deposit.RecipeNO = this.RecipeNO;
            deposit.CardNO = this.CardNO;
            deposit.Mode_Code = this.Mode_Code;
            deposit.Amount = this.Amount;
            deposit.Bank.ID = this.Bank.ID;
            deposit.Bank.Name = this.Bank.Name;
            deposit.Account = this.Account;
            deposit.AccountType = this.AccountType;
            deposit.AccountFlag = this.AccountFlag;
            deposit.PosNO = this.PosNO;
            deposit.CheckNO = this.CheckNO;
            deposit.Oper = this.Oper;
            deposit.OperTime = this.OperTime;
            deposit.OriginalClinic = this.OriginalClinic;
            deposit.Memo = this.Memo;
            deposit.DepositType = this.DepositType;
            deposit.CancelOper = this.CancelOper;
            deposit.CancelTime = this.CancelTime;
            deposit.Cancel_Flag = this.Cancel_Flag;
            deposit.HospitalID = this.HospitalID;  //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
            deposit.HospitalName = this.HospitalName;//{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
            //附加信息
            if (cloneExt)
            {
                deposit.CheckFlag = this.CheckFlag;
                deposit.CheckOper = this.CheckOper;
                deposit.CheckTime = this.CheckTime;
                deposit.BalanceFlag = this.BalanceFlag;
                deposit.BalanceNO = this.BalanceNO;
                deposit.BalanceOper = this.BalanceOper;
                deposit.BalanceTime = this.BalanceTime;
                deposit.CorrectFlag = this.CorrectFlag;
                deposit.CorrectOper = this.CorrectOper;
                deposit.CorrectTime = this.CorrectTime;
            }

            return deposit;
        }
    }

    public enum DepositType
    {
        /// <summary>
        /// 缴纳押金
        /// </summary>
        JYJ,

        /// <summary>
        /// 退押金
        /// </summary>
        TYJ,

        /// <summary>
        /// 套餐消费
        /// </summary>
        TCXF,

        /// <summary>
        /// 套餐退费
        /// </summary>
        TCTF,

        /// <summary>
        /// {F4EC0C51-BFAD-4e34-BF17-E2749E58CAE8}
        /// 消费返还
        /// </summary>
        XFHH
    }
}
