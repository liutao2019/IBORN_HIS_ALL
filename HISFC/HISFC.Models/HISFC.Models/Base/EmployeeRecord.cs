using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// EmployeeRecord<br></br>
	/// [��������: ��Ա���ұ��ʵ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class EmployeeRecord: Record 
    {
		/// <summary>
		/// ���캯��
		/// </summary>
		public EmployeeRecord() 
        {
		}


		#region ����

		/// <summary>
		/// Ա����Ϣ ID Ա����� Name ����
		/// </summary>
		private NeuObject employee = new NeuObject();

		/// <summary>
		/// �䶯���ͣ�DEPT���ң�NURSE��ʿվ�ȣ�
		/// </summary>
		private NeuObject shiftType = new NeuObject();

		/// <summary>
		/// ��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
		/// </summary>
		private string state ;

		/// <summary>
		/// �������Ա
		/// </summary>
		private NeuObject applyOperator = new NeuObject();

		/// <summary>
		/// ����Ĳ���ʱ��
		/// </summary>
		private DateTime applyTime ;

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		#endregion
                           
		#region ����

		/// <summary>
		/// Ա����Ϣ ID Ա����� Name ����
		/// </summary>
		public NeuObject Employee 
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
		/// <summary>
		/// �䶯���ͣ�DEPT���ң�NURSE��ʿվ�ȣ�
		/// </summary>
		public NeuObject ShiftType 
		{
			get
			{ 
				return this.shiftType; 
			}
			set
			{ 
				this.shiftType = value; 
			}
		}
		/// <summary>
		/// ��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
		/// </summary>
		public string State 
		{
			get
			{ 
				return this.state; 
			}
			set
			{ 
				this.state = value;
			}
		}
		/// <summary>
		/// �������Ա
		/// </summary>
		public NeuObject ApplyOperator 
		{
			get
			{ 
				return this.applyOperator; 
			}
			set
			{ 
				this.applyOperator = value; 
			}
		}
		/// <summary>
		/// ����Ĳ���ʱ��
		/// </summary>
		public DateTime ApplyTime
		{
			get
			{ 
				return this.applyTime;
			}
			set
			{ 
				this.applyTime = value; 
			}
		}
		/// <summary>
		/// ����Ա����׼�����ϣ�
		/// </summary>
		public string ComfirmOperator 
		{
			get
			{ 
				return base.OperEnvironment.ID; 
			}
			set
			{ 
				 base.OperEnvironment.ID = value;
			}
		}
		/// <summary>
		/// ����ʱ�䣨��׼�����ϣ�
		/// </summary>
		public new System.DateTime OperDate 
		{
			get
			{ 
				return base.OperEnvironment.OperTime;
			}
			set
			{ 
				base.OperEnvironment.OperTime = value;
			}
		}

		#endregion
	
		#region ����

		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing"></param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}

			if (isDisposing)
			{
				if (this.applyOperator != null)
				{
					this.applyOperator.Dispose();
					this.applyOperator = null;
				}
				if (this.employee != null)
				{
					this.employee.Dispose();
					this.employee = null;
				}
				if (this.shiftType != null)
				{
					this.shiftType.Dispose();
					this.shiftType = null;
				}
			}

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}
 
		#endregion
	
		#region ��¡

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new EmployeeRecord Clone()
        {
            EmployeeRecord employeeRecord  = base.Clone() as EmployeeRecord;

            employeeRecord.Employee = this.Employee.Clone();
            employeeRecord.ShiftType = this.ShiftType.Clone();
			employeeRecord.ApplyOperator = this.ApplyOperator.Clone();

            return employeeRecord;
        }
		#endregion
	}
}
