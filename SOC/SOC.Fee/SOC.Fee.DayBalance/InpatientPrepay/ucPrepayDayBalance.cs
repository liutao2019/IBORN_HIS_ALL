using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Fee.DayBalance.InpatientPrepay
{
    public partial class ucPrepayDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrepayDayBalance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 退款金额是否统计预交金支付
        /// </summary>
        private string isCountPrepayPay= "1";//‘0’表示不统计，‘1’表示统计

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

        private bool isSavedPrint = false;
        [Description("保存后是否自动打印；true=是；false=否"),Category("设置")]
        public bool IsSavedPrint
        {
            get
            {
                return isSavedPrint;
            }
            set
            {
                isSavedPrint = value;
            }
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        Manager.PrepayDayBalance PrepayMgr = new SOC.Fee.DayBalance.Manager.PrepayDayBalance();
        SOC.Fee.DayBalance.Object.PrepayDayBalance prepayBalance = null;

        DateTime dtBegein = DateTime.MinValue;
        DateTime dtEnd = DateTime.MinValue;
        bool isSave = true;//判断是否可以保存，点击左边的历史时间产生的数据不准保存

        private void Init()
        {
            prepayBalance = new SOC.Fee.DayBalance.Object.PrepayDayBalance();
            //获取当前收费员上次日结时间和当前时间
            PrepayMgr.GetPrepayDayBalanceDate( ref dtBegein, ref dtEnd);
            if (dtBegein == DateTime.MinValue)
            {
                dtBegein = new DateTime(2010,12,29);
            }
            this.neuDateTimePicker1.Value = dtBegein;
            this.neuDateTimePicker2.Value = dtEnd;
            this.neuDateTimePicker1.Enabled = false;
            //左边显示最近一个月日结记录
            this.neuDateTimePicker3.Value = dtEnd;
            //显示制表人为当前登录人
            this.neuLblMakeTableName.Text = this.PrepayMgr.Operator.Name;
        }
        private void Query()
        {
            isSave = true;
            if (this.neuDateTimePicker2.Value < this.neuDateTimePicker1.Value)
            {
                MessageBox.Show("开始时间大于结束时间。请调整！");
                return;
            }
            if (this.neuDateTimePicker2.Value > PrepayMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("结束时间不能大于当前时间。请调整！");
                return;
            }
            this.dtBegein = this.neuDateTimePicker1.Value;
            this.dtEnd = this.neuDateTimePicker2.Value;
            //左边显示最近一个月日结记录
            DateTime dtBegein1 = new DateTime(this.neuDateTimePicker3.Value.Year, this.neuDateTimePicker3.Value.Month, 1, 0, 0, 0);//取当月的开始时间
            DateTime dtEnd1 = dtBegein1.AddMonths(1);//取当月的结束时间
            int intReturn = 0;
            DataSet ds = new DataSet();
            intReturn = PrepayMgr.GetPrepayBalanceHistory(this.PrepayMgr.Operator.ID, dtBegein1.ToString(), dtEnd1.ToString(), ref ds);
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
                    this.neuSpread1_Sheet1.Rows[row].Tag = dr[0].ToString();//结算序号
                    this.neuSpread1_Sheet1.SetValue(row, 0, dr[1].ToString(), false);//结算结束时间
                }
            }
            int invoiceCount = 0;
            string beginINvoice = "";
            string endInvoice = "";
            this.lblDateRange.Text = dtBegein.ToString() + "-" + this.neuDateTimePicker2.Value.ToString();
            this.PrepayMgr.GetPrepayBalanceInvoice(this.PrepayMgr.Operator.ID, dtBegein.ToString(),
                this.dtEnd.ToString(), ref invoiceCount, ref beginINvoice, ref endInvoice);

            this.lblInvCount.Text = invoiceCount.ToString();
            this.lblInvoiceRand.Text = "流水号：" + beginINvoice + "-" + endInvoice;

            decimal totcost = 0;
            decimal cacost = 0;
            decimal poscost = 0;
            decimal chcost = 0;
            decimal orcost = 0;
            decimal fgcost = 0;
            decimal quitcost = 0;
            this.PrepayMgr.GetPrepayBalanceCost(this.PrepayMgr.Operator.ID, dtBegein.ToString(), this.dtEnd.ToString(), out totcost, out cacost, out poscost,
                out chcost, out orcost, out fgcost);

            this.PrepayMgr.GetPrepayQuitCost(this.PrepayMgr.Operator.ID, dtBegein.ToString(), this.dtEnd.ToString(), out  quitcost);

            if (isCountPrepayPay == "1")
            {
                decimal cacost1 = 0;
                decimal poscost1 = 0;
                decimal chcost1 = 0;
                decimal orcost1 = 0;
                decimal fgcost1 = 0;
                decimal prepayPay = 0;
                this.PrepayMgr.GetPrepayPayCost(this.PrepayMgr.Operator.ID, dtBegein.ToString(), this.dtEnd.ToString(), out  prepayPay, out cacost1, out poscost1, out chcost1, out orcost1, out fgcost1);
                quitcost = quitcost - prepayPay;             
                totcost = totcost - prepayPay;
                cacost = cacost - cacost1- poscost1;//用预交金支付的部分应该全部算现金
                poscost = poscost ;
            }

            this.lblQF.Text = (-quitcost).ToString("0.00");
            this.lblTOT.Text = (totcost).ToString("0.00");
            this.lblYJ.Text = (totcost - quitcost).ToString("0.00");
            this.lblCA.Text = (cacost).ToString("0.00");
            this.lblPOS.Text = poscost.ToString("0.00");
            if (totcost >= 0)
            {
                this.lblUper.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(totcost);
            }
            else
            {
                this.lblUper.Text ="负"+ FS.FrameWork.Public.String.LowerMoneyToUpper(totcost);
            }
            #region 日结实体赋值

            prepayBalance = new SOC.Fee.DayBalance.Object.PrepayDayBalance();
            prepayBalance.BeginDate = this.dtBegein.ToString();
            prepayBalance.EndDate = this.dtEnd.ToString();
            prepayBalance.BeginInvoice = beginINvoice;
            prepayBalance.EndInvoice = endInvoice;
            prepayBalance.PrepayNum = invoiceCount;
            prepayBalance.RealCost = totcost;
            prepayBalance.QuitCost = quitcost;
            prepayBalance.TotCost = totcost - quitcost;
            prepayBalance.CACost = cacost;
            prepayBalance.POSCost = poscost;
            prepayBalance.CHCost = 0;
            prepayBalance.ORCost = 0;
            prepayBalance.FGCost = 0;
            prepayBalance.CheckFlag = "0";
            #endregion
        }

        private void Save()
        {
            if(!isSave)
            {
                MessageBox.Show("历史日结数据不能保存，只能打印");
                return;
            }
            if (MessageBox.Show("是否进行日结,日结后数据将不能恢复?", "门诊收款员缴款日报", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            if (this.prepayBalance == null)
            {
                MessageBox.Show("请先查询日结数据 ！");
                return;
            }

           
            //和门诊收费用一个
            int rtn = 0;
            
            //判断如果总计为0的话，提示是否日结
            if (prepayBalance.TotCost == 0&&prepayBalance.QuitCost==0)
            {
                if (MessageBox.Show("日结总金额为0，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }                 

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            rtn = PrepayMgr.InsertPrepayStat(prepayBalance);
            if (rtn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("出错");
                return;
            }           

            FS.FrameWork.Management.PublicTrans.Commit();

            //this.Print();
            MessageBox.Show("日结成功！");

            if (this.isSavedPrint)
            {
                this.Print();
            }
            this.Init();
            this.Clear();
        }

        /// <summary>
        /// 双击左边日结时间显示当时日结数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
          
            int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
            if (rowCount < 0)
            {
                return;
            }
            isSave = false;
            string balanceNO = this.neuSpread1_Sheet1.Rows[rowCount].Tag.ToString();
           
            SOC.Fee.DayBalance.Object.PrepayDayBalance prepay =  this.PrepayMgr.GetPrepayDayBalance(balanceNO);
            if(prepay ==null)
            {
                MessageBox.Show("查询历史日结记录出错");
                return;
            }
           
            this.lblDateRange.Text =prepay.BeginDate.ToString() + "-" + prepay.EndDate.ToString();
           
            this.lblInvCount.Text = prepay.PrepayNum.ToString();
            this.lblInvoiceRand.Text = "流水号：" + prepay.BeginInvoice + "-" + prepay.EndInvoice;

            this.lblTOT.Text = prepay.RealCost.ToString("0.00");
            this.lblYJ.Text = prepay.TotCost.ToString("0.00");
            this.lblQF.Text = (-prepay.QuitCost).ToString("0.00");
          
            this.lblCA.Text = prepay.CACost.ToString("0.00");
            this.lblPOS.Text = prepay.POSCost.ToString("0.00");
            this.lblUper.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(prepay.RealCost);
        }

        private void Clear()
        {
            this.lblYJ.Text = "";
            this.lblQF.Text = "";
            this.lblTOT.Text = "";
            this.lblCA.Text = "";
            this.lblPOS.Text = "";
            this.lblUper.Text = "";
            this.lblDateRange.Text = "";
            this.lblInvCount.Text = "";
            this.lblInvoiceRand.Text = "";
        }

        protected void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            System.Drawing.Printing.PaperSize pagesize = new System.Drawing.Printing.PaperSize("RJ", 450, 400);
            print.SetPageSize(pagesize);
            print.PrintPage(0, 0, this.panel2);
        }

        private void CancelBalance()
        {
            if (MessageBox.Show("是否取消最近一次日结?", "住院预交金日报", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            if (PrepayMgr.DeletePrepayStat("", PrepayMgr.Operator.ID) < 0)
            {
                MessageBox.Show("取消日结失败！");
                return;
            }
            this.Init();
            this.Query();
        }


        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("重打", "重打历史记录", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolbarService.AddToolButton("取消日结", "只能取消上一次日结", FS.FrameWork.WinForms.Classes.EnumImageList.C错误单据, true, false, null);
                
            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "重打":
                    {
                        this.Print();
                        break;
                    }
                case "取消日结":
                    {
                        this.CancelBalance();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }
    }

    
}
