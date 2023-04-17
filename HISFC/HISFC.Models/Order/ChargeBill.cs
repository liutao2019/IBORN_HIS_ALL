using System;

namespace FS.HISFC.Models.Fee.Inpatient
{
	/// <summary>
	/// FS.HISFC.Models.Fee.Inpatient.ChargeBill<br></br>
	/// [��������: �����շѵ�]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///		/>
	/// </summary>
    [Serializable]
    public class ChargeBill:FS.FrameWork.Models.NeuObject,Base.IBaby,Base.IDept
	{
		public ChargeBill()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����
		
		/// <summary>
		/// �շѵ��ݺ�
		/// </summary>
		private string billNO;

		/// <summary>
		/// ҽ��ID
		/// </summary>
		private string orderID;

		/// <summary>
		/// ҽ��ִ��ID
		/// </summary>
		private string execOrderID;
		
		/// <summary>
		/// ҽ�����ʵ��
		/// </summary>
		private Order.Combo combo = new FS.HISFC.Models.Order.Combo();

		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		private string inpatientNO;

		/// <summary>
		/// �Ƿ�Ӥ���շ�
		/// </summary>
		private bool isBaby = false;

		/// <summary>
		/// Ӥ�����
		/// </summary>
		private string babyNO;

		/// <summary>
		/// �Ƿ��ӡ
		/// </summary>
		private bool isPrint = false;

		/// <summary>
		/// �Ƿ��շ�
		/// </summary>
		private bool isCharge = false;

		/// <summary>
		/// ʹ��ʱ��
		/// </summary>
		private DateTime dtuseTime;

		/// <summary>
		/// ��������  C ��ͨ���� Z �Է�ҩ���� T ������Ŀ���� D ������� XS ����ҩ���� M1 ҽ���׳��� M2 ҽ���ҳ��� M3 ҽ���Է�ҩ����
		/// </summary>
		private string outputType = "C";

		
		/// <summary>
		/// �۸�
		/// </summary>
		private decimal price;
		/// <summary>
		/// ����
		/// </summary>
		private decimal qty;
		/// <summary>
		/// ��λ
		/// </summary>
		private string priceUnit;
		/// <summary>
		/// ���
		/// </summary>
		private string specs;
		/// <summary>
		/// �Ƿ�ҩƷ
		/// </summary>
		private bool isPharmacy;
		/// <summary>
		/// ����
		/// </summary>
		private int useTimes;
		/// <summary>
		/// �ܶ�
		/// </summary>
		private decimal totCost;
		
		
		/// <summary>
		/// סԺ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject inDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��ʿվ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject nurseID = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��������
		/// </summary>
		private FS.FrameWork.Models.NeuObject recipeDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ִ�п���
		/// </summary>
		private FS.FrameWork.Models.NeuObject execDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ȡҩҩ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject storeDept = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// ����ҽ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctorDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// �շѲ���
		/// </summary>
		private Base.OperEnvironment reciptOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ������¼
		/// </summary>
		private Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��ӡ����
		/// </summary>
		private Base.OperEnvironment printOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ����ҽ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// Ƶ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject frequency = new FS.FrameWork.Models.NeuObject();

		#region ������Ϣ
		/// <summary>
		/// ������
		/// </summary>
		private string recipeNO;

		/// <summary>
		/// ��������ˮ��
		/// </summary>
		private int sequenceNO;

		#endregion

		#endregion

		#region ����

		/// <summary>
		/// �շѵ���
		/// </summary>
		public string BillNO
		{
			get
			{
				return this.billNO;
			}
			set
			{
				this.billNO = value;
			}
		}

		/// <summary>
		/// ҽ����
		/// </summary>
		public string OrderID
		{
			get
			{
				return this.orderID;
			}
			set
			{
				this.orderID = value;
			}
		}

		/// <summary>
		/// ҽ��ִ�е����
		/// </summary>
		public string ExecOrderID
		{
			get
			{
				return this.execOrderID;
			}
			set
			{
				this.execOrderID = value;
			}
		}
		
