using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    ///<br> [��������: ��תģ����]</br>
    ///<br>[�� �� ��: ŷ�ܳ�]</br>
    ///<br>[����ʱ��: 2008-07]</br>
    /// </summary>
    [System.Serializable]
    public class TurnMode : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�

        /// <summary>
        /// ��ת��ʽ
        /// </summary>
        string cycleType;

        /// <summary>
        /// ��ת����
        /// </summary>
        string cycleDeptCode;

        /// <summary>
        /// ��תʱ��
        /// </summary>
        int cycleNum;

        /// <summary>
        /// ����Ա
        /// </summary>
        Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

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
        /// ��ת����
        /// </summary>
        public string CycleDeptCode
        {
            get
            {
                return cycleDeptCode;
            }
            set
            {
                cycleDeptCode = value;
            }
        }

        /// <summary>
        /// ��תʱ��
        /// </summary>
        public int CycleNum
        {
            get
            {
                return cycleNum;
            }
            set
            {
                cycleNum = value;
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

        public new TurnMode Clone()
        {
            TurnMode turnMode = base.Clone() as TurnMode;
            turnMode.Oper = this.Oper;
            return turnMode;
        }

        #endregion

    }
}
