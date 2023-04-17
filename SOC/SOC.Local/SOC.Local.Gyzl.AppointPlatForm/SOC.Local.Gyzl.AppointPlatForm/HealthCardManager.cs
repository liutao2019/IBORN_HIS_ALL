using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SOC.Local.Gyzl.AppointPlatForm
{
	public class HealthCardManager
	{
		//������ʾ�ӿ�
		[DllImport("getprivateinfo.dll", SetLastError = true)]
		private static extern int patientexist(string unitCode, string departmentCode, string doctorID, string doctorName,
			string role, string roleAuthCode, string cardType, string cardNumber, string businessType, string businessSerialNO);

		//���߽�����������
		[DllImport("rhin_ehr_browser.dll", SetLastError = true)]
		private static extern void rhin_ehr_browser(string unitCode, string departmentCode, string doctorID, string doctorName,
			string role, string roleAuthCode, string cardType, string cardNumber, string businessType, string businessSerialNO);

		//������Ϣ
		public string Err = string.Empty;

		/// <summary>
		/// ������ʾ�ӿ�
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
					this.Err = "�޴˽�ɫ������֤�����";
					break;
				case 2:
					this.Err = "û�е���Ȩ��";
					break;
				case 3:
					this.Err = "û�л�����Ϣ";
					break;
				case 4:
					this.Err = "û������";
					break;
				case 5:
					this.Err = "ֻ�б�Ժ����";
					break;
				case 6:
					this.Err = "ֻ����Ժ����";
					break;
				case 7:
					this.Err = "��������";
					break;
				case 8:
					this.Err = "��֤�����";
					break;
				case 9:
					this.Err = "��������ʧ�ܻ�ʱ";
					break;
				default:
					break;
			}
			return existResult;
		}

		/// <summary>
		/// ���߽�����������
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
