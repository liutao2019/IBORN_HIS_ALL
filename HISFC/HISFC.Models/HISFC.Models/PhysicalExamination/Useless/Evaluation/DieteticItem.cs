namespace FS.HISFC.Models.PhysicalExamination.Evaluation
{
	/// <summary>
	/// ��ʳ�ṹ��Ŀ
	/// </summary>
    [System.Serializable]
	public class DieteticItem : FS.HISFC.Models.PhysicalExamination.Base.PE
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
