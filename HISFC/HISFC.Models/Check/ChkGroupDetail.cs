using System;

namespace neusoft.HISFC.Object.Check
{
	/// <summary>
	/// ���������ϸ 
	/// </summary>
	public class ChkGroupDetail  : neusoft.HISFC.Object.Fee.ComGroupTail
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
		public neusoft.neuFC.Object.neuObject ExecDept = new neusoft.neuFC.Object.neuObject();
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
