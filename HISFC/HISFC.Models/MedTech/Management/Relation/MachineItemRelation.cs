namespace Neusoft.HISFC.Object.MedTech.Management.Relation 
{

    /// <summary>
    /// [��������: �豸����Ŀ�Ĺ�ϵ]<br></br>
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
    public class MachineItemRelation : MedTech.Management.Machine 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public MachineItemRelation( ) 
		{
		}

		#region ����

		/// <summary>
		/// ��Ŀ
		/// </summary>
		private Item item;

		#endregion

		#region ����

		/// <summary>
		/// ��Ŀ
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
		#endregion
		
	}
}
