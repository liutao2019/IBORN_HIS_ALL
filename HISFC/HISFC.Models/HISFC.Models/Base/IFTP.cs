using System;

namespace FS.HISFC.Models.File
{
	/// <summary>
	/// IFTP<br></br>
	/// [��������: ʵ��FTP����]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    //[System.Serializable]
    public interface IFTP
	{
		/// <summary>
		/// IP��ַ
		/// </summary>
		string IP
		{
			get;
			set;
		}
		/// <summary>
		/// �û���
		/// </summary>
		string UserName
		{
			get;
			set;
		}
		/// <summary>
		/// ����
		/// </summary>
		string PassWord
		{
			get;
			set;
		}
		/// <summary>
		/// Ŀ¼
		/// </summary>
		string Folders
		{
			get;
			set;
		}
		/// <summary>
		/// �ļ���
		/// </summary>
		string FileName
		{
			get;
			set;
		}
		/// <summary>
		/// ����
		/// </summary>
		string Root
		{
			get;
			set;
		}
	}
}
