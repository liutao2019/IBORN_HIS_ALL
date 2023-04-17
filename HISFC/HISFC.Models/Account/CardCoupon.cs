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

        #region ����

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        /// <summary>
        /// ����
        /// </summary>
        private string cardNo = string.Empty;

        /// <summary>
        /// ʣ�����
        /// </summary>
        private decimal couponVacancy = 0;

        /// <summary>
        /// �������ֶ��
        /// </summary>
        private decimal coupon = 0;


        /// <summary>
        /// �ۼƻ���
        /// </summary>
        private decimal couponAccumulate = 0;




        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private EnumValidState validState = EnumValidState.Valid;


        #endregion

        #region ����

        /// <summary>
        /// ���߻�����Ϣ
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
        /// ����
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        
        /// <summary>
        /// ʣ�����
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
        /// �������ֶ��
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
        /// �����ۼ�
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
        /// �Ƿ���Ч
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


        #region ����
        /// <summary>
        /// ��¡
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
