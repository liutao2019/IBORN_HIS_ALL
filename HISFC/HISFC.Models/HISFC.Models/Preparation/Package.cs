using System;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// Package<br></br>
	/// [��������: ��װʵ��]<br></br>
	/// [�� �� ��: ]<br></br>
	/// [����ʱ��: 2006-09-14]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Package:PPRBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Package()
		{
		}


		#region  ����
		/// <summary>
		/// ��װ��Ʒ����
		/// </summary>
		private decimal divisionQty;
		/// <summary>
		/// ���װ����(�ƽ������)
		/// </summary>
		private decimal packingQty;
		/// <summary>
		/// ��Ʒ��
		/// </summary>
		private decimal wasterQty;
		/// <summary>
		/// ���Ա
		/// </summary>
		private string checkOper;
		/// <summary>
		/// ����ƽ�������
		/// </summary>
		private string inceptOper;

		private string packingOper;
		private DateTime packingDate;

		/// <summary>
		/// ����ƽ��
		/// </summary>
		private decimal pacParam;
		/// <summary>
		/// ��Ʒ��
		/// </summary>
		private decimal finParam;
		/// <summary>
		/// ���װ--�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment packingEnv = new FS.HISFC.Models.Base.OperEnvironment();
		#endregion

		#region  ����
		/// <summary>
		/// ���װ--�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment PackingEnv
		{
			get
			{
				return this.packingEnv;
			}
			set
			{
				this.packingEnv = value;
			}
		}
		/// <summary>
		/// ��װ��Ʒ����
		/// </summary>
		public decimal DivisionQty
		{
			get
			{
				return this.divisionQty;
			}
			set
			{
				this.divisionQty = value;
			}
		}

		/// <summary>
		/// ���װ����(�ƽ������)
		/// </summary>
		public decimal PackingQty
		{
			get
			{
				return this.packingQty;
			}
			set
			{
				this.packingQty = value;
			}
		}

		/// <summary>
		/// ��Ʒ��
		/// </summary>
		public decimal WasterQty
		{
			get
			{
				return this.wasterQty;
			}
			set
			{
				this.wasterQty = value;
			}
		}
		/// <summary>
		/// ����ƽ��
		/// </summary>
		public decimal PacParam
		{
			get
			{
				return this.pacParam;
			}
			set
			{
				this.pacParam = value;
			}
		}
		/// <summary>
		/// ��Ʒ��
		/// </summary>
		public decimal FinParam
		{
			get
			{
				return this.finParam;
			}
			set
			{
				this.finParam = value;
			}
		}

		/// <summary>
		/// ���Ա
		/// </summary>
		public string CheckOper
		{
			get
			{
				return this.checkOper;
			}
			set
			{
				this.checkOper = value;
			}
		}
		/// <summary>
		/// ����ƽ�������
		/// </summary>
		public string InceptOper
		{
			get
			{
				return this.inceptOper;
			}
			set
			{
				this.inceptOper = value;
			}
		}

		#endregion

		#region ����
		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>Package</returns>
		public new Package Clone()
		{
			Package package = base.Clone() as Package;
			package.packingEnv = this.packingEnv.Clone();
			return package;
		}
		#endregion
		
		#region  ��������
		/// <summary>
		/// ��װ��Ʒ����
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��DivisionQty", true)]
		public decimal DivisionNum
		{
			get
			{
				return this.divisionQty;
			}
			set
			{
				this.divisionQty = value;
			}
		}

		/// <summary>
		/// ���װ����(�ƽ������)
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PackingQty", true)]
		public decimal PackingNum
		{
			get
			{
				return this.packingQty;
			}
			set
			{
				this.packingQty = value;
			}
		}

		/// <summary>
		/// ��Ʒ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��WasterQty", true)]
		public decimal WasterNum
		{
			get
			{
				return this.wasterQty;
			}
			set
			{
				this.wasterQty = value;
			}
		}

		/// <summary>
		/// ���װ����Ա
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PackingEnv", true)]
		public string PackingOper
		{
			get
			{
				return this.packingOper;
			}
			set
			{
				this.packingOper = value;
			}
		}
		/// <summary>
		/// ���װ����ʱ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PackingEnv", true)]
		public DateTime PackingDate
		{
			get
			{
				return this.packingDate;
			}
			set
			{
				this.packingDate = value;
			}
		}
		#endregion
	}
}