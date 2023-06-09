namespace FS.HISFC.Models.PhysicalExamination.Management 
{
	/// <summary>
	/// Department <br></br>
	/// [功能描述: 体检科室实体]<br></br>
	/// [创 建 者: 飞斯]<br></br>
	/// [创建时间: 2006-11-10]<br></br>
	/// <修改记录
	///		修改人=''
	///		修改时间=''
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Department : FS.HISFC.Models.PhysicalExamination.Base.PE 
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public Department()
		{

		}

		#region 私有变量
		
		/// <summary>
		/// 体检业务
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Base.PE business;

		#endregion

		#region 属性

		/// <summary>
		/// 体检业务
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Base.PE Business 
		{
			get 
			{
				return this.business;
			}
			set 
			{
				this.business = value;
			}
		}

		#endregion

		#region 方法
		
		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns>体检科室类</returns>
		public new Department Clone()
		{
			Department department = base.Clone() as Department;

			department.Business = this.Business.Clone();

			return department;
		}
		#endregion
	}
}
