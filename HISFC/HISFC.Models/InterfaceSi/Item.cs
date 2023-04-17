using System;

namespace neusoft.HISFC.Object.InterfaceSi
{
	/// <summary>
	/// Item ��ժҪ˵����
	/// </summary>
	public class Item : neusoft.neuFC.Object.neuObject,neusoft.HISFC.Object.Base.ISpellCode
	{
		public Item()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//��ͬ��λ����
		private string pactCode;
		//��Ŀ���
		private string sysClass;
		//��ĿӢ������
		private string englishName;
		//���
		private string specs;
		//���ͱ���
		private string doseCode;
		//ƴ����
		private Base.SpellCode spellCode = new neusoft.HISFC.Object.Base.SpellCode();
		//���÷������ 1 ��λ�� 2��ҩ��3��ҩ��4�г�ҩ5�в�ҩ6����7���Ʒ�8�����9������10�����11��Ѫ��12������13����
		private string feeCode;
		//ҽ��Ŀ¼���� 1 ����ҽ�Ʒ�Χ 2 �㶫ʡ������
		private string itemType;
		//ҽ��Ŀ¼�ȼ� 1 ����(ͳ��ȫ��֧��) 2 ����(׼�貿��֧��) 3 �Է�
		private string itemGrade;
		//�Ը�����
		private Decimal rate;
		//��׼�۸�
		private Decimal price;
		//����Ա����;
		private string operCode;
		//����ʱ��
		private DateTime operDate;
		/// <summary>
		/// ��ͬ��λ����
		/// </summary>
		public string PactCode
		{
			get
			{
				return pactCode;
			}
			set
			{
				pactCode = value;
			}
		}
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public string SysClass
		{
			get
			{
				return sysClass;
			}
			set
			{
				sysClass = value;
			}
		}
		/// <summary>
		/// ��ĿӢ������
		/// </summary>
		public string EnglishName
		{
			get
			{
				return englishName;
			}
			set
			{
				englishName = value;
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
		/// ���÷������ 1 ��λ�� 2��ҩ��3��ҩ��4�г�ҩ5�в�ҩ6����7���Ʒ�8�����9������
		/// 10�����11��Ѫ��12������13���� 
		/// </summary>
		public string FeeCode
		{
			get
			{
				return feeCode;
			}
			set
			{
				feeCode = value;
			}
		}
		/// <summary>
		/// ҽ��Ŀ¼���� 1 ����ҽ�Ʒ�Χ 2 �㶫ʡ������
		/// </summary>
		public string ItemType
		{
			get
			{
				return itemType;
			}
			set
			{
				itemType = value;
			}
		}
		/// <summary>
		/// ҽ��Ŀ¼�ȼ� 1 ����(ͳ��ȫ��֧��) 2 ����(׼�貿��֧��) 3 �Է�
		/// </summary>
		public string ItemGrade
		{
			get
			{
				return itemGrade;
			}
			set
			{
				itemGrade = value;
			}
		}
		/// <summary>
		/// �Ը�����
		/// </summary>
		public Decimal Rate
		{
			get
			{
				return rate;
			}
			set
			{
				rate = value;
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
		/// ����Ա
		/// </summary>
		public string OperCode
		{
			get
			{
				return operCode;
			}
			set
			{
				operCode = value;
			}
		}
		/// <summary>
		/// ����ʱ��
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

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new Item Clone()
		{
			Item obj = base.Clone() as Item;
			obj.SpellCode = this.SpellCode.Clone();

			return obj;
		}
		#region ISpellCode ��Ա

		public string Spell_Code
		{
			get
			{
				// TODO:  ��� Item.Spell_Code getter ʵ��
				return spellCode.Spell_Code;
			}
			set
			{
				// TODO:  ��� Item.Spell_Code setter ʵ��
				spellCode.Spell_Code = value;
			}
		}

		public string WB_Code
		{
			get
			{
				// TODO:  ��� Item.WB_Code getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� Item.WB_Code setter ʵ��
			}
		}

		public string User_Code
		{
			get
			{
				// TODO:  ��� Item.User_Code getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� Item.User_Code setter ʵ��
			}
		}

		#endregion
	}

}
