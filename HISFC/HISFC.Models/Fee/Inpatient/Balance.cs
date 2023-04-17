using System;
using System.Collections;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.FrameWork.Function;

namespace FS.HISFC.Models.Fee.Inpatient
{
	/// <summary>
	/// Balance<br></br>
	/// [��������: סԺ������]<br></br>
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
	public class Balance : BalanceBase 
	{

        #region ����

        /// <summary>
        /// Ժ��id
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// Ժ����
        /// </summary>
        private string hospital_name;

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
        /// �Ƿ���ʱ��Ʊ��
        /// </summary>
        private bool isTempInvoice = false;

        /// <summary>
        /// Ƿ�ѽ��㴦���־
        /// </summary>
        private string balanceSaveType = string.Empty;

        /// <summary>
        /// Ƿ�ѽ��㴦���־
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment balanceSaveOper = new OperEnvironment();
        /// <summary>
        /// ��Ʊ�ϵĴ�ӡ��
        /// {F6C3D16B-8F52-4449-814C-5262F90C583B}
        /// </summary>
        private string printedInvoiceNO;


        //// {ED91FE21-D029-438f-8A4B-5E6C43A1C990}�����ս�ʱ��--2015-01-23
        /// <summary>
        /// �ս�ʱ��
        /// </summary>
        private DateTime dayTime;
		#endregion

		#region  ����

        /// <summary>
        ///Ժ��id
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }


        /// <summary>
        /// Ժ����
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
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
        /// �Ƿ�Ϊ��ʱ��Ʊ��
        /// </summary>
        public bool IsTempInvoice
        {
            get
            {
                return isTempInvoice;
            }
            set
            {
                isTempInvoice = value;
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
        /// <summary>
        /// ��Ʊ�ϵĴ�ӡ��
        /// 
        /// {F6C3D16B-8F52-4449-814C-5262F90C583B}
        /// </summary>
        public string PrintedInvoiceNO
        {
            get
            {
                return this.printedInvoiceNO;
            }
            set
            {
                this.printedInvoiceNO = value;
            }
        }


        //// {ED91FE21-D029-438f-8A4B-5E6C43A1C990}�����ս�ʱ��--2015-01-23
        /// <summary>
        /// �ս�ʱ��
        /// </summary>
        public DateTime DayTime
        {
            get
            {
                return this.dayTime;
            }
            set
            {
                this.dayTime = value;
            }
        }
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
