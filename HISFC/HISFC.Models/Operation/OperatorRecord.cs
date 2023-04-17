using System;


namespace Neusoft.HISFC.Object.Operation 
{
	/// <summary>
	/// �����Ǽ�ʵ����
	/// </summary>
	public class OperatorRecord : Neusoft.NFC.Object.NeuObject
	{
		public OperatorRecord()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		///<summary>
		/// �������뵥����(�����˾��󲿷�Ҫ�Ǽǵ���Ϣ)
		/// </summary>
		///  <link>aggregation</link>
		public OpsApplication m_objOpsApp = new OpsApplication();

		#region ���������Ǽ���Ϣ
		#region ����ʱ��


		private DateTime opsDate = DateTime.MinValue;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OpsDate
		{
			get
			{
				return this.opsDate;
			}
			set
			{
				this.opsDate = value;
			}
		}

		private DateTime acceptDate = DateTime.MinValue;

		[System.Obsolete("AcceptDate",true)]
		public DateTime ReceptDate = DateTime.MinValue;
		/// <summary>
		/// �ӻ���ʱ��
		/// </summary>
		public DateTime AcceptDate
		{
			get
			{
				return this.acceptDate;
			}
			set
			{
				this.acceptDate = value;
			}
		}

		private DateTime enterDate = DateTime.MinValue;
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public DateTime EnterDate
		{
			get
			{
				return this.enterDate;
			}
			set
			{
				this.enterDate = value;
			}
		}

		private DateTime outDate = DateTime.MinValue;
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public DateTime OutDate
		{
			get
			{
				return this.outDate;
			}
			set
			{
				this.outDate = value;
			}
		}

		private decimal duration = 0;
		/// <summary>
		/// ����ʵ����ʱ
		/// </summary>
		public decimal Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}
		[System.Obsolete("Duration",true)]
		public decimal RealDuation = 0;

		#endregion
		#region ����״��
	
		private bool isSoberIn = false;
		[System.Obsolete("IsSoberIn",true)]
		public bool bForeSober
		{
			get
			{
				return this.isSoberIn;
			}
			set
			{
				this.isSoberIn = value;
			}
		}
		/// <summary>
		/// ��ǰ��ʶ�Ƿ�����
		/// </summary>
		public bool IsSoberIn
		{
			get
			{
				return this.isSoberIn;
			}
			set
			{
				this.isSoberIn = value;
			}
		}


		private bool isSoberOut = false;
		[System.Obsolete("IsSoberOut",true)]
		public bool bStepSober
		{
			get
			{
				return this.isSoberOut;
			}
			set
			{
				this.isSoberOut = value;
			}
		}
		/// <summary>
		/// ������ʶ�Ƿ�����
		/// </summary>
		public bool IsSoberOut
		{
			get
			{
				return this.isSoberOut;
			}
			set
			{
				this.isSoberOut = value;
			}
		}

		private string bloodPressureIn = string.Empty;
		/// <summary>
		/// ��ǰѪѹ
		/// </summary>
		public string BloodPressureIn
		{
			get
			{
				return this.bloodPressureIn;
			}
			set
			{
				this.bloodPressureIn = value;
			}
		}
		[System.Obsolete("BloodPressureIn",true)]
		public string ForePress = "";

		private string bloodPressureOut = string.Empty;
		/// <summary>
		/// ����Ѫѹ
		/// </summary>
		public string BloodPressureOut
		{
			get
			{
				return this.bloodPressureOut;
			}
			set
			{
				this.bloodPressureOut = value;
			}
		}

		public string StepPress = "";

		private decimal pulseIn = 0;
		/// <summary>
		/// ��ǰ����
		/// </summary>
		public decimal PulseIn
		{
			get
			{
				return this.pulseIn;
			}
			set
			{
				this.pulseIn = value;
			}
		}
		public decimal ForePulse = 0;

		private decimal pulseOut = 0;
		/// <summary>
		/// ��������
		/// </summary>
		public decimal PulseOut
		{
			get
			{
				return this.pulseOut;
			}
			set
			{
				this.pulseOut = value;
			}
		}
		public decimal StepPulse = 0;

