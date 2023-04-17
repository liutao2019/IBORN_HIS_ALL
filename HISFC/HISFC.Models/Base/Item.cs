using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using System;
using FS.HISFC.Models.Fee.Item;
namespace FS.HISFC.Models.Base
{   
    
	/// <summary>
	/// Item<br></br>
	/// [��������: ��Ŀ����]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Item : Spell, IValid
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Item()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// ���ʱ���
		/// </summary>
		private string nationCode;

		/// <summary>
		/// ���ұ���
		/// </summary>
		private string gbCode;

		/// <summary>
		/// ϵͳ���
		/// </summary>
        private SysClassEnumService sysClass;

		/// <summary>
		/// ��С����
		/// </summary>
        private NeuObject minFee;

		/// <summary>
		/// ���׼�,�����۸�
		/// </summary>
		private decimal price;

		/// <summary>
		/// ��ͯ��
		/// </summary>
		private decimal childPrice;

		/// <summary>
		/// �����
		/// </summary>
		private decimal specialPrice;

		/// <summary>
		/// �Ǽ۵�λ
		/// </summary>
		private string  priceUnit;

		/// <summary>
		/// ��װ����
		/// </summary>
		private decimal packQty;

		/// <summary>
		/// ���
		/// </summary>
		private string specs;

		/// <summary>
		/// �Ǽ�����
		/// </summary>
		private decimal qty;

		/// <summary>
		/// �Ƿ�ҩƷ true:��ҩƷ false:��ҩƷ��������,�����Ŀ
		/// </summary>
		private bool isPharmacy;

        /// <summary>
        /// ��Ŀ���� Drug:ҩƷ Undrug:��ҩƷ��������,�����Ŀ MatItem:������Ŀ
        /// </summary>
        //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
        private EnumItemType itemType = EnumItemType.UnDrug;

		/// <summary>
		/// �Ƿ���Ҫ�ն�ȷ��
		/// </summary>
		private bool isNeedConfirm;

        /// <summary>
        /// ȷ�ϱ��
        /// </summary>
        private EnumNeedConfirm needConfirm = EnumNeedConfirm.None;

		/// <summary>
		/// �Ƿ���Ч true��Ч[1] false��Ч[0]
		/// </summary>
		private bool isValid;

		/// <summary>
		/// ��Ч��״̬ 1 ��Ч ����״̬�Զ���
		/// </summary>
		private string validState;

		/// <summary>
		/// �Ƿ񸽲�
		/// </summary>
		private bool isMaterial;
		
		/// <summary>
		/// �Ƿ���ҪԤԼ
		/// </summary>
		private bool isNeedBespeak;

		/// <summary>
		/// �����־
		/// </summary>
		private string specialFlag;

		/// <summary>
		/// �����־1
		/// </summary>
		private string specialFlag1;

		/// <summary>
		/// �����־2
		/// </summary>
		private string specialFlag2;

		/// <summary>
		/// �����־3
		/// </summary>
		private string specialFlag3;

       

		/// <summary>
		/// ��ʾҩƷ�Ƿ����ҩ����һ�����ҩĿ¼��ʡ������ҩĿ¼)
		/// </summary>
        //{C320A724-1219-4200-BBC9-98BA6211F75B}
		private string specialFlag4;

        /// <summary>
        /// ԭʼ�۸񣨱���Ӧ�ռ۸񣬲����Ǻ�ͬ��λ���أ�
        /// {54B0C254-3897-4241-B3BD-17B19E204C8C}
        /// </summary>
        private decimal defPrice;

		
		/// <summary>
		/// ���ұ���
		/// </summary>
        private string grade;

        /// <summary>
        /// ҽԺ�ȼ�
        /// </summary>
        private string hosLevel = string.Empty;


        /// <summary>
        /// ��չ�ֶ�1
        /// </summary>
        private string extend1 = string.Empty;

