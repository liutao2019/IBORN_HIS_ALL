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
		/// //סԺ��(�����)
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

        /// <summary>
        /// ִ�п���
        /// </summary>
        private string exeDept;
        
        /// <summary>
        /// pacs��Ŀ
        /// </summary>
        private string pacsItem;

        /// <summary>
        /// סԺ��ˮ�Ż�������ˮ��
        /// </summary>
        private string clinicCode;


        /// <summary>
        /// �����ɼ�����
        /// </summary>
        private string sampleDate;


        /// <summary>
        /// ĩ���¾�����
        /// </summary>
        private string  lastMensesDate;

        /// <summary>
        /// ����
        /// </summary>
        private bool isMenopause;

        /// <summary>
        /// ִ�е���
        /// </summary>
        private string exec_sqn;

        /// <summary>
        /// ������1
        /// </summary>
        private string antibiotic1;

        /// <summary>
        /// ������2
        /// </summary>
        private string antibiotic2;

        /// <summary>
        /// ����
        /// </summary>
        private string temperature;

        /// <summary>
        /// �걾����
        /// </summary>
        private string specimenType;
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
		/// //סԺ��(�����)
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


        /// <summary>
        /// ��ˮ��
        /// </summary>
        public string ClinicCode
        {
            get
            {
                return this.clinicCode;
            }
            set
            {
                this.clinicCode = value;
            }
        }


        /// <summary>
        /// �����ɼ�����
        /// </summary>
        public string SampleDate
        {
            get
            {
                return this.sampleDate;
            }
            set
            {
                this.sampleDate = value;
            }
        }
        /// <summary>
        /// ĩ���¾�����
        /// </summary>
        public string LastMensesDate
        {
            get
            {
                return this.lastMensesDate;
            }
            set
            {
                this.lastMensesDate = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public bool IsMenopause
        {
            get
            {
                return this.isMenopause;
            }
            set
            {
                this.isMenopause = value;
            }
        }
 
        /// <summary>
        /// ִ�е���
        /// </summary>
        public string Exec_sqn
        {
            get
            {
                return this.exec_sqn;
            }
            set
            {
                this.exec_sqn = value;
            }
        }
     
        /// <summary>
        /// ������1
        /// </summary>
        public string Antiviotic1
        {
            get
            {
                return this.antibiotic1;
            }
            set
            {
                this.antibiotic1 = value;
            }
        }

        /// <summary>
        /// ������2
        /// </summary>
        public string Antiviotic2
        {
            get
            {
                return this.antibiotic2;
            }
            set
            {
                this.antibiotic2 = value;
            }
        }
    
        /// <summary>
        /// ����
        /// </summary>
        public string Temperature
        {
            get
            {
                return this.temperature;
            }
            set
            {
                this.temperature = value;
            }
        }
     
        /// <summary>
        /// �걾����
        /// </summary>
        public string SpecimenType
        {
            get
            {
                return this.specimenType;
            }
            set
            {
                this.specimenType = value;
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


        /// <summary>
        /// ִ�п���
        /// </summary>
        public string ExeDept
        {
            get
            {
                return this.exeDept;
            }
            set
            {
                this.exeDept = value;
            }
        }


        /// <summary>
        /// PACS��Ŀ
        /// </summary>
        public string PacsItem
        {
            get
            {
                return this.pacsItem;
            }
            set
            {
                this.pacsItem = value;
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
