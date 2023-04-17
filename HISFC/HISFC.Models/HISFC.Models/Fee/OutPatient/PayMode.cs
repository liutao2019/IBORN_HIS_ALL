using System;

namespace FS.HISFC.Models.Fee.Outpatient
{


	/// <summary>
	/// PayMode 的摘要说明。
	/// </summary>
    /// 
    [System.Serializable]
	[Obsolete("作废,BalancePay代替", true)]
	public class PayMode : BalancePayBase 
	{	
      
		
		#region 变量
		
		/// <summary>
		///交易流水号 
		/// </summary>
		private int seqNo = 0;	
		
		/// <summary>
		/// 实付金额
		/// </summary>
		private decimal realCost = 0m;
		
		/// <summary>
		/// 
		/// </summary>
		private FS.HISFC.Models.Base.TransTypes transType;

		/// <summary>
		/// pos机号
		/// </summary>
		private string posNo = "";
		
		/// <summary>
		/// 核查人
		/// </summary>
		private FS.FrameWork.Models.NeuObject checkOper = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// 是否核查1未核查/2已核查
		/// </summary>
		private bool isCheck = false;
		
		/// <summary>
		/// 操作员ID,name操作员姓名 memo操作时间
		/// </summary>
		private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// 是否结算
		/// </summary>
		private bool isBalance = false;

		/// <summary>
		/// 检查时间
		/// </summary>
		private DateTime checkTime ;
		
		/// <summary>
		/// 是否对帐1未对帐/2已对帐
		/// </summary>
		private bool isCorrect = false;
		
		/// <summary>
		/// 对帐人
		/// </summary>
		private string correctOper = "";
		
		/// <summary>
		/// 对帐时间
		/// </summary>
		private DateTime correctDate;
		
		/// <summary>
		/// 发票序号，一次结算产生多张发票的combNo
		/// </summary>
		private string invoiceSeq;
		
		/// <summary>
		/// 作废标志
		/// </summary>
		private FS.HISFC.Models.Base.CancelTypes cancelFlag;

		#endregion		
		
		#region 属性
		/// <summary>
		///交易流水号
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
		/// 实付金额
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
		/// pos机号
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
		/// 是否核查1未核查/2已核查
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
		/// 核查人
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
		/// 操作员ID,name操作员姓名 memo操作时间
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
		/// 检查时间
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
		/// 是否结算
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
		/// 是否对帐1未对帐/2已对帐
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
		/// 对帐人
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
		/// 对帐时间
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
		/// 发票序号，一次结算产生多张发票的combNo
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
		/// 作废标志
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

        #region 方法
        
		#region 克隆
			
		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns>返回当前实例的对象</returns>
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
//		/// 克隆本身
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
