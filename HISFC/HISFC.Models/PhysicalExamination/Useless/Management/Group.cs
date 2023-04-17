namespace FS.HISFC.Models.PhysicalExamination.Management 
{
	/// <summary>
	/// Group <br></br>
	/// [��������: �����Ŀʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Group : FS.HISFC.Models.PhysicalExamination.Base.PE 
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
