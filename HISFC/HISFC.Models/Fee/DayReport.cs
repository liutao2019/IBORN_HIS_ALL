using Neusoft.NFC.Object;
using Neusoft.HISFC.Object.Base;
using Neusoft.HISFC.Object.RADT;
using System;

namespace Neusoft.HISFC.Object.Fee
{
    /// <summary>
    /// Prepay<br></br>
    /// [��������: סԺ�ս���]<br></br>
    /// [�� �� ��: ���峬]<br></br>
    /// [����ʱ��: 2006-12-27]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
	public class DayReport:NeuObject
	{
		public DayReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }
        #region "����"

        /// <summary>
        /// ͳ�����
        /// </summary>
        private string statNO = "";

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endDate = DateTime.MinValue;
        /// <summary>
        /// ����Ա������Ϣ
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();
        /// <summary>
        /// Ԥ������
        /// </summary>
        private decimal prepayCost = 0m;
        /// <summary>
        /// �跽֧Ʊ���
        /// </summary>
        private decimal debitCheckCost = 0m;

        /// <summary>
        /// �跽���п����
        /// </summary>
        private decimal debitBankCost = 0m;
        /// <summary>
        /// ����Ԥ������
        /// </summary>
        private decimal balancePrepayCost = 0m;
        /// <summary>
        /// ����֧Ʊ���
        /// </summary>
        private decimal lenderCheckCost = 0m;
        /// <summary>
        /// �������п����
        /// </summary>
        private decimal lenderBankCost = 0m;
        /// <summary>
        /// ���Ѽ��ʽ��
        /// </summary>
        private decimal busaryPubCost = 0m;
        /// <summary>
        /// ��ҽ���ʻ�֧�����
        /// </summary>
        private decimal cityMedicarePayCost = 0m;
        /// <summary>
        /// ��ҽ��ͳ��֧�����
        /// </summary>
        private decimal cityMedicarePubCost = 0m;
        /// <summary>
        /// ʡҽ���ʻ�֧�����
        /// </summary>
        private decimal provinceMedicarePayCost = 0m;
        /// <summary>
        /// ʡҽ��ͳ��֧�����
        /// </summary>
        private decimal provinceMedicarePubCost = 0m;
        /// <summary>
        /// ����ֽ��Ͻɽ�
        /// </summary>
        private decimal turnInCash = 0m;
        /// <summary>
        /// Ԥ����Ʊ����
        /// </summary>
        private int prepayInvCount = 0;
        /// <summary>
        /// ���㷢Ʊ��Ʊ����
        /// </summary>
        private int balanceInvCount = 0;
        /// <summary>
        /// ����Ԥ����Ʊ����
        /// </summary>
        private int prepayWasteInvCount = 0;
        /// <summary>
        /// ���Ͻ��㷢Ʊ����
        /// </summary>
        private int balanceWasteInvCount = 0;
        /// <summary>
        /// Ԥ����Ʊ����
        /// </summary>
        private string prepayInvZone = "";
        /// <summary>
        /// ���㷢Ʊ����
        /// </summary>
        private string balanceInvZone = "";
        /// <summary>
        /// Ԥ��������Ʊ��
        /// </summary>
        private string prepayWasteInvNO = "";
        /// <summary>
        /// ��������Ʊ��
        /// </summary>
        private string balanceWasteInvNO = "";
        /// <summary>
        /// �����ܽ��
        /// </summary>
        private decimal balanceCost = 0m;
        #endregion

        #region "����"

