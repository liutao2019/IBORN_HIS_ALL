using System;


namespace Neusoft.HISFC.Object.PhysicalExam {


	/// <summary>
	/// ChkItemList ��ժҪ˵����
	/// </summary>
	public class ChkItemList: Neusoft.NFC.Object.NeuObject
	{
		public ChkItemList()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		private string unitFlag ;//��λ��ʶ 1 ҩƷ 2 ��ҩƷ 3��ϸ 4����
		private string clinicNo ;//������
		private string cardNo;//���￨��
//		private int amount;//����
		private decimal ecoRate;//�Żݱ���
		private string comNO; //��Ϻ�
		private string sequenceNo; //���к�
		private Neusoft.NFC.Object.NeuObject conformOper = null; //ȷ����
		private Neusoft.NFC.Object.NeuObject oper   = null;//����Ա
		private  Neusoft.NFC.Object.NeuObject execDept = null; //ִ�п���
		private Neusoft.HISFC.Object.Fee.Item.Undrug item = null; //��Ŀ
		private string extFlag;//  ��չ��־                                
		private decimal extNumber;// ��չ��־                                
		private string extChar;//  ��չ�ַ��ֶ�                            
		private string extFlag1;// ��չ��־                                
		private decimal extNumber1;//��չ��־                                
		private string extChar1;//   ��չ�ַ��ֶ� 
		private System.DateTime conformDate ;
		private string confirmFlag;
		/// <summary>
		/// ȷ�ϱ�־
		/// </summary>
		public string ConfirmFlag
		{
			get
			{
				return  confirmFlag;
			}
			set
			{
				confirmFlag = value;
			}
		}
		/// <summary>
		/// ȷ������
		/// </summary>
		public System.DateTime ConformDate
		{
			get
			{
				return conformDate;
			}
			set
			{
				conformDate = value;
			}
		}
		/// <summary>
		/// ��չ��־
		/// </summary>
		public string ExtChar1
		{
			get
			{
				return extChar1;
			}
			set
			{
				extChar1 = value;
			}
		}
		/// <summary>
		/// ��չ��־
		/// </summary>
		public string ExtChar
		{
			get
			{
				return extChar;
			}
			set
			{
				extChar = value;
			}
		}
		/// <summary>
		/// ��չ��־
		/// </summary>
		public decimal ExtNumber1
		{
			get
			{
				return extNumber1;
			}
			set
			{
				extNumber1 = value;
			}
		}
		/// <summary>
		/// ��չ��־
		/// </summary>
		public decimal ExtNumber
		{
			get
			{
				return extNumber;
			}
			set
			{
				extNumber = value;
			}
		}
		/// <summary>
		/// ��չ��־
		/// </summary>
		public string ExtFlag1
		{
			get
			{
				return extFlag1;
			}
			set
			{
				extFlag1 = value;
			}
		}
		/// <summary>
		/// ��չ��־
		/// </summary>
		public string ExtFlag
		{
			get
			{
				return extFlag;
			}
			set
			{
				extFlag = value;
			}
		}
		/// <summary>
		/// ���к�
		/// </summary>
		public string SequenceNo
		{
			get
			{
				return sequenceNo;
			}
			set
			{
				sequenceNo = value;
			}
		}

		/// <summary>
		/// ��Ϻ�
		/// </summary>
		public string ComNo
		{
			get
			{
				return comNO;
			}
			set
			{
				comNO = value;
			}
		}
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public Neusoft.HISFC.Object.Fee.Item.Undrug Item
		{
			get
			{
				if(item == null)
				{
					item  = new Neusoft.HISFC.Object.Fee.Item.Undrug();
				}
				return item;
			}
			set
			{
				item = value;
			}
		}
		/// <summary>
		/// ִ�п���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject ExecDept
		{
			get
			{
				if(execDept == null)
				{
					execDept = new Neusoft.NFC.Object.NeuObject();
				}
				return execDept;
			}
			set
			{
				execDept = value;
			}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		public  Neusoft.NFC.Object.NeuObject Oper
		{
			get
			{
				if(oper == null)
				{
					oper = new Neusoft.NFC.Object.NeuObject();
				}
				return oper;
			}
			set
			{
				oper = value;
			}
		}
		/// <summary>
		/// �Żݱ���
		/// </summary>
		public decimal EcoRate
		{
			get
			{
				return ecoRate;
			}
			set
			{
				ecoRate = value;
			}
		}
		//		/// <summary>
		//		/// ����
		//		/// </summary>
		//		public int Amount
		//		{
		//			get
		//			{
		//				return amount;
		//			
		//			}
		//			set
		//			{
		//				amount  = value;
		//			}
		//
		//		}
				/// <summary>
				/// ȷ����
				/// </summary>
		public Neusoft.NFC.Object.NeuObject ConformOper
		{
			get
			{
				if(conformOper == null)
				{
					conformOper = new Neusoft.NFC.Object.NeuObject();
				}
				return conformOper;
			}
			set
			{
				conformOper =  value;
			}
		}
		/// <summary>
		/// ���￨��
		/// </summary>
		public string CardNo
		{
			get
			{
				return cardNo;
			}
			set
			{
				cardNo = value;
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string ClinicNo
		{
			get
			{
				return clinicNo;
			}
			set
			{
				clinicNo = value;
			}
		}
		/// <summary>
		/// ��λ��ʶ 0ҩƷ/1 ��ҩƷ/2����/3������Ŀ
		/// </summary>
		public string UnitFlag
		{
			get
			{
				return unitFlag;
			}
			set
			{
				unitFlag = value;
			}
		}

		public new ChkItemList Clone()
		{
			ChkItemList obj = base.Clone() as ChkItemList;
			obj.item= this.item.Clone();
			obj.ExecDept=this.ExecDept.Clone();//(Neusoft.HISFC.Object.Fee.Invoice)Invoice.Clone();
			obj.Oper=this.Oper.Clone();
			obj.ConformOper = this.ConformOper.Clone();
			return obj;
		}
		
	}
}
