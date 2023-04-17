using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    public class CashCoupon : NeuObject
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

        private decimal couponVacancy = 0;

        /// <summary>
        /// 积分
        /// </summary>
        public decimal CouponVacancy
        {
            get { return couponVacancy; }
            set { couponVacancy = value; }
        }

        private decimal couponAccumulate = 0;

        /// <summary>
        /// 累计积分
        /// </summary>
        public decimal CouponAccumulate
        {
            get { return couponAccumulate; }
            set { couponAccumulate = value; }
        }
    }
}
