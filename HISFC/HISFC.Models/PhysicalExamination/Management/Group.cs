namespace Neusoft.HISFC.Object.PhysicalExamination.Management 
{
	/// <summary>
	/// Group <br></br>
	/// [��������: �����Ŀʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Group : Neusoft.HISFC.Object.PhysicalExamination.Base.PE 
	{
		public Group()
		{

		}

		#region ˽�б���
		
		/// <summary>
		/// �۸�
		/// </summary>
		private Price price;

		#endregion

		#region ����
		
		/// <summary>
		/// �۸�
		/// </summary>
		public Price Price 
		{
			get 
			{
				return this.price;
			}
			set 
			{
				this.price = value;
			}
		}

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>�����Ŀ��</returns>
		public new Group Clone()
		{
			Group group = base.Clone() as Group;

			group.Price = this.Price.Clone();

			return group;
		}
		#endregion
	}
}
