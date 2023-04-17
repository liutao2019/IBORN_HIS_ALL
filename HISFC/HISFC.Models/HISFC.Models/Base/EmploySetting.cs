namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// EmploySetting<br></br>
	/// [��������: ��Ա������Ϣʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-30]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class EmploySetting : FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public EmploySetting()
		{
		}


		#region ����

		/// <summary>
		/// �ȴ�ʱ��
		/// </summary>
		private decimal waitTime ;

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ��Ա
		/// </summary>
		private FS.HISFC.Models.Base.Employee employee = new Employee();

		#endregion

		#region ����

		/// <summary>
		/// �ȴ�ʱ��
		/// </summary>
		public decimal WaitTime
		{
			get
			{
				return waitTime;
			}
			set
			{
				waitTime = value;
			}
		}
		/// <summary>
		/// ��Ա
		/// </summary>
		public FS.HISFC.Models.Base.Employee Employee
		{
			get
			{
				return this.employee;
			}
			set
			{
				this.employee = value;
			}
		}
		#endregion
		
		#region ����
		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing">�Ƿ��ͷ���Դ</param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}

			if (this.employee != null)
			{
				this.employee.Dispose();
				this.employee = null;
			}
			
			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ�����ʵ������</returns>
		public new EmploySetting Clone()
		{
			EmploySetting employeeSetting = base.Clone() as EmploySetting;

			employeeSetting.Employee = this.Employee.Clone();

			return employeeSetting;
		}

		#endregion
	}
}
