namespace Neusoft.HISFC.Object.MedTech.Management.Relation 
{

    /// <summary>
    /// [��������: ����Ա��ҽ����Ŀ�Ĺ�ϵ]<br></br>
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
    public class OperItemRelation : MedTech.Management.Oper 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public OperItemRelation( ) 
		{

		}

		#region ����

		/// <summary>
		/// ��Ŀ
		/// </summary>
		private Item item;

		/// <summary>
		/// ����Ա
		/// </summary>
		private Oper oper;

		/// <summary>
		/// ʹ��Ƶ��
		/// </summary>
		private string frequence;

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

		/// <summary>
		/// ����Ա
		/// </summary>
		public Oper Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}
		/// <summary>
		/// ʹ��Ƶ��
		/// </summary>
		public string Frequence
		{
			get
			{
				return this.frequence;
			}
			set
			{
				this.frequence = value;
			}
		}

		#endregion

	}
}
