using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Outpatient
{
	/// <summary>
	/// DayBalance ��ժҪ˵����
	/// �ս�
	/// </summary>
    /// 
    [System.Serializable]
	public class DayBalance : NeuObject
	{
		#region ����
		
		/// <summary>
		/// ������Ϣ��
		/// </summary>
		private FT ft = new FT();
		
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		private DateTime beginTime;
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime endTime;
		
		/// <summary>
		/// ��������(����Ա,����ʱ��,����Ա����)
		/// </summary>
		private OperEnvironment oper = new OperEnvironment();
		
		/// <summary>
		/// �Ƿ����
		/// </summary>
		private bool isAuditing;
		
		/// <summary>
		/// ��˲�������(��˲���Ա,���ʱ��,���Ա���ڿ���)
		/// </summary>
		private OperEnvironment auditingOper = new OperEnvironment();

		#endregion	

		#region ����

		/// <summary>
		/// ������Ϣ��
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
		/// ��������(����Ա,����ʱ��,����Ա����)
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
		/// ��˲�������(��˲���Ա,���ʱ��,���Ա���ڿ���)
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

		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new DayBalance Clone()
		{
			DayBalance dayBalance = base.Clone() as DayBalance;

			dayBalance.AuditingOper = this.AuditingOper.Clone();
			dayBalance.FT = this.FT.Clone();
			dayBalance.Oper = this.Oper.Clone();
			
			return dayBalance;
		}

		#endregion

		#endregion

		#region ���ñ���,����

		private decimal totCost = 0m;

		/// <summary>
		/// ������
		/// </summary>
		[Obsolete("����,FT.TotCost", true)]
		public decimal TotCost
		{
			get
			{
				return this.totCost;
			}
			set
			{
				this.totCost = value;
			}
		}

		private DateTime beginDate ;

		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		[Obsolete("����,BeginTime", true)]
		public DateTime BeginDate
		{
			get
			{
				return this.beginDate;
			}
			set
			{
				this.beginDate = value;
			}
		}
		private DateTime endDate ;

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("����,EndTime", true)]
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		private bool isCheck = false;

		/// <summary>
		/// ������ˣ�1δ���/2�����
		/// </summary>
		[Obsolete("����, IsAuditing", true)]
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
		private string checkOper = "";

		/// <summary>
		/// �����
		/// </summary>
		[Obsolete("����, AuditingOper", true)]
		public string CheckOper
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
		private DateTime checkDate;
		/// <summary>
		/// ���ʱ��
		/// </summary>
		[Obsolete("����, AuditingOper.OperTime", true)]
		public DateTime CheckDate
		{
			get
			{
				return this.checkDate;
			}
			set
			{
				this.checkDate = value;
			}
		}
		
		#endregion
	}
}
