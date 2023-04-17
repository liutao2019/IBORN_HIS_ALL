
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// FTRate<br></br>
	/// [��������: ���ַ��ñ���ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-08-30]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-18'
	///		�޸�Ŀ��='����ItemRate���ԣ��޸Ĵ����ʽ���淶��'
	///		�޸�����='ItemRate����: ��Ŀ���շѱ���'
	///  />
	/// </summary>
    [System.Serializable]
    public class FTRate : FS.FrameWork.Models.NeuObject
	{
		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;
		
		/// <summary>
		/// ����ӳɱ���
		/// </summary>
		private decimal emcRate;
		
		/// <summary>
		/// ��Ŀ�շѱ���
		/// </summary>
		private decimal itemRate;
		
		/// <summary>
		/// �Էѱ���
		/// </summary>
		private decimal ownRate;

		/// <summary>
		/// �Ը�����
		/// </summary>
		private decimal payRate;

		/// <summary>
		/// ���ѱ���
		/// </summary>
		private decimal pubRate;

		/// <summary>
		/// �������
		/// </summary>
		private decimal derateRate;

		/// <summary>
		/// Ƿ�ѱ���
		/// </summary>
		private decimal arrearageRate;

		/// <summary>
		/// �Żݱ���
		/// </summary>
		private decimal rebateRate;

		/// <summary>
		/// Ӥ������
		/// </summary>
		private bool isBabyShared;

        /// <summary>
        /// �޶�
        /// </summary>
        private decimal quota;

		#endregion

		#region ����

		/// <summary>
		/// ����ӳɱ���
		/// </summary>
		public decimal EMCRate
		{
			get
			{
				return this.emcRate;
			}
			set
			{
				this.emcRate = value;
			}
		}
		
		/// <summary>
		/// ��Ŀ�շѱ���
		/// </summary>
		public decimal ItemRate
		{
			get
			{
				return this.itemRate;
			}
			set
			{
				this.itemRate = value;
			}
		}

		/// <summary>
		/// �Էѱ���
		/// </summary>
		public decimal OwnRate
		{
			get
			{
				return this.ownRate;
			}
			set
			{
				this.ownRate = value;
			}
		}

		/// <summary>
		/// �Ը�����
		/// </summary>
		public decimal PayRate
		{
			get
			{
				return this.payRate;
			}
			set
			{
				this.payRate = value;
			}
		}
		
		/// <summary>
		/// ���ѱ���
		/// </summary>
		public decimal PubRate
		{
			get
			{
				return this.pubRate;
			}
			set
			{
				this.pubRate = value;
			}
		}
		
		/// <summary>
		/// �������
		/// </summary>
		public decimal DerateRate
		{
			get
			{
				return this.derateRate;
			}
			set
			{
				this.derateRate = value;
			}
		}
		
		/// <summary>
		/// Ƿ�ѱ���
		/// </summary>
		public decimal ArrearageRate
		{
			get
			{
				return this.arrearageRate;
			}
			set
			{
				this.arrearageRate = value;
			}
		}
		
		/// <summary>
		/// �Żݱ���
		/// </summary>
		public decimal RebateRate
		{
			get
			{
				return this.rebateRate;
			}
			set
			{
				this.rebateRate = value;
			}
		}
		
		/// <summary>
		/// �Ƿ�Ӥ������
		/// </summary>
		public bool IsBabyShared
		{
			get
			{
				return this.isBabyShared;
			}
			set
			{
				this.isBabyShared = value;
			}
		}

        /// <summary>
        /// �޶�
        /// </summary>
        public decimal Quota
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

		#endregion

		#region ����
		
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
			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ��</returns>
		public new FTRate Clone()
		{
			FTRate ftRate = base.Clone() as FTRate;

			return ftRate;
		}

		#endregion
	}
}
