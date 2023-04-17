using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    /// <summary>
    /// FS.HISFC.Models.Account.AccountDetail<br></br>
    /// [��������: �����ʻ���ϸʵ��]<br></br>
    /// [�� �� ��: LFHM]<br></br>
    /// [����ʱ��: 2017-03-14]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountDetail :FS.FrameWork.Models.NeuObject,IValidState
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountDetail()
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
        /// �˻�����
        /// </summary>
        protected FS.FrameWork.Models.NeuObject accountType;
        /// <summary>
        /// �ʻ���ʵ��
        /// </summary>
        private AccountCard accountcard=new AccountCard();
        /// <summary>
        /// �˻�������Ϣ�б�
        /// </summary>
        private List<AccountRecord> accountRecordList = new List<AccountRecord>();
        /// <summary>
        /// �ʻ����׿�ʵ��// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private AccountRecord accountRecord = new AccountRecord();
        
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
        /// �����˻���ֵ���ѽ��
        /// </summary>
        private decimal baseCost;

        /// <summary>
        /// �����˻���ֵ���ѽ��
        /// </summary>
        private decimal donateCost;

        /// <summary>
        /// ��ֵ����
        /// </summary>
        private decimal couponCost;

        /// <summary>
        /// �����˻����
        /// </summary>
        private decimal baseVacancy;

        /// <summary>
        /// �����˻����
        /// </summary>
        private decimal donateVacancy;

        /// <summary>
        /// ʣ�����
        /// </summary>
        private decimal couponVacancy;


        /// <summary>
        /// �����˻��ۼƽ��
        /// </summary>
        private decimal baseAccumulate;

        /// <summary>
        /// �����˻��ۼƽ��
        /// </summary>
        private decimal donateAccumulate;

        /// <summary>
        /// �˻������ۼ�
        /// </summary>
        private decimal couponAccumulate;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment createEnvironment;


        #endregion

        #region ����
        /// <summary>
        /// �˻������ۼƽ��
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
        /// �����˻��ۼƽ��
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
        /// �˻�����
        /// </summary>
        public FS.FrameWork.Models.NeuObject AccountType
        {
            get
            {
                if (accountType == null)
                {
                    accountType = new FS.FrameWork.Models.NeuObject();
                }

                return this.accountType;
            }
            set
            {
                this.accountType = value;
            }
        }
        /// <summary>
        /// �˻������ۼ�
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
        /// ��ֵ����
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
        /// �����˻���ֵ���ѽ��
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
        /// �����˻���ֵ���ѽ��
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
        /// �����˻����
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
        /// �����˻����
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
        /// �����˻����(��ֵ)
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
        /// ��������
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
        /// ������Ϣ
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
        /// �ʻ���ʵ��
        /// </summary>
        public AccountRecord AccountRecord
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
        /// �ʻ�����ʵ��
        /// </summary>
        public List<AccountRecord> AccountRecordList
        {
            get
            {
                return this.accountRecordList;
            }
            set
            {
                this.accountRecordList = value;
            }
        }

         ///<summary>
         ///�ʻ�״̬'1'����'0'ͣ��
         ///</summary>
        public EnumValidState IsValid
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
        #endregion 

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new AccountDetail Clone()
        {
            AccountDetail accountDetail = base.Clone() as AccountDetail;
            accountDetail.AccountCard = this.AccountCard.Clone();
            accountDetail.AccountRecord = this.AccountRecord.Clone();
            if (AccountRecordList.Count > 0)
            {
                foreach (AccountRecord ard in accountRecordList)
                {
                    accountDetail.AccountRecordList.Add(ard.Clone());
                }
            }

            return accountDetail;
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
