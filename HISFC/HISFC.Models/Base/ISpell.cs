
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// ISort<br></br>
	/// [��������: ʵ�ִ������]<br></br>
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
    public interface ISpell 
	{
		/// <summary>
		/// ƴ����
		/// </summary>
		string SpellCode
		{	
			get;
			set;
		}

		/// <summary>
		/// �����
		/// </summary>
		string WBCode
		{
			get;
			set;
		}

		/// <summary>
		/// �Զ�����
		/// </summary>
		string UserCode
		{	
			get;
			set;
		}
	}
}
