namespace Neusoft.HISFC.Object.MedTech.Management 
{

    /// <summary>
    /// [��������: ��ܰ��ʾ��ע������]<br></br>
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
    public class Notice : MedTech.Base.MTObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Notice( ) 
		{
		}

		/// <summary>
		/// ��Ӧ��Ŀ����
		/// </summary>
		private Item item;

		/// <summary>
		/// ��Ӧ��Ŀ����
		/// </summary>
		public Item Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}
	}
}
