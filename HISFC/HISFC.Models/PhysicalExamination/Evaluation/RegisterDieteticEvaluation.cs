namespace Neusoft.HISFC.Object.PhysicalExamination.Evaluation 
{
	/// <summary>
	/// ÿ��������ʳ����
	/// </summary>
	public class RegisterDieteticEvaluation : Neusoft.HISFC.Object.PhysicalExamination.Base.PE 
	{

		/// <summary>
		/// ���Ǽ���Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.PhysicalExamination.Register.Register register;

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
		public Neusoft.HISFC.Object.PhysicalExamination.Register.Register Register 
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
