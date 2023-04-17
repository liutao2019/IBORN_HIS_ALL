using System;

namespace Neusoft.HISFC.Object.Material
{
	/// <summary>
	/// [��������: ���ʹ���������]
	/// [�� �� ��: ������]
	/// [����ʱ��: 2007-03]
	/// 
	/// ID ������ˮ��
	/// </summary>
	public class Apply : Neusoft.HISFC.Object.IMA.IMABase
	{
		public Apply()
		{
			
		}


		#region ����

		/// <summary>
		/// ���뵥��
		/// </summary>
		private string applyListNO;

		/// <summary>
		/// �������
		/// </summary>
		private int serialNO;

		/// <summary>
		/// ����ʵ��
		/// </summary>
		private Neusoft.HISFC.Object.Material.MaterialItem item = new MaterialItem();

		/// <summary>
		/// ����۸�
		/// </summary>
		private decimal applyPrice;

		/// <summary>
		/// ������
		/// </summary>
		private decimal applyCost;

		/// <summary>
		/// ����۸�
		/// </summary>
		private decimal purchasePrice;

		/// <summary>
		/// ������
		/// </summary>
		private decimal purchaseCost;

		/// <summary>
		/// ������˾
		/// </summary>
		private Neusoft.NFC.Object.NeuObject company = new Neusoft.NFC.Object.NeuObject();

		/// <summary>
		/// Ŀ�굥λ
		/// </summary>
		private Neusoft.NFC.Object.NeuObject targeDept = new Neusoft.NFC.Object.NeuObject();

		/// <summary>
		/// �����ҿ��
		/// </summary>
		private decimal storeQty;

		/// <summary>
		/// ȫԺ���
		/// </summary>
		private decimal totStoreQty;

		/// <summary>
		/// ������
		/// </summary>
		private decimal outQty;

		/// <summary>
		/// ������(liuxq ����������)
		/// </summary>
		private decimal outCost;

		/// <summary>
		/// ��Ч��״̬
		/// </summary>
		private bool isValid = true;

        /// <summary>
        /// ���ⵥ��ˮ��
        /// </summary>
        private string outNo;

        /// <summary>
        /// ������
        /// </summary>
        private string stockNO;

		#endregion

		#region ����

		/// <summary>
		/// ���뵥��
		/// </summary>
		public string ApplyListNO
		{
			get
			{
				return this.applyListNO;
			}
			set
			{
				this.applyListNO = value;
			}
		}


		/// <summary>
		/// �������
		/// </summary>
		public int SerialNO
		{
			get
			{
				return this.serialNO;
			}
			set
			{
				this.serialNO = value;
			}
		}


		/// <summary>
		/// ������Ŀ
		/// </summary>
		public Neusoft.HISFC.Object.Material.MaterialItem Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}


		/// <summary>
		/// ����۸�
		/// </summary>
		public decimal ApplyPrice
		{
			get
			{
				return this.applyPrice;
			}
			set
			{
				this.applyPrice = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public decimal ApplyCost
		{
			get
			{
				return this.applyCost;
			}
			set
			{
				this.applyCost = value;
			}
		}


		/// <summary>
		/// ����۸�
		/// </summary>
		public decimal PurchasePrice
		{
			get
			{
				return this.purchasePrice;
			}
			set
			{
				this.purchasePrice = value;
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
		/// ������˾
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Company
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
		/// Ŀ�겿��
		/// </summary>
		public Neusoft.NFC.Object.NeuObject TargetDept
		{
			get
			{
				return this.targeDept;
			}
			set
			{
				this.targeDept = value;
			}
		}


		/// <summary>
		/// �����ҿ����
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
		/// ȫԺ�����
		/// </summary>
		public decimal TotStoreQty
		{
			get			
			{
				return this.totStoreQty;
			}
			set
			{
				this.totStoreQty = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public decimal OutQty
		{
			get
			{
				return this.outQty;
			}
			set
			{
				this.outQty = value;
			}
		}


		public decimal OutCost
		{
			get
			{
				return this.outCost;
			}
			set
			{
				this.outCost = value;
			}
		}

		/// <summary>
		/// ��Ч��״̬
		/// </summary>
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

        /// <summary>
        /// ���ⵥ��ˮ��
        /// </summary>
        public string OutNo
        {
            get { return outNo; }
            set { outNo = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string StockNO
        {
            get { return stockNO; }
            set { stockNO = value; }
        }

		#endregion

		#region ����

		public new Apply Clone()
		{
			Apply apply = base.Clone() as Apply;
			
			apply.item = this.item.Clone();

			apply.company = this.company.Clone();

			apply.targeDept = this.targeDept.Clone();

			return apply;
		}

		#endregion


	}
}
