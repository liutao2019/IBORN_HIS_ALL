using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ҩƷ���������Ϣ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� �̳���StorageBase����'
	///  />
	///  ID ��ⵥ��ˮ��
	///  TargetDept��Companyȡֵ��ͬ
	/// </summary>
    [Serializable]
    public class Input : StorageBase
	{
		public Input () 
		{
            //�˴�Ӧ�ô洢�û��Զ����������� ��Ӧ�ô洢0310
            //this.PrivType = "0310";	//���Ȩ�ޱ���

            this.Class2Type = "0310";
		}

		#region ����

		private string   myInListCode;

		private string   myOutBillCode = "0";

		private int      myOutSerialNo;

		private string   myOutListCode;

		private decimal  myApplyNum;

		private string   myApplyOperCode;

		private DateTime myApplyDate;

		private decimal  myExamNum;

		private string   myExamOperCode;

		private DateTime myExamDate;

		private string   myApproveOperCode;

		private DateTime myApproveDate;

		private string   myMedID;

		private string   myDeliveryNo;

		private string   myTenderNo;

		private string   myPayState;

		private string   myCashFlag;

		private decimal  myActualRate;

		private decimal	 myReturnNum;

        private string myStockNO;

        private DateTime invoiceDate;

        private decimal commonPurchasePrice;

        /// <summary>
        /// ���ʱ��
        /// 
        /// {24E12384-34F7-40c1-8E2A-3967CECAF615}
        /// </summary>
        private DateTime inDate;

        /// <summary>
        /// Դ���ң�������λ������  1 Ժ�ڿ��� 2 ������λ 3 ��չ
        /// 
        /// {24E12384-34F7-40c1-8E2A-3967CECAF615}
        /// </summary>
        private string sourceCompanyType;

		#endregion

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		public string InListNO 
		{
			get	
			{
				return  myInListCode;
			}
			set	
			{ 
				myInListCode = value; 
			}
		}


		/// <summary>
		/// ���ⵥ��
		/// </summary>
		public string OutBillNO 
		{
			get	
			{
				return  myOutBillCode;
			}
			set	
			{
				myOutBillCode = value;
			}
		}


		/// <summary>
		/// ���ⵥ�����
		/// </summary>
		public int OutSerialNO 
		{
			get	
			{
				return  myOutSerialNo;
			}
			set	
			{
				myOutSerialNo = value; 
			}
		}


		/// <summary>
		/// ���ⵥ�ݺ�
		/// </summary>
		public string OutListNO 
		{
			get	
			{
				return  myOutListCode;
			}
			set	
			{ 
				myOutListCode = value;
			}
		}


		/// <summary>
		/// �Ƽ����
		/// </summary>
		public string MedNO 
		{
			get	
			{
				return  myMedID;
			}
			set	
			{ 
				myMedID = value; 
			}
		}


		/// <summary>
		/// �ͻ���
		/// </summary>
		public string DeliveryNO 
		{
			get	
			{
				return  myDeliveryNo;
			}
			set	
			{
				myDeliveryNo = value;
			}
		}

		
		/// <summary>
		/// �б굥���
		/// </summary>
		public string TenderNO
		{
			get 
			{
				return this.myTenderNo;
			}
			set 
			{
				this.myTenderNo = value;
			}
		}
		
		
		/// <summary>
		/// �����̽���־ 0 δ�� 1 δȫ�� 2 ����
		/// </summary>
		public string PayState 
		{
			get 
			{
				return this.myPayState;
			}
			set 
			{
				this.myPayState = value;
			}
		}
		
		
		/// <summary>
		/// �ֽ��־
		/// </summary>
		public string CashFlag 
		{
			get 
			{
				return this.myCashFlag;
			}
			set 
			{
				this.myCashFlag = value;
			}
		}
		
		
		/// <summary>
		/// ʵ�ʿ���
		/// </summary>
		public decimal ActualRate 
		{
			get 
			{
				return this.myActualRate;
			}
			set 
			{
				this.myActualRate = value;
			}
		}

		
		/// <summary>
		/// ������˾
		/// </summary>
		public new FS.FrameWork.Models.NeuObject Company 
		{
			get	
			{
				return  this.TargetDept;
			}
			set	
			{
				this.TargetDept = value;
			}
		}

        /// <summary>
        /// �ɹ��ƻ���ˮ��
        /// </summary>
        public string StockNO
        {
            get
            {
                return this.myStockNO;
            }
            set
            {
                this.myStockNO = value;
            }
        }
        //{D28CC3CF-C502-4987-BC01-1AEBF2F9D17F} sel ����������������
        /// <summary>
        /// ��Ʊ�ϵķ�Ʊʱ�� 
        /// </summary>
        public DateTime InvoiceDate
        {
            get
            {
                return this.invoiceDate;
            }
            set
            {
                this.invoiceDate=value;
            }
        }

        /// <summary>
        /// һ�����ʱ�Ĺ����
        /// </summary>
        public decimal CommonPurchasePrice
        {
            get
            {
                return this.commonPurchasePrice;
            }
            set
            {
                this.commonPurchasePrice = value;
            }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime InDate
        {
            get
            {
                return this.inDate;
            }
            set
            {
                this.inDate = value;
            }
        }

        /// <summary>
        /// Դ���ң�������λ������  1 Ժ�ڿ��� 2 ������λ 3 ��չ
        /// </summary>
        public string SourceCompanyType
        {
            get
            {
                return this.sourceCompanyType;
            }
            set
            {
                this.sourceCompanyType = value;
            }
        }

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new Input Clone()
		{
			Input obj = base.Clone() as Input;
			obj.Company = this.Company.Clone();
			return obj;
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		[System.Obsolete("�������� ����ΪInListNO����",true)]
		public string InListCode 
		{
			get	{ return  myInListCode;}
			set	{  myInListCode = value; }
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
		/// ���ⵥ�����
		/// </summary>
		[System.Obsolete("�������� ����ΪOutSerialNO����",true)]
		public int OutSerialNo 
		{
			get	{ return  myOutSerialNo;}
			set	{  myOutSerialNo = value; }
		}


		/// <summary>
		/// ���ⵥ�ݺ�
		/// </summary>
		[System.Obsolete("�������� ����ΪOutListNO����",true)]
		public string OutListCode 
		{
			get	{ return  myOutListCode;}
			set	{  myOutListCode = value; }
		}


		/// <summary>
		/// �Ƽ����
		/// </summary>
		[System.Obsolete("�������� ����ΪMedNO����",true)]
		public string MedID 
		{
			get	{ return  myMedID;}
			set	{  myMedID = value; }
		}


		/// <summary>
		/// �ͻ���
		/// </summary>
		[System.Obsolete("�������� ����ΪDeliveryNO����",true)]
		public string DeliveryNo 
		{
			get	{ return  myDeliveryNo;}
			set	{  myDeliveryNo = value; }
		}

		
		/// <summary>
		/// �б굥���
		/// </summary>
		[System.Obsolete("�������� ����ΪtenderNO����",true)]
		public string TenderNo
		{
			get {return this.myTenderNo;}
			set {this.myTenderNo = value;}
		}
		

		/// <summary>
		/// �����������
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public decimal ApplyNum 
		{
			get	{ return  myApplyNum;}
			set	{  myApplyNum = value; }
		}


		/// <summary>
		/// ��������˱���
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public string ApplyOperCode 
		{
			get	{ return  myApplyOperCode;}
			set	{  myApplyOperCode = value; }
		}


		/// <summary>
		/// �����������
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public DateTime ApplyDate 
		{
			get	{ return  myApplyDate;}
			set	{  myApplyDate = value; }
		}


		/// <summary>
		/// ������������ҩ��
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public decimal ExamNum 
		{
			get	{ return  myExamNum;}
			set	{  myExamNum = value; }
		}


		/// <summary>
		/// �����˱��루��ҩ��
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public string ExamOperCode 
		{
			get	{ return  myExamOperCode;}
			set	{  myExamOperCode = value; }
		}


		/// <summary>
		/// �������ڣ���ҩ��
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public DateTime ExamDate 
		{
			get	{ return  myExamDate;}
			set	{  myExamDate = value; }
		}


		/// <summary>
		/// ��׼�˱���
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public string ApproveOperCode 
		{
			get	{ return  myApproveOperCode;}
			set	{  myApproveOperCode = value; }
		}


		/// <summary>
		/// ��׼����
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public DateTime ApproveDate 
		{
			get	{ return  myApproveDate;}
			set	{  myApproveDate = value; }
		}


		/// <summary>
		/// �˿�����
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public decimal ReturnNum 
		{
			get	{ return  myReturnNum;}
			set	{  myReturnNum = value; }
		}



		#endregion
	}
}
