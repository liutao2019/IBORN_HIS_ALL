using System;

namespace Neusoft.HISFC.Object.Fee.OutPatient
{
	/// <summary>
	/// Invoice �̳�Neusoft.NFC.Object.NeuObject
	/// ID ��Ʊ��
	/// 
	/// ����: wangYu ��д��: 05/07/02
	/// </summary>
	public class Invoice : Neusoft.NFC.Object.NeuObject 
	{
		public Invoice() 
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private Neusoft.HISFC.Object.Base.TransTypes transType  = new Neusoft.HISFC.Object.Base.TransTypes(); //��������
		/// <summary>
		/// ��������
		/// </summary>
		public Neusoft.HISFC.Object.Base.TransTypes TransType 
		{
			get 
			{
				return transType;
			}
			set 
			{
				transType = value;
			}
		}

		private string cardNo;//���߾��￨��
		/// <summary>
		/// ���߾��￨��
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

		private Neusoft.HISFC.Object.Fee.FT ft = new FT() ;
		/// <summary>
		/// ������Ϣ��
		/// </summary>
		public Neusoft.HISFC.Object.Fee.FT FT
		{
			get
			{
				return ft;
			}
			set
			{
				ft = value;
			}
		}
		private Neusoft.HISFC.Object.RADT.PID pID = new Neusoft.HISFC.Object.RADT.PID();
		/// <summary>
		/// ����ʶ
		/// </summary>
		public Neusoft.HISFC.Object.RADT.PID PID
		{
			get
			{
				return pID;
			}
			set
			{
				pID = value;
			}
		}
		private DateTime regDate;
		/// <summary>
		/// �Һ�����
		/// </summary>
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
		private Neusoft.HISFC.Object.Fee.PayKind payKind = new PayKind();
		/// <summary>
		/// �������
		/// </summary>
		public Neusoft.HISFC.Object.Fee.PayKind PayKind 
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

		private Neusoft.NFC.Object.NeuObject pactUnit = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject PactUnit 
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

		private Neusoft.NFC.Object.NeuObject medicalType = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ҽ�����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject MedicalType
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
		private Neusoft.NFC.Object.NeuObject balanceOper = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ����Ա
		/// </summary>
		public Neusoft.NFC.Object.NeuObject BalanceOper
		{
			get
			{
				return balanceOper;
			}
			set
			{
				balanceOper = value;
			}
		}

		private string examineFlag;
		/// <summary>
		/// ����־ 0�������/1�������/2������� 
		/// </summary>
		public string ExamineFlag 
		{
			get
			{
				return examineFlag;
			}
			set
			{
				examineFlag = value;
			}
		}
		private Neusoft.HISFC.Object.Base.CancelTypes cancelFlag;
		//private bool isCancel = false;
		/// <summary>
		/// ���ϱ�־
		/// </summary>
		public Neusoft.HISFC.Object.Base.CancelTypes CancelFlag
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

		private string cancelOper = "";
		/// <summary>
		/// ���ϲ���Ա
		/// </summary>
		public string CancelOper 
		{
			get
			{
				return cancelOper;
			}
			set
			{
				cancelOper = value;
			}
		}
		private DateTime cancelDate;
		/// <summary>
		/// ����ʱ��
		/// </summary>
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

		private Neusoft.NFC.Object.NeuObject checkOper = new Neusoft.NFC.Object.NeuObject() ;
		/// <summary>
		/// �˲���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject CheckOper 
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
		private string invoiceSeq;//��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
		/// </summary>
		public string InvoiceSeq
		{
			get
			{
				return invoiceSeq;
			}
			set
			{
				invoiceSeq = value;
			}
		}
		decimal myCTFee;//CT��
		/// <summary>
		/// CT��
		/// </summary>
		public decimal CTFee
		{
			get
			{
				return myCTFee;
			}
			set
			{
				myCTFee = value;
			}
		}
		decimal myMRIFee;//MRI��
		/// <summary>
		/// MRI��
		/// </summary>
		public decimal MRIFee
		{
			get
			{
				return myMRIFee;
			}
			set
			{
				myMRIFee = value;
			}
		}
		decimal mySXFee;//��Ѫ��
		/// <summary>
		/// ��Ѫ��
		/// </summary>
		public decimal SXFee
		{
			get
			{
				return mySXFee;
			}
			set
			{
				mySXFee = value;
			}
		}
		decimal mySYFee;//������
		/// <summary>
		/// ������
		/// </summary>
		public decimal SYFee
		{
			get
			{
				return mySYFee;
			}
			set
			{
				mySYFee = value;
			}
		}
		private DateTime printDisplayDate;//��Ʊ��ʾ�Ĵ�ӡʱ��
		/// <summary>
		/// ��Ʊ��ʾ��ӡʱ��
		/// </summary>
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

		private InvoiceExtend invoiceExtend = new InvoiceExtend();
		/// <summary>
		/// ��Ʊ��չ
		/// </summary>
		public InvoiceExtend InvoiceExtend
		{
			get
			{
				return invoiceExtend;
			}
			set
			{
				invoiceExtend = value;
			}
		}

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new Neusoft.HISFC.Object.Fee.OutPatient.Invoice Clone()
		{
			Neusoft.HISFC.Object.Fee.OutPatient.Invoice obj = base.Clone() as Neusoft.HISFC.Object.Fee.OutPatient.Invoice;
			obj.TransType = this.transType;
			obj.FT = this.FT.Clone();
			obj.PID = this.PID.Clone();
			obj.PayKind = this.PayKind.Clone();
			obj.PactUnit = this.PactUnit.Clone();
			obj.MedicalType = this.MedicalType.Clone();
			obj.BalanceOper = this.BalanceOper.Clone();
			obj.CheckOper = this.CheckOper.Clone();
			obj.InvoiceExtend = this.InvoiceExtend.Clone();

			return obj;
		}
	}
}
