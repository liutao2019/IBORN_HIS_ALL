namespace FS.HISFC.Models.PhysicalExamination.Management.Relation 
{
	/// <summary>
	/// ��������Ŀ�Ĺ�ϵ
	/// </summary>
    [System.Serializable]
    public class GroupItemRelation : Group 
	{

		/// <summary>
		/// ��Ŀ
		/// </summary>
		private FS.HISFC.Models.Base.Item item;

		/// <summary>
		/// ��Ŀ
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
