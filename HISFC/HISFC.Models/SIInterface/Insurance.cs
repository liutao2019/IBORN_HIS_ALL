using System;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// Insurance ��ժҪ˵����
	/// </summary>
    [Serializable]
    public class Insurance:FS.FrameWork.Models.NeuObject
	{
		public Insurance()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private FS.FrameWork.Models.NeuObject pactInfo = new FS.FrameWork.Models.NeuObject();
		private FS.FrameWork.Models.NeuObject kind = new FS.FrameWork.Models.NeuObject();
		private string partId;
		private Decimal rate;
		private Decimal beginCost;
		private Decimal endCost;
		private FS.FrameWork.Models.NeuObject operCode =  new FS.FrameWork.Models.NeuObject();
		private DateTime operDate;
		/// <summary>
		/// ��ͬ��λ��Ϣ
		/// </summary>
		public FS.FrameWork.Models.NeuObject PactInfo
		{
			set{pactInfo = value;}
			get{return pactInfo;}
		}
		/// <summary>
		/// ��Ա���
		/// </summary>
		public FS.FrameWork.Models.NeuObject Kind
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
		public FS.FrameWork.Models.NeuObject OperCode
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