		/// <summary>
		/// ���
		/// </summary>
		public Order.Combo Combo
		{
			get
			{
				return this.combo;
			}
			set
			{
				this.combo = value;
			}
		}

		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		public string InpatientNO
		{
			get
			{
				return this.inpatientNO;
			}
			set
			{
				this.inpatientNO = value;
			}
		}

		/// <summary>
		/// �Ƿ��ӡ
		/// </summary>
		public bool IsPrint
		{
			get
			{
				return this.isPrint;
			}
			set
			{
				this.isPrint = value;
			}
		}
		/// <summary>
		/// �Ƿ��շ�
		/// </summary>
		public bool IsCharge
		{
			get
			{
				return this.isCharge;
			}
			set
			{
				this.isCharge = value;
			}
		}

		/// <summary>
		/// ʹ��ʱ��
		/// </summary>
		public DateTime UseTime
		{
			get
			{
				return this.dtuseTime;
			}
			set
			{
				this.dtuseTime = value;
			}
		}

		/// <summary>
		/// ��������  C ��ͨ���� Z �Է�ҩ���� T ������Ŀ���� D ������� XS ����ҩ���� M1 ҽ���׳��� M2 ҽ���ҳ��� M3 ҽ���Է�ҩ����
		/// Ĭ�� ��ͨ����
		/// </summary>
		public string OutputType
		{
			get
			{
				return this.outputType;
			}
			set
			{
				this.outputType = value;
			}
		}

		/// <summary>
		/// �۸�
		/// </summary>
		public decimal Price
		{
			get
			{
				return this.price;
			}
			set
			{
				this.price = value;
			}
		}
		/// <summary>
		/// ����
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
		/// ��λ
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
		/// ���
		/// </summary>
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
		/// �Ƿ�ҩƷ
		/// </summary>
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
		/// ����
		/// ʹ�ô���
		/// </summary>
		public int HerbalQty
		{
			get
			{
				return this.useTimes;
			}
			set
			{
				this.useTimes = value;
			}
		}
		/// <summary>
		/// �ܶ�
		/// </summary>
		public decimal TotCost
		{
			get
			{
				return this.totCost;
			}
			set
			{
				this.totCost = value;
			}
		}

		/// <summary>
		/// �շѲ���
		/// </summary>
		public Base.OperEnvironment ReciptOper 
		{
			get
			{
				return this.reciptOper;
			}
			set
			{
				this.reciptOper = value;
			}
		}

		/// <summary>
		/// ������¼
		/// </summary>
		public Base.OperEnvironment Oper
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
		/// ��ӡ����
		/// </summary>
		public Base.OperEnvironment PrintOper
		{
			get
			{
				return this.printOper;
			}
			set
			{
				this.printOper = value;
			}
		}

