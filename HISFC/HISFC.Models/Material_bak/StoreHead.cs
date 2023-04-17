using System;

namespace Neusoft.HISFC.Object.Material
{
	/// <summary>
	/// [��������: ���ʿ������Ϣ]
	/// [�� �� ��: ������]
	/// [����ʱ��: 2007-03]
	/// </summary>
	public class StoreHead : Neusoft.NFC.Object.NeuObject
	{
		public StoreHead()
		{
			
		}


		#region ����

		/// <summary>
		/// ���۽��
		/// </summary>
		private decimal saleCost;

		/// <summary>
		/// �����������
		/// </summary>
		private decimal topQty;

		/// <summary>
		/// �����������
		/// </summary>
		private decimal lowQty;

		/// <summary>
		/// �Ƿ�ȱ��
		/// </summary>
		private bool isLack = false;

		/// <summary>
		/// ��������Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.Material.StoreBase storeBase = new StoreBase();

		#endregion

		#region ����


		/// <summary>
		/// ���۽��
		/// </summary>
		public decimal SaleCost
		{
			get
			{
				return this.saleCost;
			}
			set
			{
				this.saleCost = value;
			}
		}


		/// <summary>
		/// �����������
		/// </summary>
		public decimal TopQty
		{
			get			
			{
				return this.topQty;
			}
			set
			{
				this.topQty = value;
			}
		}


		/// <summary>
		/// �����������
		/// </summary>
		public decimal LowQty
		{
			get
			{
				return this.lowQty;
			}
			set
			{
				this.lowQty = value;
			}
		}


		/// <summary>
		/// �Ƿ�ȱ��
		/// </summary>
		public bool IsLack
		{
			get
			{
				return this.isLack;
			}
			set
			{
				this.isLack = value;
			}
		}


		/// <summary>
		/// �����������Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.Material.StoreBase StoreBase
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


		#endregion
		
		#region ����

		public new StoreHead Clone()
		{
			StoreHead storeHead = base.Clone() as StoreHead;

			storeHead.storeBase = this.storeBase.Clone();

			return storeHead;
		}


		#endregion
	}
}
