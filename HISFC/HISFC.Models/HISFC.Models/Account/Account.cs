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
        /// ���
        /// </summary>
        private decimal avcancy;
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
        #endregion

        #region ����


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
        /// �ʻ����
        /// </summary>
        public decimal Vacancy
        {
            get
            {
                return avcancy;
            }
            set
            {
                avcancy = value;
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
