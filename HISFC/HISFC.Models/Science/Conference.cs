using System;

namespace neusoft.HISFC.Object.Science
{
	/// <summary>
	/// Conference 的摘要说明。
	/// </summary>
	public class Conference:neusoft.neuFC.Object.neuObject
	{
		public Conference()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 定义
		private System.String primaryId = "";
		private System.String valid = "";
		private System.String operCode = "";
		private System.DateTime operDate = System.DateTime.MinValue;
		private System.String level = "";
		private System.DateTime beginTime = System.DateTime.MinValue;
		private System.DateTime endTime = System.DateTime.MinValue;
		private System.String mark = "";
		private System.String place = "";
		#endregion

		/// <summary>
		/// 主键列
		/// </summary>
		public string PrimaryId
		{
			get
			{
				return this.primaryId;
			}
			set
			{
				this.primaryId = value;
			}
		}
		/// <summary>
		/// 会议地点
		/// </summary>
		public string Place
		{
			get
			{
				return this.place;
			}
			set
			{
				this.place = value;
			}
		}
		/// <summary>
		/// 开始时间
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
		/// 结束时间
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
		/// 会议级别
		/// </summary>
		public string Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Mark
		{
			get
			{
				return this.mark;
			}
			set
			{
				this.mark = value;
			}
		}
		/// <summary>
		/// 有效状态
		/// </summary>
		public string Valid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}
		/// <summary>
		/// 操作员
		/// </summary>
		public string OperCode
		{
			get
			{
				return this.operCode;
			}
			set
			{
				this.operCode = value;
			}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}
	}
}
