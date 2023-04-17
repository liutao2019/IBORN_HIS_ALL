using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class PrePay : NeuObject, IValidState, IValid
    {

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        /// <summary>
        /// �ʺ�
        /// </summary>
        private string accountNO = string.Empty;

        /// <summary>
        /// �������
        /// </summary>
        private int happenNo = 0;

        /// <summary>
        /// Ԥ����Ʊ��
        /// </summary>
        private string invoiceNO = string.Empty;

        /// <summary>
        /// ֧������{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
        /// </summary>
        //private Fee.EnumPayTypeService payType = new FS.HISFC.Models.Fee.EnumPayTypeService();
        private FS.FrameWork.Models.NeuObject payType = new NeuObject();

        /// <summary>
        /// �˻�����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private FS.FrameWork.Models.NeuObject accountType = new NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private Bank bank = new Bank();

        /// <summary>
        /// Ԥ���������Ϣ
        /// </summary>
        private OperEnvironment prePayOper = new OperEnvironment();
        /// <summary>
        /// ������Ϣ ����Ԥ������õ�
        /// </summary>
        private FT ft = new FT();
        /// <summary>
        /// ״̬ 0������1��ȡ��2�ش�
        /// </summary>
        private EnumValidState validState = EnumValidState.Valid;

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        private OperEnvironment balanceOper = new OperEnvironment();

        /// <summary>
        /// �Ƿ��ս�
        /// </summary>
        private bool valid = false;

        /// <summary>
        /// ԭƱ�ݺ�
        /// </summary>
        private string oldInvoice = string.Empty;

        /// <summary>
        /// �ش����
        /// </summary>
        private int printTimes = 0;

        /// <summary>
        /// �ս����
        /// </summary>
        private string balanceNo = string.Empty;

        /// <summary>
        /// �Ƿ���ʷ����
        /// </summary>
        private bool isHostory = false;
        /// <summary>
        /// ��ӡ���ݺ�// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private string printNo;
        /// <summary>
        /// �����˻����
        /// </summary>
        private decimal baseCost;
        /// <summary>
        /// ���ͽ��
        /// </summary>
        private decimal donateCost;

        /// <summary>
        /// �����˻����׺����
        /// </summary>
        private decimal baseVacancy;

        /// <summary>
        /// �����˻����׺����
        /// </summary>
        private decimal donateVacancy;


        private string hospital_id;

        private string hospital_name;

        /// <summary>
        /// {37bda347-40b4-40c1-9534-520c0267ef07}
        /// </summary>
        private string memo;

        #endregion

        #region ����

        /// <summary>
        /// �����˻����
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
        /// ���ͽ��
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
        /// �����˻����׺����
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
        /// �����˻����׺����
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
        /// ������Ϣ ����Ԥ������õ�
        /// </summary>
        public FT FT
        {
            get { return ft; }
            set { ft = value; }

        }
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        public HISFC.Models.RADT.Patient Patient
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
            get { return accountNO; }
            set { accountNO = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int HappenNO
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
        ///  Ԥ����Ʊ��
        /// </summary>
        public string InvoiceNO
        {
            get { return invoiceNO; }
            set { invoiceNO = value; }
        }

        /// <summary>
        /// ֧������{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
        /// </summary>
        //public Fee.EnumPayTypeService PayType
        public NeuObject PayType
        {
            get { return payType; }
            set { payType = value; }
        }
        /// <summary>
        /// �˻�����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public NeuObject AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public Bank Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        /// <summary>
        /// Ԥ���������Ϣ
        /// </summary>
        public OperEnvironment PrePayOper
        {
            get { return prePayOper; }
            set { prePayOper = value; }
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        public OperEnvironment BalanceOper
        {
            get { return balanceOper; }
            set { balanceOper = value; }
        }

        /// <summary>
        /// �ս����
        /// </summary>
        public string BalanceNO
        {
            get
            {
                return balanceNo;
            }
            set
            {
                balanceNo = value;
            }
        }

        /// <summary>
        /// ԭƱ�ݺ�
        /// </summary>
        public string OldInvoice
        {
            get
            {
                return oldInvoice;
            }
            set
            {
                oldInvoice = value;
            }
        }

        /// <summary>
        /// �ش����
        /// </summary>
        public int PrintTimes
        {
            get
            {
                return printTimes;
            }
            set
            {
                printTimes = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʷ����(�ڽ����ʻ�ʱ��ǰ�ĵ�Ԥ����Ϊ��ʷ����)
        /// </summary>
        public bool IsHostory
        {
            get
            {
                return isHostory;
            }
            set
            {
                isHostory = value;
            }
        }

        /// <summary>
        ///  ��ӡ���ݺ�// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public string PrintNo
        {
            get { return printNo; }
            set { printNo = value; }
        }

        /// <summary>
        /// Ժ�����{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        public string Hospital_id
        {
            get { return hospital_id; }
            set { hospital_id = value; }
        }

        public string Hospital_name
        {
            get { return hospital_name; }
            set { hospital_name = value; }
        }

        #endregion

        #region IValidState ��Ա
        /// <summary>
        /// Ԥ����״̬ 0������1��ȡ��2�ش�
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

        #region IValid ��Ա
        /// <summary>
        /// �Ƿ��ս�
        /// </summary>
        public bool IsValid
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

        #region Memo

        /// <summary>
        /// ��ע{37bda347-40b4-40c1-9534-520c0267ef07}
        /// </summary>
        public string Memo
        {
            get
            {
                return memo;
            }
            set
            {
                memo = value;

            }
        }
        #endregion


        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new PrePay Clone()
        {
            PrePay prepay = base.Clone() as PrePay;
            prepay.patient = this.Patient.Clone();
            prepay.prePayOper = this.PrePayOper.Clone();
            prepay.balanceOper = this.BalanceOper.Clone();
            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
            //prepay.payType = this.PayType.Clone() as Fee.EnumPayTypeService;
            prepay.FT = this.FT.Clone();
            prepay.payType = this.PayType.Clone();
            prepay.bank = this.Bank.Clone();

            return prepay;
        }
        #endregion
    }
}
