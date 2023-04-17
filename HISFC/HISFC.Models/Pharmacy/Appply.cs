using System;

namespace Neusoft.HISFC.Object.Pharmacy
{
	/// <summary>
	/// Appply ��ժҪ˵����
	/// ҩƷ���루�����������ͳ������룩����
	/// ID    �������
	/// writed by cuipeng 
	/// 2004-12
	/// </summary>
	[System.Obsolete("�������� ��Ч���� ʹ��Base.ApplyBase",true)]
	public class Appply : Neusoft.NFC.Object.NeuObject
	{
		//˽���ֶ�
		private Item myItem = new Item();
		//private string   myApplyNumber;
		private Neusoft.NFC.Object.NeuObject myApplyDept = new Neusoft.NFC.Object.NeuObject();
		private Neusoft.NFC.Object.NeuObject myTargetDept = new Neusoft.NFC.Object.NeuObject();
		private string   myApplyType = "";
		private int      myGroupNo;
		private string   myBatchNo = "";
		private string   myShowState = "0";
		private string   myShowUnit = "";
		private string   myBillCode = "";
		private string   myApplyOperCode = "";
		private DateTime myApplyDate;
		private string   myApplyState = "";
		private decimal  myApplyNum;
		private decimal  myDays = 1;
		private DateTime myExamDate;
		private string   myExamOperCode = "";
		private decimal  myApproveNum;
		private DateTime myApproveDate;
		private string   myApproveOperCode = "";
		private Neusoft.NFC.Object.NeuObject myApproveDept = new Neusoft.NFC.Object.NeuObject();

		public Appply () 
		{
			// TODO: �ڴ˴���ӹ��캯���߼�
		}
		

		// <summary>
		// �������
		// </summary>
		//public string ApplyNumber 
		//{
		//	get	{ return  ID;}
		//	set	{  ID = value; }
		//}
		//Ŀǰʹ��ID

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
		public Neusoft.NFC.Object.NeuObject ApplyDept 
		{
			get	{ return  myApplyDept;}
			set	{  myApplyDept = value; }
		}

		/// <summary>
		/// ���������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject TargetDept 
		{
			get	{ return  myTargetDept;}
			set	{  myTargetDept = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string ApplyType 
		{
			get	{ return  myApplyType;}
			set	{  myApplyType = value; }
		}

		/// <summary>
		/// ����
		/// </summary>
		public int GroupNo {
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
		/// ��λ��ʾ״̬��1��װ��λ��0��С��λ��
		/// </summary>
		public string ShowState 
		{
			get	{ return  myShowState;}
			set	{  myShowState = value; }
		}

		/// <summary>
		/// ��ʾ��λ
		/// </summary>
		public string ShowUnit 
		{
			get	{  return  myShowState=="0"? myItem.MinUnit: myItem.PackUnit;}
			set	{  myShowUnit = value; }
		}

		/// <summary>
		/// ���뵥��
		/// </summary>
		public string BillCode 
		{
			get	{ return  myBillCode;}
			set	{  myBillCode = value; }
		}

		/// <summary>
		/// ������
		/// </summary>
		public string ApplyOperCode 
		{
			get	{ return  myApplyOperCode;}
			set	{  myApplyOperCode = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public DateTime ApplyDate 
		{
			get	{ return  myApplyDate;}
			set	{  myApplyDate = value; }
		}

		/// <summary>
		/// ����״̬
		/// </summary>
		public string ApplyState 
		{
			get	{ return  myApplyState;}
			set	{  myApplyState = value; }
		}

		/// <summary>
		/// ���������(ÿ��������)
		/// </summary>
		public decimal ApplyNum 
		{
			get	{ return  myApplyNum;}
			set	{  myApplyNum = value; }
		}

		/// <summary>
		/// ��������ҩ��
		/// </summary>
		public decimal Days 
		{
			get	{ return  myDays==0? 1: myDays;}
			set	{  myDays = value; }
		}


		/// <summary>
		/// �������ڣ���ӡ�ˣ�
		/// </summary>
		public DateTime ExamDate {
			get	{ return  myExamDate;}
			set	{  myExamDate = value; }
		}


		/// <summary>
		/// �����ˣ���ӡ�ˣ�
		/// </summary>
		public string ExamOperCode {
			get	{ return  myExamOperCode;}
			set	{  myExamOperCode = value; }
		}


		/// <summary>
		/// ��׼����
		/// </summary>
		public decimal ApproveNum 
		{
			get	{ return  myApproveNum;}
			set	{  myApproveNum = value; }
		}

		/// <summary>
		/// ��׼����
		/// </summary>
		public DateTime ApproveDate 
		{
			get	{ return  myApproveDate;}
			set	{  myApproveDate = value; }
		}


		/// <summary>
		/// ��׼��
		/// </summary>
		public string ApproveOperCode 
		{
			get	{ return  myApproveOperCode;}
			set	{  myApproveOperCode = value; }
		}


		/// <summary>
		/// ��׼����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject ApproveDept 
		{
			get	{ return  myApproveDept;}
			set	{  myApproveDept = value; }
		}
	}
}
