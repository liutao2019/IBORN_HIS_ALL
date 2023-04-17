/*----------------------------------------------------------------
            // �ļ�����			ClinicDayBalanceClass.cs
            // �ļ�����������	�����տ��ս�ʵ����
            //
            // 
            // ������ʶ��		2006-3-23
            //
            // �޸ı�ʶ��
            // �޸�������
            //
            // �޸ı�ʶ��
            // �޸�������
//----------------------------------------------------------------*/
using System;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Fee;
using FS.FrameWork.Models;

namespace FS.WinForms.Report.OutpatientFee.Class
{
    /// <summary>
    /// �����տ��ս�ʵ����
    /// </summary>
    public class ClinicDayBalance : FS.FrameWork.Models.NeuObject
    {
        public ClinicDayBalance()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        #region ����
        /// <summary>
        /// �ս����
        /// </summary>
        string balanceSequence = "";

        /// <summary>
        /// �ս����ݿ�ʼʱ��
        /// </summary>
        System.DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// �ս����ݽ�ֹʱ��
        /// </summary>
        System.DateTime endDate = DateTime.MinValue;

        /// <summary>
        /// �ս����Ա
        /// </summary>
        FS.FrameWork.Models.NeuObject balanceOperator = new NeuObject();

        /// <summary>
        /// �ս����ʱ��
        /// </summary>
        System.DateTime balanceDate = DateTime.MinValue;

        /// <summary>
        /// ������˱�־��1-δ���/2-�����
        /// </summary>
        string checkFlag = "1";

        /// <summary>
        /// ���������
        /// </summary>
        FS.FrameWork.Models.NeuObject checkOperator = new NeuObject();

        /// <summary>
        /// �������ʱ��
        /// </summary>
        System.DateTime checkDate = DateTime.MinValue;

        /// <summary>
        /// �ս���Ŀ
        /// </summary>
        FS.HISFC.Models.Base.CancelTypes balanceItem = CancelTypes.Valid;

        /// <summary>
        /// �ս���Ŀ��Ӧ�ķ�Ʊ�����䷶Χ��Ʊ��
        /// </summary>
        string invoiceNo = "";

        /// <summary>
        /// ���ʵ�����
        /// </summary>
        int accountNumber = 0;

        /// <summary>
        /// ˢ������
        /// </summary>
        int cdNumber = 0;

        /// <summary>
        /// �˷�����
        /// </summary>
        int bkNumber = 0;

        /// <summary>
        /// �˷ѷ�Ʊ��
        /// </summary>
        string bkInvoiceNo = "";

        /// <summary>
        /// ��������
        /// </summary>
        int unValidNumber = 0;

        /// <summary>
        /// ���Ϸ�Ʊ��
        /// </summary>
        string unValidInvoiceNo = "";

        /// <summary>
        /// Ʊ�ݺŷ�Χ
        /// </summary>
        string recipeBand = "";

        /// <summary>
        /// ��Ʊ�ŷ�Χ
        /// </summary>
        string invoiceBand = "";

        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        string extendField = "";

        /// <summary>
        /// ��ע���1
        /// </summary>
        decimal backCost1 = 0;

        /// <summary>
        /// ��ע���2
        /// </summary>
        decimal backCost2 = 0;

        /// <summary>
        /// ��ע���3
        /// </summary>
        decimal backCost3 = 0;

        /// <summary>
        /// ���ֽ��
        /// </summary>
        FS.HISFC.Models.Base.FT cost = new FT();
        #endregion

        #region ����
        /// <summary>
        /// �ս����
        /// </summary>
        public string BalanceSequence
        {
            get
            {
                return this.balanceSequence;
            }
            set
            {
                this.balanceSequence = value;
            }
        }


        /// <summary>
        /// �ս����ݿ�ʼʱ��
        /// </summary>
        public System.DateTime BeginDate
        {
            get
            {
                return this.beginDate;
            }
            set
            {
                this.beginDate = value;
            }
        }


        /// <summary>
        /// �ս����ݽ�ֹʱ��
        /// </summary>
        public System.DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }


        /// <summary>
        /// �ս����Ա
        /// </summary>
        public FS.FrameWork.Models.NeuObject BalanceOperator
        {
            get
            {
                return this.balanceOperator;
            }
            set
            {
                this.balanceOperator = value;
            }
        }


        /// <summary>
        /// �ս����ʱ��
        /// </summary>
        public System.DateTime BalanceDate
        {
            get
            {
                return this.balanceDate;
            }
            set
            {
                this.balanceDate = value;
            }
        }


