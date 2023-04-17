namespace FS.HISFC.Models.PhysicalExamination.Management.Relation 
{


	/// <summary>
	/// 科室与用户的关系
	/// </summary>
    [System.Serializable]
    public class DeptUserRelation : Department {

		/// <summary>
		/// 用户
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.PEUser user;

		/// <summary>
		/// 用户
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Management.PEUser User 
		{
			get 
			{
				return this.user;
			}
			set 
			{
				this.user = value;
			}
		}
	}
}
