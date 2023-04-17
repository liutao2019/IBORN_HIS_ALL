using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class ParkingTicketFeeInfo : NeuObject, IValidState
    {

        #region ����

        /// <summary>
        /// �����
        /// </summary>
        private string invoiceNo = string.Empty;

        /// <summary>
        /// �������� TransTypes.Positive ������(1), TransTypes.Negative ������(2)
        /// </summary>
        private TransTypes transType;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string itemCode = "";
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string itemName = "";
        /// <summary>
        /// ����
        /// </summary>
        private decimal unitPrice = 0m;
        /// <summary>
        /// ��������
        /// </summary>
        private decimal qty = 0m;
        /// <summary>
        /// ��λ
        /// </summary>
        private string unit = "";

        /// <summary>
        ///   ���׽��
        /// </summary>
        private decimal totCost = 0m;
        /// <summary>
        /// ֧����ʽ
        /// </summary>
        private FS.FrameWork.Models.NeuObject payMode = new NeuObject();
        /// <summary>
        /// ͣ��Ʊ��
        /// </summary>
        private string ticketNo = "";
        /// <summary>
        ///  �ɵ�Ʊ�ݺ�
        /// </summary>
        private string oldInvoiceNo = "";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime cancelDate = new DateTime();
        /// <summary>
        /// �˷ѵ�ͣ��Ʊ��
        /// </summary>
        private string oldTicketNo = "";
        /// <summary>
        /// ��ע
        /// </summary>
        private string memo = "";
        /// <summary>
        /// ��չ�ֶ�1
        /// </summary>
        private string flag1 = "";
        /// <summary>
        /// ��չ�ֶ�2
        /// </summary>
        private string flag2 = "";
        /// <summary>
        /// ��չ�ֶ�3
        /// </summary>
        private string flag3 = "";
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private EnumValidState validState = EnumValidState.Valid;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment operEnvironment;
        /// <summary>
        /// �Ƿ��սᣬ0δ�ս�/1���ս�
        /// </summary>
        private bool isBalance = false;
        /// <summary>
        /// �ս��
        /// </summary>
        private string balanceNo = "";
        /// <summary>
        /// �ս���Ϣ
        /// </summary>				
        private OperEnvironment balanceEnvironment;
        /// <summary>
        /// �Ƿ���ˣ�0δ�˲�/1�Ѻ˲�
        /// </summary>
        private bool isCheck = false;
        /// <summary>
        /// �����Ϣ
        /// </summary>				
        private OperEnvironment checkEnvironment;


        private string hospital_id;

        private string hospital_name;
        #endregion

        #region ����


        /// <summary>
        /// �����
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
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
        /// ��Ŀ����
        /// </summary>
        public string ItemCode
        {
            get { return this.itemCode; }
            set { itemCode = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemName
        {
            get { return this.itemName; }
            set { itemName = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
            set
            {
                this.unitPrice = value;
            }
        }
        /// <summary>
        /// ��λ
        /// </summary>
        public string Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                this.unit = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Qty
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
            }
        }
        /// <summary>
        /// ���׽��
        /// </summary>
        public decimal TotCost
        {
            get
            {
                return this.totCost;
            }
            set
            {
                this.totCost = value;
            }
        }
        /// <summary>
        /// ���׷�ʽ
        /// </summary>
        public FS.FrameWork.Models.NeuObject PayMode
        {
            get { return this.payMode; }
            set { payMode = value; }
        }
        /// <summary>
        /// ͣ��Ʊ��
        /// </summary>
        public string TicketNo
        {
            get
            {
                return this.ticketNo;
            }
            set
            {
                this.ticketNo = value;
            }
        }
        /// <summary>
        /// �ɵ�Ʊ�ݺ�
        /// </summary>
        public string OldInvoiceNo
        {
            get
            {
                return this.oldInvoiceNo;
            }
            set
            {
                this.oldInvoiceNo = value;
            }
        }  
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CancelDate
        {
            get { return this.cancelDate; }
            set { cancelDate = value; }
        }
        /// <summary>
        /// �˷ѵ�ͣ��Ʊ��
        /// </summary>
        public string OldTicketNo
        {
            get
            {
                return this.oldTicketNo;
            }
            set
            {
                this.oldTicketNo = value;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo
        {
            get
            {
                return this.memo;
            }
            set
            {
                this.memo = value;
            }
        }
        /// <summary>
        ///  ��չ�ֶ�1
        /// </summary>
        public string Flag1
        {
            get
            {
                return this.flag1;
            }
            set
            {
                this.flag1 = value;
            }
        }
        /// <summary>
        ///  ��չ�ֶ�2
        /// </summary>
        public string Flag2
        {
            get
            {
                return this.flag2;
            }
            set
            {
                this.flag2 = value;
            }
        }


        /// <summary>
        ///  ��չ�ֶ�3
        /// </summary>
        public string Flag3
        {
            get
            {
                return this.flag3;
            }
            set
            {
                this.flag3 = value;
            }
        }

        /// <summary>
        /// ��Ʊ״̬ 0��Ч 1��Ч
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
        /// <summary>
        /// �Ƿ��ս�
        /// </summary>
        public bool IsBalance
        {
            get
            {
                return this.isBalance;
            }
            set
            {
                this.isBalance = value;
            }
        }
        /// <summary>
        /// �ս��
        /// </summary>
        public string BalanceNo
        {
            get
            {
                return this.balanceNo;
            }
            set
            {
                this.balanceNo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheck
        {
            get
            {
                return this.isCheck;
            }
            set
            {
                this.isCheck = value;
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
        /// �սỷ��
        /// </summary>
        public OperEnvironment BalanceEnvironment
        {
            get
            {
                if (balanceEnvironment == null)
                {
                    balanceEnvironment = new OperEnvironment();
                }
                return this.balanceEnvironment;
            }
            set
            {
                this.balanceEnvironment = value;
            }
        }
        /// <summary>
        /// ��˻���
        /// </summary>
        public OperEnvironment CheckEnvironment
        {
            get
            {
                if (checkEnvironment == null)
                {
                    checkEnvironment = new OperEnvironment();
                }
                return this.checkEnvironment;
            }
            set
            {
                this.checkEnvironment = value;
            }
        }

      
        /// <summary>
        /// Ժ�����{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }

        /// <summary>
        /// Ժ�����{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }

        #endregion


        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new ParkingTicketFeeInfo Clone()
        {
            ParkingTicketFeeInfo ParkingTicketFeeInfo = base.Clone() as ParkingTicketFeeInfo;

            return ParkingTicketFeeInfo;
        }
        #endregion
    }
}
