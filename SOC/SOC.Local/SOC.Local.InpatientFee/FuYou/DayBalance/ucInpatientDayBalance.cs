using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using System.Collections;
using FS.FrameWork.Function;
using FS.SOC.Local.InpatientFee.FuYou.Models;

namespace FS.SOC.Local.InpatientFee.FuYou.DayBalance
{
    /// <summary>
    /// 住院发票日结
    /// </summary>
    public partial class ucInpatientDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatientDayBalance()
        {
            InitializeComponent();
        }

        #region 变量定义

        /// <summary>
        /// 当前操作员
        /// </summary>
        NeuObject currentOperator = new NeuObject();
        /// <summary>
        /// 日结操作时间
        /// </summary>
        string operateDate = "";
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();


        /// <summary>
        /// 上次日结时间
        /// </summary>
        public string lastDate = "";

        /// <summary>
        /// 本次日结时间
        /// </summary>
        string dayBalanceDate = "";

        /// <summary>
        /// 要日结的数据
        /// </summary>
        public List<InpatientDayBalance> alData = new List<InpatientDayBalance>();

        /// <summary>
        /// 结算日结实体
        /// </summary>
        private InpatientDayBalance dayBalance = null;

        /// <summary>
        /// 预交金日结实体
        /// </summary>
        private PrepayDayBalance prepayBalance = null;

        /// <summary>
        /// 退款金额是否统计预交金支付
        /// </summary>
        private string isCountPrepayPay = "0";//‘0’表示不统计，‘1’表示统计

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

        #region 新增

        /// <summary>
        /// 起始时间
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// 终止时间
        /// </summary>
        private DateTime endDate = DateTime.MinValue;
        /// <summary>
        /// 日结业务层
        /// </summary>
        Function.InpatientDayBalanceManage inpatientDayBalanceManage = new Function.InpatientDayBalanceManage();

        #endregion

        #region 报表标题

        /// <summary>
        /// 报表标题
        /// </summary>
        private string reportTitle = "";

        /// <summary>
        /// 报表标题
        /// </summary>
        [Description("报表标题"), Category("报表设置")]
        public string ReportTitle
        {
            get
            {
                return reportTitle;
            }
            set
            {
                reportTitle = value;
            }
        }

        #endregion


