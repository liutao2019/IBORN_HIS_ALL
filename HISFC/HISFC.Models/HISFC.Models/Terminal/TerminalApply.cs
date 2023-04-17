using System;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Registration;
using FS.FrameWork.Models;


namespace FS.HISFC.Models.Terminal
{
	/// <summary>
	/// TerminalApply <br></br>
	/// [��������: ҽ���ն�ȷ�����뵥]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2007-3-1]<br></br>
	/// [ID�����뵥��ˮ��]
	/// [user01����չ��־1]
	/// [user02����չ��־2]
	/// [user03����ע]
    /// <˵��>
    ///     1��  {F8383442-78B0-40c2-B906-50BA52ADB139}  ����ʵ������ ִ����
    /// </˵��>
	/// </summary>
    [Serializable]
    public class TerminalApply : FS.FrameWork.Models.NeuObject,IValid
	{
		public TerminalApply()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		FS.HISFC.Models.Fee.Outpatient.FeeItemList item = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

		/// <summary>
		/// ����/�˿ͻ�����Ϣ
		/// </summary>
		FS.HISFC.Models.Registration.Register patient = new Register();

		/// <summary>
		/// ҽ���豸��Ϣ
		/// </summary>
		FS.FrameWork.Models.NeuObject machine = new NeuObject();

        /// <summary>
        /// ִ����
        /// </summary>
        FS.HISFC.Models.Base.Employee execOper = new Employee();
		
		/// <summary>
		/// �������뵥��������
		/// </summary>
		FS.HISFC.Models.Base.OperEnvironment insertOperEnvironment = new OperEnvironment();

		/// <summary>
		/// �ն�ִ�в�������
		/// </summary>
		FS.HISFC.Models.Base.OperEnvironment confirmOperEnvironment = new OperEnvironment();

		/// <summary>
		/// �Ѿ�ȷ������
		/// </summary>
		decimal alreadyConfirmCount = 0;

		/// <summary>
		/// ��ҩ������Ϣ
		/// </summary>
		FS.FrameWork.Models.NeuObject drugDepartment = new NeuObject();

		/// <summary>
		/// ���¿�����ˮ��
		/// </summary>
		int updateStoreSequence = 0;

		/// <summary>
		/// ҩƷ����ʱ��
		/// </summary>
		DateTime sendDrugTime = DateTime.MinValue;

		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		FS.HISFC.Models.Order.Order order = new Order.Order();

		/// <summary>
		/// ҽ��ִ�е���ˮ��
		/// </summary>
		int orderExeSequence = 0;

		/// <summary>
		/// ��Ŀ״̬��0 ����  1 ���� 2 ִ�У�ҩƷ���ţ���
		/// </summary>
		string itemStatus = "";

		/// <summary>
		/// �¾���Ŀ��־��0-����Ŀ/1-����Ŀ
		/// </summary>
		string newOrOld = "";

        string specalFlag = string.Empty;
		/// <summary>
		/// �������1-���2-סԺ��3-���4-���
		/// </summary>
		string patientType = "";
        private bool isValid = true;
		#endregion

		#region ����
        /// <summary>
        /// ������  1 �������˻�  2 סԺ�۷���
        /// </summary>
        public string SpecalFlag
        {
            get
            {
                return specalFlag;
            }
            set
            {
                specalFlag = value;
            }
        }
		/// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		public FS.HISFC.Models.Fee.Outpatient.FeeItemList Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		/// <summary>
		/// ����/�˿���Ϣ
		/// </summary>
		public FS.HISFC.Models.Registration.Register Patient
		{
			get
			{
				return this.patient;
			}
			set
			{
				this.patient = value;
			}
		}

		/// <summary>
		/// ҽ���豸��Ϣ
		/// </summary>
		public FS.FrameWork.Models.NeuObject Machine
		{
			get
			{
				return this.machine;
			}
			set
			{
				this.machine = value;
			}
		}

		/// <summary>
		/// �������뵥��������
		/// </summary>
		public Base.OperEnvironment InsertOperEnvironment
		{
			get
			{
				return this.insertOperEnvironment;
			}
			set
			{
				this.insertOperEnvironment = value;
			}
		}
		
		/// <summary>
		/// �ն�ִ�в�������
		/// </summary>
		public Base.OperEnvironment ConfirmOperEnvironment
		{
			get
			{
				return this.confirmOperEnvironment;
			}
			set
			{
				this.confirmOperEnvironment = value;
			}
		}

		/// <summary>
		/// �Ѿ�ȷ������
		/// </summary>
		public decimal AlreadyConfirmCount
		{
			get
			{
				return this.alreadyConfirmCount;
			}
			set
			{
				this.alreadyConfirmCount = value;
			}
		}

		/// <summary>
		/// ��ҩ������Ϣ
		/// </summary>
		public FS.FrameWork.Models.NeuObject DrugDepartment
		{
			get
			{
				return this.drugDepartment;
			}
			set
			{
				this.drugDepartment = value;
			}
		}

		/// <summary>
		/// ���¿�����ˮ��
		/// </summary>
		public int UpdateStoreSequence
		{
			get
			{
				return this.updateStoreSequence;
			}
			set
			{
				this.updateStoreSequence = value;
			}
		}

