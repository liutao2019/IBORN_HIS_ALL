using System;

namespace FS.HISFC.DCP.Object
{
    /// <summary>
    /// InfectAddtionReport<br></br>
    /// [��������: InfectAddtionReport]<br></br>
    /// [�� �� ��: zengft]<br></br>
    /// [����ʱ��: 2007-12-8]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class SexReport : FS.FrameWork.Models.NeuObject
    {
        #region ˽���ֶ�

        /*
        Item marriage = new Item();
		Item nation = new Item();
		Item education = new Item();
		Item houseHoldRegion = new Item();
		Address houseHoldAddress;
		string houseHoldName;
		string contactHistory ;
		Item sexHistory = new Item();
		Item possiableInfWay = new Item();
		Item labelSource = new Item();
		Item libResult = new Item();
		DateTime libDate = DateTime.MinValue;
		string libDeptName;
		ArrayList alChild = new ArrayList();
        AdditionOther shayanAdditon = new AdditionOther();
        */


        /// <summary>
        /// ����סԺ�Ż�����
        /// </summary>
        private string patientNo = "";

        /// <summary>
        /// ����װ��[0 δ�� 1�ѻ� 2ͬ�� 3�����ɥż 4����]
        /// </summary>
        private FS.FrameWork.Models.NeuObject marigerStatus = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject nation = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// �Ļ��̶�
        /// </summary>
        private FS.FrameWork.Models.NeuObject education = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �������ڵ�
        /// </summary>
        private FS.FrameWork.Models.NeuObject houseHoldRegion = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������ַ
        /// </summary>
        private FS.FrameWork.Models.NeuObject houseHold = new FS.FrameWork.Models.NeuObject(); 

        private FS.FrameWork.Models.NeuObject contactHistory = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject sexHistory = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject possiableInfWay = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject labelSource = new FS.FrameWork.Models.NeuObject();
        
        private FS.FrameWork.Models.NeuObject libResult = new FS.FrameWork.Models.NeuObject();
        DateTime libDate = DateTime.MinValue;
        private FS.FrameWork.Models.NeuObject libDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid = true;

        #endregion

        #region �����ֶ�

        /// <summary>
        /// ����סԺ�Ż�����
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
        /// ����װ��[0 δ�� 1�ѻ� 2ͬ�� 3�����ɥż 4����]
        /// </summary>
        public FS.FrameWork.Models.NeuObject MarigerStatus        {
            get
            {
                return this.marigerStatus;
            }
            set
            {
                this.marigerStatus = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Nation
        {
            get
            {
                return this.nation;
            }
            set
            {
                this.nation = value;
            }
        }
        /// <summary>
        /// �Ļ��̶�
        /// </summary>
        public FS.FrameWork.Models.NeuObject Education
        {
            get
            {
                return this.education;
            }
            set
            {
                this.education = value;
            }
        }

        /// <summary>
        /// �������ڵ�
        /// </summary>
        public FS.FrameWork.Models.NeuObject HouseHoldRegion
        {
            get
            {
                return this.houseHoldRegion;
            }
            set
            {
                this.houseHoldRegion = value;
            }
        }

        /// <summary>
        /// ������ַ
        /// </summary>
        public FS.FrameWork.Models.NeuObject HouseHold
        {
            get
            {
                return this.houseHold;
            }
            set
            {
                this.houseHold = value;
            }
        }

        public FS.FrameWork.Models.NeuObject ContactHistory
        {
            get
            {
                return this.contactHistory;
            }
            set
            {
                this.contactHistory = value;
            }
        }
        public FS.FrameWork.Models.NeuObject SexHistory
        {
            get
            {
                return this.sexHistory;
            }
            set
            {
                this.sexHistory = value;
            }
        }
        public FS.FrameWork.Models.NeuObject PossiableInfWay
        {
            get
            {
                return this.possiableInfWay;
            }
            set
            {
                this.possiableInfWay = value;
            }
        }
        public FS.FrameWork.Models.NeuObject LabelSource
        {
            get
            {
                return this.labelSource;
            }
            set
            {
                this.labelSource = value;
            }
        }

        public FS.FrameWork.Models.NeuObject LibResult
        {
            get
            {
                return this.libResult;
            }
            set
            {
                this.libResult = value;
            }
        }
        public DateTime LibDate
        {
            get
            {
                return this.libDate;
            }
            set
            {
                this.libDate = value;
            }
        }
        public FS.FrameWork.Models.NeuObject LibDept
        {
            get
            {
                return this.libDept;
            }
            set
            {
                this.libDept = value;
            }
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
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

        #endregion

        public SexReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ��¡����
        public SexReport Clone()
        {
            SexReport sexReport = new SexReport();
            sexReport.contactHistory.Clone();
            sexReport.education.Clone();
            sexReport.houseHoldRegion.Clone();
            sexReport.labelSource.Clone();
            sexReport.libDept.Clone();
            sexReport.libResult.Clone();
            sexReport.marigerStatus.Clone();
            sexReport.nation.Clone();
            sexReport.possiableInfWay.Clone();
            sexReport.sexHistory.Clone();
            
            return sexReport;
        }
        #endregion
    }
}
