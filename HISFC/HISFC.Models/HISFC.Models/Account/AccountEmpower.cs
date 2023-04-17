using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Account
{

    /// <summary>
    /// FS.HISFC.Models.Account.AccountRecord<br></br>
    /// [��������: �����ʻ���Ȩʵ��]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2008-06-23]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountEmpower : NeuObject
    {
        #region ����
        /// <summary>
        /// ��Ȩ�ʻ���Ϣ
        /// </summary>
        private AccountCard accountCard = new AccountCard();

        /// <summary>
        /// ����Ȩ�ʻ���Ϣ
        /// </summary>
        private AccountCard empowerCard = new AccountCard();

        /// <summary>
        /// ��Ȩ����
        /// </summary>
        private string passWord = string.Empty;

        /// <summary>
        /// ��Ȩ�޶�
        /// </summary>
        private decimal empowerLimit = 0.0m;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Base.OperEnvironment oper = new Base.OperEnvironment();

        /// <summary>
        /// ���
        /// </summary>
        private decimal vacancy = 0.0m;

        /// <summary>
        /// �Ƿ����0ͣ�� 1����
        /// </summary>
        private Base.EnumValidState valid =  Base.EnumValidState.Valid;
        /// <summary>
        /// �ʺ�
        /// </summary>
        private string accountNO = string.Empty;
        #endregion

        #region ���ԡ�
        /// <summary>
        /// ��Ȩ�ʻ���Ϣ
        /// </summary>
        public AccountCard AccountCard
        {
            get { return accountCard; }
            set { accountCard = value; }
        }

        /// <summary>
        /// ����Ȩ�ʻ���Ϣ
        /// </summary>
        public AccountCard EmpowerCard
        {
            get { return empowerCard; }
            set { empowerCard = value; }
        }

        /// <summary>
        /// ��Ȩ����
        /// </summary>
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        /// <summary>
        /// ��Ȩ�޶�
        /// </summary>
        public decimal EmpowerLimit
        {
            get { return empowerLimit; }
            set { empowerLimit = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public decimal Vacancy
        {
            get { return vacancy; }
            set { vacancy = value; }
        }
        /// <summary>
        /// �ʺ�
        /// </summary>
        public string AccountNO
        {
            get
            {
                return accountNO;
            }
            set
            {
                accountNO = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new AccountEmpower Clone()
        {
            AccountEmpower accountEmpower = base.Clone() as AccountEmpower;
            accountEmpower.accountCard = this.AccountCard.Clone();
            accountEmpower.empowerCard = this.EmpowerCard.Clone();
            accountEmpower.oper = Oper.Clone();
            return accountEmpower;
        }
        #endregion

        #region IValid ��Ա
        /// <summary>
        /// �Ƿ����� 0ͣ�� 1����
        /// </summary>
        public Base.EnumValidState ValidState
        {
            get
            {
                return valid;
            }
            set
            {
                valid = value;
            }
        }

        #endregion
    }
}
