using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Order.OutPatient
{
	/// <summary>
	/// FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory<br></br>
	/// [��������: ���ﲡ��ʵ��]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class ClinicCaseHistory:FS.FrameWork.Models.NeuObject
	{

		#region	����

		#region ˽��

		/// <summary>
		/// ����
		/// </summary>
		private string caseMain = "";

		/// <summary>
		/// �ֲ�ʷ
		/// </summary>
		private string caseNow = "";

		/// <summary>
		/// ����ʷ
		/// </summary>
		private string caseOld = "";

		/// <summary>
		/// ����ʷ
		/// </summary>
		private string caseAllery ="";

		/// <summary>
		/// ����
		/// </summary>
		private string checkBody = "";

		/// <summary>
		/// ���
		/// </summary>
		private string caseDiag = "";

		/// <summary>
		/// �Ƿ��Ⱦ
		/// </summary>
		private bool isInfect = false;

		/// <summary>
		/// �Ƿ����
		/// </summary>
		private bool isAllery = false;

        /// <summary>
        /// ��� 1������ 2������
        /// </summary>
        private string moduletype;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private string doctID;

        /// <summary>
        /// ��������
        /// </summary>
        private string deptID;
        /// <summary>
        /// ������������
        /// </summary>
        private OperEnvironment caseOper = new OperEnvironment();
        
		#endregion

		#endregion
		
		#region	����
		/// <summary>
		/// ����
		/// </summary>
		public  string CaseMain
		{
			get 
			{
				return this.caseMain;    
			}
			set 
			{
				this.caseMain = value;
			}
		}

		/// <summary>
		/// �ֲ�ʷ
		/// </summary>
		public string  CaseNow
		{
			get 
			{
				return this.caseNow;	 
			}
			set 
			{
				this.caseNow = value;	 
			}
		}
		/// <summary>
		/// ����ʷ
		/// </summary>
		public  string CaseOld
		{ 
			get 
			{
				return this.caseOld;
			}
			set 
			{
				this.caseOld = value;	 
			}
		}
		/// <summary>
		/// ����ʷ
		/// </summary>
		public  string CaseAllery
		{
			get 
			{
				return this.caseAllery;
			}
			set 
			{
				this.caseAllery = value;	 
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string CheckBody
		{
			get 
			{
				return this.checkBody;    
			}
			set 
			{
				this.checkBody = value;
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public  string CaseDiag
		{ 
			get 
			{
				return this.caseDiag;
			}
			set 
			{
				this.caseDiag = value;	 
			}
		}
		/// <summary>
		/// �Ƿ��Ⱦ
		/// </summary>
		public bool IsInfect
		{
			get 
			{
				return this.isInfect;    
			}
			set 
			{
				this.isInfect = value;	 
			}
		}
		/// <summary>
		/// �Ƿ����
		/// </summary>
		public bool IsAllery
		{
			get 
			{
				return this.isAllery;    
			}
			set 
			{
				this.isAllery = value;	 
			}
		}
        /// <summary>
        /// ��� 1������ 2������
        /// </summary>
        public string ModuleType
        {
            get
            {
                return this.moduletype;
            }
            set
            {
                this.moduletype = value;
            }
        }
        /// <summary>
        /// ����ҽ��
        /// </summary>
        public string DoctID
        {
            get
            {
                return this.doctID;
            }
            set
            {
                this.doctID = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string DeptID
        {
            get
            {
                return this.deptID;
            }
            set
            {
                this.deptID = value;
            }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public OperEnvironment CaseOper
        {
            get
            {
                return this.caseOper;
            }
            set
            {
                this.caseOper = value;
            }
        }

		#endregion
		
		#region ����

		#region	��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new ClinicCaseHistory Clone()
		{
			ClinicCaseHistory obj = this.MemberwiseClone() as ClinicCaseHistory;
			return obj;
		}

		#endregion

		#endregion

	}
}
