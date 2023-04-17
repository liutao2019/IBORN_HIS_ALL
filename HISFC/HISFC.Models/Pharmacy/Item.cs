using System;
 
namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ҩƷ������Ϣ]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-11]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.ComponentModel.DisplayName("ҩƷ�ֵ���Ϣ")]
    [Serializable]
	public class Item : FS.HISFC.Models.Base.Item,FS.HISFC.Models.Base.IValidState
	{
		public Item()
		{
			//this.IsPharmacy = true;
            this.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
		}


		#region ����

		/// <summary>
		/// ����������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��Ŀ������Ϣ
		/// </summary>
		private FS.HISFC.Models.IMA.NameService nameCollection = new FS.HISFC.Models.IMA.NameService();	

		/// <summary>
		/// �۸���Ϣ
		/// </summary>
		private FS.HISFC.Models.IMA.PriceService priceCollection = new FS.HISFC.Models.IMA.PriceService();
			
		/// <summary>
		/// ��Ʒ��Ϣ
		/// </summary>
		private FS.HISFC.Models.Pharmacy.Base.ProductService product = new FS.HISFC.Models.Pharmacy.Base.ProductService();

		/// <summary>
		/// ����Ļ��ʾ
		/// </summary>
		private bool isShow;

		/// <summary>
		/// ��ʾ���� 0 ȫԺ 1 סԺ  2 ����
		/// </summary>
		private string showState;
		
		/// <summary>
		/// �Ƿ�ͣ��
		/// </summary>
		private bool isStop;	
		
		/// <summary>
		/// �Ƿ�GMP
		/// </summary>
		private bool isGMP;

		/// <summary>
		/// �Ƿ�OTC
		/// </summary>
		private bool isOTC;

		/// <summary>
		/// �Ƿ���ҩ
		/// </summary>
		private bool isNew;

		/// <summary>
		/// �Ƿ�ȱҩ
		/// </summary>
		private bool isLack;

		/// <summary>
		/// �Ƿ���Ҫ����
		/// </summary>
		private bool isAllergy;

		/// <summary>
		/// �Ƿ񸽲�
		/// </summary>
		private bool isSubtbl;

		/// <summary>
		/// ��Ч�ɷ�
		/// </summary>
		private string ingredient;

		/// <summary>
		/// ��ҩִ�б�׼
		/// </summary>
		private string executeStandard;

		/// <summary>
		/// �б���Ϣ��
		/// </summary>
		private TenderOffer tenderOffer = new TenderOffer();

		/// <summary>
		/// �䶯����
		/// </summary>
		private ItemShiftType shiftType = new ItemShiftType();

		/// <summary>
		/// �䶯ʱ��
		/// </summary>
		private DateTime shiftTime;

		/// <summary>
		/// �䶯ԭ��
		/// </summary>
		private string shiftMark;

		/// <summary>
		/// ��ϵͳҩƷ����
		/// </summary>
		private string oldDrugID;
         
		/// <summary>
        /// �������(����)
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
		/// </summary>
        private string splitType;

        /// <summary>
        /// �������(סԺ��ʱҽ��)
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
        /// </summary>
        private string lZSplitType;

        /// <summary>
        /// �������(סԺ����ҽ��)
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
        /// </summary>
        private string cDSplitType ;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private FS.HISFC.Models.Base.EnumValidState validState = FS.HISFC.Models.Base.EnumValidState.Valid;

        /// <summary>
        /// �Ƿ�Э������ҩ
        /// </summary>
        private bool isNostrum = false;


		#endregion

		#region ҩƷʹ����Ϣ����

		/// <summary>
		/// ��װ��λ
		/// </summary>
		private string packUnit;

		/// <summary>
		/// ��С��λ
		/// </summary>
		private string minUnit;

		/// <summary>
		/// ��������
		/// </summary>
		private decimal baseDose;

		/// <summary>
		/// ������λ
		/// </summary>
		private string doseUnit;

        /// <summary>
        /// ��������
        /// </summary>
        private decimal secondBaseDose;

        /// <summary>
        /// ������λ
        /// </summary>
        private string secondDoseUnit;

		/// <summary>
		/// һ�μ���
		/// </summary>
		private decimal onceDose;

        private string onceDoseUnit = "";


		/// <summary>
		/// ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject dosageForm = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��� ��ҩ����ҩ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject type = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ҩƷ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject quality = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ʹ�÷���
		/// </summary>
		private FS.FrameWork.Models.NeuObject usage = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// Ƶ��
		/// </summary>
		private FS.HISFC.Models.Order.Frequency frequency = new FS.HISFC.Models.Order.Frequency();

		/// <summary>
		/// ҩ������1
		/// </summary>
		private FS.FrameWork.Models.NeuObject phyFunction1 = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ҩ������2
		/// </summary>
		private FS.FrameWork.Models.NeuObject phyFunction2 = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ҩ������3
		/// </summary>
		private FS.FrameWork.Models.NeuObject phyFunction3 = new FS.FrameWork.Models.NeuObject();

		#endregion

        #region ҩƷ��չ��Ϣ����

        /// <summary>
        /// ��չ����1
        /// </summary>
        private string extendData1;

        /// <summary>
       /// ��չ����2
       /// </summary>
        private string extendData2;

        /// <summary>
        /// �ֵ佨��ʱ��
        /// </summary>
        private DateTime createTime;

        /// <summary>
        /// ҩƷ�ڶ����ۼ�
        /// </summary>
        private decimal retailPrice2;

        /// <summary>
        /// ��չ����01
        /// </summary>
        private decimal extNumber1;

        /// <summary>
        /// ��չ����02
        /// </summary>
        private decimal extNumber2;

        /// <summary>
        /// ��չ����03
        /// </summary>
        private string extendData3;

        /// <summary>
        /// ��չ����04
        /// </summary>
        private string extendData4;

        #endregion

        #region ������ƷID�����װ��λ�����װ����
        /// <summary>
        /// ��ƷID
        /// </summary>
        private string productID;

        /// <summary>
        /// ���װ��λ
        /// </summary>
        private string bigPackUnit;

        /// <summary>
        /// ���װ����
        /// </summary>
        private string bigPackQty;
        #endregion



        /// <summary>
		/// ��Ŀ����
		/// </summary>
		public new string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
				this.nameCollection.ID = value;
				this.priceCollection.ID = value;
				this.product.ID = value;
			}
		}


		/// <summary>
		/// ��Ŀ����
		/// </summary>
        [System.ComponentModel.DisplayName("ҩƷ����")]
        [System.ComponentModel.Description("ҩƷ��Ʒ����")]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
				this.nameCollection.Name = value;
				this.priceCollection.Name = value;
				this.product.Name = value;
			}
		}
		

		/// <summary>
		/// ��Ŀ������Ϣ
		/// </summary>
		public FS.HISFC.Models.IMA.NameService NameCollection
		{
			get
			{
				return this.nameCollection;
			}
			set
			{
				this.nameCollection = value;
			}
		}

        /// <summary>
        /// �۸� (���ۼ�)
        /// </summary>
        public new decimal Price
        {
            get
            {
                if (this.priceCollection.RetailPrice != 0)
                {
                    base.Price = this.priceCollection.RetailPrice;
                    return this.priceCollection.RetailPrice;
                }
                else
                {
                    return base.Price;
                }
            }
            set
            {
                this.priceCollection.RetailPrice = value;
                base.Price = value;
            }
        }

        /// <summary>
        /// ƴ����
        /// </summary>
        [System.ComponentModel.DisplayName("ƴ����")]
        [System.ComponentModel.Description("ҩƷ��Ʒ���Ƶ�ƴ����")]
        public new string SpellCode
        {
            get
            {
                return this.nameCollection.SpellCode;
            }
            set
            {
                base.SpellCode = value;
                this.nameCollection.SpellCode = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        [System.ComponentModel.DisplayName("�����")]
        [System.ComponentModel.Description("ҩƷ��Ʒ���Ƶ������")]
        public new string WBCode
        {
            get
            {
                return this.nameCollection.WBCode;
            }
            set
            {
                base.WBCode = value;
                this.nameCollection.WBCode = value;
            }
        }

        /// <summary>
        /// �Զ�����
        /// </summary>
        [System.ComponentModel.DisplayName("�Զ�����")]
        [System.ComponentModel.Description("ҩƷ��Ʒ���Ƶ��Զ�����")]
        public new string UserCode
        {
            get
            {
                return this.nameCollection.UserCode;
            }
            set
            {
                base.UserCode = value;
                this.nameCollection.UserCode = value;
            }
        }

        /// <summary>
        /// ���ұ���
        /// </summary>
        [System.ComponentModel.DisplayName("���ұ���")]
        [System.ComponentModel.Description("ҩƷ���ұ���")]
        public new string GBCode
        {
            get
            {
                return this.nameCollection.GbCode;
            }
            set
            {
                base.GBCode = value;
                this.nameCollection.GbCode = value;
            }
        }

        /// <summary>
        /// ���ʱ���
        /// </summary>
        [System.ComponentModel.DisplayName("���ʱ���")]
        [System.ComponentModel.Description("ҩƷ���ʱ���")]
        public new string NationCode
        {
            get
            {
                return this.nameCollection.InternationalCode;
            }
            set
            {
                base.NationCode = value;
                this.nameCollection.InternationalCode = value;
            }
        }		

		/// <summary>
		/// ��װ��λ
		/// </summary>
        [System.ComponentModel.DisplayName("��װ��λ")]
        [System.ComponentModel.Description("ҩƷ��װ��λ")]
		public string PackUnit
		{
			get
			{
				return this.packUnit;
			}
			set
			{
				this.packUnit = value;
                base.PriceUnit = value;
			}
		}
		

		/// <summary>
		/// ��С��λ
		/// </summary>
        [System.ComponentModel.DisplayName("��С��λ")]
        [System.ComponentModel.Description("ҩƷ��С��λ")]
		public string MinUnit
		{
			get
			{
				return this.minUnit;
			}
			set
			{
				this.minUnit = value;
			}
		}
		

		/// <summary>
		/// ��������
		/// </summary>
        [System.ComponentModel.DisplayName("��������")]
        [System.ComponentModel.Description("ҩƷ��������")]
		public decimal BaseDose
		{
			get
			{
				return this.baseDose;
			}
			set
			{
				this.baseDose = value;
			}
		}
		

		/// <summary>
		/// ������λ
		/// </summary>
        [System.ComponentModel.DisplayName("������λ")]
        [System.ComponentModel.Description("ҩƷ������λ")]
		public string DoseUnit
		{
			get
			{
				return this.doseUnit;
			}
			set
			{
				this.doseUnit = value;
			}
		}
		

		/// <summary>
		/// һ������(����)
		/// </summary>
        [System.ComponentModel.DisplayName("ÿ�μ���")]
        [System.ComponentModel.Description("ҩƷÿ�μ���")]
		public decimal OnceDose
		{
			get
			{
				return this.onceDose;
			}
			set
			{
				this.onceDose = value;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("ҩƷ����")]
		public FS.FrameWork.Models.NeuObject DosageForm
		{
			get
			{
				return this.dosageForm;
			}
			set
			{
				this.dosageForm = value;
			}
		}
		

		/// <summary>
		/// ��� ��ҩ����ҩ��
		/// </summary>
        [System.ComponentModel.DisplayName("���")]
        [System.ComponentModel.Description("ҩƷ���")]
		public FS.FrameWork.Models.NeuObject Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}
		

		/// <summary>
		/// ���� �գ��� 
		/// </summary>
        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("ҩƷ����")]
		public FS.FrameWork.Models.NeuObject Quality
		{
			get
			{
				return this.quality;
			}
			set
			{
				this.quality = value;
			}
		}
		

		/// <summary>
		/// ʹ�÷���
		/// </summary>
        [System.ComponentModel.DisplayName("ʹ�÷���")]
        [System.ComponentModel.Description("ҩƷʹ�÷���")]
		public FS.FrameWork.Models.NeuObject Usage
		{
			get
			{
				return this.usage;
			}
			set
			{
				this.usage = value;
			}
		}


		/// <summary>
		/// Ƶ��
		/// </summary>
        [System.ComponentModel.DisplayName("Ƶ��")]
        [System.ComponentModel.Description("ҩƷƵ��")]
		public FS.HISFC.Models.Order.Frequency Frequency
		{
			get
			{
				return this.frequency;
			}
			set
			{
				this.frequency = value;
			}
		}
		

		/// <summary>
		/// һ��ҩ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject PhyFunction1
		{
			get
			{
				return this.phyFunction1;
			}
			set
			{
				this.phyFunction1 = value;
			}
		}
		

		/// <summary>
		/// ����ҩ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject PhyFunction2
		{
			get
			{
				return this.phyFunction2;
			}
			set
			{
				this.phyFunction2 = value;
			}
		}
		

		/// <summary>
		/// ����ҩ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject PhyFunction3
		{
			get
			{
				return this.phyFunction3;
			}
			set
			{
				this.phyFunction3 = value;
			}
		}
		

		/// <summary>
		/// �۸���Ϣ
		/// </summary>
		public FS.HISFC.Models.IMA.PriceService PriceCollection
		{
			get
			{
				return this.priceCollection;
			}
			set
			{
				this.priceCollection = value;
                base.Price = value.RetailPrice;
			}
		}


		/// <summary>
		/// ��Ʒ��Ϣ
		/// </summary>
		public FS.HISFC.Models.Pharmacy.Base.ProductService Product
		{
			get
			{
				return this.product;
			}
			set
			{
				this.product = value;
			}
		}
		

		/// <summary>
		/// �Ƿ�ͣ��
		/// </summary>
        [System.ComponentModel.DisplayName("�Ƿ�ͣ��")]
        [System.ComponentModel.Description("ҩƷ�Ƿ�ͣ��")]
        [Obsolete("�����Բ������ݿ��ڻ�ȡ",false)]
		public bool IsStop
		{
			get
			{
				return this.isStop;
			}
			set
			{
				this.isStop = value;

                if (value)
                {
                    this.validState = FS.HISFC.Models.Base.EnumValidState.Invalid;
                }
                else
                {
                    this.validState = FS.HISFC.Models.Base.EnumValidState.Valid;
                }               
			}
		}

        #region IValidState ��Ա

        public new FS.HISFC.Models.Base.EnumValidState ValidState
        {
            get
            {
                return this.validState;
            }
            set
            {
                if (value == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    this.isStop = false;
                }
                else
                {
                    this.isStop = true;
                }

                this.validState = value;
            }
        }

        #endregion
		

		/// <summary>
		/// �Ƿ�GMP
		/// </summary>
        [System.ComponentModel.DisplayName("�Ƿ�GMP")]
        [System.ComponentModel.Description("ҩƷ�Ƿ�GMP")]
		public bool IsGMP
		{
			get
			{
				return this.isGMP;
			}
			set
			{
				this.isGMP = value;
			}
		}
		

		/// <summary>
		/// �Ƿ�OTC
		/// </summary>
        [System.ComponentModel.DisplayName("�Ƿ�OTC")]
        [System.ComponentModel.Description("ҩƷ�Ƿ�OTC")]
		public bool IsOTC
		{
			get
			{
				return this.isOTC;
			}
			set
			{
				this.isOTC = value;
			}
		}
		

		/// <summary>
		/// �Ƿ���ҩ
		/// </summary>
		public bool IsNew
		{
			get
			{
				return this.isNew;
			}
			set
			{
				this.isNew = value;
			}
		}
		

		/// <summary>
		/// �Ƿ�ȱҩ
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
		/// ����Ļ��ʾ
		/// </summary>
		public bool IsShow
		{
			get
			{
				return this.isShow;
			}
			set
			{
				this.isShow = value;
			}
		}


		/// <summary>
		/// ��ʾ���� 0 ȫԺ 1 סԺ  2 ����
		/// </summary>
		public string ShowState
		{
			get
			{
				return this.showState;
			}
			set
			{
				this.showState = value;
			}
		}


		/// <summary>
		/// �Ƿ���Ҫ����
		/// </summary>
        [System.ComponentModel.DisplayName("�Ƿ���Ҫ����")]
        [System.ComponentModel.Description("ҩƷ�Ƿ���Ҫ���� ������ʾҽ��")]
		public bool IsAllergy
		{
			get
			{
				return this.isAllergy;
			}
			set
			{
				this.isAllergy = value;
			}
		}
		

		/// <summary>
		/// �Ƿ񸽲�
		/// </summary>
		public bool IsSubtbl
		{
			get
			{
				return this.isSubtbl;
			}
			set
			{
				this.isSubtbl = value;
			}
		}
		
		
		/// <summary>
		/// ��Ч�ɷ�
		/// </summary>
		public string Ingredient
		{
			get
			{
				return this.ingredient;
			}
			set
			{
				this.ingredient = value;
			}
		}


		/// <summary>
		/// ��ҩִ�б�׼
		/// </summary>
		public string ExecuteStandard
		{
			get
			{
				return this.executeStandard;
			}
			set
			{
				this.executeStandard = value;
			}
		}
		
		
		/// <summary>
		/// �б���Ϣ��
		/// </summary>
		public TenderOffer TenderOffer
		{
			get
			{
				return this.tenderOffer;
			}
			set
			{
				this.tenderOffer = value;
			}
		}


		/// <summary>
		/// �䶯����
		/// </summary>
		public ItemShiftType ShiftType
		{
			get
			{
				return this.shiftType;
			}
			set
			{
				this.shiftType = value;
			}
		}


		/// <summary>
		/// �䶯ʱ��
		/// </summary>
		public DateTime ShiftTime
		{
			get
			{
				return this.shiftTime;
			}
			set
			{
				this.shiftTime = value;
			}
		}


		/// <summary>
		/// �䶯ԭ��
		/// </summary>
		public string ShiftMark
		{
			get
			{
				return this.shiftMark;
			}
			set
			{
				this.shiftMark = value;
			}
		}


		/// <summary>
		/// ��ϵͳҩƷ����
		/// </summary>
		public string OldDrugID
		{
			get
			{
				return this.oldDrugID;
			}
			set
			{
				this.oldDrugID = value;
			}
		}


		/// <summary>
        /// ����������
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
		/// </summary>
        [System.ComponentModel.DisplayName("�������")]
        [System.ComponentModel.Description("ҩƷ������� ����������Ч")]
		public string SplitType 
		{
			get
			{
				return this.splitType;
			}
			set
			{
				this.splitType = value;
			}
		}

        /// <summary>
        /// סԺ�����������
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
        /// </summary>
        [System.ComponentModel.DisplayName("�������")]
        [System.ComponentModel.Description("ҩƷ������� ����סԺ��ʱҽ����Ч")]
        public string  LZSplitType
        {
            get
            {
                return this.lZSplitType;
            }
            set 
            {
                this.lZSplitType = value; 
            }
        }

        /// <summary>
        /// ҩƷ�ȼ�
        /// </summary>
        [System.ComponentModel.DisplayName("ҩƷ�ȼ�")]
        [System.ComponentModel.Description("ҩƷ�ȼ� ��ҽ��ְ�����")]
        public new string Grade
        {
            get
            {
                return base.Grade;
            }
            set
            {
                base.Grade = value;
            }
        }


		/// <summary>
		/// ����������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}

        /// <summary>
        /// �Ƿ�Э������ҩ
        /// </summary>
        [System.ComponentModel.DisplayName("Э������ҩ���")]
        [System.ComponentModel.Description("Э������ҩ���")]
        public bool IsNostrum
        {
            get
            {
                return this.isNostrum;
            }
            set
            {
                this.isNostrum = value;
            }
        }

        /// <summary>
        /// ��չ����1     {8ADD2D48-2427-48aa-A521-4B17EECBC8B4}
        /// </summary>
        public string ExtendData1
        {
            get
            {
                return this.extendData1;
            }
            set
            {
                this.extendData1 = value;
            }
        }

        /// <summary>
        /// ��չ����2     {8ADD2D48-2427-48aa-A521-4B17EECBC8B4}
        /// </summary>
        public string ExtendData2
        {
            get
            {
                return this.extendData2;
            }
            set
            {
                this.extendData2 = value;
            }
        }

        /// <summary>
        /// �ֵ佨��ʱ��  {8ADD2D48-2427-48aa-A521-4B17EECBC8B4}
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return this.createTime;
            }
            set
            {
                this.createTime = value;
            }
        }

        /// <summary>
        /// ҩƷ�ڶ����ۼ�
        /// </summary>
        [System.ComponentModel.DisplayName("�ڶ����ۼ�")]
        [System.ComponentModel.Description("ҩƷ�ڶ����ۼ�")]
        public decimal RetailPrice2
        {
            get
            {
                return this.retailPrice2;
            }
            set
            {
                this.retailPrice2 = value;
            }
        }

        /// <summary>
        /// ��չ����01
        /// </summary>
        public decimal ExtNumber1
        {
            get
            {
                return extNumber1;
            }
            set
            {
                this.extNumber1 = value;
            }
        }

        /// <summary>
        /// ��չ����02
        /// </summary>
        public decimal ExtNumber2
        {
            get
            {
                return extNumber2;
            }
            set
            {
                this.extNumber2 = value;
            }
        }

        /// <summary>
        /// ��չ����03
        /// </summary>
        public string ExtendData3
        {
            get
            {
                return extendData3;
            }
            set
            {
                this.extendData3 = value;
            }
        }
        /// <summary>
        /// סԺ�����������
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
        /// </summary>
        [System.ComponentModel.DisplayName("�����������")]
        [System.ComponentModel.Description("�����������")]
        public string CDSplitType
        {
            get
            {
                return this.cDSplitType;
            }
            set
            {
                this.cDSplitType = value;
            }
        }

        /// <summary>
        /// ��չ����04
        /// </summary>
        public string ExtendData4
        {
            get
            {
                return this.extendData4;
            }
            set
            {
                this.extendData4 = value;
            }
        }

        /// <summary>
        /// ��ƷID
        /// </summary>
        public string ProductID
        {
            get 
            { 
                return this.productID;
            }
            set 
            { 
                this.productID = value; 
            }
        }

        /// <summary>
        /// ���װ��λ
        /// </summary>
        public string BigPackUnit
        {
            get
            { return this.bigPackUnit; }
            set
            { this.bigPackUnit = value; }
        }

        /// <summary>
        /// ���װ����
        /// </summary>
        public string BigPackQty
        {
            get { return this.bigPackQty; }
            set { this.bigPackQty = value; }
        }

        /// <summary>
        /// ÿ��������λ
        /// </summary>
        [System.ComponentModel.DisplayName("ÿ��������λ")]
        [System.ComponentModel.Description("ÿ��������λ")]
        public string OnceDoseUnit
        {
            get { return onceDoseUnit; }
            set { onceDoseUnit = value; }
        }


        /// <summary>
        /// �ڶ���������
        /// </summary>
        [System.ComponentModel.DisplayName("�ڶ���������")]
        [System.ComponentModel.Description("�ڶ���������")]
        public decimal SecondBaseDose
        {
            get { return secondBaseDose; }
            set { secondBaseDose = value; }
        }

        /// <summary>
        /// �ڶ�������λ
        /// </summary>
        [System.ComponentModel.DisplayName("�ڶ�������λ")]
        [System.ComponentModel.Description("�ڶ�������λ")]
        public string SecondDoseUnit
        {
            get { return secondDoseUnit; }
            set { secondDoseUnit = value; }
        }

		#region ����

		/// <summary>
		/// ������¡
		/// </summary>
		/// <returns>�ɹ����ؿ�¡���Itemʵ�� ʧ�ܷ���null</returns>
		public new Item Clone()
		{
			Item item = base.Clone() as Item;

			item.NameCollection = this.NameCollection.Clone();
			item.DosageForm = this.DosageForm.Clone();
			item.Type = this.Type.Clone();
			item.Quality = this.Quality.Clone();
			item.Usage = this.Usage.Clone();
			item.Frequency = this.Frequency.Clone();
			item.PhyFunction1 = this.PhyFunction1.Clone();
			item.PhyFunction2 = this.PhyFunction2.Clone();
			item.PhyFunction3 = this.PhyFunction3.Clone();
			item.PriceCollection = this.PriceCollection.Clone();
			item.Product = this.Product.Clone();	
			item.TenderOffer = this.TenderOffer.Clone();
			item.ShiftType = this.ShiftType.Clone();

			return item;
		}

		#endregion
    }
}