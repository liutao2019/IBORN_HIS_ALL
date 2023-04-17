using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{

    /// <summary>
    /// [��������: ��Ʒ������ϸ��]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    /// 
    public class SanReturnList:Neusoft.NFC.Object.NeuObject
    {
        public SanReturnList()
        {

        }

        #region ����
        /// <summary>
        /// ������ϸ��ˮ��
        /// </summary>
        private string listCode = string.Empty;

        /// <summary>
        /// ����������ˮ��
        /// </summary>
        private string returnCode = string.Empty;

        /// <summary>
        /// �����ϸ��ˮ��
        /// </summary>
        private string stockNo = string.Empty;

        /// <summary>
        /// ����״̬1����2����ȷ��3���ȷ��4���ȷ��5��ȡȷ��
        /// </summary>
        private QCReturnState returnState = QCReturnState.APPLY;

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        private HISFC.Object.Material.StoreBase storeBase = new Neusoft.HISFC.Object.Material.StoreBase();

        #endregion

        #region ʵ��

        /// <summary>
        /// ������ϸ��ˮ��
        /// </summary>
        public string ListCode
        {
            get
            {
                return listCode;
            }
            set
            {
                listCode = value;
            }
        }

        /// <summary>
        /// ����������ˮ��
        /// </summary>
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        /// <summary>
        /// �����ϸ��ˮ��
        /// </summary>
        public string StockNo
        {
            get
            {
                return stockNo;
            }
            set
            {
                stockNo = value;
            }
        }

        /// <summary>
        /// ����״̬1����2����ȷ��3���ȷ��4���ȷ��5��ȡȷ��
        /// </summary>
        public QCReturnState ReturnState
        {
            get
            {
                return returnState;
            }
            set
            {
                returnState = value;
            }
        }

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        public HISFC.Object.Material.StoreBase StoreBase
        {
            get
            {
                return storeBase;
            }
            set
            {
                storeBase = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new SanReturnList Clone()
        {
            SanReturnList sanReturnList = base.Clone() as SanReturnList;

            sanReturnList.StoreBase = this.StoreBase.Clone();

            return sanReturnList;

        } 
        #endregion
    }
    
}
