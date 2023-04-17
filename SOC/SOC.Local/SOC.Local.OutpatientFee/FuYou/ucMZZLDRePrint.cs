using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.FuYou
{
    public partial class ucMZZLDRePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMZZLDRePrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 默认有效天数
        /// </summary>
        private int defaultValidDays = 1;

        /// <summary>
        /// 默认有效天数
        /// </summary>
        [Description("默认有效天数"), Category("设置"), Browsable(true)]
        public int DefaultValidDays
        {
            get { return defaultValidDays; }
            set { defaultValidDays = value; }
        }

        /// <summary>
        /// 有效天数
        /// </summary>
        private int validDays = 1;

        enum QueryType
        {
            CardNO,
            InvoiceNO
        }

        FS.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

        ucItemListBill ucItemListBill = new ucItemListBill();

        private string GetInvoiceNO(int rowIndex)
        {
            if (rowIndex > this.neuFpEnterInvoice_Sheet1.RowCount-1 || rowIndex < 0)
            {
                return "";
            }

            return this.neuFpEnterInvoice_Sheet1.Cells[rowIndex, 0].Text;
        }

        private void QueryInvoiceList(QueryType queryType)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查....");
            Application.DoEvents();

            if (queryType == QueryType.CardNO)
            {
                if(string.IsNullOrEmpty(this.ntxtCardNO.Text.Trim()))
                {
                    return;
                }
                string sql = @"select distinct
                                       f.invoice_no 发票号,
                                       f.card_no 病历号,
                                       r.name 姓名,
                                       decode(f.cancel_flag,'0','退费','1','正常','2','重打','3','注销','其它') 状态,
                                       f.fee_date 收费日期,
                                       f.fee_cpcd 收费员工号
                                from   fin_opb_feedetail f,fin_opr_register r
                                where  f.clinic_code = r.clinic_code
                                and    f.pay_flag = '1'
                                --and    f.drug_flag = '0'
                                and    f.card_no = '{0}'
                                and    f.fee_date > trunc(sysdate)-{1}";

                string cardNO = this.ntxtCardNO.Text.Trim().PadLeft(10, '0');
                int validDays = FS.FrameWork.Function.NConvert.ToInt32(this.ntxtValidDays.Text);
                if (validDays == 0)
                {
                    validDays = 1;
                }
                validDays = validDays - 1;

                sql = string.Format(sql, cardNO, validDays.ToString());

                DataSet ds = new DataSet();

                if (this.outpatientFeeMgr.ExecQuery(sql, ref ds) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("查询费用信息发生错误，请与系统管理员联系！");
                    return;
                }
                this.neuFpEnterInvoice_Sheet1.DataSource = ds;
                if (this.neuFpEnterInvoice_Sheet1.ColumnCount > 0)
                {
                    FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                    t.ReadOnly = true;
                    this.neuFpEnterInvoice_Sheet1.Columns[0].CellType = t;
                }
                if (this.neuFpEnterInvoice_Sheet1.RowCount == 1)
                {
                    this.QueryInvoiceDetail(this.GetInvoiceNO(0));
                }
            }

            else if (queryType == QueryType.InvoiceNO)
            {
                if (string.IsNullOrEmpty(this.ntxtInvoiceNO.Text.Trim()))
                {
                    return;
                }
                string sql = @"select distinct
                                       f.invoice_no 发票号,
                                       f.card_no 病历号,
                                       r.name 姓名,
                                       decode(f.cancel_flag,'0','退费','1','正常','2','重打','3','注销','其它') 状态,
                                       f.fee_date 收费日期,
                                       f.fee_cpcd 收费员工号
                                from   fin_opb_feedetail f,fin_opr_register r
                                where  f.clinic_code = r.clinic_code
                                and    f.pay_flag = '1'
                                --and    f.drug_flag = '0'
                                and    f.invoice_no = '{0}'";

                string invoiceNO = this.ntxtInvoiceNO.Text.Trim();
                sql = string.Format(sql, invoiceNO);

                DataSet ds = new DataSet();

                if (this.outpatientFeeMgr.ExecQuery(sql, ref ds) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("查询费用信息发生错误，请与系统管理员联系！");
                    return;
                }
                this.neuFpEnterInvoice_Sheet1.DataSource = ds;
                if (this.neuFpEnterInvoice_Sheet1.ColumnCount > 0)
                {
                    FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                    t.ReadOnly = true;
                    this.neuFpEnterInvoice_Sheet1.Columns[0].CellType = t;
                }
                if (this.neuFpEnterInvoice_Sheet1.RowCount == 1)
                {
                    this.QueryInvoiceDetail(this.GetInvoiceNO(0));
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void QueryInvoiceDetail(string invoiceNO)
        {
            if (string.IsNullOrEmpty(invoiceNO))
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查....");
            Application.DoEvents();

           
            ArrayList al = outpatientFeeMgr.QueryFeeItemListsByInvoiceNO(invoiceNO);
            if (al == null)
            {
                MessageBox.Show("根据发票号查询费用信息出错：" + outpatientFeeMgr.Err);
                return;
            }

            ucItemListBill.SetData(al);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }

        private void PrintZLD()
        {
            ucItemListBill.PrintBill();
        }

        private FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee GetPrintInterfaceImplement()
        {
            object oInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.OutpatientFee.FuYou.ucMZZLDRePrint),
                typeof(FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee));
            if (oInterfaceImplement != null && oInterfaceImplement is FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee)
            {
                return oInterfaceImplement as FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee;
            }
            return null;
        }

        public override int Print(object sender, object neuObject)
        {
            this.PrintZLD();
            return base.Print(sender, neuObject);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            if (string.IsNullOrEmpty(this.ntxtCardNO.Text))
            {
                this.QueryInvoiceList(QueryType.InvoiceNO);
            }
            else
            {
                this.QueryInvoiceList(QueryType.CardNO);
            }
            return base.OnQuery(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.ntxtValidDays.Text = this.defaultValidDays.ToString();
            this.validDays = this.defaultValidDays;
            ucItemListBill.Dock = DockStyle.None;
            this.neuPanelZLD.Controls.Add(ucItemListBill);


            this.ntxtCardNO.KeyUp += new KeyEventHandler(ntxtCardNO_KeyUp);
            this.ntxtInvoiceNO.KeyUp += new KeyEventHandler(ntxtInvoiceNO_KeyUp);
            this.neuFpEnterInvoice.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(neuFpEnterInvoice_CellClick);

        }

        void neuFpEnterInvoice_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.QueryInvoiceDetail(this.GetInvoiceNO(e.Row));
        }

        void ntxtInvoiceNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryInvoiceList(QueryType.InvoiceNO);
            }
        }

        void ntxtCardNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.ntxtCardNO.Text))
                {
                    this.ntxtInvoiceNO.Select();
                    this.ntxtInvoiceNO.SelectAll();
                    this.ntxtInvoiceNO.Focus();
                }
                else
                {
                    this.QueryInvoiceList(QueryType.CardNO);
                }
            }
        }


        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareItemList : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList o1 = (x as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Clone();
                FS.HISFC.Models.Fee.Outpatient.FeeItemList o2 = (y as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Clone();

                string oX = "";
                string oY = "";


                oX = o1.UndrugComb.ID;
                oY = o2.UndrugComb.ID;

                return string.Compare(oX, oY);
            }
        }
    }
}
