using System;
using FS.HISFC.Models;
using FS.FrameWork.Models;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// Doctor ��ժҪ˵����
	/// </summary>
	public class Doctor:DataBase 
	{
		public Doctor()
		{
		}
		#region"����"
		#region ҽ���޶�
//		/// <summary>
//		/// ���ҽ���Һ��Ƿ��޶�
//		/// </summary>
//		/// <param name="DoctorID"></param>
//		/// <param name="DeptID"></param>
//		/// <param name="NoonID"></param>
//		/// <param name="RegLevelID"></param>
//		/// <param name="dtDate"></param>
//		/// <returns></returns>
//		public bool GetDoctorRegLimit(string DoctorID,string DeptID,string NoonID,string RegLevelID,DateTime dtDate)
//		{
//			return true;
//		}
//		/// <summary>
//		/// ���ҽ���Һ��޶�
//		/// </summary>
//		/// <param name="Doctor">ҽ��</param>
//		/// <returns></returns>
//		public FS.HISFC.Models.Base.RegLimit GetDoctorRegLimit(NeuObject Doctor)
//		{
//			Object.Base.RegLimit DocRegLimit=new Object.Base.RegLimit();
//			return DocRegLimit;
//		}
//		/// <summary>
//		/// ����ҽ���Һ��޶�
//		/// </summary>
//		/// <param name="Doctor">ҽ��</param>
//		/// <param name="DocRegLimit">�޶�</param>
//		/// <returns></returns>
//		public int UpdateDoctorRegLimit(NeuObject Doctor,Object.Base.RegLimit DocRegLimit)
//		{
//			return 0;
//		}
//		
//		public int DelDoctorRegLimit(NeuObject Doctor,Object.Base.RegLimit DocRegLimit)
//		{
//			return 0;
//		}
		#endregion ҽ���޶�
		#region ���ҽ��
		/// <summary>
		/// ���ҽ���б�
		/// </summary>
		/// <returns></returns>
		public ArrayList GetClientDoctor()
		{
			return null;
		}

		#endregion ���ҽ��
		#endregion
		#region סԺ
		/// <summary>
		/// ���סԺҽ��
		/// </summary>
		/// <returns></returns>
		public ArrayList GetDeptDoctore()
		{
			return null;
		}
		#endregion
	}
}
