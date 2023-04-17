using System;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// PPRBase<br></br>
	/// [��������: �Ƽ��������]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-14]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class PPRBase:FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PPRBase()
		{

		}

		#region ����

		/// <summary>
		/// ��Ʒ
		/// </summary>
        private FS.HISFC.Models.Pharmacy.Item drug = new Pharmacy.Item();

		/// <summary>
		/// �����ƻ���� 
		/// </summary>
		private string planNO = "";
		
		/// <summary>
		/// ״̬ 0 �ƻ� 1 ���� 2 ���Ʒ��װ 3 ���Ʒ���� 4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���
		/// </summary>
		private EnumState state = EnumState.Plan;
		
		/// <summary>
		/// �ƻ���Һ��
		/// </summary>
		private decimal planQty;
		
		/// <summary>
		/// ������
		/// </summary>
		private decimal assayQty;

		/// <summary>
		/// �Ƽ� ��λ
		/// </summary>
		private string unit;

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		private string batchNO;

		/// <summary>
		/// �Ƿ��峡 
		/// </summary>
		private bool isClear;

		/// <summary>
		/// �豸�Ƿ����
		/// </summary>
		private bool isWhole;

		/// <summary>
		/// �豸�Ƿ����
		/// </summary>
		private bool isCleanness;

		/// <summary>
		/// �����
		/// </summary>
		private string regulations;

		/// <summary>
		/// �������
		/// </summary>
		private string quality;

		/// <summary>
		/// ����ִ�����
		/// </summary>
		private string execute;

		/// <summary>
		/// ������Ϣ--��Ա������
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment operEnv = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��ע
		/// </summary>
		private string mark;

		/// <summary>
		/// ��չ���
		/// </summary>
		private string extend1;

		/// <summary>
		/// ��չ���1
		/// </summary>
		private string extend2;

		/// <summary>
		/// ��չ���2
		/// </summary>
		private string extend3;


		#endregion

		#region ����
		/// <summary>
		/// �Ƽ���Ʒ
		/// </summary>
        public Pharmacy.Item Drug
		{
			get
			{
				return this.drug;
			}
			set
			{
				this.drug = value;
			}
		}


		/// <summary>
		/// �����ƻ����
		/// </summary>
		public string PlanNO
		{
			get
			{
				return this.planNO;
			}
			set
			{
				this.ID = value;
                this.planNO = value;
			}
		}


		/// <summary>
		/// ״̬ 0 �ƻ� 1 ���� 2 ���Ʒ���� 3 ���Ʒ��װ 4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���
		/// </summary>
		public EnumState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}


		/// <summary>
		/// �ƻ���Һ��
		/// </summary>
		public decimal PlanQty
		{
			get
			{
				return this.planQty;
			}
			set
			{
                this.planQty = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public decimal AssayQty
		{
			get
			{
				return this.assayQty;
			}
			set
			{
				this.assayQty = value;
			}
		}


		/// <summary>
		/// �Ƽ� ��λ
		/// </summary>
		public string Unit
		{
			get
			{
				return this.unit;
			}
			set
			{
				this.unit = value;
			}
		}

        
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public string BatchNO
		{
			get
			{
				return this.batchNO;
			}
			set
			{
				this.batchNO = value;
			}
		}


		/// <summary>
		/// �Ƿ��峡
		/// </summary>
		public bool IsClear
		{
			get
			{
				return this.isClear;
			}
			set
			{
				this.isClear = value;
			}
		}


		/// <summary>
		/// �豸�Ƿ����
		/// </summary>
		public bool IsWhole
		{
			get
			{
				return this.isWhole;
			}
			set
			{
				this.isWhole = value;
			}
		}

		/// <summary>
		/// �豸�Ƿ����
		/// </summary>
		public bool IsCleanness
		{
			get
			{
				return this.isCleanness;
			}
			set
			{
				this.isCleanness = value;
			}
		}


		/// <summary>
		/// �����
		/// </summary>
		public string Regulations
		{
			get
			{
				return this.regulations;
			}
			set
			{
				this.regulations = value;
			}
		}

		
		/// <summary>
		/// �������
		/// </summary>
		public string Quality
		{
			get
			{
				return this.quality;
			}
			set
			{
				this.quality = value;
			}
		}


		/// <summary>
		/// ����ִ�����
		/// </summary>
		public string Execute
		{
			get
			{
				return this.execute;
			}
			set
			{
				this.execute = value;
			}
		}


		/// <summary>
		/// ������Ϣ--��Ա������
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment OperEnv
		{
			get
			{
				return this.operEnv;
			}
			set
			{
				this.operEnv = value;
			}
		}

		/// <summary>
		/// ��չ���1
		/// </summary>
		public string Extend1
		{
			get
			{
				return this.extend1;
			}
			set
			{
				this.extend1 = value;
			}
		}

		/// <summary>
		/// ��չ���2
		/// </summary>
		public string Extend2
		{
			get
			{
				return this.extend2;
			}
			set
			{
				this.extend2 = value;
			}
		}
		/// <summary>
		/// ��չ���3
		/// </summary>
		public string Extend3
		{
			get
			{
				return this.extend3;
			}
			set
			{
				this.extend3 = value;
			}
		}


		#endregion

		#region ��Ч����		

		/// <summary>
		/// ��ע
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��NeuObject������", true)]
		public string Mark
		{
			get
			{
				return this.mark;
			}
			set
			{
				this.mark = value;
			}
		}
		/// <summary>
		/// ��չ���2
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��Extend3", true)]
		public string ExtFlag2
		{
			get
			{
				return this.extend3;
			}
			set
			{
				this.extend3 = value;
			}
		}

		/// <summary>
		/// ��չ���
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��Extend1", true)]
		public string ExtFlag
		{
			get
			{
				return this.extend1;
			}
			set
			{
				this.extend1 = value;
			}
		}
		/// <summary>
		/// ��չ���1
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��Extend2", true)]
		public string ExtFlag1
		{
			get
			{
				return this.extend2;
			}
			set
			{
				this.extend2 = value;
			}
		}
		/// <summary>
		/// �����ƻ����
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PlanNO", true)]
		public string PlanNo
		{
			get
			{
				return this.planNO;
			}
			set
			{
				this.ID = value;
				this.planNO = value;
			}
		}
		
		/// <summary>
		/// �ƻ���Һ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PlanQty", true)]
		public decimal PlanNum
		{
			get
			{
				return this.planQty;
			}
			set
			{
				this.planQty = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��AssayQty", true)]
		public decimal AssayNum
		{
			get
			{
				return this.assayQty;
			}
			set
			{
				this.assayQty = value;
			}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[System.Obsolete("�������� ����ΪBatchNO����",true)]
		public string BatchNo
		{
			get
			{
				return this.batchNO;
			}
			set
			{
				this.batchNO = value;
			}
		}


		#region ����Ҫ��
		//		/// <summary>
		//		/// ����Ա
		//		/// </summary>
		//		public string OperCode;
		//		/// <summary>
		//		/// ����ʱ��
		//		/// </summary>
		//		public DateTime OperDate;
		#endregion



		#endregion

		#region ����

		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>PPRBase</returns>
		public new PPRBase Clone()
		{
			PPRBase pprbase = base.Clone() as PPRBase;
			pprbase.drug = this.drug.Clone();
			pprbase.operEnv = this.operEnv.Clone();
			return pprbase;
		}

		#endregion
	}
}