		private int bedsoreCount = 0;
		/// <summary>
		/// �촯����
		/// </summary>
		public int BedsoreCount
		{
			get
			{
				return this.bedsoreCount;
			}
			set
			{
				this.bedsoreCount = value;
			}
		}
		[System.Obsolete("BedsoreCount",true)]
		public int ScarNum = 0;

		private int educeFlowTubeCount = 0;
		/// <summary>
		/// �����ܸ��� 
		/// </summary>
		public int EduceFlowTubeCount
		{
			get
			{
				return this.educeFlowTubeCount;
			}
			set
			{
				this.educeFlowTubeCount = value;
			}
		}
		[System.Obsolete("EduceFlowTubeCount",true)]
		public int GuidtubeNum = 0;
		
		private int sampleCount = 0;
		/// <summary>
		/// �걾��
		/// </summary>
		public int SampleCount
		{
			get
			{
				return this.sampleCount;
			}
			set
			{
				this.sampleCount = value;
			}
		}
		[System.Obsolete("SampleCount",true)]
		public int SampleQty = 0;

		private bool isSeperated;
		[System.Obsolete("IsSeperated",true)]
		public bool bSeperate
		{
			get
			{
				return this.isSeperated;
			}
			set
			{
				this.isSeperated = value;
			}
		}
		/// <summary>
		/// �Ƿ����
		/// </summary>
		public bool IsSeperated
		{
			get
			{
				return this.isSeperated;
			}
			set
			{
				this.isSeperated = value;
			}
		}
		
		private bool isDangerous;
		/// <summary>
		/// �Ƿ�Σ��
		/// </summary>
		[System.Obsolete("IsDangerous",true)]
		public bool bDanger
		{
			get
			{
				return this.isDangerous;
			}
			set
			{
				this.isDangerous = value;
			}
		}
		public bool IsDangerous
		{
			get
			{
				return this.isDangerous;
			}
			set
			{
				this.isDangerous = value;
			}
		}
		#endregion
		#region ��ǰ׼��
		/// <summary>
		/// ��ǰ׼��״̬ ���á��һ��ȣ�
		/// </summary>
		public Neusoft.NFC.Object.NeuObject BeforeReady = new Neusoft.NFC.Object.NeuObject();

