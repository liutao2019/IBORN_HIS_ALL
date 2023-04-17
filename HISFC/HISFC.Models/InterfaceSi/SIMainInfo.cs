using System;
using neusoft.neuFC.Object;

namespace neusoft.HISFC.Object.InterfaceSi
{
	/// <summary>
	/// SIMainInfo ��ժҪ˵����
	/// Id inpatientNo, name ��������
	/// </summary>
	public class SIMainInfo:neusoft.neuFC.Object.neuObject
	{
		public SIMainInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private int feeTimes;
		/// <summary>
		/// ��������
		/// </summary>
		public int FeeTimes
		{
			set
			{
				feeTimes = value;
			}
			get
			{
				return feeTimes;
			}
		}
		private int readFlag;
		/// <summary>
		/// �����־
		/// </summary>
		public int ReadFlag
		{
			get
			{
				return readFlag;
			}
			set
			{
				readFlag = value;
			}
		}

		private string regNo;
		/// <summary>
		/// ����ǼǺš���·ҽ�����˱��
		/// </summary>
		public string RegNo
		{
			set
			{
				regNo = value;
			}
			get
			{
				return regNo;
			}
		}

		private string hosNo;
		/// <summary>
		/// ҽԺ���
		/// </summary>
		public string HosNo
		{
			set{hosNo = value;}
			get{return hosNo;}
		}

		private string balNo;
		/// <summary>
		///  �������
		/// </summary>
		public string BalNo
		{
			get
			{
				if(balNo == null || balNo == "")
				{
					balNo = "0";
				}
				return balNo;
			}
			set{balNo = value;}
		}
		private string invoiceNo;
		/// <summary>
		/// ����Ʊ��
		/// </summary>
		public string InvoiceNo
		{
			get{return invoiceNo;}
			set{invoiceNo = value;}
		}
		private neusoft.neuFC.Object.neuObject medicalType = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ҽ����� 1-סԺ 2 -�����ض���Ŀ
		/// </summary>
		public neusoft.neuFC.Object.neuObject MedicalType
		{
			get{return medicalType;}
			set{medicalType = value;}
		}
//		private string patientNo;
//		/// <summary>
//		/// סԺ��
//		/// </summary>
//		public string PatientNo
//		{
//			get{return patientNo;}
//			set{patientNo = value;}
//		}
//		private string cardNo;
//		/// <summary>
//		/// ���￨��
//		/// </summary>
//		public string CardNo
//		{
//			get{return cardNo;}
//			set{cardNo = value;}
//		}
//		private string mCardNo;
//		/// <summary>
//		/// ҽ��֤��
//		/// </summary>
//		public string MCardNo
//		{
//			get{return mCardNo;}
//			set{mCardNo = value;}
//		}
		private string proceatePcNo;
		/// <summary>
		/// �������ջ��ߵ��Ժ�
		/// </summary>
		public string ProceatePcNo
		{
			get{return proceatePcNo;}
			set{proceatePcNo = value;}
		}
		private DateTime siBeginDate;
		/// <summary>
		/// �α�����
		/// </summary>
		public DateTime SiBegionDate
		{
			get{return siBeginDate;}
			set{siBeginDate = value;}
		}
		private string siState;
		/// <summary>
		/// �α�״̬ 3-�α��ɷѡ�4-��ͣ�ɷѡ�7-��ֹ�α�
		/// </summary>
		public string SiState
		{
			get{return siState;}
			set{siState = value;}
		}
		private string emplType;
		/// <summary>
		/// ��Ա��� 1-��ְ��2-����
		/// </summary>
		public string EmplType
		{
			get{return emplType;}
			set{emplType = value;}
		}
		private string clinicDiagNose;
		/// <summary>
		/// �������
		/// </summary>
		public string ClinicDiagNose
		{
			get{return clinicDiagNose;}
			set{clinicDiagNose = value;}
		}
		private DateTime inDiagnoseDate;
		/// <summary>
		/// ��Ժ�������
		/// </summary>
		public DateTime InDiagnoseDate
		{
			get{return inDiagnoseDate;}
			set{inDiagnoseDate = value;}
		}
		
		private neusoft.neuFC.Object.neuObject inDiagnose = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��Ժ�����Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject InDiagnose
		{
			get{return inDiagnose;}
			set{inDiagnose = value;}
		}

