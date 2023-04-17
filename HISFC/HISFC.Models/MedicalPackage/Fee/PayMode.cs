using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.MedicalPackage.Fee
{
    public class PayMode : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// 发票号
        /// </summary>
        private string invoiceNO = string.Empty;

        public string InvoiceNO
        {
            get { return this.invoiceNO; }
            set { this.invoiceNO = value; }
        }

        /// <summary>
        /// 交易类型 1-正交易，2-逆交易
        /// </summary>
        private string trans_Type = "1";

        public string Trans_Type
        {
            get { return this.trans_Type; }
            set { this.trans_Type = value; }
        }

        /// <summary>
        /// 交易流水号
        /// </summary>
        private string sequenceNO = string.Empty;

        public string SequenceNO
        {
            get { return this.sequenceNO; }
            set { this.sequenceNO = value; }
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

        private string related_ID = string.Empty;

        /// <summary>
        /// 相关ID
        /// </summary>
        public string Related_ID
        {
            get { return this.related_ID; }
            set { this.related_ID = value; }
        }

        private string related_ModeCode = string.Empty;

        /// <summary>
        /// 相关记录的MODECODE
        /// </summary>
        public string Related_ModeCode
        {
            get { return this.related_ModeCode; }
            set { this.related_ModeCode = value; }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        private decimal tot_cost = 0;

        public decimal Tot_cost
        {
            get { return this.tot_cost; }
            set { this.tot_cost = value; }
        }

        /// <summary>
        /// 实收金额
        /// </summary>
        private decimal real_cost = 0;

        public decimal Real_Cost
        {
            get { return this.real_cost; }
            set { this.real_cost = value; }
        }

        /// <summary>
        /// 银行信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject Bank = new FS.FrameWork.Models.NeuObject();

        private string account = string.Empty;

        /// <summary>
        /// 账号账号
        /// </summary>
        public string Account
        {
            get { return this.account; }
            set { this.account = value; }
        }

        private string accountType = string.Empty;

        /// <summary>
        /// 账号类型
        /// </summary>
        public string AccountType
        {
            get { return this.accountType; }
            set { this.accountType = value; }
        }

        private string accountFlag = string.Empty;

        /// <summary>
        /// 账户标识(0-充值账户，1-赠送账户)
        /// </summary>
        public string AccountFlag
        {
            get { return this.accountFlag; }
            set { this.accountFlag = value; }
        }

        private string posNO = string.Empty;

        /// <summary>
        /// POS号
        /// </summary>
        public string PosNO
        {
            get { return this.posNO; }
            set { this.posNO = value; }
        }

        private string checkNO = string.Empty;

        /// <summary>
        /// 支票号
        /// </summary>
        public string CheckNO
        {
            get { return this.checkNO; }
            set { this.checkNO = value; }
        }  
        
        /// <summary>
        /// 结账人
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

        private string checkFlag = string.Empty;

        /// <summary>
        /// 核查标识
        /// </summary>
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

        private string balanceFlag = string.Empty;

        /// <summary>
        /// 日结标记
        /// </summary>
        public string BalanceFlag
        {
            get { return this.balanceFlag; }
            set { this.balanceFlag = value; }
        }

        private string balanceNO = string.Empty;

        /// <summary>
        /// 日结号
        /// </summary>
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

        private string correctFlag = string.Empty;

        /// <summary>
        /// 对账标记
        /// </summary>
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

        private string cancel_flag = string.Empty;

        /// <summary>
        /// 有效标志0-有效，1-退款，2-半退
        /// </summary>
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

        //  {D59EF243-868D-41a0-9827-5E2E608522CA}
        /// <summary>
        /// 分院id
        /// </summary>
        private string hospital_ID = string.Empty;

        /// <summary>
        /// 分院id
        /// </summary>
        public string Hospital_ID
        {
            get { return this.hospital_ID; }
            set { this.hospital_ID = value; }
        }

        //  {D59EF243-868D-41a0-9827-5E2E608522CA}
        /// <summary>
        /// 分院名
        /// </summary>
        private string hospitalName = string.Empty;

        /// <summary>
        /// 分院名
        /// </summary>
        public string HospitalName
        {
            get { return this.hospitalName; }
            set { this.hospitalName = value; }
        }


        public new PayMode Clone(bool cloneExt)
        {
            PayMode paymode = new PayMode();
            paymode.InvoiceNO = this.InvoiceNO;
            paymode.Trans_Type = this.Trans_Type;
            paymode.SequenceNO = this.SequenceNO;
            paymode.Mode_Code = this.Mode_Code;
            paymode.Related_ID = this.Related_ID;
            paymode.Related_ModeCode = this.Related_ModeCode;
            paymode.Tot_cost = this.Tot_cost;
            paymode.Real_Cost = this.Real_Cost;
            paymode.Bank.ID = this.Bank.ID;
            paymode.Bank.Name = this.Bank.Name;
            paymode.Account = this.Account;
            paymode.AccountType = this.AccountType;
            paymode.AccountFlag = this.AccountFlag;
            paymode.PosNO = this.PosNO;
            paymode.CheckNO = this.CheckNO;
            paymode.Oper = this.Oper;
            paymode.OperTime = this.OperTime;
            paymode.Memo = this.Memo;
            paymode.CancelOper = this.CancelOper;
            paymode.CancelTime = this.CancelTime;
            paymode.Cancel_Flag = this.Cancel_Flag;
            paymode.Hospital_ID = this.Hospital_ID;
            paymode.HospitalName = this.HospitalName;

            //附加信息
            if (cloneExt)
            {
                paymode.CheckFlag = this.CheckFlag;
                paymode.CheckOper = this.CheckOper;
                paymode.CheckTime = this.CheckTime;
                paymode.BalanceFlag = this.BalanceFlag;
                paymode.BalanceNO = this.BalanceNO;
                paymode.BalanceOper = this.BalanceOper;
                paymode.BalanceTime = this.BalanceTime;
                paymode.CorrectFlag = this.CorrectFlag;
                paymode.CorrectOper = this.CorrectOper;
                paymode.CorrectTime = this.CorrectTime;
            }

            return paymode;
        }

    }
}
