using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// [��������: ������]<br></br>
    /// [�� �� ��: ŷ�ܳ�]<br></br>
    /// [����ʱ��: 2008-07]<br></br>
    /// </summary>
    [System.Serializable]
    public class Premium : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// ����·�
        /// </summary>
        string bonusDate;

        /// <summary>
        /// Ա��
        /// </summary>
        Neusoft.HISFC.Models.Base.Employee empl = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ����ϵ��
        /// </summary>
        decimal bonusQuotiety;

        /// <summary>
        /// �������
        /// </summary>
        decimal bonusBase;

        /// <summary>
        /// ������
        /// </summary>
        decimal bonusCost;

        /// <summary>
        /// ����Ա
        /// </summary>
        Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion �ֶ�

        #region ����
        /// <summary>
        /// ����·�
        /// </summary>
        public string BonusDate
        {
            get
            {
                return bonusDate;
            }
            set
            {
                bonusDate = value;
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
        /// ����ϵ��
        /// </summary>
        public decimal BonusQuotiety
        {
            get
            {
                return bonusQuotiety;
            }
            set
            {
                bonusQuotiety = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public decimal BonusBase
        {
            get
            {
                return bonusBase;
            }
            set
            {
                bonusBase = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public decimal BonusCost
        {
            get
            {
                return bonusCost;
            }
            set
            {
                bonusCost = value;
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
        #endregion ����

        #region ��¡
        public new Premium Clone()
        {
            Premium premium = base.Clone() as Premium;
            premium.Oper = this.Oper;
            premium.Empl = this.Empl;
            return premium;
        }
        #endregion ��¡

    }
}
