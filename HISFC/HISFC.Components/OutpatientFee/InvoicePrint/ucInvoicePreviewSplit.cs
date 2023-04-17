using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.OutpatientFee.InvoicePrint
{
    public partial class ucInvoicePreviewSplit : UserControl
    {
        public ucInvoicePreviewSplit()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ʊ���
        /// </summary>
        protected string invoiceType = "";

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ��Ʊͳ�����DataSet
        /// </summary>
        protected DataSet dsInvoice = new DataSet();

        /// <summary>
        /// ��Ʊ����ϸ�������
        /// </summary>
        protected ArrayList invoiceAndDetails = new ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public string InvoiceType
        {
            get
            {
                return invoiceType;
            }
            set
            {
                try
                {
                    invoiceType = value;

                    switch (invoiceType)
                    {
                        case "5": //ȫ����Ʊ
                            this.lblType.Text = "ȫ      ��";
                            break;
                        case "2": //���ʷ�Ʊ
                            this.lblType.Text = "��      ��";
                            break;
                        case "3":
                            this.lblType.Text = "��      ��";
                            break;
                        case "1":
                            this.lblType.Text = "��      ��";
                            this.lblPub.Visible = false;
                            this.tbPub.Visible = false;
                            break;
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// ��Ʊ����ϸ�������
        /// </summary>
        public ArrayList InvoiceAndDetails
        {
            set
            {
                try
                {
                    invoiceAndDetails = value;
                    this.DisPlay();
                }
                catch { }
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ҷ��ϵķ�Ʊ��Ϣ
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Outpatient.Balance FindInvoice(string invoiceType)
        {
            ArrayList alTemp = invoiceAndDetails[0] as ArrayList;
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in alTemp)
            {
                if (invoice.Memo == invoiceType)
                {
                    return invoice;
                }
            }

            return null;
        }
        /// <summary>
        /// ���ҷ��ϵķ�Ʊ��ϸ��Ϣ
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        private ArrayList FindInvoiceDetail(string invoiceType)
        {
            ArrayList alTemp = new ArrayList();
            ArrayList invoiceDetails = invoiceAndDetails[1] as ArrayList;
            //���뷢Ʊ��ϸ��
            foreach (ArrayList tempsInvoices in invoiceDetails)
            {
                foreach (ArrayList tempDetals in tempsInvoices)
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList detail in tempDetals)
                    {
                        if (detail.Memo == invoiceType)
                        {
                            alTemp.Add(detail);
                        }
                    }
                }
            }
            return alTemp;
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        public void DisPlay()
        {
            this.fpSpread1_Sheet1.Rows.Count = 0;
            this.fpSpread1_Sheet1.Rows.Count = 8;

            FS.HISFC.Models.Fee.Outpatient.Balance invoice = new FS.HISFC.Models.Fee.Outpatient.Balance();
            ArrayList invoiceDetails = new ArrayList();

            decimal pccAndPczCost = 0m;

            invoice = FindInvoice(invoiceType);
            invoiceDetails = FindInvoiceDetail(invoiceType);

            foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList detail in invoiceDetails)
            {
                int y = detail.InvoiceSquence / 8;
                int x = detail.InvoiceSquence - 8 * y;
                if (x == 0)
                {
                    x = 8;
                }
                if (y == 3)
                {
                    y = 2;
                }
                this.fpSpread1_Sheet1.Cells[x - 1, 2 * y].Text = detail.FeeCodeStat.Name;
                this.fpSpread1_Sheet1.Cells[x - 1, 2 * y + 1].Text = detail.BalanceBase.FT.TotCost.ToString();

                if (detail.InvoiceSquence == 2 || detail.InvoiceSquence == 3)
                {
                    pccAndPczCost += detail.BalanceBase.FT.TotCost;
                }
            }
            if (invoice == null)
            {
                this.tbTot.Text = "0";
                this.tbPub.Text = "0";
                this.tbOwn.Text = "0";
                this.txtPccAndPcz.Text = "0";
            }
            else
            {
                this.tbTot.Text = invoice.FT.TotCost.ToString();
                this.tbPub.Text = invoice.FT.PubCost.ToString();
                this.tbOwn.Text = (invoice.FT.OwnCost + invoice.FT.PayCost).ToString();
                this.txtPccAndPcz.Text = pccAndPczCost.ToString();
            }
        }

        #endregion
    }
}
