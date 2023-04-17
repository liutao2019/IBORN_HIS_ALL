﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;

namespace FS.HISFC.Components.OutpatientFee.InvoicePrint
{
    public partial class ucInvoicePreviewGF : UserControl
    {
        public ucInvoicePreviewGF()
        {
            InitializeComponent();
        }

        #region 变量

        protected string invoiceType = "";//发票类别
        //protected FS.HISFC.Management.Fee.OutPatient outPatientManager = new FS.HISFC.Management.Fee.OutPatient();
        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        protected DataSet dsInvoice = new DataSet();
        protected ArrayList invoiceAndDetails = new ArrayList();//费用集合

        #endregion

        #region 属性

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
                        case "5": //全部发票
                            this.lblType.Text = "全      部";
                            break;
                        case "2": //记帐发票
                            this.lblType.Text = "记      帐";
                            break;
                        case "3":
                            this.lblType.Text = "特      殊";
                            break;
                        case "1":
                            this.lblType.Text = "自      费";
                            this.lblPub.Visible = false;
                            this.tbPub.Visible = false;
                            break;
                    }
                }
                catch { }
            }
        }
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

        #region 函数
        /// <summary>
        /// 查找符合的发票信息
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
        /// 查找符合的发票明细信息
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        private ArrayList FindInvoiceDetail(string invoiceType)
        {
            ArrayList alTemp = new ArrayList();
            ArrayList invoiceDetails = invoiceAndDetails[1] as ArrayList;
            //插入发票明细表
            foreach (ArrayList tempsInvoices in invoiceDetails)
            {
                foreach (ArrayList tempDetals in tempsInvoices)
                {
                    foreach (BalanceList detail in tempDetals)
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
        /// 显示费用
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
