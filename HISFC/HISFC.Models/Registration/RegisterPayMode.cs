using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Registration
{
    public class RegisterPayMode:FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// 发票号（流水号）
        /// </summary>
        string invoiceNo = string.Empty;

        /// <summary>
        /// 发票号（流水号）
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        private string sequenceNO = string.Empty;
        /// <summary>
        /// 序列号
        /// </summary>
        public string SequenceNO
        {
            get { return sequenceNO; }
            set { sequenceNO = value; }
        }

        /// <summary>
        /// 交易类型
        /// </summary>
        private TransTypes transType;

        /// <summary>
        /// 交易类型 
        /// </summary>
        public TransTypes TransType
        {
            get { return transType; }
            set { transType = value; }
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        private string mode_Code;

        /// <summary>
        /// 支付方式
        /// </summary>
        public string Mode_Code
        {
            get { return mode_Code; }
            set { mode_Code = value; }
        }

        /// <summary>
        /// 账号ID
        /// </summary>
        private string accountID;

        /// <summary>
        /// 账号ID
        /// </summary>
        public string AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        /// <summary>
        /// 账号类型
        /// </summary>
        private string accountType;

        /// <summary>
        /// 账号类型
        /// </summary>
        public string AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        /// <summary>
        /// 基本赠送标识
        /// </summary>
        private string accountFlag;

        /// <summary>
        /// 基本赠送标识
        /// </summary>
        public string AccountFlag
        {
            get { return accountFlag; }
            set { accountFlag = value; }
        }


        /// <summary>
        /// 费用总额
        /// </summary>
        private decimal tot_cost = 0;

        /// <summary>
        /// 费用总额
        /// </summary>
        public decimal Tot_cost
        {
            get { return tot_cost; }
            set { tot_cost = value; }
        }

        /// <summary>
        /// 实收
        /// </summary>
        private decimal real_cost = 0;

        /// <summary>
        /// 实收
        /// </summary>
        public decimal Real_cost
        {
            get { return real_cost; }
            set { real_cost = value; }
        }

        /// <summary>
        /// 打印发票号
        /// </summary>
        string print_invoiceNo = string.Empty;

        /// <summary>
        /// 打印发票号
        /// </summary>
        public string Print_InvoiceNo
        {
            get { return print_invoiceNo; }
            set { print_invoiceNo = value; }
        }

        /// <summary>
        /// 操作者
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();

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
        bool isBalance = false;

        /// <summary>
        /// 是否日结
        /// </summary>
        public bool IsBalance
        {
            get { return isBalance; }
            set { isBalance = value; }
        }

        /// <summary>
        /// 日结标识号
        /// </summary>
        string balanceNo = string.Empty;

        /// <summary>
        /// 日结标识号
        /// </summary>
        public string BalanceNo
        {
            get { return balanceNo; }
            set { balanceNo = value; }
        }

        /// <summary>
        /// 日结信息
        /// </summary>
        private OperEnvironment balanceOper = new OperEnvironment();

        /// <summary>
        /// 日结信息
        /// </summary>
        public OperEnvironment BalanceOper
        {
            get { return balanceOper; }
            set { balanceOper = value; }
        }

        /// <summary>
        /// 状态， 1 = 有效 0 = 无效 2 = 退费
        /// </summary>
        private int cancelFlag = 1;

        /// <summary>
        /// 状态， 1 = 有效 0 = 无效 2 = 退费
        /// </summary>
        public int CancelFlag
        {
            get { return cancelFlag; }
            set { cancelFlag = value; }
        }

        /// <summary>
        /// 取消人
        /// </summary>
        private OperEnvironment cancelOper = new OperEnvironment();

        /// <summary>
        /// 取消人
        /// </summary>
        public OperEnvironment CancelOper
        {
            get { return cancelOper; }
            set { cancelOper = value; }
        }


        private string hospital_id=string.Empty;

        /// <summary>
        /// 院区id{3515892E-1541-47de-8E0B-E306798A358C}
        /// </summary>
        public string Hospital_id
        {
            get { return hospital_id; }
            set { hospital_id = value; }
        }


        private string hospital_name=string.Empty;
        /// <summary>
        /// 院区名
        /// </summary>
        public string Hospital_name
        {
            get { return hospital_name; }
            set { hospital_name = value; }
        }
    }
}
