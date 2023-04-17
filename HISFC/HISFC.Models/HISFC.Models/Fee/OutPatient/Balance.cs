using System;

namespace FS.HISFC.Models.Fee.Outpatient
{
	/// <summary>
	/// Balance<br></br>
	/// [��������: ��������� ID:����������ˮ�� Name:��������]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class Balance : BalanceBase 
	{
		#region ����

        public Balance() 
        {
            this.Patient = new FS.HISFC.Models.Registration.Register();  
        }

		/// <summary>
		/// �Һ�����
		/// </summary>
		private DateTime regTime;
		
		/// <summary>
		/// ����־ 0�������/1�������/2������� 
		/// </summary>
		private string examineFlag;
		
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNO
		/// </summary>
		private string combNO;
		
		/// <summary>
		/// ��Ʊ�ϵĴ�ӡ��
		/// </summary>
		private string printedInvoiceNO;
		
		/// <summary>
		/// ��ҩ������Ϣ
		/// </summary>
		private string drugWindowsNO;
		
		/// <summary>
		/// �ս��ʶ��
		/// </summary>
		private string balanceID;

        /// <summary>
        /// �Ƿ��˻�֧��
        /// </summary>
        private bool isAccount = false;
		#endregion

		#region ����
		
		/// <summary>
		/// �Һ�����
		/// </summary>
		public DateTime RegTime
		{
			get
			{
				return this.regTime;
			}
			set
			{
				this.regTime = value;
			}
		}

		/// <summary>
		/// ����־ 0�������/1�������/2������� 
		/// </summary>
		public string ExamineFlag
		{
			get
			{
				return this.examineFlag;
			}
			set
			{
				this.examineFlag = value;
			}
		}
		
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNO
		/// </summary>
		public string CombNO
		{
			get
			{
				return this.combNO;
			}
			set
			{
				this.combNO = value;
			}
		}
		
		/// <summary>
		/// ��Ʊ�ϵĴ�ӡ��
		/// </summary>
		public string PrintedInvoiceNO
		{
			get
			{
				return this.printedInvoiceNO;
			}
			set
			{
				this.printedInvoiceNO = value;
			}
		}
		
		/// <summary>
		/// ��ҩ������Ϣ
		/// </summary>
		public string DrugWindowsNO
		{
			get
			{
				return this.drugWindowsNO;
			}
			set
			{
				this.drugWindowsNO = value;
			}
		}

		/// <summary>
		/// �ս��ʶ��
		/// </summary>
		public string BalanceID
		{
			get
			{
				return this.balanceID;
			}
			set
			{
				this.balanceID = value;
			}
		}
        /// <summary>
        /// �Ƿ��˻����д�ӡ��Ʊ
        /// </summary>
        public bool IsAccount
        {
            get
            {
                return isAccount;
            }
            set
            {
                isAccount = value;
            }
        }
		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ��</returns>
		public new Balance Clone()
		{
			return base.Clone() as Balance;
		} 

		#endregion

		#endregion
		
		#region ���ñ���,����

		private string cardNo;//���߾��￨��
		/// <summary>
		/// ���߾��￨��
		/// </summary>
		[Obsolete("����,�Ѿ��̳�Patinet.PID.CardNO", true)]
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

		private DateTime regDate;
		/// <summary>
		/// �Һ�����
		/// </summary>
		[Obsolete("����,RegTime����", true)]
		public DateTime RegDate
		{
			get
			{
				return regDate;
			}
			set
			{
				regDate = value;
			}
		}
		private string patientName = "";
		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("����,Base.Name", true)]
		public string PatientName 
		{
			get
			{
				return patientName;
			}
			set
			{
				patientName = value;
			}
		}
		private FS.HISFC.Models.Base.PayKind payKind = new FS.HISFC.Models.Base.PayKind();
		[Obsolete("����,�Ѿ��̳�this.Patient.Pact.PayKind", true)]
		public FS.HISFC.Models.Base.PayKind PayKind 
		{
			get
			{
				return payKind;
				
			}
			set
			{
				payKind = value;
			}
		}

