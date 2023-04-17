using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class ParkingTicketFeeInfo : NeuObject, IValidState
    {

        #region 变量

        /// <summary>
        /// 结算号
        /// </summary>
        private string invoiceNo = string.Empty;

        /// <summary>
        /// 交易类型 TransTypes.Positive 正交易(1), TransTypes.Negative 负交易(2)
        /// </summary>
        private TransTypes transType;

        /// <summary>
        /// 项目编码
        /// </summary>
        private string itemCode = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        private string itemName = "";
        /// <summary>
        /// 单价
        /// </summary>
        private decimal unitPrice = 0m;
        /// <summary>
        /// 购买数量
        /// </summary>
        private decimal qty = 0m;
        /// <summary>
        /// 单位
        /// </summary>
        private string unit = "";

        /// <summary>
        ///   交易金额
        /// </summary>
        private decimal totCost = 0m;
        /// <summary>
        /// 支付方式
        /// </summary>
        private FS.FrameWork.Models.NeuObject payMode = new NeuObject();
        /// <summary>
        /// 停车票号
        /// </summary>
        private string ticketNo = "";
        /// <summary>
        ///  旧的票据号
        /// </summary>
        private string oldInvoiceNo = "";
        /// <summary>
        /// 作废时间
        /// </summary>
        private DateTime cancelDate = new DateTime();
        /// <summary>
        /// 退费的停车票号
        /// </summary>
        private string oldTicketNo = "";
        /// <summary>
        /// 备注
        /// </summary>
        private string memo = "";
        /// <summary>
        /// 扩展字段1
        /// </summary>
        private string flag1 = "";
        /// <summary>
        /// 扩展字段2
        /// </summary>
        private string flag2 = "";
        /// <summary>
        /// 扩展字段3
        /// </summary>
        private string flag3 = "";
        /// <summary>
        /// 是否有效
        /// </summary>
        private EnumValidState validState = EnumValidState.Valid;

        /// <summary>
        /// 操作信息
        /// </summary>				
        private OperEnvironment operEnvironment;
        /// <summary>
        /// 是否日结，0未日结/1已日结
        /// </summary>
        private bool isBalance = false;
        /// <summary>
        /// 日结号
        /// </summary>
        private string balanceNo = "";
        /// <summary>
        /// 日结信息
        /// </summary>				
        private OperEnvironment balanceEnvironment;
        /// <summary>
        /// 是否审核，0未核查/1已核查
        /// </summary>
        private bool isCheck = false;
        /// <summary>
        /// 审核信息
        /// </summary>				
        private OperEnvironment checkEnvironment;


        private string hospital_id;

        private string hospital_name;
        #endregion

        #region 属性


        /// <summary>
        /// 结算号
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        /// <summary>
        /// 交易类型 TransTypes.Positive 正交易(1), TransTypes.Negative 负交易(2)
        /// </summary>
        public TransTypes TransType
        {
            get { return transType; }
            set { transType = value; }
        }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ItemCode
        {
            get { return this.itemCode; }
            set { itemCode = value; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            get { return this.itemName; }
            set { itemName = value; }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
            set
            {
                this.unitPrice = value;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                this.unit = value;
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
            }
        }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TotCost
        {
            get
            {
                return this.totCost;
            }
            set
            {
                this.totCost = value;
            }
        }
        /// <summary>
        /// 交易方式
        /// </summary>
        public FS.FrameWork.Models.NeuObject PayMode
        {
            get { return this.payMode; }
            set { payMode = value; }
        }
        /// <summary>
        /// 停车票号
        /// </summary>
        public string TicketNo
        {
            get
            {
                return this.ticketNo;
            }
            set
            {
                this.ticketNo = value;
            }
        }
        /// <summary>
        /// 旧的票据号
        /// </summary>
        public string OldInvoiceNo
        {
            get
            {
                return this.oldInvoiceNo;
            }
            set
            {
                this.oldInvoiceNo = value;
            }
        }  
        /// <summary>
        /// 作废时间
        /// </summary>
        public DateTime CancelDate
        {
            get { return this.cancelDate; }
            set { cancelDate = value; }
        }
        /// <summary>
        /// 退费的停车票号
        /// </summary>
        public string OldTicketNo
        {
            get
            {
                return this.oldTicketNo;
            }
            set
            {
                this.oldTicketNo = value;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get
            {
                return this.memo;
            }
            set
            {
                this.memo = value;
            }
        }
        /// <summary>
        ///  扩展字段1
        /// </summary>
        public string Flag1
        {
            get
            {
                return this.flag1;
            }
            set
            {
                this.flag1 = value;
            }
        }
        /// <summary>
        ///  扩展字段2
        /// </summary>
        public string Flag2
        {
            get
            {
                return this.flag2;
            }
            set
            {
                this.flag2 = value;
            }
        }


        /// <summary>
        ///  扩展字段3
        /// </summary>
        public string Flag3
        {
            get
            {
                return this.flag3;
            }
            set
            {
                this.flag3 = value;
            }
        }

        /// <summary>
        /// 发票状态 0无效 1有效
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
        /// <summary>
        /// 是否日结
        /// </summary>
        public bool IsBalance
        {
            get
            {
                return this.isBalance;
            }
            set
            {
                this.isBalance = value;
            }
        }
        /// <summary>
        /// 日结号
        /// </summary>
        public string BalanceNo
        {
            get
            {
                return this.balanceNo;
            }
            set
            {
                this.balanceNo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheck
        {
            get
            {
                return this.isCheck;
            }
            set
            {
                this.isCheck = value;
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
        /// 日结环境
        /// </summary>
        public OperEnvironment BalanceEnvironment
        {
            get
            {
                if (balanceEnvironment == null)
                {
                    balanceEnvironment = new OperEnvironment();
                }
                return this.balanceEnvironment;
            }
            set
            {
                this.balanceEnvironment = value;
            }
        }
        /// <summary>
        /// 审核环境
        /// </summary>
        public OperEnvironment CheckEnvironment
        {
            get
            {
                if (checkEnvironment == null)
                {
                    checkEnvironment = new OperEnvironment();
                }
                return this.checkEnvironment;
            }
            set
            {
                this.checkEnvironment = value;
            }
        }

      
        /// <summary>
        /// 院区相关{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }

        /// <summary>
        /// 院区相关{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }

        #endregion


        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new ParkingTicketFeeInfo Clone()
        {
            ParkingTicketFeeInfo ParkingTicketFeeInfo = base.Clone() as ParkingTicketFeeInfo;

            return ParkingTicketFeeInfo;
        }
        #endregion
    }
}
