namespace Neusoft.HISFC.Object.MedTech.Base 
{
    /// <summary>
    /// [��������: ҽ����Ա]<br></br>
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
	public class MTObject : Neusoft.NFC.Object.NeuObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public MTObject( ) 
		{

		}

		#region ����


		///<summary>
		/// ��Ч��
		/// </summary>
		///  <link>association</link>
        private MedTech.Enum.EnumValidity validity;

		/// <summary>
		/// ����
		/// </summary>
		private string code;

        /// <summary>
        /// �����
        /// </summary>
        private string wbCode;

        /// <summary>
        /// ƴ����
        /// </summary>
        private string spellCode;

        /// <summary>
        /// �Զ�����
        /// </summary>
        private string userCode;

		///<summary>
		/// ����Ա����ҽԺ
		/// </summary>
		private Hospital hospital;

		///<summary>
		/// ��������
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment createEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();

		///<summary>
		/// ��Ч��������
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment invalidEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();

		/// <summary>
		/// ��Ч��������
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment validEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();


		#endregion

		#region ����

        /// <summary>
        /// �����
        /// </summary>
        public string WbCode
        {
            get
            {
                return this.wbCode;
            }
            set
            {
                this.wbCode = value;
            }
        }

        /// <summary>
        /// ƴ����
        /// </summary>
        public string SpellCode
        {
            get
            {
                return this.spellCode;
            }
            set
            {
                this.spellCode = value;
            }
        }

        /// <summary>
        /// �Զ�����
        /// </summary>
        public string UserCode
        {
            get
            {
                return this.userCode;
            }
            set
            {
                this.userCode = value;
            }
        }
		/// <summary>
		/// ��Ч��
		/// </summary>
        public MedTech.Enum.EnumValidity Validity
        {
			get 
			{
				return this.validity;
			}
			set 
			{
				this.validity = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Code 
		{
			get 
			{
				return this.code;
			}
			set 
			{
				this.code = value;
			}
		}

		///<summary>
		/// ����Ա����ҽԺ
		/// </summary>
		public Hospital Hospital 
		{
			get 
			{
				return this.hospital;
			}
			set 
			{
				this.hospital = value;
			}
		}

		///<summary>
		/// ��������
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment CreateEnvironment 
		{
			get 
			{
				return this.createEnvironment;
			}
			set 
			{
				this.createEnvironment = value;
			}
		}

		///<summary>
		/// ��Ч��������
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment InvalidEnvironment 
		{
			get 
			{
				return this.invalidEnvironment;
			}
			set 
			{
				this.invalidEnvironment = value;
			}
		}

		/// <summary>
		/// ��Ч��������
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment ValidEnvironment 
		{
			get 
			{
				return this.validEnvironment;
			}
			set 
			{
				this.validEnvironment = value;
			}
		}

		#endregion

		#region ����
		#endregion
	}
}
