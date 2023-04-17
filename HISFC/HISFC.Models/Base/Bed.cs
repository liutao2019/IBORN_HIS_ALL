using System;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Models;
namespace  FS.HISFC.Models.Base
{
	/// <summary>
	/// Bed<br></br>
	/// [��������: ��λʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.ComponentModel.DisplayName("��λ��Ϣ")]
    [System.Serializable]
	public class Bed:FS.FrameWork.Models.NeuObject ,ISort ,IValid
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Bed()
		{
		}


		#region ����

	

		/// <summary>
		/// סԺ��
		/// </summary>
		private string inpatientNO;	
		
		/// <summary>
		/// �Ա�
		/// </summary>
        private SexEnumService sex;
		/// <summary>
		/// ����
		/// </summary>
        private FS.FrameWork.Models.NeuObject dept;
		/// <summary>
		/// ����վ
		/// </summary>
        private FS.FrameWork.Models.NeuObject nurseStation;
		
		/// <summary>
		/// ��λ״̬
		/// </summary>
        private FS.HISFC.Models.Base.BedStatusEnumService status;
		
		/// <summary>
		/// ��λ����
		/// </summary>
        private BedRankEnumService attribute;
		
		/// <summary>
		/// ��λ�ȼ�
		/// </summary>
        private FS.FrameWork.Models.NeuObject grade;
		
		/// <summary>
		/// ��λҽ��
		/// </summary>
        private FS.FrameWork.Models.NeuObject doctor;
		
		/// <summary>
		/// ����
		/// </summary>
        private NeuObject sickRoom;
		
		/// <summary>
		/// ��λ�绰
		/// </summary>
		private string phone;
		
		/// <summary>
		/// ԤԼ��Ժ����
		/// </summary>
		/// �������� 2006-08-30													
		private DateTime prepayOutdate;		
		
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>									
		private bool isValid;			
		
		/// <summary>
		/// �Ƿ�ԤԼ
		/// </summary>
		private bool isBooking;
		
		/// <summary>
		/// ���������
		/// </summary>
		private string computer;
		
		/// <summary>
		/// סԺҽ��
		/// </summary>
        private FS.FrameWork.Models.NeuObject admittingDoctor;
		
		/// <summary>
		/// ����ҽ��
		/// </summary>
        private FS.FrameWork.Models.NeuObject attendingDoctor;
		
		/// <summary>
		/// ����ҽ��
		/// </summary>
        private FS.FrameWork.Models.NeuObject consultingDoctor;
		
		/// <summary>
		/// ���λ�ʿ
		/// </summary>
        private FS.FrameWork.Models.NeuObject admittingNurse;
		
		/// <summary>
		/// ������
		/// </summary>
		private string  tendGroup;	
		
		/// <summary>
		/// �����
		/// </summary>			    				
        private int     sortID;		
																	
		#endregion
																				
		#region ����

		/// <summary>
		/// ��������סԺ��
		/// </summary>
		public string InpatientNO {
			get
			{
				return inpatientNO;
			}
			set
			{
				inpatientNO = value;
			}
		}


		/// <summary>
		/// �����Ա�
		/// </summary>
		public SexEnumService Sex
		{
			get
			{
                if (sex == null)
                {
                    sex = new SexEnumService();
                }
				return this.sex;
			}
			set
			{
				this.sex = value;
			}
		}


		/// <summary>
		/// ������������
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept
		{
			get
			{
                if (dept == null)
                {
                    dept = new NeuObject();
                }
				return this.dept;
			}
			set
			{
				this.dept = value;
			}
		}


		/// <summary>
		/// ������������
		/// </summary>
		public FS.FrameWork.Models.NeuObject NurseStation
		{
			get
            {
                if (nurseStation == null)
                {
                    nurseStation = new NeuObject();
                }
				return this.nurseStation;
			}
			set
			{
				this.nurseStation = value;
			}
		}
		

