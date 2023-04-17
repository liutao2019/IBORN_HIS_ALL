using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// �������Ŀ���Ϣ��ʵ�� �̳��� neusoft.neuFC.Object.neuObject
	/// ID ����Ա���� Name ����Ա����
	/// 
	/// ����: WangYu 2004-12-04
	/// </summary>
	public class ReadCard : neusoft.neuFC.Object.neuObject
	{
		public ReadCard()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//˽���ֶ�
		private string myCardID;
		private neusoft.neuFC.Object.neuObject myEmplInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myDeptInfo = new neusoft.neuFC.Object.neuObject();
		private DateTime myOperDate;
		private string myValidFlag;
		private neusoft.neuFC.Object.neuObject myCancelOperInfo = new neusoft.neuFC.Object.neuObject();
		private DateTime myCancelDate;


		/// <summary>
		/// ����֤��
		/// </summary>
		public string CardID 
		{
			get	
			{
				if(myCardID == null)
				{
					myCardID = "";
				}
				return  myCardID;
			}
			set	{  myCardID = value; }
		}

		/// <summary>
		/// Ա����Ϣ ID ���� Name ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject EmplInfo 
		{
			get	
			{
				return  myEmplInfo;
			}
			set	{  myEmplInfo = value; }
		}

		/// <summary>
		/// ������Ϣ ID ���� Name ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject DeptInfo 
		{
			get	
			{
				return  myDeptInfo;
			}
			set	{  myDeptInfo = value; }
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
		/// ��Ч 1��Ч/2��Ч
		/// </summary>
		public string ValidFlag 
		{
			get	
			{
				if(myValidFlag == null)
				{
					myValidFlag = "";
				}
				return  myValidFlag;
			}
			set	{  myValidFlag = value; }
		}

		/// <summary>
		/// ��������Ϣ ID ���� Name ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject CancelOperInfo 
		{
			get	
			{
				return  myCancelOperInfo;
			}
			set	{  myCancelOperInfo = value; }
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CancelDate 
		{
			get	
			{
				return  myCancelDate;
			}
			set	{  myCancelDate = value; }
		}
    
		public new ReadCard Clone()
		{
			ReadCard ReadCardClone = base.MemberwiseClone() as ReadCard;

			ReadCardClone.myCancelOperInfo = this.myCancelOperInfo.Clone();
			ReadCardClone.myDeptInfo = this.myDeptInfo.Clone();
			ReadCardClone.myEmplInfo = this.myEmplInfo.Clone();

			return ReadCardClone;
		}
	}
}
