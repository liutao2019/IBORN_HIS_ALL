using Neusoft.HISFC.Models.PhysicalExamination;
using Neusoft.HISFC.Models.Base;
using System;

namespace Neusoft.HISFC.Models.Medical
{
	/// <summary>
	/// [��������: ҽ�ƾ���ʵ��]
	/// [�� �� ��: ����ΰ]
	/// [����ʱ��: 2006-09-05]
    /// 
    /// [�� �� ��:��ά]
    /// [�޸�ʱ��:2007-12-12]
    /// 
    /// [�� �� ��:shizj]
    /// [�޸�ʱ��:2008-3-25]
	/// </summary>
    [Serializable]
	public class Dissension:Neusoft.FrameWork.Models.NeuObject
    {
        #region ���췽��

        public Dissension()
		{
        }

        #endregion

        #region ����

        /// <summary>
		/// ���ױ��
		/// </summary>
        private string dissID = string.Empty;

        /// <summary>
        /// �鵵���
        /// </summary>
        private string recordID;

        /// <summary>
        /// ״̬ 0ҽ��ƵǼ�infirmaryRegister�� 1���ҵǼ�deptRegister�� 2�᰸�Ǽ�endRegister��3����cancle
        /// </summary>
        private dissensionType state;

        /// <summary>
        /// ����סԺ��
        /// </summary>
        private string patientID;

        /// <summary>
        /// �������2סԺ��1���
        /// </summary>
        private Neusoft.HISFC.Models.Base.ServiceTypes patientType = Neusoft.HISFC.Models.Base.ServiceTypes.I;

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
        private Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ӳ����/��Ͷ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject partyCode = new Neusoft.FrameWork.Models.NeuObject();

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
        private string party1Code;

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
        private string party2Code;

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
        private string aBstract;

        /// <summary>
        /// ת�������¼��ҽ�����д��
        /// </summary>
        private string dealRecord;

        /// <summary>
        /// �����ˣ�ҽ�����д��
        /// </summary>
        private OperEnvironment registerCode = new OperEnvironment();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime occurDate;

        /// <summary>
        /// ��ʵ����
        /// </summary>
        private string factCourse;

        /// <summary>
        /// �������������Ŀ֮����'|'�ָ���
        /// </summary>
        private string afterMath;

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
        private string reasonAls;

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
        private OperEnvironment reportCode = new OperEnvironment();

        /// <summary>
        /// �����������
        /// </summary>
        private string managerOpinion;

        /// <summary>
        /// �������δ���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject managerCode = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ǩ������
        /// </summary>
        private DateTime managerDate;

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
        ///����Ժ���н��
        /// </summary>
        private string opinion7;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��ע
        /// </summary>
        private string mark;

        #endregion

        #region ����

        /// <summary>
        /// ���ױ��
        /// </summary>
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

        /// <summary>
        /// �鵵���
        /// </summary>
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

        /// <summary>
        /// ״̬ 0ҽ��ƵǼǣ� 1���ҵǼǣ� 2�᰸�Ǽǣ�3����
        /// </summary>
        public dissensionType State
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
        /// ����סԺ��
        /// </summary>
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

        /// <summary>
        /// �������0סԺ��1���
        /// </summary>
        public Neusoft.HISFC.Models.Base.ServiceTypes PatientType
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
        /// ��������
        /// </summary>
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

        /// <summary>
        /// ��ӳʱ��
        /// </summary>
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

        /// <summary>
        /// ��ӳ��ʽ
        /// </summary>
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

        /// <summary>
        /// ��ӳ������
        /// </summary>
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

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
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

        /// <summary>
        /// ����ӳ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;