		/// <summary>
		/// ҩƷ����ʱ��
		/// </summary>
		public DateTime SendDrugDate
		{
			get
			{
				return this.sendDrugTime;
			}
			set
			{
				this.sendDrugTime = value;
			}
		}

		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		public FS.HISFC.Models.Order.Order Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		/// <summary>
		/// ҽ��ִ�е���ˮ��
		/// </summary>
		public int OrderExeSequence
		{
			get
			{
				return this.orderExeSequence;
			}
			set
			{
				this.orderExeSequence = value;
			}
		}

		/// <summary>
		/// ��Ŀ״̬��0 ����  1 ���� 2 ִ�У�ҩƷ���ţ���
		/// </summary>
		public string ItemStatus
		{
			get
			{
				return this.itemStatus;
			}
			set
			{
				this.itemStatus = value;
			}
		}

		/// <summary>
		/// �¾���Ŀ��־��0-����Ŀ/1-����Ŀ
		/// </summary>
		public string NewOrOld
		{
			get
			{
				return this.newOrOld;
			}
			set
			{
				this.newOrOld = value;
			}
		}

		/// <summary>
		/// �������1-���2-סԺ��3-���4-�������  5 ���� 
		/// </summary>
		public string PatientType
		{
			get
			{
				return this.patientType;
			}
			set
			{
				this.patientType = value;
			}
		}

        /// <summary>
        /// ִ����
        /// </summary>
        public FS.HISFC.Models.Base.Employee ExecOper
        {
            get
            {
                return execOper;
            }
            set
            {
                execOper = value;
            }
        }
		#endregion

		#region ��ʱ

		/// <summary>
		/// �������뵥����Ϣ
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪinsertOperEnvironment", true)]
		FS.FrameWork.Models.NeuObject insertOperator = new NeuObject();

		/// <summary>
		/// �������뵥ʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪinsertOperEnvironment", true)]
		DateTime insertTime = DateTime.MinValue;

		/// <summary>
		/// �ն�ִ������Ϣ
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪconfirmOperEnvironment", true)]
		FS.FrameWork.Models.NeuObject confirmOperator = new NeuObject();

		/// <summary>
		/// �ն�ִ��ʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪconfirmOperEnvironment", true)]
		DateTime confirmDate = DateTime.MinValue;

		/// <summary>
		/// �������뵥����Ϣ
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪInsertOperEnvironment", true)]
		public FS.FrameWork.Models.NeuObject InsertOperator
		{
			get
			{
				return this.insertOperEnvironment;
			}
			set
			{
				this.insertOperEnvironment.Dept = value;
			}
		}

		/// <summary>
		/// �������뵥ʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪInsertOperEnvironment", true)]
		public DateTime InsertDate
		{
			get
			{
				return this.insertOperEnvironment.OperTime;
			}
			set
			{
				this.insertOperEnvironment.OperTime = value;
			}
		}

		/// <summary>
		/// �ն�ִ������Ϣ
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪConfirmOperEnvironment", true)]
		public FS.FrameWork.Models.NeuObject ConfirmOperator
		{
			get
			{
				return this.confirmOperEnvironment.Dept;
			}
			set
			{
				this.confirmOperEnvironment.Dept = value;
			}
		}

		/// <summary>
		/// �ն�ִ��ʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪConfirmOperEnvrionment", true)]
		public DateTime ConfirmDate
		{
			get
			{
				return this.confirmOperEnvironment.OperTime;
			}
			set
			{
				this.confirmOperEnvironment.OperTime = value;
			}
		}
		
		#endregion

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>�ն�ȷ�����뵥</returns>
		public new TerminalApply Clone()
		{
			TerminalApply terminalApply = base.Clone() as TerminalApply;
			
			terminalApply.Item = this.Item.Clone();
			terminalApply.Patient = this.Patient.Clone();
			terminalApply.Machine = this.Machine.Clone();
			terminalApply.InsertOperEnvironment = this.InsertOperEnvironment.Clone();
			terminalApply.ConfirmOperEnvironment = this.ConfirmOperEnvironment.Clone();
			terminalApply.DrugDepartment = this.DrugDepartment.Clone();
			terminalApply.Order = this.Order.Clone();
            terminalApply.ExecOper = this.ExecOper.Clone();
			
			return terminalApply;
		}
		#endregion

        #region IValid ��Ա

        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        #endregion
    }
	
	/// <summary>
	/// סԺ�շѷ�ʽ
	/// </summary>
	public enum InpatientChargeType
	{
		/// <summary>
		/// ֱ�����ѷ�ʽ0
		/// </summary>
		DirectChargeType = 0,
		/// <summary>
		/// ҽ���շѷ�ʽ1
		/// </summary>
		OrderChargeType = 1,
		/// <summary>
		/// �����շѷ�ʽ9
		/// </summary>
		OtherChargeType = 9,
	}
    /// <summary>
    /// �������˻���������
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Ĭ��
        /// </summary>
        None,
        /// <summary>
        /// �ն˿��˻��շ���ִ���ն�ȷ��
        /// </summary>
        ClinicFee,
        /// <summary>
        /// ����
        /// </summary>
        ClinicCharge
    }
}

