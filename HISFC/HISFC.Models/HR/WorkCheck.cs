using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// Neusoft.HISFC.Models.HR.WorkCheck<br></br>
    /// [��������: ����ʵ�� ��Ӧ��Ϊ goa_check_work]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-07-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class WorkCheck : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�

        /// <summary>
        /// ����·�
        /// </summary>
        private string yearAndMonth = "";

        /// <summary>
        /// Ա��ʵ�塣
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��������
        /// </summary>
        private string checkType = "";

        /// <summary>
        /// ��������/����
        /// </summary>
        private decimal checkNum = 0;

        /// <summary>
        /// ��������
        /// </summary>
        private decimal monthDays = 0;

        /// <summary>
        /// ���¹���������
        /// </summary>
        private decimal monthWorkDays = 0;
        
        /// <summary>
        /// ����״̬0���棬1�ύ
        /// </summary>
        private string  checkStatus;

        /// <summary>
        /// ����Ա��
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ����·�
        /// </summary>
        public string YearAndMonth
        {
            get
            {
                return yearAndMonth;
            }
            set
            {
                yearAndMonth = value;
            }
        }

        /// <summary>
        /// Ա��ʵ��
        /// </summary>
        public Neusoft.HISFC.Models.Base.Employee Employee
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
        /// ȱ������
        /// </summary>
        public string CheckType
        {
            get
            {
                return checkType;
            }
            set
            {
                checkType = value;
            }
        }

        /// <summary>
        /// ȱ������
        /// </summary>
        public decimal CheckNum
        {
            get
            {
                return checkNum;
            }
            set
            {
                checkNum = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal MonthDays
        {
            get
            {
                return monthDays;
            }
            set
            {
                monthDays = value;
            }
        }

        /// <summary>
        /// ���¹���������
        /// </summary>
        public decimal MonthWorkDays
        {
            get
            {
                return monthWorkDays;
            }
            set
            {
                monthWorkDays = value;
            }
        }

        /// <summary>
        /// ����״̬0���棬1�ύ
        /// </summary>
        public string CheckStatus
        {
            get
            {
                return checkStatus;
            }
            set
            {
                checkStatus = value;
            }
        }

        /// <summary>
        /// ����Ա��
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
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new WorkCheck Clone()
        {
            WorkCheck workCheck = base.Clone() as WorkCheck;

            workCheck.Employee = this.Employee.Clone();
            workCheck.Oper = this.Oper.Clone();

            return workCheck;
        }
        #endregion
    }
}
