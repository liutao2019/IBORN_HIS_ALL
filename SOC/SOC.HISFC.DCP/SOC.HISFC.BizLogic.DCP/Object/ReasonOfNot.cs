using System;

namespace FS.SOC.HISFC.BizLogic.DCP.Object
{
    /// <summary>
    /// ReasonOfNot<br></br>
    /// [��������: ReasonOfNot������ԭ��]<br></br>
    /// [�� �� ��: yeph]<br></br>
    /// [����ʱ��: 2014-12-31]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class ReasonOfNot : FS.FrameWork.Models.NeuObject
	{
        #region ˽���ֶ�

        /// <summary>
        /// �������
        /// </summary>
        private string diagName = "";

		/// <summary>
		/// ���濨���ͣ�C���� IסԺ O������
		/// </summary>
		private string patientType = "";

		/// <summary>
		/// ������Ϣ
		/// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

		/// <summary>
		/// ������ԭ��
		/// </summary>
		private string reasonOfNot = "";

		/// <summary>
		/// ������Ժ���� 
		/// </summary>
		private string otherName = "";

		
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

		
        #endregion

        #region �����ֶ�

        /// <summary>
        /// �������
        /// </summary>
        public string DiagName
        {
            get
            {
                return this.diagName;
            }
            set 
            {
                this.diagName = value;
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
		/// ������ԭ��
		/// </summary>
        public string ReasonOfNot1
        {
            get
            {
                return this.reasonOfNot;
            }
            set
            {
                this.reasonOfNot = value;
            }
        }

		/// <summary>
		/// ������Ժ����
		/// </summary>
        public string OtherName
        {
            get
            {
                return this.otherName;
            }
            set
            {
                this.otherName = value;
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

		
        #endregion

        public ReasonOfNot()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region ��¡����
        public new ReasonOfNot Clone()
        {
            ReasonOfNot commonReport = base.Clone() as ReasonOfNot;
            commonReport.patient = this.patient.Clone();
            commonReport.doctorDept = this.doctorDept.Clone();
            commonReport.reportDoctor = this.reportDoctor.Clone();



            return commonReport;
        }
        #endregion
    }

   
}
