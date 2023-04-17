using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Fee.DayBalance.InpatientPrepay
{
    public partial class ucPrepayCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrepayCompare()
        {
            InitializeComponent();
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        Manager.PrepayDayBalance PrepayMgr = new SOC.Fee.DayBalance.Manager.PrepayDayBalance();
        FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceMgr = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();

        /// <summary>
        /// 退款金额是否统计预交金支付
        /// </summary>
        private string isCountPrepayPay = "1";//‘0’表示不统计，‘1’表示统计

        /// <summary>
        /// 退款金额是否统计预交金支付
        /// </summary>
        [Description("退款金额是否统计预交金支付，‘0’表示不统计，‘1’表示统计"), Category("设置")]
        public string IsCountPrepayPay
        {
            get
            {
                return this.isCountPrepayPay;
            }
            set
            {
                this.isCountPrepayPay = value;
            }
        }

        /// <summary>
        /// 获取收费员収退明细，并赋值在界面上
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        public void SetValue(SOC.Fee.DayBalance.Object.PrepayDayBalance prepay)
        {
            this.Clear();
            string operCode = prepay.BalancOper.ID;
            string begin = prepay.BeginDate;
            string end = prepay.EndDate;
            int intReturn = 0;
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            //对收预交金farpoint赋值
            intReturn = PrepayMgr.GetPrepayBalanceIncomeHistory(operCode, begin, end, ref ds);
            if (intReturn == -1)
            {
                MessageBox.Show("查找数据失败");
                return;
            }
            else
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    int row = this.neuSpread1_Sheet1.RowCount - 1;
                    this.neuSpread1_Sheet1.SetValue(row, 0, dr[0].ToString(), false);//预交金号
                    this.neuSpread1_Sheet1.SetValue(row, 1, dr[1].ToString().TrimStart(new char[] { '0' }), false);//住院号
                    this.neuSpread1_Sheet1.SetValue(row, 2, dr[2].ToString(), false);//姓名
                    this.neuSpread1_Sheet1.SetValue(row, 3, dr[3].ToString(), false);//操作日期
                    this.neuSpread1_Sheet1.SetValue(row, 4, dr[4].ToString(), false);//预交金额
                    this.neuSpread1_Sheet1.SetValue(row, 5, dr[5].ToString(), false);//操作人
                }
                int i = 0;
                decimal sum = 0;
                for (i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    sum += FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 4].Text.ToString());
                }
                string invoiceNum = this.neuSpread1_Sheet1.RowCount.ToString();//发票数
                string beginInvoiceNo = "";//开始发票
                string endInvoiceNo = "";//截止发票
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    beginInvoiceNo = this.neuSpread1_Sheet1.Cells[0, 0].Text.ToString();//开始发票
                    endInvoiceNo = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text.ToString();//截止发票
                }
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计:" + sum.ToString() + "                         " + "张数:" + invoiceNum + "                          " + "预交金号起止号:" + beginInvoiceNo + "-" + endInvoiceNo;
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "会计:" + "                            " + "出纳:" + "                           " + "复核:" + "                           " + "收费员:" + this.PrepayMgr.Operator.Name.ToString();
            }
            //对退预交金farpoint赋值
            intReturn = PrepayMgr.GetPrepayBalanceQuitHistory(operCode, begin, end, ref ds1);
            if (intReturn == -1)
            {
                MessageBox.Show("查找数据失败");
                return;
            }
            else
            {
                if (this.neuSpread2_Sheet1.RowCount > 0)
                    this.neuSpread2_Sheet1.Rows.Remove(0, this.neuSpread2_Sheet1.RowCount);
                DataTable dt = ds1.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.RowCount, 1);
                    int row = this.neuSpread2_Sheet1.RowCount - 1;
                    this.neuSpread2_Sheet1.SetValue(row, 0, dr[0].ToString(), false);//预交金号
                    this.neuSpread2_Sheet1.SetValue(row, 1, dr[1].ToString().TrimStart(new char[] { '0' }), false);//住院号
                    this.neuSpread2_Sheet1.SetValue(row, 2, dr[2].ToString(), false);//姓名
                    this.neuSpread2_Sheet1.SetValue(row, 3, dr[3].ToString(), false);//操作日期
                    this.neuSpread2_Sheet1.SetValue(row, 4, dr[4].ToString(), false);//退金额
                    this.neuSpread2_Sheet1.SetValue(row, 5, dr[5].ToString(), false);//操作人
                    this.neuSpread2_Sheet1.SetValue(row, 6, "退预交金", false);
                }
                if (isCountPrepayPay == "1")
                {
                    DataSet ds2 = new DataSet();
                    intReturn = PrepayMgr.GetPrepayBalanceQuitDetail(operCode, begin, end, ref ds2);
                    if (intReturn == -1)
                    {
                        MessageBox.Show("查找数据失败");
                        return;
                    }
                    DataTable dt1= ds2.Tables[0];
                    foreach (DataRow dr in dt1.Rows)
                    {
                        this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.RowCount, 1);
                        int row = this.neuSpread2_Sheet1.RowCount - 1;
                        this.neuSpread2_Sheet1.SetValue(row, 0, dr[0].ToString(), false);//预交金号
                        this.neuSpread2_Sheet1.SetValue(row, 1, dr[1].ToString().TrimStart(new char[] { '0' }), false);//住院号
                        this.neuSpread2_Sheet1.SetValue(row, 2, dr[2].ToString(), false);//姓名
                        this.neuSpread2_Sheet1.SetValue(row, 3, dr[3].ToString(), false);//操作日期
                        this.neuSpread2_Sheet1.SetValue(row, 4, dr[4].ToString(), false);//预交金支付金额
                        this.neuSpread2_Sheet1.SetValue(row, 5, dr[5].ToString(), false);//操作人
                        this.neuSpread2_Sheet1.SetValue(row, 6, "预交金支付", false);
                    }
                }
                int i = 0;
                decimal sum = 0;
                for (i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
                {
                    sum += FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread2_Sheet1.Cells[i,4].Text.ToString());
                }
                string invoiceNum = this.neuSpread2_Sheet1.RowCount.ToString();
                this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.RowCount, 1);
                this.neuSpread2_Sheet1.Models.Span.Add(this.neuSpread2_Sheet1.RowCount - 1, 0, 1, 7);
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, 0].Text = "合计:" + sum.ToString() + "                         " + "张数:" + invoiceNum ;
                this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.RowCount, 1);
                this.neuSpread2_Sheet1.Models.Span.Add(this.neuSpread2_Sheet1.RowCount - 1, 0, 1, 7);
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, 0].Text = "会计:" + "                              " + "出纳:" + "                              " + "复核:" + "                              " + "收费员:" + this.PrepayMgr.Operator.Name.ToString();
            }
            //获取预交金预交金使用情况
            ArrayList list = invoiceMgr.QueryInvoices(operCode, "P", false);
            if (list!=null||list.Count>0)
            {
                foreach (FS.HISFC.Models.Fee.Invoice info in list)
                {
                    if (info.ValidState == "1")
                    {
                        this.neuLblBeginInvoice1.Text = "初始预交金号;" + info.BeginNO.ToString();
                        this.neuLblBeginInvoice2.Text = "初始预交金号:" + info.BeginNO.ToString();
                        this.neuLblEndInvoice1.Text = "终止预交金号:" + info.EndNO.ToString();
                        this.neuLblEndInvoice2.Text = "终止预交金号:" + info.EndNO.ToString();
                        this.neuLblUsedInvoice1.Text = "已用预交金号:" + info.UsedNO.ToString();
                        this.neuLblUsedInvoice2.Text = "已用预交金号:" + info.UsedNO.ToString();
                    }
                }
            }
            //对界面上一些label赋值
            this.neuLblBeginTime1.Text = "日结开始时间:" + begin.ToString();
            this.neuLblBeginTime2.Text = "日结开始时间:" + begin.ToString();
            this.neuLblEndTime1.Text = "日结结束时间:" + end.ToString();
            this.neuLblEndTime2.Text = "日结结束时间:" + end.ToString();
            this.neuLblPrintTime1.Text = "打印时间:" + this.PrepayMgr.GetDateTimeFromSysDateTime().ToString();
            this.neuLblPrintTime2.Text = "打印时间:" + this.PrepayMgr.GetDateTimeFromSysDateTime().ToString();
            this.neuLblPrintPerson1.Text = "打印人:" + this.PrepayMgr.Operator.Name.ToString();
            this.neuLblPrintPerson2.Text = "打印人:" + this.PrepayMgr.Operator.Name.ToString();
        }


        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.PrintPrepayBalanceIncomeHistory();
            }
            else
            {
                this.PrintPrepayBalanceQuitHistory();
            }
        }

        /// <summary>
        /// 打印收预交金明细
        /// </summary>
        protected void PrintPrepayBalanceIncomeHistory()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, this.neuPanel3);
        }

        /// <summary>
        /// 打印退预交金明细
        /// </summary>
        protected void PrintPrepayBalanceQuitHistory()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, this.neuPanel6);
        }
        #endregion

        #region 清屏
        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }
            if (this.neuSpread2_Sheet1.RowCount > 0)
            {
                this.neuSpread2_Sheet1.Rows.Remove(0, this.neuSpread2_Sheet1.RowCount);
            }
            this.neuLblBeginTime1.Text = "日结开始时间:" ;
            this.neuLblBeginTime2.Text = "日结开始时间:";
            this.neuLblEndTime1.Text = "日结结束时间" ;
            this.neuLblEndTime2.Text = "日结结束时间";
            this.neuLblPrintTime1.Text = "打印时间";
            this.neuLblPrintTime2.Text = "打印时间";
            this.neuLblPrintPerson1.Text = "打印人";
            this.neuLblPrintPerson2.Text = "打印人";
            this.neuLblBeginInvoice1.Text = "初始预交金号：" ;
            this.neuLblBeginInvoice2.Text = "初始预交金号：" ;
            this.neuLblEndInvoice1.Text = "终止预交金号：" ;
            this.neuLblEndInvoice2.Text = "终止预交金号：";
            this.neuLblUsedInvoice1.Text = "已用预交金号：";
            this.neuLblUsedInvoice2.Text = "已用预交金号：";
        }

        #endregion

 
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("清屏", "清空屏幕", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    {
                        this.Clear();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public void Init()
        {
            this.neuLblHosName1.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.neuLblHosName2.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.Clear();
        }


        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }







    }
}
