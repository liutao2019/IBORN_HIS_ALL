using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>[��������: ��ת�ƻ�����]</br>
    /// <br> [�� �� ��: ŷ�ܳ�]</br>
    /// <br>[����ʱ��: 2008-07]</br>
    /// </summary>
    [System.Serializable]
    public class TurnPlan : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// ��ת�ƻ�����
        /// </summary>
        string cyclePlanNo;

        /// <summary>
        /// Ա��
        /// </summary>
        Neusoft.FrameWork.Models.NeuObject employee = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        Neusoft.FrameWork.Models.NeuObject depart = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        DateTime cycleFromDate;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        DateTime cycleToDate;

        /// <summary>
        ///  ��ת��ʽ
        /// </summary>
        string cycleType;

        /// <summary>
        /// ����Ա
        /// </summary>
        Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����
        /// <summary>
        /// ��ת�ƻ�����
        /// </summary>
        public string CyclePlanNo
        {
            get
            {
                return cyclePlanNo;
            }
            set
            {
                cyclePlanNo = value;
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
        /// ��ʼʱ��
        /// </summary>
        public DateTime CycleFromDate
        {
            get
            {
                return cycleFromDate;
            }
            set
            {
                cycleFromDate = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CycleToDate
        {
            get
            {
                return cycleToDate;
            }
            set
            {
                cycleToDate = value;
            }
        }

        /// <summary>
        /// ��ת��ʽ
        /// </summary>
        public string CycleType
        {
            get
            {
                return cycleType;
            }
            set
            {
                cycleType = value;
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
        public new TurnPlan Clone()
        {
            TurnPlan turnPlan = base.Clone() as TurnPlan;
            turnPlan.Employee = this.Employee;
            turnPlan.Depart = this.Depart;
            turnPlan.Oper = this.Oper;
            return turnPlan;
        }
        #endregion

    }
}
