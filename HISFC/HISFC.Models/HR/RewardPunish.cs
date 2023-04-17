using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// Neusoft.HISFC.Models.HR.RewardPunish<br></br>
    /// [��������: ����ʵ��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-07-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class RewardPunish : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// �������
        /// </summary>
        private string happenNo = "";

        /// <summary>
        /// Ա��ʵ��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime rewardFunishTime;

        /// <summary>
        /// ���ͼ���
        /// </summary>
        private string rewardFunishLevel = "";

        /// <summary>
        /// ������λ
        /// </summary>
        private string rewardOrganization = "";

        /// <summary>
        /// ���ͷ���
        /// </summary>
        private string rewardFunishCatagory = "";

        /// <summary>
        /// ��������
        /// </summary>
        private string rewardFunishContent = "";

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

         /// <summary>
        /// �������
        /// </summary>
        public string HappenNo
        {
            get
            {
                return happenNo;
            }
            set
            {
                happenNo = value;
            }
        }

        /// <summary>
        /// Ա��ʵ��
        /// </summary>
        public Neusoft.HISFC.Models.Base.Employee Employee
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
        /// ����ʱ��
        /// </summary>
        public DateTime RewardFunishTime
        {
            get
            {
                return rewardFunishTime;
            }
            set
            {
                rewardFunishTime = value;
            }
        }

        /// <summary>
        /// ���ͼ���
        /// </summary>
        public string RewardFunishLevel
        {
            get
            {
                return rewardFunishLevel;
            }
            set
            {
                rewardFunishLevel = value;
            }
        }

        /// <summary>
        /// ������λ
        /// </summary>
        public string RewardOrganization
        {
            get
            {
                return rewardOrganization;
            }
            set
            {
                rewardOrganization = value;
            }
        }

        /// <summary>
        /// ���ͷ���
        /// </summary>
        public string RewardFunishCatagory
        {
            get
            {
                return rewardFunishCatagory;
            }
            set
            {
                rewardFunishCatagory = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string RewardFunishContent
        {
            get
            {
                return rewardFunishContent;
            }
            set
            {
                rewardFunishContent = value;
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
        public new RewardPunish Clone()
        {
            RewardPunish rewardFunish = base.Clone() as RewardPunish;

            rewardFunish.Employee = this.Employee.Clone();
            rewardFunish.Oper = this.Oper.Clone();

            return rewardFunish;
        }
        #endregion
    }
}
