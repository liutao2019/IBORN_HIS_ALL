using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: �ɹ��ƻ���]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-11]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	///  ID �ɹ��ƻ���ˮ��
	/// </summary>
    [Serializable]
    public class StockPlan : Base.PlanBase
	{
		public StockPlan() 
		{

		}

		#region ����

		/// <summary>
		/// �ɹ�����
		/// </summary>
		private System.String myPlanType ;

		/// <summary>
		/// ������˾
		/// </summary>
		private FS.FrameWork.Models.NeuObject myCompany = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// �ƻ������
		/// </summary>
		private System.Decimal myStockPrice ;		

		/// <summary>
		/// ������
		/// </summary>
		private System.Decimal myApproveQty;

		/// <summary>
		/// �����
		/// </summary>
		private System.Decimal myInQty;

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		private System.String myInListNO ;

		/// <summary>
		/// ������
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment approveOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// �����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment inOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �ƻ�����ˮ�� �ƻ����� �����ƻ�ʱ��' | ' �ָ�
        /// </summary>
        private string planNO;

		#endregion      


		/// <summary>
		/// ��ҩ��˾
		/// </summary>
		public FS.FrameWork.Models.NeuObject Company 
		{
			get
			{ 
				return this.myCompany; 
			}
			set
			{ 
				this.myCompany = value; 
			}
		}

        /// <summary>
        /// �ƻ������
        /// </summary>
        public System.Decimal StockPrice
        {
            get
            {
                return this.myStockPrice;
            }
            set
            {
                this.myStockPrice = value;
            }
        }
		
		/// <summary>
		/// �ɹ���������
		/// </summary>
		public System.Decimal StockApproveQty 
		{
			get
			{ 
				return this.myApproveQty;
			}
			set
			{
				this.myApproveQty = value;
			}
		}

		/// <summary>
		/// ��������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment ApproveOper
		{
			get
			{
				return this.approveOper;
			}
			set
			{
				this.approveOper = value;
			}
		}

		/// <summary>
		/// ʵ���������
		/// </summary>
		public System.Decimal InQty 
		{
			get
			{ 
				return this.myInQty; 
			}
			set
			{ 
				this.myInQty = value; 
			}
		}

		/// <summary>
		/// �����Ա��Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment InOper
		{
			get
			{
				return this.inOper;
			}
			set
			{
				this.inOper = value;
			}
		}

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		public System.String InListNO 
		{
			get
			{ 
				return this.myInListNO; 
			}
			set
			{ 
				this.myInListNO = value; 
			}
		}

        /// <summary>
        /// �ƻ�����ˮ�� �ƻ����� �����ƻ�ʱ��' | ' �ָ�
        /// </summary>
        public string PlanNO
        {
            get
            {
                return this.planNO;
            }
            set
            {
                this.planNO = value;
            }
        }

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���Ŀ�¡ʵ��</returns>
		public new StockPlan Clone()
		{
			StockPlan stockPlan = base.Clone() as StockPlan;

			stockPlan.Company = this.Dept.Clone();

			stockPlan.ApproveOper = this.ApproveOper.Clone();
			stockPlan.InOper = this.InOper.Clone();

			return stockPlan;
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// �ɹ�����
		/// </summary>
		private System.String myBillCode ;

		/// <summary>
		/// �ƻ��� 
		/// </summary>
		private System.String myPlanEmpl ;

		/// <summary>
		/// �ƻ���ǰ
		/// </summary>
		private System.DateTime myPlanDate ;

		/// <summary>
		/// �ɹ���
		/// </summary>
		private System.String myStockEmpl ;

		/// <summary>
		/// �ɹ�����
		/// </summary>
		private System.DateTime myStockDate ;

		/// <summary>
		/// �����
		/// </summary>
		private System.String myInEmpl ;

		/// <summary>
		/// �������
		/// </summary>
		private System.DateTime myInDate ;

		/// <summary>
		/// ������
		/// </summary>
		private System.String myApproveEmpl ;

		/// <summary>
		/// ��������
		/// </summary>
		private System.DateTime myApproveDate ;

		/// <summary>
		/// ������
		/// </summary>
		private System.String myOperCode ;

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private System.DateTime myOperDate ;

		/// <summary>
		/// �����ҿ����
		/// </summary>
		private System.Decimal myStoreNum ;

		/// <summary>
		/// ȫԺ�����
		/// </summary>
		private System.Decimal myStoreTotsum ;

		/// <summary>
		/// ȫԺ��������
		/// </summary>
		private System.Decimal myOutputSum ;

		/// <summary>
		/// �ƻ�������
		/// </summary>
		private System.Decimal myPlanNum ;

		/// <summary>
		/// ��������
		/// </summary>
		private System.Decimal myApproveNum ;

		/// <summary>
		/// �����
		/// </summary>
		private System.Decimal myInNum ;
		
		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		private System.String myInListCode ;

		/// <summary>
		/// �ɹ�����
		/// </summary>
		[System.Obsolete("�����ع� ����ΪBillNO����",true)]
		public System.String BillCode 
		{
			get
			{ 
				return this.myBillCode;
			}
			set
			{ 
				this.myBillCode = value; 
				this.ID = value;
			}
		}


		/// <summary>
		/// ����Ա
		/// </summary>
		[System.Obsolete("�����ع� ����ΪOper����",true)]
		public System.String OperCode 
		{
			get{ return this.myOperCode; }
			set{ this.myOperCode = value; }
		}


		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪOper����",true)]
		public System.DateTime OperDate 
		{
			get{ return this.myOperDate; }
			set{ this.myOperDate = value; }
		}


		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪInOper����",true)]
		public System.String InEmpl 
		{
			get{ return this.myInEmpl; }
			set{ this.myInEmpl = value; }
		}


		/// <summary>
		/// ���ʱ��
		/// </summary>
		[System.Obsolete("�����ع� ����ΪInOper����",true)]
		public System.DateTime InDate 
		{
			get{ return this.myInDate; }
			set{ this.myInDate = value; }
		}


		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪApproveOper����",true)]
		public System.String ApproveEmpl 
		{
			get{ return this.myApproveEmpl; }
			set{ this.myApproveEmpl = value; }
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�����ع� ����ΪApproveOper����",true)]
		public System.DateTime ApproveDate 
		{
			get{ return this.myApproveDate; }
			set{ this.myApproveDate = value; }
		}


		/// <summary>
		/// �ƻ���
		/// </summary>
		[System.Obsolete("�����ع� ����ΪPlanOper����",true)]
		public System.String PlanEmpl 
		{
			get
			{ 
				return this.myPlanEmpl; 
			}
			set
			{ 
				this.myPlanEmpl = value; 
			}
		}


		/// <summary>
		/// �ƻ�����
		/// </summary>
		[System.Obsolete("�����ع� ����ΪPlanOper����",true)]
		public System.DateTime PlanDate 
		{
			get{ return this.myPlanDate; }
			set{ this.myPlanDate = value; }
		}


		/// <summary>
		/// �ɹ���
		/// </summary>
		[System.Obsolete("�����ع� ����ΪStockOper����",true)]
		public System.String StockEmpl 
		{
			get{ return this.myStockEmpl; }
			set{ this.myStockEmpl = value; }
		}


		/// <summary>
		/// �ɹ�����
		/// </summary>
		[System.Obsolete("�����ع� ����ΪStockOper����",true)]
		public System.DateTime StockDate 
		{
			get{ return this.myStockDate; }
			set{ this.myStockDate = value; }
		}


		/// <summary>
		/// �����ҿ������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪStoreQty����",true)]
		public System.Decimal StoreNum 
		{
			get
			{ 
				return this.myStoreNum;
			}
			set
			{ 
				this.myStoreNum = value; 
			}
		}


		/// <summary>
		/// ȫԺ����ܺ�
		/// </summary>
		[System.Obsolete("�����ع� ����ΪStoreTotQty����",true)]
		public System.Decimal StoreTotsum 
		{
			get
			{ 
				return this.myStoreTotsum;
			}
			set
			{ 
				this.myStoreTotsum = value; 
			}
		}


		/// <summary>
		/// ȫԺ��������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪOutputQty����",true)]
		public System.Decimal OutputSum 
		{
			get
			{ 
				return this.myOutputSum;
			}
			set
			{ 
				this.myOutputSum = value;
			}
		}


		/// <summary>
		/// �ƻ������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪPlanQty����",true)]
		public System.Decimal PlanNum 
		{
			get
			{ 
				return this.myPlanNum; 
			}
			set
			{ 
				this.myPlanNum = value; 
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪApproveQty����",true)]
		public System.Decimal ApproveNum 
		{
			get{ return this.myApproveNum; }
			set{ this.myApproveNum = value; }
		}


		/// <summary>
		/// ʵ���������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪInQty����",true)]
		public System.Decimal InNum 
		{
			get{ return this.myInNum; }
			set{ this.myInNum = value; }
		}


		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		[System.Obsolete("�����ع� ����ΪInListNO����",true)]
		public System.String InListCode 
		{
			get
			{ 
				return this.myInListCode; 
			}
			set
			{ 
				this.myInListCode = value; 
			}
		}

        /// <summary>
        /// ��������
        /// </summary>
        [System.Obsolete("�����ع� ����ΪStockApproveQty����", true)]
        public System.Decimal ApproveQty
        {
            get
            {
                return this.myApproveQty;
            }
            set
            {
                this.myApproveQty = value;
            }
        }


		#endregion

	}
}
