namespace FS.HISFC.Models.PhysicalExamination.Base
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
    [System.Serializable]
    public class Hospital : FS.HISFC.Models.Base.Spell 
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
