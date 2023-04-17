using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Fee
{
    [System.Serializable]
    public class InvoiceJumpRecord : FS.FrameWork.Models.NeuObject
    {
        #region ����
        /// <summary>
        /// ��Ʊ��
        /// </summary>
        private FS.HISFC.Models.Fee.Invoice invoice = new Invoice();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �������
        /// </summary>
        private int happenNO;

        /// <summary>
        /// ����ʱ��Ʊ���ú�
        /// </summary>
        private string oldUsedNO = string.Empty;

        /// <summary>
        /// ���ź�Ʊ���ú�
        /// </summary>
        private string newUsedNO = string.Empty;

        #endregion

        #region ����
        /// <summary>
        /// ��Ʊ��
        /// </summary>
        public FS.HISFC.Models.Fee.Invoice Invoice
        {
            set
            {
                this.invoice = value;
            }
            get
            {
                return this.invoice;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            set
            {
                this.oper = value;
            }
            get
            {
                return this.oper;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int HappenNO
        {
            set
            {
                this.happenNO = value;
            }
            get
            {
                return this.happenNO;
            }
        }

        /// <summary>
        /// ����ʱ��Ʊ���ú�
        /// </summary>
        public string OldUsedNO
        {
            set
            {
                this.oldUsedNO = value;
            }
            get
            {
                return this.oldUsedNO;
            }
        }

        /// <summary>
        /// ���ź�Ʊ���ú�
        /// </summary>
        public string NewUsedNO
        {
            set
            {
                this.newUsedNO = value;
            }
            get
            {
                return this.newUsedNO;
            }
        }

        #endregion

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new InvoiceJumpRecord Clone()
        {
            InvoiceJumpRecord invoiceJumpRecord = base.Clone() as InvoiceJumpRecord;
            invoiceJumpRecord.Oper = this.Oper.Clone();
            invoiceJumpRecord.Invoice = this.Invoice.Clone();
            return invoiceJumpRecord;
        }

    }
}
