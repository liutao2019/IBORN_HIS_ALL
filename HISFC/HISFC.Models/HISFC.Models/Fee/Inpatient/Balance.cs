using System;
using System.Collections;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Inpatient
{
	/// <summary>
	/// Balance<br></br>
	/// [��������: סԺ������]<br></br>
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
		
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		private DateTime beginTime;
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime endTime;
		
		/// <summary>
		/// ��ӡ����
		/// </summary>
		private int printTimes;
		
		/// <summary>
		/// ������
		/// </summary>
		private string auditingNO;

		/// <summary>
		/// �Ƿ�����Ʊ
		/// </summary>
		private bool isMainInvoice;
		
		/// <summary>
		/// �Ƿ�Ϊ��������������
		/// </summary>
		private bool isLastBalance;

        /// <summary>
        /// Ƿ�ѽ��㴦���־
        /// </summary>
        private string balanceSaveType = string.Empty;

        /// <summary>
        /// Ƿ�ѽ��㴦���־
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment balanceSaveOper = new OperEnvironment();
		#endregion

		#region  ����

		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public DateTime BeginTime
		{
			get
			{
				return this.beginTime;
			}
			set
			{
				this.beginTime = value;
			}
		}
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime EndTime		
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}
		
		/// <summary>
		/// ��ӡ����
		/// </summary>
		public int PrintTimes
		{
			get
			{
				return this.printTimes;
			}
			set
			{
				this.printTimes = value;
			}
		}
		
		/// <summary>
		/// ������
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
		/// �Ƿ�����Ʊ
		/// </summary>
		public bool IsMainInvoice
		{
			get
			{
				return this.isMainInvoice;
			}
			set
			{
				this.isMainInvoice = value;
			}
		}
		
		/// <summary>
		/// �Ƿ�Ϊ��������������
		/// </summary>
		public bool IsLastBalance
		{
			get
			{
				return this.isLastBalance;
			}
			set
			{
				this.isLastBalance = value;
			}
		}

        /// <summary>
        /// Ƿ�ѽ��㴦���־
        /// </summary>
        public string BalanceSaveType
        {
            get
            {
                return balanceSaveType;
            }
            set
            {
                balanceSaveType = value;
            }
        }

        /// <summary>
        /// Ƿ�ѽ��㴦���־
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment BalanceSaveOper
        {
            get
            {
                return balanceSaveOper;
            }
            set
            {
                balanceSaveOper = value;
            }
        }
		#endregion

		#region ���ñ�������
		/// <summary>
		/// ���ϱ��
		/// </summary>
		[Obsolete("����,�Ѿ��̳�", true)]
		public string WasteFlag;
		
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		[Obsolete("����,��BeginTime���Դ���", true)]
		public DateTime DtBegin;
		/// <summary>
		/// ����ʱ��
		/// </summary>
			[Obsolete("����,��EndTime���Դ���", true)]
		public DateTime DtEnd;
        /// <summary>
        /// ����ʱ��
        /// </summary>
       [Obsolete("����,�Ѿ��̳�", true)]
        public DateTime DtBalance;  
		
		/// <summary>
		/// ������
		/// </summary>
		[Obsolete("����,AuditingNO����", true)]
		public string CheckNo;
		
		/// <summary>
		/// �������
		/// </summary>��д��new Pact
		/// ���ó�Ϊ����  
		[Obsolete("����,�����Patient.Pact.PayKind����", true)]
		public NeuObject PayKind = new NeuObject();
		/// <summary>
		/// �Ƿ�Ϊ��������������
		/// </summary>
		[Obsolete("����,IsLastBalance����", true)]
		public bool IsLastFlag;
	
		[Obsolete("����,�Ѿ��̳�", true)]
		public DateTime  DtWaste;

		#endregion
		
        #region ����

        #region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ���ʵ������</returns>
		public new Balance Clone()
		{
			return base.Clone() as Balance;
		}

		#endregion
		
		#endregion
	}
}
