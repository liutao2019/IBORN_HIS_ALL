using System;
using System.Collections.Generic;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// DayReport<br></br>
    /// [��������: �Һ��ս���Ϣʵ��]<br></br>
    /// [�� �� ��: ��С��]<br></br>
    /// [����ʱ��: 2007-2-2]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class DayReport:FS.FrameWork.Models.NeuObject
	{
		public DayReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region ����
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endDate = DateTime.MinValue;
        /// <summary>
        /// �ս���ϸ�����ۼ���
        /// </summary>
        private int sumCount = 0;
        /// <summary>
        /// �Һŷ�����
        /// </summary>
        private decimal sumRegFee = 0m;
        /// <summary>
        /// ��������
        /// </summary>
        private decimal sumDigFee = 0m;
        /// <summary>
        /// ��������
        /// </summary>
        private decimal sumChkFee = 0m;
        /// <summary>
        /// ����������
        /// </summary>
        private decimal sumOthFee = 0m;
        /// <summary>
        /// �ֽ�����
        /// </summary>
        private decimal sumOwnCost = 0m;
        /// <summary>
        /// ��������
        /// </summary>
        private decimal sumPubCost = 0m;
        /// <summary>
        /// �Ը�����
        /// </summary>
        private decimal sumPayCost = 0m;
        /// <summary>
        /// �ս���ϸ
        /// </summary>
        private List<DayDetail> details = new List<DayDetail>();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new OperEnvironment();

        /// <summary>
        /// �ս�˲���
        /// </summary>
        private FS.HISFC.Models.Base.OperStat checker = new OperStat();

        /// <summary>
        /// ��������-������
        /// </summary>
        private decimal sumCardFee = 0m;

        /// <summary>
        /// ��������-������
        /// </summary>
        private decimal sumCaseFee = 0m;

        /// <summary>
        /// �����ܼ�- ������
        /// </summary>
        private decimal sumTotal = 0m;

        #endregion

        #region ����
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime BeginDate
        {
            get { return this.beginDate; }
            set { this.beginDate = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        /// <summary>
        /// �ս���ϸ�����ۼ���
        /// </summary>
        public int SumCount
        {
            get { return this.sumCount; }
            set { this.sumCount = value; }
        }

        /// <summary>
        /// �Һŷ�����
        /// </summary>
        public decimal SumRegFee
        {
            get { return this.sumRegFee; }
            set { this.sumRegFee = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal SumDigFee
        {
            get { return this.sumDigFee; }
            set { this.sumDigFee = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal SumChkFee
        {
            get { return this.sumChkFee; }
            set { this.sumChkFee = value; }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public decimal SumOthFee
        {
            get { return this.sumOthFee; }
            set { this.sumOthFee = value; }
        }

        /// <summary>
        /// �ֽ�����
        /// </summary>
        public decimal SumOwnCost
        {
            get { return this.sumOwnCost; }
            set { this.sumOwnCost = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal SumPubCost
        {
            get { return this.sumPubCost; }
            set { this.sumPubCost = value; }
        }

        /// <summary>
        /// �Ը�����
        /// </summary>
        public decimal SumPayCost
        {
            get { return this.sumPayCost; }
            set { this.sumPayCost = value; }
        }

        /// <summary>
        /// �ս���ϸ
        /// </summary>
        public List<DayDetail> Details
        {
            get { return this.details; }
            set { this.details = value; }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return this.oper; }
            set { this.oper = value; }
        }

        /// <summary>
        /// �ս�˲���
        /// </summary>
        public FS.HISFC.Models.Base.OperStat Checker
        {
            get { return this.checker; }
            set { this.checker = value; }
        }

        /// <summary>
        /// ��������-������
        /// </summary>
        public decimal SumCardFee
        {
            get
            {
                return this.sumCardFee;
            }
            set
            {
                this.sumCardFee = value;
            }
        }

        /// <summary>
        /// ��������-������
        /// </summary>
        public decimal SumCaseFee
        {
            get
            {
                return this.sumCaseFee;
            }
            set
            {
                this.sumCaseFee = value;
            }
        }

        /// <summary>
        /// �����ܼ�- ������
        /// </summary>
        public decimal SumTotal
        {
            get { return sumTotal; }
            set { sumTotal = value; }
        }
        

        #endregion

        #region ����
        /// <summary>
        ///clone
        /// </summary>
        /// <returns></returns>
        public new DayReport Clone()
        {
            DayReport dayReport = base.Clone() as DayReport;
                       
            dayReport.Checker = this.checker.Clone();
            dayReport.Oper = this.oper.Clone();

            return dayReport;
        }
        #endregion

    }

    /// <summary>
    /// �ս���ϸ
    /// </summary>
    public class DayDetail:FS.FrameWork.Models.NeuObject
    {
        public DayDetail() { }

        #region ����
        /// <summary>
        /// ��ϸ���
        /// </summary>
        private string orderNO = "";
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private string beginRecipeNo = "";
        /// <summary>
        /// ����������
        /// </summary>
        private string endRecipeNo = "";

        /// <summary>
        /// ��������
        /// </summary>
        private int count = 0;
        /// <summary>
        /// �Һŷ�����
        /// </summary>
        private decimal regFee = 0m;
        /// <summary>
        /// ��������
        /// </summary>
        private decimal digFee = 0m;
        /// <summary>
        /// ��������
        /// </summary>
        private decimal chkFee = 0m;
        /// <summary>
        /// ����������
        /// </summary>
        private decimal othFee = 0m;
        /// <summary>
        /// �ֽ�����
        /// </summary>
        private decimal ownCost = 0m;
        /// <summary>
        /// ��������
        /// </summary>
        private decimal pubCost = 0m;
        /// <summary>
        /// �Ը�����
        /// </summary>
        private decimal payCost = 0m;

        /// <summary>
        /// ״̬
        /// </summary>
        private EnumRegisterStatus status = EnumRegisterStatus.Valid;

        /// <summary>
        /// ��������-������
        /// </summary>
        private decimal cardFee = 0m;

        /// <summary>
        /// ��������-������
        /// </summary>
        private decimal caseFee = 0m;

        /// <summary>
        /// �Һ�ȫ������ - ������
        /// </summary>
        private decimal totalFee = 0m;
        #endregion

        #region ����
        /// <summary>
        /// ��ϸ���
        /// </summary>
        public string OrderNO
        {
            get { return this.orderNO; }
            set { this.orderNO = value; }
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public string BeginRecipeNo
        {
            get { return this.beginRecipeNo; }
            set { this.beginRecipeNo = value; }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public string EndRecipeNo
        {
            get { return this.endRecipeNo; }
            set { this.endRecipeNo = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        /// <summary>
        /// �Һŷ�����
        /// </summary>
        public decimal RegFee
        {
            get { return this.regFee; }
            set { this.regFee = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal DigFee
        {
            get { return this.digFee; }
            set { this.digFee = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal ChkFee
        {
            get { return this.chkFee; }
            set { this.chkFee = value; }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public decimal OthFee
        {
            get { return this.othFee; }
            set { this.othFee = value; }
        }

        /// <summary>
        /// �ֽ�����
        /// </summary>
        public decimal OwnCost
        {
            get { return this.ownCost; }
            set { this.ownCost = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal PubCost
        {
            get { return this.pubCost; }
            set { this.pubCost = value; }
        }

        /// <summary>
        /// �Ը�����
        /// </summary>
        public decimal PayCost
        {
            get { return this.payCost; }
            set { this.payCost = value; }
        }

        /// <summary>
        /// ״̬
        /// </summary>
        public EnumRegisterStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        /// <summary>
        /// ��������-������
        /// </summary>
        public decimal CardFee
        {
            get
            {
                return this.cardFee;
            }
            set
            {
                this.cardFee = value;
            }
        }

        /// <summary>
        /// ��������-������
        /// </summary>
        public decimal CaseFee
        {
            get
            {
                return this.caseFee;
            }
            set
            {
                this.caseFee = value;
            }
        }

        /// <summary>
        /// �Һ�ȫ������ - ������
        /// </summary>
        public decimal TotalFee
        {
            get { return totalFee; }
            set { totalFee = value; }
        }

        #endregion

        #region clone
        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public new DayDetail Clone()
        {
            return base.Clone() as DayDetail;
        }
        #endregion
    }
}

