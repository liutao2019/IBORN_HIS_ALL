namespace FS.HISFC.Models.MedTech.Management 
{
    /// <summary>
    /// [��������: ҽ��ԤԼ��]<br></br>
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
	public class Group : FS.HISFC.Models.Base.Spell
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Group( ) 
		{
		}

		#region ����

		/// <summary>
		/// ������
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment master;

		#endregion

		#region ����

		/// <summary>
		/// ������
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Master 
		{
			get 
			{
				return this.master;
			}
			set 
			{
				this.master = value;
			}
		}
		#endregion
		
	}
}
