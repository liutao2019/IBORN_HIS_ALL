using System;

namespace neusoft.HISFC.Object.MedTech
{
	/// <summary>
	/// MedTechBookInfo ҽ��ԤԼ��Ϣ�� write by zhouxs  2006-3-8
	/// </summary>
	public class MedTechBookInfo :neusoft.neuFC.Object.neuObject 
	{
		public MedTechBookInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private string status;//ԤԼ״̬
		private string bookid;//ԤԼ��
		private DateTime bookdate;//ԤԼʱ��
	    
		/// <summary>
	    /// ԤԼ״̬
	    /// </summary>
		public string Status
		{
			get 
			{
				return status;
			}
			set
			{
				status = value;
			}

		}
		
		/// <summary>
		/// ԤԼ����
		/// </summary>
		public string BookId
		{
			get
			{
				return bookid;
			}
			set
			{
				bookid = value;
			}
		}
		
		/// <summary>
		/// ԤԼ����
		/// </summary>
		public DateTime BookDate
		{
			get
			{
				return bookdate;
			}
			set
			{
				bookdate = value;
			}
		}
		
		/// <summary>
		/// ���п�¡
		/// </summary>
		/// <returns></returns>
				
		public new MedTechBookInfo Clone()
		{
			MedTechBookInfo obj=base.Clone() as MedTechBookInfo;
			return obj;
		}
	}
}
