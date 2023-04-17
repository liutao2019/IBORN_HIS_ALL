using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// Neusoft.HISFC.Models.HR.SocietyInsurance<br></br>
    /// [��������: ��ᱣ��ʵ��]<br></br>
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
    public class SI : Neusoft.FrameWork.Models.NeuObject //, HISFC.Object.Base.IValidState HISFC .Object .Base .IValid 
    {
        #region �ֶ�

        /// <summary>
        /// ���
        /// </summary>
        private string yearStr = "";

        /// <summary>
        /// ��ͬ����
        /// </summary>
        //private string contractType = "";

        /// <summary>
        /// ��������(ʧҵ�����ˡ����ϡ�ҽ�Ƶ�)
        /// </summary>
        private string insuranceType = "";

        /// <summary>
        /// ���ո��˽ɷѱ���
        /// </summary>
        private decimal personalScale = 0;

        /// <summary>
        /// ���ո��˽ɷѽ��
        /// </summary>
        private decimal personalAmount = 0;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ���
        /// </summary>
        public string YearStr
        {
            get
            {
                return yearStr;
            }
            set
            {
                yearStr = value;
            }
        }

        /// <summary>
        /// ��ͬ����
        /// </summary>
        //public string ContractType
        //{
        //    get
        //    {
        //        return contractType;
        //    }
        //    set
        //    {
        //        contractType = value;
        //    }
        //}

        /// <summary>
        /// ��������(ʧҵ�����ˡ����ϡ�ҽ�Ƶ�)
        /// </summary>
        public string InsuranceType
        {
            get
            {
                return insuranceType;
            }
            set
            {
                insuranceType = value;
            }
        }

        /// <summary>
        /// ���ո��˽ɷѱ���
        /// </summary>
        public decimal PersonalScale
        {
            get
            {
                return personalScale;
            }
            set
            {
                personalScale = value;
            }
        }

        /// <summary>
        /// ���ո��˽ɷѽ��
        /// </summary>
        public decimal PersonalAmount
        {
            get
            {
                return personalAmount;
            }
            set
            {
                personalAmount = value;
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
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new SI Clone()
        {
            SI si = base.Clone() as SI;

            si.Oper = this.Oper.Clone();

            return si;
        }
        #endregion
    }
}