		private decimal totCost;
		/// <summary>
		/// סԺ�ܽ��
		/// </summary>
		public decimal TotCost
		{
			get{return totCost;}
			set{totCost = value;}
		}
		private decimal payCost;
		/// <summary>
		/// �ʻ�֧�����
		/// </summary>
		public decimal PayCost
		{
			get{return payCost;}
			set{payCost = value;}
		}

		private decimal pubCost;
		/// <summary>
		/// �籣֧�����
		/// </summary>
		public decimal PubCost
		{
			get{return pubCost;}
			set{pubCost = value;}
		}

		private decimal itemPayCost;
		/// <summary>
		/// ������Ŀ�Ը���� 
		/// </summary>
		public decimal ItemPayCost
		{
			get{return itemPayCost;}
			set{itemPayCost = value;}
		}
		private decimal baseCost;
		/// <summary>
		/// �����𸶽��
		/// </summary>
		public decimal BaseCost
		{
			get{return baseCost;}
			set{baseCost = value;}
		}
		private decimal ownCost;
		/// <summary>
		/// �����Է���Ŀ���
		/// </summary>
		public decimal OwnCost
		{
			get{return ownCost;}
			set{ownCost = value;}
		}
		private decimal itemYLCost;
		/// <summary>
		/// �����Ը��������Ը����֣�
		/// </summary>
		public decimal ItemYLCost
		{
			get{return itemYLCost;}
			set{itemYLCost = value;}
		}

		private decimal pubOwnCost;
		/// <summary>
		/// �����Ը����
		/// </summary>
		public decimal PubOwnCost
		{
			set{pubOwnCost = value;}
			get{return pubOwnCost;}
		}

		private decimal overTakeOwnCost;
		/// <summary>
		/// ��ͳ��֧���޶�����Ը����
		/// </summary>
		public decimal OverTakeOwnCost
		{
			get{return overTakeOwnCost;}
			set{overTakeOwnCost = value;}
		}
		
		private decimal hosCost;
		/// <summary>
		/// ҽҩ�����ֵ����
		/// </summary>
		public decimal HosCost
		{
			set
			{
				hosCost = value;
			}
			get
			{
				return hosCost;
			}
		}

		private neusoft.neuFC.Object.neuObject operInfo = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ����Ա��Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperInfo
		{
			get{return operInfo;}
			set{operInfo = value;}
		}
		private DateTime operDate;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			get{return operDate;}
			set{operDate = value;}
		}
		private int appNo;
		/// <summary>
		/// ������
		/// </summary>
		public int AppNo
		{
			get{return appNo;}
			set{appNo = value;}
		}
		private DateTime balanceDate;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime BalanceDate
		{
			get{return balanceDate;}
			set{balanceDate = value;}
		}
		private decimal yearCost;
		/// <summary>
		/// ����ȿ��ö���
		/// </summary>
		public decimal YearCost
		{
			get
			{
				return yearCost;
			}
			set
			{
				yearCost = value;
			}
		}
		private neusoft.neuFC.Object.neuObject outDiagnose = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��Ժ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject OutDiagnose
		{
			set{outDiagnose = value;}
			get{return outDiagnose;}
		}
		
		private bool isValid;
		/// <summary>
		/// �Ƿ���Ч True��Ч False ��Ч
		/// </summary>
		public bool IsValid
		{
			set
			{
				isValid = value;
			}
			get
			{
				return isValid;
			}
		}

		private bool isBalanced;
		/// <summary>
		/// �Ƿ���� True ���� False δ����
		/// </summary>
		public bool IsBalanced
		{
			get
			{
				return isBalanced;
			}
			set
			{
				isBalanced = value;
			}
		}


		#region ��·ҽ����������
		#region ����
		string icCardCode = "";
		neusoft.neuFC.Object.neuObject personType = new neuObject();
		neusoft.neuFC.Object.neuObject civilianGrade = new neuObject();
		neusoft.neuFC.Object.neuObject specialCare = new neuObject();
		string duty = "";
        neusoft.neuFC.Object.neuObject anotherCity = new neuObject();
		neusoft.neuFC.Object.neuObject corporation = new neuObject();
		decimal individualBalance = 0;
		string freezeMessage = "";
		string applySequence = "";
		neusoft.neuFC.Object.neuObject disease = new neuObject();
		neusoft.neuFC.Object.neuObject applyType = new neuObject();
		neusoft.neuFC.Object.neuObject fund = new neuObject();
		string businessSequence = "";
		neusoft.neuFC.Object.neuObject specialWorkKind = new neuObject();
		string hospitalBusinessSequence = "";
		string opositeBusinessSequence = "";
		#endregion
		/// <summary>
		/// IC������
		/// </summary>
		public string ICCardCode
		{
			get
			{
				return this.icCardCode;
			}
			set
			{
				this.icCardCode = value;
			}
		}
		
