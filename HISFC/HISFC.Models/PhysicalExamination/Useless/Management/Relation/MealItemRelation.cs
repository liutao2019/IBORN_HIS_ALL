namespace FS.HISFC.Models.PhysicalExamination.Management.Relation 
{
	/// <summary>
	/// ����ײ��������Ŀ�������Ŀ���׵Ķ�Ӧ��ϵ
	/// </summary>
    [System.Serializable]
    public class MealItemRelation : PEMeal 
	{

		/// <summary>
		/// �Ƿ���������Ŀ
		/// </summary>
		private bool isGroup;

		/// <summary>
		/// �Ƿ���������Ŀ
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
		/// ��Ŀ������
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.Item item;

		/// <summary>
		/// ��Ŀ������
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
