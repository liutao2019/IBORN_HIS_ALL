using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    /// <summary>
    /// FS.HISFC.Models.Account.Account<br></br>
    /// [��������: �����ʻ�ʵ��]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2007-05-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class Account :FS.FrameWork.Models.NeuObject,IValidState
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public Account()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        /// <summary>
        /// ���￨��
        /// </summary>
        private string cardNO = string.Empty;

        /// <summary>
        /// �ʻ���ʵ��
        /// </summary>
        private AccountCard accountcard=new AccountCard();
        /// <summary>
        /// �ʻ�����ʵ��
        /// </summary>
        private List<AccountRecord> accountRecord = new List<AccountRecord>();
        
        /// <summary>
        /// �ʻ�״̬
        /// </summary>
        private EnumValidState validState= EnumValidState.Valid;
        /// <summary>
        /// �ʻ�����
        /// </summary>
        private string password = string.Empty;
        /// <summary>
        /// ������������
        /// </summary>
        private decimal daylimit;
        /// <summary>
        /// �Ƿ���Ȩ
        /// </summary>
        private bool isEmpower=false;
        /// <summary>
        /// �����˻���ֵ���ѽ��
        /// </summary>
        private decimal baseCost;
        /// <summary>
        /// �����˻���ֵ���ѽ��
        /// </summary>
        private decimal donateCost;

        /// <summary>
        /// �����˻����
        /// </summary>
        private decimal baseVacancy;

        /// <summary>
        /// ����
        /// </summary>
        private decimal couponCost;

        /// <summary>
        /// �����˻����
        /// </summary>
        private decimal donateVacancy;

        /// <summary>
        /// �����˻����
        /// </summary>
        private decimal couponVacancy;

        /// <summary>
        /// ����ֵ(���ã������˻����ܵ��ڸ�ֵ)
        /// </summary>
        private decimal limit;

        /// <summary>
        /// �����˻��ۼƽ��
        /// </summary>
        private decimal baseAccumulate;

        /// <summary>
        /// �����˻��ۼƽ��
        /// </summary>
        private decimal donateAccumulate;

        /// <summary>
        /// �˻������ۼ�// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private decimal couponAccumulate;
        /// <summary>
        /// ��Ա�ȼ�:1 ��ͨ��Ա����2 ��Ա����3 �ƽ��Ա����4�׽��Ա����5��ʯ��Ա����6�����Ա��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        protected FS.FrameWork.Models.NeuObject accountLevel;

        /// <summary>
        /// ������Ϣ// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// ������Ϣ// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>				
        private OperEnvironment createEnvironment;


        #endregion

        #region ���� 
        /// <summary>
        /// ��Ա�ȼ�:1 ��ͨ��Ա����2 �ƽ��Ա����3�׽��Ա����4��ʯ��Ա����5�����Ա��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public FS.FrameWork.Models.NeuObject AccountLevel
        {
            get
            {
                if (accountLevel == null)
                {
                    accountLevel = new FS.FrameWork.Models.NeuObject();
                }
				
                return this.accountLevel;
            }
            set
            {
                this.accountLevel = value;
            }
        }
        /// <summary>
        /// �˻������ۼƽ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal DonateAccumulate
        {
            get
            {
                return this.donateAccumulate;
            }
            set
            {
                this.donateAccumulate = value;
            }
        }
        /// <summary>
        /// �����˻��ۼƽ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal BaseAccumulate
        {
            get
            {
                return this.baseAccumulate;
            }
            set
            {
                this.baseAccumulate = value;
            }
        }


        /// <summary>
        /// �˻������ۼ�// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal CouponAccumulate
        {
            get
            {
                return this.couponAccumulate;
            }
            set
            {
                this.couponAccumulate = value;
            }
        }
        /// <summary>
        /// ����ֵ(���ã������˻����ܵ��ڸ�ֵ)// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal Limit
        {
            get
            {
                return this.limit;
            }
            set
            {
                this.limit = value;
            }
        }
        /// <summary>
        /// �����˻����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal CouponVacancy
        {
            get
            {
                return this.couponVacancy;
            }
            set
            {
                this.couponVacancy = value;
            }
        }
        /// <summary>
        /// �����˻����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal DonateVacancy
        {
            get
            {
                return this.donateVacancy;
            }
            set
            {
                this.donateVacancy = value;
            }
        }
        /// <summary>
        /// �����˻����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal BaseVacancy
        {
            get
            {
                return this.baseVacancy;
            }
            set
            {
                this.baseVacancy = value;
            }
        }
        /// <summary>
        /// �����˻���ֵ����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal BaseCost
        {
            get
            {
                return this.baseCost;
            }
            set
            {
                this.baseCost = value;
            }
        }

        /// <summary>
        /// �����˻���ֵ����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal DonateCost
        {
            get
            {
                return this.donateCost;
            }
            set
            {
                this.donateCost = value;
            }
        }
        /// <summary>
        /// ����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public decimal CouponCost
        {
            get
            {
                return this.couponCost;
            }
            set
            {
                this.couponCost = value;
            }
        }
        /// <summary>
        /// ��������// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public OperEnvironment OperEnvironment
        {
            get
            {
                if (operEnvironment == null)
                {
                    operEnvironment = new OperEnvironment();
                }
                return this.operEnvironment;
            }
            set
            {
                this.operEnvironment = value;
            }
        }


        /// <summary>
        /// ������Ϣ// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public OperEnvironment CreateEnvironment
        {
            get
            {
                if (createEnvironment == null)
                {
                    createEnvironment = new OperEnvironment();
                }
                return this.createEnvironment;
            }
            set
            {
                this.createEnvironment = value;
            }
        }
        /// <summary>
        /// ���￨��
        /// </summary>
        public string CardNO
        {
            get { return cardNO; }
            set { cardNO = value; }
        }


        /// <summary>
        /// �ʻ���ʵ��
        /// </summary>
        public AccountCard AccountCard
        {
            get
            {
                return this.accountcard;
            }
            set
            {
                this.accountcard = value;
            }
        }
        /// <summary>
        /// �ʻ�����ʵ��
        /// </summary>
        public List<AccountRecord> AccountRecord
        {
            get
            {
                return this.accountRecord;
            }
            set
            {
                this.accountRecord = value;
            }
        }

        /// <summary>
        /// �ʻ�״̬'1'����'0'ͣ��
        /// </summary>
        //public EnumValidState IsValid
        //{
        //    get
        //    {
        //        return validState;
        //    }
        //    set
        //    {
        //        validState = value;
        //    }
        //}


        /// <summary>
        /// �ʻ�����
        /// </summary>
        public string PassWord
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public decimal DayLimit
        {
            get
            {
                return this.daylimit;
            }
            set
            {
                this.daylimit = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ȩ
        /// </summary>
        public bool IsEmpower
        {
            get
            {
                return isEmpower;
            }
            set
            {
                isEmpower = value;
            }
        }
        #endregion 

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Account Clone()
        {
            Account account = base.Clone() as Account;
            account.AccountCard = this.AccountCard.Clone();
            if (AccountRecord.Count > 0)
            {
                foreach (AccountRecord ard in accountRecord)
                {
                    account.AccountRecord.Add(ard.Clone());
                }
            }

            return account;
        }
        #endregion

        #region IValidState ��Ա
        /// <summary>
        /// �ʻ�״̬0ͣ�� 1���� 2ע��
        /// </summary>
        public EnumValidState ValidState
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
        }

        #endregion
    }

 
}
