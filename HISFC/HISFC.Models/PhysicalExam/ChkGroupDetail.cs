using System;


namespace Neusoft.HISFC.Object.PhysicalExam {


	/// <summary>
	/// ���������ϸ
	/// </summary>
	public class ChkGroupDetail  : Neusoft.HISFC.Object.Fee.ComGroupTail
	{
		public ChkGroupDetail()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��Ч 0 ��Ч 1��Ч 
		/// </summary>
		public  string ValidState ;
		/// <summary>
		/// ������
		/// </summary>
		public int ChkTime ; 
		/// <summary>
		/// ���
		/// </summary>
		public string Spacs;
		/// <summary>
		/// ִ�п���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject ExecDept = new Neusoft.NFC.Object.NeuObject();

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new ChkGroupDetail Clone()
		{
			return this.MemberwiseClone() as ChkGroupDetail;
		}
	}
}
