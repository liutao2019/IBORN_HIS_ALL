/*----------------------------------------------------------------
            // Copyright (C) ������������ɷ����޹�˾
            // ��Ȩ���С� 
            //
            // �ļ�����			TerminalApply.cs
            // �ļ�����������	ҽ���ն�ȷ�����뵥ʵ�����
            //
            // 
            // ������ʶ��		2006-3-17
            //
            // �޸ı�ʶ��
            // �޸�������
            //
            // �޸ı�ʶ��
            // �޸�������
//----------------------------------------------------------------*/
using System;
using neusoft.HISFC.Object.Fee.OutPatient;
using neusoft.HISFC.Object.Registration;
using neusoft.neuFC.Object;

namespace neusoft.HISFC.Object.MedTech
{
	/// <summary>
	/// ҽ���ն�ȷ�����뵥
	/// [ID�����뵥��ˮ��]
	/// [user01����չ��־1]
	/// [user02����չ��־2]
	/// [user03����ע]
	/// </summary>
	public class TerminalApply:neusoft.neuFC.Object.neuObject
	{

		#region ���캯��
		public TerminalApply()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion
		
		#region ����

		/// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		neusoft.HISFC.Object.Fee.OutPatient.FeeItemList item = new FeeItemList();

		/// <summary>
		/// ����/�˿ͻ�����Ϣ
		/// </summary>
		neusoft.HISFC.Object.Registration.Register patient = new Register();

		/// <summary>
		/// ҽ���豸��Ϣ
		/// </summary>
		neusoft.neuFC.Object.neuObject machine = new neuObject();

		/// <summary>
		/// �������뵥����Ϣ
		/// </summary>
		neusoft.neuFC.Object.neuObject insertOperator = new neuObject();

		/// <summary>
		/// �������뵥ʱ��
		/// </summary>
		DateTime insertDate = DateTime.MinValue;

		/// <summary>
		/// �ն�ִ������Ϣ
		/// </summary>
		neusoft.neuFC.Object.neuObject confirmOperator = new neuObject();

		/// <summary>
		/// �ն�ִ��ʱ��
		/// </summary>
		DateTime confirmDate = DateTime.MinValue;

		/// <summary>
		/// �Ѿ�ȷ������
		/// </summary>
		decimal alreadyConfirmCount = 0;

		/// <summary>
		/// ��ҩ������Ϣ
		/// </summary>
		neusoft.neuFC.Object.neuObject drugDepartment = new neuObject();

		/// <summary>
		/// ���¿�����ˮ��
		/// </summary>
		int updateStoreSequence = 0;

		/// <summary>
		/// ҩƷ����ʱ��
		/// </summary>
		DateTime sendDrugDate = DateTime.MinValue;

		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		neusoft.HISFC.Object.Order.Order order = new Order.Order();

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

		/// <summary>
		/// �������1-���2-סԺ��3-���4-���
		/// </summary>
		string patientType = "";

		#endregion

		#region ����

		#region ��Ŀ��Ϣ
		/// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		public neusoft.HISFC.Object.Fee.OutPatient.FeeItemList Item
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
		#endregion

		#region ����/�˿���Ϣ
		/// <summary>
		/// ����/�˿���Ϣ
		/// </summary>
		public neusoft.HISFC.Object.Registration.Register Patient
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
		#endregion

		#region ҽ���豸��Ϣ
		/// <summary>
		/// ҽ���豸��Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject Machine
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
		#endregion

		#region �������뵥����Ϣ
		/// <summary>
		/// �������뵥����Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject InsertOperator
		{
			get
			{
				return this.insertOperator;
			}
			set
			{
				this.insertOperator = value;
			}
		}
		#endregion

		#region �������뵥ʱ��
		/// <summary>
		/// �������뵥ʱ��
		/// </summary>
		public DateTime InsertDate
		{
			get
			{
				return this.insertDate;
			}
			set
			{
				this.insertDate = value;
			}
		}
		#endregion

		#region �ն�ִ������Ϣ
		/// <summary>
		/// �ն�ִ������Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject ConfirmOperator
		{
			get
			{
				return this.confirmOperator;
			}
			set
			{
				this.confirmOperator = value;
			}
		}
		#endregion

		#region �ն�ִ��ʱ��
		/// <summary>
		/// �ն�ִ��ʱ��
		/// </summary>
		public DateTime ConfirmDate
		{
			get
			{
				return this.confirmDate;
			}
			set
			{
				this.confirmDate = value;
			}
		}
		#endregion

		#region �Ѿ�ȷ������
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
		#endregion

		#region ��ҩ������Ϣ
		/// <summary>
		/// ��ҩ������Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject DrugDepartment
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
		#endregion

		#region ���¿�����ˮ��
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
		#endregion

		#region ҩƷ����ʱ��
		/// <summary>
		/// ҩƷ����ʱ��
		/// </summary>
		public DateTime SendDrugDate
		{
			get
			{
				return this.sendDrugDate;
			}
			set
			{
				this.sendDrugDate = value;
			}
		}
		#endregion

		#region ҽ����Ϣ
		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		public neusoft.HISFC.Object.Order.Order Order
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
		#endregion

		#region ҽ��ִ�е���ˮ��
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
		#endregion

		#region ��Ŀ״̬��0 ����  1 ���� 2 ִ�У�ҩƷ���ţ���
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
		#endregion

		#region �¾���Ŀ��־��0-����Ŀ/1-����Ŀ
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
		#endregion

		#region �������1-���2-סԺ��3-���4-���
		
		/// <summary>
		/// �������1-���2-סԺ��3-���4-���
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
		#endregion

		#endregion
		
		#region ��¡
		public new TerminalApply Clone()
		{
			TerminalApply terminalApply = base.Clone() as TerminalApply;
			terminalApply.Item = this.Item.Clone();
			terminalApply.Patient = (neusoft.HISFC.Object.Registration.Register)this.Patient.Clone();
			terminalApply.Machine = this.Machine.Clone();
			terminalApply.ConfirmOperator = this.ConfirmOperator.Clone();
			terminalApply.DrugDepartment = this.DrugDepartment.Clone();
			terminalApply.Order = this.Order.Clone();
			terminalApply.InsertOperator = this.InsertOperator.Clone();
			return terminalApply;
		}
		#endregion
	}
}

