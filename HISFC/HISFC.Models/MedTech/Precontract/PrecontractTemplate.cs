namespace Neusoft.HISFC.Object.MedTech.Precontract 
{
    /// <summary>
    /// [��������: ԤԼģ��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006-12-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    public class PrecontractTemplate : MedTech.Base.MTObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PrecontractTemplate( ) 
		{
		}

		#region ����

		/// <summary>
		/// �豸
		/// </summary>
        private MedTech.Management.Machine machine = new Neusoft.HISFC.Object.MedTech.Management.Machine();

		/// <summary>
		/// ����
		/// </summary>
		private string week;

		/// <summary>
		/// ���
		/// </summary>
		private Noon noon = new Noon();

		/// <summary>
		/// �޶�
		/// </summary>
		private int quota;

        /// <summary>
        /// Ԥ�۶�
        /// </summary>
        private int preDeduct;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private string startTime;
        
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private string endTime;

        ///// <summary>
        ///// ģ����Ч����ʼʱ��
        ///// </summary>
        //private Neusoft.HISFC.Object.Base.OperEnvironment startEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();

        ///// <summary>
        ///// ģ����Ч�Ľ�ֹʱ��
        ///// </summary>
        //private Neusoft.HISFC.Object.Base.OperEnvironment endEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();

		#endregion

		#region ����

        ///// <summary>
        ///// ģ����Ч����ʼʱ��
        ///// </summary>
        //public Neusoft.HISFC.Object.Base.OperEnvironment StartEnvironment
        //{
        //    get
        //    {
        //        return this.startEnvironment;
        //    }
        //    set
        //    {
        //        this.startEnvironment = value;
        //    }
        //}

        ///// <summary>
        ///// ģ����Ч�Ľ�ֹʱ��
        ///// </summary>
        //public Neusoft.HISFC.Object.Base.OperEnvironment EndEnvironment
        //{
        //    get
        //    {
        //        return this.endEnvironment;
        //    }
        //    set
        //    {
        //        this.endEnvironment = value;
        //    }
        //}

        /// <summary>
        /// ģ����Ч����ʼʱ��
        /// </summary>
        public string StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        /// <summary>
        /// ģ����Ч�Ľ�ֹʱ��
        /// </summary>
        public string EndTime
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
		/// �豸
		/// </summary>
        public MedTech.Management.Machine Machine 
		{
			get 
			{
				return this.machine;
			}
			set 
			{
				this.machine = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Week 
		{
			get 
			{
				return this.week;
			}
			set 
			{
				this.week = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		public Noon Noon 
		{
			get 
			{
				return this.noon;
			}
			set 
			{
				this.noon = value;
			}
		}

		/// <summary>
		/// �޶�
		/// </summary>
		public int Quota 
		{
			get 
			{
				return this.quota;
			}
			set 
			{
				this.quota = value;
			}
		}

        /// <summary>
        /// Ԥ�۶�
        /// </summary>
        public int PreDeduct
        {
            get
            {
                return this.preDeduct;
            }
            set
            {
                this.preDeduct = value;
            }
        }
		#endregion
		
	}
}
