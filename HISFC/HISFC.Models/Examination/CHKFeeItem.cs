using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Examination
{
    /// <summary>
    /// [����˵��:����Ա�ս�ʵ����]
    /// [����ʱ��:2008-09]
    /// [������:��]
    ///  <�޸ļ�¼ 
    ///		�޸���='������' 
    ///		�޸�ʱ��='2008-12' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class CHKFeeItem : Neusoft.HISFC.Object.Fee.FeeItemBase
    {

        #region ����
        /// <summary>
        /// �������,1��������컮�ۣ�2��������컮��
        /// </summary>
        private string chargeType = "1";
        
        /// <summary>
        /// ��쵥λ��Ϣ
        /// </summary>
        private Neusoft.NFC.Object.NeuObject compInfo = new Neusoft.NFC.Object.NeuObject ();

        /// <summary>
        /// �Ƿ��ս�
        /// </summary>
        private bool isDayBalanced = false;


        /// <summary>
        /// �Ǽ�����
        /// </summary>
        private DateTime regDate = new DateTime();
        #endregion

        #region ����

        /// <summary>
        /// �Ǽ�����
        /// </summary>
        public DateTime RegDate
        {
            get 
            { 
                return regDate;
            }
            
            set 
            { 
                regDate = value; 
            }
        }

        /// <summary>
        /// �Ƿ��ս�
        /// </summary>
        public bool IsDayBalanced
        {
            get 
            { 
                return isDayBalanced; 
            }
            
            set 
            { 
                isDayBalanced = value; 
            }
        }

        /// <summary>
        /// �������,1��������컮�ۣ�2��������컮�ۣ�Ĭ��1
        /// </summary>
        public string ChargeType
        {
            get 
            { 
                return chargeType; 
            }
            
            set 
            { 
                chargeType = value; 
            }
        }
        #endregion

        #region ��¡����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public CHKFeeItem Clone()
        {
            CHKFeeItem o = base.Clone() as CHKFeeItem;
            o.compInfo = this.compInfo.Clone();
            return o;
        }
        #endregion
    }
}