namespace Neusoft.HISFC.Object.PhysicalExamination.Evaluation 
{
	/// <summary>
	/// ��ʳ����
	/// </summary>
	public class DieteticEvaluation : Neusoft.HISFC.Object.PhysicalExamination.Base.PE
	{
		/// <summary>
		/// ���Ǽ���Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.PhysicalExamination.Register.Register register;

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
