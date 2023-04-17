using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Models.PhysicalExamination
{
    /// <summary>
    /// Register<br></br>
    /// [��������: �����Ա������Ϣ�Ǽ�]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-03-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class Register : FS.HISFC.Models.RADT.Patient
    {
        #region  ˽�б���
        /// <summary>
        /// ��쵵���� 
        /// </summary>
        private string archivesNO = string.Empty;

        /// <summary>
        /// �����ˮ��
        /// </summary>
        private string clinicNO = string.Empty;

        /// <summary>
        /// ҩ�����
        /// </summary>
        private string isAnaphy = string.Empty;

        /// <summary>
        /// ������
        /// </summary>
        private string identityLevel = string.Empty;

        /// <summary>
        /// Ѫѹ���ֵ
        /// </summary>
        private string bloodPressTop = string.Empty;

        /// <summary>
        /// Ѫѹ���ֵ
        /// </summary>
        private string bloodPressDown = string.Empty;

        /// <summary>
        /// �ۼ��Էѽ��
        /// </summary>
        private decimal ownCost;

        /// <summary>
        /// ���λ�ʿ
        /// </summary>
        private FS.FrameWork.Models.NeuObject dutyNuse = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��쵥λ
        /// </summary>
        private FS.FrameWork.Models.NeuObject companyInfo = new FS.FrameWork.Models.NeuObject();


        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.Models.RADT.Kin kin = new FS.HISFC.Models.RADT.Kin();
        /// <summary>
        /// ��չ���1
        /// </summary>
        private string extCha = string.Empty;

        /// <summary>
        /// ��չ���2
        /// </summary>
        private System.DateTime extDate;

        /// <summary>
        /// ��չ���3 
        /// </summary>
        private int extNum;

        /// <summary>
        /// ��չ���4 
        /// </summary>
        private string extCha1 = string.Empty;

        /// <summary>
        /// ��չ��� 6
        /// </summary>
        private int extNum1;

        /// <summary>
        /// ��չ��� 5
        /// </summary>
        private System.DateTime extDate1;

        /// <summary>
        /// �Һſ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject regDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��������
        /// </summary>
        private string collectivityCode = string.Empty;

        /// <summary>
        /// �������Ǽ�����
        /// </summary>
        private System.DateTime collectivityTime;

        /// <summary>
        /// ��쵥λ ����
        /// </summary>
        private string companyDeptName = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        private string companyDeptSeq = string.Empty;

        /// <summary>
        /// ����������Ϣ��
        /// </summary>
        private FS.HISFC.Models.RADT.PDisease disease = new FS.HISFC.Models.RADT.PDisease();
        /// <summary>
        /// ����������� �� �й���죬�������� 
        /// </summary>
        private FS.FrameWork.Models.NeuObject specalExamType;

        /// <summary>
        /// ����Ա����
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ������Ŀ
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        private ArrayList itemList = new ArrayList();
        private string recipeSequence = string.Empty;//���һ�η�Ʊ��Ϻ�
        #endregion

        #region ����
        /// <summary>
        /// ����������Ϣ��
        /// </summary>
        public FS.HISFC.Models.RADT.PDisease Disease
        {
            get
            {
                return disease;
            }
            set
            {
                disease = value;
            }
        }
        /// <summary>
        /// ������ 
        /// </summary>
        public FS.HISFC.Models.RADT.Kin Kin
        {
            get
            {
                return kin;
            }
            set
            {
                kin = value;
            }
        }
        public string RecipeSequence
        {
            get
            {
                return recipeSequence;
            }
            set
            {
                recipeSequence = value;
            }
        }
        /// <summary>
        /// ��쵥λ�ڲ����
        /// </summary>
        public string CompanyDeptSeq
        {
            get
            {
                return companyDeptSeq;
            }
            set
            {
                companyDeptSeq = value;
            }
        }

        /// <summary>
        /// ��쵥λ����
        /// </summary>
        public string CompanyDeptName
        {
            get
            {
                return companyDeptName;
            }
            set
            {
                companyDeptName = value;
            }
        }

        /// <summary>
        /// �������Ǽ�����
        /// </summary>
        public System.DateTime CollectivityTime
        {
            get
            {
                return collectivityTime;
            }
            set
            {
                collectivityTime = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string CollectivityCode
        {
            get
            {
                return collectivityCode;
            }
            set
            {
                collectivityCode = value;
            }
        }

        /// <summary>
        ///  //��չ���5
        /// </summary>
        public System.DateTime ExtDate1
        {
            get
            {
                return extDate1;
            }
            set
            {
                extDate1 = value;
            }
        }

        /// <summary>
        ///  //��չ���2
        /// </summary>
        public System.DateTime ExtDate
        {
            get
            {
                return extDate;
            }
            set
            {
                extDate = value;
            }
        }

        /// <summary>
        /// ��չ���3 
        /// </summary>
        public int ExtNum1
        {
            get
            {
                return extNum1;
            }
            set
            {
                extNum1 = value;
            }
        }

        /// <summary>
        /// ��չ���3 
        /// </summary>
        public int ExtNum
        {
            get
            {
                return extNum;
            }
            set
            {
                extNum = value;
            }
        }

        /// <summary>
        /// ��չ���
        /// </summary>
        public string ExtCha1
        {
            get
            {
                return extCha1;
            }
            set
            {
                extCha1 = value;
            }
        }

        /// <summary>
        /// ��չ���
        /// </summary>
        public string ExtCha
        {
            get
            {
                return extCha;
            }
            set
            {
                extCha = value;
            }
        }

        /// <summary>
        /// �Һſ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject RegDept
        {
            get
            {
                return regDept;
            }
            set
            {
                regDept = value;
            }
        }

        /// <summary>
        /// ���λ�ʿ
        /// </summary>
        public FS.FrameWork.Models.NeuObject DutyNuse
        {
            get
            {
                return dutyNuse;
            }
            set
            {
                dutyNuse = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public decimal OwnCost
        {
            get
            {
                return ownCost;
            }
            set
            {
                ownCost = value;
            }
        }

        /// <summary>
        /// Ѫѹ���ֵ
        /// </summary>
        public string BloodPressDown
        {
            get
            {
                return bloodPressDown;
            }
            set
            {
                bloodPressDown = value;
            }
        }

        /// <summary>
        /// Ѫѹ���ֵ
        /// </summary>
        public string BloodPressTop
        {
            get
            {
                return bloodPressTop;
            }
            set
            {
                bloodPressTop = value;
            }
        }

        /// <summary>
        /// ����1 ���� 2  ����
        /// </summary>
        public string ChkKind;

        /// <summary>
        /// //��ʷ
        /// </summary>
        public string CaseHospital;

        /// <summary>
        ///  //��ͥ��ʷ
        /// </summary>
        public string HomeCase;

        /// <summary>
        /// �������
        /// </summary>
        public System.DateTime CheckTime;

        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        public ArrayList ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
            }
        }

        /// <summary>
        /// ������ 
        /// </summary>
        public int ChkSortNO;

        /// <summary>
        /// ��������
        /// </summary>
        public string TransType;

        /// <summary>
        ///�����Ŀ
        /// </summary>
        public FS.HISFC.Models.Fee.Item.Undrug Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string IdentityLevel
        {
            get
            {
                return identityLevel;
            }
            set
            {
                identityLevel = value;
            }
        }

        /// <summary>
        /// ��쵥λ 
        /// </summary>
        public FS.FrameWork.Models.NeuObject Company
        {
            get
            {
                return companyInfo;
            }
            set
            {
                companyInfo = value;
            }
        }

        /// <summary>
        /// ҩ����� 
        /// </summary>
        public string AnaphyFlag
        {
            get
            {
                return isAnaphy;
            }
            set
            {
                isAnaphy = value;
            }
        }

        
        /// <summary>
        /// ����������
        /// </summary>
        public string ArchivesNO
        {
            get
            {
                return archivesNO;
            }
            set
            {
                archivesNO = value;
            }
        }

        /// <summary>
        /// ����������� �� �й���죬�������� 
        /// </summary>
        public FS.FrameWork.Models.NeuObject SpecalChkType
        {
            get
            {
                if (specalExamType == null)
                {
                    specalExamType = new FS.FrameWork.Models.NeuObject();
                }
                return specalExamType;
            }
            set
            {
                specalExamType = value;
            }
        }

        /// <summary>
        /// �����ˮ��
        /// </summary>
        public string ChkClinicNo
        {
            get
            {
                return clinicNO;
            }
            set
            {
                clinicNO = value;
            }
        }

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Operator
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
        #endregion

        #region ��¡����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new Register Clone()
        {
            Register obj = base.Clone() as Register;
            obj.item = this.item.Clone();
            obj.Company = this.companyInfo.Clone();
            //obj.PatientInfo = this.PatientInfo.Clone();
            obj.regDept = this.regDept.Clone();
            obj.Operator = this.Operator.Clone();
            obj.dutyNuse = DutyNuse.Clone();
            obj.operDept = this.operDept.Clone();
            return obj;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ����ʵ����
        /// </summary>
        [Obsolete("���� �̳� Patient", true)]
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        [Obsolete("���� �� ArchivesNO ����", true)]
        public string CHKID
        {
            get
            {
                return archivesNO;
            }
            set
            {
                archivesNO = value;
            }
        }
        /// <summary>
        /// ��쵥λ 
        /// </summary>
        [Obsolete("���� �� Company ����", true)]
        public FS.FrameWork.Models.NeuObject ChkCompany
        {
            get
            {
                return companyInfo;
            }
            set
            {
                companyInfo = value;
            }
        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        [Obsolete("���� �� operInfo ��Ŀ��Ҵ���", true)]
        public FS.FrameWork.Models.NeuObject OperDept
        {
            get
            {
                if (operDept == null)
                {
                    operDept = new FS.FrameWork.Models.NeuObject();
                }
                return operDept;
            }
            set
            {
                operDept = value;
            }
        }
        /// <summary>
        /// �������Ǽ�����
        /// </summary>
        [Obsolete("���� �� CollectivityTime����", true)]
        public System.DateTime CollectivityDate
        {
            get
            {
                return collectivityTime;
            }
            set
            {
                collectivityTime = value;
            }
        }
        /// <summary>
        /// ��쵥λ����
        /// </summary>
        [Obsolete("���� �� CompanyDeptName����", true)]
        public string DeptName
        {
            get
            {
                return companyDeptName;
            }
            set
            {
                companyDeptName = value;
            }
        }
        /// <summary>
        /// ��쵥λ�ڲ����
        /// </summary>
        [Obsolete("���� �� CompanyDeptSeq����", true)]
        public string DeptSeq
        {
            get
            {
                return companyDeptSeq;
            }
            set
            {
                companyDeptSeq = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        [Obsolete("���� ��IdentityLevel����", true)]
        public string ChkLevel
        {
            get
            {
                return identityLevel;
            }
            set
            {
                identityLevel = value;
            }
        }
        #endregion

        #region ���ڱ���

        [Obsolete("���� �� ItemList ����", true)]
        public ArrayList chkItemList = new ArrayList();

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        [Obsolete("���� �̳�Patient ����")]
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        #endregion

    }
}
