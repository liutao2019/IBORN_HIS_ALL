using System;

namespace neusoft.HISFC.Object.InterfaceSi
{
	/// <summary>
	/// Compare ��ժҪ˵����
	/// </summary>
	public class Compare : neusoft.neuFC.Object.neuObject 
	{
		public Compare()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//ҽ��������Ϣ
		private Item centerItem = new Item();
		//������Ŀ����
		private string hisCode;
		//������Ŀ������Ϣ
		private Base.SpellCode spellCode  = new neusoft.HISFC.Object.Base.SpellCode(); 
		//������Ŀ���
		private string specs;
		//������Ŀ�۸�
		private Decimal price;
		//������Ŀ����
		private string doseCode;

		private string regularName;

		public string RegularName
		{
			set
			{
				regularName = value;
			}
			get
			{
				return regularName;
			}
		}

		/// <summary>
		/// ҽ��������Ϣ
		/// </summary>
		public Item CenterItem
		{
			get
			{
				return centerItem;
			}
			set
			{
				centerItem = value;
			}
		}
		/// <summary>
		/// ������Ŀ����
		/// </summary>
		public string HisCode
		{
			get
			{
				return hisCode;
			}
			set
			{
				hisCode = value;
			}
		}
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public Base.SpellCode SpellCode
		{
			get
			{
				return spellCode;
			}
			set
			{
				spellCode = value;
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public string Specs
		{
			get
			{
				return specs;
			}
			set
			{
				specs = value;
			}
		}
		/// <summary>
		/// �Ը�����
		/// </summary>
		public Decimal Price
		{
			get
			{
				return price;
			}
			set
			{
				price = value;
			}
		}
		/// <summary>
		/// ���ͱ���
		/// </summary>
		public string DoseCode
		{
			get
			{
				return doseCode;
			}
			set
			{
				doseCode = value;
			}
		}

		public new Compare Clone()
		{
			Compare obj = base.Clone() as Compare;
			obj.centerItem = this.CenterItem.Clone();
			obj.SpellCode = this.SpellCode.Clone();

			return obj;
		}
	}
}