        /// <summary>
        /// ��չ�ֶ�2
        /// </summary>
        private string extend2 = string.Empty;
		#endregion

		#region ����

		/// <summary>
		/// ���ʱ���
		/// </summary>
		public string NationCode
		{
			get
			{
				return this.nationCode;
			}
			set
			{
				this.nationCode = value;
			}
		}

		/// <summary>
		/// ���ұ���
		/// </summary>
		public string GBCode
		{
			get
			{
				return gbCode;
			}
			set
			{
				gbCode = value;
			}
		}

		/// <summary>
		/// ϵͳ���
		/// </summary>
		public SysClassEnumService SysClass
		{
			get
			{
                if (sysClass == null)
                {
                    sysClass = new SysClassEnumService();
                }
				return this.sysClass;
			}
			set
			{
				this.sysClass = value;
			}
		}

		/// <summary>
		/// ��С����
		/// </summary>
		public NeuObject MinFee
		{
			get
			{
                if (minFee == null)
                {
                    minFee = new NeuObject();
                }
				return this.minFee;
			}
			set
			{
				this.minFee = value;
			}
		}

		/// <summary>
		/// ���׼�,�����۸�
		/// </summary>
		public decimal Price
		{
			get
			{
				return this.price;
			}
			set
			{
				if (value < 0)
				{
					this.price = 0;
				}
				else
				{
					this.price = value;
				}
			}
		}

		/// <summary>
		/// ��ͯ��
		/// </summary>
		public decimal ChildPrice
		{
			get
			{
				return this.childPrice;
			}
			set
			{
				if (value < 0)
				{
					this.childPrice = 0;
				}
				else
				{
					this.childPrice = value;
				}
			}
		}

		/// <summary>
		/// �����
		/// </summary>
		public decimal SpecialPrice
		{
			get
			{
				return this.specialPrice;
			}
			set
			{
				if (value < 0)
				{
					this.specialPrice = 0;
				}
				else
				{
					this.specialPrice = value;
				}
			}
		}

		/// <summary>
		/// �Ǽ۵�λ
		/// </summary>
		public string PriceUnit
		{
			get
			{
				return this.priceUnit;
			}
			set
			{
				this.priceUnit = value;
			}
		}

		/// <summary>
		/// ��װ����
		/// </summary>
		public decimal PackQty
		{
			get
			{
				return this.packQty;
			}
			set
			{
				//��װ������С��1
				if(value < 1)
				{
					this.packQty = 1;
				}
				else
				{
					this.packQty = value;
				}
			}
		}

		/// <summary>
		/// ���
		/// </summary>
        [System.ComponentModel.DisplayName("���")]
        [System.ComponentModel.Description("ҩƷ���")]
		public string Specs
		{
			get
			{
				return this.specs;
			}
			set
			{
				this.specs = value;
			}
		}

		/// <summary>
		/// �Ǽ�����
		/// </summary>
		public decimal Qty
		{
			get
			{
				return this.qty;
			}
			set
			{
				this.qty = value;
			}
		}

        
		/// <summary>
		/// �Ƿ�ҩƷ
		/// </summary>
        //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
        [Obsolete("��ö��ItemType����",true)]
		public bool IsPharmacy
		{
			get
			{
				return this.isPharmacy;
			}
			set
			{
				this.isPharmacy = value;
			}
		}

        /// <summary>
        /// ��Ŀ���� Drug:ҩƷ Undrug:��ҩƷ��������,�����Ŀ MatItem:������Ŀ
        /// </summary>
        //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
        public EnumItemType ItemType
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
		/// �Ƿ���Ҫ�ն�ȷ��
		/// </summary>
		public bool IsNeedConfirm
		{
			get
			{
				return this.isNeedConfirm;
			}
			set
			{
				this.isNeedConfirm = value;
			}
        }

        /// <summary>
        /// ȷ�ϱ��
        /// </summary>
        public EnumNeedConfirm NeedConfirm
        {
            get
            {
                return needConfirm;
            }
            set
            {
                needConfirm = value;
            }
        }

