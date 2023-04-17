using System;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// Preparation<br></br>
	/// [��������: �Ƽ����� ����]<br></br>
	/// [�� �� ��: ]<br></br>
	/// [����ʱ��: 2006-09-14]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Preparation:PPRBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Preparation()
		{
		}

		#region ����

        /// <summary>
        /// �ƻ��ƶ�---�ˣ�����
        /// </summary>
		private FS.HISFC.Models.Base.OperEnvironment planEnv = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ����--�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment confectEnv = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        ///  ���Ʒ��(������)
        /// </summary>
        private decimal confectQty;

		/// <summary>
		/// �����Ƿ�ϸ�  Ĭ�Ϻϸ�
		/// </summary>
		private bool isAssayEligible = true;

		/// <summary>
		/// ����---�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment assayEnv = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ���״̬ 0 ����� 1 ��ʽ���
		/// </summary>
		private string inputState;

		/// <summary>
		/// �������
		/// </summary>
		private decimal inputQty;

		/// <summary>
		/// ���---�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment inputEnv = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ������
		/// </summary>
		private string checkResult;

		/// <summary>
		/// ���Ա
		/// </summary>
		private string checkOper;

        /// <summary>
        /// �������̱�־
        /// </summary>
        private string processState;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private DateTime validDate = System.DateTime.MaxValue;

        /// <summary>
        /// �ɱ���
        /// </summary>
        private decimal costPrice;
		#endregion

		#region  ����

		/// <summary>
		/// �������Ƿ�ϸ� Ĭ�Ϻϸ�
		/// </summary>
		public bool IsAssayEligible
		{
			get
			{
				return this.isAssayEligible;
			}
			set
			{
				this.isAssayEligible = value;
			}
		}

		/// <summary>
		/// ���״̬ 0 ����� 1 ��ʽ���
		/// </summary>
		public string InputState
		{
			get
			{
				return this.inputState;
			}
			set
			{
				this.inputState = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public decimal InputQty
		{
			get
			{
				return this.inputQty;
			}
			set
			{
				this.inputQty = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string CheckResult
		{
			get
			{
				return this.checkResult;
			}
			set
			{
				this.checkResult = value;
			}
		}

		/// <summary>
		/// ���Ա
		/// </summary>
		public string CheckOper
		{
			get			
			{
				return this.checkOper;
			}
			set
			{
				this.checkOper = value;
			}
		}

        /// <summary>
        /// ���Ʒ��(������)
        /// </summary>
        public decimal ConfectQty
        {
            get
            {
                return this.confectQty;
            }
            set
            {
                this.confectQty = value;
            }
        }

		/// <summary>
		/// ���---�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment InputEnv
		{
			get
			{
				return this.inputEnv;
			}
			set
			{
				this.inputEnv = value;
			}
		}

		/// <summary>
		/// ����---�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment AssayEnv
		{
			get
			{
				return this.assayEnv;
			}
			set
			{
				this.assayEnv = value;
			}
		}

		/// <summary>
		/// �ƻ��ƶ�---�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment PlanEnv
		{
			get
			{
				return this.planEnv;
			}
			set
			{
				this.planEnv = value;
			}
		}

		/// <summary>
		/// ����--�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment ConfectEnv
		{
			get
			{
				return this.confectEnv;
			}
			set
			{
				this.confectEnv = value;
			}
		}

        /// <summary>
        /// �������̱�־
        /// </summary>
        public string ProcessState
        {
            get
            {
                return this.processState;
            }
            set
            {
                this.processState = value;
            }
        }

        /// <summary>
        /// ��Ч�ڡ����������ݴ��ݡ����洢���ݿ�
        /// </summary>
        public DateTime ValidDate
        {
            get
            {
                return this.validDate;
            }
            set
            {
                this.validDate = value;
            }
        }

        /// <summary>
        /// �ɱ���
        /// </summary>
        public decimal CostPrice
        {
            get
            {
                return this.costPrice;
            }
            set
            {
                this.costPrice = value;
            }
        }

		#endregion

		#region ����

		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>Preparation</returns>
		public new Preparation Clone()
		{
			Preparation preparation = base.Clone() as Preparation;
			preparation.inputEnv = this.inputEnv.Clone();
			preparation.planEnv = this.planEnv.Clone();
			preparation.confectEnv = this.confectEnv.Clone();
			preparation.assayEnv = this.assayEnv.Clone();
			return preparation;
		}

		#endregion

		#region  ��������
		/// <summary>
		/// �������
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��InputQty", true)]
		public decimal InputNum
		{
			get
			{
				return 0;
			}
			set
			{
			//	this.inputNum = value;
			}
		}
		/// <summary>
		/// �ƻ��ƶ���
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PlanEnv", true)]
		public string PlanOper
		{
			get
			{
				return null;
			}
			set
			{
				//this.planOper = value;
			}
		}
		/// <summary>
		/// �ƻ�ʱ�� 
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PlanEnv", true)]
		public DateTime PlanDate
		{
			get
			{
				return DateTime.Now;
			}
			set
			{
				//this.planDate = value;
			}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ConfectEnv", true)]
		public string ConfectOper
		{
			get
			{
				return null;
			}
			set
			{
				//this.confectOper = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ConfectEnv", true)]
		public DateTime ConfectDate
		{
			get
			{
				return DateTime.Now;
			}
			set
			{
				//this.confectDate = value;
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��AssayEnv", true)]
		public string AssayOper
		{
			get
			{
				return null;
			}
			set
			{
				//this.assayOper = value;
			}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��AssayEnv", true)]
		public DateTime AssayDate
		{
			get
			{
				return DateTime.Now;
			}
			set
			{
				//this.assayDate = value;
			}
		}
		/// <summary>
		/// ������Ա
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��InputEnv", true)]
		public string InputOper
		{
			get
			{
				return null;
			}
			set
			{
				//this.inputOper = value;
			}
		}
		/// <summary>
		/// ���ʱ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��InputEnv", true)]
		public DateTime InputDate
		{
			get
			{
				return DateTime.Now;
			}
			set
			{
				//this.inputDate = value;
			}
		}
		#endregion
	}
}
