using System;

namespace FS.HISFC.Models.Pharmacy 
{
	/// <summary>
	/// [��������: ҩƷ����������Ϣ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� �̳���ApplyBase����'
	///  />
	///  ID �������
	/// </summary>
    [Serializable]
    public class ApplyOut : FS.HISFC.Models.IMA.IMAApplyBase
	{
		public ApplyOut ()
		{
			
		}

		#region ����

		/// <summary>
		/// ҩƷʵ��
		/// </summary>
		private FS.HISFC.Models.Pharmacy.Item item = new Item();

		/// <summary>
		/// ���� ��ҩ
		/// </summary>
		private decimal days;

		/// <summary>
		/// ��λ��ʾ״̬ 1 ��װ��λ 0 ��С��λ
		/// </summary>
		private string showState;

		/// <summary>
		/// ��ʾ��λ
		/// </summary>
		private string showUnit;

		/// <summary>
		/// ����
		/// </summary>
		private decimal myGroupNo;

		/// <summary>
		/// ����
		/// </summary>
		private string myBatchNo = "";

		/// <summary>
		/// ��ҩ����
		/// </summary>
		private string drugNO;

		/// <summary>
		/// Ԥ�ۿ��״̬
		/// </summary>
		private bool isPreOut;

		/// <summary>
		/// �շ�״̬
		/// </summary>
		private bool isCharge;

		/// <summary>
		/// ����ID
		/// </summary>
		private string  patientNO;

		/// <summary>
		/// �������ڿ���
		/// </summary>
		private FS.FrameWork.Models.NeuObject patientDept= new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ÿ�μ���
		/// </summary>
		private decimal doseOnce;

		/// <summary>
		/// �÷�
		/// </summary>
		private FS.FrameWork.Models.NeuObject myUsage = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// Ƶ��
		/// </summary>
		private FS.HISFC.Models.Order.Frequency myFrequency = new FS.HISFC.Models.Order.Frequency();

		/// <summary>
		/// ҽ������
		/// </summary>
		private FS.HISFC.Models.Order.OrderType myOrderType = new FS.HISFC.Models.Order.OrderType();

		/// <summary>
		/// ҽ����
		/// </summary>
		private string  myOrderNo;

		/// <summary>
		/// ��Ϻ�
		/// </summary>
		private string  myCombNo;

		/// <summary>
		/// ִ�е���ˮ��
		/// </summary>
		private string  myExecNo;

		/// <summary>
		/// ������
		/// </summary>
		private string  myRecipeNo;

		/// <summary>
		/// ��������Ŀ��ˮ��
		/// </summary>
		private int mySequenceNo;

		/// <summary>
		/// ��������
		/// </summary>
		private int mySendType;

		/// <summary>
		/// ��ҩ���������
		/// </summary>
		private string myBillClassNo;

		/// <summary>
		/// ��ӡ״̬
		/// </summary>
		private string myPrintState;

		/// <summary>
		/// ���ⵥ�ݺ�
		/// </summary>
		private string myOutBillNo = "0";

		/// <summary>
		/// ��λ��
		/// </summary>
		private string myPlaceNo;

        /// <summary>
        /// ��ҩ���ں� ֻ����ʵ�����ݴ洢 ���������ݿ����
        /// </summary>
        private string sendWindow = "";

        /// <summary>
        /// Ժע���� ֻ����ʵ�����ݴ洢 ���������ݿ����
        /// </summary>
        private int injectQty;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment recipeInfo = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �Ƿ�Ӥ��
        /// </summary>
        private bool isBaby;
   
        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        private string extFlag;

        /// <summary>
        /// ��չ�ֶ�1
        /// </summary>
        private string extFlag1;

        /// <summary>
        /// ��Һ��Ϣ��
        /// </summary>
        private Order.Compound compound = new FS.HISFC.Models.Order.Compound();

        /// <summary>
        /// ����������ˮ�� ���κ� + ҽ����Ϻ�
        /// </summary>
        private string compoundGroup;

        /// <summary>
        /// ҽ��ʹ��ʱ��
        /// </summary>
        private DateTime useTime = System.DateTime.MinValue;

