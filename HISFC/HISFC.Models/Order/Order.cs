using System;
//using FS.NFC;
using FS.HISFC;
using FS.FrameWork.Models;
namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.Order<br></br>
	/// [��������: ҽ������ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Order : FS.FrameWork.Models.NeuObject,
        FS.HISFC.Models.Base.IDept,
        FS.HISFC.Models.Base.IBaby, FS.HISFC.Models.Base.ISort
    {

        /// <summary>
        /// ҽ������ʵ��
        /// ID ҽ����ˮ��
        /// </summary>
        public Order()
        {

        }

        #region ����

        #region ˽��

        //{23F37636-DC34-44a3-A13B-071376265450}
        /// <summary>
        /// Ժ��id
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// Ժ����
        /// </summary>
        private string hospital_name;

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient;

        /// <summary>
        /// 0 ����Ƥ�ԣ�1 ���ԣ�2 ��Ƥ�ԣ�3 Ƥ������[+]��4 Ƥ������[-]
        /// </summary>
        private EnumHypoTest hypoTest;

        /// <summary>
        /// Ժ��ע�����
        /// </summary>
        private int injectCount;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject doctor;

        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject doctorDept;

        /// <summary>
        /// ���/ִ�л�ʿ
        /// </summary>
        private FS.FrameWork.Models.NeuObject nurse;

        /// <summary>
        /// ¼����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper;


        /// <summary>
        /// ֹͣ��
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment dcOper;


        /// <summary>
        /// ִ���߻�ʿ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment execOper;


        /// <summary>
        /// ֹͣԭ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject dcReason;

        /// <summary>
        /// ������ҩ��˽��
        /// </summary>
        private string passCheckResults;

        /// <summary>
        /// ҽ������
        /// </summary>
        private string orderType;

        /// <summary>
        /// ������ҩ��˽��
        /// </summary>
        public string PassCheckResults
        {
            get
            {
                if (passCheckResults == null)
                {
                    passCheckResults = string.Empty;
                }
                return passCheckResults;
            }
            set
            {
                passCheckResults = value;
            }
        }

        /// <summary>
        /// ҩƷ��Ŀ/��ҩƷ��Ŀ
        /// </summary>
        private FS.HISFC.Models.Base.Item item;


        /// <summary>
        /// ҽ������ʱ��
        /// </summary>
        private DateTime dtMOTime;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginTime;


        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// ���ηֽ�ʱ��
        /// </summary>
        private DateTime curMOTime;

        /// <summary>
        /// �´ηֽ�ʱ��
        /// </summary>
        private DateTime nextMOTime;

        /// <summary>
        /// ���ʱ��
        /// </summary>
        private DateTime confirmTime;

        /// <summary>
        /// ״̬��0������ 1����� 2 ִ�� 3 ���ϣ�
        /// </summary>
        private int status;

        /// <summary>
        /// �����Ϣ
        /// </summary>
        private Combo combo =null;// new Combo();

        /// <summary>
        /// �÷�
        /// </summary>
        private FS.FrameWork.Models.NeuObject usage;//new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ÿ�μ���
        /// </summary>
        private decimal doseOnce;

        /// <summary>
        /// ÿ�μ�����λ
        /// </summary>
        private string doseUnit;

        /// <summary>
        /// Ƶ��
        /// </summary>
        private Frequency frequency =null;// new Frequency();


        /// <summary>
        /// ��ҩ����
        /// </summary>
        private decimal herbalQty;

        /// <summary>
        /// ������λ
        /// </summary>
        private string unit;

        /// <summary>
        /// ʹ������
        /// </summary>
        private int usetimes;

        /// <summary>
        /// ִ��״̬
        /// </summary>
        private int execStatus;

        /// <summary>
        /// ��ʱҽ��ִ��ʱ��ʹ��
        /// ��ע1
        /// </summary>
        [Obsolete("�����ƿ�õ���������", true)]
        private string mark1;

        /// <summary>
        /// ��ע2
        /// </summary>
        [Obsolete("��������ǩ���ģ�������", true)]
        private string mark2;

        /// <summary>
        /// סԺ�洢 ����ҽ��  ԭҽ����ˮ��
        /// </summary>
        private string reTidyInfo;

        /// <summary>
        /// ��鲿λ��¼
        /// </summary>
        private string checkPartRecord;

        /// <summary>
        /// ��ע��Ϣ �磺�ȵ����÷� ƽƬλ�� ҩƷ����ע������
        /// </summary>
        private string note;

        /// <summary>
        /// ������
        /// </summary>
        private string recipeNO;

        /// <summary>
        /// ������ˮ���
        /// </summary>
        private int sequenceNO;

        /// <summary>
        /// �ͼ�����
        /// </summary>
        private FS.FrameWork.Models.NeuObject sample;

        /// <summary>
        /// ������Ŀ������Ϣ ID ������Ŀ������� NAME ������Ŀ��������
        /// </summary>
        private FS.HISFC.Models.Base.Item package;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private NeuObject reciptDoctor;

        /// <summary>
        /// <br>�Ӽ�</br>
        /// </summary>
        private bool isEmergency;

        /// <summary>
        /// �Ƿ񸽲�
        /// </summary>
        private bool isSubtbl;

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        private bool isHaveSubtbl;

        /// <summary>
        /// �Ƿ��ҩ�����
        /// </summary>
        private bool isStock;

        /// <summary>
        /// �Ƿ���ҽ������ͬ����ҩ
        /// </summary>
        private bool isPermission;

        /// <summary>
        /// ����Ƶ�Σ�ִ�м���
        /// ִ��ʱ�����Ƶ��ִ��ʱ��������д���
        /// </summary>
        private string execDose = "";

        /// <summary>
        /// ��Һ��Ϣ
        /// </summary>
        private Compound compound;

        //{E1902932-1839-4a92-8A6A-E42F448FA27F}
        /// <summary>
        /// ���뵥��
        /// </summary>
        private string applyNo;

        /// <summary>
        /// ��� {5BC77A1A-C3BB-4117-8791-4D4E664DC63E} ������� houwb
        /// </summary>
        private int subCombNO;

        /// <summary>
        /// ��С��λ��ǣ�1 ������λΪ��С��λ��0 ������λΪ������λ��
        /// </summary>
        private string minunitFlag;

        /// <summary>
        /// ��С��λ��ǣ�1 ������λΪ��С��λ��0 ������λΪ������λ��
        /// </summary>
        public string MinunitFlag
        {
            get
            {
                if (minunitFlag == null)
                {
                    minunitFlag = string.Empty;
                }
                return minunitFlag;
            }
            set
            {
                minunitFlag = value;
            }
        }

         /// <summary>
        /// ������ʾ��ÿ�������洢������ʾ������ ��1/3
        /// </summary>
        private string doseOnceDisplay;

        /// <summary>
        /// ������ʾ��ÿ������λ��������ʾ�ĵ�λ ��Ƭ
        /// </summary>
        private string doseUnitDisplay;

        /// <summary>
        /// ������
        /// </summary>
        private string firstUseNum;

        /// <summary>
        /// {5C7887F1-A4D5-4a66-A814-18D45367443E}
        /// �˷ѱ�� 0-�������˷ѣ�1-�����˷�
        /// </summary>
        private int quitFlag;

        /// <summary>
        /// {5C7887F1-A4D5-4a66-A814-18D45367443E}
        /// �˷Ѳ�����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment quitOper;

        /// <summary>
        /// ҽ���˷�ԭ��ҽ�������˷ѵģ�
        /// </summary>
        private string refundReason;

        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        protected FS.FrameWork.Models.NeuObject reciptDept;

        /// <summary>
        /// ִ�п���
        /// </summary>
        protected FS.FrameWork.Models.NeuObject ExecDept;

        /// <summary>
        /// ҩ������
        /// </summary>
        protected FS.FrameWork.Models.NeuObject DrugDept;

        /// <summary>
        /// ҽ��������ţ�ҽ������ͨ����ק����ҽ������λ�ã�
        /// </summary>
        protected int sortid;

        /// <summary>
        /// ����ҽ����־
        /// </summary>
        public bool Reorder;

        /// <summary>
        /// �Ƿ�Ӥ��
        /// </summary>
        protected bool bIsBaby;

        /// <summary>
        /// Ӥ�������Ϣ
        /// </summary>
        protected string strBabyNo;

        /// <summary>
        /// ҳ��
        /// </summary>
        public int PageNo = -1;

        /// <summary>
        /// �к�
        /// </summary>
        public int RowNo = -1;

        //{6B70B558-72C9-4DEF-874F-DABD0A9B5198}
        /// <summary>
        /// ����
        /// </summary>
        public string dripspreed;

        /// <summary>
        /// ����ҽ������ {FA143951-748B-4c45-9D1B-853A31B9E006}
        /// </summary>
        private string countrycode;

        /// <summary>
        /// ���߱�ע��Ϣ
        /// </summary>
        //private string patientMark;
        #endregion

        #endregion

        #region ����

        /// <summary>
        ///Ժ��id //{23F37636-DC34-44a3-A13B-071376265450}
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }


        /// <summary>
        /// Ժ����
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }

        /// <summary>
        /// ����Ƶ�Σ�ִ�м���
        /// ִ��ʱ�����Ƶ��ִ��ʱ��������д���
        /// </summary>
        public string ExecDose
        {
            get
            {
                if (execDose == null)
                {
                    execDose = string.Empty;
                }
                return execDose;
            }
            set
            {
                execDose = value;
            }
        }

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                if (patient == null)
                {
                    patient = new FS.HISFC.Models.RADT.PatientInfo();
                }
                return this.patient;
            }
            set
            {
                this.patient = value;
            }
        }

        /// <summary>
        /// 0 ����Ƥ�ԣ�1 ���ԣ�2 ��Ƥ�ԣ�3 Ƥ������[+]��4 Ƥ������[-]
        /// </summary>
        public EnumHypoTest HypoTest
        {
            get
            {
                if (hypoTest == null)
                {
                    hypoTest = EnumHypoTest.FreeHypoTest;
                }
                return this.hypoTest;
            }
            set
            {
                this.hypoTest = value;
            }
        }

        /// <summary>
        /// Ժ��ע�����
        /// </summary>
        public int InjectCount
        {
            get
            {
                return injectCount;
            }
            set
            {
                injectCount = value;
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        [Obsolete("���ϣ���ReciptDoctor", true)]
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
        /// ���/ִ�л�ʿ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Nurse
        {
            get
            {
                if (nurse == null)
                {
                    nurse = new NeuObject();
                }
                return this.nurse;
            }
            set
            {
                this.nurse = value;
            }
        }

        /// <summary>
        /// ¼����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                if (oper == null)
                {
                    oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }
        /// <summary>
        /// ֹͣ��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment DCOper
        {
            get
            {
                if (dcOper == null)
                {
                    dcOper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.dcOper;
            }
            set
            {
                this.dcOper = value;
            }
        }

        /// <summary>
        /// ִ����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment ExecOper
        {
            get
            {
                if (execOper == null)
                {
                    execOper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.execOper;
            }
            set
            {
                this.execOper = value;
            }
        }


        /// <summary>
        /// ֹͣԭ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject DcReason
        {
            get
            {
                if (dcReason == null)
                {
                    dcReason = new NeuObject();
                }
                return this.dcReason;
            }
            set
            {

                this.dcReason = value;
            }
        }

        #region ʱ��
        /// <summary>
        /// ҩƷ��Ŀ/��ҩƷ��Ŀ
        /// </summary>
        public FS.HISFC.Models.Base.Item Item
        {
            get
            {
                if (item == null)
                {
                    item = new FS.HISFC.Models.Base.Item();
                }

                return this.item;
            }
            set
            {
                this.item = value;
            }
        }
        /// <summary>
        /// ҽ������ʱ��
        /// </summary>
        public DateTime MOTime
        {
            get
            {
                return this.dtMOTime;
            }
            set
            {
                this.dtMOTime = value;
            }
        }

        /// <summary>
        /// ��ʼʱ��
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
        /// ����ʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        /// <summary>
        /// ���ηֽ�ʱ��
        /// </summary>
        public DateTime CurMOTime
        {
            get
            {
                return this.curMOTime;
            }
            set
            {
                this.curMOTime = value;
            }
        }

        /// <summary>
        /// �´ηֽ�ʱ��
        /// </summary>
        public DateTime NextMOTime
        {
            get
            {
                return this.nextMOTime;
            }
            set
            {
                this.nextMOTime = value;
            }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime ConfirmTime
        {
            get
            {
                return this.confirmTime;
            }
            set
            {
                this.confirmTime = value;
            }
        }

        #endregion

        /// <summary>
        /// ״̬��0������ 1����� 2 ִ�� 3 ���ϣ�
        /// </summary>
        public int Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        /// <summary>
        /// �����Ϣ
        /// </summary>
        public Combo Combo
        {
            get
            {
                if (combo == null)
                {
                    combo = new Combo();
                }
                return this.combo;
            }
            set
            {
                this.combo = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public int SubCombNO
        {
            get
            {
                return subCombNO;
            }
            set
            {
                subCombNO = value;
            }
        }

        /// <summary>
        /// �÷�
        /// </summary>
        public FS.FrameWork.Models.NeuObject Usage
        {
            get
            {
                if (usage == null)
                {
                    usage = new NeuObject();
                }
                return this.usage;
            }
            set
            {
                this.usage = value;
            }
        }

        /// <summary>
        /// ÿ�μ���
        /// </summary>
        public decimal DoseOnce
        {
            get
            {
                return this.doseOnce;
            }
            set
            {
                this.doseOnce = value;
            }
        }

        /// <summary>
        /// ÿ�μ�����λ
        /// </summary>
        public string DoseUnit
        {
            get
            {
                if (doseUnit == null)
                {
                    doseUnit = string.Empty;
                }
                return this.doseUnit;
            }
            set
            {
                this.doseUnit = value;
            }
        }

        /// <summary>
        /// Ƶ��
        /// </summary>
        public Frequency Frequency
        {
            get
            {
                if (frequency == null)
                {
                    frequency = new Frequency();
                }
                return this.frequency;
            }
            set
            {
                this.frequency = value;
            }
        }

        /// <summary>
        /// ����
        /// �շ�ʱ���ǲ�ҩ�� ==����*����
        /// </summary>
        public decimal Qty
        {
            get
            {
                return this.Item.Qty;
            }
            set
            {
                this.Item.Qty = value;
            }
        }

        /// <summary>
        /// ��ҩ����
        /// </summary>
        public decimal HerbalQty
        {
            get
            {
                return this.herbalQty;
            }
            set
            {
                this.herbalQty = value;
            }
        }

        /// <summary>
        /// ������λ
        /// </summary>
        public string Unit
        {
            get
            {
                if (unit == null)
                {
                    unit = string.Empty;
                }
                return this.unit;
            }
            set
            {
                this.unit = value;
            }
        }

        /// <summary>
        /// ʹ������
        /// </summary>
        public int Usetimes
        {
            get
            {
                return this.usetimes;
            }
            set
            {
                this.usetimes = value;
            }
        }

        /// <summary>
        /// ִ��״̬
        /// </summary>
        public int ExecStatus
        {
            get
            {
                return this.execStatus;
            }
            set
            {
                this.execStatus = value;
            }
        }

        /// <summary>
        /// ��ʱҽ��ִ��ʱ��ʹ�ã����������ƿ����  ����������
        /// </summary>
        [Obsolete("�����ƿ�õ���������", false)]
        public string ExtendFlag1
        {
            get
            {
                if (mark1 == null)
                {
                    mark1 = string.Empty;
                }
                return this.mark1;
            }
            set
            {
                this.mark1 = value;
            }
        }

        /// <summary>
        /// ��ע2
        /// </summary>
        [Obsolete("��������ǩ���ģ�������", false)]
        public string ExtendFlag2
        {
            get
            {
                if (mark2 == null)
                {
                    mark2 = string.Empty;
                }
                return this.mark2;
            }
            set
            {
                this.mark2 = value;
            }
        }

        /// <summary>
        /// סԺ�洢 ����ҽ��  ԭҽ����ˮ��
        /// </summary>
        [Obsolete("����ReTidyInfo", true)]
        public string ExtendFlag3
        {
            get
            {
                if (reTidyInfo == null)
                {
                    reTidyInfo = string.Empty;
                }
                return this.reTidyInfo;
            }
            set
            {
                this.reTidyInfo = value;
            }
        }

        /// <summary>
        /// סԺ�洢 ����ҽ��  ԭҽ����ˮ��
        /// </summary>
        public string ReTidyInfo
        {
            get
            {
                if (reTidyInfo == null)
                {
                    reTidyInfo = string.Empty;
                }
                return this.reTidyInfo;
            }
            set
            {
                this.reTidyInfo = value;
            }
        }

        /// <summary>
        /// ��鲿λ��¼
        /// </summary>
        public string CheckPartRecord
        {
            get
            {
                if (checkPartRecord == null)
                {
                    checkPartRecord = string.Empty;
                }
                return this.checkPartRecord;
            }
            set
            {
                this.checkPartRecord = value;
            }
        }

        /// <summary>
        /// ��ע��Ϣ �磺�ȵ����÷� ƽƬλ�� ҩƷ����ע������
        /// </summary>
        public string Note
        {
            get
            {
                if (note == null)
                {
                    note = string.Empty;
                }
                return this.note;
            }
            set
            {
                this.note = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string ReciptNO
        {
            get
            {
                if (recipeNO == null)
                {
                    recipeNO = string.Empty;
                }
                return this.recipeNO;
            }
            set
            {
                this.recipeNO = value;
            }
        }

        /// <summary>
        /// ������ˮ���
        /// </summary>
        public int SequenceNO
        {
            get
            {
                return this.sequenceNO;
            }
            set
            {
                this.sequenceNO = value;
            }
        }

        /// <summary>
        /// �ͼ�����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Sample
        {
            get
            {
                if (sample == null)
                {
                    sample = new NeuObject();
                }
                return this.sample;
            }
            set
            {
                this.sample = value;
            }
        }

        /// <summary>
        /// ������Ŀ������Ϣ ID ������Ŀ������� NAME ������Ŀ��������
        /// </summary>
        public FS.HISFC.Models.Base.Item Package
        {
            get
            {
                if (package == null)
                {
                    package = new FS.HISFC.Models.Base.Item();
                }
                return this.package;
            }
            set
            {
                this.package = value;
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public NeuObject ReciptDoctor
        {
            get
            {
                if (reciptDoctor == null)
                {
                    reciptDoctor = new NeuObject();
                }
                return this.reciptDoctor;
            }
            set
            {
                this.reciptDoctor = value;
            }
        }

        #region ��־

        /// <summary>
        /// <br>�Ӽ�</br>
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
        /// �Ƿ񸽲�
        /// </summary>
        public bool IsSubtbl
        {
            get
            {
                return this.isSubtbl;
            }
            set
            {
                this.isSubtbl = value;
            }
        }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsHaveSubtbl
        {
            get
            {
                return this.isHaveSubtbl;
            }
            set
            {
                this.isHaveSubtbl = value;
            }
        }

        /// <summary>
        /// �Ƿ��ҩ�����
        /// </summary>
        public bool IsStock
        {
            get
            {
                return this.isStock;
            }
            set
            {
                this.isStock = value;
            }
        }

        /// <summary>
        /// �Ƿ���ҽ������ͬ����ҩ
        /// </summary>
        public bool IsPermission
        {
            get
            {
                return this.isPermission;
            }
            set
            {
                this.isPermission = value;
            }
        }

        #endregion

        /// <summary>
        /// �Ƿ���Һ
        /// </summary>
        public Compound Compound
        {
            get
            {
                if (compound == null)
                {
                    compound = new Compound();
                }

                return this.compound;
            }
            set
            {
                this.compound = value;
            }
        }


        /// <summary>
        /// ���뵥��
        /// </summary>
        public string ApplyNo
        {
            get
            {
                if (applyNo == null)
                {
                    applyNo = string.Empty;
                }
                return applyNo;
            }
            set
            {
                applyNo = value;
            }
        }

        /// <summary>
        /// ������ʾ��ÿ�������洢������ʾ������ ��1/3
        /// </summary>
        [Obsolete("����DoseOnceDisplay", true)]
        public string ExtendFlag4
        {
            get
            {
                if (doseOnceDisplay == null)
                {
                    doseOnceDisplay = string.Empty;
                }
                return this.doseOnceDisplay;
            }
            set
            {
                this.doseOnceDisplay = value;
            }
        }

        /// <summary>
        /// ������ʾ��ÿ�������洢������ʾ������ ��1/3
        /// </summary>
        public string DoseOnceDisplay
        {
            get
            {
                if (doseOnceDisplay == null)
                {
                    doseOnceDisplay = string.Empty;
                }
                return this.doseOnceDisplay;
            }
            set
            {
                this.doseOnceDisplay = value;
            }
        }

        /// <summary>
        /// ������ʾ��ÿ������λ��������ʾ�ĵ�λ ��Ƭ
        /// </summary>
        [Obsolete("����DoseUnitDisplay", true)]
        public string ExtendFlag5
        {
            get
            {
                if (doseUnitDisplay == null)
                {
                    doseUnitDisplay = string.Empty;
                }
                return this.doseUnitDisplay;
            }
            set
            {
                this.doseUnitDisplay = value;
            }
        }

        /// <summary>
        /// ������ʾ��ÿ������λ��������ʾ�ĵ�λ ��Ƭ
        /// </summary>
        public string DoseUnitDisplay
        {
            get
            {
                if (doseUnitDisplay == null)
                {
                    doseUnitDisplay = string.Empty;
                }
                return this.doseUnitDisplay;
            }
            set
            {
                this.doseUnitDisplay = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        [Obsolete("����FirstUseNum", true)]
        public string ExtendFlag6
        {
            get
            {
                if (firstUseNum == null)
                {
                    firstUseNum = string.Empty;
                }
                return this.firstUseNum;
            }
            set
            {
                this.firstUseNum = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string FirstUseNum
        {
            get
            {
                if (firstUseNum == null)
                {
                    firstUseNum = string.Empty;
                }
                return this.firstUseNum;
            }
            set
            {
                this.firstUseNum = value;
            }
        }

        /// <summary>
        /// ҽ������
        /// </summary>
        public string OrderType
        {
            get
            {
                if (orderType == null)
                {
                    orderType = string.Empty;
                }
                return orderType;
            }
            set
            {
                orderType = value;
            }
        }

        /// <summary>
        /// {5C7887F1-A4D5-4a66-A814-18D45367443E}
        /// �˷ѱ�� 0-�������˷ѣ�1-�����˷�
        /// </summary>
        public int QuitFlag
        {
            get { return this.quitFlag; }
            set { this.quitFlag = value; }
        }

        /// <summary>
        /// {5C7887F1-A4D5-4a66-A814-18D45367443E}
        /// �˷Ѳ�����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment QuitOper
        {
            get
            {
                if (quitOper == null)
                {
                    quitOper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.quitOper;
            }
            set
            {
                this.quitOper = value;
            }
        }

        public string RefundReason
        {
            get { return this.refundReason; }
            set { this.refundReason = value; }
        }

        #endregion

        #region �ӿ�ʵ��

        #region IDept ��Ա
        /// <summary>
        /// ������Ժ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject InDept
        {
            get
            {
                // TODO:  ��� Order.InDept getter ʵ��
                return this.Patient.PVisit.PatientLocation.Dept;
            }
            set
            {
                // TODO:  ��� Order.InDept setter ʵ��
                this.Patient.PVisit.PatientLocation.Dept = value;
            }
        }

        /// <summary>
        /// ִ�п���
        /// </summary>
        public FS.FrameWork.Models.NeuObject ExeDept
        {
            get
            {
                if (ExecDept == null)
                {
                    ExecDept = new NeuObject();
                }

                // TODO:  ��� Order.ExeDept getter ʵ��
                return this.ExecDept;
            }
            set
            {
                // TODO:  ��� Order.ExeDept setter ʵ��
                this.ExecDept = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ReciptDept
        {
            get
            {
                if (reciptDept == null)
                {
                    reciptDept = new NeuObject();
                }
                // TODO:  ��� Order.ReciptDept getter ʵ��
                return this.reciptDept;
            }
            set
            {
                // TODO:  ��� Order.ReciptDept setter ʵ��
                this.reciptDept = value;
            }
        }

        /// <summary>
        /// ����ִ�л�ʿվ
        /// </summary>
        public FS.FrameWork.Models.NeuObject NurseStation
        {
            get
            {
                // TODO:  ��� Order.NurseStation getter ʵ��
                return this.Patient.PVisit.PatientLocation.NurseCell;
            }
            set
            {
                // TODO:  ��� Order.NurseStation setter ʵ��
                this.Patient.PVisit.PatientLocation.NurseCell = value;
            }
        }

        /// <summary>
        /// �ۿ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject StockDept
        {
            get
            {
                if (DrugDept == null)
                {
                    DrugDept = new NeuObject();
                }
                // TODO:  ��� Order.StockDept getter ʵ��
                return this.DrugDept;
            }
            set
            {
                // TODO:  ��� Order.StockDept setter ʵ��
                this.DrugDept = value;
            }
        }

        /// <summary>
        /// ����ҽ�����ڿ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject DoctorDept
        {
            get
            {
                if (doctorDept == null)
                {
                    doctorDept = new NeuObject();
                }

                // TODO:  ��� Order.ReciptDoct getter ʵ��
                return this.doctorDept;
            }
            set
            {
                // TODO:  ��� Order.ReciptDoct setter ʵ��
                this.doctorDept = value;
            }
        }

        #endregion

        #region IBaby ��Ա
        /// <summary>
        /// Ӥ�����
        /// </summary>
        public string BabyNO
        {
            get
            {
                // TODO:  ��� Order.FS.HISFC.Models.Base.IBaby.BabyNo getter ʵ��
                if (strBabyNo == null)
                {
                    this.strBabyNo = "0";
                }
                return this.strBabyNo;
            }
            set
            {
                // TODO:  ��� Order.FS.HISFC.Models.Base.IBaby.BabyNo setter ʵ��
                strBabyNo = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ӥ��
        /// </summary>
        public bool IsBaby
        {
            get
            {
                // TODO:  ��� Order.FS.HISFC.Models.Base.IBaby.IsBaby getter ʵ��
                return this.bIsBaby;
            }
            set
            {
                // TODO:  ��� Order.FS.HISFC.Models.Base.IBaby.IsBaby setter ʵ��
                this.bIsBaby = value;
            }
        }

        //{6B70B558-72C9-4DEF-874F-DABD0A9B5198}

        /// <summary>
        /// ����
        /// </summary>
        public string Dripspreed
        {
            get
            {
                // TODO:  ��� Order.FS.HISFC.Models.Base.IBaby.IsBaby getter ʵ��
                return this.dripspreed;
            }
            set
            {
                // TODO:  ��� Order.FS.HISFC.Models.Base.IBaby.IsBaby setter ʵ��
                this.dripspreed = value;
            }
        }

        /// <summary>
        /// ����ҽ������ {FA143951-748B-4c45-9D1B-853A31B9E006}
        /// </summary>
        public string CountryCode
        {
            get
            {
                return this.countrycode;
            }
            set
            {
                this.countrycode = value;
            }
        }

        //public string PatientMark
        //{
        //    get
        //    {
        //        if (this.patientMark == null)
        //            this.patientMark = "";
        //        return this.patientMark;
        //    }
        //    set { this.patientMark = value; }
        //}

        #endregion

        #region ISort ��Ա

        public int SortID
        {
            get
            {
                // TODO:  ��� Order.SortID getter ʵ��
                return this.sortid;
            }
            set
            {
                // TODO:  ��� Order.SortID setter ʵ��
                this.sortid = value;
            }
        }

        #endregion

        #endregion

        #region ����

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Order Clone()
        {
            // TODO:  ��� Order.Clone ʵ��
            Order obj = base.Clone() as Order;
            obj.Combo = this.Combo.Clone();
            obj.DcReason = this.DcReason.Clone();

            obj.Frequency = (Frequency)this.Frequency.Clone();

            try { obj.ExeDept = this.ExeDept.Clone(); }
            catch { };
            try { obj.InDept = this.InDept.Clone(); }
            catch { };
            try { obj.NurseStation = this.NurseStation.Clone(); }
            catch { };
            try { obj.ReciptDept = this.ReciptDept.Clone(); }
            catch { };
            try { obj.DoctorDept = this.DoctorDept.Clone(); }
            catch { };
            try { obj.StockDept = this.StockDept.Clone(); }
            catch { };

            obj.Item = this.Item.Clone();
            obj.Nurse = this.Nurse.Clone();

            try { obj.Patient = this.Patient.Clone(); }
            catch { };

            obj.Usage = this.Usage.Clone();
            obj.oper = this.Oper.Clone();
            obj.execOper = this.ExecOper.Clone();
            obj.dcOper = this.DCOper.Clone();

            obj.compound = this.Compound.Clone();
            return obj;
        }


        #endregion

        #endregion
    }

    /// <summary>
    /// Ƥ��
    /// </summary>
    public enum EnumHypoTest
    {
        /// <summary>
        /// 0 ����ҪƤ��
        /// </summary>
        [FS.FrameWork.Public.Description("")]
        FreeHypoTest = 0,

        /// <summary>
        /// 1 ����
        /// </summary>
        [FS.FrameWork.Public.Description("[����]")]
        NoHypoTest,

        /// <summary>
        /// 2 ��ҪƤ��
        /// </summary>
        [FS.FrameWork.Public.Description("[��Ƥ��]")]
        NeedHypoTest,

        /// <summary>
        /// 3 ����
        /// </summary>
        [FS.FrameWork.Public.Description("[+]")]
        Negative,

        /// <summary>
        /// 4 ����
        /// </summary>
        [FS.FrameWork.Public.Description("[-]")]
        Positive
    }
}
