using System;

namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.EnumMutex<br></br>
	/// [��������: ������-ö��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public enum EnumMutex
	{
		/// <summary>
		/// �޻���0
		/// </summary>
		None =0,
		/// <summary>
		/// �黥��1
		/// </summary>
		Group =1,
		/// <summary>
		/// ��𻥳�2
		/// </summary>
		SysClass =2,
		/// <summary>
		/// ȫ��5
		/// </summary>
		All =5
	}
}
