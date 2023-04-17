using System;

namespace neusoft.HISFC.Object.InterfaceSi
{
	/// <summary>
	/// Insurance ��ժҪ˵����
	/// </summary>
	public class Insurance:neusoft.neuFC.Object.neuObject
	{
		public Insurance()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private neusoft.neuFC.Object.neuObject pactInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject kind = new neusoft.neuFC.Object.neuObject();
		private string partId;
		private Decimal rate;
		private Decimal beginCost;
		private Decimal endCost;
		private neusoft.neuFC.Object.neuObject operCode =  new neusoft.neuFC.Object.neuObject();
		private DateTime operDate;
		/// <summary>
		/// ��ͬ��λ��Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject PactInfo
		{
			set{pactInfo = value;}
			get{return pactInfo;}
		}
		/// <summary>
		/// ��Ա���
		/// </summary>
		public neusoft.neuFC.Object.neuObject Kind
		{
			set{kind = value;}
			get{return kind;}
		}
		/// <summary>
		/// �ֶ����
		/// </summary>
		public string PartId
		{
			set{partId = value;}
			get{return partId;}
		}
		/// <summary>
		/// �����Ը�����
		/// </summary>
		public Decimal Rate
		{
			set{rate = value;}
			get{return rate;}
		}
		/// <summary>
		/// ���俪ʼ
		/// </summary>
		public Decimal BeginCost
		{
			set{beginCost = value;}
			get{return beginCost;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public Decimal EndCost
		{
			set{endCost = value;}
			get{return endCost;}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperCode
		{
			set{operCode = value;}
			get{return operCode;}
		}
		public DateTime OperDate
		{
			set{operDate = value;}
			get{return operDate;}
		}
	}	
}
