using System;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// Division<br></br>
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
    public class Division:PPRBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Division()
		{
		}
		

		#region ����
		/// <summary>
		/// ��װ����
		/// </summary>
		private decimal divisionQty;
		/// <summary>
		/// ��װ��Ʒ����
		/// </summary>
		private decimal wasterQty;
		/// <summary>
		/// ��װ�ʿز��� ����ƽ��
		/// </summary>
		private decimal divisionParam;
//		/// <summary>
//		/// ��װ��
//		/// </summary>
//		private string divisionOper;
//		/// <summary>
//		/// ��װʱ��
//		/// </summary>
//		private DateTime divisionDate;
		/// <summary>
		/// ��װ--�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment divisionEnv = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��װ��Ʒ�ƽ�������
		/// </summary>
		private string inceptOper;
		#endregion

		#region ����
		/// <summary>
		/// ��װ����
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
		/// ��װ��Ʒ����
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
		/// ��װ�ʿز��� ����ƽ��
		/// </summary>
		public decimal DivisionParam
		{
			get
			{
				return this.divisionParam;
			}
			set
			{
				this.divisionParam = value;
			}
		}

		/// <summary>
		/// ��װ--�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment DivisionEnv
		{
			get
			{
				return this.divisionEnv;
			}
			set
			{
				this.divisionEnv = value;
			}
		}
		
		/// <summary>
		/// ��װ��Ʒ�ƽ�������
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
		/// <returns>Division</returns>
		public new Division Clone()
		{
			Division division = base.Clone() as Division;
			division.divisionEnv = this.divisionEnv.Clone();
			return division;
		}
		#endregion

		#region ��������
		/// <summary>
		/// ��װ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��DivisionEnv", true)]
		public string DivisionOper
		{
			get
			{
				return null;
			}
			set
			{
				//this.divisionOper = value;
			}
		}
		/// <summary>
		/// ��װʱ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��DivisionEnv", true)]
		public DateTime DivisionDate
		{
			get
			{
				return DateTime.Now;
			}
			set
			{
				//this.divisionDate = value;
			}
		}
		/// <summary>
		/// ��װ����
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
		/// ��װ��Ʒ����
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
		#endregion
	}
}