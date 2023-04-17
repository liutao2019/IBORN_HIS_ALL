using System;

namespace FS.HISFC.Models.FeeStuff
{
	/// <summary>
	/// [��������: ���ʲɹ��ƻ���]
	/// [�� �� ��: �]
	/// [����ʱ��: 2007-04]
	/// </summary>
    [Serializable]
	public class InputPlan: FS.FrameWork.Models.NeuObject
	{
		public InputPlan() 
		{
			this.storeBase.Class2Type = "0510";
		}

		
		#region  ����
		/// <summary>
		/// �ƻ�����
		/// </summary>
		private System.String storageCode = string.Empty ;
		/// <summary>
		/// �ƻ�����
		/// </summary>
		private System.String planListCode = string.Empty;
		/// <summary>
		/// �ƻ��������
		/// </summary>
		private System.Int32 planNo ;
		/// <summary>
		/// ״̬
		/// </summary>
		private System.String state = string.Empty ;
		/// <summary>
		/// �ƻ�����
		/// </summary>
		private System.String planType = string.Empty ;		
		/// <summary>
		/// ������˱�־
		/// </summary>
		private System.String financeFlag = string.Empty ;
		/// <summary>
		/// �ƻ�����
		/// </summary>
		private decimal planNum ;
		/// <summary>
		/// �ƻ��۸�
		/// </summary>
		private decimal planPrice ;
		/// <summary>
		/// �ƻ����
		/// </summary>
		private decimal planCost ;		
		/// <summary>
		/// �ɹ�����
		/// </summary>
		private decimal stockNum ;		
		/// <summary>
		/// �ɹ�Ա
		/// </summary>
		private FS.FrameWork.Models.NeuObject stockOper = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// �ɹ�ʱ��
		/// </summary>
		private System.DateTime stockTime;		
		/// <summary>
		/// ������˾
		/// </summary>
		private FS.FrameWork.Models.NeuObject company = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ��Ʊ��liuxq add
		/// </summary>
		private System.String invoiceNo;
		/// <summary>
		/// ��������
		/// </summary>
		private FS.FrameWork.Models.NeuObject producer = new FS.FrameWork.Models.NeuObject();			
		/// <summary>
		/// ��������
		/// </summary>
		private System.Int32 deptType ;
		/// <summary>
		/// �����ҿ��
		/// </summary>
		private decimal storeSum ;
		/// <summary>
		/// ȫԺ���
		/// </summary>
		private decimal storeTotsum ;
		/// <summary>
		/// ȫԺ��������
		/// </summary>
		private decimal outputSum ;		
		/// <summary>
		/// ��������Ϣ
		/// </summary>
        private FS.HISFC.Models.FeeStuff.StoreBase storeBase = new StoreBase();
        /// <summary>
        /// ��չ�ֶ�1{2703362B-7819-4c82-89DA-BACB65784CAD}
        /// </summary>
        private string extend1 = "";
        /// <summary>
        /// ��չ�ֶ�2
        /// </summary>
        private string extend2 = "";
        
        /// <summary>
        /// ��չ�ֶ�3
        /// </summary>
        private string extend3 = "";
		#endregion

		#region ����
		/// <summary>
		/// �ƻ�����
		/// </summary>
		public System.String StorageCode
		{
			get
			{ 
				return this.storageCode; 
			}
			set
			{ 
				this.storageCode = value; 
			}
		}


		/// <summary>
		/// �ƻ�����
		/// </summary>
		public System.String PlanListCode
		{
			get
			{ 
				return this.planListCode;
			}
			set
			{ 
				this.planListCode = value;
			}
		}


		/// <summary>
		/// �ƻ��������
		/// </summary>
		public System.Int32 PlanNo
		{
			get
			{ 
				return this.planNo;
			}
			set
			{ 
				this.planNo = value;
			}
		}


		/// <summary>
		/// �ƻ���״̬(0�ƻ�����1�ɹ�����2��ˣ�3�����) 4 ���ϼƻ���
		/// </summary>
		public System.String State
		{
			get
			{
				return this.state; 
			}
			set
			{ 
				this.state = value;
			}
		}


