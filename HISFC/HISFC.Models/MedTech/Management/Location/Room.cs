namespace Neusoft.HISFC.Object.MedTech.Management.Location 
{
    /// <summary>
    /// [��������: ����]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006-12-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
	/// <summary>
	/// ����
	/// </summary>
    public class Room : Neusoft.HISFC.Object.Base.Spell 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Room( ) 
		{
		}

		#region ����

		/// <summary>
		/// ��������¥��
		/// </summary>
		private Floor floor;

		#endregion

		#region ����

		/// <summary>
		/// ��������¥��
		/// </summary>
		public Floor Floor 
		{
			get 
			{
				return this.floor;
			}
			set 
			{
				this.floor = value;
			}
		}
		#endregion
		
	}
}
