using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.Fee.Inpatient
{
	/// <summary>
	/// Prepay<br></br>
	/// [��������: סԺԤ������]<br></br>
	/// [�� �� ��: ��˹]<br></br>
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
	public class Prepay : NeuObject
	{
		
		#region ����
		
		/// <summary>
		/// ��Ա������Ϣ
		/// </summary>
		private PatientInfo patient = new PatientInfo();

		/// <summary>
		/// Ԥ����Ʊ�ݺ�
		/// </summary>
		private string recipeNO = string.Empty;

		/// <summary>
		/// ������Ϣ ����Ԥ������õ�
		/// </summary>
		private FT ft = new FT();
		
		/// <summary>
        /// ���ѷ�ʽ{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
		/// </summary>
        //private EnumPayTypeService payType = new EnumPayTypeService();
        private NeuObject payType = new NeuObject();
	
		/// <summary>
		///���㷢Ʊ�� 
		/// </summary>
		private Invoice invoice = new Invoice();
		
		/// <summary>
		/// ͳ������
		/// </summary>
		private DateTime staticTime;
		
		/// <summary>
		/// ��������
		/// </summary>
		private DateTime balanceTime;
		
		/// <summary>
		/// ����Ա����
		/// </summary>
		private OperEnvironment balanceOper = new OperEnvironment ();
		
		/// <summary>
		/// ����״̬
		/// </summary>
		private string balanceState = string.Empty;
		
		/// <summary>
		/// �������
		/// </summary>
		private int balanceNO;
		
		/// <summary>
		/// Ԥ����״̬
		/// </summary>
		private string prepayState = string.Empty;
		
		/// <summary>
		/// ��������
		/// </summary>
		private Bank bank = new Bank();
		
		/// <summary>
		/// �Ƿ��Ͻ�
		/// </summary>
		private bool isTurnIn;

		/// <summary>
		/// ���������
		/// </summary>
		private FinanceGroup finGroup = new FinanceGroup();
		
		/// <summary>
		/// תѺ��״̬ 0��תѺ��1תѺ��2תѺ���Ѵ�ӡ
		/// </summary>
		private string transferPrepayState = string.Empty;

		/// <summary>
		/// תѺ��ʱ��
		/// </summary>
		private DateTime transferPrepayTime;
		
		/// <summary>
		/// ԭ��Ʊ��Ϣ
		/// </summary>
		private Invoice orgInvoice = new Invoice();
		
		/// <summary>
		/// ����������
		/// </summary>
		private string auditingNO = string.Empty;
		
		/// <summary>
		/// תѺ�������Ϣ
		/// </summary>
		private OperEnvironment transferPrepayOper = new OperEnvironment();
		
		/// <summary>
		/// Ԥ���������Ϣ
		/// </summary>
		private OperEnvironment prepayOper =  new OperEnvironment();
		
		/// <summary>
		/// תѺ��ʱ������� 
		/// </summary>
		private int transferPrepayBalanceNO;

        /// <summary>
        /// ����������ȡ�ͽ����ٻ��������� ������ȡ 1 �����ٻ� 2
        /// </summary>
        private string prepaySourceState ="1";

        /// <summary>
        /// �Ƿ��ӡ
        /// </summary>
        private bool isPrint = true;
		#endregion
		
		#region ����
		
		/// <summary>
		/// ��Ա������Ϣ
		/// </summary>
		public PatientInfo Patient
		{
			get
			{
				return this.patient;
			}
			set
			{
				this.patient = value;
			}
		}

		/// <summary>
		/// Ԥ����Ʊ�ݺ�
		/// </summary>
		public string RecipeNO
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
		/// ������Ϣ ����Ԥ������õ�
		/// </summary>
		public FT FT
		{
			get
			{
				return this.ft;
			}
			set
			{
				this.ft = value;
			}
		}
		
		/// <summary>
        /// ���ѷ�ʽ{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
		/// </summary>
		//public EnumPayTypeService PayType
        public NeuObject PayType
		{
			get
			{
				return this.payType;
			}
			set
			{
				this.payType = value;
			}
		}
	
		/// <summary>
		/// ���㷢Ʊ��
		/// </summary>
		public Invoice Invoice
		{
			get
			{
				return this.invoice;
			}
			set
			{
				this.invoice = value;
			}
		}

		/// <summary>
		/// ͳ������
		/// </summary>
		public DateTime StaticTime
		{
			get
			{
				return this.staticTime;
			}
			set
			{
				this.staticTime = value;
			}
		}
		
		/// <summary>
		/// ��������
		/// </summary>
        [Obsolete("����,��BalanceOper.OperTime����", true)]
		public DateTime BalanceTime
		{
			get
			{
				return this.balanceTime;
			}
			set
			{
				this.balanceTime = value;
			}
		}
		
		/// <summary>
		/// ����Ա����
		/// </summary>
		public OperEnvironment BalanceOper
		{
			get
			{
				return this.balanceOper;
			}
			set
			{
				this.balanceOper = value;
			}
		}
		
		/// <summary>
		/// ����״̬
		/// </summary>
		public string BalanceState
		{
			get
			{
				return this.balanceState;
			}
			set
			{
				this.balanceState = value;
			}
		}
		
		/// <summary>
		/// �������
		/// </summary>
		public int BalanceNO
		{
			get
			{
				return this.balanceNO;
			}
			set
			{
				this.balanceNO = value;
			}
		}
		
		/// <summary>
		/// Ԥ����״̬
		/// </summary>
		public string PrepayState
		{
			get
			{
				return this.prepayState;
			}
			set
			{
				this.prepayState = value;
			}
		}
		
		/// <summary>
		/// ��������
		/// </summary>
		public Bank Bank
		{
			get
			{
				return this.bank;
			}
			set
			{
				this.bank = value;
			}
		}
		
		/// <summary>
		/// �Ƿ��Ͻ�
		/// </summary>
		public bool IsTurnIn
		{
			get
			{
				return this.isTurnIn;
			}
			set
			{
				this.isTurnIn = value;
			}
		}
		
		/// <summary>
		/// ���������
		/// </summary>
		public FinanceGroup FinGroup
		{
			get
			{
				return this.finGroup;
			}
			set
			{
				this.finGroup = value;
			}
		}
		
		/// <summary>
		/// תѺ��״̬ 0��תѺ��1תѺ��2תѺ���Ѵ�ӡ
		/// </summary>
		public string TransferPrepayState
		{
			get
			{
				return this.transferPrepayState;
			}
			set
			{
				this.transferPrepayState = value;
			}
		}
		
		/// <summary>
		/// תѺ��ʱ��
		/// </summary>
		public DateTime TransferPrepayTime
		{
			get
			{
				return this.transferPrepayTime;
			}
			set
			{
				this.transferPrepayTime = value;
			}
		}
		
		/// <summary>
		/// ԭ��Ʊ��Ϣ
		/// </summary>
		public Invoice OrgInvoice
		{
			get
			{
				return this.orgInvoice;
			}
			set
			{
				this.orgInvoice = value;
			}
		}
		
		/// <summary>
		/// ����������
		/// </summary>
		public string AuditingNO
		{
		
			get
			{
				return this.auditingNO;
			}
			set
			{
				this.auditingNO = value;
			}
		}
		
		/// <summary>
		/// תѺ�������Ϣ
		/// </summary>
		public OperEnvironment TransferPrepayOper
		{
			get
			{
				return this.transferPrepayOper;
			}
			set
			{
				this.transferPrepayOper = value;
			}
		}
		
		/// <summary>
		/// Ԥ���������Ϣ
		/// </summary>
		public OperEnvironment PrepayOper
		{
			get
			{
				return this.prepayOper;
			}
			set
			{
				this.prepayOper = value;
			}
			
		}
		
		/// <summary>
		/// תѺ��ʱ������� 
		/// </summary>
		public int TransferPrepayBalanceNO
		{
			get
			{
				return this.transferPrepayBalanceNO;
			}
			set
			{
				this.transferPrepayBalanceNO = value;
			}
		}

        /// <summary>
        /// ����������ȡ�ͽ����ٻ��������� ������ȡ 1 �����ٻ� 2
        /// </summary>
        public string PrepaySourceState 
        {
            get 
            {
                return this.prepaySourceState;
            }
            set
            {
                this.prepaySourceState = value;
            }
        }

        //{EC04199C-9F39-48f1-BCFE-98430D9962E6}
        /// <summary>
        /// Ԥ����Ʊ���Ƿ��ӡ
        /// </summary>
        public bool IsPrint
        {
            get
            {
                return isPrint;
            }
            set
            {
                isPrint = value;
            }
        }

		#endregion 

		#region ����
		
		/// <summary>
		/// ���߿���
		/// </summary>
		[Obsolete("����,��Patient�еĿ������Դ���", true)]
		public Department PatientDept
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
		
		/// <summary>
		///  Ԥ�����
		/// </summary>
		[Obsolete("����,��FT���Է��ô���", true)]
		public decimal Pre_Cost;
		/// <summary>
		/// ���߿���
		/// </summary>
		//FS.HISFC.Models.Base.Department
		public NeuObject Dept=new NeuObject();
		
		/// <summary>
		/// ���㷢Ʊ��
		/// </summary>
		[Obsolete("����,��Invoice�����Ե�ID����", true)]
		public string InvoiceNo;

		/// <summary>
		/// ͳ������
		/// </summary>
		[Obsolete("����,��StaticTime���Դ���", true)]
		public DateTime StatisticDate;
		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("����,��BalanceTime���Դ���", true)]
		public DateTime DtBalanceDate;
		/// <summary>
		/// ����״̬
		/// </summary>
		[Obsolete("����,��BalanceState���Դ���", true)]
		public string BalanceStatus;
		/// <summary>
		/// �������
		/// </summary>\
		[Obsolete("����,��BalanceNO���Դ���", true)]
		public int BalanceSequece;
		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("����,��Bank���Դ���", true)]
		public FS.HISFC.Models.Base.Bank AccountBank=new FS.HISFC.Models.Base.Bank();
		/// <summary>
		/// �Ƿ��Ͻ�
		/// </summary>
		[Obsolete("����,��IsTurnIn���Դ���", true)]
		public bool IsReport;
		/// <summary>
		/// ���������
		/// </summary>
		//Fee.FinanceGroup
		[Obsolete("����,��FinGroup���Դ���", true)]
		public NeuObject FinGrpCode = new NeuObject();
		/// <summary>
		/// תѺ��״̬ 0��תѺ��1תѺ��2תѺ���Ѵ�ӡ
		/// </summary>
		[Obsolete("����,��TransferPrepayState���Դ���", true)]
		public string  TransPrepayState;
		/// <summary>
		/// תѺ��ʱ��
		/// </summary>
		[Obsolete("����,��TransferPrepayTime���Դ���", true)]
		public DateTime DtTransPrepay;
		/// <summary>
		/// Ԥ����Ʊ��
		/// </summary>
		[Obsolete("����,��Invoice.ID����", true)]
		public string ReceiptNo;
		/// <summary>
		/// ԭ��Ʊ��
		/// </summary>
		[Obsolete("����,��OrgInvoice.ID����", true)]
		public string OldReceiptNo;
        /// <summary>
		/// ����������
		/// </summary>
		[Obsolete("����,��AuditingNO����", true)]
		public string CheckNo;
		/// <summary>
		/// תѺ�����Ա
		/// </summary>
		//OperEnvironment
		public NeuObject TransPrepayOper = new NeuObject();
		/// <summary>
		/// ����Ա����
		/// </summary>
		[Obsolete("����",true)]
		public NeuObject OperDept = new NeuObject();
		/// <summary>
		/// Ԥ�������ʱ��
		/// </summary>
		[Obsolete("����,��PrepayOper.OperTime����", true)]
		public DateTime DtOperate;
		/// <summary>
		/// תѺ��ʱ������� 
		/// </summary>
		[Obsolete("����,��TransferPrepayBalanceNO����", true)]
		public int ChangeBalanceNo;
		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Prepay Clone()
		{
			Prepay prepay = base.Clone() as Prepay;

			prepay.FT = this.FT.Clone();
            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
            //prepay.payType = this.PayType.Clone()as EnumPayTypeService;
            prepay.PayType = this.PayType.Clone();
			prepay.Invoice = this.Invoice.Clone();
			prepay.BalanceOper = this.BalanceOper.Clone();
			prepay.Bank = this.Bank.Clone();
			prepay.FinGroup = this.FinGroup.Clone();
			prepay.OrgInvoice = this.OrgInvoice.Clone();
			prepay.TransferPrepayOper = this.TransferPrepayOper.Clone();
			prepay.PrepayOper = this.PrepayOper.Clone();

			return prepay;
		}

		#endregion

		#endregion
		
	}
}