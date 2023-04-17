namespace Neusoft.HISFC.Object.MedTech.Management 
{

    /// <summary>
    /// [��������: ҽ����Ŀ]<br></br>
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
	public class Item : MedTech.Base.MTObject 
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Item( ) 
		{
		}

		#region ����

		/// <summary>
		/// �Ƿ���ҪԤԼ:1-��Ҫ,0-����Ҫ
		/// </summary>
		private bool isNeedPrecontract;

		/// <summary>
		/// �Ƿ�ҩƷ:1:��ҩƷ,0:����ҩƷ
		/// </summary>
		private bool isPharmacy;

		/// <summary>
		/// ��Ŀ��ִ�еص�
		/// </summary>
        private MedTech.Management.Location.Room room;

		/// <summary>
		/// ��ܰ��ʾ��ע������
		/// </summary>
		private Notice notice;

		#endregion

		#region ����

		/// <summary>
		/// �Ƿ���ҪԤԼ:1-��Ҫ,0-����Ҫ
		/// </summary>
		public bool IsNeedPrecontract 
		{
			get 
			{
				return this.isNeedPrecontract;
			}
			set 
			{
				this.isNeedPrecontract = value;
			}
		}

		/// <summary>
		/// �Ƿ�ҩƷ:1:��ҩƷ,0:����ҩƷ
		/// </summary>
		public bool IsPharmacy 
		{
			get 
			{
				return this.isPharmacy;
			}
			set 
			{
				this.isPharmacy = value;
			}
		}

		/// <summary>
		/// ��Ŀ��ִ�еص�
		/// </summary>
        public MedTech.Management.Location.Room Room 
		{
			get 
			{
				return this.room;
			}
			set 
			{
				this.room = value;
			}
		}

		/// <summary>
		/// ��ܰ��ʾ��ע������
		/// </summary>
		public Notice Notice 
		{
			get 
			{
				return this.notice;
			}
			set 
			{
				this.notice = value;
			}
		}

		#endregion

	}
}
