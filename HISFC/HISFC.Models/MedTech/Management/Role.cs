namespace FS.HISFC.Models.MedTech.Management 
{
    /// <summary>
    /// [��������: Ȩ��]<br></br>
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
    /// 
    [System.Serializable]
    public class Role 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Role( ) 
		{
		}

		#region ����

		/// <summary>
		/// ����
		/// </summary>
		private Window window;

		#endregion

		#region ����

		/// <summary>
		/// ����
		/// </summary>
		public Window Window 
		{
			get 
			{
				return this.window;
			}
			set 
			{
				this.window = value;
			}
		}

		#endregion

		#region ����
		#endregion
	}
}
