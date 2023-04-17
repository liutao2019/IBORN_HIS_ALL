using System;

namespace FS.HISFC.Models.IMA
{
	/// <summary>
	/// [��������: ҩƷ�����ʿ�������� ���������� �������ɴ˼̳�]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// 
	/// </summary>
    [Serializable]
    public class IMAStoreBase : IMABase,Base.IValidState
	{
		public IMAStoreBase()
		{
			
		}


		#region ����

		/// <summary>
		/// �������
		/// </summary>
		private int serialNo;

		/// <summary>
		/// ������Ŀ�����
		/// </summary>
		private FS.FrameWork.Models.NeuObject targetDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��������
		/// </summary>
		private FS.FrameWork.Models.NeuObject producer = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ������˾
		/// </summary>
		private FS.FrameWork.Models.NeuObject company = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����
		/// </summary>
		private decimal quantity;

		/// <summary>
		/// �۸���Ϣ
		/// </summary>
		private FS.HISFC.Models.IMA.PriceService priceCollection = new FS.HISFC.Models.IMA.PriceService(); 

		/// <summary>
		/// ���۽��
		/// </summary>
		private decimal retailCost;

		/// <summary>
		/// �������
		/// </summary>
		private decimal wholeSaleCost;

		/// <summary>
		/// ������
		/// </summary>
		private decimal purchaseCost;

		/// <summary>
		/// �������(���ܺ�)
		/// </summary>
		private decimal storeQty;

		/// <summary>
		/// �����
		/// </summary>
		private decimal storeCost;

		/// <summary>
		/// ��λ��
		/// </summary>
		private string placeNo;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private FS.HISFC.Models.Base.EnumValidState validState = FS.HISFC.Models.Base.EnumValidState.Valid;

		#endregion
		
		/// <summary>
		/// �������
		/// </summary>
		public int SerialNO
		{
			get
			{
				return this.serialNo;
			}
			set
			{
				this.serialNo = value;
			}
		}

		/// <summary>
		/// ������Ŀ�����
		/// </summary>
		public FS.FrameWork.Models.NeuObject TargetDept
		{
			get
			{
				return this.targetDept;
			}
			set
			{
				this.targetDept = value;
			}
		}

		/// <summary>
		/// ��������
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
		/// ������˾
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
		/// ����
		/// </summary>
		public decimal Quantity
		{
			get
			{
				return this.quantity;
			}
			set
			{
				this.quantity = value;
			}
		}

		/// <summary>
		/// �۸���Ϣ
		/// </summary>
		public FS.HISFC.Models.IMA.PriceService PriceCollection
		{
			get
			{
				return this.priceCollection;
			}
			set
			{
				this.priceCollection = value;
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
		public decimal WholeSaleCost
		{
			get
			{
				return this.wholeSaleCost;
			}
			set
			{
				this.wholeSaleCost = value;
			}
		}

		/// <summary>
		/// ������
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
		/// ������� ��������
		/// </summary>
		public decimal StoreQty
		{
			get
			{
				return this.storeQty;
			}
			set
			{
				this.storeQty = value;
			}
		}

		/// <summary>
		/// ����� ���ܽ��
		/// </summary>
		public decimal StoreCost
		{
			get
			{
				return this.storeCost;
			}
			set
			{
				this.storeCost = value;
			}
		}

		/// <summary>
		/// ��λ��
		/// </summary>
		public string PlaceNO
		{
			get
			{
				return this.placeNo;
			}
			set
			{
				this.placeNo = value;
			}
		}

        #region IValidState ��Ա

        public FS.HISFC.Models.Base.EnumValidState ValidState
        {
            get
            {
                return this.validState;
            }
            set
            {
                this.validState = value;
            }
        }

        #endregion


		#region ����

		public new IMAStoreBase Clone()
		{
			IMAStoreBase imaStoreBase = base.Clone() as IMAStoreBase;

			imaStoreBase.TargetDept = this.TargetDept.Clone();
			imaStoreBase.Producer = this.Producer.Clone();
			imaStoreBase.Company = this.Company.Clone();
			imaStoreBase.PriceCollection = this.PriceCollection.Clone();

			return imaStoreBase;
		}


		#endregion        
    }
}
