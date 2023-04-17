namespace FS.HISFC.Models.PhysicalExamination.Management.Relation 
{
	/// <summary>
	/// 组套与项目的关系
	/// </summary>
    [System.Serializable]
    public class GroupItemRelation : Group 
	{

		/// <summary>
		/// 项目
		/// </summary>
		private FS.HISFC.Models.Base.Item item;

		/// <summary>
		/// 项目
		/// </summary>
		public FS.HISFC.Models.Base.Item Item 
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
