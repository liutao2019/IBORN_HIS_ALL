using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>[��������: ���˼ƻ�����]</br>
    /// <br>[�� �� ��: ŷ�ܳ�]</br>
    /// <br>[����ʱ��: 2008-07]</br>
    /// </summary>
    [System.Serializable]
    public class TestPlan : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// �ƻ�����
        /// </summary>
        string testPlanID;

        /// <summary>
        /// ���˼ƻ�����
        /// </summary>
        string testPlanCode;

        /// <summary>
        /// Ա��
        /// </summary>
        Neusoft.FrameWork.Models.NeuObject employee = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        Neusoft.FrameWork.Models.NeuObject depart = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        DateTime outDepartDate;

        /// <summary>
        /// ��������
        /// </summary>
        DateTime testDate;

        /// <summary>
        /// ������ר������
        /// </summary>
        string expertList1;

        /// <summary>
        /// ����������
        /// </summary>
        string expertList2;

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
        /// �ƻ�����
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
        /// ���˼ƻ�����
        /// </summary>
        public string TestPlanCode
        {
            get 
            {
                return testPlanCode; 
            }
            set 
            {
                testPlanCode = value; 
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
        /// ��������
        /// </summary>
        public DateTime OutDepartDate
        {
            get
            {
                return outDepartDate;
            }
            set
            {
                outDepartDate = value;
            }
        }

        /// <summary>
        /// ��������
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
        /// ������ר������
        /// </summary>
        public string ExpertList1
        {
            get
            {
                return expertList1;
            }
            set
            {
                expertList1 = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public string ExpertList2
        {
            get
            {
                return expertList2;
            }
            set
            {
                expertList2 = value;
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
        public new TestPlan Clone()
        {
            TestPlan testPlan = base.Clone() as TestPlan;
            testPlan.Employee = this.Employee;
            testPlan.Depart = this.Depart;
            testPlan.Oper = this.Oper;
            return testPlan;
        }
        #endregion
    }



}
