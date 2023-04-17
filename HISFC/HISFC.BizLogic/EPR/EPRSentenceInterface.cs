using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
	/// <summary>
	/// EPRSentenceInterface ,���׹���ӿڡ�
	/// </summary>
	public interface EPRSentenceInterface
	{
		/// <summary>
		/// װ����Ϣ�ӿ�-�����б�
		/// </summary>
		/// <param name="alInfo">��Ϣ����</param>
		void LoadInfo(ArrayList alInfo);
		/// <summary>
		/// ��ȡ��Ϣ�ӿ�
		/// </summary>
		/// <returns>��Ϣ����</returns>
		ArrayList GetInfo();
		/// <summary>
		/// �����Ϣ
		/// </summary>
		void ClsInfo();
		/// <summary>
		/// ���÷�����
		/// </summary>
		/// <param name="alGroup">������</param>
		void SetGroups(ArrayList alGroup);
		/// <summary>
		/// ��÷�����
		/// </summary>
		/// <returns>ArrayList ������</returns>
		ArrayList GetGoups();
	}
}
