namespace FS.HISFC.Models.PhysicalExamination.Evaluation
{
	/// <summary>
	/// 饮食结构项目
	/// </summary>
    [System.Serializable]
	public class DieteticItem : FS.HISFC.Models.PhysicalExamination.Base.PE
	{
		/// <summary>
		/// 参考内容
		/// </summary>
		private string referenceContent;

		/// <summary>
		/// 参考内容
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
