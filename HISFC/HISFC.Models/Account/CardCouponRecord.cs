using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class CardCouponRecord : NeuObject, IValidState
    {

        #region ����

        /// <summary>
        /// ʹ�øÿ���Ļ��߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        /// <summary>
        /// ����
        /// </summary>
        private string cardNo = string.Empty;

        /// <summary>
        /// ���ѽ��
        /// </summary>
        private decimal money = 0;


        /// <summary>
        /// �������ֶ��
        /// </summary>
        private decimal coupon = 0;


        /// <summary>
        /// ������ʣ�����
        /// </summary>
        private decimal couponVacancy = 0;


        /// <summary>
        /// ��Ӧ���ѷ�Ʊ��
        /// </summary>
        private string invoiceNo = string.Empty;


        /// <summary>
        /// ������ʽ��1���ѻ�û��֣�2�һ�����
        /// </summary>
        private string operType = string.Empty;

        /// <summary>
        /// �һ���Ʒ
        /// </summary>
        private string exchangeGoods = string.Empty;


        /// <summary>
        /// ��������
        /// </summary>				
        private OperEnvironment operEnvironment;


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
        /// ���ѽ��
        /// </summary>
        public decimal Money
        {
            get
            {
                return this.money;
            }
            set
            {
                this.money = value;
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
        /// ��Ӧ���ѵķ�Ʊ��
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        /// <summary>
        /// ������ʽ��1���ѻ�û��֣�2�һ�����
        /// </summary>
        public string OperType
        {
            get { return this.operType; }
            set { operType = value; }
        }


        /// <summary>
        /// �һ���Ʒ
        /// </summary>
        public string ExchangeGoods
        {
            get { return this.exchangeGoods; }
            set { exchangeGoods = value; }
        }


        /// <summary>
        /// ��������
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
        public new CardCouponRecord Clone()
        {
            CardCouponRecord couponRecord = base.Clone() as CardCouponRecord;
            couponRecord.patient = this.Patient.Clone();

            return couponRecord;
        }
        #endregion
    }
}
