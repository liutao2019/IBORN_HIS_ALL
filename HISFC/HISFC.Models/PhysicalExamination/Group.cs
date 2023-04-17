using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.PhysicalExamination
{
    /// <summary>
    /// Group<br></br>
    /// [��������: ������׹�����]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-03-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class Group : FS.HISFC.Models.Fee.ComGroup
    {
        #region ˽�б���
        /// <summary>
        /// �Էѱ���
        /// </summary>
        private decimal ownRate;
        /// <summary>
        /// �Ը�����
        /// </summary>
        private decimal payRate;
        /// <summary>
        /// ���ѱ���
        /// </summary>
        private decimal pubRate;
        /// <summary>
        /// �Żݱ���
        /// </summary>
        private decimal ecoRate;
        /// <summary>
        /// �Ƿ���
        /// </summary>
        private string isShare;
        /// <summary>
        /// �Էѽ��
        /// </summary>
        private decimal ownCost;
        /// <summary>
        /// �Ը����
        /// </summary>
        private decimal payCost;
        /// <summary>
        /// ���ѽ��
        /// </summary>
        private decimal pubCost;
        /// <summary>
        /// �Żݽ��
        /// </summary>
        private decimal ecoCost;
        #endregion

        #region ����
        /// <summary>
        /// �Ը����
        /// </summary>
        public decimal OwnCost
        {
            get
            {
                return ownCost;
            }
            set
            {
                ownCost = value;
            }
        }
        /// <summary>
        /// �Ը����
        /// </summary>
        public decimal PayCost
        {
            get
            {
                return payCost;
            }
            set
            {
                payCost = value;
            }
        }
        /// <summary>
        /// ���ѽ��
        /// </summary>
        public decimal PubCost
        {
            get
            {
                return pubCost;
            }
            set
            {
                pubCost = value;
            }
        }
        /// <summary>
        ///�Żݽ�� 
        /// </summary>
        public decimal EcoCost
        {
            get
            {
                return ecoCost;
            }
            set
            {
                ecoCost = value;
            }
        }
        /// <summary>
        /// �Ը�����
        /// </summary>
        public decimal OwnRate
        {
            get
            {
                return ownRate;
            }
            set
            {
                ownRate = value;
            }
        }
        /// <summary>
        /// �Ը�����
        /// </summary>
        public decimal PayRate
        {
            get
            {
                return payRate;
            }
            set
            {
                payRate = value;
            }
        }
        /// <summary>
        /// ���ѱ���
        /// </summary>
        public decimal PubRate
        {
            get
            {
                return pubRate;
            }
            set
            {
                pubRate = value;
            }
        }
        /// <summary>
        ///�Żݱ��� 
        /// </summary>
        public decimal EcoRate
        {
            get
            {
                return ecoRate;
            }
            set
            {
                ecoRate = value;
            }
        }
        /// <summary>
        /// �Ƿ��� 
        /// </summary>
        public string IsShare
        {
            get
            {
                return isShare;
            }
            set
            {
                isShare = value;
            }
        }
        #endregion

        #region ��¡����
        public new Group Clone()
        {
            Group obj = base.Clone() as Group;
            return obj;
        }
        #endregion 
    }
}
