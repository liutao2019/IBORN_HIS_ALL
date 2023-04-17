using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>Neusoft.HISFC.Models.HR.SIBase</br>
    /// <br>[��������: ��ᱣ�ջ���ʵ����]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2008-07-15]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class SIBase : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�

        /// <summary>
        /// ���
        /// </summary>
        private string yearStr = "";

        /// <summary>
        /// ��ͬ����
        /// </summary>
        private string contractType = "";

        /// <summary>
        /// �ɷѲ�������
        /// </summary>
        private string referType = "";

        /// <summary>
        /// ����
        /// </summary>
        private decimal limitUp = 0;

        /// <summary>
        /// ����
        /// </summary>
        private decimal limitDown = 0;

        /// <summary>
        /// ��ƽ������
        /// </summary>
        private decimal avgPay = 0;

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
        public string ContractType
        {
            get
            {
                return contractType;
            }
            set
            {
                contractType = value;
            }
        }

        /// <summary>
        /// �ɷѲ�������
        /// </summary>
        public string ReferType
        {
            get
            {
                return referType;
            }
            set
            {
                referType = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal LimitUp
        {
            get
            {
                return limitUp;
            }
            set
            {
                limitUp = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal LimitDown
        {
            get
            {
                return limitDown;
            }
            set
            {
                limitDown = value;
            }
        }

        /// <summary>
        /// ��ƽ������
        /// </summary>
        public decimal AvgPay
        {
            get
            {
                return avgPay;
            }
            set
            {
                avgPay = value;
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
        public new SIBase Clone()
        {
            SIBase siBase = base.Clone() as SIBase;

            siBase.Oper = this.Oper.Clone();

            return siBase;
        }
        #endregion
    }
}
