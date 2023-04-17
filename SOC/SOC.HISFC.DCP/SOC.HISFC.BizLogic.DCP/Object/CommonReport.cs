using System;

namespace FS.HISFC.DCP.Object
{
    /// <summary>
    /// CommonReport<br></br>
    /// [��������: CommonReport��������Ԥ������ʵ��]<br></br>
    /// [�� �� ��: zengft]<br></br>
    /// [����ʱ��: 2008-8-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class CommonReport : FS.FrameWork.Models.NeuObject
	{
        #region ˽���ֶ�

        /// <summary>
        /// �������
        /// </summary>
        private string reportNO = "";

		/// <summary>
		/// ���濨���ͣ�C���� IסԺ O������
		/// </summary>
		private string patientType = "";

		/// <summary>
		/// ������Ϣ
		/// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

		/// <summary>
		/// �ҳ�����
		/// </summary>
		private string patientParents = "";

		/// <summary>
		/// ���䵥λ
		/// </summary>
		private string ageUnit = "";

		/// <summary>
		/// ������Դ
		/// </summary>
		private string homeArea = "";

		/// <summary>
		/// ��סַ-ʡ
		/// </summary>
		private FS.FrameWork.Models.NeuObject homeProvince = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��סַ-��
		/// </summary>
		private FS.FrameWork.Models.NeuObject homeCity = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��סַ-�أ�����
		/// </summary>
        private FS.FrameWork.Models.NeuObject homeCouty = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��סַ-���硢�ֵ���
		/// </summary>
		private FS.FrameWork.Models.NeuObject homeTown = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���߿��ң�סԺ���ҡ�������ң�
		/// </summary>
		private FS.FrameWork.Models.NeuObject patientDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ������ID���� Name���� Memo�����־��
		/// </summary>
		private FS.FrameWork.Models.NeuObject disease = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��������
		/// </summary>
		private System.DateTime infectDate;

		/// <summary>
		/// �������
		/// </summary>
		private System.DateTime diagnosisTime;

		/// <summary>
		/// ��������
		/// </summary>
		private System.DateTime deadDate;

		/// <summary>
		/// ��������
		/// </summary>
		private FS.FrameWork.Models.NeuObject caseClass1 = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// �������ࣨ0���ԡ�1���ԡ�2δ���ͣ�
		/// </summary>
		private string caseClass2 = "";

		/// <summary>
		/// ���޽Ӵ�������1�С�0�ޣ�
		/// </summary>
		private string infectOtherFlag = "";

		/// <summary>
		/// ���濨״̬��0�¼ӡ�1�ϸ�2���ϸ�3���������ϡ�4���������ϣ�
		/// </summary>
        private string state = "";//enumReportState.�¼�;

		/// <summary>
		/// �Ƿ��и�����1�� 0��
		/// </summary>
		private string addtionFlag = "";

        /// <summary>
        /// �Ա���
        /// </summary>
        private string sexDiseaseFlag = "";

		/// <summary>
		/// ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject reportDoctor = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��������
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctorDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private System.DateTime reportTime;

		/// <summary>
		/// ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject cancelOper = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private System.DateTime cancelTime;

		/// <summary>
		/// �޸���
		/// </summary>
		private FS.FrameWork.Models.NeuObject modifyOper = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		private System.DateTime modifyTime;

		/// <summary>
		/// �����
		/// </summary>
		private FS.FrameWork.Models.NeuObject approveOper = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���ʱ��
		/// </summary>
		private System.DateTime approveTime;

        /// <summary>
        /// ������־[1�Ѿ�������0δ����]
        /// </summary>
        private string correctFlag = "";

        /// <summary>
        /// ������ID
        /// </summary>
        private string correctReportNO = "";

        /// <summary>
        /// ��������ԭ��ID
        /// </summary>
        private string correctedReportNO = "";

		/// <summary>
		/// ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��������
		/// </summary>
		private FS.FrameWork.Models.NeuObject operDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private System.DateTime operTime;
		
		/// <summary>
		/// ���ɣ���¼����ԭ��
		/// </summary>
		private string operCase = "";

		/// <summary>
		/// ��չ��Ϣ
		/// </summary>
		private string extendInfo1 = "";

		/// <summary>
		/// ��չ��Ϣ
		/// </summary>
		private string extendInfo2 = "";

		/// <summary>
		/// ��չ��Ϣ
		/// </summary>
		private string extendInfo3 = "";
        /// <summary>
        /// ��������־
        /// </summary>
        private string cancer_Flag;
        /// <summary>
        /// ���������
        /// </summary>
        private string cancer_no = "";
        #endregion

        #region �����ֶ�

        /// <summary>
        /// �������
        /// </summary>
        public string ReportNO
        {
            get
            {
                return this.reportNO;
            }
            set 
            {
                this.reportNO = value;
            }
        }

		/// <summary>
		/// ���濨���ͣ�C���� IסԺ O������
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
		/// ������Ϣ
		/// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
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
		/// �ҳ�����
		/// </summary>
        public string PatientParents
        {
            get
            {
                return this.patientParents;
            }
            set
            {
                this.patientParents = value;
            }
        }

		/// <summary>
		/// ���䵥λ
		/// </summary>
        public string AgeUnit
        {
            get
            {
                return this.ageUnit;
            }
            set
            {
                this.ageUnit = value;
            }
        }

		/// <summary>
		/// ������Դ
		/// </summary>
        public string HomeArea
        {
            get
            {
                return this.homeArea;
            }
            set
            {
                this.homeArea = value;
            }
        }

		/// <summary>
		/// ��סַ-ʡ
		/// </summary>
        public FS.FrameWork.Models.NeuObject HomeProvince
        {
            get
            {
                return this.homeProvince;
            }
            set 
            {
                this.homeProvince = value;
            }
        }

		/// <summary>
		/// ��סַ-��
		/// </summary>
        public FS.FrameWork.Models.NeuObject HomeCity
        {
            get 
            {
                return this.homeCity;
            }
            set
            {
                this.homeCity = value;
            }
        }

		/// <summary>
		/// ��סַ-�أ�����
		/// </summary>
        public FS.FrameWork.Models.NeuObject HomeCouty
        {
            get
            {
                return this.homeCouty;
            }
            set
            {
                this.homeCouty = value;
            }
        }

		/// <summary>
		/// ��סַ-���硢�ֵ���
		/// </summary>
        public FS.FrameWork.Models.NeuObject HomeTown
        {
            get
            {
                return this.homeTown;
            }
            set 
            {
                this.homeTown = value;
            }
        }

		/// <summary>
		/// ���߿��ң�סԺ���ҡ�������ң�
		/// </summary>
        public FS.FrameWork.Models.NeuObject PatientDept
        {
            get
            {
                return this.patientDept;
            }
            set
            {
                this.patientDept = value;
            }
        }

		/// <summary>
		/// ������ID���� Name���� Memo�����־��
		/// </summary>
        public FS.FrameWork.Models.NeuObject Disease
        {
            get
            {
                return this.disease;
            }
            set
            {
                this.disease = value;
            }
        }

		/// <summary>
		/// ��������
		/// </summary>
        public System.DateTime InfectDate
        {
            get
            {
                return this.infectDate;
            }
            set
            {
                this.infectDate = value;
            }
        }

		/// <summary>
		/// ���ʱ��
		/// </summary>
        public System.DateTime DiagnosisTime
        {
            get
            {
                return this.diagnosisTime;
            }
            set 
            {
                this.diagnosisTime = value;
            }
        }

		/// <summary>
		/// ��������
		/// </summary>
        public System.DateTime DeadDate
        {
            get 
            {
                return this.deadDate;
            }
            set 
            {
                this.deadDate = value;
            }
        }

		/// <summary>
		/// ��������
		/// </summary>
        public FS.FrameWork.Models.NeuObject CaseClass1
        {
            get
            {
                return this.caseClass1;
            }
            set
            {
                this.caseClass1 = value;
            }
        }

		/// <summary>
		/// �������ࣨ0���ԡ�1���ԡ�2δ���ͣ�
		/// </summary>
		public string CaseClass2
        {
            get
            {
                return this.caseClass2;
            }
            set
            {
                this.caseClass2 = value;
            }
        }

		/// <summary>
		/// ���޽Ӵ�������1�С�0�ޣ�
		/// </summary>
		public string InfectOtherFlag
        {
            get
            {
                return this.infectOtherFlag;
            }
            set
            {
                this.infectOtherFlag = value;
            }
        }

		/// <summary>
		/// ���濨״̬��0�¼ӡ�1�ϸ�2���ϸ�3���������ϡ�4���������ϣ�
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
		/// �Ƿ��и�����1�� 0��
		/// </summary>
        public string AddtionFlag
        {
            get
            {
                return this.addtionFlag;
            }
            set
            {
                this.addtionFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ��Բ���1�� 0��
        /// </summary>
        public string SexDiseaseFlag
        {
            get
            {
                return this.sexDiseaseFlag;
            }
            set
            {
                this.sexDiseaseFlag = value;
            }
        }

		/// <summary>
		/// ������
		/// </summary>
        public FS.FrameWork.Models.NeuObject ReportDoctor
        {
            get
            {
                return this.reportDoctor;
            }
            set
            {
                this.reportDoctor = value;
            }
        }

		/// <summary>
		/// ��������
		/// </summary>
        public FS.FrameWork.Models.NeuObject DoctorDept
        {
            get
            {
                return this.doctorDept;
            }
            set 
            {
                this.doctorDept = value;
            }
        }

		/// <summary>
		/// ����ʱ��
		/// </summary>
        public System.DateTime ReportTime
        {
            get            
            {
                return this.reportTime;
            }
            set 
            {
                this.reportTime = value;
            }
        }

		/// <summary>
		/// ������
		/// </summary>
        public FS.FrameWork.Models.NeuObject CancelOper
        {
            get
            {
                return this.cancelOper;
            }
            set
            {
                this.cancelOper = value;
            }
        }

		/// <summary>
		/// ����ʱ��
		/// </summary>
        public System.DateTime CancelTime
        {
            get
            {
                return this.cancelTime;
            }
            set
            {
                this.cancelTime = value;
            }
        }

		/// <summary>
		/// �޸���
		/// </summary>
        public FS.FrameWork.Models.NeuObject ModifyOper
        {
            get
            {
                return this.modifyOper;
            }
            set
            {
                this.modifyOper = value;
            }
        }

		/// <summary>
		/// �޸�ʱ��
		/// </summary>
        public System.DateTime ModifyTime
        {
            get
            {
                return  this.modifyTime;
            }
            set
            {
                this.modifyTime = value;
            }
        }

		/// <summary>
		/// �����
		/// </summary>
        public FS.FrameWork.Models.NeuObject ApproveOper
        {
            get
            {
                return this.approveOper;
            }
            set
            {
                this.approveOper = value;
            }
        }

		/// <summary>
		/// ���ʱ��
		/// </summary>
        public System.DateTime ApproveTime
        {
            get
            {
                return this.approveTime;
            }
            set
            {
                this.approveTime = value;
            }
        }

        /// <summary>
        /// �Ƿ��Ѿ�����[1������0δ����]
        /// </summary>
        public string CorrectFlag
        {
            get
            {
                return this.correctFlag;
            }
            set 
            {
                this.correctFlag = value;
            }
        }

        /// <summary>
        /// ������ID
        /// </summary>
        public string CorrectReportNO
        {
            get
            {
                return this.correctReportNO;
            }
            set
            {
                this.correctReportNO = value;
            }
        }

        /// <summary>
        /// ��������ԭ��ID
        /// </summary>
        public string CorrectedReportNO
        {
            get
            {
                return this.correctedReportNO;
            }
            set
            {
                this.correctedReportNO = value;
            }
        }

		/// <summary>
		/// ������
		/// </summary>
        public FS.FrameWork.Models.NeuObject Oper
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
		/// ��������
		/// </summary>
        public FS.FrameWork.Models.NeuObject OperDept
        {
            get
            {
                return this.operDept;
            }
            set
            {
                this.operDept = value;
            }
        }

		/// <summary>
		/// ����ʱ��
		/// </summary>
        public System.DateTime OperTime
        {
            get
            {
                return this.operTime;
            }
            set
            {
                this.operTime = value;
            }
        }
		
		/// <summary>
		/// ���ɣ���¼����ԭ��
		/// </summary>
        public string OperCase
        {
            get
            {
                return this.operCase;
            }
            set
            {
                this.operCase = value;
            }
        }

		/// <summary>
		/// ��չ��Ϣ
		/// </summary>
        public string ExtendInfo1
        {
            get
            {
                return this.extendInfo1;
            }
            set 
            {
                this.extendInfo1 = value;
            }
        }

		/// <summary>
		/// ��չ��Ϣ
		/// </summary>
		public string ExtendInfo2
        {
            get
            {
                return this.extendInfo2;
            }
            set
            {
                this.extendInfo2 = value;
            }
        }

		/// <summary>
		/// ��չ��Ϣ
		/// </summary>
		public string ExtendInfo3
        {
            get
            {
                return this.extendInfo3;
            }
            set
            {
                this.extendInfo3 = value;
            }
        }
        /// <summary>
        /// ��������־
        /// </summary>
        public string Cancer_Flag
        {
            get
            {
                return this.cancer_Flag;
            }
            set
            {
                this.cancer_Flag = value;
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public string Cancer_No
        {
            get
            {
                return this.cancer_no;
            }
            set
            {
                this.cancer_no = value;
            }
        }
        #endregion

        public CommonReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region ��¡����
        public new CommonReport Clone()
        {
            CommonReport commonReport = base.Clone() as CommonReport;
            commonReport.patient = this.patient.Clone();
            commonReport.homeCity = this.homeCity.Clone();
            commonReport.homeCouty = this.homeCouty.Clone();
            commonReport.homeProvince = this.homeProvince.Clone();
            commonReport.homeTown = this.homeTown.Clone();
            commonReport.patientDept = this.patientDept.Clone();
            commonReport.disease = this.disease.Clone();
            commonReport.caseClass1 = this.caseClass1.Clone();
            commonReport.reportDoctor = this.reportDoctor.Clone();
            commonReport.doctorDept = this.doctorDept.Clone();
            commonReport.cancelOper = this.cancelOper.Clone();
            commonReport.modifyOper = this.modifyOper.Clone();
            commonReport.approveOper = this.approveOper.Clone();
            commonReport.operDept = this.operDept.Clone();

            return commonReport;
        }
        #endregion
    }

   
}
