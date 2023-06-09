using System;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// [功能描述: 患者诊断实体]<br></br>
	/// [创 建 者: 张立伟]<br></br>
	/// [创建时间: 2006-09-05]<br></br>
	/// <修改记录
	///		修改人=''
	///		修改时间='yyyy-mm-dd'
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary> 
    [Serializable]
    public class Diagnose:Base.Spell,Base.IValid
	{

		/// <summary>
		/// 构造函数
		/// </summary>
		public Diagnose()
		{
		}


		#region 变量

		/// <summary>
		/// 患者信息
		/// </summary>
		private FS.HISFC.Models.RADT.Patient patient = new Patient();

		/// <summary>
		/// 发生序号(10位整数)
		/// </summary>
		private long happenNO;
		
		/// <summary>
		/// 诊断类别
		/// </summary>
		private FS.FrameWork.Models.NeuObject type = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ICD10
		/// </summary>
		private FS.HISFC.Models.HealthRecord.ICD iCD10 = new FS.HISFC.Models.HealthRecord.ICD();

		/// <summary>
		/// 诊断时间
		/// </summary>
		private DateTime diagTime;

		/// <summary>
		/// 诊断医生
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 诊断科室
		/// </summary>
		private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 是否有效
		/// </summary>
		private bool isValid;

		/// <summary>
		/// 是否主诊断
		/// </summary>
		public bool isMain;

		#endregion

		#region 属性
		/// <summary>
		/// 患者信息
		/// </summary>
		public FS.HISFC.Models.RADT.Patient Patient
		{
			get
			{
				return this.patient ;
			}
			set
			{
				this.patient = value ;
			}
		}

		/// <summary>
		/// 发生序号(10位整数)
		/// </summary>
		public long HappenNO
		{
			get
			{
				return this.happenNO;
			}
			set
			{
				this.happenNO = value ;
			}
		}

		/// <summary>
		/// 诊断类别
		/// </summary>
		public FS.FrameWork.Models.NeuObject Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value ;
			}
		}

		/// <summary>
		/// ICD10
		/// </summary>
		public FS.HISFC.Models.HealthRecord.ICD ICD10
		{
			get
			{
				return this.iCD10 ;
			}
			set
			{
				this.iCD10 = value ;
			}
		}

		/// <summary>
		/// 诊断时间
		/// </summary>
		public DateTime DiagTime
		{
			get
			{
				return this.diagTime ;
			}
			set
			{
				this.diagTime = value ;
			}
		}

		/// <summary>
		/// 诊断医生
		/// </summary>
		public FS.FrameWork.Models.NeuObject Doctor
		{
			get
			{
				return this.doctor ;
			}
			set
			{
				this.doctor = value ;
			}
		}

		/// <summary>
		/// 诊断科室
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept
		{
			get
			{
				return this.dept;
			}
			set
			{
				this.dept = value ;
			}
		}

		/// <summary>
		/// 是否主诊断
		/// </summary>
		public bool IsMain
		{
			get
			{
				return this.isMain ;
			}
			set
			{
				this.isMain = value ;
			}
		}

		#endregion

		#region 方法

		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns></returns>
		public new Diagnose Clone()
		{
			Diagnose diagnose= base.Clone() as Diagnose;
			diagnose.Type = Type.Clone();
			diagnose.Dept = Dept.Clone();
			diagnose.Doctor = Doctor.Clone();
			diagnose.Patient = Patient.Clone();
			diagnose.ICD10 = ICD10.Clone();
			return diagnose;
		}

		#endregion

		#region IValid 成员

		/// <summary>
		/// 有效标志
		/// </summary>
		bool FS.HISFC.Models.Base.IValid.IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value ;
			}
		}

		#endregion
		
		#region
		
		/// <summary>
		/// 诊断时间
		/// </summary>
		[System.Obsolete("改为属性 DiagTime",true)]
		public DateTime DiagDate;
		
		[System.Obsolete("改为属性 HappenNO",true)]
		public long HappenNo;
		#endregion
	}
}
