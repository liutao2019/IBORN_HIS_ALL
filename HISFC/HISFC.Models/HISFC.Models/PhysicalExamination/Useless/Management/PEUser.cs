namespace FS.HISFC.Models.PhysicalExamination.Management 
{
	/// <summary>
	/// PEUser <br></br>
	/// [��������: ����û�]<br></br>
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
    public class PEUser : FS.HISFC.Models.PhysicalExamination.Base.PE 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PEUser()
		{

		}

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>����û�</returns>
		public new PEUser Clone()
		{
			return base.Clone() as PEUser;
		}
		#endregion
	}
}
