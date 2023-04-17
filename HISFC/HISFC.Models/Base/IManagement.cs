using System.Collections;
using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// IManagement<br></br>
	/// [��������: ʵ��ʵ���������]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    //[Serializable]
    public interface IManagement
	{
		/// <summary>
		/// ��õ���ʵ��
		/// </summary>
		/// <param name="obj">ʵ������</param>
		/// <returns>��ʵ���ʵ��</returns>
		NeuObject Get(System.Object obj);

		/// <summary>
		/// ���ʵ���б�
		/// </summary>
		/// <returns></returns>
		ArrayList GetList();

		/// <summary>
		/// ���õ���ʵ��
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		int Set(NeuObject obj);

		/// <summary>
		/// ����ʵ���б�
		/// </summary>
		/// <param name="al"></param>
		/// <returns></returns>
		int SetList(ArrayList al);

		/// <summary>
		/// ɾ��ʵ��
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		int Del(System.Object obj);
	}
}
