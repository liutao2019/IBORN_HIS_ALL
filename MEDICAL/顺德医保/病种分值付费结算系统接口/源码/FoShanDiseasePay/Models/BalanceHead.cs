using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiseasePay.Models
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class BalanceHead : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// 
        /// </summary>
        public BalanceHead()
        {
        }

        #region 变量

        /// <summary>
        ///头表编码
        /// </summary>
        private string payHeadNo;


        /// <summary>
        /// 明细表编码
        /// </summary>
        private string payDetailNo;


        /// <summary>
        /// 未付款凭证
        /// </summary>
        private string unpayCredence;

        /// <summary>
        /// 付款凭证
        /// </summary>
        private string payCredence;

        /// <summary>
        /// 未付款凭证日期
        /// </summary>
        private DateTime unpayCredenceTime;

        /// <summary>
        /// 入库单据号
        /// </summary>
        private string inListCode;

        /// <summary>
        /// 入库日期
        /// </summary>
        private DateTime inputDate;

        /// <summary>
        /// 发票号
        /// </summary>
        private string invoiceNo;

        /// <summary>
        /// 发票日期
        /// </summary>
        private DateTime invoiceTime;

        /// <summary>
        /// 零售金额
        /// </summary>
        private decimal retailCost;

        /// <summary>
        /// 批发金额
        /// </summary>
        private decimal wholesaleCost;

        /// <summary>
        /// 购入金额(发票金额)
        /// </summary>
        private decimal purchaseCost;

        /// <summary>
        /// 优惠金额
        /// </summary>
        private decimal discountCost;

        /// <summary>
        /// 运费
        /// </summary>
        private decimal deliveryCost;

        /// <summary>
        /// 同一发票内的付款序号
        /// </summary>
        private int sequenceNo;

        /// <summary>
        /// 付款标志 0未付款  1已付款 2完成付款
        /// </summary>
        private string payState;

        /// <summary>
        /// 付款类型 0现金/1支票
        /// </summary>
        private string payType;

        /// <summary>
        /// 付款金额
        /// </summary>
        private decimal payCost;

        /// <summary>
        /// 未付款金额
        /// </summary>
        private decimal unpayCost;

        /// <summary>
        /// 付款信息(人员 时间)
        /// </summary>
        private FS.FrameWork.Models.NeuObject payOper = new FS.FrameWork.Models.NeuObject();

        private DateTime payDate;

        public DateTime PayDate
        {
            get { return payDate; }
            set { payDate = value; }
        }

        /// <summary>
        /// 入库科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject stockDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 供货公司
        /// </summary>
        private FS.FrameWork.Models.NeuObject company = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 操作信息
        /// </summary>
        private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();

        private DateTime operDate;

        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }

        /// <summary>
        /// 流水单号
        /// </summary>
        private string listnum;


        private bool isDrug;

        #endregion

        #region 属性
        /// <summary>
        /// 头表编码
        /// </summary>
        public string PayHeadNo
        {
            get { return payHeadNo; }
            set { payHeadNo = value; }
        }

        /// <summary>
        /// 明细表编码
        /// </summary>
        public string PayDetailNo
        {
            get { return payDetailNo; }
            set { payDetailNo = value; }
        }

        /// <summary>
        /// 未付款凭证
        /// </summary>
        public string UnpayCredence
        {
            get
            {
                return this.unpayCredence;
            }
            set
            {
                this.unpayCredence = value;
            }
        }

        /// <summary>
        /// 未付款凭证日期
        /// </summary>
        public DateTime UnpayCredenceTime
        {
            get
            {
                return this.unpayCredenceTime;
            }
            set
            {
                this.unpayCredenceTime = value;
            }
        }

        /// <summary>
        /// 付款凭证
        /// </summary>
        public string PayCredence
        {
            get
            {
                return this.payCredence;
            }
            set
            {
                this.payCredence = value;
            }
        }

        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime InputDate
        {
            get
            {
                return this.inputDate;
            }
            set
            {
                this.inputDate = value;
            }
        }

        /// <summary>
        /// 入库单据号
        /// </summary>
        public string InListCode
        {
            get
            {
                return this.inListCode;
            }
            set
            {
                this.inListCode = value;
            }
        }

        /// <summary>
        /// 发票号
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
        /// 发票日期
        /// </summary>
        public DateTime InvoiceTime
        {
            get
            {
                return this.invoiceTime;
            }
            set
            {
                this.invoiceTime = value;
            }
        }

        /// <summary>
        /// 零售金额
        /// </summary>
        public decimal RetailCost
        {
            get
            {
                return this.retailCost;
            }
            set
            {
                this.retailCost = value;
            }
        }

        /// <summary>
        /// 批发金额
        /// </summary>
        public decimal WholesaleCost
        {
            get
            {
                return this.wholesaleCost;
            }
            set
            {
                this.wholesaleCost = value;
            }
        }

        /// <summary>
        /// 购入金额(发票金额)
        /// </summary>
        public decimal PurchaseCost
        {
            get
            {
                return this.purchaseCost;
            }
            set
            {
                this.purchaseCost = value;
            }
        }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountCost
        {
            get
            {
                return this.discountCost;
            }
            set
            {
                this.discountCost = value;
            }
        }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal DeliveryCost
        {
            get
            {
                return this.deliveryCost;
            }
            set
            {
                this.deliveryCost = value;
            }
        }

        /// <summary>
        /// 同一发票内付款序号
        /// </summary>
        public int SequenceNo
        {
            get
            {
                return this.sequenceNo;
            }
            set
            {
                this.sequenceNo = value;
            }
        }

        /// <summary>
        /// 付款标志0未付款1已付款2付款完成
        /// </summary>
        public string PayState
        {
            get
            {
                return this.payState;
            }
            set
            {
                this.payState = value;
            }
        }

        /// <summary>
        /// 付款类型0现金1发票
        /// </summary>
        public string PayType
        {
            get
            {
                return this.payType;
            }
            set
            {
                this.payType = value;
            }
        }

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PayCost
        {
            get
            {
                return this.payCost;
            }
            set
            {
                this.payCost = value;
            }
        }

        /// <summary>
        /// 未付款金额
        /// </summary>
        public decimal UnpayCost
        {
            get
            {
                return this.unpayCost;
            }
            set
            {
                this.unpayCost = value;
            }
        }

        /// <summary>
        /// 付款信息(人员 时间)
        /// </summary>
        public FS.FrameWork.Models.NeuObject PayOper
        {
            get
            {
                return this.payOper;
            }
            set
            {
                this.payOper = value;
            }
        }

        /// <summary>
        /// 入库科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject StockDept
        {
            get
            {
                return this.stockDept;
            }
            set
            {
                this.stockDept = value;
            }
        }

        /// <summary>
        /// 供货公司
        /// </summary>
        public FS.FrameWork.Models.NeuObject Company
        {
            get
            {
                return this.company;
            }
            set
            {
                this.company = value;
            }
        }

        /// <summary>
        /// 操作信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }
        /// <summary>
        /// 流水单号
        /// </summary>
        public string ListNum
        {
            get
            {
                return this.listnum;
            }
            set
            {
                this.listnum = value;
            }
        }

        /// <summary>
        /// 是否药品
        /// </summary>
        public bool IsDrug
        {
            get { return isDrug; }
            set { isDrug = value; }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 覆盖克隆方法
        /// </summary>
        /// <returns>返回当前实例副本</returns>
        public new BalanceHead Clone()
        {
            BalanceHead pay = base.Clone() as BalanceHead;

            pay.PayOper = this.PayOper.Clone();

            pay.StockDept = this.StockDept.Clone();

            pay.Company = this.Company.Clone();

            pay.Oper = this.Oper.Clone();

            return pay;
        }

        #endregion
    }
}
