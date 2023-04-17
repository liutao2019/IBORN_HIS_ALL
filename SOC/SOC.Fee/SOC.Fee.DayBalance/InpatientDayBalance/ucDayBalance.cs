using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;

namespace SOC.Fee.DayBalance.InpatientDayBalance
{
    public partial class ucDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayBalance()
        {
            InitializeComponent();
        }

        #region 变量定义

        /// <summary>
        /// 日结操作时间
        /// </summary>
        string operateDate = "";
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 日结业务层
        /// </summary>
        SOC.Fee.DayBalance.Manager.InpatientDayBalanceManage inpatientDayBalanceManage = new SOC.Fee.DayBalance.Manager.InpatientDayBalanceManage();

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
        /// 单元格信息
        /// </summary>
        FarPoint.Win.Spread.Cell cell = null;

        #endregion

        #region 属性

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

        /// <summary>
        /// 日结格式
        /// </summary>
        private FileNameFarPoint fileName = null;
        [Description("日结数据格式"), Category("设置")]
        public FileNameFarPoint FileName
        {
            get
            {
                if (fileName == null)
                {
                    fileName = new FileNameFarPoint();
                    fileName.FarCell = this.neuSpread1_Sheet1;
                    
                }

                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitUC()
        {
            // 返回值
            int intReturn = 0;

            // 获取最近一次日结时间
            string strLastDayBalanceDate = string.Empty;
            string strCurrentDate = string.Empty;
            intReturn = this.inpatientDayBalanceManage.GetLastDayBalanceDate(this.inpatientDayBalanceManage.Operator.ID, out strLastDayBalanceDate, out strCurrentDate);
            if (intReturn == -1)
            {
                this.MessageBox("获取上次日结时间失败！不能进行日结操作！");
                return;
            }

            if (string.IsNullOrEmpty(strLastDayBalanceDate))
            {
                this.dtpLastBalanceDate.Value = this.dtpLastBalanceDate.MinDate;
            }
            else
            {
                this.dtpLastBalanceDate.Value = NConvert.ToDateTime(strLastDayBalanceDate);
            }

            this.dtpBalanceDate.Value = NConvert.ToDateTime(strCurrentDate);

            // 设置截止时间为当前服务器时间
            this.dtpDateFrom.Value = NConvert.ToDateTime(strCurrentDate);
            this.dtpDateTo.Value = NConvert.ToDateTime(strCurrentDate);

            //this.ucInpatientDayBalanceReportNew1.InitUC(this.inpatientDayBalanceManage.Hospital.Name + reportTitle);
            //this.ucInpatientDayBalanceReportNew2.InitUC(inpatientDayBalanceManage.Hospital.Name + reportTitle);
            //this.ucInpatientDayBalanceReportNew1.SetDetailName();

            //// 初始化费用清单页面
            //this.lblTitle.Text = this.inpatientDayBalanceManage.Hospital.Name + "收费清单";
            //this.lblInvoiceInfo.Text = "";
            //this.lblSumary.Text = "汇总号：";
            //this.lblOper.Text = "收费员：" + currentOperator.Name;

            //加载Fp格式
            if (this.fileName != null)
            {
                this.neuSpread1.Open(this.fileName.FileName);
                this.neuSpread2.Open(this.fileName.FileName);
            }

        }

        /// <summary>
        /// 获取日结截止时间(1：成功/-1：非法)
        /// </summary>
        /// <param name="balanceDate">返回的日结截止时间</param>
        /// <returns>1：成功/-1：非法</returns>
        private int GetBalanceDate(ref string balanceDate)
        {
            //上次日结时间
            DateTime dtLastBalance = this.dtpLastBalanceDate.Value;
            // 获取用户输入的日结截止时间
            DateTime dtInput = this.dtpBalanceDate.Value;
            // 获取当前时间
            DateTime dtNow = this.inpatientDayBalanceManage.GetDateTimeFromSysDateTime();

            // 判断用户输入的合法性
            if (dtInput > dtNow)
            {
                this.MessageBox("日结时间不能大于当前时间");
                this.dtpBalanceDate.Value = dtNow;
                this.dtpBalanceDate.Focus();
                return -1;
            }
            else if (dtInput < dtLastBalance)
            {
                this.MessageBox("日结时间不能小于上次日结时间");
                this.dtpBalanceDate.Value = dtNow;
                return -1;
            }

            // 设置返回值
            balanceDate = dtInput.ToString("yyyy-MM-dd HH:mm:ss");

            return 1;
        }

        /// <summary>
        /// 返回用户设置的日结查询时间(1：成功获取/-1：输入非法)
        /// </summary>
        /// <param name="dtFrom">用户输入的起始时间</param>
        /// <param name="dtTo">用户输入的截止时间</param>
        /// <returns>1：成功获取/-1：输入非法</returns>
        public int GetInputDateTime(ref DateTime dtFrom, ref DateTime dtTo)
        {
            //
            // 获取用户输入时间和系统当前时间
            //
            // 获取当前时间
            DateTime dtNowDateTime = this.inpatientDayBalanceManage.GetDateTimeFromSysDateTime();
            // 用户录入的起始时间
            dtFrom = this.dtpDateFrom.Value;
            // 用户录入的截止时间
            dtTo = this.dtpDateTo.Value;

            //
            // 判断用户输入的合法性
            //
            if (dtNowDateTime < dtTo)
            {
                this.MessageBox("截止日期不能大于当前日期");
                this.dtpDateTo.Value = dtNowDateTime;
                this.dtpDateTo.Focus();
                return -1;
            }

            if (dtFrom > dtTo)
            {
                this.MessageBox("起始时间不能大于开始时间");
                this.dtpLastBalanceDate.Focus();
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 查询要日结的数据
        /// </summary>
        private void QueryDayBalanceData()
        {
            string dayBalanceDate = "";
            int intReturn  = this.GetBalanceDate(ref dayBalanceDate);
            if (intReturn == -1)
            {
                return;
            }
            //显示报表信息
            string strEmpID = this.inpatientDayBalanceManage.Operator.ID;
            DataTable dtCostStat = null;
            this.inpatientDayBalanceManage.QueryDayBalanceCostByStat(strEmpID, this.dtpLastBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), dayBalanceDate, out dtCostStat);

            this.Clear(this.neuSpread1);
            //设置其他信息
            this.SetOneCellText(this.neuSpread1.Sheets[0], EnumOtherInfo.DayBalanceOper1, this.inpatientDayBalanceManage.Operator.Name);
            this.SetOneCellText(this.neuSpread1.Sheets[0], EnumOtherInfo.DayBalanceOper2, this.inpatientDayBalanceManage.Operator.Name);
            this.SetOneCellText(this.neuSpread1.Sheets[0], EnumOtherInfo.DayBalanceBeginTime, this.dtpLastBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            this.SetOneCellText(this.neuSpread1.Sheets[0], EnumOtherInfo.DayBalanceEndTime, dayBalanceDate);
            this.SetOneCellText(this.neuSpread1.Sheets[0], EnumOtherInfo.HospitalName, FS.FrameWork.Management.Connection.Hospital.Name);

            this.SetDetial(this.neuSpread1.Sheets[0],dtCostStat);



            //显示发票汇总数据
            SetInvoice(this.neuSpread1.Sheets[0],this.dtpLastBalanceDate.Value,this.dtpBalanceDate.Value);

            //显示发票起止号，张数相关信息
            DataTable dtInvoice = null;
            this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.dtpLastBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), dayBalanceDate, out dtInvoice);
            SetInvoiceBeginAndEnd(dtInvoice, this.neuSpread1.Sheets[0]);

            //显示金额数据
            this.SetMoneyValue(this.neuSpread1.Sheets[0],this.dtpLastBalanceDate.Value,this.dtpBalanceDate.Value);
        }

        /// <summary>
        /// 查询已日结数据
        /// </summary>
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
            // 查询的日记流水号
            string sequence = "";

            // 获取查询时间
            intReturn = this.GetInputDateTime(ref dtFrom, ref dtTo);
            if (intReturn == -1)
            {
                return;
            }

            intReturn = this.inpatientDayBalanceManage.GetBalanceRecord(this.inpatientDayBalanceManage.Operator.ID, dtFrom.ToString("yyyy-MM-dd HH:mm:ss"), dtTo.ToString("yyyy-MM-dd HH:mm:ss"), out lstBalanceRecord);

            if (intReturn == -1)
            {
                this.MessageBox("获取日志记录失败");
                return;
            }

            string begin = string.Empty, end = string.Empty;

            // 判断结果记录数，如果多条，那么弹出窗口让用户选择
            if (lstBalanceRecord == null || lstBalanceRecord.Count <= 0)
            {
                this.MessageBox("该时间段内没有要查找的数据！");
                return;
            }

            if (lstBalanceRecord.Count > 1)
            {
                SOC.Fee.DayBalance.Inpatient.frmConfirmBalanceRecord confirmBalanceRecord = new SOC.Fee.DayBalance.Inpatient.frmConfirmBalanceRecord();
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
            string lastDate = begin.ToString();
            string dayBalanceDate = end.ToString();

            //显示报表信息
            this.Clear(this.neuSpread2);

            string strEmpID = this.inpatientDayBalanceManage.Operator.ID;

            DataTable dtCostStat = null;
            this.inpatientDayBalanceManage.QueryDayBalanceCostByStat(strEmpID, lastDate, dayBalanceDate, out dtCostStat);

            this.SetDetial(this.neuSpread2.Sheets[0], dtCostStat);

            //设置其他信息
            this.SetOneCellText(this.neuSpread2.Sheets[0], EnumOtherInfo.DayBalanceOper1, this.inpatientDayBalanceManage.Operator.Name);
            this.SetOneCellText(this.neuSpread2.Sheets[0], EnumOtherInfo.DayBalanceOper2, this.inpatientDayBalanceManage.Operator.Name);
            this.SetOneCellText(this.neuSpread2.Sheets[0], EnumOtherInfo.DayBalanceBeginTime, lastDate);
            this.SetOneCellText(this.neuSpread2.Sheets[0], EnumOtherInfo.DayBalanceEndTime, dayBalanceDate);
            this.SetOneCellText(this.neuSpread2.Sheets[0], EnumOtherInfo.HospitalName, FS.FrameWork.Management.Connection.Hospital.Name);


            //显示发票汇总数据
            SetInvoice(this.neuSpread2.Sheets[0], FS.FrameWork.Function.NConvert.ToDateTime(begin), FS.FrameWork.Function.NConvert.ToDateTime(end));

            // 显示金额数据
            this.SetMoneyValue(this.neuSpread2.Sheets[0], FS.FrameWork.Function.NConvert.ToDateTime(begin), FS.FrameWork.Function.NConvert.ToDateTime(end));

            // 处理收费清单
            DataTable dtInvoice = null;
            this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, lastDate, dayBalanceDate, out dtInvoice);
            this.SetInvoiceDetial(sequence, dtInvoice);
            SetInvoiceBeginAndEnd(dtInvoice, this.neuSpread2.Sheets[0]);

        }

        #region 设置日结Farpoint数据

        /// <summary>
        /// 设置显示项目数据
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetDetial(FarPoint.Win.Spread.SheetView sheet,DataTable table)
        {
            if (table.Rows.Count == 0) return;

            //显示项目数据
            decimal countMoney = 0;
            decimal decTemp = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                
                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][2]);
                countMoney += decTemp;
                //获取合同单位对应的编码
                this.cell = sheet.GetCellFromTag(sheet.Cells[0, 0], table.Rows[i][0]);
                if (this.cell != null)
                {
                    decTemp += FS.FrameWork.Function.NConvert.ToDecimal(this.cell.Value);
                    this.cell.Value = decTemp.ToString("0.00");
                }
            }

            //查找汇总信息
            this.cell = sheet.GetCellFromTag(sheet.Cells[0, 0], EnumOtherInfo.TotCost.ToString());
            if (this.cell != null)
            {
                this.cell.Value = countMoney.ToString("0.00");
            }
            this.cell = sheet.GetCellFromTag(sheet.Cells[0, 0], EnumOtherInfo.TotCostUpper.ToString());
            if (this.cell != null)
            {
                this.cell.Value = FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);  
            }

        }

        /// <summary>
        /// 查询并显示发票数据 
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoice(FarPoint.Win.Spread.SheetView sheet, DateTime begin, DateTime end)
        {
            //获取发票数据
            string strEmpID = this.inpatientDayBalanceManage.Operator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }

            string effectiveBill = string.Empty;
            string uneffectiveBill = string.Empty;
            int resultValue = this.inpatientDayBalanceManage.QueryDayBalanceEffectiveBill(strEmpID, begin.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss"), out effectiveBill);
            if (resultValue == -1)
            {
                this.MessageBox(this.inpatientDayBalanceManage.Err);
                return;
            }

            this.SetOneCellText(sheet, EnumOtherInfo.EffectiveBill, effectiveBill);

            //这里只显示作废号码
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceUneffectiveBill(strEmpID, begin.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss"), out uneffectiveBill);
            if (resultValue == -1)
            {
                this.MessageBox(this.inpatientDayBalanceManage.Err);
                return;
            }

            this.SetOneCellText(sheet, EnumOtherInfo.UnEffectiveBill, uneffectiveBill);

            return;
        }

        /// <summary>
        /// 显示发票起止号，张数相关信息
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoiceBeginAndEnd(DataTable dtInvoice, FarPoint.Win.Spread.SheetView sheet)
        {
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

            //设置发票数
            this.SetOneCellText(sheet, EnumOtherInfo.BillBeginAndEnd, lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1]);
            lblInvoiceInfo.Text = "发票数：     " + lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1];
        }


        /// <summary>
        /// 设置显示金额数据
        /// </summary>
        protected virtual void SetMoneyValue(FarPoint.Win.Spread.SheetView sheet,DateTime begin,DateTime end)
        {
            int resultValue;

            string employeeID = this.inpatientDayBalanceManage.Operator.ID;
            DataTable dtBillMoney = null;
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceBillMoney(employeeID, begin.ToString("yyyy-MM-dd HH:mm:ss"),end.ToString("yyyy-MM-dd HH:mm:ss"), out dtBillMoney);

            SetPayTypeValue(sheet, dtBillMoney);

            return;

        }

        /// <summary>
        /// 按类型显示金额
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtBillMoney"></param>
        protected virtual void SetPayTypeValue(FarPoint.Win.Spread.SheetView sheet, DataTable dtBillMoney)
        {
            for (int i = 0; i < sheet.RowCount; i++)
            {
                for (int j = 0; j < sheet.ColumnCount; j++)
                {
                    if (sheet.Cells[i, j].Tag == null)
                    {
                        continue;
                    }

                    this.cell = sheet.Cells[i, j];

                    if (this.cell.Tag.ToString().StartsWith("P") == false)
                    {
                        continue;
                    }

                    for(int m=0;m<dtBillMoney.Rows.Count;m++)
                    {
                        for (int n = 1; n < dtBillMoney.Columns.Count; n++)
                        {
                            if (this.cell.Tag.ToString().Contains("P" + dtBillMoney.Rows[m][0].ToString() + "|" + dtBillMoney.Columns[n].ColumnName.ToString()))
                            {
                                decimal decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Rows[m][dtBillMoney.Columns[n]]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(this.cell.Value);
                                this.cell.Value = decTemp.ToString("0.00");
                            }
                        }
                    }
                }
            }

            //设置总金额
            decimal decYJJJS = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(prepaycost)", ""));
            decimal decCA = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(supplycost)", "")) - FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(returncost)", ""));
            decimal decSJJE = decYJJJS + decCA;
            string strTemp = FS.FrameWork.Public.String.LowerMoneyToUpper(decSJJE);
            if (decSJJE < 0)
            {
                strTemp = "负" + strTemp;
            }

            this.SetOneCellText(sheet, EnumOtherInfo.CashCost, decCA.ToString("0.00"));
            this.SetOneCellText(sheet, EnumOtherInfo.PrePayCost, decYJJJS.ToString("0.00"));
            this.SetOneCellText(sheet, EnumOtherInfo.PaidCost, decSJJE.ToString("0.00"));
            this.SetOneCellText(sheet, EnumOtherInfo.PaidCostUpper, strTemp);


        }

        /// <summary>
        /// 设置单个Cell的Text
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="tagStr">Cell的tag</param>
        /// <param name="strText">要显示的Text</param>
        private void SetOneCellText(FarPoint.Win.Spread.SheetView sheet, EnumOtherInfo tagStr, string strText)
        {
            this.cell = sheet.GetCellFromTag(null, tagStr.ToString());
            if (cell != null)
            {
                this.cell.Text = strText;
            }
        }

        #endregion

        /// <summary>
        /// 显示收费清单
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <param name="dtInvoice"></param>
        private void SetInvoiceDetial(string balanceNO, DataTable dtInvoice)
        {
            this.neuSpread3_Sheet1.RowCount = 0;
            lblSumary.Text = "汇总号：  " + balanceNO;
            lblInvoiceInfo.Text = "";

            if (dtInvoice == null || dtInvoice.Rows.Count <= 0)
            {
                return;
            }

            int iRowCount = dtInvoice.Rows.Count;
            this.neuSpread3_Sheet1.RowCount = iRowCount + 1;

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

                this.neuSpread3_Sheet1.Cells[idx, 0].Text = strTemp;
                this.neuSpread3_Sheet1.Cells[idx, 1].Text = drTemp["invoice_no"].ToString().Trim();
                this.neuSpread3_Sheet1.Cells[idx, 2].Text = drTemp["patient_no"].ToString().Trim();
                this.neuSpread3_Sheet1.Cells[idx, 3].Text = drTemp["name"].ToString().Trim();

                this.neuSpread3_Sheet1.Cells[idx, 4].Text = drTemp["balance_date"] != DBNull.Value ? FS.FrameWork.Function.NConvert.ToDateTime(drTemp["balance_date"]).ToString("yyyy.MM.dd") : "";
                this.neuSpread3_Sheet1.Cells[idx, 5].Text = drTemp["tot_cost"].ToString();
                this.neuSpread3_Sheet1.Cells[idx, 6].Text = drTemp["eco_cost"].ToString();
                this.neuSpread3_Sheet1.Cells[idx, 7].Text = drTemp["owncost"].ToString();
                this.neuSpread3_Sheet1.Cells[idx, 8].Text = drTemp["pub_cost"].ToString();
                this.neuSpread3_Sheet1.Cells[idx, 9].Text = drTemp["empl_name"].ToString().Trim();
                this.neuSpread3_Sheet1.Cells[idx, 10].Text = drTemp["trans_type"].ToString() == "1" ? "" : "作废";
            }
            // 显示汇总信息
            this.neuSpread3_Sheet1.Cells[idx, 4].Text = "合计：";
            this.neuSpread3_Sheet1.Cells[idx, 5].Text = dtInvoice.Compute("Sum(tot_cost)", "").ToString();
            this.neuSpread3_Sheet1.Cells[idx, 6].Text = dtInvoice.Compute("Sum(eco_cost)", "").ToString();
            this.neuSpread3_Sheet1.Cells[idx, 7].Text = dtInvoice.Compute("Sum(owncost)", "").ToString();
            this.neuSpread3_Sheet1.Cells[idx, 8].Text = dtInvoice.Compute("Sum(pub_cost)", "").ToString();

            //增加一些人员信息
            this.neuSpread3_Sheet1.Rows.Add(this.neuSpread3_Sheet1.RowCount, 1);
            this.neuSpread3_Sheet1.Models.Span.Add(this.neuSpread3_Sheet1.RowCount - 1, 0, 1, 11);
            this.neuSpread3_Sheet1.Cells[this.neuSpread3_Sheet1.RowCount - 1, 0].Text = "会计:" + "                           " + "出纳:" + "                           " + "复合:" + "                           " + "收费员:" + this.inpatientDayBalanceManage.Operator.Name;

            lblInvoiceInfo.Text = "发票数：     " + lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1];

        }

        private void MessageBox(string errInfo)
        {
            System.Windows.Forms.MessageBox.Show(this, errInfo, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Clear(FS.FrameWork.WinForms.Controls.NeuSpread sheet)
        {
            //清空数据
            this.lblInvoiceInfo.Text = "";
            this.lblSumary.Text = "汇总号：";
            this.neuSpread3_Sheet1.RowCount = 0;
            //加载Fp格式
            if (this.fileName != null)
            {
                sheet.Open(this.fileName.FileName);
            }
            else
            {
                for (int i = 0; i < sheet.Sheets[0].RowCount; i++)
                {
                    for (int j = 0; j < sheet.Sheets[0].ColumnCount; j++)
                    {
                        if (sheet.Sheets[0].Cells[i, j].Tag == null)
                        {
                            continue;
                        }

                        this.cell = sheet.Sheets[0].Cells[i, j];
                        this.cell.Text = "";
                    }
                }
            }
        }

        #endregion

        #region 保存日结数据
        /// <summary>
        /// 保存日结数据
        /// </summary>
        public void DayBalance()
        {
            if (System.Windows.Forms.MessageBox.Show("是否进行日结,日结后数据将不能恢复?", "住院收款员缴款日报>>", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // 等待窗口
                FS.FrameWork.WinForms.Forms.frmWait waitForm = new FS.FrameWork.WinForms.Forms.frmWait();

                //if (this.alData == null)
                //{
                //    return;
                //}
                // 启动等待窗口
                waitForm.Tip = "正在进行日结...";
                waitForm.Show();

                this.QueryDayBalanceData();

                string strEmpID = this.inpatientDayBalanceManage.Operator.ID;
                string strOperID = this.inpatientDayBalanceManage.Operator.ID;
               
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int iRes = this.inpatientDayBalanceManage.DealOperDayBalance(strEmpID, strOperID, this.dtpLastBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                if (iRes < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    this.MessageBox("日结出错！");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                waitForm.Hide();
                this.MessageBox("日结成功完成");
                PrintInfo(this.pnlInvoiceDetial);

                if (System.Windows.Forms.MessageBox.Show("是否打印收费清单？", "系统提示>>", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    // 返回的日志记录
                    List<FS.FrameWork.Models.NeuObject> lstBalanceRecord = null;
                    iRes = this.inpatientDayBalanceManage.GetBalanceRecord(this.inpatientDayBalanceManage.Operator.ID, this.dtpLastBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), out lstBalanceRecord);
                    if (iRes == -1)
                    {
                        this.MessageBox("获取日志记录失败");
                        return;
                    }
                        
                    if (lstBalanceRecord == null || lstBalanceRecord.Count <= 0)
                    {
                        this.MessageBox("该时间段内没有要查找的数据！");
                        return;
                    }                   

                    // 处理收费清单
                    DataTable dtInvoice = null;
                    this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.dtpLastBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpBalanceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), out dtInvoice);
                    this.SetInvoiceDetial(lstBalanceRecord[0].ID, dtInvoice);

                    this.neuTabControl1.SelectedIndex = 2;
                    this.PrintInfo(this.pnlInvoiceDetial);
                }

                this.InitUC();
                this.QueryDayBalanceData();
            }
        }
        #endregion


        #region 取消最近一次日结
        /// <summary>
        /// 取消最近一次日结
        /// </summary>
        public void UnDoDayBalance()
        {
            FS.FrameWork.Models.NeuObject balance = null;

            string strOperID = this.inpatientDayBalanceManage.Operator.ID;
            int iRes = this.inpatientDayBalanceManage.QueryLastBalanceRecord(strOperID, out balance);
            if (iRes <= 0)
            {
                this.MessageBox(this.inpatientDayBalanceManage.Err);
                return;
            }

            if (balance.User02 == "1")
            {
                MessageBox("最近一次日结信息已审核，不允许取消！");

                return;
            }

            iRes = this.inpatientDayBalanceManage.UnDoOperDayBalance(balance.ID, balance.Name, balance.Memo);

            if (iRes <= 0)
            {
                MessageBox("操作失败！" + this.inpatientDayBalanceManage.Err);
            }
            else
            {
                MessageBox("操作成功！");

                this.InitUC();
                this.QueryDayBalanceData();
            }
        }

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
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
            toolBarService.AddToolButton("日结", "保存日结信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("取消日结", "取消最近一次日结信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H合并取消, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "日结")
            {
                if (this.neuTabControl1.SelectedIndex == 1 || this.neuTabControl1.SelectedIndex == 2)
                {
                    this.MessageBox("重打、收费清单界面不可以日结!");
                    return;
                }
                else
                {
                    // 日结
                    this.DayBalance();
                }
            }
            else if (e.ClickedItem.Text == "取消日结")
            {
                if (System.Windows.Forms.MessageBox.Show("是否取消最后一次日结信息？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }

                UnDoDayBalance();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            switch (neuTabControl1.SelectedIndex)
            {
                case 0:
                    {
                        this.MessageBox("请日结后打印！");
                        break;
                    }
                case 1:
                    {
                        this.PrintInfo(this.gbDayBalanced);

                        break;
                    }
                case 2:
                    PrintInfo(this.pnlInvoiceDetial);
                    break;
            }

            return base.OnPrint(sender, neuObject);
        }

        protected virtual void PrintInfo(System.Windows.Forms.Control panelPrint)
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

                int intReturn = this.inpatientDayBalanceManage.GetLastDayBalanceDate(this.inpatientDayBalanceManage.Operator.ID, out strLastDayBalanceDate, out strCurrentDate);
                if (intReturn == -1)
                {
                    this.MessageBox("获取上次日结时间失败！不能进行日结操作！");
                    return;
                }

                if (string.IsNullOrEmpty(strLastDayBalanceDate))
                {
                    this.dtpLastBalanceDate.Value = this.dtpLastBalanceDate.MinDate;

                }
                else
                {
                    this.dtpLastBalanceDate.Value = NConvert.ToDateTime(strLastDayBalanceDate);
                }
                this.dtpBalanceDate.Value = NConvert.ToDateTime(strCurrentDate);
            }
        }

        #endregion

    }


    public enum EnumOtherInfo
    {
        /// <summary>
        /// 项目总金额
        /// </summary>
        TotCost,
        /// <summary>
        /// 项目总金额大写
        /// </summary>
        TotCostUpper,
        /// <summary>
        /// 有效发票号
        /// </summary>
        EffectiveBill,
        /// <summary>
        /// 作废发票号
        /// </summary>
        UnEffectiveBill,
        /// <summary>
        /// 发票起始号结束号
        /// </summary>
        BillBeginAndEnd,
        /// <summary>
        /// 开始时间
        /// </summary>
        DayBalanceBeginTime,
        /// <summary>
        /// 结束时间
        /// </summary>
        DayBalanceEndTime,
        /// <summary>
        /// 制表人1
        /// </summary>
        DayBalanceOper1,
        /// <summary>
        /// 制表人2
        /// </summary>
        DayBalanceOper2,
        /// <summary>
        /// 医院名称
        /// </summary>
        HospitalName,

        /// <summary>
        /// 预交金额
        /// </summary>
        PrePayCost,
        /// <summary>
        /// 现金金额
        /// </summary>
        CashCost,
        /// <summary>
        /// 上缴金额
        /// </summary>
        PaidCost,
        /// <summary>
        /// 上缴金额大写
        /// </summary>
        PaidCostUpper
        
    }

    [Serializable]
    [TypeConverter(typeof(FarPointTypeConverter))]
    [Editor(typeof(SetInfoUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class FileNameFarPoint
    {
        private string fileName = "";
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        private FarPoint.Win.Spread.SheetView farCell = null;

        public FarPoint.Win.Spread.SheetView FarCell
        {
            get
            {
                return farCell;
            }
            set
            {
                farCell = value;
            }
        }

        public override string ToString()
        {
            return fileName;
        }
    }

    public class FarPointTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is String && string.IsNullOrEmpty(value.ToString()) == false)
            {
                FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                //sheet.OpenSpreadFile(value.ToString());
                FileNameFarPoint f = new FileNameFarPoint();
                f.FileName = value.ToString();
                f.FarCell = sheet;
                return f;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
            {
                return "";
            }

            if (value is FileNameFarPoint)
            {
                return ((FileNameFarPoint)value).ToString();
            }

            return "";
        }
    }

    public class SetInfoUITypeEditor : System.Drawing.Design.UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (value is FileNameFarPoint)
            {
                FarPoint.Win.Spread.Design.DesignerMain c = new FarPoint.Win.Spread.Design.DesignerMain();
                FileNameFarPoint a = value as FileNameFarPoint;
                if (string.IsNullOrEmpty(a.FileName))
                {
                    c.Spread.Sheets[0] = a.FarCell;
                    c.Tag = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\" + this.GetType().Name + ".xml";
                    a.FileName = c.Tag.ToString();
                }
                else
                {
                    c.Spread.Open(a.FileName);
                    c.Tag = a.FileName;
                }
                c.FormClosing += new FormClosingEventHandler(c_FormClosing);
                c.ShowDialog();
                
                return base.EditValue(context, provider, a);
            }

            return base.EditValue(context, provider, value);
        }

        void c_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FarPoint.Win.Spread.Design.DesignerMain)sender).SaveFile(((FarPoint.Win.Spread.Design.DesignerMain)sender).Tag.ToString());
        }


        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}

