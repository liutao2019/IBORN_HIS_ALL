using System;

namespace FS.HISFC.Models.Fee
{


	/// <summary>
	/// ���ýӿ�
	/// </summary>
    /// 
    //[System.Serializable]
	public interface IFee {

		/// <summary>
		/// ��ӻ��� ��Ŀ����
		/// </summary>
		/// <param name="PatientInfo">������Ϣ</param>
		/// <param name="FeeItemList">���ķ�����Ŀ��Ϣ</param>
		/// <returns><br>0 �ɹ�</br><br>-1 ʧ��</br></returns>
        int AddPatientAccount(FS.HISFC.Models.RADT.PatientInfo PatientInfo, Models.Fee.Inpatient.FeeItemList FeeItemList);
		/// <summary>
		/// ���»��߷�����Ϣ
		/// </summary>
		/// <param name="PatientInfo">������Ϣ</param>
		/// <param name="FeeInfo">������Ϣ</param>
		/// <returns><br>0 �ɹ�</br><br>-1 ʧ��</br></returns>
		 int UpdateAccount(FS.HISFC.Models.RADT.PatientInfo PatientInfo,Models.Fee.Inpatient.FeeInfo FeeInfo);
			/// <summary>
		/// ������߷�����Ϣ
		/// </summary>
		/// <param name="PatientInfo">������Ϣ</param>
		/// <returns><br>0 �ɹ�</br><br>-1 ʧ��</br></returns>
		 int PurgePatientAccount(FS.HISFC.Models.RADT.PatientInfo PatientInfo);
		
		/// <summary>
		/// ���ʻ�����Ϣ
		/// </summary>
		/// <param name="PatientInfo">������Ϣ</param>
		/// <returns><br>0 �ɹ�</br><br>-1 ʧ��</br></returns>
		int EndAccount(FS.HISFC.Models.RADT.PatientInfo PatientInfo);
	}
}