        /// <summary>
        /// Э��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject drugNostrum = new FS.FrameWork.Models.NeuObject();
		#endregion

		/// <summary>
		/// Itemʵ��
		/// </summary>
		public FS.HISFC.Models.Pharmacy.Item Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
				base.IMAItem = value;
			}
		}

		/// <summary>
		/// ���� ��ҩ
		/// </summary>
		public decimal Days
		{
			get
			{
				return this.days;
			}
			set
			{
				this.days = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public decimal GroupNO 
		{
			get	
			{
				return  myGroupNo;
			}
			set	
			{  
				myGroupNo = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string BatchNO 
		{
			get	
			{
				return  myBatchNo;
			}
			set	
			{
				myBatchNo = value;
			}
		}

		/// <summary>
		/// ��λ��ʾ״̬ 1 ��װ��λ 0 ��С��λ
		/// </summary>
		public string ShowState
		{
			get
			{
				return this.showState;
			}
			set
			{
				this.showState = value;
			}
		}

		/// <summary>
		/// ��ʾ��λ
		/// </summary>
		public string ShowUnit
		{
			get
			{
				return this.showUnit;
			}
			set
			{
				this.showUnit = value;
			}
		}

		/// <summary>
		/// Ԥ�ۿ��״̬��falseδ�ۿ�棬true�ѿۿ�棩
		/// </summary>
		public bool IsPreOut 
		{
			get	
			{
				return this.isPreOut;
			}
			set	
			{ 
				this.isPreOut = value; 
			}
		}

		/// <summary>
		/// �շ�״̬��falseδ�շѣ�true���շѣ�
		/// </summary>
		public bool IsCharge
		{
			get	
			{
				return  this.isCharge;
			}
			set	
			{
				this.isCharge = value;
			}
		}

		/// <summary>
		/// ����ID�ţ����￨�Ż���סԺ�ţ�
		/// </summary>
		public string PatientNO
		{
			get	
			{
				return  this.patientNO;
			}
			set	
			{ 
				this.patientNO = value; 
			}
		}

		/// <summary>
		/// �������ڲ�����������ҩ�Ĳ�����
		/// </summary>
		public FS.FrameWork.Models.NeuObject PatientDept 
		{
			get	
			{ 
				return  this.patientDept;
			}
			set	
			{ 
				this.patientDept = value;
			}
		}

		/// <summary>
		/// ÿ�μ���
		/// </summary>
		public decimal DoseOnce 
		{
			get	
			{ 
				return  this.doseOnce;
			}
			set	
			{  
				this.doseOnce = value; 
			}
		}

		/// <summary>
		/// �÷�
		/// </summary>
		public FS.FrameWork.Models.NeuObject Usage 
		{
			get	
			{
				return  myUsage;
			}
			set	
			{  
				myUsage = value; 
			}
		}

		/// <summary>
		/// Ƶ��
		/// </summary>
		public FS.HISFC.Models.Order.Frequency Frequency 
		{
			get	
			{
				return  myFrequency;
			}
			set	
			{  
				myFrequency = value; 
			}
		}

		/// <summary>
		/// ҽ������
		/// </summary>
		public FS.HISFC.Models.Order.OrderType OrderType 
		{
			get	
			{
				return  myOrderType;
			}
			set	
			{  
				myOrderType = value;
			}
		}

		/// <summary>
		/// ҽ����ˮ��
		/// </summary>
		public string OrderNO 
		{
			get	
			{
				return  myOrderNo;
			}
			set	
			{ 
				myOrderNo = value; 
			}
		}

		/// <summary>
		/// ҽ��������
		/// </summary>
		public string CombNO 
		{
			get	
			{
				return  myCombNo;
			}
			set	
			{
				myCombNo = value; 
			}
		}

		/// <summary>
		/// ִ�е���ˮ��
		/// </summary>
		public string ExecNO 
		{
			get	
			{
				return  myExecNo;
			}
			set	
			{
				myExecNo = value; 
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string RecipeNO 
		{
			get	
			{
				return  myRecipeNo;
			}
			set	
			{
				myRecipeNo = value;
			}
		}

		/// <summary>
		/// ��������Ŀ��ˮ��
		/// </summary>
		public int SequenceNO 
		{
			get	
			{
				return  mySequenceNo;
			}
			set	
			{
				mySequenceNo = value; 
			}
		}

		/// <summary>
		/// ��������0-��ʱ���ͣ�1-���з���
		/// </summary>
		public int SendType 
		{
			get
			{
				return this.mySendType;
			}
			set
			{
				this.mySendType = value;
			}
		}

		/// <summary>
		/// ��ҩ���������
		/// </summary>
		public string BillClassNO 
		{
			get
			{ 
				return this.myBillClassNo; 
			}
			set
			{ 
				this.myBillClassNo = value; 
			}
		}

		/// <summary>
		/// ��ҩ����
		/// </summary>
		public string DrugNO
		{
			get
			{
				return this.drugNO;
			}
			set
			{
				this.drugNO = value;
			}
		}

		/// <summary>
		/// ��ӡ״̬��0δ��ӡ��1�Ѵ�ӡ��
		/// </summary>
		public string PrintState 
		{
			get
			{
				return this.myPrintState;
			}
			set
			{
				this.myPrintState = value;
			}
		}

		/// <summary>
		/// ���ⵥ��
		/// </summary>
		public string OutBillNO 
		{
			get	
			{ 
				return  this.myOutBillNo;
			}
			set	
			{  
				this.myOutBillNo = value; 
			}
		}

		/// <summary>
		/// ��λ��
		/// </summary>
		public string PlaceNO
		{
			get	
			{ 
				return  myPlaceNo;
			}
			set	
			{ 
				myPlaceNo = value; 
			}
		}

        /// <summary>
        /// ��ҩ���ں� ֻ����ʵ�����ݴ洢 ���������ݿ����
        /// </summary>
        public string SendWindow
        {
            get
            {
                return this.sendWindow;
            }
            set
            {
                this.sendWindow = value;
            }
        }

        /// <summary>
        /// Ժע���� ֻ����ʵ�����ݴ洢 ���������ݿ����
        /// </summary>
        public int InjectQty
        {
            get
            {
                return this.injectQty;
            }
            set
            {
                this.injectQty = value;
            }
        }

        /// <summary>
        ///  ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment RecipeInfo
        {
            get
            {
                return this.recipeInfo;
            }
            set
            {
                this.recipeInfo = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ӥ��
        /// </summary>
        public bool IsBaby
        {
            get
            {
                return isBaby;
            }
            set
            {
                isBaby = value;
            }
        }

        /// <summary>
        /// ��Һ��Ϣ��
        /// </summary>
        public FS.HISFC.Models.Order.Compound Compound
        {
            get
            {
                return this.compound;
            }
            set
            {
                this.compound = value;
            }
        }

        /// <summary>
        /// ����������ˮ�� ���κ� + ҽ����Ϻ�
        /// </summary>
        public string CompoundGroup
        {
            get
            {
                return this.compoundGroup;
            }
            set
            {
                this.compoundGroup = value;
            }
        }

        /// <summary>
        /// ҽ��ʹ��ʱ�� ����Ч���ϵ������¼Ϊ����ʱ��
        /// </summary>
        public DateTime UseTime
        {
            get
            {
                return this.useTime;
            }
            set
            {
                this.useTime = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        public string ExtFlag
        {
            get
            {
                return extFlag;
            }
            set
            {
                extFlag = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�1
        /// </summary>
        public string ExtFlag1
        {
            get
            {
                return extFlag1;
            }
            set
            {
                extFlag1 = value;
            }
        }

        /// <summary>
        /// Э��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject NostrumDrug
        {
            get
            {
                return this.drugNostrum;
            }
            set
            {
                this.drugNostrum = value;
            }
        }

		#region ����

		public new ApplyOut Clone()
		{
			ApplyOut applyOut = base.Clone() as ApplyOut;

			applyOut.PatientDept = this.PatientDept.Clone();
			applyOut.Usage = this.Usage.Clone();
			applyOut.Frequency = this.Frequency.Clone();
			applyOut.OrderType = this.OrderType.Clone();
            applyOut.RecipeInfo = this.recipeInfo.Clone();
            applyOut.Compound = this.compound.Clone();
            applyOut.item = this.item.Clone();
            applyOut.NostrumDrug = this.NostrumDrug.Clone();

			return applyOut;

		}

		#endregion

		#region ��Ч����

		/// <summary>
		/// ���������(������)
		/// </summary>
		private FS.FrameWork.Models.NeuObject myTargetDept = new FS.FrameWork.Models.NeuObject();

		//˽���ֶ�
		private string  myDrugBill;

		/// <summary>
		/// ����ID
		/// </summary>
		private string  patientId;

		/// <summary>
		/// ��ҩ���������
		/// </summary>
		private string  myBillClassCode;

		/// <summary>
		/// ���ⵥ�ݺ�
		/// </summary>
		private string  myOutBillCode = "0";

		/// <summary>
		/// ��λ��
		/// </summary>
		private string  myPlaceCode;

		/// <summary>
		/// ��������
		/// </summary>
		private string applyType;

		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�������� ����ΪSystemType����",true)]
		public string ApplyType
		{
			get
			{
				return this.applyType;
			}
			set
			{
				this.applyType = value;
			}
		}


		/// <summary>
		/// ���������
		/// </summary>
		[System.Obsolete("�������� �̳���ApplyBase���� ����Ϊ����StockDept����",true)]
		public FS.FrameWork.Models.NeuObject TargetDept 
		{
			get	{ return  myTargetDept;}
			set	{  myTargetDept = value; }
		}


		/// <summary>
		/// ��ҩ����
		/// </summary>
		[System.Obsolete("�������� ����ΪDrugNO����",true)]
		public string DrugBill 
		{
			get	{ return  myDrugBill;}
			set	{  myDrugBill = value; }
		}


		/// <summary>
		/// ����ID�ţ����￨�Ż���סԺ�ţ�
		/// </summary>
		[System.Obsolete("�������� ����ΪPatientNO����",true)]
		public string PatientID
		{
			get	
			{
				return  this.patientId;
			}
			set	
			{ 
				this.patientId = value; 
			}
		}


		/// <summary>
		/// ��ҩ���������
		/// </summary>
		[System.Obsolete("�������� ����ΪBillClassNO",true)]
		public string BillClassCode 
		{
			get{ return this.myBillClassCode; }
			set{ this.myBillClassCode = value; }
		}


		/// <summary>
		/// ���ⵥ��
		/// </summary>
		[System.Obsolete("�������� ����ΪOutBillNO����",true)]
		public string OutBillCode 
		{
			get	{ return  myOutBillCode;}
			set	{  myOutBillCode = value; }
		}


		/// <summary>
		/// ��λ��
		/// </summary>
		[System.Obsolete("�������� ����ΪPlaceNO",true)]
		public string PlaceCode 
		{
			get	{ return  myPlaceCode;}
			set	{  myPlaceCode = value; }
		}


		/// <summary>
		/// ҽ����ˮ��
		/// </summary>
		[System.Obsolete("�������� ����ΪOrderNO����",true)]
		public string OrderNo 
		{
			get	{ return  myOrderNo;}
			set	{  myOrderNo = value; }
		}


		/// <summary>
		/// ҽ��������
		/// </summary>
		[System.Obsolete("�������� ����ΪCombNO����",true)]
		public string CombNo 
		{
			get	{ return  myCombNo;}
			set	{  myCombNo = value; }
		}


		/// <summary>
		/// ִ�е���ˮ��
		/// </summary>
		[System.Obsolete("�������� ����ΪExecNO����",true)]
		public string ExecNo 
		{
			get	{ return  myExecNo;}
			set	{  myExecNo = value; }
		}


		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�������� ����ΪRecipeNO����",true)]
		public string RecipeNo 
		{
			get	{ return  myRecipeNo;}
			set	{  myRecipeNo = value; }
		}


		/// <summary>
		/// ��������Ŀ��ˮ��
		/// </summary>
		[System.Obsolete("�������� ����ΪSequenceNO����",true)]
		public int SequenceNo 
		{
			get	{ return  mySequenceNo;}
			set	{  mySequenceNo = value; }
		}


		/// <summary>
		/// ����
		/// </summary>
		[System.Obsolete("�������� ����ΪGroupNO����",true)]
		public decimal GroupNo 
		{
			get	{ return  myGroupNo;}
			set	{  myGroupNo = value; }
		}


		/// <summary>
		/// ����
		/// </summary>
		[System.Obsolete("BatchNO����",true)]
		public string BatchNo 
		{
			get	{ return  myBatchNo;}
			set	{  myBatchNo = value; }
		}


		
		//������Apply�����ڵĳ�Ա���� Apply����Ч ����IMAApplyBase�׼̳�

		private string myBillCode;
		private string   myApplyOperCode = "";
		private DateTime myApplyDate;
		private string   myApplyState = "";
		private decimal  myApplyNum;
		private DateTime myExamDate;
		private string   myExamOperCode = "";
		private decimal  myApproveNum;
		private DateTime myApproveDate;
		private string   myApproveOperCode = "";
		private FS.FrameWork.Models.NeuObject myApproveDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public string ApplyOperCode 
		{
			get	{ return  myApplyOperCode;}
			set	{  myApplyOperCode = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public DateTime ApplyDate 
		{
			get	{ return  myApplyDate;}
			set	{  myApplyDate = value; }
		}

		/// <summary>
		/// ����״̬
		/// </summary>
		[System.Obsolete("�������� ����ΪState����",true)]
		public string ApplyState 
		{
			get	{ return  myApplyState;}
			set	{  myApplyState = value; }
		}

		/// <summary>
		/// ���������(ÿ��������)
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public decimal ApplyNum 
		{
			get	{ return  myApplyNum;}
			set	{  myApplyNum = value; }
		}

		/// <summary>
		/// �������ڣ���ӡ�ˣ�
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public DateTime ExamDate 
		{
			get	{ return  myExamDate;}
			set	{  myExamDate = value; }
		}

		/// <summary>
		/// �����ˣ���ӡ�ˣ�
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public string ExamOperCode 
		{
			get	{ return  myExamOperCode;}
			set	{  myExamOperCode = value; }
		}

		/// <summary>
		/// ��׼����
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public decimal ApproveNum 
		{
			get	{ return  myApproveNum;}
			set	{  myApproveNum = value; }
		}

		/// <summary>
		/// ��׼����
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public DateTime ApproveDate 
		{
			get	{ return  myApproveDate;}
			set	{  myApproveDate = value; }
		}

		/// <summary>
		/// ��׼��
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public string ApproveOperCode 
		{
			get	{ return  myApproveOperCode;}
			set	{  myApproveOperCode = value; }
		}

		/// <summary>
		/// ��׼����
		/// </summary>
		[System.Obsolete("�������� �������̳л��� ����Ϊ��IMAAppleBase�����ڵ�Operation�����ڻ�ȡ",true)]
		public FS.FrameWork.Models.NeuObject ApproveDept 
		{
			get	{ return  myApproveDept;}
			set	{  myApproveDept = value; }
		}

		/// <summary>
		/// ���뵥��
		/// </summary>
		[System.Obsolete("�������� ����ΪBillNO����",true)]
		public string BillCode 
		{
			get	{ return  myBillCode;}
			set	{  myBillCode = value; }
		}

		#endregion

        #region �����¼�
        private string bedNO = "";

        /// <summary>
        /// סԺ�����еĴ�λ��
        /// </summary>
        public string BedNO
        {
            get { return bedNO; }
            set { bedNO = value; }
        }

        private string patientName = "";

        /// <summary>
        /// סԺ�����еĻ�������
        /// </summary>
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

         /// <summary>
        /// ����С��λ��
        /// </summary>
        private uint costDecimals = 2;

        /// <summary>
        /// ����С��λ��
        /// </summary>
        public uint CostDecimals
        {
            get { return costDecimals; }
            set { costDecimals = value; }
        }

        /// <summary>
        /// �洢��Ӧ������ִ�е���ˮ��
        /// </summary>
        private string execSeqAll;

        /// <summary>
        /// �洢��Ӧ������ִ�е���ˮ��
        /// </summary>
        public string ExecSeqAll
        {
            get
            {
                return execSeqAll;
            }
            set
            {
                execSeqAll = value;
            }
        }
        #endregion
    }
}
