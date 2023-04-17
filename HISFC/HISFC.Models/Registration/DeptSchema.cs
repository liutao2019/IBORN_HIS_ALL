using System;

namespace Neusoft.HISFC.Object.Registration
{
	/// <summary>
	/// ר���Ű�ʵ��
	/// </summary>
	public class DeptSchema:Neusoft.NFC.Object.NeuObject
	{
		/// <summary>
		/// 
		/// </summary>
		public DeptSchema()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private DateTime seeDate=DateTime.MinValue;
		private string week="";
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime SeeDate
		{
			get{return this.seeDate;}
			set
			{
				this.seeDate=value;

				this.week=((int)this.seeDate.DayOfWeek).ToString();
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Week
		{
			get{return this.week;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string NoonID="";
				
		/// <summary>
		/// �������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Dept=new Neusoft.NFC.Object.NeuObject();
				
		/// <summary>
		/// �Һż���
		/// </summary>
		public string RegLevel="";
		
		/// <summary>
		/// �Һ��޶�
		/// </summary>
		public int RegLimit;
		
		/// <summary>
		/// ԤԼ�Һ��޶�
		/// </summary>
		public int PreRegLimit;
		
		/// <summary>
		/// �ѹҺ���
		/// </summary>
		public int HasReg;
		
		/// <summary>
		/// �ѹ�ԤԼ����
		/// </summary>
		public int HasPreReg;
		
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		public bool IsValid=false;
		
		/// <summary>
		/// ͣ��ԭ��
		/// </summary>
		public Neusoft.NFC.Object.NeuObject StopReason=new Neusoft.NFC.Object.NeuObject();
		
		/// <summary>
		/// ֹͣ��
		/// </summary>
		public string StopID;
		
		/// <summary>
		/// ֹͣʱ��
		/// </summary>
		public DateTime StopDate=DateTime.MinValue;
				
		//public string Memo;
		
		/// <summary>
		/// ����Աid
		/// </summary>
		public string OperID;
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate=DateTime.MinValue;		
	}
}
