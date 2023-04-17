using System;
using FS.FrameWork.Models;


namespace FS.HISFC.Models.HealthRecord
{
    /// <summary>
    /// Base<br></br>
    /// [��������:����������Ϣ�Ǽ�]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class Base //: FS.FrameWork.Models.NeuObject
    {
        public Base()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ˽�б���
        private string caseNO = "";
        /// <summary>
        /// ������ 
        /// </summary>
        private System.String nomen;
        /// <summary>
        /// ���䵥λ
        /// </summary>
        private System.String ageUnit; 
        /// <summary>
        /// ת��ҽԺ
        /// </summary>
        private System.String comeFrom;
        /// <summary>
        /// ��Ժ��Դ
        /// </summary>
        private System.String inAvenue;
        /// <summary>
        ///��Ժ״̬
        /// </summary>
        private System.String inCircs;
        /// <summary>
        /// �������
        /// </summary>
        private System.DateTime diagDate;
        /// <summary>
        /// ��������
        /// </summary>
        private System.DateTime operationDate;
        /// <summary>
        /// ȷ������
        /// </summary>
        private System.Int32 diagDays;
        /// <summary>
        /// סԺ����
        /// </summary>
        private System.Int32 inHospitalDays;
        /// <summary>
        /// ��������
        /// </summary>
        private System.DateTime deadDate;
        /// <summary>
        /// ����ԭ��
        /// </summary>
        private System.String deadReason;
        /// <summary>
        /// �Ƿ�ʬ��
        /// </summary>
        private System.String cadaverCheck;
        /// <summary>
        /// ��������
        /// </summary>
        private System.String deadKind;
        /// <summary>
        /// ���ʺ�
        /// </summary>
        private System.String bodyAnotomize;
        /// <summary>
        /// �Ҹα��濹ԭ�����ԡ����ԡ�δ���� 
        /// </summary>
        private System.String hbsag;
        /// <summary>
        /// ���β������壨���ԡ����ԡ�δ����
        /// </summary>
        private System.String hcvAb;
        /// <summary>
        /// �������������ȱ�ݲ������壨���ԡ����ԡ�δ����
        /// </summary>
        private System.String hivAb;
        /// <summary>
        /// �ż���Ժ����
        /// </summary>
        private System.String cePi;
        /// <summary>
        /// ���Ժ����
        /// </summary>
        private System.String piPo;
        /// <summary>
        /// ��ǰ�����
        /// </summary>
        private System.String opbOpa;
        /// <summary>
        /// �ٴ�X�����
        /// </summary>
        private System.String clX;
        /// <summary>
        /// �ٴ�CT����
        /// </summary>
        private System.String clCt;
        /// <summary>
        /// �ٴ�MRI����
        /// </summary>
        private System.String clMri;
        /// <summary>
        /// �ٴ��������
        /// </summary>
        private System.String clPa;
        /// <summary>
        /// ���䲡�����
        /// </summary>
        private System.String fsBl;
        /// <summary>
        /// ���ȴ���
        /// </summary>
        private System.Int32 salvTimes;
        /// <summary>
        /// �ɹ�����
        /// </summary>
        private System.Int32 succTimes;
        /// <summary>
        /// /ʾ�̿���
        /// </summary>
        private System.String techSerc;
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private System.String visiStat;
        /// <summary>
        /// �������
        /// </summary>
        private System.DateTime visiPeriod;
        /// <summary>
        /// Ժ�ʻ������
        /// </summary>
        private System.Int32 inconNum;
        /// <summary>
        /// Զ�̻������
        /// </summary>
        private System.Int32 outconNum;

        private System.DateTime coutDate;
        /// <summary>
        /// ��������
        /// </summary>
        private System.String mrQual;
        /// <summary>
        /// �ϸ񲡰�
        /// </summary>
        private System.String mrElig; 
        /// <summary>
        ///  ���ʱ��
        /// </summary>
        private System.DateTime checkDate;
        /// <summary>
        /// ���������������ơ���顢���Ϊ��Ժ��һ����Ŀ
        /// </summary>
        private System.String ynFirst;
        /// <summary>
        /// RhѪ��(������)
        /// </summary>
        private System.String rhBlood;
        /// <summary>
        /// ��Ѫ��Ӧ���С��ޣ�
        /// </summary>
        private System.String reactionBlood;
        /// <summary>
        /// ��ϸ����
        /// </summary>
        private System.String bloodRed;
        /// <summary>
        /// ѪС����
        /// </summary>
        private System.String bloodPlatelet;
        /// <summary>
        /// Ѫ����
        /// </summary>
        private System.String bloodPlasma;
        /// <summary>
        /// ȫѪ��
        /// </summary>
        private System.String bloodWhole;
        /// <summary>
        /// ������Ѫ��
        /// </summary>
        private System.String bloodOther;
        /// <summary>
        ///  X���
        /// </summary>
        private System.String xNumb;
        /// <summary>
        /// CT��
        /// </summary>
        private System.String ctNumb;
        /// <summary>
        /// MRI��
        /// </summary>
        private System.String mriNumb;
        /// <summary>
        /// �����
        /// </summary>
        private System.String pathNumb;
        /// <summary>
        /// DSA��
        /// </summary>
        private System.String dsaNumb;
        /// <summary>
        /// PET��
        /// </summary>
        private System.String petNumb;
        /// <summary>
        ///  ECT��
        /// </summary>
        private System.String ectNumb;
        /// <summary>
        /// X�ߴ���
        /// </summary>
        private System.Int32 xQty;
        /// <summary>
        /// CT����
        /// </summary>
        private System.Int32 ctQty;
        /// <summary>
        ///  MR����
        /// </summary>
        private System.Int32 mrQty;
        /// <summary>
        /// DSA����
        /// </summary>
        private System.Int32 dsaQty;
        /// <summary>
        ///  PET����
        /// </summary>
        private System.Int32 petQty;
        /// <summary>
        /// ECT����
        /// </summary>
        private System.Int32 ectQty;
        /// <summary>
        /// �鵵�����
        /// </summary>
        private System.String barCode;
        /// <summary>
        /// lendStus
        /// </summary>
        private System.String lendStat;
        /// <summary>
        /// ����״̬1�����ʼ�/2�ǼǱ���/3����/4�������ʼ�/5��Ч
        /// </summary>
        private System.String caseStat;
        /// <summary>
        /// s������� ��������
        /// </summary>
        private string visiPeriWeek;
        /// <summary>
        /// s������� ��������
        /// </summary>
        private string visiPeriMonth;
        /// <summary>
        /// s������� ��������
        /// </summary>
        private string visiPeriYear;
        /// <summary>
        /// I������ʱ��(��)  
        /// </summary>
        private decimal iNus;
        /// <summary>
        /// II������ʱ��(��)  
        /// </summary>
        private decimal iiNus;
        /// <summary>
        /// III������ʱ��(��)    
        /// </summary>
        private decimal iiiNus;
        /// <summary>
        /// ��֢�໤ʱ��( Сʱ)     
        /// </summary>          
        private decimal strictnessNus;
        /// <summary>
        /// �ؼ�����ʱ��(Сʱ) 
        /// </summary>
        private decimal superNus;
        /// <summary>
        /// ���⻤��(��)  
        /// </summary>
        private decimal specalNus;
        /// <summary>
        /// �Ƿ񵥲���
        /// </summary>
        private string disease30;
        /// <summary>
        /// �Ƿ��Զ�¼�벡��
        /// </summary>
        private string isHandCraft;
        /// <summary>
        /// �Ƿ��в���֢
        /// </summary>
        private string syndromeFlag;
        /// <summary>
        /// ������������Ա 
        /// </summary>
        private NeuObject operationCodingOper = new NeuObject();
        /// <summary>
        /// ��Ժ����
        /// </summary>
        private FS.HISFC.Models.RADT.Location inDept = new FS.HISFC.Models.RADT.Location();

        /// <summary>
        /// ҩ�����
        /// </summary>
        private System.String anaphyFlag;
        /// <summary>
        /// ����ҩ������
        /// </summary>
        private NeuObject firstAnaphyPharmacy = new NeuObject();
        /// <summary>
        /// ����ҩ������
        /// </summary>
        private NeuObject secondAnaphyPharmacy = new NeuObject();
        /// <summary>
        /// ��Ժ����
        /// </summary>
        private FS.HISFC.Models.RADT.Location outDept = new FS.HISFC.Models.RADT.Location();
        /// <summary>
        /// �������
        /// </summary>
        private FS.FrameWork.Models.NeuObject clinicDiag = new NeuObject();

        /// <summary>
        /// ��Ժ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject inHospitalDiag = new NeuObject();
        /// <summary>
        /// ��Ժ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject outDiag = new NeuObject();

        /// <summary>
        /// ��һ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject firstOperation = new NeuObject();

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        private FS.FrameWork.Models.NeuObject firstOperationDoc = new NeuObject();
        ///// <summary>
        ///// ������
        ///// </summary>
        //private FS.HISFC.Models.RADT.Kin kin = new FS.HISFC.Models.RADT.Kin();
        //private FS.HISFC.Models.RADT.PVisit pVisit = new FS.HISFC.Models.RADT.PVisit();
        /// <summary>
        /// ����ҽ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject clinicDoc = new NeuObject();
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// ����ҽ��
        /// </summary>
        private NeuObject refresherDoc = new NeuObject();
        /// <summary>
        /// �о���ʵϰҽʦ����
        /// </summary>
        private NeuObject graduateDoc = new NeuObject();
        /// <summary>
        /// ����Ա
        /// </summary>
        private NeuObject codingOper = new NeuObject();
        /// <summary>
        /// �ʿ�ҽ��
        /// </summary>
        private NeuObject qcDoc = new NeuObject();
        /// <summary>
        /// �ʿػ�ʿ
        /// </summary>
        private NeuObject qcNucd = new NeuObject();

        /// <summary>
        /// ����Ա 
        /// </summary>
        private FS.FrameWork.Models.NeuObject packupOper = new NeuObject();
        /// <summary>
        /// ����Ա��
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// Ժ�ڸ�Ⱦ��λ
        /// </summary>
        private FS.FrameWork.Models.NeuObject infectionPosition = new NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private string cure_type;
        /// <summary>
        /// ������ҩ�Ƽ�
        /// </summary>
        private string use_cha_med;
        /// <summary>
        /// ���ȷ�ʽ
        /// </summary>
        private string save_type;
        /// <summary>
        /// �Ƿ����Σ�ء�
        /// </summary>
        private string ever_sickintodeath;
        /// <summary>
        /// �Ƿ���ּ�֢
        /// </summary>
        private string ever_firstaid;
        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        private string ever_difficulty;
        /// <summary>
        /// ��Һ��Ӧ
        /// </summary>
        private string reaction_liquid;

        /// <summary>
        /// �����ж�ԭ��
        /// </summary>
        private string injuryOrPoisoningCause;

        /// <summary>
        /// �����ж�ԭ�����
        /// add by chengym 2012-1-31
        /// </summary>
        private string injuryOrPoisoningCauseCode;

        /// <summary>
        /// �����������
        ///  add by chengym 2012-1-31
        /// </summary>
        private string pathologicalDiagName=string.Empty;

        /// <summary>
        /// ������ϱ���
        ///  add by chengym 2012-1-31
        /// </summary>
        private string pathologicalDiagCode=string.Empty;

        /// <summary>
        /// ��Ⱦ������
        /// </summary>
        private string infectionDiseasesReport;

        /// <summary>
        /// �Ĳ�����
        /// </summary>
        private string fourDiseasesReport;

        /// <summary>
        /// ��ע˵��
        /// </summary>
        private string remark;

        /// <summary>
        /// ����һ��������
        /// add by chengym 2012-1-31
        /// </summary>
        private string babyAge;
        /// <summary>
        /// ��������������
        /// add by chengym 2012-1-31
        /// </summary>
        private string babyBirthWeight = "0";
        /// <summary>
        /// ��������Ժ����
        /// add by chengym 2012-1-31
        /// </summary>
        private string babyInWeight = "0";

        /// <summary>
        /// ���λ�ʿ
        ///  add by chengym 2012-1-31
        /// </summary>
        private FS.FrameWork.Models.NeuObject dutyNurse =new NeuObject();

        /// <summary>
        /// ��Ժ��ʽ
        /// </summary>
        private string out_type;

        /// <summary>
        /// ҽ��תԺ����ҽ�ƻ���
        /// add by chengym 2012-1-31
        /// </summary>
        private string highReceiveHopital;
        /// <summary>
        /// ҽ��ת����
        /// add by chengym 2012-1-31
        /// </summary>
        private string lowerReceiveHopital;
        /// <summary>
        /// ��Ժ31������סԺ��־
        /// add by chengym 2012-1-31
        /// </summary>
        private string comeBackInMonth;
        /// <summary>
        /// ��Ժ31����סԺĿ��
        /// add by chengym 2012-1-31
        /// </summary>
        private string comeBackPurpose;
        /// <summary>
        /// ­�����˻��߻���ʱ��-��Ժǰ��
        /// add by chengym 2012-1-31
        /// </summary>
        private int outComeDay = 0;
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��ԺǰСʱ
        ///  add by chengym 2012-1-31
        /// </summary>
        private int outComeHour = 0;
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��Ժǰ����
        ///  add by chengym 2012-1-31
        /// </summary>
        private int outComeMin = 0;
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��Ժ����
        ///  add by chengym 2012-1-31
        /// </summary>
        private int inComeDay = 0;
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��Ժ��Сʱ
        ///  add by chengym 2012-1-31
        /// </summary>
        private int inComeHour = 0;
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��Ժ�����
        ///  add by chengym 2012-1-31
        /// </summary>
        private int inComeMin = 0;
        /// <summary>
        /// ת�ƿƱ�
        /// add by chengym 2012-1-31
        /// </summary>
        private string dept_Change;
        /// <summary>
        /// ��סַ
        ///  add by chengym 2012-1-31
        /// </summary>
        private string currentAddr=string.Empty;
        /// <summary>
        /// ��סַ�绰
        ///  add by chengym 2012-1-31
        /// </summary>
        private string currentPhone=string.Empty;
        /// <summary>
        /// ��סַ�ʱ�
        ///  add by chengym 2012-1-31
        /// </summary>
        private string currentZip=string.Empty;

        /// <summary>
        /// ��Ժ����
        ///  add by chengym 2012-1-31
        /// </summary>
        private string inRoom = string.Empty;

        /// <summary>
        /// ��Ժ����
        ///  add by chengym 2012-1-31
        /// </summary>
        private string outRoom = string.Empty;
        /// <summary>
        /// ��Ժ;��
        ///  add by chengym 2012-1-31
        /// </summary>
        private string inPath = string.Empty;
        /// <summary>
        /// ��������
        ///  add by chengym 2012-2-17
        /// </summary>
        private string exampleType = string.Empty;
        /// <summary>
        /// �ٴ�·������
        ///  add by chengym 2012-2-17
        /// </summary>
        private string clinicPath = string.Empty;

        /// <summary>
        /// �㶫ʡ�����ϴ���־ 0 δ�ϴ� 1 ���ϴ�
        ///  add by chengym 2012-2-23
        /// </summary>
        private string uploadStatu = string.Empty;
        /// <summary>
        /// Drgs�������
        ///  add by chengym 2012-2-23
        /// </summary>
        private string isDrgs = string.Empty;
        #endregion

        //{7D094A18-0FC9-4e8b-A8E6-901E55D4C20C}

        #region ����

        

        //public FS.HISFC.Models.RADT.PVisit PVisit
        //{
        //    get
        //    {
        //        return pVisit;
        //    }
        //    set
        //    {
        //        pVisit = value;
        //    }
        //}

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
        /// �Ƿ��в���֢
        /// </summary>
        public string SyndromeFlag
        {
            get
            {
                return syndromeFlag;
            }
            set
            {
                syndromeFlag = value;
            }
        }

        /// <summary>
        /// ������������Ա 
        /// </summary>
        public NeuObject OperationCoding
        {
            get
            {
                return operationCodingOper;
            }
            set
            {
                operationCodingOper = value;
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public NeuObject ClinicDoc
        {
            get
            {
                return clinicDoc;
            }
            set
            {
                clinicDoc = value;
            }
        }

        ///// <summary>
        ///// ������ϵ�� 
        ///// </summary>
        //public FS.HISFC.Models.RADT.Kin Kin
        //{
        //    get
        //    {
        //        return kin;
        //    }
        //    set
        //    {
        //        kin = value;
        //    }
        //}

        /// <summary>
        /// ��Ժ����
        /// </summary>
        public FS.HISFC.Models.RADT.Location InDept
        {
            get
            {
                return inDept;
            }
            set
            {
                inDept = value;
            }
        }

        /// <summary>
        /// ��Ժ����
        /// </summary>
        public FS.HISFC.Models.RADT.Location OutDept
        {
            get
            {
                return outDept;
            }
            set
            {
                outDept = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ClinicDiag
        {
            get
            {
                return clinicDiag;
            }
            set
            {
                clinicDiag = value;
            }
        }

        /// <summary>
        /// ��Ժ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject InHospitalDiag
        {
            get
            {
                return inHospitalDiag;
            }
            set
            {
                inHospitalDiag = value;
            }
        }

        /// <summary>
        /// ��Ժ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject OutDiag
        {
            get
            {
                return outDiag;
            }
            set
            {
                outDiag = value;
            }
        }

        /// <summary>
        /// ��һ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject FirstOperation
        {
            get
            {
                return firstOperation;
            }
            set
            {
                firstOperation = value;
            }
        }

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        public FS.FrameWork.Models.NeuObject FirstOperationDoc
        {
            get
            {
                return firstOperationDoc;
            }
            set
            {
                firstOperationDoc = value;
            }
        }

        /// <summary>
        /// Ժ�ڸ�Ⱦ����
        /// </summary>
        public int InfectionNum;

        /// <summary>
        /// �ֹ�¼�벡����־
        /// </summary>
        public string IsHandCraft
        {
            get
            {
                if (isHandCraft == null)
                {
                    isHandCraft = "";
                }
                return isHandCraft;
            }
            set
            {
                isHandCraft = value;
            }
        }

        /// <summary>
        /// �Ƿ񵥲���
        /// </summary>
        public string Disease30
        {
            get
            {
                return disease30;
            }
            set
            {
                disease30 = value;
            }
        }

        /// <summary>
        /// ����Ա 
        /// </summary>
        public FS.FrameWork.Models.NeuObject PackupMan
        {
            get
            {
                return packupOper;
            }
            set
            {
                packupOper = value;
            }
        }

        /// <summary>
        /// ���⻤��
        /// </summary>
        public decimal SpecalNus
        {
            get
            {
                return specalNus;
            }
            set
            {
                specalNus = value;
            }
        }

        /// <summary>
        /// �ؼ�����ʱ��
        /// </summary>
        public decimal SuperNus
        {
            get
            {
                return superNus;
            }
            set
            {
                superNus = value;
            }
        }

        /// <summary>
        /// ��֢�໤ʱ��
        /// </summary>
        public decimal StrictNuss
        {
            get
            {
                return strictnessNus;
            }
            set
            {
                strictnessNus = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal IIINus
        {
            get
            {
                return iiiNus;
            }
            set
            {
                iiiNus = value;
            }
        }

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public decimal IINus
        {
            get
            {
                return iiNus;
            }
            set
            {
                iiNus = value;
            }
        }

        /// <summary>
        /// һ������ʱ��
        /// </summary>
        public decimal INus
        {
            get
            {
                return iNus;
            }
            set
            {
                iNus = value;
            }
        }

        /// <summary>
        /// s������� ��������
        /// </summary>
        public string VisiPeriodYear
        {
            get
            {
                if (visiPeriYear == null)
                {
                    visiPeriYear = "";
                }
                return visiPeriYear;
            }
            set
            {
                visiPeriYear = value;
            }
        }

        /// <summary>
        /// s������� ��������
        /// </summary>
        public string VisiPeriodMonth
        {
            get
            {
                return visiPeriMonth;
            }
            set
            {
                visiPeriMonth = value;
            }
        }

        /// <summary>
        /// s������� ��������
        /// </summary>
        public string VisiPeriodWeek
        {
            get
            {
                return visiPeriWeek;
            }
            set
            {
                visiPeriWeek = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public System.String Nomen
        {
            get
            {
                return this.nomen;
            }
            set
            {
                this.nomen = value;
            }
        }

        /// <summary>
        /// ���䵥λ
        /// </summary>
        public System.String AgeUnit
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
        /// ת��ҽԺ
        /// </summary>
        public System.String ComeFrom
        {
            get
            {
                return this.comeFrom;
            }
            set
            {
                this.comeFrom = value;
            }
        }

        /// <summary>
        /// ��Ժ��Դ
        /// </summary>
        public System.String InAvenue
        {
            get
            {
                return this.inAvenue;
            }
            set
            {
                this.inAvenue = value;
            }
        }

        /// <summary>
        /// ��Ժ״̬
        /// </summary>
        public System.String InCircs
        {
            get
            {
                return this.inCircs;
            }
            set
            {
                this.inCircs = value;
            }
        }

        /// <summary>
        /// ȷ������
        /// </summary>
        public System.DateTime DiagDate
        {
            get
            {
                return this.diagDate;
            }
            set
            {
                this.diagDate = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public System.DateTime OperationDate
        {
            get
            {
                return this.operationDate;
            }
            set
            {
                this.operationDate = value;
            }
        }

        /// <summary>
        /// ȷ������
        /// </summary>
        public System.Int32 DiagDays
        {
            get
            {
                return this.diagDays;
            }
            set
            {
                this.diagDays = value;
            }
        }

        /// <summary>
        /// סԺ����
        /// </summary>
        public System.Int32 InHospitalDays
        {
            get
            {
                return this.inHospitalDays;
            }
            set
            {
                this.inHospitalDays = value;
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
        /// ����ԭ��
        /// </summary>
        public System.String DeadReason
        {
            get
            {
                return this.deadReason;
            }
            set
            {
                this.deadReason = value;
            }
        }

        /// <summary>
        /// ʬ��
        /// </summary>
        public System.String CadaverCheck
        {
            get
            {
                return this.cadaverCheck;
            }
            set
            {
                this.cadaverCheck = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public System.String DeadKind
        {
            get
            {
                return this.deadKind;
            }
            set
            {
                this.deadKind = value;
            }
        }

        /// <summary>
        /// ʬ����ʺ�
        /// </summary>
        public System.String BodyAnotomize
        {
            get
            {
                return this.bodyAnotomize;
            }
            set
            {
                this.bodyAnotomize = value;
            }
        }

        /// <summary>
        /// �Ҹα��濹ԭ�����ԡ����ԡ�δ����
        /// </summary>
        public System.String Hbsag
        {
            get
            {
                return this.hbsag;
            }
            set
            {
                this.hbsag = value;
            }
        }

        /// <summary>
        /// ���β������壨���ԡ����ԡ�δ����
        /// </summary>
        public System.String HcvAb
        {
            get
            {
                return this.hcvAb;
            }
            set
            {
                this.hcvAb = value;
            }
        }

        /// <summary>
        /// �������������ȱ�ݲ������壨���ԡ����ԡ�δ����
        /// </summary>
        public System.String HivAb
        {
            get
            {
                return this.hivAb;
            }
            set
            {
                this.hivAb = value;
            }
        }

        /// <summary>
        /// �ż���Ժ����
        /// </summary>
        public System.String CePi
        {
            get
            {
                return this.cePi;
            }
            set
            {
                this.cePi = value;
            }
        }

        /// <summary>
        /// ���Ժ����
        /// </summary>
        public System.String PiPo
        {
            get
            {
                return this.piPo;
            }
            set
            {
                this.piPo = value;
            }
        }

        /// <summary>
        /// ��ǰ�����
        /// </summary>
        public System.String OpbOpa
        {
            get
            {
                return this.opbOpa;
            }
            set
            {
                this.opbOpa = value;
            }
        }

        /// <summary>
        /// �ٴ�X�����
        /// </summary>
        public System.String ClX
        {
            get
            {
                return this.clX;
            }
            set
            {
                this.clX = value;
            }
        }

        /// <summary>
        /// �ٴ�CT����
        /// </summary>
        public System.String ClCt
        {
            get
            {
                return this.clCt;
            }
            set
            {
                this.clCt = value;
            }
        }

        /// <summary>
        /// �ٴ�MRI����
        /// </summary>
        public System.String ClMri
        {
            get
            {
                return this.clMri;
            }
            set
            {
                this.clMri = value;
            }
        }

        /// <summary>
        /// �ٴ��������
        /// </summary>
        public System.String ClPa
        {
            get
            {
                return this.clPa;
            }
            set
            {
                this.clPa = value;
            }
        }

        /// <summary>
        /// ���䲡�����
        /// </summary>
        public System.String FsBl
        {
            get
            {
                return this.fsBl;
            }
            set
            {
                this.fsBl = value;
            }
        }

        /// <summary>
        /// ���ȴ���
        /// </summary>
        public System.Int32 SalvTimes
        {
            get
            {
                return this.salvTimes;
            }
            set
            {
                this.salvTimes = value;
            }
        }

        /// <summary>
        /// �ɹ�����
        /// </summary>
        public System.Int32 SuccTimes
        {
            get
            {
                return this.succTimes;
            }
            set
            {
                this.succTimes = value;
            }
        }

        /// <summary>
        /// ʾ�̿���
        /// </summary>
        public System.String TechSerc
        {
            get
            {
                return this.techSerc;
            }
            set
            {
                this.techSerc = value;
            }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public System.String VisiStat
        {
            get
            {
                return this.visiStat;
            }
            set
            {
                this.visiStat = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public System.DateTime VisiPeriod
        {
            get
            {
                return this.visiPeriod;
            }
            set
            {
                this.visiPeriod = value;
            }
        }

        /// <summary>
        /// Ժ�ʻ������
        /// </summary>
        public System.Int32 InconNum
        {
            get
            {
                return this.inconNum;
            }
            set
            {
                this.inconNum = value;
            }
        }

        /// <summary>
        /// Զ�̻������
        /// </summary>
        public System.Int32 OutconNum
        {
            get
            {
                return this.outconNum;
            }
            set
            {
                this.outconNum = value;
            }
        }

        /// <summary>
        /// ҩ�����
        /// </summary>
        public System.String AnaphyFlag
        {
            get
            {
                return this.anaphyFlag;
            }
            set
            {
                this.anaphyFlag = value;
            }
        }

        /// <summary>
        /// ���ĺ��Ժ����
        /// </summary>
        public System.DateTime CoutDate
        {
            get
            {
                return this.coutDate;
            }
            set
            {
                this.coutDate = value;
            }
        }

        /// <summary>
        /// ����ҩ��1
        /// </summary>
        public NeuObject FirstAnaphyPharmacy
        {
            get
            {
                return this.firstAnaphyPharmacy;
            }
            set
            {
                this.firstAnaphyPharmacy = value;
            }
        }

        /// <summary>
        /// ����ҩ��2
        /// </summary>
        public NeuObject SecondAnaphyPharmacy
        {
            get
            {
                return this.secondAnaphyPharmacy;
            }
            set
            {
                this.secondAnaphyPharmacy = value;
            }
        }
        /// <summary>
        /// ����ҽʦ����
        /// </summary>
        public NeuObject RefresherDoc
        {
            get
            {
                return this.refresherDoc;
            }
            set
            {
                this.refresherDoc = value;
            }
        }

        /// <summary>
        /// �о���ʵϰҽʦ����
        /// </summary>
        public NeuObject GraduateDoc
        {
            get
            {
                return this.graduateDoc;
            }
            set
            {
                this.graduateDoc = value;
            }
        }

        /// <summary>
        /// ����Ա����
        /// </summary>
        public NeuObject CodingOper
        {
            get
            {
                return this.codingOper;
            }
            set
            {
                this.codingOper = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public System.String MrQuality
        {
            get
            {
                return this.mrQual;
            }
            set
            {
                this.mrQual = value;
            }
        }

        /// <summary>
        /// �ϸ񲡰�
        /// </summary>
        public System.String MrEligible
        {
            get
            {
                return this.mrElig;
            }
            set
            {
                this.mrElig = value;
            }
        }

        /// <summary>
        /// �ʿ�ҽʦ
        /// </summary>
        public NeuObject QcDoc
        {
            get
            {
                return this.qcDoc;
            }
            set
            {
                this.qcDoc = value;
            }
        }

        /// <summary>
        /// �ʿػ�ʿ����
        /// </summary>
        public NeuObject QcNurse
        {
            get
            {
                return this.qcNucd;
            }
            set
            {
                this.qcNucd = value;
            }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public System.DateTime CheckDate
        {
            get
            {
                return this.checkDate;
            }
            set
            {
                this.checkDate = value;
            }
        }

        /// <summary>
        /// ���������������ơ���顢���Ϊ��Ժ��һ����Ŀ
        /// </summary>
        public System.String YnFirst
        {
            get
            {
                return this.ynFirst;
            }
            set
            {
                this.ynFirst = value;
            }
        }

        /// <summary>
        /// RhѪ��(������)
        /// </summary>
        public System.String RhBlood
        {
            get
            {
                return this.rhBlood;
            }
            set
            {
                this.rhBlood = value;
            }
        }

        /// <summary>
        /// ��Ѫ��Ӧ���С��ޣ�
        /// </summary>
        public System.String ReactionBlood
        {
            get
            {
                return this.reactionBlood;
            }
            set
            {
                this.reactionBlood = value;
            }
        }

        /// <summary>
        /// ��ϸ����
        /// </summary>
        public System.String BloodRed
        {
            get
            {
                return this.bloodRed;
            }
            set
            {
                this.bloodRed = value;
            }
        }

        /// <summary>
        /// ѪС����
        /// </summary>
        public System.String BloodPlatelet
        {
            get
            {
                return this.bloodPlatelet;
            }
            set
            {
                this.bloodPlatelet = value;
            }
        }

        /// <summary>
        /// Ѫ����
        /// </summary>
        public System.String BloodPlasma
        {
            get
            {
                return this.bloodPlasma;
            }
            set
            {
                this.bloodPlasma = value;
            }
        }

        /// <summary>
        /// ȫѪ��
        /// </summary>
        public System.String BloodWhole
        {
            get
            {
                return this.bloodWhole;
            }
            set
            {
                this.bloodWhole = value;
            }
        }

        /// <summary>
        /// ������Ѫ��
        /// </summary>
        public System.String BloodOther
        {
            get
            {
                return this.bloodOther;
            }
            set
            {
                this.bloodOther = value;
            }
        }

        /// <summary>
        /// X���
        /// </summary>
        public System.String XNum
        {
            get
            {
                return this.xNumb;
            }
            set
            {
                this.xNumb = value;
            }
        }

        /// <summary>
        /// CT��
        /// </summary>
        public System.String CtNum
        {
            get
            {
                return this.ctNumb;
            }
            set
            {
                this.ctNumb = value;
            }
        }

        /// <summary>
        /// MRI��
        /// </summary>
        public System.String MriNum
        {
            get
            {
                return this.mriNumb;
            }
            set
            {
                this.mriNumb = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public System.String PathNum
        {
            get
            {
                return this.pathNumb;
            }
            set
            {
                this.pathNumb = value;
            }
        }

        /// <summary>
        /// DSA��
        /// </summary>
        public System.String DsaNum
        {
            get
            {
                return this.dsaNumb;
            }
            set
            {
                this.dsaNumb = value;
            }
        }

        /// <summary>
        /// PET��
        /// </summary>
        public System.String PetNum
        {
            get
            {
                return this.petNumb;
            }
            set
            {
                this.petNumb = value;
            }
        }

        /// <summary>
        /// ECT��
        /// </summary>
        public System.String EctNum
        {
            get
            {
                return this.ectNumb;
            }
            set
            {
                this.ectNumb = value;
            }
        }

        /// <summary>
        /// X�ߴ���
        /// </summary>
        public System.Int32 XQty
        {
            get
            {
                return this.xQty;
            }
            set
            {
                this.xQty = value;
            }
        }

        /// <summary>
        /// CT����
        /// </summary>
        public System.Int32 CTQty
        {
            get
            {
                return this.ctQty;
            }
            set
            {
                this.ctQty = value;
            }
        }

        /// <summary>
        /// MR����
        /// </summary>
        public System.Int32 MRQty
        {
            get
            {
                return this.mrQty;
            }
            set
            {
                this.mrQty = value;
            }
        }

        /// <summary>
        /// DSA����
        /// </summary>
        public System.Int32 DSAQty
        {
            get
            {
                return this.dsaQty;
            }
            set
            {
                this.dsaQty = value;
            }
        }

        /// <summary>
        /// PET����
        /// </summary>
        public System.Int32 PetQty
        {
            get
            {
                return this.petQty;
            }
            set
            {
                this.petQty = value;
            }
        }

        /// <summary>
        /// ECT����
        /// </summary>
        public System.Int32 EctQty
        {
            get
            {
                return this.ectQty;
            }
            set
            {
                this.ectQty = value;
            }
        }

        /// <summary>
        /// �鵵�����
        /// </summary>
        public System.String BarCode
        {
            get
            {
                return this.barCode;
            }
            set
            {
                this.barCode = value;
            }
        }

        /// <summary>
        /// ��������״̬(O��� I�ڼ�)
        /// </summary>
        public System.String LendStat
        {
            get
            {
                return this.lendStat;
            }
            set
            {
                this.lendStat = value;
            }
        }

        /// <summary>
        /// ����״̬1�����ʼ�/2�ǼǱ���/3����/4�������ʼ�/5��Ч
        /// </summary>
        public System.String CaseStat
        {
            get
            {
                return this.caseStat;
            }
            set
            {
                this.caseStat = value;
            }
        }
        /// <summary>
        /// ���߻�������
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string CaseNO
        {
            get
            {
                return caseNO;
            }
            set
            {
                caseNO = value;
            }
        }
        /// <summary>
        /// Ժ�ڸ�Ⱦ��λ
        /// </summary>
        public NeuObject InfectionPosition
        {
            get
            {
                return this.infectionPosition;
            }
            set
            {
                this.infectionPosition = value;
            }
        }


        /// <summary>
        /// ��Ժ��ʽ
        /// </summary>
        public string Out_Type
        {
            get
            {
                return this.out_type;
            }
            set
            {
                this.out_type = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Cure_Type
        {
            get
            {
                return this.cure_type;
            }
            set
            {
                this.cure_type = value;
            }
        }

        /// <summary>
        /// ������ҩ�Ƽ�
        /// </summary>
        public string Use_CHA_Med
        {
            get
            {
                return this.use_cha_med;
            }
            set
            {
                this.use_cha_med = value;
            }
        }
        /// <summary>
        /// ���ȷ�ʽ
        /// </summary>
        public string Save_Type
        {
            get
            {
                return this.save_type;
            }
            set
            {
                this.save_type = value;
            }
        }
        /// <summary>
        /// �Ƿ����Σ�ء�
        /// </summary>
        public string Ever_Sickintodeath
        {
            get
            {
                return this.ever_sickintodeath;
            }
            set
            {
                this.ever_sickintodeath = value;
            }
        }
        /// <summary>
        /// �Ƿ���ּ�֢
        /// </summary>
        public string Ever_Firstaid
        {
            get
            {
                return this.ever_firstaid;
            }
            set
            {
                this.ever_firstaid = value;
            }
        }
        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        public  string Ever_Difficulty
        {
            get
            {
                return this.ever_difficulty;
            }
            set
            {
                this.ever_difficulty = value;
            }
        }
        /// <summary>
        /// ��Һ��Ӧ
        /// </summary>
        public string ReactionLiquid
        {
            get
            {
                return this.reaction_liquid;
            }
            set
            {
                this.reaction_liquid = value;
            }
        }

        /// <summary>
        /// �����ж�ԭ��
        /// </summary>
        public string InjuryOrPoisoningCause
        {
            get
            {
                return injuryOrPoisoningCause;
            }
            set
            {
                this.injuryOrPoisoningCause = value;
            }
        }

        public string InfectionDiseasesReport
        {
            get
            {
                return this.infectionDiseasesReport;
            }
            set
            {
                this.infectionDiseasesReport = value;
            }
        }

        /// <summary>
        /// �Ĳ�����
        /// </summary>
        public string FourDiseasesReport
        {
            get 
            {
                return fourDiseasesReport;
            }
            set
            {
                fourDiseasesReport = value;
            }
        }

        /// <summary>
        /// ��ע˵��
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }
        /// <summary>
        /// �����ж�ԭ�����
        /// add by chengym 2012-1-31
        /// </summary>
        public string InjuryOrPoisoningCauseCode
        {
            get
            {
                return injuryOrPoisoningCauseCode;
            }
            set
            {
                injuryOrPoisoningCauseCode = value;
            }
        }

        /// <summary>
        /// �����������
        ///  add by chengym 2012-1-31
        /// </summary>
        public string PathologicalDiagName
        {
            get
            {
                return pathologicalDiagName;
            }
            set
            {
                pathologicalDiagName = value;
            }
        }

        /// <summary>
        /// ������ϱ���
        ///  add by chengym 2012-1-31
        /// </summary>
        public string PathologicalDiagCode
        {
            get
            {
                return pathologicalDiagCode;
            }
            set
            {
                pathologicalDiagCode = value;
            }
        }

        /// <summary>
        /// ����һ��������
        /// add by chengym 2012-1-31
        /// </summary>
        public string BabyAge
        {
            get
            {
                return babyAge;
            }
            set
            {
                babyAge = value;
            }
        }
        /// <summary>
        /// ��������������
        /// add by chengym 2012-1-31
        /// </summary>
        public string BabyBirthWeight
        {
            get
            {
                return babyBirthWeight;
            }
            set
            {
                babyBirthWeight = value;
            }
        }
        /// <summary>
        /// ��������Ժ����
        /// add by chengym 2012-1-31
        /// </summary>
        public string BabyInWeight
        {
            get
            {
                return babyInWeight;
            }
            set
            {
                babyInWeight = value;
            }
        }

        /// <summary>
        /// ���λ�ʿ
        ///  add by chengym 2012-1-31
        /// </summary>
        public FS.FrameWork.Models.NeuObject DutyNurse
        {
            get
            {
                return this.dutyNurse;
            }
            set
            {
                this.dutyNurse = value;
            }
        }

        /// <summary>
        /// ҽ��תԺ����ҽ�ƻ���
        /// add by chengym 2012-1-31
        /// </summary>
        public string HighReceiveHopital
        {
            get
            {
                return this.highReceiveHopital;
            }
            set
            {
                this.highReceiveHopital = value;
            }
        }
        /// <summary>
        /// ҽ��ת����
        /// add by chengym 2012-1-31
        /// </summary>
        public string LowerReceiveHopital
        {
            get
            {
                return this.lowerReceiveHopital;
            }
            set
            {
                this.lowerReceiveHopital = value;
            }
        }
        /// <summary>
        /// ��Ժ31������סԺ��־
        /// add by chengym 2012-1-31
        /// </summary>
        public string ComeBackInMonth
        {
            get
            {
                return this.comeBackInMonth;
            }
            set
            {
                this.comeBackInMonth = value;
            }
        }
        /// <summary>
        /// ��Ժ31����סԺĿ��
        /// add by chengym 2012-1-31
        /// </summary>
        public string ComeBackPurpose
        {
            get
            {
                return this.comeBackPurpose;
            }
            set
            {
                this.comeBackPurpose = value;
            }
        }
        /// <summary>
        /// ­�����˻��߻���ʱ��-��Ժǰ��
        /// add by chengym 2012-1-31
        /// </summary>
        public int OutComeDay
        {
            get
            {
                return this.outComeDay;
            }
            set
            {
                this.outComeDay = value;
            }
        }
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��ԺǰСʱ
        ///  add by chengym 2012-1-31
        /// </summary>
        public int OutComeHour
        {
            get
            {
                return this.outComeHour;
            }
            set
            {
                this.outComeHour = value;
            }
        }
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��Ժǰ����
        ///  add by chengym 2012-1-31
        /// </summary>
        public int OutComeMin
        {
            get
            {
                return this.outComeMin;
            }
            set
            {
                this.outComeMin = value;
            }
        }
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��Ժ����
        ///  add by chengym 2012-1-31
        /// </summary>
        public int InComeDay
        {
            get
            {
                return this.inComeDay;
            }
            set
            {
                this.inComeDay = value;
            }
        }
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��Ժ��Сʱ
        ///  add by chengym 2012-1-31
        /// </summary>
        public int InComeHour
        {
            get
            {
                return this.inComeHour;
            }
            set
            {
                this.inComeHour = value;
            }
        }
        /// <summary>
        ///  ­�����˻��߻���ʱ��-��Ժ�����
        ///  add by chengym 2012-1-31
        /// </summary>
        public int InComeMin
        {
            get
            {
                return this.inComeMin;
            }
            set
            {
                this.inComeMin = value;
            }
        }

        /// <summary>
        /// ת�ƿƱ�
        /// add by chengym 2012-1-31
        /// </summary>
        public string Dept_Change
        {
            get
            {
                return this.dept_Change;
            }
            set
            {
                this.dept_Change = value;
            }
        }

        /// <summary>
        /// ��סַ
        ///  add by chengym 2012-1-31
        /// </summary>
        public string CurrentAddr
        {
            get { return this.currentAddr; }
            set { this.currentAddr = value; }
        }
        /// <summary>
        /// ��סַ�绰
        ///  add by chengym 2012-1-31
        /// </summary>
        public string CurrentPhone
        {
            get { return this.currentPhone; }
            set { this.currentPhone = value; }
        }
        /// <summary>
        /// ��סַ�ʱ�
        ///  add by chengym 2012-1-31
        /// </summary>
        public string CurrentZip
        {
            get { return this.currentZip; }
            set { this.currentZip = value; }
        }
        /// <summary>
        /// ��Ժ����
        ///  add by chengym 2012-1-31
        /// </summary>
        public string InRoom
        {
            get { return this.inRoom; }
            set { this.inRoom = value; }
        }
        /// <summary>
        /// ��Ժ����
        ///  add by chengym 2012-1-31
        /// </summary>
        public string OutRoom
        {
            get { return this.outRoom; }
            set { this.outRoom = value; }
        }
        /// <summary>
        /// ��Ժ;��
        /// add by chengym 2012-1-31
        /// </summary>
        public string InPath
        {
            get { return this.inPath; }
            set { this.inPath = value; }
        }
        /// <summary>
        /// ��������
        ///  add by chengym 2012-2-17
        /// </summary>
        public string ExampleType
        {
            get { return this.exampleType; }
            set { this.exampleType = value; }
        }
        /// <summary>
        /// �ٴ�·������
        /// add by chengym 2012-2-17
        /// </summary>
        public string ClinicPath
        {
            get { return this.clinicPath; }
            set { this.clinicPath = value; }
        }

        /// <summary>
        /// �㶫ʡ�����ϴ���־ 0 δ�ϴ� 1 ���ϴ�
        ///  add by chengym 2012-2-23
        /// </summary>
        public string UploadStatu
        {
            get { return this.uploadStatu; }
            set { this.uploadStatu = value; }
        }
        /// <summary>
        /// Drgs�������
        ///  add by chengym 2012-2-23
        /// </summary>
        public string IsDrgs
        {
            get { return this.isDrgs; }
            set { this.isDrgs = value; }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new Base Clone()
        {
            Base b = base.MemberwiseClone() as Base;
            b.qcDoc = qcDoc.Clone();
            b.qcNucd = qcNucd.Clone();
            b.refresherDoc = refresherDoc.Clone();
            b.graduateDoc = graduateDoc.Clone();
            b.codingOper = codingOper.Clone();
            b.firstAnaphyPharmacy = firstAnaphyPharmacy.Clone();
            b.secondAnaphyPharmacy = secondAnaphyPharmacy.Clone();
            b.clinicDiag = this.clinicDiag.Clone();
            b.inHospitalDiag = this.inHospitalDiag.Clone();
            b.firstOperation = this.firstOperation.Clone();
            b.firstOperationDoc = this.firstOperationDoc.Clone();
            b.outDiag = this.outDiag.Clone();
            b.operInfo = this.operInfo.Clone();
            b.packupOper = this.packupOper.Clone();
            b.clinicDoc = this.clinicDoc.Clone();
            b.inDept = this.inDept.Clone();
            b.outDept = this.outDept.Clone();
            b.patientInfo = this.patientInfo.Clone();
            b.operationCodingOper = operationCodingOper.Clone();
            b.infectionPosition = infectionPosition.Clone();
            b.dutyNurse = dutyNurse.Clone();
            return b;
        }
        #endregion

        #region  ����
        [Obsolete("���� ")]
        private System.String operCode;
        [Obsolete("���� ")]
        private System.DateTime operDate;
        [Obsolete("���� ")]
        private System.String codingName;
        [Obsolete("������ ClinicDoc ����")]
        private System.String clinicDocd;
        [Obsolete("������ ClinicDoc ����")]
        private System.String clinicDonm;
        [Obsolete("������  ����")]
        private System.String graDocName;
        [Obsolete("�����ü̳д���")]
        private System.String linkmanTel;
        [Obsolete("�����ü̳д���")]
        private System.String linkmanAdd;


        [Obsolete("���� �� nationality ���� ")]
        private System.String nationCode;

        /// <summary>
        /// PET����
        /// </summary>
        [Obsolete("������ PetQty ����", true)]
        public System.Int32 PetTimes
        {
            get
            {
                return this.petQty;
            }
            set
            {
                this.petQty = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        [Obsolete("���� �� Nationality ���� ", true)]
        public System.String NationCode
        {
            get
            {
                return this.nationCode;
            }
            set
            {
                this.nationCode = value;
            }
        }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        [Obsolete("������ Kin ����", true)]
        public System.String LinkmanTel
        {
            get
            {
                return this.linkmanTel;
            }
            set
            {
                this.linkmanTel = value;
            }
        }
        /// <summary>
        /// ��ϵ��ַ
        /// </summary>
        [Obsolete("������ Kin ����", true)]
        public System.String LinkmanAdd
        {
            get
            {
                return this.linkmanAdd;
            }
            set
            {
                this.linkmanAdd = value;
            }
        }
        /// <summary>
        /// �������ҽ��
        /// </summary>
        [Obsolete("������ ClinicDoc ����", true)]
        public System.String ClinicDocd
        {
            get
            {
                return this.clinicDocd;
            }
            set
            {
                this.clinicDocd = value;
            }
        }
        /// <summary>
        /// �������ҽ������
        /// </summary>
        [Obsolete("������ ClinicDoc ����", true)]
        public System.String ClinicDonm
        {
            get
            {
                return this.clinicDonm;
            }
            set
            {
                this.clinicDonm = value;
            }
        }
        /// <summary>
        /// סԺ����
        /// </summary>
        [Obsolete("������ InHospitalDays ����", true)]
        public System.Int32 PiDays
        {
            get
            {
                return this.inHospitalDays;
            }
            set
            {
                this.inHospitalDays = value;
            }
        }
        /// <summary>
        /// ʬ��
        /// </summary>
        [Obsolete("������ CadaverCheck ����", true)]
        public System.String BodyCheck
        {
            get
            {
                return this.cadaverCheck;
            }
            set
            {
                this.cadaverCheck = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        [Obsolete("������ VisiPeriod ����", true)]
        public System.DateTime VisiPeri
        {
            get
            {
                return this.visiPeriod;
            }
            set
            {
                this.visiPeriod = value;
            }
        }
        /// <summary>
        /// ����ҩ������
        /// </summary>
        [Obsolete("������ firstAnaphyPharmacy ����", true)]
        public NeuObject AnaphyName1
        {
            get
            {
                return this.firstAnaphyPharmacy;
            }
            set
            {
                this.firstAnaphyPharmacy = value;
            }
        }
        /// <summary>
        /// ����ҩ������
        /// </summary>
        [Obsolete("������ firstAnaphyPharmacy ����", true)]
        public NeuObject AnaphyName2
        {
            get
            {
                return this.firstAnaphyPharmacy;
            }
            set
            {
                this.firstAnaphyPharmacy = value;
            }
        }
        /// <summary>
        /// ����ҽ������
        /// </summary>
        [Obsolete("������ RefresherDoc.Name ����", true)]
        public System.String RefresherDonm
        {
            get
            {
                return null;
            }
            //set
            //{
            //    this.refresherDonm = value;
            //}
        }
        /// <summary>
        /// �о���ʵϰҽʦ����
        /// </summary>
        [Obsolete("������ GraduateDoc.Name ����", true)]
        public System.String GraDocName
        {
            get
            {
                return this.graDocName;
            }
            set
            {
                this.graDocName = value;
            }
        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        [Obsolete("������ CodingOper.Name ����", true)]
        public System.String CodingName
        {
            get
            {
                return this.codingName;
            }
            set
            {
                this.codingName = value;
            }
        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        [Obsolete("������ CodingOper.Name ����", true)]
        public NeuObject codingCode
        {
            get
            {
                return null;
            }
            set
            {
                //this.codingOper = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        [Obsolete("������ MrQuality ����", true)]
        public System.String MrQual
        {
            get
            {
                return this.mrQual;
            }
            set
            {
                this.mrQual = value;
            }
        }
        /// <summary>
        /// �ϸ񲡰�
        /// </summary>
        [Obsolete("������ MrEligible ����", true)]
        public System.String MrElig
        {
            get
            {
                return this.mrElig;
            }
            set
            {
                this.mrElig = value;
            }
        }
        /// <summary>
        /// �ʿ�ҽʦ����
        /// </summary>
        [Obsolete("������ QcDocd.Name ����", true)]
        public System.String QcDonm
        {
            get
            {
                return null;
            }
            //set
            //{
            //    this.qcDonm = value;
            //}
        }
        /// <summary>
        /// �ʿػ�ʿ����
        /// </summary>
        [Obsolete("������ QcNurse ����", true)]
        public System.String QcNucd
        {
            get
            {
                return null;
            }
            //set
            //{
            //    this.qcNucd = value;
            //}
        }
        /// <summary>
        /// �ʿػ�ʿ����
        /// </summary>
        [Obsolete("������ QcNurse.Name ����", true)]
        public System.String QcNunm
        {
            get
            {
                return null;
            }
            //set
            //{
            //    this.qcNunm = value;
            //}
        }
        /// <summary>
        /// X�ߴ���
        /// </summary>
        [Obsolete("������ XQty ����", true)]
        public System.Int32 XTimes
        {
            get
            {
                return this.xQty;
            }
            set
            {
                this.xQty = value;
            }
        }
        /// <summary>
        /// CT����
        /// </summary>
        [Obsolete("������ CTQty ����", true)]
        public System.Int32 CtTimes
        {
            get
            {
                return this.ctQty;
            }
            set
            {
                this.ctQty = value;
            }
        }
        /// <summary>
        /// MR����
        /// </summary>
        [Obsolete("������ MRQty ����", true)]
        public System.Int32 MrTimes
        {
            get
            {
                return this.mrQty;
            }
            set
            {
                this.mrQty = value;
            }
        }
        /// <summary>
        /// DSA����
        /// </summary>
        [Obsolete("������ DSAQty ����", true)]
        public System.Int32 DsaTimes
        {
            get
            {
                return this.dsaQty;
            }
            set
            {
                this.dsaQty = value;
            }
        }
        /// <summary>
        /// ECT����
        /// </summary>
        [Obsolete("������ EctQty ����", true)]
        public System.Int32 EctTimes
        {
            get
            {
                return this.ectQty;
            }
            set
            {
                this.ectQty = value;
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        [Obsolete("������ OperInfo ����", true)]
        public System.String OperCode
        {
            get
            {
                return this.operCode;
            }
            set
            {
                this.operCode = value;
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("������ OperInfo ����", true)]
        public System.DateTime OperDate
        {
            get
            {
                return this.operDate;
            }
            set
            {
                this.operDate = value;
            }
        }
        /// <summary>
        /// ���⻤��
        /// </summary>
        [Obsolete("������ SpecalNus ����", true)]
        public decimal SPecalNus
        {
            get
            {
                return specalNus;
            }
            set
            {
                specalNus = value;
            }
        }
        /// <summary>
        /// ��������״̬(O��� I�ڼ�)
        /// </summary>
        [Obsolete("������ lendStat ����", true)]
        public System.String LendStus
        {
            get
            {
                return this.lendStat;
            }
            set
            {
                this.lendStat = value;
            }
        }
        /// <summary>
        /// ����״̬1�����ʼ�/2�ǼǱ���/3����/4�������ʼ�/5��Ч
        /// </summary>
        [Obsolete("������ CaseStat ����", true)]
        public System.String CaseStus
        {
            get
            {
                return this.caseStat;
            }
            set
            {
                this.caseStat = value;
            }
        }
        /// <summary>
        /// ��Ժ����
        /// </summary>
        [Obsolete("������ INDept ����", true)]
        public FS.FrameWork.Models.NeuObject deptIN = new NeuObject();

        /// <summary>
        /// ��Ժ����
        /// </summary>
        [Obsolete("������ OutDept ����", true)]
        public FS.FrameWork.Models.NeuObject deptOut = new NeuObject();

        /// <summary>
        /// ʵϰҽʦ����
        /// </summary>
        [Obsolete("������ PVisit.TempDoctor ����", true)]
        public System.String PraDocCode
        {
            get
            {
                return null;
            }
            //set
            //{
            //    this.praDocCode = value;
            //}
        }

        /// <summary>
        /// ʵϰҽʦ����
        /// </summary>
        [Obsolete("������ PVisit.TempDoctor ����", true)]
        public System.String PraDocName
        {
            get
            {
                return null;
            }
            //set
            //{
            //    this.praDocName = value;
            //}
        }
        /// <summary>
        /// ��Ժ���
        /// </summary>
        [Obsolete("������InHospitalDiag����",true)]
        public FS.FrameWork.Models.NeuObject InhosDiag
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
