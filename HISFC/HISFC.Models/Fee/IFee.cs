using System;

namespace FS.HISFC.Models.Fee
{


	/// <summary>
	/// 费用接口
	/// </summary>
    /// 
    //[System.Serializable]
	public interface IFee {

		/// <summary>
		/// 添加患者 项目费用
		/// </summary>
		/// <param name="PatientInfo">患者信息</param>
		/// <param name="FeeItemList">交的费用项目信息</param>
		/// <returns><br>0 成功</br><br>-1 失败</br></returns>
        int AddPatientAccount(FS.HISFC.Models.RADT.PatientInfo PatientInfo, Models.Fee.Inpatient.FeeItemList FeeItemList);
		/// <summary>
		/// 更新患者费用信息
		/// </summary>
		/// <param name="PatientInfo">患者信息</param>
		/// <param name="FeeInfo">费用信息</param>
		/// <returns><br>0 成功</br><br>-1 失败</br></returns>
		 int UpdateAccount(FS.HISFC.Models.RADT.PatientInfo PatientInfo,Models.Fee.Inpatient.FeeInfo FeeInfo);
			/// <summary>
		/// 清除患者费用信息
		/// </summary>
		/// <param name="PatientInfo">患者信息</param>
		/// <returns><br>0 成功</br><br>-1 失败</br></returns>
		 int PurgePatientAccount(FS.HISFC.Models.RADT.PatientInfo PatientInfo);
		
		/// <summary>
		/// 结帐患者信息
		/// </summary>
		/// <param name="PatientInfo">患者信息</param>
		/// <returns><br>0 成功</br><br>-1 失败</br></returns>
		int EndAccount(FS.HISFC.Models.RADT.PatientInfo PatientInfo);
	}
}
