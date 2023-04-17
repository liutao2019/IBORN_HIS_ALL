using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Medical
{
    /// <summary>
    /// [��������: ҽ���Ű�ʵ��]
    /// [�� �� ��: wangw]
    /// [����ʱ��: 2008-04-11]
    /// </summary>
    [Serializable]
    public class WorkArrange : Neusoft.FrameWork.Models.NeuObject
    {
        #region ���췽��

        public WorkArrange()
        {
        }

        #endregion

        #region �ֶ�

        /// <summary>
        /// ���  RES �Ű� OVER ֵ��
        /// </summary>
        private string arrangeType;

        /// <summary>
        /// ��Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��/ֵ�� ����
        /// </summary>
        private Neusoft.HISFC.Models.Base.Department dept = new Neusoft.HISFC.Models.Base.Department();

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime workDate;

        /// <summary>
        /// ���ڰ��  �洢������ GOA_MEA_ATTENDTYPE
        /// </summary>
        private Neusoft.HISFC.Models.Medical.Attendance attend = new Neusoft.HISFC.Models.Medical.Attendance();

        /// <summary>
        /// ȱ��ԭ��  ��Ӧ������� GOAABSEN
        /// </summary>
        private string absentEeism;

        /// <summary>
        /// �Ű���ʼʱ��  ��������
        /// </summary>
        private DateTime beginTime;

        /// <summary>
        /// �Ű���ֹʱ��  ��������
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// ��Ч�Ա�־ 1 ��Ч 0 ͣ�� 
        /// </summary>
        private string vaildFlag;

        /// <summary>
        /// ʵ�ʳ���  ��չ�� �洢��Ӧ������������
        /// </summary>
        private string infactType;

        /// <summary>
        /// �Ű������Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �Ƿ���� 1��/0��
        /// </summary>
        private string checkFlag;

        /// <summary>
        /// ��˲�����Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment checkOper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment apprOper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// �Ű����
        /// </summary>
        public string ArrangeType
        {
            get
            {
                return this.arrangeType;
            }
            set
            {
                this.arrangeType = value;
            }
        }

        /// <summary>
        /// ��Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.Employee Employee
        {
            get
            {
                return this.employee;
            }
            set
            {
                this.employee = value;
            }
        }

        /// <summary>
        /// ��/ֵ�� ����
        /// </summary>
        public Neusoft.HISFC.Models.Base.Department Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime WorkDate
        {
            get
            {
                return this.workDate;
            }
            set
            {
                this.workDate = value;
            }
        }

        /// <summary>
        /// ���ڰ��  �洢������ GOA_MEA_ATTENDTYPE
        /// </summary>
        public Neusoft.HISFC.Models.Medical.Attendance Attend
        {
            get
            {
                return this.attend;
            }
            set
            {
                this.attend = value;
            }
        }

        /// <summary>
        /// ȱ��ԭ��  ��Ӧ������� GOAABSEN
        /// </summary>
        public string AbsentEeism
        {
            get
            {
                return this.absentEeism;
            }
            set
            {
                this.absentEeism = value;
            }
        }

        /// <summary>
        /// �Ű���ʼʱ��  ��������
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
        /// �Ű���ֹʱ��  ��������
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
        /// ��Ч�Ա�־ 1 ��Ч 0 ͣ�� 
        /// </summary>
        public string VaildFlag
        {
            get
            {
                return this.vaildFlag;
            }
            set
            {
                this.vaildFlag = value;
            }
        }

        /// <summary>
        /// ʵ�ʳ���  ��չ�� �洢��Ӧ������������
        /// </summary>
        public string InfactType
        {
            get
            {
                return this.infactType;
            }
            set
            {
                this.infactType = value;
            }
        }

        /// <summary>
        /// �Ű������Ϣ
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
        /// �Ƿ���� 1��/0��
        /// </summary>
        public string CheckFlag
        {
            get
            {
                return this.checkFlag;
            }
            set
            {
                this.checkFlag = value;
            }
        }

        /// <summary>
        /// ��˲�����Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment CheckOper
        {
            get
            {
                return this.checkOper;
            }
            set
            {
                this.checkOper = value;
            }
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment ApprOper
        {
            get
            {
                return this.apprOper;
            }
            set
            {
                this.apprOper = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>ҽ���Ű�(ҽ������ʿ)ʵ��</returns>
        public new WorkArrange Clone()
        {
            WorkArrange workArrange = base.Clone() as WorkArrange;

            workArrange.employee = this.employee.Clone();
            workArrange.dept = this.dept.Clone();
            workArrange.attend = this.attend.Clone();
            workArrange.oper = this.oper.Clone();
            workArrange.checkOper = this.checkOper.Clone();
            workArrange.apprOper = this.apprOper.Clone();

            return workArrange;
        }

        #endregion
    }
}
