namespace Neusoft.HISFC.Object.MedTech.Interface 
{
    /// <summary>
    /// [��������: ��¼�����ӿ�]<br></br>
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
	public interface RowInterface 
	{
		/// <summary>
		/// ��¼���ܸ���
		/// </summary>
		int RowCount 
		{
			get;
			set;
		}

		/// <summary>
		/// ��ȡ��ǰ�ļ�¼�к�
		/// </summary>
		/// <returns>��ǰ��¼���к�</returns>
		int GetRowNumber( );

		/// <summary>
		/// ѡ��ȫ��
		/// </summary>
		void SelectAll( );

		/// <summary>
		/// ����һ����¼
		/// </summary>
		/// <returns>��1ʧ��,���ڵ���0�ɹ�</returns>
		int InsertRow( );

		/// <summary>
		/// ɾ��ѡ��ļ�¼
		/// </summary>
		/// <returns>��1ʧ��,���ڵ���0�ɹ�</returns>
		int DeleteRow( );

		/// <summary>
		/// ��ȡ��ǰ�м�¼�ĳ��ض���
		/// </summary>
		/// <param name="getObject">��ȡ�ĳ��ض���</param>
		/// <param name="row">ָ�����к�</param>
		/// <returns>��1ʧ��,���ڵ���0�ɹ�</returns>
		int GetObjectFromRow( ref Neusoft.NFC.Object.NeuObject getObject, int row );
		
	}
}
