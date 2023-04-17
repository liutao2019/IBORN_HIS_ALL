using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class PrePay : NeuObject, IValidState, IValid
    {

        #region 变量

        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        /// <summary>
        /// 帐号
        /// </summary>
        private string accountNO = string.Empty;

        /// <summary>
        /// 发生序号
        /// </summary>
        private int happenNo = 0;

        /// <summary>
        /// 预交金发票号
        /// </summary>
        private string invoiceNO = string.Empty;

        /// <summary>
        /// 支付类型{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
        /// </summary>
        //private Fee.EnumPayTypeService payType = new FS.HISFC.Models.Fee.EnumPayTypeService();
        private FS.FrameWork.Models.NeuObject payType = new NeuObject();

        /// <summary>
        /// 账户类型// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private FS.FrameWork.Models.NeuObject accountType = new NeuObject();

        /// <summary>
        /// 开户银行
        /// </summary>
        private Bank bank = new Bank();

        /// <summary>
        /// 预交金操作信息
        /// </summary>
        private OperEnvironment prePayOper = new OperEnvironment();
        /// <summary>
        /// 费用信息 包含预交金费用等
        /// </summary>
        private FT ft = new FT();
        /// <summary>
        /// 状态 0反还，1收取，2重打
        /// </summary>
        private EnumValidState validState = EnumValidState.Valid;

        /// <summary>
        /// 结算操作信息
        /// </summary>
        private OperEnvironment balanceOper = new OperEnvironment();

        /// <summary>
        /// 是否日结
        /// </summary>
        private bool valid = false;

        /// <summary>
        /// 原票据号
        /// </summary>
        private string oldInvoice = string.Empty;

        /// <summary>
        /// 重打次数
        /// </summary>
        private int printTimes = 0;

        /// <summary>
        /// 日结序号
        /// </summary>
        private string balanceNo = string.Empty;

        /// <summary>
        /// 是否历史数据
        /// </summary>
        private bool isHostory = false;
        /// <summary>
        /// 打印单据号// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private string printNo;
        /// <summary>
        /// 基本账户金额
        /// </summary>
        private decimal baseCost;
        /// <summary>
        /// 赠送金额
        /// </summary>
        private decimal donateCost;

        /// <summary>
        /// 基本账户交易后余额
        /// </summary>
        private decimal baseVacancy;

        /// <summary>
        /// 赠送账户交易后余额
        /// </summary>
        private decimal donateVacancy;


        private string hospital_id;

        private string hospital_name;

        /// <summary>
        /// {37bda347-40b4-40c1-9534-520c0267ef07}
        /// </summary>
        private string memo;

        #endregion

        #region 属性

        /// <summary>
        /// 基本账户金额
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
        /// 赠送金额
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
        /// 基本账户交易后余额
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
        /// 赠送账户交易后余额
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
        /// 费用信息 包含预交金费用等
        /// </summary>
        public FT FT
        {
            get { return ft; }
            set { ft = value; }

        }
        /// <summary>
        /// 患者基本信息
        /// </summary>
        public HISFC.Models.RADT.Patient Patient
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
            get { return accountNO; }
            set { accountNO = value; }
        }

        /// <summary>
        /// 发生序号
        /// </summary>
        public int HappenNO
        {
            get
            {
                return happenNo;
            }
            set
            {
                happenNo = value;
            }
        }

        /// <summary>
        ///  预交金发票号
        /// </summary>
        public string InvoiceNO
        {
            get { return invoiceNO; }
            set { invoiceNO = value; }
        }

        /// <summary>
        /// 支付类型{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
        /// </summary>
        //public Fee.EnumPayTypeService PayType
        public NeuObject PayType
        {
            get { return payType; }
            set { payType = value; }
        }
        /// <summary>
        /// 账户类型// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public NeuObject AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        /// <summary>
        /// 开户银行
        /// </summary>
        public Bank Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        /// <summary>
        /// 预交金操作信息
        /// </summary>
        public OperEnvironment PrePayOper
        {
            get { return prePayOper; }
            set { prePayOper = value; }
        }

        /// <summary>
        /// 结算操作信息
        /// </summary>
        public OperEnvironment BalanceOper
        {
            get { return balanceOper; }
            set { balanceOper = value; }
        }

        /// <summary>
        /// 日结序号
        /// </summary>
        public string BalanceNO
        {
            get
            {
                return balanceNo;
            }
            set
            {
                balanceNo = value;
            }
        }

        /// <summary>
        /// 原票据号
        /// </summary>
        public string OldInvoice
        {
            get
            {
                return oldInvoice;
            }
            set
            {
                oldInvoice = value;
            }
        }

        /// <summary>
        /// 重打次数
        /// </summary>
        public int PrintTimes
        {
            get
            {
                return printTimes;
            }
            set
            {
                printTimes = value;
            }
        }

        /// <summary>
        /// 是否历史数据(在结清帐户时以前的的预交金为历史数据)
        /// </summary>
        public bool IsHostory
        {
            get
            {
                return isHostory;
            }
            set
            {
                isHostory = value;
            }
        }

        /// <summary>
        ///  打印单据号// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public string PrintNo
        {
            get { return printNo; }
            set { printNo = value; }
        }

        /// <summary>
        /// 院区相关{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        public string Hospital_id
        {
            get { return hospital_id; }
            set { hospital_id = value; }
        }

        public string Hospital_name
        {
            get { return hospital_name; }
            set { hospital_name = value; }
        }

        #endregion

        #region IValidState 成员
        /// <summary>
        /// 预交金状态 0反还，1收取，2重打
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

        #region IValid 成员
        /// <summary>
        /// 是否日结
        /// </summary>
        public bool IsValid
        {
            get
            {
                return valid;
            }
            set
            {
                valid = value;

            }
        }

        #endregion

        #region Memo

        /// <summary>
        /// 备注{37bda347-40b4-40c1-9534-520c0267ef07}
        /// </summary>
        public string Memo
        {
            get
            {
                return memo;
            }
            set
            {
                memo = value;

            }
        }
        #endregion


        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new PrePay Clone()
        {
            PrePay prepay = base.Clone() as PrePay;
            prepay.patient = this.Patient.Clone();
            prepay.prePayOper = this.PrePayOper.Clone();
            prepay.balanceOper = this.BalanceOper.Clone();
            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
            //prepay.payType = this.PayType.Clone() as Fee.EnumPayTypeService;
            prepay.FT = this.FT.Clone();
            prepay.payType = this.PayType.Clone();
            prepay.bank = this.Bank.Clone();

            return prepay;
        }
        #endregion
    }
}
