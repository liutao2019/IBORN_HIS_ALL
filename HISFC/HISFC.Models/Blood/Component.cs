using System;

namespace Neusoft.HISFC.Object.Blood
{


	/// <summary>
	/// ѪҺ�ɷ�ʵ��
	/// �̳�Neusoft.NFC.Object.NeuObject
	/// ʵ��Neusoft.HISFC.Object.Base.ISpellCode�ӿ�
	/// ID: �ɷֱ���
	/// NAME:�ɷ�����
	/// </summary>
	public class Component : Neusoft.HISFC.Object.Base.Spell {

		public Component()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private string seq; //���
		/// <summary>
		/// ���
		/// </summary>
		public string Seq
		{
			get
			{
				return seq;
			}
			set
			{
				seq = value;
			}
		}

		private bool isNeedMixed; //�Ƿ���Ҫ��Ѫ true ��Ҫ fase ����Ҫ
		/// <summary>
		/// �Ƿ���Ҫ��Ѫ true ��Ҫ fase ����Ҫ
		/// </summary>
		public bool IsNeedMixed
		{
			get
			{
				return isNeedMixed;
			}
			set
			{
				isNeedMixed = value;
			}
		}

		private int keepingDays; //��������
		/// <summary>
		/// ��������
		/// </summary>
		public int KeepingDays
		{
			get
			{
				return keepingDays;
			}
			set
			{
				keepingDays = value;
			}
		}

		private decimal keepingTemperature;//�����¶�
		/// <summary>
		/// �����¶�
		/// </summary>
		public decimal KeepingTemperature
		{
			get
			{
				return keepingTemperature;
			}
			set
			{
				keepingTemperature = value;
			}
		}

		private Neusoft.NFC.Object.NeuObject unit = new Neusoft.NFC.Object.NeuObject();//��λ// ID:��λ����  NAME:��λ����													
		/// <summary>
		/// ��λ
		/// ID:��λ����
		/// NAME:��λ����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Unit
		{
			get
			{
				return unit;
			}
			set
			{
				unit = value;
			}
		}

		private decimal minAmount;//��С�Ʒ�����
		/// <summary>
		/// ��С�Ʒ�����
		/// </summary>
		public decimal MinAmount
		{
			get
			{
				return minAmount;
			}
			set
			{
				minAmount = value;
			}
		}

		private decimal tradePrice;//�����
		/// <summary>
		/// �����
		/// </summary>
		public decimal TradePrice
		{
			get
			{
				return tradePrice;
			}
			set
			{
				tradePrice = value;
			}
		}

		private decimal salePrice;//���ۼ�
		/// <summary>
		/// ���ۼ�
		/// </summary>
		public decimal SalePrice
		{
			get
			{
				return salePrice;
			}
			set
			{
				salePrice = value;
			}
		}

		private int applyValidDays; //���뵥��Ч���� 0 Ϊһֱ��Ч
		/// <summary>
		/// ���뵥��Ч���� 0 Ϊһֱ��Ч
		/// </summary>
		public int ApplyValidDays
		{
			get
			{
				return applyValidDays;
			}
			set
			{
				applyValidDays = value;
			}
		}
		
		private bool isValid; //�Ƿ���Ч true��Ч false��Ч
		/// <summary>
		/// �Ƿ���Ч true��Ч false��Ч
		/// </summary>
		public bool IsValid
		{
			get
			{
				return isValid;
			}
			set
			{
				isValid = value;
			}
		}

		private Neusoft.NFC.Object.NeuObject operInfo = new Neusoft.NFC.Object.NeuObject(); //����Ա��Ϣ ID ��� Name ����
		/// <summary>
		/// ����Ա��Ϣ ID ��� Name ����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject OperInfo
		{
			get
			{
				return operInfo;
			}
			set
			{
				operInfo = value;
			}
		}

		private DateTime operDate; //��������
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime OperDate
		{
			get
			{
				return operDate;
			}
			set
			{
				operDate = value;
			}
		}

		#region Clone����
		/// <summary>
		/// Cloneʵ�屾��
		/// </summary>
		/// <returns></returns>
		public new Component Clone()
		{
			Component clone = base.Clone() as Component;
			
			clone.unit = this.unit.Clone();
			clone.operInfo = this.operInfo.Clone();
			
			return clone;
		}
		#endregion

	}
}
