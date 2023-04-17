using System;

namespace FS.HISFC.Models.FeeStuff
{
	/// <summary>
	/// [��������: ���ʿ����ϸ��Ϣ]
	/// [�� �� ��: ������]
	/// [����ʱ��: 2007-03]
	/// 
	/// ID ������
	/// </summary>
    [Serializable]
	public class StoreDetail : FS.FrameWork.Models.NeuObject
	{
		public StoreDetail()
		{
			
		}


		#region ����
		
		/// <summary>
		/// ���۽��
		/// </summary>
		private decimal saleCost;
		
		/// <summary>
		/// ��������
		/// </summary>
		private DateTime outTime;

		/// <summary>
		/// �������
		/// </summary>
		private DateTime inTime;

		/// <summary>
		/// ��������
		/// </summary>
		private DateTime produceTime;

		/// <summary>
		/// �Ƿ�Ӽ�
		/// </summary>
		private bool isModifyPrice = false;

		/// <summary>
		/// �Ӽ���
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment modifyOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ����������ˮ��
		/// </summary>
		private string outSequence;

		/// <summary>
		/// �����������Ϣ
		/// </summary>
        private FS.HISFC.Models.FeeStuff.StoreBase storeBase = new StoreBase();

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
		/// ���һ�γ�������
		/// </summary>
		public DateTime OutTime
		{
			get
			{
				return this.outTime;
			}
			set
			{
				this.outTime = value;
			}
		}


		/// <summary>
		/// ���һ���������
		/// </summary>
		public DateTime InTime
		{
			get
			{
				return this.inTime;
			}
			set
			{
				this.inTime = value;
			}
		}


		/// <summary>
		/// ���һ����������
		/// </summary>
		public DateTime ProduceTime
		{
			get
			{
				return this.produceTime;
			}
			set
			{
				this.produceTime = value;
			}
		}


		/// <summary>
		/// �Ƿ�Ӽ�
		/// </summary>
		public bool IsModifyPrice
		{
			get
			{
				return this.isModifyPrice;
			}
			set
			{
				this.isModifyPrice = value;
			}
		}


		/// <summary>
		/// �Ӽ���
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment ModifyOper
		{
			get
			{
				return this.modifyOper;
			}
			set
			{
				this.modifyOper = value;
			}
		}


		/// <summary>
		/// ����������ˮ��
		/// </summary>
		public string OutSequence
		{
			get
			{
				return this.outSequence;
			}
			set
			{
				this.outSequence = value;
			}
		}


		/// <summary>
		/// �����������Ϣ
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

		#endregion

		#region ����

		public new StoreDetail Clone()
		{
			StoreDetail storeDetail = base.Clone() as StoreDetail;

			storeDetail.modifyOper = this.modifyOper.Clone();

			storeDetail.storeBase = this.storeBase.Clone();

			return storeDetail;
		}


		#endregion
	}
}
