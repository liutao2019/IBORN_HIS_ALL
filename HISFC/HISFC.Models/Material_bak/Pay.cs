using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Material
{
    /// <summary>
    /// [��������: �����̸���ʵ��]
    /// [�� �� ��: Wangw]
    /// [����ʱ��: 2008-03]
    ///  ID �������
    ///  </summary>
    public class Pay : Neusoft.NFC.Object.NeuObject
    {
        public Pay()
        {
        }

        #region ����

        /// <summary>
        /// δ����ƾ֤
        /// </summary>
        private string unpayCredence;

        /// <summary>
        /// ����ƾ֤
        /// </summary>
        private string payCredence;

        /// <summary>
        /// δ����ƾ֤����
        /// </summary>
        private DateTime unpayCredenceTime;

        /// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
        private string inListCode;

        /// <summary>
        /// �������
        /// </summary>
        private DateTime inputDate;

        /// <summary>
        /// ��Ʊ��
        /// </summary>
        private string invoiceNo;

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        private DateTime invoiceTime;

        /// <summary>
        /// ���۽��
        /// </summary>
        private decimal retailCost;

        /// <summary>
        /// �������
        /// </summary>
        private decimal wholesaleCost;

        /// <summary>
        /// ������(��Ʊ���)
        /// </summary>
        private decimal purchaseCost;

        /// <summary>
        /// �Żݽ��
        /// </summary>
        private decimal discountCost;

        /// <summary>
        /// �˷�
        /// </summary>
        private decimal deliveryCost;

        /// <summary>
        /// ͬһ��Ʊ�ڵĸ������
        /// </summary>
        private int sequenceNo;

        /// <summary>
        /// �����־ 0δ����  1�Ѹ��� 2��ɸ���
        /// </summary>
        private string payState;

        /// <summary>
        /// �������� 0�ֽ�/1֧Ʊ
        /// </summary>
        private string payType;

        /// <summary>
        /// ������
        /// </summary>
        private decimal payCost;

        /// <summary>
        /// δ������
        /// </summary>
        private decimal unpayCost;

        /// <summary>
        /// ������Ϣ(��Ա ʱ��)
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment payOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// ������
        /// </summary>
        private Neusoft.NFC.Object.NeuObject stockDept = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ������˾
        /// </summary>
        private Neusoft.HISFC.Object.Material.MaterialCompany company = new MaterialCompany();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// δ����ƾ֤
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
        /// δ����ƾ֤����
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
        /// ����ƾ֤
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
        /// �������
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
        /// ��ⵥ�ݺ�
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
        /// ��Ʊ��
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
        /// ��Ʊ����
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
        /// ���۽��
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
        /// �������
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
        /// ������(��Ʊ���)
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
        /// �Żݽ��
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
        /// �˷�
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
        /// ͬһ��Ʊ�ڸ������
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
        /// �����־0δ����1�Ѹ���2�������
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
        /// ��������0�ֽ�1��Ʊ
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
        /// ������
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
        /// δ������
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
        /// ������Ϣ(��Ա ʱ��)
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment PayOper
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
        /// ������
        /// </summary>
        public Neusoft.NFC.Object.NeuObject StockDept
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
        /// ������˾
        /// </summary>
        public Neusoft.HISFC.Object.Material.MaterialCompany Company
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
        /// ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment Oper
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

        #endregion

        #region ����

        /// <summary>
        /// ���ǿ�¡����
        /// </summary>
        /// <returns>���ص�ǰʵ������</returns>
        public new Pay Clone()
        {
            Pay pay = base.Clone() as Pay;

            pay.PayOper = this.PayOper.Clone();

            pay.StockDept = this.StockDept.Clone();

            pay.Company = this.Company.Clone();

            pay.Oper = this.Oper.Clone();

            return pay;
        }

        #endregion
    }
}
