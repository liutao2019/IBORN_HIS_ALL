namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// PactInfo<br></br>
    /// [��������: ��ͬ��λ��Ϣ������ҵ��ʵ��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2006-08-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class PactCompare : Pact, ISort
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PactCompare()
        {

        }

        #region ����

        /// <summary>
        /// �Ƿ��Ѿ��ͷ���Դ
        /// </summary>
        private bool alreadyDisposed = false;

        /// <summary>
        /// �������
        /// </summary>
        private PayKind payKind = new PayKind();

        private string pactCode;	//		��ͬ��λ����
        private string pactHead;	//		��ͬ��λͳ����ͷ
        private string pactName;	//		��ͬ��λ����
        private string parentPact;	//			������ͬ��λ����

        private string parentName;		//	������ͬ��λ����
        private string pactFlag;		//	//��ͬ��λ����

        private string valldState;			//0��Ч 1��Ч


        /// <summary>
        /// ��ͬ��λ����ҽ������dll����
        /// </summary>
        private string pactDllName = string.Empty;
        /// <summary>
        /// ��ͬ��λ����ҽ������dll����
        /// </summary>
        private string pactDllDescription = string.Empty;
        /// <summary>
        /// ��ͬ��λϵͳ���
        /// ������� 0=ȫԺ��1=���2=סԺ��3=ϵͳ��̨ʹ��
        /// </summary>
        private string pactSystemType = string.Empty;

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        public PayKind PayKind
        {
            get
            {
                return this.payKind;
            }
            set
            {
                this.payKind = value;
            }
        }

        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        public string PactCode
        {
            get { return pactCode; }
            set { pactCode = value; }
        }
        /// <summary>
        /// ��ͬ��λͳ����ͷ
        /// </summary>
        public string PactHead
        {
            get { return pactHead; }
            set { pactHead = value; }
        }
        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        public string PactName
        {
            get { return pactName; }
            set { pactName = value; }
        }
        /// <summary>
        /// ������ͬ��λ����
        /// </summary>
        public string ParentPact
        {
            get { return parentPact; }
            set { parentPact = value; }
        }
        /// <summary>
        /// ������ͬ��λ����
        /// </summary>
        public string ParentName
        {
            get { return parentName; }
            set { parentName = value; }
        }
        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        public string PactFlag
        {
            get { return pactFlag; }
            set { pactFlag = value; }
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        public string ValldState
        {
            get { return valldState; }
            set
            {
                valldState = value;
            }
        }
        /*
		/// <summary>
		/// �Ƿ�Ҫ�������ҽ��֤��
		/// </summary>
		public bool IsNeedMCard
		{
			get
			{
				return this.isNeedMCard;
			}
			set
			{
				this.isNeedMCard = value;
			}
		}
       
		/// <summary>
		/// �Ƿ��ܼ��
		/// </summary>
		public bool IsInControl
		{
			get
			{
				return this.isInControl;
			}
			set
			{
				this.isInControl = value;
			}
		}

		/// <summary>
		/// ��Ŀ����� 0 ȫ��, 1 ҩƷ, 2 ��ҩƷ
		/// </summary>
		public string ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}
         */


        /// <summary>
        /// ��ͬ��λ����ҽ������dll����
        /// </summary>
        public string PactDllName
        {
            get
            {
                return pactDllName;
            }
            set
            {
                pactDllName = value;
            }
        }
        /// <summary>
        /// ��ͬ��λ����ҽ������dll����
        /// </summary>
        public string PactDllDescription
        {
            get
            {
                return pactDllDescription;
            }
            set
            {
                pactDllDescription = value;
            }
        }
        /// <summary>
        /// ��ͬ��λϵͳ���
        /// ������� 0=ȫԺ��1=���2=סԺ��3=ϵͳ��̨ʹ��
        /// </summary>
        public string PactSystemType
        {
            get
            {
                return pactSystemType;
            }
            set
            {
                pactSystemType = value;
            }
        }
        #endregion

        #region ����

        #region �ͷ���Դ

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        /// <param name="isDisposing"></param>
        protected override void Dispose(bool isDisposing)
        {
            if (this.alreadyDisposed)
            {
                return;
            }

            if (this.payKind != null)
            {
                this.payKind.Dispose();
                this.payKind = null;
            }
            /*if (this.rate != null)
            {
                this.rate.Dispose();
                this.rate = null;
            }*/

            base.Dispose(isDisposing);

            this.alreadyDisposed = true;
        }

        #endregion

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>��ǰ����ʵ���ĸ���</returns>
        public new PactInfo Clone()
        {
            PactInfo pactInfo = base.Clone() as PactInfo;

            pactInfo.PayKind = this.PayKind.Clone();
            //pactInfo.Rate = this.Rate.Clone();

            return pactInfo;
        }

        #endregion

        #endregion

        #region �ӿ�ʵ��

        #region ISort ��Ա
        /// <summary>
        /// �������
        /// </summary>
        /*public new int SortID
        {
            get
            {
                return this.sortID ;
            }
            set
            {
                this.sortID = value;
            }
        }*/
        #endregion

        #endregion

    }
}