		private bool isToolExamed;
		public bool bToolExam
		{
			get
			{
				return this.isToolExamed;
			}
			set
			{
				this.isToolExamed = value;
			}
		}
		/// <summary>
		/// ���ߺ˶�
		/// </summary>
		public bool IsToolExamed
		{
			get
			{
				return this.isToolExamed;
			}
			set
			{
				this.isToolExamed = value;
			}
		}
		#endregion
		#region ����״��
		/// <summary>
		/// ��Ѫ����
		/// </summary>
		[System.Obsolete("PhlebotmomizeTimes",true)]
		public int LetBlood = 0;
		/// <summary>
		/// ��ע����
		/// </summary>
		[System.Obsolete("VeinInjectionTimes",true)]
		public int MainLine = 0;
		/// <summary>
		/// ��ע����
		/// </summary>
		[System.Obsolete("MuscleInjectionTimes",true)]
		public int MusleLine = 0;
		/// <summary>
		/// ��Һ����
		/// </summary>
		[System.Obsolete("TransfusionTimes",true)]
		public int TransFusion = 0;
		/// <summary>
		/// ��Һ��
		/// </summary>
		[System.Obsolete("TransfusionQuantity",true)]
		public decimal TransFusionQty = 0;
		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("TransoxygenTimes",true)]
		public int TransOxyen = 0;
		/// <summary>
		/// �������
		/// </summary>
		[System.Obsolete("ExportUrineTimes",true)]
		public int Stale = 0;
		/// <summary>
		/// �Ƿ���
		/// </summary>
		private string Question = "";
		[System.Obsolete("IsMistaken",true)]
		public bool bQuestion
		{
			get
			{
				if(Question == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value)
					Question = "1";
				else 
					Question = "0";
			}
		}
		/// <summary>
		/// I���пڸ�Ⱦ
		/// </summary>
		private string I_Infection = "";
		[System.Obsolete("IsInfected",true)]
		public bool bI_Infection
		{
			get
			{
				if(I_Infection == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value)
					I_Infection = "1";
				else 
					I_Infection = "0";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		private string Die = "";
		[System.Obsolete("IsDead",true)]
		public bool bDie
		{
			get
			{
				if(Die == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value)
					Die = "1";
				else 
					Die = "0";
			}
		}
//		/// <summary>
//		/// ����˵��
//		/// </summary>
//		[System.Obsolete("Memo",true)]
//		public string SpecialComment = "";
	
		private int phlebotomizeTimes = 0;
		/// <summary>
		/// ��Ѫ����
		/// </summary>
		public int PhlebotmomizeTimes
		{
			get
			{
				return this.phlebotomizeTimes;
			}
			set
			{
				this.phlebotomizeTimes = value;
			}
		}

		private int veinInjectionTimes = 0;
		/// <summary>
		/// ��ע����
		/// </summary>
		public int VeinInjectionTimes
		{
			get
			{
				return this.veinInjectionTimes;
			}
			set
			{
				this.veinInjectionTimes = value;
			}
		}

		private int muscleInjectionTimes = 0;
		/// <summary>
		/// ��ע����
		/// </summary>
		public int MuscleInjectionTimes
		{
			get
			{
				return this.muscleInjectionTimes;
			}
			set
			{
				this.muscleInjectionTimes = value;
			}
		}

		
		private int transfusionTimes = 0;
		/// <summary>
		/// ��Һ����
		/// </summary>
		public int TransfusionTimes
		{
			get
			{
				return this.transfusionTimes;
			}
			set
			{
				this.transfusionTimes = value;
			}
		}
		
		private decimal transfusionQuantity = 0;
		/// <summary>
		/// ��Һ��
		/// </summary>
		public decimal TransfusionQuantity
		{
			get
			{
				return this.transfusionQuantity;
			}
			set
			{
				this.transfusionQuantity = value;
			}
		}

		private int transoxygenTimes = 0;
		/// <summary>
		/// ��������
		/// </summary>
		public int TransoxygenTimes
		{
			get
			{
				return this.transoxygenTimes;
			}
			set
			{
				this.transoxygenTimes = value;
			}
		}


		private int exportUrieTimes = 0;
		/// <summary>
		/// �������
		/// </summary>
		public int ExportUrineTimes
		{
			get
			{
				return this.exportUrieTimes;
			}
			set
			{
				this.exportUrieTimes = value;
			}
		}


		private bool isMistaken;
		/// <summary>
		/// �Ƿ���
		/// </summary>
		public bool IsMistaken
		{
			get
			{
				return this.isMistaken;
			}
			set
			{
				this.isMistaken = value;
			}
		}

		private bool isInfected;
		/// <summary>
		/// I���п��Ƿ��Ⱦ
		/// </summary>
		public bool IsInfected
		{
			get
			{
				return this.isInfected;
			}
			set
			{
				this.isInfected = value;
			}
		}

		private bool isDead;
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public bool IsDead
		{
			get
			{
				return this.isDead;
			}
			set
			{
				this.isDead = value;
			}
		}


		private string extraMemo = string.Empty;
		/// <summary>
		/// ����˵��
		/// </summary>
		public string ExtraMemo
		{
			get
			{
				return this.extraMemo;
			}
			set
			{
				this.extraMemo = value;
			}
		}
		[System.Obsolete("ExtraMemo",true)]
		public string SpecialComment = "";
				#endregion	
		#region ������־
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		private bool isValid;
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

		/// <summary>
		/// �Ƿ��շ�
		/// </summary>
		private bool isCharged;
		public bool IsCharged
		{
			get
			{
				return this.isCharged;
			}
			set
			{
				this.isCharged = value;
			}
		}

		private DateTime operDate = DateTime.MinValue;
		/// <summary>
		/// ����Ա�Ǽǲ�������---Add By Maokb
		/// </summary>
		public DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}
		#endregion
		/// <summary>
		/// ��ע
		/// </summary>
		[System.Obsolete("Memo",true)]
		public string Remark = "";
		#endregion
	}
}