		private FS.FrameWork.Models.NeuObject pactUnit = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		[Obsolete("����,�Ѿ��̳�this.Patient.Pact", true)]
		public FS.FrameWork.Models.NeuObject PactUnit 
		{
			get
			{
				return pactUnit;
			}
			set
			{
				pactUnit = value;
			}
		}

		private FS.FrameWork.Models.NeuObject medicalType = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ҽ�����
		/// </summary>
		[Obsolete("should use Pact property",true)]
		public FS.FrameWork.Models.NeuObject MedicalType
		{
			get
			{
				return medicalType;
			}
			set
			{
				medicalType = value;
			}
		}
		
		private DateTime balanceDate;
		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("BalanceOper.OperTime",true)]
		public DateTime BalanceDate 
		{
			get
			{
				return balanceDate;
			}
			set
			{
				balanceDate = value;
			}
		}
		private FS.FrameWork.Models.NeuObject balanceOper = new FS.FrameWork.Models.NeuObject();

		
		private FS.HISFC.Models.Base.CancelTypes cancelFlag;
		//private bool isCancel = false;
		/// <summary>
		/// ���ϱ�־
		/// </summary>
		[Obsolete("CancelType",true)]
		public FS.HISFC.Models.Base.CancelTypes CancelFlag
		{
			get
			{
				return cancelFlag;
			}
			set
			{
				cancelFlag = value;
			}
		}

		private string cancelInvoice = "";
		/// <summary>
		/// ����Ʊ�ݺ�
		/// </summary>
		[Obsolete("canceledInvoiceNO",true)]
		public string CancelInvoice 
		{
			get
			{
				return cancelInvoice;
			}
			set
			{
				cancelInvoice = value;
			}
		}

		private DateTime cancelDate;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("CancelOper.OperTime",true)]
		public DateTime CancelDate 
		{
			get
			{
				return cancelDate;
			}
			set
			{
				cancelDate = value;
			}
		}

		private bool isCheck;
		/// <summary>
		/// �Ƿ�˲�
		/// </summary>
		[Obsolete("IsAuditing",true)]
		public bool IsCheck 
		{
			get
			{
				return isCheck;
			}
			set
			{
				isCheck = value;
			}
		}

		private FS.FrameWork.Models.NeuObject checkOper = new FS.FrameWork.Models.NeuObject() ;
		/// <summary>
		/// �˲���
		/// </summary>
		[Obsolete("AuditingOper.Oper",true)]
		public FS.FrameWork.Models.NeuObject CheckOper 
		{
			get
			{
				return checkOper;
			}
			set
			{
				checkOper = value;
			}
		}

		private string checkDate = "";
		/// <summary>
		/// �˲�ʱ��
		/// </summary>
		[Obsolete("AuditingOper.OperTime",true)]
		public string CheckDate 
		{
			get
			{
				return checkDate;
			}
			set
			{
				checkDate = value;
			}
		}

		private DateTime dayBalanceDate ;
		/// <summary>
		/// �ս�ʱ��
		/// </summary>
		[Obsolete("DayBalanceOper.OperTime",true)]
		public DateTime DayBalanceDate 
		{
			get
			{
				return dayBalanceDate;
			}
			set
			{
				dayBalanceDate = value;
			}
		}
		
		private bool isBalanced;
		/// <summary>
		/// �Ƿ��ս�
		/// </summary>
		[Obsolete("IsDayBalanced",true)]
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
		
		
		private DateTime printDisplayDate;//��Ʊ��ʾ�Ĵ�ӡʱ��
		/// <summary>
		/// ��Ʊ��ʾ��ӡʱ��
		/// </summary>
		[Obsolete("PrintTime",true)]
		public DateTime PrintDisplayDate
		{
			get
			{
				return printDisplayDate;
			}
			set
			{
				printDisplayDate = value;
			}
		}
		private string clinicNO;//�Һ���ˮ��
		/// <summary>
		/// �Һ���ˮ��
		/// </summary>
		[Obsolete("Base.ID",true)]
		public string ClinicNO
		{
			get
			{
				return clinicNO;
			}
			set
			{
				clinicNO = value;
			}
		}
		
		#endregion
	}
}
