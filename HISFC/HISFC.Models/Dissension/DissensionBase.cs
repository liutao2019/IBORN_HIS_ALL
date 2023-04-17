using System;

namespace Neusoft.HISFC.Object.Dissension
{
    /// <summary>
    /// [��������: ���׹���]
    /// [�� �� ��: ��ά]
    /// [����ʱ��: 2007-12-10]
    /// </summary>
    public class DissensionBase
    {
        public DissensionBase()
        {

        }

        #region ����

        /// <summary>
        /// ���ױ��
        /// </summary>
        private string dissID;

        /// <summary>
        /// �鵵���
        /// </summary>
        private string recordID;

        /// <summary>
        /// ״̬ 0ҽ��ƵǼǣ� 1���ҵǼǣ� 2�᰸�Ǽǣ�3����
        /// </summary>
        private string dissState;

        /// <summary>
        /// ����סԺ��
        /// </summary>
        private string patientID;

        /// <summary>
        /// �������0סԺ��1���
        /// </summary>
        private string patientTypes;

        /// <summary>
        /// ��������
        /// </summary>
        private string patientName;

        /// <summary>
        /// ��ӳʱ��
        /// </summary>
        private DateTime reflectDate;

        /// <summary>
        /// ��ӳ��ʽ
        /// </summary>
        private string reflectStyle;

        /// <summary>
        /// ��ӳ������
        /// </summary>
        private string refterName;

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        private string phoneNumber;

        /// <summary>
        /// ����ӳ����
        /// </summary>
        private string deptID;

        /// <summary>
        /// ����ӳ����/��Ͷ����
        /// </summary>
        private string partyID;

        /// <summary>
        /// ��Ͷ����ְ��
        /// </summary>
        private string partyLevel;

        /// <summary>
        /// ��Ͷ���˷���
        /// </summary>
        private string partyClass;

        /// <summary>
        /// ����������1����
        /// </summary>
        private string party1ID;

        /// <summary>
        /// ����������1ְ��
        /// </summary>
        private string party1Level;

        /// <summary>
        /// ����������1����
        /// </summary>
        private string party1Class;

        /// <summary>
        /// ����������2����
        /// </summary>
        private string party2ID;

        /// <summary>
        /// ����������2ְ��
        /// </summary>
        private string party2Level;

        /// <summary>
        /// ����������2����
        /// </summary>
        private string party2Class;

        /// <summary>
        /// ����ժҪ��ҽ�����д��
        /// </summary>
        private string abStract;

        /// <summary>
        /// ת�������¼��ҽ�����д��
        /// </summary>
        private string dealRecord;

        /// <summary>
        /// �����ˣ�ҽ�����д��
        /// </summary>
        private string registerID;

        /// <summary>
        /// ����ʱ�䣨ҽ�����д��
        /// </summary>
        private DateTime registerDate;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime occurTime;

        /// <summary>
        /// ��ʵ����
        /// </summary>
        private string factCourse;

        /// <summary>
        /// �������������Ŀ֮����'|'�ָ���
        /// </summary>
        private string aftermath;

        /// <summary>
        /// Ͷ�ߺ�ʵ���û��Զ�����Ŀ��
        /// </summary>
        private string auditState;

        /// <summary>
        /// ���Խ��飨�û��Զ�����Ŀ��
        /// </summary>
        private string suggestion;

        /// <summary>
        /// ԭ�����
        /// </summary>
        private string reason_Als;

        /// <summary>
        /// ���Ĵ�ʩ
        /// </summary>
        private string measure;

        /// <summary>
        /// ���Ҵ������
        /// </summary>
        private string deptOpinion;

        /// <summary>
        /// ������˴���
        /// </summary>
        private string reportID;

        /// <summary>
        /// �����ʱ��
        /// </summary>
        private string reportDate;

        /// <summary>
        /// �����������
        /// </summary>
        private string managerOpinion;

        /// <summary>
        /// �������δ���
        /// </summary>
        private string managerID;

        /// <summary>
        /// ����ǩ������
        /// </summary>
        private string managerDate;

        /// <summary>
        /// ҽ��Ƴ����������
        /// </summary>
        private string opinion1;

