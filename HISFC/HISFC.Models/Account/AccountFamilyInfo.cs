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
    public class AccountFamilyInfo :FS.FrameWork.Models.NeuObject,IValidState
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountFamilyInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        /// <summary>
        /// �������˲�����
        /// </summary>
        private string cardNO = string.Empty;
        /// <summary>
        /// �����˲�����
        /// </summary>
        private string linkedCardNO = string.Empty;

        /// <summary>
        /// ���������˻�
        /// </summary>
        private string accountNo = string.Empty;

        /// <summary>
        /// �������˻�
        /// </summary>
        private string linkedAccountNo = string.Empty;
        /// <summary>
        /// ��ϵ
        /// </summary>
        protected FS.FrameWork.Models.NeuObject relation;
        /// <summary>
        /// �Ա�
        /// </summary>
        protected FS.FrameWork.Models.NeuObject sex;

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime birthday = new DateTime();

        /// <summary>
        /// ֤������
        /// </summary>
        protected FS.FrameWork.Models.NeuObject cardType;

        /// <summary>
        /// ֤������
        /// </summary>
        private string idCardNo = string.Empty;

        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        private string phone = string.Empty;

        /// <summary>
        /// ��ϵ�˵�ַ
        /// </summary>
        private string address = string.Empty; 

        /// <summary>
        /// �ʻ���ʵ��
        /// </summary>
        private AccountCard accountcard = new AccountCard();

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private EnumValidState validState= EnumValidState.Valid;
        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment createEnvironment;


        /// <summary>
        /// ��ͥ�� 
        /// </summary>
        private string familyCode = string.Empty;
        /// <summary>
        /// ��ͥ���� 
        /// </summary>
        private string familyName = string.Empty;
        #endregion

        #region ����

        /// <summary>
        /// ��ͥ����
        /// </summary>
        public string FamilyName
        {
            get
            {
                return this.familyName;
            }
            set
            {
                this.familyName = value;
            }
        }
        /// <summary>
        /// ��ͥ��
        /// </summary>
        public string FamilyCode
        {
            get
            {
                return this.familyCode;
            }
            set
            {
                this.familyCode = value;
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
        /// �������˲�����
        /// </summary>
        public string CardNO
        {
            get { return cardNO; }
            set { cardNO = value; }
        }

        /// <summary>
        /// ���������˺�
        /// </summary>
        public string AccountNo
        {
            get { return this.accountNo; }
            set { accountNo = value; }
        }
        /// <summary>
        /// �������˺�
        /// </summary>
        public string LinkedAccountNo
        {
            get { return this.linkedAccountNo; }
            set { linkedAccountNo = value; }
        }
        /// <summary>
        /// �����˲�����
        /// </summary>
        public string LinkedCardNO
        {
            get { return this.linkedCardNO; }
            set { linkedCardNO = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime Birthday
        {
            get { return this.birthday; }
            set { birthday = value; }
        }
        /// <summary>
        /// �Ա�
        /// </summary>
        public FS.FrameWork.Models.NeuObject Sex
        {
            get
            {
                if (this.sex == null)
                {
                    sex = new FS.FrameWork.Models.NeuObject();
                }

                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }

        /// <summary>
        /// ��ϵ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Relation
        {
            get
            {
                if (this.relation == null)
                {
                    relation = new FS.FrameWork.Models.NeuObject();
                }

                return this.relation;
            }
            set
            {
                this.relation = value;
            }
        }
        /// <summary>
        /// ֤������
        /// </summary>
        public FS.FrameWork.Models.NeuObject CardType
        {
            get
            {
                if (this.cardType == null)
                {
                    cardType = new FS.FrameWork.Models.NeuObject();
                }

                return this.cardType;
            }
            set
            {
                this.cardType = value;
            }
        }
        /// <summary>
        /// ֤������
        /// </summary>
        public string IDCardNo
        {
            get { return this.idCardNo; }
            set { idCardNo = value; }
        }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string Phone
        {
            get { return this.phone; }
            set { phone = value; }
        }

        /// <summary>
        /// ��סַ
        /// </summary>
        public string Address
        {
            get { return this.address; }
            set { address = value; }
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


        #endregion 

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new AccountFamilyInfo Clone()
        {
            AccountFamilyInfo accountFamilyInfo = base.Clone() as AccountFamilyInfo;
            accountFamilyInfo.AccountCard = this.AccountCard.Clone();
            return accountFamilyInfo;
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
