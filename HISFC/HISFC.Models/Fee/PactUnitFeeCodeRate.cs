using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// PactUnitFeeCodeRate ��ժҪ˵����
	/// </summary>
	public class PactUnitFeeCodeRate :Neusoft.NFC.Object.NeuObject 
	{
		// id ��ͬ��λ����
		//name ��ͬ��λ����
		public string  Fee_Code;  //��С���ô���
		public string  Fee_Name; //��С��������
		public float   Pub_Ratio  ;//���ѱ���
        public float   Own_Ratio ;//�Էѱ���
		public float   Pay_Ratio ;//�Ը�����
		public float   Eco_Ratio ;//�������
		public float   Arr_Ratio; //Ƿ�ѱ���
		public PactUnitFeeCodeRate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯
			//
		}
	}
}
