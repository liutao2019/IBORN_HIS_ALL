namespace FS.HISFC.Models.PhysicalExamination.Management.Relation
{
	/// <summary>
	/// ������Ŀ��ϵ
	/// </summary>
    [System.Serializable]
    public class DeptItemRelation : FS.HISFC.Models.PhysicalExamination.Management.Department
	{
		/// <summary>
		/// ��Ŀ
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.Item item;

		/// <summary>
		/// ��Ŀ
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