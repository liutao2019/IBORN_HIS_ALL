namespace Neusoft.HISFC.Object.MedTech.Management.Location 
{
    /// <summary>
    /// [��������: ¥��]<br></br>
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
    public class Floor :Neusoft.HISFC.Object.Base.Spell
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Floor( ) 
		{
		}

		#region ����

		/// <summary>
		/// ¥������������
		/// </summary>
		private Building building;

		#endregion

		#region ����

		/// <summary>
		/// ¥������������
		/// </summary>
		public Building Building 
		{
			get 
			{
				return this.building;
			}
			set 
			{
				this.building = value;
			}
		}
		#endregion
		
	}
}
