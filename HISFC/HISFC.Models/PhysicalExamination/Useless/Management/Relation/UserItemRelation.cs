namespace FS.HISFC.Models.PhysicalExamination.Management.Relation 
{
	/// <summary>
	/// �û�����Ŀ�Ĺ�ϵ�����������¼��ʱ����֤
	/// </summary>
    [System.Serializable]
    public class UserItemRelation : PEUser 
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