        /// <summary>
        /// ҽ���¹�С���������
        /// </summary>
        private string opinion2;

        /// <summary>
        /// ҽ�ƹ���ίԱ�����
        /// </summary>
        private string opinion3;

        /// <summary>
        /// �����м������
        /// </summary>
        private string opinion4;

        /// <summary>
        /// �㶫ʡ�������
        /// </summary>
        private string opinion5;
       
        /// <summary>
        /// һ��Ժ���н��
        /// </summary>
        private string opinion6;

        /// <summary>
        /// ����Ժ���н��
        /// </summary>
        private string opinion7;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// ��ע
        /// </summary>
        private string mark;

        #endregion

        #region ����

        public string DissID
        {
            get
            {
                return this.dissID;
            }
            set
            {
                this.dissID = value;
            }
        }

        public string RecordID
        {
            get
            {
                return this.recordID;
            }
            set
            {
                this.recordID = value;
            }
        }

        public string DissState
        {
            get
            {
                return this.dissState;
            }
            set
            {
                this.dissState = value;
            }
        }

        public string PatientID
        {
            get
            {
                return this.patientID;
            }
            set
            {
                this.patientID = value;
            }
        }

        public string PatientTypes
        {
            get
            {
                return this.patientTypes;
            }
            set
            {
                this.patientTypes = value;
            }
        }

        public string PatientName
        {
            get
            {
                return this.patientName;
            }
            set
            {
                this.patientName = value;
            }
        }

        public DateTime ReflectDate
        {
            get
            {
                return this.reflectDate;
            }
            set
            {
                this.reflectDate = value;
            }
        }

        public string ReflectStyle
        {
            get
            {
                return this.reflectStyle;
            }
            set
            {
                this.reflectStyle = value;
            }
        }

        public string RefterName
        {
            get
            {
                return this.refterName;
            }
            set
            {
                this.refterName = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                this.phoneNumber = value;
            }
        }

        public string DeptID
        {
            get
            {
                return this.deptID;
            }
            set
            {
                this.deptID = value;
            }
        }

        public string PartyID
        {
            get
            {
                return this.partyID;
            }
            set
            {
                this.partyID = value;
            }
        }

        public string PartyLevel
        {
            get
            {
                return this.partyLevel;
            }
            set
            {
                this.partyLevel = value;
            }
        }

        public string PartyClass
        {
            get
            {
                return this.partyClass;
            }
            set
            {
                this.partyClass = value;
            }
        }

        public string Party1ID
        {
            get
            {
                return this.party1ID;
            }
            set
            {
                this.party1ID = value;
            }
        }

        public string Party1Level
        {
            get
            {
                return this.party1Level;
            }
            set
            {
                this.party1Level = value;
            }
        }

        public string Party1Class
        {
            get
            {
                return this.party1Class;
            }
            set
            {
                this.party1Class = value;
            }
        }

        public string Party2ID
        {
            get
            {
                return this.party2ID;
            }
            set
            {
                this.party2ID = value;
            }
        }

        public string Party2Level
        {
            get
            {
                return this.party2Level;
            }
            set
            {
                this.party2Level = value;
            }
        }

        public string Party2Class
        {
            get
            {
                return this.party2Class;
            }
            set
            {
                this.party2Class = value;
            }
        }

        public string ABStract
        {
            get
            {
                return this.abStract;
            }
            set
            {
                this.abStract = value;
            }
        }

        public string DealRecord
        {
            get
            {
                return this.dealRecord;
            }
            set
            {
                this.dealRecord = value;
            }
        }

        public string RegisterID
        {
            get
            {
                return this.registerID;
            }
            set
            {
                this.registerID = value;
            }
        }

        public DateTime RegisterDate
        {
            get
            {
                return this.registerDate;
            }
            set
            {
                this.registerDate = value;
            }
        }

        public DateTime OccurTime
        {
            get
            {
                return this.occurTime;
            }
            set
            {
                this.occurTime = value;
            }
        }

        public DateTime FactCourse
        {
            get
            {
                return this.factCourse;
            }
            set
            {
                this.factCourse = value;
            }
        }
        #endregion
    }
}
