using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.GYZL.DayBalance
{
    public partial class ucAccDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAccDayBalance()
        {
            InitializeComponent();
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance accMgr = new FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant(); 

        /// <summary>
        /// 记录日结开始时间
        /// </summary>
        DateTime dtBegein = DateTime.MinValue;
        /// <summary>
        /// 记录日结结束时间
        /// </summary>
        DateTime dtEnd = DateTime.MinValue;
        bool isSave = true;//判断是否可以保存，点击左边的历史时间产生的数据不准保存

        private void Init()
        {
            //获取当前收费员上次日结时间和当前时间
            accMgr.GetAccBalanceDate(this.accMgr.Operator.ID, ref dtBegein, ref dtEnd);
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
            this.neuLblMakeTableName.Text = this.accMgr.Operator.Name;
            this.neuLabel1.Text = FS.FrameWork.Management.Connection.Hospital.Name + this.neuLabel1.Text;
        }
        private void Query()
        {
            isSave = true;
            if (this.neuDateTimePicker2.Value < this.neuDateTimePicker1.Value)
            {
                MessageBox.Show("开始时间大于结束时间。请调整！");
                return;
            }
            if (this.neuDateTimePicker2.Value > conMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("结束时间不能大于当前时间。请调整！");
                return;
            }
            //左边显示最近一个月日结记录
            DateTime dtBegein1 = new DateTime(this.neuDateTimePicker3.Value.Year, this.neuDateTimePicker3.Value.Month, 1, 0, 0, 0);//取当月的开始时间
            DateTime dtEnd1 = dtBegein1.AddMonths(1);//取当月的结束时间
            int intReturn = 0;
            DataSet ds = new DataSet();
            intReturn = accMgr.GetAccBalanceHistory(this.accMgr.Operator.ID, dtBegein1.ToString(), dtEnd1.ToString(), ref ds);
            if (intReturn == -1)
            {
                MessageBox.Show("查找数据失败");
                return;
            }
            else
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                DataTable dt= ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    int row = this.neuSpread1_Sheet1.RowCount - 1;
                    this.neuSpread1_Sheet1.Rows[row].Tag = dr[0].ToString();//结算序号
                    this.neuSpread1_Sheet1.SetValue(row, 0, dr[1].ToString(), false);//结算结束时间
                }
            }

            this.dtBegein = this.neuDateTimePicker1.Value;
            this.dtEnd = this.neuDateTimePicker2.Value;

            int invoiceCount = 0;
            string beginINvoice = "";
            string endInvoice = "";
            this.lblDateRange.Text = dtBegein.ToString() + "-" + dtEnd.ToString();
            this.accMgr.GetAccBalanceInvoice(this.accMgr.Operator.ID, dtBegein.ToString(),
                dtEnd.ToString(), "0", ref invoiceCount, ref beginINvoice, ref endInvoice);

            this.lblInvCount.Text = invoiceCount.ToString();
            this.lblInvoiceRand.Text ="流水号：" + beginINvoice + "-" + endInvoice;

            decimal totcost = 0; 
            decimal cacost = 0;
            decimal poscost = 0;
            decimal quitcost = 0;
            this.accMgr.GetAccBalanceCost(this.accMgr.Operator.ID, dtBegein.ToString(), dtEnd.ToString(), "0", out totcost, out cacost, out poscost);

            this.accMgr.GetAccQuitCost(this.accMgr.Operator.ID, dtBegein.ToString(), dtEnd.ToString(), out  quitcost);

            this.lblYJ.Text = totcost.ToString("0.00");
            this.lblQF.Text = (-quitcost).ToString("0.00");
            this.lblTOT.Text = (totcost + quitcost).ToString("0.00");
            this.lblCA.Text = (cacost+quitcost).ToString("0.00");
            this.lblPOS.Text = poscost.ToString("0.00");
            this.lblUper.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(totcost + quitcost);
        }

        private void Save()
        {
            if(!isSave)
            {
                MessageBox.Show("历史日结数据不能保存，只能打印");
                return;
            }

            // 不能刷新日结结束时间
            //this.neuDateTimePicker2.Value =conMgr.GetDateTimeFromSysDateTime();

            if (MessageBox.Show("是否进行日结,日结后数据将不能恢复?", "门诊收款员缴款日报", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //和门诊收费用一个
            int rtn = 0;
            decimal totCost = FS.FrameWork.Function.NConvert.ToDecimal(this.lblYJ.Text);
            decimal quitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.lblQF.Text);
            decimal leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.lblTOT.Text);
            //判断如果总计为0的话，提示是否日结
            if (leftCost == 0)
            {
                if (MessageBox.Show("日结总金额为0，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            decimal ca = FS.FrameWork.Function.NConvert.ToDecimal(this.lblCA.Text);
            decimal pos = FS.FrameWork.Function.NConvert.ToDecimal(this.lblPOS.Text);
            int invoiceCount = 0;
            string beginInvoice = "";
            string endInvoice = "";
            this.accMgr.GetAccBalanceInvoice(this.accMgr.Operator.ID, this.dtBegein.ToString(),
                this.dtEnd.ToString(),"0", ref invoiceCount, ref beginInvoice, ref endInvoice);
            string balanceNO = "";
            rtn = accMgr.GetBalanceSequence(ref balanceNO);

            rtn = accMgr.SaveBalance(balanceNO, this.accMgr.Operator.ID, this.dtBegein.ToString(), this.dtEnd.ToString(),
                leftCost, totCost, quitCost, ca, pos, "0", "", "", beginInvoice, endInvoice);

            if (rtn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("出错");
                return;
            }
            rtn = accMgr.UpdateBalanceFlag(this.accMgr.Operator.ID, this.dtBegein.ToString(),
                this.dtEnd.ToString(), balanceNO);
            if (rtn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("出错");
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.Print();
            this.Print();
            MessageBox.Show("日结成功！");
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
            isSave = false;
            int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
            string balanceNO = this.neuSpread1_Sheet1.Rows[rowCount].Tag.ToString();
            string beginTime = "";
            string endTime = "";
            this.accMgr.GetAccBalanceBeginAndEndTime(balanceNO, ref beginTime, ref endTime);
            int invoiceCount = 0;
            string beginINvoice = "";
            string endInvoice = "";
            this.lblDateRange.Text = beginTime + "-" + endTime;
            this.accMgr.GetAccBalanceInvoice(this.accMgr.Operator.ID, beginTime, endTime, "1", ref invoiceCount, ref beginINvoice, ref endInvoice);

            this.lblInvCount.Text = invoiceCount.ToString();
            this.lblInvoiceRand.Text = "流水号：" + beginINvoice + "-" + endInvoice;

            decimal totcost = 0;
            decimal cacost = 0;
            decimal poscost = 0;
            decimal quitcost = 0;
            this.accMgr.GetAccBalanceCost(this.accMgr.Operator.ID, beginTime, endTime, "1", out totcost, out cacost, out poscost);

            this.accMgr.GetAccQuitCost(this.accMgr.Operator.ID, beginTime, endTime, out  quitcost);

            this.lblYJ.Text = totcost.ToString("0.00");
            this.lblQF.Text = (-quitcost).ToString("0.00");
            this.lblTOT.Text = (totcost + quitcost).ToString("0.00");
            this.lblCA.Text = (cacost + quitcost).ToString("0.00");
            this.lblPOS.Text = poscost.ToString("0.00");
            this.lblUper.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(totcost + quitcost);
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

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("重打", "重打历史记录", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
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
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
           // this.Init();
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
