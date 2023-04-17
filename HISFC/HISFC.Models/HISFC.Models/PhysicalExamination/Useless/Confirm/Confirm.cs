using System;

namespace FS.HISFC.Models.PhysicalExamination.Confirm
{
	/// <summary>
	/// Confirm <br></br>
	/// [��������: �����Ŀִ����Ϣ]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Confirm : FS.HISFC.Models.PhysicalExamination.Base.PE
	{

		#region ˽�б���
		
		/// <summary>
		/// ���Ǽǵļ����Ŀ
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.Item regItem;

		/// <summary>
		/// ����ʱִ�п���
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.Department execDept;

		/// <summary>
		/// ʵ��ִ�п���
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.Department confirmDept;

		/// <summary>
		/// ִ����
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Management.PEUser confirmOper;

		/// <summary>
		/// ִ��ʱ��
		/// </summary>
		private DateTime confirmTime;

		/// <summary>
		/// ��Ŀ���
		/// </summary>
		private string itemResult;

		#endregion

		#region ����

		/// <summary>
		/// ���Ǽǵļ����Ŀ
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Management.Item RegItem 
		{
			get 
			{
				return this.regItem;
			}
			set 
			{
				this.regItem = value;
			}
		}

		/// <summary>
		/// ����ʱִ�п���
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Management.Department ExecDept
		{
			get
			{
				return this.execDept;
			}
			set
			{
				this.execDept = value;
			}
		}

		/// <summary>
		/// ʵ��ִ�п���
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Management.Department ConfirmDept
		{
			get
			{
				return this.confirmDept;
			}
			set
			{
				this.confirmDept = value;
			}
		}

		/// <summary>
		/// ִ����
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Management.PEUser ConfirmOper
		{
			get
			{
				return this.confirmOper;
			}
			set
			{
				this.confirmOper = value;
			}
		}

		/// <summary>
		/// ִ��ʱ��
		/// </summary>
		public DateTime ConfirmTime
		{
			get
			{
				return this.confirmTime;
			}
			set
			{
				this.confirmTime = value;
			}
		}

		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public string ItemResult
		{
			get
			{
				return this.itemResult;
			}
			set
			{
				this.itemResult = value;
			}
		}
		
		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>�����Ŀִ����Ϣ</returns>
		public new Confirm Clone()
		{
			Confirm confirm = base.Clone() as Confirm;

			confirm.RegItem = this.RegItem.Clone();
			confirm.ExecDept = this.ExecDept.Clone();
			confirm.ConfirmDept = this.ConfirmDept.Clone();
			confirm.ConfirmOper = this.ConfirmOper.Clone();

			return confirm;
		}
		#endregion
	}
}
