namespace FS.HISFC.Models.PhysicalExamination.Management 
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
    [System.Serializable]
    public class Business : FS.HISFC.Models.PhysicalExamination.Base.PE 
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
