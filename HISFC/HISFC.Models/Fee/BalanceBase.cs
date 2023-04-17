using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.Fee 
{
	/// <summary>
	/// BalanceBase<br></br>
	/// [��������: ���ý�������� ID:������ˮ�� Name:��������]<br></br>
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
	public abstract class BalanceBase : NeuObject
	{
		#region ����
		
		/// <summary>
		/// �������� Positive ������ Negative ������
		/// </summary>
		private TransTypes transType;
		
		/// <summary>
		/// ��������
		/// </summary>
		private BalanceTypeEnumService balanceType = new BalanceTypeEnumService();
		
		/// <summary>
		/// ���߻�����Ϣ
		/// </summary>
		private Patient patient = new Patient();
		
		/// <summary>
		/// ��Ʊ��Ϣ
		/// </summary>
		private Invoice invoice = new Invoice();
		
		/// <summary>
		/// ������
		/// </summary>
		private FinanceGroup financeGroup = new FinanceGroup();
		
		/// <summary>
		/// ��������(�������Ա,����ʱ��)
		/// </summary>
		private OperEnvironment oper = new OperEnvironment();
		
		/// <summary>
		/// �����������(����������Ա,����ʱ��)
		/// </summary>
		private OperEnvironment balanceOper = new OperEnvironment();
		
		/// <summary>
		/// ��˲�������(��˲���Ա,���ʱ��)
		/// </summary>
		private OperEnvironment auditingOper = new OperEnvironment();
		
		/// <summary>
		/// ���ϲ�������(���ϲ���Ա,����ʱ��)
		/// </summary>
        private OperEnvironment cancelOper = new OperEnvironment();

        /// <summary>
        /// ������������(����ҽ��,����ҽ�����ڿ���,ҽ������ʱ��)
        /// </summary>
        private OperEnvironment recipeOper;// new OperEnvironment();
		
		/// <summary>
		/// ��Ʊ״̬
		/// </summary>
		private CancelTypes cancelType;
		
		/// <summary>
		/// ����Ʊ�ݺ�
		/// </summary>
		private string canceledInvoiceNO;
		
		/// <summary>
		/// �Ƿ����
		/// </summary>
		private bool isAuditing;
		
		/// <summary>
		/// �Ƿ��ս�
		/// </summary>
		private bool isDayBalanced;
		
		/// <summary>
		/// �ս��������(�ս���,�ս�ʱ��)
		/// </summary>
		private OperEnvironment dayBalanceOper = new OperEnvironment();
		
		/// <summary>
		/// ��Ʊ��ӡ����
		/// </summary>
		private DateTime printTime;
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FT ft = new FT();
		
		/// <summary>
		/// ��Ʊ�ķ������ 1 �Է� 2 ���� 3 ����
		/// </summary>
        private NeuObject invoiceType = new NeuObject();

        /// <summary>
        /// ��ֺ�  ���շѱ��
        /// </summary>
        private bool splitFeeFlag = false;

        /// <summary>
        /// Ƿ�ѱ��
        /// </summary>
        private bool arrearFlag = false;

        /// <summary>
        /// //{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
        /// �ײͱ�ʶ 0-���ײ� 1-�ײ�
        /// </summary>
        private string packageFlag = "0";
		#endregion
		
		#region ����

		/// <summary>
		/// �������� Positive ������ Negative ������
		/// </summary>
		public TransTypes TransType
		{
			get
			{
				return this.transType;
			}
			set
			{
				this.transType = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public BalanceTypeEnumService BalanceType 
        {
			get
			{
				return this.balanceType;
			}
			set
			{
				this.balanceType = value;
			}
		}

		/// <summary>
		/// ��Ʊ��Ϣ
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
		/// ���߻�����Ϣ
		/// </summary>
		public Patient Patient
		{
			get
			{
				return this.patient;
			}
			set
			{
				this.patient = value;
				
				if (this.patient != null)
				{
					this.Name = this.patient.Name;
				}
			}
		}
		
		/// <summary>
		/// ������
		/// </summary>
		public FinanceGroup FinanceGroup
		{
			get
			{
				return this.financeGroup;
			}
			set
			{
				this.financeGroup = value;
			}
		}
		
		/// <summary>
		/// ��������(�������Ա,����ʱ��)
		/// </summary>
		public OperEnvironment Oper
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
		/// �����������(����������Ա,����ʱ��)
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
		/// ��˲�������(��˲���Ա,���ʱ��)
		/// </summary>
		public OperEnvironment AuditingOper
		{
			get
			{
				return this.auditingOper;
			}
			set
			{
				this.auditingOper = value;
			}
		}

        /// <summary>
        /// ��˲�������(��˲���Ա,���ʱ��)
        /// </summary>
        public OperEnvironment RecipeOper
        {
            get
            {
                if (this.recipeOper == null)
                {
                    this.recipeOper = new OperEnvironment();
                }
                return this.recipeOper;
            }
            set
            {
                this.recipeOper = value;
            }
        }
		
		/// <summary>
		/// ���ϲ�������(���ϲ���Ա,����ʱ��)
		/// </summary>
		public OperEnvironment CancelOper
		{
			get
			{
				return this.cancelOper;
			}
			set
			{
				this.cancelOper = value;
			}
		}

		/// <summary>
		/// ��Ʊ״̬
		/// </summary>
		public CancelTypes CancelType
		{
			get
			{
				return this.cancelType;
			}
			set
			{
				this.cancelType = value;
			}
		}
		
		/// <summary>
		/// ����Ʊ�ݺ�
		/// </summary>
		public string CanceledInvoiceNO
		{
			get
			{
				return this.canceledInvoiceNO;
			}
			set
			{
				this.canceledInvoiceNO = value;
			}
		}

		/// <summary>
		/// �Ƿ����
		/// </summary>
		public bool IsAuditing
		{
			get
			{
				return this.isAuditing;
			}
			set
			{
				this.isAuditing = value;
			}
		}

		/// <summary>
		/// �Ƿ��ս�
		/// </summary>
		public bool IsDayBalanced
		{
			get
			{
				return this.isDayBalanced;
			}
			set
			{
				this.isDayBalanced = value;
			}
		}
		
		/// <summary>
		/// �ս��������(�ս���,�ս�ʱ��)
		/// </summary>
		public OperEnvironment DayBalanceOper
		{
			get
			{
				return this.dayBalanceOper;
			}
			set
			{
				this.dayBalanceOper = value;
			}
		}

		/// <summary>
		/// ��Ʊ��ӡ����
		/// </summary>
		public DateTime PrintTime
		{
			get
			{
				return this.printTime;
			}
			set
			{
				this.printTime = value;
			}
		}

		/// <summary>
		/// ������Ϣ
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
		/// ��Ʊ�ķ������ 1 �Է� 2 ���� 3 ����
		/// </summary>
		public NeuObject InvoiceType
		{
			get
			{
				return this.invoiceType;
			}
			set
			{
				this.invoiceType = value;
			}
		}

        /// <summary>
        /// ��ֺ�  ���շѱ��
        /// </summary>
        public bool SplitFeeFlag
        {
            get
            {
                return splitFeeFlag;
            }
            set
            {
                splitFeeFlag = value;
            }
        }

        /// <summary>
        /// {18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
        /// �ײͱ�ʶ 0-�ײ� 1-���ײ�
        /// </summary>
        public string PackageFlag
        {
            get
            {
                return packageFlag;
            }
            set
            {
                packageFlag = value;
            }
        }

		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ�����ʵ������</returns>
		public new BalanceBase Clone()
		{
			BalanceBase balanceBase = base.Clone() as BalanceBase;

            if (this.recipeOper != null)
            {
                balanceBase.RecipeOper = this.recipeOper.Clone();
            }
			balanceBase.AuditingOper = this.AuditingOper.Clone();
			balanceBase.BalanceOper = this.BalanceOper.Clone();
			balanceBase.CancelOper = this.CancelOper.Clone();
			balanceBase.DayBalanceOper = this.DayBalanceOper.Clone();
			balanceBase.FinanceGroup = this.FinanceGroup.Clone();
			balanceBase.FT = this.FT.Clone();
			balanceBase.Invoice = this.Invoice.Clone();
			balanceBase.Oper = this.Oper.Clone();
			balanceBase.Patient = this.Patient.Clone();
			balanceBase.InvoiceType = this.InvoiceType.Clone();
            balanceBase.BalanceType = this.BalanceType.Clone() as BalanceTypeEnumService;

			return balanceBase;
		}

		#endregion

		#endregion

	}
}
