using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// PactUnitItemRate ��ժҪ˵����
	/// id ��ͬ���� 
	/// name ��ͬ���� 
	/// </summary>
	public class PactUnitItemRate :Neusoft.NFC.Object.NeuObject 
	{
		
		public Neusoft.NFC.Object.NeuObject Item = new Neusoft.NFC.Object.NeuObject(); //��Ŀ���� ��Ŀ���� ����С���ô��� ��С��������
		
		/// <summary>
		/// ��Ŀ������� 0��С���ã�1 ҩƷ��2 ��ҩƷ
		/// </summary>
		public int ItemType = 0 ;

		public FTRate FTRate = new FTRate();//���ñ���

		public string OperID ;
		public DateTime OperDateTime;

		public PactUnitItemRate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
