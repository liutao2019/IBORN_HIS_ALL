using System;

namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.Consultation<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����'
	///		�޸�ʱ��='2007-8-23'
	///		�޸�Ŀ��='�������ܷ���ҽ������'
    ///		�޸�����='������Ƿ��ܿ���ҽ�����Ժͷ���'
	///  />
	/// </summary>
    [Serializable]
    public class Consultation:FS.FrameWork.Models.NeuObject 
	{
		public Consultation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// סԺ��
		/// </summary>
        private string patientNo = "";

        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        private string inpatientNo = "";
		
		/// <summary>
		/// ������
		/// </summary>
		private string bedNO ="";

		/// <summary>
		/// �������
		/// </summary>
		private FS.FrameWork.Models.NeuObject dept= new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���벡��
		/// </summary>
		private FS.FrameWork.Models.NeuObject nurseStation = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����ҽ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��������
		/// </summary>
		private DateTime applyTime ;

		/// <summary>
		/// ԤԼ��������
		/// </summary>
		private DateTime preConsultationTime;

		/// <summary>
		/// ��������
		/// </summary>
		private DateTime consultationTime;

		/// <summary>
		/// ����ҽԺ
		/// </summary>
		private FS.FrameWork.Models.NeuObject hosConsultation= new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// �������
		/// </summary>
		private FS.FrameWork.Models.NeuObject deptConsultation= new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����ҽ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctorConsultation = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// �Ƿ�Ӽ�
		/// </summary>
		private bool isEmergency = false;

        /// <summary>
        /// �Ƿ��п���ҽ��Ȩ��
        /// </summary>
        private bool isCreateOrder = false;

		/// <summary>
		/// ����˵��
		/// </summary>
		private string emergencyMemo = "";

		/// <summary>
		/// �������������
		/// </summary>
		private string suggestion = "";

		/// <summary>
		/// �����¼
		/// </summary>
		private string record = "";

		/// <summary>
		/// ҽ����Ȩ��ʼ����
		/// </summary>
		private DateTime beginTime;

		/// <summary>
		/// ҽ����Ȩ��������
		/// </summary>
		private DateTime endTime;

		/// <summary>
		/// ���
		/// </summary>
		private string result;

		/// <summary>
		/// ȷ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctorConfirm=new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����״̬1 ����, 2ȷ��
		/// </summary>
		private int state = 1;//1 ����, 2ȷ��

		#endregion

		#region ���ϵ�

		[Obsolete("����������ApplyTime����",true)]
		public DateTime DateApply ;

		[Obsolete("ԤԼ����������PreConsultationTime����",true)]
		public DateTime DatePreConsultation;

		[Obsolete("����������ConsultationTime����",true)]
		public DateTime DateConsultation;

		[Obsolete("��BeginTime����",true)]
		public DateTime DateBegin;

		[Obsolete("��EndTime����",true)]
		public DateTime DateEnd;
		
		#endregion

		#region ����

		/// <summary>
		/// ����סԺ��
		/// </summary>
		public string PatientNo
		{
			get
			{
				return this.patientNo;
			}
			set
			{
				this.patientNo = value;
			}
		}

        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string InpatientNo
        {
            get
            {
                return this.inpatientNo;
            }
            set
            {
                this.inpatientNo = value;
            }
        }
	
		/// <summary>
		/// ������
		/// </summary>
		public string BedNO
		{
			get
			{
				return this.bedNO;
			}
			set
			{
				this.bedNO = value;
			}
		}
		/// <summary>
		/// �������
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
		/// ���벡��
		/// </summary>
		public FS.FrameWork.Models.NeuObject NurseStation
		{
			get
			{
				return this.nurseStation;
			}
			set
			{
				this.nurseStation = value;
			}
		}
		/// <summary>
		/// ����ҽ��
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
		/// ��������
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
		/// ԤԼ��������
		/// </summary>
		public DateTime PreConsultationTime
		{
			get
			{
				return this.preConsultationTime;
			}
			set
			{
				this.preConsultationTime = value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime ConsultationTime
		{
			get
			{
				return this.consultationTime;
			}
			set
			{
				this.consultationTime = value;
			}
		}
		/// <summary>
		/// ����ҽԺ
		/// </summary>
		public FS.FrameWork.Models.NeuObject HosConsultation
		{
			get
			{
				return this.hosConsultation;
			}
			set
			{
				this.hosConsultation = value;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public FS.FrameWork.Models.NeuObject DeptConsultation
		{
			get
			{
				return this.deptConsultation;
			}
			set
			{
				this.deptConsultation = value;
			}
		}
		/// <summary>
		/// ����ҽ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject DoctorConsultation 
		{
			get
			{
				return this.doctorConsultation;
			}
			set
			{
				this.doctorConsultation = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public EnumConsultationType Type
		{
			get
			{
				if(this.HosConsultation.Name !="" || this.HosConsultation.ID !="") return EnumConsultationType.Hos;//ҽԺ����

				if(this.DoctorConsultation.ID =="" )//���һ���
				{
					return EnumConsultationType.Dept;
				}
				else//��Ա����
				{
					return EnumConsultationType.Doctor;
				}
			}
		}
	    /// <summary>
        /// �Ƿ��п���ҽ��Ȩ��
		/// </summary>
        public bool IsCreateOrder
        {
            get
            {
                return isCreateOrder; 
            }
            set 
            { 
                isCreateOrder = value; 
            }
        }
		/// <summary>
		/// �Ƿ�Ӽ�
		/// </summary>
		public bool IsEmergency 
		{
			get
			{
				return this.isEmergency;
			}
			set
			{
				this.isEmergency = value;
			}
		}
		/// <summary>
		/// ����˵��
		/// </summary>
		public string EmergencyMemo 
		{
			get
			{
				return this.emergencyMemo ;
			}
			set
			{
				this.emergencyMemo = value;
			}
		}
        /// <summary>
        /// �������������
        /// </summary>
		public string Suggestion 
		{
			get
			{
				return this.suggestion;
			}
			set
			{
				this.suggestion = value;
			}
		}
		/// <summary>
        /// �����¼
        /// </summary>
		public string Record 
		{
			get
			{
				return this.record;
			}
			set
			{
				this.record = value;
			}
		}
		/// <summary>
		/// ҽ����Ȩ��ʼ����
		/// </summary>
		public DateTime BeginTime
		{
			get
			{
				return this.beginTime;
			}
			set
			{
				this.beginTime = value;
			}
		}
		/// <summary>
		/// ҽ����Ȩ��������
		/// </summary>
		public DateTime EndTime
		{
			get
			{
				return this.endTime ;
			}
			set
			{
				this.endTime = value;
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public string Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}
		/// <summary>
		/// ȷ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject DoctorConfirm
		{
			get
			{
				return this.doctorConfirm;
			}
			set
			{
				this.doctorConfirm = value;
			}
		}
		/// <summary>
		/// ����״̬1 ����, 2ȷ��
		/// </summary>
		public int State
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
		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Consultation Clone()
		{
			Consultation obj=base.Clone() as Consultation;		
			obj.dept = this.dept.Clone();
			obj.deptConsultation = this.deptConsultation.Clone();
			obj.doctor = this.doctor.Clone();
			obj.doctorConfirm = this.doctorConfirm.Clone();
			obj.doctorConsultation = this.doctorConsultation.Clone();
			obj.hosConsultation = this.hosConsultation.Clone();
			obj.nurseStation = this.nurseStation.Clone();
			
			return obj;
		}

		#endregion

		#endregion

	}

	#region ö��

	/// <summary>
	/// ��������
	/// </summary>
	public enum EnumConsultationType 
	{

		/// <summary>
		/// ҽ��
		/// </summary>
		Doctor=0,
		/// <summary>
		/// ����
		/// </summary>
		Dept =1,
		/// <summary>
		/// ҽԺԺ�����
		/// </summary>
		Hos = 2
	}

	#endregion
}
