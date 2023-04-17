using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// ��ʼ����Ʊ��
    /// </summary>
    public partial class ucChangeInvoiceNO : UserControl
    {
        public ucChangeInvoiceNO()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ʊʵ��
        /// </summary>
        private FS.HISFC.Models.Fee.Invoice invoice = new FS.HISFC.Models.Fee.Invoice();

        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        public event EventHandler my;

        #endregion

        #region ����

        /// <summary>
        /// ���뷢Ʊʵ��
        /// </summary>
        public FS.HISFC.Models.Fee.Invoice Invoice
        {
            set
            {
                this.invoice = value;
                this.SetValue(this.invoice);
            }
            get
            {
                return this.invoice;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��ֵ
        /// </summary>
        private void SetValue(FS.HISFC.Models.Fee.Invoice invoiceJumpRecord)
        {
            this.lblBeginNO.Text = invoiceJumpRecord.BeginNO;
            this.lblEndNO.Text = invoiceJumpRecord.EndNO;
            this.lblInvoiceTypeID.Text = invoiceJumpRecord.Type.ID;
            this.lblInvoiceTypeName.Text = invoiceJumpRecord.Type.Name;
            this.lblUseNO.Text = invoiceJumpRecord.UsedNO;
            this.lblAcceptPerson.Text = invoiceJumpRecord.AcceptOper.ID;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string nextInoviceNo = this.feeIntegrate.GetNewInvoiceNO(invoiceJumpRecord.Type.ID);
            if (nextInoviceNo == string.Empty)
            {
                this.lblNextNO.Text = "���Ŷ��Ѿ�ʹ����";
            }
            else
            {
                this.lblNextNO.Text = nextInoviceNo;
            }
            FS.FrameWork.Management.PublicTrans.RollBack();

            this.txtInput.Focus();

        }

        /// <summary>
        /// У�������
        /// </summary>
        /// <param name="inputNO"></param>
        /// <returns></returns>
        private int ValidInputNO(string inputNO)
        {
            //2014-09-24 by han-zf ��Ʊǰ׺���ȼ���,����������ֵķ�Ʊ���ų���
            string prefixStr = string.Empty;
            this.GetPrefix(this.lblBeginNO.Text, this.lblEndNO.Text, ref prefixStr);
            int prefixLength = prefixStr.Length;

            inputNO = inputNO.Substring(prefixLength);
            Int64 intInutno;
            try
            {
                intInutno = Int64.Parse(inputNO);
            }
            catch (Exception)
            {

                MessageBox.Show("���뷢Ʊ��Ӧ��Ϊ���֣�����������");
                this.txtInput.Focus();
                return -1;
            }

            Int64 intBegin = Int64.Parse(this.invoice.BeginNO.Substring(prefixLength));
            Int64 intEnd = Int64.Parse(this.invoice.EndNO.Substring(prefixLength));
            Int64 intUsedNO = Int64.Parse(this.invoice.UsedNO.Substring(prefixLength));
            Int64 intNextNO;
            try
            {
                intNextNO = Int64.Parse(this.lblNextNO.Text.Substring(prefixLength));
            }
            catch (Exception)
            {

                MessageBox.Show("�úŶ��Ѿ�ʹ���꣬���ܵ���");
                return -1;
            }

            //�úŶ��Ѿ�ʹ���꣬���ܵ���
            if (intEnd <= intUsedNO)
            {
                MessageBox.Show("�úŶ��Ѿ�ʹ���꣬���ܵ���");
                return -1;
            }

            //Int64 intInputTemp = intInutno -1;

            //У�����������

            if (!(intInutno >= intBegin && intInutno <= intEnd))
            {
                MessageBox.Show("���Ӧ���ںŶ�֮�䣬����������");
                return -1;
            }

            if (intInutno <= intUsedNO)
            {
                MessageBox.Show("������Ѿ�ʹ�ò��ܵ���");
                return -1;
            }

            if (intInutno == intNextNO)
            {
                MessageBox.Show("���������һ����ͬ���������");
                return -1;
            }

            return 1;
        }

        protected virtual int Save()
        {
            string inputNO = this.txtInput.Text;

            if (string.IsNullOrEmpty(inputNO))
            {
                MessageBox.Show("�����뷢Ʊ��");
                return -1;
            }

            //inputNO = inputNO.PadLeft(12,'0');  
            //��ʽ����Ʊ
            this.txtInput.Text = this.txtInput.Text.PadLeft(10, '0');
            string prefixStr = string.Empty;
            this.GetPrefix(this.lblBeginNO.Text, this.lblEndNO.Text, ref prefixStr);
            int prefixLength = prefixStr.Length;//ǰ׺����
            int invoiceLength = this.lblBeginNO.Text.Length;//��Ʊ����
            int suffixLength = invoiceLength - prefixLength;//��׺����
            //��Ʊ=ǰ׺+��׺
            this.txtInput.Text = prefixStr + this.txtInput.Text.Substring(this.txtInput.Text.Length - suffixLength, suffixLength);

            inputNO = this.txtInput.Text;

            int returnValue = this.ValidInputNO(inputNO);
            if (returnValue == -1)
            {
                this.txtInput.Focus();
                return -1;
            }

            FS.HISFC.Models.Fee.InvoiceJumpRecord invoiceJumpRecord = this.GetInvoiceChangeRecord();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.feeIntegrate.InsertInvoiceJumpRecord(invoiceJumpRecord);
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.Trans.Rollback();
                MessageBox.Show("���ó���" + this.feeIntegrate.Err);
                return -1;
            }
            //FS.FrameWork.Management.PublicTrans.Trans.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("���ųɹ�");
            this.SetValue(invoiceJumpRecord.Invoice);

            return 1;
        }

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <returns></returns>
        protected virtual FS.HISFC.Models.Fee.InvoiceJumpRecord GetInvoiceChangeRecord()
        {
            string prefixStr = string.Empty;
            this.GetPrefix(this.lblBeginNO.Text, this.lblEndNO.Text, ref prefixStr);
            int prefixLength = prefixStr.Length;

            FS.HISFC.Models.Fee.InvoiceJumpRecord invoiceJumpRecord = new FS.HISFC.Models.Fee.InvoiceJumpRecord();

            invoiceJumpRecord.Invoice = this.Invoice;
            invoiceJumpRecord.OldUsedNO = this.Invoice.UsedNO;
            //invoiceJumpRecord.Invoice.UsedNO = (Int64.Parse(this.txtInput.Text) - 1).ToString().PadLeft(12, '0');
            //invoiceJumpRecord.Invoice.UsedNO = (Int64.Parse(this.txtInput.Text) - 1).ToString();
            invoiceJumpRecord.Invoice.UsedNO = prefixStr + (Int64.Parse(this.txtInput.Text.Substring(prefixLength)) - 1).ToString();

            //invoiceJumpRecord.NewUsedNO = (Int64.Parse(this.txtInput.Text) - 1).ToString().PadLeft(12, '0');
            //invoiceJumpRecord.NewUsedNO = (Int64.Parse(this.txtInput.Text) - 1).ToString();
            invoiceJumpRecord.NewUsedNO = prefixStr + (Int64.Parse(this.txtInput.Text.Substring(prefixLength)) - 1).ToString();
            invoiceJumpRecord.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;


            return invoiceJumpRecord;
        }

        /// <summary>
        /// ��÷�Ʊ�Ŷ�ǰ׺
        /// </summary>
        /// <param name="bgn"></param>
        /// <param name="end"></param>
        /// <param name="prefix"></param>
        private void GetPrefix(string bgn, string end, ref string prefix)
        {
            prefix = string.Empty;//��Ʊ����ͬ��ǰ׺
            int prefixLength = 0;
            for (int i = 0; i < bgn.Length; i++)
            {
                if (bgn[i] == end[i])
                {
                    prefix = prefix + bgn[i];
                }
            }
        }
        #endregion


        protected override void OnLoad(EventArgs e)
        {
            this.FindForm().Text = "��Ʊ����";
            this.txtInput.Focus();
            base.OnLoad(e);
        }

        private void btOk_Click(object sender, EventArgs e)
        {

            int returnValue = this.Save();

            if (returnValue == -1)
            {
                return;
            }
            else
            {
                this.FindForm().DialogResult = DialogResult.OK;
                this.FindForm().Close();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void neuTabControl1_Enter(object sender, EventArgs e)
        {
            this.txtInput.Focus();
        }

    }
}
