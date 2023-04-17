using System;

namespace FS.HISFC.Models.Fee.Outpatient
{


	/// <summary>
	/// PayMode ��ժҪ˵����
	/// </summary>
    /// 
    [System.Serializable]
	[Obsolete("����,BalancePay����", true)]
	public class PayMode : BalancePayBase 
	{	
      
		
		#region ����
		
		/// <summary>
		///������ˮ�� 
		/// </summary>
		private int seqNo = 0;	
		
		/// <summary>
		/// ʵ�����
		/// </summary>
		private decimal realCost = 0m;
		
		/// <summary>
		/// 
		/// </summary>
		private FS.HISFC.Models.Base.TransTypes transType;

		/// <summary>
		/// pos����
		/// </summary>
		private string posNo = "";
		
		/// <summary>
		/// �˲���
		/// </summary>
		private FS.FrameWork.Models.NeuObject checkOper = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// �Ƿ�˲�1δ�˲�/2�Ѻ˲�
		/// </summary>
		private bool isCheck = false;
		
		/// <summary>
		/// ����ԱID,name����Ա���� memo����ʱ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// �Ƿ����
		/// </summary>
		private bool isBalance = false;

		/// <summary>
		/// ���ʱ��
		/// </summary>
		private DateTime checkTime ;
		
		/// <summary>
		/// �Ƿ����1δ����/2�Ѷ���
		/// </summary>
		private bool isCorrect = false;
		
		/// <summary>
		/// ������
		/// </summary>
		private string correctOper = "";
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime correctDate;
		
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
		/// </summary>
		private string invoiceSeq;
		
		/// <summary>
		/// ���ϱ�־
		/// </summary>
		private FS.HISFC.Models.Base.CancelTypes cancelFlag;

		#endregion		
		
		#region ����
		/// <summary>
		///������ˮ��
		/// </summary>
		public int SeqNo
		{
			get
			{
				return this.seqNo;
			}
			set
			{
				this.seqNo = value;
			}
		}

		/// <summary>
		/// ʵ�����
		/// </summary>
		public decimal RealCost
		{
			get
			{
				return this.realCost;
			}
			set
			{
				this.realCost = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public new FS.HISFC.Models.Base.TransTypes TransType
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
		/// pos����
		/// </summary>
		public string PosNo
		{
			get
			{
				return this.posNo;
			}
			set
			{
				this.posNo = value;
			}
		}
		
		/// <summary>
		/// �Ƿ�˲�1δ�˲�/2�Ѻ˲�
		/// </summary>
		public bool IsCheck
		{
			get
			{
				return this.isCheck;
			}
			set
			{
				this.isCheck = value;
			}
		}
	
		/// <summary>
		/// �˲���
		/// </summary>
		public new FS.FrameWork.Models.NeuObject CheckOper
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
		/// ����ԱID,name����Ա���� memo����ʱ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Oper
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
		/// ���ʱ��
		/// </summary>
		public DateTime CheckTime
		{
			get
			{
				return this.checkTime;
			}
			set
			{
				this.checkTime = value;
			}
		}
		
		/// <summary>
		/// �Ƿ����
		/// </summary>
		public bool IsBalance
		{
			get
			{
				return this.isBalance;
			}
			set
			{
				this.isBalance = value;
			}
		}
	
		/// <summary>
		/// �Ƿ����1δ����/2�Ѷ���
		/// </summary>
		public bool IsCorrect
		{
			get
			{
				return this.isCorrect;
			}
			set
			{
				this.isCorrect = value;
			}
		}
				
		/// <summary>
		/// ������
		/// </summary>
		public string CorrectOper
		{
			get
			{
				return this.correctOper;
			}
			set
			{
				this.correctOper = value;
			}
		}
	
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CorrectDate
		{
			get
			{
				return this.correctDate;
			}
			set
			{
				this.correctDate = value;
			}
		}

		
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
				
		/// <summary>
		/// ���ϱ�־
		/// </summary>
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

		#endregion

        #region ����
        
		#region ��¡
			
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰʵ���Ķ���</returns>
		public new FS.HISFC.Models.Fee.Outpatient.PayMode Clone()
		{
			PayMode payMode = base.Clone() as PayMode;

			payMode.CheckOper = this.CheckOper.Clone();
			payMode.Oper = this.Oper.Clone();

			return payMode;
		}
		#endregion

       #endregion

//		/// <summary>
//		/// ��¡����
//		/// </summary>
//		/// <returns></returns>
//		public new FS.HISFC.Models.Fee.OutPatient.PayMode Clone()
//		{
//			FS.HISFC.Models.Fee.OutPatient.PayMode obj = base.Clone() as FS.HISFC.Models.Fee.OutPatient.PayMode;
//			
//			obj.BalanceOper = this.BalanceOper;
//			obj.CheckOper = this.CheckOper;
//
//			return obj;
//		}
	}
}