        /// <summary>
        /// ͳ�����
        /// </summary>
        public string StatNO
        {
            get
            {
                return this.statNO;
               
            }
            set
            {
                statNO = value;
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return this.beginDate;
            }
            set
            {
                beginDate = value;
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }
        /// <summary>
        /// ����Ա������Ϣ
        /// </summary>
        public OperEnvironment Oper
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
        /// Ԥ������
        /// </summary>
        public decimal PrepayCost
        {
            get
            {
                return this.prepayCost;
            }
            set
            {
                this.prepayCost = value;
            }
        }
        /// <summary>
        /// �跽֧Ʊ���
        /// </summary>
        public decimal DebitCheckCost
        {
            get
            {
                return this.debitCheckCost;
            }
            set
            {
                this.debitCheckCost = value;
            }
        }

        /// <summary>
        /// �跽���п����
        /// </summary>
        public decimal DebitBankCost
        {
            get
            {
                return this.debitBankCost;
            }
            set
            {
                this.debitBankCost = value;
            }
        }

        /// <summary>
        /// ����Ԥ������
        /// </summary>
        public decimal BalancePrepayCost
        {
            get
            {
                return this.balancePrepayCost;
            }
            set
            {
                this.balancePrepayCost = value;
            }
        }
        /// <summary>
        /// ����֧Ʊ���
        /// </summary>
        public decimal LenderCheckCost
        {
            get
            {
                return this.lenderCheckCost;
            }
            set
            {
                this.lenderCheckCost = value;
            }
        }
        /// <summary>
        /// �������п����
        /// </summary>
        public decimal LenderBankCost
        {
            get
            {
                return this.lenderBankCost;
            }
            set
            {
                this.lenderBankCost = value;
            }
        }
        /// <summary>
        /// ���Ѽ��ʽ��
        /// </summary>
        public decimal BursaryPubCost
        {
            get
            {
                return this.busaryPubCost;
            }
            set
            {
                this.busaryPubCost = value;
            }
        }
        /// <summary>
        /// ��ҽ���ʻ�֧�����
        /// </summary>
        public decimal CityMedicarePayCost
        {
            get
            {
                return this.cityMedicarePayCost;
            }
            set
            {
                this.cityMedicarePayCost = value;
            }
        }
        /// <summary>
        /// ��ҽ��ͳ��֧�����
        /// </summary>
        public decimal CityMedicarePubCost
        {
            get
            {
                return this.cityMedicarePubCost;
            }
            set
            {
                this.cityMedicarePubCost = value;
            }
        }

        /// <summary>
        /// ʡҽ���ʻ�֧�����
        /// </summary>
        public decimal ProvinceMedicarePayCost
        {
            get
            {
                return this.provinceMedicarePayCost;
            }
            set
            {
                this.provinceMedicarePayCost = value;
            }
        }
        /// <summary>
        /// ʡҽ��ͳ��֧�����
        /// </summary>
        public decimal ProvinceMedicarePubCost
        {
            get
            {
                return this.provinceMedicarePubCost;
            }
            set
            {
                this.provinceMedicarePubCost = value;
            }
        }

        /// <summary>
        /// ����ֽ��Ͻɽ�
        /// </summary>
        public decimal TurnInCash
        {
            get
            {
                return this.turnInCash;
            }
            set
            {
                this.turnInCash = value;
            }
        }

        /// <summary>
        /// Ԥ����Ʊ����
        /// </summary>
        public int PrepayInvCount
        {
            get
            {
                return this.prepayInvCount;
            }
            set
            {
                this.prepayInvCount = value;
            }
        }
        /// <summary>
        /// ���㷢Ʊ��Ʊ����
        /// </summary>
        public int BalanceInvCount
        {
            get
            {
                return this.balanceInvCount;
            }
            set
            {
                this.balanceInvCount = value;
            }
        }
        /// <summary>
        /// ����Ԥ����Ʊ����
        /// </summary>
        public int PrepayWasteInvCount
        {
            get
            {
                return this.prepayWasteInvCount;
            }
            set
            {
                this.prepayWasteInvCount = value;
            }
        }
        /// <summary>
        /// ���Ͻ��㷢Ʊ����
        /// </summary>
        public int BalanceWasteInvCount
        {
            get
            {
                return this.balanceWasteInvCount;
            }
            set
            {
                this.balanceWasteInvCount = value;
            }
        }


        /// <summary>
        /// Ԥ����Ʊ����
        /// </summary>
        public string PrepayInvZone
        {
            get
            {
                return this.prepayInvZone;
            }
            set
            {
                this.prepayInvZone = value;
            }
        }
        /// <summary>
        /// ���㷢Ʊ����
        /// </summary>
        public string BalanceInvZone
        {
            get
            {
                return this.balanceInvZone;
            }
            set
            {
                this.balanceInvZone = value;
            }
        }

        /// <summary>
        /// Ԥ��������Ʊ��
        /// </summary>
        public string PrepayWasteInvNO
        {
            get
            {
                return this.prepayWasteInvNO;
            }
            set
            {
                this.prepayWasteInvNO = value;
            }
        }
        /// <summary>
        /// ��������Ʊ��
        /// </summary>
        public string BalanceWasteInvNO
        {
            get
            {
                return this.balanceWasteInvNO;
            }
            set
            {
                this.balanceWasteInvNO = value;
            }
        }
        /// <summary>
        /// �����ܽ��
        /// </summary>
        public decimal BalanceCost
        {
            get
            {
                return this.balanceCost;
            }
            set
            {
                this.balanceCost = value;
            }
        }
        #endregion

        #region "����"
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new DayReport Clone()
        {
            Neusoft.HISFC.Object.Fee.DayReport dayReport = base.Clone() as DayReport;
            dayReport.Oper = this.Oper.Clone();
            return dayReport;
        }

        #endregion

 

       
	

		

	}
}
