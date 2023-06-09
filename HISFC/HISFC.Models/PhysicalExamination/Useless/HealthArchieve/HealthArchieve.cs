using FS.HISFC.Models.Base;
using FS.HISFC.Models.PhysicalExamination.Base;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.PhysicalExamination.HealthArchieve
{
	/// <summary>
	/// HealthArchieves <br></br>
	/// [功能描述: 健康档案]<br></br>
	/// [创 建 者: 飞斯]<br></br>
	/// [创建时间: 2006-11-10]<br></br>
	/// <修改记录
	///		修改人=''
	///		修改时间=''
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
    [System.Serializable]
    public class HealthArchieve : FS.HISFC.Models.PhysicalExamination.Base.PE
	{
		#region 私有变量
		
		/// <summary>
		/// 客户关系类别
		/// </summary>
		private PE crmType = new PE();

		/// <summary>
		/// 健康档案类别
		/// </summary>
		private PE archieveType = new PE();

		/// <summary>
		/// 顾客的基本信息
		/// </summary>
		private FS.HISFC.Models.RADT.Patient guest = new Patient();

		/// <summary>
		/// 体检总次数
		/// </summary>
		private int totalCount;

		/// <summary>
		/// 体检总花费
		/// </summary>
		private decimal totalCost;

		/// <summary>
		/// 所属集体
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Collective.Collective collective = new Collective.Collective ();
		
		/// <summary>
		/// 性别
		/// </summary>
		private FS.HISFC.Models.Base.EnumSex sex = new EnumSex();

		#endregion

		#region 属性
		
		/// <summary>
		/// 性别
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
		/// 客户关系类别
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
		/// 健康档案类别
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
		/// 顾客的基本信息
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
		/// 体检总次数
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
		/// 体检总花费
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
		/// 所属集体
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

		#region 方法
		
		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns>健康档案</returns>
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