        /// <summary>
        /// 全院日结还是单人日结，全院日结不分收费员日结
        /// </summary>
        private string isAll = "1";//‘0’表示全院日结，‘1’表示单个收费员日结
        /// <summary>
        /// 全院日结人
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        private FS.HISFC.Models.Base.Employee empBalance = null;
        /// <summary>
        /// 全院日结还是单人日结‘0’表示全院日结，‘1’表示单个收费员日结
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        [Description("全院日结还是单人日结，‘0’表示全院日结，‘1’表示单个收费员日结"), Category("设置")]
        public string IsShow
        {
            get
            {
                return this.isAll;
            }
            set
            {
                this.isAll = value;
                if (value == "0")
                {
                    empBalance = new FS.HISFC.Models.Base.Employee();
                    empBalance.ID = "T00001";
                    empBalance.Name = "T-全院";
                }
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitUC()
        {
            try
            {
                // 返回值
                int intReturn = 0;

                // 获取当前操作员
                currentOperator = this.inpatientDayBalanceManage.Operator;

                // 获取最近一次日结时间
                string strLastDayBalanceDate = string.Empty;
                string strCurrentDate = string.Empty;
                intReturn = this.inpatientDayBalanceManage.GetLastDayBalanceDate(currentOperator.ID, out strLastDayBalanceDate, out strCurrentDate);
                if (intReturn == -1)
                {
                    MessageBox.Show("获取上次日结时间失败！不能进行日结操作！");
                    return;
                }

                if (string.IsNullOrEmpty(strLastDayBalanceDate))
                {
                    this.ucInpatientDayBalanceDateControl1.dtLastBalance = DateTime.MinValue;
                    this.lastDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                    this.ucInpatientDayBalanceDateControl1.tbLastDate.Text = this.lastDate;

                }
                else
                {
                    this.ucInpatientDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(strLastDayBalanceDate);
                    this.ucInpatientDayBalanceDateControl1.tbLastDate.Text = strLastDayBalanceDate;
                    this.lastDate = strLastDayBalanceDate;
                }
                this.ucInpatientDayBalanceDateControl1.dtpBalanceDate.Value = NConvert.ToDateTime(strCurrentDate);

                // 初始化子控件的变量
                this.ucInpatientDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(this.lastDate);
                this.ucInpatientDayBalanceReportNew1.InitUC(this.inpatientDayBalanceManage.Hospital.Name + reportTitle);
                this.ucInpatientDayBalanceReportNew2.InitUC(inpatientDayBalanceManage.Hospital.Name + reportTitle);
                this.ucInpatientDayBalanceReportNew1.SetDetailName();

                // 初始化费用清单页面
                this.lblTitle.Text = this.inpatientDayBalanceManage.Hospital.Name + "收费清单";
                this.lblInvoiceInfo.Text = "";
                this.lblSumary.Text = "汇总号：";
                this.lblOper.Text = "收费员：" + currentOperator.Name;

                this.neuSpread1_Sheet1.RowCount = 0;
                this.neuSpread1_Sheet1.RowHeader.Visible = false;
                this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region 查询要日结的数据

        /// <summary>
        /// 查询要日结的数据
        /// </summary>
        private void QueryDayBalanceData()
        {
            this.alData.Clear();
            int intReturn = 0;

            intReturn = this.ucInpatientDayBalanceDateControl1.GetBalanceDate(ref dayBalanceDate);
            if (intReturn == -1)
            {
                return;
            }
            //显示报表信息
            this.ucInpatientDayBalanceReportNew1.Clear(lastDate, dayBalanceDate);
            string strEmpID = this.currentOperator.ID;

            DataTable dtCostStat = null;
            this.inpatientDayBalanceManage.QueryDayBalanceCostByStat(strEmpID, this.lastDate, dayBalanceDate, out dtCostStat);

            this.SetDetial(dtCostStat);

            //设置farpoint格式
            this.ucInpatientDayBalanceReportNew1.SetFarPoint();

            //显示发票汇总数据
            SetInvoice(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);

            //显示发票起止号，张数相关信息
            DataTable dtInvoice = null;
            this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.lastDate, dayBalanceDate, out dtInvoice);
            SetInvoiceBeginAndEnd(dtInvoice, this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);

            //显示金额数据
            this.SetMoneyValue(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);
        }

        #endregion

        #region 设置要日结Farpoint数据

        /// <summary>
        /// 设置显示项目数据
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetDetial(DataTable table)
        {
            if (table.Rows.Count == 0) return;
            //清除数据
            if (ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count > 0)
            {
                //先清空数据，然后初始化3行
                ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = 0;
                ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = 4;
                ucInpatientDayBalanceReportNew1.SetDetailName();
            }
            //日结明细显示和发票显示一致。
            //

            //显示项目数据
            int sortID = 0;
            decimal countMoney = 0;
            decimal decTemp = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                sortID = FS.FrameWork.Function.NConvert.ToInt32(table.Rows[i][3]);
                //int rowIndex = Convert.ToInt32(sortID % 3);
                //int colIndex = Convert.ToInt32(sortID/3)*2;
                int rowIndex = Convert.ToInt32(Math.Ceiling(sortID / 4.0)) - 1;
                int colIndex = (sortID % 4) * 2 == 0 ? 7 : (sortID % 4) * 2 - 1;

                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][2]);
                countMoney += decTemp;

                //decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);
                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text);

                //ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");
                ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text = decTemp.ToString("0.00");
            }
            ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[3, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);

        }


        /// <summary>
        /// 设置显示已日结项目数据
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetRePrintDetial(DataTable table)
        {
            if (table.Rows.Count == 0) return;
            //清除数据
            if (ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count > 0)
            {
                //先清空数据，然后初始化3行
                ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count = 0;
                ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count = 4;
                ucInpatientDayBalanceReportNew2.SetDetailName();
            }
            //日结明细显示和发票显示一致。
            //

            //显示项目数据
            int sortID = 0;
            decimal countMoney = 0;
            decimal decTemp = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                sortID = FS.FrameWork.Function.NConvert.ToInt32(table.Rows[i][3]);
                //int rowIndex = Convert.ToInt32(sortID % 3);
                //int colIndex = Convert.ToInt32(sortID / 3) * 2;
                int rowIndex = Convert.ToInt32(Math.Ceiling(sortID / 4.0)) - 1;
                int colIndex = (sortID % 4) * 2 == 0 ? 7 : (sortID % 4) * 2 - 1;

                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][2]);
                countMoney += decTemp;

                //decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);
                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text);

                //ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");
                ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text = decTemp.ToString("0.00");
            }
            ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[3, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);

        }

        /// <summary>
        /// 添加日结实体到列表
        /// </summary>
        private void AddDayBalanceToArray()
        {
            dayBalance.BeginTime = NConvert.ToDateTime(this.lastDate);
            dayBalance.EndTime = NConvert.ToDateTime(this.dayBalanceDate);

            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            if (this.isAll == "0")
            {
                dayBalance.Oper.ID = this.empBalance.ID;
                dayBalance.Oper.Name = this.empBalance.Name;
            }
            else
            {
                dayBalance.Oper.ID = currentOperator.ID;
                dayBalance.Oper.Name = currentOperator.Name;
            }
            this.alData.Add(dayBalance);
        }

        /// <summary>
        /// 查询并显示发票数据 -- 
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoice(FarPoint.Win.Spread.SheetView sheet)
        {
            #region 结算数据
            //获取发票数据
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }

            string effectiveBill = string.Empty;
            string uneffectiveBill = string.Empty;
            //有效数
            int resultValue = this.inpatientDayBalanceManage.QueryDayBalanceEffectiveBill(strEmpID, this.lastDate, dayBalanceDate, out effectiveBill);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            this.SetOneCellText(sheet, EnumCellName.A002, effectiveBill);

            //作废数
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceUneffectiveBill(strEmpID, this.lastDate, dayBalanceDate, out uneffectiveBill);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            this.SetOneCellText(sheet, EnumCellName.A003, uneffectiveBill);

            //作废总金额
            decimal quitTotCost = 0;
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceUneffectiveBillMoney(strEmpID, this.lastDate, dayBalanceDate, out quitTotCost);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A004, quitTotCost.ToString("0.00"));

            //显示使用票据号
            DataSet ds1 = new DataSet();
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceEffectiveInvoiceNo(strEmpID, this.lastDate, dayBalanceDate, ref ds1);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                this.SetOneCellText(sheet, EnumCellName.A005, GetInvoiceStartAndEnd(ds1.Tables[0]));
                FarPoint.Win.Spread.Cell cell1 = sheet.GetCellFromTag(null, EnumCellName.A005);
                string[] sTemp = cell1.Text.Split('～', '，');
                cell1.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;
            }
            //退费、作废票据号
            DataSet ds2 = new DataSet();
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceUneffectiveInvoiceNo(strEmpID, this.lastDate, dayBalanceDate, ref ds2);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                string InvoiceStr = GetInvoiceStr(ds2.Tables[0].DefaultView);
                this.SetOneCellText(sheet, EnumCellName.A006, InvoiceStr);
                FarPoint.Win.Spread.Cell cell2 = sheet.GetCellFromTag(null, EnumCellName.A006);
                string[] sTemp = cell2.Text.Split('|');
                cell2.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;
            }
            #endregion

            #region 预交数据
            string beginInvoice = "";
            string endInvoice = "";
            int invoiceCount = 0;
            int invoiceQuitCount = 0;
            decimal quitCost = 0;
            //押金有效数
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceInvoiceCount(strEmpID, this.lastDate, dayBalanceDate, ref invoiceCount, ref beginInvoice, ref endInvoice);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A014, invoiceCount.ToString("0"));
            //押金作废数
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceQuitInvoiceCount(strEmpID, this.lastDate, dayBalanceDate, out invoiceQuitCount);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A015, invoiceQuitCount.ToString("0"));
            //作废金额
            resultValue = this.inpatientDayBalanceManage.GetPrepayQuitCost(strEmpID, this.lastDate, dayBalanceDate, out quitCost);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A016, quitCost.ToString("0.00"));
            //显示使用票据号
            DataSet ds3 = new DataSet();
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceInvoiceNo(strEmpID, this.lastDate, dayBalanceDate, ref ds3);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                this.SetOneCellText(sheet, EnumCellName.A017, GetInvoiceStartAndEnd(ds3.Tables[0]));
                FarPoint.Win.Spread.Cell cell1 = sheet.GetCellFromTag(null, EnumCellName.A017);
                string[] sTemp = cell1.Text.Split('～', '，');
                cell1.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;
            }
            //退费、作废票据号
            DataSet ds4 = new DataSet();
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceQuitInvoiceNo(strEmpID, this.lastDate, dayBalanceDate, ref ds4);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                string InvoiceStr = GetInvoiceStr(ds4.Tables[0].DefaultView);
                this.SetOneCellText(sheet, EnumCellName.A018, InvoiceStr);
                FarPoint.Win.Spread.Cell cell2 = sheet.GetCellFromTag(null, EnumCellName.A018);
                string[] sTemp = cell2.Text.Split('|');
                cell2.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;
            }
            //预交金信息
            decimal totCost = 0;
            decimal cash = 0;
            decimal pos = 0;
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceCost(strEmpID, this.lastDate, dayBalanceDate, out totCost, out cash, out pos);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            if (isCountPrepayPay == "1")
            {
                decimal cacost1 = 0;
                decimal poscost1 = 0;
                decimal chcost1 = 0;
                decimal orcost1 = 0;
                decimal fgcost1 = 0;
                decimal prepayPay = 0;
                this.inpatientDayBalanceManage.GetPrepayPayCost(strEmpID, this.lastDate, dayBalanceDate, out prepayPay, out cacost1, out poscost1, out chcost1, out orcost1, out fgcost1);
                quitCost = quitCost - prepayPay;
                totCost = totCost - prepayPay;
                cash = cash - cacost1;
                pos = pos - poscost1;
            }
            this.SetOneCellText(sheet, EnumCellName.A019, totCost.ToString("0.00"));
            this.SetOneCellText(sheet, EnumCellName.A020, cash.ToString("0.00"));
            this.SetOneCellText(sheet, EnumCellName.A021, pos.ToString("0.00"));

            #region 日结实体赋值
            prepayBalance = new PrepayDayBalance();
            prepayBalance.BeginDate = this.lastDate;
            prepayBalance.EndDate = dayBalanceDate;
            prepayBalance.BeginInvoice = beginInvoice;
            prepayBalance.EndInvoice = endInvoice;
            prepayBalance.PrepayNum = invoiceCount;
            prepayBalance.RealCost = totCost;
            prepayBalance.QuitCost = quitCost;
            prepayBalance.TotCost = totCost - quitCost;
            prepayBalance.CACost = cash;
            prepayBalance.POSCost = pos;
            prepayBalance.CHCost = 0;
            prepayBalance.ORCost = 0;
            prepayBalance.FGCost = 0;
            prepayBalance.CheckFlag = "0";
            #endregion

            #endregion
            return;

            #region 没用

            ////起止票据号
            ////this.SetOneCellText(sheet, "A00101", GetInvoiceStartAndEnd(ds.Tables[0]));

            ////总票据数
            //dv.RowFilter = "trans_type='1'";
            //this.SetOneCellText(sheet, EnumCellName.A002, dv.Count.ToString());

            ////退费票据号 
            //string InvoiceStr = GetInvoiceStr(dv);
            ////aaaaaaaaaaaaaaaaa
            ////this.SetOneCellText(sheet, EnumCellName."A00402", InvoiceStr);

            ////作废票据号
            //InvoiceStr = GetInvoiceStr(dv);
            ////aaaaaaaaaaaaaaaa
            ////this.SetOneCellText(sheet, "A00502", InvoiceStr);

            ////退费票据
            //dv.RowFilter = "cancel_flag='0' and trans_type='2'";
            //this.SetOneCellText(sheet, EnumCellName.A003, dv.Count.ToString());

            ////作废票据
            //dv.RowFilter = "cancel_flag in ('2','3') and trans_type='2'";
            ////aaaaaaaaaaaaaaaaaa
            ////this.SetOneCellText(sheet, "A00501", dv.Count.ToString());
            #endregion
        }

        /// <summary>
        /// 显示发票起止号，张数相关信息
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoiceBeginAndEnd(DataTable dtInvoice, FarPoint.Win.Spread.SheetView sheet)
        {
            this.ucInpatientDayBalanceReportNew1.lblinvoiceInfo.Text = "";

            if (dtInvoice == null || dtInvoice.Rows.Count <= 0)
            {
                return;
            }

            int iRowCount = dtInvoice.Rows.Count;

            int idx = 0;
            string strTemp = "";
            DataRow drTemp = null;

            List<string> lstInvoice = new List<string>();

            for (idx = 0; idx < iRowCount; idx++)
            {
                drTemp = dtInvoice.Rows[idx];

                strTemp = drTemp["print_invoiceno"].ToString().TrimStart(new char[] { '0' });
                if (!lstInvoice.Contains(strTemp))
                {
                    lstInvoice.Add(strTemp);
                }

            }
            if (sheet == this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1)
            {
                this.ucInpatientDayBalanceReportNew1.lblinvoiceInfo.Text = "发票数：     " + lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1];
            }
            else
            {
                this.ucInpatientDayBalanceReportNew2.lblinvoiceInfo.Text = "发票数：     " + lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1];
            }

        }

        /// <summary>
        /// 获得作废、退费票据号
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <param name="aMod">作废还是退费1是作废 0是退费</param>
        /// <returns></returns>
        private string GetInvoiceStr(DataView dv)
        {
            StringBuilder sb = new StringBuilder();
            if (dv.Count == 0)
            {
                sb.Append("无");
            }
            else
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    sb.Append(dv[i][0].ToString() + "|");

                }
            }
            return sb.ToString();
        }

        #region 获得起始、终止票据号

        private string GetInvoiceStartAndEnd(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            int count = dt.Rows.Count - 1;
            string minStr = dt.Rows[0][0].ToString();
            string maxStr = dt.Rows[0][0].ToString();
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    long froInt = Convert.ToInt64(dt.Rows[i][0].ToString());
                    long nxtInt = Convert.ToInt64(dt.Rows[j][0].ToString());
                    long chaInt = nxtInt - froInt;
                    if (chaInt > 1)
                    {
                        maxStr = dt.Rows[i][0].ToString();
                        if (maxStr.Equals(minStr))
                        {
                            sb.Append(minStr + ",");
                        }
                        else
                        {
                            sb.Append(minStr + "～" + maxStr + ",");
                        }
                        minStr = dt.Rows[j][0].ToString();
                        break;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            maxStr = dt.Rows[count][0].ToString();
            sb.Append(minStr + "～" + maxStr);
            return sb.ToString();

        }

        #endregion

        /// <summary>
        /// 设置显示金额数据
        /// </summary>
        protected virtual void SetMoneyValue(FarPoint.Win.Spread.SheetView sheet)
        {
            int resultValue;

            string employeeID = this.currentOperator.ID;
            DataTable dtBillMoney = null;
            ArrayList al = new ArrayList();

            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceBillMoney(employeeID, this.lastDate, dayBalanceDate, out dtBillMoney);

            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceCACDMoney(employeeID, this.lastDate, dayBalanceDate, ref al);

            SetPayTypeValue(sheet, dtBillMoney, al);

            return;

        }

        /// <summary>
        /// 按类型显示金额
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtBillMoney"></param>
        protected virtual void SetPayTypeValue(FarPoint.Win.Spread.SheetView sheet, DataTable dtBillMoney, ArrayList al)
        {
            // 顺德社保
            decimal decSDSB = 0;
            // 优惠优待
            decimal decYHYD = 0;
            // 本院减免
            decimal decBYJM = 0;
            // 特约单位
            decimal decTYDW = 0;
            // 预交金结算
            decimal decYJJJS = 0;
            //上缴金额
            decimal decSJJE = 0;
            //上缴卡数
            decimal decSJKS = 0;
            //现金金额
            decimal decCA = 0;
            //刷卡金额
            decimal decCD = 0;

            // 职工医保
            string strPact = "5";
            decSDSB = this.ComputeByPact(strPact, "Sum(pubcost)", dtBillMoney);
            // 老年减免
            // 合作医疗
            // 优惠优待
            // 暂不处理

            // 预交金结算
            decYJJJS = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(prepaycost)", ""));

            // 本院减免
            decBYJM = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(dercost)", ""));

            //decCA = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(supplycost)", "")) - FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(returncost)", ""));

            //decCA = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(cacost)", ""));

            //decCD = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(cdcost)", ""));

            decimal decCABalCost1 = 0;
            decimal decCDBalCost1 = 0;
            decimal decCAPreCost1 = 0;
            decimal decCDPreCost1 = 0;
            decimal decCABalCost2 = 0;
            decimal decCDBalCost2 = 0;
            decimal decCAPreCost2 = 0;
            decimal decCDPreCost2 = 0;

            foreach(FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.User01 == "1")//补收
                {
                    if (obj.Memo == "0")//预交款
                    {
                        decCAPreCost1 = FS.FrameWork.Function.NConvert.ToDecimal(obj.ID);
                        decCDPreCost1 = FS.FrameWork.Function.NConvert.ToDecimal(obj.Name);
                    }
                    else//结算款
                    {
                        decCABalCost1 = FS.FrameWork.Function.NConvert.ToDecimal(obj.ID);
                        decCDBalCost1 = FS.FrameWork.Function.NConvert.ToDecimal(obj.Name);
                    }
                }
                else//返还
                {
                    if (obj.Memo == "0")//预交款
                    {
                        decCAPreCost2 = FS.FrameWork.Function.NConvert.ToDecimal(obj.ID);
                        decCDPreCost2 = FS.FrameWork.Function.NConvert.ToDecimal(obj.Name);
                    }
                    else//结算款
                    {
                        decCABalCost2 = FS.FrameWork.Function.NConvert.ToDecimal(obj.ID);
                        decCDBalCost2 = FS.FrameWork.Function.NConvert.ToDecimal(obj.Name);
                    }
                }
            }

            decCA = decCAPreCost1 + decCABalCost1 + decCAPreCost2 - decCABalCost2;
            decCD = decCDPreCost1 + decCDBalCost1 + decCDPreCost2 - decCDBalCost2;

            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            //如果是日结赋值在日结界面，如果是查询重打则赋值在查询重打界面
            //string strTemp = string.Empty;
            if (sheet == this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1)
            {
                decSJJE = decCA + FS.FrameWork.Function.NConvert.ToDecimal(this.GetOneCellText(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1, EnumCellName.A020)) - decYJJJS;
                decSJKS = decCD + FS.FrameWork.Function.NConvert.ToDecimal(this.GetOneCellText(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1, EnumCellName.A021));
                //strTemp = FS.FrameWork.Public.String.LowerMoneyToUpper(decSJJE);
                //if (decSJJE < 0)
                //{
                //    strTemp = "负" + strTemp;
                //}
                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows[9].Border = bevelBorder1;

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[4, 1].Text = decSJJE.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[4, 3].Text = decSJKS.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 1].Text = decSDSB.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 3].Text = decYHYD.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 5].Text = decBYJM.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 7].Text = decTYDW.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 1].Text = decCA.ToString("0.00"); //decYJJJS.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 3].Text = decCD.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 5].Text = decYJJJS.ToString("0.00");

                //this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows[4].Visible = false;
            }
            else
            {
                decSJJE = decCA + FS.FrameWork.Function.NConvert.ToDecimal(this.GetOneCellText(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1, EnumCellName.A020)) - decYJJJS;
                decSJKS = decCD + FS.FrameWork.Function.NConvert.ToDecimal(this.GetOneCellText(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1, EnumCellName.A021));
                //strTemp = FS.FrameWork.Public.String.LowerMoneyToUpper(decSJJE);
                //if (decSJJE < 0)
                //{
                //    strTemp = "负 " + strTemp;
                //}
                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows[9].Border = bevelBorder1;

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[4, 1].Text = decSJJE.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[4, 3].Text = decSJKS.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 1].Text = decSDSB.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 3].Text = decYHYD.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 5].Text = decBYJM.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 7].Text = decTYDW.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 1].Text = decCA.ToString("0.00"); //decYJJJS.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 3].Text = decCD.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 5].Text = decYJJJS.ToString("0.00");

                //this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows[4].Visible = false;
            }
        }
        /// <summary>
        /// 计算费用
        /// </summary>
        /// <param name="strPactList"></param>
        /// <param name="excepition"></param>
        /// <param name="dtBillMoney"></param>
        /// <returns></returns>
        private decimal ComputeByPact(string strPactList, string excepition, DataTable dtBillMoney)
        {
            string[] strPactArr = strPactList.Split(new char[] { '|' });

            string strPactFilter = "pact_code = '" + strPactArr[0] + "'";
            for (int i = 1; i < strPactArr.Length; i++)
            {
                strPactFilter += " or pact_code = '" + strPactArr[i] + "'";
            }

            return FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute(excepition, strPactFilter));
        }


        #endregion

        #region 查询已日结数据

        private void QueryDayBalanceRecord()
        {
            // 返回值
            int intReturn = 0;
            // 查询的起始时间
            DateTime dtFrom = DateTime.MinValue;
            // 查询的截止时间
            DateTime dtTo = DateTime.MinValue;
            // 返回的日志记录
            List<FS.FrameWork.Models.NeuObject> lstBalanceRecord = null;
            //预交金日结记录
            List<FS.FrameWork.Models.NeuObject> lstPrePayRecord = null;
            // 查询的日记流水号
            string sequence = "";

            // 获取查询时间
            intReturn = this.ucReprintDateControl1.GetInputDateTime(ref dtFrom, ref dtTo);
            if (intReturn == -1)
            {
                return;
            }

            intReturn = this.inpatientDayBalanceManage.GetBalanceRecord(this.currentOperator.ID, dtFrom.ToString("yyyy-MM-dd HH:mm:ss"), dtTo.ToString("yyyy-MM-dd HH:mm:ss"), out lstBalanceRecord);

            if (intReturn == -1)
            {
                MessageBox.Show("获取日志记录失败！");
                return;
            }

            intReturn = this.inpatientDayBalanceManage.GetPrepayBalanceHistory(this.currentOperator.ID, dtFrom.ToString("yyyy-MM-dd HH:mm:ss"), dtTo.ToString("yyyy-MM-dd HH:mm:ss"), out lstPrePayRecord);

            if (intReturn == -1)
            {
                MessageBox.Show("获取预交金日结记录失败！");
                return;
            }

            // 判断结果记录数，如果多条，那么弹出窗口让用户选择
            if (lstBalanceRecord == null || lstBalanceRecord.Count <= 0 || lstPrePayRecord == null || lstPrePayRecord.Count <= 0)
            {
                MessageBox.Show("该时间段内没有要查找的数据！");
                return;
            }

            for (int i = 0; i < lstBalanceRecord.Count; i++)
            {
                for (int j = 0; j < lstPrePayRecord.Count; j++)
                {
                    if (lstBalanceRecord[i].Name == lstPrePayRecord[j].Name
                        && lstBalanceRecord[i].Memo == lstPrePayRecord[j].Memo)
                    {
                        lstBalanceRecord[i].User02 = lstPrePayRecord[j].ID;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            for (int i = 0; i < lstBalanceRecord.Count; i++)
            {
                if (lstBalanceRecord[i].User02 == "")
                {
                    MessageBox.Show("获取预交金日结记录失败！");
                    //return;
                }
                else
                {
                    continue;
                }
            }

            string begin = string.Empty, end = string.Empty;
            
            if (lstBalanceRecord.Count > 1)
            {
                frmConfirmBalanceRecord confirmBalanceRecord = new frmConfirmBalanceRecord();
                confirmBalanceRecord.LstBalanceRecord = lstBalanceRecord;
                if (confirmBalanceRecord.ShowDialog() == DialogResult.OK)
                {
                    sequence = confirmBalanceRecord.neuSpread1.Sheets[0].Cells[confirmBalanceRecord.neuSpread1.Sheets[0].ActiveRowIndex, 0].Text;
                    begin = confirmBalanceRecord.neuSpread1.Sheets[0].Cells[confirmBalanceRecord.neuSpread1.Sheets[0].ActiveRowIndex, 1].Text;
                    end = confirmBalanceRecord.neuSpread1.Sheets[0].Cells[confirmBalanceRecord.neuSpread1.Sheets[0].ActiveRowIndex, 2].Text;
                }
                else
                {
                    return;
                }
            }
            else
            {
                sequence = lstBalanceRecord[0].ID;
                begin = lstBalanceRecord[0].Name;
                end = lstBalanceRecord[0].Memo;
            }

            //通过查询当时日结开始时间和结束时间来实现重打
            this.lastDate = begin.ToString();
            this.dayBalanceDate = end.ToString();
            //显示报表信息
            this.ucInpatientDayBalanceReportNew2.Clear(lastDate, dayBalanceDate);
            string strEmpID = this.currentOperator.ID;

            DataTable dtCostStat = null;
            this.inpatientDayBalanceManage.QueryDayBalanceCostByStat(strEmpID, this.lastDate, dayBalanceDate, out dtCostStat);

            this.SetRePrintDetial(dtCostStat);

            //设置farpoint格式
            this.ucInpatientDayBalanceReportNew2.SetFarPoint();

            //显示发票汇总数据
            SetInvoice(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

            // 显示金额数据
            this.SetMoneyValue(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

            // 处理收费清单
            DataTable dtInvoice = null;
            this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.lastDate, dayBalanceDate, out dtInvoice);
            this.SetInvoiceDetial(sequence, dtInvoice);
            SetInvoiceBeginAndEnd(dtInvoice, this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

        }

        #endregion

        #region 显示收费清单
        /// <summary>
        /// 显示收费清单
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <param name="dtInvoice"></param>
        private void SetInvoiceDetial(string balanceNO, DataTable dtInvoice)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            lblSumary.Text = "汇总号：  " + balanceNO;
            lblInvoiceInfo.Text = "";



            if (dtInvoice == null || dtInvoice.Rows.Count <= 0)
            {
                return;
            }

            int iRowCount = dtInvoice.Rows.Count;
            this.neuSpread1_Sheet1.RowCount = iRowCount + 1;

            int idx = 0;
            string strTemp = "";
            DataRow drTemp = null;

            List<string> lstInvoice = new List<string>();

            for (idx = 0; idx < iRowCount; idx++)
            {
                drTemp = dtInvoice.Rows[idx];

                strTemp = drTemp["print_invoiceno"].ToString().TrimStart(new char[] { '0' });
                if (!lstInvoice.Contains(strTemp))
                {
                    lstInvoice.Add(strTemp);
                }

                this.neuSpread1_Sheet1.Cells[idx, 0].Text = strTemp;
                this.neuSpread1_Sheet1.Cells[idx, 1].Text = drTemp["invoice_no"].ToString().Trim();
                this.neuSpread1_Sheet1.Cells[idx, 2].Text = drTemp["patient_no"].ToString().Trim();
                this.neuSpread1_Sheet1.Cells[idx, 3].Text = drTemp["name"].ToString().Trim();

                this.neuSpread1_Sheet1.Cells[idx, 4].Text = drTemp["balance_date"] != DBNull.Value ? FS.FrameWork.Function.NConvert.ToDateTime(drTemp["balance_date"]).ToString("yyyy.MM.dd") : "";
                this.neuSpread1_Sheet1.Cells[idx, 5].Text = drTemp["tot_cost"].ToString();
                this.neuSpread1_Sheet1.Cells[idx, 6].Text = drTemp["eco_cost"].ToString();
                this.neuSpread1_Sheet1.Cells[idx, 7].Text = drTemp["owncost"].ToString();
                this.neuSpread1_Sheet1.Cells[idx, 8].Text = drTemp["pub_cost"].ToString();
                this.neuSpread1_Sheet1.Cells[idx, 9].Text = drTemp["empl_name"].ToString().Trim();
                this.neuSpread1_Sheet1.Cells[idx, 10].Text = drTemp["trans_type"].ToString() == "1" ? "" : "作废";
            }
            // 显示汇总信息
            this.neuSpread1_Sheet1.Cells[idx, 4].Text = "合计：";
            this.neuSpread1_Sheet1.Cells[idx, 5].Text = dtInvoice.Compute("Sum(tot_cost)", "").ToString();
            this.neuSpread1_Sheet1.Cells[idx, 6].Text = dtInvoice.Compute("Sum(eco_cost)", "").ToString();
            this.neuSpread1_Sheet1.Cells[idx, 7].Text = dtInvoice.Compute("Sum(owncost)", "").ToString();
            this.neuSpread1_Sheet1.Cells[idx, 8].Text = dtInvoice.Compute("Sum(pub_cost)", "").ToString();

            //增加一些人员信息
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 11);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "会计:" + "                           " + "出纳:" + "                           " + "复合:" + "                           " + "收费员:" + currentOperator.Name;

            lblInvoiceInfo.Text = "发票数：     " + lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1];

        }

        #endregion

        #region 设置已日结Farpoint数据
        private void SetOldFarPointData(DataTable table)
        {
            FarPoint.Win.Spread.SheetView sheet = this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1;
            int rowCount = sheet.Rows.Count;
            if (sheet.Rows.Count > 0)
            {
                sheet.Rows.Remove(0, rowCount - 1);
            }
            DataView dv = table.DefaultView;
            //设置项目明细
            SetDetialed(sheet, dv);
            this.ucInpatientDayBalanceReportNew2.SetFarPoint();
            this.SetInvoiced(sheet, dv);
            this.SetMoneyed(sheet, dv);
        }

        /// <summary>
        /// 设置已日结发票信息
        /// </summary>
        /// <param name="sheet">FarPoint.Win.Spread.SheetView</param>
        /// <param name="dv">DataView</param>
        protected virtual void SetInvoiced(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            dv.RowFilter = "BALANCE_ITEM='5'";
            this.SetFarpointValue(sheet, dv);
        }

        protected virtual void SetMoneyed(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            dv.RowFilter = "BALANCE_ITEM='6'";
            this.SetFarpointValue(sheet, dv);
        }

        protected virtual void SetFarpointValue(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            //if (dv.Count > 0)
            //{
            //    string fieldStr = string.Empty;
            //    string tagStr = string.Empty;
            //    string field = string.Empty;
            //    int Index = 0;
            //    for (int k = 0; k < dv.Count; k++)
            //    {
            //        fieldStr = dv[k]["sort_id"].ToString();
            //        int index = fieldStr.IndexOf('、');
            //        if (index == -1)
            //        {
            //            Index = fieldStr.IndexOf("|");
            //            tagStr = fieldStr.Substring(0, Index);
            //            field = fieldStr.Substring(Index + 1);
            //            SetOneCellText(sheet, tagStr, dv[k][field].ToString());
            //            if (dv[k][1].ToString() == "A023")
            //            {
            //                SetOneCellText(sheet, "A1000", FS.FrameWork.Public.String.LowerMoneyToUpper(NConvert.ToDecimal(dv[k][field])));
            //            }
            //        }
            //        else
            //        {
            //            string[] aField = fieldStr.Split('、');
            //            if (aField.Length == 0) continue;
            //            foreach (string s in aField)
            //            {
            //                Index = s.IndexOf("|");
            //                tagStr = s.Substring(0, Index);
            //                field = s.Substring(Index + 1);
            //                SetOneCellText(sheet, tagStr, dv[k][field].ToString());
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 设置已日结项目明细
        /// </summary>
        /// <param name="sheet">FarPoint.Win.Spread.SheetView</param>
        /// <param name="dv">DataView</param>
        private void SetDetialed(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            #region 显示项目数据
            //项目数据
            dv.RowFilter = "BALANCE_ITEM='4'";
            int count = dv.Count;
            decimal countMoney = 0;
            if (count > 0)
            {
                if (count % 2 == 0)
                {
                    sheet.Rows.Count = Convert.ToInt32(count / 2);
                }
                else
                {
                    sheet.Rows.Count = Convert.ToInt32(count / 2) + 1;
                }

                //显示项目数据
                for (int i = 0; i < count; i++)
                {
                    int index = Convert.ToInt32(i / 2);
                    int intMod = (i + 1) % 2;
                    if (intMod > 0)
                    {
                        sheet.Models.Span.Add(index, 0, 1, 2);
                        sheet.Cells[index, 0].Text = dv[i]["extent_field1"].ToString();
                        sheet.Cells[index, 2].Text = dv[i]["tot_cost"].ToString();
                    }
                    else
                    {
                        sheet.Models.Span.Add(index, 3, 1, 2);
                        sheet.Cells[index, 3].Text = dv[i]["extent_field1"].ToString();
                        sheet.Cells[index, 5].Text = dv[i]["tot_cost"].ToString();
                    }
                    countMoney += Convert.ToDecimal(dv[i][0]);

                }
                if (count % 2 > 0)
                {
                    sheet.Models.Span.Add(sheet.Rows.Count - 1, 3, 1, 2);
                }
                //显示合计
                sheet.Rows.Count += 1;
                count = sheet.Rows.Count;
                sheet.Models.Span.Add(count - 1, 0, 1, 2);
                sheet.Cells[count - 1, 0].Text = "合计：";
                sheet.Models.Span.Add(count - 1, 2, 1, 4);
                sheet.Cells[count - 1, 2].Text = countMoney.ToString();
            }
            #endregion
        }
        #endregion

        #region 按tag读取FarPoint的cell数据

        /// <summary>
        /// 设置单个Cell的Text
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="tagStr">Cell的tag</param>
        /// <param name="strText">要显示的Text</param>
        private void SetOneCellText(FarPoint.Win.Spread.SheetView sheet, EnumCellName tagStr, string strText)
        {
            FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, tagStr);
            if (cell != null)
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.Multiline = true;
                t.WordWrap = true;
                cell.CellType = t;
                //把字符串转换成数字进行相加，成功后转回字符串
                try
                {
                    if (cell.Text == string.Empty || cell.Text == null)
                    {
                        cell.Text = "0";
                    }
                    if (strText == string.Empty || strText == null)
                    {
                        strText = "0";
                    }
                    decimal intText = (Convert.ToDecimal(cell.Text) + Convert.ToDecimal(strText));
                    cell.Text = intText.ToString();
                }
                //如果转换失败则把字符串相加
                catch
                {
                    if (cell.Text == "0")
                    {
                        cell.Text = "";
                    }
                    if (strText == "0")
                    {
                        strText = "";
                    }
                    cell.Text += strText;
                }
                //相加结果为零，变成空字符串
                if (cell.Text == "0")
                {
                    cell.Text = "";
                }
                //      cell.Text += strText;
            }
        }

        private string GetOneCellText(FarPoint.Win.Spread.SheetView sheet, EnumCellName tagStr)
        {
            FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, tagStr);
            if (cell != null)
                return cell.Text;
            return string.Empty;
        }
        #endregion

        #region 设置日结实体

        /// <summary>
        /// 获得日结实体
        /// </summary>
        private void SetDayBalanceData()
        {
            //FarPoint.Win.Spread.SheetView sheet = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1;
            //string strValue = string.Empty;

            //#region 起止发票号
            //dayBalance = new Models.ClinicDayBalanceNew();
            ////dayBalance.InvoiceNO.ID = "A001";
            ////dayBalance.InvoiceNO.Name = "起始结束票据号";
            ////strValue = GetOneCellText(sheet, "A00101");
            ////dayBalance.BegionInvoiceNO = strValue;
            ////strValue = GetOneCellText(sheet, "A00102");
            //dayBalance.EndInvoiceNo = strValue;
            ////设置Cell显示数据的Tag和字段名称
            //dayBalance.SortID = "A00101|EXTENT_FIELD2、A00102|EXTENT_FIELD3";
            //dayBalance.TypeStr = "5";
            //AddDayBalanceToArray();
            //#endregion

            //#region 票据总数
            //strValue = GetOneCellText(sheet, EnumCellName.A002);
            //this.SetOneCellDayBalance("A002", "票据总数", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region 有效票据
            //strValue = GetOneCellText(sheet, EnumCellName.A003);
            //this.SetOneCellDayBalance("A003", "有效票据", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region 退费票据
            //dayBalance = new Models.ClinicDayBalanceNew();
            //dayBalance.InvoiceNO.ID = "A004";
            //dayBalance.InvoiceNO.Name = "退费票据";
            ////票据数
            //strValue = this.GetOneCellText(sheet, "A00401");
            //dayBalance.TotCost = NConvert.ToDecimal(strValue);
            ////票据号
            //strValue = this.GetOneCellText(sheet, "A00402");
            //dayBalance.CancelInvoiceNo = strValue;
            //dayBalance.TypeStr = "5";
            //dayBalance.SortID = "A00401|TOT_COST、A00402|EXTENT_FIELD5";
            //AddDayBalanceToArray();
            //#endregion

            //#region 作废票据
            //dayBalance = new Models.ClinicDayBalanceNew();
            //dayBalance.InvoiceNO.ID = "A005";
            //dayBalance.InvoiceNO.Name = "作废票据";
            //strValue = this.GetOneCellText(sheet, "A00501");
            //dayBalance.TotCost = NConvert.ToDecimal(strValue);
            //strValue = this.GetOneCellText(sheet, "A00502");
            //dayBalance.FalseInvoiceNo = strValue;
            //dayBalance.TypeStr = "5";
            //dayBalance.SortID = "A00501|TOT_COST、A00502|EXTENT_FIELD4";
            //AddDayBalanceToArray();
            //#endregion

            //#region 退费金额
            //strValue = GetOneCellText(sheet, "A006");
            //this.SetOneCellDayBalance("A006", "退费金额", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region 作废金额
            //strValue = GetOneCellText(sheet, "A007");
            //this.SetOneCellDayBalance("A007", "作废金额", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region 暂时无数据
            //#region 押金金额
            //strValue = GetOneCellText(sheet, "A008");
            //this.SetOneCellDayBalance("A008", "押金金额", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region 退押金额
            //strValue = GetOneCellText(sheet, "A009");
            //this.SetOneCellDayBalance("A009", "退押金额", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region  减免金额
            //strValue = GetOneCellText(sheet, "A010");
            //this.SetOneCellDayBalance("A010", "减免金额", NConvert.ToDecimal(strValue), "5");
            //#endregion
            //#endregion

            //#region  四舍五入
            //strValue = GetOneCellText(sheet, "A011");
            //this.SetOneCellDayBalance("A011", "四舍五入", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region 公费医疗
            //strValue = this.GetOneCellText(sheet, "A012");
            //SetOneCellDayBalance("A012", "公费医疗", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 公费自付
            //strValue = this.GetOneCellText(sheet, "A013");
            //SetOneCellDayBalance("A013", "公费自费", NConvert.ToDecimal(strValue), "6");
            //#endregion
            //#region 公费账户
            //strValue = this.GetOneCellText(sheet, "A026");
            //SetOneCellDayBalance("A026", "公费账户", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 市保自付
            //strValue = this.GetOneCellText(sheet, "A014");
            //SetOneCellDayBalance("A014", "市保自费", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 市保账户
            //strValue = this.GetOneCellText(sheet, "A015");
            //SetOneCellDayBalance("A015", "市保账户", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 市保统筹
            //strValue = this.GetOneCellText(sheet, "A016");
            //SetOneCellDayBalance("A016", "市保统筹", NConvert.ToDecimal(strValue), "6");

            //#endregion

            //#region 市保大额
            //strValue = this.GetOneCellText(sheet, "A017");
            //SetOneCellDayBalance("A017", "市保大额", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 省保自付
            //strValue = this.GetOneCellText(sheet, "A018");
            //SetOneCellDayBalance("A018", "省保自费", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 省保账户
            //strValue = this.GetOneCellText(sheet, "A019");
            //SetOneCellDayBalance("A019", "省保账户", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 省保统筹
            //strValue = this.GetOneCellText(sheet, "A020");
            //SetOneCellDayBalance("A020", "省保统筹", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 省保大额
            //strValue = this.GetOneCellText(sheet, "A021");
            //SetOneCellDayBalance("A021", "省保大额", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 省公务员
            //strValue = this.GetOneCellText(sheet, "A022");
            //SetOneCellDayBalance("A022", "省公务员", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 上缴现金额
            //strValue = this.GetOneCellText(sheet, "A023");
            //SetOneCellDayBalance("A023", "上缴现金额", NConvert.ToDecimal(strValue), "6");

            //#endregion

            //#region 上缴支票额
            //strValue = this.GetOneCellText(sheet, "A024");
            //SetOneCellDayBalance("A024", "上缴支票额", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region 上缴银联额
            //strValue = this.GetOneCellText(sheet, "A025");
            //SetOneCellDayBalance("A025", "上缴银联额", NConvert.ToDecimal(strValue), "6");
            //#endregion
        }

        #endregion

        #region 保存日结数据
        /// <summary>
        /// 保存日结数据
        /// </summary>
        public void DayBalance()
        {
            if (MessageBox.Show("是否进行日结,日结后数据将不能恢复?", "门诊收款员缴款日报", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // 等待窗口
                FS.FrameWork.WinForms.Forms.frmWait waitForm = new FS.FrameWork.WinForms.Forms.frmWait();

                if (this.alData == null || this.prepayBalance == null)
                {
                    return;
                }
                // 启动等待窗口
                waitForm.Tip = "正在进行日结";
                waitForm.Show();

                string strEmpID = this.currentOperator.ID;
                string strOperID = this.currentOperator.ID;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int iRes = this.inpatientDayBalanceManage.DealOperDayBalance(strEmpID, strOperID, this.lastDate, this.dayBalanceDate);
                if (iRes < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("日结出错！");
                    return;
                }

                iRes = this.inpatientDayBalanceManage.InsertPrepayStat(this.prepayBalance);
                if (iRes < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("押金日结出错！");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                waitForm.Hide();
                MessageBox.Show("日结成功完成");
                //PrintInfo(this.neuPanel1);

                DialogResult dr = DialogResult.No;
                //MessageBox.Show("是否打印收费清单？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // 返回的日志记录
                    List<FS.FrameWork.Models.NeuObject> lstBalanceRecord = null;
                    iRes = this.inpatientDayBalanceManage.GetBalanceRecord(this.currentOperator.ID, lastDate, dayBalanceDate, out lstBalanceRecord);
                    if (iRes == -1)
                    {
                        MessageBox.Show("获取日志记录失败");
                        return;
                    }

                    if (lstBalanceRecord == null || lstBalanceRecord.Count <= 0)
                    {
                        MessageBox.Show("该时间段内没有要查找的数据！");
                        return;
                    }

                    // 处理收费清单
                    DataTable dtInvoice = null;
                    this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.lastDate, dayBalanceDate, out dtInvoice);
                    this.SetInvoiceDetial(lstBalanceRecord[0].ID, dtInvoice);

                    this.neuTabControl1.SelectedIndex = 2;
                    this.PrintInfo(this.pnlInvoiceDetial);
                }

                alData.Clear();
                this.InitUC();
                this.ucInpatientDayBalanceDateControl1.dtpBalanceDate.Value = this.ucInpatientDayBalanceDateControl1.dtLastBalance;
                this.QueryDayBalanceData();
            }
        }
        #endregion

        #region 事件
        private void ucOutPatientDayBalance_Load(object sender, EventArgs e)
        {
            this.InitUC();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                QueryDayBalanceData();
            }
            else
            {
                this.QueryDayBalanceRecord();
            }

            return base.OnQuery(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitUC();

            toolBarService.AddToolButton("日结", "保存日结信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "日结")
            {
                if (this.neuTabControl1.SelectedIndex == 1 || this.neuTabControl1.SelectedIndex == 2)
                {
                    MessageBox.Show("重打、收费清单界面不可以日结!");
                    return;
                }
                else
                {
                    // 日结
                    this.DayBalance();
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            switch (neuTabControl1.SelectedIndex)
            {
                case 0:
                    {
                        MessageBox.Show("请日结后打印！");
                        break;
                    }
                case 1:
                    {
                        this.PrintInfo(this.panelPrint);

                        break;
                    }
                case 2:
                    PrintInfo(this.pnlInvoiceDetial);
                    break;
            }

            return base.OnPrint(sender, neuObject);
        }

        protected virtual void PrintInfo(FS.FrameWork.WinForms.Controls.NeuPanel panelPrint)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 0, panelPrint);
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuTabControl1.SelectedIndex == 0)
            {
                // 获取最近一次日结时间
                string strLastDayBalanceDate = string.Empty;
                string strCurrentDate = string.Empty;

                int intReturn = this.inpatientDayBalanceManage.GetLastDayBalanceDate(currentOperator.ID, out strLastDayBalanceDate, out strCurrentDate);
                if (intReturn == -1)
                {
                    MessageBox.Show("获取上次日结时间失败！不能进行日结操作！");
                    return;
                }

                if (string.IsNullOrEmpty(strLastDayBalanceDate))
                {
                    this.ucInpatientDayBalanceDateControl1.dtLastBalance = DateTime.MinValue;
                    this.lastDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                    this.ucInpatientDayBalanceDateControl1.tbLastDate.Text = this.lastDate;

                }
                else
                {
                    this.ucInpatientDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(strLastDayBalanceDate);
                    this.ucInpatientDayBalanceDateControl1.tbLastDate.Text = strLastDayBalanceDate;
                    this.lastDate = strLastDayBalanceDate;
                }
                this.ucInpatientDayBalanceDateControl1.dtpBalanceDate.Value = NConvert.ToDateTime(strCurrentDate);
            }
        }
        #endregion
    }
}
