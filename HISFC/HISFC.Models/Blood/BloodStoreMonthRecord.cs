using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.Blood
{
    /// <summary>
    /// [��������: �½����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-4-19]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    public class BloodStoreMonthRecord : Spell, ISort, IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public BloodStoreMonthRecord()
        {
        }

        #region �ֶ�

        /// <summary>
        /// ISort
        /// </summary>
        private int iSort;

        /// <summary>
        /// IValid
        /// </summary>
        private bool iValid;

        /// <summary>
        ///  ����Ա������ʱ��
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment bloodStoreMonthOperator = new OperEnvironment();

        /// <summary>
        ///  ѪҺ�ɷ֣�Ѫ��
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodComponents bloodStoreMonthType = new BloodComponents();

        /// <summary>
        /// ��λ
        /// </summary>
        private string stockUnit;

        /// <summary>
        /// �½����ʱ��
        /// </summary>
        private DateTime bloodToDate;

        /// <summary>
        /// ������ĩ�������
        /// </summary>
        private decimal bloodLastMonthNum;

        /// <summary>
        /// ������⹺������
        /// </summary>
        private decimal bloodThisMonthNum;

        /// <summary>
        /// ������ĩ��湺������
        /// </summary>
        private decimal bloodThisLastMonthNum;

        /// <summary>
        /// ������ĩ�����С��װ��������
        /// </summary>
        private decimal bloodLastMinPrice;

        /// <summary>
        /// ������⹺�뵥��
        /// </summary>
        private decimal bloodThisSinglePrice;
 
        /// <summary>
        /// ������ĩ��湺�뵥��
        /// </summary>
        private decimal bloodThisLastSinglePrice;

        /// <summary>
        /// ������ĩ��湺����
        /// </summary>
        private decimal bloodLastBuyPrice;

        /// <summary>
        /// ������⹺����
        /// </summary>
        private decimal bloodThisBuyPrice;

        /// <summary>
        /// ������ĩ��湺����
        /// </summary>
        private decimal bloodThisLastBuyPrice;

        /// <summary>
        /// ������ĩ������۽��
        /// </summary>
        private decimal bloodLastSalePrice;

        /// <summary>
        /// ����������۽��
        /// </summary>
        private decimal bloodThisSalePrice;

        /// <summary>
        /// ������ĩ������۽��
        /// </summary>
        private decimal bloodThisLastSalePrice;

        #endregion

        #region ����
        /// <summary>
        ///  ����Ա������ʱ��
        /// </summary>

        public Neusoft.HISFC.Object.Base.OperEnvironment BloodStoreMonthOperator
        {
            get { return bloodStoreMonthOperator; }
            set { bloodStoreMonthOperator = value; }
        }

        /// <summary>
        ///  ѪҺ�ɷ֣�Ѫ��
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodComponents BloodStoreMonthType
        {
            get { return bloodStoreMonthType; }
            set { bloodStoreMonthType = value; }
        }

        /// <summary>
        /// ��λ
        /// </summary>
        public string StockUnit
        {
            get { return stockUnit; }
            set { stockUnit = value; }
        }

        /// <summary>
        /// �½����ʱ��
        /// </summary>
        public DateTime BloodToDate
        {
            get { return bloodToDate; }
            set { bloodToDate = value; }
        }

        /// <summary>
        /// ������ĩ�������
        /// </summary>
        public decimal BloodLastMonthNum
        {
            get { return bloodLastMonthNum; }
            set { bloodLastMonthNum = value; }
        }

        /// <summary>
        /// ������⹺������
        /// </summary>
        public decimal BloodThisMonthNum
        {
            get { return bloodThisMonthNum; }
            set { bloodThisMonthNum = value; }
        }

        /// <summary>
        /// ������ĩ��湺������
        /// </summary>
        public decimal BloodThisLastMonthNum
        {
            get { return bloodThisLastMonthNum; }
            set { bloodThisLastMonthNum = value; }
        }

        /// <summary>
        /// ������ĩ�����С��װ��������
        /// </summary>
        public decimal BloodLastMinPrice
        {
            get { return bloodLastMinPrice; }
            set { bloodLastMinPrice = value; }
        }

        /// <summary>
        /// ������⹺�뵥��
        /// </summary>
        public decimal BloodThisSinglePrice
        {
            get { return bloodThisSinglePrice; }
            set { bloodThisSinglePrice = value; }
        }

        /// <summary>
        /// ������ĩ��湺�뵥��
        /// </summary>
        public decimal BloodThisLastSinglePrice
        {
            get { return bloodThisLastSinglePrice; }
            set { bloodThisLastSinglePrice = value; }
        }

        /// <summary>
        /// ������ĩ��湺����
        /// </summary>
        public decimal BloodLastBuyPrice
        {
            get { return bloodLastBuyPrice; }
            set { bloodLastBuyPrice = value; }
        }

        /// <summary>
        /// ������⹺����
        /// </summary>
        public decimal BloodThisBuyPrice
        {
            get { return bloodThisBuyPrice; }
            set { bloodThisBuyPrice = value; }
        }

        /// <summary>
        /// ������ĩ��湺����
        /// </summary>
        public decimal BloodThisLastBuyPrice
        {
            get { return bloodThisLastBuyPrice; }
            set { bloodThisLastBuyPrice = value; }
        }

        /// <summary>
        /// ������ĩ������۽��
        /// </summary>
        public decimal BloodLastSalePrice
        {
            get { return bloodLastSalePrice; }
            set { bloodLastSalePrice = value; }
        }

        /// <summary>
        /// ����������۽��
        /// </summary>
        public decimal BloodThisSalePrice
        {
            get { return bloodThisSalePrice; }
            set { bloodThisSalePrice = value; }
        }

        /// <summary>
        /// ������ĩ������۽��
        /// </summary>
        public decimal BloodThisLastSalePrice
        {
            get { return bloodThisLastSalePrice; }
            set { bloodThisLastSalePrice = value; }
        }

        #endregion

        #region ISort ��Ա

        public int SortID
        {
            get
            {
                return iSort;
            }
            set
            {
                this.iSort = value;
            }
        }

        #endregion

        #region IValid ��Ա

        public bool IsValid
        {
            get
            {
                return iValid;
            }
            set
            {
                this.iValid = value;
            }
        }

        #endregion

        #region ��¡
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new BloodStoreMonthRecord Clone()
        {
            BloodStoreMonthRecord bloodStoreMonthRecord = base.Clone() as BloodStoreMonthRecord;

            bloodStoreMonthRecord.BloodStoreMonthOperator = this.BloodStoreMonthOperator.Clone();
            bloodStoreMonthRecord.BloodStoreMonthType = this.BloodStoreMonthType.Clone();

            return bloodStoreMonthRecord;
        }
        #endregion
    }
}
