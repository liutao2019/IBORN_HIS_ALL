using System;
using System.Collections.Generic;
using System.Text;



namespace Neusoft.HISFC.Models.HR
{
    ///==================================================˵�� begin============================
    /// <summary>
    /// <br></br>
    /// <br>[��������: ���ļ���Ÿ��ֽṹ��]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2008-07-21]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// ==================================================˵�� end============================


    //�ýṹ���ʾÿһ�ֱ��ն�Ӧ�Ľɷѽ��
    [Serializable]
    public struct SIFee
    {
        #region �ֶ�

        /// <summary>
        /// ��������
        /// </summary>
        private string siType;

        /// <summary>
        /// ���˽ɷѽ��
        /// </summary>
        private decimal personalFee;

        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        public string SiType
        {
            get
            {
                return siType;
            }
            set
            {
                siType = value;
            }
        }

        /// <summary>
        /// ���˽ɷѽ��
        /// </summary>
        public decimal PersonalFee
        {
            get
            {
                return personalFee;
            }
            set
            {
                personalFee = value;
            }
        }
        #endregion
    }//end of SIMoney

    //���ڿۿ�����ͷ���
    [Serializable]
    public struct WCDeductFee
    {
        #region �ֶ�

        /// <summary>
        /// ������Ŀ
        /// </summary>
        private string payItem;

        /// <summary>
        /// �ۿ����
        /// </summary>
        private decimal deductScale;

        /// <summary>
        /// �ۿ���
        /// </summary>
        private decimal deductMoney;

        #endregion

        #region ����

        /// <summary>
        /// ������Ŀ
        /// </summary>
        public string PayItem
        {
            get
            {
                return payItem;
            }
            set
            {
                payItem = value;
            }
        }

        /// <summary>
        /// �ۿ����
        /// </summary>
        public decimal DeductScale
        {
            get
            {
                return deductScale;
            }
            set
            {
                deductScale = value;
            }
        }

        /// <summary>
        /// �ۿ���
        /// </summary>
        public decimal DeductMoney
        {
            get
            {
                return deductMoney;
            }
            set
            {
                deductMoney = value;
            }
        }

        #endregion

    }//end of WCDeductFee

}
