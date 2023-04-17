namespace FS.HISFC.Models.PhysicalExamination.Evaluation 
{
	/// <summary>
	/// ��ʳ����
	/// </summary>
    [System.Serializable]
    public class DieteticEvaluation : FS.HISFC.Models.PhysicalExamination.Base.PE
	{
		/// <summary>
		/// ���Ǽ���Ϣ
		/// </summary>
        private FS.HISFC.Models.PhysicalExamination.Useless.Register.Register register;

		/// <summary>
		/// ���Ǽ���Ϣ
		/// </summary>
        public FS.HISFC.Models.PhysicalExamination.Useless.Register.Register Register 
		{
			get 
			{
				return this.register;
			}
			set 
			{
				this.register = value;
			}
		}

		/// <summary>
		/// ��ʳ�������
		/// </summary>
		private DieteticItemResult itemResult;

		/// <summary>
		/// ��ʳ�������
		/// </summary>
		public DieteticItemResult ItemResult 
		{
			get 
			{
				return this.itemResult;
			}
			set 
			{
				this.itemResult = value;
			}
		}
	}
}
