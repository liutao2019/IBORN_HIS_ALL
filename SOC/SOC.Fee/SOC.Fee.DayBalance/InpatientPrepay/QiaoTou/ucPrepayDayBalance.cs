using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Fee.DayBalance.InpatientPrepay.QiaoTou
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
            //this.neuLblMakeTableName.Text = this.PrepayMgr.Operator.Name;
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
            int invalidInvoicecount = 0;
            string beginInvalidInvoice = "";
            string endInvalidInvoice = "";
            string invalidInvoiceText = "";
            System.Collections.ArrayList invalidInvoiceNo = new System.Collections.ArrayList();
            this.lblDateRange.Text = dtBegein.ToString() + "-" + this.neuDateTimePicker2.Value.ToString();
            this.PrepayMgr.GetPrepayBalanceInvoice(this.PrepayMgr.Operator.ID, dtBegein.ToString(),
                this.dtEnd.ToString(), ref invoiceCount, ref beginINvoice, ref endInvoice);

            DataSet dsInvoiceUsed = new DataSet();
            if (this.PrepayMgr.GetPrepayBalanceInvoiceUsedNew(this.PrepayMgr.Operator.ID, this.dtBegein.ToString(), this.dtEnd.ToString(), ref dsInvoiceUsed) == -1)
            {
                MessageBox.Show("查询住院押金发票出错!");
                return;
            }

            //使用发票段
            DataView dvInvoice = dsInvoiceUsed.Tables[0].DefaultView;
            string strInvoiceUsed = this.GetInvoiceStartAndEnd(dvInvoice);


            this.PrepayMgr.GetPrepayBalanceInvalidInvoice(this.PrepayMgr.Operator.ID, dtBegein.ToString(), this.dtEnd.ToString(), ref invalidInvoiceNo);
            this.lblInvCount.Text = invoiceCount.ToString();
            int invalidCount = 0;
            foreach (string temp in invalidInvoiceNo)
            {
                invalidInvoiceText += temp;
                invalidInvoiceText += ";";
                invalidCount++;
            }

            //this.lblInvoiceRand.Text = "流水号：" + beginINvoice + "-" + endInvoice + "\n" + "作废票据：" + invalidInvoiceText.ToString();//beginInvalidInvoice+"-"+endInvalidInvoice;
            this.lblInvoiceRand.Text = "流水号[" + dvInvoice.Count + "]：" + strInvoiceUsed + "\n作废票据[" + invalidCount + "]：" + invalidInvoiceText.ToString();



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
            this.lblTA.Text = orcost.ToString("0.00");   //桥头医院作为银行转账
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
            prepayBalance.ORCost = orcost;
            prepayBalance.FGCost = 0;
            prepayBalance.CheckFlag = "0";
            #endregion
        }

        /// <summary>
        /// 获取发票段
        /// </summary>
        /// <param name="dv"></param>
        /// <returns></returns>
        private string GetInvoiceStartAndEnd(DataView dv)
        {
            if (dv != null && dv.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                int count = dv.Count - 1;
                string minStr = GetPrintInvoiceno(dv[0]);
                string maxStr = GetPrintInvoiceno(dv[0]);
                for (int i = 0; i < count - 1; i++)
                    for (int j = i + 1; j < count; j++)
                    {
                        long froInt = Convert.ToInt64(GetPrintInvoiceno(dv[i]));
                        long nxtInt = Convert.ToInt64(GetPrintInvoiceno(dv[j]));
                        long chaInt = nxtInt - froInt;
                        if (chaInt > 1)
                        {
                            maxStr = GetPrintInvoiceno(dv[i]);
                            if (maxStr.Equals(minStr))
                            {
                                sb.Append(minStr + "，");
                            }
                            else
                            {
                                sb.Append(minStr + "至" + maxStr + "，");
                            }
                            minStr = GetPrintInvoiceno(dv[j]);

                            if (j == count - 1)
                            {
                                long fTemp = System.Convert.ToInt64(GetPrintInvoiceno(dv[j]));
                                long nTemp = System.Convert.ToInt64(GetPrintInvoiceno(dv[count]));
                                long cTemp = nTemp - fTemp;
                                if (cTemp > 1)
                                {
                                    sb.Append(minStr + ", ");
                                    minStr = GetPrintInvoiceno(dv[count]);
                                }
                            }


                            break;
                        }
                        else
                        {
                            if (j == count - 1)
                            {
                                long fTemp = System.Convert.ToInt64(GetPrintInvoiceno(dv[j]));
                                long nTemp = System.Convert.ToInt64(GetPrintInvoiceno(dv[count]));
                                long cTemp = nTemp - fTemp;
                                if (cTemp > 1)
                                {
                                    maxStr = GetPrintInvoiceno(dv[j]);
                                    if (maxStr.Equals(minStr))
                                    {
                                        sb.Append(minStr + ", ");
                                    }
                                    else
                                    {
                                        sb.Append(minStr + "至" + maxStr + ", ");
                                    }

                                    minStr = GetPrintInvoiceno(dv[count]);

                                }
                            }
                            break;
                        }

                    }

                maxStr = GetPrintInvoiceno(dv[count]);

                if (maxStr.Equals(minStr))
                {
                    sb.Append(maxStr);
                }
                else
                {
                    sb.Append(minStr + "至" + maxStr);
                }
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        private string GetPrintInvoiceno(DataRowView dv)
        {
            string str = dv[0].ToString();
            str = str.TrimStart(new char[] { '0' });
            str = str.PadLeft(14, '0');

            return str;
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
            DialogResult dr = MessageBox.Show("日结成功！是否打印?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
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
            //this.lblInvoiceRand.Text = "流水号：" + prepay.BeginInvoice + "-" + prepay.EndInvoice;

            this.lblTOT.Text = prepay.RealCost.ToString("0.00");
            this.lblYJ.Text = prepay.TotCost.ToString("0.00");
            this.lblQF.Text = (-prepay.QuitCost).ToString("0.00");
          
            this.lblCA.Text = prepay.CACost.ToString("0.00");
            this.lblPOS.Text = prepay.POSCost.ToString("0.00");
            this.lblUper.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(prepay.RealCost);
            this.lblTA.Text = prepay.ORCost.ToString("0.00"); //桥头医院作为银行转账

            //显示使用的发票号
            DataSet dsInvoiceUsed = new DataSet();
            if (this.PrepayMgr.GetPrepayBalanceInvoiceUsedNew(this.PrepayMgr.Operator.ID, prepay.BeginDate.ToString(), prepay.EndDate.ToString(), ref dsInvoiceUsed) == -1)
            {
                MessageBox.Show("查询住院押金发票出错!");
                return;
            }

            //使用发票段
            DataView dvInvoice = dsInvoiceUsed.Tables[0].DefaultView;
            string strInvoiceUsed = this.GetInvoiceStartAndEnd(dvInvoice);

            //显示作废的票据号
            System.Collections.ArrayList listInvalidInvoice = new System.Collections.ArrayList();
            string invalidInvoiceText = "";
            if (this.PrepayMgr.GetPrepayBalanceInvalidInvoice(this.PrepayMgr.Operator.ID, prepay.BeginDate.ToString(), prepay.EndDate.ToString(), ref listInvalidInvoice) == -1)
            {
                MessageBox.Show("查询作废发票出错!"+this.PrepayMgr.Err);
                return;
            }

            int invalidCount = 0;
            if (listInvalidInvoice != null && listInvalidInvoice.Count > 0)
            {
                foreach (string invoiceTemp in listInvalidInvoice)
                {
                    invalidInvoiceText += invoiceTemp + ";";
                    invalidCount++;
                }
            }
            this.lblInvoiceRand.Text = "流水号[" + dvInvoice.Count + "]：" + strInvoiceUsed + "\n作废票据[" + invalidCount + "]：" + invalidInvoiceText.ToString();

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
            this.lblTA.Text = "";
        }

        protected void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
            //>>打印纸张设置{9FB32BAB-E792-4bcf-8B52-5B0DAE1EC392}
            FS.HISFC.Models.Base.PageSize ps = null;
            ps = psManager.GetPageSize("ZYYJJRJ");//住院预交金日结

            if (ps == null)
            {
                ps = new FS.HISFC.Models.Base.PageSize("ZYYJJRJ", 450, 700);
                ps.Top = 0;
                ps.Left = 0;

            }
            //System.Drawing.Printing.PaperSize pagesize = new System.Drawing.Printing.PaperSize("RJ", 450, 450);
            //<<
            print.SetPageSize(ps);
            print.PrintPage(ps.Left , ps.Top, this.panel2);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("重打", "重打历史记录", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolbarService.AddToolButton("日结", "日结", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "重打":
                    {
                        if (this.isSave)
                        {
                            MessageBox.Show("必须是日结之后才能使用重打功能!");
                            return;
                        }
                        this.Print();
                        break;
                    }
                case "日结":
                    this.Save();
                    break;
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
