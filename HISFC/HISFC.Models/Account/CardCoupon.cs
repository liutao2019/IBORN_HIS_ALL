using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class CardCoupon : NeuObject, IValidState
    {

        #region 变量

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        /// <summary>
        /// 卡号
        /// </summary>
        private string cardNo = string.Empty;

        /// <summary>
        /// 剩余积分
        /// </summary>
        private decimal couponVacancy = 0;

        /// <summary>
        /// 增减积分额度
        /// </summary>
        private decimal coupon = 0;


        /// <summary>
        /// 累计积分
        /// </summary>
        private decimal couponAccumulate = 0;




        /// <summary>
        /// 是否有效
        /// </summary>
        private EnumValidState validState = EnumValidState.Valid;


        #endregion

        #region 属性

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
                patient = new FS.HISFC.Models.RADT.Patient();
                patient = value;
            }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        
        /// <summary>
        /// 剩余积分
        /// </summary>
        public decimal CouponVacancy
        {
            get
            {
                return this.couponVacancy;
            }
            set
            {
                this.couponVacancy = value;
            }
        }

        /// <summary>
        /// 增减积分额度
        /// </summary>
        public decimal Coupon
        {
            get
            {
                return this.coupon;
            }
            set
            {
                this.coupon = value;
            }
        }

        /// <summary>
        /// 积分累计
        /// </summary>
        public decimal CouponAccumulate
        {
            get
            {
                return this.couponAccumulate;
            }
            set
            {
                this.couponAccumulate = value;
            }
        }


        /// <summary>
        /// 是否有效
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


        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new CardCoupon Clone()
        {
            CardCoupon coupon = base.Clone() as CardCoupon;
            coupon.patient = this.Patient.Clone();

            return coupon;
        }
        #endregion
    }
}
