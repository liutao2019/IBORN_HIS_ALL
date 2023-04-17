using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// [��������: �ɼ�����]<br></br>
    /// [�� �� ��: ŷ�ܳ�]<br></br>
    /// [����ʱ��: 2008-07]<br></br>
    /// </summary>
    [System.Serializable]
    public class Score : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// ���˼ƻ�����
        /// </summary>
        string testPlanID;

        /// <summary>
        /// Ա��
        /// </summary>
        Neusoft.FrameWork.Models.NeuObject employee = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        Neusoft.FrameWork.Models.NeuObject depart = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ʱ��
        /// </summary>
        DateTime inDepartDate;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        DateTime testDate;

        /// <summary>
        /// ҽ��ҽ��ɼ�
        /// </summary>
        decimal score1;

        /// <summary>
        /// ����ɼ�
        /// </summary>
        decimal score2;

        /// <summary>
        /// ֵ���Ӱ�ɼ�
        /// </summary>
        decimal score3;

        /// <summary>
        /// �鷿�ɼ�
        /// </summary>
        decimal score4;

        /// <summary>
        /// ���������ɼ�
        /// </summary>
        decimal score5;

        /// <summary>
        /// ���������ɼ�
        /// </summary>
        decimal score6;

        /// <summary>
        /// �����ɼ�
        /// </summary>
        decimal score7;

        /// <summary>
        /// �Ӽ��ɼ�
        /// </summary>
        decimal score8;

        /// <summary>
        /// �ϼ�
        /// </summary>
        decimal totalScore;

        /// <summary>
        /// ��Ժʱ��
        /// </summary>
        DateTime inCompannyDate;

        /// <summary>
        /// ���ڵ�λ
        /// </summary>
        string compannyName;

        /// <summary>
        /// ����Ա
        /// </summary>
        Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ���˼ƻ�����
        /// </summary>
        public string TestPlanID
        {
            get
            {
                return testPlanID;
            }
            set
            {
                testPlanID = value;
            }
        }

        /// <summary>
        /// Ա��
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Depart
        {
            get
            {
                return depart;
            }
            set
            {
                depart = value;
            }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime InDepartDate
        {
            get
            {
                return inDepartDate;
            }
            set
            {
                inDepartDate = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime TestDate
        {
            get
            {
                return testDate;
            }
            set
            {
                testDate = value;
            }
        }

        /// <summary>
        /// ҽ��ҽ��ɼ�
        /// </summary>
        public decimal Score1
        {
            get
            {
                return score1;
            }
            set
            {
                score1 = value;
            }
        }

        /// <summary>
        /// ����ɼ�
        /// </summary>
        public decimal Score2
        {
            get
            {
                return score2;
            }
            set
            {
                score2 = value;
            }
        }

        /// <summary>
        /// ֵ���Ӱ�ɼ�
        /// </summary>
        public decimal Score3
        {
            get
            {
                return score3;
            }
            set
            {
                score3 = value;
            }
        }

        /// <summary>
        /// �鷿�ɼ�
        /// </summary>
        public decimal Score4
        {
            get
            {
                return score4;
            }
            set
            {
                score4 = value;
            }
        }

        /// <summary>
        /// ���������ɼ�
        /// </summary>
        public decimal Score5
        {
            get
            {
                return score5;
            }
            set
            {
                score5 = value;
            }
        }

        /// <summary>
        /// ���������ɼ�
        /// </summary>
        public decimal Score6
        {
            get
            {
                return score6;
            }
            set
            {
                score6 = value;
            }
        }

        /// <summary>
        /// �����ɼ�
        /// </summary>
        public decimal Score7
        {
            get
            {
                return score7;
            }
            set
            {
                score7 = value;
            }
        }

        /// <summary>
        /// �Ӽ��ɼ�
        /// </summary>
        public decimal Score8
        {
            get
            {
                return score8;
            }
            set
            {
                score8 = value;
            }
        }

        /// <summary>
        /// �ϼ�
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
        /// ��Ժʱ��
        /// </summary>
        public DateTime InCompannyDate
        {
            get
            {
                return inCompannyDate;
            }
            set
            {
                inCompannyDate = value;
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

        public new Score Clone()
        {
            Score score = base.Clone() as Score;
            score.Depart = this.Depart;
            score.Employee = this.Employee;
            score.Oper = this.Oper;
            return score;
        }

        #endregion
    }
}
