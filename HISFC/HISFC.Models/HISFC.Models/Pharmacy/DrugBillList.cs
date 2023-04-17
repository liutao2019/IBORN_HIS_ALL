using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ��ҩ����ϸʵ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� '
	///  />
	/// </summary>
    [Serializable]
    public class DrugBillList :  FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ��ҩ��������ϸʵ��
		/// </summary>
		public DrugBillList() 
		{
			
		}


		#region ����

		/// <summary>
		/// ��ҩ������
		/// </summary>
		private FS.HISFC.Models.Pharmacy.DrugBillClass myDrugBillClass = new FS.HISFC.Models.Pharmacy.DrugBillClass();

		/// <summary>
		/// ҽ�����
		/// </summary>
		private FS.HISFC.Models.Order.OrderType orderType = new FS.HISFC.Models.Order.OrderType();
	
		/// <summary>
		/// �÷�
		/// </summary>
		private FS.FrameWork.Models.NeuObject usage = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ҩƷ���
		/// </summary>
		private FS.FrameWork.Models.NeuObject drugType = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ҩƷ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject drugQuality = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject dosageForm = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ҽ��״̬ 1����/2��ʱ/3ȫ��
		/// </summary>
		private string orderState;

		#endregion

		/// <summary>
		/// ��ҩ������
		/// </summary>
		public FS.HISFC.Models.Pharmacy.DrugBillClass DrugBillClass
		{
			get
			{ 
				return this.myDrugBillClass;
			}
			set
			{
				this.myDrugBillClass = value;
			}
		}


		/// <summary>
		/// ҽ�����
		/// </summary>
		public FS.HISFC.Models.Order.OrderType OrderType
		{
			get
			{
				return this.orderType;
			}
			set
			{
				this.orderType = value;
			}
		}


		/// <summary>
		/// �÷�
		/// </summary>
		public FS.FrameWork.Models.NeuObject Usage
		{
			get
			{
				return this.usage;
			}
			set
			{
				this.usage = value;
			}
		}


		/// <summary>
		/// ҩƷ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject DrugType
		{
			get
			{
				return this.drugType;
			}
			set
			{
				this.drugType = value;
			}
		}


		/// <summary>
		/// ҩƷ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject DrugQuality
		{
			get
			{
				return this.drugQuality;
			}
			set
			{
				this.drugQuality = value;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject DosageForm
		{
			get
			{
				return this.dosageForm;
			}
			set
			{
				this.dosageForm = value;
			}
		}


		/// <summary>
		/// ҽ��״̬ 1����/2��ʱ/3ȫ��
		/// </summary>
		public string OrderState
		{
			get
			{
				return this.orderState;
			}
			set
			{
				this.orderState = value;
			}
		}


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new DrugBillList Clone()
		{
			DrugBillList drugBillList = base.Clone() as DrugBillList;

			drugBillList.DrugBillClass = this.DrugBillClass.Clone();
			drugBillList.OrderType = this.OrderType.Clone();
			drugBillList.Usage = this.Usage.Clone();
			drugBillList.DrugType = this.DrugType.Clone();
			drugBillList.DrugQuality = this.DrugQuality.Clone();
			drugBillList.DosageForm = this.DosageForm.Clone();

			return drugBillList;

		}


		#endregion

		#region ��Ч����

		private System.String myTypeCode ;
		private System.String myUsageCode ;
		private System.String myDoseModelCode ;
		private System.String myDoseModelName ;
		private System.String myIpmState ;

		/// <summary>
		/// ҽ�����
		/// </summary>
		[System.Obsolete("�������� ����ΪOrderType���͵�OrderType����",true)]
		public System.String TypeCode
		{
			get{ return this.myTypeCode; }
			set{ this.myTypeCode = value; }
		}


		/// <summary>
		/// �÷�����
		/// </summary>
		[System.Obsolete("�������� ����ΪNeuobject���͵�Usage����",true)]
		public System.String UsageCode
		{
			get{ return this.myUsageCode; }
			set{ this.myUsageCode = value; }
		}


		/// <summary>
		/// ���ʹ���
		/// </summary>
		[System.Obsolete("�������� ����ΪNeuobject���͵�DosageForm����",true)]
		public System.String DosageFormCode
		{
			get{ return this.myDoseModelCode; }
			set{ this.myDoseModelCode = value; }
		}


		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�������� ����ΪNeuobject���͵�DosageForm����",true)]
		public System.String DoseModelName
		{
			get{ return this.myDoseModelName; }
			set{ this.myDoseModelName = value; }
		}


		/// <summary>
		/// ҽ��״̬1����/2��ʱ/3ȫ��
		/// </summary>
		[System.Obsolete("�������� ����ΪorderState����",true)]
		public System.String IpmState
		{
			get{ return this.myIpmState; }
			set{ this.myIpmState = value; }
		}


		#endregion

	}
}
