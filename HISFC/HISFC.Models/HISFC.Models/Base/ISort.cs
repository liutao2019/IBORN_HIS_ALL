namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// ISort<br></br>
	/// [��������: ʵ����������]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    //[System.Serializable]
	public interface ISort
	{
		/// <summary>
		/// �����
		/// </summary>
		int SortID
		{
			get;
			set;
		}
	}
}