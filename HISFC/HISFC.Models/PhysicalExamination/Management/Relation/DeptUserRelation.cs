namespace Neusoft.HISFC.Object.PhysicalExamination.Management.Relation 
{


	/// <summary>
	/// �������û��Ĺ�ϵ
	/// </summary>
	public class DeptUserRelation : Department {

		/// <summary>
		/// �û�
		/// </summary>
		private Neusoft.HISFC.Object.PhysicalExamination.Management.PEUser user;

		/// <summary>
		/// �û�
		/// </summary>
		public Neusoft.HISFC.Object.PhysicalExamination.Management.PEUser User 
		{
			get 
			{
				return this.user;
			}
			set 
			{
				this.user = value;
			}
		}
	}
}
