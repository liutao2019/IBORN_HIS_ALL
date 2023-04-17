using System;

namespace Neusoft.HISFC.Object.Material
{
	/// <summary>
	/// [��������: �����ֵ�]
	/// [�� �� ��: �]
	/// [����ʱ��: 2007-03-10]
	/// ID ������Ʒ���� Name ������Ʒ����
	/// </summary>
	public class MaterialItem:Neusoft.HISFC.Object.Base.Item
	{		
		public MaterialItem()
		{
			
		}


		#region �� 

		/// <summary>
		/// �ֿ����
		/// </summary>
		private Neusoft.NFC.Object.NeuObject storageInfo = new Neusoft.NFC.Object.NeuObject();
	
		/// <summary>
		/// ���
		/// </summary>
		private string specification;
		
		/// <summary>
		/// ��Ʒ��Ŀ��Ϣ
		/// </summary>		
		private Neusoft.NFC.Object.NeuObject materialKind = new Neusoft.NFC.Object.NeuObject();		
	
		/// <summary>
		/// ������λ
		/// </summary>
		private string minUnit = string.Empty;

		/// <summary>
		/// ����(��С��λ)
		/// </summary>
		private decimal unitPrice;
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private string approveInfo;

		/// <summary>
		/// ���Ķ�����Ϣ(ҽ����Ŀ)
		/// </summary>
		private Neusoft.HISFC.Object.SIInterface.Compare compare = new Neusoft.HISFC.Object.SIInterface.Compare();

		/// <summary>
		/// ��Ӧ�ķ�ҩƷ��Ŀ��Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.Fee.Item.Undrug undrugInfo = new Neusoft.HISFC.Object.Fee.Item.Undrug();

		/// <summary>
		/// ͣ�ñ��(0����Ч,1��ͣ�� 2 ����״̬)
		/// </summary>
		private bool validState;

		/// <summary>
		/// ������
		/// </summary>
		private string specialFlag = string.Empty;

		/// <summary>
		/// ��������
		/// </summary>
		private MaterialCompany factory = new MaterialCompany();

		/// <summary>
		/// ������˾
		/// </summary>
		private MaterialCompany company = new MaterialCompany();

		/// <summary>
		/// ͳ�ƴ���
		/// </summary>
		private Neusoft.NFC.Object.NeuObject statInfo = new Neusoft.NFC.Object.NeuObject();

		/// <summary>
		/// �Ӽ۹���
		/// </summary>
		private string addRule = string.Empty;

		/// <summary>
		/// ��Դ
		/// </summary>
		private string inSource = string.Empty;

		/// <summary>
		/// ��;
		/// </summary>
		private string usage = string.Empty;

		/// <summary>
		/// ���װ��λ
		/// </summary>
		private string packUnit = string.Empty;

		/// <summary>
		/// ���װ�۸�
		/// </summary>
		private decimal packPrice; 

		/// <summary>
		/// ����Ա
		/// </summary>
		private Neusoft.NFC.Object.NeuObject oper = new Neusoft.NFC.Object.NeuObject();
		
		/// <summary>
		/// ��������
		/// </summary>
		private DateTime operTime;	

		/// <summary>
		/// �����շ���Ʒ��־(0����,1����)
		/// </summary>
		private bool financeState;
		/// <summary>
		/// ������
		/// </summary>
		private string mader="";
		/// <summary>
		/// ע���
		/// </summary>
		private string zch="";
		/// <summary>
		/// �������
		/// </summary>
		private string speType = "";
		/// <summary>
		/// ע��ʱ��
		/// </summary>
		private DateTime zc_date;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime over_date;

		
				
		#endregion

