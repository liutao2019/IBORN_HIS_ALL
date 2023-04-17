using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Account
{
    /// <summary>
    /// FS.HISFC.Models.Account.AccountCard<br></br>
    /// [��������: �����ʻ���ʵ��]<br></br>
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
    public class AccountCard :FS.FrameWork.Models.NeuObject, Base.IValid
    {
       
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountCard()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        private string markNO = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject markType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid =true;
        /// <summary>
        /// �������'1'����,'0'�·���
        /// </summary>
        private string reFlag = string.Empty;
        /// <summary>
        /// ��α��
        /// </summary>
        private string securityCode = string.Empty;

        /// <summary>
        /// �ʻ�������ʵ��
        /// </summary>
        private List< AccountCardRecord> accountCardRecord = new List<AccountCardRecord>();
        /// <summary>
        /// ��״̬
        /// </summary>
        private MarkOperateTypes markStatus = MarkOperateTypes.Begin;
        /// <summary>
        /// ������
        /// </summary>
        private OperEnvironment createOper = new OperEnvironment();
        /// <summary>
        /// ͣ����
        /// </summary>
        private OperEnvironment stopOper = new OperEnvironment();
        /// <summary>
        /// �˿���
        /// </summary>
        private OperEnvironment backOper = new OperEnvironment();
        /// <summary>
        /// ��Ա�ȼ�:1 ��ͨ��Ա����2 ��Ա����3 �ƽ��Ա����4�׽��Ա����5��ʯ��Ա����6�����Ա��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        protected FS.FrameWork.Models.NeuObject accountLevel;

        /// <summary>
        /// ���Ŀ�ʼʱ��
        /// </summary>
        private DateTime begTime = new DateTime();
        /// <summary>
        /// ���Ľ�ֹʱ��
        /// </summary>
        private DateTime endTime = new DateTime();

        #endregion 

        #region ����
        /// <summary>
        /// ���Ŀ�ʼʱ��
        /// </summary>
        public DateTime BegTime
        {
            get { return this.begTime; }
            set { this.begTime = value; }
        }

        /// <summary>
        /// ���Ľ�ֹʱ��
        /// </summary>
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }

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
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                patient = value;
            }
            get
            {
                return patient;
            }
        }

        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        public string MarkNO
        {
            get
            {
                return this.markNO;
            }
            set
            {
                this.markNO = value;
            }
        }
        /// <summary>
        /// ��ݱ�ʶ����� 1�ſ� 2IC�� 3���Ͽ�
        /// </summary>
        public FS.FrameWork.Models.NeuObject MarkType
        {
            get
            {
                return this.markType;
            }
            set
            {
                this.markType = value;
            }
        }

        /// <summary>
        /// �����ʻ�������ʵ��
        /// </summary>
        public List<AccountCardRecord> AccountCardRecord
        {
            get
            {
                return this.accountCardRecord;
            }
            set
            {
                this.accountCardRecord = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public OperEnvironment CreateOper
        {
            get { return createOper; }
            set { createOper = value;  }
        }
        /// <summary>
        /// ͣ����
        /// </summary>
        public OperEnvironment StopOper
        {
            get { return stopOper; }
            set
            {
                stopOper = value;
            }
        }
        /// <summary>
        /// �˿���
        /// </summary>
        public OperEnvironment BackOper
        {
            get { return backOper; }
            set { backOper = value; }
        }
        /// <summary>
        /// �������'1'����,'0'�·���
        /// </summary>
        public string ReFlag
        {
            get { return reFlag; }
            set { reFlag = value; }
        }
        /// <summary>
        /// ��α��
        /// </summary>
        public string SecurityCode
        {
            get { return securityCode; }
            set { securityCode = value; }
        }
        #endregion 

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new AccountCard Clone()
        {
            AccountCard accountCard = base.Clone() as AccountCard;
            accountCard.MarkType = this.MarkType.Clone() as FS.FrameWork.Models.NeuObject;
            accountCard.CreateOper = this.CreateOper.Clone();
            accountCard.StopOper = this.StopOper.Clone();
            accountCard.BackOper = this.BackOper.Clone();

            foreach (AccountCardRecord cardRecord in this.AccountCardRecord)
            {
                accountCard.AccountCardRecord.Add(cardRecord);
            }
            return accountCard;
        }
        #endregion

        #region IValid ��Ա
        /// <summary>
        /// �Ƿ���Ч true��Ч false����
        /// </summary>
        [Obsolete("���ϣ�������ʹ�ã���ʹ�� MarkStatus ", false)]
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        #region ��״̬
        /// <summary>
        /// ��״̬
        /// </summary>
        public MarkOperateTypes MarkStatus
        {
            get
            {
                return this.markStatus;
            }
            set
            {
                this.markStatus = value;
            }
        }

        #endregion



    }

    /// <summary>
    /// FS.HISFC.Models.Account.AccountCardFee
    /// [��������: ���￨����ʵ��]
    /// </summary>
    [Serializable]
    public class AccountCardFee : FS.FrameWork.Models.NeuObject
    {
        #region ����
        /// <summary>
        /// ��Ʊ�ţ���ˮ�ţ�
        /// </summary>
        string invoiceNo = string.Empty;
        /// <summary>
        /// ��ӡ��Ʊ��
        /// </summary>
        string print_invoiceNo = string.Empty;
        /// <summary>
        /// �������� TransTypes.Positive ������(1), TransTypes.Negative ������(2)
        /// </summary>
        private TransTypes transType;
        /// <summary>
        /// �������
        /// </summary>
        AccCardFeeType feeType; 
        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        private string markNO = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject markType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// �����ܶ�
        /// </summary>
        private decimal tot_cost = 0;
        /// <summary>
        /// �շ���
        /// </summary>
        private OperEnvironment feeOper = new OperEnvironment();
        /// <summary>
        /// ������
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();
        /// <summary>
        /// �Ƿ��ս�
        /// </summary>
        bool isBalance = false;
        /// <summary>
        /// �ս��ʶ��
        /// </summary>
        string balanceNo = string.Empty;
        /// <summary>
        /// �ս��������(�ս���,�ս�ʱ��)
        /// </summary>
        private OperEnvironment balanceOper = new OperEnvironment();
        /// <summary>
        /// ״̬�� 1 = ��Ч 0 = ��Ч 2 = �˷�
        /// </summary>
        int iStatus = 1;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// �����
        /// </summary>
        private string clinicNo = string.Empty;
        /// <summary>
        /// ��ע
        /// </summary>
        private string remark = string.Empty;
        /// <summary>
        /// �Էѽ��
        /// </summary>
        private decimal own_cost = 0;
        /// <summary>
        /// �Ը����
        /// </summary>
        private decimal pay_cost = 0;
        /// <summary>
        /// �������
        /// </summary>
        private decimal pub_cost = 0;

        private NeuObject payType = new NeuObject();


        // {C0554E8C-39D6-48cb-983A-EDC6C1D63843}

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string itemCode = string.Empty;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string itemName = string.Empty;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private int itemQty = 0;

        /// <summary>
        /// ��Ŀ��λ
        /// </summary>
        private string itemUnit = string.Empty;

        /// <summary>
        /// ��Ŀ�۸�
        /// </summary>
        private decimal itemPrice = 0;

        /// <summary>
        /// �籣��־��0δ�ϴ���1���ϴ�
        /// </summary>
        private string siFlag = string.Empty;

        /// <summary>
        /// �籣��������
        /// </summary>
        private DateTime siBalanceDate;

        /// <summary>
        /// �籣���㵥��
        /// </summary>
        private string siBalanceNO = string.Empty;

        /// <summary>
        /// �Żݽ��
        /// </summary>
        private decimal eco_cost = 0;


        #endregion

        #region ����
        /// <summary>
        /// ��Ʊ�ţ���ˮ�ţ�
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        /// <summary>
        /// ��ӡ��Ʊ��
        /// </summary>
        public string Print_InvoiceNo
        {
            get { return print_invoiceNo; }
            set { print_invoiceNo = value; }
        }
        /// <summary>
        /// �������� TransTypes.Positive ������(1), TransTypes.Negative ������(2)
        /// </summary>
        public TransTypes TransType
        {
            get { return transType; }
            set { transType = value; }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public AccCardFeeType FeeType
        {
            get { return this.feeType; }
            set { this.feeType = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string CardNo
        {
            get { return this.patient.PID.CardNO; }
            set { this.patient.PID.CardNO = value; }
        }

        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        public string MarkNO
        {
            get { return markNO; }
            set { markNO = value; }
        }

        public FS.FrameWork.Models.NeuObject MarkType
        {
            get { return markType; }
            set { markType = value; }
        }
        /// <summary>
        /// �����ܶ�
        /// </summary>
        public decimal Tot_cost
        {
            get { return tot_cost; }
            set { tot_cost = value; }
        }
        /// <summary>
        /// �շ���
        /// </summary>
        public OperEnvironment FeeOper
        {
            get { return feeOper; }
            set { feeOper = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }
        /// <summary>
        /// �Ƿ��ս�
        /// </summary>
        public bool IsBalance
        {
            get { return isBalance; }
            set { isBalance = value; }
        }

        /// <summary>
        /// �ս��������(�ս���,�ս�ʱ��)
        /// </summary>
        public OperEnvironment BalnaceOper
        {
            get { return balanceOper; }
            set { balanceOper = value; }
        }
        /// <summary>
        /// �ս��ʶ��
        /// </summary>
        public string BalanceNo
        {
            get { return balanceNo; }
            set { balanceNo = value; }
        }
        /// <summary>
        /// ״̬�� 1 = ��Ч 0 = ��Ч 2 = �˷�
        /// </summary>
        public int IStatus
        {
            get { return iStatus; }
            set { iStatus = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                patient = value;
            }
            get
            {
                return patient;
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string ClinicNO
        {
            get { return clinicNo; }
            set { clinicNo = value; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        /// <summary>
        /// �Էѽ��
        /// </summary>
        public decimal Own_cost
        {
            get { return this.own_cost; }
            set { own_cost = value; }
        }
        /// <summary>
        /// �Ը����
        /// </summary>
        public decimal Pay_cost
        {
            get { return this.pay_cost; }
            set { pay_cost = value; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public decimal Pub_cost
        {
            get { return this.pub_cost; }
            set { pub_cost = value; }
        }

        /// <summary>
        /// ֧����ʽ
        /// </summary>
        public NeuObject PayType
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
        // {C0554E8C-39D6-48cb-983A-EDC6C1D63843}

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public int ItemQty
        {
            get { return itemQty; }
            set { itemQty = value; }
        }

        /// <summary>
        /// ��Ŀ��λ
        /// </summary>
        public string ItemUnit
        {
            get { return itemUnit; }
            set { itemUnit = value; }
        }

        /// <summary>
        /// ��Ŀ�۸�
        /// </summary>
        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        /// <summary>
        /// �籣��־��0δ�ϴ���1���ϴ�
        /// </summary>
        public string SiFlag
        {
            get { return siFlag; }
            set { siFlag = value; }
        }

        /// <summary>
        /// �籣��������
        /// </summary>
        public DateTime SiBalanceDate
        {
            get { return siBalanceDate; }
            set { siBalanceDate = value; }
        }

        /// <summary>
        /// �籣���㵥��
        /// </summary>
        public string SiBalanceNO
        {
            get { return siBalanceNO; }
            set { siBalanceNO = value; }
        }

        /// <summary>
        /// �Żݽ��
        /// </summary>
        public decimal Eco_cost
        {
            get { return eco_cost; }
            set { eco_cost = value; }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new AccountCardFee Clone()
        {
            AccountCardFee accountCardFee = base.Clone() as AccountCardFee;
            accountCardFee.MarkType = this.MarkType.Clone();
            accountCardFee.FeeOper = this.FeeOper.Clone();
            accountCardFee.Oper = this.Oper.Clone();
            accountCardFee.BalnaceOper = this.BalnaceOper.Clone();
            accountCardFee.Patient = this.Patient.Clone();

            return accountCardFee;
        }
        #endregion
    }

    /// <summary>
    /// ���������
    /// </summary>
    public enum AccCardFeeType
    {
        /// <summary>
        /// ������
        /// </summary>
        CardFee = 1,
        /// <summary>
        /// ����������
        /// </summary>
        CaseFee = 2,
        /// <summary>
        /// �Һŷ�
        /// </summary>
        RegFee = 3,
        /// <summary>
        /// ���
        /// </summary>
        DiaFee = 4,
        /// <summary>
        /// ����
        /// </summary>
        ChkFee = 5,
        /// <summary>
        /// �յ���
        /// </summary>
        AirConFee = 6,
        /// <summary>
        /// ��������
        /// </summary>
        OthFee = 7
    }

}