		/// <summary>
		/// ��Ա���
		/// </summary>
		public neusoft.neuFC.Object.neuObject PersonType
		{
			get
			{
				return this.personType;
			}
			set
			{
				this.personType = value;
			}
		}
		/// <summary>
		/// ����Ա����
		/// </summary>
		public neusoft.neuFC.Object.neuObject CivilianGrade
		{
			get
			{
				return this.civilianGrade;
			}
			set
			{
				this.civilianGrade = value;
			}
		}
		/// <summary>
		/// �����չ���Ⱥ
		/// </summary>
		public neusoft.neuFC.Object.neuObject SpecialCare
		{
			get
			{
				return this.specialCare;
			}
			set
			{
				this.specialCare = value;
			}
		}
		/// <summary>
		/// ְ��
		/// </summary>
		public string Duty
		{
			get
			{
				return this.duty;
			}
			set
			{
				this.duty = value;
			}
		}
		/// <summary>
		/// ��ذ��ó���
		/// </summary>
		public neusoft.neuFC.Object.neuObject AnotherCity
		{
			get
			{
				return this.anotherCity;
			}
			set
			{
				this.anotherCity = value;
			}
		}
		/// <summary>
		/// �α��˵�λ
		/// </summary>
		public neusoft.neuFC.Object.neuObject Corporation
		{
			get
			{
				return this.corporation;
			}
			set
			{
				this.corporation = value;
			}
		}
		/// <summary>
		/// �����ʻ����
		/// </summary>
		public decimal IndividualBalance
		{
			get
			{
				return this.individualBalance;
			}
			set
			{
				this.individualBalance = value;
			}
		}
		/// <summary>
		/// �Ѷ��������Ϣ
		/// </summary>
		public string FreezeMessage
		{
			get
			{
				return this.freezeMessage;
			}
			set
			{
				this.freezeMessage = value;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string ApplySequence
		{
			get
			{
				return this.applySequence;
			}
			set
			{
				this.applySequence = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject Disease
		{
			get
			{
				return this.disease;
			}
			set
			{
				this.disease = value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject ApplyType
		{
			get
			{
				return this.applyType;
			}
			set
			{
				this.applyType = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject Fund
		{
			get
			{
				return this.fund;
			}
			set
			{
				this.fund = value;
			}
		}
		/// <summary>
		/// ҵ�����
		/// </summary>
		public string BusinessSequence
		{
			get
			{
				return this.businessSequence;
			}
			set
			{
				this.businessSequence = value;
			}
		}
		/// <summary>
		/// ���⹤��
		/// </summary>
		public neusoft.neuFC.Object.neuObject SpecialWorkKind
		{
			get
			{
				return this.specialWorkKind;
			}
			set
			{
				this.specialWorkKind = value;
			}
		}
		/// <summary>
		/// ҽԺ�������к�
		/// </summary>
		public string HospitalBusinessSequence
		{
			get
			{
				return this.hospitalBusinessSequence;
			}
			set
			{
				this.hospitalBusinessSequence = value;
			}
		}
		/// <summary>
		/// ��Ӧ�������к�
		/// </summary>
		public string OpositeBusinessSequence
		{
			get
			{
				return this.opositeBusinessSequence;
			}
			set
			{
				this.opositeBusinessSequence = value;
			}
		}

		public new SIMainInfo Clone()
		{
			SIMainInfo obj = base.Clone() as SIMainInfo;
			obj.medicalType = this.medicalType.Clone();
			obj.inDiagnose = this.inDiagnose.Clone();
			obj.operInfo = this.operInfo.Clone();
			obj.PersonType = this.PersonType.Clone();
			obj.CivilianGrade = this.CivilianGrade.Clone();
			obj.SpecialCare = this.SpecialCare.Clone();
			obj.AnotherCity = this.AnotherCity.Clone();
			obj.Corporation = this.Corporation.Clone();
			obj.Disease = this.Disease.Clone();
			obj.ApplyType = this.ApplyType.Clone();
			obj.Fund = this.Fund.Clone();
			obj.SpecialWorkKind = this.SpecialWorkKind.Clone();

			return obj;
		}
		#endregion
	}
}