		/// <summary>
		/// ����ҽ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Doctor
		{
			get
			{
				return this.doctor;
			}
			set
			{
				this.doctor = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string ReciptNO
		{
			get
			{
				return this.recipeNO;
			}
			set
			{
				this.recipeNO = value;
			}
		}
		
		/// <summary>
		/// ��������ˮ��
		/// </summary>
		public int SequenceNO
		{
			get
			{
				return this.sequenceNO;
			}
			set
			{
				this.sequenceNO = value;
			}
		}

		/// <summary>
		/// Ƶ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Frequency
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
		#endregion

		#region ���ϵ�

		/// <summary>
		/// ��Ŀ���к�,����
		/// </summary>
		[Obsolete("��ID������",true)]
		public string BillID;

		[Obsolete("��BillNO������",true)]
		public string BillNo;

		[Obsolete("��ExecOrderID������",true)]
		public string ExecID;

		[Obsolete("��Combo.ID������",true)]
		public string CombNo;

		[Obsolete("��InpatientNO������",true)]
		public string InpatientNo;

		[Obsolete("��IsBaby������",true)]
		public bool IsBabyCharge;

		[Obsolete("��UseTime������",true)]
		public DateTime useTime;

		[Obsolete("��HerbalQty������",true)]
		public int Days;

		[Obsolete("��ReciptNO������",true)]
		public string RecipeNo;

		[Obsolete("��Doctor.ID������",true)]
		public string DoctID;
		
		[Obsolete("��Oper.Oper.ID������",true)]
		public string RecordID;
		
		[Obsolete("��PrintOper.Oper.ID������",true)]
		public string PrintID;
		
		[Obsolete("��ChargeOper.Oper.ID������",true)]
		public string ChargeID;

		[Obsolete("��Oper.OperTime������",true)]
		public DateTime RecordDate;

		[Obsolete("��PrintOper.OperTime������",true)]
		public DateTime PrintDate;
		
		[Obsolete("��ChargeOper.OperTime������",true)]
		public DateTime ChargeDate;

		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new ChargeBill Clone()
		{
			ChargeBill obj=base.Clone() as ChargeBill;		
			obj.combo = this.combo.Clone();
			obj.oper = this.oper.Clone();
			obj.printOper = this.printOper.Clone();
			obj.reciptOper = this.reciptOper.Clone();
			return obj;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region IBaby ��Ա

		/// <summary>
		/// Ӥ�����
		/// </summary>
		public string BabyNO
		{
			get
			{
				// TODO:  ��� ChargeBill.BabyNO getter ʵ��
				return this.babyNO;
			}
			set
			{
				// TODO:  ��� ChargeBill.BabyNO setter ʵ��
				this.babyNO = value;
			}
		}

		/// <summary>
		/// �Ƿ�Ӥ��
		/// </summary>
		public bool IsBaby
		{
			get
			{
				// TODO:  ��� ChargeBill.IsBaby getter ʵ��
				return this.isBaby;
			}
			set
			{
				// TODO:  ��� ChargeBill.IsBaby setter ʵ��
				this.isBaby = value;
			}
		}

		#endregion

		#region IDept ��Ա
		
		/// <summary>
		/// ������Ժ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject InDept
		{
			get
			{
				// TODO:  ��� ChargeBill.InDept getter ʵ��
				return this.inDept;
			}
			set
			{
				// TODO:  ��� ChargeBill.InDept setter ʵ��
				this.inDept = value;
			}
		}
		
		/// <summary>
		/// ִ�п���
		/// </summary>
		public FS.FrameWork.Models.NeuObject ExeDept
		{
			get
			{
				// TODO:  ��� ChargeBill.ExeDept getter ʵ��
				return this.execDept;
			}
			set
			{
				// TODO:  ��� ChargeBill.ExeDept setter ʵ��
				this.execDept = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public FS.FrameWork.Models.NeuObject ReciptDept
		{
			get
			{
				// TODO:  ��� ChargeBill.ReciptDept getter ʵ��
				return this.recipeDept;
			}
			set
			{
				// TODO:  ��� ChargeBill.ReciptDept setter ʵ��
				this.recipeDept = value;
			}
		}

		/// <summary>
		/// �����ڻ�ʿվ
		/// </summary>
		public FS.FrameWork.Models.NeuObject NurseStation
		{
			get
			{
				// TODO:  ��� ChargeBill.NurseStation getter ʵ��
				return this.nurseID;
			}
			set
			{
				// TODO:  ��� ChargeBill.NurseStation setter ʵ��
				this.nurseID = value;
			}
		}

		/// <summary>
		/// �ۿ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject StockDept
		{
			get
			{
				// TODO:  ��� ChargeBill.StockDept getter ʵ��
				return this.storeDept;
			}
			set
			{
				// TODO:  ��� ChargeBill.StockDept setter ʵ��
				this.storeDept = value;
			}
		}

		/// <summary>
		/// ����ҽ�����ڿ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject DoctorDept
		{
			get
			{
				// TODO:  ��� ChargeBill.DoctorDept getter ʵ��
				return this.doctorDept;
			}
			set
			{
				// TODO:  ��� ChargeBill.DoctorDept setter ʵ��
				this.doctorDept = value;
			}
		}

		#endregion

		#endregion
	}
}
