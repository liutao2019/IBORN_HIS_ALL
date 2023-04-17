using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SOC.Local.Gyzl.AppointPlatForm
{
	public class HealthCardManager
	{
		//调阅提示接口
		[DllImport("getprivateinfo.dll", SetLastError = true)]
		private static extern int patientexist(string unitCode, string departmentCode, string doctorID, string doctorName,
			string role, string roleAuthCode, string cardType, string cardNumber, string businessType, string businessSerialNO);

		//患者健康档案调阅
		[DllImport("rhin_ehr_browser.dll", SetLastError = true)]
		private static extern void rhin_ehr_browser(string unitCode, string departmentCode, string doctorID, string doctorName,
			string role, string roleAuthCode, string cardType, string cardNumber, string businessType, string businessSerialNO);

		//错误信息
		public string Err = string.Empty;

		/// <summary>
		/// 调阅提示接口
		/// </summary>
		/// <param name="healthCard"></param>
		/// <returns></returns>
		public int PatientExist(HealthCard card)
		{
			this.Err = string.Empty;
			int existResult;
			existResult = patientexist(card.UnitCode, card.DepartmentCode, card.DoctorID, card.DoctorName, card.Role, card.AuthPassword, 
									   card.CardType, card.CardNumber, card.BusinessType, card.BusinessSerialNO);
            
			switch (existResult)
			{
				case 1:
					this.Err = "无此角色名或认证码错误";
					break;
				case 2:
					this.Err = "没有调阅权限";
					break;
				case 3:
					this.Err = "没有患者信息";
					break;
				case 4:
					this.Err = "没有数据";
					break;
				case 5:
					this.Err = "只有本院数据";
					break;
				case 6:
					this.Err = "只有外院数据";
					break;
				case 7:
					this.Err = "都有数据";
					break;
				case 8:
					this.Err = "验证码错误";
					break;
				case 9:
					this.Err = "网络连接失败或超时";
					break;
				default:
					break;
			}
			return existResult;
		}

		/// <summary>
		/// 患者健康档案调阅
		/// </summary>
		/// <param name="healthCard"></param>
		/// <returns></returns>
		public void PopHealthRecord(HealthCard card)
		{
			this.Err = string.Empty;
			rhin_ehr_browser(card.UnitCode, card.DepartmentCode, card.DoctorID, card.DoctorName, card.Role, card.AuthPassword, 
							 card.CardType, card.CardNumber, card.BusinessType, card.BusinessSerialNO);
		}
	}
}
