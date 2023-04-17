using FS.HISFC.Models.PhysicalExamination.Base;

namespace FS.HISFC.Models.PhysicalExamination.Collective
{
	/// <summary>
	/// Relation <br></br>
	/// [��������: ��ϵ��ʽ]<br></br>
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
    public class Relation : FS.HISFC.Models.PhysicalExamination.Base.PE
	{
		#region ˽�б���

		/// <summary>
		/// ��ϵ��ʽ���
		/// </summary>
		private PE relationType = new PE();

		#endregion

		#region ����

		/// <summary>
		/// ��ϵ��ʽ���
		/// </summary>
		public PE RelationType 
		{
			get 
			{
				return this.relationType;
			}
			set 
			{
				this.relationType = value;
			}
		}

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ϵ��ʽ</returns>
		public new Relation Clone()
		{
			Relation relation = base.Clone() as Relation;

			relation.RelationType = this.RelationType.Clone();

			return relation;
		}

		#endregion
	}
}