		/// <summary>
		/// �ƻ�����0�ֹ��ƻ���1�����ߣ�2���ģ�3ʱ�䣬4������
		/// </summary>
		public System.String PlanType
		{
			get
			{
				return this.planType;
			}
			set
			{ 
				this.planType = value;
			}
		}


		/// <summary>
		/// �����շ���Ʒ��־(0����,1����)
		/// </summary>
		public System.String FinanceFlag
		{
			get
			{ 
				return this.financeFlag; 
			}
			set
			{ 
				this.financeFlag = value; 
			}
		}


		/// <summary>
		/// �ƻ�����
		/// </summary>
		public decimal PlanNum
		{
			get
			{ 
				return this.planNum;
			}
			set
			{ 
				this.planNum = value;
			}
		}


		/// <summary>
		/// �ƻ��۸�
		/// </summary>
		public decimal PlanPrice
		{
			get
			{ 
				return this.planPrice;
			}
			set
			{ 
				this.planPrice = value;
			}
		}


		/// <summary>
		/// �ƻ����
		/// </summary>
		public decimal PlanCost
		{
			get
			{ 
				return this.planCost; 
			}
			set
			{ 
				this.planCost = value;
			}
		}

		
		/// <summary>
		/// �ɹ�����
		/// </summary>
		public decimal StockNum
		{
			get
			{ 
				return this.stockNum; 
			}
			set
			{ 
				this.stockNum = value;
			}
		}


		/// <summary>
		/// �ɹ���Ϣ
		/// </summary>
		public FS.FrameWork.Models.NeuObject  StockOper
		{
			get
			{
				return this.stockOper;
			}
			set
			{ 
				this.stockOper = value; 
			}
		}


		public System.DateTime StockTime 
		{
			get
			{
				return this.stockTime;
			}
			set
			{
				this.stockTime = value;
			}
		}

		
		/// <summary>
		/// ������˾��Ϣ
		/// </summary>
		public FS.FrameWork.Models.NeuObject Company
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
		/// ��Ʊ��liuxq add 
		/// </summary>
		public System.String InvoiceNo
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
		/// �������ұ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject Producer
		{
			get
			{ 
				return this.producer;
			}
			set
			{ 
				this.producer = value; 
			}
		}


		/// <summary>
		/// 1�ֿⲿ��   0 ����
		/// </summary>
		public System.Int32 DeptType
		{
			get
			{ 
				return this.deptType;
			}
			set
			{
				this.deptType = value; 
			}
		}


		/// <summary>
		/// �����ҿ��
		/// </summary>
		public decimal StoreSum
		{
			get
			{ 
				return this.storeSum;
			}
			set
			{ 
				this.storeSum = value;
			}
		}


		/// <summary>
		/// ȫԺ����ܺ�
		/// </summary>
		public decimal StoreTotsum
		{
			get
			{ 
				return this.storeTotsum; 
			}
			set
			{ 
				this.storeTotsum = value; 
			}
		}


		/// <summary>
		/// ȫԺ��������
		/// </summary>
		public decimal OutputSum
		{
			get
			{ 
				return this.outputSum; 
			}
			set
			{ 
				this.outputSum = value; 
			}
		}


		/// <summary>
		/// ��������Ϣ
		/// </summary>
        public FS.HISFC.Models.FeeStuff.StoreBase StoreBase
		{
			get
			{
				return this.storeBase;
			}
			set
			{
				this.storeBase = value;
			}
		}

        /// <summary>
        /// ��չ�ֶ�1{2703362B-7819-4c82-89DA-BACB65784CAD}
        /// </summary>
        public string Extend1
        {
            get
            {
                return extend1;
            }
            set
            {
                extend1 = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�2
        /// </summary>
        public string Extend2
        {
            get
            {
                return extend2;
            }
            set
            {
                extend2 = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�3
        /// </summary>
        public string Extend3
        {
            get
            {
                return extend3;
            }
            set
            {
                extend3 = value;
            }
        }
		#endregion 

		#region ����

		public new InputPlan Clone()
		{
			InputPlan inputPlan = base.Clone() as InputPlan;

			inputPlan.company = this.company.Clone();

			inputPlan.producer = this.producer.Clone();			

			inputPlan.stockOper = this.stockOper.Clone();
			
			inputPlan.storeBase = this.storeBase.Clone();

			return inputPlan;
		}

		#endregion


	}
}
