namespace FS.HISFC.Models.PhysicalExamination.Evaluation 
{
	/// <summary>
	/// ÿ��������ʳ����
	/// </summary>
    [System.Serializable]
    public class RegisterDieteticEvaluation : FS.HISFC.Models.PhysicalExamination.Base.PE 
	{

		/// <summary>
		/// ���Ǽ���Ϣ
		/// </summary>
        private FS.HISFC.Models.PhysicalExamination.Useless.Register.Register register;

		/// <summary>
		/// ����
		/// </summary>
		private IntegratedEvaluation evaluation;

		/// <summary>
		/// ����
		/// </summary>
		public IntegratedEvaluation Evaluation 
		{
			get 
			{
				return this.evaluation;
			}
			set 
			{
				this.evaluation = value;
			}
		}

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
	}
}
