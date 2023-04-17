using Neusoft.HISFC.Object.PhysicalExamination.Base;

namespace Neusoft.HISFC.Object.PhysicalExamination.Collective
{
	/// <summary>
	/// Collective <br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Collective : Neusoft.HISFC.Object.PhysicalExamination.Base.PE
	{
		#region ����
		
		/// <summary>
		/// �������
		/// </summary>
		private PE collectiveTYpe = new PE();

		#endregion

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		public PE CollectiveType 
		{
			get 
			{
				return this.collectiveTYpe;
			}
			set 
			{
				this.collectiveTYpe = value;
			}
		}

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��켯��</returns>
		public new Collective Clone()
		{
			Collective collective = base.Clone() as Collective;

			collective.CollectiveType = this.CollectiveType.Clone();

			return collective;
		}
		#endregion
	}
}
