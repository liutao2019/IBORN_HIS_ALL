namespace Neusoft.HISFC.Object.PhysicalExamination.Management 
{
	/// <summary>
	/// Business <br></br>
	/// [��������: ���ҵ��ʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Business : Neusoft.HISFC.Object.PhysicalExamination.Base.PE 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Business()
		{
			
		}
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Business Clone()
		{
			return base.Clone() as Business;
		}
	}
}
