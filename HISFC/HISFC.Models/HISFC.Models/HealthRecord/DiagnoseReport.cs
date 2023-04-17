using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// DiagnoseReport ��ժҪ˵����
    /// ��Ⱦ�����濨��
    /// </summary>
    [Serializable]
    public class DiagnoseReport : FS.HISFC.Models.RADT.Patient
    {
        public DiagnoseReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region  ˽�б���
        //������
        private string reportNO;
        //��������
        private string beginDate;
        //�������
        private string diagnoseDate;
        //��������
        private string deadDate;
        //����״̬
        private string state;
        //��������
        private string diseaseType;
        //��������
        private FS.FrameWork.Models.NeuObject diseaseName;
        //��������
        private string cureDate;
        //����ȥ��
        private string patientGoing;
        //�Ǽ�ҽ��
        private FS.FrameWork.Models.NeuObject registerCode;
        //�Ǽǿ���
        private FS.FrameWork.Models.NeuObject registerDept;
        //�Ǽ�ʱ��
        private string registerDate;
        //�����
        private FS.FrameWork.Models.NeuObject auditCode;
        //���ʱ��
        private string auditDate;
        //������
        private FS.FrameWork.Models.NeuObject cancelCode;
        //����ʱ��
        private string cancelDate;
        //���ҽ��
        private FS.FrameWork.Models.NeuObject diagnoseCode;
        //���Ҳ���Ա
        private FS.FrameWork.Models.NeuObject deptOperCode;
        //���Ҳ���ʱ��
        private string deptOperDate;
        //סַ����
        private FS.FrameWork.Models.NeuObject addCode;
        //ְҵ
        private FS.FrameWork.Models.NeuObject profession;
        //��ϸסֵ
        private FS.FrameWork.Models.NeuObject addHome;
        #endregion

        #region  ���б���
        //������
        public string ReportNo
        {
            get
            {
                return reportNO;
            }
            set
            {
                reportNO = value;
            }
        }
        //��������
        public string BeginDate
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
        //�������
        public string DiagnoseDate
        {
            get
            {
                return diagnoseDate;
            }
            set
            {
                diagnoseDate = value;
            }
        }
        //��������
        public string DeadDate
        {
            get
            {
                return deadDate;
            }
            set
            {
                deadDate = value;
            }
        }
        //����״̬
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        //��������
        public string DiseaseType
        {
            get
            {
                return diseaseType;
            }
            set
            {
                diseaseType = value;
            }
        }
        //��������
        public FS.FrameWork.Models.NeuObject DiseaseName
        {
            get
            {
                return diseaseName;
            }
            set
            {
                diseaseName = value;
            }
        }
        //��������
        public string CureDate
        {
            get
            {
                return cureDate;
            }
            set
            {
                cureDate = value;
            }
        }
        //����ȥ��
        public string PatientGoing
        {
            get
            {
                return patientGoing;
            }
            set
            {
                patientGoing = value;
            }
        }
        //�Ǽ�ҽ��
        public FS.FrameWork.Models.NeuObject RegisterCode
        {
            get
            {
                return registerCode;
            }
            set
            {
                registerCode = value;
            }
        }
        //�Ǽǿ���
        public FS.FrameWork.Models.NeuObject RegisterDept
        {
            get
            {
                return registerDept;
            }
            set
            {
                registerDept = value;
            }
        }
        //�Ǽ�ʱ��
        public string RegisterDate
        {
            get
            {
                return registerDate;
            }
            set
            {
                registerDate = value;
            }
        }
        //�����
        public FS.FrameWork.Models.NeuObject AuditCode
        {
            get
            {
                return auditCode;
            }
            set
            {
                auditCode = value;
            }
        }
        //���ʱ��
        public string AuditDate
        {
            get
            {
                return auditDate;
            }
            set
            {
                auditDate = value;
            }
        }
        //������
        public FS.FrameWork.Models.NeuObject CancelCode
        {
            get
            {
                return cancelCode;
            }
            set
            {
                cancelCode = value;
            }
        }
        //����ʱ��
        public string CancelDate
        {
            get
            {
                return cancelDate;
            }
            set
            {
                cancelDate = value;
            }
        }
        //���ҽ��
        public FS.FrameWork.Models.NeuObject DiagnoseDoc
        {
            get
            {
                return diagnoseCode;
            }
            set
            {
                diagnoseCode = value;
            }
        }
        //���Ҳ���Ա
        public FS.FrameWork.Models.NeuObject DeptOper
        {
            get
            {
                return deptOperCode;
            }
            set
            {
                deptOperCode = value;
            }
        }
        //���Ҳ���ʱ��
        public string DeptOperDate
        {
            get
            {
                return deptOperDate;
            }
            set
            {
                deptOperDate = value;
            }
        }
        //סַ����
        public FS.FrameWork.Models.NeuObject AddCode
        {
            get
            {
                return addCode;
            }
            set
            {
                addCode = value;
            }
        }
        //ְҵ
        public FS.FrameWork.Models.NeuObject Profession
        {
            get
            {
                return profession;
            }
            set
            {
                profession = value;
            }
        }
        //��ϸסֵ
        public FS.FrameWork.Models.NeuObject AddHome
        {
            get
            {
                return addHome;
            }
            set
            {
                addHome = value;
            }
        }
        #endregion

        #region ����

        //סԺ��ˮ��
        [Obsolete("���� ���ü̳д���", true)]
        public string inpatientNo = "";
        //��������
        [Obsolete("���� �ü̳д���", true)]
        public string patientName = "";
        //�ҳ�����
        [Obsolete("�������ü̳д���", true)]
        public string parentName = "";
        //����Ա
        [Obsolete("��������OperInfo����", true)]
        public string operCode = "";
        //����ʱ��
        [Obsolete("��������OperInfo����", true)]
        public string operDate = "";
        #endregion
    }
}
