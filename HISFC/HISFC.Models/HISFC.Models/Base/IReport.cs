
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// IReport<br></br>
	/// [��������: ʵ�ֲ�ѯ��������]<br></br>
	/// [�� �� ��: ����]<br></br>
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
	public interface IReport
	{
		/// <summary>
		/// ��ѯ
		/// </summary>
		void Query();

		/// <summary>
		/// ��ӡ
		/// </summary>
		void Print();

		/// <summary>
		/// ����
		/// </summary>
		void Export();

		/// <summary>
		/// ����
		/// </summary>
		void Import();

		/// <summary>
		/// ���ò������
		/// </summary>
		string Parm
		{
			get;
			set;
		}
	}
}
