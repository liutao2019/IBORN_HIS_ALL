using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ҩƷ��������]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������'
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
	}
}