                base.ID = value.ID;
                base.Name = value.Name;
            }
        }

        /// <summary>
        /// ����ӳ����/��Ͷ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject PartyCode
        {
            get
            {
                return this.partyCode;
            }
            set
            {
                this.partyCode = value;

                base.ID = value.ID;
                base.Name = value.Name;
            }
        }

        /// <summary>
        /// ��Ͷ����ְ��
        /// </summary>
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

        /// <summary>
        /// ��Ͷ���˷���
        /// </summary>
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

        /// <summary>
        /// ����������1����
        /// </summary>
        public string Party1Code
        {
            get
            {
                return this.party1Code;
            }
            set
            {
                this.party1Code = value;
            }
        }

        /// <summary>
        /// ����������1ְ��
        /// </summary>
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

        /// <summary>
        /// ����������1����
        /// </summary>
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

        /// <summary>
        /// ����������2����
        /// </summary>
        public string Party2Code
        {
            get
            {
                return this.party2Code;
            }
            set
            {
                this.party2Code = value;
            }
        }

        /// <summary>
        /// ����������2ְ��
        /// </summary>
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

        /// <summary>
        /// ����������2����
        /// </summary>
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

        /// <summary>
        /// ����ժҪ��ҽ�����д��
        /// </summary>
        public string ABstract
        {
            get
            {
                return this.aBstract;
            }
            set
            {
                this.aBstract = value;
            }
        }

        /// <summary>
        /// ת�������¼��ҽ�����д��
        /// </summary>
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

        /// <summary>
        /// �����ˣ�ҽ�����д��
        /// </summary>
        public OperEnvironment RegisterCode
        {
            get
            {
                return this.registerCode;
            }
            set
            {
                this.registerCode = value;
            }
        }

        ///// <summary>
        ///// ����ʱ�䣨ҽ�����д��
        ///// </summary>
        //public DateTime RegisterDate
        //{
        //    get
        //    {
        //        return this.registerDate;
        //    }
        //    set
        //    {
        //        this.registerDate = value;
        //    }
        //}

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OccurDate
        {
            get
            {
                return this.occurDate;
            }
            set
            {
                this.occurDate = value;
            }
        }

        /// <summary>
        /// ��ʵ����
        /// </summary>
        public string FactCourse
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

        /// <summary>
        /// �������������Ŀ֮����'|'�ָ���
        /// </summary>
        public string AfterMath
        {
            get
            {
                return this.afterMath;
            }
            set
            {
                this.afterMath = value;
            }
        }

        /// <summary>
        /// Ͷ�ߺ�ʵ���û��Զ�����Ŀ��
        /// </summary>
        public string AuditState
        {
            get
            {
                return this.auditState;
            }
            set
            {
                this.auditState = value;
            }
        }

        /// <summary>
        /// ���Խ��飨�û��Զ�����Ŀ��
        /// </summary>
        public string Suggestion
        {
            get
            {
                return this.suggestion;
            }
            set
            {
                this.suggestion = value;
            }
        }

        /// <summary>
        /// ԭ�����
        /// </summary>
        public string ReasonAls
        {
            get
            {
                return this.reasonAls;
            }
            set
            {
                this.reasonAls = value;
            }
        }

        /// <summary>
        /// ���Ĵ�ʩ
        /// </summary>
        public string Measure
        {
            get
            {
                return this.measure;
            }
            set
            {
                this.measure = value;
            }
        }

        /// <summary>
        /// ���Ҵ������
        /// </summary>
        public string DeptOpinion
        {
            get
            {
                return this.deptOpinion;
            }
            set
            {
                this.deptOpinion = value;
            }
        }

        /// <summary>
        /// ������˴���
        /// </summary>
        public OperEnvironment ReportCode
        {
            get
            {
                return this.reportCode;
            }
            set
            {
                this.reportCode = value;
            }
        }

        ///// <summary>
        ///// �����ʱ��
        ///// </summary>
        //public DateTime ReportDate
        //{
        //    get
        //    {
        //        return this.report_Date;
        //    }
        //    set
        //    {
        //        this.report_Date = value;
        //    }
        //}

        /// <summary>
        /// �����������
        /// </summary>
        public string ManagerOpinion
        {
            get
            {
                return this.managerOpinion;
            }
            set
            {
                this.managerOpinion = value;
            }
        }

        /// <summary>
        /// �������δ���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject ManagerCode
        {
            get
            {
                return this.managerCode;
            }
            set
            {
                this.managerCode = value;
            }
        }

        /// <summary>
        /// ����ǩ������
        /// </summary>
        public DateTime ManagerDate
        {
            get
            {
                return this.managerDate;
            }
            set
            {
                this.managerDate = value;
            }
        }

        /// <summary>
        /// ҽ��Ƴ����������
        /// </summary>
        public string Opinion1
        {
            get
            {
                return this.opinion1;
            }
            set
            {
                this.opinion1 = value;
            }
        }

        /// <summary>
        /// ҽ���¹�С���������
        /// </summary>
        public string Opinion2
        {
            get
            {
                return this.opinion2;
            }
            set
            {
                this.opinion2 = value;
            }
        }

        /// <summary>
        /// ҽ�ƹ���ίԱ�����
        /// </summary>
        public string Opinion3
        {
            get
            {
                return this.opinion3;
            }
            set
            {
                this.opinion3 = value;
            }
        }

        /// <summary>
        /// �����м������
        /// </summary>
        public string Opinion4
        {
            get
            {
                return this.opinion4;
            }
            set
            {
                this.opinion4 = value;
            }
        }

        /// <summary>
        /// �㶫ʡ�������
        /// </summary>
        public string Opinion5
        {
            get
            {
                return this.opinion5;
            }
            set
            {
                this.opinion5 = value;
            }
        }

        /// <summary>
        /// һ��Ժ���н��
        /// </summary>
        public string Opinion6
        {
            get
            {
                return this.opinion6;
            }
            set
            {
                this.opinion6 = value;
            }
        }

        /// <summary>
        ///����Ժ���н��
        /// </summary>
        public string Opinion7
        {
            get
            {
                return this.opinion7;
            }
            set
            {
                this.opinion7 = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
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
        /// ��ע
        /// </summary>
        public string Mark
        {
            get
            {
                return this.mark;
            }
            set
            {
                this.mark = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���Dissensionʵ�� ʧ�ܷ���null</returns>
        public new Dissension Clone()
        {
            Dissension dissension = base.Clone() as Dissension;

            dissension.Oper = this.Oper.Clone();
            dissension.RegisterCode = this.RegisterCode.Clone();
            dissension.ReportCode = this.ReportCode.Clone();
            dissension.PartyCode = this.PartyCode.Clone();

            return dissension;
        }
        #endregion


    }
    /// <summary>
    /// ����״̬
    /// </summary>
    public enum dissensionType
    {
        
        
        /// <summary>
        /// 0ҽ��ƵǼ�
        /// </summary>
        infirmaryRegister,
        /// <summary>
        /// 1���ҵǼ�
        /// </summary>
        deptRegister,
        /// <summary>
        ///  2�᰸�Ǽ�
        /// </summary>
        endRegister,
        /// <summary>
        /// 3����
        /// </summary>
         cancle
    }
}

