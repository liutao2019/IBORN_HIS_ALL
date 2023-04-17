using System;
using System.Collections;
using FS.HISFC;
namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.PacsBill<br></br>
	/// [��������: ������뵥ʵ��]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class PacsBill:FS.FrameWork.Models.NeuObject,Base.IValid
	{
		public PacsBill()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ����
		/// <summary>
		/// //����ʱ��
		/// </summary>
		private string applyDate;//����ʱ��
		/// <summary>
		/// //����Ա
		/// </summary>
		private string operatorID;
		/// <summary>
		/// //�������1
		/// </summary>
		private string diag1;
		/// <summary>
		/// //�������2
		/// </summary>
		private string diag2;
		/// <summary>
		/// //�������3
		/// </summary>
		private string diag3;
		/// <summary>
		/// //ע������
		/// </summary>
		private string caution;
		/// <summary>
		/// //ʵ���Ҽ����
		/// </summary>
		private string lisResult;
		/// <summary>
		/// //��ʷ������
		/// </summary>
		private string illnessHistory;
		/// <summary>
		/// //��鲿λ_Ŀ��
		/// </summary>
		private string checkOrder;
		/// <summary>
		/// //סԺ��ˮ��(������ˮ��)
		/// </summary>
		private string patientNO;
		/// <summary>
		/// //��鵥����
		/// </summary>
		private string billName;
		/// <summary>
		/// //��Ϻ�
		/// </summary>
		private string comboNO;
		/// <summary>
		/// �豸����
		/// </summary>
		private string machineType;
		/// <summary>
		/// ��鲿λ
		/// </summary>
		private string checkBody;
		/// <summary>
		/// �������
		/// </summary>
		private PatientType patientType;
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		private string itemCode;
		/// <summary>
		/// ��Ч״̬
		/// </summary>
		private bool validFlag;
		/// <summary>
		/// �ܽ��
		/// </summary>
		private decimal totCost;
		/// <summary>
		/// ����Ա
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
		/// <summary>
		/// ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ҽ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();

		#endregion

		#region ����
		
		/// <summary>
		/// //����ʱ��
		/// </summary>
		public  string ApplyDate 
		{
			get
			{
				return applyDate;
			}	 
			set 
			{
				applyDate = value;	 
			}
		}
		
		/// <summary>
		/// ����Ա
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept 
		{
			get
			{
				return this.dept;
			}
			set
			{
				this.dept = value;
			}
		}

		/// <summary>
		/// ���1
		/// </summary>
		public  string Diagnose1 
		{
			get 
			{
				return diag1;	 
			}
			set 
			{
				diag1 = value;	 
			}
		}
		/// <summary>
		/// //�������2
		/// </summary>
		public  string Diagnose2
		{
			get 
			{
				return diag2;	 
			}
			set 
			{
				diag2 = value;	 
			}
		}

		/// <summary>
		/// //�������3
		/// </summary>
		public  string Diagnose3
		{
			get 
			{
				return diag3;	 
			}
			set 
			{
				diag3 = value;	 
			}
		}
		
		/// <summary>
		/// //ע������
		/// </summary>
		public  string Caution 
		{
			get 
			{
				return caution;	 
			}
			set 
			{
				caution = value;	 
			}
		}

		/// <summary>
		/// //ʵ���Ҽ����
		/// </summary>
		public  string LisResult 
		{
			get 
			{
				return lisResult;	 
			}
			set 
			{
				lisResult = value;	 
			}
		}

		/// <summary>
		/// //��ʷ������
		/// </summary>
		public  string IllHistory 
		{
			get 
			{ 
				return illnessHistory;	 
			}
			set 
			{
				illnessHistory = value;	 
			}
		}

		/// <summary>
		/// //��鲿λ_Ŀ��
		/// </summary>
		public  string CheckOrder 
		{
			get 
			{
				return checkOrder;	 
			}
			set 
			{
				checkOrder = value;	 
			}
		}

		/// <summary>
		/// //סԺ��ˮ��(������ˮ��)
		/// </summary>
		public  string PatientNO 
		{
			get 
			{
				return patientNO;	 
			}
			set 
			{
				patientNO = value;	 
			}
		}

		/// <summary>
		/// //��鵥����
		/// </summary>
		public  string BillName 
		{
			get
			{
				return billName;
			}
			set 
			{
				billName = value;	 
			}
		}

		/// <summary>
		/// //��Ϻ�
		/// </summary>
		public  string ComboNO
		{
			get 
			{ 
				return comboNO;	 
			}
			set 
			{
				comboNO = value;	 
			}
		}

		/// <summary>
		/// �豸����
		/// </summary>
		public  string MachineType
		{
			get
			{
				return this.machineType;
			}
			set
			{
				this.machineType = value;
			}
		}

		/// <summary>
		/// ��鲿λ
		/// </summary>
		public  string CheckBody
		{
			get
			{
				return this.checkBody;
			}
			set
			{
				this.checkBody = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public  PatientType PatientType
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
		/// ��Ŀ����
		/// </summary>
		public  string ItemCode
		{
			get
			{
				return this.itemCode;
			}
			set
			{
				this.itemCode = value;
			}
		}

		/// <summary>
		/// ҽ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Doctor
		{
			get
			{
				return this.doctor;
			}
			set
			{
				this.doctor = value;
			}
		}
		
		/// <summary>
		/// �ܽ��
		/// </summary>
		public  decimal TotCost
		{
			get
			{
				return this.totCost;
			}
			set
			{
				this.totCost = value;
			}
		}

		#endregion

		#region ����

		/// <summary>
		/// //����Ա
		/// </summary>
		[Obsolete("��Oper.OperID����",true)]
		public  string OperID 
		{
			get 
			{
				return operatorID;	 
			}
			set 
			{
				operatorID = value;	 
			}
		}
		/// <summary>
		/// //�������1
		/// </summary>
		[Obsolete("��Diagnose1����",true)]
		public  string Diag1 
		{
			get 
			{
				return diag1;	 
			}
			set 
			{
				diag1 = value;	 
			}
		}
		/// <summary>
		/// //�������2
		/// </summary>
		[Obsolete("��Diagnose2����",true)]
		public  string Diag2 
		{
			get 
			{
				return diag2;	 
			}
			set 
			{
				diag2 = value;	 
			}
		}
		/// <summary>
		/// //�������3
		/// </summary>
		[Obsolete("��Diagnose3����",true)]
		public  string Diag3 
		{
			get 
			{
				return diag3;	 
			}
			set 
			{
				diag3 = value;	 
			}
		}
		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		[Obsolete("��Doctore����",true)]
		public FS.FrameWork.Models.NeuObject Doct = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ��Ч״̬
		/// </summary>
		[Obsolete("��IsValid����",true)]
		public  string ValidFlag
		{
			get
			{
				return "";
			}
			set
			{
				
			}
		}

		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new PacsBill Clone()
		{
			PacsBill pacsBill = this.MemberwiseClone() as PacsBill;
			pacsBill.Dept = this.Dept.Clone();
			pacsBill.doctor  = this.doctor.Clone();
			return pacsBill;
		}

		#endregion

		#endregion

		#region �����ֶΣ���Ϊͬ��������
		/// <summary>
		/// //����ʱ��
		/// </summary>
		private string oper_Date;//����ʱ��
		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("��Oper.OperTime����",true)]
		public  string Oper_Date 
		{
			get
			{
				return oper_Date;
			}	 
			set 
			{
				oper_Date = value;	 
			}
		}
		/// <summary>
		/// //�������
		/// </summary>
		private string diag_Name;
		/// <summary>
		/// �������
		/// </summary>
		public  string DiagName 
		{
			get 
			{
				return diag_Name;	 
			}
			set 
			{
				diag_Name = value;	 
			}
		}
		#endregion

		#region �ӿ�ʵ��

		#region IValid ��Ա
		/// <summary>
		/// �Ƿ����
		/// </summary>
		public bool IsValid
		{
			get
			{
				// TODO:  ��� PacsBill.IsValid getter ʵ��
				return this.validFlag;
			}
			set
			{
				// TODO:  ��� PacsBill.IsValid setter ʵ��
				this.validFlag = value;
			}
		}

		#endregion

		#endregion
	}

	    #region ö��
		/// <summary>
		/// �������
		/// </summary>
		public enum PatientType
		{
			InPatient = 0,//סԺ
			OutPatient = 1//����
		}
		#endregion
}
