using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// ChangePrepay ��ժҪ˵����
	/// ת��Ԥ����
	/// </summary>
	public class ChangePrepay:Neusoft.NFC.Object.NeuObject
	{
		public ChangePrepay()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ת������
		/// </summary>
		public string ChangeType;
		/// <summary>
		/// ҽ����ˮ��
		/// </summary>
		public string ClinicNo;
	
		// ����
		//public string name;
		/// <summary>
		/// �������
		/// </summary>
		public Neusoft.HISFC.Object.Fee.PayKind payKind = new PayKind();
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Pact = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ת��Ԥ�����
		/// </summary>
		public decimal ChangePrepayCost = 0m;
		/// <summary>
		/// ת�����Ա
		/// </summary>
		public Neusoft.NFC.Object.NeuObject ChangeOper =new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ת��ʱ��
		/// </summary>
		public DateTime ChangeOperDate;
		/// <summary>
		/// ����״̬
		/// </summary>
		public string BalanceState;


	}
}
