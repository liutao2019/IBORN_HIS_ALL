using System;

namespace Neusoft.HISFC.Object.Material
{
	/// <summary>
	/// [��������: ���ʿ�������Ϣ����]
	/// [�� �� ��: ������]
	/// [����ʱ��: 2007-03]
	/// </summary>
	public class StoreBase : Neusoft.HISFC.Object.IMA.IMAStoreBase
	{
		public StoreBase()
		{

		}


		#region ����

		/// <summary>
		/// ������Ŀ��Ϣ
		/// </summary>
		private MaterialItem item = new MaterialItem();

		/// <summary>
		/// ����
		/// </summary>
		private string batchNO;

		/// <summary>
		/// ������(���κ�)
		/// </summary>
		private string stockNO;

		/// <summary>
		/// ������� Ĭ�ϲֿ�� 1 �ֿ� 0 ����
		/// </summary>
		private string stockType = "1";

		/// <summary>
		/// stockTypeΪ"0"ʱ �����Ƿ�Ϊ����
		/// </summary>
		private bool isNurse = false;

		/// <summary>
		/// ƽ������
		/// </summary>
		private decimal avgPrice;		

		/// <summary>
		/// ƽ�����ۼ�
		/// </summary>
		private decimal avgSalePrice;

		/// <summary>
		/// ��Ч��
		/// </summary>
		private DateTime validTime;

		/// <summary>
		/// ������
		/// </summary>
		private string barNO;

		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		private string extend;

        /// <summary>
        /// ��������
        /// </summary>
        private decimal returns;

		#endregion

		#region ����

		/// <summary>
		/// ������Ŀ��Ϣ
		/// </summary>
		public MaterialItem Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;

				base.IMAItem = value;				
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		public string BatchNO
		{
			get
			{
				return this.batchNO;
			}
			set
			{
				this.batchNO = value;
			}
		}


		/// <summary>
		/// ������(���κ�)
		/// </summary>
		public string StockNO
		{
			get
			{
				return this.stockNO;
			}
			set
			{
				this.stockNO = value;
			}
		}


		/// <summary>
		/// ������� Ĭ�ϲֿ��
		/// </summary>
		public string StockType
		{
			get
			{
				return this.stockType;
			}
			set
			{
				this.stockType = value;
			}
		}


		/// <summary>
		/// stockTypeΪ"0"ʱ �����Ƿ�Ϊ����
		/// </summary>
		public bool IsNurse
		{
			get
			{
				return this.isNurse;
			}
			set
			{
				this.isNurse = value;
			}
		}


		/// <summary>
		/// ƽ������
		/// </summary>
		public decimal AvgPrice
		{
			get
			{
				return this.avgPrice;
			}
			set
			{
				this.avgPrice = value;
			}
		}


		/// <summary>
		/// ƽ�����ۼ�
		/// </summary>
		public decimal AvgSalePrice
		{
			get
			{
				return this.avgSalePrice;
			}
			set
			{
				this.avgSalePrice = value;
			}
		}


		/// <summary>
		/// ��Ч��
		/// </summary>
		public DateTime ValidTime
		{
			get
			{
				return this.validTime;
			}
			set
			{
				this.validTime = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public string BarNO
		{
			get
			{
				return this.barNO;
			}
			set
			{
				this.barNO = value;
			}
		}


		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		public string Extend
		{
			get
			{
				return this.extend;
			}
			set
			{
				this.extend = value;
			}
		}


        public decimal Returns
        {
            get
            {
                return this.returns;
            }
            set
            {
                this.returns = value;
            }
        }


		#endregion

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new StoreBase Clone()
		{
			StoreBase storeBase = base.Clone() as StoreBase;

			storeBase.item = this.item.Clone();

			return storeBase;
		}


		#endregion
	}
}
