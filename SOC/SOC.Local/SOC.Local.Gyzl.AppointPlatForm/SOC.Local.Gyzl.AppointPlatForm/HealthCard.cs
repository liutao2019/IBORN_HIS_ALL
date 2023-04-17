using System;
using System.Text;
using System.Xml;

namespace SOC.Local.Gyzl.AppointPlatForm
{
	public class HealthCard
	{
		public HealthCard()
		{

		}

		private string unitCode = string.Empty;
		private string departmentCode = string.Empty;
		private string doctorID = string.Empty;
		private string doctorName = string.Empty;
		private string role = string.Empty;
		private string authPassword = string.Empty;
		private string cardType = "0";
		private string cardNumber = string.Empty;
		private string businessType = "01";
		private string businessSerialNO = string.Empty;

		/// <summary>
		/// 机构编码
		/// </summary>
		public string UnitCode
		{
			get { return unitCode; }
			set { unitCode = value; }
		}

		/// <summary>
		/// 科室编码
		/// </summary>
		public string DepartmentCode
		{
			get { return departmentCode; }
			set { departmentCode = value; }
		}

		/// <summary>
		/// 医生工号
		/// </summary>
		public string DoctorID
		{
			get { return doctorID; }
			set { doctorID = value; }
		}

		/// <summary>
		/// 医生姓名
		/// </summary>
		public string DoctorName
		{
			get { return doctorName; }
			set { doctorName = value; }
		}

		/// <summary>
		/// 角色
		/// </summary>
		public string Role
		{
			get { return role; }
			set { role = value; }
		}

		/// <summary>
		/// 角色认证码
		/// </summary>
		public string AuthPassword
		{
			get { return authPassword; }
			set { authPassword = value; }
		}

		/// <summary>
		/// 卡类型
		/// </summary>
		public string CardType
		{
			get { return cardType; }
			set { cardType = value; }
		}

		/// <summary>
		/// 卡号
		/// </summary>
		public string CardNumber
		{
			get { return cardNumber; }
			set { cardNumber = value; }
		}

		/// <summary>
		/// 业务类型
		/// </summary>
		public string BusinessType
		{
			get { return businessType; }
			set { businessType = value; }
		}

		/// <summary>
		/// 业务流水号
		/// </summary>
		public string BusinessSerialNO
		{
			get { return businessSerialNO; }
			set { businessSerialNO = value; }
		}
	}
}