using FS.HISFC.Models.Base;
using FS.HISFC.Models.PhysicalExamination.Base;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.PhysicalExamination.HealthArchieve
{
	/// <summary>
	/// HealthArchieves <br></br>
	/// [��������: ��������]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class HealthArchieve : FS.HISFC.Models.PhysicalExamination.Base.PE
	{
		#region ˽�б���
		
		/// <summary>
		/// �ͻ���ϵ���
		/// </summary>
		private PE crmType = new PE();

		/// <summary>
		/// �����������
		/// </summary>
		private PE archieveType = new PE();

		/// <summary>
		/// �˿͵Ļ�����Ϣ
		/// </summary>
		private FS.HISFC.Models.RADT.Patient guest = new Patient();

		/// <summary>
		/// ����ܴ���
		/// </summary>
		private int totalCount;

		/// <summary>
		/// ����ܻ���
		/// </summary>
		private decimal totalCost;

		/// <summary>
		/// ��������
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Collective.Collective collective = new Collective.Collective ();
		
		/// <summary>
		/// �Ա�
		/// </summary>
		private FS.HISFC.Models.Base.EnumSex sex = new EnumSex();

		#endregion

		#region ����
		
		/// <summary>
		/// �Ա�
		/// </summary>
		public FS.HISFC.Models.Base.EnumSex Sex
		{
			get
			{
				return this.sex;
			}
			set
			{
				this.sex = value;
			}
		}

		/// <summary>
		/// �ͻ���ϵ���
		/// </summary>
		public PE CRMType 
		{
			get 
			{
				return this.crmType;
			}
			set 
			{
				this.crmType = value;
			}
		}

		/// <summary>
		/// �����������
		/// </summary>
		public PE ArchieveType
		{
			get
			{
				return this.archieveType;
			}
			set
			{
				this.archieveType = value;
			}
		}

		/// <summary>
		/// �˿͵Ļ�����Ϣ
		/// </summary>
		public FS.HISFC.Models.RADT.Patient Guest
		{
			get
			{
				return this.guest;
			}
			set
			{
				this.guest = value;
			}
		}

		/// <summary>
		/// ����ܴ���
		/// </summary>
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
			set
			{
				this.totalCount = value;
			}
		}

		/// <summary>
		/// ����ܻ���
		/// </summary>
		public decimal TotalCost
		{
			get
			{
				return this.totalCost;
			}
			set
			{
				this.totalCost = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Collective.Collective Collective
		{
			get
			{
				return this.collective;
			}
			set
			{
				this.collective = value;
			}
		}

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��������</returns>
		public new HealthArchieve Clone()
		{
			HealthArchieve healthArchieve = base.Clone() as HealthArchieve;

			healthArchieve.CRMType = this.CRMType.Clone();
			healthArchieve.ArchieveType = this.ArchieveType.Clone();
			healthArchieve.Guest = this.Guest.Clone();
			healthArchieve.Collective = this.Collective.Clone();

			return healthArchieve;
		}

		#endregion
	}
}