		/// <summary>
		/// ����״̬
		/// </summary>
        [System.ComponentModel.DisplayName("����״̬")]
        [System.ComponentModel.Description("��λ��ǰ����״̬")]
		public FS.HISFC.Models.Base.BedStatusEnumService Status
		{
			get
            {
                if (status == null)
                {
                    status = new BedStatusEnumService();
                }
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}


		/// <summary>
		/// ��λ����
		/// </summary>
        [System.ComponentModel.DisplayName("��λ����")]
        [System.ComponentModel.Description("��λ��ǰ����")]
		public FS.HISFC.Models.Base.BedRankEnumService BedRankEnumService 
		{
			get
			{
                if (attribute == null)
                {
                    attribute = new BedRankEnumService();
                }
				return this.attribute;
			}
			set
			{
				this.attribute = value;
			}
		}


		/// <summary>
		/// ��λ�ȼ�
		/// </summary>
        [System.ComponentModel.DisplayName("��λ�ȼ�")]
        [System.ComponentModel.Description("��λ��ǰ�ȼ�")]
		public FS.FrameWork.Models.NeuObject BedGrade 
		{
			get
			{
                if (grade == null)
                {
                    grade = new NeuObject();
                }
				return this.grade;
			}
			set
			{
				this.grade = value;
			}
		}


		/// <summary>
		/// ҽ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Doctor 
		{
			get
            {
                if (doctor == null)
                {
                    doctor = new NeuObject();
                }
				return this.doctor;
			}
			set
			{
				this.doctor = value;
			}
		}


		/// <summary>
		/// ������Ϣ
		/// </summary>
        [System.ComponentModel.DisplayName("������Ϣ")]
        [System.ComponentModel.Description("��λ��ǰ������Ϣ")]
		public FS.FrameWork.Models.NeuObject SickRoom
		{
			get
            {
                if (sickRoom == null)
                {
                    sickRoom = new NeuObject();
                }
				return this.sickRoom;
			}
			set
			{
				this.sickRoom = value;
			}
		}


		/// <summary>
		/// �����绰
		/// </summary>
		public string Phone
		{
			get
			{
				return this.phone;
			}
			set
			{
				this.phone = value;
			}

		}


		/// <summary>
		/// ��Ժ����(ԤԼ)
		/// </summary>
		public DateTime PrepayOutdate
		{
			get
			{
				return this.prepayOutdate;
			}
			set
			{
				this.prepayOutdate = value;
			}
		}


		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
        [System.ComponentModel.DisplayName("��λ��Ч��")]
        [System.ComponentModel.Description("��λ��ǰ��Ч��")]
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}


		/// <summary>
		/// �Ƿ�ԤԼ
		/// </summary>
		public bool IsPrepay
		{
			get
			{
				return this.isBooking;
			}
			set
			{
				this.isBooking = value;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		public string OwnerPc
		{
			get
			{
				return this.computer;
			}
			set
			{
				this.computer = value;
			}
		}


		/// <summary>
		/// סԺҽ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject AdmittingDoctor
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
		/// ����ҽ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject AttendingDoctor
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
		/// ����ҽ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject  ConsultingDoctor
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
		/// ���λ�ʿ
		/// </summary>
		public FS.FrameWork.Models.NeuObject AdmittingNurse
		{
			get
            {
                if (admittingNurse == null)
                {
                    admittingNurse = new NeuObject();
                }
				return this.admittingNurse;
			}
			set
			{
				this.admittingNurse = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public string TendGroup
		{
			get
			{
				return this.tendGroup;
			}
			set
			{
				this.tendGroup = value;
			}
				
		}
	

		/// <summary>
		/// �������
		/// </summary>
		public int SortID
		{
			get
			{
				// TODO:  ��� Order.SortID getter ʵ��
				return this.sortID ;
			}
			set
			{
				// TODO:  ��� Order.SortID setter ʵ��
				this.sortID =value;
			}
		}


		#endregion

		#region ����


		#region ��¡
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
        public new Bed Clone()
        {
            Bed bed = base.Clone() as Bed;

            bed.Dept = this.Dept.Clone();
            bed.NurseStation = this.NurseStation.Clone();
            bed.BedGrade = this.BedGrade.Clone();
            bed.AdmittingDoctor = this.AdmittingDoctor.Clone();
            bed.AdmittingNurse = this.AdmittingNurse.Clone();
            bed.Doctor = this.Doctor.Clone();
            bed.AttendingDoctor = this.AttendingDoctor.Clone();
            bed.ConsultingDoctor = this.ConsultingDoctor.Clone();
            bed.Status = this.Status.Clone() as BedStatusEnumService;
            return bed;
        }
		#endregion

		#endregion



	}
}
