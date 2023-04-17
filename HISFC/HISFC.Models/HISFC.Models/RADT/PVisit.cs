using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// PVisit <br></br>
	/// [��������: ���߷���ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��һ��'
	///		�޸�ʱ��='2006-09-11'
	///		�޸�Ŀ��='�汾����'
	///		�޸�����=''
	///  />
	/// </summary>
    ///	 <�޸ļ�¼
    ///		�޸���='����'
    ///		�޸�ʱ��='2007-09-05'
    ///		�޸�Ŀ��='�汾����'
    ///		�޸�����='���ӿ�����attendingDirector'
    ///  />
    /// </summary>
    [Serializable]
    public class PVisit : FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PVisit()
		{
		}

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		private PatientTypeEnumService enumPatientType = new PatientTypeEnumService();

		/// <summary>
		/// ҽ�����
		/// </summary>
		private NeuObject medicalType = new NeuObject();

		/// <summary>
		/// ����λ��
		/// </summary>
		private Location patientLocation = new Location();

		/// <summary>
		/// ����ҽ�� 
		/// </summary>
		private NeuObject attendingDoctor = new NeuObject();

        /// <summary>
        /// ������
        /// </summary>
        private NeuObject attendingDirector = new NeuObject();
                
		/// <summary>
		/// ������ҽʦ
		/// </summary>
		private NeuObject referringDoctor = new NeuObject();

		/// <summary>
		/// ����ҽʦ
		/// </summary>
		private NeuObject consultingDoctor = new NeuObject();

		/// <summary>
		/// ����ҽʦ��סԺҽʦ ����Ϊ�Һ�ҽʦ
		/// </summary>
		private NeuObject admittingDoctor = new NeuObject();

		/// <summary>
		/// ʵϰҽ�� 
		/// </summary>
		private NeuObject tempDoctor = new NeuObject();

		/// <summary>
		/// ת��/ת������ 
		/// </summary>
		private Location temporaryLocation = new Location();

		/// <summary>
		/// ��ICUλ��
		/// </summary>
		private Location icuLocation = new Location();

		/// <summary>
		/// ��Ժ;��
		/// </summary>
		private NeuObject admitSource = new NeuObject();

		/// <summary>
		/// ��Ժ��Դ
		/// </summary>
		private NeuObject inSource = new NeuObject();

		/// <summary>
		/// ��Ժ���
		/// </summary>
		private NeuObject circs = new NeuObject();

		/// <summary>
		/// ���λ�ʿ
		/// </summary>
        private Models.Base.Employee admittingNurse = new Employee();

		/// <summary>
		/// ����״̬ �Էѣ����ѣ���ͬ...
		/// </summary>
		private NeuObject accountStatus = new NeuObject();

		/// <summary>
		/// סԺ״̬ 0-סԺ�Ǽ�  1-�������� 2-��Ժ�Ǽ� 3-��Ժ���� 4-ԤԼ��Ժ,5-�޷���Ժ
		/// </summary>
		private InStateEnumService inState = new InStateEnumService();

		/// <summary>
		/// סԺ����
		/// </summary>
		private DateTime inTime;

		/// <summary>
		/// ��Ժ����
		/// </summary>
		private DateTime outTime;

		/// <summary>
		/// ԤԼ��Ժ����
		/// </summary>
		private DateTime preOutTime;

		/// <summary>
		/// ע������
		/// </summary>
		private DateTime registTime;

		/// <summary>
		/// ������
		/// </summary>
		private decimal moneyAlert;

		/// <summary>
		/// ���
		/// </summary>
		private NeuObject noon = new NeuObject();

		/// <summary>
		/// �������
		/// </summary>
		private string seeNO;

		/// <summary>
		/// ת�����
		/// </summary>
		private NeuObject zg = new NeuObject();

		/// <summary>
		/// ��ʳ����״̬
		/// </summary>
		private string boardState = "";

        /// <summary>
        /// �������������
        /// </summary>
        private EnumAlertTypeService alertType = new EnumAlertTypeService();

        /// <summary>
        /// ������ʱ������ÿ�ʼʱ��
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// ������ʱ������ý���ʱ��
        /// </summary>
        private DateTime endDate = DateTime.MinValue;

        #region ���Ӿ�������׼�˺���׼ʱ��
        //{6D09DD27-A256-44ad-9D89-33DF0B8DF9FA}
        /// <summary>
        /// ��׼��
        /// </summary>
        private NeuObject approveOper = new NeuObject();

        /// <summary>
        /// ��׼ʱ��
        /// </summary>
        private DateTime approveDate = DateTime.MinValue;
        #endregion 
		#endregion

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		public PatientTypeEnumService PatientType
		{
			get
			{
				return this.enumPatientType;
			}
			set
			{
				this.enumPatientType = value;
			}
		}

		/// <summary>
		/// ҽ�����
		/// </summary>
		public NeuObject MedicalType
		{
			get
			{
				return this.medicalType;
			}
			set
			{
				this.medicalType = value;
			}
		}

		/// <summary>
		/// ����λ��
		/// </summary>
		public Location PatientLocation
		{
			get
			{
				return this.patientLocation;
			}
			set
			{
				this.patientLocation = value;
			}
		}

		/// <summary>
		/// ����ҽ��
		/// </summary>
		public NeuObject AttendingDoctor
		{
			get
			{
				return this.attendingDoctor;
			}
			set
			{
				this.attendingDoctor = value;
			}
		}

        /// <summary>
        /// ������
        /// </summary>   
        public NeuObject AttendingDirector
        {
            get
            {
                return attendingDirector;
            }
            set
            {
                attendingDirector = value;
            }
        }

		/// <summary>
		/// ������ҽʦ
		/// </summary>
		public NeuObject ReferringDoctor
		{
			get
			{
				return this.referringDoctor;
			}
			set
			{
				this.referringDoctor = value;
			}
		}

		/// <summary>
		/// ����ҽʦ
		/// </summary>
		public NeuObject ConsultingDoctor
		{
			get
			{
				return this.consultingDoctor;
			}
			set
			{
				this.consultingDoctor = value;
			}
		}

		/// <summary>
		/// ����ҽʦ��סԺҽʦ
		/// </summary>
		public NeuObject AdmittingDoctor
		{
			get
			{
				return this.admittingDoctor;
			}
			set
			{
				this.admittingDoctor = value;
			}
		}

		/// <summary>
		/// ʵϰҽ��
		/// </summary>
		public NeuObject TempDoctor
		{
			get
			{
				return this.tempDoctor;
			}
			set
			{
				this.tempDoctor = value;
			}
		}

		/// <summary>
		/// ת��/ת������
		/// </summary>
		public Location TemporaryLocation
		{
			get
			{
				return this.temporaryLocation;
			}
			set
			{
				this.temporaryLocation = value;
			}
		}

		/// <summary>
		/// ��ICUλ��
		/// </summary>
		public Location ICULocation
		{
			get
			{
				return this.icuLocation;
			}
			set
			{
				this.icuLocation = value;
			}
		}

		/// <summary>
		/// ��Ժ;��
		/// </summary>
		public NeuObject AdmitSource
		{
			get
			{
				return this.admitSource;
			}
			set
			{
				this.admitSource = value;
			}
		}

		/// <summary>
		/// ��Ժ��Դ
		/// </summary>
		public NeuObject InSource
		{
			get
			{
				return this.inSource;
			}
			set
			{
				this.inSource = value;
			}
		}

		/// <summary>
		/// ��Ժ���
		/// </summary>
		public NeuObject Circs
		{
			get
			{
				return this.circs;
			}
			set
			{
				this.circs = value;
			}
		}

		/// <summary>
		/// ���λ�ʿ
		/// </summary>
		public Employee AdmittingNurse
		{
			get
			{
				return this.admittingNurse;
			}
			set
			{
				this.admittingNurse = value;
			}
		}

		/// <summary>
		/// ����״̬ �Էѣ����ѣ���ͬ...
		/// </summary>
		public NeuObject AccountStatus
		{
			get
			{
				return this.accountStatus;
			}
			set
			{
				this.accountStatus = value;
			}
		}

		/// <summary>
		/// סԺ״̬ 0-סԺ�Ǽ�  1-�������� 2-��Ժ�Ǽ� 3-��Ժ���� 4-ԤԼ��Ժ,5-�޷���Ժ
		/// </summary>
		public InStateEnumService InState
		{
			get
			{
				return this.inState;
			}
			set
			{
				this.inState = value;
			}
		}

		/// <summary>
		/// סԺ����
		/// </summary>
		public DateTime InTime
		{
			get
			{
				return this.inTime;
			}
			set
			{
				this.inTime = value;
			}
		}

		/// <summary>
		/// ��Ժ����
		/// </summary>
		public DateTime OutTime
		{
			get
			{
				return this.outTime;
			}
			set
			{
				this.outTime = value;
			}
		}

		/// <summary>
		/// ԤԼ��Ժ����
		/// </summary>
		public DateTime PreOutTime
		{
			get
			{
				return this.preOutTime;
			}
			set
			{
				this.preOutTime = value;
			}
		}

		/// <summary>
		/// ע������
		/// </summary>
		public DateTime RegistTime
		{
			get
			{
				return this.registTime;
			}
			set
			{
				this.registTime = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public decimal MoneyAlert
		{
			get
			{
				return this.moneyAlert;
			}
			set
			{
				this.moneyAlert = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		public NeuObject Noon
		{
			get
			{
				return this.noon;
			}
			set
			{
				this.noon = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public string SeeNO
		{
			get
			{
				return this.seeNO;
			}
			set
			{
				this.seeNO = value;
			}
		}

		/// <summary>
		/// ת�����
		/// </summary>
		public NeuObject ZG
		{
			get
			{
				return this.zg;
			}
			set
			{
				this.zg = value;
			}
		}

		/// <summary>
		/// ��ʳ����״̬
		/// </summary>
		public string BoardState
		{
			get
			{
				return this.boardState;
			}
			set
			{
				this.boardState = value;
			}
		}

        /// <summary>
        /// �������������
        /// </summary>
        public EnumAlertTypeService AlertType
        {
            get
            {
                return alertType;
            }
            set
            {
                alertType = value;
            }
        }

        /// <summary>
        /// ������ʱ������ÿ�ʼʱ��
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return beginDate;
            }
            set
            {
                beginDate = value;
            }
        }

        /// <summary>
        /// ������ʱ������ý���ʱ��
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        #region ���Ӿ�������׼�˺���׼ʱ��
        //{6D09DD27-A256-44ad-9D89-33DF0B8DF9FA}
        /// <summary>
        /// ��׼��
        /// </summary>
        public NeuObject ApproveOper
        {
            get
            {
                return approveOper;
            }
            set
            {
                approveOper = value;
            }
        }

        /// <summary>
        /// ��׼ʱ��
        /// </summary>
        public DateTime ApproveDate
        {
            get
            {
                return approveDate;
            }
            set
            {
                approveDate = value;
            }
        }
        #endregion
		#endregion

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪPatientType", true)]
		public EnumPatientType PatientClass = new EnumPatientType();

		/// <summary>
		/// ��ICUλ��
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪICULocation", true)]
		private Location IcuLocation = new Location();

		/// <summary>
		/// סԺ״̬ 0-סԺ�Ǽ�  1-�������� 2-��Ժ�Ǽ� 3-��Ժ���� 4-ԤԼ��Ժ,5-�޷���Ժ
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪInState", true)]
		public EnumInState In_State = new EnumInState();

		/// <summary>
		/// סԺ����
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪInTime", true)]
		public DateTime Date_In;

		/// <summary>
		/// ��Ժ����
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪOutTime", true)]
		public DateTime Date_Out;

		/// <summary>
		/// ԤԼ��Ժ����
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪpreOutTime", true)]
		public DateTime Date_PreOut;

		/// <summary>
		/// ע������
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪRegistTime", true)]
		public DateTime Date_Register;

		/// <summary>
		/// �������
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪSeeNO", true)]
		public string SeeNo="0";

		/// <summary>
		/// ת�����
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪZG", true)]
		public NeuObject Zg = new NeuObject();

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new PVisit Clone()
		{
			PVisit pVisit = base.Clone() as PVisit;

			pVisit.MedicalType = this.MedicalType.Clone();
			pVisit.PatientLocation = this.PatientLocation.Clone();
			pVisit.AttendingDoctor = this.AttendingDoctor.Clone();
            pVisit.AttendingDirector = this.AttendingDirector.Clone();
			pVisit.ReferringDoctor = this.ReferringDoctor.Clone();
			pVisit.ConsultingDoctor = this.ConsultingDoctor.Clone();
			pVisit.AdmittingDoctor = this.AdmittingDoctor.Clone();
			pVisit.TempDoctor = this.TempDoctor.Clone();
			pVisit.TemporaryLocation = this.TemporaryLocation.Clone();
			pVisit.AdmitSource = this.AdmitSource.Clone();
			pVisit.InSource = this.InSource.Clone();
			pVisit.Circs = this.Circs.Clone();
			pVisit.AdmittingNurse = this.AdmittingNurse.Clone();
			pVisit.AccountStatus = this.AccountStatus.Clone();
			pVisit.Noon = this.Noon.Clone();
			pVisit.ZG = this.ZG.Clone();
			pVisit.PatientType = this.PatientType.Clone();
            pVisit.alertType = this.alertType.Clone();
			return pVisit;
		}

		#endregion
	}
}
