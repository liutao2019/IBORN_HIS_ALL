namespace FS.HISFC.Models.PhysicalExamination.Management.Relation
{
	/// <summary>
	/// 科室项目关系
	/// </summary>
    [System.Serializable]
    public class DeptItemRelation : FS.HISFC.Models.PhysicalExamination.Management.Department
	{
		/// <summary>
		/// 项目
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.Item item;

		/// <summary>
		/// 项目
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Management.Item Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}
	}
}