namespace Neusoft.HISFC.Object.PhysicalExamination.Base
{
	/// <summary>
	/// Hospital <br></br>
	/// [��������: ҽԺʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Hospital : Neusoft.HISFC.Object.Base.Spell 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Hospital( ) 
		{
		}

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>ҽԺ��</returns>
		public new Hospital Clone()
		{
			Hospital hospital = base.Clone() as Hospital;

			return hospital;
		}
		#endregion
	}
}
