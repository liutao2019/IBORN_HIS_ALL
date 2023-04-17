namespace Neusoft.HISFC.Object.PhysicalExamination.Management 
{
	/// <summary>
	/// Role <br></br>
	/// [��������: ���Ȩ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Role : Neusoft.HISFC.Object.PhysicalExamination.Base.PE 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Role()
		{

		}

		#region ˽�б���
		
		/// <summary>
		/// ����û�
		/// </summary>
		private PEUser user;

		#endregion

		#region ����

		/// <summary>
		/// ����û�
		/// </summary>
		public PEUser User 
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

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���Ȩ��</returns>
		public new Role Clone()
		{
			Role role = base.Clone() as Role;

			role.User = this.User.Clone();

			return role;
		}
		#endregion
	}
}
