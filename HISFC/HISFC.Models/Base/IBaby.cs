
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// IBaby<br></br>
	/// [��������: ʵ��Ӥ������]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    //[System.Serializable]
    public interface IBaby
	{
		/// <summary>
		/// Ӥ�����
		/// </summary>
		string BabyNO
		{
			get;
			set;
		}

		/// <summary>
		/// �Ƿ�Ӥ��
		/// </summary>
		bool IsBaby
		{
			get;
			set;
		}
	}
}
