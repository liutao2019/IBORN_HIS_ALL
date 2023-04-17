using System;

namespace FoShanDiseasePay.Base
{
	/// <summary>
	/// jobinfo 的摘要说明。
	/// </summary>
	public class JobInfo
	{
		public JobInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region Getter / Setter
		
		private string jOBCODE;
		private string jOBNAME = String.Empty;
		private string jOBSTATE = String.Empty;
		private DateTime lASTDTIME;
		private DateTime nEXTDTIME;
		private long iNTERVAL;
		private string oPERCODE = String.Empty;
		private DateTime oPERDATE;
		private string mARK = String.Empty;
		private string dLLNAME = String.Empty;
		private string cLASSNAME = String.Empty;
		private string jOBSTARTTIME = String.Empty;
        private string jOBENDTIME = String.Empty;
		
		/// <summary>
		/// job_ID
		/// </summary>
		public string JOBCODE
		{
			get{ return jOBCODE; }
			set{ jOBCODE = value; }
		}

        /// <summary>
		/// 名称
		/// </summary>
		public string JOBNAME
		{
			get{ return jOBNAME; }
			set{ jOBNAME = value; }
		}

		/// <summary>
		/// 状态N_不执行, D_每日执行一次, M_隔几分钟就执行的（这个不能用主线程）S_正在执行
		/// </summary>
		public string JOBSTATE
		{
			get{ return jOBSTATE; }
			set{ jOBSTATE = value; }
		}

		/// <summary>
		/// 上次执行日期
		/// </summary>
		public DateTime LASTDTIME
		{
			get{ return lASTDTIME; }
			set{ lASTDTIME = value; }
		}

		/// <summary>
		/// 下次执行日期
		/// </summary>
		public DateTime NEXTDTIME
		{
			get{ return nEXTDTIME; }
			set{ nEXTDTIME = value; }
		}

		/// <summary>
		/// 间隔时间（时间间隔 毫秒级别）
		/// </summary>
		public long INTERVAL
		{
			get{ return iNTERVAL; }
			set{ iNTERVAL = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string OPERCODE
		{
			get{ return oPERCODE; }
			set{ oPERCODE = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime OPERDATE
		{
			get{ return oPERDATE; }
			set{ oPERDATE = value; }
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string MARK
		{
			get{ return mARK; }
			set{ mARK = value; }
		}

		/// <summary>
		/// DLL名字
		/// </summary>
		public string DLLNAME
		{
			get{ return dLLNAME; }
			set{ dLLNAME = value; }
		}

		/// <summary>
		/// 类名
		/// </summary>
		public string CLASSNAME
		{
			get{ return cLASSNAME; }
			set{ cLASSNAME = value; }
		}

        /// <summary>
		/// 执行开始时间
		/// </summary>
		public string JOBSTARTTIME
		{
            get { return jOBSTARTTIME; }
            set { jOBSTARTTIME = value; }
		}

        /// <summary>
        /// 执行结束时间
        /// </summary>
        public string JOBENDTIME
        {
            get { return jOBENDTIME; }
            set { jOBENDTIME = value; }
        }

		#endregion		
	}
}
