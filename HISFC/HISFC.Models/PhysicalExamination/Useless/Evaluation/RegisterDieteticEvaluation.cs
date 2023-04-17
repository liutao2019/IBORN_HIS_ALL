namespace FS.HISFC.Models.PhysicalExamination.Evaluation 
{
	/// <summary>
	/// 每次体检的饮食评估
	/// </summary>
    [System.Serializable]
    public class RegisterDieteticEvaluation : FS.HISFC.Models.PhysicalExamination.Base.PE 
	{

		/// <summary>
		/// 体检登记信息
		/// </summary>
        private FS.HISFC.Models.PhysicalExamination.Useless.Register.Register register;

		/// <summary>
		/// 评估
		/// </summary>
		private IntegratedEvaluation evaluation;

		/// <summary>
		/// 评估
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
		/// 体检登记信息
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
