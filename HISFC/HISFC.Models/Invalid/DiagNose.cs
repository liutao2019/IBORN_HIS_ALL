using System;
 
using FS.FrameWork.Models;
using System.Collections;
namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// 诊断类
	/// ID 流水号 name 姓名
	/// 2004-5 
	/// </summary>
	[Obsolete("用HealthRecord里面的诊断实体",true)]
    [Serializable]
	public class DiagNose:FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		///患者类
		/// </summary>
		public DiagNose()
		{
		
		}
		/// <summary>
		/// 患者基本信息
		/// </summary>
		public FS.HISFC.Models.RADT.Patient Patient=new FS.HISFC.Models.RADT.Patient();
		/// <summary>
		/// 诊断类别 
		/// </summary>
		public NeuObject DiagType=new NeuObject();
		/// <summary>
		/// 发生序号
		/// </summary>
		public int HappenNo;
		/// <summary>
		/// 诊断日期
		/// </summary>
		public DateTime DiagDate;
		/// <summary>
		/// 医生
		/// </summary>
		public NeuObject Doctor;
		/// <summary>
		/// 科室
		/// </summary>
		public NeuObject Dept;
		/// <summary>
		/// 是否有效 
		/// </summary>
		public bool IsValid=true;
		/// <summary>
		/// 是否主诊断
		/// </summary>
		public bool IsMain=false;
		
		public new DiagNose Clone()
		{
			DiagNose obj=base.Clone() as DiagNose;
			obj.Doctor=this.Doctor.Clone();
			obj.Dept = this.Dept.Clone();
			obj.DiagType = this.DiagType.Clone();
			return obj;
		}
	}
}
