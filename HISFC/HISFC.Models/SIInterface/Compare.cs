using System;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// Compare ��ժҪ˵����
	/// </summary>
    [Serializable]
    public class Compare : FS.FrameWork.Models.NeuObject 
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
		private FS.HISFC.Models.Base.Spell spellCode  = new FS.HISFC.Models.Base.Spell(); 
		//������Ŀ���
		private string specs;
		//������Ŀ�۸�
		private Decimal price;
		//������Ŀ����
		private string doseCode;

		private string regularName;
        #region ������Ӧ֢
        //{8FE289B0-3034-442b-A9C3-CDBF7EBDB7B2}
        /// <summary>
        /// �Ƿ�����Ӧ֢
        /// </summary>
        private bool ispracticablesymptom = false;

        /// <summary>
        /// ҩƷ��Ӧ�ȼ���IDΪ����,NAMEΪ���ƣ�
        /// </summary>
        private FS.FrameWork.Models.NeuObject practicablesymptom = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ӧ֢����
        /// </summary>
        private string practicablesymptomdepiction;
        //{8FE289B0-3034-442b-A9C3-CDBF7EBDB7B2} 
        #endregion 

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
		public Item CenterItem {
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
		public FS.HISFC.Models.Base.Spell SpellCode {
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

        /// <summary>
        /// ҽ����ʶ 1��ҽ���ڣ�0��ҽ����
        /// </summary>
        private string centerFlag;

        /// <summary>
        /// ҽ����ʶ 1��ҽ���ڣ�0��ҽ����
        /// </summary>
        public string CenterFlag
        {
            set
            {
                this.centerFlag = value;
            }
            get
            {
                return centerFlag;
            }
        }
        /// <summary>
        /// ��װ��λ
        /// </summary>
        private string packUnit;

        public string PackUnit
        {
            set
            {
                this.packUnit = value;
            }
            get
            {
                return packUnit;
            }
        }
        #region ������Ӧ֢
        //{8FE289B0-3034-442b-A9C3-CDBF7EBDB7B2}
        /// <summary>
        /// �Ƿ�����Ӧ֢
        /// </summary>
        public bool Ispracticablesymptom
        {
            get
            {
                //{8DF3D566-FA34-44cb-A2D5-919FE05D1702}
                //if (this.practicablesymptomdepiction == "" || this.practicablesymptomdepiction == null)
                if (this.practicablesymptom.ID == "" || this.practicablesymptom.ID == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                ispracticablesymptom = value;
            }
        }

        /// <summary>
        /// ҩƷ��Ӧ�ȼ���IDΪ����,NAMEΪ���ƣ�
        /// </summary>
        public FS.FrameWork.Models.NeuObject Practicablesymptom
        {
            get
            {
                return practicablesymptom;
            }
            set
            {
                practicablesymptom = value;
            }
        }

        /// <summary>
        /// ��Ӧ֢����
        /// </summary>
        public string Practicablesymptomdepiction
        {
            get
            {
                return practicablesymptomdepiction;
            }
            set
            {
                practicablesymptomdepiction = value;
            }
        }
        //{8FE289B0-3034-442b-A9C3-CDBF7EBDB7B2} 
        #endregion

        #region (�麣ҽ������) 

        private FS.FrameWork.Models.NeuObject feeProperty = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������(�麣ҽ������) 
        /// IDΪ����,NAMEΪ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject FeeProperty
        {
            get
            {
                return this.feeProperty;
            }
            set
            {
                this.feeProperty = value;
            }
        }

        private string fdaDrugCode;

        /// <summary>
        /// ҩ���ҩƷ����(�麣ҽ������) 
        /// </summary>
        public string FdaDrguCode
        {
            get
            {
                return this.fdaDrugCode;
            }
            set
            {
                this.fdaDrugCode = value;
            }
        }

        #endregion
        public new Compare Clone()
		{
			Compare obj = base.Clone() as Compare;
			obj.centerItem = this.CenterItem.Clone();
			obj.SpellCode = this.SpellCode.Clone();
            obj.practicablesymptom = this.Practicablesymptom.Clone();
            obj.feeProperty = this.FeeProperty.Clone();
			return obj;
		}
	}
}
