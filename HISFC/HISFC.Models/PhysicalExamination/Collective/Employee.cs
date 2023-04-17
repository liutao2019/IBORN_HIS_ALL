namespace Neusoft.HISFC.Object.PhysicalExamination.Collective
{
	/// <summary>
	/// Employee <br></br>
	/// [��������: ������Աʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Employee : Neusoft.HISFC.Object.PhysicalExamination.Base.PE
	{
		#region ˽�б���

		/// <summary>
		/// ��Ա���
		/// </summary>
		private EmployeeType employeeType;

		/// <summary>
		/// ��������
		/// </summary>
		private Collective collective;

		#endregion

		#region ����

		/// <summary>
		/// ��Ա���
		/// </summary>
		public EmployeeType EmployeeType 
		{
			get 
			{
				return this.employeeType;
			}
			set 
			{
				this.employeeType = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public Collective Collective
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
		/// <returns></returns>
		public new Employee Clone()
		{
			Employee employee = base.Clone() as Employee;

			employee.EmployeeType = this.EmployeeType.Clone();
			employee.Collective = this.Collective.Clone();

			return employee;
		}

		#endregion
	}
}
