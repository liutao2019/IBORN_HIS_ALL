namespace FS.HISFC.Models.PhysicalExamination.Management.Relation 
{


	/// <summary>
	/// �������û��Ĺ�ϵ
	/// </summary>
    [System.Serializable]
    public class DeptUserRelation : Department {

		/// <summary>
		/// �û�
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.PEUser user;

		/// <summary>
		/// �û�
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Management.PEUser User 
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
