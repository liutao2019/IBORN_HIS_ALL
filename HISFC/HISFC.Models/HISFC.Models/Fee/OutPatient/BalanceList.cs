using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Outpatient
{


	/// <summary>
	/// BalanceList<br></br>
	/// [��������: ���ý�����ϸ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>>
    /// 
    [System.Serializable]
	public class BalanceList : BalanceListBase 
	{
		public BalanceList( ) 
		{	
			this.balanceBase = new Balance();
		}

		#region ����
		
		/// <summary>
		/// ��Ʊ�ڲ���ˮ��
		/// </summary>
		private int invoiceSquence;
		
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
		/// </summary>
		private string combNO;

        /// <summary>
        /// CT��
        /// </summary>
        decimal myCTFee;
        

        /// <summary>
        /// MRI��
        /// </summary>
        decimal myMRIFee;
        

        /// <summary>
        /// ��Ѫ��
        /// </summary>
        decimal mySXFee;
       
        /// <summary>
        /// ������
        /// </summary>
        private decimal mySYFee;
        
		#endregion

		#region ����

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
		/// ��Ʊ�ڲ���ˮ��
		/// </summary>
		public int InvoiceSquence
		{
			get
			{
				return this.invoiceSquence;
			}
			set
			{
				this.invoiceSquence = value;
			}
		}
		
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
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

		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ��</returns>
		public new BalanceList Clone()
		{
			return base.Clone() as BalanceList;
		} 

		#endregion

		#endregion

		
		#region ���� ����


		private int invoSequence;
		/// <summary>
		/// ��Ʊ�ڲ���ˮ��
		/// </summary>
		[Obsolete("����,InvoiceSquence����", true)]
		public int InvoSequence
		{
			get{return invoSequence;}
			set{invoSequence = value;}
		}

		private FS.FrameWork.Models.NeuObject invoItem = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ��Ʊ��Ŀ
		/// </summary>
		[Obsolete("����,FeeCodeStat����", true)]
		public FS.FrameWork.Models.NeuObject InvoItem 
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

		private FS.FrameWork.Models.NeuObject execDept = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ִ�п���
		/// </summary>
		[Obsolete("����", true)]
		public FS.FrameWork.Models.NeuObject ExecDept 
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
		[Obsolete("���� Oper.OperTime", true)]
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

		private string balanceFlag;
		/// <summary>
		/// �ս��ʶ1���ս�/2δ�ս�
		/// </summary>
		[Obsolete("���� IsBalanced", true)]
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
		[Obsolete("����", true)]
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
		[Obsolete("���� BalanceOper.OperTime", true)]
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
		private FS.HISFC.Models.Base.CancelTypes cancelFlag;
		/// <summary>
		/// ��Ŀ״̬0������1�˷ѣ�2�ش�3ע�� 
		/// </summary>
		[Obsolete("���� CancelType", true)]
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

		private string invoiceSeq;//��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
		/// </summary>
		[Obsolete("���� CombNO", true)]
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

		#endregion
	}
}
