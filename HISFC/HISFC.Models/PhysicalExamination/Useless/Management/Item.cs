namespace FS.HISFC.Models.PhysicalExamination.Management 
{
	/// <summary>
	/// Item <br></br>
	/// [��������: �����Ŀʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Item : FS.HISFC.Models.PhysicalExamination.Base.PE
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Item()
		{

		}

		#region ˽�б���
		
		/// <summary>
		/// ���������
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Enum.EnumResultType resultType;

		/// <summary>
		/// �Ƿ���ҪԤԼ
		/// </summary>
		private bool isNeedPrecontract;

		/// <summary>
		/// �Ƿ���ҩƷ
		/// </summary>
		private bool isPharmacy;

		#endregion

		#region ����

		/// <summary>
		/// ���������
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Enum.EnumResultType ResultType 
		{
			get 
			{
				return this.resultType;
			}
			set 
			{
				this.resultType = value;
			}
		}

		/// <summary>
		/// �Ƿ���ҪԤԼ
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
		/// �Ƿ���ҩƷ
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

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>�����Ŀ��</returns>
		public new Item Clone()
		{
			return base.Clone() as Item;
		}
		#endregion
	}
}
