using System;

namespace Neusoft.HISFC.Object.Material
{
	/// <summary>
	/// StoreBase ��ժҪ˵����
	/// ҩ���������Ϣ��ҩƷ��⡢���⡢���̳��ڱ�ʵ�壩
	/// ID     ��,���,�̵�,���۵��ݺ�BillCode
	/// writed by cuipeng 
	/// 2004-12
	/// </summary>
	public class StorageBase : Neusoft.NFC.Object.NeuObject
	{
		#region  Internal member variables		
		private int      mySerialNo;
		private string   myPrivType = "";
		private string   mySystemType = "";
		private Neusoft.NFC.Object.NeuObject myDept = new Neusoft.NFC.Object.NeuObject();
		private Neusoft.NFC.Object.NeuObject myTargetDept = new Neusoft.NFC.Object.NeuObject();
		private MaterialItem myItem = new MaterialItem();
		private int      myGroupNo;
		private string   myBatchNo = "";
		private DateTime myValidDate;
		private Neusoft.NFC.Object.NeuObject myProducer = new Neusoft.NFC.Object.NeuObject();
		private Neusoft.NFC.Object.NeuObject myCompany = new Neusoft.NFC.Object.NeuObject();
		private decimal  myQuantity;
		private decimal  myRetailCost;
		private decimal  myWholesaleCost;
		private decimal  myPurchaseCost;
		private string   myPlaceCode = "";
		private decimal  myStoreNum;
		private decimal  myStoreCost;
		private string   myState = "";
		private string   myShowFlag = "0";
		private string   myShowUnit = "";
		private string   myOperCode = "";
		private DateTime myOperDate;
		private string   myInvoiceNo;
		#endregion

		/// <summary>
		/// ���캯��
		/// </summary>
		public StorageBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// �������
		/// </summary>
		public int SerialNo 
		{
			get	{ return  mySerialNo;}
			set	{  mySerialNo = value; }
		}


		/// <summary>
		/// Ȩ������
		/// </summary>
		public string PrivType 
		{
			get	{ return  myPrivType;}
			set	{  myPrivType = value; }
		}


		/// <summary>
		/// ϵͳ����
		/// </summary>
		public string SystemType 
		{
			get	{ return  mySystemType;}
			set	{  mySystemType = value; }
		}


		/// <summary>
		/// ����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Dept 
		{
			get	{ return  myDept;}
			set	{  myDept = value; }
		}


		/// <summary>
		/// Ŀ�겿��
		/// </summary>
		public Neusoft.NFC.Object.NeuObject TargetDept 
		{
			get	{ return  myTargetDept;}
			set	{  myTargetDept = value; }
		}


		/// <summary>
		/// ҩƷʵ��
		/// </summary>
		public MaterialItem Item 
		{
			get	{ return  myItem;}
			set	{  myItem = value; }
		}


		/// <summary>
		/// �������
		/// </summary>
		public int GroupNo
		{
			get	{ return  myGroupNo;}
			set	{  myGroupNo = value; }
		}


		/// <summary>
		/// ����
		/// </summary>
		public string BatchNo 
		{
			get	{ return  myBatchNo;}
			set	{  myBatchNo = value; }
		}


		/// <summary>
		/// ��Ч��
		/// </summary>
		public DateTime ValidDate 
		{
			get	{ return  myValidDate;}
			set	{  myValidDate = value; }
		}


		/// <summary>
		/// ���ɳ���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Producer 
		{
			get	{ return  myProducer;}
			set	{  myProducer = value; }
		}


		/// <summary>
		/// ������˾
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Company 
		{
			get	{ return  myCompany;}
			set	{  myCompany = value; }
		}


		/// <summary>
		/// ����
		/// </summary>
		public decimal Quantity 
		{
			get	{ return myQuantity; }
			set	{ myQuantity = value; }
		}


		/// <summary>
		/// ���۽��
		/// ֻ��
		/// </summary>
		public decimal RetailCost 
		{
			get	{ return myRetailCost; }
			set { myRetailCost = value; }
		}


		/// <summary>
		/// �������
		/// ֻ��
		/// </summary>
		public decimal WholesaleCost 
		{
			get	{ return myWholesaleCost; }
			set { myWholesaleCost = value; }
		}


		/// <summary>
		/// ������
		/// ֻ��
		/// </summary>
		public decimal PurchaseCost 
		{
			get	{ return myPurchaseCost; }
			set { myPurchaseCost = value; }
		}


		/// <summary>
		/// ��λ��
		/// </summary>
		public string PlaceCode 
		{
			get	{ return  myPlaceCode;}
			set	{  myPlaceCode = value; }
		}


		/// <summary>
		/// ͬ��ҩƷ��������ϼ�
		/// </summary>
		public decimal StoreNum 
		{
			get	{ return  myStoreNum;}
			set	{  myStoreNum = value; }
		}


		/// <summary>
		/// ͬ��ҩƷ�����ϼƣ��˴�û�������������ۣ���Ϊ�����ܱ��еĽ���Ƕ������ҩƷ���ܽ�
		/// </summary>
		public decimal StoreCost 
		{
			get	{ return  myStoreCost;}
			set	{  myStoreCost = value; }
		}


		/// <summary>
		/// ״̬
		/// </summary>
		public string State 
		{
			get	{ return  myState;}
			set	{  myState = value; }
		}


		/// <summary>
		/// ��ʾ��λ��ǣ�1��װ��λ��0��С��λ��
		/// </summary>
		public string ShowState
		{
			get	{ return  myShowFlag;}
			set	{  myShowFlag = value; }
		}


		/// <summary>
		/// ��ʾ��λ
		/// </summary>
		public string ShowUnit 
		{
			get	{ return  myShowUnit;}
			set	{  myShowUnit = value; }
		}


		/// <summary>
		/// ����Ա����
		/// </summary>
		public string OperCode 
		{
			get	{ return  myOperCode;}
			set	{  myOperCode = value; }
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate 
		{
			get	{ return  myOperDate;}
			set	{  myOperDate = value; }
		}


		
		/// <summary>
		/// ��Ʊ��
		/// </summary>
		public string InvoiceNo 
		{
			get	{ return  myInvoiceNo;}
			set	{  myInvoiceNo = value; }
		}
	}
}
