using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// PVisit <br></br>
	/// [��������: ���߷���ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
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
        private PatientTypeEnumService enumPatientType;

		/// <summary>
		/// ҽ�����
		/// </summary>
        private NeuObject medicalType;

		/// <summary>
		/// ����λ��
		/// </summary>
        private Location patientLocation;

		/// <summary>
		/// ����ҽ�� 
		/// </summary>
        private NeuObject attendingDoctor;

        /// <summary>
        /// ������
        /// </summary>
        private NeuObject attendingDirector;
                
		/// <summary>
		/// ������ҽʦ
		/// </summary>
        private NeuObject referringDoctor;

		/// <summary>
		/// ����ҽʦ
		/// </summary>
        private NeuObject consultingDoctor;

		/// <summary>
		/// ����ҽʦ��סԺҽʦ ����Ϊ�Һ�ҽʦ
		/// </summary>
        private NeuObject admittingDoctor;

        /// <summary>
        /// ����ҽʦ��Ӥ���Ǽǣ�
        /// </summary>
        private NeuObject responsibleDoctor;

		/// <summary>
		/// ʵϰҽ�� 
		/// </summary>
        private NeuObject tempDoctor;

		/// <summary>
		/// ת��/ת������ 
		/// </summary>
        private Location temporaryLocation;

		/// <summary>
		/// ��ICUλ��
		/// </summary>
        private Location icuLocation;

		/// <summary>
		/// ��Ժ;��
		/// </summary>
        private NeuObject admitSource;

		/// <summary>
		/// ��Ժ��Դ
		/// </summary>
        private NeuObject inSource;

		/// <summary>
		/// ��Ժ���
		/// </summary>
        private NeuObject circs;

		/// <summary>
		/// ���λ�ʿ
		/// </summary>
        private Models.Base.Employee admittingNurse;

		/// <summary>
		/// ����״̬ �Էѣ����ѣ���ͬ...
		/// </summary>
        private NeuObject accountStatus;

		/// <summary>
		/// סԺ״̬ 0-סԺ�Ǽ�  1-�������� 2-��Ժ�Ǽ� 3-��Ժ���� 4-ԤԼ��Ժ,5-�޷���Ժ
		/// </summary>
        private InStateEnumService inState;

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
        private NeuObject noon;

		/// <summary>
		/// �������
		/// </summary>
		private string seeNO;

		/// <summary>
		/// ת�����
		/// </summary>
        private NeuObject zg;

        /// <summary>
        /// ҽ�����ͣ�0��ͨ  1���� 2��������  3ICU   4�ھ�   5�ۿ�    6�ǿ�ָ������  7��������
        /// </summary>
        private string healthCareType;

		/// <summary>
		/// ��ʳ����״̬
		/// </summary>
        private string boardState;

        /// <summary>
        /// �������������
        /// </summary>
        private EnumAlertTypeService alertType;

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
        private NeuObject approveOper;

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
                if (enumPatientType == null)
                {
                    enumPatientType = new PatientTypeEnumService();
                }
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
                if (medicalType == null)
                {
                    medicalType = new NeuObject();
                }

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
                if (patientLocation == null)
                {
                    patientLocation = new Location();
                }

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
                if (attendingDoctor == null)
                {
                    attendingDoctor = new NeuObject();
                }
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
                if (attendingDirector == null)
                {
                    attendingDirector = new NeuObject();
                }
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
                if (referringDoctor == null)
                {
                    referringDoctor = new NeuObject();
                }
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
                if (consultingDoctor == null)
                {
                    consultingDoctor = new NeuObject();
                }

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
                if (admittingDoctor == null)
                {
                    admittingDoctor = new NeuObject();
                }

				return this.admittingDoctor;
			}
			set
			{
				this.admittingDoctor = value;
			}
		}

        /// <summary>
        /// ����ҽʦ��Ӥ���Ǽǣ�
        /// </summary>
        public NeuObject ResponsibleDoctor
        {
            get
            {
                if (responsibleDoctor == null)
                {
                    responsibleDoctor = new NeuObject();
                }
                return this.responsibleDoctor;
            }
            set
            {
                this.responsibleDoctor = value;
            }
        }

		/// <summary>
		/// ʵϰҽ��
		/// </summary>
		public NeuObject TempDoctor
		{
			get
			{
                if (tempDoctor == null)
                {
                    tempDoctor = new NeuObject();
                }

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
                if (temporaryLocation == null)
                {
                    temporaryLocation = new Location();
                }
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
                if (icuLocation == null)
                {
                    icuLocation = new Location();
                }
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
                if (admitSource == null)
                {
                    admitSource = new NeuObject();
                }
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
                if (inSource == null)
                {
                    inSource = new NeuObject();
                }

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
                if (circs == null)
                {
                    circs = new NeuObject();
                }

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
                if (admittingNurse == null)
                {
                    admittingNurse = new Employee();
                }
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
                if (accountStatus == null)
                {
                    accountStatus = new NeuObject();
                }
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
                if (inState == null)
                {
                    inState = new InStateEnumService();
                }
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
        /// ���������ñ��
        /// </summary>
        private bool alertFlag = false;

        /// <summary>
        /// ���������ñ��
        /// </summary>
        public bool AlertFlag
        {
            get
            {
                return alertFlag;
            }
            set
            {
                alertFlag = value;
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
                if (noon == null)
                {
                    noon = new NeuObject();
                }
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
                if (zg == null)
                {
                    zg = new NeuObject();
                }
				return this.zg;
			}
			set
			{
				this.zg = value;
			}
		}

        /// <summary>
        /// ҽ�����ͣ�0��ͨ  1���� 2��������  3ICU   4�ھ�   5�ۿ�    6�ǿ�ָ������  7��������
        /// </summary>
        public string HealthCareType
        {
            get
            {
                return healthCareType;
            }
            set
            {
                this.healthCareType = value;
            }
        }

		/// <summary>
		/// ��ʳ����״̬
		/// </summary>
		public string BoardState
		{
			get
			{
                if (boardState == null)
                {
                    boardState = string.Empty;
                }
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
                if (alertType == null)
                {
                    alertType = new EnumAlertTypeService();
                }

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
                if (approveOper == null)
                {
                    approveOper = new NeuObject();
                }
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
        public EnumPatientType PatientClass
        {
            get
            {
                return  EnumPatientType.C;
            }
            set
            {
            }
        }

		/// <summary>
		/// ��ICUλ��
		/// </summary>
        [Obsolete("�Ѿ����ڣ�����ΪICULocation", true)]
        private Location IcuLocation;

		/// <summary>
		/// סԺ״̬ 0-סԺ�Ǽ�  1-�������� 2-��Ժ�Ǽ� 3-��Ժ���� 4-ԤԼ��Ժ,5-�޷���Ժ
		/// </summary>
        [Obsolete("�Ѿ����ڣ�����ΪInState", true)]
        public EnumInState In_State
        {
            get
            {
                return EnumInState.N;
            }
            set
            {
            }
        }

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
        public NeuObject Zg
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

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
            pVisit.alertType = this.AlertType.Clone();
            pVisit.ResponsibleDoctor = this.ResponsibleDoctor.Clone();
			return pVisit;
		}

		#endregion
	}
}
