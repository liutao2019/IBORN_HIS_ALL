using System;

namespace Neusoft.HISFC.Models.Fee.Inpatient
{
	/// <summary>
	/// PrepayStat ��ժҪ˵����
	/// TODO:
	/// ��Ҫ��һ������
	/// </summary>
    /// 
    [System.Serializable]
	public class PrepayStat:Neusoft.FrameWork.Models.NeuObject
	{
		public PrepayStat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public string BeginDate;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string EndDate;
		/// <summary>
		///  Ԥ���ֽ�
		/// </summary>
		public decimal Pre_Cost;
		/// <summary>
		/// Ԥ��֧Ʊ
		/// </summary>
		public decimal Pre_Check;
		/// <summary>
		/// Ԥ������
		/// </summary>
		public decimal Pre_Other;
		/// <summary>
		/// Ԥ����Ʊ
		/// </summary>
		public decimal Pre_Draft;
		/// <summary>
		/// תѺ��
		/// </summary>
		public decimal Foregift_Cost;
		/// <summary>
		/// Ʊ������
		/// </summary>
		public string Receipt;
		/// <summary>
		/// Ԥ������
		/// </summary>
		public int PrepayNum;
		/// <summary>
		/// Ԥ������Ʊ�Ӻ�
		/// </summary>
		public string ReturnNo;
		
	}
}
