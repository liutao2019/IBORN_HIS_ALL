using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Cadjustundrugpricedetai ��ժҪ˵����
	/// </summary>
	public class Cadjustundrugpricedetai :Neusoft.NFC.Object.NeuObject
	{
		public Cadjustundrugpricedetai()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public string AdjustPriceNo;
		public string ItemCode;
		public string ItemName;
		public decimal PriceOld; //���׼�  ��
		public decimal PriceNew; // ���׼� ��
		public decimal PriceOld1; //��ͯ��  ��
		public decimal PriceNew1; // ��ͯ�� ��
		public decimal PriceOld2; //�����  ��
		public decimal PriceNew2; // ����� ��
		public string OperCode;
		public System.DateTime  operdate;
		public bool IsNow;

	}
}
