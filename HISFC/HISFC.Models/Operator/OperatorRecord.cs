using System;

namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// OperatorRecord ��ժҪ˵����
	/// �����Ǽ�ʵ����
	/// </summary>
	public class OperatorRecord:neusoft.neuFC.Object.neuObject
	{
		public OperatorRecord()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �������뵥����(�����˾��󲿷�Ҫ�Ǽǵ���Ϣ)
		/// </summary>
		public neusoft.HISFC.Object.Operator.OpsApplication m_objOpsApp = new OpsApplication();
		#region ���������Ǽ���Ϣ
		#region ����ʱ��
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OpsDate = DateTime.MinValue;
		/// <summary>
		/// �ӻ���ʱ��
		/// </summary>
		public DateTime ReceptDate = DateTime.MinValue;
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public DateTime EnterDate = DateTime.MinValue;
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public DateTime OutDate = DateTime.MinValue;
		/// <summary>
		/// ����ʵ����ʱ
		/// </summary>
		public decimal RealDuation = 0;
		#endregion
		#region ����״��
		/// <summary>
		/// ��ǰ��ʶ 1����0������
		/// </summary>
		private string ForeYNSober = "";
		public bool bForeSober
		{
			get
			{
				if(ForeYNSober == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					ForeYNSober = "1";
				else
					ForeYNSober = "0";
			}
		}
		/// <summary>
		/// ������ʶ 1����0������
		/// </summary>
		private string StepYNSober = "";
		public bool bStepSober
		{
			get
			{
				if(StepYNSober == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					StepYNSober = "1";
				else
					StepYNSober = "0";
			}
		}
		/// <summary>
		/// ��ǰѪѹ
		/// </summary>
		public string ForePress = "";
		/// <summary>
		/// ����Ѫѹ
		/// </summary>
		public string StepPress = "";
		/// <summary>
		/// ��ǰ����
		/// </summary>
		public decimal ForePulse = 0;
		/// <summary>
		/// ��������
		/// </summary>
		public decimal StepPulse = 0;
		/// <summary>
		/// �촯����
		/// </summary>
		public int ScarNum = 0;
		/// <summary>
		/// �����ܸ��� 
		/// </summary>
		public int GuidtubeNum = 0;
		/// <summary>
		/// �걾��
		/// </summary>
		public int SampleQty = 0;
		/// <summary>
		/// �Ƿ����
		/// </summary>
		private string Seperate = "";
		public bool bSeperate
		{
			get
			{
				if(Seperate == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					Seperate = "1";
				else
					Seperate = "0";
			}
		}
		/// <summary>
		/// �Ƿ�Σ��
		/// </summary>
		private string Danger = "";
		public bool bDanger
		{
			get
			{
				if(Danger == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					Danger = "1";
				else
					Danger = "0";
			}
		}
		#endregion
		#region ��ǰ׼��
		/// <summary>
		/// ��ǰ׼��״̬ ���á��һ��ȣ�
		/// </summary>
		public neusoft.neuFC.Object.neuObject BeforeReady = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ���ߺ˶�
		/// </summary>
		private string ToolExam = "";
		public bool bToolExam
		{
			get
			{
				if(ToolExam == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					ToolExam = "1";
				else
					ToolExam = "0";
			}
		}
		#endregion
		#region ����״��
		/// <summary>
		/// ��Ѫ����
		/// </summary>
		public int LetBlood = 0;
		/// <summary>
		/// ��ע����
		/// </summary>
		public int MainLine = 0;
		/// <summary>
		/// ��ע����
		/// </summary>
		public int MusleLine = 0;
		/// <summary>
		/// ��Һ����
		/// </summary>
		public int TransFusion = 0;
		/// <summary>
		/// ��Һ��
		/// </summary>
		public decimal TransFusionQty = 0;
		/// <summary>
		/// ��������
		/// </summary>
		public int TransOxyen = 0;
		/// <summary>
		/// �������
		/// </summary>
		public int Stale = 0;
		/// <summary>
		/// �Ƿ���
		/// </summary>
		private string Question = "";
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
		/// <summary>
		/// ����˵��
		/// </summary>
		public string SpecialComment = "";
		#endregion		
		#region ������־
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		private string Valid = "";
		public bool bValid
		{
			get
			{
				if(Valid == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value)
					Valid = "1";
				else 
					Valid = "0";
			}
		}
		/// <summary>
		/// �Ƿ��շ�
		/// </summary>
		private string Fee = "";
		public bool bFee
		{
			get
			{
				if(Fee == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value)
					Fee = "1";
				else 
					Fee = "0";
			}
		}
		/// <summary>
		/// ����Ա�Ǽǲ�������---Add By Maokb
		/// </summary>
		public DateTime OperDate = DateTime.MinValue;
		#endregion
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark = "";
		#endregion
	}
}
