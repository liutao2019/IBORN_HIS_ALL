namespace Neusoft.HISFC.Object.PhysicalExamination.Management.Relation 
{
	/// <summary>
	/// ��������Ŀ�Ĺ�ϵ
	/// </summary>
	public class GroupItemRelation : Group 
	{

		/// <summary>
		/// ��Ŀ
		/// </summary>
		private Neusoft.HISFC.Object.Base.Item item;

		/// <summary>
		/// ��Ŀ
		/// </summary>
		public Neusoft.HISFC.Object.Base.Item Item 
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
