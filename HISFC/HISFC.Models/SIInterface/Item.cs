using System;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// Item ��ժҪ˵����
	/// </summary>
    [Serializable]
    public class Item : FS.HISFC.Models.Base.Spell {

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
        /// ����ҩ��־
        /// </summary>
        private bool prescFlag;
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
        /// ����ҩ��־
        /// </summary>
        public bool PrescFlag
        {
            set
            {
                this.prescFlag = value;
            }
            get
            {
                return prescFlag;
            }
        }

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new Item Clone()
		{
			Item obj = base.Clone() as Item;

			return obj;
		}
		

	}
}
