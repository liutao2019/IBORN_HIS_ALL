using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// [��������: ��ת�������Գɼ���]<br></br>
    /// [�� �� ��: ŷ�ܳ�]<br></br>
    /// [����ʱ��: 2008-07]<br></br>
    /// </summary>
    [System.Serializable]
    public class LastScore : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// ���˵���
        /// </summary>
        string testBillCode;
        /// <summary>
        /// Ա��
        /// </summary>
        Neusoft.HISFC.Models.Base.Employee empl = new Neusoft.HISFC.Models.Base.Employee();
        /// <summary>
        /// ���ڵ�λ
        /// </summary>
        string compannyName;
        /// <summary>
        /// רҵ
        /// </summary>
        string speciality;
        /// <summary>
        /// רҵ����
        /// </summary>
        string specialityTheory;
        /// <summary>
        /// �ɼ�
        /// </summary>
        decimal specialityTheoryScore;
        /// <summary>
        /// רҵ����
        /// </summary>
        string foreignLanguage;
        /// <summary>
        /// �������ݳɼ�
        /// </summary>
        decimal commonScore;
        /// <summary>
        /// רҵ���ֳɼ�
        /// </summary>
        decimal specialityScore;
        /// <summary>
        /// �ϼƳɼ�
        /// </summary>
        decimal totalScore;
        /// <summary>
        /// ��ע
        /// </summary>
        string remark;
        /// <summary>
        /// ����Ա
        /// </summary>
        Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����
        /// <summary>
        /// ���˵���
        /// </summary>
        public string TestBillCode
        {
            get
            {
                return testBillCode;
            }
            set
            {
                testBillCode = value;
            }
        }

        /// <summary>
        /// Ա��
        /// </summary>
        public Neusoft.HISFC.Models.Base.Employee Empl
        {
            get
            {
                return empl;
            }
            set
            {
                empl = value;
            }
        }

        /// <summary>
        /// ���ڵ�λ
        /// </summary>
        public string CompannyName
        {
            get
            {
                return compannyName;
            }
            set
            {
                compannyName = value;
            }
        }

        /// <summary>
        /// רҵ
        /// </summary>
        public string Speciality
        {
            get
            {
                return speciality;
            }
            set
            {
                speciality = value;
            }
        }

        /// <summary>
        /// רҵ����
        /// </summary>
        public string SpecialityTheory
        {
            get
            {
                return specialityTheory;
            }
            set
            {
                specialityTheory = value;
            }
        }

        /// <summary>
        /// רҵ���۳ɼ�
        /// </summary>
        public decimal SpecialityTheoryScore
        {
            get
            {
                return specialityTheoryScore;
            }
            set
            {
                specialityTheoryScore = value;
            }
        }

        /// <summary>
        /// רҵ����
        /// </summary>
        public string ForeignLanguage
        {
            get
            {
                return foreignLanguage;
            }
            set
            {
                foreignLanguage = value;
            }
        }

        /// <summary>
        /// �������ݳɼ�
        /// </summary>
        public decimal CommonScore
        {
            get
            {
                return commonScore;
            }
            set
            {
                commonScore = value;
            }
        }

        /// <summary>
        /// רҵ���ֳɼ�
        /// </summary>
        public decimal SpecialityScore
        {
            get
            {
                return specialityScore;
            }
            set
            {
                specialityScore = value;
            }
        }

        /// <summary>
        /// �ϼƳɼ�
        /// </summary>
        public decimal TotalScore
        {
            get
            {
                return totalScore;
            }
            set
            {
                totalScore = value;
            }
        }

        /// <summary>
        /// ��ע
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
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }
        #endregion

        #region ����
        public new LastScore Clone()
        {
            LastScore lastScore = base.Clone() as LastScore;
            lastScore.Empl = this.Empl;
            lastScore.Oper = this.Oper;
            return lastScore;
        }
        #endregion
    }
}