		/// <summary>
		/// ��Ч��״̬ 1 ��Ч ����״̬�Զ���
		/// </summary>
		public string ValidState
		{
			get
			{
				
				return validState;
			}
			set
			{
				this.validState = value;
				//�����Ч��״̬����"1"����Ч,��ô����Ŀ����Ч���ж�Ϊfalse;
				if (validState == "1")
				{
					this.isValid = true;
				}
				else
				{
					this.isValid = false;
				}
			}
		}

		/// <summary>
		/// �Ƿ񸽲�
		/// </summary>
		public bool IsMaterial
		{
			get
			{
				return this.isMaterial;
			}
			set
			{
				this.isMaterial = value;
			}
		}
		
		/// <summary>
		/// �Ƿ���ҪԤԼ
		/// </summary>
		public bool IsNeedBespeak
		{
			get
			{
				return this.isNeedBespeak;
			}
			set
			{
				this.isNeedBespeak = value;
			}
		}

		/// <summary>
		/// �����־
		/// </summary>
		public string SpecialFlag
		{
			get
			{
				return this.specialFlag;
			}
			set
			{
				this.specialFlag = value;
			}
		}

		/// <summary>
		/// �����־1
		/// </summary>
		public string SpecialFlag1
		{
			get
			{
				return this.specialFlag1;
			}
			set
			{
				this.specialFlag1 = value;
			}
		}

		/// <summary>
		/// �����־2
		/// </summary>
		public string SpecialFlag2
		{
			get
			{
				return this.specialFlag2;
			}
			set
			{
				this.specialFlag2 = value;
			}
		}

		/// <summary>
		/// �����־3
		/// </summary>
		public string SpecialFlag3
		{
			get
			{
				return this.specialFlag3;
			}
			set
			{
				this.specialFlag3 = value;
			}

		}

		/// <summary>
        /// ��ʾҩƷ�Ƿ����ҩ����һ�����ҩĿ¼��ʡ������ҩĿ¼)
		/// </summary>
        //{C320A724-1219-4200-BBC9-98BA6211F75B}
		public string SpecialFlag4
		{
			get
			{
				return this.specialFlag4;
			}
			set
			{
				this.specialFlag4 = value;
			}
		}

		/// <summary>
		/// ���ұ���
		/// </summary>
		public string Grade
		{
			get
			{
                if (grade == null)
                {
                    grade = string.Empty;
                }
				return this.grade;
			}
			set
			{
				this.grade = value;
			}
		}

        /// <summary>
        /// ҽԺ�ȼ�
        /// </summary>
        public string HosLevel
        {
            get { return hosLevel; }
            set { hosLevel = value; }
        }

        /// <summary>
        /// ԭʼ�۸񣨱���Ӧ�ռ۸񣬲����Ǻ�ͬ��λ���أ�
        /// {54B0C254-3897-4241-B3BD-17B19E204C8C}
        /// </summary>
        public decimal DefPrice
        {
            get
            {
                return defPrice;
            }
            set
            {
                defPrice = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�1
        /// </summary>
        public string Extend1
        {
            get { return extend1; }
            set { extend1 = value; }
        }

        /// <summary>
        /// ��չ�ֶ�2
        /// </summary>
        public string Extend2
        {
            get { return extend2; }
            set { extend2 = value; }
        }



		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��¡��ĵ�ǰ�����ʵ��</returns>
		public new Item Clone()
		{
			Item item = base.Clone() as Item;

			item.MinFee = this.MinFee.Clone();
			
			return item;
		}

		#endregion

		#endregion
	
		#region �ӿ�ʵ��
		
		#region IValid ��Ա
		/// <summary>
		/// �Ƿ���Ч true��Ч[1] false��Ч[0]
		/// </summary>
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		#endregion
		
		#endregion

		
	}
}