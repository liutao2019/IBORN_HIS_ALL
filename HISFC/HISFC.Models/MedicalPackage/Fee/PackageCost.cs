using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.MedicalPackage.Fee
{
    public class PackageCost : FS.FrameWork.Models.NeuObject
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

        private string sequenceNO = string.Empty;

        /// <summary>
        /// 单内序列号
        /// </summary>
        public string SequenceNO
        {
            get { return this.sequenceNO; }
            set { this.sequenceNO = value; }
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


        private string packageClinic = string.Empty;

        /// <summary>
        /// 套餐流水号
        /// </summary>
        public string PackageClinic
        {
            get { return packageClinic; }
            set { packageClinic = value; }
        }


        private string detailSeq = string.Empty;

        /// <summary>
        /// 套餐内流水号
        /// </summary>
        public string DetailSeq
        {
            get { return detailSeq; }
            set { detailSeq = value; }
        }

        private decimal amount = 0.0m;

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private string unit = string.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        private decimal tot_cost = 0;

        /// <summary>
        /// 套餐原价
        /// </summary>
        public decimal Tot_Cost
        {
            get { return this.tot_cost; }
            set { this.tot_cost = value; }
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

        /// <summary>
        /// 结账人
        /// </summary>
        private OperEnvironment OperInfo = new OperEnvironment();

        /// <summary>
        /// 结账人
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
        /// 结账时间
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

        private string balance_flag = string.Empty;

        /// <summary>
        /// 日结标识
        /// </summary>
        public string Balance_flag
        {
            get { return this.balance_flag; }
            set { this.balance_flag = value; }
        }


        private string balance_no = string.Empty;

        /// <summary>
        /// 日结号
        /// </summary>
        public string Balance_no
        {
            get { return this.balance_no; }
            set { this.balance_no = value; }
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

        //{DD31280F-7321-42BB-B150-4C63018ED85F} 
        /// <summary>
        /// 拥有套餐就诊卡号
        /// </summary>
        public string Has_Card_NO { set; get; }

        /// <summary>
        /// 使用人套餐就诊卡号
        /// </summary>
        public string Use_Card_NO { set; get; }

        //{6974FE57-7E0F-4c8f-AFC8-675CA7536C61}//{795AA18A-0093-492b-AAB9-DE654606A309}
        private string costid = "";
        public string COSTID { set; get; }

        private string costclinic = "";
        public string COSTCLINIC { set; get; }

        private string cardno = "";
        public string CARDNO { set; get; }

        private string costtype = "";
        public string COST_TYPE { set; get; }

        private string itemcode = "";
        public string ITEM_CODE { set; get; }

        private string hospitalid = "";
        public string HOSPITAL_ID { set; get; }

        private string hospitalname = "";
        public string HOSPITAL_NAME { set; get; }


        ////{EC52C67F-A234-4ef6-824E-34DF69778A6A}
        private string newpackageClinic = string.Empty;

        /// <summary>
        /// 套餐流水号
        /// </summary>
        public string NewPackageClinic
        {
            get { return newpackageClinic; }
            set { newpackageClinic = value; }
        }


        private string newdetailSeq = string.Empty;

        /// <summary>
        /// 套餐内流水号
        /// </summary>
        public string NewDetailSeq
        {
            get { return newdetailSeq; }
            set { newdetailSeq = value; }
        }
    }
}
