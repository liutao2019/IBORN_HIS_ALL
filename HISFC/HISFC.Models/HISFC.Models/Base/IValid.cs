namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// IInvalid<br></br>
	/// [��������: ʵ����Ч�Ա�ʶ]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    //[System.Serializable]
    public interface IValid
	{
		/// <summary>
		/// ��Ч��� 0 false ��Ч��1 true ��Ч
		/// </summary>		
		bool IsValid
		{
			get;
			set;
		}
	}
}
