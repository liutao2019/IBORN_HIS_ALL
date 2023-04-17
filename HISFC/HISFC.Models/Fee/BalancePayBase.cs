using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee 
{

	/// <summary>
	/// BalancePayBase<br></br>
	/// [��������: ֧����Ϣ������]<br></br>
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
	public abstract class BalancePayBase : NeuObject 
	{
		#region ����
		
		/// <summary>
		/// ��Ʊ��Ϣ
		/// </summary>
		private Invoice invoice = new Invoice();
		
		/// <summary>
		/// ��������
		/// </summary>
		private TransTypes transType;
		
		/// <summary>
        /// ֧������{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
		/// </summary>
		//private EnumPayTypeService payType = new EnumPayTypeService();
        private NeuObject payType = new NeuObject();
		
		/// <summary>
		/// �����Ϣ
		/// </summary>
		private FT ft = new FT();
		
		/// <summary>
		/// ����
		/// </summary>
		private int qty;
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private Bank bank = new Bank();
		
		/// <summary>
		/// �������ձ�� 1����2����
		/// </summary>
		private string returnOrSupplyFlag;

        /// <summary>
        /// ����Ա��Ϣ(¼��,�շ�,�˷Ѳ���Ա)
        /// </summary>
        private OperEnvironment inputOper = new OperEnvironment();
		
		/// <summary>
		/// �ս���Ϣ(�ս���,�ս�ʱ��)
		/// </summary>
		private OperEnvironment balanceOper = new OperEnvironment();
		
		/// <summary>
		/// POS������
		/// </summary>
		private string posNO;

		/// <summary>
		/// �Ƿ����
		/// </summary>
		private bool isAuditing;

		/// <summary>
		/// ��˲�������(��˲���Ա,���ʱ��)
		/// </summary>
		private OperEnvironment auditingOper = new OperEnvironment();
		
		/// <summary>
		/// �Ƿ��ս�
		/// </summary>
		private bool isDayBalanced;
		
		/// <summary>
		/// �Ƿ����ж���
		/// </summary>
		private bool isChecked;
		
		/// <summary>
		/// ���ʲ�������(��������Ϣ,����ʱ��)
		/// </summary>
		private OperEnvironment checkOper = new OperEnvironment();
		
		/// <summary>
		/// ֧��״̬
		/// </summary>
		private CancelTypes cancelType;

        /// <summary>
        /// ͨ��ʵ��
        /// </summary>
        private Object usualObject = null;

		#endregion

		#region ����

        /// <summary>
        /// ����Ա��Ϣ(¼��,�շ�,�˷Ѳ���Ա)
        /// </summary>
        public OperEnvironment InputOper 
        {
            get 
            {
                return this.inputOper;
            }
            set 
            {
                this.inputOper = value;
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
		/// ��������
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
        /// ֧������{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
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
		/// �����Ϣ
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
		/// ����
		/// </summary>
		public int Qty
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
		/// ������Ϣ
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
		/// �������ձ�� 1����2����
		/// </summary>
		public string RetrunOrSupplyFlag
		{
			get
			{
				return this.returnOrSupplyFlag;
			}
			set
			{
				this.returnOrSupplyFlag = value;
			}
		}

		/// <summary>
		/// ���㻷����Ϣ(������,����ʱ��)
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
		/// POS������
		/// </summary>
		public string POSNO
		{
			get
			{
				return this.posNO;
			}
			set
			{
				this.posNO = value;
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
		/// �Ƿ����ж���
		/// </summary>
		public bool IsChecked
		{
			get
			{
				return this.isChecked;
			}
			set
			{
				this.isChecked = value;
			}
		}

		/// <summary>
		/// ���ʲ�������(��������Ϣ,����ʱ��)
		/// </summary>
		public OperEnvironment CheckOper
		{
			get
			{
				return this.checkOper;
			}
			set
			{
				this.checkOper = value;
			}
		}
		
		/// <summary>
		/// ֧��״̬
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
        /// ͨ��ʵ��
        /// </summary>
        public Object UsualObject
        {
            get
            {
                return usualObject;
            }
            set
            {
                usualObject = value;
            }
        }

		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ�����ʵ������</returns>
		public new BalancePayBase Clone()
		{
			BalancePayBase balancePayBase = base.Clone() as BalancePayBase;

			balancePayBase.AuditingOper = this.AuditingOper.Clone();
			balancePayBase.BalanceOper = this.BalanceOper.Clone();
			balancePayBase.Bank = this.Bank.Clone();
			balancePayBase.CheckOper = this.CheckOper.Clone();
			balancePayBase.FT = this.FT.Clone();
			balancePayBase.Invoice = this.Invoice.Clone();
			//{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
            balancePayBase.PayType = this.PayType.Clone();

			return balancePayBase;
		}

		#endregion

		#endregion
	}
}
