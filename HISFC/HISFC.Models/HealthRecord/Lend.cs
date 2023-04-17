using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// Lend ��ժҪ˵������������ʵ�� ID ¼��������Ա���� Name ¼��������Ա����
    /// </summary>
    [Serializable]
    public class Lend : FS.FrameWork.Models.NeuObject
    {
        public Lend()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ˽�б���

        private Base caseBase = new Base();
        private FS.FrameWork.Models.NeuObject employeeInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject employeeDept = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject returnOperInfo = new FS.FrameWork.Models.NeuObject();
        private string seqNO;
        private DateTime lendDate;
        private DateTime prerDate;
        private string lendKind;
        private string lendStus;
        private DateTime returnDate;
        private string cardNo;
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();


        private string patientType = "";
        private string lendNum = "";

        #endregion

        #region ����
        /// <summary>
        /// �������
        /// </summary>
        public string SeqNO
        {
            get
            {
                return seqNO;
            }
            set
            {
                seqNO = value;
            }
        }
        /// <summary>
        /// ���� 
        /// </summary>
        public string CardNO
        {
            set
            {
                cardNo = value;
            }
            get
            {
                return cardNo;
            }
        }

        /// <summary>
        /// ���˻�����Ϣ
        /// </summary>
        public Base CaseBase
        {
            get
            {
                return caseBase;
            }
            set
            {
                caseBase = value;
            }
        }

        /// <summary>
        /// ��������Ϣ ID �����˱�� Name ����������
        /// </summary>
        public FS.FrameWork.Models.NeuObject EmployeeInfo
        {
            get
            {
                return employeeInfo;
            }
            set
            {
                employeeInfo = value;
            }
        }

        /// <summary>
        /// ���������ڿ�����Ϣ ID ���ұ�� Name ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject EmployeeDept
        {
            get
            {
                return employeeDept;
            }
            set
            {
                employeeDept = value;
            }
        }

        /// <summary>
        /// ��������(����)
        /// </summary>
        public DateTime LendDate
        {
            get
            {
                return lendDate;
            }
            set
            {
                lendDate = value;
            }
        }

        /// <summary>
        /// Ԥ���黹����(����)
        /// </summary>
        public DateTime PrerDate
        {
            get
            {
                return prerDate;
            }
            set
            {
                prerDate = value;
            }
        }

        /// <summary>
        /// ��������(����)
        /// </summary>
        public string LendKind
        {
            get
            {
                return lendKind;
            }
            set
            {
                lendKind = value;
            }
        }

        /// <summary>
        /// ����״̬ 1���/2���� 
        /// </summary>
        public string LendStus
        {
            get
            {
                return lendStus;
            }
            set
            {
                lendStus = value;
            }
        }

        /// <summary>
        ///�黹����Ա��Ϣ ID �黹����Ա���� Name �黹����Ա����
        /// </summary>
        public FS.FrameWork.Models.NeuObject ReturnOperInfo
        {
            get
            {
                return returnOperInfo;
            }
            set
            {
                returnOperInfo = value;
            }
        }

        /// <summary>
        /// �黹����
        /// </summary>
        public DateTime ReturnDate
        {
            get
            {
                return returnDate;
            }
            set
            {
                returnDate = value;
            }
        }
        /// <summary>
        /// ����Ա��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperInfo
        {
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }

  
        /// <summary>
        /// �������� 1���� 2סԺ
        /// </summary>
        public string PatientType
        {
            get
            {
                return patientType;
            }
            set
            {
                patientType = value;
            }
        }


        /// <summary>
        /// ���ķ���
        /// </summary>
        public string LendNum
        {
            get
            {
                return lendNum;
            }
            set
            {
                lendNum = value;
            }
        }       

        #endregion

        #region ���ú���
        public new Lend Clone()
        {
            Lend LendClone = base.MemberwiseClone() as Lend;

            LendClone.caseBase = this.caseBase.Clone();
            LendClone.EmployeeInfo = this.employeeInfo.Clone(); ;
            LendClone.employeeDept = this.employeeDept.Clone();
            LendClone.ReturnOperInfo = this.ReturnOperInfo.Clone();
            LendClone.operInfo = operInfo.Clone();
            return LendClone;
        }
        #endregion

        #region ����
        /// <summary>
        /// ���˻�����Ϣ
        /// </summary>
        [Obsolete("���� ��CaseBase", true)]
        public Base PatientInfo
        {
            get
            {
                return caseBase;
            }
            set
            {
                caseBase = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        [Obsolete("���� ��OperInfo.OperTime", true)]
        public DateTime OperDate
        {
            get
            {
                return System.DateTime.Now;
            }
        }

        /// <summary>
        /// ��������Ϣ ID �����˱�� Name ����������
        /// </summary>
        [Obsolete("���� �� EmployeeInfo ����", true)]
        public FS.FrameWork.Models.NeuObject EmplInfo
        {
            get
            {
                return employeeInfo;
            }
            set
            {
                employeeInfo = value;
            }
        }

        /// <summary>
        /// ���������ڿ�����Ϣ ID ���ұ�� Name ��������
        /// </summary>
        [Obsolete("���� �� EmployeeDept ����", true)]
        public FS.FrameWork.Models.NeuObject EmplDeptInfo
        {
            get
            {
                return employeeDept;
            }
            set
            {
                employeeDept = value;
            }
        }
        #endregion
    }
}
