using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>Neusoft.HISFC.Models.HR.SIRefer</br>
    /// <br>[��������: ��ᱣ�ն���ʵ����]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2008-07-17]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class SIRefer : Neusoft.FrameWork.Models.NeuObject
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
        /// ��������(ʧҵ�����ˡ����ϡ�ҽ�Ƶ�)
        /// </summary>
        private string insuranceType = "";

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
        public new SIRefer Clone()
        {
            SIRefer siRefer = base.Clone() as SIRefer;

            siRefer.Oper = this.Oper.Clone();

            return siRefer;
        }
        #endregion
    }
}
