using System;

namespace neusoft.HISFC.Object.Pharmacy
{
	/// <summary>
	/// StoreBase ��ժҪ˵����
	/// </summary>
	public class StoreBase : neusoft.neuFC.Object.neuObject
	{
		// Internal member variables
		private neusoft.neuFC.Object.neuObject myDept = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myTargetDept = new neusoft.neuFC.Object.neuObject();
		private Item myItem = new Item();
		private string  myGroupNo;
		private string  myBatchNo;
		private DateTime myValidDate;
		private neusoft.neuFC.Object.neuObject myProducer = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myCompany = new neusoft.neuFC.Object.neuObject();
		private decimal myRetailPrice;
		private decimal myWholesalePrice;
		private decimal myPurchasePrice;
		private decimal myQuantity;
		private string  myPlaceCode;
		private decimal myStoreNum;
		private decimal myStoreCost;
		private string  myState;
		private string  myShowFlag;
		private string  myShowUnit;


		public StoreBase()
		{
		    //
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject Dept 
		{
			get	{ return  myDept;}
			set	{  myDept = value; }
		}

		/// <summary>
		/// Ŀ�겿��
		/// </summary>
		public neusoft.neuFC.Object.neuObject TargetDept 
		{
			get	{ return  myTargetDept;}
			set	{  myTargetDept = value; }
		}

		/// <summary>
		/// ҩƷʵ��
		/// </summary>
		public Item Item 
		{
			get	{ return  myItem;}
			set	{  myItem = value; }
		}

		/// <summary>
		/// �������
		/// </summary>
		public string GroupNo
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
		public neusoft.neuFC.Object.neuObject Producer 
		{
			get	{ return  myProducer;}
			set	{  myProducer = value; }
		}

		/// <summary>
		/// ������˾
		/// </summary>
		public neusoft.neuFC.Object.neuObject Company 
		{
			get	{ return  myCompany;}
			set	{  myCompany = value; }
		}

		/// <summary>
		/// ���ۼ�
		/// </summary>
		public string RetailPrice 
		{
			get	{ return myRetailPrice; }
			set	{ myRetailPrice = value; }
		}

		/// <summary>
		/// ������
		/// </summary>
		public string WholesalePrice {
			get	{ return myWholesalePrice; }
			set	{ myWholesalePrice = value; }
		}

		/// <summary>
		/// �����
		/// </summary>
		public string PurchasePrice	
		{
			get	{ return myPurchasePrice; }
			set	{ myPurchasePrice = value; }
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Quantity {
			get	{ return myQuantity; }
			set	{ myQuantity = value; }
		}

		/// <summary>
		/// ���۽��
		/// </summary>
		public string RetailCost {
			get	{ return myRetailPrice * myQuantity; }
		}


		/// <summary>
		/// �������
		/// </summary>
		public string WholesaleCost {
			get	{ return myWholesalePrice * myQuantity; }
		}
		/// <summary>
		/// ������
		/// </summary>
		public string PurchaseCost {
			get	{ return myPurchasePrice * myQuantity; }
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
		/// �������
		/// </summary>
		public decimal StoreNum 
		{
			get	{ return  myStoreNum;}
			set	{  myStoreNum = value; }
		}

		/// <summary>
		/// �����
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
		public string ShowFlag 
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
	}
}
