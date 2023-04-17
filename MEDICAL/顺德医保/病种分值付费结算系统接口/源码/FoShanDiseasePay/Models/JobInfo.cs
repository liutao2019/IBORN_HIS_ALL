using System;

namespace FoShanDiseasePay.Base
{
	/// <summary>
	/// jobinfo ��ժҪ˵����
	/// </summary>
	public class JobInfo
	{
		public JobInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
		/// ����
		/// </summary>
		public string JOBNAME
		{
			get{ return jOBNAME; }
			set{ jOBNAME = value; }
		}

		/// <summary>
		/// ״̬N_��ִ��, D_ÿ��ִ��һ��, M_�������Ӿ�ִ�еģ�������������̣߳�S_����ִ��
		/// </summary>
		public string JOBSTATE
		{
			get{ return jOBSTATE; }
			set{ jOBSTATE = value; }
		}

		/// <summary>
		/// �ϴ�ִ������
		/// </summary>
		public DateTime LASTDTIME
		{
			get{ return lASTDTIME; }
			set{ lASTDTIME = value; }
		}

		/// <summary>
		/// �´�ִ������
		/// </summary>
		public DateTime NEXTDTIME
		{
			get{ return nEXTDTIME; }
			set{ nEXTDTIME = value; }
		}

		/// <summary>
		/// ���ʱ�䣨ʱ���� ���뼶��
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
		/// ��ע
		/// </summary>
		public string MARK
		{
			get{ return mARK; }
			set{ mARK = value; }
		}

		/// <summary>
		/// DLL����
		/// </summary>
		public string DLLNAME
		{
			get{ return dLLNAME; }
			set{ dLLNAME = value; }
		}

		/// <summary>
		/// ����
		/// </summary>
		public string CLASSNAME
		{
			get{ return cLASSNAME; }
			set{ cLASSNAME = value; }
		}

        /// <summary>
		/// ִ�п�ʼʱ��
		/// </summary>
		public string JOBSTARTTIME
		{
            get { return jOBSTARTTIME; }
            set { jOBSTARTTIME = value; }
		}

        /// <summary>
        /// ִ�н���ʱ��
        /// </summary>
        public string JOBENDTIME
        {
            get { return jOBENDTIME; }
            set { jOBENDTIME = value; }
        }

		#endregion		
	}
}
