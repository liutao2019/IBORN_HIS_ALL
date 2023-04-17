using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ҩƷ���������]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-13'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� �̳���IMAStoreBase����'
	///  />
	///  ID �������
	/// </summary>
    [Serializable]
    public class StorageBase : FS.HISFC.Models.IMA.IMAStoreBase
	{
		#region  ����

		private int mySerialNo;

		private FS.FrameWork.Models.NeuObject myDept = new FS.FrameWork.Models.NeuObject();

		private FS.FrameWork.Models.NeuObject myTargetDept = new FS.FrameWork.Models.NeuObject();

		private Item myItem = new Item();

		private decimal myGroupNo;

		private string myBatchNo = "";

		private DateTime myValidDate;

		private FS.FrameWork.Models.NeuObject myProducer = new FS.FrameWork.Models.NeuObject();

		private FS.FrameWork.Models.NeuObject myCompany = new FS.FrameWork.Models.NeuObject();

		private string myPlaceCode = "";

		private decimal myStoreNum;

		private string myShowFlag = "0";

		private string myShowUnit = "";

		private string myOperCode = "";

		private DateTime myOperDate;

		private string myInvoiceNo;

        private string myInvoiceType;

        /// <summary>
        /// �Ƿ�ҩ�����
        /// </summary>
        private bool isChestManager;

        /// <summary>
        /// ҩ���������
        /// </summary>
        private decimal chestQty;

		#endregion

		/// <summary>
		/// ���캯��
		/// </summary>
		public StorageBase()
		{
			
		}

		/// <summary>
		/// ҩƷʵ��
		/// </summary>
		public Item Item 
		{
			get	
			{ 
				return  myItem;
			}
			set	
			{
				myItem = value; 
				base.IMAItem = value;
                base.PriceCollection = value.PriceCollection;
			}
		}

		/// <summary>
		/// �������
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
		/// ��Ч��
		/// </summary>
		public DateTime ValidTime
		{
			get	
			{
				return  myValidDate;
			}
			set	
			{
				myValidDate = value; 
			}
		}

		/// <summary>
		/// ��ʾ��λ��ǣ�1��װ��λ��0��С��λ��
		/// </summary>
		public string ShowState
		{
			get	
			{
				return  myShowFlag;
			}
			set	
			{
				myShowFlag = value; 
			}
		}

		/// <summary>
		/// ��ʾ��λ
		/// </summary>
		public string ShowUnit 
		{
			get	
			{
				return  myShowUnit;
			}
			set	
			{
				myShowUnit = value; 
			}
		}

		/// <summary>
		/// ��Ʊ��
		/// </summary>
		public string InvoiceNO 
		{
			get	
			{
				return  myInvoiceNo;
			}
			set	
			{  
				myInvoiceNo = value;
			}
		}

        /// <summary>
        /// ��Ʊ���
        /// </summary>
        public string InvoiceType
        {
            get
            {
                return this.myInvoiceType;
            }
            set
            {
                this.myInvoiceType = value;
            }
        }

        /// <summary>
        /// �Ƿ�ҩ�����
        /// </summary>
        public bool IsArkManager
        {
            get
            {
                return isChestManager;
            }
            set
            {
                isChestManager = value;
            }
        }

        /// <summary>
        /// ҩ���������
        /// </summary>
        public decimal ArkQty
        {
            get
            {
                return chestQty;
            }
            set
            {
                chestQty = value;
            }
        }

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new StorageBase Clone()
		{
			StorageBase storageBase = base.Clone() as StorageBase;

			storageBase.Item = this.Item.Clone();

			return storageBase;
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// ��λ��
		/// </summary>
		[System.Obsolete("�������� ����ΪPlaceNO����",true)]
		public string PlaceCode 
		{
			get	{ return  myPlaceCode;}
			set	{  myPlaceCode = value; }
		}


		/// <summary>
		/// ��Ч��
		/// </summary>
		[System.Obsolete("�������� ����ΪValidTime����",true)]
		public DateTime ValidDate 
		{
			get	{ return  myValidDate;}
			set	{  myValidDate = value; }
		}


		/// <summary>
		/// �������
		/// </summary>
		[System.Obsolete("�������� ����ΪGroupNO����")]
		public decimal GroupNo
		{
			get	{ return  myGroupNo;}
			set	{  myGroupNo = value; }
		}


		/// <summary>
		/// ����
		/// </summary>
		[System.Obsolete("�������� ����ΪBatchNO����")]
		public string BatchNo 
		{
			get	{ return  myBatchNo;}
			set	{  myBatchNo = value; }
		}


		/// <summary>
		/// ����
		/// </summary>
		[System.Obsolete("�������� ����ΪStockDept����")]
		public FS.FrameWork.Models.NeuObject Dept 
		{
			get	{ return  myDept;}
			set	{  myDept = value; }
		}


		/// <summary>
		/// ����Ա����
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�����ڵ�Opertation����",true)]
		public string OperCode {
			get	{ return  myOperCode;}
			set	{  myOperCode = value; }
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�����ڵ�Opertation����",true)]
		public DateTime OperDate {
			get	{ return  myOperDate;}
			set	{  myOperDate = value; }
		}

		
		/// <summary>
		/// �������
		/// </summary>
		[System.Obsolete("�������� ����ΪSerialNO����",true)]
		public int SerialNo 
		{
			get	{ return  mySerialNo;}
			set	{  mySerialNo = value; }
		}


		/// <summary>
		/// ��Ʊ��
		/// </summary>
		[System.Obsolete("�������� ����ΪInvoiceNO����",true)]
		public string InvoiceNo 
		{
			get	{ return  myInvoiceNo;}
			set	{  myInvoiceNo = value; }
		}


		/// <summary>
		/// ͬ��ҩƷ��������ϼ�
		/// </summary>
		[System.Obsolete("�������� ����ΪStoreQty����",true)]
		public decimal StoreNum 
		{
			get	{ return  myStoreNum;}
			set	{  myStoreNum = value; }
		}


		#endregion
	}
}
