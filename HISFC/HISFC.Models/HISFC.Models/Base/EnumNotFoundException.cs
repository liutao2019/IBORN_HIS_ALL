using System;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// EnumNotFoundException<br></br>
	/// [��������: ö�ٷ���δ�ҵ��쳣]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-31]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class EnumNotFoundException : ApplicationException 
	{
		public EnumNotFoundException( Enum enumType ) : 
			base("δ��ö�ٷ��������ҵ�ö��: " + enumType.GetType().ToString() + "." + enumType.ToString()) 
		{
		}
	}
}
