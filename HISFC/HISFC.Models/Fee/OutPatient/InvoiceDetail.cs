using System;

namespace Neusoft.HISFC.Object.Fee.OutPatient
{
	/// <summary>
	/// InvoiceDetail ��ժҪ˵����write by lisn
	/// Id��Ʊ��
	/// </summary>
	public class InvoiceDetail: Neusoft.NFC.Object.NeuObject
	{
		public InvoiceDetail()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private Neusoft.HISFC.Object.Base.TransTypes transType = new Neusoft.HISFC.Object.Base.TransTypes(); //��������
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

		private int invoSequence;
		/// <summary>
		/// ��Ʊ�ڲ���ˮ��
		/// </summary>
		public int InvoSequence
		{
			get{return invoSequence;}
			set{invoSequence = value;}
		}

		private Neusoft.NFC.Object.NeuObject invoItem = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ��Ʊ��Ŀ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject InvoItem 
		{
			get
			{
				return invoItem;
			}
			set
			{
				invoItem = value;
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

		private Neusoft.NFC.Object.NeuObject execDept = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ִ�п���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject ExecDept 
		{
			get
			{
				return execDept;
			}
			set
			{
				execDept = value;
			}
		}

		private DateTime operDate;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate 
		{
			get
			{
				return operDate;
			}
			set
			{
				operDate = value;
			}
		}

		private string oper;
		/// <summary>
		/// ����Ա
		/// </summary>
		public string Oper
		{
			get
			{
				return oper;
			}
			set
			{
				oper = value;
			}
		}

		private string balanceOper;
		/// <summary>
		/// �ս���Ա
		/// </summary>
		public string BalanceOper
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

		private string balanceFlag;
		/// <summary>
		/// �ս��ʶ1���ս�/2δ�ս�
		/// </summary>
		public string BalanceFlag
		{
			get
			{
				return balanceFlag;
			}
			set
			{
				balanceFlag = value;
			}
		}


		private string balanceNo;
		/// <summary>
		/// �ս��ʶ��
		/// </summary>
		public string BalanceNo
		{
			get
			{
				return balanceNo;
			}
			set
			{
				balanceNo = value;
			}
		}

		private DateTime balanceDate;
		/// <summary>
		/// �ս��ʶ��
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
		private Neusoft.HISFC.Object.Base.CancelTypes cancelFlag;
		/// <summary>
		/// ��Ŀ״̬0������1�˷ѣ�2�ش�3ע�� 
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


		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new Neusoft.HISFC.Object.Fee.OutPatient.InvoiceDetail Clone()
		{
			Neusoft.HISFC.Object.Fee.OutPatient.InvoiceDetail obj = base.Clone() as Neusoft.HISFC.Object.Fee.OutPatient.InvoiceDetail;
			
			obj.TransType = this.transType;
			obj.InvoItem = this.InvoItem.Clone();
			obj.FT = this.FT.Clone();
			obj.ExecDept = this.ExecDept.Clone();


			return obj;
		}


	}
}
