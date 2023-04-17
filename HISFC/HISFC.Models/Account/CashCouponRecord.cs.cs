using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    public class CashCouponRecord : NeuObject
    {
        private string cardNo = string.Empty;

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        private decimal coupon = 0;

        /// <summary>
        /// 本次积分
        /// </summary>
        public decimal Coupon
        {
            get { return coupon; }
            set { coupon = value; }
        }

        private decimal couponVacancy = 0;

        /// <summary>
        /// 本次积分余额
        /// </summary>
        public decimal CouponVacancy
        {
            get { return couponVacancy; }
            set { couponVacancy = value; }
        }

        private string invoiceNo = string.Empty;

        /// <summary>
        /// 对应消费发票号
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        private string couponType = string.Empty;

        /// <summary>
        /// 积分来源
        /// </summary>
        public string CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }
				
        private OperEnvironment operEnvironment;

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
                return operEnvironment; 
            }
            set { operEnvironment = value; }
        }

    }
}
