using System;
using Neusoft.NFC.Object;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Surety ��ժҪ˵����
	/// ������Ϣ
	/// ID�������
	/// </summary>
	public class Surety:Neusoft.NFC.Object.NeuObject
	{
		public Surety()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        /// <summary>
        /// ������
        /// </summary>
		public NeuObject SuretyPerson = new NeuObject();
		/// <summary>
		/// ������
		/// </summary>
		public NeuObject ApplyPerson = new NeuObject();
		/// <summary>
		/// �������
		/// </summary>
		public decimal SuretyCost= 0m;
		/// <summary>
		/// ��ע
		/// </summary>
		public string Mark;
		/// <summary>
		/// ����Ա
		/// </summary>
		public NeuObject SuretyOper = new NeuObject();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime DtOper;
//		/// <summary>
//		/// ������
//		/// </summary>
//		public NeuObject ApplyPerson = new NeuObject();
		/// <summary>
		/// ��������
		/// </summary>
		public string SuretyType;
	}
}
