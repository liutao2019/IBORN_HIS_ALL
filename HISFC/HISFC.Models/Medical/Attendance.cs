using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.FrameWork.Models;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.Medical
{
    /// <summary>
    /// [��������: �������ʵ��]
    /// [�� �� ��: wangw]
    /// [����ʱ��: 2008-04-01]
    /// </summary>
    [Serializable]
    public class Attendance : Neusoft.HISFC.Models.Base.Spell
    {
        #region ���췽��

        public Attendance()
        {

        }

        #endregion

        #region �ֶ�

        /// <summary>
        /// ���ڴ���  ������� AttendType     Normal ��������  Absenteeism ���� Overtime �Ӱ� Compensation  ���� NightShift ҹ�� LegalDay ������ Evection ����
        /// </summary>
        private string classID;

        /// <summary>
        /// ��Ч��ʱ
        /// </summary>
        private decimal workHours;

        /// <summary>
        /// Ĭ����ʼʱ��
        /// </summary>
        private DateTime beginTime;

        /// <summary>
        /// Ĭ����ֹʱ��
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// ��������Ȩֵ
        /// </summary>
        private decimal minusDays;

        /// <summary>
        /// ��������Ȩֵ
        /// </summary>
        private decimal positiveDays;

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ���ڴ���  ������� AttendType     Normal ��������  Absenteeism ���� Overtime �Ӱ� Compensation  ���� NightShift ҹ�� LegalDay ������ Evection ����
        /// </summary>
        public string ClassID
        {
            get
            {
                return this.classID;
            }
            set
            {
                this.classID = value;
            }
        }

        /// <summary>
        /// ��Ч��ʱ
        /// </summary>
        public decimal WorkHours
        {
            get
            {
                return this.workHours;
            }
            set
            {
                this.workHours = value;
            }
        }

        /// <summary>
        /// Ĭ����ʼʱ��
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
        /// Ĭ����ֹʱ��
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
        /// ��������Ȩֵ
        /// </summary>
        public decimal MinusDays
        {
            get
            {
                return this.minusDays;
            }
            set
            {
                this.minusDays = value;
            }
        }

        /// <summary>
        /// ��������Ȩֵ
        /// </summary>
        public decimal PositiveDays
        {
            get
            {
                return this.positiveDays;
            }
            set
            {
                this.positiveDays = value;
            }
        }

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public OperEnvironment Oper
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new Attendance Clone()
        {
            Attendance attend = base.Clone() as Attendance;
            attend.oper = this.oper.Clone();
            return attend;
        }

        #endregion
    }
}
