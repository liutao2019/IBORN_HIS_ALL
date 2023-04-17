using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.MedicalPackage.Fee
{
    public class Invoice : FS.FrameWork.Models.NeuObject
    {
        private string invoiceNO = string.Empty;

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNO
        {
            get { return this.invoiceNO; }
            set { this.invoiceNO = value; }
        }

        private string trans_Type = "1";

        /// <summary>
        /// 交易类型 1-正交易，2-逆交易
        /// </summary>
        public string Trans_Type
        {
            get { return this.trans_Type; }
            set { this.trans_Type = value; }
        }

        private string paykindcode = string.Empty;

        /// <summary>
        /// 合同单位
        /// </summary>
        public string Paykindcode
        {
            get { return this.paykindcode; }
            set { this.paykindcode = value; }
        }

        private string card_Level = string.Empty;

        /// <summary>
        /// 会员等级
        /// </summary>
        public string Card_Level
        {
            get { return this.card_Level; }
            set { this.card_Level = value; }
        }

        private decimal package_cost = 0;

        /// <summary>
        /// 套餐原价
        /// </summary>
        public decimal Package_Cost
        {
            get { return this.package_cost; }
            set { this.package_cost = value; }
        }

        private decimal real_cost = 0;

        /// <summary>
        /// 实收原价
        /// </summary>
        public decimal Real_Cost
        {
            get { return this.real_cost; }
            set { this.real_cost = value; }
        }

        private decimal gift_cost = 0;

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal Gift_cost
        {
            get { return this.gift_cost; }
            set { this.gift_cost = value; }
        }

        private decimal etc_cost = 0;

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal Etc_cost
        {
            get { return this.etc_cost; }
            set { this.etc_cost = value; }
        }

        private string invoiceseq = string.Empty;

        /// <summary>
        /// 发票序号
        /// </summary>
        public string InvoiceSeq
        {
            get { return this.invoiceseq; }
            set { this.invoiceseq = value; }
        }

        private string printInvoiceNO = string.Empty;

        /// <summary>
        /// 发票号
        /// </summary>
        public string PrintInvoiceNO
        {
            get { return this.printInvoiceNO; }
            set { this.printInvoiceNO = value; }
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
        /// 日结信息
        /// </summary>
        private OperEnvironment CorrectInfo = new OperEnvironment();

        /// <summary>
        /// 日结人
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
        /// 日结时间
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


        public new Invoice Clone(bool cloneExt)
        {
            Invoice invoice = new Invoice();
            invoice.ID = this.ID;
            invoice.Trans_Type = this.Trans_Type;
            invoice.InvoiceNO = this.InvoiceNO;
            invoice.Paykindcode = this.Paykindcode;
            invoice.Card_Level = this.Card_Level;
            invoice.Package_Cost = this.Package_Cost;
            invoice.Real_Cost = this.Real_Cost;
            invoice.Gift_cost = this.Gift_cost;
            invoice.Etc_cost = this.Etc_cost;
            invoice.PrintInvoiceNO = this.PrintInvoiceNO;
            invoice.InvoiceSeq = this.InvoiceSeq;
            invoice.Oper = this.Oper;
            invoice.OperTime = this.OperTime;
            invoice.Memo = this.Memo;
            invoice.CancelOper = this.CancelOper;
            invoice.CancelTime = this.CancelTime;
            invoice.Cancel_Flag = this.Cancel_Flag;
            invoice.Hospital_ID = this.Hospital_ID; //  {D59EF243-868D-41a0-9827-5E2E608522CA}
            invoice.HospitalName = this.HospitalName; //  {D59EF243-868D-41a0-9827-5E2E608522CA}
            //附加信息
            if (cloneExt)
            {
                invoice.CheckFlag = this.CheckFlag;
                invoice.CheckOper = this.CheckOper;
                invoice.CheckTime = this.CheckTime;
                invoice.BalanceFlag = this.BalanceFlag;
                invoice.BalanceNO = this.BalanceNO;
                invoice.BalanceOper = this.BalanceOper;
                invoice.BalanceTime = this.BalanceTime;
                invoice.CorrectFlag = this.CorrectFlag;
                invoice.CorrectOper = this.CorrectOper;
                invoice.CorrectTime = this.CorrectTime;
            }

            return invoice;
        }

    }
}
