namespace Neusoft.HISFC.Object.PhysicalExamination.Evaluation 
{
	/// <summary>
	/// ��ʳ������Ŀ�Ĳο����
	/// </summary>
	public class DieteticItemResult : Neusoft.HISFC.Object.PhysicalExamination.Base.PE 
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
