namespace Neusoft.HISFC.Object.PhysicalExamination.Management.Relation
{
	/// <summary>
	/// ������Ŀ��ϵ
	/// </summary>
	public class DeptItemRelation : Neusoft.HISFC.Object.PhysicalExamination.Management.Department
	{
		/// <summary>
		/// ��Ŀ
		/// </summary>
		private Neusoft.HISFC.Object.PhysicalExamination.Management.Item item;

		/// <summary>
		/// ��Ŀ
		/// </summary>
		public Neusoft.HISFC.Object.PhysicalExamination.Management.Item Item
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