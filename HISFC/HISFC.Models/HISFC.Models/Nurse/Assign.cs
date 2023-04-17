using System;

namespace FS.HISFC.Models.Nurse
{
	/// <summary>
	/// Assign<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��ΰ��'
	///		�޸�ʱ��='2007-02-07'
	///		�޸�Ŀ��='��һ��'
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class Assign : FS.FrameWork.Models.NeuObject
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Assign()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// ʵ�ʿ������
		/// </summary>
		private int seeNO = 0; 
		/// <summary>
		/// ��������
		/// </summary>
		private DateTime seeTime = DateTime.MinValue;
		/// <summary>
		/// �������
		/// </summary>
        private string triageDept = "";
		/// <summary>
		/// ����ʱ��
		/// </summary>
        private DateTime triageTime = DateTime.MinValue;
		/// <summary>
		/// ������ʱ��
		/// </summary>
        private DateTime inTime = DateTime.MinValue;
		/// <summary>
		/// ������ʱ��
		/// </summary>
        private DateTime outTime = DateTime.MinValue;
		/// <summary>
		/// ����״̬
		/// </summary>
        private EnuTriageStatus triageStatus = EnuTriageStatus.None;

        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        
		/// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register register =
            new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Nurse.Queue queue = new Queue();
		#endregion


        #region ����

        /// <summary>
		/// ʵ�ʿ������
		/// </summary>
		public int SeeNO
		{
			get
			{
				return this.seeNO;
			}
			set
			{
				this.seeNO = value;
			}
		}

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime SeeTime
        {
            get
            {
                return this.seeTime;
            }
            set
            {
                this.seeTime = value;
            }
        }

		/// <summary>
		/// �������
		/// </summary>
		public string TriageDept
		{
			get
			{
				return this.triageDept;
			}
			set
			{
				this.triageDept = value;
			}
		}

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime TirageTime
        {
            get
            {
                return this.triageTime;
            }
            set
            {
                this.triageTime = value;
            }
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        public DateTime InTime
        {
            get
            {
                return this.inTime;
            }
            set
            {
                this.inTime = value;
            }
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        public DateTime OutTime
        {
            get
            {
                return this.outTime;
            }
            set
            {
                this.outTime = value;
            }
        }

		/// <summary>
		/// ����״̬
		/// </summary>
		public EnuTriageStatus TriageStatus
		{
			get
			{
				return this.triageStatus;
			}
			set
			{
				this.triageStatus = value;
			}
		}

        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
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
        /// ���߹Һ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.register;
            }
            set
            {
                this.register = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Nurse.Queue Queue
        {
            get
            {
                return this.queue;
            }
            set
            {
                this.queue = value;
            }
        }
		#endregion

        #region ����
        /// <summary>
        /// ����Ա
        /// </summary>
        private string operID = "";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime operDate = DateTime.MinValue;

        /// <summary>
        /// ����Ա
        /// </summary>
        [Obsolete("ʹ��Oper.ID", true)]
        public string OperID
        {
            get
            {
                return this.operID;
            }
            set
            {
                this.operID = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("ʹ��Oper.OperTime", true)]
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

        /// <summary>
        /// ������ʱ��
        /// </summary>
        [Obsolete("ʹ��OutTime", true)]
        public DateTime OutDate
        {
            get
            {
                return this.outTime;
            }
            set
            {
                this.outTime = value;
            }
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        [Obsolete("ʹ��InTime", true)]
        public DateTime InDate
        {
            get
            {
                return this.inTime;
            }
            set
            {
                this.inTime = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("ʹ��TriageTime", true)]
        public DateTime TriageDate
        {
            get
            {
                return this.triageTime;
            }
            set
            {
                this.triageTime = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [Obsolete("ʹ��SeeTime", true)]
        public DateTime SeeDate
        {
            get
            {
                return this.seeTime;
            }
            set
            {
                this.seeTime = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Assign Clone()
        {
            Assign assign = base.Clone() as Assign;
            assign.oper = this.oper.Clone();
            assign.register = this.register.Clone();
            assign.queue = this.queue.Clone();
            return assign;
        }

		#endregion

	}

	#region ö��
	/// <summary>
	/// ����״̬
	/// </summary>
	public enum EnuTriageStatus
	{
		/// <summary>
		/// ������
		/// </summary>
		None,
		/// <summary>
		/// �ѷ���
		/// </summary>
		Triage,
		/// <summary>
		/// �ѽ���
		/// </summary>
		In,
		/// <summary>
		/// �ѳ���
		/// </summary>
		Out
	}
	#endregion
}
