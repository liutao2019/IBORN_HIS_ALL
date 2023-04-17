using System;
using System.Collections;
using FS.FrameWork.Models;
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// IEnumService<br></br>
	/// [��������: Enum������ӿڣ�����ʵ��Enum��������]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-31]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    //[Serializable]
    public interface IEnumService
	{
		/// <summary>
		/// �õ�ö����������
		/// </summary>
		/// <param name="enumType">ö��</param>
		/// <returns>��������</returns>
		string GetName(Enum enumType);
		
		/// <summary>
		/// ö������
		/// </summary>
		FrameWork.Models.NeuObject[] ObjectItems
		{
			get;
		}
		
		/// <summary>
		/// ö����������
		/// </summary>
		string[] StringItems
		{
			get;			
		}

	}
}
