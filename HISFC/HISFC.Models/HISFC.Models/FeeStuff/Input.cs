using System;

namespace FS.HISFC.Models.FeeStuff
{
	/// <summary>
	/// [��������: ���������Ϣ]
	/// [�� �� ��: ������]
	/// [����ʱ��: 2007-03]
	/// 
	/// ID ����¼��ˮ��
	/// </summary>
    [Serializable]
	public class Input : FS.FrameWork.Models.NeuObject
	{
		public Input()
		{
			this.storeBase.Class2Type = "0510";
		}


		#region ����

		/// <summary>
		/// ��ⵥ�ݺ� Ĭ��������+��ˮ��
		/// </summary>
		private string inListNO;

		/// <summary>
		/// �����
		/// </summary>
		private decimal inCost;

		/// <summary>
		/// ��ʽ�������
		/// </summary>
		private DateTime inFormalTime;

		/// <summary>
		/// ���װ�������
		/// </summary>
		private decimal packInQty;

		/// <summary>
		/// ��Ʊ��
		/// </summary>
		private string invoiceNO;

		/// <summary>
		/// ��Ʊ����
		/// </summary>
		private DateTime invoiceTime;

		/// <summary>
		/// ������Ŀ
		/// </summary>
		private string credit;

		/// <summary>
		/// ��������
		/// </summary>
		private DateTime produceTime;

		/// <summary>
		/// �Ƿ��������
		/// </summary>
		private bool isProduce;

		/// <summary>
		/// �ɹ�Ա
		/// </summary>
		private FS.FrameWork.Models.NeuObject buyer = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// �ƻ�����
		/// </summary>
		private string planListNO;

		/// <summary>
		/// �ƻ��������
		/// </summary>
		private int planSerialNO;

		/// <summary>
		/// ������ˮ��
		/// </summary>
		private string outNO;

		/// <summary>
		/// ��������Ϣ
		/// </summary>
        private FS.HISFC.Models.FeeStuff.StoreBase storeBase = new StoreBase();

		#endregion

		#region ����

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		public string InListNO
		{
			get
			{
				return this.inListNO;
			}
			set
			{
				this.inListNO = value;
			}
		}


		/// <summary>
		/// �����
		/// </summary>
		public decimal InCost
		{
			get
			{
				return this.inCost;
			}
			set
			{
				this.inCost = value;
			}
		}


		/// <summary>
		/// ��ʽ�������
		/// </summary>
		public DateTime InFormalTime
		{
			get
			{
				return this.inFormalTime;
			}
			set			
			{
				this.inFormalTime = value;
			}
		}


		/// <summary>
		/// ���װ�������
		/// </summary>
		public decimal PackInQty
		{
			get
			{
				return this.packInQty;
			}
			set
			{
				this.packInQty = value;
			}
		}


		/// <summary>
		/// ��Ʊ��
		/// </summary>
		public string InvoiceNO
		{
			get
			{
				return this.invoiceNO;
			}
			set			
			{
				this.invoiceNO = value;
			}
		}


		/// <summary>
		/// ��Ʊ����
		/// </summary>
		public DateTime InvoiceTime
		{
			get
			{
				return this.invoiceTime;
			}
			set
			{
				this.invoiceTime = value;
			}
		}


		/// <summary>
		/// ������Ŀ
		/// </summary>
		public string Credit
		{
			get
			{
				return this.credit;
			}
			set
			{
				this.credit = value;
			}
		}


		/// <summary>
		/// ��������
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
		/// �Ƿ��������
		/// </summary>
		public bool IsProduce
		{
			get
			{
				return this.isProduce;
			}
			set
			{
				this.isProduce = value;
			}
		}


		/// <summary>
		/// �ɹ�Ա
		/// </summary>
		public FS.FrameWork.Models.NeuObject Buyer
		{
			get
			{
				return this.buyer;
			}
			set
			{
				this.buyer = value;
			}
		}


		/// <summary>
		/// �ƻ�����
		/// </summary>
		public string PlanListNO
		{
			get
			{
				return this.planListNO;
			}
			set
			{
				this.planListNO = value;
			}
		}


		/// <summary>
		/// �ƻ��������
		/// </summary>
		public int PlanSerialNO
		{
			get
			{
				return this.planSerialNO;
			}
			set
			{
				this.planSerialNO = value;
			}
		}


		/// <summary>
		/// ������ˮ��
		/// </summary>
		public string OutNO
		{
			get
			{
				return this.outNO;
			}
			set
			{
				this.outNO = value;
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
		#endregion

		#region ����

		public new Input Clone()
		{
			Input input = base.Clone() as Input;

			input.buyer = this.buyer.Clone();

			input.storeBase = this.storeBase.Clone();

			return input;
		}

		#endregion
	}
}
