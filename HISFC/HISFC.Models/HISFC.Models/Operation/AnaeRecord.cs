using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Operation 
{
	/// <summary>
	/// [��������: ����(Anaesthesia)�Ǽ�ʵ����]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
	public class AnaeRecord : NeuObject
	{
		public AnaeRecord()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public AnaeRecord(OperationAppllication operationApplication)
		{
			this.operationApplication = operationApplication;
		}

		private OperationAppllication operationApplication;
		/// <summary>
		/// �������뵥����(�����˾��󲿷�Ҫ�Ǽǵ���Ϣ)
		/// </summary>
		public OperationAppllication OperationApplication
		{
			get
			{
				if (this.operationApplication == null) 
				{
					this.operationApplication = new OperationAppllication();
				}
				return this.operationApplication;
			}
			set
			{
				this.operationApplication = value;
			}
		}
		[Obsolete("OperationApplication",true)]
		public OperationAppllication m_objOpsApp = new OperationAppllication();

		private DateTime anaeDate = DateTime.MinValue;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime AnaeDate
		{
			get
			{
				return this.anaeDate;
			}
			set
			{
				this.anaeDate = value;
			}
		}
		
		private NeuObject anaeResult = new NeuObject();
		/// <summary>
		/// ����Ч��
		/// </summary>
		public FS.FrameWork.Models.NeuObject AnaeResult
		{
			get
			{
				return this.anaeResult;
			}
			set
			{
				this.anaeResult = value;
			}
		}

		private bool isPACU;
		[Obsolete("IsPACU",true)]
		public bool bIsPACU
		{
			get
			{
				return this.isPACU;
			}
			set
			{
				this.isPACU = value;
			}
		}
		/// <summary>
		/// �Ƿ���PACU 1��/0��
		/// </summary>
		public bool IsPACU
		{
			get
			{
				return this.isPACU;
			}
			set
			{
				this.isPACU = value;
			}
		}
		
		private DateTime inPacuDate = DateTime.MinValue;
		/// <summary>
		/// ��(PACU)��ʱ��
		/// </summary>
		public DateTime InPacuDate
		{
			get
			{
				return this.inPacuDate;
			}
			set
			{
				this.inPacuDate = value;
			}
		}

		private DateTime outPacuDate = DateTime.MinValue;
		/// <summary>
		/// ��(PACU)��ʱ��
		/// </summary>
		public DateTime OutPacuDate
		{
			get
			{
				return this.outPacuDate;
			}
			set
			{
				this.outPacuDate = value;
			}
		}

		private NeuObject inPacuStatus = new NeuObject();
		/// <summary>
		/// ��(PACU)��״̬
		/// </summary>
		public FS.FrameWork.Models.NeuObject InPacuStatus
		{
			get
			{
				return this.inPacuStatus;
			}
			set
			{
				this.inPacuStatus = value;
			}
		}

		private NeuObject outPacuStatus = new NeuObject();
		/// <summary>
		/// ��(PACU)��״̬
		/// </summary>
		public FS.FrameWork.Models.NeuObject OutPacuStatus
		{
			get
			{
				return this.outPacuStatus;
			}
			set
			{
				this.outPacuStatus = value;
			}
		}


		/// <summary>
		/// ��ע
		/// </summary>
		[Obsolete("Memo",true)]
		public string Remark = "";

		private bool isDemulcent;
		[Obsolete("IsDemulcent",true)]
		public bool bIsDemulcent
		{
			get
			{
				return this.isDemulcent;
			}
			set
			{
				this.isDemulcent = value;
			}
		}
		/// <summary>
		/// �Ƿ�������ʹ
		/// </summary>
		public bool IsDemulcent
		{
			get
			{
				return this.isDemulcent;
			}
			set
			{
				this.isDemulcent = value;
			}
		}
		
		private NeuObject demulcentType =new NeuObject();
		/// <summary>
		/// ��ʹ��ʽ
		/// </summary>
		public NeuObject DemulcentType
		{
			get
			{
				return this.demulcentType;
			}
			set
			{
				this.demulcentType = value;
			}
		}
		[Obsolete("DemulcentType",true)]
		public FS.FrameWork.Models.NeuObject DemuKind = new FS.FrameWork.Models.NeuObject();
		
		private NeuObject demulcentModel = new NeuObject();
		/// <summary>
		/// ����
		/// </summary>
		public NeuObject DemulcentModel
		{
			get
			{
				return this.demulcentModel;
			}
			set
			{
				this.demulcentModel = value;
			}
		}
		[Obsolete("DemulcentModel",true)]
		public FS.FrameWork.Models.NeuObject DemuModel = new FS.FrameWork.Models.NeuObject();
		
		private int demulcentDays = 0;
		/// <summary>
		/// ��ʹ����
		/// </summary>
		public int DemulcentDays
		{
			get
			{
				return this.demulcentDays;
			}
			set
			{
				this.demulcentDays = value;
			}
		}

		private NeuObject demulcentEffect = new NeuObject();
		/// <summary>
		/// ��ʹЧ��
		/// </summary>
		public NeuObject DemulcentEffect
		{
			get
			{
				return this.demulcentEffect;
			}
			set
			{
				this.demulcentEffect = value;
			}
		}
		[Obsolete("DemulcentEffect",true)]
		public FS.FrameWork.Models.NeuObject DemuResult = new FS.FrameWork.Models.NeuObject();

		private DateTime pullOutDate = DateTime.MinValue;
		/// <summary>
		/// �ι�ʱ��
		/// </summary>
		public DateTime PullOutDate
		{
			get
			{
				return this.pullOutDate;
			}
			set
			{
				this.pullOutDate = value;
			}
		}

		private NeuObject pullOutOperator = new NeuObject();
		/// <summary>
		/// �ι���
		/// </summary>
		public NeuObject PullOutOperator
		{
			get
			{
				return this.pullOutOperator;
			}
			set
			{
				this.pullOutOperator = value;
			}
		}
		[Obsolete("PullOutOperator",true)]
		public FS.FrameWork.Models.NeuObject PullOutOpcd = new FS.FrameWork.Models.NeuObject();


		private bool chargeFlag;
		[Obsolete("IsCharged",true)]
		public bool bChargeFlag
		{
			get
			{
				return this.chargeFlag;
			}
			set
			{
				this.chargeFlag = value;
			}
		}	
		/// <summary>
		/// �Ƿ����
		/// </summary>
		public bool IsCharged
		{
			get
			{
				return this.chargeFlag;
			}
			set
			{
				this.chargeFlag = value;
			}
		}

		private NeuObject execDept = new NeuObject();
		/// <summary>
		/// ִ�п���
		/// </summary>
		public FS.FrameWork.Models.NeuObject ExecDept
		{
			get
			{
				return this.execDept;
			}
			set
			{
				this.execDept = value;
			}
		}
        //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}
        /// <summary>
        /// ����ȥ��
        /// </summary>
        private string direction;
        public string Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }
        //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}
        private string demuDrug;
        /// <summary>
        /// ��ʹ��ҩ
        /// </summary>
        public string DemuDrug
        {
            get
            {
                return this.demuDrug;
            }
            set
            {
                this.demuDrug = value;
            }
        }
	}
}
