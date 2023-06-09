namespace FS.HISFC.Models.PhysicalExamination.Management.Relation 
{
	/// <summary>
	/// 体检套餐与体检项目、体检项目组套的对应关系
	/// </summary>
    [System.Serializable]
    public class MealItemRelation : PEMeal 
	{

		/// <summary>
		/// 是否是组套项目
		/// </summary>
		private bool isGroup;

		/// <summary>
		/// 是否是组套项目
		/// </summary>
		public bool IsGroup 
		{
			get 
			{
				return this.isGroup;
			}
			set 
			{
				this.isGroup = value;
			}
		}

		/// <summary>
		/// 项目、组套
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.Item item;

		/// <summary>
		/// 项目、组套
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
