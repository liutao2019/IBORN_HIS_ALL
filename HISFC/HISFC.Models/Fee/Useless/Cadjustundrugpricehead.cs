using System;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// Cadjustundrugpricehead ��ժҪ˵����
	/// ��ҩƷ���� ͷ��
	/// </summary>
    /// 

    [System.Serializable]
	public class Cadjustundrugpricehead :FS.FrameWork.Models.NeuObject
	{
		public Cadjustundrugpricehead()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//���۵���
		public string AdjustPriceNo;
		//������Чʱ��
		public System.DateTime date;
		//����Ա���� 
		public string opercode;
		//����ʱ��
		public System.DateTime OperDate;
	}
}
