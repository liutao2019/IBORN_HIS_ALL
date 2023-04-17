using System.Collections;

namespace Neusoft.HISFC.Object.MedTech.Interface 
{
    /// <summary>
    /// [��������: UC�ؼ��Ļ����ӿ�]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006-12-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
	public interface UCInterface 
	{

		/// <summary>
		/// �ӿڴ�����Ϣ����
		/// </summary>
		Neusoft.NFC.Object.NeuObject Error
		{
			get;
			set;
		}

		/// <summary>
		/// ���ء���ʼ������
		/// </summary>
		/// <param name="interfaceParameter">����</param>
		/// <returns>��1ʧ��,���ڵ���0�ɹ�</returns>
		int Load( Neusoft.NFC.Object.NeuObject interfaceParameter );

		/// <summary>
		/// ж�ء��رշ���
		/// </summary>
		/// <param name="interfaceParameter">����</param>
		/// <returns>��1ʧ��,���ڵ���0�ɹ�</returns>
		int Unload( Neusoft.NFC.Object.NeuObject interfaceParameter );

		/// <summary>
		/// ��Ч�ԡ��Ϸ���У�鷽��
		/// </summary>
		/// <param name="interfaceParameter">����</param>
		/// <returns>��1ʧ��,���ڵ���0�ɹ�</returns>
		int Validity( Neusoft.NFC.Object.NeuObject interfaceParameter );

		/// <summary>
		/// ���淽��
		/// </summary>
		/// <param name="interfaceParameter">����</param>
		/// <returns>��1ʧ��,���ڵ���0�ɹ�</returns>
		int Save( Neusoft.NFC.Object.NeuObject interfaceParameter );

		/// <summary>
		/// ��ѯ��ͳ�Ʒ���
		/// </summary>
		/// <param name="interfaceParameter">����</param>
		/// <param name="resultList">��ѯ��ͳ�ƵĽ��</param>
		/// <returns>��1ʧ��,���ڵ���0�ɹ�</returns>
		int Query( Neusoft.NFC.Object.NeuObject interfaceParameter, ref ArrayList resultList );

		/// <summary>
		/// ��ӡ����
		/// </summary>
		/// <param name="interfaceParameter">����</param>
		void Print( Neusoft.NFC.Object.NeuObject interfaceParameter );

		/// <summary>
		/// ���򷽷�
		/// </summary>
		/// <param name="interfaceParameter">����</param>
		void Sort( Neusoft.NFC.Object.NeuObject interfaceParameter );

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="interfaceParameter">����</param>
		void Export( Neusoft.NFC.Object.NeuObject interfaceParameter );
	}
}
