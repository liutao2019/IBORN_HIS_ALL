namespace Neusoft.HISFC.Object.PhysicalExamination.Evaluation
{
	/// <summary>
	/// ��ʳ�ṹ��Ŀ
	/// </summary>
	public class DieteticItem : Neusoft.HISFC.Object.PhysicalExamination.Base.PE
	{
		/// <summary>
		/// �ο�����
		/// </summary>
		private string referenceContent;

		/// <summary>
		/// �ο�����
		/// </summary>
		public string ReferenceContent
		{
			get
			{
				return this.referenceContent;
			}
			set
			{
				this.referenceContent = value;
			}
		}
	}
}
