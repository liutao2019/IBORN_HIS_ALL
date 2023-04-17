using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Order.OutPatient
{
	/// <summary>
	/// FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory<br></br>
	/// [��������: ���ﲡ��ʵ��]<br></br>
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
    public class ClinicCaseHistory : FS.FrameWork.Models.NeuObject
    {
        #region	����

        #region ˽��

        /// <summary>
        /// ����
        /// </summary>
        private string caseMain;

        /// <summary>
        /// �ֲ�ʷ
        /// </summary>
        private string caseNow;

        /// <summary>
        /// ����ʷ
        /// </summary>
        private string caseOld;

        /// <summary>
        /// ����ʷ
        /// </summary>
        private string caseAllery;

        /// <summary>
        /// ����
        /// </summary>
        private string checkBody;

        /// <summary>
        /// ���
        /// </summary>
        private string caseDiag;

        /// <summary>
        /// �Ƿ��Ⱦ
        /// </summary>
        private bool isInfect = false;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isAllery = false;

        /// <summary>
        /// ��� 1������ 2������
        /// </summary>
        private string moduletype;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private string doctID;

        /// <summary>
        /// ��������
        /// </summary>
        private string deptID;


        // //  {4694CFAC-9041-496a-93C1-FAE7863E055E}
        /// <summary>
        /// ��ע2������
        /// </summary>
        private string memo2;


        /// <summary>
        /// �������
        /// </summary>
        private string supexamination;

        /// <summary>
        /// ������������
        /// </summary>
        private OperEnvironment caseOper;

        /// <summary>
        /// ��������
        /// </summary>
        private string emr_educational;

        /// <summary>
        /// ��������
        /// </summary>
        private string  educationcontent; 

        /// <summary>
        /// �������
        /// </summary>
        private string patientdiagnose; 

        /// <summary>
        /// ��ҩ֪ʶ
        /// </summary>
        private string medicationknowledge;

        /// <summary>
        /// ��ʳ֪ʶ
        /// </summary>
        private string diteknowledge;

        /// <summary>
        /// ����
        /// </summary>
        private string diseaseknowledge;

        /// <summary>
        /// ����Ч��
        /// </summary>
        private string educationaleffect;

        /// <summary>
        /// ��ͨ����
        /// </summary>
        private string trafficknowledge;  

        #endregion

        #endregion

        #region	����
        /// <summary>
        /// ����
        /// </summary>
        public string CaseMain
        {
            get
            {
                if (caseMain == null)
                {
                    caseMain = string.Empty;
                }
                return this.caseMain;
            }
            set
            {
                this.caseMain = value;
            }
        }

        /// <summary>
        /// �ֲ�ʷ
        /// </summary>
        public string CaseNow
        {
            get
            {
                if (caseNow == null)
                {
                    caseNow = string.Empty;
                }
                return this.caseNow;
            }
            set
            {
                this.caseNow = value;
            }
        }

        /// <summary>
        /// ����ʷ
        /// </summary>
        public string CaseOld
        {
            get
            {
                if (caseOld == null)
                {
                    caseOld = string.Empty;
                }
                return this.caseOld;
            }
            set
            {
                this.caseOld = value;
            }
        }

        /// <summary>
        /// ����ʷ
        /// </summary>
        public string CaseAllery
        {
            get
            {
                if (caseAllery == null)
                {
                    caseAllery = string.Empty;
                }
                return this.caseAllery;
            }
            set
            {
                this.caseAllery = value;
            }
        }

      //  {4694CFAC-9041-496a-93C1-FAE7863E055E}
        /// <summary>
        /// ��ע2,��ӱ�ע�ֶ�
        /// </summary>
        public string Memo2
        {
            get
            {
                if (memo2 == null)
                {
                    memo2 = string.Empty;
                }
                return this.memo2;
            }
            set
            {
                this.memo2 = value;
            }
        }


        /// <summary>
        /// �������
        /// </summary>
        public string SupExamination
        {
            get
            {
                if (supexamination == null)
                {
                    supexamination = string.Empty;
                }
                return this.supexamination;
            }
            set
            {
                this.supexamination = value;
            }
        
        }
       


        /// <summary>
        /// ����
        /// </summary>
        public string CheckBody
        {
            get
            {
                if (checkBody == null)
                {
                    checkBody = string.Empty;
                }
                return this.checkBody;
            }
            set
            {
                this.checkBody = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public string CaseDiag
        {
            get
            {
                if (caseDiag == null)
                {
                    caseDiag = string.Empty;
                }
                return this.caseDiag;
            }
            set
            {
                this.caseDiag = value;
            }
        }

        /// <summary>
        /// �Ƿ��Ⱦ
        /// </summary>
        public bool IsInfect
        {
            get
            {
                return this.isInfect;
            }
            set
            {
                this.isInfect = value;
            }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsAllery
        {
            get
            {
                return this.isAllery;
            }
            set
            {
                this.isAllery = value;
            }
        }

        /// <summary>
        /// ��� 1������ 2������
        /// </summary>
        public string ModuleType
        {
            get
            {
                if (moduletype == null)
                {
                    moduletype = string.Empty;
                }
                return this.moduletype;
            }
            set
            {
                this.moduletype = value;
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public string DoctID
        {
            get
            {
                if (doctID == null)
                {
                    doctID = string.Empty;
                }
                return this.doctID;
            }
            set
            {
                this.doctID = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string DeptID
        {
            get
            {
                if (deptID == null)
                {
                    deptID = string.Empty;
                }
                return this.deptID;
            }
            set
            {
                this.deptID = value;
            }
        }

        /// <summary>
        /// �����������
        /// </summary>
        public OperEnvironment CaseOper
        {
            get
            {
                if (this.caseOper == null)
                {
                    this.caseOper = new OperEnvironment();
                }
                return this.caseOper;
            }
            set
            {
                this.caseOper = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string Emr_Educational
        {
            get
            {
                if (emr_educational == null)
                {
                    emr_educational = string.Empty;
                }
                return this.emr_educational;
            }
            set
            {
                this.emr_educational = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string EducationContent
        {
            get
            {
                if (educationcontent == null)
                {
                    educationcontent = string.Empty;
                }
                return this.educationcontent;
            }
            set
            {
                this.educationcontent = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>

        public string PatientDiagnose
        {
            get
            {
                if (patientdiagnose == null)
                {
                    patientdiagnose = string.Empty;
                }
                return this.patientdiagnose;
            }
            set
            {
                this.patientdiagnose = value;
            }
        }

        /// <summary>
        /// ��ҩ֪ʶ
        /// </summary>
        public string MedicationKnowledge
        {
            get
            {
                if (medicationknowledge == null)
                {
                    medicationknowledge = string.Empty;
                }
                return this.medicationknowledge;
            }
            set
            {
                this.medicationknowledge = value;
            }
        }

        /// <summary>
        /// ��ʳ֪ʶ
        /// </summary>
        public string DiteKnowledge
        {
            get
            {
                if (diteknowledge == null)
                {
                    diteknowledge = string.Empty;
                }
                return this.diteknowledge;
            }
            set
            {
                this.diteknowledge = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string DiseaseKnowledge
        {
            get
            {
                if (diseaseknowledge == null)
                {
                    diseaseknowledge = string.Empty;
                }
                return this.diseaseknowledge;
            }
            set
            {
                this.diseaseknowledge = value;
            }
        }

        /// <summary>
        /// ����Ч��
        /// </summary>
        public string EducationalEffect
        {
            get
            {
                if (educationaleffect == null)
                {
                    educationaleffect = string.Empty;
                }
                return this.educationaleffect;
            }
            set
            {
                this.educationaleffect = value;
            }
        }

        /// <summary>
        /// ��ͨ����
        /// </summary>
        public string TrafficKnowledge
        {
            get
            {
                if (trafficknowledge == null)
                {
                    trafficknowledge = string.Empty;
                }
                return this.trafficknowledge;
            }
            set
            {
                this.trafficknowledge = value;
            }
        }


        #endregion

        #region ����

        #region	��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new ClinicCaseHistory Clone()
        {
            ClinicCaseHistory obj = this.MemberwiseClone() as ClinicCaseHistory;
            return obj;
        }

        #endregion

        #endregion

    }
}
