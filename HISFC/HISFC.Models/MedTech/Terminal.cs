namespace Neusoft.HISFC.Object.MedTech 
{
    /// <summary>
    /// [��������: ҽ���ն�]<br></br>
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
    /// 
    [System.Obsolete("�Ѿ���ʱ�������Ѿ����HISFC.OBJECT.Medtech.Terminal.TerminalApplyInfo", true)]
    public class TerminalOld : Neusoft.HISFC.Object.Base.Spell
	{
		/// <summary>
		/// ���캯��
		/// </summary>
        public TerminalOld() 
		{
			
		}

		#region ����

        
		/// <summary>
		/// ����ҽ����
		/// </summary>
        private MedTech.Management.Group group = new Neusoft.HISFC.Object.MedTech.Management.Group();

		/// <summary>
		/// �豸
		/// </summary>
        private Neusoft.NFC.Object.NeuObject machine = new Neusoft.NFC.Object.NeuObject();

		/// <summary>
		/// �������
		/// </summary>
		private Neusoft.HISFC.Object.RADT.EnumPatientType patientType;

		/// <summary>
		/// ���ߵĿ���
		/// </summary>
		private string cardNO;//last

		/// <summary>
		/// ���ߵı��ξ����
		/// </summary>
		private string currentNO;//last

		/// <summary>
		/// ҽ��
		/// </summary>
		private Neusoft.HISFC.Object.Order.Order order = new Neusoft.HISFC.Object.Order.Order();

		/// <summary>
		/// ҽ����Ŀ����
		/// </summary>
        private Neusoft.HISFC.Object.Base.DeptItem itemProperty = new Neusoft.HISFC.Object.Base.DeptItem();

		//�������ң��Ѿ���MTObject�̳�

		/// <summary>
		/// ��Ŀ�շ�ʱ��
		/// </summary>
		private System.DateTime chargeTime = System.DateTime.MinValue;//last

		// ��Ŀ������Ϣ����ʱû��

		/// <summary>
		/// �Ƿ�����, 1-yes, 0-no;
		/// </summary>
		private string isGroup = "0";//last

		/// <summary>
		/// ִ��ҽԺ
		/// </summary>
        private Neusoft.HISFC.Object.Base.Hospital executeHospital = new Neusoft.HISFC.Object.Base.Hospital();

		/// <summary>
		/// ִ�в�������
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment executeEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();

		/// <summary>
		/// ԤԼ��������
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment precontractEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();

		/// <summary>
		/// ���
		/// </summary>
        private Neusoft.HISFC.Object.Base.Noon noon = new Neusoft.HISFC.Object.Base.Noon();

		/// <summary>
		/// ҽ��ԤԼ���ִ�еص�
		/// </summary>
        private MedTech.Management.Location.Building location = new Neusoft.HISFC.Object.MedTech.Management.Location.Building();//last

		/// <summary>
		/// �Ѿ�ִ�е�����
		/// </summary>
		private decimal alreadyExecuteQty;

		/// <summary>
		/// ���һ��ִ�еĲ�������
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment lastEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();

		/// <summary>
		/// ��Ŀ״̬
		/// </summary>
        private MedTech.Booking.EnumBookingState itemState;

		/// <summary>
		/// ҽ��ԤԼȷ����
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment confirmEnvironment = new Neusoft.HISFC.Object.Base.OperEnvironment();
		
		#endregion

		#region ����

		/// <summary>
		/// ҽ��ԤԼȷ����
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment ConfirmEnvironment
		{
			get
			{
				return this.confirmEnvironment;
			}
			set
			{
				this.confirmEnvironment = value;
			}
		}
		/// <summary>
		/// ��Ŀ�շ�ʱ��
		/// </summary>
		public System.DateTime ChargeTime
		{
			get
			{
				return this.chargeTime;
			}
			set
			{
				this.chargeTime = value;
			}
		}
	
		/// <summary>
		/// ҽ��ԤԼ���ִ�еص�
		/// </summary>
        public MedTech.Management.Location.Building Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		/// <summary>
		/// �Ƿ����ף�1-yes, 0-no
		/// </summary>
		public string IsGroup
		{
			get
			{
				return this.isGroup;
			}
			set
			{
				this.isGroup = value;
			}
		}

		/// <summary>
		/// ���ߵĿ���
		/// </summary>
		public string CardNO
		{
			get
			{
				return this.cardNO;
			}
			set
			{
				this.cardNO = value;
			}
		}

		/// <summary>
		/// ���ߵı��ξ���ſ���
		/// </summary>
		public string CurrentNO
		{
			get
			{
				return this.currentNO;
			}
			set
			{
				this.currentNO = value;
			}
		}

		/// <summary>
		/// �豸
		/// </summary>
        public Neusoft.NFC.Object.NeuObject Machine 
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
		/// �������
		/// </summary>
		public Neusoft.HISFC.Object.RADT.EnumPatientType PatientType 
		{
			get 
			{
				return this.patientType;
			}
			set 
			{
				this.patientType = value;
			}
		}
		
		/// <summary>
		/// ����ҽ����
		/// </summary>
        public MedTech.Management.Group Group 
		{
			get 
			{
				return this.group;
			}
			set 
			{
				this.group = value;
			}
		}

		/// <summary>
		/// ҽ��
		/// </summary>
		public Neusoft.HISFC.Object.Order.Order Order 
		{
			get 
			{
				return this.order;
			}
			set 
			{
				this.order = value;
			}
		}

		/// <summary>
		/// ִ��ҽԺ
		/// </summary>
        public Neusoft.HISFC.Object.Base.Hospital ExecuteHospital 
		{
			get 
			{
				return this.executeHospital;
			}
			set 
			{
				this.executeHospital = value;
			}
		}

		/// <summary>
		/// ִ�в�������
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment ExecuteEnvironment 
		{
			get 
			{
				return this.executeEnvironment;
			}
			set 
			{
				this.executeEnvironment = value;
			}
		}

		/// <summary>
		/// ԤԼ��������
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment PrecontractEnvironment 
		{
			get 
			{
				return this.precontractEnvironment;
			}
			set 
			{
				this.precontractEnvironment = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
        public Neusoft.HISFC.Object.Base.Noon Noon 
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
		/// ҽ����Ŀ����
		/// </summary>
        public Neusoft.HISFC.Object.Base.DeptItem ItemProperty 
		{
			get 
			{
				return this.itemProperty;
			}
			set 
			{
				this.itemProperty = value;
			}
		}

		/// <summary>
		/// �Ѿ�ִ�е�����
		/// </summary>
		public decimal AlreadyExecuteQty 
		{
			get 
			{
				return this.alreadyExecuteQty;
			}
			set 
			{
				this.alreadyExecuteQty = value;
			}
		}

		/// <summary>
		/// ���һ��ִ�еĲ�������
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment LastEnvironment 
		{
			get 
			{
				return this.lastEnvironment;
			}
			set 
			{
				this.lastEnvironment = value;
			}
		}

		/// <summary>
		/// ��Ŀ״̬
		/// </summary>
        public MedTech.Booking.EnumBookingState ItemState 
		{
			get 
			{
				return this.itemState;
			}
			set 
			{
				this.itemState = value;
			}
		}
		#endregion

	}
}
