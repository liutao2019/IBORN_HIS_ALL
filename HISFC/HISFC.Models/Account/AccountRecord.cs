using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Account
{
    
    /// <summary>
    /// FS.HISFC.Models.Account.AccountRecord<br></br>
    /// [��������: �����ʻ�����ʵ��]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2007-05-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountRecord : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountRecord()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        
        /// <summary>
        /// �ʻ�������Ϣ
        /// </summary>
        private HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// �ʺ�
        /// </summary>
        private string accountNO = string.Empty;

        /// <summary>
        /// ��������0Ԥ����1���ʻ�2ͣ�ʻ�3�����ʻ�4֧��5�˷��뻧
        /// </summary>
        private EnumOperTypesService operType = new EnumOperTypesService();

        /// <summary>
        /// �������ͣ�P�����ײͣ�R����Һţ�C�������ѣ�IסԺ���㣻// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private EnumPayTypesService payType = new EnumPayTypesService();

        /// <summary>
        /// ���ѿ���
        /// {68539124-2891-4358-8EF2-D8500CCCD28A}
        /// </summary>
        private FS.FrameWork.Models.NeuObject feeDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ա
        /// {68539124-2891-4358-8EF2-D8500CCCD28A}
        /// </summary>
        private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime operTime = new DateTime();

        /// <summary>
        /// ��ע
        /// </summary>
        private string reMark = string.Empty;

        /// <summary>
        /// ����״̬
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// �����˻���ֵ���ѽ��
        /// </summary>
        private decimal baseCost;
        /// <summary>
        /// ���ͳ�ֵ���ѽ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private decimal donateCost;

        /// <summary>
        /// �����˻����׺����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private decimal baseVacancy;

        /// <summary>
        /// �����˻����׺����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private decimal donateVacancy;

        /// <summary>
        /// ����Ȩ���߻�����Ϣ
        /// </summary>
        private HISFC.Models.RADT.Patient empwoerPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��Ȩ���
        /// </summary>
        private decimal empowerCost = 0m;

        /// <summary>
        /// ��Ʊ����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        FS.FrameWork.Models.NeuObject invoiceType = new FS.FrameWork.Models.NeuObject();



        /// <summary>
        /// �˻�����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private FS.FrameWork.Models.NeuObject accountType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ֵ��Ʊ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private string invoiceNo;

        /// <summary>
        /// ��Ӧ�����ѷ�Ʊ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private string payInvoiceNo;
        /// <summary>
        /// ��Ȩ�˲�����
        /// </summary>
        private string cardNo;
        
        #endregion

        #region ����
        /// <summary>
        /// ��Ȩ�˲�����
        /// </summary>
        public string CardNo
        {
            get
            {
                return this.cardNo;
            }
            set
            {
                this.cardNo = value;
            }
        }
        /// <summary>
        /// ��ֵ��Ʊ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public string InvoiceNo
        {
            get
            {
                return this.invoiceNo;
            }
            set
            {
                this.invoiceNo = value;
            }
        }
        /// <summary>
        /// ��Ӧ�����ѷ�Ʊ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public string PayInvoiceNo
        {
            get
            {
                return this.payInvoiceNo;
            }
            set
            {
                this.payInvoiceNo = value;
            }
        }
        /// <summary>
        /// �˻�����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// �����˻���ֵ���ѽ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// ���ͳ�ֵ���ѽ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// �����˻����׺����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// �����˻����׺����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
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
        /// �ʻ�������Ϣ
        /// </summary>
        public RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
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

        /// <summary>
        /// ��������0Ԥ����1���ʻ�2ͣ�ʻ�3�����ʻ�4֧��5�˷��뻧
        /// </summary>
        public EnumOperTypesService OperType
        {
            get
            {
                return operType;
            }
            set
            {
                operType = value;
            }
        }
        /// <summary>
        /// �������ͣ�P�����ײͣ�R����Һţ�C�������ѣ�IסԺ���㣻// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public EnumPayTypesService PayType
        {
            get
            {
                return payType;
            }
            set
            {
                payType = value;
            }
        }

        /// <summary>
        /// ���ѿ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject FeeDept
        {
            get
            {
                return feeDept;
            }
            set
            {
                feeDept = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.FrameWork.Models.NeuObject Oper
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

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperTime
        {
            get
            {
                return operTime;
            }
            set
            {
                operTime = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string ReMark
        {
            get
            {
                return reMark;
            }
            set
            {
                reMark = value;
            }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        /// <summary>
        /// ����Ȩ���߻�����Ϣ
        /// </summary>
        public HISFC.Models.RADT.Patient EmpowerPatient
        {
            get
            {
                return empwoerPatient;
            }
            set
            {
                empwoerPatient = value;
            }

        }

        /// <summary>
        /// ��Ȩ���
        /// </summary>
        public decimal EmpowerCost
        {
            get
            {
                return empowerCost;
            }
            set
            {
                empowerCost = value;
            }
        }

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject InvoiceType
        {
            get
            {
                return invoiceType;
            }
            set
            {
                invoiceType = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new AccountRecord Clone()
        {
            AccountRecord accountCard = base.Clone() as AccountRecord;
            accountCard.patient = this.Patient.Clone();
            accountCard.empwoerPatient = this.EmpowerPatient.Clone();
            accountCard.operType = this.OperType.Clone() as EnumOperTypesService;
            accountCard.invoiceType = this.InvoiceType.Clone();
            return accountCard;
        }

        #endregion

    }
}
