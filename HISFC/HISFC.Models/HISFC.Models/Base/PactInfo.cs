namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// PactInfo<br></br>
	/// [��������: ��ͬ��λ��Ϣ������ҵ��ʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class PactInfo :  Pact,  ISort
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PactInfo( ) 
		{
			
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// �������
		/// </summary>
		private PayKind payKind = new PayKind();
		
		/// <summary>
		/// �۸���ʽ
		/// </summary>
		private string priceForm;
		
		/// <summary>
		/// ���ֱ���
		/// </summary>
		private FS.HISFC.Models.Base.FTRate rate = new FTRate();
		
		/// <summary>
		/// �Ƿ�Ҫ�������ҽ��֤��
		/// </summary>
		private bool isNeedMCard;
		
		/// <summary>
		/// �Ƿ��ܼ��
		/// </summary>
		private bool isInControl;
		
		/// <summary>
		/// ��Ŀ����� 0 ȫ��, 1 ҩƷ, 2 ��ҩƷ
		/// </summary>
		private string itemType = "";
		
		/// <summary>
		/// ���޶�
		/// </summary>
		private decimal dayQuota;
		
		/// <summary>
		/// ���޶�
		/// </summary>
		private decimal monthQuota;
		
		/// <summary>
		/// ���޶�
		/// </summary>
		private decimal yearQuota;
		
		/// <summary>
		/// һ���޶�
		/// </summary>
		private decimal onceQuota;
		
		/// <summary>
		/// ��λ��׼
		/// </summary>
		private decimal bedQuota;
		
		/// <summary>
		/// �յ���׼
		/// </summary>
		private decimal airConditionQuota;
		
		/// <summary>
		/// ��ͬ��λ���
		/// </summary>
		private string shortName;
		
		/// <summary>
		/// �������
		/// </summary>
		private int sortID;

        /// <summary>
        /// ��ͬ��λ����ҽ������dll����
        /// </summary>
        private string pactDllName = string.Empty;
        /// <summary>
        /// ��ͬ��λ����ҽ������dll����
        /// </summary>
        private string pactDllDescription = string.Empty;

		#endregion

		#region ����
		
		/// <summary>
		/// �������
		/// </summary>
		public PayKind PayKind
		{
			get
			{
				return this.payKind;
			}
			set
			{
				this.payKind = value;
			}
		}

		/// <summary>
		/// �۸���ʽ
		/// </summary>
		public string PriceForm
		{
			get
			{
				return this.priceForm;
			}
			set
			{
				this.priceForm = value;
			}
		}

		/// <summary>
		/// ���ֱ���
		/// </summary>
		public FS.HISFC.Models.Base.FTRate Rate
		{
			get
			{
				return this.rate;
			}
			set
			{
				this.rate = value;
			}
		}

		/// <summary>
		/// �Ƿ�Ҫ�������ҽ��֤��
		/// </summary>
		public bool IsNeedMCard
		{
			get
			{
				return this.isNeedMCard;
			}
			set
			{
				this.isNeedMCard = value;
			}
		}

		/// <summary>
		/// �Ƿ��ܼ��
		/// </summary>
		public bool IsInControl
		{
			get
			{
				return this.isInControl;
			}
			set
			{
				this.isInControl = value;
			}
		}

		/// <summary>
		/// ��Ŀ����� 0 ȫ��, 1 ҩƷ, 2 ��ҩƷ
		/// </summary>
		public string ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		/// <summary>
		/// ���޶�
		/// </summary>
		public decimal DayQuota
		{
			get
			{
				return this.dayQuota;
			}
			set
			{
				this.dayQuota = value;
			}
		}

		/// <summary>
		/// ���޶�
		/// </summary>
		public decimal MonthQuota
		{
			get
			{
				return this.monthQuota;
			}
			set
			{
				this.monthQuota = value;
			}
		}

		/// <summary>
		/// ���޶�
		/// </summary>
		public decimal YearQuota
		{
			get
			{
				return this.yearQuota;
			}
			set
			{
				this.yearQuota = value;
			}
		}

		/// <summary>
		/// һ���޶�
		/// </summary>
		public decimal OnceQuota
		{
			get
			{
				return this.onceQuota;
			}
			set
			{
				this.onceQuota = value;
			}
		}

		/// <summary>
		/// ��λ��׼
		/// </summary>
		public decimal BedQuota
		{
			get
			{
				return this.bedQuota;
			}
			set
			{
				this.bedQuota = value;
			}
		}

		/// <summary>
		/// �յ���׼
		/// </summary>
		public decimal AirConditionQuota
		{
			get
			{
				return this.airConditionQuota;
			}
			set
			{
				this.airConditionQuota = value;
			}
		}

		/// <summary>
		/// ��ͬ��λ���
		/// </summary>
		public string ShortName
		{
			get
			{
				return this.shortName;
			}
			set
			{
				this.shortName = value;
			}
		}

        /// <summary>
        /// ��ͬ��λ����ҽ������dll����
        /// </summary>
        public string PactDllName
        {
            get
            {
                return pactDllName;
            }
            set
            {
                pactDllName = value;
            }
        }
        /// <summary>
        /// ��ͬ��λ����ҽ������dll����
        /// </summary>
        public string PactDllDescription
        {
            get
            {
                return pactDllDescription;
            }
            set
            {
                pactDllDescription = value;
            }
        }
		#endregion

		#region ����
		
		#region �ͷ���Դ
		
		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing"></param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}

			if (this.payKind != null)
			{
				this.payKind.Dispose();
				this.payKind = null;
			}
			if (this.rate != null)
			{
				this.rate.Dispose();
				this.rate = null;
			}

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ����ʵ���ĸ���</returns>
		public new PactInfo Clone()
		{
			PactInfo pactInfo = base.Clone() as PactInfo;

			pactInfo.PayKind = this.PayKind.Clone();
			pactInfo.Rate = this.Rate.Clone();

			return pactInfo;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region ISort ��Ա
		/// <summary>
		/// �������
		/// </summary>
		public new int SortID
		{
			get
			{
				return this.sortID ;
			}
			set
			{
				this.sortID = value;
			}
		}
		#endregion

		#endregion
		
	}
}
