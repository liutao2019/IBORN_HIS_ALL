using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// ���ýӿ�
	/// </summary>
	public interface FeeInterface
	{
		/// <summary>
		/// ��ӻ��� ��Ŀ����
		/// </summary>
		/// <param name="PatientInfo">������Ϣ</param>
		/// <param name="FeeItemList">���ķ�����Ŀ��Ϣ</param>
		/// <returns><br>0 �ɹ�</br><br>-1 ʧ��</br></returns>
		int AddPatientAccount(Neusoft.HISFC.Object.RADT.PatientInfo PatientInfo,Object.Fee.FeeItemList  FeeItemList);
		/// <summary>
		/// ���»��߷�����Ϣ
		/// </summary>
		/// <param name="PatientInfo">������Ϣ</param>
		/// <param name="FeeInfo">������Ϣ</param>
		/// <returns><br>0 �ɹ�</br><br>-1 ʧ��</br></returns>
		 int UpdateAccount(Neusoft.HISFC.Object.RADT.PatientInfo PatientInfo,Object.Fee.FeeInfo FeeInfo);
			/// <summary>
		/// ������߷�����Ϣ
		/// </summary>
		/// <param name="PatientInfo">������Ϣ</param>
		/// <returns><br>0 �ɹ�</br><br>-1 ʧ��</br></returns>
		 int PurgePatientAccount(Neusoft.HISFC.Object.RADT.PatientInfo PatientInfo);
		
		/// <summary>
		/// ���ʻ�����Ϣ
		/// </summary>
		/// <param name="PatientInfo">������Ϣ</param>
		/// <returns><br>0 �ɹ�</br><br>-1 ʧ��</br></returns>
		int EndAccount(Neusoft.HISFC.Object.RADT.PatientInfo PatientInfo);
		
	}
}