        /// <summary>
        /// ������˱�־��1-δ���/2-�����
        /// </summary>
        public string CheckFlag
        {
            get
            {
                return this.checkFlag;
            }
            set
            {
                this.checkFlag = value;
            }
        }


        /// <summary>
        /// ���������
        /// </summary>
        public FS.FrameWork.Models.NeuObject CheckOperator
        {
            get
            {
                return this.checkOperator;
            }
            set
            {
                this.checkOperator = value;
            }
        }


        /// <summary>
        /// �������ʱ��
        /// </summary>
        public System.DateTime CheckDate
        {
            get
            {
                return this.checkDate;
            }
            set
            {
                this.checkDate = value;
            }
        }


        /// <summary>
        /// �ս���Ŀ(0-������1-�˷ѣ�2-�ش�3-ע��)
        /// </summary>
        public FS.HISFC.Models.Base.CancelTypes BalanceItem
        {
            get
            {
                return this.balanceItem;
            }
            set
            {
                this.balanceItem = value;
            }
        }


        /// <summary>
        /// �ս���Ŀ��Ӧ�ķ�Ʊ�����䷶Χ��Ʊ��
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
        /// ���ʵ�����
        /// </summary>
        public int AccountNumber
        {
            get
            {
                return this.accountNumber;
            }
            set
            {
                this.accountNumber = value;
            }
        }


        /// <summary>
        /// ˢ������
        /// </summary>
        public int CDNumber
        {
            get
            {
                return this.cdNumber;
            }
            set
            {
                this.cdNumber = value;
            }
        }


        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        public string ExtendField
        {
            get
            {
                return this.extendField;
            }
            set
            {
                this.extendField = value;
            }
        }


        /// <summary>
        /// ��ע���1
        /// </summary>
        public decimal BackCost1
        {
            get
            {
                return this.backCost1;
            }
            set
            {
                this.backCost1 = value;
            }
        }


        /// <summary>
        /// ��ע���2
        /// </summary>
        public decimal BackCost2
        {
            get
            {
                return this.backCost2;
            }
            set
            {
                this.backCost2 = value;
            }
        }


        /// <summary>
        /// ��ע���3
        /// </summary>
        public decimal BackCost3
        {
            get
            {
                return this.backCost3;
            }
            set
            {
                this.backCost3 = value;
            }
        }


        /// <summary>
        /// �˷ѷ�Ʊ����
        /// </summary>
        public int BKNumber
        {
            get
            {
                return this.bkNumber;
            }
            set
            {
                this.bkNumber = value;
            }
        }


        /// <summary>
        /// �˷ѷ�Ʊ��Χ
        /// </summary>
        public string BKInvoiceNo
        {
            get
            {
                return this.bkInvoiceNo;
            }
            set
            {
                this.bkInvoiceNo = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public int UnValidNumber
        {
            get
            {
                return this.unValidNumber;
            }
            set
            {
                this.unValidNumber = value;
            }
        }


        /// <summary>
        /// ���Ϸ�Ʊ��Χ
        /// </summary>
        public string UnValidInvoiceNo
        {
            get
            {
                return this.unValidInvoiceNo;
            }
            set
            {
                this.unValidInvoiceNo = value;
            }
        }


        /// <summary>
        /// Ʊ�ݺ�����
        /// </summary>
        public string RecipeBand
        {
            get
            {
                return this.recipeBand;
            }
            set
            {
                this.recipeBand = value;
            }
        }


        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public string InvoiceBand
        {
            get
            {
                return this.invoiceBand;
            }
            set
            {
                this.invoiceBand = value;
            }
        }


        /// <summary>
        /// ���ֽ��ܽ�Cost.tot_cost/ʵ�ս�Cost.own_cost/���ʽ�Cost.left_cost��
        /// </summary>
        public FS.HISFC.Models.Base.FT Cost
        {
            get
            {
                return this.cost;
            }
            set
            {
                this.cost = value;
            }
        }
        #endregion

        #region ��¡
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new ClinicDayBalance Clone()
        {
            ClinicDayBalance clinicDayBalance = new ClinicDayBalance().Clone();
            clinicDayBalance.BalanceOperator = this.BalanceOperator.Clone();
            clinicDayBalance.CheckOperator = this.CheckOperator.Clone();
            clinicDayBalance.Cost = this.Cost.Clone();
            return clinicDayBalance;
        }
        #endregion
    }
}