		#region ����
		/// <summary>
		/// ������
		/// </summary>
		public string Mader
		{
			get
			{
				return this.mader;
			}
			set
			{
				this.mader = value;
			}
		}
		/// <summary>
		/// ע���
		/// </summary>
		public string ZCH
		{
			get
			{
				return this.zch;
			}
			set
			{
				this.zch = value;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string SpeType
		{
			get
			{
				return this.speType;
			}
			set
			{
				this.speType = value;
			}
		}
		/// <summary>
		/// ע��ʱ��
		/// </summary>
		public DateTime ZCDate
		{
			get
			{
				return this.zc_date;
			}
			set
			{
				this.zc_date = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OverDate
		{
			get
			{
				return this.over_date;
			}
			set
			{
				this.over_date = value;
			}
		}
		/// <summary>
		/// �ֿ����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject StorageInfo
		{
			get
			{
				return this.storageInfo;
			}
			set
			{
				this.storageInfo = value;
			}
		}
		
		/// <summary>
		/// ��Ʒ����
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
			}
		}

		/// <summary>
		/// ��Ʒ��Ŀ��Ϣ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject MaterialKind
		{
			get
			{
				return this.materialKind;
			}
			set
			{
                this.materialKind = value;
			}
		}    
		
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}
		
		/// <summary>
		/// ƴ������
		/// </summary>
		public new string SpellCode
		{
			get
			{
				return base.SpellCode;
			}
			set
			{
                base.SpellCode = value;
			}
		}

		/// <summary>
		/// �����
		/// </summary>
		public new string WbCode 
		{
			get
			{
				return base.WBCode;
			}
			set
			{
				base.WBCode = value;
			}
		}

		/// <summary>
		/// �Զ�����
		/// </summary>
		public new string UserCode
		{
			get
			{
				return base.UserCode;
			}
			set
			{
				base.UserCode = value;
			}
		}

		/// <summary>
		/// ���ұ���
		/// </summary>
		public new string GbCode
		{
			get
			{
				return base.GBCode;
			}
			set
			{
				base.GBCode = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		public new string Specs
		{
			get
			{
				return this.specification;
			}
			set
			{
				this.specification = value;

			}
		}

		/// <summary>
		/// ��С��λ
		/// </summary>
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
		/// ����(��С��λ�۸�)
		/// </summary>
		public decimal UnitPrice 
		{
			get
			{
				return this.unitPrice;
			}
			set
			{
				this .unitPrice = value;
			}
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public string ApproveInfo
		{
			get
			{
				return this.approveInfo;
			}
			set
			{
				this.approveInfo = value;
			}
		}

		/// <summary>
		/// ���Ķ�����Ϣ(ҽ����Ŀ)
		/// </summary>
		public Neusoft.HISFC.Object.SIInterface.Compare Compare
		{
			get
			{
				return this.compare;
			}
			set
			{
				this.compare = value;
			}
		}

		/// <summary>
		/// ��ҩƷ��Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.Fee.Item.Undrug UndrugInfo  
		{
			get
			{
				return this.undrugInfo;
			}
			set
			{
				this.undrugInfo = value;
			}
		}

		/// <summary>
		/// ͣ�ñ��(1����Ч,0��ͣ�� 2 ����״̬)
		/// </summary>
		public bool ValidState 
		{
			get
			{
				return this.validState;
			}
			set
			{
				this.validState = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public new string SpecialFlag
		{
			get
			{
				return base.SpecialFlag;
			}
			set
			{
				base.SpecialFlag = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public MaterialCompany Factory 
		{
			get
			{
				return this.factory;
			}
			set
			{
				this.factory = value;
			}
		}

		/// <summary>
		/// ������˾
		/// </summary>
		public MaterialCompany Company 
		{
			get
			{
				return this.company;
			}
			set
			{
				this.company = value;
			}
		}
	
		/// <summary>
		/// ͳ�ƴ���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject StatInfo 
		{
			get
			{
				return this.statInfo;
			}
			set
			{
				this.statInfo = value;
			}
		}

		/// <summary>
		/// �Ӽ۹���
		/// </summary>
		public string AddRule
		{
			get
			{
				return this.addRule;
			}
			set
			{
				this.addRule = value;
			}
		}

		/// <summary>
		/// ���װ��λ
		/// </summary>
		public string PackUnit
		{
			get
			{
				return this.packUnit;
			}
			set
			{
				this.packUnit = value;
			}
		}

		/// <summary>
		/// ���װ����
		/// </summary>
		public new decimal PackQty
		{
			get
			{
				return base.PackQty;
			}
			set
			{
				base.PackQty = value;
			}
		}

		/// <summary>
		/// ���װ�۸�
		/// </summary>
		public decimal PackPrice
		{
			get
			{
				return this.packPrice;
			}
			set
			{
				this.packPrice = value;
			}
		}

		/// <summary>
		/// ��Դ
		/// </summary>
		public string InSource 
		{
			get
			{
				return this.inSource;
			}
			set
			{
				this.inSource = value;
			}
		}

		/// <summary>
		/// ��;
		/// </summary>
		public string Usage
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
		/// ����Ա
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Oper 
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
		/// ��������
		/// </summary>
		public DateTime OperTime 
		{
			get
			{
				return this.operTime;
			}
			set
			{
				this.operTime = value;
			}
		}

		/// <summary>
		/// �����շѱ�־
		/// </summary>
		public bool FinanceState 
		{
			get
			{
				return this.financeState;
			}
			set
			{
				this.financeState = value;
			}
		}

		

    	#endregion

		#region ����		
	
		/// <summary>
		/// ������¡
		/// </summary>
		/// <returns>�ɹ����ؿ�¡���MaterialItemʵ�� ʧ�ܷ���null</returns>
		public new MaterialItem Clone()
		{
			MaterialItem materialItem = base.Clone() as MaterialItem;

			materialItem.StorageInfo = this.StorageInfo.Clone();

			materialItem.Compare = this.Compare.Clone();

			materialItem.Oper = this.Oper.Clone();

			materialItem.UndrugInfo = this.UndrugInfo.Clone();

			materialItem.Factory = this.Factory.Clone();

			materialItem.Company = this.Company.Clone();

			materialItem.MaterialKind = this.MaterialKind.Clone();

			materialItem.StatInfo = this.StatInfo.Clone();

			return materialItem;
		} 

		#endregion
	}
}
