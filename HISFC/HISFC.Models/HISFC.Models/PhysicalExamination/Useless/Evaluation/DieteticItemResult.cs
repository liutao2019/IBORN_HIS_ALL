namespace FS.HISFC.Models.PhysicalExamination.Evaluation 
{
	/// <summary>
	/// ��ʳ������Ŀ�Ĳο����
	/// </summary>
    [System.Serializable]
	public class DieteticItemResult : FS.HISFC.Models.PhysicalExamination.Base.PE 
	{
		/// <summary>
		/// ��ʳ������Ŀ
		/// </summary>
		private DieteticItem dieteticItem;

		/// <summary>
		/// ��ʳ������Ŀ
		/// </summary>
		public DieteticItem DieteticItem 
		{
			get 
			{
				return this.dieteticItem;
			}
			set 
			{
				this.dieteticItem = value;
			}
		}
	}
}
