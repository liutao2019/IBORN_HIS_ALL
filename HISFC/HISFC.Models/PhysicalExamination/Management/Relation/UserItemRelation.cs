namespace Neusoft.HISFC.Object.PhysicalExamination.Management.Relation 
{
	/// <summary>
	/// �û�����Ŀ�Ĺ�ϵ�����������¼��ʱ����֤
	/// </summary>
	public class UserItemRelation : PEUser 
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
