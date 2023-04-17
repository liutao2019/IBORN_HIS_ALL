using System.Collections;

namespace FS.HISFC.BizLogic.PhysicalExamination.Base 
{

	/// <summary>
	/// ��ҵ���ӿ�
	/// </summary>
	public interface TableInterface 
	{

		/// <summary>
		/// �����
		/// <param name="record">��¼�ĳ��ض���</param>
		/// <returns>1���ɹ�,-1-ʧ��</returns>
		/// </summary>
		int Insert( FS.FrameWork.Models.NeuObject record );

		/// <summary>
		/// ���±�
		/// <param name="record">��¼�ĳ��ض���</param>
		/// <returns>1���ɹ�,-1-ʧ��</returns>
		/// </summary>
		int Update( FS.FrameWork.Models.NeuObject record );

		/// <summary>
		/// ����������ѯ��
		/// <param name="recordList">���صĳ��ض�������</param>
		/// <param name="whereCondition">SQL�������</param>
		/// <returns>1���ɹ�,-1-ʧ��</returns>
		/// </summary>
		int Select (ref ArrayList recordList, string whereCondition);
		
		/// <summary>
		/// ����ֶ�
		/// <param name="record">���ض���</param>
		/// </summary>
		void FillFields(FS.FrameWork.Models.NeuObject record);

		/// <summary>
		/// ���ؽ������
		/// <param name="recordList">�������</param>
		/// </summary>
		void ReturnArray(ref ArrayList recordList);
	}
}
