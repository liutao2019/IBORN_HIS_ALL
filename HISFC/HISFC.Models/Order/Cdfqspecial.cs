using System;

namespace Neusoft.HISFC.Object.Order
{
	/// <summary>
	/// Cdfqspecial ��ժҪ˵����
	/// </summary>
	[Obsolete("����Ƶ������SpecialFrequency",true)]
	public class Cdfqspecial
	{
		public Cdfqspecial()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public string moOrder; //ҽ����ˮ��
		public string combNo;  //ҽ����Ϻ�
		public string drqFreqtype; //Ƶ������
		public string drefreqName; //Ƶ������
		public string drqPoint; //Ƶ�ε�
		public string dosePoint; //  Ƶ�ε�����
		public string OperID; // ����Ա
		public System.DateTime operDate; //����ʱ��
	}
}
