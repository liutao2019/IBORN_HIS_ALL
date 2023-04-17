using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ҩƷ��������]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-13'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� �̳���IMAStoreBase����'
	///  />
	/// </summary>
    [Serializable]
    public class Storage : StorageBase
	{
		public Storage () 
		{
			
		}

		#region ����

		private decimal preOutQty;

		private decimal myPreOutCost;

		private decimal lastMonthQty;

		private decimal lowQty;

		private decimal topQty;

		private bool myIsCheck;

		private bool myIsStop;

        private bool myIsLack;

        /// <summary>
        /// �������(����)
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
        /// </summary>
        private string splitType;

        /// <summary>
        /// �������(סԺ��ʱҽ��)
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
        /// </summary>
        private string lZSplitType;

        /// <summary>
        /// �������(סԺ����ҽ��)
        /// 0����С��λ����ȡ��
        /// 1����װ��λ����ȡ��
        /// 2����С��λÿ��ȡ��
        /// 3����װ��λÿ��ȡ��
        /// 4����С��λ�ɲ��
        /// </summary>
        private string cDSplitType;

        /// <summary>
        /// ҩƷ�������
        /// </summary>
        private FS.FrameWork.Models.NeuObject manageQuality = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �������
        /// </summary>
        private decimal negativeQty;       

		#endregion

		/// <summary>
		/// Ԥ��������
		/// </summary>
		public decimal PreOutQty
		{
			get 
			{
				return this.preOutQty;
			}
			set 
			{
				this.preOutQty = value;
			}
		}

		/// <summary>
		/// Ԥ������
		/// </summary>
		public decimal PreOutCost 
		{
			get 
			{
				return myPreOutCost;
			}
			set 
			{
				myPreOutCost = value;
			}
		}
		
		/// <summary>
		/// ��Ϳ����
		/// </summary>
		public decimal LowQty 
		{
			get 
			{
				return this.lowQty;
			}
			set 
			{
				this.lowQty = value;
			}
		}
		
		/// <summary>
		/// ��߿����
		/// </summary>
		public decimal TopQty 
		{
			get 
			{
				return this.topQty;
			}
			set 
			{
				this.topQty = value;
			}
		}

		/// <summary>
		/// ���½������
		/// </summary>
		public decimal LastMonthQty
		{
			get 
			{
				return this.lastMonthQty;
			}
			set 
			{
				lastMonthQty = value;
			}
		}

		/// <summary>
		/// �Ƿ�ͣ�� �����Բ������ݿ��ڻ�ȡ��ͨ��ValidState��ֵ
		/// </summary>
        [Obsolete("�����Բ������ݿ��ڻ�ȡ",false)]
		public bool IsStop 
		{
			get 
			{
				return myIsStop;
			}
			set 
			{
				myIsStop = value;

                if (value)
                {
                    base.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
                }
                else
                {
                    base.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                }
			}
		}

		/// <summary>
		/// �Ƿ�ÿ���̵�
		/// </summary>
		public bool IsCheck 
		{
			get 
			{
				return myIsCheck;
			}
			set 
			{
				myIsCheck = value;
			}
		}

        /// <summary>
        /// �Ƿ�ȱҩ
        /// </summary>
        public bool IsLack
        {
            get
            {
                return this.myIsLack;
            }
            set
            {
                this.myIsLack = value;
            }
        }

        /// <summary>
        /// ҩƷ�������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ManageQuality
        {
            get
            {
                return this.manageQuality;
            }
            set
            {
                this.manageQuality = value;
            }
        }

        /// <summary>
        /// ��Ч��״̬
        /// </summary>
        public new FS.HISFC.Models.Base.EnumValidState ValidState
        {
            get
            {
                return base.ValidState;
            }
            set
            {
                if (value == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    this.myIsStop = false;
                }
                else
                {
                    this.myIsStop = true;
                }

                base.ValidState = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public decimal NegativeQty
        {
            get
            {
                return this.negativeQty;
            }
            set
            {
                this.negativeQty = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public String SplitType
        {
            get
            {
                return this.splitType;
            }
            set
            {
                this.splitType = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public String LZSplitType
        {
            get
            {
                return this.lZSplitType;
            }
            set
            {
                this.lZSplitType = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public String CDSplitType
        {
            get
            {
                return this.cDSplitType;
            }
            set
            {
                this.cDSplitType = value;
            }
        }

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new Storage Clone()
		{
            Storage cloneStorage = base.Clone() as Storage;

            cloneStorage.manageQuality = this.manageQuality.Clone();

            return cloneStorage;
		}

		#endregion  

        #region ��������
        /// <summary>
        /// �Ƿ�������ﻼ��ʹ��
        /// </summary>
        private bool isUseForOutpatient = true;

        /// <summary>
        /// �Ƿ�������ﻼ��ʹ��
        /// </summary>
        public bool IsUseForOutpatient
        {
            get { return isUseForOutpatient; }
            set { isUseForOutpatient = value; }
        }

        /// <summary>
        /// �Ƿ����סԺ����ʹ��
        /// </summary>
        private bool isUseForInpatient = true;

        /// <summary>
        /// �Ƿ����סԺ����ʹ��
        /// </summary>
        public bool IsUseForInpatient
        {
            get { return isUseForInpatient; }
            set { isUseForInpatient = value; }
        }

        /// <summary>
        /// סԺȱҩ��־
        /// </summary>
        private bool isLackForInpatient = false;

        /// <summary>
        /// סԺȱҩ��־
        /// </summary>
        public bool IsLackForInpatient
        {
            get { return isLackForInpatient; }
            set { isLackForInpatient = value; }
        }

        /// <summary>
        /// �Ƿ����ҩ(���ҳ���ҩ)
        /// </summary>
        private bool isRadix = false;

        /// <summary>
        /// �Ƿ����ҩ(���ҳ���ҩ)
        /// </summary>
        public bool IsRadix
        {
            get { return isRadix; }
            set { isRadix = value; }
        }
        #endregion
    }
}
